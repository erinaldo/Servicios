Public Class frmEmpeniosNover
    Dim Tabla As New DataTable
    Dim IdsSucursales As New elemento
    Dim ConsultaOn As Boolean = False
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Dim EC As dbEmpeniosConfiguracion
    Private Sub frmEmpeniosNover_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        EC = New dbEmpeniosConfiguracion(MySqlcon)
        EC.LlenaDatos()
        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        Tabla.Columns.Add("Id", I.GetType)
        Tabla.Columns.Add("Empeño", S.GetType)
        Tabla.Columns.Add("Folio", I.GetType)
        Tabla.Columns.Add("Fecha", S.GetType)
        Tabla.Columns.Add("Importe", D.GetType)
        Tabla.Columns.Add("ver", I.GetType)

        ComboBox1.Items.Add("Sin criterio")
        ComboBox1.Items.Add("Menor a mayor")
        ComboBox1.Items.Add("Mayor a menor")
        TextBox1.Text = EC.Rango2.ToString
        TextBox2.Text = EC.Maximo.ToString
        TextBox5.Text = EC.Rango1.ToString
        DateTimePicker1.Value = Date.Now
        DateTimePicker2.Value = Date.Now
        ComboBox1.SelectedIndex = EC.Criterio
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        ConsultaOn = True
        Consulta()
    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim MsgError As String = ""
                Dim Rango1 As Double = 0
                Dim Rango2 As Double = 0
                Dim Maximo As Double = 0

                If (IsNumeric(TextBox5.Text) = False And TextBox5.Text <> "") Or (IsNumeric(TextBox1.Text) = False And TextBox1.Text <> "") Then
                    MsgError = "El rango debe ser un valor numérico." + vbCrLf
                End If
                If IsNumeric(TextBox2.Text) = False And TextBox2.Text <> "" Then
                    MsgError += " El total a acumular debe ser un valor numérico."
                End If
                Dim Suma As Double = 0
                If MsgError = "" Then
                    If TextBox5.Text = "" Then
                        Rango1 = 0
                    Else
                        Rango1 = CDbl(TextBox5.Text)
                    End If
                    If TextBox1.Text = "" Then
                        Rango2 = 0
                    Else
                        Rango2 = CDbl(TextBox1.Text)
                    End If
                    If TextBox2.Text = "" Then
                        Maximo = 0
                    Else
                        Maximo = CDbl(TextBox2.Text)
                    End If
                    Tabla.Rows.Clear()

                    Dim T As MySql.Data.MySqlClient.MySqlDataReader
                    Dim EP As New dbEmpeniosPagos(MySqlcon)
                    T = EP.ConsultaPagosExtra(DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), Rango1, Rango2, ComboBox1.SelectedIndex, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                    While T.Read
                        If Suma <= Maximo Then
                            Tabla.Rows.Add(T("idabono"), T("folioe"), T("folio"), T("fecha"), T("cantidad"), T("vis"))
                            Suma += T("cantidad")
                        Else
                            Exit While
                        End If
                    End While
                    T.Close()
                    DGDetalles.DataSource = Tabla
                    DGDetalles.Columns(0).Visible = False
                    DGDetalles.Columns(5).Visible = False
                    DGDetalles.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                    DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                    DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                End If
                Label14.Text = suma.ToString("#,###,##0.00")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Consulta()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker2.Value = DateTimePicker1.Value
        Consulta()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        'Consulta()
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        Consulta()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Consulta()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Consulta()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Consulta()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Try
            If IsNumeric(TextBox5.Text) Then EC.Rango1 = CDbl(TextBox5.Text)
            If IsNumeric(TextBox1.Text) Then EC.Rango2 = CDbl(TextBox1.Text)
            If IsNumeric(TextBox2.Text) Then EC.Maximo = CDbl(TextBox2.Text)
            EC.Criterio = ComboBox1.SelectedIndex
            EC.ModificaExtras()
        Catch ex As Exception

        End Try
        Dim ep As New dbEmpeniosPagos(MySqlcon)
        Try
            If ep.DiaProcesado(DateTimePicker1.Value.ToString("yyyy/MM/dd"), IdsSucursales.Valor(ComboBox3.SelectedIndex)) Then
                MsgBox("Este dia ya fue procesado.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If DGDetalles.RowCount > 0 Then
                If MsgBox("¿Procesar los pagos?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    ep.Comm.Transaction = ep.Comm.Connection.BeginTransaction
                    ep.DeshacerVis(DateTimePicker1.Value.ToString("yyyy/MM/dd"), IdsSucursales.Valor(ComboBox3.SelectedIndex), 0)
                    Dim r As DataGridViewRow
                    For Each r In DGDetalles.Rows
                        ep.OcultaPago(r.Cells(0).Value, 1)
                    Next
                    ep.Comm.Transaction.Commit()
                    PopUp("Proceso Terminado.", 90)
                    Consulta()
                End If
            End If
        Catch ex As Exception
            ep.Comm.Transaction.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("¿Volver a mostrar los registros del día seleccionado?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim ep As New dbEmpeniosPagos(MySqlcon)
            ep.DeshacerVis(DateTimePicker1.Value.ToString("yyyy/MM/dd"), IdsSucursales.Valor(ComboBox3.SelectedIndex), 0)
            PopUp("Proceso terminado", 90)
            Consulta()
        End If
        
    End Sub

    Private Sub DGDetalles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub DGDetalles_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGDetalles.CellFormatting
        If DGDetalles.Item(5, e.RowIndex).Value = 1 Then
            e.CellStyle.BackColor = ColorVerde
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

End Class