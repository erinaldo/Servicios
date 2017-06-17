Public Class frmComplementoPagos
    Public CPago As dbComplementoPagos
    Dim IdsMonedas As New elemento
    Dim IdsCuentas As New elemento
    Dim IdsFormas As New elemento
    Dim Op As dbOpciones
    Private Sub frmComplementoPagos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Op = New dbOpciones(MySqlcon)
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IdsMonedas, "idmoneda>1")
        LlenaCombos("tblformasdepagosat", cmbFormapago, "concat(if(clave<1000,lpad(convert(clave using utf8),2,'0'),''),' ',descripcion)", "nombret", "idforma", IdsFormas, "clave<1000", , "clave")
        LlenaCombos("tblcuentas", ComboBox2, "numero", "cuentaN", "idcuenta", IdsCuentas)
        If CPago.IdCPago <> 0 Then
            CPago.LlenaDatos()
            dtpFecha.Value = CPago.Fecha
            TextBox1.Text = CPago.Serie
            TextBox2.Text = CPago.Folio.ToString
            TextBox3.Text = CPago.Monto.ToString("0.00")
            ComboBox1.SelectedIndex = IdsMonedas.Busca(CPago.idMoneda)
            TextBox4.Text = CPago.TipoDeCambio
            cmbFormapago.SelectedIndex = IdsFormas.Busca(CPago.idForma)
            txtReferencia.Text = CPago.NumOperacion
            TextBox5.Text = CPago.RFCEmisorCuenta
            TextBox6.Text = CPago.NombreBancoExt
            TextBox7.Text = CPago.NoCuentaEmisor
            ComboBox2.Text = CPago.NoCuenta
            If CPago.TipoCadPago = "SPEI" Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
            TextBox9.Text = CPago.CertificadoPago
            TextBox10.Text = CPago.CadenaPago
            TextBox11.Text = CPago.SelloPago
            If CPago.Estado = 4 Then
                Label16.Visible = True
                Label16.Text = "FECHA DE CANCELACIÓN: " + CPago.FechaCancelado
                For Each C As Control In Me.Controls
                    C.Enabled = False
                Next
                Button5.Enabled = True
                Button3.Enabled = True
            End If
            If CPago.Estado = 3 Then
                For Each C As Control In Me.Controls
                    C.Enabled = False
                Next
                Button5.Enabled = True
                Button2.Enabled = True
                Button3.Enabled = True
            End If
        End If

    End Sub
    Public Sub New(pIdPago As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        CPago = New dbComplementoPagos(MySqlcon)
        ' Add any initialization after the InitializeComponent() call.
        CPago.IdCPago = pIdPago
    End Sub

  
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text = "XEXX010101000" Then
            Label8.Visible = True
            TextBox6.Text = True
        Else
            Label8.Visible = False
            TextBox6.Text = False
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox9.Enabled = True
            TextBox10.Enabled = True
            TextBox11.Enabled = True
        Else
            TextBox9.Enabled = False
            TextBox10.Enabled = False
            TextBox11.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Guardar()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Guardar()
        Dim MsgError As String = ""
        If CPago.ChecaFolioRepetido(CInt(TextBox2.Text), TextBox1.Text) Then
            MsgError = "Folio Repetido"
        End If

        If MsgError = "" Then
            CPago.Fecha = dtpFecha.Value.ToString("yyyy/MM/dd")
            CPago.Serie = TextBox1.Text
            CPago.Folio = CInt(TextBox2.Text)
            CPago.Monto = CDbl(TextBox3.Text)
            CPago.idMoneda = IdsMonedas.Valor(ComboBox1.SelectedIndex)
            CPago.TipoDeCambio = CDbl(TextBox4.Text)
            CPago.idForma = IdsFormas.Valor(cmbFormapago.SelectedIndex)
            CPago.NumOperacion = txtReferencia.Text
            CPago.RFCEmisorCuenta = TextBox5.Text
            CPago.NombreBancoExt = TextBox6.Text
            CPago.NoCuentaEmisor = TextBox7.Text
            CPago.NoCuenta = ComboBox2.Text
            If CheckBox1.Checked Then
                CPago.TipoCadPago = "SPEI"
            Else
                CPago.TipoCadPago = ""
            End If
            CPago.CertificadoPago = TextBox9.Text
            CPago.CadenaPago = TextBox10.Text
            CPago.SelloPago = TextBox11.Text
            CPago.Estado = 3
            CPago.Modificar()
        End If

    End Sub
    'Private Function Timbrar() As Boolean
    '    Dim en As New Encriptador
    '    Dim HuboError As Boolean = False
    '    If (CPago.uuid = "**No Timbrado**" Or CPago.uuid = "") And CPago.Estado = Estados.Guardada Then
    '        If dtpFecha.Value.ToString("yyyy/MM/dd") < DateAdd(DateInterval.Day, -2, Date.Now).ToString("yyyy/MM/dd") Then
    '            If MsgBox("La fecha ya no es válida para timbrar esta pago. ¿Cambiarla a la fecha actual?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
    '                CPago.ModificarFechaHora()
    '                dtpFecha.Value = Date.Now
    '            Else
    '                Return False
    '            End If
    '        End If

    '    End If

    '    Dim RutaXml As String = ""
    '    Dim RutaPDF As String = ""
    '    Dim MsgError As String = ""
    '    Dim Cadena As String
    '    Dim Sello As String
    '    Try
    '        Cadena = V.CreaCadenaOriginali33(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa, pXMLAdenda, Op.FacturaComoegreso, pCadenaOriginalComp)
    '        Dim Archivos As New dbSucursalesArchivos
    '        Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
    '        RutaXml = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
    '        RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
    '        Archivos.CierraDB()
    '        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(CPago.Fecha), "yyyy"), True)
    '        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\")
    '        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\" + Format(CDate(CPago.Fecha), "MM") + "\")
    '        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\")
    '        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\" + Format(CDate(CPago.Fecha), "MM") + "\")
    '        RutaXml = RutaXml + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\" + Format(CDate(CPago.Fecha), "MM") + "\PSSPAGO-" + CPago.Serie + CPago.Folio.ToString + ".xml"
    '        If Op._NoRutas = "0" Then
    '            RutaPDF = RutaPDF + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\" + Format(CDate(CPago.Fecha), "MM")
    '        End If
    '        Dim strXML As String


    '            strXML = V.CreaXMLi33(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa, "", Op.FacturaComoegreso)


    '        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
    '        If (CPago.uuid = "**No Timbrado**" Or CPago.uuid = "") Then
    '            If GlobalPacCFDI = 2 Then
    '                en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
    '                Dim Timbre As String
    '                Dim sa As New dbSucursalesArchivos
    '                sa.DaOpciones(GlobalIdEmpresa, True)
    '                Timbre = Timbrar33(S.RFC, strXML, "", Op._ApiKey, True, CPago.Folio, CPago.Serie, "Pago", CPago.IdCPago)
    '                If UCase(Timbre.Substring(0, 5).ToUpper) <> "ERROR" Then
    '                    Dim xmldoc As New Xml.XmlDocument
    '                    en.GuardaArchivoTexto(RutaXml, Timbre, System.Text.Encoding.UTF8)
    '                    xmldoc.Load(RutaXml)
    '                    CPago.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
    '                    CPago.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("SelloCFD").Value
    '                    CPago.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("NoCertificadoSAT").Value
    '                    CPago.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
    '                    CPago.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("SelloSAT").Value
    '                    CPago.RFCProvCertif = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("RfcProvCertif").Value
    '                    CPago.GuardaDatosTimbrado()
    '                    en.GuardaArchivoTexto(RutaXml, Timbre, System.Text.Encoding.UTF8)

    '                Else
    '                    MsgError = Timbre
    '                    CPago.NoCertificadoSAT = "Error"
    '                    HuboError = True
    '                End If
    '            End If

    '        Else
    '            'Crear xml timbrado
    '            Dim ExisteArchivo As Boolean = False
    '            If IO.File.Exists(RutaXml) Then ExisteArchivo = True
    '            If ExisteArchivo = False Then
    '                Dim strTimbrado As String
    '                strTimbrado += "<tfd:TimbreFiscalDigital Version=""1.1"" UUID=""" + CPago.uuid + """ FechaTimbrado=""" + CPago.FechaTimbrado + """ selloCFD=""" + CPago.SelloCFD + """ NoCertificadoSAT=""" + CPago.NoCertificadoSAT + """ selloSAT=""" + CPago.SelloSAT + """ RfcProvCertif=""" + CPago.RFCProvCertif + """ xsi:schemaLocation=""http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/TimbreFiscalDigital/TimbreFiscalDigital.xsd"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital""/>"
    '                strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Complemento>"), strTimbrado)
    '                en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
    '            End If
    '        End If

    '        If CPago.NoCertificadoSAT = "Error" Then
    '            MsgBox("Ha ocurrido un error en el timbrado del pago, intente mas tarde." + vbCrLf + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
    '            AddErrorTimbrado(Replace(MsgError, "'", "''"), "Pagos - Timbrado", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), CPago.IdCPago)
    '            HuboError = True
    '        End If
    '    Catch ex As Exception
    '        AddError(Replace(ex.Message, "'", "''"), "Pagos - Timbrado", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), CPago.IdCPago)
    '        MsgBox("Error al timbrar " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '        HuboError = True
    '    End Try
    'End Function
End Class