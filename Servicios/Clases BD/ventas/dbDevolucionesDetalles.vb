Public Class dbDevolucionesDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    'Public Producto As dbProductosVariantes
    Public Servicio As dbServicios
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public idDevolucion As Integer
    Public Descripcion As String
    Public NuevoConcepto As Boolean
    Public IdAlmacen As Integer
    Public Iva As Double
    Public Extra As String
    Public Descuento As Double
    Public idVariante As Integer
    Public idServicio As Integer
    'Public Costo As Double
    Public CantidadM As Double
    Public TipoCantidadM As Integer
    Public Equivalencia As Double
    Public Equivalenciab As Double
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        iddevolucion = 0
        Descripcion = ""
        IdAlmacen = 0
        Iva = 0
        Extra = ""
        Descuento = 0
        idServicio = 0
        idVariante = 0
        'Costo = 0
        NuevoConcepto = False
        Comm.Connection = Conexion
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbldevolucionesdetalles where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            iddevolucion = DReader("iddevolucion")
            Descripcion = DReader("descripcion")
            IdAlmacen = DReader("idalmacen")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
            idVariante = DReader("idvariante")
            idServicio = DReader("idservicio")
            CantidadM = DReader("cantidadm")
            TipoCantidadM = DReader("tipocantidadm")
            Equivalencia = DReader("equivalencia")
            Equivalenciab = DReader("equivalenciab")
            'Costo = DReader("costo")
        End If
        DReader.Close()
        If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
        'If idVariante > 1 Then Producto = New dbProductosVariantes(idVariante, Comm.Connection)
        'If idVariante > 0 Then Servicio = New dbServicios(idServicio, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal piddevolucion As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pidVariante As Integer, ByVal pidServicio As Integer, ByVal pCantidadM As Double, ByVal pIdTipoCantidad As Integer)
        'Dim CTemp As Double
        'Dim PTemp As Double
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        idDevolucion = piddevolucion
        Descripcion = pDescripcion
        IdAlmacen = pIdAlmacen
        Iva = pIva
        Descuento = pDescuento
        idVariante = pidVariante
        idServicio = pidServicio
        CantidadM = pCantidadM
        TipoCantidadM = pIdTipoCantidad
        'Costo = pCosto
        'Dim IdTemp As Integer
        'If Idinventario <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tbldevolucionesdetalles where iddevolucion=" + idDevolucion.ToString + " and idinventario=" + Idinventario.ToString + "),0)"
        '    IdTemp = Comm.ExecuteScalar
        'Else
        '    Idinventario = 1
        'End If
        'If idVariante <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tbldevolucionesdetalles where iddevolucion=" + idDevolucion.ToString + " and idvariante=" + idVariante.ToString + "),0)"
        '    IdTemp = Comm.ExecuteScalar
        'Else
        '    idVariante = 1
        'End If
        'If idServicio <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tbldevolucionesdetalles where iddevolucion=" + idDevolucion.ToString + " and idservicio=" + idServicio.ToString + "),0)"
        '    IdTemp = Comm.ExecuteScalar
        'End If

        'If IdTemp <> 0 Then
        '    Comm.CommandText = "select cantidad from tbldevolucionesdetalles where iddetalle=" + IdTemp.ToString
        '    CTemp = Comm.ExecuteScalar
        '    Comm.CommandText = "select precio from tbldevolucionesdetalles where iddetalle=" + IdTemp.ToString
        '    PTemp = Comm.ExecuteScalar
        '    If PTemp <> 0 Then
        '        Precio = PTemp / CTemp
        '    Else
        '        Precio = 0
        '    End If
        '    Cantidad += CTemp
        '    Precio = Precio * Cantidad
        '    Comm.CommandText = "update tbldevolucionesdetalles set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + ",cantidadm=" + CantidadM.ToString + " where iddetalle=" + IdTemp.ToString
        '    Comm.ExecuteNonQuery()
        '    ID = IdTemp
        '    LlenaDatos()
        '    NuevoConcepto = False
        'Else
        NuevoConcepto = True
        Comm.CommandText = "insert into tbldevolucionesdetalles(iddevolucion,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,cantidadm,tipocantidadm,equivalencia,equivalenciab,ieps,ivaretenido) values(" + idDevolucion.ToString + "," + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + IdAlmacen.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + "," + idVariante.ToString + "," + idServicio.ToString + "," + Cantidad.ToString + "," + TipoCantidadM.ToString + ",1,1,0,0)"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull((select max(iddetalle) from tbldevolucionesdetalles),0)"
        'Comm.CommandText = "select if(max(iddetalle) is null,0,max(iddetalle)) from tbldevolucionesdetalles"
        ID = Comm.ExecuteScalar
        'End If

    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pCantidadM As Double)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        CantidadM = pCantidadM
        Comm.CommandText = "update tbldevolucionesdetalles set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + ",cantidadm=" + CantidadM.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbldevolucionesdetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal piddevolucion As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura from tbldevolucionesdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.iddevolucion=" + piddevolucion.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevolucionesdetalles")
        Return DS.Tables("tbldevolucionesdetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal piddevolucion As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idvariante,tvi.idservicio,tblproductos.clave as pclave,tvi.idinventario,tvi.idvariante,tvi.cantidadm,tvi.equivalencia,tvi.equivalenciab,(select abreviatura from tbltiposcantidades where idtipocantidad=tvi.tipocantidadm) as tipocantidad2,tvi.ieps,tvi.ivaretenido from tbldevolucionesdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocantidad=tbltiposcantidades.idtipocantidad inner join tblproductosvariantes on tvi.idvariante=tblproductosvariantes.idvariante inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto  where tvi.iddevolucion=" + piddevolucion.ToString
        Return Comm.ExecuteReader
    End Function

    Public Function ConsultaReader2(ByVal piddevolucion As Integer, ByVal pIDRenglon As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idvariante,tvi.idservicio,tblproductos.clave as pclave,tvi.idinventario,tvi.idvariante,tvi.cantidadm,tvi.equivalencia,tvi.equivalenciab,(select abreviatura from tbltiposcantidades where idtipocantidad=tvi.tipocantidadm) as tipocantidad2,tvi.ieps,tvi.ivaretenido from tbldevolucionesdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocantidad=tbltiposcantidades.idtipocantidad inner join tblproductosvariantes on tvi.idvariante=tblproductosvariantes.idvariante inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto  where tvi.iddevolucion=" + piddevolucion.ToString + " and tvi.iddetalle= " + pIDRenglon.ToString
        Return Comm.ExecuteReader
    End Function
End Class
