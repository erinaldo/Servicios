Public Class dbEmpresas
    Public MySqlconE As MySql.Data.MySqlClient.MySqlConnection
    Public Comm As MySql.Data.MySqlClient.MySqlCommand
    Public NombreEmpresa As String
    Public IdEmpresa As Integer
    Public NombreDB As String
    Public Servidor As String
    Public Usuario As String
    Dim Password() As Byte
    Public EsDefault As Byte
    Public PasswordS As String

    Public Function IniciarMySQLE(ByVal DBaConectarse As String, ByVal ServidorAConectarse As String, ByVal DBUsuario As String, ByVal DBPassword As String, ByVal DBPuerto As String) As Integer
        Dim TodoBien As Integer = 1
        Try
            If DBPuerto <> "" Then
                MySqlconE = New MySql.Data.MySqlClient.MySqlConnection("Server=" + ServidorAConectarse + ";Database=" + DBaConectarse + ";Uid=" + DBUsuario + ";Pwd=" + DBPassword + ";Port=" + DBPuerto)
            Else
                MySqlconE = New MySql.Data.MySqlClient.MySqlConnection("Server=" + ServidorAConectarse + ";Database=" + DBaConectarse + ";Uid=" + DBUsuario + ";Pwd=" + DBPassword + ";")
            End If
            Comm = New MySql.Data.MySqlClient.MySqlCommand
            MySqlconE.Open()
            Comm.Connection = MySqlconE
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, GlobalNombreApp)
            TodoBien = 0
        End Try
        Return TodoBien
    End Function
    Public Function Guardar(ByVal pNombreEmpresa As String, ByVal pNombreDB As String, ByVal pUsuario As String, ByVal pPassword As String, ByVal pServidor As String, ByVal pDefault As Byte) As Integer
        Dim E As New Encriptador
        Password = E.Encriptar3DES(pPassword)
        Dim P As New MySql.Data.MySqlClient.MySqlParameter
        P.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Blob
        P.ParameterName = "@pass"
        P.Value = Password
        NombreEmpresa = pNombreEmpresa
        NombreDB = pNombreDB
        Servidor = pServidor
        Usuario = pUsuario
        EsDefault = pDefault
        Comm.Parameters.Clear()
        Comm.Parameters.Add(P)
        Comm.CommandText = "insert into tblempresas(nombreempresa,db,pass,servidor,usuario,esdefault) values('" + Replace(Trim(NombreEmpresa), "'", "''") + "','" + Replace(Trim(NombreDB), "'", "''") + "',@pass,'" + Replace(Trim(Servidor), "'", "''") + "','" + Replace(Trim(Usuario), "'", "''") + "'," + EsDefault.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idempresa) from tblempresas"
        Return Comm.ExecuteScalar
    End Function
    Public Sub Modificar(ByVal pId As Integer, ByVal pNombreEmpresa As String, ByVal pNombreDB As String, ByVal pUsuario As String, ByVal pPassword As String, ByVal pServidor As String, ByVal pDefault As Byte)
        'If pDefault = 1 Then
        '    Comm.CommandText = "update tblempresas set esdefault=0"
        '    Comm.ExecuteNonQuery()
        'End If
        Dim E As New Encriptador
        Password = E.Encriptar3DES(pPassword)
        Dim P As New MySql.Data.MySqlClient.MySqlParameter
        P.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Blob
        P.ParameterName = "@pass"
        P.Value = Password
        NombreEmpresa = pNombreEmpresa
        NombreDB = pNombreDB
        Servidor = pServidor
        Usuario = pUsuario
        EsDefault = pDefault
        Comm.Parameters.Clear()
        Comm.Parameters.Add(P)
        Comm.CommandText = "update tblempresas set nombreempresa='" + Replace(Trim(NombreEmpresa), "'", "''") + "',db='" + Replace(Trim(NombreDB), "'", "''") + "',pass=@pass,servidor='" + Replace(Trim(Servidor), "'", "''") + "',usuario='" + Replace(Trim(Usuario), "'", "''") + "',esdefault=" + EsDefault.ToString + " where idempresa=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub LlenaDatos(ByVal pIDempresa As Integer)
        IdEmpresa = pIDempresa
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblempresas where idempresa=" + IdEmpresa.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            NombreEmpresa = DReader("nombreempresa")
            Password = DReader("pass")
            NombreDB = DReader("db")
            Servidor = DReader("servidor")
            Usuario = DReader("usuario")
            EsDefault = DReader("esdefault")
        End If
        DReader.Close()
        Dim E As New Encriptador
        PasswordS = E.Desencriptar3DES(Password)
    End Sub
    Public Function ConsultaReader() As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select idempresa,nombreempresa from tblempresas order by nombreempresa"
        Return Comm.ExecuteReader
    End Function
    Public Function Consulta(ByVal pNombreEmpresa As String, ByVal pIdEmpresaDefault As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idempresa,nombreempresa,if(idempresa=" + pIdEmpresaDefault.ToString + ",'Si','No') as D from tblempresas where nombreempresa like '%" + Replace(Trim(pNombreEmpresa), "'", "''") + "%' order by nombreempresa"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblempresas")
        Return DS.Tables("tblempresas").DefaultView
    End Function
    Public Function ChecaEmpresaRepetida(ByVal pNombreEmpresa As String) As Boolean
        Comm.CommandText = "select count(idempresa) from tblempresas where nombreempresa='" + Replace(Trim(pNombreEmpresa), "'", "''") + "'"
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Sub Eliminar(ByVal pidEmpresa As Integer)
        Comm.CommandText = "delete from tblempresas where idempresa=" + pidEmpresa.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub MarcarDefault(ByVal pidEmpresa As Integer)
        Comm.CommandText = "update tblempresas set esdefault=0"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblempresas set esdefault=1 where idempresa=" + pidEmpresa.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaDefault() As Integer
        Comm.CommandText = "select ifnull((select idempresa from tblempresas where esdefault=1 limit 1),0)"
        Return Comm.ExecuteScalar
    End Function
End Class
