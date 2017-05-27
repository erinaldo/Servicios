Public Class frmGastosAviso
    Dim datos As New dbGastosProgramables(MySqlcon)
    Dim MyDate As Date = Now
    Dim DaysInMonth As Integer = Date.DaysInMonth(MyDate.Year, MyDate.Month)

    Private Sub btn_cerrar_Click(sender As Object, e As EventArgs) Handles btn_cerrar.Click
        Me.Close()
    End Sub
    Private Sub frmGastosAviso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception
        End Try
        If GlobalUltimoDia = False Then
            Data_gastos.DataSource = datos.buscarNotificaciones()
        ElseIf GlobalUltimoDia = True Then
            Data_gastos.DataSource = datos.buscarNotificaciones2()
        End If
        Data_gastos.Columns(1).Width = 100
        Data_gastos.Columns(1).HeaderText = "Día"
        Data_gastos.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        For Each rw As DataGridViewRow In Data_gastos.Rows
            If rw.Cells("Fecha").Value.ToString = Today.Day.ToString Then
                rw.Cells("Fecha").Value = "Hoy"
            ElseIf rw.Cells("Fecha").Value.ToString = "00" Then
                rw.Cells("Fecha").Value = DaysInMonth
            End If
        Next
        Data_gastos.ClearSelection()
        Data_gastos.CurrentCell = Nothing
    End Sub
    'Public Function pintaTabla()
    '    For Each rw As DataGridViewRow In Data_gastos.Rows
    '        If rw.Cells("Día").Value.ToString = "Hoy" Then
    '            rw.DefaultCellStyle.BackColor = Color.Yellow
    '        End If
    '    Next
    '    Return 0
    'End Function
    'Public Function pintaTablaNot()
    '    For Each rw As DataGridViewRow In Data_gastos.Rows
    '            rw.DefaultCellStyle.BackColor = Color.White
    '    Next
    '    Return 0
    'End Function
    'Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    '    If CheckBox1.Checked = True Then
    '        pintaTabla()
    '    ElseIf CheckBox1.Checked = False Then
    '        pintaTablaNot()
    '    End If
    'End Sub

    Private Sub Data_gastos_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles Data_gastos.CellFormatting
        If Data_gastos.Item("Fecha", e.RowIndex).Value = "Hoy" Then
            e.CellStyle.BackColor = Color.Yellow
        End If
    End Sub
    Private Sub Data_gastos_SelectionChanged(sender As Object, e As EventArgs) Handles Data_gastos.SelectionChanged
        Data_gastos.ClearSelection()
    End Sub
End Class