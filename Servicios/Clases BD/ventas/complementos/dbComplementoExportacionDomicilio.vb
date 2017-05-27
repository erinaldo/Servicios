Imports MySql.Data.MySqlClient
Public Class dbComplementoExportacionDomicilio
    Private comm As New MySqlCommand
    Public idDomicilio As Integer
    Public calle As String
    Public numExterior As String
    Public numInterior As String
    Public colonia As String
    Public localidad As String
    Public referencia As String
    Public municipio As String
    Public estado As String
    Public pais As String
    Public cp As String
    Public idDestinatario As Integer
    Public guardado As Integer

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal idDomicilio As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.idDomicilio = idDomicilio
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblcomercioexteriordomicilio where iddomicilio=" + idDomicilio.ToString
        Dim dr As MySqlDataReader = comm.ExecuteReader()
        While dr.Read()
            calle = dr("calle")
            numExterior = dr("numexterior")
            numInterior = dr("numinterior")
            colonia = dr("colonia")
            localidad = dr("localidad")
            referencia = dr("referencia")
            municipio = dr("municipio")
            estado = dr("estado")
            pais = dr("pais")
            cp = dr("cp")
            idDestinatario = dr("iddestinatario")
            guardado = dr("guardado")
        End While
        dr.Close()
    End Sub

    Public Function guardar(ByVal calle As String, ByVal numExterior As String, ByVal numInterior As String, ByVal colonia As String, ByVal localidad As String, ByVal referencia As String, ByVal municipio As String, ByVal estado As String, pais As String, cp As String, idDestinatario As Integer) As Boolean
        comm.CommandText = "insert into tblcomercioexteriordomicilio(calle,numexterior,numinterior,colonia,localidad,referencia,municipio,estado,pais,cp,iddestinatario,guardado)"
        comm.CommandText += "values('" + calle + "','" + numExterior + "','" + numInterior + "','" + colonia + "','" + localidad + "','" + referencia + "','" + municipio + "','" + estado + "','" + pais + "','" + cp + "'," + idDestinatario.ToString() + "," + CInt(Estados.Inicio).ToString() + ");"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByVal idDomicilio As Integer, ByVal calle As String, ByVal numExterior As String, ByVal numInterior As String, ByVal colonia As String, ByVal localidad As String, ByVal referencia As String, ByVal municipio As String, ByVal estado As String, pais As String, cp As String, idDestinatario As Integer) As Boolean
        comm.CommandText = "update tblcomercioexteriordomicilio set calle='" + calle + "', numexterior='" + numExterior + "', numinterior='" + numInterior + "', colonia='" + colonia + "', localidad='" + localidad + "', referencia='" + referencia + "', municipio='" + municipio + "', estado='" + estado + "', pais='" + pais + "', cp='" + cp + "', iddestinatario=" + idDestinatario.ToString()
        comm.CommandText += " where iddomicilio=" + idDomicilio.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal idDomicilio As Integer) As Boolean
        comm.CommandText = "delete from tblcomercioexteriordomicilio where iddomicilio=" + idDomicilio.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function buscar(ByVal idDomicilio As Integer) As Boolean
        comm.CommandText = "select iddomicilio from tblcomercioexteriordomicilio where iddomicilio=" + idDomicilio.ToString()
        Dim i As Integer = comm.ExecuteScalar()
        If i > 0 Then
            Me.idDomicilio = idDomicilio
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function buscaDestinatario(ByVal idDestinatario As Integer) As Boolean
        comm.CommandText = "select iddomicilio from tblcomercioexteriordomicilio where iddestinatario=" + idDestinatario.ToString()
        Dim i As Integer = comm.ExecuteScalar()
        If i > 0 Then
            Me.idDomicilio = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function
    Public Function guardarTodo(ByVal idDestinatario As Integer, ByVal idDomicilio As Integer) As Boolean
        comm.CommandText = "update tblcomercioexteriordomicilio set guardado=" + CInt(Estados.Guardada).ToString() + " where iddestinatario=" + idDestinatario.ToString() + " and iddomicilio=" + idDomicilio.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
