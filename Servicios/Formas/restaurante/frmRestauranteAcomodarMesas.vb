Public Class frmRestauranteAcomodarMesas
    Private idSucursal As Integer
    Private mesa As RestauranteMesa
    Public Sub New(pIdSucursal As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        idSucursal = pIdSucursal
        
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim seccion As Integer
        For Each r As RestauranteSeccion In pnlSecciones.Controls
            If r.Checked Then seccion = r.Id
        Next
        Dim mesas As New dbRestauranteMesas(MySqlcon, idSucursal)
        mesa = New RestauranteMesa(0, mesas.CuentaMesas(seccion), seccion, 1, 4, comboSucursal.SelectedValue, 0, 0)
        AddHandler mesa.MouseDown, AddressOf Mesa_MouseDown
        pnlMesasFijo.Controls.Add(mesa)
        nudLeft.Value = mesa.Left
        nudTop.Value = mesa.Top
        nudHeight.Value = mesa.Height
        nudWidth.Value = mesa.Width
        nudNumero.Value = mesa.Numero
        nudCapacidad.Value = mesa.Capacidad
    End Sub

    Private Sub rdbSeccion_CheckedChanged(sender As Object, e As EventArgs)
        If sender.Checked Then
            pnlMesasFijo.Controls.Clear()
            Dim mesasController As New dbRestauranteMesas(MySqlcon, idSucursal)
            For Each m As RestauranteMesa In mesasController.Mesas(sender.Id, -1)
                AddHandler m.MouseDown, AddressOf Mesa_MouseDown
                'AddHandler m.MouseMove, AddressOf Mesa_MouseMove
                pnlMesasFijo.Controls.Add(m)
            Next
        End If
    End Sub

    Private Sub frmRestauranteAcomodarMesas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sucursales As New dbSucursales(MySqlcon)
        comboSucursal.DataSource = sucursales.Consulta()
        'Dim configuracion As New dbRestauranteConfiguracion(MySqlcon)
        'Me.BackColor = Color.FromArgb(configuracion.colorVentanas)
        Dim secciones As New dbRestauranteSecciones(MySqlcon)
        For Each r As RadioButton In secciones.ListaSecciones(idSucursal)
            AddHandler r.CheckedChanged, AddressOf rdbSeccion_CheckedChanged
            pnlSecciones.Controls.Add(r)
        Next
        If pnlSecciones.Controls.Count > 0 Then
            DirectCast(pnlSecciones.Controls(0), RadioButton).Checked = True
        End If
    End Sub


    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim mesasController As New dbRestauranteMesas(MySqlcon, idSucursal)
        For Each m As RestauranteMesa In pnlMesasFijo.Controls
            If m.Id = 0 Then
                mesasController.agregar(m)
            Else
                mesasController.modificar(m)
            End If
        Next
        pnlMesasFijo.Controls.Clear()
        For Each r As RestauranteSeccion In pnlSecciones.Controls
            If r.Checked Then
                For Each m As RestauranteMesa In mesasController.Mesas(r.Id, -1)
                    AddHandler m.MouseDown, AddressOf Mesa_MouseDown
                    'AddHandler m.MouseMove, AddressOf Mesa_MouseMove
                    pnlMesasFijo.Controls.Add(m)
                Next
            End If
        Next

    End Sub

    Private Sub Mesa_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlMesasFijo.MouseMove
        If e.Button = MouseButtons.Left And mesa IsNot Nothing Then
            mesa.Left = If(e.X - (mesa.Width / 2) < 0, 0, e.X - (mesa.Width / 2))
            mesa.Top = If(e.Y - (mesa.Height / 2) < 0, 0, e.Y - (mesa.Height / 2))
            pnlMesasFijo.Refresh()
        End If
    End Sub


    Private Sub Mesa_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            mesa = sender
            nudLeft.Value = mesa.Left
            nudTop.Value = mesa.Top
            nudHeight.Value = mesa.Height
            nudWidth.Value = mesa.Width
            nudNumero.Value = mesa.Numero
            nudCapacidad.Value = mesa.Capacidad
        End If
    End Sub

    Private Sub nudLeft_ValueChanged(sender As Object, e As EventArgs) Handles nudLeft.ValueChanged
        If mesa IsNot Nothing Then
            mesa.Left = nudLeft.Value
            pnlMesasFijo.Refresh()
        End If
    End Sub

    Private Sub nudTop_ValueChanged(sender As Object, e As EventArgs) Handles nudTop.ValueChanged
        If mesa IsNot Nothing Then
            mesa.Top = nudTop.Value
            pnlMesasFijo.Refresh()
        End If
    End Sub

    Private Sub nudHeight_ValueChanged(sender As Object, e As EventArgs) Handles nudHeight.ValueChanged
        If mesa IsNot Nothing Then
            mesa.Height = nudHeight.Value
            pnlMesasFijo.Refresh()
        End If
    End Sub

    Private Sub nudWidth_ValueChanged(sender As Object, e As EventArgs) Handles nudWidth.ValueChanged
        If mesa IsNot Nothing Then
            mesa.Width = nudWidth.Value
            pnlMesasFijo.Refresh()
        End If
    End Sub

    Private Sub nudCapacidad_ValueChanged(sender As Object, e As EventArgs) Handles nudCapacidad.ValueChanged
        If mesa IsNot Nothing Then
            mesa.Capacidad = nudCapacidad.Value
            pnlMesasFijo.Refresh()
        End If
    End Sub

    Private Sub nudNumero_ValueChanged(sender As Object, e As EventArgs) Handles nudNumero.ValueChanged
        If mesa IsNot Nothing Then
            mesa.Numero = nudNumero.Value
            pnlMesasFijo.Refresh()
        End If
    End Sub
End Class