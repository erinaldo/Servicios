Public Class dbVentasPedidosProductos
    Public ID As Integer
    Public Idvariante As Integer
    Public Variante As dbProductosVariantes
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdPedido As Integer
    Public Descripcion As String
    Public NuevoConcepto As Boolean
    Public Iva As Double
    Public Extra As String
    Public Descuento As Double
    'Public IdAlmacen As Integer
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idvariante = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdPedido = 0
        Descripcion = ""
        Iva = 0
        Descuento = 0
        Extra = ""
        'IdAlmacen = 0
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
        Comm.CommandText = "select * from tblventaspedidosproductos where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idvariante = DReader("idvariante")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdPedido = DReader("idpedido")
            Descripcion = DReader("descripcion")
            Iva = DReader("iva")
            Descuento = DReader("descuento")
            Extra = DReader("extra")
            'IdAlmacen = DReader("idalmacen")
        End If
        DReader.Close()
        Variante = New dbProductosVariantes(Idvariante, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdPedido As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double) ', ByVal pIdAlmacen As Integer)
        Dim CTemp As Double
        Dim PTemp As Double
        Idvariante = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdPedido = pIdPedido
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        'IdAlmacen = pIdAlmacen
        Comm.CommandText = "select ifnull((select cantidad from tblventaspedidosproductos where idpedido=" + IdPedido.ToString + " and idvariante=" + Idvariante.ToString + "),-1)"
        CTemp = Comm.ExecuteScalar

        If CTemp > -1 Then
            Comm.CommandText = "select max(precio) from tblventaspedidosproductos where idpedido=" + IdPedido.ToString + " and idvariante=" + Idvariante.ToString
            PTemp = Comm.ExecuteScalar
            If PTemp <> 0 Then
                Precio = PTemp / CTemp
            Else
                Precio = 0
            End If
            Cantidad += CTemp
            Precio = Precio * Cantidad
            Comm.CommandText = "update tblventaspedidosproductos set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + " where idvariante=" + Idvariante.ToString + " and idpedido=" + IdPedido.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select iddetalle from tblventaspedidosproductos where idvariante=" + Idvariante.ToString + " and idpedido=" + IdPedido.ToString
            ID = Comm.ExecuteScalar
            LlenaDatos()
            NuevoConcepto = False
        Else
            NuevoConcepto = True
            Comm.CommandText = "insert into tblventaspedidosproductos(idpedido,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) values(" + IdPedido.ToString + "," + Idvariante.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + ")" '," + IdAlmacen.ToString + ")"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select ifnull((select max(iddetalle) from tblventaspedidosproductos),0)"
            ID = Comm.ExecuteScalar
        End If

    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        Comm.CommandText = "update tblventaspedidosproductos set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventaspedidosproductos where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdCotizacion As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblproductos.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura from tblventaspedidosproductos tvi inner join tblproductosvariantes on tvi.idvariante=tblproductosvariantes.idvariante inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto where tvi.idcotizacion=" + pIdCotizacion.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspedidosproductos")
        Return DS.Tables("tblventaspedidosproductos").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdCotizacion As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblproductos.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad from tblventaspedidosproductos tvi inner join tblproductosvariantes on tvi.idvariante=tblproductosvariantes.idvariante inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto inner join tbltiposcantidades on tblproductos.tipocontenido=tbltiposcantidades.idtipocantidad  where tvi.idpedido=" + pIdCotizacion.ToString
        Return Comm.ExecuteReader
    End Function
End Class
