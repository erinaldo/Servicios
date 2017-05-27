Imports MySql.Data.MySqlClient
Public Class dbRestauranteVentasMesas
    Public Property id As Integer
    Public Property idMesa As Integer
    Public Property idVenta As Integer
    Public Property idVendedor As Integer
    Private comm As New MySqlCommand

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub agregar(ByVal idMesa As Integer, ByVal idVenta As Integer, ByVal idVendedor As Integer)
        comm.CommandText = "insert into tblrestaurantemesaventa(idMesa,idVenta,idvendedor) values(" + idMesa.ToString() + "," + idVenta.ToString() + "," + idVendedor.ToString() + ");"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub eliminar(ByVal idMesa As Integer)
        comm.CommandText = "delete from tblrestaurantemesaventa where idMesa=" + idMesa.ToString()
        comm.ExecuteNonQuery()
    End Sub

    

    Public Sub llenaDatos()
        comm.CommandText = "select * from tblrestaurantemesaventa where idMesa=" + idMesa.ToString() + ";"
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        While dr.Read()
            id = dr("id")
            idMesa = dr("idMesa")
            idVenta = dr("idVenta")
            idVendedor = dr("idvendedor")
        End While
        dr.Close()
    End Sub

    Public Function cambiarMesa(ByVal mesaActual As Integer, ByVal mesaNueva As Integer) As Boolean
        comm.CommandText = "update tblrestaurantemesaventa set idMesa=" + mesaNueva.ToString() + " where idMesa=" + mesaActual.ToString() + ";" + vbCrLf
        comm.CommandText += "update tblrestaurantecomensales set mesa=" + mesaNueva.ToString() + " where mesa=" + mesaActual.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
