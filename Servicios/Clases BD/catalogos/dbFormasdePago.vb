Public Class dbFormasdePago
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Tipo As Byte
    Public clavesat As Integer
    Public Parabancos As Byte
    Public Enum Tipos
        Contado = 1
        Credito = 0
        Parcialidad = 2
    End Enum
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Tipo = 0
        clavesat = 99
        Parabancos = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblformasdepago where idforma=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Tipo = DReader("tipo")
            clavesat = DReader("clavesat")
            Parabancos = DReader("parabancos")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pTipo As Tipos, pClaveSat As Integer, pParabancos As Byte)
        Nombre = pNombre
        Tipo = pTipo
        Comm.CommandText = "insert into tblformasdepago(nombre,tipo,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,clavesat,parabancos) values('" + Replace(Nombre, "'", "''") + "'," + Tipo.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + pClaveSat.ToString() + "," + pParabancos.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idforma) from tblformasdepago"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pTipo As Tipos, pClavesat As Integer, pParaBancos As Byte)
        ID = pID
        Nombre = pNombre
        Tipo = pTipo
        Comm.CommandText = "update tblformasdepago set nombre='" + Replace(Nombre, "'", "''") + "',tipo=" + Tipo.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "',clavesat=" + pClavesat.ToString() + ",parabancos=" + pParaBancos.ToString + " where idforma=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblformasdepago where idforma=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idconcepto,nombre,precio from tblformasdepago where nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblformasdepago")
        Return DS.Tables("tblformasdepago").DefaultView
    End Function

    Public Function BuscaForma(ByVal pNombre As String, ByVal pTipo As Byte) As Boolean
        Dim Encontro As Integer
        Comm.CommandText = " select ifnull((select idforma from tblformasdepago where tipo=" + pTipo.ToString + " and nombre='" + Replace(pNombre, "'", "''") + "' limit 1),0)"
        Encontro = Comm.ExecuteScalar
        If Encontro = 0 Then
            Return False
        Else
            ID = Encontro
            Return True
        End If
    End Function
    Public Function FormaSat(idFormaSat As Integer) As String
        Comm.CommandText = "select nombre from tblformasdepagosat where id=" + idFormaSat.ToString
        Return Comm.ExecuteScalar
    End Function
End Class
