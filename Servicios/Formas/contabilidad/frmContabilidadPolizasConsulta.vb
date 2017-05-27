Public Class frmContabilidadPolizasConsulta
    Public IdPoliza As Integer
    Dim ConsultaOn As Boolean
    Dim IdsClasificacionPolizaBusqeda As New elemento
    Dim P As dbContabilidadPolizas
    
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
    Private Sub AbreDetalles()
        If IdPoliza <> 0 Then
            IdPoliza = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub DGServicios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellClick
        IdPoliza = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DGServicios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then

                Dim tipo As Integer
                If cmbTipoBuscar.SelectedIndex = 4 Then
                    tipo = 5
                Else
                    tipo = cmbTipoBuscar.SelectedIndex
                End If
                Dim clas As Integer

                If cmbClasBusqueda.Items.Count = 0 Then
                    clas = 0
                Else
                    clas = IdsClasificacionPolizaBusqeda.Valor(cmbClasBusqueda.SelectedIndex)
                End If
                DGServicios.DataSource = P.reporte(dtpDesde.Value.Year.ToString + "/" + dtpDesde.Value.Month.ToString("00") + "/" + dtpDesde.Value.Day.ToString("00"), dtpHasta.Value.Year.ToString + "/" + dtpHasta.Value.Month.ToString("00") + "/" + dtpHasta.Value.Day.ToString("00"), txtBuscarConecpto.Text, tipo, clas, CheckBox1.Checked, TextBox4.Text)
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(5).Visible = False
                DGServicios.Columns(1).HeaderText = "Póliza"
                DGServicios.Columns(1).Width = 70
                DGServicios.Columns(4).Width = 100
                DGServicios.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
                DGServicios.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
                Busquedas(VentanasdeBusqueda.ContabilidadPolizas) = dtpDesde.Value.ToString("yyyy/MM/dd") + "ª" + dtpHasta.Value.ToString("yyyy/MM/dd") + "ª" + cmbTipoBuscar.SelectedIndex.ToString + "ª" + txtBuscarConecpto.Text + "ª" + TextBox4.Text + "ª" + cmbClasBusqueda.SelectedIndex.ToString + "ª" + CheckBox1.Checked.ToString
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    
    Private Sub frmContabilidadPolizasConsulta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        P = New dbContabilidadPolizas(MySqlcon)
        LlenaCombos("tblcontabilidadclas", cmbClasBusqueda, "nombre", "nombret", "id", IdsClasificacionPolizaBusqeda, , "TODOS", "nombre")
        If P.ActivarFechaTrabajo = 0 Then
            dtpDesde.Value = "01/" + Date.Now.Month.ToString + "/" + P.anio
        Else
            dtpDesde.Value = Format(CDate(P.FechaTRabajo), "yyyy/MM/01")
        End If
        cmbTipoBuscar.SelectedIndex = 0
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        Try
            If Busquedas(VentanasdeBusqueda.ContabilidadPolizas) <> "" Then
                Dim Campos() As String
                Campos = Busquedas(VentanasdeBusqueda.ContabilidadPolizas).Split("ª")
                dtpDesde.Value = Campos(0)
                dtpHasta.Value = Campos(1)
                cmbTipoBuscar.SelectedIndex = CInt(Campos(2))
                txtBuscarConecpto.Text = Campos(3)
                TextBox4.Text = Campos(4)
                cmbClasBusqueda.SelectedIndex = CInt(Campos(5))
                If Campos(6) = "True" Then CheckBox1.Checked = True
            End If
        Catch ex As Exception

        End Try
        ConsultaOn = True
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub dtpDesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpDesde.ValueChanged
        Dim fecha As Date
        Dim ConsultaOnTemp As Boolean
        fecha = CDate(Format(dtpDesde.Value, "yyyy/MM/01"))
        fecha = DateAdd(DateInterval.Month, 1, fecha)
        fecha = DateAdd(DateInterval.Day, -1, fecha)
        ConsultaOnTemp = ConsultaOn
        ConsultaOn = False
        dtpHasta.Value = fecha
        ConsultaOn = ConsultaOnTemp
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub dtpHasta_ValueChanged(sender As Object, e As EventArgs) Handles dtpHasta.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub cmbTipoBuscar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoBuscar.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub txtBuscarConecpto_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarConecpto.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub cmbClasBusqueda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClasBusqueda.SelectedIndexChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGServicios_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        IdPoliza = DGServicios.Item(0, DGServicios.CurrentCell.RowIndex).Value
        AbreDetalles()
    End Sub

    Private Sub DGServicios_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        If DGServicios.RowCount > 0 Then
            If e.ColumnIndex = 4 Then
                If e.Value <> "" Then
                    e.Value = Format(e.Value + 0, "$###,##0.00")
                End If
            End If
        End If
        If DGServicios.Item(5, e.RowIndex).Value >= 0.01 Or DGServicios.Item(5, e.RowIndex).Value <= -0.01 Then
            e.CellStyle.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AbreDetalles()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Consulta()
    End Sub
End Class