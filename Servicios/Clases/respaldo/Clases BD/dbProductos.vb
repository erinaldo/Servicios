Public Class dbProductos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Nombre As String
    Public Clasificacion As dbProductosClasificaciones
    Public Clave As String
    Public Cantidad As Double
    Public TipoCantidad As dbTiposCantidades
    Public Contenido As Double
    Public TipoContenido As dbTiposCantidades
    Public Inventariable As Byte
    Public Costo As Double
    Public Iva As Double
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Nombre = ""
        Clave = ""
        Cantidad = 0
        Contenido = 0
        Inventariable = 0
        Costo = 0
        Iva = 0
        Comm.Connection = Conexion
        Clasificacion = New dbProductosClasificaciones(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub Guardar(ByVal pNombre As String, ByVal pIdClasificacion As Integer, ByVal pClave As String, ByVal pCantidad As Double, ByVal pidtipoCantidad As Integer, ByVal pContenido As Double, ByVal pIdTipoContenido As Integer, ByVal pInventariable As Byte, ByVal pIva As Double)
        Nombre = pNombre
        Clave = pClave
        Contenido = pContenido
        Cantidad = pCantidad
        Inventariable = pInventariable
        Iva = pIva
        Comm.CommandText = "insert into tblproductos(nombre,idclasificacion,clave,cantidad,tipocantidad,contenido,tipocontenido,inventariable,iva,costo) values('" + Replace(Nombre, "'", "''") + "'," + pIdClasificacion.ToString + ",'" + Replace(Clave, "'", "''") + "'," + Cantidad.ToString + "," + pidtipoCantidad.ToString + "," + Contenido.ToString + "," + pIdTipoContenido.ToString + "," + Inventariable.ToString + "," + Iva.ToString + ",0)"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idproducto) from tblproductos"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pNombre As String, ByVal pIdClasificacion As Integer, ByVal pClave As String, ByVal pCantidad As Double, ByVal pidtipoCantidad As Integer, ByVal pContenido As Double, ByVal pIdTipoContenido As Integer, ByVal pInventariable As Byte, ByVal piva As Double)
        ID = pID
        Nombre = pNombre
        Clave = pClave
        Contenido = pContenido
        Inventariable = pInventariable
        Iva = piva
        Comm.CommandText = "update tblproductos set nombre='" + Replace(Nombre, "'", "''") + "',idclasificacion=" + pIdClasificacion.ToString + ",clave='" + Replace(Clave, "'", "''") + "',tipocantidad=" + pidtipoCantidad.ToString + ",contenido=" + Contenido.ToString + ",tipocontenido=" + pIdTipoContenido.ToString + ",inventariable=" + Inventariable.ToString + ",iva=" + Iva.ToString + " where idproducto=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblproductos where idproducto=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(Optional ByVal pNombre As String = "", Optional ByVal pidaIgnorar As Integer = 0, Optional ByVal pIdClasificacion As Integer = 0) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblproductos.idproducto,tblproductos.clave,tblproductos.nombre,tblproductosclasificaciones.nombre as nomclas from tblproductos inner join tblproductosclasificaciones on tblproductos.idclasificacion=tblproductosclasificaciones.idclasificacion where tblproductos.idproducto>1 and concat(tblproductos.clave,tblproductos.nombre) like '%" + Replace(pNombre, "'", "''") + "%'"
        If pIdClasificacion > 0 Then
            Comm.CommandText += " and tblproductos.idclasificacion=" + pIdClasificacion.ToString
        End If
        If pidaIgnorar > 0 Then
            Comm.CommandText += " and tblproductos.idproducto<>" + pidaIgnorar.ToString
        End If
        Comm.CommandText += " order by tblproductos.clave,tblproductos.nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproductos")
        Return DS.Tables("tblproductos").DefaultView
    End Function
    Public Function ConsultaI(Optional ByVal pNombre As String = "", Optional ByVal pidaIgnorar As Integer = 0, Optional ByVal pIdClasificacion As Integer = 0) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblproductos.idproducto,tblproductos.clave,tblproductos.nombre,tblproductos.cantidad from tblproductos inner join tblproductosclasificaciones on tblproductos.idclasificacion=tblproductosclasificaciones.idclasificacion where tblproductos.idproducto>1 and concat(tblproductos.clave,tblproductos.nombre) like '%" + Replace(pNombre, "'", "''") + "%' and tblproductos.inventariable=1"
        If pIdClasificacion > 0 Then
            Comm.CommandText += " and tblproductos.idclasificacion=" + pIdClasificacion.ToString
        End If
        If pidaIgnorar > 0 Then
            Comm.CommandText += " and tblproductos.idproducto<>" + pidaIgnorar.ToString
        End If
        Comm.CommandText += " order by tblproductos.clave,tblproductos.nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproductos")
        Return DS.Tables("tblproductos").DefaultView
    End Function
    Public Function ChecaClaveRepetida(ByVal pClave As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(clave) from tblproductos where clave='" + Replace(pClave, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            Comm.CommandText = "select count(clave) from tblinventario where clave='" + Replace(pClave, "'", "''") + "'"
            Resultado = Comm.ExecuteScalar
        End If
        If Resultado = 0 Then
            ChecaClaveRepetida = False
        Else
            ChecaClaveRepetida = True
        End If
    End Function
    Public Sub ActualizaInventario(ByVal pIdProducto As Integer, ByVal pCantidad As Double)
        Comm.CommandText = "update tblproductos set cantidad=" + pCantidad.ToString + " where idproducto=" + pIdProducto.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub GuardaAHistorial(ByVal Fecha As String)
        Comm.CommandText = "delete from tblhproductos where fecha='" + Fecha + "'"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblhproductos select '" + Fecha + "',idproducto,cantidad from tblproductos"
        Comm.ExecuteNonQuery()
    End Sub
    Private Sub LlenaDatos()
        Dim IdClas As Integer
        Dim IdtipoCant As Integer
        Dim IdTipoCont As Integer
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblproductos where idproducto=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Nombre = DReader("nombre")
            IdClas = DReader("idclasificacion")
            Clave = DReader("clave")
            Cantidad = DReader("cantidad")
            Contenido = DReader("contenido")
            IdtipoCant = DReader("tipocantidad")
            IdTipoCont = DReader("tipocontenido")
            Inventariable = DReader("inventariable")
            Iva = DReader("iva")
            Costo = DReader("costo")
        End If
        DReader.Close()
        Clasificacion = New dbProductosClasificaciones(IdClas, Comm.Connection)
        TipoCantidad = New dbTiposCantidades(IdtipoCant, Comm.Connection)
        TipoContenido = New dbTiposCantidades(IdTipoCont, Comm.Connection)
    End Sub
    Public Function BuscaProducto(ByVal pClave As String) As Boolean
        Dim Encontro As Integer
        Comm.CommandText = " select if((select idproducto from tblproductos where idproducto>1 and clave='" + Replace(pClave, "'", "''") + "') is null,0,(select idproducto from tblproductos where idproducto>1 and clave='" + Replace(pClave, "'", "''") + "'))"
        Encontro = Comm.ExecuteScalar
        If Encontro = 0 Then
            BuscaProducto = False
        Else
            ID = Encontro
            BuscaProducto = True
            LlenaDatos()
        End If
    End Function 
    Private Sub VerificaTablaAlmacen(ByVal pidProducto As Integer, ByVal pidAlmacen As Integer)
        Dim Hay As String
        Comm.CommandText = "select ifnull((select 'si' from tblalmacenesp where idproducto=" + pidProducto.ToString + " and idalmacen=" + pidAlmacen.ToString + "),'no')"
        Hay = Comm.ExecuteScalar
        If Hay = "no" Then
            Comm.CommandText = "insert into tblalmacenesp(idalmacen,idproducto,cantidad) values(" + pidAlmacen.ToString + "," + pidProducto.ToString + ",0)"
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Function DaIdProducto(ByVal pIdVariante) As Integer
        Comm.CommandText = "select ifnull((select idproducto from tblproductosvariantes where idvariante=" + pIdVariante.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaInventario(ByVal pIdAlmacen As Integer, ByVal pIdProducto As Integer) As Double
        Comm.CommandText = "select ifnull((select cantidad from tblalmacenesp where idproducto=" + pIdProducto.ToString + " and idalmacen=" + pIdAlmacen.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaInventarioTodos(ByVal pIdProducto As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(cantidad) from tblalmacenesp where idproducto=" + pIdProducto.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function reporte(ByVal pIdClasificacion As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select p.idproducto,p.clave,p.nombre,c.nombre as nomclas from tblproductos p inner join tblproductosclasificaciones c on p.idclasificacion=c.idclasificacion where true"
        If pIdClasificacion > 0 Then
            Comm.CommandText += " and p.idclasificacion=" + pIdClasificacion.ToString
        End If
        Comm.CommandText += " order by p.clave,p.nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproductos")
        'DS.WriteXmlSchema("tblproductos.xml")
        Return DS.Tables("tblproductos").DefaultView
    End Function
End Class
