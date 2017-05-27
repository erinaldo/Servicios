Public Class frmEmpeniosCorte
    Dim Co As New dbEmpeniosReportes(MySqlcon)
    Dim fechaInicial As String
    Dim fechaFinal As String
    Dim IdsSucursales As New elemento
    Dim IdsCajas As New elemento
    Dim idCaja As Integer = -1
    Dim idSucursal As Integer = -1
    Dim sa As New dbSucursalesArchivos
    Private Sub frmEmpeniosCorte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblsucursales", cmbSucursal, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        dtpFecha.Value = Date.Now
        dtpFechaHasta.Value = Date.Now
        cmbSucursal.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        dtpFechaHasta.MinDate = dtpFecha.Value
        buscar()
    End Sub
    Public Sub buscar()
        'Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Dim EC As New dbEmpeniosConfiguracion(MySqlcon)
        EC.LlenaDatos()
        fechaInicial = dtpFecha.Value.ToString("yyyy/MM/dd")
        fechaFinal = dtpFechaHasta.Value.ToString("yyyy/MM/dd")


        Co.filtroRMEmpenios(fechaInicial, fechaFinal, idSucursal, idCaja, EC.Vis)
        Co.filtroRMECantidad(fechaInicial, fechaFinal, idSucursal, idCaja, EC.Vis)

        lblSaldoInicial.Text = Co.saldoInicialF.ToString("C2")
        lblEmpenios.Text = Co.prestamosF.ToString("C2")
        lblCapital.Text = Co.capitalF.ToString("C2")
        lblInteres.Text = Co.interesF.ToString("C2")
        lblCompras.Text = Co.comprasF.ToString("C2")
        lblSueldos.Text = Co.sueldosF.ToString("C2")
        lblVarios.Text = Co.variosF.ToString("C2")
        lblIngresos.Text = Co.ingresosF.ToString("C2")
        lblDepositos.Text = Co.depositosF.ToString("C2")
        lblTotal.Text = Co.saldoTotalF.ToString("C2")

        lblCanCapital.Text = Co.CancapitalF
        lblCanCompras.Text = Co.CancomprasF
        lblCanDepositos.Text = Co.CandepositosF
        lblCanEmpenios.Text = Co.CanprestamosF
        lblCanIngresos.Text = Co.CaningresosF
        lblCanInteres.Text = Co.CaninteresF
        lblCanSueldos.Text = Co.CansueldosF
        lblCanVarios.Text = Co.CanvariosF

    End Sub

    Private Sub cmbSucursal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged
        If cmbSucursal.SelectedIndex <> 0 Then
            cmbCaja.Enabled = True
            sa.DaOpciones(GlobalIdEmpresa, False)
            idCaja = sa.idCaja
            LlenaCombos("tblcajas", cmbCaja, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, "Todas")
            cmbCaja.SelectedIndex = IdsCajas.Busca(idCaja)

            idSucursal = IdsSucursales.Valor(cmbSucursal.SelectedIndex)
        Else
            idSucursal = -1
            cmbCaja.Enabled = False
            cmbCaja.Items.Clear()
            idCaja = -1
        End If
    End Sub

    Private Sub cmbCaja_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCaja.SelectedIndexChanged
        If cmbCaja.SelectedIndex = 0 Then
            idCaja = -1
        Else
            idCaja = IdsCajas.Valor(cmbCaja.SelectedIndex)
        End If
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        buscar()
    End Sub

    Private Sub dtpFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        dtpFechaHasta.MinDate = dtpFecha.Value
    End Sub

    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblCanSueldos.Click

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Try
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Rep = New rptEmpeniosCorte
            'Rep.SetDataSource(Co.filtroTodos(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor))
            Rep.SetParameterValue("cSI", "")
            Rep.SetParameterValue("cEMP", lblCanEmpenios.Text)
            Rep.SetParameterValue("cCAP", lblCanCapital.Text)
            Rep.SetParameterValue("cINT", lblCanInteres.Text)
            Rep.SetParameterValue("cCOMP", lblCanCompras.Text)
            Rep.SetParameterValue("cSUEL", lblCanSueldos.Text)
            Rep.SetParameterValue("cVAR", lblCanVarios.Text)
            Rep.SetParameterValue("cINGR", lblCanIngresos.Text)
            Rep.SetParameterValue("cDEPO", lblCanDepositos.Text)

            Rep.SetParameterValue("tSI", lblSaldoInicial.Text)
            Rep.SetParameterValue("tEMP", lblEmpenios.Text)
            Rep.SetParameterValue("tCAP", lblCapital.Text)
            Rep.SetParameterValue("tINT", lblInteres.Text)
            Rep.SetParameterValue("tCOMP", lblCompras.Text)
            Rep.SetParameterValue("tSUEL", lblSueldos.Text)
            Rep.SetParameterValue("tVAR", lblVarios.Text)
            Rep.SetParameterValue("tINGR", lblIngresos.Text)
            Rep.SetParameterValue("tDEPO", lblDepositos.Text)

            Rep.SetParameterValue("total", lblTotal.Text)
            Rep.SetParameterValue("filtros", "Del " + dtpFecha.Value.ToString("dd/MM/yyyy") + " al " + dtpFechaHasta.Value.ToString("dd/MM/yyyy") + vbCrLf + cmbSucursal.Text + vbCrLf + "CAJA: " + cmbCaja.Text)
            Dim PrintLayout As New CrystalDecisions.Shared.PrintLayoutSettings
            PrintLayout.Scaling = CrystalDecisions.Shared.PrintLayoutSettings.PrintScaling.Scale
            'PrintLayout.Centered = True
            Dim PS As New System.Drawing.Printing.PrinterSettings
            PS.PrinterName = My.Settings.impresoraempenios
            Dim pageSettings As New System.Drawing.Printing.PageSettings(PS)
            Rep.PrintOptions.PrinterName = My.Settings.impresoraempenios
            'Rep.PrintOptions.DissociatePageSizeAndPrinterPaperSize = True
            Rep.PrintToPrinter(PS, pageSettings, False, PrintLayout)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        

        'Dim RV As New frmReportes(Rep, False)
        'RV.Show()
    End Sub
End Class