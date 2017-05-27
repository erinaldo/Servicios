Public Class frmContabilidadBuscarAgrupador
    Dim conta As New dbContabilidadClasificacion(MySqlcon)
    Public TIPO As String
    Public id As String
    Dim negrita As New Font("Arial", 9, FontStyle.Bold)
    Private Sub frmContabilidadBuscarAgrupador_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        cmbTipo.SelectedIndex = 0
        filtro()
    End Sub
    Private Sub filtro()
        DataGridView1.DataSource = conta.agrupadores(txtBuscador.Text, cmbTipo.Text)
    End Sub

    Private Sub txtBuscador_TextChanged(sender As Object, e As EventArgs) Handles txtBuscador.TextChanged
        filtro()
    End Sub

    Private Sub cmbTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipo.SelectedIndexChanged
        filtro()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        seleccionar()
    End Sub

    Private Sub btnSeleccionar_Click(sender As Object, e As EventArgs) Handles btnSeleccionar.Click
        seleccionar()

    End Sub
    Private Sub seleccionar()

        id = DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex).Value.ToString + " - " + DataGridView1.Item(2, DataGridView1.CurrentCell.RowIndex).Value.ToString
        TIPO = DataGridView1.Item(3, DataGridView1.CurrentCell.RowIndex).Value
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If DataGridView1.Item(5, e.RowIndex).Value.ToString = "1" Then
            e.CellStyle.Font = negrita
        End If
    End Sub
End Class