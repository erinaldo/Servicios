Imports MySql.Data.MySqlClient
Public Class dbEmpeniosClasificacion
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        
        Comm.Connection = Conexion
        'Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Function esRepetida(ByVal pNombre As String) As Boolean
        Dim repetida As Boolean = False
        Comm.CommandText = "select count(nombre) from tblempeniosclasificacion where nombre='" + pNombre + "'"
        If Comm.ExecuteScalar > 0 Then
            repetida = True
        End If
        Return repetida
    End Function
    Public Sub Guardar(ByVal pNombre As String)
        Comm.CommandText = "insert into tblempeniosclasificacion (nombre,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(pNombre, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        Comm.ExecuteNonQuery()

    End Sub

    Public Sub Modificar(ByVal pId As Integer, ByVal pNombre As String)
        'Modifica
        Comm.CommandText = "update tblempeniosclasificacion set nombre='" + Replace(pNombre, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idClasificacion=" + pId.ToString()
        Comm.ExecuteNonQuery()
    End Sub

    Public Function filtroClasificacion(ByVal pNombre As String) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idclasificacion,nombre from tblempeniosclasificacion  where nombre LIKE '%" + pNombre.ToString() + "%' "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempeniosclasificacion ")
        Return DS.Tables("tblempeniosclasificacion ")
    End Function

    Public Sub Eliminar(ByVal pId As Integer)
        'Eliminar
        Comm.CommandText = "delete from tblempeniosclasificacion where idClasificacion=" + pId.ToString()
        Comm.ExecuteNonQuery()

    End Sub
    Public Function idProximo() As Integer
        Dim sig As Integer
        Comm.CommandText = "select count(idClasificacion) from tblempeniosclasificacion"
        sig = Comm.ExecuteScalar
        If sig > 0 Then
            Comm.CommandText = "select max(idClasificacion) from tblempeniosclasificacion"
            sig = Comm.ExecuteScalar
        End If


        Return sig + 1
    End Function
End Class
