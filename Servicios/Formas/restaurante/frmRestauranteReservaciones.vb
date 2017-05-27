Public Class frmRestauranteReservaciones
    Private idSeccion As Integer
    Private listaSecciones As New elemento
    Private reservaciones As New dbRestauranteReservacion(MySqlcon)
    Private cliente As New dbClientes(MySqlcon)
    Dim listaMesas As List(Of RestauranteMesa)
    Dim mesasController As New dbRestauranteMesas(MySqlcon, GlobalIdSucursalDefault)
    Private configuracion As New dbRestauranteConfiguracion(1, MySqlcon)
    Private botones As List(Of Button)
    Private teclado As escuchaTeclado
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        LlenaCombos("tblrestaurantesecciones", comboSeccion, "nombre", "nombret", "idseccion", listaSecciones)
        ' Add any initialization after the InitializeComponent() call.
        teclado = New escuchaTeclado()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboSeccion.SelectedIndexChanged
        Me.BackColor = Color.FromArgb(configuracion.colorVentanas)
        idSeccion = listaSecciones.Valor(comboSeccion.SelectedIndex)
        muestraReservaciones()
    End Sub

    Private Sub muestraReservaciones()
        listaMesas = mesasController.listaMesas(idSeccion)
        botones = New List(Of Button)
        panelMesas.Controls.Clear()
        For Each m As RestauranteMesa In listaMesas
            Dim b As New Button()
            b.Width = m.Width
            b.Height = m.Height
            b.Location = New Point(m.X, m.Y)
            b.BackColor = m.BackColor
            b.Text = m.Text
            b.Name = m.id.ToString()
            AddHandler b.Click, AddressOf checaMesa
            panelMesas.Controls.Add(b)
        Next
        panelMesas.Refresh()
    End Sub

    Private Sub frmRestauranteReservaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim f As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 1, True, False, False)
        f.ShowDialog()
        If Not f.Cliente Is Nothing Then
            cliente = f.Cliente
            txtClave.Text = cliente.Clave
            txtNombre.Text = cliente.Nombre
        End If
    End Sub

    Private Sub txtClave_TextChanged(sender As Object, e As EventArgs) Handles txtClave.TextChanged
        If cliente.BuscaCliente(txtClave.Text) Then
            txtClave.Text = cliente.Clave
            txtNombre.Text = cliente.Nombre
        End If
    End Sub

    Private Sub dtpFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha.ValueChanged

    End Sub

    Private Sub checaMesa(ByVal sender As Object, e As EventArgs)
        listaMesas = mesasController.listaMesas(idSeccion)
        Dim i As Integer
        i = CType(sender, System.Windows.Forms.Button).Name
        For Each m As RestauranteMesa In listaMesas
            If i = m.id Then
                If m.estado = EstadosMesas.Reservada Then

                    muestraDatos(i)
                End If
            End If
        Next
    End Sub

    Private Sub muestraDatos(ByVal idMesa As Integer)

    End Sub

    Private Sub txtClave_Enter(sender As Object, e As EventArgs) Handles txtClave.Enter
        'teclado.cajaTexto = txtClave
        Dim cp As Process = Process.GetCurrentProcess()
        Dim f As New frmRestauranteTeclado(txtClave)
        f.MdiParent = Me
        f.Show()
        'txtClave.Focus()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

    End Sub
    Public Sub checaEstado(ByRef btn As Button, ByVal estado As Integer)
        Select Case estado
            Case EstadosMesas.Libre
                btn.BackColor = Color.Green
                btn.Text = "LIBRE"
            Case EstadosMesas.Ocupada
                btn.BackColor = Color.Red
                btn.Text = "OCUPADA"
            Case EstadosMesas.Reservada
                btn.BackColor = Color.Yellow
                btn.Text = "RESERVADA"
            Case EstadosMesas.Sucia
                btn.BackColor = Color.Blue
                btn.Text = "SUCIA"
        End Select
    End Sub
End Class