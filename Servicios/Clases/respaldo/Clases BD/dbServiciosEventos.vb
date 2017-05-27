Public Class dbServiciosEventos
    Public ID As Integer
    Public IdServicio As Integer
    Public IdClasificacion As Integer
    Public IdClasificacion2 As Integer
    Public Comentario As String
    Public Tiempo As Double
    Public Precio As Double
    Public IdTecnico As Integer
    Public nombreInvenario As String
    Public codigoInventario As String
    Public detalles As String
    Public horae As String
    Public fechae As String
    Public horas As String
    Public fechas As String
    Public estado As String
    Public fecha As String
    Public iva As Double
    '***

    Public pidEvento As String
    Public pidInventario As String
    Public pPrecio As String
    Public pCantidad As String
    Public pTotal As String
    Public pIdEquipo As String
    Public pFecha As String

    ' Public precio As Double
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdServicio = 0
        IdClasificacion = 0
        IdClasificacion2 = 0
        Comentario = ""
        Tiempo = 0
        Precio = 0
        IdTecnico = 0
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblservicioseventos where idevento=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            IdServicio = DReader("idservicio")
            IdClasificacion = DReader("idclasificacion")
            IdClasificacion2 = DReader("idclasificacion2")
            Comentario = DReader("comentario")
            Tiempo = DReader("tiempo")
            IdTecnico = DReader("idtecnico")
            fecha = DReader("fecha")
            iva = DReader("iva")
        End If
        DReader.Close()
    End Sub
    Public Sub llenaDatosSuc(ByVal pID As Integer)
        ID = pID
        'Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblservicioseventossuc where idevento=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            IdServicio = DReader("idservicio")
            IdClasificacion = DReader("idclasificacion")
            IdClasificacion2 = DReader("idclasificacion2")
            Comentario = DReader("comentario")
            Tiempo = DReader("tiempo")
            IdTecnico = DReader("idtecnico")
            fecha = DReader("fecha")
            iva = DReader("iva")
        End If
        DReader.Close()
    End Sub
    Public Sub Guardar(ByVal pIdServicio As Integer, ByVal pIdClasificacion As Integer, ByVal pIdClasificacion2 As Integer, ByVal pComentario As String, ByVal pTiempo As Double, ByVal pPrecio As Double, ByVal pidTecnico As Integer, ByVal pidEquipo As Integer, ByVal pFecha As String, ByVal pIva As Double)
        IdServicio = pIdServicio
        IdClasificacion = pIdClasificacion
        IdClasificacion2 = pIdClasificacion2
        If IdClasificacion2 < 0 Then IdClasificacion2 = 0
        Comentario = pComentario
        Tiempo = pTiempo
        Precio = pPrecio
        iva = pIva
        IdTecnico = pidTecnico
        fecha = pFecha
        Comm.CommandText = "insert into tblservicioseventos(idservicio,idclasificacion,idclasificacion2,comentario,tiempo,precio,idtecnico,idEquipo, fecha,iva) values(" + IdServicio.ToString + "," + IdClasificacion.ToString + "," + IdClasificacion2.ToString + ",'" + Replace(Comentario, "'", "''") + "'," + Tiempo.ToString + "," + Precio.ToString + "," + IdTecnico.ToString + "," + pidEquipo.ToString + ",'" + fecha + "'," + iva.ToString() + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select if(max(idevento) is null,0,max(idevento)) from tblservicioseventos"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Guardarsuc(ByVal pIdServicio As Integer, ByVal pIdClasificacion As Integer, ByVal pIdClasificacion2 As Integer, ByVal pComentario As String, ByVal pTiempo As Double, ByVal pPrecio As Double, ByVal pidTecnico As Integer, ByVal pidEquipo As Integer, ByVal pFecha As String, ByVal pIva As Double)
        IdServicio = pIdServicio
        IdClasificacion = pIdClasificacion
        IdClasificacion2 = pIdClasificacion2
        If IdClasificacion2 < 0 Then IdClasificacion2 = 0
        Comentario = pComentario
        Tiempo = pTiempo
        Precio = pPrecio
        iva = pIva
        IdTecnico = pidTecnico
        fecha = pFecha
        Comm.CommandText = "insert into tblservicioseventossuc(idservicio,idclasificacion,idclasificacion2,comentario,tiempo,precio,idtecnico,idEquipo, fecha,iva) values(" + IdServicio.ToString + "," + IdClasificacion.ToString + "," + IdClasificacion2.ToString + ",'" + Replace(Comentario, "'", "''") + "'," + Tiempo.ToString + "," + Precio.ToString + "," + IdTecnico.ToString + "," + pidEquipo.ToString + ",'" + fecha + "'," + iva.ToString() + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select if(max(idevento) is null,0,max(idevento)) from tblservicioseventossuc"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pIdClasificacion As Integer, ByVal pIdClasificacion2 As Integer, ByVal pComentario As String, ByVal pTiempo As Double, ByVal pPrecio As Double, ByVal pidTecnico As Integer, ByVal pFecha As String, ByVal pIVA As Double)
        ID = pID
        Precio = pPrecio
        IdClasificacion = pIdClasificacion
        IdClasificacion2 = pIdClasificacion2
        If IdClasificacion2 < 0 Then IdClasificacion2 = 0
        Comentario = pComentario
        Tiempo = pTiempo
        IdTecnico = pidTecnico
        fecha = pFecha
        iva = pIVA
        Comm.CommandText = "update tblservicioseventos set precio=" + Precio.ToString + ",idclasificacion=" + IdClasificacion.ToString + ",idclasificacion2=" + IdClasificacion2.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "',tiempo=" + Tiempo.ToString + ",idtecnico=" + IdTecnico.ToString + ",fecha='" + fecha + "'" + ",iva=" + iva.ToString + " where idevento=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Modificarsuc(ByVal pID As Integer, ByVal pIdClasificacion As Integer, ByVal pIdClasificacion2 As Integer, ByVal pComentario As String, ByVal pTiempo As Double, ByVal pPrecio As Double, ByVal pidTecnico As Integer, ByVal pFecha As String, ByVal pIVA As Double)
        ID = pID
        Precio = pPrecio
        IdClasificacion = pIdClasificacion
        IdClasificacion2 = pIdClasificacion2
        If IdClasificacion2 < 0 Then IdClasificacion2 = 0
        Comentario = pComentario
        Tiempo = pTiempo
        IdTecnico = pidTecnico
        fecha = pFecha
        iva = pIVA
        Comm.CommandText = "update tblservicioseventossuc set precio=" + Precio.ToString + ",idclasificacion=" + IdClasificacion.ToString + ",idclasificacion2=" + IdClasificacion2.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "',tiempo=" + Tiempo.ToString + ",idtecnico=" + IdTecnico.ToString + ",fecha='" + fecha + "'" + ",iva=" + iva.ToString + " where idevento=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblservicioseventos where idevento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarSuc(ByVal pID As Integer)
        Comm.CommandText = "delete from tblservicioseventossuc where idevento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdServicio As Integer, ByVal pIdEquipo As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblservicioseventos.idevento,tblserviciosclasificaciones.nombre,ifnull((select tblserviciosclasificaciones2.nombre from tblserviciosclasificaciones2 where tblserviciosclasificaciones2.idclasificacion=tblservicioseventos.idclasificacion2),'GENERAL') as `tblserviciosclasificaciones2.nombre`,tblservicioseventos.comentario,tblservicioseventos.precio,tblservicioseventos.tiempo,tblservicioseventos.fecha,tblservicioseventos.iva from tblservicioseventos inner join tblserviciosclasificaciones on tblservicioseventos.idclasificacion=tblserviciosclasificaciones.idclasificacion where tblservicioseventos.idservicio=" + pIdServicio.ToString + " and tblservicioseventos.idEquipo=" + pIdEquipo.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicioseventos")
        Return DS.Tables("tblservicioseventos").DefaultView
    End Function

    Public Function ConsultaSuc(ByVal pIdServicio As Integer, ByVal pIdEquipo As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblservicioseventossuc.idevento,tblserviciosclasificaciones.nombre,ifnull((select tblserviciosclasificaciones2.nombre from tblserviciosclasificaciones2 where tblserviciosclasificaciones2.idclasificacion=tblservicioseventossuc.idclasificacion2),'GENERAL') as `tblserviciosclasificaciones2.nombre`,tblservicioseventossuc.comentario,tblservicioseventossuc.precio,tblservicioseventossuc.tiempo,tblservicioseventossuc.fecha,tblservicioseventossuc.iva from tblservicioseventossuc inner join tblserviciosclasificaciones on tblservicioseventossuc.idclasificacion=tblserviciosclasificaciones.idclasificacion where tblservicioseventossuc.idservicio=" + pIdServicio.ToString + " and tblservicioseventossuc.idEquipo=" + pIdEquipo.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicioseventos")
        Return DS.Tables("tblservicioseventos").DefaultView
    End Function
    Public Function InventarioUtilizado(ByVal pIdEvento As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idserviciosinventario,tblinventario.clave,tblinventario.nombre,tblserviciosinventario2.cant,tblserviciosinventario2.precio from tblserviciosinventario2 inner join tblinventario on tblserviciosinventario2.idinventario=tblinventario.idinventario where idevento=" + pIdEvento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventarioeventos")
        Return DS.Tables("tblinventarioeventos").DefaultView
    End Function

    Public Function filtroTodosInventario() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idinventario,clave,nombre,costobase from tblinventario"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventario")
        Return DS.Tables("tblinventario").DefaultView
    End Function
    'AgregarArticulo
    'Public Function filtroInventario(ByVal palabra As String) As DataView
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select idinventario,clave,nombre,costobase from tblinventario where nombre like '%" + palabra.ToString() + "%' and idinventario<>1"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblinventario")
    '    Return DS.Tables("tblinventario").DefaultView
    'End Function
    Public Function filtroInventario(ByVal palabra As String) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblinventario.idinventario,tblinventario.clave,tblinventario.nombre,tblinventarioprecios.precio from tblinventario inner join tblinventarioprecios on tblinventario.idinventario=tblinventarioprecios.idinventario where  tblinventarioprecios.idlista=1 and tblinventario.nombre like '%" + palabra.ToString() + "%' and tblinventario.idinventario<>1"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblinventario")
        Return DS.Tables("tblinventario").DefaultView
    End Function
    Public Sub GuardarArticulo(ByVal pIdServicio As Integer, ByVal pIdInventario As Integer, ByVal pCantidad As Integer, ByVal pPrecio As Double, ByVal pTotal As Double, ByVal pidEquipo As Integer, ByVal pfecha1 As String)
        Comm.CommandText = "insert into tblserviciosinventario2(idevento,idinventario,precio,cantidad,total, idEquipo,fecha) values(" + pIdServicio.ToString + "," + pIdInventario.ToString + "," + pPrecio.ToString + "," + pCantidad.ToString + "," + pTotal.ToString + "," + pidEquipo.ToString + ", '" + pfecha1.ToString + "' )"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub GuardarArticulosuc(ByVal pIdServicio As Integer, ByVal pIdInventario As Integer, ByVal pCantidad As Integer, ByVal pPrecio As Double, ByVal pTotal As Double, ByVal pidEquipo As Integer, ByVal pfecha1 As String)
        Comm.CommandText = "insert into tblserviciosinventario2suc(idevento,idinventario,precio,cantidad,total, idEquipo,fecha) values(" + pIdServicio.ToString + "," + pIdInventario.ToString + "," + pPrecio.ToString + "," + pCantidad.ToString + "," + pTotal.ToString + "," + pidEquipo.ToString + ", '" + pfecha1.ToString + "' )"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function filtroArticulosConsumidos(ByVal idevento As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblserviciosinventario2 where idevento=" + idevento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosinventario2")
        Return DS.Tables("tblserviciosinventario2")
    End Function
    Public Function filtroArticulosConsumidossuc(ByVal idevento As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select * from tblserviciosinventario2suc where idevento=" + idevento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosinventario2")
        Return DS.Tables("tblserviciosinventario2")
    End Function
    Public Sub InventarioUtilizado2(ByVal pIdEvento As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select nombre,clave from tblinventario where idinventario=" + pIdEvento.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            nombreInvenario = DReader("nombre")
            codigoInventario = DReader("clave")
        End If
        DReader.Close()
    End Sub
    Public Sub consultaPrecio(ByVal pIdEvento As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select precio from tblserviciosinventario2 where idserviciosinventario=" + pIdEvento.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")

        End If
        DReader.Close()
    End Sub
    Public Sub consultaPreciosuc(ByVal pIdEvento As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select precio from tblserviciosinventario2suc where idserviciosinventario=" + pIdEvento.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")

        End If
        DReader.Close()
    End Sub
    Public Sub EliminarAnadido(ByVal pID As Integer)
        Comm.CommandText = "delete from tblserviciosinventario2 where idserviciosinventario=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarAnadidosuc(ByVal pID As Integer)
        Comm.CommandText = "delete from tblserviciosinventario2suc where idserviciosinventario=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub llenarDatosArticulos(ByVal pID As Integer)
        ID = pID

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblserviciosinventario2 where idserviciosinventario=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            pidEvento = DReader("idEvento")
            pidInventario = DReader("idInventario")
            pPrecio = DReader("Precio")
            pCantidad = DReader("Cantidad")
            pTotal = DReader("Total")
            pIdEquipo = DReader("idEquipo")
            pFecha = DReader("Fecha")

        End If
        DReader.Close()
    End Sub
    Public Sub llenarDatosArticulossuc(ByVal pID As Integer)
        ID = pID

        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblserviciosinventario2suc where idserviciosinventario=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            pidEvento = DReader("idEvento")
            pidInventario = DReader("idInventario")
            pPrecio = DReader("Precio")
            pCantidad = DReader("Cantidad")
            pTotal = DReader("Total")
            pIdEquipo = DReader("idEquipo")
            pFecha = DReader("Fecha")

        End If
        DReader.Close()
    End Sub
    Public Sub ModificarAnadido(ByVal pID As Integer, ByVal pIdServicio As Integer, ByVal pIdInventario As Integer, ByVal pCantidad As Integer, ByVal pPrecio As Double, ByVal pTotal As Double, ByVal pidEquipo As Integer, ByVal pfecha1 As String)

        '  Comm.CommandText = "update tblserviciosinventario2 set cantidad=" + pcantidad.ToString() + " where idserviciosinventario=" + ID.ToString + " ;"
        ' Comm.ExecuteNonQuery()
        Comm.CommandText = "delete from tblserviciosinventario2 where idserviciosinventario=" + pID.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblserviciosinventario2(idevento,idinventario,precio,cantidad,total, idEquipo,fecha) values(" + pIdServicio.ToString + "," + pIdInventario.ToString + "," + pPrecio.ToString + "," + pCantidad.ToString + "," + pTotal.ToString + "," + pidEquipo.ToString + ", '" + pfecha1.ToString + "' )"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificarAnadidosuc(ByVal pID As Integer, ByVal pIdServicio As Integer, ByVal pIdInventario As Integer, ByVal pCantidad As Integer, ByVal pPrecio As Double, ByVal pTotal As Double, ByVal pidEquipo As Integer, ByVal pfecha1 As String)

        '  Comm.CommandText = "update tblserviciosinventario2 set cantidad=" + pcantidad.ToString() + " where idserviciosinventario=" + ID.ToString + " ;"
        ' Comm.ExecuteNonQuery()
        Comm.CommandText = "delete from tblserviciosinventario2suc where idserviciosinventario=" + pID.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblserviciosinventario2suc(idevento,idinventario,precio,cantidad,total, idEquipo,fecha) values(" + pIdServicio.ToString + "," + pIdInventario.ToString + "," + pPrecio.ToString + "," + pCantidad.ToString + "," + pTotal.ToString + "," + pidEquipo.ToString + ", '" + pfecha1.ToString + "' )"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function buscarFecha(ByVal pID As Integer) As String
        Dim fechita As String
        Comm.CommandText = "select fecha from tblserviciosinventario2 where idserviciosinventario=" + pID.ToString
        fechita = Comm.ExecuteScalar
        Return fechita
    End Function


    Public Function filtroHistorialArticulos(ByVal pidEquipo As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select fecha,idevento,idinventario,cantidad,precio from tblserviciosinventario2 where idEquipo=" + pidEquipo.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosinventario2")
        Return DS.Tables("tblserviciosinventario2")
    End Function
    Public Function filtroHistorialArticulossuc(ByVal pidEquipo As Integer) As DataTable
        Dim DS As New DataSet
        Comm.CommandText = "select fecha,idevento,idinventario,cantidad,precio from tblserviciosinventario2suc where idEquipo=" + pidEquipo.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosinventario2")
        Return DS.Tables("tblserviciosinventario2")
    End Function

    Public Function ConsultaTodosServicios(ByVal pIdEquipo As Integer, ByVal pidServicio As Integer) As DataView
        Dim DS As New DataSet
        If pidServicio = 0 Then
            Comm.CommandText = "select tblservicioseventos.idevento,tblservicioseventos.idservicio,tblserviciosclasificaciones.nombre,tblserviciosclasificaciones2.nombre,tblservicioseventos.comentario,tblservicioseventos.precio,tblservicioseventos.tiempo from tblservicioseventos inner join tblserviciosclasificaciones on tblservicioseventos.idclasificacion=tblserviciosclasificaciones.idclasificacion inner join tblserviciosclasificaciones2 on tblservicioseventos.idclasificacion2=tblserviciosclasificaciones2.idclasificacion2 where  tblservicioseventos.idEquipo=" + pIdEquipo.ToString + " ORDER BY idevento ASC"
        Else
            Comm.CommandText = "select tblservicioseventos.idevento,tblservicioseventos.idservicio,tblserviciosclasificaciones.nombre,tblserviciosclasificaciones2.nombre,tblservicioseventos.comentario,tblservicioseventos.precio,tblservicioseventos.tiempo from tblservicioseventos inner join tblserviciosclasificaciones on tblservicioseventos.idclasificacion=tblserviciosclasificaciones.idclasificacion inner join tblserviciosclasificaciones2 on tblservicioseventos.idclasificacion2=tblserviciosclasificaciones2.idclasificacion2 where  tblservicioseventos.idservicio=" + pidServicio.ToString + " and tblservicioseventos.idEquipo=" + pIdEquipo.ToString + " ORDER BY idevento ASC"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicioseventos")
        Return DS.Tables("tblservicioseventos").DefaultView
    End Function
    Public Function ConsultaTodosServiciossuc(ByVal pIdEquipo As Integer, ByVal pidServicio As Integer) As DataView
        Dim DS As New DataSet
        If pidServicio = 0 Then
            Comm.CommandText = "select tblservicioseventossuc.idevento,tblservicioseventossuc.idservicio,tblserviciosclasificaciones.nombre,tblserviciosclasificaciones2.nombre,tblservicioseventossuc.comentario,tblservicioseventossuc.precio,tblservicioseventossuc.tiempo from tblservicioseventossuc inner join tblserviciosclasificaciones on tblservicioseventossuc.idclasificacion=tblserviciosclasificaciones.idclasificacion inner join tblserviciosclasificaciones2 on tblservicioseventossuc.idclasificacion2=tblserviciosclasificaciones2.idclasificacion2 where  tblservicioseventossuc.idEquipo=" + pIdEquipo.ToString + " ORDER BY idevento ASC"
        Else
            Comm.CommandText = "select tblservicioseventossuc.idevento,tblservicioseventossuc.idservicio,tblserviciosclasificaciones.nombre,tblserviciosclasificaciones2.nombre,tblservicioseventossuc.comentario,tblservicioseventossuc.precio,tblservicioseventossuc.tiempo from tblservicioseventossuc inner join tblserviciosclasificaciones on tblservicioseventossuc.idclasificacion=tblserviciosclasificaciones.idclasificacion inner join tblserviciosclasificaciones2 on tblservicioseventossuc.idclasificacion2=tblserviciosclasificaciones2.idclasificacion2 where  tblservicioseventossuc.idservicio=" + pidServicio.ToString + " and tblservicioseventossuc.idEquipo=" + pIdEquipo.ToString + " ORDER BY idevento ASC"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicioseventos")
        Return DS.Tables("tblservicioseventos").DefaultView
    End Function
    Public Function filtroArticulosConsumidos(ByVal pIdEquipo As Integer, ByVal pidServicio As Integer) As DataTable
        Dim DS As New DataSet
        If pidServicio = 0 Then
            Comm.CommandText = "select * from tblserviciosinventario2 where idEquipo=" + pIdEquipo.ToString
        Else
            Comm.CommandText = "select * from tblserviciosinventario2 where idEquipo=" + pIdEquipo.ToString + " and idevento=" + pidServicio.ToString
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosinventario2")
        Return DS.Tables("tblserviciosinventario2")
    End Function
    Public Function filtroArticulosConsumidossuc(ByVal pIdEquipo As Integer, ByVal pidServicio As Integer) As DataTable
        Dim DS As New DataSet
        If pidServicio = 0 Then
            Comm.CommandText = "select * from tblserviciosinventario2suc where idEquipo=" + pIdEquipo.ToString
        Else
            Comm.CommandText = "select * from tblserviciosinventario2suc where idEquipo=" + pIdEquipo.ToString + " and idevento=" + pidServicio.ToString
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblserviciosinventario2")
        Return DS.Tables("tblserviciosinventario2")
    End Function
    Public Function ConsultaServiciosId(ByVal pIdEquipo As Integer) As DataView
        Dim DS As New DataSet
        '' If pidServicio = 0 Then
        Comm.CommandText = "select DISTINCT idservicio from tblservicioseventos  where  tblservicioseventos.idEquipo=" + pIdEquipo.ToString + " ORDER BY idevento ASC"
        'Else
        'Comm.CommandText = "select tblservicioseventos.idevento,tblservicioseventos.idservicio,tblserviciosclasificaciones.nombre,tblserviciosclasificaciones2.nombre,tblservicioseventos.comentario,tblservicioseventos.precio,tblservicioseventos.tiempo from tblservicioseventos inner join tblserviciosclasificaciones on tblservicioseventos.idclasificacion=tblserviciosclasificaciones.idclasificacion inner join tblserviciosclasificaciones2 on tblservicioseventos.idclasificacion2=tblserviciosclasificaciones2.idclasificacion2 where  tblservicioseventos.idservicio=" + pidServicio.ToString + " and tblservicioseventos.idEquipo=" + pIdEquipo.ToString + " ORDER BY idevento ASC"
        'End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicioseventos")
        Return DS.Tables("tblservicioseventos").DefaultView
    End Function
    Public Function ConsultaServiciosIdSuc(ByVal pIdEquipo As Integer) As DataView
        Dim DS As New DataSet
        '' If pidServicio = 0 Then
        Comm.CommandText = "select DISTINCT idservicio from tblservicioseventossuc  where  tblservicioseventossuc.idEquipo=" + pIdEquipo.ToString + " ORDER BY idevento ASC"
        'Else
        'Comm.CommandText = "select tblservicioseventos.idevento,tblservicioseventos.idservicio,tblserviciosclasificaciones.nombre,tblserviciosclasificaciones2.nombre,tblservicioseventos.comentario,tblservicioseventos.precio,tblservicioseventos.tiempo from tblservicioseventos inner join tblserviciosclasificaciones on tblservicioseventos.idclasificacion=tblserviciosclasificaciones.idclasificacion inner join tblserviciosclasificaciones2 on tblservicioseventos.idclasificacion2=tblserviciosclasificaciones2.idclasificacion2 where  tblservicioseventos.idservicio=" + pidServicio.ToString + " and tblservicioseventos.idEquipo=" + pIdEquipo.ToString + " ORDER BY idevento ASC"
        'End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicioseventos")
        Return DS.Tables("tblservicioseventos").DefaultView
    End Function

    Public Sub detallesServicio(ByVal pidServicio As Integer)
        'ID = pID
        '  Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblservicios where idservicio=" + pidServicio.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            detalles = DReader("detalles")
            fechae = DReader("fechae")
            horae = DReader("horae")
            fechas = DReader("fechas")
            horas = DReader("horas")
            estado = DReader("estado")
        End If
        DReader.Close()
    End Sub
    Public Sub detallesServiciosuc(ByVal pidServicio As Integer)
        'ID = pID
        '  Comm.Connection = Conexion
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblserviciossuc where idservicio=" + pidServicio.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            detalles = DReader("detalles")
            fechae = DReader("fechae")
            horae = DReader("horae")
            fechas = DReader("fechas")
            horas = DReader("horas")
            estado = DReader("estado")
        End If
        DReader.Close()
    End Sub

    Public Function idEquipoConServicio() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select DISTINCT idEquipo from tblservicios  ORDER BY idEquipo ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicios")
        Return DS.Tables("tblservicios").DefaultView
    End Function
    Public Function idEquipoConServiciosuc() As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select DISTINCT idEquipo from tblserviciossuc  ORDER BY idEquipo ASC"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicios")
        Return DS.Tables("tblservicios").DefaultView
    End Function

    Public Function buscarServiciosTecnico(ByVal pidTecnico As Integer, ByVal pidclasificacion As Integer, ByVal pidSubClasificacion As Integer) As DataView
        Dim DS As New DataSet
        If pidclasificacion = 0 And pidSubClasificacion = 0 And pidTecnico <> 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventos where idtecnico=" + pidTecnico.ToString() + " ORDER BY idEquipo ASC"
        End If
        If pidclasificacion = 0 And pidSubClasificacion <> 0 And pidTecnico <> 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventos where idtecnico=" + pidTecnico.ToString() + " and idclasificacion2=" + pidSubClasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If
        If pidclasificacion <> 0 And pidSubClasificacion = 0 And pidTecnico <> 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventos where idtecnico=" + pidTecnico.ToString() + " and idclasificacion=" + pidclasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If
        If pidclasificacion <> 0 And pidSubClasificacion <> 0 And pidTecnico <> 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventos where idtecnico=" + pidTecnico.ToString() + " and idclasificacion=" + pidclasificacion.ToString() + " and idclasificacion2=" + pidSubClasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If

        If pidclasificacion = 0 And pidSubClasificacion = 0 And pidTecnico = 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventos  ORDER BY idEquipo ASC"
        End If
        If pidclasificacion = 0 And pidSubClasificacion <> 0 And pidTecnico = 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventos where  idclasificacion2=" + pidSubClasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If
        If pidclasificacion <> 0 And pidSubClasificacion = 0 And pidTecnico = 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventos where  idclasificacion=" + pidclasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If
        If pidclasificacion <> 0 And pidSubClasificacion <> 0 And pidTecnico = 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventos where  idclasificacion=" + pidclasificacion.ToString() + " and idclasificacion2=" + pidSubClasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicios")
        Return DS.Tables("tblservicios").DefaultView
    End Function
    Public Function buscarServiciosTecnicosuc(ByVal pidTecnico As Integer, ByVal pidclasificacion As Integer, ByVal pidSubClasificacion As Integer) As DataView
        Dim DS As New DataSet
        If pidclasificacion = 0 And pidSubClasificacion = 0 And pidTecnico <> 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventossuc where idtecnico=" + pidTecnico.ToString() + " ORDER BY idEquipo ASC"
        End If
        If pidclasificacion = 0 And pidSubClasificacion <> 0 And pidTecnico <> 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventossuc where idtecnico=" + pidTecnico.ToString() + " and idclasificacion2=" + pidSubClasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If
        If pidclasificacion <> 0 And pidSubClasificacion = 0 And pidTecnico <> 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventossuc where idtecnico=" + pidTecnico.ToString() + " and idclasificacion=" + pidclasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If
        If pidclasificacion <> 0 And pidSubClasificacion <> 0 And pidTecnico <> 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventossuc where idtecnico=" + pidTecnico.ToString() + " and idclasificacion=" + pidclasificacion.ToString() + " and idclasificacion2=" + pidSubClasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If

        If pidclasificacion = 0 And pidSubClasificacion = 0 And pidTecnico = 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventossuc  ORDER BY idEquipo ASC"
        End If
        If pidclasificacion = 0 And pidSubClasificacion <> 0 And pidTecnico = 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventossuc where  idclasificacion2=" + pidSubClasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If
        If pidclasificacion <> 0 And pidSubClasificacion = 0 And pidTecnico = 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventossuc where  idclasificacion=" + pidclasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If
        If pidclasificacion <> 0 And pidSubClasificacion <> 0 And pidTecnico = 0 Then
            Comm.CommandText = "select idevento,idservicio,idclasificacion,idclasificacion2,comentario, tiempo, precio, idEquipo,idtecnico from tblservicioseventossuc where  idclasificacion=" + pidclasificacion.ToString() + " and idclasificacion2=" + pidSubClasificacion.ToString() + " ORDER BY idEquipo ASC"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblservicios")
        Return DS.Tables("tblservicios").DefaultView
    End Function

    Public Function buscarClasificacionNombre(ByVal pID As Integer) As String
        Dim nombre As String
        Comm.CommandText = "select nombre from tblserviciosclasificaciones where idclasificacion=" + pID.ToString
        nombre = Comm.ExecuteScalar
        Return nombre
    End Function

    Public Function buscarSubClasificacionNombre(ByVal pID As Integer) As String
        Dim nombre As String
        Comm.CommandText = "select nombre from tblserviciosclasificaciones2 where idclasificacion2=" + pID.ToString
        nombre = Comm.ExecuteScalar
        Return nombre
    End Function

    Public Function buscarEquipoNombre(ByVal pID As Integer) As String
        Dim nombre As String
        Comm.CommandText = "select nombre from tblclientesequipos where idequipo=" + pID.ToString
        nombre = Comm.ExecuteScalar
        Return nombre
    End Function
    Public Function buscarEquipoNombreSUC(ByVal pID As Integer) As String
        Dim nombre As String
        Comm.CommandText = "select nombre from tblsucequipos where idequipo=" + pID.ToString
        nombre = Comm.ExecuteScalar
        Return nombre
    End Function

    Public Function buscarTecnicoNombre(ByVal pID As Integer) As String
        Dim nombre As String
        Comm.CommandText = "select nombre from tbltecnicos where idtecnico=" + pID.ToString
        nombre = Comm.ExecuteScalar
        Return nombre
    End Function
    Public Function esTerminal(ByVal pID As Integer) As Boolean
        Dim nombre As String
        Comm.CommandText = "select estado from tblserviciosestados where idEstado=" + pID.ToString + " and final=1"
        nombre = Comm.ExecuteScalar
        If nombre <> "" Then
            Return True
        Else
            Return False
        End If

    End Function
End Class
