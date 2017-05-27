Public Class dbValidacionXML
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public fecha As String
    Public uuid As String
    Public rfc As String
    Public monto As Double
    Public ID As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        fecha = ""
        uuid = ""
        rfc = ""
        monto = 0
        ID = -1
        Comm.Connection = Conexion
    End Sub
    Public Sub guardar(ByVal puuid As String, pRFC As String, pMonto As Double)
        Comm.CommandText = "insert into tblxmlvalidados(fecha,uuid,rfc,monto) values('" + Date.Now.ToString("yyyy/MM/dd") + "','" + puuid + "','" + pRFC + "'," + pMonto.ToString + ");"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(id) from tblxmlvalidados"
        ID = Comm.ExecuteScalar
    End Sub
    Public Function contador()
        Comm.CommandText = "select count(id) from tblxmlvalidados"
        Return Comm.ExecuteScalar
    End Function
End Class
