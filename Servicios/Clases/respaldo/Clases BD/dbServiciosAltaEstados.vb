Imports MySql.Data.MySqlClient
Public Class dbServiciosAltaEstados
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Codigo As String
    Public Estado As String

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = 0
        Codigo = ""
        Estado = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblserviciosestados where idEstado=" + ID.ToString()
        DReader = Comm.ExecuteReader
        If DReader.Read() Then

            Codigo = DReader("codigo")
            Estado = DReader("estado")

        End If
        DReader.Close()
    End Sub

    Public Sub Guardar(ByVal pCodigo As String, ByVal pEstado As String)
        Codigo = pCodigo
        Estado = pEstado
        Comm.CommandText = "insert into tblserviciosestados(codigo,estado) values('" + Replace(Codigo, "'", "''") + "','" + Replace(Estado, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub

    Public Function ClaveRepetida(ByVal pCodigo As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(codigo) from tblserviciosestados where codigo='" + Replace(pCodigo, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ClaveRepetida = False
        Else
            ClaveRepetida = True
        End If
    End Function
    Public Function reporte() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select codigo,estado,idEstado from tblserviciosestados;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosestados")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblserviciosestados").DefaultView
    End Function
    Public Function Consultar(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select codigo,estado,idEstado from tblserviciosestados where concat(codigo,estado) like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosestados")
        Return DS.Tables("tblserviciosestados").DefaultView
    End Function
    Public Sub Modificar(ByVal pID As Integer, ByVal pCodigo As String, ByVal pNombre As String)
        ID = pID
        Estado = pNombre
        Codigo = pCodigo
        Comm.CommandText = "update tblserviciosestados set codigo='" + Replace(Codigo, "'", "''") + "',estado='" + Replace(Estado, "'", "''") + "' where idEstado=" + ID.ToString()
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblserviciosestados where idEstado=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
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
        Comm.CommandText = "select count(codigo) from tblserviciosestados where codigo='" + Replace(x, "'", "''") + "'"
        Resultado1 = Comm.ExecuteScalar
        If Resultado1 = 0 Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Function EstadoRepetida(ByVal pEstado As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(estado) from tblserviciosestados where estado='" + Replace(pEstado, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
