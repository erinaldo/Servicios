Public Class dbMovimientosDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    Public Producto As dbProductosVariantes
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
        Comm.CommandText = "select * from tblmovimientosdetalles where iddetalle=" + ID.ToString
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
        End If
        DReader.Close()
        If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
        If idVariante > 1 Then Producto = New dbProductosVariantes(idVariante, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdMovimiento As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIdAlmacen2 As Integer, ByVal pidVariante As Integer, ByVal pSeparado As Integer, ByVal pInventarioAnterior As Double)
        Dim CTemp As Double
        Dim PTemp As Double

        IdMovimiento = pIdMovimiento
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        IdAlmacen = pIdAlmacen
        IdAlmacen2 = pIdAlmacen2
        idVariante = pidVariante
        InventarioAnterior = pInventarioAnterior

        Dim IdTemp As Integer
        If Idinventario <> 0 And pSeparado = 1 Then
            Comm.CommandText = "select ifnull((select iddetalle from tblmovimientosdetalles where idmovimiento=" + IdMovimiento.ToString + " and idinventario=" + Idinventario.ToString + " limit 1),0)"
            IdTemp = Comm.ExecuteScalar
        Else
            If pSeparado = 1 Or idVariante > 1 Then Idinventario = 1
        End If
        If idVariante <> 0 And pSeparado = 1 Then
            Comm.CommandText = "select ifnull((select iddetalle from tblmovimientosdetalles where idmovimiento=" + IdMovimiento.ToString + " and idvariante=" + idVariante.ToString + "),0)"
            IdTemp = Comm.ExecuteScalar
        Else
            If pSeparado = 1 Or Idinventario > 1 Then idVariante = 1
        End If
        pSeparado = 0
        If IdTemp <> 0 And pSeparado = 1 Then
            Comm.CommandText = "select cantidad from tblmovimientosdetalles where iddetalle=" + IdTemp.ToString
            CTemp = Comm.ExecuteScalar
            Comm.CommandText = "select precio from tblmovimientosdetalles where iddetalle=" + IdTemp.ToString
            PTemp = Comm.ExecuteScalar
            If PTemp <> 0 Then
                Precio = PTemp / CTemp
            Else
                Precio = 0
            End If
            Cantidad += CTemp
            Precio = Precio * Cantidad
            Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + " where iddetalle=" + IdTemp.ToString
            Comm.ExecuteNonQuery()
            ID = IdTemp
            LlenaDatos()
            NuevoConcepto = False
        Else
            NuevoConcepto = True
            Comm.CommandText = "insert into tblmovimientosdetalles(idinventario,cantidad,precio,idmovimiento,descripcion,idalmacen,idalmacen2,idvariante,surtido,inventarioanterior,idmoneda) " + _
            "values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMovimiento.ToString + ",'" + Trim(Replace(Descripcion, "'", "''")) + "'," + IdAlmacen.ToString + "," + IdAlmacen2.ToString + "," + idVariante.ToString + ",0," + InventarioAnterior.ToString + "," + IdMoneda.ToString + ")"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select ifnull((select max(iddetalle) from tblmovimientosdetalles),0)"
            ID = Comm.ExecuteScalar
        End If

    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pIdMoneda As Integer, ByVal pidAlmacen As Integer, ByVal pIdAlmacen2 As Integer, ByVal pPrecio As Double)
        ID = pID
        Cantidad = pCantidad
        IdAlmacen = pidAlmacen
        IdAlmacen2 = pIdAlmacen2
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Comm.CommandText = "update tblmovimientosdetalles set cantidad=" + Cantidad.ToString + ",idalmacen=" + IdAlmacen.ToString + ",idalmacen2=" + IdAlmacen2.ToString + ",idmoneda=" + IdMoneda.ToString + ",precio=" + Precio.ToString + " where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
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
    Public Function Consulta(ByVal pIdMovimiento As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idmovimiento=" + pIdMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmovimientosdetalles")
        Return DS.Tables("tblmovimientosdetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdMovimiento As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.idmoneda,tvi.idvariante,tblproductos.clave as pclave,tvi.idinventario,(select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen) as almacen1,(select nombre from tblalmacenes where tblalmacenes.idalmacen=tvi.idalmacen2) as almacen2 from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad inner join tblproductosvariantes on tvi.idvariante=tblproductosvariantes.idvariante inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto  where tvi.idmovimiento=" + pIdMovimiento.ToString
        Return Comm.ExecuteReader
    End Function
End Class
