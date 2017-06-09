Public Class dbDevolucionesCompras
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdProvedor As Integer
    Public Fecha As String
    Public Proveedor As dbproveedores
    Public Folio As String
    Public Facturado As Byte
    Public Credito As Double
    Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    Public Estado As Byte
    Public IdSucursal As Integer
    Public IdFormadePago As Integer
    Public TipodeCambio As Double
    Public IdConversion As Integer
    Public TotalVenta As Double
    Public Subtototal As Double
    Public TotalIva As Double
    Public TotalIeps As Double
    Public TotalIvaretenidoCon As Double
    Public idCompra As Integer
    Public IdRemision As Integer
    Public Comentario As String
    Public Serie As String
    Public Folioi As Integer
    Public Foliocfdi As String
    Public FechaCancelado As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdProvedor = -1
        Fecha = ""
        Hora = ""
        Folio = 0
        Facturado = 0
        Credito = 0
        Iva = 0
        TotalaPagar = 0
        Total = 0
        Comentario = ""
        Estado = 0
        IdSucursal = 0
        IdFormadePago = 0
        TipodeCambio = 0
        IdConversion = 0
        idcompra = 0
        IdRemision = 0
        Folioi = 0
        Serie = ""
        Foliocfdi = ""
        Comm.Connection = Conexion
        Proveedor = New dbproveedores(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbldevolucionescompras where iddevolucion=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdProvedor = DReader("idproveedor")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            Facturado = DReader("facturado")
            Credito = DReader("credito")
            Iva = DReader("iva")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            IdFormadePago = DReader("idforma")
            TipodeCambio = DReader("tipodecambio")
            IdConversion = DReader("idconversion")
            idCompra = DReader("idcompra")
            IdRemision = DReader("idremision")
            Comentario = DReader("comentario")
            Serie = DReader("serie")
            Folioi = DReader("folioi")
            Foliocfdi = DReader("uuid")
            FechaCancelado = DReader("fechacancelado")
        End If
        DReader.Close()
        Proveedor = New dbproveedores(IdProvedor, Comm.Connection)
    End Sub
   
    Public Sub Guardar(ByVal pIdProveedor As Integer, ByVal pFecha As String, ByVal pFolio As String, ByVal pIva As Double, ByVal pidSucursal As Integer, ByVal pIdFormaDepago As Integer, ByVal pTipodeCambio As Double, ByVal pIdConversion As Integer, ByVal pidVenta As Integer, ByVal pidRemision As Integer, ByVal pSerie As String, ByVal pFolioi As Integer, ByVal pfolioCFDI As String)
        IdProvedor = pIdProveedor
        Fecha = pFecha
        Folio = pFolio
        Iva = pIva
        IdSucursal = pidSucursal
        IdFormadePago = pIdFormaDepago
        TipodeCambio = pTipodeCambio
        IdConversion = pIdConversion
        idCompra = pidVenta
        IdRemision = pidRemision
        Comm.CommandText = "insert into tbldevolucionescompras(idproveedor, fecha, folio, facturado, credito, iva, totalapagar, total, hora, estado, idsucursal, idforma, tipodecambio, idconversion, fechacancelado, horacancelado, idcompra, idremision, comentario, serie, folioi, uuid, idUsuarioAlta, fechaAlta, horaAlta, idUsuarioCambio, fechaCambio, horaCambio) values(" + IdProvedor.ToString + ",'" + Fecha + "','" + Replace(Folio, "'", "''") + "',0,0," + Iva.ToString + ",0,0,'" + Format(TimeOfDay, "HH:mm:ss") + "',1," + IdSucursal.ToString + "," + IdFormadePago.ToString + "," + TipodeCambio.ToString + "," + IdConversion.ToString + ",'',''," + idCompra.ToString + "," + IdRemision.ToString + ",'','" + Replace(pSerie.Trim, "'", "''") + "'," + pFolioi.ToString + ",'" + Replace(pfolioCFDI, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(iddevolucion) from tbldevolucionescompras"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As String, ByVal pIva As Double, ByVal pEstado As Byte, ByVal pIdFormadePago As Integer, ByVal pCredito As Byte, ByVal pTipodeCambio As Double, ByVal pidConversion As Integer, ByVal pSubTotal As Double, ByVal pTotal As Double, ByVal pIdProveedor As Integer, ByVal pComentario As String, ByVal pSerie As String, ByVal pFolioi As Integer, ByVal pFolioCFDI As String)
        ID = pID
        Fecha = pFecha
        Folio = pFolio
        Iva = pIva
        Estado = pEstado
        Credito = Credito
        IdFormadePago = pIdFormadePago
        TipodeCambio = pTipodeCambio
        IdConversion = pidConversion
        IdProvedor = pIdProveedor
        Comentario = pComentario
        Comm.CommandText = "update tbldevolucionescompras set fecha='" + Fecha + "',folio='" + Replace(Folio, "'", "''") + "',iva=" + Iva.ToString + ",estado=" + Estado.ToString + ",idforma=" + IdFormadePago.ToString + ",credito=" + Credito.ToString + ",tipodecambio=" + TipodeCambio.ToString + ",idconversion=" + IdConversion.ToString + ",total=" + pSubTotal.ToString + ",totalapagar=" + pTotal.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',idproveedor=" + IdProvedor.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "',serie='" + Replace(pSerie.Trim, "'", "''") + "',folioi=" + pFolioi.ToString + ",uuid='" + Replace(pFolioCFDI.Trim, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where iddevolucion=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folioi) from tbldevolucionescompras where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado>=2)),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Sub ActualizaComentario(ByVal piddevolucion As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tbldevolucionescompras set comentario='" + Replace(pTexto, "'", "''") + "' where iddevolucion=" + piddevolucion.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete dcu from tbldevolucionescubicaciones dcu inner join tbldevolucionesdetallesc ddc on dcu.iddetalle=ddc.iddetalle where ddc.iddevolucion=" + pID.ToString() + ";delete from tbldevolucionesdetallesc where iddevolucion=" + pID.ToString + ";delete from tbldevolucionescompras where iddevolucion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarDetalles(ByVal pID As Integer)
        Comm.CommandText = "delete dcu from tbldevolucionescubicaciones dcu inner join tbldevolucionesdetallesc ddc on dcu.iddetalle=ddc.iddetalle where ddc.iddevolucion=" + pID.ToString() + "; delete from tbldevolucionesdetallesc where iddevolucion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, Optional ByVal pNombreClave As String = "", Optional ByVal pFolio As String = "", Optional ByVal pEstado As Byte = 0, Optional ByVal pCredido As Byte = 200) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevolucionescompras.iddevolucion,tbldevolucionescompras.fecha,concat(tbldevolucionescompras.serie,convert(tbldevolucionescompras.folioi using utf8),' ',tbldevolucionescompras.folio),tblproveedores.clave,tblproveedores.nombre as Cliente,case tbldevolucionescompras.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado from tbldevolucionescompras inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblproveedores.clave,tblproveedores.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and tbldevolucionescompras.folio like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tbldevolucionescompras.estado<>1"
        End If
        Comm.CommandText += " order by tbldevolucionescompras.fecha desc,tbldevolucionescompras.serie,tbldevolucionescompras.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevolucionescompras")
        Return DS.Tables("tbldevolucionescompras").DefaultView
    End Function

    Public Function ConsultaDeudas(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidCliente As Integer, ByVal pFolio As String, ByVal pidTipodePago As Integer, ByVal PorFechas As Boolean, ByVal Todas As Boolean, ByVal pTipodeOrden As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevolucionescompras.iddevolucion,0 as sel,tbldevolucionescompras.fecha,tbldevolucionescompras.folio,tbldevolucionescompras.credito,tbldevolucionescompras.totalapagar,tbldevolucionescompras.totalapagar-tbldevolucionescompras.credito as restante from tbldevolucionescompras inner join tblproveedores on tbldevoluciones.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tbldevolucionescompras.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idproveedor=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        If Todas = False Then
            Comm.CommandText += " and tbldevolucionescompras.credito<tbldevolucionescompras.totalapagar"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and tbldevolucionescompras.folio like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pTipodeOrden = 0 Then
            Comm.CommandText += " order by tbldevolucionescompras.fecha,tbldevolucionescompras.folio"
        Else
            Comm.CommandText += " order by tbldevolucionescompras.totalapagar,tbldevolucionescompras.folio"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevolucionescompras")
        Return DS.Tables("tbldevolucionescompras").DefaultView
    End Function

    Public Function DaTotal(ByVal piddevolucion As Integer, ByVal pidMoneda As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0
        Dim iIva As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double
        Dim pIEPS As Double
        Dim pIVARetenido As Double
        Subtototal = 0
        TotalIva = 0
        TotalVenta = 0
        TotalIeps = 0
        TotalIvaretenidoCon = 0
        Comm.CommandText = "select tipodecambio from tbldevolucionescompras where iddevolucion=" + piddevolucion.ToString
        iTipoCambio = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tbldevolucionesdetallesc where iddevolucion=" + piddevolucion.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tbldevolucionesdetallesc where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select iva from tbldevolucionesdetallesc where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tbldevolucionesdetallesc where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            Comm.CommandText = "select ieps from tbldevolucionesdetallesc where iddetalle=" + IDs.Item(Cont).ToString
            pIEPS = Comm.ExecuteScalar
            Comm.CommandText = "select ivaRetenido from tbldevolucionesdetallesc where iddetalle=" + IDs.Item(Cont).ToString
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
            TotalIeps += (Precio * (pIEPS / 100))
            TotalIvaretenidoCon += (Precio * (pIVARetenido / 100))

            Subtototal += Precio
            TotalIva += (Precio * (iIva / 100))
            Cont += 1
        End While
        TotalVenta = Subtototal + TotalIva + TotalIeps - TotalIvaretenidoCon '- TotalISR - TotalIvaRetenido
        Return TotalVenta
    End Function
    
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pIdProveedor As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folioi) from tbldevolucionescompras where folioi=" + pFolio.ToString + " and estado<>1 and estado<>2 and serie='" + Replace(pSerie.Trim, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    
    Public Sub AgregarDetallesReferencia(ByVal Piddevolucion As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte)
        '0 cotizacion
        '1 pedido
        '2 remision
        '3 ventas
        If Tipo = 2 Then
            Comm.CommandText = "insert into tbldevolucionesdetallesc(iddevolucion, idinventario, cantidad, precio, descripcion, idmoneda , idalmacen, iva, descuento, IEPS, ivaRetenido) select " + Piddevolucion.ToString + ", crd.idinventario, crd.cantidad-(ifnull((select sum(ddc.cantidad) from tbldevolucionesdetallesc ddc inner join tbldevolucionescompras on ddc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idremision=crd.idremision and ddc.idinventario=crd.idinventario and tbldevolucionescompras.estado=3),0)), crd.precio/crd.cantidad*(crd.cantidad-(ifnull((select sum(ddc.cantidad) from tbldevolucionesdetallesc ddc inner join tbldevolucionescompras on ddc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idremision=crd.idremision and crd.idinventario=ddc.idinventario and tbldevolucionescompras.estado=3),0))), i.nombre, crd.idmoneda, crd.idalmacen, crd.iva, crd.descuento,  crd.IEPS,  crd.ivaRetenido " + _
            "from tblcomprasremisionesdetalles crd inner join tblinventario i on crd.idinventario=i.idinventario where i.inventariable=1 and idremision=" + pIdDocumento.ToString + " and (crd.cantidad-(ifnull((select sum(ddc.cantidad) from tbldevolucionesdetallesc ddc inner join tbldevolucionescompras on ddc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idremision=crd.idremision and ddc.idinventario=crd.idinventario and tbldevolucionescompras.estado=3),0)))>0;" + _
            "insert into tbldevolucionescubicaciones (iddetalle, ubicacion, cantidad, surtido) select ddc.iddetalle, ubicacion, tcu.cantidad, 0 from tblcomprasremisionesubicaciones tcu inner join tblcomprasremisionesdetalles cd on cd.iddetalle=tcu.iddetalle inner join tbldevolucionesdetallesc ddc on ddc.idinventario=cd.idinventario and ddc.cantidad=cd.cantidad and ddc.precio=cd.precio where cd.idremision=" + pIdDocumento.ToString() + " and ddc.iddevolucion=" + Piddevolucion.ToString()
            Comm.ExecuteNonQuery()
        ElseIf Tipo = 3 Then
            Comm.CommandText = "insert into tbldevolucionesdetallesc(iddevolucion, idinventario, cantidad, precio, descripcion, idmoneda, idalmacen, iva, descuento, IEPS,  ivaRetenido) select " + Piddevolucion.ToString + ", cd.idinventario, cd.cantidad-(ifnull((select sum(ddc.cantidad) from tbldevolucionesdetallesc ddc inner join tbldevolucionescompras on ddc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idcompra=cd.idcompra and ddc.idinventario=cd.idinventario and tbldevolucionescompras.estado=3),0)), cd.precio/cd.cantidad*(cd.cantidad-(ifnull((select sum(ddc.cantidad) from tbldevolucionesdetallesc ddc inner join tbldevolucionescompras on ddc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idcompra=cd.idcompra and ddc.idinventario=cd.idinventario and tbldevolucionescompras.estado=3),0))), i.nombre, cd.idmoneda, cd.idalmacen, cd.iva, cd.descuento,  cd.IEPS,  cd.ivaRetenido " + _
            "from tblcomprasdetalles cd inner join tblinventario i on cd.idinventario=i.idinventario where i.inventariable=1 and idcompra=" + pIdDocumento.ToString + " and (cd.cantidad-(ifnull((select sum(ddc.cantidad) from tbldevolucionesdetallesc ddc inner join tbldevolucionescompras on ddc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idcompra=cd.idcompra and ddc.idinventario=cd.idinventario and tbldevolucionescompras.estado=3),0)))>0;" + _
            "insert into tbldevolucionescubicaciones (iddetalle, ubicacion, cantidad, surtido) select ddc.iddetalle, ubicacion, tcu.cantidad, 0 from tblcomprasubicaciones tcu inner join tblcomprasdetalles cd on cd.iddetalle=tcu.iddetalle inner join tbldevolucionesdetallesc ddc on ddc.idinventario=cd.idinventario and ddc.cantidad=cd.cantidad and ddc.precio=cd.precio where cd.idcompra=" + pIdDocumento.ToString() + " and ddc.iddevolucion=" + Piddevolucion.ToString()
            Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Sub RegresaInventario(ByVal pId As Integer)
        Comm.Transaction = Comm.Connection.BeginTransaction
        Try
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,cantidad,0,0,1) from tbldevolucionesdetallesc where iddevolucion=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventariolotesf(tbldevolucionesdetallesc.idinventario, tbldevolucionesdetallesc.idalmacen, tbldevolucionesclotes.surtido, 0, 0, 1, tbldevolucionesclotes.idlote) from tbldevolucionesclotes inner join tbldevolucionesdetallesc on tbldevolucionesclotes.iddetalle=tbldevolucionesdetallesc.iddetalle where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioaduanaf(tbldevolucionesdetallesc.idinventario, tbldevolucionesdetallesc.idalmacen, tbldevolucionescaduana.surtido, 0, 0, 1, tbldevolucionescaduana.idaduana) from tbldevolucionescaduana inner join tbldevolucionesdetallesc on tbldevolucionescaduana.iddetalle = tbldevolucionesdetallesc.iddetalle where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioubicacionesf(tbldevolucionesdetallesc.idinventario, tbldevolucionesdetallesc.idalmacen, tbldevolucionescubicaciones.surtido, 0, 0, 1, tbldevolucionescubicaciones.ubicacion) from tbldevolucionescubicaciones inner join tbldevolucionesdetallesc on tbldevolucionescubicaciones.iddetalle = tbldevolucionesdetallesc.iddetalle where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
            Comm.CommandText += "update tbldevolucionesclotes inner join tbldevolucionesdetallesc on tbldevolucionesclotes.iddetalle=tbldevolucionesdetallesc.iddetalle set tbldevolucionesclotes.surtido=0 where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
            Comm.CommandText += "update tbldevolucionescaduana inner join tbldevolucionesdetallesc on tbldevolucionescaduana.iddetalle=tbldevolucionesdetallesc.iddetalle set tbldevolucionescaduana.surtido=0 where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
            Comm.CommandText += "update tbldevolucionescubicaciones inner join tbldevolucionesdetallesc on tbldevolucionescubicaciones.iddetalle=tbldevolucionesdetallesc.iddetalle set tbldevolucionescubicaciones.surtido=0 where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
            Comm.Transaction.Commit()
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Comm.Transaction.Rollback()
            Throw ex
        End Try
    End Sub
    Public Sub ModificaInventario(ByVal pId As Integer)
        Comm.Transaction = Comm.Connection.BeginTransaction
        Try
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,cantidad,0,1,1) from tbldevolucionesdetallesc where iddevolucion=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventariolotesf(tbldevolucionesdetallesc.idinventario, tbldevolucionesdetallesc.idalmacen, tbldevolucionesclotes.cantidad-tbldevolucionesclotes.surtido, 0, 1, 1, tbldevolucionesclotes.idlote) from tbldevolucionesclotes inner join tbldevolucionesdetallesc on tbldevolucionesclotes.iddetalle = tbldevolucionesdetallesc.iddetalle where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + "; "
            Comm.CommandText += "select spmodificainventarioaduanaf(tbldevolucionesdetallesc.idinventario, tbldevolucionesdetallesc.idalmacen, tbldevolucionescaduana.cantidad-tbldevolucionescaduana.surtido, 0, 1, 1, tbldevolucionescaduana.idaduana) from tbldevolucionescaduana inner join tbldevolucionesdetallesc on tbldevolucionescaduana.iddetalle = tbldevolucionesdetallesc.iddetalle where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + "; "
            Comm.CommandText += "select spmodificainventarioubicacionesf(tbldevolucionesdetallesc.idinventario, tbldevolucionesdetallesc.idalmacen, tbldevolucionescubicaciones.cantidad-tbldevolucionescubicaciones.surtido, 0, 1, 1, tbldevolucionescubicaciones.ubicacion) from tbldevolucionescubicaciones inner join tbldevolucionesdetallesc on tbldevolucionescubicaciones.iddetalle = tbldevolucionesdetallesc.iddetalle where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + "; "
            Comm.CommandText += "update tbldevolucionesclotes inner join tbldevolucionesdetallesc on tbldevolucionesclotes.iddetalle=tbldevolucionesdetallesc.iddetalle set tbldevolucionesclotes.surtido=tbldevolucionesclotes.cantidad where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
            Comm.CommandText += "update tbldevolucionescaduana inner join tbldevolucionesdetallesc on tbldevolucionescaduana.iddetalle=tbldevolucionesdetallesc.iddetalle set tbldevolucionescaduana.surtido=tbldevolucionescaduana.cantidad where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
            Comm.CommandText += "update tbldevolucionescubicaciones inner join tbldevolucionesdetallesc on tbldevolucionescubicaciones.iddetalle=tbldevolucionesdetallesc.iddetalle set tbldevolucionescubicaciones.surtido=tbldevolucionescubicaciones.cantidad where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
            Comm.Transaction.Commit()
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Comm.Transaction.Rollback()
            Throw ex
        End Try
    End Sub

    Public Function DaIvas(ByVal piddevolucion As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iva,precio,idmoneda from tbldevolucionesdetallesc where iddevolucion=" + piddevolucion.ToString
        Return Comm.ExecuteReader
    End Function

    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pSoloCanceladas As Boolean, ByVal pMostrarEnPesos As Byte, ByVal pidInventario As Integer, pIdTipoSucursal As Integer, pidTipoProv As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select td.iddevolucion,td.fecha,td.folio as serie,0 as folio,tdd.cantidad,if(tdd.idmoneda=2,tdd.precio,tdd.precio*td.tipodecambio) as precio,td.estado,tdd.iva,tdd.ieps,tdd.ivaretenido,if(td.idcompra<>0,'FACTURAS','REMISIONES') as tipodev," + _
            "(select clave from tblinventario where tblinventario.idinventario=tdd.idinventario) as clave," + _
            "(select nombre from tblinventario where tblinventario.idinventario=tdd.idinventario) as nombre, " + _
            "if(td.idcompra<>0,(select tblcompras.serie from tblcompras where tblcompras.idcompra=td.idcompra),(select tblcomprasremisiones.serie from tblcomprasremisiones where tblcomprasremisiones.idremision=td.idremision)) as docserie," +
            "if(td.idcompra<>0,(select tblcompras.folioi from tblcompras where tblcompras.idcompra=td.idcompra),(select tblcomprasremisiones.folioi from tblcomprasremisiones where tblcomprasremisiones.idremision=td.idremision)) as docfolio" +
            " from tbldevolucionescompras as td inner join tbldevolucionesdetallesc as tdd on td.iddevolucion=tdd.iddevolucion inner join tblsucursales s on td.idsucursal=s.idsucursal inner join tblproveedores p on td.idproveedor=p.idproveedor where td.fecha>='" + pFecha1 + "' and td.fecha<='" + pFecha2 + "' "
        Else
            Comm.CommandText = "select td.iddevolucion,td.fecha,td.folio as serie,0 as folio,tdd.cantidad,tdd.precio as precio,td.estado,tdd.iva,tdd.ieps,tdd.ivaretenido,if(td.idcompra<>0,'FACTURAS','REMISIONES') as tipodev," + _
            "(select clave from tblinventario where tblinventario.idinventario=tdd.idinventario) as clave," + _
            "(select nombre from tblinventario where tblinventario.idinventario=tdd.idinventario) as nombre, " + _
            "if(td.idcompra<>0,(select tblcompras.serie from tblcompras where tblcompras.idcompra=td.idcompra),(select tblcomprasremisiones.serie from tblcomprasremisiones where tblcomprasremisiones.idremision=td.idremision)) as docserie," +
            "if(td.idcompra<>0,(select tblcompras.folioi from tblcompras where tblcompras.idcompra=td.idcompra),(select tblcomprasremisiones.folioi from tblcomprasremisiones where tblcomprasremisiones.idremision=td.idremision)) as docfolio" +
            " from tbldevolucionescompras as td inner join tbldevolucionesdetallesc as tdd on td.iddevolucion=tdd.iddevolucion inner join tblsucursales s on td.idsucursal=s.idsucursal inner join tblproveedores p on td.idproveedor=p.idproveedor where td.fecha>='" + pFecha1 + "' and td.fecha<='" + pFecha2 + "' "
        End If
        If pSoloCanceladas Then
            Comm.CommandText += " and td.estado=4"
        Else
            Comm.CommandText += " and td.estado=3"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and td.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pidTipoProv > 0 Then Comm.CommandText += " and p.idtipo=" + pidTipoProv.ToString
        If pIdProveedor > 0 Then
            Comm.CommandText += " and td.idproveedor=" + pIdProveedor.ToString
        End If
        If pidInventario > 1 Then
            Comm.CommandText += " and tdd.idinventario=" + pidInventario.ToString
        End If
        Comm.CommandText += " order by td.fecha,td.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevoluciones")
        'DS.WriteXmlSchema("tbldevoluciones.xml")
        Return DS.Tables("tbldevoluciones").DefaultView
    End Function
    Public Function ReportePorTipodePago(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, Optional ByVal pIdCliente As Integer = 0) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevolucionescompras.iddevolucion,tbldevolucionescompras.folio,tbldevolucionescompras.serie,tbldevolucionescompras.estado,if(tbldevolucionescompras.idconversion=2,tbldevolucionescompras.total,tbldevolucionescompras.total*tbldevolucionescompras.tipodecambio) as total,if(tbldevolucionescompras.idconversion=2,tbldevolucionescompras.totalapagar,tbldevolucionescompras.totalapagar*tbldevolucionescompras.tipodecambio) as totalapagar,tbldevolucionescompras.fecha,tbldevolucionescompras.tipodecambio,tbldevolucionescompras.idconversion,tbldevolucionesdetallesc.cantidad,tbldevolucionesdetallesc.descripcion,tbldevolucionesdetallesc.precio,0 as costoinv,0 as costopro,tbldevolucionesdetallesc.idinventario,tblformasdepago.nombre as formadepago,tblproveedores.nombre as cnombre " + _
        "from tbldevolucionescompras inner join tbldevolucionesdetallesc on tbldevolucionescompras.iddevolucion=tbldevolucionesdetallesc.iddevolucion inner join tblinventario on tbldevolucionesdetallesc.idinventario=tblinventario.idinventario inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tbldevolucionescompras.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idproveedor=" + pIdCliente.ToString
        End If
        Comm.CommandText += " order by tbldevolucionescompras.fecha,tbldevolucionescompras.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevolucionescompras")
        'DS.WriteXmlSchema("tbldevolucionescompras.xml")
        Return DS.Tables("tbldevolucionescompras").DefaultView
    End Function
    Public Function ReporteVentasArticulos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevolucionescompras.iddevolucion,tbldevolucionescompras.folio,tbldevolucionescompras.estado,tbldevolucionescompras.total,tbldevolucionescompras.totalapagar,tbldevolucionescompras.fecha,tbldevolucionescompras.tipodecambio,tbldevolucionescompras.idconversion,tbldevolucionesdetallesc.cantidad,tblinventario.nombre as descripcion,if(tbldevolucionesdetallesc.idmoneda=2,tbldevolucionesdetallesc.precio,tbldevolucionesdetallesc.precio*tbldevolucionescompras.tipodecambio) as precio,0 as costoinv,0 as costopro,tbldevolucionesdetallesc.idinventario,tbldevolucionesdetallesc.idvariante,tblformasdepago.tipo as formadepago,tblproveedores.nombre as cnombre,tbldevolucionesdetallesc.iva from tbldevolucionescompras inner join tbldevolucionesdetallesc on tbldevolucionescompras.iddevolucion=tbldevolucionesdetallesc.iddevolucion inner join tblinventario on tbldevolucionesdetallesc.idinventario=tblinventario.idinventario inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tbldevolucionescompras.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idproveedor=" + pIdCliente.ToString
        End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tbldevolucionesdetallesc.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            End If
            If pidClasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion.ToString
            End If
            If pidClasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion.ToString
            End If
        End If
        Comm.CommandText += " order by tbldevolucionescompras.fecha,tbldevolucionescompras.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevolucionescompras")
        'DS.WriteXmlSchema("tbldevolucionescompras.xml")
        Return DS.Tables("tbldevolucionescompras").DefaultView
    End Function
    Public Sub Aplicar(ByVal pId As Integer, ByVal pCantidad As Double, ByVal pSuma As Boolean)
        If pSuma Then
            Comm.CommandText = "update tbldevolucionescompras set credito=credito+" + pCantidad.ToString + " where iddevolucion=" + pId.ToString
        Else
            Comm.CommandText = "update tbldevolucionescompras set credito=credito-" + pCantidad.ToString + " where iddevolucion=" + pId.ToString
        End If
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaDatosDocumento(ByVal pIdDevolucion As Integer) As String
        Comm.CommandText = "select if(idcompra<>0," + _
        "ifnull((select concat('COMPRA-',serie,convert(folioi using utf8)) from tblcompras where tblcompras.idcompra=tbldevolucionescompras.idcompra),'')," + _
        "ifnull((select concat('REMISIÓN-',serie,convert(folioi using utf8)) from tblcomprasremisiones where tblcomprasremisiones.idremision=tbldevolucionescompras.idremision),'')) from tbldevolucionescompras where iddevolucion=" + pIdDevolucion.ToString
        Return Comm.ExecuteScalar
    End Function
End Class
