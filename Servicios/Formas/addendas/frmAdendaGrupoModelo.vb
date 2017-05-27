Imports MySql.Data.MySqlClient
Imports System.IO
Public Class frmAdendaGrupoModelo
    Private adendas As AdendaModeloDAO
    Private adenda As AdendaModelo
    Private detalles As detalleAdendaModeloDAO
    Private detalle As detalleAdendaModelo
    Private idVenta As Integer
    Private moneda As String
    Private tipoCambio As String
    Private tipoDocumentos() As String = {"FA=Factura/Nota de Cargo", "AN=Anticipo", "ND=Nota de Crédito x devolución", "NA=Nota de Crédito por ajuste en precio", "NE=Nota de Crédito por descuento"}
    Private tipoMonedas() As String = {"ADP", "AED", "AFA", "AFN", "ALL", "AMD", "ANG", "MXN", "USD", "EU"}
    Private idiomas() As String = {"ES", "EN"}
    Private unidadDeMedida As String() = {"PZA", "CA", "SER", "KG", "T", "L"}
    Private tipoArancel() As String = {"VAT=Impuesto sobre el valor agregado (IVA)", "LAC=ISR", "GST=IEPS", "AAA=Impuesto de Petróleo", "AAD=Cigarros", "FRE=Exento", "LOC=Municipal", "STT=Estatal", "OTH=Otros Impuestos"}
    Private catalogoSociedades() As String = {"1001 Grupo Modelo, S.A.B. C.V.",
"1002 Diblo, S.A. de C.V.",
"1003 ANHEUSER-BUSCH MEXICO HOLDING",
"1004 Selket",
"1005 AB International Holding",
"1511 Nva Fabrica Nal de Vidrio",
"1512 Vidriera Indt del Potosí",
"1513 Vidriera de Tierra Blanca",
"1514 Ind Vidriera de Coahuila",
"1521 Ind Vidriera del Potosí",
"1522 Op de Decorado de Envases",
"1523 Ind Decoradora de Envases",
"1524 DIFA Integración Administ",
"1531 Materiales del Istmo, S.A",
"1532 Mat Ind de Cuichapa",
"1541 Construcciones del Anahua",
"1542 Dsitribuidora rib Industrial Cuicha",
"1543 Transportes Empresariales",
"1551 Asistencia Técnica y Admi",
"1561 Fabrica Nal de Molduras",
"1562 Analisis Vidrio y Cerámic",
"1571 Difa Arrendadora, S.A. de CV.",
"2001 Cervecería Modelo SA. De CV.",
"2003 Cervecería Modelo de Guadalajara",
"2005 Compañía Cervecera del Trópico",
"2007 Cervecería Modelo de Torreón",
"2009 Cervecería Modelo del Noroeste",
"2011 Cervecería del Pacífico",
"2013 Compañía  Cervecera de Zacatecas",
"2014 Servicios Modelo Zacatecas",
"2015 Compañía  Cervecera de Coahuila",
"2016 Servicios Modelo Coahuila",
"2017 Compañía  Cervecera de Colima",
"2501 Cervecería Ambev Peru",
"2502 Cervecería Boliviana Nacional",
"2503 Industrias del Atlantico",
"2504 Anheuser Busch INT’L, INC",
"2505 Anheuser Busch Houston",
"2506 Anheuser Busch LA",
"2507 Anheuser Busch Colorado",
"2508 Nanlien International Cor",
"2509 Anheuser Busch México Inc",
"2510 FNC S.A.",
"2511 Cervecería Ambev Dominicana",
"2512 Compañía  de Bebidas Das América",
"2513 Anheuser Busch Packaging",
"2514 Eagle Packaging Inc",
"2515 Glass Container Corporation",
"2516 Metal Container Corporati",
"2517 Cervecería y Malteria Quilmes",
"2518 A-B Inbev Services LLC.",
"2519 Cervecera Nacional Dominicana",
"2520 AB Netherlands Hold II BV",
"2521 Busch Agricultural Resour",
"2522 AB-InBev NV",
"2523 Sun InBev OJSC",
"2524 Anheuser-Busch InBev Chin",
"2525 Labatt Breweries of Canada",
"2526 Anheuser-Busch InBev",
"2527 Cervecería Chile, S.A.",
"2528 Cervecería Paraguaya S.A.",
"2529 Labatt Brewing Company Lt",
"2530 Anheuser-Busch Intl, Inc.",
"2531 AmBev S.A. - F. Jacarei",
"2532 Anheuser-Busch Deutschlan",
"2533 BRASSERIE DE L'ABBAYE DE",
"2534 BREW  RE  S.A.",
"3001 Las Cervezas  Modelo B.California",
"3002 Las Cervezas  Modelo en Sonora",
"3003 Las Cervezas  Modelo Pacífico",
"3004 Las Cervezas  Modelo Nayarit",
"3005 Las Cervezas  Modelo Occidente",
"3006 Las Cervezas  Modelo Michoacán",
"3007 Corona del Balsas",
"3008 Las Cervezas  Modelo Hidalgo",
"3009 Las Cervezas  Modelo en S.L.P.",
"3010 Las Cervezas  Modelo Zacatecas",
"3011 Las Cervezas  Modelo Sureste",
"3012 Las Cervezas  Modelo Campeche",
"3013 Distribuidora de Tabasco",
"3014 Distribuidora Macfe",
"3015 Las Cervezas  Modelo Veracruz",
"3016 Las Cervezas  Modelo Edo.Mex.",
"3017 Las Cervezas  Modelo Centro",
"3018 Las Cervezas  Modelo del Bajío",
"3019 Las Cervezas Modelo Zona Metrop.",
"3020 Las Cervezas  Modelo Acapulco",
"3021 Las Cervezas  Modelo Guerrero",
"3022 Las Cervezas  Modelo Morelos",
"3023 Las Cervezas  Modelo en Oaxaca",
"3024 Agencia Modelo del Istmo",
"3025 Las Cervezas  Modelo Altiplano",
"3026 Dsitribuidora de Modelo en Chihuahua",
"3027 Dsitribuidora  de Cervezas en el Norte",
"3028 Las Cervezas  Modelo Nvo. León",
"3029 Las Cervezas  Modelo Noreste",
"3030 Cervezas Internacionales",
"3031 Dsitribuidora  PacíficoMod Culiacán",
"3032 DISPAMOCUSA S.A. de C.V.",
"3033 DINCUSA S.A. de C.V.",
"3040 Dsitribuidora  de Excelencia Modelo",
"3050 Inmobiliaria y Prom de In",
"4001 Gmodelo Corporation, Inc.",
"4002 Crown Imports, L.L.C.",
"4003 Gmodelo USA, L.L.C.",
"4004 Marcas Modelo, S.A. de C.",
"4005 Gmodelo Internacional, S.",
"4006 Gmodelo Canada, Inc.",
"4007 Procermex, Inc.",
"4008 Iberocermex, S.A.U.",
"4009 Eurocermex, S.A.",
"4010 Asiacermex, P.T.E. Ltd.",
"4011 Latincermex, S.A.",
"4012 Canacermex, Inc.",
"4013 Gmodelo Europa, S.A.U.",
"4014 Riverside Investiments, I",
"4501 Extractos y Maltas,SAdeCV",
"4502 Cebadas y Maltas",
"4503 InteGrow Malt, LLC",
"5001 Envases de Zacatecas",
"5002 Envases y Tapas Modelo",
"5003 Tapas y Tapones Zacatecas",
"5501 Tramo Cia Transportes",
"5502 Fleza, S.A. de C.V.",
"5503 Logistica Transp Sureste",
"5504 Logistica Trans Occidente",
"5505 Logistica Transp Noreste",
"6001 Inamex Cerveza y Malta",
"6002 Maninasa, S.A. de C.V.",
"6501 Diblo Corporativo",
"6502 Cenexis, S.A. de C.V.",
"6503 Soporte Nacional Modelo",
"6504 CDEDF, SA de CV",
"6505 Seguridad Privada Modelo",
"6506 Aeromodelo, S.A. de C.V.",
"6507 Servicios Personal Modelo",
"6508 Sociedad que se borrará",
"6509 Patentes y Marcas Promoc",
"7001 Extrade, S.A. de C.V.",
"7002 Bodegas Alprosa, S.A. de",
"7003 Rancho Cermo, S.A. de C.V",
"7004 Industria del Campo, S.A",
"7005 Promotora de Inmuebles Mk",
"7006 Marketing Modelo, S.A. de",
"7301 Emp Detallistas de México",
"7501 Comercio y Dsitribuidora rib.Modelo",
"7502 Tiendas Extra, S.A. de C.",
"7503 Inmobiliaria Exmod",
"7504 Extraser, SA de CV",
"8001 Promotora e Inm.Cuyd",
"8002 Desarrollo Inm.Siglo XXI",
"8003 Espect.Costa del Pacífico",
"8004 Territorio Santos Modelo",
"8005 Santos Laguna, SA de CV",
"8006 Prom.dep.Cult.La Laguna",
"8007 Prom.dep.Cult.Zacatecas",
"8008 Prom.dep.Cult.Tuxtepec",
"8301 Control Fair Value",
"8501 Difa Arrendadora SA de CV",
"8502 Manantiales La Asunción",
"8503 Waters Parners Servic.Méx",
"9001 Jardín Cerveza, SA de CV",
"9002 Museo Mod Ciencia e Ind",
"9004 Sociedad que se borrará",
"9005 Maratón Pacífico, A.C.",
"9006 Fundación Gpo Modelo, A.C",
"9007 Extrade II, S.A. de C.V.",
"9008 Club de Base Ball Obregón",
"9009 Compañía  Cervecera  Rio Bravo"
}
    Private categoriaImpuesto() As String = {"TRANSFERIDO", "RETENIDO"}
    Private impuestoTipo() As String = {"GTS", "VAT", "LAC"}
    Public adendaXML As String = ""
    Private op As Boolean = True
    Private opAdenda As Boolean = True
    Private listaDetalles As New List(Of detalleAdendaModelo)


    Public Sub New(ByVal idVenta As Integer, ByVal moneda As String, ByVal tipoCambio As String)

        ' This call is required by the designer.
        InitializeComponent()
        adendas = New AdendaModeloDAO(MySqlcon)
        detalles = New detalleAdendaModeloDAO(MySqlcon)
        Me.idVenta = idVenta
        'adenda = New AdendaModelo()
        'Me.moneda = moneda
        'Me.tipoCambio = tipoCambio
        llenarCombos(comboEntity, tipoDocumentos)
        llenarCombos(comboMonedas, tipoMonedas)
        llenarCombos(comboIdioma, idiomas)
        'llenarCombos(comboUnit, unidadDeMedida)
        llenarCombos(comboBase, tipoArancel)
        llenarCombos(comboTaxCategory, categoriaImpuesto)
        llenarCombos(comboTaxCategory2, categoriaImpuesto)
        llenarCombos(txtTaxType, tipoArancel)
        llenarCombos(txtBuyer, catalogoSociedades)
        comboMonedas.Text = moneda
        txtRate.Text = tipoCambio
        comboEntity.SelectedIndex = 0
        'comboUnit.SelectedIndex = 0
        comboBase.SelectedIndex = 0
        comboIdioma.SelectedIndex = 0
        txtTaxType.SelectedIndex = 0
        comboTaxCategory2.SelectedIndex = 0
        comboTaxCategory.SelectedIndex = 0
        txtBuyer.SelectedIndex = 0
        txtTax2.Text = "16"
        adenda = adendas.buscarPorVenta(idVenta)
        If Not adenda Is Nothing Then
            opAdenda = False
            llenaDatos()
            dgvDetalles.DataSource = detalles.vistaDetallesGrid(adenda.id)
        Else
            adenda = adendas.crearAdenda()
            cargaDetalles()
        End If
        dgvDetalles.ReadOnly = True

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Function parsea(ByVal cadena As String) As Double
        Return Double.Parse(cadena)
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Guardar() Then
            Me.Dispose()
        End If
    End Sub
    Private Function Guardar() As Boolean
        If checarCampos() Then
            If checaDetalles() Then
                adenda.entityType = comboEntity.Text.ToString().Split("=")(0)
                adenda.idCreador = txtIdCreador.Text
                adenda.texto = txtText.Text
                adenda.idReferencia = txtReference.Text
                adenda.informacionComprador = txtBuyer.Text.ToString().Split(" ")(0)
                adenda.idCreadorAlt = txtInvoice.Text
                adenda.tipoDivisa = comboMonedas.Text
                adenda.tipoCambio = parsea(txtRate.Text)
                adenda.cantidadTotal = parsea(txtAmount.Text)
                adenda.cantidadBase2 = parsea(txtAmount2.Text)
                adenda.porcentajeTax2 = parsea(txtTax2.Text)
                adenda.categoriaTax2 = comboTaxCategory2.Text
                adenda.tipoTax = txtTaxType.Text.ToString().Split("=")(0)
                adenda.cantidadFinal = parsea(txtAmount3.Text)
                adenda.idVenta = idVenta
                adenda.cantidadTax2 = parsea(txtTaxAmount2.Text)
                Try

                    If opAdenda Then
                        adendas.actualizar(adenda)
                        'Dim i = adendas.ultimoId()
                        'guardarDetalles(i)
                        adendaXML = adenda.crearXml()
                        'crearXml()
                        'MsgBox("Adenda creada y guardada", MsgBoxStyle.Information, GlobalNombreApp)
                        PopUp("Guardado", 30)
                        Return True
                    Else
                        adendas.actualizar(adenda)
                        adendaXML = adenda.crearXml()
                        'crearXml()
                        'MsgBox("Adenda modificada y guardada", MsgBoxStyle.Information, GlobalNombreApp)
                        PopUp("Guardado", 30)
                        Return True
                    End If
                Catch ex As Exception
                    MsgBox(ex.ToString(), MsgBoxStyle.Information, GlobalNombreApp)
                    Return False
                End Try
            Else
                MsgBox("Debe completar los datos de los detalles.", MsgBoxStyle.Information, GlobalNombreApp)
                Return False
            End If
        Else
            MsgBox("Debe llenar todos los campos.", MsgBoxStyle.Information, GlobalNombreApp)
            Return False
        End If
    End Function

    Private Sub llenaDatos()
        If Not adenda Is Nothing Then
            comboEntity.SelectedIndex = buscaIndex(adenda.entityType, comboEntity)
            txtIdCreador.Text = adenda.idCreador
            txtText.Text = adenda.texto
            txtReference.Text = adenda.idReferencia
            txtBuyer.SelectedIndex = buscaIndex(adenda.informacionComprador, txtBuyer)
            txtInvoice.Text = adenda.idCreadorAlt
            comboMonedas.SelectedIndex = buscaIndex(adenda.tipoDivisa, comboMonedas)
            txtRate.Text = adenda.tipoCambio
            txtAmount.Text = adenda.cantidadTotal
            txtAmount2.Text = adenda.cantidadBase2
            txtTax2.Text = adenda.porcentajeTax2
            comboTaxCategory2.SelectedIndex = buscaIndex(adenda.categoriaTax2, comboTaxCategory2)
            txtTaxType.SelectedIndex = buscaIndex(adenda.tipoTax, txtTaxType)
            txtAmount3.Text = adenda.cantidadFinal
            txtTaxAmount2.Text = adenda.cantidadTax2
            idVenta = adenda.idVenta
        Else
            MsgBox("La adenda no existe", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub llenarCombos(ByRef combo As ComboBox, ByVal datos() As String)
        For i As Integer = 0 To datos.Length - 1
            combo.Items.Add(datos(i))
        Next
    End Sub

    Private Function checarCampos() As Boolean
        Dim res As Boolean = True
        If comboEntity.Text = "" Then
            comboEntity.BackColor = Color.Red
            res = False
        Else
            comboEntity.BackColor = Color.White
        End If
        If txtIdCreador.Text = "" Then
            txtIdCreador.BackColor = Color.Red
            res = False
        Else
            txtIdCreador.BackColor = Color.White
        End If
        If txtText.Text = "" Then
            txtText.BackColor = Color.Red
            res = False
        Else
            txtText.BackColor = Color.White
        End If
        If txtReference.Text = "" Then
            txtReference.BackColor = Color.Red
            res = False
        Else
            txtReference.BackColor = Color.White
        End If
        If txtBuyer.Text = "" Then
            txtBuyer.BackColor = Color.Red
            res = False
        Else
            txtBuyer.BackColor = Color.White
        End If
        If txtInvoice.Text = "" Then
            txtInvoice.BackColor = Color.Red
            res = False
        Else
            txtInvoice.BackColor = Color.White
        End If
        If comboMonedas.Text = "" Then
            comboMonedas.BackColor = Color.Red
            res = False
        Else
            comboMonedas.BackColor = Color.White
        End If
        If txtRate.Text = "" Then
            txtRate.BackColor = Color.Red
            res = False
        Else
            txtRate.BackColor = Color.White
        End If

        If txtAmount.Text = "" Then
            txtAmount.BackColor = Color.Red
            res = False
        Else
            txtAmount.BackColor = Color.White
        End If
        If txtAmount2.Text = "" Then
            txtAmount2.BackColor = Color.Red
            res = False
        Else
            txtAmount2.BackColor = Color.White
        End If
        If txtTax2.Text = "" Then
            txtTax2.BackColor = Color.Red
            res = False
        Else
            txtTax2.BackColor = Color.White
        End If
        If txtTaxAmount2.Text = "" Then
            txtTaxAmount2.BackColor = Color.Red
            res = False
        Else
            txtTaxAmount2.BackColor = Color.White
        End If
        If comboTaxCategory2.Text = "" Then
            comboTaxCategory2.BackColor = Color.Red
            res = False
        Else
            comboTaxCategory2.BackColor = Color.White
        End If
        If txtTaxType.Text = "" Then
            txtTaxType.BackColor = Color.Red
            res = False
        Else
            txtTaxType.BackColor = Color.White
        End If
        If txtAmount3.Text = "" Then
            txtAmount3.BackColor = Color.Red
            res = False
        Else
            txtAmount3.BackColor = Color.White
        End If

        Return res
    End Function

    Private Function checaCamposDetalle() As Boolean
        Dim res As Boolean = True
        If txtGTIN.Text = "" Then
            txtGTIN.BackColor = Color.Red
            res = False
        Else
            txtGTIN.BackColor = Color.White
        End If
        If txtIdAlt.Text = "" Then
            txtIdAlt.BackColor = Color.Red
            res = False
        Else
            txtIdAlt.BackColor = Color.White
        End If
        If txtTextoLargo.Text = "" Then
            txtTextoLargo.BackColor = Color.Red
            res = False
        Else
            txtTextoLargo.BackColor = Color.White
        End If
        If comboIdioma.Text = "" Then
            comboIdioma.BackColor = Color.Red
            res = False
        Else
            comboIdioma.BackColor = Color.White
        End If
        If txtInvoiced.Text = "" Then
            txtInvoiced.BackColor = Color.Red
            res = False
        Else
            txtInvoiced.BackColor = Color.White
        End If
        If comboUnit.Text = "" Then
            comboUnit.BackColor = Color.Red
            res = False
        Else
            comboUnit.BackColor = Color.White
        End If
        If txtPrecioBruto.Text = "" Then
            txtPrecioBruto.BackColor = Color.Red
            res = False
        Else
            txtPrecioBruto.BackColor = Color.White
        End If
        If txtPrecioNeto.Text = "" Then
            txtPrecioNeto.BackColor = Color.Red
            res = False
        Else
            txtPrecioNeto.BackColor = Color.White
        End If
        If txtNumOrden.Text = "" Then
            txtNumOrden.BackColor = Color.Red
            res = False
        Else
            txtNumOrden.BackColor = Color.White
        End If
        If comboBase.Text = "" Then
            comboBase.BackColor = Color.Red
            res = False
        Else
            comboBase.BackColor = Color.White
        End If
        'If txtNumReferencia.Text = "" Then
        '    txtNumReferencia.BackColor = Color.Red
        '    res = False
        'Else
        '    txtNumReferencia.BackColor = Color.White
        'End If
        If txtTax.Text = "" Then
            txtTax.BackColor = Color.Red
            res = False
        Else
            txtTax.BackColor = Color.White
        End If
        If txtTaxAmount.Text = "" Then
            txtTaxAmount.BackColor = Color.Red
            res = False
        Else
            txtTaxAmount.BackColor = Color.White
        End If
        If comboTaxCategory.Text = "" Then
            comboTaxCategory.BackColor = Color.Red
            res = False
        Else
            comboTaxCategory.BackColor = Color.White
        End If
        If txtGrossAmount.Text = "" Then
            txtGrossAmount.BackColor = Color.Red
            res = False
        Else
            txtGrossAmount.BackColor = Color.White
        End If
        If txtNetAmount.Text = "" Then
            txtNetAmount.BackColor = Color.Red
            res = False
        Else
            txtNetAmount.BackColor = Color.White
        End If
        If txtReference2.Text = "" Then
            txtReference2.BackColor = Color.Red
            res = False
        Else
            txtReference2.BackColor = Color.White
        End If
        Return res
    End Function



    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        ' If op Then
        'if checaCamposDetalle() then
        'agregarDetalle()
        'End if
        'Else
        If checaCamposDetalle() Then
            modificarDetalle()
        End If
        ' End If
    End Sub
    Private Sub limpiaCampos()
        txtNumOrden.Text = ""
        txtGTIN.Text = ""
        txtIdAlt.Text = ""
        comboIdioma.Text = ""
        txtInvoiced.Text = ""
        comboUnit.Text = ""
        txtPrecioBruto.Text = ""
        txtPrecioNeto.Text = ""
        txtTextoLargo.Text = ""
        comboBase.SelectedIndex = 0
        txtNumReferencia.Text = ""
        txtTax.Text = ""
        txtTaxAmount.Text = ""
        comboTaxCategory.SelectedIndex = 0
        txtGrossAmount.Text = ""
        txtNetAmount.Text = ""
    End Sub
    Private Sub configuraGrid()
        dgvDetalles.Columns(0).Visible = False
        dgvDetalles.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Sub muestraDetalle(ByRef detalle As detalleAdendaModelo)
        If Not detalle Is Nothing Then
            txtNumOrden.Text = detalle.posicionPedido
            txtGTIN.Text = detalle.codigoEAN
            txtIdAlt.Text = detalle.numProveedor
            txtTextoLargo.Text = detalle.descripcion
            txtInvoiced.Text = detalle.cantidadProductosFacturada
            comboUnit.Text = detalle.unidadMedida
            comboIdioma.SelectedIndex = buscaIndex(detalle.idioma, comboIdioma)
            comboBase.SelectedIndex = buscaIndex(detalle.tipoArancel, comboBase)
            comboTaxCategory.SelectedIndex = buscaIndex(detalle.identificacionImpuesto, comboTaxCategory)
            txtPrecioBruto.Text = Format(detalle.precioBruto, "0.00")
            txtPrecioNeto.Text = Format(detalle.precioNeto, "0.00")
            txtTax.Text = detalle.porcentajeImpuesto
            txtTaxAmount.Text = Format(detalle.montoImpuesto, "0.00")
            txtGrossAmount.Text = Format(detalle.precioBrutoArticulos, "0.00")
            txtNetAmount.Text = Format(detalle.precioNetoArticulos, "0.00")
            txtNumReferencia.Text = detalle.numIdImpuesto
            txtReference2.Text = detalle.numReferenciaAdicional
        End If
    End Sub
    Private Sub agregarDetalle()
        detalle = New detalleAdendaModelo
        detalle.idAdenda = adenda.id
        detalle.posicionPedido = Integer.Parse(txtNumOrden.Text)
        detalle.codigoEAN = txtGTIN.Text
        detalle.numProveedor = txtIdAlt.Text
        detalle.idioma = comboIdioma.Text
        detalle.cantidadProductosFacturada = Double.Parse(txtInvoiced.Text)
        detalle.unidadMedida = comboUnit.Text
        detalle.precioBruto = Double.Parse(txtPrecioBruto.Text)
        detalle.precioNeto = Double.Parse(txtPrecioNeto.Text)
        detalle.descripcion = txtTextoLargo.Text
        detalle.numReferenciaAdicional = txtReference2.Text
        detalle.tipoArancel = comboBase.Text.ToString().Split("=")(0)
        detalle.numIdImpuesto = txtNumReferencia.Text
        detalle.porcentajeImpuesto = parsea(txtTax.Text)
        detalle.montoImpuesto = parsea(txtTaxAmount.Text)
        detalle.identificacionImpuesto = comboTaxCategory.Text
        detalle.precioBrutoArticulos = parsea(txtGrossAmount.Text)
        detalle.precioNetoArticulos = parsea(txtNetAmount.Text)
        'listaDetalles.Add(detalle)
        detalles.agregar(detalle)
        limpiaCampos()
        dgvDetalles.DataSource = detalles.vistaDetalles(adenda.id)
        'dgvDetalles.DataSource = listaDetalles
        configuraGrid()
    End Sub
    Private Sub modificarDetalle()
        detalle = New detalleAdendaModelo
        detalle.idAdenda = adenda.id
        detalle.id = Integer.Parse(dgvDetalles.CurrentRow.Cells(0).Value)
        detalle.posicionPedido = Integer.Parse(txtNumOrden.Text)
        detalle.codigoEAN = txtGTIN.Text
        detalle.numProveedor = txtIdAlt.Text
        detalle.idioma = comboIdioma.Text
        detalle.cantidadProductosFacturada = Double.Parse(txtInvoiced.Text)
        detalle.unidadMedida = comboUnit.Text
        detalle.precioBruto = Double.Parse(txtPrecioBruto.Text)
        detalle.precioNeto = Double.Parse(txtPrecioNeto.Text)
        detalle.descripcion = txtTextoLargo.Text
        detalle.numReferenciaAdicional = txtReference2.Text
        detalle.tipoArancel = comboBase.Text.ToString().Split("=")(0)
        detalle.numIdImpuesto = txtNumReferencia.Text
        detalle.porcentajeImpuesto = parsea(txtTax.Text)
        detalle.montoImpuesto = parsea(txtTaxAmount.Text)
        detalle.identificacionImpuesto = comboTaxCategory.Text
        detalle.precioBrutoArticulos = parsea(txtGrossAmount.Text)
        detalle.precioNetoArticulos = parsea(txtNetAmount.Text)
        'Dim d As New detalleAdendaModelo(Integer.Parse(dgvDetalles.CurrentRow.Cells(0).Value))
        ' Dim i As Integer = listaDetalles.IndexOf(detalle)
        'listaDetalles.Item(i) = detalle
        detalles.actualizar(detalle)
        limpiaCampos()
        actualizaGrid()
        'btnAgregar.Text = "Agregar"
        op = True
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs)
        Dim i As Integer = Integer.Parse(dgvDetalles.CurrentRow.Cells(0).Value)
        detalles.eliminar(i)
        dgvDetalles.DataSource = detalles.vistaDetalles(adenda.id)
    End Sub

    Private Sub txtRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRate.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtTax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTax.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtTaxAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTaxAmount.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtGrossAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGrossAmount.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtNetAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNetAmount.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtAmount2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount2.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtTax2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTax2.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtTaxAmount2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTaxAmount2.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtAmount3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount3.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtNumOrden_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNumOrden.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtInvoiced_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInvoiced.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtPrecioBruto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrecioBruto.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtPrecioNeto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrecioNeto.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Char.IsPunctuation(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub guardarDetalles(ByVal i As Integer)
        If Not listaDetalles Is Nothing Then
            For Each d As detalleAdendaModelo In listaDetalles
                d.idAdenda = i
                detalles.agregar(d)
            Next
        End If
    End Sub

    Private Sub cargaDetalles()
        Dim d As New dbVentasInventario(MySqlcon)
        Dim dr As MySqlDataReader = d.ConsultaReader(idVenta, False, "1", 1, "0", False)
        Dim lista As New List(Of detalleAdendaModelo)
        Dim detalle As detalleAdendaModelo
        Dim bruto As Double
        Dim total As Double = 0
        Dim subTotalBruto As Double = 0
        Dim totalImpuestos = 0
        If dr.HasRows Then
            While dr.Read()
                detalle = New detalleAdendaModelo()
                detalle.codigoEAN = dr("clave2")
                detalle.descripcion = dr("descripcion")
                detalle.numProveedor = dr("clave")
                bruto = dr("precio")
                detalle.cantidadProductosFacturada = dr("cantidad")
                detalle.precioBruto = bruto / detalle.cantidadProductosFacturada
                detalle.porcentajeImpuesto = dr("iva")
                detalle.unidadMedida = dr("tipom")
                detalle.precioNeto = (detalle.precioBruto + (detalle.precioBruto * (detalle.porcentajeImpuesto) / 100))
                detalle.precioBrutoArticulos = detalle.precioBruto * detalle.cantidadProductosFacturada
                detalle.precioNetoArticulos = detalle.precioNeto * detalle.cantidadProductosFacturada
                detalle.montoImpuesto = (detalle.precioBruto * (detalle.porcentajeImpuesto / 100)) * detalle.cantidadProductosFacturada
                lista.Add(detalle)
            End While
            dr.Close()
            Dim ventas As New dbVentas(idVenta, MySqlcon, "")
            Dim folio = ventas.Serie + ventas.Folio.ToString()
            txtIdCreador.Text = folio

            For Each de As detalleAdendaModelo In lista
                de.idAdenda = adenda.id
                de.numReferenciaAdicional = folio
                If de.descripcion.Length > 35 Then
                    de.descripcion = recortaCadena(de.descripcion, 35)
                End If
                detalles.agregar(de)

            Next
            ventas.DaTotal(idVenta, ventas.IdConversion, "0", "0")
            txtAmount2.Text = Format(ventas.Subtototal, "0.000")
            txtAmount3.Text = Format(ventas.TotalVenta, "0.00")
            txtTaxAmount2.Text = Format(ventas.TotalIva, "0.00")
            txtAmount.Text = Format(ventas.Subtototal, "0.00")
            dgvDetalles.DataSource = detalles.vistaDetallesGrid(adenda.id)
            configuraGrid()
        End If
    End Sub

    Private Sub dgvDetalles_Click(sender As Object, e As EventArgs) Handles dgvDetalles.Click
        op = False
        Dim i As Integer = Integer.Parse(dgvDetalles.CurrentRow.Cells(0).Value)
        Dim d As detalleAdendaModelo = detalles.buscar(i)
        muestraDetalle(d)
        btnAgregar.Text = "guardar cambio"
    End Sub

    Private Sub actualizaGrid()
        dgvDetalles.DataSource = detalles.vistaDetallesGrid(adenda.id)
        configuraGrid()
    End Sub

    Private Function buscaIndex(ByVal cadena As String, ByRef combo As ComboBox) As Integer
        If cadena = "" Then
            Return 0
        End If
        For i As Integer = 0 To combo.Items.Count - 1
            If combo.Items(i).ToString().Contains(cadena) Then
                Return i
            End If
        Next
        Return 1
    End Function

    Public Sub crearXml()
        Dim sRenglon As String = Nothing
        Dim strStreamW As Stream = Nothing
        Dim strStreamWriter As StreamWriter = Nothing
        Dim ContenidoArchivo As String = Nothing
        ' Donde guardamos los paths de los archivos que vamos a estar utilizando ..
        Dim PathArchivo As String


        'Dim i As Integer

        Try

            If Directory.Exists("C:\Capeta") = False Then ' si no existe la carpeta se crea
                Directory.CreateDirectory("C:\carpeta")
            End If

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            PathArchivo = "C:\carpeta\Archivo" & Format(Today.Date, "ddMMyyyy") & ".xml" ' Se determina el nombre del archivo con la fecha actual

            'verificamos si existe el archivo

            If File.Exists(PathArchivo) Then
                strStreamW = File.Open(PathArchivo, FileMode.Open) 'Abrimos el archivo
            Else
                strStreamW = File.Create(PathArchivo) ' lo creamos
            End If

            strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default) ' tipo de codificacion para escritura


            'escribimos en el archivo

            strStreamWriter.WriteLine(adendaXML)


            strStreamWriter.Close() ' cerramos

        Catch ex As Exception
            MsgBox("Error al Guardar la ingormacion en el archivo. " & ex.ToString, MsgBoxStyle.Critical, Application.ProductName)
            strStreamWriter.Close() ' cerramos
        End Try
    End Sub

    Private Function recortaCadena(ByVal cadena As String, ByVal tamanho As Integer) As String
        Dim aux(35) As Char
        For i As Integer = 0 To tamanho - 1
            aux(i) = cadena(i)
        Next
        Dim res As New String(aux)
        Return res
    End Function

    Public Function checaDetalles() As Boolean
        Dim res = True
        Dim lista As List(Of detalleAdendaModelo) = detalles.listaDetalles(adenda.id)
        For Each d As detalleAdendaModelo In lista
            If d.idioma = "" Then
                res = False
            End If
        Next
        Return res
    End Function


    Private Sub frmAdendaGrupoModelo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim opc As DialogResult = MsgBox("¿Desea guardar los cambion en la addenda?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Salir")
        If opc = Windows.Forms.DialogResult.Yes Then
            If Guardar() Then
                Me.Dispose()
            Else
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
    End Sub

   
End Class