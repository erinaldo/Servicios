Imports MySql.Data.MySqlClient

Public Class dbMovimientos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdConcepto As Integer
    Public Fecha As String
    Public Concepto As dbInventarioConceptos
    Public Folio As Integer
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    Public Estado As Byte
    Public Desglosar As Byte
    Public IdSucursal As Integer
    Public Serie As String
    Public Comentario As String
    'Public TotalISR As Double
    Public Subtotal As Double
    'Public TotalIva As Double
    'Public TotalIvaRetenido As Double
    Public TotalVenta As Double

    Public HoraCancelado As String
    Public FechaCancelado As String
    Public TipodeCambio As Double
    Public IdMoneda As Integer
    Public IdVenta As Integer
    Public idRemision As Integer
    Public FolioRef As String
    Public ClienteRef As String
    Public ClienteClRef As String
    Public IdPedido As Integer
    Public Transito As Byte
    Public IdAlmacenO As Integer
    Public IdAlmacenD As Integer
    Public IdCliente As Integer
    Public IdPedidoV As Integer
    Private Structure CrearDesdeRem
        Dim Idinventario As Integer
        Dim Precio As Double
        Dim IdDetalle As Integer
    End Structure
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdConcepto = -1
        Fecha = ""
        Hora = ""
        Folio = 0
        Desglosar = 0
        TotalaPagar = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        Serie = ""
        HoraCancelado = ""
        FechaCancelado = ""
        IdPedido = 0
        Transito = 0
        IdAlmacenD = 0
        IdAlmacenO = 0
        Comm.Connection = Conexion
        Concepto = New dbInventarioConceptos(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblmovimientos where idmovimiento=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdConcepto = DReader("idconcepto")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            'Desglosar = DReader("desglosar")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            Serie = DReader("serie")
            HoraCancelado = DReader("horacancelado")
            FechaCancelado = DReader("fechacancelado")
            Comentario = DReader("comentario")
            TipodeCambio = DReader("tipodecambio")
            IdMoneda = DReader("idmoneda")
            IdVenta = DReader("idventa")
            idRemision = DReader("idremision")
            Transito = DReader("transito")
            IdPedido = DReader("idpedido")
            IdAlmacenD = DReader("idalmacend")
            IdAlmacenO = DReader("idalmaceno")
            IdPedidoV = DReader("idpedidov")
            IdCliente = DReader("idcliente")
        End If
        DReader.Close()
        If IdVenta <> 0 Then
            Comm.CommandText = "select ifnull((select concat(serie,convert(folio using utf8)) from tblventas where idventa=" + IdVenta.ToString + "),'')"
            FolioRef = "Factura: " + Comm.ExecuteScalar
            Comm.CommandText = "select ifnull((select nombre from tblclientes where idcliente=(select idcliente from tblventas where idventa=" + IdVenta.ToString + ")),'')"
            ClienteRef = Comm.ExecuteScalar
        End If
        If idRemision <> 0 Then
            Comm.CommandText = "select ifnull((select concat(serie,convert(folio using utf8)) from tblventasremisiones where idremision=" + idRemision.ToString + "),'')"
            FolioRef = "Remisión: " + Comm.ExecuteScalar
            Comm.CommandText = "select ifnull((select nombre from tblclientes where idcliente=(select idcliente from tblventasremisiones where idremision=" + idRemision.ToString + ")),'')"
            ClienteRef = Comm.ExecuteScalar
        End If
        If IdPedido <> 0 Then
            Comm.CommandText = "select ifnull((select concat(serie,convert(folio using utf8)) from tblinventariopedidos where idpedido=" + IdPedido.ToString + "),'')"
            FolioRef = "Pedido: " + Comm.ExecuteScalar
            Comm.CommandText = "select ifnull((select nombre from tblsucursales where idsucursal=(select idsucursala from tblinventariopedidos where idpedido=" + IdPedido.ToString + ")),'')"
            ClienteRef = Comm.ExecuteScalar
        End If
        If IdPedidoV <> 0 Then
            Comm.CommandText = "select ifnull((select concat(serie,convert(folio using utf8)) from tblventaspedidos where idpedido=" + IdPedidoV.ToString + "),'')"
            FolioRef = "Pedido: " + Comm.ExecuteScalar
            Comm.CommandText = "select ifnull((select nombre from tblclientes where idcliente=(select idcliente from tblventaspedidos where idpedido=" + IdPedidoV.ToString + ")),'')"
            ClienteRef = Comm.ExecuteScalar
        End If
        If IdCliente <> 0 Then
            Comm.CommandText = "select ifnull((select nombre from tblclientes where idcliente=" + IdCliente.ToString + "),0)"
            ClienteRef = Comm.ExecuteScalar
            Comm.CommandText = "select ifnull((select clave from tblclientes where idcliente=" + IdCliente.ToString + "),0)"
            ClienteClRef = Comm.ExecuteScalar
        End If
        Concepto = New dbInventarioConceptos(IdConcepto, Comm.Connection)
    End Sub

    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer, ByVal pIdConcepto As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblmovimientos where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado>=2) and idsucursal=" + pidSucursal.ToString + " and idconcepto=" + pIdConcepto.ToString + "),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String, ByVal pidSucursal As Integer, ByVal pIdConcepto As Integer) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblmovimientos where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2 and idsucursal=" + pidSucursal.ToString + " and idconcepto=" + pIdConcepto.ToString
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    Public Sub Guardar(ByVal pFolio As Integer, ByVal pFecha As String, ByVal pIdConcepto As Integer, ByVal pSerie As String, ByVal pidSucursal As Integer, ByVal pTipodeCambio As Double, ByVal pIdMoneda As Integer, pIdAlmacenO As Integer, pIdAlmacenD As Integer, pIdPedidoV As Integer, pIdCliente As Integer)
        Folio = pFolio
        Fecha = pFecha
        IdConcepto = pIdConcepto
        Serie = pSerie
        IdSucursal = pidSucursal
        TipodeCambio = pTipodeCambio
        IdMoneda = pIdMoneda
        Comm.CommandText = "insert into tblmovimientos(folio,fecha,estado,idconcepto,comentario,serie,idsucursal,total,totalapagar,fechacancelado,horacancelado,hora,tipodecambio,idmoneda,idventa,idremision,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,transito,idpedido,idalmaceno,idalmacend,idcliente,idpedidov) values(" + _
        Folio.ToString + ",'" + Fecha + "',1," + IdConcepto.ToString + ",'','" + Replace(Trim(Serie), "'", "''") + "'," + IdSucursal.ToString + ",0,0,'" + Fecha + "','" + Format(TimeOfDay, "HH:mm:ss") + "','" + Format(TimeOfDay, "HH:mm:ss") + "'," + TipodeCambio.ToString + "," + IdMoneda.ToString + ",0,0," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:ss:mm") + "',0,0," + pIdAlmacenO.ToString + "," + pIdAlmacenD.ToString + "," + pIdCliente.ToString + "," + pIdPedidoV.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull(last_insert_id(),0);"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub GuardarImp(ByVal pFolio As Integer, ByVal pFecha As String, ByVal pIdConcepto As Integer, ByVal pSerie As String, ByVal pidSucursal As Integer, ByVal pTipodeCambio As Double, ByVal pIdMoneda As Integer, pTotal As Double, pIdAlmacenD As Integer, pIdAlmacenO As Integer, pIdPedidov As Integer, pIdCliente As Integer)
        Folio = pFolio
        Fecha = pFecha
        IdConcepto = pIdConcepto
        Serie = pSerie
        IdSucursal = pidSucursal
        TipodeCambio = pTipodeCambio
        IdMoneda = pIdMoneda
        Hora = Format(TimeOfDay, "HH:mm:ss")
        IdAlmacenO = pIdAlmacenO
        IdAlmacenD = pIdAlmacenD
        Comm.CommandText = "insert into tblmovimientos(folio,fecha,estado,idconcepto,comentario,serie,idsucursal,total,totalapagar,fechacancelado,horacancelado,hora,tipodecambio,idmoneda,idventa,idremision,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,transito,idpedido,idalmaceno,idalmacend,idcliente,idpedidov) values(" + _
        Folio.ToString() + ",'" + Fecha + "',3," + IdConcepto.ToString() + ",'','" + Replace(Trim(Serie), "'", "''") + "'," + IdSucursal.ToString() + "," + pTotal.ToString() + "," + pTotal.ToString() + ",'" + Fecha + "','" + Hora + "','" + Hora + "'," + TipodeCambio.ToString() + "," + IdMoneda.ToString() + ",0,0," + GlobalIdUsuario.ToString() + ",'" + Fecha + "','" + Hora + "'," + GlobalIdUsuario.ToString() + ",'" + Fecha + "','" + Hora + "',1,0," + IdAlmacenO.ToString() + "," + IdAlmacenD.ToString() + "," + pIdCliente.ToString() + "," + pIdPedidov.ToString() + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull(last_insert_id(),0);"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFolio As Integer, ByVal pEstado As Byte, ByVal pComentario As String, ByVal pSerie As String, ByVal pTotal As Double, ByVal pTotalaPagar As Double, ByVal pTipodeCambio As Double, ByVal pidMoneda As Integer, ByVal pfecha As String, ByVal pIdVenta As Integer, ByVal pIdRemision As Integer, pIdPedido As Integer, pIdPedidoV As Integer, pIdCliente As Integer)
        Folio = pFolio
        Estado = pEstado
        Comentario = pComentario
        Serie = pSerie
        Total = pTotal
        TotalaPagar = pTotalaPagar
        TipodeCambio = pTipodeCambio
        IdMoneda = pidMoneda
        Fecha = pfecha
        IdVenta = pIdVenta
        idRemision = pIdRemision
        IdPedido = pIdPedido
        Comm.CommandText = "update tblmovimientos set fecha='" + Fecha + "',folio=" + Folio.ToString + ",estado=" + Estado.ToString + ",comentario='" + Trim(Replace(Comentario, "'", "''")) + "',serie='" + Replace(Trim(Serie), "'", "''") + "',total=" + Total.ToString + ",totalapagar=" + TotalaPagar.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',tipodecambio=" + TipodeCambio.ToString + ",idmoneda=" + IdMoneda.ToString + ",idventa=" + pIdVenta.ToString + ",idremision=" + pIdRemision.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "',idpedido=" + IdPedido.ToString + ",idpedidov=" + pIdPedidoV.ToString + ",idcliente=" + pIdCliente.ToString + " where idmovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer, ByVal pTipoCosteo As Byte, ByVal pTipodeCambio As Double, ByVal pCosteoTiempoReal As Byte)
        RegresaInventario(pID, pTipoCosteo, pTipodeCambio, pCosteoTiempoReal)
        Comm.CommandText = "delete from tblmovimientosentrega where idmovimiento=" + pID.ToString() + "; delete from tblmovimientos where idmovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Recibir(pid As Integer)
        'Comm.CommandText = "update tblalmacenesiubicaciones inner join tblmovimientosdetalles on tblmovimientosubicaciones.iddetalle=tblmovimientosdetalles.iddetalle set  where idmovimiento=" + pid.ToString
        Comm.CommandText = "update tblmovimientos set transito=1 where idmovimiento=" + pid.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitarBoletas(ByVal pidMovimiento As Integer)
        Dim Ids As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iddetalle from tblmovimientosdetalles where idmovimiento=" + pidMovimiento.ToString
        DR = Comm.ExecuteReader
        While DR.Read
            Ids.Add(DR("iddetalle"))
        End While
        DR.Close()
        For Each Idd As Integer In Ids
            Comm.CommandText = "delete from tblsemillasboletas where iddetalle=" + Idd.ToString
            Comm.ExecuteNonQuery()
        Next
    End Sub
    Public Function TieneBoletas(pIdMovimiento As Integer) As Boolean
        Dim Ids As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim Sihay As Boolean = False
        Comm.CommandText = "select iddetalle from tblmovimientosdetalles where idmovimiento=" + pIdMovimiento.ToString
        DR = Comm.ExecuteReader
        While DR.Read
            Ids.Add(DR("iddetalle"))
        End While
        DR.Close()
        For Each Idd As Integer In Ids
            Comm.CommandText = "select id from tblsemillasboletas where iddetalle=" + Idd.ToString
            DR = Comm.ExecuteReader
            If DR.Read Then
                Sihay = True
            End If
            DR.Close()
        Next
        Return Sihay
    End Function
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pEstado As Byte, ByVal pFolio As String, ByVal pIdConcepto As Integer, ByVal pidSucursal As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblmovimientos.idmovimiento,tblmovimientos.fecha,tblmovimientos.serie,tblmovimientos.folio,tblinventarioconceptos.nombre,case tblmovimientos.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado  from tblmovimientos inner join tblinventarioconceptos on tblmovimientos.idconcepto=tblinventarioconceptos.idconcepto where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblmovimientos.serie,convert(tblmovimientos.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado > 1 Then
            Comm.CommandText += " and tblmovimientos.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblmovimientos.estado<>1"
        End If
        If pIdConcepto > 0 Then
            Comm.CommandText += " and tblmovimientos.idconcepto=" + pIdConcepto.ToString
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblmovimientos.idsucursal=" + pidSucursal.ToString
        End If
        Comm.CommandText += " order by tblmovimientos.fecha desc,tblmovimientos.serie,tblmovimientos.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmovimientos")
        Return DS.Tables("tblmovimientos").DefaultView
    End Function
    'Public Function DaIvas(ByVal pIdCotizacion As Integer) As MySql.Data.MySqlClient.MySqlDataReader
    '    Comm.CommandText = "select iva,precio,idmoneda from tblventascotizacionesinventario where idcotizacion=" + pIdCotizacion.ToString
    '    Return Comm.ExecuteReader
    'End Function

    Public Function DaTotal(ByVal pidMovimiento As Integer, ByVal pidMoneda As Integer) As Double
        Dim IDs As New Collection
        Dim Total As Double = 0
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double = 1
        Subtotal = 0
        TotalVenta = 0
        Comm.CommandText = "select tipodecambio from tblmovimientos where idmovimiento=" + pidMovimiento.ToString
        iTipoCambio = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        If pidMoneda = 2 Then
            Comm.CommandText = "select ifnull((select sum(if(idmoneda=2,precio,precio*" + iTipoCambio.ToString + ")) from tblmovimientosdetalles where idmovimiento=" + pidMovimiento.ToString + "),0)"
        Else
            Comm.CommandText = "select ifnull((select sum(if(idmoneda<>2,precio,precio/" + iTipoCambio.ToString + ")) from tblmovimientosdetalles where idmovimiento=" + pidMovimiento.ToString + "),0)"
        End If
        Subtotal = Comm.ExecuteScalar

        TotalVenta = Subtotal '+ TotalIva - TotalISR - TotalIvaRetenido
        Return TotalVenta
    End Function

    Public Sub AgregarDetallesReferencia(ByVal PidMovimiento As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte, pIdAlmacen As Integer, pIdalmacen2 As Integer)
        '0 cotizacion
        '1 pedido inventario
        '2 remision
        '3 ventas
        If Tipo = 1 Then
            Comm.CommandText = "insert into tblmovimientosdetalles(idinventario,cantidad,precio,idmovimiento,descripcion,idalmacen,idalmacen2,idvariante,surtido,inventarioanterior,idmoneda) " + _
             "select ipd.idinventario," + _
             "sum(ipd.cantidadaut)-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idpedido=ipd.idpedido and tblmovimientosdetalles.idinventario=ipd.idinventario and tblmovimientos.estado=3),0))," + _
             "round((ipd.precio/ipd.cantidadaut)*(sum(ipd.cantidadaut)-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idpedido=ipd.idpedido and tblmovimientosdetalles.idinventario=ipd.idinventario and tblmovimientos.estado=3),0))),2)," + _
             PidMovimiento.ToString + "," + _
             "i.nombre," + pIdAlmacen.ToString + "," + pIdalmacen2.ToString + ",1,0,0,2 from tblinventariopedidosdetalles ipd inner join tblinventario i on ipd.idinventario=i.idinventario where i.inventariable=1 and ipd.idpedido=" + pIdDocumento.ToString + " and (ifnull((select sum(vi.cantidadaut) from tblinventariopedidosdetalles vi where vi.idpedido=" + pIdDocumento.ToString + " and vi.idinventario=ipd.idinventario),0)-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idpedido=ipd.idpedido and tblmovimientosdetalles.idinventario=ipd.idinventario and tblmovimientos.estado=3),0)))>0 and ipd.autorizado=1 group by ipd.idinventario"
            Comm.ExecuteNonQuery()
        End If
        If Tipo = 2 Then
            Comm.CommandText = "insert into tblmovimientosdetalles(idinventario,cantidad,precio,idmovimiento,descripcion,idalmacen,idalmacen2,idvariante,surtido,inventarioanterior,idmoneda) " + _
             "select tblventasremisionesinventario.idinventario," + _
             "sum(tblventasremisionesinventario.cantidad)-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=tblventasremisionesinventario.idremision and tblmovimientosdetalles.idinventario=tblventasremisionesinventario.idinventario and tblmovimientos.estado=3),0))," + _
             "round((tblventasremisionesinventario.precio/tblventasremisionesinventario.cantidad*(1+(tblventasremisionesinventario.iva-tblventasremisionesinventario.ivaretenido-tblventasremisionesinventario.ieps)/100))*(tblventasremisionesinventario.cantidad-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=tblventasremisionesinventario.idremision and tblmovimientosdetalles.idinventario=tblventasremisionesinventario.idinventario and tblmovimientos.estado=3),0))),2)," + _
             PidMovimiento.ToString + "," + _
             "tblventasremisionesinventario.descripcion,tblventasremisionesinventario.idalmacen,tblventasremisionesinventario.idalmacen,tblventasremisionesinventario.idvariante,0,0,tblventasremisionesinventario.idmoneda from tblventasremisionesinventario inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario where tblinventario.inventariable=1 and idremision=" + pIdDocumento.ToString + " and (ifnull((select sum(vi.cantidad) from tblventasremisionesinventario vi where vi.idremision=" + pIdDocumento.ToString + " and vi.idinventario=tblventasremisionesinventario.idinventario),0)-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=tblventasremisionesinventario.idremision and tblmovimientosdetalles.idinventario=tblventasremisionesinventario.idinventario and tblmovimientos.estado=3),0)))>0 group by tblventasremisionesinventario.idinventario"
            Comm.ExecuteNonQuery()

            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            Dim Detalles As New Collection
            Dim Detalle As CrearDesdeRem
            Comm.CommandText = "select idinventario,precio,iddetalle from tblmovimientosdetalles where idmovimiento=" + PidMovimiento.ToString
            DR = Comm.ExecuteReader
            While DR.Read
                Detalle.IdDetalle = DR("iddetalle")
                Detalle.Idinventario = DR("idinventario")
                Detalle.Precio = DR("precio")
                Detalles.Add(Detalle)
            End While
            DR.Close()
            For Each Det As CrearDesdeRem In Detalles
                Comm.CommandText = "insert into tblmovimientoslotes(idlote,iddetalle,cantidad,surtido) select tblventasremisioneslotes.idlote," + Det.IdDetalle.ToString + ",tblventasremisioneslotes.cantidad,0 from tblventasremisioneslotes inner join tblventasremisionesinventario ri on tblventasremisioneslotes.iddetalle=ri.iddetalle where " +
                    " round(ri.precio*(1+(ri.iva+ri.ieps-ri.ivaretenido)/100),2)=" + Det.Precio.ToString + " and ri.idinventario=" + Det.Idinventario.ToString + " and ri.idremision=" + pIdDocumento.ToString
                Comm.ExecuteNonQuery()
            Next
            For Each Det As CrearDesdeRem In Detalles
                Comm.CommandText = "insert into tblmovimientosaduana(idaduana,iddetalle,cantidad,surtido) select tblventasremisionesaduana.idaduana," + Det.IdDetalle.ToString + ",tblventasremisionesaduana.cantidad,0 from tblventasremisionesaduana inner join tblventasremisionesinventario ri on tblventasremisionesaduana.iddetalle=ri.iddetalle where " +
                    " round(ri.precio*(1+(ri.iva+ri.ieps-ri.ivaretenido)/100),2)=" + Det.Precio.ToString + " and ri.idinventario=" + Det.Idinventario.ToString + " and ri.idremision=" + pIdDocumento.ToString
                Comm.ExecuteNonQuery()
            Next
            For Each Det As CrearDesdeRem In Detalles
                Comm.CommandText = "insert into tblmovimientosubicaciones(ubicacion,iddetalle,cantidad,surtido,ubicaciond) select tblventasremisionesubicaciones.ubicacion," + Det.IdDetalle.ToString + ",tblventasremisionesubicaciones.cantidad,0,'' from tblventasremisionesubicaciones inner join tblventasremisionesinventario ri on tblventasremisionesubicaciones.iddetalle=ri.iddetalle where " +
                    " round(ri.precio*(1+(ri.iva+ri.ieps-ri.ivaretenido)/100),2)=" + Det.Precio.ToString + " and ri.idinventario=" + Det.Idinventario.ToString + " and ri.idremision=" + pIdDocumento.ToString
                Comm.ExecuteNonQuery()
            Next
        End If
        If Tipo = 3 Then
            '(CDbl(TextBox5.Text) * Equivalenciab) / Equivalencia
            Comm.CommandText = "insert into tblmovimientosdetalles(idinventario, cantidad, precio, idmovimiento, descripcion, idalmacen, idalmacen2, idvariante, surtido, inventarioanterior, idmoneda) select tblventasinventario.idinventario, sum(tblventasinventario.cantidad)-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=tblventasinventario.idventa and tblmovimientosdetalles.idinventario=tblventasinventario.idinventario and tblmovimientos.estado=3),0)), round(tblventasinventario.precio/tblventasinventario.cantidad*((1+(tblventasinventario.iva-tblventasinventario.ivaretenido-tblventasinventario.ieps)/100))*(tblventasinventario.cantidad-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=tblventasinventario.idventa and tblmovimientosdetalles.idinventario=tblventasinventario.idinventario and tblmovimientos.estado=3),0))),2)," + PidMovimiento.ToString + "," + "tblventasinventario.descripcion,tblventasinventario.idalmacen,tblventasinventario.idalmacen,tblventasinventario.idvariante,0,0,tblventasinventario.idmoneda from tblventasinventario inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario where tblinventario.inventariable=1 and idventa=" + pIdDocumento.ToString + " and (ifnull((select sum(vi.cantidad) from tblventasinventario vi where vi.idventa=tblventasinventario.idventa and vi.idinventario=tblventasinventario.idinventario),0)-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=" + pIdDocumento.ToString + " and tblmovimientosdetalles.idinventario=tblventasinventario.idinventario and tblmovimientos.estado=3),0)))>0 group by tblventasinventario.idinventario;"
            Comm.ExecuteNonQuery()

            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            Dim Detalles As New Collection
            Dim Detalle As CrearDesdeRem
            Comm.CommandText = "select idinventario,precio,iddetalle from tblmovimientosdetalles where idmovimiento=" + PidMovimiento.ToString
            DR = Comm.ExecuteReader
            While DR.Read
                Detalle.IdDetalle = DR("iddetalle")
                Detalle.Idinventario = DR("idinventario")
                Detalle.Precio = DR("precio")
                Detalles.Add(Detalle)
            End While
            DR.Close()
            For Each Det As CrearDesdeRem In Detalles
                Comm.CommandText = "insert into tblmovimientoslotes(idlote,iddetalle,cantidad,surtido) select tblventaslotes.idlote," + Det.IdDetalle.ToString + ",tblventaslotes.cantidad,0 from tblventaslotes inner join tblventasinventario ri on tblventaslotes.iddetalle=ri.idventasinventario where " +
                    " round(ri.precio*(1+(ri.iva+ri.ieps-ri.ivaretenido)/100),2)=" + Det.Precio.ToString + " and ri.idinventario=" + Det.Idinventario.ToString + " and ri.idventa=" + pIdDocumento.ToString
                Comm.ExecuteNonQuery()
            Next
            For Each Det As CrearDesdeRem In Detalles
                Comm.CommandText = "insert into tblmovimientosaduana(idaduana,iddetalle,cantidad,surtido) select tblventasaduanan.idaduana," + Det.IdDetalle.ToString + ",tblventasaduanan.cantidad,0 from tblventasaduanan inner join tblventasinventario ri on tblventasaduanan.iddetalle=ri.idventasinventario where " +
                    " round(ri.precio*(1+(ri.iva+ri.ieps-ri.ivaretenido)/100),2)=" + Det.Precio.ToString + " and ri.idinventario=" + Det.Idinventario.ToString + " and ri.idventa=" + pIdDocumento.ToString
                Comm.ExecuteNonQuery()
            Next
            For Each Det As CrearDesdeRem In Detalles
                Comm.CommandText = "insert into tblmovimientosubicaciones(ubicacion,iddetalle,cantidad,surtido,ubicaciond) select tblventasubicaciones.ubicacion," + Det.IdDetalle.ToString + ",tblventasubicaciones.cantidad,0,'' from tblventasubicaciones inner join tblventasinventario ri on tblventasubicaciones.iddetalle=ri.idventasinventario where " +
                    " round(ri.precio*(1+(ri.iva+ri.ieps-ri.ivaretenido)/100),2)=" + Det.Precio.ToString + " and ri.idinventario=" + Det.Idinventario.ToString + " and ri.idventa=" + pIdDocumento.ToString
                Comm.ExecuteNonQuery()
            Next

        End If
    End Sub

    Public Sub RegresaInventario(ByVal pId As Integer, ByVal pTipoCosteo As Byte, ByVal PTipodeCambio As Double, ByVal pCosteoTiempoREal As Byte)
        Dim Tipo As Integer
        Comm.CommandText = "select tblinventarioconceptos.tipo from tblinventarioconceptos inner join tblmovimientos on tblinventarioconceptos.idconcepto=tblmovimientos.idconcepto where tblmovimientos.idmovimiento=" + pId.ToString
        Tipo = Comm.ExecuteScalar
        If Tipo = dbInventarioConceptos.Tipos.Entrada Or Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,surtido,0,1,1) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"

            'lotes
            Comm.CommandText += "select spmodificainventariolotesf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen, tblmovimientoslotes.surtido, 0, 1, 1, tblmovimientoslotes.idlote) from tblmovimientosdetalles inner join tblmovimientoslotes on tblmovimientosdetalles.iddetalle = tblmovimientoslotes.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'aduana
            Comm.CommandText += "select spmodificainventarioaduanaf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen, tblmovimientosaduana.surtido, 0, 1, 1, tblmovimientosaduana.idaduana) from tblmovimientosdetalles inner join tblmovimientosaduana on tblmovimientosdetalles.iddetalle=tblmovimientosaduana.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'ubicaciones
            Comm.CommandText += "select spmodificainventarioubicacionesf(d.idinventario, d.idalmacen, u.surtido, 0, 1, 1, u.ubicacion) from tblmovimientosdetalles d inner join tblmovimientosubicaciones u on d.iddetalle = u.iddetalle where d.idmovimiento=" + pId.ToString + ";"

            Comm.ExecuteNonQuery()
        ElseIf Tipo = dbInventarioConceptos.Tipos.Salida Then
            Comm.CommandText = "select spmodificainventarioi(idinventario, idalmacen, surtido, 0, 0, 1) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"

            'lotes
            Comm.CommandText += "select spmodificainventariolotesf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen, tblmovimientoslotes.surtido, 0, 0, 1, tblmovimientoslotes.idlote) from tblmovimientosdetalles inner join tblmovimientoslotes on tblmovimientosdetalles.iddetalle=tblmovimientoslotes.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'aduana
            Comm.CommandText += "select spmodificainventarioaduanaf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen, tblmovimientosaduana.surtido, 0, 0, 1, tblmovimientosaduana.idaduana) from tblmovimientosdetalles inner join tblmovimientosaduana on tblmovimientosdetalles.iddetalle=tblmovimientosaduana.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'ubicaciones
            Comm.CommandText += "select spmodificainventarioubicacionesf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen, tblmovimientosubicaciones.surtido, 0, 0, 1, tblmovimientosubicaciones.ubicacion) from tblmovimientosdetalles inner join tblmovimientosubicaciones on tblmovimientosdetalles.iddetalle=tblmovimientosubicaciones.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            Comm.ExecuteNonQuery()
        ElseIf Tipo = dbInventarioConceptos.Tipos.Traspaso Then
            Comm.CommandText = "select spmodificainventarioi(idinventario, idalmacen, surtido, 0, 0, 1) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioi(idinventario, idalmacen2, surtido, 0, 1, 1) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"

            'lotes
            Comm.CommandText += "select spmodificainventariolotesf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen, tblmovimientoslotes.surtido, 0, 0, 1, tblmovimientoslotes.idlote) from tblmovimientosdetalles inner join tblmovimientoslotes on tblmovimientosdetalles.iddetalle = tblmovimientoslotes.iddetalle  where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventariolotesf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen2, tblmovimientoslotes.surtido, 0, 1, 1, tblmovimientoslotes.idlote) from tblmovimientosdetalles inner join tblmovimientoslotes on tblmovimientosdetalles.iddetalle = tblmovimientoslotes.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'aduana
            Comm.CommandText += "select spmodificainventarioaduanaf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen2, tblmovimientosaduana.surtido, 0, 1, 1, tblmovimientosaduana.idaduana) from tblmovimientosdetalles inner join tblmovimientosaduana on tblmovimientosdetalles.iddetalle = tblmovimientosaduana.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioaduanaf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen, tblmovimientosaduana.surtido, 0, 0, 1, tblmovimientosaduana.idaduana) from tblmovimientosdetalles inner join tblmovimientosaduana on tblmovimientosdetalles.iddetalle=tblmovimientosaduana.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'ubicaciones
            Comm.CommandText += "select spmodificainventarioubicacionesf(d.idinventario, d.idalmacen, u.surtido, 0, 0, 1, u.ubicacion) from tblmovimientosdetalles d inner join tblmovimientosubicaciones u on d.iddetalle = u.iddetalle  where d.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioubicacionesf(d.idinventario, d.idalmacen2, u.surtido, 0, 1, 1, u.ubicaciond) from tblmovimientosdetalles d inner join tblmovimientosubicaciones u on d.iddetalle = u.iddetalle where d.idmovimiento=" + pId.ToString + ";"

            Comm.ExecuteNonQuery()
        ElseIf Tipo = dbInventarioConceptos.Tipos.Ajuste Then
            'Comm.CommandText = "select if(idinventario>1,spajustainventarioi(idinventario,idalmacen,inventarioanterior),0),if(idvariante>1,spajustainventariop(idvariante,idalmacen,inventarioanterior),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            'Comm.ExecuteNonQuery()
            Comm.CommandText = "select spmodificainventarioi(idinventario, idalmacen, inventarioanterior, 0, 0, 1) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Sub ModificaInventario(ByVal pId As Integer, ByVal PTipoCosteo As Byte, ByVal pTipodeCambio As Double)

        Dim Tipo As Integer
        Comm.CommandText = "select tblinventarioconceptos.tipo from tblinventarioconceptos inner join tblmovimientos on tblinventarioconceptos.idconcepto=tblmovimientos.idconcepto where tblmovimientos.idmovimiento=" + pId.ToString
        Tipo = Comm.ExecuteScalar
        If Tipo = dbInventarioConceptos.Tipos.Entrada Or Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
            Comm.CommandText = "select spmodificainventarioi(idinventario, idalmacen, cantidad-surtido, 0, 0, 1) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString + ";"

            'lotes
            Comm.CommandText += "select spmodificainventariolotesf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen, tblmovimientoslotes.cantidad-tblmovimientoslotes.surtido, 0, 0, 1, tblmovimientoslotes.idlote) from tblmovimientosdetalles inner join tblmovimientoslotes on tblmovimientosdetalles.iddetalle=tblmovimientoslotes.iddetalle where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientoslotes inner join tblmovimientosdetalles on tblmovimientoslotes.iddetalle=tblmovimientosdetalles.iddetalle set tblmovimientoslotes.surtido=tblmovimientoslotes.cantidad where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'aduana
            Comm.CommandText += "select spmodificainventarioaduanaf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen, tblmovimientosaduana.cantidad-tblmovimientosaduana.surtido, 0, 0, 1, tblmovimientosaduana.idaduana) from tblmovimientosdetalles inner join tblmovimientosaduana on tblmovimientosdetalles.iddetalle=tblmovimientosaduana.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosaduana inner join tblmovimientosdetalles on tblmovimientosaduana.iddetalle=tblmovimientosdetalles.iddetalle set tblmovimientosaduana.surtido=tblmovimientosaduana.cantidad where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'ubicaciones
            Comm.CommandText += "select spmodificainventarioubicacionesf(d.idinventario, d.idalmacen, u.cantidad-u.surtido, 0, 0, 1, u.ubicacion) from tblmovimientosdetalles d inner join tblmovimientosubicaciones u on d.iddetalle=u.iddetalle where d.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosubicaciones inner join tblmovimientosdetalles on tblmovimientosubicaciones.iddetalle = tblmovimientosdetalles.iddetalle set tblmovimientosubicaciones.surtido = tblmovimientosubicaciones.cantidad where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            Comm.ExecuteNonQuery()
        End If
        If Tipo = dbInventarioConceptos.Tipos.Salida Then
            Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,cantidad-surtido,0,1,1) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString + ";"

            'lotes
            Comm.CommandText += "select spmodificainventariolotesf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen, tblmovimientoslotes.cantidad-tblmovimientoslotes.surtido, 0, 1, 1, tblmovimientoslotes.idlote) from tblmovimientosdetalles inner join tblmovimientoslotes on tblmovimientosdetalles.iddetalle=tblmovimientoslotes.iddetalle where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles inner join tblmovimientoslotes on tblmovimientosdetalles.iddetalle = tblmovimientoslotes.iddetalle set tblmovimientoslotes.surtido = tblmovimientoslotes.cantidad where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'aduana
            Comm.CommandText += "select spmodificainventarioaduanaf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen, tblmovimientosaduana.cantidad-tblmovimientosaduana.surtido, 0, 1, 1, tblmovimientosaduana.idaduana) from tblmovimientosdetalles inner join tblmovimientosaduana on tblmovimientosdetalles.iddetalle = tblmovimientosaduana.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles inner join tblmovimientosaduana on tblmovimientosdetalles.iddetalle = tblmovimientosaduana.iddetalle set tblmovimientosaduana.surtido=tblmovimientosaduana.cantidad where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'ubicaciones
            Comm.CommandText += "select spmodificainventarioubicacionesf(d.idinventario, d.idalmacen, u.cantidad-u.surtido, 0, 1, 1, u.ubicacion) from tblmovimientosdetalles d inner join tblmovimientosubicaciones u on d.iddetalle = u.iddetalle where d.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles inner join tblmovimientosubicaciones on tblmovimientosdetalles.iddetalle = tblmovimientosubicaciones.iddetalle set tblmovimientosubicaciones.surtido = tblmovimientosubicaciones.cantidad where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            Comm.ExecuteNonQuery()
        End If
        If Tipo = dbInventarioConceptos.Tipos.Traspaso Then
            Comm.CommandText = "select spmodificainventarioi(idinventario, idalmacen, cantidad-surtido, 0, 1, 1) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioi(idinventario, idalmacen2, cantidad-surtido, 0, 0, 1) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString + ";"

            'lotes
            Comm.CommandText += "select spmodificainventariolotesf(tblmovimientosdetalles.idinventario,tblmovimientosdetalles.idalmacen, tblmovimientoslotes.cantidad-tblmovimientoslotes.surtido, 0, 1, 1, tblmovimientoslotes.idlote) from tblmovimientosdetalles inner join tblmovimientoslotes on tblmovimientosdetalles.iddetalle = tblmovimientoslotes.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventariolotesf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen2, tblmovimientoslotes.cantidad-tblmovimientoslotes.surtido, 0, 0, 1, tblmovimientoslotes.idlote) from tblmovimientosdetalles inner join tblmovimientoslotes on tblmovimientosdetalles.iddetalle = tblmovimientoslotes.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles inner join tblmovimientoslotes on tblmovimientosdetalles.iddetalle=tblmovimientoslotes.iddetalle set tblmovimientoslotes.surtido=tblmovimientoslotes.cantidad where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'aduana
            Comm.CommandText += "select spmodificainventarioaduanaf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen ,tblmovimientosaduana.cantidad-tblmovimientosaduana.surtido, 0, 1, 1, tblmovimientosaduana.idaduana) from tblmovimientosdetalles inner join tblmovimientosaduana on tblmovimientosdetalles.iddetalle = tblmovimientosaduana.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioaduanaf(tblmovimientosdetalles.idinventario, tblmovimientosdetalles.idalmacen2, tblmovimientosaduana.cantidad-tblmovimientosaduana.surtido, 0, 0, 1, tblmovimientosaduana.idaduana) from tblmovimientosdetalles inner join tblmovimientosaduana on tblmovimientosdetalles.iddetalle = tblmovimientosaduana.iddetalle where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles inner join tblmovimientosaduana on tblmovimientosdetalles.iddetalle=tblmovimientosaduana.iddetalle set tblmovimientosaduana.surtido=tblmovimientosaduana.cantidad where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            'ubicaciones
            Comm.CommandText += "select spmodificainventarioubicacionesf(d.idinventario,d.idalmacen, u.cantidad-u.surtido, 0, 1, 1, u.ubicacion) from tblmovimientosdetalles d inner join tblmovimientosubicaciones u on d.iddetalle = u.iddetalle where d.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "select spmodificainventarioubicacionesf(d.idinventario, d.idalmacen2, u.cantidad-u.surtido, 0, 0, 1, u.ubicaciond) from tblmovimientosdetalles d inner join tblmovimientosubicaciones u on d.iddetalle = u.iddetalle where d.idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles inner join tblmovimientosubicaciones on tblmovimientosdetalles.iddetalle=tblmovimientosubicaciones.iddetalle set tblmovimientosubicaciones.surtido=tblmovimientosubicaciones.cantidad where tblmovimientosdetalles.idmovimiento=" + pId.ToString + ";"

            Comm.ExecuteNonQuery()
        End If
        If Tipo = dbInventarioConceptos.Tipos.Ajuste Then
            Comm.CommandText = "update tblmovimientosdetalles set inventarioanterior=spdainventario(idinventario,idalmacen,0)-cantidad where idinventario>1 and idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select if(idinventario>1,spajustainventarioi(idinventario,idalmacen,cantidad),0),if(idvariante>1,spajustainventariop(idvariante,idalmacen,cantidad),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString
            Comm.CommandText += "select spmodificainventarioubicacionesf(d.idinventario,d.idalmacen, u.cantidad, 0, 2, 1, u.ubicacion) from tblmovimientosdetalles d inner join tblmovimientosubicaciones u on d.iddetalle = u.iddetalle where d.idmovimiento=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Function VerificaExistencias(ByVal pId As Integer) As String
        Dim Str As String = ""
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Cont As Integer = 1
        Comm.CommandText = "select iddetalle from tblmovimientosdetalles where idmovimiento=" + pId.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read()
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        Dim I As New dbInventario(MySqlcon)
        'Dim P As New dbProductos(MySqlcon)
        Dim iIdInventario As Integer
        Dim iIdVariante As Integer
        Dim iCantidad As Double
        Dim iIdAlmacen As Integer
        Dim iCantidad2 As Double
        Dim EsInventariable As Integer
        While Cont <= IDs.Count
            Comm.CommandText = "select idinventario,idvariante,cantidad,idalmacen,if(idinventario>1,(select inventariable from tblinventario where tblinventario.idinventario=tblmovimientosdetalles.idinventario),0) as esinventariable,if(idvariante>1,(select inventariable from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblmovimientosdetalles.idvariante),0) as esinventariablep from tblmovimientosdetalles where iddetalle=" + IDs(Cont).ToString
            DReader = Comm.ExecuteReader
            If DReader.Read() Then
                iIdInventario = DReader("idinventario")
                iCantidad = DReader("cantidad")
                iIdVariante = DReader("idvariante")
                iIdAlmacen = DReader("idalmacen")
                EsInventariable = DReader("esinventariable")
                DReader.Close()
                If iIdInventario > 1 And EsInventariable = 1 Then
                    iCantidad2 = I.DaInventario(iIdAlmacen, iIdInventario)
                    If iCantidad > iCantidad2 Then
                        Str = " Hay artículos con insuficiente inventario."
                    End If
                End If
                If iIdVariante > 1 Then
                    Comm.CommandText = "select spverificaexistenciarecetas(" + iIdVariante.ToString + "," + iIdAlmacen.ToString + "," + iCantidad.ToString + ")"
                    Str = Comm.ExecuteScalar
                End If
            Else
                DReader.Close()
            End If
            Cont += 1
        End While
        Return Str
    End Function
    Public Function Reporte(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidSucursal As Integer, ByVal pidalmacen As Integer, ByVal pidalmacen2 As Integer, ByVal pidconcepto As Integer, ByVal ptipo As Integer, ByVal pIdInventario As Integer, ByVal pIdClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, pIdTipoSucursal As Integer, pMostrarenPesos As Byte, pIdCliente As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarenPesos = 0 Then
            Comm.CommandText = "select tvi.idmovimiento,tvi.fecha,tblinventarioconceptos.nombre,tvi.serie,tvi.folio,tvd.cantidad,tblinventario.nombre as nombrei,tblinventario.clave as aclave,if(tvd.idmoneda=2,tvd.precio,tvd.precio*tvi.tipodecambio) as precio,if(tvd.idmoneda=2,tvd.precio/tvd.cantidad,tvd.precio/tvd.cantidad*tvi.tipodecambio) as preciou,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen) as almacen,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen2) as almacen2,tblinventario.costobase,tblinventarioconceptos.tipo," + _
            "if(tvi.idventa<>0,(select concat('F-',tblventas.serie,convert(tblventas.folio using utf8)) from tblventas where tblventas.idventa=tvi.idventa),if(tvi.idremision<>0,(select concat('R-',serie,convert(folio using utf8)) from tblventasremisiones where tblventasremisiones.idremision=tvi.idremision),'')) as referencia,tvi.estado " + _
            "from tblmovimientos as tvi inner join tblmovimientosdetalles as tvd on tvi.idmovimiento=tvd.idmovimiento inner join tblinventario on tvd.idinventario=tblinventario.idinventario inner join tblinventarioconceptos on tvi.idconcepto=tblinventarioconceptos.idconcepto inner join tblsucursales s on tvi.idsucursal=s.idsucursal where (tvi.estado=3 or tvi.estado=4) and tvi.fecha>='" + pFecha + "' and tvi.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tvi.idmovimiento,tvi.fecha,tblinventarioconceptos.nombre,tvi.serie,tvi.folio,tvd.cantidad,tblinventario.nombre as nombrei,tblinventario.clave as aclave,tvd.precio,tvd.precio/tvd.cantidad as preciou,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen) as almacen,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen2) as almacen2,tblinventario.costobase,tblinventarioconceptos.tipo," + _
            "if(tvi.idventa<>0,(select concat('F-',tblventas.serie,convert(tblventas.folio using utf8)) from tblventas where tblventas.idventa=tvi.idventa),if(tvi.idremision<>0,(select concat('R-',serie,convert(folio using utf8)) from tblventasremisiones where tblventasremisiones.idremision=tvi.idremision),'')) as referencia,tvi.estado " + _
            "from tblmovimientos as tvi inner join tblmovimientosdetalles as tvd on tvi.idmovimiento=tvd.idmovimiento inner join tblinventario on tvd.idinventario=tblinventario.idinventario inner join tblinventarioconceptos on tvi.idconcepto=tblinventarioconceptos.idconcepto inner join tblsucursales s on tvi.idsucursal=s.idsucursal where (tvi.estado=3 or tvi.estado=4) and tvi.fecha>='" + pFecha + "' and tvi.fecha<='" + pFecha2 + "'"
        End If

        If pidSucursal > 0 Then
            Comm.CommandText += " and tvi.idsucursal=" + pidSucursal.ToString
        End If
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idsucursal=" + pIdTipoSucursal.ToString
        If pidalmacen > 0 Then
            Comm.CommandText += " and tvd.idalmacen=" + pidalmacen.ToString
        End If
        If pidalmacen2 > 0 Then
            Comm.CommandText += " and tvd.idalmacen2=" + pidalmacen2.ToString
        End If
        If pidconcepto > 0 Then
            Comm.CommandText += " and tvi.idconcepto=" + pidconcepto.ToString
        End If
        If ptipo >= 0 Then
            Comm.CommandText += " and tblinventarioconceptos.tipo=" + ptipo.ToString
        End If
        If pIdInventario <> 0 Then
            Comm.CommandText += " and tvd.idinventario=" + pIdInventario.ToString
        End If
        If pIdClasificacion > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion=" + pIdClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pIdCliente > 0 Then Comm.CommandText += " and tvi.idcliente=" + pIdCliente.ToString
        Comm.CommandText += " order by tvi.fecha,tvi.serie,tvi.folio"
        'Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tblinventario.nombre as descripcion,tvi.cantidad,tblinventarioseries.noserie,tblinventarioseries.fechagarantia,tblmovimientos.serie,tblmovimientos.folio,tblmovimientos.fecha,tblmovimientos.hora,'' as cnombre,'' as cclave from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblinventarioseries on tvi.idinventario=tblinventarioseries.idinventario and tvi.idmovimiento=tblinventarioseries.idmovimiento inner join tblmovimientos on tvi.idmovimiento=tblmovimientos.idmovimiento where tvi.idmovimiento=" + pidMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrepmovimientos")
        'DS.WriteXmlSchema("tblrepmovimientos.xml")
        Return DS.Tables("tblrepmovimientos").DefaultView
    End Function
    Public Function ReporteMovimientosxCliente(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidSucursal As Integer, ByVal pidalmacen As Integer, ByVal pidalmacen2 As Integer, ByVal pidconcepto As Integer, ByVal ptipo As Integer, ByVal pIdInventario As Integer, ByVal pIdClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pIdCliente As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "delete from tblrepmovimientosclientes"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblrepmovimientosclientes(idmovimiento,fecha,concepto,serie,folio,cantidad,nombrei,clavei,precio,preciou,almacen1,almacen2,costobase,conceptotipo,referencia,idcliente,nombrec,idconcepto,estado) " + _
        "select tvi.idmovimiento,tvi.fecha,tblinventarioconceptos.nombre,tvi.serie,tvi.folio,tvd.cantidad,tblinventario.nombre as nombrei,tblinventario.clave as aclave,tvd.precio,tvd.precio/tvd.cantidad as preciou,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen) as almacen,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen2) as almacen2,tblinventario.costobase,tblinventarioconceptos.tipo," + _
        "if(tvi.idventa<>0,(select concat('F-',tblventas.serie,convert(tblventas.folio using utf8)) from tblventas where tblventas.idventa=tvi.idventa limit 1),if(tvi.idremision<>0,(select concat('R-',serie,convert(folio using utf8)) from tblventasremisiones where tblventasremisiones.idremision=tvi.idremision limit 1),'')) as referencia,tblclientes.idcliente,tblclientes.nombre,tvi.idconcepto,tvi.estado " + _
        "from tblmovimientos as tvi inner join tblmovimientosdetalles as tvd on tvi.idmovimiento=tvd.idmovimiento inner join tblinventario on tvd.idinventario=tblinventario.idinventario inner join tblinventarioconceptos on tvi.idconcepto=tblinventarioconceptos.idconcepto inner join tblventas on tblventas.idventa=tvi.idventa inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where (tvi.estado=3 or tvi.estado=4) and tvi.fecha>='" + pFecha + "' and tvi.fecha<='" + pFecha2 + "' and tvi.idventa<>0"
        If pidSucursal > 0 Then
            Comm.CommandText += " and tvi.idsucursal=" + pidSucursal.ToString
        End If
        If pidalmacen > 0 Then
            Comm.CommandText += " and tvd.idalmacen=" + pidalmacen.ToString
        End If
        If pidalmacen2 > 0 Then
            Comm.CommandText += " and tvd.idalmacen2=" + pidalmacen2.ToString
        End If
        If pidconcepto > 0 Then
            Comm.CommandText += " and tvi.idconcepto=" + pidconcepto.ToString
        End If
        If ptipo >= 0 Then
            Comm.CommandText += " and tblinventarioconceptos.tipo=" + ptipo.ToString
        End If
        If pIdInventario <> 0 Then
            Comm.CommandText += " and tvd.idinventario=" + pIdInventario.ToString
        End If
        If pIdClasificacion > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion=" + pIdClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        Comm.CommandText += " order by tvi.fecha,tvi.serie,tvi.folio"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "insert into tblrepmovimientosclientes(idmovimiento,fecha,concepto,serie,folio,cantidad,nombrei,clavei,precio,preciou,almacen1,almacen2,costobase,conceptotipo,referencia,idcliente,nombrec,idconcepto,estado) " + _
        "select tvi.idmovimiento,tvi.fecha,tblinventarioconceptos.nombre,tvi.serie,tvi.folio,tvd.cantidad,tblinventario.nombre as nombrei,tblinventario.clave as aclave,tvd.precio,tvd.precio/tvd.cantidad as preciou,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen) as almacen,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen2) as almacen2,tblinventario.costobase,tblinventarioconceptos.tipo," + _
        "if(tvi.idventa<>0,(select concat('F-',tblventas.serie,convert(tblventas.folio using utf8)) from tblventas where tblventas.idventa=tvi.idventa limit 1),if(tvi.idremision<>0,(select concat('R-',serie,convert(folio using utf8)) from tblventasremisiones where tblventasremisiones.idremision=tvi.idremision limit 1),'')) as referencia,tblclientes.idcliente,tblclientes.nombre,tvi.idconcepto,tvi.estado " + _
        "from tblmovimientos as tvi inner join tblmovimientosdetalles as tvd on tvi.idmovimiento=tvd.idmovimiento inner join tblinventario on tvd.idinventario=tblinventario.idinventario inner join tblinventarioconceptos on tvi.idconcepto=tblinventarioconceptos.idconcepto inner join tblventasremisiones on tblventasremisiones.idremision=tvi.idremision inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente where (tvi.estado=3 or tvi.estado=4) and tvi.fecha>='" + pFecha + "' and tvi.fecha<='" + pFecha2 + "' and tvi.idremision<>0"
        If pidSucursal > 0 Then
            Comm.CommandText += " and tvi.idsucursal=" + pidSucursal.ToString
        End If
        If pidalmacen > 0 Then
            Comm.CommandText += " and tvd.idalmacen=" + pidalmacen.ToString
        End If
        If pidalmacen2 > 0 Then
            Comm.CommandText += " and tvd.idalmacen2=" + pidalmacen2.ToString
        End If
        If pidconcepto > 0 Then
            Comm.CommandText += " and tvi.idconcepto=" + pidconcepto.ToString
        End If
        If ptipo >= 0 Then
            Comm.CommandText += " and tblinventarioconceptos.tipo=" + ptipo.ToString
        End If
        If pIdInventario <> 0 Then
            Comm.CommandText += " and tvd.idinventario=" + pIdInventario.ToString
        End If
        If pIdClasificacion > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion=" + pIdClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventasremisiones.idcliente=" + pIdCliente.ToString
        End If
        Comm.CommandText += " order by tvi.fecha,tvi.hora"
        Comm.ExecuteNonQuery()
        'Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tblinventario.nombre as descripcion,tvi.cantidad,tblinventarioseries.noserie,tblinventarioseries.fechagarantia,tblmovimientos.serie,tblmovimientos.folio,tblmovimientos.fecha,tblmovimientos.hora,'' as cnombre,'' as cclave from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblinventarioseries on tvi.idinventario=tblinventarioseries.idinventario and tvi.idmovimiento=tblinventarioseries.idmovimiento inner join tblmovimientos on tvi.idmovimiento=tblmovimientos.idmovimiento where tvi.idmovimiento=" + pidMovimiento.ToString
        Comm.CommandText = "select * from tblrepmovimientosclientes "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrepmovimientosc")
        'DS.WriteXmlSchema("tblrepmovimientosc.xml")
        Return DS.Tables("tblrepmovimientosc").DefaultView
    End Function
    Public Function ReporteVentasSeries(ByVal pidMovimiento As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tblinventario.nombre as descripcion,tvi.cantidad,tblinventarioseries.noserie,tblinventarioseries.fechagarantia,tblmovimientos.serie,tblmovimientos.folio,tblmovimientos.fecha,tblmovimientos.hora,'' as cnombre,'' as cclave from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblinventarioseries on tvi.idinventario=tblinventarioseries.idinventario and tvi.idmovimiento=tblinventarioseries.idmovimiento inner join tblmovimientos on tvi.idmovimiento=tblmovimientos.idmovimiento where tvi.idmovimiento=" + pidMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasseries")
        'DS.WriteXmlSchema("tblventasseries.xml")
        Return DS.Tables("tblventasseries").DefaultView
    End Function
    Public Sub ReCalculaCostos(ByVal pIdMovimiento As Integer, ByVal pTipoCosteo As Byte, ByVal TiempoReal As Byte, ByVal pTipodeCambio As Double)
        If TiempoReal = 1 Then
            Comm.CommandTimeout = 10000
            Comm.CommandText = "select sprecalculacostos(tblmovimientos.fecha,tblmovimientosdetalles.idinventario," + pTipoCosteo.ToString + ") from tblmovimientos inner join tblmovimientosdetalles on tblmovimientos.idmovimiento=tblmovimientosdetalles.idmovimiento inner join tblinventarioconceptos on tblmovimientos.idconcepto=tblinventarioconceptos.idconcepto where (tblinventarioconceptos.tipo=0 or tblinventarioconceptos.tipo=4) and tblmovimientosdetalles.idmovimiento=" + pIdMovimiento.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "update tblinventario inner join tblmovimientosdetalles on tblinventario.idinventario=tblmovimientosdetalles.idinventario set tblinventario.costobase=spsacacostoarticulo(tblinventario.idinventario,1,tblinventario.contenido," + pTipodeCambio.ToString + "," + pTipoCosteo.ToString + ") where tblmovimientosdetalles.idmovimiento=" + pIdMovimiento.ToString + " and tblmovimientosdetalles.idinventario>1"
            Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto inner join tblmovimientosdetalles on tblproductosvariantes.idvariante=tblmovimientosdetalles.idvariante set tblproductos.costo=spsacacostoproducto(tblproductos.idproducto," + pTipoCosteo.ToString + ") where tblmovimientosdetalles.idmovimiento=" + pId.ToString + " and tblmovimientosdetalles.idvariante>1"
            'Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Function reporteTraspasosMercancia(ByVal desde As String, ByVal hasta As String, ByVal pidSucursal As Integer, ByVal idAlmacen As Integer, ByVal idIventario As Integer, pIdSucursal2 As Integer) As DataView
        Dim ds As New DataSet
        Dim relaciones As New dbInventarioRelaciones(MySqlcon)
        Dim IdConceptoDevP As Integer
        Dim IdConceptoMer As Integer
        Dim IdConceptoDevC As Integer
        Dim IdConceptoOb As Integer
        Dim IdConceptoE As Integer
        Dim CadenaIds As String
        Dim IdConceptoDevP2 As Integer
        Dim IdConceptoMer2 As Integer
        Dim IdConceptoDevC2 As Integer
        Dim IdConceptoOb2 As Integer
        Dim IdConceptoE2 As Integer
        Dim CadenaIds2 As String
        Comm.CommandTimeout = 10000
        Comm.CommandText = "select ifnull((select idconcepto from tblinventariorelacionesconceptos where idsucursal=" + pidSucursal.ToString + " and tipo=2),0)"
        IdConceptoDevP = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select idconcepto from tblinventariorelacionesconceptos where idsucursal=" + pidSucursal.ToString + " and tipo=0),0)"
        IdConceptoMer = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select idconcepto from tblinventariorelacionesconceptos where idsucursal=" + pidSucursal.ToString + " and tipo=4),0)"
        IdConceptoDevC = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select idconcepto from tblinventariorelacionesconceptos where idsucursal=" + pidSucursal.ToString + " and tipo=1),0)"
        IdConceptoOb = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select idconcepto from tblinventariorelacionesconceptos where idsucursal=" + pidSucursal.ToString + " and tipo=8),0)"
        IdConceptoE = Comm.ExecuteScalar
        CadenaIds = " m.idconcepto<>" + IdConceptoDevC.ToString + " and m.idconcepto<>" + IdConceptoDevP.ToString + " and m.idconcepto<>" + IdConceptoE.ToString + " and m.idconcepto<>" + IdConceptoMer.ToString + " and m.idconcepto<>" + IdConceptoOb.ToString

        Comm.CommandText = "select ifnull((select idconcepto from tblinventariorelacionesconceptos where idsucursal=" + pIdSucursal2.ToString + " and tipo=2),0)"
        IdConceptoDevP2 = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select idconcepto from tblinventariorelacionesconceptos where idsucursal=" + pIdSucursal2.ToString + " and tipo=0),0)"
        IdConceptoMer2 = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select idconcepto from tblinventariorelacionesconceptos where idsucursal=" + pIdSucursal2.ToString + " and tipo=4),0)"
        IdConceptoDevC2 = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select idconcepto from tblinventariorelacionesconceptos where idsucursal=" + pIdSucursal2.ToString + " and tipo=1),0)"
        IdConceptoOb2 = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select idconcepto from tblinventariorelacionesconceptos where idsucursal=" + pIdSucursal2.ToString + " and tipo=8),0)"
        IdConceptoE2 = Comm.ExecuteScalar
        CadenaIds2 = " m.idconcepto<>" + IdConceptoDevC2.ToString + " and m.idconcepto<>" + IdConceptoDevP2.ToString + " and m.idconcepto<>" + IdConceptoE2.ToString + " and m.idconcepto<>" + IdConceptoMer2.ToString + " and m.idconcepto<>" + IdConceptoOb2.ToString

        If idAlmacen > 0 Then
            Comm.CommandText = "select ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as ic on m.idconcepto=ic.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and ic.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.enviosPlanta).ToString + " and m.estado=" + CInt(Estados.Guardada).ToString + " and md.idinventario=tblinventario.idinventario and md.idalmacen2=" + idAlmacen.ToString() + "),0) as entradas,"
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.mermas).ToString + " and md.idalmacen=" + idAlmacen.ToString + " and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0) as mermas,"
            Comm.CommandText += "ifnull((select sum(dr.cantidad) from tblventasremisiones as r inner join tblventasremisionesinventario as dr on r.idremision=dr.idremision where dr.idinventario=tblinventario.idinventario and dr.idalmacen=" + idAlmacen.ToString + " and r.fecha='" + desde + "' and r.fecha<='" + hasta + "' and r.estado=" + CInt(Estados.Guardada).ToString + "),0)as facturado,"
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.devPlanta).ToString + " and md.idalmacen=" + idAlmacen.ToString() + " and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as devolucionesPlanta,"
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.obsequios).ToString + " and md.idalmacen=" + idAlmacen.ToString + " and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as obsequios,"
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.dvCliente).ToString + " and md.idalmacen=" + idAlmacen.ToString + " and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as devolucionesClientes,"
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as c on m.idconcepto=c.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and md.idalmacen=" + idAlmacen.ToString + " and md.idinventario=tblinventario.idinventario and m.estado=3 and (c.tipo=0 or c.tipo=4) and " + CadenaIds + " ),0)+ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as c on m.idconcepto=c.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and md.idalmacen2=" + idAlmacen.ToString + " and md.idinventario=tblinventario.idinventario and m.estado=3 and (c.tipo=3) and " + CadenaIds + " ),0) as otrasentradas,"
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as c on m.idconcepto=c.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and md.idalmacen=" + idAlmacen.ToString + " and md.idinventario=tblinventario.idinventario and m.estado=3 and (c.tipo=1) and " + CadenaIds + " ),0)+ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as c on m.idconcepto=c.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and md.idalmacen=" + idAlmacen.ToString + " and md.idinventario=tblinventario.idinventario and m.estado=3 and (c.tipo=3) and " + CadenaIds + " ),0) as otrassalidas,"
        Else
            If pidSucursal > 0 Then
                If pIdSucursal2 <= 0 Then
                    Comm.CommandText = "select ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as ic on m.idconcepto=ic.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and ic.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.enviosPlanta).ToString + " and ic.idsucursal=" + pidSucursal.ToString + " and m.estado=" + CInt(Estados.Guardada).ToString + " and md.idinventario=tblinventario.idinventario),0) as entradas,"
                Else
                    Comm.CommandText = "select ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as ic on m.idconcepto=ic.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and ic.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.enviosPlanta).ToString + " and (ic.idsucursal=" + pidSucursal.ToString + " or ic.idsucursal=" + pIdSucursal2.ToString + ") and m.estado=" + CInt(Estados.Guardada).ToString + " and md.idinventario=tblinventario.idinventario),0) as entradas,"
                End If
            Else
                Comm.CommandText = "select ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as ic on m.idconcepto=ic.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and ic.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.enviosPlanta).ToString + " and m.estado=" + CInt(Estados.Guardada).ToString + " and md.idinventario=tblinventario.idinventario),0) as entradas,"
            End If
            If pidSucursal > 0 Then
                If pIdSucursal2 <= 0 Then
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.mermas).ToString + " and md.idinventario=tblinventario.idinventario and rel.idsucursal=" + pidSucursal.ToString + " and m.estado=" + CInt(Estados.Guardada).ToString + "),0) as mermas,"
                Else
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.mermas).ToString + " and md.idinventario=tblinventario.idinventario and (rel.idsucursal=" + pidSucursal.ToString + " or rel.idsucursal=" + pIdSucursal2.ToString + ") and m.estado=" + CInt(Estados.Guardada).ToString + "),0) as mermas,"
                End If
            Else
                Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.mermas).ToString + " and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0) as mermas,"
            End If
            If pidSucursal > 0 Then
                If pIdSucursal2 <= 0 Then
                    Comm.CommandText += "ifnull((select sum(dr.cantidad) from tblventasremisiones as r inner join tblventasremisionesinventario as dr on r.idremision=dr.idremision where dr.idinventario=tblinventario.idinventario and r.fecha='" + desde + "' and r.fecha<='" + hasta + "' and r.estado=" + CInt(Estados.Guardada).ToString + " and r.idsucursal=" + pidSucursal.ToString + "),0)as facturado,"
                Else
                    Comm.CommandText += "ifnull((select sum(dr.cantidad) from tblventasremisiones as r inner join tblventasremisionesinventario as dr on r.idremision=dr.idremision where dr.idinventario=tblinventario.idinventario and r.fecha='" + desde + "' and r.fecha<='" + hasta + "' and r.estado=" + CInt(Estados.Guardada).ToString + " and (r.idsucursal=" + pidSucursal.ToString + " or r.idsucursal=" + pIdSucursal2.ToString + ")),0)as facturado,"
                End If
            Else
                Comm.CommandText += "ifnull((select sum(dr.cantidad) from tblventasremisiones as r inner join tblventasremisionesinventario as dr on r.idremision=dr.idremision where dr.idinventario=tblinventario.idinventario and r.fecha='" + desde + "' and r.fecha<='" + hasta + "' and r.estado=" + CInt(Estados.Guardada).ToString + "),0)as facturado,"
            End If
            If pidSucursal > 0 Then
                If pIdSucursal2 <= 0 Then
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.devPlanta).ToString + " and rel.idsucursal=" + pidSucursal.ToString + " and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as devolucionesPlanta,"
                Else
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.devPlanta).ToString + " and (rel.idsucursal=" + pidSucursal.ToString + " or rel.idsucursal=" + pIdSucursal2.ToString + ") and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as devolucionesPlanta,"
                End If
            Else
                Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.devPlanta).ToString + " and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as devolucionesPlanta,"
            End If
            If pidSucursal > 0 Then
                If pIdSucursal2 <= 0 Then
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.obsequios).ToString + " and rel.idsucursal=" + pidSucursal.ToString + " and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as obsequios,"
                Else
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.obsequios).ToString + " and (rel.idsucursal=" + pidSucursal.ToString + " or rel.idsucursal=" + pIdSucursal2.ToString + ") and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as obsequios,"
                End If
            Else
                Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.obsequios).ToString + " and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as obsequios,"
            End If
            If pidSucursal > 0 Then
                If pIdSucursal2 <= 0 Then
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.dvCliente).ToString + " and rel.idsucursal=" + pidSucursal.ToString + " and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as devolucionesClientes,"
                Else
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.dvCliente).ToString + " and (rel.idsucursal=" + pidSucursal.ToString + " or rel.idsucursal=" + pIdSucursal2.ToString + ") and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as devolucionesClientes,"
                End If
            Else
                Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.dvCliente).ToString + " and md.idinventario=tblinventario.idinventario and m.estado=" + CInt(Estados.Guardada).ToString + "),0)as devolucionesClientes,"
            End If
            If pidSucursal > 0 Then
                If pIdSucursal2 <= 0 Then
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as c on m.idconcepto=c.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and md.idinventario=tblinventario.idinventario and m.estado=3 and (c.tipo=0 or c.tipo=4) and " + CadenaIds + " and m.idsucursal=" + pidSucursal.ToString + "),0) as otrasentradas,"
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as c on m.idconcepto=c.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and md.idinventario=tblinventario.idinventario and m.estado=3 and (c.tipo=1) and " + CadenaIds + " and m.idsucursal=" + pidSucursal.ToString + "),0) as otrassalidas,"
                Else
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as c on m.idconcepto=c.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and md.idinventario=tblinventario.idinventario and m.estado=3 and (c.tipo=0 or c.tipo=4) and " + CadenaIds + " and " + CadenaIds2 + " and (m.idsucursal=" + pidSucursal.ToString + " or m.idsucursal=" + pIdSucursal2.ToString + ")),0) as otrasentradas,"
                    Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as c on m.idconcepto=c.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and md.idinventario=tblinventario.idinventario and m.estado=3 and (c.tipo=1) and " + CadenaIds + " and " + CadenaIds2 + "  and (m.idsucursal=" + pidSucursal.ToString + " or m.idsucursal=" + pIdSucursal2.ToString + ")),0) as otrassalidas,"
                End If
            Else
                Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as c on m.idconcepto=c.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and md.idinventario=tblinventario.idinventario and m.estado=3 and (c.tipo=0 or c.tipo=4) and " + CadenaIds + " ),0) as otrasentradas,"
                Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as c on m.idconcepto=c.idconcepto where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and md.idinventario=tblinventario.idinventario and m.estado=3 and (c.tipo=1) and " + CadenaIds + " ),0) as otrassalidas,"
            End If
        End If
        If pIdSucursal2 <= 0 Then
            Comm.CommandText += "ifnull(spdainventarioafecha(tblinventario.idinventario,'" + desde + "'," + pidSucursal.ToString + "," + idAlmacen.ToString + "),0) as inicial, nombre,clave"
        Else
            Comm.CommandText += "ifnull(spdainventarioafecha(tblinventario.idinventario,'" + desde + "'," + pidSucursal.ToString + ",0),0)+ifnull(spdainventarioafecha(tblinventario.idinventario,'" + desde + "'," + pIdSucursal2.ToString + ",0),0) as inicial, nombre,clave"
        End If
        Comm.CommandText += " from tblinventario"
        If idIventario > 0 Then
            Comm.CommandText += " where idinventario=" + idIventario.ToString()
        Else
            Comm.CommandText += " where idinventario>1"
        End If
        Comm.CommandText += " order by clave;"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "ventas")
        'ds.WriteXmlSchema("repInventarioMercancia1.xml")
        Return ds.Tables("ventas").DefaultView
    End Function

    Public Function controlEntregas(ByVal desde As String, ByVal hasta As String, ByVal idAlmacen As Integer, ByVal idInventario As Integer) As DataView
        Dim ds As New DataSet
        If idAlmacen > 0 Then
            Comm.CommandText = "select ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as ic on m.idconcepto=ic.idconcepto where ic.tipo=" + CInt(dbInventarioConceptos.Tipos.Traspaso).ToString + " and md.idalmacen2=" + idAlmacen.ToString + " and md.idinventario=tblinventario.idinventario and m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and m.estado=" + CInt(Estados.Guardada).ToString + "),0) as cantidad,"
        Else
            Comm.CommandText = "select ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventarioconceptos as ic on m.idconcepto=ic.idconcepto where ic.tipo=" + CInt(dbInventarioConceptos.Tipos.Traspaso).ToString + " and md.idinventario=tblinventario.idinventario and m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and m.estado=" + CInt(Estados.Guardada).ToString + "),0) as cantidad,"

        End If
        If idAlmacen > 0 Then
            Comm.CommandText += "ifnull((select sum(d.cantidad) from tblventasremisiones as r inner join tblventasremisionesinventario as d on r.idremision=d.idremision where d.idinventario=tblinventario.idinventario and d.idalmacen=" + idAlmacen.ToString() + " and r.fecha>='" + desde + "' and r.fecha<='" + hasta + "' and r.estado=" + CInt(Estados.Guardada).ToString() + "),0) as facturado, "
        Else
            Comm.CommandText += "ifnull((select sum(d.cantidad) from tblventasremisiones as r inner join tblventasremisionesinventario as d on r.idremision=d.idremision where d.idinventario=tblinventario.idinventario and r.fecha>='" + desde + "' and r.fecha<='" + hasta + "' and r.estado=" + CInt(Estados.Guardada).ToString() + "),0) as facturado, "
        End If
        If idAlmacen > 0 Then
            Comm.CommandText += "ifnull((select sum(d.cantidad) from tblventas as r inner join tblventasinventario as d on r.idventa=d.idventa where d.idinventario=tblinventario.idinventario and d.idalmacen=" + idAlmacen.ToString() + " and r.fecha>='" + desde + "' and r.fecha<='" + hasta + "' and r.estado=" + CInt(Estados.Guardada).ToString() + "),0) as relacion, "
        Else
            Comm.CommandText += "ifnull((select sum(d.cantidad) from tblventas as r inner join tblventasinventario as d on r.idventa=d.idventa where d.idinventario=tblinventario.idinventario and r.fecha>='" + desde + "' and r.fecha<='" + hasta + "' and r.estado=" + CInt(Estados.Guardada).ToString() + "),0) as relacion, "
        End If
        If idAlmacen > 0 Then
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.buenas).ToString() + " and md.idalmacen=" + idAlmacen.ToString() + " and md.idinventario=tblinventario.idinventario and m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and m.estado=" + CInt(Estados.Guardada).ToString() + "),0) as buenas, "
        Else
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.buenas).ToString() + " and md.idinventario=tblinventario.idinventario and m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and m.estado=" + CInt(Estados.Guardada).ToString() + "),0) as buenas, "

        End If
        If idAlmacen > 0 Then
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.mermas2).ToString + " and md.idalmacen=" + idAlmacen.ToString + " and md.idinventario=tblinventario.idinventario and m.fecha='" + desde + "' and m.fecha<='" + hasta + "' and m.estado=" + CInt(Estados.Guardada).ToString + "),0) as malas,"
        Else
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.mermas2).ToString + " and md.idinventario=tblinventario.idinventario and m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and m.estado=" + CInt(Estados.Guardada).ToString + "),0) as malas,"
        End If
        If idAlmacen > 0 Then
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.obsequios2).ToString() + "  and md.idalmacen=" + idAlmacen.ToString + " and md.idinventario=tblinventario.idinventario and m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and m.estado=" + CInt(Estados.Guardada).ToString + "),0) as obsequios, "
        Else
            Comm.CommandText += "ifnull((select sum(md.cantidad) from tblmovimientosdetalles as md inner join tblmovimientos as m on md.idmovimiento=m.idmovimiento inner join tblinventariorelacionesconceptos as rel on m.idconcepto=rel.idconcepto where rel.tipo=" + CInt(dbRelacionesConceptos.tipoConcepto.obsequios2).ToString() + " and md.idinventario=tblinventario.idinventario and m.fecha>='" + desde + "' and m.fecha<='" + hasta + "' and m.estado=" + CInt(Estados.Guardada).ToString + "),0) as obsequios, "

        End If
        Comm.CommandText += "nombre as descripcion, clave as producto from tblinventario"
        If idInventario > 0 Then
            Comm.CommandText += " where idinventario=" + idInventario.ToString
        Else
            Comm.CommandText += " where idinventario>1"
        End If
        Comm.CommandText += " order by clave;"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        da.Fill(ds, "entregas")
        'ds.WriteXmlSchema("repInventarioEntregaMercancia1.xml")
        Return ds.Tables("entregas").DefaultView
    End Function

    Public Function ConsultarEntrega(idmovimiento As Integer) As Entrega
        Comm.CommandText = "select * from tblmovimientosentrega where idmovimiento=" + idmovimiento.ToString() + ";"
        Dim dr As MySqlDataReader = Comm.ExecuteReader
        Try
            If dr.Read Then Return New Entrega(dr("idmovimiento"), dr("unidad"), dr("marca"), dr("modelo"), dr("color"), dr("placas"), dr("chofer"), dr("salida"), dr("lugar"), dr("paquetes"), dr("lote"), dr("numerosellos"), dr("kilos"), dr("transportista"), dr("llegada"))
            Return New Entrega(0, "", "", "", "", "", "", Now, "", 0, "", "", 0, "", Now)
        Finally
            dr.Close()
        End Try
    End Function

    Public Sub GuardarEntrega(entrega As Entrega)
        Comm.CommandText = "select count(*) from tblmovimientosentrega where idmovimiento=" + entrega.Id.ToString() + ";"
        If Comm.ExecuteScalar() = 0 Then
            Comm.CommandText = "insert into tblmovimientosentrega (idmovimiento, unidad, marca, modelo, color, placas, chofer, salida, lugar, paquetes, lote, numerosellos, kilos, llegada, transportista) values (@id, @unidad, @marca, @modelo, @color, @placas, @chofer, @salida, @lugar, @paquetes, @lote, @numerosellos, @kilos, @llegada, @transportista);"
        Else
            Comm.CommandText = "update tblmovimientosentrega set unidad=@unidad, marca=@marca, modelo=@modelo, color=@color, placas=@placas, chofer=@chofer, salida=@salida, lugar=@lugar, paquetes=@paquetes, lote=@lote, numerosellos=@numerosellos, kilos=@kilos, llegada=@llegada, transportista=@transportista where idmovimiento=@id;"
        End If
        Comm.Parameters.Add(New MySqlParameter("@id", entrega.Id))
        Comm.Parameters.Add(New MySqlParameter("@unidad", entrega.Unidad))
        Comm.Parameters.Add(New MySqlParameter("@marca", entrega.Marca))
        Comm.Parameters.Add(New MySqlParameter("@modelo", entrega.Modelo))
        Comm.Parameters.Add(New MySqlParameter("@color", entrega.Color))
        Comm.Parameters.Add(New MySqlParameter("@placas", entrega.Placas))
        Comm.Parameters.Add(New MySqlParameter("@chofer", entrega.Chofer))
        Comm.Parameters.Add(New MySqlParameter("@salida", entrega.Salida))
        Comm.Parameters.Add(New MySqlParameter("@lugar", entrega.Lugar))
        Comm.Parameters.Add(New MySqlParameter("@paquetes", entrega.Paquetes))
        Comm.Parameters.Add(New MySqlParameter("@lote", entrega.Lote))
        Comm.Parameters.Add(New MySqlParameter("@numerosellos", entrega.NumeroSellos))
        Comm.Parameters.Add(New MySqlParameter("@kilos", entrega.Kilos))
        Comm.Parameters.Add(New MySqlParameter("@llegada", entrega.Llegada))
        Comm.Parameters.Add(New MySqlParameter("@transportista", entrega.Transportista))
        Comm.ExecuteNonQuery()
        Comm.Parameters.Clear()
    End Sub

    Public Function ReporteEntregas(desde As String, hasta As String, pIdCliente As Integer, pIdSucursal As Integer) As DataSet
        Comm.CommandText = "select me.*,m.fecha,ifnull((select c.nombre from tblclientes c where c.idcliente=m.idcliente),'SIN CLIENTE ASIGNADO') cliente from tblmovimientosentrega me inner join tblmovimientos m on m.idmovimiento=me.idmovimiento where m.fecha>='" + desde + "' and m.fecha<='" + hasta + "'"
        If pIdCliente > 0 Then Comm.CommandText += " and m.idcliente=" + pIdCliente.ToString
        If pIdSucursal > 0 Then Comm.CommandText += " and m.idsucursal=" + pIdSucursal.ToString
        Comm.CommandText += " order by m.fecha;"
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(Comm)
        da.Fill(ds, "tabla")
        'ds.WriteXmlSchema("tblEntregas.xml")
        Return ds
    End Function
End Class
