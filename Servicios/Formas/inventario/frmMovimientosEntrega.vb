Public Class frmMovimientosEntrega
    Private idmovimiento As Integer
    Public Sub New(idmovimiento As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.idmovimiento = idmovimiento
    End Sub
    Private Sub frmMovimientosEntrega_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Dim bd As New dbMovimientos(MySqlcon)
        Dim entrega As Entrega = bd.ConsultarEntrega(idmovimiento)
        txtUnidad.Text = entrega.Unidad
        txtPlacas.Text = entrega.Placas
        txtModelo.Text = entrega.Modelo
        txtMarca.Text = entrega.Marca
        txtColor.Text = entrega.Color
        txtChofer.Text = entrega.Chofer
        dtpSalida.Value = entrega.Salida
        txtLugar.Text = entrega.Lugar
        nudPaquetes.Value = entrega.Paquetes
        txtLote.Text = entrega.Lote
        txtNumeroSellos.Text = entrega.NumeroSellos
        nudKilos.Value = entrega.Kilos
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim bd As New dbMovimientos(idmovimiento, MySqlcon)
        bd.GuardarEntrega(New Entrega(idmovimiento, txtUnidad.Text, txtMarca.Text, txtModelo.Text, txtColor.Text, txtPlacas.Text, txtChofer.Text, dtpSalida.Value, txtLugar.Text, nudPaquetes.Value, txtLote.Text, txtNumeroSellos.Text, nudKilos.Value))
        Close()
    End Sub
End Class