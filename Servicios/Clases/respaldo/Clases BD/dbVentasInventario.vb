﻿Public Class dbVentasInventario
    Public ID As Integer
    Public Idinventario As Integer
    Public Inventario As dbInventario
    Public Producto As dbProductosVariantes
    Public Servicio As dbServicios
    Public Moneda As dbMonedas
    Public Cantidad As Double
    Public Precio As Double
    Public IdMoneda As Integer
    Public IdVenta As Integer
    Public Descripcion As String
    Public NuevoConcepto As Boolean
    Public IdAlmacen As Integer
    Public Iva As Double
    Public Extra As String
    Public Descuento As Double
    Public idVariante As Integer
    Public idServicio As Integer
    Public Surtido As Double
    Public CantidadM As Double
    Public TipoCantidadM As Integer
    Public IEPS As Double
    Public ivaRetencion As Double
    Public Predial As String
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idinventario = 0
        Cantidad = 0
        IdMoneda = 0
        Precio = 0
        IdVenta = 0
        Descripcion = ""
        IdAlmacen = 0
        Iva = 0
        Extra = ""
        Descuento = 0
        idServicio = 0
        idVariante = 0
        Surtido = 0
        'Costo = 0
        CantidadM = 0
        Predial = ""
        TipoCantidadM = 0
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
        Comm.CommandText = "select * from tblventasinventario where idventasinventario=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Precio = DReader("precio")
            Idinventario = DReader("idinventario")
            Cantidad = DReader("cantidad")
            IdMoneda = DReader("idmoneda")
            IdVenta = DReader("idventa")
            Descripcion = DReader("descripcion")
            IdAlmacen = DReader("idalmacen")
            Iva = DReader("iva")
            Extra = DReader("extra")
            Descuento = DReader("descuento")
            idVariante = DReader("idvariante")
            idServicio = DReader("idservicio")
            Surtido = DReader("surtido")
            'Costo = DReader("costo")
            CantidadM = DReader("cantidadm")
            TipoCantidadM = DReader("tipocantidadm")
            IEPS = DReader("ieps")
            ivaRetencion = DReader("IVARetenido")
            Predial = DReader("predial")
        End If
        DReader.Close()
        Inventario = New dbInventario(Idinventario, Comm.Connection)
        'If idVariante > 1 Then Producto = New dbProductosVariantes(idVariante, Comm.Connection)
        'If idVariante > 0 Then Servicio = New dbServicios(idServicio, Comm.Connection)
        Moneda = New dbMonedas(IdMoneda, Comm.Connection)
    End Sub
    Public Sub Guardar(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pidVariante As Integer, ByVal pidServicio As Integer, ByVal pSeparado As Integer, ByVal pCantidadM As Double, ByVal pTipoCantidadM As Integer, ByVal pIEPS As Double, ByVal pIvaRetenido As Double, ByVal pPredial As String)
        Dim CTemp As Double
        Dim PTemp As Double
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdVenta = pIdVenta
        Descripcion = pDescripcion
        IdAlmacen = pIdAlmacen
        Iva = pIva
        Descuento = pDescuento
        idVariante = pidVariante
        idServicio = pidServicio
        IEPS = pIEPS
        ivaRetencion = pIvaRetenido
        Surtido = 0
        'Costo = pCosto
        CantidadM = pCantidadM
        TipoCantidadM = pTipoCantidadM
        Predial = pPredial
        Dim IdTemp As Integer = 0
        pSeparado = 0
        If Idinventario <> 0 And pSeparado = 1 Then
            Comm.CommandText = "select ifnull((select idventasinventario from tblventasinventario where idventa=" + IdVenta.ToString + " and idinventario=" + Idinventario.ToString + "),0)"
            IdTemp = Comm.ExecuteScalar
        Else
            If pSeparado = 1 Or idVariante > 1 Then Idinventario = 1
        End If
        If idVariante <> 0 And pSeparado = 1 Then
            Comm.CommandText = "select ifnull((select idventasinventario from tblventasinventario where idventa=" + IdVenta.ToString + " and idvariante=" + idVariante.ToString + "),0)"
            IdTemp = Comm.ExecuteScalar
        Else
            If pSeparado = 1 Or Idinventario > 1 Then idVariante = 1
        End If
        If idServicio <> 0 And pSeparado = 1 Then
            Comm.CommandText = "select ifnull((select idventasinventario from tblventasinventario where idventa=" + IdVenta.ToString + " and idservicio=" + idServicio.ToString + "),0)"
            IdTemp = Comm.ExecuteScalar
        End If
        If IdTemp <> 0 And pSeparado = 1 Then
            Comm.CommandText = "select cantidad from tblventasinventario where idventasinventario=" + IdTemp.ToString
            CTemp = Comm.ExecuteScalar
            Comm.CommandText = "select precio from tblventasinventario where idventasinventario=" + IdTemp.ToString
            PTemp = Comm.ExecuteScalar
            If PTemp <> 0 Then
                Precio = PTemp / CTemp
            Else
                Precio = 0
            End If
            Cantidad += CTemp
            Precio = Precio * Cantidad
            Comm.CommandText = "update tblventasinventario set cantidad=" + Cantidad.ToString + ",precio=" + Precio.ToString + ",cantidadm=" + CantidadM.ToString + ",tipocantidad=" + TipoCantidadM.ToString + " where idventasinventario=" + IdTemp.ToString
            Comm.ExecuteNonQuery()
            ID = IdTemp
            LlenaDatos()
            NuevoConcepto = False
        Else
            NuevoConcepto = True
            Comm.CommandText = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm, ieps, ivaRetenido,predial) values(" + IdVenta.ToString + "," + Idinventario.ToString + "," + Cantidad.ToString + "," + Precio.ToString + ",'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + IdAlmacen.ToString + "," + Iva.ToString + ",'" + Replace(Extra, "'", "''") + "'," + Descuento.ToString + "," + idVariante.ToString + "," + idServicio.ToString + ",0," + CantidadM.ToString + "," + TipoCantidadM.ToString + "," + IEPS.ToString() + "," + ivaRetencion.ToString + ",'" + Replace(Predial, "'", "''") + "')"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select ifnull((select max(idventasinventario) from tblventasinventario),0)"
            'Comm.CommandText = "select if(max(idventasinventario) is null,0,max(idventasinventario)) from tblventasinventario"
            ID = Comm.ExecuteScalar
        End If

    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pCantidadM As Double, ByVal pTipoCantidadM As Integer, ByVal pIEPS As Double, ByVal pIVARetenido As Double, ByVal pPredial As String)
        ID = pID
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        Descripcion = pDescripcion
        Iva = pIva
        Descuento = pDescuento
        CantidadM = pCantidadM
        TipoCantidadM = pTipoCantidadM
        IEPS = pIEPS
        ivaRetencion = pIVARetenido
        Predial = pPredial
        Comm.CommandText = "update tblventasinventario set precio=" + Precio.ToString + ",idmoneda=" + IdMoneda.ToString + ",cantidad=" + Cantidad.ToString + ",descripcion='" + Replace(Descripcion, "'", "''") + "',iva=" + Iva.ToString + ",descuento=" + Descuento.ToString + ",cantidadm=" + CantidadM.ToString + ",tipocantidadm=" + TipoCantidadM.ToString + " , ieps=" + IEPS.ToString() + ", ivaRetenido=" + ivaRetencion.ToString() + ",predial='" + Replace(Predial, "'", "''") + "' where idventasinventario=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventasinventario where idventasinventario=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pIdVenta As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.idventasinventario,tblinventario.clave,tvi.descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura from tblventasinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda where tvi.idventa=" + pIdVenta.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasinventario")
        Return DS.Tables("tblventasinventario").DefaultView
    End Function
    Public Function ConsultaReader(ByVal pIdVenta As Integer, ByVal pConSeries As Boolean, ByVal pConDetalles As String, ByVal pAduana As Byte, ByVal pOrdenporUbicacion As String) As MySql.Data.MySqlClient.MySqlDataReader
        If pConDetalles = "1" Then
            If pConSeries Then
                Comm.CommandText = "select tvi.idventasinventario,tblinventario.clave,concat(tvi.descripcion,spdaseriesventa(tvi.idinventario,tvi.idventa),spdadetalleskit(tvi.idventa,tvi.idventasinventario,1,0),spdadetallesaduana(tvi.idventa,tvi.idventasinventario,0," + pAduana.ToString + "),spdadetalleslotes(tvi.idventa,tvi.idventasinventario,3," + pAduana.ToString + ")) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idinventario,tvi.cantidadm,ifnull((select abreviatura from tbltiposcantidades where idtipocantidad=tvi.tipocantidadm),tbltiposcantidades.abreviatura)  as tipom,tvi.descuento,tvi.ieps,tvi.IVARetenido,tvi.extra,tblinventario.clave2,tblinventario.ubicacion,tvi.predial,tblinventario.esrevdev from tblventasinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad where tvi.idventa=" + pIdVenta.ToString
            Else
                Comm.CommandText = "select tvi.idventasinventario,tblinventario.clave,concat(tvi.descripcion,spdadetalleskit(tvi.idventa,tvi.idventasinventario,0,0),spdadetallesaduana(tvi.idventa,tvi.idventasinventario,0," + pAduana.ToString + "),spdadetalleslotes(tvi.idventa,tvi.idventasinventario,3," + pAduana.ToString + ")) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idinventario,tvi.cantidadm,ifnull((select abreviatura from tbltiposcantidades where idtipocantidad=tvi.tipocantidadm),tbltiposcantidades.abreviatura)  as tipom,tvi.descuento,tvi.ieps,tvi.IVARetenido,tvi.extra,tblinventario.clave2,tblinventario.ubicacion,tvi.predial,tblinventario.esrevdev from tblventasinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad where tvi.idventa=" + pIdVenta.ToString
            End If
        Else
            If pConSeries Then
                Comm.CommandText = "select tvi.idventasinventario,tblinventario.clave,concat(tvi.descripcion,spdaseriesventa(tvi.idinventario,tvi.idventa),spdadetallesaduana(tvi.idventa,tvi.idventasinventario,0," + pAduana.ToString + "),spdadetalleslotes(tvi.idventa,tvi.idventasinventario,3," + pAduana.ToString + ")) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idinventario,tvi.cantidadm,ifnull((select abreviatura from tbltiposcantidades where idtipocantidad=tvi.tipocantidadm),tbltiposcantidades.abreviatura)  as tipom,tvi.descuento,tvi.ieps,tvi.IVARetenido,tvi.extra,tblinventario.clave2,tblinventario.ubicacion,tvi.predial,tblinventario.esrevdev from tblventasinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad where tvi.idventa=" + pIdVenta.ToString
            Else
                Comm.CommandText = "select tvi.idventasinventario,tblinventario.clave,concat(tvi.descripcion,spdadetallesaduana(tvi.idventa,tvi.idventasinventario,0," + pAduana.ToString + "),spdadetalleslotes(tvi.idventa,tvi.idventasinventario,3," + pAduana.ToString + ")) as descripcion,tvi.cantidad,tvi.precio,tblmonedas.abreviatura,tbltiposcantidades.abreviatura as tipocantidad,tvi.iva,tvi.idmoneda,tvi.idinventario,tvi.cantidadm,ifnull((select abreviatura from tbltiposcantidades where idtipocantidad=tvi.tipocantidadm),tbltiposcantidades.abreviatura)  as tipom,tvi.descuento,tvi.ieps,tvi.IVARetenido,tvi.extra,tblinventario.clave2,tblinventario.ubicacion,tvi.predial,tblinventario.esrevdev from tblventasinventario tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblmonedas on tvi.idmoneda=tblmonedas.idmoneda inner join tbltiposcantidades on tblinventario.tipocontenido=tbltiposcantidades.idtipocantidad where tvi.idventa=" + pIdVenta.ToString
            End If
        End If
        If pOrdenporUbicacion = "1" Then
            Comm.CommandText += " order by tblinventario.ubicacion"
        End If
        Return Comm.ExecuteReader
    End Function
    Public Sub SeparaKit(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pidVariante As Integer, ByVal pidServicio As Integer, ByVal pSeparado As Integer, ByVal pCantidadM As Double, ByVal pTipoCantidadM As Integer, ByVal pIeps As Double, ByVal pIveratenido As Double)
        Dim STR As String
        Idinventario = pIdinventario
        Cantidad = pCantidad
        Precio = pPrecio
        IdMoneda = pIdMoneda
        IdVenta = pIdVenta
        Descripcion = pDescripcion
        IdAlmacen = pIdAlmacen
        Iva = pIva
        Descuento = pDescuento
        idVariante = 1
        idServicio = pidServicio
        Surtido = 0
        'Costo = pCosto
        CantidadM = pCantidadM
        TipoCantidadM = pTipoCantidadM
        STR = "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm,ieps,ivaretenido,predial) values(" + IdVenta.ToString + "," + Idinventario.ToString + ",0,0,'" + Replace(Descripcion, "'", "''") + "'," + IdMoneda.ToString + "," + IdAlmacen.ToString + "," + Iva.ToString + ",'" + Replace(Extra, "'", "''") + "'," + Descuento.ToString + "," + idVariante.ToString + "," + idServicio.ToString + ",0,0," + TipoCantidadM.ToString + "," + pIeps.ToString + "," + pIveratenido.ToString + ",11);"
        STR += "insert into tblventasinventario(idventa,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,surtido,cantidadm,tipocantidadm,ieps,ivaretenido,predial) select " + IdVenta.ToString + ",tblinventariodetalles.idinventario,tblinventariodetalles.cantidad,(select precio from tblinventarioprecios where idinventario=tblinventariodetalles.idinventario and idlista=1 limit 1)*tblinventariodetalles.cantidad,(select nombre from tblinventario where idinventario=tblinventariodetalles.idinventario)," + IdMoneda.ToString + "," + IdAlmacen.ToString + ",(select iva from tblinventario where idinventario=tblinventariodetalles.idinventario),'" + Replace(Extra, "'", "''") + "'," + Descuento.ToString + "," + idVariante.ToString + "," + idServicio.ToString + ",0,tblinventariodetalles.cantidad,(select tipocontenido from tblinventario where idinventario=tblinventariodetalles.idinventario),(select ieps from tblinventario where idinventario=tblinventariodetalles.idinventario),(select ivaretenido from tblinventario where idinventario=tblinventariodetalles.idinventario),'' from tblinventariodetalles inner join tblinventario on tblinventario.idinventario=tblinventariodetalles.idinventariop  where tblinventariodetalles.idinventariop=" + pIdinventario.ToString + ";"
        Comm.CommandText = STR
        Comm.ExecuteNonQuery()
    End Sub
    Public Function UltomoRegistro() As Integer
        Dim id As Integer
        Comm.CommandText = "select max(idventasinventario) FROM tblventasinventario"

        id = Comm.ExecuteScalar
        Return id
        'checar si devulve el ultimo id insertado
    End Function

    Public Function BuscaridInventario(ByVal pId As Integer) As Integer
        Dim Resultado As Integer = 0
        Comm.CommandText = "select idinventario from tblventasinventario where idventasinventario=" + pId.ToString
        Resultado = Comm.ExecuteScalar
        Return Resultado
    End Function

End Class
