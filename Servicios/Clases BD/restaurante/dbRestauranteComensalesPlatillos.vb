Imports MySql.Data.MySqlClient
Public Class dbRestauranteComensalesPlatillos
    Private comm As New MySqlCommand
    Private comensal As Integer
    Private iddetalle As Integer

    Public Sub New(ByVal comensal As Integer, ByVal conexion As MySqlConnection)
        Me.comensal = comensal
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Function agregar(ByVal comensal As Integer, ByVal iddetalle As Integer) As Boolean
        comm.CommandText = "insert into tblrestaurantecomensalesplatillos(idcomensal,iddetalle) values(" + comensal.ToString() + "," + iddetalle.ToString() + ");"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByVal comensal As Integer, ByVal iddetalle As Integer) As Boolean
        comm.CommandText = "update tblrestaurantecomensalesplatillos set idcomensal=" + comensal.ToString() + ", iddetalle=" + iddetalle.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminarComensal(ByVal comensal As Integer) As Boolean
        comm.CommandText = "delete from tblrestaurantecomensalesplatillos where idcomensal=" + comensal.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminarDetalle(ByVal iddetalle As Integer) As Boolean
        comm.CommandText = "delete from tblrestaurantecomesalesplatillos where iddetalle=" + iddetalle.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function listaDetalles(ByVal comensal As Integer, ByVal estado As Integer) As List(Of Integer)
        Dim lista As New List(Of Integer)
        If estado > -1 Then
            comm.CommandText = "select c.iddetalle from tblrestaurantecomensalesplatillos as c inner join tblrestauranteventasdetalles as d on c.iddetalle=d.iddetalle where c.idcomensal=" + comensal.ToString() + " and d.estado=" + estado.ToString()
        Else
            comm.CommandText = "select iddetalle from tblrestaurantecomensalesplatillos where idcomensal=" + comensal.ToString
        End If
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            lista.Add(dr("iddetalle"))
        End While
        dr.Close()
        Return lista
    End Function


End Class
