Public Class dbVentasCotizacionesInventario
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
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
    Public PrecioOriginal As Integer
    Public IEPS As Double
    Public IVARetenido As Double
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
        PrecioOriginal = 0
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
        Comm.CommandText = "select * from tblventascotizacionesinventario where iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdVenta = DReader("idcotizacion")
            Descripcion = DReader("descripcion")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
            IdVariante = DReader("idvariante")
            PrecioOriginal = DReader("preciooriginal")
            IEPS = DReader("IEPS")
            IVARetenido = DReader("IVARetenido")
        End If
        DReader.Close()
        If Idinventario > 1 Then Inventario = New dbInventario(Idinventario, Comm.Connection)
        'If IdVariante > 1 Then Producto = New dbProductosVariantes(IdVariante, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer, ByVal pIEPS As Double, ByVal pIVARetenido As Double)
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
        IEPS = pIEPS
        IVARetenido = pIVARetenido

        If Cantidad = 1 Then
            PrecioOriginal = Precio
        Else
            PrecioOriginal = Precio / Cantidad
        End If
        If Idinventario = 0 Then
            Idinventario = 1
        Else
            IdVariante = 1
        End If

        'Dim IdTemp As Integer
        'If Idinventario <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblventascotizacionesinventario where idcotizacion=" + IdVenta.ToString + " and idinventario=" + Idinventario.ToString + "),0)"
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


        'If False Then
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
        Comm.CommandText = "insert into tblventascotizacionesinventario(idcotizacion,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,preciooriginal, IEPS, IVARetenido) values(" + IdVenta.ToString + "," + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + "," + IdVariante.ToString + "," + Precio.ToString + "," + IEPS.ToString() + "," + IVARetenido.ToString() + ");"
        'Comm.ExecuteNonQuery()
        Comm.CommandText += "select ifnull((select max(iddetalle) from tblventascotizacionesinventario),0);"
        ID = Comm.ExecuteScalar
        ' End If

    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal piva As Double, ByVal pDescuento As Double, ByVal pIEPS As Double, ByVal pIVARetenido As Double)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = piva
        Descuento = pDescuento
        IEPS = pIEPS
        IVARetenido = pIVARetenido
        If Cantidad = 1 Then
            PrecioOriginal = Precio
        Else
            PrecioOriginal = Precio / Cantidad
        End If
        Comm.CommandText = "update tblventascotizacionesinventario set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + ",preciooriginal=" + Precio.ToString + ",IEPS=" + IEPS.ToString() + ",IVARetenido=" + IVARetenido.ToString() + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura from tblventascotizacionesinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idcotizacion=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventascotizacionesinventario")
        Return DS.Tables("tblventascotizacionesinventario").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.IEPS,tvi.IVARetenido,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tblproductos.clave as pclave,tvi.idinventario,tvi.idvariante,tvi.descuento,tvi.iva from tblventascotizacionesinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad inner join tblproductosvariantes on tvi.idvariante=tblproductosvariantes.idvariante inner join tblproductos on tblproductos.idproducto=tblproductosvariantes.idproducto where tvi.idcotizacion=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
    Public Function ConsultaReaderIVA(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio*(1+(tvi.iva+tvi.ieps-tvi.ivaretenido)/100) as precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tblproductos.clave as pclave,tvi.idinventario,tvi.idvariante,tvi.descuento,tvi.iva from tblventascotizacionesinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad inner join tblproductosvariantes on tvi.idvariante=tblproductosvariantes.idvariante inner join tblproductos on tblproductos.idproducto=tblproductosvariantes.idproducto where tvi.idcotizacion=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
    Public Sub AgregarCantidad(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTiporedondeo As Byte, ByVal pCantidadDecimales As Byte)
        Dim PrecioTemp As Double
        Dim IvaTemp As Double
        ID = pID
        Comm.CommandText = "select precio from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        Precio = Comm.ExecuteScalar

        Comm.CommandText = "select cantidad from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        Cantidad = Comm.ExecuteScalar
        PrecioTemp = PrecioOriginal / Cantidad * (Cantidad + pCantidad)
        Precio = Precio / Cantidad * (Cantidad + pCantidad)
        Cantidad = Cantidad + pCantidad

        
        Comm.CommandText = "select iva from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        PrecioTemp = PrecioTemp * (1 + (IvaTemp / 100))
        If pTiporedondeo <> 0 Then
            PrecioTemp = GlobalRedondea(PrecioTemp, pTiporedondeo, pCantidadDecimales)
            Precio = PrecioTemp / (1 + (IvaTemp / 100))
        End If

        If Cantidad > 0 Then
            Comm.CommandText = "update tblventascotizacionesinventario set precio=" + Precio.ToString + ",cantidad=" + Cantidad.ToString + " where iddetalle=" + ID.ToString
            Comm.ExecuteNonQuery()
        Else
            Eliminar(ID)
            ID = 0
        End If
    End Sub
    Public Sub AsignaCantidad(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte)
        Dim PrecioTemp As Double
        Dim IvaTemp As Double
        Comm.CommandText = "select precio from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        Precio = Comm.ExecuteScalar

        Comm.CommandText = "select cantidad from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        Cantidad = Comm.ExecuteScalar
        ID = pID
        Precio = Precio / Cantidad
        'PrecioOriginal = PrecioOriginal / Cantidad
        Cantidad = pCantidad
        Precio = Precio * Cantidad
        PrecioTemp = PrecioOriginal * Cantidad

        Comm.CommandText = "select iva from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        PrecioTemp = PrecioOriginal * (1 + (IvaTemp / 100))
        If pTipoRedondeo <> 0 Then
            PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
            Precio = PrecioTemp / (1 + (IvaTemp / 100))
        End If

        If Cantidad > 0 Then
            Comm.CommandText = "update tblventascotizacionesinventario set precio=" + Precio.ToString + ",cantidad=" + Cantidad.ToString + " where iddetalle=" + ID.ToString
            Comm.ExecuteNonQuery()
        Else
            Eliminar(ID)
            ID = 0
        End If
    End Sub
    Public Sub CambiaPrecio(ByVal pId As Integer, ByVal pPrecio As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte, ByVal pDescuento As Double)
        If pPrecio <> 0 Then
            Dim CTEmp As Double
            Comm.CommandText = "select cantidad from tblventascotizacionesinventario where iddetalle=" + pId.ToString
            CTEmp = Comm.ExecuteScalar
            Precio = pPrecio * CTEmp
            Dim PrecioTemp As Double
            Dim IvaTemp As Double
            Comm.CommandText = "select iva from tblventascotizacionesinventario where iddetalle=" + pId.ToString
            IvaTemp = Comm.ExecuteScalar
            PrecioTemp = Precio * (1 + (IvaTemp / 100))
            If pTipoRedondeo <> 0 Then
                PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
                Precio = PrecioTemp / (1 + (IvaTemp / 100))
            End If
            PrecioOriginal = Precio / CTEmp
            If pDescuento = 0 Then
                Comm.CommandText = "update tblventascotizacionesinventario set precio=" + Precio.ToString + ",preciooriginal=" + Precio.ToString + " where iddetalle=" + pId.ToString
            Else
                Comm.CommandText = "update tblventascotizacionesinventario set precio=" + Precio.ToString + ",preciooriginal=" + Precio.ToString + ",descuento=" + pDescuento.ToString + " where iddetalle=" + pId.ToString
            End If
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub SeparaKit(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer, ByVal pIeps As Double, ByVal pivaRet As Double)
        Dim STR As String
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdVenta = pIdVenta
        Descripcion = pDescripcion
        'IdAlmacen = pIdAlmacen
        Iva = pIva
        Descuento = pDescuento
        IdVariante = 1
        'idServicio = pidServicio
        'Surtido = 0
        'Costo = pCosto
        'CantidadM = pCantidadM
        'TipoCantidadM = pTipoCantidad
        STR = "insert into tblventascotizacionesinventario(idcotizacion,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,preciooriginal,ieps,ivaretenido) values(" + IdVenta.ToString + "," + Idinventario.ToString + ",0,0,'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + "," + IdVariante.ToString + "," + Precio.ToString + "," + pIeps.ToString + "," + pivaRet.ToString + ");"
        STR += "insert into tblventascotizacionesinventario(idcotizacion,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,preciooriginal,ieps,ivaretenido) select " + IdVenta.ToString + ",tblinventariodetalles.idinventario,tblinventariodetalles.cantidad,(select precio from tblinventarioprecios where idinventario=tblinventariodetalles.idinventario and idlista=1 limit 1)*tblinventariodetalles.cantidad,(select nombre from tblinventario where idinventario=tblinventariodetalles.idinventario)," + IdMoneda.ToString + ",(select iva from tblinventario where idinventario=tblinventariodetalles.idinventario),''," + Descuento.ToString + ",1,(select precio from tblinventarioprecios where idinventario=tblinventariodetalles.idinventario and idlista=1 limit 1),(select ieps from tblinventario where idinventario=tblinventariodetalles.idinventario),(select ivaretenido from tblinventario where idinventario=tblinventariodetalles.idinventario) from tblinventariodetalles inner join tblinventario on tblinventario.idinventario=tblinventariodetalles.idinventariop  where tblinventariodetalles.idinventariop=" + pIdinventario.ToString + ";"
        Comm.CommandText = STR
        Comm.ExecuteNonQuery()
    End Sub

    Public Function UltomoRegistro() As Integer
        Dim id As Integer
        Comm.CommandText = "select max(iddetalle) FROM tblventascotizacionesinventario"
        id = Comm.ExecuteScalar
        Return id
        'checar si devulve el ultimo id insertado
    End Function

    Public Function BuscaridInventario(ByVal pId As Integer) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select idinventario from tblventascotizacionesinventario where iddetalle=" + pId.ToString
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function

    Public Sub AsignaCantidadDescuento(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte)
        Dim PrecioTemp As Double
        Dim IvaTemp As Double
        Dim auxPrecio As Double
        Dim auxCantidad As Double
        Dim pPrecioOriginal As Double

        Comm.CommandText = "select precio from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        auxPrecio = Comm.ExecuteScalar

        Comm.CommandText = "select cantidad from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        auxCantidad = Comm.ExecuteScalar

        pPrecioOriginal = auxPrecio / auxCantidad

        auxCantidad = pCantidad
        auxPrecio = auxPrecio * auxCantidad
        PrecioTemp = pPrecioOriginal * auxCantidad
        auxPrecio = PrecioTemp
        'ID = pID
        ' Precio = Precio / Cantidad
        'PrecioOriginal = PrecioOriginal / Cantidad
        'Cantidad = pCantidad
        'Precio = Precio * Cantidad
        ' PrecioTemp = PrecioOriginal * Cantidad

        Comm.CommandText = "select iva from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        PrecioTemp = pPrecioOriginal * (1 + (IvaTemp / 100))
        If pTipoRedondeo <> 0 Then
            PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
            auxPrecio = PrecioTemp / (1 + (IvaTemp / 100))
        End If

        If Cantidad > 0 Then
            Comm.CommandText = "update tblventascotizacionesinventario set precio=" + auxPrecio.ToString + ",cantidad=" + auxCantidad.ToString + " where iddetalle=" + pID.ToString
            Comm.ExecuteNonQuery()
        Else
            Eliminar(pID)
            'ID = 0
        End If
    End Sub


    Public Sub GuardarDescuento(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer, ByVal pIEPS As Double, ByVal pIVARetenido As Double)
        'Dim CTemp As Double
        'Dim PTemp As Double
        Dim pPrecioOriginal As Double
        'Idinventario = pIdinventario
        'Cantidad = pCantidad
        'Precio = pPrecio
        'IdMoneda = pIdMoneda
        'IdVenta = pIdVenta
        'Descripcion = pDescripcion

        'Iva = pIva
        'Descuento = pDescuento
        'IdVariante = pIdVariante
        If pCantidad = 1 Then
            pPrecioOriginal = pPrecio
        Else
            pPrecioOriginal = pPrecio / pCantidad
        End If
        If pIdinventario = 0 Then
            pIdinventario = 1
        Else
            pIdVariante = 1
        End If

        'Dim IdTemp As Integer
        'If Idinventario <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblventascotizacionesinventario where idcotizacion=" + IdVenta.ToString + " and idinventario=" + Idinventario.ToString + "),0)"
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


        'If False Then
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
        Comm.CommandText = "insert into tblventascotizacionesinventario(idcotizacion,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,preciooriginal,IEPS, IVARetenido) values(" + pIdVenta.ToString + "," + pIdinventario.ToString + "," + pCantidad.ToString + "," + pPrecio.ToString + ",'" + Replace(pDescripcion, "'", "''") + "'," + pIdMoneda.ToString + "," + pIva.ToString + ",''," + pDescuento.ToString + "," + pIdVariante.ToString + "," + pPrecio.ToString + "," + pIEPS.ToString() + "," + pIVARetenido.ToString + ");"
        Comm.ExecuteNonQuery()
        'Comm.CommandText += "select ifnull((select max(iddetalle) from tblventascotizacionesinventario),0);"
        'pID = Comm.ExecuteScalar
        ' End If

    End Sub

    Public Sub CambiaPrecioDescuento(ByVal pId As Integer, ByVal pPrecio As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte, ByVal pDescuento As Double)
        If pPrecio <> 0 Then
            Dim CTEmp As Double
            Comm.CommandText = "select cantidad from tblventascotizacionesinventario where iddetalle=" + pId.ToString
            CTEmp = Comm.ExecuteScalar
            pPrecio = pPrecio * CTEmp
            Dim PrecioTemp As Double
            Dim IvaTemp As Double
            Comm.CommandText = "select iva from tblventascotizacionesinventario where iddetalle=" + pId.ToString
            IvaTemp = Comm.ExecuteScalar
            PrecioTemp = pPrecio * (1 + (IvaTemp / 100))
            If pTipoRedondeo <> 0 Then
                PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
                pPrecio = PrecioTemp / (1 + (IvaTemp / 100))
            End If
            PrecioOriginal = pPrecio / CTEmp
            If pDescuento = 0 Then
                Comm.CommandText = "update tblventascotizacionesinventario set precio=" + pPrecio.ToString + ",preciooriginal=" + pPrecio.ToString + " where iddetalle=" + pId.ToString
            Else
                Comm.CommandText = "update tblventascotizacionesinventario set precio=" + pPrecio.ToString + ",preciooriginal=" + pPrecio.ToString + ",descuento=" + pDescuento.ToString + " where iddetalle=" + pId.ToString
            End If
            Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Sub AgregarCantidadDescuento(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTiporedondeo As Byte, ByVal pCantidadDecimales As Byte)
        ' Dim PrecioTemp As Double
        Dim IvaTemp As Double
        Dim pPrecioOriginal As Double
        'ID = pID

        'Precio = Precio / Cantidad * (Cantidad + pCantidad)
        'Cantidad = Cantidad + pCantidad
        Dim auxPrecio As Double
        Dim auxCantidad As Double

        'ID = pID
        Comm.CommandText = "select precio from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        auxPrecio = Comm.ExecuteScalar

        Comm.CommandText = "select cantidad from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        auxCantidad = Comm.ExecuteScalar

        pPrecioOriginal = auxPrecio / auxCantidad
        'PrecioTemp = pPrecioOriginal / auxCantidad * (auxCantidad + pCantidad)
        auxPrecio = auxPrecio / auxCantidad * (auxCantidad + pCantidad)
        auxCantidad = auxCantidad + pCantidad


        Comm.CommandText = "select iva from tblventascotizacionesinventario where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        auxPrecio = auxPrecio * (1 + (IvaTemp / 100))
        If pTiporedondeo <> 0 Then
            auxPrecio = GlobalRedondea(auxPrecio, pTiporedondeo, pCantidadDecimales)
            auxPrecio = auxPrecio / (1 + (IvaTemp / 100))
        End If

        If Cantidad > 0 Then
            Comm.CommandText = "update tblventascotizacionesinventario set precio=" + auxPrecio.ToString + ",cantidad=" + auxCantidad.ToString + " where iddetalle=" + pID.ToString
            Comm.ExecuteNonQuery()
        Else
            Eliminar(pID)
            ' ID = 0
        End If
    End Sub
End Class
