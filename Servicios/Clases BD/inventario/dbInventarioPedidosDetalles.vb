Public Class dbInventarioPedidosDetalles
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public IdDetalle As Integer
    Public IdPedido As Integer
    Public Autorizado As Byte
    Public IdInventario As Integer
    Public Cantidad As Double
    Public CantidadAut As Double
    Public Precio As Double
    Public Inventario As dbInventario
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
        Inicializa()
    End Sub
    Public Sub Inicializa()
        IdDetalle = 0
        IdPedido = 0
        Autorizado = 0
        IdInventario = 0
        Cantidad = 0
        CantidadAut = 0
        Precio = 0
    End Sub
    Public Sub Guardar(pIdPedido As Integer, PIdinventario As Integer, pCantidad As Double, pPrecio As Double)
        Comm.CommandText = "insert into tblinventariopedidosdetalles(idpedido,idinventario,autorizado,cantidad,cantidadaut,precio) values(" +
            "" + pIdPedido.ToString + "," + PIdinventario.ToString + ",0," + pCantidad.ToString + "," + pCantidad.ToString + "," + pPrecio.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(pidDetalle As Integer, pCantidad As Double, pPrecio As Double, pCantidadAut As Double)
        Comm.CommandText = "update tblinventariopedidosdetalles set cantidad=" + pCantidad.ToString + ",cantidadaut=" + pCantidadAut.ToString + ",precio=" + pPrecio.ToString + " where iddetalle=" + pidDetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(pIdDetalle As Integer)
        Comm.CommandText = "delete from tblinventariopedidosdetalles where iddetalle=" + pIdDetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Autorizar(pIdDetalle As Integer, pAutorizar As Byte)
        Comm.CommandText = "update tblinventariopedidosdetalles set autorizado=" + pAutorizar.ToString + " where iddetalle=" + pIdDetalle.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(pIdPedido As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select ip.iddetalle,ip.autorizado,ip.cantidad,ip.cantidadaut,i.clave,i.nombre,if(ip.cantidadaut<>0,round(ip.precio/ip.cantidadaut,2),0) pu,ip.precio from tblinventariopedidosdetalles ip inner join tblinventario i on ip.idinventario=i.idinventario where idpedido=" + pIdPedido.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpedidosd")
        Return DS.Tables("tblpedidosd").DefaultView
    End Function
    Public Function ConsultaReader(pIdPedido As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select ip.iddetalle,if(ip.autorizado=0,'','*') autorizado,ip.cantidad,ip.cantidadaut,i.clave,i.nombre descripcion,if(ip.cantidadaut<>0,round(ip.precio/ip.cantidadaut,2),0) pu,ip.precio,tc.abreviatura from tblinventariopedidosdetalles ip inner join tblinventario i on ip.idinventario=i.idinventario inner join tbltiposcantidades tc on i.tipocontenido=tc.idtipocantidad where idpedido=" + pIdPedido.ToString
        Return Comm.ExecuteReader
    End Function
    Public Sub LlenaDatos(pIdDetalle As Integer)
        IdDetalle = pIdDetalle
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblinventariopedidosdetalles where iddetalle=" + pIdDetalle.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read Then
            IdPedido = DReader("idpedido")
            Autorizado = DReader("autorizado")
            IdInventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            CantidadAut = DReader("cantidadaut")
            Precio = DReader("precio")
        End If
        DReader.Close()
        Inventario = New dbInventario(IdInventario, Comm.Connection)
    End Sub
End Class
