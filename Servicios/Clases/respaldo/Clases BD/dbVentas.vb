﻿
Public Class dbVentas
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdCliente As Integer
    Public Fecha As String
    Public Cliente As dbClientes
    Public Folio As Integer
    Public Desglosar As Byte
    Public Facturado As Byte
    Public Credito As Double
    Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    Public Serie As String
    Public NoCertificado As String
    Public NoAprobacion As String
    Public YearAprobacion As String
    Public EsElectronica As Byte
    Public Estado As Byte
    Public SerieCertificado As String
    Public IdSucursal As Integer
    Public IdFormadePago As Integer
    Public TipodeCambio As Double
    Public IdConversion As Integer
    Public TotalVenta As Double
    Public Subtototal As Double
    Public TotalIva As Double
    Public TotalISR As Double
    Public TotalIvaRetenido As Double
    Public ISR As Double
    Public IvaRetenido As Double
    Public IdVendedor As Integer
    Public Comentario As String
    Public uuid As String
    Public FechaTimbrado As String
    Public SelloCFD As String
    Public NoCertificadoSAT As String
    Public SelloSAT As String
    Public NoCuenta As String
    Public MensajeError As String
    Public Descuento As Double
    Public Cargo As Double
    Public IdVentaOrigen As Integer
    Public Parcialidad As Integer
    Public Parcialidades As Integer

    Public FolioOrigen As Integer
    Public SerieOrigen As String
    Public FolioUUIDOrigen As String
    Public MontoOrigen As Double
    Public FechaOrigen As String
    Public PorSurtir As Byte
    Public TotalPeso As Double
    Public TotalDescuento As Double
    Public DeRemision As Byte
    Public TotalRetLocal As Double
    Public TotalTrasLocal As Double
    Public TotalSinRetencion As Double
    Public TotalIvaRetenidoConceptos As Double
    Public TotalIEPS As Double
    ' Public ivaRetenido As Double
    Public RefDocumento As String
    Public Adicional As String
    Public TotalNegativos As Double
    Public TotalNegativosSinIVA As Double
    Public noNegativos As String
    Public Alterno As String = "0"
    Public TotalRevdev As Double
    Public DescuentoG2 As Double
    Public SobreEscribeImpLoc As Double
    Public FechaCancelado As String
    Public TotalOfertas As Double
    Private Structure Implocal
        Dim Tasa As Double
        Dim Nombre As String
        Dim Tipo As Byte
        Dim Importe As Double
        Dim Importe2 As Double
    End Structure
    Dim ILocal As Implocal
    Public ImpLocales As New Collection
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
        Facturado = 0
        Credito = 0
        Iva = 0
        TotalaPagar = 0
        Total = 0
        Serie = ""
        NoCertificado = ""
        NoAprobacion = ""
        YearAprobacion = ""
        EsElectronica = 0
        Estado = 0
        IdSucursal = 0
        IdFormadePago = 0
        TipodeCambio = 0
        IdConversion = 0
        ISR = 0
        IvaRetenido = 0
        IdVendedor = 0
        Comentario = 0
        NoCuenta = ""
        Descuento = 0
        Cargo = 0
        IdVentaOrigen = 0
        Parcialidad = 1
        Parcialidades = 1
        PorSurtir = 0
        DeRemision = 0
        RefDocumento = ""
        Adicional = ""
        DescuentoG2 = 0
        SobreEscribeImpLoc = 0
        Comm.Connection = Conexion
        Comm.CommandTimeout = 120
        Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection, ByVal NoNeg As String)
        ID = pID
        Comm.Connection = Conexion
        Comm.CommandTimeout = 120
        noNegativos = NoNeg
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandType = CommandType.Text
        Comm.CommandText = "select * from tblventas where idventa=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdCliente = DReader("idcliente")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            Desglosar = DReader("desglosar")
            Facturado = DReader("facturado")
            Credito = DReader("credito")
            Iva = DReader("iva")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Serie = DReader("serie")
            NoAprobacion = DReader("noaprobacion")
            NoCertificado = DReader("nocertificado")
            YearAprobacion = DReader("yearaprobacion")
            EsElectronica = DReader("eselectronica")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            IdFormadePago = DReader("idforma")
            TipodeCambio = DReader("tipodecambio")
            IdConversion = DReader("idconversion")
            IvaRetenido = DReader("ivaretenido")
            ISR = DReader("isr")
            IdVendedor = DReader("idvendedor")
            Comentario = DReader("comentariof")
            NoCuenta = DReader("nocuenta")
            Descuento = DReader("descuentog")
            Cargo = DReader("cargog")
            IdVentaOrigen = DReader("idventaorigen")
            Parcialidad = DReader("parcialidad")
            Parcialidades = DReader("parcialidades")
            PorSurtir = DReader("porsurtir")
            DeRemision = DReader("deremision")
            RefDocumento = DReader("refdocumento")
            Adicional = DReader("adicional")
            DescuentoG2 = DReader("descuentog2")
            SobreEscribeImpLoc = DReader("sobreimploc")
            FechaCancelado = DReader("fechacancelado")
        End If
        DReader.Close()
        Cliente = New dbClientes(IdCliente, Comm.Connection)
    End Sub
    'Public Function ExisteFolio(ByVal pfolio As Integer, Optional ByVal idventa As Integer = -1) As Boolean
    '    Folio = pfolio
    '    Comm.CommandText = "select count(folio) from tblventas where folio=" + Folio.ToString + If(idventa = -1, "", " and idventa<>" + CStr(idventa))
    '    If Comm.ExecuteScalar = 0 Then Return False Else Return True
    'End Function

    Public Sub Guardar(ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pSerie As String, ByVal pNoAprobacion As String, ByVal pNoCertificado As String, ByVal pYearAprovacion As String, ByVal pEsElectronica As Byte, ByVal pidSucursal As Integer, ByVal pIdFormaDepago As Integer, ByVal pTipodeCambio As Double, ByVal pIdConversion As Integer, ByVal pIsr As Double, ByVal pIvaretenido As Double, ByVal pIdVendedor As Integer, ByVal pDescuento As Double, pDescuento2 As Double, pSobreImpLoc As Double)
        NoAprobacion = pNoAprobacion
        NoCertificado = pNoCertificado
        YearAprobacion = pYearAprovacion
        EsElectronica = pEsElectronica
        IdCliente = pIdCliente
        Fecha = pFecha
        Folio = pFolio
        Desglosar = pDesglosar
        Iva = pIva
        Serie = pSerie
        IdSucursal = pidSucursal
        IdFormadePago = pIdFormaDepago
        TipodeCambio = pTipodeCambio
        IdConversion = pIdConversion
        IvaRetenido = pIvaretenido
        ISR = pIsr
        IdVendedor = pIdVendedor
        Descuento = pDescuento
        IdVentaOrigen = 0
        Parcialidad = 1
        Parcialidades = 1
        DescuentoG2 = pDescuento2
        SobreEscribeImpLoc = pSobreImpLoc
        Comm.CommandText = "insert into tblventas(idcliente,fecha,folio,desglosar,facturado,credito,iva,totalapagar,total,hora,serie,noaprobacion,nocertificado,yearaprobacion,eselectronica,estado,idsucursal,idforma,tipodecambio,idconversion,ivaretenido,isr,fechacancelado,horacancelado,idvendedor,comentariof,nocuenta,descuentog,cargog,idventaorigen,parcialidad,parcialidades,porsurtir,deremision,refdocumento,adicional,descuentog2,sobreimploc) values(" + IdCliente.ToString + ",'" + Fecha + "'," + Folio.ToString + "," + Desglosar.ToString + ",0,0," + Iva.ToString + ",0,0,'" + Format(TimeOfDay, "HH:mm:ss") + "','" + Serie + "','" + NoAprobacion + "','" + NoCertificado + "','" + YearAprobacion + "'," + EsElectronica.ToString + ",1," + IdSucursal.ToString + "," + IdFormadePago.ToString + "," + TipodeCambio.ToString + "," + IdConversion.ToString + "," + IvaRetenido.ToString + "," + ISR.ToString + ",'',''," + IdVendedor.ToString + ",'',''," + Descuento.ToString + ",0,0,1,1,0,0" + ",'',''," + DescuentoG2.ToString + "," + SobreEscribeImpLoc.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idventa) from tblventas"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pSerie As String, ByVal pNoAprobacion As String, ByVal pNoCertificado As String, ByVal pYearAprovacion As String, ByVal pEsElectronica As Byte, ByVal pEstado As Byte, ByVal pIdFormadePago As Integer, ByVal pCredito As Byte, ByVal pTipodeCambio As Double, ByVal pidConversion As Integer, ByVal pSubTotal As Double, ByVal pTotal As Double, ByVal pIdCliente As Integer, ByVal pIdVendedor As Integer, ByVal pComentario As String, ByVal pNoCuenta As String, ByVal pDescuento As Double, ByVal pCargo As Double, ByVal pIdVentaOrigen As Integer, ByVal pParcialidad As Integer, ByVal pParcialidades As Integer, ByVal pPorSurtir As Byte, ByVal pRefDocumento As String, ByVal pAdicional As String, pDescuentog2 As Double, pSobreImpLoc As Double)
        ID = pID
        Fecha = pFecha
        Folio = pFolio
        Desglosar = pDesglosar
        Iva = pIva
        Serie = pSerie
        NoAprobacion = pNoAprobacion
        NoCertificado = pNoCertificado
        YearAprobacion = pYearAprovacion
        EsElectronica = pEsElectronica
        Estado = pEstado
        Credito = Credito
        IdFormadePago = pIdFormadePago
        TipodeCambio = pTipodeCambio
        IdConversion = pidConversion
        IdCliente = pIdCliente
        IdVendedor = pIdVendedor
        Comentario = pComentario
        NoCuenta = pNoCuenta
        Descuento = pDescuento
        Cargo = pCargo
        IdVentaOrigen = pIdVentaOrigen
        Parcialidad = pParcialidad
        Parcialidades = pParcialidades
        PorSurtir = pPorSurtir
        RefDocumento = pRefDocumento
        Adicional = pAdicional
        DescuentoG2 = pDescuentog2
        SobreEscribeImpLoc = pSobreImpLoc
        If Estado = Estados.Guardada Then
            Comm.CommandText = "update tblventas set fecha='" + Fecha + "',folio=" + Folio.ToString + ",serie='" + Serie + "',desglosar=" + Desglosar.ToString + ",iva=" + Iva.ToString + ",noaprobacion='" + NoAprobacion + "',nocertificado='" + NoCertificado + "',yearaprobacion='" + YearAprobacion + "',eselectronica=" + EsElectronica.ToString + ",estado=" + Estado.ToString + ",idforma=" + IdFormadePago.ToString + ",credito=" + Credito.ToString + ",tipodecambio=" + TipodeCambio.ToString + ",idconversion=" + IdConversion.ToString + ",total=" + pSubTotal.ToString + ",totalapagar=" + pTotal.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',idcliente=" + IdCliente.ToString + ",idvendedor=" + IdVendedor.ToString + ",hora='" + Format(TimeOfDay, "HH:mm:ss") + "',comentariof='" + Trim(Replace(Comentario, "'", "''")) + "',nocuenta='" + Replace(Trim(NoCuenta), "'", "''") + "',descuentog=" + Descuento.ToString + ",cargog=" + Cargo.ToString + _
            ",idventaorigen=" + IdVentaOrigen.ToString + ",parcialidad=" + Parcialidad.ToString + ",parcialidades=" + Parcialidades.ToString + ",porsurtir=" + PorSurtir.ToString + ",refdocumento='" + Replace(RefDocumento, "'", "''") + "',adicional='" + Replace(Adicional, "'", "''") + "',hora='" + Format(TimeOfDay, "HH:mm:ss") + "',descuentog2=" + DescuentoG2.ToString + ",sobreimploc=" + SobreEscribeImpLoc.ToString + " where idventa=" + ID.ToString
        Else
            Comm.CommandText = "update tblventas set fecha='" + Fecha + "',folio=" + Folio.ToString + ",serie='" + Serie + "',desglosar=" + Desglosar.ToString + ",iva=" + Iva.ToString + ",noaprobacion='" + NoAprobacion + "',nocertificado='" + NoCertificado + "',yearaprobacion='" + YearAprobacion + "',eselectronica=" + EsElectronica.ToString + ",estado=" + Estado.ToString + ",idforma=" + IdFormadePago.ToString + ",credito=" + Credito.ToString + ",tipodecambio=" + TipodeCambio.ToString + ",idconversion=" + IdConversion.ToString + ",total=" + pSubTotal.ToString + ",totalapagar=" + pTotal.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',idcliente=" + IdCliente.ToString + ",idvendedor=" + IdVendedor.ToString + ",comentariof='" + Trim(Replace(Comentario, "'", "''")) + "',nocuenta='" + Replace(Trim(NoCuenta), "'", "''") + "',descuentog=" + Descuento.ToString + ",cargog=" + Cargo.ToString + _
            ",idventaorigen=" + IdVentaOrigen.ToString + ",parcialidad=" + Parcialidad.ToString + ",parcialidades=" + Parcialidades.ToString + ",porsurtir=" + PorSurtir.ToString + ",refdocumento='" + Replace(RefDocumento, "'", "''") + "',adicional='" + Replace(Adicional, "'", "''") + "',descuentog2=" + DescuentoG2.ToString + ",sobreimploc=" + SobreEscribeImpLoc.ToString + " where idventa=" + ID.ToString
        End If
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificaEstado(ByVal pidVenta As Integer, ByVal pEstado As Byte)
        Comm.CommandText = "update tblventas set estado=" + pEstado.ToString + " where idventa=" + pidVenta.ToString
        Comm.ExecuteNonQuery()
        If pEstado <> Estados.Guardada Or pEstado <> Estados.Cancelada Then
            RegresaInventario(pidVenta)
        End If
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        DesligaRemisiones(pID)
        Comm.CommandText = "delete from tblventas where idventa=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, Optional ByVal pNombreClave As String = "", Optional ByVal pFolio As String = "", Optional ByVal pEstado As Byte = 0, Optional ByVal pCredido As Byte = 200, Optional ByVal pIdVendedor As Integer = 0, Optional ByVal pSurtido As Byte = 0, Optional ByVal pIdSucursal As Integer = 0, Optional pSemillas As Byte = 0) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventas.idventa,tblventas.fecha,concat(tblventas.serie,convert(tblventas.folio using utf8)),tblclientes.clave,tblclientes.nombre as Cliente,tblventas.totalapagar,case tblventas.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblclientes.clave,tblclientes.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblventas.serie,convert(tblventas.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado <> 0 Then
            Comm.CommandText += " and tblventas.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblventas.estado<>1"
        End If
        If pCredido <> 200 Then
            Comm.CommandText += " and tblformasdepago.tipo=" + pCredido.ToString
        End If
        If pIdVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pIdVendedor.ToString
        End If
        If pSurtido <> 0 Then
            Comm.CommandText += " and tblventas.porsurtir=" + CStr(pSurtido - 1)
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pSemillas <> 0 Then
            Comm.CommandText += " and (select count(idfactura) from tblsemillasanticiposfacturas where idfactura=tblventas.idventa)=0"
        End If
        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
        Comm.CommandTimeout = 10000
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        Return DS.Tables("tblventas").DefaultView
    End Function

    Public Function ConsultaFacturasconDepositos(pIdDeposito As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventas.idventa,tblventas.fecha,concat(tblventas.serie,convert(tblventas.folio using utf8)),tblclientes.clave,tblclientes.nombre as Cliente,tblventas.totalapagar,case tblventas.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblventasdepositos on tblventasdepositos.idventa=tblventas.idventa where iddeposito=" + pIdDeposito.ToString
        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
        Comm.CommandTimeout = 10000
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Function ConsultaTimbres(ByVal pFecha As String, ByVal pFecha2 As String, Optional ByVal pNombreClave As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventas.idventa,tblventas.fecha,concat(tblventas.serie,convert(tblventas.folio using utf8)),tblclientes.nombre as Cliente,tblventas.totalapagar,tblventastimbrado.uuid from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblventastimbrado on tblventastimbrado.idventa=tblventas.idventa where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblclientes.clave,tblclientes.nombre,tblventas.serie,convert(tblventas.folio using utf8)) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Function ConsultaDeudas(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidCliente As Integer, ByVal pFolio As String, ByVal pidTipodePago As Integer, ByVal PorFechas As Boolean, ByVal Todas As Boolean, ByVal pTipodeOrden As Byte, ByVal pMostrarCanceladas As Boolean, ByVal pTipodeCambio As Double, ByVal pEnPesos As Boolean, ByVal pIdSucursal As Integer, ByVal pSoloContado As Boolean) As DataView
        Dim DS As New DataSet
        If pSoloContado = True Then pidTipodePago = 1
        Comm.CommandText = "delete from tblclientesdeudas where idcliente=" + pidCliente.ToString
        Comm.ExecuteNonQuery()

        'Facturas
        If pEnPesos Then
            Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        ",tblventas.idventa,0,tblventas.fecha,tblventas.estado,tblventas.serie,tblventas.folio,if(tblventas.idconversion=2,tblventas.credito,tblventas.credito*" + pTipodeCambio.ToString + "),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*" + pTipodeCambio.ToString + "),0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and  tblventas.idforma<>98 and tblformasdepago.tipo=" + pidTipodePago.ToString
        Else
            Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        ",tblventas.idventa,0,tblventas.fecha,tblventas.estado,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblventas.idforma<>98 and tblformasdepago.tipo=" + pidTipodePago.ToString
        End If
        If Todas = False And pSoloContado = False Then
            Comm.CommandText += " and round(tblventas.totalapagar-tblventas.credito,2)>0"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblventas.serie,convert(tblventas.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pMostrarCanceladas Then
            Comm.CommandText += " and (tblventas.estado=4 or tblventas.estado=3)"
        Else
            Comm.CommandText += " and tblventas.estado=3"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.ExecuteNonQuery()

        If pSoloContado = False Then
            If pEnPesos Then
                Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
            ",tblventas.idventa,4,tblventas.fecha,tblventas.estado,tblventas.serie,tblventas.folio,if(tblventas.idconversion=2,tblventas.credito,tblventas.credito*" + pTipodeCambio.ToString + "),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*" + pTipodeCambio.ToString + "),0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblventas.idforma=98 and tblformasdepago.tipo=" + pidTipodePago.ToString
            Else
                Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
            ",tblventas.idventa,4,tblventas.fecha,tblventas.estado,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblventas.idforma=98 and tblformasdepago.tipo=" + pidTipodePago.ToString
            End If
            'select tblventas.idventa,0 as sel,tblventas.fecha,if(tblventas.estado=3,'A','C') as estadof,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,tblventas.totalapagar-tblventas.credito as restante from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
            If Todas = False And pSoloContado = False Then
                Comm.CommandText += " and round(tblventas.totalapagar-tblventas.credito,2)>0"
            End If
            If PorFechas Then
                Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
            End If
            If pFolio <> "" Then
                Comm.CommandText += " and concat(tblventas.serie,convert(tblventas.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
            End If
            If pMostrarCanceladas Then
                Comm.CommandText += " and (tblventas.estado=4 or tblventas.estado=3)"
            Else
                Comm.CommandText += " and tblventas.estado=3"
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
            End If
            Comm.ExecuteNonQuery()

            'Notas de Cargo
            If pEnPesos Then
                Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
            ",tblnotasdecargo.idcargo,1,tblnotasdecargo.fecha,tblnotasdecargo.estado,tblnotasdecargo.serie,tblnotasdecargo.folio,if(tblnotasdecargo.idmoneda=2,tblnotasdecargo.aplicado,tblnotasdecargo.aplicado*" + pTipodeCambio.ToString + "),if(tblnotasdecargo.idmoneda=2,tblnotasdecargo.totalapagar,tblnotasdecargo.totalapagar*" + pTipodeCambio.ToString + "),0 from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.idcliente=" + pidCliente.ToString
            Else
                Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
            ",tblnotasdecargo.idcargo,1,tblnotasdecargo.fecha,tblnotasdecargo.estado,tblnotasdecargo.serie,tblnotasdecargo.folio,tblnotasdecargo.aplicado,tblnotasdecargo.totalapagar,0 from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.idcliente=" + pidCliente.ToString
            End If


            If Todas = False Then
                Comm.CommandText += " and round(tblnotasdecargo.totalapagar-tblnotasdecargo.aplicado,2)>0"
            End If
            If PorFechas Then
                Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
            End If
            If pFolio <> "" Then
                Comm.CommandText += " and concat(tblnotasdecargo.serie,convert(tblnotasdecargo.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
            End If
            If pMostrarCanceladas Then
                Comm.CommandText += " and (tblnotasdecargo.estado=4 or tblnotasdecargo.estado=3)"
            Else
                Comm.CommandText += " and tblnotasdecargo.estado=3"
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
            End If
            Comm.ExecuteNonQuery()

            'Documentos saldo inicial
            If pEnPesos Then
                Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
            ",dc.iddocumento,2,dc.fecha,dc.estado,dc.serie,dc.folio,if(dc.idmoneda=2,dc.credito,dc.credito*" + pTipodeCambio.ToString + "),if(dc.idmoneda=2,dc.totalapagar,dc.totalapagar*" + pTipodeCambio.ToString + "),0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=0"
            Else
                Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
                ",dc.iddocumento,2,dc.fecha,dc.estado,dc.serie,dc.folio,dc.credito,dc.totalapagar,0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=0"
            End If


            If Todas = False Then
                Comm.CommandText += " and round(dc.totalapagar-dc.credito,2)>0"
            End If
            If PorFechas Then
                Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
            End If
            If pFolio <> "" Then
                Comm.CommandText += " and concat(dc.serie,convert(dc.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
            End If
            If pMostrarCanceladas Then
                Comm.CommandText += " and (dc.estado=4 or dc.estado=3)"
            Else
                Comm.CommandText += " and dc.estado=3"
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and dc.idsucursal=" + pIdSucursal.ToString
            End If
            Comm.ExecuteNonQuery()

            'Documentos documentos
            If pEnPesos Then
                Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
            ",dc.iddocumento,3,dc.fecha,dc.estado,dc.seriereferencia,dc.folioreferencia,if(dc.idmoneda=2,dc.credito,dc.credito*" + pTipodeCambio.ToString + "),if(dc.idmoneda=2,dc.totalapagar,dc.totalapagar*" + pTipodeCambio.ToString + "),0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=1"
            Else
                Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
                ",dc.iddocumento,3,dc.fecha,dc.estado,dc.seriereferencia,dc.folioreferencia,dc.credito,dc.totalapagar,0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=1"
            End If


            If Todas = False Then
                Comm.CommandText += " and round(dc.totalapagar-dc.credito,2)>0"
            End If
            If PorFechas Then
                Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
            End If
            If pFolio <> "" Then
                Comm.CommandText += " and concat(dc.seriereferencia,convert(dc.folioreferencia using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
            End If
            If pMostrarCanceladas Then
                Comm.CommandText += " and (dc.estado=4 or dc.estado=3)"
            Else
                Comm.CommandText += " and dc.estado=3"
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and dc.idsucursal=" + pIdSucursal.ToString
            End If
            Comm.ExecuteNonQuery()
        End If

        Comm.CommandText = "select iddocumento as idventa,0 as sel,cant,fecha,case tipo when 0 then 'Factura' when 1 then 'Nota de Cargo' when 2 then 'S. Inicial' when 3 then 'Documento' when 4 then 'Factura Par.' end as tipodoc,if(estado=3,'A','C') as estadof,serie,folio,credito,totalapagar,totalapagar-credito as restante,tipo,totalapagar-credito as restante2 from tblclientesdeudas where idcliente=" + pidCliente.ToString
        'select tblventas.idventa,0 as sel,tblventas.fecha,if(tblventas.estado=3,'A','C') as estadof,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,tblventas.totalapagar-tblventas.credito as restante from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        If pTipodeOrden = 0 Then
            Comm.CommandText += " order by tipo,fecha,serie,folio"
        Else
            Comm.CommandText += " order by tipo,totalapagar,serie,folio"
        End If


        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesdeudas")
        Return DS.Tables("tblclientesdeudas").DefaultView
    End Function

    Public Function DaTotal(ByVal pidVenta As Integer, ByVal pidMoneda As Integer, ByVal NoNegativos As String, ByVal pAlterno As String) As Double
        If pAlterno = "0" Then
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
            Dim iCargo As Double
            Dim iDescuento As Double
            Dim iDescuento2 As Double
            Dim pIEPS As Double
            Dim pIVARetenido As Double
            Dim EsRevDEv As Byte
            Dim iIdInventario As Integer
            Dim SobreImp As Double
            'Dim iIdCliente As Integer
            'Dim iTruncar As Byte
            Subtototal = 0
            TotalIva = 0
            TotalVenta = 0
            'Comm.CommandText = "select desglosar from tblventas where idventa=" + pidVenta.ToString
            'iTruncar = Comm.ExecuteScalar
            Comm.CommandText = "select tipodecambio from tblventas where idventa=" + pidVenta.ToString
            iTipoCambio = Comm.ExecuteScalar
            'Comm.CommandText = "select idcliente from tblventas where idventa=" + pidVenta.ToString
            'iIdCliente = Comm.ExecuteScalar
            Comm.CommandText = "select isr from tblventas where idventa=" + pidVenta.ToString
            iIsr = Comm.ExecuteScalar
            Comm.CommandText = "select ivaretenido from tblventas where idventa=" + pidVenta.ToString
            iIvaRetenido = Comm.ExecuteScalar
            Comm.CommandText = "select descuentog from tblventas where idventa=" + pidVenta.ToString
            iDescuento = Comm.ExecuteScalar
            Comm.CommandText = "select descuentog2 from tblventas where idventa=" + pidVenta.ToString
            iDescuento2 = Comm.ExecuteScalar
            Comm.CommandText = "select cargog from tblventas where idventa=" + pidVenta.ToString
            iCargo = Comm.ExecuteScalar
            Comm.CommandText = "select sobreimploc from tblventas where idventa=" + pidVenta.ToString
            SobreImp = Comm.ExecuteScalar
            '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
            Comm.CommandText = "select idventasinventario from tblventasinventario where idventa=" + pidVenta.ToString
            DReader = Comm.ExecuteReader
            TotalDescuento = 0
            TotalIEPS = 0
            TotalIvaRetenidoConceptos = 0
            TotalRevdev = 0
            TotalDescuento = 0
            While DReader.Read
                IDs.Add(DReader("idventasinventario"))
            End While
            DReader.Close()
            Dim Desc As Double
            While Cont <= IDs.Count
                Comm.CommandText = "select precio from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
                Precio = Comm.ExecuteScalar

                Comm.CommandText = "select iva from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
                iIva = Comm.ExecuteScalar
                Comm.CommandText = "select idmoneda from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
                IdMonedaC = Comm.ExecuteScalar
                Comm.CommandText = "select descuento from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
                Desc = Comm.ExecuteScalar
                'IEPS e IVA Retenido
                Comm.CommandText = "select ieps from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
                pIEPS = Comm.ExecuteScalar
                Comm.CommandText = "select IVARetenido from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
                pIVARetenido = Comm.ExecuteScalar
                Comm.CommandText = "select idinventario from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
                iIdInventario = Comm.ExecuteScalar
                Comm.CommandText = "select esrevdev from tblinventario where idinventario=" + iIdInventario.ToString
                EsRevDEv = Comm.ExecuteScalar
                If iIdInventario = 1 Then
                    TotalOfertas += Precio
                End If
                If pidMoneda = 2 Then
                    If pidMoneda <> IdMonedaC Then
                        Precio = Precio * iTipoCambio
                    End If
                Else
                    If IdMonedaC = 2 Then
                        Precio = Precio / iTipoCambio
                    End If
                End If
                TotalIvaRetenidoConceptos += (Precio * (pIVARetenido / 100))
                TotalIEPS += (Precio * (pIEPS / 100))
                TotalIva += (Precio * (iIva / 100))
                If Desc <> 0 Then
                    If Desc <> 100 Then
                        TotalDescuento += (Precio / (1 - Desc / 100)) - Precio
                    Else
                        TotalDescuento += Precio
                    End If
                End If
                If EsRevDEv = 1 Then
                    TotalRevdev += Precio
                End If
                Subtototal += Precio
                ''If NoNegativos <> "1" And Precio < 0 Then
                'TotalIvaRetenidoConceptos += (Precio * (pIVARetenido / 100))
                'TotalIEPS += (Precio * (pIEPS / 100))
                'TotalIva += (Precio * (iIva / 100))
                'TotalDescuento += (Precio / (1 - Desc / 100)) - Precio
                'Subtototal += Precio
                ''Else
                'TotalIva += (Precio * (iIva / 100))
                ''End If

                Cont += 1
            End While
            'If iTruncar = 0 Then

            Comm.CommandText = "select ifnull((select sum(precio+(precio*(iva/100))) from tblventasinventario where idventa=" + pidVenta.ToString + " and precio<0),0)"
            TotalNegativos = Comm.ExecuteScalar
            Comm.CommandText = "select ifnull((select sum(precio) from tblventasinventario where idventa=" + pidVenta.ToString + " and precio<0),0)"
            TotalNegativosSinIVA = Comm.ExecuteScalar

            Dim Cimp As New dbClientesImpuestos(Comm.Connection)
            ImpLocales.Clear()
            DReader = Cimp.ConsultaReaderI(pidVenta)
            TotalRetLocal = 0
            TotalTrasLocal = 0
            While DReader.Read
                ILocal.Nombre = DReader("nombre")
                ILocal.Tasa = DReader("tasa")
                ILocal.Tipo = DReader("tipo")
                ILocal.Importe2 = DReader("importe")
                If ILocal.Tasa <> 0 Then
                    If NoNegativos = "0" Then
                        If SobreImp = 0 Then
                            ILocal.Importe = (Subtototal + TotalDescuento - TotalRevdev) * (DReader("tasa") / 100)
                        Else
                            ILocal.Importe = SobreImp * (DReader("tasa") / 100)
                        End If
                        ILocal.Importe = CDbl(Format(ILocal.Importe, "#.00"))
                    Else
                        If SobreImp = 0 Then
                            ILocal.Importe = (Subtototal + TotalDescuento - TotalNegativosSinIVA - TotalRevdev) * (DReader("tasa") / 100)
                        Else
                            ILocal.Importe = SobreImp * (DReader("tasa") / 100)
                        End If
                        ILocal.Importe = CDbl(Format(ILocal.Importe, "#.00"))
                    End If
                Else
                    ILocal.Importe = ILocal.Importe2
                End If
                If ILocal.Tipo = 0 Then
                    TotalTrasLocal += ILocal.Importe
                Else
                    TotalRetLocal += ILocal.Importe
                End If
                ImpLocales.Add(ILocal)
            End While
            DReader.Close()

            'Subtototal = Subtototal

            TotalDescuento += iDescuento
            If NoNegativos = "1" Then
                TotalDescuento = CDbl(Format(TotalDescuento, "#.00"))
                Subtototal = CDbl(Format(Subtototal, "#.00"))
                TotalIva = CDbl(Format(TotalIva, "#.00"))
            End If
            TotalISR = Subtototal * (iIsr / 100)
            TotalIvaRetenido = Subtototal * (iIvaRetenido / 100)
            TotalVenta = Subtototal + TotalIva - TotalISR - TotalIvaRetenido - iDescuento - iDescuento2 + iCargo - TotalRetLocal + TotalTrasLocal + TotalIEPS - TotalIvaRetenidoConceptos
            TotalSinRetencion = Subtototal + TotalIva - TotalISR - TotalIvaRetenido - iDescuento - iDescuento2 + iCargo - TotalIvaRetenidoConceptos
            Comm.CommandText = "select ifnull((select sum(tblinventario.peso*tblventasinventario.cantidad) from tblventasinventario inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario where tblventasinventario.idventa=" + pidVenta.ToString + "),0)"
            TotalPeso = Comm.ExecuteScalar
            Return TotalVenta
        Else
            Return DaTotalb(pidVenta, pidMoneda, NoNegativos)
        End If
    End Function


    Public Function DaTotalb(ByVal pidVenta As Integer, ByVal pidMoneda As Integer, ByVal NoNegativos As String) As Double
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
        Dim iCargo As Double
        Dim iDescuento As Double
        Dim iDescuento2 As Double
        Dim pIEPS As Double
        Dim pIVARetenido As Double
        Dim EsRevDEv As Byte
        Dim iIdInventario As Integer
        Dim SobreImp As Double
        'Dim iIdCliente As Integer
        'Dim iTruncar As Byte
        Subtototal = 0
        TotalIva = 0
        TotalVenta = 0
        'Comm.CommandText = "select desglosar from tblventas where idventa=" + pidVenta.ToString
        'iTruncar = Comm.ExecuteScalar
        Comm.CommandText = "select tipodecambio from tblventas where idventa=" + pidVenta.ToString
        iTipoCambio = Comm.ExecuteScalar
        'Comm.CommandText = "select idcliente from tblventas where idventa=" + pidVenta.ToString
        'iIdCliente = Comm.ExecuteScalar
        Comm.CommandText = "select isr from tblventas where idventa=" + pidVenta.ToString
        iIsr = Comm.ExecuteScalar
        Comm.CommandText = "select ivaretenido from tblventas where idventa=" + pidVenta.ToString
        iIvaRetenido = Comm.ExecuteScalar
        Comm.CommandText = "select descuentog from tblventas where idventa=" + pidVenta.ToString
        iDescuento = Comm.ExecuteScalar
        Comm.CommandText = "select descuentog2 from tblventas where idventa=" + pidVenta.ToString
        iDescuento2 = Comm.ExecuteScalar
        Comm.CommandText = "select cargog from tblventas where idventa=" + pidVenta.ToString
        iCargo = Comm.ExecuteScalar
        Comm.CommandText = "select sobreimploc from tblventas where idventa=" + pidVenta.ToString
        SobreImp = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select idventasinventario from tblventasinventario where idventa=" + pidVenta.ToString
        DReader = Comm.ExecuteReader
        TotalDescuento = 0
        TotalIEPS = 0
        TotalIvaRetenidoConceptos = 0
        TotalRevdev = 0
        While DReader.Read
            IDs.Add(DReader("idventasinventario"))
        End While
        DReader.Close()
        Dim Desc As Double
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar

            Comm.CommandText = "select iva from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            Comm.CommandText = "select descuento from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
            Desc = Comm.ExecuteScalar
            'IEPS e IVA Retenido
            Comm.CommandText = "select ieps from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
            pIEPS = Comm.ExecuteScalar
            Comm.CommandText = "select IVARetenido from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
            pIVARetenido = Comm.ExecuteScalar
            Comm.CommandText = "select idinventario from tblventasinventario where idventasinventario=" + IDs.Item(Cont).ToString
            iIdInventario = Comm.ExecuteScalar
            Comm.CommandText = "select esrevdev from tblinventario where idinventario=" + iIdInventario.ToString
            EsRevDEv = Comm.ExecuteScalar
            If iIdInventario = 1 Then
                TotalOfertas += Precio
            End If
            If pidMoneda = 2 Then
                If pidMoneda <> IdMonedaC Then
                    Precio = Precio * iTipoCambio
                End If
            Else
                If IdMonedaC = 2 Then
                    Precio = Precio / iTipoCambio
                End If
            End If
            TotalIvaRetenidoConceptos += (Precio * (pIVARetenido / 100))
            TotalIEPS += (Precio * (pIEPS / 100))
            TotalIva += (Precio * (iIva / 100))
            If Desc <> 100 Then
                TotalDescuento += (Precio / (1 - Desc / 100)) - Precio
            Else
                TotalDescuento += Precio
            End If
            Subtototal += Precio
            If EsRevDEv = 1 Then
                TotalRevdev += Precio
            End If
            ''If NoNegativos <> "1" And Precio < 0 Then
            'TotalIvaRetenidoConceptos += (Precio * (pIVARetenido / 100))
            'TotalIEPS += (Precio * (pIEPS / 100))
            'TotalIva += (Precio * (iIva / 100))
            'TotalDescuento += (Precio / (1 - Desc / 100)) - Precio
            'Subtototal += Precio
            ''Else
            'TotalIva += (Precio * (iIva / 100))
            ''End If

            Cont += 1
        End While
        'If iTruncar = 0 Then

        Comm.CommandText = "select ifnull((select sum(precio+(precio*(iva/100))) from tblventasinventario where idventa=" + pidVenta.ToString + " and precio<0),0)"
        TotalNegativos = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(precio) from tblventasinventario where idventa=" + pidVenta.ToString + " and precio<0),0)"
        TotalNegativosSinIVA = Comm.ExecuteScalar

        Dim Cimp As New dbClientesImpuestos(Comm.Connection)
        ImpLocales.Clear()
        DReader = Cimp.ConsultaReaderI(pidVenta)
        TotalRetLocal = 0
        TotalTrasLocal = 0
        While DReader.Read
            ILocal.Nombre = DReader("nombre")
            ILocal.Tasa = DReader("tasa")
            ILocal.Tipo = DReader("tipo")
            ILocal.Importe2 = DReader("importe")
            'If NoNegativos = "0" Then
            '    ILocal.Importe = (Subtototal + TotalDescuento - TotalRevdev) * (DReader("tasa") / 100)
            '    ILocal.Importe = CDbl(Format(ILocal.Importe, "#.00"))
            'Else
            '    ILocal.Importe = (Subtototal + TotalDescuento - TotalNegativosSinIVA - TotalRevdev) * (DReader("tasa") / 100)
            '    ILocal.Importe = CDbl(Format(ILocal.Importe, "#.00"))
            'End If
            If ILocal.Tasa <> 0 Then
                If NoNegativos = "0" Then
                    If SobreImp = 0 Then
                        ILocal.Importe = (Subtototal + TotalDescuento - TotalRevdev) * (DReader("tasa") / 100)
                    Else
                        ILocal.Importe = SobreImp * (DReader("tasa") / 100)
                    End If
                    ILocal.Importe = CDbl(Format(ILocal.Importe, "#.00"))
                Else
                    If SobreImp = 0 Then
                        ILocal.Importe = (Subtototal + TotalDescuento - TotalNegativosSinIVA - TotalRevdev) * (DReader("tasa") / 100)
                    Else
                        ILocal.Importe = SobreImp * (DReader("tasa") / 100)
                    End If
                    ILocal.Importe = CDbl(Format(ILocal.Importe, "#.00"))
                End If
            Else
                ILocal.Importe = ILocal.Importe2
            End If
            If ILocal.Tipo = 0 Then
                TotalTrasLocal += ILocal.Importe
            Else
                TotalRetLocal += ILocal.Importe
            End If
            ImpLocales.Add(ILocal)
        End While
        DReader.Close()

        'Ivas esto es lo que realmente es diferente del total normal al total alterno

        Dim Ivas As New Collection
        'Dim IvasImporte As New Collection
        DReader = DaIvas(pidVenta)
        'Dim IAnt As Double
        While DReader.Read
            If Ivas.Contains(DReader("iva").ToString) = False Then
                Ivas.Add(DReader("iva"), DReader("iva").ToString)
            End If
        End While
        DReader.Close()
        TotalIva = 0
        Dim Suma As Double = 0
        For Each I As Double In Ivas
            Comm.CommandText = "select ifnull((select sum(precio) from tblventasinventario where iva=" + I.ToString + " and idventa=" + pidVenta.ToString + " and precio>0),0)"
            Suma = Comm.ExecuteScalar - iDescuento
            Comm.CommandText = "select ifnull((select sum(precio) from tblventasinventario where iva=" + I.ToString + " and idventa=" + pidVenta.ToString + " and precio<0),0)"
            Suma += Comm.ExecuteScalar
            TotalIva += (Suma * (I / 100))
        Next
        'Ivas fin ---------------




        TotalDescuento += iDescuento
        If NoNegativos = "1" Then
            TotalDescuento = CDbl(Format(TotalDescuento, "#.00"))
            Subtototal = CDbl(Format(Subtototal, "#.00"))
            TotalIva = CDbl(Format(TotalIva, "#.00"))
        End If
        TotalISR = Subtototal * (iIsr / 100)
        TotalIvaRetenido = Subtototal * (iIvaRetenido / 100)
        TotalVenta = Subtototal + TotalIva - TotalISR - TotalIvaRetenido - iDescuento - iDescuento2 + iCargo - TotalRetLocal + TotalTrasLocal + TotalIEPS - TotalIvaRetenidoConceptos
        TotalSinRetencion = Subtototal + TotalIva - TotalISR - TotalIvaRetenido - iDescuento - iDescuento2 + iCargo - TotalIvaRetenidoConceptos
        Comm.CommandText = "select ifnull((select sum(tblinventario.peso*tblventasinventario.cantidad) from tblventasinventario inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario where tblventasinventario.idventa=" + pidVenta.ToString + "),0)"
        TotalPeso = Comm.ExecuteScalar
        Subtototal = Subtototal - iDescuento
        Return TotalVenta
    End Function
    Public Function DaTotalConceptosporIva(pidventa As Integer, I As Double) As Double
        Dim Total As Double
        Dim iDescuento As Double
        Comm.CommandText = "select descuentog from tblventas where idventa=" + pidventa.ToString
        iDescuento = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(precio) from tblventasinventario where iva=" + I.ToString + " and idventa=" + pidventa.ToString + " and precio>0),0)"
        Total = Comm.ExecuteScalar - iDescuento
        Comm.CommandText = "select ifnull((select sum(precio) from tblventasinventario where iva=" + I.ToString + " and idventa=" + pidventa.ToString + " and precio<0),0)"
        Total += Comm.ExecuteScalar
        Return Total
    End Function

    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer, ByVal pEsElectronica As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblventas where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4) and eselectronica=" + pEsElectronica.ToString + " ),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblventas where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    Public Function DaId(ByVal pFolio As Integer, ByVal pSerie As String) As Integer
        Comm.CommandText = "select ifnull((select idventa from tblventas where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2 limit 1),0)"
        ID = Comm.ExecuteScalar
        If ID <> 0 Then LlenaDatos()
        Return ID
    End Function
    'Public Sub SetnFacturado(ByVal pidVenta As Integer, ByVal pTipo As TiposFactura, ByVal pCredito As Byte, ByVal pTotal As Double)
    '    Dim Tipo As Byte
    '    Tipo = pTipo
    '    Total = pTotal
    '    If pCredito = 0 Then
    '        Comm.CommandText = "update tblventas set facturado=" + Tipo.ToString + ",total=" + Total.ToString + ",hora='" + Format(Date.Today, "HH:mm:ss") + "' where idventa=" + pidVenta.ToString
    '    Else
    '        Comm.CommandText = "update tblventas set facturado=" + Tipo.ToString + ",totalapagar=" + Total.ToString + ",credito=1,total=" + Total.ToString + ",hora='" + Format(Date.Today, "HH:mm:ss") + "' where idventa=" + pidVenta.ToString
    '    End If
    '    Comm.ExecuteNonQuery()
    'End Sub

    'Public Function print(ByVal idmoneda As Integer) As ArrayList
    '    Dim nodos As New ArrayList
    '    Dim abd As New BDImpresiones
    '    Dim n As NodoImpresionTexto

    '    Dim dr As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim CD As New dbVentasInventario(MySqlcon)
    '    dr = CD.ConsultaReader(ID)
    '    Dim articulos As New ArrayList

    '    While dr.Read
    '        articulos.Add(New ArticuloFactura(dr("idventasinventario"), "A", "", dr("cantidad"), dr("clave"), dr("descripcion"), dr("precio"))) ', dr("abreviatura")))
    '    End While
    '    dr.Close()
    '    Dim VP As New dbVentasProductos(MySqlcon)
    '    dr = VP.ConsultaReader(ID)
    '    While dr.Read
    '        articulos.Add(New ArticuloFactura(dr("idventasproducto"), "P", "", dr("cantidad"), dr("clave"), dr("descripcion"), dr("precio"))) ', dr("abreviatura")))
    '    End While
    '    dr.Close()
    '    Dim VS As New dbVentasServicios(MySqlcon)
    '    dr = VS.ConsultaReader(ID)
    '    While dr.Read
    '        articulos.Add(New ArticuloFactura(dr("idventasservicio"), "S", "", dr("cantidad"), dr("folio"), dr("descripcion"), dr("precio"))) ', dr("abreviatura")))
    '    End While
    '    dr.Close()

    '    Cliente.BuscaCliente(IdCliente)

    '    Dim descripcion = "", cantidad = "", codigo = "", importe = "", preciounitario As String = ""
    '    Dim af As ArticuloFactura
    '    Const descmaxlength As Integer = 50
    '    For Each af In articulos
    '        Dim start = 0, length As Integer = 0
    '        'If af.idarticulo <> 0 Then
    '        cantidad += CStr(af.cantidad).PadLeft(6)
    '        codigo += af.codigo
    '        'importe += Format(af.importe, "C2").PadLeft(10)
    '        preciounitario += Format(af.preciounitario, "C2").PadLeft(10)
    '        'End If

    '        Do
    '            length = If(Format(af.descripcion).Substring(start, Format(af.descripcion).Length - start).Length <= descmaxlength, Format(af.descripcion).Length - start, If(Format(af.descripcion).Substring(start, descmaxlength).LastIndexOf(" ") = -1, descmaxlength, Format(af.descripcion).Substring(start, descmaxlength).LastIndexOf(" ")))
    '            descripcion += Format(af.descripcion).Substring(start, length) + vbNewLine
    '            cantidad += vbNewLine
    '            codigo += vbNewLine
    '            importe += vbNewLine
    '            preciounitario += vbNewLine
    '            start += length + 1
    '        Loop While start < Format(af.descripcion).Length

    '    Next

    '    For Each n In abd.find(7).campos
    '        If n.visible Then
    '            If n.texto = "titulo" Then nodos.Add(New NodoImpresionTexto("FACTURA", n.x, n.y, n.visible))
    '            If n.texto = "nombreempresa" Then nodos.Add(New NodoImpresionTexto(My.Settings.empresa, n.x, n.y, n.visible))
    '            If n.texto = "rfcempresa" Then nodos.Add(New NodoImpresionTexto(My.Settings.rfc, n.x, n.y, n.visible))

    '            If n.texto = "nombre" Then nodos.Add(New NodoImpresionTexto(Cliente.Nombre, n.x, n.y, n.visible))
    '            If n.texto = "nocliente" Then nodos.Add(New NodoImpresionTexto(Cliente.Clave, n.x, n.y, n.visible))
    '            If n.texto = "direccion" Then nodos.Add(New NodoImpresionTexto(Cliente.Direccion, n.x, n.y, n.visible))
    '            'If n.texto = "direccioncol" Then nodos.Add(New NodoImpresionTexto(Cliente.Direccion + " " + Cliente.colonia, n.x, n.y, n.visible))
    '            If n.texto = "ciudad" Then nodos.Add(New NodoImpresionTexto(Cliente.Ciudad + ", " + Cliente.Estado + " " + Cliente.CP, n.x, n.y, n.visible))
    '            'If n.texto = "colonia" Then nodos.Add(New NodoImpresionTexto(Cliente.colonia, n.x, n.y, n.visible))
    '            If n.texto = "telefono" Then nodos.Add(New NodoImpresionTexto(Cliente.Telefono, n.x, n.y, n.visible))
    '            'If n.texto = "curp" Then nodos.Add(New NodoImpresionTexto(Cliente.curp, n.x, n.y, n.visible))
    '            'If n.texto = "ruc" Then nodos.Add(New NodoImpresionTexto(Cliente.ruc, n.x, n.y, n.visible))
    '            'If n.texto = "cnpj" Then nodos.Add(New NodoImpresionTexto(Cliente.cnpj, n.x, n.y, n.visible))

    '            'If n.texto = "adicional1" Then nodos.Add(New NodoImpresionTexto(_adicional1, n.x, n.y, n.visible))
    '            'If n.texto = "adicional2" Then nodos.Add(New NodoImpresionTexto(_adicional2, n.x, n.y, n.visible))
    '            'If n.texto = "adicionalc" Then If _tipoventa = TIPOSVENTAS.CREDITO Then nodos.Add(New NodoImpresionTexto(_adicionalc, n.x, n.y, n.visible))
    '            'If n.texto = "comentario" Then nodos.Add(New NodoImpresionTexto(_comentario, n.x, n.y, n.visible))
    '            'If n.texto = "condiciones" Then nodos.Add(New NodoImpresionTexto(condiciones, n.x, n.y, n.visible))
    '            'If n.texto = "exhibiciones1" Then If exhibiciones = 0 Then nodos.Add(New NodoImpresionTexto("X", n.x, n.y, n.visible))
    '            'If n.texto = "exhibiciones2" Then If exhibiciones = 1 Then nodos.Add(New NodoImpresionTexto("X", n.x, n.y, n.visible))

    '            'If n.texto = "tasaiva" Then nodos.Add(New NodoImpresionTexto(CStr(Cliente.iva.tasa) + "%", n.x, n.y, n.visible))
    '            If n.texto = "fecha" Then nodos.Add(New NodoImpresionTexto(Format(Fecha, "dd MM yyyy"), n.x, n.y, n.visible))
    '            If n.texto = "nofactura" Then nodos.Add(New NodoImpresionTexto(Format(CInt(Folio), "0000"), n.x, n.y, n.visible))
    '            If n.texto = "rfc" Then nodos.Add(New NodoImpresionTexto(Cliente.RFC, n.x, n.y, n.visible))

    '            ''''''
    '            Dim T As Double = DaTotal(ID, idmoneda)
    '            Dim Iva As Double
    '            If Desglosar Then
    '                Iva = T - (T / (1 + (Iva / 100)))
    '                If n.texto = "subtotal" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(T - Iva, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '                If n.texto = "iva" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(Iva, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '                If n.texto = "total" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(T, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '            Else
    '                Iva = T * (Iva / 100)
    '                If n.texto = "subtotal" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(T, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '                If n.texto = "iva" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(Iva, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '                If n.texto = "total" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(T + Iva, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '            End If

    '            ''''''

    '            If n.texto = "totalletra" Then nodos.Add(New NodoImpresionTexto(totalLetra(idmoneda), n.x, n.y, n.visible))

    '            If n.texto = "descripcion" Then nodos.Add(New NodoImpresionTexto(descripcion, n.x, n.y, n.visible))
    '            If n.texto = "cantidad" Then nodos.Add(New NodoImpresionTexto(cantidad, n.x, n.y, n.visible))
    '            If n.texto = "codigo" Then nodos.Add(New NodoImpresionTexto(codigo, n.x, n.y, n.visible))
    '            If n.texto = "importe" Then nodos.Add(New NodoImpresionTexto(importe, n.x, n.y, n.visible))
    '            If n.texto = "preciounitario" Then nodos.Add(New NodoImpresionTexto(preciounitario, n.x, n.y, n.visible))

    '            'If n.texto = "pagfecha1" Then nodos.Add(New NodoImpresionTexto(Format(CDate(Fecha), "dd MM yyyy"), n.x, n.y, n.visible))
    '            'If n.texto = "pagfecha2" Then nodos.Add(New NodoImpresionTexto(Format(CDate(Fecha), "dd MM yyyy"), n.x, n.y, n.visible))
    '            'If n.texto = "pagtotal" Then nodos.Add(New NodoImpresionTexto(Format(total, "C2").PadLeft(10), n.x, n.y, n.visible))
    '            'If n.texto = "pagtotalletra" Then nodos.Add(New NodoImpresionTexto(totalLetra, n.x, n.y, n.visible))
    '            'If n.texto = "pagnombre" Then nodos.Add(New NodoImpresionTexto(Cliente.Nombre, n.x, n.y, n.visible))
    '            'If n.texto = "pagdireccion" Then nodos.Add(New NodoImpresionTexto(Cliente.Direccion, n.x, n.y, n.visible))
    '            'If n.texto = "pagciudad" Then nodos.Add(New NodoImpresionTexto(Cliente.Ciudad + ", " + Cliente.Estado + " " + Cliente.CP, n.x, n.y, n.visible))
    '            'If n.texto = "pagcondiciones" Then nodos.Add(New NodoImpresionTexto(condiciones, n.x, n.y, n.visible))
    '        End If
    '    Next

    '    Return nodos
    'End Function

    'Public ReadOnly Property totalLetra(ByVal idmoneda As Integer) As String
    '    Get
    '        Dim f As New PaseLetras
    '        Return f.PASELETRAS(DaTotal(ID, idmoneda), idmoneda) + " " + [Enum].GetName(GetType(MONEDAS), idmoneda)
    '    End Get
    'End Property


    'Public Sub AgregarDetallesReferencia(ByVal PidVenta As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte, ByVal pidAlmacen As Integer)
    '    '0 cotizacion
    '    '1 pedido
    '    '2 remision
    '    '3 ventas

    '    If Tipo = 0 Then
    '        Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm) select " + PidVenta.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0,0,cantidad,if(idinventario>1,(select tipocantidad from tblinventario where tblinventario.idinventario=tblventascotizacionesinventario.idinventario),(select tipocantidad from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventascotizacionesinventario.idvariante )) from tblventascotizacionesinventario where idcotizacion=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()
    '        Comm.CommandText = "update tblinventarioseries set idventa=" + PidVenta.ToString + ",idcotizacion=0 where idcotizacion=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()
    '    End If

    '    If Tipo = 1 Then
    '        Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm) select " + PidVenta.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0,0,cantidad,if(idinventario>1,(select tipocantidad from tblinventario where tblinventario.idinventario=tblventaspedidosinventario.idinventario),(select tipocantidad from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventaspedidosinventario.idvariante )) from tblventaspedidosinventario where idpedido=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()    
    '    End If

    '    If Tipo = 2 Then
    '        Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm) select " + PidVenta.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,cantidad,cantidad,if(idinventario>1,(select tipocantidad from tblinventario where tblinventario.idinventario=tblventasremisionesinventario.idinventario),(select tipocantidad from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventasremisionesinventario.idvariante )) from tblventasremisionesinventario where idremision=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()
    '        Comm.CommandText = "update tblinventarioseries set idventa=" + PidVenta.ToString + ",idremision=0 where idremision=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()
    '    End If

    '    If Tipo = 3 Then
    '        Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm) select " + PidVenta.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,0,cantidadm,tipocantidadm from tblventasinventario where idventa=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()
    '    End If
    'End Sub

    Public Sub AgregarDetallesReferencia(ByVal PidVenta As Integer, ByVal pIdDocumento As Integer(), ByVal Tipo As Byte, ByVal pidAlmacen As Integer)
        '0 cotizacion
        '1 pedido
        '2 remision
        '3 ventas

        Dim whereFields As String = ""
        Dim id As Integer

        If Tipo = 0 Then
            For Each id In pIdDocumento
                whereFields += " or idcotizacion=" + id.ToString
            Next
            Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm,ieps,ivaretenido,predial) select " + PidVenta.ToString + ",idinventario,sum(cantidad),sum(precio),descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0,0,sum(cantidad),if(idinventario>1,(select tipocantidad from tblinventario where tblinventario.idinventario=tblventascotizacionesinventario.idinventario),(select tipocantidad from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventascotizacionesinventario.idvariante )),tblventascotizacionesinventario.ieps,tblventascotizacionesinventario.ivaretenido,'' from tblventascotizacionesinventario where false" + whereFields + " group by idinventario,precio;"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "update tblinventarioseries set idventa=" + PidVenta.ToString + ",idcotizacion=0 where false " + whereFields
            Comm.ExecuteNonQuery()
        End If

        If Tipo = 1 Then
            For Each id In pIdDocumento
                whereFields += " or idpedido=" + id.ToString
            Next
            Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm,ieps,ivaretenido,predial) select " + PidVenta.ToString + ",idinventario,sum(cantidad),sum(precio),descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0,0,sum(cantidad),if(idinventario>1,(select tipocantidad from tblinventario where tblinventario.idinventario=tblventaspedidosinventario.idinventario),(select tipocantidad from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventaspedidosinventario.idvariante )),tblventaspedidosinventario.ieps,tblventaspedidosinventario.ivaretenido,'' from tblventaspedidosinventario where false" + whereFields + " group by idinventario,precio;"
            Comm.ExecuteNonQuery()
        End If

        If Tipo = 2 Then
            For Each id In pIdDocumento
                whereFields += " or idremision=" + id.ToString
            Next
            Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm,ieps,ivaretenido,predial) select " + PidVenta.ToString + ",idinventario,sum(cantidad),sum(precio),descripcion,tblventasremisionesinventario.idmoneda,idalmacen,tblventasremisionesinventario.iva,extra,descuento,idvariante,idservicio,sum(cantidad),sum(tblventasremisionesinventario.cantidadm),tblventasremisionesinventario.tipocantidadm,tblventasremisionesinventario.ieps,tblventasremisionesinventario.ivaretenido,'' from tblventasremisionesinventario inner join tblventasremisiones on tblventasremisionesinventario.idremision=tblventasremisiones.idremision where tblventasremisiones.estado=3 and (false " + Replace(whereFields, "idremision", "tblventasremisiones.idremision") + ") group by idinventario,round(precio/cantidad,3);"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "update tblinventarioseries set idventa=" + PidVenta.ToString + ",idremision=0 where false " + whereFields
            Comm.ExecuteNonQuery()
        End If

        If Tipo = 3 Then
            For Each id In pIdDocumento
                whereFields += " or idventa=" + id.ToString
            Next
            Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm,ieps,ivaretenido,predial) select " + PidVenta.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,0,cantidadm,tipocantidadm,ieps,ivaretenido,predial from tblventasinventario where false" + whereFields + " order by idventasinventario;"
            Comm.ExecuteNonQuery()
        End If

        If Tipo = 4 Then
            For Each id In pIdDocumento
                whereFields += " or idpedido=" + id.ToString
            Next
            Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm,ieps,ivaretenido,predial) select " + PidVenta.ToString + ",idinventario,sum(cantidad),sum(precio),descripcion,tblfertilizantespedidosdetalles.idmoneda," + pidAlmacen.ToString + ",tblfertilizantespedidosdetalles.iva,'',tblfertilizantespedidosdetalles.descuento,1,0,sum(cantidad),sum(cantidad),(select tipocantidad from tblinventario where tblinventario.idinventario=tblfertilizantespedidosdetalles.idinventario),tblfertilizantespedidosdetalles.ieps,tblfertilizantespedidosdetalles.ivaretenido,'' from tblfertilizantespedidosdetalles inner join tblfertilizantespedidos on tblfertilizantespedidosdetalles.idpedido=tblfertilizantespedidos.idpedido where tblfertilizantespedidos.estado=3 and (false " + Replace(whereFields, "idpedido", "tblfertilizantespedidos.idpedido") + ") group by idinventario,round(precio/cantidad,3);"
            Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblinventarioseries set idventa=" + PidVenta.ToString + ",idremision=0 where false " + whereFields
            'Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Sub RegresaInventario(ByVal pId As Integer)
        Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,surtido,0,0,0),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen,surtido,0,0),0) from tblventasinventario where idventa=" + pId.ToString + ";"
        Comm.CommandText += "select spmodificainventarioi(idinventario,idalmacen,surtido,0,0,0) from tblventaskits where idventa=" + pId.ToString + ";"
        Comm.CommandText += "select spmodificainventariolotesf(tblventaslotes.idlote,tblventaslotes.surtido,0,0,0) from tblventaslotes inner join tblventasinventario on tblventaslotes.iddetalle = tblventasinventario.idventasinventario where tblventasinventario.idventa=" + pId.ToString + "; "
        Comm.CommandText += "select spmodificainventarioaduanaf(tblventasaduanan.idaduana,tblventasaduanan.surtido,0,0,0) from tblventasaduanan inner join tblventasinventario on tblventasaduanan.iddetalle = tblventasinventario.idventasinventario where tblventasinventario.idventa=" + pId.ToString + "; "
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblventasinventario set surtido=0 where idventa=" + pId.ToString + ";"
        Comm.CommandText += "update tblventaslotes inner join tblventasinventario on tblventaslotes.iddetalle=tblventasinventario.idventasinventario set tblventaslotes.surtido=0 where idventa=" + pId.ToString + ";"
        Comm.CommandText += "update tblventasaduanan inner join tblventasinventario on tblventasaduanan.iddetalle=tblventasinventario.idventasinventario set tblventasaduanan.surtido=0 where idventa=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub


    Public Function CreaCadenaOriginal(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|2.0|"
        ID = pIdVenta
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, 2, noNegativos, Alterno)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        CO += Serie + "|"
        CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        CO += NoAprobacion + "|"
        CO += YearAprobacion + "|"
        CO += "ingreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        CO += Format(Subtototal, "#0.00") + "|"
        CO += Format(Descuento, "#0.00") + "|" 'descuento

        CO += Format(TotalVenta, "#0.00") + "|" ' total factura con iva

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
        Dim VI As New dbVentasInventario(MySqlcon)
        DR = VI.ConsultaReader(ID, False, "0", 0, "0")
        'Dim PrecioTemp As Double
        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                CO += DR("cantidad").ToString + "|"
                CO += DR("tipocantidad") + "|"
                'CO += DR("clave") + "|"
                CO += Trim(DR("descripcion")) + "|"
                If DR("idmoneda") <> 2 Then
                    CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
                    CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
                Else
                    CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
                    CO += Format(DR("precio"), "#0.00") + "|"
                End If
            End If
        End While
        DR.Close()

        'Dim VP As New dbVentasProductos(MySqlcon)
        'DR = VP.ConsultaReader(ID)

        'While DR.Read
        '    CO += DR("cantidad").ToString + "|"
        '    CO += DR("tipocantidad") + "|"
        '    'CO += DR("clave") + "|"
        '    CO += DR("descripcion") + "|"
        '    If DR("idmoneda") <> 2 Then
        '        CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
        '        CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
        '    Else
        '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
        '        CO += Format(DR("precio"), "#0.00") + "|"
        '    End If
        'End While
        'DR.Close()

        'Dim VS As New dbVentasServicios(MySqlcon)
        'DR = VS.ConsultaReader(ID)

        'While DR.Read
        '    CO += DR("cantidad").ToString + "|"
        '    CO += "SERV|"
        '    CO += DR("folio") + "|"
        '    CO += DR("descripcion") + "|"
        '    If DR("idmoneda") <> 2 Then
        '        CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
        '        CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
        '    Else
        '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
        '        CO += Format(DR("precio"), "#0.00") + "|"
        '    End If
        'End While
        'DR.Close()

        If ISR <> 0 Then
            CO += "ISR|" + Format(TotalISR, "#0.00") + "|"
        End If
        If IvaRetenido <> 0 Then
            CO += "IVA|" + Format(TotalIvaRetenido, "#0.00") + "|"
        End If
        If ISR <> 0 Or IvaRetenido <> 0 Then
            CO += Format(TotalISR + TotalIvaRetenido, "#0.00") + "|"
        End If
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
                    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                Else
                    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                Else
                    IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
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


    Public Function CreaCadenaOriginal22(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|2.2|"
        ID = pIdVenta
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, IdConversion, noNegativos, Alterno)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        CO += Serie + "|"
        CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        CO += NoAprobacion + "|"
        CO += YearAprobacion + "|"
        Dim CP As New dbVentasCartaPorte(ID, MySqlcon)
        If CP.Origen = "Nohay" Then
            CO += "ingreso|"
        Else
            CO += "traslado|"
        End If

        Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        If IdFormadePago = 98 Then
            If Parcialidades <> 1 Then
                CO += "Pago en " + Parcialidades.ToString + " parcialidades|"
            Else
                CO += "Pago en parcialidades|"
            End If
        End If
        'If FP.Tipo = 2 Then
        '    CO += "Parcialidad " + Parcialidad.ToString + " de " + Parcialidades.ToString + "|"
        'End If
        If IdFormadePago <> 98 Then
            CO += "Pago en una sola exhibición|"
        End If

        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        CO += Format(Subtototal, "#0.00") + "|"
        CO += Format(Descuento, "#0.00") + "|" 'descuento

        CO += Format(TotalVenta, "#0.00") + "|" ' total factura con iva

        'metododepago

        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        'CO += Trim(FP.Nombre) + "|"
        If IdFormadePago <> 98 Then
            CO += Trim(FP.Nombre) + "|"
        Else
            CO += "No aplica|"
        End If
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """"
        'Else
        'CO += "No identificado|"
        'End If
        'lugar de expedicion
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            CO += Trim(Sucursal.Ciudad2) + "," + Trim(Sucursal.Estado2) + "|"
        Else
            CO += Trim(Sucursal.Ciudad) + "," + Trim(Sucursal.Estado) + "|"
        End If
        If NoCuenta <> "" Then CO += Trim(NoCuenta) + "|"
        'Tipo de cambio

        If IdConversion <> 2 Then
            CO += Format(TipodeCambio, "#0.00") + "|"
            Dim Moneda As New dbMonedas(IdConversion, Comm.Connection)
            CO += Moneda.Abreviatura + "|"
        Else
            CO += "MXN|"
        End If

        'Aqui lo de parcialidades
        If FP.Tipo = 2 Then
            ObtenerFacturaOriginal(IdVentaOrigen)
            CO += FolioOrigen.ToString + "|"
            CO += SerieOrigen + "|"
            CO += FechaOrigen + "|"
            CO += Format(MontoOrigen, "0.00") + "|"
        End If

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
        Dim AduanaCol As New Collection

        Dim VA As New dbventasaduana(MySqlcon)
        DR = VA.ConsultaTodoReader(ID)
        While DR.Read
            AduanaCol.Add(New InfoAduana(DR("numero"), DR("fecha"), DR("aduana"), DR("iddetalle")))
        End While
        DR.Close()
        Dim VI As New dbVentasInventario(MySqlcon)
        DR = VI.ConsultaReader(ID, False, "0", 0, "0")
        'Dim PrecioTemp As Double
        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                CO += DR("cantidad").ToString + "|"
                CO += DR("tipocantidad") + "|"
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
                'numero fecha aduana
                For Each ad As InfoAduana In AduanaCol
                    If ad.IdDetalle = DR("idventasinventario") Then
                        CO += Trim(ad.Numero) + "|"
                        CO += Trim(ad.Fecha.Replace("/", "-")) + "|"
                        CO += Trim(ad.Aduana) + "|"
                    End If
                Next
            End If
        End While
        DR.Close()




        If ISR <> 0 Then
            CO += "ISR|" + Format(TotalISR, "#0.00") + "|"
        End If
        If IvaRetenido <> 0 Then
            CO += "IVA|" + Format(TotalIvaRetenido, "#0.00") + "|"
        End If
        If ISR <> 0 Or IvaRetenido <> 0 Then
            CO += Format(TotalISR + TotalIvaRetenido, "#0.00") + "|"
        End If
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


        If ImpLocales.Count > 0 Then
            CO += "1.0|" + Format(TotalRetLocal, "#0.00") + "|" + Format(TotalTrasLocal, "#0.00") + "|"
            For Each Im As Implocal In ImpLocales
                If Im.Tipo = 1 Then
                    CO += Im.Nombre + "|" + Format(Im.Tasa, "#0.00") + "|" + Format(Im.Importe, "#0.00") + "|"
                Else
                    CO += Im.Nombre + "|" + Format(Im.Tasa, "#0.00") + "|" + Format(Im.Importe, "#0.00") + "|"
                End If
            Next
        End If


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



    '************************************CFDi**************************************
    Public Function CreaCadenaOriginali(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|3.0|"
        ID = pIdVenta
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, 2, noNegativos, Alterno)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        'CO += Serie + "|"
        'CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        'CO += NoAprobacion + "|"
        'CO += YearAprobacion + "|"
        CO += "ingreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        CO += Format(Subtototal, "#0.00") + "|"
        CO += Format(Descuento, "#0.00") + "|" 'descuento

        CO += Format(TotalVenta, "#0.00") + "|" ' total factura con iva

        CO += Sucursal.RFC + "|"
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
        Dim VI As New dbVentasInventario(MySqlcon)
        DR = VI.ConsultaReader(ID, False, "0", 0, "0")
        'Dim PrecioTemp As Double
        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                CO += DR("cantidad").ToString + "|"
                CO += DR("tipocantidad") + "|"
                'CO += DR("clave") + "|"
                CO += Trim(DR("descripcion")) + "|"
                If DR("idmoneda") <> 2 Then
                    CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
                    CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
                Else
                    CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
                    CO += Format(DR("precio"), "#0.00") + "|"
                End If
            End If
        End While
        DR.Close()

        If ISR <> 0 Then
            CO += "ISR|" + Format(TotalISR, "#0.00") + "|"
        End If
        If IvaRetenido <> 0 Then
            CO += "IVA|" + Format(TotalIvaRetenido, "#0.00") + "|"
        End If
        If ISR <> 0 Or IvaRetenido <> 0 Then
            CO += Format(TotalISR + TotalIvaRetenido, "#0.00") + "|"
        End If
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
                    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                Else
                    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                Else
                    IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
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
        DaTotal(ID, IdConversion, noNegativos, Alterno)
        Dim Sucursal As New dbSucursales(IdSucursal, Comm.Connection)
        'CI = TI * (Iva / 100)
        'CO += Serie + "|"
        'CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        'CO += NoAprobacion + "|"
        'CO += YearAprobacion + "|"
        Dim CP As New dbVentasCartaPorte(ID, Comm.Connection)
        If CP.Origen = "Nohay" Then
            CO += "ingreso|"
        Else
            CO += "traslado|"
        End If

        Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        If IdFormadePago = 98 Then
            If Parcialidades <> 1 Then
                CO += "Pago en " + Parcialidades.ToString + " parcialidades|"
            Else
                CO += "Pago en parcialidades|"
            End If
        End If
        'If FP.Tipo = 2 Then
        '    CO += "Parcialidad " + Parcialidad.ToString + " de " + Parcialidades.ToString + "|"
        'End If
        If IdFormadePago <> 98 Then
            CO += "Pago en una sola exhibición|"
        End If
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        If Alterno = "0" Then
            CO += Format(Subtototal, "#0.00####") + "|"
        Else
            CO += Format(Subtototal + Descuento, "#0.00####") + "|"
        End If
        CO += Format(Descuento + DescuentoG2, "#0.00####") + "|" 'descuento
        'Tipo de cambio
        If IdConversion <> 2 Then
            CO += Format(TipodeCambio, "#0.00####") + "|"
            Dim Moneda As New dbMonedas(IdConversion, Comm.Connection)
            CO += Moneda.Abreviatura + "|"
        Else
            CO += "MXN|"
        End If
        CO += Format(TotalVenta, "#0.00####") + "|" ' total factura con iva

        'metododepago

        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        If IdFormadePago <> 98 Then
            CO += Trim(FP.Nombre) + "|"
        Else
            CO += "No aplica|"
        End If
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """"
        'Else
        'CO += "No identificado|"
        'End If
        'lugar de expedicion
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            CO += Trim(Sucursal.Ciudad2) + "," + Trim(Sucursal.Estado2) + "|"
        Else
            CO += Trim(Sucursal.Ciudad) + "," + Trim(Sucursal.Estado) + "|"
        End If
        If NoCuenta <> "" Then CO += Trim(NoCuenta) + "|"




        'Aqui lo de parcialidades
        If FP.Tipo = 2 Then
            ObtenerFacturaOriginal(IdVentaOrigen)
            If FolioUUIDOrigen = "" Then
                CO += FolioOrigen.ToString + "|"
            Else
                CO += FolioUUIDOrigen + "|"
            End If
            If SerieOrigen <> "" Then
                CO += SerieOrigen + "|"
            End If
            CO += FechaOrigen + "|"
            CO += Format(MontoOrigen, "0.00####") + "|"
        End If

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
        Dim AduanaCol As New Collection
        Dim IA As New dbInventarioAduana(Comm.Connection)
        If IA.HayViejaAduanaGlobal(ID) Then
            Dim VA As New dbventasaduana(Comm.Connection)
            DR = VA.ConsultaTodoReader(ID)
            While DR.Read
                AduanaCol.Add(New InfoAduana(DR("numero"), DR("fecha"), DR("aduana"), DR("iddetalle")))
            End While
            DR.Close()
        Else
            DR = IA.ConsultaAduanaVentaReader(ID)
            While DR.Read
                AduanaCol.Add(New InfoAduana(DR("numero"), DR("fecha"), DR("aduana"), DR("iddetalle")))
            End While
            DR.Close()
        End If
        Dim VI As New dbVentasInventario(Comm.Connection)
        DR = VI.ConsultaReader(ID, False, "0", 0, "0")
        'Dim PrecioTemp As Double
        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                CO += DR("cantidad").ToString + "|"
                CO += DR("tipocantidad") + "|"
                'CO += DR("clave") + "|"
                CO += Trim(Replace(DR("descripcion"), vbCrLf, "")) + "|"
                'If DR("idmoneda") <> 2 Then
                '    CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
                '    CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
                'Else
                CO += Format(DR("precio") / DR("cantidad"), "#0.00####") + "|"
                CO += Format(DR("precio"), "#0.00####") + "|"
                'End If
                For Each ad As InfoAduana In AduanaCol
                    If ad.IdDetalle = DR("idventasinventario") Then
                        CO += Trim(ad.Numero) + "|"
                        CO += Trim(ad.Fecha.Replace("/", "-")) + "|"
                        CO += Trim(ad.Aduana) + "|"
                    End If
                Next
                If DR("predial") <> "" Then
                    CO += Trim(DR("predial")) + "|"
                End If
            End If
        End While
        DR.Close()

        If ISR <> 0 Then
            CO += "ISR|" + Format(TotalISR, "#0.00####") + "|"
        End If
        If IvaRetenido <> 0 Or TotalIvaRetenidoConceptos <> 0 Then
            CO += "IVA|" + Format(TotalIvaRetenido + TotalIvaRetenidoConceptos, "#0.00####") + "|"
        End If
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        'DR = DaIvasRetenido(ID)
        Dim IAnt As Double
        'While DR.Read
        '    If Ivas.Contains(DR("iva").ToString) = False Then
        '        Ivas.Add(DR("ivaretenido"), DR("ivaretenido").ToString)
        '    End If
        '    If IvasImporte.Contains(DR("ivaretenido").ToString) = False Then
        '        'If DR("idmoneda") <> 2 Then
        '        '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
        '        'Else
        '        IvasImporte.Add(DR("precio") * (DR("ivaretenido") / 100), DR("ivaretenido").ToString)
        '        ' End If
        '    Else
        '        IAnt = IvasImporte(DR("ivaretenido").ToString)
        '        IvasImporte.Remove(DR("ivaretenido").ToString)
        '        'If DR("idmoneda") <> 2 Then
        '        '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
        '        'Else
        '        IvasImporte.Add(IAnt + (DR("precio") * (DR("ivaretenido") / 100)), DR("ivaretenido").ToString)
        '        'End If
        '    End If
        'End While
        'DR.Close()
        'For Each I As Double In Ivas
        '    CO += "IVA|"
        '    CO += Format(I, "#0.00") + "|"
        '    CO += Format(IvasImporte(I.ToString), "#0.00") + "|"
        'Next
        'total retenidos
        If ISR <> 0 Or IvaRetenido <> 0 Or TotalIvaRetenidoConceptos <> 0 Then
            CO += Format(TotalISR + TotalIvaRetenido + TotalIvaRetenidoConceptos, "#0.00####") + "|"
        End If


        'Dim Ivas As New Collection
        'Dim IvasImporte As New Collection
        Ivas.Clear()
        IvasImporte.Clear()
        DR = DaIvas(ID)
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                'Else
                'IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                If Alterno = "0" Then
                    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                Else
                    If DR("precio") > 0 Then
                        IvasImporte.Add((DR("precio") - Descuento) * (DR("iva") / 100), DR("iva").ToString)
                    Else
                        IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                    End If
                End If
                ' End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                'Else
                If Alterno = "0" Then
                    IvasImporte.Add(IAnt + DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                Else
                    If DR("precio") > 0 Then
                        IvasImporte.Add(IAnt + (DR("precio") - Descuento) * (DR("iva") / 100), DR("iva").ToString)
                    Else
                        IvasImporte.Add(IAnt + DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                    End If
                End If
                'End If
            End If
        End While
        DR.Close()
        For Each I As Double In Ivas
            CO += "IVA|"
            CO += Format(I, "#0.00####") + "|"
            CO += Format(IvasImporte(I.ToString), "#0.00####") + "|"
        Next

        Ivas.Clear()
        IvasImporte.Clear()
        DR = DaIvasIEPS(ID)
        While DR.Read
            If Ivas.Contains(DR("ieps").ToString) = False Then
                Ivas.Add(DR("ieps"), DR("ieps").ToString)
            End If
            If IvasImporte.Contains(DR("ieps").ToString) = False Then
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                'Else
                IvasImporte.Add(DR("precio") * (DR("ieps") / 100), DR("ieps").ToString)
                ' End If
            Else
                IAnt = IvasImporte(DR("ieps").ToString)
                IvasImporte.Remove(DR("ieps").ToString)
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                'Else
                IvasImporte.Add(IAnt + (DR("precio") * (DR("ieps") / 100)), DR("ieps").ToString)
                'End If
            End If
        End While
        DR.Close()
        For Each I As Double In Ivas
            CO += "IEPS|"
            CO += Format(I, "#0.00####") + "|"
            CO += Format(IvasImporte(I.ToString), "#0.00####") + "|"
        Next

        CO += Format(TotalIva + TotalIEPS, "#0.00####") + "|"


        If ImpLocales.Count > 0 Then
            CO += "1.0|" + Format(TotalRetLocal, "#0.00####") + "|" + Format(TotalTrasLocal, "#0.00####") + "|"
            For Each Im As Implocal In ImpLocales
                If Im.Tipo = 1 Then
                    CO += Im.Nombre + "|" + Format(Im.Tasa, "#0.00####") + "|" + Format(Im.Importe, "#0.00####") + "|"
                Else
                    CO += Im.Nombre + "|" + Format(Im.Tasa, "#0.00####") + "|" + Format(Im.Importe, "#0.00####") + "|"
                End If
            Next
        End If

        Dim IDNotaP As Integer
        Dim NotaP As New dbNotariosPublicos(MySqlcon)
        IDNotaP = NotaP.HayDatosNotarios(pIdVenta)
        If IDNotaP <> 0 Then
            CO += NotaP.CreaCadenaOriginal(IDNotaP)
        End If

        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        Dim en As New Encriptador()
        en.GuardaArchivoTexto("co.txt", CO, System.Text.Encoding.Default)
        Return CO

    End Function




    Public Function CreaXML(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
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
        DaTotal(ID, 2, noNegativos, Alterno)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "version = ""2.0""" + vbCrLf
        XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf

        XMLDoc += "subTotal=""" + Format(Subtototal, "#0.00") + """" + vbCrLf

        XMLDoc += "total=""" + Format(TotalVenta, "#0.00") + """" + vbCrLf
        If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf
        XMLDoc += "descuento=""" + Format(Descuento, "#0.00") + """" + vbCrLf
        XMLDoc += "tipoDeComprobante=""ingreso""" + vbCrLf
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
        Dim VI As New dbVentasInventario(MySqlcon)
        DR = VI.ConsultaReader(ID, False, "0", 0, "0")

        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                XMLDoc += "<Concepto " + vbCrLf
                XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
                XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
                Dim Des As String
                Des = Trim(DR("descripcion"))
                While Des.IndexOf("  ") <> -1
                    Des = Replace(Des, "  ", " ")
                End While
                Des = Replace(Des, vbTab, "")
                XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trim(Des), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
                If DR("idmoneda") <> 2 Then
                    XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
                    XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
                    XMLDoc += "/> " + vbCrLf
                Else
                    XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
                    XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
                    XMLDoc += "/> " + vbCrLf
                End If
            End If
        End While
        DR.Close()

        XMLDoc += "</Conceptos>"

        XMLDoc += "<Impuestos totalImpuestosTrasladados=""" + Format(TotalIva, "#0.00") + """ "
        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00") + """"
        End If
        XMLDoc += ">" + vbCrLf

        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "<Retenciones>" + vbCrLf
            If ISR <> 0 Then
                XMLDoc += "<Retencion impuesto=""ISR""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalISR, "#0.00") + """ />" + vbCrLf
            End If

            If IvaRetenido <> 0 Then
                XMLDoc += "<Retencion impuesto=""IVA""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00") + """ />" + vbCrLf
            End If

            XMLDoc += "</Retenciones>" + vbCrLf

        End If



        XMLDoc += "<Traslados>" + vbCrLf


        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                Else
                    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                Else
                    IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
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

    '**************************************************************************************************
    Public Function CreaXML22(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

        XMLDoc += "<Comprobante " + vbCrLf
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()

        Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdConversion, noNegativos, Alterno)
        Dim Sucursal As New dbSucursales(IdSucursal, Comm.Connection)

        XMLDoc += "version=""2.2""" + vbCrLf
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
        If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        If IdFormadePago = 98 Then
            If Parcialidades <> 1 Then
                XMLDoc += "formaDePago=""Pago en " + Parcialidades.ToString + " parcialidades""" + vbCrLf
            Else
                XMLDoc += "formaDePago=""Pago en parcialidades""" + vbCrLf
            End If
        End If
        'If FP.Tipo = 2 Then
        '    If Parcialidades <> 1 Then
        '        XMLDoc += "formaDePago=""Parcialidad " + Parcialidad.ToString + " de " + Parcialidades.ToString + """" + vbCrLf
        '    Else
        '        XMLDoc += "formaDePago=""Parcialidad " + vbCrLf
        '    End If
        'End If
        If IdFormadePago <> 98 Then
            XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf
        End If
        'XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf

        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf
        XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf
        XMLDoc += "subTotal=""" + Format(Subtototal, "#0.00") + """" + vbCrLf
        XMLDoc += "descuento=""" + Format(Descuento, "#0.00") + """" + vbCrLf
        'Nuevo
        If IdConversion <> 2 Then
            XMLDoc += "TipoCambio=""" + Format(TipodeCambio, "#0.00") + """" + vbCrLf
            Dim Moneda As New dbMonedas(IdConversion, Comm.Connection)
            XMLDoc += "Moneda=""" + Moneda.Abreviatura + """" + vbCrLf
        Else
            XMLDoc += "Moneda=""MXN""" + vbCrLf
        End If
        XMLDoc += "total=""" + Format(TotalVenta, "#0.00") + """" + vbCrLf

        Dim CP As New dbVentasCartaPorte(ID, MySqlcon)
        If CP.Origen = "Nohay" Then
            XMLDoc += "tipoDeComprobante=""ingreso""" + vbCrLf
        Else
            XMLDoc += "tipoDeComprobante=""traslado""" + vbCrLf
        End If


        'Nuevo

        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        If IdFormadePago <> 98 Then
            XMLDoc += "metodoDePago=""" + FP.Nombre + """" + vbCrLf
        Else
            XMLDoc += "metodoDePago=""No aplica""" + vbCrLf
        End If
        If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """" + vbCrLf
        'Else
        'XMLDoc += "metodoDePago=""No identificado""" + vbCrLf
        'End If
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad2 + "," + Sucursal.Estado2 + """" + vbCrLf
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad + "," + Sucursal.Estado + """" + vbCrLf
        End If

        If FP.Tipo = 2 Then
            ObtenerFacturaOriginal(IdVentaOrigen)
            XMLDoc += "FolioFiscalOrig=""" + FolioOrigen.ToString + """" + vbCrLf
            XMLDoc += "SerieFolioFiscalOrig=""" + SerieOrigen + """" + vbCrLf
            XMLDoc += "FechaFolioFiscalOrig=""" + FechaOrigen + """" + vbCrLf
            XMLDoc += "MontoFolioFiscalOrig=""" + Format(MontoOrigen, "0.00") + """" + vbCrLf
        End If


        '-------------------------
        If ImpLocales.Count = 0 Then
            XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
            XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv22.xsd""" + vbCrLf
            XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf
        Else
            XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
            XMLDoc += "xmlns:implocal=""http://www.sat.gob.mx/implocal""" + vbCrLf
            XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv22.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd""" + vbCrLf
            XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf
        End If
        XMLDoc += ">"

        XMLDoc += "<Emisor rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf

        XMLDoc += "<DomicilioFiscal " + vbCrLf
        If Sucursal.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
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
        Comm.CommandText = "select ifnull((select id from tblventasexpedicion where documento=0 and iddocumento=" + ID.ToString + "),0)"
        Dim HayExp As Integer
        HayExp = Comm.ExecuteScalar
        If HayExp > 0 Then
            'If Sucursal.Pais2 <> "" Then
            XMLDoc += "<ExpedidoEn  " + vbCrLf
            Comm.CommandText = "select ifnull((select calexp from tblventasexpedicion where id=" + HayExp.ToString + "),0)"
            XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Comm.ExecuteScalar, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            Comm.CommandText = "select ifnull((select numexp from tblventasexpedicion where id=" + HayExp.ToString + "),0)"
            XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Comm.ExecuteScalar, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            'If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            'If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            Comm.CommandText = "select ifnull((select lugarexp from tblventasexpedicion where id=" + HayExp.ToString + "),0)"
            XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Comm.ExecuteScalar, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            'If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            'If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            'If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace("MÉXICO", "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            'If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            XMLDoc += "/>" + vbCrLf
            'End If
        Else
            If Sucursal.Pais2 <> "" Then
                XMLDoc += "<ExpedidoEn  " + vbCrLf
                If Sucursal.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
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

        Dim AduanaCol As New Collection
        Dim AduanaCont As Integer
        Dim AduanaXML As String
        Dim VA As New dbventasaduana(MySqlcon)
        DR = VA.ConsultaTodoReader(ID)
        While DR.Read
            AduanaCol.Add(New InfoAduana(DR("numero"), DR("fecha"), DR("aduana"), DR("iddetalle")))
        End While
        DR.Close()

        Dim VI As New dbVentasInventario(MySqlcon)
        DR = VI.ConsultaReader(ID, False, "0", 0, "0")

        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                XMLDoc += "<Concepto " + vbCrLf
                XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
                XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
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
                AduanaCont = 0
                AduanaXML = ""
                For Each ad As InfoAduana In AduanaCol
                    If ad.IdDetalle = DR("idventasinventario") Then
                        AduanaXML += "<InformacionAduanera "
                        AduanaXML += "numero=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trim(ad.Numero), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
                        AduanaXML += "fecha=""" + Trim(ad.Fecha.Replace("/", "-")) + """ "
                        AduanaXML += "aduana=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trim(ad.Aduana), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ />"
                        AduanaCont += 1
                    End If
                Next
                If AduanaCont = 0 Then
                    XMLDoc += "/> " + vbCrLf
                Else
                    XMLDoc += ">" + AduanaXML + "</Concepto>"
                End If
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
        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00") + """"
        End If
        XMLDoc += ">" + vbCrLf

        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "<Retenciones>" + vbCrLf
            If ISR <> 0 Then
                XMLDoc += "<Retencion impuesto=""ISR""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalISR, "#0.00") + """ />" + vbCrLf
            End If

            If IvaRetenido <> 0 Then
                XMLDoc += "<Retencion impuesto=""IVA""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00") + """ />" + vbCrLf
            End If

            XMLDoc += "</Retenciones>" + vbCrLf

        End If



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
        If ImpLocales.Count > 0 Then
            XMLDoc += "<Complemento>" + vbCrLf
            XMLDoc += "<implocal:ImpuestosLocales version=""1.0"" TotaldeRetenciones=""" + Format(TotalRetLocal, "#0.00") + """ TotaldeTraslados=""" + Format(TotalTrasLocal, "#0.00") + """>" + vbCrLf
            For Each Im As Implocal In ImpLocales
                If Im.Tipo = 1 Then
                    XMLDoc += "<implocal:RetencionesLocales ImpLocRetenido=""" + Replace(Replace(Replace(Replace(Replace(Im.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TasadeRetencion=""" + Format(Im.Tasa, "#0.00") + """ Importe=""" + Format(Im.Importe, "#0.00") + """ />" + vbCrLf
                Else
                    XMLDoc += "<implocal:TrasladosLocales ImpLocTrasladado=""" + Replace(Replace(Replace(Replace(Replace(Im.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TasadeTraslado=""" + Format(Im.Tasa, "#0.00") + """ Importe=""" + Format(Im.Importe, "#0.00") + """ />" + vbCrLf
                End If
            Next
            XMLDoc += "</implocal:ImpuestosLocales>" + vbCrLf
            XMLDoc += "</Complemento>" + vbCrLf
        End If
        XMLDoc += "</Comprobante>"


        Return XMLDoc

    End Function

    '**************************************CFDi***********************************************************++


    Public Function CreaXMLi(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

        XMLDoc += "<cfdi:Comprobante " + vbCrLf
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, 2, noNegativos, Alterno)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "version = ""3.0""" + vbCrLf
        XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf

        XMLDoc += "subTotal=""" + Format(Subtototal, "#0.00") + """" + vbCrLf

        XMLDoc += "total=""" + Format(TotalVenta, "#0.00") + """" + vbCrLf
        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf
        XMLDoc += "descuento=""" + Format(Descuento, "#0.00") + """" + vbCrLf
        XMLDoc += "tipoDeComprobante=""ingreso""" + vbCrLf
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
        Dim VI As New dbVentasInventario(MySqlcon)
        DR = VI.ConsultaReader(ID, False, "0", 0, "0")

        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                XMLDoc += "<cfdi:Concepto " + vbCrLf
                XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
                XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
                Dim Des As String
                Des = Trim(DR("descripcion"))
                While Des.IndexOf("  ") <> -1
                    Des = Replace(Des, "  ", " ")
                End While
                Des = Replace(Des, vbTab, "")
                XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(Des, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
                If DR("idmoneda") <> 2 Then
                    XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
                    XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
                    XMLDoc += "/> " + vbCrLf
                Else
                    XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
                    XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
                    XMLDoc += "/> " + vbCrLf
                End If
            End If
        End While
        DR.Close()

        XMLDoc += "</cfdi:Conceptos>"

        XMLDoc += "<cfdi:Impuestos totalImpuestosTrasladados=""" + Format(TotalIva, "#0.00") + """ "
        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00") + """"
        End If
        XMLDoc += ">" + vbCrLf

        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "<cfdi:Retenciones>" + vbCrLf
            If ISR <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""ISR""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalISR, "#0.00") + """ />" + vbCrLf
            End If

            If IvaRetenido <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""IVA""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00") + """ />" + vbCrLf
            End If

            XMLDoc += "</cfdi:Retenciones>" + vbCrLf

        End If



        XMLDoc += "<cfdi:Traslados>" + vbCrLf


        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                Else
                    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                Else
                    IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
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

    Public Function CreaXMLi32(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        Dim IAnt As Double
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>"

        XMLDoc += "<cfdi:Comprobante "
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()
        Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdConversion, noNegativos, Alterno)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        XMLDoc += "version=""3.2"" "
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        XMLDoc += "folio=""" + Folio.ToString + """ "
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """ "
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """ "

        'XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf
        If IdFormadePago = 98 Then
            If Parcialidades <> 1 Then
                XMLDoc += "formaDePago=""Pago en " + Parcialidades.ToString + " parcialidades"" "
            Else
                XMLDoc += "formaDePago=""Pago en parcialidades"" "
            End If
        End If
        'If FP.Tipo = 2 Then
        '    If Parcialidades <> 1 Then
        '        XMLDoc += "formaDePago=""Parcialidad " + Parcialidad.ToString + " de " + Parcialidades.ToString + """" + vbCrLf
        '    Else
        '        XMLDoc += "formaDePago=""Parcialidad " + vbCrLf
        '    End If
        'End If
        If IdFormadePago <> 98 Then
            XMLDoc += "formaDePago=""Pago en una sola exhibición"" "
        End If

        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """ "
        XMLDoc += "certificado=""" + en.Certificado64 + """ "
        If Alterno = "0" Then
            XMLDoc += "subTotal=""" + Format(Subtototal, "#0.00####") + """ "
        Else
            XMLDoc += "subTotal=""" + Format(Subtototal + Descuento, "#0.00####") + """ "
        End If

        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf

        XMLDoc += "descuento=""" + Format(Descuento + DescuentoG2, "#0.00####") + """ "

        'Tipo deCambio nuevo
        If IdConversion <> 2 Then
            XMLDoc += "TipoCambio=""" + Format(TipodeCambio, "#0.00####") + """ "
            Dim Moneda As New dbMonedas(IdConversion, Comm.Connection)
            XMLDoc += "Moneda=""" + Moneda.Abreviatura + """ "
        Else
            XMLDoc += "Moneda=""MXN"" "
        End If
        XMLDoc += "total=""" + Format(TotalVenta, "#0.00####") + """ "


        Dim CP As New dbVentasCartaPorte(ID, MySqlcon)
        If CP.Origen = "Nohay" Then
            XMLDoc += "tipoDeComprobante=""ingreso"" "
        Else
            XMLDoc += "tipoDeComprobante=""traslado"" "
        End If
        'XMLDoc += "tipoDeComprobante=""ingreso"" "
        'Metodo de pago lugar exibibicion nuevo

        ''If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        If IdFormadePago <> 98 Then
            XMLDoc += "metodoDePago=""" + FP.Nombre + """ "
        Else
            XMLDoc += "metodoDePago=""No aplica"" "
        End If
        If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """ "
        'Else
        'XMLDoc += "metodoDePago=""No identificado""" + vbCrLf
        'End If
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad2 + "," + Sucursal.Estado2 + """ "
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad + "," + Sucursal.Estado + """ "
        End If
        If FP.Tipo = 2 Then
            ObtenerFacturaOriginal(IdVentaOrigen)
            If FolioUUIDOrigen = "" Then
                XMLDoc += "FolioFiscalOrig=""" + FolioOrigen.ToString + """ "
            Else
                XMLDoc += "FolioFiscalOrig=""" + FolioUUIDOrigen + """ "
            End If
            If SerieOrigen <> "" Then
                XMLDoc += "SerieFolioFiscalOrig=""" + SerieOrigen + """ "
            End If
            XMLDoc += "FechaFolioFiscalOrig=""" + FechaOrigen + """ "
            XMLDoc += "MontoFolioFiscalOrig=""" + Format(MontoOrigen, "0.00####") + """ "
        End If
        'XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
        'XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        'XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf
        '++++++++++++++++++++++++++++
        'XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:ecb=""http://www.sat.gob.mx/ecb"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:implocal=""http://www.sat.gob.mx/implocal"" xmlns:bfa2=""http://www.buzonfiscal.com/ns/addenda/bf/2""" + vbCrLf
        'XMLDoc += "xmlns:terceros=""http://www.sat.gob.mx/terceros"" xmlns:detallista=""http://www.sat.gob.mx/detallista"" xmlns:psgecfd=""http://www.sat.gob.mx/psgecfd"" xmlns:ecc=""http://www.sat.gob.mx/ecc"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital""" + vbCrLf
        'XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/TimbreFiscalDigital/TimbreFiscalDigital.xsd http://www.sat.gob.mx/detallista " + _
        '"http://www.sat.gob.mx/sitio_internet/cfd/detallista/detallista.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd " + _
        '"http://www.sat.gob.mx/ecc http://www.sat.gob.mx/sitio_internet/cfd/ecc/ecc.xsd http://www.buzonfiscal.com/ns/addenda/bf/2 http://www.buzonfiscal.com/schema/xsd/Addenda_BF_v2.2.xsd http://www.sat.gob.mx/psgecfd http://www.sat.gob.mx/sitio_internet/cfd/psgecfd/psgecfd.xsd http://www.sat.gob.mx/ecb " + _
        '"http://www.sat.gob.mx/sitio_internet/cfd/ecb/ecb.xsd http://www.sat.gob.mx/terceros http://www.sat.gob.mx/sitio_internet/cfd/terceros/terceros11.xsd http://www.sat.gob.mx/donat http://www.sat.gob.mx/sitio_internet/cfd/donat/donat11.xsd""" + vbCrLf

        ' ''XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:implocal=""http://www.sat.gob.mx/implocal"" "
        ' ''XMLDoc += "xmlns:terceros=""http://www.sat.gob.mx/terceros"" xmlns:detallista=""http://www.sat.gob.mx/detallista"" xmlns:psgecfd=""http://www.sat.gob.mx/psgecfd"" xmlns:ecc=""http://www.sat.gob.mx/ecc"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital"" "
        ' ''XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/TimbreFiscalDigital/TimbreFiscalDigital.xsd http://www.sat.gob.mx/detallista " + _
        ' ''"http://www.sat.gob.mx/sitio_internet/cfd/detallista/detallista.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd " + _
        ' ''"http://www.sat.gob.mx/ecc http://www.sat.gob.mx/sitio_internet/cfd/ecc/ecc.xsd http://www.sat.gob.mx/psgecfd http://www.sat.gob.mx/sitio_internet/cfd/psgecfd/psgecfd.xsd " + _
        ' ''"http://www.sat.gob.mx/terceros http://www.sat.gob.mx/sitio_internet/cfd/terceros/terceros11.xsd http://www.sat.gob.mx/donat http://www.sat.gob.mx/sitio_internet/cfd/donat/donat11.xsd"" "


        'XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:implocal=""http://www.sat.gob.mx/implocal"" "
        'XMLDoc += "xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital"" "
        'XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/TimbreFiscalDigital/TimbreFiscalDigital.xsd " + _
        '"http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd"" "
        XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" "
        If ImpLocales.Count > 0 Then
            XMLDoc += "xmlns:implocal=""http://www.sat.gob.mx/implocal"" "
        End If
        Dim IDNotaP As Integer
        Dim NotaP As New dbNotariosPublicos(MySqlcon)
        IDNotaP = NotaP.HayDatosNotarios(pIdVenta)
        If IDNotaP <> 0 Then
            XMLDoc += "xmlns:notariospublicos=""http://www.sat.gob.mx/notariospublicos"" "
            'XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd http://www.sat.gob.mx/notariospublicos http://www.sat.gob.mx/sitio_internet/cfd/notariospublicos/notariospublicos.xsd"" "
            'Else
            '   XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd"" "
        End If
        XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd"
        If ImpLocales.Count > 0 Then
            XMLDoc += " http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd"
        End If
        If IDNotaP <> 0 Then
            XMLDoc += " http://www.sat.gob.mx/notariospublicos http://www.sat.gob.mx/sitio_internet/cfd/notariospublicos/notariospublicos.xsd"
        End If
        XMLDoc += """ "
        ' xsi:schemaLocation=""http://www.sat.gob.mx/notariospublicos http://www.sat.gob.mx/sitio_internet/cfd/notariospublicos/notariospublicos.xsd""

        'If ImpLocales.Count > 0 Then

        'XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv22.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd"""
        'XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance"""
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

        Comm.CommandText = "select ifnull((select id from tblventasexpedicion where documento=0 and iddocumento=" + ID.ToString + "),0)"
        Dim HayExp As Integer
        HayExp = Comm.ExecuteScalar
        If HayExp > 0 Then
            'If Sucursal.Pais2 <> "" Then
            XMLDoc += "<cfdi:ExpedidoEn  "
            Comm.CommandText = "select ifnull((select calexp from tblventasexpedicion where id=" + HayExp.ToString + "),0)"
            XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Comm.ExecuteScalar, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            Comm.CommandText = "select ifnull((select numexp from tblventasexpedicion where id=" + HayExp.ToString + "),0)"
            XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Comm.ExecuteScalar, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            'If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            'If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            Comm.CommandText = "select ifnull((select lugarexp from tblventasexpedicion where id=" + HayExp.ToString + "),0)"
            XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Comm.ExecuteScalar, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            'If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            'If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            'If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace("MÉXICO", "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            'If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            XMLDoc += "/>"
            'End If
        Else
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
                XMLDoc += "/>"
            End If
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
            If Cliente.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        XMLDoc += "/>"

        XMLDoc += "</cfdi:Receptor>"

        XMLDoc += "<cfdi:Conceptos>"

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader

        Dim AduanaCol As New Collection
        Dim AduanaCont As Integer
        Dim AduanaXML As String
        Dim PredialXML As String
        Dim IA As New dbInventarioAduana(Comm.Connection)
        Dim VA As New dbventasaduana(Comm.Connection)
        If IA.HayViejaAduanaGlobal(ID) Then
            DR = VA.ConsultaTodoReader(ID)
            While DR.Read
                AduanaCol.Add(New InfoAduana(DR("numero"), DR("fecha"), DR("aduana"), DR("iddetalle")))
            End While
            DR.Close()
        Else
            DR = IA.ConsultaAduanaVentaReader(ID)
            While DR.Read
                AduanaCol.Add(New InfoAduana(DR("numero"), DR("fecha"), DR("aduana"), DR("iddetalle")))
            End While
            DR.Close()
        End If


        Dim VI As New dbVentasInventario(MySqlcon)
        DR = VI.ConsultaReader(ID, False, "0", 0, "0")

        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                XMLDoc += "<cfdi:Concepto "
                XMLDoc += "cantidad=""" + DR("cantidad").ToString + """ "
                XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
                Dim Des As String
                Des = Trim(Replace(DR("descripcion"), vbCrLf, ""))
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
                XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00####") + """ "
                XMLDoc += "importe=""" + Format(DR("precio"), "#0.00####") + """ "
                AduanaCont = 0
                AduanaXML = ""
                For Each ad As InfoAduana In AduanaCol
                    If ad.IdDetalle = DR("idventasinventario") Then
                        AduanaXML += "<cfdi:InformacionAduanera "
                        AduanaXML += "numero=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trim(ad.Numero), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
                        AduanaXML += "fecha=""" + Trim(ad.Fecha.Replace("/", "-")) + """ "
                        AduanaXML += "aduana=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trim(ad.Aduana), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ />"
                        AduanaCont += 1
                    End If
                Next
                PredialXML = ""
                If DR("predial") <> "" Then
                    PredialXML = "<cfdi:CuentaPredial numero=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trim(DR("predial")), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ />"
                End If
                If AduanaCont = 0 And DR("predial") = "" Then
                    XMLDoc += "/> "
                Else
                    XMLDoc += ">" + AduanaXML + PredialXML + "</cfdi:Concepto>"
                End If
                'End If


            End If
        End While
        DR.Close()

        XMLDoc += "</cfdi:Conceptos>"

        XMLDoc += "<cfdi:Impuestos totalImpuestosTrasladados=""" + Format(TotalIva + TotalIEPS, "#0.00####") + """"
        If ISR <> 0 Or IvaRetenido <> 0 Or TotalIvaRetenidoConceptos <> 0 Then
            XMLDoc += " totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido + TotalIvaRetenidoConceptos, "#0.00####") + """ "
        End If
        XMLDoc += ">"

        'Ivas.Clear()
        'IvasImporte.Clear()
        'DR = DaIvas(ID)
        'While DR.Read
        '    If Ivas.Contains(DR("iva").ToString) = False Then
        '        Ivas.Add(DR("iva"), DR("iva").ToString)
        '    End If
        '    If IvasImporte.Contains(DR("iva").ToString) = False Then
        '        'If DR("idmoneda") <> 2 Then
        '        '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
        '        'Else
        '        IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
        '        'End If
        '    Else
        '        IAnt = IvasImporte(DR("iva").ToString)
        '        IvasImporte.Remove(DR("iva").ToString)
        '        'If DR("idmoneda") <> 2 Then
        '        '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
        '        'Else
        '        IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
        '        'End If

        '    End If
        'End While
        'DR.Close()


        If ISR <> 0 Or IvaRetenido <> 0 Or TotalIvaRetenidoConceptos <> 0 Then
            XMLDoc += "<cfdi:Retenciones>"
            If ISR <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""ISR"" "
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalISR, "#0.00####") + """/>"
            End If

            If IvaRetenido <> 0 Or TotalIvaRetenidoConceptos <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""IVA"" "
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalIvaRetenido + TotalIvaRetenidoConceptos, "#0.00####") + """/>"
            End If


            'For Each I As Double In Ivas

            '    XMLDoc += "<cfdi:Traslado impuesto=""IVA"" "
            '    XMLDoc += "tasa=""" + Format(I, "#0.00") + """ "
            '    XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00") + """ />"

            'Next

            XMLDoc += "</cfdi:Retenciones>"

        End If
        XMLDoc += "<cfdi:Traslados>"
        Ivas.Clear()
        IvasImporte.Clear()
        DR = DaIvas(ID)
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                'Else
                If Alterno = "0" Then
                    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                Else
                    If DR("precio") > 0 Then
                        IvasImporte.Add((DR("precio") - Descuento) * (DR("iva") / 100), DR("iva").ToString)
                    Else
                        IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                    End If
                End If
                'End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                'Else
                'IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                If Alterno = "0" Then
                    IvasImporte.Add(IAnt + DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                Else
                    If DR("precio") > 0 Then
                        IvasImporte.Add(IAnt + (DR("precio") - Descuento) * (DR("iva") / 100), DR("iva").ToString)
                    Else
                        IvasImporte.Add(IAnt + DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                    End If
                End If
                'End If

            End If
        End While
        DR.Close()
        For Each I As Double In Ivas

            XMLDoc += "<cfdi:Traslado impuesto=""IVA"" "
            XMLDoc += "tasa=""" + Format(I, "#0.00####") + """ "
            XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00####") + """ />"

        Next

        Ivas.Clear()
        IvasImporte.Clear()
        DR = DaIvasIEPS(ID)
        While DR.Read
            If Ivas.Contains(DR("ieps").ToString) = False Then
                Ivas.Add(DR("ieps"), DR("ieps").ToString)
            End If
            If IvasImporte.Contains(DR("ieps").ToString) = False Then
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                'Else
                IvasImporte.Add(DR("precio") * (DR("ieps") / 100), DR("ieps").ToString)
                'End If
            Else
                IAnt = IvasImporte(DR("ieps").ToString)
                IvasImporte.Remove(DR("ieps").ToString)
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                'Else
                IvasImporte.Add(IAnt + (DR("precio") * (DR("ieps") / 100)), DR("ieps").ToString)
                'End If

            End If
        End While
        DR.Close()
        For Each I As Double In Ivas

            XMLDoc += "<cfdi:Traslado impuesto=""IEPS"" "
            XMLDoc += "tasa=""" + Format(I, "#0.00####") + """ "
            XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00####") + """ />"

        Next


        XMLDoc += "</cfdi:Traslados>"
        XMLDoc += "</cfdi:Impuestos>"

        If ImpLocales.Count > 0 Then
            XMLDoc += "<cfdi:Complemento>" + vbCrLf
            XMLDoc += "<implocal:ImpuestosLocales version=""1.0"" TotaldeRetenciones=""" + Format(TotalRetLocal, "#0.00####") + """ TotaldeTraslados=""" + Format(TotalTrasLocal, "#0.00####") + """>" + vbCrLf
            For Each Im As Implocal In ImpLocales
                If Im.Tipo = 1 Then
                    XMLDoc += "<implocal:RetencionesLocales ImpLocRetenido=""" + Replace(Replace(Replace(Replace(Replace(Im.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TasadeRetencion=""" + Format(Im.Tasa, "#0.00####") + """ Importe=""" + Format(Im.Importe, "#0.00####") + """ />" + vbCrLf
                Else
                    XMLDoc += "<implocal:TrasladosLocales ImpLocTrasladado=""" + Replace(Replace(Replace(Replace(Replace(Im.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ TasadeTraslado=""" + Format(Im.Tasa, "#0.00####") + """ Importe=""" + Format(Im.Importe, "#0.00####") + """ />" + vbCrLf
                End If
            Next
            XMLDoc += "</implocal:ImpuestosLocales>" + vbCrLf
            XMLDoc += "</cfdi:Complemento>" + vbCrLf
        End If

        If IDNotaP <> 0 Then
            XMLDoc += "<cfdi:Complemento>" + vbCrLf
            XMLDoc += NotaP.CreaXML(IDNotaP)
            XMLDoc += "</cfdi:Complemento>" + vbCrLf
        End If

        XMLDoc += "</cfdi:Comprobante>"


        Return XMLDoc

    End Function
    Public Function DaIvas(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tblventasinventario.iva,tblventasinventario.precio,tblventasinventario.idmoneda,tblventas.tipodecambio from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa where tblventasinventario.idventa=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
    Public Function DaIvasIEPS(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tblventasinventario.ieps,tblventasinventario.precio,tblventasinventario.idmoneda,tblventas.tipodecambio from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa where tblventasinventario.idventa=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
    Public Function DaIvasRetenido(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tblventasinventario.ivaretenido,tblventasinventario.precio,tblventasinventario.idmoneda,tblventas.tipodecambio from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa where tblventasinventario.idventa=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
    'Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidinventario As Integer, ByVal pidclasificacion1 As Integer, ByVal pidclasificacion2 As Integer, ByVal pidclasificacion3 As Integer, ByVal pSoloCanceladas As Boolean, ByVal pSerie As String) As DataView
    '    Dim DS As New DataSet
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,round(tblventas.total,2),round(tblventas.total*tblventas.tipodecambio,2)) as total,if(tblventas.idconversion=2,round(tblventas.totalapagar,2),round(tblventas.totalapagar*tblventas.tipodecambio,2)) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2) as cantidad,tblventasinventario.descripcion,round(tblventasinventario.precio,2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    Else
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,round(tblventas.total,2) as total,round(tblventas.totalapagar,2) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2),tblventasinventario.descripcion,round(tblventasinventario.precio,2),0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido  " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    End If
    '    If pSoloCanceladas Then
    '        Comm.CommandText += " and tblventas.estado=4"
    '    Else
    '        Comm.CommandText += " and tblventas.estado=3"
    '    End If
    '    'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdCliente > 0 Then
    '        Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
    '    End If
    '    If pidVendedor > 0 Then
    '        Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
    '    End If
    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
    '    End If
    '    If pidinventario > 1 Then
    '        Comm.CommandText += " and tblventasinventario.idinventario=" + pidinventario.ToString
    '    Else
    '        If pidclasificacion1 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion=" + pidclasificacion1.ToString
    '        End If
    '        If pidclasificacion2 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidclasificacion2.ToString
    '        End If
    '        If pidclasificacion3 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidclasificacion3.ToString
    '        End If
    '    End If
    '    If pSerie <> "" Then
    '        Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
    '    End If
    '    Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblventas")
    '    'DS.WriteXmlSchema("tblventas.xml")
    '    Return DS.Tables("tblventas").DefaultView
    'End Function
    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidinventario As Integer, ByVal pidclasificacion1 As Integer, ByVal pidclasificacion2 As Integer, ByVal pidclasificacion3 As Integer, ByVal pSoloCanceladas As Boolean, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal pSoloActivas As Boolean, pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,round(tblventas.total,2),round(tblventas.total*tblventas.tipodecambio,2)) as total,if(tblventas.idconversion=2,round(tblventas.totalapagar,2),round(tblventas.totalapagar*tblventas.tipodecambio,2)) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2) as cantidad,tblventasinventario.descripcion,round(tblventasinventario.precio,2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido,tblventasinventario.ieps,tblventasinventario.ivaretenido retcon " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor  where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,round(tblventas.total,2) as total,round(tblventas.totalapagar,2) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2),tblventasinventario.descripcion,round(tblventasinventario.precio,2),0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido,tblventasinventario.ieps,tblventasinventario.ivaretenido retcon  " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor  where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If
        If pSoloActivas Then
            Comm.CommandText += " and tblventas.estado=3"
        Else
            If pSoloCanceladas Then
                Comm.CommandText += " and tblventas.estado=4"
            Else
                Comm.CommandText += " and (tblventas.estado=3 or tblventas.estado=4)"
            End If
        End If

        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and (tblclientes.zona='" + pZona2.ToString + "' or tblclientes.zona2='" + pZona2.ToString() + "')"
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        If pidinventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pidinventario.ToString
        Else
            If pidclasificacion1 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidclasificacion1.ToString
            End If
            If pidclasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidclasificacion2.ToString
            End If
            If pidclasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidclasificacion3.ToString
            End If
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio,tblventas.idventa"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventas.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Function ReporteCanceladas(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidinventario As Integer, ByVal pidclasificacion1 As Integer, ByVal pidclasificacion2 As Integer, ByVal pidclasificacion3 As Integer, ByVal pSoloCanceladas As Boolean, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal pSoloActivas As Boolean, pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,round(tblventas.total,2),round(tblventas.total*tblventas.tipodecambio,2)) as total,if(tblventas.idconversion=2,round(tblventas.totalapagar,2),round(tblventas.totalapagar*tblventas.tipodecambio,2)) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2) as cantidad,tblventasinventario.descripcion,round(tblventasinventario.precio,2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido,tblventasinventario.ieps,tblventasinventario.ivaretenido retcon " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor  where tblformasdepago.tipo<>2 and tblventas.fechacancelado>='" + pFecha1 + "' and tblventas.fechacancelado<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,round(tblventas.total,2) as total,round(tblventas.totalapagar,2) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2),tblventasinventario.descripcion,round(tblventasinventario.precio,2),0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido,tblventasinventario.ieps,tblventasinventario.ivaretenido retcon  " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor  where tblformasdepago.tipo<>2 and tblventas.fechacancelado>='" + pFecha1 + "' and tblventas.fechacancelado<='" + pFecha2 + "'"
        End If
        'If pSoloActivas Then
        'Comm.CommandText += " and tblventas.estado=3"
        'Else
        'If pSoloCanceladas Then
        Comm.CommandText += " and tblventas.estado=4"
        'Else
        'Comm.CommandText += " and (tblventas.estado=3 or tblventas.estado=4)"
        'End If
        'End If

        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and (tblclientes.zona='" + pZona2.ToString + "' or tblclientes.zona2='" + pZona2.ToString() + "')"
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        If pidinventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pidinventario.ToString
        Else
            If pidclasificacion1 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidclasificacion1.ToString
            End If
            If pidclasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidclasificacion2.ToString
            End If
            If pidclasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidclasificacion3.ToString
            End If
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio,tblventas.idventa"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventas.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    'Public Function ReporteIvas(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidinventario As Integer, ByVal pidclasificacion1 As Integer, ByVal pidclasificacion2 As Integer, ByVal pidclasificacion3 As Integer, ByVal pSoloCanceladas As Boolean) As DataView
    '    Dim DS As New DataSet
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    Else
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    End If
    '    If pSoloCanceladas Then
    '        Comm.CommandText += " and tblventas.estado=4"
    '    Else
    '        Comm.CommandText += " and tblventas.estado=3"
    '    End If
    '    'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdCliente > 0 Then
    '        Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
    '    End If
    '    If pidVendedor > 0 Then
    '        Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
    '    End If
    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
    '    End If
    '    If pidinventario > 1 Then
    '        Comm.CommandText += " and tblventasinventario.idinventario=" + pidinventario.ToString
    '    Else
    '        If pidclasificacion1 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion=" + pidclasificacion1.ToString
    '        End If
    '        If pidclasificacion2 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidclasificacion2.ToString
    '        End If
    '        If pidclasificacion3 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidclasificacion3.ToString
    '        End If
    '    End If
    '    Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblventas")
    '    DS.WriteXmlSchema("repventasivas.xml")
    '    Return DS.Tables("tblventas").DefaultView
    'End Function
    Public Function ReporteUtilidad(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidinventario As Integer, ByVal pidclasificacion1 As Integer, ByVal pidclasificacion2 As Integer, ByVal pidclasificacion3 As Integer, ByVal ptipoCosteo As Byte, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer, pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        'If ptipoCosteo = 2 Then
        'If pMostrarEnPesos = 0 Then
        Comm.CommandTimeout = 3000
        If ptipoCosteo = 1 Then
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventasinventario.cantidad,if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio) as precio,tblventasinventario.iva,tblclientes.clave,tblclientes.nombre,tblinventario.clave as clavei,tblinventario.nombre as nombrei,tblventas.fecha," + _
    "ifnull((select costo from tblinventariocostoh where idinventario=tblventasinventario.idinventario and fecha<=tblventas.fecha order by fecha desc limit 1),0) as costo,tblventasinventario.ieps,tblventasinventario.ivaretenido ivaret " + _
    "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo<>2 and tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventasinventario.cantidad,if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio) as precio,tblventasinventario.iva,tblclientes.clave,tblclientes.nombre,tblinventario.clave as clavei,tblinventario.nombre as nombrei,tblventas.fecha," + _
    "spdaultimocostoinv(tblventasinventario.idinventario) as costo,tblventasinventario.ieps,tblventasinventario.ivaretenido ivaret " + _
    "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo<>2 and tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio," + _
        '"if(tblventasinventario.idinventario>1,if(tblventas.idconversion=2,spsacacostoarticulo(tblventasinventario.idinventario," + ptipoCosteo.ToString + ",tblinventario.contenido,tblventas.tipodecambio),spsacacostoarticulo(tblventasinventario.idinventario," + ptipoCosteo.ToString + ",tblinventario.contenido,tblventas.tipodecambio)*tblventas.tipodecambio),0)*tblventasinventario.cantidad as costoinv," + _
        '"if(tblventasinventario.idvariante>1,if(tblventas.idconversion=2,spsacacostoproducto((select idproducto from tblproductos where idproducto=tblproductosvariantes.idproducto)," + ptipoCosteo.ToString + "),spsacacostoproducto((select idproducto from tblproductos where idproducto=tblproductosvariantes.idproducto)," + ptipoCosteo.ToString + ")*tblventas.tipodecambio),0)*tblventasinventario.cantidad as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre " + _
        '"from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        'Else
        '   Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio," + _
        '   "if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + ptipoCosteo.ToString + ",tblinventario.contenido,tblventas.tipodecambio),0)*tblventasinventario.cantidad as costoinv," + _
        '   "if(tblventasinventario.idvariante>1,spsacacostoproducto((select idproducto from tblproductos where idproducto=tblproductosvariantes.idproducto)," + ptipoCosteo.ToString + "),0)*tblventasinventario.cantidad as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre " + _
        '   "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        'End If
        'Else
        'If  = 0 Then
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio," + _
        '"if(tblventasinventario.idinventario>1,ifnull((select costo from tblinventariocostoh where idinventario=tblventasinventario.idinventario and fecha<=tblventas.fecha order by fecha desc limit 1),0)*tblventasinventario.cantidad,0) as costoinv," + _
        '"if(tblventasinventario.idvariante>1,0,0)*tblventasinventario.cantidad as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre " + _
        '"from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        'Else
        '   Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio," + _
        '   "if(tblventasinventario.idinventario>1,ifnull((select costo from tblinventariocostoh where idinventario=tblventasinventario.idinventario and fecha<=tblventas.fecha order by fecha desc limit 1),0)*tblventasinventario.cantidad,0) as costoinv," + _
        '   "if(tblventasinventario.idvariante>1,tblventasinventario.cantidad,0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre " + _
        '   "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        'End If
        'End If
        'Comm.CommandText += " and tblinventario.inventariable=1"
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and tblclientes.zona='" + pZona2.ToString() + "' or tblclientes.zona2='" + pZona2.ToString() + "'"
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        If pidinventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pidinventario.ToString
        Else
            If pidclasificacion1 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidclasificacion1.ToString
            End If
            If pidclasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidclasificacion2.ToString
            End If
            If pidclasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidclasificacion3.ToString
            End If
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventascosto")
        'DS.WriteXmlSchema("tblventascosto.xml")
        Return DS.Tables("tblventascosto").DefaultView
        'select fecha,tblclientes.creditodias,if(pfecha<=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),),tblventas.credito from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente
    End Function
    Public Function ReportePorTipodePagob(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pIdVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pIdVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        'If pidInventario > 1 Then
        '    Comm.CommandText += " and tblventasinventario.idinventario=" + pidInventario.ToString
        'Else
        '    If pidClasificacion > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
        '    End If
        '    If pidClasificacion2 > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion.ToString
        '    End If
        '    If pidClasificacion3 > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion.ToString
        '    End If
        'End If
        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventas.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function

    'Public Function ReportePorTipodePago(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSerie As String, ByVal pIdForma As Integer) As DataView
    '    Dim DS As New DataSet
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre,tblventas.nocuenta,tblventas.idforma,tblformasdepago.tipo " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblformasdepago.tipo<>2 and tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    Else
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre,tblventas.nocuenta,tblventas.idforma,tblformasdepago.tipo " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblformasdepago.tipo<>2 and tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    End If
    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdCliente > 0 Then
    '        Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
    '    End If
    '    If pIdVendedor > 0 Then
    '        Comm.CommandText += " and tblventas.idvendedor=" + pIdVendedor.ToString
    '    End If
    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
    '    End If
    '    If pSerie <> "" Then
    '        Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
    '    End If
    '    If pIdForma > 0 Then
    '        Comm.CommandText += " and tblventas.idforma=" + pIdForma.ToString
    '    End If
    '    'If pidInventario > 1 Then
    '    '    Comm.CommandText += " and tblventasinventario.idinventario=" + pidInventario.ToString
    '    'Else
    '    '    If pidClasificacion > 0 Then
    '    '        Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
    '    '    End If
    '    '    If pidClasificacion2 > 0 Then
    '    '        Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion.ToString
    '    '    End If
    '    '    If pidClasificacion3 > 0 Then
    '    '        Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion.ToString
    '    '    End If
    '    'End If
    '    Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblventas")
    '    'DS.WriteXmlSchema("tblventastp.xml")
    '    Return DS.Tables("tblventas").DefaultView
    'End Function

    Public Function ReportePorTipodePago(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSerie As String, ByVal pIdForma As Integer, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal pSoloCanceladas As Boolean, pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre,tblventas.nocuenta,tblventas.idforma,tblformasdepago.tipo " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo<>2  and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre,tblventas.nocuenta,tblventas.idforma,tblformasdepago.tipo " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and tblclientes.zona='" + pZona2.ToString() + "' or tblclientes.zona2='" + pZona2.ToString() + "'"
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pIdVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pIdVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pIdForma > 0 Then
            Comm.CommandText += " and tblventas.idforma=" + pIdForma.ToString
        End If
        If pSoloCanceladas Then
            Comm.CommandText += " and tblventas.estado=3"
        Else
            Comm.CommandText += " and (tblventas.estado=3 or tblventas.estado=4)"
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        'If pidInventario > 1 Then
        '    Comm.CommandText += " and tblventasinventario.idinventario=" + pidInventario.ToString
        'Else
        '    If pidClasificacion > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
        '    End If
        '    If pidClasificacion2 > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion.ToString
        '    End If
        '    If pidClasificacion3 > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion.ToString
        '    End If
        'End If
        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventastp.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function

    Public Function ReporteClientesDeudas(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pModo As Byte, ByVal pIdForma As Integer, ByVal pZona2 As Integer) As DataView
        Dim DS As New DataSet
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre " + _
        '"from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo=0 and tblventas.credito<tblventas.totalapagar"
        If pIdCliente = 0 Then
            Comm.CommandText = "delete from tblclientesrepdeudas"
        Else
            Comm.CommandText = "delete from tblclientesrepdeudas where idcliente=" + pIdCliente.ToString
        End If
        Comm.ExecuteNonQuery()
        If pModo = 0 Then
            If pMostrarEnPesos = 0 Then
                Comm.CommandText = "insert into tblclientesrepdeudas(idventa,idcliente,folio,serie,estado,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave) select tblventas.idventa,tblventas.idcliente,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblclientes.nombre as cnombre,if(tblventas.idconversion=2,tblventas.credito,tblventas.credito*tblventas.tipodecambio) as abonado,tblclientes.clave " + _
                        "from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblformasdepago.tipo=0 and round(tblventas.credito,2)<round(tblventas.totalapagar,2)"
            Else
                Comm.CommandText = "insert into tblclientesrepdeudas(idventa,idcliente,folio,serie,estado,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave) select tblventas.idventa,tblventas.idcliente,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblclientes.nombre as cnombre,tblventas.credito as abonado,tblclientes.clave " + _
                        "from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblformasdepago.tipo=0 and round(tblventas.credito,2)<round(tblventas.totalapagar,2)"
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
            End If
            If pIdCliente > 0 Then
                Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
            End If
            If pidMoneda > 0 Then
                Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
            End If
            If pIdForma > 0 Then
                Comm.CommandText += " and tblventas.idforma=" + pIdForma.ToString
            End If
            If pZona2 > 0 Then
                Comm.CommandText += " and (tblclientes.zona='" + pZona2.ToString + "' or tblclientes.zona2='" + pZona2.ToString() + "')"
            End If
            Comm.ExecuteNonQuery()
            'If pIdVendedor > 0 Then
            'Comm.CommandText += " and tblventas.idvendedor=" + pIdVendedor.ToString
            'End If
            'Notas de Cargo
            If pMostrarEnPesos = 0 Then
                Comm.CommandText = "insert into tblclientesrepdeudas(idventa,idcliente,folio,serie,estado,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave) select tblnotasdecargo.idcargo,tblnotasdecargo.idcliente,tblnotasdecargo.folio,tblnotasdecargo.serie,tblnotasdecargo.estado,if(tblnotasdecargo.idmoneda=2,tblnotasdecargo.total,tblnotasdecargo.total*tblnotasdecargo.tipodecambio) as total,if(tblnotasdecargo.idmoneda=2,tblnotasdecargo.totalapagar,tblnotasdecargo.totalapagar*tblnotasdecargo.tipodecambio) as totalapagar,tblnotasdecargo.fecha,tblnotasdecargo.tipodecambio,tblnotasdecargo.idmoneda,tblclientes.nombre as cnombre,if(tblnotasdecargo.idmoneda=2,tblnotasdecargo.aplicado,tblnotasdecargo.aplicado*tblnotasdecargo.tipodecambio) as abonado,tblclientes.clave " + _
                        "from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.estado=3 and round(tblnotasdecargo.aplicado,2)<round(tblnotasdecargo.totalapagar,2)"
            Else
                Comm.CommandText = "insert into tblclientesrepdeudas(idventa,idcliente,folio,serie,estado,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave) select tblnotasdecargo.idcargo,tblnotasdecargo.idcliente,tblnotasdecargo.folio,tblnotasdecargo.serie,tblnotasdecargo.estado,tblnotasdecargo.total,tblnotasdecargo.totalapagar,tblnotasdecargo.fecha,tblnotasdecargo.tipodecambio,tblnotasdecargo.idmoneda,tblclientes.nombre as cnombre,tblnotasdecargo.aplicado as abonado,tblclientes.clave " + _
                        "from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.estado=3 and round(tblnotasdecargo.aplicado,2)<round(tblnotasdecargo.totalapagar,2)"
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
            End If
            If pIdCliente > 0 Then
                Comm.CommandText += " and tblnotasdecargo.idcliente=" + pIdCliente.ToString
            End If
            If pidMoneda > 0 Then
                Comm.CommandText += " and tblnotasdecargo.idmoneda=" + pidMoneda.ToString
            End If
            If pZona2 > 0 Then
                Comm.CommandText += " and (tblclientes.zona='" + pZona2.ToString + "' or tblclientes.zona2='" + pZona2.ToString() + "')"
            End If
            Comm.ExecuteNonQuery()
            'Documentos
            If pMostrarEnPesos = 0 Then
                Comm.CommandText = "insert into tblclientesrepdeudas(idventa,idcliente,folio,serie,estado,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave) " + _
                "select dc.iddocumento,dc.idcliente,if(dc.tiposaldo=0,dc.folio,dc.folioreferencia),if(dc.tiposaldo=0,dc.serie,dc.seriereferencia),dc.estado,dc.totalapagar as total,if(dc.idmoneda=2,dc.totalapagar,dc.totalapagar*dc.tipodecambio) as totalapagar,dc.fecha,dc.tipodecambio,dc.idmoneda,tblclientes.nombre as cnombre,if(dc.idmoneda=2,dc.credito,dc.credito*dc.tipodecambio) as abonado,tblclientes.clave " + _
                        "from tbldocumentosclientes as dc inner join tblclientes on dc.idcliente=tblclientes.idcliente where dc.estado=3 and round(dc.credito,2)<round(dc.totalapagar,2)"
            Else
                Comm.CommandText = "insert into tblclientesrepdeudas(idventa,idcliente,folio,serie,estado,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave) " + _
           "select dc.iddocumento,dc.idcliente,if(dc.tiposaldo=0,dc.folio,dc.folioreferencia),if(dc.tiposaldo=0,dc.serie,dc.seriereferencia),dc.estado,dc.totalapagar as total,dc.totalapagar as totalapagar,dc.fecha,dc.tipodecambio,dc.idmoneda,tblclientes.nombre as cnombre,dc.credito as abonado,tblclientes.clave " + _
                   "from tbldocumentosclientes as dc inner join tblclientes on dc.idcliente=tblclientes.idcliente where dc.estado=3 and round(dc.credito,2)<round(dc.totalapagar,2)"
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and dc.idsucursal=" + pIdSucursal.ToString
            End If
            If pIdCliente > 0 Then
                Comm.CommandText += " and dc.idcliente=" + pIdCliente.ToString
            End If
            If pidMoneda > 0 Then
                Comm.CommandText += " and dc.idmoneda=" + pidMoneda.ToString
            End If
            If pZona2 > 0 Then
                Comm.CommandText += " and (tblclientes.zona='" + pZona2.ToString + "' or tblclientes.zona2='" + pZona2.ToString() + "')"
            End If
            Comm.ExecuteNonQuery()
        End If
        'Remisiones
        If pModo = 1 Then
            If pMostrarEnPesos = 0 Then
                Comm.CommandText = "insert into tblclientesrepdeudas(idventa,idcliente,folio,serie,estado,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave) " + _
                        "select vr.idremision,vr.idcliente,vr.folio,vr.serie,vr.estado,if(vr.idmoneda=2,vr.total,vr.total*vr.tipodecambio) as total,if(vr.idmoneda=2,vr.totalapagar,vr.totalapagar*vr.tipodecambio) as totalapagar,vr.fecha,vr.tipodecambio,vr.idmoneda,tblclientes.nombre as cnombre,if(vr.idmoneda=2,vr.credito,vr.credito*vr.tipodecambio) as abonado,tblclientes.clave " + _
                        "from tblventasremisiones as vr inner join tblclientes on vr.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones on tblformasdepagoremisiones.idforma=vr.idforma where vr.estado=3 and tblformasdepagoremisiones.tipo=3 and round(vr.credito,2)<round(vr.totalapagar,2)"
            Else
                Comm.CommandText = "insert into tblclientesrepdeudas(idventa,idcliente,folio,serie,estado,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave) " + _
                        "select vr.idremision,vr.idcliente,vr.folio,vr.serie,vr.estado,vr.total,vr.totalapagar,vr.fecha,vr.tipodecambio,vr.idmoneda,tblclientes.nombre as cnombre,vr.credito as abonado,tblclientes.clave " + _
                        "from tblventasremisiones as vr inner join tblclientes on vr.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones on tblformasdepagoremisiones.idforma=vr.idforma where vr.estado=3 and tblformasdepagoremisiones.tipo=3 and round(vr.credito,2)<round(vr.totalapagar,2)"
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and vr.idsucursal=" + pIdSucursal.ToString
            End If
            If pIdCliente > 0 Then
                Comm.CommandText += " and vr.idcliente=" + pIdCliente.ToString
            End If
            If pidMoneda > 0 Then
                Comm.CommandText += " and vr.idmoneda=" + pidMoneda.ToString
            End If
            If pIdForma > 0 Then
                Comm.CommandText += " and vr.idforma=" + pIdForma.ToString
            End If
            If pZona2 > 0 Then
                Comm.CommandText += " and (tblclientes.zona='" + pZona2.ToString + "' or tblclientes.zona2='" + pZona2.ToString() + "')"
            End If
            Comm.ExecuteNonQuery()
        End If

        If pModo = 2 Then
            If pMostrarEnPesos = 0 Then
                Comm.CommandText = "insert into tblclientesrepdeudas(idventa,idcliente,folio,serie,estado,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave) " + _
                        "select vr.idapartado,vr.idcliente,vr.folio,vr.serie,vr.estado,if(vr.idmoneda=2,vr.totalapagar,vr.totalapagar*vr.tipodecambio) as total,if(vr.idmoneda=2,vr.totalapagar,vr.totalapagar*vr.tipodecambio) as totalapagar,vr.fecha,vr.tipodecambio,vr.idmoneda,tblclientes.nombre as cnombre,if(vr.idmoneda=2,vr.credito,vr.credito*vr.tipodecambio) as abonado,tblclientes.clave " + _
                        "from tblventasapartados as vr inner join tblclientes on vr.idcliente=tblclientes.idcliente where vr.estado=3 and round(vr.credito,2)<round(vr.totalapagar,2)"
            Else
                Comm.CommandText = "insert into tblclientesrepdeudas(idventa,idcliente,folio,serie,estado,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave) " + _
                        "select vr.idapartado,vr.idcliente,vr.folio,vr.serie,vr.estado,vr.totalapagar as total,vr.totalapagar,vr.fecha,vr.tipodecambio,vr.idmoneda,tblclientes.nombre as cnombre,vr.credito as abonado,tblclientes.clave " + _
                        "from tblventasapartados as vr inner join tblclientes on vr.idcliente=tblclientes.idcliente where vr.estado=3 and round(vr.credito,2)<round(vr.totalapagar,2)"
            End If
            If pIdSucursal > 0 Then
                Comm.CommandText += " and vr.idsucursal=" + pIdSucursal.ToString
            End If
            If pIdCliente > 0 Then
                Comm.CommandText += " and vr.idcliente=" + pIdCliente.ToString
            End If
            If pidMoneda > 0 Then
                Comm.CommandText += " and vr.idmoneda=" + pidMoneda.ToString
            End If
            If pZona2 > 0 Then
                Comm.CommandText += " and (tblclientes.zona='" + pZona2.ToString + "' or tblclientes.zona2='" + pZona2.ToString() + "')"
            End If
            'If pIdForma > 0 Then
            'Comm.CommandText += " and vr.idforma=" + pIdForma.ToString
            'End If
            Comm.ExecuteNonQuery()
        End If

        If pIdCliente = 0 Then
            Comm.CommandText = "select idventa,idcliente,folio,serie,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave from tblclientesrepdeudas order by cnombre,fecha,serie,folio"
        Else
            Comm.CommandText = "select idventa,idcliente,folio,serie,total,totalapagar,fecha,tipodecambio,idconversion,cnombre,abonado,clave from tblclientesrepdeudas where idcliente=" + pIdCliente.ToString + " order by cnombre,fecha,serie,folio"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventasdeudas.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    'Public Function ReporteVentasArticulos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pConcendaso As Boolean, ByVal pSerie As String, ByVal pOrdenxVendedor As Boolean) As DataView
    '    Dim DS As New DataSet
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,if(tblventasinventario.idinventario>1,tblinventario.nombre,if(tblventasinventario.idvariante>1,tblproductos.nombre,'SERVICIO')) as descripcion,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio),2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio/tblventasinventario.cantidad,tblventasinventario.precio*tblventas.tipodecambio/tblventasinventario.cantidad),2) as preciou,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventas.porsurtir,tblformasdepago.tipo as tipoforma " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo<>2 and tblventasinventario.cantidad<>0"
    '    Else
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,if(tblventasinventario.idinventario>1,tblinventario.nombre,if(tblventasinventario.idvariante>1,tblproductos.nombre,'SERVICIO')) as descripcion,round(tblventasinventario.precio,2),0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(tblventasinventario.precio/tblventasinventario.cantidad,2) as preciou,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventas.porsurtir,tblformasdepago.tipo as tipoforma " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto where tblventas.estado=3 and tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventasinventario.cantidad<>0 "
    '    End If
    '    'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdCliente > 0 Then
    '        Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
    '    End If
    '    If pidVendedor > 0 Then
    '        Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
    '    End If
    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
    '    End If
    '    If pIdInventario > 1 Then
    '        Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
    '    Else
    '        If pidClasificacion > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
    '        End If
    '        If pidClasificacion2 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
    '        End If
    '        If pidClasificacion3 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
    '        End If
    '    End If

    '    If pSerie <> "" Then
    '        Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
    '    End If

    '    If pConcendaso = False Then
    '        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
    '    Else
    '        If pOrdenxVendedor Then
    '            Comm.CommandText += " order by tblventas.idvendedor,tblventasinventario.idinventario,preciou"
    '        Else
    '            Comm.CommandText += " order by tblventasinventario.idinventario,preciou"
    '        End If

    '    End If
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblventas")
    '    'DS.WriteXmlSchema("tblventasa.xml")
    '    Return DS.Tables("tblventas").DefaultView
    'End Function

    Public Function ReporteVentasArticulos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pConcendaso As Boolean, ByVal pSerie As String, ByVal pOrdenxVendedor As Boolean, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal PsoloCanceladas As Boolean, ByVal pSoloActivas As Boolean, pidAlmacen As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblinventario.nombre as descripcion,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio),2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,0 idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio/tblventasinventario.cantidad,tblventasinventario.precio*tblventas.tipodecambio/tblventasinventario.cantidad),2) as preciou,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventas.porsurtir,tblformasdepago.tipo as tipoforma,tblventasinventario.ieps,tblventasinventario.ivaretenido ivar " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo<>2 and tblventasinventario.cantidad<>0"
        Else
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblinventario.nombre as descripcion,round(tblventasinventario.precio,2) precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,0 idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(tblventasinventario.precio/tblventasinventario.cantidad,2) as preciou,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventas.porsurtir,tblformasdepago.tipo as tipoforma,tblventasinventario.ieps,tblventasinventario.ivaretenido ivar " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventasinventario.cantidad<>0 "
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and tblclientes.zona='" + pZona2.ToString() + "' or tblclientes.zona2='" + pZona2.ToString() + "'"
        End If
        If pSoloActivas Then
            Comm.CommandText += " and tblventas.estado=3"
        Else
            If PsoloCanceladas = False Then
                Comm.CommandText += " and (tblventas.estado=3 or tblventas.estado=4)"
            Else
                Comm.CommandText += " and tblventas.estado=4"
            End If
        End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            End If
            If pidClasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
            End If
            If pidClasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
            End If
        End If

        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If

        If pidAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pidAlmacen.ToString
        End If

        If pConcendaso = False Then
            Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
        Else
            If pOrdenxVendedor Then
                Comm.CommandText += " order by tblventas.idvendedor,tblventasinventario.idinventario,preciou"
            Else
                Comm.CommandText += " order by tblventasinventario.idinventario,preciou"
            End If

        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventasa.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function

    Public Function ReporteVentasArticulosDetalle(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pConcendaso As Boolean, ByVal pSerie As String, ByVal pOrdenxVendedor As Boolean, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal PsoloCanceladas As Boolean, ByVal pSoloActivas As Boolean, pidAlmacen As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio),2) as precio,tblventasinventario.idinventario,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio/tblventasinventario.cantidad,tblventasinventario.precio*tblventas.tipodecambio/tblventasinventario.cantidad),2) as preciou,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventas.porsurtir,tblformasdepago.tipo as tipoforma,tblventasinventario.ieps,tblventasinventario.ivaretenido ivar,tblventas.idcliente,tblventas.credito,ifnull(tblzona.idzona,0) as idzona,ifnull(tblzona.zona,'SIN ZONA') as zona " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor left outer join tblzona on tblclientes.zona=tblzona.idzona where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo<>2 and tblventasinventario.cantidad<>0 and tblventasinventario.idinventario>1 "
        Else
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,round(tblventasinventario.precio,2) precio,tblventasinventario.idinventario,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(tblventasinventario.precio/tblventasinventario.cantidad,2) as preciou,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventas.porsurtir,tblformasdepago.tipo as tipoforma,tblventasinventario.ieps,tblventasinventario.ivaretenido ivar,tblventas.idcliente,tblventas.credito,ifnull(tblzona.idzona,0) as idzona,ifnull(tblzona.zona,'SIN ZONA') as zona " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor left outer join tblzona on tblclientes.zona=tblzona.idzona where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventasinventario.cantidad<>0 and tblventasinventario.idinventario>1 "
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and tblclientes.zona='" + pZona2.ToString() + "' or tblclientes.zona2='" + pZona2.ToString() + "'"
        End If
        If pSoloActivas Then
            Comm.CommandText += " and tblventas.estado=3"
        Else
            If PsoloCanceladas = False Then
                Comm.CommandText += " and (tblventas.estado=3 or tblventas.estado=4)"
            Else
                Comm.CommandText += " and tblventas.estado=4"
            End If
        End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            End If
            If pidClasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
            End If
            If pidClasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
            End If
        End If

        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If

        If pidAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pidAlmacen.ToString
        End If

        If pConcendaso = False Then
            Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
        Else
            If pOrdenxVendedor Then
                Comm.CommandText += " order by tblventas.idvendedor,tblventasinventario.idinventario,preciou"
            Else
                Comm.CommandText += " order by tblventasinventario.idinventario,preciou"
            End If

        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventasdetalle.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function

    Public Function ReporteVentasArticulosMasVendidos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSerie As String, ByVal pOrdenxVendedor As Boolean, ByVal pLimite As String, ByVal pTipoOrden As Byte, ByVal pZona As Integer, ByVal pZona2 As Integer, pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select sum(tblventasinventario.cantidad) as cantidad,tblinventario.nombre as descripcion,sum(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio)) as precio,tblventasinventario.idinventario,tblventasinventario.iva,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventasinventario.ieps,tblventasinventario.ivaretenido ivaret " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo<>2 and tblventasinventario.cantidad<>0"
        Else
            Comm.CommandText = "select sum(tblventasinventario.cantidad) as cantidad,tblinventario.nombre as descripcion,sum(tblventasinventario.precio) as precio,tblventasinventario.idinventario,tblventasinventario.iva,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventasinventario.ieps,tblventasinventario.ivaretenido ivaret " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo<>2 and tblventasinventario.cantidad<>0"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and tblclientes.zona='" + pZona2.ToString() + "' or tblclientes.zona2='" + pZona2.ToString() + "'"
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
        End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            End If
            If pidClasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
            End If
            If pidClasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
            End If
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If

        If pOrdenxVendedor Then
            Comm.CommandText += " group by tblventas.idvendedor,tblventasinventario.idinventario"
        Else
            Comm.CommandText += " group by tblventasinventario.idinventario"
        End If
        If pTipoOrden = 0 Then
            Comm.CommandText += " order by cantidad desc limit " + pLimite
        Else
            Comm.CommandText += " order by precio desc limit " + pLimite
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasmas")
        'DS.WriteXmlSchema("tblventasmas.xml")
        Return DS.Tables("tblventasmas").DefaultView
    End Function
    Public Function ReporteViejosSaldos(ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pTipodeCambio As Double, ByVal pZona As Integer) As DataView
        'Dim DS As New DataSet
        'Dim F As String
        'F = Format(Date.Now, "yyyy/MM/dd")
        'If pIdCliente > 0 Then
        '    Comm.CommandText = "delete from tblclientesviejossaldos where idcliente=" + pIdCliente.ToString
        'Else
        '    Comm.CommandText = "delete from tblclientesviejossaldos"
        'End If
        'Comm.ExecuteNonQuery()
        'If pMostrarEnPesos = 0 Then
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sietedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sesentadias," + _
        '    "if(tblventas.idconversion=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'Else
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sietedias," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as quincedias," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as treintadias," + _
        '   "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sesentadias," + _
        '   "(tblventas.totalapagar-tblventas.credito) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'End If

        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        'End If
        'If pIdCliente > 0 Then
        '    Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        'End If
        'If pidVendedor > 0 Then
        '    Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        'End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        'End If
        'Comm.ExecuteNonQuery()
        ''---------Notas de Cargo

        'If pMostrarEnPesos = 0 Then
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as sietedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as sesentadias," + _
        '    "if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where round(tblventas.totalapagar-tblventas.aplicado,2)>0 and tblventas.estado=3"
        'Else
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as sietedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as sesentadias," + _
        '    "(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where round(tblventas.totalapagar-tblventas.aplicado,2)>0 and tblventas.estado=3"
        'End If

        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        'End If
        'If pIdCliente > 0 Then
        '    Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        'End If
        'If pidVendedor > 0 Then
        '    Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        'End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        'End If
        'Comm.ExecuteNonQuery()
        ''-------------Documentos Saldo Inicial

        'If pMostrarEnPesos = 0 Then
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sietedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sesentadias," + _
        '    "if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio) as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'Else
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sietedias," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sesentadias," + _
        '    "(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'End If

        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        'End If
        'If pIdCliente > 0 Then
        '    Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        'End If
        'If pidVendedor > 0 Then
        '    Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        'End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        'End If
        'Comm.ExecuteNonQuery()
        ''Documentos documentos

        'If pMostrarEnPesos = 0 Then
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sietedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sesentadias," + _
        '    "if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio) as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'Else
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sietedias," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sesentadias," + _
        '    "(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'End If

        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        'End If
        'If pIdCliente > 0 Then
        '    Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        'End If
        'If pidVendedor > 0 Then
        '    Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        'End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        'End If
        'Comm.ExecuteNonQuery()
        'If pIdCliente <= 0 Then
        '    Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente  order by tblclientes.nombre,fecha,serie,folio"
        'Else
        '    Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente where tblclientesviejossaldos.idcliente=" + pIdCliente.ToString + " order by tblclientes.nombre,fecha,serie,folio"
        'End If

        'Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        'DA.Fill(DS, "tblventasviejo")
        ''DS.WriteXmlSchema("tblventasviejo.xml")
        'Return DS.Tables("tblventasviejo").DefaultView
        Dim DS As New DataSet
        Dim F As String
        F = Format(Date.Now, "yyyy/MM/dd")
        'F = pFecha2
        'Comm.CommandText = "select ifnull((select tipodecambio from tblcompras where fecha<='" + F + "' and idmoneda=2 order by fecha desc limit 1),1)"
        'pTipodeCambio = Comm.ExecuteScalar
        Comm.CommandTimeout = 10000
        If pIdCliente > 0 Then
            Comm.CommandText = "delete from tblclientesviejossaldos where idcliente=" + pIdCliente.ToString
        Else
            Comm.CommandText = "delete from tblclientesviejossaldos"
        End If
        Comm.ExecuteNonQuery()

        Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        "tblventas.credito as credito," + _
        "0,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
        ",totalapagar from  tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.idconversion=2 and round(tblventas.totalapagar-tblventas.credito,2)>0"


        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        Comm.ExecuteNonQuery()
        If pMostrarEnPesos = 0 Then
            'Dolares a pesos
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
            "if(tblventas.idconversion=2,tblventas.credito,tblventas.credito*tblventas.tipodecambio) as credito," + _
            "0," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.idconversion<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "tblventas.credito as credito," + _
            "0,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.idconversion<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        End If

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If

        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If

        Comm.ExecuteNonQuery()



        '---------Notas de Cargo



        Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "tblventas.aplicado as credito," + _
        "1,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.estado=3 and tblventas.idmoneda=2 and round(tblventas.totalapagar-tblventas.aplicado,2)>0"



        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()

        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "tblventas.aplicado*tblventas.tipodecambio as credito," + _
            "1," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente  where tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.aplicado,2)>0"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "tblventas.aplicado as credito," + _
            "1,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.aplicado,2)>0"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()
        '-------------Documentos Saldo Inicial

        Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "tblventas.credito as credito," + _
        "2,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.estado=3 and tblventas.idmoneda=2 and round(tblventas.totalapagar-tblventas.credito,2)>0"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()

        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "tblventas.credito*tblventas.tipodecambio as credito," + _
            "2," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente  where tblventas.tiposaldo=0 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "tblventas.credito as credito," + _
            "2,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()
        'Documentos documentos

        'If pMostrarEnPesos = 0 Then

        Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "tblventas.credito as credito," + _
        "3,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and tblventas.estado=3 and tblventas.idmoneda=2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        'Else

        'End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "tblventas.credito*tblventas.tipodecambio as credito," + _
            "3," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente  where tblventas.tiposaldo=1 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "tblventas.credito as credito," + _
            "3,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente  where tblventas.tiposaldo=1 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()
        'Saca saldos
        'Comm.CommandText = "delete from tblclientesmovimientossaldos"
        'Comm.ExecuteNonQuery()
        'Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select idcliente,spdasaldoafechaproveedor(idcliente,'" + Format(DateAdd(DateInterval.Day, 1, CDate(pFecha2)), "yyyy/MM/dd") + "') from tblclientesviejossaldos group by idcliente"
        'Comm.ExecuteNonQuery()
        If pIdCliente <= 0 Then
            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant,tblclientesviejossaldos.tipodecambio,tblclientesviejossaldos.corriente from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente order by tblclientes.nombre,fecha,serie,folio"
        Else
            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant,tblclientesviejossaldos.tipodecambio,tblclientesviejossaldos.corriente from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente where tblclientesviejossaldos.idcliente=" + pIdCliente.ToString + " order by tblclientes.nombre,fecha,serie,folio"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasviejo")
        'DS.WriteXmlSchema("tblventasviejo.xml")
        Return DS.Tables("tblventasviejo").DefaultView
    End Function

    Public Function ReporteViejosSaldosH(ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pFecha As String, ByVal pTipodeCambio As Double) As DataView
        Dim DS As New DataSet
        Dim F As String
        'F = Format(Date.Now, "yyyy/MM/dd")
        F = pFecha
        Comm.CommandTimeout = 10000
        Comm.CommandText = "select ifnull((select tipodecambio from tblventas where fecha<='" + F + "' and idconversion<>2 order by fecha desc limit 1),1)"
        pTipodeCambio = Comm.ExecuteScalar
        If IdCliente > 0 Then
            Comm.CommandText = "delete from tblclientesviejossaldos where idcliente=" + IdCliente.ToString
        Else
            Comm.CommandText = "delete from tblclientesviejossaldos"
        End If
        Comm.ExecuteNonQuery()
        'If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
        '"if(tblventas.idmoneda=2," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + Pfecha + "' and tblventaspagos.idventa=tblventas.idventa),0)," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + Pfecha + "' and tblventaspagos.idventa=tblventas.idventa),0)*tblventas.tipodecambio) as credito," + _
        '"0 from tblcompras as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + Pfecha + "' and tblventas.estado=3"
        Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        "ifnull((select sum(if(tblventaspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0) as credito," + _
        "0,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
        ",totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idconversion=2"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        'If pidMoneda > 0 Then
        'Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        'End If
        Comm.ExecuteNonQuery()
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idconversion,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)*tblventas.tipodecambio as credito," + _
            "0," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idconversion<>2"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idconversion,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0) as credito," + _
            "0,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idconversion<>2"
        End If

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()



        '---------Notas de Cargo



        Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "ifnull((select sum(if(tblventaspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0) as credito," + _
        "1,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda=2"



        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()

        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)*tblventas.tipodecambio as credito," + _
            "1," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0) as credito," + _
            "1,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        '-------------Documentos Saldo Inicial

        Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "ifnull((select sum(if(tblventaspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
        "2,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda=2"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()

        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)*tblventas.tipodecambio as credito," + _
            "2," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
            "2,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        'Documentos documentos

        'If pMostrarEnPesos = 0 Then

        Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "ifnull((select sum(if(tblventaspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
        "3,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda=2"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)*tblventas.tipodecambio as credito," + _
            "3," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
            "3,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        'Saca saldos
        'Comm.CommandText = "delete from tblclientesmovimientossaldos"
        'Comm.ExecuteNonQuery()
        'Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select idcliente,spdasaldoafechaproveedor(idcliente,'" + Format(DateAdd(DateInterval.Day, 1, CDate(Pfecha)), "yyyy/MM/dd") + "') from tblclientesviejossaldos group by idcliente"
        'Comm.ExecuteNonQuery()


        If IdCliente <= 0 Then
            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant,tblclientesviejossaldos.tipodecambio,tblclientesviejossaldos.corriente from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente where round(totalapagar-tblclientesviejossaldos.credito,2)>0  order by tblclientes.nombre,fecha,serie,folio"
        Else
            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant,tblclientesviejossaldos.tipodecambio,tblclientesviejossaldos.corriente from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente where tblclientesviejossaldos.idcliente=" + IdCliente.ToString + " and round(totalapagar-tblclientesviejossaldos.credito,2)>0 order by tblclientes.nombre,fecha,serie,folio"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesviejo")
        'DS.WriteXmlSchema("tblcomprasviejo.xml")
        Return DS.Tables("tblclientesviejo").DefaultView
        '        Dim DS As New DataSet
        '        Dim F As String
        '        F = pFecha
        '        Comm.CommandTimeout = 10000
        '        If pIdCliente > 0 Then
        '            Comm.CommandText = "delete from tblclientesviejossaldos where idcliente=" + pIdCliente.ToString
        '        Else
        '            Comm.CommandText = "delete from tblclientesviejossaldos"
        '        End If
        '        Comm.ExecuteNonQuery()
        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '"if(tblventas.idconversion=2," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)*tblventas.tipodecambio) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "'"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "'"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        '---------Notas de Cargo

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)*tblventas.tipodecambio) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        '-------------Documentos Saldo Inicial

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)*tblventas.tipodecambio) as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)) as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        'Documentos documentos

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)*tblventas.tipodecambio) as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()

        '        'Saca saldos
        '        Comm.CommandText = "delete from tblclientesmovimientossaldos"
        '        Comm.ExecuteNonQuery()
        '        'Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select idcliente,spdasaldoafechaclientes(idcliente,'" + Format(DateAdd(DateInterval.Day, 1, CDate(pFecha)), "yyyy/MM/dd") + "') from tblclientesviejossaldos group by idcliente"
        '        Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select c.idcliente, " + _
        '        "ifnull((select if(idconversion=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and idcliente=c.idcliente and estado=3 and tblventas.fecha<='" + pFecha + "'),0)+" + _
        '"ifnull((select if(idmoneda=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tblnotasdecargo where idcliente=c.idcliente and estado=3 and tblnotasdecargo.fecha<='" + pFecha + "'),0)+" + _
        '"ifnull((select if(idmoneda=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tbldocumentosclientes where idcliente=c.idcliente and estado=3 and tbldocumentosclientes.fecha<='" + pFecha + "'),0)-" + _
        '"ifnull((select if(idmoneda=2,sum(cantidad),sum(cantidad*ptipodecambio)) from tblventaspagos where idcliente=c.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "'),0) AS saldo " + _
        '"from tblclientesviejossaldos as c group by c.idcliente"
        '        Comm.ExecuteNonQuery()
        '        If pIdCliente <= 0 Then
        '            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente inner join tblclientesmovimientossaldos on tblclientesviejossaldos.idcliente=tblclientesmovimientossaldos.idcliente where round(sietedias+quincedias+treintadias+sesentadias-tblclientesviejossaldos.credito,2)>0 order by tblclientes.nombre,fecha,serie,folio"
        '        Else
        '            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente inner join tblclientesmovimientossaldos on tblclientesviejossaldos.idcliente=tblclientesmovimientossaldos.idcliente where tblclientesviejossaldos.idcliente=" + pIdCliente.ToString + " and round(sietedias+quincedias+treintadias+sesentadias-tblclientesviejossaldos.credito,2)>0 order by tblclientes.nombre,fecha,serie,folio"
        '        End If

        '        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        '        DA.Fill(DS, "tblventasviejo")
        '        'DS.WriteXmlSchema("tblventasviejo.xml")
        '        Return DS.Tables("tblventasviejo").DefaultView
        '    End Function

        '    Public Function ReporteViejosSaldosHN(ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pFecha As String) As DataView
        '        Dim DS As New DataSet
        '        Dim F As String
        '        F = pFecha
        '        Comm.CommandTimeout = 10000
        '        If pIdCliente > 0 Then
        '            Comm.CommandText = "delete from tblclientesviejossaldos where idcliente=" + pIdCliente.ToString
        '        Else
        '            Comm.CommandText = "delete from tblclientesviejossaldos"
        '        End If
        '        Comm.ExecuteNonQuery()
        '        If pMostrarEnPesos = 0 Then

        '            '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            '"if(tblventas.idconversion=2," + _
        '            '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)," + _
        '            '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)*tblventas.tipodecambio) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "' and round(tblventas.totalapagar-tblventas.credito,2)>0;"


        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(tblventas.fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(tblventas.fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(tblventas.fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(tblventas.fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(tblventas.fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(tblventas.fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(tblventas.fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '"if(tblventas.idconversion=2," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)*tblventas.tipodecambio) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "' and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0),2)>0"


        '        Else
        '            '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '            '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "' and round(tblventas.totalapagar-tblventas.credito,2)>0"

        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "' and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0),2)>0"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        '---------Notas de Cargo

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)*tblventas.tipodecambio) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0),2)>0"

        '            'Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            '"if(tblventas.idmoneda=2," + _
        '            '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)," + _
        '            '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)*tblventas.tipodecambio) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"

        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0),2)>0"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        '-------------Documentos Saldo Inicial

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)*tblventas.tipodecambio) as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0),2)>0"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)) as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0),2)>0"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        'Documentos documentos

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)*tblventas.tipodecambio) as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0),2)>0"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0),2)>0"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()

        '        'Saca saldos
        '        Comm.CommandText = "delete from tblclientesmovimientossaldos"
        '        Comm.ExecuteNonQuery()
        '        'Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select idcliente,spdasaldoafechaclientes(idcliente,'" + Format(DateAdd(DateInterval.Day, 1, CDate(pFecha)), "yyyy/MM/dd") + "') from tblclientesviejossaldos group by idcliente"
        '        Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select c.idcliente, " + _
        '        "ifnull((select if(idconversion=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and idcliente=c.idcliente and estado=3 and tblventas.fecha<='" + pFecha + "'),0)+" + _
        '"ifnull((select if(idmoneda=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tblnotasdecargo where idcliente=c.idcliente and estado=3 and tblnotasdecargo.fecha<='" + pFecha + "'),0)+" + _
        '"ifnull((select if(idmoneda=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tbldocumentosclientes where idcliente=c.idcliente and estado=3 and tbldocumentosclientes.fecha<='" + pFecha + "'),0)-" + _
        '"ifnull((select if(idmoneda=2,sum(cantidad),sum(cantidad*ptipodecambio)) from tblventaspagos where idcliente=c.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "'),0) AS saldo " + _
        '"from tblclientesviejossaldos as c group by c.idcliente"
        '        Comm.ExecuteNonQuery()
        '        If pIdCliente <= 0 Then
        '            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,round(sietedias,2) as sietedias,quincedias,treintadias,sesentadias,tipo,round(tblclientesviejossaldos.credito,2) as credito,round(saldoant,2) as saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente inner join tblclientesmovimientossaldos on tblclientesviejossaldos.idcliente=tblclientesmovimientossaldos.idcliente where round(sietedias+quincedias+treintadias+sesentadias-tblclientesviejossaldos.credito,2)>0 order by tblclientes.nombre,fecha,serie,folio"
        '        Else
        '            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,round(sietedias,2) as sietedias,quincedias,treintadias,sesentadias,tipo,round(tblclientesviejossaldos.credito,2) as credito,round(saldoant,2) as saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente inner join tblclientesmovimientossaldos on tblclientesviejossaldos.idcliente=tblclientesmovimientossaldos.idcliente where tblclientesviejossaldos.idcliente=" + pIdCliente.ToString + " and round(sietedias+quincedias+treintadias+sesentadias-tblclientesviejossaldos.credito,2)>0 order by tblclientes.nombre,fecha,serie,folio"
        '        End If

        '        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        '        DA.Fill(DS, "tblventasviejo")
        '        'DS.WriteXmlSchema("tblventasviejo.xml")
        '        Return DS.Tables("tblventasviejo").DefaultView
    End Function

    Public Function ReporteVentasSeries(ByVal pidVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.idventasinventario,tblinventario.clave,tblinventario.nombre as descripcion,tvi.cantidad,tblinventarioseries.noserie,tblinventarioseries.fechagarantia,tblventas.serie,tblventas.folio,tblventas.fecha,tblventas.hora,tblclientes.nombre as cnombre,tblclientes.clave as cclave from tblventasinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblinventarioseries on tvi.idinventario=tblinventarioseries.idinventario and tvi.idventa=tblinventarioseries.idventa inner join tblventas on tvi.idventa=tblventas.idventa inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tvi.idventa=" + pidVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasseries")
        'DS.WriteXmlSchema("tblventasseries.xml")
        Return DS.Tables("tblventasseries").DefaultView
    End Function
    Public Sub ReporteMensualCFD(ByVal pFecha As Date, ByVal pRutaArchivo As String, ByVal pidSucursal As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim Fecha1 As Date
        Dim Fecha2 As Date
        'Dim Mes1 As Integer
        'Dim Mes2 As Integer
        Fecha1 = DateSerial(Year(pFecha), Month(pFecha), 1)
        Fecha2 = DateSerial(Year(pFecha), Month(pFecha) + 1, 0)
        Dim S As String = ""
        'If pidSucursal <> 0 Then
        '    Comm.CommandText = "select tblventas.idventa,tblclientes.rfc,tblventas.serie,tblventas.folio,tblventas.noaprobacion,tblventas.yearaprobacion,tblventas.fecha,tblventas.hora,tblventas.totalapagar,tblventas.total,tblventas.estado,tblventas.fechacancelado,tblventas.tipodecambio,tblventas.idconversion from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where ((fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "') or (fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "')) and (tblventas.estado=3 or tblventas.estado=4) and tblventas.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        'Else
        '    Comm.CommandText = "select tblventas.idventa,tblclientes.rfc,tblventas.serie,tblventas.folio,tblventas.noaprobacion,tblventas.yearaprobacion,tblventas.fecha,tblventas.hora,tblventas.totalapagar,tblventas.total,tblventas.estado,tblventas.fechacancelado,tblventas.tipodecambio,tblventas.idconversion from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where ((fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "') or (fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "')) and (tblventas.estado=3 or tblventas.estado=4) order by serie,folio"
        'End If
        'Facturas normales
        If pidSucursal <> 0 Then
            Comm.CommandText = "select tblventas.idventa,tblclientes.rfc,tblventas.serie,tblventas.folio,tblventas.noaprobacion,tblventas.yearaprobacion,tblventas.fecha,tblventas.hora,tblventas.totalapagar,tblventas.total,tblventas.estado,tblventas.fechacancelado,tblventas.tipodecambio,tblventas.idconversion from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "' and (tblventas.estado=3 or tblventas.estado=4) and tblventas.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        Else
            Comm.CommandText = "select tblventas.idventa,tblclientes.rfc,tblventas.serie,tblventas.folio,tblventas.noaprobacion,tblventas.yearaprobacion,tblventas.fecha,tblventas.hora,tblventas.totalapagar,tblventas.total,tblventas.estado,tblventas.fechacancelado,tblventas.tipodecambio,tblventas.idconversion from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "' and (tblventas.estado=3 or tblventas.estado=4) order by serie,folio"
        End If

        DReader = Comm.ExecuteReader
        While DReader.Read
            'Mes1 = Month(CDate(DReader("fecha")))
            'Mes2 = Month(CDate(DReader("fechacancelado")))
            'If Mes1 = Mes2 And DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|I||||" + vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|I||||"
            'Else
            'If DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|I||||"
            'End If
            'If DReader("estado") = Estados.Guardada Then
            If S <> "" Then S += vbCrLf
            S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            S += DReader("serie") + "|"
            S += DReader("folio").ToString + "|"
            S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            If DReader("idconversion") = 2 Then
                S += Format(DReader("totalapagar"), "#0.00") + "|"
                S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            Else
                S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
                S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            End If
            S += "1|I||||"
            'End If
            'End If
        End While
        DReader.Close()


        'Facturas canceladas
        If pidSucursal <> 0 Then
            Comm.CommandText = "select tblventas.idventa,tblclientes.rfc,tblventas.serie,tblventas.folio,tblventas.noaprobacion,tblventas.yearaprobacion,tblventas.fecha,tblventas.hora,tblventas.totalapagar,tblventas.total,tblventas.estado,tblventas.fechacancelado,tblventas.tipodecambio,tblventas.idconversion from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "' and tblventas.estado=4 and tblventas.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        Else
            Comm.CommandText = "select tblventas.idventa,tblclientes.rfc,tblventas.serie,tblventas.folio,tblventas.noaprobacion,tblventas.yearaprobacion,tblventas.fecha,tblventas.hora,tblventas.totalapagar,tblventas.total,tblventas.estado,tblventas.fechacancelado,tblventas.tipodecambio,tblventas.idconversion from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "' and tblventas.estado=4 order by serie,folio"
        End If

        DReader = Comm.ExecuteReader
        While DReader.Read
            'Mes1 = Month(CDate(DReader("fecha")))
            'Mes2 = Month(CDate(DReader("fechacancelado")))
            'If Mes1 = Mes2 And DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|I||||" + vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|I||||"
            'Else
            If DReader("estado") = Estados.Cancelada Then
                If S <> "" Then S += vbCrLf
                S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
                S += DReader("serie") + "|"
                S += DReader("folio").ToString + "|"
                S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
                S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
                If DReader("idconversion") = 2 Then
                    S += Format(DReader("totalapagar"), "#0.00") + "|"
                    S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
                Else
                    S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
                    S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
                End If
                S += "0|I||||"
            End If
            'If DReader("estado") = Estados.Guardada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|I||||"
            'End If
            'End If
        End While
        DReader.Close()

        '--------------------------------------------------------------------------------------------------------------
        '-------------------------------------------------Notas de Crédito-----------------------------------------------
        'If pidSucursal <> 0 Then
        '    Comm.CommandText = "select tblnotasdecredito.idnota,tblclientes.rfc,tblnotasdecredito.serie,tblnotasdecredito.folio,tblnotasdecredito.noaprobacion,tblnotasdecredito.yearaprobacion,tblnotasdecredito.fecha,tblnotasdecredito.hora,tblnotasdecredito.totalapagar,tblnotasdecredito.total,tblnotasdecredito.estado,tblnotasdecredito.fechacancelado,tblnotasdecredito.tipodecambio,tblnotasdecredito.idmoneda from tblnotasdecredito inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente where ((fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "') or (fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "')) and (tblnotasdecredito.estado=3 or tblnotasdecredito.estado=4) and tblnotasdecredito.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        'Else
        '    Comm.CommandText = "select tblnotasdecredito.idnota,tblclientes.rfc,tblnotasdecredito.serie,tblnotasdecredito.folio,tblnotasdecredito.noaprobacion,tblnotasdecredito.yearaprobacion,tblnotasdecredito.fecha,tblnotasdecredito.hora,tblnotasdecredito.totalapagar,tblnotasdecredito.total,tblnotasdecredito.estado,tblnotasdecredito.fechacancelado,tblnotasdecredito.tipodecambio,tblnotasdecredito.idmoneda from tblnotasdecredito inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente where ((fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "') or (fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "')) and (tblnotasdecredito.estado=3 or tblnotasdecredito.estado=4) order by serie,folio"
        'End If
        'NC normales
        If pidSucursal <> 0 Then
            Comm.CommandText = "select tblnotasdecredito.idnota,tblclientes.rfc,tblnotasdecredito.serie,tblnotasdecredito.folio,tblnotasdecredito.noaprobacion,tblnotasdecredito.yearaprobacion,tblnotasdecredito.fecha,tblnotasdecredito.hora,tblnotasdecredito.totalapagar,tblnotasdecredito.total,tblnotasdecredito.estado,tblnotasdecredito.fechacancelado,tblnotasdecredito.tipodecambio,tblnotasdecredito.idmoneda from tblnotasdecredito inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente where fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "' and (tblnotasdecredito.estado=3 or tblnotasdecredito.estado=4) and tblnotasdecredito.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        Else
            Comm.CommandText = "select tblnotasdecredito.idnota,tblclientes.rfc,tblnotasdecredito.serie,tblnotasdecredito.folio,tblnotasdecredito.noaprobacion,tblnotasdecredito.yearaprobacion,tblnotasdecredito.fecha,tblnotasdecredito.hora,tblnotasdecredito.totalapagar,tblnotasdecredito.total,tblnotasdecredito.estado,tblnotasdecredito.fechacancelado,tblnotasdecredito.tipodecambio,tblnotasdecredito.idmoneda from tblnotasdecredito inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente where fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "' and (tblnotasdecredito.estado=3 or tblnotasdecredito.estado=4) order by serie,folio"
        End If
        DReader = Comm.ExecuteReader
        While DReader.Read
            'Mes1 = Month(CDate(DReader("fecha")))
            'Mes2 = Month(CDate(DReader("fechacancelado")))
            'If Mes1 = Mes2 And DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|E||||" + vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|E||||"
            'Else
            'If DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|E||||"
            'End If
            'If DReader("estado") = Estados.Guardada Then
            If S <> "" Then S += vbCrLf
            S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            S += DReader("serie") + "|"
            S += DReader("folio").ToString + "|"
            S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            If DReader("idmoneda") = 2 Then
                S += Format(DReader("totalapagar"), "#0.00") + "|"
                S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            Else
                S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
                S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            End If
            S += "1|E||||"
            'End If
            'End If
        End While
        DReader.Close()

        'NC canceladas
        If pidSucursal <> 0 Then
            Comm.CommandText = "select tblnotasdecredito.idnota,tblclientes.rfc,tblnotasdecredito.serie,tblnotasdecredito.folio,tblnotasdecredito.noaprobacion,tblnotasdecredito.yearaprobacion,tblnotasdecredito.fecha,tblnotasdecredito.hora,tblnotasdecredito.totalapagar,tblnotasdecredito.total,tblnotasdecredito.estado,tblnotasdecredito.fechacancelado,tblnotasdecredito.tipodecambio,tblnotasdecredito.idmoneda from tblnotasdecredito inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente where fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "' and tblnotasdecredito.estado=4 and tblnotasdecredito.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        Else
            Comm.CommandText = "select tblnotasdecredito.idnota,tblclientes.rfc,tblnotasdecredito.serie,tblnotasdecredito.folio,tblnotasdecredito.noaprobacion,tblnotasdecredito.yearaprobacion,tblnotasdecredito.fecha,tblnotasdecredito.hora,tblnotasdecredito.totalapagar,tblnotasdecredito.total,tblnotasdecredito.estado,tblnotasdecredito.fechacancelado,tblnotasdecredito.tipodecambio,tblnotasdecredito.idmoneda from tblnotasdecredito inner join tblclientes on tblnotasdecredito.idcliente=tblclientes.idcliente where fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "' and tblnotasdecredito.estado=4 order by serie,folio"
        End If
        DReader = Comm.ExecuteReader
        While DReader.Read
            'Mes1 = Month(CDate(DReader("fecha")))
            'Mes2 = Month(CDate(DReader("fechacancelado")))
            'If Mes1 = Mes2 And DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|E||||" + vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|E||||"
            'Else
            If DReader("estado") = Estados.Cancelada Then
                If S <> "" Then S += vbCrLf
                S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
                S += DReader("serie") + "|"
                S += DReader("folio").ToString + "|"
                S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
                S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
                If DReader("idmoneda") = 2 Then
                    S += Format(DReader("totalapagar"), "#0.00") + "|"
                    S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
                Else
                    S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
                    S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
                End If
                S += "0|E||||"
            End If
            'If DReader("estado") = Estados.Guardada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|E||||"
            'End If
            'End If
        End While
        DReader.Close()
        '----------------------------------------------------------------------------------------------------------
        '----------------------------------------------Notas de Cargo----------------------------------------------

        'If pidSucursal <> 0 Then
        '    Comm.CommandText = "select tblnotasdecargo.idcargo,tblclientes.rfc,tblnotasdecargo.serie,tblnotasdecargo.folio,tblnotasdecargo.noaprobacion,tblnotasdecargo.yearaprobacion,tblnotasdecargo.fecha,tblnotasdecargo.hora,tblnotasdecargo.totalapagar,tblnotasdecargo.total,tblnotasdecargo.estado,tblnotasdecargo.fechacancelado,tblnotasdecargo.tipodecambio,tblnotasdecargo.idmoneda from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where ((fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "') or (fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "')) and (tblnotasdecargo.estado=3 or tblnotasdecargo.estado=4) and tblnotasdecargo.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        'Else
        '    Comm.CommandText = "select tblnotasdecargo.idcargo,tblclientes.rfc,tblnotasdecargo.serie,tblnotasdecargo.folio,tblnotasdecargo.noaprobacion,tblnotasdecargo.yearaprobacion,tblnotasdecargo.fecha,tblnotasdecargo.hora,tblnotasdecargo.totalapagar,tblnotasdecargo.total,tblnotasdecargo.estado,tblnotasdecargo.fechacancelado,tblnotasdecargo.tipodecambio,tblnotasdecargo.idmoneda from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where ((fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "') or (fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "')) and (tblnotasdecargo.estado=3 or tblnotasdecargo.estado=4) order by serie,folio"
        'End If
        'NCargo normales
        If pidSucursal <> 0 Then
            Comm.CommandText = "select tblnotasdecargo.idcargo,tblclientes.rfc,tblnotasdecargo.serie,tblnotasdecargo.folio,tblnotasdecargo.noaprobacion,tblnotasdecargo.yearaprobacion,tblnotasdecargo.fecha,tblnotasdecargo.hora,tblnotasdecargo.totalapagar,tblnotasdecargo.total,tblnotasdecargo.estado,tblnotasdecargo.fechacancelado,tblnotasdecargo.tipodecambio,tblnotasdecargo.idmoneda from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "' and (tblnotasdecargo.estado=3 or tblnotasdecargo.estado=4)  and tblnotasdecargo.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        Else
            Comm.CommandText = "select tblnotasdecargo.idcargo,tblclientes.rfc,tblnotasdecargo.serie,tblnotasdecargo.folio,tblnotasdecargo.noaprobacion,tblnotasdecargo.yearaprobacion,tblnotasdecargo.fecha,tblnotasdecargo.hora,tblnotasdecargo.totalapagar,tblnotasdecargo.total,tblnotasdecargo.estado,tblnotasdecargo.fechacancelado,tblnotasdecargo.tipodecambio,tblnotasdecargo.idmoneda from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "' and (tblnotasdecargo.estado=3 or tblnotasdecargo.estado=4) order by serie,folio"
        End If
        DReader = Comm.ExecuteReader
        While DReader.Read
            'Mes1 = Month(CDate(DReader("fecha")))
            'Mes2 = Month(CDate(DReader("fechacancelado")))
            'If Mes1 = Mes2 And DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|I||||" + vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|I||||"
            'Else
            'If DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|I||||"
            'End If
            'If DReader("estado") = Estados.Guardada Then
            If S <> "" Then S += vbCrLf
            S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            S += DReader("serie") + "|"
            S += DReader("folio").ToString + "|"
            S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            If DReader("idmoneda") = 2 Then
                S += Format(DReader("totalapagar"), "#0.00") + "|"
                S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            Else
                S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
                S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            End If
            S += "1|I||||"
            'End If
            'End If
        End While
        DReader.Close()

        'NCargo canceladas
        If pidSucursal <> 0 Then
            Comm.CommandText = "select tblnotasdecargo.idcargo,tblclientes.rfc,tblnotasdecargo.serie,tblnotasdecargo.folio,tblnotasdecargo.noaprobacion,tblnotasdecargo.yearaprobacion,tblnotasdecargo.fecha,tblnotasdecargo.hora,tblnotasdecargo.totalapagar,tblnotasdecargo.total,tblnotasdecargo.estado,tblnotasdecargo.fechacancelado,tblnotasdecargo.tipodecambio,tblnotasdecargo.idmoneda from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "' and tblnotasdecargo.estado=4  and tblnotasdecargo.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        Else
            Comm.CommandText = "select tblnotasdecargo.idcargo,tblclientes.rfc,tblnotasdecargo.serie,tblnotasdecargo.folio,tblnotasdecargo.noaprobacion,tblnotasdecargo.yearaprobacion,tblnotasdecargo.fecha,tblnotasdecargo.hora,tblnotasdecargo.totalapagar,tblnotasdecargo.total,tblnotasdecargo.estado,tblnotasdecargo.fechacancelado,tblnotasdecargo.tipodecambio,tblnotasdecargo.idmoneda from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "' and tblnotasdecargo.estado=4 order by serie,folio"
        End If
        DReader = Comm.ExecuteReader
        While DReader.Read
            'Mes1 = Month(CDate(DReader("fecha")))
            'Mes2 = Month(CDate(DReader("fechacancelado")))
            'If Mes1 = Mes2 And DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|I||||" + vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|I||||"
            'Else
            If DReader("estado") = Estados.Cancelada Then
                If S <> "" Then S += vbCrLf
                S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
                S += DReader("serie") + "|"
                S += DReader("folio").ToString + "|"
                S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
                S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
                If DReader("idmoneda") = 2 Then
                    S += Format(DReader("totalapagar"), "#0.00") + "|"
                    S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
                Else
                    S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
                    S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
                End If
                S += "0|I||||"
            End If
            'If DReader("estado") = Estados.Guardada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idmoneda") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|I||||"
            'End If
            'End If
        End While
        DReader.Close()
        '---------------------------------------------------------------------------------------------------------
        '------------------------------------------Devoluciones--------------------------------------------------------
        'If pidSucursal <> 0 Then
        '    Comm.CommandText = "select tbldevoluciones.iddevolucion,tblclientes.rfc,tbldevoluciones.serie,tbldevoluciones.folio,tbldevoluciones.noaprobacion,tbldevoluciones.yearaprobacion,tbldevoluciones.fecha,tbldevoluciones.hora,tbldevoluciones.totalapagar,tbldevoluciones.total,tbldevoluciones.estado,tbldevoluciones.fechacancelado,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion from tbldevoluciones inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente where ((fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "') or (fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "')) and (tbldevoluciones.estado=3 or tbldevoluciones.estado=4) and tbldevoluciones.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        'Else
        '    Comm.CommandText = "select tbldevoluciones.iddevolucion,tblclientes.rfc,tbldevoluciones.serie,tbldevoluciones.folio,tbldevoluciones.noaprobacion,tbldevoluciones.yearaprobacion,tbldevoluciones.fecha,tbldevoluciones.hora,tbldevoluciones.totalapagar,tbldevoluciones.total,tbldevoluciones.estado,tbldevoluciones.fechacancelado,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion from tbldevoluciones inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente where ((fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "') or (fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "')) and (tbldevoluciones.estado=3 or tbldevoluciones.estado=4) order by serie,folio"
        'End If
        'Devoluciones normales
        If pidSucursal <> 0 Then
            Comm.CommandText = "select tbldevoluciones.iddevolucion,tblclientes.rfc,tbldevoluciones.serie,tbldevoluciones.folio,tbldevoluciones.noaprobacion,tbldevoluciones.yearaprobacion,tbldevoluciones.fecha,tbldevoluciones.hora,tbldevoluciones.totalapagar,tbldevoluciones.total,tbldevoluciones.estado,tbldevoluciones.fechacancelado,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion from tbldevoluciones inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente where fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "' and (tbldevoluciones.estado=3 or tbldevoluciones.estado=4) and tbldevoluciones.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        Else
            Comm.CommandText = "select tbldevoluciones.iddevolucion,tblclientes.rfc,tbldevoluciones.serie,tbldevoluciones.folio,tbldevoluciones.noaprobacion,tbldevoluciones.yearaprobacion,tbldevoluciones.fecha,tbldevoluciones.hora,tbldevoluciones.totalapagar,tbldevoluciones.total,tbldevoluciones.estado,tbldevoluciones.fechacancelado,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion from tbldevoluciones inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente where fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "' and (tbldevoluciones.estado=3 or tbldevoluciones.estado=4) order by serie,folio"
        End If

        DReader = Comm.ExecuteReader
        While DReader.Read
            'Mes1 = Month(CDate(DReader("fecha")))
            'Mes2 = Month(CDate(DReader("fechacancelado")))
            'If Mes1 = Mes2 And DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|E||||" + vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|E||||"
            'Else
            'If DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|E||||"
            'End If
            'If DReader("estado") = Estados.Guardada Then
            If S <> "" Then S += vbCrLf
            S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            S += DReader("serie") + "|"
            S += DReader("folio").ToString + "|"
            S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            If DReader("idconversion") = 2 Then
                S += Format(DReader("totalapagar"), "#0.00") + "|"
                S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            Else
                S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
                S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            End If
            S += "1|E||||"
            'End If
            'End If
        End While
        DReader.Close()

        'Devoluciones canceladas
        If pidSucursal <> 0 Then
            Comm.CommandText = "select tbldevoluciones.iddevolucion,tblclientes.rfc,tbldevoluciones.serie,tbldevoluciones.folio,tbldevoluciones.noaprobacion,tbldevoluciones.yearaprobacion,tbldevoluciones.fecha,tbldevoluciones.hora,tbldevoluciones.totalapagar,tbldevoluciones.total,tbldevoluciones.estado,tbldevoluciones.fechacancelado,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion from tbldevoluciones inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente where fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "' and tbldevoluciones.estado=4 and tbldevoluciones.idsucursal=" + pidSucursal.ToString + " order by serie,folio"
        Else
            Comm.CommandText = "select tbldevoluciones.iddevolucion,tblclientes.rfc,tbldevoluciones.serie,tbldevoluciones.folio,tbldevoluciones.noaprobacion,tbldevoluciones.yearaprobacion,tbldevoluciones.fecha,tbldevoluciones.hora,tbldevoluciones.totalapagar,tbldevoluciones.total,tbldevoluciones.estado,tbldevoluciones.fechacancelado,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion from tbldevoluciones inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente where fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "' and tbldevoluciones.estado=4 order by serie,folio"
        End If

        DReader = Comm.ExecuteReader
        While DReader.Read
            'Mes1 = Month(CDate(DReader("fecha")))
            'Mes2 = Month(CDate(DReader("fechacancelado")))
            'If Mes1 = Mes2 And DReader("estado") = Estados.Cancelada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|E||||" + vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "0|E||||"
            'Else
            If DReader("estado") = Estados.Cancelada Then
                If S <> "" Then S += vbCrLf
                S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
                S += DReader("serie") + "|"
                S += DReader("folio").ToString + "|"
                S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
                S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
                If DReader("idconversion") = 2 Then
                    S += Format(DReader("totalapagar"), "#0.00") + "|"
                    S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
                Else
                    S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
                    S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
                End If
                S += "0|E||||"
            End If
            'If DReader("estado") = Estados.Guardada Then
            '    If S <> "" Then S += vbCrLf
            '    S += "|" + Replace(Replace(Trim(DReader("rfc")), " ", ""), "-", "") + "|"
            '    S += DReader("serie") + "|"
            '    S += DReader("folio").ToString + "|"
            '    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
            '    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
            '    If DReader("idconversion") = 2 Then
            '        S += Format(DReader("totalapagar"), "#0.00") + "|"
            '        S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
            '    Else
            '        S += Format(DReader("totalapagar") * DReader("tipodecambio"), "#0.00") + "|"
            '        S += Format((DReader("totalapagar") - DReader("total")) * DReader("tipodecambio"), "#0.00") + "|"
            '    End If
            '    S += "1|E||||"
            'End If
            'End If
        End While
        DReader.Close()

        Dim Enc As New System.Text.UTF8Encoding
        Dim Bytes() As Byte = Enc.GetBytes(S)
        Dim en As New Encriptador
        en.GuardaArchivo(pRutaArchivo, Bytes)
    End Sub

    Public Sub ModificaInventario(ByVal pId As Integer, ByVal pPorSurtir As Byte)
        If pPorSurtir = 0 Then
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,cantidad-surtido,0,1,0),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen,cantidad-surtido,0,1),0) from tblventasinventario where idventa=" + pId.ToString + ";"
            Comm.CommandText += "update tblventasinventario set surtido=cantidad where idventa=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,cantidad-surtido,0,1,0) from tblventaskits where idventa=" + pId.ToString + ";"
            Comm.CommandText += "update tblventaskits set surtido=cantidad where idventa=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select spmodificainventariolotesf(tblventaslotes.idlote,tblventaslotes.cantidad-tblventaslotes.surtido,0,1,0) from tblventaslotes inner join tblventasinventario on tblventaslotes.iddetalle=tblventasinventario.idventasinventario where tblventasinventario.idventa=" + pId.ToString + ";"
            Comm.CommandText += "update tblventaslotes inner join tblventasinventario on tblventaslotes.iddetalle=tblventasinventario.idventasinventario set tblventaslotes.surtido=tblventaslotes.cantidad where idventa=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioaduanaf(tblventasaduanan.idaduana,tblventasaduanan.cantidad-tblventasaduanan.surtido,0,1,0) from tblventasaduanan inner join tblventasinventario on tblventasaduanan.iddetalle=tblventasinventario.idventasinventario where tblventasinventario.idventa=" + pId.ToString + ";"
            Comm.CommandText += "update tblventasaduanan inner join tblventasinventario on tblventasaduanan.iddetalle=tblventasinventario.idventasinventario set tblventasaduanan.surtido=tblventasaduanan.cantidad where idventa=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Function VerificaExistencias(ByVal pId As Integer) As String
        Dim Str As String = ""
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Cont As Integer = 1
        Comm.CommandText = "select idinventario from tblventasinventario where idventa=" + pId.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read()
            IDs.Add(DReader("idinventario"))
        End While
        DReader.Close()
        Dim I As New dbInventario(MySqlcon)
        Dim P As New dbProductos(MySqlcon)
        Dim iIdInventario As Integer
        'Dim iIdVariante As Integer
        Dim iCantidad As Double
        Dim iIdAlmacen As Integer
        Dim iCantidad2 As Double
        Dim EsInventariable As Integer
        Dim iContenido As Double
        While Cont <= IDs.Count
            'Comm.CommandText = "select idinventario,idvariante,cantidad,idalmacen,if(idinventario>1,(select inventariable from tblinventario where tblinventario.idinventario=tblventasinventario.idinventario),0) as esinventariable,if(idvariante>1,(select inventariable from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventasinventario.idvariante),0) as esinventariablep,(select contenido from tblinventario where tblinventario.idinventario=tblventasinventario.idinventario) as contenido from tblventasinventario where idventasinventario=" + IDs(Cont).ToString
            Comm.CommandText = "select tblinventario.inventariable as esinventariable,tblinventario.contenido,ifnull((sum(tblventasinventario.cantidad)),0) as cantidad,tblventasinventario.idalmacen from tblventasinventario inner join tblinventario on tblinventario.idinventario=tblventasinventario.idinventario where tblventasinventario.idventa=" + pId.ToString + " and tblventasinventario.idinventario=" + IDs(Cont).ToString + " limit 1"
            DReader = Comm.ExecuteReader
            If DReader.Read() Then
                iIdInventario = IDs(Cont)
                iCantidad = DReader("cantidad")
                'iIdVariante = DReader("idvariante")
                iIdAlmacen = DReader("idalmacen")
                EsInventariable = DReader("esinventariable")
                iContenido = DReader("contenido")
                DReader.Close()
                If iIdInventario > 1 And EsInventariable = 1 Then
                    iCantidad2 = I.DaInventario(iIdAlmacen, iIdInventario)
                    If iContenido <> 1 Then
                        iCantidad2 = iCantidad2 * iContenido
                    End If
                    If iCantidad > iCantidad2 Then
                        Str = " Hay artículos con insuficiente inventario."
                    End If
                End If
                'If iIdVariante > 1 Then
                '    Comm.CommandText = "select spverificaexistenciarecetas(" + iIdVariante.ToString + "," + iIdAlmacen.ToString + "," + iCantidad.ToString + ")"
                '    Str = Comm.ExecuteScalar
                'End If
            Else
                DReader.Close()
            End If
            Cont += 1
        End While

        ''Verifica Kits
        Cont = 1
        Comm.CommandText = "select idinventario from tblventaskits where idventa=" + pId.ToString
        DReader = Comm.ExecuteReader
        IDs.Clear()
        While DReader.Read()
            IDs.Add(DReader("idinventario"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            'Comm.CommandText = "select idinventario,idvariante,cantidad,idalmacen,if(idinventario>1,(select inventariable from tblinventario where tblinventario.idinventario=tblventasinventario.idinventario),0) as esinventariable,if(idvariante>1,(select inventariable from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventasinventario.idvariante),0) as esinventariablep,(select contenido from tblinventario where tblinventario.idinventario=tblventasinventario.idinventario) as contenido from tblventasinventario where idventasinventario=" + IDs(Cont).ToString
            Comm.CommandText = "select tblinventario.inventariable as esinventariable,tblinventario.contenido,ifnull((sum(tblventaskits.cantidad)),0) as cantidad,tblventaskits.idalmacen from tblventaskits inner join tblinventario on tblinventario.idinventario=tblventaskits.idinventario where tblventaskits.idventa=" + pId.ToString + " and tblventaskits.idinventario=" + IDs(Cont).ToString + " limit 1"
            DReader = Comm.ExecuteReader
            If DReader.Read() Then
                iIdInventario = IDs(Cont)
                iCantidad = DReader("cantidad")
                'iIdVariante = DReader("idvariante")
                iIdAlmacen = DReader("idalmacen")
                EsInventariable = DReader("esinventariable")
                iContenido = DReader("contenido")
                DReader.Close()
                If iIdInventario > 1 And EsInventariable = 1 Then
                    iCantidad2 = I.DaInventario(iIdAlmacen, iIdInventario)
                    If iContenido <> 1 Then
                        iCantidad2 = iCantidad2 * iContenido
                    End If
                    If iCantidad > iCantidad2 Then
                        Str = " Hay artículos con insuficiente inventario para completar un kit."
                    End If
                End If
                'If iIdVariante > 1 Then
                '    Comm.CommandText = "select spverificaexistenciarecetas(" + iIdVariante.ToString + "," + iIdAlmacen.ToString + "," + iCantidad.ToString + ")"
                '    Str = Comm.ExecuteScalar
                'End If
            Else
                DReader.Close()
            End If
            Cont += 1
        End While


        Return Str
    End Function

    Private Function ValidarCertificadoRemoto(ByVal sender As Object, ByVal certificate As Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As Security.Cryptography.X509Certificates.X509Chain, ByVal policyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function
    Public Function Timbrar2(ByVal pUsuario As String, ByVal pPassword As String, ByVal pRFC As String, ByVal pRutaXML As String, ByVal pRutaSalida As String, ByVal pConector As Byte, ByVal conMsgBox As Boolean) As Integer
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
            If conMsgBox Then
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
                MensajeError = StrCancelacion
                Return 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Return 0
        End Try
    End Function
    Public Function CancelarTimbrado2(ByVal pRFC As String, ByVal pUUID As String, ByVal pAPIKey As String) As Integer
        Dim Cadena As String = ""
        Dim Cancela As New facturafielcancelacion.server
        If pUUID.Contains("Ambiente") Then Return 1
        Cancela.Url = "http://www.facturafiel.com/websrv/servicio_cancelacion.php?wsdl"
        Cadena = pRFC + "~" + pAPIKey + "~" + pUUID
        Cadena = Cancela.servicio_cancelacion(Cadena)
        If Cadena.Contains("EXITOSAMENTE") Then
            Return 1
        Else
            MensajeError = Cadena
            Return 0
        End If
    End Function
    Public Function Timbrar(ByVal RFCEmisor As String, ByVal RFCCliente As String, ByVal ArchivoCer As String, ByVal CerPassword As String, ByVal strXml As String, ByVal pDireccionTimbrado As String, ByVal conMsgBox As Boolean) As TimbreFiscal.TimbreFiscalDigital
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
            If conMsgBox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError = ex.Message
            End If
            Return T
        End Try

    End Function

    Public Sub GuardaDatosTimbrado(ByVal pidVenta As Integer, ByVal pUuid As String, ByVal pFechaTimbrado As String, ByVal pSellocfd As String, ByVal pNoCertificadoSat As String, ByVal pSelloSat As String)
        Comm.CommandText = "insert into tblventastimbrado(idventa,uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat) values(" + pidVenta.ToString + ",'" + Replace(pUuid, "'", "''") + "','" + Replace(pFechaTimbrado, "'", "''") + "','" + Replace(pSellocfd, "'", "''") + "','" + Replace(pNoCertificadoSat, "'", "''") + "','" + Replace(pSelloSat, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub DaDatosTimbrado(ByVal pidVenta As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventastimbrado where idventa=" + pidVenta.ToString
        DReader = Comm.ExecuteReader
        uuid = "**No Timbrado**"
        SelloCFD = ""
        If DReader.Read Then
            uuid = DReader("uuid")
            FechaTimbrado = DReader("fechatimbrado")
            SelloCFD = DReader("sellocfd")
            NoCertificadoSAT = DReader("nocertificadosat")
            SelloSAT = DReader("sellosat")
        End If
        DReader.Close()
    End Sub
    Public Function ReporteNotasDeCreditoPorConcepto(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pIdConcepto As Integer, ByVal pSerie As String, ByVal pSoloCanceladas As Boolean) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idnota idventa,v.folio,v.serie,v.estado,if(v.idmoneda=2,ifnull((select sum(precio) from tblnotasdecreditodetalles where idnota=v.idnota),0),ifnull((select sum(precio) from tblnotasdecreditodetalles where idnota=v.idnota),0)*v.tipodecambio) as totalapagar," + _
            "if(v.idmoneda=2,ifnull((select sum((precio/(1+(iva/100)))) from tblnotasdecreditodetalles where idnota=v.idnota),0),ifnull((select sum((precio/(1+(iva/100)))) from tblnotasdecreditodetalles where idnota=v.idnota),0)*v.tipodecambio) as total,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.idconcepto,cnv.nombre formadepago " + _
            "from tblnotasdecredito v inner join tblclientes c on v.idcliente=c.idcliente inner join tblconceptosnotasventas cnv on v.idconcepto=cnv.idconceptonotaventa where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idnota idventa,v.folio,v.serie,v.estado,ifnull((select sum(precio) from tblnotasdecreditodetalles where idnota=v.idnota),0) as totalapagar,ifnull((select sum((precio/(1+(iva/100)))) from tblnotasdecreditodetalles where idnota=v.idnota),0) as total,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.idconcepto,cnv.nombre formadepago " + _
            "from tblnotasdecredito v inner join tblclientes c on v.idcliente=c.idcliente inner join tblconceptosnotasventas cnv on v.idconcepto=cnv.idconceptonotaventa where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pidMoneda > 0 Then Comm.CommandText += " and v.idmoneda=" + pidMoneda.ToString
        If pIdConcepto > 0 Then
            Comm.CommandText += " and v.idconcepto=" + pIdConcepto.ToString
        End If
        If pSoloCanceladas Then
            Comm.CommandText += " and v.estado=4"
        Else
            Comm.CommandText += " and (v.estado=3 or v.estado=4)"
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        Comm.CommandText += " order by v.fecha,v.serie,v.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventas.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Function ReporteNotasDeCargoPorConcepto(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pIdConcepto As Integer, ByVal pSerie As String, ByVal pSoloCanceladas As Boolean) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idcargo idventa,v.folio,v.serie,v.estado,if(v.idmoneda=2,v.total,v.total*v.tipodecambio) as total,if(v.idmoneda=2,v.totalapagar,v.totalapagar*v.tipodecambio) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.idconcepto,cnv.nombre formadepago " + _
            "from tblnotasdecargo v inner join tblclientes c on v.idcliente=c.idcliente inner join tblconceptosnotasventas cnv on v.idconcepto=cnv.idconceptonotaventa where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idcargo idventa,v.folio,v.serie,v.estado,v.total,v.totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.idconcepto,cnv.nombre formadepago " + _
            "from tblnotasdecargo v inner join tblclientes c on v.idcliente=c.idcliente inner join tblconceptosnotasventas cnv on v.idconcepto=cnv.idconceptonotaventa where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pidMoneda > 0 Then Comm.CommandText += " and v.idmoneda=" + pidMoneda.ToString
        If pIdConcepto > 0 Then
            Comm.CommandText += " and v.idconcepto=" + pIdConcepto.ToString
        End If
        If pSoloCanceladas Then
            Comm.CommandText += " and v.estado=4"
        Else
            Comm.CommandText += " and (v.estado=3 or v.estado=4)"
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        Comm.CommandText += " order by v.fecha,v.serie,v.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventas.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function

    Public Function Pagare(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pIdCliente As Integer, ByVal pFolio As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select v.idventa,0 selected,v.fecha Fecha,concat(v.serie,convert(v.folio using utf8)) referencia,c.clave,c.nombre cliente,v.totalapagar Importe,v.credito Abonado,v.totalapagar-v.credito Saldo,concat(c.direccion,' ',c.noexterior,' ',c.nointerior,' ',c.colonia,' ',c.ciudad,', ',c.estado) Domicilio,abreviatura Moneda from tblventas v inner join tblclientes c on v.idcliente=c.idcliente inner join tblformasdepago fp on v.idforma=fp.idforma INNER JOIN tblmonedas m on m.idmoneda=v.idconversion inner join tblformasdepago on tblformasdepago.idforma=v.idforma where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "' and v.estado=3 and tblformasdepago.tipo=0 and round(v.totalapagar-v.credito,2)>0 and c.idcliente=" + pIdCliente.ToString + " and concat(v.serie,convert(v.folio using utf8)) like '%" + Replace(Trim(pFolio), "'", "''") + "%' order by v.fecha,v.serie,v.folio;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("pagare.xml")
        Return DS.Tables("tblventas")
    End Function
    Public Sub ActualizaComentario(ByVal pidventa As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblventas set comentariof='" + Replace(pTexto, "'", "''") + "' where idventa=" + pidventa.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaDescuento(ByVal pidventa As Integer, ByVal pDescuento As Double, pDescuento2 As Double, pImpLoc As Double)
        Comm.CommandText = "update tblventas set descuentog='" + Replace(pDescuento, "'", "''") + "',descuentog2=" + pDescuento2.ToString + ",sobreimploc=" + pImpLoc.ToString + " where idventa=" + pidventa.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function RevisaConceptos(ByVal pIdVenta As Integer) As Boolean
        Dim C1 As Integer
        Dim C2 As Integer
        Comm.CommandText = "select count(idmoneda) from tblventasinventario where idventa=" + pIdVenta.ToString + " and idmoneda=2"
        C1 = Comm.ExecuteScalar
        Comm.CommandText = "select count(idmoneda) from tblventasinventario where idventa=" + pIdVenta.ToString + " and idmoneda<>2"
        C2 = Comm.ExecuteScalar
        If C1 <> 0 And C2 <> 0 Then
            Return False
        End If
        Return True
    End Function
    Public Sub ObtenerFacturaOriginal(ByVal pidventa As Integer)
        Dim iEsElectronica As Byte = 0
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select folio,serie,fecha,hora,totalapagar,eselectronica from tblventas where idventa=" + pidventa.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read Then
            FolioOrigen = DReader("folio")
            SerieOrigen = DReader("serie")
            FechaOrigen = Replace(DReader("fecha"), "/", "-") + "T" + DReader("hora")
            MontoOrigen = DReader("totalapagar")
            iEsElectronica = DReader("eselectronica")
        End If
        DReader.Close()
        If iEsElectronica > 1 Then
            Comm.CommandText = "select * from tblventastimbrado where idventa=" + pidventa.ToString
            DReader = Comm.ExecuteReader
            If DReader.Read Then
                FolioUUIDOrigen = DReader("uuid")
                FechaOrigen = DReader("fechatimbrado")
            End If
            DReader.Close()
        Else
            FolioUUIDOrigen = ""
        End If

        'Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
    End Sub
    Public Function DaCantidadParcialidades(ByVal pIdVenta As Integer) As Integer
        Comm.CommandText = "select ifnull((select count(idventa) from tblventas where idventaorigen=" + pIdVenta.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaTotalxIVa(ByVal pidVenta As Integer, ByVal pIva As Double) As Double
        Comm.CommandText = "select sum(tblventasinventario.precio) from tblventasinventario inner join tblventas on tblventasinventario.idventa=tblventas.idventa where tblventas.idventa=" + pidVenta.ToString + " and tblventasinventario.iva=" + pIva.ToString
        Return Comm.ExecuteScalar
    End Function
    Public Function DaTotalGravado(ByVal pidVenta As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(tblventasinventario.precio) from tblventasinventario where tblventasinventario.idventa=" + pidVenta.ToString + " and (tblventasinventario.iva<>0 or tblventasinventario.ieps<>0)),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaTotalNoGravado(ByVal pidVenta As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(tblventasinventario.precio) from tblventasinventario where tblventasinventario.idventa=" + pidVenta.ToString + " and tblventasinventario.iva=0 and tblventasinventario.ieps=0 and tblventasinventario.ivaretenido=0),0)"
        Return Comm.ExecuteScalar
    End Function
    'Public Function ReportexVendedor(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidinventario As Integer, ByVal pidclasificacion1 As Integer, ByVal pidclasificacion2 As Integer, ByVal pidclasificacion3 As Integer, ByVal pSoloCanceladas As Boolean, ByVal pSerie As String) As DataView
    '    Dim DS As New DataSet
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,round(tblventas.total,2),round(tblventas.total*tblventas.tipodecambio,2)) as total,if(tblventas.idconversion=2,round(tblventas.totalapagar,2),round(tblventas.totalapagar*tblventas.tipodecambio,2)) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2) as cantidad,tblventasinventario.descripcion,round(tblventasinventario.precio,2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido,tblvendedores.nombre as vnombre,tblventas.idvendedor " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    Else
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,round(tblventas.total,2) as total,round(tblventas.totalapagar,2) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2),tblventasinventario.descripcion,round(tblventasinventario.precio,2),0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido,tblvendedores.nombre as vnombre,tblventas.idvendedor  " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    End If
    '    If pSoloCanceladas Then
    '        Comm.CommandText += " and tblventas.estado=4"
    '    Else
    '        Comm.CommandText += " and tblventas.estado=3"
    '    End If
    '    'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdCliente > 0 Then
    '        Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
    '    End If
    '    If pidVendedor > 0 Then
    '        Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
    '    End If
    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
    '    End If
    '    If pidinventario > 1 Then
    '        Comm.CommandText += " and tblventasinventario.idinventario=" + pidinventario.ToString
    '    Else
    '        If pidclasificacion1 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion=" + pidclasificacion1.ToString
    '        End If
    '        If pidclasificacion2 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidclasificacion2.ToString
    '        End If
    '        If pidclasificacion3 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidclasificacion3.ToString
    '        End If
    '    End If
    '    If pSerie <> "" Then
    '        Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
    '    End If
    '    Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio,tblventas.idvendedor"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblventas")
    '    'DS.WriteXmlSchema("tblventasxv.xml")
    '    Return DS.Tables("tblventas").DefaultView
    'End Function
    Public Function ReportexVendedor(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidinventario As Integer, ByVal pidclasificacion1 As Integer, ByVal pidclasificacion2 As Integer, ByVal pidclasificacion3 As Integer, ByVal pSoloCanceladas As Boolean, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer, pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,round(tblventas.total,2),round(tblventas.total*tblventas.tipodecambio,2)) as total,if(tblventas.idconversion=2,round(tblventas.totalapagar,2),round(tblventas.totalapagar*tblventas.tipodecambio,2)) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2) as cantidad,tblventasinventario.descripcion,round(tblventasinventario.precio,2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido,tblvendedores.nombre as vnombre,tblventas.idvendedor,tblventasinventario.ieps,tblventasinventario.ivaretenido ivaret " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma  inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,round(tblventas.total,2) as total,round(tblventas.totalapagar,2) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2),tblventasinventario.descripcion,round(tblventasinventario.precio,2),0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido,tblvendedores.nombre as vnombre,tblventas.idvendedor,tblventasinventario.ieps,tblventasinventario.ivaretenido ivaret  " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma  inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If
        If pSoloCanceladas Then
            Comm.CommandText += " and tblventas.estado=4"
        Else
            Comm.CommandText += " and (tblventas.estado=3 or tblventas.estado=4)"
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and tblclientes.zona='" + pZona2.ToString() + "' or tblclientes.zona2='" + pZona2.ToString() + "'"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        If pidinventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pidinventario.ToString
        Else
            If pidclasificacion1 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidclasificacion1.ToString
            End If
            If pidclasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidclasificacion2.ToString
            End If
            If pidclasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidclasificacion3.ToString
            End If
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio,tblventas.idvendedor"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventasxv.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    'Public Function ReporteVentasArticulosClas(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pConcendaso As Boolean, ByVal pSerie As String) As DataView
    '    Dim DS As New DataSet
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblinventario.nombre as descripcion,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio),2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio/tblventasinventario.cantidad,tblventasinventario.precio*tblventas.tipodecambio/tblventasinventario.cantidad),2) as preciou,tblinventario.clave,(select nombre from tblinventarioclasificaciones where idclasificacion=tblinventario.idclasificacion) as nclase1,(select nombre from tblinventarioclasificaciones2 where idclasificacion=tblinventario.idclasificacion2) as nclase2,(select nombre from tblinventarioclasificaciones3 where idclasificacion=tblinventario.idclasificacion3) as nclase3,tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3,tblventas.porsurtir " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo<>2 and tblventasinventario.cantidad<>0"
    '    Else
    '        Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblinventario.nombre as descripcion,round(tblventasinventario.precio,2),0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(tblventasinventario.precio/tblventasinventario.cantidad,2) as preciou,tblinventario.clave,(select nombre from tblinventarioclasificaciones where idclasificacion=tblinventario.idclasificacion) as nclase1,(select nombre from tblinventarioclasificaciones2 where idclasificacion=tblinventario.idclasificacion2) as nclase2,(select nombre from tblinventarioclasificaciones3 where idclasificacion=tblinventario.idclasificacion3) as nclase3,tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3,tblventas.porsurtir " + _
    '        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventasinventario.cantidad<>0"
    '    End If
    '    'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdCliente > 0 Then
    '        Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
    '    End If
    '    If pidVendedor > 0 Then
    '        Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
    '    End If
    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
    '    End If
    '    If pIdInventario > 1 Then
    '        Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
    '    Else
    '        If pidClasificacion > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
    '        End If
    '        If pidClasificacion2 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
    '        End If
    '        If pidClasificacion3 > 0 Then
    '            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
    '        End If
    '    End If

    '    If pSerie <> "" Then
    '        Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
    '    End If

    '    If pConcendaso = False Then
    '        Comm.CommandText += " order by tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3,tblventas.fecha,tblventas.serie,tblventas.folio"
    '    Else
    '        Comm.CommandText += " order by tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3,tblventasinventario.idinventario,preciou"
    '    End If
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblventas")
    '    DS.WriteXmlSchema("tblventasclas.xml")
    '    Return DS.Tables("tblventas").DefaultView
    'End Function
    Public Function ReporteVentasArticulosClas(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pConcendaso As Boolean, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer, pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblinventario.nombre as descripcion,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio),2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio/tblventasinventario.cantidad,tblventasinventario.precio*tblventas.tipodecambio/tblventasinventario.cantidad),2) as preciou,tblinventario.clave,(select nombre from tblinventarioclasificaciones where idclasificacion=tblinventario.idclasificacion) as nclase1,(select nombre from tblinventarioclasificaciones2 where idclasificacion=tblinventario.idclasificacion2) as nclase2,(select nombre from tblinventarioclasificaciones3 where idclasificacion=tblinventario.idclasificacion3) as nclase3,tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3,tblventas.porsurtir,tblventasinventario.ivaretenido ivar,tblventasinventario.ieps " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo<>2 and tblventasinventario.cantidad<>0"
        Else
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblinventario.nombre as descripcion,round(tblventasinventario.precio,2),0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(tblventasinventario.precio/tblventasinventario.cantidad,2) as preciou,tblinventario.clave,(select nombre from tblinventarioclasificaciones where idclasificacion=tblinventario.idclasificacion) as nclase1,(select nombre from tblinventarioclasificaciones2 where idclasificacion=tblinventario.idclasificacion2) as nclase2,(select nombre from tblinventarioclasificaciones3 where idclasificacion=tblinventario.idclasificacion3) as nclase3,tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3,tblventas.porsurtir,tblventasinventario.ivaretenido ivar,tblventasinventario.ieps " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblventas.estado=3 and tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventasinventario.cantidad<>0"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and tblclientes.zona='" + pZona2.ToString() + "' or tblclientes.zona2='" + pZona2.ToString() + "'"
        End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            End If
            If pidClasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
            End If
            If pidClasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
            End If
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If

        If pConcendaso = False Then
            Comm.CommandText += " order by tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3,tblventas.fecha,tblventas.serie,tblventas.folio"
        Else
            Comm.CommandText += " order by tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3,tblventasinventario.idinventario,preciou"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventasclas.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Function ReporteVentasPorSurtir(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pConcendaso As Boolean, ByVal pSerie As String, ByVal pTipo As Byte, pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        'If pMostrarEnPesos = 0 Then
        'idcliente,nombre,idinventario,cantidad,cantidad surtida,cantidad por surtir,
        Comm.CommandText = "delete from tblrepventasporsurtir"
        Comm.ExecuteNonQuery()
        'Agrega Facturas
        Comm.CommandText = "insert into tblrepventasporsurtir(folio,serie,fecha,idcliente,nombre,clave,inombre,cantidad,csurtido,tipo) select tblventas.folio,tblventas.serie,tblventas.fecha,tblventas.idcliente,tblclientes.nombre,tblinventario.clave,tblinventario.nombre as inombre,sum(tblventasinventario.cantidad)," + _
        "ifnull((select sum(cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=tblventas.idventa and tblmovimientosdetalles.idinventario=tblventasinventario.idinventario and tblmovimientos.estado=3),0) as csurtido,'F' as tipo  " + _
        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblclientes on tblclientes.idcliente=tblventas.idcliente inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario where tblventas.porsurtir=1 and tblventas.estado=3 "
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblinventario.nombre as descripcion,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio),2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio/tblventasinventario.cantidad,tblventasinventario.precio*tblventas.tipodecambio/tblventasinventario.cantidad),2) as preciou,tblinventario.clave,(select nombre from tblinventarioclasificaciones where idclasificacion=tblinventario.idclasificacion) as nclase1,(select nombre from tblinventarioclasificaciones2 where idclasificacion=tblinventario.idclasificacion2) as nclase2,(select nombre from tblinventarioclasificaciones3 where idclasificacion=tblinventario.idclasificacion3) as nclase3,tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3 " + _
        '"from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo<>2 and tblventasinventario.cantidad<>0"
        'End If
        If pTipo = 0 Then
            Comm.CommandText += " and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If
        If pTipo = 1 Then
            Comm.CommandText += " and (ifnull((select sum(vi.cantidad) from tblventasinventario vi where vi.idventa=tblventas.idventa and vi.idinventario=tblventasinventario.idinventario),0)-ifnull((select sum(cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=tblventas.idventa and tblmovimientosdetalles.idinventario=tblventasinventario.idinventario and tblmovimientos.estado=3),0))>0"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
        'End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            End If
            If pidClasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
            End If
            If pidClasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
            End If
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        Comm.CommandText += " group by tblventas.serie,tblventas.folio,tblventasinventario.idinventario"

        Comm.ExecuteNonQuery()


        'Agrega Remisiones


        Comm.CommandText = "insert into tblrepventasporsurtir(folio,serie,fecha,idcliente,nombre,clave,inombre,cantidad,csurtido,tipo) select tblventasremisiones.folio,tblventasremisiones.serie,tblventasremisiones.fecha,tblventasremisiones.idcliente,tblclientes.nombre,tblinventario.clave,tblinventario.nombre as inombre,sum(tblventasremisionesinventario.cantidad)," + _
        "ifnull((select sum(cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=tblventasremisiones.idremision and tblmovimientosdetalles.idinventario=tblventasremisionesinventario.idinventario and tblmovimientos.estado=3),0) as csurtido,'R' as tipo  " + _
        "from tblventasremisiones inner join tblventasremisionesinventario on tblventasremisiones.idremision=tblventasremisionesinventario.idremision inner join tblclientes on tblclientes.idcliente=tblventasremisiones.idcliente inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario where tblventasremisiones.porsurtir=1 and tblventasremisiones.estado=3 and idventar=0"
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblinventario.nombre as descripcion,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio),2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio/tblventasinventario.cantidad,tblventasinventario.precio*tblventas.tipodecambio/tblventasinventario.cantidad),2) as preciou,tblinventario.clave,(select nombre from tblinventarioclasificaciones where idclasificacion=tblinventario.idclasificacion) as nclase1,(select nombre from tblinventarioclasificaciones2 where idclasificacion=tblinventario.idclasificacion2) as nclase2,(select nombre from tblinventarioclasificaciones3 where idclasificacion=tblinventario.idclasificacion3) as nclase3,tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3 " + _
        '"from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo<>2 and tblventasinventario.cantidad<>0"
        'End If
        If pTipo = 0 Then
            Comm.CommandText += " and tblventasremisiones.fecha>='" + pFecha1 + "' and tblventasremisiones.fecha<='" + pFecha2 + "'"
        End If
        If pTipo = 1 Then
            Comm.CommandText += " and (ifnull((select sum(vi.cantidad) from tblventasremisionesinventario vi where vi.idremision=tblventasremisiones.idremision and vi.idinventario=tblventasremisionesinventario.idinventario),0)-ifnull((select sum(cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=tblventasremisiones.idremision and tblmovimientosdetalles.idinventario=tblventasremisionesinventario.idinventario and tblmovimientos.estado=3),0))>0"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventasremisiones.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventasremisiones.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventasremisiones.idvendedor=" + pidVendedor.ToString
        End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
        'End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tblventasremisionesinventario.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            End If
            If pidClasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
            End If
            If pidClasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
            End If
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasremisionesinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventasremisiones.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        Comm.CommandText += " group by tblventasremisiones.serie,tblventasremisiones.folio,tblventasremisionesinventario.idinventario"
        Comm.ExecuteNonQuery()




        'If pConcendaso = False Then
        Comm.CommandText = "select * from tblrepventasporsurtir order by idcliente,fecha,serie,folio"
        'Else
        '    Comm.CommandText += " order by tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3,tblventasinventario.idinventario,preciou"
        'End If


        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioporsurtir")
        'DS.WriteXmlSchema("tblinventarioporsurtir.xml")
        Return DS.Tables("tblinventarioporsurtir").DefaultView
    End Function

    Public Function ReporteVentasPorSurtirDetallado(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pConcendaso As Boolean, ByVal pSerie As String, ByVal pTipo As Byte, pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        'If pMostrarEnPesos = 0 Then
        'idcliente,nombre,idinventario,cantidad,cantidad surtida,cantidad por surtir,
        Comm.CommandText = "delete from tblrepventasporsurtird"
        Comm.ExecuteNonQuery()
        'Agrega Facturas
        Comm.CommandText = "insert into tblrepventasporsurtird(folio,serie,fecha,idcliente,nombre,clave,inombre,cantidad,csurtido,tipo,cantidadm,fecham,seriem,foliom,idventa,idmovimiento) select tblventas.folio,tblventas.serie,tblventas.fecha,tblventas.idcliente,tblclientes.nombre,tblinventario.clave,tblinventario.nombre as inombre,tblventasinventario.cantidad," + _
        "ifnull((select sum(cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=tblventas.idventa and tblmovimientosdetalles.idinventario=tblventasinventario.idinventario and tblmovimientos.estado=3),0) as csurtido,'F' as tipo,  " + _
        "tblmovimientosdetalles.cantidad,tblmovimientos.fecha,tblmovimientos.serie,tblmovimientos.folio,tblventas.idventa,tblmovimientos.idmovimiento  " + _
        "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblclientes on tblclientes.idcliente=tblventas.idcliente inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblmovimientos on tblmovimientos.idventa=tblventas.idventa" + _
        " inner join tblmovimientosdetalles on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblventas.porsurtir=1 and tblventas.estado=3 and tblmovimientos.estado=3 "
        If pTipo = 0 Then
            Comm.CommandText += " and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If
        If pTipo = 1 Then
            Comm.CommandText += " and (tblventasinventario.cantidad-ifnull((select sum(cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=tblventas.idventa and tblmovimientosdetalles.idinventario=tblventasinventario.idinventario),0))>0"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
        'End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            End If
            If pidClasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
            End If
            If pidClasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
            End If
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        Comm.ExecuteNonQuery()


        'Agrega Remisiones


        Comm.CommandText = "insert into tblrepventasporsurtird(folio,serie,fecha,idcliente,nombre,clave,inombre,cantidad,csurtido,tipo,cantidadm,fecham,seriem,foliom,idventa,idmovimiento) select tblventasremisiones.folio,tblventasremisiones.serie,tblventasremisiones.fecha,tblventasremisiones.idcliente,tblclientes.nombre,tblinventario.clave,tblinventario.nombre as inombre,tblventasremisionesinventario.cantidad," + _
        "ifnull((select sum(cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=tblventasremisiones.idremision and tblmovimientosdetalles.idinventario=tblventasremisionesinventario.idinventario and tblmovimientos.estado=3),0) as csurtido,'R' as tipo," + _
        "tblmovimientosdetalles.cantidad,tblmovimientos.fecha,tblmovimientos.serie,tblmovimientos.folio,tblventasremisiones.idremision,tblmovimientos.idmovimiento " + _
        "from tblventasremisiones inner join tblventasremisionesinventario on tblventasremisiones.idremision=tblventasremisionesinventario.idremision inner join tblclientes on tblclientes.idcliente=tblventasremisiones.idcliente inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario  inner join tblmovimientos on tblmovimientos.idremision=tblventasremisiones.idremision" + _
        " inner join tblmovimientosdetalles on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblventasremisiones.porsurtir=1 and tblventasremisiones.estado=3  and tblmovimientos.estado=3 "
        If pTipo = 0 Then
            Comm.CommandText += " and tblventasremisiones.fecha>='" + pFecha1 + "' and tblventasremisiones.fecha<='" + pFecha2 + "'"
        End If
        If pTipo = 1 Then
            Comm.CommandText += " and (tblventasremisionesinventario.cantidad-ifnull((select sum(cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=tblventasremisiones.idremision and tblmovimientosdetalles.idinventario=tblventasremisionesinventario.idinventario),0))>0"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventasremisiones.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventasremisiones.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventasremisiones.idvendedor=" + pidVendedor.ToString
        End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
        'End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tblventasremisionesinventario.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            End If
            If pidClasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
            End If
            If pidClasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
            End If
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasremisionesinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventasremisiones.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        Comm.ExecuteNonQuery()




        'If pConcendaso = False Then
        Comm.CommandText = "select * from tblrepventasporsurtird order by idcliente,fecha,serie,folio"
        'Else
        '    Comm.CommandText += " order by tblinventario.idclasificacion,tblinventario.idclasificacion2,tblinventario.idclasificacion3,tblventasinventario.idinventario,preciou"
        'End If


        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioporsurtird")
        'DS.WriteXmlSchema("tblinventarioporsurtird.xml")
        Return DS.Tables("tblinventarioporsurtird").DefaultView
    End Function
    Public Function VienedeRemision(ByVal pIdVenta As Integer) As Integer
        Comm.CommandText = "select ifnull((select count(idremision) from tblventasremisiones where idventar=" + pIdVenta.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function VienedeFertilizantePedido(ByVal pIdVenta As Integer) As Integer
        Comm.CommandText = "select ifnull((select count(idpedido) from tblfertilizantespedidos where idventa=" + pIdVenta.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Sub DesligaRemisiones(ByVal pIdVenta As Integer)
        Comm.CommandText = "update tblventasremisiones set idventar=0,usado=0 where idventar=" + pIdVenta.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblventas set deremision=0 where idventa=" + pIdVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub DesligaFertilizantesPedidos(ByVal pIdVenta As Integer)
        Comm.CommandText = "update tblfertilizantespedidos set idventa=0 where idventa=" + pIdVenta.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblventas set deremision=0 where idventa=" + pIdVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Usar(ByVal pidVenta As Integer)
        Comm.CommandText = "update tblventas set deremision=1 where idventa=" + pidVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function TieneSalidas(ByVal pIdVenta As Integer) As Integer
        Comm.CommandText = "select ifnull((select count(idventa) from tblmovimientos where idventa=" + pIdVenta.ToString + " and estado=3),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function TotalAmortizacion(ByVal pIdVenta As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(precio*(1+tblventasinventario.iva/100)) from tblventasinventario inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblventas on tblventas.idventa=tblventasinventario.idventa where tblinventario.esamortizacion=1 and tblventas.idventa=" + pIdVenta.ToString + "),0)"
        '"+ifnull((select sum(precio*(1+tblventasremisionesinventario.iva/100)) from tblventasremisionesinventario inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario inner join tblventasremisiones on tblventasremisiones.idremision=tblventasremisionesinventario.idremision where tblinventario.esamortizacion=1 and tblventasremisiones.estado=3 and tblventasremisiones.idcliente=" + pIdcliente.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaTotalCantidad(ByVal pIdVenta As Integer) As Double
        Comm.CommandText = "select ifnull((select count(tblventasinventario.idventa) from tblventasinventario inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario where idventa=" + pIdVenta.ToString + " and inventariable=1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaTextoImpLocales(ByVal pFormato As String, ByVal pEspacio As Integer) As String
        Dim Str As String = ""
        Dim Longitud As Integer = 40
        Dim strAux As String
        'For Each IM As Implocal In ImpLocales
        '    If Longitud < Len(IM.Nombre + " " + Format(IM.Tasa, "#0.0###") + "%: ") Then Longitud = Len(IM.Nombre + " " + Format(IM.Tasa, "#0.0###") + "%: ")
        'Next
        For Each im As Implocal In ImpLocales
            If Str <> "" Then Str += vbCrLf
            If im.Tasa > 0.1 Then
                strAux = im.Nombre + " " + Format(im.Tasa, "#0.0#") + "%: "
            Else
                If im.Tasa > 0.01 Then
                    strAux = im.Nombre + " " + Format(im.Tasa, "#0.00#") + "%: "
                Else
                    strAux = im.Nombre + " " + Format(im.Tasa, "#0.0###") + "%: "
                End If
            End If
                Str += strAux.PadLeft(Longitud, " ") + Format(im.Importe, pFormato).PadLeft(pEspacio)
        Next
        Return Str
    End Function
    Public Function Timbrar3(ByVal pRFC As String, ByVal pXML As String, ByVal pRutaSalida As String, ByVal pAPIKEY As String, ByVal pConMsgbox As Boolean, ByVal pFolio As Integer, ByVal pSerie As String) As String
        Dim Cadena As String
        Dim XmlTimbrado As String = ""
        Try
            Dim Pruebas As String = "NO"
            Comm.CommandText = "select codigopostal from tblopciones limit 1"
            Pruebas = Comm.ExecuteScalar
            Dim Alterno As String = "0"
            Comm.CommandText = "select nombreempresalocal from tblopciones limit 1"
            Alterno = Comm.ExecuteScalar
            If Alterno = "1" Then
                Return Timbrar3Alt(pRFC, pXML, pRutaSalida, pAPIKEY, pConMsgbox, pFolio, pSerie)
            End If
            Cadena = pRFC + "~" + pAPIKEY + "~" + Pruebas + "~" + "Factura" + "~" + pXML
            Dim FF As New facturafiel.server()
            FF.Url = "http://www.facturafiel.com/websrv/servicio_timbrado_xml.php?wsdl"
            XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
            FF.Dispose()
            Dim Checa As String
            Checa = XmlTimbrado
            Checa = Checa.ToUpper
            If Checa.Contains("YA FUE UTILIZADO. VERIFÍQUELO") Then
                XmlTimbrado = "Recuperar"
            End If
            'Return XmlTimbrado
        Catch ex As Exception
            'If ex.Message.Contains("Response is not well-formed XML") Then
            '    If pConMsgbox Then
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
            If IO.File.Exists(Application.StartupPath + "\temp\ferror.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\ferror.txt")
            End If
            en.GuardaArchivoTexto(Application.StartupPath + "\temp\ferror.txt", ex.Message, System.Text.Encoding.Default)

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
                If XmlTimbrado.StartsWith("Error") = False Then
                    XmlTimbrado = XmlTimbrado.Substring(1, XmlTimbrado.Length - 1)
                End If
                R.Dispose()
            End If
        Catch ex As Exception
            If pConMsgbox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError = ex.Message
            End If
            XmlTimbrado = "ERROR"
            NoCertificadoSAT = "Error"
        End Try
        Return XmlTimbrado
    End Function

    Public Function Timbrar3Alt(ByVal pRFC As String, ByVal pXML As String, ByVal pRutaSalida As String, ByVal pAPIKEY As String, ByVal pConMsgbox As Boolean, ByVal pFolio As Integer, ByVal pSerie As String) As String
        Dim Cadena As String
        Dim XmlTimbrado As String = ""
        Try
            Dim Pruebas As String = "NO"
            Comm.CommandText = "select codigopostal from tblopciones limit 1"
            Pruebas = Comm.ExecuteScalar
            Cadena = pRFC + "~" + pAPIKEY + "~" + Pruebas + "~" + "Factura" + "~" + pXML
            Dim FF As New facturafiel.server
            FF.Url = "http://www.facturafiel.com/websrv/servicio_timbrado_xml.php?wsdl"
            XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
            FF.Dispose()
            Dim Checa As String
            Checa = XmlTimbrado
            Checa = Checa.ToUpper
            If Checa.Contains("YA FUE UTILIZADO. VERIFÍQUELO") Then
                XmlTimbrado = "Recuperar"
            End If
            'Return XmlTimbrado
        Catch ex As Exception
            'If ex.Message.Contains("Response is not well-formed XML") Then
            '    If pConMsgbox Then
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
            If IO.File.Exists(Application.StartupPath + "\temp\ferror.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\ferror.txt")
            End If
            en.GuardaArchivoTexto(Application.StartupPath + "\temp\ferror.txt", ex.Message, System.Text.Encoding.Default)

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
                If XmlTimbrado.StartsWith("Error") = False Then
                    XmlTimbrado = XmlTimbrado.Substring(1, XmlTimbrado.Length - 1)
                End If
                R.Dispose()
            End If
        Catch ex As Exception
            If pConMsgbox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError = ex.Message
            End If
            XmlTimbrado = "ERROR"
            NoCertificadoSAT = "Error"
        End Try
        Return XmlTimbrado
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
            If XMLTimbrado.StartsWith("Error") = False Then
                XMLTimbrado = XMLTimbrado.Substring(1, XMLTimbrado.Length - 1)
            End If
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
    Public Function DaMoneda(ByVal pIdventa As Integer) As Integer
        Comm.CommandText = "select ifnull((select idmoneda from tblventasinventario where idventa=" + pIdventa.ToString + " limit 1),2)"
        Return Comm.ExecuteScalar
    End Function
    Public Function Recuperar(ByVal pidventa As Integer, ByVal pRFC As String, ByVal pAPIKey As String) As Boolean
        ID = pidventa
        LlenaDatos()
        Dim Cadena As String
        Dim XMLTimbrado As String
        Try

            Dim R As New facturafielrecuperacion.server()
            R.Url = "http://www.facturafiel.com/websrv/servicio_recuperacion.php?wsdl"
            If Serie <> "" Then
                Cadena = pRFC + "~" + pAPIKey + "~" + Serie + "+" + Folio.ToString
            Else
                Cadena = pRFC + "~" + pAPIKey + "~" + Folio.ToString
            End If
            XMLTimbrado = R.servicio_recuperacion(Cadena)
            XMLTimbrado = XMLTimbrado.Substring(1, XMLTimbrado.Length - 1)
            If UCase(XMLTimbrado.Substring(0, 5)) <> "ERROR" Then
                Dim xmldoc As New Xml.XmlDocument
                xmldoc.Load(XMLTimbrado)
                uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value
                GuardaDatosTimbrado(pidventa, uuid, FechaTimbrado, SelloCFD, NoCertificadoSAT, SelloSAT)
                R.Dispose()
                Return True
            Else
                R.Dispose()
                Return False
            End If
        Catch ex As Exception
            'If pConMsgbox Then
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            'Else
            'MensajeError = ex.Message
            'End If
            'XmlTimbrado = "ERROR"
            Return False
        End Try
    End Function

    Public Function ReporteViejosSaldosEx(ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pTipodeCambio As Double, ByVal pZona As Integer) As DataView
        'Dim DS As New DataSet
        'Dim F As String
        'F = Format(Date.Now, "yyyy/MM/dd")
        'If pIdCliente > 0 Then
        '    Comm.CommandText = "delete from tblclientesviejossaldos where idcliente=" + pIdCliente.ToString
        'Else
        '    Comm.CommandText = "delete from tblclientesviejossaldos"
        'End If
        'Comm.ExecuteNonQuery()
        'If pMostrarEnPesos = 0 Then
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sietedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sesentadias," + _
        '    "if(tblventas.idconversion=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'Else
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sietedias," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as quincedias," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as treintadias," + _
        '   "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sesentadias," + _
        '   "(tblventas.totalapagar-tblventas.credito) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'End If

        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        'End If
        'If pIdCliente > 0 Then
        '    Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        'End If
        'If pidVendedor > 0 Then
        '    Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        'End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        'End If
        'Comm.ExecuteNonQuery()
        ''---------Notas de Cargo

        'If pMostrarEnPesos = 0 Then
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as sietedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as sesentadias," + _
        '    "if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where round(tblventas.totalapagar-tblventas.aplicado,2)>0 and tblventas.estado=3"
        'Else
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as sietedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as sesentadias," + _
        '    "(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where round(tblventas.totalapagar-tblventas.aplicado,2)>0 and tblventas.estado=3"
        'End If

        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        'End If
        'If pIdCliente > 0 Then
        '    Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        'End If
        'If pidVendedor > 0 Then
        '    Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        'End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        'End If
        'Comm.ExecuteNonQuery()
        ''-------------Documentos Saldo Inicial

        'If pMostrarEnPesos = 0 Then
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sietedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sesentadias," + _
        '    "if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio) as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'Else
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sietedias," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sesentadias," + _
        '    "(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'End If

        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        'End If
        'If pIdCliente > 0 Then
        '    Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        'End If
        'If pidVendedor > 0 Then
        '    Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        'End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        'End If
        'Comm.ExecuteNonQuery()
        ''Documentos documentos

        'If pMostrarEnPesos = 0 Then
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sietedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sesentadias," + _
        '    "if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio) as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'Else
        '    Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sietedias," + _
        '   "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as quincedias," + _
        '    "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as treintadias," + _
        '    "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sesentadias," + _
        '    "(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        'End If

        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        'End If
        'If pIdCliente > 0 Then
        '    Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        'End If
        'If pidVendedor > 0 Then
        '    Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        'End If
        'If pidMoneda > 0 Then
        '    Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        'End If
        'Comm.ExecuteNonQuery()
        'If pIdCliente <= 0 Then
        '    Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente  order by tblclientes.nombre,fecha,serie,folio"
        'Else
        '    Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente where tblclientesviejossaldos.idcliente=" + pIdCliente.ToString + " order by tblclientes.nombre,fecha,serie,folio"
        'End If

        'Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        'DA.Fill(DS, "tblventasviejo")
        ''DS.WriteXmlSchema("tblventasviejo.xml")
        'Return DS.Tables("tblventasviejo").DefaultView
        Dim DS As New DataSet
        Dim F As String
        F = Format(Date.Now, "yyyy/MM/dd")
        'F = pFecha2
        'Comm.CommandText = "select ifnull((select tipodecambio from tblcompras where fecha<='" + F + "' and idmoneda=2 order by fecha desc limit 1),1)"
        'pTipodeCambio = Comm.ExecuteScalar
        Comm.CommandTimeout = 10000
        If pIdCliente > 0 Then
            Comm.CommandText = "delete from tblclientesviejossaldosex where idcliente=" + pIdCliente.ToString
        Else
            Comm.CommandText = "delete from tblclientesviejossaldosex"
        End If
        Comm.ExecuteNonQuery()

        Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
        "tblventas.credito as credito," + _
        "0,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
        ",totalapagar from  tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.idconversion=2 and round(tblventas.totalapagar-tblventas.credito,2)>0"


        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        Comm.ExecuteNonQuery()
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as tresmeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as seismeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as docemeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as dosyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as tresyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as cuatroyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as cincoyears," + _
            "tblventas.credito*tblventas.tipodecambio as credito," + _
            "0," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.idconversion<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
            "tblventas.credito as credito," + _
            "0,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.idconversion<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        End If

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If

        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If

        Comm.ExecuteNonQuery()



        '---------Notas de Cargo



        Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
        "tblventas.aplicado as credito," + _
        "1,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.estado=3 and tblventas.idmoneda=2 and round(tblventas.totalapagar-tblventas.aplicado,2)>0"



        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()

        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as tresmeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as seismeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as docemeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as dosyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as tresyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as cuatroyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as cincoyears," + _
            "tblventas.aplicado*tblventas.tipodecambio as credito," + _
            "1," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente  where tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.aplicado,2)>0"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,traintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
            "tblventas.aplicado as credito," + _
            "1,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.aplicado,2)>0"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()
        '-------------Documentos Saldo Inicial

        Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as cincoyears," + _
        "tblventas.credito as credito," + _
        "2,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.estado=3 and tblventas.idmoneda=2 and round(tblventas.totalapagar-tblventas.credito,2)>0"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()

        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as tresmeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as seismeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as docemeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as dosyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as tresyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as cuatroyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as cincoyears," + _
            "tblventas.credito*tblventas.tipodecambio as credito," + _
            "2," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente  where tblventas.tiposaldo=0 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
            "tblventas.credito as credito," + _
            "2,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()
        'Documentos documentos

        'If pMostrarEnPesos = 0 Then

        Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
        "tblventas.credito as credito," + _
        "3,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and tblventas.estado=3 and tblventas.idmoneda=2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        'Else

        'End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as tresmeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as seismeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as docemeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as dosyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as tresyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as cuatroyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as cincoyears," + _
            "tblventas.credito*tblventas.tipodecambio as credito," + _
            "3," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente  where tblventas.tiposaldo=1 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
            "tblventas.credito as credito," + _
            "3,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente  where tblventas.tiposaldo=1 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        'If pZona > 0 Then
        '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
        '    Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        'End If
        Comm.ExecuteNonQuery()
        'Saca saldos
        'Comm.CommandText = "delete from tblclientesmovimientossaldos"
        'Comm.ExecuteNonQuery()
        'Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select idcliente,spdasaldoafechaproveedor(idcliente,'" + Format(DateAdd(DateInterval.Day, 1, CDate(pFecha2)), "yyyy/MM/dd") + "') from tblclientesviejossaldos group by idcliente"
        'Comm.ExecuteNonQuery()
        If pIdCliente <= 0 Then
            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,tipo,tblclientesviejossaldosex.credito,0 as saldoant,tblclientesviejossaldosex.tipodecambio,tblclientesviejossaldosex.corriente from tblclientesviejossaldosex inner join tblclientes on tblclientesviejossaldosex.idcliente=tblclientes.idcliente order by tblclientes.nombre,fecha,serie,folio"
        Else
            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,tipo,tblclientesviejossaldosex.credito,0 as saldoant,tblclientesviejossaldosex.tipodecambio,tblclientesviejossaldosex.corriente from tblclientesviejossaldosex inner join tblclientes on tblclientesviejossaldosex.idcliente=tblclientes.idcliente where tblclientesviejossaldosex.idcliente=" + pIdCliente.ToString + " order by tblclientes.nombre,fecha,serie,folio"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasviejo")
        'DS.WriteXmlSchema("tblventasviejo.xml")
        Return DS.Tables("tblventasviejo").DefaultView
    End Function

    Public Function ReporteViejosSaldosHEx(ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pFecha As String, ByVal pTipodeCambio As Double) As DataView
        Dim DS As New DataSet
        Dim F As String
        'F = Format(Date.Now, "yyyy/MM/dd")
        F = pFecha
        Comm.CommandTimeout = 10000
        Comm.CommandText = "select ifnull((select tipodecambio from tblventas where fecha<='" + F + "' and idconversion<>2 order by fecha desc limit 1),1)"
        pTipodeCambio = Comm.ExecuteScalar
        If IdCliente > 0 Then
            Comm.CommandText = "delete from tblclientesviejossaldosex where idcliente=" + IdCliente.ToString
        Else
            Comm.CommandText = "delete from tblclientesviejossaldosex"
        End If
        Comm.ExecuteNonQuery()
        'If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
        '"if(tblventas.idmoneda=2," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + Pfecha + "' and tblventaspagos.idventa=tblventas.idventa),0)," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + Pfecha + "' and tblventaspagos.idventa=tblventas.idventa),0)*tblventas.tipodecambio) as credito," + _
        '"0 from tblcompras as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + Pfecha + "' and tblventas.estado=3"
        Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
        "ifnull((select sum(if(tblventaspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0) as credito," + _
        "0,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
        ",totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idconversion=2"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        'If pidMoneda > 0 Then
        'Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        'End If
        Comm.ExecuteNonQuery()
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as cincoyears," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idconversion,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0) as credito," + _
            "0," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idconversion<>2"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idconversion,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0) as credito," + _
            "0,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idconversion<>2"
        End If

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()



        '---------Notas de Cargo



        Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
        "ifnull((select sum(if(tblventaspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0) as credito," + _
        "1,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda=2"



        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()

        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as cincoyears," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0) as credito," + _
            "1," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.folio,0,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0) as credito," + _
            "1,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        '-------------Documentos Saldo Inicial

        Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
        "ifnull((select sum(if(tblventaspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
        "2,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda=2"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()

        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as cincoyears," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
            "2," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
            "2,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        'Documentos documentos

        'If pMostrarEnPesos = 0 Then

        Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
        "ifnull((select sum(if(tblventaspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
        "3,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda=2"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as cincoyears," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
            "3," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldosex(fecha,idcliente,serie,folio,limite,sietedias,quincedias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d'),tblventas.totalapagar,0) as tresmeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+90),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d'),tblventas.totalapagar,0) as seismeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+180),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d'),tblventas.totalapagar,0) as docemeses," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+365),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d'),tblventas.totalapagar,0) as dosyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+730),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d'),tblventas.totalapagar,0) as tresyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1095),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cuatroyears," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+1460),'%Y/%m/%d'),tblventas.totalapagar,0) as cincoyears," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
            "3,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        'Saca saldos
        'Comm.CommandText = "delete from tblclientesmovimientossaldos"
        'Comm.ExecuteNonQuery()
        'Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select idcliente,spdasaldoafechaproveedor(idcliente,'" + Format(DateAdd(DateInterval.Day, 1, CDate(Pfecha)), "yyyy/MM/dd") + "') from tblclientesviejossaldos group by idcliente"
        'Comm.ExecuteNonQuery()


        If IdCliente <= 0 Then
            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,tipo,tblclientesviejossaldosex.credito,0 as saldoant,tblclientesviejossaldosex.tipodecambio,tblclientesviejossaldosex.corriente from tblclientesviejossaldosex inner join tblclientes on tblclientesviejossaldosex.idcliente=tblclientes.idcliente where round(totalapagar-tblclientesviejossaldosex.credito,2)>0  order by tblclientes.nombre,fecha,serie,folio"
        Else
            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,tresmeses,seismeses,docemeses,dosyears,tresyears,cuatroyears,cincoyears,tipo,tblclientesviejossaldosex.credito,0 as saldoant,tblclientesviejossaldosex.tipodecambio,tblclientesviejossaldosex.corriente from tblclientesviejossaldosex inner join tblclientes on tblclientesviejossaldosex.idcliente=tblclientes.idcliente where tblclientesviejossaldosex.idcliente=" + IdCliente.ToString + " and round(totalapagar-tblclientesviejossaldosex.credito,2)>0 order by tblclientes.nombre,fecha,serie,folio"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesviejo")
        'DS.WriteXmlSchema("tblcomprasviejo.xml")
        Return DS.Tables("tblclientesviejo").DefaultView
        '        Dim DS As New DataSet
        '        Dim F As String
        '        F = pFecha
        '        Comm.CommandTimeout = 10000
        '        If pIdCliente > 0 Then
        '            Comm.CommandText = "delete from tblclientesviejossaldos where idcliente=" + pIdCliente.ToString
        '        Else
        '            Comm.CommandText = "delete from tblclientesviejossaldos"
        '        End If
        '        Comm.ExecuteNonQuery()
        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '"if(tblventas.idconversion=2," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)*tblventas.tipodecambio) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "'"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "'"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        '---------Notas de Cargo

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)*tblventas.tipodecambio) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        '-------------Documentos Saldo Inicial

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)*tblventas.tipodecambio) as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)) as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        'Documentos documentos

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)*tblventas.tipodecambio) as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()

        '        'Saca saldos
        '        Comm.CommandText = "delete from tblclientesmovimientossaldos"
        '        Comm.ExecuteNonQuery()
        '        'Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select idcliente,spdasaldoafechaclientes(idcliente,'" + Format(DateAdd(DateInterval.Day, 1, CDate(pFecha)), "yyyy/MM/dd") + "') from tblclientesviejossaldos group by idcliente"
        '        Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select c.idcliente, " + _
        '        "ifnull((select if(idconversion=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and idcliente=c.idcliente and estado=3 and tblventas.fecha<='" + pFecha + "'),0)+" + _
        '"ifnull((select if(idmoneda=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tblnotasdecargo where idcliente=c.idcliente and estado=3 and tblnotasdecargo.fecha<='" + pFecha + "'),0)+" + _
        '"ifnull((select if(idmoneda=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tbldocumentosclientes where idcliente=c.idcliente and estado=3 and tbldocumentosclientes.fecha<='" + pFecha + "'),0)-" + _
        '"ifnull((select if(idmoneda=2,sum(cantidad),sum(cantidad*ptipodecambio)) from tblventaspagos where idcliente=c.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "'),0) AS saldo " + _
        '"from tblclientesviejossaldos as c group by c.idcliente"
        '        Comm.ExecuteNonQuery()
        '        If pIdCliente <= 0 Then
        '            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente inner join tblclientesmovimientossaldos on tblclientesviejossaldos.idcliente=tblclientesmovimientossaldos.idcliente where round(sietedias+quincedias+treintadias+sesentadias-tblclientesviejossaldos.credito,2)>0 order by tblclientes.nombre,fecha,serie,folio"
        '        Else
        '            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente inner join tblclientesmovimientossaldos on tblclientesviejossaldos.idcliente=tblclientesmovimientossaldos.idcliente where tblclientesviejossaldos.idcliente=" + pIdCliente.ToString + " and round(sietedias+quincedias+treintadias+sesentadias-tblclientesviejossaldos.credito,2)>0 order by tblclientes.nombre,fecha,serie,folio"
        '        End If

        '        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        '        DA.Fill(DS, "tblventasviejo")
        '        'DS.WriteXmlSchema("tblventasviejo.xml")
        '        Return DS.Tables("tblventasviejo").DefaultView
        '    End Function

        '    Public Function ReporteViejosSaldosHN(ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pFecha As String) As DataView
        '        Dim DS As New DataSet
        '        Dim F As String
        '        F = pFecha
        '        Comm.CommandTimeout = 10000
        '        If pIdCliente > 0 Then
        '            Comm.CommandText = "delete from tblclientesviejossaldos where idcliente=" + pIdCliente.ToString
        '        Else
        '            Comm.CommandText = "delete from tblclientesviejossaldos"
        '        End If
        '        Comm.ExecuteNonQuery()
        '        If pMostrarEnPesos = 0 Then

        '            '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            '"if(tblventas.idconversion=2," + _
        '            '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)," + _
        '            '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)*tblventas.tipodecambio) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "' and round(tblventas.totalapagar-tblventas.credito,2)>0;"


        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(tblventas.fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(tblventas.fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(tblventas.fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(tblventas.fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(tblventas.fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(tblventas.fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(tblventas.fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '"if(tblventas.idconversion=2," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0)*tblventas.tipodecambio) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "' and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0),2)>0"


        '        Else
        '            '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '            '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "' and round(tblventas.totalapagar-tblventas.credito,2)>0"

        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0) as credito,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.estado=3 and tblventas.fecha<='" + pFecha + "' and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idventa=tblventas.idventa),0),2)>0"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        '---------Notas de Cargo

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)*tblventas.tipodecambio) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0),2)>0"

        '            'Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            '"if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            '"if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            '"if(tblventas.idmoneda=2," + _
        '            '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)," + _
        '            '"ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0)*tblventas.tipodecambio) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.fecha<='" + pFecha + "' and tblventas.estado=3"

        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0) as credito,1 from tblnotasdecargo as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idcargo=tblventas.idcargo),0),2)>0"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        '-------------Documentos Saldo Inicial

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)*tblventas.tipodecambio) as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0),2)>0"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)) as credito,2 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=0 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0),2)>0"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        'Documentos documentos

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0)*tblventas.tipodecambio) as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0),2)>0"
        '        Else
        '            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblclientes.idcliente,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblclientes.creditodias-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '            "ifnull((select sum(cantidad) from tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0) as credito,3 from tbldocumentosclientes as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where tblventas.tiposaldo=1 and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and round(tblventas.totalapagar-ifnull((select sum(cantidad) from tblventaspagos where tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.iddocumentod=tblventas.iddocumento),0),2)>0"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdCliente > 0 Then
        '            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        '        End If
        '        If pidVendedor > 0 Then
        '            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        '        End If
        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()

        '        'Saca saldos
        '        Comm.CommandText = "delete from tblclientesmovimientossaldos"
        '        Comm.ExecuteNonQuery()
        '        'Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select idcliente,spdasaldoafechaclientes(idcliente,'" + Format(DateAdd(DateInterval.Day, 1, CDate(pFecha)), "yyyy/MM/dd") + "') from tblclientesviejossaldos group by idcliente"
        '        Comm.CommandText = "insert into tblclientesmovimientossaldos(idcliente,saldoant) select c.idcliente, " + _
        '        "ifnull((select if(idconversion=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and idcliente=c.idcliente and estado=3 and tblventas.fecha<='" + pFecha + "'),0)+" + _
        '"ifnull((select if(idmoneda=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tblnotasdecargo where idcliente=c.idcliente and estado=3 and tblnotasdecargo.fecha<='" + pFecha + "'),0)+" + _
        '"ifnull((select if(idmoneda=2,sum(totalapagar),sum(totalapagar*tipodecambio)) from tbldocumentosclientes where idcliente=c.idcliente and estado=3 and tbldocumentosclientes.fecha<='" + pFecha + "'),0)-" + _
        '"ifnull((select if(idmoneda=2,sum(cantidad),sum(cantidad*ptipodecambio)) from tblventaspagos where idcliente=c.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "'),0) AS saldo " + _
        '"from tblclientesviejossaldos as c group by c.idcliente"
        '        Comm.ExecuteNonQuery()
        '        If pIdCliente <= 0 Then
        '            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,round(sietedias,2) as sietedias,quincedias,treintadias,sesentadias,tipo,round(tblclientesviejossaldos.credito,2) as credito,round(saldoant,2) as saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente inner join tblclientesmovimientossaldos on tblclientesviejossaldos.idcliente=tblclientesmovimientossaldos.idcliente where round(sietedias+quincedias+treintadias+sesentadias-tblclientesviejossaldos.credito,2)>0 order by tblclientes.nombre,fecha,serie,folio"
        '        Else
        '            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,round(sietedias,2) as sietedias,quincedias,treintadias,sesentadias,tipo,round(tblclientesviejossaldos.credito,2) as credito,round(saldoant,2) as saldoant from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente inner join tblclientesmovimientossaldos on tblclientesviejossaldos.idcliente=tblclientesmovimientossaldos.idcliente where tblclientesviejossaldos.idcliente=" + pIdCliente.ToString + " and round(sietedias+quincedias+treintadias+sesentadias-tblclientesviejossaldos.credito,2)>0 order by tblclientes.nombre,fecha,serie,folio"
        '        End If

        '        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        '        DA.Fill(DS, "tblventasviejo")
        '        'DS.WriteXmlSchema("tblventasviejo.xml")
        '        Return DS.Tables("tblventasviejo").DefaultView
    End Function
    Public Function ReporteGroupByFolio(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidinventario As Integer, ByVal pidclasificacion1 As Integer, ByVal pidclasificacion2 As Integer, ByVal pidclasificacion3 As Integer, ByVal pSoloCanceladas As Boolean, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idconversion=2,round(tblventas.total,2),round(tblventas.total*tblventas.tipodecambio,2)) as total,if(tblventas.idconversion=2,round(tblventas.totalapagar,2),round(tblventas.totalapagar*tblventas.tipodecambio,2)) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2) as cantidad,tblventasinventario.descripcion,round(tblventasinventario.precio,2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,if(tblformasdepago.tipo=0,'Crédito','Contado') as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido,tblventasinventario.ieps,tblventasinventario.ivaretenido retcon " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor  where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,round(tblventas.total,2) as total,round(tblventas.totalapagar,2) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,round(tblventasinventario.cantidad,2),tblventasinventario.descripcion,round(tblventasinventario.precio,2),0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,if(tblformasdepago.tipo=0,'Crédito','Contado') as formadepago,tblclientes.nombre as cnombre,tblventas.isr,tblventas.ivaretenido,tblventasinventario.ieps,tblventasinventario.ivaretenido retcon  " + _
            "from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor  where tblformasdepago.tipo<>2 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If
        If pSoloCanceladas Then
            Comm.CommandText += " and tblventas.estado=4"
        Else
            Comm.CommandText += " and tblventas.estado=3"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and (tblclientes.zona='" + pZona2.ToString() + "' or tblclientes.zona2='" + pZona2.ToString() + "')"
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idconversion=" + pidMoneda.ToString
        End If
        If pidinventario > 1 Then
            Comm.CommandText += " and tblventasinventario.idinventario=" + pidinventario.ToString
        Else
            If pidclasificacion1 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidclasificacion1.ToString
            End If
            If pidclasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidclasificacion2.ToString
            End If
            If pidclasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidclasificacion3.ToString
            End If
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        Comm.CommandText += " group by tblventas.folio order by tblventas.fecha,tblventas.serie,tblventas.folio "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventas.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Sub LlenaTablaCorte(pFecha1 As String, pFecha2 As String, pIdsucursal As Integer, pidCaja As Integer, pMostrarOc As Byte, pSeriesOc As String)
        'Dim DS As New DataSet
        Dim Cantidad As Double
        Comm.CommandText = "delete from tblventascorte"
        Comm.ExecuteNonQuery()
        'ventas contado
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=1 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('FACTURAS CONTADO:'," + Cantidad.ToString + ",0,0)"
        Comm.ExecuteNonQuery()

        'ventas crédito
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblventas inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('FACTURAS CRÉDITO:'," + Cantidad.ToString + ",0,1)"
        Comm.ExecuteNonQuery()

        'ventas pagos
        Comm.CommandText = "select ifnull((select sum(if(tblventaspagos.idmoneda=2,round(cantidad,2),round(cantidad*tblventaspagos.ptipodecambio,2))) from tblventaspagos inner join tblventas on tblventaspagos.idventa=tblventas.idventa where tblventaspagos.iddocumento=0 and tblventaspagos.fecha>='" + pFecha1 + "' and tblventaspagos.fecha<='" + pFecha2 + "'"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(if(tblventaspagos.idmoneda=2,round(cantidad,2),round(cantidad*tblventaspagos.ptipodecambio,2))) from tblventaspagos inner join tblnotasdecargo on tblventaspagos.idcargo=tblnotasdecargo.idcargo where tblventaspagos.iddocumento=0 and tblventaspagos.fecha>='" + pFecha1 + "' and tblventaspagos.fecha<='" + pFecha2 + "'"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad += Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select sum(if(tblventaspagos.idmoneda=2,round(cantidad,2),round(cantidad*tblventaspagos.ptipodecambio,2))) from tblventaspagos inner join tbldocumentosclientes on tblventaspagos.iddocumentod=tbldocumentosclientes.iddocumento where tblventaspagos.iddocumento=0 and tblventaspagos.fecha>='" + pFecha1 + "' and tblventaspagos.fecha<='" + pFecha2 + "'"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tbldocumentosclientes.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad += Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('COBRANZA FACTURAS:'," + Cantidad.ToString + ",0,2)"
        Comm.ExecuteNonQuery()


        'remisiones contado
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblventasremisiones inner join tblformasdepagoremisiones on tblventasremisiones.idforma=tblformasdepagoremisiones.idforma where (tblformasdepagoremisiones.tipo=1 or tblformasdepagoremisiones.tipo=2) and tblventasremisiones.fecha>='" + pFecha1 + "' and tblventasremisiones.fecha<='" + pFecha2 + "' and tblventasremisiones.estado=3 and tblventasremisiones.idventar=0"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblventasremisiones.idsucursal=" + pIdsucursal.ToString
        End If
        If pMostrarOc = 1 Then
            'Comm.CommandText += " and tblventas.serie<>'" + Replace(pSerieOC, "'", "''") + "'"
            Comm.CommandText += Replace(pSeriesOc, "-", " and tblventasremisiones.serie")
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('REMISIONES CONTADO:'," + Cantidad.ToString + ",0,3)"
        Comm.ExecuteNonQuery()

        'remisiones crédito
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblventasremisiones inner join tblformasdepagoremisiones on tblventasremisiones.idforma=tblformasdepagoremisiones.idforma where tblformasdepagoremisiones.tipo=3 and tblventasremisiones.fecha>='" + pFecha1 + "' and tblventasremisiones.fecha<='" + pFecha2 + "' and tblventasremisiones.estado=3 and tblventasremisiones.idventar=0"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblventasremisiones.idsucursal=" + pIdsucursal.ToString
        End If
        If pMostrarOc = 1 Then
            'Comm.CommandText += " and tblventas.serie<>'" + Replace(pSerieOC, "'", "''") + "'"
            Comm.CommandText += Replace(pSeriesOc, "-", " and tblventasremisiones.serie")
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('REMISIONES CRÉDITO:'," + Cantidad.ToString + ",0,4)"
        Comm.ExecuteNonQuery()

        'remisiones pagos
        Comm.CommandText = "select ifnull((select sum(if(tblventaspagosremisiones.idmoneda=2,round(cantidad,2),round(cantidad*tblventaspagosremisiones.ptipodecambio,2))) from tblventaspagosremisiones inner join tblventasremisiones on tblventaspagosremisiones.idremision=tblventasremisiones.idremision where tblventaspagosremisiones.iddevolucion=0 and tblventaspagosremisiones.fecha>='" + pFecha1 + "' and tblventaspagosremisiones.fecha<='" + pFecha2 + "'"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblventasremisiones.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('COBRANZA REMISIONES:'," + Cantidad.ToString + ",0,5)"
        Comm.ExecuteNonQuery()

        'ventas devoluciones
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tbldevoluciones where tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "' and tbldevoluciones.idventa<>0 and tbldevoluciones.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('DEVOLUCIONES FACTURAS:'," + Cantidad.ToString + ",0,6)"
        Comm.ExecuteNonQuery()

        'ventas devoluciones remisiones
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tbldevoluciones where tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "' and tbldevoluciones.idremision<>0 and tbldevoluciones.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('DEVOLUCIONES REMISIONES:'," + Cantidad.ToString + ",0,7)"
        Comm.ExecuteNonQuery()

        'ventas notas de credito
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblnotasdecredito where tblnotasdecredito.fecha>='" + pFecha1 + "' and tblnotasdecredito.fecha<='" + pFecha2 + "' and tblnotasdecredito.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblnotasdecredito.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('NOTAS DE CRÉDITO VENTAS:'," + Cantidad.ToString + ",0,8)"
        Comm.ExecuteNonQuery()

        'ventas notas de cargo
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblnotasdecargo where tblnotasdecargo.fecha>='" + pFecha1 + "' and tblnotasdecargo.fecha<='" + pFecha2 + "' and tblnotasdecargo.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End I,f
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('NOTAS DE CARGO VENTAS:'," + Cantidad.ToString + ",0,9)"
        Comm.ExecuteNonQuery()


        'apartados
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblventasapartados where tblventasapartados.fecha>='" + pFecha1 + "' and tblventasapartados.fecha<='" + pFecha2 + "' and tblventasapartados.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblventasapartados.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End I,f
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('APARTADOS:'," + Cantidad.ToString + ",0,10)"
        Comm.ExecuteNonQuery()

        'apartados pagos
        Comm.CommandText = "select ifnull((select sum(if(tblventaspagosapartados.idmoneda=2,round(cantidad,2),round(cantidad*tblventaspagosapartados.ptipodecambio,2))) from tblventaspagosapartados inner join tblventasapartados on tblventaspagosapartados.idremision=tblventasapartados.idapartado where tblventaspagosapartados.fecha>='" + pFecha1 + "' and tblventaspagosapartados.fecha<='" + pFecha2 + "'"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblventasapartados.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('COBRANZA APARTADOS:'," + Cantidad.ToString + ",0,11)"
        Comm.ExecuteNonQuery()

        '''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''COMPRAS''''''''''''''''
        '''''''''''''''''''''''''''''''''''''''''''''''''


        'compras contado
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblcompras inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tblformasdepago.tipo=1 and tblcompras.fecha>='" + pFecha1 + "' and tblcompras.fecha<='" + pFecha2 + "' and tblcompras.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('COMPRAS CONTADO:'," + Cantidad.ToString + ",1,0)"
        Comm.ExecuteNonQuery()

        'compras crédito
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblcompras inner join tblformasdepago on tblcompras.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblcompras.fecha>='" + pFecha1 + "' and tblcompras.fecha<='" + pFecha2 + "' and tblcompras.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('COMPRAS CRÉDITO:'," + Cantidad.ToString + ",1,1)"
        Comm.ExecuteNonQuery()

        'compras pagos
        Comm.CommandText = "select ifnull((select sum(if(tblcompraspagos.idmoneda=2,round(cantidad,2),round(cantidad*tblcompraspagos.ptipodecambio,2))) from tblcompraspagos inner join tblcompras on tblcompraspagos.idcompra=tblcompras.idcompra where tblcompraspagos.iddocumento=0 and tblcompraspagos.fecha>='" + pFecha1 + "' and tblcompraspagos.fecha<='" + pFecha2 + "'"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(if(tblcompraspagos.idmoneda=2,round(cantidad,2),round(cantidad*tblcompraspagos.ptipodecambio,2))) from tblcompraspagos inner join tblnotasdecargocompras on tblcompraspagos.idcargo=tblnotasdecargocompras.idcargo where tblcompraspagos.iddocumento=0 and tblcompraspagos.fecha>='" + pFecha1 + "' and tblcompraspagos.fecha<='" + pFecha2 + "'"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad += Comm.ExecuteScalar

        Comm.CommandText = "select ifnull((select sum(if(tblcompraspagos.idmoneda=2,round(cantidad,2),round(cantidad*tblcompraspagos.ptipodecambio,2))) from tblcompraspagos inner join tbldocumentosproveedores on tblcompraspagos.iddocumentod=tbldocumentosproveedores.iddocumento where tblcompraspagos.iddocumento=0 and tblcompraspagos.fecha>='" + pFecha1 + "' and tblcompraspagos.fecha<='" + pFecha2 + "'"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tbldocumentosproveedores.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad += Comm.ExecuteScalar

        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('PAGO A PROVEEDORES:'," + Cantidad.ToString + ",1,2)"
        Comm.ExecuteNonQuery()


        'remisiones compras
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblcomprasremisiones where tblcomprasremisiones.fecha>='" + pFecha1 + "' and tblcomprasremisiones.fecha<='" + pFecha2 + "' and tblcomprasremisiones.estado=3 and tblcomprasremisiones.idcomprar=0"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblcomprasremisiones.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('REMISIONES COMPRAS:'," + Cantidad.ToString + ",1,3)"
        Comm.ExecuteNonQuery()

        'compras devoluciones compras
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tbldevolucionescompras where tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "' and tbldevolucionescompras.idcompra<>0 and tbldevolucionescompras.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('DEVOLUCIONES COMPRAS:'," + Cantidad.ToString + ",1,8)"
        Comm.ExecuteNonQuery()

        'compras devoluciones remisiones
        Comm.CommandText = "select ifnull((select sum(if(idconversion=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tbldevolucionescompras where tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "' and tbldevolucionescompras.idremision<>0 and tbldevolucionescompras.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('DEVOLUCIONES COMPRAS REM:'," + Cantidad.ToString + ",1,9)"
        Comm.ExecuteNonQuery()

        ' notas de credito compras
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblnotasdecreditocompras where tblnotasdecreditocompras.fecha>='" + pFecha1 + "' and tblnotasdecreditocompras.fecha<='" + pFecha2 + "' and tblnotasdecreditocompras.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('NOTAS DE CRÉDITO COMPRAS:'," + Cantidad.ToString + ",1,10)"
        Comm.ExecuteNonQuery()

        'compras notas de cargo
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tblnotasdecargocompras where tblnotasdecargocompras.fecha>='" + pFecha1 + "' and tblnotasdecargocompras.fecha<='" + pFecha2 + "' and tblnotasdecargocompras.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('NOTAS DE CARGO COMPRAS:'," + Cantidad.ToString + ",1,7)"
        Comm.ExecuteNonQuery()

        'Gastos
        Comm.CommandText = "select ifnull((select sum(round(precio,2)) from tblgastosdetalles inner join tblgastos on tblgastosdetalles.idmovimiento=tblgastos.idmovimiento where tblgastos.fecha>='" + pFecha1 + "' and tblgastos.fecha<='" + pFecha2 + "' and tblgastos.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tblgastos.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('GASTOS:'," + Cantidad.ToString + ",0,12)"
        Comm.ExecuteNonQuery()


        'documentos clientes
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tbldocumentosclientes where tbldocumentosclientes.fecha>='" + pFecha1 + "' and tbldocumentosclientes.fecha<='" + pFecha2 + "' and tbldocumentosclientes.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tbldocumentosclientes.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('DOCUMENTOS CLIENTES:'," + Cantidad.ToString + ",0,13)"
        Comm.ExecuteNonQuery()

        'documentos proveedores
        Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2))) from tbldocumentosproveedores where tbldocumentosproveedores.fecha>='" + pFecha1 + "' and tbldocumentosproveedores.fecha<='" + pFecha2 + "' and tbldocumentosproveedores.estado=3"
        If pIdsucursal > 0 Then
            Comm.CommandText += " and tbldocumentosproveedores.idsucursal=" + pIdsucursal.ToString
        End If
        'If pidCaja > 0 Then
        '    Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        'End If
        Comm.CommandText += "),0)"
        Cantidad = Comm.ExecuteScalar
        Comm.CommandText = "insert into tblventascorte(concepto,cantidad,modulo,posicion) values('DOCUMENTOS PROVEEDORES:'," + Cantidad.ToString + ",1,12)"
        Comm.ExecuteNonQuery()




    End Sub
    Public Function ConsultaCorte(pModulo As Byte, pNoceros As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = " select * from tblventascorte"
        If pNoceros Then
            Comm.CommandText += " where cantidad<>0"
        End If
        If pModulo > 0 Then
            If pNoceros Then
                Comm.CommandText += " and modulo=" + CStr(pModulo - 1)
            Else
                Comm.CommandText += " where modulo=" + CStr(pModulo - 1)
            End If

        End If
        Comm.CommandText += " order by modulo,posicion"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventascorte")
        'DS.WriteXmlSchema("tblventascorte.xml")
        Return DS.Tables("tblventascorte").DefaultView
    End Function
    Public Sub ActualizaImporteImpuesto(pidimpuesto As Integer, pImporte As Double)
        Comm.CommandText = "update tblventasimpuestos set importe=" + pImporte.ToString + " where idimpuesto=" + pidimpuesto.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaImporteImpuesto(pidimpuesto As Integer) As Double
        Comm.CommandText = "select importe from tblventasimpuestos where idimpuesto=" + pidimpuesto.ToString
        Return Comm.ExecuteScalar
    End Function
End Class
