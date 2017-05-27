Public Class dbServiciosClasificaciones2
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public IdClasificacion1 As Integer
    Public Precio As Double
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        IdClasificacion1 = -1
        Precio = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblserviciosclasificaciones2 where idclasificacion2=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            IdClasificacion1 = DReader("idclasificacion")
            Precio = DReader("precio")
        End If
        DReader.Close()
    End Sub
 
    Public Sub Guardar(ByVal pNombre As String, ByVal pIdClasificacion As Integer, ByVal pPrecio As Double)
        Nombre = pNombre
        IdClasificacion1 = pIdClasificacion
        Precio = pPrecio
        Comm.CommandText = "insert into tblserviciosclasificaciones2(nombre,idclasificacion,precio) values('" + Replace(Nombre, "'", "''") + "'," + IdClasificacion1.ToString + "," + Precio.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pPrecio As Double)
        ID = pID
        Nombre = pNombre
        Precio = pPrecio
        Comm.CommandText = "update tblserviciosclasificaciones2 set nombre='" + Replace(Nombre, "'", "''") + "',precio=" + Precio.ToString + " where idclasificacion2=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblserviciosclasificaciones2 where idclasificacion2=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idclasificacion2,nombre,precio from tblserviciosclasificaciones2 where nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosclasificaciones2")
        Return DS.Tables("tblserviciosclasificaciones2").DefaultView
    End Function
End Class
