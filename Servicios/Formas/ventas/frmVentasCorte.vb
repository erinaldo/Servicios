Public Class frmVentasCorte
    Dim IdsSucursales As New elemento
    Dim IdsCajas As New elemento
    Dim ConsultaOn As Boolean = False
    Dim Oc As dbOpcionesOc
    Dim V As dbVentas
    Dim Op As dbOpciones
    Private Sub frmVentasCorte_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            Op = New dbOpciones(MySqlcon)
            Oc = New dbOpcionesOc(MySqlcon)
            Oc.LlenaDatos(0, GlobalIdSucursalDefault)
            V = New dbVentas(MySqlcon)
            DateTimePicker2.Value = Date.Now
            DateTimePicker3.Value = Date.Now
            DateTimePicker1.Value = "2100/01/01 00:00"
            DateTimePicker4.Value = "2100/01/01 23:59"
            LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "TODAS")
            ConsultaOn = True
            Consulta()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim Ctemo As Boolean
        Ctemo = ConsultaOn
        ConsultaOn = False
        LlenaCombos("tblcajas", ComboBox6, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "TODAS")
        ConsultaOn = Ctemo
        If ConsultaOn Then Consulta()
    End Sub
    Private Sub Consulta()
        V.LlenaTablaCorte(DateTimePicker2.Value.ToString("yyyy/MM/dd"), DateTimePicker3.Value.ToString("yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsCajas.Valor(ComboBox6.SelectedIndex), Oc.OcultarOc, Oc.SeriesOc, DateTimePicker1.Value.ToString("HH:mm:00"), DateTimePicker4.Value.ToString("HH:mm:59"), Op.VentasCorteRemTodas, CheckBox6.Checked, Op.VentasCorteRemxMetodo)
        DGVentas.DataSource = V.ConsultaCorte(1, chkTiempoReal.Checked)
        DGVentas.Columns(0).HeaderText = "Concepto"
        DGVentas.Columns(1).HeaderText = "Importe"
        DGVentas.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGVentas.Columns(1).Width = 170
        DGVentas.Columns(2).Visible = False
        DGVentas.Columns(3).Visible = False
        DGVentas.Columns(4).Visible = False

        DGCompras.DataSource = V.ConsultaCorte(2, chkTiempoReal.Checked)
        DGCompras.Columns(0).HeaderText = "Concepto"
        DGCompras.Columns(1).HeaderText = "Importe"
        DGCompras.Columns(1).Width = 170
        DGCompras.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGCompras.Columns(2).Visible = False
        DGCompras.Columns(3).Visible = False
        DGCompras.Columns(4).Visible = False

        DGVentas.ClearSelection()
        DGCompras.ClearSelection()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        If ConsultaOn Then Consulta()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker3.ValueChanged
        If ConsultaOn Then Consulta()
    End Sub

    Private Sub chkTiempoReal_CheckedChanged(sender As Object, e As EventArgs) Handles chkTiempoReal.CheckedChanged
        If ConsultaOn Then Consulta()
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ConsultaOn Then Consulta()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub DGVentas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVentas.CellContentClick

    End Sub

    Private Sub DGVentas_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGVentas.CellFormatting
        If e.ColumnIndex = 1 Then
            e.Value = Format(e.Value, "$#,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub DGCompras_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGCompras.CellContentClick

    End Sub

    Private Sub DGCompras_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGCompras.CellFormatting
        If e.ColumnIndex = 1 Then
            e.Value = Format(e.Value, "$#,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Rep = New repVentasCorte
        Dim comentario As String = ""
        Dim res = MsgBox("¿Desea agregar comentarios al corte?", MsgBoxStyle.YesNo)
        If res = DialogResult.Yes Then
            Dim et As New frmVentasTextoExtra("", 5000, False, "Arial", 12)
            If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
                comentario = et.Texto
            End If
            et.Dispose()
        End If
        Rep.SetDataSource(V.ConsultaCorte(1, chkTiempoReal.Checked))
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Rep.SetParameterValue("Encabezado", S.Nombre)
        Dim Filtros As String
        If CheckBox6.Checked = False Then
            Filtros = "Fechas: " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker3.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text
        Else
            Filtros = "Fechas: " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " " + Format(DateTimePicker1.Value, "HH:mm") + " al " + Format(DateTimePicker3.Value, "yyyy/MM/dd") + " " + Format(DateTimePicker4.Value, "HH:mm") + " Sucursal: " + ComboBox1.Text
        End If
        Rep.SetParameterValue("Filtros", Filtros)
        Rep.SetParameterValue("comentario", comentario)
        Rep.SetParameterValue("todo", True)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
        Rep = New repVentasCorte
        Dim comentario As String = ""
        Dim res = MsgBox("¿Desea agregar comentarios al corte?", MsgBoxStyle.YesNo)
        If res = DialogResult.Yes Then
            Dim et As New frmVentasTextoExtra("", 5000, False, "Arial", 12)
            If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
                comentario = et.Texto
            End If
            et.Dispose()
        End If
        Rep.SetDataSource(V.ConsultaCorteConfiguracion(1, chkTiempoReal.Checked, V.listaConfiguracion, Op.VentasCorteRemxMetodo))
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Rep.SetParameterValue("Encabezado", S.Nombre)
        Dim Filtros As String
        If CheckBox6.Checked = False Then
            Filtros = "Fechas: " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker3.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text
        Else
            Filtros = "Fechas: " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " " + Format(DateTimePicker1.Value, "HH:mm") + " al " + Format(DateTimePicker3.Value, "yyyy/MM/dd") + " " + Format(DateTimePicker4.Value, "HH:mm") + " Sucursal: " + ComboBox1.Text
        End If
        Rep.SetParameterValue("Filtros", Filtros)
        Rep.SetParameterValue("comentario", comentario)
        Rep.SetParameterValue("todo", False)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim F As New frmConfiguracionCorte
        F.ShowDialog()
        F.Dispose()
        Op = New dbOpciones(MySqlcon)
        Consulta()
    End Sub

    Private Sub DateTimePicker4_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker4.ValueChanged
        If ConsultaOn And CheckBox6.Checked Then Consulta()
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If ConsultaOn Then Consulta()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If ConsultaOn And CheckBox6.Checked Then Consulta()
    End Sub
End Class