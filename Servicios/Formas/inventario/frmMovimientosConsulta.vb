Public Class frmMovimientosConsulta
    Public IdMovimiento As Integer
    Dim ConsultaOn As Boolean = True
    Dim idsSucursales As New elemento
    Dim IdsMovimientos As New elemento
    Dim Modo As Integer
    Public Sub New(ByVal pModo As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Modo = pModo
    End Sub

    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim Estado As Byte
                Select Case cmbEstado.SelectedIndex
                    Case 0
                        Estado = 0
                    Case 1
                        Estado = Estados.Pendiente
                    Case 2
                        Estado = Estados.Guardada
                    Case 3
                        Estado = Estados.Cancelada
                End Select
                Dim S As New dbMovimientos(MySqlcon)
                DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), Estado, txtFolio.Text, IdsMovimientos.Valor(ComboBox6.SelectedIndex), idsSucursales.Valor(ComboBox3.SelectedIndex))
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Serie"
                DGServicios.Columns(3).HeaderText = "Folio"
                DGServicios.Columns(4).HeaderText = "Concepto"
                DGServicios.Columns(5).HeaderText = "Estado"
                DGServicios.Columns(2).Width = 100
                DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
                IdMovimiento = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        AbreDetalles()
    End Sub
    Private Sub AbreDetalles()
        If IdMovimiento <> 0 Then
            If Modo = ModosDeBusqueda.Principal Then
                Dim F As New frmInventarioMovimientosN(DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value)
                F.ShowDialog()
            Else
                IdMovimiento = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha1.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha2.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        IdMovimiento = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        IdMovimiento = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
        AbreDetalles()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolio.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub frmInventarioKardex_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        ConsultaOn = False
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", idsSucursales, , "Todas")
        cmbEstado.Items.Add("Todos")
        cmbEstado.Items.Add("Pendiente")
        cmbEstado.Items.Add("Guardado")
        cmbEstado.Items.Add("Cancelado")
        ComboBox3.SelectedIndex = idsSucursales.Busca(GlobalIdSucursalDefault)
        cmbEstado.SelectedIndex = 0
        dtpFecha1.Value = CDate(Format(Date.Now, "yyyy") + "/01/01")
        dtpFecha2.Value = Date.Now
        ConsultaOn = True
        Consulta()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        LlenaCombos("tblinventarioconceptos", ComboBox6, "nombre", "nombret", "idconcepto", IdsMovimientos, "idsucursal=" + idsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Todos")
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Consulta()
    End Sub
End Class