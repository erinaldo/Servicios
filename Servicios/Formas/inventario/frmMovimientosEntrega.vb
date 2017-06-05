Public Class frmMovimientosEntrega
    Private identrega As Integer
    Private Property Entrega As Entrega
        Get
            Return New Entrega(identrega, txtUnidad.Text, txtMarca.Text, txtModelo.Text, txtColor.Text, txtPlacas.Text, txtChofer.Text, dtpSalida.Value, txtLugar.Text, nudPaquetes.Value, txtLote.Text, txtNumeroSellos.Text, nudKilos.Value, txtTransportista.Text, dtpLlegada.Value)
        End Get
        Set(value As Entrega)
            identrega = value.Id
            txtUnidad.Text = value.Unidad
            txtPlacas.Text = value.Placas
            txtModelo.Text = value.Modelo
            txtMarca.Text = value.Marca
            txtColor.Text = value.Color
            txtChofer.Text = value.Chofer
            dtpSalida.Value = value.Salida
            txtLugar.Text = value.Lugar
            nudPaquetes.Value = value.Paquetes
            txtLote.Text = value.Lote
            txtNumeroSellos.Text = value.NumeroSellos
            nudKilos.Value = value.Kilos
            txtTransportista.Text = value.Transportista
            dtpLlegada.Value = value.Llegada
        End Set
    End Property
    Public Sub New(identrega As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.identrega = identrega
    End Sub
    Private Sub frmMovimientosEntrega_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Dim bd As New dbMovimientos(MySqlcon)
        Entrega = bd.ConsultarEntrega(identrega)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim bd As New dbMovimientos(MySqlcon)
        bd.GuardarEntrega(Entrega)
        Close()
    End Sub
End Class