Public Class dbContabilidadClasificacionesPolizas
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Dim ID As Integer = -1
    Dim nombre As String = ""
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub

    Public Function buscar(ByVal pNombre As String)
        Dim DS As New DataSet
        Comm.CommandText = "select id,nombre from tblcontabilidadclas where nombre like '%" + Replace(pNombre, "'", "''") + "%';"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblClas")
        Return DS.Tables("tblclas")
    End Function
    Public Sub guardar(ByVal pNombre As String)
        Comm.CommandText = "insert into tblcontabilidadclas(nombre) values('" + Replace(pNombre, "'", "''") + "')"
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub modificar(ByVal pID As Integer, ByVal pNombre As String)
        Comm.CommandText = "update tblcontabilidadclas set nombre='" + Replace(pNombre, "'", "''") + "' where id=" + pID.ToString
        Comm.CommandText = Replace(Comm.CommandText, "|", "")
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcontabilidadclas where id=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
End Class
