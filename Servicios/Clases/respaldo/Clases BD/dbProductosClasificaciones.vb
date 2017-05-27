Public Class dbProductosClasificaciones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblproductosclasificaciones where idclasificacion=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String)
        Nombre = pNombre
        Comm.CommandText = "insert into tblproductosclasificaciones(nombre) values('" + Replace(Nombre, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String)
        ID = pID
        Nombre = pNombre
        Comm.CommandText = "update tblproductosclasificaciones set nombre='" + Replace(Nombre, "'", "''") + "' where idclasificacion=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblproductosclasificaciones where idclasificacion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idclasificacion,nombre from tblproductosclasificaciones where idclasificacion>1 and nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproductosclasificaciones")
        Return DS.Tables("tblproductosclasificaciones").DefaultView
    End Function
End Class
