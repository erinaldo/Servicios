Imports MySql.Data.MySqlClient
Public Class dbBanco
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Codigo As String
    Public Nombre As String
    Public Consulta As String

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = 0
        Codigo = ""
        Nombre = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblbancos where idBanco=" + ID.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then

            Codigo = DReader("codigo")
            Nombre = DReader("Nombre")
            'Numero = DReader("numero")
            ' IdSucursal = DReader("idsucursal")
        End If
        DReader.Close()
    End Sub

    Public Sub Guardar(ByVal pCodigo As String, ByVal pNombre As String)
        Codigo = pCodigo
        Nombre = pNombre
        Comm.CommandText = "insert into tblbancos(codigo,Nombre) values('" + Replace(Codigo, "'", "''") + "','" + Replace(Nombre, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub

    Public Function ClaveRepetida(ByVal pCodigo As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(codigo) from tblbancos where codigo='" + Replace(pCodigo, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ClaveRepetida = False
        Else
            ClaveRepetida = True
        End If
    End Function

    Public Function Consultar(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select codigo,Nombre,idBanco from tblbancos where concat(codigo,Nombre) like '%" + Replace(pNombre, "'", "''") + "%' and idBanco<>0"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        Return DS.Tables("tblbancos").DefaultView
    End Function

    Public Function reporte() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select codigo,Nombre,idBanco from tblbancos;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblbancos").DefaultView
    End Function
    Public Sub Modificar(ByVal pID As Integer, ByVal pCodigo As String, ByVal pNombre As String)
        ID = pID
        Nombre = pNombre
        Codigo = pCodigo
        Comm.CommandText = "update tblbancos set codigo='" + Replace(Codigo, "'", "''") + "',Nombre='" + Replace(Nombre, "'", "''") + "' where idBanco=" + ID.ToString()
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblbancos where idBanco=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function ChecaNumeroRepetido(ByVal pNumero As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(codigo) from tblbancos where codigo='" + Replace(pNumero, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaNumeroRepetido = False
        Else
            ChecaNumeroRepetido = True
        End If
    End Function
    Public Function Bancos(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select Nombre from tblbancos"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancos")
        Return DS.Tables("tblbancos").DefaultView
    End Function
    Public Function Folio() As String
        Dim Resultado As Integer = 0
        Dim repetida As Integer = 0
        Dim Rep As String = ""
       
        Resultado = 1
        Rep = Format(Resultado, "0000")
        repetida = esRepetida3(Rep)

        If repetida > 0 Then 'es repetida

            Do While repetida > 0
                Resultado = Integer.Parse(Rep)
                Resultado = Resultado + 1
                Rep = Format(Resultado, "0000")
                repetida = esRepetida3(Rep)

            Loop
            Return Format(Resultado, "0000")
        Else
            Return Format(Resultado, "0000")
        End If
    End Function

    Public Function esRepetida3(ByVal x As String) As Integer
        Dim Resultado1 As Integer
        'checar si esta usandose
        Comm.CommandText = "select count(codigo) from tblbancos where codigo='" + Replace(x, "'", "''") + "'"
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Function Reporte1() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select clave as codigo,nombre,idbanco from tblbancoscatalogo order by Nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblbancoscatalogo")
        'DS.WriteXmlSchema("tblbancos.xml")
        Return DS.Tables("tblbancoscatalogo").DefaultView
    End Function

End Class
