Public Class frmRestauranteReservaciones
    Private IdMesa As Integer
    Public Sub New(idMesa As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.IdMesa = idMesa
    End Sub


    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim reservaciones As New dbRestauranteReservacion(MySqlcon)
        reservaciones.Agregar(IdMesa, dtpFecha.Value, txtNombre.Text)
       
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
       
    End Sub

    Private Sub frmRestauranteReservaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class