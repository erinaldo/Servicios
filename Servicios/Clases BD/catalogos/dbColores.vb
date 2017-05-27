Public Class dbColores
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Codigo As String
    Public Comentario As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Codigo = ""
        Comentario = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcolores where idcolor=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Codigo = DReader("codigo")
            Comentario = DReader("comentario")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pDireccion As String, ByVal pComentario As String)
        Nombre = pNombre
        Codigo = pDireccion
        Comentario = pComentario
        Comm.CommandText = "insert into tblcolores(nombre,codigo,comentario,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Codigo, "'", "''") + "','" + Replace(Comentario, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pDireccion As String, ByVal pComentario As String)
        ID = pID
        Nombre = pNombre
        Codigo = pDireccion
        Comentario = pComentario
        Comm.CommandText = "update tblcolores set nombre='" + Replace(Nombre, "'", "''") + "',codigo='" + Replace(Codigo, "'", "''") + "',comentario='" + Replace(Comentario, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString() + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idcolor=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcolores where idcolor=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pNombre As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idcolor,codigo,nombre,comentario from tblcolores where concat(codigo,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcolores")
        Return DS.Tables("tblcolores").DefaultView
    End Function
    Public Function ChecaNumeroRepetido(ByVal pNumero As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(codigo) from tblcolores where codigo='" + Replace(pNumero, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaNumeroRepetido = False
        Else
            ChecaNumeroRepetido = True
        End If
    End Function
    Public Function reporte() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idcolor,codigo,nombre,comentario from tblcolores;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcolores")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblcolores").DefaultView
    End Function
    Public Function DaMaximo() As Integer
        Dim Maximo As Integer
        Comm.CommandText = "select ifnull((select count(codigo) from tblcolores),0)"
        Maximo = Comm.ExecuteScalar + 1
        While ChecaNumeroRepetido(Format(Maximo, "00"))
            Maximo += 1
        End While
        Return Maximo
    End Function
End Class
