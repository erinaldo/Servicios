Public Class dbComprasRemisiones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Idproveedor As Integer
    Public Fecha As String
    Public Proveedor As dbproveedores
    Public Folio As String
    Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    Public Estado As Byte
    Public idMoneda As Integer
    Public IdSucursal As Integer
    Public Serie As String
    Public Folioi As Integer
    Public TipodeCambio As Double
    Public IdPedido As Integer

    Public TotalISR As Double
    Public Subtotal As Double
    Public TotalIva As Double
    Public TotalIvaRetenido As Double
    Public TotalVenta As Double

    Public Usado As Byte
    Public FechaCancelado As String
    Public HoraCancelado As String
    Public Comentario As String

    Public idComprar As Integer
    Public FolioRef As String
    Public TotalIEPS As Double
    Public TotalIvaRetenidoCon As Double
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idproveedor = -1
        Fecha = ""
        Hora = ""
        Folio = ""
        idMoneda = 0
        Iva = 0
        TotalaPagar = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        idComprar = 0
        Comentario = ""
        FolioRef = ""
        Comm.Connection = Conexion
        Proveedor = New dbproveedores(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcomprasremisiones where idremision=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            Idproveedor = DReader("idproveedor")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            idMoneda = DReader("idmoneda")
            Iva = DReader("iva")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            Serie = DReader("serie")
            Folioi = DReader("folioi")
            TipodeCambio = DReader("tipodecambio")
            Usado = DReader("usado")
            FechaCancelado = DReader("fechacancelado")
            HoraCancelado = DReader("horacancelado")
            Comentario = DReader("comentario")
            idComprar = DReader("idcomprar")
            IdPedido = DReader("idpedido")
        End If
        DReader.Close()

        If idComprar <> 0 Then
            Comm.CommandText = "select ifnull((select concat(serie,convert(folioi using utf8),'\n',referencia) from tblcompras where idcompra=" + idComprar.ToString + "),'')"
            FolioRef = "Facturado en: " + vbCrLf + Comm.ExecuteScalar
        End If
        Proveedor = New dbproveedores(Idproveedor, Comm.Connection)
    End Sub

    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folioi) from tblcomprasremisiones where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado>=2)),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As String, ByVal pIdProveedor As Integer) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblcomprasremisiones where folio='" + pFolio + "' and idproveedor=" + pIdProveedor.ToString + " and estado>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    Public Function ChecaFolioRepetidoi(ByVal pSerie As String, ByVal pFolio As Integer, ByVal pIdSucursal As Integer) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folioi) from tblcomprasremisiones where folioi=" + pFolio.ToString + " and idsucursal=" + pIdSucursal.ToString + " and serie='" + Replace(pSerie, "'", "''") + "' and estado>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetidoi = False
        Else
            ChecaFolioRepetidoi = True
        End If
    End Function
    Public Sub Guardar(ByVal pIdProveedor As Integer, ByVal pFecha As String, ByVal pFolio As String, ByVal pidMoneda As Integer, ByVal pIva As Double, ByVal pidSucursal As Integer, ByVal pSerie As String, ByVal pFolioi As Integer, ByVal pTipodeCambio As Double, pIdPedido As String)
        Idproveedor = pIdProveedor
        Fecha = pFecha
        Folio = pFolio
        idMoneda = pidMoneda
        Iva = pIva
        IdSucursal = pidSucursal
        Serie = pSerie
        Folioi = pFolioi
        TipodeCambio = pTipodeCambio
        IdPedido = pIdPedido
        Comm.CommandText = "insert into tblcomprasremisiones(idproveedor,fecha,folio,total,hora,estado,iva,totalapagar,idmoneda,idsucursal,serie,folioi,tipodecambio,usado,fechacancelado,horacancelado,comentario,idcomprar,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,idpedido) values(" + Idproveedor.ToString + ",'" + Fecha + "','" + Folio + "',0,'" + Format(TimeOfDay, "HH:mm:ss") + "'," + CStr(Estados.SinGuardar) + "," + Iva.ToString + ",0," + idMoneda.ToString + "," + IdSucursal.ToString + ",'" + Replace(Trim(Serie), "'", "''") + "'," + Folioi.ToString + "," + TipodeCambio.ToString + ",0,'" + Fecha + "','" + Format(TimeOfDay, "HH:mm:ss") + "','',0," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + pIdPedido.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idremision) from tblcomprasremisiones"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As String, ByVal pidmoneda As Integer, ByVal pIva As Double, ByVal pEstado As Byte, ByVal pTotal As Double, ByVal pTotalaPagar As Double, ByVal pIdproveedor As Integer, ByVal pSerie As String, ByVal pFolioi As Integer, ByVal pTipodeCambio As Double, ByVal pcomentario As String)
        ID = pID
        Fecha = pFecha
        Folio = pFolio
        idMoneda = pidmoneda
        Iva = pIva
        Estado = pEstado
        Total = pTotal
        TotalaPagar = pTotalaPagar
        Idproveedor = pIdproveedor
        Serie = pSerie
        Folioi = pFolioi
        TipodeCambio = pTipodeCambio
        Comentario = pcomentario
        '(idcliente,fecha,folio,total,hora,estado,iva,totalapagar,desglozar)
        Estado = pEstado
        Comm.CommandText = "update tblcomprasremisiones set fecha='" + Fecha + "',folio='" + Folio + "',idmoneda=" + idMoneda.ToString + ",iva=" + Iva.ToString + ",estado=" + Estado.ToString + ",total=" + Total.ToString + ",totalapagar=" + TotalaPagar.ToString + ",idproveedor=" + pIdproveedor.ToString + ",folioi=" + Folioi.ToString + ",serie='" + Replace(Trim(Serie), "'", "''") + "',tipodecambio=" + TipodeCambio.ToString + ",fechacancelado='" + Fecha + "',comentario='" + Replace(Comentario, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idremision=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaComentario(ByVal pidremision As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblcomprasremisiones set comentario='" + Replace(pTexto, "'", "''") + "' where idremision=" + pidremision.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcomprasremisiones where idremision=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Usar(ByVal pID As Integer, ByVal pIdComprar As Integer)
        Comm.CommandText = "update tblcomprasremisiones set usado=1,idcomprar=" + pIdComprar.ToString + ",idpedido=0 where idremision=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function EstaLigada(ByVal pIdremision As Integer) As Integer
        Comm.CommandText = "select ifnull((select idcomprar from tblcomprasremisiones where idremision=" + pIdremision.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pNombreClave As String, ByVal pEstado As Byte, ByVal pFolio As String, ByVal pUsado As Byte, ByVal pIdSucursal As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblcomprasremisiones.idremision,tblcomprasremisiones.fecha,concat(tblcomprasremisiones.serie,convert(tblcomprasremisiones.folioi using utf8),' - ',tblcomprasremisiones.folio) as folio,tblproveedores.clave,tblproveedores.nombre as Proveedor,tblcomprasremisiones.totalapagar as Importe,case tblcomprasremisiones.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as Estado " + _
        "from tblcomprasremisiones inner join tblproveedores on tblcomprasremisiones.idproveedor=tblproveedores.idproveedor where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblproveedores.clave,tblproveedores.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblcomprasremisiones.serie,convert(tblcomprasremisiones.folioi using utf8),tblcomprasremisiones.folio) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado <> Estados.Inicio Then
            Comm.CommandText += " and tblcomprasremisiones.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblcomprasremisiones.estado<>1"
        End If
        If pUsado <> 0 Then
            Comm.CommandText += " and tblcomprasremisiones.usado<>1"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblcomprasremisiones.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " order by tblcomprasremisiones.fecha desc,folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasremisiones")
        Return DS.Tables("tblcomprasremisiones").DefaultView
    End Function
    Public Function DaIvas(ByVal pIdRemision As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iva,precio,idmoneda from tblcomprasremisionesdetalles where idremision=" + pIdRemision.ToString
        Return Comm.ExecuteReader
    End Function

    Public Function DaTotal(ByVal pidRemision As Integer, ByVal pidMoneda As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0
        'Dim Encontro As Double
        Dim iIva As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double = 1
        Dim iIsr As Double = 0
        Dim iIvaRetenido As Double = 0
        Dim pIEPS As Double
        Dim pivaRetenido As Double
        TotalIEPS = 0
        TotalIvaRetenidoCon = 0
        Subtotal = 0
        TotalIva = 0
        TotalVenta = 0
        'Comm.CommandText = "select tipodecambio from tblventas where idventa=" + pidVenta.ToString
        'iTipoCambio = Comm.ExecuteScalar
        'Comm.CommandText = "select isr from tblventas where idventa=" + pidVenta.ToString
        'iIsr = Comm.ExecuteScalar
        'Comm.CommandText = "select ivaretenido from tblventas where idventa=" + pidVenta.ToString
        'iIvaRetenido = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tblcomprasremisionesdetalles where idremision=" + pidRemision.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblcomprasremisionesdetalles where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select iva from tblcomprasremisionesdetalles where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblcomprasremisionesdetalles where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            Comm.CommandText = "select ieps from tblcomprasremisionesdetalles where iddetalle=" + IDs.Item(Cont).ToString
            pIEPS = Comm.ExecuteScalar
            Comm.CommandText = "select IVARetenido from tblcomprasremisionesdetalles where iddetalle=" + IDs.Item(Cont).ToString
            pivaRetenido = Comm.ExecuteScalar
            If pidMoneda = 2 Then
                If pidMoneda <> IdMonedaC Then
                    Precio = Precio * iTipoCambio
                End If
            Else
                If IdMonedaC = 2 Then
                    Precio = Precio / iTipoCambio
                End If
            End If
            TotalIEPS += (Precio * (pIEPS / 100))
            TotalIvaRetenidoCon += (Precio * (pivaRetenido / 100))
            Subtotal += Precio
            TotalIva += (Precio * (iIva / 100))
            Cont += 1
        End While

        TotalISR = Subtotal * (iIsr / 100)
        TotalIvaRetenido = Subtotal * (iIvaRetenido / 100)
        TotalVenta = Subtotal + TotalIva - TotalISR - TotalIvaRetenido + TotalIEPS - TotalIvaRetenidoCon
        Return TotalVenta
    End Function


    'Public ReadOnly Property totalLetra(ByVal idmoneda As Integer) As String
    '    Get
    '        Dim f As New StringFunctions
    '        Return f.PASELETRAS(DaTotal(ID, idmoneda), idmoneda, GlobalIdiomaLetras)
    '    End Get
    'End Property


    Public Sub AgregarDetallesReferencia(ByVal PidCotizacion As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte, ByVal pidAlmacen As Integer)
        '0 cotizacion
        '1 pedido
        '2 remision
        '3 ventas

        If Tipo = 0 Then
            Comm.CommandText = "insert into tblcomprasremisionesdetalles(idremision,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idalmacen,surtido,IEPS,ivaRetenido) select " + PidCotizacion.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento," + pidAlmacen.ToString + ",0,IEPS,ivaRetenido from tblcomprascotizacionesdetalles where idcotizacion=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventascotizacionesproductos(idcotizacion,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventascotizacionesproductos where idcotizacion=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 1 Then
            Comm.CommandText = "insert into tblcomprasremisionesdetalles(idremision, idinventario, cantidad, precio, descripcion, idmoneda, iva, extra, descuento, idalmacen, surtido, IEPS, ivaRetenido) select " + PidCotizacion.ToString + ",idinventario, cantidad-surtido, precio, descripcion, idmoneda ,iva, extra, descuento," + pidAlmacen.ToString + ", 0, IEPS, ivaRetenido from tblcompraspedidosdetalles where idpedido = " + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventascotizacionesproductos(idcotizacion,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventaspedidosproductos where idpedido=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 2 Then
            Comm.CommandText = "insert into tblcomprasremisionesdetalles(idremision,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idalmacen,surtido,IEPS,ivaRetenido) select " + PidCotizacion.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idalmacen,0,IEPS,ivaRetenido from tblcomprasremisionesdetalles where idremision=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventascotizacionesproductos(idcotizacion,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasremisionesproductos where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventasservicios(idventa,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasremisionesservicios where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblinventarioseries set idventa=" + PidCotizacion.ToString + " where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 3 Then
            Comm.CommandText = "insert into tblcomprasremisionesdetalles(idremision,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idalmacen,surtido,IEPS,ivaRetenido) select " + PidCotizacion.ToString + ",idinventario,cantidad,precio,(select nombre from tblinventario where tblinventario.idinventario=tblcomprasdetalles.idinventario),idmoneda,iva,extra,descuento,idalmacen,0,IEPS,ivaRetenido from tblcomprasdetalles where idcompra=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventascotizacionesproductos(idcotizacion,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasproductos where idventa=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventasservicios(idventa,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasservicios where idventa=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub ModificaInventario(ByVal pId As Integer)
        Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,cantidad-surtido,0,0,1) from tblcomprasremisionesdetalles where idremision=" + pId.ToString + ";"
        Comm.CommandText += "update tblcomprasremisionesdetalles set surtido=cantidad where idremision=" + pId.ToString + ";"
        Comm.CommandText += "select spmodificainventariolotesf(tblcomprasremisionesdetalles.idinventario,tblcomprasremisionesdetalles.idalmacen,tblcomprasremisioneslotes.cantidad-tblcomprasremisioneslotes.surtido,0,0,1,tblcomprasremisioneslotes.idlote) from tblcomprasremisioneslotes inner join tblcomprasremisionesdetalles on tblcomprasremisioneslotes.iddetalle = tblcomprasremisionesdetalles.iddetalle where tblcomprasremisionesdetalles.idremision= " + pId.ToString + "; "
        Comm.CommandText += "select spmodificainventarioaduanaf(tblcomprasremisionesdetalles.idinventario,tblcomprasremisionesdetalles.idalmacen,tblcomprasremisionesaduana.cantidad-tblcomprasremisionesaduana.surtido,0,0,1,tblcomprasremisionesaduana.idaduana) from tblcomprasremisionesaduana inner join tblcomprasremisionesdetalles on tblcomprasremisionesaduana.iddetalle = tblcomprasremisionesdetalles.iddetalle where tblcomprasremisionesdetalles.idremision= " + pId.ToString + "; "
        Comm.CommandText += "update tblcomprasremisioneslotes inner join tblcomprasremisionesdetalles on tblcomprasremisioneslotes.iddetalle=tblcomprasremisionesdetalles.iddetalle set tblcomprasremisioneslotes.surtido=tblcomprasremisioneslotes.cantidad where idremision=" + pId.ToString + ";"
        Comm.CommandText += "update tblcomprasremisionesaduana inner join tblcomprasremisionesdetalles on tblcomprasremisionesaduana.iddetalle=tblcomprasremisionesdetalles.iddetalle set tblcomprasremisionesaduana.surtido=tblcomprasremisionesaduana.cantidad where idremision=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()

        'ubicaciones
        Comm.CommandText = "select spmodificainventarioubicacionesf(d.idinventario, d.idalmacen, u.cantidad-u.surtido, 0, 0, 1, u.ubicacion) from tblcomprasremisionesdetalles d inner join tblcomprasremisionesubicaciones u on d.iddetalle=u.iddetalle where d.idremision=" + pId.ToString + ";"
        Comm.CommandText += "update tblcomprasremisionesubicaciones inner join tblcomprasremisionesdetalles on tblcomprasremisionesubicaciones.iddetalle = tblcomprasremisionesdetalles.iddetalle set tblcomprasremisionesubicaciones.surtido = tblcomprasremisionesubicaciones.cantidad where tblcomprasremisionesdetalles.idremision=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()

        'verifica si la compra se creó desde un pedido y le modifica los surtidos al pedido según los que se remisionaron
        Comm.CommandText = "select idpedido from tblcomprasremisiones where idremision=" + pId.ToString
        Dim idpedido As Integer = Comm.ExecuteScalar
        If idpedido <> Nothing And idpedido <> 0 Then
            Comm.CommandText = "update tblcompraspedidosdetalles as pd set pd.surtido=pd.surtido+ifnull((select cd.cantidad from tblcomprasremisiones as c inner join tblcomprasremisionesdetalles as cd on c.idremision=cd.idremision where c.idremision=" + pId.ToString + " and cd.idinventario=pd.idinventario),0) where pd.idpedido=" + idpedido.ToString
            Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Sub RegresaInventario(ByVal pId As Integer)
        Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,surtido,0,1,1) from tblcomprasremisionesdetalles where idremision=" + pId.ToString + ";"
        Comm.CommandText += "select spmodificainventariolotesf(tblcomprasremisionesdetalles.idinventario,tblcomprasremisionesdetalles.idalmacen,tblcomprasremisioneslotes.surtido,0,1,1,tblcomprasremisioneslotes.idlote) from tblcomprasremisioneslotes inner join tblcomprasremisionesdetalles on tblcomprasremisioneslotes.iddetalle=tblcomprasremisionesdetalles.iddetalle where tblcomprasremisionesdetalles.idremision=" + pId.ToString + ";"
        Comm.CommandText += "select spmodificainventarioaduanaf(tblcomprasremisionesdetalles.idinventario,tblcomprasremisionesdetalles.idalmacen,tblcomprasremisionesaduana.surtido,0,1,1,tblcomprasremisionesaduana.idaduana) from tblcomprasremisionesaduana inner join tblcomprasremisionesdetalles on tblcomprasremisionesaduana.iddetalle=tblcomprasremisionesdetalles.iddetalle where tblcomprasremisionesdetalles.idremision=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblcomprasremisionesdetalles set surtido=0 where idremision=" + pId.ToString + ";"
        Comm.CommandText += "update tblcomprasremisioneslotes inner join tblcomprasremisionesdetalles on tblcomprasremisioneslotes.iddetalle=tblcomprasremisionesdetalles.iddetalle set tblcomprasremisioneslotes.surtido=0 where idremision=" + pId.ToString + ";"
        Comm.CommandText += "update tblcomprasremisionesaduana inner join tblcomprasremisionesdetalles on tblcomprasremisionesaduana.iddetalle=tblcomprasremisionesdetalles.iddetalle set tblcomprasremisionesaduana.surtido=0 where idremision=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()

        'ubicaciones
        Comm.CommandText = "select spmodificainventarioubicacionesf(d.idinventario, d.idalmacen, u.surtido, 0, 1, 1, u.ubicacion) from tblcomprasremisionesdetalles d inner join tblcomprasremisionesubicaciones u on d.iddetalle=u.iddetalle where d.idremision=" + pId.ToString + ";"
        Comm.CommandText += "update tblcomprasremisionesubicaciones inner join tblcomprasremisionesdetalles on tblcomprasremisionesubicaciones.iddetalle = tblcomprasremisionesdetalles.iddetalle set tblcomprasremisionesubicaciones.surtido = tblcomprasremisionesubicaciones.cantidad where tblcomprasremisionesdetalles.idremision=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ReporteVentasSeries(ByVal pidRemision As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tblinventario.nombre as descripcion,tvi.cantidad,tblinventarioseries.noserie,tblinventarioseries.fechagarantia,'' as serie,tblcomprasremisiones.folio,tblcomprasremisiones.fecha,tblcomprasremisiones.hora,tblproveedores.nombre as cnombre,tblproveedores.clave as cclave from tblcomprasremisionesdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblinventarioseries on tvi.idinventario=tblinventarioseries.idinventario and tvi.idremision=tblinventarioseries.idremisionc inner join tblcomprasremisiones on tvi.idremision=tblcomprasremisiones.idremision inner join tblproveedores on tblcomprasremisiones.idproveedor=tblproveedores.idproveedor where tvi.idremision=" + pidRemision.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasseriesr")
        'DS.WriteXmlSchema("tblventasseriesr.xml")
        Return DS.Tables("tblcomprasseriesr").DefaultView
    End Function
    Public Function ReporteComprasArticulos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, pIdTipoSucursal As Integer, ByVal pUsado As Byte, pIdTipoProv As Integer, pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select c.idremision idcompra,c.serie,c.folioi,c.folio referencia,c.estado,c.fecha,c.tipodecambio,c.idmoneda,cd.cantidad,i.nombre as descripcion,if(cd.idmoneda=2,cd.precio,cd.precio*c.tipodecambio) as precio,0 as costoinv,0 as costopro,cd.iva,i.clave,0 cindirecto,p.clave as clavep,p.nombre as nombrep," + _
            "if(cd.idmoneda=2,cd.iva*cd.precio/100,cd.precio*c.tipodecambio*cd.iva/100) as ivacalc,if(cd.idmoneda=2,cd.ieps*cd.precio/100,cd.precio*c.tipodecambio*cd.ieps/100) as iepscalc,if(cd.idmoneda=2,cd.ivaretenido*cd.precio/100,cd.precio*c.tipodecambio*cd.ivaretenido/100) as ivaretcalc, c.usado " + _
            "from tblcomprasremisiones c inner join tblcomprasremisionesdetalles cd on c.idremision=cd.idremision inner join tblinventario i on cd.idinventario=i.idinventario inner join tblproveedores p on c.idproveedor=p.idproveedor inner join tblsucursales s on c.idsucursal=s.idsucursal where c.estado=3 and c.fecha>='" + pFecha1 + "' and c.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select c.idremision idcompra,c.serie,c.folioi,c.folio referencia,c.estado,c.fecha,c.tipodecambio,c.idmoneda,cd.cantidad,i.nombre as descripcion,cd.precio,0 as costoinv,0 as costopro,cd.iva,i.clave,0 cindirecto,p.clave as clavep,p.nombre as nombrep," + _
            "cd.iva*cd.precio/100 as ivacalc,cd.precio*cd.ieps/100 as iepscalc,cd.ivaretenido*cd.precio/100 as ivaretcalc,c.usado " + _
            "from tblcomprasremisiones c inner join tblcomprasremisionesdetalles cd on c.idremision=cd.idremision inner join tblinventario i on cd.idinventario=i.idinventario inner join tblproveedores p on c.idproveedor=p.idproveedor inner join tblsucursales s on c.idsucursal=s.idsucursal where c.estado=3 and  c.fecha>='" + pFecha1 + "' and c.fecha<='" + pFecha2 + "'"
        End If
        If pUsado = 0 Then Comm.CommandText += " and c.usado=0"
        If pIdSucursal > 0 Then Comm.CommandText += " and c.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdProveedor > 0 Then Comm.CommandText += " and c.idproveedor=" + pIdProveedor.ToString
        If pidMoneda > 0 Then Comm.CommandText += " and cd.idmoneda=" + pidMoneda.ToString
        If pIdAlmacen > 0 Then Comm.CommandText += " and cd.idalmacen=" + pIdAlmacen.ToString
        If pIdTipoProv > 0 Then Comm.CommandText += " and p.idtipo=" + pIdTipoProv.ToString
        If pIdInventario > 1 Then
            Comm.CommandText += " and cd.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then Comm.CommandText += " and i.idclasificacion=" + pidClasificacion.ToString
            If pidClasificacion2 > 0 Then Comm.CommandText += " and i.idclasificacion2=" + pidClasificacion.ToString
            If pidClasificacion3 > 0 Then Comm.CommandText += " and i.idclasificacion3=" + pidClasificacion.ToString
        End If
        Comm.CommandText += " order by c.fecha,referencia"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasrep")
        Return DS.Tables("tblcomprasrep").DefaultView
    End Function
    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, pIdTipoSucursal As Integer, ByVal pUsado As Integer, pIdTipoProv As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idremision idventa,v.fecha,v.folioi as folio,concat(v.folio,' ',v.serie) as serie,v.estado,if(v.idmoneda=2,v.total,v.total*v.tipodecambio) as total,if(v.idmoneda=2,v.totalapagar,v.totalapagar*v.tipodecambio) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.usado " + _
            "from tblcomprasremisiones v inner join tblproveedores c on v.idproveedor=c.idproveedor inner join tblsucursales s on v.idsucursal=s.idsucursal where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idremision idventa,v.fecha,v.folioi as folio,concat(v.folio,' ',v.serie) as serie,v.estado,v.total as total,v.totalapagar as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.usado " + _
            "from tblcomprasremisiones v inner join tblproveedores c on v.idproveedor=c.idproveedor inner join tblsucursales s on v.idsucursal=s.idsucursal where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pUsado = 0 Then Comm.CommandText += " and v.usado=0"
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idproveedor=" + pIdCliente.ToString
        If pIdTipoProv > 0 Then Comm.CommandText += " and c.idtipo=" + pIdTipoProv.ToString
        If pSoloCanceladas Then
            Comm.CommandText += " and v.estado=4"
        Else
            Comm.CommandText += " and v.estado=3"
        End If
        Comm.CommandText += " order by v.fecha,v.serie,v.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblremisiones")
        'DS.WriteXmlSchema("tblremisiones.xml")
        Return DS.Tables("tblremisiones").DefaultView
    End Function

    Public Function RevisaConceptos(ByVal pIdcompra As Integer) As Boolean
        Dim C1 As Integer
        Dim C2 As Integer
        Comm.CommandText = "select count(idmoneda) from tblcomprasremisionesdetalles where idremision=" + pIdcompra.ToString + " and idmoneda=2"
        C1 = Comm.ExecuteScalar
        Comm.CommandText = "select count(idmoneda) from tblcomprasremisionesdetalles where idremision=" + pIdcompra.ToString + " and idmoneda<>2"
        C2 = Comm.ExecuteScalar
        If C1 <> 0 And C2 <> 0 Then
            Return False
        End If
        Return True
    End Function
End Class
