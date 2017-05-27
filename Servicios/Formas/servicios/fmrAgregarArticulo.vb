Public Class fmrAgregarArticulo
    Public idServicio As Integer
    Public totalFull As Double
    Public idEquipo As Integer
    Dim idInventario As Integer
    Dim iniciando As Integer = 0
    Dim totalConsumidos As Double
    Dim idAnadido As Integer
    Dim TipoEquipo As Integer
    Public Sub New(ByVal pIdServicio As Integer, ByVal pIdEquipo As Integer, ByVal pTipoEquipo As Integer)
        InitializeComponent()
        idServicio = pIdServicio
        idEquipo = pIdEquipo
        TipoEquipo = pTipoEquipo

    End Sub
    Private Sub fmrAgregarArticulo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'txtbusqueda.Text = ""
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        nuevo()
        filtros()
        filtroArticulosAnadidos()
        iniciando = 1
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
      RegresaValor()

    End Sub
    Private Sub RegresaValor()
        Try
            totalFull = Double.Parse(lblTotalArticulos.Text)
            Me.DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub filtros()
        If txtbusqueda.Text = "" Then
            'filtro todos
            Try
                Dim P As New dbServiciosEventos(MySqlcon)
                Dim a As New dbInventarioPrecios(MySqlcon)
                Dim TablaFull As New DataTable
                Dim Tabla As New DataTable
                TablaFull.Columns.Add("ID")
                TablaFull.Columns.Add("Código")
                TablaFull.Columns.Add("Nombre")
                TablaFull.Columns.Add("Precio")

                ' Tabla = P.filtroInventario(txtbusqueda.Text).ToTable

                DGInventario2.DataSource = P.filtroInventario(txtbusqueda.Text).ToTable



                'For i As Integer = 0 To Tabla.Rows.Count() - 1
                '    Dim dr As DataRow

                '    dr = TablaFull.NewRow()
                '    dr("ID") = Tabla.Rows(i)(0).ToString
                '    dr("Código") = Tabla.Rows(i)(1).ToString
                '    dr("Nombre") = Tabla.Rows(i)(2).ToString
                '    a.BuscaPrecio(Tabla.Rows(i)(0).ToString, 1)
                '    dr("Precio") = Format(a.Precio, "0.00")
                '    TablaFull.Rows.Add(dr)
                'Next

                ' DGInventario2.DataSource = TablaFull
                DGInventario2.Columns(0).Visible = False
                DGInventario2.Columns(1).HeaderText = "Código"
                DGInventario2.Columns(2).HeaderText = "Nombre"
                DGInventario2.Columns(3).HeaderText = "Precio"
                DGInventario2.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                total()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try

        Else
            'filtro específico
            Try
                Dim P As New dbServiciosEventos(MySqlcon)
                Dim a As New dbInventarioPrecios(MySqlcon)
                Dim TablaFull As New DataTable
                Dim Tabla As New DataTable
                TablaFull.Columns.Add("ID")
                TablaFull.Columns.Add("Código")
                TablaFull.Columns.Add("Nombre")
                TablaFull.Columns.Add("Costo")
                Tabla = P.filtroInventario(txtbusqueda.Text).ToTable

                For i As Integer = 0 To Tabla.Rows.Count() - 1
                    Dim dr As DataRow

                    dr = TablaFull.NewRow()
                    dr("ID") = Tabla.Rows(i)(0).ToString
                    dr("Código") = Tabla.Rows(i)(1).ToString
                    dr("Nombre") = Tabla.Rows(i)(2).ToString
                    a.BuscaPrecio(Tabla.Rows(i)(0).ToString, 1)
                    dr("Costo") = Format(a.Precio, "0.00")
                    TablaFull.Rows.Add(dr)
                Next

                DGInventario2.DataSource = TablaFull

                '  DGInventario2.DataSource = P.filtroInventario(txtbusqueda.Text)

                DGInventario2.Columns(0).Visible = False
                DGInventario2.Columns(1).HeaderText = "Código"
                DGInventario2.Columns(2).HeaderText = "Nombre"
                DGInventario2.Columns(3).HeaderText = "Precio"
                DGInventario2.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                total()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try



        End If
    End Sub

  

    Private Sub txtbusqueda_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbusqueda.TextChanged
        filtros()
    End Sub

    Private Sub DGInventario2_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGInventario2.CellClick
        LlenaDatosEventos()
        btnagregar.Text = "Agregar"
        Button6.Enabled = False
    End Sub
    Private Sub LlenaDatosEventos()
        Try
            ' ConsultaOn = False
            idInventario = DGInventario2.Item(0, DGInventario2.CurrentCell.RowIndex).Value
            txtCodigo.Text = DGInventario2.Item(1, DGInventario2.CurrentCell.RowIndex).Value
            txtNombre.Text = DGInventario2.Item(2, DGInventario2.CurrentCell.RowIndex).Value
            txtCantidad.Value = 1
            txtPrecio.Text = DGInventario2.Item(3, DGInventario2.CurrentCell.RowIndex).Value
            total()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnagregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnagregar.Click
        Try
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            txtCodigo.BackColor = Color.White
            txtNombre.BackColor = Color.White
            Dim ano As String
            Dim mes As String
            Dim dia As String
            Dim fecha As String
            ano = Date.Now.Year.ToString()
            mes = Date.Now.Month.ToString()
            dia = Date.Now.Day.ToString()
            Dim P As New dbServiciosEventos(MySqlcon)
            If txtCodigo.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + "Debe indicar un código para el artículo."
                txtCodigo.BackColor = Color.FromArgb(250, 150, 150)
                '  txtNombre.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If txtNombre.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + "Debe indicar un nombre para el artículo."
                '  txtCodigo.BackColor = Color.FromArgb(250, 150, 150)
                txtNombre.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If NoErrores = True Then
                If btnagregar.Text = "Agregar" Then
                    Dim total As Double
                    Dim q As New dbServicios(MySqlcon)
                    Dim idEquipo As Integer
                    If TipoEquipo = 0 Then
                        idEquipo = q.ConsultaIdEquipo(idServicio)
                    Else
                        idEquipo = q.ConsultaIdEquiposuc(idServicio)
                    End If


                    fecha = dtpFecha.Value.ToString("yyyy/MM/dd")
                    total = Double.Parse(txtPrecio.Text) * Integer.Parse(txtCantidad.Value)
                    If TipoEquipo = 0 Then
                        'Inventario para clientes
                        P.GuardarArticulo(idServicio, idInventario, Integer.Parse(txtCantidad.Value), Double.Parse(txtPrecio.Text), total, idEquipo, fecha)
                    Else
                        'inventario para sucursales
                        P.GuardarArticulosuc(idServicio, idInventario, Integer.Parse(txtCantidad.Value), Double.Parse(txtPrecio.Text), total, idEquipo, fecha)
                    End If

                    nuevo()
                    PopUp("Evento Agregado", 90)

                Else
                    Dim total As Double
                    Dim q As New dbServicios(MySqlcon)
                    Dim idEquipo As Integer
                    'Dim fechita As String
                    If TipoEquipo = 0 Then
                        idEquipo = q.ConsultaIdEquipo(idServicio)
                    Else
                        idEquipo = q.ConsultaIdEquiposuc(idServicio)
                    End If


                    fecha = dtpFecha.Value.ToString("yyyy/MM/dd")
                    total = Double.Parse(txtPrecio.Text) * Integer.Parse(txtCantidad.Value)
                    ' fechita = P.buscarFecha(idAnadido)
                    If TipoEquipo = 0 Then
                        P.ModificarAnadido(idAnadido, idServicio, idInventario, Integer.Parse(txtCantidad.Value), Double.Parse(txtPrecio.Text), total, idEquipo, fecha)
                    Else
                        P.ModificarAnadidosuc(idAnadido, idServicio, idInventario, Integer.Parse(txtCantidad.Value), Double.Parse(txtPrecio.Text), total, idEquipo, fecha)
                    End If


                    PopUp("Evento Modificado", 90)
                    nuevo()
                End If
            End If
            If NoErrores = False Then
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub total()
        Dim total As Double
        Dim tamano As Integer
        Dim cosa As String
        tamano = txtPrecio.Text.Length()
        cosa = txtPrecio.Text
        If txtPrecio.Text = "" Then
            lblTotal.Text = "0.00"
        Else
            If cosa.Chars(tamano - 1) = "." And tamano > 1 Then
                cosa = txtPrecio.Text + "0"
                '  txtPrecio.Text = cosa
                total = Double.Parse(cosa) * Integer.Parse(txtCantidad.Value)
                lblTotal.Text = Format(total, "0.00")
                txtPrecio.Select(txtPrecio.Text.Length, 0)
            Else
                If cosa.Chars(tamano - 1) = "." And tamano = 1 Then
                    cosa = "0" + txtPrecio.Text
                    txtPrecio.Text = cosa
                    txtPrecio.Select(txtPrecio.Text.Length, 0)
                End If

                total = Double.Parse(txtPrecio.Text) * Integer.Parse(txtCantidad.Value)
                lblTotal.Text = Format(total, "0.00")
            End If
        End If
    End Sub

    Private Sub txtPrecio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrecio.Click
        txtPrecio.SelectAll()
    End Sub

    Private Sub txtPrecio_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrecio.Enter
        txtPrecio.SelectAll()
    End Sub

    Private Sub txtPrecio_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrecio.KeyDown
        If txtPrecio.Text = "0.00" Then
            txtPrecio.Text = ""
        End If
    End Sub

    Private Sub txtPrecio_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrecio.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If

    End Sub

    Private Sub txtPrecio_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrecio.Leave
        Dim x As Double
        If txtPrecio.Text = "." Then
            txtPrecio.Text = "0.00"
        End If
        If txtPrecio.Text = "" Then
            txtPrecio.Text = "0.00"
        Else
            x = Double.Parse(txtPrecio.Text)
            If x >= 2000.0 Then
                'Leyenda.Checked = True
            Else
                '  Leyenda.Checked = False
            End If

            txtPrecio.Text = Format(x, "0.00")
        End If

    End Sub

    Private Sub txtPrecio_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrecio.TextChanged

    End Sub
    Function obText() As String
        Return txtPrecio.Text
    End Function

    Public Sub insText(ByVal dato As String)
        txtPrecio.Text = dato
    End Sub

    Public Sub txtPrecio_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrecio.DoubleClick
        txtPrecio.DeselectAll()
    End Sub

    Private Sub txtPrecio_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrecio.KeyUp
        total()
    End Sub

    Private Sub txtCantidad_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.ValueChanged
        If iniciando = 1 Then
            total()
        End If

    End Sub
    Private Sub nuevo()
        txtCodigo.Text = ""
        txtNombre.Text = ""
        txtCantidad.Value = 1
        txtPrecio.Text = "0.00"
        lblTotal.Text = "0.00"
        Button6.Enabled = False
        btnagregar.Text = "Agregar"
        txtbusqueda.Text = ""
        dtpFecha.Value = Date.Now()
        filtros()
        filtroArticulosAnadidos()
        txtbusqueda.Focus()
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            ' If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos) = True Then
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim P As New dbServiciosEventos(MySqlcon)
                If TipoEquipo = 0 Then
                    P.EliminarAnadido(idAnadido)
                Else
                    P.EliminarAnadidosuc(idAnadido)
                End If

                nuevo()
                
                PopUp("Eliminado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub filtroArticulosAnadidos()
        'filtro todos
        Try
            totalConsumidos = 0
            Dim P As New dbServiciosEventos(MySqlcon)
            Dim TablaFull As New DataTable
            Dim Tabla As New DataTable

            TablaFull.Columns.Add("ID")
            TablaFull.Columns.Add("idEvento")
            TablaFull.Columns.Add("idInventario")
            TablaFull.Columns.Add("Codigo")
            TablaFull.Columns.Add("Nombre")
            TablaFull.Columns.Add("Precio")
            TablaFull.Columns.Add("Cantidad")
            TablaFull.Columns.Add("Total")
            TablaFull.Columns.Add("Fecha")
            If TipoEquipo = 0 Then
                Tabla = P.filtroArticulosConsumidos(idServicio)
            Else
                Tabla = P.filtroArticulosConsumidossuc(idServicio)
            End If


            For i As Integer = 0 To Tabla.Rows.Count() - 1
                Dim dr As DataRow

                dr = TablaFull.NewRow()
                dr("ID") = Tabla.Rows(i)(0).ToString
                dr("idEvento") = Tabla.Rows(i)(1).ToString
                dr("idInventario") = Tabla.Rows(i)(2).ToString
                P.InventarioUtilizado2(Integer.Parse(Tabla.Rows(i)(2).ToString))
                dr("Codigo") = P.codigoInventario
                dr("Nombre") = P.nombreInvenario
                dr("Precio") = "$" + Format(Tabla.Rows(i)(3), "0.00")
                dr("Cantidad") = Tabla.Rows(i)(4).ToString
                dr("Total") = "$" + Format(Tabla.Rows(i)(5), "0.00")
                dr("Fecha") = Format(Tabla.Rows(i)(7))
                TablaFull.Rows.Add(dr)
                totalConsumidos = totalConsumidos + Double.Parse(Tabla.Rows(i)(5).ToString)
            Next

            lblTotalArticulos.Text = Format(totalConsumidos, "0.00")

            DGInventario.DataSource = TablaFull
            DGInventario.Columns(0).Visible = False
            DGInventario.Columns(1).Visible = False
            DGInventario.Columns(2).Visible = False

            DGInventario.Columns(3).HeaderText = "Código"
            DGInventario.Columns(4).HeaderText = "Nombre"
            DGInventario.Columns(5).HeaderText = "Precio"
            DGInventario.Columns(6).HeaderText = "Cant."
            DGInventario.Columns(7).HeaderText = "Total"
            DGInventario.Columns(3).Width = 50
            DGInventario.Columns(6).Width = 50
            DGInventario.Columns(8).Width = 70
            DGInventario.Columns(5).Width = 70
            DGInventario.Columns(7).Width = 70
            DGInventario.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGInventario.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DGInventario.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGInventario.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGInventario.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            DGInventario.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub filtroArticulos()
        Try
            ' ConsultaOn = False
            Dim P As New dbServiciosEventos(MySqlcon)
            idAnadido = DGInventario.Item(0, DGInventario.CurrentCell.RowIndex).Value
            If TipoEquipo = 0 Then
                P.llenarDatosArticulos(idAnadido)
            Else
                P.llenarDatosArticulossuc(idAnadido)
            End If

            txtCodigo.Text = DGInventario.Item(3, DGInventario.CurrentCell.RowIndex).Value
            txtNombre.Text = DGInventario.Item(4, DGInventario.CurrentCell.RowIndex).Value
            dtpFecha.Value = DGInventario.Item(8, DGInventario.CurrentCell.RowIndex).Value
            txtCantidad.Value = Integer.Parse(DGInventario.Item(6, DGInventario.CurrentCell.RowIndex).Value)
            idInventario = P.pidInventario
            If TipoEquipo = 0 Then
                P.consultaPrecio(idAnadido)
            Else
                P.consultaPreciosuc(idAnadido)
            End If

            txtPrecio.Text = Format(P.Precio, "0.00")
            total()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGInventario_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGInventario.CellClick
        filtroArticulos()
        btnagregar.Text = "Modificar"
        Button6.Enabled = True
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevo()
    End Sub

    Private Sub fmrAgregarArticulo_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        RegresaValor()
    End Sub

    Private Sub btnconsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnconsultar.Click
        Dim P As New dbServicios(MySqlcon)
        Dim idEquipo As Integer
        If TipoEquipo = 0 Then
            idEquipo = P.ConsultaIdEquipo(idServicio)
        Else
            idEquipo = P.ConsultaIdEquiposuc(idServicio)
        End If

        Dim B As New frmComponentes(idEquipo, TipoEquipo)
        B.ShowDialog()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim P As New dbServicios(MySqlcon)
        Dim idEquipo As Integer
        If TipoEquipo = 0 Then
            idEquipo = P.ConsultaIdEquipo(idServicio)
        Else
            idEquipo = P.ConsultaIdEquiposuc(idServicio)
        End If

        Dim B As New frmServiciosArticulosHistorial(idEquipo, TipoEquipo)
        B.ShowDialog()

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class