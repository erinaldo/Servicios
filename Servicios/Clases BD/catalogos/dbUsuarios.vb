Public Class dbUsuarios
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public NombreUsuario As String
    Dim Password() As Byte
    Public PasswordS As String
    Public Tipo As Integer
    Public Nombre As String
    Public PermisosCatalogos As ULong
    Public PermisosCatalogos2 As ULong
    Public PermisosVentas As ULong
    Public PermisosCompras As ULong
    Public PermisosInventario As ULong
    Public PermisosHerramientas As ULong
    Public PermisosPuntodeVenta As ULong
    Public PermisosBancos As ULong
    Public PermisosServicios As ULong
    Public PermisosNomina As ULong
    Public PermisosGastos As ULong
    Public PermisosEmpenios As ULong
    Public PermisosFertilizantes As ULong
    Public PermisosContabilidad As ULong
    Public PermisosSemillas As ULong
    Public IdVendedor As Integer
    Private intentos As New dbIntentosLogin(MySqlcon)
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        NombreUsuario = ""
        Tipo = 0
        Nombre = ""
        PermisosCatalogos = 0
        PermisosCatalogos2 = 0
        PermisosVentas = 0
        PermisosCompras = 0
        PermisosInventario = 0
        PermisosHerramientas = 0
        PermisosPuntodeVenta = 0
        PermisosServicios = 0
        PermisosNomina = 0
        PermisosGastos = 0
        PermisosEmpenios = 0
        PermisosFertilizantes = 0
        PermisosContabilidad = 0
        PermisosSemillas = 0
        IdVendedor = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)

        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblusuarios where idusuario=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            NombreUsuario = DReader("nombreusuario")
            Password = DReader("password")
            Tipo = DReader("tipo")
            Nombre = DReader("nombre")
            PermisosCatalogos = DReader("permisos")
            PermisosCatalogos2 = DReader("permisos2")
            PermisosVentas = DReader("permisos3")
            PermisosCompras = DReader("permisos4")
            PermisosInventario = DReader("permisos5")
            PermisosHerramientas = DReader("permisos6")
            PermisosPuntodeVenta = DReader("permisos7")
            PermisosBancos = DReader("permisos8")
            IdVendedor = DReader("idvendedor")
            PermisosServicios = DReader("permisos9")
            PermisosNomina = DReader("permisos10")
            PermisosGastos = DReader("permisos11")
            PermisosEmpenios = DReader("permisos12")
            PermisosFertilizantes = DReader("permisos13")
            PermisosContabilidad = DReader("permisos14")
            PermisosSemillas = DReader("permisos15")
        End If
        DReader.Close()
        Dim E As New Encriptador
        PasswordS = E.Desencriptar3DES(Password)
    End Sub
    Public Sub Guardar(ByVal pNombreUsuario As String, ByVal pTipo As Integer, ByVal pNombre As String, ByVal pPassword As String, ByVal ppermisoscatalogos1 As UInt64, ByVal pPermisosCatalogos12 As UInt64, ByVal pPermisosVentas As UInt64, ByVal pPermisosCompras As UInt64, ByVal pPermisosInventario As UInt64, ByVal pPermisosOtros As UInt64, ByVal pIdVendedor As Integer, ByVal pPermisosPV As ULong, ByVal pPermisosBancos As ULong, ByVal pPermisosServicios As ULong, ByVal pPermisosNomina As ULong, ByVal pPermisosGastos As ULong, ByVal pPermisosEmpenios As ULong, pPermisosFertilizantes As ULong, pPermisosContabilidad As ULong, pPermisosSemillas As ULong)

        Dim E As New Encriptador
        PermisosServicios = pPermisosServicios
        NombreUsuario = pNombreUsuario
        Tipo = pTipo
        Nombre = pNombre.ToLower
        PermisosCatalogos = ppermisoscatalogos1
        PermisosCatalogos2 = pPermisosCatalogos12
        PermisosVentas = pPermisosVentas
        PermisosCompras = pPermisosCompras
        PermisosInventario = pPermisosInventario
        PermisosHerramientas = pPermisosOtros
        PermisosPuntodeVenta = pPermisosPV
        PermisosBancos = pPermisosBancos
        PermisosNomina = pPermisosNomina
        PermisosGastos = pPermisosGastos
        PermisosEmpenios = pPermisosEmpenios
        PermisosFertilizantes = pPermisosFertilizantes
        PermisosContabilidad = pPermisosContabilidad
        PermisosSemillas = pPermisosSemillas
        Password = E.Encriptar3DES(pPassword)
        IdVendedor = pIdVendedor
        If IdVendedor < 0 Then IdVendedor = 0
        Dim P As New MySql.Data.MySqlClient.MySqlParameter
        P.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Blob
        P.ParameterName = "@pass"
        P.Value = Password
        Comm.Parameters.Clear()
        Comm.Parameters.Add(P)
        Comm.CommandText = "insert into tblusuarios(nombreusuario,tipo,nombre,permisos,password,permisos2,permisos3,permisos4,permisos5,permisos6,idvendedor,permisos7,permisos8,permisos9,permisos10,permisos11,permisos12,permisos13,permisos14,permisos15,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(NombreUsuario, "'", "''") + "'," + Tipo.ToString + ",'" + Replace(Nombre, "'", "''") + "'," + PermisosCatalogos.ToString + ",@pass," + PermisosCatalogos2.ToString + "," + PermisosVentas.ToString + "," + PermisosCompras.ToString + "," + PermisosInventario.ToString + "," + PermisosHerramientas.ToString + "," + IdVendedor.ToString + "," + PermisosPuntodeVenta.ToString + "," + PermisosBancos.ToString + "," + PermisosServicios.ToString() + "," + PermisosNomina.ToString + "," + PermisosGastos.ToString + "," + PermisosEmpenios.ToString + "," + PermisosFertilizantes.ToString + "," + PermisosContabilidad.ToString + "," + PermisosSemillas.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pId As Integer, ByVal pNombreUsuario As String, ByVal pTipo As Integer, ByVal pNombre As String, ByVal pPassword As String, ByVal ppermisoscatalogos1 As UInt64, ByVal pPermisosCatalogos12 As UInt64, ByVal pPermisosVentas As UInt64, ByVal pPermisosCompras As UInt64, ByVal pPermisosInventario As UInt64, ByVal pPermisosOtros As UInt64, ByVal pIdVendedor As Integer, ByVal pPermisosPV As ULong, ByVal pPermisosBancos As ULong, ByVal pPermisosServicios As ULong, ByVal pPermisosNomina As ULong, ByVal pPermisosGastos As ULong, ByVal pPermisosEmpenios As ULong, pPermisosFertilizantes As ULong, pPermisosContabilidad As ULong, pPermisosSemillas As ULong)
        ID = pId
        Dim E As New Encriptador
        NombreUsuario = pNombreUsuario.ToLower
        Tipo = pTipo
        Nombre = pNombre
        IdVendedor = pIdVendedor
        PermisosServicios = pPermisosServicios
        PermisosCatalogos = ppermisoscatalogos1
        PermisosCatalogos2 = pPermisosCatalogos12
        PermisosVentas = pPermisosVentas
        PermisosCompras = pPermisosCompras
        PermisosInventario = pPermisosInventario
        PermisosHerramientas = pPermisosOtros
        PermisosPuntodeVenta = pPermisosPV
        PermisosBancos = pPermisosBancos
        PermisosNomina = pPermisosNomina
        PermisosGastos = pPermisosGastos
        PermisosEmpenios = pPermisosEmpenios
        PermisosContabilidad = pPermisosContabilidad
        PermisosFertilizantes = pPermisosFertilizantes
        PermisosSemillas = pPermisosSemillas
        Password = E.Encriptar3DES(pPassword)
        Dim P As New MySql.Data.MySqlClient.MySqlParameter
        P.MySqlDbType = MySql.Data.MySqlClient.MySqlDbType.Blob
        P.ParameterName = "@pass"
        P.Value = Password
        If IdVendedor < 0 Then IdVendedor = 0
        Comm.Parameters.Clear()
        Comm.Parameters.Add(P)
        Comm.CommandText = "update tblusuarios set nombreusuario='" + Replace(NombreUsuario, "'", "''") + "',tipo=" + Tipo.ToString + ",nombre='" + Replace(Nombre, "'", "''") + "',permisos=" + PermisosCatalogos.ToString + ",permisos2=" + PermisosCatalogos2.ToString + ",permisos3=" + PermisosVentas.ToString + ",permisos4=" + PermisosCompras.ToString + ",permisos5=" + PermisosInventario.ToString + ",permisos6=" + PermisosHerramientas.ToString + ",password=@pass,idvendedor=" + IdVendedor.ToString + ",permisos7=" + PermisosPuntodeVenta.ToString + ",permisos8=" + PermisosBancos.ToString + ",permisos9=" + PermisosServicios.ToString + ",permisos10=" + PermisosNomina.ToString + ",permisos11=" + PermisosGastos.ToString + ",permisos12=" + PermisosEmpenios.ToString + ",permisos13=" + PermisosFertilizantes.ToString + ",permisos14=" + PermisosContabilidad.ToString + ",permisos15=" + PermisosSemillas.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idusuario=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblusuarios where idusuario=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idusuario,nombreusuario,nombre from tblusuarios where concat(nombreusuario,nombre) like '%" + Replace(pNombre, "'", "''") + "%' order by nombreusuario"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblusuarios")
        Return DS.Tables("tblusuarios").DefaultView
    End Function
    Public Function ConsultaCombo(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select -1 as idusuario,'Todos' as nombreusuario, 'Todos' as nombre union all (select idusuario,nombreusuario,nombre from tblusuarios where concat(nombreusuario,nombre) like '%" + Replace(pNombre, "'", "''") + "%' order by nombreusuario)"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblusuarios")
        Return DS.Tables("tblusuarios").DefaultView
    End Function
    Public Function ChecaNombreUsuarioRepetido(ByVal pNUsuario As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(idusuario) from tblusuarios where nombreusuario='" + Replace(pNUsuario.ToLower, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaNombreUsuarioRepetido = False
        Else
            ChecaNombreUsuarioRepetido = True
        End If
    End Function
    Public Function ChecaLogin(ByVal pNUsuario As String, ByVal pPassword As String) As Boolean
        Comm.CommandText = "select ifnull((select idusuario from tblusuarios where nombreusuario='" + Replace(pNUsuario.ToLower, "'", "''") + "'),0)"
        ID = Comm.ExecuteScalar
        If ID <> 0 Then
            LlenaDatos()
            If PasswordS = pPassword Then
                intentos.agregar(ID, "SI", 1, 0)
                Dim fecha As Date
                fecha = Today
                'fecha.AddDays(-10)
                Dim f As Date = DateAdd(DateInterval.Day, -10, fecha)
                intentos.borraViejos(ID, f.ToString("yyyy/MM/dd"))
                ChecaLogin = True
            Else
                intentos.agregar(ID, "NO", 0, 1)
                ChecaLogin = False
            End If
        Else
            ChecaLogin = False
        End If
    End Function
    Public Function CuentaUsuarios() As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(idusuario) from tblusuarios"
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function
    Public Sub GuardaPerfil(pNombre As String, ByVal ppermisoscatalogos1 As UInt64, ByVal pPermisosCatalogos12 As UInt64, ByVal pPermisosVentas As UInt64, ByVal pPermisosCompras As UInt64, ByVal pPermisosInventario As UInt64, ByVal pPermisosOtros As UInt64, ByVal pPermisosPV As ULong, ByVal pPermisosBancos As ULong, ByVal pPermisosServicios As ULong, ByVal pPermisosNomina As ULong, ByVal pPermisosGastos As ULong, ByVal pPermisosEmpenios As ULong, pPermisosFertilizantes As ULong, pPermisosContabilidad As ULong, pPermisosSemillas As ULong)
        Comm.CommandText = "insert into tblperfilespermisos(nombre,permisos,permisos2,permisos3,permisos4,permisos5,permisos6,permisos7,permisos8,permisos9,permisos10,permisos11,permisos12,permisos13,permisos14,permisos15) values('" + Replace(pNombre.Trim, "'", "''") + "'," + _
        ppermisoscatalogos1.ToString + "," + pPermisosCatalogos12.ToString + "," + pPermisosVentas.ToString + "," + pPermisosCompras.ToString + "," + pPermisosInventario.ToString + "," + pPermisosOtros.ToString + "," + pPermisosPV.ToString + "," + pPermisosBancos.ToString + "," + pPermisosServicios.ToString + "," + pPermisosNomina.ToString + "," + pPermisosGastos.ToString + "," + pPermisosEmpenios.ToString + "," + pPermisosFertilizantes.ToString + "," + pPermisosContabilidad.ToString + "," + pPermisosSemillas.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminaPerfil(pIdPerfil As Integer)
        Comm.CommandText = "delete from tblperfilespermisos where idperfil=" + pIdPerfil.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ChecaPerfilRepetido(pNombre As String) As Boolean
        Comm.CommandText = "select count(nombre) from tblperfilespermisos where nombre='" + Replace(pNombre.Trim, "'", "''") + "'"
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Sub LlenaPerfil(pIdperfil As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblperfilespermisos where idperfil=" + pIdperfil.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            PermisosCatalogos = DReader("permisos")
            PermisosCatalogos2 = DReader("permisos2")
            PermisosVentas = DReader("permisos3")
            PermisosCompras = DReader("permisos4")
            PermisosInventario = DReader("permisos5")
            PermisosHerramientas = DReader("permisos6")
            PermisosPuntodeVenta = DReader("permisos7")
            PermisosBancos = DReader("permisos8")
            PermisosServicios = DReader("permisos9")
            PermisosNomina = DReader("permisos10")
            PermisosGastos = DReader("permisos11")
            PermisosEmpenios = DReader("permisos12")
            PermisosFertilizantes = DReader("permisos13")
            PermisosContabilidad = DReader("permisos14")
            PermisosSemillas = DReader("permisos15")
        End If
        DReader.Close()
    End Sub
End Class
