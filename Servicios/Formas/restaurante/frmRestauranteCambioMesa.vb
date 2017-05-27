Public Class frmRestauranteCambioMesa
    Private configuracion As New dbRestauranteConfiguracion(1, MySqlcon)
    Private secciones As New dbRestauranteSecciones(MySqlcon)
    Private mesas As dbRestauranteMesas
    Private tablaSecciones As New DataTable
    Private listaMesas As List(Of RestauranteMesa)
    Private mesaVenta As New dbRestauranteVentasMesas(MySqlcon)
    Private mesaB As RestauranteMesa
    Public idMesa As Integer
    Public idMesaNueva As Integer = 0
    Public MesaSeleccionada As Integer = -1
    Public listaMesasOcupadas As List(Of Integer)
    Dim IdSucursal As Integer
    Public Sub New(ByVal idMesa As Integer, ByVal listaMesasOcupadas As List(Of Integer), pIdSucursal As Integer)
        InitializeComponent()
        Me.idMesa = idMesa
        Me.listaMesasOcupadas = listaMesasOcupadas
        IdSucursal = pIdSucursal
        mesas = New dbRestauranteMesas(MySqlcon, IdSucursal)
    End Sub
    Private Sub frmRestauranteCambioMesa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
            Me.BackColor = Color.FromArgb(configuracion.colorVentanas)
        Catch ex As Exception

        End Try
        If idMesa = 0 Then
            btnCambiar.Text = "Seleccionar"
        End If
        muestraSecciones()
        muestraMesas(CInt(panelSecciones.Controls(0).Name))
    End Sub

    Private Sub muestraSecciones()
        Dim x = 0
        Dim y = 0
        tablaSecciones = secciones.vistaSecciones(IdSucursal).ToTable
        For Each r As DataRow In tablaSecciones.Rows
            If x >= panelSecciones.Width Then
                x = 0
                y += 60
            End If
            Dim b As New Button()
            b.Name = r("idseccion")
            b.Text = r("nombre")
            b.Width = panelSecciones.Width / 4
            b.Height = 60
            b.BackColor = Color.Gray
            b.Location = New Point(x, y)
            AddHandler b.Click, AddressOf clickSecciones
            panelSecciones.Controls.Add(b)
            x += panelSecciones.Width / 4
        Next
    End Sub

    Private Sub clickSecciones(sender As Object, e As EventArgs)
        Dim b As Button = DirectCast(sender, Button)
        Dim idSeccion As Integer = CInt(b.Name)
        muestraMesas(idSeccion)
    End Sub

    Private Sub muestraMesas(ByVal idSeccion As Integer)
        Dim x = 0
        Dim y = 0
        If idMesa = 0 Then
            listaMesas = mesas.listaMesasEstado(idSeccion, CInt(EstadosMesas.Ocupada))
        End If
        For Each m As RestauranteMesa In listaMesas
            If listaMesasOcupadas.IndexOf(m.id) < 0 Then
                If x >= panelMesas.Width Then
                    x = 0
                    y += 60
                End If
                Dim b As New Button
                b.Name = m.id.ToString
                b.Text = m.numero.ToString
                If idMesa = 0 Then
                    b.BackColor = Color.FromArgb(configuracion.colorOcupado)
                Else
                    b.BackColor = Color.FromArgb(configuracion.colorLibre)
                End If
                AddHandler b.Click, AddressOf clickMesa
                b.Width = panelMesas.Width / 4
                b.Height = 60
                b.Location = New Point(x, y)
                panelMesas.Controls.Add(b)
                x += panelMesas.Width / 4
            End If
        Next
    End Sub

    Private Sub clickMesa(sender As Object, e As EventArgs)
        Dim b As Button = DirectCast(sender, Button)
        idMesaNueva = CInt(b.Name)
        b.Text = "Seleccionada"
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Dispose()
    End Sub

    Private Sub btnCambiar_Click(sender As Object, e As EventArgs) Handles btnCambiar.Click
        If idMesa > 0 Then
            If mesaVenta.cambiarMesa(Me.idMesa, Me.idMesaNueva) Then
                Dispose()
            Else
                MsgBox("No se ha podido realizar el cambio.")
                Exit Sub
            End If
        Else
            Dispose()
        End If

    End Sub
End Class