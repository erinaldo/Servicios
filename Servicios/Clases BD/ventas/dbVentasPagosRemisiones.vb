Public Class dbVentasPagosRemisiones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Cantidad As Double
    Public Estado As Byte
    Public IdRemision As Integer
    Public Fecha As String
    Public Tipo As String
    'Public IdDocumento As Integer
    'Public TipoDocumento As Byte
    Public Hora As String
    Public FechaCancelado As String
    Public HoraCancelado As String
    'Public IdCargo As String
    Public IdCliente As Integer
    Public idMoneda As Integer
    Public pTipodeCambio As Double
    Public IdDevolucion As Integer
    'Public TipoDoci As Byte
    Public Idconceptonotaventa As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Cantidad = 0
        IdRemision = 0
        Fecha = ""
        Tipo = ""
        'IdDocumento = 0
        'TipoDocumento = 0
        Hora = ""
        FechaCancelado = ""
        HoraCancelado = ""
        'IdCargo = 0
        IdCliente = 0
        idMoneda = 0
        pTipodeCambio = 0
        'IdDocumentod = 0
        'TipoDoci = 0
        IdDevolucion = 0
        Idconceptonotaventa = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventaspagosremisiones where idpago=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Cantidad = DReader("cantidad")
            Estado = DReader("estado")
            IdRemision = DReader("idremision")
            Fecha = DReader("fecha")
            Tipo = DReader("tipo")
            'IdDocumento = DReader("iddocumento")
            'TipoDocumento = DReader("tipodocumento")
            Hora = DReader("hora")
            FechaCancelado = DReader("fechacancelado")
            HoraCancelado = DReader("horacancelado")
            'IdCargo = DReader("idcargo")
            IdCliente = DReader("idcliente")
            idMoneda = DReader("idmoneda")
            pTipodeCambio = DReader("ptipodecambio")
            'IdDocumentod = DReader("iddocumentod")
            'TipoDoci = DReader("tipodoci")
            IdDevolucion = DReader("iddevolucion")
            Idconceptonotaventa = DReader("idconceptonotaventa")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pIdRemision As Integer, ByVal pCantidad As Double, ByVal pFecha As String, ByVal pidCliente As Integer, ByVal ppTipodeCambio As Double, ByVal pIdMoneda As Integer, ByVal pIdConceptoNotaVenta As Integer, ByVal pTipo As String, ByVal pIdDevolucion As Integer)
        Cantidad = pCantidad
        IdRemision = pIdRemision
        Fecha = pFecha
        Tipo = pTipo
        'IdDocumento = pidDocumento
        'TipoDocumento = pTipoDocumento
        'IdCargo = pIdCargo
        IdCliente = pidCliente
        pTipodeCambio = ppTipodeCambio
        idMoneda = pIdMoneda
        'IdDocumentod = pIdDocumentod
        'TipoDoci = pTipoDoci
        IdDevolucion = pIdDevolucion
        Idconceptonotaventa = pIdConceptoNotaVenta
        Comm.CommandText = "insert into tblventaspagosremisiones(idremision,cantidad,estado,fecha,hora,fechacancelado,horacancelado,idcliente,idmoneda,ptipodecambio,idconceptonotaventa,tipo,iddevolucion,usado,idusuarioalta,fechaalta,horaalta,idusuariocambio,fechacambio,horacambio) values(" + IdRemision.ToString + "," + Cantidad.ToString + ",3,'" + Fecha + "','" + Format(TimeOfDay, "HH:mm:ss") + "','" + Format(Date.Now, "yyyy/MM/dd") + "','" + Format(TimeOfDay, "HH:mm:ss") + "'," + IdCliente.ToString + "," + idMoneda.ToString + "," + pTipodeCambio.ToString + "," + Idconceptonotaventa.ToString + ",'" + Replace(Tipo, "'", "''") + "'," + IdDevolucion.ToString + ",0," + GlobalIdUsuario.ToString + ",'" + Date.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString + ",'" + Date.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idpago) from tblventaspagosremisiones;"
        ID = Comm.ExecuteScalar
        AplicarSaldoaDocumento(IdRemision, Cantidad, 0, idMoneda, "+", "+", pTipodeCambio)
        'Comm.CommandText = "update tblclientes set saldo=saldo-" + pCantidad.ToString + " where idcliente=" + pidCliente.ToString
        'Comm.ExecuteNonQuery()
    End Sub
    Private Sub AplicarSaldoaDocumento(ByVal pIdRemision As Integer, ByVal pCantidad As Double, ByVal pCantidadAnt As Double, ByVal pidMoneda As Integer, ByVal pOperador1 As String, ByVal pOperador2 As String, ByVal ppTipodeCambio As Double)
        Dim docMoneda As Integer
        If pIdRemision <> 0 Then
            Comm.CommandText = "select idmoneda from tblventasremisiones where idremision=" + pIdRemision.ToString
            docMoneda = Comm.ExecuteScalar
            If docMoneda = pidMoneda Then
                Comm.CommandText = "update tblventasremisiones set credito=credito" + pOperador1 + pCantidad.ToString + pOperador2 + pCantidadAnt.ToString + " where idremision=" + pIdRemision.ToString
                Comm.ExecuteNonQuery()
            Else
                If docMoneda = 2 Then
                    Comm.CommandText = "update tblventasremisiones set credito=credito" + pOperador1 + CStr(pCantidad * ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt * ppTipodeCambio) + " where idremision=" + pIdRemision.ToString
                    Comm.ExecuteNonQuery()
                Else
                    Comm.CommandText = "update tblventasremisiones set credito=credito" + pOperador1 + CStr(pCantidad / ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt / ppTipodeCambio) + " where idremision=" + pIdRemision.ToString
                    Comm.ExecuteNonQuery()
                End If
            End If
        End If
        'If pidCargo <> 0 Then
        '    Comm.CommandText = "select idmoneda from tblnotasdecargo where idcargo=" + pidCargo.ToString
        '    docMoneda = Comm.ExecuteScalar
        '    If docMoneda = pidMoneda Then
        '        Comm.CommandText = "update tblnotasdecargo set aplicado=aplicado" + pOperador1 + pCantidad.ToString + pOperador2 + pCantidadAnt.ToString + " where idcargo=" + pidCargo.ToString
        '        Comm.ExecuteNonQuery()
        '    Else
        '        If docMoneda = 2 Then
        '            Comm.CommandText = "update tblnotasdecargo set aplicado=aplicado" + pOperador1 + CStr(pCantidad * ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt * ppTipodeCambio) + " where idcargo=" + pidCargo.ToString
        '            Comm.ExecuteNonQuery()
        '        Else
        '            Comm.CommandText = "update tblnotasdecargo set aplicado=aplicado" + pOperador1 + CStr(pCantidad / ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt / ppTipodeCambio) + " where idcargo=" + pidCargo.ToString
        '            Comm.ExecuteNonQuery()
        '        End If
        '    End If
        'End If
        'If pIdDocumentod <> 0 Then
        '    Comm.CommandText = "select idmoneda from tbldocumentosclientes where iddocumento=" + pIdDocumentod.ToString
        '    docMoneda = Comm.ExecuteScalar
        '    If docMoneda = pidMoneda Then
        '        Comm.CommandText = "update tbldocumentosclientes set credito=credito" + pOperador1 + pCantidad.ToString + pOperador2 + pCantidadAnt.ToString + " where iddocumento=" + pIdDocumentod.ToString
        '        Comm.ExecuteNonQuery()
        '    Else
        '        If docMoneda = 2 Then
        '            Comm.CommandText = "update tbldocumentosclientes set credito=credito" + pOperador1 + CStr(pCantidad * ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt * ppTipodeCambio) + " where iddocumento=" + pIdDocumentod.ToString
        '            Comm.ExecuteNonQuery()
        '        Else
        '            Comm.CommandText = "update tbldocumentosclientes set credito=credito" + pOperador1 + CStr(pCantidad / ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt / ppTipodeCambio) + " where iddocumento=" + pIdDocumentod.ToString
        '            Comm.ExecuteNonQuery()
        '        End If
        '    End If
        'End If
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pFecha As String, ByVal pIdCliente As Integer, ByVal pTipo As String)
        Dim pCantidadAnt As Double
        ID = pID
        LlenaDatos()
        pCantidadAnt = Cantidad
        Cantidad = pCantidad
        Fecha = pFecha
        Tipo = pTipo
        Comm.CommandText = "update tblventaspagosremisiones set cantidad=" + Cantidad.ToString + ",fecha='" + Fecha + "',tipo='" + Replace(Tipo, "'", "''") + "' where idpago=" + ID.ToString
        Comm.ExecuteNonQuery()
        AplicarSaldoaDocumento(IdRemision, Cantidad, pCantidadAnt, idMoneda, "+", "-", pTipodeCambio)
        'If IdVenta <> 0 Then
        '    Comm.CommandText = "update tblventas set credito=credito-" + pCantidadAnt.ToString + "+" + Cantidad.ToString + " where idventa=" + IdVenta.ToString
        '    Comm.ExecuteNonQuery()
        'Else
        '    Comm.CommandText = "update tblnotasdecargo set aplicado=aplicado-" + pCantidadAnt.ToString + "+" + Cantidad.ToString + " where idcargo=" + IdCargo.ToString
        '    Comm.ExecuteNonQuery()
        'End If
        'Comm.CommandText = "update tblclientes set saldo=saldo+" + pCantidadAnt.ToString + "-" + Cantidad.ToString + " where idcliente=" + pIdCliente.ToString
        'Comm.ExecuteNonQuery()
    End Sub
    Public Sub CancelarPago(ByVal pIdPago As Integer, ByVal pEstado As Integer, ByVal pidCliente As Integer)
        ID = pIdPago
        LlenaDatos()
        Estado = pEstado
        Comm.CommandText = "update tblventaspagosremisiones set estado=" + Estado.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "' where idpago=" + ID.ToString
        Comm.ExecuteNonQuery()
        AplicarSaldoaDocumento(IdRemision, Cantidad, 0, idMoneda, "-", "-", pTipodeCambio)
        Eliminar(ID)
        'If IdVenta <> 0 Then
        '    Comm.CommandText = "update tblventas set credito=credito-" + Cantidad.ToString + " where idventa=" + IdVenta.ToString
        '    Comm.ExecuteNonQuery()
        'Else
        '    Comm.CommandText = "update tblnotasdecargo set aplicado=aplicado-" + Cantidad.ToString + " where idcargo=" + IdCargo.ToString
        '    Comm.ExecuteNonQuery()
        'End If
        'Comm.CommandText = "update tblclientes set saldo=saldo+" + Cantidad.ToString + " where idcliente=" + pidCliente.ToString
        'Comm.ExecuteNonQuery()
    End Sub
    Public Sub CancelarPAgosporDevolucion(ByVal pIdDevolucion As Integer)
        Comm.CommandText = "select idpago from tblventaspagosremisiones where iddevolucion=" + pIdDevolucion.ToString
        ID = Comm.ExecuteScalar
        LlenaDatos()
        AplicarSaldoaDocumento(IdRemision, Cantidad, 0, idMoneda, "-", "-", pTipodeCambio)
        Eliminar(ID)
    End Sub
    'Public Sub CancelarPagosxDocumento(ByVal pidDocumento As Integer, ByVal pTipoDocumento As Byte, ByVal pIdCliente As Integer, ByVal pEstado As Byte)
    '    Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim IDs As New Collection
    '    Dim Cont As Integer = 1
    '    Comm.CommandText = "update tblventaspagosremisiones set estado=" + pEstado.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "' where iddocumento=" + pidDocumento.ToString + " and tipodocumento=" + pTipoDocumento.ToString
    '    Comm.ExecuteNonQuery()
    '    Comm.CommandText = "select idpago from tblventaspagos where iddocumento=" + pidDocumento.ToString + " and tipodocumento=" + pTipoDocumento.ToString
    '    DReader = Comm.ExecuteReader
    '    While DReader.Read
    '        IDs.Add(DReader("idpago"))
    '    End While
    '    DReader.Close()
    '    While Cont <= IDs.Count
    '        ID = IDs.Item(Cont)
    '        LlenaDatos()
    '        AplicarSaldoaDocumento(IdVenta, IdCargo, Cantidad, 0, idMoneda, "-", "-", pTipodeCambio, IdDocumentod)
    '        'Comm.CommandText = "update tblventas set credito=credito-" + Cantidad.ToString + " where idventa=(select idventa from tblventaspagos where idpago=" + IDs.Item(Cont).ToString + ")"
    '        'Comm.ExecuteNonQuery()
    '        'Comm.CommandText = "update tblnotasdecargo set aplicado=aplicado-" + Cantidad.ToString + " where idcargo=(select idcargo from tblventaspagos where idpago=" + IDs.Item(Cont).ToString + ")"
    '        'Comm.ExecuteNonQuery()
    '        'Comm.CommandText = "update tblclientes set saldo=saldo+" + Cantidad.ToString + " where idcliente=" + pIdCliente.ToString
    '        'Comm.ExecuteNonQuery()
    '        Cont += 1
    '    End While
    '    Comm.CommandText = "delete from tblventaspagos where iddocumento=" + pidDocumento.ToString + " and tipodocumento=" + pTipoDocumento.ToString
    '    Comm.ExecuteNonQuery()
    'End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        ID = pID
        ' LlenaDatos()
        Comm.CommandText = "delete from tblventaspagosremisiones where idpago=" + pID.ToString
        Comm.ExecuteNonQuery()
        'AplicarSaldoaDocumento(IdVenta, IdCargo, Cantidad, 0, idMoneda, "-", "-", pTipodeCambio, IdDocumentod)
        'If IdVenta <> 0 Then
        '    Comm.CommandText = "update tblventas set credito=credito-" + Cantidad.ToString + " where idventa=" + IdVenta.ToString
        '    Comm.ExecuteNonQuery()
        'Else
        '    Comm.CommandText = "update tblnotasdecargo set aplicado=aplicado-" + Cantidad.ToString + " where idcargo=" + IdCargo.ToString
        '    Comm.ExecuteNonQuery()
        'End If
        'Comm.CommandText = "update tblclientes set saldo=saldo+" + Cantidad.ToString + " where idcliente=" + pIdCliente.ToString
        'Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdremision As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select  idpago,fecha,cantidad,tblventaspagosremisiones.tipo,tblconceptosnotasventas.nombre,if(tblventaspagosremisiones.usado<>0,ifnull((select concat(v.serie,' ',convert(v.folio using utf8)) from tblventas v where v.idventa=tblventaspagosremisiones.usado),''),'') Fac from tblventaspagosremisiones inner join tblconceptosnotasventas on tblventaspagosremisiones.idconceptonotaventa=tblconceptosnotasventas.idconceptonotaventa where estado=3 and idremision=" + pIdremision.ToString
        'If pidCargo <> 0 Then Comm.CommandText = "select  idpago,fecha,cantidad,tblventaspagos.tipo,iddocumento,tipodocumento,tblconceptosnotasventas.nombre from tblventaspagos inner join tblconceptosnotasventas on tblventaspagos.idconceptonotaventa=tblconceptosnotasventas.idconceptonotaventa where estado=3 and idcargo=" + pidCargo.ToString
        'If pidDocumento <> 0 Then Comm.CommandText = "select  idpago,fecha,cantidad,tblventaspagos.tipo,iddocumento,tipodocumento,tblconceptosnotasventas.nombre from tblventaspagos inner join tblconceptosnotasventas on tblventaspagos.idconceptonotaventa=tblconceptosnotasventas.idconceptonotaventa where estado=3 and iddocumentod=" + pidDocumento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspagosr")
        Return DS.Tables("tblventaspagosr").DefaultView
    End Function
    Public Function ConsultaParaFacturar(pFecha1 As String, pFecha2 As String, pIdCliente As Integer, pRFC As String, pFacturados As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select  tvp.idpago,tvp.fecha,concat(tblventasremisiones.serie,' ',convert(tblventasremisiones.folio using utf8)) folio,tblclientes.rfc,tblclientes.nombre ncliente,if(tvp.idmoneda=2,tvp.cantidad,tvp.cantidad*tvp.ptipodecambio) importe,tvp.idremision from tblventaspagosremisiones tvp inner join tblconceptosnotasventas on tvp.idconceptonotaventa=tblconceptosnotasventas.idconceptonotaventa inner join tblventasremisiones on tvp.idremision=tblventasremisiones.idremision inner join tblclientes on tvp.idcliente=tblclientes.idcliente where tvp.iddevolucion=0 and tvp.fecha>='" + pFecha1 + "' and tvp.fecha<='" + pFecha2 + "' "
        If pidcliente > 0 Then
            Comm.CommandText += " and tvp.idcliente=" + pIdCliente.ToString
        End If
        If pRFC <> "" Then
            Comm.CommandText += " and tblclientes.rfc like '%" + Replace(pRFC, "'", "''") + "%'"
        End If
        If pFacturados > 0 Then
            Comm.CommandText += " and tvp.usado<>0"
        Else
            Comm.CommandText += " and tvp.usado=0"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspagos")
        Return DS.Tables("tblventaspagos").DefaultView
    End Function
    Public Sub Usar(ByVal pID() As Integer, ByVal pIdventa As Integer)
        Dim Wherefields As String = ""
        For Each IDf As Integer In pID
            Wherefields += " or idpago=" + IDf.ToString
        Next
        Comm.CommandText = "update tblventaspagosremisiones set usado=" + pIdventa.ToString + " where false " + Wherefields
        Comm.ExecuteNonQuery()

    End Sub
    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdCliente As Integer, ByVal pIdMoneda As Integer, ByVal pMostrasEnPesos As Byte, ByVal pIdConcepto As Integer, ByVal pIdSucursal As Integer, ByVal pConContado As Boolean, ByVal pZona As Integer, pIdTipo As Integer, pIdTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        'If pMostrasEnPesos = 0 Then
        '    Comm.CommandText = "select idpago,tblventaspagos.fecha,if(tblventaspagos.idmoneda=2,tblventaspagos.cantidad,tblventaspagos.cantidad*tblventaspagos.ptipodecambio) as cantidad,tblventaspagos.tipo," + _
        '    "if(tblventaspagos.idventa<>0,(select serie from tblventas where idventa=tblventaspagos.idventa limit 1),(select serie from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1)) as serie," + _
        '    "if(tblventaspagos.idventa<>0,(select folio from tblventas where idventa=tblventaspagos.idventa limit 1),(select folio from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1)) as folio," + _
        '    "tblclientes.nombre," + _
        '    "if(tblventaspagos.idventa<>0,(select if(idconversion=2,credito,credito*tipodecambio) from tblventas where idventa=tblventaspagos.idventa limit 1),(select if(idmoneda=2,aplicado,aplicado*tipodecambio) from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1)) as credito," + _
        '    "if(tblventaspagos.idventa<>0,(select if(idconversion=2,totalapagar,totalapagar*tipodecambio) from tblventas where idventa=tblventaspagos.idventa limit 1),(select if(idmoneda=2,totalapagar,totalapagar*tipodecambio) from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1)) as totalapagar," + _
        '    "if(tblventaspagos.idventa<>0,idventa,idcargo) as idventa," + _
        '    "if(tblventaspagos.idventa<>0,(select fecha from tblventas where idventa=tblventaspagos.idventa limit 1),(select fecha from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1)) as fechaf" + _
        '    " from tblventaspagos inner join tblclientes on tblventaspagos.idcliente=tblclientes.idcliente where tblventaspagos.estado=3 and tblventaspagos.fecha>='" + pFecha1 + "' and tblventaspagos.fecha<='" + pFecha2 + "' "
        'Else
        '    Comm.CommandText = "select idpago,tblventaspagos.fecha,tblventaspagos.cantidad,tblventaspagos.tipo," + _
        '    "if(tblventaspagos.idventa<>0,(select serie from tblventas where idventa=tblventaspagos.idventa limit 1),(select serie from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1)) as serie," + _
        '    "if(tblventaspagos.idventa<>0,(select folio from tblventas where idventa=tblventaspagos.idventa limit 1),(select folio from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1)) as folio," + _
        '    "tblclientes.nombre," + _
        '    "if(tblventaspagos.idventa<>0,(select credito from tblventas where idventa=tblventaspagos.idventa limit 1),(select aplicado from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1)) as credito," + _
        '    "if(tblventaspagos.idventa<>0,(select totalapagar from tblventas where idventa=tblventaspagos.idventa limit 1),(select totalapagar from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1)) as totalapagar," + _
        '    "if(tblventaspagos.idventa<>0,idventa,idcargo) as idventa," + _
        '    "if(tblventaspagos.idventa<>0,(select fecha from tblventas where idventa=tblventaspagos.idventa limit 1),(select fecha from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1)) as fechaf" + _
        '    " from tblventaspagos inner join tblclientes on tblventaspagos.idcliente=tblclientes.idcliente where tblventaspagos.estado=3 and tblventaspagos.fecha>='" + pFecha1 + "' and tblventaspagos.fecha<='" + pFecha2 + "' "
        'End If
        Comm.CommandText = "delete from tblreppagos"
        Comm.ExecuteNonQuery()
        If pMostrasEnPesos = 0 Then
            Comm.CommandText = "insert into tblreppagos select idpago,vp.fecha,if(vp.idmoneda=2,round(vp.cantidad,2),round(vp.cantidad*vp.ptipodecambio,2)) as cantidad,vp.tipo," + _
            "(select serie from tblventasremisiones where idremision=vp.idremision limit 1) " + _
            " as serie," + _
            "(select folio from tblventasremisiones where idremision=vp.idremision limit 1)" + _
            " as folio," + _
            "tblclientes.nombre," + _
            "(select if(idmoneda=2,round(credito,2),round(credito*tipodecambio,2)) from tblventasremisiones where idremision=vp.idremision limit 1)" + _
            " as credito," + _
            "(select if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2)) from tblventasremisiones where idremision=vp.idremision limit 1)" + _
            " as totalapagar," + _
            "vp.idremision as idventa," + _
            "(select fecha from tblventasremisiones where idremision=vp.idremision limit 1)" + _
            " as fechaf," + _
            "'REM' as tipodocd," + _
            "tblconceptosnotasventas.idconceptonotaventa,tblconceptosnotasventas.nombre," + _
            "(select idsucursal from tblventasremisiones where idremision=vp.idremision limit 1) " + _
            " as idsucursal,0,if(vp.usado<>0,ifnull((select concat(v.serie,' ',convert(folio using utf8)) from tblventas v where v.idventa=vp.usado),''),'')" + _
            " from tblventaspagosremisiones as vp inner join tblclientes on vp.idcliente=tblclientes.idcliente inner join tblconceptosnotasventas on vp.idconceptonotaventa=tblconceptosnotasventas.idconceptonotaventa where vp.estado=3 and vp.fecha>='" + pFecha1 + "' and vp.fecha<='" + pFecha2 + "' "
        Else
            Comm.CommandText = "insert into tblreppagos select idpago,vp.fecha,round(vp.cantidad,2) as cantidad,vp.tipo," + _
            "(select serie from tblventasremisiones where idremision=vp.idremision limit 1) " + _
            " as serie," + _
            "(select folio from tblventasremisiones where idremision=vp.idremision limit 1)" + _
            " as folio," + _
            "tblclientes.nombre," + _
            "(select round(credito,2) from tblventasremisiones where idremision=vp.idremision limit 1)" + _
            " as credito," + _
            "(select round(totalapagar,2) from tblventasremisiones where idremision=vp.idremision limit 1)" + _
            " as totalapagar," + _
            "vp.idremision as idventa," + _
            "(select fecha from tblventasremisiones where idremision=vp.idremision limit 1)" + _
            " as fechaf," + _
            "'REMISIÓN' as tipodocd," + _
            "tblconceptosnotasventas.idconceptonotaventa,tblconceptosnotasventas.nombre," + _
            "(select idsucursal from tblventasremisiones where idremision=vp.idremision limit 1) " + _
            " as idsucursal,0,if(vp.usado<>0,ifnull((select concat(v.serie,' ',convert(folio using utf8)) from tblventas v where v.idventa=vp.usado),''),'')" + _
            " from tblventaspagosremisiones as vp inner join tblclientes on vp.idcliente=tblclientes.idcliente inner join tblconceptosnotasventas on vp.idconceptonotaventa=tblconceptosnotasventas.idconceptonotaventa where vp.estado=3 and vp.fecha>='" + pFecha1 + "' and vp.fecha<='" + pFecha2 + "' "
        End If

        If pIdCliente <> 0 Then
            Comm.CommandText += " and vp.idcliente=" + pIdCliente.ToString
        End If
        If pZona <> 0 Then
            Comm.CommandText += " and tblclientes.zona='" + pZona.ToString + "'"
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and tblclientes.idtipo=" + pIdTipo.ToString
        End If
        If pIdMoneda > 0 Then
            Comm.CommandText += " and vp.idmoneda=" + pIdMoneda.ToString
        End If
        If pIdConcepto > 0 Then
            Comm.CommandText += " and vp.idconceptonotaventa=" + pIdConcepto.ToString
        End If
        Comm.ExecuteNonQuery()

        If pConContado Then
            If pMostrasEnPesos = 0 Then
                Comm.CommandText = "insert into tblreppagos(idpago,fecha,cantidad,tipo,serie,folio,nombre,credito,totalapagar,idventa,fechaf,tipocd,idconceptonotaventa,nombre1,idsucursal) select idremision,fecha,if(idmoneda=2,totalapagar,totalapagar*tipodecambio),'Remisión Contado',serie,folio,tblclientes.nombre,if(idmoneda=2,tblventas.credito,tblventas.credito*tblventas.tipodecambio),if(idmoneda=2,totalapagar,totalapagar*tblventas.tipodecambio),idremision,fecha,'F.CONTADO',90,(select nombre from tblconceptosnotasventas where idconceptonotaventa=90),idsucursal from tblventasremisiones as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones on tblventas.idforma=tblformasdepagoremisiones.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and (tblformasdepagoremisiones.tipo=1 or tblformasdepagoremisiones.tipo=2)"
            Else
                Comm.CommandText = "insert into tblreppagos(idpago,fecha,cantidad,tipo,serie,folio,nombre,credito,totalapagar,idventa,fechaf,tipocd,idconceptonotaventa,nombre1,idsucursal) select idremision,fecha,totalapagar,'Remisión Contado',serie,folio,tblclientes.nombre,tblventas.credito,totalapagar,idremision,fecha,'R.CONTADO',90,(select nombre from tblconceptosnotasventas where idconceptonotaventa=90),idsucursal from tblventasremisiones as tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepagoremisiones on tblventas.idforma=tblformasdepagoremisiones.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and (tblformasdepagoremisiones.tipo=1 or tblformasdepagoremisiones.tipo=2)"
            End If
            If pIdCliente <> 0 Then
                Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
            End If
            If pIdMoneda > 0 Then
                Comm.CommandText += " and tblventas.idmoneda=" + pIdMoneda.ToString
            End If
            If pZona > 0 Then
                Comm.CommandText += " and tblclientes.zona='" + pZona.ToString + "'"
            End If
            If pIdTipo <> 0 Then
                Comm.CommandText += " and tblclientes.idtipo=" + pIdTipo.ToString
            End If
            'If pIdConcepto > 0 Then
            '    Comm.CommandText += " and tblventaspagos.idconceptonotaventa=" + pIdConcepto.ToString
            'End If
            Comm.ExecuteNonQuery()
        End If

        'Comm.CommandText = "select * from tblreppagos "
        'Comm.CommandText += " order by idconceptonotaventa,fecha,nombre,serie,folio"
        Comm.CommandText = "select *,s.nombre as sucursal from tblreppagos  inner join tblsucursales as s on tblreppagos.idsucursal=s.idsucursal"
        If pIdTipoSucursal > 0 Then
            Comm.CommandText += " where s.idtipo=" + pIdTipoSucursal.ToString()
        Else
            If pIdSucursal > 0 Then
                Comm.CommandText += " where tblreppagos.idsucursal=" + pIdSucursal.ToString()
            End If
        End If
        

        Comm.CommandText += " order by idconceptonotaventa,tblreppagos.fecha,tblreppagos.nombre,tblreppagos.serie,tblreppagos.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspagos")
        'DS.WriteXmlSchema("tblventaspagosr.xml")
        Return DS.Tables("tblventaspagos").DefaultView
    End Function
    'Public Sub Automatico(ByVal pidCliente As Integer, ByVal pOrden As Byte)
    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Comm.CommandType = CommandType.StoredProcedure
    '    Comm.CommandText = "select tblventas.idventa,tblventas.credito,tblventas.totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=0 and tblventas.credito<tblventas.totalapagar"
    '    If pOrden = 0 Then
    '        'Orden por Fecha
    '        Comm.CommandText += " order by tblventas.fecha"
    '    Else
    '        'Orden por Cantidad
    '        Comm.CommandText += " order by tblventas.totalapagar"
    '    End If
    '    DR = Comm.ExecuteReader
    'End Sub
    Public Function HayPagosVentas(ByVal pidVenta As Integer) As Integer
        Comm.CommandText = "select count(idremision) from tblventaspagosremisiones where idremision=" + pidVenta.ToString + " and estado=3"
        Return Comm.ExecuteScalar
    End Function
    'Public Function HayPagosCargos(ByVal pidNota As Integer) As Integer
    '    Comm.CommandText = "select count(idcargo) from tblventaspagos where idcargo=" + pidNota.ToString + " and estado=3"
    '    Return Comm.ExecuteScalar
    'End Function
    'Public Function HayPagosDocumentos(ByVal pidDocumento As Integer) As Integer
    '    Comm.CommandText = "select count(iddocumentod) from tblventaspagos where iddocumentod=" + pidDocumento.ToString + " and estado=3"
    '    Return Comm.ExecuteScalar
    'End Function
    Public Function impresion(ByVal wherestr As String) As DataSet
        Dim ds As New DataSet
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        Comm.CommandText = "select vp.cantidad,c.nombre,c.direccion,c.ciudad,c.rfc,vp.fecha,vp.tipo,c.colonia,c.estado,ifnull((select zona from tblzona where idzona=c.zona),'') as czona,(select folio from tblventasremisiones where idremision=vp.idremision) folio,(select credito from tblventasremisiones where idremision=vp.idremision) credito,(select totalapagar from tblventasremisiones where idremision=vp.idremision) total,(select serie from tblventasremisiones where idremision=vp.idremision) serie,0 as tipodoci,cnv.nombre concepto from tblventaspagosremisiones vp inner join tblclientes c on vp.idcliente=c.idcliente inner join tblconceptosnotasventas cnv on vp.idconceptonotaventa=cnv.idconceptonotaventa where false" + wherestr
        da.Fill(ds, "pago")
        'ds.WriteXmlSchema("pago.xml")
        Return ds
    End Function
End Class
