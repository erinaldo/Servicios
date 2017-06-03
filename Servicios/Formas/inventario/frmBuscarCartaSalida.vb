Public Class frmBuscarCartaSalida
    Public ReadOnly Property IdCarta As Integer
        Get
            If dgvResultados.SelectedRows.Count = 0 Then Return 0
            Return dgvResultados.SelectedRows(0).Cells(colId.Index).Value
        End Get
    End Property

    Private Sub frmBuscarCartaSalida_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvResultados.AutoGenerateColumns = False
        dtpDesde.Value = Now.Date.AddDays(1 - Now.Day)
        consultar()
    End Sub

    Private Sub consultar()
        Dim db As New dbCartasSalida(MySqlcon)
        dgvResultados.DataSource = db.Consultar(dtpDesde.Value.Date, dtpHasta.Value.Date)
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If dgvResultados.SelectedRows.Count > 0 Then
            DialogResult = Windows.Forms.DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub dtpDesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpDesde.ValueChanged, dtpHasta.ValueChanged
        consultar()
    End Sub

    Private Sub dgvResultados_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResultados.CellDoubleClick
        DialogResult = Windows.Forms.DialogResult.OK
        Close()
    End Sub
End Class