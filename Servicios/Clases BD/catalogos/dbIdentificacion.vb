Imports MySql.Data.MySqlClient
Public Class dbIdentificacion
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Nombre As String
    Public Ididentificacion As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)

        Comm.Connection = Conexion
        'Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Function esRepetida(ByVal pNombre As String) As Boolean
        Dim repetida As Boolean = False
        Comm.CommandText = "select count(nombre) from tblidentificacion where nombre='" + pNombre + "'"
        If Comm.ExecuteScalar > 0 Then
            repetida = True
        End If
        Return repetida
    End Function
    Public Sub Guardar(ByVal pNombre As String)
        Comm.CommandText = "insert into tblidentificacion (nombre,idUsuarioAlta,fechaAlta,horaAlta,idUsuariarioCambio,fechaCambio,horaCambio) values('" + pNombre + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        Comm.ExecuteNonQuery()

    End Sub
    Public Sub LlenaDatos(ByVal pId As Integer)
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select ididentificacion,nombre from tblidentificacion where ididentificacion=" + pId.ToString
        DR = Comm.ExecuteReader
        If DR.Read Then
            Ididentificacion = pId
            Nombre = DR("nombre")
        End If
        DR.Close()
    End Sub


    Public Sub Modificar(ByVal pId As Integer, ByVal pNombre As String)
        'Modifica
        Comm.CommandText = "update tblidentificacion set nombre='" + pNombre + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idIdentificacion=" + pId.ToString()
        Comm.ExecuteNonQuery()
    End Sub

    Public Function filtroClasificacion(ByVal pNombre As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select ididentificacion,nombre from tblidentificacion  where nombre LIKE '%" + pNombre.ToString() + "%' "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblidentificacion ")
        Return DS.Tables("tblidentificacion ")
    End Function

    Public Sub Eliminar(ByVal pId As Integer)
        'Eliminar
        Comm.CommandText = "delete from tblidentificacion where idIdentificacion=" + pId.ToString()
        Comm.ExecuteNonQuery()

    End Sub
    Public Function idProximo() As Integer
        Dim sig As Integer
        Comm.CommandText = "select count(idIdentificacion) from tblidentificacion"
        sig = Comm.ExecuteScalar
        If sig > 0 Then
            Comm.CommandText = "select max(idIdentificacion) from tblidentificacion"
            sig = Comm.ExecuteScalar
        End If


        Return sig + 1
    End Function

End Class
