Public Class dbProductosRecetas
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdInventario As Integer
    Public Cantidad As Double
    Public IdVariante As Double
    Public IdProducto As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdInventario = -1
        Cantidad = 0
        IdVariante = 0
        IdProducto = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblproductosrecetas where idreceta=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdInventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdVariante = DReader("idvariante")
            IdProducto = DReader("idproducto")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pIdVariante As Integer, ByVal pIdInventario As Integer, ByVal pIdProducto As Integer, ByVal pCantidad As Double)
        IdInventario = pIdInventario
        Cantidad = pCantidad
        IdVariante = pIdVariante
        IdProducto = pIdProducto
        Comm.CommandText = "insert into tblproductosrecetas(idinventario,cantidad,idvariante,idproducto) values(" + IdInventario.ToString + "," + Cantidad.ToString + "," + IdVariante.ToString + "," + IdProducto.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double)
        ID = pID
        Cantidad = pCantidad
        Comm.CommandText = "update tblproductosrecetas set cantidad=" + Cantidad.ToString + " where idreceta=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblproductosrecetas where idreceta=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVariante As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idreceta,if(tblproductos.idproducto<>1,tblproductos.nombre,tblinventario.nombre) as rnombre,tblproductosrecetas.cantidad,tbltiposcantidades.abreviatura as abre from tblproductosrecetas inner join tblinventario on tblproductosrecetas.idinventario=tblinventario.idinventario inner join tblproductos on tblproductosrecetas.idproducto=tblproductos.idproducto inner join tbltiposcantidades on (tbltiposcantidades.idtipocantidad=if(tblproductos.idproducto<>1,tblproductos.tipocontenido,tblinventario.tipocontenido)) where tblproductosrecetas.idvariante=" + pIdVariante.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrecetas")
        Return DS.Tables("tblrecetas").DefaultView
    End Function
    Public Function ConsultaExistencias(ByVal pIdVariante As Integer) As String
        Comm.CommandText = "select idreceta,if(tblproductos.idproducto<>1,tblproductos.nombre,tblinventario.nombre) as rnombre,tblproductosrecetas.cantidad,tbltiposcantidades.abreviatura as abre from tblproductosrecetas inner join tblinventario on tblproductosrecetas.idinventario=tblinventario.idinventario inner join tblproductos on tblproductosrecetas.idproducto=tblproductos.idproducto inner join tbltiposcantidades on (tbltiposcantidades.idtipocantidad=if(tblproductos.idproducto<>1,tblproductos.tipocontenido,tblinventario.tipocontenido)) where tblproductosrecetas.idvariante=" + pIdVariante.ToString
        Return Comm.ExecuteScalar
    End Function
End Class
