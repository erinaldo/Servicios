Public Class frmRestauranteReportes
    Private config As New dbRestauranteConfiguracion(1, MySqlcon)
    Private idSucursal As Integer = -1
    Private idMesero As Integer = -1
    Private idCajero As Integer = -1
    Private idInventario As Integer = -1
    Private idSeccion As Integer = -1
    Private Sub frmRestauranteReportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
            Me.BackColor = Color.FromArgb(config.colorVentanas)
            llenaLista()
        Catch ex As Exception

        End Try
        panelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub frmRestauranteReportes_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        panelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub llenaLista()
        listaReportes.Items.Add("Ventas")
        listaReportes.Items.Add("Pedidos")
        listaReportes.Items.Add("Ventas por Mesero")
    End Sub
End Class