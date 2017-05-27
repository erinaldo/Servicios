Public Class frmRestauranteMonitor
    Private config As New dbRestauranteConfiguracion(1, MySqlcon)
    Private Sub frmRestauranteMonitor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        GlobalEstadoRestaurante = "Cerrado"
    End Sub

    Private Sub frmRestauranteMonitor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.BackColor = Color.FromArgb(config.colorVentanas)
            InitializeTimer()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Actualiza()
    End Sub

    Private Sub Actualiza()

    End Sub

    Private Sub InitializeTimer()
        ' Run this procedure in an appropriate event.
        ' Set to 1 second.
        Timer1.Interval = 10000
        ' Enable timer.
        Timer1.Enabled = True

    End Sub
End Class