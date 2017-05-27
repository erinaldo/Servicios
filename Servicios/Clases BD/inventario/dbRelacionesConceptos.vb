Imports MySql.Data.MySqlClient
Public Class dbRelacionesConceptos
    Private comm As New MySqlCommand

    Public idRelacion As Integer
    Public tipo As Integer
    Public idConcepto As Integer
    Public idSucursal As Integer

    Public Enum tipoConcepto
        mermas = 0
        obsequios = 1
        devPlanta = 2
        carga = 3
        dvCliente = 4
        buenas = 5
        mermas2 = 6
        obsequios2 = 7
        enviosPlanta = 8
    End Enum

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        idRelacion = -1
        tipo = -1
        idConcepto = -1
        idSucursal = -1
    End Sub

    Public Sub New(ByVal idRelacion As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.idRelacion = idRelacion
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblinventariorelacionesconceptos where idrelacion=" + idRelacion.ToString()
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            tipo = dr("tipo")
            idConcepto = dr("idconcepto")
            idSucursal = dr("idsucursal")
        End While
        dr.Close()
    End Sub

    Public Function agregar(ByVal idconcepto As Integer, ByVal tipo As Integer, ByVal idSucursal As Integer) As Boolean
        comm.CommandText = "insert into tblinventariorelacionesconceptos(idconcepto,tipo,idsucursal) values(" + idconcepto.ToString() + "," + tipo.ToString() + "," + idSucursal.ToString() + ");"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByVal idRelacion As Integer, ByVal idconcepto As Integer, ByVal tipo As Integer, ByVal idSucursal As Integer) As Boolean
        comm.CommandText = "update tblinventariorelacionesconceptos set idconcepto=" + idconcepto.ToString() + ", tipo=" + tipo.ToString() + ", idSucursal=" + idSucursal.ToString() + " where idrelacion=" + idRelacion.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal idrelacion As Integer) As Boolean
        comm.CommandText = "delete from tblinventariorelacionesconceptos where idrelacion=" + idrelacion.ToString()
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function buscar(ByVal idrelacion As Integer) As Boolean
        comm.CommandText = "select idrelacion from tblinventariorelacionesconceptos where idrelacion=" + idrelacion.ToString()
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idRelacion = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function buscarTipo(ByVal idSucursal As Integer, ByVal tipo As Integer) As Boolean
        comm.CommandText = "select idrelacion from tblinventariorelacionesconceptos where idsucursal=" + idSucursal.ToString() + " and tipo=" + tipo.ToString()
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idRelacion = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function
End Class
