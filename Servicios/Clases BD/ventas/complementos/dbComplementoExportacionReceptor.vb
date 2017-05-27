Imports MySql.Data.MySqlClient
Public Class dbComplementoExportacionReceptor
    Private comm As New MySqlCommand
    Public idReceptor As Integer
    Public numRegIdTrib As String
    Public idComplemento As Integer
    Public curp As String
    Public guardado As Integer
    Public clave_Pais


    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal idReceptor As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.idReceptor = idReceptor
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblcomercioexteriorreceptor where idReceptor=" + idReceptor.ToString
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            numRegIdTrib = dr("numRegIdTrib")
            idComplemento = dr("idComplemento")
            curp = dr("curp")
            guardado = dr("guardado")
            clave_Pais = dr("clavePais")
        End While
        dr.Close()
    End Sub

    Public Function guardar(ByVal numRegIdTrib As String, ByVal idComplemento As Integer, ByVal curp As String, ByVal clavePais As String) As Boolean
        comm.CommandText = "insert into tblcomercioexteriorreceptor(numRegIdTrib,idComplemento,curp,guardado,clavePais) values('" + numRegIdTrib + "'," + idComplemento.ToString + ",'" + curp + "'," + CInt(Estados.Inicio).ToString() + ",'" + clavePais + "');"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MsgBox("Guardar receptor: " + ex.Message)
            Return False
        End Try
    End Function

    Public Function modificar(ByVal idReceptor As Integer, ByVal numRefIdTrib As String, ByVal idComplemento As Integer, ByVal curp As String, ByVal clavePais As String) As Boolean
        comm.CommandText = "update tblcomercioexteriorreceptor set numregidtrib='" + numRefIdTrib + "', idComplemento=" + idComplemento.ToString + ", curp='" + curp + "', clavePais='" + clavePais + "' where idReceptor=" + idReceptor.ToString
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MsgBox("Guardar receptor: " + ex.Message)
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal idReceptor As Integer) As Boolean
        comm.CommandText = "delete from tblcomercioexteriorreceptor where idreceptor=" + idReceptor.ToString
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function buscar(ByVal idReceptor As Integer) As Boolean
        comm.CommandText = "select idReceptor from tblcomercioexteriorreceptor where idreceptor=" + idReceptor.ToString
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idReceptor = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function buscaComplemento(ByVal idComplemento As Integer) As Boolean
        comm.CommandText = "select idReceptor from tblcomercioexteriorreceptor where idcomplemento=" + idComplemento.ToString
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idReceptor = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function guardarTodo(ByVal idComplemento As Integer, ByVal idReceptor As Integer) As Boolean
        comm.CommandText = "update tblcomercioexteriorreceptor set guardado=" + CInt(Estados.Guardada).ToString() + " where idComplemento=" + idComplemento.ToString() + " and idreceptor=" + idReceptor.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
