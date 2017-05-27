Public Class dbModelos
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
        Comm.CommandText = "select * from tblmodelos where idmodelo=" + ID.ToString
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
        Comm.CommandText = "insert into tblmodelos(nombre,codigo,comentario) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Codigo, "'", "''") + "','" + Replace(Comentario, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pDireccion As String, ByVal pComentario As String)
        ID = pID
        Nombre = pNombre
        Codigo = pDireccion
        Comentario = pComentario
        Comm.CommandText = "update tblmodelos set nombre='" + Replace(Nombre, "'", "''") + "',codigo='" + Replace(Codigo, "'", "''") + "',comentario='" + Replace(Comentario, "'", "''") + "' where idmodelo=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblmodelos where idmodelo=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pNombre As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idmodelo,codigo,nombre,comentario from tblmodelos where concat(codigo,nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmodelos")
        Return DS.Tables("tblmodelos").DefaultView
    End Function
    Public Function ChecaNumeroRepetido(ByVal pNumero As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(codigo) from tblmodelos where codigo='" + Replace(pNumero, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaNumeroRepetido = False
        Else
            ChecaNumeroRepetido = True
        End If
    End Function
    Public Function reporte() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idmodelo,codigo,nombre,comentario from tblmodelos;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmodelos")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblmodelos").DefaultView
    End Function
    Public Function DaMaximo() As Integer
        Dim Maximo As Integer
        Comm.CommandText = "select ifnull((select count(codigo) from tblmodelos),0)"
        Maximo = Comm.ExecuteScalar + 1
        While ChecaNumeroRepetido(Format(Maximo, "00"))
            Maximo += 1
        End While
        Return Maximo
    End Function
    Public Function Generacombinaciones(ByVal C As Boolean, ByVal T As Boolean, ByVal A As Boolean) As DataView
        Comm.CommandText = "delete from tblmtctemp"
        Comm.ExecuteNonQuery()
        If C Then
            Comm.CommandText = "insert into tblmtctemp(color,talla,codigoc,codigot) select nombre,'',codigo,'' from tblcolores order by codigo"
            Comm.ExecuteNonQuery()
        End If
        If T Then
            Comm.CommandText = "insert into tblmtctemp(color,talla,codigoc,codigot) select '',nombre,'',codigo from tbltallas order by codigo"
            Comm.ExecuteNonQuery()
        End If
        If A Then
            Comm.CommandText = "insert into tblmtctemp(color,talla,codigoc,codigot) select c.nombre,t.nombre,c.codigo,t.codigo from tblcolores c,tbltallas t order by c.codigo"
            Comm.ExecuteNonQuery()
        End If
        Comm.CommandText = "select 0 as sel,color,talla,codigoc,codigot from tblmtctemp"
        Dim DS As New DataSet
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmtctemp")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblmtctemp").DefaultView
    End Function
End Class
