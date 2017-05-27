Public Class dbPedidosDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdPedido As Integer
    Public Surtido As Double
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdPedido = 0
        Surtido = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub ModificarSurtido(ByVal pCantidad As Double, ByVal pId As Integer)
        Comm.CommandText = "update tblpedidosdetalles set surtido=" + Cantidad.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblpedidosdetalles where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdPedido = DReader("idpedido")
            Surtido = DReader("surtido")
        End If
        DReader.Close()
        Inventario = New dbInventario(Idinventario, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdPedido As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer)
        Dim CTemp As Double
        Dim PTemp As Double
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdPedido = pIdPedido
        Comm.CommandText = "select if(max(cantidad) is null,-1,cantidad) from tblpedidosdetalles where idpedido=" + IdPedido.ToString + " and idinventario=" + Idinventario.ToString
        CTemp = Comm.ExecuteScalar
        If CTemp > -1 Then
            Comm.CommandText = "select max(precio) from tblpedidosdetalles where idpedido=" + IdPedido.ToString + " and idinventario=" + Idinventario.ToString
            PTemp = Comm.ExecuteScalar
            If PTemp <> 0 Then
                Precio = PTemp / CTemp
            Else
                Precio = 0
            End If
            Cantidad += CTemp
            Precio = Precio * Cantidad
            Comm.CommandText = "update tblpedidosdetalles set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + " where idinventario=" + Idinventario.ToString + " and idpedido=" + IdPedido.ToString
        Else
            Comm.CommandText = "insert into tblpedidosdetalles(idinventario,cantidad,precio,idmoneda,idpedido,surtido) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + "," + IdMoneda.ToString + "," + IdPedido.ToString + ",0)"
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select if(max(iddetalle) is null,0,max(iddetalle)) from tblpedidosdetalles"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Comm.CommandText = "update tblpedidosdetalles set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblpedidosdetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdcompra As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblpedidosdetalles.iddetalle,tblpedidosdetalles.cantidad,tblinventario.clave,tblinventario.nombre,tblpedidosdetalles.precio,tblmonedas.abreviatura from tblpedidosdetalles inner join tblinventario on tblpedidosdetalles.idinventario=tblinventario.idinventario inner join tblmonedas on tblpedidosdetalles.idmoneda=tblmonedas.idmoneda where tblpedidosdetalles.idpedido=" + pIdcompra.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpedidosdetalles")
        Return DS.Tables("tblpedidosdetalles").DefaultView
    End Function
End Class
