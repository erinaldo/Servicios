Imports System.IO
Imports System.Threading
Public Class frmRestaurantePrincipal
    Private IdMesaOrigen As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim frm As New frmRestauranteAcomodarMesas(GlobalIdSucursalDefault)
        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Consultar()
        End If
    End Sub

    Private Sub Consultar()
        pnlMesasFijo.Controls.Clear()
        pnlMesasFlujo.Controls.Clear()
        pnlPedidos.Controls.Clear()
        Dim mesasController As New dbRestauranteMesas(MySqlcon, GlobalIdSucursalDefault)
        For Each r As RestauranteSeccion In pnlSecciones.Controls
            If r.Checked Then
                For Each m As RestauranteMesa In mesasController.Mesas(r.Id, -1)
                    AddHandler m.Click, AddressOf RestauranteMesa_Click
                    ToolTip1.SetToolTip(m, [Enum].GetName(GetType(EstadosMesas), m.Estado))
                    pnlMesasFijo.Controls.Add(m)
                Next
                For Each m As RestauranteMesa In mesasController.Mesas(r.Id, -1)
                    AddHandler m.Click, AddressOf RestauranteMesa_Click
                    ToolTip1.SetToolTip(m, [Enum].GetName(GetType(EstadosMesas), m.Estado))
                    pnlMesasFlujo.Controls.Add(m)
                Next
            End If
        Next
        Dim ventas As New dbRestauranteVentas(MySqlcon)
        For Each p As RestaurantePedido In ventas.Pedidos()
            AddHandler p.Click, AddressOf RestaurantePedido_Click
            ToolTip1.SetToolTip(p, p.Text)
            pnlPedidos.Controls.Add(p)
        Next
    End Sub

    Private Sub frmRestaurantePrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim config As New dbRestauranteConfiguracion(1, MySqlcon)
        config.llenaDatos()

        Dim secciones As New dbRestauranteSecciones(MySqlcon)
        Try
            pnlMesasFijo.BackgroundImage = Image.FromFile(secciones.rutaMapa)
        Catch

        End Try

        GlobalUsuarioActivado = True
        For Each r As RadioButton In secciones.ListaSecciones(GlobalIdSucursalDefault)
            AddHandler r.CheckedChanged, AddressOf rdbSeccion_CheckedChanged
            pnlSecciones.Controls.Add(r)
        Next
        If pnlSecciones.Controls.Count > 0 Then
            DirectCast(pnlSecciones.Controls(0), RadioButton).Checked = True
        End If

        Me.BackColor = Color.FromArgb(config.colorVentanas)
        PanelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)

        Dim Us As New dbUsuarios(GlobalIdUsuario, MySqlcon)
        Dim vendedores As New dbVendedores(Us.IdVendedor, MySqlcon)
        Me.Show()
        If Us.IdVendedor <= 0 Then
            MsgBox("Este usuario no tiene asociado ningun vendedor, no podrá realizar ninguna operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            GlobalUsuarioActivado = False
        End If

    End Sub

    Private Sub LogIn()
        Dim f As New frmCambioUsuario(0, 0)
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPass.Text = GlobalUsuario
            GlobalUsuarioActivado = True
            Dim us As New dbUsuarios(GlobalIdUsuario, MySqlcon)
            If us.IdVendedor <= 0 Then
                MsgBox("Este usuario no tiene asociado ningun vendedor, no podrá realizar ninguna operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                GlobalUsuarioActivado = False
            End If
        End If
        f.Dispose()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click
        Dim f As New frmCambioUsuario(0, 0)
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPass.Text = GlobalUsuario

        End If
        f.Dispose()
    End Sub

    Private Sub txtPass_Enter(sender As Object, e As EventArgs) Handles txtPass.Enter
        Dim config As New dbRestauranteConfiguracion(1, MySqlcon)
        If config.activarTeclado Then
            Dim t As frmRestauranteTeclado = frmRestauranteTeclado.Instanciar(txtPass)
            t.Show()
            t.BringToFront()
        End If

    End Sub

    Private Sub frmRestaurantePrincipal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        GlobalEstadoRestaurante = "Cerrado"
    End Sub

    Private Sub RestaurantePedido_Click(sender As Object, e As EventArgs)
        Dim IdVenta As Integer = DirectCast(sender, RestaurantePedido).Id
        Dim ventanaOrden As New frmRestauranteOrden(0, IdVenta, GlobalUsuarioIdVendedor, GlobalIdSucursalDefault)
        ventanaOrden.ShowDialog()

        GlobalUsuarioActivado = False

        Consultar()
    End Sub

    Private Sub RestauranteMesa_Click(sender As Object, e As EventArgs)
        If Not GlobalUsuarioActivado Then
            Dim f As New frmCambioUsuario(0, 0)
            If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                GlobalUsuarioActivado = True
            End If
        End If
        If GlobalUsuarioActivado Then
            Dim IdMesa As Integer = DirectCast(sender, RestauranteMesa).Id
            Dim IdSucursal As Integer = DirectCast(sender, RestauranteMesa).IdSucursal
            Dim estadoMesa As EstadosMesas = DirectCast(sender, RestauranteMesa).Estado
            If rdbOrdenar.Checked Then
                Dim ventanaOrden As New frmRestauranteOrden(IdMesa, 0, GlobalUsuarioIdVendedor, IdSucursal)
                DirectCast(sender, RestauranteMesa).Mesero = New dbVendedores(GlobalUsuarioIdVendedor, MySqlcon)
                ventanaOrden.ShowDialog()
                GlobalUsuarioActivado = False
                Consultar()

            ElseIf rdbPagar.Checked Then
                Dim ventas As New dbRestauranteVentas(MySqlcon)
                If ventas.checaPendientes(0, IdMesa) Then
                    MsgBox("Exsisten platillos sin envíar.")
                Else
                    Dim f As New frmRestaurantePuntoVenta(IdMesa, 0, , True)
                    f.ShowDialog()
                    GlobalUsuarioActivado = False
                    rdbOrdenar.Checked = True
                    Consultar()
                End If

            ElseIf rdbReservar.Checked Then
                Dim f As New frmRestauranteReservaciones(IdMesa)
                If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Consultar()
                    rdbOrdenar.Checked = True
                End If

            ElseIf rdbCambiarMesa.Checked Then
                If IdMesaOrigen = 0 Then
                    IdMesaOrigen = IdMesa
                    For Each m As RestauranteMesa In pnlMesasFijo.Controls
                        m.Enabled = m.Estado <> EstadosMesas.Ocupada
                    Next
                    For Each m As RestauranteMesa In pnlMesasFlujo.Controls
                        m.Enabled = m.Estado <> EstadosMesas.Ocupada
                    Next
                Else
                    Dim ventas As New dbRestauranteVentas(MySqlcon)
                    ventas.CambiarMesa(IdMesaOrigen, IdMesa)
                    Consultar()
                    rdbOrdenar.Checked = True

                End If
            End If
        End If
    End Sub

    Private Sub rdbSeccion_CheckedChanged(sender As Object, e As EventArgs)
        If GlobalUsuarioActivado = False Then LogIn()
        If GlobalUsuarioActivado Then
            If DirectCast(sender, RadioButton).Checked Then
                Consultar()

                Try
                    Dim secciones As New dbRestauranteSecciones
                    pnlMesasFijo.BackgroundImage = Image.FromFile(secciones.rutaMapa)
                Catch

                End Try
            End If
        End If
    End Sub

    Private Sub btnPedidos_Click(sender As Object, e As EventArgs) Handles btnPedidos.Click
        If Not GlobalUsuarioActivado Then
            Dim f As New frmCambioUsuario(0, 0)
            If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                GlobalUsuarioActivado = True
            End If
        End If
        If GlobalUsuarioActivado Then
            Dim ventanaOrden As New frmRestauranteOrden(0, 0, GlobalUsuarioIdVendedor, GlobalIdSucursalDefault)
            ventanaOrden.ShowDialog()

            GlobalUsuarioActivado = False

            Consultar()
        End If
    End Sub

    Private Sub rdbVender_CheckedChanged(sender As Object, e As EventArgs) Handles rdbOrdenar.CheckedChanged, rdbPagar.CheckedChanged, rdbReservar.CheckedChanged, rdbCambiarMesa.CheckedChanged
        If rdbOrdenar.Checked Then
            IdMesaOrigen = 0
            For Each m As RestauranteMesa In pnlMesasFijo.Controls
                m.Enabled = True
            Next
            For Each m As RestauranteMesa In pnlMesasFlujo.Controls
                m.Enabled = True
            Next
        ElseIf rdbCambiarMesa.Checked Then
            For Each m As RestauranteMesa In pnlMesasFijo.Controls
                m.Enabled = m.Estado = EstadosMesas.Ocupada
            Next
            For Each m As RestauranteMesa In pnlMesasFlujo.Controls
                m.Enabled = m.Estado = EstadosMesas.Ocupada
            Next
        ElseIf rdbReservar.Checked Then
            For Each m As RestauranteMesa In pnlMesasFijo.Controls
                m.Enabled = True
            Next
            For Each m As RestauranteMesa In pnlMesasFlujo.Controls
                m.Enabled = True
            Next
        ElseIf rdbPagar.Checked Then
            For Each m As RestauranteMesa In pnlMesasFijo.Controls
                m.Enabled = m.Estado = EstadosMesas.Ocupada
            Next
            For Each m As RestauranteMesa In pnlMesasFlujo.Controls
                m.Enabled = m.Estado = EstadosMesas.Ocupada
            Next
        End If
    End Sub
End Class