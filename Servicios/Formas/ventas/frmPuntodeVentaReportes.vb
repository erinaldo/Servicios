Public Class frmPuntodeVentaReportes
    Dim IdsVendedores As New elemento
    Dim IdsSucursales As New elemento
    Dim IdsCajas As New elemento
    Private Sub frmPuntodeVentaReportes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblvendedores", ComboBox2, "nombre", "nombret", "idvendedor", IdsVendedores, , "Todos")
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        ComboBox3.Items.Add("Todos")
        ComboBox3.Items.Add("Depósito")
        ComboBox3.Items.Add("Retiro")
        ComboBox3.SelectedIndex = 0
        DateTimePicker1.Value = Now
        DateTimePicker2.Value = Now
        DateTimePicker3.Value = "2013/01/01 00:00"
        DateTimePicker4.Value = "2013/01/01 23:59"
        TextBox1.Text = My.Settings.impresorapv
        If My.Settings.cortexformato = 0 Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If
        ListBox1.Items.Add("Corte X.")
        ListBox1.Items.Add("Corte X por vendedor.")
        ListBox1.Items.Add("Corte X por método de pago.")
        ListBox1.Items.Add("Corte X por método de pago concentrado.")
        ListBox1.Items.Add("Movimientos de caja ticket.")
        ListBox1.Items.Add("Movimientos de caja.")
        ComboBox1.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Select Case ListBox1.Text
                Case "Movimientos de caja ticket."
                    Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim CM As New dbCajasMovimientos(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    If RadioButton1.Checked Then
                        Rep = New repCajasMovimientosTicket
                    Else
                        Rep = New repCajasMovimientosTicket2p
                    End If
                    Rep.SetDataSource(CM.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), 0, 0, False, IdsVendedores.Valor(ComboBox2.SelectedIndex), "", IdsCajas.Valor(ComboBox6.SelectedIndex), ComboBox3.SelectedIndex, Oc.OcultarOc, Oc.SeriesOc))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Vendedor: " + ComboBox2.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim PrintLayout As New CrystalDecisions.Shared.PrintLayoutSettings
                    PrintLayout.Scaling = CrystalDecisions.Shared.PrintLayoutSettings.PrintScaling.Scale
                    'PrintLayout.Centered = True
                    Dim PS As New System.Drawing.Printing.PrinterSettings
                    PS.PrinterName = TextBox1.Text
                    Dim pageSettings As New System.Drawing.Printing.PageSettings(PS)
                    Rep.PrintOptions.PrinterName = TextBox1.Text
                    'Rep.PrintOptions.DissociatePageSizeAndPrinterPaperSize = True
                    Rep.PrintToPrinter(PS, pageSettings, False, PrintLayout)

                    'Dim RV As New frmReportes(Rep, False)
                    'RV.Show()
                Case "Movimientos de caja."
                    Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim CM As New dbCajasMovimientos(MySqlcon)
                    Dim OC As New dbOpcionesOc(MySqlcon)
                    OC.LlenaDatos(0, GlobalIdSucursalDefault)
                    Rep = New repCajasMovimientos
                    Rep.SetDataSource(CM.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), 0, 0, False, IdsVendedores.Valor(ComboBox2.SelectedIndex), "", IdsCajas.Valor(ComboBox6.SelectedIndex), ComboBox3.SelectedIndex, OC.OcultarOc, OC.SeriesOc))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Vendedor: " + ComboBox2.Text + " Caja: " + ComboBox6.Text + " Tipo Mov: " + ComboBox3.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case Else
                    PrintDocument1.PrinterSettings.PrinterName = TextBox1.Text
                    If RadioButton1.Checked Then
                        My.Settings.cortexformato = 0
                    Else
                        My.Settings.cortexformato = 1
                    End If

                    My.Settings.impresorapv = TextBox1.Text
                    If TextBox1.Text = "Bullzip PDF Printer" Then
                        Dim AR As New dbSucursalesArchivos()
                        Dim obj As New Bullzip.PdfWriter.PdfSettings
                        obj.Init()
                        obj.PrinterName = TextBox1.Text
                        obj.SetValue("Output", AR.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, True) + "\<docname>.pdf")
                        obj.SetValue("ShowSettings", "never")
                        obj.SetValue("ShowPDF", "yes")
                        obj.SetValue("ShowSaveAS", "nofile")
                        obj.SetValue("ConfirmOverwrite", "no")
                        obj.SetValue("Target", "printer")
                        obj.WriteSettings()
                    End If
                    PrintDocument1.Print()
            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

    
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'ListBox1.Items.Add("Corte X.")
        'ListBox1.Items.Add("Corte X por vendedor.")
        'ListBox1.Items.Add("Corte X por tipo de pago.")
        If ListBox1.SelectedIndex >= 0 Then
            Dim Dibuja As New DibujaReportes
            If RadioButton1.Checked Then
                Dibuja.CorteX3P(e.Graphics, MySqlcon, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsVendedores.Valor(ComboBox2.SelectedIndex), ComboBox2.Text, Format(DateTimePicker3.Value, "HH:mm"), Format(DateTimePicker4.Value, "HH:mm"), CheckBox6.Checked, ListBox1.SelectedIndex, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsCajas.Valor(ComboBox6.SelectedIndex))
            Else
                Dibuja.CorteX2P(e.Graphics, MySqlcon, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsVendedores.Valor(ComboBox2.SelectedIndex), ComboBox2.Text, Format(DateTimePicker3.Value, "HH:mm"), Format(DateTimePicker4.Value, "HH:mm"), CheckBox6.Checked, ListBox1.SelectedIndex, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsCajas.Valor(ComboBox6.SelectedIndex))
            End If
        End If


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex > 0 Then
            LlenaCombos("tblcajas", ComboBox6, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Sel. Caja")
        End If
    End Sub
End Class