Imports MySql.Data.MySqlClient
Public Class dbComplementoIneEntidad
    Private comm As New MySqlCommand
    Public id As Integer
    Public ambito As String
    Public idComplemento As String
    Public entidad As String

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        id = -1
        ambito = ""
        idComplemento = -1
        entidad = ""
    End Sub

    Public Sub New(ByVal id As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.id = id
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "Select * from tblcomplementoineentidad where idtblcomplementoineentidad=" + id.ToString() + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            ambito = dr("ambito")
            idComplemento = dr("idcomplemento")
            entidad = dr("entidad")
        End While
        dr.Close()
    End Sub

    Public Function guardar(ByVal entidad As String, ByVal ambito As String, ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "insert into tblcomplementoineentidad(idcomplemento,entidad,ambito) values(" + idComplemento.ToString() + ",'" + entidad + "','" + ambito + "');"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function actualizar(ByVal id As Integer, ByVal entidad As String, ByVal ambito As String, ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "update tblcomplementoineentidad set entidad='" + entidad + "', ambito='" + ambito + "', idComplemento=" + idComplemento.ToString() + " where idtblcomplementoineentidad=" + id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal id As Integer) As Boolean
        comm.CommandText = "delete from tblcomplementoineentidad where idtblcomplementoineentidad=" + id.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function buscar(ByVal idComplemento As Integer) As MySqlDataReader
        comm.CommandText = "select * from tblcomplementoineentidad where idComplemento=" + idComplemento.ToString() + ";"
        Return comm.ExecuteReader
    End Function

    Public Function buscarEntidad(ByVal entidad As Integer) As Boolean
        comm.CommandText = "select idtblcomplementoineentidad where idtblcomplementoineentidad=" + entidad.ToString()
        Dim res As Integer = comm.ExecuteScalar
        If res > 0 Then
            id = res
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function buscaComplementoEntidad(ByVal complemento As Integer, ByVal entidad As String) As Boolean
        comm.CommandText = "select idtblcomplementoineentidad from tblcomplementoineentidad where idcomplemento=" + complemento.ToString() + " and entidad='" + entidad + "'"
        Dim res As Integer = comm.ExecuteScalar
        If res > 0 Then
            id = res
            llenaDatos()
            Return True
        End If
        Return False
    End Function
End Class
