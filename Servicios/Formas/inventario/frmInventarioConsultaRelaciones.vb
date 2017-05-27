Public Class frmInventarioConsultaRelaciones
    Dim IdInventario As Integer
    Public Clave As String
    Public Sub New(ByVal pIdInventario As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdInventario = pIdInventario
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmInventarioConsultaRelaciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Dim IR As New dbInventarioRelaciones(MySqlcon)
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex

            'Dim I As New dbInventario(MySqlcon)
            DataGridView1.DataSource = IR.ConsultaRelaciones(IdInventario)
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "Código"
            DataGridView1.Columns(2).HeaderText = "Descripción"
            DataGridView1.Columns(3).HeaderText = "Exist."
            DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView1.Columns(1).Width = 180
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            IR.IdDetalle = 0
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    


    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If DataGridView1.RowCount = 1 Then
            Clave = DataGridView1.Item(1, 0).Value
        Else
            Clave = DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex).Value
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.RowCount = 1 Then
            Clave = DataGridView1.Item(1, 0).Value
        Else
            Clave = DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex).Value
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class