Public Class dbMonedas
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Abreviatura As String

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Abreviatura = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblmonedas where idmoneda=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Abreviatura = DReader("abreviatura")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pAbreviatura As String)
        Nombre = pNombre
        Abreviatura = pAbreviatura
        Comm.CommandText = "insert into tblmonedas(nombre,abreviatura) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Abreviatura, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pAbreviatura As String)
        ID = pID
        Nombre = pNombre
        Abreviatura = pAbreviatura
        Comm.CommandText = "update tblmonedas set nombre='" + Replace(Nombre, "'", "''") + "',abreviatura='" + Replace(Abreviatura, "'", "''") + "' where idmoneda=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblmonedas where idmoneda=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idmoneda,nombre,abreviatura from tblmonedas where idmoneda>1 and nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmonedas")
        Return DS.Tables("tblmonedas").DefaultView
    End Function
End Class
