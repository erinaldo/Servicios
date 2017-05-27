Imports MySql.Data.MySqlClient
Public Class dbRestauranteMesas
    Private comm As MySqlCommand
    Dim IdSucursal As Integer
    Public Sub New(ByVal conexion As MySqlConnection, pIdSucursal As Integer)
        comm = New MySqlCommand
        comm.Connection = conexion
        IdSucursal = pIdSucursal
    End Sub

    Public Function agregar(ByRef mesa As RestauranteMesa) As Boolean
        comm.CommandText = "insert into tblrestaurantemesas(numero,seccion,ancho,alto,x,y,estado,capacidad) values("
        comm.CommandText += mesa.numero.ToString() + ","
        comm.CommandText += mesa.seccion.ToString() + ","
        comm.CommandText += mesa.Width.ToString() + ","
        comm.CommandText += mesa.Height.ToString() + ","
        comm.CommandText += mesa.X.ToString() + ","
        comm.CommandText += mesa.Y.ToString() + ","
        comm.CommandText += mesa.estado.ToString() + ","
        comm.CommandText += mesa.capacidad.ToString() + ");"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function modificar(ByRef mesa As RestauranteMesa) As Boolean
        comm.CommandText = "update tblrestaurantemesas set "
        comm.CommandText += "numero=" + mesa.numero.ToString() + ", "
        comm.CommandText += "seccion=" + mesa.seccion.ToString() + ", "
        comm.CommandText += "ancho=" + mesa.Width.ToString() + ", "
        comm.CommandText += "alto=" + mesa.Height.ToString() + ", "
        comm.CommandText += "x=" + mesa.X.ToString() + ", "
        comm.CommandText += "y=" + mesa.Y.ToString() + ", "
        comm.CommandText += "estado=" + mesa.estado.ToString() + ", "
        comm.CommandText += "capacidad=" + mesa.capacidad.ToString()
        comm.CommandText += " where idMesa=" + mesa.id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminar(ByVal id As Integer) As Boolean
        comm.CommandText = "delete from tblrestaurantemesas where idMesa=" + id.ToString() + ";"
        Try
            comm.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function buscar(ByVal id As Integer) As RestauranteMesa
        comm.CommandText = "select * from tblrestaurantemesas where idMesa=" + id.ToString() + ";"
        Dim dr As MySqlDataReader
        Dim mesa As RestauranteMesa = Nothing
        dr = comm.ExecuteReader()
        While dr.Read()
            mesa = New RestauranteMesa(IdSucursal)
            mesa.id = dr("idMesa")
            mesa.numero = dr("numero")
            mesa.seccion = dr("seccion")
            mesa.Width = dr("ancho")
            mesa.Height = dr("alto")
            mesa.X = dr("x")
            mesa.Y = dr("y")
            mesa.estado = dr("estado")
            mesa.capacidad = dr("capacidad")
        End While
        dr.Close()
        Return mesa
    End Function

    Public Function listaMesas(ByVal idSeccion As Integer) As List(Of RestauranteMesa)
        Dim lista As New List(Of RestauranteMesa)
        Dim mesa As RestauranteMesa
        comm.CommandText = "select *,if(ifnull((select count(rt.estado) from tblrestauranteventas rt where rt.estado=2 and rt.idmesa=rm.idmesa),0)<>0,1,if(rm.estado=3,2,0)) estadov from tblrestaurantemesas rm where seccion=" + idSeccion.ToString() + ";"
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        While dr.Read()
            mesa = New RestauranteMesa(IdSucursal)
            mesa.id = dr("idmesa")
            mesa.numero = dr("numero")
            mesa.Height = dr("alto")
            mesa.Width = dr("ancho")
            mesa.X = dr("x")
            mesa.Y = dr("y")
            mesa.seccion = dr("seccion")
            mesa.estado = dr("estadov")
            mesa.Location = New Point(mesa.X, mesa.Y)
            mesa.capacidad = dr("capacidad")
            'mesa.checaEstado()
            lista.Add(mesa)
        End While
        dr.Close()
        Return lista
    End Function
    Public Function listaMesasEstado(ByVal idSeccion As Integer, ByVal estado As Integer) As List(Of RestauranteMesa)
        Dim lista As New List(Of RestauranteMesa)
        Dim mesa As RestauranteMesa
        comm.CommandText = "select * from tblrestaurantemesas where seccion=" + idSeccion.ToString() + " and estado=" + estado.ToString + ";"

        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        While dr.Read()
            mesa = New RestauranteMesa(IdSucursal)
            mesa.id = dr("idmesa")
            mesa.numero = dr("numero")
            mesa.Height = dr("alto")
            mesa.Width = dr("ancho")
            mesa.X = dr("x")
            mesa.Y = dr("y")
            mesa.seccion = dr("seccion")
            mesa.estado = dr("estado")
            mesa.Location = New Point(mesa.X, mesa.Y)
            mesa.capacidad = dr("capacidad")
            'mesa.checaEstado()
            lista.Add(mesa)
        End While
        dr.Close()
        Return lista
    End Function
    Public Function CuentaMesas(pIdSeccion As Integer) As Integer
        comm.CommandText = "select ifnull((select numero from tblrestaurantemesas where seccion=" + pIdSeccion.ToString + " order by numero desc limit 1),0)"
        Return comm.ExecuteScalar + 1
    End Function
    Public Function ultimoId() As Integer
        comm.CommandText = "select idMesa as id from tblrestaurantemesas"
        Return comm.ExecuteScalar
    End Function

    Public Function totalComensales(ByVal idMesa As Integer) As Integer
        comm.CommandText = "select count(*) from tblrestaurantecomensales where mesa=" + idMesa.ToString() + ";"
        Return comm.ExecuteScalar
    End Function

    Public Sub dibujaMesas(ByRef lienzo As System.Windows.Forms.Panel, ByVal idSucursal As Integer)
        'Dim g As Graphics = lienzo.CreateGraphics
        Dim lista As List(Of RestauranteMesa) = listaMesas(idSucursal)
        For Each m As RestauranteMesa In lista
            lienzo.Controls.Add(m)
        Next
    End Sub

    Public Function vistaMesas(ByVal idSeccion As Integer) As DataView
        comm.CommandText = "select numero from tblrestaurantemesas where seccion=" + idSeccion.ToString() + ";"
        Dim ds As New DataSet
        Dim da As New MySqlDataAdapter(comm)
        da.Fill(ds, "mesas")
        Return ds.Tables("mesas").DefaultView
    End Function
End Class
