Imports MySql.Data.MySqlClient
Public Class dbClientesDescuentos
    Private comm As New MySqlCommand
    Public idDescuento As Integer
    Public idCliente As Integer
    Public idClasificacion1 As Integer
    Public idClasificacion2 As Integer
    Public idClasificacion3 As Integer
    Public descuento As Double
    Public modo As Integer
    Public idUsuarioAlta As Integer
    Public fechaAlta As String
    Public horaAlta As String
    Public idUsuarioCambio As Integer
    Public fechaCambio As String
    Public horaCambio As String

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        idDescuento = -1
        idCliente = -1
        idClasificacion1 = -1
        idClasificacion2 = -1
        idClasificacion3 = -1
        descuento = 0
        modo = 0
        idUsuarioAlta = -1
        fechaAlta = ""
        horaAlta = ""
        idUsuarioCambio = -1
        fechaCambio = ""
        horaCambio = ""
    End Sub

    Public Sub New(ByVal idDescuento As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.idDescuento = idDescuento
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblclientesdescuentos where iddescuento=" + idDescuento.ToString + ";"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        While dr.Read()
            idCliente = dr("idcliente")
            idClasificacion1 = dr("idclasificacion1")
            idClasificacion2 = dr("idclasificacion2")
            idClasificacion3 = dr("idclasificacion3")
            descuento = dr("descuento")
            modo = dr("modo")
            idUsuarioAlta = dr("idusuarioalta")
            fechaAlta = dr("fechaalta")
            horaAlta = dr("horaalta")
            idUsuarioCambio = dr("idusuariocambio")
            fechaCambio = dr("fechacambio")
            horaCambio = dr("horacambio")
        End While
        dr.Close()
    End Sub

    Public Function guardar(ByVal idCliente As Integer, ByVal idClasificacion1 As Integer, ByVal idClasificacion2 As Integer, ByVal idClasificacion3 As Integer, ByVal descuento As Double, ByVal modo As Integer) As Boolean
        comm.CommandText = "insert into tblclientesdescuentos(idcliente,idclasificacion1,idclasificacion2,idclasificacion3,descuento,modo,idusuarioalta,fechaalta,horaalta,idusuariocambio,fechacambio,horacambio)"
        comm.CommandText += " values(" + idCliente.ToString + "," + idClasificacion1.ToString + "," + idClasificacion2.ToString + "," + idClasificacion3.ToString + "," + descuento.ToString + "," + modo.ToString + "," + GlobalIdUsuario.ToString + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByVal idDescuento As Integer, ByVal idCliente As Integer, ByVal idClasificacion1 As Integer, ByVal idClasificacion2 As Integer, ByVal idClasificacion3 As Integer, ByVal descuento As Double, ByVal modo As Integer) As Boolean
        comm.CommandText = "update tblclientesdescuentos set idcliente=" + idCliente.ToString + ", idclasificacion1=" + idClasificacion1.ToString + ", idclasificacion2=" + idClasificacion2.ToString + ", idclasificacion3=" + idClasificacion3.ToString + ", descuento=" + descuento.ToString + ", modo=" + modo.ToString + ", idusuariocambio=" + GlobalIdUsuario.ToString + ", fechacambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horacambio='" + TimeOfDay.ToString("HH:mm:ss") + "' "
        comm.CommandText += "where iddescuento=" + idDescuento.ToString + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal idDescuento As Integer) As Boolean
        comm.CommandText = "delete from tblclientesdescuentos where iddescuento=" + idDescuento.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function buscaFiltrado(ByVal idCliente As Integer) As DataView
        Dim ds As New DataSet
        comm.CommandText = "select d.iddescuento,ifnull((select nombre from tblinventarioclasificaciones where idclasificacion=d.idclasificacion1),'') Nivel1,ifnull((select nombre from tblinventarioclasificaciones2 where idclasificacion=d.idclasificacion2),'') Nivel2,ifnull((select nombre from tblinventarioclasificaciones3 where idclasificacion=d.idclasificacion3),'') Nivel3,d.descuento from tblclientesdescuentos d where d.idcliente=" + idCliente.ToString
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "descuentos")
        Return ds.Tables("descuentos").DefaultView
    End Function

    Public Function buscar(ByVal idDescuento As Integer) As Boolean
        comm.CommandText = "select iddescuento from tblclientesdescuentos where iddescuento=" + idDescuento.ToString + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idDescuento = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function buscaDescuento(ByVal idCliente As Integer, ByVal idClasificacion1 As Integer, ByVal idClasificacion2 As Integer, ByVal idClasificacion3 As Integer) As Double
        comm.CommandText = "select ifnull((select descuento from tblclientesdescuentos where"
            comm.CommandText += " idcliente=" + idCliente.ToString()
            'If idClasificacion1 > 0 Then
            comm.CommandText += " and idclasificacion1=" + idClasificacion1.ToString()
            'End If
            'If idClasificacion2 > 0 Then
            comm.CommandText += " and idclasificacion2=" + idClasificacion2.ToString()
            'End If
            'If idClasificacion3 > 0 Then
            comm.CommandText += " and idclasificacion3=" + idClasificacion3.ToString()
            ' End If
            comm.CommandText += "),-1000)"
            Dim res As Double = comm.ExecuteScalar
            Return res
    End Function
End Class

