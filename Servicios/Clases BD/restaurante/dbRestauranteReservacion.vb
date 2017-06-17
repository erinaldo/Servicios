Imports MySql.Data.MySqlClient
Public Class dbRestauranteReservacion
    Private comm As New MySqlCommand
    
    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub Agregar(ByVal idMesa As Integer, fecha As DateTime, nombre As String)
        comm.CommandText = "insert into tblrestaurantereservaciones(idMesa,fecha,nombre) values (@idmesa,@fecha,@nombre);update tblrestaurantemesas set estado=" + CInt(EstadosMesas.Reservada).ToString() + " where idmesa=@idmesa and estado=" + CInt(EstadosMesas.Libre).ToString() + ";"
        comm.Parameters.Add(New MySqlParameter("@idmesa", idMesa))
        comm.Parameters.Add(New MySqlParameter("@fecha", fecha))
        comm.Parameters.Add(New MySqlParameter("@nombre", nombre))
        comm.ExecuteNonQuery()

        comm.CommandText = "select estado from tblrestaurantemesas where idmesa=@idmesa;"
        If comm.ExecuteScalar = EstadosMesas.Libre Then
            comm.CommandText = "update tblrestaurantemesas set estado=" + CInt(EstadosMesas.Reservada).ToString() + " where idmesa=@idmesa;"
            comm.ExecuteNonQuery()
        End If
        comm.Parameters.Clear()
    End Sub

    Public Sub Modificar()

    End Sub

    Public Sub Eliminar()

    End Sub

    'Public Function Buscar(ByVal idReservacion As Integer) As Boolean
    '    comm.CommandText = "select idReservacion from tblrestaurantereservaciones where idReservacion=" + idReservacion.ToString() + ";"
    '    Dim res As Integer = comm.ExecuteScalar
    '    If res > 0 Then
    '        Me.idReservacion = res
    '        llenaDatos()
    '        Return True
    '    End If
    '    Return False
    'End Function

    'Private Sub llenaDatos()
    '    Dim dr As MySqlDataReader
    '    comm.CommandText = "select * from tblrestaurantereservaciones where idreseravacion=" + idReservacion.ToString()
    '    dr = comm.ExecuteReader
    '    While dr.Read()
    '        Me.idReservacion = dr("idReservacion")
    '        Me.idMesa = dr("idMesa")
    '        Me.fecha = dr("fecha")
    '        Me.horaInicio = dr("horaInicio")
    '        Me.idSeccion = dr("idSeccion")
    '        Me.idCliente = dr("idCliente")
    '        Me.horaFin = dr("horaFin")
    '    End While
    '    dr.Close()
    'End Sub

    'Public Function vistaReservaciones(ByVal idSucursal As Integer, ByVal idMesa As Integer, ByVal idSeccion As Integer, ByVal desde As String, ByVal hasta As String, ByVal idCliente As Integer) As DataView
    '    comm.CommandText = "select r.fecha,c.nombre,m.numero,s.nombre, from tblrestaurantereservaciones as r inner join tblrestaurantemesas as m on r.idmesa=mesa.idmesa inner join tblrestaurantesecciones as s "
    '    comm.CommandText += "on m.idseccion=s.idseccion inner jon tblclientes as c on r.idcliente=c.idcliente where r.fecha>='" + desde + "' and r.fecha<='" + hasta + "' "
    '    comm.CommandText += "and r.idsucursal=" + idSucursal.ToString()
    '    If idMesa > 0 Then
    '        comm.CommandText += " r.idmesa=" + idMesa.ToString()
    '    End If
    '    If idSeccion > 0 Then
    '        comm.CommandText += " r.idSeccion=" + idSeccion.ToString()
    '    End If
    '    If idCliente > 0 Then
    '        comm.CommandText += " r.idcliente=" + idCliente.ToString()
    '    End If
    '    Dim da As New MySqlDataAdapter(comm)
    '    Dim ds As New DataSet
    '    da.Fill(ds, "reservaciones")
    '    Return ds.Tables("reservaciones").DefaultView

    'End Function

    Public Function Reservaciones(ByVal fecha As DateTime) As ArrayList
        comm.CommandText = "select * from tblrestaurantereservaciones where fecha >= @fecha order by fecha;"
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Dim lista As New ArrayList
        While dr.Read()
            lista.Add(New Reservacion(dr("idreservacion"), dr("idmesa"), dr("nombre"), dr("fecha")))
        End While
        dr.Close()
        Return lista
    End Function


End Class

Public Class Reservacion
    Public Sub New(id As Integer, idmesa As Integer, nombre As String, fecha As DateTime)
        Me.Id = id
        Me.IdMesa = idmesa
        Me.Nombre = nombre
        Me.Fecha = fecha
    End Sub
    Public Property Id As Integer
    Public Property IdMesa As Integer
    Public Property Nombre As String
    Public Property Fecha As DateTime
End Class