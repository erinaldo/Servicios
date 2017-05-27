Public Class dbVentasRemisionesProductos
    Public ID As Integer
    Public IdVariante As Integer
    Public Variante As dbProductosVariantes
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdRemision As Integer
    Public Descripcion As String
    Public IdAlmacen As Integer
    Public NuevoConcepto As Boolean
    Dim Iva As Double
    Dim Extra As String
    Dim Descuento As Double
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdVariante = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdRemision = 0
        Descripcion = ""
        IdAlmacen = 0
        Iva = 0
        Extra = ""
        Descuento = 0
        NuevoConcepto = False
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub

    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblventasremisionesproductos where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            IdVariante = DReader("idvariante")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdRemision = DReader("idremision")
            IdAlmacen = DReader("idalmacen")
            Descripcion = DReader("descripcion")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
        End If
        DReader.Close()
        Variante = New dbProductosVariantes(IdVariante, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdVenta As Integer, ByVal pIdVariante As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double)
        Dim CTemp As Double
        Dim PTemp As Double
        IdVariante = pIdVariante
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdRemision = pIdVenta
        IdAlmacen = pIdAlmacen
        Descripcion = pDescripcion
        Descuento = pDescuento
        Iva = pIva
        Comm.CommandText = "select if(max(cantidad) is null,-1,cantidad) from tblventasremisionesproductos where idremision=" + IdRemision.ToString + " and idvariante=" + IdVariante.ToString
        CTemp = Comm.ExecuteScalar
        If CTemp > -1 Then
            Comm.CommandText = "select max(precio) from tblventasremisionesproductos where idremision=" + IdRemision.ToString + " and idvariante=" + IdVariante.ToString
            PTemp = Comm.ExecuteScalar
            If PTemp <> 0 Then
                Precio = PTemp / CTemp
            Else
                Precio = 0
            End If
            Cantidad += CTemp
            Precio = Precio * Cantidad
            Comm.CommandText = "update tblventasremisionesproductos set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + " where idvariante=" + IdVariante.ToString + " and idremision=" + IdRemision.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select iddetalle from tblventasremisionesproductos where idvariante=" + IdVariante.ToString + " and idremision=" + IdRemision.ToString
            ID = Comm.ExecuteScalar
            LlenaDatos()
            NuevoConcepto = False
        Else
            Comm.CommandText = "insert into tblventasremisionesproductos(idremision,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen) values(" + IdRemision.ToString + "," + IdVariante.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + IdAlmacen.ToString + ")"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select ifnull(max(iddetalle),0) from tblventasremisionesproductos"
            ID = Comm.ExecuteScalar
            NuevoConcepto = True
        End If

    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIVa As Double, ByVal pDescuento As Double)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = pIVa
        Descuento = pDescuento
        Comm.CommandText = "update tblventasremisionesproductos set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventasremisionesproductos where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvp.iddetalle,tblproductos.clave,tvp.descripcion,tvp.cantidad,tvp.precio,tblmonedas.abreviatura from tblventasremisionesproductos tvp inner join tblproductosvariantes on tvp.idvariante=tblproductosvariantes.idvariante inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto inner join tblmonedas on tvp.idmoneda=tblmonedas.idmoneda where tvp.idremision=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasremisionesproductos")
        Return DS.Tables("tblventasremisionesproductos").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvp.iddetalle,tblproductos.clave,tvp.descripcion,tvp.cantidad,tvp.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad from tblventasremisionesproductos tvp inner join tblproductosvariantes on tvp.idvariante=tblproductosvariantes.idvariante inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto inner join tblmonedas on tvp.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tbltiposcantidades.idtipocantidad=tblproductos.tipocontenido where tvp.idremision=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
End Class
