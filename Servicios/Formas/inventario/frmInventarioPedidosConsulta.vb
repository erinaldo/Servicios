Public Class frmInventarioPedidosConsulta

    Public IdPedido As Integer
    Dim IdsSucursalesA As New elemento
    Dim IdsSucursalesB As New elemento
    Dim ConsultaOn As Boolean = True
    Dim Modo As Integer
    Dim Pedido As dbInventarioPedidos
    Dim EsMonitor As Boolean
    Dim Consultanto As Boolean = False
    Dim IdSucursalSurtidora As Integer
    Dim ColorAmarillo As Color = Color.FromArgb(255, 255, 118)
    Public Sub New(ByVal pModo As Integer, pEsMonitor As Boolean, pIdSucursal As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Modo = pModo
        EsMonitor = pEsMonitor
        IdSucursalSurtidora = pIdSucursal
    End Sub

    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Consultanto = True
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
                    Case 4
                        Estado = 5
                End Select
                DGServicios.DataSource = Pedido.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtFolio.Text, Estado, IdsSucursalesA.Valor(ComboBox1.SelectedIndex), IdsSucursalesB.Valor(ComboBox2.SelectedIndex), ComboBox3.SelectedIndex - 1)
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Folio"
                DGServicios.Columns(3).HeaderText = "Solicitante"
                DGServicios.Columns(4).HeaderText = "Solicitar a"
                DGServicios.Columns(5).HeaderText = "Estado"
                DGServicios.Columns(6).HeaderText = "Tipo"
                DGServicios.Columns(2).Width = 80
                DGServicios.Columns(3).Width = 250
                DGServicios.Columns(5).Width = 120
                DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
                IdPedido = 0
                Consultanto = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        AbreDetalles()
    End Sub
    Private Sub AbreDetalles()
        If IdPedido <> 0 Then
            If Modo = ModosDeBusqueda.Principal Then
                Dim F As New frmInventarioPedidos(DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value)
                F.ShowDialog()
                F.Dispose()
                Consulta()
            Else
                IdPedido = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha1.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha2.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        IdPedido = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        IdPedido = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
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
        Pedido = New dbInventarioPedidos(MySqlcon)
        ConsultaOn = False
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        cmbEstado.Items.Add("Todos")
        cmbEstado.Items.Add("Pendiente")
        cmbEstado.Items.Add("Sin Autorizar")
        cmbEstado.Items.Add("Cancelado")
        cmbEstado.Items.Add("Autorizado")
        If IdSucursalSurtidora = 0 Then
            If EsMonitor Then
                cmbEstado.SelectedIndex = 2
            Else
                cmbEstado.SelectedIndex = 0
            End If
        Else
            cmbEstado.SelectedIndex = 4
            CheckBox1.Enabled = False
        End If
        ComboBox3.Items.Add("Todos")
        ComboBox3.Items.Add("Normal")
        ComboBox3.Items.Add("Urgente")
        ComboBox3.SelectedIndex = 0
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursalesA, , "Todas")
        LlenaCombos("tblsucursales", ComboBox2, "nombre", "nombret", "idsucursal", IdsSucursalesB, , "Todas")
        If IdSucursalSurtidora <> 0 Then
            ComboBox2.Enabled = False
            ComboBox2.SelectedIndex = IdsSucursalesB.Busca(IdSucursalSurtidora)
        Else
            ComboBox1.SelectedIndex = IdsSucursalesA.Busca(GlobalIdSucursalDefault)
        End If
        ComboBox1.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.CambioSucursal, PermisosN.Secciones.Inventario)
        ComboBox2.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.CambioSucursal, PermisosN.Secciones.Inventario)
        dtpFecha1.Value = CDate(Format(Date.Now, "yyyy") + "/01/01")
        dtpFecha2.Value = Date.Now
        CheckBox1.Checked = EsMonitor
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
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    
    Private Sub DGServicios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub DGServicios_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        If DGServicios.Item(6, e.RowIndex).Value = "URGENTE" Then
            e.CellStyle.BackColor = ColorAmarillo
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            Timer1.Enabled = True
        Else
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Consultanto = False Then Consulta()
    End Sub
End Class