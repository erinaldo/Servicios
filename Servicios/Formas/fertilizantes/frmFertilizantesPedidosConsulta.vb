Public Class frmFertilizantesPedidosConsulta
    Public IdVenta As Integer
    Dim ConsultaOn As Boolean = True
    Dim Modo As Integer
    Dim IdsSucursales As New elemento

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
                Dim Credito As Byte
                Select Case ComboBox1.SelectedIndex
                    Case 0
                        Credito = 200
                    Case 1
                        Credito = 1
                    Case 2
                        Credito = 0
                End Select
                Dim S As New dbFertilizantesPedido(MySqlcon)
                DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, Estado, txtFolio.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), ComboBox2.SelectedIndex, False)
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Folio"
                DGServicios.Columns(3).HeaderText = "C. Cliente"
                DGServicios.Columns(4).HeaderText = "Cliente"
                DGServicios.Columns(5).HeaderText = "Importe"
                DGServicios.Columns(6).HeaderText = "Estado"
                DGServicios.Columns(7).HeaderText = "Estado P."
                DGServicios.Columns(2).Width = 80
                DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
                IdVenta = 0
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        AbreDetalles()
    End Sub
    Private Sub AbreDetalles()
        If IdVenta <> 0 Then
            If Modo = ModosDeBusqueda.Principal Then
                Dim F As New frmFertilizantesPedido(DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value)
                F.ShowDialog()
            Else
                IdVenta = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Else
            MsgBox("Selecciona un registro", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCliente.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha1.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha2.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        IdVenta = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        IdVenta = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
        AbreDetalles()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolio.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub frmVentasConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
            ComboBox3.Enabled = False
            ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        End If
        cmbEstado.Items.Add("Todos")
        cmbEstado.Items.Add("Pendiente")
        cmbEstado.Items.Add("Guardado")
        cmbEstado.Items.Add("Cancelado")

        ComboBox1.Items.Add("Todos")
        ComboBox1.Items.Add("Contado")
        ComboBox1.Items.Add("Crédito")
        ComboBox2.Items.Add("Todos")
        ComboBox2.Items.Add("Abiertos")
        ComboBox2.Items.Add("Cerrados")
        ComboBox2.SelectedIndex = 0
        If Modo = ModosDeBusqueda.SoloDeudas Then
            ComboBox1.SelectedIndex = 2
            cmbEstado.SelectedIndex = 2
            ComboBox1.Enabled = False
            cmbEstado.Enabled = False
        Else
            ComboBox1.SelectedIndex = 0
            cmbEstado.SelectedIndex = 0
        End If

        dtpFecha1.Value = CDate(Format(Date.Now, "yyyy") + "/01/01")
        dtpFecha2.Value = Date.Now
        ConsultaOn = True
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEstado.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Consulta()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Consulta()
    End Sub

    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub DGServicios_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        If e.ColumnIndex = 5 Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            e.Value = Format(e.Value, "#0.00")
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub
End Class