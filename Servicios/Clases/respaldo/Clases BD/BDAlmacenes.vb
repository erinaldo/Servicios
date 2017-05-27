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
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Direccion = ""
        Numero = ""
        IdSucursal = 0
        Tipo = 0
        Peso = 0
        Estado = 0
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
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pDireccion As String, ByVal pNumero As String, ByVal pIdSucursal As Integer, ByVal pPeso As Double, ByVal pTipo As Byte, ByVal pEstado As Byte)
        Nombre = pNombre
        Direccion = pDireccion
        Numero = pNumero
        IdSucursal = pIdSucursal
        Peso = pPeso
        Tipo = pTipo
        Estado = pEstado
        Comm.CommandText = "insert into tblalmacenes(nombre,direccion,numero,idsucursal,peso,tipo,estado,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values('" + Replace(Nombre, "'", "''") + "','" + Replace(Direccion, "'", "''") + "','" + Replace(Numero, "'", "''") + "'," + IdSucursal.ToString + "," + Peso.ToString + "," + Tipo.ToString + "," + Estado.ToString + "," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pDireccion As String, ByVal pNumero As String, ByVal pIdSucursal As Integer, ByVal pPeso As Double, ByVal ptipo As Byte)
        ID = pID
        Nombre = pNombre
        Direccion = pDireccion
        Numero = pNumero
        IdSucursal = pIdSucursal
        Tipo = ptipo
        Peso = pPeso
        Comm.CommandText = "update tblalmacenes set nombre='" + Replace(Nombre, "'", "''") + "',direccion='" + Replace(Direccion, "'", "''") + "',numero='" + Replace(Numero, "'", "''") + "',idsucursal=" + IdSucursal.ToString + ",peso=" + Peso.ToString + ",tipo=" + Tipo.ToString + ", idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idalmacen=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblalmacenes where idalmacen=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "") As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idalmacen,numero,nombre,case tipo when 0 then 'Estacionario' when 1 then 'Móvil' end as Tipo from tblalmacenes where concat(numero,nombre) like '%" + Replace(pNombre, "'", "''") + "%' and idalmacen<>1"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblalmacenes")
        Return DS.Tables("tblalmacenes").DefaultView
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
End Class
