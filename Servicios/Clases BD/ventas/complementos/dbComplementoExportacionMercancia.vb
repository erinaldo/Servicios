Imports MySql.Data.MySqlClient
Public Class dbComplementoExportacionMercancia

    Private comm As New MySqlCommand
    Public idMercancia As Integer
    Public noIdentificacion As String
    Public fraccionArancelaria As String
    Public cantidadAduana As Double
    Public unidadAduana As String
    Public valorUnitarioAduana As Double
    Public valorDolares As Double
    Public idComplemento As Integer
    Public guardado As Integer
    Public nombre As String

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal idMercancia As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.idMercancia = idMercancia
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblcomercioexteriormercancia where idMercancia=" + idMercancia.ToString
        Dim dr As MySqlDataReader = comm.ExecuteReader()
        While dr.Read()
            noIdentificacion = dr("noidentificacion")
            fraccionArancelaria = dr("fraccionarancelaria")
            cantidadAduana = dr("cantidadaduana")
            unidadAduana = dr("unidadaduana")
            valorUnitarioAduana = dr("valorunitarioaduana")
            valorDolares = dr("valordolares")
            idComplemento = dr("idcomplemento")
            guardado = dr("guardado")
            nombre = dr("nombre")
        End While
        dr.Close()
    End Sub

    Public Function guardar(ByVal noIdentificacion As String, ByVal fraccionArancelaria As String, ByVal cantidadAduana As Double, ByVal unidadAduana As String, ByVal valorUnitarioAduana As Double, ByVal valorDolares As Double, ByVal idComplemento As Integer, ByVal nombre As String) As Boolean
        comm.CommandText = "insert into tblcomercioexteriormercancia(noidentificacion,fraccionarancelaria,cantidadaduana,unidadaduana,valorunitarioaduana,valordolares,idcomplemento,guardado,nombre)"
        comm.CommandText += "values('" + noIdentificacion + "','" + fraccionArancelaria + "'," + cantidadAduana.ToString + ",'" + unidadAduana + "'," + valorUnitarioAduana.ToString + "," + valorDolares.ToString + "," + idComplemento.ToString + "," + CInt(Estados.Inicio).ToString() + ",'" + nombre + "');"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByVal idMercancia As Integer, ByVal noIdentificacion As String, ByVal fraccionArancelaria As String, ByVal cantidadAduana As Double, ByVal unidadAduana As String, ByVal valorUnitarioAduana As Double, ByVal valorDolares As Double, ByVal idComplemento As Integer, ByVal nombre As String) As Boolean
        comm.CommandText = "update tblcomercioexteriormercancia set noidentificacion='" + noIdentificacion + "', fraccionarancelaria='" + fraccionArancelaria + "', cantidadAduana=" + cantidadAduana.ToString + ", unidadaduana='" + unidadAduana + "', valorunitarioaduana=" + valorUnitarioAduana.ToString + ", valordolares=" + valorDolares.ToString + ", idcomplemento=" + idComplemento.ToString
        comm.CommandText += ", nombre='" + nombre + "' where idMercancia=" + idMercancia.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal idMercancia As Integer) As Boolean
        comm.CommandText = "delete from tblcomercioexteriormercancia where idmercancia=" + idMercancia.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function lista(ByVal idComplemento) As DataView
        comm.CommandText = "select idmercancia,nombre from tblcomercioexteriormercancia where idComplemento=" + idComplemento.ToString
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "mercancia")
        Return ds.Tables("mercancia").DefaultView
    End Function

    Public Function consultaReader(ByVal idComplemento As Integer) As MySqlDataReader
        comm.CommandText = "select * from tblcomercioexteriormercancia where idComplemento=" + idComplemento.ToString
        Return comm.ExecuteReader()
    End Function

    Public Function buscar(ByVal idMercancia As Integer) As Boolean
        comm.CommandText = "select idMercancia from tblcomercioexteriormercancia where idmercancia=" + idMercancia.ToString
        Dim i As Integer = comm.ExecuteScalar()
        If i > 0 Then
            Me.idMercancia = idMercancia
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function buscaComplemento(ByVal idComplemento As Integer) As Integer()
        Dim Ids As New List(Of Integer)
        comm.CommandText = "select idMercancia from tblcomercioexteriormercancia where idcomplemento=" + idComplemento.ToString
        Dim dr As MySqlDataReader = comm.ExecuteReader()
        While dr.Read()
            Ids.Add(dr("idMercancia"))
        End While
        dr.Close()
        Return Ids.ToArray()
    End Function
    Public Function guardarTodo(ByVal idComplemento As Integer, ByVal idMercancia As Integer) As Boolean
        comm.CommandText = "update tblcomercioexteriormercancia set guardado=" + CInt(Estados.Guardada).ToString() + " and idcomplemento=" + idComplemento.ToString() + " and idmercancia=" + idMercancia.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
