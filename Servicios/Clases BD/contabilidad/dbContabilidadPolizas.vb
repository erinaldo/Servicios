Imports MySql.Data.MySqlClient
Public Class dbContabilidadPolizas
    Public Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public IDPoliza As Integer
    Public numeroPoliza As Integer
    Public fecha As String
    Public concepto As String
    Public beneficiario As String
    Public tipo2 As String
    Public tipo As Integer
    Public Numero As Integer
    Public Importe As String
    Public descripcionCuenta As String
    Public NNiv1 As Integer
    Public NNiv2 As Integer
    Public NNiv3 As Integer
    Public NNiv4 As Integer
    Public NNiv5 As Integer
    Public DNiv1 As String
    Public DNiv2 As String
    Public DNiv3 As String
    Public DNiv4 As String
    Public DNiv5 As String
    Public anio As String
    Public Estado As Byte
    Public IdClasificacion As Integer
    Public folioFact As String
    Public idProvee As Integer
    Public iva As String
    Public concepFactura As String
    Public totalCargo As Double
    Public totalAbono As Double
    Public MayorID As Integer
    Public IDRenglon As Integer
    Public tablaCheque As DataTable
    Public tablaTrans As DataTable
    Public tablaCompro As DataTable
    Public tablaComproNac2 As DataTable
    Public tablaComproE As DataTable
    Public tablaOtros As DataTable
    Public SoloIvaEnDiot As Byte

    Dim tablaChequeN As New DataTable
    Dim tablaTransN As New DataTable
    Dim tablaComproN As New DataTable
    Public tablaComproNac2N As New DataTable
    Public tablaComproEN As New DataTable
    Public tablaOtrosN As New DataTable
    Dim tablaDetalles As New DataTable

    Public TcargoD As Double = 0
    Public TcargoA As Double = 0
    Public TabonoD As Double = 0
    Public TabonoA As Double = 0
    Public activoC1 As String
    Public activoC2 As String
    Public activoF1 As String
    Public activoF2 As String
    Public activoD1 As String
    Public activoD2 As String
    Public activoO1 As String
    Public activoO2 As String
    Public pasivoC1 As String
    Public pasivoC2 As String
    Public pasivoF1 As String
    Public pasivoF2 As String
    Public pasivoD1 As String
    Public pasivoD2 As String
    Public capital1 As String
    Public capital2 As String
    Public capital3 As String
    Public capital4 As String
    Public resultados As String
    Public ordenA1 As String
    Public ordenA2 As String
    Public ordenD1 As String
    Public ordenD2 As String
    Public ingresos1 As String
    Public ingresos2 As String
    Public egresos1 As String
    Public egresos2 As String
    Public ordenVisible As Integer
    Private totalEdoRe As Double
    Public rutaUUID As String
    Public PreguntarCuadrar As Byte
    Public FechaTRabajo As String
    Public ActivarFechaTrabajo As Byte
    Public RutaXMLIngresos As String


    Public DetalleId As Integer
    Public DetalleIdPoliza As Integer
    Public Detallecuenta As String
    Public Detalledescripcion As String
    Public Detallecargo As Double
    Public DetalleAbono As Double
    Public DetalleidCuenta As Integer
    Public Detallefactura As String
    Public Detalleidproveedor As Integer
    Public Detalleiva As String
    Public Detalleconcepto As String
    Public DetalleesDIOT As Byte
    Public DetalleDIOTHabilitado As Byte
    Public DetallefechaDiot As String
    Public DetallevalorActos As String
    Public Detallereferencia As String
    Public Detalleivaret As Double
    Public Detalleieps As Double
    Public DetallesOrden As Integer
    Public UltimoOrden As Integer

    Public ResultadosDesc As String
    Public ResultadosidCuenta As Integer
    Public ResultadosCuenta As String
    Public ResultadosSaldo As Double
    Public ResultadosNaturaleza As String

    Public ComprobanteId As Integer
    Public ComprobanteUUID As String
    Public ComprobanteRFC As String
    Public ComprobanteMonto As Double
    Public ComprobanteMoneda As Integer
    Public ComprobanteTipodeCambio As Double

    Public Comprobante2Id As Integer
    Public Comprobante2Serie As String
    Public Comprobante2Folio As Integer
    Public Comprobante2RFC As String
    Public Comprobante2Monto As Double
    Public Comprobante2Moneda As Integer
    Public Comprobante2TipodeCambio As Double

    Public ComprobanteEId As Integer
    Public ComprobanteEFolio As String
    Public ComprobanteETax As String
    Public ComprobanteEMonto As Double
    Public ComprobanteEMoneda As Integer
    Public ComprobanteETipodeCambio As Double

    Public ChequeId As Integer
    'numero,banco,ctaOri,fecha,monto,benef,rfc,bancoemisor,moneda,tipocambio
    Public ChequeNunero As String
    Public ChequeBanco As Integer
    Public ChequeCtaOri As String
    Public ChequeFehca As String
    Public ChequeMonto As Double
    Public ChequeBenef As String
    Public ChequeRFC As String
    Public ChequeBancoEx As String
    Public ChequeMoneda As Integer
    Public ChequeTipoCambio As Double

    Public TransferenciaId As Integer
    ',ctaOri, bancoOri, monto,ctaDest, bancoDest,fecha,benef,rfc,bancoOrie,bancoDeste,moneda,tipoCambio
    Public TransCaOri As String
    Public TransBancoOri As Integer
    Public TransMonto As Double
    Public TransBancoDest As Integer
    Public TransFecha As String
    Public TransBenefi As String
    Public TransRFC As String
    Public TransBancoOriE As String
    Public TransBancoDestE As String
    Public TransMoneda As Integer
    Public TransTipoCambio As Double
    Public TransCtaDest As String

    Public OtrosId As Integer
    ',metodoPago,fecha,benef,rfc,monto,moneda,tipoCambio
    Public OtrosMetodoPago As Integer
    Public OtrosFecha As String
    Public OtrosBenef As String
    Public OtrosRFC As String
    Public OtrosMonto As Double
    Public OtrosMoneda As Integer
    Public OtrosTipoCambio As Double
    Public CantidadCheque As Double
    Public Leyenda As Byte

    Structure stCrearDesde
        Dim ID As Integer
        Dim IdCuenta As Integer
        Dim Orden As Integer
    End Structure

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
        Comm.CommandTimeout = 10000
        IDPoliza = -1
        numeroPoliza = -1
        fecha = ""
        concepto = ""
        beneficiario = ""
        llenaDatosConfig()
        llenarTablas()
    End Sub
    Private Sub llenarTablas()
        tablaChequeN.Columns.Add("numero")
        tablaChequeN.Columns.Add("clave")
        tablaChequeN.Columns.Add("ctaOri")
        tablaChequeN.Columns.Add("fecha")
        tablaChequeN.Columns.Add("monto")
        tablaChequeN.Columns.Add("benef")
        tablaChequeN.Columns.Add("rfc")


        tablaTransN.Columns.Add("ctaOri")
        tablaTransN.Columns.Add("bancoOri")
        tablaTransN.Columns.Add("monto")
        tablaTransN.Columns.Add("ctaDest")
        tablaTransN.Columns.Add("bancoDest")
        tablaTransN.Columns.Add("fecha")
        tablaTransN.Columns.Add("benef")
        tablaTransN.Columns.Add("rfc")



        tablaComproN.Columns.Add("UUID")
        tablaComproN.Columns.Add("monto")
        tablaComproN.Columns.Add("rfc")


        tablaDetalles.Columns.Add("id")
        tablaDetalles.Columns.Add("cuenta")
        tablaDetalles.Columns.Add("descripcion")
        tablaDetalles.Columns.Add("cargo")
        tablaDetalles.Columns.Add("abono")
    End Sub
    Public Function bucarNumero(ByVal pMes As String, ByVal pAnio As String, ByVal ptipo As String, pIdClasificacion As Integer) As Integer
        Dim contador As Integer = 0
        Dim numero As Integer = 0

        Comm.CommandText = "select count(id) from tblpolizas where tipo='" + ptipo + "' and  fecha>='" + pAnio + "/" + pMes + "/01' and fecha<='" + pAnio + "/" + pMes + "/31' and clasificacion=" + pIdClasificacion.ToString
        contador = Comm.ExecuteScalar
        If contador > 0 Then
            Comm.CommandText = "select max(numero) from tblpolizas where fecha>='" + pAnio + "/" + pMes + "/01' and fecha<='" + pAnio + "/" + pMes + "/31' and tipo='" + ptipo + "' and clasificacion=" + pIdClasificacion.ToString
            numero = Comm.ExecuteScalar + 1
        Else
            numero = 1
        End If
        Return numero
    End Function
    Public Function folioRepetido(ByVal pMes As String, ByVal pAnio As String, ByVal ptipo As String, ByVal pNumero As Integer, pIdClasificacion As Integer) As Integer
        Dim contador As Integer = 0
        Dim numero As Integer = 0
        Comm.CommandText = "select count(id) from tblpolizas where tipo='" + ptipo + "' and numero=" + pNumero.ToString + " and  fecha>='" + pAnio + "/" + pMes + "/01' and fecha<='" + pAnio + "/" + pMes + "/31' and clasificacion=" + pIdClasificacion.ToString
        contador = Comm.ExecuteScalar
        Return contador
    End Function
    Public Function folioRepetidoId(ByVal pMes As String, ByVal pAnio As String, ByVal ptipo As String, ByVal pNumero As Integer, pIdClasificacion As Integer) As Integer
        Dim contador As Integer = 0
        Dim numero As Integer = 0
        Comm.CommandText = "select ifnull((select id from tblpolizas where tipo='" + ptipo + "' and numero=" + pNumero.ToString + " and  fecha>='" + pAnio + "/" + pMes + "/01' and fecha<='" + pAnio + "/" + pMes + "/31' and clasificacion=" + pIdClasificacion.ToString + "),0)"
        contador = Comm.ExecuteScalar
        Return contador
    End Function
    Public Function existeCuenta(ByVal pIDCuenta As Integer) As Boolean
        Dim cont As Integer = 0
        Comm.CommandText = "select count(idCContable) from tblccontables where idCContable=" + pIDCuenta.ToString
        cont = Comm.ExecuteScalar
        If cont > 0 Then
            Return True
        Else
            Return False

        End If
    End Function
    Public Function BuscarIdCuenta(ByVal pnivel As Integer, ByVal pn1 As String, ByVal pn2 As String, ByVal pn3 As String, ByVal pn4 As String, ByVal pn5 As String) As Integer
        Dim pID As Integer = 0
        Comm.CommandText = "select ifnull((select idCContable from tblccontables where (tblccontables.descontinuada like '%" + anio + "%')=False and Cuenta='" + pn1.ToString + "'"
        If pnivel >= 2 Then
            Comm.CommandText += " and N2='" + pn2 + "'"
        End If
        If pnivel >= 3 Then
            Comm.CommandText += " and N3='" + pn3 + "'"
        End If
        If pnivel >= 4 Then
            Comm.CommandText += " and N4='" + pn4 + "'"
        End If
        If pnivel >= 5 Then
            Comm.CommandText += " and N5='" + pn5 + "'"
        End If
        Comm.CommandText += " limit 1 ),0)"
        pID = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select descripcion from tblccontables where idCContable=" + pID.ToString + " limit 1 ),'')"
        descripcionCuenta = Comm.ExecuteScalar()
        DNiv1 = ""
        DNiv2 = ""
        DNiv3 = ""
        DNiv4 = ""
        DNiv5 = ""
        Comm.CommandText = "select ifnull((select descripcion from tblccontables where cuenta='" + pn1 + "' and n2='' and n3='' and n4='' and n5='' limit 1 ),'')"
        DNiv1 = Comm.ExecuteScalar()
        If pn2 <> "" Then
            Comm.CommandText = "select ifnull((select descripcion from tblccontables where cuenta='" + pn1 + "' and n2='" + pn2 + "' and n3='' and n4='' and n5='' limit 1 ),'')"
            DNiv2 = Comm.ExecuteScalar()
        End If
        If pn3 <> "" Then
            Comm.CommandText = "select ifnull((select descripcion from tblccontables where cuenta='" + pn1 + "' and n2='" + pn2 + "' and n3='" + pn3 + "' and n4='' and n5='' limit 1 ),'')"
            DNiv3 = Comm.ExecuteScalar()
        End If
        If pn4 <> "" Then
            Comm.CommandText = "select ifnull((select descripcion from tblccontables where cuenta='" + pn1 + "' and n2='" + pn2 + "' and n3='" + pn3 + "' and n4='" + pn4 + "' and n5='' limit 1 ),'')"
            DNiv4 = Comm.ExecuteScalar()
        End If
        If pn5 <> "" Then
            'Comm.CommandText = "select ifnull((select descripcion from tblccontables where cuenta='" + pn1 + "' and n2='" + pn2 + "' and n3='" + pn3 + "' and n4='" + pn4 + "' and n5='" + pn5 + "' limit 1 ),'')"
            DNiv5 = descripcionCuenta
        End If
        Return pID
    End Function
    Public Function bucarCuenta(ByVal pIDCuenta As Integer) As String
        Dim niv As Integer = 0
        Dim cuen As String = ""

        Comm.CommandText = "select nivel from tblccontables where idCContable=" + pIDCuenta.ToString
        niv = Comm.ExecuteScalar
        Comm.CommandText = "select concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0') "
        If niv >= 2 Then
            Comm.CommandText += ",' ',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0') "
        End If
        If niv >= 3 Then
            Comm.CommandText += ",' ',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0') "
        End If
        If niv >= 4 Then
            Comm.CommandText += ",' ',LPAD(tblccontables.N4," + NNiv4.ToString + ",'0') "
        End If
        If niv >= 5 Then
            Comm.CommandText += ",' ',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0') "
        End If
        Comm.CommandText += ")from tblccontables where idCContable=" + pIDCuenta.ToString
        cuen = Comm.ExecuteScalar

        Return cuen
    End Function

    Public Sub guardarPoliza(ByVal ptipo As String, ByVal pNumer As Integer, ByVal pFecha As String, ByVal pConcepto As String, ByVal pBeneficiario As String, ByVal pImporte As String, ByVal pClas As Integer, pEstado As Byte, pCantCheque As Double, pLeyenda As Byte)
        If pBeneficiario.Length > 1500 Then pBeneficiario = pBeneficiario.Substring(0, 1500)
        Comm.CommandText = "insert into tblpolizas (tipo,numero,fecha,concepto,beneficiario,importe,clasificacion,idUsuarioalta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,estado,cantidadcheque,leyendach) values('" + ptipo.ToString + "'," + pNumer.ToString + ",'" + pFecha + "','" + Replace(pConcepto, "'", "''") + "','" + Replace(pBeneficiario, "'", "''") + "','" + pImporte.ToString + "'," + pClas.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH.mm:ss") + "'," + pEstado.ToString + "," + pCantCheque.ToString + "," + pLeyenda.ToString + ");"
        Comm.CommandText += "select ifnull(last_insert_id(),0);"
        IDPoliza = Comm.ExecuteScalar

    End Sub
    Public Sub ActualizaIdsDetalles(pIdPoliza As Integer)
        Comm.CommandText = "update tblpolizasdetalles set idcuentan1=(select c.idcuentam from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan2=(select c.idcuentan2 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan3=(select c.idcuentan3 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan4=(select c.idcuentan4 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan5=(select c.idcuentan5 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta) where idpoliza=" + pIdPoliza.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub CrearDesde(PIdpolizaAnt As Integer, pIdPolizaNueva As Integer)
        Comm.CommandText = "insert into tblpolizasdetalles (idpoliza,cuenta,descripcion,cargo,abono,idCuenta,factura,idproveedor,iva,concepto,esDIOT,DIOTHabilitado,fechaDiot,valorActos,referencia,ivaret,ieps,orden,idcuentan1,idcuentan2,idcuentan3,idcuentan4,idcuentan5) select " + pIdPolizaNueva.ToString + ",cuenta,descripcion,cargo,abono,idCuenta,factura,idproveedor,iva,concepto,esDIOT,DIOTHabilitado,fechaDiot,valorActos,referencia,ivaret,ieps,orden,idcuentan1,idcuentan2,idcuentan3,idcuentan4,idcuentan5 from tblpolizasdetalles where idpoliza=" + PIdpolizaAnt.ToString
        Comm.ExecuteNonQuery()
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim Detalles As New Collection
        Dim cd As stCrearDesde
        Comm.CommandText = "select id,idcuenta,orden from tblpolizasdetalles where idpoliza=" + PIdpolizaAnt.ToString
        DR = Comm.ExecuteReader
        While DR.Read
            cd.ID = DR("id")
            cd.IdCuenta = DR("idcuenta")
            cd.Orden = DR("orden")
            Detalles.Add(cd)
        End While
        DR.Close()
        Dim Iddetalleint As Integer
        For Each e As stCrearDesde In Detalles
            Comm.CommandText = "select id from tblpolizasdetalles where idcuenta=" + e.IdCuenta.ToString + " and orden=" + e.Orden.ToString + " and idpoliza=" + pIdPolizaNueva.ToString
            Iddetalleint = Comm.ExecuteScalar
            Comm.CommandText = "insert into tblcontabilidadCompro(idPoliza,idRenglon,UUID,monto,rfc,moneda,tipoCambio) select " + pIdPolizaNueva.ToString + "," + Iddetalleint.ToString + ",UUID,monto,rfc,moneda,tipoCambio from tblcontabilidadcompro where idrenglon=" + e.ID.ToString + ";"
            Comm.CommandText += "insert into tblcontabilidadcompro2(idPoliza,idRenglon,serie,folio,rfc,monto,moneda,tipoCambio) select " + pIdPolizaNueva.ToString + "," + Iddetalleint.ToString + ",serie,folio,rfc,monto,moneda,tipoCambio from tblcontabilidadcompro2 where idrenglon=" + e.ID.ToString + ";"
            Comm.CommandText += "insert into tblcontabilidadcomproe(idPoliza,idRenglon,numFactura,taxID,monto,moneda,tipoCambio) select " + pIdPolizaNueva.ToString + "," + Iddetalleint.ToString + ",numFactura,taxID,monto,moneda,tipoCambio from tblcontabilidadcomproe where idrenglon=" + e.ID.ToString + ";"
            Comm.CommandText += "insert into tblcontabilidadCheque(idPoliza,idRenglon,numero,banco,ctaOri,fecha,monto,benef,rfc,bancoemisor,moneda,tipocambio) select " + pIdPolizaNueva.ToString + "," + Iddetalleint.ToString + ",numero,banco,ctaOri,fecha,monto,benef,rfc,bancoemisor,moneda,tipocambio from tblcontabilidadcheque where idrenglon=" + e.ID.ToString + ";"
            Comm.CommandText += "insert into tblcontabilidadTrans(idPoliza,idRenglon,ctaOri, bancoOri, monto,ctaDest, bancoDest,fecha,benef,rfc,bancoOrie,bancoDeste,moneda,tipoCambio) select " + pIdPolizaNueva.ToString + "," + Iddetalleint.ToString + ",ctaOri, bancoOri, monto,ctaDest, bancoDest,fecha,benef,rfc,bancoOrie,bancoDeste,moneda,tipoCambio from tblcontabilidadtrans where idrenglon=" + e.ID.ToString + ";"
            Comm.CommandText += "insert into tblcontabilidadotros(idPoliza,idRenglon,metodoPago,fecha,benef,rfc,monto,moneda,tipoCambio) select " + pIdPolizaNueva.ToString + "," + Iddetalleint.ToString + ",metodoPago,fecha,benef,rfc,monto,moneda,tipoCambio from tblcontabilidadotros where idrenglon=" + e.ID.ToString + ";"
            Comm.ExecuteNonQuery()
        Next
    End Sub
    Public Function ActualizaMontoPoliza(pidPoliza As Integer) As Boolean
        Dim pMonto As Double
        Dim pMonto2 As Double
        Comm.CommandText = "select ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo)) from tblpolizasdetalles where idpoliza=" + pidPoliza.ToString + "),0)"
        pMonto = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)) from tblpolizasdetalles where idpoliza=" + pidPoliza.ToString + "),0)"
        pMonto2 = Comm.ExecuteScalar
        If pMonto = 0 And pMonto2 = 0 Then
            eliminarPoliza(pidPoliza)
            IDPoliza = 0
            Return False
        Else
            Comm.CommandText = "update tblpolizas set importe='" + pMonto.ToString("0.00") + "' where id=" + pidPoliza.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "update tblpolizasdetalles set idcuentan1=(select c.idcuentam from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan2=(select c.idcuentan2 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan3=(select c.idcuentan3 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan4=(select c.idcuentan4 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan5=(select c.idcuentan5 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta) where idpoliza=" + pidPoliza.ToString
            Comm.ExecuteNonQuery()
        End If
        Return True
    End Function
    Public Sub guardarDetalles(ByVal pCuenta As String, ByVal pConcepto As String, ByVal pCargo As String, ByVal pAbono As String, ByVal pIDCuenta As Integer, ByVal pFolioFact As String, ByVal pIdProv As Integer, ByVal piva As String, ByVal pconFac As String, ByVal pesDIOT As Integer, ByVal pValorActos As Double, ByVal pFechaDIOT As String, pRefdiot As String, pIvaret As Double, pIeps As Double, pOrden As Integer, pIdNivel1 As Integer, pIdNivel2 As Integer, pIdNivel3 As Integer, pIdNivel4 As Integer, pIdNivel5 As Integer)
        Dim cargo As Double
        Dim abono As Double
        If pCargo = "" Then
            cargo = -999999999
        Else
            cargo = pCargo
        End If
        If pAbono = "" Then
            abono = -999999999
        Else
            abono = pAbono
        End If

        Comm.CommandText = "insert into tblpolizasdetalles (idpoliza,cuenta,descripcion,cargo,abono,idCuenta,factura,idproveedor,iva,concepto,esDIOT,DIOTHabilitado,fechaDiot,valorActos,referencia,ivaret,ieps,orden,idcuentan1,idcuentan2,idcuentan3,idcuentan4,idcuentan5) values(" + IDPoliza.ToString + ",'" + pCuenta + "','" + Replace(pConcepto, "'", "''") + "'," + cargo.ToString + "," + abono.ToString + "," + pIDCuenta.ToString + ",'" + Replace(pFolioFact, "'", "''") + "'," + pIdProv.ToString + ",'" + piva.ToString + "','" + pconFac.ToString + "'," + pesDIOT.ToString + ",0,'" + pFechaDIOT + "'," + pValorActos.ToString + ",'" + Replace(pRefdiot.Trim, "'", "''") + "'," + pIvaret.ToString + "," + pIeps.ToString + "," + pOrden.ToString + "," + pIdNivel1.ToString + "," + pIdNivel2.ToString + "," + pIdNivel3.ToString + "," + pIdNivel4.ToString + "," + pIdNivel5.ToString + ");"
        Comm.CommandText += "select ifnull(last_insert_id(),0);"
        IDRenglon = Comm.ExecuteScalar
        DetalleId = IDRenglon
        DetallesOrden += 1
    End Sub
    Public Sub ActulizaIdsPolizas()
        Comm.CommandText = "update tblpolizasdetalles set idcuentan1=(select c.idcuentam from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan2=(select c.idcuentan2 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan3=(select c.idcuentan3 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan4=(select c.idcuentan4 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta),idcuentan5=(select c.idcuentan5 from tblccontables c where c.idccontable=tblpolizasdetalles.idcuenta)"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub LlenaOrdenesPorMientras(pIdPoliza As Integer)
        Comm.CommandText = "update tblpolizadetalles set orden=id where idpoliza=" + pIdPoliza.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificaDetalle(pIdDetalle As Integer, ByVal pCuenta As String, ByVal pConcepto As String, ByVal pCargo As String, ByVal pAbono As String, ByVal pIDCuenta As Integer, ByVal pFolioFact As String, ByVal pIdProv As Integer, ByVal piva As String, ByVal pconFac As String, ByVal pesDIOT As Integer, ByVal pValorActos As Double, ByVal pFechaDIOT As String, pRefdiot As String, pIvaret As Double, pIeps As Double, pIdNivel1 As Integer, pidNivel2 As Integer, pIdNivel3 As Integer, pIdNivel4 As Integer, pIdnivel5 As Integer)
        Dim cargo As Double
        Dim abono As Double
        If pCargo = "" Then
            cargo = -999999999
        Else
            cargo = pCargo
        End If
        If pAbono = "" Then
            abono = -999999999
        Else
            abono = pAbono
        End If

        Comm.CommandText = "update tblpolizasdetalles set cuenta='" + pCuenta + "',descripcion='" + Replace(pConcepto, "'", "''") + "',cargo=" + cargo.ToString + ",abono=" + abono.ToString + ",idcuenta=" + pIDCuenta.ToString + ",factura='" + Replace(pFolioFact, "'", "''") + "',idproveedor=" + pIdProv.ToString + ",iva='" + piva.ToString + "',concepto='" + pconFac.ToString + "',esdiot=" + pesDIOT.ToString + ",fechadiot='" + pFechaDIOT + "',valoractos=" + pValorActos.ToString + ",referencia='" + Replace(pRefdiot.Trim, "'", "''") + "',ivaret=" + pIvaret.ToString + ",ieps=" + pIeps.ToString + ",idcuentan1=" + pIdNivel1.ToString + ",idcuentan2=" + pidNivel2.ToString + ",idcuentan3=" + pIdNivel3.ToString + ",idcuentan4=" + pIdNivel4.ToString + ",idcuentan5=" + pIdnivel5.ToString + " where id=" + pIdDetalle.ToString + ";"
        Comm.ExecuteNonQuery()
        IDRenglon = pIdDetalle
    End Sub
    'comprobantes
    Public Sub guardarCheque(ByVal pIDPoliza As Integer, ByVal pRenglon As Integer, ByVal pNumero As String, ByVal pBanco As Integer, ByVal pBancoEx As String, ByVal pCtaOri As String, ByVal pFecha As String, ByVal pBeneficiario As String, ByVal pRFC As String, ByVal pMonto As Double, ByVal pIDMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "insert into tblcontabilidadCheque (idPoliza,idRenglon,numero,banco,ctaOri,fecha,monto,benef,rfc,bancoemisor,moneda,tipocambio) values(" + pIDPoliza.ToString + "," + pRenglon.ToString + ",'" + Replace(pNumero, "'", "''") + "'," + pBanco.ToString + ",'" + Replace(pCtaOri, "'", "''") + "','" + Replace(pFecha, "'", "''") + "'," + pMonto.ToString + ",'" + Replace(pBeneficiario, "'", "''") + "','" + Replace(pRFC, "'", "''") + "','" + Replace(pBancoEx, "'", "''") + "'," + pIDMoneda.ToString + "," + pTipoCambio.ToString + ");"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarCheque(pId As Integer, ByVal pNumero As String, ByVal pBanco As Integer, ByVal pBancoEx As String, ByVal pCtaOri As String, ByVal pFecha As String, ByVal pBeneficiario As String, ByVal pRFC As String, ByVal pMonto As Double, ByVal pIDMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "update tblcontabilidadCheque set numero='" + Replace(pNumero, "'", "''") + "',banco=" + pBanco.ToString + ",ctaOri='" + Replace(pCtaOri, "'", "''") + "',fecha='" + pFecha + "',monto=" + pMonto.ToString + ",benef='" + Replace(pBeneficiario, "'", "''") + "',rfc='" + Replace(pRFC, "'", "''") + "',bancoemisor='" + Replace(pBancoEx, "'", "''") + "',moneda=" + pIDMoneda.ToString + ",tipocambio=" + pTipoCambio.ToString + " where id=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarCheque(ByVal pID As Integer)
        Comm.CommandText = " delete from tblcontabilidadCheque where id=" + pID.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaCheques(ByVal pIdrenglon As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select id,numero,(select nombre from tblbancoscatalogo where clave=banco),ctaOri,fecha,monto from tblcontabilidadcheque where idrenglon=" + pIdrenglon.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcch")
        Return DS.Tables("tblcch").DefaultView
    End Function
    Public Sub LlenaDatosCheque(pId As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcontabilidadcheque where id=" + pId.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            ChequeBanco = DReader("banco")
            ChequeBancoEx = DReader("bancoemisor")
            ChequeBenef = DReader("benef")
            ChequeCtaOri = DReader("ctaori")
            ChequeFehca = DReader("fecha")
            ChequeMoneda = DReader("moneda")
            ChequeMonto = DReader("monto")
            ChequeNunero = DReader("numero")
            ChequeRFC = DReader("rfc")
            ChequeTipoCambio = DReader("tipocambio")
        End If
        DReader.Close()
    End Sub
    Public Function TotalCheques(pIDRenglon As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(monto) from tblcontabilidadcheque where idrenglon=" + pIDRenglon.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Sub guardarTransferencia(ByVal pIdPoliza As Integer, ByVal pIdRenglon As Integer, ByVal pCtaOri As String, ByVal pBancoOri As Integer, ByVal pBancoExO As String, ByVal pCtaDEst As String, ByVal pBancoDest As Integer, ByVal pBancoExD As String, ByVal pFecha As String, ByVal pBenef As String, ByVal pRFC As String, ByVal pMonto As Double, ByVal pidMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "insert into tblcontabilidadTrans (idPoliza,idRenglon,ctaOri, bancoOri, monto,ctaDest, bancoDest,fecha,benef,rfc,bancoOrie,bancoDeste,moneda,tipoCambio) values(" + pIdPoliza.ToString + "," + pIdRenglon.ToString + ",'" + pCtaOri + "'," + pBancoOri.ToString + "," + pMonto.ToString + ",'" + pCtaDEst + "'," + pBancoDest.ToString + ",'" + pFecha + "','" + Replace(pBenef, "'", "''") + "','" + pRFC + "','" + Replace(pBancoExO, "'", "''") + "','" + Replace(pBancoExD, "'", "''") + "'," + pidMoneda.ToString + "," + pTipoCambio.ToString + ");"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarTransaccion(ByVal pId As Integer, ByVal pCtaOri As String, ByVal pBancoOri As Integer, ByVal pBancoExO As String, ByVal pCtaDEst As String, ByVal pBancoDest As Integer, ByVal pBancoExD As String, ByVal pFecha As String, ByVal pBenef As String, ByVal pRFC As String, ByVal pMonto As Double, ByVal pidMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "update tblcontabilidadTrans set ctaOri='" + Replace(pCtaOri, "'", "''") + "', bancoOri=" + pBancoOri.ToString + ", monto=" + pMonto.ToString + ",ctaDest='" + Replace(pCtaDEst, "'", "''") + "', bancoDest=" + pBancoDest.ToString + ",fecha='" + pFecha + "',benef='" + Replace(pBenef, "'", "''") + "',rfc='" + Replace(pRFC, "'", "''") + "',bancoOrie='" + Replace(pBancoExO, "'", "''") + "',bancoDeste='" + Replace(pBancoExD, "'", "''") + "',moneda=" + pidMoneda.ToString + ",tipoCambio=" + pTipoCambio.ToString + " where id=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarTrans(ByVal pID As Integer)
        Comm.CommandText = " delete from tblcontabilidadTrans where id=" + pID.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaTrans(ByVal pIdrenglon As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select id,fecha,concat(ctaOri,' ',(select nombre from tblbancoscatalogo where clave=bancoori)),concat(ctaDest,' ',(select nombre from tblbancoscatalogo where clave=bancodest)),monto from tblcontabilidadtrans where idrenglon=" + pIdrenglon.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbltr")
        Return DS.Tables("tbltr").DefaultView
    End Function
    Public Sub LlenaDatosTransferencia(pId As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcontabilidadtrans where id=" + pId.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            TransBancoDest = DReader("bancodest")
            TransBancoDestE = DReader("bancodeste")
            TransBancoOri = DReader("bancoori")
            TransBancoOriE = DReader("bancoorie")
            TransBenefi = DReader("benef")
            TransCaOri = DReader("ctaori")
            TransCtaDest = DReader("ctadest")
            TransFecha = DReader("fecha")
            TransMoneda = DReader("moneda")
            TransMonto = DReader("monto")
            TransRFC = DReader("rfc")
            TransTipoCambio = DReader("tipocambio")
        End If
        DReader.Close()
    End Sub
    Public Function TotalTrans(pIDRenglon As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(monto) from tblcontabilidadtrans where idrenglon=" + pIDRenglon.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Sub guardarComprobante(ByVal pIDPoliza As Integer, ByVal pIDRenglon As Integer, ByVal pUUID As String, ByVal pMonto As Double, ByVal pRFC As String, ByVal pidMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "insert into tblcontabilidadCompro(idPoliza,idRenglon,UUID,monto,rfc,moneda,tipoCambio) values(" + pIDPoliza.ToString + "," + pIDRenglon.ToString + ",'" + Replace(pUUID, "'", "''") + "'," + pMonto.ToString + ",'" + Replace(pRFC, "'", "''") + "'," + pidMoneda.ToString + "," + pTipoCambio.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarComprobante(pID As Integer, ByVal pUUID As String, ByVal pMonto As Double, ByVal pRFC As String, ByVal pidMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "update tblcontabilidadCompro set UUID='" + Replace(pUUID, "'", "''") + "',monto=" + pMonto.ToString + ",rfc='" + Replace(pRFC, "'", "''") + "',moneda=" + pidMoneda.ToString + ",tipoCambio=" + pTipoCambio.ToString + " where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarComprobante(ByVal pID As Integer)
        Comm.CommandText = " delete from tblcontabilidadCompro where id=" + pID.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaComprobantes(ByVal pIdrenglon As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select id,uuid,monto,rfc from tblcontabilidadcompro where idrenglon=" + pIdrenglon.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblccom")
        Return DS.Tables("tblccom").DefaultView
    End Function
    Public Function TotalComprobantes(pIDRenglon As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(monto) from tblcontabilidadcompro where idrenglon=" + pIDRenglon.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Sub LlenaDatosComprobante(pId As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcontabilidadcompro where id=" + pId.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            ComprobanteMoneda = DReader("moneda")
            ComprobanteMonto = DReader("monto")
            ComprobanteRFC = DReader("rfc")
            ComprobanteTipodeCambio = DReader("tipocambio")
            ComprobanteUUID = DReader("uuid")
        End If
        DReader.Close()
    End Sub
    Public Sub guardarComprobanteNac2(ByVal pIDPoliza As Integer, ByVal pIDRenglon As Integer, ByVal pSerie As String, ByVal pFolio As Integer, ByVal pRFC As String, ByVal pmonto As Double, ByVal pidMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "insert into tblcontabilidadcompro2(idPoliza,idRenglon,serie,folio,rfc,monto,moneda,tipoCambio) values(" + pIDPoliza.ToString + "," + pIDRenglon.ToString + ",'" + Replace(pSerie, "'", "''") + "'," + pFolio.ToString + ",'" + Replace(pRFC, "'", "''") + "'," + pmonto.ToString + "," + pidMoneda.ToString + "," + pTipoCambio.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarComprobanteNac2(ByVal pID As Integer, ByVal pSerie As String, ByVal pFolio As Integer, ByVal pRFC As String, ByVal pmonto As Double, ByVal pidMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "update tblcontabilidadcompro2 set serie='" + Replace(pSerie, "'", "''") + "',folio=" + pFolio.ToString + ",rfc='" + Replace(pRFC, "'", "''") + "',monto=" + pmonto.ToString + ",moneda=" + pidMoneda.ToString + ",tipoCambio=" + pTipoCambio.ToString + " where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarComproNac2(ByVal pID As Integer)
        Comm.CommandText = " delete from tblcontabilidadcompro2 where id=" + pID.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub

    Public Function ConsultaComprobantesNa2(ByVal pIdrenglon As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select id,serie,folio,rfc,monto from tblcontabilidadcompro2 where idrenglon=" + pIdrenglon.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblccom2")
        Return DS.Tables("tblccom2").DefaultView
    End Function
    Public Function TotalComprobantesNa2(pIDRenglon As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(monto) from tblcontabilidadcompro2 where idrenglon=" + pIDRenglon.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Sub LlenaDatosComprobanteNa2(pId As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcontabilidadcompro2 where id=" + pId.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Comprobante2Moneda = DReader("moneda")
            Comprobante2Monto = DReader("monto")
            Comprobante2RFC = DReader("rfc")
            Comprobante2TipodeCambio = DReader("tipocambio")
            Comprobante2Serie = DReader("serie")
            Comprobante2Folio = DReader("folio")
        End If
        DReader.Close()
    End Sub
    Public Sub guardarComprobanteE(ByVal pIDPoliza As Integer, ByVal pIDRenglon As Integer, ByVal pNumerdoFactura As String, ByVal ptaxID As String, ByVal pmonto As Double, ByVal pidMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "insert into tblcontabilidadcomproe(idPoliza,idRenglon,numFactura,taxID,monto,moneda,tipoCambio) values(" + pIDPoliza.ToString + "," + pIDRenglon.ToString + ",'" + Replace(pNumerdoFactura, "'", "''") + "','" + Replace(ptaxID, "'", "''") + "'," + pmonto.ToString + "," + pidMoneda.ToString + "," + pTipoCambio.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub modificarComprobanteE(ByVal pID As Integer, ByVal pNumerdoFactura As String, ByVal ptaxID As String, ByVal pmonto As Double, ByVal pidMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "update tblcontabilidadcomproe set numFactura='" + Replace(pNumerdoFactura, "'", "''") + "',taxID='" + Replace(ptaxID, "'", "''") + "',monto=" + pmonto.ToString + ",moneda=" + pidMoneda.ToString + ",tipoCambio=" + pTipoCambio.ToString + " where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarComproE(ByVal pID As Integer)
        Comm.CommandText = " delete from tblcontabilidadcomproe where id=" + pID.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaComprobantesE(ByVal pIdrenglon As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select id,numfactura,taxid,monto,(select tblmonedassat.moneda from tblmonedassat where tblmonedassat.id=tblcontabilidadcomproe.moneda ) Moneda,tipocambio from tblcontabilidadcomproe where idrenglon=" + pIdrenglon.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblccome")
        Return DS.Tables("tblccome").DefaultView
    End Function
    Public Function TotalComprobantesE(pIDRenglon As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(monto) from tblcontabilidadcomproe where idrenglon=" + pIDRenglon.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Sub LlenaDatosComprobanteE(pId As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcontabilidadcomproe where id=" + pId.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            ComprobanteEMoneda = DReader("moneda")
            ComprobanteEMonto = DReader("monto")
            ComprobanteETax = DReader("taxid")
            ComprobanteETipodeCambio = DReader("tipocambio")
            ComprobanteEFolio = DReader("numfactura")
        End If
        DReader.Close()
    End Sub

    Public Sub guardarOtro(ByVal pIDPoliza As Integer, ByVal pIDRenglon As Integer, ByVal pMetodoPago As Integer, ByVal pFecha As String, ByVal pBeneficiario As String, ByVal pRFC As String, ByVal pmonto As Double, ByVal pidMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "insert into tblcontabilidadotros(idPoliza,idRenglon,metodoPago,fecha,benef,rfc,monto,moneda,tipoCambio) values(" + pIDPoliza.ToString + "," + pIDRenglon.ToString + "," + pMetodoPago.ToString + ",'" + Replace(pFecha, "'", "''") + "','" + Replace(pBeneficiario, "'", "''") + "','" + Replace(pRFC, "'", "''") + "'," + pmonto.ToString + "," + pidMoneda.ToString + "," + pTipoCambio.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub modificarOtro(ByVal pID As Integer, ByVal pMetodoPago As Integer, ByVal pFecha As String, ByVal pBeneficiario As String, ByVal pRFC As String, ByVal pmonto As Double, ByVal pidMoneda As Integer, ByVal pTipoCambio As Double)
        Comm.CommandText = "update tblcontabilidadotros set metodoPago=" + pMetodoPago.ToString + ",fecha='" + pFecha + "',benef='" + Replace(pBeneficiario, "'", "''") + "',rfc='" + Replace(pRFC, "'", "''") + "',monto=" + pmonto.ToString + ",moneda=" + pidMoneda.ToString + ",tipoCambio=" + pTipoCambio.ToString + " where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarOtro(ByVal pID As Integer)
        Comm.CommandText = " delete from tblcontabilidadotros where id=" + pID.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaOtro(ByVal pIdrenglon As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select id,(select concat(lpad(convert(clave using utf8),2,'0'),' ',nombre) from tblformasdepagosat where clave=metodopago),fecha,benef,monto from tblcontabilidadotros where idrenglon=" + pIdrenglon.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblotro")
        Return DS.Tables("tblotro").DefaultView
    End Function
    Public Sub LlenaDatosOtros(pId As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcontabilidadotros where id=" + pId.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            OtrosBenef = DReader("benef")
            OtrosFecha = DReader("fecha")
            OtrosMetodoPago = DReader("metodopago")
            OtrosMoneda = DReader("moneda")
            OtrosMonto = DReader("monto")
            OtrosRFC = DReader("rfc")
            OtrosTipoCambio = DReader("tipocambio")
        End If
        DReader.Close()
    End Sub
    Public Function TotalOtros(pIDRenglon As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(monto) from tblcontabilidadotros where idrenglon=" + pIDRenglon.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function reporte(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pCon As String, ByVal ptipo As Integer, ByVal pClasif As Integer, pDesbalanceadas As Boolean, pCon2 As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblpolizas.id,Concat(tblpolizas.tipo,tblpolizas.numero) as Póliza,tblpolizas.fecha as Fecha,tblpolizas.concepto as Concepto,tblpolizas.importe as Importe,(select (ifnull(sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))),0)-ifnull(sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))),0)) from tblpolizasdetalles where  tblpolizasdetalles.idPoliza=tblPolizas.id) as dif,tblcontabilidadclas.nombre as Clasificacion from tblpolizas inner join tblcontabilidadclas on tblcontabilidadclas.id=tblPolizas.clasificacion  where fecha>='" + pFechaI + "' and fecha<='" + pFechaF + "' and estado=3 "
        If pCon <> "" Then
            'Comm.CommandText += " and concepto like '%" + pCon + "%'"
            Comm.CommandText += " and convert(tblpolizas.numero using utf8) like '%" + Replace(pCon, "'", "''") + "%'"
        End If
        If pCon2 <> "" Then
            Comm.CommandText += " and concepto like '%" + pCon2 + "%'"
        End If
        If pClasif > 0 Then
            Comm.CommandText += " and clasificacion=" + pClasif.ToString
        End If
        If ptipo <> 0 Then
            If ptipo = 1 Then
                Comm.CommandText += " and tipo='E'"
            End If
            If ptipo = 2 Then
                Comm.CommandText += " and tipo='I'"
            End If
            If ptipo = 3 Then
                Comm.CommandText += " and tipo='D'"
            End If
            If ptipo = 4 Then
                Comm.CommandText += " and tipo='A'"
            End If
        End If
        If pDesbalanceadas Then
            Comm.CommandText += " and (select (ifnull(sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))),0)-ifnull(sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))),0)) from tblpolizasdetalles where  tblpolizasdetalles.idPoliza=tblPolizas.id)<>0"
        End If
        Comm.CommandText += " order by tipo,numero,fecha"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpoliza")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblpoliza").DefaultView
    End Function
    Public Function llenaDatosPoliza(ByVal pID As Integer)

        IDPoliza = pID
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblpolizas where id=" + IDPoliza.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            tipo2 = DReader("tipo")
            Numero = DReader("numero")
            fecha = DReader("fecha")
            concepto = DReader("concepto")
            beneficiario = DReader("beneficiario")
            Importe = DReader("importe")
            CantidadCheque = DReader("cantidadcheque")
            Leyenda = DReader("leyendach")
        End If
        DReader.Close()
        If tipo2 = "E" Then
            tipo = 0
        End If
        If tipo2 = "I" Then
            tipo = 1
        End If
        If tipo2 = "D" Then
            tipo = 2
        End If
        If tipo2 = "O" Then
            tipo = 2
        End If
        If tipo2 = "A" Then
            tipo = 3
        End If
        Comm.CommandText = "select COALESCE(MAX(id),-1) from tblpolizasdetalles where idPoliza=" + IDPoliza.ToString
        MayorID = Comm.ExecuteScalar
        MayorID = MayorID + 1

        Dim DS1 As New DataSet
        Comm.CommandText = "select tblcontabilidadcheque.id as id,tblcontabilidadcheque.idPoliza as idPoliza,tblcontabilidadcheque.idRenglon as idRenglon,tblcontabilidadcheque.numero as Numero,tblcontabilidadcheque.banco as BancoCod, tblbancoscatalogo.nombre as Banco,tblcontabilidadcheque.bancoemisor as  Banco_extranjero,tblcontabilidadcheque.ctaOri as CuentaOri,tblcontabilidadcheque.fecha as Fecha,tblcontabilidadcheque.benef as Beneficiario,tblcontabilidadcheque.rfc as RFC, tblcontabilidadcheque.monto as Monto, tblcontabilidadcheque.moneda as idMoneda, tblmonedassat.moneda as Moneda,tblcontabilidadcheque.tipoCambio as Tipo_cambio    from tblcontabilidadcheque inner join tblbancoscatalogo on tblcontabilidadcheque.banco=tblbancoscatalogo.clave inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcheque.moneda  where tblcontabilidadcheque.idPoliza=" + IDPoliza.ToString
        Dim DA1 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA1.Fill(DS1, "tblComprobCheques")
        'DS.WriteXmlSchema("tblComprobCheques.xml")
        tablaCheque = DS1.Tables("tblComprobCheques")

        Dim DS2 As New DataSet
        Comm.CommandText = "select tblcontabilidadtrans.id as id,tblcontabilidadtrans.idPoliza as idPoliza,tblcontabilidadtrans.idRenglon as idRenglon,tblcontabilidadtrans.ctaOri as CuentaOri,tblcontabilidadtrans.bancoOri as BancoCodO,(select nombre from tblbancoscatalogo where clave=tblcontabilidadtrans.bancoOri) as BancoOri,tblcontabilidadtrans.bancoOrie as Banco_extranjero_O,tblcontabilidadtrans.ctaDest as CuentaDest,tblcontabilidadtrans.bancoDest as BancoCodD,(select nombre from tblbancoscatalogo where clave=tblcontabilidadtrans.bancoDest) as BancoDest,tblcontabilidadtrans.bancoDeste as Banco_extranjero_D,tblcontabilidadtrans.Fecha as Fecha, tblcontabilidadtrans.benef as Beneficiario, tblcontabilidadtrans.rfc as RFC,tblcontabilidadtrans.Monto,tblcontabilidadtrans.moneda as idMoneda, tblmonedassat.moneda  as Moneda, tblcontabilidadtrans.tipoCambio as tipo_Cambio from tblcontabilidadtrans inner join tblmonedassat on tblmonedassat.id=tblcontabilidadtrans.moneda where idPoliza=" + IDPoliza.ToString
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblComprobTrans")
        'DS.WriteXmlSchema("tblComprobTrans.xml")
        tablaTrans = DS2.Tables("tblComprobTrans")

        Dim DS3 As New DataSet
        Comm.CommandText = "select tblcontabilidadcompro.id as id,tblcontabilidadcompro.idPoliza as idPoliza,tblcontabilidadcompro.idRenglon as idRenglon,tblcontabilidadcompro.UUID as UUID,tblcontabilidadcompro.rfc as RFC,tblcontabilidadcompro.moneda as idMoneda,tblmonedassat.moneda  as Moneda,tblcontabilidadcompro.tipoCambio as Tipo_Cambio,tblcontabilidadcompro.monto as Monto  from tblcontabilidadcompro inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcompro.moneda where tblcontabilidadcompro.idPoliza=" + IDPoliza.ToString
        Dim DA3 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA3.Fill(DS3, "tblComprobCompro")
        'DS.WriteXmlSchema("tblComprobTrans.xml")
        tablaCompro = DS3.Tables("tblComprobCompro")

        'tabla compro Nac 2
        Dim DS4 As New DataSet
        Comm.CommandText = "select tblcontabilidadcompro2.id as id, tblcontabilidadcompro2.idPoliza as idPoliza, tblcontabilidadcompro2.idRenglon as idRenglon, tblcontabilidadcompro2.serie as Serie, tblcontabilidadcompro2.folio as Folio, tblcontabilidadcompro2.rfc as RFC, tblcontabilidadcompro2.monto as Monto, tblcontabilidadcompro2.moneda as idMoneda,tblmonedassat.moneda as Moneda ,tblcontabilidadcompro2.tipoCambio as Tipo_Cambio from tblcontabilidadcompro2 inner join tblmonedassat on tblcontabilidadcompro2.moneda=tblmonedassat.id where tblcontabilidadcompro2.idPoliza=" + IDPoliza.ToString
        Dim DA4 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA4.Fill(DS4, "tblComprobComproN2")
        'DS.WriteXmlSchema("tblComprobN2.xml")
        tablaComproNac2 = DS4.Tables("tblComprobComproN2")

        'tabla compro extranjeros
        Dim DS5 As New DataSet
        Comm.CommandText = "select tblcontabilidadcomproe.id as id, tblcontabilidadcomproe.idPoliza as idPoliza,tblcontabilidadcomproe.idRenglon as idRenglon, tblcontabilidadcomproe.numFactura as Num_Factura, tblcontabilidadcomproe.taxID as taxID, tblcontabilidadcomproe.monto as Monto, tblcontabilidadcomproe.moneda as idMoneda,tblmonedassat.moneda as Moneda ,tblcontabilidadcomproe.tipoCambio as Tipo_Cambio from tblcontabilidadcomproe inner join tblmonedassat on tblcontabilidadcomproe.moneda=tblmonedassat.id where tblcontabilidadcomproe.idPoliza=" + IDPoliza.ToString
        Dim DA5 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA5.Fill(DS5, "tblComprobComproE")
        'DS.WriteXmlSchema("tblComprobE.xml")
        tablaComproE = DS5.Tables("tblComprobComproE")

        'tablaOtros
        Dim DS6 As New DataSet
        Comm.CommandText = "select tblcontabilidadotros.id as id, tblcontabilidadotros.idPoliza as idPoliza,tblcontabilidadotros.idRenglon as idRenglon, tblcontabilidadotros.metodoPago as idMetodoPago,tblmetodospago.concepto as Metodo_Pago , tblcontabilidadotros.fecha as Fecha, tblcontabilidadotros.benef as Beneficiario, tblcontabilidadotros.rfc as RFC, tblcontabilidadotros.monto as Monto, tblcontabilidadotros.moneda as idMoneda,tblmonedassat.moneda as Moneda ,tblcontabilidadotros.tipoCambio as Tipo_Cambio from tblcontabilidadotros inner join tblmonedassat on tblcontabilidadotros.moneda=tblmonedassat.id inner join tblmetodospago on tblmetodospago.id=tblcontabilidadotros.metodoPago where tblcontabilidadotros.idPoliza=" + IDPoliza.ToString
        Dim DA6 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA6.Fill(DS6, "tblComproOtros")
        'DS.WriteXmlSchema("tblComproOtros.xml")
        tablaOtros = DS6.Tables("tblComproOtros")

        Dim DS As New DataSet
        Comm.CommandText = "select id,idCuenta,cuenta as Cuenta,descripcion,if(cargo=-999999999,'',cargo)as cargo,if(abono=-999999999,'',abono)as abono,factura,idproveedor,iva,concepto,esDIOT,valorActos,referencia,ivaret,ieps from tblpolizasdetalles where idPoliza=" + IDPoliza.ToString + " order by tblpolizasdetalles.orden"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpoliza")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblpoliza")

    End Function
    Public Function llenaDatosPolizaImpresion(ByVal pID As Integer)

        IDPoliza = pID
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblpolizas where id=" + IDPoliza.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            tipo2 = DReader("tipo")
            Numero = DReader("numero")
            fecha = DReader("fecha")
            concepto = DReader("concepto")
            beneficiario = DReader("beneficiario")
            Importe = DReader("importe")
        End If
        DReader.Close()
        If tipo2 = "E" Then
            tipo = 0
        End If
        If tipo2 = "I" Then
            tipo = 1
        End If
        If tipo2 = "D" Then
            tipo = 2
        End If
        If tipo2 = "O" Then
            tipo = 2
        End If
        Dim DS As New DataSet
        Comm.CommandText = " select tblpolizasdetalles.id, tblccontables.cuenta,tblccontables.N2,tblccontables.N3,tblccontables.N4,tblccontables.N5,tblpolizasdetalles.descripcion, if(tblpolizasdetalles.cargo=-999999999,'',tblpolizasdetalles.cargo)as cargo,if(tblpolizasdetalles.abono=-999999999,'',tblpolizasdetalles.abono)as abono, tblccontables.nivel,IF(tblccontables.Nivel=1,LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),IF(tblccontables.Nivel=2,concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0')),IF(tblccontables.Nivel=3,concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0')),IF(tblccontables.Nivel=4,concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),' ',LPAD(tblccontables.N4," + NNiv4.ToString + ",'0')),IF(tblccontables.Nivel=5,concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),' ',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),' ',LPAD(tblccontables.N4," + NNiv4.ToString + ",'0'),' ',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0')),''))))) as cpncat_cuenta from tblpolizasdetalles INNER JOIN tblccontables ON tblpolizasdetalles.idCuenta=tblccontables.idCContable where tblpolizasdetalles.idPoliza=" + IDPoliza.ToString + " order by tblpolizasdetalles.orden"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpoliza")
        Return DS.Tables("tblpoliza")
    End Function
    Public Sub sacarCuentas(ByVal pIdPoliza As Integer, ByVal pN1 As String)
        Comm.CommandText = " select sum(tblpolizasdetalles.cargo) from tblpolizasdetalles inner join tblccontables on tblpolizasdetalles.idCuenta= tblccontables.idCContable where tblpolizasdetalles.idPoliza=" + pIdPoliza.ToString + " and tblpolizasdetalles.cargo IS NOT NULL and tblccontables.Cuenta='" + pN1 + "'"
        totalCargo = Comm.ExecuteScalar
        Comm.CommandText = " select sum(tblpolizasdetalles.abono) from tblpolizasdetalles inner join tblccontables on tblpolizasdetalles.idCuenta= tblccontables.idCContable where tblpolizasdetalles.idPoliza=" + pIdPoliza.ToString + " and tblpolizasdetalles.abono IS NOT NULL and tblccontables.Cuenta='" + pN1 + "'"
        totalAbono = Comm.ExecuteScalar
    End Sub
    Public Function contadorCuentas(ByVal pCuenta As String, ByVal pPoliza As Integer)
        Comm.CommandText = " select count(tblpolizasdetalles.id) from tblpolizasdetalles inner join tblccontables on tblpolizasdetalles.idCuenta=tblccontables.idCContable where tblccontables.Cuenta='" + pCuenta + "' and tblpolizasdetalles.idPoliza=" + pPoliza.ToString
        Return Comm.ExecuteScalar
    End Function


    Public Sub modificarPoliza(ByVal pID As Integer, ByVal ptipo As String, ByVal pNumer As Integer, ByVal pFecha As String, ByVal pConcepto As String, ByVal pBeneficiario As String, ByVal pImporte As String, ByVal pClas As Integer, pCantCheque As Double, pLeyenda As Byte)
        IDPoliza = pID
        Comm.CommandText = "update tblpolizas set tipo='" + ptipo + "',numero=" + pNumer.ToString + ",fecha='" + pFecha + "',concepto='" + Replace(pConcepto, "'", "''") + "',beneficiario='" + Replace(pBeneficiario, "'", "''") + "',importe='" + pImporte.ToString + "', clasificacion=" + pClas.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "',estado=3,cantidadcheque=" + pCantCheque.ToString + ",leyendach=" + pLeyenda.ToString + " where id=" + pID.ToString
        Comm.ExecuteNonQuery()

    End Sub
    Public Sub modificarImportePoliza(ByVal pID As Integer, ByVal pImporte As String)
        IDPoliza = pID
        Comm.CommandText = "update tblpolizas set importe='" + pImporte.ToString + "' where id=" + pID.ToString
        Comm.ExecuteNonQuery()

    End Sub
    Public Sub sumaImportePoliza(ByVal pID As Integer)
        Dim suma As Double
        Comm.CommandText = "select sum(if(cargo=-999999999,0,cargo)) from tblPolizasDetalles where idPoliza=" + pID.ToString
        suma = Comm.ExecuteScalar
    End Sub
    Public Sub eliminarDetalles()

        Comm.CommandText = "delete from tblpolizasdetalles where idPoliza=" + IDPoliza.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarDetalle(pIdRenglon As Integer)
        Comm.CommandText = "delete from tblpolizasdetalles where id=" + pIdRenglon.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub eliminarPoliza(ByVal pIdPoliza As Integer)
        IDPoliza = pIdPoliza
        Comm.CommandText = "delete from tblpolizas where id = " + pIdPoliza.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ultimoNivel(ByVal pN1 As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pNivel As Integer) As Integer
        Dim cont As Integer
        Comm.CommandText = "select count(idCContable) from tblccontables where cuenta='" + pN1.ToString + "'"
        If pNivel >= 2 Then
            Comm.CommandText += " and N2='" + pN2.ToString + "'"
        End If
        If pNivel >= 3 Then
            Comm.CommandText += " and N3='" + pN3.ToString + "'"
        End If
        If pNivel >= 4 Then
            Comm.CommandText += " and N4='" + pN4.ToString + "'"
        End If
        If pNivel >= 5 Then
            Comm.CommandText += " and N5='" + pN5.ToString + "'"
        End If
        Comm.CommandText += " and nivel=" + (pNivel + 1).ToString
        cont = Comm.ExecuteScalar
        Return cont
    End Function
    Public Function esDIOT(ByVal pIDCUENTa As Integer)
        Dim aux As Integer = 0
        Comm.CommandText = "select DIOT from tblccontables where idCContable=" + pIDCUENTa.ToString + ""
        aux = Comm.ExecuteScalar
        If aux = 1 Then
            Return True
        Else
            Return False
        End If
    End Function














    '***********************CONFIGURACIÓN**********************

    Public Sub llenaDatosConfig()

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcontabilidadconf where id=1"
        DReader = Comm.ExecuteReader

        If DReader.Read() Then
            NNiv1 = DReader("nNiv1")
            NNiv2 = DReader("nNiv2")
            NNiv3 = DReader("nNiv3")
            NNiv4 = DReader("nNiv4")
            NNiv5 = DReader("nNiv5")
            anio = DReader("anio")
            activoC1 = DReader("activoC1")
            activoC2 = DReader("activoC2")
            activoF1 = DReader("activoF1")
            activoF2 = DReader("activoF2")
            activoD1 = DReader("activoD1")
            activoD2 = DReader("activoD2")
            activoO1 = DReader("activoO1")
            activoO2 = DReader("activoO2")
            pasivoC1 = DReader("pasivoC1")
            pasivoC2 = DReader("pasivoC2")
            pasivoF1 = DReader("pasivoF1")
            pasivoF2 = DReader("pasivoF2")
            pasivoD1 = DReader("pasivoD1")
            pasivoD2 = DReader("pasivoD2")
            capital1 = DReader("capital1")
            capital2 = DReader("capital2")
            resultados = DReader("resultados")
            ordenA1 = DReader("ordenA1")
            ordenA2 = DReader("ordenA2")
            ordenVisible = DReader("ordenVisible")
            ordenD1 = DReader("ordenD1")
            ordenD2 = DReader("ordenD2")
            ingresos1 = DReader("ingresos1")
            ingresos2 = DReader("ingresos2")
            egresos1 = DReader("egresos1")
            egresos2 = DReader("egresos2")
            rutaUUID = DReader("rutaUUID")
            PreguntarCuadrar = DReader("guardarcuadrada")
            FechaTRabajo = DReader("fechatrabajo")
            ActivarFechaTrabajo = DReader("activarfecha")
            capital3 = DReader("capital3")
            capital4 = DReader("capital4")
            SoloIvaEnDiot = DReader("soloivadiot")
            RutaXMLIngresos = DReader("rutaxmlingresos")
        End If

        DReader.Close()
    End Sub
    Public Function contadorUUID(ByVal pUUID As String)
        Dim conta As Integer = 0
        Comm.CommandText = "select count(id) from tblcontabilidadcompro where UUID='" + pUUID + "';"
        conta = Comm.ExecuteScalar()
        If conta <> 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Sub modificarConf(ByVal pNniv1 As String, ByVal pNniv2 As String, ByVal pNniv3 As String, ByVal pNniv4 As String, ByVal pNniv5 As String, ByVal pactivoC1 As String, ByVal pactivoC2 As String, ByVal pactivoF1 As String, ByVal pactivoF2 As String, ByVal pactivoD1 As String, ByVal pactivoD2 As String, ByVal pactivoO1 As String, ByVal pactivoO2 As String, ByVal ppasivoC1 As String, ByVal ppasivoC2 As String, ByVal ppasivoF1 As String, ByVal ppasivoF2 As String, ByVal ppasivoD1 As String, ByVal ppasivoD2 As String, ByVal pcapital1 As String, ByVal pcapital2 As String, ByVal presultados As String, ByVal pordenA1 As String, ByVal pordenA2 As String, ByVal pordenD1 As String, ByVal pordenD2 As String, ByVal pordenVisible As Integer, ByVal pingresos1 As String, ByVal pingresos2 As String, ByVal pegresos1 As String, ByVal pegresos2 As String, ByVal pRutaUUID As String, pPreguntaralCuadrar As Byte, pFechaTrabajo As String, pActivarFecha As Byte, pCapital3 As String, pCapital4 As String, pRutaXMLIngresos As String, pSoloIva As Byte)
        Comm.CommandText = "update tblcontabilidadconf set nNiv1=" + pNniv1 + ",nNiv2=" + pNniv2 + ",nNiv3=" + pNniv3 + ",nNiv4=" + pNniv4 + ",nNiv5=" + pNniv5 + ", activoC1='" + pactivoC1 + "',activoC2='" + pactivoC2 + "',activoF1='" + pactivoF1 + "',activoF2='" + pactivoF2 + "',activoD1='" + pactivoD1 + "',activoD2='" + pactivoD2 + "',activoO1='" + pactivoO1 + "',activoO2='" + pactivoO2 + "',pasivoC1='" + ppasivoC1 + "',pasivoC2='" + ppasivoC2 + "',pasivoF1='" + ppasivoF1 + "',pasivoF2='" + ppasivoF2 + "',pasivoD1='" + ppasivoD1 + "',pasivoD2='" + ppasivoD2 + "',capital1='" + pcapital1 + "',capital2='" + pcapital2 + "',resultados='" + presultados + "',ordenA1='" + pordenA1 + "',ordenA2='" + pordenA2 + "',ordenD1='" + pordenD1 + "',ordenD2='" + pordenD2 + "',ordenVisible=" + pordenVisible.ToString + ",ingresos1='" + pingresos1 + "',ingresos2='" + pingresos2 + "',egresos1='" + pegresos1 + "',egresos2='" + pegresos2 + "', rutaUUID='" + Replace(pRutaUUID.Replace("'", "''"), "\", "\\") + "',guardarcuadrada=" + pPreguntaralCuadrar.ToString + ",fechatrabajo='" + pFechaTrabajo + "',activarfecha=" + pActivarFecha.ToString + ",capital3='" + pCapital3 + "',capital4='" + pCapital4 + "',rutaxmlingresos='" + Replace(pRutaXMLIngresos.Replace("'", "''"), "\", "\\") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH.mm:ss") + "',soloivadiot=" + pSoloIva.ToString + " where id=1"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function modificarPeriodo(ByVal periodo As String, ByVal pass As String)
        Dim per As String
        If pass <> "" Then
            Comm.CommandText = "select passPeriodo from tblcontabilidadconf where id=1"
            per = Comm.ExecuteScalar
            If per = pass Then
                Comm.CommandText = "update tblcontabilidadconf set anio='" + periodo + "' where id=1"
                Comm.ExecuteNonQuery()
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function
    Public Function buscarPeriodo() As String
        Comm.CommandText = "select anio from tblcontabilidadconf where id=1"
        Return Comm.ExecuteScalar
    End Function
    '-*************REPORTES************
    Public Function reporteDIOT(ByVal pfechaI As String, ByVal pFechaF As String, ByVal pProveedor As String, ByVal ivasCero As Boolean)


        Dim DS As New DataSet
        Comm.CommandText = "select tblproveedores.RFC, tblproveedores.nombre, tblpolizasdetalles.iva, concat(tblpolizas.tipo,'',tblpolizas.numero),tblpolizas.fecha,tblpolizasdetalles.factura,if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo) as cargo, if(tblpolizasdetalles.iva='0' or tblpolizasdetalles.iva='E',tblpolizasdetalles.valorActos,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.iva,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.iva)) as ValorActos  from tblpolizasdetalles INNER JOIN tblproveedores ON tblpolizasdetalles.idProveedor=tblproveedores.idProveedor inner join tblpolizas ON tblpolizasdetalles.idPoliza=tblpolizas.id where tblpolizasdetalles.esDIOT=1 and tblproveedores.RFC<>''"
        Comm.CommandText += " and tblpolizas.fecha>='" + pfechaI.ToString + "' and tblpolizas.fecha<='" + pFechaF.ToString + "'"
        If pProveedor <> "" Then
            Comm.CommandText += " and tblproveedores.clave=" + pProveedor.ToString
        End If
        If ivasCero Then
            Comm.CommandText += " and tblpolizasdetalles.iva<>'0' and tblpolizasdetalles.iva<>'E'"
        End If
        Comm.CommandText += " and tblpolizasdetalles.iva<>'' and tblpolizas.tipo<>'A' order by tblproveedores.rfc,tblpolizas.fecha,tblpolizas.tipo,tblpolizas.numero"

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblDIOT")
        ' DS.WriteXmlSchema("tblDIOT.xml")
        Return DS.Tables("tblDIOT")
    End Function
    Public Function ReporteBalanza(pFechaInicial As String, pFechaFinal As String, pSoloMayor As Boolean, pTodosNiveles As Boolean) As DataView
        Dim DS2 As New DataSet
        Dim anioint As String = pFechaFinal.Substring(0, 4)
        Comm.CommandTimeout = 300000
        Comm.CommandText = "delete from tblcontabilidadbalanzarep"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadbalanzarep(cuenta,descripcion,agrupador,naturaleza,cargoini,abonoini,cargo,abono,nivel,dep) select concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),'')) as Cuenta," +
            "tblccontables.descripcion,tblagrupadorcuentas.codigo as agrupador,tblccontables.Naturaleza," +
            "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan1=tblccontables.idccontable and ((tblpolizas.fecha='" + anioint + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anioint + "/01/01' and tblpolizas.fecha<'" + pFechaInicial + "' and tblpolizas.tipo<>'A'))),0) cargoini," +
            "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan1=tblccontables.idccontable and ((tblpolizas.fecha='" + anioint + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anioint + "/01/01' and tblpolizas.fecha<'" + pFechaInicial + "' and tblpolizas.tipo<>'A'))),0) abonoini," +
            "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan1=tblccontables.idccontable and (tblpolizas.fecha>='" + pFechaInicial + "' and tblpolizas.fecha<='" + pFechaFinal + "' and tblpolizas.tipo<>'A')),0) cargo," +
            "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan1=tblccontables.idccontable and (tblpolizas.fecha>='" + pFechaInicial + "' and tblpolizas.fecha<='" + pFechaFinal + "' and tblpolizas.tipo<>'A')),0) abono, " +
            "1,tblccontables.depreciacion from tblccontables inner join tblagrupadorcuentas on tblccontables.idcontable=tblagrupadorcuentas.id where tblccontables.nivel=1 and (tblccontables.descontinuada like '%" + anioint + "%')=False"
        Comm.ExecuteNonQuery()
        If pSoloMayor = False Or pTodosNiveles = True Then
            Comm.CommandText = "insert into tblcontabilidadbalanzarep(cuenta,descripcion,agrupador,naturaleza,cargoini,abonoini,cargo,abono,nivel,dep) select concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),'')) as Cuenta," +
                "tblccontables.descripcion,tblagrupadorcuentas.codigo as agrupador,tblccontables.Naturaleza," +
                "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan2=tblccontables.idccontable and ((tblpolizas.fecha='" + anioint + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anioint + "/01/01' and tblpolizas.fecha<'" + pFechaInicial + "' and tblpolizas.tipo<>'A'))),0) cargoini," +
                "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan2=tblccontables.idccontable and ((tblpolizas.fecha='" + anioint + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anioint + "/01/01' and tblpolizas.fecha<'" + pFechaInicial + "' and tblpolizas.tipo<>'A'))),0) abonoini," +
                "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan2=tblccontables.idccontable and (tblpolizas.fecha>='" + pFechaInicial + "' and tblpolizas.fecha<='" + pFechaFinal + "' and tblpolizas.tipo<>'A')),0) cargo," +
                "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan2=tblccontables.idccontable and (tblpolizas.fecha>='" + pFechaInicial + "' and tblpolizas.fecha<='" + pFechaFinal + "' and tblpolizas.tipo<>'A')),0) abono, " +
                "2,tblccontables.depreciacion from tblccontables inner join tblagrupadorcuentas on tblccontables.idcontable=tblagrupadorcuentas.id where tblccontables.nivel=2 and (tblccontables.descontinuada like '%" + anioint + "%')=False"
            Comm.ExecuteNonQuery()
        End If
        If pTodosNiveles Then
            Comm.CommandText = "insert into tblcontabilidadbalanzarep(cuenta,descripcion,agrupador,naturaleza,cargoini,abonoini,cargo,abono,nivel,dep) select concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),' ',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),'')) as Cuenta," +
                "tblccontables.descripcion,tblagrupadorcuentas.codigo as agrupador,tblccontables.Naturaleza," +
                "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan3=tblccontables.idccontable and ((tblpolizas.fecha='" + anioint + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anioint + "/01/01' and tblpolizas.fecha<'" + pFechaInicial + "' and tblpolizas.tipo<>'A'))),0) cargoini," +
                "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan3=tblccontables.idccontable and ((tblpolizas.fecha='" + anioint + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anioint + "/01/01' and tblpolizas.fecha<'" + pFechaInicial + "' and tblpolizas.tipo<>'A'))),0) abonoini," +
                "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan3=tblccontables.idccontable and (tblpolizas.fecha>='" + pFechaInicial + "' and tblpolizas.fecha<='" + pFechaFinal + "' and tblpolizas.tipo<>'A')),0) cargo," +
                "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan3=tblccontables.idccontable and (tblpolizas.fecha>='" + pFechaInicial + "' and tblpolizas.fecha<='" + pFechaFinal + "' and tblpolizas.tipo<>'A')),0) abono, " +
                "3,tblccontables.depreciacion from tblccontables inner join tblagrupadorcuentas on tblccontables.idcontable=tblagrupadorcuentas.id where tblccontables.nivel=3 and (tblccontables.descontinuada like '%" + anioint + "%')=False"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "insert into tblcontabilidadbalanzarep(cuenta,descripcion,agrupador,naturaleza,cargoini,abonoini,cargo,abono,nivel,dep) select concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),' ',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),''),' ',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + NNiv4.ToString + ",'0'),'')) as Cuenta," +
                "tblccontables.descripcion,tblagrupadorcuentas.codigo as agrupador,tblccontables.Naturaleza," +
                "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan4=tblccontables.idccontable and ((tblpolizas.fecha='" + anioint + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anioint + "/01/01' and tblpolizas.fecha<'" + pFechaInicial + "' and tblpolizas.tipo<>'A'))),0) cargoini," +
                "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan4=tblccontables.idccontable and ((tblpolizas.fecha='" + anioint + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anioint + "/01/01' and tblpolizas.fecha<'" + pFechaInicial + "' and tblpolizas.tipo<>'A'))),0) abonoini," +
                "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan4=tblccontables.idccontable and (tblpolizas.fecha>='" + pFechaInicial + "' and tblpolizas.fecha<='" + pFechaFinal + "' and tblpolizas.tipo<>'A')),0) cargo," +
                "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan4=tblccontables.idccontable and (tblpolizas.fecha>='" + pFechaInicial + "' and tblpolizas.fecha<='" + pFechaFinal + "' and tblpolizas.tipo<>'A')),0) abono, " +
                "4,tblccontables.depreciacion from tblccontables inner join tblagrupadorcuentas on tblccontables.idcontable=tblagrupadorcuentas.id where tblccontables.nivel=4 and (tblccontables.descontinuada like '%" + anioint + "%')=False"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "insert into tblcontabilidadbalanzarep(cuenta,descripcion,agrupador,naturaleza,cargoini,abonoini,cargo,abono,nivel,dep) select concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),' ',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),''),' ',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + NNiv4.ToString + ",'0'),''),' ',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),'')) as Cuenta," +
                "tblccontables.descripcion,tblagrupadorcuentas.codigo as agrupador,tblccontables.Naturaleza," +
                "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan5=tblccontables.idccontable and ((tblpolizas.fecha='" + anioint + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anioint + "/01/01' and tblpolizas.fecha<'" + pFechaInicial + "' and tblpolizas.tipo<>'A'))),0) cargoini," +
                "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan5=tblccontables.idccontable and ((tblpolizas.fecha='" + anioint + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anioint + "/01/01' and tblpolizas.fecha<'" + pFechaInicial + "' and tblpolizas.tipo<>'A'))),0) abonoini," +
                "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan5=tblccontables.idccontable and (tblpolizas.fecha>='" + pFechaInicial + "' and tblpolizas.fecha<='" + pFechaFinal + "' and tblpolizas.tipo<>'A')),0) cargo," +
                "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and tblpolizasdetalles.idcuentan5=tblccontables.idccontable and (tblpolizas.fecha>='" + pFechaInicial + "' and tblpolizas.fecha<='" + pFechaFinal + "' and tblpolizas.tipo<>'A')),0) abono, " +
                "5,tblccontables.depreciacion from tblccontables inner join tblagrupadorcuentas on tblccontables.idcontable=tblagrupadorcuentas.id where tblccontables.nivel=5 and (tblccontables.descontinuada like '%" + anioint + "%')=False"
            Comm.ExecuteNonQuery()
        End If
        '        if ({tblBalanza.Naturaleza}='D') then
        '{tblBalanza.SaldoInicMayor}+{tblBalanza.saldoCMay}-{tblBalanza.saldoAMay}
        '        Else
        '{tblBalanza.SaldoInicMayor}-{tblBalanza.saldoCMay}+{tblBalanza.saldoAMay}
        Comm.CommandText = "select cuenta,descripcion,agrupador,if(naturaleza=1 or dep=1,'A','D') naturalezastr,if(naturaleza=0,cargoini-abonoini,abonoini-cargoini) saldoini,cargo,abono,if(naturaleza=0,cargoini-abonoini+cargo-abono,abonoini-cargoini+abono-cargo) saldofin,nivel from tblcontabilidadbalanzarep order by cuenta"
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblBalanza")
        'DS2.WriteXmlSchema("repbalanza.xml")
        Return DS2.Tables("tblBalanza").DefaultView
    End Function
    Public Function reporteBalanzaXML(ByVal pfechaInicial As String, ByVal pFechaFinal As String)
        Dim DS2 As New DataSet
        ' Dim pHasta As Date = Date.Parse("01/" + pMes + "/" + anio)
        Dim anioint As String = pFechaFinal.Substring(0, 4)
        todosLosSaldos(pfechaInicial, pFechaFinal, "", "")
        'SaldosIniciales(pfechaInicial, Date.Parse(pfechaInicial).Month.ToString("00"))
        'Comm.CommandText = "Select (select concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),'')) from tblccontables where idCContable=detalles.idCuenta) as Cuenta"
        'Comm.CommandText += " ,(select descripcion from tblccontables where concat(Cuenta,N2)=(select concat(Cuenta,N2) from tblccontables where idCContable=detalles.idCuenta)) as descripcion "
        'Comm.CommandText += " ,(select codigo from tblagrupadorcuentas where id=(select idContable from tblccontables where concat(Cuenta,N2)=(select concat(Cuenta,N2) from tblccontables where idCContable=detalles.idCuenta))) as agrupador"
        'Comm.CommandText += " ,(select if(tblccontables.Naturaleza=0,'D','A') from tblccontables where tblccontables.idCContable=detalles.idCuenta) as Naturaleza "
        'Comm.CommandText += ",ifnull((select ifnull(saldoI,0) from tblcontabilidadsaldosi where concat(tblcontabilidadsaldosi.Cuenta,tblcontabilidadsaldosi.N1)=(select concat(Cuenta,N2) from tblccontables where idCContable=detalles.idCuenta) limit 1),0) as saldoInicial"
        'Comm.CommandText += ", (select if(p.tipo<>'A' and p.fecha>='" + anio + "/" + pMes + "/01',ifnull(if(cargo=-999999999,0,cargo),0),0) from tblpolizasdetalles as d inner join tblPolizas as p on d.idPoliza=p.id where d.id=detalles.id ) as cargo, (select if(p2.tipo<>'A' and p2.fecha>='" + anio + "/" + pMes + "/01',ifnull(if (abono=-999999999,0,abono),0),0) from tblpolizasdetalles as d2 inner join tblPolizas as p2 on d2.idPoliza=p2.id where d2.id=detalles.id )as abono"
        'Comm.CommandText += ", (select tblccontables.cuenta from tblccontables where tblccontables.idCContable=detalles.idCuenta) as cuenta1"
        'Comm.CommandText += ", (select ifnull(tblccontables.N2,'') from tblccontables where tblccontables.idCContable=detalles.idCuenta) as cuenta2"
        'Comm.CommandText += " ,(select cco.descripcion from tblccontables as cco where cco.Cuenta=(select cc.Cuenta from tblccontables as cc where cc.idCContable=detalles.idCuenta ) and cco.Nivel=1) as descripcionMAyor "
        'Comm.CommandText += " ,(select codigo from tblagrupadorcuentas where id=(select idContable from tblccontables where Cuenta=(select Cuenta from tblccontables where idCContable=detalles.idCuenta) and nivel=1)) as agruMayor "
        'Comm.CommandText += " ,(select sum(saldoI) from tblcontabilidadsaldosi where Cuenta=(select Cuenta from tblccontables where idCContable=detalles.idCuenta)) as SaldoInicMayor "
        'Comm.CommandText += ", (select ifnull(sum(if(d.cargo=-999999999,0,d.cargo)),0) from tblpolizasdetalles as d inner join tblccontables as dd on d.idCuenta=dd.idCContable inner join tblpolizas as pp on d.idPoliza=pp.id  where dd.Cuenta=(select Cuenta from tblccontables where idCContable=detalles.idCuenta) and pp.tipo<>'A' and pp.fecha<='" + anio + "/" + pMes + "/31' and pp.fecha>='" + anio + "/" + pMes + "/01') as saldoCMay"
        'Comm.CommandText += ", (select ifnull(sum(if(d.abono=-999999999,0,d.abono)),0) from tblpolizasdetalles as d inner join tblccontables as dd on d.idCuenta=dd.idCContable inner join tblpolizas as pp on d.idPoliza=pp.id  where dd.Cuenta=(select Cuenta from tblccontables where idCContable=detalles.idCuenta) and pp.tipo<>'A' and pp.fecha<='" + anio + "/" + pMes + "/31' and pp.fecha>='" + anio + "/" + pMes + "/01') as saldoAMay"
        'Comm.CommandText += " from tblpolizasdetalles as detalles inner join tblPolizas as poliza on detalles.idPoliza=poliza.id "
        'Comm.CommandText += " where  poliza.tipo<>'O'  and poliza.fecha<='" + anio + "/" + pMes + "/31'  and poliza.fecha>='" + anio + "/01/01' order by Cuenta"
        'cuenta,descripcion,agrupador,naturaleza,saldoinicial,cargo,abono,
        

        Comm.CommandText = "select concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),'')) as Cuenta," + _
   "tblccontables.descripcion," + _
   "tblagrupadorcuentas.codigo as agrupador," + _
  "if(tblccontables.Naturaleza=0,'D','A') as Naturaleza," + _
  "round(ifnull((select saldoI from tblcontabilidadsaldosi where concat(tblcontabilidadsaldosi.Cuenta,tblcontabilidadsaldosi.N1)=(select concat(Cuenta,N2) from tblccontables where idCContable=tblpolizasdetalles.idCuenta) limit 1),0),2) as saldoInicial," + _
  "round(if(tblpolizas.tipo<>'A' and tblpolizas.fecha>='" + pfechaInicial + "',ifnull(if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo),0),0),2) as cargo," + _
  "round(if(tblpolizas.tipo<>'A' and tblpolizas.fecha>='" + pfechaInicial + "',ifnull(if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono),0),0),2) as abono," + _
  "tblccontables.cuenta as cuenta1," + _
  "tblccontables.N2 as cuenta2," + _
  "tblccontables.descripcion as descripcionMAyor," + _
  "'' as agruMayor," + _
  "if(tblccontables.nivel=1,(select sum(round(saldoI,2)) from tblcontabilidadsaldosi where Cuenta=tblccontables.Cuenta),0)  as SaldoInicMayor," + _
  "if(tblccontables.nivel=1,(select ifnull(sum(if(d.cargo=-999999999,0,round(d.cargo,2))),0) from tblpolizasdetalles as d inner join tblccontables as dd on d.idCuenta=dd.idCContable inner join tblpolizas as pp on d.idPoliza=pp.id  where dd.Cuenta=tblccontables.cuenta and pp.tipo<>'A' and pp.fecha<='" + pFechaFinal + "' and pp.fecha>='" + pfechaInicial + "'),0) as saldoCMay," + _
  "if(tblccontables.nivel=1,(select ifnull(sum(if(d.abono=-999999999,0,round(d.abono,2))),0) from tblpolizasdetalles as d inner join tblccontables as dd on d.idCuenta=dd.idCContable inner join tblpolizas as pp on d.idPoliza=pp.id  where dd.Cuenta=tblccontables.cuenta and pp.tipo<>'A' and pp.fecha<='" + pFechaFinal + "' and pp.fecha>='" + pfechaInicial + "'),0) as saldoAMay" + _
  " from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id and tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaFinal + "' and tblpolizas.tipo<>'O' right outer join tblccontables inner join tblagrupadorcuentas on tblccontables.idcontable=tblagrupadorcuentas.id  on tblpolizasdetalles.idcuenta=tblccontables.idccontable where (tblccontables.descontinuada like '%" + anio + "%')=False"
        'If pSoloMayor Then
        '    Comm.CommandText += " and tblccontables.n2='' and tblccontables.n3='' and tblccontables.n4='' and tblccontables.n5=''"
        'End If
        Comm.CommandText += " order by Cuenta;"
        Comm.CommandTimeout = 300000
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblBalanza")
        Return DS2.Tables("tblBalanza")

    End Function
    Public Function reporteBalanzaXML2(ByVal pMes As String, pEs13 As Boolean)
        Dim DS2 As New DataSet
        Dim pHasta As Date = Date.Parse("01/" + pMes + "/" + anio)
        SaldosIniciales(pHasta.ToString("yyyy/MM/dd"), pMes)
        Comm.CommandText = "Select (select concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),'',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),''),'',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),'')) from tblccontables where idCContable=detalles.idCuenta) as Cuenta"
        'Comm.CommandText += " ,(select descripcion from tblccontables where  idCContable=detalles.idCuenta) as descripcion "
        ' Comm.CommandText += " ,(select codigo from tblagrupadorcuentas where id=(select idContable from tblccontables where  idCContable=detalles.idCuenta)) as agrupador"
        Comm.CommandText += " ,(select if(tblccontables.Naturaleza=0,'D','A') from tblccontables where tblccontables.idCContable=detalles.idCuenta) as Naturaleza "

        Comm.CommandText += ",ifnull((select ifnull(saldoI,0) from tblcontabilidadsaldosi where tblcontabilidadsaldosi.idCContable=detalles.idCuenta),0) as saldoInicial"
        If pEs13 = False Then
            Comm.CommandText += ", (select if(p.tipo<>'A' and p.fecha>='" + anio + "/" + pMes + "/01',ifnull(if(cargo=-999999999,0,cargo),0),0) from tblpolizasdetalles as d inner join tblPolizas as p on d.idPoliza=p.id where d.id=detalles.id ) as cargo, (select if(p2.tipo<>'A' and p2.fecha>='" + anio + "/" + pMes + "/01',ifnull(if (abono=-999999999,0,abono),0),0) from tblpolizasdetalles as d2 inner join tblPolizas as p2 on d2.idPoliza=p2.id where d2.id=detalles.id )as abono"
        Else
            Comm.CommandText += ", (select if(p.tipo<>'A' and p.fecha>='" + anio + "/01/01',ifnull(if(cargo=-999999999,0,cargo),0),0) from tblpolizasdetalles as d inner join tblPolizas as p on d.idPoliza=p.id where d.id=detalles.id ) as cargo, (select if(p2.tipo<>'A' and p2.fecha>='" + anio + "/01/01',ifnull(if (abono=-999999999,0,abono),0),0) from tblpolizasdetalles as d2 inner join tblPolizas as p2 on d2.idPoliza=p2.id where d2.id=detalles.id )as abono"
        End If
        ' Comm.CommandText += ", (select tblccontables.cuenta from tblccontables where tblccontables.idCContable=detalles.idCuenta) as cuenta1"
        ' Comm.CommandText += ", (select ifnull(tblccontables.N2,'') from tblccontables where tblccontables.idCContable=detalles.idCuenta) as cuenta2"
        ' Comm.CommandText += " ,(select cco.descripcion from tblccontables as cco where cco.Cuenta=(select cc.Cuenta from tblccontables as cc where cc.idCContable=detalles.idCuenta ) and cco.Nivel=1) as descripcionMAyor "
        ' Comm.CommandText += " ,(select codigo from tblagrupadorcuentas where id=(select idContable from tblccontables where Cuenta=(select Cuenta from tblccontables where idCContable=detalles.idCuenta) and nivel=1)) as agruMayor "
        ' Comm.CommandText += " ,(select sum(saldoI) from tblcontabilidadsaldosi where Cuenta=(select Cuenta from tblccontables where idCContable=detalles.idCuenta)) as SaldoInicMayor "
        ' Comm.CommandText += ", (select ifnull(sum(if(d.cargo=-999999999,0,d.cargo)),0) from tblpolizasdetalles as d inner join tblccontables as dd on d.idCuenta=dd.idCContable inner join tblpolizas as pp on d.idPoliza=pp.id  where dd.Cuenta=(select Cuenta from tblccontables where idCContable=detalles.idCuenta) and pp.tipo<>'A' and pp.fecha<='" + anio + "/" + pMes + "/31' and pp.fecha>='" + anio + "/" + pMes + "/01') as saldoCMay"
        ' Comm.CommandText += ", (select ifnull(sum(if(d.abono=-999999999,0,d.abono)),0) from tblpolizasdetalles as d inner join tblccontables as dd on d.idCuenta=dd.idCContable inner join tblpolizas as pp on d.idPoliza=pp.id  where dd.Cuenta=(select Cuenta from tblccontables where idCContable=detalles.idCuenta) and pp.tipo<>'A' and pp.fecha<='" + anio + "/" + pMes + "/31' and pp.fecha>='" + anio + "/" + pMes + "/01') as saldoAMay"
        Comm.CommandText += " from tblpolizasdetalles as detalles inner join tblPolizas as poliza on detalles.idPoliza=poliza.id "
        If pEs13 = False Then
            Comm.CommandText += " where   poliza.fecha<='" + anio + "/" + pMes + "/31'  and poliza.fecha>='" + anio + "/01/01' order by Cuenta"
        Else
            Comm.CommandText += " where   poliza.fecha<='" + anio + "/12/31'  and poliza.fecha>='" + anio + "/01/01' order by Cuenta"
        End If
        Comm.CommandTimeout = 30000000
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblBalanza")

        Return DS2.Tables("tblBalanza")

    End Function
    Public Sub SaldosIniciales(ByVal pHasta As String, ByVal pMes As String)
        'Dim DS2 As New DataSet
        'Dim tb As DataTable
        Comm.CommandText = "Delete from tblcontabilidadsaldosi "
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadsaldosi(idCContable,Cuenta,N1,naturaleza,saldoI) select tblccontables.idccontable,tblccontables.Cuenta,tblccontables.N2,tblccontables.Naturaleza,round(ifnull(sum(if(cargo=-999999999,0,round(cargo,2))),0)-ifnull(sum(if(abono=-999999999,0,round(abono,2))),0),2) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id"
        If pMes = "01" Then
            Comm.CommandText += " and tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<='" + anio.ToString + "/12/31' and tblPolizas.tipo='A'"
        Else
            Comm.CommandText += " and tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<'" + pHasta + "'"
        End If
        Comm.CommandText += " right outer join tblccontables on tblpolizasdetalles.idcuenta=tblccontables.idccontable where tblccontables.naturaleza=0  group by tblccontables.Cuenta,tblccontables.N2;"

        Comm.CommandText += "insert into tblcontabilidadsaldosi(idCContable,Cuenta,N1,naturaleza,saldoI) select tblccontables.idccontable,tblccontables.Cuenta,tblccontables.N2,tblccontables.Naturaleza,round(ifnull(sum(if(abono=-999999999,0,round(abono,2))),0)-ifnull(sum(if(cargo=-999999999,0,round(cargo,2))),0),2) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id"
        If pMes = "01" Then
            Comm.CommandText += " and tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<='" + anio.ToString + "/12/31' and tblPolizas.tipo='A'"
        Else
            Comm.CommandText += " and tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<'" + pHasta + "'"
        End If
        Comm.CommandText += " right outer join tblccontables on tblpolizasdetalles.idcuenta=tblccontables.idccontable where tblccontables.naturaleza=1  group by tblccontables.Cuenta,tblccontables.N2;"

        Comm.ExecuteNonQuery()

    End Sub
    Public Sub SaldosInicialesCierre(ByVal pHasta As String, ByVal pMes As String)
        'Dim DS2 As New DataSet
        'Dim tb As DataTable
        Comm.CommandText = "Delete from tblcontabilidadsaldosi "
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadsaldosi(idCContable,Cuenta,N1,naturaleza,saldoI) select tblccontables.idccontable,tblccontables.Cuenta,tblccontables.N2,tblccontables.Naturaleza,round(ifnull(sum(if(cargo=-999999999,0,round(cargo,2))),0)-ifnull(sum(if(abono=-999999999,0,round(abono,2))),0),2) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id and tblpolizas.estado=3"
        If pMes = "01" Then
            Comm.CommandText += " and tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<='" + anio.ToString + "/12/31' and tblPolizas.tipo='A'"
        Else
            Comm.CommandText += " and tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<'" + pHasta + "'"
        End If
        Comm.CommandText += " inner join tblccontables on tblpolizasdetalles.idcuenta=tblccontables.idccontable where tblccontables.naturaleza=0 group by tblpolizasdetalles.idcuenta;"

        Comm.CommandText += "insert into tblcontabilidadsaldosi(idCContable,Cuenta,N1,naturaleza,saldoI) select tblccontables.idccontable,tblccontables.Cuenta,tblccontables.N2,tblccontables.Naturaleza,round(ifnull(sum(if(abono=-999999999,0,round(abono,2))),0)-ifnull(sum(if(cargo=-999999999,0,round(cargo,2))),0),2) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id and tblpolizas.estado=3"
        If pMes = "01" Then
            Comm.CommandText += " and tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<='" + anio.ToString + "/12/31' and tblPolizas.tipo='A'"
        Else
            Comm.CommandText += " and tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<'" + pHasta + "'"
        End If
        Comm.CommandText += " inner join tblccontables on tblpolizasdetalles.idcuenta=tblccontables.idccontable where tblccontables.naturaleza=1 group by tblpolizasdetalles.idcuenta;"

        Comm.ExecuteNonQuery()

    End Sub
    Public Function reporteRelaionesAnaliti(ByVal pMes As String, ByVal pTipo As Integer, ByVal pRango1 As String, ByVal pRango2 As String, ByVal pFechaI As String, ByVal pFechaF As String, NoCeros As Boolean) As DataView
        Dim DS2 As New DataSet
        Dim anioant As String = pFechaF.Substring(0, 4)
        Dim pHasta As Date = Date.Parse("01/" + pMes + "/" + anioant)
        'SaldosIniciales(pFechaI, pMes)
        todosLosSaldos(pFechaI, pFechaF, "", "")
        'Comm.CommandText = "delete from tblreprelacionesanaliticas"
        'Comm.ExecuteNonQuery()
        '"round(ifnull((select ifnull(saldoI,0) from tblcontabilidadsaldosi where concat(tblcontabilidadsaldosi.Cuenta,tblcontabilidadsaldosi.N1)=(select concat(Cuenta,N2) from tblccontables where idCContable=tblpolizasdetalles.idCuenta) limit 1),0),2) as saldoInicial," + _
        Comm.CommandText = "select concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),' ',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),''),' ',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + NNiv4.ToString + ",'0'),''),' ',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),'')) as Cuenta," + _
"tblccontables.descripcion," + _
"tblagrupadorcuentas.codigo as agrupador," + _
"if(tblccontables.Naturaleza=1 or tblccontables.depreciacion=1,'A','D') as Naturaleza,if(tblccontables.Naturaleza=1,'A','D') as NatR," + _
"round(ifnull((select ifnull(saldoI,0) from tblcontabilidadsaldosi where tblcontabilidadsaldosi.idccontable=tblpolizasdetalles.idCuenta limit 1),0),2) as saldoInicial," + _
"sum(if(tblpolizas.tipo<>'A' and tblpolizas.fecha>='" + pFechaI + "',ifnull(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)),0),0)) as cargo," + _
"sum(if(tblpolizas.tipo<>'A' and tblpolizas.fecha>='" + pFechaI + "',ifnull(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),0),0)) as abono," + _
"tblccontables.cuenta as cuenta1," + _
"tblccontables.Naturaleza as cuenta2," + _
"case tblccontables.tipo when 0 then 'ACTIVO' WHEN 1 THEN 'PASIVO' WHEN 2 THEN 'CAPITAL' WHEN 3 THEN 'INGRESOS' WHEN 4 THEN 'COSTOS' WHEN 5 THEN 'EGRESOS' WHEN 6 THEN 'ORDEN' END as Tipo," + _
"tblccontables.descripcion as descripcionMAyor," + _
"ifnull((select tsub.codigo from tblagrupadorcuentas tsub where tsub.id=(select csub.idcontable from tblccontables csub where csub.cuenta=tblccontables.cuenta and csub.n2='' and csub.n3='' and csub.n4='' and csub.n5='' limit 1)),'') as agruMayor," + _
"if(tblccontables.nivel=1,(select sum(round(saldoI,2)) from tblcontabilidadsaldosi where Cuenta=tblccontables.Cuenta limit 1),0)  as SaldoInicMayor," + _
"if(tblccontables.nivel=1,(select ifnull(sum(if(d.cargo=-999999999,0,round(d.cargo,2))),0) from tblpolizasdetalles as d inner join tblccontables as dd on d.idCuenta=dd.idCContable inner join tblpolizas as pp on d.idPoliza=pp.id  where dd.Cuenta=tblccontables.cuenta and pp.tipo<>'A' and pp.fecha<='" + pFechaF + "' and pp.fecha>='" + pFechaI + "'),0) as saldoCMay," + _
"if(tblccontables.nivel=1,(select ifnull(sum(if(d.abono=-999999999,0,round(d.abono,2))),0) from tblpolizasdetalles as d inner join tblccontables as dd on d.idCuenta=dd.idCContable inner join tblpolizas as pp on d.idPoliza=pp.id  where dd.Cuenta=tblccontables.cuenta and pp.tipo<>'A' and pp.fecha<='" + pFechaF + "' and pp.fecha>='" + pFechaI + "'),0) as saldoAMay," + _
"tblccontables.idcuentam,(select intc.descripcion from tblccontables intc where intc.idccontable=tblccontables.idcuentam) nmayor," + _
"(select intc.cuenta from tblccontables intc where intc.idccontable=tblccontables.idcuentam) cintmayor" + _
" from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id and tblPolizas.fecha<='" + pFechaF + "' and tblPolizas.fecha>='" + anioant + "/01/01' and tblpolizas.tipo<>'O'and tblpolizas.estado=3 inner join tblccontables on tblpolizasdetalles.idcuenta=tblccontables.idccontable " + _
"inner join tblagrupadorcuentas on tblccontables.idcontable=tblagrupadorcuentas.id "

        If pTipo <> 0 Then
            Comm.CommandText += "and tblccontables.tipo = " + (pTipo - 1).ToString
        End If
        Comm.CommandText += " where (tblccontables.descontinuada like '%" + anio + "%')=False "
        If pRango1 <> -1 Then
            Comm.CommandText += " and concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),'',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),''),'',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + NNiv4.ToString + ",'0'),''),'',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),''))>='" + pRango1.ToString + "' and concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),'',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),''),'',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + NNiv4.ToString + ",'0'),''),'',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),''))<='" + pRango2.ToString + "' "
        End If
        Comm.CommandText += " group by Cuenta order by (case tblccontables.tipo when 0 then 0 when 1 then 1 when 2 then 2 when 3 then 3 WHEN 5 THEN 4 WHEN 6 THEN 5 end),Cuenta"
        Comm.CommandTimeout = 30000000
        'Comm.ExecuteNonQuery()
        'Comm.CommandText = "select * from tblreprelacionesanaliticas"
        'If NoCeros Then
        '    Comm.CommandText = " where ()<>0"
        'End If
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblBalanza")
        DS2.WriteXmlSchema("tblRelacionesAnaliticas.xml")
        Return DS2.Tables("tblBalanza").DefaultView

    End Function

    Public Function verificadorPolizas(ByVal pFechaInicio As String, ByVal pFechaFinal As String, ByVal pTipo As String, ByVal pRangoDesde As Integer, ByVal pRangoHasta As Integer, Optional ByVal pIDCuenta As Integer = -1, Optional ByVal pClasif As Integer = -1)
        Dim DS2 As New DataSet
        Comm.CommandText = "Select tblpolizas.id as id, tblpolizas.concepto as concepto, ifnull(tblpolizas.beneficiario,'')as beneficiario, tblpolizasdetalles.cuenta as cuenta,tblCContables.Descripcion as descrip_cuenta, tblpolizasdetalles.descripcion, tblPolizas.tipo, tblpolizas.numero, tblpolizas.fecha, if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo) as cargo, if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)as abono from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id inner join tblCContables on tblpolizasdetalles.idCuenta=tblCContables.idCContable where tblPolizas.fecha>='" + pFechaInicio + "' and tblPolizas.fecha<='" + pFechaFinal + "' and tblpolizas.estado=3 "
        If pTipo <> "T" Then
            Comm.CommandText += " and tblPolizas.tipo='" + pTipo + "'"
        Else
            Comm.CommandText += " and tblPolizas.tipo<>'A'"
        End If
        If pRangoDesde <> -1 Or pRangoHasta <> -1 Then
            Comm.CommandText += " and tblPolizas.numero>=" + pRangoDesde.ToString + " and tblPolizas.numero<=" + pRangoHasta.ToString

        End If
        If pIDCuenta <> -1 Then
            Comm.CommandText += " and tblpolizasdetalles.idCuenta=" + pIDCuenta.ToString
        End If
        If pClasif > 0 Then
            Comm.CommandText += " and tblPolizas.clasificacion=" + pClasif.ToString
        End If
        Comm.CommandText += " order by (case tblpolizas.tipo when 'E' then '0' when 'I' then '1' when 'D' then '2' when 'A' then '3'  end),tblPolizas.numero"

        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblRelacionPolizas")
        ' DS2.WriteXmlSchema("tblRelacionPolizas.xml")
        Return DS2.Tables("tblRelacionPolizas")
    End Function
    Public Function impresionPolizasID(ByVal pFechaInicio As String, ByVal pFechaFinal As String, ByVal pTipo As String, ByVal pRangoDesde As Integer, ByVal pRangoHasta As Integer, Optional ByVal pIDCuenta As Integer = -1, Optional ByVal pClasif As Integer = -1)
        Dim DS2 As New DataSet
        Comm.CommandText = "Select tblpolizas.id as id, tblpolizas.concepto as concepto,tblpolizas.cantidadcheque,tblpolizas.leyendach, if(tblCContables.nivel>=1,LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),'') as N1,if(tblCContables.nivel>=2,LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),'') as N2,if(tblCContables.nivel>=3,LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),'') as N3,if(tblCContables.nivel>=4,LPAD(tblccontables.N4," + NNiv4.ToString + ",'0'),'') as N4,if(tblCContables.nivel>=5,LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),'') as N5, if(tblCContables.nivel>=1,(select cc.descripcion from tblccontables as cc where cc.Cuenta=tblCContables.Cuenta and cc.nivel=1 limit 1),'')as D1,if(tblCContables.nivel>=2,(select cc.descripcion from tblccontables as cc where cc.Cuenta=tblCContables.Cuenta and cc.N2=tblCContables.N2 and cc.nivel=2 limit 1),'')as D2,if(tblCContables.nivel>=3,(select cc.descripcion from tblccontables as cc where cc.Cuenta=tblCContables.Cuenta and cc.N2=tblCContables.N2 and cc.N3=tblCContables.N3 and cc.nivel=3 limit 1),'')as D3,if(tblCContables.nivel>=4,(select cc.descripcion from tblccontables as cc where cc.Cuenta=tblCContables.Cuenta and cc.N2=tblCContables.N2 and cc.N3=tblCContables.N3 and  cc.N4=tblCContables.N4 and cc.nivel=4 limit 1),'')as D4,if(tblCContables.nivel>=5,(select cc.descripcion from tblccontables as cc where cc.Cuenta=tblCContables.Cuenta and cc.N2=tblCContables.N2 and cc.N3=tblCContables.N3 and  cc.N4=tblCContables.N4 and  cc.N5=tblCContables.N5 and cc.nivel=5 limit 1),'')as D5, tblpolizasdetalles.descripcion, tblPolizas.tipo, tblpolizas.numero,tblpolizas.importe, tblpolizas.fecha, if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo) as cargo, if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)as abono,tblpolizas.beneficiario from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id inner join tblCContables on tblpolizasdetalles.idCuenta=tblCContables.idCContable where tblPolizas.fecha>='" + pFechaInicio + "' and tblPolizas.fecha<='" + pFechaFinal + "' and tblpolizas.estado=3"
        If pTipo <> "T" Then
            Comm.CommandText += " and tblPolizas.tipo='" + pTipo + "'"
            'Else
            '   Comm.CommandText += " and tblPolizas.tipo<>'A' and tblPolizas.tipo<>'E' "
        End If
        If pRangoDesde <> -1 Or pRangoHasta <> -1 Then
            Comm.CommandText += " and tblPolizas.numero>=" + pRangoDesde.ToString + " and tblPolizas.numero<=" + pRangoHasta.ToString

        End If
        If pIDCuenta <> -1 Then
            Comm.CommandText += " and tblpolizasdetalles.idCuenta=" + pIDCuenta.ToString
        End If
        If pClasif > 0 Then
            Comm.CommandText += " and tblPolizas.clasificacion=" + pClasif.ToString
        End If
        Comm.CommandText += " order by tblpolizas.fecha,(case tblpolizas.tipo when 'E' then '0' when 'I' then '1' when 'D' then '2' when 'A' then '3'  end),tblPolizas.numero,tblpolizasdetalles.orden"

        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblImpresionPolizas")
        'DS2.WriteXmlSchema("tblImpresionPolizas.xml")
        Return DS2.Tables("tblImpresionPolizas")
    End Function
    Public Function impresionPolizasE(ByVal pFechaInicio As String, ByVal pFechaFinal As String, ByVal pTipo As String, ByVal pRangoDesde As Integer, ByVal pRangoHasta As Integer, Optional ByVal pIDCuenta As Integer = -1, Optional ByVal pClasif As Integer = -1)
        Dim DS2 As New DataSet
        Comm.CommandText = "Select tblpolizas.id as id, tblpolizas.concepto as concepto, if(tblCContables.nivel>=1,LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),'') as N1,if(tblCContables.nivel>=2,LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),'') as N2,if(tblCContables.N3>=3,LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),'') as N3,if(tblCContables.N3>=4,LPAD(tblccontables.N4," + NNiv4.ToString + ",'0'),'') as N4,if(tblCContables.N5>=5,LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),'') as N5, if(tblCContables.nivel>=1,(select cc.descripcion from tblccontables as cc where cc.Cuenta=tblCContables.Cuenta and cc.nivel=1),'')as D1,if(tblCContables.nivel>=2,(select cc.descripcion from tblccontables as cc where cc.Cuenta=tblCContables.Cuenta and cc.N2=tblCContables.N2 and cc.nivel=2),'')as D2,if(tblCContables.nivel>=3,(select cc.descripcion from tblccontables as cc where cc.Cuenta=tblCContables.Cuenta and cc.N2=tblCContables.N2 and cc.N3=tblCContables.N3 and cc.nivel=3),'')as D3,if(tblCContables.nivel>=4,(select cc.descripcion from tblccontables as cc where cc.Cuenta=tblCContables.Cuenta and cc.N2=tblCContables.N2 and cc.N3=tblCContables.N3 and  cc.N4=tblCContables.N4 and cc.nivel=4),'')as D4,if(tblCContables.nivel>=5,(select cc.descripcion from tblccontables as cc where cc.Cuenta=tblCContables.Cuenta and cc.N2=tblCContables.N2 and cc.N3=tblCContables.N3 and  cc.N4=tblCContables.N4 and  cc.N5=tblCContables.N5 and cc.nivel=5),'')as D5, tblpolizasdetalles.descripcion, concat(tblPolizas.tipo) as tipo,tblPolizas.numero, tblpolizas.importe, DATE_FORMAT(tblpolizas.fecha, '%d - %m - %Y') as fecha, if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo) as cargo, if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)as abono, tblPolizas.beneficiario from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id inner join tblCContables on tblpolizasdetalles.idCuenta=tblCContables.idCContable where tblPolizas.fecha>='" + pFechaInicio + "' and tblPolizas.fecha<='" + pFechaFinal + "' "
        ' If pTipo <> "T" Then
        'Comm.CommandText += " and tblPolizas.tipo='" + pTipo + "'"
        ' Else
        Comm.CommandText += "  and tblPolizas.tipo='E' "
        '  End If
        If pRangoDesde <> -1 Or pRangoHasta <> -1 Then
            Comm.CommandText += " and tblPolizas.numero>=" + pRangoDesde.ToString + " and tblPolizas.numero<=" + pRangoHasta.ToString
        End If
        If pIDCuenta <> -1 Then
            Comm.CommandText += " and tblpolizasdetalles.idCuenta=" + pIDCuenta.ToString
        End If
        If pClasif > 0 Then
            Comm.CommandText += " and tblPolizas.clasificacion=" + pClasif.ToString
        End If
        Comm.CommandText += " order by tblpolizas.fecha,(case tblpolizas.tipo when 'E' then '0' when 'I' then '1' when 'D' then '2' when 'A' then '3'  end),tblPolizas.numero"
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblImpresionPolizasE")
        'DS2.WriteXmlSchema("tblImpresionPolizasE.xml")
        Return DS2.Tables("tblImpresionPolizasE")
    End Function
    Public Function polizaCierre()
        Dim DS2 As New DataSet

        SaldosInicialesCierre((anio + 1).ToString + "/01/01", 12)
        Comm.CommandText = "Select  detalles.idCuenta as IdCuenta ,(select concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),' ',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),' ',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),''),' ',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + NNiv4.ToString + ",'0'),''),' ',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),'')) from tblccontables where idCContable=detalles.idCuenta) as Cuenta"
        Comm.CommandText += " ,(select if(tblccontables.Naturaleza=0,'D','A') from tblccontables where tblccontables.idCContable=detalles.idCuenta) as Naturaleza "
        Comm.CommandText += ",ifnull((select ifnull(saldoI,0) from tblcontabilidadsaldosi where tblcontabilidadsaldosi.idccontable=detalles.idCuenta limit 1),0) as saldoInicial"
        ' Comm.CommandText += ")"
        Comm.CommandText += " from tblpolizasdetalles as detalles inner join tblPolizas as poliza on detalles.idPoliza=poliza.id inner join tblccontables as cc on cc.idCContable=detalles.idCuenta"
        Comm.CommandText += " where  poliza.tipo<>'O'  and poliza.fecha<='" + anio + "/12/31' and poliza.fecha>='" + anio + "/01/01' and poliza.estado=3 "
        Comm.CommandText += "and ((cc.Cuenta>=" + activoC1 + " and cc.Cuenta<=" + activoC2 + ") or (cc.Cuenta>=" + activoF1 + " and cc.Cuenta<=" + activoF2 + ") or (cc.Cuenta>=" + activoD1 + " and cc.Cuenta<=" + activoD2 + ") or (cc.Cuenta>=" + activoO1 + " and cc.Cuenta<=" + activoO2 + ") or (cc.Cuenta>=" + capital3 + " and cc.Cuenta<=" + capital4 + ") or (cc.Cuenta>=" + pasivoC1 + " and cc.Cuenta<=" + pasivoC2 + ") or (cc.Cuenta>=" + pasivoF1 + " and cc.Cuenta<=" + pasivoF2 + ") or (cc.Cuenta>=" + pasivoD1 + " and cc.Cuenta<=" + pasivoD2 + "))"

        Comm.CommandText += " group by detalles.idCuenta "
        Comm.CommandText += " order by (case poliza.tipo when 'ACTÍVO' then '0' when 'PASIVO' then '1' when 'CAPITAL' then '2' when 'INGRESOS' then '3' WHEN 'EGRESOS' THEN '4' WHEN 'ORDEN' THEN '5' end),Cuenta"

        Comm.CommandTimeout = 30000000
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblBalanza")
        ' DS2.WriteXmlSchema("tblRelacionesAnaliticas.xml")
        Return DS2.Tables("tblBalanza")

    End Function
    Public Function existePolizaCierre()
        Dim contador As Integer
        Comm.CommandText = "Select  count(id) from tblPolizas where tipo='A' and fecha>='" + (anio + 1).ToString + "/01/01' and fecha<='" + (anio + 1).ToString + "/12/31'"
        contador = Comm.ExecuteScalar
        If contador > 0 Then
            Comm.CommandText = "Select  id from tblPolizas where tipo='A' and fecha>='" + (anio + 1).ToString + "/01/01' and fecha<='" + (anio + 1).ToString + "/12/31'"
            IDPoliza = Comm.ExecuteScalar
        End If
        Return contador
    End Function
    Public Function eliminarAperturaExistente(Optional ByVal pAnio As Integer = -1)
        Dim contador As Integer
        Dim idP As Integer
        If pAnio <> -1 Then
            anio = pAnio
        End If
        Comm.CommandText = "Select id from tblPolizas where tipo='A' and fecha>='" + (anio + 1).ToString + "/01/01' and fecha<='" + (anio + 1).ToString + "/12/31'"
        idP = Comm.ExecuteScalar
        Comm.CommandText = "delete from tblPolizasdetalles where idPoliza=" + idP.ToString + "; "
        Comm.CommandText += "delete from tblPolizas where tipo='A' and fecha>='" + (anio + 1).ToString + "/01/01' and fecha<='" + (anio + 1).ToString + "/12/31'"
        contador = Comm.ExecuteScalar
        Return contador
    End Function
    'Public Function generaXMLBalanza(ByVal pMes As String, ByVal pTipoEnvio As String, ByVal pFechaModBal As String, ByVal pSello As String, ByVal pNoCertificado As String, ByVal pCertificado As String)
    '    Dim tablaBalanza As DataTable = reporteBalanzaXML2(pMes)
    '    Dim xml As String = ""
    '    Dim xml3 As String = ""
    '    Dim cuenta As String = ""
    '    Dim cargo As Double = 0
    '    Dim abono As Double = 0
    '    Dim Na As String = ""
    '    Dim SI As Double
    '    Dim SF As Double
    '    Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
    '    Dim xmldoc As New System.Xml.XmlDocument
    '    Dim cadena As String = ""
    '    Dim cadenaAux As String = ""

    '    For i As Integer = 0 To tablaBalanza.Rows.Count - 1
    '        If i = 0 Then
    '            'primero
    '            cuenta = tablaBalanza.Rows(i)(0).ToString
    '            cargo = tablaBalanza.Rows(i)(3).ToString
    '            abono = tablaBalanza.Rows(i)(4).ToString
    '            SI = tablaBalanza.Rows(i)(2).ToString
    '            Na = tablaBalanza.Rows(i)(1).ToString
    '        Else
    '            If i = tablaBalanza.Rows.Count - 1 Then
    '                'ultimo registro
    '                If tablaBalanza.Rows(i)(0).ToString <> cuenta Then
    '                    'agrupar lo que ya tenemos y volver a cargar los datos
    '                    If Na = "D" Then
    '                        SF = SI + cargo - abono
    '                    End If
    '                    xml += "<BCE:Ctas NumCta=""" + cuenta + """ SaldoIni=""" + SI.ToString("0.00") + """ Debe=""" + cargo.ToString("0.00") + """ Haber=""" + abono.ToString("0.00") + """ SaldoFin=""" + SF.ToString("0.00") + """/>" + vbCrLf
    '                    cadenaAux += cuenta + "|" + SI.ToString("0.00") + "|" + cargo.ToString("0.00") + "|" + abono.ToString("0.00") + "|" + SF.ToString("0.00") + "|"
    '                    cuenta = tablaBalanza.Rows(i)(0).ToString
    '                    cargo = tablaBalanza.Rows(i)(3).ToString
    '                    abono = tablaBalanza.Rows(i)(4).ToString
    '                    SI = tablaBalanza.Rows(i)(2).ToString
    '                    Na = tablaBalanza.Rows(i)(1).ToString

    '                    If Na = "D" Then
    '                        SF = SI + cargo - abono
    '                    End If
    '                    xml += "<BCE:Ctas NumCta=""" + cuenta + """ SaldoIni=""" + SI.ToString("0.00") + """ Debe=""" + cargo.ToString("0.00") + """ Haber=""" + abono.ToString("0.00") + """ SaldoFin=""" + SF.ToString("0.00") + """/>" + vbCrLf
    '                    cadenaAux += cuenta + "|" + SI.ToString("0.00") + "|" + cargo.ToString("0.00") + "|" + abono.ToString("0.00") + "|" + SF.ToString("0.00") + "|"
    '                Else
    '                    'es la misma cuenta; sumar cargos y abonos
    '                    cargo += tablaBalanza.Rows(i)(3).ToString
    '                    abono += tablaBalanza.Rows(i)(4).ToString

    '                End If

    '            Else
    '                If tablaBalanza.Rows(i)(0).ToString <> cuenta Then
    '                    'agrupar lo que ya tenemos y volver a cargar los datos
    '                    If Na = "D" Then
    '                        SF = SI + cargo - abono
    '                    End If
    '                    xml += "<BCE:Ctas NumCta=""" + cuenta + """ SaldoIni=""" + SI.ToString("0.00") + """ Debe=""" + cargo.ToString("0.00") + """ Haber=""" + abono.ToString("0.00") + """ SaldoFin=""" + SF.ToString("0.00") + """/>" + vbCrLf
    '                    cadenaAux += cuenta + "|" + SI.ToString("0.00") + "|" + cargo.ToString("0.00") + "|" + abono.ToString("0.00") + "|" + SF.ToString("0.00") + "|"
    '                    cuenta = tablaBalanza.Rows(i)(0).ToString
    '                    cargo = tablaBalanza.Rows(i)(3).ToString
    '                    abono = tablaBalanza.Rows(i)(4).ToString
    '                    SI = tablaBalanza.Rows(i)(2).ToString
    '                    Na = tablaBalanza.Rows(i)(1).ToString
    '                Else
    '                    'es la misma cuenta; sumar cargos y abonos
    '                    cargo += tablaBalanza.Rows(i)(3).ToString
    '                    abono += tablaBalanza.Rows(i)(4).ToString
    '                End If
    '            End If

    '        End If

    '    Next


    '    cadena = "||1.1|" + s.RFC + "|" + pMes + "|" + buscarPeriodo() + "|" + pTipoEnvio + "|"
    '    If pFechaModBal <> "" Then
    '        cadena += pFechaModBal + "|"
    '    End If
    '    cadena += cadenaAux
    '    cadena += "|"
    '    Dim Archivos As New dbSucursalesArchivos
    '    Dim en As New Encriptador
    '    Dim Sello As String = ""

    '    Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
    '    Sello = en.GeneraSello(cadena, Archivos.RutaCer, "2011")

    '    Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, True)
    '    en.Leex509(Archivos.RutaCer)

    '    xml3 = "<?xml version=""1.0"" encoding=""UTF-8""?>"
    '    xml3 += "<BCE:Balanza "
    '    xml3 += "xsi:schemaLocation=""http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion/BalanzaComprobacion_1_1.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:BCE=""http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion"""
    '    xml3 += " Version=""1.1"" RFC=""" + s.RFC + """ Mes=""" + pMes + """ Anio=""" + buscarPeriodo() + """ TipoEnvio=""" + pTipoEnvio + """"
    '    If pFechaModBal <> "" Then
    '        xml3 += " FechaModBal=""" + pFechaModBal + """"
    '    End If
    '    xml3 += " Sello=""" + Sello + """"
    '    xml3 += " noCertificado=""" + en.Seriex509 + """"
    '    xml3 += " Certificado=""" + en.Certificado64 + """"
    '    xml3 += ">"
    '    xml3 += xml
    '    xml3 += "</BCE:Balanza>"





    '    xmldoc.LoadXml(xml3)

    '    Return xmldoc

    'End Function
    Public Function generaXMLBalanza(ByVal pMes As String, ByVal pTipoEnvio As String, ByVal pFechaModBal As String, ByVal pSello As String, ByVal pNoCertificado As String, ByVal pCertificado As String, pEs13 As Boolean)
        Dim tablaBalanza As DataTable = reporteBalanzaXML2(pMes, pEs13)
        Dim xml As String = ""
        Dim xml3 As String = ""
        Dim cuenta As String = ""
        Dim cargo As Double = 0
        Dim abono As Double = 0
        Dim Na As String = ""
        Dim SI As Double
        Dim SF As Double
        Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Dim xmldoc As New System.Xml.XmlDocument
        Dim cadena As String = ""
        Dim cadenaAux As String = ""

        For i As Integer = 0 To tablaBalanza.Rows.Count - 1
            If i = 0 Then
                'primero
                cuenta = tablaBalanza.Rows(i)(0).ToString
                cargo = tablaBalanza.Rows(i)(3).ToString
                abono = tablaBalanza.Rows(i)(4).ToString
                SI = tablaBalanza.Rows(i)(2).ToString
                Na = tablaBalanza.Rows(i)(1).ToString
            Else
                If i = tablaBalanza.Rows.Count - 1 Then
                    'ultimo registro
                    If tablaBalanza.Rows(i)(0).ToString = cuenta Then
                        cargo += tablaBalanza.Rows(i)(3).ToString
                        abono += tablaBalanza.Rows(i)(4).ToString
                    End If
                    'agrupar lo que ya tenemos y volver a cargar los datos
                    If Na = "D" Then
                        SF = SI + cargo - abono
                    Else
                        SF = SI - cargo + abono
                    End If
                    xml += "<BCE:Ctas NumCta=""" + cuenta.Trim + """ SaldoIni=""" + SI.ToString("0.00") + """ Debe=""" + cargo.ToString("0.00") + """ Haber=""" + abono.ToString("0.00") + """ SaldoFin=""" + SF.ToString("0.00") + """/>" + vbCrLf
                    cadenaAux += cuenta.Trim + "|" + SI.ToString("0.00") + "|" + cargo.ToString("0.00") + "|" + abono.ToString("0.00") + "|" + SF.ToString("0.00") + "|"

                    If tablaBalanza.Rows(i)(0).ToString <> cuenta Then
                        'cargo += tablaBalanza.Rows(i)(3).ToString
                        'abono += tablaBalanza.Rows(i)(4).ToString
                        cuenta = tablaBalanza.Rows(i)(0).ToString
                        cargo = tablaBalanza.Rows(i)(3).ToString
                        abono = tablaBalanza.Rows(i)(4).ToString
                        SI = tablaBalanza.Rows(i)(2).ToString
                        Na = tablaBalanza.Rows(i)(1).ToString
                        If Na = "D" Then
                            SF = SI + cargo - abono
                        Else
                            SF = SI - cargo + abono
                        End If
                        xml += "<BCE:Ctas NumCta=""" + cuenta.Trim + """ SaldoIni=""" + SI.ToString("0.00") + """ Debe=""" + cargo.ToString("0.00") + """ Haber=""" + abono.ToString("0.00") + """ SaldoFin=""" + SF.ToString("0.00") + """/>" + vbCrLf
                        cadenaAux += cuenta.Trim + "|" + SI.ToString("0.00") + "|" + cargo.ToString("0.00") + "|" + abono.ToString("0.00") + "|" + SF.ToString("0.00") + "|"

                    End If
                    'If Na = "D" Then
                    '    SF = SI + cargo - abono
                    'Else
                    '    SF = SI - cargo + abono
                    'End If
                    'xml += "<BCE:Ctas NumCta=""" + cuenta.Trim + """ SaldoIni=""" + SI.ToString("0.00") + """ Debe=""" + cargo.ToString("0.00") + """ Haber=""" + abono.ToString("0.00") + """ SaldoFin=""" + SF.ToString("0.00") + """/>" + vbCrLf
                    'cadenaAux += cuenta.Trim + "|" + SI.ToString("0.00") + "|" + cargo.ToString("0.00") + "|" + abono.ToString("0.00") + "|" + SF.ToString("0.00") + "|"
                    'Else
                    '    'es la misma cuenta; sumar cargos y abonos
                    '    cargo += tablaBalanza.Rows(i)(3).ToString
                    '    abono += tablaBalanza.Rows(i)(4).ToString

                    'End If

                Else
                    If tablaBalanza.Rows(i)(0).ToString <> cuenta Then
                        'agrupar lo que ya tenemos y volver a cargar los datos
                        If Na = "D" Then
                            SF = SI + cargo - abono
                        Else
                            SF = SI - cargo + abono
                        End If
                        xml += "<BCE:Ctas NumCta=""" + cuenta.Trim + """ SaldoIni=""" + SI.ToString("0.00") + """ Debe=""" + cargo.ToString("0.00") + """ Haber=""" + abono.ToString("0.00") + """ SaldoFin=""" + SF.ToString("0.00") + """/>" + vbCrLf
                        cadenaAux += cuenta.Trim + "|" + SI.ToString("0.00") + "|" + cargo.ToString("0.00") + "|" + abono.ToString("0.00") + "|" + SF.ToString("0.00") + "|"
                        cuenta = tablaBalanza.Rows(i)(0).ToString
                        cargo = tablaBalanza.Rows(i)(3).ToString
                        abono = tablaBalanza.Rows(i)(4).ToString
                        SI = tablaBalanza.Rows(i)(2).ToString
                        Na = tablaBalanza.Rows(i)(1).ToString
                    Else
                        'es la misma cuenta; sumar cargos y abonos
                        cargo += tablaBalanza.Rows(i)(3).ToString
                        abono += tablaBalanza.Rows(i)(4).ToString
                    End If
                End If

            End If

        Next

        If pEs13 = False Then
            cadena = "||1.1|" + s.RFC + "|" + pMes + "|" + buscarPeriodo() + "|" + pTipoEnvio + "|"
        Else
            cadena = "||1.1|" + s.RFC + "|13|" + buscarPeriodo() + "|" + pTipoEnvio + "|"
        End If
        If pFechaModBal <> "" Then
            cadena += pFechaModBal + "|"
        End If
        cadena += cadenaAux
        cadena += "|"
        While cadena.IndexOf("  ") <> -1
            cadena = Replace(cadena, "  ", " ")
        End While
        Dim Archivos As New dbSucursalesArchivos
        Dim en As New Encriptador
        Dim Sello As String = ""

        Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
        Sello = en.GeneraSello(cadena, Archivos.RutaCer, "2011", False)

        Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)

        xml3 = "<?xml version=""1.0"" encoding=""UTF-8""?>"
        xml3 += "<BCE:Balanza "
        xml3 += "xsi:schemaLocation=""www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion/BalanzaComprobacion_1_1.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:BCE=""www.sat.gob.mx/esquemas/ContabilidadE/1_1/BalanzaComprobacion"""
        If pEs13 = False Then
            xml3 += " Version=""1.1"" RFC=""" + s.RFC + """ Mes=""" + pMes + """ Anio=""" + buscarPeriodo() + """ TipoEnvio=""" + pTipoEnvio + """"
        Else
            xml3 += " Version=""1.1"" RFC=""" + s.RFC + """ Mes=""13"" Anio=""" + buscarPeriodo() + """ TipoEnvio=""" + pTipoEnvio + """"
        End If
        If pFechaModBal <> "" Then
            xml3 += " FechaModBal=""" + pFechaModBal + """"
        End If
        xml3 += " Sello=""" + Sello + """"
        xml3 += " noCertificado=""" + en.Seriex509 + """"
        xml3 += " Certificado=""" + en.Certificado64 + """"
        xml3 += ">"
        xml3 += xml
        xml3 += "</BCE:Balanza>"
        xmldoc.LoadXml(xml3)

        Return xmldoc

    End Function
    Public Function SumaCargo(ByVal pFechaI As String, ByVal pFechaF As String)
        Dim cargoD As Double = 0
        Dim cargoA As Double = 0
        Dim abonoD As Double = 0
        Dim abonoA As Double = 0
        Comm.CommandText = "Select ifnull(sum(if(cargo=-999999999,0,cargo)),0) from tblpolizasdetalles inner join tblPolizas as Poliza on tblpolizasdetalles.idPoliza=poliza.id inner join tblccontables on tblpolizasdetalles.idCuenta=tblccontables.idCContable where poliza.tipo<>'A' and poliza.tipo<>'O' and poliza.fecha>='" + pFechaI + "' and poliza.fecha<='" + pFechaF + "' and tblccontables.Naturaleza='0' "
        cargoD = Comm.ExecuteScalar
        Comm.CommandText = "Select ifnull(sum(if(cargo=-999999999,0,cargo)),0) from tblpolizasdetalles inner join tblPolizas as Poliza on tblpolizasdetalles.idPoliza=poliza.id inner join tblccontables on tblpolizasdetalles.idCuenta=tblccontables.idCContable where poliza.tipo<>'A' and poliza.tipo<>'O' and poliza.fecha>='" + pFechaI + "' and poliza.fecha<='" + pFechaF + "' and tblccontables.Naturaleza='1' "
        cargoA = Comm.ExecuteScalar
        Comm.CommandText = "Select ifnull(sum(if(abono=-999999999,0,abono)),0) from tblpolizasdetalles inner join tblPolizas as Poliza on tblpolizasdetalles.idPoliza=poliza.id inner join tblccontables on tblpolizasdetalles.idCuenta=tblccontables.idCContable where poliza.tipo<>'A' and poliza.tipo<>'O' and poliza.fecha>='" + pFechaI + "' and poliza.fecha<='" + pFechaF + "' and tblccontables.Naturaleza='0' "
        abonoD = Comm.ExecuteScalar
        Comm.CommandText = "Select ifnull(sum(if(abono=-999999999,0,abono)),0) from tblpolizasdetalles inner join tblPolizas as Poliza on tblpolizasdetalles.idPoliza=poliza.id inner join tblccontables on tblpolizasdetalles.idCuenta=tblccontables.idCContable where poliza.tipo<>'A' and poliza.tipo<>'O' and poliza.fecha>='" + pFechaI + "' and poliza.fecha<='" + pFechaF + "' and tblccontables.Naturaleza='1' "
        abonoA = Comm.ExecuteScalar
        Return cargoD - abonoD + abonoA - cargoA
    End Function
    Public Function provSINRFC(ByVal pfechaI As String, ByVal pFechaF As String, ByVal pProveedor As String)
        Comm.CommandText = "select count(tblproveedores.clave) from tblpolizasdetalles INNER JOIN tblproveedores ON tblpolizasdetalles.idProveedor=tblproveedores.clave inner join tblpolizas ON tblpolizasdetalles.idPoliza=tblpolizas.id where  tblpolizasdetalles.esDIOT=1 and tblproveedores.rfc=''"
        Comm.CommandText += " and tblpolizas.fecha>='" + pfechaI.ToString + "' and tblpolizas.fecha<='" + pFechaF.ToString + "'"
        If pProveedor <> "" Then
            Comm.CommandText += " and tblproveedores.idProveedor=" + pProveedor.ToString
        End If
        Return Comm.ExecuteScalar()
    End Function
    Public Function RFCEmpresa() As String 'consulta rfc de la empresa
        Dim rfc1 As String
        Comm.CommandText = "select rfc from tblsucursales where idSucursal=" + GlobalIdSucursalDefault.ToString
        rfc1 = Comm.ExecuteScalar
        Return rfc1
    End Function
    'GENERAR XML
    'Public Function Generaxml(ByVal pdesde As String, ByVal pHasta As String, ByVal pTipoSolicitud As String, ByVal pNumOrden As String, ByVal pNumTramite As String, ByVal pSEllo As String, ByVal pNoCertificado As String, ByVal pCertificado As String)
    '    Dim xmls As String = ""
    '    Dim tabla As DataTable
    '    Dim xml2 As String = ""
    '    Dim cadena As String = ""
    '    Dim cadena2 As String = ""
    '    Dim xmldoc As New System.Xml.XmlDocument

    '    Dim fechita As Date = Date.Parse(pdesde)

    '    Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)





    '    Dim DS As New DataSet
    '    Comm.CommandText = "select id,IF(tipo='I',1,IF (tipo='E',2,3)) as tipo,concat(tipo,numero) ,fecha as Fecha,concepto as Concepto from tblpolizas where fecha>='" + pdesde + "' and fecha<='" + pHasta + "' and tipo<>'O' and tipo<>'A' "
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblpoliza")
    '    tabla = DS.Tables("tblpoliza")

    '    For i As Integer = 0 To tabla.Rows.Count - 1
    '        tablaChequeN.Clear()
    '        tablaTransN.Clear()
    '        tablaDetalles.Clear()
    '        tablaComproN.Clear()
    '        '  tablaComproNac2.Clear()
    '        tablaComproEN.Clear()
    '        tablaOtrosN.Clear()
    '        xmls += "<PLZ:Poliza NumUnIdenPol=""" + RC(tabla.Rows(i)(2).ToString) + """ Fecha=""" + tabla.Rows(i)(3).ToString + """ Concepto=""" + RC(tabla.Rows(i)(4).ToString) + """>" + vbCrLf
    '        'Detalles
    '        cadena2 += tabla.Rows(i)(2).ToString + "|" + tabla.Rows(i)(3).ToString + "|" + tabla.Rows(i)(4).ToString + "|"

    '        Dim DS4 As New DataSet
    '        Comm.CommandText = "select tblpolizasdetalles.id, REPLACE(tblpolizasdetalles.cuenta, ' ','')as cuenta,tblpolizasdetalles.descripcion,if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo) as cargo,if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono) as abonom,tblCContables.Descripcion from tblpolizasdetalles inner join tblccontables on tblccontables.idCContable=tblpolizasdetalles.idCuenta where idPoliza=" + tabla.Rows(i)(0).ToString
    '        Dim DA4 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '        DA4.Fill(DS4, "tblpoliza")
    '        tablaDetalles = DS4.Tables("tblpoliza")
    '        For j As Integer = 0 To tablaDetalles.Rows.Count - 1
    '            xmls += "<PLZ:Transaccion NumCta=""" + tablaDetalles.Rows(j)(1).ToString + """ DesCta=""" + RC(tablaDetalles.Rows(j)(5).ToString) + """ Concepto=""" + RC(tablaDetalles.Rows(j)(2).ToString) + """ Debe=""" + Double.Parse(tablaDetalles.Rows(j)(3).ToString).ToString("0.00") + """ Haber=""" + Double.Parse(tablaDetalles.Rows(j)(4).ToString).ToString("0.00") + """"
    '            cadena2 += tablaDetalles.Rows(j)(1).ToString + "|" + tablaDetalles.Rows(j)(2).ToString + "|" + Double.Parse(tablaDetalles.Rows(j)(3).ToString).ToString("0.00") + "|" + Double.Parse(tablaDetalles.Rows(j)(4).ToString).ToString("0.00") + "|"

    '            'cheques
    '            Dim DS2 As New DataSet
    '            Comm.CommandText = "select tblcontabilidadcheque.numero as numero ,tblcontabilidadcheque.banco as banco ,tblcontabilidadcheque.bancoemisor,tblcontabilidadcheque.ctaOri as ctaOri,tblcontabilidadcheque.fecha as fecha,tblcontabilidadcheque.benef as benef,tblcontabilidadcheque.rfc as rfc,tblcontabilidadcheque.monto as monto,tblmonedassat.codigo,tblcontabilidadcheque.tipoCambio from tblcontabilidadcheque inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcheque.moneda where tblcontabilidadcheque.idrenglon=" + tablaDetalles.Rows(j)(0).ToString
    '            Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '            DA2.Fill(DS2, "tblcontabilidadcheque")
    '            tablaChequeN = DS2.Tables("tblcontabilidadcheque")

    '            'trans
    '            Dim DS3 As New DataSet
    '            Comm.CommandText = "select tblcontabilidadtrans.ctaOri as ctaOri,tblcontabilidadtrans.bancoOri as bancoOri,tblcontabilidadtrans.bancoOrie ,tblcontabilidadtrans.ctaDest as ctaDest,tblcontabilidadtrans.bancoDest as bancoDest,tblcontabilidadtrans.bancoDeste,tblcontabilidadtrans.fecha as fecha,tblcontabilidadtrans.benef as benef,tblcontabilidadtrans.rfc as rfc,tblcontabilidadtrans.monto as monto,tblmonedassat.codigo,tblcontabilidadtrans.tipoCambio from tblcontabilidadtrans inner join tblmonedassat on tblmonedassat.id=tblcontabilidadtrans.moneda where idRenglon=" + tablaDetalles.Rows(j)(0).ToString
    '            Dim DA3 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '            DA3.Fill(DS3, "tblpoliza")
    '            tablaTransN = DS3.Tables("tblpoliza")

    '            'compro
    '            Dim DS5 As New DataSet
    '            Comm.CommandText = "select UUID,rfc,monto,tblmonedassat.codigo,tipocambio from tblcontabilidadcompro inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcompro.moneda where idRenglon=" + tablaDetalles.Rows(j)(0).ToString
    '            Dim DA5 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '            DA5.Fill(DS5, "tblpoliza")
    '            tablaComproN = DS5.Tables("tblpoliza")

    '            'comproNall2
    '            Dim DS6 As New DataSet
    '            Comm.CommandText = "select Serie,folio,rfc,monto,tblmonedassat.codigo,tipoCambio from tblcontabilidadcompro2 inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcompro2.moneda where idRenglon=" + tablaDetalles.Rows(j)(0).ToString
    '            Dim DA6 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '            DA6.Fill(DS6, "tblpoliza")
    '            tablaComproNac2 = DS6.Tables("tblpoliza")

    '            'compro extranjero
    '            Dim DS7 As New DataSet
    '            Comm.CommandText = "select numFactura,taxID,monto,tblmonedassat.codigo,tipoCambio from tblcontabilidadcomproe inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcomproe.moneda where idRenglon=" + tablaDetalles.Rows(j)(0).ToString
    '            Dim DA7 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '            DA7.Fill(DS7, "tblpoliza")
    '            tablaComproEN = DS7.Tables("tblpoliza")

    '            'compro otros
    '            Dim DS8 As New DataSet
    '            Comm.CommandText = "select tblmetodospago.clave,fecha,benef,rfc,monto,tblmonedassat.codigo,tipoCambio from tblcontabilidadotros inner join tblmonedassat on tblmonedassat.id=tblcontabilidadotros.moneda inner join tblmetodospago on  tblmetodospago.id=tblcontabilidadotros.metodoPago where idRenglon=" + tablaDetalles.Rows(j)(0).ToString
    '            Dim DA8 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '            DA8.Fill(DS8, "tblpoliza")
    '            tablaOtrosN = DS8.Tables("tblpoliza")

    '            If tablaChequeN.Rows.Count = 0 And tablaComproN.Rows.Count = 0 And tablaTransN.Rows.Count = 0 And tablaComproNac2.Rows.Count = 0 And tablaComproEN.Rows.Count = 0 And tablaOtrosN.Rows.Count = 0 Then
    '                xmls += "/>" + vbCrLf
    '            Else
    '                xmls += ">" + vbCrLf
    '                'Cheques 
    '                If tablaChequeN.Rows.Count > 0 Then
    '                    For t As Integer = 0 To tablaChequeN.Rows.Count - 1
    '                        xmls += "<PLZ:Cheque Num=""" + tablaChequeN.Rows(t)(0).ToString + """ BanEmisNal=""" + tablaChequeN.Rows(t)(1).ToString + """"
    '                        cadena2 += tablaChequeN.Rows(t)(0).ToString + "|" + tablaChequeN.Rows(t)(1).ToString + "|"
    '                        If tablaChequeN.Rows(t)(2).ToString <> "" Then
    '                            xmls += " BanEmisExt=""" + RC(tablaChequeN.Rows(t)(2).ToString) + """"
    '                            cadena2 += tablaChequeN.Rows(t)(2).ToString + "|"
    '                        End If
    '                        xmls += " CtaOri=""" + tablaChequeN.Rows(t)(3).ToString + """ Fecha=""" + tablaChequeN.Rows(t)(4).ToString + """ Benef=""" + RC(tablaChequeN.Rows(t)(5).ToString) + """ RFC=""" + tablaChequeN.Rows(t)(6).ToString + """ Monto=""" + Double.Parse(tablaChequeN.Rows(t)(7)).ToString("0.00") + """"
    '                        cadena2 += tablaChequeN.Rows(t)(3).ToString + "|" + tablaChequeN.Rows(t)(4).ToString + "|" + tablaChequeN.Rows(t)(5).ToString + "|" + tablaChequeN.Rows(t)(6).ToString + "|" + Double.Parse(tablaChequeN.Rows(t)(7)).ToString("0.00") + "|"

    '                        If tablaChequeN.Rows(t)(8) <> "MXN" Then
    '                            xmls += "  Moneda=""" + tablaChequeN.Rows(t)(8).ToString + """ TipCamb=""" + tablaChequeN.Rows(t)(9).ToString + """"
    '                            cadena2 += tablaChequeN.Rows(t)(8).ToString + "|" + tablaChequeN.Rows(t)(9).ToString + "|"
    '                        End If
    '                        xmls += "/>" + vbCrLf



    '                    Next
    '                End If
    '                'transferencia
    '                If tablaTransN.Rows.Count > 0 Then
    '                    For t As Integer = 0 To tablaTransN.Rows.Count - 1
    '                        xmls += "<PLZ:Transferencia CtaOri=""" + tablaTransN.Rows(t)(0).ToString + """ BancoOriNal=""" + tablaTransN.Rows(t)(1).ToString + """"
    '                        cadena2 += tablaTransN.Rows(t)(0).ToString + "|" + tablaTransN.Rows(t)(1).ToString + "|"

    '                        If tablaTransN.Rows(t)(2).ToString <> "" Then
    '                            xmls += " BancoOriExt=""" + RC(tablaTransN.Rows(t)(2).ToString) + """"
    '                            cadena2 += tablaTransN.Rows(t)(2).ToString + "|"
    '                        End If
    '                        xmls += " CtaDest=""" + tablaTransN.Rows(t)(3).ToString + """ BancoDestNal=""" + tablaTransN.Rows(t)(4).ToString + """"
    '                        cadena2 += tablaTransN.Rows(t)(3).ToString + "|" + tablaTransN.Rows(t)(4).ToString + "|"

    '                        If tablaTransN.Rows(t)(5).ToString <> "" Then
    '                            xmls += " BancoDestExt=""" + RC(tablaTransN.Rows(t)(5).ToString) + """"
    '                            cadena2 += tablaTransN.Rows(t)(5).ToString + "|"
    '                        End If

    '                        xmls += " Fecha=""" + tablaTransN.Rows(t)(6).ToString + """ Benef=""" + RC(tablaTransN.Rows(t)(7).ToString) + """ RFC=""" + tablaTransN.Rows(t)(8).ToString + """ Monto=""" + Double.Parse(tablaTransN.Rows(t)(9)).ToString("0.00") + """"
    '                        cadena2 += tablaTransN.Rows(t)(6).ToString + "|" + tablaTransN.Rows(t)(7).ToString + "|" + tablaTransN.Rows(t)(8).ToString + "|" + Double.Parse(tablaTransN.Rows(t)(9)).ToString("0.00") + "|"

    '                        If tablaTransN.Rows(t)(10).ToString <> "MXN" Then
    '                            xmls += " Moneda=""" + tablaTransN.Rows(t)(10).ToString + """ TipCamb=""" + tablaTransN.Rows(t)(11).ToString + """"
    '                            cadena2 += tablaTransN.Rows(t)(10).ToString + "|" + tablaTransN.Rows(t)(11).ToString + "|"
    '                        End If
    '                        xmls += "/>"
    '                    Next
    '                End If

    '                'comprobante
    '                If tablaComproN.Rows.Count > 0 Then
    '                    For t As Integer = 0 To tablaComproN.Rows.Count - 1
    '                        xmls += "<PLZ:CompNal UUID_CFDI=""" + RC(tablaComproN.Rows(t)(0).ToString) + """ RFC=""" + tablaComproN.Rows(t)(1).ToString + """ MontoTotal=""" + Double.Parse(tablaComproN.Rows(t)(2)).ToString("0.00") + """"
    '                        cadena2 += tablaComproN.Rows(t)(0).ToString + "|"
    '                        If tablaComproN.Rows(t)(3).ToString <> "MXN" Then
    '                            xmls += "  Moneda=""" + tablaComproN.Rows(t)(3).ToString + """ TipCamb = """ + tablaComproN.Rows(t)(4).ToString + """"
    '                        End If
    '                        xmls += "/>"
    '                    Next
    '                End If

    '                'comprobantes nac 2
    '                If tablaComproNac2.Rows.Count > 0 Then
    '                    For t As Integer = 0 To tablaComproNac2.Rows.Count - 1
    '                        xmls += "<PLZ:CompNalOtr CFD_CBB_Serie=""" + RC(tablaComproNac2.Rows(t)(0).ToString) + """ CFD_CBB_NumFol=""" + RC(tablaComproNac2.Rows(t)(1).ToString) + """ RFC=""" + tablaComproNac2.Rows(t)(2).ToString + """ MontoTotal=""" + Double.Parse(tablaComproNac2.Rows(t)(3)).ToString("0.00") + """"
    '                        cadena2 += tablaComproNac2.Rows(t)(0).ToString + "|" + tablaComproNac2.Rows(t)(1).ToString + "|"
    '                        If tablaComproNac2.Rows(t)(4).ToString <> "MXN" Then
    '                            xmls += " Moneda=""" + tablaComproNac2.Rows(t)(4).ToString + """ TipCamb=""" + tablaComproNac2.Rows(t)(5).ToString + """"
    '                        End If

    '                        xmls += "/>"
    '                    Next
    '                End If


    '                'comprobantes ex
    '                If tablaComproEN.Rows.Count > 0 Then
    '                    For t As Integer = 0 To tablaComproEN.Rows.Count - 1
    '                        xmls += "<PLZ:CompExt NumFactExt=""" + RC(tablaComproEN.Rows(t)(0).ToString) + """ TaxID=""" + RC(tablaComproEN.Rows(t)(1).ToString) + """ MontoTotal=""" + Double.Parse(tablaComproEN.Rows(t)(2)).ToString("0.00") + """"
    '                        cadena2 += tablaComproEN.Rows(t)(0).ToString + "|"
    '                        If tablaComproEN.Rows(t)(3).ToString <> "MXN" Then
    '                            xmls += " Moneda=""" + tablaComproEN.Rows(t)(3).ToString + """ TipCamb=""" + tablaComproEN.Rows(t)(4).ToString + """"
    '                        End If
    '                        xmls += "/>"
    '                    Next
    '                End If

    '                'otros
    '                If tablaOtrosN.Rows.Count > 0 Then
    '                    For t As Integer = 0 To tablaOtrosN.Rows.Count - 1
    '                        xmls += "<PLZ:OtrMetodoPago MetPagoPol=""" + tablaOtrosN.Rows(t)(0).ToString + """ Fecha=""" + tablaOtrosN.Rows(t)(1).ToString + """ Benef=""" + RC(tablaOtrosN.Rows(t)(2).ToString) + """ RFC=""" + tablaOtrosN.Rows(t)(3).ToString + """ Monto=""" + Double.Parse(tablaOtrosN.Rows(t)(4)).ToString("0.00") + """"
    '                        cadena2 += tablaOtrosN.Rows(t)(0).ToString + "|" + tablaOtrosN.Rows(t)(1).ToString + "|" + tablaOtrosN.Rows(t)(2).ToString + "|" + tablaOtrosN.Rows(t)(3).ToString + "|" + Double.Parse(tablaOtrosN.Rows(t)(4)).ToString("0.00") + "|"
    '                        If tablaOtrosN.Rows(t)(5).ToString <> "MXN" Then
    '                            xmls += " Moneda=""" + tablaOtrosN.Rows(t)(5).ToString + """ TipCamb=""" + tablaOtrosN.Rows(t)(6).ToString + """"
    '                            cadena2 += tablaOtrosN.Rows(t)(5).ToString + "|" + tablaOtrosN.Rows(t)(6).ToString + "|"
    '                        End If
    '                        xmls += "/>"
    '                    Next
    '                End If

    '                xmls += "</PLZ:Transaccion>" + vbCrLf
    '            End If

    '        Next
    '        xmls += "</PLZ:Poliza>" + vbCrLf
    '    Next

    '    cadena = "||1.1|" + s.RFC + "|" + fechita.Month.ToString("00") + "|" + fechita.Year.ToString + "|" + pTipoSolicitud + "|"
    '    If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
    '        cadena += pNumOrden + "|"
    '    Else
    '        cadena += pNumTramite + "|"
    '    End If
    '    cadena += cadena2
    '    cadena += "|"
    '    Dim Archivos As New dbSucursalesArchivos
    '    Dim en As New Encriptador
    '    Dim Sello As String = ""

    '    Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
    '    Sello = en.GeneraSello(cadena, Archivos.RutaCer, "2011")

    '    Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, True)
    '    en.Leex509(Archivos.RutaCer)

    '    xml2 += "<?xml version=""1.0"" encoding=""UTF-8""?>"
    '    xml2 += "<PLZ:Polizas xsi:schemaLocation=""http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/PolizasPeriodo/PolizasPeriodo_1_1.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:PLZ=""http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/PolizasPeriodo"""
    '    xml2 += " Version=""1.1"" RFC=""" + s.RFC + """ Mes=""" + fechita.Month.ToString("00") + """ Anio=""" + fechita.Year.ToString + """ TipoSolicitud=""" + pTipoSolicitud + """"
    '    If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
    '        xml2 += " NumOrden=""" + pNumOrden + """"
    '    Else
    '        xml2 += " NumTramite=""" + pNumTramite + """"
    '    End If
    '    xml2 += " Sello=""" + Sello + """"
    '    xml2 += " noCertificado=""" + en.Seriex509 + """"
    '    xml2 += " Certificado=""" + en.Certificado64 + """"
    '    xml2 += ">"

    '    xml2 += xmls

    '    xml2 += "</PLZ:Polizas>"


    '    xmldoc.LoadXml(xml2)
    '    Return xmldoc
    'End Function
    Public Function GeneraxmlPolizas(ByVal pdesde As String, ByVal pHasta As String, ByVal pTipoSolicitud As String, ByVal pNumOrden As String, ByVal pNumTramite As String, ByVal pSEllo As String, ByVal pNoCertificado As String, ByVal pCertificado As String)
        Dim xmls As String = ""
        Dim tabla As DataTable
        Dim xml2 As String = ""
        Dim cadena As String = ""
        Dim cadena2 As String = ""
        Dim xmldoc As New System.Xml.XmlDocument

        Dim fechita As Date = Date.Parse(pdesde)

        Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)





        Dim DS As New DataSet
        Comm.CommandText = "select id,IF(tipo='I',1,IF (tipo='E',2,3)) as tipo,concat(tipo,numero) ,fecha as Fecha,concepto as Concepto from tblpolizas where fecha>='" + pdesde + "' and fecha<='" + pHasta + "' and tipo<>'O' and tipo<>'A' "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpoliza")
        tabla = DS.Tables("tblpoliza")

        For i As Integer = 0 To tabla.Rows.Count - 1
            tablaChequeN.Clear()
            tablaTransN.Clear()
            tablaDetalles.Clear()
            tablaComproN.Clear()
            '  tablaComproNac2.Clear()
            tablaComproEN.Clear()
            tablaOtrosN.Clear()
            xmls += "<PLZ:Poliza NumUnIdenPol=""" + RC(tabla.Rows(i)(2).ToString) + """ Fecha=""" + tabla.Rows(i)(3).ToString.Replace("/", "-") + """ Concepto=""" + RC(tabla.Rows(i)(4).ToString.Trim) + """>" + vbCrLf
            'Detalles
            cadena2 += tabla.Rows(i)(2).ToString + "|" + tabla.Rows(i)(3).ToString.Replace("/", "-") + "|" + tabla.Rows(i)(4).ToString.Trim + "|"

            Dim DS4 As New DataSet
            Comm.CommandText = "select tblpolizasdetalles.id, REPLACE(tblpolizasdetalles.cuenta, ' ','')as cuenta,tblpolizasdetalles.descripcion,if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo) as cargo,if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono) as abonom,tblCContables.Descripcion from tblpolizasdetalles inner join tblccontables on tblccontables.idCContable=tblpolizasdetalles.idCuenta where idPoliza=" + tabla.Rows(i)(0).ToString
            Dim DA4 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA4.Fill(DS4, "tblpoliza")
            tablaDetalles = DS4.Tables("tblpoliza")
            For j As Integer = 0 To tablaDetalles.Rows.Count - 1
                xmls += "<PLZ:Transaccion NumCta=""" + tablaDetalles.Rows(j)(1).ToString + """ DesCta=""" + RC(tablaDetalles.Rows(j)(5).ToString) + """ Concepto=""" + RC(tablaDetalles.Rows(j)(2).ToString) + """ Debe=""" + Double.Parse(tablaDetalles.Rows(j)(3).ToString).ToString("0.00") + """ Haber=""" + Double.Parse(tablaDetalles.Rows(j)(4).ToString).ToString("0.00") + """"
                cadena2 += tablaDetalles.Rows(j)(1).ToString + "|" + tablaDetalles.Rows(j)(2).ToString + "|" + Double.Parse(tablaDetalles.Rows(j)(3).ToString).ToString("0.00") + "|" + Double.Parse(tablaDetalles.Rows(j)(4).ToString).ToString("0.00") + "|"

                'cheques
                Dim DS2 As New DataSet
                Comm.CommandText = "select tblcontabilidadcheque.numero as numero ,tblcontabilidadcheque.banco as banco ,tblcontabilidadcheque.bancoemisor,tblcontabilidadcheque.ctaOri as ctaOri,tblcontabilidadcheque.fecha as fecha,tblcontabilidadcheque.benef as benef,tblcontabilidadcheque.rfc as rfc,tblcontabilidadcheque.monto as monto,tblmonedassat.codigo,tblcontabilidadcheque.tipoCambio from tblcontabilidadcheque inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcheque.moneda where tblcontabilidadcheque.idrenglon=" + tablaDetalles.Rows(j)(0).ToString
                Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
                DA2.Fill(DS2, "tblcontabilidadcheque")
                tablaChequeN = DS2.Tables("tblcontabilidadcheque")

                'trans
                Dim DS3 As New DataSet
                Comm.CommandText = "select tblcontabilidadtrans.ctaOri as ctaOri,tblcontabilidadtrans.bancoOri as bancoOri,tblcontabilidadtrans.bancoOrie ,tblcontabilidadtrans.ctaDest as ctaDest,tblcontabilidadtrans.bancoDest as bancoDest,tblcontabilidadtrans.bancoDeste,tblcontabilidadtrans.fecha as fecha,tblcontabilidadtrans.benef as benef,tblcontabilidadtrans.rfc as rfc,tblcontabilidadtrans.monto as monto,tblmonedassat.codigo,tblcontabilidadtrans.tipoCambio from tblcontabilidadtrans inner join tblmonedassat on tblmonedassat.id=tblcontabilidadtrans.moneda where idRenglon=" + tablaDetalles.Rows(j)(0).ToString
                Dim DA3 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
                DA3.Fill(DS3, "tblpoliza")
                tablaTransN = DS3.Tables("tblpoliza")

                'compro
                Dim DS5 As New DataSet
                Comm.CommandText = "select UUID,rfc,monto,tblmonedassat.codigo,tipocambio from tblcontabilidadcompro inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcompro.moneda where idRenglon=" + tablaDetalles.Rows(j)(0).ToString
                Dim DA5 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
                DA5.Fill(DS5, "tblpoliza")
                tablaComproN = DS5.Tables("tblpoliza")

                'comproNall2
                Dim DS6 As New DataSet
                Comm.CommandText = "select Serie,folio,rfc,monto,tblmonedassat.codigo,tipoCambio from tblcontabilidadcompro2 inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcompro2.moneda where idRenglon=" + tablaDetalles.Rows(j)(0).ToString
                Dim DA6 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
                DA6.Fill(DS6, "tblpoliza")
                tablaComproNac2 = DS6.Tables("tblpoliza")

                'compro extranjero
                Dim DS7 As New DataSet
                Comm.CommandText = "select numFactura,taxID,monto,tblmonedassat.codigo,tipoCambio from tblcontabilidadcomproe inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcomproe.moneda where idRenglon=" + tablaDetalles.Rows(j)(0).ToString
                Dim DA7 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
                DA7.Fill(DS7, "tblpoliza")
                tablaComproEN = DS7.Tables("tblpoliza")

                'compro otros
                Dim DS8 As New DataSet
                Comm.CommandText = "select tblmetodospago.clave,fecha,benef,rfc,monto,tblmonedassat.codigo,tipoCambio from tblcontabilidadotros inner join tblmonedassat on tblmonedassat.id=tblcontabilidadotros.moneda inner join tblmetodospago on  tblmetodospago.id=tblcontabilidadotros.metodoPago where idRenglon=" + tablaDetalles.Rows(j)(0).ToString
                Dim DA8 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
                DA8.Fill(DS8, "tblpoliza")
                tablaOtrosN = DS8.Tables("tblpoliza")

                If tablaChequeN.Rows.Count = 0 And tablaComproN.Rows.Count = 0 And tablaTransN.Rows.Count = 0 And tablaComproNac2.Rows.Count = 0 And tablaComproEN.Rows.Count = 0 And tablaOtrosN.Rows.Count = 0 Then
                    xmls += "/>" + vbCrLf
                Else
                    xmls += ">" + vbCrLf
                    'Cheques 
                    If tablaChequeN.Rows.Count > 0 Then
                        For t As Integer = 0 To tablaChequeN.Rows.Count - 1
                            xmls += "<PLZ:Cheque Num=""" + tablaChequeN.Rows(t)(0).ToString.Trim + """ BanEmisNal=""" + tablaChequeN.Rows(t)(1).ToString.Trim + """"
                            cadena2 += tablaChequeN.Rows(t)(0).ToString.Trim + "|" + tablaChequeN.Rows(t)(1).ToString.Trim + "|"
                            If tablaChequeN.Rows(t)(2).ToString <> "" Then
                                xmls += " BanEmisExt=""" + RC(tablaChequeN.Rows(t)(2).ToString.Trim) + """"
                                cadena2 += tablaChequeN.Rows(t)(2).ToString.Trim + "|"
                            End If
                            xmls += " CtaOri=""" + tablaChequeN.Rows(t)(3).ToString + """ Fecha=""" + tablaChequeN.Rows(t)(4).ToString.Replace("/", "-") + "T00:00:00" + """ Benef=""" + RC(tablaChequeN.Rows(t)(5).ToString.Trim) + """ RFC=""" + tablaChequeN.Rows(t)(6).ToString + """ Monto=""" + Double.Parse(tablaChequeN.Rows(t)(7)).ToString("0.00") + """"
                            cadena2 += tablaChequeN.Rows(t)(3).ToString + "|" + tablaChequeN.Rows(t)(4).ToString.Replace("/", "-") + "T00:00:00" + "|" + tablaChequeN.Rows(t)(5).ToString.Trim + "|" + tablaChequeN.Rows(t)(6).ToString + "|" + Double.Parse(tablaChequeN.Rows(t)(7)).ToString("0.00") + "|"

                            If tablaChequeN.Rows(t)(8) <> "MXN" Then
                                xmls += "  Moneda=""" + tablaChequeN.Rows(t)(8).ToString + """ TipCamb=""" + tablaChequeN.Rows(t)(9).ToString + """"
                                cadena2 += tablaChequeN.Rows(t)(8).ToString + "|" + tablaChequeN.Rows(t)(9).ToString + "|"
                            End If
                            xmls += "/>" + vbCrLf



                        Next
                    End If
                    'transferencia
                    If tablaTransN.Rows.Count > 0 Then
                        For t As Integer = 0 To tablaTransN.Rows.Count - 1
                            xmls += "<PLZ:Transferencia CtaOri=""" + tablaTransN.Rows(t)(0).ToString + """ BancoOriNal=""" + tablaTransN.Rows(t)(1).ToString + """"
                            cadena2 += tablaTransN.Rows(t)(0).ToString + "|" + tablaTransN.Rows(t)(1).ToString + "|"

                            If tablaTransN.Rows(t)(2).ToString <> "" Then
                                xmls += " BancoOriExt=""" + RC(tablaTransN.Rows(t)(2).ToString) + """"
                                cadena2 += tablaTransN.Rows(t)(2).ToString + "|"
                            End If
                            xmls += " CtaDest=""" + tablaTransN.Rows(t)(3).ToString + """ BancoDestNal=""" + tablaTransN.Rows(t)(4).ToString + """"
                            cadena2 += tablaTransN.Rows(t)(3).ToString + "|" + tablaTransN.Rows(t)(4).ToString + "|"

                            If tablaTransN.Rows(t)(5).ToString <> "" Then
                                xmls += " BancoDestExt=""" + RC(tablaTransN.Rows(t)(5).ToString) + """"
                                cadena2 += tablaTransN.Rows(t)(5).ToString + "|"
                            End If

                            xmls += " Fecha=""" + tablaTransN.Rows(t)(6).ToString.Replace("/", "-") + "T00:00:00" + """ Benef=""" + RC(tablaTransN.Rows(t)(7).ToString) + """ RFC=""" + tablaTransN.Rows(t)(8).ToString + """ Monto=""" + Double.Parse(tablaTransN.Rows(t)(9)).ToString("0.00") + """"
                            cadena2 += tablaTransN.Rows(t)(6).ToString.Replace("/", "-") + "T00:00:00" + "|" + tablaTransN.Rows(t)(7).ToString + "|" + tablaTransN.Rows(t)(8).ToString + "|" + Double.Parse(tablaTransN.Rows(t)(9)).ToString("0.00") + "|"

                            If tablaTransN.Rows(t)(10).ToString <> "MXN" Then
                                xmls += " Moneda=""" + tablaTransN.Rows(t)(10).ToString + """ TipCamb=""" + tablaTransN.Rows(t)(11).ToString + """"
                                cadena2 += tablaTransN.Rows(t)(10).ToString + "|" + tablaTransN.Rows(t)(11).ToString + "|"
                            End If
                            xmls += "/>"
                        Next
                    End If

                    'comprobante
                    If tablaComproN.Rows.Count > 0 Then
                        For t As Integer = 0 To tablaComproN.Rows.Count - 1
                            xmls += "<PLZ:CompNal UUID_CFDI=""" + RC(tablaComproN.Rows(t)(0).ToString) + """ RFC=""" + tablaComproN.Rows(t)(1).ToString + """ MontoTotal=""" + Double.Parse(tablaComproN.Rows(t)(2)).ToString("0.00") + """"
                            cadena2 += tablaComproN.Rows(t)(0).ToString + "|"
                            If tablaComproN.Rows(t)(3).ToString <> "MXN" Then
                                xmls += "  Moneda=""" + tablaComproN.Rows(t)(3).ToString + """ TipCamb = """ + tablaComproN.Rows(t)(4).ToString + """"
                            End If
                            xmls += "/>"
                        Next
                    End If

                    'comprobantes nac 2
                    If tablaComproNac2.Rows.Count > 0 Then
                        For t As Integer = 0 To tablaComproNac2.Rows.Count - 1
                            xmls += "<PLZ:CompNalOtr CFD_CBB_Serie=""" + RC(tablaComproNac2.Rows(t)(0).ToString) + """ CFD_CBB_NumFol=""" + RC(tablaComproNac2.Rows(t)(1).ToString) + """ RFC=""" + tablaComproNac2.Rows(t)(2).ToString + """ MontoTotal=""" + Double.Parse(tablaComproNac2.Rows(t)(3)).ToString("0.00") + """"
                            cadena2 += tablaComproNac2.Rows(t)(0).ToString + "|" + tablaComproNac2.Rows(t)(1).ToString + "|"
                            If tablaComproNac2.Rows(t)(4).ToString <> "MXN" Then
                                xmls += " Moneda=""" + tablaComproNac2.Rows(t)(4).ToString + """ TipCamb=""" + tablaComproNac2.Rows(t)(5).ToString + """"
                            End If

                            xmls += "/>"
                        Next
                    End If


                    'comprobantes ex
                    If tablaComproEN.Rows.Count > 0 Then
                        For t As Integer = 0 To tablaComproEN.Rows.Count - 1
                            xmls += "<PLZ:CompExt NumFactExt=""" + RC(tablaComproEN.Rows(t)(0).ToString) + """ TaxID=""" + RC(tablaComproEN.Rows(t)(1).ToString) + """ MontoTotal=""" + Double.Parse(tablaComproEN.Rows(t)(2)).ToString("0.00") + """"
                            cadena2 += tablaComproEN.Rows(t)(0).ToString + "|"
                            If tablaComproEN.Rows(t)(3).ToString <> "MXN" Then
                                xmls += " Moneda=""" + tablaComproEN.Rows(t)(3).ToString + """ TipCamb=""" + tablaComproEN.Rows(t)(4).ToString + """"
                            End If
                            xmls += "/>"
                        Next
                    End If

                    'otros
                    If tablaOtrosN.Rows.Count > 0 Then
                        For t As Integer = 0 To tablaOtrosN.Rows.Count - 1
                            xmls += "<PLZ:OtrMetodoPago MetPagoPol=""" + tablaOtrosN.Rows(t)(0).ToString + """ Fecha=""" + tablaOtrosN.Rows(t)(1).ToString.Replace("/", "-") + "T00:00:00" + """ Benef=""" + RC(tablaOtrosN.Rows(t)(2).ToString) + """ RFC=""" + tablaOtrosN.Rows(t)(3).ToString + """ Monto=""" + Double.Parse(tablaOtrosN.Rows(t)(4)).ToString("0.00") + """"
                            cadena2 += tablaOtrosN.Rows(t)(0).ToString + "|" + tablaOtrosN.Rows(t)(1).ToString.Replace("/", "-") + "T00:00:00" + "|" + tablaOtrosN.Rows(t)(2).ToString + "|" + tablaOtrosN.Rows(t)(3).ToString + "|" + Double.Parse(tablaOtrosN.Rows(t)(4)).ToString("0.00") + "|"
                            If tablaOtrosN.Rows(t)(5).ToString <> "MXN" Then
                                xmls += " Moneda=""" + tablaOtrosN.Rows(t)(5).ToString + """ TipCamb=""" + tablaOtrosN.Rows(t)(6).ToString + """"
                                cadena2 += tablaOtrosN.Rows(t)(5).ToString + "|" + tablaOtrosN.Rows(t)(6).ToString + "|"
                            End If
                            xmls += "/>"
                        Next
                    End If

                    xmls += "</PLZ:Transaccion>" + vbCrLf
                End If

            Next
            xmls += "</PLZ:Poliza>" + vbCrLf
        Next

        cadena = "||1.1|" + s.RFC + "|" + fechita.Month.ToString("00") + "|" + fechita.Year.ToString + "|" + pTipoSolicitud + "|"
        If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
            cadena += pNumOrden + "|"
        Else
            cadena += pNumTramite + "|"
        End If
        cadena += cadena2
        cadena += "|"
        While cadena.IndexOf("  ") <> -1
            cadena = Replace(cadena, "  ", " ")
        End While
        Dim Archivos As New dbSucursalesArchivos
        Dim en As New Encriptador
        Dim Sello As String = ""

        Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
        Sello = en.GeneraSello(cadena, Archivos.RutaCer, "2011", False)

        Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)

        xml2 += "<?xml version=""1.0"" encoding=""UTF-8""?>"
        xml2 += "<PLZ:Polizas xsi:schemaLocation=""www.sat.gob.mx/esquemas/ContabilidadE/1_1/PolizasPeriodo http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/PolizasPeriodo/PolizasPeriodo_1_1.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:PLZ=""www.sat.gob.mx/esquemas/ContabilidadE/1_1/PolizasPeriodo"""
        xml2 += " Version=""1.1"" RFC=""" + s.RFC + """ Mes=""" + fechita.Month.ToString("00") + """ Anio=""" + fechita.Year.ToString + """ TipoSolicitud=""" + pTipoSolicitud + """"
        If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
            xml2 += " NumOrden=""" + pNumOrden + """"
        Else
            xml2 += " NumTramite=""" + pNumTramite + """"
        End If
        xml2 += " Sello=""" + Sello + """"
        xml2 += " noCertificado=""" + en.Seriex509 + """"
        xml2 += " Certificado=""" + en.Certificado64 + """"
        xml2 += ">"

        xml2 += xmls

        xml2 += "</PLZ:Polizas>"


        xmldoc.LoadXml(xml2)
        Return xmldoc
    End Function

    Public Function verificarCuentas(ByVal pDesde As String, ByVal pHasta As String)
        'Dim cadena As String
        Dim contados As Integer
        Comm.CommandText = "select count(idCContable) from tblccontables where fecha<='" + pHasta + "' and fecha>='" + pDesde + "'"
        contados = Comm.ExecuteScalar

        Return contados
    End Function

    Public Function CuentasSinAgrupar()
        'Dim cadena As String
        Dim contados As Integer
        Comm.CommandText = "select count(idContable) from tblccontables where idccontable<=0"
        contados = Comm.ExecuteScalar
        Return contados
    End Function
    'Public Function xmlAuxiliarFolios(ByVal pdesde As String, ByVal pHasta As String, ByVal pTipoSolicitud As String, ByVal pNumOrden As String, ByVal pNumTramite As String, ByVal pSEllo As String, ByVal pNoCertificado As String, ByVal pCertificado As String)
    '    Dim xmls As String = ""
    '    Dim tabla As DataTable
    '    Dim xmls2 As String = ""
    '    Dim xmldoc As New System.Xml.XmlDocument
    '    Dim cadena As String = ""
    '    Dim cadena2 As String = ""
    '    Dim fechita As Date = Date.Parse(pdesde)

    '    Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)

    '    cadena = "||1.1|" + s.RFC + "|" + fechita.Month.ToString("00") + "|" + fechita.Year.ToString + "|" + pTipoSolicitud + "|"
    '    If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
    '        cadena += pNumOrden + "|"
    '    Else
    '        cadena += pNumTramite + "|"
    '    End If
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select id,concat(tipo,numero) ,fecha as Fecha from tblpolizas where fecha>='" + pdesde + "' and fecha<='" + pHasta + "' and tipo<>'O' and tipo<>'A'"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblpoliza")
    '    tabla = DS.Tables("tblpoliza")

    '    For i As Integer = 0 To tabla.Rows.Count - 1



    '        '  For j As Integer = 0 To tabla.Rows.Count - 1

    '        'compro
    '        Dim DS5 As New DataSet
    '        Comm.CommandText = "select UUID,rfc,monto,tblmonedassat.codigo,tipocambio from tblcontabilidadcompro inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcompro.moneda where idPoliza=" + tabla.Rows(i)(0).ToString
    '        Dim DA5 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '        DA5.Fill(DS5, "tblpoliza")
    '        tablaComproN = DS5.Tables("tblpoliza")

    '        'comproNall2
    '        Dim DS6 As New DataSet
    '        Comm.CommandText = "select Serie,folio,rfc,monto,tblmonedassat.codigo,tipoCambio from tblcontabilidadcompro2 inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcompro2.moneda where idPoliza=" + tabla.Rows(i)(0).ToString
    '        Dim DA6 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '        DA6.Fill(DS6, "tblpoliza")
    '        tablaComproNac2 = DS6.Tables("tblpoliza")

    '        'compro extranjero
    '        Dim DS7 As New DataSet
    '        Comm.CommandText = "select numFactura,taxID,monto,tblmonedassat.codigo,tipoCambio from tblcontabilidadcomproe inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcomproe.moneda where idPoliza=" + tabla.Rows(i)(0).ToString
    '        Dim DA7 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '        DA7.Fill(DS7, "tblpoliza")
    '        tablaComproEN = DS7.Tables("tblpoliza")

    '        If tablaComproN.Rows.Count = 0 And tablaComproNac2.Rows.Count = 0 And tablaComproEN.Rows.Count = 0 Then
    '            'xmls2 = ""
    '            'cadena2 = ""
    '        Else
    '            'comprobante
    '            cadena2 += ""
    '            xmls2 += "<RepAux:DetAuxFol NumUnIdenPol=""" + tabla.Rows(i)(1).ToString + """ Fecha=""" + tabla.Rows(i)(2).ToString + """>" + vbCrLf
    '            cadena2 += tabla.Rows(i)(1).ToString + "|" + tabla.Rows(i)(2).ToString + "|"

    '            If tablaComproN.Rows.Count > 0 Then
    '                For t As Integer = 0 To tablaComproN.Rows.Count - 1
    '                    xmls2 += "<RepAux:ComprNal UUID_CFDI=""" + RC(tablaComproN.Rows(t)(0).ToString) + """ MontoTotal=""" + Double.Parse(tablaComproN.Rows(t)(2)).ToString("0.00") + """ RFC=""" + tablaComproN.Rows(t)(1).ToString + """"
    '                    cadena2 += tablaComproN.Rows(t)(0).ToString + "|" + tablaComproN.Rows(t)(1).ToString + "|" + Double.Parse(tablaComproN.Rows(t)(2)).ToString("0.00") + "|"
    '                    If tablaComproN.Rows(t)(3).ToString <> "MXN" Then
    '                        xmls2 += "  Moneda=""" + tablaComproN.Rows(t)(3).ToString + """ TipCamb = """ + tablaComproN.Rows(t)(4).ToString + """"
    '                        cadena2 += tablaComproN.Rows(t)(3).ToString + "|" + tablaComproN.Rows(t)(4).ToString + "|"
    '                    End If
    '                    xmls2 += "/>"
    '                Next
    '            End If

    '            'comprobantes nac 2
    '            If tablaComproNac2.Rows.Count > 0 Then
    '                For t As Integer = 0 To tablaComproNac2.Rows.Count - 1
    '                    xmls2 += "<RepAux:CompNalOtr CFD_CBB_Serie=""" + RC(tablaComproNac2.Rows(t)(0).ToString) + """ CFD_CBB_NumFol=""" + tablaComproNac2.Rows(t)(1).ToString + """ MontoTotal=""" + Double.Parse(tablaComproNac2.Rows(t)(3)).ToString("0.00") + """ RFC=""" + tablaComproNac2.Rows(t)(2).ToString + """"
    '                    cadena2 += tablaComproNac2.Rows(t)(0).ToString + "|" + tablaComproNac2.Rows(t)(1).ToString + "|" + tablaComproNac2.Rows(t)(2).ToString + "|" + Double.Parse(tablaComproNac2.Rows(t)(3)).ToString("0.00") + "|"
    '                    If tablaComproNac2.Rows(t)(4).ToString <> "MXN" Then
    '                        xmls2 += " Moneda=""" + tablaComproNac2.Rows(t)(4).ToString + """ TipCamb=""" + tablaComproNac2.Rows(t)(5).ToString + """"
    '                        cadena2 += tablaComproNac2.Rows(t)(4).ToString + "|" + tablaComproNac2.Rows(t)(5).ToString + "|"
    '                    End If

    '                    xmls2 += "/>"
    '                Next
    '            End If

    '            'comprobantes ex
    '            If tablaComproEN.Rows.Count > 0 Then
    '                For t As Integer = 0 To tablaComproEN.Rows.Count - 1
    '                    xmls2 += "<RepAux:ComprExt NumFactExt=""" + RC(tablaComproEN.Rows(t)(0).ToString) + """ TaxID=""" + RC(tablaComproEN.Rows(t)(1).ToString) + """ MontoTotal=""" + Double.Parse(tablaComproEN.Rows(t)(2)).ToString("0.00") + """"
    '                    cadena2 += tablaComproEN.Rows(t)(0).ToString + "|" + Double.Parse(tablaComproEN.Rows(t)(2)).ToString("0.00") + "|"
    '                    If tablaComproEN.Rows(t)(3).ToString <> "MXN" Then
    '                        xmls2 += " Moneda=""" + tablaComproEN.Rows(t)(3).ToString + """ TipCamb=""" + tablaComproEN.Rows(t)(4).ToString + """"
    '                        cadena2 += tablaComproEN.Rows(t)(3).ToString + "|" + tablaComproEN.Rows(t)(4).ToString + "|"
    '                    End If
    '                    xmls2 += "/>"
    '                Next
    '            End If
    '            xmls2 += "</RepAux:DetAuxFol>" + vbCrLf
    '        End If

    '        ' Next
    '    Next
    '    cadena += cadena2
    '    cadena += "|"
    '    Dim Archivos As New dbSucursalesArchivos
    '    Dim en As New Encriptador
    '    Dim Sello As String = ""

    '    Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
    '    Sello = en.GeneraSello(cadena, Archivos.RutaCer, "2011")

    '    Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, True)
    '    en.Leex509(Archivos.RutaCer)

    '    xmls += "<?xml version=""1.0"" encoding=""UTF-8""?>"
    '    xmls += "<RepAux:RepAuxFol xsi:schemaLocation=""http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/AuxiliarFolios/AuxiliarFolios_1_1.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:RepAux=""http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/AuxiliarFolios"""
    '    xmls += " Version=""1.1"" RFC=""" + s.RFC + """ Mes=""" + fechita.Month.ToString("00") + """ Anio=""" + fechita.Year.ToString + """ TipoSolicitud=""" + pTipoSolicitud + """"
    '    If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
    '        xmls += " NumOrden=""" + pNumOrden + """"
    '    Else
    '        xmls += " NumTramite=""" + pNumTramite + """"
    '    End If
    '    xmls += " Sello=""" + Sello + """"
    '    xmls += " noCertificado=""" + en.Seriex509 + """"
    '    xmls += " Certificado=""" + en.Certificado64 + """"
    '    xmls += ">"
    '    xmls += xmls2
    '    xmls += "</RepAux:RepAuxFol>"
    '    xmldoc.LoadXml(xmls)
    '    Return xmldoc
    'End Function

    Public Function xmlAuxiliarFolios(ByVal pdesde As String, ByVal pHasta As String, ByVal pTipoSolicitud As String, ByVal pNumOrden As String, ByVal pNumTramite As String, ByVal pSEllo As String, ByVal pNoCertificado As String, ByVal pCertificado As String)
        Dim xmls As String = ""
        Dim tabla As DataTable
        Dim xmls2 As String = ""
        Dim xmldoc As New System.Xml.XmlDocument
        Dim cadena As String = ""
        Dim cadena2 As String = ""
        Dim fechita As Date = Date.Parse(pdesde)

        Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)

        cadena = "||1.1|" + s.RFC + "|" + fechita.Month.ToString("00") + "|" + fechita.Year.ToString + "|" + pTipoSolicitud + "|"
        If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
            cadena += pNumOrden + "|"
        Else
            cadena += pNumTramite + "|"
        End If
        Dim DS As New DataSet
        Comm.CommandText = "select id,concat(tipo,numero) ,fecha as Fecha from tblpolizas where fecha>='" + pdesde + "' and fecha<='" + pHasta + "' and tipo<>'O' and tipo<>'A'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpoliza")
        tabla = DS.Tables("tblpoliza")

        For i As Integer = 0 To tabla.Rows.Count - 1



            '  For j As Integer = 0 To tabla.Rows.Count - 1

            'compro
            Dim DS5 As New DataSet
            Comm.CommandText = "select UUID,rfc,monto,tblmonedassat.codigo,tipocambio from tblcontabilidadcompro inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcompro.moneda where idPoliza=" + tabla.Rows(i)(0).ToString
            Dim DA5 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA5.Fill(DS5, "tblpoliza")
            tablaComproN = DS5.Tables("tblpoliza")

            'comproNall2
            Dim DS6 As New DataSet
            Comm.CommandText = "select Serie,folio,rfc,monto,tblmonedassat.codigo,tipoCambio from tblcontabilidadcompro2 inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcompro2.moneda where idPoliza=" + tabla.Rows(i)(0).ToString
            Dim DA6 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA6.Fill(DS6, "tblpoliza")
            tablaComproNac2 = DS6.Tables("tblpoliza")

            'compro extranjero
            Dim DS7 As New DataSet
            Comm.CommandText = "select numFactura,taxID,monto,tblmonedassat.codigo,tipoCambio from tblcontabilidadcomproe inner join tblmonedassat on tblmonedassat.id=tblcontabilidadcomproe.moneda where idPoliza=" + tabla.Rows(i)(0).ToString
            Dim DA7 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA7.Fill(DS7, "tblpoliza")
            tablaComproEN = DS7.Tables("tblpoliza")

            If tablaComproN.Rows.Count = 0 And tablaComproNac2.Rows.Count = 0 And tablaComproEN.Rows.Count = 0 Then
                'xmls2 = ""
                'cadena2 = ""
            Else
                'comprobante
                cadena2 += ""
                xmls2 += "<RepAux:DetAuxFol NumUnIdenPol=""" + tabla.Rows(i)(1).ToString + """ Fecha=""" + tabla.Rows(i)(2).ToString + """>" + vbCrLf
                cadena2 += tabla.Rows(i)(1).ToString + "|" + tabla.Rows(i)(2).ToString + "|"

                If tablaComproN.Rows.Count > 0 Then
                    For t As Integer = 0 To tablaComproN.Rows.Count - 1
                        xmls2 += "<RepAux:ComprNal UUID_CFDI=""" + RC(tablaComproN.Rows(t)(0).ToString) + """ MontoTotal=""" + Double.Parse(tablaComproN.Rows(t)(2)).ToString("0.00") + """ RFC=""" + tablaComproN.Rows(t)(1).ToString + """"
                        cadena2 += tablaComproN.Rows(t)(0).ToString + "|" + tablaComproN.Rows(t)(1).ToString + "|" + Double.Parse(tablaComproN.Rows(t)(2)).ToString("0.00") + "|"
                        If tablaComproN.Rows(t)(3).ToString <> "MXN" Then
                            xmls2 += "  Moneda=""" + tablaComproN.Rows(t)(3).ToString + """ TipCamb = """ + tablaComproN.Rows(t)(4).ToString + """"
                            cadena2 += tablaComproN.Rows(t)(3).ToString + "|" + tablaComproN.Rows(t)(4).ToString + "|"
                        End If
                        xmls2 += "/>"
                    Next
                End If

                'comprobantes nac 2
                If tablaComproNac2.Rows.Count > 0 Then
                    For t As Integer = 0 To tablaComproNac2.Rows.Count - 1
                        xmls2 += "<RepAux:CompNalOtr CFD_CBB_Serie=""" + RC(tablaComproNac2.Rows(t)(0).ToString) + """ CFD_CBB_NumFol=""" + tablaComproNac2.Rows(t)(1).ToString + """ MontoTotal=""" + Double.Parse(tablaComproNac2.Rows(t)(3)).ToString("0.00") + """ RFC=""" + tablaComproNac2.Rows(t)(2).ToString + """"
                        cadena2 += tablaComproNac2.Rows(t)(0).ToString + "|" + tablaComproNac2.Rows(t)(1).ToString + "|" + tablaComproNac2.Rows(t)(2).ToString + "|" + Double.Parse(tablaComproNac2.Rows(t)(3)).ToString("0.00") + "|"
                        If tablaComproNac2.Rows(t)(4).ToString <> "MXN" Then
                            xmls2 += " Moneda=""" + tablaComproNac2.Rows(t)(4).ToString + """ TipCamb=""" + tablaComproNac2.Rows(t)(5).ToString + """"
                            cadena2 += tablaComproNac2.Rows(t)(4).ToString + "|" + tablaComproNac2.Rows(t)(5).ToString + "|"
                        End If

                        xmls2 += "/>"
                    Next
                End If

                'comprobantes ex
                If tablaComproEN.Rows.Count > 0 Then
                    For t As Integer = 0 To tablaComproEN.Rows.Count - 1
                        xmls2 += "<RepAux:ComprExt NumFactExt=""" + RC(tablaComproEN.Rows(t)(0).ToString) + """ TaxID=""" + RC(tablaComproEN.Rows(t)(1).ToString) + """ MontoTotal=""" + Double.Parse(tablaComproEN.Rows(t)(2)).ToString("0.00") + """"
                        cadena2 += tablaComproEN.Rows(t)(0).ToString + "|" + Double.Parse(tablaComproEN.Rows(t)(2)).ToString("0.00") + "|"
                        If tablaComproEN.Rows(t)(3).ToString <> "MXN" Then
                            xmls2 += " Moneda=""" + tablaComproEN.Rows(t)(3).ToString + """ TipCamb=""" + tablaComproEN.Rows(t)(4).ToString + """"
                            cadena2 += tablaComproEN.Rows(t)(3).ToString + "|" + tablaComproEN.Rows(t)(4).ToString + "|"
                        End If
                        xmls2 += "/>"
                    Next
                End If
                xmls2 += "</RepAux:DetAuxFol>" + vbCrLf
            End If

            ' Next
        Next
        cadena += cadena2
        cadena += "|"
        While cadena.IndexOf("  ") <> -1
            cadena = Replace(cadena, "  ", " ")
        End While
        Dim Archivos As New dbSucursalesArchivos
        Dim en As New Encriptador
        Dim Sello As String = ""

        Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
        Sello = en.GeneraSello(cadena, Archivos.RutaCer, "2011", False)

        Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)

        xmls += "<?xml version=""1.0"" encoding=""UTF-8""?>"
        xmls += "<RepAux:RepAuxFol xsi:schemaLocation=""www.sat.gob.mx/esquemas/ContabilidadE/1_1/AuxiliarFolios http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/AuxiliarFolios/AuxiliarFolios_1_2.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:RepAux=""www.sat.gob.mx/esquemas/ContabilidadE/1_1/AuxiliarFolios"""
        xmls += " Version=""1.1"" RFC=""" + s.RFC + """ Mes=""" + fechita.Month.ToString("00") + """ Anio=""" + fechita.Year.ToString + """ TipoSolicitud=""" + pTipoSolicitud + """"
        If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
            xmls += " NumOrden=""" + pNumOrden + """"
        Else
            xmls += " NumTramite=""" + pNumTramite + """"
        End If
        xmls += " Sello=""" + Sello + """"
        xmls += " noCertificado=""" + en.Seriex509 + """"
        xmls += " Certificado=""" + en.Certificado64 + """"
        xmls += ">"
        xmls += xmls2
        xmls += "</RepAux:RepAuxFol>"
        xmldoc.LoadXml(xmls)
        Return xmldoc
    End Function

    'Public Function xmlAuxiliarcuentas(ByVal pdesde As String, ByVal pHasta As String, ByVal pTipoSolicitud As String, ByVal pNumOrden As String, ByVal pNumTramite As String, ByVal pSEllo As String, ByVal pNoCertificado As String, ByVal pCertificado As String)
    '    Dim xmls As String = ""
    '    Dim tabla As DataTable
    '    Dim xmls2 As String = ""
    '    Dim xmls3 As String = ""
    '    Dim xmldoc As New System.Xml.XmlDocument
    '    Dim cuenta As String = ""
    '    Dim desCuenta As String = ""
    '    Dim saldoIni As Double = 0
    '    Dim saldoFinal As Double = 0
    '    Dim cadena As String = ""
    '    Dim cadena2 As String = ""

    '    SaldosIniciales(pdesde, Date.Parse(pdesde).Month.ToString("00"))

    '    Dim fechita As Date = Date.Parse(pdesde)

    '    Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)

    '    cadena = "||1.1|" + s.RFC + "|" + fechita.Month.ToString("00") + "|" + fechita.Year.ToString + "|" + pTipoSolicitud + "|"
    '    If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
    '        cadena += pNumOrden + "|"
    '    Else
    '        cadena += pNumTramite + "|"
    '    End If

    '    Dim DS As New DataSet
    '    Comm.CommandText = "select  concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),'',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),''),'',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),'')) as cuenta,tblccontables.Descripcion, "
    '    Comm.CommandText += " ifnull((select ifnull(saldoI,0) from tblcontabilidadsaldosi where concat(tblcontabilidadsaldosi.Cuenta,tblcontabilidadsaldosi.N1)=(select concat(Cuenta,N2) from tblccontables where idCContable=detalles.idCuenta) limit 1),0) as saldoInicial"
    '    Comm.CommandText += " ,tblPolizas.fecha,concat(tblPolizas.tipo,tblPolizas.numero),tblPolizas.concepto,if(detalles.cargo=-999999999,0,detalles.cargo) as cargo,if(detalles.abono=-999999999,0,detalles.abono) as abono,tblccontables.Naturaleza"
    '    Comm.CommandText += "  from tblpolizasdetalles as detalles inner join tblccontables on tblccontables.idCContable=detalles.idCuenta inner join  tblPolizas on tblPolizas.id=detalles.idPoliza where tblPolizas.fecha>='" + pdesde + "' and tblPolizas.fecha<='" + pHasta + "' and tblPolizas.tipo<>'O' and tblPolizas.tipo<>'A' order by concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),'',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),''),'',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),'')),concat(tblPolizas.tipo,LPAD(tblPolizas.numero,2,'0'))"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblpoliza")
    '    tabla = DS.Tables("tblpoliza")

    '    For i As Integer = 0 To tabla.Rows.Count - 1
    '        If i = 0 Then
    '            cuenta = tabla.Rows(i)(0).ToString
    '            desCuenta = tabla.Rows(i)(1).ToString
    '            saldoIni = tabla.Rows(i)(2).ToString
    '            saldoFinal = tabla.Rows(i)(2).ToString
    '        End If
    '        If cuenta = tabla.Rows(i)(0).ToString Then
    '            xmls3 += "<AuxiliarCtas:DetalleAux Fecha=""" + tabla.Rows(i)(3).ToString + """ NumUnIdenPol=""" + tabla.Rows(i)(4).ToString + """ Concepto=""" + RC(tabla.Rows(i)(5).ToString) + """ Debe=""" + tabla.Rows(i)(6).ToString + """ Haber=""" + tabla.Rows(i)(7).ToString + """/>" + vbCrLf
    '            cadena2 += tabla.Rows(i)(3).ToString + "|" + tabla.Rows(i)(4).ToString + "|" + tabla.Rows(i)(6).ToString + "|" + tabla.Rows(i)(7).ToString + "|"
    '            If tabla.Rows(i)(8).ToString = 0 Then 'es deudora
    '                saldoFinal += (tabla.Rows(i)(6).ToString - tabla.Rows(i)(7).ToString) 'cargo menos abono
    '            Else
    '                saldoFinal += (tabla.Rows(i)(7).ToString - tabla.Rows(i)(6).ToString) 'abono menos cargo
    '            End If

    '        Else

    '            xmls2 += "<AuxiliarCtas:Cuenta NumCta=""" + cuenta + """ DesCta=""" + RC(desCuenta) + """ SaldoIni=""" + saldoIni.ToString("0.00") + """ SaldoFin=""" + saldoFinal.ToString("0.00") + """>" + vbCrLf
    '            cadena += cuenta + "|" + desCuenta + "|" + saldoIni.ToString("0.00") + "|" + saldoFinal.ToString("0.00") + "|"
    '            cadena += cadena2
    '            xmls2 += xmls3
    '            xmls2 += "</AuxiliarCtas:Cuenta>" + vbCrLf

    '            xmls3 = ""
    '            cadena2 += ""

    '            cuenta = tabla.Rows(i)(0).ToString
    '            desCuenta = tabla.Rows(i)(1).ToString
    '            saldoIni = tabla.Rows(i)(2).ToString
    '            saldoFinal = tabla.Rows(i)(2).ToString

    '            xmls3 = "<AuxiliarCtas:DetalleAux Fecha=""" + tabla.Rows(i)(3).ToString + """ NumUnIdenPol=""" + tabla.Rows(i)(4).ToString + """ Concepto=""" + RC(tabla.Rows(i)(5).ToString) + """ Debe=""" + tabla.Rows(i)(6).ToString + """ Haber=""" + tabla.Rows(i)(7).ToString + """/>" + vbCrLf
    '            cadena2 += tabla.Rows(i)(3).ToString + "|" + tabla.Rows(i)(4).ToString + "|" + tabla.Rows(i)(6).ToString + "|" + tabla.Rows(i)(7).ToString + "|"

    '            If tabla.Rows(i)(8).ToString = 0 Then 'es deudora
    '                saldoFinal += (tabla.Rows(i)(6).ToString - tabla.Rows(i)(7).ToString) 'cargo menos abono
    '            Else
    '                saldoFinal += (tabla.Rows(i)(7).ToString - tabla.Rows(i)(6).ToString) 'abono menos cargo
    '            End If

    '            If i = tabla.Rows.Count - 1 Then 'si es el ultimo registro hay que hacer el nodo completo
    '                xmls2 += "<AuxiliarCtas:Cuenta NumCta=""" + cuenta + """ DesCta=""" + RC(desCuenta) + """ SaldoIni=""" + saldoIni.ToString("0.00") + """ SaldoFin=""" + saldoFinal.ToString("0.00") + """>" + vbCrLf
    '                cadena += cuenta + "|" + desCuenta + "|" + saldoIni.ToString("0.00") + "|" + saldoFinal.ToString("0.00") + "|"
    '                cadena += cadena2
    '                xmls2 += xmls3
    '                xmls2 += "</AuxiliarCtas:Cuenta>" + vbCrLf
    '            End If
    '        End If
    '    Next

    '    cadena += "|"
    '    Dim Archivos As New dbSucursalesArchivos
    '    Dim en As New Encriptador
    '    Dim Sello As String = ""

    '    Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
    '    Sello = en.GeneraSello(cadena, Archivos.RutaCer, "2011")

    '    Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, True)
    '    en.Leex509(Archivos.RutaCer)


    '    xmls += "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
    '    xmls += "<AuxiliarCtas:AuxiliarCtas xsi:schemaLocation=""http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/AuxiliarCtas/AuxiliarCtas_1_1.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:AuxiliarCtas=""http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/AuxiliarCtas"""
    '    xmls += " Version=""1.1"" RFC=""" + s.RFC + """ Mes=""" + fechita.Month.ToString("00") + """ Anio=""" + fechita.Year.ToString + """ TipoSolicitud=""" + pTipoSolicitud + """"
    '    If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
    '        xmls += " NumOrden=""" + pNumOrden + """"
    '    Else
    '        xmls += " NumTramite=""" + pNumTramite + """"
    '    End If
    '    xmls += " Sello=""" + Sello + """"
    '    xmls += " noCertificado=""" + en.Seriex509 + """"
    '    xmls += " Certificado=""" + en.Certificado64 + """"
    '    xmls += ">" + vbCrLf

    '    xmls += xmls2

    '    xmls += "</AuxiliarCtas:AuxiliarCtas>"
    '    xmldoc.LoadXml(xmls)
    '    Return xmldoc
    'End Function

    Public Function xmlAuxiliarcuentas(ByVal pdesde As String, ByVal pHasta As String, ByVal pTipoSolicitud As String, ByVal pNumOrden As String, ByVal pNumTramite As String, ByVal pSEllo As String, ByVal pNoCertificado As String, ByVal pCertificado As String)
        Dim xmls As String = ""
        Dim tabla As DataTable
        Dim xmls2 As String = ""
        Dim xmls3 As String = ""
        Dim xmldoc As New System.Xml.XmlDocument
        Dim cuenta As String = ""
        Dim desCuenta As String = ""
        Dim saldoIni As Double = 0
        Dim saldoFinal As Double = 0
        Dim cadena As String = ""
        Dim cadena2 As String = ""

        SaldosIniciales(pdesde, Date.Parse(pdesde).Month.ToString("00"))

        Dim fechita As Date = Date.Parse(pdesde)

        Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)

        cadena = "||1.1|" + s.RFC + "|" + fechita.Month.ToString("00") + "|" + fechita.Year.ToString + "|" + pTipoSolicitud + "|"
        If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
            cadena += pNumOrden + "|"
        Else
            cadena += pNumTramite + "|"
        End If

        Dim DS As New DataSet
        Comm.CommandText = "select  concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),'',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),''),'',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),'')) as cuenta,tblccontables.Descripcion, "
        Comm.CommandText += " ifnull((select ifnull(saldoI,0) from tblcontabilidadsaldosi where concat(tblcontabilidadsaldosi.Cuenta,tblcontabilidadsaldosi.N1)=(select concat(Cuenta,N2) from tblccontables where idCContable=detalles.idCuenta) limit 1),0) as saldoInicial"
        Comm.CommandText += " ,tblPolizas.fecha,concat(tblPolizas.tipo,tblPolizas.numero),tblPolizas.concepto,if(detalles.cargo=-999999999,0,detalles.cargo) as cargo,if(detalles.abono=-999999999,0,detalles.abono) as abono,tblccontables.Naturaleza"
        Comm.CommandText += "  from tblpolizasdetalles as detalles inner join tblccontables on tblccontables.idCContable=detalles.idCuenta inner join  tblPolizas on tblPolizas.id=detalles.idPoliza where tblPolizas.fecha>='" + pdesde + "' and tblPolizas.fecha<='" + pHasta + "' and tblPolizas.tipo<>'O' and tblPolizas.tipo<>'A' and (tblccontables.descontinuada like '%" + anio + "%')=False order by concat(LPAD(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),'',if(tblccontables.N2<>'',LPAD(tblccontables.N2," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N3<>'',LPAD(tblccontables.N3," + NNiv3.ToString + ",'0'),''),'',if(tblccontables.N4<>'',LPAD(tblccontables.N4," + NNiv2.ToString + ",'0'),''),'',if(tblccontables.N5<>'',LPAD(tblccontables.N5," + NNiv5.ToString + ",'0'),'')),concat(tblPolizas.tipo,LPAD(tblPolizas.numero,2,'0'))"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpoliza")
        tabla = DS.Tables("tblpoliza")

        For i As Integer = 0 To tabla.Rows.Count - 1
            If i = 0 Then
                cuenta = tabla.Rows(i)(0).ToString
                desCuenta = tabla.Rows(i)(1).ToString
                saldoIni = tabla.Rows(i)(2).ToString
                saldoFinal = tabla.Rows(i)(2).ToString
            End If
            If cuenta = tabla.Rows(i)(0).ToString Then
                xmls3 += "<AuxiliarCtas:DetalleAux Fecha=""" + tabla.Rows(i)(3).ToString + """ NumUnIdenPol=""" + tabla.Rows(i)(4).ToString + """ Concepto=""" + RC(tabla.Rows(i)(5).ToString) + """ Debe=""" + tabla.Rows(i)(6).ToString + """ Haber=""" + tabla.Rows(i)(7).ToString + """/>" + vbCrLf
                cadena2 += tabla.Rows(i)(3).ToString + "|" + tabla.Rows(i)(4).ToString + "|" + tabla.Rows(i)(6).ToString + "|" + tabla.Rows(i)(7).ToString + "|"
                If tabla.Rows(i)(8).ToString = 0 Then 'es deudora
                    saldoFinal += (tabla.Rows(i)(6).ToString - tabla.Rows(i)(7).ToString) 'cargo menos abono
                Else
                    saldoFinal += (tabla.Rows(i)(7).ToString - tabla.Rows(i)(6).ToString) 'abono menos cargo
                End If

            Else

                xmls2 += "<AuxiliarCtas:Cuenta NumCta=""" + cuenta + """ DesCta=""" + RC(desCuenta) + """ SaldoIni=""" + saldoIni.ToString("0.00") + """ SaldoFin=""" + saldoFinal.ToString("0.00") + """>" + vbCrLf
                cadena += cuenta + "|" + desCuenta + "|" + saldoIni.ToString("0.00") + "|" + saldoFinal.ToString("0.00") + "|"
                cadena += cadena2
                xmls2 += xmls3
                xmls2 += "</AuxiliarCtas:Cuenta>" + vbCrLf

                xmls3 = ""
                cadena2 += ""

                cuenta = tabla.Rows(i)(0).ToString
                desCuenta = tabla.Rows(i)(1).ToString
                saldoIni = tabla.Rows(i)(2).ToString
                saldoFinal = tabla.Rows(i)(2).ToString

                xmls3 = "<AuxiliarCtas:DetalleAux Fecha=""" + tabla.Rows(i)(3).ToString + """ NumUnIdenPol=""" + tabla.Rows(i)(4).ToString + """ Concepto=""" + RC(tabla.Rows(i)(5).ToString) + """ Debe=""" + tabla.Rows(i)(6).ToString + """ Haber=""" + tabla.Rows(i)(7).ToString + """/>" + vbCrLf
                cadena2 += tabla.Rows(i)(3).ToString + "|" + tabla.Rows(i)(4).ToString + "|" + tabla.Rows(i)(6).ToString + "|" + tabla.Rows(i)(7).ToString + "|"

                If tabla.Rows(i)(8).ToString = 0 Then 'es deudora
                    saldoFinal += (tabla.Rows(i)(6).ToString - tabla.Rows(i)(7).ToString) 'cargo menos abono
                Else
                    saldoFinal += (tabla.Rows(i)(7).ToString - tabla.Rows(i)(6).ToString) 'abono menos cargo
                End If

                If i = tabla.Rows.Count - 1 Then 'si es el ultimo registro hay que hacer el nodo completo
                    xmls2 += "<AuxiliarCtas:Cuenta NumCta=""" + cuenta + """ DesCta=""" + RC(desCuenta) + """ SaldoIni=""" + saldoIni.ToString("0.00") + """ SaldoFin=""" + saldoFinal.ToString("0.00") + """>" + vbCrLf
                    cadena += cuenta + "|" + desCuenta + "|" + saldoIni.ToString("0.00") + "|" + saldoFinal.ToString("0.00") + "|"
                    cadena += cadena2
                    xmls2 += xmls3
                    xmls2 += "</AuxiliarCtas:Cuenta>" + vbCrLf
                End If
            End If
        Next

        cadena += "|"
        While cadena.IndexOf("  ") <> -1
            cadena = Replace(cadena, "  ", " ")
        End While
        Dim Archivos As New dbSucursalesArchivos
        Dim en As New Encriptador
        Dim Sello As String = ""

        Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, False)
        Sello = en.GeneraSello(cadena, Archivos.RutaCer, "2011", False)

        Archivos.DaRutaCER(GlobalIdSucursalDefault, GlobalIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)


        xmls += "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
        xmls += "<AuxiliarCtas:AuxiliarCtas xsi:schemaLocation=""www.sat.gob.mx/esquemas/ContabilidadE/1_1/AuxiliarCtas http://www.sat.gob.mx/esquemas/ContabilidadE/1_1/AuxiliarCtas/AuxiliarCtas_1_1.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:AuxiliarCtas=""www.sat.gob.mx/esquemas/ContabilidadE/1_1/AuxiliarCtas"""
        xmls += " Version=""1.1"" RFC=""" + s.RFC + """ Mes=""" + fechita.Month.ToString("00") + """ Anio=""" + fechita.Year.ToString + """ TipoSolicitud=""" + pTipoSolicitud + """"
        If pTipoSolicitud = "AF" Or pTipoSolicitud = "FC" Then
            xmls += " NumOrden=""" + pNumOrden + """"
        Else
            xmls += " NumTramite=""" + pNumTramite + """"
        End If
        xmls += " Sello=""" + Sello + """"
        xmls += " noCertificado=""" + en.Seriex509 + """"
        xmls += " Certificado=""" + en.Certificado64 + """"
        xmls += ">" + vbCrLf

        xmls += xmls2

        xmls += "</AuxiliarCtas:AuxiliarCtas>"
        xmldoc.LoadXml(xmls)
        Return xmldoc
    End Function

    Public Function fechaCambio(ByVal pFecha As String) As String
        Return pFecha.Chars(6) + pFecha.Chars(7) + pFecha.Chars(8) + pFecha.Chars(9) + "/" + pFecha.Chars(3) + pFecha.Chars(4) + "/" + pFecha.Chars(0) + pFecha.Chars(1)
    End Function
    Public Function auxiliarCuentas(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pNivel As Integer, ByVal pidCuentas As Integer) As DataView

        Comm.CommandText = "Select if(conta.cuenta<>'',LPAD(conta.cuenta," + NNiv1.ToString + ",'0'),'')as N1, if(conta.N2<>'',LPAD(conta.N2," + NNiv2.ToString + ",'0'),'')as N2, if(conta.N3<>'',LPAD(conta.N3," + NNiv3.ToString + ",'0'),'')as N3,  if(conta.N4<>'',LPAD(conta.N4," + NNiv4.ToString + ",'0'),'') as N4,  if(conta.N5<>'',LPAD(conta.N5," + NNiv5.ToString + ",'0'),'') as N5, concat(conta.Cuenta,' ',conta.N2) as con1y2, concat(LPAD(conta.Cuenta," + NNiv1.ToString + ",'0'),' ',if(conta.N2<>'',LPAD(conta.N2," + NNiv2.ToString + ",'0'),''),' ',if(conta.N3<>'',LPAD(conta.N3," + NNiv3.ToString + ",'0'),''),' ',if(conta.N4<>'',LPAD(conta.N4," + NNiv4.ToString + ",'0'),''),' ',if(conta.N5<>'',LPAD(conta.N5," + NNiv5.ToString + ",'0'),'')) as concaComple,conta.Descripcion as des_cuenta ,tblpolizasdetalles.descripcion, tblpolizas.tipo, tblpolizas.numero, tblpolizas.fecha, "
        'If Date.Parse(pFechaI).Month.ToString("00") = "01" Then
        ' Comm.CommandText += " if(tblpolizas.tipo<>'A',if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo),0) as cargo,if(tblpolizas.tipo<>'A',if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono),0) as abono,"
        ' Else
        Comm.CommandText += " if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo) as cargo,if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono) as abono,"
        ' End If

        Comm.CommandText += " tblcontabilidadsaldosi.saldoI as saldoI,conta.Naturaleza  from tblpolizasdetalles inner join tblpolizas on tblpolizasdetalles.idPoliza=tblpolizas.id inner join tblccontables as conta on tblpolizasdetalles.idCuenta=conta.idCContable inner join tblcontabilidadsaldosi on conta.idCContable=tblcontabilidadsaldosi.idCContable where tblPolizas.tipo<>'A' and tblPolizas.fecha>='" + pFechaI + "' and tblPolizas.fecha<='" + pFechaF + "'"
        Comm.CommandText += " and concat(conta.cuenta"
        If pNivel > 1 Then
            Comm.CommandText += ",conta.N2"
        End If
        If pNivel > 2 Then
            Comm.CommandText += ",conta.N3"
        End If
        If pNivel > 3 Then
            Comm.CommandText += ",conta.N4"
        End If
        If pNivel > 4 Then
            Comm.CommandText += ",conta.N5"
        End If
        Comm.CommandText += ")=(Select concat(c.cuenta"
        If pNivel > 1 Then
            Comm.CommandText += ",c.N2"
        End If
        If pNivel > 2 Then
            Comm.CommandText += ",c.N3"
        End If
        If pNivel > 3 Then
            Comm.CommandText += ",c.N4"
        End If
        If pNivel > 4 Then
            Comm.CommandText += ",c.N5"
        End If
        Comm.CommandText += ") from tblCContables as c where idCContable=" + pidCuentas.ToString + ") order by  tblpolizas.fecha,tblpolizas.tipo,tblpolizas.numero"
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblauxiliarCuentas")
        ' DS2.WriteXmlSchema("tblauxiliarCuentas.xml")
        Return DS2.Tables("tblauxiliarCuentas").DefaultView
    End Function
    Public Function auxiliarCuentasPantalla(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pidCuenta As Integer) As DataView
        Dim strCuenta As String
        Comm.CommandText = "select ifnull((select concat(LPAD(c.cuenta," + NNiv1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + NNiv2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + NNiv3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + NNiv4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + NNiv5.ToString + ",'0'))) as Cuenta from tblccontables as c where idccontable=" + pidCuenta.ToString + "),'')"
        strCuenta = Comm.ExecuteScalar
        Comm.CommandText = "Select tblpolizas.id, tblpolizas.fecha,tblpolizas.tipo,tblpolizas.numero," + _
            "concat(LPAD(c.cuenta," + NNiv1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + NNiv2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + NNiv3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + NNiv4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + NNiv5.ToString + ",'0'))) as Cuenta," + _
            "tblpolizasdetalles.descripcion,"
        Comm.CommandText += "if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo) as cargo,if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono) as abono"
        Comm.CommandText += " from tblpolizasdetalles inner join tblpolizas on tblpolizasdetalles.idPoliza=tblpolizas.id inner join tblccontables as c on tblpolizasdetalles.idCuenta=c.idCContable where tblPolizas.tipo<>'A' and tblPolizas.fecha>='" + pFechaI + "' and tblPolizas.fecha<='" + pFechaF + "'" ' and tblpolizasdetalles.idcuenta=" + pidCuenta.ToString
        Comm.CommandText += " and tblpolizasdetalles.cuenta like '" + strCuenta.Trim + "%'"
        Comm.CommandText += " order by  tblpolizas.fecha,tblpolizas.tipo,tblpolizas.numero"
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblauxiliarCuentas")
        ' DS2.WriteXmlSchema("tblauxiliarCuentas.xml")
        Return DS2.Tables("tblauxiliarCuentas").DefaultView
    End Function
    Public Sub SaldosIniciales2(ByVal pHasta As String, ByVal pMes As String, ByVal PNivel As Integer, ByVal pN1 As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pIdCuenta As Integer)
        'Dim DS2 As New DataSet
        'Dim tb As DataTable
        Dim aux() As String = pHasta.Split("/")
        Dim mes = aux(1)

        Comm.CommandText = "Delete from tblcontabilidadsaldosi where idccontable=" + pIdCuenta.ToString + " and cuenta=(select cuenta from tblccontables where idccontable=+" + pIdCuenta.ToString + ")"
        Comm.ExecuteNonQuery()

        '        Comm.CommandText = "insert into tblcontabilidadsaldosi(idCContable,Cuenta,N1,naturaleza,saldoI)" +
        ' "Select tblccontables.idCContable, tblccontables.Cuenta, tblccontables.N2, tblccontables.Naturaleza," +
        '"(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo)-if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono),if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)-if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo))),0)" +
        '" from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        '        If mes = "01" Then
        '            Comm.CommandText += "tblPolizas.fecha>='" + anio.ToString + "/01/01' and tblPolizas.fecha<='" + anio.ToString + "/12/31' and tblPolizas.tipo='A' "
        '        Else
        '            Comm.CommandText += "tblPolizas.fecha>='" + anio.ToString + "/01/01' and tblPolizas.fecha<'" + pHasta + "' "
        '        End If
        '        Comm.CommandText += " and concat(tblccontables.Cuenta,tblccontables.N2)=(select concat(conta.Cuenta,conta.N2) from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial " +
        '" from tblccontables  where concat(tblccontables.Cuenta,if(tblccontables.n2<>'',lpad(tblccontables.N2,4,'0'),''),if(tblccontables.n3<>'',lpad(tblccontables.N3,4,'0'),''),if(tblccontables.n4<>'',lpad(tblccontables.N4,4,'0'),''),if(tblccontables.n5<>'',lpad(tblccontables.N5,4,'0'),''))>='" + inicioRango + "'" +
        '" and concat(tblccontables.Cuenta,if(tblccontables.n2<>'',lpad(tblccontables.N2,4,'0'),''),if(tblccontables.n3<>'',lpad(tblccontables.N3,4,'0'),''),if(tblccontables.n4<>'',lpad(tblccontables.N4,4,'0'),''),if(tblccontables.n5<>'',lpad(tblccontables.N5,4,'0'),''))<='" + finRango + "';"
        '        Comm.ExecuteNonQuery()




        Comm.CommandText = "insert into tblcontabilidadsaldosi(idCContable,Cuenta,N1,naturaleza,saldoI) Select tblccontables.idCContable, tblccontables.Cuenta, tblccontables.N2, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo)-if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono),if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)-if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        If pMes = "01" Then
            Comm.CommandText += "tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<='" + anio.ToString + "/12/31' and tblPolizas.tipo='A' "
        Else
            Comm.CommandText += " tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<'" + pHasta + "' "
        End If

        Comm.CommandText += " and concat(tblccontables.Cuenta,tblccontables.N2)=(select concat(conta.Cuenta,conta.N2) from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  where tblccontables.Nivel<=2 and "
        Comm.CommandText += "concat(tblccontables.Cuenta"
        If PNivel > 1 Then
            Comm.CommandText += ",tblccontables.N2"
        End If
        If PNivel > 2 Then
            Comm.CommandText += ",tblccontables.N3"
        End If
        If PNivel > 3 Then
            Comm.CommandText += ",tblccontables.N4"
        End If
        If PNivel > 4 Then
            Comm.CommandText += ",tblccontables.N5"
        End If
        Comm.CommandText += ")='" + pN1.ToString
        If PNivel > 1 Then
            Comm.CommandText += pN2.ToString
        End If
        If PNivel > 2 Then
            Comm.CommandText += pN3.ToString
        End If
        If PNivel > 3 Then
            Comm.CommandText += pN4.ToString
        End If
        If PNivel > 4 Then
            Comm.CommandText += pN5.ToString
        End If
        Comm.CommandText += "'"
        '   Comm.CommandText += "  and tblccontables.Nivel<=2  "
        Comm.ExecuteNonQuery()

    End Sub
    Public Function consultaSaldos()
        llenadoTablas()
        Comm.CommandText = "select c.idCContable as ID, c.Naturaleza as Nat,concat(LPAD(c.Cuenta," + NNiv1.ToString + ",'0'),' ',if(c.N2<>''," + "LPAD(c.N2," + NNiv2.ToString + ",'0'),''),' ',if(c.N3<>'', LPAD(c.N3," + NNiv3.ToString + ",'0'),''),' ',if(c.N4<>'',LPAD(c.N4," + NNiv4.ToString + ",'0'),''),' ',if(c.N5<>'',LPAD(c.N5," + NNiv5.ToString + ",'0'),'')) as Cuenta,c.Descripcion as D,(Select sum(total) from tblsisi as si where concat(si.Cuenta,if(c.Nivel>=2,si.N2,''),if(c.Nivel>=3,si.N3,''),if(c.Nivel>=4,si.N4,''),if(c.Nivel>=5,si.N5,''))=concat(c.Cuenta,c.N2,c.N3,c.N4,c.N5))as SI,(Select sum(total) from tblsicargos as si where concat(si.Cuenta,if(c.Nivel>=2,si.N2,''),if(c.Nivel>=3,si.N3,''),if(c.Nivel>=4,si.N4,''),if(c.Nivel>=5,si.N5,''))=concat(c.Cuenta,c.N2,c.N3,c.N4,c.N5))as SC,(Select sum(total) from tblsiabonos as sa where concat(sa.Cuenta,if(c.Nivel>=2,sa.N2,''),if(c.Nivel>=3,sa.N3,''),if(c.Nivel>=4,sa.N4,''),if(c.Nivel>=5,sa.N5,''))=concat(c.Cuenta,c.N2,c.N3,c.N4,c.N5))as SA,'0'as SF, c.Nivel as Nivel"
        Comm.CommandText += " from tblccontables as c"
        Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5"

        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblSaldos")
        'DS2.WriteXmlSchema("tblSaldos.xml")
        Return DS2.Tables("tblSaldos")

    End Function

    Public Sub llenadoTablas()
        Comm.CommandText = "delete from tblSISI;delete from tblSICargos;delete from tblSIAbonos;"
        Comm.CommandText += "insert into tblSISI(Cuenta,N2,N3,N4,N5,total) Select c.Cuenta,c.N2,c.N3,c.N4,c.N5,(select ifnull(sum(if(cc.Naturaleza=0,if( po.cargo=-999999999,0,po.cargo)-if(po.abono=-999999999,0,po.abono),if( po.abono=-999999999,0,po.abono)-if( po.cargo=-999999999,0,po.cargo))),0) from tblpolizasdetalles as po inner join tblccontables as cc on po.idCuenta=cc.idCContable inner join tblPolizas as poliz on po.idPoliza=poliz.id where poliz.tipo='A' and poliz.fecha>='" + anio + "/01/01' and poliz.fecha<='" + anio + "/01/31' and "
        Comm.CommandText += " c.idCContable=po.idCuenta)as saldoI from tblccontables as c order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"

        Comm.CommandText += "insert into tblSICargos(Cuenta,N2,N3,N4,N5,total) Select c.Cuenta,c.N2,c.N3,c.N4,c.N5, (select ifnull(sum(if( po.cargo=-999999999,0,po.cargo)),0) from tblpolizasdetalles as po inner join tblccontables as cc on po.idCuenta=cc.idCContable inner join tblPolizas as poliz on po.idPoliza=poliz.id where poliz.tipo<>'A' and poliz.fecha>='" + anio + "/01/01' and poliz.fecha<='" + anio + "/12/31' and "
        Comm.CommandText += " c.idCContable=po.idCuenta)as saldoI from tblccontables as c order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"

        Comm.CommandText += "insert into tblSIAbonos(Cuenta,N2,N3,N4,N5,total) Select c.Cuenta,c.N2,c.N3,c.N4,c.N5, (select ifnull(sum(if( po.abono=-999999999,0,po.abono)),0) from tblpolizasdetalles as po inner join tblccontables as cc on po.idCuenta=cc.idCContable inner join tblPolizas as poliz on po.idPoliza=poliz.id where poliz.tipo<>'A' and poliz.fecha>='" + anio + "/01/01' and poliz.fecha<='" + anio + "/12/31' and "
        Comm.CommandText += " c.idCContable=po.idCuenta)as saldoI from tblccontables as c order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        Comm.ExecuteScalar()
    End Sub

    Public Sub llenadoTablasMes(ByVal fechaI As String, ByVal pFechaF As String, ByVal pidCContable As Integer, ByVal pNivel As Integer)
        Comm.CommandText = "delete from tblSISI;delete from tblSICargos;delete from tblSIAbonos;"
        Comm.CommandText += "insert into tblSISI(Cuenta,N2,N3,N4,N5,total) Select c.Cuenta,c.N2,c.N3,c.N4,c.N5,(select ifnull(sum(if(cc.Naturaleza=0,if( po.cargo=-999999999,0,po.cargo)-if(po.abono=-999999999,0,po.abono),if( po.abono=-999999999,0,po.abono)-if( po.cargo=-999999999,0,po.cargo))),0) from tblpolizasdetalles as po inner join tblccontables as cc on po.idCuenta=cc.idCContable inner join tblPolizas as poliz on po.idPoliza=poliz.id where"
        If fechaI = anio + "/01/01" Then
            Comm.CommandText += " poliz.tipo='A' and "
            Comm.CommandText += " poliz.fecha>='" + anio + "/01/01' and poliz.fecha<='" + pFechaF + "' and "
        Else
            Comm.CommandText += " poliz.fecha>='" + anio + "/01/01' and poliz.fecha<'" + fechaI + "' and "
        End If
        'Comm.CommandText += " poliz.fecha>='" + anio + "/01/01' and poliz.fecha<='" + pFechaF + "' and "
        Comm.CommandText += " c.idCContable=po.idCuenta)as saldoI from tblccontables as c where c.Cuenta=(select p.Cuenta from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        If pNivel >= 2 Then
            Comm.CommandText += "  and c.N2=(select p.N2 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        If pNivel >= 3 Then
            Comm.CommandText += "  and c.N3=(select p.N3 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        If pNivel >= 4 Then
            Comm.CommandText += "  and c.N4=(select p.N4 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        If pNivel >= 5 Then
            Comm.CommandText += "  and c.N5=(select p.N5 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        Comm.CommandText += ";"
        'CARGOS
        Comm.CommandText += "insert into tblSICargos(Cuenta,N2,N3,N4,N5,total) Select c.Cuenta,c.N2,c.N3,c.N4,c.N5, (select ifnull(sum(if( po.cargo=-999999999,0,po.cargo)),0) from tblpolizasdetalles as po inner join tblccontables as cc on po.idCuenta=cc.idCContable inner join tblPolizas as poliz on po.idPoliza=poliz.id where poliz.tipo<>'A' and poliz.fecha>='" + fechaI + "' and poliz.fecha<='" + pFechaF + "' and "
        Comm.CommandText += " c.idCContable=po.idCuenta)as saldoI from tblccontables as c  where c.Cuenta=(select p.Cuenta from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        If pNivel >= 2 Then
            Comm.CommandText += "  and c.N2=(select p.N2 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        If pNivel >= 3 Then
            Comm.CommandText += "  and c.N3=(select p.N3 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        If pNivel >= 4 Then
            Comm.CommandText += "  and c.N4=(select p.N4 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        If pNivel >= 5 Then
            Comm.CommandText += "  and c.N5=(select p.N5 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        Comm.CommandText += ";"
        'ABONOS
        Comm.CommandText += "insert into tblSIAbonos(Cuenta,N2,N3,N4,N5,total) Select c.Cuenta,c.N2,c.N3,c.N4,c.N5, (select ifnull(sum(if( po.abono=-999999999,0,po.abono)),0) from tblpolizasdetalles as po inner join tblccontables as cc on po.idCuenta=cc.idCContable inner join tblPolizas as poliz on po.idPoliza=poliz.id where poliz.tipo<>'A' and poliz.fecha>='" + fechaI + "' and poliz.fecha<='" + pFechaF + "' and "
        Comm.CommandText += " c.idCContable=po.idCuenta)as saldoI from tblccontables as c  where c.Cuenta=(select p.Cuenta from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        If pNivel >= 2 Then
            Comm.CommandText += "  and c.N2=(select p.N2 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        If pNivel >= 3 Then
            Comm.CommandText += "  and c.N3=(select p.N3 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        If pNivel >= 4 Then
            Comm.CommandText += "  and c.N4=(select p.N4 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        If pNivel >= 5 Then
            Comm.CommandText += "  and c.N5=(select p.N5 from tblCContables as p where p.idCContable=" + pidCContable.ToString + ")"
        End If
        Comm.CommandText += ";"
        'Comm.CommandText += "insert into tblSIAbonos(Cuenta,N2,N3,N4,N5,total) Select c.Cuenta,c.N2,c.N3,c.N4,c.N5, (select ifnull(sum(if( po.abono=-999999999,0,po.abono)),0) from tblpolizasdetalles as po inner join tblccontables as cc on po.idCuenta=cc.idCContable inner join tblPolizas as poliz on po.idPoliza=poliz.id where poliz.tipo<>'A' and poliz.fecha>='" + anio + "/01/01' and poliz.fecha<='" + anio + "/12/31' and "
        'Comm.CommandText += " c.idCContable=po.idCuenta)as saldoI from tblccontables as c order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function consultaSaldosMes(ByVal pIDC As Integer, ByVal pNivel As Integer, pLicencia As String) As DataView
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        'llenadoTablasMes(anio + "/01/01", anio + "/01/31", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'ENERO' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.Fill(DS2, "tblSaldos")


        'llenadoTablasMes(anio + "/02/01", anio + "/02/29", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'FEBRERO' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.UpdateCommand = Comm
        'DA2.Fill(DS2, "tblSaldos")

        'llenadoTablasMes(anio + "/03/01", anio + "/03/31", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'MARZO' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.UpdateCommand = Comm
        'DA2.Fill(DS2, "tblSaldos")

        'llenadoTablasMes(anio + "/04/01", anio + "/04/31", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'ABRIL' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.UpdateCommand = Comm
        'DA2.Fill(DS2, "tblSaldos")
        'llenadoTablasMes(anio + "/05/01", anio + "/05/31", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'MAYO' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.UpdateCommand = Comm
        'DA2.Fill(DS2, "tblSaldos")
        'llenadoTablasMes(anio + "/06/01", anio + "/06/31", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'JUNIO' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.UpdateCommand = Comm
        'DA2.Fill(DS2, "tblSaldos")
        'llenadoTablasMes(anio + "/07/01", anio + "/07/31", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'JULIO' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.UpdateCommand = Comm
        'DA2.Fill(DS2, "tblSaldos")
        'llenadoTablasMes(anio + "/08/01", anio + "/08/31", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'AGOSTO' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.UpdateCommand = Comm
        'DA2.Fill(DS2, "tblSaldos")
        'llenadoTablasMes(anio + "/09/01", anio + "/09/31", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'SEPTIEMBRE' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.UpdateCommand = Comm
        'DA2.Fill(DS2, "tblSaldos")
        'llenadoTablasMes(anio + "/10/01", anio + "/10/31", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'OCTUBRE' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.UpdateCommand = Comm
        'DA2.Fill(DS2, "tblSaldos")
        'llenadoTablasMes(anio + "/11/01", anio + "/11/31", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'NOVIEMBRE' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.UpdateCommand = Comm
        'DA2.Fill(DS2, "tblSaldos")
        'llenadoTablasMes(anio + "/12/01", anio + "/12/31", pIDC, pNivel)
        'Comm.CommandText = "select  c.Naturaleza as Nat,'DICIEMBRE' as Mes,(Select sum(total) from tblsisi as si) as SI,(Select sum(total) from tblsiCargos as si) as SC,(Select sum(total) from tblsiAbonos as si) as SA,if(c.Naturaleza=0,(Select sum(total) from tblsisi as si)+(Select sum(total) from tblsiCargos as si)-(Select sum(total) from tblsiAbonos as si),(Select sum(total) from tblsisi as si)-(Select sum(total) from tblsiCargos as si)+(Select sum(total) from tblsiAbonos as si)) as SF"
        'Comm.CommandText += " from tblccontables as c where c.IDCContable=" + pIDC.ToString
        'Comm.CommandText += " order by c.Cuenta,c.N2,c.N3,c.N4,c.N5;"
        'DA2.UpdateCommand = Comm
        'DA2.Fill(DS2, "tblSaldos")


        Comm.CommandText = "delete from tblcontabilidadsaldostemp where maquina='" + pLicencia + "'"
        Comm.ExecuteNonQuery()
        Dim CualCuenta As String = ""
        Dim Naturaleza As String = ""
        Comm.CommandText = "select naturaleza from tblccontables where idccontable=" + pIDC.ToString
        Naturaleza = Comm.ExecuteScalar
        If pNivel = 1 Then CualCuenta = "tblpolizasdetalles.idcuentan1=" + pIDC.ToString
        If pNivel = 2 Then CualCuenta = "tblpolizasdetalles.idcuentan2=" + pIDC.ToString
        If pNivel = 3 Then CualCuenta = "tblpolizasdetalles.idcuentan3=" + pIDC.ToString
        If pNivel = 4 Then CualCuenta = "tblpolizasdetalles.idcuentan4=" + pIDC.ToString
        If pNivel = 5 Then CualCuenta = "tblpolizasdetalles.idcuentan5=" + pIDC.ToString

        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'ENERO'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and ((tblpolizas.fecha='" + anio + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/01/31' and tblpolizas.tipo<>'A'))),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and ((tblpolizas.fecha='" + anio + "/01/01' and tblpolizas.tipo='A') or (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/01/31' and tblpolizas.tipo<>'A'))),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<='" + anio + "/01/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<='" + anio + "/01/31' and tblpolizas.tipo<>'A')),0)," +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'FEBRERO'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/02/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/02/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/02/01' and tblpolizas.fecha<='" + anio + "/02/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/02/01' and tblpolizas.fecha<='" + anio + "/02/31' and tblpolizas.tipo<>'A')),0), " +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'MARZO'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/03/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/03/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/03/01' and tblpolizas.fecha<='" + anio + "/03/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/03/01' and tblpolizas.fecha<='" + anio + "/03/31' and tblpolizas.tipo<>'A')),0), " +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'ABRIL'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/04/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/04/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/04/01' and tblpolizas.fecha<='" + anio + "/04/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/04/01' and tblpolizas.fecha<='" + anio + "/04/31' and tblpolizas.tipo<>'A')),0), " +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'MAYO'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/05/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/05/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/05/01' and tblpolizas.fecha<='" + anio + "/05/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/05/01' and tblpolizas.fecha<='" + anio + "/05/31' and tblpolizas.tipo<>'A')),0), " +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'JUNIO'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/06/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/06/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/06/01' and tblpolizas.fecha<='" + anio + "/06/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/06/01' and tblpolizas.fecha<='" + anio + "/06/31' and tblpolizas.tipo<>'A')),0), " +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'JULIO'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/07/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/07/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/07/01' and tblpolizas.fecha<='" + anio + "/07/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/07/01' and tblpolizas.fecha<='" + anio + "/07/31' and tblpolizas.tipo<>'A')),0), " +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'AGOSTO'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/08/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/08/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/08/01' and tblpolizas.fecha<='" + anio + "/08/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/08/01' and tblpolizas.fecha<='" + anio + "/08/31' and tblpolizas.tipo<>'A')),0), " +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'SEPTIEMBRE'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/09/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/09/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/09/01' and tblpolizas.fecha<='" + anio + "/09/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/09/01' and tblpolizas.fecha<='" + anio + "/09/31' and tblpolizas.tipo<>'A')),0), " +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'OCTUBRE'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/10/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/10/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/10/01' and tblpolizas.fecha<='" + anio + "/10/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/10/01' and tblpolizas.fecha<='" + anio + "/10/31' and tblpolizas.tipo<>'A')),0), " +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'NOVIEMBRE'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/11/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/11/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/11/01' and tblpolizas.fecha<='" + anio + "/11/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/11/01' and tblpolizas.fecha<='" + anio + "/11/31' and tblpolizas.tipo<>'A')),0), " +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblcontabilidadsaldostemp(naturaleza,mes,sic,sia,sc,sa,idcuenta,maquina) values(" +
           Naturaleza + ",'DICIEMBRE'," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/12/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/01/01' and tblpolizas.fecha<'" + anio + "/12/01')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/12/01' and tblpolizas.fecha<='" + anio + "/12/31' and tblpolizas.tipo<>'A')),0)," +
           "ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))) from tblpolizas inner join tblpolizasdetalles on tblpolizas.id=tblpolizasdetalles.idpoliza where tblpolizas.estado=3 and " + CualCuenta + " and (tblpolizas.fecha>='" + anio + "/12/01' and tblpolizas.fecha<='" + anio + "/12/31' and tblpolizas.tipo<>'A')),0), " +
           pIDC.ToString + ",'" + pLicencia + "')"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "select if(naturaleza=1,'A','D') nat,mes,if(naturaleza=0,sic-sia,sia-sic) si,sc,sa,if(naturaleza=0,sic-sia+sc-sa,sia-sic+sa-sc) sf from tblcontabilidadsaldostemp where idcuenta=" + pIDC.ToString + " and maquina='" + pLicencia + "'"
        DA2.Fill(DS2, "tblSaldos")
        '  DS2.WriteXmlSchema("tblSaldos.xml")
        Return DS2.Tables("tblSaldos").DefaultView

    End Function

    Public Function Sumaactivo(pFechaInicio As String, ByVal pFechaHasta As String)
        Dim saldo As Double
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "delete from tblSaldos;"

        Comm.CommandText += "insert into tblSaldos(saldo) Select (select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3"


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta))  from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + activoC1 + "' and tblccontables.Cuenta<='" + activoO2 + "'  group by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Comm.ExecuteScalar()
        Comm.CommandText = "select ifnull((select sum(saldo) from tblSaldos),0)"
        saldo = Comm.ExecuteScalar()
        Return saldo
    End Function
    Public Function Sumaapasivo(pFechaInicio As String, ByVal pFechaHasta As String)
        Dim saldo As Double
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "delete from tblSaldos;"

        Comm.CommandText += "insert into tblSaldos(saldo) Select (select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3"
        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta))  from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + pasivoC1 + "' and tblccontables.Cuenta<='" + pasivoD2 + "'  group by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Comm.ExecuteScalar()
        Comm.CommandText = "select ifnull((select sum(saldo) from tblSaldos),0)"
        saldo = Comm.ExecuteScalar()
        Return saldo
    End Function
    Public Function Sumacapital(ByVal pFechaHasta As String)
        Dim saldo As Double
        Comm.CommandText = "delete from tblSaldos;"

        Comm.CommandText += "insert into tblSaldos(saldo) Select (select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo)-if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono),if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)-if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' "


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta))  from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + capital1 + "' and tblccontables.Cuenta<='" + capital2 + "'  group by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Comm.ExecuteScalar()
        Comm.CommandText = "select sum(saldo) from tblSaldos"
        saldo = Comm.ExecuteScalar()
        Return saldo
    End Function
    Public Function activo(pFechaInicio As String, ByVal pFechaHasta As String)

        Dim anioint As String = pFechaHasta.Substring(0, 4)
        ' ''Comm.CommandText = "Select DISTINCT tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        ' ''Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' "


        ' ''Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        ' ''Comm.CommandText += "  where tblccontables.Cuenta>='" + activoC1 + "' and tblccontables.Cuenta<='" + activoC2 + "' order by tblccontables.Cuenta  "


        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1 limit 1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3 "


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + activoC1 + "' and tblccontables.Cuenta<='" + activoC2 + "' group by tblccontables.Cuenta order by tblccontables.Cuenta  "



        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblActivo")
        'DS2.WriteXmlSchema("tblActivo.xml")
        Return DS2.Tables("tblActivo")

    End Function
    Public Function activofijo(pFechaInicio As String, ByVal pFechaHasta As String)
        'Dim DS2 As New DataSet
        'Dim tb As DataTable
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1 limit 1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3 "
        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + activoF1 + "' and tblccontables.Cuenta<='" + activoF2 + "' group by tblccontables.Cuenta order by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblActivoFijo")
        'DS2.WriteXmlSchema("tblActivoFijo.xml")
        Return DS2.Tables("tblActivoFijo")

    End Function
    Public Function activodiferido(pFechaInicio As String, ByVal pFechaHasta As String)
        'Dim DS2 As New DataSet
        'Dim tb As DataTable
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1 limit 1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3 "
        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + activoD1 + "' and tblccontables.Cuenta<='" + activoD2 + "' group by tblccontables.Cuenta order by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblActivoDiferido")
        'DS2.WriteXmlSchema("tblActivoDiferido.xml")
        Return DS2.Tables("tblActivoDiferido")

    End Function
    Public Function activoOtros(pFechaInicio As String, ByVal pFechaHasta As String)
        'Dim DS2 As New DataSet
        'Dim tb As DataTable
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1 limit 1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3 "
        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + activoO1 + "' and tblccontables.Cuenta<='" + activoO2 + "' group by tblccontables.Cuenta order by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblActivoOtros")
        '  DS2.WriteXmlSchema("tblActivoOtros.xml")
        Return DS2.Tables("tblActivoOtros")

    End Function
    Public Function pasivo(pFechaInicio As String, ByVal pFechaHasta As String)
        'Dim DS2 As New DataSet
        'Dim tb As DataTable
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1 limit 1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3"
        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + pasivoC1 + "' and tblccontables.Cuenta<='" + pasivoC2 + "' group by tblccontables.Cuenta order by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblPasivos")
        ' DS2.WriteXmlSchema("tblPasivos.xml")
        Return DS2.Tables("tblPasivos")

    End Function
    Public Function pasivofIJO(pFechaInicio As String, ByVal pFechaHasta As String)
        'Dim DS2 As New DataSet
        'Dim tb As DataTable
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1 limit 1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3 "


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + pasivoF1 + "' and tblccontables.Cuenta<='" + pasivoF2 + "' group by tblccontables.Cuenta order by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblPasivosFijo")
        ' DS2.WriteXmlSchema("tblPasivosFijo.xml")
        Return DS2.Tables("tblPasivosFijo")

    End Function
    Public Function pasivoDiferido(pFechaInicio As String, ByVal pFechaHasta As String)
        'Dim DS2 As New DataSet
        'Dim tb As DataTable
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1 limit 1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3 "


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + pasivoD1 + "' and tblccontables.Cuenta<='" + pasivoD2 + "' group by tblccontables.Cuenta order by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblPasivosDif")
        'DS2.WriteXmlSchema("tblPasivosDif.xml")
        Return DS2.Tables("tblPasivosDif")

    End Function
    Public Function CAPITAL(pFechaInicio As String, ByVal pFechaHasta As String) As Data.DataTable
        'Dim DS2 As New DataSet
        'Dim tb As DataTable

        'Comm.CommandText = "Select DISTINCT tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo)-if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono),if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)-if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        'Comm.CommandText += " tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' "


        'Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        'Comm.CommandText += "  where tblccontables.Cuenta>='" + activoC1 + "' and tblccontables.Cuenta<='" + activoC2 + "' order by tblccontables.Cuenta  "


        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1 limit 1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3 "

        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial "
        Comm.CommandText += " ,tblccontables.idCContable "
        Comm.CommandText += " from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta='" + capital1 + "' or (tblccontables.Cuenta>='" + capital3 + "' and tblccontables.Cuenta<='" + capital4 + "') group by tblccontables.Cuenta order by tblccontables.Cuenta "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblCapital")
        'DS2.WriteXmlSchema("tblCapital.xml")
        Return DS2.Tables("tblCapital")

    End Function
    Public Function CAPITALCierre(ByVal pFechaHasta As String) As Data.DataTable
        'Dim DS2 As New DataSet
        'Dim tb As DataTable

        'Comm.CommandText = "Select DISTINCT tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo)-if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono),if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)-if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        'Comm.CommandText += " tblPolizas.fecha>='" + anio + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' "


        'Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        'Comm.CommandText += "  where tblccontables.Cuenta>='" + activoC1 + "' and tblccontables.Cuenta<='" + activoC2 + "' order by tblccontables.Cuenta  "


        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' "

        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial "
        Comm.CommandText += " ,tblccontables.idCContable "
        Comm.CommandText += " from tblccontables  "
        Comm.CommandText += "  where (tblccontables.Cuenta>='" + capital3 + "' and tblccontables.Cuenta<='" + capital4 + "') group by tblccontables.Cuenta order by tblccontables.Cuenta "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblCapital")
        'DS2.WriteXmlSchema("tblCapital.xml")
        Return DS2.Tables("tblCapital")

    End Function
    Public Sub CAPITALResultados(ByVal pFechaHasta As String)
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3"
        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial "
        Comm.CommandText += " ,tblccontables.idCContable "
        Comm.CommandText += " from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta='" + capital1 + "' group by tblccontables.Cuenta order by tblccontables.Cuenta "
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        DReader = Comm.ExecuteReader
        If DReader.Read Then
            ResultadosCuenta = DReader("cuenta")
            ResultadosDesc = DReader("nombre")
            ResultadosNaturaleza = DReader("naturaleza")
            ResultadosSaldo = DReader("saldoinicial")
            ResultadosidCuenta = DReader("idccontable")
        End If
        DReader.Close()
    End Sub
    Public Function OrdenAcre(pFechaInicio As String, ByVal pFechaHasta As String)
        'Dim DS2 As New DataSet
        'Dim tb As DataTable
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1 limit 1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where tblpolizas.estado=3"
        If ordenVisible = 1 Then
            Comm.CommandText += " and tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' "
        Else
            Comm.CommandText += " and tblPolizas.fecha>='4000/01/01' and tblPolizas.fecha<='4000/02/01' "
        End If
        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + ordenA1 + "' and tblccontables.Cuenta<='" + ordenA2 + "' and tblccontables.Naturaleza=1 group by tblccontables.Cuenta order by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblOrdenA")
        'DS2.WriteXmlSchema("tblOrdenA.xml")
        Return DS2.Tables("tblOrdenA")

    End Function
    Public Function OrdenDeu(pFechaInicio As String, ByVal pFechaHasta As String)
        'Dim DS2 As New DataSet
        'Dim tb As DataTable
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1 limit 1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where tblpolizas.estado=3"

        If ordenVisible = 1 Then
            Comm.CommandText += " and tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' "
        Else
            Comm.CommandText += " and tblPolizas.fecha>='4000/01/01' and tblPolizas.fecha<='4000/02/01' "
        End If

        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + ordenD1 + "' and tblccontables.Cuenta<='" + ordenD2 + "' and tblccontables.Naturaleza=0 group by tblccontables.Cuenta order by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblOrdenDeu")
        ' DS2.WriteXmlSchema("tblOrdenDeu.xml")
        Return DS2.Tables("tblOrdenDeu")

    End Function
    Public Function nombreResultado()
        Dim nom As String
        Comm.CommandText = "Select ifnull(Descripcion,' ') from tblCContables where Cuenta='" + resultados + "' and Nivel=1"
        nom = Comm.ExecuteScalar
        If nom Is Nothing Then
            nom = " "
        End If
        Return nom
    End Function
    Public Function egresos(pFechaInicio As String, ByVal pFechaHasta As String)
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3"


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial "

        Comm.CommandText += ",(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + pFechaInicio + "' and tblPolizas.fecha<='" + Date.Parse(anioint + "/" + Date.Parse(pFechaHasta).Month.ToString("00") + "/01").AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd") + "' "


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoMes"

        Comm.CommandText += " from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + egresos1 + "' and tblccontables.Cuenta<='" + egresos2 + "' group by tblccontables.Cuenta order by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblEgresos")
        'DS2.WriteXmlSchema("tblEgresos.xml")
        Return DS2.Tables("tblEgresos")

    End Function
    Public Function ingresos(pFechaInicio As String, ByVal pFechaHasta As String)
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "Select tblccontables.Cuenta,(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3"


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial "

        Comm.CommandText += ",(select c.descripcion from tblccontables as c where c.Cuenta=tblccontables.Cuenta and c.Nivel=1)as nombre, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + pFechaInicio + "' and tblPolizas.fecha<='" + Date.Parse(anioint + "/" + Date.Parse(pFechaHasta).Month.ToString("00") + "/01").AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd") + "' "


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoMes"

        Comm.CommandText += " from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + ingresos1 + "' and tblccontables.Cuenta<='" + ingresos2 + "' group by tblccontables.Cuenta order by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblIngresos")
        'DS2.WriteXmlSchema("tblIngresos.xml")
        Return DS2.Tables("tblIngresos")

    End Function
    Public Function sumaEdoResTodo(pFechaInicio As String, ByVal pFechaHasta As String)
        Dim saldo As Double
        Dim saldoE As Double
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "delete from tblSaldos;"

        Comm.CommandText += "insert into tblSaldos(saldo) Select (select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3"


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta))  "

        Comm.CommandText += " from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + ingresos1 + "' and tblccontables.Cuenta<='" + ingresos2 + "'  group by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull((select sum(round(saldo,2)) from tblSaldos),0)"
        saldo = Comm.ExecuteScalar()

        Comm.CommandText = "delete from tblSaldos;"

        Comm.CommandText += "insert into tblSaldos(saldo) Select (select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3"


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta))  from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + egresos1 + "' and tblccontables.Cuenta<='" + egresos2 + "'  group by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull((select sum(round(saldo,2)) from tblSaldos),0)"
        saldoE = Comm.ExecuteScalar()
        totalEdoRe = saldo - saldoE
        Return totalEdoRe

    End Function

    Public Function sumaCapitalYEdo(pFechaInicio As String, ByVal pFechaHasta As String)
        Dim saldo As Double
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "delete from tblSaldos;"

        Comm.CommandText += "insert into tblSaldos(saldo) Select (select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + anioint + "/01/01' and tblPolizas.fecha<='" + pFechaHasta + "' and tblpolizas.estado=3"


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) from tblccontables "
        Comm.CommandText += "  where (tblccontables.Cuenta>='" + capital1 + "' and tblccontables.Cuenta<='" + capital1 + "') or (tblccontables.Cuenta>='" + capital3 + "' and tblccontables.Cuenta<='" + capital4 + "')  group by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Comm.ExecuteScalar()
        Comm.CommandText = "select ifnull((select sum(saldo) from tblSaldos),0)"
        saldo = Comm.ExecuteScalar()

        Return totalEdoRe + saldo

    End Function
    Public Function sumaEdoResMes(pFechaInicio As String, ByVal pFechaHasta As String) As Double
        Dim saldo As Double
        Dim saldoE As Double
        Dim anioint As String = pFechaHasta.Substring(0, 4)
        Comm.CommandText = "delete from tblSaldos;"

        Comm.CommandText += "insert into tblSaldos(saldo) Select (select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo)-if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono),if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)-if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + pFechaInicio + "' and tblPolizas.fecha<='" + Date.Parse(anioint + "/" + Date.Parse(pFechaHasta).Month.ToString("00") + "/01").AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd") + "' and tblpolizas.estado=3 "


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta))  from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + ingresos1 + "' and tblccontables.Cuenta<='" + ingresos2 + "'  group by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Comm.ExecuteScalar()
        Comm.CommandText = "select ifnull((select sum(saldo) from tblSaldos),0)"
        saldo = Comm.ExecuteScalar()

        Comm.CommandText = "delete from tblSaldos;"

        Comm.CommandText += "insert into tblSaldos(saldo) Select (select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo)-if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono),if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)-if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        Comm.CommandText += " tblPolizas.fecha>='" + pFechaInicio + "' and tblPolizas.fecha<='" + Date.Parse(anioint + "/" + Date.Parse(pFechaHasta).Month.ToString("00") + "/01").AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd") + "' "


        Comm.CommandText += " and tblccontables.Cuenta=(select conta.Cuenta from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta))  from tblccontables  "
        Comm.CommandText += "  where tblccontables.Cuenta>='" + egresos1 + "' and tblccontables.Cuenta<='" + egresos2 + "'  group by tblccontables.Cuenta  "
        ' Comm.CommandText += "  where tblccontables.Nivel<=2  "
        Comm.ExecuteScalar()
        Comm.CommandText = "select ifnull((select sum(saldo) from tblSaldos),0)"
        saldoE = Comm.ExecuteScalar()

        Return saldo - saldoE

    End Function
    Public Function buscarPolizasPosteriores(ByVal pAnio As Integer) As Integer
        Dim contador As Integer = 0
        Comm.CommandText = " Select count(id) from tblPolizas where tipo='A' and fecha>=" + pAnio.ToString + "/01/01 and  fecha<=" + pAnio.ToString + "/12/31;"
        contador = Comm.ExecuteScalar()
        Return contador
    End Function
    Private Function RC(ByVal pTexto As String) As String
        Dim texto As String = pTexto
        texto = Replace(texto, "&", "&amp;")
        texto = Replace(texto, "'", "&apos;")
        texto = Replace(texto, ">", "&gt;")
        texto = Replace(texto, "<", "&lt;")
        texto = Replace(texto, """", "&quot;")
        Return texto
    End Function
    'Public Function conciliacionDIOT(ByVal pDesde As String, ByVal pHasta As String, ByVal ptipo As String)

    '    Comm.CommandText = " Select tblpolizasdetalles.DIOTHabilitado as Selec,tblpolizasdetalles.id as ID, tblpolizasdetalles.Factura as Cod,tblproveedores.RFC as RFC, tblproveedores.nombre as Prov,if(tblpolizasdetalles.iva=0 or tblpolizasdetalles.iva='E',tblpolizasdetalles.valorActos,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.iva,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.iva)) as valor,tblpolizasdetalles.iva as ivaP,if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo) as iva, tblpolizas.tipo as tPoliza, tblpolizas.numero as nPoliza,tblpolizas.fecha as Fecha from tblpolizasdetalles inner join tblproveedores on tblproveedores.idproveedor=tblpolizasdetalles.idProveedor inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where tblpolizasdetalles.esDiot<>0 and tblpolizasdetalles.idproveedor<>0"
    '    Comm.CommandText += " and DiotHabilitado=1 "
    '    Comm.CommandText += " and  '" + pHasta + "'>=tblpolizas.fecha"
    '    Comm.CommandText += " order by tblpolizas.fecha,concat(tblpolizas.tipo,tblpolizas.numero)"
    '    Dim DS2 As New DataSet
    '    Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA2.Fill(DS2, "tblDIOTS")
    '    Comm.CommandText = " Select tblpolizasdetalles.DIOTHabilitado as Selec,tblpolizasdetalles.id as ID, tblpolizasdetalles.Factura as Cod,tblproveedores.RFC as RFC, tblproveedores.nombre as Prov,if(tblpolizasdetalles.iva=0 or tblpolizasdetalles.iva='E',tblpolizasdetalles.valorActos,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.iva,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.iva)) as valor,tblpolizasdetalles.iva as ivaP,if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo) as iva, tblpolizas.tipo as tPoliza, tblpolizas.numero as nPoliza,tblpolizas.fecha as Fecha from tblpolizasdetalles inner join tblproveedores on tblproveedores.idproveedor=tblpolizasdetalles.idProveedor inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where tblpolizasdetalles.esDiot<>0 and tblpolizasdetalles.idproveedor<>0"
    '    Comm.CommandText += " and tblpolizasdetalles.fechaDiot>='" + pDesde + "' and tblpolizasdetalles.fechaDiot<='" + pHasta + "'"
    '    If ptipo <> "T" Then
    '        Comm.CommandText += " and tblpolizas.tipo='" + ptipo + "'"
    '    End If
    '    Comm.CommandText += "order by tblpolizasdetalles.fechaDIOT,concat(tblpolizas.tipo,tblpolizas.numero)"
    '    DA2.Fill(DS2, "tblDIOTS")
    '    Return DS2.Tables("tblDIOTS")
    'End Function

    Public Function conciliacionDIOT(ByVal pDesde As String, ByVal pHasta As String, ByVal ptipo As String) As DataView

        'Comm.CommandText = " Select tblpolizasdetalles.DIOTHabilitado as Selec,tblpolizasdetalles.id as ID, tblpolizasdetalles.Factura as Cod,tblproveedores.RFC as RFC, tblproveedores.nombre as Prov,if(tblpolizasdetalles.iva=0 or tblpolizasdetalles.iva='E',tblpolizasdetalles.valorActos,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.iva,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.iva)) as valor,tblpolizasdetalles.iva as ivaP,if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo) as iva, tblpolizas.tipo as tPoliza, tblpolizas.numero as nPoliza,tblpolizas.fecha as Fecha from tblpolizasdetalles inner join tblproveedores on tblproveedores.idproveedor=tblpolizasdetalles.idProveedor inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where tblpolizasdetalles.esDiot>0 and tblpolizasdetalles.idproveedor<>0"
        'Comm.CommandText += " and DiotHabilitado=1 "
        'Comm.CommandText += " and  '" + pHasta + "'>=tblpolizas.fecha"
        'Comm.CommandText += " order by tblpolizas.fecha,concat(tblpolizas.tipo,tblpolizas.numero)"
        Dim DS2 As New DataSet

        'DA2.Fill(DS2, "tblDIOTS")
        Comm.CommandText = " Select tblpolizasdetalles.DIOTHabilitado as Selec,tblpolizasdetalles.id as ID, tblpolizasdetalles.Factura as Cod,tblproveedores.RFC as RFC, tblproveedores.nombre as Prov,if(tblpolizasdetalles.ieps=0 and tblpolizasdetalles.ivaret=0,if(tblpolizasdetalles.iva=0 or tblpolizasdetalles.iva='E',tblpolizasdetalles.valorActos,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.iva,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.iva)),if(tblpolizasdetalles.ieps=0,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.ivaret,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.ivaret),if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.ieps,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.ieps))) as valor,tblpolizasdetalles.iva as ivaP,if(tblpolizasdetalles.ieps=0 and tblpolizasdetalles.ivaret=0,if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo),0) as iva," +
            "tblpolizasdetalles.ivaret,tblpolizasdetalles.ieps,if(tblpolizasdetalles.ivaret<>0,if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo),0) as ivaretc,if(tblpolizasdetalles.ieps<>0,if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo),0) as iepsc,tblpolizas.tipo as tPoliza, tblpolizas.numero as nPoliza,tblpolizas.fecha as Fecha,tblpolizasdetalles.referencia,tblpolizasdetalles.fechaDIOT" +
            " from tblpolizasdetalles inner join tblproveedores on tblproveedores.idproveedor=tblpolizasdetalles.idProveedor inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where tblpolizasdetalles.esDiot<>0 and tblpolizasdetalles.idproveedor<>0 and tblpolizas.estado=3"
        Comm.CommandText += " and tblpolizasdetalles.fechaDiot>='" + pDesde + "' and tblpolizasdetalles.fechaDiot<='" + pHasta + "'"
        If ptipo <> "T" Then
            Comm.CommandText += " and tblpolizas.tipo='" + ptipo + "'"
        Else
            Comm.CommandText += " and tblpolizas.tipo<>'A'"
        End If
        Comm.CommandText += "order by Fecha,tblpolizas.tipo,tblpolizas.numero"
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblDIOTS")
        Return DS2.Tables("tblDIOTS").DefaultView
    End Function

    Public Function generarDIOT(ByVal pDesde As String, ByVal pHasta As String, ByVal ptipo As String) As MySql.Data.MySqlClient.MySqlDataReader
        'Comm.CommandText = " Select tblpolizasdetalles.DIOTHabilitado as Selec,tblpolizasdetalles.id as ID, tblpolizasdetalles.Factura as Cod,tblproveedores.RFC as RFC, tblproveedores.nombre as Prov,sum(if(tblpolizasdetalles.iva=0 or tblpolizasdetalles.iva='E',tblpolizasdetalles.valorActos,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.iva,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.iva))) as valor,tblpolizasdetalles.iva as ivaP,sum(if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo)) as iva, tblpolizas.tipo as tPoliza, tblpolizas.numero as nPoliza,tblpolizas.fecha as Fecha from tblpolizasdetalles inner join tblproveedores on tblproveedores.idproveedor=tblpolizasdetalles.idProveedor inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where tblpolizasdetalles.esDiot<>0 "
        Comm.CommandText = " Select tblproveedores.RFC as RFC,tblpolizasdetalles.iva as ivaP," +
        "sum(if(tblpolizasdetalles.ieps=0 and tblpolizasdetalles.ivaret=0,if(tblpolizasdetalles.iva=0 or tblpolizasdetalles.iva='E',0,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.iva,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.iva)),0)) as cantidadiva," +
        "sum(if(tblpolizasdetalles.ieps=0,0,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.ieps,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.ieps))) as cantidadieps," +
        "sum(if(tblpolizasdetalles.ivaret=0,0,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.ivaret,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.ivaret))) as cantidadivaret," +
    "sum(if(tblpolizasdetalles.iva=0 or tblpolizasdetalles.iva='E',tblpolizasdetalles.valorActos,0)) as cantidadivacero,tblpolizasdetalles.ivaret,tblpolizasdetalles.ieps from tblpolizasdetalles inner join tblproveedores on tblproveedores.idproveedor=tblpolizasdetalles.idProveedor inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where tblpolizasdetalles.esDiot<>0 and tblpolizasdetalles.idproveedor<>0 "
        Comm.CommandText += " and tblpolizasdetalles.fechaDiot>='" + pDesde + "' and tblpolizasdetalles.fechaDiot<='" + pHasta + "' and tblpolizasdetalles.diothabilitado=0 and tblpolizas.estado=3 "
        If ptipo <> "T" Then
            Comm.CommandText += " and tblpolizas.tipo='" + ptipo + "'"
        Else
            Comm.CommandText += " and tblpolizas.tipo<>'A'"
        End If
        'Comm.CommandText += "order by tblpolizasdetalles.fechaDIOT,concat(tblpolizas.tipo,tblpolizas.numero) group by RFC,ivaP"
        Comm.CommandText += " group by RFC"
        'Dim DS2 As New DataSet
        'Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        'DA2.Fill(DS2, "tblDIOTS")
        'DS2.WriteXmlSchema("tblOrdenA.xml")
        'Return DS2.Tables("tblDIOTS")
        Return Comm.ExecuteReader
    End Function
    Public Sub modificarDIOTTran(ByVal pID As Integer, ByVal pValor As Integer, ByVal pFecha As String)
        Comm.CommandText = " update tblpolizasdetalles set DIOTHabilitado=" + pValor.ToString + ", fechaDIOT='" + pFecha + "' where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function TotalIVA(ByVal pDesde As String, ByVal pHasta As String, ByVal ptipo As String, pDiotHabilitado As Byte) As Double
        Dim total As Double
        Comm.CommandText = " Select ifnull(sum(if(tblpolizasdetalles.iva<>'0' and tblpolizasdetalles.iva<>'E',if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo),0)),0) from tblpolizasdetalles inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where tblpolizasdetalles.esDiot<>0"
        Comm.CommandText += " and tblpolizasdetalles.fechaDIOT>='" + pDesde + "' and tblpolizasdetalles.fechaDIOT<='" + pHasta + "' and abono=-999999999"
        If ptipo <> "T" Then
            Comm.CommandText += " and tblpolizas.tipo='" + ptipo + "'"
        End If
        If pDiotHabilitado > 0 Then
            Comm.CommandText += " and tblpolizasdetalles.diothabilitado=" + CStr(pDiotHabilitado - 1)
        End If
        total = Comm.ExecuteScalar
        Return total
    End Function
    Public Function TotalIVARet(ByVal pDesde As String, ByVal pHasta As String, ByVal ptipo As String, pDiotHabilitado As Byte) As Double
        Dim total As Double
        Comm.CommandText = " Select ifnull(sum(if(tblpolizasdetalles.ivaret<>0,if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo),0)),0) from tblpolizasdetalles inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where tblpolizasdetalles.esDiot<>0"
        Comm.CommandText += " and tblpolizasdetalles.fechaDIOT>='" + pDesde + "' and tblpolizasdetalles.fechaDIOT<='" + pHasta + "' and abono=-999999999"
        If ptipo <> "T" Then
            Comm.CommandText += " and tblpolizas.tipo='" + ptipo + "'"
        End If
        If pDiotHabilitado > 0 Then
            Comm.CommandText += " and tblpolizasdetalles.diothabilitado=" + CStr(pDiotHabilitado - 1)
        End If
        total = Comm.ExecuteScalar
        Return total
    End Function
    Public Function TotalIeps(ByVal pDesde As String, ByVal pHasta As String, ByVal ptipo As String, pDiotHabilitado As Byte) As Double
        Dim total As Double
        Comm.CommandText = " Select ifnull(sum(if(tblpolizasdetalles.ieps<>0,if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo),0)),0) from tblpolizasdetalles inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where tblpolizasdetalles.esDiot<>0"
        Comm.CommandText += " and tblpolizasdetalles.fechaDIOT>='" + pDesde + "' and tblpolizasdetalles.fechaDIOT<='" + pHasta + "' and abono=-999999999"
        If ptipo <> "T" Then
            Comm.CommandText += " and tblpolizas.tipo='" + ptipo + "'"
        End If
        If pDiotHabilitado > 0 Then
            Comm.CommandText += " and tblpolizasdetalles.diothabilitado=" + CStr(pDiotHabilitado - 1)
        End If
        total = Comm.ExecuteScalar
        Return total
    End Function
    Public Function TotalValor(ByVal pDesde As String, ByVal pHasta As String, ByVal ptipo As String, pDiotHabilitado As Byte) As Double
        Dim total As Double
        Comm.CommandText = " Select ifnull(sum(if(tblpolizasdetalles.ieps=0 and tblpolizasdetalles.ivaret=0,if(tblpolizasdetalles.iva=0 or tblpolizasdetalles.iva='E',tblpolizasdetalles.valorActos,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.iva,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.iva)),if(tblpolizasdetalles.ieps=0,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.ivaret,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.ivaret),if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/tblpolizasdetalles.ieps,(tblpolizasdetalles.cargo*100)/tblpolizasdetalles.ieps)))),0) from tblpolizasdetalles inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where tblpolizasdetalles.esDiot<>0"
        Comm.CommandText += " and tblpolizasdetalles.fechaDIOT>='" + pDesde + "' and tblpolizasdetalles.fechaDIOT<='" + pHasta + "' and tblpolizas.estado=3 "
        If ptipo <> "T" Then
            Comm.CommandText += " and tblpolizas.tipo='" + ptipo + "'"
        End If
        If pDiotHabilitado > 0 Then
            Comm.CommandText += " and tblpolizasdetalles.diothabilitado=" + CStr(pDiotHabilitado - 1)
        End If
        total = Comm.ExecuteScalar
        Return total
    End Function
    Public Function TotalIVAMesPasado(ByVal pDesde As String, ByVal pHasta As String, ByVal ptipo As String) As Double
        Dim total As Double
        pDesde = Date.Parse("01/01/" + Date.Parse(pDesde).Year.ToString()).ToString("yyyy/MM/dd")

        Comm.CommandText = " Select ifnull(sum(if(tblpolizasdetalles.cargo=-999999999,tblpolizasdetalles.abono,tblpolizasdetalles.cargo)),0) from tblpolizasdetalles inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where DIOTHabilitado=1 and tblpolizasdetalles.esDiot<>0"
        ' Comm.CommandText += " and tblpolizas.fecha>='" + pDesde + "' and tblpolizas.fecha<'" + pHasta + "'"
        Comm.CommandText += " and  '" + pHasta + "'>=tblpolizas.fecha"
        If ptipo <> "T" Then
            Comm.CommandText += " and tblpolizas.tipo='" + ptipo + "'"
        End If
        total = Comm.ExecuteScalar
        Return total
    End Function
    Public Function TotalValormesPasado(ByVal pDesde As String, ByVal pHasta As String, ByVal ptipo As String) As Double
        Dim total As Double
        pDesde = Date.Parse("01/01/" + Date.Parse(pDesde).Year.ToString()).ToString("yyyy/MM/dd")

        Comm.CommandText = " Select ifnull(sum(if(iva=0 or iva='E',tblpolizasdetalles.valorActos,if(tblpolizasdetalles.cargo=-999999999,(tblpolizasdetalles.abono*100)/iva,(tblpolizasdetalles.cargo*100)/iva))),0) from tblpolizasdetalles inner join tblpolizas on tblpolizas.id=tblpolizasdetalles.idPoliza where DIOTHabilitado=1 and tblpolizasdetalles.esDiot<>0"
        '  Comm.CommandText += " and tblpolizas.fecha>='" + pDesde + "' and tblpolizas.fecha<'" + pHasta + "'"
        Comm.CommandText += " and  '" + pHasta + "'>=tblpolizas.fecha"
        If ptipo <> "T" Then
            Comm.CommandText += " and tblpolizas.tipo='" + ptipo + "'"
        End If
        total = Comm.ExecuteScalar
        Return total
    End Function

    Public Function cambiarContra(ByVal actual As String, ByVal nueva As String) As Boolean
        Dim data As MySqlDataReader
        Comm.CommandText = " select passPeriodo from tblcontabilidadconf where id=1;"
        data = Comm.ExecuteReader
        Dim aux As String = ""
        While (data.Read())
            aux = data(0).ToString
        End While
        data.Close()
        If aux <> "" Then
            If actual = aux Then
                Comm.CommandText = "update tblcontabilidadconf set passPeriodo='" + nueva.ToString + "' where id=1;"
                Comm.ExecuteNonQuery()
                Return True
            End If
        End If
        data.Close()
        Return False
    End Function
    Public Function auxiliarCuentasRango(ByVal pFechaI As String, ByVal pFechaF As String, ByVal pNivel As Integer, ByVal pidCuentas As Integer, ByVal inicioRango As String, ByVal finRango As String) As DataTable
        'Dim strCuenta As String
        'Comm.CommandText = "select ifnull((select concat(LPAD(c.cuenta," + NNiv1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + NNiv2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + NNiv3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + NNiv4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + NNiv5.ToString + ",'0'))) as Cuenta from tblccontables as c where idccontable=" + pidCuentas.ToString + "),'')"
        'strCuenta = Comm.ExecuteScalar
        Comm.CommandText = "select concat(LPAD(c.cuenta," + NNiv1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + NNiv2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + NNiv3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + NNiv4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + NNiv5.ToString + ",'0'))) as concaComple, c.descripcion as des_cuenta, c.naturaleza as Naturaleza,tblpolizas.fecha as fecha, tblpolizas.tipo as tipo, tblpolizas.numero as numero," +
"tblpolizasdetalles.descripcion, if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)) as cargo,if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)) as abono, round(tblcontabilidadsaldosi.saldoI,2) as saldoI,substr(tblpolizas.fecha,1,7) as fecha2 " +
"from tblpolizasdetalles inner join tblpolizas on tblpolizasdetalles.idPoliza=tblpolizas.id " +
 "inner join tblccontables as c on tblpolizasdetalles.idCuenta = c.idccontable " +
  "inner join tblcontabilidadsaldosi on tblcontabilidadsaldosi.idccontable=c.idccontable where tblPolizas.tipo<>'A' " +
  "and tblPolizas.fecha>='" + pFechaI + "' and tblPolizas.fecha<='" + pFechaF + "' " +
 "and concat(LPAD(c.cuenta," + NNiv1.ToString + ",'0'),if(c.n2='','',LPAD(c.n2," + NNiv2.ToString + ",'0')),if(c.n3='','',LPAD(c.n3," + NNiv3.ToString + ",'0')),if(c.n4='','',LPAD(c.n4," + NNiv4.ToString + ",'0')),if(c.n5='','',LPAD(c.n5," + NNiv5.ToString + ",'0')))>='" + inicioRango + "' and concat(LPAD(c.cuenta," + NNiv1.ToString + ",'0'),if(c.n2='','',LPAD(c.n2," + NNiv2.ToString + ",'0')),if(c.n3='','',LPAD(c.n3," + NNiv3.ToString + ",'0')),if(c.n4='','',LPAD(c.n4," + NNiv4.ToString + ",'0')),if(c.n5='','',LPAD(c.n5," + NNiv5.ToString + ",'0')))<='" + finRango + "' and (c.descontinuada like '%" + anio + "%')=False " +
"order by c.cuenta,tblpolizas.fecha,tblpolizas.tipo,tblpolizas.numero;"
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblauxiliarCuentas")
        ' DS2.WriteXmlSchema("tblauxiliarCuentas.xml")
        Return DS2.Tables("tblauxiliarCuentas")
    End Function

    Public Function imprimirTodascuentas(ByVal fechaDesde As String, ByVal fechaHasta As String, pCuenta1 As String, pCuenta2 As String) As DataView
        Dim ds As New DataSet
        Comm.CommandText = "select concat(LPAD(c.cuenta," + NNiv1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + NNiv2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + NNiv3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + NNiv4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + NNiv5.ToString + ",'0'))) as concaComple, c.descripcion as des_cuenta, c.naturaleza as Naturaleza,tblpolizas.fecha as fecha,tblpolizas.tipo as tipo, tblpolizas.numero as numero," +
"tblpolizasdetalles.descripcion, if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)) as cargo,if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)) as abono, round(tblcontabilidadsaldosi.saldoI,2) as saldoI,substr(tblpolizas.fecha,1,7) as fecha2 " +
"from tblpolizasdetalles inner join tblpolizas on tblpolizasdetalles.idPoliza=tblpolizas.id " +
"inner join tblccontables as c on tblpolizasdetalles.idCuenta = c.idccontable " +
"inner join tblcontabilidadsaldosi on tblcontabilidadsaldosi.idccontable=c.idccontable where tblPolizas.tipo<>'A' " +
"and tblpolizas.estado=3 and tblPolizas.fecha>='" + fechaDesde + "' and tblPolizas.fecha<='" + fechaHasta + "' " + " and (c.descontinuada like '%" + anio + "%')=False "

        If pCuenta1 <> "" And pCuenta2 <> "" Then
            Comm.CommandText += " and concat(lpad(c.Cuenta," + NNiv1.ToString + ",'0'),if(c.n2<>'',lpad(c.N2," + NNiv2.ToString + ",'0'),''),if(c.n3<>'',lpad(c.N3," + NNiv3.ToString + ",'0'),''),if(c.n4<>'',lpad(c.N4," + NNiv4.ToString + ",'0'),''),if(c.n5<>'',lpad(c.N5," + NNiv5.ToString + ",'0'),''))>='" + pCuenta1 + "'"
            Comm.CommandText += " and concat(lpad(c.Cuenta," + NNiv1.ToString + ",'0'),if(c.n2<>'',lpad(c.N2," + NNiv2.ToString + ",'0'),''),if(c.n3<>'',lpad(c.N3," + NNiv3.ToString + ",'0'),''),if(c.n4<>'',lpad(c.N4," + NNiv4.ToString + ",'0'),''),if(c.n5<>'',lpad(c.N5," + NNiv5.ToString + ",'0'),''))<='" + pCuenta2 + "'"
        End If
        Comm.CommandText += "order by c.cuenta,tblpolizas.fecha,tblpolizas.tipo,tblpolizas.numero;"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "tblauxiliarCuentas")
        'ds.WriteXmlSchema("tblauxiliarCuentas.xml")
        Return ds.Tables("tblauxiliarCuentas").DefaultView
    End Function

    Public Sub borraSaldosIniciales()
        Comm.CommandText = "delete from tblcontabilidadsaldosi"
        Comm.ExecuteNonQuery()
    End Sub



    Public Function listaCuentas() As List(Of String)
        Dim aux As New List(Of String)
        Comm.CommandText = "select idccontable,cuenta from tblccontables;"
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader = Comm.ExecuteReader
        While dr.Read()
            aux.Add(dr(0).ToString() + "," + dr(1).ToString())
        End While
        dr.Close()
        Return aux
    End Function
    Public Sub todosLosSaldos(ByVal pDesde As String, ByVal pHasta As String, pCuenta1 As String, pCuenta2 As String)

        Dim aux() As String = pDesde.Split("/")
        Dim mes As String = aux(1)
        Dim Anioint As String = aux(0)
        Comm.CommandText = "delete from tblcontabilidadsaldosi"
        Comm.ExecuteNonQuery()
        'Comm.CommandText = "insert into tblcontabilidadsaldosi(idCContable,Cuenta,N1,naturaleza,saldoI) Select tblccontables.idCContable, tblccontables.Cuenta, tblccontables.N2, tblccontables.Naturaleza,(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo)-if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono),if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)-if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo))),0) from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        'If mes = "01" Then
        '    Comm.CommandText += "tblPolizas.fecha>='" + anio.ToString + "/01/01' and tblPolizas.fecha<='" + anio.ToString + "/12/31' and tblPolizas.tipo='A' "
        'Else
        '    Comm.CommandText += "tblPolizas.fecha>='" + anio.ToString + "/01/01' and tblPolizas.fecha<'" + pDesde + "' "
        'End If
        'Comm.CommandText += " and concat(tblccontables.Cuenta,tblccontables.N2)=(select concat(conta.Cuenta,conta.N2) from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial from tblccontables  where tblccontables.Nivel<=2"
        'Comm.ExecuteNonQuery()

        'Dim aux() As String = pDesde.Split("/")
        'Dim mes = aux(1)
        Comm.CommandText = "insert into tblcontabilidadsaldosi(idCContable,Cuenta,N1,naturaleza,saldoI)" +
 "Select tblccontables.idCContable, tblccontables.Cuenta, tblccontables.N2, tblccontables.Naturaleza," +
"(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0)" +
" from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        If mes = "01" Then
            Comm.CommandText += "tblPolizas.fecha>='" + Anioint.ToString + "/01/01' and tblPolizas.fecha<='" + Anioint.ToString + "/12/31' and tblPolizas.tipo='A' "
        Else
            Comm.CommandText += "tblPolizas.fecha>='" + Anioint.ToString + "/01/01' and tblPolizas.fecha<'" + pDesde + "' "
        End If
        Comm.CommandText += " and tblpolizasdetalles.idCuenta=tblccontables.idccontable) as saldoInicial"
        'Comm.CommandText += " and concat(tblccontables.Cuenta,tblccontables.N2)=(select concat(conta.Cuenta,conta.N2) from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial " +
        Comm.CommandText += " from tblccontables" ' where concat(tblccontables.Cuenta,if(tblccontables.n2<>'',lpad(tblccontables.N2,4,'0'),''),if(tblccontables.n3<>'',lpad(tblccontables.N3,4,'0'),''),if(tblccontables.n4<>'',lpad(tblccontables.N4,4,'0'),''),if(tblccontables.n5<>'',lpad(tblccontables.N5,4,'0'),''))>='" + inicioRango + "'" +
        '" and concat(tblccontables.Cuenta,if(tblccontables.n2<>'',lpad(tblccontables.N2,4,'0'),''),if(tblccontables.n3<>'',lpad(tblccontables.N3,4,'0'),''),if(tblccontables.n4<>'',lpad(tblccontables.N4,4,'0'),''),if(tblccontables.n5<>'',lpad(tblccontables.N5,4,'0'),''))<='" + finRango + "';"
        If pCuenta1 <> "" And pCuenta2 <> "" Then
            Comm.CommandText += " where concat(lpad(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),if(tblccontables.n2<>'',lpad(tblccontables.N2," + NNiv2.ToString + ",'0'),''),if(tblccontables.n3<>'',lpad(tblccontables.N3," + NNiv3.ToString + ",'0'),''),if(tblccontables.n4<>'',lpad(tblccontables.N4," + NNiv4.ToString + ",'0'),''),if(tblccontables.n5<>'',lpad(tblccontables.N5," + NNiv5.ToString + ",'0'),''))>='" + pCuenta1 + "' and"
            Comm.CommandText += " concat(lpad(tblccontables.Cuenta," + NNiv1.ToString + ",'0'),if(tblccontables.n2<>'',lpad(tblccontables.N2," + NNiv2.ToString + ",'0'),''),if(tblccontables.n3<>'',lpad(tblccontables.N3," + NNiv3.ToString + ",'0'),''),if(tblccontables.n4<>'',lpad(tblccontables.N4," + NNiv4.ToString + ",'0'),''),if(tblccontables.n5<>'',lpad(tblccontables.N5," + NNiv5.ToString + ",'0'),''))<='" + pCuenta2 + "' "
        End If
            Comm.ExecuteNonQuery()

    End Sub
    Public Sub SaldosInicialesRango(ByVal pDesde As String, ByVal pHasta As String, ByVal PNivel As Integer, ByVal pN1 As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pIdCuenta As Integer, inicioRango As String, finRango As String)
        Dim aux() As String = pDesde.Split("/")
        Dim mes As String = aux(1)
        Dim anioint As String = aux(0)
        Comm.CommandText = "insert into tblcontabilidadsaldosi(idCContable,Cuenta,N1,naturaleza,saldoI)" +
 "Select tblccontables.idCContable, tblccontables.Cuenta, tblccontables.N2, tblccontables.Naturaleza," +
"(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0)" +
" from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        If mes = "01" Then
            Comm.CommandText += "tblPolizas.fecha>='" + anioint.ToString + "/01/01' and tblPolizas.fecha<='" + anioint.ToString + "/12/31' and tblPolizas.tipo='A' "
        Else
            Comm.CommandText += "tblPolizas.fecha>='" + anioint.ToString + "/01/01' and tblPolizas.fecha<'" + pDesde + "' "
        End If
        'Comm.CommandText += " and concat(tblccontables.Cuenta,tblccontables.N2)=(select concat(conta.Cuenta,conta.N2) from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial " +
        Comm.CommandText += " and tblpolizasdetalles.idCuenta=tblccontables.idccontable) as saldoInicial" +
" from tblccontables  where concat(tblccontables.Cuenta,if(tblccontables.n2<>'',lpad(tblccontables.N2,4,'0'),''),if(tblccontables.n3<>'',lpad(tblccontables.N3,4,'0'),''),if(tblccontables.n4<>'',lpad(tblccontables.N4,4,'0'),''),if(tblccontables.n5<>'',lpad(tblccontables.N5,4,'0'),''))>='" + inicioRango + "'" +
" and concat(tblccontables.Cuenta,if(tblccontables.n2<>'',lpad(tblccontables.N2,4,'0'),''),if(tblccontables.n3<>'',lpad(tblccontables.N3,4,'0'),''),if(tblccontables.n4<>'',lpad(tblccontables.N4,4,'0'),''),if(tblccontables.n5<>'',lpad(tblccontables.N5,4,'0'),''))<='" + finRango + "';"
        Comm.ExecuteNonQuery()

    End Sub

    Public Function AuxiliarRangoMayores(ByVal desde As String, ByVal hasta As String, ByVal pFechaI As String, ByVal pFechaF As String) As DataView
        Comm.CommandText = "select concat(LPAD(c.cuenta," + NNiv1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + NNiv2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + NNiv3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + NNiv4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + NNiv5.ToString + ",'0'))) as concaComple, c.descripcion as des_cuenta, c.naturaleza as Naturaleza,tblpolizas.fecha as fecha, tblpolizas.tipo as tipo, tblpolizas.numero as numero," +
"tblpolizasdetalles.descripcion, if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)) as cargo,if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)) as abono, round(tblcontabilidadsaldosi.saldoI,2) as saldoI,substr(tblpolizas.fecha,1,7) as fecha2 " +
"from tblpolizasdetalles inner join tblpolizas on tblpolizasdetalles.idPoliza=tblpolizas.id " +
"inner join tblccontables as c on tblpolizasdetalles.idCuenta = c.idccontable " +
"inner join tblcontabilidadsaldosi on tblcontabilidadsaldosi.idccontable=c.idccontable where tblPolizas.tipo<>'A' " +
"and tblPolizas.fecha>='" + pFechaI + "' and tblPolizas.fecha<='" + pFechaF + "' and (c.descontinuada like '%" + anio + "%')=False " +
"and c.cuenta>='" + desde + "' and c.cuenta<='" + hasta + "' order by c.cuenta,tblpolizas.fecha,tblpolizas.tipo,tblpolizas.numero;"
        Dim DS2 As New DataSet
        Dim DA2 As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA2.Fill(DS2, "tblauxiliarCuentas")
        Return DS2.Tables("tblauxiliarCuentas").DefaultView
    End Function






    Public Sub iniciaSaldosPorMayores(ByVal desde As String, ByVal hasta As String, ByVal fechaDesde As String, ByVal fechaHasta As String)
        Dim aux() As String = fechaDesde.Split("/")
        Dim anioint As String = aux(0)
        Dim mes = aux(1)
        Comm.CommandText = "insert into tblcontabilidadsaldosi(idCContable,Cuenta,N1,naturaleza,saldoI)" +
 "Select tblccontables.idCContable, tblccontables.Cuenta, tblccontables.N2, tblccontables.Naturaleza," +
"(select ifnull(sum(if(tblccontables.Naturaleza=0,if( tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2))-if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2)),if(tblpolizasdetalles.abono=-999999999,0,round(tblpolizasdetalles.abono,2))-if(tblpolizasdetalles.cargo=-999999999,0,round(tblpolizasdetalles.cargo,2)))),0)" +
" from tblpolizasdetalles inner join tblPolizas on tblpolizasdetalles.idPoliza=tblPolizas.id where "
        If mes = "01" Then
            Comm.CommandText += "tblPolizas.fecha>='" + anioint.ToString + "/01/01' and tblPolizas.fecha<='" + anioint.ToString + "/12/31' and tblPolizas.tipo='A' "
        Else
            Comm.CommandText += "tblPolizas.fecha>='" + anioint.ToString + "/01/01' and tblPolizas.fecha<'" + fechaDesde + "' "
        End If
        'Comm.CommandText += " and concat(tblccontables.Cuenta,tblccontables.N2)=(select concat(conta.Cuenta,conta.N2) from tblccontables as conta where conta.idCContable=tblpolizasdetalles.idCuenta)) as saldoInicial " +
        Comm.CommandText += " and tblpolizasdetalles.idCuenta=tblccontables.idccontable) as saldoInicial" +
" from tblccontables  where tblccontables.cuenta>='" + desde + "'" +
" and tblccontables.cuenta<='" + hasta + "';"
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub LlenaDatosDetalle(pid As Integer)
        Dim Dr As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblpolizasdetalles where id=" + pid.ToString
        Dr = Comm.ExecuteReader
        If Dr.Read Then
            DetalleId = pid
            DetalleIdPoliza = Dr("idpoliza")
            Detallecuenta = Dr("cuenta")
            Detalledescripcion = Dr("descripcion")
            Detallecargo = Dr("cargo")
            DetalleAbono = Dr("abono")
            DetalleidCuenta = Dr("idcuenta")
            Detallefactura = Dr("factura")
            Detalleidproveedor = Dr("idproveedor")
            Detalleiva = Dr("iva")
            Detalleconcepto = Dr("concepto")
            DetalleesDIOT = Dr("esdiot")
            DetalleDIOTHabilitado = Dr("diothabilitado")
            DetallefechaDiot = Dr("fechadiot")
            DetallevalorActos = Dr("valoractos")
            Detallereferencia = Dr("referencia")
            Detalleivaret = Dr("ivaret")
            Detalleieps = Dr("ieps")
            DetallesOrden = Dr("orden")
        End If
        Dr.Close()
        Comm.CommandText = "select ifnull((select orden from tblpolizasdetalles where idpoliza=" + IDPoliza.ToString + " order by orden desc limit 1),0)"
        UltimoOrden = Comm.ExecuteScalar + 1
    End Sub
    Public Sub CambiaOrden(pidPoliza As Integer, pOrden As Integer)
        Comm.CommandText = "update tblpolizasdetalles set orden=orden+1 where idpoliza=" + pidPoliza.ToString + " and orden>" + pOrden.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaDetalles(ByVal pIdPoliza As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select id,idcuenta,(select concat(LPAD(c.cuenta," + NNiv1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + NNiv2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + NNiv3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + NNiv4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + NNiv5.ToString + ",'0'))) from tblccontables c where tblpolizasdetalles.idcuenta=c.idccontable),descripcion,if(cargo=-999999999,'',cargo) as cargo,if(abono=-999999999,'',abono) as abono from tblpolizasdetalles where idpoliza=" + pIdPoliza.ToString + " order by orden"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpolizadetalles")
        Return DS.Tables("tblpolizadetalles").DefaultView
    End Function
    Public Function DaTotalCargo(pIdPoliza As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(if(tblpolizasdetalles.cargo=-999999999,0,tblpolizasdetalles.cargo)) from tblpolizasdetalles where idpoliza=" + pIdPoliza.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaTotalAbono(pIdPoliza As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(if(tblpolizasdetalles.abono=-999999999,0,tblpolizasdetalles.abono)) from tblpolizasdetalles where idpoliza=" + pIdPoliza.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Sub llenaDatosPolizaN(ByVal pID As Integer)
        IDPoliza = pID
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblpolizas where id=" + IDPoliza.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            tipo2 = DReader("tipo")
            Numero = DReader("numero")
            fecha = DReader("fecha")
            concepto = DReader("concepto")
            beneficiario = DReader("beneficiario")
            Importe = DReader("importe")
            IdClasificacion = DReader("clasificacion")
            CantidadCheque = DReader("cantidadcheque")
            Leyenda = DReader("leyendach")
        End If
        DReader.Close()
        If tipo2 = "E" Then
            tipo = 0
        End If
        If tipo2 = "I" Then
            tipo = 1
        End If
        If tipo2 = "D" Then
            tipo = 2
        End If
        If tipo2 = "O" Then
            tipo = 2
        End If
        If tipo2 = "A" Then
            tipo = 3
        End If
        Comm.CommandText = "select ifnull((select orden from tblpolizasdetalles where idpoliza=" + IDPoliza.ToString + " order by orden desc limit 1),0)"
        UltimoOrden = Comm.ExecuteScalar + 1
        DetallesOrden = UltimoOrden
    End Sub
    Public Function DaCuentaTxt(pIdCuenta As Integer) As String
        Comm.CommandText = "select ifnull((select concat(LPAD(c.cuenta," + NNiv1.ToString + ",'0'),' ',if(c.n2='','',LPAD(c.n2," + NNiv2.ToString + ",'0')),' ',if(c.n3='','',LPAD(c.n3," + NNiv3.ToString + ",'0')),' ',if(c.n4='','',LPAD(c.n4," + NNiv4.ToString + ",'0')),' ',if(c.n5='','',LPAD(c.n5," + NNiv5.ToString + ",'0'))) as Cuenta from tblccontables as c where idccontable=" + pIdCuenta.ToString + "),'')"
        Return Comm.ExecuteScalar
    End Function
End Class
