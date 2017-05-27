Public Class dbComprasPagos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Cantidad As Double
    Public Estado As Byte
    Public IdCompra As Integer
    Public Fecha As String
    Public Tipo As String
    Public IdDocumento As Integer
    Public TipoDocumento As Byte
    Public Hora As String
    Public FechaCancelado As String
    Public HoraCancelado As String
    Public IdCargo As String
    Public IdProveedor As Integer
    Public idMoneda As Integer
    Public pTipodeCambio As Double
    Public IdDocumentoD As Integer
    Public Tipodoci As Byte
    Public IdConceptoNotaCompra As Integer
    Public IdPagoProv As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Cantidad = 0
        IdCompra = 0
        Fecha = ""
        Tipo = ""
        IdDocumento = 0
        TipoDocumento = 0
        Hora = ""
        FechaCancelado = ""
        HoraCancelado = ""
        IdCargo = 0
        IdProveedor = 0
        idMoneda = 0
        IdDocumentoD = 0
        pTipodeCambio = 0
        Tipodoci = 0
        IdPagoProv = 0
        IdConceptoNotaCompra = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcompraspagos where idpago=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Cantidad = DReader("cantidad")
            Estado = DReader("estado")
            IdCompra = DReader("idcompra")
            Fecha = DReader("fecha")
            Tipo = DReader("tipo")
            IdDocumento = DReader("iddocumento")
            TipoDocumento = DReader("tipodocumento")
            Hora = DReader("hora")
            FechaCancelado = DReader("fechacancelado")
            HoraCancelado = DReader("horacancelado")
            IdCargo = DReader("idcargo")
            IdProveedor = DReader("idproveedor")
            idMoneda = DReader("idmoneda")
            pTipodeCambio = DReader("ptipodecambio")
            IdDocumentoD = DReader("iddocumentod")
            Tipodoci = DReader("tipodoci")
            IdConceptoNotaCompra = DReader("idconceptonotacompra")
            IdPagoProv = DReader("idpagoprov")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pIdVenta As Integer, ByVal pCantidad As Double, ByVal pFecha As String, ByVal pTipo As String, ByVal pidCliente As Integer, ByVal pidDocumento As Integer, ByVal pTipoDocumento As Byte, ByVal pIdCargo As Integer, ByVal ppTipodeCambio As Double, ByVal pIdMoneda As Integer, ByVal pIdDocumentoD As Integer, ByVal pTipoddoci As Byte, ByVal PIdconceptoNotaCompra As Integer)
        Cantidad = pCantidad
        IdCompra = pIdVenta
        Fecha = pFecha
        Tipo = pTipo
        IdDocumento = pidDocumento
        TipoDocumento = pTipoDocumento
        IdCargo = pIdCargo
        IdProveedor = pidCliente
        pTipodeCambio = ppTipodeCambio
        idMoneda = pIdMoneda
        IdDocumentoD = pIdDocumentoD
        Tipodoci = pTipoddoci
        IdConceptoNotaCompra = PIdconceptoNotaCompra
        Comm.CommandTimeout = 200
        Comm.Transaction = Comm.Connection.BeginTransaction
        Comm.CommandText = "insert into tblcompraspagos(idcompra,cantidad,estado,fecha,tipo,iddocumento,tipodocumento,hora,fechacancelado,horacancelado,idcargo,idproveedor,idmoneda,ptipodecambio,iddocumentod,tipodoci,idconceptonotacompra,idpagoprov,idusuarioalta,fechaalta,horaalta,idusuariocambio,fechacambio,horacambio) values(" + IdCompra.ToString + "," + Cantidad.ToString + ",3,'" + Fecha + "','" + Replace(Tipo, "'", "''") + "'," + IdDocumento.ToString + "," + TipoDocumento.ToString + ",'" + Format(TimeOfDay, "HH:mm:ss") + "','" + Format(Date.Now, "yyyy/MM/dd") + "','" + Format(TimeOfDay, "HH:mm:ss") + "'," + IdCargo.ToString + "," + IdProveedor.ToString + "," + idMoneda.ToString + "," + pTipodeCambio.ToString + "," + IdDocumentoD.ToString + "," + Tipodoci.ToString + "," + IdConceptoNotaCompra.ToString + ",0," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idpago) from tblcompraspagos;"
        ID = Comm.ExecuteScalar
        AplicarSaldoaDocumento(IdCompra, IdCargo, Cantidad, 0, idMoneda, "+", "+", pTipodeCambio, IdDocumentoD)
        Comm.Transaction.Commit()
        'Comm.CommandText = "update tblclientes set saldo=saldo-" + pCantidad.ToString + " where idcliente=" + pidCliente.ToString
        'Comm.ExecuteNonQuery()
    End Sub
    Public Function LigadoABancos(ByVal pIdPago As Integer) As Boolean
        Comm.CommandText = "select idpagoprov from tblcompraspagos where idpago=" + pIdPago.ToString
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Sub AplicarSaldoaDocumento(ByVal pIdCompra As Integer, ByVal pidCargo As Integer, ByVal pCantidad As Double, ByVal pCantidadAnt As Double, ByVal pidMoneda As Integer, ByVal pOperador1 As String, ByVal pOperador2 As String, ByVal ppTipodeCambio As Double, ByVal pidDocumentod As Integer)
        Dim docMoneda As Integer
        If pIdCompra <> 0 Then
            Comm.CommandText = "select idmoneda from tblcompras where idcompra=" + pIdCompra.ToString
            docMoneda = Comm.ExecuteScalar
            If docMoneda = pidMoneda Then
                Comm.CommandText = "update tblcompras set credito=credito" + pOperador1 + pCantidad.ToString + pOperador2 + pCantidadAnt.ToString + " where idcompra=" + pIdCompra.ToString
                Comm.ExecuteNonQuery()
            Else
                If docMoneda = 2 Then
                    Comm.CommandText = "update tblcompras set credito=credito" + pOperador1 + CStr(pCantidad * ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt * ppTipodeCambio) + " where idcompra=" + pIdCompra.ToString
                    Comm.ExecuteNonQuery()
                Else
                    Comm.CommandText = "update tblcompras set credito=credito" + pOperador1 + CStr(pCantidad / ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt / ppTipodeCambio) + " where idcompra=" + pIdCompra.ToString
                    Comm.ExecuteNonQuery()
                End If
            End If
        End If
        If pidCargo <> 0 Then
            Comm.CommandText = "select idmoneda from tblnotasdecargocompras where idcargo=" + pidCargo.ToString
            docMoneda = Comm.ExecuteScalar
            If docMoneda = pidMoneda Then
                Comm.CommandText = "update tblnotasdecargocompras set aplicado=aplicado" + pOperador1 + pCantidad.ToString + pOperador2 + pCantidadAnt.ToString + " where idcargo=" + pidCargo.ToString
                Comm.ExecuteNonQuery()
            Else
                If docMoneda = 2 Then
                    Comm.CommandText = "update tblnotasdecargocompras set aplicado=aplicado" + pOperador1 + CStr(pCantidad * ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt * ppTipodeCambio) + " where idcargo=" + pidCargo.ToString
                    Comm.ExecuteNonQuery()
                Else
                    Comm.CommandText = "update tblnotasdecargocompras set aplicado=aplicado" + pOperador1 + CStr(pCantidad / ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt / ppTipodeCambio) + " where idcargo=" + pidCargo.ToString
                    Comm.ExecuteNonQuery()
                End If
            End If
        End If
        If pidDocumentod <> 0 Then
            Comm.CommandText = "select idmoneda from tbldocumentosproveedores where iddocumento=" + pidDocumentod.ToString
            docMoneda = Comm.ExecuteScalar
            If docMoneda = pidMoneda Then
                Comm.CommandText = "update tbldocumentosproveedores set credito=credito" + pOperador1 + pCantidad.ToString + pOperador2 + pCantidadAnt.ToString + " where iddocumento=" + pidDocumentod.ToString
                Comm.ExecuteNonQuery()
            Else
                If docMoneda = 2 Then
                    Comm.CommandText = "update tbldocumentosproveedores set credito=credito" + pOperador1 + CStr(pCantidad * ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt * ppTipodeCambio) + " where iddocumento=" + pidDocumentod.ToString
                    Comm.ExecuteNonQuery()
                Else
                    Comm.CommandText = "update tbldocumentosproveedores set credito=credito" + pOperador1 + CStr(pCantidad / ppTipodeCambio) + pOperador2 + CStr(pCantidadAnt / ppTipodeCambio) + " where iddocumento=" + pidDocumentod.ToString
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
        Comm.CommandTimeout = 200
        Comm.Transaction = Comm.Connection.BeginTransaction
        Comm.CommandText = "update tblcompraspagos set cantidad=" + Cantidad.ToString + ",fecha='" + Fecha + "',tipo='" + Replace(Tipo, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idpago=" + ID.ToString
        Comm.ExecuteNonQuery()
        AplicarSaldoaDocumento(IdCompra, IdCargo, Cantidad, pCantidadAnt, idMoneda, "+", "-", pTipodeCambio, IdDocumentoD)
        Comm.Transaction.Commit()
    End Sub
    Public Sub CancelarPago(ByVal pIdPago As Integer, ByVal pEstado As Integer, ByVal pidCliente As Integer)
        ID = pIdPago
        LlenaDatos()
        Estado = pEstado
        Comm.CommandTimeout = 200
        Comm.Transaction = Comm.Connection.BeginTransaction
        Comm.CommandText = "update tblcompraspagos set estado=" + Estado.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "' where idpago=" + ID.ToString
        Comm.ExecuteNonQuery()
        AplicarSaldoaDocumento(IdCompra, IdCargo, Cantidad, 0, idMoneda, "-", "-", pTipodeCambio, IdDocumentoD)
        Comm.Transaction.Commit()
        Eliminar(ID)
    End Sub
    Public Sub CancelarPagosxDocumento(ByVal pidDocumento As Integer, ByVal pTipoDocumento As Byte, ByVal pIdCliente As Integer, ByVal pEstado As Byte)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Cont As Integer = 1
        Comm.CommandText = "update tblcompraspagos set estado=" + pEstado.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "' where iddocumento=" + pidDocumento.ToString + " and tipodocumento=" + pTipoDocumento.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select idpago from tblcompraspagos where iddocumento=" + pidDocumento.ToString + " and tipodocumento=" + pTipoDocumento.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("idpago"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            ID = IDs.Item(Cont)
            LlenaDatos()
            AplicarSaldoaDocumento(IdCompra, IdCargo, Cantidad, 0, idMoneda, "-", "-", pTipodeCambio, IdDocumentoD)
            Cont += 1
        End While
        Comm.CommandText = "delete from tblcompraspagos where iddocumento=" + pidDocumento.ToString + " and tipodocumento=" + pTipoDocumento.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        ID = pID
        'LlenaDatos()
        Comm.CommandText = "delete from tblcompraspagos where idpago=" + pID.ToString
        Comm.ExecuteNonQuery()
        'AplicarSaldoaDocumento(IdCompra, IdCargo, Cantidad, 0, idMoneda, "-", "-", pTipodeCambio, IdDocumentoD)

    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer, ByVal pidCargo As Integer, ByVal pidDocumento As Integer) As DataView
        Dim DS As New DataSet
        If pIdVenta <> 0 Then Comm.CommandText = "select  idpago,fecha,cantidad,tblcompraspagos.tipo,iddocumento,tipodocumento,tblconceptosnotascompras.nombre,case tblcompraspagos.idpagoprov when 0 then ' ' else 'SI' end as Ligado from tblcompraspagos inner join tblconceptosnotascompras on tblcompraspagos.idconceptonotacompra=tblconceptosnotascompras.idconceptonotacompra where estado=3 and idcompra=" + pIdVenta.ToString
        If pidCargo <> 0 Then Comm.CommandText = "select  idpago,fecha,cantidad,tblcompraspagos.tipo,iddocumento,tipodocumento,tblconceptosnotascompras.nombre,case tblcompraspagos.idpagoprov when 0 then ' ' else 'SI' end as Ligado from tblcompraspagos inner join tblconceptosnotascompras on tblcompraspagos.idconceptonotacompra=tblconceptosnotascompras.idconceptonotacompra where estado=3 and idcargo=" + pidCargo.ToString
        If pidDocumento <> 0 Then Comm.CommandText = "select  idpago,fecha,cantidad,tblcompraspagos.tipo,iddocumento,tipodocumento,tblconceptosnotascompras.nombre,case tblcompraspagos.idpagoprov when 0 then ' ' else 'SI' end as Ligado from tblcompraspagos inner join tblconceptosnotascompras on tblcompraspagos.idconceptonotacompra=tblconceptosnotascompras.idconceptonotacompra where estado=3 and iddocumentod=" + pidDocumento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcompraspagos")
        Return DS.Tables("tblcompraspagos").DefaultView
    End Function
    'Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pidproveedor As Integer, ByVal pIdMoneda As Integer, ByVal pMostrasEnPesos As Byte) As DataView
    '    Dim DS As New DataSet
    '    If pMostrasEnPesos = 0 Then
    '        Comm.CommandText = "select idpago,tblcompraspagos.fecha,if(tblcompraspagos.idmoneda=2,tblcompraspagos.cantidad,tblcompraspagos.cantidad*tblcompraspagos.ptipodecambio) as cantidad,tblcompraspagos.tipo," + _
    '        "if(tblcompraspagos.idcompra<>0,(select referencia from tblcompras where idcompra=tblcompraspagos.idcompra limit 1),(select folio from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1)) as folio," + _
    '        "tblproveedores.nombre," + _
    '        "if(tblcompraspagos.idcompra<>0,(select if(idmoneda=2,credito,credito*tipodecambio) from tblcompras where idcompra=tblcompraspagos.idcompra limit 1),(select if(idmoneda=2,aplicado,aplicado*tipodecambio) from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1)) as credito," + _
    '        "if(tblcompraspagos.idcompra<>0,(select if(idmoneda=2,totalapagar,totalapagar*tipodecambio) from tblcompras where idcompra=tblcompraspagos.idcompra limit 1),(select if(idmoneda=2,totalapagar,totalapagar*tipodecambio) from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1)) as totalapagar," + _
    '        "if(tblcompraspagos.idcompra<>0,idcompra,idcargo) as idcompra," + _
    '        "if(tblcompraspagos.idcompra<>0,(select fecha from tblcompras where idcompra=tblcompraspagos.idcompra limit 1),(select fecha from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1)) as fechaf" + _
    '        " from tblcompraspagos inner join tblproveedores on tblcompraspagos.idproveedor=tblproveedores.idproveedor where tblcompraspagos.estado=3 and tblcompraspagos.fecha>='" + pFecha1 + "' and tblcompraspagos.fecha<='" + pFecha2 + "' "
    '    Else
    '        Comm.CommandText = "select idpago,tblcompraspagos.fecha,tblcompraspagos.cantidad,tblcompraspagos.tipo," + _
    '        "if(tblcompraspagos.idcompra<>0,(select referencia from tblcompras where idcompra=tblcompraspagos.idcompra limit 1),(select folio from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1)) as folio," + _
    '        "tblproveedores.nombre," + _
    '        "if(tblcompraspagos.idcompra<>0,(select credito from tblcompras where idcompra=tblcompraspagos.idcompra limit 1),(select aplicado from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1)) as credito," + _
    '        "if(tblcompraspagos.idcompra<>0,(select totalapagar from tblcompras where idcompra=tblcompraspagos.idcompra limit 1),(select totalapagar from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1)) as totalapagar," + _
    '        "if(tblcompraspagos.idcompra<>0,idcompra,idcargo) as idcompra," + _
    '        "if(tblcompraspagos.idcompra<>0,(select fecha from tblcompras where idcompra=tblcompraspagos.idcompra limit 1),(select fecha from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1)) as fechaf" + _
    '        " from tblcompraspagos inner join tblproveedores on tblcompraspagos.idproveedor=tblproveedores.idproveedor where tblcompraspagos.estado=3 and tblcompraspagos.fecha>='" + pFecha1 + "' and tblcompraspagos.fecha<='" + pFecha2 + "' "
    '    End If
    '    If pidproveedor <> 0 Then
    '        Comm.CommandText += " and tblcompraspagos.idproveedor=" + pidproveedor.ToString
    '    End If
    '    If pIdMoneda > 0 Then
    '        Comm.CommandText += " and tblcompraspagos.idmoneda=" + pIdMoneda.ToString
    '    End If
    '    Comm.CommandText += " order by tblcompraspagos.fecha,tblproveedores.nombre,serie,folio"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblcompraspagos")
    '    'DS.WriteXmlSchema("tblcompraspagos.xml")
    '    Return DS.Tables("tblcompraspagos").DefaultView
    'End Function
    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdProveedor As Integer, ByVal pIdMoneda As Integer, ByVal pMostrasEnPesos As Byte, ByVal pIdConcepto As Integer, ByVal pConContado As Boolean, pIdTipoProv As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "delete from tblreppagosc"
        Comm.ExecuteNonQuery()
        If pMostrasEnPesos = 0 Then
            Comm.CommandText = "insert into tblreppagosc select idpago,tblcompraspagos.fecha,if(tblcompraspagos.idmoneda=2,tblcompraspagos.cantidad,tblcompraspagos.cantidad*tblcompraspagos.ptipodecambio) as cantidad,tblcompraspagos.tipo," + _
            "ifnull((case tblcompraspagos.tipodoci when 0 then (select serie from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) " + _
            "when 1 then (select folio from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
            "when 2 then (select serie from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select seriereferencia from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end),'') as serie," + _
            "ifnull((case tblcompraspagos.tipodoci when 0 then (select folioi from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then 0 " + _
            "when 2 then (select folio from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select folioreferencia from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end),0) as folio," + _
            "tblproveedores.nombre," + _
            "case tblcompraspagos.tipodoci when 0 then (select if(idmoneda=2,credito,credito*tipodecambio) from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then (select if(idmoneda=2,aplicado,aplicado*tipodecambio) from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
            "when 2 then (select if(idmoneda=2,credito,credito*tipodecambio) from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select if(idmoneda=2,credito,credito*tipodecambio) from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end as credito," + _
            "case tblcompraspagos.tipodoci when 0 then (select if(idmoneda=2,totalapagar,totalapagar*tipodecambio) from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then (select if(idmoneda=2,totalapagar,totalapagar*tipodecambio) from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
            "when 2 then (select if(idmoneda=2,totalapagar,totalapagar*tipodecambio) from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select if(idmoneda=2,totalapagar,totalapagar*tipodecambio) from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end as totalapagar," + _
            "case tblcompraspagos.tipodoci when 0 then tblcompraspagos.idcompra when 1 then tblcompraspagos.idcargo when 2 then tblcompraspagos.iddocumentod when 3 then tblcompraspagos.iddocumentod end as idcompra," + _
            "case tblcompraspagos.tipodoci when 0 then (select fecha from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then (select fecha from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
            "when 2 then (select fecha from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select fecha from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end as fechaf," + _
            "case tblcompraspagos.tipodoci when 0 then 'Factura' when 1 then 'NC' when 2 then 'S. Ini.' when 3 then 'Doc' end as tipodocd," + _
            "tblconceptosnotascompras.idconceptonotacompra,tblconceptosnotascompras.nombre,tblcompraspagos.idpagoprov " + _
            " from tblcompraspagos inner join tblproveedores on tblcompraspagos.idproveedor=tblproveedores.idproveedor inner join tblconceptosnotascompras on tblcompraspagos.idconceptonotacompra=tblconceptosnotascompras.idconceptonotacompra where tblcompraspagos.estado=3 and tblcompraspagos.fecha>='" + pFecha1 + "' and tblcompraspagos.fecha<='" + pFecha2 + "' "
        Else
            Comm.CommandText = "insert into tblreppagosc select idpago,tblcompraspagos.fecha,tblcompraspagos.cantidad as cantidad,tblcompraspagos.tipo," + _
            "ifnull((case tblcompraspagos.tipodoci when 0 then (select serie from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) " + _
            "when 1 then (select folio from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
            "when 2 then (select serie from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select seriereferencia from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end),'') as serie," + _
            "ifnull((case tblcompraspagos.tipodoci when 0 then (select folioi from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then 0 " + _
            "when 2 then (select folio from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select folioreferencia from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end),0) as folio," + _
            "tblproveedores.nombre," + _
            "case tblcompraspagos.tipodoci when 0 then (select credito from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then (select aplicado from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
            "when 2 then (select credito from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select credito from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end as credito," + _
            "case tblcompraspagos.tipodoci when 0 then (select totalapagar from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then (select totalapagar from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
            "when 2 then (select totalapagar from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select totalapagar from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end as totalapagar," + _
            "case tblcompraspagos.tipodoci when 0 then tblcompraspagos.idcompra when 1 then tblcompraspagos.idcargo when 2 then tblcompraspagos.iddocumentod when 3 then tblcompraspagos.iddocumentod end as idcompra," + _
            "case tblcompraspagos.tipodoci when 0 then (select fecha from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then (select fecha from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
            "when 2 then (select fecha from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select fecha from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end as fechaf," + _
            "case tblcompraspagos.tipodoci when 0 then 'Factura' when 1 then 'NC' when 2 then 'S. Ini.' when 3 then 'Doc.' end as tipodocd," + _
            "tblconceptosnotascompras.idconceptonotacompra,tblconceptosnotascompras.nombre,tblcompraspagos.idpagoprov " + _
            " from tblcompraspagos inner join tblproveedores on tblcompraspagos.idproveedor=tblproveedores.idproveedor inner join tblconceptosnotascompras on tblcompraspagos.idconceptonotacompra=tblconceptosnotascompras.idconceptonotacompra where tblcompraspagos.estado=3 and tblcompraspagos.fecha>='" + pFecha1 + "' and tblcompraspagos.fecha<='" + pFecha2 + "' "
        End If

        If pIdProveedor <> 0 Then
            Comm.CommandText += " and tblcompraspagos.idproveedor=" + pIdProveedor.ToString
        End If
        If pIdMoneda > 0 Then
            Comm.CommandText += " and tblcompraspagos.idmoneda=" + pIdMoneda.ToString
        End If
        If pIdConcepto > 0 Then
            Comm.CommandText += " and tblcompraspagos.idconceptonotacompra=" + pIdConcepto.ToString
        End If
        If pIdTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pIdTipoProv.ToString
        Comm.ExecuteNonQuery()
        If pConContado Then
            If pMostrasEnPesos = 0 Then
                Comm.CommandText = "insert into tblreppagosc(idpago,fecha,cantidad,tipo,serie,folio,nombre,credito,totalapagar,idcompra,fechaf,tipocd,idconceptonotacompra,nombre1) select idcompra,fecha,if(idmoneda=2,totalapagar,totalapagar*tipodecambio),'Compra Contado',serie,folioi,tblproveedores.nombre,if(idmoneda=2,tblventas.credito,tblventas.credito*tblventas.tipodecambio),if(idmoneda=2,totalapagar,totalapagar*tblventas.tipodecambio),idcompra,fecha,'C.CONTADO',90,(select nombre from tblconceptosnotascompras where idconceptonotacompra=90) from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo=1"
            Else
                Comm.CommandText = "insert into tblreppagosc(idpago,fecha,cantidad,tipo,serie,folio,nombre,credito,totalapagar,idcompra,fechaf,tipocd,idconceptonotacompra,nombre1) select idcompra,fecha,totalapagar,'Compra Contado',serie,folioi,tblproveedores.nombre,tblventas.credito,totalapagar,idcompra,fecha,'C.CONTADO',90,(select nombre from tblconceptosnotascompras where idconceptonotacompra=90) from tblcompras as tblventas inner join tblclientes on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "' and tblformasdepago.tipo=1"
            End If
            If pIdProveedor <> 0 Then
                Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
            End If
            If pIdMoneda > 0 Then
                Comm.CommandText += " and tblventas.idmoneda=" + pIdMoneda.ToString
            End If
            'If pIdConcepto > 0 Then
            '    Comm.CommandText += " and tblventaspagos.idconceptonotaventa=" + pIdConcepto.ToString
            'End If
            Comm.ExecuteNonQuery()
        End If

        Comm.CommandText = "select * from tblreppagosc "
        Comm.CommandText += " order by idconceptonotacompra,fecha,nombre,serie,folio"

        'Comm.CommandText += " order by tblcompraspagos.fecha,tblproveedores.nombre,serie,folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcompraspagos")
        'DS.WriteXmlSchema("tblcompraspagos.xml")
        Return DS.Tables("tblcompraspagos").DefaultView
    End Function

    Public Function ReporteRecibo(ByVal pIdPago As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idpago,tblcompraspagos.fecha,if(tblcompraspagos.idmoneda=2,tblcompraspagos.cantidad,tblcompraspagos.cantidad*tblcompraspagos.ptipodecambio) as cantidad,tblcompraspagos.tipo," + _
          "case tblcompraspagos.tipodoci when 0 then (select serie from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) " + _
          "when 1 then (select folio from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
          "when 2 then (select serie from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select seriereferencia from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end as serie," + _
          "case tblcompraspagos.tipodoci when 0 then (select folioi from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then 0 " + _
          "when 2 then (select folio from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select folioreferencia from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end as folio," + _
          "tblproveedores.nombre," + _
          "case tblcompraspagos.tipodoci when 0 then (select if(idmoneda=2,credito,credito*tipodecambio) from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then (select if(idmoneda=2,aplicado,aplicado*tipodecambio) from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
          "when 2 then (select if(idmoneda=2,credito,credito*tipodecambio) from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select if(idmoneda=2,credito,credito*tipodecambio) from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end as credito," + _
          "case tblcompraspagos.tipodoci when 0 then (select if(idmoneda=2,totalapagar,totalapagar*tipodecambio) from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then (select if(idmoneda=2,totalapagar,totalapagar*tipodecambio) from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
          "when 2 then (select if(idmoneda=2,totalapagar,totalapagar*tipodecambio) from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select if(idmoneda=2,totalapagar,totalapagar*tipodecambio) from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end as totalapagar," + _
          "case tblcompraspagos.tipodoci when 0 then tblcompraspagos.idcompra when 1 then tblcompraspagos.idcargo when 2 then tblcompraspagos.iddocumentod when 3 then tblcompraspagos.iddocumentod end as idcompra," + _
          "case tblcompraspagos.tipodoci when 0 then (select fecha from tblcompras where idcompra=tblcompraspagos.idcompra limit 1) when 1 then (select fecha from tblnotasdecargocompras where idcargo=tblcompraspagos.idcargo limit 1) " + _
          "when 2 then (select fecha from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) when 3 then (select fecha from tbldocumentosproveedores where iddocumento=tblcompraspagos.iddocumentod limit 1) end as fechaf," + _
          "case tblcompraspagos.tipodoci when 0 then 'Factura' when 1 then 'NC' when 2 then 'S. Ini.' when 3 then 'Doc' end as tipodocd," + _
          "tblconceptosnotascompras.idconceptonotacompra,tblconceptosnotascompras.nombre " + _
          " from tblcompraspagos inner join tblproveedores on tblcompraspagos.idproveedor=tblproveedores.idproveedor inner join tblconceptosnotascompras on tblcompraspagos.idconceptonotacompra=tblconceptosnotascompras.idconceptonotacompra where tblcompraspagos.idpago=" + pIdPago.ToString
        
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcompraspagosrecibo")
        DS.WriteXmlSchema("tblcompraspagosrecibo.xml")
        Return DS.Tables("tblcompraspagosrecibo").DefaultView
    End Function
    Public Function HayPagosCompras(ByVal pidCompra As Integer) As Integer
        Comm.CommandText = "select count(idcompra) from tblcompraspagos where idcompra=" + pidCompra.ToString + " and estado=3"
        Return Comm.ExecuteScalar
    End Function
    Public Function HayPagosCargos(ByVal pidNota As Integer) As Integer
        Comm.CommandText = "select count(idcargo) from tblcompraspagos where idcargo=" + pidNota.ToString + " and estado=3"
        Return Comm.ExecuteScalar
    End Function
    Public Function HayPagosDocumentos(ByVal pidDocumento As Integer) As Integer
        Comm.CommandText = "select count(iddocumentod) from tblcompraspagos where iddocumentod=" + pidDocumento.ToString + " and estado=3"
        Return Comm.ExecuteScalar
    End Function
    Public Function impresion(ByVal wherestr As String) As DataSet
        Dim ds As New DataSet
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)

        Comm.CommandText = "select vp.cantidad,c.nombre,c.direccion,c.ciudad,c.rfc,vp.fecha,vp.tipo,c.colonia,c.estado,case vp.tipodoci when 0 then (select folioi from tblcompras where idcompra=vp.idcompra) when 1 then 0 when 2 then (select folio from tbldocumentosproveedores where iddocumento=vp.iddocumentod) when 3 then (select folioreferencia from tbldocumentosproveedores where iddocumento=vp.iddocumentod) end folio, case vp.tipodoci when 0 then (select credito from tblcompras where idcompra=vp.idcompra) when 1 then (select aplicado from tblnotasdecargocompras where idcargo=vp.idcargo) when 2 then (select credito from tbldocumentosproveedores where iddocumento=vp.iddocumentod) when 3 then (select credito from tbldocumentosproveedores where iddocumento=vp.iddocumentod) end credito,case vp.tipodoci when 0 then (select totalapagar from tblcompras where idcompra=vp.idcompra) when 1 then (select totalapagar from tblnotasdecargocompras where idcargo=vp.idcargo) when 2 then (select totalapagar from tbldocumentosproveedores where iddocumento=vp.iddocumentod) when 3 then (select totalapagar from tbldocumentosproveedores where iddocumento=vp.iddocumentod) end total,case vp.tipodoci when 0 then (select concat(serie,' ',referencia) from tblcompras where idcompra=vp.idcompra) when 1 then (select folio from tblnotasdecargocompras where idcargo=vp.idcargo) when 2 then (select serie from tbldocumentosproveedores where iddocumento=vp.iddocumentod) when 3 then (select seriereferencia from tbldocumentosproveedores where iddocumento=vp.iddocumentod) end serie,vp.tipodoci,cnv.nombre concepto from tblcompraspagos vp inner join tblproveedores c on vp.idproveedor=c.idproveedor inner join tblconceptosnotascompras cnv on vp.idconceptonotacompra=cnv.idconceptonotacompra where false" + wherestr
        da.Fill(ds, "pago")
        'ds.WriteXmlSchema("pago.xml")
        Return ds
    End Function
End Class
