Public Class dbInventarioClasificaciones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Codigo As String
    Dim Tabla As String
    Dim IdNivelSuperior As Integer
    Dim TipoC As TiposClasificacion
    Public Enum TiposClasificacion
        Nivel1 = 1
        Nivel2 = 2
        Nivel3 = 3
        Nivel4 = 4
        Nivel5 = 5
    End Enum
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection, ByVal Tipo As TiposClasificacion)
        ID = -1
        Nombre = ""
        TipoC = Tipo
        IdNivelSuperior = 1
        Select Case Tipo
            Case TiposClasificacion.Nivel1
                Tabla = "tblinventarioclasificaciones"
            Case TiposClasificacion.Nivel2
                Tabla = "tblinventarioclasificaciones2"
            Case TiposClasificacion.Nivel3
                Tabla = "tblinventarioclasificaciones3"
            Case TiposClasificacion.Nivel4
                Tabla = "tblinventarioclasificaciones4"
            Case TiposClasificacion.Nivel5
                Tabla = "tblinventarioclasificaciones5"
        End Select
        Codigo = ""
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection, ByVal Tipo As TiposClasificacion)
        ID = pID
        Comm.Connection = Conexion
        Select Case Tipo
            Case TiposClasificacion.Nivel1
                Tabla = "tblinventarioclasificaciones"
            Case TiposClasificacion.Nivel2
                Tabla = "tblinventarioclasificaciones2"
            Case TiposClasificacion.Nivel3
                Tabla = "tblinventarioclasificaciones3"
            Case TiposClasificacion.Nivel4
                Tabla = "tblinventarioclasificaciones4"
            Case TiposClasificacion.Nivel5
                Tabla = "tblinventarioclasificaciones5"
        End Select
        TipoC = Tipo
        LlenaDatos()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pCodigo As String, Optional ByVal pIdNivelSuperior As Integer = 1)
        Nombre = pNombre
        Codigo = pCodigo
        IdNivelSuperior = pIdNivelSuperior
        If TipoC = TiposClasificacion.Nivel1 Then
            Comm.CommandText = "insert into " + Tabla + "(nombre,codigo,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Codigo, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Else
            Comm.CommandText = "insert into " + Tabla + "(nombre,codigo,idnivelsuperior,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Codigo, "'", "''") + "'," + IdNivelSuperior.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idclasificacion) from " + Tabla
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pCodido As String)
        ID = pID
        Nombre = pNombre
        Codigo = pCodido
        Comm.CommandText = "update " + Tabla + " set nombre='" + Replace(Nombre, "'", "''") + "',codigo='" + Replace(Codigo, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idclasificacion=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from " + Tabla + " where idclasificacion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idclasificacion,nombre from " + Tabla + " where idclasificacion>1 nombre like '%" + Replace(pNombre, "'", "''") + "%'"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, Tabla)
        Return DS.Tables(Tabla).DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdNivelSuperior As Integer, ByVal pTodo As Boolean) As MySql.Data.MySqlClient.MySqlDataReader

        If pIdNivelSuperior = 0 Or pTodo Then
            Comm.CommandText = "select idclasificacion,nombre from " + Tabla + " where idclasificacion>1 order by codigo"
        Else
            Comm.CommandText = "select idclasificacion,nombre from " + Tabla + " idclasificacion>1 and idnivelsuperior=" + pIdNivelSuperior.ToString + " order by codigo"
        End If
        Return Comm.ExecuteReader
    End Function
    Public Function ChecaCodigoRepetido(ByVal pCodigo As String, Optional ByVal pIDnivelSuperior As Integer = 1) As Boolean
        Dim Resultado As Integer = 0
        Select Case TipoC
            Case TiposClasificacion.Nivel1
                Comm.CommandText = "select count(codigo) from " + Tabla + " where codigo='" + Replace(pCodigo, "'", "''") + "'"
            Case TiposClasificacion.Nivel2
                Comm.CommandText = "select count(codigo) from " + Tabla + " where codigo='" + Replace(pCodigo, "'", "''") + "' and idnivelsuperior=" + pIDnivelSuperior.ToString
            Case TiposClasificacion.Nivel3
                Comm.CommandText = "select count(codigo) from " + Tabla + " where codigo='" + Replace(pCodigo, "'", "''") + "' and idnivelsuperior=" + pIDnivelSuperior.ToString
            Case TiposClasificacion.Nivel4
                Comm.CommandText = "select count(codigo) from " + Tabla + " where codigo='" + Replace(pCodigo, "'", "''") + "' and idnivelsuperior=" + pIDnivelSuperior.ToString
            Case TiposClasificacion.Nivel5
                Comm.CommandText = "select count(codigo) from " + Tabla + " where codigo='" + Replace(pCodigo, "'", "''") + "' and idnivelsuperior=" + pIDnivelSuperior.ToString
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
            Case TiposClasificacion.Nivel1
                Comm.CommandText = "select count(nombre) from " + Tabla + " where nombre='" + Replace(pNombre, "'", "''") + "'"
            Case TiposClasificacion.Nivel2
                Comm.CommandText = "select count(nombre) from " + Tabla + " where nombre='" + Replace(pNombre, "'", "''") + "' and idnivelsuperior=" + pIDnivelSuperior.ToString
            Case TiposClasificacion.Nivel3
                Comm.CommandText = "select count(nombre) from " + Tabla + " where nombre='" + Replace(pNombre, "'", "''") + "' and idnivelsuperior=" + pIDnivelSuperior.ToString
            Case TiposClasificacion.Nivel4
                Comm.CommandText = "select count(nombre) from " + Tabla + " where nombre='" + Replace(pNombre, "'", "''") + "' and idnivelsuperior=" + pIDnivelSuperior.ToString
            Case TiposClasificacion.Nivel5
                Comm.CommandText = "select count(nombre) from " + Tabla + " where nombre='" + Replace(pNombre, "'", "''") + "' and idnivelsuperior=" + pIDnivelSuperior.ToString
        End Select
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaNombreRepetido = False
        Else
            ChecaNombreRepetido = True
        End If
    End Function
    Public Function BuscaClasificacion(ByVal pCodigo As String, Optional ByVal pIdNivelSuperior As Integer = 1) As Boolean
        Dim Encontro As Integer
        Select Case TipoC
            Case TiposClasificacion.Nivel1
                Comm.CommandText = " select if((select idclasificacion from " + Tabla + " where idclasificacion>1 and codigo='" + Replace(pCodigo, "'", "''") + "') is null,0,(select idclasificacion from " + Tabla + " where idclasificacion>1 and codigo='" + Replace(pCodigo, "'", "''") + "'))"
            Case TiposClasificacion.Nivel2
                Comm.CommandText = " select if((select idclasificacion from " + Tabla + " where idclasificacion>1 and idnivelsuperior=" + pIdNivelSuperior.ToString + " and codigo='" + Replace(pCodigo, "'", "''") + "') is null,0,(select idclasificacion from " + Tabla + " where idclasificacion>1 and idnivelsuperior=" + pIdNivelSuperior.ToString + " and codigo='" + Replace(pCodigo, "'", "''") + "'))"
            Case TiposClasificacion.Nivel3
                Comm.CommandText = " select if((select idclasificacion from " + Tabla + " where idclasificacion>1 and idnivelsuperior=" + pIdNivelSuperior.ToString + " and codigo='" + Replace(pCodigo, "'", "''") + "') is null,0,(select idclasificacion from " + Tabla + " where idclasificacion>1 and idnivelsuperior=" + pIdNivelSuperior.ToString + " and codigo='" + Replace(pCodigo, "'", "''") + "'))"
            Case TiposClasificacion.Nivel4
                Comm.CommandText = " select if((select idclasificacion from " + Tabla + " where idclasificacion>1 and idnivelsuperior=" + pIdNivelSuperior.ToString + " and codigo='" + Replace(pCodigo, "'", "''") + "') is null,0,(select idclasificacion from " + Tabla + " where idclasificacion>1 and idnivelsuperior=" + pIdNivelSuperior.ToString + " and codigo='" + Replace(pCodigo, "'", "''") + "'))"
            Case TiposClasificacion.Nivel5
                Comm.CommandText = " select if((select idclasificacion from " + Tabla + " where idclasificacion>1 and idnivelsuperior=" + pIdNivelSuperior.ToString + " and codigo='" + Replace(pCodigo, "'", "''") + "') is null,0,(select idclasificacion from " + Tabla + " where idclasificacion>1 and idnivelsuperior=" + pIdNivelSuperior.ToString + " and codigo='" + Replace(pCodigo, "'", "''") + "'))"
        End Select
        Encontro = Comm.ExecuteScalar
        If Encontro = 0 Then
            BuscaClasificacion = False
        Else
            ID = Encontro
            BuscaClasificacion = True
            LlenaDatos()
        End If
    End Function
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from " + Tabla + " where idclasificacion=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Codigo = DReader("codigo")
            If TipoC = TiposClasificacion.Nivel2 Or TipoC = TiposClasificacion.Nivel3 Then
                IdNivelSuperior = DReader("idnivelsuperior")
            End If
        End If
        DReader.Close()
    End Sub
    Public Function Reporte(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select c1.idclasificacion id1,c1.nombre nombre1,c2.idclasificacion id1,c2.nombre nombre2,c3.idclasificacion id1,c3.nombre nombre3 from tblinventarioclasificaciones c1 left outer join tblinventarioclasificaciones2 c2 on c1.idclasificacion=c2.idnivelsuperior left outer join tblinventarioclasificaciones3 c3 on c2.idclasificacion=c3.idnivelsuperior where c1.idclasificacion>1"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "clasificaciones")
        'DS.WriteXmlSchema("tblinventarioclasificaciones.xml")
        Return DS.Tables("clasificaciones").DefaultView
    End Function
    Public Function DaSiguienteCodigo(Optional ByVal pIDnivelSuperior As Integer = 1) As String
        Dim Resultado As Integer = 0
        Select Case TipoC
            Case TiposClasificacion.Nivel1
                Comm.CommandText = "select count(codigo) from " + Tabla + " where idclasificacion>1"
            Case TiposClasificacion.Nivel2
                Comm.CommandText = "select count(codigo) from " + Tabla + " where idnivelsuperior=" + pIDnivelSuperior.ToString + " and idclasificacion>1"
            Case TiposClasificacion.Nivel3
                Comm.CommandText = "select count(codigo) from " + Tabla + " where idnivelsuperior=" + pIDnivelSuperior.ToString + " and idclasificacion>1"
            Case TiposClasificacion.Nivel4
                Comm.CommandText = "select count(codigo) from " + Tabla + " where idnivelsuperior=" + pIDnivelSuperior.ToString + " and idclasificacion>1"
            Case TiposClasificacion.Nivel5
                Comm.CommandText = "select count(codigo) from " + Tabla + " where idnivelsuperior=" + pIDnivelSuperior.ToString + " and idclasificacion>1"
        End Select
        Resultado = Comm.ExecuteScalar + 1
        Return Format(Resultado, "000")
    End Function
    Public Function DaColeccion(ByVal pTipo As TiposClasificacion, ByVal pIdnivelsuperior As Integer) As Collection
        Dim Clases As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Select Case pTipo
            Case TiposClasificacion.Nivel1
                Comm.CommandText = "select nombre,idclasificacion from tblinventarioclasificaciones where idclasificacion>1 order by codigo"
            Case TiposClasificacion.Nivel2
                Comm.CommandText = "select nombre,idclasificacion from tblinventarioclasificaciones2 where idnivelsuperior=" + pIdnivelsuperior.ToString + " and idclasificacion>1 order by codigo"
            Case TiposClasificacion.Nivel3
                Comm.CommandText = "select nombre,idclasificacion from tblinventarioclasificaciones3 where idnivelsuperior=" + pIdnivelsuperior.ToString + " and idclasificacion>1 order by codigo"
            Case TiposClasificacion.Nivel4
                Comm.CommandText = "select nombre,idclasificacion from tblinventarioclasificaciones4 where idnivelsuperior=" + pIdnivelsuperior.ToString + " and idclasificacion>1 order by codigo"
            Case TiposClasificacion.Nivel5
                Comm.CommandText = "select nombre,idclasificacion from tblinventarioclasificaciones5 where idnivelsuperior=" + pIdnivelsuperior.ToString + " and idclasificacion>1 order by codigo"
        End Select
        DR = Comm.ExecuteReader
        While DR.Read
            Clases.Add(New ClasificacionBase(DR("nombre"), DR("idclasificacion")))
        End While
        DR.Close()
        Return Clases
    End Function
    Public Function listaClasificaciones() As List(Of String)
        Comm.CommandText = "select if((select count(*) from tblinventario where idclasificacion=" + Tabla + ".idclasificacion and restaurante=1)>0," + Tabla + ".codigo,'') as codigo from " + Tabla + ";"
        Dim lista As New List(Of String)
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader = Comm.ExecuteReader
        While dr.Read()
            If dr("codigo") <> "" Then
                lista.Add(dr("codigo"))
            End If
        End While
        dr.Close()
        Return lista
    End Function
    Public Function totalClasificaciones() As Integer
        Return listaClasificaciones().Count
    End Function
End Class
