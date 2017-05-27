Public Class dbCajasMovimientosDetalles
    Public ID As Integer
    Public Idinventario As Integer
    'Public Inventario As dbInventario
    'Public Producto As dbProductosVariantes
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdMovimiento As Integer
    Public Descripcion As String
    Public NuevoConcepto As Boolean
    Public Iva As Double
    Public Extra As String
    Public Descuento As Double
    Public IdVariante As Integer
    Public Surtido As Double
    Public PrecioOriginal As Double
    'Public IdAlmacen As Integer
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdMovimiento = 0
        Descripcion = ""
        Iva = 0
        Descuento = 0
        Extra = ""
        IdVariante = 0
        Surtido = 0
        PrecioOriginal = 0
        'IdAlmacen = 0
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
        Comm.CommandText = "select * from tblcajasmovimientosdetalles where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            'Idinventario = DReader("idmovimiento")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdMovimiento = DReader("idmovimiento")
            Descripcion = DReader("descripcion")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
            'IdVariante = DReader("idvariante")
            'Surtido = DReader("surtido")
            'PrecioOriginal = DReader("preciooriginal")
        End If
        DReader.Close()
        'If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
        'If IdVariante > 1 Then Producto = New dbProductosVariantes(IdVariante, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdPedido As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer)
        Dim CTemp As Double
        Dim PTemp As Double
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdMovimiento = pIdPedido
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        IdVariante = pIdVariante
        If Cantidad = 1 Then
            PrecioOriginal = Precio
        Else
            PrecioOriginal = Precio / Cantidad
        End If
        Dim IdTemp As Integer
        'If Idinventario <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblventaspedidosinventario where idpedido=" + IdPedido.ToString + " and idinventario=" + Idinventario.ToString + "),0)"
        '    IdTemp = Comm.ExecuteScalar
        'Else
        '    Idinventario = 1
        'End If
        'If IdVariante <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblventaspedidosinventario where idpedido=" + IdPedido.ToString + " and idvariante=" + IdVariante.ToString + "),0)"
        '    IdTemp = Comm.ExecuteScalar
        'Else
        '    IdVariante = 1
        'End If
        If Idinventario <> 0 Then
            IdVariante = 1
        Else
            Idinventario = 1
        End If
        IdTemp = 0
        If IdTemp <> 0 Then
            Comm.CommandText = "select cantidad from tblcajasmovimientosdetalles where iddetalle=" + IdTemp.ToString
            CTemp = Comm.ExecuteScalar
            Comm.CommandText = "select precio from tblcajasmovimientosdetalles where iddetalle=" + IdTemp.ToString
            PTemp = Comm.ExecuteScalar
            If PTemp <> 0 Then
                Precio = PTemp / CTemp
            Else
                Precio = 0
            End If
            Cantidad += CTemp
            Precio = Precio * Cantidad
            Comm.CommandText = "update tblcajasmovimientosdetalles set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + " where iddetalle=" + IdTemp.ToString
            Comm.ExecuteNonQuery()
            ID = IdTemp
            LlenaDatos()
            NuevoConcepto = False
        Else
            NuevoConcepto = True
            Comm.CommandText = "insert into tblcajasmovimientosdetalles(idmovimiento,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) values(" + IdMovimiento.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + ",0,'',0);"
            'Comm.ExecuteNonQuery()
            Comm.CommandText += "select ifnull((select max(iddetalle) from tblcajasmovimientosdetalles),0);"
            ID = Comm.ExecuteScalar
        End If

    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        If Cantidad = 1 Then
            PrecioOriginal = Precio
        Else
            PrecioOriginal = Precio / Cantidad
        End If
        Comm.CommandText = "update tblcajasmovimientosdetalles set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcajasmovimientosdetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdPedido As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura from tblcajasmovimientosdetalles tvi inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idmovimiento=" + pIdPedido.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcajasmovimientosdetalles")
        Return DS.Tables("tblcajasmovimientosdetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdPedido As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tvi.descuento,tvi.iva from tblcajasmovimientosdetalles tvi inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idmovimiento=" + pIdPedido.ToString
        Return Comm.ExecuteReader
    End Function
    Public Function ConsultaReaderIVA(ByVal pIdPedido As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tvi.descripcion,tvi.cantidad,tvi.precio*(1+tvi.iva/100) as precio,tblmonedas.abreviatura,tvi.idinventario,tvi.idvariante,tvi.descuento,tvi.iva from tblcajasmovimientosdetalles tvi inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idmovimiento=" + pIdPedido.ToString
        Return Comm.ExecuteReader
    End Function
    Public Sub AgregarCantidad(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTiporedondeo As Byte, ByVal pCantidadDecimales As Byte)
        Dim PrecioTemp As Double
        Dim IvaTemp As Double
        ID = pID
        PrecioTemp = PrecioOriginal / Cantidad * (Cantidad + pCantidad)
        Precio = Precio / Cantidad * (Cantidad + pCantidad)
        Cantidad = Cantidad + pCantidad


        Comm.CommandText = "select iva from tblcajasmovimientosdetalles where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        PrecioTemp = PrecioTemp * (1 + (IvaTemp / 100))
        If pTiporedondeo <> 0 Then
            PrecioTemp = GlobalRedondea(PrecioTemp, pTiporedondeo, pCantidadDecimales)
            Precio = PrecioTemp / (1 + (IvaTemp / 100))
        End If

        If Cantidad > 0 Then
            Comm.CommandText = "update tblcajasmovimientosdetalles set precio=" + Precio.ToString + ",cantidad=" + Cantidad.ToString + " where iddetalle=" + ID.ToString
            Comm.ExecuteNonQuery()
        Else
            Eliminar(ID)
            ID = 0
        End If
    End Sub
    Public Sub AsignaCantidad(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte)
        ID = pID
        Dim PrecioTemp As Double
        Dim IvaTemp As Double
        Precio = Precio / Cantidad
        'PrecioOriginal = PrecioOriginal / Cantidad
        Cantidad = pCantidad
        Precio = Precio * Cantidad
        PrecioTemp = PrecioOriginal * Cantidad

        Comm.CommandText = "select iva from tblcajasmovimientosdetalles where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        PrecioTemp = PrecioTemp * (1 + (IvaTemp / 100))
        If pTipoRedondeo <> 0 Then
            PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
            Precio = PrecioTemp / (1 + (IvaTemp / 100))
        End If

        If Cantidad > 0 Then
            Comm.CommandText = "update tblcajasmovimientosdetalles set precio=" + Precio.ToString + ",cantidad=" + Cantidad.ToString + " where iddetalle=" + ID.ToString
            Comm.ExecuteNonQuery()
        Else
            Eliminar(ID)
            ID = 0
        End If
    End Sub
    Public Sub CambiaPrecio(ByVal pId As Integer, ByVal pPrecio As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte, ByVal pDescuento As Double)
        If pPrecio <> 0 Then
            Dim CTEmp As Double
            Comm.CommandText = "select cantidad from tblcajasmovimientosdetalles where iddetalle=" + pId.ToString
            CTEmp = Comm.ExecuteScalar
            Precio = pPrecio * CTEmp

            Dim PrecioTemp As Double
            Dim IvaTemp As Double
            Comm.CommandText = "select iva from tblcajasmovimientosdetalles where iddetalle=" + pId.ToString
            IvaTemp = Comm.ExecuteScalar
            PrecioTemp = Precio * (1 + (IvaTemp / 100))
            If pTipoRedondeo <> 0 Then
                PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
                Precio = PrecioTemp / (1 + (IvaTemp / 100))
            End If
            PrecioOriginal = Precio / CTEmp
            If pDescuento = 0 Then
                Comm.CommandText = "update tblcajasmovimientosdetalles set precio=" + Precio.ToString + ",preciooriginal=" + Precio.ToString + " where iddetalle=" + pId.ToString
            Else
                Comm.CommandText = "update tblcajasmovimientosdetalles set precio=" + Precio.ToString + ",preciooriginal=" + Precio.ToString + ",descuento=" + pDescuento.ToString + " where iddetalle=" + pId.ToString
            End If
            Comm.ExecuteNonQuery()
        End If
    End Sub
End Class
