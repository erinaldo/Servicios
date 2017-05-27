Public Class dbFertilizantesPedidoDetalles
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    'Public Producto As dbProductosVariantes
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdPedido As Integer
    Public Descripcion As String
    Public NuevoConcepto As Boolean
    Public Iva As Double
    'Public Extra As String
    Public Descuento As Double
    'Public IdVariante As Integer
    Public Surtido As Double
    Public PrecioOriginal As Double
    Public IEPS As Double
    Public IVARetenido As Double
    Public Hectareas As Double
    Public Cantxhect As Double
    Public AFavor As Byte
    Public AfavorAnterior As Double
    'Public IdAlmacen As Integer
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdPedido = 0
        Descripcion = ""
        Iva = 0
        Descuento = 0
        Surtido = 0
        PrecioOriginal = 0
        Hectareas = 0
        Cantxhect = 0
        AFavor = 0
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
        Comm.CommandText = "select * from tblfertilizantespedidosdetalles where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            AfavorAnterior = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdPedido = DReader("idpedido")
            Descripcion = DReader("descripcion")
            Iva = DReader("iva")
            Descuento = DReader("descuento")
            Surtido = DReader("surtido")
            PrecioOriginal = DReader("preciooriginal")
            IEPS = DReader("IEPS")
            IVARetenido = DReader("IVARetenido")
            Hectareas = DReader("hectareas")
            Cantxhect = DReader("cantxhec")
            AFavor = DReader("afavor")
        End If
        DReader.Close()
        If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)

        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdPedido As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIEPS As Double, ByVal pIVARetenido As Double, ByVal pHectareas As Double, ByVal pCantXHec As Double, pAfavor As Integer)
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdPedido = pIdPedido
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        'IdVariante = pIdVariante
        IEPS = pIEPS
        IVARetenido = pIVARetenido
        Hectareas = pHectareas
        Cantxhect = pCantXHec
        AFavor = pAfavor
        If Cantidad = 1 Then
            PrecioOriginal = Precio
        Else
            PrecioOriginal = Precio / Cantidad
        End If
        'Dim IdTemp As Integer
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

        'IdTemp = 0
        'If IdTemp <> 0 Then
        '    Comm.CommandText = "select cantidad from tblventaspedidosinventario where iddetalle=" + IdTemp.ToString
        '    CTemp = Comm.ExecuteScalar
        '    Comm.CommandText = "select precio from tblventaspedidosinventario where iddetalle=" + IdTemp.ToString
        '    PTemp = Comm.ExecuteScalar
        '    If PTemp <> 0 Then
        '        Precio = PTemp / CTemp
        '    Else
        '        Precio = 0
        '    End If
        '    Cantidad += CTemp
        '    Precio = Precio * Cantidad
        '    Comm.CommandText = "update tblventaspedidosinventario set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + " where iddetalle=" + IdTemp.ToString
        '    Comm.ExecuteNonQuery()
        '    ID = IdTemp
        '    LlenaDatos()
        '    NuevoConcepto = False
        'Else
        NuevoConcepto = True
        Comm.CommandText = "insert into tblfertilizantespedidosdetalles(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,descuento,surtido,preciooriginal, IEPS, IVARetenido,hectareas,cantxhec,afavor) values(" + IdPedido.ToString + "," + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + Iva.ToString + "," + Descuento.ToString + ",0," + Precio.ToString + "," + IEPS.ToString() + "," + IVARetenido.ToString() + "," + Hectareas.ToString + "," + Cantxhect.ToString + "," + AFavor.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select ifnull((select max(iddetalle) from tblfertilizantespedidosdetalles),0)"
        ID = Comm.ExecuteScalar
        'End If

    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIEPS As Double, ByVal pIVARetenido As Double, ByVal pHectareas As Double, ByVal pCantxHec As Double)

        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        IEPS = pIEPS
        IVARetenido = pIVARetenido
        Hectareas = Hectareas
        Cantxhect = pCantxHec
        If Cantidad = 1 Then
            PrecioOriginal = Precio
        Else
            PrecioOriginal = Precio / Cantidad
        End If
        Comm.CommandText = "update tblfertilizantespedidosdetalles set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + ",preciooriginal=" + Precio.ToString + ",IEPS=" + IEPS.ToString() + ",IVARetenido=" + IVARetenido.ToString() + ",hectareas=" + Hectareas.ToString + ",cantxhec=" + Cantxhect.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblfertilizantespedidosdetalles where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdPedido As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura from tblfertilizantespedidosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idpedido=" + pIdPedido.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfertilizantespedidosdetalles")
        Return DS.Tables("tblfertilizantespedidosdetalles").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdPedido As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.idinventario,tvi.descuento,tvi.IEPS,tvi.IVARetenido,tvi.iva,tvi.hectareas,tvi.cantxhec from tblfertilizantespedidosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad where tvi.idpedido=" + pIdPedido.ToString
        Return Comm.ExecuteReader
    End Function
    Public Function ConsultaReaderIVA(ByVal pIdPedido As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio*(1+(tvi.iva+tvi.ieps-tvi.ivaretenido)/100) as precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tblproductos.clave as pclave,tvi.idinventario,tvi.idvariante,tvi.descuento,tvi.iva,tvi.hectareas,tvi.cantxhec from tblfertilizantespedidosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad where tvi.idpedido=" + pIdPedido.ToString
        Return Comm.ExecuteReader
    End Function
    'Public Sub AgregarCantidad(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTiporedondeo As Byte, ByVal pCantidadDecimales As Byte)
    '    Dim PrecioTemp As Double
    '    Dim IvaTemp As Double

    '    Comm.CommandText = "select precio from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    Precio = Comm.ExecuteScalar

    '    Comm.CommandText = "select cantidad from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    Cantidad = Comm.ExecuteScalar

    '    ID = pID
    '    PrecioTemp = PrecioOriginal / Cantidad * (Cantidad + pCantidad)
    '    Precio = Precio / Cantidad * (Cantidad + pCantidad)
    '    Cantidad = Cantidad + pCantidad


    '    Comm.CommandText = "select iva from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    IvaTemp = Comm.ExecuteScalar
    '    PrecioTemp = PrecioTemp * (1 + (IvaTemp / 100))
    '    If pTiporedondeo <> 0 Then
    '        PrecioTemp = GlobalRedondea(PrecioTemp, pTiporedondeo, pCantidadDecimales)
    '        Precio = PrecioTemp / (1 + (IvaTemp / 100))
    '    End If

    '    If Cantidad > 0 Then
    '        Comm.CommandText = "update tblventaspedidosinventario set precio=" + Precio.ToString + ",cantidad=" + Cantidad.ToString + " where iddetalle=" + ID.ToString
    '        Comm.ExecuteNonQuery()
    '    Else
    '        Eliminar(ID)
    '        ID = 0
    '    End If
    'End Sub
    'Public Sub AsignaCantidad(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte)
    '    ID = pID
    '    Dim PrecioTemp As Double
    '    Dim IvaTemp As Double
    '    Comm.CommandText = "select precio from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    Precio = Comm.ExecuteScalar

    '    Comm.CommandText = "select cantidad from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    Cantidad = Comm.ExecuteScalar
    '    Precio = Precio / Cantidad
    '    'PrecioOriginal = PrecioOriginal / Cantidad
    '    Cantidad = pCantidad
    '    Precio = Precio * Cantidad
    '    PrecioTemp = PrecioOriginal * Cantidad

    '    Comm.CommandText = "select iva from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    IvaTemp = Comm.ExecuteScalar
    '    PrecioTemp = PrecioTemp * (1 + (IvaTemp / 100))
    '    If pTipoRedondeo <> 0 Then
    '        PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
    '        Precio = PrecioTemp / (1 + (IvaTemp / 100))
    '    End If

    '    If Cantidad > 0 Then
    '        Comm.CommandText = "update tblventaspedidosinventario set precio=" + Precio.ToString + ",cantidad=" + Cantidad.ToString + " where iddetalle=" + ID.ToString
    '        Comm.ExecuteNonQuery()
    '    Else
    '        Eliminar(ID)
    '        ID = 0
    '    End If
    'End Sub
    'Public Sub CambiaPrecio(ByVal pId As Integer, ByVal pPrecio As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte, ByVal pDescuento As Double)
    '    If pPrecio <> 0 Then
    '        Dim CTEmp As Double
    '        Comm.CommandText = "select cantidad from tblventaspedidosinventario where iddetalle=" + pId.ToString
    '        CTEmp = Comm.ExecuteScalar
    '        Precio = pPrecio * CTEmp

    '        Dim PrecioTemp As Double
    '        Dim IvaTemp As Double
    '        Comm.CommandText = "select iva from tblventaspedidosinventario where iddetalle=" + pId.ToString
    '        IvaTemp = Comm.ExecuteScalar
    '        PrecioTemp = Precio * (1 + (IvaTemp / 100))
    '        If pTipoRedondeo <> 0 Then
    '            PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
    '            Precio = PrecioTemp / (1 + (IvaTemp / 100))
    '        End If
    '        PrecioOriginal = Precio / CTEmp
    '        If pDescuento = 0 Then
    '            Comm.CommandText = "update tblventaspedidosinventario set precio=" + Precio.ToString + ",preciooriginal=" + Precio.ToString + " where iddetalle=" + pId.ToString
    '        Else
    '            Comm.CommandText = "update tblventaspedidosinventario set precio=" + Precio.ToString + ",preciooriginal=" + Precio.ToString + ",descuento=" + pDescuento.ToString + " where iddetalle=" + pId.ToString
    '        End If
    '        Comm.ExecuteNonQuery()
    '    End If
    'End Sub
    Public Sub SeparaKit(ByVal pIdPedido As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer, ByVal pIesp As Double, ByVal pivaRet As Double, ByVal pHectareas As Double, ByVal pCantxhec As Double)
        Dim STR As String
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdPedido = pIdPedido
        Descripcion = pDescripcion
        'IdAlmacen = pIdAlmacen
        Iva = pIva
        Descuento = pDescuento
        Hectareas = pHectareas
        Cantxhect = pCantxhec
        'idServicio = pidServicio
        Surtido = 0
        'Costo = pCosto
        'CantidadM = pCantidadM
        'TipoCantidadM = pTipoCantidadM
        STR = "insert into tblfertilizantespedidosdetalles(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,descuento,surtido,preciooriginal,ieps,ivaretenido,hectareas,cantxhec) values(" + IdPedido.ToString + "," + Idinventario.ToString + ",0,0,'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + Iva.ToString + "," + Descuento.ToString + ",0," + Precio.ToString + "," + pIesp.ToString + "," + pivaRet.ToString + "," + Hectareas.ToString + "," + Cantxhect.ToString + ");"
        STR += "insert into tblfertilizantespedidosdetalles(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,descuento,surtido,preciooriginal,ieps,ivaretenido,hectareas,cantxhec) select " + IdPedido.ToString + ",tblinventariodetalles.idinventario,tblinventariodetalles.cantidad,(select precio from tblinventarioprecios where idinventario=tblinventariodetalles.idinventario and idlista=1 limit 1)*tblinventariodetalles.cantidad,(select nombre from tblinventario where idinventario=tblinventariodetalles.idinventario)," + IdMoneda.ToString + ",(select iva from tblinventario where idinventario=tblinventariodetalles.idinventario)," + Descuento.ToString + ",0,(select precio from tblinventarioprecios where idinventario=tblinventariodetalles.idinventario and idlista=1 limit 1),(select ieps from tblinventario where idinventario=tblinventariodetalles.idinventario),(select ivaretenido from tblinventario where idinventario=tblinventariodetalles.idinventario) from tblinventariodetalles inner join tblinventario on tblinventario.idinventario=tblinventariodetalles.idinventariop," + pHectareas.ToString + "," + pCantxhec.ToString + "  where tblinventariodetalles.idinventariop=" + pIdinventario.ToString + ";"
        Comm.CommandText = STR
        Comm.ExecuteNonQuery()
    End Sub
    Public Function BuscaridInventario(ByVal pId As Integer) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select idinventario from tblfertilizantespedidosdetalles where iddetalle=" + pId.ToString
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function
    Public Sub GuardarDescuento(ByVal pIdPedido As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer, ByVal pIEPS As Double, ByVal pIvaRetenido As Double)
        Dim CTemp As Double
        Dim PTemp As Double
        Dim pPrecioOriginal As Double = 0

        
        If pCantidad = 1 Then
            pPrecioOriginal = pPrecio
        Else
            pPrecioOriginal = pPrecio / pCantidad
        End If
        Dim IdTemp As Integer
        
        If pIdinventario <> 0 Then
            pIdVariante = 1
        Else
            pIdinventario = 1
        End If
        IdTemp = 0
        If IdTemp <> 0 Then
            Comm.CommandText = "select cantidad from tblfertilizantespedidosdetalles where iddetalle=" + IdTemp.ToString
            CTemp = Comm.ExecuteScalar
            Comm.CommandText = "select precio from tblfertilizantespedidosdetalles where iddetalle=" + IdTemp.ToString
            PTemp = Comm.ExecuteScalar
            If PTemp <> 0 Then
                pPrecio = PTemp / CTemp
            Else
                pPrecio = 0
            End If
            pCantidad += CTemp
            pPrecio = pPrecio * pCantidad
            Comm.CommandText = "update tblfertilizantespedidosdetalles set cantidad=" + pCantidad.ToString + ",precio=" + pPrecio.ToString + " where iddetalle=" + IdTemp.ToString
            Comm.ExecuteNonQuery()
            'ID = IdTemp
            LlenaDatos()
            'NuevoConcepto = False
        Else
            NuevoConcepto = True
            Comm.CommandText = "insert into tblfertilizantespedidosdetalles(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,descuento,surtido,preciooriginal,IEPS, IVARetenido,hectareas,cantxhec) values(" + pIdPedido.ToString + "," + pIdinventario.ToString + "," + pCantidad.ToString + "," + pPrecio.ToString + ",'" + Replace(pDescripcion, "'", "''") + "'," + pIdMoneda.ToString + "," + pIva.ToString + "," + pDescuento.ToString + ",0," + pPrecio.ToString + "," + pIEPS.ToString + "," + pIvaRetenido + ",0,0)"
            Comm.ExecuteNonQuery()

            ' Comm.CommandText = "select ifnull((select max(iddetalle) from tblventaspedidosinventario),0)"
            ' pID = Comm.ExecuteScalar
        End If

    End Sub
    Public Function UltomoRegistro() As Integer
        Dim id As Integer
        Comm.CommandText = "select max(iddetalle) FROM tblfertilizantespedidosdetalles"

        id = Comm.ExecuteScalar
        Return id
        'checar si devulve el ultimo id insertado
    End Function
    'Public Sub AgregarCantidadDescuento(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTiporedondeo As Byte, ByVal pCantidadDecimales As Byte, ByVal des As Double)
    '    ' Dim PrecioTemp As Double
    '    'Dim IvaTemp As Double
    '    Dim PrecioTemp2 As Double
    '    Dim IvaTemp2 As Double
    '    Dim auxPrecio As Double
    '    Dim auxCantidad As Double

    '    'ID = pID
    '    Comm.CommandText = "select precio from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    auxPrecio = Comm.ExecuteScalar

    '    Comm.CommandText = "select cantidad from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    auxCantidad = Comm.ExecuteScalar

    '    auxPrecio = auxPrecio / auxCantidad * (auxCantidad + pCantidad)
    '    auxCantidad = auxCantidad + pCantidad

    '    'PrecioTemp = PrecioOriginal / Cantidad * (Cantidad + pCantidad)
    '    'Precio = Precio / Cantidad * (Cantidad + pCantidad)
    '    'Cantidad = Cantidad + pCantidad


    '    Comm.CommandText = "select iva from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    IvaTemp2 = Comm.ExecuteScalar
    '    PrecioTemp2 = PrecioTemp2 * (1 + (IvaTemp2 / 100))
    '    If pTiporedondeo <> 0 Then
    '        PrecioTemp2 = GlobalRedondea(PrecioTemp2, pTiporedondeo, pCantidadDecimales)
    '        auxPrecio = PrecioTemp2 / (1 + (IvaTemp2 / 100))
    '    End If

    '    If Cantidad > 0 Then
    '        Comm.CommandText = "update tblventaspedidosinventario set precio=" + auxPrecio.ToString + ",cantidad=" + auxCantidad.ToString + " where iddetalle=" + pID.ToString
    '        Comm.ExecuteNonQuery()
    '    Else
    '        Eliminar(pID)
    '        'ID = 0
    '    End If
    'End Sub

    'Public Sub AsignaCantidadDescuento(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte)
    '    'ID = pID
    '    Dim PrecioTemp As Double
    '    Dim IvaTemp As Double
    '    Dim auxPrecio As Double
    '    Dim auxCantidad As Double
    '    Dim pPrecioOriginal As Double
    '    'Precio = Precio / Cantidad
    '    'PrecioOriginal = PrecioOriginal / Cantidad
    '    Comm.CommandText = "select precio from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    auxPrecio = Comm.ExecuteScalar

    '    Comm.CommandText = "select cantidad from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    auxCantidad = Comm.ExecuteScalar

    '    pPrecioOriginal = auxPrecio / auxCantidad

    '    auxCantidad = pCantidad
    '    auxPrecio = pPrecioOriginal * auxCantidad
    '    PrecioTemp = pPrecioOriginal * auxCantidad

    '    Comm.CommandText = "select iva from tblventaspedidosinventario where iddetalle=" + pID.ToString
    '    IvaTemp = Comm.ExecuteScalar
    '    PrecioTemp = PrecioTemp * (1 + (IvaTemp / 100))
    '    If pTipoRedondeo <> 0 Then
    '        PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
    '        auxPrecio = PrecioTemp / (1 + (IvaTemp / 100))
    '    End If

    '    If Cantidad > 0 Then
    '        Comm.CommandText = "update tblventaspedidosinventario set precio=" + auxPrecio.ToString + ",cantidad=" + auxCantidad.ToString + " where iddetalle=" + pID.ToString
    '        Comm.ExecuteNonQuery()
    '    Else
    '        Eliminar(pID)
    '        ' ID = 0
    '    End If
    'End Sub

End Class
