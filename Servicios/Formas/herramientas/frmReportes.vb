Public Class frmReportes
    Dim Cargando As Boolean
    Public Sub New(ByVal r As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal pCargando As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        CrystalReportViewer1.ReportSource = r
        CrystalReportViewer1.Zoom(125)
        ' Add any initialization after the InitializeComponent() call.
        Cargando = pCargando
        If Cargando Then Me.Opacity = 0
    End Sub
    Private Sub frmReportes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Cargando Then Me.Close()
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
    End Sub
End Class