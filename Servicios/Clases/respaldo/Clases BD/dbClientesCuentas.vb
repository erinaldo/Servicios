Public Class dbClientesCuentas
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Cuenta As String
    Public IdCliente As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Cuenta = ""
        IdCliente = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblclientescuentas where idcuenta=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Cuenta = DReader("cuenta")
            IdCliente = DReader("idcliente")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pCuenta As String, ByVal pIdCliente As String, ByVal idBanco As String, ByVal clabe As String, ByVal numCuenta As String)
        Cuenta = pCuenta
        IdCliente = pIdCliente
        Comm.CommandText = "insert into tblclientescuentas(idcliente,cuenta,clabe,idBanco,numCuenta) values(" + IdCliente.ToString + ",'" + Replace(Trim(Cuenta), "'", "''") + "'," + "'" + Replace(Trim(clabe), "'", "''") + "'" + "," + idBanco.ToString + ",'" + Replace(Trim(numCuenta), "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, Optional ByVal pCuenta As String = "", Optional ByVal clabe As String = "", Optional ByVal idBanco As Integer = 0, Optional ByVal numCuenta As String="")
        ID = pID
        Comm.CommandText = "update tblclientescuentas set cuenta='" + Replace(Trim(pCuenta), "'", "''") + "', clabe='" + Replace(Trim(clabe), "'", "''") + "', idBanco=" + idBanco.ToString + ", numCuenta='" + Replace(Trim(numCuenta), "'", "''") + "' where idcuenta=" + pID.ToString

        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblclientescuentas where idcuenta=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdCliente As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select  idcuenta,cuenta,numCuenta,clabe,tblbancoscatalogo.nombre,tblbancoscatalogo.idbanco from tblclientescuentas,tblbancoscatalogo where idcliente=" + pIdCliente.ToString + " and tblbancoscatalogo.idbanco=tblclientescuentas.idBanco;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientescuentas")
        Return DS.Tables("tblclientescuentas").DefaultView
    End Function


End Class
