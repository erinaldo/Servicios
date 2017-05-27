Imports MySql.Data.MySqlClient
Public Class dbRestauranteColores
    Private comm As New MySqlCommand
    Public id As Integer
    Public idClas As Integer
    Public color As String

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal id As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.id = id
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblrestaurantecolores where id=" + id.ToString
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            Me.idClas = dr("idclasificacion")
            Me.color = dr("color")
        End While
        dr.Close()
    End Sub

    Public Function agregar(ByVal idClas As Integer, ByVal color As String) As Boolean
        comm.CommandText = "insert into tblrestaurantecolores(idclasificacion,color) values(" + idClas.ToString + ",'" + color + "');"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByVal id As Integer, ByVal idClas As Integer, color As String) As Boolean
        comm.CommandText = "update tblrestaurantecolores set idclasificacion=" + idClas.ToString + ", color='" + color + "' where id=" + id.ToString + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal id As Integer) As Boolean
        comm.CommandText = "delete from tblrestaurantecolores where id=" + id.ToString + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function buscar(ByVal id As Integer) As Boolean
        comm.CommandText = "select id from tblrestaurantecolores where id=" + id.ToString() + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.id = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function
    Public Function buscarPorClasifiacacion(ByVal idClas As Integer) As Boolean
        comm.CommandText = "select id from tblrestaurantecolores where idclasificacion=" + idClas.ToString() + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.id = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function
End Class
