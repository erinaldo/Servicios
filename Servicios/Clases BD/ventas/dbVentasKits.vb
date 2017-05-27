Public Class dbVentasKits
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    Public Cantidad As Double
    Public IdVenta As Integer
    Public IdAlmacen As Integer
    Public Surtido As Double
    Public IdRemision As Integer
    Public idCotizacion As Integer
    Public idPedido As Integer
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdVenta = 0
        IdAlmacen = 0
        Surtido = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventaskits where iddetallekit=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdVenta = DReader("idventa")
            IdAlmacen = DReader("idalmacen")
            Surtido = DReader("surtido")
            'Costo = DReader("costo")
        End If
        DReader.Close()
        If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
    End Sub
    Public Sub LlenaDatosRemision()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventaskitsr where iddetallekit=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdRemision = DReader("idremision")
            IdAlmacen = DReader("idalmacen")
            Surtido = DReader("surtido")
            'Costo = DReader("costo")
        End If
        DReader.Close()
        If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
    End Sub
    Public Sub LlenaDatosCotizacion()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventaskitsc where iddetallekit=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            idCotizacion = DReader("idcotizacion")
            IdAlmacen = DReader("idalmacen")
            Surtido = DReader("surtido")
            'Costo = DReader("costo")
        End If
        DReader.Close()
        If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
    End Sub
    Public Sub LlenaDatosPedido()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventaskitsp where iddetallekit=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            idPedido = DReader("idpedido")
            IdAlmacen = DReader("idalmacen")
            Surtido = DReader("surtido")
            'Costo = DReader("costo")
        End If
        DReader.Close()
        If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pidVariante As Integer, ByVal pidServicio As Integer, ByVal pSeparado As Integer, ByVal pCantidadM As Double, ByVal pTipoCantidadM As Integer)
        
        Idinventario = pIdinventario
        Cantidad = pCantidad
        IdVenta = pIdVenta
        IdAlmacen = pIdAlmacen
        Surtido = 0
        pSeparado = 0
        Comm.CommandText = "insert into tblventaskits(idventa,idinventario,cantidad,surtido,iddetalle,idalmacen) "
        Comm.CommandText += "select ifnull((select max(iddetallekit) from tblventaskits),0);"
        'Comm.CommandText = "select if(max(idventasinventario) is null,0,max(idventasinventario)) from tblventasinventario"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub GuardarRemision(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pidVariante As Integer, ByVal pidServicio As Integer, ByVal pSeparado As Integer, ByVal pCantidadM As Double, ByVal pTipoCantidadM As Integer)

        Idinventario = pIdinventario
        Cantidad = pCantidad
        IdRemision = pIdVenta
        IdAlmacen = pIdAlmacen
        Surtido = 0
        pSeparado = 0
        Comm.CommandText = "insert into tblventaskitsr(idremision,idinventario,cantidad,surtido,iddetalle,idalmacen) "
        Comm.CommandText += "select ifnull((select max(iddetallekit) from tblventaskitsr),0);"
        'Comm.CommandText = "select if(max(idventasinventario) is null,0,max(idventasinventario)) from tblventasinventario"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub GuardarPedido(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pidVariante As Integer, ByVal pidServicio As Integer, ByVal pSeparado As Integer, ByVal pCantidadM As Double, ByVal pTipoCantidadM As Integer)

        Idinventario = pIdinventario
        Cantidad = pCantidad
        idPedido = pIdVenta
        IdAlmacen = pIdAlmacen
        Surtido = 0
        pSeparado = 0
        Comm.CommandText = "insert into tblventaskitsp(idventa,idinventario,cantidad,surtido,iddetalle,idalmacen) "
        Comm.CommandText += "select ifnull((select max(iddetallekit) from tblventaskitsp),0);"
        'Comm.CommandText = "select if(max(idventasinventario) is null,0,max(idventasinventario)) from tblventasinventario"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub GuardarCotizacion(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pidVariante As Integer, ByVal pidServicio As Integer, ByVal pSeparado As Integer, ByVal pCantidadM As Double, ByVal pTipoCantidadM As Integer)

        Idinventario = pIdinventario
        Cantidad = pCantidad
        idCotizacion = pIdVenta
        IdAlmacen = pIdAlmacen
        Surtido = 0
        pSeparado = 0
        Comm.CommandText = "insert into tblventaskitsc(idcotizacion,idinventario,cantidad,surtido,iddetalle,idalmacen) "
        Comm.CommandText += "select ifnull((select max(iddetallekit) from tblventaskitsc),0);"
        'Comm.CommandText = "select if(max(idventasinventario) is null,0,max(idventasinventario)) from tblventasinventario"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double)
        ID = pID
        Cantidad = pCantidad
        Comm.CommandText = "update tblventaskits set cantidad=" + pCantidad.ToString + " where iddetallekit=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarRemision(ByVal pID As Integer, ByVal pCantidad As Double)
        ID = pID
        Cantidad = pCantidad
        Comm.CommandText = "update tblventaskitsr set cantidad=" + pCantidad.ToString + " where iddetallekit=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarPedido(ByVal pID As Integer, ByVal pCantidad As Double)
        ID = pID
        Cantidad = pCantidad
        Comm.CommandText = "update tblventaskitsp set cantidad=" + pCantidad.ToString + " where iddetallekit=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarCotizacion(ByVal pID As Integer, ByVal pCantidad As Double)
        ID = pID
        Cantidad = pCantidad
        Comm.CommandText = "update tblventaskitsc set cantidad=" + pCantidad.ToString + " where iddetallekit=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventaskits where iddetallekit=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarRemision(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventaskitsr where iddetallekit=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarCotizacion(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventaskitsc where iddetallekit=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarPedido(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventaskitsp where iddetallekit=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetallekit,tblinventario.clave,tblinventario.nombre,tvi.cantidad from tblventaskits tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario where tvi.iddetalle=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaskits")
        Return DS.Tables("tblventaskits").DefaultView
    End Function
    Public Function ConsultaRemision(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetallekit,tblinventario.clave,tblinventario.nombre,tvi.cantidad from tblventaskitsr tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario where tvi.iddetalle=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaskits")
        Return DS.Tables("tblventaskits").DefaultView
    End Function
    Public Function ConsultaCotizacion(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetallekit,tblinventario.clave,tblinventario.nombre,tvi.cantidad from tblventaskitsc tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario where tvi.iddetalle=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaskits")
        Return DS.Tables("tblventaskits").DefaultView
    End Function
    Public Function ConsultaPedido(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetallekit,tblinventario.clave,tblinventario.nombre,tvi.cantidad from tblventaskitsp tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario where tvi.iddetalle=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaskits")
        Return DS.Tables("tblventaskits").DefaultView
    End Function

    Public Sub InsertarArticulos(ByVal pIdIventario As Integer, ByVal pIdVenta As Integer, ByVal pIdDetalle As Integer, ByVal pCantidad As Double, ByVal pidAlmacen As Integer)
        Comm.CommandText = "insert into tblventaskits(idventa,idinventario,cantidad,surtido,iddetalle,idalmacen) select " + pIdVenta.ToString + ",idinventario,cantidad*" + pCantidad.ToString + ",0," + pIdDetalle.ToString + "," + pidAlmacen.ToString + " from tblinventariodetalles where idinventariop=" + pIdIventario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub InsertarArticulosRemision(ByVal pIdIventario As Integer, ByVal pIdRemision As Integer, ByVal pIdDetalle As Integer, ByVal pCantidad As Double, ByVal pidAlmacen As Integer)
        Comm.CommandText = "insert into tblventaskitsr(idremision,idinventario,cantidad,surtido,iddetalle,idalmacen) select " + pIdRemision.ToString + ",idinventario,cantidad*" + pCantidad.ToString + ",0," + pIdDetalle.ToString + "," + pidAlmacen.ToString + " from tblinventariodetalles where idinventariop=" + pIdIventario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub InsertarArticulosPedidos(ByVal pIdIventario As Integer, ByVal pIdPedido As Integer, ByVal pIdDetalle As Integer, ByVal pCantidad As Double, ByVal pidAlmacen As Integer)
        Comm.CommandText = "insert into tblventaskitsp(idpedido,idinventario,cantidad,surtido,iddetalle,idalmacen) select " + pIdPedido.ToString + ",idinventario,cantidad*" + pCantidad.ToString + ",0," + pIdDetalle.ToString + "," + pidAlmacen.ToString + " from tblinventariodetalles where idinventariop=" + pIdIventario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub InsertarArticulosCotizacion(ByVal pIdIventario As Integer, ByVal pIdCotizacion As Integer, ByVal pIdDetalle As Integer, ByVal pCantidad As Double, ByVal pidAlmacen As Integer)
        Comm.CommandText = "insert into tblventaskitsc(idcotizacion,idinventario,cantidad,surtido,iddetalle,idalmacen) select " + pIdCotizacion.ToString + ",idinventario,cantidad*" + pCantidad.ToString + ",0," + pIdDetalle.ToString + "," + pidAlmacen.ToString + " from tblinventariodetalles where idinventariop=" + pIdIventario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarArticulos(ByVal piddetalle As Integer)
        Comm.CommandText = "delete from tblventaskits where iddetalle=" + piddetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarArticulosRemisiones(ByVal piddetalle As Integer)
        Comm.CommandText = "delete from tblventaskitsr where iddetalle=" + piddetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarArticulosPedidos(ByVal piddetalle As Integer)
        Comm.CommandText = "delete from tblventaskitsp where iddetalle=" + piddetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarArticulosCotizaciones(ByVal piddetalle As Integer)
        Comm.CommandText = "delete from tblventaskitsc where iddetalle=" + piddetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificaArtículos(ByVal pidDetalle As Integer, ByVal pCantidad As Double, ByVal pIdInventarioP As Integer)
        Comm.CommandText = "update tblventaskits set cantidad=(select cantidad from tblinventariodetalles where idinventario=tblventaskits.idinventario and idinventariop=" + pIdInventarioP.ToString + " limit 1)*" + pCantidad.ToString + " where iddetalle=" + pidDetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificaArtículosRemision(ByVal pidDetalle As Integer, ByVal pCantidad As Double, ByVal pIdInventarioP As Integer)
        Comm.CommandText = "update tblventaskitsr set cantidad=(select cantidad from tblinventariodetalles where idinventario=tblventaskitsr.idinventario and idinventariop=" + pIdInventarioP.ToString + ")*" + pCantidad.ToString + " where iddetalle=" + pidDetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificaArtículosCotizacion(ByVal pidDetalle As Integer, ByVal pCantidad As Double, ByVal pIdInventarioP As Integer)
        Comm.CommandText = "update tblventaskitsc set cantidad=(select cantidad from tblinventariodetalles where idinventario=tblventaskitsc.idinventario and idinventariop=" + pIdInventarioP.ToString + ")*" + pCantidad.ToString + " where iddetalle=" + pidDetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificaArtículosPedido(ByVal pidDetalle As Integer, ByVal pCantidad As Double, ByVal pIdInventarioP As Integer)
        Comm.CommandText = "update tblventaskitsp set cantidad=(select cantidad from tblinventariodetalles where idinventario=tblventaskitsp.idinventario and idinventariop=" + pIdInventarioP.ToString + ")*" + pCantidad.ToString + " where iddetalle=" + pidDetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    'Public Function ConsultaReader(ByVal pIdVenta As Integer, ByVal pConSeries As Boolean) As MySql.Data.MySqlClient.MySqlDataReader
    '    If pConSeries Then
    '        Comm.CommandText = "select tvi.idventasinventario,tblinventario.clave,concat(tvi.descripcion,spdaseriesventa(tvi.idinventario,tvi.idventa)) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idvariante,tvi.idservicio,tblproductos.clave as pclave,tvi.idinventario,tvi.idvariante,tvi.cantidadm,ifnull((select abreviatura from tbltiposcantidades where idtipocantidad=tvi.tipocantidadm),tbltiposcantidades.abreviatura)  as tipom,tvi.descuento,tvi.extra,tblinventario.clave2 from tblventasinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad inner join tblproductosvariantes on tvi.idvariante=tblproductosvariantes.idvariante inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto  where tvi.idventa=" + pIdVenta.ToString
    '    Else
    '        Comm.CommandText = "select tvi.idventasinventario,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idvariante,tvi.idservicio,tblproductos.clave as pclave,tvi.idinventario,tvi.idvariante,tvi.cantidadm,ifnull((select abreviatura from tbltiposcantidades where idtipocantidad=tvi.tipocantidadm),tbltiposcantidades.abreviatura)  as tipom,tvi.descuento,tvi.extra,tblinventario.clave2 from tblventasinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad inner join tblproductosvariantes on tvi.idvariante=tblproductosvariantes.idvariante inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto  where tvi.idventa=" + pIdVenta.ToString
    '    End If
    '    Return Comm.ExecuteReader
    'End Function
End Class
