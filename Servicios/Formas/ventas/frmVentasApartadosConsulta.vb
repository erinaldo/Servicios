Public Class frmVentasApartadosConsulta
    Public IdApartado As Integer
    Dim ConsultaOn As Boolean = True
    Dim Tipo As Byte
    Dim Modo As Integer

    Public Sub New(ByVal pModo As Integer, ByVal pTipo As Byte)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Modo = pModo
        Tipo = pTipo
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
                
                Dim S As New dbVentasApartados(MySqlcon)
                DGServicios.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, txtFolio.Text, Estado, 0, ComboBox1.SelectedIndex, ComboBox2.SelectedIndex)
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(3).Visible = False
                DGServicios.Columns(1).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Folio"
                DGServicios.Columns(3).HeaderText = "C. Cliente"
                DGServicios.Columns(4).HeaderText = "Cliente"
                DGServicios.Columns(5).HeaderText = "Importe"
                DGServicios.Columns(6).HeaderText = "Prioridad"
                DGServicios.Columns(7).HeaderText = "Mensaje"
                DGServicios.Columns(8).HeaderText = "Salida"
                DGServicios.Columns(9).HeaderText = "Sur."
                DGServicios.Columns(10).HeaderText = "Estado"
                DGServicios.Columns(2).Width = 70
                DGServicios.Columns(1).Width = 80
                DGServicios.Columns(8).Width = 120
                DGServicios.Columns(4).Width = 180
                DGServicios.Columns(6).Width = 60
                DGServicios.Columns(9).Width = 40
                DGServicios.Columns(7).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
                IdApartado = 0
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        AbreDetalles()
    End Sub
    Private Sub AbreDetalles()
        If IdApartado <> 0 Then
            If Modo = ModosDeBusqueda.Principal Then
                Dim F As New frmVentasApartados(DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value, 0, 0, 0)
                F.ShowDialog()
                F.Dispose()
            Else
                IdApartado = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
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
        IdApartado = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        IdApartado = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
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
        cmbEstado.Items.Add("Todos")
        cmbEstado.Items.Add("Pendiente")
        cmbEstado.Items.Add("Guardado")
        cmbEstado.Items.Add("Cancelado")

        ComboBox1.Items.Add("Todos")
        ComboBox1.Items.Add("Alta")
        ComboBox1.Items.Add("Media")
        ComboBox1.Items.Add("Baja")
        ComboBox2.Items.Add("Todos")
        ComboBox2.Items.Add("Por Surtir")
        ComboBox2.Items.Add("Surtido")

        ComboBox2.SelectedIndex = 0
        If Tipo = 1 Then
            cmbEstado.SelectedIndex = 2
            ComboBox2.SelectedIndex = 1
        Else
            cmbEstado.SelectedIndex = 0
        End If
        ComboBox1.SelectedIndex = 0
        'If Modo = ModosDeBusqueda.SoloDeudas Then
        '    ComboBox1.SelectedIndex = 2
        '    cmbEstado.SelectedIndex = 2
        '    ComboBox1.Enabled = False
        '    cmbEstado.Enabled = False
        'Else

        'End If

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
        Consulta()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
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

        Dim S As New dbVentasApartados(MySqlcon)
        Dim Su As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        'V.Reporte(Tipo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente)
        Rep = New repVentasApartados
        Rep.SetDataSource(S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), txtCliente.Text, txtFolio.Text, Estado, 0, ComboBox1.SelectedIndex, ComboBox2.SelectedIndex))
        Rep.SetParameterValue("Encabezado", Su.Nombre)
        'Dim Filtros As String
        'Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Vendedor: " + ComboBox2.Text
        'If CheckBox14.Checked Then
        '    Filtros += " SOLO CANCELADAS"
        'End If
        'Rep.SetParameterValue("Filtros", Filtros)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub
End Class