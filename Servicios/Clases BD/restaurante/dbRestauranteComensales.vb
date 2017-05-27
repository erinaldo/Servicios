Imports MySql.Data.MySqlClient
Public Class dbRestauranteComensales
    Public id As Integer
    Public numero As Integer
    Public mesa As Integer
    Public comm As New MySqlCommand
    Public Sub New(ByVal conexion As MySqlConnection)
        id = -1
        numero = -1
        mesa = -1
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal id As Integer, ByVal conexion As MySqlConnection)
        Me.id = id
        comm.Connection = conexion
        llenaDatos()
    End Sub

    Public Function buscar(ByVal idComensal As Integer) As Boolean
        comm.CommandText = "select idcomensal from tblrestaurantecomensales where idcomensal=" + idComensal.ToString()
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.id = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Private Sub llenaDatos()
        Dim dr As MySqlDataReader
        comm.CommandText = "select * from tblrestaurantecomensales where idcomensal=" + id.ToString() + ";"
        dr = comm.ExecuteReader
        While dr.Read
            id = dr("idcomensal")
            numero = dr("numero")
            mesa = dr("mesa")
        End While
        dr.Close()
    End Sub

    Public Function guardar(ByVal numero As Integer, ByVal mesa As Integer) As Integer
        comm.CommandText = "insert into tblrestaurantecomensales(numero,mesa) values(" + numero.ToString() + "," + mesa.ToString() + ");"
        comm.CommandText += "select ifnull(last_insert_id(),0);"
        Return comm.ExecuteScalar
    End Function

    Public Sub modificar(ByVal id As Integer, ByVal numero As Integer, mesa As Integer)
        comm.CommandText = "update tblrestaurantecomensales set numero=" + numero.ToString() + ", mesa=" + mesa.ToString() + " where idcomensal=" + id.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub eliminar(ByVal id As Integer)
        comm.CommandText = "delete from tblrestaurantecomensales where idcomensal=" + id.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Function vistaComensales(ByVal idMesa As Integer) As DataView
        comm.CommandText = "select c.idcomensal,c.numero as Comensal from tblrestaurantecomensales as c where c.mesa=" + idMesa.ToString() + ";"
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "comensales")
        Return ds.Tables("comensales").DefaultView
    End Function

    Public Sub eliminarPorMesa(ByVal idMesa As Integer)
        comm.CommandText = "delete from tblrestaurantecomensales where mesa=" + idMesa.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Function listaComensales(ByVal idMesa As Integer) As List(Of Integer)
        comm.CommandText = "select idcomensal from tblrestaurantecomensales where mesa=" + idMesa.ToString()
        Dim lista As New List(Of Integer)
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            lista.Add(dr("idcomensal"))
        End While
        dr.Close()
        Return lista
    End Function
End Class
