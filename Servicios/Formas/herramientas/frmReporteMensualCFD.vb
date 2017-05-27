Public Class frmReporteMensualCFD

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'Dim o As New dbOpciones(MySqlcon)
            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim v As New dbVentas(MySqlcon)
            v.ReporteMensualCFD(DateTimePicker1.Value, FolderBrowserDialog1.SelectedPath + "\1" + S.RFC + Format(Month(DateTimePicker1.Value), "00") + Format(DateTimePicker1.Value, "yyyy") + ".txt", 0)
            MsgBox("Listo", MsgBoxStyle.OkOnly, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub frmReporteMensualCFD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
    End Sub
End Class