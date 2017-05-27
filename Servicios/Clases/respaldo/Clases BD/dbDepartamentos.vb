Public Class dbDepartamentos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Codigo As String
    Dim Tabla As String
    Dim IdNivelSuperior As Integer
    Dim TipoC As TiposDepartamentos
    Public Enum TiposDepartamentos
        Nivel1 = 1
        Nivel2 = 2
    End Enum
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection, ByVal Tipo As TiposDepartamentos)
        ID = -1
        Nombre = ""
        TipoC = Tipo
        IdNivelSuperior = 1
        Select Case Tipo
            Case TiposDepartamentos.Nivel1
                Tabla = "tbldepartamentos"
            Case TiposDepartamentos.Nivel2
                Tabla = "tbldepartamentosareas"
        End Select
        Codigo = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection, ByVal Tipo As TiposDepartamentos)
        ID = pID
        Comm.Connection = Conexion
        Select Case Tipo
            Case TiposDepartamentos.Nivel1
                Tabla = "tbldepartamentos"
            Case TiposDepartamentos.Nivel2
                Tabla = "tbldepartamentosareas"
        End Select
        TipoC = Tipo
        LlenaDatos()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pCodigo As String, Optional ByVal pIdNivelSuperior As Integer = 1)
        Nombre = pNombre
        Codigo = pCodigo
        IdNivelSuperior = pIdNivelSuperior
        If TipoC = TiposDepartamentos.Nivel1 Then
            Comm.CommandText = "insert into " + Tabla + "(nombre,codigo) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Codigo, "'", "''") + "')"
        Else
            Comm.CommandText = "insert into " + Tabla + "(nombre,codigo,iddepartamentos) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Codigo, "'", "''") + "'," + IdNivelSuperior.ToString + ")"
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(iddepartamento) from " + Tabla
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pCodido As String)
        ID = pID
        Nombre = pNombre
        Codigo = pCodido
        Comm.CommandText = "update " + Tabla + " set nombre='" + Replace(Nombre, "'", "''") + "',codigo='" + Replace(Codigo, "'", "''") + "' where iddepartamento=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from " + Tabla + " where iddepartamento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select iddepartamento,nombre from " + Tabla + " where  nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, Tabla)
        Return DS.Tables(Tabla).DefaultView
    End Function
    Public Function ChecaCodigoRepetido(ByVal pCodigo As String, Optional ByVal pIDnivelSuperior As Integer = 1) As Boolean
        Dim Resultado As Integer = 0
        Select Case TipoC
            Case TiposDepartamentos.Nivel1
                Comm.CommandText = "select count(codigo) from " + Tabla + " where codigo='" + Replace(pCodigo, "'", "''") + "'"
            Case TiposDepartamentos.Nivel2
                Comm.CommandText = "select count(codigo) from " + Tabla + " where codigo='" + Replace(pCodigo, "'", "''") + "' and iddepartamentos=" + pIDnivelSuperior.ToString
        End Select
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaCodigoRepetido = False
        Else
            ChecaCodigoRepetido = True
        End If
    End Function
    Public Function ChecaNombreRepetido(ByVal pNombre As String, Optional ByVal pIDnivelSuperior As Integer = 1) As Boolean
        Dim Resultado As Integer = 0
        Select Case TipoC
            Case TiposDepartamentos.Nivel1
                Comm.CommandText = "select count(nombre) from " + Tabla + " where nombre='" + Replace(pNombre, "'", "''") + "'"
            Case TiposDepartamentos.Nivel2
                Comm.CommandText = "select count(nombre) from " + Tabla + " where nombre='" + Replace(pNombre, "'", "''") + "' and iddepartamentos=" + pIDnivelSuperior.ToString
        End Select
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaNombreRepetido = False
        Else
            ChecaNombreRepetido = True
        End If
    End Function
    Public Function BuscaDepartamento(ByVal pCodigo As String, Optional ByVal pIdNivelSuperior As Integer = 1) As Boolean
        Dim Encontro As Integer
        Select Case TipoC
            Case TiposDepartamentos.Nivel1
                Comm.CommandText = " select if((select iddepartamento from " + Tabla + " where codigo='" + Replace(pCodigo, "'", "''") + "') is null,0,(select iddepartamento from " + Tabla + " where codigo='" + Replace(pCodigo, "'", "''") + "'))"
            Case TiposDepartamentos.Nivel2
                Comm.CommandText = " select if((select iddepartamento from " + Tabla + " where iddepartamentos=" + pIdNivelSuperior.ToString + " and codigo='" + Replace(pCodigo, "'", "''") + "') is null,0,(select iddepartamento from " + Tabla + " where iddepartamentos=" + pIdNivelSuperior.ToString + " and codigo='" + Replace(pCodigo, "'", "''") + "'))"
        End Select
        Encontro = Comm.ExecuteScalar
        If Encontro = 0 Then
            BuscaDepartamento = False
        Else
            ID = Encontro
            BuscaDepartamento = True
            LlenaDatos()
        End If
    End Function
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from " + Tabla + " where iddepartamento=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Codigo = DReader("codigo")
            If TipoC = TiposDepartamentos.Nivel2 Then
                IdNivelSuperior = DReader("iddepartamentos")
            End If
        End If
        DReader.Close()
    End Sub
End Class
