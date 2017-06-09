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
    Public Ubicacion As String
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
        Comm.CommandText = "select precio, idinventario, d.cantidad, idmovimiento, descripcion, ifnull(idalmacen,-1) idalmacen, ifnull(idalmacen2,-1) idalmacen2, idvariante, d.surtido, inventarioanterior, idmoneda, ifnull(ubicacion,'') ubicacion, ifnull(ubicaciond,'') ubicaciond from tblmovimientosdetalles d left outer join tblmovimientosubicaciones u on d.iddetalle=u.iddetalle where d.iddetalle = " + ID.ToString
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
            Ubicacion = DReader("ubicacion")
            UbicacionD = DReader("ubicaciond")
        End If
        DReader.Close()
        If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdMovimiento As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIdAlmacen2 As Integer, ByVal pidVariante As Integer, ByVal pSeparado As Integer, ByVal pInventarioAnterior As Double, pUbicacion As String, pUbicacionD As String)
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
        Ubicacion = pUbicacion
        UbicacionD = pUbicacionD

        NuevoConcepto = True
        Comm.CommandText = "insert into tblmovimientosdetalles(idinventario, cantidad, precio, idmovimiento, descripcion, idalmacen, idvariante, surtido, inventarioanterior, idmoneda,idalmacen2) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMovimiento.ToString + ",'" + Trim(Replace(Descripcion, "'", "''")) + "'," + IdAlmacen.ToString + "," + idVariante.ToString + ",0," + InventarioAnterior.ToString + "," + IdMoneda.ToString + "," + pIdAlmacen2.ToString + ");"
        Comm.CommandText += "select ifnull(last_insert_id(),0);"
        ID = Comm.ExecuteScalar

        If pUbicacion <> "" And pUbicacionD = "" Then
            Comm.CommandText = "insert into tblmovimientosubicaciones (iddetalle, cantidad, surtido, ubicacion,ubicaciond,ubicaciond2) values(" + ID.ToString + ", " + Cantidad.ToString() + ", 0, '" + Trim(Replace(Ubicacion, "'", "''")) + "','','')"
            Comm.ExecuteNonQuery()
        End If
        If pUbicacion <> "" And pUbicacionD <> "" Then
            Comm.CommandText = "insert into tblmovimientosubicaciones (iddetalle, cantidad, surtido, ubicacion,ubicaciond,ubicaciond2) values(" + ID.ToString + ", " + Cantidad.ToString() + ", 0, '" + Trim(Replace(Ubicacion, "'", "''")) + "','" + Trim(Replace(UbicacionD, "'", "''")) + "','" + Trim(Replace(UbicacionD, "'", "''")) + "')"
            Comm.ExecuteNonQuery()
        End If

        'Comm.CommandText = "select tipo from tblinventarioconceptos c inner join tblmovimientos m on m.idconcepto=c.idconcepto where idmovimiento=" + IdMovimiento.ToString()
        'Select Case Comm.ExecuteScalar
        '    Case 0, 4
        '        Comm.CommandText = "insert into tblmovimientosdetalles(idinventario, cantidad, precio, idmovimiento, descripcion, idalmacen, idvariante, surtido, inventarioanterior, idmoneda,idalmacen2) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMovimiento.ToString + ",'" + Trim(Replace(Descripcion, "'", "''")) + "'," + IdAlmacen.ToString + "," + idVariante.ToString + ",0," + InventarioAnterior.ToString + "," + IdMoneda.ToString + "," + pIdAlmacen2.ToString + ");"
        '        Comm.ExecuteNonQuery()
        '        If pUbicacion <> "" Then
        '            Comm.CommandText = "insert into tblmovimientosubicaciones (iddetalle, cantidad, surtido, ubicacion) select max(iddetalle), " + Cantidad.ToString() + ", 0, '" + Trim(Replace(Ubicacion, "'", "''")) + "' from tblmovimientosdetalles;"
        '            Comm.ExecuteNonQuery()
        '        End If
        '    Case 1
        '        Comm.CommandText = "insert into tblmovimientosdetalles(idinventario, cantidad, precio, idmovimiento, descripcion, idalmacen, idvariante, surtido, inventarioanterior, idmoneda,idalmacen2) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMovimiento.ToString + ",'" + Trim(Replace(Descripcion, "'", "''")) + "'," + IdAlmacen.ToString + "," + idVariante.ToString + ",0," + InventarioAnterior.ToString + "," + IdMoneda.ToString + "," + pIdAlmacen2.ToString + ");"
        '        Comm.ExecuteNonQuery()
        '        If pUbicacion <> "" Then
        '            Comm.CommandText = "insert into tblmovimientosubicaciones (ubicacion, iddetalle, cantidad, surtido) select '" + Trim(Replace(Ubicacion, "'", "''")) + "', max(iddetalle), " + Cantidad.ToString() + ", 0 from tblmovimientosdetalles;"
        '            Comm.ExecuteNonQuery()
        '        End If
        '    Case 3
        '        Comm.CommandText = "insert into tblmovimientosdetalles(idinventario, cantidad, precio, idmovimiento, descripcion, idalmacen, idalmacen2, idvariante, surtido, inventarioanterior, idmoneda) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMovimiento.ToString + ",'" + Trim(Replace(Descripcion, "'", "''")) + "'," + IdAlmacen.ToString + "," + IdAlmacen2.ToString + "," + idVariante.ToString + ",0," + InventarioAnterior.ToString + "," + IdMoneda.ToString + ");"
        '        Comm.ExecuteNonQuery()
        '        If pUbicacion <> "" And pUbicacionD <> "" Then
        '            Comm.CommandText = "insert into tblmovimientosubicaciones (ubicacion, iddetalle, cantidad, surtido, ubicaciond) select '" + Trim(Replace(Ubicacion, "'", "''")) + "', max(iddetalle), " + Cantidad.ToString() + ", 0, '" + Trim(Replace(UbicacionD, "'", "''")) + "' from tblmovimientosdetalles;"
        '            Comm.ExecuteNonQuery()
        '        End If
        '    Case Else
        '        Comm.CommandText = "insert into tblmovimientosdetalles(idinventario, cantidad, precio, idmovimiento, descripcion, idalmacen, idvariante, surtido, inventarioanterior, idmoneda,idalmacen2) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMovimiento.ToString + ",'" + Trim(Replace(Descripcion, "'", "''")) + "'," + IdAlmacen.ToString + "," + idVariante.ToString + ",0," + InventarioAnterior.ToString + "," + IdMoneda.ToString + "," + pIdAlmacen2.ToString + ");"
        '        Comm.ExecuteNonQuery()
        'End Select

        'si el detalle tiene ubicación se agrega el registro de tblmovimientosubicaciones 
        
        'End If
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pIdMoneda As Integer, ByVal pidAlmacen As Integer, ByVal pIdAlmacen2 As Integer, ByVal pPrecio As Double, pUbicacion As String, pUbicacionD As String)
        ID = pID
        Cantidad = pCantidad
        IdAlmacen = pidAlmacen
        IdAlmacen2 = pIdAlmacen2
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Ubicacion = pUbicacion
        UbicacionD = pUbicacionD

        Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + Cantidad.ToString + ", idalmacen=" + IdAlmacen.ToString + ", idalmacen2=" + IdAlmacen2.ToString + ", idmoneda=" + IdMoneda.ToString + ", precio=" + Precio.ToString + " where iddetalle=" + pID.ToString + ";"
        Comm.ExecuteNonQuery()
        If pUbicacion <> "" And pUbicacionD = "" Then
            Comm.CommandText = "update tblmovimientosubicaciones set cantidad=" + Cantidad.ToString + ",ubicacion='" + Trim(Replace(Ubicacion, "'", "''")) + "' where iddetalle=" + ID.ToString
            Comm.ExecuteNonQuery()
        End If
        If pUbicacion <> "" And pUbicacionD <> "" Then
            Comm.CommandText = "update tblmovimientosubicaciones set cantidad=" + Cantidad.ToString + ",ubicacion='" + Trim(Replace(Ubicacion, "'", "''")) + "',ubicaciond='" + Trim(Replace(UbicacionD, "'", "''")) + "' where iddetalle=" + ID.ToString
            Comm.ExecuteNonQuery()
        End If

        'Comm.CommandText = "select tipo from tblinventarioconceptos c inner join tblmovimientos m on m.idconcepto=c.idconcepto where idmovimiento=" + IdMovimiento.ToString()
        'Select Case Comm.ExecuteScalar
        '    Case 0, 4
        '        Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + Cantidad.ToString + ", idalmacen=" + IdAlmacen.ToString + ", idmoneda=" + IdMoneda.ToString + ", precio=" + Precio.ToString + " where iddetalle=" + pID.ToString + ";"
        '        'modifica la ubicación
        '        If pUbicacionD <> "" Then
        '            'le resta la existencia a la ubicacion original
        '            Comm.CommandText += "update tblmovimientosdetalles inner join tblmovimientosubicaciones on tblmovimientosdetalles.iddetalle=tblmovimientosubicaciones.iddetalle inner join tblalmacenesiubicaciones on tblmovimientosubicaciones.ubicaciond=tblalmacenesiubicaciones.ubicacion and tblmovimientosdetalles.idalmacen2=tblalmacenesiubicaciones.idalmacen and tblmovimientosdetalles.idinventario=tblalmacenesiubicaciones.idinventario set tblalmacenesiubicaciones.cantidad=tblalmacenesiubicaciones.cantidad-" + Cantidad.ToString() + " where tblmovimientosdetalles.iddetalle=" + pID.ToString() + "; "
        '            'actualiza la ubicacion en los detalles del movimiento
        '            Comm.CommandText += "update tblmovimientosubicaciones set cantidad=" + Cantidad.ToString() + ", ubicaciond='" + Trim(Replace(UbicacionD, "'", "''")) + "'  where iddetalle=" + pID.ToString() + "; "
        '            'inserta la ubicacion si hace falta
        '            Comm.CommandText += "insert into tblalmacenesiubicaciones (ubicacion,cantidad,idalmacen,idinventario) select mu.ubicaciond, 0, md.idalmacen2, md.idinventario from tblmovimientosdetalles md inner join tblmovimientosubicaciones mu on md.iddetalle=mu.iddetalle left outer join tblalmacenesiubicaciones aiu on mu.ubicaciond=aiu.ubicacion and md.idalmacen=aiu.idalmacen and md.idinventario=aiu.idinventario and mu.ubicaciond='" + Trim(Replace(UbicacionD, "'", "''")) + "' where md.iddetalle=" + pID.ToString() + " and isnull(aiu.ubicacion); "
        '            'le suma la existencia a la ubicacion nueva
        '            Comm.CommandText += "update tblmovimientosdetalles inner join tblmovimientosubicaciones on tblmovimientosdetalles.iddetalle=tblmovimientosubicaciones.iddetalle inner join tblalmacenesiubicaciones on tblmovimientosubicaciones.ubicaciond=tblalmacenesiubicaciones.ubicacion and tblmovimientosdetalles.idalmacen2=tblalmacenesiubicaciones.idalmacen and tblmovimientosdetalles.idinventario=tblalmacenesiubicaciones.idinventario set tblalmacenesiubicaciones.cantidad=tblalmacenesiubicaciones.cantidad+" + Cantidad.ToString() + " where tblmovimientosdetalles.iddetalle=" + pID.ToString() + "; "
        '        End If
        '        Comm.ExecuteNonQuery()
        '    Case 1
        '        Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + Cantidad.ToString + ", idalmacen=" + IdAlmacen.ToString + ", idmoneda=" + IdMoneda.ToString + ", precio=" + Precio.ToString + " where iddetalle=" + pID.ToString + ";"
        '        Comm.ExecuteNonQuery()
        '    Case 3
        '        Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + Cantidad.ToString + ", idalmacen=" + IdAlmacen.ToString + ", idalmacen2=" + IdAlmacen2.ToString + ", idmoneda=" + IdMoneda.ToString + ", precio=" + Precio.ToString + " where iddetalle=" + pID.ToString + ";"
        '        Comm.ExecuteNonQuery()
        '    Case Else
        '        Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + Cantidad.ToString + ", idalmacen=" + IdAlmacen.ToString + ", idmoneda=" + IdMoneda.ToString + ", precio=" + Precio.ToString + " where iddetalle=" + pID.ToString + ";"
        '        Comm.ExecuteNonQuery()
        'End Select

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
            Comm.CommandText = "select tvi.iddetalle,tvi.cantidad,tblinventario.clave,concat(tvi.descripcion,spdaseriesmovimiento(tvi.idinventario,tvi.idmovimiento," + pConSeries.ToString + "),spdadetallesaduanaotros(tvi.idmovimiento,tvi.iddetalle,3," + pAduana.ToString + "),spdadetalleslotes(tvi.idmovimiento,tvi.iddetalle,4," + pAduana.ToString + ")) as descripcion,round(tvi.precio,2) as precio,tblmonedas.abreviatura,concat((select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen), ' ', ifnull(u.ubicacion,'')) as almacen1,concat((select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen2), ' ',ifnull( u.ubicaciond,'')) as almacen2 from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido = tbltiposcantidades.idtipocantidad left outer join tblmovimientosubicaciones u on tvi.iddetalle=u.iddetalle where tvi.idmovimiento=" + pIdMovimiento.ToString
        Else
            Comm.CommandText = "select tvi.iddetalle,tvi.cantidad,tblinventario.clave,concat(tvi.descripcion,spdaseriesmovimiento(tvi.idinventario,tvi.idmovimiento," + pConSeries.ToString + "),spdadetallesaduanaotros(tvi.idmovimiento,tvi.iddetalle,3," + pAduana.ToString + "),spdadetalleslotes(tvi.idmovimiento,tvi.iddetalle,4," + pAduana.ToString + ")) as descripcion,round(tvi.precio,2) as precio,tblmonedas.abreviatura,concat((select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen), ' ', ifnull(u.ubicacion,'')) as almacen1,'' as almacen2 from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario = tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad left outer join tblmovimientosubicaciones u on tvi.iddetalle=u.iddetalle where tvi.idmovimiento=" + pIdMovimiento.ToString
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmovimientosdetalles")
        Return DS.Tables("tblmovimientosdetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdMovimiento As Integer, ByVal pConSeries As Byte, ByVal pAduana As Byte) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,concat(tvi.descripcion,spdaseriesmovimiento(tvi.idinventario,tvi.idmovimiento," + pConSeries.ToString + "),spdadetallesaduanaotros(tvi.idmovimiento,tvi.iddetalle,3," + pAduana.ToString + "),spdadetalleslotes(tvi.idmovimiento,tvi.iddetalle,4," + pAduana.ToString + ")) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.idmoneda,tvi.idinventario,concat((select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen), ' ', ifnull((select u.ubicacion from tblmovimientosubicaciones u where u.iddetalle=tvi.iddetalle),'')) as almacen1,concat((select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen2), ' ',ifnull((select u.ubicaciond from tblmovimientosubicaciones u where u.iddetalle=tvi.iddetalle),'')) as almacen2 from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido = tbltiposcantidades.idtipocantidad where tvi.idmovimiento=" + pIdMovimiento.ToString
        Return Comm.ExecuteReader
    End Function

    
End Class
