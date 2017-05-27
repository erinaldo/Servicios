Public Class dbOpciones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public SerieElectronica As String
    Public RutaXMLEgresos As String
    Public Imp As Double
    Public _EncabezadoEmail As String
    Public _PiedepaginaEmail As String
    Public Email As String
    Public EmailUsuario As String
    Public EmailHost As String
    Public EmailPass As String
    Public EmailPuerto As String
    Public EmailSSL As Byte
    Public _SerieElectronica As String
    Public _ConsultaRealTime As Byte
    Public _IdMoneda As Integer
    Public _idAlmacen As Integer
    Public _TipoCosteo As Byte
    Public _ModoFoliosB As String
    Public _noCertificado As String
    Public _Conector As Byte
    Public _yearAprobacion As String
    Public _FolioInicial As Integer
    Public _FolioFinal As Integer

    Public _RFC As String
    Public _NombreEmpresa As String
    Public _Calle As String
    Public _ActivarPDF As String
    Public _MostrarPDF As String
    Public _Sinnegativos As String
    Public _ConectorMunrec As String
    Public _OrdenUbicacion As String
    Public _IgualarFechaTimbrado As Integer
    Public _CalculoAlterno As String
    Public _DetalleKits As String
    Public _TimbradoPruebas As String

    Public _ServiciowebSwitch As String
    Public _TipoSelAlmacen As String
    Public _ApartadosInventariable As String
    Public _ConectorEnviarCorreos As String
    Public _AvisoCosto As String
    Public _CursorVentas As String
    Public _MetodoUtilidad As String
    Public _VersionConector As String
    Public _NoRutas As String
    Public _ApiKey As String
    Public _CodigoPostalLocal As String
    Public _TipoFacturacion As Byte
    Public _DireccionTimbrado As String
    Public _PacCFDI As Byte
    Public _UsuarioFacCom As String
    Public _passFacCom As String
    Public _formatocantidad As String
    Public _formatoPrecioU As String
    Public _formatoImporte As String
    Public _formatoSubtotal As String
    Public _formatoTotal As String
    Public _formatoIva As String
    Public _NCDosPasos As Byte
    Public _FechaPunto2 As String
    Public _AgregaSeriesAVenta As Byte
    Public _LimitarCredito As Byte
    Public _PassCredito As String
    Public _IVaCero As Byte
    Public FechaAdd As String
    Public FechaVen As String
    Public Timbres As Integer
    Public AvisoTimbres As Integer
    Public AvisoDias As Integer
    Public CotizarSoloExistencia As Byte
    Public PreguntarImpresora As Byte
    Public EspacioCantidad As Byte
    Public EspacioPrecioUnitacio As Byte
    Public EspacioImporte As Byte
    Public EspacioSubtotal As Byte
    Public EspacioIva As Byte
    Public Espaciototal As Byte
    Public TituloFactura As String
    Public TituloParcialidad As String
    Public IdClienteDefault As Integer
    Public CierreConVentana As Byte
    Public TipoRedondeo As Byte
    Public PuertoBascula As String
    Public BasculaBaundRate As Integer
    Public BasculaParity As Integer
    Public BasculaDataBits As Integer
    Public BasculaHandshake As Integer
    Public BasculaSecuencia As String
    Public PedirPrecio As Byte
    Public DecimalesRedondeo As Byte
    Public CostoTiempoReal As Byte
    Public EliminarRefPV As Byte
    Public Serieconector As String
    Public idSucursalconector As Integer
    Public SerieNCConecto As String
    Public FormatoFechaPv As String
    Public IdiomaLetras As Byte
    Public PVRojo As Integer
    Public PVVerde As Integer
    Public PVAzul As Integer
    Public BuscaxFabricante As Byte
    Public NParcialidades As Byte
    Public BuscaModoB As Byte
    Public CorreoContenido As String
    Public MostrarPredial As Byte
    Public ClientesMayusculas As Byte
    Public ClienteBloquearCodigo As Byte
    Public TituloOriginalFactura As String
    Public TituloCopiaFactura As String
    Public TituloCopia2Factura As String
    Public ActivarCopiaFactura As Byte
    Public ActivarCopiaFacturaCredito As Byte
    Public ActivarCopia2Factura As Byte
    Public ActivarCopiaFacturaCredito2 As Byte
    Public TituloOriginalRemision As String
    Public TituloCopiaRemision As String
    Public TituloCopia2Remision As String
    Public ActivarCopiaRemision As Byte
    Public ActivarCopiaRemisionCredito As Byte
    Public ActivarCopiaRemisionCredito2 As Byte
    Public ActivarCopia2Remision As Byte
    Public NoPermitirFacturasdeCredito As Byte
    Public NoPermitirRemisionesdeCredito As Byte
    Public TipoProrrateo As Byte
    Public BusquedaporClases As Byte
    Public MaximizarVentas As Byte
    Public PagosTicket As Byte
    Public IntegrarBancosVentasPagos As Byte
    Public IntegrarBancosComprasPagos As Byte
    Public IntegrarBancosVentasContado As Byte
    Public IntegrarBancosComprasContado As Byte
    Public ClientesSinRepetir As Byte
    Public SobreEscribeImpLocales As Byte
    Public FacturarSoloExistencia As Byte
    Public TipoCostoPrecios As Byte
    Public BoletasInventario As Byte
    Public BoletasResumida As Byte
    Public SiemprePorSurtirVentas As Byte
    Public SiemprePorSurtirRemisiones As Byte
    Public Copiaflujoventas As Byte
    Public Copiaflujorem As Byte
    Public IntegrarContabilidad As Byte
    Public PVVentabaCompleta As Byte
    Public ChecaFolioFacturas As Byte
    Public RecibidoDefault As Byte
    Public NoBloquearCreardesde As Byte
    Public FacturaComoegreso As Byte
    Public VentasCorteRemTodas As Byte
    Public PVConfirmarCobrar As Byte
    Public VentasCorteRemxMetodo As Byte
    Public NoImpSinGuardar As Byte
    Public RemisionesSinDetalleCD As Byte
    Public IdInventarioCD As Integer
    Public ActNom12 As Byte
    Public FacturarPagosRemisiones As Byte
    Public PedirAnticipoRemisiones As Byte
    Public FacturarSoloaCredito As Byte
    Public RemisionesSoloaCredito As Byte
    Public VendedorUsuario As Byte
    Public NoProveedorSoriana As Integer
    Public NoProveedorWalmart As Integer
    'Public bolFacturarSoloExistencia As Boolean
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
        Dim Dreader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblopciones"
        Dreader = Comm.ExecuteReader
        If Dreader.Read Then
            Imp = Dreader("impuesto")
            _EncabezadoEmail = Dreader("encabezadoemail")
            _PiedepaginaEmail = Dreader("piedepaginaemail")
            _ConsultaRealTime = Dreader("consultarealtime")
            _IdMoneda = Dreader("idmoneda")
            _idAlmacen = Dreader("idalmacen")
            _ModoFoliosB = Dreader("noaprobacion")
            _noCertificado = Dreader("nocertificado")
            _yearAprobacion = Dreader("yearaprobacion")
            _FolioFinal = Dreader("foliofinal")
            _FolioInicial = Dreader("folioinicial")
            _SerieElectronica = Dreader("serieelectronica")
            _RFC = Dreader("rfc")
            _NombreEmpresa = Dreader("nombreempresa")
            _Calle = Dreader("calle")
            _ActivarPDF = Dreader("noexterior")
            _MostrarPDF = Dreader("nointerior")
            _Sinnegativos = Dreader("colonia")
            _ConectorMunrec = Dreader("localidad")
            _OrdenUbicacion = Dreader("referenciadomicilio")
            '_IgualarFechaTimbrado = Dreader("municipio")
            _CalculoAlterno = Dreader("estado")
            _DetalleKits = Dreader("pais")
            _TimbradoPruebas = Dreader("codigopostal")
            _ServiciowebSwitch = Dreader("nombreempresalocal")
            _TipoSelAlmacen = Dreader("callelocal")
            _ApartadosInventariable = Dreader("noexteriorlocal")
            _ConectorEnviarCorreos = Dreader("nointeriorlocal")
            _AvisoCosto = Dreader("colonialocal")
            _CursorVentas = Dreader("localidadlocal")
            _MetodoUtilidad = Dreader("referenciadomiciliolocal")
            _VersionConector = Dreader("municipiolocal")
            _NoRutas = Dreader("estadolocal")
            _ApiKey = Dreader("paislocal")
            _CodigoPostalLocal = Dreader("codigopostallocal")
            _TipoFacturacion = Dreader("tipofacturacion")
            _DireccionTimbrado = Dreader("direcciontimbrado")
            _PacCFDI = Dreader("paccfdi")
            _UsuarioFacCom = Dreader("usuariofaccom")
            _passFacCom = Dreader("passfaccom")
            _formatocantidad = Dreader("formatocantidad")
            _formatoPrecioU = Dreader("formatopreciou")
            _formatoImporte = Dreader("formatoimporte")
            _formatoSubtotal = Dreader("formatosubtotal")
            _formatoIva = Dreader("formatoiva")
            _formatoTotal = Dreader("formatototal")
            _FechaPunto2 = Dreader("fechapunto2")
            _NCDosPasos = Dreader("ncdospasos")
            _TipoCosteo = Dreader("tipocosteo")
            _Conector = Dreader("conector")
            _AgregaSeriesAVenta = Dreader("agregaseriesventa")
            _LimitarCredito = Dreader("limitarcredito")
            _PassCredito = Dreader("passcredito")
            _IVaCero = Dreader("ivacero")
            FechaAdd = Dreader("fechaadd")
            FechaVen = Dreader("fechaven")
            Timbres = Dreader("timbres")
            AvisoTimbres = Dreader("avisotimbres")
            AvisoDias = Dreader("avisodias")
            CotizarSoloExistencia = Dreader("cotizarexistencia")
            PreguntarImpresora = Dreader("preguntarimpresora")
            EspacioCantidad = Dreader("espaciocantidad")
            EspacioImporte = Dreader("espacioimporte")
            EspacioIva = Dreader("espacioiva")
            EspacioPrecioUnitacio = Dreader("espaciopreciounitario")
            EspacioSubtotal = Dreader("espaciosubtotal")
            Espaciototal = Dreader("espaciototal")
            TituloFactura = Dreader("titulofactura")
            TituloParcialidad = Dreader("tituloparcialidad")
            CierreConVentana = Dreader("cierreconventana")
            TipoRedondeo = Dreader("tiporedondeo")
            IdClienteDefault = Dreader("idclientedefault")
            PuertoBascula = Dreader("puertobascula")
            BasculaBaundRate = Dreader("basculabaudrate")
            BasculaParity = Dreader("basculaparity")
            BasculaDataBits = Dreader("basculadatabits")
            BasculaHandshake = Dreader("basculahandshake")
            BasculaSecuencia = Dreader("basculasecuencia")
            PedirPrecio = Dreader("pedirprecio")
            DecimalesRedondeo = Dreader("decimalesredondeo")
            CostoTiempoReal = Dreader("costeotiemporeal")
            EliminarRefPV = Dreader("eliminarrefpv")
            Serieconector = Dreader("serieconector")
            idSucursalconector = Dreader("idsucursalconector")
            SerieNCConecto = Dreader("seriencconector")
            FormatoFechaPv = Dreader("formatofechapv")
            IdiomaLetras = Dreader("idiomacletras")
            PVRojo = Dreader("pvrojo")
            PVVerde = Dreader("pvverde")
            PVAzul = Dreader("pvazul")
            BuscaxFabricante = Dreader("buscaxfabricante")
            NParcialidades = Dreader("nparcialidades")
            BuscaModoB = Dreader("buscamodob")
            Email = Dreader("email")
            EmailUsuario = Dreader("emailusuario")
            EmailHost = Dreader("emailhost")
            EmailPass = Dreader("emailpass")
            EmailPuerto = Dreader("emailpuerto")
            EmailSSL = Dreader("emailssl")
            CorreoContenido = Dreader("correocontenido")
            MostrarPredial = Dreader("mostrarpredial")
            ClienteBloquearCodigo = Dreader("clientesbloqcod")
            ClientesMayusculas = Dreader("clientesmayus")
            _IgualarFechaTimbrado = Dreader("igualarfechas")
            TituloOriginalFactura = Dreader("titulooriginalfactura")
            TituloCopiaFactura = Dreader("titulocopiafactura")
            TituloCopia2Factura = Dreader("titulocopia2factura")
            ActivarCopiaFactura = Dreader("activarcopiafactura")
            ActivarCopia2Factura = Dreader("activarcopia2factura")
            TituloOriginalRemision = Dreader("titulooriginalremision")
            TituloCopiaRemision = Dreader("titulocopiaremision")
            TituloCopia2Remision = Dreader("titulocopia2remision")
            ActivarCopiaRemision = Dreader("activarcopiaremision")
            ActivarCopia2Remision = Dreader("activarcopia2remision")
            NoPermitirFacturasdeCredito = Dreader("nopermitirventacredito")
            NoPermitirRemisionesdeCredito = Dreader("nopermitirremisioncredito")
            TipoProrrateo = Dreader("tipoprorrateo")
            BusquedaporClases = Dreader("busquedaporclases")
            MaximizarVentas = Dreader("maximizarventas")
            ActivarCopiaFacturaCredito2 = Dreader("activarcopiafacturacre2")
            ActivarCopiaFacturaCredito = Dreader("activarcopiafacturacre")
            ActivarCopiaRemisionCredito = Dreader("activarcopiaremisioncre")
            ActivarCopiaRemisionCredito2 = Dreader("activarcopiaremisioncre2")
            PagosTicket = Dreader("pagosticket")
            IntegrarBancosVentasPagos = Dreader("integrarbancos")
            ClientesSinRepetir = Dreader("clientessinrepetir")
            SobreEscribeImpLocales = Dreader("sobreescribeimploc")
            FacturarSoloExistencia = Dreader("facturarsoloexistencia")
            TipoCostoPrecios = Dreader("preciosultimocosto")
            BoletasInventario = Dreader("boletasinventario")
            BoletasResumida = Dreader("boletasresumida")
            SiemprePorSurtirRemisiones = Dreader("siempreporsurtirr")
            SiemprePorSurtirVentas = Dreader("siempreporsurtir")
            Copiaflujoventas = Dreader("copiaflujoventas")
            Copiaflujorem = Dreader("copiaflujorem")
            IntegrarContabilidad = Dreader("integrarcont")
            PVVentabaCompleta = Dreader("pvventanacomp")
        Else
            Imp = 0
        End If
        Dreader.Close()
        Comm.CommandText = "select * from tblopciones2"
        Dreader = Comm.ExecuteReader
        If Dreader.Read Then
            ChecaFolioFacturas = Dreader("checafoliofacturas")
            RecibidoDefault = Dreader("surtidodefault")
            NoBloquearCreardesde = Dreader("nobloquearcd")
            VentasCorteRemTodas = Dreader("ventascorteremtodas")
            PVConfirmarCobrar = Dreader("pvconfirmarcobrar")
            VentasCorteRemxMetodo = Dreader("ventascorteremxm")
            NoImpSinGuardar = Dreader("noimpsinguardar")
            RemisionesSinDetalleCD = Dreader("remsindetallecd")
            IdInventarioCD = Dreader("idinventariocd")
            IntegrarBancosComprasContado = Dreader("integrarbancoscc")
            IntegrarBancosComprasPagos = Dreader("integrarbancoscp")
            IntegrarBancosVentasContado = Dreader("integrarbancosvp")
            ActNom12 = Dreader("acteneunodos")
            FacturarPagosRemisiones = Dreader("facturarpagosrem")
            PedirAnticipoRemisiones = Dreader("pediranticiporem")
            FacturarSoloaCredito = Dreader("facturarsolocredito")
            RemisionesSoloaCredito = Dreader("remisionessolocredito")
            NoProveedorSoriana = Dreader("noproveedorsoriana")
            NoProveedorWalmart = Dreader("noproveedorwalmart")
            VendedorUsuario = Dreader("vendedorusuario")
        End If
        Dreader.Close()
    End Sub
    Public Sub GuardaCorreo()
        Comm.CommandText = "update tblopciones set email='" + Replace(Email, "'", "''") + "',emailusuario='" + Replace(EmailUsuario, "'", "''") + "',emailhost='" + Replace(EmailHost, "'", "''") + "',emailpass='" + Replace(EmailPass, "'", "''") + "',emailpuerto='" + Replace(EmailPuerto, "'", "''") + "',emailssl=" + EmailSSL.ToString + ",correocontenido='" + Replace(CorreoContenido, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ",fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "'"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub GuardaOpciones()
        Dim Str As String
        Str = "update tblopciones set "
        Str += "impuesto=" + Imp.ToString
        Str += ",encabezadoemail='" + Replace(_EncabezadoEmail, "'", "''") + "'"
        Str += ",consultarealtime=" + _ConsultaRealTime.ToString
        Str += ",piedepaginaemail='" + Replace(_PiedepaginaEmail, "'", "''") + "'"
        Str += ",idmoneda=" + _IdMoneda.ToString
        Str += ",idalmacen=" + _idAlmacen.ToString
        Str += ",noaprobacion='" + Replace(_ModoFoliosB, "'", "''") + "'"
        Str += ",nocertificado='" + Replace(_noCertificado, "'", "''") + "'"
        Str += ",yearaprobacion='" + Replace(_yearAprobacion, "'", "''") + "'"
        Str += ",foliofinal='" + Replace(_FolioFinal, "'", "''") + "'"
        Str += ",folioinicial='" + Replace(_FolioInicial, "'", "''") + "'"
        Str += ",serieelectronica='" + Replace(_SerieElectronica, "'", "''") + "'"
        Str += ",rfc='" + Replace(_RFC, "'", "''") + "'"
        Str += ",nombreempresa='" + Replace(_NombreEmpresa, "'", "''") + "'"
        Str += ",calle='" + Replace(_Calle, "'", "''") + "'"
        Str += ",noexterior='" + Replace(_ActivarPDF, "'", "''") + "'"
        Str += ",nointerior='" + Replace(_MostrarPDF, "'", "''") + "'"
        Str += ",colonia='" + Replace(_Sinnegativos, "'", "''") + "'"
        Str += ",localidad='" + Replace(_ConectorMunrec, "'", "''") + "'"
        Str += ",referenciadomicilio='" + Replace(_OrdenUbicacion, "'", "''") + "'"
        Str += ",igualarfechas=" + _IgualarFechaTimbrado.ToString
        Str += ",estado='" + Replace(_CalculoAlterno, "'", "''") + "'"
        Str += ",pais='" + Replace(_DetalleKits, "'", "''") + "'"
        Str += ",codigopostal='" + Replace(_TimbradoPruebas, "'", "''") + "'"
        Str += ",nombreempresalocal='" + Replace(_ServiciowebSwitch, "'", "''") + "'"
        Str += ",callelocal='" + Replace(_TipoSelAlmacen, "'", "''") + "'"
        Str += ",noexteriorlocal='" + Replace(_ApartadosInventariable, "'", "''") + "'"
        Str += ",nointeriorlocal='" + Replace(_ConectorEnviarCorreos, "'", "''") + "'"
        Str += ",colonialocal='" + Replace(_AvisoCosto, "'", "''") + "'"
        Str += ",localidadlocal='" + Replace(_CursorVentas, "'", "''") + "'"
        Str += ",referenciadomiciliolocal='" + Replace(_MetodoUtilidad, "'", "''") + "'"
        Str += ",municipiolocal='" + Replace(_VersionConector, "'", "''") + "'"
        Str += ",estadolocal='" + Replace(_NoRutas, "'", "''") + "'"
        Str += ",paislocal='" + Replace(_ApiKey, "'", "''") + "'"
        Str += ",codigopostallocal='" + Replace(_CodigoPostalLocal, "'", "''") + "'"
        Str += ",tipofacturacion=" + _TipoFacturacion.ToString
        Str += ",direcciontimbrado='" + Replace(_DireccionTimbrado, "'", "''") + "'"
        Str += ",paccfdi=" + _PacCFDI.ToString
        Str += ",usuariofaccom='" + Replace(Trim(_UsuarioFacCom), "'", "''") + "'"
        Str += ",passfaccom='" + Replace(Trim(_passFacCom), "'", "''") + "'"
        Str += ",formatocantidad='" + Replace(Trim(_formatocantidad), "'", "''") + "'"
        Str += ",formatopreciou='" + Replace(Trim(_formatoPrecioU), "'", "''") + "'"
        Str += ",formatoimporte='" + Replace(Trim(_formatoImporte), "'", "''") + "'"
        Str += ",formatosubtotal='" + Replace(Trim(_formatoSubtotal), "'", "''") + "'"
        Str += ",formatoiva='" + Replace(Trim(_formatoIva), "'", "''") + "'"
        Str += ",formatototal='" + Replace(Trim(_formatoTotal), "'", "''") + "'"
        Str += ",ncdospasos=" + _NCDosPasos.ToString
        Str += ",tipocosteo=" + _TipoCosteo.ToString
        Str += ",agregaseriesventa=" + _AgregaSeriesAVenta.ToString
        Str += ",limitarcredito=" + _LimitarCredito.ToString
        Str += ",passcredito='" + Replace(_PassCredito, "'", "''") + "'"
        Str += ",ivacero=" + _IVaCero.ToString
        Str += ",fechaadd='" + FechaAdd + "'"
        Str += ",fechaven='" + FechaVen + "'"
        Str += ",timbres=" + Timbres.ToString
        Str += ",avisotimbres=" + AvisoTimbres.ToString
        Str += ",avisodias=" + AvisoDias.ToString
        Str += ",cotizarexistencia=" + CotizarSoloExistencia.ToString
        Str += ",preguntarimpresora=" + PreguntarImpresora.ToString
        Str += ",espaciocantidad=" + EspacioCantidad.ToString
        Str += ",espacioimporte=" + EspacioImporte.ToString
        Str += ",espacioiva=" + EspacioIva.ToString
        Str += ",espaciopreciounitario=" + EspacioPrecioUnitacio.ToString
        Str += ",espaciosubtotal=" + EspacioSubtotal.ToString
        Str += ",espaciototal=" + Espaciototal.ToString
        Str += ",titulofactura='" + Replace(TituloFactura, "'", "''") + "'"
        Str += ",tituloparcialidad='" + Replace(TituloParcialidad, "'", "''") + "'"
        Str += ",idclientedefault=" + IdClienteDefault.ToString
        Str += ",tiporedondeo=" + TipoRedondeo.ToString
        Str += ",cierreconventana=" + Espaciototal.ToString
        Str += ",puertobascula='" + Replace(PuertoBascula, "'", "''") + "'"
        Str += ",basculabaudrate=" + BasculaBaundRate.ToString
        Str += ",basculaparity=" + BasculaParity.ToString
        Str += ",basculadatabits=" + BasculaDataBits.ToString
        Str += ",basculahandshake=" + BasculaHandshake.ToString
        Str += ",basculasecuencia='" + Replace(BasculaSecuencia, "'", "''") + "'"
        Str += ",pedirprecio=" + PedirPrecio.ToString
        Str += ",decimalesredondeo=" + DecimalesRedondeo.ToString
        Str += ",costeotiemporeal=" + CostoTiempoReal.ToString
        Str += ",eliminarrefpv=" + EliminarRefPV.ToString
        Str += ",serieconector='" + Replace(Serieconector, "'", "''") + "'"
        Str += ",idsucursalconector=" + idSucursalconector.ToString
        Str += ",seriencconector='" + Replace(SerieNCConecto, "'", "''") + "'"
        Str += ",formatofechapv='" + Replace(FormatoFechaPv, "'", "''") + "'"
        Str += ",pvrojo=" + PVRojo.ToString
        Str += ",pvverde=" + PVVerde.ToString
        Str += ",pvazul=" + PVAzul.ToString
        Str += ",buscaxfabricante=" + BuscaxFabricante.ToString
        Str += ",nparcialidades=" + NParcialidades.ToString
        Str += ",buscamodob=" + BuscaModoB.ToString
        Str += ",mostrarpredial=" + MostrarPredial.ToString
        Str += ",clientesbloqcod=" + ClienteBloquearCodigo.ToString
        Str += ",clientesmayus=" + ClientesMayusculas.ToString
        Str += ",titulooriginalfactura='" + Replace(TituloOriginalFactura, "'", "''") + "'"
        Str += ",titulocopiafactura='" + Replace(TituloCopiaFactura, "'", "''") + "'"
        Str += ",titulocopia2factura='" + Replace(TituloCopia2Factura, "'", "''") + "'"
        Str += ",activarcopiafactura=" + ActivarCopiaFactura.ToString
        Str += ",activarcopia2factura=" + ActivarCopia2Factura.ToString
        Str += ",titulooriginalremision='" + Replace(TituloOriginalRemision, "'", "''") + "'"
        Str += ",titulocopiaremision='" + Replace(TituloCopiaRemision, "'", "''") + "'"
        Str += ",titulocopia2remision='" + Replace(TituloCopia2Remision, "'", "''") + "'"
        Str += ",activarcopiaremision=" + ActivarCopiaRemision.ToString
        Str += ",activarcopia2remision=" + ActivarCopia2Remision.ToString
        Str += ",nopermitirventacredito=" + NoPermitirFacturasdeCredito.ToString
        Str += ",nopermitirremisioncredito=" + NoPermitirRemisionesdeCredito.ToString
        Str += ",tipoprorrateo=" + TipoProrrateo.ToString
        Str += ",busquedaporclases=" + BusquedaporClases.ToString
        Str += ",maximizarventas=" + MaximizarVentas.ToString
        Str += ",activarcopiafacturacre2=" + ActivarCopiaFacturaCredito2.ToString
        Str += ",activarcopiafacturacre=" + ActivarCopiaFacturaCredito.ToString
        Str += ",activarcopiaremisioncre=" + ActivarCopiaRemisionCredito.ToString
        Str += ",activarcopiaremisioncre2=" + ActivarCopiaRemisionCredito2.ToString
        Str += ",pagosticket=" + PagosTicket.ToString
        Str += ",integrarbancos=" + IntegrarBancosVentasPagos.ToString
        Str += ",clientessinrepetir=" + ClientesSinRepetir.ToString
        Str += ",sobreescribeimploc=" + SobreEscribeImpLocales.ToString
        Str += ",facturarsoloexistencia=" + FacturarSoloExistencia.ToString
        Str += ",preciosultimocosto=" + TipoCostoPrecios.ToString
        Str += ",boletasinventario=" + BoletasInventario.ToString
        Str += ",boletasresumida=" + BoletasResumida.ToString
        Str += ",siempreporsurtir=" + SiemprePorSurtirVentas.ToString
        Str += ",siempreporsurtirr=" + SiemprePorSurtirRemisiones.ToString
        Str += ",copiaflujoventas=" + Copiaflujoventas.ToString
        Str += ",copiaflujorem=" + Copiaflujorem.ToString
        Str += ",integrarcont=" + IntegrarContabilidad.ToString
        Str += ",pvventanacomp=" + PVVentabaCompleta.ToString
        Str += ",idUsuarioCambio=" + GlobalIdUsuario.ToString()
        Str += ",fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "'"
        Str += ",horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "'"
        Comm.CommandText = Str
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblopciones2 set checafoliofacturas=" + ChecaFolioFacturas.ToString +
            ",surtidodefault=" + RecibidoDefault.ToString +
            ",nobloquearcd=" + NoBloquearCreardesde.ToString +
        ",pvconfirmarcobrar=" + PVConfirmarCobrar.ToString +
        ",noimpsinguardar=" + NoImpSinGuardar.ToString +
        ",remsindetallecd=" + RemisionesSinDetalleCD.ToString +
        ",idinventariocd=" + IdInventarioCD.ToString +
        ",integrarbancoscc=" + IntegrarBancosComprasContado.ToString +
        ",integrarbancoscp=" + IntegrarBancosComprasPagos.ToString +
        ",integrarbancosvp=" + IntegrarBancosVentasContado.ToString +
        ",acteneunodos=" + ActNom12.ToString +
        ",facturarpagosrem=" + FacturarPagosRemisiones.ToString +
        ",pediranticiporem=" + PedirAnticipoRemisiones.ToString +
        ",facturarsolocredito=" + FacturarSoloaCredito.ToString +
        ",remisionessolocredito=" + RemisionesSoloaCredito.ToString +
        ",vendedorusuario=" + VendedorUsuario.ToString +
        ",noproveedorsoriana=" + NoProveedorSoriana.ToString +
        ",noproveedorwalmart=" + NoProveedorWalmart.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaRutaXMLCompras() As String
        Comm.CommandText = "select ifnull((select rutaUUID from tblcontabilidadconf where id=1),'')"
        Return Comm.ExecuteScalar
    End Function
    Public Sub DaOpciones3(pIdSucursal As Integer)
        FacturaComoegreso = 0
        Dim Dreader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblopciones3 where idsucursal=" + pIdSucursal.ToString
        Dreader = Comm.ExecuteReader
        If Dreader.Read Then
            FacturaComoegreso = Dreader("facturacomoegreso")
        End If
        Dreader.Close()
    End Sub
    Public Sub GuardaOpciones3(pIdSucursal As Integer)
        Comm.CommandText = "select count(idsucursal) from tblopciones3 where idsucursal=" + pIdSucursal.ToString
        If Comm.ExecuteScalar > 0 Then
            Comm.CommandText = "update tblopciones3 set facturacomoegreso=" + FacturaComoegreso.ToString + " where idsucursal=" + pIdSucursal.ToString
            Comm.ExecuteNonQuery()
        Else
            Comm.CommandText = "insert into tblopciones3(idsucursal,facturacomoegreso) values(" + pIdSucursal.ToString + "," + FacturaComoegreso.ToString + ")"
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub GuardaOpcionCorte(pVentasRemTodas As Byte, pVentasRemxM As Byte)
        Comm.CommandText = "update tblopciones2 set ventascorteremtodas=" + pVentasRemTodas.ToString + ",ventascorteremxm=" + pVentasRemxM.ToString
        Comm.ExecuteNonQuery()
    End Sub
End Class
