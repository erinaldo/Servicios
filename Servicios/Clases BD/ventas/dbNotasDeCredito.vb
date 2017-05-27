Public Class dbNotasDeCredito
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdCliente As Integer
    Public Fecha As String
    Public Cliente As dbClientes
    Public Folio As Integer
    Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    Public Estado As Byte
    Public Desglosar As Byte
    Public IdSucursal As Integer
    Public Serie As String
    Public Subtotal As Double
    Public TotalIva As Double
    Public TotalIvaRetenida As Double
    Public TotalISR As Double
    Public TotalNota As Double
    Public TipodeCambio As Double
    Public NoAprobacion As String
    Public YearAprobacion As String
    Public NoCertificado As String
    Public EsElectronica As Byte
    Public IdMoneda As Integer
    Public Aplicado As Double
    Public HoraCancelado As String
    Public FechaCancelado As String
    Public MensajeError As String
    Public uuid As String
    Public FechaTimbrado As String
    Public SelloCFD As String
    Public NoCertificadoSAT As String
    Public SelloSAT As String
    Public IdConcepto As Integer
    Public ComentarioNC As String
    Public IvaRetenido As Double
    Public ISR As Double
    Public Enum TiposFactura As Byte
        Enproceso = 0
        Facturado = 1
        Cancelado = 2
    End Enum
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdCliente = -1
        Fecha = ""
        Hora = ""
        Folio = 0
        Desglosar = 0
        Iva = 0
        TotalaPagar = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        Serie = ""
        NoAprobacion = ""
        YearAprobacion = ""
        NoCertificado = ""
        EsElectronica = 0
        TipodeCambio = 0
        IdMoneda = 0
        Aplicado = 0
        HoraCancelado = 0
        FechaCancelado = 0
        ComentarioNC = ""
        IvaRetenido = 0
        ISR = 0
        Comm.Connection = Conexion
        IdConcepto = 0
        Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblnotasdecredito where idnota=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdCliente = DReader("idcliente")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            Desglosar = DReader("desglosar")
            Iva = DReader("iva")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            Serie = DReader("serie")
            TipodeCambio = DReader("tipodecambio")
            NoAprobacion = DReader("noaprobacion")
            YearAprobacion = DReader("yearaprobacion")
            NoCertificado = DReader("nocertificado")
            EsElectronica = DReader("eselectronica")
            IdMoneda = DReader("idmoneda")
            Aplicado = DReader("aplicado")
            FechaCancelado = DReader("fechacancelado")
            HoraCancelado = DReader("horacancelado")
            IdConcepto = DReader("idconcepto")
            ComentarioNC = DReader("comentarionc")
            IvaRetenido = DReader("ivaretenido")
            ISR = DReader("isr")
        End If
        DReader.Close()
        Cliente = New dbClientes(IdCliente, Comm.Connection)
    End Sub
    'Public Function ExisteFolio(ByVal pfolio As Integer, Optional ByVal idventa As Integer = -1) As Boolean
    '    Folio = pfolio
    '    Comm.CommandText = "select count(folio) from tblventas where folio=" + Folio.ToString + If(idventa = -1, "", " and idventa<>" + CStr(idventa))
    '    If Comm.ExecuteScalar = 0 Then Return False Else Return True
    'End Function

    Public Sub Guardar(ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pidSucursal As Integer, ByVal pSerie As String, ByVal pTipoDeCambio As Double, ByVal pNoAprobacion As String, ByVal pYearAprobacion As String, ByVal pNoCertificado As String, ByVal pEselectronica As Byte, ByVal pIdMoneda As Integer, ByVal pIdConcepto As Integer, pISR As Double, pIvaRet As Double)
        IdCliente = pIdCliente
        Fecha = pFecha
        Folio = pFolio
        Desglosar = pDesglosar
        Iva = pIva
        IdSucursal = pidSucursal
        Serie = pSerie
        TipodeCambio = pTipoDeCambio
        NoAprobacion = pNoAprobacion
        NoCertificado = pNoCertificado
        YearAprobacion = pYearAprobacion
        EsElectronica = pEselectronica
        IdMoneda = pIdMoneda
        IdConcepto = pIdConcepto
        IvaRetenido = pIvaRet
        ISR = pISR
        Comm.CommandText = "insert into tblnotasdecredito(idcliente,fecha,folio,total,hora,estado,iva,totalapagar,desglosar,idsucursal,serie,tipodecambio,noaprobacion,yearaprobacion,nocertificado,eselectronica,idmoneda,aplicado,fechacancelado,horacancelado,idconcepto,comentarionc,ivaretenido,isr,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + IdCliente.ToString + ",'" + Fecha + "'," + Folio.ToString + ",0,'" + Format(TimeOfDay, "HH:mm:ss") + "'," + CStr(Estados.SinGuardar) + "," + Iva.ToString + ",0," + Desglosar.ToString + "," + IdSucursal.ToString + ",'" + Replace(Serie, "'", "''") + "'," + TipodeCambio.ToString + ",'" + Replace(NoAprobacion, "'", "''") + "','" + Replace(YearAprobacion, "'", "''") + "','" + Replace(NoCertificado, "'", "''") + "'," + EsElectronica.ToString + "," + IdMoneda.ToString + ",0,'',''," + IdConcepto.ToString + ",''," + IvaRetenido.ToString + "," + ISR.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idnota) from tblnotasdecredito"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As String, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pEstado As Byte, ByVal pTotal As Double, ByVal pTotalaPagar As Double, ByVal pIdCliente As Integer, ByVal pSerie As String, ByVal pTipodeCambio As Double, ByVal pNoAprobacion As String, ByVal pYearAprobacion As String, ByVal pNoCertificado As String, ByVal pÌdMoneda As Integer, ByVal pIdConcepto As Integer, ByVal pEselectronica As Byte, ByVal pComentario As String, pIvaRet As Double, pISR As Double)
        ID = pID
        Fecha = pFecha
        Folio = pFolio
        Desglosar = pDesglosar
        Iva = pIva
        Estado = pEstado
        Total = pTotal
        TotalaPagar = pTotalaPagar
        IdCliente = pIdCliente
        Serie = pSerie
        TipodeCambio = pTipodeCambio
        NoAprobacion = pNoAprobacion
        NoCertificado = pNoCertificado
        YearAprobacion = pYearAprobacion
        IdMoneda = pÌdMoneda
        Estado = pEstado
        EsElectronica = pEselectronica
        IdConcepto = pIdConcepto
        ComentarioNC = pComentario
        IvaRetenido = pIvaRet
        ISR = pISR
        Comm.CommandText = "update tblnotasdecredito set fecha='" + Fecha + "',folio=" + Folio.ToString + ",desglosar=" + Desglosar.ToString + ",iva=" + Iva.ToString + ",estado=" + Estado.ToString + ",total=" + Total.ToString + ",totalapagar=" + TotalaPagar.ToString + ",idcliente=" + pIdCliente.ToString + ",serie='" + Replace(Serie, "'", "''") + "',tipodecambio=" + TipodeCambio.ToString + ",noaprobacion='" + Replace(NoAprobacion, "'", "''") + "',yearaprobacion='" + Replace(YearAprobacion, "'", "''") + "',nocertificado='" + Replace(NoCertificado, "'", "''") + "',idmoneda=" + IdMoneda.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',idconcepto=" + IdConcepto.ToString + ",eselectronica=" + EsElectronica.ToString + ",comentarionc='" + Replace(ComentarioNC, "'", "''") + "',ivaretenido=" + IvaRetenido.ToString + ",isr=" + ISR.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idnota=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaComentario(ByVal pidnota As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblnotasdecredito set comentarionc='" + Replace(pTexto, "'", "''") + "' where idnota=" + pidnota.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblnotasdecredito where idnota=" + pID.ToString
        Comm.ExecuteNonQuery()

    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, Optional ByVal pNombreClave As String = "", Optional ByVal pFolio As String = "", Optional ByVal pEstado As Byte = Estados.Inicio, Optional ByVal pSinAplicar As Boolean = False) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblnotasdecredito.idnota,tblnotasdecredito.fecha,tblnotasdecredito.serie,tblnotasdecredito.folio,tblclientes.clave,tblclientes.nombre as Cliente,tblnotasdecredito.totalapagar,tblnotasdecredito.aplicado,case tblnotasdecredito.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado  from tblnotasdecredito inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblclientes.clave,tblclientes.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblnotasdecredito.serie,convert(tblnotasdecredito.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado <> Estados.Inicio Then
            Comm.CommandText += " and tblnotasdecredito.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblnotasdecredito.estado<>1"
        End If
        If pSinAplicar Then
            Comm.CommandText += " and tblnotasdecredito.totalapagar-tblnotasdecredito.aplicado>0"
        End If
        Comm.CommandText += " order by tblnotasdecredito.fecha desc,tblnotasdecredito.serie,tblnotasdecredito.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblnotasdecredito")
        Return DS.Tables("tblnotasdecredito").DefaultView
    End Function

    Public Function ConsultaxCliente(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdCliente As Integer, ByVal pPorFecha As Boolean, ByVal pFolio As String, ByVal pOrdenDecendente As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblnotasdecredito.idnota,tblnotasdecredito.fecha,tblnotasdecredito.serie,tblnotasdecredito.folio,tblclientes.clave,tblclientes.nombre as Cliente,tblnotasdecredito.totalapagar,tblnotasdecredito.aplicado,case tblnotasdecredito.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado  from tblnotasdecredito inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente where tblnotasdecredito.estado<>1"
        If pPorFecha Then
            Comm.CommandText += " fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblnotasdecredito.serie,convert(tblnotasdecredito.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If IdCliente <> 0 Then
            Comm.CommandText += " and tblnotasdecredito.idcliente=" + pIdCliente.ToString
        End If
        If pOrdenDecendente Then
            Comm.CommandText += " order by tblnotasdecredito.serie desc,tblnotasdecredito.folio desc,tblnotasdecredito.fecha desc"
        Else
            Comm.CommandText += " order by tblnotasdecredito.serie,tblnotasdecredito.folio,tblnotasdecredito.fecha"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblnotasdecredito")
        Return DS.Tables("tblnotasdecredito").DefaultView
    End Function

    'Public Function ConsultaDeudas(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidCliente As Integer, Optional ByVal pFolio As String = "") As DataView
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select tblventas.idventa,tblventas.fecha,tblventas.folio,tblventas.total,tblventas.totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "' and tblventas.facturado=1 and tblventas.credito=1 and tblventas.idcliente=" + pidCliente.ToString
    '    If pFolio <> "" Then
    '        Comm.CommandText += " and tblventas.folio like '%" + Replace(pFolio, "'", "''") + "%'"
    '    End If
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblventas")
    '    Return DS.Tables("tblventas").DefaultView
    'End Function

    Public Function DaTotal(ByVal pidNota As Integer, ByVal pidMoneda As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0
        'Dim Encontro As Double
        Dim iIva As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double
        Dim iIsr As Double
        Dim iIvaRetenido As Double
        Dim iDesglosado As Double
        Subtotal = 0
        TotalIva = 0
        TotalNota = 0
        TotalISR = 0
        TotalIvaRetenida = 0
        Comm.CommandText = "select tipodecambio from tblnotasdecredito where idnota=" + pidNota.ToString
        iTipoCambio = Comm.ExecuteScalar
        Comm.CommandText = "select isr from tblnotasdecredito where idnota=" + pidNota.ToString
        iIsr = Comm.ExecuteScalar
        Comm.CommandText = "select ivaretenido from tblnotasdecredito where idnota=" + pidNota.ToString
        iIvaRetenido = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tblnotasdecreditodetalles where idnota=" + pidNota.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblnotasdecreditodetalles where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select iva from tblnotasdecreditodetalles where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblnotasdecreditodetalles where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            If pidMoneda = 2 Then
                If pidMoneda <> IdMonedaC Then
                    Precio = Precio * iTipoCambio
                End If
            Else
                If IdMonedaC = 2 Then
                    Precio = Precio / iTipoCambio
                End If
            End If
            
            Subtotal += Precio
            iDesglosado = Precio / (1 + ((iIva - iIvaRetenido - iIsr) / 100))
            'TotalIva += Precio - (Precio / (1 + (iIva / 100)))
            TotalIva += iDesglosado * iIva / 100
            TotalIvaRetenida += iDesglosado * iIvaRetenido / 100
            TotalISR += iDesglosado * iIsr / 100
            Cont += 1
        End While
        'TotalISR = Subtotal * (iIsr / 100)
        'TotalIvaRetenida = Subtotal * (iIvaRetenido / 100)
        TotalNota = Subtotal '- TotalISR - TotalIvaRetenida
        'Subtotal = Total - TotalIva
        Return TotalNota
    End Function
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblnotasdecredito where serie='" + pSerie + "' and (estado=3 or estado=4) ),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As String, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblnotasdecredito where folio='" + pFolio + "' and serie='" + Replace(pSerie, "'", "''") + "' and estado<>1 and estado<>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    'Public ReadOnly Property totalLetra(ByVal idmoneda As Integer) As String
    '    Get
    '        Dim f As New StringFunctions
    '        Return f.PASELETRAS(DaTotal(ID, idmoneda), idmoneda)
    '    End Get
    'End Property
    Public Function CreaCadenaOriginal22(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|2.2|"
        ID = pIdVenta
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, IdMoneda)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        CO += Serie + "|"
        CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        CO += NoAprobacion + "|"
        CO += YearAprobacion + "|"
        CO += "egreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        CO += Format(Subtotal - TotalIva, "#0.00") + "|"
        CO += "0|" 'descuento

        CO += Format(TotalNota, "#0.00") + "|" ' total factura con iva

        'metododepago
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        ' CO += Trim(FP.Nombre) + "|"
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """"
        'Else
        CO += "No identificado|"
        'End If
        'lugar de expedicion
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            CO += Trim(Sucursal.Ciudad2) + "," + Trim(Sucursal.Estado2) + "|"
        Else
            CO += Trim(Sucursal.Ciudad) + "," + Trim(Sucursal.Estado) + "|"
        End If
        'If NoCuenta <> "" And FP.Tipo = dbFormasdePago.Tipos.Contado Then CO += Trim(NoCuenta) + "|"
        'Tipo de cambio

        If IdMoneda <> 2 Then
            CO += Format(TipodeCambio, "#0.00") + "|"
            Dim Moneda As New dbMonedas(IdMoneda, Comm.Connection)
            CO += Moneda.Abreviatura + "|"
        Else
            CO += "MXN|"
        End If

        'Aqui lo de parcialidades
        'proximamente

        CO += Trim(Sucursal.RFC) + "|"
        CO += Trim(Sucursal.NombreFiscal) + "|"
        CO += Trim(Sucursal.Direccion) + "|"
        CO += Trim(Sucursal.NoExterior) + "|"
        CO += Trim(Sucursal.NoInterior) + "|"
        CO += Trim(Sucursal.Colonia) + "|"
        CO += Trim(Sucursal.Ciudad) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio) + "|"
        CO += Trim(Sucursal.Municipio) + "|"
        CO += Trim(Sucursal.Estado) + "|"
        CO += Trim(Sucursal.Pais) + "|"
        CO += Trim(Sucursal.CP) + "|"

        CO += Trim(Sucursal.Direccion2) + "|"
        CO += Trim(Sucursal.NoExterior2) + "|"
        CO += Trim(Sucursal.NoInterior2) + "|"
        CO += Trim(Sucursal.Colonia2) + "|"
        CO += Trim(Sucursal.Ciudad2) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio2) + "|"
        CO += Trim(Sucursal.Municipio2) + "|"
        CO += Trim(Sucursal.Estado2) + "|"
        CO += Trim(Sucursal.Pais2) + "|"
        CO += Trim(Sucursal.CP2) + "|"


        'Regimen fiscal

        Dim Pos As Integer = 0
        Dim Listo As Boolean = False
        Dim AddDir As String = ""

        While Sucursal.RegimenFiscal.Length > Pos
            If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
                CO += AddDir + "|"
                AddDir = ""
                Listo = True
            Else
                AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
                Listo = False
            End If
            Pos += 1
        End While

        If Listo = False Then CO += AddDir + "|"

        CO += Trim(Cliente.RFC) + "|"
        CO += Trim(Cliente.Nombre) + "|"
        If Cliente.DireccionFiscal = 0 Then
            CO += Trim(Cliente.Direccion) + "|"
            CO += Trim(Cliente.NoExterior) + "|"
            CO += Trim(Cliente.NoInterior) + "|"
            CO += Trim(Cliente.Colonia) + "|"
            CO += Trim(Cliente.Ciudad) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio) + "|"
            CO += Trim(Cliente.Municipio) + "|"
            CO += Trim(Cliente.Estado) + "|"
            CO += Trim(Cliente.Pais) + "|"
            CO += Trim(Cliente.CP) + "|"
        Else
            CO += Trim(Cliente.Direccion2) + "|"
            CO += Trim(Cliente.NoExterior2) + "|"
            CO += Trim(Cliente.NoInterior2) + "|"
            CO += Trim(Cliente.Colonia2) + "|"
            CO += Trim(Cliente.Ciudad2) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio2) + "|"
            CO += Trim(Cliente.Municipio2) + "|"
            CO += Trim(Cliente.Estado2) + "|"
            CO += Trim(Cliente.Pais2) + "|"
            CO += Trim(Cliente.CP2) + "|"
        End If

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        'Dim PrecioTemp As Double
        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                CO += DR("cantidad").ToString + "|"
                CO += "No aplica|"
                'CO += DR("clave") + "|"
                CO += Trim(DR("descripcion")) + "|"
                'If DR("idmoneda") <> 2 Then
                'CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
                'CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
                'Else
                CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
                CO += Format(DR("precio"), "#0.00") + "|"
                'End If
                'informacion aduanera proximamente
            End If
        End While
        DR.Close()



        'If ISR <> 0 Then
        '    CO += "ISR|" + Format(TotalISR, "#0.00") + "|"
        'End If
        'If IvaRetenido <> 0 Then
        '    CO += "IVA|" + Format(TotalIvaRetenido, "#0.00") + "|"
        'End If
        'If ISR <> 0 Or IvaRetenido <> 0 Then
        '    CO += Format(TotalISR + TotalIvaRetenido, "#0.00") + "|"
        'End If
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                'If DR("idmoneda") <> 2 Then
                '  IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                'Else
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                'End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                'If DR("idmoneda") <> 2 Then
                'IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                'Else
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
            End If
            'End If
        End While
        DR.Close()
        For Each I As Double In Ivas
            CO += "IVA|"
            CO += Format(I, "#0.00") + "|"
            CO += Format(IvasImporte(I.ToString), "#0.00") + "|"
        Next

        CO += Format(TotalIva, "#0.00") + "|"
        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        Return CO

    End Function


    Public Function CreaCadenaOriginal(ByVal pIdNota As Integer, ByVal pIdMoneda As Integer) As String
        Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|2.0|"
        ID = pIdNota
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, 2)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        CO += Serie + "|"
        CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        CO += NoAprobacion + "|"
        CO += YearAprobacion + "|"
        CO += "egreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        CO += Format(Subtotal - TotalIva, "#0.00") + "|"
        CO += "0|" 'descuento

        CO += Format(TotalNota, "#0.00") + "|" ' total factura con iva

        CO += Trim(Sucursal.RFC) + "|"
        CO += Trim(Sucursal.NombreFiscal) + "|"
        CO += Trim(Sucursal.Direccion) + "|"
        CO += Trim(Sucursal.NoExterior) + "|"
        CO += Trim(Sucursal.NoInterior) + "|"
        CO += Trim(Sucursal.Colonia) + "|"
        CO += Trim(Sucursal.Ciudad) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio) + "|"
        CO += Trim(Sucursal.Municipio) + "|"
        CO += Trim(Sucursal.Estado) + "|"
        CO += Trim(Sucursal.Pais) + "|"
        CO += Trim(Sucursal.CP) + "|"

        CO += Trim(Sucursal.Direccion2) + "|"
        CO += Trim(Sucursal.NoExterior2) + "|"
        CO += Trim(Sucursal.NoInterior2) + "|"
        CO += Trim(Sucursal.Colonia2) + "|"
        CO += Trim(Sucursal.Ciudad2) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio2) + "|"
        CO += Trim(Sucursal.Municipio2) + "|"
        CO += Trim(Sucursal.Estado2) + "|"
        CO += Trim(Sucursal.Pais2) + "|"
        CO += Trim(Sucursal.CP2) + "|"

        CO += Trim(Cliente.RFC) + "|"
        CO += Trim(Cliente.Nombre) + "|"
        If Cliente.DireccionFiscal = 0 Then
            CO += Trim(Cliente.Direccion) + "|"
            CO += Trim(Cliente.NoExterior) + "|"
            CO += Trim(Cliente.NoInterior) + "|"
            CO += Trim(Cliente.Colonia) + "|"
            CO += Trim(Cliente.Ciudad) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio) + "|"
            CO += Trim(Cliente.Municipio) + "|"
            CO += Trim(Cliente.Estado) + "|"
            CO += Trim(Cliente.Pais) + "|"
            CO += Trim(Cliente.CP) + "|"
        Else
            CO += Trim(Cliente.Direccion2) + "|"
            CO += Trim(Cliente.NoExterior2) + "|"
            CO += Trim(Cliente.NoInterior2) + "|"
            CO += Trim(Cliente.Colonia2) + "|"
            CO += Trim(Cliente.Ciudad2) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio2) + "|"
            CO += Trim(Cliente.Municipio2) + "|"
            CO += Trim(Cliente.Estado2) + "|"
            CO += Trim(Cliente.Pais2) + "|"
            CO += Trim(Cliente.CP2) + "|"
        End If

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        'Dim PrecioTemp As Double
        Dim PU As Double
        Dim PT As Double
        While DR.Read
            CO += DR("cantidad").ToString + "|"
            'CO += DR("tipocantidad") + "|"
            'CO += DR("clave") + "|"
            CO += Trim(DR("descripcion")) + "|"

            If DR("idmoneda") <> 2 Then

                PU = DR("precio") / DR("cantidad") * TipodeCambio
                PT = DR("precio") * TipodeCambio
                CO += Format((PU / (1 + DR("iva") / 100)), "#0.00") + "|"
                CO += Format((PT / (1 + DR("iva") / 100)), "#0.00") + "|"
            Else
                PU = DR("precio") / DR("cantidad")
                PT = DR("precio")
                CO += Format((PU / (1 + DR("iva") / 100)), "#0.00") + "|"
                CO += Format((PT / (1 + DR("iva") / 100)), "#0.00") + "|"
            End If

        End While
        DR.Close()


        'If ISR <> 0 Then
        '    CO += "ISR|" + Format(TotalISR, "#0.00") + "|"
        'End If
        'If IvaRetenido <> 0 Then
        '    CO += "IVA|" + Format(TotalIvaRetenido, "#0.00") + "|"
        'End If
        'If ISR <> 0 Or IvaRetenido <> 0 Then
        '    CO += Format(TotalISR + TotalIvaRetenido, "#0.00") + "|"
        'End If
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
                Else
                    PT = DR("precio")
                    IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
                Else
                    PT = DR("precio")
                    IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
                End If
            End If
        End While
        DR.Close()
        For Each I As Double In Ivas
            CO += "IVA|"
            CO += CInt(I).ToString + "|"
            CO += Format(IvasImporte(I.ToString), "#0.00") + "|"
        Next

        CO += Format(TotalIva, "#0.00") + "|"
        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        Return CO

    End Function
    Public Function CreaCadenaOriginali32(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|3.2|"
        ID = pIdVenta
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, IdMoneda)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        'CO += Serie + "|"
        'CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        'CO += NoAprobacion + "|"
        'CO += YearAprobacion + "|"
        CO += "egreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        CO += Format(Subtotal - TotalIva + TotalISR + TotalIvaRetenida, "#0.00####") + "|"
        CO += "0|" 'descuento
        'Tipo de cambio
        If IdMoneda <> 2 Then
            CO += Format(TipodeCambio, "#0.00####") + "|"
            Dim Moneda As New dbMonedas(IdMoneda, Comm.Connection)
            CO += Moneda.Abreviatura + "|"
        Else
            CO += "MXN|"
        End If
        CO += Format(TotalNota, "#0.00####") + "|" ' total factura con iva

        'metododepago
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        'CO += Trim(FP.Nombre) + "|"
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """"
        'Else
        If Fecha < "2016/06/01" Then
            CO += "No identificado|"
        Else
            CO += "99|"
        End If
        'End If
        'lugar de expedicion
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            CO += Trim(Sucursal.Ciudad2) + "," + Trim(Sucursal.Estado2) + "|"
        Else
            CO += Trim(Sucursal.Ciudad) + "," + Trim(Sucursal.Estado) + "|"
        End If
        'If NoCuenta <> "" And FP.Tipo = dbFormasdePago.Tipos.Contado Then CO += Trim(NoCuenta) + "|"




        'Aqui lo de parcialidades
        'proximamente

        CO += Trim(Sucursal.RFC) + "|"
        CO += Trim(Sucursal.NombreFiscal) + "|"
        CO += Trim(Sucursal.Direccion) + "|"
        CO += Trim(Sucursal.NoExterior) + "|"
        CO += Trim(Sucursal.NoInterior) + "|"
        CO += Trim(Sucursal.Colonia) + "|"
        CO += Trim(Sucursal.Ciudad) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio) + "|"
        CO += Trim(Sucursal.Municipio) + "|"
        CO += Trim(Sucursal.Estado) + "|"
        CO += Trim(Sucursal.Pais) + "|"
        CO += Trim(Sucursal.CP) + "|"

        CO += Trim(Sucursal.Direccion2) + "|"
        CO += Trim(Sucursal.NoExterior2) + "|"
        CO += Trim(Sucursal.NoInterior2) + "|"
        CO += Trim(Sucursal.Colonia2) + "|"
        CO += Trim(Sucursal.Ciudad2) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio2) + "|"
        CO += Trim(Sucursal.Municipio2) + "|"
        CO += Trim(Sucursal.Estado2) + "|"
        CO += Trim(Sucursal.Pais2) + "|"
        CO += Trim(Sucursal.CP2) + "|"

        'regimen

        Dim Pos As Integer = 0
        Dim Listo As Boolean = False
        Dim AddDir As String = ""

        While Sucursal.RegimenFiscal.Length > Pos
            If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
                CO += Trim(AddDir) + "|"
                AddDir = ""
                Listo = True
            Else
                AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
                Listo = False
            End If
            Pos += 1
        End While

        If Listo = False Then CO += Trim(AddDir) + "|"

        CO += Trim(Cliente.RFC) + "|"
        CO += Trim(Cliente.Nombre) + "|"
        If Cliente.DireccionFiscal = 0 Then
            CO += Trim(Cliente.Direccion) + "|"
            CO += Trim(Cliente.NoExterior) + "|"
            CO += Trim(Cliente.NoInterior) + "|"
            CO += Trim(Cliente.Colonia) + "|"
            CO += Trim(Cliente.Ciudad) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio) + "|"
            CO += Trim(Cliente.Municipio) + "|"
            CO += Trim(Cliente.Estado) + "|"
            CO += Trim(Cliente.Pais) + "|"
            CO += Trim(Cliente.CP) + "|"
        Else
            CO += Trim(Cliente.Direccion2) + "|"
            CO += Trim(Cliente.NoExterior2) + "|"
            CO += Trim(Cliente.NoInterior2) + "|"
            CO += Trim(Cliente.Colonia2) + "|"
            CO += Trim(Cliente.Ciudad2) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio2) + "|"
            CO += Trim(Cliente.Municipio2) + "|"
            CO += Trim(Cliente.Estado2) + "|"
            CO += Trim(Cliente.Pais2) + "|"
            CO += Trim(Cliente.CP2) + "|"
        End If

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        'Dim PrecioTemp As Double
        Dim PU As Double
        Dim PT As Double
        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                CO += DR("cantidad").ToString + "|"
                CO += "No aplica|"
                'CO += DR("clave") + "|"
                CO += Trim(DR("descripcion")) + "|"
                'If DR("idmoneda") <> 2 Then
                '    CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
                '    CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
                'Else
                PU = DR("precio") / DR("cantidad")
                PT = DR("precio")
                CO += Format((PU / (1 + (DR("iva") - ISR - IvaRetenido) / 100)), "#0.00####") + "|"
                CO += Format((PT / (1 + (DR("iva") - ISR - IvaRetenido) / 100)), "#0.00####") + "|"
                'End If
            End If
        End While
        DR.Close()

        If ISR <> 0 Then
            CO += "ISR|" + Format(TotalISR, "#0.00####") + "|"
        End If
        If IvaRetenido <> 0 Then
            CO += "IVA|" + Format(TotalIvaRetenida, "#0.00####") + "|"
        End If
        If ISR <> 0 Or IvaRetenido <> 0 Then
            CO += Format(TotalISR + TotalIvaRetenida, "#0.00####") + "|"
        End If
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = DaIvas(ID)
        Dim IAnt As Double
        Dim Desgloce As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If

            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    Desgloce = (PT / (1 + (DR("iva") - ISR - IvaRetenido) / 100))
                    IvasImporte.Add(Desgloce * DR("iva") / 100, DR("iva").ToString)
                Else
                    PT = DR("precio")
                    Desgloce = (PT / (1 + (DR("iva") - ISR - IvaRetenido) / 100))
                    IvasImporte.Add(Desgloce * DR("iva") / 100, DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    Desgloce = (PT / (1 + (DR("iva") - ISR - IvaRetenido) / 100))
                    IvasImporte.Add(IAnt + (Desgloce * DR("iva") / 100), DR("iva").ToString)
                Else
                    PT = DR("precio")
                    Desgloce = (PT / (1 + (DR("iva") - ISR - IvaRetenido) / 100))
                    IvasImporte.Add(IAnt + (Desgloce * DR("iva") / 100), DR("iva").ToString)
                End If
            End If
        End While
        DR.Close()
        For Each I As Double In Ivas
            CO += "IVA|"
            CO += Format(I, "#0.00####") + "|"
            CO += Format(IvasImporte(I.ToString), "#0.00####") + "|"
        Next

        CO += Format(TotalIva, "#0.00####") + "|"
        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        Return CO

    End Function

    Public Function CreaCadenaOriginali(ByVal pIdNota As Integer, ByVal pIdMoneda As Integer) As String
        Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|3.0|"
        ID = pIdNota
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, 2)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        'CO += Serie + "|"
        'CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        'CO += NoAprobacion + "|"
        'CO += YearAprobacion + "|"
        CO += "egreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        CO += Format(Subtotal - TotalIva, "#0.00") + "|"
        CO += "0|" 'descuento

        CO += Format(TotalNota, "#0.00") + "|" ' total factura con iva

        CO += Trim(Sucursal.RFC) + "|"
        CO += Trim(Sucursal.NombreFiscal) + "|"
        CO += Trim(Sucursal.Direccion) + "|"
        CO += Trim(Sucursal.NoExterior) + "|"
        CO += Trim(Sucursal.NoInterior) + "|"
        CO += Trim(Sucursal.Colonia) + "|"
        CO += Trim(Sucursal.Ciudad) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio) + "|"
        CO += Trim(Sucursal.Municipio) + "|"
        CO += Trim(Sucursal.Estado) + "|"
        CO += Trim(Sucursal.Pais) + "|"
        CO += Trim(Sucursal.CP) + "|"

        CO += Trim(Sucursal.Direccion2) + "|"
        CO += Trim(Sucursal.NoExterior2) + "|"
        CO += Trim(Sucursal.NoInterior2) + "|"
        CO += Trim(Sucursal.Colonia2) + "|"
        CO += Trim(Sucursal.Ciudad2) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio2) + "|"
        CO += Trim(Sucursal.Municipio2) + "|"
        CO += Trim(Sucursal.Estado2) + "|"
        CO += Trim(Sucursal.Pais2) + "|"
        CO += Trim(Sucursal.CP2) + "|"

        CO += Trim(Cliente.RFC) + "|"
        CO += Trim(Cliente.Nombre) + "|"
        If Cliente.DireccionFiscal = 0 Then
            CO += Trim(Cliente.Direccion) + "|"
            CO += Trim(Cliente.NoExterior) + "|"
            CO += Trim(Cliente.NoInterior) + "|"
            CO += Trim(Cliente.Colonia) + "|"
            CO += Trim(Cliente.Ciudad) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio) + "|"
            CO += Trim(Cliente.Municipio) + "|"
            CO += Trim(Cliente.Estado) + "|"
            CO += Trim(Cliente.Pais) + "|"
            CO += Trim(Cliente.CP) + "|"
        Else
            CO += Trim(Cliente.Direccion2) + "|"
            CO += Trim(Cliente.NoExterior2) + "|"
            CO += Trim(Cliente.NoInterior2) + "|"
            CO += Trim(Cliente.Colonia2) + "|"
            CO += Trim(Cliente.Ciudad2) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio2) + "|"
            CO += Trim(Cliente.Municipio2) + "|"
            CO += Trim(Cliente.Estado2) + "|"
            CO += Trim(Cliente.Pais2) + "|"
            CO += Trim(Cliente.CP2) + "|"
        End If

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        'Dim PrecioTemp As Double
        Dim PU As Double
        Dim PT As Double
        While DR.Read
            CO += DR("cantidad").ToString + "|"
            'CO += DR("tipocantidad") + "|"
            'CO += DR("clave") + "|"
            CO += Replace(Trim(DR("descripcion")), vbCrLf, "") + "|"

            If DR("idmoneda") <> 2 Then

                PU = DR("precio") / DR("cantidad") * TipodeCambio
                PT = DR("precio") * TipodeCambio
                CO += Format((PU / (1 + DR("iva") / 100)), "#0.00") + "|"
                CO += Format((PT / (1 + DR("iva") / 100)), "#0.00") + "|"
            Else
                PU = DR("precio") / DR("cantidad")
                PT = DR("precio")
                CO += Format((PU / (1 + DR("iva") / 100)), "#0.00") + "|"
                CO += Format((PT / (1 + DR("iva") / 100)), "#0.00") + "|"
            End If

        End While
        DR.Close()


        'If ISR <> 0 Then
        '    CO += "ISR|" + Format(TotalISR, "#0.00") + "|"
        'End If
        'If IvaRetenido <> 0 Then
        '    CO += "IVA|" + Format(TotalIvaRetenido, "#0.00") + "|"
        'End If
        'If ISR <> 0 Or IvaRetenido <> 0 Then
        '    CO += Format(TotalISR + TotalIvaRetenido, "#0.00") + "|"
        'End If
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
                Else
                    PT = DR("precio")
                    IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
                Else
                    PT = DR("precio")
                    IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
                End If
            End If
        End While
        DR.Close()
        For Each I As Double In Ivas
            CO += "IVA|"
            CO += CInt(I).ToString + "|"
            CO += Format(IvasImporte(I.ToString), "#0.00") + "|"
        Next

        CO += Format(TotalIva, "#0.00") + "|"
        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        Return CO

    End Function

    Public Function CreaXML22(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

        XMLDoc += "<Comprobante " + vbCrLf
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdMoneda)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        XMLDoc += "version=""2.2""" + vbCrLf
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
        If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf

        XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf

        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf
        XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf


        XMLDoc += "subTotal=""" + Format(Subtotal - TotalIva, "#0.00") + """" + vbCrLf
        XMLDoc += "descuento=""" + "0" + """" + vbCrLf
        'Nuevo
        If IdMoneda <> 2 Then
            XMLDoc += "TipoCambio=""" + Format(TipodeCambio, "#0.00") + """" + vbCrLf
            Dim Moneda As New dbMonedas(IdMoneda, Comm.Connection)
            XMLDoc += "Moneda=""" + Moneda.Abreviatura + """" + vbCrLf
        Else
            XMLDoc += "Moneda=""MXN""" + vbCrLf
        End If
        XMLDoc += "total=""" + Format(TotalNota, "#0.00") + """" + vbCrLf



        XMLDoc += "tipoDeComprobante=""egreso""" + vbCrLf
        'Nuevo
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        'XMLDoc += "metodoDePago=""" + FP.Nombre + """" + vbCrLf
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """" + vbCrLf
        'Else
        XMLDoc += "metodoDePago=""No identificado""" + vbCrLf
        'End If
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad2 + "," + Sucursal.Estado2 + """" + vbCrLf
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad + "," + Sucursal.Estado + """" + vbCrLf
        End If
        'Proximamente
        'XMLDoc += "FolioFiscalOrig="
        'XMLDoc += "FechaFolioFiscalOrig="
        'XMLDoc += "MontoFolioFiscalOrig="

        '-------------------------
        XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv22.xsd""" + vbCrLf
        XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf
        XMLDoc += ">"

        XMLDoc += "<Emisor rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf

        XMLDoc += "<DomicilioFiscal " + vbCrLf
        If Sucursal.Direccion <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "/>" + vbCrLf
        If Sucursal.Pais2 <> "" Then
            XMLDoc += "<ExpedidoEn  " + vbCrLf
            If Sucursal.Direccion2 <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            XMLDoc += "/>" + vbCrLf
        End If

        'Aqui va el regimen fiscal
        Dim Pos As Integer = 0
        Dim Listo As Boolean = False
        Dim AddDir As String = ""

        While Sucursal.RegimenFiscal.Length > Pos
            If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
                XMLDoc += "<RegimenFiscal Regimen=""" + AddDir + """ />"
                AddDir = ""
                Listo = True
            Else
                AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
                Listo = False
            End If
            Pos += 1
        End While

        If Listo = False Then XMLDoc += "<RegimenFiscal Regimen=""" + AddDir + """ /> "


        XMLDoc += "</Emisor>" + vbCrLf


        XMLDoc += "<Receptor rfc=""" + Replace(Replace(Replace(Replace(Replace(Cliente.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf
        XMLDoc += "<Domicilio "
        If Cliente.DireccionFiscal = 0 Then
            If Cliente.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        Else
            If Cliente.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        End If
        XMLDoc += "/>" + vbCrLf

        XMLDoc += "</Receptor>" + vbCrLf

        XMLDoc += "<Conceptos>" + vbCrLf

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)

        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                XMLDoc += "<Concepto " + vbCrLf
                XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
                'XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
                XMLDoc += "unidad=""No aplica""" + vbCrLf
                'noIdentificacion por si se llega a usar
                Dim Des As String
                Des = Trim(DR("descripcion"))
                While Des.IndexOf("  ") <> -1
                    Des = Replace(Des, "  ", " ")
                End While
                Des = Replace(Des, vbTab, "")
                XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trim(Des), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
                'If DR("idmoneda") <> 2 Then
                '    XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
                '    XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
                '    XMLDoc += "/> " + vbCrLf
                'Else
                XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
                XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
                XMLDoc += "/> " + vbCrLf

                'Informacion aduanera
                '<InformacionAduanera numero=string fecha=aaaa-mm-dd aduana=string />
                'Cuenta Predia
                '<CuentaPredial numero=string />
                'Complemento concepto
                '<ComplementoConcepto ??? />
                'Parte
                '< />
                'End If
            End If
        End While
        DR.Close()

        XMLDoc += "</Conceptos>"

        XMLDoc += "<Impuestos totalImpuestosTrasladados=""" + Format(TotalIva, "#0.00") + """ "
        'If ISR <> 0 Or IvaRetenido <> 0 Then
        '    XMLDoc += "totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00") + """"
        'End If
        XMLDoc += ">" + vbCrLf

        'If ISR <> 0 Or IvaRetenido <> 0 Then
        '    XMLDoc += "<Retenciones>" + vbCrLf
        '    If ISR <> 0 Then
        '        XMLDoc += "<Retencion impuesto=""ISR""" + vbCrLf
        '        'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(TotalISR, "#0.00") + """ />" + vbCrLf
        '    End If

        '    If ISR <> 0 Then
        '        XMLDoc += "<Retencion impuesto=""IVA""" + vbCrLf
        '        'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00") + """ />" + vbCrLf
        '    End If

        '    XMLDoc += "</Retenciones>" + vbCrLf

        'End If



        XMLDoc += "<Traslados>" + vbCrLf


        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                'Else
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                'End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                'If DR("idmoneda") <> 2 Then
                'IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                'Else
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                'End If

            End If
        End While
        DR.Close()
        For Each I As Double In Ivas

            XMLDoc += "<Traslado impuesto=""IVA""" + vbCrLf
            XMLDoc += "tasa=""" + Format(I, "#0.00") + """" + vbCrLf
            XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00") + """ />" + vbCrLf

        Next



        XMLDoc += "</Traslados>" + vbCrLf





        XMLDoc += "</Impuestos>" + vbCrLf
        XMLDoc += "</Comprobante>"


        Return XMLDoc

    End Function

    Public Function CreaXML(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As String) As String
        Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

        XMLDoc += "<Comprobante " + vbCrLf
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, 2)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "version = ""2.0""" + vbCrLf
        XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf

        XMLDoc += "subTotal=""" + Format(Subtotal - TotalIva, "#0.00") + """" + vbCrLf

        XMLDoc += "total=""" + Format(TotalNota, "#0.00") + """" + vbCrLf
        If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf
        XMLDoc += "descuento=""" + "0" + """" + vbCrLf
        XMLDoc += "tipoDeComprobante=""egreso""" + vbCrLf
        XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf
        XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
        XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf

        XMLDoc += ">"

        XMLDoc += "<Emisor rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf

        XMLDoc += "<DomicilioFiscal " + vbCrLf
        If Sucursal.Direccion <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "/>" + vbCrLf
        If Sucursal.Pais2 <> "" Then
            XMLDoc += "<ExpedidoEn  " + vbCrLf


            If Sucursal.Direccion2 <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf

            'If O._CalleLocal <> "" Then XMLDoc += "calle=""" + O._CalleLocal + """" + vbCrLf
            'If O._noExteriorLocal <> "" Then XMLDoc += "noExterior=""" + O._noExteriorLocal + """" + vbCrLf
            'If O._noInteriorLocal <> "" Then XMLDoc += "noInterior=""" + O._noInteriorLocal + """" + vbCrLf
            'If O._ColoniaLocal <> "" Then XMLDoc += "colonia=""" + O._ColoniaLocal + """" + vbCrLf
            'If O._LocalidadLocal <> "" Then XMLDoc += "localidad=""" + O._LocalidadLocal + """" + vbCrLf
            'If O._ReferenciaDomicilioLocal <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilioLocal + """" + vbCrLf
            'If O._MunicipioLocal <> "" Then XMLDoc += "municipio=""" + O._MunicipioLocal + """" + vbCrLf
            'If O._EstadoLocal <> "" Then XMLDoc += "estado=""" + O._EstadoLocal + """" + vbCrLf
            'If O._PaisLocal <> "" Then XMLDoc += "pais=""" + O._PaisLocal + """" + vbCrLf
            'If O._CodigoPostalLocal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostalLocal + """"
            XMLDoc += "/>" + vbCrLf
        End If

        XMLDoc += "</Emisor>" + vbCrLf


        XMLDoc += "<Receptor rfc=""" + Replace(Replace(Replace(Replace(Replace(Cliente.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf
        XMLDoc += "<Domicilio "
        If Cliente.DireccionFiscal = 0 Then
            If Cliente.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        Else
            If Cliente.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        End If
        XMLDoc += "/>" + vbCrLf

        XMLDoc += "</Receptor>" + vbCrLf

        XMLDoc += "<Conceptos>" + vbCrLf

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        Dim PU As Double
        Dim PT As Double
        While DR.Read
            XMLDoc += "<Concepto " + vbCrLf
            XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
            'XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("descripcion"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If DR("idmoneda") <> 2 Then
                PU = DR("precio") / DR("cantidad") * TipodeCambio
                PT = DR("precio") * TipodeCambio
                XMLDoc += "valorUnitario=""" + Format((PU) / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
                XMLDoc += "importe=""" + Format(PT / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
                XMLDoc += "/> " + vbCrLf
            Else
                PU = DR("precio") / DR("cantidad")
                PT = DR("precio")
                XMLDoc += "valorUnitario=""" + Format(PU / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
                XMLDoc += "importe=""" + Format(PT / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
                XMLDoc += "/> " + vbCrLf
            End If

        End While
        DR.Close()

        'Dim VP As New dbVentasProductos(MySqlcon)
        'DR = VP.ConsultaReader(ID)

        'While DR.Read

        '    XMLDoc += "<Concepto " + vbCrLf
        '    XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
        '    XMLDoc += "unidad=""" + DR("tipocantidad") + """" + vbCrLf
        '    XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
        '    If DR("idmoneda") <> 2 Then

        '        XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    Else
        '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    End If
        '    XMLDoc += "/> " + vbCrLf
        'End While
        'DR.Close()

        'Dim VS As New dbVentasServicios(MySqlcon)
        'DR = VS.ConsultaReader(ID)

        'While DR.Read

        '    XMLDoc += "<Concepto " + vbCrLf
        '    XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
        '    XMLDoc += "unidad=""SERV""" + vbCrLf
        '    XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
        '    If DR("idmoneda") <> 2 Then
        '        XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    Else
        '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    End If
        '    XMLDoc += "/> " + vbCrLf
        'End While
        'DR.Close()
        XMLDoc += "</Conceptos>"

        XMLDoc += "<Impuestos totalImpuestosTrasladados=""" + Format(TotalIva, "#0.00") + """ "
        'If ISR <> 0 Or IvaRetenido <> 0 Then
        '    XMLDoc += "totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00") + """"
        'End If
        XMLDoc += ">" + vbCrLf

        'If ISR <> 0 Or IvaRetenido <> 0 Then
        '    XMLDoc += "<Retenciones>" + vbCrLf
        '    If ISR <> 0 Then
        '        XMLDoc += "<Retencion impuesto=""ISR""" + vbCrLf
        '        'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(TotalISR, "#0.00") + """ />" + vbCrLf
        '    End If

        '    If ISR <> 0 Then
        '        XMLDoc += "<Retencion impuesto=""IVA""" + vbCrLf
        '        'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00") + """ />" + vbCrLf
        '    End If

        '    XMLDoc += "</Retenciones>" + vbCrLf

        'End If



        XMLDoc += "<Traslados>" + vbCrLf


        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
                Else
                    PT = DR("precio")
                    IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
                Else
                    PT = DR("precio")
                    IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
                End If

            End If
        End While
        DR.Close()
        For Each I As Double In Ivas

            XMLDoc += "<Traslado impuesto=""IVA""" + vbCrLf
            XMLDoc += "tasa=""" + I.ToString + """" + vbCrLf
            XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00") + """ />" + vbCrLf

        Next
        XMLDoc += "</Traslados>" + vbCrLf
        XMLDoc += "</Impuestos>" + vbCrLf
        XMLDoc += "</Comprobante>"


        Return XMLDoc

    End Function
    Public Function CreaXMLi32(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>"

        XMLDoc += "<cfdi:Comprobante "
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdMoneda)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        XMLDoc += "version=""3.2"" "
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        XMLDoc += "folio=""" + Folio.ToString + """ "
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """ "
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """ "

        XMLDoc += "formaDePago=""Pago en una sola exhibición"" "

        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """ "
        XMLDoc += "certificado=""" + en.Certificado64 + """ "

        XMLDoc += "subTotal=""" + Format(Subtotal - TotalIva + TotalISR + TotalIvaRetenida, "#0.00####") + """ "


        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf

        XMLDoc += "descuento=""" + "0" + """ "

        'Tipo deCambio nuevo
        If IdMoneda <> 2 Then
            XMLDoc += "TipoCambio=""" + Format(TipodeCambio, "#0.00####") + """ "
            Dim Moneda As New dbMonedas(IdMoneda, Comm.Connection)
            XMLDoc += "Moneda=""" + Moneda.Abreviatura + """ "
        Else
            XMLDoc += "Moneda=""MXN"" "
        End If
        XMLDoc += "total=""" + Format(TotalNota, "#0.00####") + """ "

        XMLDoc += "tipoDeComprobante=""egreso"" "
        'Metodo de pago lugar exibibicion nuevo
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        'XMLDoc += "metodoDePago=""" + FP.Nombre + """" + vbCrLf
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """" + vbCrLf
        'Else
        If Fecha < "2016/06/01" Then
            XMLDoc += "metodoDePago=""No identificado"" "
        Else
            XMLDoc += "metodoDePago=""99"" "
        End If
        'End If
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad2 + "," + Sucursal.Estado2 + """ "
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad + "," + Sucursal.Estado + """ "
        End If

        'XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:implocal=""http://www.sat.gob.mx/implocal"" "
        'XMLDoc += "xmlns:terceros=""http://www.sat.gob.mx/terceros"" xmlns:detallista=""http://www.sat.gob.mx/detallista"" xmlns:psgecfd=""http://www.sat.gob.mx/psgecfd"" xmlns:ecc=""http://www.sat.gob.mx/ecc"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital"" "
        'XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/TimbreFiscalDigital/TimbreFiscalDigital.xsd http://www.sat.gob.mx/detallista " + _
        '"http://www.sat.gob.mx/sitio_internet/cfd/detallista/detallista.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd " + _
        '"http://www.sat.gob.mx/ecc http://www.sat.gob.mx/sitio_internet/cfd/ecc/ecc.xsd http://www.sat.gob.mx/psgecfd http://www.sat.gob.mx/sitio_internet/cfd/psgecfd/psgecfd.xsd " + _
        '"http://www.sat.gob.mx/terceros http://www.sat.gob.mx/sitio_internet/cfd/terceros/terceros11.xsd http://www.sat.gob.mx/donat http://www.sat.gob.mx/sitio_internet/cfd/donat/donat11.xsd"""

        XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" "
        XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd"
        XMLDoc += """ "

        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        XMLDoc += ">"

        XMLDoc += "<cfdi:Emisor rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>"

        XMLDoc += "<cfdi:DomicilioFiscal "
        If Sucursal.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        XMLDoc += "/>"
        If Sucursal.Pais2 <> "" Then
            XMLDoc += "<cfdi:ExpedidoEn  "


            If Sucursal.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "

            'If O._CalleLocal <> "" Then XMLDoc += "calle=""" + O._CalleLocal + """" + vbCrLf
            'If O._noExteriorLocal <> "" Then XMLDoc += "noExterior=""" + O._noExteriorLocal + """" + vbCrLf
            'If O._noInteriorLocal <> "" Then XMLDoc += "noInterior=""" + O._noInteriorLocal + """" + vbCrLf
            'If O._ColoniaLocal <> "" Then XMLDoc += "colonia=""" + O._ColoniaLocal + """" + vbCrLf
            'If O._LocalidadLocal <> "" Then XMLDoc += "localidad=""" + O._LocalidadLocal + """" + vbCrLf
            'If O._ReferenciaDomicilioLocal <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilioLocal + """" + vbCrLf
            'If O._MunicipioLocal <> "" Then XMLDoc += "municipio=""" + O._MunicipioLocal + """" + vbCrLf
            'If O._EstadoLocal <> "" Then XMLDoc += "estado=""" + O._EstadoLocal + """" + vbCrLf
            'If O._PaisLocal <> "" Then XMLDoc += "pais=""" + O._PaisLocal + """" + vbCrLf
            'If O._CodigoPostalLocal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostalLocal + """"
            XMLDoc += "/>"
        End If

        Dim Pos As Integer = 0
        Dim Listo As Boolean = False
        Dim AddDir As String = ""

        While Sucursal.RegimenFiscal.Length > Pos
            If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
                XMLDoc += "<cfdi:RegimenFiscal Regimen=""" + AddDir + """/>"
                AddDir = ""
                Listo = True
            Else
                AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
                Listo = False
            End If
            Pos += 1
        End While

        If Listo = False Then XMLDoc += "<cfdi:RegimenFiscal Regimen=""" + AddDir + """/>"



        XMLDoc += "</cfdi:Emisor>"


        XMLDoc += "<cfdi:Receptor rfc=""" + Replace(Replace(Replace(Replace(Replace(Cliente.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>"
        XMLDoc += "<cfdi:Domicilio "
        If Cliente.DireccionFiscal = 0 Then
            If Cliente.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        Else
            If Cliente.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """"
        End If
        XMLDoc += "/>"

        XMLDoc += "</cfdi:Receptor>"

        XMLDoc += "<cfdi:Conceptos>"

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        Dim PU As Double
        Dim PT As Double
        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                XMLDoc += "<cfdi:Concepto "
                XMLDoc += "cantidad=""" + DR("cantidad").ToString + """ "
                'XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
                XMLDoc += "unidad=""No aplica"" "
                Dim Des As String
                Des = Trim(DR("descripcion"))
                While Des.IndexOf("  ") <> -1
                    Des = Replace(Des, "  ", " ")
                End While
                Des = Replace(Des, vbTab, "")
                XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(Des, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
                'If DR("idmoneda") <> 2 Then
                '    XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
                '    XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
                '    XMLDoc += "/> " + vbCrLf
                'Else
                PU = DR("precio") / DR("cantidad")
                PT = DR("precio")
                XMLDoc += "valorUnitario=""" + Format(PU / (1 + (DR("iva") - ISR - IvaRetenido) / 100), "#0.00####") + """ "
                XMLDoc += "importe=""" + Format(PT / (1 + (DR("iva") - ISR - IvaRetenido) / 100), "#0.00####") + """"
                XMLDoc += "/>"

                'End If
            End If
        End While
        DR.Close()

        XMLDoc += "</cfdi:Conceptos>"

        XMLDoc += "<cfdi:Impuestos totalImpuestosTrasladados=""" + Format(TotalIva, "#0.00####") + """"
        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += " totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenida, "#0.00####") + """"
        End If
        XMLDoc += ">"

        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "<cfdi:Retenciones>" + vbCrLf
            If ISR <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""ISR""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalISR, "#0.00####") + """ />" + vbCrLf
            End If

            If IvaRetenido <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""IVA""" + vbCrLf
                'XMLDoc += "tasa=""" + IvaRetenido.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalIvaRetenida, "#0.00####") + """ />" + vbCrLf
            End If

            XMLDoc += "</cfdi:Retenciones>" + vbCrLf

        End If



        XMLDoc += "<cfdi:Traslados>"


        DR = DaIvas(ID)
        Dim IAnt As Double
        Dim Desgloce As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    Desgloce = PT / (1 + (DR("iva") - ISR - IvaRetenido) / 100)
                    IvasImporte.Add(Desgloce * DR("iva") / 100, DR("iva").ToString)
                Else
                    PT = DR("precio")
                    Desgloce = PT / (1 + (DR("iva") - ISR - IvaRetenido) / 100)
                    IvasImporte.Add(Desgloce * DR("iva") / 100, DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    Desgloce = PT / (1 + (DR("iva") - ISR - IvaRetenido) / 100)
                    IvasImporte.Add(IAnt + (Desgloce * DR("iva") / 100), DR("iva").ToString)
                Else
                    PT = DR("precio")
                    Desgloce = PT / (1 + (DR("iva") - ISR - IvaRetenido) / 100)
                    IvasImporte.Add(IAnt + (Desgloce * DR("iva") / 100), DR("iva").ToString)
                End If

            End If
        End While
        DR.Close()
        For Each I As Double In Ivas

            XMLDoc += "<cfdi:Traslado impuesto=""IVA"" "
            XMLDoc += "tasa=""" + Format(I, "#0.00####") + """ "
            XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00####") + """/>"

        Next

        XMLDoc += "</cfdi:Traslados>"
        XMLDoc += "</cfdi:Impuestos>"
        XMLDoc += "</cfdi:Comprobante>"


        Return XMLDoc

    End Function
    Public Function CreaXMLi(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
        Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

        'XMLDoc += "<Comprobante " + vbCrLf

        'en.Leex509(My.Settings.rutacer)
        'ID = pIdVenta
        'LlenaDatos()
        'If TipodeCambio = 0 Then TipodeCambio = 1
        'DaTotal(ID, 2)
        'Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        'XMLDoc += "version = ""2.0""" + vbCrLf
        'XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
        'XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
        'If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
        'If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf

        'XMLDoc += "subTotal=""" + Format(Subtotal - TotalIva, "#0.00") + """" + vbCrLf

        'XMLDoc += "total=""" + Format(TotalNota, "#0.00") + """" + vbCrLf
        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        'XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf
        'XMLDoc += "descuento=""" + "0" + """" + vbCrLf
        'XMLDoc += "tipoDeComprobante=""egreso""" + vbCrLf
        'XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf
        'XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
        'XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        'XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf

        'XMLDoc += ">"

        XMLDoc += "<cfdi:Comprobante " + vbCrLf
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, 2)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "version = ""3.0""" + vbCrLf
        XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf

        XMLDoc += "subTotal=""" + Format(Subtotal - TotalIva, "#0.00") + """" + vbCrLf

        XMLDoc += "total=""" + Format(TotalNota, "#0.00") + """" + vbCrLf
        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf
        XMLDoc += "descuento=""" + "0" + """" + vbCrLf
        XMLDoc += "tipoDeComprobante=""egreso""" + vbCrLf
        XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf
        'XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
        'XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        'XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf
        '++++++++++++++++++++++++++++
        XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:ecb=""http://www.sat.gob.mx/ecb"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:implocal=""http://www.sat.gob.mx/implocal"" xmlns:bfa2=""http://www.buzonfiscal.com/ns/addenda/bf/2""" + vbCrLf
        XMLDoc += "xmlns:terceros=""http://www.sat.gob.mx/terceros"" xmlns:detallista=""http://www.sat.gob.mx/detallista"" xmlns:psgecfd=""http://www.sat.gob.mx/psgecfd"" xmlns:ecc=""http://www.sat.gob.mx/ecc"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital""" + vbCrLf
        XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3  http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv3.xsd http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/TimbreFiscalDigital/TimbreFiscalDigital.xsd http://www.sat.gob.mx/detallista http://www.sat.gob.mx/sitio_internet/cfd/detallista/detallista.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd http://www.buzonfiscal.com/ns/addenda/bf/2 http://www.buzonfiscal.com/schema/xsd/Addenda_BF_v2.2.xsd http://www.sat.gob.mx/ecc http://www.sat.gob.mx/sitio_internet/cfd/ecc/ecc.xsd http://www.sat.gob.mx/psgecfd http://www.sat.gob.mx/sitio_internet/cfd/psgecfd/psgecfd.xsd "
        XMLDoc += "http://www.sat.gob.mx/ecb http://www.sat.gob.mx/sitio_internet/cfd/ecb/ecb.xsd http://www.sat.gob.mx/terceros http://www.sat.gob.mx/sitio_internet/cfd/terceros/terceros.xsd""" + vbCrLf
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        XMLDoc += ">"


        XMLDoc += "<cfdi:Emisor rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf

        XMLDoc += "<cfdi:DomicilioFiscal " + vbCrLf
        If Sucursal.Direccion <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "/>" + vbCrLf
        If Sucursal.Pais2 <> "" Then
            XMLDoc += "<cfdi:ExpedidoEn  " + vbCrLf


            If Sucursal.Direccion2 <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf

            'If O._CalleLocal <> "" Then XMLDoc += "calle=""" + O._CalleLocal + """" + vbCrLf
            'If O._noExteriorLocal <> "" Then XMLDoc += "noExterior=""" + O._noExteriorLocal + """" + vbCrLf
            'If O._noInteriorLocal <> "" Then XMLDoc += "noInterior=""" + O._noInteriorLocal + """" + vbCrLf
            'If O._ColoniaLocal <> "" Then XMLDoc += "colonia=""" + O._ColoniaLocal + """" + vbCrLf
            'If O._LocalidadLocal <> "" Then XMLDoc += "localidad=""" + O._LocalidadLocal + """" + vbCrLf
            'If O._ReferenciaDomicilioLocal <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilioLocal + """" + vbCrLf
            'If O._MunicipioLocal <> "" Then XMLDoc += "municipio=""" + O._MunicipioLocal + """" + vbCrLf
            'If O._EstadoLocal <> "" Then XMLDoc += "estado=""" + O._EstadoLocal + """" + vbCrLf
            'If O._PaisLocal <> "" Then XMLDoc += "pais=""" + O._PaisLocal + """" + vbCrLf
            'If O._CodigoPostalLocal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostalLocal + """"
            XMLDoc += "/>" + vbCrLf
        End If

        XMLDoc += "</cfdi:Emisor>" + vbCrLf


        XMLDoc += "<cfdi:Receptor rfc=""" + Replace(Replace(Replace(Replace(Replace(Cliente.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf
        XMLDoc += "<cfdi:Domicilio "
        If Cliente.DireccionFiscal = 0 Then
            If Cliente.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        Else
            If Cliente.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        End If
        XMLDoc += "/>" + vbCrLf

        XMLDoc += "</cfdi:Receptor>" + vbCrLf

        XMLDoc += "<cfdi:Conceptos>" + vbCrLf

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        Dim PU As Double
        Dim PT As Double
        While DR.Read
            XMLDoc += "<cfdi:Concepto " + vbCrLf
            XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
            'XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            Dim Des As String
            Des = Trim(DR("descripcion"))
            While Des.IndexOf("  ") <> -1
                Des = Replace(Des, "  ", " ")
            End While
            Des = Replace(Des, vbTab, "")
            XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(Des, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            'XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trim(DR("descripcion")), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If DR("idmoneda") <> 2 Then
                PU = DR("precio") / DR("cantidad") * TipodeCambio
                PT = DR("precio") * TipodeCambio
                XMLDoc += "valorUnitario=""" + Format((PU) / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
                XMLDoc += "importe=""" + Format(PT / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
                XMLDoc += "/> " + vbCrLf
            Else
                PU = DR("precio") / DR("cantidad")
                PT = DR("precio")
                XMLDoc += "valorUnitario=""" + Format(PU / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
                XMLDoc += "importe=""" + Format(PT / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
                XMLDoc += "/> " + vbCrLf
            End If

        End While
        DR.Close()

        'Dim VP As New dbVentasProductos(MySqlcon)
        'DR = VP.ConsultaReader(ID)

        'While DR.Read

        '    XMLDoc += "<Concepto " + vbCrLf
        '    XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
        '    XMLDoc += "unidad=""" + DR("tipocantidad") + """" + vbCrLf
        '    XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
        '    If DR("idmoneda") <> 2 Then

        '        XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    Else
        '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    End If
        '    XMLDoc += "/> " + vbCrLf
        'End While
        'DR.Close()

        'Dim VS As New dbVentasServicios(MySqlcon)
        'DR = VS.ConsultaReader(ID)

        'While DR.Read

        '    XMLDoc += "<Concepto " + vbCrLf
        '    XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
        '    XMLDoc += "unidad=""SERV""" + vbCrLf
        '    XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
        '    If DR("idmoneda") <> 2 Then
        '        XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    Else
        '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    End If
        '    XMLDoc += "/> " + vbCrLf
        'End While
        'DR.Close()
        XMLDoc += "</cfdi:Conceptos>"

        XMLDoc += "<cfdi:Impuestos totalImpuestosTrasladados=""" + Format(TotalIva, "#0.00") + """ "
        'If ISR <> 0 Or IvaRetenido <> 0 Then
        '    XMLDoc += "totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00") + """"
        'End If
        XMLDoc += ">" + vbCrLf

        'If ISR <> 0 Or IvaRetenido <> 0 Then
        '    XMLDoc += "<Retenciones>" + vbCrLf
        '    If ISR <> 0 Then
        '        XMLDoc += "<Retencion impuesto=""ISR""" + vbCrLf
        '        'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(TotalISR, "#0.00") + """ />" + vbCrLf
        '    End If

        '    If ISR <> 0 Then
        '        XMLDoc += "<Retencion impuesto=""IVA""" + vbCrLf
        '        'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00") + """ />" + vbCrLf
        '    End If

        '    XMLDoc += "</Retenciones>" + vbCrLf

        'End If



        XMLDoc += "<cfdi:Traslados>" + vbCrLf


        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
                Else
                    PT = DR("precio")
                    IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    PT = DR("precio") * TipodeCambio
                    IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
                Else
                    PT = DR("precio")
                    IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
                End If

            End If
        End While
        DR.Close()
        For Each I As Double In Ivas

            XMLDoc += "<cfdi:Traslado impuesto=""IVA""" + vbCrLf
            XMLDoc += "tasa=""" + I.ToString + """" + vbCrLf
            XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00") + """ />" + vbCrLf

        Next
        XMLDoc += "</cfdi:Traslados>" + vbCrLf
        XMLDoc += "</cfdi:Impuestos>" + vbCrLf
        XMLDoc += "</cfdi:Comprobante>"


        Return XMLDoc

    End Function

    Public Function DaIvas(ByVal pIdNota As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iva,precio,idmoneda from tblnotasdecreditodetalles where idnota=" + pIdNota.ToString
        Return Comm.ExecuteReader
    End Function
    Public Sub Aplicar(ByVal pId As Integer, ByVal pCantidad As Double, ByVal pSuma As Boolean)
        If pSuma Then
            Comm.CommandText = "update tblnotasdecredito set aplicado=aplicado+" + pCantidad.ToString + " where idnota=" + pId.ToString
        Else
            Comm.CommandText = "update tblnotasdecredito set aplicado=aplicado-" + pCantidad.ToString + " where idnota=" + pId.ToString
        End If
        Comm.ExecuteNonQuery()
    End Sub

    Private Function ValidarCertificadoRemoto(ByVal sender As Object, ByVal certificate As Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As Security.Cryptography.X509Certificates.X509Chain, ByVal policyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function


    'Public Function Timbrar(ByVal RFCEmisor As String, ByVal RFCCliente As String, ByVal ArchivoCer As String, ByVal CerPassword As String, ByVal strXml As String, ByVal pDireccionTimbrado As String) As TimbreFiscal.TimbreFiscalDigital
    '    Dim en As New Encriptador
    '    Dim T As TimbreFiscal.TimbreFiscalDigital
    '    Try
    '        Dim Tcfdi As New TimbreFiscal.TimbradoCFDI
    '        Dim x509 As New Security.Cryptography.X509Certificates.X509Certificate(en.LeeArchivo(ArchivoCer), CerPassword)
    '        Tcfdi.ClientCertificates.Add(x509)
    '        Tcfdi.Url = pDireccionTimbrado '"https://demotf.buzonfiscal.com/timbrado?wsdl"
    '        Dim Req As New TimbreFiscal.RequestTimbradoCFDType
    '        Req.InfoBasica = New TimbreFiscal.InfoBasicaType
    '        Req.InfoBasica.RfcEmisor = RFCEmisor
    '        Req.InfoBasica.RfcReceptor = Cliente.RFC
    '        Req.InfoBasica.Serie = Serie
    '        Req.RefID = Serie + Folio.ToString
    '        Req.Documento = New TimbreFiscal.DocumentoType
    '        Dim Encoder As New System.Text.UTF8Encoding
    '        'Dim Bytes() As Byte = Encoder.GetBytes(strXml)
    '        'Req.Documento.Archivo = Bytes
    '        Dim s As New Xml.Serialization.XmlSerializer(GetType(TimbreFiscal.Comprobante), "http://www.sat.gob.mx/cfd/3")
    '        Dim xml As String = strXml
    '        Dim c As TimbreFiscal.Comprobante = s.Deserialize(New IO.StringReader(xml))
    '        Req.Comprobante = c

    '        Dim x As New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificadoRemoto)
    '        System.Net.ServicePointManager.ServerCertificateValidationCallback = x
    '        System.Net.ServicePointManager.Expect100Continue = True

    '        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
    '        T = Tcfdi.timbradoCFD(Req)
    '        Return T
    '    Catch ex As Exception
    '        T = New TimbreFiscal.TimbreFiscalDigital
    '        T.noCertificadoSAT = "Error"
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '        Return T
    '    End Try

    'End Function
    Public Function Timbrar(ByVal RFCEmisor As String, ByVal RFCCliente As String, ByVal ArchivoCer As String, ByVal CerPassword As String, ByVal strXml As String, ByVal pDireccionTimbrado As String, ByVal ConMsgBox As Boolean) As TimbreFiscal.TimbreFiscalDigital
        Dim en As New Encriptador
        Dim T As TimbreFiscal.TimbreFiscalDigital
        Try
            Dim Tcfdi As New TimbreFiscal.TimbradoCFDI
            Dim x509 As New Security.Cryptography.X509Certificates.X509Certificate(en.LeeArchivo(ArchivoCer), CerPassword)
            Tcfdi.ClientCertificates.Add(x509)
            Tcfdi.Url = pDireccionTimbrado '"https://demotf.buzonfiscal.com/timbrado?wsdl"
            Dim Req As New TimbreFiscal.RequestTimbradoCFDType
            Req.InfoBasica = New TimbreFiscal.InfoBasicaType
            Req.InfoBasica.RfcEmisor = RFCEmisor
            Req.InfoBasica.RfcReceptor = Cliente.RFC
            Req.InfoBasica.Serie = Serie

            Req.RefID = Serie + Folio.ToString
            'Req.Documento.Tipo = TimbreFiscal.DocumentoTypeTipo.XML
            Req.Documento = New TimbreFiscal.DocumentoType
            ' Req.Documento.Version = "3.0"
            Dim Encoder As New System.Text.UTF8Encoding
            'Dim Bytes() As Byte = Encoder.GetBytes(strXml)
            'Req.Documento.Archivo = Bytes
            'Dim s As New Xml.Serialization.XmlSerializer(GetType(TimbreFiscal.Comprobante), "http://www.sat.gob.mx/cfd/3")
            'Dim xml As String = strXml
            'Dim c As TimbreFiscal.Comprobante = s.Deserialize(New IO.StringReader(xml))
            Dim Bytes() As Byte = Encoder.GetBytes(strXml)
            Req.Documento.Archivo = Bytes
            'Dim c As TimbreFiscal.Comprobante = s.Deserialize(New IO.StringReader(Convert.ToBase64String(xml)))
            'Req.Comprobante = c
            Dim x As New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificadoRemoto)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = x
            System.Net.ServicePointManager.Expect100Continue = True
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
            'Tcfdi.SoapVersion = Web.Services.Protocols.SoapProtocolVersion.Default
            T = Tcfdi.timbradoCFD(Req)
            Return T
        Catch ex As Exception
            T = New TimbreFiscal.TimbreFiscalDigital
            T.noCertificadoSAT = "Error"
            If ConMsgBox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError = ex.Message
            End If
            Return T
        End Try

    End Function
    Public Sub GuardaDatosTimbrado(ByVal pidNota As Integer, ByVal pUuid As String, ByVal pFechaTimbrado As String, ByVal pSellocfd As String, ByVal pNoCertificadoSat As String, ByVal pSelloSat As String)
        Comm.CommandText = "insert into tblnotasdecreditotimbrado(idnota,uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat) values(" + pidNota.ToString + ",'" + Replace(pUuid, "'", "''") + "','" + Replace(pFechaTimbrado, "'", "''") + "','" + Replace(pSellocfd, "'", "''") + "','" + Replace(pNoCertificadoSat, "'", "''") + "','" + Replace(pSelloSat, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub DaDatosTimbrado(ByVal pidNota As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblnotasdecreditotimbrado where idnota=" + pidNota.ToString
        DReader = Comm.ExecuteReader
        uuid = "**No Timbrado**"
        If DReader.Read Then
            uuid = DReader("uuid")
            FechaTimbrado = DReader("fechatimbrado")
            SelloCFD = DReader("sellocfd")
            NoCertificadoSAT = DReader("nocertificadosat")
            SelloSAT = DReader("sellosat")
        End If
        DReader.Close()
    End Sub

    Public Sub ModificaEstado(ByVal pidVenta As Integer, ByVal pEstado As Byte)
        Comm.CommandText = "update tblnotasdecredito set estado=" + pEstado.ToString + " where idnota=" + pidVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Timbrar2(ByVal pUsuario As String, ByVal pPassword As String, ByVal pRFC As String, ByVal pRutaXML As String, ByVal pRutaSalida As String, ByVal pConector As Byte, ByVal ConMsgBox As Boolean) As Integer
        Try
            Dim Comando As String
            If pConector = 0 Then
                Comando = Application.StartupPath + "\conector\timbrador_cliente.exe " + pUsuario + "*" + pPassword + "*" + pRFC + "*" + pRutaXML + "*" + pRutaSalida
            Else
                Comando = Application.StartupPath + "\conector2012\timbrar.exe " + pUsuario + "*" + pPassword + "*" + pRFC + "*" + pRutaSalida
            End If
            Shell(Comando, AppWinStyle.Hide, True)
            Return 1
        Catch ex As Exception
            If ConMsgBox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError = ex.Message
            End If
            NoCertificadoSAT = "Error"
            Return 0
        End Try

    End Function

    Public Function CancelarTimbrado(ByVal pRFC As String, ByVal pUUID As String) As Integer
        Try
            Dim Comando As String
            Dim StrCancelacion As String
            Dim en As New Encriptador
            IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
            If IO.File.Exists(Application.StartupPath + "\temp\cancelacion.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\cancelacion.txt")
            End If
            If pRFC = "SUL010720JN8" Then
                Comando = Application.StartupPath + "\conector2012\CLIENTE_TIMBRADOR222.exe CANCELAR*" + pRFC + "*" + pUUID + "*" + Application.StartupPath + "\temp\cancelacion.txt"
            Else
                Comando = Application.StartupPath + "\conector2012\CLIENTE_TIMBRADOR.exe CANCELAR*" + pRFC + "*" + pUUID + "*" + Application.StartupPath + "\temp\cancelacion.txt"
            End If
            Shell(Comando, AppWinStyle.Hide, True)
            StrCancelacion = en.LeeArchivoTexto(Application.StartupPath + "\temp\cancelacion.txt")
            StrCancelacion = StrCancelacion.ToUpper
            If StrCancelacion.Contains("HAS BEEN CANCELED") Then
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Return 0
        End Try
    End Function
    Public Function DaId(ByVal pFolio As Integer, ByVal pSerie As String) As Integer
        Comm.CommandText = "select ifnull((select idnota from tblnotasdecredito where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2 limit 1),0)"
        ID = Comm.ExecuteScalar
        If ID <> 0 Then LlenaDatos()
        Return ID
    End Function
    Public Function Timbrar3(ByVal pRFC As String, ByVal pXML As String, ByVal pRutaSalida As String, ByVal pAPIKEY As String, ByVal ConMsgBox As Boolean, ByVal pSerie As String, ByVal pFolio As Integer) As String
        'Try
        '    Dim Cadena As String
        '    Dim XmlTimbrado As String
        '    Cadena = pRFC + "~" + pAPIKEY + "~" + "NO" + "~" + "Factura" + "~" + pXML
        '    Dim FF As New facturafiel.server()()
        '    XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
        '    Return XmlTimbrado
        'Catch ex As Exception
        '    If ConMsgBox Then
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        '    Else
        '        MensajeError = ex.Message
        '    End If
        '    NoCertificadoSAT = "Error"
        '    Return "ERROR"
        'End Try
        Dim Cadena As String
        Dim XmlTimbrado As String = ""
        Try

            Dim Alterno As String = "0"
            Comm.CommandText = "select nombreempresalocal from tblopciones limit 1"
            Alterno = Comm.ExecuteScalar
            If Alterno = "1" Then
                Return Timbrar3alt(pRFC, pXML, pRutaSalida, pAPIKEY, ConMsgBox, pSerie, pFolio)
            End If

            Dim Pruebas As String = "NO"
            Comm.CommandText = "select codigopostal from tblopciones limit 1"
            Pruebas = Comm.ExecuteScalar
            Cadena = pRFC + "~" + pAPIKEY + "~" + Pruebas + "~" + "Factura" + "~" + pXML
            Dim FF As New facturafiel.server()
            FF.Url = "http://www.facturafiel.com/websrv/servicio_timbrado_xml.php?wsdl"
            XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
            FF.Dispose()
            'Return XmlTimbrado
        Catch ex As Exception
            'If ex.Message.Contains("Response is not well-formed XML") Then
            '    If ConMsgBox Then
            '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            '    Else
            '        MensajeError = ex.Message
            '    End If
            '    'Response is not well-formed XML.
            '    NoCertificadoSAT = "Error"
            '    XmlTimbrado = "ERROR"
            '    'Return "ERROR"
            'Else
            Dim en As New Encriptador
            IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
            If IO.File.Exists(Application.StartupPath + "\temp\ncrerror.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\ncrerror.txt")
            End If
            en.GuardaArchivoTexto(Application.StartupPath + "\temp\ncrerror.txt", ex.Message, System.Text.Encoding.Default)


            XmlTimbrado = "Recuperar"
            'End If
        End Try
        'recuperacion
        Try
            If XmlTimbrado = "Recuperar" Then
                Dim R As New facturafielrecuperacion.server()
                R.Url = "http://www.facturafiel.com/websrv/servicio_recuperacion.php?wsdl"
                If pSerie <> "" Then
                    Cadena = pRFC + "~" + pAPIKEY + "~" + pSerie + "+" + pFolio.ToString
                Else
                    Cadena = pRFC + "~" + pAPIKEY + "~+" + pFolio.ToString
                End If
                XmlTimbrado = R.servicio_recuperacion(Cadena)
                XmlTimbrado = XmlTimbrado.Substring(1, XmlTimbrado.Length - 1)
                R.Dispose()
            End If
        Catch ex As Exception
            If ConMsgBox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError = ex.Message
            End If
            XmlTimbrado = "ERROR"
            NoCertificadoSAT = "Error"
        End Try
        Return XmlTimbrado

    End Function

    Public Function Timbrar3alt(ByVal pRFC As String, ByVal pXML As String, ByVal pRutaSalida As String, ByVal pAPIKEY As String, ByVal ConMsgBox As Boolean, ByVal pSerie As String, ByVal pFolio As Integer) As String
        'Try
        '    Dim Cadena As String
        '    Dim XmlTimbrado As String
        '    Cadena = pRFC + "~" + pAPIKEY + "~" + "NO" + "~" + "Factura" + "~" + pXML
        '    Dim FF As New facturafiel.server()()
        '    XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
        '    Return XmlTimbrado
        'Catch ex As Exception
        '    If ConMsgBox Then
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        '    Else
        '        MensajeError = ex.Message
        '    End If
        '    NoCertificadoSAT = "Error"
        '    Return "ERROR"
        'End Try
        Dim Cadena As String
        Dim XmlTimbrado As String = ""
        Try
            Dim Pruebas As String = "NO"
            Comm.CommandText = "select codigopostal from tblopciones limit 1"
            Pruebas = Comm.ExecuteScalar
            Cadena = pRFC + "~" + pAPIKEY + "~" + Pruebas + "~" + "Factura" + "~" + pXML
            Dim FF As New facturafiel.server()
            FF.Url = "http://www.facturafiel.com/websrv/servicio_timbrado_xml.php?wsdl"
            XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
            FF.Dispose()
            'Return XmlTimbrado
        Catch ex As Exception
            'If ex.Message.Contains("Response is not well-formed XML") Then
            '    If ConMsgBox Then
            '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            '    Else
            '        MensajeError = ex.Message
            '    End If
            '    'Response is not well-formed XML.
            '    NoCertificadoSAT = "Error"
            '    XmlTimbrado = "ERROR"
            '    'Return "ERROR"
            'Else
            Dim en As New Encriptador
            IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
            If IO.File.Exists(Application.StartupPath + "\temp\ncrerror.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\ncrerror.txt")
            End If
            en.GuardaArchivoTexto(Application.StartupPath + "\temp\ncrerror.txt", ex.Message, System.Text.Encoding.Default)


            XmlTimbrado = "Recuperar"
            'End If
        End Try
        'recuperacion
        Try
            If XmlTimbrado = "Recuperar" Then
                Dim R As New facturafielrecuperacion.server()
                R.Url = "http://www.facturafiel.com/websrv/servicio_recuperacion.php?wsdl"
                If pSerie <> "" Then
                    Cadena = pRFC + "~" + pAPIKEY + "~" + pSerie + "+" + pFolio.ToString
                Else
                    Cadena = pRFC + "~" + pAPIKEY + "~+" + pFolio.ToString
                End If
                XmlTimbrado = R.servicio_recuperacion(Cadena)
                XmlTimbrado = XmlTimbrado.Substring(1, XmlTimbrado.Length - 1)
                R.Dispose()
            End If
        Catch ex As Exception
            If ConMsgBox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError = ex.Message
            End If
            XmlTimbrado = "ERROR"
            NoCertificadoSAT = "Error"
        End Try
        Return XmlTimbrado

    End Function

    Public Function CancelarTimbrado2(ByVal pRFC As String, ByVal pUUID As String, ByVal pAPIKey As String) As Integer
        Dim Cadena As String = ""
        Dim en As New Encriptador
        IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
        If IO.File.Exists(Application.StartupPath + "\temp\cancelacion.txt") Then
            IO.File.Delete(Application.StartupPath + "\temp\cancelacion.txt")
        End If
        Dim Cancela As New facturafielcancelacion.server
        Cancela.Url = "http://www.facturafiel.com/websrv/servicio_cancelacion.php?wsdl"
        Cadena = pRFC + "~" + pAPIKey + "~" + pUUID
        Cadena = Cancela.servicio_cancelacion(Cadena)
        en.GuardaArchivoTexto(Application.StartupPath + "\temp\cancelacion.txt", Cadena, System.Text.Encoding.Default)
        If Cadena.Contains("EXITOSAMENTE") Then
            Return 1
        Else
            'MensajeError = Cadena
            Return 0
        End If
    End Function

    Public Function Recuperar(ByVal PRFC As String, ByVal pAPIKEY As String, ByVal PSerie As String, ByVal pFolio As Integer, ByVal pConMsgBox As Boolean) As String
        Dim XMLTimbrado As String
        Dim Cadena As String
        Try
            'If XMLTimbrado = "Recuperar" Then
            Dim R As New facturafielrecuperacion.server()
            R.Url = "http://www.facturafiel.com/websrv/servicio_recuperacion.php?wsdl"
            If PSerie <> "" Then
                Cadena = PRFC + "~" + pAPIKEY + "~" + PSerie + "+" + pFolio.ToString
            Else
                Cadena = PRFC + "~" + pAPIKEY + "~+" + pFolio.ToString
            End If
            XMLTimbrado = R.servicio_recuperacion(Cadena)
            XMLTimbrado = XMLTimbrado.Substring(1, XMLTimbrado.Length - 1)
            R.Dispose()
            'End If
        Catch ex As Exception
            If pConMsgBox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError = ex.Message
            End If
            XMLTimbrado = "ERROR"
            NoCertificadoSAT = "Error"
        End Try
        Return XMLTimbrado
    End Function
End Class
