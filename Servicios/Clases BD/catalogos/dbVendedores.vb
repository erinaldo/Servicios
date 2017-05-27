Public Class dbVendedores
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Direccion As String
    Public Telefono As String
    Public Email As String
    'Public Especialidad As String
    Public Clave As String
    Public Ciudad As String
    Public CP As String
    Public Estado As String
    Public Pais As String
    Public zona As String
    Public EsMesero As Byte
    Public EsCajero As Byte

    Public idUsuarioAlta As Integer
    Public fechaAlta As String
    Public horaAlta As String
    Public idUsuarioCambio As Integer
    Public fechaCambio As String
    Public horaCambio As String
    'Public idUsuario As Integer
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Direccion = ""
        Telefono = ""
        Email = ""
        'Especialidad = ""
        Clave = ""
        Ciudad = ""
        CP = ""
        Estado = ""
        Pais = ""
        EsMesero = 0
        EsCajero = 0
        'idUsuario = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)

        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblvendedores where idvendedor=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Direccion = DReader("direccion")
            Telefono = DReader("telefono")
            Email = DReader("email")
            'Especialidad = DReader("especialidad")
            Clave = DReader("clave")
            Ciudad = DReader("ciudad")
            CP = DReader("cp")
            Estado = DReader("estado")
            Pais = DReader("pais")
            zona = DReader("zona")

            idUsuarioAlta = DReader("idUsuarioAlta")
            fechaAlta = DReader("fechaAlta")
            horaAlta = DReader("horaAlta")
            idUsuarioCambio = DReader("idUsuarioCambio")
            fechaCambio = DReader("fechaCambio")
            horaCambio = DReader("horaCambio")
            EsCajero = DReader("escajero")
            EsMesero = DReader("esmesero")
            'idUsuario = DReader("pidusuario")
        End If
        DReader.Close()

    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pClave As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String, ByVal pZona As String, pEsMesero As Byte, pEsCajero As Byte)
        Nombre = pNombre
        Direccion = pDireccion
        Telefono = pTelefono
        Email = pEmail
        'Especialidad = pEspecialidad
        Clave = pClave
        Ciudad = pCiudad
        CP = pCP
        Estado = pEstado
        Pais = pPais
        zona = pZona
        'idUsuario = pIdUsuario
        'If idUsuario < 0 Then idUsuario = 0
        Comm.CommandText = "insert into tblvendedores(nombre,direccion,telefono,email,clave,ciudad,cp,estado,pais,zona,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,escajero,esmesero) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Direccion, "'", "''") + "','" + Replace(Telefono, "'", "''") + "','" + Replace(Email, "'", "''") + "','" + Replace(Clave, "'", "''") + "','" + Replace(Ciudad, "'", "''") + "','" + Replace(CP, "'", "''") + "','" + Replace(Estado, "'", "''") + "','" + Replace(Pais, "'", "''") + "','" + Replace(zona, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + pEsCajero.ToString + "," + pEsMesero.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idvendedor) from tblvendedores"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pDireccion As String, ByVal pTelefono As String, ByVal pEmail As String, ByVal pClave As String, ByVal pCiudad As String, ByVal pCP As String, ByVal pEstado As String, ByVal pPais As String, ByVal pZona As String, pEsCajero As Byte, pEsMesero As Byte)
        ID = pID
        Nombre = pNombre
        Direccion = pDireccion
        Telefono = pTelefono
        Email = pEmail
        'Especialidad = pEspecialidad
        Clave = pClave
        Ciudad = pCiudad
        CP = pCP
        Estado = pEstado
        Pais = pPais
        zona = pZona
        Comm.CommandText = "update tblvendedores set nombre='" + Replace(Nombre, "'", "''") + "',direccion='" + Replace(Direccion, "'", "''") + "',telefono='" + Replace(Telefono, "'", "''") + "',email='" + Replace(Email, "'", "''") + "',clave='" + Replace(Clave, "'", "''") + "',ciudad='" + Replace(Ciudad, "'", "''") + "',cp='" + Replace(CP, "'", "''") + "',estado='" + Replace(Estado, "'", "''") + "',pais='" + Replace(Pais, "'", "''") + "',zona='" + Replace(zona, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "',escajero=" + pEsCajero.ToString + ",esmesero=" + pEsMesero.ToString + " where idvendedor=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblvendedores where idvendedor=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pZona As Integer, Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        'If ClaveyNombre Then
        If pZona > 0 Then
            Comm.CommandText = "select idvendedor,clave,nombre,telefono from tblvendedores where concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%' and zona='" + pZona.ToString() + "'"
        Else
            Comm.CommandText = "select idvendedor,clave,nombre,telefono from tblvendedores where concat(clave,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        End If

        'Else
        'Comm.CommandText = "select idvendedor,clave,nombre,telefono from tblvendedores where nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        'End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblvendedores")
        Return DS.Tables("tblvendedores").DefaultView
    End Function
    Public Function ChecaClaveRepetida(ByVal pClave As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(clave) from tblvendedores where clave='" + Replace(pClave, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaClaveRepetida = False
        Else
            ChecaClaveRepetida = True
        End If
    End Function
    Public Function BuscaVendedor(ByVal pNombre As String) As Boolean
        Comm.CommandText = "select ifnull((select idvendedor from tblvendedores where nombre='" + Replace(pNombre, "'", "''") + "' limit 1),0)"
        ID = Comm.ExecuteScalar
        If ID = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function DaMaximo() As Integer
        Comm.CommandText = "select count(idvendedor) from tblvendedores"
        Return Comm.ExecuteScalar + 1
    End Function
    
End Class
