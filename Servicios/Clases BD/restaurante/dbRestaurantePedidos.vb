Imports MySql.Data.MySqlClient
Public Class dbRestaurantePedidos
    Private comm As New MySqlCommand
#Region "Atributos"
    Public idPedido As Integer
    Public idVenta As Integer
    Public fecha As String
    Public hora As String
    Public serie As String
    Public folio As Integer
    Public estado As Integer
    Public idVendedor As Integer
    Public llevar As Integer
#End Region

#Region "Metodos"
    Public Sub New(ByVal conexion As MySqlConnection)
        comm.Connection = conexion
    End Sub

    Public Sub New(ByVal idPedido As Integer, ByVal conexion As MySqlConnection)
        Me.idPedido = idPedido
        comm.Connection = conexion
        llenaDatos()
    End Sub

    Private Sub llenaDatos()
        comm.CommandText = "select * from tblrestaurantepedidos where idpedido=" + idPedido.ToString
        Dim dr As MySqlDataReader = comm.ExecuteReader()
        While dr.Read()
            idVenta = dr("idventa")
            fecha = dr("fecha")
            hora = dr("hora")
            serie = dr("serie")
            folio = dr("folio")
            estado = dr("estado")
            idVendedor = dr("idvendedor")
            llevar = dr("llevar")
        End While
        dr.Close()
    End Sub

    Public Sub agregar(ByVal idVenta As Integer, ByVal fecha As String, ByVal hora As String, ByVal serie As String, ByVal folio As Integer, ByVal estado As Integer, ByVal idVendedor As Integer, ByVal llevar As Integer)
        comm.CommandText = "insert into tblrestaurantepedidos(idventa,fecha,hora,serie,folio,estado,idvendedor,llevar) values(" + idVenta.ToString + ",'" + fecha + "','" + hora + "','" + serie + "'," + folio.ToString + "," + estado.ToString + "," + idVendedor.ToString + "," + llevar.ToString + ");"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub modificar(ByVal idPedido As Integer, ByVal idVenta As Integer, ByVal fecha As String, ByVal hora As String, ByVal serie As String, ByVal folio As String, ByVal estado As Integer, ByVal idVendedor As Integer, ByVal llevar As Integer)
        comm.CommandText = "update tblrestaurantepedidos set idventa=" + idVenta.ToString + ", fecha='" + fecha + "', hora='" + hora + "', serie='" + serie + "', folio=" + folio.ToString + ", estado=" + estado.ToString + ", idVendedor=" + idVendedor.ToString + ", llevar=" + llevar.ToString + " where idpedido=" + idPedido.ToString + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Sub eliminar(ByVal idPedido As Integer)
        comm.CommandText = "delete from tblrestaurantepedidos where idpedido=" + idPedido.ToString + ";"
        comm.ExecuteNonQuery()
    End Sub

    Public Function buscar(ByVal idPedido As Integer) As Boolean
        comm.CommandText = "select idpedido from tblrestaurantepedidos where idpedido=" + idPedido.ToString
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idPedido = idPedido
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function vistaPedidos(ByVal desde As String, ByVal hasta As String) As DataView
        comm.CommandText = "select p.idpedido,p.idventa,p.estado,v.folio,c.nombre,p.fecha,p.hora from tblrestaurantepedidos as p inner join tblrestauranteventas as v on p.idventa=v.idventa inner join tblclientes as c on v.idcliente=c.idcliente where p.fecha>='" + desde + "' and p.fecha<='" + hasta + "';"
        Dim da As New MySqlDataAdapter(comm)
        Dim ds As New DataSet
        da.Fill(ds, "pedidos")
        Return ds.Tables("pedidos").DefaultView
    End Function

    Public Function buscar(ByVal serie As String, ByVal folio As Integer) As Boolean
        comm.CommandText = "select idPedido from tblrestaurantepedidos where serie='" + serie + "' and folio=" + folio.ToString + ";"
        Dim i As Integer = comm.ExecuteScalar
        If i > 0 Then
            Me.idPedido = i
            llenaDatos()
            Return True
        End If
        Return False
    End Function

    Public Function obtenFolio() As Integer
        comm.CommandText = " select ifnull(max(folio),0) as folio from tblrestaurantepedidos;"
        Dim i As Integer = comm.ExecuteScalar
        Return i + 1
    End Function

    Public Function listaPedidos(ByVal estado As Integer, Optional ByVal llevar As Integer = -1) As List(Of Integer)
        comm.CommandText = "select idpedido from tblrestaurantepedidos where estado=" + estado.ToString
        If llevar > -1 Then
            comm.CommandText += " and llevar=" + llevar.ToString
        End If
        Dim dr As MySqlDataReader = comm.ExecuteReader
        Dim lista As New List(Of Integer)
        While dr.Read()
            lista.Add(dr("idpedido"))
        End While
        dr.Close()
        Return lista
    End Function

    Public Function ultimoId() As Integer
        comm.CommandText = "select max(idpedido) from tblrestaurantepedidos;"
        Dim i As Integer = comm.ExecuteScalar
        Return i
    End Function
#End Region
End Class
