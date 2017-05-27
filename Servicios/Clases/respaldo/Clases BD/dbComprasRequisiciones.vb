Public Class dbComprasRequisiciones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Fecha As String
    Public Referencia As String
    Public Estado As Byte
    Public Autorizar As Byte
    Public Observaciones As String
    Public IdSucursal As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Fecha = ""
        Referencia = ""
        Estado = 0
        Autorizar = 0
        Observaciones = ""
        IdSucursal = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcomprasrequisicion where idrequisicion=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Fecha = DReader("fecha")
            Referencia = DReader("referencia")
            Estado = DReader("estado")
            Autorizar = DReader("autorizar")
            Observaciones = DReader("observaciones")
            IdSucursal = DReader("idsucursal")
        End If
        DReader.Close()

    End Sub
    Public Sub Guardar(ByVal pFecha As String, ByVal pReferencia As String, ByVal pEstado As Byte, ByVal pObservaciones As String, ByVal pIdSucursal As Integer)
        Fecha = pFecha
        Referencia = pReferencia
        Estado = pEstado
        Observaciones = pObservaciones
        IdSucursal = pIdSucursal
        Comm.CommandText = "insert into tblcomprasrequisicion(fecha,referencia,estado,autorizar,observaciones,idsucursal) values('" + Fecha + "','" + Replace(Referencia, "'", "''") + "'," + Estado.ToString + ",0,'" + Replace(Observaciones, "'", "''") + "'," + IdSucursal.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idrequisicion) from tblcomprasrequisicion"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pReferencia As String, ByVal pEstado As Byte, ByVal pAutorizar As Byte, ByVal pObservaciones As String)
        ID = pID
        Fecha = pFecha
        Referencia = pReferencia
        Estado = pEstado
        Autorizar = pAutorizar
        Observaciones = pObservaciones
        Comm.CommandText = "update tblcomprasrequisicion set fecha='" + Fecha + "',referencia='" + Replace(Referencia, "'", "''") + "',estado=" + Estado.ToString + ",autorizar=" + Autorizar.ToString + ",observaciones='" + Replace(Observaciones, "'", "''") + "' where idrequisicion=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcomprasrequisicion where idrequisicion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, Optional ByVal pReferencia As String = "", Optional ByVal pEstado As Byte = 0) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblcomprasrequisicion.idrequisicion,tblcomprasrequisicion.fecha,tblcomprasrequisicion.referencia,tblcomprasrequisicion.estado from tblcomprasrequisicion where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pReferencia <> "" Then
            Comm.CommandText += " and tblcomprasrequisicion.referencia like '%" + Replace(pReferencia, "'", "''") + "%'"
        End If
        If pEstado > 0 Then
            Comm.CommandText += " and tblcomprasrequisicion.estado=" + pEstado.ToString
        End If
        Comm.CommandText += " order by tblcomprasrequisicion.fecha,tblcomprasrequisicion.referencia"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasrequisicion")
        Return DS.Tables("tblcomprasrequisicion").DefaultView
    End Function
    
End Class
