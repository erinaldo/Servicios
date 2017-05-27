Public Class frmContabilidadMascarasConsulta
    Public IdMascara As Integer
    Dim ConsultaOn As Boolean = True
    Dim idsSucursales As New elemento
    Dim idsCajas As New elemento
    Dim ModuloSel As Integer
    Dim Modo As Integer
    Public Sub New(ByVal pModo As Integer, pDeshabilidadTipo As Boolean, pModulo As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Modo = pModo
        ModuloSel = pModulo
        ComboBox4.Enabled = pDeshabilidadTipo
    End Sub
    Private Sub frmComprasConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        ComboBox6.Items.Add("Todos")
        ComboBox6.Items.Add("Ventas")
        ComboBox6.Items.Add("Compras")
        ComboBox6.Items.Add("Ventas Devoluciones")
        ComboBox6.Items.Add("Compras Devoluciones")
        ComboBox6.Items.Add("Depósitos")
        ComboBox6.Items.Add("Pagos")
        ComboBox6.Items.Add("Notas de crédito ventas") '6
        ComboBox6.Items.Add("Notas de crédito compras") '7
        ComboBox6.Items.Add("Notas de cargo ventas") '8
        ComboBox6.Items.Add("Notas de cargo compras") '9
        ComboBox6.Items.Add("Nómina") '10
        ComboBox6.Items.Add("Movimientos de inventario") '11
        ComboBox6.Items.Add("Documentos Proveedores") '12

        ComboBox4.Items.Add("Todos")
        ComboBox4.Items.Add("Mensual")
        ComboBox4.Items.Add("Semanal")
        ComboBox4.Items.Add("Diario")
        ComboBox4.Items.Add("Por Movimiento")
        ComboBox4.Items.Add("Por Rango")

        ComboBox2.Items.Add("Todos")
        ComboBox2.Items.Add("Desactivos")
        ComboBox2.Items.Add("Activos")
        ComboBox6.SelectedIndex = 0
        ComboBox4.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0
        If ComboBox4.Enabled = False Then
            ComboBox2.Enabled = False
            ComboBox2.SelectedIndex = 2
            ComboBox4.SelectedIndex = 4
            ComboBox6.Enabled = False
            ComboBox6.SelectedIndex = ModuloSel + 1
        End If
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        ConsultaOn = True
        If chkTiempoReal.Checked Then Consulta()
    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                
                Dim S As New dbContabilidadMascaras(MySqlcon)
                DGServicios.DataSource = S.Consulta(TextBox2.Text, ComboBox6.SelectedIndex, ComboBox4.SelectedIndex, ComboBox2.SelectedIndex)
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Titulo"
                DGServicios.Columns(2).HeaderText = "Modulo"
                DGServicios.Columns(3).HeaderText = "Tipo"
                DGServicios.Columns(4).HeaderText = "Estado"
                'DGServicios.Columns(5).HeaderText = "Caja"
                'DGServicios.Columns(6).HeaderText = "Estado"
                DGServicios.Columns(2).Width = 130
                DGServicios.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
                IdMascara = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        AbreDetalles()
    End Sub
    Private Sub AbreDetalles()
        If IdMascara <> 0 Then
            If Modo = ModosDeBusqueda.Principal Then
                Dim F As New frmContabilidadMascaras()
                F.ShowDialog()
            Else
                IdMascara = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        IdMascara = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        IdMascara = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
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

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub
End Class