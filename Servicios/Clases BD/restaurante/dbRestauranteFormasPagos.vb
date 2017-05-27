Imports MySql.Data.MySqlClient
Public Class dbRestauranteFormasPagos
    Public Property id As Integer
    Public Property nombre As String
    Public Property clave As String

    Private comm As New MySqlCommand

    Public Sub New()

    End Sub

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        id = -1
        nombre = ""
        clave = ""
    End Sub

    Public Sub New(ByVal id As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.id = id
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblrestauranteformaspagos where id=" + id.ToString() + ";"
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        While dr.Read()
            id = dr("id")
            nombre = dr("nombre")
            clave = dr("clave")
        End While
        dr.Close()
    End Sub

    Public Sub agregar(ByVal nombre As String, ByVal clave As String)
        comm.CommandText = "insert into tblrestauranteformaspagos (nombre,clave) values('" + nombre + "','" + clave + "');"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("no se pudo agregar: " + ex.ToString())
        End Try
    End Sub

    Public Sub modificar(ByVal id As Integer, ByVal nombre As String, ByVal clave As String)
        comm.CommandText = "update tblrestauranteformaspagos set nombre='" + nombre + "', clave='" + clave + "' where id" + id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub eliminar(ByVal id As Integer)
        comm.CommandText = "delete from tblrestauranteformaspagos where id=" + id.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Function buscar(ByVal clave As String) As Boolean
        comm.CommandText = "select id from tblrestauranteformaspagos where clave like '%" + clave + "%';"
        Dim res As Integer = comm.ExecuteScalar
        If res > 0 Then
            id = res
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function buscarId(ByVal id As Integer) As Boolean
        comm.CommandText = "select id from tblrestauranteformaspagos where id=" + id.ToString() + ";"
        Dim res As Integer = comm.ExecuteScalar
        If res > 0 Then
            Me.id = res
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function vistaFormas() As DataView
        comm.CommandText = "select * from tblrestauranteformaspagos;"
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "formas")
        Return ds.Tables("formas").DefaultView()
    End Function
End Class
