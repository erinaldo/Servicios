Public Class dbVentasRemisionesInventario
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    Public Servicio As dbServicios
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdRemision As Integer
    Public Descripcion As String
    Public NuevoConcepto As Boolean
    Public IdAlmacen As Integer
    Public Iva As Double
    Public Extra As String
    Public Descuento As Double
    Public IdVariante As Integer
    Public IdServicio As Integer
    Public Surtido As Double
    Public PrecioOriginal As Double
    Public IEPS As Double
    Public IVARetenido As Double
    Public CantidadM As Double
    Public TipoCantidadM As Integer
    Public CDescuento As Integer
    Public Ubicacion As String
    'Public Costo As Double
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdRemision = 0
        Descripcion = ""
        IdAlmacen = 0
        Iva = 0
        Extra = ""
        Descuento = 0
        IdVariante = 0
        IdServicio = 0
        Surtido = 0
        PrecioOriginal = 0
        TipoCantidadM = 0
        CantidadM = 0
        CDescuento = 0
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
        Comm.CommandText = "select vri.*,ifnull(vru.ubicacion,'') ubicacion from tblventasremisionesinventario vri left outer join tblventasremisionesubicaciones vru on vri.iddetalle=vru.iddetalle where vri.iddetalle=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdRemision = DReader("idremision")
            Descripcion = DReader("descripcion")
            IdAlmacen = DReader("idalmacen")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
            IdVariante = DReader("idvariante")
            IdServicio = DReader("idservicio")
            Surtido = DReader("surtido")
            PrecioOriginal = DReader("preciooriginal")
            IEPS = DReader("IEPS")
            IVARetenido = DReader("IVARetenido")
            TipoCantidadM = DReader("tipocantidadm")
            CantidadM = DReader("cantidadm")
            CDescuento = DReader("cdescuento")
            Ubicacion = DReader("ubicacion")
        End If
        DReader.Close()
        Inventario = New dbInventario(Idinventario, Comm.Connection)
        'If IdVariante > 1 Then Producto = New dbProductosVariantes(IdVariante, Comm.Connection)
        'If IdVariante > 0 Then Servicio = New dbServicios(IdServicio, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)

    End Sub
    Public Sub Guardar(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer, ByVal pIdServicio As Integer, ByVal pIEPS As Double, ByVal pIVARetenido As Double, pCantidadM As Double, pTipoCantidadM As Integer, pCDescuento As Double, pUbicacion As String)


        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdRemision = pIdVenta
        Descripcion = pDescripcion
        IdAlmacen = pIdAlmacen
        Iva = pIva
        Descuento = pDescuento
        IdVariante = pIdVariante
        IdServicio = pIdServicio
        IEPS = pIEPS
        IVARetenido = pIVARetenido
        CantidadM = pCantidadM
        CDescuento = pCDescuento
        TipoCantidadM = pTipoCantidadM
        Ubicacion = pUbicacion
        If Cantidad = 1 Then
            PrecioOriginal = Precio
        Else
            PrecioOriginal = Precio / Cantidad
        End If

        NuevoConcepto = True
        Comm.CommandText = "insert into tblventasremisionesinventario(idremision, idinventario, cantidad, precio, descripcion, idmoneda, idalmacen, iva, extra, descuento, idvariante, idservicio, surtido, preciooriginal, IEPS, IVARetenido, cantidadm, tipocantidadm, cdescuento) values (" + IdRemision.ToString + "," + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + IdAlmacen.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + ",1," + IdServicio.ToString + ",0," + Precio.ToString + " , " + IEPS.ToString() + " , " + IVARetenido.ToString() + "," + CantidadM.ToString + "," + TipoCantidadM.ToString + "," + CDescuento.ToString + ");"
        Comm.CommandText += "select ifnull(last_insert_id(),0);"
        ID = Comm.ExecuteScalar
        If pUbicacion <> "" Then
            Comm.CommandText = "insert into tblventasremisionesubicaciones (iddetalle, cantidad, surtido, ubicacion) values(" + ID.ToString + ", " + Cantidad.ToString() + ", 0, '" + Trim(Replace(Ubicacion, "'", "''")) + "');"
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIEPS As Double, ByVal pIVARetenido As Double, pCantidadM As Double, pTipocantidadM As Integer, pcDescuento As Double, pUbicacion As String)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        IEPS = pIEPS
        IVARetenido = pIVARetenido
        CantidadM = pCantidadM
        TipoCantidadM = pTipocantidadM
        CDescuento = pcDescuento
        If Cantidad = 1 Then
            PrecioOriginal = Precio
        Else
            PrecioOriginal = Precio / Cantidad
        End If
        Comm.CommandText = "update tblventasremisionesinventario set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + ",preciooriginal=" + Precio.ToString + " ,IEPS=" + IEPS.ToString() + " ,IVARetenido=" + IVARetenido.ToString() + ",cantidadm=" + CantidadM.ToString + ",tipocantidadm=" + TipoCantidadM.ToString + ",cdescuento=" + CDescuento.ToString + " where iddetalle=" + ID.ToString
        Comm.ExecuteNonQuery()
        If pUbicacion <> "" Then
            Comm.CommandText = "update tblventasremisionesubicaciones set cantidad=" + pCantidad.ToString + ", ubicacion='" + Trim(Replace(pUbicacion, "'", "''")) + "' where iddetalle=" + ID.ToString
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub AgregarCantidad(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTiporedondeo As Byte, ByVal pCantidadDecimales As Byte)
        Dim PrecioTemp As Double
        Dim IvaTemp As Double
        ID = pID
        'PrecioTemp = PrecioOriginal / Cantidad * (Cantidad + pCantidad)
        Comm.CommandText = "select precio from tblventasremisionesinventario where iddetalle=" + pID.ToString
        Precio = Comm.ExecuteScalar

        Comm.CommandText = "select cantidad from tblventasremisionesinventario where iddetalle=" + pID.ToString
        Cantidad = Comm.ExecuteScalar

        Precio = Precio / Cantidad * (Cantidad + pCantidad)
        Cantidad = Cantidad + pCantidad
        Comm.CommandText = "select iva from tblventasremisionesinventario where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        PrecioTemp = PrecioOriginal * (1 + (IvaTemp / 100))
        If pTiporedondeo <> 0 Then
            PrecioTemp = GlobalRedondea(PrecioTemp * Cantidad, pTiporedondeo, pCantidadDecimales)
            Precio = PrecioTemp / (1 + (IvaTemp / 100))
        End If

        If Cantidad > 0 Then
            Comm.CommandText = "update tblventasremisionesinventario set precio=" + Precio.ToString + ",cantidad=" + Cantidad.ToString + ",cantidadm=" + Cantidad.ToString + " where iddetalle=" + ID.ToString
            Comm.ExecuteNonQuery()
        Else
            Try
                AddError("PV: cambió cantidad:" + pCantidad.ToString, "Punto de venta cambio cantidad eliminar.", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), ID.ToString)
            Catch ex As Exception

            End Try
            Eliminar(ID)
            ID = 0
        End If
    End Sub
    Public Sub AsignaCantidad(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte)
        Dim IvaTemp As Double
        Dim PrecioTemp As Double
        ID = pID
        Comm.CommandText = "select precio from tblventasremisionesinventario where iddetalle=" + pID.ToString
        Precio = Comm.ExecuteScalar

        Comm.CommandText = "select cantidad from tblventasremisionesinventario where iddetalle=" + pID.ToString
        Cantidad = Comm.ExecuteScalar
        Precio = Precio / Cantidad
        'PrecioOriginal = PrecioOriginal / Cantidad
        Cantidad = pCantidad
        Precio = Precio * Cantidad
        PrecioTemp = PrecioOriginal * Cantidad
        Comm.CommandText = "select iva from tblventasremisionesinventario where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        PrecioTemp = PrecioTemp * (1 + (IvaTemp / 100))
        If pTipoRedondeo <> 0 Then
            PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
            Precio = PrecioTemp / (1 + (IvaTemp / 100))
        End If

        If Cantidad > 0 Then
            Comm.CommandText = "update tblventasremisionesinventario set precio=" + Precio.ToString + ",cantidad=" + Cantidad.ToString + ",cantidadm=" + Cantidad.ToString + " where iddetalle=" + ID.ToString
            Comm.ExecuteNonQuery()
        Else
            Try
                AddError("PV: Asignó cantidad:" + pCantidad.ToString, "Punto de venta Asignar cantidad eliminar.", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), ID.ToString)
            Catch ex As Exception

            End Try
            Eliminar(ID)
            ID = 0
        End If
    End Sub
    Public Sub CambiaPrecio(ByVal pId As Integer, ByVal pPrecio As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte, ByVal pDescuento As Double)
        If pPrecio <> 0 Then
            Dim CTEmp As Double
            Comm.CommandText = "select cantidad from tblventasremisionesinventario where iddetalle=" + pId.ToString
            CTEmp = Comm.ExecuteScalar
            Precio = pPrecio * CTEmp

            Dim PrecioTemp As Double
            Dim IvaTemp As Double
            Comm.CommandText = "select iva from tblventasremisionesinventario where iddetalle=" + pId.ToString
            IvaTemp = Comm.ExecuteScalar
            PrecioTemp = Precio * (1 + (IvaTemp / 100))
            If pTipoRedondeo <> 0 Then
                PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
                Precio = PrecioTemp / (1 + (IvaTemp / 100))
            End If
            'PrecioOriginal = Precio / CTEmp
            If pDescuento = 0 Then
                Comm.CommandText = "update tblventasremisionesinventario set precio=" + Precio.ToString + " where iddetalle=" + pId.ToString
            Else
                Comm.CommandText = "update tblventasremisionesinventario set precio=" + Precio.ToString + ",descuento=" + pDescuento.ToString + " where iddetalle=" + pId.ToString
            End If
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventasremisionesinventario where iddetalle=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura from tblventasremisionesinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idremision=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasremisionesinventario")
        Return DS.Tables("tblventasremisionesinventario").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdVenta As Integer, Pnada As Byte, pconserires As Byte, pConDetalles As String) As MySql.Data.MySqlClient.MySqlDataReader
        If pConDetalles = "1" Then
            Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,concat(convert(tvi.descripcion using utf8),spdadetalleslotes(tvi.idremision,tvi.iddetalle,2," + Pnada.ToString + "),spdadetallesaduanaotros(tvi.idremision,tvi.iddetalle,2," + Pnada.ToString + "),spdaseriesremisiones(tvi.idinventario,tvi.idremision," + pconserires.ToString + "),spdadetalleskit(tvi.idremision,tvi.iddetalle,1," + pconserires.ToString + ")) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tblproductos.clave as pclave,tvi.idinventario,tvi.idvariante,tvi.idservicio,tvi.descuento,tvi.IEPS,tvi.IVARetenido,tvi.iva,tvi.cantidadm,ifnull((select abreviatura from tbltiposcantidades where idtipocantidad=tvi.tipocantidadm),tbltiposcantidades.abreviatura)  as tipom,tblinventario.clave2 from tblventasremisionesinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad inner join tblproductosvariantes on tblproductosvariantes.idvariante=tvi.idvariante inner join tblproductos on tblproductos.idproducto=tblproductosvariantes.idproducto where tvi.idremision=" + pIdVenta.ToString
        Else
            Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,concat(convert(tvi.descripcion using utf8),spdadetalleslotes(tvi.idremision,tvi.iddetalle,2," + Pnada.ToString + "),spdadetallesaduanaotros(tvi.idremision,tvi.iddetalle,2," + Pnada.ToString + "),spdaseriesremisiones(tvi.idinventario,tvi.idremision," + pconserires.ToString + ")) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tblproductos.clave as pclave,tvi.idinventario,tvi.idvariante,tvi.idservicio,tvi.descuento,tvi.IEPS,tvi.IVARetenido,tvi.iva,tvi.cantidadm,ifnull((select abreviatura from tbltiposcantidades where idtipocantidad=tvi.tipocantidadm),tbltiposcantidades.abreviatura)  as tipom,tblinventario.clave2 from tblventasremisionesinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad inner join tblproductosvariantes on tblproductosvariantes.idvariante=tvi.idvariante inner join tblproductos on tblproductos.idproducto=tblproductosvariantes.idproducto where tvi.idremision=" + pIdVenta.ToString
        End If

        Return Comm.ExecuteReader
    End Function
    Public Function ConsultaReaderIVa(ByVal pIdVenta As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio*(1+(tvi.iva+tvi.ieps-tvi.ivaretenido)/100) as precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tblproductos.clave as pclave,tvi.idinventario,tvi.idvariante,tvi.idservicio,tvi.descuento,tvi.iva,tvi.cantidadm,ifnull((select abreviatura from tbltiposcantidades where idtipocantidad=tvi.tipocantidadm),tbltiposcantidades.abreviatura)  as tipom from tblventasremisionesinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad inner join tblproductosvariantes on tblproductosvariantes.idvariante=tvi.idvariante inner join tblproductos on tblproductos.idproducto=tblproductosvariantes.idproducto where tvi.idremision=" + pIdVenta.ToString
        Return Comm.ExecuteReader
    End Function
    Public Sub SeparaKit(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer, ByVal pIdServicio As Integer, ByVal pIeps As Double, ByVal pivaretenido As Double, pCantidadM As Double, pTipoCantidadM As Integer, pcDescuento As Double)
        Dim STR As String
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdRemision = pIdVenta
        Descripcion = pDescripcion
        IdAlmacen = pIdAlmacen
        Iva = pIva
        Descuento = pDescuento
        IdVariante = 1
        IdServicio = pIdServicio
        Surtido = 0
        'Costo = pCosto
        CantidadM = pCantidadM
        TipoCantidadM = pTipoCantidadM
        STR = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal,ieps,ivaretenido,cantidadm,tipocantidadm,cdescuento) values(" + IdRemision.ToString + "," + Idinventario.ToString + ",0,0,'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + IdAlmacen.ToString + "," + Iva.ToString + ",''," + Descuento.ToString + "," + IdVariante.ToString + "," + IdServicio.ToString + ",0," + Precio.ToString + "," + pIeps.ToString + "," + pivaretenido.ToString + ",0," + TipoCantidadM.ToString + ",0);"
        STR += "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal,ieps,ivaretenido,cantidadm,tipocantidadm,cdescuento) select " + IdRemision.ToString + ",tblinventariodetalles.idinventario,tblinventariodetalles.cantidad,(select precio from tblinventarioprecios where idinventario=tblinventariodetalles.idinventario and idlista=1 limit 1)*tblinventariodetalles.cantidad,(select nombre from tblinventario where idinventario=tblinventariodetalles.idinventario)," + IdMoneda.ToString + "," + IdAlmacen.ToString + ",(select iva from tblinventario where idinventario=tblinventariodetalles.idinventario),''," + Descuento.ToString + "," + IdVariante.ToString + "," + IdServicio.ToString + ",0,(select precio from tblinventarioprecios where idinventario=tblinventariodetalles.idinventario and idlista=1 limit 1),(select ieps from tblinventario where idinventario=tblinventariodetalles.idinventario),(select ivaretenido from tblinventario where idinventario=tblinventariodetalles.idinventario),tblinventariodetalles.cantidad,(select tipocontenido from tblinventario where idinventario=tblinventariodetalles.idinventario)," + pcDescuento.ToString + " from tblinventariodetalles inner join tblinventario on tblinventario.idinventario=tblinventariodetalles.idinventariop  where tblinventariodetalles.idinventariop=" + pIdinventario.ToString + ";"
        Comm.CommandText = STR
        Comm.ExecuteNonQuery()
    End Sub
    Public Function BuscaridInventario(ByVal pId As Integer) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select idinventario from tblventasremisionesinventario where iddetalle=" + pId.ToString
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function
    Public Function UltomoRegistro() As Integer
        Dim id As Integer
        Comm.CommandText = "select max(iddetalle) FROM tblventasremisionesinventario"

        id = Comm.ExecuteScalar
        Return id
        'checar si devulve el ultimo id insertado
    End Function

    'guardar descuentos
    Public Sub GuardarDescuento(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer, ByVal pIdServicio As Integer, ByVal pIEPS As Double, ByVal pIVARetenido As Double, pTipoCantidad As Integer)

        'Dim CTemp As Double
        'Dim PTemp As Double
        Dim pPrecioOriginal As Double
        ' Dim auxID As Integer
        'Idinventario = pIdinventario
        'Cantidad = pCantidad
        'Precio = pPrecio
        'IdMoneda = pIdMoneda
        'IdRemision = pIdVenta
        'Descripcion = pDescripcion
        'IdAlmacen = pIdAlmacen
        'Iva = pIva
        'Descuento = pDescuento
        'IdVariante = pIdVariante
        'IdServicio = pIdServicio
        If pCantidad = 1 Then
            pPrecioOriginal = pPrecio
        Else
            pPrecioOriginal = pPrecio / pCantidad
        End If
        'Costo = pCosto
        'Dim IdTemp As Integer
        'If pIdinventario <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblventasremisionesinventario where idremision=" + IdRemision.ToString + " and idinventario=" + pIdinventario.ToString + " limit 1),0)"
        '    IdTemp = Comm.ExecuteScalar
        'Else
        '    pIdinventario = 1
        'End If
        'If pIdVariante <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblventasremisionesinventario where idremision=" + IdRemision.ToString + " and idvariante=" + pIdVariante.ToString + " limit 1),0)"
        '    IdTemp = Comm.ExecuteScalar
        'Else
        '    pIdVariante = 1
        'End If
        'If pIdServicio <> 0 Then
        '    Comm.CommandText = "select ifnull((select iddetalle from tblventasremisionesinventario where idremision=" + IdRemision.ToString + " and idservicio=" + pIdServicio.ToString + " limit 1),0)"
        '    IdTemp = Comm.ExecuteScalar
        'End If
        'IdTemp = 0
        'If IdTemp <> 0 Then
        '    Comm.CommandText = "select cantidad from tblventasremisionesinventario where iddetalle=" + IdTemp.ToString
        '    CTemp = Comm.ExecuteScalar
        '    Comm.CommandText = "select precio from tblventasremisionesinventario where iddetalle=" + IdTemp.ToString
        '    PTemp = Comm.ExecuteScalar
        '    If PTemp <> 0 Then
        '        pPrecio = PTemp / CTemp
        '    Else
        '        pPrecio = 0
        '    End If
        '    pCantidad += CTemp
        '    pPrecio = pPrecio * pCantidad
        '    Comm.CommandText = "update tblventasremisionesinventario set cantidad=" + pCantidad.ToString + ",precio=" + pPrecio.ToString + " where iddetalle=" + IdTemp.ToString
        '    Comm.ExecuteNonQuery()
        '    auxID = ID
        '    ID = IdTemp
        '    LlenaDatos()
        '    ID = auxID
        '    NuevoConcepto = False
        'Else
        '    NuevoConcepto = True
        '    If Idinventario <> 1 Then
        '        Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal) values(" + IdRemision.ToString + "," + pIdinventario.ToString + "," + pCantidad.ToString + "," + pPrecio.ToString + ",'" + Replace(pDescripcion, "'", "''") + "'," + pIdMoneda.ToString + "," + pIdAlmacen.ToString + "," + pIva.ToString + ",''," + pDescuento.ToString + "," + pIdVariante.ToString + "," + pIdServicio.ToString + ",0," + pPrecio.ToString + ");"
        '        'Comm.ExecuteNonQuery()

        '        Comm.CommandText += "select ifnull((select max(iddetalle) from tblventasremisionesinventario),0)"
        '        auxID = Comm.ExecuteScalar()
        '    Else
        Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal,IEPS, IVARetenido,cantidadm,tipocantidadm) values(" + pIdVenta.ToString + "," + pIdinventario.ToString + "," + pCantidad.ToString + "," + pPrecio.ToString + ",'" + Replace(pDescripcion, "'", "''") + "'," + pIdMoneda.ToString + "," + pIdAlmacen.ToString + "," + pIva.ToString + ",''," + pDescuento.ToString + "," + pIdVariante.ToString + "," + pIdServicio.ToString + ",0," + pPrecioOriginal.ToString + "," + pIEPS.ToString() + "," + pIVARetenido.ToString + "," + pCantidad.ToString + "," + pTipoCantidad.ToString + ");"
        ' Comm.CommandText = "insert into tblventasremisionesinventario(idremision,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,preciooriginal) values(" + IdRemision.ToString + "," + pIdinventario.ToString + "," + pCantidad.ToString + "," + pPrecio.ToString + ",'" + Replace(pDescripcion, "'", "''") + "'," + pIdMoneda.ToString + "," + pIdAlmacen.ToString + "," + pIva.ToString + "," + Descuento.ToString + "," + pIdVariante.ToString + "," + pIdServicio.ToString + ",0," + pPrecio.ToString + ");"
        Comm.ExecuteNonQuery()

        'End If

        'End If


    End Sub

    Public Sub AgregarCantidadDescuento(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTiporedondeo As Byte, ByVal pCantidadDecimales As Byte, ByVal pPrecioTot As Double)
        Dim PrecioTemp As Double
        Dim IvaTemp As Double
        Dim auxPrecio As Double
        Dim auxCantidad As Double
        'ID = pID
        'PrecioTemp = PrecioOriginal / Cantidad * (Cantidad + pCantidad)
        'Precio = Precio / Cantidad * (Cantidad + pCantidad)

        Comm.CommandText = "select precio from tblventasremisionesinventario where iddetalle=" + pID.ToString
        auxPrecio = Comm.ExecuteScalar

        Comm.CommandText = "select cantidad from tblventasremisionesinventario where iddetalle=" + pID.ToString
        auxCantidad = Comm.ExecuteScalar


        auxPrecio = auxPrecio / auxCantidad * (auxCantidad + pCantidad)
        auxCantidad = auxCantidad + pCantidad
        Comm.CommandText = "select iva from tblventasremisionesinventario where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        PrecioTemp = auxPrecio * (1 + (IvaTemp / 100))
        'If pTiporedondeo <> 0 Then
        '    PrecioTemp = GlobalRedondea(PrecioTemp * Cantidad, pTiporedondeo, pCantidadDecimales)
        '    Precio = PrecioTemp / (1 + (IvaTemp / 100))
        'End If

        If auxCantidad > 0 Then
            Comm.CommandText = "update tblventasremisionesinventario set precio=" + auxPrecio.ToString + ",cantidad=" + auxCantidad.ToString + " where iddetalle=" + pID.ToString
            Comm.ExecuteNonQuery()
        Else
            Eliminar(pID)
            'ID = 0
        End If
    End Sub

    Public Sub AsignaCantidadDescuento(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte)
        Dim IvaTemp As Double
        Dim PrecioTemp As Double
        Dim auxPrecio As Double
        Dim auxCantidad As Double
        Dim pPrecioOriginal As Double
        Comm.CommandText = "select precio from tblventasremisionesinventario where iddetalle=" + pID.ToString
        auxPrecio = Comm.ExecuteScalar

        Comm.CommandText = "select cantidad from tblventasremisionesinventario where iddetalle=" + pID.ToString
        auxCantidad = Comm.ExecuteScalar

        auxPrecio = auxPrecio / auxCantidad
        pPrecioOriginal = auxPrecio
        auxCantidad = pCantidad
        auxPrecio = auxPrecio * auxCantidad
        'Precio = Precio / Cantidad
        'PrecioOriginal = PrecioOriginal / Cantidad
        'Cantidad = pCantidad
        'Precio = Precio * Cantidad
        PrecioTemp = pPrecioOriginal * pCantidad
        Comm.CommandText = "select iva from tblventasremisionesinventario where iddetalle=" + pID.ToString
        IvaTemp = Comm.ExecuteScalar
        PrecioTemp = PrecioTemp * (1 + (IvaTemp / 100))
        If pTipoRedondeo <> 0 Then
            PrecioTemp = GlobalRedondea(PrecioTemp, pTipoRedondeo, pCantidadDecimales)
            auxPrecio = PrecioTemp / (1 + (IvaTemp / 100))
        End If

        If pCantidad > 0 Then
            Comm.CommandText = "update tblventasremisionesinventario set precio=" + auxPrecio.ToString + ",cantidad=" + auxCantidad.ToString + " where iddetalle=" + pID.ToString
            Comm.ExecuteNonQuery()
        Else
            Eliminar(pID)
            ' ID = 0
        End If
    End Sub
    Public Function DaAlmacen(ByVal pIdRemision As Integer) As String
        Dim Str As String
        Dim IdAlmacenTemp As Integer
        Comm.CommandText = "select ifnull((select idalmacen from tblventasremisionesinventario where idremision=" + pIdRemision.ToString + " limit 1),0)"
        IdAlmacenTemp = Comm.ExecuteScalar
        Comm.CommandText = "select ifnull((select nombre from tblalmacenes where idalmacen=" + IdAlmacenTemp.ToString + "),'No almacen')"
        Str = Comm.ExecuteScalar
        Return Str
    End Function
End Class
