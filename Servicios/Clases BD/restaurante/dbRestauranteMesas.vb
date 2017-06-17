Imports MySql.Data.MySqlClient
Public Class dbRestauranteMesas
    Private comm As MySqlCommand
    Dim IdSucursal As Integer
    Public Sub New(ByVal conexion As MySqlConnection, pIdSucursal As Integer)
        comm = New MySqlCommand
        comm.Connection = conexion
        IdSucursal = pIdSucursal
    End Sub

    Public Sub Agregar(ByRef mesa As RestauranteMesa)
        comm.CommandText = "insert into tblrestaurantemesas(numero,seccion,ancho,alto,x,y,estado,capacidad) values("
        comm.CommandText += mesa.Numero.ToString() + ","
        comm.CommandText += mesa.Seccion.ToString() + ","
        comm.CommandText += mesa.Width.ToString() + ","
        comm.CommandText += mesa.Height.ToString() + ","
        comm.CommandText += mesa.Left.ToString() + ","
        comm.CommandText += mesa.Top.ToString() + ","
        comm.CommandText += mesa.Estado.ToString() + ","
        comm.CommandText += mesa.Capacidad.ToString() + ");"
        comm.ExecuteNonQuery()
    End Sub

    Public Function Modificar(ByRef mesa As RestauranteMesa) As Boolean
        comm.CommandText = "update tblrestaurantemesas set "
        comm.CommandText += "numero=" + mesa.Numero.ToString() + ", "
        comm.CommandText += "seccion=" + mesa.Seccion.ToString() + ", "
        comm.CommandText += "ancho=" + mesa.Width.ToString() + ", "
        comm.CommandText += "alto=" + mesa.Height.ToString() + ", "
        comm.CommandText += "x=" + mesa.Left.ToString() + ", "
        comm.CommandText += "y=" + mesa.Top.ToString() + ", "
        comm.CommandText += "estado=" + mesa.Estado.ToString() + ", "
        comm.CommandText += "capacidad=" + mesa.Capacidad.ToString()
        comm.CommandText += " where idMesa=" + mesa.Id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Eliminar(ByVal id As Integer) As Boolean
        comm.CommandText = "delete from tblrestaurantemesas where idMesa=" + id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Buscar(ByVal id As Integer) As RestauranteMesa
        comm.CommandText = "select * from tblrestaurantemesas where idMesa=" + id.ToString() + ";"
        Dim dr As MySqlDataReader
        Dim mesa As RestauranteMesa = Nothing
        dr = comm.ExecuteReader()
        While dr.Read()
            mesa = New RestauranteMesa(IdSucursal)
            mesa.Id = dr("idMesa")
            mesa.Numero = dr("numero")
            mesa.Seccion = dr("seccion")
            mesa.Width = dr("ancho")
            mesa.Height = dr("alto")
            mesa.Top = dr("y")
            mesa.Left = dr("x")
            mesa.Estado = dr("estado")
            mesa.Capacidad = dr("capacidad")
        End While
        dr.Close()
        Return mesa
    End Function

    Public Function CuentaMesas(pIdSeccion As Integer) As Integer
        comm.CommandText = "select ifnull((select numero from tblrestaurantemesas where seccion=" + pIdSeccion.ToString + " order by numero desc limit 1),0)"
        Return comm.ExecuteScalar + 1
    End Function

    Public Sub Desocupar(idVenta As Integer)
        Try
            comm.CommandText = "select count(*) from tblrestaurantereservaciones where fecha>=now() and idmesa=(select idmesa from tblrestauranteventas where idventa=@idventa);"
            comm.Parameters.Add(New MySqlParameter("@idventa", idVenta))
            If comm.ExecuteScalar = 0 Then
                comm.CommandText = "delete from tblrestaurantecomensales where mesa = (select idmesa from tblrestauranteventas where idventa=@idventa); update tblrestaurantemesas set estado = " + CInt(EstadosMesas.Libre).ToString() + " where idmesa = (select idmesa from tblrestauranteventas where idventa=@idventa); delete from tblrestauranteventas where idventa = @idventa;"
            Else
                comm.CommandText = "delete from tblrestaurantecomensales where mesa = (select idmesa from tblrestauranteventas where idventa=@idventa); update tblrestaurantemesas set estado = " + CInt(EstadosMesas.Reservada).ToString() + " where idmesa = (select idmesa from tblrestauranteventas where idventa=@idventa); delete from tblrestauranteventas where idventa = @idventa;"
            End If
            comm.ExecuteNonQuery()
        Finally
            comm.Parameters.Clear()
        End Try
    End Sub


    Public Function Mesas(ByVal idseccion As Integer, estado As Integer) As ArrayList
        comm.CommandText = "update tblrestaurantemesas left outer join tblrestaurantereservaciones on tblrestaurantemesas.idmesa=tblrestaurantereservaciones.idmesa and fecha>=now() left outer join tblrestauranteventas on tblrestauranteventas.idmesa=tblrestaurantemesas.idmesa and tblrestauranteventas.estado=" + CInt(EstadosMesas.Ocupada).ToString() + " set tblrestaurantemesas.estado= case isnull(tblrestauranteventas.estado) when 1 then " + CInt(EstadosMesas.Libre).ToString() + " else " + CInt(EstadosMesas.Ocupada).ToString() + " end where seccion= " + idseccion.ToString() + " and tblrestaurantemesas.estado= " + CInt(EstadosMesas.Reservada).ToString() + " and isnull(tblrestaurantereservaciones.idreservacion); select *, colorlibre, colorocupado, colorreservado, colorsucio, colorletralibre, colorletraocupado, colorletrareservado, colorletrasucio, ifnull(v.nombre,'') vendedor, min(rr.fecha) reservacion from tblrestaurantemesas rm inner join tblrestauranteconfiguracion rc left outer join tblrestauranteventas rv on rv.idmesa=rm.idmesa and rv.estado = 2 left outer join tblvendedores v on rv.idmesero=v.idvendedor left outer join tblrestaurantereservaciones rr on rr.idmesa=rm.idmesa and rr.fecha>=now()  where seccion=@seccion"
        If estado <> -1 Then comm.CommandText += " and tblrestaurantemesas.estado=@estado"
        comm.CommandText += " group by numero order by numero;"
        comm.Parameters.Add(New MySqlParameter("@seccion", idseccion))
        comm.Parameters.Add(New MySqlParameter("@estado", estado))
        Dim array As New ArrayList
        Dim dr As MySqlDataReader = Nothing
        Try
            dr = comm.ExecuteReader
            While dr.Read
                Select dr("estado")
                    Case EstadosMesas.Libre
                        'If dr("reservacion") IsNot DBNull.Value Then
                        '    Dim m As New RestauranteMesa(dr("idmesa"), dr("numero"), dr("seccion"), EstadosMesas.Reservada, dr("capacidad"), IdSucursal, dr("y"), dr("x"))
                        '    If dr("colorReservado") <> "" Then m.BackColor = Color.FromArgb(dr("colorReservado"))
                        '    If dr("colorletraReservado") <> "" Then m.ForeColor = Color.FromArgb(dr("colorletraReservado"))
                        '    m.Text = "Mesa " + m.Numero.ToString() + vbNewLine + Format(dr("reservacion"), "HH:mm")
                        '    array.Add(m)
                        'Else
                        Dim m As New RestauranteMesa(dr("idmesa"), dr("numero"), dr("seccion"), dr("estado"), dr("capacidad"), IdSucursal, dr("y"), dr("x"))
                        If dr("colorLibre") <> "" Then m.BackColor = Color.FromArgb(dr("colorLibre"))
                        If dr("colorletraLibre") <> "" Then m.ForeColor = Color.FromArgb(dr("colorletraLibre"))
                        m.Text = "Mesa " + m.Numero.ToString() + vbNewLine + dr("vendedor")
                        array.Add(m)
                        'End If
                    Case EstadosMesas.Ocupada
                        Dim m As New RestauranteMesa(dr("idmesa"), dr("numero"), dr("seccion"), dr("estado"), dr("capacidad"), IdSucursal, dr("y"), dr("x"))
                        If dr("colorOcupado") <> "" Then m.BackColor = Color.FromArgb(dr("colorOcupado"))
                        If dr("colorletraOcupado") <> "" Then m.ForeColor = Color.FromArgb(dr("colorletraOcupado"))
                        m.Text = "Mesa " + m.Numero.ToString() + vbNewLine + dr("vendedor")
                        array.Add(m)
                    Case EstadosMesas.Reservada
                        Dim m As New RestauranteMesa(dr("idmesa"), dr("numero"), dr("seccion"), EstadosMesas.Reservada, dr("capacidad"), IdSucursal, dr("y"), dr("x"))
                        If dr("colorReservado") <> "" Then m.BackColor = Color.FromArgb(dr("colorReservado"))
                        If dr("colorletraReservado") <> "" Then m.ForeColor = Color.FromArgb(dr("colorletraReservado"))
                        m.Text = "Mesa " + m.Numero.ToString() + vbNewLine + Format(dr("reservacion"), "HH:mm")
                        array.Add(m)
                    Case EstadosMesas.Sucia
                        Dim m As New RestauranteMesa(dr("idmesa"), dr("numero"), dr("seccion"), dr("estado"), dr("capacidad"), IdSucursal, dr("y"), dr("x"))
                        If dr("colorSucio") <> "" Then m.BackColor = Color.FromArgb(dr("colorSucio"))
                        If dr("colorletraSucio") <> "" Then m.ForeColor = Color.FromArgb(dr("colorletraSucio"))
                        If dr("reservacion") IsNot DBNull.Value Then
                            m.Text = "Mesa " + m.Numero.ToString() + vbNewLine + Format(dr("reservacion"), "HH:mm")
                        Else
                            m.Text = "Mesa " + m.Numero.ToString() + vbNewLine + dr("vendedor")
                        End If
                        array.Add(m)
                End Select
            End While
            Return array
        Finally
            If Not dr.IsClosed Then dr.Close()
            comm.Parameters.Clear()

        End Try
    End Function
End Class
