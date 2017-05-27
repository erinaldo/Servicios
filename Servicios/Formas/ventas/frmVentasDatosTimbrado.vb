Public Class frmVentasDatosTimbrado
    Public Sub New(ByVal pUUID As String, ByVal pFechaTimbrado As String, ByVal pNoCertificado As String, ByVal pselloCDS As String, ByVal pSelloSAT As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        TextBox5.Text = pUUID
        TextBox1.Text = pFechaTimbrado
        TextBox2.Text = pNoCertificado
        TextBox3.Text = pselloCDS
        TextBox4.Text = pSelloSAT
        ' Add any initialization after the InitializeComponent() call.

    End Sub
  
    Private Sub frmVentasDatosTimbrado_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
    End Sub
End Class