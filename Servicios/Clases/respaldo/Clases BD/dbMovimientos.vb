Public Class dbMovimientos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdConcepto As Integer
    Public Fecha As String
    Public Concepto As dbInventarioConceptos
    Public Folio As Integer
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    Public Estado As Byte
    Public Desglosar As Byte
    Public IdSucursal As Integer
    Public Serie As String
    Public Comentario As String
    'Public TotalISR As Double
    Public Subtotal As Double
    'Public TotalIva As Double
    'Public TotalIvaRetenido As Double
    Public TotalVenta As Double

    Public HoraCancelado As String
    Public FechaCancelado As String
    Public TipodeCambio As Double
    Public IdMoneda As Integer
    Public IdVenta As Integer
    Public idRemision As Integer
    Public FolioRef As String
    Public ClienteRef As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdConcepto = -1
        Fecha = ""
        Hora = ""
        Folio = 0
        Desglosar = 0

        TotalaPagar = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        Serie = ""
        HoraCancelado = ""
        FechaCancelado = ""
        Comm.Connection = Conexion
        Concepto = New dbInventarioConceptos(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblmovimientos where idmovimiento=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdConcepto = DReader("idconcepto")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            'Desglosar = DReader("desglosar")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            Serie = DReader("serie")
            HoraCancelado = DReader("horacancelado")
            FechaCancelado = DReader("fechacancelado")
            Comentario = DReader("comentario")
            TipodeCambio = DReader("tipodecambio")
            IdMoneda = DReader("idmoneda")
            IdVenta = DReader("idventa")
            idRemision = DReader("idremision")
        End If
        DReader.Close()
        If IdVenta <> 0 Then
            Comm.CommandText = "select ifnull((select concat(serie,convert(folio using utf8)) from tblventas where idventa=" + IdVenta.ToString + "),'')"
            FolioRef = "Factura: " + Comm.ExecuteScalar
            Comm.CommandText = "select ifnull((select nombre from tblclientes where idcliente=(select idcliente from tblventas where idventa=" + IdVenta.ToString + ")),'')"
            ClienteRef = Comm.ExecuteScalar
        End If
        If idRemision <> 0 Then
            Comm.CommandText = "select ifnull((select concat(serie,convert(folio using utf8)) from tblventasremisiones where idremision=" + idRemision.ToString + "),'')"
            FolioRef = "Remisión: " + Comm.ExecuteScalar
            Comm.CommandText = "select ifnull((select nombre from tblclientes where idcliente=(select idcliente from tblventasremisiones where idremision=" + idRemision.ToString + ")),'')"
            ClienteRef = Comm.ExecuteScalar
        End If
        Concepto = New dbInventarioConceptos(IdConcepto, Comm.Connection)
    End Sub

    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer, ByVal pIdConcepto As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblmovimientos where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado>=2) and idsucursal=" + pidSucursal.ToString + " and idconcepto=" + pIdConcepto.ToString + "),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String, ByVal pidSucursal As Integer, ByVal pIdConcepto As Integer) As Boolean
        Dim Resultado As Integer = 0

        Comm.CommandText = "select count(folio) from tblmovimientos where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2 and idsucursal=" + pidSucursal.ToString + " and idconcepto=" + pIdConcepto.ToString
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    Public Sub Guardar(ByVal pFolio As Integer, ByVal pFecha As String, ByVal pIdConcepto As Integer, ByVal pSerie As String, ByVal pidSucursal As Integer, ByVal pTipodeCambio As Double, ByVal pIdMoneda As Integer)
        Folio = pFolio
        Fecha = pFecha
        IdConcepto = pIdConcepto
        Serie = pSerie
        IdSucursal = pidSucursal
        TipodeCambio = pTipodeCambio
        IdMoneda = pIdMoneda
        Comm.CommandText = "insert into tblmovimientos(folio,fecha,estado,idconcepto,comentario,serie,idsucursal,total,totalapagar,fechacancelado,horacancelado,hora,tipodecambio,idmoneda,idventa,idremision) values(" + _
        Folio.ToString + ",'" + Fecha + "',1," + IdConcepto.ToString + ",'','" + Replace(Trim(Serie), "'", "''") + "'," + IdSucursal.ToString + ",0,0,'" + Fecha + "','" + Format(TimeOfDay, "HH:mm:ss") + "','" + Format(TimeOfDay, "HH:mm:ss") + "'," + TipodeCambio.ToString + "," + IdMoneda.ToString + ",0,0)"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idmovimiento) from tblmovimientos"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFolio As Integer, ByVal pEstado As Byte, ByVal pComentario As String, ByVal pSerie As String, ByVal pTotal As Double, ByVal pTotalaPagar As Double, ByVal pTipodeCambio As Double, ByVal pidMoneda As Integer, ByVal pfecha As String, ByVal pIdVenta As Integer, ByVal pIdRemision As Integer)
        Folio = pFolio
        Estado = pEstado
        Comentario = pComentario
        Serie = pSerie
        Total = pTotal
        TotalaPagar = pTotalaPagar
        TipodeCambio = pTipodeCambio
        IdMoneda = pidMoneda
        Fecha = pfecha
        IdVenta = pIdVenta
        idRemision = pIdRemision
        Comm.CommandText = "update tblmovimientos set fecha='" + Fecha + "',folio=" + Folio.ToString + ",estado=" + Estado.ToString + ",comentario='" + Trim(Replace(Comentario, "'", "''")) + "',serie='" + Replace(Trim(Serie), "'", "''") + "',total=" + Total.ToString + ",totalapagar=" + TotalaPagar.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',tipodecambio=" + TipodeCambio.ToString + ",idmoneda=" + IdMoneda.ToString + ",idventa=" + pIdVenta.ToString + ",idremision=" + pIdRemision.ToString + " where idmovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer, ByVal pTipoCosteo As Byte, ByVal pTipodeCambio As Double, ByVal pCosteoTiempoReal As Byte)
        RegresaInventario(pID, pTipoCosteo, pTipodeCambio, pCosteoTiempoReal)
        Comm.CommandText = "delete from tblmovimientos where idmovimiento=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub QuitarBoletas(ByVal pidMovimiento As Integer)
        Dim Ids As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iddetalle from tblmovimientosdetalles where idmovimiento=" + pidMovimiento.ToString
        DR = Comm.ExecuteReader
        While DR.Read
            Ids.Add(DR("iddetalle"))
        End While
        DR.Close()
        For Each Idd As Integer In Ids
            Comm.CommandText = "delete from tblsemillasboletas where iddetalle=" + Idd.ToString
            Comm.ExecuteNonQuery()
        Next
    End Sub
    Public Function TieneBoletas(pIdMovimiento As Integer) As Boolean
        Dim Ids As New Collection
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim Sihay As Boolean = False
        Comm.CommandText = "select iddetalle from tblmovimientosdetalles where idmovimiento=" + pIdMovimiento.ToString
        DR = Comm.ExecuteReader
        While DR.Read
            Ids.Add(DR("iddetalle"))
        End While
        DR.Close()
        For Each Idd As Integer In Ids
            Comm.CommandText = "select id from tblsemillasboletas where iddetalle=" + Idd.ToString
            DR = Comm.ExecuteReader
            If DR.Read Then
                Sihay = True
            End If
            DR.Close()
        Next
        Return Sihay
    End Function
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pEstado As Byte, ByVal pFolio As String, ByVal pIdConcepto As Integer, ByVal pidSucursal As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblmovimientos.idmovimiento,tblmovimientos.fecha,tblmovimientos.serie,tblmovimientos.folio,tblinventarioconceptos.nombre,case tblmovimientos.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado  from tblmovimientos inner join tblinventarioconceptos on tblmovimientos.idconcepto=tblinventarioconceptos.idconcepto where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblmovimientos.serie,convert(tblmovimientos.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado > 1 Then
            Comm.CommandText += " and tblmovimientos.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblmovimientos.estado<>1"
        End If
        If pIdConcepto > 0 Then
            Comm.CommandText += " and tblmovimientos.idconcepto=" + pIdConcepto.ToString
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblmovimientos.idsucursal=" + pidSucursal.ToString
        End If
        Comm.CommandText += " order by tblmovimientos.fecha,tblmovimientos.serie,tblmovimientos.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblmovimientos")
        Return DS.Tables("tblmovimientos").DefaultView
    End Function
    'Public Function DaIvas(ByVal pIdCotizacion As Integer) As MySql.Data.MySqlClient.MySqlDataReader
    '    Comm.CommandText = "select iva,precio,idmoneda from tblventascotizacionesinventario where idcotizacion=" + pIdCotizacion.ToString
    '    Return Comm.ExecuteReader
    'End Function

    Public Function DaTotal(ByVal pidMovimiento As Integer, ByVal pidMoneda As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0
        'Dim Encontro As Double
        'Dim iIva As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double = 1
        'Dim iIsr As Double = 0
        'Dim iIvaRetenido As Double = 0
        Subtotal = 0
        TotalVenta = 0
        Comm.CommandText = "select tipodecambio from tblmovimientos where idmovimiento=" + pidMovimiento.ToString
        iTipoCambio = Comm.ExecuteScalar
        'Comm.CommandText = "select isr from tblventas where idventa=" + pidVenta.ToString
        'iIsr = Comm.ExecuteScalar
        'Comm.CommandText = "select ivaretenido from tblventas where idventa=" + pidVenta.ToString
        'iIvaRetenido = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tblmovimientosdetalles where idmovimiento=" + pidMovimiento.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblmovimientosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            'Comm.CommandText = "select iva from tblmovimientosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            'iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblmovimientosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            If pidMoneda = 2 Then
                If pidMoneda <> IdMonedaC Then
                    Precio = Precio * iTipoCambio
                End If
            Else
                If IdMonedaC = 2 Then
                    Precio = Precio / iTipoCambio
                End If
            End If
            Subtotal += Precio
            'TotalIva += (Precio * (iIva / 100))
            Cont += 1
        End While

        'TotalISR = Subtotal * (iIsr / 100)
        'TotalIvaRetenido = Subtotal * (iIvaRetenido / 100)
        TotalVenta = Subtotal '+ TotalIva - TotalISR - TotalIvaRetenido
        Return TotalVenta
    End Function

    Public Sub AgregarDetallesReferencia(ByVal PidMovimiento As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte)
        '0 cotizacion
        '1 pedido
        '2 remision
        '3 ventas

        If Tipo = 2 Then
            Comm.CommandText = "insert into tblmovimientosdetalles(idinventario,cantidad,precio,idmovimiento,descripcion,idalmacen,idalmacen2,idvariante,surtido,inventarioanterior,idmoneda) " + _
             "select tblventasremisionesinventario.idinventario," + _
             "sum(tblventasremisionesinventario.cantidad)-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=tblventasremisionesinventario.idremision and tblmovimientosdetalles.idinventario=tblventasremisionesinventario.idinventario and tblmovimientos.estado=3),0))," + _
             "tblventasremisionesinventario.precio/tblventasremisionesinventario.cantidad*(tblventasremisionesinventario.cantidad-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=tblventasremisionesinventario.idremision and tblmovimientosdetalles.idinventario=tblventasremisionesinventario.idinventario and tblmovimientos.estado=3),0)))," + _
             PidMovimiento.ToString + "," + _
             "tblventasremisionesinventario.descripcion,tblventasremisionesinventario.idalmacen,tblventasremisionesinventario.idalmacen,tblventasremisionesinventario.idvariante,0,0,tblventasremisionesinventario.idmoneda from tblventasremisionesinventario inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario where tblinventario.inventariable=1 and idremision=" + pIdDocumento.ToString + " and (ifnull((select sum(vi.cantidad) from tblventasremisionesinventario vi where vi.idremision=" + pIdDocumento.ToString + " and vi.idinventario=tblventasremisionesinventario.idinventario),0)-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idremision=tblventasremisionesinventario.idremision and tblmovimientosdetalles.idinventario=tblventasremisionesinventario.idinventario and tblmovimientos.estado=3),0)))>0 group by tblventasremisionesinventario.idinventario"
            Comm.ExecuteNonQuery()
        End If
        If Tipo = 3 Then
            '(CDbl(TextBox5.Text) * Equivalenciab) / Equivalencia
            Comm.CommandText = "insert into tblmovimientosdetalles(idinventario,cantidad,precio,idmovimiento,descripcion,idalmacen,idalmacen2,idvariante,surtido,inventarioanterior,idmoneda) " + _
             "select tblventasinventario.idinventario," + _
             "sum(tblventasinventario.cantidad)-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=tblventasinventario.idventa and tblmovimientosdetalles.idinventario=tblventasinventario.idinventario and tblmovimientos.estado=3),0))," + _
             "tblventasinventario.precio/tblventasinventario.cantidad*(tblventasinventario.cantidad-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=tblventasinventario.idventa and tblmovimientosdetalles.idinventario=tblventasinventario.idinventario and tblmovimientos.estado=3),0)))," + _
             PidMovimiento.ToString + "," + _
             "tblventasinventario.descripcion,tblventasinventario.idalmacen,tblventasinventario.idalmacen,tblventasinventario.idvariante,0,0,tblventasinventario.idmoneda from tblventasinventario inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario where tblinventario.inventariable=1 and idventa=" + pIdDocumento.ToString + " and (ifnull((select sum(vi.cantidad) from tblventasinventario vi where vi.idventa=tblventasinventario.idventa and vi.idinventario=tblventasinventario.idinventario),0)-(ifnull((select sum(tblmovimientosdetalles.cantidad) from tblmovimientosdetalles inner join tblmovimientos on tblmovimientosdetalles.idmovimiento=tblmovimientos.idmovimiento where tblmovimientos.idventa=" + pIdDocumento.ToString + " and tblmovimientosdetalles.idinventario=tblventasinventario.idinventario and tblmovimientos.estado=3),0)))>0 group by tblventasinventario.idinventario"
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tbldevolucionesdetalles(iddevolucion,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,cantidadm,tipocantidadm,equivalencia,equivalenciab) select " + PidMovimiento.ToString + ",tblventasinventario.idinventario,tblventasinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idventa=tblventasinventario.idventa and tbldevolucionesdetalles.idinventario=tblventasinventario.idinventario),0)),tblventasinventario.precio/tblventasinventario.cantidad*(tblventasinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idventa=tblventasinventario.idventa and tbldevolucionesdetalles.idinventario=tblventasinventario.idinventario),0))),tblventasinventario.descripcion,tblventasinventario.idmoneda,tblventasinventario.idalmacen,tblventasinventario.iva,tblventasinventario.extra,tblventasinventario.descuento,tblventasinventario.idvariante,tblventasinventario.idservicio," + _
            ' "(tblventasinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idventa=tblventasinventario.idventa and tbldevolucionesdetalles.idinventario=tblventasinventario.idinventario),0)))*tblventasinventario.cantidadm/tblventasinventario.cantidad," + _
            ' "tblventasinventario.tipocantidadm,tblventasinventario.cantidad,tblventasinventario.cantidadm from tblventasinventario inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario where tblinventario.inventariable=1 and idventa=" + pIdDocumento.ToString + " and (tblventasinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idventa=tblventasinventario.idventa and tbldevolucionesdetalles.idinventario=tblventasinventario.idinventario),0)))>0"


            'Comm.CommandText = "insert into tbldevolucionesproductos(iddevolucion,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + Piddevolucion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento from tbldevolucionesproductos where iddevolucion=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tbldevolucionesservicios(iddevolucion,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + Piddevolucion.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tbldevolucionesservicios where iddevolucion=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Sub RegresaInventario(ByVal pId As Integer, ByVal pTipoCosteo As Byte, ByVal PTipodeCambio As Double, ByVal pCosteoTiempoREal As Byte)
        Dim Tipo As Integer
        Comm.CommandText = "select tblinventarioconceptos.tipo from tblinventarioconceptos inner join tblmovimientos on tblinventarioconceptos.idconcepto=tblmovimientos.idconcepto where tblmovimientos.idmovimiento=" + pId.ToString
        Tipo = Comm.ExecuteScalar
        If Tipo = dbInventarioConceptos.Tipos.Entrada Or Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,surtido,0,1,1),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen,cantidad-surtido,0,1),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
            'If pCosteoTiempoREal = 1 Then
            'Comm.CommandText = "update tblinventario inner join tblmovimientosdetalles on tblinventario.idinventario=tblmovimientosdetalles.idinventario set tblinventario.costobase=spsacacostoarticulo(tblinventario.idinventario,1,tblinventario.contenido," + PTipodeCambio.ToString + "," + pTipoCosteo.ToString + ") where tblmovimientosdetalles.idmovimiento=" + pId.ToString + " and tblmovimientosdetalles.idinventario>1"
            'Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto inner join tblmovimientosdetalles on tblproductosvariantes.idvariante=tblmovimientosdetalles.idvariante set tblproductos.costo=spsacacostoproducto(tblproductos.idproducto," + pTipoCosteo.ToString + ") where tblmovimientosdetalles.idmovimiento=" + pId.ToString + " and tblmovimientosdetalles.idvariante>1"
            'Comm.ExecuteNonQuery()
            'End If
        End If
        If Tipo = dbInventarioConceptos.Tipos.Salida Then
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,surtido,0,0,1),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen,cantidad-surtido,0,0),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
        End If
        If Tipo = dbInventarioConceptos.Tipos.Traspaso Then
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,surtido,0,0,1),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen,cantidad-surtido,0,0),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen2,surtido,0,1,1),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen2,cantidad-surtido,0,1),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
        End If
        If Tipo = dbInventarioConceptos.Tipos.Ajuste Then
            'Comm.CommandText = "select if(idinventario>1,spajustainventarioi(idinventario,idalmacen,inventarioanterior),0),if(idvariante>1,spajustainventariop(idvariante,idalmacen,inventarioanterior),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            'Comm.ExecuteNonQuery()
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,inventarioanterior,0,0,1),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen,inventarioanterior,0,0),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Sub ModificaInventario(ByVal pId As Integer, ByVal PTipoCosteo As Byte, ByVal pTipodeCambio As Double)

        Dim Tipo As Integer
        Comm.CommandText = "select tblinventarioconceptos.tipo from tblinventarioconceptos inner join tblmovimientos on tblinventarioconceptos.idconcepto=tblmovimientos.idconcepto where tblmovimientos.idmovimiento=" + pId.ToString
        Tipo = Comm.ExecuteScalar
        If Tipo = dbInventarioConceptos.Tipos.Entrada Or Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,cantidad-surtido,0,0,1),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen,cantidad-surtido,0,0),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
        End If
        If Tipo = dbInventarioConceptos.Tipos.Salida Then
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,cantidad-surtido,0,1,1),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen,cantidad-surtido,0,1),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
        End If
        If Tipo = dbInventarioConceptos.Tipos.Traspaso Then
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen,cantidad-surtido,0,1,1),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen,cantidad-surtido,0,1),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select if(idinventario>1,spmodificainventarioi(idinventario,idalmacen2,cantidad-surtido,0,0,1),0),if(idvariante>1,spmodificainventariop(idvariante,idalmacen2,cantidad-surtido,0,0),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
        End If
        If Tipo = dbInventarioConceptos.Tipos.Ajuste Then
            Comm.CommandText = "update tblmovimientosdetalles set inventarioanterior=spdainventario(idinventario,idalmacen,0)-cantidad where idinventario>1 and idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "update tblmovimientosdetalles inner join tblproductosvariantes on tblmovimientosdetalles.idvariante=tblproductosvariantes.idvariante set inventarioanterior=spdainventariop(idproducto,idalmacen,0)-cantidad where tblmovimientosdetalles.idvariante>1 and idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "select if(idinventario>1,spajustainventarioi(idinventario,idalmacen,cantidad),0),if(idvariante>1,spajustainventariop(idvariante,idalmacen,cantidad),0) from tblmovimientosdetalles where idmovimiento=" + pId.ToString + ";"
            Comm.CommandText += "update tblmovimientosdetalles set surtido=cantidad where idmovimiento=" + pId.ToString
            Comm.ExecuteNonQuery()
        End If


    End Sub

    Public Function VerificaExistencias(ByVal pId As Integer) As String
        Dim Str As String = ""
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Cont As Integer = 1
        Comm.CommandText = "select iddetalle from tblmovimientosdetalles where idmovimiento=" + pId.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read()
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        Dim I As New dbInventario(MySqlcon)
        Dim P As New dbProductos(MySqlcon)
        Dim iIdInventario As Integer
        Dim iIdVariante As Integer
        Dim iCantidad As Double
        Dim iIdAlmacen As Integer
        Dim iCantidad2 As Double
        Dim EsInventariable As Integer
        While Cont <= IDs.Count
            Comm.CommandText = "select idinventario,idvariante,cantidad,idalmacen,if(idinventario>1,(select inventariable from tblinventario where tblinventario.idinventario=tblmovimientosdetalles.idinventario),0) as esinventariable,if(idvariante>1,(select inventariable from tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto where tblproductosvariantes.idvariante=tblmovimientosdetalles.idvariante),0) as esinventariablep from tblmovimientosdetalles where iddetalle=" + IDs(Cont).ToString
            DReader = Comm.ExecuteReader
            If DReader.Read() Then
                iIdInventario = DReader("idinventario")
                iCantidad = DReader("cantidad")
                iIdVariante = DReader("idvariante")
                iIdAlmacen = DReader("idalmacen")
                EsInventariable = DReader("esinventariable")
                DReader.Close()
                If iIdInventario > 1 And EsInventariable = 1 Then
                    iCantidad2 = I.DaInventario(iIdAlmacen, iIdInventario)
                    If iCantidad > iCantidad2 Then
                        Str = " Hay artículos con insuficiente inventario."
                    End If
                End If
                If iIdVariante > 1 Then
                    Comm.CommandText = "select spverificaexistenciarecetas(" + iIdVariante.ToString + "," + iIdAlmacen.ToString + "," + iCantidad.ToString + ")"
                    Str = Comm.ExecuteScalar
                End If
            Else
                DReader.Close()
            End If
            Cont += 1
        End While
        Return Str
    End Function
    Public Function Reporte(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidSucursal As Integer, ByVal pidalmacen As Integer, ByVal pidalmacen2 As Integer, ByVal pidconcepto As Integer, ByVal ptipo As Integer, ByVal pIdInventario As Integer, ByVal pIdClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.idmovimiento,tvi.fecha,tblinventarioconceptos.nombre,tvi.serie,tvi.folio,tvd.cantidad,tblinventario.nombre as nombrei,tblinventario.clave as aclave,tvd.precio,tvd.precio/tvd.cantidad as preciou,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen) as almacen,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen2) as almacen2,tblinventario.costobase,tblinventarioconceptos.tipo," + _
        "if(tvi.idventa<>0,(select concat('F-',tblventas.serie,convert(tblventas.folio using utf8)) from tblventas where tblventas.idventa=tvi.idventa),if(tvi.idremision<>0,(select concat('R-',serie,convert(folio using utf8)) from tblventasremisiones where tblventasremisiones.idremision=tvi.idremision),'')) as referencia,tvi.estado " + _
        "from tblmovimientos as tvi inner join tblmovimientosdetalles as tvd on tvi.idmovimiento=tvd.idmovimiento inner join tblinventario on tvd.idinventario=tblinventario.idinventario inner join tblinventarioconceptos on tvi.idconcepto=tblinventarioconceptos.idconcepto where (tvi.estado=3 or tvi.estado=4) and tvi.fecha>='" + pFecha + "' and tvi.fecha<='" + pFecha2 + "'"
        If pidSucursal > 0 Then
            Comm.CommandText += " and tvi.idsucursal=" + pidSucursal.ToString
        End If
        If pidalmacen > 0 Then
            Comm.CommandText += " and tvd.idalmacen=" + pidalmacen.ToString
        End If
        If pidalmacen2 > 0 Then
            Comm.CommandText += " and tvd.idalmacen2=" + pidalmacen2.ToString
        End If
        If pidconcepto > 0 Then
            Comm.CommandText += " and tvi.idconcepto=" + pidconcepto.ToString
        End If
        If ptipo >= 0 Then
            Comm.CommandText += " and tblinventarioconceptos.tipo=" + ptipo.ToString
        End If
        If pIdInventario <> 0 Then
            Comm.CommandText += " and tvd.idinventario=" + pIdInventario.ToString
        End If
        If pidClasificacion > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion=" + pIdClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
        End If
        Comm.CommandText += " order by tvi.fecha,tvi.serie,tvi.folio"
        'Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tblinventario.nombre as descripcion,tvi.cantidad,tblinventarioseries.noserie,tblinventarioseries.fechagarantia,tblmovimientos.serie,tblmovimientos.folio,tblmovimientos.fecha,tblmovimientos.hora,'' as cnombre,'' as cclave from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblinventarioseries on tvi.idinventario=tblinventarioseries.idinventario and tvi.idmovimiento=tblinventarioseries.idmovimiento inner join tblmovimientos on tvi.idmovimiento=tblmovimientos.idmovimiento where tvi.idmovimiento=" + pidMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrepmovimientos")
        'DS.WriteXmlSchema("tblrepmovimientos.xml")
        Return DS.Tables("tblrepmovimientos").DefaultView
    End Function
    Public Function ReporteMovimientosxCliente(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidSucursal As Integer, ByVal pidalmacen As Integer, ByVal pidalmacen2 As Integer, ByVal pidconcepto As Integer, ByVal ptipo As Integer, ByVal pIdInventario As Integer, ByVal pIdClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pIdCliente As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "delete from tblrepmovimientosclientes"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "insert into tblrepmovimientosclientes(idmovimiento,fecha,concepto,serie,folio,cantidad,nombrei,clavei,precio,preciou,almacen1,almacen2,costobase,conceptotipo,referencia,idcliente,nombrec,idconcepto,estado) " + _
        "select tvi.idmovimiento,tvi.fecha,tblinventarioconceptos.nombre,tvi.serie,tvi.folio,tvd.cantidad,tblinventario.nombre as nombrei,tblinventario.clave as aclave,tvd.precio,tvd.precio/tvd.cantidad as preciou,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen) as almacen,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen2) as almacen2,tblinventario.costobase,tblinventarioconceptos.tipo," + _
        "if(tvi.idventa<>0,(select concat('F-',tblventas.serie,convert(tblventas.folio using utf8)) from tblventas where tblventas.idventa=tvi.idventa limit 1),if(tvi.idremision<>0,(select concat('R-',serie,convert(folio using utf8)) from tblventasremisiones where tblventasremisiones.idremision=tvi.idremision limit 1),'')) as referencia,tblclientes.idcliente,tblclientes.nombre,tvi.idconcepto,tvi.estado " + _
        "from tblmovimientos as tvi inner join tblmovimientosdetalles as tvd on tvi.idmovimiento=tvd.idmovimiento inner join tblinventario on tvd.idinventario=tblinventario.idinventario inner join tblinventarioconceptos on tvi.idconcepto=tblinventarioconceptos.idconcepto inner join tblventas on tblventas.idventa=tvi.idventa inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where (tvi.estado=3 or tvi.estado=4) and tvi.fecha>='" + pFecha + "' and tvi.fecha<='" + pFecha2 + "' and tvi.idventa<>0"
        If pidSucursal > 0 Then
            Comm.CommandText += " and tvi.idsucursal=" + pidSucursal.ToString
        End If
        If pidalmacen > 0 Then
            Comm.CommandText += " and tvd.idalmacen=" + pidalmacen.ToString
        End If
        If pidalmacen2 > 0 Then
            Comm.CommandText += " and tvd.idalmacen2=" + pidalmacen2.ToString
        End If
        If pidconcepto > 0 Then
            Comm.CommandText += " and tvi.idconcepto=" + pidconcepto.ToString
        End If
        If ptipo >= 0 Then
            Comm.CommandText += " and tblinventarioconceptos.tipo=" + ptipo.ToString
        End If
        If pIdInventario <> 0 Then
            Comm.CommandText += " and tvd.idinventario=" + pIdInventario.ToString
        End If
        If pIdClasificacion > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion=" + pIdClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventas.idcliente=" + pIdCliente.ToString
        End If
        Comm.CommandText += " order by tvi.fecha,tvi.serie,tvi.folio"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "insert into tblrepmovimientosclientes(idmovimiento,fecha,concepto,serie,folio,cantidad,nombrei,clavei,precio,preciou,almacen1,almacen2,costobase,conceptotipo,referencia,idcliente,nombrec,idconcepto,estado) " + _
        "select tvi.idmovimiento,tvi.fecha,tblinventarioconceptos.nombre,tvi.serie,tvi.folio,tvd.cantidad,tblinventario.nombre as nombrei,tblinventario.clave as aclave,tvd.precio,tvd.precio/tvd.cantidad as preciou,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen) as almacen,(select tblalmacenes.nombre from tblalmacenes where tblalmacenes.idalmacen=tvd.idalmacen2) as almacen2,tblinventario.costobase,tblinventarioconceptos.tipo," + _
        "if(tvi.idventa<>0,(select concat('F-',tblventas.serie,convert(tblventas.folio using utf8)) from tblventas where tblventas.idventa=tvi.idventa limit 1),if(tvi.idremision<>0,(select concat('R-',serie,convert(folio using utf8)) from tblventasremisiones where tblventasremisiones.idremision=tvi.idremision limit 1),'')) as referencia,tblclientes.idcliente,tblclientes.nombre,tvi.idconcepto,tvi.estado " + _
        "from tblmovimientos as tvi inner join tblmovimientosdetalles as tvd on tvi.idmovimiento=tvd.idmovimiento inner join tblinventario on tvd.idinventario=tblinventario.idinventario inner join tblinventarioconceptos on tvi.idconcepto=tblinventarioconceptos.idconcepto inner join tblventasremisiones on tblventasremisiones.idremision=tvi.idremision inner join tblclientes on tblventasremisiones.idcliente=tblclientes.idcliente where (tvi.estado=3 or tvi.estado=4) and tvi.fecha>='" + pFecha + "' and tvi.fecha<='" + pFecha2 + "' and tvi.idremision<>0"
        If pidSucursal > 0 Then
            Comm.CommandText += " and tvi.idsucursal=" + pidSucursal.ToString
        End If
        If pidalmacen > 0 Then
            Comm.CommandText += " and tvd.idalmacen=" + pidalmacen.ToString
        End If
        If pidalmacen2 > 0 Then
            Comm.CommandText += " and tvd.idalmacen2=" + pidalmacen2.ToString
        End If
        If pidconcepto > 0 Then
            Comm.CommandText += " and tvi.idconcepto=" + pidconcepto.ToString
        End If
        If ptipo >= 0 Then
            Comm.CommandText += " and tblinventarioconceptos.tipo=" + ptipo.ToString
        End If
        If pIdInventario <> 0 Then
            Comm.CommandText += " and tvd.idinventario=" + pIdInventario.ToString
        End If
        If pIdClasificacion > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion=" + pIdClasificacion.ToString
        End If
        If pidClasificacion2 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion2.ToString
        End If
        If pidClasificacion3 > 0 Then
            Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion3.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tblventasremisiones.idcliente=" + pIdCliente.ToString
        End If
        Comm.CommandText += " order by tvi.fecha,tvi.hora"
        Comm.ExecuteNonQuery()
        'Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tblinventario.nombre as descripcion,tvi.cantidad,tblinventarioseries.noserie,tblinventarioseries.fechagarantia,tblmovimientos.serie,tblmovimientos.folio,tblmovimientos.fecha,tblmovimientos.hora,'' as cnombre,'' as cclave from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblinventarioseries on tvi.idinventario=tblinventarioseries.idinventario and tvi.idmovimiento=tblinventarioseries.idmovimiento inner join tblmovimientos on tvi.idmovimiento=tblmovimientos.idmovimiento where tvi.idmovimiento=" + pidMovimiento.ToString
        Comm.CommandText = "select * from tblrepmovimientosclientes "
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblrepmovimientosc")
        'DS.WriteXmlSchema("tblrepmovimientosc.xml")
        Return DS.Tables("tblrepmovimientosc").DefaultView
    End Function
    Public Function ReporteVentasSeries(ByVal pidMovimiento As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tblinventario.nombre as descripcion,tvi.cantidad,tblinventarioseries.noserie,tblinventarioseries.fechagarantia,tblmovimientos.serie,tblmovimientos.folio,tblmovimientos.fecha,tblmovimientos.hora,'' as cnombre,'' as cclave from tblmovimientosdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblinventarioseries on tvi.idinventario=tblinventarioseries.idinventario and tvi.idmovimiento=tblinventarioseries.idmovimiento inner join tblmovimientos on tvi.idmovimiento=tblmovimientos.idmovimiento where tvi.idmovimiento=" + pidMovimiento.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventasseries")
        'DS.WriteXmlSchema("tblventasseries.xml")
        Return DS.Tables("tblventasseries").DefaultView
    End Function
    Public Sub ReCalculaCostos(ByVal pIdMovimiento As Integer, ByVal pTipoCosteo As Byte, ByVal TiempoReal As Byte, ByVal pTipodeCambio As Double)
        If TiempoReal = 1 Then
            Comm.CommandTimeout = 10000
            Comm.CommandText = "select sprecalculacostos(tblmovimientos.fecha,tblmovimientosdetalles.idinventario," + pTipoCosteo.ToString + ") from tblmovimientos inner join tblmovimientosdetalles on tblmovimientos.idmovimiento=tblmovimientosdetalles.idmovimiento inner join tblinventarioconceptos on tblmovimientos.idconcepto=tblinventarioconceptos.idconcepto where (tblinventarioconceptos.tipo=0 or tblinventarioconceptos.tipo=4) and tblmovimientosdetalles.idmovimiento=" + pIdMovimiento.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "update tblinventario inner join tblmovimientosdetalles on tblinventario.idinventario=tblmovimientosdetalles.idinventario set tblinventario.costobase=spsacacostoarticulo(tblinventario.idinventario,1,tblinventario.contenido," + pTipodeCambio.ToString + "," + pTipoCosteo.ToString + ") where tblmovimientosdetalles.idmovimiento=" + pIdMovimiento.ToString + " and tblmovimientosdetalles.idinventario>1"
            Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes.idproducto inner join tblmovimientosdetalles on tblproductosvariantes.idvariante=tblmovimientosdetalles.idvariante set tblproductos.costo=spsacacostoproducto(tblproductos.idproducto," + pTipoCosteo.ToString + ") where tblmovimientosdetalles.idmovimiento=" + pId.ToString + " and tblmovimientosdetalles.idvariante>1"
            'Comm.ExecuteNonQuery()
        End If
    End Sub
End Class
