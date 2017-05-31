Public Class dbCompras
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdProveedor As Integer
    Public Fecha As String
    Public Proveedor As dbproveedores
    Public IdPedido As Integer
    Public Referencia As String
    Public Desglosar As Byte
    Public Estado As Byte
    Public idSucursal As Integer
    Public TipodeCambio As Double
    Public Hora As String
    Public Total As Double
    Public TotalaPagar As Double
    Public Credito As Double
    Public Idforma As Integer
    Public IdMoneda As Integer
    Public Serie As String
    Public Folioi As Integer
    Public TotalISR As Double
    Public Subtotal As Double
    Public TotalIva As Double
    Public TotalIvaRetenido As Double
    Public TotalVenta As Double
    Public FechaCancelado As String
    Public HoraCancelado As String
    Public CostoIndirecto As Double
    Public Comentario As String
    Public IEPS As Double
    Public ivaRetenido As Double
    Public TotalIeps As Double
    Public TotalIvaRetenidoCon As Double
    Public ImpLocales As New Collection 'agregué
    Dim ILocal As Implocal
    Public FolioCFDI As String
    Private Structure Implocal
        Dim Tasa As Double
        Dim Nombre As String
        Dim Tipo As Byte
        Dim Importe As Double
    End Structure
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdProveedor = -1
        Fecha = ""
        Referencia = ""
        Desglosar = 0
        Estado = 0
        idSucursal = 0
        TipodeCambio = 0
        Total = 0
        TotalaPagar = 0
        Credito = 0
        Idforma = 0
        IdMoneda = 0
        FechaCancelado = ""
        HoraCancelado = ""
        Hora = ""
        CostoIndirecto = 0
        Serie = ""
        Folioi = 0
        Comentario = ""
        IEPS = 0
        ivaRetenido = 0
        FolioCFDI = ""
        Comm.Connection = Conexion
        Comm.CommandTimeout = 10000
        Proveedor = New dbproveedores(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        Comm.CommandTimeout = 10000
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblcompras where idcompra=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdProveedor = DReader("idproveedor")
            Fecha = DReader("fecha")
            Referencia = DReader("referencia")
            Desglosar = DReader("desglosar")
            Estado = DReader("estado")
            idSucursal = DReader("idsucursal")
            TipodeCambio = DReader("tipodecambio")
            Total = DReader("total")
            TotalaPagar = DReader("totalapagar")
            Credito = DReader("credito")
            Idforma = DReader("idforma")
            IdMoneda = DReader("idmoneda")
            FechaCancelado = DReader("fechacancelado")
            HoraCancelado = DReader("horacancelado")
            Hora = DReader("hora")
            CostoIndirecto = DReader("costoindirecto")
            Folioi = DReader("folioi")
            Serie = DReader("serie")
            'IEPS = DReader("IEPS")
            'ivaRetenido = DReader("ivaRetenido")
            Comentario = DReader("comentario")
            FolioCFDI = DReader("foliocfdi")
            IdPedido = DReader("idpedido")
        End If
        DReader.Close()
        Proveedor = New dbproveedores(IdProveedor, Comm.Connection)

    End Sub
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folioi) from tblcompras where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado>=2)),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetidoi(ByVal pSerie As String, ByVal pFolio As Integer, ByVal pIdSucursal As Integer) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folioi) from tblcompras where folioi=" + pFolio.ToString + " and idsucursal=" + pIdSucursal.ToString + " and serie='" + Replace(pSerie, "'", "''") + "' and estado>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetidoi = False
        Else
            ChecaFolioRepetidoi = True
        End If
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As String, ByVal pIdProveedor As Integer) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(referencia) from tblcompras where referencia='" + Replace(pFolio, "'", "''") + "' and idproveedor=" + pIdProveedor.ToString + " and estado>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    Public Sub Guardar(ByVal pIdProveedor As Integer, ByVal pFecha As String, ByVal pReferencia As String, ByVal pDesglosar As Byte, ByVal pIdSucursal As Integer, ByVal pTipodeCambio As Double, ByVal pIdMoneda As Integer, ByVal pidFormadePago As Integer, ByVal pFolioi As Integer, ByVal pSerie As String, ByVal pFoliocfdi As String, pIdPedido As String)
        IdProveedor = pIdProveedor
        Fecha = pFecha
        Referencia = pReferencia
        Desglosar = pDesglosar
        idSucursal = pIdSucursal
        TipodeCambio = pTipodeCambio
        IdMoneda = pIdMoneda
        Idforma = pidFormadePago
        Serie = pSerie
        Folioi = pFolioi
        FolioCFDI = pFoliocfdi
        idPedido = pIdPedido
        Comm.CommandText = "insert into tblcompras(idproveedor,fecha,referencia,desglosar,estado,idsucursal,tipodecambio,total,totalapagar,credito,idforma,idmoneda,hora,fechacancelado,horacancelado,costoindirecto,serie,folioi,comentario,deremision,foliocfdi,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio,idpedido) values(" + IdProveedor.ToString + ",'" + Fecha + "','" + Replace(Referencia, "'", "''") + "'," + Desglosar.ToString + ",1," + idSucursal.ToString + "," + TipodeCambio.ToString + ",0,0,0," + Idforma.ToString + "," + IdMoneda.ToString + ",'" + Format(TimeOfDay, "HH:mm:ss") + "','" + Fecha + "','" + Format(TimeOfDay, "HH:mm:ss") + "',0,'" + Replace(Trim(Serie), "'", "''") + "'," + Folioi.ToString + ",'',0,'" + Replace(FolioCFDI.Trim, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + pIdPedido.ToString + ")"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idcompra) from tblcompras"
        ID = Comm.ExecuteScalar
        
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pReferencia As String, ByVal pDesglosar As Byte, ByVal pEstado As Byte, ByVal pTipodeCambio As Double, ByVal pTotal As Double, ByVal pTotalaPagar As Double, ByVal pTipodePAgo As Byte, ByVal pIdMoneda As Integer, ByVal pCostoIndirecto As Double, ByVal pSerie As String, ByVal pFolioi As Integer, ByVal pComentario As String, ByVal pFolioCFDI As String)
        ID = pID
        Fecha = pFecha
        Referencia = pReferencia
        Desglosar = pDesglosar
        Estado = pEstado
        TipodeCambio = pTipodeCambio
        Idforma = pTipodePAgo
        IdMoneda = pIdMoneda
        Total = pTotal
        TotalaPagar = pTotalaPagar
        CostoIndirecto = pCostoIndirecto
        Serie = pSerie
        Folioi = pFolioi
        FolioCFDI = pFolioCFDI
        Comentario = pComentario
        Comm.CommandText = "update tblcompras set fecha='" + Fecha + "',referencia='" + Replace(Referencia, "'", "''") + "',desglosar=" + Desglosar.ToString + ",estado=" + Estado.ToString + ",tipodecambio=" + TipodeCambio.ToString + ",total=" + Total.ToString + ",totalapagar=" + TotalaPagar.ToString + ",idmoneda=" + IdMoneda.ToString + ",idforma=" + Idforma.ToString + ",fechacancelado='" + Fecha + "',hora='" + Format(TimeOfDay, "HH:mm:ss") + "',costoindirecto=" + CostoIndirecto.ToString + ",serie='" + Replace(Trim(Serie), "'", "''") + "',folioi=" + Folioi.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "',foliocfdi='" + Replace(FolioCFDI.Trim, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idcompra=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer, ByVal pTipoCosteo As Byte, ByVal ptipodeCambio As Double, ByVal pEstado As Byte, ByVal pCosteoTiempoReal As Byte)
        If pEstado <> Estados.Cancelada And VienedeRemision(pID) = 0 Then
            RegresaInventario(pID, pTipoCosteo, ptipodeCambio, pCosteoTiempoReal)
        End If
        DesligaRemisiones(pID)
        Comm.CommandText = "delete from tblcompras where idcompra=" + pID.ToString
        Comm.ExecuteNonQuery()

    End Sub
    Public Sub RegresaInventario(ByVal pId As Integer, ByVal pTipoCosteo As Byte, ByVal pTipodeCambio As Double, ByVal pCosteoTiempoReal As Byte)

        Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,surtido,0,1,1) from tblcomprasdetalles where idcompra=" + pId.ToString + ";"
        Comm.CommandText += "select spmodificainventariolotesf(tblcomprasdetalles.idinventario,tblcomprasdetalles.idalmacen,tblcompraslotes.surtido,0,1,1,tblcompraslotes.idlote) from tblcompraslotes inner join tblcomprasdetalles on tblcompraslotes.iddetalle=tblcomprasdetalles.iddetalle where tblcomprasdetalles.idcompra=" + pId.ToString + ";"
        Comm.CommandText += "select spmodificainventarioaduanaf(tblcomprasdetalles.idinventario,tblcomprasdetalles.idalmacen,tblcomprasaduana.surtido,0,1,1,tblcomprasaduana.idaduana) from tblcomprasaduana inner join tblcomprasdetalles on tblcomprasaduana.iddetalle=tblcomprasdetalles.iddetalle where tblcomprasdetalles.idcompra=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()

        Comm.CommandText = "select ifnull((select idpedido from tblcompras where idcompra=" + pId.ToString + "),0)"
        Dim idpedido As Integer = Comm.ExecuteScalar
        If idpedido <> 0 Then
            Comm.CommandText = "update tblcompraspedidosdetalles as pd set pd.surtido=pd.surtido-ifnull((select cd.surtido from tblcompras as c inner join tblcomprasdetalles as cd on c.idcompra=cd.idcompra where c.idcompra=" + pId.ToString + " and cd.idinventario=pd.idinventario),0) where pd.idpedido=" + idpedido.ToString
            Comm.ExecuteNonQuery()
        End If

        Comm.CommandText = "update tblcomprasdetalles set surtido=0 where idcompra=" + pId.ToString + ";"
        Comm.CommandText += "update tblcompraslotes inner join tblcomprasdetalles on tblcompraslotes.iddetalle=tblcomprasdetalles.iddetalle set tblcompraslotes.surtido=0 where idcompra=" + pId.ToString + ";"
        Comm.CommandText += "update tblcomprasaduana inner join tblcomprasdetalles on tblcomprasaduana.iddetalle=tblcomprasdetalles.iddetalle set tblcomprasaduana.surtido=0 where idcompra=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()

        'ubicaciones
        Comm.CommandText = "select spmodificainventarioubicacionesf(d.idinventario, d.idalmacen, u.surtido, 0, 1, 1, u.ubicacion) from tblcomprasdetalles d inner join tblcomprasubicaciones u on d.iddetalle=u.iddetalle where d.idcompra=" + pId.ToString + ";"
        Comm.CommandText += "update tblcomprasubicaciones inner join tblcomprasdetalles on tblcomprasubicaciones.iddetalle = tblcomprasdetalles.iddetalle set tblcomprasubicaciones.surtido = tblcomprasubicaciones.cantidad where tblcomprasdetalles.idcompra=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub

    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, Optional ByVal pNombreClave As String = "", Optional ByVal pReferencia As String = "", Optional ByVal pEstado As Byte = 0, Optional ByVal pIdSucursal As Integer = 0) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblcompras.idcompra,tblcompras.fecha,concat(tblcompras.serie,convert(tblcompras.folioi using utf8),' - ',tblcompras.referencia) as folioc,tblproveedores.clave,tblproveedores.nombre as Proveedor,tblcompras.totalapagar as Importe,case tblcompras.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as Estado " + _
        " from tblcompras inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblproveedores.clave,tblproveedores.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pReferencia <> "" Then
            Comm.CommandText += " and concat(tblcompras.serie,convert(tblcompras.folioi using utf8),tblcompras.referencia) like '%" + Replace(pReferencia, "'", "''") + "%'"
        End If
        If pEstado <> 0 Then
            Comm.CommandText += " and tblcompras.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblcompras.estado>=2"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        Comm.CommandText += " order by tblcompras.fecha desc,tblcompras.serie,tblcompras.folioi desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcompras")
        Return DS.Tables("tblcompras").DefaultView
    End Function
    Public Function ConsultaComprasconRetiro(PidPagoProv As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblcompras.idcompra,tblcompras.fecha,concat(tblcompras.serie,convert(tblcompras.folioi using utf8),' - ',tblcompras.referencia),tblproveedores.clave,tblproveedores.nombre as Proveedor,tblcompras.totalapagar as Importe,case tblcompras.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as Estado " + _
        " from tblcompras inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblcomprasretiros on tblcomprasretiros.idcompra=tblcompras.idcompra where tblcomprasretiros.idpagoprov=" + PidPagoProv.ToString
        Comm.CommandText += " order by tblcompras.fecha"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcompras")
        Return DS.Tables("tblcompras").DefaultView
    End Function
    Public Sub ModificaReferencia(ByVal pIdCompra As Integer, ByVal pReferencia As String)
        Comm.CommandText = "update tblcompras set referencia='" + Replace(Trim(pReferencia), "'", "''") + "' where idcompra=" + pIdCompra.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaTotal(ByVal pidCompra As Integer, ByVal pidMoneda As Integer) As Double
        ''Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        ''Dim IDs As New Collection
        ''Dim Precio As Double
        ''Dim IdMonedaC As Integer
        ''Dim Total As Double = 0
        ' ''Dim Encontro As Double
        ''Dim iIva As Double
        ''Dim Cont As Integer = 1
        ''Dim iTipoCambio As Double = 1
        ''Dim iIsr As Double = 0
        ''Dim iIvaRetenido As Double = 0
        ''Subtotal = 0
        ''TotalIva = 0
        ''TotalVenta = 0
        ''Comm.CommandText = "select tipodecambio from tblcompras where idcompra=" + pidCompra.ToString
        ''iTipoCambio = Comm.ExecuteScalar
        ' ''Comm.CommandText = "select isr from tblventas where idventa=" + pidVenta.ToString
        ' ''iIsr = Comm.ExecuteScalar
        ' ''Comm.CommandText = "select ivaretenido from tblventas where idventa=" + pidVenta.ToString
        ' ''iIvaRetenido = Comm.ExecuteScalar
        ' ''+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        ''Comm.CommandText = "select iddetalle from tblcomprasdetalles where idcompra=" + pidCompra.ToString
        ''DReader = Comm.ExecuteReader
        ''While DReader.Read
        ''    IDs.Add(DReader("iddetalle"))
        ''End While
        ''DReader.Close()
        ''While Cont <= IDs.Count
        ''    Comm.CommandText = "select precio from tblcomprasdetalles where iddetalle=" + IDs.Item(Cont).ToString
        ''    Precio = Comm.ExecuteScalar
        ''    Comm.CommandText = "select iva from tblcomprasdetalles where iddetalle=" + IDs.Item(Cont).ToString
        ''    iIva = Comm.ExecuteScalar
        ''    Comm.CommandText = "select idmoneda from tblcomprasdetalles where iddetalle=" + IDs.Item(Cont).ToString
        ''    IdMonedaC = Comm.ExecuteScalar
        ''    If pidMoneda = 2 Then
        ''        If pidMoneda <> IdMonedaC Then
        ''            Precio = Precio * iTipoCambio
        ''        End If
        ''    Else
        ''        If IdMonedaC = 2 Then
        ''            Precio = Precio / iTipoCambio
        ''        End If
        ''    End If
        ''    Subtotal += Precio
        ''    TotalIva += (Precio * (iIva / 100))
        ''    Cont += 1
        ''End While

        ''TotalISR = Subtotal * (iIsr / 100)
        ''TotalIvaRetenido = Subtotal * (iIvaRetenido / 100)
        ''TotalVenta = Subtotal + TotalIva
        'Comm.CommandText = "select spdatotalcompra(" + pidCompra.ToString + ",0," + pidMoneda.ToString + ")"
        'Subtotal = Comm.ExecuteScalar
        'Comm.CommandText = "select spdatotalcompra(" + pidCompra.ToString + ",1," + pidMoneda.ToString + ")"
        'TotalVenta = Comm.ExecuteScalar
        'TotalIva = TotalVenta - Subtotal
        'Return TotalVenta
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0
        'Dim Encontro As Double
        Dim iIva As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double
        Dim iIsr As Double
        Dim iIvaRetenido As Double
        Dim iCargo As Double
        Dim iDescuento As Double
        Dim pIEPS As Double
        Dim pIVARetenido As Double
        ' Dim Subtototal As Double
        Dim TotalDescuento As Double
        Dim TotalRetLocal As Double = 0
        Dim TotalTrasLocal As Double = 0
        Dim TotalSinRetencion As Double
        Dim TotalPeso As Double
        TotalIeps = 0
        TotalIvaRetenidoCon = 0
        'Dim iIdCliente As Integer
        'Dim iTruncar As Byte

        TotalIva = 0
        TotalVenta = 0
        'Comm.CommandText = "select desglosar from tblventas where idventa=" + pidVenta.ToString
        'iTruncar = Comm.ExecuteScalar
        Comm.CommandText = "select tipodecambio from tblcompras where idcompra=" + pidCompra.ToString
        iTipoCambio = Comm.ExecuteScalar
        'Comm.CommandText = "select idcliente from tblventas where idventa=" + pidVenta.ToString
        'iIdCliente = Comm.ExecuteScalar
        'Comm.CommandText = "select isr from  tblcompras where idcompra=" + pidVenta.ToString
        'iIsr = Comm.ExecuteScalar
        'Comm.CommandText = "select ivaretenido from  tblcompras where idcompra=" + pidVenta.ToString
        'iIvaRetenido = Comm.ExecuteScalar
        'Comm.CommandText = "select descuentog from  tblcompras where idcompra=" + pidVenta.ToString
        'iDescuento = Comm.ExecuteScalar
        'Comm.CommandText = "select cargog from  tblcompras where idcompra=" + pidVenta.ToString
        'iCargo = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tblcomprasdetalles where idcompra=" + pidCompra.ToString
        DReader = Comm.ExecuteReader
        TotalDescuento = 0
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        Dim Desc As Double
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblcomprasdetalles where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select iva from tblcomprasdetalles where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblcomprasdetalles where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            Comm.CommandText = "select descuento from tblcomprasdetalles where iddetalle=" + IDs.Item(Cont).ToString
            Desc = Comm.ExecuteScalar
            'IEPS e IVA Retenido
            Comm.CommandText = "select ieps from tblcomprasdetalles where iddetalle=" + IDs.Item(Cont).ToString
            pIEPS = Comm.ExecuteScalar
            Comm.CommandText = "select IVARetenido from tblcomprasdetalles where iddetalle=" + IDs.Item(Cont).ToString
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
            TotalIvaRetenidoCon += (Precio * (pIVARetenido / 100))


            TotalIva += (Precio * (iIva / 100))
            TotalDescuento += (Precio / (1 - Desc / 100)) - Precio
            Subtotal += Precio
            Cont += 1
        End While
        'If iTruncar = 0 Then
        'Dim Cimp As New dbClientesImpuestos(Comm.Connection)
        'ImpLocales.Clear()
        'DReader = Cimp.ConsultaReaderI(pidCompra)
        'TotalRetLocal = 0
        'TotalTrasLocal = 0
        'While DReader.Read
        '    ILocal.Nombre = DReader("nombre")
        '    ILocal.Tasa = DReader("tasa")
        '    ILocal.Tipo = DReader("tipo")
        '    ILocal.Importe = (Subtototal + TotalDescuento) * (DReader("tasa") / 100)
        '    If ILocal.Tipo = 0 Then
        '        TotalTrasLocal += ILocal.Importe
        '    Else
        '        TotalRetLocal += ILocal.Importe
        '    End If
        '    ImpLocales.Add(ILocal)
        'End While
        'DReader.Close()

        'Subtototal = Subtototal
        TotalISR = Subtotal * (iIsr / 100)
        TotalIvaRetenido = Subtotal * (iIvaRetenido / 100)
        TotalVenta = Subtotal + TotalIva - TotalISR - TotalIvaRetenido - iDescuento + iCargo - TotalRetLocal + TotalTrasLocal + TotalIeps - TotalIvaRetenidoCon
        TotalSinRetencion = Subtotal + TotalIva + TotalIeps - iDescuento + iCargo
        Comm.CommandText = "select ifnull((select sum(tblinventario.peso*tblcomprasdetalles.cantidad) from tblcomprasdetalles inner join tblinventario on tblcomprasdetalles.idinventario=tblinventario.idinventario where tblcomprasdetalles.idcompra=" + pidCompra.ToString + "),0)"
        TotalPeso = Comm.ExecuteScalar
        Return TotalVenta
    End Function

    Public Function ConsultaDeudas(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidCliente As Integer, ByVal pFolio As String, ByVal pidTipodePago As Integer, ByVal PorFechas As Boolean, ByVal Todas As Boolean, ByVal pTipodeOrden As Byte, ByVal pMostrarCanceladas As Boolean, ByVal pTipodeCambio As Double, ByVal pEnPesos As Boolean, ByVal pidSucursal As Integer) As DataView
        Dim DS As New DataSet

        Comm.CommandText = "delete from tblproveedoresdeudas where idproveedor=" + pidCliente.ToString
        Comm.ExecuteNonQuery()
        '"if(tblcompras.idmoneda=2,tblcompras.credito,tblcompras.credito*" + pTipodeCambio.ToString + ")," +
        '"ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblcompras.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblcompras.idcompra),0)," +
        '"ifnull((select sum(if(tblcompraspagos.idmoneda<>2,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblcompras.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblcompras.idcompra),0)," +
        'Facturas en pesos
        If pEnPesos Then
            Comm.CommandText = "insert into tblproveedoresdeudas(idproveedor,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        ",tblcompras.idcompra,0,tblcompras.fecha,tblcompras.estado,concat(tblcompras.serie,convert(tblcompras.folioi using utf8),'-',tblcompras.referencia),0," +
        "if(tblcompras.idmoneda=2,tblcompras.credito,tblcompras.credito*" + pTipodeCambio.ToString + ")," +
        "if(tblcompras.idmoneda=2,tblcompras.totalapagar,tblcompras.totalapagar*" + pTipodeCambio.ToString + "),0 from tblcompras inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma where tblcompras.idproveedor=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        Else
            Comm.CommandText = "insert into tblproveedoresdeudas(idproveedor,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        ",tblcompras.idcompra,0,tblcompras.fecha,tblcompras.estado,concat(tblcompras.serie,convert(tblcompras.folioi using utf8),'-',tblcompras.referencia),0," +
        "tblcompras.credito," +
        "tblcompras.totalapagar,0 from tblcompras inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma where tblcompras.idproveedor=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        End If

        'select tblventas.idventa,0 as sel,tblventas.fecha,if(tblventas.estado=3,'A','C') as estadof,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,tblventas.totalapagar-tblventas.credito as restante from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        If Todas = False Then
            'If pEnPesos Then
            'Comm.CommandText += " and round((tblcompras.totalapagar*" + pTipodeCambio.ToString + ")-ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblcompras.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblcompras.idcompra),0),2)>0"
            'Else
            '   Comm.CommandText += " and round((tblcompras.totalapagar)-ifnull((select sum(if(tblcompraspagos.idmoneda<>2,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblcompras.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblcompras.idcompra),0),2)>0"
            'End If
            Comm.CommandText += " and round(tblcompras.totalapagar-tblcompras.credito,2)>0"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblcompras.referencia,tblcompras.serie,convert(folioi using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pMostrarCanceladas Then
            Comm.CommandText += " and (tblcompras.estado=4 or tblcompras.estado=3)"
        Else
            Comm.CommandText += " and tblcompras.estado=3"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pidSucursal.ToString
        End If
        Comm.ExecuteNonQuery()

        ''Facturas en no pesos
        ''Facturas en pesos
        'If pEnPesos Then
        '    Comm.CommandText = "insert into tblproveedoresdeudas(idproveedor,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",tblcompras.idcompra,0,tblcompras.fecha,tblcompras.estado,concat(tblcompras.serie,convert(tblcompras.folioi using utf8),'-',tblcompras.referencia),0," +
        '"ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblcompras.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblcompras.idcompra),0)," +
        '"if(tblcompras.idmoneda=2,tblcompras.totalapagar,tblcompras.totalapagar*" + pTipodeCambio.ToString + "),0 from tblcompras inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma where tblcompras.idproveedor=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString + " and tblcompras.idmoneda<>2"
        'Else
        '    Comm.CommandText = "insert into tblproveedoresdeudas(idproveedor,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        '",tblcompras.idcompra,0,tblcompras.fecha,tblcompras.estado,concat(tblcompras.serie,convert(tblcompras.folioi using utf8),'-',tblcompras.referencia),0," +
        '"ifnull((select sum(if(tblcompraspagos.idmoneda<>2,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblcompras.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblcompras.idcompra),0)," +
        '"tblcompras.totalapagar,0 from tblcompras inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma where tblcompras.idproveedor=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString + " and tblcompras.idmoneda<>2"
        'End If

        ''select tblventas.idventa,0 as sel,tblventas.fecha,if(tblventas.estado=3,'A','C') as estadof,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,tblventas.totalapagar-tblventas.credito as restante from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        'If Todas = False Then
        '    If pEnPesos Then
        '        Comm.CommandText += " round((ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblcompras.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblcompras.idcompra),0)),2)>0"
        '    Else

        '    End If
        '    'Comm.CommandText += " and round(tblcompras.totalapagar-tblcompras.credito,2)>0"
        'End If
        '    If PorFechas Then
        '        Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        '    End If
        '    If pFolio <> "" Then
        '        Comm.CommandText += " and tblcompras.referencia like '%" + Replace(pFolio, "'", "''") + "%'"
        '    End If
        '    If pMostrarCanceladas Then
        '        Comm.CommandText += " and (tblcompras.estado=4 or tblcompras.estado=3)"
        '    Else
        '        Comm.CommandText += " and tblcompras.estado=3"
        '    End If
        '    If pidSucursal > 0 Then
        '        Comm.CommandText += " and tblcompras.idsucursal=" + pidSucursal.ToString
        '    End If
        '    Comm.ExecuteNonQuery()


        'Notas de Cargo
        If pEnPesos Then
            Comm.CommandText = "insert into tblproveedoresdeudas(idproveedor,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        ",tblnotasdecargocompras.idcargo,1,tblnotasdecargocompras.fecha,tblnotasdecargocompras.estado,tblnotasdecargocompras.folio,0,if(tblnotasdecargocompras.idmoneda=2,tblnotasdecargocompras.aplicado,tblnotasdecargocompras.aplicado*" + pTipodeCambio.ToString + "),if(tblnotasdecargocompras.idmoneda=2,tblnotasdecargocompras.totalapagar,tblnotasdecargocompras.totalapagar*" + pTipodeCambio.ToString + "),0 from tblnotasdecargocompras inner join tblproveedores on tblnotasdecargocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecargocompras.idproveedor=" + pidCliente.ToString
        Else
            Comm.CommandText = "insert into tblproveedoresdeudas(idproveedor,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        ",tblnotasdecargocompras.idcargo,1,tblnotasdecargocompras.fecha,tblnotasdecargocompras.estado,tblnotasdecargocompras.folio,0,tblnotasdecargocompras.aplicado,tblnotasdecargocompras.totalapagar,0 from tblnotasdecargocompras inner join tblproveedores on tblnotasdecargocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecargocompras.idproveedor=" + pidCliente.ToString
        End If


        If Todas = False Then
            Comm.CommandText += " and round(tblnotasdecargocompras.totalapagar-tblnotasdecargocompras.aplicado,2)>0"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and tblnotasdecargocompras.folio like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pMostrarCanceladas Then
            Comm.CommandText += " and (tblnotasdecargocompras.estado=4 or tblnotasdecargocompras.estado=3)"
        Else
            Comm.CommandText += " and tblnotasdecargocompras.estado=3"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pidSucursal.ToString
        End If
        Comm.ExecuteNonQuery()


        'Documentos saldo inicial
        If pEnPesos Then
            Comm.CommandText = "insert into tblproveedoresdeudas(idproveedor,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
            ",dc.iddocumento,2,dc.fecha,dc.estado,concat(dc.serie,convert(dc.folio using utf8)),dc.folio,if(dc.idmoneda=2,dc.credito,dc.credito*" + pTipodeCambio.ToString + "),if(dc.idmoneda=2,dc.totalapagar,dc.totalapagar*" + pTipodeCambio.ToString + "),0 from tbldocumentosproveedores as dc where dc.idproveedor=" + pidCliente.ToString + " and dc.tiposaldo=0"
        Else
            Comm.CommandText = "insert into tblproveedoresdeudas(idproveedor,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
            ",dc.iddocumento,2,dc.fecha,dc.estado,concat(dc.serie,convert(dc.folio using utf8)),dc.folio,dc.credito,dc.totalapagar,0 from tbldocumentosproveedores as dc where dc.idproveedor=" + pidCliente.ToString + " and dc.tiposaldo=0"
        End If


        If Todas = False Then
            Comm.CommandText += " and round(dc.totalapagar-dc.credito,2)>0"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(dc.serie,convert(dc.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pMostrarCanceladas Then
            Comm.CommandText += " and (dc.estado=4 or dc.estado=3)"
        Else
            Comm.CommandText += " and dc.estado=3"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and dc.idsucursal=" + pidSucursal.ToString
        End If
        Comm.ExecuteNonQuery()

        'Documentos documentos
        If pEnPesos Then
            Comm.CommandText = "insert into tblproveedoresdeudas(idproveedor,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
        ",dc.iddocumento,3,dc.fecha,dc.estado,concat(dc.seriereferencia,convert(dc.folioreferencia using utf8)),dc.folioreferencia,if(dc.idmoneda=2,dc.credito,dc.credito*" + pTipodeCambio.ToString + "),if(dc.idmoneda=2,dc.totalapagar,dc.totalapagar*" + pTipodeCambio.ToString + "),0 from tbldocumentosproveedores as dc where dc.idproveedor=" + pidCliente.ToString + " and dc.tiposaldo=1"
        Else
            Comm.CommandText = "insert into tblproveedoresdeudas(idproveedor,iddocumento,tipo,fecha,estado,serie,folio,credito,totalapagar,cant) select " + pidCliente.ToString + _
            ",dc.iddocumento,3,dc.fecha,dc.estado,concat(dc.seriereferencia,convert(dc.folioreferencia using utf8)),dc.folioreferencia,dc.credito,dc.totalapagar,0 from tbldocumentosproveedores as dc where dc.idproveedor=" + pidCliente.ToString + " and dc.tiposaldo=1"
        End If


        If Todas = False Then
            Comm.CommandText += " and round(dc.totalapagar-dc.credito,2)>0"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(dc.seriereferencia,convert(dc.folioreferencia using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pMostrarCanceladas Then
            Comm.CommandText += " and (dc.estado=4 or dc.estado=3)"
        Else
            Comm.CommandText += " and dc.estado=3"
        End If
        If pidSucursal > 0 Then
            Comm.CommandText += " and dc.idsucursal=" + pidSucursal.ToString
        End If
        Comm.ExecuteNonQuery()


        Comm.CommandText = "select iddocumento as idventa,0 as sel,cant,fecha,case tipo when 0 then 'Factura' when 1 then 'Nota de Cargo' when 2 then 'S. Inicial' when 3 then 'Documento' end as tipodoc,if(estado=3,'A','C') as estadof,serie,folio,credito,totalapagar,totalapagar-credito as restante,tipo,totalapagar-credito as restante2 from tblproveedoresdeudas where idproveedor=" + pidCliente.ToString
        'select tblventas.idventa,0 as sel,tblventas.fecha,if(tblventas.estado=3,'A','C') as estadof,tblventas.serie,tblventas.folio,tblventas.credito,tblventas.totalapagar,tblventas.totalapagar-tblventas.credito as restante from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        If pTipodeOrden = 0 Then
            Comm.CommandText += " order by tipo,fecha,serie"
        Else
            Comm.CommandText += " order by tipo,totalapagar,serie"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblproveedoresdeudas")
        Return DS.Tables("tblproveedoresdeudas").DefaultView
    End Function

    Public Function DaIvas(ByVal pidcompra As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iva,precio,idmoneda from tblcomprasdetalles where idcompra=" + pidcompra.ToString
        Return Comm.ExecuteReader
    End Function
    Public Sub AgregarDetallesReferencia(ByVal PidCompra As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte, ByVal pidAlmacen As Integer)
        Select Case Tipo
            Case 0 'cotizacion
                Comm.CommandText = "insert into tblcomprasdetalles(idcompra,idinventario,cantidad,precio,idmoneda,iva,extra,descuento,idalmacen,surtido,costoindirecto,IEPS,ivaRetenido) select " + PidCompra.ToString + ",idinventario,cantidad,precio,idmoneda,iva,extra,descuento," + pidAlmacen.ToString + ",0,0,IEPS,ivaRetenido from tblcomprascotizacionesdetalles where idcotizacion=" + pIdDocumento.ToString
                Comm.ExecuteNonQuery()
            Case 1 'pedido
                Comm.CommandText = "insert into tblcomprasdetalles(idcompra, idinventario, cantidad, precio, idmoneda, iva, extra, descuento, idalmacen, surtido, costoindirecto, IEPS, ivaRetenido) select " + PidCompra.ToString + ", idinventario, cantidad-surtido, precio, idmoneda, iva, extra, descuento, " + pidAlmacen.ToString + ", 0, 0, IEPS, ivaRetenido from tblcompraspedidosdetalles where cantidad>surtido and idpedido=" + pIdDocumento.ToString
                Comm.ExecuteNonQuery()
            Case 2 'remision
                Comm.CommandText = "insert into tblcomprasdetalles(idcompra,idinventario,cantidad,precio,idmoneda,iva,extra,descuento,idalmacen,surtido,costoindirecto,IEPS,ivaRetenido) select " + PidCompra.ToString + ",idinventario,cantidad,precio,idmoneda,iva,extra,descuento,idalmacen,surtido,0,IEPS,ivaRetenido from tblcomprasremisionesdetalles where idremision=" + pIdDocumento.ToString
                Comm.ExecuteNonQuery()
                Comm.CommandText = "update tblinventarioseries set idcompra=" + PidCompra.ToString + ",idremisionc=0 where idremisionc=" + pIdDocumento.ToString
                Comm.ExecuteNonQuery()
            Case 3 'venta
                Comm.CommandText = "insert into tblcomprasdetalles(idcompra,idinventario,cantidad,precio,idmoneda,iva,extra,descuento,idalmacen,surtido,costoindirecto,IEPS,ivaRetenido) select " + PidCompra.ToString + ",idinventario,cantidad,precio,idmoneda,iva,extra,descuento,idalmacen,0,0,IEPS,ivaRetenido from tblcomprasdetalles where idcompra=" + pIdDocumento.ToString
                Comm.ExecuteNonQuery()
        End Select
    End Sub
    Public Sub ModificaInventario(ByVal pId As Integer, ByVal pTipoCosteo As Byte, ByVal pTipodeCambio As Double)
        Comm.CommandTimeout = 10000

        Comm.CommandText = "select idpedido from tblcompras where idcompra=" + pId.ToString
        Dim idpedido As Integer = Comm.ExecuteScalar
        If idpedido <> Nothing And idpedido <> 0 Then
            Comm.CommandText = "update tblcompraspedidosdetalles as pd set pd.surtido=pd.surtido+ifnull((select cd.cantidad-cd.surtido from tblcompras as c inner join tblcomprasdetalles as cd on c.idcompra=cd.idcompra where c.idcompra=" + pId.ToString + " and cd.idinventario=pd.idinventario),0) where pd.idpedido=" + idpedido.ToString
            Comm.ExecuteNonQuery()
        End If
        Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,cantidad-surtido,0,0,1) from tblcomprasdetalles where idcompra=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblcomprasdetalles set surtido=cantidad where idcompra=" + pId.ToString + "; "
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select spmodificainventariolotesf(tblcomprasdetalles.idinventario,tblcomprasdetalles.idalmacen,tblcompraslotes.cantidad-tblcompraslotes.surtido,0,0,1,tblcompraslotes.idlote) from tblcompraslotes inner join tblcomprasdetalles on tblcompraslotes.iddetalle = tblcomprasdetalles.iddetalle where tblcomprasdetalles.idcompra=" + pId.ToString + "; "
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select spmodificainventarioaduanaf(tblcomprasdetalles.idinventario,tblcomprasdetalles.idalmacen,tblcomprasaduana.cantidad-tblcomprasaduana.surtido,0,0,1,tblcomprasaduana.idaduana) from tblcomprasaduana inner join tblcomprasdetalles on tblcomprasaduana.iddetalle = tblcomprasdetalles.iddetalle where tblcomprasdetalles.idcompra=" + pId.ToString + "; "
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblcompraslotes inner join tblcomprasdetalles on tblcompraslotes.iddetalle=tblcomprasdetalles.iddetalle set tblcompraslotes.surtido=tblcompraslotes.cantidad where idcompra=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
        Comm.CommandText += "update tblcomprasaduana inner join tblcomprasdetalles on tblcomprasaduana.iddetalle=tblcomprasdetalles.iddetalle set tblcomprasaduana.surtido=tblcomprasaduana.cantidad where idcompra=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()

        'ubicaciones
        Comm.CommandText = "select spmodificainventarioubicacionesf(d.idinventario, d.idalmacen, u.cantidad-u.surtido, 0, 0, 1, u.ubicacion) from tblcomprasdetalles d inner join tblcomprasubicaciones u on d.iddetalle=u.iddetalle where d.idcompra=" + pId.ToString + ";"
        Comm.CommandText += "update tblcomprasubicaciones inner join tblcomprasdetalles on tblcomprasubicaciones.iddetalle = tblcomprasdetalles.iddetalle set tblcomprasubicaciones.surtido = tblcomprasubicaciones.cantidad where tblcomprasdetalles.idcompra=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()

        'verifica si la compra se creó desde un pedido y le modifica los surtidos al pedido según los que se facturaron
        
    End Sub
    Public Function ReporteVentasSeries(ByVal pidCompra As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tvi.iddetalle,tblinventario.clave,tblinventario.nombre as descripcion,tvi.cantidad,tblinventarioseries.noserie,tblinventarioseries.fechagarantia,'' as serie,tblcompras.referencia as folio,tblcompras.fecha,tblcompras.hora,tblproveedores.nombre as cnombre,tblproveedores.clave as cclave from tblcomprasdetalles tvi inner join tblinventario on tvi.idinventario=tblinventario.idinventario inner join tblinventarioseries on tvi.idinventario=tblinventarioseries.idinventario and tvi.idcompra=tblinventarioseries.idcompra inner join tblcompras on tvi.idcompra=tblcompras.idcompra inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor where tvi.idcompra=" + pidCompra.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasseriesc")
        'DS.WriteXmlSchema("tblventasseriesc.xml")
        Return DS.Tables("tblcomprasseriesc").DefaultView
    End Function
    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, pIdTipoSucursal As Integer, pOrdenPorprov As Boolean, pidTipoProv As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblcompras.idcompra,tblcompras.referencia,tblcompras.estado,if(tblcompras.idmoneda=2,tblcompras.total,tblcompras.total*tblcompras.tipodecambio) as total,if(tblcompras.idmoneda=2,tblcompras.totalapagar,tblcompras.totalapagar*tblcompras.tipodecambio) as totalapagar,tblcompras.fecha,tblcompras.tipodecambio,tblcompras.idmoneda,tblcomprasdetalles.cantidad,tblinventario.nombre as descripcion,tblcomprasdetalles.precio,tblcomprasdetalles.idinventario,tblformasdepago.tipo as formadepago,tblproveedores.nombre as cnombre,tblcompras.costoindirecto as cindirecto,tblcompras.serie,tblcompras.folioi,tblcompras.idproveedor, " + _
            "ifnull(((select sum(if(tblcomprasdetalles.idmoneda=2,precio,precio*tblcompras.tipodecambio)) from tblcomprasdetalles where tblcomprasdetalles.idcompra=tblcompras.idcompra)/(select sum(cantidad) from tblcomprasdetalles where tblcomprasdetalles.idcompra=tblcompras.idcompra)),0) as costoprom,if(tblcomprasdetalles.idmoneda=2,tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100,tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio) ivaretenido,if(tblcomprasdetalles.idmoneda=2,tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100,tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio) ieps,if(tblcomprasdetalles.idmoneda=2,tblcomprasdetalles.precio*tblcomprasdetalles.iva/100,tblcomprasdetalles.precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio) iva " + _
            "from tblcompras inner join tblcomprasdetalles on tblcompras.idcompra=tblcomprasdetalles.idcompra inner join tblinventario on tblcomprasdetalles.idinventario=tblinventario.idinventario inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma inner join tblsucursales s on tblcompras.idsucursal=s.idsucursal "
        Else
            Comm.CommandText = "select tblcompras.idcompra,tblcompras.referencia,tblcompras.estado,tblcompras.total as total,tblcompras.totalapagar as totalapagar,tblcompras.fecha,tblcompras.tipodecambio,tblcompras.idmoneda,tblcomprasdetalles.cantidad,tblinventario.nombre as descripcion,tblcomprasdetalles.precio,tblcomprasdetalles.idinventario,tblformasdepago.tipo as formadepago,tblproveedores.nombre as cnombre,tblcompras.costoindirecto as cindirecto,tblcompras.serie,tblcompras.folioi,tblcompras.idproveedor," + _
            "ifnull(((select sum(precio) from tblcomprasdetalles where tblcomprasdetalles.idcompra=tblcompras.idcompra)/(select sum(cantidad) from tblcomprasdetalles where tblcomprasdetalles.idcompra=tblcompras.idcompra)),0) as costoprom,,tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100 ivaretenido,tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100 ieps,tblcomprasdetalles.precio*tblcomprasdetalles.iva/100 iva " + _
            "from tblcompras inner join tblcomprasdetalles on tblcompras.idcompra=tblcomprasdetalles.idcompra inner join tblinventario on tblcomprasdetalles.idinventario=tblinventario.idinventario inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma inner join tblsucursales s on tblcompras.idsucursal=s.idsucursal "
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pSoloCanceladas Then
            Comm.CommandText += "where tblcompras.fechacancelado>='" + pFecha1 + "' and tblcompras.fechacancelado<='" + pFecha2 + "'"
            Comm.CommandText += " and tblcompras.estado=4"
        Else
            Comm.CommandText += "where tblcompras.fecha>='" + pFecha1 + "' and tblcompras.fecha<='" + pFecha2 + "'"
            Comm.CommandText += " and tblcompras.estado=3"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblcompras.idproveedor=" + pIdProveedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblcompras.idmoneda=" + pidMoneda.ToString
        End If
        'If pidInventario > 1 Then
        '    Comm.CommandText += " and tblventasinventario.idinventario=" + pidInventario.ToString
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
        If pOrdenPorprov = False Then
            Comm.CommandText += " order by tblcompras.fecha,tblcompras.serie,tblcompras.folioi"
        Else
            Comm.CommandText += " order by cnombre,tblcompras.idcompra"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasrep")
        'DS.WriteXmlSchema("tblcomprasrep.xml")
        Return DS.Tables("tblcomprasrep").DefaultView
    End Function
    Public Function ReporteComprasArticulos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, pIdTipoSucursal As Integer, pidTipoProv As Integer, pIdAlmacen As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblcompras.idcompra,tblcompras.serie,tblcompras.folioi,tblcompras.referencia,tblcompras.estado,tblcompras.fecha,tblcompras.tipodecambio,tblcompras.idmoneda,tblcomprasdetalles.cantidad,tblinventario.nombre as descripcion,if(tblcomprasdetalles.idmoneda=2,tblcomprasdetalles.precio,tblcomprasdetalles.precio*tblcompras.tipodecambio) as precio,0 as costoinv,0 as costopro,tblformasdepago.tipo as formadepago,tblcomprasdetalles.iva,tblinventario.clave,tblcomprasdetalles.costoindirecto as cindirecto,tblproveedores.clave as clavep,tblproveedores.nombre as nombrep," + _
            "if(tblcomprasdetalles.idmoneda=2,tblcomprasdetalles.iva*tblcomprasdetalles.precio/100,tblcomprasdetalles.precio*tblcompras.tipodecambio*tblcomprasdetalles.iva/100) as ivacalc,if(tblcomprasdetalles.idmoneda=2,tblcomprasdetalles.ieps*tblcomprasdetalles.precio/100,tblcomprasdetalles.precio*tblcompras.tipodecambio*tblcomprasdetalles.ieps/100) as iepscalc,if(tblcomprasdetalles.idmoneda=2,tblcomprasdetalles.ivaretenido*tblcomprasdetalles.precio/100,tblcomprasdetalles.precio*tblcompras.tipodecambio*tblcomprasdetalles.ivaretenido/100) as ivaretcalc " + _
            "from tblcompras inner join tblcomprasdetalles on tblcompras.idcompra=tblcomprasdetalles.idcompra inner join tblinventario on tblcomprasdetalles.idinventario=tblinventario.idinventario inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblsucursales s on tblcompras.idsucursal=s.idsucursal  where tblcompras.estado=3 and tblcompras.fecha>='" + pFecha1 + "' and tblcompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblcompras.idcompra,tblcompras.serie,tblcompras.folioi,tblcompras.referencia,tblcompras.estado,tblcompras.fecha,tblcompras.tipodecambio,tblcompras.idmoneda,tblcomprasdetalles.cantidad,tblinventario.nombre as descripcion,tblcomprasdetalles.precio,0 as costoinv,0 as costopro,tblformasdepago.tipo as formadepago,tblcomprasdetalles.iva,tblinventario.clave,tblcomprasdetalles.costoindirecto as cindirecto,tblproveedores.clave as clavep,tblproveedores.nombre as nombrep," + _
            "tblcomprasdetalles.iva*tblcomprasdetalles.precio/100 as ivacalc,tblcomprasdetalles.ieps*tblcomprasdetalles.precio/100 as iepscalc,tblcomprasdetalles.ivaretenido*tblcomprasdetalles.precio/100 as ivaretcalc " + _
            "from tblcompras inner join tblcomprasdetalles on tblcompras.idcompra=tblcomprasdetalles.idcompra inner join tblinventario on tblcomprasdetalles.idinventario=tblinventario.idinventario inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblsucursales s on tblcompras.idsucursal=s.idsucursal where tblcompras.estado=3 and tblcompras.fecha>='" + pFecha1 + "' and tblcompras.fecha<='" + pFecha2 + "'"
            'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,if(tblventasinventario.idinventario>1,tblinventario.nombre,if(tblventasinventario.idvariante>1,tblproductos.nombre,'SERVICIO')) as descripcion,tblventasinventario.precio,0 as costoinv,0 as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tblventasinventario.iva,tblventas.isr,tblventas.ivaretenido from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        If pIdAlmacen > 0 Then Comm.CommandText += " abd tblcomprasdetalles.idalmacen=" + pIdAlmacen.ToString
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblcompras.idproveedor=" + pIdProveedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblcomprasdetalles.idmoneda=" + pidMoneda.ToString
        End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tblcomprasdetalles.idinventario=" + pIdInventario.ToString
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
        Comm.CommandText += " order by tblcompras.fecha,tblcompras.serie,tblcompras.folioi"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasrep")
        'DS.WriteXmlSchema("tblcomprasrepa.xml")
        Return DS.Tables("tblcomprasrep").DefaultView
    End Function
    Public Function ReporteOrdenesSurtir(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer, ByVal pidMoneda As Integer, pIdTipoSucursal As Integer, pTodos As Boolean) As DataView
        Dim DS As New DataSet

        Comm.CommandText = "select tblcompraspedidos.idpedido as idcompra, tblcompraspedidos.serie,tblcompraspedidos.folio as folioi, '' as referencia, tblcompraspedidos.estado, tblcompraspedidos.fecha, 0 as tipodecambio, tblcompraspedidos.idmoneda, tblcompraspedidosdetalles.cantidad, tblinventario.nombre as descripcion, tblcompraspedidosdetalles.precio, 0 as costoinv, 0 as costopro, 0 as formadepago, tblcompraspedidosdetalles.iva, tblinventario.clave, 0 as costoindirecto, tblproveedores.clave as clavep, tblproveedores.nombre as nombrep, tblcompraspedidosdetalles.iva * tblcompraspedidosdetalles.precio/100 as ivacalc, tblcompraspedidosdetalles.ieps * tblcompraspedidosdetalles.precio/100 as iepscalc, tblcompraspedidosdetalles.ivaretenido * tblcompraspedidosdetalles.precio/100 as ivaretcalc, tblcompraspedidosdetalles.surtido from tblcompraspedidos inner join tblcompraspedidosdetalles on tblcompraspedidos.idpedido = tblcompraspedidosdetalles.idpedido inner join tblinventario on tblcompraspedidosdetalles.idinventario = tblinventario.idinventario inner join tblproveedores on tblcompraspedidos.idproveedor = tblproveedores.idproveedor inner join tblsucursales s on tblcompraspedidos.idsucursal = s.idsucursal where tblcompraspedidos.estado = 3 "
        If pTodos Then
            Comm.CommandText += " and tblcompraspedidos.fecha>='" + pFecha1 + "' and tblcompraspedidos.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText += " and tblcompraspedidosdetalles.cantidad>tblcompraspedidosdetalles.surtido"
        End If
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdSucursal > 0 Then Comm.CommandText += " and tblcompraspedidos.idsucursal=" + pIdSucursal.ToString
        If pIdProveedor > 0 Then Comm.CommandText += " and tblcompraspedidos.idproveedor=" + pIdProveedor.ToString
        If pidMoneda > 0 Then Comm.CommandText += " and tblcompraspedidosdetalles.idmoneda=" + pidMoneda.ToString
        If pIdInventario > 1 Then
            Comm.CommandText += " and tblcompraspedidosdetalles.idinventario=" + pIdInventario.ToString
        Else
            If pidClasificacion > 0 Then Comm.CommandText += " and tblinventario.idclasificacion=" + pidClasificacion.ToString
            If pidClasificacion2 > 0 Then Comm.CommandText += " and tblinventario.idclasificacion2=" + pidClasificacion.ToString
            If pidClasificacion3 > 0 Then Comm.CommandText += " and tblinventario.idclasificacion3=" + pidClasificacion.ToString
        End If
        Comm.CommandText += " order by tblcompraspedidos.fecha,tblcompraspedidos.serie"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasrep")
        'DS.WriteXmlSchema("tblcomprasrepa.xml")
        Return DS.Tables("tblcomprasrep").DefaultView
    End Function
    Public Function ReporteProveedoresDeudas(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, pidTipoProv As Integer) As DataView
        Dim DS As New DataSet
        If pIdProveedor = 0 Then
            Comm.CommandText = "delete from tblproveedoresrepdeudas"
        Else
            Comm.CommandText = "delete from tblproveedoresrepdeudas where idproveedor=" + pIdProveedor.ToString
        End If
        Comm.ExecuteNonQuery()
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblproveedoresrepdeudas(idcompra,idproveedor,folio,estado,total,totalapagar,fecha,tipodecambio,idmoneda,cnombre,abonado,clave) select tblcompras.idcompra,tblcompras.idproveedor,tblcompras.referencia,tblcompras.estado,if(tblcompras.idmoneda=2,tblcompras.total,tblcompras.total*tblcompras.tipodecambio) as total,if(tblcompras.idmoneda=2,tblcompras.totalapagar,tblcompras.totalapagar*tblcompras.tipodecambio) as totalapagar,tblcompras.fecha,tblcompras.tipodecambio,tblcompras.idmoneda,tblproveedores.nombre cnombre,if(tblcompras.idmoneda=2,tblcompras.credito,tblcompras.credito*tblcompras.tipodecambio) as abonado,tblproveedores.clave " + _
                    "from tblcompras inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma where tblcompras.estado=3 and tblformasdepago.tipo=0 and round(tblcompras.credito,2)<round(tblcompras.totalapagar,2)"
        Else
            Comm.CommandText = "insert into tblproveedoresrepdeudas(idcompra,idproveedor,folio,estado,total,totalapagar,fecha,tipodecambio,idmoneda,cnombre,abonado,clave) select tblcompras.idcompra,tblcompras.idproveedor,tblcompras.referencia,tblcompras.estado,tblcompras.total,tblcompras.totalapagar,tblcompras.fecha,tblcompras.tipodecambio,tblcompras.idmoneda,tblproveedores.nombre cnombre,tblcompras.credito as abonado,tblproveedores.clave " + _
                     "from tblcompras inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma where tblcompras.estado=3 and tblformasdepago.tipo=0 and round(tblcompras.credito,2)<round(tblcompras.totalapagar,2)"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblcompras.idproveedor=" + pIdProveedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblcompras.idmoneda=" + pidMoneda.ToString
        End If
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        Comm.ExecuteNonQuery()
        'If pIdVendedor > 0 Then
        'Comm.CommandText += " and tblventas.idvendedor=" + pIdVendedor.ToString
        'End If
        'Notas de cargo
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblproveedoresrepdeudas(idcompra,idproveedor,folio,estado,total,totalapagar,fecha,tipodecambio,idmoneda,cnombre,abonado,clave) select tblnotasdecargocompras.idcargo,tblnotasdecargocompras.idproveedor,tblnotasdecargocompras.folio,tblnotasdecargocompras.estado,if(tblnotasdecargocompras.idmoneda=2,tblnotasdecargocompras.total,tblnotasdecargocompras.total*tblnotasdecargocompras.tipodecambio) as total,if(tblnotasdecargocompras.idmoneda=2,tblnotasdecargocompras.totalapagar,tblnotasdecargocompras.totalapagar*tblnotasdecargocompras.tipodecambio) as totalapagar,tblnotasdecargocompras.fecha,tblnotasdecargocompras.tipodecambio,tblnotasdecargocompras.idmoneda,tblproveedores.nombre as cnombre,if(tblnotasdecargocompras.idmoneda=2,tblnotasdecargocompras.aplicado,tblnotasdecargocompras.aplicado*tblnotasdecargocompras.tipodecambio) as abonado,tblproveedores.clave " + _
                    "from tblnotasdecargocompras inner join tblproveedores on tblnotasdecargocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecargocompras.estado=3 and round(tblnotasdecargocompras.aplicado,2)<round(tblnotasdecargocompras.totalapagar,2)"
        Else
            Comm.CommandText = "insert into tblproveedoresrepdeudas(idcompra,idproveedor,folio,estado,total,totalapagar,fecha,tipodecambio,idmoneda,cnombre,abonado,clave) select tblnotasdecargocompras.idcargo,tblnotasdecargocompras.idproveedor,tblnotasdecargocompras.folio,tblnotasdecargocompras.estado,tblnotasdecargocompras.total,tblnotasdecargocompras.totalapagar,tblnotasdecargocompras.fecha,tblnotasdecargocompras.tipodecambio,tblnotasdecargocompras.idmoneda,tblproveedores.nombre as cnombre,tblnotasdecargocompras.aplicado as abonado,tblproveedores.clave " + _
                    "from tblnotasdecargocompras inner join tblproveedores on tblnotasdecargocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecargocompras.estado=3 and round(tblnotasdecargocompras.aplicado,2)<round(tblnotasdecargocompras.totalapagar,2)"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idproveedor=" + pIdProveedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblnotasdecargocompras.idmoneda=" + pidMoneda.ToString
        End If
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        Comm.ExecuteNonQuery()
        'Documentos
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblproveedoresrepdeudas(idcompra,idproveedor,folio,estado,total,totalapagar,fecha,tipodecambio,idmoneda,cnombre,abonado,clave) " + _
            "select dc.iddocumento,dc.idproveedor,if(dc.tiposaldo=0,concat(dc.serie,convert(dc.folio using utf8)),concat(dc.seriereferencia,convert(dc.folioreferencia using utf8))),dc.estado,dc.totalapagar as total,if(dc.idmoneda=2,dc.totalapagar,dc.totalapagar*dc.tipodecambio) as totalapagar,dc.fecha,dc.tipodecambio,dc.idmoneda,tblproveedores.nombre as cnombre,if(dc.idmoneda=2,dc.credito,dc.credito*dc.tipodecambio) as abonado,tblproveedores.clave " + _
                    "from tbldocumentosproveedores as dc inner join tblproveedores on dc.idproveedor=tblproveedores.idproveedor where dc.estado=3 and round(dc.credito,2)<round(dc.totalapagar,2)"
        Else
            Comm.CommandText = "insert into tblproveedoresrepdeudas(idcompra,idproveedor,folio,estado,total,totalapagar,fecha,tipodecambio,idmoneda,cnombre,abonado,clave) " + _
       "select dc.iddocumento,dc.idproveedor,if(dc.tiposaldo=0,concat(dc.serie,convert(dc.folio using utf8)),concat(dc.seriereferencia,convert(dc.folioreferencia using utf8))),dc.estado,dc.totalapagar as total,dc.totalapagar as totalapagar,dc.fecha,dc.tipodecambio,dc.idmoneda,tblproveedores.nombre as cnombre,dc.credito as abonado,tblproveedores.clave " + _
               "from tbldocumentosproveedores as dc inner join tblproveedores on dc.idproveedor=tblproveedores.idproveedor where dc.estado=3 and round(dc.credito,2)<round(dc.totalapagar,2)"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and dc.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and dc.idproveedor=" + pIdProveedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and dc.idmoneda=" + pidMoneda.ToString
        End If
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        Comm.ExecuteNonQuery()
        If pIdProveedor = 0 Then
            Comm.CommandText = "select idcompra,idproveedor,folio,total,totalapagar,fecha,tipodecambio,idmoneda,cnombre,abonado,clave from tblproveedoresrepdeudas order by cnombre,fecha,folio"
        Else
            Comm.CommandText = "select idcompra,idproveedor,folio,total,totalapagar,fecha,tipodecambio,idmoneda,cnombre,abonado,clave from tblproveedoresrepdeudas where idproveedor=" + pIdProveedor.ToString + " order by cnombre,fecha,folio"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasdeudas")
        'DS.WriteXmlSchema("tblcomprasdeudas.xml")
        Return DS.Tables("tblcomprasdeudas").DefaultView
    End Function
    Public Sub AsignaCostoIndirecto(ByVal pidCompra As Integer, ByVal pTipoProrrateo As Byte)
        If pTipoProrrateo = 0 Then
            Dim TotalCantidadC As Double
            Comm.CommandText = "select ifnull((select sum(cantidad) from tblcomprasdetalles where idcompra=" + pidCompra.ToString + "),0)"
            TotalCantidadC = Comm.ExecuteScalar
            If TotalCantidadC > 0 Then
                Comm.CommandText = "update tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra set tblcomprasdetalles.costoindirecto=cantidad*tblcompras.costoindirecto/" + TotalCantidadC.ToString + " where tblcompras.idcompra=" + pidCompra.ToString
                Comm.ExecuteNonQuery()
            End If
        End If
        If pTipoProrrateo = 1 Then
            Comm.CommandText = "update tblcomprasdetalles inner join tblcompras on tblcomprasdetalles.idcompra=tblcompras.idcompra set tblcomprasdetalles.costoindirecto=(precio/tblcompras.total)*tblcompras.costoindirecto where tblcompras.idcompra=" + pidCompra.ToString
            Comm.ExecuteNonQuery()
        End If

    End Sub

    Public Function ReporteViejosSaldos(ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pTipodeCambio As Double) As DataView
        '        Dim DS As New DataSet
        '        Dim F As String
        '        F = Format(Date.Now, "yyyy/MM/dd")
        '        'F = pFecha1
        '        If pIdProveedor > 0 Then
        '            Comm.CommandText = "delete from tblproveedoresviejossaldos where idproveedor=" + pIdProveedor.ToString
        '        Else
        '            Comm.CommandText = "delete from tblproveedoresviejossaldos"
        '        End If
        '        Comm.ExecuteNonQuery()
        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sesentadias," + _
        '"if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio) as credito,0 from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        '        Else
        '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sesentadias," + _
        '"(tblventas.totalapagar-tblventas.credito) as credito,0 from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdProveedor > 0 Then
        '            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        '        End If

        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        '---------Notas de Cargo

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,tblventas.folio,0,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.aplicado,(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio) as credito,1 from tblnotasdecargocompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where round(tblventas.totalapagar-tblventas.aplicado,2)>0 and tblventas.estado=3"
        '        Else
        '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,tblventas.folio,0,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.aplicado,0) as sesentadias," + _
        '            "(tblventas.totalapagar-tblventas.aplicado)*tblventas.tipodecambio as credito,1 from tblnotasdecargocompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where round(tblventas.totalapagar-tblventas.aplicado,2)>0 and tblventas.estado=3"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdProveedor > 0 Then
        '            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        '        End If

        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        '-------------Documentos Saldo Inicial

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio) as credito,2 from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        '        Else
        '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sesentadias," + _
        '            "(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio as credito,2 from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=0 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdProveedor > 0 Then
        '            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        '        End If

        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        'Documentos documentos

        '        If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio),0) as sesentadias," + _
        '            "if(tblventas.idmoneda=2,tblventas.totalapagar-tblventas.credito,(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio) as credito,3 from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=1 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        '        Else
        '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as quincedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as treintadias," + _
        '            "if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),tblventas.totalapagar-tblventas.credito,0) as sesentadias," + _
        '            "(tblventas.totalapagar-tblventas.credito)*tblventas.tipodecambio as credito,3 from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=1 and round(tblventas.totalapagar-tblventas.credito,2)>0 and tblventas.estado=3"
        '        End If

        '        If pIdSucursal > 0 Then
        '            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        '        End If
        '        If pIdProveedor > 0 Then
        '            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        '        End If

        '        If pidMoneda > 0 Then
        '            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        '        End If
        '        Comm.ExecuteNonQuery()
        '        'Saca saldos
        '        'Comm.CommandText = "delete from tblproveedoresmovimientossaldos"
        '        'Comm.ExecuteNonQuery()
        '        'Comm.CommandText = "insert into tblproveedoresmovimientossaldos(idproveedor,saldoant) select idproveedor,spdasaldoafechaproveedor(idproveedor,'" + pFecha1 + "') from tblproveedoresviejossaldos group by idproveedor"
        '        'Comm.ExecuteNonQuery()


        '        If pIdProveedor <= 0 Then
        '            Comm.CommandText = "select fecha,tblproveedores.clave,tblproveedores.nombre,tblproveedores.idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblproveedoresviejossaldos.credito,0 as saldoant from tblproveedoresviejossaldos inner join tblproveedores on tblproveedoresviejossaldos.idproveedor=tblproveedores.idproveedor   order by tblproveedores.nombre,fecha,serie,folio"
        '        Else
        '            Comm.CommandText = "select fecha,tblproveedores.clave,tblproveedores.nombre,tblproveedores.idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tipo,tblproveedoresviejossaldos.credito,0 as saldoant from tblproveedoresviejossaldos inner join tblproveedores on tblproveedoresviejossaldos.idproveedor=tblproveedores.idproveedor  where tblproveedoresviejossaldos.idproveedor=" + pIdProveedor.ToString + " order by tblproveedores.nombre,fecha,serie,folio"
        '        End If

        '        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        '        DA.Fill(DS, "tblcomprasviejo")
        '        'DS.WriteXmlSchema("tblcomprasviejo.xml")
        '        Return DS.Tables("tblcomprasviejo").DefaultView
        Dim DS As New DataSet
        Dim F As String
        F = Format(Date.Now, "yyyy/MM/dd")
        'F = pFecha2
        'Comm.CommandText = "select ifnull((select tipodecambio from tblcompras where fecha<='" + F + "' and idmoneda=2 order by fecha desc limit 1),1)"
        'pTipodeCambio = Comm.ExecuteScalar
        Comm.CommandTimeout = 10000
        If pIdProveedor > 0 Then
            Comm.CommandText = "delete from tblproveedoresviejossaldos where idproveedor=" + pIdProveedor.ToString
        Else
            Comm.CommandText = "delete from tblproveedoresviejossaldos"
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandTimeout = 10000
        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
        "tblventas.credito as credito," + _
        "0,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.idmoneda=2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandTimeout = 10000
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
            "tblventas.credito*tblventas.tipodecambio as credito," + _
            "0," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        Else
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "tblventas.credito as credito," + _
            "0,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        End If

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()



        '---------Notas de Cargo

        Comm.CommandTimeout = 10000

        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.folio,0,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "tblventas.aplicado as credito," + _
        "1,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tblnotasdecargocompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.estado=3 and tblventas.idmoneda=2 and round(tblventas.totalapagar-tblventas.aplicado,2)>0"



        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandTimeout = 10000
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.folio,0,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "tblventas.aplicado*tblventas.tipodecambio as credito," + _
            "1," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tblnotasdecargocompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.aplicado,2)>0"
        Else
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.folio,0,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "tblventas.aplicado as credito," + _
            "1,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tblnotasdecargocompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.aplicado,2)>0"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If

        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        '-------------Documentos Saldo Inicial
        Comm.CommandTimeout = 10000
        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "tblventas.credito as credito," + _
        "2,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=0 and tblventas.estado=3 and tblventas.idmoneda=2 and round(tblventas.totalapagar-tblventas.credito,2)>0"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandTimeout = 10000
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "tblventas.credito*tblventas.tipodecambio as credito," + _
            "2," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=0 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        Else
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "tblventas.credito as credito," + _
            "2,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=0 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        'Documentos documentos
        Comm.CommandTimeout = 10000
        'If pMostrarEnPesos = 0 Then

        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "tblventas.credito as credito," + _
        "3,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=1 and tblventas.estado=3 and tblventas.idmoneda=2 and round(tblventas.totalapagar-tblventas.credito,2)>0"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandTimeout = 10000
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "tblventas.credito*tblventas.tipodecambio as credito," + _
            "3," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=1 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        Else
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "tblventas.credito as credito," + _
            "3,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=1 and tblventas.estado=3 and tblventas.idmoneda<>2 and round(tblventas.totalapagar-tblventas.credito,2)>0"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        'Saca saldos
        'Comm.CommandText = "delete from tblproveedoresmovimientossaldos"
        'Comm.ExecuteNonQuery()
        'Comm.CommandText = "insert into tblproveedoresmovimientossaldos(idproveedor,saldoant) select idproveedor,spdasaldoafechaproveedor(idproveedor,'" + Format(DateAdd(DateInterval.Day, 1, CDate(pFecha2)), "yyyy/MM/dd") + "') from tblproveedoresviejossaldos group by idproveedor"
        'Comm.ExecuteNonQuery()

        Comm.CommandTimeout = 10000
        If pIdProveedor <= 0 Then
            Comm.CommandText = "select fecha,tblproveedores.clave,tblproveedores.nombre,tblproveedores.idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tblproveedoresviejossaldos.tipo,tblproveedoresviejossaldos.credito,0 as saldoant,tblproveedoresviejossaldos.tipodecambio,tblproveedoresviejossaldos.corriente from tblproveedoresviejossaldos inner join tblproveedores on tblproveedoresviejossaldos.idproveedor=tblproveedores.idproveedor order by tblproveedores.nombre,fecha,serie,folio"
        Else
            Comm.CommandText = "select fecha,tblproveedores.clave,tblproveedores.nombre,tblproveedores.idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tblproveedoresviejossaldos.tipo,tblproveedoresviejossaldos.credito,0 as saldoant,tblproveedoresviejossaldos.tipodecambio,tblproveedoresviejossaldos.corriente from tblproveedoresviejossaldos inner join tblproveedores on tblproveedoresviejossaldos.idproveedor=tblproveedores.idproveedor where tblproveedoresviejossaldos.idproveedor=" + pIdProveedor.ToString + " order by tblproveedores.nombre,fecha,serie,folio"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasviejo")
        'DS.WriteXmlSchema("tblcomprasviejo.xml")
        Return DS.Tables("tblcomprasviejo").DefaultView
    End Function

    Public Function ReporteViejosSaldosH(ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pTipodeCambio As Double, AFecha As Boolean, pidTipoProv As Integer) As DataView
        Dim DS As New DataSet
        Dim F As String
        'F = Format(Date.Now, "yyyy/MM/dd")
        F = pFecha2
        Comm.CommandTimeout = 10000
        'Comm.CommandText = "select ifnull((select tipodecambio from tblcompras where fecha<='" + F + "' and idmoneda<>2 order by fecha desc limit 1),1)"
        'pTipodeCambio = Comm.ExecuteScalar
        If pIdProveedor > 0 Then
            Comm.CommandText = "delete from tblproveedoresviejossaldos where idproveedor=" + pIdProveedor.ToString
        Else
            Comm.CommandText = "delete from tblproveedoresviejossaldos"
        End If
        Comm.ExecuteNonQuery()
        'If pMostrarEnPesos = 0 Then
        '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
        '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
        '"if(tblventas.idmoneda=2," + _
        '"ifnull((select sum(cantidad) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0)," + _
        '"ifnull((select sum(cantidad) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0)*tblventas.tipodecambio) as credito," + _
        '"0 from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3"
        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
        "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0) as credito," + _
        "0,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente,tblventas.totalapagar"

        If AFecha Then
            Comm.CommandText += ",tblventas.credito as creditoreal"
        Else
            Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0) as creditoreal"
        End If

        Comm.CommandText += " from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda=2"
        'Else
        '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
        '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
        '"if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
        '"if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
        '"ifnull((select sum(cantidad) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0) as credito,0 from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3"
        'End If
        Comm.CommandTimeout = 10000
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandTimeout = 10000
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
            "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0) as credito," + _
            "0," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar"
            If AFecha Then
                Comm.CommandText += ",tblventas.credito*tblventas.tipodecambio as creditoreal"
            Else
                Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0) as creditoreal"
            End If
            Comm.CommandText += " from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        Else
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "ifnull((select sum(if(tblcompraspagos.idmoneda<>2,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0) as credito," + _
            "0,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",tblventas.totalapagar"
            If AFecha Then
                Comm.CommandText += ",tblventas.credito as creditoreal"
            Else
                Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda<>2,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0) as creditoreal"
            End If
            Comm.CommandText += " from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        End If

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()


        Comm.CommandTimeout = 10000
        '---------Notas de Cargo
        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,tblventas.folio,0,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcargo=tblventas.idcargo),0) as credito," + _
        "1,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar"
        If AFecha Then
            Comm.CommandText += ",tblventas.aplicado as creditoreal"
        Else
            Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcargo=tblventas.idcargo),0) as creditoreal"
        End If
        Comm.CommandText += " from tblnotasdecargocompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda=2"



        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandTimeout = 10000
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,tblventas.folio,0,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcargo=tblventas.idcargo),0) as credito," + _
            "1," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar"
            If AFecha Then
                Comm.CommandText += ",tblventas.aplicado*tblventas.tipodecambio as creditoreal"
            Else
                Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcargo=tblventas.idcargo),0) as creditoreal"
            End If
            Comm.CommandText += " from tblnotasdecargocompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        Else
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,tblventas.folio,0,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "ifnull((select sum(if(tblcompraspagos.idmoneda<>2,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcargo=tblventas.idcargo),0) as credito," + _
            "1,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar"
            If AFecha Then
                Comm.CommandText += ",tblventas.aplicado as creditoreal"
            Else
                Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda<>2,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcargo=tblventas.idcargo),0) as creditoreal"
            End If
            Comm.CommandText += " from tblnotasdecargocompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        '-------------Documentos Saldo Inicial
        Comm.CommandTimeout = 10000
        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
        "2,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar"
        If AFecha Then
            Comm.CommandText += ",tblventas.credito"
        Else
            Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as creditoreal"
        End If
        Comm.CommandText += " from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda=2"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandTimeout = 10000
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
            "2," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar"
            If AFecha Then
                Comm.CommandText += ",tblventas.credito*tblventas.tipodecambio"
            Else
                Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as creditoreal"
            End If
            Comm.CommandText += " from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        Else
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "ifnull((select sum(if(tblcompraspagos.idmoneda<>2,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
            "2,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar"
            If AFecha Then
                Comm.CommandText += ",tblventas.credito"
            Else
                Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda<>2,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as creditoreal"
            End If
            Comm.CommandText += " from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        'Documentos documentos
        Comm.CommandTimeout = 10000
        'If pMostrarEnPesos = 0 Then

        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
        "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
        "3,0," + _
        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
        ",totalapagar"
        If AFecha Then
            Comm.CommandText += ",tblventas.credito"
        Else
            Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as creditoreal"
        End If
        Comm.CommandText += " from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda=2"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        Comm.CommandTimeout = 10000
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
            "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
            "3," + pTipodeCambio.ToString + "," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
            ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar"
            If AFecha Then
                Comm.CommandText += ",tblventas.credito*tblventas.tipodecambio"
            Else
                Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as creditoreal"
            End If
            Comm.CommandText += " from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        Else
            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar,creditoreal) select tblventas.fecha,tblproveedores.idproveedor,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
            "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
            "ifnull((select sum(if(tblcompraspagos.idmoneda<>2,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
            "3,0," + _
            "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
            ",totalapagar"
            If AFecha Then
                Comm.CommandText += ",tblventas.credito"
            Else
                Comm.CommandText += ",ifnull((select sum(if(tblcompraspagos.idmoneda<>2,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as creditoreal"
            End If
            Comm.CommandText += " from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
        End If
        If pidTipoProv > 0 Then Comm.CommandText += " and tblproveedores.idtipo=" + pidTipoProv.ToString
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
        End If
        Comm.ExecuteNonQuery()
        'Saca saldos
        'Comm.CommandText = "delete from tblproveedoresmovimientossaldos"
        'Comm.ExecuteNonQuery()
        'Comm.CommandText = "insert into tblproveedoresmovimientossaldos(idproveedor,saldoant) select idproveedor,spdasaldoafechaproveedor(idproveedor,'" + Format(DateAdd(DateInterval.Day, 1, CDate(pFecha2)), "yyyy/MM/dd") + "') from tblproveedoresviejossaldos group by idproveedor"
        'Comm.ExecuteNonQuery()
        Comm.CommandTimeout = 10000

        If pIdProveedor <= 0 Then
            Comm.CommandText = "select fecha,tblproveedores.clave,tblproveedores.nombre,tblproveedores.idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tblproveedoresviejossaldos.tipo,tblproveedoresviejossaldos.credito,0 as saldoant,tblproveedoresviejossaldos.tipodecambio,tblproveedoresviejossaldos.corriente from tblproveedoresviejossaldos inner join tblproveedores on tblproveedoresviejossaldos.idproveedor=tblproveedores.idproveedor where round(totalapagar-creditoreal,2)>0  order by tblproveedores.nombre,fecha,serie,folio"
        Else
            Comm.CommandText = "select fecha,tblproveedores.clave,tblproveedores.nombre,tblproveedores.idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tblproveedoresviejossaldos.tipo,tblproveedoresviejossaldos.credito,0 as saldoant,tblproveedoresviejossaldos.tipodecambio,tblproveedoresviejossaldos.corriente from tblproveedoresviejossaldos inner join tblproveedores on tblproveedoresviejossaldos.idproveedor=tblproveedores.idproveedor where tblproveedoresviejossaldos.idproveedor=" + pIdProveedor.ToString + " and round(totalapagar-creditoreal,2)>0 order by tblproveedores.nombre,fecha,serie,folio"
        End If

        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasviejo")
        'DS.WriteXmlSchema("tblcomprasviejo.xml")
        Return DS.Tables("tblcomprasviejo").DefaultView
    End Function

    'Public Function ReporteViejosSaldosH(ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pTipodeCambio As Double) As DataView
    '    Dim DS As New DataSet
    '    Dim F As String
    '    'F = Format(Date.Now, "yyyy/MM/dd")
    '    F = pFecha2
    '    Comm.CommandTimeout = 10000
    '    Comm.CommandText = "select ifnull((select tipodecambio from tblcompras where fecha>='" + F + "' and idmoneda<>2 order by fecha desc limit 1),1)"
    '    pTipodeCambio = Comm.ExecuteScalar
    '    If pIdProveedor > 0 Then
    '        Comm.CommandText = "delete from tblproveedoresviejossaldos where idproveedor=" + pIdProveedor.ToString
    '    Else
    '        Comm.CommandText = "delete from tblproveedoresviejossaldos"
    '    End If
    '    Comm.ExecuteNonQuery()
    '    'If pMostrarEnPesos = 0 Then
    '    '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '    '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sietedias," + _
    '    '            "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
    '    '"if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
    '    '"if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
    '    '"if(tblventas.idmoneda=2," + _
    '    '"ifnull((select sum(cantidad) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0)," + _
    '    '"ifnull((select sum(cantidad) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0)*tblventas.tipodecambio) as credito," + _
    '    '"0 from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3"
    '    Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
    '    "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0) as credito," + _
    '    "0,0," + _
    '    "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
    '    ",totalapagar from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda=2"
    '    'Else
    '    '            Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '    '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
    '    '           "if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-7),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
    '    '"if('" + F + "'>=date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d') and '" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-15),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
    '    '"if('" + F + "'<date_format(adddate(fecha,tblproveedores.diasdecredito-30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
    '    '"ifnull((select sum(cantidad) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0) as credito,0 from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0 and  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3"
    '    'End If
    '    Comm.CommandTimeout = 10000
    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdProveedor > 0 Then
    '        Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
    '    End If

    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
    '    End If
    '    Comm.ExecuteNonQuery()
    '    Comm.CommandTimeout = 10000
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as quincedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as treintadias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sesentadias," + _
    '        "ifnull((select sum(if(tblcompraspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0) as credito," + _
    '        "0," + pTipodeCambio.ToString + "," + _
    '        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
    '        ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
    '    Else
    '        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,concat(tblventas.referencia,' - ',tblventas.serie),tblventas.folioi,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
    '        "ifnull((select sum(if(tblcompraspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcompra=tblventas.idcompra),0) as credito," + _
    '        "0,0," + _
    '        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
    '        ",totalapagar from tblcompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblventas.idforma=tblformasdepago.idforma where tblformasdepago.tipo=0  and tblventas.fecha>='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
    '    End If

    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdProveedor > 0 Then
    '        Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
    '    End If

    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
    '    End If
    '    Comm.ExecuteNonQuery()


    '    Comm.CommandTimeout = 10000
    '    '---------Notas de Cargo



    '    Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.folio,0,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
    '    "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcargo=tblventas.idcargo),0) as credito," + _
    '    "1,0," + _
    '    "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
    '    ",totalapagar from tblnotasdecargocompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda=2"



    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdProveedor > 0 Then
    '        Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
    '    End If

    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
    '    End If
    '    Comm.ExecuteNonQuery()
    '    Comm.CommandTimeout = 10000
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.folio,0,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
    '        "ifnull((select sum(if(tblcompraspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcargo=tblventas.idcargo),0) as credito," + _
    '        "1," + pTipodeCambio.ToString + "," + _
    '        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
    '        ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tblnotasdecargocompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
    '    Else
    '        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.folio,0,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
    '        "ifnull((select sum(if(tblcompraspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.idcargo=tblventas.idcargo),0) as credito," + _
    '        "1,0," + _
    '        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
    '        ",totalapagar from tblnotasdecargocompras as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
    '    End If
    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdProveedor > 0 Then
    '        Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
    '    End If

    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
    '    End If
    '    Comm.ExecuteNonQuery()
    '    '-------------Documentos Saldo Inicial
    '    Comm.CommandTimeout = 10000
    '    Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
    '    "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
    '    "2,0," + _
    '    "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
    '    ",totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda=2"

    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdProveedor > 0 Then
    '        Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
    '    End If
    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
    '    End If
    '    Comm.ExecuteNonQuery()
    '    Comm.CommandTimeout = 10000
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
    '        "ifnull((select sum(if(tblcompraspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
    '        "2," + pTipodeCambio.ToString + "," + _
    '        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
    '        ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
    '    Else
    '        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.serie,tblventas.folio,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
    '        "ifnull((select sum(if(tblcompraspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
    '        "2,0," + _
    '        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
    '        ",totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=0 and  tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
    '    End If
    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdProveedor > 0 Then
    '        Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
    '    End If
    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
    '    End If
    '    Comm.ExecuteNonQuery()
    '    'Documentos documentos
    '    Comm.CommandTimeout = 10000
    '    'If pMostrarEnPesos = 0 Then

    '    Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
    '    "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
    '    "ifnull((select sum(if(tblcompraspagos.idmoneda=2,cantidad,cantidad*ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
    '    "3,0," + _
    '    "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
    '    ",totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda=2"

    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdProveedor > 0 Then
    '        Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
    '    End If
    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
    '    End If
    '    Comm.ExecuteNonQuery()
    '    Comm.CommandTimeout = 10000
    '    If pMostrarEnPesos = 0 Then
    '        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,(tblventas.totalapagar)*tblventas.tipodecambio),0) as sietedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as quincedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as treintadias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as sesentadias," + _
    '        "ifnull((select sum(if(tblcompraspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
    '        "3," + pTipodeCambio.ToString + "," + _
    '        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),if(tblventas.idmoneda=2,tblventas.totalapagar,tblventas.totalapagar*tblventas.tipodecambio),0) as corriente" + _
    '        ",tblventas.totalapagar*tblventas.tipodecambio as totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
    '    Else
    '        Comm.CommandText = "insert into tblproveedoresviejossaldos(fecha,idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,credito,tipo,tipodecambio,corriente,totalapagar) select tblventas.fecha,tblproveedores.idproveedor,tblventas.seriereferencia,tblventas.folioreferencia,date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') as limite," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d'),tblventas.totalapagar,0) as sietedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+7),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d'),tblventas.totalapagar,0) as quincedias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+15),'%Y/%m/%d') and '" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as treintadias," + _
    '        "if('" + F + "'>date_format(adddate(fecha,tblproveedores.diasdecredito+30),'%Y/%m/%d'),tblventas.totalapagar,0) as sesentadias," + _
    '        "ifnull((select sum(if(tblcompraspagos.idmoneda=tblventas.idmoneda,cantidad,cantidad/ptipodecambio)) from tblcompraspagos where idproveedor=tblventas.idproveedor and tblcompraspagos.estado=3 and tblcompraspagos.fecha<='" + pFecha2 + "' and tblcompraspagos.iddocumentod=tblventas.iddocumento),0) as credito," + _
    '        "3,0," + _
    '        "if('" + F + "'<=date_format(adddate(fecha,tblproveedores.diasdecredito),'%Y/%m/%d'),tblventas.totalapagar,0) as corriente" + _
    '        ",totalapagar from tbldocumentosproveedores as tblventas inner join tblproveedores on tblventas.idproveedor=tblproveedores.idproveedor where tblventas.tiposaldo=1  and tblventas.fecha<='" + pFecha2 + "' and tblventas.estado=3 and tblventas.idmoneda<>2"
    '    End If
    '    If pIdSucursal > 0 Then
    '        Comm.CommandText += " and tblventas.idsucursal=" + pIdSucursal.ToString
    '    End If
    '    If pIdProveedor > 0 Then
    '        Comm.CommandText += " and tblventas.idproveedor=" + pIdProveedor.ToString
    '    End If
    '    If pidMoneda > 0 Then
    '        Comm.CommandText += " and tblventas.idmoneda=" + pidMoneda.ToString
    '    End If
    '    Comm.ExecuteNonQuery()
    '    'Saca saldos
    '    'Comm.CommandText = "delete from tblproveedoresmovimientossaldos"
    '    'Comm.ExecuteNonQuery()
    '    'Comm.CommandText = "insert into tblproveedoresmovimientossaldos(idproveedor,saldoant) select idproveedor,spdasaldoafechaproveedor(idproveedor,'" + Format(DateAdd(DateInterval.Day, 1, CDate(pFecha2)), "yyyy/MM/dd") + "') from tblproveedoresviejossaldos group by idproveedor"
    '    'Comm.ExecuteNonQuery()
    '    Comm.CommandTimeout = 10000

    '    If pIdProveedor <= 0 Then
    '        Comm.CommandText = "select fecha,tblproveedores.clave,tblproveedores.nombre,tblproveedores.idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tblproveedoresviejossaldos.tipo,tblproveedoresviejossaldos.credito,0 as saldoant,tblproveedoresviejossaldos.tipodecambio,tblproveedoresviejossaldos.corriente from tblproveedoresviejossaldos inner join tblproveedores on tblproveedoresviejossaldos.idproveedor=tblproveedores.idproveedor where round(totalapagar-credito,2)>0 and tblproveedoresviejossaldos.fecha>='" + pFecha1 + "' order by tblproveedores.nombre,fecha,serie,folio"
    '    Else
    '        Comm.CommandText = "select fecha,tblproveedores.clave,tblproveedores.nombre,tblproveedores.idproveedor,serie,folio,limite,sietedias,quincedias,treintadias,sesentadias,tblproveedoresviejossaldos.tipo,tblproveedoresviejossaldos.credito,0 as saldoant,tblproveedoresviejossaldos.tipodecambio,tblproveedoresviejossaldos.corriente from tblproveedoresviejossaldos inner join tblproveedores on tblproveedoresviejossaldos.idproveedor=tblproveedores.idproveedor where tblproveedoresviejossaldos.idproveedor=" + pIdProveedor.ToString + " and round(totalapagar-credito,2)>0 and tblproveedoresviejossaldos.fecha>='" + pFecha1 + "' order by tblproveedores.nombre,fecha,serie,folio"
    '    End If

    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblcomprasviejo")
    '    'DS.WriteXmlSchema("tblcomprasviejo.xml")
    '    Return DS.Tables("tblcomprasviejo").DefaultView
    'End Function

    Public Function ReporteNotasDeCreditoPorConcepto(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pIdConcepto As Integer, pIdTipoSucursal As Integer, pIdTipoProv As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idnota idventa,v.folio,v.estado,if(v.idmoneda=2,ifnull((select sum(precio) from tblnotasdecreditodetallesc where idnota=v.idnota),0),ifnull((select sum(precio) from tblnotasdecreditodetallesc where idnota=v.idnota),0)*v.tipodecambio) as total," + _
            "if(v.idmoneda=2,ifnull((select sum(precio-(precio/(1+(iva/100)))) from tblnotasdecreditodetallesc where idnota=v.idnota),0),ifnull((select sum(precio-(precio/(1+(iva/100)))) from tblnotasdecreditodetallesc where idnota=v.idnota),0)*v.tipodecambio) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.idconcepto,cnv.nombre formadepago " + _
            "from tblnotasdecreditocompras v inner join tblproveedores c on v.idproveedor=c.idproveedor inner join tblconceptosnotascompras cnv on v.idconcepto=cnv.idconceptonotacompra inner join tblsucursales s on v.idsucursal=s.idsucursal where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idnota idventa,v.folio,v.estado,ifnull((select sum(precio) from tblnotasdecreditodetallesc where idnota=v.idnota),0) as total,ifnull((select sum(precio-(precio/(1+(iva/100)))) from tblnotasdecreditodetallesc where idnota=v.idnota),0) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.idconcepto,cnv.nombre formadepago " + _
            "from tblnotasdecreditocompras v inner join tblproveedores c on v.idproveedor=c.idproveedor inner join tblconceptosnotascompras cnv on v.idconcepto=cnv.idconceptonotacompra inner join tblsucursales s on v.idsucursal=s.idsucursal where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdProveedor > 0 Then Comm.CommandText += " and v.idproveedor=" + pIdProveedor.ToString
        If pIdTipoProv > 0 Then Comm.CommandText += " and c.idtipo=" + pIdTipoProv.ToString
        If pidMoneda > 0 Then Comm.CommandText += " and v.idmoneda=" + pidMoneda.ToString
        If pIdConcepto > 0 Then Comm.CommandText += " and v.idconcepto=" + pIdConcepto.ToString
        Comm.CommandText += " order by v.fecha,v.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventas.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Function ReporteNotasDeCargoPorConcepto(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pidConcepto As Integer, pIdTipoSucursal As Integer, pIdTipoProv As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select v.idcargo idventa,v.folio,v.estado,if(v.idmoneda=2,v.total,v.total*v.tipodecambio) as total,if(v.idmoneda=2,v.totalapagar,v.totalapagar*v.tipodecambio) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.idconcepto,cnv.nombre formadepago " + _
            "from tblnotasdecargocompras v inner join tblproveedores c on v.idproveedor=c.idproveedor inner join tblconceptosnotascompras cnv on v.idconcepto=cnv.idconceptonotacompra inner join tblsucursales s on v.idsucursal=s.idsucursal where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select v.idcargo idventa,v.folio,v.estado,v.total,v.totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre,v.idconcepto,cnv.nombre formadepago " + _
            "from tblnotasdecargocompras v inner join tblproveedores c on v.idproveedor=c.idproveedor inner join tblconceptosnotascompras cnv on v.idconcepto=cnv.idconceptonotacompra inner join tblsucursales s on v.idsucursal=s.idsucursal where v.estado=3 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdProveedor > 0 Then Comm.CommandText += " and v.idproveedor=" + pIdProveedor.ToString
        If pidMoneda > 0 Then Comm.CommandText += " and v.idmoneda=" + pidMoneda.ToString
        If pidConcepto > 0 Then Comm.CommandText += " and v.idconcepto=" + pidConcepto.ToString
        If pIdTipoProv > 0 Then Comm.CommandText += " and c.idtipo=" + pIdTipoProv.ToString
        Comm.CommandText += " order by v.fecha,v.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventas")
        'DS.WriteXmlSchema("tblventas.xml")
        Return DS.Tables("tblventas").DefaultView
    End Function
    Public Sub ReCalculaCostos(ByVal pIdCompra As Integer, ByVal pTipoCosteo As Byte, ByVal TiempoReal As Byte, ByVal pTipodeCambio As Double)
        If TiempoReal = 1 Then
            Comm.CommandTimeout = 10000
            Comm.CommandText = "select sprecalculacostos(tblcompras.fecha,tblcomprasdetalles.idinventario," + pTipoCosteo.ToString + ") from tblcompras inner join tblcomprasdetalles on tblcompras.idcompra=tblcomprasdetalles.idcompra where tblcomprasdetalles.idcompra=" + pIdCompra.ToString
            Comm.ExecuteNonQuery()
            Comm.CommandText = "update tblinventario inner join tblcomprasdetalles on tblinventario.idinventario=tblcomprasdetalles.idinventario set tblinventario.costobase=ifnull(spsacacostoarticulo(tblinventario.idinventario,1,tblinventario.contenido," + pTipodeCambio.ToString + "," + pTipoCosteo.ToString + "),0) where tblcomprasdetalles.idcompra=" + pIdCompra.ToString + " and tblcomprasdetalles.idinventario>1"
            Comm.ExecuteNonQuery()
        End If
    End Sub

    Public Function RevisaConceptos(ByVal pIdcompra As Integer) As Boolean
        Dim C1 As Integer
        Dim C2 As Integer
        Comm.CommandText = "select count(idmoneda) from tblcomprasdetalles where idcompra=" + pIdcompra.ToString + " and idmoneda=2"
        C1 = Comm.ExecuteScalar
        Comm.CommandText = "select count(idmoneda) from tblcomprasdetalles where idcompra=" + pIdcompra.ToString + " and idmoneda<>2"
        C2 = Comm.ExecuteScalar
        If C1 <> 0 And C2 <> 0 Then
            Return False
        End If
        Return True
    End Function
    Public Sub ActualizaComentario(ByVal pidcompra As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblcompras set comentario='" + Replace(pTexto, "'", "''") + "' where idcompra=" + pidcompra.ToString
        Comm.ExecuteNonQuery()
    End Sub

    Public Function VienedeRemision(ByVal pIdVenta As Integer) As Integer
        Comm.CommandText = "select ifnull((select count(idremision) from tblcomprasremisiones where idcomprar=" + pIdVenta.ToString + "),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Sub DesligaRemisiones(ByVal pIdVenta As Integer)
        Comm.CommandText = "update tblcomprasremisiones set idcomprar=0,usado=0,idpedido=(select idpedido from tblcompras where idcompra=" + pIdVenta.ToString + ") where idcomprar=" + pIdVenta.ToString
        Comm.ExecuteNonQuery()
        Comm.CommandText = "update tblcompras set deremision=0,idpedido=0 where idcompra=" + pIdVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Usar(ByVal pidCompra As Integer)
        Comm.CommandText = "update tblcompras set deremision=1 where idcompra=" + pidCompra.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ReporteGrafica(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdProveedor As Integer, ByVal pidMoneda As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select tblcompras.idcompra,tblcompras.referencia,tblcompras.estado,if(tblcompras.idmoneda=2,tblcompras.total,tblcompras.total*tblcompras.tipodecambio) as total,if(tblcompras.idmoneda=2,tblcompras.totalapagar,tblcompras.totalapagar*tblcompras.tipodecambio) as totalapagar,tblcompras.fecha,tblcompras.tipodecambio,tblcompras.idmoneda,tblcomprasdetalles.cantidad,tblinventario.nombre as descripcion,tblcomprasdetalles.precio,tblcomprasdetalles.idinventario,if(tblformasdepago.tipo=0,'Crédito','Contado') as formadepago,tblproveedores.nombre as cnombre,tblcompras.costoindirecto as cindirecto,tblcompras.serie,tblcompras.folioi, " + _
            "ifnull(((select sum(if(tblcomprasdetalles.idmoneda=2,precio,precio*tblcompras.tipodecambio)) from tblcomprasdetalles where tblcomprasdetalles.idcompra=tblcompras.idcompra)/(select sum(cantidad) from tblcomprasdetalles where tblcomprasdetalles.idcompra=tblcompras.idcompra)),0) as costoprom,if(tblcomprasdetalles.idmoneda=2,tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100,tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100*tblcompras.tipodecambio) ivaretenido,if(tblcomprasdetalles.idmoneda=2,tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100,tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100*tblcompras.tipodecambio) ieps,if(tblcomprasdetalles.idmoneda=2,tblcomprasdetalles.precio*tblcomprasdetalles.iva/100,tblcomprasdetalles.precio*tblcomprasdetalles.iva/100*tblcompras.tipodecambio) iva " + _
            "from tblcompras inner join tblcomprasdetalles on tblcompras.idcompra=tblcomprasdetalles.idcompra inner join tblinventario on tblcomprasdetalles.idinventario=tblinventario.idinventario inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma where tblcompras.fecha>='" + pFecha1 + "' and tblcompras.fecha<='" + pFecha2 + "'"
        Else
            Comm.CommandText = "select tblcompras.idcompra,tblcompras.referencia,tblcompras.estado,tblcompras.total as total,tblcompras.totalapagar as totalapagar,tblcompras.fecha,tblcompras.tipodecambio,tblcompras.idmoneda,tblcomprasdetalles.cantidad,tblinventario.nombre as descripcion,tblcomprasdetalles.precio,tblcomprasdetalles.idinventario,if(tblformasdepago.tipo=0,'Crédito','Contado') as formadepago,tblproveedores.nombre as cnombre,tblcompras.costoindirecto as cindirecto,tblcompras.serie,tblcompras.folioi," + _
            "ifnull(((select sum(precio) from tblcomprasdetalles where tblcomprasdetalles.idcompra=tblcompras.idcompra)/(select sum(cantidad) from tblcomprasdetalles where tblcomprasdetalles.idcompra=tblcompras.idcompra)),0) as costoprom,,tblcomprasdetalles.precio*tblcomprasdetalles.ivaretenido/100 ivaretenido,tblcomprasdetalles.precio*tblcomprasdetalles.ieps/100 ieps,tblcomprasdetalles.precio*tblcomprasdetalles.iva/100 iva " + _
            "from tblcompras inner join tblcomprasdetalles on tblcompras.idcompra=tblcomprasdetalles.idcompra inner join tblinventario on tblcomprasdetalles.idinventario=tblinventario.idinventario inner join tblproveedores on tblcompras.idproveedor=tblproveedores.idproveedor inner join tblformasdepago on tblformasdepago.idforma=tblcompras.idforma where tblcompras.fecha>='" + pFecha1 + "' and tblcompras.fecha<='" + pFecha2 + "'"
        End If
        'Comm.CommandText = "select tblventas.idventa,tblventas.folio,tblventas.serie,tblventas.estado,tblventas.total,tblventas.totalapagar,tblventas.fecha,tblventas.tipodecambio,tblventas.idconversion,tblventasinventario.cantidad,tblventasinventario.descripcion,tblventasinventario.precio,if(tblventasinventario.idinventario>1,spsacacostoarticulo(tblventasinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tblventasinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tblventasinventario.idinventario,tblventasinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tblventasinventario.idvariante inner join tblclientes on tblventas.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tblventas.idforma where tblventas.estado=3 and tblventas.fecha>='" + pFecha1 + "' and tblventas.fecha<='" + pFecha2 + "'"
        If pSoloCanceladas Then
            Comm.CommandText += " and tblcompras.estado=4"
        Else
            Comm.CommandText += " and tblcompras.estado=3"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblcompras.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdProveedor > 0 Then
            Comm.CommandText += " and tblcompras.idproveedor=" + pIdProveedor.ToString
        End If
        If pidMoneda > 0 Then
            Comm.CommandText += " and tblcompras.idmoneda=" + pidMoneda.ToString
        End If
        'If pidInventario > 1 Then
        '    Comm.CommandText += " and tblventasinventario.idinventario=" + pidInventario.ToString
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
        Comm.CommandText += " Group by tblcompras.folioi order by tblcompras.fecha,tblcompras.referencia"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcomprasrep")
        'DS.WriteXmlSchema("tblcomprasrep.xml")
        Return DS.Tables("tblcomprasrep").DefaultView
    End Function
    Public Function ConsultaComprasconOrdenCompra(pIdPedido As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select c.idcompra, c.fecha, concat(c.serie, convert(c.folioi using utf8), ' - ', c.referencia), p.clave, p.nombre as Proveedor, c.totalapagar as Importe, case c.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as Estado from tblcompras as c inner join tblproveedores as p on c.idproveedor=p.idproveedor where c.idpedido=" + pIdPedido.ToString + " and c.idpedido<>0 order by c.fecha"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcompras")
        Return DS.Tables("tblcompras").DefaultView
    End Function
End Class
