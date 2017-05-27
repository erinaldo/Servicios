Public Class dbComprasRequisicionesDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    Public Cantidad As Double
    Public IdRequisicion As Integer
    Public Razon As String
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdRequisicion = 0
        Razon = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcomprasrequisiciondetalles where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdRequisicion = DReader("idrequisicion")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            Razon = DReader("razon")
        End If
        DReader.Close()
        Inventario = New dbInventario(Idinventario, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdRequisicion As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pRazon As String)
        Dim CTemp As Double
        Dim CRazon As String
        Idinventario = pIdinventario
        Cantidad = pCantidad
        IdRequisicion = pIdRequisicion
        Razon = pRazon
        Comm.CommandText = "select ifnull(max(cantidad),-1) from tblcomprasrequisiciondetalles where idrequisicion=" + IdRequisicion.ToString + " and idinventario=" + Idinventario.ToString
        CTemp = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull(razon,'') from tblcomprasrequisiciondetalles where idrequisicion=" + IdRequisicion.ToString + " and idinventario=" + Idinventario.ToString
        CRazon = Comm.ExecuteScalar
        If CTemp > -1 Then
            Cantidad += CTemp
            Razon += " " + CRazon
            If Razon.Length > 100 Then Razon = Razon.Substring(1, 100)
            Comm.CommandText = "update tblcomprasrequisiciondetalles set cantidad=" + Cantidad.ToString + " where idinventario=" + Idinventario.ToString + " and idrequisicion=" + IdRequisicion.ToString
        Else
            Comm.CommandText = "insert into tblcomprasrequisiciondetalles(idinventario,cantidad,idrequisicion,razon) values(" + Idinventario.ToString + "," + Cantidad.ToString + "," + IdRequisicion.ToString + ",'" + Replace(Razon, "'", "''") + "')"
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select if(max(iddetalle) is null,0,max(iddetalle)) from tblcomprasrequisiciondetalles"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pRazon As String)
        ID = pID
        Razon = pRazon
        Cantidad = pCantidad
        Comm.CommandText = "update tblcomprasrequisiciondetalles set cantidad=" + Cantidad.ToString + ",razon='" + Replace(Razon, "'", "''") + "' where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcomprasrequisiciondetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdRequisicion As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblcomprasrequisiciondetalles.iddetalle,tblcomprasrequisiciondetalles.cantidad,tblinventario.clave,tblinventario.nombre,tblcomprasrequisiciondetalles.razon from tblcomprasrequisiciondetalles inner join tblinventario on tblcomprasrequisiciondetalles.idinventario=tblinventario.idinventario where tblcomprasrequisiciondetalles.idrequisicion=" + pIdRequisicion.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasrequisiciondetalles")
        Return DS.Tables("tblcomprasrequisiciondetalles").DefaultView
    End Function
End Class
