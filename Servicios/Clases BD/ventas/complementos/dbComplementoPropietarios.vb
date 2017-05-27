Imports MySql.Data.MySqlClient
Public Class dbComplementoPropietarios
    Private comm As New MySqlCommand
    Public ID As Integer
    Public numRegIdTrib As String
    Public clavePais As String
    Public idComplemento As String

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal ID As Integer, ByVal conexion As MySqlConnection)
        Me.ID = ID
        comm.Connection = conexion
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblcomercioexteriorpropietario where ID=" + ID.ToString()
        Dim dr As MySqlDataReader = comm.ExecuteReader()
        While dr.Read()
            ID = dr("ID")
            numRegIdTrib = dr("numRegIdTrib")
            clavePais = dr("clavePais")
            idComplemento = dr("idComplemento")
        End While
        dr.Close()
    End Sub

    Public Function guardar(ByVal numRegIdTrib As String, ByVal clavePais As String, ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "insert into tblcomercioexteriorpropietario(numRegIdTrib,clavePais,idComplemento)"
        comm.CommandText += "values('" + numRegIdTrib + "','" + clavePais + "','" + idComplemento.ToString() + "');"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MsgBox("Guardar propietario: " + ex.Message)
            Return False
        End Try
    End Function

    Public Function modificar(ByVal idComplemento As Integer, ByVal numRegIdTrib As String, ByVal clavePais As String) As Boolean
        comm.CommandText = "update tblcomercioexteriorpropietario set numRegIdTrib='" + numRegIdTrib + "', clavePais='" + clavePais + "where idComplemento=" + idComplemento.ToString
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MsgBox("Modificar propietario: " + ex.Message)
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "delete from tblcomercioexteriorpropietario where idcomplemento=" + idComplemento.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MsgBox("Guardar propietario: " + ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
    End Function

    Public Function lista(ByVal idComplemento) As DataView
        comm.CommandText = "select numRegIdTrib as 'Numero de Identificacion',clavePais as 'Clave del Pais' from tblcomercioexteriorpropietario where idComplemento=" + idComplemento.ToString
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "propietarios")
        Return ds.Tables("propietarios").DefaultView
    End Function

    Public Function buscaComplemento(ByVal idComplemento As Integer) As Integer()
        Dim Ids As New List(Of Integer)
        comm.CommandText = "select ID from tblcomercioexteriorpropietario where idComplemento=" + idComplemento.ToString
        Dim dr As MySqlDataReader = comm.ExecuteReader()
        While dr.Read()
            Ids.Add(dr("ID"))
        End While
        dr.Close()
        Return Ids.ToArray()
    End Function

    Public Function buscar(ByVal ID As Integer) As Boolean
        comm.CommandText = "select ID from tblcomercioexteriorpropietario where ID=" + ID.ToString
        Dim i As Integer = comm.ExecuteScalar()
        If i > 0 Then
            Me.ID = ID
            llenaDatos()
            Return True
        End If
        Return False
    End Function
End Class
