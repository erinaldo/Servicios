Public Class frmRestauranteLlevar
    Private idVenta As Integer
    Private configuracion As dbRestauranteConfiguracion
    Public cliente As New dbClientes(MySqlcon)

    Public Sub New(ByVal idVenta As Integer)
        Me.idVenta = idVenta
        InitializeComponent()
    End Sub
    Private Sub frmRestauranteLlevar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        configuracion = New dbRestauranteConfiguracion(1, MySqlcon)
        Me.BackColor = Color.FromArgb(configuracion.colorVentanas)
    End Sub

    Private Sub llenaTabla()

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim f As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, True, False, False)
        f.ShowDialog()
        If Not f.Cliente Is Nothing Then
            cliente = f.Cliente
            txtTelefono.Text = cliente.Telefono
            txtCliente.Text = cliente.Nombre
            txtNumero.Text = cliente.NoExterior
            txtCalle.Text = cliente.Direccion
            txtColonia.Text = cliente.Colonia
            txtReferencia.Text = cliente.ReferenciaDomicilio
        End If
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Dispose()
    End Sub
End Class