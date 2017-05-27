Public Class dbNotasDeCreditoDetalles
    Public ID As Integer
    Public Idinventario As Integer
    'Public Inventario As dbInventario
    'Public Producto As dbProductosVariantes
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdVenta As Integer
    Public Descripcion As String
    Public NuevoConcepto As Boolean
    Public Iva As Double
    Public Extra As String
    Public Descuento As Double
    Public IdVariante As Integer
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdVenta = 0
        Descripcion = ""
        Iva = 0
        Extra = ""
        Descuento = 0
        IdVariante = 0
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
        Comm.CommandText = "select * from tblnotasdecreditodetalles where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdVenta = DReader("idnota")
            Descripcion = DReader("descripcion")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
            IdVariante = DReader("idventa")
        End If
        DReader.Close()
        'If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
        'If IdVariante > 1 Then Producto = New dbProductosVariantes(IdVariante, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer)
        'Dim CTemp As Double
        'Dim PTemp As Double
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdVenta = pIdVenta
        Descripcion = pDescripcion

        Iva = pIva
        Descuento = pDescuento
        IdVariante = pIdVariante

        'Dim IdTemp As Integer
        'If Idinventario <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblnotasdecreditodetalles where idnota=" + IdVenta.ToString + " and idinventario=" + Idinventario.ToString + "),0)"
        '    IdTemp = Comm.ExecuteScalar
        'Else
        '    Idinventario = 1
        'End If
        'If IdVariante <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblventascotizacionesinventario where idcotizacion=" + IdVenta.ToString + " and idvariante=" + IdVariante.ToString + "),0)"
        '    IdTemp = Comm.ExecuteScalar
        'Else
        '    IdVariante = 1
        'End If


        'If IdTemp <> 0 Then
        '    Comm.CommandText = "select cantidad from tblventascotizacionesinventario where iddetalle=" + IdTemp.ToString
        '    CTemp = Comm.ExecuteScalar
        '    Comm.CommandText = "select precio from tblventascotizacionesinventario where iddetalle=" + IdTemp.ToString
        '    PTemp = Comm.ExecuteScalar
        '    If PTemp <> 0 Then
        '        Precio = PTemp / CTemp
        '    Else
        '        Precio = 0
        '    End If
        '    Cantidad += CTemp
        '    Precio = Precio * Cantidad
        '    Comm.CommandText = "update tblventascotizacionesinventario set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + " where iddetalle=" + IdTemp.ToString
        '    Comm.ExecuteNonQuery()
        '    ID = IdTemp
        '    LlenaDatos()
        '    NuevoConcepto = False
        'Else
        NuevoConcepto = True
        Comm.CommandText = "insert into tblnotasdecreditodetalles(idnota,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idventa) values(" + IdVenta.ToString + "," + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + "," + IdVariante.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull((select max(iddetalle) from tblnotasdecreditodetalles),0)"
        ID = Comm.ExecuteScalar
        'End If

    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal piva As Double, ByVal pDescuento As Double)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = piva
        Descuento = pDescuento
        Comm.CommandText = "update tblnotasdecreditodetalles set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblnotasdecreditodetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura from tblnotasdecreditodetalles tvi inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idnota=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblnotasdecreditodetalles")
        Return DS.Tables("tblnotasdecreditodetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tvi.iva,tvi.idmoneda,tvi.idventa,tvi.idinventario," + _
        "case tvi.idinventario when 0 then ifnull((select serie from tblventas where idventa=tvi.idventa limit 1),'') when 1 then ifnull((select serie from tblnotasdecargo where idcargo=tvi.idventa limit 1),'') when 2 then ifnull((select serie from tbldocumentosclientes where iddocumento=tvi.idventa limit 1),'') when 3 then ifnull((select seriereferencia from tbldocumentosclientes where iddocumento=tvi.idventa limit 1),'') end as serieventa," + _
        "case tvi.idinventario when 0 then ifnull((select folio from tblventas where idventa=tvi.idventa limit 1),0) when 1 then ifnull((select folio from tblnotasdecargo where idcargo=tvi.idventa limit 1),0) when 2 then ifnull((select folio from tbldocumentosclientes where iddocumento=tvi.idventa limit 1),0) when 3 then ifnull((select folioreferencia from tbldocumentosclientes where iddocumento=tvi.idventa limit 1),0) end as folioventa" + _
        " from tblnotasdecreditodetalles tvi inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idnota=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
End Class
