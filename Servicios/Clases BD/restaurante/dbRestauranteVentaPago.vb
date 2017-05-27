Imports MySql.Data.MySqlClient
Public Class dbRestauranteVentaPago
    Public Property idForma As Integer
    Public Property idVenta As Integer
    Public Property idFormaPago As Integer
    Public Property total As Double

    Private comm As New MySqlCommand

    Public Sub New()

    End Sub

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        idForma = -1
        idVenta = -1
        idFormaPago = -1
        total = 0
    End Sub

    Public Sub New(ByVal idForma As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.idForma = idForma
        llenaDatos()
    End Sub

    Public Sub llenaDatos()
        comm.CommandText = "select * from tblrestauranteventaspagos where id=" + idForma.ToString() + ";"
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        While dr.Read()
            idVenta = dr("idventa")
            idFormaPago = dr("idmedioPago")
            total = dr("total")
        End While
        dr.Close()
    End Sub

    Public Sub agregar(ByVal idventa As Integer, ByVal idFormaPago As Integer, ByVal total As Double)
        comm.CommandText = "insert into tblrestauranteventaspagos(idventa,idmedioPago,total) values(" + idventa.ToString() + "," + idFormaPago.ToString() + "," + total.ToString() + ");"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub modificar(ByVal idForma As Integer, ByVal idVenta As Integer, ByVal idFormaPago As Integer, ByVal total As Double)
        comm.CommandText = "update tblrestauranteventaspagos set idVenta=" + idVenta.ToString() + ", idmedioPago=" + idFormaPago.ToString() + ", total=" + total.ToString() + " where id=" + idForma.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub eliminar(ByVal idForma As Integer)
        comm.CommandText = "delete from tblrestauranteventaspagos where id=" + idForma.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Function buscarPorVenta(ByVal idVenta As Integer) As DataView
        comm.CommandText = "select fp.nombre, vp.total from tblrestauranteventaspagos as vp inner join tblformasdepago as fp on vp.idmedioPago=fp.idforma where vp.idventa=" + idVenta.ToString() + ";"
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "pagos")
        Return ds.Tables("pagos").DefaultView()
    End Function

    Public Function sumaPagos(ByVal idVenta As Integer) As Double
        comm.CommandText = "select sum(total) from tblrestauranteventaspagos where idventa=" + idVenta.ToString() + ";"
        Dim suma As Double = comm.ExecuteScalar
        Return suma
    End Function
End Class
