Imports MySql.Data.MySqlClient
Public Class dbComplementoIneContabilidad
    Private comm As New MySqlCommand
    Public id As Integer
    Public clave As Integer
    Public idEntidad As Integer

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        id = -1
        clave = -1
        idEntidad = -1
    End Sub

    Public Sub New(ByVal id As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.id = id
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblcomplementoinecontabilidad where id=" + id.ToString()
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            clave = dr("idcontabilidad")
            idEntidad = dr("idEntidad")
        End While
        dr.Close()
    End Sub

    Public Function guardar(ByVal clave As Integer, ByVal idEntidad As Integer) As Boolean
        comm.CommandText = "insert into tblcomplementoinecontabilidad (idcontabilidad,identidad) values(" + clave.ToString() + "," + idEntidad.ToString() + ");"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByVal id As Integer, ByVal clave As Integer, ByVal idEntidad As Integer) As Boolean
        comm.CommandText = "update tblcomplementoinecontabilidad set idcontabilidad=" + clave.ToString() + ", identidad=" + idEntidad.ToString() + " where id=" + id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal id As Integer) As Boolean
        comm.CommandText = "delete from tblcomplementoinecontabilidad where id=" + id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function buscaClaves(ByVal entidad As Integer) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select id,idcontabilidad from tblcomplementoinecontabilidad where idEntidad=" + entidad.ToString()
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "claves")
        Return ds.Tables("claves").DefaultView
    End Function

    Public Function listaClaves(ByVal entidad As Integer) As String()
        Dim claves As New List(Of String)
        comm.CommandText = "select idcontabilidad from tblcomplementoinecontabilidad where idEntidad=" + entidad.ToString()
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            claves.Add(dr("idcontabilidad"))
        End While
        dr.Close()
        Return claves.ToArray
    End Function

    Public Function buscaEntidadClave(ByVal entidad As Integer, ByVal clave As Integer) As Boolean
        comm.CommandText = "select id from tblcomplementoinecontabilidad where identidad=" + entidad.ToString + " and idcontabilidad=" + clave.ToString() + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.id = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function
End Class
