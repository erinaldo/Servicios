Public Class frmPuntodeVentaCajaEstado
    Dim Caja As dbCajas
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Public Sub New(ByVal pidCaja As Integer)
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Caja = New dbCajas(pidCaja, MySqlcon)
        Label11.Text = Caja.Nombre + ": $" + Format(Caja.Deposito, "#,###,##0.00")
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmPuntodeVentaCajaEstado_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
    End Sub
End Class