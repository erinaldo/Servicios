Imports MySql.Data.MySqlClient
Public Class dbRestauranteReservacion
    Private comm As New MySqlCommand
    Public Property idReservacion As Integer
    Public Property fecha As String
    Public Property horaInicio As String
    Public Property idMesa As Integer
    Public Property idSeccion As Integer
    Public Property idCliente As Integer
    Public Property horaFin

    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        idReservacion = -1
        fecha = ""
        horaInicio = ""
        idMesa = -1
        idSeccion = -1
        idCliente = -1
    End Sub

    Public Sub New()
        idReservacion = -1
        fecha = ""
        horaInicio = ""
        idMesa = -1
        idSeccion = -1
        idCliente = -1
    End Sub

    Public Sub New(ByVal conexion As MySqlConnection, ByVal idReservacion As Integer)
        comm.Connection = conexion
        Me.idReservacion = idReservacion
        llenaDatos()
    End Sub

    Public Sub agregar(ByVal idMesa As Integer, fecha As String, hora As String, idSeccion As Integer, idCliente As Integer, horaFin As String)
        comm.CommandText = "insert into tblrestaurantereservaciones(idMesa,fecha,horaInicio,idSeccion,idCliente,horaFin) values("
        comm.CommandText += idMesa.ToString + ",'" + fecha + "','" + hora + "'," + idSeccion.ToString() + "," + idCliente.ToString() + ");"
        Try
            comm.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub modificar()

    End Sub

    Public Sub eliminar()

    End Sub

    Public Function buscar(ByVal idReservacion As Integer) As Boolean
        comm.CommandText = "select idReservacion from tblrestaurantereservaciones where idReservacion=" + idReservacion.ToString() + ";"
        Dim res As Integer = comm.ExecuteScalar
        If res > 0 Then
            Me.idReservacion = res
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Private Sub llenaDatos()
        Dim dr As MySqlDataReader
        comm.CommandText = "select * from tblrestaurantereservaciones where idreseravacion=" + idReservacion.ToString()
        dr = comm.ExecuteReader
        While dr.Read()
            Me.idReservacion = dr("idReservacion")
            Me.idMesa = dr("idMesa")
            Me.fecha = dr("fecha")
            Me.horaInicio = dr("horaInicio")
            Me.idSeccion = dr("idSeccion")
            Me.idCliente = dr("idCliente")
            Me.horaFin = dr("horaFin")
        End While
        dr.Close()
    End Sub

    Public Function vistaReservaciones(ByVal idSucursal As Integer, ByVal idMesa As Integer, ByVal idSeccion As Integer, ByVal desde As String, ByVal hasta As String, ByVal idCliente As Integer) As DataView
        comm.CommandText = "select r.fecha,c.nombre,m.numero,s.nombre, from tblrestaurantereservaciones as r inner join tblrestaurantemesas as m on r.idmesa=mesa.idmesa inner join tblrestaurantesecciones as s "
        comm.CommandText += "on m.idseccion=s.idseccion inner jon tblclientes as c on r.idcliente=c.idcliente where r.fecha>='" + desde + "' and r.fecha<='" + hasta + "' "
        comm.CommandText += "and r.idsucursal=" + idSucursal.ToString()
        If idMesa > 0 Then
            comm.CommandText += " r.idmesa=" + idMesa.ToString()
        End If
        If idSeccion > 0 Then
            comm.CommandText += " r.idSeccion=" + idSeccion.ToString()
        End If
        If idCliente > 0 Then
            comm.CommandText += " r.idcliente=" + idCliente.ToString()
        End If
        Dim da As New MySqlDataAdapter(comm)
        Dim ds As New DataSet
        da.Fill(ds, "reservaciones")
        Return ds.Tables("reservaciones").DefaultView

    End Function

    Public Function mesasReservadas(ByVal idSeccion As Integer, ByVal fecha As String, ByVal hora As String) As List(Of Integer)
        Dim lista As New List(Of Integer)
        Dim dr As MySqlDataReader
        comm.CommandText = "select idmesa from tblrestaurantereservaciones where idseccion=" + idSeccion.ToString() + " and fecha='" + fecha + "' "
        If hora <> "" Then
            comm.CommandText += "and hora='" + hora + ""
        End If
        dr = comm.ExecuteReader
        While dr.Read()
            Dim i As Integer = dr("idmesa")
            lista.Add(i)
        End While
        dr.Close()
        Return lista
    End Function

End Class
