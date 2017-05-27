Public Class dbSucursalesCertificados
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdSucursal As Integer
    Public NoSerie As String
    Dim PasswordKey() As Byte
    Public PasswordS As String
    Public Certificado As String
    Public Activo As Byte
    Public FechaVencimiento As String
    Public Aviso As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        NoSerie = ""
        IdSucursal = 0
        PasswordS = ""
        Activo = 0
        FechaVencimiento = "2000/01/01"
        Aviso = 15
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        If ID = 0 Then
            NoSerie = ""
            IdSucursal = 0
            PasswordS = ""
            Activo = 0
        Else
            LlenaDatos()
        End If

    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblsucursalescertificados where idcertificado=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdSucursal = DReader("idsucursal")
            NoSerie = DReader("noserie")
            Certificado = DReader("certificado")
            PasswordKey = DReader("passkey")
            Activo = DReader("activo")
            FechaVencimiento = DReader("fechavencimiento")
            Aviso = DReader("aviso")
        Else
            NoSerie = ""
            IdSucursal = 0
            PasswordS = ""
            Activo = 0
        End If
        DReader.Close()
        Dim E As New Encriptador
        PasswordS = E.Desencriptar3DES(PasswordKey)
    End Sub
    Public Sub Guardar(ByVal pIdSucursal As Integer, ByVal pNoSerie As String, ByVal pCertificado As String, ByVal pPassKey As String, ByVal pActivo As Byte, ByVal pFechaVencimiento As String, ByVal pAviso As Integer)
        Dim E As New Encriptador
        IdSucursal = pIdSucursal
        NoSerie = pNoSerie
        PasswordS = pPassKey
        PasswordKey = E.Encriptar3DES(PasswordS)
        Activo = pActivo
        FechaVencimiento = pFechaVencimiento
        Aviso = pAviso
        Dim P As New MySql.Data.MySqlClient.MySqlParameter
        P.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Blob
        P.ParameterName = "@pass"
        P.Value = PasswordKey
        Comm.Parameters.Clear()
        Comm.Parameters.Add(P)
        Comm.CommandText = "insert into tblsucursalescertificados(idsucursal,noserie,certificado,passkey,activo,fechavencimiento,aviso,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + IdSucursal.ToString + ",'" + Replace(NoSerie, "'", "''") + "','" + Replace(Certificado, "'", "''") + "',@pass," + Activo.ToString + ",'" + FechaVencimiento + "'," + Aviso.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pId As Integer, ByVal pIdSucursal As Integer, ByVal pNoSerie As String, ByVal pCertificado As String, ByVal pPassKey As String, ByVal pActivo As Byte, ByVal pFechaVencimiento As String, ByVal pAviso As Integer)
        Dim E As New Encriptador
        IdSucursal = pIdSucursal
        NoSerie = pNoSerie
        PasswordS = pPassKey
        PasswordKey = E.Encriptar3DES(PasswordS)
        Activo = pActivo
        FechaVencimiento = pFechaVencimiento
        Aviso = pAviso
        Dim P As New MySql.Data.MySqlClient.MySqlParameter
        P.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Blob
        P.ParameterName = "@pass"
        P.Value = PasswordKey
        Comm.Parameters.Clear()
        Comm.Parameters.Add(P)
        Comm.CommandText = "update tblsucursalescertificados set noserie='" + Replace(NoSerie, "'", "''") + "',certificado='" + Replace(Certificado, "'", "''") + "',passkey=@pass,activo=" + Activo.ToString + ",fechavencimiento='" + FechaVencimiento + "',aviso=" + Aviso.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idcertificado=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblsucursalescertificados where idcertificado=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdSucursal As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idcertificado,noserie,if(activo=0,'No','Si') as ceractivo from tblsucursalescertificados where idsucursal=" + pIdSucursal.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblsucursalescertificados")
        Return DS.Tables("tblsucursalescertificados").DefaultView
    End Function
    Public Function ChecaSerieRepetida(ByVal pNoSerie As String, ByVal pidSucursal As Integer) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(idcertificado) from tblsucursalescertificados where noserie='" + Replace(pNoSerie.ToLower, "'", "''") + "' and idsucursal=" + pidSucursal.ToString
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaSerieRepetida = False
        Else
            ChecaSerieRepetida = True
        End If
    End Function
    Public Function ChecaArchivoPfx(ByVal pRutaCer As String, ByVal pPassKey As String) As Boolean
        If System.IO.File.Exists(pRutaCer + ".pfx") Then
            Dim Fi As New System.IO.FileInfo(pRutaCer + ".pfx")
            If Fi.Length <= 0 Then
                Certificado = Fi.Name
                ChecaArchivoPfx = False
            Else
                ChecaArchivoPfx = True
            End If
        Else
            ChecaArchivoPfx = False
        End If
    End Function
    Public Function BuscaCertificado(ByVal pidSucursal As Integer) As Boolean
        Comm.CommandText = "select ifnull((select idcertificado from tblsucursalescertificados where idsucursal=" + pidSucursal.ToString + " and activo=1),0)"
        ID = Comm.ExecuteScalar
        If ID = 0 Then
            Return False
        Else
            LlenaDatos()
            Return True
        End If
    End Function
End Class
