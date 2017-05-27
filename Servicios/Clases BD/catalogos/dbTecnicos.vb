Public Class dbTecnicos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Direccion As String
    Public Telefono As String
    Public Email As String
    Public Especialidad As String
    Public Clave As String
    Public Ciudad As String
    Public CP As String
    Public Estado As String
    Public Pais As String

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Direccion = ""
        Telefono = ""
        Email = ""
        Especialidad = ""
        Clave = ""
        Ciudad = ""
        CP = ""
        Estado = ""
        Pais = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)

        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbltecnicos where idtecnico=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Direccion = DReader("direccion")
            Telefono = DReader("telefono")
            Email = DReader("email")
            Especialidad = DReader("especialidad")
            Clave = DReader("clave")
            Ciudad = DReader("ciudad")
            CP = DReader("cp")
            Estado = DReader("estado")
            Pais = DReader("pais")
        End If
        DReader.Close()

    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pEspecialidad As String, ByVal pClave As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String)
        Nombre = pNombre
        Direccion = pDireccion
        Telefono = pTelefono
        Email = pEmail
        Especialidad = pEspecialidad
        Clave = pClave
        Ciudad = pCiudad
        CP = pCP
        Estado = pEstado
        Pais = pPais
        Comm.CommandText = "insert into tbltecnicos(nombre,direccion,telefono,email,especialidad,clave,ciudad,cp,estado,pais,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Direccion, "'", "''") + "','" + Replace(Telefono, "'", "''") + "','" + Replace(Email, "'", "''") + "','" + Replace(Especialidad, "'", "''") + "','" + Replace(Clave, "'", "''") + "','" + Replace(Ciudad, "'", "''") + "','" + Replace(CP, "'", "''") + "','" + Replace(Estado, "'", "''") + "','" + Replace(Pais, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pEspecialidad As String, ByVal pClave As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String)
        ID = pID
        Nombre = pNombre
        Direccion = pDireccion
        Telefono = pTelefono
        Email = pEmail
        Especialidad = pEspecialidad
        Clave = pClave
        Ciudad = pCiudad
        CP = pCP
        Estado = pEstado
        Pais = pPais
        Comm.CommandText = "update tbltecnicos set nombre='" + Replace(Nombre, "'", "''") + "',direccion='" + Replace(Direccion, "'", "''") + "',telefono='" + Replace(Telefono, "'", "''") + "',email='" + Replace(Email, "'", "''") + "',especialidad='" + Replace(Especialidad, "'", "''") + "',clave='" + Replace(Clave, "'", "''") + "',ciudad='" + Replace(Ciudad, "'", "''") + "',cp='" + Replace(CP, "'", "''") + "',estado='" + Replace(Estado, "'", "''") + "',pais='" + Replace(Pais, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idtecnico=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbltecnicos where idtecnico=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        'If ClaveyNombre Then
        Comm.CommandText = "select idtecnico,clave,nombre,telefono from tbltecnicos where concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        'Else
        'Comm.CommandText = "select idtecnico,clave,nombre,telefono from tbltecnicos where nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        'End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbltecnicos")
        Return DS.Tables("tbltecnicos").DefaultView
    End Function
    Public Function ChecaClaveRepetida(ByVal pClave As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(clave) from tbltecnicos where clave='" + Replace(pClave, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaClaveRepetida = False
        Else
            ChecaClaveRepetida = True
        End If
    End Function
    Public Function DaMaximo() As Integer
        Comm.CommandText = "select count(idtecnico) from tbltecnicos"
        Return Comm.ExecuteScalar + 1
    End Function
End Class
