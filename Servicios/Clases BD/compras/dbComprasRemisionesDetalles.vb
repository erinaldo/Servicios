Imports MySql.Data.MySqlClient
Public Class dbComprasRemisionesDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    'Public Producto As dbProductosVariantes
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
    Public IdAlmacen As Integer
    Public Surtido As Double
    Public IEPS As Double
    Public Ubicacion As String
    Public Tarima As String
    Public ivaRetenido As Double
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdPedido = 0
        Descripcion = ""
        Iva = 0
        Extra = ""
        IEPS = 0
        ivaRetenido = 0
        Descuento = 0
        IdAlmacen = 0
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
        Comm.CommandText = "select crd.*,ifnull(cru.ubicacion,'') ubicacion, ifnull(cru.tarima,'') tarima from tblcomprasremisionesdetalles crd left outer join tblcomprasremisionesubicaciones cru on crd.iddetalle=cru.iddetalle where crd.iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdPedido = DReader("idremision")
            Descripcion = DReader("descripcion")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
            IdAlmacen = DReader("idalmacen")
            Surtido = DReader("surtido")
            IEPS = DReader("IEPS")
            ivaRetenido = DReader("ivaRetenido")
            Ubicacion = DReader("ubicacion")
            Tarima = DReader("tarima")
        End If
        DReader.Close()
        Inventario = New dbInventario(Idinventario, Comm.Connection)
        'If IdVariante > 1 Then Producto = New dbProductosVariantes(IdVariante, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdRemision As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdAlmacen As Integer, ByVal pIEPS As Double, ByVal pivaRetenido As Double, pUbicacion As String, pTarima As String)
        'Dim CTemp As Double
        'Dim PTemp As Double
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdPedido = pIdRemision
        Descripcion = pDescripcion
        IEPS = pIEPS
        ivaRetenido = pivaRetenido
        Iva = pIva
        Descuento = pDescuento
        IdAlmacen = pIdAlmacen
        Ubicacion = pUbicacion
        Tarima = pTarima
        NuevoConcepto = True
        Comm.CommandText = "insert into tblcomprasremisionesdetalles(idremision, idinventario, cantidad, precio, descripcion, idmoneda, iva, extra, descuento, idalmacen, surtido, IEPS, ivaRetenido) values (" + IdPedido.ToString + "," + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + "," + IdAlmacen.ToString + ", 0" + "," + pIEPS.ToString() + " , " + pivaRetenido.ToString() + ");"
        Comm.CommandText += "select ifnull(last_insert_id(),0);"
        ID = Comm.ExecuteScalar
        If pUbicacion <> "" Then
            Comm.CommandText = "insert into tblcomprasremisionesubicaciones (iddetalle, cantidad, surtido, ubicacion, tarima) values(" + ID.ToString + ", " + Cantidad.ToString() + ", 0, @ubicacion, @tarima);"
            Comm.Parameters.Add(New mysqlparameter("@ubicacion", Ubicacion))
            Comm.Parameters.Add(New MySqlParameter("@tarima", If(Tarima = "", DBNull.Value, Tarima)))
            Comm.ExecuteNonQuery()
            Comm.Parameters.Clear()
        End If

    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal piva As Double, ByVal pDescuento As Double, ByVal pIEPS As Double, ByVal pivaRetenido As Double, pUbicacion As String, pTarima As String)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = piva
        IEPS = pIEPS
        ivaRetenido = pivaRetenido
        Descuento = pDescuento
        Ubicacion = pUbicacion
        Tarima = pTarima
        Comm.CommandText = "update tblcomprasremisionesdetalles set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + " ,IEPS=" + IEPS.ToString() + " ,ivaRetenido=" + ivaRetenido.ToString() + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
        If pUbicacion <> "" Then
            Comm.CommandText = "update tblcomprasremisionesubicaciones set ubicacion=@ubicacion, tarima=@ptarima ,cantidad=" + pCantidad.ToString + " where iddetalle=" + pID.ToString
            Comm.Parameters.Add(New mysqlparameter("@ubicacion", Ubicacion))
            Comm.Parameters.Add(New MySqlParameter("@tarima", If(Tarima = "", DBNull.Value, Tarima)))
            Comm.ExecuteNonQuery()
            Comm.Parameters.Clear()
        End If
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcomprasremisionesdetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdPedido As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura from tblcomprasremisionesdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idpedido=" + pIdPedido.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasremisionesdetalles")
        Return DS.Tables("tblcomprasremisionesdetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdPedido As Integer, pNada As Byte) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,concat(convert(tvi.descripcion using utf8),spdadetalleslotes(tvi.idremision,tvi.iddetalle,1," + pNada.ToString + "),spdadetallesaduanaotros(tvi.idremision,tvi.iddetalle,1," + pNada.ToString + ")) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.idinventario from tblcomprasremisionesdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad where tvi.idremision=" + pIdPedido.ToString
        Return Comm.ExecuteReader
    End Function

End Class
