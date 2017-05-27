Public Class dbFormasdePagoRemisiones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Tipo As Byte
    Public Codigo As String
    Public Enum Tipos
        Contado = 1
        Credito = 3
        Otro = 2
    End Enum
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Tipo = 0
        Codigo = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblformasdepagoremisiones where idforma=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Tipo = DReader("tipo")
            Codigo = DReader("codigo")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pTipo As Tipos, ByVal pCodigo As String)
        Nombre = pNombre
        Tipo = pTipo
        Codigo = pCodigo
        Comm.CommandText = "insert into tblformasdepagoremisiones(nombre,tipo,codigo,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Nombre, "'", "''") + "'," + Tipo.ToString + ",'" + Replace(Codigo, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idforma) from tblformasdepagoremisiones"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pTipo As Tipos, ByVal pCodigo As String)
        ID = pID
        Nombre = pNombre
        Tipo = pTipo
        Codigo = pCodigo
        Comm.CommandText = "update tblformasdepagoremisiones set nombre='" + Replace(Nombre, "'", "''") + "',tipo=" + Tipo.ToString + ",codigo='" + Replace(Codigo, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idforma=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblformasdepagoremisiones where idforma=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idconcepto,nombre,precio,codigo from tblformasdepagoreisiones where nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblformasdepagor")
        Return DS.Tables("tblformasdepagor").DefaultView
    End Function

    Public Function BuscaForma(ByVal pCodigo As String) As Boolean
        Dim Encontro As Integer
        Comm.CommandText = " select ifnull((select idforma from tblformasdepagoremisiones where codigo='" + Replace(pCodigo, "'", "''") + "' limit 1),0)"
        Encontro = Comm.ExecuteScalar
        If Encontro = 0 Then
            Return False
        Else
            ID = Encontro
            LlenaDatos()
            Return True
        End If
    End Function
    Public Function ChecaCodigoRepetida(ByVal pClave As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(codigo) from tblformasdepagoremisiones where codigo='" + Replace(pClave, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaCodigoRepetida = False
        Else
            ChecaCodigoRepetida = True
        End If
    End Function
    Public Function listaFormas() As List(Of Integer)
        Dim lista As New List(Of Integer)
        Comm.CommandText = "select idforma from tblformasdepagoremisiones;"
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader = Comm.ExecuteReader()
        While dr.Read()
            lista.Add(dr("idforma"))
        End While
        dr.Close()
        Return lista
    End Function

End Class
