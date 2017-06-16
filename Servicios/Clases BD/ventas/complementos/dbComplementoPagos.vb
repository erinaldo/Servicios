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
        Comm.CommandText = "insert into tblcomplementopagos(fecha,hora,idforma,idmoneda,tipodecambio,monto,numoperacion,rfcemisorcuenta,nombrebancoext,nocuentaemisor,nocuenta,tipocadpago,certificadopago,cadenapago,sellopago,serie,folio,estado,fechacancelado,horacancelado,eselectronica,idcliente,uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat,rfcprovcertif) values(" +
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
        EsElectronica.ToString + "," + IdCliente.ToString + ",'','','','','','')"
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
        CO += "99|"
        If NoCertificado <> "" Then CO += NoCertificado + "|"
        'CO += "Certificado=""" + en.Certificado64 + """ "

        'CO+="CondicionesDePago="""""

        CO += Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + "|"
        If TotalExentoD + TotalGravadoD > 0 Then CO += Format(TotalExentoD + TotalGravadoD, "#0.00####") + "|"
        CO += "MXN|"
        CO += Format(TotalExentoP + TotalGravadoP - TotalExentoD - TotalGravadoD + totalOtrosPagos, "#0.00####") + "|"


        'Dim CP As New dbVentasCartaPorte(ID, MySqlcon)

        CO += "N|"


        'If IdFormadePago <> 98 Then
        CO += "PUE|"
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


        CO += Replace(Replace(Replace(Replace(Replace(Trabajador.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Replace(Replace(Replace(Replace(Replace(Trabajador.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"

        CO += "P01|"


        CO += "84111505|"
        CO += "1|"
        CO += "ACT|"
        'CO += "ACT|"
        CO += "Pago de nómina|"

        CO += Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + "|"
        CO += Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + "|"
        If TotalExentoD + TotalGravadoD > 0 Then CO += Format(TotalExentoD + TotalGravadoD, "#0.00####") + "|"

        '-----------------Aqui lo de la nomina
        CO += "1.2|"
        CO += tipoNomina + "|"
        CO += Replace(FechaPago, "/", "-") + "|" 'fecha pago
        CO += Replace(FechaInicialPago, "/", "-") + "|" 'fecha inicial
        CO += Replace(FechaFinalPAgo, "/", "-") + "|" ' fecha final
        CO += Format(DiasPagados, "0") + "|" ' numero dias
        If TotalExentoP + TotalGravadoP <> 0 Then CO += Format(TotalExentoP + TotalGravadoP, "#0.00####") + "|"
        If TotalExentoD + TotalGravadoD <> 0 Then CO += Format(TotalExentoD + TotalGravadoD, "#0.00####") + "|"
        If totalOtrosPagos <> 0 Or CantidadOtrosPagos <> 0 Then CO += Format(totalOtrosPagos, "#0.00####") + "|"
        If Sucursal.CURP <> "" Then CO += Sucursal.CURP.Trim + "|"
        If Trabajador.RegistroPatronal <> "" Then CO += Trabajador.RegistroPatronal + "|"
        If Trabajador.RFCPatronOrigen <> "" Then CO += Trabajador.RFCPatronOrigen.Trim + "|"
        If origenRecurso <> "NA" And origenRecurso <> "" Then
            CO += origenRecurso + "|"
            If origenRecurso = "IM" Then CO += Format(montoRecurso, "0.00####") + "|"
        End If

        'If Trabajador.RegistroPatronal
        CO += Trim(Trabajador.Curp) + "|" 'curp
        CO += Trim(Trabajador.NumeroSeguroSocial) + "|" 'num seg social
        If Antiguedad <> 0 And Trabajador.RegistroPatronal <> "" Then
            CO += Replace(Trabajador.FechaInicioLaboral, "/", "-") + "|" 'fechainiciolaboral
            CO += CStr(Antiguedad) + "|" 'anti
        End If
        'CO += "01|"
        CO += Trabajador.TipoContrato.Substring(0, 2) + "|" 'tipocontrato
        If Trabajador.sindicalizado = 1 Then CO += "Sí|"
        CO += Trabajador.TipoJornada.Substring(0, 2) + "|" 'tipo jornada
        'CO += "01|"
        If Trabajador.TipoRegimen < 12 Then
            CO += Trim(Trabajador.TipoRegimen.ToString("00")) + "|" 'tipo regimen
        Else
            CO += "99|"
        End If
        CO += Trim(Trabajador.NumeroEmpleado) + "|" 'num empleado
        CO += Trabajador.Departamento + "|" ' departamento
        CO += Trabajador.Puesto + "|" 'puesto
        If Trabajador.RiesgoPuesto < 6 Then CO += Trabajador.RiesgoPuesto.ToString + "|" 'riesgo puesto
        If Trabajador.Periodicidad <> "" And Trabajador.Periodicidad <> "No aplica" Then CO += Trabajador.Periodicidad.Substring(0, 2) + "|" 'periodicidad pago
        'CO += "04|"
        If Trabajador.Banco > 0 Then
            CO += Format(Trabajador.Banco, "000") + "|" 'banco
            If Trabajador.CLABE <> "" And Trabajador.CLABE.Length <> 18 Then
                CO += Trabajador.CLABE + "|" 'Clabe
            End If
        End If
        If Trabajador.Banco = 0 And Trabajador.CLABE.Length = 18 Then
            CO += Trabajador.CLABE + "|" 'Clabe
        End If
        If Trabajador.SalarioBaseCotApor <> 0 Then CO += Format(Trabajador.SalarioBaseCotApor, "#0.00####") + "|" 'salariobato a cot
        If Trabajador.SalarioDiarioIntegrado <> 0 Then CO += Format(Trabajador.SalarioDiarioIntegrado, "#0.00####") + "|" 'salario diaro
        CO += DaClaveEstadosMexico(Trabajador.EstadoLabora) + "|"
        Dim Subc As New dbnominasubcontratacion(Comm.Connection)
        DR = Subc.ConsultaReader(IdTrabajador)
        While DR.Read
            CO += DR("rfclaboral".Trim) + "|"
            CO += DR("porcentaje").ToString + "|"
        End While
        DR.Close()

        Dim HE As New dbNoominaHorasExtra(Comm.Connection)
        Dim HorasCol As New Collection
        Dim HoraE As HEx
        DR = HE.ConsultaReaderCxml(pIdNomina)
        While DR.Read
            HoraE.IdDetalle = DR("iddetalle")
            HoraE.Dias = DR("dias")
            HoraE.HorasExtra = DR("horasextra")
            HoraE.TipoHoras = DR("tipohoras")
            HoraE.ImportePagado = DR("importepagado")
            HorasCol.Add(HoraE)
        End While
        DR.Close()

        Dim HayJubilacionUnaEx As Boolean = False
        Dim HayJubilacionParcial As Boolean = False
        Dim HaySeparacion As Boolean = False
        Dim VI As New dbNominasDetalles(MySqlcon)
        'Percepciones
        DR = VI.ConsultaReader(ID, 0)
        Dim Hay As Byte = 0
        While DR.Read
            If Hay = 0 Then
                If TotalSueldos <> 0 Then CO += Format(TotalSueldos, "#0.00####") + "|"
                If TotalSeparacion <> 0 Then CO += Format(TotalSeparacion, "#0.00####") + "|"
                If TotalJubilacion <> 0 Then CO += Format(TotalJubilacion, "#0.00####") + "|"
                CO += Format(TotalGravadoP, "#0.00####") + "|" 'totalgrava
                CO += Format(TotalExentoP, "#0.00####") + "|" 'totalexen
                Hay = 1
            End If
            HayJubilacionUnaEx = False
            HayJubilacionParcial = False
            HaySeparacion = False
            'XMLDoc += "<nomina:Percepcion ImporteGravado=""" + +""" ImporteExento=""" + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("concepto"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoPercepcion=""" + +""" />"
            'por cada un
            If DR("tipocl") = "039" Then
                HayJubilacionUnaEx = True
            End If
            If DR("tipocl") = "044" Then
                HayJubilacionParcial = True
            End If
            If DR("tipocl") = "022" Or DR("tipocl") = "023" Or DR("tipocl") = "025" Then
                HaySeparacion = True
            End If
            CO += DR("tipocl") + "|" 'tipo
            CO += Trim(DR("clave")) + "|" 'clave
            CO += Trim(DR("tipode")) + "|" 'concepto
            CO += Format(DR("importegravado"), "#0.00####") + "|" 'importe grava
            CO += Format(DR("importeexento"), "#0.00####") + "|" 'importe exn
            If DR("tipocl") = "045" Then
                CO += Format(DR("valormercado"), "#0.00####") + "|"
                CO += Format(DR("precioalotorgarse"), "#0.00####") + "|"
            End If
            If DR("tipocl") = "019" Then
                For Each horita As HEx In HorasCol
                    If horita.IdDetalle = DR("iddetalle") Then
                        CO += horita.Dias.ToString + "|" 'dias
                        CO += horita.TipoHoras.Substring(0, 2) + "|" 'tipohoras
                        CO += horita.HorasExtra.ToString + "|" 'horas
                        CO += Format(horita.ImportePagado, "#0.00####") + "|" 'importe
                        'XMLDoc += "<nomina12:HorasExtra Dias=""" + horita.Dias.ToString + """ TipoHoras=""" + horita.TipoHoras + """ HorasExtra=""" + horita.HorasExtra.ToString + """ ImportePagado=""" + Format(horita.ImportePagado, "#0.00####") + """ />"
                    End If
                Next
            End If
        End While
        DR.Close()

        Dim NT As New dbNominaTRabajador(pIdNomina, Comm.Connection)
        If NT.HayHatos Then
            If HayJubilacionUnaEx Then
                CO += Format(NT.JtotalUnaExhibicion, "#0.00####") + "|"
                CO += Format(NT.Jacumulable, "#0.00####") + "|"
                CO += Format(NT.JnoAcumulable, "#0.00####") + "|"
            Else
                If HayJubilacionParcial Then
                    CO += Format(NT.JtotalParcialidad, "#0.00####") + "|"
                    CO += Format(NT.JmontoDiario, "#0.00####") + "|"
                    CO += Format(NT.Jacumulable, "#0.00####") + "|"
                    CO += Format(NT.JnoAcumulable, "#0.00####") + "|"
                End If
            End If
            If HaySeparacion Then
                CO += Format(NT.StotalPagado, "#0.00####") + "|"
                CO += NT.SanhosServicio.ToString + "|"
                CO += Format(NT.SsueldoMensual, "#0.00####") + "|"
                CO += Format(NT.Sacumulable, "#0.00####") + "|"
                CO += Format(NT.SnoAcumulable, "#0.00####") + "|"
            End If
        End If
        'Fin Percepciones

        'Deducciones
        If TotalOtrasDeducciones <> 0 Then CO += Format(TotalOtrasDeducciones, "#0.00####") + "|"
        If TotalImpuestosRetenidos <> 0 Then CO += Format(TotalImpuestosRetenidos, "#0.00####") + "|"
        DR = VI.ConsultaReader(ID, 1)
        While DR.Read
            'por cada una
            CO += DR("tipocl") + "|" 'tipo
            CO += Trim(DR("clave")) + "|" 'clave
            CO += Trim(DR("tipode")) + "|" 'concepto
            CO += Format(DR("importegravado") + DR("importeexento"), "#0.00####") + "|" 'importe grava
        End While
        DR.Close()
        'Otros Pagos

        Dim OP As New dbNominaOtrosPagos(Comm.Connection)
        DR = OP.consultaPagos(pIdNomina)
        Dim TipoOP As String
        Dim ConOp As String
        While DR.Read
            If DR("tipopago") < 4 Then
                TipoOP = Format(DR("tipopago") + 1, "000")
            Else
                TipoOP = "999"
            End If
            ConOp = DR("concepto")
            CO += TipoOP + "|"
            CO += DR("clave") + "|"
            CO += ConOp.Substring(4, ConOp.Length - 4) + "|"
            CO += Format(DR("importe"), "0.00####") + "|"
            If TipoOP = "002" Then
                CO += Format(DR("subsidio"), "0.00####") + "|"
            End If
            If TipoOP = "004" Then
                CO += Format(DR("saldoafavor"), "0.00####") + "|"
                CO += Format(DR("anhos"), "0.00####") + "|"
                CO += Format(DR("remanente"), "0.00####") + "|"
            End If
        End While
        DR.Close()


        Dim VIn As New dbNominasIncapacidades(MySqlcon)
        'incapacidades
        DR = VIn.ConsultaReader(ID)
        Hay = 0
        While DR.Read
            CO += DR("dias").ToString + "|" 'dias
            CO += Format(DR("tin") + 1, "00") + "|" 'tipo
            CO += Format(DR("descuento"), "#0.00####") + "|" 'descuento
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
    Public Function CreaXMLi33(ByVal pIdNomina As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer, pXMLINE As String, pEsEgreso As Byte) As String
        Dim en As New Encriptador
        Dim XMLDoc As String
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>"
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        XMLDoc += "<cfdi:Comprobante "
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdNomina
        LlenaDatos()
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdMoneda)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
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

        XMLDoc += "FormaPago=""99"" "

        If NoCertificado <> "" Then XMLDoc += "NoCertificado=""" + NoCertificado + """ "
        If Sucursal.RFC <> "SUL010720JN8" Then
            XMLDoc += "Certificado=""" + en.Certificado64 + """ "
        Else
            XMLDoc += "Certificado="""" "
        End If
        'xmldoc+="CondicionesDePago="""""

        XMLDoc += "SubTotal=""" + Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + """ "


        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        'If Descuento + DescuentoG2 > 0 Then
        '    If pEsEgreso = 0 Then
        If TotalExentoD + TotalGravadoD > 0 Then XMLDoc += "Descuento=""" + Format(TotalExentoD + TotalGravadoD, "#0.00####") + """ "
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
        XMLDoc += "Moneda=""MXN"" "
        'End If

        XMLDoc += "Total=""" + Format(TotalExentoP + TotalGravadoP - TotalExentoD - TotalGravadoD + totalOtrosPagos, "#0.00####") + """ "
        XMLDoc += "TipoDeComprobante=""N"" "
        XMLDoc += "MetodoPago=""PUE"" "

        If Sucursal.CP2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.CP2 + """ "
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.CP + """ "
        End If
        'Confirmacion
        'If NoConfirmacion <> "" Then XMLDoc += " Confirmacion=""" + NoConfirmacion + """"


        XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:nomina12=""http://www.sat.gob.mx/nomina12"" "
        XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/nomina12 http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina12.xsd"

        XMLDoc += """ "

        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        XMLDoc += ">"

        XMLDoc += "<cfdi:Emisor Rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """"
        XMLDoc += " RegimenFiscal=""" + Sucursal.ClaveRegimen.ToString + """"
        XMLDoc += "/>"


        XMLDoc += "<cfdi:Receptor Rfc=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Nombre=""" + Replace(Replace(Replace(Replace(Replace(Trabajador.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """"
        XMLDoc += " UsoCFDI=""P01"""
        XMLDoc += "/>"


        XMLDoc += "<cfdi:Conceptos>"

        XMLDoc += "<cfdi:Concepto "
        XMLDoc += "ClaveProdServ=""84111505"" "
        XMLDoc += "Cantidad=""1"" "
        XMLDoc += "ClaveUnidad=""ACT"" "
        'XMLDoc += "Unidad=""ACT"" "
        XMLDoc += "Descripcion=""Pago de nómina"" "
        XMLDoc += "ValorUnitario=""" + Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + """ "
        XMLDoc += "Importe=""" + Format(TotalExentoP + TotalGravadoP + totalOtrosPagos, "#0.00####") + """ "
        If TotalExentoD + TotalGravadoD > 0 Then XMLDoc += "Descuento=""" + Format(TotalExentoD + TotalGravadoD, "#0.00####") + """ "
        XMLDoc += "/>"

        XMLDoc += "</cfdi:Conceptos>"

        '-----------------Aqui lo de la nomina
        Dim HayJubilacionUnaEx As Boolean = False
        Dim HayJubilacionParcial As Boolean = False
        Dim HaySeparacion As Boolean = False
        XMLDoc += "<cfdi:Complemento>"
        XMLDoc += "<nomina12:Nomina Version=""1.2"" TipoNomina=""" + tipoNomina + """ FechaPago=""" + Replace(FechaPago, "/", "-") + """ FechaInicialPago=""" + Replace(FechaInicialPago, "/", "-") + """ FechaFinalPago=""" + Replace(FechaFinalPAgo, "/", "-") + """ NumDiasPagados=""" + Format(DiasPagados, "0") + """ "
        If TotalExentoP + TotalGravadoP <> 0 Then
            XMLDoc += "TotalPercepciones=""" + Format(TotalExentoP + TotalGravadoP, "#0.00####") + """ "
        End If
        If TotalExentoD + TotalGravadoD <> 0 Then
            XMLDoc += "TotalDeducciones=""" + Format(TotalExentoD + TotalGravadoD, "#0.00####") + """ "
        End If
        If totalOtrosPagos <> 0 Or CantidadOtrosPagos <> 0 Then
            XMLDoc += "TotalOtrosPagos=""" + Format(totalOtrosPagos, "#0.00####") + """ "
        End If
        XMLDoc += ">"
        XMLDoc += "<nomina12:Emisor "
        If Sucursal.CURP <> "" Then
            XMLDoc += "Curp=""" + Sucursal.CURP + """ "
        End If
        If Trabajador.RegistroPatronal <> "" Then
            XMLDoc += "RegistroPatronal=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.RegistroPatronal, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.RFCPatronOrigen <> "" Then
            XMLDoc += "RfcPatronOrigen=""" + Trabajador.RFCPatronOrigen + """"
        End If
        XMLDoc += ">"
        If origenRecurso <> "NA" And origenRecurso <> "" Then
            XMLDoc += "<nomina12:EntidadSNCF OrigenRecurso=""" + origenRecurso + """ "
            If origenRecurso = "IM" Then
                XMLDoc += "MontoRecursoPropio=""" + Format(montoRecurso, "0.00####") + """ "
            End If
            XMLDoc += "/>"
        End If
        XMLDoc += "</nomina12:Emisor>"
        XMLDoc += "<nomina12:Receptor Curp=""" + Trabajador.Curp + """ "
        If Trabajador.NumeroSeguroSocial <> "" Then
            XMLDoc += "NumSeguridadSocial=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.NumeroSeguroSocial, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Antiguedad <> 0 And Trabajador.RegistroPatronal <> "" Then
            XMLDoc += "FechaInicioRelLaboral=""" + Replace(Trabajador.FechaInicioLaboral, "/", "-") + """ "
            XMLDoc += "Antigüedad=""P" + CStr(Antiguedad) + "W"" "
        End If
        'XMLDoc += "TipoContrato=""01"" "
        XMLDoc += "TipoContrato=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.TipoContrato.Substring(0, 2), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.sindicalizado = 1 Then
            XMLDoc += "Sindicalizado=""Sí"" "
        End If
        If Trabajador.TipoJornada <> "No aplica" Then
            'XMLDoc += "TipoJornada=""01"" "
            XMLDoc += "TipoJornada=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.TipoJornada.Substring(0, 2), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.TipoRegimen < 12 Then
            XMLDoc += "TipoRegimen=""" + Trabajador.TipoRegimen.ToString("00") + """ "
        Else
            XMLDoc += "TipoRegimen=""99"" "
        End If
        XMLDoc += "NumEmpleado=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.NumeroEmpleado, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Trabajador.Departamento <> "" Then
            XMLDoc += "Departamento=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Departamento, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.Puesto <> "" Then
            XMLDoc += "Puesto=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Puesto, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.RiesgoPuesto < 6 Then
            XMLDoc += "RiesgoPuesto=""" + Trabajador.RiesgoPuesto.ToString + """ "
        End If
        If Trabajador.Periodicidad <> "" And Trabajador.Periodicidad <> "No aplica" Then XMLDoc += "PeriodicidadPago=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.Periodicidad.Substring(0, 2), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        'XMLDoc += "PeriodicidadPago=""04"" "
        If Trabajador.Banco > 0 Then
            XMLDoc += " Banco=""" + Format(Trabajador.Banco, "000") + """ "
            If Clabe <> "" And Clabe.Length <> 18 Then XMLDoc += "CuentaBancaria=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.CLABE, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.Banco = 0 And Trabajador.CLABE.Length = 18 Then
            If Clabe <> "" Then XMLDoc += "CuentaBancaria=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trabajador.CLABE, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        If Trabajador.SalarioBaseCotApor <> 0 Then
            XMLDoc += "SalarioBaseCotApor=""" + Format(Trabajador.SalarioBaseCotApor, "#0.00####") + """ "
        End If
        If Trabajador.SalarioDiarioIntegrado <> 0 Then
            XMLDoc += "SalarioDiarioIntegrado=""" + Format(Trabajador.SalarioDiarioIntegrado, "#0.00####") + """ "
        End If
        XMLDoc += "ClaveEntFed=""" + DaClaveEstadosMexico(Trabajador.EstadoLabora) + """ "
        XMLDoc += ">"
        Dim Subc As New dbnominasubcontratacion(Comm.Connection)
        DR = Subc.ConsultaReader(IdTrabajador)
        While DR.Read
            XMLDoc += "<nomina12:SubContratacion RfcLabora=""" + DR("rfclaboral") + """ PorcentajeTiempo=""" + DR("porcentaje").ToString + """/>"
        End While
        DR.Close()
        XMLDoc += "</nomina12:Receptor>"
        Dim HE As New dbNoominaHorasExtra(Comm.Connection)
        Dim HorasCol As New Collection
        Dim HoraE As HEx
        DR = HE.ConsultaReaderCxml(pIdNomina)
        While DR.Read
            HoraE.IdDetalle = DR("iddetalle")
            HoraE.Dias = DR("dias")
            HoraE.HorasExtra = DR("horasextra")
            HoraE.TipoHoras = DR("tipohoras")
            HoraE.ImportePagado = DR("importepagado")
            HorasCol.Add(HoraE)
        End While
        DR.Close()
        Dim VI As New dbNominasDetalles(Comm.Connection)
        'Percepciones
        DR = VI.ConsultaReader(ID, 0)
        Dim Hay As Byte = 0
        While DR.Read
            HayJubilacionUnaEx = False
            HayJubilacionParcial = False
            HaySeparacion = False
            If Hay = 0 Then
                XMLDoc += "<nomina12:Percepciones "
                If TotalSueldos <> 0 Then
                    XMLDoc += "TotalSueldos=""" + Format(TotalSueldos, "#0.00####") + """ "
                End If
                If TotalSeparacion <> 0 Then
                    XMLDoc += "TotalSeparacionIndemnizacion=""" + Format(TotalSeparacion, "#0.00####") + """ "
                End If
                If TotalJubilacion <> 0 Then
                    XMLDoc += "TotalJubilacionPensionRetiro=""" + Format(TotalJubilacion, "#0.00####") + """ "
                End If
                XMLDoc += "TotalGravado=""" + Format(TotalGravadoP, "#0.00####") + """ TotalExento=""" + Format(TotalExentoP, "#0.00####") + """>"
                Hay = 1
            End If
            If DR("tipocl") = "039" Then
                HayJubilacionUnaEx = True
            End If
            If DR("tipocl") = "044" Then
                HayJubilacionParcial = True
            End If
            If DR("tipocl") = "022" Or DR("tipocl") = "023" Or DR("tipocl") = "025" Then
                HaySeparacion = True
            End If
            XMLDoc += "<nomina12:Percepcion ImporteGravado=""" + Format(DR("importegravado"), "#0.00####") + """ ImporteExento=""" + Format(DR("importeexento"), "#0.00####") + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("tipode"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoPercepcion=""" + DR("tipocl") + """ "
            If DR("tipocl") = "045" Or DR("tipocl") = "019" Then
                XMLDoc += ">"
            Else
                XMLDoc += "/>"
            End If
            If DR("tipocl") = "045" Then
                XMLDoc += "<nomina12:AccionesOTitulos ValorMercado=""" + Format(DR("valormercado"), "#0.00####") + """ PrecioAlOtorgarse=""" + Format(DR("precioalotorgarse"), "#0.00####") + """ />"
            End If
            If DR("tipocl") = "019" Then
                For Each horita As HEx In HorasCol
                    If horita.IdDetalle = DR("iddetalle") Then
                        XMLDoc += "<nomina12:HorasExtra Dias=""" + horita.Dias.ToString + """ TipoHoras=""" + horita.TipoHoras.Substring(0, 2) + """ HorasExtra=""" + horita.HorasExtra.ToString + """ ImportePagado=""" + Format(horita.ImportePagado, "#0.00####") + """ />"
                    End If
                Next
            End If
            If DR("tipocl") = "045" Or DR("tipocl") = "019" Then
                XMLDoc += "</nomina12:Percepcion>"
            End If
        End While
        DR.Close()
        Dim NT As New dbNominaTRabajador(pIdNomina, Comm.Connection)
        If NT.HayHatos Then
            If HayJubilacionUnaEx Then
                XMLDoc += "<nomina12:JubilacionPensionRetiro TotalUnaExhibicion=""" + Format(NT.JtotalUnaExhibicion, "#0.00####") + """ IngresoAcumulable=""" + Format(NT.Jacumulable, "#0.00####") + """ IngresoNoAcumulable=""" + Format(NT.JnoAcumulable, "#0.00####") + """ />"
            Else
                If HayJubilacionParcial Then
                    XMLDoc += "<nomina12:JubilacionPensionRetiro TotalParcialidad=""" + Format(NT.JtotalParcialidad, "#0.00####") + """ MontoDiario=""" + Format(NT.JmontoDiario, "#0.00####") + """ IngresoAcumulable=""" + Format(NT.Jacumulable, "#0.00####") + """ IngresoNoAcumulable=""" + Format(NT.JnoAcumulable, "#0.00####") + """ />"
                End If
            End If
            If HaySeparacion Then
                XMLDoc += "<nomina12:SeparacionIndemnizacion TotalPagado=""" + Format(NT.StotalPagado, "#0.00####") + """ NumAñosServicio=""" + NT.SanhosServicio.ToString + """ UltimoSueldoMensOrd=""" + Format(NT.SsueldoMensual, "#0.00####") + """ IngresoAcumulable=""" + Format(NT.Sacumulable, "#0.00####") + """ IngresoNoAcumulable=""" + Format(NT.SnoAcumulable, "#0.00####") + """/>"
            End If
        End If
        If Hay = 1 Then
            XMLDoc += "</nomina12:Percepciones>"
        End If
        'Deducciones
        DR = VI.ConsultaReader(ID, 1)
        Hay = 0
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina12:Deducciones "
                If TotalOtrasDeducciones <> 0 Then
                    XMLDoc += " TotalOtrasDeducciones=""" + Format(TotalOtrasDeducciones, "#0.00####") + """ "
                End If
                If TotalImpuestosRetenidos <> 0 Then
                    XMLDoc += " TotalImpuestosRetenidos=""" + Format(TotalImpuestosRetenidos, "#0.00####") + """ "
                End If
                XMLDoc += ">"
                Hay = 1
            End If
            XMLDoc += "<nomina12:Deduccion Importe=""" + Format(DR("importegravado") + DR("importeexento"), "#0.00####") + """ Concepto=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("tipode"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Clave=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("clave"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TipoDeduccion=""" + DR("tipocl") + """ />"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina12:Deducciones>"
        End If

        Dim OP As New dbNominaOtrosPagos(Comm.Connection)
        DR = OP.consultaPagos(pIdNomina)
        Hay = 0
        Dim TipoOP As String
        Dim ConOP As String
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina12:OtrosPagos>"
                Hay = 1
            End If
            If DR("tipopago") < 4 Then
                TipoOP = Format(DR("tipopago") + 1, "000")
            Else
                TipoOP = "999"
            End If
            ConOP = DR("concepto")
            XMLDoc += "<nomina12:OtroPago  TipoOtroPago=""" + TipoOP + """ Clave=""" + DR("clave") + """ Concepto=""" + ConOP.Substring(4, ConOP.Length - 4) + """ Importe=""" + Format(DR("importe"), "0.00####") + """>"
            If TipoOP = "002" Then
                XMLDoc += "<nomina12:SubsidioAlEmpleo SubsidioCausado=""" + Format(DR("subsidio"), "0.00####") + """/>"
            End If
            If TipoOP = "004" Then
                XMLDoc += "<nomina12:CompensacionSaldosAFavor SaldoaFavor=""" + Format(DR("saldoafavor"), "0.00####") + """ Año=""" + Format(DR("anhos"), "0.00####") + """ RemanenteSalFav=""" + Format(DR("remanente"), "0.00####") + """ />"
            End If
            XMLDoc += "</nomina12:OtroPago>"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina12:OtrosPagos>"
        End If
        Dim VIn As New dbNominasIncapacidades(MySqlcon)
        'Incapacidades
        DR = VIn.ConsultaReader(ID)
        Hay = 0
        While DR.Read
            If Hay = 0 Then
                XMLDoc += "<nomina12:Incapacidades>"
                Hay = 1
            End If
            XMLDoc += "<nomina12:Incapacidad ImporteMonetario=""" + Format(DR("descuento"), "#0.00####") + """ TipoIncapacidad=""" + Format(DR("tin") + 1, "00") + """ DiasIncapacidad=""" + DR("dias").ToString + """ />"
        End While
        DR.Close()
        If Hay = 1 Then
            XMLDoc += "</nomina12:Incapacidades>"
        End If

        XMLDoc += "</nomina12:Nomina>"
        XMLDoc += "</cfdi:Complemento>"


        XMLDoc += "</cfdi:Comprobante>"
        Return XMLDoc

    End Function
End Class
