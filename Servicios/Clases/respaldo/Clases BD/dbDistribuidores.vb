Imports MySql.Data.MySqlClient
Public Class dbDistribuidores
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public nombre As String
    Public contacto As String
    Public telefono As String
    Public celular As String
    Public email As String
    Public direccion As String
    Public comentario As String
    Public ID As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        nombre = ""
        contacto = ""
        telefono = ""
        celular = ""
        email = ""
        direccion = ""
        comentario = ""
        Comm.Connection = Conexion

    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos(ID)
    End Sub
    Public Sub LlenaDatos(ByVal pid As Integer)
        ID = pid
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbldistribuidor where ID=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            nombre = DReader("nombre")
            contacto = DReader("contacto")
            telefono = DReader("telefono")
            celular = DReader("celular")
            email = DReader("email")
            direccion = DReader("direccion")
            comentario = DReader("comentario")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pContacto As String, ByVal pTelefono As String, ByVal pCelular As String, ByVal pEmail As String, ByVal pDireccion As String, ByVal pComentario As String)
        nombre = pNombre
        contacto = pContacto
        telefono = pTelefono
        celular = pCelular
        email = pEmail
        direccion = pDireccion
        comentario = pComentario
        Comm.CommandText = "insert into tbldistribuidor(nombre,contacto,telefono,celular, email, direccion,comentario) values('" + Replace(nombre, "'", "''") + "','" + Replace(contacto, "'", "''") + "','" + Replace(telefono, "'", "''") + "','" + Replace(celular, "'", "''") + "','" + Replace(email, "'", "''") + "','" + Replace(direccion, "'", "''") + "','" + Replace(comentario, "'", "''") + "');"
        Comm.CommandText += "select max(ID) from tbldistribuidor;"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pId As Integer, ByVal pNombre As String, ByVal pContacto As String, ByVal pTelefono As String, ByVal pCelular As String, ByVal pEmail As String, ByVal pDireccion As String, ByVal pComentario As String)
        nombre = pNombre
        contacto = pContacto
        telefono = pTelefono
        celular = pCelular
        email = pEmail
        direccion = pDireccion
        comentario = pComentario
        ID = pId
        Comm.CommandText = "update tbldistribuidor set nombre='" + Replace(nombre, "'", "''") + "',contacto='" + Replace(contacto, "'", "''") + "',telefono='" + Replace(telefono, "'", "''") + "',celular='" + Replace(celular, "'", "''") + "', email='" + Replace(email, "'", "''") + "', direccion='" + Replace(direccion, "'", "''") + "',comentario='" + Replace(comentario, "'", "''") + "' where ID=" + ID.ToString + ";"
        Comm.ExecuteScalar()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbldistribuidor where ID=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pnombre As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select * from tbldistribuidor  where nombre like '%" + pnombre + "%' or contacto like '%" + pnombre + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldistribuidor")
        Return DS.Tables("tbldistribuidor").DefaultView
    End Function
End Class
