Public Class dbConectorEnvios
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdDocumento As Integer
    Public Documento As Byte
    Public Enviado As Byte
    Public Lic As String
    Public Intentos As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdDocumento = 0
        Documento = 0
        Enviado = 0
        Lic = ""
        Comm.Connection = Conexion
    End Sub
   
    Public Sub Guardar(ByVal pIdDocumento As Integer, ByVal pDocumento As Byte, ByVal pLic As String)
        Comm.CommandText = "insert into tblconectorenvios(iddocumento,documento,enviado,lic) values(" + pIdDocumento.ToString + "," + pDocumento.ToString + ",0,'" + Replace(pLic, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub MarcarEnviado(ByVal pIdDocumento As Integer, ByVal pDocumento As Byte, ByVal plic As String)
        Comm.CommandText = "update tblconectorenvios set enviado=1 where iddocumento=" + pIdDocumento.ToString + " and documento=" + pDocumento.ToString + " and lic='" + Replace(plic, "'", "''") + "'"
        Comm.ExecuteNonQuery()
        Intentos = 0
    End Sub
    
    Public Function ChecaSiHay(ByVal pLic As String) As Boolean
        Dim Resultado As Boolean = False
        'If pDocumento = 0 Then
        Comm.CommandText = "select ifnull((select id from tblconectorenvios where enviado=0 and lic='" + Replace(pLic, "'", "''") + "' limit 1),0)"
        'Else
        ID = Comm.ExecuteScalar
        If ID <> 0 Then
            Comm.CommandText = "select ifnull((select iddocumento from tblconectorenvios where id=" + ID.ToString + "),0)"
            IdDocumento = Comm.ExecuteScalar
            Comm.CommandText = "select ifnull((select documento from tblconectorenvios where id=" + ID.ToString + "),0)"
            Documento = Comm.ExecuteScalar
            Resultado = True
        Else
            IdDocumento = 0
            Documento = 0
        End If
        'End If
        Return Resultado
    End Function
    
End Class
