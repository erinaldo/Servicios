Imports MySql.Data.MySqlClient
Public Class dbComplementoExportacioDestinatario
    Private comm As New MySqlCommand
    Public idDestinatario As Integer
    Public numRegIdTrip As String
    Public rfc As String
    Public curp As String
    Public nombre As String
    Public idComplemento As Integer
    Public guardado As Integer

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal idDestinatario As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.idDestinatario = idDestinatario
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select *from tblcomercioexteriordestinatario where iddestinatario=" + idDestinatario.ToString()
        Dim dr As MySqlDataReader = comm.ExecuteReader()
        While dr.Read()
            numRegIdTrip = dr("numRegIdTrip")
            rfc = dr("rfc")
            curp = dr("curp")
            nombre = dr("nombre")
            idComplemento = dr("idComplemento")
            guardado = dr("guardado")
        End While
        dr.Close()
    End Sub

    Public Function guardar(ByVal numRegIdTrip As String, ByVal rfc As String, ByVal curp As String, ByVal nombre As String, ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "insert into tblcomercioexteriordestinatario(numRegIdTrip,rfc,curp,nombre,idComplemento,guardado)"
        comm.CommandText += "values('" + numRegIdTrip + "','" + rfc + "','" + curp + "','" + nombre + "'," + idComplemento.ToString() + "," + CInt(Estados.Inicio).ToString() + ");"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByVal idDestinatario As Integer, ByVal numRegIdTrip As String, ByVal rfc As String, ByVal curp As String, ByVal nombre As String, ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "update tblcomercioexteriordestinatario set numregidtrip='" + numRegIdTrip + "', rfc='" + rfc + "', curp='" + curp + "', numbre='" + nombre + "', idComplemento=" + idComplemento.ToString()
        comm.CommandText += " where idDestinatario=" + idDestinatario.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal idDestinatario As Integer) As Boolean
        comm.CommandText = "delete from tblcomercioexteriordestinatario where iddestinatario=" + idDestinatario.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function buscar(ByVal idDestinatario As Integer) As Boolean
        comm.CommandText = "select idDestinatario from tbl comercioexteriordestinatario where iddestinatario=" + idDestinatario.ToString()
        Dim i As Integer = comm.ExecuteScalar()
        If i > 0 Then
            Me.idDestinatario = idDestinatario
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function buscaComplemento(ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "select idDestinatario from tblcomercioexteriordestinatario where idcomplemento=" + idComplemento.ToString()
        Dim i As Integer = comm.ExecuteScalar()
        If i > 0 Then
            Me.idDestinatario = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function guardarTodo(ByVal idDestinatario As Integer, ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "update tblcomercioexteriordestinatario set guardado=" + CInt(Estados.Guardada).ToString() + " where idDestinatario=" + idDestinatario.ToString() + " and idComplemento=" + idComplemento.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
