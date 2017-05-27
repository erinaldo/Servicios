Imports MySql.Data.MySqlClient

Public Class dbAlmacenes
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Direccion As String
    Public Numero As String
    Public IdSucursal As Integer
    Public Tipo As Byte
    Public Peso As Double
    Public Estado As Byte

    Public idUsuarioAlta As Integer
    Public fechaAlta As String
    Public horaAlta As String
    Public idUsuarioCambio As Integer
    Public fechaCambio As String
    Public horaCambio As String
    Public idCuenta As Integer
    Public idCuenta2 As Integer
    Public idCuenta3 As Integer
    Public idCuenta4 As Integer
    Public IdsSinPermiso As New Collection
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Direccion = ""
        Numero = ""
        IdSucursal = 0
        Tipo = 0
        Peso = 0
        Estado = 0
        idCuenta = 0
        idCuenta2 = 0
        idCuenta3 = 0
        idCuenta4 = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblalmacenes where idalmacen=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            Direccion = DReader("direccion")
            Numero = DReader("numero")
            IdSucursal = DReader("idsucursal")
            Peso = DReader("peso")
            Tipo = DReader("tipo")
            Estado = DReader("estado")
            idCuenta = DReader("idcuenta")
            idCuenta2 = DReader("idcuenta2")
            idCuenta3 = DReader("idcuenta3")
            idCuenta4 = DReader("idcuenta4")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pDireccion As String, ByVal pNumero As String, ByVal pIdSucursal As Integer, ByVal pPeso As Double, ByVal pTipo As Byte, ByVal pEstado As Byte, pIdcuenta As Integer, pIdCuenta2 As Integer, pIdCuenta3 As Integer, pIdCuenta4 As Integer)
        Nombre = pNombre
        Direccion = pDireccion
        Numero = pNumero
        IdSucursal = pIdSucursal
        Peso = pPeso
        Tipo = pTipo
        Estado = pEstado
        Comm.CommandText = "insert into tblalmacenes(nombre,direccion,numero,idsucursal,peso,tipo,estado,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,idcuenta,idcuenta2,idcuenta3,idcuenta4) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Direccion, "'", "''") + "','" + Replace(Numero, "'", "''") + "'," + IdSucursal.ToString + "," + Peso.ToString + "," + Tipo.ToString + "," + Estado.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + pIdcuenta.ToString + "," + pIdCuenta2.ToString + "," + pIdCuenta3.ToString + "," + pIdCuenta4.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pDireccion As String, ByVal pNumero As String, ByVal pIdSucursal As Integer, ByVal pPeso As Double, ByVal ptipo As Byte, pIdcuenta As Integer, pIdCuenta2 As Integer, pIdCuenta3 As Integer, pIdCuenta4 As Integer)
        ID = pID
        Nombre = pNombre
        Direccion = pDireccion
        Numero = pNumero
        IdSucursal = pIdSucursal
        Tipo = ptipo
        Peso = pPeso
        Comm.CommandText = "update tblalmacenes set nombre='" + Replace(Nombre, "'", "''") + "',direccion='" + Replace(Direccion, "'", "''") + "',numero='" + Replace(Numero, "'", "''") + "',idsucursal=" + IdSucursal.ToString + ",peso=" + Peso.ToString + ",tipo=" + Tipo.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "',idcuenta=" + pIdcuenta.ToString + ",idcuenta2=" + pIdCuenta2.ToString + ",idcuenta3=" + pIdCuenta3.ToString + ",idcuenta4=" + pIdCuenta4.ToString + " where idalmacen=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblalmacenes where idalmacen=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select a.idalmacen, a.numero, a.nombre, case a.tipo when 0 then 'Estacionario' when 1 then 'Móvil' end as Tipo,count(au.idalmacen) ubicaciones from tblalmacenes a left outer join tblalmacenesubicaciones au on a.idalmacen=au.idalmacen where (a.numero like '%" + Replace(pNombre, "'", "''") + "%' or a.nombre like '%" + Replace(pNombre, "'", "''") + "%') and a.idalmacen<>1 group by a.idalmacen;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblalmacenes")
        Return DS.Tables("tblalmacenes").DefaultView
    End Function

    Public Function Ubicaciones(idalmacen As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblalmacenesubicaciones where idalmacen=" + idalmacen.ToString() + " order by ubicacion;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblalmacenes")
        Return DS.Tables("tblalmacenes")
    End Function

    Public Function ChecaNumeroRepetido(ByVal pNumero As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(numero) from tblalmacenes where numero='" + Replace(pNumero, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaNumeroRepetido = False
        Else
            ChecaNumeroRepetido = True
        End If
    End Function
    Public Function reporte() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblalmacenes.idalmacen,numero,tblalmacenes.nombre,peso,tipo,tblsucursales.nombre as nombres from tblalmacenes inner join tblsucursales on tblalmacenes.idsucursal=tblsucursales.idsucursal where tblalmacenes.idalmacen>1;"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblalmacenes")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblalmacenes").DefaultView
    End Function
    Public Function ReporteEstadoAlmacenes(pIdSucursal As Integer, pEstado As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idalmacen,numero,nombre,peso," + _
 "if(ifnull((select false from tblfertilizantesmovimientos where idalmacen=tblalmacenes.idalmacen and estado=3 and estadosurtido=0 and tipo<>2 limit 1),true),ifnull((select 'EN TRÁNSITO' from tblfertilizantesmovimientos where idalmacendestino=tblalmacenes.idalmacen and estado=3 and estadosurtido=0 and tipo=2 limit 1),'EN PLANTA'),'EN TRÁNSITO') as estado," + _
 "if(ifnull((select true from tblfertilizantesmovimientos where idalmacen=tblalmacenes.idalmacen and estado=3 and estadosurtido=0 and tipo<>2 limit 1),false),(select concat(tblfertilizantespedidos.serie,tblfertilizantespedidos.folio,' ',tblfertilizantesmovimientos.comentario) from tblfertilizantesmovimientos inner join tblfertilizantespedidos on tblfertilizantesmovimientos.idpedido=tblfertilizantespedidos.idpedido where idalmacen=tblalmacenes.idalmacen and tblfertilizantesmovimientos.estado=3 and estadosurtido=0 and tipo<>2 limit 1),ifnull((select concat(tblfertilizantespedidos.serie,tblfertilizantespedidos.folio,' ',tblfertilizantesmovimientos.comentario) from tblfertilizantesmovimientos inner join tblfertilizantespedidos on tblfertilizantesmovimientos.idpedido=tblfertilizantespedidos.idpedido where idalmacendestino=tblalmacenes.idalmacen and tblfertilizantesmovimientos.estado=3 and estadosurtido=0 and tipo=2 limit 1),'')) as pedido from tblalmacenes where idalmacen>1 "
            If pIdSucursal > 0 Then
            Comm.CommandText += " and idsucursal=" + pIdSucursal.ToString
            End If
            Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
            DA.Fill(DS, "tblalmacenesestado")
        ' DS.WriteXmlSchema("tblalmacenesestado.xml")
            Return DS.Tables("tblalmacenesestado").DefaultView
    End Function
    Public Sub AgregarUsuario(pIdUsuario As Integer, pIdAlmacen As Integer)
        Comm.CommandText = "insert into tblalmacenespermisos(idalmacen,idusuario) values(" + pIdAlmacen.ToString + "," + pIdUsuario.ToString + ")"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ChecaUsuario(pIdUsuario As Integer, pIdAlmacen As Integer) As Integer
        Comm.CommandText = "select ifnull((select idusuario from tblalmacenespermisos where idusuario=" + pIdUsuario.ToString + " and idalmacen=" + pIdAlmacen.ToString + " limit 1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function UsuariosAgregados(pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select ap.idusuario,i.nombreusuario,i.nombre from tblalmacenespermisos ap inner join tblusuarios i on ap.idusuario=i.idusuario where ap.idalmacen=" + pIdAlmacen.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblalmacenesp")
        'DS.WriteXmlSchema("tblalmacenes.xml")
        Return DS.Tables("tblalmacenesp").DefaultView
    End Function
    Public Sub QuitarUsuario(pIdUsuario As Integer, pIdAlmacen As Integer)
        Comm.CommandText = "delete from tblalmacenespermisos where idalmacen=" + pIdAlmacen.ToString + " and idusuario=" + pIdUsuario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub AlmacenesSinPermiso(pIdUsuario As Integer)
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select idalmacen from tblalmacenespermisos where idusuario=" + pIdUsuario.ToString
        DR = Comm.ExecuteReader
        IdsSinPermiso.Clear()
        While DR.Read
            IdsSinPermiso.Add(DR("idalmacen"))
        End While
        DR.Close()
    End Sub
    Public Function TienePermiso(pIdAlmacen As Integer) As Boolean
        For Each Idu As Integer In IdsSinPermiso
            If Idu = pIdAlmacen Then Return False
        Next
        Return True
    End Function

    Public Sub AgregarUbicacion(idalmacen As Integer, ubicacion As String)
        Comm.CommandText = "insert into tblalmacenesubicaciones (idalmacen,ubicacion) values (" + idalmacen.ToString() + ",'" + Trim(Replace(ubicacion, "'", "''")) + "');"
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub ModificarUbicacion(idubicacion As Integer, ubicacion As String)
        Comm.CommandText = "update tblalmacenesubicaciones set ubicacion='" + Trim(Replace(ubicacion, "'", "''")) + "' where id=" + idubicacion.ToString() + ";"
        Comm.ExecuteNonQuery()
    End Sub

    Public Function EliminarUbicacion(idubicacion As Integer) As Boolean
        Comm.CommandText = "select count(id) from tblalmacenesubicaciones au inner join tblalmacenesiubicaciones aiu on au.idalmacen=aiu.idalmacen and au.ubicacion=aiu.ubicacion where  id=" + idubicacion.ToString() + ";"
        If Comm.ExecuteScalar = 0 Then
            Comm.CommandText = "delete from tblalmacenesubicaciones where id=" + idubicacion.ToString() + ";"
            Comm.ExecuteNonQuery()
            Return True
        End If
        Return False
    End Function
End Class
