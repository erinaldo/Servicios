Public Class frmSelectorPrecios
    Dim IdInventario As Integer
    Dim IdProducto As Integer
    Public Precio As Double
    Public Sub New(ByVal pIdinventario As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdInventario = pIdinventario
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmSelectorPrecios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Consulta()
    End Sub
    Private Sub Consulta()
        Try
            Dim I As New dbInventarioPrecios(MySqlcon)
            DataGridView2.DataSource = I.Consulta(IdInventario)
            DataGridView2.Columns(0).Visible = False
            DataGridView2.Columns(1).HeaderText = "Lista precio"
            DataGridView2.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView2.Columns(2).HeaderText = "Precio"
            DataGridView2.Columns(2).DefaultCellStyle.Format = "C2"
            DataGridView2.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DataGridView2.Columns(2).Width = 120
            DataGridView2.Columns(3).Visible = False
            'DataGridView2.Columns(4).Visible = False
            'DataGridView2.Columns(3).Width = 80
            'DataGridView2.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DataGridView2.Columns(5).HeaderText = "Comentario"
            DataGridView2.Columns(4).HeaderText = "Desc."
            DataGridView2.Columns(4).Width = 55
            DataGridView2.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Aceptar()
        Precio = DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Aceptar()
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub DataGridView2_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        Aceptar()
    End Sub

    Private Sub DataGridView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Aceptar()
        End If
    End Sub
End Class