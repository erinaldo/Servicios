Imports MySql.Data.MySqlClient
Public Class dbRestauranteSecciones
    Private comm As MySqlCommand
    Public Property id As Integer
    Public Property sucursal As Integer
    Public Property numero As Integer
    Public Property nombre As String
    Public Property rutaMapa As String
    Public Seleccionado As Boolean
    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer, ByVal conexion As MySqlConnection)
        Me.id = id
        comm = New MySqlCommand
        comm.Connection = conexion
        llenaDatos()
    End Sub

    Public Sub New(ByVal conexion As MySqlConnection)
        comm = New MySqlCommand
        comm.Connection = conexion
        id = -1
        sucursal = -1
        numero = -1
        nombre = ""
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblrestaurantesecciones where idSeccion=" + id.ToString()
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader()
        While dr.Read()
            id = dr("idSeccion")
            sucursal = dr("idSucursal")
            numero = dr("numSeccion")
            nombre = dr("nombre")
            rutaMapa = dr("rutaMapa")
        End While
        dr.Close()
    End Sub

    Public Function buscar(ByVal id As Integer) As Boolean
        comm.CommandText = "select idSeccion as idSeccion from tblrestaurantesecciones where idseccion=" + id.ToString()
        Dim res As Integer = comm.ExecuteScalar
        If res > 0 Then
            Me.id = res
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function guardar(ByVal sucursal As Integer, ByVal numero As Integer, ByVal nombre As String, ByVal rutaMapa As String) As Boolean
        Me.sucursal = sucursal
        Me.numero = numero
        Me.nombre = nombre
        comm.CommandText = "insert into tblrestaurantesecciones(idsucursal,numSeccion,nombre,rutamapa) values(" + Me.sucursal.ToString() + "," + Me.numero.ToString() + ",'" + Me.nombre + "','" + rutaMapa + "');"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByVal id As Integer, ByVal sucursal As Integer, ByVal numero As Integer, ByVal nombre As String, ByVal rutaMapa As String) As Boolean
        Me.id = id
        Me.sucursal = sucursal
        Me.numero = numero
        Me.nombre = nombre
        comm.CommandText = "update tblrestaurantesecciones set idsucursal=" + Me.sucursal.ToString() + ", numSeccion=" + Me.numero.ToString() + ", nombre='" + Me.nombre + "', rutaMapa='" + rutaMapa + "' where idseccion=" + Me.id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal id As Integer) As Boolean
        comm.CommandText = "delete from tblrestaurantesecciones where idseccion=" + id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function vistaSecciones(pIdSucursal As Integer) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select * from tblrestaurantesecciones where idsucursal=" + pIdSucursal.ToString
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "secciones")
        Return ds.Tables("secciones").DefaultView
    End Function

    Public Function seccionesSucursal(ByVal idSucursal As Integer) As List(Of dbRestauranteSecciones)
        Dim s As dbRestauranteSecciones
        Dim lista As New List(Of dbRestauranteSecciones)
        comm.CommandText = "select * from tblrestaurantesecciones where idsucursal=" + idSucursal.ToString() + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            s = New dbRestauranteSecciones(dr("idseccion"), MySqlcon)
            lista.Add(s)
        End While
        Return lista
    End Function
End Class
