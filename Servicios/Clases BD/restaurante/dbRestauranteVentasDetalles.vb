Imports MySql.Data.MySqlClient
Public Class dbRestauranteVentasDetalles
    Public Property idDetalle As Integer
    Public Property idInventario As Integer
    Public Property cantidad As Double
    Public Property descripcion As String
    Public Property precio As Double
    Public Property iva As Double
    Public Property idVenta As Integer
    Public Property pagado As Integer
    Public Property comentario As String
    Public Property estado As Integer

    Private comm As New MySqlCommand
    Public idUltimoDetalle As Integer
    Public idUltimoInventario As Integer
    Public Comensal As Integer
    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        idDetalle = -1
        idInventario = -1
        cantidad = 0
        descripcion = ""
        precio = 0
        iva = 0
        idVenta = -1
    End Sub

    Public Sub New(ByVal idDetalle As Integer, ByVal conexion As MySqlConnection)
        comm.Connection = conexion
        Me.idDetalle = idDetalle
        llenaDatos()
    End Sub

    Public Function buscar(ByVal idDetalle As Integer) As Boolean
        comm.CommandText = "select * from tblrestauranteventasdetalles where idDetalle=" + idDetalle.ToString()
        Dim res As Integer = comm.ExecuteScalar
        If res > 0 Then
            Me.idDetalle = idDetalle
            llenaDatos()
            Return True
        End If
        Return False
    End Function
    Private Sub llenaDatos()
        comm.CommandText = "select * from tblrestauranteventasdetalles where idDetalle=" + idDetalle.ToString()
        Dim dr As MySqlDataReader
        dr = comm.ExecuteReader
        While dr.Read()
            idDetalle = dr("iddetalle")
            idInventario = dr("idinventario")
            cantidad = dr("cantidad")
            precio = dr("precio")
            descripcion = dr("descripcion")
            iva = dr("iva")
            idVenta = dr("idventa")
            pagado = dr("pagado")
            comentario = dr("comentario")
            estado = dr("estado")
            Comensal = dr("comensal")
        End While
        dr.Close()
    End Sub

    Public Sub agregar(ByVal idInventario As Integer, ByVal cantidad As Double, ByVal descripcion As String, ByVal precio As Double, ByVal iva As Double, ByVal idVenta As Integer, ByVal comentario As String, pComensal As Integer)
        comm.CommandText = "insert into tblrestauranteventasdetalles(idinventario,cantidad,descripcion,precio,iva,idventa,pagado,comentario,estado,comensal) "
        comm.CommandText += "values(" + idInventario.ToString() + "," + cantidad.ToString() + ",'" + descripcion + "'," + precio.ToString() + "," + iva.ToString() + "," + idVenta.ToString() + "," + CInt(estadosPlatillos.inicio).ToString + ",'" + comentario + "'," + CInt(estadosPlatillos.sinEnviar).ToString + "," + pComensal.ToString + ");"
        comm.CommandText += "select ifnull(last_insert_id(),0);"
        idUltimoDetalle = comm.ExecuteScalar
        idUltimoInventario = idInventario
    End Sub

    Public Sub eliminar(ByVal idDetalle As Integer)
        comm.CommandText = "delete from tblrestauranteventasdetalles where idDetalle=" + idDetalle.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub
    Public Sub DaUltimoDetalle(pIdVenta As Integer)
        comm.CommandText = "select ifnull((select idinventario from tblrestauranteventasdetalles where idventa=" + pIdVenta.ToString + " order by iddetalle desc limit 1),0)"
        idUltimoInventario = comm.ExecuteScalar
        comm.CommandText = "select ifnull((select iddetalle from tblrestauranteventasdetalles where idventa=" + pIdVenta.ToString + " order by iddetalle desc limit 1),0)"
        idUltimoDetalle = comm.ExecuteScalar
    End Sub
    Public Function EsBorrable(pIdDetalle As Integer) As Boolean
        Dim est As Byte
        comm.CommandText = "select ifnull((select estado from tblrestauranteventasdetalles where iddetalle=" + pIdDetalle.ToString + "),0)"
        est = comm.ExecuteScalar
        If est = 6 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function vistaDetalles(ByVal idventa As Integer, ByVal estado As Integer, Optional ByVal pagado As Integer = -1) As DataView
        comm.CommandText = "select d.iddetalle,d.cantidad,d.descripcion,d.comensal,d.precio,d.estado from tblrestauranteventasdetalles as d where d.idventa=" + idventa.ToString()
        If pagado > -1 Then
            comm.CommandText += " and pagado=" + pagado.ToString
        Else
            If estado > -1 Then
                comm.CommandText += " and estado=" + estado.ToString()
            End If
        End If
        comm.CommandText += " order by d.comensal,d.iddetalle"
        Dim da As New MySqlDataAdapter(comm)
        Dim ds As New DataSet
        da.Fill(ds, "tblplatillos")
        Return ds.Tables("tblplatillos").DefaultView
    End Function

    Public Sub pagarDetalle(ByVal idDetalle As Integer, ByVal pagado As Integer)
        comm.CommandText = "update tblrestauranteventasdetalles set pagado=" + pagado.ToString() + " where idDetalle=" + idDetalle.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Function cuentaPagadaCompleta(ByVal idventa As Integer) As Boolean
        Dim res As Boolean = False
        comm.CommandText = "select * from tblrestauranteventasdetalles where idventa=" + idventa.ToString + " and pagado=0;"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            res = True
        End If
        Return res
    End Function

    Public Sub modificar(ByVal idDetalle As Integer, ByVal idInventario As Integer, ByVal cantidad As Double, ByVal descripcion As String, ByVal precio As Double, ByVal iva As Double, ByVal idVenta As Integer, ByVal comentario As String)
        comm.CommandText = "update tblrestauranteventasdetalles set idinventario=" + idInventario.ToString() + ", cantidad=" + cantidad.ToString() + ", descripcion='" + descripcion + "', precio=" + precio.ToString() + ", iva=" + iva.ToString() + ", idventa=" + idVenta.ToString() + ", comentario='" + comentario + "' where iddetalle=" + idDetalle.ToString() + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Function buscar(ByVal idVenta As Integer, ByVal idInventario As Integer) As Boolean
        comm.CommandText = "select * from tblrestauranteventasdetalles where idventa=" + idVenta.ToString + " and idinventario=" + idInventario.ToString
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            idDetalle = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function ultimoId() As Integer
        comm.CommandText = "select max(iddetalle) from tblrestauranteventasdetalles;"
        Dim i As Integer = comm.ExecuteScalar
        Return i
    End Function

    Public Sub cambiarEstado(ByVal pidVenta As Integer, ByVal estado As Integer)
        comm.CommandText = "update tblrestauranteventasdetalles set estado=" + estado.ToString + " where idventa=" + pidVenta.ToString + " and estado=6"
        comm.ExecuteNonQuery()
    End Sub

    Public Function platillosMesas() As DataView
        comm.CommandText = "select m.numero, s.nombre,p.descripcion from tblrestauranteventasdetallas as p inner join tblrestaurantemesaventa"
        Return New DataView
    End Function
End Class
