Public Class frmVentasCorte
    Dim IdsSucursales As New elemento
    Dim IdsCajas As New elemento
    Dim ConsultaOn As Boolean = False
    Dim Oc As dbOpcionesOc
    Dim V As dbVentas
    Private Sub frmVentasCorte_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            Oc = New dbOpcionesOc(MySqlcon)
            Oc.LlenaDatos(0, GlobalIdSucursalDefault)
            V = New dbVentas(MySqlcon)
            DateTimePicker2.Value = Date.Now
            DateTimePicker3.Value = Date.Now
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
        V.LlenaTablaCorte(DateTimePicker2.Value.ToString("yyyy/MM/dd"), DateTimePicker3.Value.ToString("yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsCajas.Valor(ComboBox6.SelectedIndex), Oc.OcultarOc, Oc.SeriesOc)
        DGVentas.DataSource = V.ConsultaCorte(1, chkTiempoReal.Checked)
        DGVentas.Columns(0).HeaderText = "Concepto"
        DGVentas.Columns(1).HeaderText = "Importe"
        DGVentas.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGVentas.Columns(1).Width = 170
        DGVentas.Columns(2).Visible = False
        DGVentas.Columns(3).Visible = False

        DGCompras.DataSource = V.ConsultaCorte(2, chkTiempoReal.Checked)
        DGCompras.Columns(0).HeaderText = "Concepto"
        DGCompras.Columns(1).HeaderText = "Importe"
        DGCompras.Columns(1).Width = 170
        DGCompras.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGCompras.Columns(2).Visible = False
        DGCompras.Columns(3).Visible = False

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
        'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
        Rep = New repVentasCorte
        Rep.SetDataSource(V.ConsultaCorte(1, chkTiempoReal.Checked))
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Rep.SetParameterValue("Encabezado", S.Nombre)
        Dim Filtros As String
        Filtros = "Fechas: " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker3.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text
        Rep.SetParameterValue("Filtros", Filtros)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub
End Class