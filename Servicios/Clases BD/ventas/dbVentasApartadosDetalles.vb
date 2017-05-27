Public Class dbVentasApartadosDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    'Public Producto As dbProductosVariantes
    'Public Servicio As dbServicios
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdApartado As Integer
    Public Descripcion As String
    Public NuevoConcepto As Boolean
    Public IdAlmacen As Integer
    Public Iva As Double
    Public Extra As String
    Public Descuento As Double
    'Public idVariante As Integer
    'Public idServicio As Integer
    Public Surtido As Double
    'Public CantidadM As Double
    'Public TipoCantidadM As Integer
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdApartado = 0
        Descripcion = ""
        IdAlmacen = 0
        Iva = 0
        Extra = ""
        Descuento = 0
        'idServicio = 0
        'idVariante = 0
        Surtido = 0
        'Costo = 0
        'CantidadM = 0
        'TipoCantidadM = 0
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
        Comm.CommandText = "select * from tblventasapartadosdetalles where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdApartado = DReader("idapartado")
            Descripcion = DReader("descripcion")
            IdAlmacen = DReader("idalmacen")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
            'idVariante = DReader("idvariante")
            'idServicio = DReader("idservicio")
            Surtido = DReader("surtido")
            'Costo = DReader("costo")
            'CantidadM = DReader("cantidadm")
            'TipoCantidadM = DReader("tipocantidadm")
        End If
        DReader.Close()
        If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
        'If idVariante > 1 Then Producto = New dbProductosVariantes(idVariante, Comm.Connection)
        'If idVariante > 0 Then Servicio = New dbServicios(idServicio, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pidapartado As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double)
        'Dim CTemp As Double
        'Dim PTemp As Double
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdApartado = pidapartado
        Descripcion = pDescripcion
        IdAlmacen = pIdAlmacen
        Iva = pIva
        Descuento = pDescuento
        'idVariante = pidVariante
        'idServicio = pidServicio
        Surtido = 0
        'Costo = pCosto
        'CantidadM = pCantidadM
        'TipoCantidadM = pTipoCantidadM
        'Dim IdTemp As Integer = 0
        'pSeparado = 0
        NuevoConcepto = True
        Comm.CommandText = "insert into tblventasapartadosdetalles(idapartado,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,surtido) values(" + IdApartado.ToString + "," + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + IdAlmacen.ToString + "," + Iva.ToString + ",'" + Replace(Extra, "'", "''") + "'," + Descuento.ToString + ",0);"
        'Comm.ExecuteNonQuery()
        Comm.CommandText += "select ifnull((select max(iddetalle) from tblventasapartadosdetalles),0);"
        'Comm.CommandText = "select if(max(iddetalle) is null,0,max(iddetalle)) from tblventasapartadosdetalles"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        'CantidadM = pCantidadM
        'TipoCantidadM = pTipoCantidadM
        Comm.CommandText = "update tblventasapartadosdetalles set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventasapartadosdetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pidapartado As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura from tblventasapartadosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idapartado=" + pidapartado.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasapartadosdetalles")
        Return DS.Tables("tblventasapartadosdetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pidapartado As Integer, ByVal pConSeries As Boolean) As MySql.Data.MySqlClient.MySqlDataReader
        If pConSeries Then
            Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,concat(tvi.descripcion,spdaseriesventa(tvi.idinventario,tvi.idapartado)) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idinventario,tvi.descuento,tvi.extra,tblinventario.clave2 from tblventasapartadosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad where tvi.idapartado=" + pidapartado.ToString
        Else
            Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idinventario,tvi.descuento,tvi.extra,tblinventario.clave2 from tblventasapartadosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad  where tvi.idapartado=" + pidapartado.ToString
        End If
        Return Comm.ExecuteReader
    End Function

End Class
