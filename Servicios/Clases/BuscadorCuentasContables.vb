Imports System.Text.RegularExpressions
Public Class BuscadorCuentasContables
    Dim TipoB As Integer
    Public ID As Integer
    'Public contador As Integer
    Public Cuenta As String
    Public descripcion As String
    Public Nivel As String
    Public Tipo As String
    Public P As dbContabilidadClasificacion
    Dim negrita As New Font("Arial", 9, FontStyle.Bold)
    Public filtroCuenta As String
    Public Sub Consulta()

        Try
            Dim PrimerCeldaRow As Integer = -1
            'Dim cuenta As String = txtBuscar.Text
            Dim Palabras() As String
            Dim txtAbuscar As String = ""
            Dim txtAbuscar2 As String = ""
            Palabras = filtroCuenta.Split(Chr(32))
            For Each palabra As String In Palabras
                If Regex.IsMatch(palabra, "[A-Z]|[a-z]") Then
                    txtAbuscar += " " + palabra
                Else
                    txtAbuscar2 += " " + palabra
                End If
            Next
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            DataGridView1.DataSource = P.Consulta(txtAbuscar2.Trim, 0, txtAbuscar.Trim, True, False)
            DataGridView1.Columns(0).HeaderText = "id"
            DataGridView1.Columns(1).HeaderText = "Cuenta"
            DataGridView1.Columns(2).HeaderText = "Descripción"
            DataGridView1.Columns(3).HeaderText = "Nivel"
            DataGridView1.Columns(4).HeaderText = "Tipo"
            DataGridView1.Columns(5).HeaderText = "Naturaleza"
            DataGridView1.Columns(6).HeaderText = "SAT"
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(6).Visible = False
            DataGridView1.Columns(1).Width = 200
            DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        '    End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            ID = DataGridView1.Item(0, e.RowIndex).Value
            Me.OnClick(e)
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If DataGridView1.Item(3, e.RowIndex).Value.ToString = "1" Then
            e.CellStyle.Font = negrita
        End If
    End Sub

    Public Sub PresionaArriba()
        If DataGridView1.RowCount > 1 Then
            If DataGridView1.CurrentRow.Index > 0 Then DataGridView1.CurrentCell = DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex - 1)
        End If
    End Sub
    Public Sub PresionaAbajo()
        If DataGridView1.RowCount > 1 Then
            If DataGridView1.CurrentRow.Index < DataGridView1.RowCount - 1 Then DataGridView1.CurrentCell = DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex + 1)
        End If
    End Sub
    Public Function DaId()
        ID = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
        Return ID
    End Function
    Public Sub CambiaAncho(pValor As Integer)
        DataGridView1.Width = pValor
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
