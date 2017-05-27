Public Class dbDevolucionesCompras
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdProvedor As Integer
    Public Fecha As String
    Public Proveedor As dbproveedores
    Public Folio As String
    'Public Desglosar As Byte
    Public Facturado As Byte
    Public Credito As Double
    Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    'Public Serie As String
    'Public NoCertificado As String
    'Public NoAprobacion As String
    'Public YearAprobacion As String
    'Public EsElectronica As Byte
    Public Estado As Byte
    'Public SerieCertificado As String
    Public IdSucursal As Integer
    Public IdFormadePago As Integer
    Public TipodeCambio As Double
    Public IdConversion As Integer
    Public TotalVenta As Double
    Public Subtototal As Double
    Public TotalIva As Double
    Public TotalIeps As Double
    Public TotalIvaretenidoCon As Double
    'Public TotalISR As Double
    'Public TotalIvaRetenido As Double
    'Public ISR As Double
    'Public IvaRetenido As Double
    Public idCompra As Integer
    Public IdRemision As Integer
    Public Comentario As String
    Public Serie As String
    Public Folioi As Integer
    Public Foliocfdi As String
    Public FechaCancelado As String
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdProvedor = -1
        Fecha = ""
        Hora = ""
        Folio = 0
        'Desglosar = 0
        Facturado = 0
        Credito = 0
        Iva = 0
        TotalaPagar = 0
        Total = 0
        Comentario = ""
        'Serie = ""
        'NoCertificado = ""
        'NoAprobacion = ""
        'YearAprobacion = ""
        'EsElectronica = 0
        Estado = 0
        IdSucursal = 0
        IdFormadePago = 0
        TipodeCambio = 0
        IdConversion = 0
        'ISR = 0
        'IvaRetenido = 0
        idcompra = 0
        IdRemision = 0
        Folioi = 0
        Serie = ""
        Foliocfdi = ""
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
        Comm.CommandText = "select * from tbldevolucionescompras where iddevolucion=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdProvedor = DReader("idproveedor")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            'Desglosar = DReader("desglosar")
            Facturado = DReader("facturado")
            Credito = DReader("credito")
            Iva = DReader("iva")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            'Serie = DReader("serie")
            'NoAprobacion = DReader("noaprobacion")
            'NoCertificado = DReader("nocertificado")
            'YearAprobacion = DReader("yearaprobacion")
            'EsElectronica = DReader("eselectronica")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            IdFormadePago = DReader("idforma")
            TipodeCambio = DReader("tipodecambio")
            IdConversion = DReader("idconversion")
            'IvaRetenido = DReader("ivaretenido")
            'ISR = DReader("isr")
            idCompra = DReader("idcompra")
            IdRemision = DReader("idremision")
            Comentario = DReader("comentario")
            Serie = DReader("serie")
            Folioi = DReader("folioi")
            Foliocfdi = DReader("uuid")
            FechaCancelado = DReader("fechacancelado")
        End If
        DReader.Close()
        Proveedor = New dbproveedores(IdProvedor, Comm.Connection)
    End Sub
    'Public Function ExisteFolio(ByVal pfolio As Integer, Optional ByVal iddevolucion As Integer = -1) As Boolean
    '   Folio = pfolio
    '  Comm.CommandText = "select count(folio) from tbldevoluciones where folio=" + Folio.ToString + If(iddevolucion = -1, "", " and iddevolucion<>" + CStr(iddevolucion))
    ' If Comm.ExecuteScalar = 0 Then Return False Else Return True
    'End Function

    Public Sub Guardar(ByVal pIdProveedor As Integer, ByVal pFecha As String, ByVal pFolio As String, ByVal pIva As Double, ByVal pidSucursal As Integer, ByVal pIdFormaDepago As Integer, ByVal pTipodeCambio As Double, ByVal pIdConversion As Integer, ByVal pidVenta As Integer, ByVal pidRemision As Integer, ByVal pSerie As String, ByVal pFolioi As Integer, ByVal pfolioCFDI As String)
        'NoAprobacion = pNoAprobacion
        'NoCertificado = pNoCertificado
        'YearAprobacion = pYearAprovacion
        'EsElectronica = pEsElectronica
        IdProvedor = pIdProveedor
        Fecha = pFecha
        Folio = pFolio
        'Desglosar = pDesglosar
        Iva = pIva
        'Serie = pSerie
        IdSucursal = pidSucursal
        IdFormadePago = pIdFormaDepago
        TipodeCambio = pTipodeCambio
        IdConversion = pIdConversion
        'IvaRetenido = pIvaretenido
        'ISR = pIsr
        idCompra = pidVenta
        IdRemision = pidRemision
        Comm.CommandText = "insert into tbldevolucionescompras(idproveedor,fecha,folio,facturado,credito,iva,totalapagar,total,hora,estado,idsucursal,idforma,tipodecambio,idconversion,fechacancelado,horacancelado,idcompra,idremision,comentario,serie,folioi,uuid,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + IdProvedor.ToString + ",'" + Fecha + "','" + Replace(Folio, "'", "''") + "',0,0," + Iva.ToString + ",0,0,'" + Format(TimeOfDay, "HH:mm:ss") + "',1," + IdSucursal.ToString + "," + IdFormadePago.ToString + "," + TipodeCambio.ToString + "," + IdConversion.ToString + ",'',''," + idCompra.ToString + "," + IdRemision.ToString + ",'','" + Replace(pSerie.Trim, "'", "''") + "'," + pFolioi.ToString + ",'" + Replace(pfolioCFDI, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(iddevolucion) from tbldevolucionescompras"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As String, ByVal pIva As Double, ByVal pEstado As Byte, ByVal pIdFormadePago As Integer, ByVal pCredito As Byte, ByVal pTipodeCambio As Double, ByVal pidConversion As Integer, ByVal pSubTotal As Double, ByVal pTotal As Double, ByVal pIdProveedor As Integer, ByVal pComentario As String, ByVal pSerie As String, ByVal pFolioi As Integer, ByVal pFolioCFDI As String)
        ID = pID
        Fecha = pFecha
        Folio = pFolio
        'Desglosar = pDesglosar
        Iva = pIva
        'Serie = pSerie
        'NoAprobacion = pNoAprobacion
        'NoCertificado = pNoCertificado
        'YearAprobacion = pYearAprovacion
        'EsElectronica = pEsElectronica
        Estado = pEstado
        Credito = Credito
        IdFormadePago = pIdFormadePago
        TipodeCambio = pTipodeCambio
        IdConversion = pidConversion
        IdProvedor = pIdProveedor
        Comentario = pComentario
        Comm.CommandText = "update tbldevolucionescompras set fecha='" + Fecha + "',folio='" + Replace(Folio, "'", "''") + "',iva=" + Iva.ToString + ",estado=" + Estado.ToString + ",idforma=" + IdFormadePago.ToString + ",credito=" + Credito.ToString + ",tipodecambio=" + TipodeCambio.ToString + ",idconversion=" + IdConversion.ToString + ",total=" + pSubTotal.ToString + ",totalapagar=" + pTotal.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',idproveedor=" + IdProvedor.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "',serie='" + Replace(pSerie.Trim, "'", "''") + "',folioi=" + pFolioi.ToString + ",uuid='" + Replace(pFolioCFDI.Trim, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where iddevolucion=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folioi) from tbldevolucionescompras where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado>=2)),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Sub ActualizaComentario(ByVal piddevolucion As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tbldevolucionescompras set comentario='" + Replace(pTexto, "'", "''") + "' where iddevolucion=" + piddevolucion.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbldevolucionesdetallesc where iddevolucion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminarDetalles(ByVal pID As Integer)
        Comm.CommandText = "delete from tbldevolucionescompras where iddevolucion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, Optional ByVal pNombreClave As String = "", Optional ByVal pFolio As String = "", Optional ByVal pEstado As Byte = 0, Optional ByVal pCredido As Byte = 200) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevolucionescompras.iddevolucion,tbldevolucionescompras.fecha,concat(tbldevolucionescompras.serie,convert(tbldevolucionescompras.folioi using utf8),' ',tbldevolucionescompras.folio),tblproveedores.clave,tblproveedores.nombre as Cliente,case tbldevolucionescompras.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado from tbldevolucionescompras inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblproveedores.clave,tblproveedores.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and tbldevolucionescompras.folio like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado <> 0 Then
            Comm.CommandText += " and tbldevolucionescompras.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tbldevolucionescompras.estado<>1"
        End If
        'If pCredido <> 200 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=" + pCredido.ToString
        ' End If
        Comm.CommandText += " order by tbldevolucionescompras.fecha desc,tbldevolucionescompras.serie,tbldevolucionescompras.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevolucionescompras")
        Return DS.Tables("tbldevolucionescompras").DefaultView
    End Function

    Public Function ConsultaDeudas(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidCliente As Integer, ByVal pFolio As String, ByVal pidTipodePago As Integer, ByVal PorFechas As Boolean, ByVal Todas As Boolean, ByVal pTipodeOrden As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevolucionescompras.iddevolucion,0 as sel,tbldevolucionescompras.fecha,tbldevolucionescompras.folio,tbldevolucionescompras.credito,tbldevolucionescompras.totalapagar,tbldevolucionescompras.totalapagar-tbldevolucionescompras.credito as restante from tbldevolucionescompras inner join tblproveedores on tbldevoluciones.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tbldevolucionescompras.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.idproveedor=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        If Todas = False Then
            Comm.CommandText += " and tbldevolucionescompras.credito<tbldevolucionescompras.totalapagar"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and tbldevolucionescompras.folio like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pTipodeOrden = 0 Then
            Comm.CommandText += " order by tbldevolucionescompras.fecha,tbldevolucionescompras.folio"
        Else
            Comm.CommandText += " order by tbldevolucionescompras.totalapagar,tbldevolucionescompras.folio"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevolucionescompras")
        Return DS.Tables("tbldevolucionescompras").DefaultView
    End Function

    Public Function DaTotal(ByVal piddevolucion As Integer, ByVal pidMoneda As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0
        'Dim Encontro As Double
        Dim iIva As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double
        Dim pIEPS As Double
        Dim pIVARetenido As Double
        'Dim iIsr As Double
        'Dim iIvaRetenido As Double
        Subtototal = 0
        TotalIva = 0
        TotalVenta = 0
        TotalIeps = 0
        TotalIvaretenidoCon = 0
        Comm.CommandText = "select tipodecambio from tbldevolucionescompras where iddevolucion=" + piddevolucion.ToString
        iTipoCambio = Comm.ExecuteScalar
        'Comm.CommandText = "select isr from tbldevolucionescompras where iddevolucion=" + piddevolucion.ToString
        'iIsr = Comm.ExecuteScalar
        'Comm.CommandText = "select ivaretenido from tbldevolucionescomppas where iddevolucion=" + piddevolucion.ToString
        'iIvaRetenido = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tbldevolucionesdetallesc where iddevolucion=" + piddevolucion.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tbldevolucionesdetallesc where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select iva from tbldevolucionesdetallesc where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tbldevolucionesdetallesc where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            Comm.CommandText = "select ieps from tbldevolucionesdetallesc where iddetalle=" + IDs.Item(Cont).ToString
            pIEPS = Comm.ExecuteScalar
            Comm.CommandText = "select ivaRetenido from tbldevolucionesdetallesc where iddetalle=" + IDs.Item(Cont).ToString
            pIVARetenido = Comm.ExecuteScalar
            If pidMoneda = 2 Then
                If pidMoneda <> IdMonedaC Then
                    Precio = Precio * iTipoCambio
                End If
            Else
                If IdMonedaC = 2 Then
                    Precio = Precio / iTipoCambio
                End If
            End If
            TotalIeps += (Precio * (pIEPS / 100))
            TotalIvaretenidoCon += (Precio * (pIVARetenido / 100))

            Subtototal += Precio
            TotalIva += (Precio * (iIva / 100))
            Cont += 1
        End While
        'TotalISR = Subtototal * (iIsr / 100)
        'TotalIvaRetenido = Subtototal * (iIvaRetenido / 100)
        TotalVenta = Subtototal + TotalIva + TotalIeps - TotalIvaretenidoCon '- TotalISR - TotalIvaRetenido
        Return TotalVenta
    End Function
    'Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
    '   Comm.CommandText = "select ifnull((select max(folio) from tbldevoluciones where serie='" + pSerie + "' and (estado=3 or estado=4) ),0)"
    '   DaNuevoFolio = Comm.ExecuteScalar + 1
    'End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pIdProveedor As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folioi) from tbldevolucionescompras where folioi=" + pFolio.ToString + " and estado<>1 and estado<>2 and serie='" + Replace(pSerie.Trim, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    'Public Sub SetnFacturado(ByVal piddevolucion As Integer, ByVal pTipo As TiposFactura, ByVal pCredito As Byte, ByVal pTotal As Double)
    '    Dim Tipo As Byte
    '    Tipo = pTipo
    '    Total = pTotal
    '    If pCredito = 0 Then
    '        Comm.CommandText = "update tbldevoluciones set facturado=" + Tipo.ToString + ",total=" + Total.ToString + ",hora='" + Format(Date.Today, "HH:mm:ss") + "' where iddevolucion=" + piddevolucion.ToString
    '    Else
    '        Comm.CommandText = "update tbldevoluciones set facturado=" + Tipo.ToString + ",totalapagar=" + Total.ToString + ",credito=1,total=" + Total.ToString + ",hora='" + Format(Date.Today, "HH:mm:ss") + "' where iddevolucion=" + piddevolucion.ToString
    '    End If
    '    Comm.ExecuteNonQuery()
    'End Sub

   

    'Public ReadOnly Property totalLetra(ByVal idmoneda As Integer) As String
    '    Get
    '        Dim f As New StringFunctions
    '        Return f.PASELETRAS(DaTotal(ID, idmoneda), idmoneda)
    '    End Get
    'End Property


    Public Sub AgregarDetallesReferencia(ByVal Piddevolucion As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte)
        '0 cotizacion
        '1 pedido
        '2 remision
        '3 ventas

        'If Tipo = 0 Then
        '    Comm.CommandText = "insert into tbldevolucionesinventario(iddevolucion,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio) select " + Piddevolucion.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0 from tbldevolucionescotizacionesinventario where idcotizacion=" + pIdDocumento.ToString
        '    Comm.ExecuteNonQuery()

        '    'Comm.CommandText = "insert into tbldevolucionesproductos(iddevolucion,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + Piddevolucion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento from tbldevolucionescotizacionesproductos where idcotizacion=" + pIdDocumento.ToString
        '    'Comm.ExecuteNonQuery()
        'End If

        'If Tipo = 1 Then
        '    Comm.CommandText = "insert into tbldevolucionesinventario(iddevolucion,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio) select " + Piddevolucion.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento,idvariante,0 from tbldevolucionespedidosinventario where idpedido=" + pIdDocumento.ToString
        '    Comm.ExecuteNonQuery()

        '    'Comm.CommandText = "insert into tbldevolucionesproductos(iddevolucion,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + Piddevolucion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda," + pidAlmacen.ToString + ",iva,extra,descuento from tbldevolucionespedidosproductos where idpedido=" + pIdDocumento.ToString
        '    'Comm.ExecuteNonQuery()
        'End If

        If Tipo = 2 Then
            Comm.CommandText = "insert into tbldevolucionesdetallesc(iddevolucion,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,descuento,IEPS,ivaRetenido) " + _
            "select " + Piddevolucion.ToString + "," + _
            "tblcomprasremisionesdetalles.idinventario," + _
            "tblcomprasremisionesdetalles.cantidad-(ifnull((select sum(tbldevolucionesdetallesc.cantidad) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idremision=tblcomprasremisionesdetalles.idremision and tbldevolucionesdetallesc.idinventario=tblcomprasremisionesdetalles.idinventario and tbldevolucionescompras.estado=3),0))," + _
            "tblcomprasremisionesdetalles.precio/tblcomprasremisionesdetalles.cantidad*(tblcomprasremisionesdetalles.cantidad-(ifnull((select sum(tbldevolucionesdetallesc.cantidad) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idremision=tblcomprasremisionesdetalles.idremision and tblcomprasremisionesdetalles.idinventario=tbldevolucionesdetallesc.idinventario and tbldevolucionescompras.estado=3),0)))," + _
            "tblinventario.nombre," + _
            "tblcomprasremisionesdetalles.idmoneda," + _
            "tblcomprasremisionesdetalles.idalmacen," + _
            "tblcomprasremisionesdetalles.iva," + _
            "tblcomprasremisionesdetalles.descuento, " + _
            "tblcomprasremisionesdetalles.IEPS, " + _
            "tblcomprasremisionesdetalles.ivaRetenido " + _
            "from tblcomprasremisionesdetalles inner join tblinventario on tblcomprasremisionesdetalles.idinventario=tblinventario.idinventario where tblinventario.inventariable=1 and idremision=" + pIdDocumento.ToString + " and (tblcomprasremisionesdetalles.cantidad-(ifnull((select sum(tbldevolucionesdetallesc.cantidad) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idremision=tblcomprasremisionesdetalles.idremision and tbldevolucionesdetallesc.idinventario=tblcomprasremisionesdetalles.idinventario and tbldevolucionescompras.estado=3),0)))>0"
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tbldevolucionesproductos(iddevolucion,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + Piddevolucion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento from tbldevolucionesremisionesproductos where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tbldevolucionesservicios(iddevolucion,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + Piddevolucion.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tbldevolucionesremisionesservicios where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblinventarioseries set iddevolucion=" + Piddevolucion.ToString + " where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 3 Then
            Comm.CommandText = "insert into tbldevolucionesdetallesc(iddevolucion,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,descuento,IEPS, ivaRetenido) " + _
            "select " + Piddevolucion.ToString + "," + _
            "tblcomprasdetalles.idinventario," + _
            "tblcomprasdetalles.cantidad-(ifnull((select sum(tbldevolucionesdetallesc.cantidad) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idcompra=tblcomprasdetalles.idcompra and tbldevolucionesdetallesc.idinventario=tblcomprasdetalles.idinventario and tbldevolucionescompras.estado=3),0))," + _
            "tblcomprasdetalles.precio/tblcomprasdetalles.cantidad*(tblcomprasdetalles.cantidad-(ifnull((select sum(tbldevolucionesdetallesc.cantidad) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idcompra=tblcomprasdetalles.idcompra and tbldevolucionesdetallesc.idinventario=tblcomprasdetalles.idinventario and tbldevolucionescompras.estado=3),0)))," + _
            "tblinventario.nombre," + _
            "tblcomprasdetalles.idmoneda," + _
            "tblcomprasdetalles.idalmacen," + _
            "tblcomprasdetalles.iva," + _
            "tblcomprasdetalles.descuento, " + _
             "tblcomprasdetalles.IEPS, " + _
            "tblcomprasdetalles.ivaRetenido " + _
            "from tblcomprasdetalles inner join tblinventario on tblcomprasdetalles.idinventario=tblinventario.idinventario where tblinventario.inventariable=1 and idcompra=" + pIdDocumento.ToString + " and (tblcomprasdetalles.cantidad-(ifnull((select sum(tbldevolucionesdetallesc.cantidad) from tbldevolucionesdetallesc inner join tbldevolucionescompras on tbldevolucionesdetallesc.iddevolucion=tbldevolucionescompras.iddevolucion where tbldevolucionescompras.idcompra=tblcomprasdetalles.idcompra and tbldevolucionesdetallesc.idinventario=tblcomprasdetalles.idinventario and tbldevolucionescompras.estado=3),0)))>0"
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tbldevolucionesproductos(iddevolucion,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + Piddevolucion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento from tbldevolucionesproductos where iddevolucion=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tbldevolucionesservicios(iddevolucion,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + Piddevolucion.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tbldevolucionesservicios where iddevolucion=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Sub RegresaInventario(ByVal pId As Integer)
        'Dim Str As String = ""
        'Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        'Comm.CommandText = "select idalmacen,cantidad,idinventario from tbldevoluciones inner join tbldevolucionesdetalles on tbldevoluciones.iddevolucion=tbldevolucionesdetalles.iddevolucion where tbldevoluciones.iddevolucion=" + pId.ToString
        'DReader = Comm.ExecuteReader
        'While DReader.Read()
        '    Str += "update tblalmacenesi set cantidad=cantidad-" + DReader("cantidad").ToString + " where idinventario=" + DReader("idinventario").ToString + " and idalmacen=" + DReader("idalmacen").ToString + "; "
        'End While
        'DReader.Close()
        'Comm.CommandText = Str
        'If Str <> "" Then
        '    Comm.ExecuteNonQuery()
        'End If
        Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,cantidad,0,0,1) from tbldevolucionesdetallesc where iddevolucion=" + pId.ToString + ";"
        Comm.CommandText += "select spmodificainventariolotesf(tbldevolucionesdetallesc.idinventario,tbldevolucionesdetallesc.idalmacen,tbldevolucionesclotes.surtido,0,0,1,tbldevolucionesclotes.idlote) from tbldevolucionesclotes inner join tbldevolucionesdetallesc on tbldevolucionesclotes.iddetalle=tbldevolucionesdetallesc.iddetalle where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
        Comm.CommandText += "select spmodificainventarioaduanaf(tbldevolucionesdetallesc.idinventario,tbldevolucionesdetallesc.idalmacen,tbldevolucionescaduana.surtido,0,0,1,tbldevolucionescaduana.idaduana) from tbldevolucionescaduana inner join tbldevolucionesdetallesc on tbldevolucionescaduana.iddetalle=tbldevolucionesdetallesc.iddetalle where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
        'Comm.CommandText = "update tbldevolucionesdetallesc set surtido=0 where iddevolucion=" + pId.ToString + ";"
        Comm.CommandText = "update tbldevolucionesclotes inner join tbldevolucionesdetallesc on tbldevolucionesclotes.iddetalle=tbldevolucionesdetallesc.iddetalle set tbldevolucionesclotes.surtido=0 where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
        Comm.CommandText += "update tbldevolucionescaduana inner join tbldevolucionesdetallesc on tbldevolucionescaduana.iddetalle=tbldevolucionesdetallesc.iddetalle set tbldevolucionescaduana.surtido=0 where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificaInventario(ByVal pId As Integer)
        'idinventario,idalmacen,surtido,0,1,1
        Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,cantidad,0,1,1) from tbldevolucionesdetallesc where iddevolucion=" + pId.ToString + ";"
        'Comm.CommandText += "update tbldevolucionesdetallesc set surtido=cantidad where iddevolucion=" + pId.ToString + "; "
        Comm.CommandText += "select spmodificainventariolotesf(tbldevolucionesdetallesc.idinventario,tbldevolucionesdetallesc.idalmacen,tbldevolucionesclotes.cantidad-tbldevolucionesclotes.surtido,0,1,1,tbldevolucionesclotes.idlote) from tbldevolucionesclotes inner join tbldevolucionesdetallesc on tbldevolucionesclotes.iddetalle = tbldevolucionesdetallesc.iddetalle where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + "; "
        Comm.CommandText += "select spmodificainventarioaduanaf(tbldevolucionesdetallesc.idinventario,tbldevolucionesdetallesc.idalmacen,tbldevolucionescaduana.cantidad-tbldevolucionescaduana.surtido,0,1,1,tbldevolucionescaduana.idaduana) from tbldevolucionescaduana inner join tbldevolucionesdetallesc on tbldevolucionescaduana.iddetalle = tbldevolucionesdetallesc.iddetalle where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + "; "
        Comm.CommandText += "update tbldevolucionesclotes inner join tbldevolucionesdetallesc on tbldevolucionesclotes.iddetalle=tbldevolucionesdetallesc.iddetalle set tbldevolucionesclotes.surtido=tbldevolucionesclotes.cantidad where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
        Comm.CommandText += "update tbldevolucionescaduana inner join tbldevolucionesdetallesc on tbldevolucionescaduana.iddetalle=tbldevolucionesdetallesc.iddetalle set tbldevolucionescaduana.surtido=tbldevolucionescaduana.cantidad where tbldevolucionesdetallesc.iddevolucion=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()

        'Comm.CommandText = "update tblproductos inner join tblproductosvariantes on tblproductos.idproducto=tblproductosvariantes inner join tblcomprasdetalles on tblproductosvariantes.idvariante=tblcomprasdetalles.idvariante set tblproductos.costo=spsacacostoproducto(tblproductos.idproducto," + pTipoCosteo.ToString + ") where tblcomprasdetalles.idcompra=" + pId.ToString + " and tblcomprasdetalles.idvariante>1"
        'Comm.ExecuteNonQuery()
    End Sub
    'Public Sub ModificaInventario(ByVal pId As Integer)
    '    Dim Str As String = ""
    '    Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim IDs As New Collection
    '    Dim Cont As Integer = 1
    '    Comm.CommandText = "select iddetalle from tbldevolucionesdetallesc where iddevolucion=" + ID.ToString
    '    DReader = Comm.ExecuteReader
    '    While DReader.Read()
    '        IDs.Add(DReader("iddetalle"))
    '    End While
    '    DReader.Close()
    '    Dim I As New dbInventario(MySqlcon)
    '    'Dim PV As New dbProductosVariantes(MySqlcon)
    '    Dim iIdInventario As Integer
    '    'Dim iIdVariante As Integer
    '    Dim iCantidad As Double
    '    Dim iIdAlmacen As Integer
    '    While Cont <= IDs.Count
    '        Comm.CommandText = "select idinventario,cantidad,idalmacen from tbldevolucionesdetallesc where iddetalle=" + IDs(Cont).ToString
    '        DReader = Comm.ExecuteReader
    '        If DReader.Read() Then
    '            iIdInventario = DReader("idinventario")
    '            'iIdVariante = DReader("idvariante")
    '            iCantidad = DReader("cantidad")
    '            iIdAlmacen = DReader("idalmacen")
    '            DReader.Close()
    '            If iIdInventario > 1 Then
    '                I.MovimientoDeInventario(iIdInventario, iCantidad, 0, dbInventario.TipoMovimiento.Baja, iIdAlmacen)
    '            End If
    '            'If iIdVariante > 1 Then
    '            'PV.ModificaInventario(iIdVariante, iCantidad * -1, iIdAlmacen)
    '            'End If
    '        Else
    '        DReader.Close()
    '        End If

    '        Cont += 1
    '    End While


    'End Sub


    'Public Function CreaCadenaOriginal(ByVal piddevolucion As Integer, ByVal pIdMoneda As Integer) As String
    '    Dim O As New dbOpciones(Comm.Connection)
    '    Dim CO As String = "|2.0|"
    '    ID = piddevolucion
    '    LlenaDatos()
    '    'Dim TI As Double
    '    If TipodeCambio = 0 Then TipodeCambio = 1
    '    'Dim CI As Double
    '    DaTotal(ID, 2)
    '    Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
    '    'CI = TI * (Iva / 100)
    '    CO += Serie + "|"
    '    CO += Folio.ToString + "|"
    '    CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
    '    CO += NoAprobacion + "|"
    '    CO += YearAprobacion + "|"
    '    CO += "egreso|Pago en una sola exhibición|"
    '    'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
    '    CO += Format(Subtototal, "#0.00") + "|"
    '    CO += "0|" 'descuento

    '    CO += Format(TotalVenta, "#0.00") + "|" ' total factura con iva

    '    CO += Sucursal.RFC + "|"
    '    CO += Sucursal.NombreFiscal + "|"
    '    CO += Sucursal.Direccion + "|"
    '    CO += Sucursal.NoExterior + "|"
    '    CO += Sucursal.NoInterior + "|"
    '    CO += Sucursal.Colonia + "|"
    '    CO += Sucursal.Ciudad + "|"
    '    CO += Sucursal.ReferenciaDomicilio + "|"
    '    CO += Sucursal.Municipio + "|"
    '    CO += Sucursal.Estado + "|"
    '    CO += Sucursal.Pais + "|"
    '    CO += Sucursal.CP + "|"

    '    CO += Sucursal.Direccion2 + "|"
    '    CO += Sucursal.NoExterior2 + "|"
    '    CO += Sucursal.NoInterior2 + "|"
    '    CO += Sucursal.Colonia2 + "|"
    '    CO += Sucursal.Ciudad2 + "|"
    '    CO += Sucursal.ReferenciaDomicilio2 + "|"
    '    CO += Sucursal.Municipio2 + "|"
    '    CO += Sucursal.Estado2 + "|"
    '    CO += Sucursal.Pais2 + "|"
    '    CO += Sucursal.CP2 + "|"

    '    CO += Proveedor.RFC + "|"
    '    CO += Proveedor.Nombre + "|"
    '    If Cliente.DireccionFiscal = 0 Then
    '        CO += Proveedor.Direccion + "|"
    '        CO += Proveedor.NoExterior + "|"
    '        CO += Proveedor.NoInterior + "|"
    '        CO += Proveedor.Colonia + "|"
    '        CO += Proveedor.Ciudad + "|"
    '        CO += Proveedor.ReferenciaDomicilio + "|"
    '        CO += Proveedor.Municipio + "|"
    '        CO += Proveedor.Estado + "|"
    '        CO += Proveedor.Pais + "|"
    '        CO += Proveedor.CP + "|"
    '    Else
    '        CO += Cliente.Direccion2 + "|"
    '        CO += Cliente.NoExterior2 + "|"
    '        CO += Cliente.NoInterior2 + "|"
    '        CO += Cliente.Colonia2 + "|"
    '        CO += Cliente.Ciudad2 + "|"
    '        CO += Cliente.ReferenciaDomicilio2 + "|"
    '        CO += Cliente.Municipio2 + "|"
    '        CO += Cliente.Estado2 + "|"
    '        CO += Cliente.Pais2 + "|"
    '        CO += Cliente.CP2 + "|"
    '    End If

    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbDevolucionesDetalles(MySqlcon)
    '    DR = VI.ConsultaReader(ID)
    '    'Dim PrecioTemp As Double
    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        CO += DR("descripcion") + "|"
    '        If DR("idmoneda") <> 2 Then
    '            CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
    '            CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
    '        Else
    '            CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '            CO += Format(DR("precio"), "#0.00") + "|"
    '        End If

    '    End While
    '    DR.Close()

    '    'Dim VP As New dbVentasProductos(MySqlcon)
    '    'DR = VP.ConsultaReader(ID)

    '    'While DR.Read
    '    '    CO += DR("cantidad").ToString + "|"
    '    '    CO += DR("tipocantidad") + "|"
    '    '    'CO += DR("clave") + "|"
    '    '    CO += DR("descripcion") + "|"
    '    '    If DR("idmoneda") <> 2 Then
    '    '        CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
    '    '        CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
    '    '    Else
    '    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    '    End If
    '    'End While
    '    'DR.Close()

    '    'Dim VS As New dbVentasServicios(MySqlcon)
    '    'DR = VS.ConsultaReader(ID)

    '    'While DR.Read
    '    '    CO += DR("cantidad").ToString + "|"
    '    '    CO += "SERV|"
    '    '    CO += DR("folio") + "|"
    '    '    CO += DR("descripcion") + "|"
    '    '    If DR("idmoneda") <> 2 Then
    '    '        CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
    '    '        CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
    '    '    Else
    '    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    '    End If
    '    'End While
    '    'DR.Close()

    '    If ISR <> 0 Then
    '        CO += "ISR|" + Format(TotalISR, "#0.00") + "|"
    '    End If
    '    If IvaRetenido <> 0 Then
    '        CO += "IVA|" + Format(TotalIvaRetenido, "#0.00") + "|"
    '    End If
    '    If ISR <> 0 Or IvaRetenido <> 0 Then
    '        CO += Format(TotalISR + TotalIvaRetenido, "#0.00") + "|"
    '    End If
    '    Dim Ivas As New Collection
    '    Dim IvasImporte As New Collection
    '    DR = DaIvas(ID)
    '    Dim IAnt As Double
    '    While DR.Read
    '        If Ivas.Contains(DR("iva").ToString) = False Then
    '            Ivas.Add(DR("iva"), DR("iva").ToString)
    '        End If
    '        If IvasImporte.Contains(DR("iva").ToString) = False Then
    '            If DR("idmoneda") <> 2 Then
    '                IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
    '            Else
    '                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
    '            End If
    '        Else
    '            IAnt = IvasImporte(DR("iva").ToString)
    '            IvasImporte.Remove(DR("iva").ToString)
    '            If DR("idmoneda") <> 2 Then
    '                IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
    '            Else
    '                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
    '            End If
    '        End If
    '    End While
    '    DR.Close()
    '    For Each I As Double In Ivas
    '        CO += "IVA|"
    '        CO += CInt(I).ToString + "|"
    '        CO += Format(IvasImporte(I.ToString), "#0.00") + "|"
    '    Next

    '    CO += Format(TotalIva, "#0.00") + "|"

    '    While CO.IndexOf("||") <> -1
    '        CO = Replace(CO, "||", "|")
    '    End While
    '    While CO.IndexOf("  ") <> -1
    '        CO = Replace(CO, "  ", " ")
    '    End While
    '    CO = "|" + CO + "|"
    '    Return CO

    'End Function





    'Public Function CreaXML(ByVal piddevolucion As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String) As String
    '    Dim O As New dbOpciones(Comm.Connection)
    '    Dim en As New Encriptador
    '    Dim XMLDoc As String
    '    Dim Ivas As New Collection
    '    Dim IvasImporte As New Collection
    '    XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

    '    XMLDoc += "<Comprobante " + vbCrLf

    '    en.Leex509(My.Settings.rutacer)
    '    ID = piddevolucion
    '    LlenaDatos()
    '    If TipodeCambio = 0 Then TipodeCambio = 1
    '    DaTotal(ID, 2)
    '    Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
    '    If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    XMLDoc += "version = ""2.0""" + vbCrLf
    '    XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
    '    XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
    '    If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
    '    If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf

    '    XMLDoc += "subTotal=""" + Format(Subtototal, "#0.00") + """" + vbCrLf

    '    XMLDoc += "total=""" + Format(TotalVenta, "#0.00") + """" + vbCrLf
    '    If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
    '    If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
    '    XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf
    '    XMLDoc += "descuento=""" + "0" + """" + vbCrLf
    '    XMLDoc += "tipoDeComprobante=""egreso""" + vbCrLf
    '    XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf
    '    XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
    '    XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
    '    XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf

    '    XMLDoc += ">"

    '    XMLDoc += "<Emisor rfc=""" + Sucursal.RFC + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf

    '    XMLDoc += "<DomicilioFiscal " + vbCrLf
    '    If Sucursal.Direccion <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    If Sucursal.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    If Sucursal.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    If Sucursal.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    If Sucursal.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    If Sucursal.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    If Sucursal.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    If Sucursal.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    If Sucursal.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    If Sucursal.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    XMLDoc += "/>" + vbCrLf
    '    If Sucursal.Pais2 <> "" Then
    '        XMLDoc += "<ExpedidoEn  " + vbCrLf


    '        If Sucursal.Direccion2 <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Sucursal.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Sucursal.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Sucursal.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf

    '        'If O._CalleLocal <> "" Then XMLDoc += "calle=""" + O._CalleLocal + """" + vbCrLf
    '        'If O._noExteriorLocal <> "" Then XMLDoc += "noExterior=""" + O._noExteriorLocal + """" + vbCrLf
    '        'If O._noInteriorLocal <> "" Then XMLDoc += "noInterior=""" + O._noInteriorLocal + """" + vbCrLf
    '        'If O._ColoniaLocal <> "" Then XMLDoc += "colonia=""" + O._ColoniaLocal + """" + vbCrLf
    '        'If O._LocalidadLocal <> "" Then XMLDoc += "localidad=""" + O._LocalidadLocal + """" + vbCrLf
    '        'If O._ReferenciaDomicilioLocal <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilioLocal + """" + vbCrLf
    '        'If O._MunicipioLocal <> "" Then XMLDoc += "municipio=""" + O._MunicipioLocal + """" + vbCrLf
    '        'If O._EstadoLocal <> "" Then XMLDoc += "estado=""" + O._EstadoLocal + """" + vbCrLf
    '        'If O._PaisLocal <> "" Then XMLDoc += "pais=""" + O._PaisLocal + """" + vbCrLf
    '        'If O._CodigoPostalLocal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostalLocal + """"
    '        XMLDoc += "/>" + vbCrLf
    '    End If

    '    XMLDoc += "</Emisor>" + vbCrLf


    '    XMLDoc += "<Receptor rfc=""" + Proveedor.RFC + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf
    '    If Cliente.DireccionFiscal = 0 Then
    '        If Proveedor.Direccion <> "" Then XMLDoc += "<Domicilio calle=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    Else
    '        If Cliente.Direccion2 <> "" Then XMLDoc += "<Domicilio calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Cliente.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Cliente.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Cliente.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Cliente.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Cliente.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Cliente.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Cliente.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Cliente.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Cliente.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    End If
    '    XMLDoc += "/>" + vbCrLf

    '    XMLDoc += "</Receptor>" + vbCrLf

    '    XMLDoc += "<Conceptos>" + vbCrLf

    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbDevolucionesDetalles(MySqlcon)
    '    DR = VI.ConsultaReader(ID)

    '    While DR.Read
    '        XMLDoc += "<Concepto " + vbCrLf
    '        XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
    '        XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(DR("descripcion"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If DR("idmoneda") <> 2 Then
    '            XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
    '            XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
    '            XMLDoc += "/> " + vbCrLf
    '        Else
    '            XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
    '            XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
    '            XMLDoc += "/> " + vbCrLf
    '        End If

    '    End While
    '    DR.Close()

    '    'Dim VP As New dbVentasProductos(MySqlcon)
    '    'DR = VP.ConsultaReader(ID)

    '    'While DR.Read

    '    '    XMLDoc += "<Concepto " + vbCrLf
    '    '    XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
    '    '    XMLDoc += "unidad=""" + DR("tipocantidad") + """" + vbCrLf
    '    '    XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
    '    '    If DR("idmoneda") <> 2 Then

    '    '        XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
    '    '        XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
    '    '        XMLDoc += "/> " + vbCrLf
    '    '    Else
    '    '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
    '    '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
    '    '        XMLDoc += "/> " + vbCrLf
    '    '    End If
    '    '    XMLDoc += "/> " + vbCrLf
    '    'End While
    '    'DR.Close()

    '    'Dim VS As New dbVentasServicios(MySqlcon)
    '    'DR = VS.ConsultaReader(ID)

    '    'While DR.Read

    '    '    XMLDoc += "<Concepto " + vbCrLf
    '    '    XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
    '    '    XMLDoc += "unidad=""SERV""" + vbCrLf
    '    '    XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
    '    '    If DR("idmoneda") <> 2 Then
    '    '        XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
    '    '        XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
    '    '        XMLDoc += "/> " + vbCrLf
    '    '    Else
    '    '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
    '    '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
    '    '        XMLDoc += "/> " + vbCrLf
    '    '    End If
    '    '    XMLDoc += "/> " + vbCrLf
    '    'End While
    '    'DR.Close()
    '    XMLDoc += "</Conceptos>"

    '    XMLDoc += "<Impuestos totalImpuestosTrasladados=""" + Format(TotalIva, "#0.00") + """ "
    '    If ISR <> 0 Or IvaRetenido <> 0 Then
    '        XMLDoc += "totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00") + """"
    '    End If
    '    XMLDoc += ">" + vbCrLf

    '    If ISR <> 0 Or IvaRetenido <> 0 Then
    '        XMLDoc += "<Retenciones>" + vbCrLf
    '        If ISR <> 0 Then
    '            XMLDoc += "<Retencion impuesto=""ISR""" + vbCrLf
    '            'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
    '            XMLDoc += "importe=""" + Format(TotalISR, "#0.00") + """ />" + vbCrLf
    '        End If

    '        If ISR <> 0 Then
    '            XMLDoc += "<Retencion impuesto=""IVA""" + vbCrLf
    '            'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
    '            XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00") + """ />" + vbCrLf
    '        End If

    '        XMLDoc += "</Retenciones>" + vbCrLf

    '    End If



    '    XMLDoc += "<Traslados>" + vbCrLf


    '    DR = DaIvas(ID)
    '    Dim IAnt As Double
    '    While DR.Read
    '        If Ivas.Contains(DR("iva").ToString) = False Then
    '            Ivas.Add(DR("iva"), DR("iva").ToString)
    '        End If
    '        If IvasImporte.Contains(DR("iva").ToString) = False Then
    '            If DR("idmoneda") <> 2 Then
    '                IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
    '            Else
    '                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
    '            End If
    '        Else
    '            IAnt = IvasImporte(DR("iva").ToString)
    '            IvasImporte.Remove(DR("iva").ToString)
    '            If DR("idmoneda") <> 2 Then
    '                IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
    '            Else
    '                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
    '            End If

    '        End If
    '    End While
    '    DR.Close()
    '    For Each I As Double In Ivas

    '        XMLDoc += "<Traslado impuesto=""IVA""" + vbCrLf
    '        XMLDoc += "tasa=""" + I.ToString + """" + vbCrLf
    '        XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00") + """ />" + vbCrLf

    '    Next



    '    XMLDoc += "</Traslados>" + vbCrLf





    '    XMLDoc += "</Impuestos>" + vbCrLf
    '    XMLDoc += "</Comprobante>"


    '    Return XMLDoc

    'End Function

    Public Function DaIvas(ByVal piddevolucion As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iva,precio,idmoneda from tbldevolucionesdetallesc where iddevolucion=" + piddevolucion.ToString
        Return Comm.ExecuteReader
    End Function

    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pSoloCanceladas As Boolean, ByVal pMostrarEnPesos As Byte, ByVal pidInventario As Integer, pIdTipoSucursal As Integer, pidTipoProv As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select td.iddevolucion,td.fecha,td.folio as serie,0 as folio,tdd.cantidad,if(tdd.idmoneda=2,tdd.precio,tdd.precio*td.tipodecambio) as precio,td.estado,tdd.iva,tdd.ieps,tdd.ivaretenido,if(td.idcompra<>0,'FACTURAS','REMISIONES') as tipodev," + _
            "(select clave from tblinventario where tblinventario.idinventario=tdd.idinventario) as clave," + _
            "(select nombre from tblinventario where tblinventario.idinventario=tdd.idinventario) as nombre, " + _
            "if(td.idcompra<>0,(select tblcompras.serie from tblcompras where tblcompras.idcompra=td.idcompra),(select tblcomprasremisiones.serie from tblcomprasremisiones where tblcomprasremisiones.idremision=td.idremision)) as docserie," +
            "if(td.idcompra<>0,(select tblcompras.folioi from tblcompras where tblcompras.idcompra=td.idcompra),(select tblcomprasremisiones.folioi from tblcomprasremisiones where tblcomprasremisiones.idremision=td.idremision)) as docfolio" +
            " from tbldevolucionescompras as td inner join tbldevolucionesdetallesc as tdd on td.iddevolucion=tdd.iddevolucion inner join tblsucursales s on td.idsucursal=s.idsucursal inner join tblproveedores p on td.idproveedor=p.idproveedor where td.fecha>='" + pFecha1 + "' and td.fecha<='" + pFecha2 + "' "
        Else
            Comm.CommandText = "select td.iddevolucion,td.fecha,td.folio as serie,0 as folio,tdd.cantidad,tdd.precio as precio,td.estado,tdd.iva,tdd.ieps,tdd.ivaretenido,if(td.idcompra<>0,'FACTURAS','REMISIONES') as tipodev," + _
            "(select clave from tblinventario where tblinventario.idinventario=tdd.idinventario) as clave," + _
            "(select nombre from tblinventario where tblinventario.idinventario=tdd.idinventario) as nombre, " + _
            "if(td.idcompra<>0,(select tblcompras.serie from tblcompras where tblcompras.idcompra=td.idcompra),(select tblcomprasremisiones.serie from tblcomprasremisiones where tblcomprasremisiones.idremision=td.idremision)) as docserie," +
            "if(td.idcompra<>0,(select tblcompras.folioi from tblcompras where tblcompras.idcompra=td.idcompra),(select tblcomprasremisiones.folioi from tblcomprasremisiones where tblcomprasremisiones.idremision=td.idremision)) as docfolio" +
            " from tbldevolucionescompras as td inner join tbldevolucionesdetallesc as tdd on td.iddevolucion=tdd.iddevolucion inner join tblsucursales s on td.idsucursal=s.idsucursal inner join tblproveedores p on td.idproveedor=p.idproveedor where td.fecha>='" + pFecha1 + "' and td.fecha<='" + pFecha2 + "' "
        End If
        If pSoloCanceladas Then
            Comm.CommandText += " and td.estado=4"
        Else
            Comm.CommandText += " and td.estado=3"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and td.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pidTipoProv > 0 Then Comm.CommandText += " and p.idtipo=" + pidTipoProv.ToString
        If pIdProveedor > 0 Then
            Comm.CommandText += " and td.idproveedor=" + pIdProveedor.ToString
        End If
        If pidInventario > 1 Then
            Comm.CommandText += " and tdd.idinventario=" + pidInventario.ToString
        End If
        '    Comm.CommandText += " and tbldevolucionesinventario.idinventario=" + pidInventario.ToString
        'Else
        '    If pidClasificacion > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
        '    End If
        '    If pidClasificacion2 > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion.ToString
        '    End If
        '    If pidClasificacion3 > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion.ToString
        '    End If
        'End If
        Comm.CommandText += " order by td.fecha,td.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevoluciones")
        'DS.WriteXmlSchema("tbldevoluciones.xml")
        Return DS.Tables("tbldevoluciones").DefaultView
    End Function
    Public Function ReportePorTipodePago(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, Optional ByVal pIdCliente As Integer = 0) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevolucionescompras.iddevolucion,tbldevolucionescompras.folio,tbldevolucionescompras.serie,tbldevolucionescompras.estado,if(tbldevolucionescompras.idconversion=2,tbldevolucionescompras.total,tbldevolucionescompras.total*tbldevolucionescompras.tipodecambio) as total,if(tbldevolucionescompras.idconversion=2,tbldevolucionescompras.totalapagar,tbldevolucionescompras.totalapagar*tbldevolucionescompras.tipodecambio) as totalapagar,tbldevolucionescompras.fecha,tbldevolucionescompras.tipodecambio,tbldevolucionescompras.idconversion,tbldevolucionesdetallesc.cantidad,tbldevolucionesdetallesc.descripcion,tbldevolucionesdetallesc.precio,0 as costoinv,0 as costopro,tbldevolucionesdetallesc.idinventario,tblformasdepago.nombre as formadepago,tblproveedores.nombre as cnombre " + _
        "from tbldevolucionescompras inner join tbldevolucionesdetallesc on tbldevolucionescompras.iddevolucion=tbldevolucionesdetallesc.iddevolucion inner join tblinventario on tbldevolucionesdetallesc.idinventario=tblinventario.idinventario inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tbldevolucionescompras.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idproveedor=" + pIdCliente.ToString
        End If
        'If pidInventario > 1 Then
        '    Comm.CommandText += " and tbldevolucionesdetallesc.idinventario=" + pidInventario.ToString
        'Else
        '    If pidClasificacion > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
        '    End If
        '    If pidClasificacion2 > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion.ToString
        '    End If
        '    If pidClasificacion3 > 0 Then
        '        Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion.ToString
        '    End If
        'End If
        Comm.CommandText += " order by tbldevolucionescompras.fecha,tbldevolucionescompras.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevolucionescompras")
        'DS.WriteXmlSchema("tbldevolucionescompras.xml")
        Return DS.Tables("tbldevolucionescompras").DefaultView
    End Function
    Public Function ReporteVentasArticulos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevolucionescompras.iddevolucion,tbldevolucionescompras.folio,tbldevolucionescompras.estado,tbldevolucionescompras.total,tbldevolucionescompras.totalapagar,tbldevolucionescompras.fecha,tbldevolucionescompras.tipodecambio,tbldevolucionescompras.idconversion,tbldevolucionesdetallesc.cantidad,tblinventario.nombre as descripcion,if(tbldevolucionesdetallesc.idmoneda=2,tbldevolucionesdetallesc.precio,tbldevolucionesdetallesc.precio*tbldevolucionescompras.tipodecambio) as precio,0 as costoinv,0 as costopro,tbldevolucionesdetallesc.idinventario,tbldevolucionesdetallesc.idvariante,tblformasdepago.tipo as formadepago,tblproveedores.nombre as cnombre,tbldevolucionesdetallesc.iva from tbldevolucionescompras inner join tbldevolucionesdetallesc on tbldevolucionescompras.iddevolucion=tbldevolucionesdetallesc.iddevolucion inner join tblinventario on tbldevolucionesdetallesc.idinventario=tblinventario.idinventario inner join tblproveedores on tbldevolucionescompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tbldevolucionescompras.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        'Comm.CommandText = "select tbldevolucionescompras.iddevolucion,tbldevolucionescompras.folio,tbldevolucionescompras.serie,tbldevolucionescompras.estado,tbldevolucionescompras.total,tbldevolucionescompras.totalapagar,tbldevolucionescompras.fecha,tbldevolucionescompras.tipodecambio,tbldevolucionescompras.idconversion,tbldevolucionesdetallesc.cantidad,tbldevolucionesdetallesc.descripcion,tbldevolucionesdetallesc.precio,if(tbldevolucionesdetallesc.idinventario>1,spsacacostoarticulo(tbldevolucionesdetallesc.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tbldevolucionesdetallesc.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tbldevolucionesdetallesc.idinventario,tbldevolucionesdetallesc.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tbldevolucionescompras inner join tbldevolucionesdetallesc on tbldevolucionescompras.iddevolucion=tbldevolucionesdetallesc.iddevolucion inner join tblinventario on tbldevolucionesdetallesc.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tbldevolucionesdetallesc.idvariante inner join tblclientes on tbldevolucionescompras.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tbldevolucionescompras.idforma where tbldevolucionescompras.estado=3 and tbldevolucionescompras.fecha>='" + pFecha1 + "' and tbldevolucionescompras.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tbldevolucionescompras.idproveedor=" + pIdCliente.ToString
        End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tbldevolucionesdetallesc.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            End If
            If pidClasificacion2 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion.ToString
            End If
            If pidClasificacion3 > 0 Then
                Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion.ToString
            End If
        End If
        Comm.CommandText += " order by tbldevolucionescompras.fecha,tbldevolucionescompras.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevolucionescompras")
        'DS.WriteXmlSchema("tbldevolucionescompras.xml")
        Return DS.Tables("tbldevolucionescompras").DefaultView
    End Function
    'Public Sub ReporteMensualCFD(ByVal pFecha As Date, ByVal pRutaArchivo As String)
    '    Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim Fecha1 As Date
    '    Dim Fecha2 As Date
    '    Dim Mes1 As Integer
    '    Dim Mes2 As Integer
    '    Fecha1 = DateSerial(Year(pFecha), Month(pFecha), 1)
    '    Fecha2 = DateSerial(Year(pFecha), Month(pFecha) + 1, 0)
    '    Dim S As String = ""
    '    Comm.CommandText = "select tbldevoluciones.iddevolucion,tblclientes.rfc,tbldevoluciones.serie,tbldevoluciones.folio,tbldevoluciones.noaprobacion,tbldevoluciones.yearaprobacion,tbldevoluciones.fecha,tbldevoluciones.hora,tbldevoluciones.totalapagar,tbldevoluciones.total,tbldevoluciones.estado,tbldevoluciones.fechacancelado from tbldevoluciones inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente where (fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "') or (fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "') order by serie,folio"
    '    DReader = Comm.ExecuteReader
    '    While DReader.Read
    '        Mes1 = Month(CDate(DReader("fecha")))
    '        Mes2 = Month(CDate(DReader("fechacancelado")))
    '        If Mes1 = Mes2 And DReader("estado") = Estados.Cancelada Then
    '            If S <> "" Then S += vbCrLf
    '            S += "|" + DReader("rfc") + "|"
    '            S += DReader("serie") + "|"
    '            S += DReader("folio").ToString + "|"
    '            S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
    '            S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
    '            S += Format(DReader("totalapagar"), "#0.00") + "|"
    '            S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
    '            S += "1|I||||" + vbCrLf
    '            S += "|" + DReader("rfc") + "|"
    '            S += DReader("serie") + "|"
    '            S += DReader("folio").ToString + "|"
    '            S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
    '            S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
    '            S += Format(DReader("totalapagar"), "#0.00") + "|"
    '            S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
    '            S += "0|I||||"
    '        Else
    '            If DReader("estado") = Estados.Cancelada Then
    '                If S <> "" Then S += vbCrLf
    '                S += "|" + DReader("rfc") + "|"
    '                S += DReader("serie") + "|"
    '                S += DReader("folio").ToString + "|"
    '                S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
    '                S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
    '                S += Format(DReader("totalapagar"), "#0.00") + "|"
    '                S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
    '                S += "0|I||||"
    '            End If
    '            If DReader("estado") = Estados.Guardada Then
    '                If S <> "" Then S += vbCrLf
    '                S += "|" + DReader("rfc") + "|"
    '                S += DReader("serie") + "|"
    '                S += DReader("folio").ToString + "|"
    '                S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
    '                S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
    '                S += Format(DReader("totalapagar"), "#0.00") + "|"
    '                S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
    '                S += "1|I||||"
    '            End If
    '        End If
    '    End While
    '    DReader.Close()
    '    Dim Enc As New System.Text.UTF8Encoding
    '    Dim Bytes() As Byte = Enc.GetBytes(S)
    '    Dim en As New Encriptador
    '    en.GuardaArchivo(pRutaArchivo, Bytes)
    'End Sub
    Public Sub Aplicar(ByVal pId As Integer, ByVal pCantidad As Double, ByVal pSuma As Boolean)
        If pSuma Then
            Comm.CommandText = "update tbldevolucionescompras set credito=credito+" + pCantidad.ToString + " where iddevolucion=" + pId.ToString
        Else
            Comm.CommandText = "update tbldevolucionescompras set credito=credito-" + pCantidad.ToString + " where iddevolucion=" + pId.ToString
        End If
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaDatosDocumento(ByVal pIdDevolucion As Integer) As String
        Comm.CommandText = "select if(idcompra<>0," + _
        "ifnull((select concat('COMPRA-',serie,convert(folioi using utf8)) from tblcompras where tblcompras.idcompra=tbldevolucionescompras.idcompra),'')," + _
        "ifnull((select concat('REMISIÓN-',serie,convert(folioi using utf8)) from tblcomprasremisiones where tblcomprasremisiones.idremision=tbldevolucionescompras.idremision),'')) from tbldevolucionescompras where iddevolucion=" + pIdDevolucion.ToString
        Return Comm.ExecuteScalar
    End Function
End Class
