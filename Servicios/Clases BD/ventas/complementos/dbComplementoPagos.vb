Public Class dbComplementoPagos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public IdCPago As Integer
    Public Fecha As String
    Public Hora As String
    Public idForma As Integer
    Public idMoneda As Integer
    Public TipoDeCambio As Double
    Public Monto As Double
    Public NumOperacion As String
    Public RFCEmisorCuenta As String
    Public NombreBancoExt As String
    Public NoCuentaEmisor As String
    Public RFCEmisorBen As String
    Public NoCuenta As String
    Public TipoCadPago As String
    Public CertificadoPago As String
    Public CadenaPago As String
    Public SelloPago As String
    Public Relaciones As New Collection
    Public Serie As String
    Public Folio As Integer
    Public Estado As Byte
    Public EsElectronica As Byte
    Public IdCliente As Integer

    Public NoCertificado As String
    Public uuid As String
    Public FechaTimbrado As String
    Public SelloCFD As String
    Public NoCertificadoSAT As String
    Public SelloSAT As String
    Public RFCProvCertif As String


    'Public Structure Relacionados
    Public IdDetalle As Integer
    Public IdVenta As Integer
    Public IdDocumento As String
    Public SerieR As String
    Public FolioR As Integer
    Public MonedaDR As String
    Public TipodeCambioDR As Double
    Public MetodoPagoDR As String
    Public NumParcialidad As Integer
    Public ImporteSaldoAnt As Double
    Public ImportePagado As Double
    Public ImporteSaldoInsoluto As Double
    Public FechaCancelado As String

    'End Structure


    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub

    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer, ByVal pEsElectronica As Integer, pModoB As String) As Integer
        Comm.CommandText = "select ifnull((select folio from tblcomplementopagos where estado>=3 and serie='" + Replace(Trim(pSerie), "'", "''") + "' order by folio desc limit 1),0)"
        Return Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Comm.CommandText = "select count(folio) from tblcomplementopagos where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2"
        If Comm.ExecuteScalar = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    Public Sub Guardar()
        Comm.CommandText = "insert into tblcomplementopagos(fecha,hora,idforma,idmoneda,tipodecambio,monto,numoperacion,rfcemisorcuenta,nombrebancoext,nocuentaemisor,rfcemisorben,nocuenta,tipocadpago,certificadopago,cadenapago,sellopago,serie,folio,estado,fechacancelado,horacancelado,eselectronica,idcliente,uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat,rfcprovcertif,nocertificado) values(" +
        "'" + Fecha + "'," +
        "'" + Format(TimeOfDay, "HH:mm:ss") + "'" +
        idForma.ToString + "," +
        idMoneda.ToString + "," +
        TipoDeCambio.ToString + "," +
        Monto.ToString + "," +
        "'" + NumOperacion.Replace("'", "''") + "'," +
        "'" + RFCEmisorCuenta.Replace("'", "''") + "'," +
        "'" + NombreBancoExt.Replace("'", "''") + "'," +
        "'" + NoCuentaEmisor.Replace("'", "''") + "'," +
        "'" + RFCEmisorBen.Replace("'", "''") + "'," +
        "'" + NoCuenta.Replace("'", "''") + "'," +
        "'" + TipoCadPago.Replace("'", "''") + "'," +
        "'" + CertificadoPago.Replace("'", "''") + "'," +
        "'" + CadenaPago.Replace("'", "''") + "'," +
        "'" + SelloPago.Replace("'", "''") + "'," +
        "'" + Serie.Replace("'", "''") + "'," +
        Folio.ToString + "," +
        "1," +
        "'" + Fecha + "'," +
        "'" + Format(TimeOfDay, "HH:mm:ss") + "'," +
        EsElectronica.ToString + "," + IdCliente.ToString + ",'','','','','',''," +
        "'" + NoCertificado + "')"
        Comm.ExecuteNonQuery()

        
    End Sub

    Public Sub Modificar()
        Comm.CommandText = "update tblcomplementopagos set " +
        "fecha='" + Fecha + "'," +
        "idforma=" + idForma.ToString + "," +
        "idmoneda=" + idMoneda.ToString + "," +
        "tipodecambio=" + TipoDeCambio.ToString + "," +
        "monto=" + Monto.ToString + "," +
        "numoperacion='" + NumOperacion.Replace("'", "''") + "'," +
        "rfcemisorcuenta='" + RFCEmisorCuenta.Replace("'", "''") + "'," +
        "nombrebancoext='" + NombreBancoExt.Replace("'", "''") + "'," +
        "nocuentaemisor='" + NoCuentaEmisor.Replace("'", "''") + "'," +
        "rfcemisorben='" + RFCEmisorBen.Replace("'", "''") + "'," +
        "nocuenta='" + NoCuenta.Replace("'", "''") + "'," +
        "tipocadpago='" + TipoCadPago.Replace("'", "''") + "'," +
        "certificado='" + CertificadoPago.Replace("'", "''") + "'," +
        "cadenapago='" + CadenaPago.Replace("'", "''") + "'," +
        "sellopago='" + SelloPago.Replace("'", "''") + "'," +
        "serie='" + Serie.Replace("'", "''") + "'," +
        "folio=" + Folio.ToString + "," +
        "estado=" + Estado.ToString +
        "fechacancelado='" + Fecha + "'," +
        "horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "'" +
        " where idcpago=" + IdCPago.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Cancelar()
        Comm.CommandText = "update tblcomplementopagos set estado=4 where idcpago=" + IdCPago.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcomplementopagos where idcpago=" + IdCPago.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then

            Fecha = DReader("fecha")
            idForma = DReader("idforma")
            idMoneda = DReader("idmoneda")
            TipoDeCambio = DReader("tipodecambio")
            Monto = DReader("monto")
            NumOperacion = DReader("numoperacion")
            RFCEmisorCuenta = DReader("rfcemisorcuenta")
            NombreBancoExt = DReader("nombrebancoext")
            NoCuentaEmisor = DReader("nocuentaemisor")
            RFCEmisorBen = DReader("rfcemisorben")
            NoCuenta = DReader("nocuenta")
            TipoCadPago = DReader("tipocadpago")
            CertificadoPago = DReader("certificado")
            CadenaPago = DReader("cadenapago")
            SelloPago = DReader("sellopago")
            Serie = DReader("serie")
            Folio = DReader("folio")
            Estado = DReader("estado")
            FechaCancelado = DReader("fechacancelado")
            EsElectronica = DReader("eselectronica")
            IdCliente = DReader("idcliente")
            uuid = DReader("uuid")
            FechaTimbrado = DReader("fechatimbrado")
            SelloCFD = DReader("sellocfd")
            NoCertificadoSAT = DReader("nocertificadosat")
            SelloSAT = DReader("sellosat")
            RFCProvCertif = DReader("rfcprovcertif")
            NoCertificado = DReader("nocertificado")
        End If
    End Sub
    Public Sub ModificarFechaHora()
        Comm.CommandText = "update fecha='" + Date.Now.ToString("yyyy/MM/dd") + "' hora='" + TimeOfDay.ToString("HH:mm:ss") + "' where idcpago=" + IdCPago.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar()
        Comm.CommandText = "delete from tblcomplementopagos where idcpago=" + IdCPago.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub GuardaDatosTimbrado()
        Comm.CommandText = "update tblcomplementopagos set uuid='" + uuid + "',sellocfd='" + SelloCFD + "',nocertificadosat='" + NoCertificadoSAT + "',fechatimbrado='" + FechaTimbrado + "',sellosat='" + SelloSAT + "',rfcprovcertif='" + RFCProvCertif + "' where idcpago=" + IdCPago.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub GuardaRelacion()
        Comm.CommandText = "insert into tblcomplementopagosdetalles(idventa,iddocumento,serier,folior,monedadr,tipodecambiodr,metododepagodr,numparcialidad,importesaldoant,importepagado,importesaldoinsoluto,idcpago) values(" +
        IdVenta.ToString + "," +
        "'" + IdDocumento + "'," +
        "'" + SerieR.Replace("'", "''") + "'," +
        FolioR.ToString() + "," +
        "'" + MonedaDR + "'," +
        TipodeCambioDR.ToString() + "," +
        "'" + MetodoPagoDR + "'," +
        NumParcialidad.ToString + "," +
        ImporteSaldoAnt.ToString + "," +
        ImportePagado.ToString + "," +
        ImporteSaldoInsoluto + "," +
        IdCPago.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminaRelacion()
        Comm.CommandText = "delete from tblcomplemtopagosdetalles where iddetalle=" + IdDetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaDetallesReader() As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcomplementopagosdetalles where idcpago=" + IdCPago.ToString
        Return Comm.ExecuteReader
    End Function
    Public Sub LigarAVentasPagos(ByVal pId As Integer, ByVal pWherestr As String)
        pWherestr = pWherestr.Substring(3, pWherestr.Length - 3)
        Comm.CommandText = "update tblventaspagos set idcpago=" + pId.ToString + " where " + pWherestr
        Comm.ExecuteNonQuery()
    End Sub

    Public Function CreaCadenaOriginali33(ByVal pIdNomina As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer, pXMLINE As String, pEsEgreso As Byte, pCadenaOriginalComp As String, pIdSucursal As Integer) As String
        Dim CO As String = "|3.3|"

        Dim en As New Encriptador
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(pIdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        LlenaDatos()
        If TipoDeCambio = 0 Then TipoDeCambio = 1
        Dim Sucursal As New dbSucursales(pIdSucursal, MySqlcon)
        If Serie <> "" Then CO += Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"

        'If pSelloDigital <> "" Then CO += "Sello=""" + pSelloDigital + """ "

        'Dim strMetodos As String = ""
        'Dim MeP As New dbVentasAddMetodos(Comm.Connection)
        'DR = MeP.ConsultaReader(0, ID)
        'DR.Read()
        'If strMetodos <> "" Then strMetodos += ","
        'If DR("clavesat") < 1000 Then
        'strMetodos += Format(DR("clavesat"), "00")
        'Else
        'strMetodos += "NA"
        'End If
        'DR.Close()
        'CO += strMetodos + "|"
        'CO += "99|"
        If NoCertificado <> "" Then CO += NoCertificado + "|"
        'CO += "Certificado=""" + en.Certificado64 + """ "

        'CO+="CondicionesDePago="""""

        CO += "0|"
        'If TotalExentoD + TotalGravadoD > 0 Then CO += Format(TotalExentoD + TotalGravadoD, "#0.00####") + "|"
        CO += "XXX|"
        CO += "0|"


        'Dim CP As New dbVentasCartaPorte(ID, MySqlcon)

        CO += "P|"


        'If IdFormadePago <> 98 Then
        'CO += "PUE|"
        'End If

        If Sucursal.CP2 <> "" Then
            CO += Sucursal.CP2 + "|"
        Else
            CO += Sucursal.CP + "|"
        End If
        'Confirmacion
        'If NoConfirmacion <> "" Then CO += NoConfirmacion + "|"

        'CFDIS relacionados aqui'
        'CO += "01|"
        'DR = DaIvasUUIDS(ID)
        'While DR.Read
        '    CO += DR("uuid") + "|"
        'End While
        'DR.Close()

        CO += Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Sucursal.ClaveRegimen.ToString + "|"

        Dim Cliente As New dbClientes(IdCliente, Comm.Connection)
        CO += Replace(Replace(Replace(Replace(Replace(cliente.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Replace(Replace(Replace(Replace(Replace(Cliente.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"

        CO += "P01|"


        CO += "84111506|"
        CO += "1|"
        CO += "ACT|"
        'CO += "ACT|"
        CO += "Pago|"

        CO += "0|"
        CO += "0|"
        'If TotalExentoD + TotalGravadoD > 0 Then CO += Format(TotalExentoD + TotalGravadoD, "#0.00####") + "|"

        '-----------------Aqui lo del pago

        CO += "1.0|"
        CO += +Fecha.Replace("/", "-") + "T" + Hora + "|"
        CO += idForma.ToString("00") + "|"
        If idMoneda = 2 Then
            CO += "MXN|"
        Else
            CO += "USD|"
            CO += TipoDeCambio.ToString + "|"
        End If
        CO += +Monto.ToString("0.00") + "|"
        If idForma <> 1 And idForma <> 99 Then
            CO += NumOperacion + "|"
            CO += RFCEmisorCuenta + "|"
            If NombreBancoExt <> "" Then CO += NombreBancoExt + "|"
            CO += NoCuentaEmisor + "|"
            CO += RFCEmisorBen + "|"
            CO += NoCuenta + "|"
            If TipoCadPago <> "" Then
                CO += "01|"
                CO += CertificadoPago + "|"
                CO += CadenaPago + "|"
                CO += SelloPago + "|"
            End If
        End If
        DR = ConsultaDetallesReader()
        While DR.Read
            CO += DR("iddocumento") + "|"
            CO += DR("serie") + "|"
            CO += DR("folio").ToString + "|"
            CO += DR("monedadr") + "|"
            If DR("moedadr") <> "MXN" Then
                CO += DR("tipocambiodr").ToString("0.00") + "|"
            End If
            CO += DR("metododepagodr") + "|"
            If DR("metodopagodr") = "PPD" Then CO += DR("numparcialidad") + "|"
            If DR("metodopagodr") = "PPD" Then CO += DR("importesaldoant").ToString("0.00") + "|"
            CO += DR("importepagado").ToString("0.00") + "|"
            If DR("metodopagodr") = "PPD" Then CO += DR("importesaldoinsoluto").ToString("0.00") + "|"
        End While
        DR.Close()

        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        Replace(CO, "----", "  ")
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        en.GuardaArchivoTexto("co.txt", CO, System.Text.Encoding.Default)
        Return CO
    End Function
    Public Function CreaXMLi33(ByVal pIdNomina As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer, pXMLINE As String, pIdSucursal As Integer) As String
        Dim en As New Encriptador
        Dim XMLDoc As String
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>"
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        XMLDoc += "<cfdi:Comprobante "
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(pIdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        LlenaDatos()
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        If TipoDeCambio = 0 Then TipoDeCambio = 1
        Dim Sucursal As New dbSucursales(pIdSucursal, Comm.Connection)
        Dim Cl As New dbClientes(IdCliente, Comm.Connection)
        XMLDoc += "Version=""3.3"" "
        If Serie <> "" Then XMLDoc += "Serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        XMLDoc += "Folio=""" + Folio.ToString + """ "
        XMLDoc += "Fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """ "
        If Sucursal.RFC <> "SUL010720JN8" Then
            XMLDoc += "Sello=""" + pSelloDigital + """ "
        Else
            XMLDoc += "Sello="""" "
        End If

        'Dim strMetodos As String = ""
        'Dim MeP As New dbVentasAddMetodos(Comm.Connection)
        'DR = MeP.ConsultaReader(0, ID)
        'DR.Read()
        'While DR.Read()
        '    If strMetodos <> "" Then strMetodos += ","
        '    If DR("clavesat") < 1000 Then
        '        strMetodos += Format(DR("clavesat"), "00")
        '    Else
        '        strMetodos += "NA"
        '    End If
        'End While
        'DR.Close()

        'XMLDoc += "FormaPago=""99"" "

        If NoCertificado <> "" Then XMLDoc += "NoCertificado=""" + NoCertificado + """ "
        If Sucursal.RFC <> "SUL010720JN8" Then
            XMLDoc += "Certificado=""" + en.Certificado64 + """ "
        Else
            XMLDoc += "Certificado="""" "
        End If
        'xmldoc+="CondicionesDePago="""""

        XMLDoc += "SubTotal=""0"" "


        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        'If Descuento + DescuentoG2 > 0 Then
        '    If pEsEgreso = 0 Then
        'If TotalExentoD + TotalGravadoD > 0 Then XMLDoc += "Descuento=""" + Format(TotalExentoD + TotalGravadoD, "#0.00####") + """ "
        '    Else
        '        XMLDoc += "Descuento=""" + Format(If(Descuento + DescuentoG2 >= 0, Descuento + DescuentoG2, (Descuento + DescuentoG2) * -1), "#0.00####") + """ "
        '    End If
        'End If

        'Tipo deCambio nuevo
        'If IdMoneda <> 2 Then
        '    Dim Moneda As New dbMonedas(IdMoneda, Comm.Connection)
        '    XMLDoc += "Moneda=""" + Moneda.Abreviatura + """ "
        '    XMLDoc += "TipoCambio=""" + Format(TipodeCambio, "#0.00####") + """ "
        'Else
        XMLDoc += "Moneda=""XXX"" "
        'End If

        XMLDoc += "Total=""0"" "
        XMLDoc += "TipoDeComprobante=""P"" "
        'XMLDoc += "MetodoPago=""PUE"" "

        If Sucursal.CP2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.CP2 + """ "
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.CP + """ "
        End If
        'Confirmacion
        'If NoConfirmacion <> "" Then XMLDoc += " Confirmacion=""" + NoConfirmacion + """"


        XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:pago10=""http://www.sat.gob.mx/Pagos"" "
        XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd"

        XMLDoc += """ "

        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        XMLDoc += ">"
        'Relacionados aqui

        XMLDoc += "<cfdi:Emisor Rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """"
        XMLDoc += " RegimenFiscal=""" + Sucursal.ClaveRegimen.ToString + """"
        XMLDoc += "/>"


        XMLDoc += "<cfdi:Receptor Rfc=""" + Replace(Replace(Replace(Replace(Replace(Cl.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Nombre=""" + Replace(Replace(Replace(Replace(Replace(Cl.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """"
        XMLDoc += " UsoCFDI=""P01"""
        XMLDoc += "/>"


        XMLDoc += "<cfdi:Conceptos>"

        XMLDoc += "<cfdi:Concepto "
        XMLDoc += "ClaveProdServ=""84111506"" "
        XMLDoc += "Cantidad=""1"" "
        XMLDoc += "ClaveUnidad=""ACT"" "
        'XMLDoc += "Unidad=""ACT"" "
        XMLDoc += "Descripcion=""Pago"" "
        XMLDoc += "ValorUnitario=""0"" "
        XMLDoc += "Importe=""0"" "
        'If TotalExentoD + TotalGravadoD > 0 Then XMLDoc += "Descuento=""" + Format(TotalExentoD + TotalGravadoD, "#0.00####") + """ "
        XMLDoc += "/>"

        XMLDoc += "</cfdi:Conceptos>"

        '-----------------Aqui lo de los pagos

        XMLDoc += "<cfdi:Complemento>"
        XMLDoc += "<pago10:Pagos Version=""1.0"">"
        XMLDoc += "<pago10:Pago "
        XMLDoc += "FechaPago""" + Fecha.Replace("/", "-") + "T" + Hora + """ "
        XMLDoc += "FormaDePagoP=""" + idForma.ToString("00") + """ "
        If idMoneda = 2 Then
            XMLDoc += "MonedaP=""MXN"" "
        Else
            XMLDoc += "MonedaP=""USD"" "
            XMLDoc += "TipoCambioP=""" + TipoDeCambio.ToString + """ "
        End If
        XMLDoc += "Monto=""" + Monto.ToString("0.00") + """ "
        If idForma <> 1 And idForma <> 99 Then
            XMLDoc += "NumOperacion=""" + NumOperacion + """ "
            XMLDoc += "RfcEmisorCtaOrd=""" + RFCEmisorCuenta + """ "
            If NombreBancoExt <> "" Then XMLDoc += "NomBancoOrdExt=""" + NombreBancoExt + """ "
            XMLDoc += "CtaOrdenante=""" + NoCuentaEmisor + """ "
            XMLDoc += "RfcEmisorBen=""" + RFCEmisorBen + """ "
            XMLDoc += "CtaBeneficiario=""" + NoCuenta + """ "
            If TipoCadPago <> "" Then
                XMLDoc += "TipoCadPago=""01"" "
                XMLDoc += "CertPago=""" + CertificadoPago + """ "
                XMLDoc += "CadPago=""" + CadenaPago + """ "
                XMLDoc += "SelloPago=""" + SelloPago + """ "
            End If
        End If

        DR = ConsultaDetallesReader()
        While DR.Read
            XMLDoc += "<pago10:DoctoRelacionado "
            XMLDoc += "IdDocumento=""" + DR("iddocumento") + """ "
            XMLDoc += "Serie=""" + DR("serie") + """ "
            XMLDoc += "Folio=""" + DR("folio").ToString + """ "
            XMLDoc += "MonedaDR" + DR("monedadr") + """ "
            If DR("moedadr") <> "MXN" Then
                XMLDoc += "TipoCambioDR=""" + DR("tipocambiodr").ToString("0.00") + """ "
            End If
            XMLDoc += "MetodoDePagoDR=""" + DR("metododepagodr") + """ "
            If DR("metodopagodr") = "PPD" Then XMLDoc += "NumParcialidad=""" + DR("numparcialidad") + """ "
            If DR("metodopagodr") = "PPD" Then XMLDoc += "ImpSaldoAnt=""" + DR("importesaldoant").ToString("0.00") + """ "
            XMLDoc += "ImpPagado=" + DR("importepagado").ToString("0.00") + """ "
            If DR("metodopagodr") = "PPD" Then XMLDoc += "ImpSaldoInsoluto=""" + DR("importesaldoinsoluto").ToString("0.00") + """ "
            XMLDoc += "/>"
        End While
        DR.Close()

        XMLDoc += "</pago10:Pagos>"
        XMLDoc += "</cfdi:Complemento>"


        XMLDoc += "</cfdi:Comprobante>"
        Return XMLDoc

    End Function
End Class
