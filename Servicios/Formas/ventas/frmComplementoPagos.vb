Public Class frmComplementoPagos
    Public CPago As dbComplementoPagos
    Dim IdsMonedas As New elemento
    Dim IdsCuentas As New elemento
    Dim IdsFormas As New elemento
    Dim Op As dbOpciones
    Dim ImpDoc As ImprimirDocumento
    Private Sub frmComplementoPagos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Op = New dbOpciones(MySqlcon)
        ImpDoc = New ImprimirDocumento
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
            CPago.RFCEmisorBen = TextBox8.Text
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
            Timbrar()
            Imprimir()
            Me.Close()
        End If

    End Sub
    Private Function Timbrar() As Boolean
        Dim en As New Encriptador
        Dim HuboError As Boolean = False
        If (CPago.uuid = "**No Timbrado**" Or CPago.uuid = "") And CPago.Estado = Estados.Guardada Then
            If dtpFecha.Value.ToString("yyyy/MM/dd") < DateAdd(DateInterval.Day, -2, Date.Now).ToString("yyyy/MM/dd") Then
                If MsgBox("La fecha ya no es válida para timbrar esta pago. ¿Cambiarla a la fecha actual?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    CPago.ModificarFechaHora()
                    dtpFecha.Value = Date.Now
                Else
                    Return False
                End If
            End If

        End If

        Dim RutaXml As String = ""
        Dim RutaPDF As String = ""
        Dim MsgError As String = ""
        Dim Cadena As String = ""
        Dim Sello As String = ""
        Try
            Cadena = CPago.CreaCadenaOriginali33(CPago.IdCPago, GlobalIdMoneda, Sello, GlobalIdEmpresa, "", 0, "", GlobalIdSucursalDefault)
            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
            RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
            Archivos.CierraDB()
            Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(CPago.Fecha), "yyyy"), True)
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\" + Format(CDate(CPago.Fecha), "MM") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\" + Format(CDate(CPago.Fecha), "MM") + "\")
            RutaXml = RutaXml + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\" + Format(CDate(CPago.Fecha), "MM") + "\PSSPAGO-" + CPago.Serie + CPago.Folio.ToString + ".xml"
            If Op._NoRutas = "0" Then
                RutaPDF = RutaPDF + "\" + Format(CDate(CPago.Fecha), "yyyy") + "\" + Format(CDate(CPago.Fecha), "MM")
            End If
            Dim strXML As String
            strXML = CPago.CreaXMLi33(CPago.IdCPago, GlobalIdMoneda, Sello, GlobalIdEmpresa, "", GlobalIdSucursalDefault)

            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            If (CPago.uuid = "**No Timbrado**" Or CPago.uuid = "") Then
                If GlobalPacCFDI = 2 Then
                    en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                    Dim Timbre As String
                    Dim sa As New dbSucursalesArchivos
                    sa.DaOpciones(GlobalIdEmpresa, True)
                    Timbre = Timbrar33(S.RFC, strXML, "", Op._ApiKey, True, CPago.Folio, CPago.Serie, "Pago", CPago.IdCPago)
                    If UCase(Timbre.Substring(0, 5).ToUpper) <> "ERROR" Then
                        Dim xmldoc As New Xml.XmlDocument
                        en.GuardaArchivoTexto(RutaXml, Timbre, System.Text.Encoding.UTF8)
                        xmldoc.Load(RutaXml)
                        CPago.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                        CPago.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("SelloCFD").Value
                        CPago.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("NoCertificadoSAT").Value
                        CPago.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                        CPago.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("SelloSAT").Value
                        CPago.RFCProvCertif = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("RfcProvCertif").Value
                        CPago.GuardaDatosTimbrado()
                        en.GuardaArchivoTexto(RutaXml, Timbre, System.Text.Encoding.UTF8)

                    Else
                        MsgError = Timbre
                        CPago.NoCertificadoSAT = "Error"
                        HuboError = True
                    End If
                End If

            Else
                'Crear xml timbrado
                Dim ExisteArchivo As Boolean = False
                If IO.File.Exists(RutaXml) Then ExisteArchivo = True
                If ExisteArchivo = False Then
                    Dim strTimbrado As String = ""
                    strTimbrado += "<tfd:TimbreFiscalDigital Version=""1.1"" UUID=""" + CPago.uuid + """ FechaTimbrado=""" + CPago.FechaTimbrado + """ selloCFD=""" + CPago.SelloCFD + """ NoCertificadoSAT=""" + CPago.NoCertificadoSAT + """ selloSAT=""" + CPago.SelloSAT + """ RfcProvCertif=""" + CPago.RFCProvCertif + """ xsi:schemaLocation=""http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/TimbreFiscalDigital/TimbreFiscalDigital.xsd"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital""/>"
                    strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Complemento>"), strTimbrado)
                    en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                End If
            End If

            If CPago.NoCertificadoSAT = "Error" Then
                MsgBox("Ha ocurrido un error en el timbrado del pago, intente mas tarde." + vbCrLf + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
                AddErrorTimbrado(Replace(MsgError, "'", "''"), "Pagos - Timbrado", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), CPago.IdCPago)
                HuboError = True
            End If
        Catch ex As Exception
            AddError(Replace(ex.Message, "'", "''"), "Pagos - Timbrado", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), CPago.IdCPago)
            MsgBox("Error al timbrar " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            HuboError = True
        End Try
    End Function

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ImpDoc.DibujaPaginaN(e.Graphics)
        If ImpDoc.MasPaginas = True Or ImpDoc.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(ImpDoc.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If
        'If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
        '    e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
        'End If
        e.HasMorePages = ImpDoc.MasPaginas
    End Sub
    Private Sub Imprimir()
        Try
            'Dim Nomina As New dbNominas(pIdNomina, MySqlcon)
            CPago.LlenaDatos()
            ImpDoc.IdSucursal = GlobalIdSucursalDefault
            ImpDoc.TipoDocumento = TiposDocumentos.ComplementoPagos
            ImpDoc.TipoDocumentoT = TiposDocumentos.ComplementoPagos + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.ComplementoPagos
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.PagosPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresion()
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(dtpFecha.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(dtpFecha.Value, "yyyy") + "\" + Format(dtpFecha.Value, "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(dtpFecha.Value, "yyyy") + "\" + Format(dtpFecha.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "PSSPAGO-" + CPago.Serie + CPago.Folio.ToString("0000")
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaNodosImpresion()

        'Dim V As New dbNominas(idNota, MySqlcon)
        Dim Sucursal As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        'Dim O As New dbOpciones(MySqlcon)
        'V.DaTotal(idNota, 2)
        'V.DaDatosTimbrado(idNota)

        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "direccion", Sucursal.Direccion, 0), "direccion")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "noexterior", Sucursal.NoExterior, 0), "noexterior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nointerior", Sucursal.NoInterior, 0), "nointerior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "colonia", Sucursal.Colonia, 0), "colonia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudad", Sucursal.Ciudad, 0), "ciudad")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "estado", Sucursal.Estado, 0), "estado")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cp", Sucursal.CP, 0), "cp")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "tel", Sucursal.Telefono, 0), "tel")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "email", Sucursal.Email, 0), "email")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "referencia", Sucursal.ReferenciaDomicilio, 0), "referencia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "municipio", Sucursal.Municipio, 0), "municipio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "regimenfiscal", Sucursal.RegimenFiscal, 0), "regimenfiscal")
        Dim Cliente As New dbClientes(CPago.IdCliente, MySqlcon)
        If Cliente.DireccionFiscal = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", Cliente.Direccion, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", Cliente.NoExterior, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", Cliente.NoInterior, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", Cliente.Colonia, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", Cliente.CP, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", Cliente.Ciudad, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", Cliente.Estado, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", Cliente.Municipio, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "paiscliente", Cliente.Pais, 0), "paiscliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", Cliente.ReferenciaDomicilio, 0), "refcliente")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", Cliente.Direccion2, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", Cliente.NoExterior2, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", Cliente.NoInterior2, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", Cliente.Colonia2, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", Cliente.CP2, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", Cliente.Ciudad2, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", Cliente.Estado2, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", Cliente.Municipio2, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "paiscliente", Cliente.Pais2, 0), "paiscliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", Cliente.ReferenciaDomicilio2, 0), "refcliente")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfccliente", Cliente.RFC, 0), "rfccliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "curpcliente", Cliente.CURP, 0), "curpcliente")

        Dim FP As New dbFormasdePago(MySqlcon)
        Dim strMetodos As String = FP.FormaSat(CPago.idForma)
        ImpDoc.ImpND.Add(New NodoImpresionN("", "metododepago", CPago.idForma.ToString("00") + " " + strMetodos, 0), "metododepago")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", CPago.Serie, 0), "serie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "folio", CPago.Folio, 0), "folio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "certificado", CPago.NoCertificado, 0), "certificado")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "fecha", Replace(CPago.Fecha, "/", "-"), 0), "fecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "hora", CPago.Hora, 0), "hora")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "uuid", CPago.uuid, 0), "uuid")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cersat", CPago.NoCertificadoSAT, 0), "cersat")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "fechatimbrado", CPago.FechaTimbrado, 0), "fechatimbrado")

        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(CPago.Fecha, "/", "-") + " " + CPago.Hora, 0), "lugar")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(CPago.Fecha, "/", "-") + " " + CPago.Hora, 0), "lugar")
        End If
        Dim CadenaCFDI As String
        CadenaCFDI = "||1.0|" + CPago.uuid + "|" + CPago.FechaTimbrado + "|" + CPago.SelloCFD + "|" + CPago.NoCertificadoSAT + "||"
        ImpDoc.ImpND.Add(New NodoImpresionN("", "sellocfdi", CPago.SelloCFD, 0), "sellocfdi")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "sellosat", CPago.SelloSAT, 0), "sellosat")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")




        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        DR = CPago.ConsultaDetallesReader
        ImpDoc.ImpNDD.Clear()
        ImpDoc.CuantosRenglones = 0
        Dim Cont As Integer = 0
        While DR.Read
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "iddoc", DR("iddocumento"), 0), "iddoc" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "serier", DR("serier"), 0), "serier" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "folior", DR("folior"), 0), "folior" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "monedadr", DR("monedadr"), 0), "monedadr" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipodecambiodr", Format(DR("tipodecambiodr"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "tipodecambiodr" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "metododepagodr", DR("metododepagodr"), 0), "metododepagodr" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "numparcialidad", Format(DR("numparcialidad"), "00"), 0), "numparcialidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impsaldoant", Format(DR("impsaldoant"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "impsaldoant" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "imppagado", Format(DR("imppagado"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "imppagado" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impactual", Format(DR("impactual"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "impactual" + Format(Cont, "000"))

            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()

        'ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        
        'ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(CPago.Monto, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "total")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Tipo de cambio:", Format(CPago.Monto, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "tipodecambio")
        If CPago.idMoneda = 2 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "Moneda: ", "MXN", 0), "moneda")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "Moneda: ", "USD", 0), "moneda")
        End If

        ImpDoc.ImpND.Add(New NodoImpresionN("", "No. Operación: ", CPago.NumOperacion, 0), "nooperacion")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "RFC Cuenta Emisora: ", CPago.RFCEmisorCuenta, 0), "rfcctaemisora")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Banco Extranjero:", CPago.NombreBancoExt, 0), "nombrebancoext")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "No. Cuenta Emisora: ", CPago.NoCuentaEmisor, 0), "nocuentaemisora")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "RFC Cuenta Beneficiario: ", CPago.RFCEmisorBen, 0), "rfcctaben")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "No. Cuenta Beneficiario: ", CPago.NoCuenta, 0), "nocuenta")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Certificado Pago: ", CPago.CertificadoPago, 0), "certificadopago")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Cadena Pago: ", CPago.CadenaPago, 0), "cadenapago")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Sello Pago: ", CPago.SelloPago, 0), "sellopago")


        Dim f As New StringFunctions
        Dim CL As New CLetras
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(CPago.Monto, CPago.idMoneda, GlobalIdiomaLetras), 0), "totalletra")
        If CPago.Estado = Estados.Cancelada Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        ImpDoc.Posicion = 0
        Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        ImpDoc.CodigoBidimensional = CB.Encode("?re=" + Sucursal.RFC + "&rr=" + Cliente.RFC + "&tt=" + Format(CPago.Monto, "0000000000.000000") + "&id=" + CPago.uuid, System.Text.Encoding.Default)
        ImpDoc.NumeroPagina = 1
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Imprimir()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CPago.Cancelar()
    End Sub
End Class