Public Class dbMovimientosDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    Public Cantidad As Double
    Public Precio As Double
    Public IdMovimiento As Integer
    Public Descripcion As String
    Public NuevoConcepto As Boolean
    Public IdAlmacen As Integer
    Public IdAlmacen2 As Integer
    Public idVariante As Integer
    Public Surtido As Double
    Public IdMoneda As Integer
    Public InventarioAnterior As Double
    Public UbicacionO As String
    Public UbicacionD As String
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        Precio = 0
        IdMovimiento = 0
        Descripcion = ""
        IdAlmacen = 0
        IdAlmacen2 = 0
        idVariante = 0
        Surtido = 0
        NuevoConcepto = False
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select precio, idinventario, d.cantidad, idmovimiento, descripcion, ifnull(idalmacen,-1) idalmacen, ifnull(idalmacen2,-1) idalmacen2, idvariante, d.surtido, inventarioanterior, idmoneda, ubicaciono, ubicaciond from tblmovimientosdetalles d left outer join tblmovimientosubicaciones u on d.iddetalle=u.iddetalle where d.iddetalle = " + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdMovimiento = DReader("idmovimiento")
            Descripcion = DReader("descripcion")
            IdAlmacen = DReader("idalmacen")
            IdAlmacen2 = DReader("idalmacen2")
            idVariante = DReader("idvariante")
            Surtido = DReader("surtido")
            InventarioAnterior = DReader("inventarioanterior")
            IdMoneda = DReader("idmoneda")
            UbicacionO = If(DReader("ubicaciono") Is DBNull.Value, "", DReader("ubicaciono"))
            UbicacionD = If(DReader("ubicaciond") Is DBNull.Value, "", DReader("ubicaciond"))
        End If
        DReader.Close()
        If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdMovimiento As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIdAlmacen2 As Integer, ByVal pidVariante As Integer, ByVal pSeparado As Integer, ByVal pInventarioAnterior As Double, pUbicacionO As String, pUbicacionD As String)
        'Dim CTemp As Double
        'Dim PTemp As Double

        IdMovimiento = pIdMovimiento
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        IdAlmacen = pIdAlmacen
        IdAlmacen2 = pIdAlmacen2
        idVariante = 1
        InventarioAnterior = pInventarioAnterior
        UbicacionO = pUbicacionO
        UbicacionD = pUbicacionD
        
        NuevoConcepto = True
        Comm.CommandText = "select tipo from tblinventarioconceptos c inner join tblmovimientos m on m.idconcepto=c.idconcepto where idmovimiento=" + IdMovimiento.ToString()
        Select Case Comm.ExecuteScalar
            Case 0, 4
                Comm.CommandText = "insert into tblmovimientosdetalles(idinventario, cantidad, precio, idmovimiento, descripcion, idalmacen, idvariante, surtido, inventarioanterior, idmoneda) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMovimiento.ToString + ",'" + Trim(Replace(Descripcion, "'", "''")) + "'," + IdAlmacen.ToString + "," + idVariante.ToString + ",0," + InventarioAnterior.ToString + "," + IdMoneda.ToString + ");"
                Comm.ExecuteNonQuery()
                If pUbicacionO <> "" Then
                    Comm.CommandText = "insert into tblmovimientosubicaciones (iddetalle, cantidad, surtido, ubicaciono) select max(iddetalle), " + Cantidad.ToString() + ", 0, '" + Trim(Replace(UbicacionO, "'", "''")) + "' from tblmovimientosdetalles;"
                    Comm.ExecuteNonQuery()
                End If
            Case 1
                Comm.CommandText = "insert into tblmovimientosdetalles(idinventario, cantidad, precio, idmovimiento, descripcion, idalmacen, idvariante, surtido, inventarioanterior, idmoneda) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMovimiento.ToString + ",'" + Trim(Replace(Descripcion, "'", "''")) + "'," + IdAlmacen.ToString + "," + idVariante.ToString + ",0," + InventarioAnterior.ToString + "," + IdMoneda.ToString + ");"
                Comm.ExecuteNonQuery()
                If pUbicacionO <> "" Then
                    Comm.CommandText = "insert into tblmovimientosubicaciones (ubicaciono, iddetalle, cantidad, surtido) select '" + Trim(Replace(UbicacionO, "'", "''")) + "', max(iddetalle), " + Cantidad.ToString() + ", 0 from tblmovimientosdetalles;"
                    Comm.ExecuteNonQuery()
                End If
            Case 3
                Comm.CommandText = "insert into tblmovimientosdetalles(idinventario, cantidad, precio, idmovimiento, descripcion, idalmacen, idalmacen2, idvariante, surtido, inventarioanterior, idmoneda) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMovimiento.ToString + ",'" + Trim(Replace(Descripcion, "'", "''")) + "'," + IdAlmacen.ToString + "," + IdAlmacen2.ToString + "," + idVariante.ToString + ",0," + InventarioAnterior.ToString + "," + IdMoneda.ToString + ");"
                Comm.ExecuteNonQuery()
                If pUbicacionO <> "" And pUbicacionD <> "" Then
                    Comm.CommandText = "insert into tblmovimientosubicaciones (ubicaciono, iddetalle, cantidad, surtido, ubicaciond) select '" + Trim(Replace(UbicacionO, "'", "''")) + "', max(iddetalle), " + Cantidad.ToString() + ", 0, '" + Trim(Replace(UbicacionD, "'", "''")) + "' from tblmovimientosdetalles;"
                    Comm.ExecuteNonQuery()
                End If
            Case Else
                Comm.CommandText = "insert into tblmovimientosdetalles(idinventario, cantidad, precio, idmovimiento, descripcion, idalmacen, idvariante, surtido, inventarioanterior, idmoneda) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMovimiento.ToString + ",'" + Trim(Replace(Descripcion, "'", "''")) + "'," + IdAlmacen.ToString + "," + idVariante.ToString + ",0," + InventarioAnterior.ToString + "," + IdMoneda.ToString + ");"
                Comm.ExecuteNonQuery()
        End Select

        'si el detalle tiene ubicación se agrega el registro de tblmovimientosubicaciones 
        Comm.CommandText = "select ifnull((select max(iddetalle) from tblmovimientosdetalles),0)"
        ID = Comm.ExecuteScalar
        'End If


    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pIdMoneda As Integer, ByVal pidAlmacen As Integer, ByVal pIdAlmacen2 As Integer, ByVal pPrecio As Double, pUbicacionO As String, pUbicacionD As String)
        ID = pID
        Cantidad = pCantidad
        IdAlmacen = pidAlmacen
        IdAlmacen2 = pIdAlmacen2
        Precio = pPrecio
        IdMoneda = pIdMoneda
        UbicacionO = pUbicacionO
        UbicacionD = pUbicacionD

        Comm.CommandText = "select tipo from tblinventarioconceptos c inner join tblmovimientos m on m.idconcepto=c.idconcepto where idmovimiento=" + IdMovimiento.ToString()
        Select Case Comm.ExecuteScalar
            Case 0, 4
                Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + Cantidad.ToString + ", idalmacen=" + IdAlmacen.ToString + ", idmoneda=" + IdMoneda.ToString + ", precio=" + Precio.ToString + " where iddetalle=" + pID.ToString + "; update tblmovimientosubicaciones set ubicaciono='" + Trim(Replace(UbicacionO, "'", "''")) + "' where iddetalle = " + pID.ToString() + ";"
                Comm.ExecuteNonQuery()
            Case 1
                Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + Cantidad.ToString + ", idalmacen=" + IdAlmacen.ToString + ", idmoneda=" + IdMoneda.ToString + ", precio=" + Precio.ToString + " where iddetalle=" + pID.ToString + "; update tblmovimientosubicaciones set ubicaciono='" + Trim(Replace(UbicacionO, "'", "''")) + "' where iddetalle = " + pID.ToString() + ";"
                Comm.ExecuteNonQuery()
            Case 3
                Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + Cantidad.ToString + ", idalmacen=" + IdAlmacen.ToString + ", idalmacen2=" + IdAlmacen2.ToString + ", idmoneda=" + IdMoneda.ToString + ", precio=" + Precio.ToString + " where iddetalle=" + pID.ToString + "; update tblmovimientosubicaciones set ubicaciono='" + Trim(Replace(UbicacionO, "'", "''")) + "', ubicaciond='" + Trim(Replace(UbicacionD, "'", "''")) + "' where iddetalle = " + pID.ToString() + ";"
                Comm.ExecuteNonQuery()
            Case Else
                Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + Cantidad.ToString + ", idalmacen=" + IdAlmacen.ToString + ", idmoneda=" + IdMoneda.ToString + ", precio=" + Precio.ToString + " where iddetalle=" + pID.ToString + "; update tblmovimientosubicaciones set ubicaciono='" + Trim(Replace(UbicacionO, "'", "''")) + "', ubicaciond='" + Trim(Replace(UbicacionD, "'", "''")) + "' where iddetalle = " + pID.ToString() + ";"
                Comm.ExecuteNonQuery()
        End Select
    End Sub
    Public Sub ModificarCantidad(pId As Integer, pCantidad As Double, pPrecio As Double)
        Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + pCantidad.ToString + ",precio=" + pPrecio.ToString + " where iddetalle=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblmovimientosdetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitarBoletaxArticulo(ByVal piddetalle As Integer)
        Comm.CommandText = "delete from tblsemillasboletas where iddetalle=" + piddetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdMovimiento As Integer, pConSeries As Byte, pAduana As Byte, pverSegundoAlmacen As Boolean) As DataView
        Dim DS As New DataSet
        If pverSegundoAlmacen Then
            Comm.CommandText = "select tvi.iddetalle,tvi.cantidad,tblinventario.clave,concat(tvi.descripcion,spdaseriesmovimiento(tvi.idinventario,tvi.idmovimiento," + pConSeries.ToString + "),spdadetallesaduanaotros(tvi.idmovimiento,tvi.iddetalle,3," + pAduana.ToString + "),spdadetalleslotes(tvi.idmovimiento,tvi.iddetalle,4," + pAduana.ToString + ")) as descripcion,round(tvi.precio,2) as precio,tblmonedas.abreviatura,concat((select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen), ' ', ifnull(u.ubicaciono,'')) as almacen1,concat((select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen2), ' ', ifnull(u.ubicaciond,'')) as almacen2 from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido = tbltiposcantidades.idtipocantidad left outer join tblmovimientosubicaciones u on tvi.iddetalle=u.iddetalle where tvi.idmovimiento=" + pIdMovimiento.ToString
        Else
            Comm.CommandText = "select tvi.iddetalle,tvi.cantidad,tblinventario.clave,concat(tvi.descripcion,spdaseriesmovimiento(tvi.idinventario,tvi.idmovimiento," + pConSeries.ToString + "),spdadetallesaduanaotros(tvi.idmovimiento,tvi.iddetalle,3," + pAduana.ToString + "),spdadetalleslotes(tvi.idmovimiento,tvi.iddetalle,4," + pAduana.ToString + ")) as descripcion,round(tvi.precio,2) as precio,tblmonedas.abreviatura,concat((select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen), ' ', ifnull(u.ubicaciono,'')) as almacen1,'' as almacen2 from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario = tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad left outer join tblmovimientosubicaciones u on tvi.iddetalle=u.iddetalle where tvi.idmovimiento=" + pIdMovimiento.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmovimientosdetalles")
        Return DS.Tables("tblmovimientosdetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdMovimiento As Integer, ByVal pConSeries As Byte, ByVal pAduana As Byte) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,concat(tvi.descripcion,spdaseriesmovimiento(tvi.idinventario,tvi.idmovimiento," + pConSeries.ToString + "),spdadetallesaduanaotros(tvi.idmovimiento,tvi.iddetalle,3," + pAduana.ToString + "),spdadetalleslotes(tvi.idmovimiento,tvi.iddetalle,4," + pAduana.ToString + ")) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.idmoneda,tvi.idvariante,tblproductos.clave as pclave,tvi.idinventario,concat((select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen), ' ', u.ubicaciono) as almacen1,concat((select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen2), ' ', u.ubicaciond) as almacen2 from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido = tbltiposcantidades.idtipocantidad inner join tblproductosvariantes on tvi.idvariante=tblproductosvariantes.idvariante inner join tblproductos on tblproductosvariantes.idproducto = tblproductos.idproducto left outer join tblmovimientosubicaciones u on tvi.iddetalle=u.iddetalle where tvi.idmovimiento=" + pIdMovimiento.ToString
        Return Comm.ExecuteReader
    End Function

    Public Function Ubicaciones(idalmacen As Integer, idinventario As Integer) As DataTable
        Comm.CommandText = "select au.ubicacion, concat(au.ubicacion, ' (', ifnull(aiu.cantidad,0), ')') ubicacionc from tblalmacenesubicaciones au left outer join tblalmacenesiubicaciones aiu on au.idalmacen=aiu.idalmacen and au.ubicacion=aiu.ubicacion and aiu.idinventario=" + idinventario.ToString() + " where au.idalmacen=" + idalmacen.ToString() + " order by au.ubicacion;"
        Dim da As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        Dim ds As New DataSet
        da.Fill(ds, "tabla")
        Return ds.Tables("tabla")
    End Function
End Class
