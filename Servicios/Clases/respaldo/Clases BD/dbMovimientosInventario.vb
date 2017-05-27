﻿Public Class dbMovimientosInventario
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    Public Cantidad As Double
    Public IdMovimiento As Integer
    Public Comentario As String
    Public NuevoConceto As Boolean
    Public IdAlmacen1 As Integer
    Public IdAlmacen2 As Integer
    Public ExistenciaAnt As Double
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdMovimiento = 0
        Comentario = ""
        IdAlmacen1 = 0
        IdAlmacen2 = 0
        ExistenciaAnt = 0
        NuevoConceto = False
        Comm.Connection = Conexion

    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblmovimientosinventario where id=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdMovimiento = DReader("idmovimiento")
            Comentario = DReader("comentario")
            IdAlmacen1 = DReader("idalmacen1")
            IdAlmacen2 = DReader("idalmacen2")
            ExistenciaAnt = DReader("existenciaant")
        End If
        DReader.Close()
        Inventario = New dbInventario(Idinventario, Comm.Connection)

    End Sub
    Public Sub Guardar(ByVal pIdMovimiento As Integer, ByVal pIdInventario As Integer, ByVal pCantidad As Double, ByVal pidAlmacen1 As Integer, ByVal pIdAlmacen2 As Integer, ByVal pComentario As String, ByVal pExistenciaAnt As Double)
        Dim CTemp As Double
        Idinventario = pIdInventario
        Cantidad = pCantidad
        IdMovimiento = pIdMovimiento
        Comentario = pComentario
        IdAlmacen1 = pidAlmacen1
        IdAlmacen2 = pIdAlmacen2
        ExistenciaAnt = pExistenciaAnt
        Comm.CommandText = "select if(max(cantidad) is null,-1,cantidad) from tblmovimientosinventario where idmovimiento=" + IdMovimiento.ToString + " and idinventario=" + Idinventario.ToString
        CTemp = Comm.ExecuteScalar
        If CTemp > -1 Then
            Cantidad += CTemp
            Comm.CommandText = "update tblmovimientosinventario set cantidad=" + Cantidad.ToString + " where idinventario=" + Idinventario.ToString + " and idmovimiento=" + IdMovimiento.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select id from tblmovimientosinventario where idinventario=" + Idinventario.ToString + " and idmovimiento=" + IdMovimiento.ToString
            ID = Comm.ExecuteScalar
            LlenaDatos()
            NuevoConceto = False
        Else
            NuevoConceto = True
            Comm.CommandText = "insert into tblmovimientosinventario(idmovimiento,idinventario,cantidad,comentario,idalmacen1,idalmacen2,existenciaant) values(" + IdMovimiento.ToString + "," + Idinventario.ToString + "," + Cantidad.ToString + ",'" + Replace(Comentario, "'", "''") + "'," + IdAlmacen1.ToString + "," + IdAlmacen2.ToString + "," + ExistenciaAnt.ToString + ")"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select ifnull((select max(id) from tblmovimientosinventario),0)"
            ID = Comm.ExecuteScalar
        End If

    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pComentario As String)
        ID = pID
        Cantidad = pCantidad
        Comentario = pComentario
        Comm.CommandText = "update tblmovimientosinventario set cantidad=" + Cantidad.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "' where id=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblmovimientosinventario where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdMovimiento As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select id,tblmovimientosinventario.cantidad,tblinventario.clave,tblinventario.nombre,(select nombre from tblalmacenes where idalmacen=tblmovimientosinventario.idalmacen1) as almacen1,(select nombre from tblalmacenes where idalmacen=tblmovimientosinventario.idalmacen2) as almacen2 from tblmovimientosinventario inner join tblinventario on tblmovimientosinventario.idinventario=tblinventario.idinventario where idmovimiento=" + pIdMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmovimientosinventario")
        Return DS.Tables("tblmovimientosinventario").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdMovimiento As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select id,tblmovimientosinventario.cantidad,tblinventario.clave,tblinventario.nombre,(select nombre from tblalmacenes where idalmacen=tblmovimientosinventario.idalmacen1) as almacen1,(select nombre from tblalmacenes where idalmacen=tblmovimientosinventario.idalmacen2) as almacen2 from tblmovimientosinventario inner join tblinventario on tblmovimientosinventario.idinventario=tblinventario.idinventario where idmovimiento=" + pIdMovimiento.ToString
        Return Comm.ExecuteReader
    End Function
End Class
