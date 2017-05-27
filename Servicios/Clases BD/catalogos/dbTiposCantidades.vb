Public Class dbTiposCantidades
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Abreviatura As String
    Public UsaBascula As Byte
    Public ParaRemision As Byte
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Abreviatura = ""
        UsaBascula = 0
        ParaRemision = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbltiposcantidades where idtipocantidad=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Abreviatura = DReader("abreviatura")
            UsaBascula = DReader("usabascula")
            ParaRemision = DReader("pararemision")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pAbreviatura As String, ByVal pUsaBascula As Byte, pParaRemision As Byte)
        Nombre = pNombre
        Abreviatura = pAbreviatura
        UsaBascula = pUsaBascula
        ParaRemision = pParaRemision
        Comm.CommandText = "insert into tbltiposcantidades(nombre,abreviatura,usabascula,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,pararemision) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Abreviatura, "'", "''") + "'," + UsaBascula.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + ParaRemision.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idtipocantidad) from tbltiposcantidades"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pAbreviatura As String, ByVal pUsaBascula As Byte, pPararemision As Byte)
        ID = pID
        Nombre = pNombre
        Abreviatura = pAbreviatura
        UsaBascula = pUsaBascula
        ParaRemision = pPararemision
        Comm.CommandText = "update tbltiposcantidades set nombre='" + Replace(Nombre, "'", "''") + "',abreviatura='" + Replace(Abreviatura, "'", "''") + "',usabascula=" + UsaBascula.ToString + ",idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "',pararemision=" + ParaRemision.ToString + " where idtipocantidad=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbltiposcantidades where idtipocantidad=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idtipocantidad,nombre,abreviatura from tbltiposcantidades where idtipocantidad>1 and nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbltiposcantidfades")
        Return DS.Tables("tbltiposcantidades").DefaultView
    End Function
    Public Function BuscaUnidad(ByVal pUnidad As String) As Boolean
        Dim Encontro As Integer
        Comm.CommandText = " select ifnull((select idtipocantidad from tbltiposcantidades where nombre='" + Replace(pUnidad, "'", "''") + "' limit 1),0)"
        Encontro = Comm.ExecuteScalar
        If Encontro = 0 Then
            Return False
        Else
            ID = Encontro
            Return True
        End If
    End Function
End Class
