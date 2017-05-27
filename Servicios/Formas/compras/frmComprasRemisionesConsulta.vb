Public Class frmComprasRemisionesConsulta
    Public IdRemision As Integer
    Dim ConsultaOn As Boolean = True
    Dim Modo As Integer
    Dim IdsSucursales As New elemento
    Public Sub New(ByVal pModo As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Modo = pModo

    End Sub
    Private Sub frmComprasRemisionesConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        ComboBox2.Items.Add("Todas")
        ComboBox2.Items.Add("Guardadas")
        ComboBox2.Items.Add("Pendientes")
        ComboBox2.Items.Add("Canceladas")
        ComboBox2.SelectedIndex = 0
        DateTimePicker2.Value = CDate(Format(Date.Now, "yyyy") + "/01/01")
        DateTimePicker3.Value = Date.Now
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CambioSucursal, PermisosN.Secciones.Compras) = False Then
            ComboBox3.Enabled = False
            ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        End If
        ConsultaOn = True
        If chkTiempoReal.Checked Then Consulta()
    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim Estado As Byte
                If ComboBox2.SelectedIndex = 0 Then Estado = Estados.Inicio
                If ComboBox2.SelectedIndex = 1 Then Estado = Estados.Guardada
                If ComboBox2.SelectedIndex = 2 Then Estado = Estados.Pendiente
                If ComboBox2.SelectedIndex = 3 Then Estado = Estados.Cancelada
                Dim S As New dbComprasRemisiones(MySqlcon)
                DGServicios.DataSource = S.Consulta(Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), TextBox1.Text, Estado, TextBox2.Text, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Folio - Referencia"
                DGServicios.Columns(3).HeaderText = "C. Proveedor"
                DGServicios.Columns(4).HeaderText = "Proveedor"
                DGServicios.Columns(2).Width = 200
                DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
                IdRemision = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        AbreDetalles()
    End Sub
    Private Sub AbreDetalles()
        If IdRemision <> 0 Then
            If Modo = ModosDeBusqueda.Principal Then
                Dim F As New frmComprasRemisiones(DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value)
                F.ShowDialog()
            Else
                IdRemision = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker3.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        IdRemision = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        IdRemision = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
        AbreDetalles()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Consulta()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub
End Class