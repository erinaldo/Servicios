Public Class dbEquipoInternoDetalles
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Marca As String

    Public Modelo As String
    Public NoSerie As String
    Public NoMotor As String
    Public Matricula As String
    Public Color As String
    Public Kilometraje As Double

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Marca = ""
        Modelo = ""
        NoSerie = ""
        NoMotor = ""
        Matricula = ""
        Color = ""
        Kilometraje = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblequipointerno where idequipo=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Marca = DReader("marca")
            Modelo = DReader("modelo")
            NoSerie = DReader("noserie")
            NoMotor = DReader("nomotor")
            Matricula = DReader("matricula")
            Color = DReader("color")
            Kilometraje = DReader("kilometraje")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pMarca As String, ByVal pModelo As String, ByVal pNoSerie As String, ByVal pNoMotor As String, ByVal pMatricula As String, ByVal pColor As String, ByVal pKilometraje As Double)
        Nombre = pNombre
        Marca = pMarca
        Modelo = pModelo
        NoSerie = pNoSerie
        NoMotor = pNoMotor
        Matricula = pMatricula
        Color = pColor
        Kilometraje = pKilometraje
        Comm.CommandText = "insert into tblequipointerno(nombre,marca,modelo,noserie,nomotor,matricula,color,kilometraje) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Marca, "'", "''") + "','" + Replace(Modelo, "'", "''") + "','" + Replace(NoSerie, "'", "''") + "','" + Replace(NoMotor, "'", "''") + "','" + Replace(Matricula, "'", "''") + "','" + Replace(Color, "'", "''") + "'," + Kilometraje.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pMarca As String, ByVal pModelo As String, ByVal pNoSerie As String, ByVal pNoMotor As String, ByVal pMatricula As String, ByVal pColor As String, ByVal pKilometraje As Double)
        ID = pID
        Nombre = pNombre
        Marca = pMarca
        Modelo = pModelo
        NoSerie = pNoSerie
        NoMotor = pNoMotor
        Matricula = pMatricula
        Color = pColor
        Kilometraje = pKilometraje
        Comm.CommandText = "update tblequipointerno set nombre='" + Replace(Nombre, "'", "''") + "',marca='" + Replace(Marca, "'", "''") + "',modelo='" + Replace(Modelo, "'", "''") + "',noserie='" + Replace(NoSerie, "'", "''") + "',nomotor='" + Replace(NoMotor, "'", "''") + "',matricula='" + Replace(Matricula, "'", "''") + "',color='" + Replace(Color, "'", "''") + "',kilometraje=" + Kilometraje.ToString + " where idequipo=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblequipointerno where idequipo=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdcliente As Integer, Optional ByVal pTextoBusqueda As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idequipo,nombre,matricula,noserie from tblequipointerno where concat(nombre,matricula) like '%" + Replace(pTextoBusqueda, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblequipointerno")
        Return DS.Tables("tblequipointerno").DefaultView
    End Function
End Class
