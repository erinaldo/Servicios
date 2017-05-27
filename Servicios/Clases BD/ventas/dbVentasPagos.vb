Public Class dbVentasPagos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Cantidad As Double
    Public Estado As Byte
    Public IdVenta As Integer
    Public Fecha As String
    Public Tipo As String
    Public IdDocumento As Integer
    Public TipoDocumento As Byte
    Public Hora As String
    Public FechaCancelado As String
    Public HoraCancelado As String
    Public IdCargo As String
    Public IdCliente As Integer
    Public idMoneda As Integer
    Public pTipodeCambio As Double
    Public IdDocumentod As Integer
    Public TipoDoci As Byte
    Public Idconceptonotaventa As Integer
    Public IDDeposito As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Cantidad = 0
        IdVenta = 0
        Fecha = ""
        Tipo = ""
        IdDocumento = 0
        TipoDocumento = 0
        Hora = ""
        FechaCancelado = ""
        HoraCancelado = ""
        IdCargo = 0
        IdCliente = 0
        idMoneda = 0
        pTipodeCambio = 0
        IdDocumentod = 0
        TipoDoci = 0
        Idconceptonotaventa = 0
        IDDeposito = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventaspagos where idpago=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Cantidad = DReader("cantidad")
            Estado = DReader("estado")
            IdVenta = DReader("idventa")
            Fecha = DReader("fecha")
            Tipo = DReader("tipo")
            IdDocumento = DReader("iddocumento")
            TipoDocumento = DReader("tipodocumento")
            Hora = DReader("hora")
            FechaCancelado = DReader("fechacancelado")
            HoraCancelado = DReader("horacancelado")
            IdCargo = DReader("idcargo")
            IdCliente = DReader("idcliente")
            idMoneda = DReader("idmoneda")
            pTipodeCambio = DReader("ptipodecambio")
            IdDocumentod = DReader("iddocumentod")
            TipoDoci = DReader("tipodoci")
            Idconceptonotaventa = DReader("idconceptonotaventa")
            IDDeposito = DReader("iddeposito")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pIdVenta As Integer, ByVal pCantidad As Double, ByVal pFecha As String, ByVal pTipo As String, ByVal pidCliente As Integer, ByVal pidDocumento As Integer, ByVal pTipoDocumento As Byte, ByVal pIdCargo As Integer, ByVal ppTipodeCambio As Double, ByVal pIdMoneda As Integer, ByVal pIdDocumentod As Integer, ByVal pTipoDoci As Byte, ByVal pIdConceptoNotaVenta As Integer)
        Cantidad = pCantidad
        IdVenta = pIdVenta
        Fecha = pFecha
        Tipo = pTipo
        IdDocumento = pidDocumento
        TipoDocumento = pTipoDocumento
        IdCargo = pIdCargo
        IdCliente = pidCliente
        pTipodeCambio = ppTipodeCambio
        idMoneda = pIdMoneda
        IdDocumentod = pIdDocumentod
        TipoDoci = pTipoDoci
        Idconceptonotaventa = pIdConceptoNotaVenta
        Comm.CommandTimeout = 200
        Comm.Transaction = Comm.Connection.BeginTransaction
        Comm.CommandText = "insert into tblventaspagos(idventa,cantidad,estado,fecha,tipo,iddocumento,tipodocumento,hora,fechacancelado,horacancelado,idcargo,idcliente,idmoneda,ptipodecambio,iddocumentod,tipodoci,idconceptonotaventa,iddeposito,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + IdVenta.ToString + "," + Cantidad.ToString + ",3,'" + Fecha + "','" + Replace(Tipo, "'", "''") + "'," + IdDocumento.ToString + "," + TipoDocumento.ToString + ",'" + Format(TimeOfDay, "HH:mm:ss") + "','" + Format(Date.Now, "yyyy/MM/dd") + "','" + Format(TimeOfDay, "HH:mm:ss") + "'," + IdCargo.ToString + "," + IdCliente.ToString + "," + idMoneda.ToString + "," + pTipodeCambio.ToString + "," + IdDocumentod.ToString + "," + TipoDoci.ToString + "," + Idconceptonotaventa.ToString + ",0," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'); select max(idpago) from tblventaspagos;"
        'Comm.ExecuteNonQuery()
        'Comm.CommandText = ""
        ID = Comm.ExecuteScalar
        AplicarSaldoaDocumento(IdVenta, IdCargo, Cantidad, 0, idMoneda, "+", "+", pTipodeCambio, IdDocumentod)
        Comm.Transaction.Commit()
        'Comm.CommandText = "update tblclientes set saldo=saldo-" + pCantidad.ToString + " where idcliente=" + pidCliente.ToString
        'Comm.ExecuteNonQuery()
    End Sub
    Public Function LigadoABancos(ByVal pIdPago As Integer) As Boolean
        Comm.CommandText = "select iddeposito from tblventaspagos where idpago=" + pIdPago.ToString
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Sub AplicarSaldoaDocumento(ByVal pIdVenta As Integer, ByVal pidCargo As Integer, ByVal pCantidad As Double, ByVal pCantidadAnt As Double, ByVal pidMoneda As Integer, ByVal pOperador1 As String, ByVal pOperador2 As String, ByVal ppTipodeCambio As Double, ByVal pIdDocumentod As Integer)
        Dim docMoneda As Integer
        'Comm.CommandTimeout = 200
        If pIdVenta <> 0 Then
            Comm.CommandText = "select idconversion from tblventas where idventa=" + pIdVenta.ToString
            docMoneda = Comm.ExecuteScalar
            If docMoneda = pidMoneda Then
                Comm.CommandText = "update tblventas set credito=credito" + pOperador1 + pCantidad.ToString + pOperador2 + pCantidadAnt.ToString + " where idventa=" + pIdVenta.ToString
                Comm.ExecuteNonQuery()
            Else
                If docMoneda = 2 Then
                    Comm.CommandText = "update tblventas set credito=credito" + pOperador1 + CStr(pCantidad * ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt * ppTipodeCambio) + " where idventa=" + pIdVenta.ToString
                    Comm.ExecuteNonQuery()
                Else
                    Comm.CommandText = "update tblventas set credito=credito" + pOperador1 + CStr(pCantidad / ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt / ppTipodeCambio) + " where idventa=" + pIdVenta.ToString
                    Comm.ExecuteNonQuery()
                End If
            End If
        End If
        If pidCargo <> 0 Then
            Comm.CommandText = "select idmoneda from tblnotasdecargo where idcargo=" + pidCargo.ToString
            docMoneda = Comm.ExecuteScalar
            If docMoneda = pidMoneda Then
                Comm.CommandText = "update tblnotasdecargo set aplicado=aplicado" + pOperador1 + pCantidad.ToString + pOperador2 + pCantidadAnt.ToString + " where idcargo=" + pidCargo.ToString
                Comm.ExecuteNonQuery()
            Else
                If docMoneda = 2 Then
                    Comm.CommandText = "update tblnotasdecargo set aplicado=aplicado" + pOperador1 + CStr(pCantidad * ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt * ppTipodeCambio) + " where idcargo=" + pidCargo.ToString
                    Comm.ExecuteNonQuery()
                Else
                    Comm.CommandText = "update tblnotasdecargo set aplicado=aplicado" + pOperador1 + CStr(pCantidad / ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt / ppTipodeCambio) + " where idcargo=" + pidCargo.ToString
                    Comm.ExecuteNonQuery()
                End If
            End If
        End If
        If pIdDocumentod <> 0 Then
            Comm.CommandText = "select idmoneda from tbldocumentosclientes where iddocumento=" + pIdDocumentod.ToString
            docMoneda = Comm.ExecuteScalar
            If docMoneda = pidMoneda Then
                Comm.CommandText = "update tbldocumentosclientes set credito=credito" + pOperador1 + pCantidad.ToString + pOperador2 + pCantidadAnt.ToString + " where iddocumento=" + pIdDocumentod.ToString
                Comm.ExecuteNonQuery()
            Else
                If docMoneda = 2 Then
                    Comm.CommandText = "update tbldocumentosclientes set credito=credito" + pOperador1 + CStr(pCantidad * ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt * ppTipodeCambio) + " where iddocumento=" + pIdDocumentod.ToString
                    Comm.ExecuteNonQuery()
                Else
                    Comm.CommandText = "update tbldocumentosclientes set credito=credito" + pOperador1 + CStr(pCantidad / ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt / ppTipodeCambio) + " where iddocumento=" + pIdDocumentod.ToString
                    Comm.ExecuteNonQuery()
                End If
            End If
        End If
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pFecha As String, ByVal pTipo As String, ByVal pIdCliente As Integer)
        Dim pCantidadAnt As Double
        ID = pID
        LlenaDatos()
        pCantidadAnt = Cantidad
        Cantidad = pCantidad
        Fecha = pFecha
        Tipo = pTipo
        Try
            Comm.CommandTimeout = 200
            Comm.Transaction = Comm.Connection.BeginTransaction
            Comm.CommandText = "update tblventaspagos set cantidad=" + Cantidad.ToString + ",fecha='" + Fecha + "',tipo='" + Replace(Tipo, "'", "''") + "',idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idpago=" + ID.ToString
            Comm.ExecuteNonQuery()
            AplicarSaldoaDocumento(IdVenta, IdCargo, Cantidad, pCantidadAnt, idMoneda, "+", "-", pTipodeCambio, IdDocumentod)
            Comm.Transaction.Commit()
        Catch ex As Exception
            Comm.Transaction.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
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
        Comm.Transaction = Comm.Connection.BeginTransaction
        Comm.CommandText = "update tblventaspagos set estado=" + Estado.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "' where idpago=" + ID.ToString
        Comm.ExecuteNonQuery()
        AplicarSaldoaDocumento(IdVenta, IdCargo, Cantidad, 0, idMoneda, "-", "-", pTipodeCambio, IdDocumentod)
        Comm.Transaction.Commit()
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
    Public Sub CancelarPagosxDocumento(ByVal pidDocumento As Integer, ByVal pTipoDocumento As Byte, ByVal pIdCliente As Integer, ByVal pEstado As Byte)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Cont As Integer = 1
        Comm.CommandText = "update tblventaspagos set estado=" + pEstado.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "' where iddocumento=" + pidDocumento.ToString + " and tipodocumento=" + pTipoDocumento.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select idpago from tblventaspagos where iddocumento=" + pidDocumento.ToString + " and tipodocumento=" + pTipoDocumento.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("idpago"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            ID = IDs.Item(Cont)
            LlenaDatos()
            AplicarSaldoaDocumento(IdVenta, IdCargo, Cantidad, 0, idMoneda, "-", "-", pTipodeCambio, IdDocumentod)
            'Comm.CommandText = "update tblventas set credito=credito-" + Cantidad.ToString + " where idventa=(select idventa from tblventaspagos where idpago=" + IDs.Item(Cont).ToString + ")"
            'Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblnotasdecargo set aplicado=aplicado-" + Cantidad.ToString + " where idcargo=(select idcargo from tblventaspagos where idpago=" + IDs.Item(Cont).ToString + ")"
            'Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblclientes set saldo=saldo+" + Cantidad.ToString + " where idcliente=" + pIdCliente.ToString
            'Comm.ExecuteNonQuery()
            Cont += 1
        End While
        Comm.CommandText = "delete from tblventaspagos where iddocumento=" + pidDocumento.ToString + " and tipodocumento=" + pTipoDocumento.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        ID = pID
        ' LlenaDatos()
        Comm.CommandText = "delete from tblventaspagos where idpago=" + pID.ToString
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
    Public Function Consulta(ByVal pIdVenta As Integer, ByVal pidCargo As Integer, ByVal pidDocumento As Integer) As DataView
        Dim DS As New DataSet
        If pIdVenta <> 0 Then Comm.CommandText = "select  idpago,fecha,cantidad,tblventaspagos.tipo,iddocumento,tipodocumento,tblconceptosnotasventas.nombre,case tblventaspagos.iddeposito when 0 then '' else 'SI' end as Ligado from tblventaspagos inner join tblconceptosnotasventas on tblventaspagos.idconceptonotaventa=tblconceptosnotasventas.idconceptonotaventa where estado=3 and idventa=" + pIdVenta.ToString
        If pidCargo <> 0 Then Comm.CommandText = "select  idpago,fecha,cantidad,tblventaspagos.tipo,iddocumento,tipodocumento,tblconceptosnotasventas.nombre,case tblventaspagos.iddeposito when 0 then '' else 'SI' end as Ligado from tblventaspagos inner join tblconceptosnotasventas on tblventaspagos.idconceptonotaventa=tblconceptosnotasventas.idconceptonotaventa where estado=3 and idcargo=" + pidCargo.ToString
        If pidDocumento <> 0 Then Comm.CommandText = "select  idpago,fecha,cantidad,tblventaspagos.tipo,iddocumento,tipodocumento,tblconceptosnotasventas.nombre,case tblventaspagos.iddeposito when 0 then '' else 'SI' end as Ligado from tblventaspagos inner join tblconceptosnotasventas on tblventaspagos.idconceptonotaventa=tblconceptosnotasventas.idconceptonotaventa where estado=3 and iddocumentod=" + pidDocumento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspagos")
        Return DS.Tables("tblventaspagos").DefaultView
    End Function
    
    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdCliente As Integer, ByVal pIdMoneda As Integer, ByVal pMostrasEnPesos As Byte, ByVal pIdConcepto As Integer, ByVal pIdSucursal As Integer, ByVal pConContado As Boolean, ByVal pZona As Integer, pIdTipo As Integer, pidTipoSucursal As Integer) As DataView
        

        Dim DS As New DataSet
        
        Comm.CommandText = "delete from tblreppagos"
        Comm.ExecuteNonQuery()
        If pMostrasEnPesos = 0 Then
            Comm.CommandText = "insert into tblreppagos(idpago,fecha,cantidad,tipo,serie,folio,nombre,credito,totalapagar,idventa,fechaf,tipocd,idconceptonotaventa,nombre1,idsucursal,iddeposito) select idpago,tblventaspagos.fecha,if(tblventaspagos.idmoneda=2,round(tblventaspagos.cantidad,2),round(tblventaspagos.cantidad*tblventaspagos.ptipodecambio,2)) as cantidad,tblventaspagos.tipo," + _
            "case tblventaspagos.tipodoci when 0 then (select serie from tblventas where idventa=tblventaspagos.idventa limit 1) " + _
            "when 1 then (select serie from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select serie from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select seriereferencia from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as serie," + _
            "case tblventaspagos.tipodoci when 0 then (select folio from tblventas where idventa=tblventaspagos.idventa limit 1) when 1 then (select folio from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select folio from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select folioreferencia from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as folio," + _
            "tblclientes.nombre," + _
            "case tblventaspagos.tipodoci when 0 then (select if(idconversion=2,round(credito,2),round(credito*tipodecambio,2)) from tblventas where idventa=tblventaspagos.idventa limit 1) when 1 then (select if(idmoneda=2,round(aplicado,2),round(aplicado*tipodecambio,2)) from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select if(idmoneda=2,round(credito,2),round(credito*tipodecambio,2)) from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select if(idmoneda=2,round(credito,2),round(credito*tipodecambio,2)) from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as credito," + _
            "case tblventaspagos.tipodoci when 0 then (select if(idconversion=2,round(totalapagar,2),round(totalapagar*tipodecambio,2)) from tblventas where idventa=tblventaspagos.idventa limit 1) when 1 then (select if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2)) from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2)) from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select if(idmoneda=2,round(totalapagar,2),round(totalapagar*tipodecambio,2)) from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as totalapagar," + _
            "case tblventaspagos.tipodoci when 0 then tblventaspagos.idventa when 1 then tblventaspagos.idcargo when 2 then tblventaspagos.iddocumentod when 3 then tblventaspagos.iddocumentod end as idventa," + _
            "case tblventaspagos.tipodoci when 0 then (select fecha from tblventas where idventa=tblventaspagos.idventa limit 1) when 1 then (select fecha from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select fecha from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select fecha from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as fechaf," + _
            "case tblventaspagos.tipodoci when 0 then 'Factura' when 1 then 'NC' when 2 then 'S. Ini.' when 3 then 'Doc' end as tipodocd," + _
            "tblconceptosnotasventas.idconceptonotaventa,tblconceptosnotasventas.nombre," + _
            "case tblventaspagos.tipodoci when 0 then (select idsucursal from tblventas where idventa=tblventaspagos.idventa limit 1) " + _
            "when 1 then (select idsucursal from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select idsucursal from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select idsucursal from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as idsucursal," + _
            "tblventaspagos.iddeposito" +
            " from tblventaspagos inner join tblclientes on tblventaspagos.idcliente=tblclientes.idcliente inner join tblconceptosnotasventas on tblventaspagos.idconceptonotaventa=tblconceptosnotasventas.idconceptonotaventa  where tblventaspagos.estado=3 and tblventaspagos.fecha>='" + pFecha1 + "' and tblventaspagos.fecha<='" + pFecha2 + "' "
        Else
            Comm.CommandText = "insert into tblreppagos(idpago,fecha,cantidad,tipo,serie,folio,nombre,credito,totalapagar,idventa,fechaf,tipocd,idconceptonotaventa,nombre1,idsucursal,iddeposito) select idpago,tblventaspagos.fecha,round(tblventaspagos.cantidad,2) as cantidad,tblventaspagos.tipo," + _
            "case tblventaspagos.tipodoci when 0 then (select serie from tblventas where idventa=tblventaspagos.idventa limit 1) " + _
            "when 1 then (select serie from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select serie from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select seriereferencia from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as serie," + _
            "case tblventaspagos.tipodoci when 0 then (select folio from tblventas where idventa=tblventaspagos.idventa limit 1) when 1 then (select folio from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select folio from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select folioreferencia from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as folio," + _
            "tblclientes.nombre," + _
            "case tblventaspagos.tipodoci when 0 then (select round(credito,2) from tblventas where idventa=tblventaspagos.idventa limit 1) when 1 then (select round(aplicado,2) from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select round(credito,2) from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select round(credito,2) from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as credito," + _
            "case tblventaspagos.tipodoci when 0 then (select round(totalapagar,2) from tblventas where idventa=tblventaspagos.idventa limit 1) when 1 then (select round(totalapagar,2) from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select round(totalapagar,2) from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select round(totalapagar,2) from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as totalapagar," + _
            "case tblventaspagos.tipodoci when 0 then tblventaspagos.idventa when 1 then tblventaspagos.idcargo when 2 then tblventaspagos.iddocumentod when 3 then tblventaspagos.iddocumentod end as idventa," + _
            "case tblventaspagos.tipodoci when 0 then (select fecha from tblventas where idventa=tblventaspagos.idventa limit 1) when 1 then (select fecha from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select fecha from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select fecha from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as fechaf," + _
            "case tblventaspagos.tipodoci when 0 then 'Factura' when 1 then 'NC' when 2 then 'S. Ini.' when 3 then 'Doc.' end as tipodocd," + _
            "tblconceptosnotasventas.idconceptonotaventa,tblconceptosnotasventas.nombre," + _
            "case tblventaspagos.tipodoci when 0 then (select idsucursal from tblventas where idventa=tblventaspagos.idventa limit 1) " + _
            "when 1 then (select idsucursal from tblnotasdecargo where idcargo=tblventaspagos.idcargo limit 1) " + _
            "when 2 then (select idsucursal from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) when 3 then (select idsucursal from tbldocumentosclientes where iddocumento=tblventaspagos.iddocumentod limit 1) end as idsucursal," + _
            "tblventaspagos.iddeposito" +
            " from tblventaspagos inner join tblclientes on tblventaspagos.idcliente=tblclientes.idcliente inner join tblconceptosnotasventas on tblventaspagos.idconceptonotaventa=tblconceptosnotasventas.idconceptonotaventa where tblventaspagos.estado=3 and tblventaspagos.fecha>='" + pFecha1 + "' and tblventaspagos.fecha<='" + pFecha2 + "' "
        End If
        If pZona > 0 Then
            '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
            Comm.CommandText += " and tblclientes.zona='" + pZona.ToString() + "'"
        End If
        If pIdCliente <> 0 Then
            Comm.CommandText += " and tblventaspagos.idcliente=" + pIdCliente.ToString
        End If
        If pIdMoneda > 0 Then
            Comm.CommandText += " and tblventaspagos.idmoneda=" + pIdMoneda.ToString
        End If
        If pIdConcepto > 0 Then
            Comm.CommandText += " and tblventaspagos.idconceptonotaventa=" + pIdConcepto.ToString
        End If
        Comm.ExecuteNonQuery()

        If pConContado Then
            If pMostrasEnPesos = 0 Then
                Comm.CommandText = "insert into tblreppagos(idpago,fecha,cantidad,tipo,serie,folio,nombre,credito,totalapagar,idventa,fechaf,tipocd,idconceptonotaventa,nombre1,idsucursal,iddeposito) select idventa,fecha,if(idconversion=2,totalapagar,totalapagar*tipodecambio),'Factura Contado',serie,folio,tblclientes.nombre,if(idconversion=2,tblventas.credito,tblventas.credito*tblventas.tipodecambio),if(idconversion=2,totalapagar,totalapagar*tblventas.tipodecambio),idventa,fecha,'F.CONTADO',90,(select nombre from tblconceptosnotasventas where idconceptonotaventa=90),idsucursal,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo=1"
            Else
                Comm.CommandText = "insert into tblreppagos(idpago,fecha,cantidad,tipo,serie,folio,nombre,credito,totalapagar,idventa,fechaf,tipocd,idconceptonotaventa,nombre1,idsucursal,iddeposito) select idventa,fecha,totalapagar,'Factura Contado',serie,folio,tblclientes.nombre,tblventas.credito,totalapagar,idventa,fecha,'F.CONTADO',90,(select nombre from tblconceptosnotasventas where idconceptonotaventa=90),idsucursal,0 from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo=1"
            End If
            If pIdCliente <> 0 Then
                Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
            End If
            If pIdMoneda > 0 Then
                Comm.CommandText += " and tblventas.idconcersion=" + pIdMoneda.ToString
            End If
            If pZona > 0 Then
                '    'Comm.CommandText += " INNER JOIN tblvendedores t2 ON ( tblventas.idvendedor=t2.id)"
                Comm.CommandText += " and tblclientes.zona='" + pZona.ToString() + "'"
            End If
            'If pIdConcepto > 0 Then
            '    Comm.CommandText += " and tblventaspagos.idconceptonotaventa=" + pIdConcepto.ToString
            'End If
            Comm.ExecuteNonQuery()
        End If

        ' Comm.CommandText = "select * from tblreppagos"

        Comm.CommandText = "select *,s.nombre as sucursal from tblreppagos  inner join tblsucursales as s on tblreppagos.idsucursal=s.idsucursal"
        
        If pidTipoSucursal > 0 Then
                Comm.CommandText += " and s.idtipo=" + pidTipoSucursal.ToString
            Else
                If pIdSucursal > 0 Then
                    Comm.CommandText += " where tblreppagos.idsucursal=" + pIdSucursal.ToString()
                End If
            End If
        Comm.CommandText += " order by idconceptonotaventa,tblreppagos.fecha,tblreppagos.nombre,tblreppagos.serie,tblreppagos.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspagos")
        'DS.WriteXmlSchema("tblventaspagos.xml")
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
        Comm.CommandText = "select count(idventa) from tblventaspagos where idventa=" + pidVenta.ToString + " and estado=3"
        Return Comm.ExecuteScalar
    End Function
    Public Function HayPagosCargos(ByVal pidNota As Integer) As Integer
        Comm.CommandText = "select count(idcargo) from tblventaspagos where idcargo=" + pidNota.ToString + " and estado=3"
        Return Comm.ExecuteScalar
    End Function
    Public Function HayPagosDocumentos(ByVal pidDocumento As Integer) As Integer
        Comm.CommandText = "select count(iddocumentod) from tblventaspagos where iddocumentod=" + pidDocumento.ToString + " and estado=3"
        Return Comm.ExecuteScalar
    End Function
    Public Function impresion(ByVal wherestr As String) As DataSet
        Dim ds As New DataSet
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        Comm.CommandText = "select vp.cantidad,c.nombre,c.direccion,c.ciudad,c.rfc,vp.fecha,vp.tipo,c.colonia,c.estado,ifnull((select zona from tblzona where idzona=c.zona),'') as czona,case vp.tipodoci when 0 then (select folio from tblventas where idventa=vp.idventa) when 1 then (select folio from tblnotasdecargo where idcargo=vp.idcargo) when 2 then (select folio from tbldocumentosclientes where iddocumento=vp.iddocumentod) when 3 then (select folioreferencia from tbldocumentosclientes where iddocumento=vp.iddocumentod) end folio,case vp.tipodoci when 0 then (select credito from tblventas where idventa=vp.idventa) when 1 then (select aplicado from tblnotasdecargo where idcargo=vp.idcargo) when 2 then (select credito from tbldocumentosclientes where iddocumento=vp.iddocumentod) when 3 then (select credito from tbldocumentosclientes where iddocumento=vp.iddocumentod) end credito,case vp.tipodoci when 0 then (select totalapagar from tblventas where idventa=vp.idventa) when 1 then (select totalapagar from tblnotasdecargo where idcargo=vp.idcargo) when 2 then (select totalapagar from tbldocumentosclientes where iddocumento=vp.iddocumentod) when 3 then (select totalapagar from tbldocumentosclientes where iddocumento=vp.iddocumentod) end total,case vp.tipodoci when 0 then (select serie from tblventas where idventa=vp.idventa) when 1 then (select serie from tblnotasdecargo where idcargo=vp.idcargo) when 2 then (select serie from tbldocumentosclientes where iddocumento=vp.iddocumentod) when 3 then (select seriereferencia from tbldocumentosclientes where iddocumento=vp.iddocumentod) end serie,vp.tipodoci,cnv.nombre concepto from tblventaspagos vp inner join tblclientes c on vp.idcliente=c.idcliente inner join tblconceptosnotasventas cnv on vp.idconceptonotaventa=cnv.idconceptonotaventa where false" + wherestr
        da.Fill(ds, "pago")
        'ds.WriteXmlSchema("pago.xml")
        Return ds
    End Function
End Class
