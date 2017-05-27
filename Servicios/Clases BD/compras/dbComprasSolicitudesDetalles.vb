Public Class dbComprasSolicitudesDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    Public Cantidad As Double
    Public IdSolicitud As Integer
    Public Razon As String
    Public CantidadSurtida As Integer
    Public Estado As Byte
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdSolicitud = 0
        Razon = ""
        CantidadSurtida = 0
        Estado = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcomprassolicitudesdetalles where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdSolicitud = DReader("idsolicitud")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            Razon = DReader("razon")
            CantidadSurtida = DReader("cantidadsurtida")
            Estado = DReader("estado")
        End If
        DReader.Close()
        Inventario = New dbInventario(Idinventario, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdSolicitud As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pRazon As String)
        Dim CTemp As Double
        Idinventario = pIdinventario
        Cantidad = pCantidad
        IdSolicitud = pIdSolicitud
        Razon = pRazon
        Comm.CommandText = "select if(max(cantidad) is null,-1,cantidad) from tblcomprassolicitudesdetalles where idsolicitud=" + IdSolicitud.ToString + " and idinventario=" + Idinventario.ToString
        CTemp = Comm.ExecuteScalar
        If CTemp > -1 Then
            Cantidad += CTemp
            Comm.CommandText = "update tblcomprassolicitudesdetalles set cantidad=" + Cantidad.ToString + " where idinventario=" + Idinventario.ToString + " and idsolicitud=" + IdSolicitud.ToString
        Else
            Comm.CommandText = "insert into tblcomprassolicitudesdetalles(idinventario,cantidad,idsolicitud,razon,cantidadsurtida,estado) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + IdSolicitud.ToString + ",'" + Replace(Razon, "'", "''") + "',0,0)"
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select if(max(iddetalle) is null,0,max(iddetalle)) from tblcomprassolicitudesdetalles"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pRazon As String)
        ID = pID
        Razon = pRazon
        Cantidad = pCantidad
        Comm.CommandText = "update tblcomprassolicitudesdetalles set cantidad=" + Cantidad.ToString + ",razon='" + Replace(Razon, "'", "''") + "' where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarEstado(ByVal pID As Integer, ByVal pCantidadSurtida As Double, ByVal pEstado As Byte)
        ID = pID
        Estado = pEstado
        CantidadSurtida = pCantidadSurtida
        Comm.CommandText = "update tblcomprassolicitudesdetalles set cantidadsurtida=" + CantidadSurtida.ToString + ",estado=" + Estado.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcomprassolicitudesdetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdSolicitud As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblcomprassolicitudesdetalles.iddetalle,tblcomprassolicitudesdetalles.cantidad,tblinventario.clave,tblinventario.nombre,tblcomprassolicitudesdetalles.razon,tblcomprassolicitudesdetalles.cantidadsurtida,tblcomprassolicitudesdetalles.estado from tblcomprassolicitudesdetalles inner join tblinventario on tblcomprassolicitudesdetalles.idinventario=tblinventario.idinventario where tblcomprassolicitudesdetalles.idsolicitud=" + pIdSolicitud.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprassolicitudesdetalles")
        Return DS.Tables("tblcomprassolicitudesdetalles").DefaultView
    End Function
End Class
