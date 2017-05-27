Imports MySql.Data.MySqlClient
Public Class dbComplementoExportacionDescripcion
    Private comm As New MySqlCommand
    Public idDescripcion As Integer
    Public marca As String
    Public modelo As String
    Public submodelo As String
    Public numeroSerie As String
    Public idMercancia As Integer
    Public guardado As Integer
    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal idDescripcion As Integer, ByVal conexion As MySqlConnection)
        Me.idDescripcion = idDescripcion
        comm.Connection = conexion
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblcomercioexteriordescripciones where iddescripcion=" + idDescripcion.ToString()
        Dim dr As MySqlDataReader = comm.ExecuteReader()
        While dr.Read()
            idDescripcion = dr("iddescripcion")
            marca = dr("marca")
            modelo = dr("modelo")
            submodelo = dr("submodelo")
            numeroSerie = dr("numeroserie")
            idMercancia = dr("idmercancia")
            guardado = dr("guardado")
        End While
        dr.Close()
    End Sub

    Public Function guardar(ByVal marca As String, ByVal modelo As String, ByVal subModelo As String, ByVal numeroSerie As String, ByVal idMercancia As Integer) As Boolean
        comm.CommandText = "insert into tblcomercioexteriordescripciones(marca,modelo,submodelo,numeroserie,idmercancia,guardado) values('" + marca + "','" + modelo + "','" + subModelo + "','" + numeroSerie + "'," + idMercancia.ToString() + "," + CInt(Estados.Inicio).ToString() + ");"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByVal idDescripcion As Integer, ByVal marca As String, ByVal modelo As String, ByVal subModelo As String, ByVal numeroSerie As String, ByVal idMercancia As Integer) As Boolean
        comm.CommandText = "update tblcomercioexteriordescripciones set marca='" + marca + "', modelo='" + modelo + "', submodelo='" + subModelo + "', numeroserie='" + numeroSerie + "', idmercancia=" + idMercancia.ToString() + " where iddescripcion=" + idDescripcion.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal idDescripcion As Integer) As Boolean
        comm.CommandText = "delete from tblcomercioexteriordescripciones where iddescripcion=" + idDescripcion.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function buscar(ByVal idDescripcion As Integer) As Boolean
        comm.CommandText = "Select iddescripcion from tblcomercioexteriordescripciones where iddescripcion=" + idDescripcion.ToString
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idDescripcion = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function lista(ByVal idMercancia As Integer) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select * from tblcomercioexteriordescripciones where idmercancia=" + idMercancia.ToString
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "descripciones")
        Return ds.Tables("descripciones").DefaultView
    End Function

    Public Function consultaReader(ByVal idMercancia As Integer) As MySqlDataReader
        comm.CommandText = "select * from tblcomercioexteriordescripciones where idmercancia=" + idMercancia.ToString
        Return comm.ExecuteReader()
    End Function

    Public Function buscaMercancia(ByVal idMercancia As Integer) As Integer()
        Dim Ids As New List(Of Integer)
        comm.CommandText = "select idDescripcion from tblcomercioexteriordescripciones where idmercancia=" + idMercancia.ToString()
        Dim dr As MySqlDataReader = comm.ExecuteReader()
        While dr.Read()
            Ids.Add(dr("idDescripcion"))
        End While
        dr.Close()
        Return Ids.ToArray()
    End Function

    Public Function guardarTodo(ByVal idMercancia As Integer, idDescripcion As Integer) As Boolean
        comm.CommandText = "update tblcomercioexteriordescripciones set guardado=" + CInt(Estados.Guardada).ToString() + " where idMercancia=" + idMercancia.ToString() + " and idDescripcion=" + idDescripcion.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
