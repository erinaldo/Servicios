Public Class dbLogErrores
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        Comm.Connection = Conexion
    End Sub
    Public Sub AddError(pDescripcion As String, pDondeFue As String, pFecha As String, pHora As String)
        Comm.CommandText = "insert into tbllogdeerrores(descripcion,dondefue,fecha,hora) values('" + pDescripcion + "','" + pDondeFue + "','" + pFecha + "','" + pHora + "')"
        Comm.ExecuteNonQuery()
    End Sub

End Class
