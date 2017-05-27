Public Class dbProveedoresCuentas
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Cuenta As String
    Public Clabe As String
    Public IdProv As Integer
    Public IdBanco As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Cuenta = ""
        Clabe = ""
        IdProv = 0
        IdBanco = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Cuenta = ""
        Clabe = ""
        IdBanco = 0
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblproveedorescuentas where idProvCuenta=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Cuenta = DReader("cuenta")
            IdProv = DReader("idProv")
            Clabe = DReader("clabe")
            IdBanco = DReader("idbanco")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pIdProv As String, ByVal cuenta As String, ByVal clabe As String, ByVal idBanco As String)
        IdProv = pIdProv
        Comm.CommandText = "insert into tblproveedorescuentas(cuenta,clabe,idBanco,idProv,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Trim(cuenta), "'", "''") + "'," + "'" + Replace(Trim(clabe), "'", "''") + "'" + "," + idBanco.ToString + "," + IdProv.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCuenta As String, ByVal clabe As String, ByVal banco As String)
        ID = pID
        Cuenta = pCuenta
        Comm.CommandText = "update tblproveedorescuentas set cuenta='" + Replace(Trim(Cuenta), "'", "''") + "'," + "clabe='" + Replace(Trim(clabe), "'", "''") + "', idBanco=" + banco + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idprovCuenta=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblproveedorescuentas where idProvCuenta=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdProv As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idProvCuenta,cuenta,clabe,tblbancoscatalogo.nombre,tblbancoscatalogo.idbanco from tblproveedorescuentas,tblbancoscatalogo where idProv=" + pIdProv.ToString + " and tblbancoscatalogo.idbanco=tblproveedorescuentas.idBanco;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproveedorescuentas")
        Return DS.Tables("tblproveedorescuentas").DefaultView
    End Function

    Public Function consultaCuenta(ByVal id As Integer) As String()
        Dim DS As New DataSet
        Dim res(3) As String
        Comm.CommandText = "select idBanco,cuenta,clabe from tblproveedorescuentas, where idProvCuenta=" + id.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproveedorescuentas")
        res(0) = DS.Tables(0).Rows(0)("idBanco").ToString()
        res(1) = DS.Tables(0).Rows(0)("cuenta").ToString()
        res(2) = DS.Tables(0).Rows(0)("clabe").ToString()
        Return res
    End Function
    Public Function consultaSet(idprov As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select idBanco,cuenta,clabe from tblproveedorescuentas where idProvCuenta=" + ID.ToString()
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproveedorescuentas")
        Return DS.Tables(0)
    End Function


End Class
