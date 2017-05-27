Public Class frmTimbres
    Private idLicencia As Integer
    Dim Lic As Licencia
    Public idTimbre As Integer
    Public Sub New(ByVal pidLicencia As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        idLicencia = pidLicencia
        ' Add any initialization after the InitializeComponent() call.
        idTimbre = -1
    End Sub
    Private Sub frmTimbres_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Lic = New Licencia("servidor", True)
        DataGridView1.DataSource = Lic.ConsultaTimbres(idLicencia)
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).Visible = False
        DataGridView1.Columns(2).HeaderText = "Cantidad"
        DataGridView1.Columns(3).HeaderText = "Timbres"
        DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

    End Sub

    Private Sub frmTimbres_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Lic.CierraConexion()
    End Sub
    Private Sub AbreDetalles()
        If idTimbre <> -1 Then
            
            idTimbre = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

        Else
            MsgBox("Debe seleccionar un timbre.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        idTimbre = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
        AbreDetalles()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        idTimbre = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
    End Sub
End Class