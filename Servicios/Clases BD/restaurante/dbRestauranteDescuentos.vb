Imports MySql.Data.MySqlClient
Public Class dbRestauranteDescuentos
    Private comm As New MySqlCommand
    Public idDescuento As Integer
    Public idCliente As Integer
    Public idUsuario As Integer
    Public descuento As Double

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal idDescuento As Integer, ByVal conexion As MySqlConnection)
        Me.idDescuento = idDescuento
        comm.Connection = conexion
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblrestaurantedescuentos where iddescuento=" + idDescuento.ToString + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            idCliente = dr("idcliente")
            idUsuario = dr("idusuario")
            descuento = dr("descuento")
        End While
        dr.Close()
    End Sub

    Public Sub agregar(ByVal idCliente As Integer, ByVal idUsuario As Integer, ByVal descuento As Double)
        comm.CommandText = "insert into tblrestaurantedescuentos(idcliente,idusuario,descuento)values(" + idCliente.ToString + "," + idUsuario.ToString + "," + descuento.ToString + ");"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub modificar(ByVal idDescuento As Integer, ByVal idCliente As Integer, ByVal idUsuario As Integer, ByVal descuento As Double)
        comm.CommandText = "update tblrestaurantedescuentos set idCliente=" + idCliente.ToString + ", idUsuario=" + idUsuario.ToString + ", descuento=" + descuento.ToString + " where idDescuento=" + idDescuento.ToString + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub eliminar(ByVal idDescuento As Integer)
        comm.CommandText = "delete from tblrestaurantedescuentos where idDescuento=" + idDescuento.ToString + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Function buscar(ByVal idDescuento As Integer) As Boolean
        comm.CommandText = "select iddescuento from tblrestuarantedescuentos where iddescuento=" + idDescuento.ToString + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idDescuento = idDescuento
            llenaDatos()
            Return True
        End If
        Return False
    End Function
    Public Function buscarCliente(ByVal idCliente As Integer) As Boolean
        comm.CommandText = "select iddescuento from tblrestuarantedescuentos where idCliente=" + idCliente.ToString + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idDescuento = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function
    Public Function buscarUsuario(ByVal idUsuario As Integer) As Boolean
        comm.CommandText = "select iddescuento from tblrestuarantedescuentos where idUsuario=" + idUsuario.ToString + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idDescuento = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function
End Class
