Public Class dbVentasRemisiones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdCliente As Integer
    Public Fecha As String
    Public Cliente As dbClientes
    Public Folio As Integer
    Public Desglosar As Byte
    Public Credito As Double
    Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    Public Estado As Byte
    Public IdSucursal As Integer
    Public Serie As String
    Public IdMoneda As Integer
    Public TipodeCambio As Double

    Public TotalVenta As Double
    Public Subtototal As Double
    Public TotalIva As Double
    Public FechaCancelado As String
    Public HoraCancelado As String
    Public Usado As Byte
    Public idCaja As Integer
    Public idForma As Integer
    Public IdVEndedor As Integer
    Public Comentario As String
    Public PorSurtir As Byte
    Public IdVentaR As Integer
    Public FolioRef As String
    Public TotalPeso As Double
    Public pIEPS As Double
    Public pIVARetenido As Double
    Public TotalIvaRetenidoConceptos As Double
    Public TotalIeps As Double
    Public TotalOfertas As Double
    Public Usuario As String
    Public UltimoFolio As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdCliente = -1
        Fecha = ""
        Hora = ""
        Folio = 0
        Desglosar = 0
        Credito = 0
        Iva = 0
        TotalaPagar = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        Serie = ""
        TipodeCambio = 0
        IdMoneda = 0
        idCaja = 1
        idForma = 1
        IdVEndedor = 1
        Comentario = ""
        PorSurtir = 0
        IdVentaR = 0
        FolioRef = ""
        Comm.Connection = Conexion
        Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select *,(select nombreusuario from tblusuarios where idusuario=tblventasremisiones.idusuariocambio) as usuario from tblventasremisiones where idremision=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdCliente = DReader("idcliente")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            Desglosar = DReader("desglosar")
            Credito = DReader("credito")
            Iva = DReader("iva")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            Serie = DReader("serie")
            IdMoneda = DReader("idmoneda")
            TipodeCambio = DReader("tipodecambio")
            FechaCancelado = DReader("fechacancelado")
            HoraCancelado = DReader("horacancelado")
            Usado = DReader("usado")
            idCaja = DReader("idcaja")
            idForma = DReader("idforma")
            IdVEndedor = DReader("idvendedor")
            Comentario = DReader("comentariof")
            PorSurtir = DReader("porsurtir")
            IdVentaR = DReader("idventar")
            Usuario = DReader("usuario")
        End If
        DReader.Close()
        If IdVentaR <> 0 Then
            Comm.CommandText = "select ifnull((select concat(serie,convert(folio using utf8)) from tblventas where idventa=" + IdVentaR.ToString + "),'')"
            FolioRef = "Facturado en: " + Comm.ExecuteScalar
        End If
        Cliente = New dbClientes(IdCliente, Comm.Connection)
    End Sub
    'Public Function ExisteFolio(ByVal pfolio As Integer, Optional ByVal idventa As Integer = -1) As Boolean
    '    Folio = pfolio
    '    Comm.CommandText = "select count(folio) from tblventas where folio=" + Folio.ToString + If(idventa = -1, "", " and idventa<>" + CStr(idventa))
    '    If Comm.ExecuteScalar = 0 Then Return False Else Return True
    'End Function

    Public Sub Guardar(ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pidSucursal As Integer, ByVal pSerie As String, ByVal pTipodeCambio As Double, ByVal pIdMoneda As Integer, ByVal pIdCaja As Integer)
        IdCliente = pIdCliente
        Fecha = pFecha
        Folio = pFolio
        Desglosar = pDesglosar
        Iva = pIva
        IdSucursal = pidSucursal
        Serie = pSerie
        TipodeCambio = pTipodeCambio
        IdMoneda = pIdMoneda
        idCaja = pIdCaja
        Comm.CommandText = "insert into tblventasremisiones(folio,idcliente,fecha,desglosar,credito,iva,totalapagar,total,hora,estado,idsucursal,serie,tipodecambio,idmoneda,usado,fechacancelado,horacancelado,idcaja,idforma,idvendedor,comentariof,porsurtir,idventar,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + Folio.ToString + "," + IdCliente.ToString + ",'" + Fecha + "'," + Desglosar.ToString + "," + Credito.ToString + "," + Iva.ToString + "," + TotalaPagar.ToString + "," + Total.ToString + ",'" + Format(TimeOfDay, "HH:mm:ss") + "'," + CStr(Estados.SinGuardar) + "," + IdSucursal.ToString + ",'" + Replace(Serie, "'", "''") + "'," + TipodeCambio.ToString + "," + IdMoneda.ToString + ",0,'',''," + idCaja.ToString + ",1,1,'',0,0," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        'Comm.ExecuteNonQuery()
        Comm.CommandText += "select ifnull(last_insert_id(),0);"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pEstado As Byte, ByVal pSerie As String, ByVal pTipodeCambio As Double, ByVal pIdMoneda As Integer, ByVal pSubtotal As Double, ByVal pTotal As Double, ByVal pidCliente As Integer, ByVal pidForma As Integer, ByVal pIdVendedor As Integer, ByVal pComentario As String, ByVal pPorSurtir As Byte, ByVal pChecaFolio As Boolean, pIdCaja As Integer)
        ID = pID
        Fecha = pFecha
        Folio = pFolio
        Desglosar = pDesglosar
        Iva = pIva
        Estado = pEstado
        Serie = pSerie
        IdMoneda = pIdMoneda
        TipodeCambio = pTipodeCambio
        IdCliente = pidCliente
        idForma = pidForma
        IdVEndedor = pIdVendedor
        Comentario = pComentario
        PorSurtir = pPorSurtir
        idCaja = pIdCaja
        'Comm.Transaction = Comm.Connection.BeginTransaction()
        AddError("REM: Serie:" + Serie + " Folio:" + Folio.ToString + " Cliente: " + IdCliente.ToString + " Total: " + pTotal.ToString + " Estado: " + Estado.ToString, "Remisiones", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), ID)
        If pChecaFolio And Estado = Estados.Guardada Then
            'Comm.CommandText = "select ifnull((select max(folio) from tblventasremisiones where serie='" + pSerie + "' and (estado=3 or estado=4)),0)"
            'Folio = Comm.ExecuteScalar + 1
            If Estado <> Estados.Guardada Then
                Comm.CommandText = "update tblventasremisiones rp,(select ifnull((select max(folio) from tblventasremisiones where serie='" + pSerie + "' and (estado=3 or estado=4)),0) as fr) as r set rp.fecha='" + Fecha + "',rp.folio=r.fr+1,rp.desglosar=" + Desglosar.ToString + ",rp.iva=" + Iva.ToString + ",rp.estado=" + Estado.ToString + ",rp.serie='" + Replace(Serie, "'", "''") + "',rp.tipodecambio=" + TipodeCambio.ToString + ",rp.idmoneda=" + IdMoneda.ToString + ",rp.total=" + pSubtotal.ToString + ",rp.totalapagar=" + pTotal.ToString + ",rp.idcliente=" + IdCliente.ToString + ",rp.fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',rp.horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',rp.idforma=" + idForma.ToString + ",rp.idvendedor=" + IdVEndedor.ToString + ",rp.comentariof='" + Replace(Comentario, "'", "''") + "',rp.porsurtir=" + PorSurtir.ToString + ",rp.idcaja=" + idCaja.ToString + " where rp.idremision=" + ID.ToString
            Else
                Comm.CommandText = "update tblventasremisiones rp,(select ifnull((select max(folio) from tblventasremisiones where serie='" + pSerie + "' and (estado=3 or estado=4)),0) as fr) as r set rp.fecha='" + Fecha + "',rp.folio=r.fr+1,rp.desglosar=" + Desglosar.ToString + ",rp.iva=" + Iva.ToString + ",rp.estado=" + Estado.ToString + ",rp.serie='" + Replace(Serie, "'", "''") + "',rp.tipodecambio=" + TipodeCambio.ToString + ",rp.idmoneda=" + IdMoneda.ToString + ",rp.total=" + pSubtotal.ToString + ",rp.totalapagar=" + pTotal.ToString + ",rp.idcliente=" + IdCliente.ToString + ",rp.fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',rp.horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',rp.idforma=" + idForma.ToString + ",rp.idvendedor=" + IdVEndedor.ToString + ",rp.hora='" + Format(TimeOfDay, "HH:mm:ss") + "',rp.comentariof='" + Replace(Comentario, "'", "''") + "',rp.porsurtir=" + PorSurtir.ToString + ",rp.idcaja=" + idCaja.ToString + " where rp.idremision=" + ID.ToString
            End If
        Else
            If Estado <> Estados.Guardada Then
                Comm.CommandText = "update tblventasremisiones set fecha='" + Fecha + "',folio=" + Folio.ToString + ",desglosar=" + Desglosar.ToString + ",iva=" + Iva.ToString + ",estado=" + Estado.ToString + ",serie='" + Replace(Serie, "'", "''") + "',tipodecambio=" + TipodeCambio.ToString + ",idmoneda=" + IdMoneda.ToString + ",total=" + pSubtotal.ToString + ",totalapagar=" + pTotal.ToString + ",idcliente=" + IdCliente.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',idforma=" + idForma.ToString + ",idvendedor=" + IdVEndedor.ToString + ",comentariof='" + Replace(Comentario, "'", "''") + "',porsurtir=" + PorSurtir.ToString + ",idcaja=" + idCaja.ToString + " where idremision=" + ID.ToString
            Else
                Comm.CommandText = "update tblventasremisiones set fecha='" + Fecha + "',folio=" + Folio.ToString + ",desglosar=" + Desglosar.ToString + ",iva=" + Iva.ToString + ",estado=" + Estado.ToString + ",serie='" + Replace(Serie, "'", "''") + "',tipodecambio=" + TipodeCambio.ToString + ",idmoneda=" + IdMoneda.ToString + ",total=" + pSubtotal.ToString + ",totalapagar=" + pTotal.ToString + ",idcliente=" + IdCliente.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',idforma=" + idForma.ToString + ",idvendedor=" + IdVEndedor.ToString + ",hora='" + Format(TimeOfDay, "HH:mm:ss") + "',comentariof='" + Replace(Comentario, "'", "''") + "',porsurtir=" + PorSurtir.ToString + ",idcaja=" + idCaja.ToString + " where idremision=" + ID.ToString
            End If
        End If
        
        Comm.ExecuteNonQuery()
        'Comm.Transaction.Commit()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventasremisiones where idremision=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Usar(ByVal pID() As Integer, ByVal pIdventa As Integer)
        Dim Wherefields As String = ""
        For Each IDf As Integer In pID
            Wherefields += " or idremision=" + IDf.ToString
        Next
        Comm.CommandText = "update tblventasremisiones set usado=1,idventar=" + pIdventa.ToString + " where false " + Wherefields
        Comm.ExecuteNonQuery()
        Comm.CommandText = ""
        For Each IDf As Integer In pID
            Comm.CommandText += "update tblmovimientos set idventa=" + pIdventa.ToString + ",idremision=0 where idremision=" + IDf.ToString + ";"
        Next
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pNombreClave As String, ByVal pFolio As String, ByVal pEstado As Byte, ByVal pUsado As Byte, ByVal pidSucursal As Integer, ByVal pIdForma As Integer, ByVal pSerieoc As String, ByVal pMostraroc As Byte, ByVal pSurtido As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventasremisiones.idremision,tblventasremisiones.fecha,concat(tblventasremisiones.serie,convert(tblventasremisiones.folio using utf8)) as folio,tblclientes.clave,tblclientes.nombre as Cliente,tblventasremisiones.totalapagar as Importe,case tblventasremisiones.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado from tblventasremisiones inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblclientes.clave,tblclientes.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblventasremisiones.serie,convert(tblventasremisiones.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado <> 0 Then
            Comm.CommandText += " and tblventasremisiones.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblventasremisiones.estado<>1"
        End If
        If pUsado <> 0 Then
            Comm.CommandText += " and tblventasremisiones.usado<>1"
        End If
        If pIdForma <> 0 Then
            Comm.CommandText += " and tblventasremisiones.idforma=" + pIdForma.ToString
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblventasremisiones.idsucursal=" + pidSucursal.ToString
        End If
        If pMostraroc = 1 Then
            Comm.CommandText += Replace(pSerieoc, "-", " and tblventasremisiones.serie")
            'Comm.CommandText += " and tblventasremisiones.serie<>'" + Replace(pSerieoc, "'", "''") + "'"
        End If
        If pSurtido <> 0 Then
            Comm.CommandText += " and tblventasremisiones.porsurtir=" + CStr(pSurtido - 1)
        End If
        Comm.CommandText += " order by tblventasremisiones.fecha desc,tblventasremisiones.serie,tblventasremisiones.folio desc"
        Comm.CommandTimeout = 10000
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasremisiones")
        Return DS.Tables("tblventasremisiones").DefaultView
    End Function
    Public Function ConsultaRemisionesFacturadas(ByVal pIdventa As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventasremisiones.idremision,tblventasremisiones.fecha,concat(tblventasremisiones.serie,convert(tblventasremisiones.folio using utf8)) as folio,tblclientes.clave,tblclientes.nombre as Cliente,tblventasremisiones.totalapagar as Importe,case tblventasremisiones.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado from tblventasremisiones inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente where idventar=" + pIdventa.ToString
        
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasremisiones")
        Return DS.Tables("tblventasremisiones").DefaultView
    End Function
    Public Function ConsultaDeudas(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidCliente As Integer, ByVal pFolio As String, ByVal pidTipodePago As Integer, ByVal PorFechas As Boolean, ByVal Todas As Boolean, ByVal pTipodeOrden As Byte, ByVal pMostrarCanceladas As Boolean, ByVal pTipodeCambio As Double, ByVal pEnPesos As Boolean, ByVal pIdSucursal As Integer) As DataView
        Dim DS As New DataSet

        'Comm.CommandText = "delete from tblclientesdeudas where idcliente=" + pidCliente.ToString
        'Comm.ExecuteNonQuery()

        'Remisiones
        '"select iddocumento as idventa,0 as sel,cant,fecha,case tipo when 0 then 'Factura'  Par.' end as tipodoc,if(estado=3,'A','C') as estadof,serie,folio,credito,totalapagar,totalapagar-credito as restante,tipo,totalapagar-credito as restante2 from tblclientesdeudas where idcliente=" + pidCliente.ToString

        If pEnPesos Then
            Comm.CommandText = "select " + _
        "tblventasremisiones.idremision,0 as sel,tblventasremisiones.fecha,'Remisión' as tipodoc,if(tblventasremisiones.estado=3,'A','C') as estadof,tblventasremisiones.serie,tblventasremisiones.folio," + _
        "if(tblventasremisiones.idmoneda=2,tblventasremisiones.credito,tblventasremisiones.credito*" + pTipodeCambio.ToString + ") as credito," + _
        "if(tblventasremisiones.idmoneda=2,tblventasremisiones.totalapagar,tblventasremisiones.totalapagar*" + pTipodeCambio.ToString + ") as totalapagar," + _
        "if(tblventasremisiones.idmoneda=2,tblventasremisiones.totalapagar-tblventasremisiones.credito,(tblventasremisiones.totalapagar-tblventasremisiones.credito)*" + pTipodeCambio.ToString + ") as restante," + _
        "0 as tipo," + _
        "if(tblventasremisiones.idmoneda=2,tblventasremisiones.totalapagar-tblventasremisiones.credito,(tblventasremisiones.totalapagar-tblventasremisiones.credito)*" + pTipodeCambio.ToString + ") as restante2" + _
        " from tblventasremisiones inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones on tblformasdepagoremisiones.idforma=tblventasremisiones.idforma where tblventasremisiones.idcliente=" + pidCliente.ToString + " and tblformasdepagoremisiones.tipo=3"
        Else
            Comm.CommandText = "select " + _
        "tblventasremisiones.idremision,0 as sel,tblventasremisiones.fecha,'Remisión' as tipodoc,if(tblventasremisiones.estado=3,'A','C') as estadof,tblventasremisiones.serie,tblventasremisiones.folio,tblventasremisiones.credito,tblventasremisiones.totalapagar,0 from tblventasremisiones inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones on tblformasdepagoremisiones.idforma=tblventasremisiones.idforma where tblventasremisiones.idcliente=" + pidCliente.ToString + " and tblformasdepagoremisiones.tipo=3"
        End If
        If Todas = False Then
            Comm.CommandText += " and round(tblventasremisiones.totalapagar-tblventasremisiones.credito,2)>0"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblventasremisiones.serie,convert(tblventasremisiones.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pMostrarCanceladas Then
            Comm.CommandText += " and (tblventasremisiones.estado=4 or tblventasremisiones.estado=3)"
        Else
            Comm.CommandText += " and tblventasremisiones.estado=3"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventasremisiones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.ExecuteNonQuery()


        'If pEnPesos Then
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",tblventas.idventa,4,tblventas.fecha,tblventas.estado,tblventas.serie,tblventas.folio,if(tblventas.idconversion=2,tblventas.credito,tblventas.credito*" + pTipodeCambio.ToString + "),if(tblventas.idconversion=2,tblventas.totalapagar,tblventas.totalapagar*" + pTipodeCambio.ToString + "),0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblventas.idforma=98 and tblformasdepago.tipo=" + pidTipodePago.ToString
        'Else
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",tblventas.idventa,4,tblventas.fecha,tblventas.estado,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblventas.idforma=98 and tblformasdepago.tipo=" + pidTipodePago.ToString
        'End If

        ''select tblventas.idventa,0 as sel,tblventas.fecha,if(tblventas.estado=3,'A','C') as estadof,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,tblventas.totalapagar-tblventas.credito as restante from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        'If Todas = False Then
        '    Comm.CommandText += " and round(tblventas.totalapagar-tblventas.credito,2)>0"
        'End If
        'If PorFechas Then
        '    Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        'End If
        'If pFolio <> "" Then
        '    Comm.CommandText += " and concat(tblventas.serie,convert(tblventas.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        'End If
        'If pMostrarCanceladas Then
        '    Comm.CommandText += " and (tblventas.estado=4 or tblventas.estado=3)"
        'Else
        '    Comm.CommandText += " and tblventas.estado=3"
        'End If
        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        'End If
        'Comm.ExecuteNonQuery()

        ''Notas de Cargo
        'If pEnPesos Then
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",tblnotasdecargo.idcargo,1,tblnotasdecargo.fecha,tblnotasdecargo.estado,tblnotasdecargo.serie,tblnotasdecargo.folio,if(tblnotasdecargo.idmoneda=2,tblnotasdecargo.aplicado,tblnotasdecargo.aplicado*" + pTipodeCambio.ToString + "),if(tblnotasdecargo.idmoneda=2,tblnotasdecargo.totalapagar,tblnotasdecargo.totalapagar*" + pTipodeCambio.ToString + "),0 from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.idcliente=" + pidCliente.ToString
        'Else
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",tblnotasdecargo.idcargo,1,tblnotasdecargo.fecha,tblnotasdecargo.estado,tblnotasdecargo.serie,tblnotasdecargo.folio,tblnotasdecargo.aplicado,tblnotasdecargo.totalapagar,0 from tblnotasdecargo inner join tblclientes on tblnotasdecargo.idcliente=tblclientes.idcliente where tblnotasdecargo.idcliente=" + pidCliente.ToString
        'End If


        'If Todas = False Then
        '    Comm.CommandText += " and round(tblnotasdecargo.totalapagar-tblnotasdecargo.aplicado,2)>0"
        'End If
        'If PorFechas Then
        '    Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        'End If
        'If pFolio <> "" Then
        '    Comm.CommandText += " and concat(tblnotasdecargo.serie,convert(tblnotasdecargo.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        'End If
        'If pMostrarCanceladas Then
        '    Comm.CommandText += " and (tblnotasdecargo.estado=4 or tblnotasdecargo.estado=3)"
        'Else
        '    Comm.CommandText += " and tblnotasdecargo.estado=3"
        'End If
        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and tblnotasdecargo.idsucursal=" + pIdSucursal.ToString
        'End If
        'Comm.ExecuteNonQuery()

        ''Documentos saldo inicial
        'If pEnPesos Then
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",dc.iddocumento,2,dc.fecha,dc.estado,dc.serie,dc.folio,if(dc.idmoneda=2,dc.credito,dc.credito*" + pTipodeCambio.ToString + "),if(dc.idmoneda=2,dc.totalapagar,dc.totalapagar*" + pTipodeCambio.ToString + "),0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=0"
        'Else
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '    ",dc.iddocumento,2,dc.fecha,dc.estado,dc.serie,dc.folio,dc.credito,dc.totalapagar,0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=0"
        'End If


        'If Todas = False Then
        '    Comm.CommandText += " and round(dc.totalapagar-dc.credito,2)>0"
        'End If
        'If PorFechas Then
        '    Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        'End If
        'If pFolio <> "" Then
        '    Comm.CommandText += " and concat(dc.serie,convert(dc.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        'End If
        'If pMostrarCanceladas Then
        '    Comm.CommandText += " and (dc.estado=4 or dc.estado=3)"
        'Else
        '    Comm.CommandText += " and dc.estado=3"
        'End If
        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and dc.idsucursal=" + pIdSucursal.ToString
        'End If
        'Comm.ExecuteNonQuery()

        ''Documentos documentos
        'If pEnPesos Then
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",dc.iddocumento,3,dc.fecha,dc.estado,dc.seriereferencia,dc.folioreferencia,if(dc.idmoneda=2,dc.credito,dc.credito*" + pTipodeCambio.ToString + "),if(dc.idmoneda=2,dc.totalapagar,dc.totalapagar*" + pTipodeCambio.ToString + "),0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=1"
        'Else
        '    Comm.CommandText = "insert into tblclientesdeudas(idcliente,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '    ",dc.iddocumento,3,dc.fecha,dc.estado,dc.seriereferencia,dc.folioreferencia,dc.credito,dc.totalapagar,0 from tbldocumentosclientes as dc where dc.idcliente=" + pidCliente.ToString + " and dc.tiposaldo=1"
        'End If


        'If Todas = False Then
        '    Comm.CommandText += " and round(dc.totalapagar-dc.credito,2)>0"
        'End If
        'If PorFechas Then
        '    Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        'End If
        'If pFolio <> "" Then
        '    Comm.CommandText += " and concat(dc.seriereferencia,convert(dc.folioreferencia using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        'End If
        'If pMostrarCanceladas Then
        '    Comm.CommandText += " and (dc.estado=4 or dc.estado=3)"
        'Else
        '    Comm.CommandText += " and dc.estado=3"
        'End If
        'If pIdSucursal > 0 Then
        '    Comm.CommandText += " and dc.idsucursal=" + pIdSucursal.ToString
        'End If
        'Comm.ExecuteNonQuery()


        'Comm.CommandText = "select iddocumento as idventa,0 as sel,cant,fecha,case tipo when 0 then 'Factura' when 1 then 'Nota de Cargo' when 2 then 'S. Inicial' when 3 then 'Documento' when 4 then 'Factura Par.' end as tipodoc,if(estado=3,'A','C') as estadof,serie,folio,credito,totalapagar,totalapagar-credito as restante,tipo,totalapagar-credito as restante2 from tblclientesdeudas where idcliente=" + pidCliente.ToString
        'select tblventas.idventa,0 as sel,tblventas.fecha,if(tblventas.estado=3,'A','C') as estadof,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,tblventas.totalapagar-tblventas.credito as restante from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        If pTipodeOrden = 0 Then
            Comm.CommandText += " order by fecha,serie,folio"
        Else
            Comm.CommandText += " order by totalapagar,serie,folio"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesdeudasr")
        Return DS.Tables("tblclientesdeudasr").DefaultView
    End Function

    Public Function DaTotal(ByVal pidVenta As Integer, ByVal pidMoneda As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0

        Dim iIva As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double
        Dim iIdInventario As Integer
        Subtototal = 0
        TotalIva = 0
        TotalVenta = 0
        TotalIeps = 0
        TotalOfertas = 0
        TotalIvaRetenidoConceptos = 0
        Comm.CommandText = "select tipodecambio from tblventasremisiones where idremision=" + pidVenta.ToString
        iTipoCambio = Comm.ExecuteScalar
        
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tblventasremisionesinventario where idremision=" + pidVenta.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblventasremisionesinventario where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select idinventario from tblventasremisionesinventario where iddetalle=" + IDs.Item(Cont).ToString
            iIdInventario = Comm.ExecuteScalar
            If iIdInventario = 1 Then
                TotalOfertas += Precio
            End If
            Comm.CommandText = "select iva from tblventasremisionesinventario where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblventasremisionesinventario where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            Comm.CommandText = "select IEPS from tblventasremisionesinventario where iddetalle=" + IDs.Item(Cont).ToString
            pIEPS = Comm.ExecuteScalar
            Comm.CommandText = "select IVARetenido from tblventasremisionesinventario where iddetalle=" + IDs.Item(Cont).ToString
            pIVARetenido = Comm.ExecuteScalar
            If pidMoneda = 2 Then
                If pidMoneda <> IdMonedaC Then
                    Precio = Precio * iTipoCambio
                End If
            Else
                If IdMonedaC = 2 Then
                    Precio = Precio / iTipoCambio
                End If
            End If
            Subtototal += Precio
            TotalIva += (Precio * (iIva / 100))
            TotalIeps += (Precio * (pIEPS / 100))
            TotalIvaRetenidoConceptos += (Precio * (pIVARetenido / 100))
            Cont += 1
        End While
        TotalVenta = Subtototal + TotalIva + TotalIeps - TotalIvaRetenidoConceptos '- TotalISR - TotalIvaRetenido
        Comm.CommandText = "select ifnull((select sum(tblinventario.peso*tblventasremisionesinventario.cantidad) from tblventasremisionesinventario inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario where tblventasremisionesinventario.idremision=" + pidVenta.ToString + "),0)"
        TotalPeso = Comm.ExecuteScalar
        Return TotalVenta
    End Function
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        'Comm.CommandText = "select ifnull((select max(folio) from tblventasremisiones where serie='" + pSerie + "' and (estado=3 or estado=4)),0)"
        'DaNuevoFolio = Comm.ExecuteScalar + 1
        Comm.CommandText = "select ifnull((select max(folio) from tblventasremisiones where serie='" + Replace(pSerie, "'", "''") + "' and (estado=3 or estado=4) group by serie),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As String, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblventasremisiones where folio='" + pFolio + "' and serie='" + Replace(pSerie, "'", "''") + "' and estado<>1 and estado<>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    'Public Sub SetFacturado(ByVal pidVenta As Integer, ByVal pTipo As TiposFactura, ByVal pCredito As Byte, ByVal pTotal As Double)
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

    

    'Public ReadOnly Property totalLetra(ByVal idmoneda As Integer) As String
    '    Get
    '        Dim f As New PaseLetras
    '        Return f.PASELETRAS(DaTotal(ID, idmoneda), idmoneda) + " " + [Enum].GetName(GetType(MONEDAS), idmoneda)
    '    End Get
    'End Property


    'Public Sub AgregarDetallesReferencia(ByVal PidRemision As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte, ByVal pidAlmacen As Integer)
    '    '0 cotizacion
    '    '1 pedido
    '    '2 remision
    '    '3 ventas

    '    If Tipo = 0 Then
    '        Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal) select " + PidRemision.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0,0,precio from tblventascotizacionesinventario where idcotizacion=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()
    '        Comm.CommandText = "update tblinventarioseries set idremision=" + PidRemision.ToString + ",idcotizacion=0 where idcotizacion=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()
    '        'Comm.CommandText = "insert into tblventasremisionesproductos(idremision,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + PidRemision.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento from tblventascotizacionesproductos where idcotizacion=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()
    '    End If

    '    If Tipo = 1 Then
    '        Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal) select " + PidRemision.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0,0,precio from tblventaspedidosinventario where idpedido=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()

    '        'Comm.CommandText = "insert into tblventasremisionesproductos(idremision,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + PidRemision.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento from tblventaspedidosproductos where idpedido=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()
    '    End If

    '    If Tipo = 2 Then
    '        Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal) select " + PidRemision.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,0,precio from tblventasremisionesinventario where idremision=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()

    '        'Comm.CommandText = "insert into tblventasremisionesproductos(idremision,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + PidRemision.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento from tblventasremisionesproductos where idremision=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()

    '        'Comm.CommandText = "insert into tblventasremisionesservicios(idremision,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidRemision.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasremisionesservicios where idremision=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()
    '        'Comm.CommandText = "update tblinventarioseries set idventa=" + PidRemision.ToString + " where idremision=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()
    '    End If

    '    If Tipo = 3 Then
    '        Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal) select " + PidRemision.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,0,precio from tblventasinventario where idventa=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()

    '        'Comm.CommandText = "insert into tblventasremisionesproductos(idremision,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + PidRemision.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento from tblventasproductos where idventa=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()

    '        'Comm.CommandText = "insert into tblventasremisionesservicios(idremision,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidRemision.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasservicios where idventa=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()
    '    End If
    'End Sub

    Public Sub AgregarDetallesReferencia(ByVal PidRemision As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte, ByVal pidAlmacen As Integer)
        '0 cotizacion
        '1 pedido
        '2 remision
        '3 ventas

        If Tipo = 0 Then
            Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal,IEPS,ivaRetenido,cantidadm,tipocantidadm,cdescuento) select " + PidRemision.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0,0,precio,IEPS,ivaRetenido,cantidad,(select tipocontenido from tblinventario where idinventario=tblventascotizacionesinventario.idinventario),0 from tblventascotizacionesinventario where idcotizacion=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "update tblinventarioseries set idremision=" + PidRemision.ToString + ",idcotizacion=0 where idcotizacion=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()
            'Comm.CommandText = "insert into tblventasremisionesproductos(idremision,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + PidRemision.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento from tblventascotizacionesproductos where idcotizacion=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 1 Then
            Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal,IEPS,ivaRetenido,cantidadm,tipocantidadm,cdescuento) select " + PidRemision.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0,0,precio,IEPS,ivaRetenido,cantidad,(select tipocontenido from tblinventario where idinventario=tblventaspedidosinventario.idinventario),0 from tblventaspedidosinventario where idpedido=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventasremisionesproductos(idremision,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + PidRemision.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento from tblventaspedidosproductos where idpedido=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 2 Then
            Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal,IEPS,ivaRetenido,cantidadm,tipocantidadm,cdescuento) select " + PidRemision.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,0,precio,IEPS,ivaRetenido,cantidadm,tipocantidadm,cdescuento from tblventasremisionesinventario where idremision=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventasremisionesproductos(idremision,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + PidRemision.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento from tblventasremisionesproductos where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventasremisionesservicios(idremision,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidRemision.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasremisionesservicios where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblinventarioseries set idventa=" + PidRemision.ToString + " where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 3 Then
            Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal,IEPS,ivaRetenido,cantidadm,tipocantidadm,cdescuento) select " + PidRemision.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,0,precio,IEPS,ivaRetenido,cantidadm,tipocantidadm,cdescuento from tblventasinventario where idventa=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventasremisionesproductos(idremision,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + PidRemision.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento from tblventasproductos where idventa=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventasremisionesservicios(idremision,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidRemision.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasservicios where idventa=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Sub RegresaInventario(ByVal pId As Integer)
        Dim iUsado As Byte
        Comm.CommandText = "select usado from tblventasremisiones where idremision=" + pId.ToString
        iUsado = Comm.ExecuteScalar
        If iUsado = 0 Then
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,surtido,0,0,0),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen,surtido,0,0),0) from tblventasremisionesinventario where idremision=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioi(idinventario,idalmacen,surtido,0,0,0) from tblventaskitsr where idremision=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventariolotesf(tblventasremisionesinventario.idinventario,tblventasremisionesinventario.idalmacen,tblventasremisioneslotes.surtido,0,0,0,tblventasremisioneslotes.idlote) from tblventasremisioneslotes inner join tblventasremisionesinventario on tblventasremisioneslotes.iddetalle = tblventasremisionesinventario.iddetalle where tblventasremisionesinventario.idremision=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioaduanaf(tblventasremisionesinventario.idinventario,tblventasremisionesinventario.idalmacen,tblventasremisionesaduana.surtido,0,0,0,tblventasremisionesaduana.idaduana) from tblventasremisionesaduana inner join tblventasremisionesinventario on tblventasremisionesaduana.iddetalle = tblventasremisionesinventario.iddetalle where tblventasremisionesinventario.idremision=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "update tblventasremisioneslotes inner join tblventasremisionesinventario on tblventasremisioneslotes.iddetalle=tblventasremisionesinventario.iddetalle set tblventasremisioneslotes.surtido=0 where idremision=" + pId.ToString + ";"
            Comm.CommandText += "update tblventasremisionesaduana inner join tblventasremisionesinventario on tblventasremisionesaduana.iddetalle=tblventasremisionesinventario.iddetalle set tblventasremisionesaduana.surtido=0 where idremision=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()

        End If
    End Sub

    Public Function DaIvas(ByVal pIdRemision As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iva,tvi.precio,tvi.idmoneda,tblventasremisiones.tipodecambio  from tblventasremisionesinventario as tvi inner join tblventasremisiones on tvi.idremision=tblventasremisiones.idremision where tvi.idremision=" + pIdRemision.ToString
        Return Comm.ExecuteReader
    End Function
    Public Function ReporteVentasSeries(ByVal pidRemision As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tblinventario.nombre as descripcion,tvi.cantidad,tblinventarioseries.noserie,tblinventarioseries.fechagarantia,tblventasremisiones.serie,tblventasremisiones.folio,tblventasremisiones.fecha,tblventasremisiones.hora,tblclientes.nombre as cnombre,tblclientes.clave as cclave from tblventasremisionesinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblinventarioseries on tvi.idinventario=tblinventarioseries.idinventario and tvi.idremision=tblinventarioseries.idremision inner join tblventasremisiones on tvi.idremision=tblventasremisiones.idremision inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente where tvi.idremision=" + pidRemision.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasseriesr")
        'DS.WriteXmlSchema("tblventasseriesr.xml")
        Return DS.Tables("tblventasseriesr").DefaultView
    End Function
    'Public Function CreaCadenaOriginal(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
    '    Dim O As New dbOpciones(Comm.Connection)
    '    Dim CO As String = "|2.0|"
    '    ID = pIdVenta
    '    LlenaDatos()
    '    Dim TI As Double
    '    Dim CI As Double
    '    TI = DaTotal(ID, pIdMoneda)
    '    CI = TI * (Iva / 100)
    '    CO += Serie + "|"
    '    CO += Folio.ToString + "|"
    '    CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
    '    CO += NoAprobacion + "|"
    '    CO += YearAprobacion + "|"
    '    CO += "ingreso|UNA SOLA EXHIBICION|"
    '    'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
    '    CO += Format(TI, "#0.00") + "|"
    '    CO += "0|" 'descuento

    '    CO += Format(TI + CI, "#0.00") + "|" ' total factura con iva

    '    CO += O._RFC + "|"
    '    CO += O._NombreEmpresa + "|"
    '    CO += O._Calle + "|"
    '    CO += O._noExterior + "|"
    '    CO += O._noInterior + "|"
    '    CO += O._Colonia + "|"
    '    CO += O._Localidad + "|"
    '    CO += O._ReferenciaDomicilio + "|"
    '    CO += O._Municipio + "|"
    '    CO += O._Estado + "|"
    '    CO += O._Pais + "|"
    '    CO += O._CodigoPostal + "|"

    '    CO += O._CalleLocal + "|"
    '    CO += O._noExteriorLocal + "|"
    '    CO += O._noInteriorLocal + "|"
    '    CO += O._ColoniaLocal + "|"
    '    CO += O._LocalidadLocal + "|"
    '    CO += O._ReferenciaDomicilioLocal + "|"
    '    CO += O._MunicipioLocal + "|"
    '    CO += O._EstadoLocal + "|"
    '    CO += O._PaisLocal + "|"
    '    CO += O._CodigoPostalLocal + "|"

    '    CO += Cliente.RFC + "|"
    '    CO += Cliente.Nombre + "|"
    '    CO += Cliente.Direccion + "|"
    '    CO += Cliente.NoExterior + "|"
    '    CO += Cliente.NoInterior + "|"
    '    CO += Cliente.Colonia + "|"
    '    CO += Cliente.Ciudad + "|"
    '    CO += Cliente.ReferenciaDomicilio + "|"
    '    CO += Cliente.Municipio + "|"
    '    CO += Cliente.Estado + "|"
    '    CO += Cliente.Pais + "|"
    '    CO += Cliente.CP + "|"

    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    End While
    '    DR.Close()

    '    Dim VP As New dbVentasProductos(MySqlcon)
    '    DR = VP.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    End While
    '    DR.Close()

    '    Dim VS As New dbVentasServicios(MySqlcon)
    '    DR = VS.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += "SERV|"
    '        CO += DR("folio") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    End While
    '    DR.Close()
    '    'CO += "IVA|0|0|"
    '    CO += "IVA|"
    '    CO += Iva.ToString + "|"
    '    'CO += Format(((TI - (TI / (1 + (Iva / 100))))), "#0.00") + "|"
    '    'CO += Format((TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|"

    '    CO += Format(CI, "#0.00") + "|"
    '    CO += Format(CI, "#0.00") + "|"

    '    CO = Replace(CO, "||", "|")
    '    CO = Replace(CO, "||", "|")
    '    CO = Replace(CO, "||", "|")
    '    CO = "|" + CO + "|"
    '    Return CO

    'End Function


    'Public Function CreaCadenaOriginal(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
    '    Dim O As New dbOpciones(Comm.Connection)
    '    Dim CO As String = "|2.0|"
    '    ID = pIdVenta
    '    LlenaDatos()

    '    Dim mf As New Gooru.Componentes.CFD.MotorCFD(My.Settings.rutacer, My.Settings.rutakey, My.Settings.passwordkey, Application.StartupPath + "\")

    '    Dim Factura As New Gooru.Componentes.CFD.Comprobante(Fecha, Serie, Folio, NoAprobacion, YearAprobacion, Gooru.Componentes.CFD.ComprobanteTipoDeComprobante.ingreso, "UNA SOLA EXIBICIÓN", "CONTADO", 0, "")
    '    Dim TI As Double
    '    Dim CI As Double
    '    TI = DaTotal(ID, pIdMoneda)
    '    CI = TI * (Iva / 100)
    '    CO += Serie + "|"
    '    CO += Folio.ToString + "|"
    '    CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
    '    CO += NoAprobacion + "|"
    '    CO += YearAprobacion + "|"
    '    CO += "ingreso|UNA SOLA EXHIBICION|"
    '    'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
    '    CO += Format(TI, "#0.00") + "|"
    '    CO += "0|" 'descuento

    '    CO += Format(TI + CI, "#0.00") + "|" ' total factura con iva

    '    CO += O._RFC + "|"
    '    CO += O._NombreEmpresa + "|"
    '    CO += O._Calle + "|"
    '    CO += O._noExterior + "|"
    '    CO += O._noInterior + "|"
    '    CO += O._Colonia + "|"
    '    CO += O._Localidad + "|"
    '    CO += O._ReferenciaDomicilio + "|"
    '    CO += O._Municipio + "|"
    '    CO += O._Estado + "|"
    '    CO += O._Pais + "|"
    '    CO += O._CodigoPostal + "|"

    '    Factura.AgregaDatosEmisor("DEMO101010A1A", O._NombreEmpresa, O._Calle, O._noExterior, O._noInterior, O._Colonia, O._CodigoPostal, O._Municipio, O._Estado, O._Localidad, O._Pais, O._ReferenciaDomicilio)
    '    Factura.AgregaDatosExpedicion(O._CalleLocal, O._noExteriorLocal, O._noInteriorLocal, O._ColoniaLocal, O._CodigoPostalLocal, O._MunicipioLocal, O._EstadoLocal, O._LocalidadLocal, O._PaisLocal, O._ReferenciaDomicilioLocal)
    '    Factura.AgregaDatosReceptor(Cliente.RFC, Cliente.Nombre, Cliente.Direccion, Cliente.NoExterior, Cliente.NoInterior, Cliente.Colonia, Cliente.CP, Cliente.Municipio, Cliente.Estado, Cliente.Ciudad, Cliente.Pais, Cliente.ReferenciaDomicilio)
    '    CO += O._CalleLocal + "|"
    '    CO += O._noExteriorLocal + "|"
    '    CO += O._noInteriorLocal + "|"
    '    CO += O._ColoniaLocal + "|"
    '    CO += O._LocalidadLocal + "|"
    '    CO += O._ReferenciaDomicilioLocal + "|"
    '    CO += O._MunicipioLocal + "|"
    '    CO += O._EstadoLocal + "|"
    '    CO += O._PaisLocal + "|"
    '    CO += O._CodigoPostalLocal + "|"

    '    CO += Cliente.RFC + "|"
    '    CO += Cliente.Nombre + "|"
    '    CO += Cliente.Direccion + "|"
    '    CO += Cliente.NoExterior + "|"
    '    CO += Cliente.NoInterior + "|"
    '    CO += Cliente.Colonia + "|"
    '    CO += Cliente.Ciudad + "|"
    '    CO += Cliente.ReferenciaDomicilio + "|"
    '    CO += Cliente.Municipio + "|"
    '    CO += Cliente.Estado + "|"
    '    CO += Cliente.Pais + "|"
    '    CO += Cliente.CP + "|"

    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '        Factura.AgregaConcepto(DR("cantidad").ToString, DR("tipocantidad").ToString, "", DR("descripcion"), DR("precio") / DR("cantidad"))
    '    End While
    '    DR.Close()

    '    Dim VP As New dbVentasProductos(MySqlcon)
    '    DR = VP.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    End While
    '    DR.Close()

    '    Dim VS As New dbVentasServicios(MySqlcon)
    '    DR = VS.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += "SERV|"
    '        CO += DR("folio") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    End While
    '    DR.Close()
    '    'CO += "IVA|0|0|"
    '    CO += "IVA|"
    '    CO += Iva.ToString + "|"
    '    'CO += Format(((TI - (TI / (1 + (Iva / 100))))), "#0.00") + "|"
    '    'CO += Format((TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|"
    '    Factura.AgregaImpuesto(Gooru.Componentes.CFD.ComprobanteImpuestosTrasladoImpuesto.IVA, CDec(Iva), CDec(CI))
    '    CO += Format(CI, "#0.00") + "|"
    '    CO += Format(CI, "#0.00") + "|"

    '    CO = Replace(CO, "||", "|")
    '    CO = Replace(CO, "||", "|")
    '    CO = Replace(CO, "||", "|")
    '    CO = "|" + CO + "|"

    '    mf.Comprobantes.Add(Factura)
    '    Dim resultado As Gooru.Componentes.CFD.ResultadoProceso = Nothing
    '    'dispara el proceso para la generacion de archivos
    '    resultado = mf.ProcesarComprobantes(True, False)
    '    If resultado.ArchivosXMLPDF.Length <> 0 Then
    '        Dim en As New Encriptador
    '        en.GuardaArchivoTexto("Pueba.xml", resultado.ArchivosXMLPDF(0), System.Text.Encoding.UTF8)
    '    Else
    '        For Each c As Gooru.Componentes.CFD.Comprobante In resultado.ComprobantesNoGenerados
    '            MsgBox(c.ErrorGeneracion.Message)
    '        Next
    '    End If
    '    Return CO

    'End Function


    'Public Function CreaXML(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String) As String
    '    Dim O As New dbOpciones(Comm.Connection)
    '    Dim en As New Encriptador
    '    Dim XMLDoc As String

    '    XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

    '    XMLDoc += "<Comprobante " + vbCrLf

    '    en.Leex509(My.Settings.rutacer)



    '    ID = pIdVenta
    '    LlenaDatos()

    '    Dim TI As Double
    '    Dim CI As Double
    '    TI = DaTotal(ID, pIdMoneda)
    '    CI = TI * (Iva / 100)
    '    If Serie <> "" Then XMLDoc += "serie=""" + Serie + """" + vbCrLf
    '    XMLDoc += "version = ""2.0""" + vbCrLf
    '    XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
    '    XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
    '    If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
    '    If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf
    '    'XMLDoc += "subTotal=""" + Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + """" + vbCrLf
    '    XMLDoc += "subTotal=""" + Format(TI, "#0.00") + """" + vbCrLf
    '    'XMLDoc += "total=""" + Format(TI, "#0.00") + """" + vbCrLf
    '    XMLDoc += "total=""" + Format(TI + CI, "#0.00") + """" + vbCrLf
    '    If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
    '    If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
    '    XMLDoc += "formaDePago=""UNA SOLA EXHIBICION""" + vbCrLf
    '    XMLDoc += "descuento=""" + "0" + """" + vbCrLf
    '    XMLDoc += "tipoDeComprobante=""ingreso""" + vbCrLf
    '    XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf
    '    XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
    '    XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
    '    XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf

    '    XMLDoc += ">"

    '    XMLDoc += "<Emisor rfc=""" + O._RFC + """ nombre=""" + O._NombreEmpresa + """>" + vbCrLf

    '    XMLDoc += "<DomicilioFiscal " + vbCrLf
    '    If O._Calle <> "" Then XMLDoc += "calle = """ + O._Calle + """" + vbCrLf
    '    If O._noExterior <> "" Then XMLDoc += "noExterior=""" + O._noExterior + """" + vbCrLf
    '    If O._noInterior <> "" Then XMLDoc += "noInterior=""" + O._noInterior + """" + vbCrLf
    '    If O._Colonia <> "" Then XMLDoc += "colonia=""" + O._Colonia + """" + vbCrLf
    '    If O._Localidad <> "" Then XMLDoc += "localidad=""" + O._Localidad + """" + vbCrLf
    '    If O._ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilio + """" + vbCrLf
    '    If O._Municipio <> "" Then XMLDoc += "municipio=""" + O._Municipio + """" + vbCrLf
    '    If O._Estado <> "" Then XMLDoc += "estado=""" + O._Estado + """" + vbCrLf
    '    If O._Pais <> "" Then XMLDoc += "pais=""" + O._Pais + """" + vbCrLf
    '    If O._CodigoPostal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostal + """" + vbCrLf
    '    XMLDoc += "/>" + vbCrLf

    '    If O._CalleLocal <> "" Then XMLDoc += "<ExpedidoEn calle=""" + O._CalleLocal + """" + vbCrLf
    '    If O._noExteriorLocal <> "" Then XMLDoc += "noExterior=""" + O._noExteriorLocal + """" + vbCrLf
    '    If O._noInteriorLocal <> "" Then XMLDoc += "noInterior=""" + O._noInteriorLocal + """" + vbCrLf
    '    If O._ColoniaLocal <> "" Then XMLDoc += "colonia=""" + O._ColoniaLocal + """" + vbCrLf
    '    If O._LocalidadLocal <> "" Then XMLDoc += "localidad=""" + O._LocalidadLocal + """" + vbCrLf
    '    If O._ReferenciaDomicilioLocal <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilioLocal + """" + vbCrLf
    '    If O._MunicipioLocal <> "" Then XMLDoc += "municipio=""" + O._MunicipioLocal + """" + vbCrLf
    '    If O._EstadoLocal <> "" Then XMLDoc += "estado=""" + O._EstadoLocal + """" + vbCrLf
    '    If O._PaisLocal <> "" Then XMLDoc += "pais=""" + O._PaisLocal + """" + vbCrLf
    '    If O._CodigoPostalLocal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostalLocal + """"
    '    XMLDoc += "/>" + vbCrLf


    '    XMLDoc += "</Emisor>" + vbCrLf


    '    XMLDoc += "<Receptor rfc=""" + Cliente.RFC + """ nombre=""" + Cliente.Nombre + """>" + vbCrLf

    '    If Cliente.Direccion <> "" Then XMLDoc += "<Domicilio calle=""" + Cliente.Direccion + """" + vbCrLf
    '    If Cliente.NoExterior <> "" Then XMLDoc += "noExterior=""" + Cliente.NoExterior + """" + vbCrLf
    '    If Cliente.NoInterior <> "" Then XMLDoc += "noInterior=""" + Cliente.NoInterior + """" + vbCrLf
    '    If Cliente.Colonia <> "" Then XMLDoc += "colonia=""" + Cliente.Colonia + """" + vbCrLf
    '    If Cliente.Ciudad <> "" Then XMLDoc += "localidad=""" + Cliente.Ciudad + """" + vbCrLf
    '    If Cliente.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Cliente.ReferenciaDomicilio + """" + vbCrLf
    '    If Cliente.Municipio <> "" Then XMLDoc += "municipio=""" + Cliente.Municipio + """" + vbCrLf
    '    If Cliente.Estado <> "" Then XMLDoc += "estado=""" + Cliente.Estado + """" + vbCrLf
    '    If Cliente.Pais <> "" Then XMLDoc += "pais=""" + Cliente.Pais + """" + vbCrLf
    '    If Cliente.CP <> "" Then XMLDoc += "codigoPostal=""" + Cliente.CP + """" + vbCrLf
    '    XMLDoc += "/>" + vbCrLf

    '    XMLDoc += "</Receptor>" + vbCrLf

    '    XMLDoc += "<Conceptos>" + vbCrLf

    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReader(ID)

    '    While DR.Read
    '        XMLDoc += "<Concepto " + vbCrLf
    '        XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
    '        XMLDoc += "unidad=""" + DR("tipocantidad") + """" + vbCrLf
    '        XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
    '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "/> " + vbCrLf
    '    End While
    '    DR.Close()

    '    Dim VP As New dbVentasProductos(MySqlcon)
    '    DR = VP.ConsultaReader(ID)

    '    While DR.Read

    '        XMLDoc += "<Concepto " + vbCrLf
    '        XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
    '        XMLDoc += "unidad=""" + DR("tipocantidad") + """" + vbCrLf
    '        XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
    '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "/> " + vbCrLf

    '        'CO += DR("cantidad").ToString + "|"
    '        'CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        'CO += DR("descripcion") + "|"
    '        'CO += CStr(DR("precio") / DR("cantidad")) + "|"
    '        'CO += DR("precio").ToString + "|"
    '    End While
    '    DR.Close()

    '    Dim VS As New dbVentasServicios(MySqlcon)
    '    DR = VS.ConsultaReader(ID)

    '    While DR.Read

    '        XMLDoc += "<Concepto " + vbCrLf
    '        XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
    '        XMLDoc += "unidad=""SERV""" + vbCrLf
    '        XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
    '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "/> " + vbCrLf

    '        'CO += DR("cantidad").ToString + "|"
    '        'CO += "SERV|"
    '        'CO += DR("folio") + "|"
    '        'CO += DR("descripcion") + "|"
    '        'CO += CStr(DR("precio") / DR("cantidad")) + "|"
    '        'CO += DR("precio").ToString + "|"
    '    End While
    '    DR.Close()
    '    XMLDoc += "</Conceptos>"
    '    'XMLDoc += "<Impuestos totalImpuestosTrasladados=""" + Format(((TI - (TI / (1 + (Iva / 100))))), "#0.00") + """>" + vbCrLf
    '    XMLDoc += "<Impuestos totalImpuestosTrasladados=""" + Format(CI, "#0.00") + """>" + vbCrLf
    '    XMLDoc += "<Traslados>" + vbCrLf
    '    XMLDoc += "<Traslado impuesto=""IVA""" + vbCrLf
    '    XMLDoc += "tasa=""" + Iva.ToString + """" + vbCrLf
    '    XMLDoc += "importe=""" + Format(CI, "#0.00") + """ />" + vbCrLf
    '    XMLDoc += "</Traslados>" + vbCrLf
    '    XMLDoc += "</Impuestos>" + vbCrLf
    '    XMLDoc += "</Comprobante>"


    '    Return XMLDoc

    'End Function
    Public Sub ModificaInventario(ByVal pId As Integer, ByVal pPorSurtir As Byte)
        If PorSurtir = 0 Then
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,cantidad-surtido,0,1,0),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen,cantidad-surtido,0,1),0) from tblventasremisionesinventario where idremision=" + pId.ToString + ";"
            Comm.CommandText += "update tblventasremisionesinventario set surtido=cantidad where idremision=" + pId.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,cantidad-surtido,0,1,0) from tblventaskitsr where idremision=" + pId.ToString + ";"
            Comm.CommandText += "update tblventaskitsr set surtido=cantidad where idremision=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select spmodificainventariolotesf(tblventasremisionesinventario.idinventario,tblventasremisionesinventario.idalmacen,tblventasremisioneslotes.cantidad-tblventasremisioneslotes.surtido,0,1,0,tblventasremisioneslotes.idlote) from tblventasremisioneslotes inner join tblventasremisionesinventario on tblventasremisioneslotes.iddetalle=tblventasremisionesinventario.iddetalle where tblventasremisionesinventario.idremision=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioaduanaf(tblventasremisionesinventario.idinventario,tblventasremisionesinventario.idalmacen,tblventasremisionesaduana.cantidad-tblventasremisionesaduana.surtido,0,1,0,tblventasremisionesaduana.idaduana) from tblventasremisionesaduana inner join tblventasremisionesinventario on tblventasremisionesaduana.iddetalle=tblventasremisionesinventario.iddetalle where tblventasremisionesinventario.idremision=" + pId.ToString + ";"
            Comm.CommandText += "update tblventasremisioneslotes inner join tblventasremisionesinventario on tblventasremisioneslotes.iddetalle=tblventasremisionesinventario.iddetalle set tblventasremisioneslotes.surtido=tblventasremisioneslotes.cantidad where idremision=" + pId.ToString + ";"
            Comm.CommandText += "update tblventasremisionesaduana inner join tblventasremisionesinventario on tblventasremisionesaduana.iddetalle=tblventasremisionesinventario.iddetalle set tblventasremisionesaduana.surtido=tblventasremisionesaduana.cantidad where idremision=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
        End If
    End Sub
    'Public Sub ModificaInventario(ByVal pId As Integer)


    '    Dim Str As String = ""
    '    Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim IDs As New Collection
    '    Dim Cont As Integer = 1
    '    Comm.CommandText = "select iddetalle from tblventasremisionesinventario where idremision=" + pId.ToString
    '    DReader = Comm.ExecuteReader
    '    While DReader.Read()
    '        IDs.Add(DReader("iddetalle"))
    '    End While
    '    DReader.Close()
    '    Dim I As New dbInventario(MySqlcon)
    '    Dim PV As New dbProductosVariantes(MySqlcon)
    '    Dim iIdInventario As Integer
    '    Dim iIdVariante As Integer
    '    Dim iCantidad As Double
    '    Dim iIdAlmacen As Integer
    '    While Cont <= IDs.Count
    '        Comm.CommandText = "select idinventario,idvariante,cantidad,idalmacen from tblventasremisionesinventario where iddetalle=" + IDs(Cont).ToString
    '        DReader = Comm.ExecuteReader
    '        If DReader.Read() Then
    '            iIdInventario = DReader("idinventario")
    '            iIdVariante = DReader("idvariante")
    '            iCantidad = DReader("cantidad")
    '            iIdAlmacen = DReader("idalmacen")
    '            DReader.Close()
    '            If iIdInventario > 1 Then
    '                I.MovimientoDeInventario(iIdInventario, iCantidad, 0, dbInventario.TipoMovimiento.Baja, iIdAlmacen)
    '            End If
    '            If iIdVariante > 1 Then
    '                PV.ModificaInventario(iIdVariante, iCantidad, iIdAlmacen)
    '            End If
    '        Else
    '            DReader.Close()
    '        End If

    '        Cont += 1
    '    End While


    'End Sub
    Public Function VerificaExistencias(ByVal pId As Integer) As String
        'Dim Str As String = ""
        'Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        'Dim IDs As New Collection
        'Dim Cont As Integer = 1
        ''If pIdInventario = 0 Then
        'Comm.CommandText = "select distinct idinventario from tblventasremisionesinventario where idremision=" + pId.ToString
        'DReader = Comm.ExecuteReader
        'While DReader.Read()
        '    IDs.Add(DReader("idinventario"))
        'End While
        'DReader.Close()
        ''Else
        ''IDs.Add(pIdInventario)
        ''End If

        'Dim I As New dbInventario(MySqlcon)
        ''Dim P As New dbProductos(MySqlcon)
        'Dim iIdInventario As Integer
        ''Dim iIdVariante As Integer
        'Dim iCantidad As Double
        'Dim i
        'Dim iIdAlmacen As Integer
        'Dim iCantidad2 As Double
        'Dim EsInventariable As Integer
        'While Cont <= IDs.Count
        '    'Comm.CommandText = "select idinventario,idvariante,cantidad,idalmacen," + _
        '    '"(select inventariable from tblinventario where tblinventario.idinventario=tblventasremisionesinventario.idinventario) as esinventariable,ifnull((select sum(cantidad) from tblventasremisionesinventario where ),0) from tblventasremisionesinventario where iddetalle=" + IDs(Cont).ToString
        '    Comm.CommandText = "select tblinventario.inventariable as esinventariable,ifnull((sum(tblventasremisionesinventario.cantidad)),0) as cantidad,tblventasremisionesinventario.idalmacen from tblventasremisionesinventario inner join tblinventario on tblinventario.idinventario=tblventasremisionesinventario.idinventario where tblventasremisionesinventario.idremision=" + pId.ToString + " and tblventasremisionesinventario.idinventario=" + IDs(Cont).ToString + " limit 1"
        '    'Comm.CommandText = "select "
        '    DReader = Comm.ExecuteReader
        '    If DReader.Read() Then
        '        iIdInventario = IDs(Cont)
        '        iCantidad = DReader("cantidad")
        '        'iIdVariante = DReader("idvariante")
        '        iIdAlmacen = DReader("idalmacen")
        '        EsInventariable = DReader("esinventariable")
        '        DReader.Close()
        '        If iIdInventario > 1 And EsInventariable = 1 Then
        '            iCantidad2 = I.DaInventario(iIdAlmacen, iIdInventario)
        '            'If pIdInventario <> 0 Then iCantidad += 1
        '            If iCantidad > iCantidad2 Then
        '                Str = " Hay artículos con insuficiente inventario."
        '            End If
        '        End If
        '        'If iIdVariante > 1 Then
        '        '    Comm.CommandText = "select spverificaexistenciarecetas(" + iIdVariante.ToString + "," + iIdAlmacen.ToString + "," + iCantidad.ToString + ")"
        '        '    Str = Comm.ExecuteScalar
        '        'End If
        '    Else
        '        DReader.Close()
        '    End If
        '    Cont += 1
        'End While

        Dim Str As String = ""
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Cont As Integer = 1
        Comm.CommandText = "select idinventario from tblventasremisionesinventario where idremision=" + pId.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read()
            IDs.Add(DReader("idinventario"))
        End While
        DReader.Close()
        Dim I As New dbInventario(MySqlcon)
        Dim iIdInventario As Integer
        'Dim iIdVariante As Integer
        Dim iCantidad As Double
        Dim iIdAlmacen As Integer
        Dim iCantidad2 As Double
        Dim EsInventariable As Integer
        Dim iContenido As Double
        While Cont <= IDs.Count
            'Comm.CommandText = "select idinventario,idvariante,cantidad,idalmacen,if(idinventario>1,(select inventariable from tblinventario where tblinventario.idinventario=tblventasinventario.idinventario),0) as esinventariable,if(idvariante>1,(select inventariable from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventasinventario.idvariante),0) as esinventariablep,(select contenido from tblinventario where tblinventario.idinventario=tblventasinventario.idinventario) as contenido from tblventasinventario where idventasinventario=" + IDs(Cont).ToString
            Comm.CommandText = "select tblinventario.inventariable as esinventariable,tblinventario.contenido,ifnull((sum(tblventasremisionesinventario.cantidad)),0) as cantidad,tblventasremisionesinventario.idalmacen from tblventasremisionesinventario inner join tblinventario on tblinventario.idinventario=tblventasremisionesinventario.idinventario where tblventasremisionesinventario.idremision=" + pId.ToString + " and tblventasremisionesinventario.idinventario=" + IDs(Cont).ToString + " limit 1"
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
        Comm.CommandText = "select idinventario from tblventaskitsr where idremision=" + pId.ToString
        DReader = Comm.ExecuteReader
        IDs.Clear()
        While DReader.Read()
            IDs.Add(DReader("idinventario"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            'Comm.CommandText = "select idinventario,idvariante,cantidad,idalmacen,if(idinventario>1,(select inventariable from tblinventario where tblinventario.idinventario=tblventasinventario.idinventario),0) as esinventariable,if(idvariante>1,(select inventariable from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventasinventario.idvariante),0) as esinventariablep,(select contenido from tblinventario where tblinventario.idinventario=tblventasinventario.idinventario) as contenido from tblventasinventario where idventasinventario=" + IDs(Cont).ToString
            Comm.CommandText = "select tblinventario.inventariable as esinventariable,tblinventario.contenido,ifnull((sum(tblventaskitsr.cantidad)),0) as cantidad,tblventaskitsr.idalmacen from tblventaskitsr inner join tblinventario on tblinventario.idinventario=tblventaskitsr.idinventario where tblventaskitsr.idremision=" + pId.ToString + " and tblventaskitsr.idinventario=" + IDs(Cont).ToString + " limit 1"
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
    'Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, ByVal pUsado As Boolean, ByVal pIdVendedor As Integer, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal pSerie As String) As DataView
    '    Dim DS As New DataSet
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "select v.idremision idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idmoneda=2,round(v.total,2),round(v.total*v.tipodecambio,2)) as total,if(v.idmoneda=2,round(v.totalapagar,2),round(v.totalapagar*v.tipodecambio,2)) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.usado,if(v.idventar=0,'',(select concat(serie,convert(folio using utf8)) from tblventas where idventa=v.idventar)) as ref,fp.tipo " + _
    '        "from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente inner join tblformasdepagoremisiones as fp on v.idforma=fp.idforma where  v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
    '    Else
    '        Comm.CommandText = "select v.idremision idventa,v.fecha,v.folio,v.serie,v.estado,round(v.total,2) as total,round(v.totalapagar,2) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.usado,if(v.idventar=0,'',(select concat(serie,convert(folio using utf8)) from tblventas where idventa=idventar)) as ref,fp.tipo " + _
    '        "from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente inner join tblformasdepagoremisiones as fp on v.idforma=fp.idforma where  v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
    '    End If
    '    If pUsado Then
    '        Comm.CommandText += " and v.usado=0"
    '    End If
    '    If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
    '    If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
    '    If pIdVendedor > 0 Then Comm.CommandText += " and v.idvendedor=" + pIdVendedor.ToString
    '    If pSoloCanceladas Then
    '        Comm.CommandText += " and v.estado=4"
    '    Else
    '        Comm.CommandText += " and v.estado=3"
    '    End If
    '    If pMostrarOc = 1 Then
    '        Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
    '    End If
    '    If pSerie <> "" Then
    '        Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
    '    End If
    '    Comm.CommandText += " order by v.fecha,v.serie,v.folio"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblremisiones")
    '    'DS.WriteXmlSchema("tblremisiones.xml")
    '    Return DS.Tables("tblremisiones").DefaultView
    'End Function
    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, ByVal pUsado As Boolean, ByVal pIdVendedor As Integer, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal pSoloActivas As Boolean, pIdCaja As Integer, pIdTipo As Integer, pOrderporCliente As Boolean, pidTipoSucursal As Integer, pFormadepago As Byte) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idremision idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idmoneda=2,round(v.total,2),round(v.total*v.tipodecambio,2)) as total,if(v.idmoneda=2,round(v.totalapagar,2),round(v.totalapagar*v.tipodecambio,2)) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.usado,if(v.idventar=0,'',(select concat(serie,convert(folio using utf8)) from tblventas where idventa=v.idventar)) as ref,fp.tipo,v.idsucursal,tblsucursales.nombre snombre,c.idcliente " + _
            "from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente inner join tblformasdepagoremisiones as fp on v.idforma=fp.idforma inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales on v.idsucursal=tblsucursales.idsucursal where  v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idremision idventa,v.fecha,v.folio,v.serie,v.estado,round(v.total,2) as total,round(v.totalapagar,2) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.usado,if(v.idventar=0,'',(select concat(serie,convert(folio using utf8)) from tblventas where idventa=idventar)) as ref,fp.tipo,v.idsucursal,tblsucursales.nombre snombre,c.idcliente " + _
            "from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente inner join tblformasdepagoremisiones as fp on v.idforma=fp.idforma inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales on v.idsucursal=tblsucursales.idsucursal where  v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pUsado Then
            Comm.CommandText += " and v.usado=0"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pidTipoSucursal > 0 Then Comm.CommandText += " and tblsucursales.idtipo=" + pidTipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pIdTipo > 0 Then Comm.CommandText += " and c.idtipo=" + pIdTipo.ToString
        If pIdVendedor > 0 Then Comm.CommandText += " and v.idvendedor=" + pIdVendedor.ToString
        If pFormadepago = 1 Then Comm.CommandText += " and fp.tipo=3"
        If pFormadepago = 2 Then Comm.CommandText += " and fp.tipo<>3"
        If pSoloActivas Then
            Comm.CommandText += " and v.estado=3"
        Else
            If pSoloCanceladas Then
                Comm.CommandText += " and v.estado=4"
            Else
                Comm.CommandText += " and (v.estado=3 or v.estado=4)"
            End If
        End If
        If pIdCaja > 0 Then
            Comm.CommandText += " and v.idcaja=" + pIdCaja.ToString
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and c.zona='" + pZona2.ToString() + "' or c.zona2='" + pZona2.ToString() + "'"
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pOrderporCliente = False Then
            Comm.CommandText += " order by v.fecha,v.serie,v.folio"
        Else
            Comm.CommandText += " order by c.nombre,v.fecha,v.serie,v.folio"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblremisiones")
        'DS.WriteXmlSchema("tblremisiones.xml")
        Return DS.Tables("tblremisiones").DefaultView
    End Function
    Public Function ReporteCanceladas(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, ByVal pUsado As Boolean, ByVal pIdVendedor As Integer, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal pSoloActivas As Boolean, pIdCaja As Integer, pIdTipo As Integer, pidTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idremision idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idmoneda=2,round(v.total,2),round(v.total*v.tipodecambio,2)) as total,if(v.idmoneda=2,round(v.totalapagar,2),round(v.totalapagar*v.tipodecambio,2)) as totalapagar,v.fecha,s.nombre as tipodecambio,v.idmoneda,c.nombre as cnombre,v.usado,if(v.idventar=0,'',(select concat(serie,convert(folio using utf8)) from tblventas where idventa=v.idventar)) as ref,fp.tipo " + _
            "from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente inner join tblformasdepagoremisiones as fp on v.idforma=fp.idforma inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on v.idsucursal=s.idsucursal where  v.fechacancelado>='" + pFecha1 + "' and v.fechacancelado<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idremision idventa,v.fecha,v.folio,v.serie,v.estado,round(v.total,2) as total,round(v.totalapagar,2) as totalapagar,v.fecha,s.nombre as tipodecambio,v.idmoneda,c.nombre as cnombre,v.usado,if(v.idventar=0,'',(select concat(serie,convert(folio using utf8)) from tblventas where idventa=idventar)) as ref,fp.tipo " + _
            "from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente inner join tblformasdepagoremisiones as fp on v.idforma=fp.idforma inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on v.idsucursal=s.idsucursal where  v.fechacancelado>='" + pFecha1 + "' and v.fechacancelado<='" + pFecha2 + "'"
        End If
        If pUsado Then
            Comm.CommandText += " and v.usado=0"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdtipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdtipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pIdVendedor > 0 Then Comm.CommandText += " and v.idvendedor=" + pIdVendedor.ToString
        'If pSoloActivas Then
        'Comm.CommandText += " and v.estado=3"
        'Else
        'If pSoloCanceladas Then
        Comm.CommandText += " and v.estado=4"
        'Else
        'Comm.CommandText += " and (v.estado=3 or v.estado=4)"
        'End If
        'End If
        If pIdCaja > 0 Then
            Comm.CommandText += " and v.idcaja=" + pIdCaja.ToString
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and c.zona='" + pZona2.ToString() + "' or c.zona2='" + pZona2.ToString() + "'"
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and c.idtipo=" + pIdTipo.ToString
        End If
        Comm.CommandText += " order by v.fecha,v.serie,v.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblremisiones")
        'DS.WriteXmlSchema("tblremisiones.xml")
        Return DS.Tables("tblremisiones").DefaultView
    End Function
    Public Function ReporteVentasArticulos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pUsado As Boolean, ByVal pCondensado As Boolean, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal pSerie As String, ByVal pPorVendedor As Boolean, ByVal pZona As Integer, ByVal pZona2 As Integer, pIdAlmacen As Integer, pidCaja As Integer, pIdTipo As Integer, pidTipoSucursal As Integer, pSoloIEPS As Boolean, pFormadePago As Byte) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idremision idventa,v.folio,v.serie,v.estado,v.total,v.totalapagar,f.tipo as formadepago,v.fecha,s.nombre as tipodecambio,vi.idmoneda,vi.cantidad,i.nombre as descripcion,round(if(vi.idmoneda=2,vi.precio,vi.precio*v.tipodecambio),2) as precio,0 as costoinv,0 as costopro,vi.idinventario,vi.idvariante,c.nombre as cnombre,vi.iva,v.usado,round(if(vi.idmoneda=2,vi.precio/vi.cantidad,vi.precio*v.tipodecambio/vi.cantidad),2) as preciou,i.clave as codigoa,v.idvendedor,(select nombre from tblvendedores where idvendedor=v.idvendedor) as vnombre,v.porsurtir,vi.ieps,vi.ivaretenido ivaret " + _
            "from tblventasremisiones v inner join tblventasremisionesinventario vi on v.idremision=vi.idremision inner join tblinventario i on vi.idinventario=i.idinventario inner join tblclientes c on v.idcliente=c.idcliente inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on v.idsucursal=s.idsucursal inner join tblformasdepagoremisiones as f on v.idforma=f.idforma where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idremision idventa,v.folio,v.serie,v.estado,v.total,v.totalapagar,f.tipo as formadepago,v.fecha,s.nombre as tipodecambio,vi.idmoneda,vi.cantidad,i.nombre as descripcion,round(vi.precio,2),0 as costoinv,0 as costopro,vi.idinventario,vi.idvariante,c.nombre as cnombre,vi.iva,v.usado,round(vi.precio/vi.cantidad,2) as preciou,i.clave as codigoa,v.idvendedor,(select nombre from tblvendedores where idvendedor=v.idvendedor) as vnombre,v.porsurtir,vi.ieps,vi.ivaretenido ivaret " + _
            "from tblventasremisiones v inner join tblventasremisionesinventario vi on v.idremision=vi.idremision inner join tblinventario i on vi.idinventario=i.idinventario inner join tblclientes c on v.idcliente=c.idcliente inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on v.idsucursal=s.idsucursal inner join tblformasdepagoremisiones as f on v.idforma=f.idforma where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pidTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pidTipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pidVendedor > 0 Then Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        If pidMoneda > 0 Then Comm.CommandText += " and vi.idmoneda=" + pidMoneda.ToString
        If pIdTipo > 0 Then Comm.CommandText += " and c.idtipo=" + pIdTipo.ToString()
        If pUsado Then Comm.CommandText += " and v.usado=0"
        If pSoloIEPS Then Comm.CommandText += " and vi.ieps<>0"
        If pFormadePago = 1 Then Comm.CommandText += " and f.tipo=3"
        If pFormadePago = 2 Then Comm.CommandText += " and f.tipo<>3"
        If pIdInventario > 1 Then
            Comm.CommandText += " and vi.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then Comm.CommandText += " and i.idclasificacion=" + pidClasificacion.ToString
            If pidClasificacion2 > 0 Then Comm.CommandText += " and i.idclasificacion2=" + pidClasificacion2.ToString
            If pidClasificacion3 > 0 Then Comm.CommandText += " and i.idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and c.zona='" + pZona2.ToString() + "' or c.zona2='" + pZona2.ToString() + "'"
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and vi.idalmacen=" + pIdAlmacen.ToString
        End If
        If pidCaja > 0 Then
            Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
        End If
        If pCondensado = False Then
            Comm.CommandText += " order by v.fecha,v.serie,v.folio"
        Else
            If pPorVendedor = False Then
                Comm.CommandText += " order by vi.idinventario,preciou"
            Else
                Comm.CommandText += " order by v.idvendedor,vi.idinventario"
            End If
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and c.idtipo=" + pIdTipo.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventasra.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Function ReporteVentasArticulosClases(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pUsado As Boolean, ByVal pCondensado As Boolean, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer, pIdAlmacen As Integer, pIdCaja As Integer, pIdTipo As Integer, pIdTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idremision idventa,v.folio,v.serie,v.estado,v.total,v.totalapagar,v.fecha,s.nombre as tipodecambio,vi.idmoneda,vi.cantidad,i.nombre as descripcion,round(if(vi.idmoneda=2,vi.precio,vi.precio*v.tipodecambio),2) as precio,0 as costoinv,0 as costopro,vi.idinventario,c.nombre as cnombre,vi.iva,v.usado,round(if(vi.idmoneda=2,vi.precio/vi.cantidad,vi.precio*v.tipodecambio/vi.cantidad),2) as preciou,i.clave as codigoa,(select nombre from tblinventarioclasificaciones where idclasificacion=i.idclasificacion) as nclase1,(select nombre from tblinventarioclasificaciones2 where idclasificacion=i.idclasificacion2) as nclase2,(select nombre from tblinventarioclasificaciones3 where idclasificacion=i.idclasificacion3) as nclase3,i.idclasificacion,i.idclasificacion2,i.idclasificacion3,v.porsurtir,vi.ieps,vi.ivaretenido ivaret  " + _
            "from tblventasremisiones v inner join tblventasremisionesinventario vi on v.idremision=vi.idremision inner join tblinventario i on vi.idinventario=i.idinventario inner join tblclientes c on v.idcliente=c.idcliente inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on v.idsucursal=s.idsucursal where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idremision idventa,v.folio,v.serie,v.estado,v.total,v.totalapagar,v.fecha,s.nombre as tipodecambio,vi.idmoneda,vi.cantidad,i.nombre as descripcion,round(vi.precio,2),0 as costoinv,0 as costopro,vi.idinventario,c.nombre as cnombre,vi.iva,v.usado,round(vi.precio/vi.cantidad,2) as preciou,i.clave as codigoa,(select nombre from tblinventarioclasificaciones where idclasificacion=i.idclasificacion) as nclase1,(select nombre from tblinventarioclasificaciones2 where idclasificacion=i.idclasificacion2) as nclase2,(select nombre from tblinventarioclasificaciones3 where idclasificacion=i.idclasificacion3) as nclase3,i.idclasificacion,i.idclasificacion2,i.idclasificacion3,v.porsurtir,vi.ieps,vi.ivaretenido ivaret  " + _
            "from tblventasremisiones v inner join tblventasremisionesinventario vi on v.idremision=vi.idremision innner join tblinventario i on vi.idinventario=i.idinventario inner join tblclientes c on v.idcliente=c.idcliente inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on v.idsucursal=s.idsucursal where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pidVendedor > 0 Then Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        If pidMoneda > 0 Then Comm.CommandText += " and vi.idmoneda=" + pidMoneda.ToString
        If pUsado Then Comm.CommandText += " and v.usado=0"
        If pIdInventario > 1 Then
            Comm.CommandText += " and vi.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then Comm.CommandText += " and i.idclasificacion=" + pidClasificacion.ToString
            If pidClasificacion2 > 0 Then Comm.CommandText += " and i.idclasificacion2=" + pidClasificacion2.ToString
            If pidClasificacion3 > 0 Then Comm.CommandText += " and i.idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and c.zona='" + pZona2.ToString() + "' or c.zona2='" + pZona2.ToString() + "'"
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and vi.idalmacen=" + pIdAlmacen.ToString
        End If
        If pIdCaja > 0 Then
            Comm.CommandText += " and v.idcaja=" + pIdCaja.ToString
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and c.idtipo=" + pIdTipo.ToString
        End If
        If pCondensado = False Then
            Comm.CommandText += " order by i.idclasificacion,i.idclasificacion2,i.idclasificacion3,v.fecha,v.serie,v.folio"
        Else
            Comm.CommandText += " order by i.idclasificacion,i.idclasificacion2,i.idclasificacion3,vi.idinventario"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventasrac.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Function RevisaConceptos(ByVal pIdVenta As Integer) As Boolean
        Dim C1 As Integer
        Dim C2 As Integer
        Comm.CommandText = "select count(idmoneda) from tblventasremisionesinventario where idremision=" + pIdVenta.ToString + " and idmoneda=2"
        C1 = Comm.ExecuteScalar
        Comm.CommandText = "select count(idmoneda) from tblventasremisionesinventario where idremision=" + pIdVenta.ToString + " and idmoneda<>2"
        C2 = Comm.ExecuteScalar
        If C1 <> 0 And C2 <> 0 Then
            Return False
        End If
        Return True
    End Function
    Public Function BuscaRemision(ByVal pFolio As String) As Integer
        Comm.CommandText = "select ifnull((select idremision from tblventasremisiones where concat(tblventasremisiones.serie,convert(tblventasremisiones.folio using utf8))='" + Replace(pFolio, "'", "''") + "' and estado>2 limit 1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function ReportexVendedor(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, ByVal pUsado As Boolean, ByVal pIdVendedor As Integer, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer, pidCaja As Integer, pIdTipo As Integer, pIdTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idremision idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idmoneda=2,v.total,v.total*v.tipodecambio) as total,if(v.idmoneda=2,v.totalapagar,v.totalapagar*v.tipodecambio) as totalapagar,v.fecha,s.nombre as tipodecambio,v.idmoneda,c.nombre as cnombre,v.usado,tblvendedores.nombre as vnombre,v.idvendedor " + _
            "from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on v.idsucursal=s.idsucursal where  v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idremision idventa,v.fecha,v.folio,v.serie,v.estado,v.total as total,v.totalapagar as totalapagar,v.fecha,s.nombre as tipodecambio,v.idmoneda,c.nombre as cnombre,v.usado,tblvendedores.nombre as vnombre,v.idvendedor " + _
            "from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on v.idsucursal=s.idsucursal where  v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pUsado Then
            Comm.CommandText += " and v.usado=0"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pIdVendedor > 0 Then Comm.CommandText += " and v.idvendedor=" + pIdVendedor.ToString
        If pSoloCanceladas Then
            Comm.CommandText += " and v.estado=4"
        Else
            Comm.CommandText += " and (v.estado=3 or v.estado=4)"
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and c.zona='" + pZona2.ToString() + "' or c.zona2='" + pZona2.ToString() + "'"
        End If
        If pidCaja > 0 Then
            Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and c.idtipo=" + pIdTipo.ToString()
        End If
        Comm.CommandText += " order by v.fecha,v.serie,v.folio,v.idvendedor"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblremisiones")
        'DS.WriteXmlSchema("tblremisionesv.xml")
        Return DS.Tables("tblremisiones").DefaultView
    End Function
    Public Function ReporteVentasArticulosMasVendidos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSerie As String, ByVal pOrdenxVendedor As Boolean, ByVal pLimite As String, ByVal pTipoOrden As Byte, ByVal pMostrarOc As Byte, ByVal pSerieOc As String, ByVal pZona As Integer, ByVal pZona2 As Integer, pIdAlmacen As Integer, pIdCaja As Integer, pIdTipo As Integer, pIdTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select sum(tblventasinventario.cantidad) as cantidad,tblinventario.nombre as descripcion,sum(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio)) as precio,tblventasinventario.idinventario,tblventasinventario.iva,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,s.nombre as idvendedor,tblventasinventario.ieps,tblventasinventario.ivaretenido ivaret,s.nombre snombre,tblventas.idsucursal  " + _
            "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on tblventas.idsucursal=s.idsucursal where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventasinventario.cantidad<>0"
        Else
            Comm.CommandText = "select sum(tblventasinventario.cantidad) as cantidad,tblinventario.nombre as descripcion,sum(tblventasinventario.precio) as precio,tblventasinventario.idinventario,tblventasinventario.iva,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventasinventario.ieps,tblventasinventario.ivaretenido ivaret,s.nombre snombre,tblventas.idsucursal  " + _
            "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on tblventas.idsucursal=s.idsucursal where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventasinventario.cantidad<>0"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
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
            Comm.CommandText += " and tblclientes.zona='" + pZona2.ToString() + "' or tblclientes.zona2='" + pZona2.ToString() + "'"
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
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and tblventas.serie")
            'Comm.CommandText += " and tblventas.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        If pIdCaja > 0 Then
            Comm.CommandText += " and tblventas.idcaja=" + pIdCaja.ToString
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and tblclientes.idtipo=" + pIdTipo.ToString
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
    'Public Function ReportePorTipodePago(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSerie As String, ByVal pMostrarOc As Byte, ByVal pSerieOc As String, ByVal pUsado As Boolean, ByVal pIdForma As Integer) As DataView
    '    Dim DS As New DataSet
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "select tblventas.idremision,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idmoneda=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idmoneda,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre,tblventas.idforma,tblformasdepago.tipo,tblventas.usado,(select concat(v.serie,convert(v.folio using utf8)) from tblventas as v where v.idventa=tblventas.idventar) as ref,tblventasinventario.iva " + _
    '        "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
    '    Else
    '        Comm.CommandText = "select tblventas.idremision,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idmoneda,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre,tblventas.idforma,tblformasdepago.tipo,tblventas.usado,(select concat(v.serie,convert(v.folio using utf8)) from tblventas as v where v.idventa=tblventas.idventar) as ref,tblventasinventario.iva " + _
    '        "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
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
    '        Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
    '    End If
    '    If pSerie <> "" Then
    '        Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
    '    End If
    '    If pMostrarOc = 1 Then
    '        Comm.CommandText += " and tblventas.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
    '    End If
    '    If pUsado Then
    '        Comm.CommandText += " and tblventas.usado=0"
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
    Public Function ReportePorTipodePago(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSerie As String, ByVal pMostrarOc As Byte, ByVal pSerieOc As String, ByVal pUsado As Boolean, ByVal pIdForma As Integer, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal PSoloCanceladas As Boolean, pIdAlmacen As Integer, pIdCaja As Integer, pIdTipo As Integer, pIdInventario As Integer, pIdClasificacion As Integer, pIdClasificacion2 As Integer, pIdClasificacion3 As Integer, pOrdenarPorClas As Boolean, pTipoB As Boolean, pSubRep As Boolean, pIdTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idremision,tblventas.folio,tblventas.serie,tblventas.estado,if(tblventas.idmoneda=2,tblventas.total,tblventas.total*tblventas.tipodecambio) as total,if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio) as totalapagar,tblventas.fecha,s.nombre as sucursal,tblventas.idmoneda,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventas.usado," +
                "round(if(tblventasinventario.idmoneda=2,if(tblventas.totalapagar<>0,tblventasinventario.precio*(tvf.cantidad/tblventas.totalapagar),0),if(tblventas.totalapagar<>0,tblventasinventario.precio*(tvf.cantidad/tblventas.totalapagar),0)*tblventas.tipodecambio),2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre,tvf.idforma,tblformasdepago.tipo,tblventas.usado,(select concat(v.serie,convert(v.folio using utf8)) from tblventas as v where v.idventa=tblventas.idventar) as ref," +
                "round(if(tblventas.idmoneda=2,if(tblventas.totalapagar<>0,tblventasinventario.precio*(tvf.cantidad/tblventas.totalapagar)*(tblventasinventario.iva+tblventasinventario.ieps-tblventasinventario.ivaretenido)/100,0),if(tblventas.totalapagar<>0,tblventasinventario.precio*(tvf.cantidad/tblventas.totalapagar)*(tblventasinventario.iva+tblventasinventario.ieps-tblventasinventario.ivaretenido)/100,0)*tblventas.tipodecambio),2) as iva,ic.nombre nombreclas,ic.idclasificacion, " + _
                "round(if(tblventas.idmoneda=2,tvf.cantidad,tvf.cantidad*tblventas.tipodecambio),2) as totalmetodo,round(if(totalapagar<>0,if(tblventas.idmoneda=2,(tblventas.totalapagar-tblventas.total)*(tvf.cantidad/tblventas.totalapagar),(tblventas.totalapagar-tblventas.total)*(tvf.cantidad/tblventas.totalapagar)*tblventas.tipodecambio),0),2) as ivametodo " + _
            "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblremisionesformasdepago tvf on tblventas.idremision=tvf.idremision inner join tblformasdepagoremisiones as tblformasdepago on tblformasdepago.idforma=tvf.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on tblventas.idsucursal=s.idsucursal inner join tblinventarioclasificaciones ic on tblinventario.idclasificacion=ic.idclasificacion where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblventas.idremision,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,s.nombre as sucursal,tblventas.idmoneda,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventas.usado," +
                "round(if(tblventas.totalapagar<>0,tblventasinventario.precio*(tvf.cantidad/tblventas.totalapagar),0),2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre,tvf.idforma,tblformasdepago.tipo,tblventas.usado,(select concat(v.serie,convert(v.folio using utf8)) from tblventas as v where v.idventa=tblventas.idventar) as ref," +
                "round(if(tblventas.totalapagar<>0,tblventasinventario.precio*(tvf.cantidad/tblventas.totalapagar)*(tblventasinventario.iva+tblventasinventario.ieps-tblventasinventario.ivaretenido)/100,0),2) as iva,ic.nombre nombreclas,ic.idclasificacion, " + _
                "round(tvf.cantidad,2) as totalmetodo,round(if(totalapagar<>0,(tblventas.totalapagar-tblventas.total)*(tvf.cantidad/tblventas.totalapagar),0),2) as ivametodo " + _
            "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblremisionesformasdepago tvf on tblventas.idremision=tvf.idremision inner join tblformasdepagoremisiones as tblformasdepago on tblformasdepago.idforma=tvf.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on tblventas.idsucursal=s.idsucursal inner join tblinventarioclasificaciones ic on tblinventario.idclasificacion=ic.idclasificacion where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
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
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and tblventas.serie")
            'Comm.CommandText += " and tblventas.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pIdCaja > 0 Then
            Comm.CommandText += " and tblventas.idcaja=" + pIdCaja.ToString
        End If
        If pUsado Then
            Comm.CommandText += " and tblventas.usado=0"
        End If
        If pIdForma > 0 Then
            Comm.CommandText += " and tblventas.idforma=" + pIdForma.ToString
        End If
        If PSoloCanceladas Then
            Comm.CommandText += " and tblventas.estado=4"
        Else
            If pOrdenarPorClas Then
                Comm.CommandText += " and tblventas.estado=3"
            Else
                Comm.CommandText += " and (tblventas.estado=3 or tblventas.estado=4)"
            End If

        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasremisionesinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and tblclientes.idtipo=" + pIdTipo.ToString()
        End If
        If pOrdenarPorClas Then
            If pIdInventario > 1 Then
                Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
            Else
                If pIdClasificacion > 0 Then
                    Comm.CommandText += " and tblinventario.idclasificacion=" + pIdClasificacion.ToString
                End If
                If pIdClasificacion2 > 0 Then
                    Comm.CommandText += " and tblinventario.idclasificacion2=" + pIdClasificacion.ToString
                End If
                If pIdClasificacion3 > 0 Then
                    Comm.CommandText += " and tblinventario.idclasificacion3=" + pIdClasificacion.ToString
                End If
            End If
            Comm.CommandText += " order by ic.nombre,tblventas.fecha,tblventas.serie,tblventas.folio"
        Else
            'Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
            If pTipoB = False Then
                Comm.CommandText += " order by tblformasdepago.tipo,tblformasdepago.idforma,tblventas.fecha,tblventas.serie,tblventas.folio"
            Else
                If pSubRep = False Then
                    Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio,tblformasdepago.idforma"
                Else
                    Comm.CommandText += " order by tblformasdepago.idforma,tblventas.idventa"
                End If
            End If
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventastpr.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function

    Public Function ReportePorTipodePagoN(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSerie As String, ByVal pMostrarOc As Byte, ByVal pSerieOc As String, ByVal pUsado As Boolean, ByVal pIdForma As Integer, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal PSoloCanceladas As Boolean, pIdCaja As Integer, pIdTipo As Integer, pidTipoSucursal As Integer, pFormadepago As Byte) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select vr.idremision,fp.idforma,fp.nombre,fp.tipo,vr.serie,vr.folio,vr.fecha,round(if(vr.idmoneda=2,tvf.cantidad,tvf.cantidad*vr.tipodecambio),2) as totalmetodo,round(if(vr.totalapagar<>0,if(vr.idmoneda=2,(vr.totalapagar-vr.total)*(tvf.cantidad/vr.totalapagar),(vr.totalapagar-vr.total)*(tvf.cantidad/vr.totalapagar)*vr.tipodecambio),0),2) as ivametodo,s.idsucursal,s.nombre as sucursal,vr.usado,c.nombre as cnombre,vr.estado " +
                " from tblventasremisiones vr inner join tblremisionesformasdepago tvf on vr.idremision=tvf.idremision inner join tblformasdepagoremisiones fp on tvf.idforma=fp.idforma inner join tblclientes c on c.idcliente=vr.idcliente inner join tblsucursales s on vr.idsucursal=s.idsucursal inner join tblvendedores vd on vr.idvendedor=vd.idvendedor " +
                "where vr.fecha>='" + pFecha1 + "' and vr.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select vr.idremision,fp.idforma,fp.nombre,fp.tipo,vr.serie,vr.folio,vr.fecha,round(tvf.cantidad,2) as totalmetodo,round(if(vr.totalapagar<>0,(vr.totalapagar-vr.total)*(tvf.cantidad/vr.totalapagar),0),2) as ivametodo,s.idsucursal,s.nombre as sucursal,vr.usado,c.nombre as cnombre,vr.estado " +
                " from tblventasremisiones vr inner join tblremisionesformasdepago tvf on vr.idremision=tvf.idremision inner join tblformasdepagoremisiones fp on tvf.idforma=fp.idforma inner join tblclientes c on c.idcliente=vr.idcliente inner join tblsucursales s on vr.idsucursal=s.idsucursal inner join tblvendedores vd on vr.idvendedor=vd.idvendedor " +
                "where vr.fecha>='" + pFecha1 + "' and vr.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and vr.idsucursal=" + pIdSucursal.ToString
        End If
        If pidTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pidTipoSucursal.ToString
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and vd.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and c.zona='" + pZona2.ToString() + "' or c.zona2='" + pZona2.ToString() + "'"
        End If

        If pIdCliente > 0 Then
            Comm.CommandText += " and vr.idcliente=" + pIdCliente.ToString
        End If
        If pIdVendedor > 0 Then
            Comm.CommandText += " and vr.idvendedor=" + pIdVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and vr.idmoneda=" + pidMoneda.ToString
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and vr.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pFormadepago = 1 Then Comm.CommandText += " and fp.tipo=3"
        If pFormadepago = 2 Then Comm.CommandText += " and fp.tipo<>3"
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and vr.serie")
            'Comm.CommandText += " and tblventas.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pIdCaja > 0 Then
            Comm.CommandText += " and vr.idcaja=" + pIdCaja.ToString
        End If
        If pUsado Then
            Comm.CommandText += " and vr.usado=0"
        End If
        If pIdForma > 0 Then
            Comm.CommandText += " and tvf.idforma=" + pIdForma.ToString
        End If
        If PSoloCanceladas Then
            Comm.CommandText += " and vr.estado=4"
        Else
            'If pOrdenarPorClas Then
            'Comm.CommandText += " and vr.estado=3"
            'Else
            Comm.CommandText += " and (vr.estado=3 or vr.estado=4)"
            'End If
        End If
        'If pIdAlmacen > 0 Then
        '    Comm.CommandText += " and tblventasremisionesinventario.idalmacen=" + pIdAlmacen.ToString
        'End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and c.idtipo=" + pIdTipo.ToString()
        End If
        'If pOrdenarPorClas Then
        '    If pIdInventario > 1 Then
        '        Comm.CommandText += " and tblventasinventario.idinventario=" + pIdInventario.ToString
        '    Else
        '        If pIdClasificacion > 0 Then
        '            Comm.CommandText += " and tblinventario.idclasificacion=" + pIdClasificacion.ToString
        '        End If
        '        If pIdClasificacion2 > 0 Then
        '            Comm.CommandText += " and tblinventario.idclasificacion2=" + pIdClasificacion.ToString
        '        End If
        '        If pIdClasificacion3 > 0 Then
        '            Comm.CommandText += " and tblinventario.idclasificacion3=" + pIdClasificacion.ToString
        '        End If
        '    End If
        '    Comm.CommandText += " order by ic.nombre,tblventas.fecha,tblventas.serie,tblventas.folio"
        'Else
        'Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
        'If pTipoB = False Then
        Comm.CommandText += " order by fp.tipo,fp.idforma,vr.fecha,vr.serie,vr.folio"
        'Else
        '    If pSubRep = False Then
        '        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio,tblformasdepago.idforma"
        '    Else
        '        Comm.CommandText += " order by tblformasdepago.idforma,tblventas.idventa"
        '    End If
        'End If
        'End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventastp")
        DS.WriteXmlSchema("tblventastprn.xml")
        Return DS.Tables("tblventastp").DefaultView
    End Function
    Public Sub ActualizaComentario(ByVal pidRemision As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblventasremisiones set comentariof='" + Replace(pTexto, "'", "''") + "' where idremision=" + pidRemision.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ReporteUtilidad(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidinventario As Integer, ByVal pidclasificacion1 As Integer, ByVal pidclasificacion2 As Integer, ByVal pidclasificacion3 As Integer, ByVal ptipoCosteo As Byte, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal pMostrarOc As Byte, ByVal pSeriesOC As String, pIdAlmacen As Integer, pidCaja As Integer, pIdTipo As Integer, pPorClasificacion As Boolean, pIdTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandTimeout = 3000
        If ptipoCosteo = 1 Then
            'If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idremision as idventa,tblventas.folio,tblventas.serie,tblventasinventario.cantidad,if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio) as precio,tblventasinventario.iva,tblclientes.clave,tblclientes.nombre,tblinventario.clave as clavei,tblinventario.nombre as nombrei,tblventas.fecha," + _
    "ifnull((select costo from tblinventariocostoh where idinventario=tblventasinventario.idinventario and fecha<=tblventas.fecha order by fecha desc limit 1),0) as costo,tblventas.porsurtir,tblventasinventario.ieps,tblventasinventario.ivaretenido ivaret,tblventas.idsucursal,tblsucursales.nombre snombre,tblinventario.idclasificacion,tblinventarioclasificaciones.nombre as clasnombre " + _
    "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor inner join tblsucursales on tblventas.idsucursal=tblsucursales.idsucursal  inner join tblinventarioclasificaciones on tblinventario.idclasificacion=tblinventarioclasificaciones.idclasificacion where  tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblventas.idremision as idventa,tblventas.folio,tblventas.serie,tblventasinventario.cantidad,if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio) as precio,tblventasinventario.iva,tblclientes.clave,tblclientes.nombre,tblinventario.clave as clavei,tblinventario.nombre as nombrei,tblventas.fecha," + _
    "spdaultimocostoinv(tblventasinventario.idinventario) as costo,tblventas.porsurtir,tblventasinventario.ieps,tblventasinventario.ivaretenido ivaret,tblventas.idsucursal,tblsucursales.nombre snombre,tblinventario.idclasificacion,tblinventarioclasificaciones.nombre as clasnombre " + _
    "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor inner join tblsucursales on tblventas.idsucursal=tblsucursales.idsucursal  inner join tblinventarioclasificaciones on tblinventario.idclasificacion=tblinventarioclasificaciones.idclasificacion where  tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdTipoSucursal > 0 Then
            Comm.CommandText += " and tblsucursales.idtipo=" + pIdTipoSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and tblclientes.zona='" + pZona2.ToString() + "' or tblclientes.zona2='" + pZona2.ToString() + "'"
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
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSeriesOC, "-", " and tblventas.serie")
        End If
        If pidCaja > 0 Then
            Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and tblclientes.idtipo=" + pIdTipo.ToString
        End If
        If pPorClasificacion Then
            Comm.CommandText += " order by tblinventarioclasificaciones.nombre"
        Else
            Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventascosto")
        'DS.WriteXmlSchema("tblventascosto.xml")
        Return DS.Tables("tblventascosto").DefaultView
        'select fecha,tblclientes.creditodias,if(pfecha<=date_format(adddate(fecha,tblclientes.creditodias-7),'%Y/%m/%d'),),tblventas.credito from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente
    End Function
    Public Function EstaLigada(ByVal pIdremision As Integer) As Integer
        Comm.CommandText = "select ifnull((select idventar from tblventasremisiones where idremision=" + pIdremision.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function TieneSalidas(ByVal pIdRemision As Integer) As Integer
        Comm.CommandText = "select ifnull((select count(idremision) from tblmovimientos where idremision=" + pIdRemision.ToString + " and estado=3),0)"
        Return Comm.ExecuteScalar
    End Function

    Public Function TotalAmortizacion(ByVal pIdVenta As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(precio*(1+tblventasremisionesinventario.iva/100)) from tblventasremisionesinventario inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario inner join tblventasremisiones on tblventasremisiones.idremision=tblventasremisionesinventario.idremision where tblinventario.esamortizacion=1 and tblventasremisiones.idremision=" + pIdVenta.ToString + "),0)"
        '"select ifnull((select sum(precio*(1+tblventasinventario.iva/100)) from tblventasinventario inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblventas on tblventas.idventa=tblventasinventario.idventa where tblinventario.esamortizacion=1 and tblventas.estado=3 and tblventas.idventa=" + pIdVenta.ToString + "),0)"
        '"+ifnull((select sum(precio*(1+tblventasremisionesinventario.iva/100)) from tblventasremisionesinventario inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario inner join tblventasremisiones on tblventasremisiones.idremision=tblventasremisionesinventario.idremision where tblinventario.esamortizacion=1 and tblventasremisiones.estado=3 and tblventasremisiones.idcliente=" + pIdcliente.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaTotalCantidad(ByVal pIdVenta As Integer) As Double
        Comm.CommandText = "select ifnull((select count(tblventasremisionesinventario.idremision) from tblventasremisionesinventario inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario where idremision=" + pIdVenta.ToString + " and inventariable=1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaTotalCantidadxArticulo(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(tblventasremisionesinventario.cantidad) from tblventasremisionesinventario inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario where idremision=" + pIdVenta.ToString + " and inventariable=1 and tblventasremisionesinventario.idinventario=" + pIdinventario.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaTotalAbonado(ByVal pIdVenta As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(tblventaspagosremisiones.cantidad) from tblventaspagosremisiones where tblventaspagosremisiones.idremision=" + pIdVenta.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    'Public Function VerificaExistencias(ByVal pId As Integer) As String
    '    Dim Str As String = ""
    '    Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim IDs As New Collection
    '    Dim Cont As Integer = 1
    '    Comm.CommandText = "select iddetalle from tblventasinventario where idventa=" + pId.ToString
    '    DReader = Comm.ExecuteReader
    '    While DReader.Read()
    '        IDs.Add(DReader("idventasinventario"))
    '    End While
    '    DReader.Close()
    '    Dim I As New dbInventario(MySqlcon)
    '    Dim P As New dbProductos(MySqlcon)
    '    Dim iIdInventario As Integer
    '    Dim iIdVariante As Integer
    '    Dim iCantidad As Double
    '    Dim iIdAlmacen As Integer
    '    Dim iCantidad2 As Double
    '    Dim EsInventariable As Integer
    '    Dim iContenido As Double
    '    While Cont <= IDs.Count
    '        Comm.CommandText = "select idinventario,idvariante,cantidad,idalmacen,if(idinventario>1,(select inventariable from tblinventario where tblinventario.idinventario=tblventasremisionesinventario.idinventario),0) as esinventariable,if(idvariante>1,(select inventariable from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblventasremisionesinventario.idvariante),0) as esinventariablep,(select contenido from tblinventario where tblinventario.idinventario=tblventasremisionesinventario.idinventario) as contenido from tblventasremisionesinventario where iddetalle=" + IDs(Cont).ToString
    '        DReader = Comm.ExecuteReader
    '        If DReader.Read() Then
    '            iIdInventario = DReader("idinventario")
    '            iCantidad = DReader("cantidad")
    '            iIdVariante = DReader("idvariante")
    '            iIdAlmacen = DReader("idalmacen")
    '            EsInventariable = DReader("esinventariable")
    '            iContenido = DReader("contenido")
    '            DReader.Close()
    '            If iIdInventario > 1 And EsInventariable = 1 Then
    '                iCantidad2 = I.DaInventario(iIdAlmacen, iIdInventario)
    '                If iContenido <> 1 Then
    '                    iCantidad2 = iCantidad2 * iContenido
    '                End If
    '                If iCantidad > iCantidad2 Then
    '                    Str = " Hay artículos con insuficiente inventario."
    '                End If
    '            End If
    '            'If iIdVariante > 1 Then
    '            '    Comm.CommandText = "select spverificaexistenciarecetas(" + iIdVariante.ToString + "," + iIdAlmacen.ToString + "," + iCantidad.ToString + ")"
    '            '    Str = Comm.ExecuteScalar
    '            'End If
    '        Else
    '            DReader.Close()
    '        End If
    '        Cont += 1
    '    End While
    '    Return Str
    'End Function
    Public Function DaMoneda(ByVal pIdRemision As Integer) As Integer
        Comm.CommandText = "select ifnull((select idmoneda from tblventasremisionesinventario where idremision=" + pIdRemision.ToString + " limit 1),2)"
        Return Comm.ExecuteScalar
    End Function

    Public Function ReporteViejosSaldosH(ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pFecha As String, ByVal pTipodeCambio As Double, pIdTipo As Integer, pIdZonaCliente As Integer) As DataView
        Dim DS As New DataSet
        Dim F As String

        F = pFecha
        Comm.CommandText = "select ifnull((select tipodecambio from tblventas where fecha<='" + F + "' and idconversion<>2 order by fecha desc limit 1),1)"
        pTipodeCambio = Comm.ExecuteScalar
        If IdCliente > 0 Then
            Comm.CommandText = "delete from tblclientesviejossaldos where idcliente=" + IdCliente.ToString
        Else
            Comm.CommandText = "delete from tblclientesviejossaldos"
        End If
        Comm.ExecuteNonQuery()

        Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        "ifnull((select sum(if(tblventaspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblventaspagosremisiones as tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idremision=tblventas.idremision),0) as credito," + _
        "0,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
        ",totalapagar from tblventasremisiones as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=3  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda=2"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        If pIdZonaCliente > 0 Then
            Comm.CommandText += " and tblclientes.zona=" + pIdZonaCliente.ToString
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and tblclientes.idtipo=" + pIdTipo.ToString
        End If
        'If pidMoneda > 0 Then
        'Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        'End If
        Comm.ExecuteNonQuery()
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagosremisiones as tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idremision=tblventas.idremision),0) as credito," + _
            "0," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",totalapagar from tblventasremisiones as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=3  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "ifnull((select sum(if(tblventaspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblventaspagosremisiones as tblventaspagos where idcliente=tblventas.idcliente and tblventaspagos.estado=3 and tblventaspagos.fecha<='" + pFecha + "' and tblventaspagos.idremision=tblventas.idremision),0) as credito," + _
            "0,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tblventasremsiones as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=3  and tblventas.fecha<='" + pFecha + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        End If

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pIdZonaCliente > 0 Then
            Comm.CommandText += " and tblclientes.zona=" + pIdZonaCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and tblclientes.idtipo=" + pIdTipo.ToString
        End If
        Comm.ExecuteNonQuery()

        If IdCliente <= 0 Then
            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant,tblclientesviejossaldos.tipodecambio,tblclientesviejossaldos.corriente from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente where round(totalapagar-tblclientesviejossaldos.credito,2)>0  order by tblclientes.nombre,fecha,serie,folio"
        Else
            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant,tblclientesviejossaldos.tipodecambio,tblclientesviejossaldos.corriente from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente where tblclientesviejossaldos.idcliente=" + IdCliente.ToString + " and round(totalapagar-tblclientesviejossaldos.credito,2)>0 order by tblclientes.nombre,fecha,serie,folio"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesviejo")
        'DS.WriteXmlSchema("tblcomprasviejo.xml")
        Return DS.Tables("tblclientesviejo").DefaultView
    End Function
    Public Function ReporteViejosSaldos(ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pTipodeCambio As Double, ByVal pZona As Integer, pIdTipo As Integer, pIdZonaCliente As Integer) As DataView
        Dim DS As New DataSet
        Dim F As String
        F = Format(Date.Now, "yyyy/MM/dd")
        'F = pFecha2
        'Comm.CommandText = "select ifnull((select tipodecambio from tblcompras where fecha<='" + F + "' and idmoneda=2 order by fecha desc limit 1),1)"
        'pTipodeCambio = Comm.ExecuteScalar

        If pIdCliente > 0 Then
            Comm.CommandText = "delete from tblclientesviejossaldos where idcliente=" + pIdCliente.ToString
        Else
            Comm.CommandText = "delete from tblclientesviejossaldos"
        End If
        Comm.ExecuteNonQuery()

        Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,idsucursal) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        "tblventas.credito as credito," + _
        "0,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
        ",totalapagar,tblventas.idsucursal from tblventasremisiones as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo=3 and tblventas.estado=3 and tblventas.idmoneda=2 and round(tblventas.totalapagar-tblventas.credito,2)>0"

        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pIdZonaCliente > 0 Then
            Comm.CommandText += " and tblclientes.zona=" + pIdZonaCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()

        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,idsucursal) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
            "tblventas.credito as credito," + _
            "0," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",totalapagar,tblventas.idsucursal from tblventasremisiones as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo=3 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        Else
            Comm.CommandText = "insert into tblclientesviejossaldos(fecha,idcliente,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,idsucursal) select tblventas.fecha,tblclientes.idcliente,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblclientes.creditodias+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "tblventas.credito as credito," + _
            "0,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblclientes.creditodias),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar,tblventas.idsucursal from tblventasremisiones as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblventas.idforma=tblformasdepago.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor where tblformasdepago.tipo=3 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        End If

        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pIdZonaCliente > 0 Then
            Comm.CommandText += " and tblclientes.zona=" + pIdZonaCliente.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        If pIdCliente <= 0 Then
            If pIdTipo < 0 Then
                Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,tblclientesviejossaldos.serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant,s.nombre as tipodecambio,tblclientesviejossaldos.corriente from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente inner join tblsucursales as s on tblclientesviejossaldos.idsucursal=s.idsucursal order by tblclientes.nombre,fecha,serie,folio"
            Else
                Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,tblclientesviejossaldos.serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant,s.nombre as tipodecambio,tblclientesviejossaldos.corriente from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente inner join tblsucursales as s on tblclientesviejossaldos.idsucursal=s.idsucursal where tblclientes.idtipo=" + pIdTipo.ToString + " order by tblclientes.nombre,fecha,serie,folio"
            End If
        Else
            Comm.CommandText = "select fecha,tblclientes.clave,tblclientes.nombre,tblclientes.idcliente,tblclientesviejossaldos.serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblclientesviejossaldos.credito,0 as saldoant,tblclientesviejossaldos.tipodecambio,tblclientesviejossaldos.corriente from tblclientesviejossaldos inner join tblclientes on tblclientesviejossaldos.idcliente=tblclientes.idcliente inner join tblsucursales as s on tblclientesviejossaldos.idsucursal=s.idsucursal where tblclientesviejossaldos.idcliente=" + pIdCliente.ToString + " order by tblclientes.nombre,fecha,serie,folio"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasviejo")
        'DS.WriteXmlSchema("tblventasviejo.xml")
        Return DS.Tables("tblventasviejo").DefaultView
    End Function
    
    Public Function ReporteVentastipoB(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pConcendaso As Boolean, ByVal pSerie As String, ByVal pOrdenxVendedor As Boolean, ByVal pZona As Integer, ByVal pSoloCanceladas As Boolean, ByVal pMostrarOC As Byte, ByVal pSerieOC As String, pidAlmacen As Integer, pIdCaja As Integer, pidTipo As Integer, pIdTipoSucursal As Integer, pFormadePAgo As Byte) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idremision as idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,s.nombre as tipodecambio,tblventas.idmoneda as idconversion,tblventasinventario.cantidad,if(tblventasinventario.idinventario>1,tblinventario.nombre,if(tblventasinventario.idvariante>1,tblproductos.nombre,'SERVICIO')) as descripcion,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio),2) as precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,0 as isr,0 as ivaretenido,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio/tblventasinventario.cantidad,tblventasinventario.precio*tblventas.tipodecambio/tblventasinventario.cantidad),2) as preciou,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventas.porsurtir,tblformasdepago.tipo as tipoforma,tblventasinventario.ieps,tblventasinventario.ivaretenido ivar " + _
            "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on tblventas.idsucursal=s.idsucursal where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventasinventario.cantidad<>0"
        Else
            Comm.CommandText = "select tblventas.idremision as idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,s.nombre as tipodecambio,tblventas.idmoneda as idconversion,tblventasinventario.cantidad,if(tblventasinventario.idinventario>1,tblinventario.nombre,if(tblventasinventario.idvariante>1,tblproductos.nombre,'SERVICIO')) as descripcion,round(tblventasinventario.precio,2),0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,0 as isr,0 as ivaretenido,round(tblventasinventario.precio/tblventasinventario.cantidad,2) as preciou,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventas.porsurtir,tblformasdepago.tipo as tipoforma,tblventasinventario.ieps,tblventasinventario.ivaretenido ivar  " + _
            "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on tblventas.idsucursal=s.idsucursal where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventasinventario.cantidad<>0 "
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdTipoSucursal > 0 Then
            Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventas.idvendedor=" + pidVendedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventasinventario.idmoneda=" + pidMoneda.ToString
        End If
        If pSoloCanceladas Then
            Comm.CommandText += " and tblventas.estado=4"
        Else
            Comm.CommandText += " and (tblventas.estado=3 or tblventas.estado=4)"
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
        If pFormadePAgo = 1 Then Comm.CommandText += " and tblformasdepago.tipo=3"
        If pFormadePAgo = 2 Then Comm.CommandText += " and tblformasdepago.tipo<>3"
        If pMostrarOC = 1 Then
            'Comm.CommandText += " and tblventas.serie<>'" + Replace(pSerieOC, "'", "''") + "'"
            Comm.CommandText += Replace(pSerieOC, "-", " and tblventas.serie")
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and tblventas.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pidAlmacen > 0 Then
            Comm.CommandText += " and tblventasinventario.idalmacen=" + pidAlmacen.ToString
        End If
        If pIdCaja > 0 Then
            Comm.CommandText += " and tblventas.idcaja=" + pIdCaja.ToString
        End If
        'agregar el filtro del tipo de cliente
        If pidTipo > 0 Then
            Comm.CommandText += " and tblclientes.idtipo=" + pidTipo.ToString
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

    Public Sub GuardarDescuento(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer, ByVal pIdServicio As Integer)

        'Dim CTemp As Double
        'Dim PTemp As Double
        Dim pPrecioOriginal As Double
        ' Dim auxID As Integer
        'Idinventario = pIdinventario
        'Cantidad = pCantidad
        'Precio = pPrecio
        'IdMoneda = pIdMoneda
        'IdRemision = pIdVenta
        'Descripcion = pDescripcion
        'IdAlmacen = pIdAlmacen
        'Iva = pIva
        'Descuento = pDescuento
        'IdVariante = pIdVariante
        'IdServicio = pIdServicio
        If pCantidad = 1 Then
            pPrecioOriginal = pPrecio
        Else
            pPrecioOriginal = pPrecio / pCantidad
        End If
        'Costo = pCosto
        'Dim IdTemp As Integer
        'If pIdinventario <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblventasremisionesinventario where idremision=" + IdRemision.ToString + " and idinventario=" + pIdinventario.ToString + " limit 1),0)"
        '    IdTemp = Comm.ExecuteScalar
        'Else
        '    pIdinventario = 1
        'End If
        'If pIdVariante <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblventasremisionesinventario where idremision=" + IdRemision.ToString + " and idvariante=" + pIdVariante.ToString + " limit 1),0)"
        '    IdTemp = Comm.ExecuteScalar
        'Else
        '    pIdVariante = 1
        'End If
        'If pIdServicio <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblventasremisionesinventario where idremision=" + IdRemision.ToString + " and idservicio=" + pIdServicio.ToString + " limit 1),0)"
        '    IdTemp = Comm.ExecuteScalar
        'End If
        'IdTemp = 0
        'If IdTemp <> 0 Then
        '    Comm.CommandText = "select cantidad from tblventasremisionesinventario where iddetalle=" + IdTemp.ToString
        '    CTemp = Comm.ExecuteScalar
        '    Comm.CommandText = "select precio from tblventasremisionesinventario where iddetalle=" + IdTemp.ToString
        '    PTemp = Comm.ExecuteScalar
        '    If PTemp <> 0 Then
        '        pPrecio = PTemp / CTemp
        '    Else
        '        pPrecio = 0
        '    End If
        '    pCantidad += CTemp
        '    pPrecio = pPrecio * pCantidad
        '    Comm.CommandText = "update tblventasremisionesinventario set cantidad=" + pCantidad.ToString + ",precio=" + pPrecio.ToString + " where iddetalle=" + IdTemp.ToString
        '    Comm.ExecuteNonQuery()
        '    auxID = ID
        '    ID = IdTemp
        '    LlenaDatos()
        '    ID = auxID
        '    NuevoConcepto = False
        'Else
        '    NuevoConcepto = True
        '    If Idinventario <> 1 Then
        '        Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal) values(" + IdRemision.ToString + "," + pIdinventario.ToString + "," + pCantidad.ToString + "," + pPrecio.ToString + ",'" + Replace(pDescripcion, "'", "''") + "'," + pIdMoneda.ToString + "," + pIdAlmacen.ToString + "," + pIva.ToString + ",''," + pDescuento.ToString + "," + pIdVariante.ToString + "," + pIdServicio.ToString + ",0," + pPrecio.ToString + ");"
        '        'Comm.ExecuteNonQuery()

        '        Comm.CommandText += "select ifnull((select max(iddetalle) from tblventasremisionesinventario),0)"
        '        auxID = Comm.ExecuteScalar()
        '    Else
        Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal) values(" + pIdVenta.ToString + "," + pIdinventario.ToString + "," + pCantidad.ToString + "," + pPrecio.ToString + ",'" + Replace(pDescripcion, "'", "''") + "'," + pIdMoneda.ToString + "," + pIdAlmacen.ToString + "," + pIva.ToString + ",''," + pDescuento.ToString + "," + pIdVariante.ToString + "," + pIdServicio.ToString + ",0," + pPrecioOriginal.ToString + ");"
        ' Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal) values(" + IdRemision.ToString + "," + pIdinventario.ToString + "," + pCantidad.ToString + "," + pPrecio.ToString + ",'" + Replace(pDescripcion, "'", "''") + "'," + pIdMoneda.ToString + "," + pIdAlmacen.ToString + "," + pIva.ToString + "," + Descuento.ToString + "," + pIdVariante.ToString + "," + pIdServicio.ToString + ",0," + pPrecio.ToString + ");"
        Comm.ExecuteNonQuery()

        'End If

        'End If


    End Sub

    Public Sub AgregarCantidadDescuento(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTiporedondeo As Byte, ByVal pCantidadDecimales As Byte, ByVal pPrecioTot As Double)
        Dim PrecioTemp As Double
        Dim IvaTemp As Double
        Dim auxPrecio As Double
        Dim auxCantidad As Double
        'ID = pID
        'PrecioTemp = PrecioOriginal / Cantidad * (Cantidad + pCantidad)
        'Precio = Precio / Cantidad * (Cantidad + pCantidad)

        Comm.CommandText = "select precio from tblventasremisionesinventario where iddetalle=" + pID.ToString
        auxPrecio = Comm.ExecuteScalar

        Comm.CommandText = "select cantidad from tblventasremisionesinventario where iddetalle=" + pID.ToString
        auxCantidad = Comm.ExecuteScalar


        auxPrecio = auxPrecio / auxCantidad * (auxCantidad + pCantidad)
        auxCantidad = auxCantidad + pCantidad
        Comm.CommandText = "select iva from tblventasremisionesinventario where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        PrecioTemp = auxPrecio * (1 + (IvaTemp / 100))
        'If pTiporedondeo <> 0 Then
        '    PrecioTemp = GlobalRedondea(PrecioTemp * Cantidad, pTiporedondeo, pCantidadDecimales)
        '    Precio = PrecioTemp / (1 + (IvaTemp / 100))
        'End If

        If auxCantidad > 0 Then
            Comm.CommandText = "update tblventasremisionesinventario set precio=" + auxPrecio.ToString + ",cantidad=" + auxCantidad.ToString + " where iddetalle=" + pID.ToString
            Comm.ExecuteNonQuery()
        Else
            Eliminar(pID)
            'ID = 0
        End If
    End Sub

    Public Sub AsignaCantidadDescuento(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte)
        Dim IvaTemp As Double
        Dim PrecioTemp As Double
        Dim auxPrecio As Double
        Dim auxCantidad As Double
        Dim pPrecioOriginal As Double
        Comm.CommandText = "select precio from tblventasremisionesinventario where iddetalle=" + pID.ToString
        auxPrecio = Comm.ExecuteScalar

        Comm.CommandText = "select cantidad from tblventasremisionesinventario where iddetalle=" + pID.ToString
        auxCantidad = Comm.ExecuteScalar

        auxPrecio = auxPrecio / auxCantidad
        pPrecioOriginal = auxPrecio
        auxCantidad = pCantidad
        auxPrecio = auxPrecio * auxCantidad
        'Precio = Precio / Cantidad
        'PrecioOriginal = PrecioOriginal / Cantidad
        'Cantidad = pCantidad
        'Precio = Precio * Cantidad
        PrecioTemp = pPrecioOriginal * pCantidad
        Comm.CommandText = "select iva from tblventasremisionesinventario where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        PrecioTemp = PrecioTemp * (1 + (IvaTemp / 100))
        If pTipoRedondeo <> 0 Then
            PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
            auxPrecio = PrecioTemp / (1 + (IvaTemp / 100))
        End If

        If pCantidad > 0 Then
            Comm.CommandText = "update tblventasremisionesinventario set precio=" + auxPrecio.ToString + ",cantidad=" + auxCantidad.ToString + " where iddetalle=" + pID.ToString
            Comm.ExecuteNonQuery()
        Else
            Eliminar(pID)
            ' ID = 0
        End If
    End Sub
    Public Function ReporteVentasArticulosDetalle(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pConcendaso As Boolean, ByVal pSerie As String, ByVal pOrdenxVendedor As Boolean, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal PsoloCanceladas As Boolean, ByVal pSoloActivas As Boolean, pidAlmacen As Integer, pidCaja As Integer, pIdTipo As Integer, pidTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.idremision as idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,s.nombre as tipodecambio,tblventas.idmoneda as idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio,tblventasinventario.precio*tblventas.tipodecambio),2) as precio,tblventasinventario.idinventario,tblclientes.nombre as cnombre,tblventasinventario.iva,0 as isr,0 as ivaretenido,round(if(tblventasinventario.idmoneda=2,tblventasinventario.precio/tblventasinventario.cantidad,tblventasinventario.precio*tblventas.tipodecambio/tblventasinventario.cantidad),2) as preciou,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventas.porsurtir,tblformasdepago.tipo as tipoforma,tblventasinventario.ieps,tblventasinventario.ivaretenido ivar,tblventas.idcliente,tblventas.credito,ifnull(tblzona.idzona,0) as idzona,ifnull(tblzona.zona,'SIN ZONA') as zona,tblventas.idsucursal,s.nombre snombre " + _
            "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on tblventas.idsucursal=s.idsucursal left outer join tblzona on tblclientes.zona=tblzona.idzona where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventasinventario.cantidad<>0 and tblventasinventario.idinventario>1 "
        Else
            Comm.CommandText = "select tblventas.idremision as idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,s.nombre as tipodecambio,tblventas.idmoneda as idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,round(tblventasinventario.precio,2) precio,tblventasinventario.idinventario,tblclientes.nombre as cnombre,tblventasinventario.iva,0 as isr,0 as ivaretenido,round(tblventasinventario.precio/tblventasinventario.cantidad,2) as preciou,tblinventario.clave,(select nombre from tblvendedores where idvendedor=tblventas.idvendedor) as vnombre,tblventas.idvendedor,tblventas.porsurtir,tblformasdepago.tipo as tipoforma,tblventasinventario.ieps,tblventasinventario.ivaretenido ivar,tblventas.idcliente,tblventas.credito,ifnull(tblzona.idzona,0) as idzona,ifnull(tblzona.zona,'SIN ZONA') as zona,tblventas.idsucursal,s.nombre snombre " + _
            "from tblventasremisiones as tblventas inner join tblventasremisionesinventario as tblventasinventario on tblventas.idremision=tblventasinventario.idremision inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones as tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblvendedores on tblventas.idvendedor=tblvendedores.idvendedor  inner join tblsucursales as s on tblventas.idsucursal=s.idsucursal left outer join tblzona on tblclientes.zona=tblzona.idzona where tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblventasinventario.cantidad<>0 and tblventasinventario.idinventario>1 "
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pidTipoSucursal > 0 Then
            Comm.CommandText += " and s.idtipo=" + pidTipoSucursal.ToString
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

        If pidCaja > 0 Then
            Comm.CommandText += " and tblventas.idcaja=" + pidCaja.ToString
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and tblclientes.idtipo=" + pIdTipo.ToString
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

    Public Function ReporteVentasArticulosPeso(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pUsado As Boolean, ByVal pCondensado As Boolean, ByVal pSerieOc As String, ByVal pMostrarOc As Byte, ByVal pSerie As String, ByVal pPorVendedor As Boolean, ByVal pZona As Integer, ByVal pZona2 As Integer, pIdAlmacen As Integer, pidCaja As Integer, ByVal idTipo As Integer, pIdTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idremision idventa,v.folio,v.serie,v.estado,v.total,v.totalapagar,f.tipo as formadepago,v.fecha,s.nombre as tipodecambio,vi.idmoneda,vi.cantidad,if(vi.idinventario>1,i.nombre,if(vi.idvariante>1,p.nombre,'SERVICIO')) as descripcion,round(if(vi.idmoneda=2,vi.precio,vi.precio*v.tipodecambio),2) as precio,0 as costoinv,0 as costopro,vi.idinventario,vi.idvariante,c.nombre as cnombre,vi.iva,v.usado,round(if(vi.idmoneda=2,vi.precio/vi.cantidad,vi.precio*v.tipodecambio/vi.cantidad),2) as preciou,i.clave as codigoa,v.idvendedor,(select nombre from tblvendedores where idvendedor=v.idvendedor) as vnombre,v.porsurtir,vi.ieps,vi.ivaretenido ivaret, i.peso as peso " + _
            "from tblventasremisiones v inner join tblventasremisionesinventario vi on v.idremision=vi.idremision inner join tblinventario i on vi.idinventario=i.idinventario inner join tblproductosvariantes pv on pv.idvariante=vi.idvariante inner join tblclientes c on v.idcliente=c.idcliente inner join tblproductos p on pv.idproducto=p.idproducto inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on v.idsucursal=s.idsucursal inner join tblformasdepagoremisiones as f on v.idforma=f.idforma where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idremision idventa,v.folio,v.serie,v.estado,v.total,v.totalapagar,f.tipo as formadepago,v.fecha,s.nombre as tipodecambio,vi.idmoneda,vi.cantidad,if(vi.idinventario>1,i.nombre,if(vi.idvariante>1,p.nombre,'SERVICIO')) as descripcion,round(vi.precio,2),0 as costoinv,0 as costopro,vi.idinventario,vi.idvariante,c.nombre as cnombre,vi.iva,v.usado,round(vi.precio/vi.cantidad,2) as preciou,i.clave as codigoa,v.idvendedor,(select nombre from tblvendedores where idvendedor=v.idvendedor) as vnombre,v.porsurtir,vi.ieps,vi.ivaretenido ivaret " + _
            "from tblventasremisiones v inner join tblventasremisionesinventario vi on v.idremision=vi.idremision inner join tblinventario i on vi.idinventario=i.idinventario inner join tblproductosvariantes pv on pv.idvariante=vi.idvariante inner join tblclientes c on v.idcliente=c.idcliente inner join tblproductos p on pv.idproducto=p.idproducto inner join tblvendedores on v.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on v.idsucursal=s.idsucursal inner join tblformasdepagoremisiones as f on v.idforma=f.idforma where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pidVendedor > 0 Then Comm.CommandText += " and v.idvendedor=" + pidVendedor.ToString
        If pidMoneda > 0 Then Comm.CommandText += " and vi.idmoneda=" + pidMoneda.ToString
        If idTipo > 0 Then Comm.CommandText += " and c.idtipo=" + idTipo.ToString()
        If pUsado Then Comm.CommandText += " and v.usado=0"
        If pIdInventario > 1 Then
            Comm.CommandText += " and vi.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then Comm.CommandText += " and i.idclasificacion=" + pidClasificacion.ToString
            If pidClasificacion2 > 0 Then Comm.CommandText += " and i.idclasificacion2=" + pidClasificacion2.ToString
            If pidClasificacion3 > 0 Then Comm.CommandText += " and i.idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pMostrarOc = 1 Then
            Comm.CommandText += Replace(pSerieOc, "-", " and v.serie")
            'Comm.CommandText += " and v.serie<>'" + Replace(pSerieOc, "'", "''") + "'"
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and c.zona='" + pZona2.ToString() + "' or c.zona2='" + pZona2.ToString() + "'"
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and vi.idalmacen=" + pIdAlmacen.ToString
        End If
        If pidCaja > 0 Then
            Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
        End If
        If pCondensado = False Then
            Comm.CommandText += " order by v.fecha,v.serie,v.folio"
        Else
            If pPorVendedor = False Then
                Comm.CommandText += " order by vi.idinventario,preciou"
            Else
                Comm.CommandText += " order by v.idvendedor,vi.idinventario"
            End If
        End If
        If idTipo > 0 Then
            Comm.CommandText += " and c.idtipo=" + idTipo.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventasra.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Function reporteComparativo(ByVal desde As String, ByVal hasta As String, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, ByVal pSoloActivas As Boolean, ByVal facturadas As Boolean, pIdSucursal As Integer, pIdCliente As Integer, pTipoSucursal As Integer, pIdVendedor As Integer, pFormadepago As Byte) As DataView
        Dim ds As New DataSet
        Dim dt As New DataTable
        dt.TableName = "ventas"
        dt.Columns.Add("mes", GetType(String))
        dt.Columns.Add("importe", GetType(Double))
        dt.Columns.Add("iva", GetType(Double))
        dt.Columns.Add("total", GetType(Double))
        dt.Columns.Add("formadepago", GetType(Integer))
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.iva,tblventas.fecha,if(tblventas.idmoneda=2,round(tblventas.total,2),round(tblventas.total*tblventas.tipodecambio,2)) as precio,if(tblventas.idmoneda=2,round(tblventas.totalapagar,2),round(tblventas.totalapagar*tblventas.tipodecambio,2)) as total,0 as formadepago " + _
            "from tblventasremisiones as tblventas inner join tblsucursales s on tblventas.idsucursal=s.idsucursal inner join tblformasdepagoremisiones tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblventas.fecha>='" + desde + "' and tblventas.fecha<='" + hasta + "'"
        Else
            Comm.CommandText = "select tblventas.iva,tblventas.fecha,round(tblventas.total,2) as precio,round(tblventas.totalapagar,2) as total,0 as formadepago " + _
            "from tblventasremisiones as tblventas inner join tblsucursales s on tblventas.idsucursal=s.idsucursal inner join tblformasdepagoremisiones tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblventas.fecha>='" + desde + "' and tblventas.fecha<='" + hasta + "'"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        If pTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pTipoSucursal.ToString
        If pIdVendedor > 0 Then Comm.CommandText += " and tblventas.idvendedor=" + pIdVendedor.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        'If pSoloActivas Then
        Comm.CommandText += " and tblventas.estado=3"

        If pFormadepago = 1 Then Comm.CommandText += " and tblformasdepago.tipo=3"
        If pFormadepago = 2 Then Comm.CommandText += " and tblformasdepago.tipo<>3"
        'Else
        'If pSoloCanceladas Then
        ' Comm.CommandText += " and tblventas.estado=4"
        'Else
        'Comm.CommandText += " and (tblventas.estado=3 or tblventas.estado=4)"
        'End If
        'End If
        If facturadas Then
            Comm.CommandText += " and tblventas.usado=0"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"

        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio,tblventas.idremision"
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader = Comm.ExecuteReader
        While dr.Read()
            Dim f As String = dr("fecha")
            Dim m() As String = f.Split("/")
            Dim r As DataRow = dt.NewRow
            r("mes") = m(1)
            r("importe") = dr("precio")
            r("iva") = dr("iva")
            r("total") = dr("total")
            r("formadepago") = dr("formadepago")
            dt.Rows.Add(r)
        End While
        dr.Close()
        'Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        'da.Fill(ds, "ventas")
        ds.Tables.Add(dt)
        'ds.WriteXmlSchema("tblventascomparativo.xml")
        Return ds.Tables("ventas").DefaultView
    End Function

    Public Function comparativoMes(ByVal anhoActual As String, ByVal anhoAnterior As String, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, ByVal pSoloActivas As Boolean, ByVal facturadas As Boolean, pFormadepago As Byte) As DataView

        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventas.iva,tblventas.fecha,if(tblventas.idmoneda=2,round(tblventas.total,2),round(tblventas.total*tblventas.tipodecambio,2)) as precio,if(tblventas.idmoneda=2,round(tblventas.totalapagar,2),round(tblventas.totalapagar*tblventas.tipodecambio,2)) as total,tblformasdepago.tipo as formadepago " + _
            "from tblventasremisiones as tblventas inner join tblformasdepagoremisiones tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblformasdepago.tipo<>2 and tblventas.fecha>='" + anhoAnterior + "/01/01' and tblventas.fecha<='" + anhoActual + "/12/31'"
        Else
            Comm.CommandText = "select tblventas.iva,tblventas.fecha,round(tblventas.total,2) as precio,round(tblventas.totalapagar,2) as total,tblformasdepago.tipo as formadepago " + _
            "from tblventasremisionas as tblventas inner join tblformasdepagoremisiones tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblformasdepago.tipo<>2 and tblventas.fecha>='" + anhoAnterior + "/01/31' and tblventas.fecha<='" + anhoActual + "/12/31'"
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
        If facturadas Then
            Comm.CommandText += " and tblventas.usado=0"
        End If
        If pFormadepago = 1 Then Comm.CommandText += " and tblformasdepago.tipo=3"
        If pFormadepago = 2 Then Comm.CommandText += " and tblformasdepago.tipo<>3"
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"

        Comm.CommandText += " order by tblventas.fecha,tblventas.serie,tblventas.folio,tblventas.idremision"
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader = Comm.ExecuteReader
        Dim dt As New DataTable
        dt.TableName = "ventas"
        dt.Columns.Add("mes", GetType(String))
        dt.Columns.Add("importe", GetType(Double))
        dt.Columns.Add("iva", GetType(Double))
        dt.Columns.Add("total", GetType(Double))
        dt.Columns.Add("formadepago", GetType(Integer))
        dt.Columns.Add("anho", GetType(Integer))
        While dr.Read()
            Dim f As String = dr("fecha")
            Dim m() As String = f.Split("/")
            Dim r As DataRow = dt.NewRow
            'Dim aux As String = m(1)
            r("mes") = m(1)
            r("importe") = dr("precio")
            r("iva") = dr("iva")
            r("total") = dr("total")
            r("formadepago") = dr("formadepago")
            r("anho") = CInt(m(0))
            dt.Rows.Add(r)
        End While
        dr.Close()
        'dt.WriteXmlSchema("comparativoAño.xml")
        Return dt.DefaultView
    End Function
    Public Function RevisaEstado(ByVal pIdRemision As Integer) As Boolean
        Dim res As Integer
        Comm.CommandText = "select ifnull((select estado from tblventasremisiones where idremision=" + pIdRemision.ToString + "),0)"
        res = Comm.ExecuteScalar
        If res = 3 Or res = 4 Or res = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ReporteIEPSRemisiones(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pidVendedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidinventario As Integer, ByVal pidclasificacion1 As Integer, ByVal pidclasificacion2 As Integer, ByVal pidclasificacion3 As Integer, ByVal pSoloCanceladas As Boolean, ByVal pSerie As String, ByVal pZona As Integer, ByVal pZona2 As Integer, ByVal pSoloActivas As Boolean, pIdAlmacen As Integer, pIdTipo As Integer, pOrdenPorCliente As Boolean, pIdTipoSucursal As Integer, pConIL As Boolean) As DataView
        Dim DS As New DataSet
        Dim strIL As String = ""
        Dim strCl As String = "tblclientes.nombre"
        If pConIL Then
            strIL = ",0 as imploct," +
                     "0 as implocr"
            strCl = "tblclientes.clave"
        End If
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblventasremisiones.idremision as idventa,tblventasremisiones.folio,tblventasremisiones.serie,tblventasremisiones.estado,if(tblventasremisiones.idmoneda=2,round(tblventasremisiones.total,2),round(tblventasremisiones.total*tblventasremisiones.tipodecambio,2)) as total,if(tblventasremisiones.idmoneda=2,round(tblventasremisiones.totalapagar,2),round(tblventasremisiones.totalapagar*tblventasremisiones.tipodecambio,2)) as totalapagar,tblventasremisiones.fecha,s.nombre as nsucursal,tblventasremisiones.idmoneda,round(tblventasremisionesinventario.cantidad,2) as cantidad,tblventasremisionesinventario.descripcion,round(tblventasremisionesinventario.precio,2) as precio,0 as costoinv,0 as costopro,tblventasremisionesinventario.idinventario,tblformasdepagoremisiones.tipo as formadepago," + strCl + " as cnombre,0 as isr,0 as ivaretenido,tblventasremisionesinventario.ieps,tblventasremisionesinventario.iva ivai,tblventasremisionesinventario.ivaretenido retcon,tblventasremisiones.idsucursal,tblventasremisiones.idcliente" + strIL +
            " from tblventasremisiones inner join tblventasremisionesinventario on tblventasremisiones.idremision=tblventasremisionesinventario.idremision inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones on tblformasdepagoremisiones.idforma=tblventasremisiones.idforma inner join tblvendedores on tblventasremisiones.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on tblventasremisiones.idsucursal=s.idsucursal  where tblventasremisiones.fecha>='" + pFecha1 + "' and tblventasremisiones.fecha<='" + pFecha2 + "' and tblventasremisionesinventario.ieps>0"
        Else
            Comm.CommandText = "select tblventasremisiones.idremision,tblventasremisiones.folio,tblventasremisiones.serie,tblventasremisiones.estado,round(tblventasremisiones.total,2) as total,round(tblventasremisiones.totalapagar,2) as totalapagar,tblventasremisiones.fecha,s.nombre as nsucursal,tblventasremisiones.idmoneda,round(tblventasremisionesinventario.cantidad,2),tblventasremisionesinventario.descripcion,round(tblventasremisionesinventario.precio,2),0 as costoinv,0 as costopro,tblventasremisionesinventario.idinventario,tblformasdepagoremisiones.tipo as formadepago," + strCl + " as cnombre,0 as isr,0 as ivaretenido,tblventasremisionesinventario.ieps,tblventasremisionesinventario.iva ivai,tblventasremisionesinventario.ivaretenido retcon,tblventasremisiones.idsucursal,tblventasremisiones.idcliente" + strIL +
            " from tblventasremisiones inner join tblventasremisionesinventario on tblventasremisiones.idremision=tblventasremisionesinventario.idremision inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones on tblformasdepagoremisiones.idforma=tblventasremisiones.idforma inner join tblvendedores on tblventasremisiones.idvendedor=tblvendedores.idvendedor inner join tblsucursales as s on tblventasremisiones.idsucursal=s.idsucursal  where tblventasremisiones.fecha>='" + pFecha1 + "' and tblventasremisiones.fecha<='" + pFecha2 + "' and tblventasremisionesinventario.ieps>0"
        End If
        If pSoloActivas Then
            Comm.CommandText += " and tblventasremisiones.estado=3"
        Else
            If pSoloCanceladas Then
                Comm.CommandText += " and tblventasremisiones.estado=4"
            Else
                Comm.CommandText += " and (tblventasremisiones.estado=3 or tblventasremisiones.estado=4)"
            End If
        End If

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventasremisiones.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdTipoSucursal > 0 Then
            Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventasremisiones.idcliente=" + pIdCliente.ToString
        End If
        If pidVendedor > 0 Then
            Comm.CommandText += " and tblventasremisiones.idvendedor=" + pidVendedor.ToString
        End If
        If pZona > 0 Then
            'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventasremisiones.idvendedor=t2.id)"
            Comm.CommandText += " and tblvendedores.zona='" + pZona.ToString() + "'"
        End If
        If pZona2 > 0 Then
            Comm.CommandText += " and (tblclientes.zona='" + pZona2.ToString + "' or tblclientes.zona2='" + pZona2.ToString() + "')"
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventasremisiones.idmoneda=" + pidMoneda.ToString
        End If
        If pidinventario > 1 Then
            Comm.CommandText += " and tblventasremisionesinventario.idinventario=" + pidinventario.ToString
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
            Comm.CommandText += " and tblventasremisiones.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tblventasremisionesinventario.idalmacen=" + pIdAlmacen.ToString
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and tblclientes.idtipo=" + pIdTipo.ToString
        End If
        If pOrdenPorCliente Then
            Comm.CommandText += " order by tblclientes.nombre,tblventasremisiones.fecha,tblventasremisiones.serie,tblventasremisiones.folio,tblventasremisiones.idremision"
        Else
            Comm.CommandText += " order by tblventasremisiones.fecha,tblventasremisiones.serie,tblventasremisiones.folio,tblventasremisiones.idremision"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventas.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
End Class
