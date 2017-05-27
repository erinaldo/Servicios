Public Class dbBancosUuids
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public UUID As String
    Public Moneda As Integer
    Public TipodeCambio As Double
    Public Monto As Double
    Public IdPagoProv As Integer
    Public Fecha As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
        llenaDatos(pID)
    End Sub
    Public Sub llenaDatos(pId As Integer)
        ID = pID
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblbancosuuids where iduuid=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            UUID = DReader("uuid")
            Moneda = DReader("moneda")
            TipodeCambio = DReader("tipocambio")
            Monto = DReader("monto")
            IdPagoProv = DReader("idpagoprov")
            Fecha = DReader("fecha")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(pUUID As String, pMoneda As Integer, pTipodeCambio As Double, pMonto As Double, pIdPagoProv As Integer, pFecha As String)
        Comm.CommandText = "insert into tblbancosuuids(uuid,moneda,tipocambio,monto,idpagoprov,fecha) values('" + Replace(pUUID, "'", "''") + "'," + pMoneda.ToString + "," + pTipodeCambio.ToString + "," + pMonto.ToString + "," + pIdPagoProv.ToString + ",'" + pFecha + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(pid As Integer, pUUID As String, pMoneda As Integer, pTipodeCambio As Double, pMonto As Double, pIdPagoProv As Integer, pFecha As String)
        ID = pid
        Comm.CommandText = "update tblbancosuuids set uuid='" + Replace(pUUID, "'", "''") + "',moneda=" + pMoneda.ToString + ",tipocambio=" + pTipodeCambio.ToString + ",monto=" + pMonto.ToString + ",idpagoprov=" + pIdPagoProv.ToString + ",fecha='" + pFecha + "' where iduuid=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblbancosuuids where iduuid=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdPagoProv As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select b.iduuid,b.fecha,b.uuid,m.moneda,b.tipocambio,b.monto from tblbancosuuids b inner join tblmonedassat m on b.moneda=m.id where idpagoprov=" + pIdPagoProv.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbluuids")
        Return DS.Tables("tbluuids").DefaultView
    End Function
    Public Function ChecauuidRepetido(ByVal pUuid As String, pIdPagoProv As Integer) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(uuid) from tblbancosuuids where uuid='" + Replace(pUuid, "'", "''") + "' and idpagoprov=" + pIdPagoProv.ToString
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
