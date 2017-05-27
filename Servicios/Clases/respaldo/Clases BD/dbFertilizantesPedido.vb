Public Class dbFertilizantesPedido
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdCliente As Integer
    Public Fecha As String
    Public Cliente As dbClientes
    Public Folio As Integer
    Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Subtotal As Double
    Public TotalIva As Double
    Public TotalVenta As Double
    Public Hora As String
    Public Estado As Byte
    Public Desglosar As Byte
    Public IdSucursal As Integer
    Public Serie As String
    Public IdCaja As Integer
    Public Usado As Byte
    Public IdVendedor As Integer
    Public Comentario As String
    Public TotalPeso As Double
    Public pIEPS As Double
    Public pIVARetenido As Double
    Public TotalIvaRetenidoConceptos As Double
    Public TotalIeps As Double
    Public Cultivo As String
    Public TipodeAplicacion As String
    'Public NoHectareas As Double
    'Public Cantidadxhora As Double
    Public FechaInicio As String
    Public FechaFin As String
    Public DiasAplicacion As Double
    Public DiaEntrega As String
    Public EquipoAplicacion As String
    Public TipodeCambio As Double
    Public IdForma As Integer
    Public EstadoPedido As Byte
    Public IdVenta As Integer
    Public FacturadoEn As String
    Public ClienteInvAFavor As Double
    Public TotalSurtido As Double
    Public TotalRestante As Double
    Public Enum TiposFactura As Byte
        Enproceso = 0
        Facturado = 1
        Cancelado = 2
    End Enum
    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdCliente = -1
        Fecha = ""
        Hora = ""
        Serie = ""
        Folio = 0
        Desglosar = 0
        Iva = 0
        TotalaPagar = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        IdCaja = 1
        Usado = 0
        EstadoPedido = 0
        IdVendedor = 0
        Comentario = ""
        Comm.Connection = Conexion
        Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Public Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tblfertilizantespedidos where idpedido=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdCliente = DReader("idcliente")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            'Desglosar = DReader("desglosar")
            Iva = DReader("iva")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            Serie = DReader("serie")
            'IdCaja = DReader("idcaja")
            'Usado = DReader("usado")
            IdVendedor = DReader("idvendedor")
            Comentario = DReader("comentario")
            Cultivo = DReader("cultivo")
            TipodeAplicacion = DReader("tipoaplicacion")
            FechaInicio = DReader("fechainicio")
            FechaFin = DReader("fechafin")
            DiasAplicacion = DReader("diasaplicacion")
            DiaEntrega = DReader("diaentrega")
            EquipoAplicacion = DReader("equipoaplicacion")
            TipodeCambio = DReader("tipodecambio")
            EstadoPedido = DReader("estadopedido")
            IdVenta = DReader("idventa")
        End If
        DReader.Close()
        Cliente = New dbClientes(IdCliente, Comm.Connection)
        If IdVenta <> 0 Then
            Comm.CommandText = "select concat(serie,convert(folio using utf8)) from tblventas where idventa=" + IdVenta.ToString
            FacturadoEn = "Facturado en: " + Comm.ExecuteScalar
        End If
    End Sub
    'Public Function ExisteFolio(ByVal pfolio As Integer, Optional ByVal idventa As Integer = -1) As Boolean
    '    Folio = pfolio
    '    Comm.CommandText = "select count(folio) from tblventas where folio=" + Folio.ToString + If(idventa = -1, "", " and idventa<>" + CStr(idventa))
    '    If Comm.ExecuteScalar = 0 Then Return False Else Return True
    'End Function
    Public Function DaId(SerieFolio As String) As Integer
        Comm.CommandText = "select idpedido from tblfertilizantespedidos where concat(serie,convert(folio using utf8))='" + SerieFolio + "' limit 1"
        Return Comm.ExecuteScalar
    End Function
    Public Sub Guardar(ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pIva As Double, ByVal pidSucursal As Integer, ByVal pSerie As String, ByVal pIdVendedor As Integer, ByVal pCultivo As String, ByVal pTipoAplicacion As String, ByVal pFechaInicio As String, ByVal pFechaFin As String, ByVal pDiasAplicacion As Double, ByVal pDiaEntrega As String, ByVal pEquipoAplicacion As String, pTipodeCambio As Double, pIdForma As Integer)
        IdCliente = pIdCliente
        IdVendedor = pIdVendedor
        Fecha = pFecha
        Folio = pFolio
        Iva = pIva
        IdSucursal = pidSucursal
        Serie = pSerie
        'Cultivo = DReader("cultivo")s
        'TipodeAplicacion = DReader("tipoaplicacion")s
        'FechaInicio = DReader("fechainicio")s
        'FechaFin = DReader("fechafin")s
        'DiasAplicacion = DReader("diasaplicacion")d
        'DiaEntrega = DReader("diasentrega")s
        'EquipoAplicacion = DReader("equipoaplicacion")s
        Comm.CommandText = "insert into tblfertilizantespedidos(idcliente,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,serie,idvendedor,comentario" + _
        ",cultivo,tipoaplicacion,fechainicio,fechafin,diasaplicacion,diaentrega,equipoaplicacion,tipodecambio,idforma,estadopedido,idventa) values(" + IdCliente.ToString + ",'" + Fecha + "'," + Folio.ToString + ",0,'" + Format(TimeOfDay, "HH:mm:ss") + "'," + CStr(Estados.SinGuardar) + "," + Iva.ToString + ",0," + IdSucursal.ToString + ",'" + Replace(Trim(Serie), "'", "''") + "'," + IdVendedor.ToString + ",''," + _
        "'" + Replace(pCultivo.Trim, "'", "''") + "','" + Replace(pTipoAplicacion.Trim, "'", "''") + "','" + pFechaInicio + "','" + pFechaFin + "'," + pDiasAplicacion.ToString + ",'" + pDiaEntrega + "','" + Replace(pEquipoAplicacion.Trim, "'", "''") + "'," + pTipodeCambio.ToString + "," + pIdForma.ToString + ",0,0)"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idpedido) from tblfertilizantespedidos"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pIva As Double, ByVal pEstado As Byte, ByVal pTotal As Double, ByVal pTotalaPagar As Double, ByVal pIdCliente As Integer, ByVal pSerie As String, ByVal pIdVendedor As Integer, ByVal pComentario As String, ByVal pCultivo As String, ByVal pTipoAplicacion As String, ByVal pFechaInicio As String, ByVal pFechaFin As String, ByVal pDiasAplicacion As Double, ByVal pDiaEntrega As String, ByVal pEquipoAplicacion As String, pTipodeCambio As Double, pIdForma As Integer, pEstadoPedido As Byte)
        ID = pID
        Fecha = pFecha
        Folio = pFolio
        Iva = pIva
        Estado = pEstado
        Total = pTotal
        TotalaPagar = pTotalaPagar
        IdCliente = pIdCliente
        Serie = pSerie
        IdVendedor = pIdVendedor
        Comentario = pComentario
        EstadoPedido = pEstadoPedido
        '(idcliente,fecha,folio,total,hora,estado,iva,totalapagar,desglozar)
        Estado = pEstado
        Comm.CommandText = "update tblfertilizantespedidos set fecha='" + Fecha + "',folio=" + Folio.ToString + ",iva=" + Iva.ToString + ",estado=" + Estado.ToString + ",total=" + Total.ToString + ",totalapagar=" + TotalaPagar.ToString + ",idcliente=" + IdCliente.ToString + ",serie='" + Replace(Trim(Serie), "'", "''") + "',idvendedor=" + IdVendedor.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "'" + _
        ",cultivo='" + Replace(pCultivo.Trim, "'", "''") + "',tipoaplicacion='" + Replace(pTipoAplicacion.Trim, "'", "''") + "',fechainicio='" + pFechaInicio + "',fechafin='" + pFechaFin + "',diasaplicacion=" + pDiasAplicacion.ToString + ",diaentrega='" + pDiaEntrega + "',equipoaplicacion='" + Replace(pEquipoAplicacion.Trim, "'", "''") + "',tipodecambio=" + pTipodeCambio.ToString + ",idforma=" + pIdForma.ToString + ",estadopedido=" + EstadoPedido.ToString + _
        " where idpedido=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ModificaTotales(pId As Integer, pidMoneda As Integer)
        DaTotal(pId, pidMoneda)
        Comm.CommandText = "update tblfertilizantespedidos set total=" + Total.ToString + ",totalapagar=" + TotalaPagar.ToString + " where idpedido=" + pId.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaComentario(ByVal pidpedido As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblfertilizantespedidos set comentario='" + Replace(pTexto, "'", "''") + "' where idpedido=" + pidpedido.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblfertilizantespedidos where idpedido=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pNombreClave As String, ByVal pEstado As Byte, ByVal pFolio As String, ByVal pIdSucursal As Integer, pEstadoPedido As Byte, pSinFacturar As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventaspedidos.idpedido,tblventaspedidos.fecha,concat(tblventaspedidos.serie,convert(tblventaspedidos.folio using utf8)),tblclientes.clave,tblclientes.nombre as Cliente,round(tblventaspedidos.totalapagar) as totalapagar,case tblventaspedidos.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado,case tblventaspedidos.estadopedido when 0 then 'Abierto' when 1 then 'Cerrado' end as spestado from tblfertilizantespedidos as tblventaspedidos inner join tblclientes on tblventaspedidos.idcliente=tblclientes.idcliente where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblclientes.clave,tblclientes.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pEstado <> Estados.Inicio Then
            Comm.CommandText += " and tblventaspedidos.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblventaspedidos.estado<>1"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tblventaspedidos.idsucursal=" + pIdSucursal.ToString
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tblventaspedidos.serie,convert(tblventaspedidos.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstadoPedido > 0 Then
            Comm.CommandText += " and estadopedido=" + (pEstadoPedido - 1).ToString
        End If
        If pSinFacturar Then
            Comm.CommandText += " and idventa=0"
        End If
        Comm.CommandText += " order by tblventaspedidos.fecha,tblventaspedidos.serie,tblventaspedidos.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspedidos")
        Return DS.Tables("tblventaspedidos").DefaultView
    End Function

    
    Public Function DaTotal(ByVal pidCotizacion As Integer, ByVal pidMoneda As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0
        'Dim Encontro As Double
        Dim iIva As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double = 1
        'Dim iIsr As Double = 0
        'Dim iIvaRetenido As Double = 0
        Subtotal = 0
        TotalIva = 0
        TotalVenta = 0
        TotalIeps = 0
        TotalIvaRetenidoConceptos = 0
        'Comm.CommandText = "select tipodecambio from tblventas where idventa=" + pidVenta.ToString
        'iTipoCambio = Comm.ExecuteScalar
        'Comm.CommandText = "select isr from tblventas where idventa=" + pidVenta.ToString
        'iIsr = Comm.ExecuteScalar
        'Comm.CommandText = "select ivaretenido from tblventas where idventa=" + pidVenta.ToString
        'iIvaRetenido = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tblfertilizantespedidosdetalles where idpedido=" + pidCotizacion.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblfertilizantespedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select iva from tblfertilizantespedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblfertilizantespedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            Comm.CommandText = "select IEPS from tblfertilizantespedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            pIEPS = Comm.ExecuteScalar
            Comm.CommandText = "select IVARetenido from tblfertilizantespedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
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
            Subtotal += Precio
            TotalIva += (Precio * (iIva / 100))
            TotalIeps += (Precio * (pIEPS / 100))
            TotalIvaRetenidoConceptos += (Precio * (pIVARetenido / 100))
            Cont += 1
        End While
        Comm.CommandText = "select ifnull((select sum(tblinventario.peso*tblfertilizantespedidosdetalles.cantidad) from tblfertilizantespedidosdetalles inner join tblinventario on tblfertilizantespedidosdetalles.idinventario=tblinventario.idinventario where tblfertilizantespedidosdetalles.idpedido=" + pidCotizacion.ToString + " and tblfertilizantespedidosdetalles.afavor=0),0)"
        TotalPeso = Comm.ExecuteScalar
        'TotalISR = Subtotal * (iIsr / 100)
        'TotalIvaRetenido = Subtotal * (iIvaRetenido / 100)
        TotalVenta = Subtotal + TotalIva + TotalIeps - TotalIvaRetenidoConceptos '- TotalISR - TotalIvaRetenido
        Return TotalVenta
    End Function
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblfertilizantespedidos where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4)),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblfertilizantespedidos where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado<>1 and estado<>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function

    Public Function DaIvas(ByVal pIdPedido As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iva,precio,idmoneda from tblfertilizantespedidosdetalles where idpedido=" + pIdPedido.ToString
        Return Comm.ExecuteReader
    End Function


    Public Sub AgregarDetallesReferencia(ByVal PidPedido As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte)
        '0 cotizacion
        '1 pedido
        '2 remision
        '3 ventas

        If Tipo = 0 Then
            Comm.CommandText = "insert into tblventaspedidosinventario(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,surtido,preciooriginal,IEPS,ivaRetenido) select " + PidPedido.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,0,precio,IEPS,ivaRetenido from tblventascotizacionesinventario where idcotizacion=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventaspedidosproductos(idpedido,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventascotizacionesproductos where idcotizacion=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 1 Then
            Comm.CommandText = "insert into tblventaspedidosinventario(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,surtido,preciooriginal,IEPS,ivaRetenido) select " + PidPedido.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,0,precio,IEPS,ivaRetenido from tblventaspedidosinventario where idpedido=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventaspedidosproductos(idpedido,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventaspedidosproductos where idpedido=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 2 Then
            Comm.CommandText = "insert into tblventaspedidosinventario(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,surtido,preciooriginal,IEPS,ivaRetenido) select " + PidPedido.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,0,precio,IEPS,ivaRetenido from tblventasremisionesinventario where idremision=" + pIdDocumento.ToString + " and idservicio=0"
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventaspedidosproductos(idpedido,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasremisionesproductos where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventaspedidosservicios(idventa,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasremisionesservicios where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblinventarioseries set idventa=" + PidPedido.ToString + " where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 3 Then
            Comm.CommandText = "insert into tblventaspedidosinventario(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,surtido,preciooriginal,IEPS,ivaRetenido) select " + PidPedido.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,0,precio,IEPS,ivaRetenido from tblventasinventario where idventa=" + pIdDocumento.ToString + " and idservicio=0"
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventaspedidosproductos(idpedido,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasproductos where idventa=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventasservicios(idventa,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasservicios where idventa=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If
    End Sub
    'Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, ByVal pIdVendedor As Integer, ByVal pSerie As String) As DataView
    '    Dim DS As New DataSet
    '    'If pMostrarEnPesos = 0 Then
    '    'Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idmoneda=2,v.total,v.total*v.tipodecambio) as total,if(v.idmoneda=2,v.totalapagar,v.totalapagar*v.tipodecambio) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre " + _
    '    '"from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente where v.usado=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
    '    'Else
    '    Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,v.total as total,v.totalapagar as totalapagar,v.fecha,0 as tipodecambio,2 as idmoneda,c.nombre as cnombre,v.usado " + _
    '    "from tblfertilizantespedidos v inner join tblclientes c on v.idcliente=c.idcliente where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
    '    'End If

    '    If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
    '    If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
    '    If pIdCliente > 0 Then Comm.CommandText += " and v.idvendedor=" + pIdVendedor.ToString
    '    If pSoloCanceladas Then
    '        Comm.CommandText += " and v.estado=4"
    '    Else
    '        Comm.CommandText += " and v.estado=3"
    '    End If
    '    If pSerie <> "" Then
    '        Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
    '    End If
    '    Comm.CommandText += " order by v.fecha,v.serie,v.folio"
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblfertilizantespedidos")
    '    'DS.WriteXmlSchema("tblremisiones.xml")
    '    Return DS.Tables("tblfertilizantespedidos").DefaultView
    'End Function
    'Public Function BuscaPedido(ByVal pFolio As String) As Integer
    '    Comm.CommandText = "select ifnull((select idpedido from tblventaspedidos where concat(tblventaspedidos.serie,convert(tblventaspedidos.folio using utf8))='" + Replace(pFolio, "'", "''") + "' and estado>2 limit 1),0)"
    '    Return Comm.ExecuteScalar
    'End Function
    'Public Function DaId(ByVal pFolio As String) As Integer
    '    Comm.CommandText = "select ifnull((select idpedido from tblventaspedidos where tblventaspedidos.estado=3 and usado=0 and concat(tblventaspedidos.serie,convert(tblventaspedidos.folio using utf8))='" + Replace(pFolio, "'", "''") + "'),0)"
    '    Return Comm.ExecuteScalar
    'End Function
    'Public Sub Usar(ByVal pid As Integer)
    '    Comm.CommandText = "update tblventaspedidos set usado=1 where idpedido=" + pid.ToString
    '    Comm.ExecuteNonQuery()
    'End Sub
    Public Function DaTotalCantidad(ByVal pIdVenta As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(tblfertilizantespedidosdetalles.cantidad) from tblfertilizantespedidosdetalles inner join tblinventario on tblfertilizantespedidosdetalles.idinventario=tblinventario.idinventario where idpedido=" + pIdVenta.ToString + " and inventariable=1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function RevisaConceptos(ByVal pIdPedido As Integer) As Boolean
        Dim C1 As Integer
        Dim C2 As Integer
        Comm.CommandText = "select count(idmoneda) from tblfertilizantespedidosdetalles where idpedido=" + pIdPedido.ToString + " and idmoneda=2"
        C1 = Comm.ExecuteScalar
        Comm.CommandText = "select count(idmoneda) from tblfertilizantespedidosdetalles where idpedido=" + pIdPedido.ToString + " and idmoneda<>2"
        C2 = Comm.ExecuteScalar
        If C1 <> 0 And C2 <> 0 Then
            Return False
        End If
        Return True
    End Function
    Public Function DaMoneda(ByVal pIdPedido As Integer) As Integer
        Comm.CommandText = "select ifnull((select idmoneda from tblfertilizantespedidosdetalles where idpedido=" + pIdPedido.ToString + " limit 1),2)"
        Return Comm.ExecuteScalar
    End Function
    Public Function RevisaMovimientos(pIdPedido As Integer) As Boolean
        Comm.CommandText = "select count(idmovimiento) from tblfertilizantesmovimientos where estado=3 and idpedido=" + pIdPedido.ToString
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function RevisaMovimientosEntransito(pIdPedido As Integer) As Boolean
        Comm.CommandText = "select count(idmovimiento) from tblfertilizantesmovimientos where estado=3 and estadosurtido=0 and idpedido=" + pIdPedido.ToString
        If Comm.ExecuteScalar = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function ChecaTotalSurtido(pIdPedido As Integer, pIdinventario As Integer) As Double
        Dim iSurtido As Double
        Dim iTotalPedido As Double
        Comm.CommandText = "select ifnull((select sum(surtido) from tblfertilizantesmovimientos where idinventario=" + pIdinventario.ToString + " and idpedido=" + pIdPedido.ToString + " and estado=3 and tipo<>3),0)"
        iSurtido = Comm.ExecuteScalar
        TotalSurtido = iSurtido
        Comm.CommandText = "select ifnull((select sum(cantidad) from tblfertilizantespedidosdetalles where idinventario=" + pIdinventario.ToString + " and idpedido=" + pIdPedido.ToString + " and afavor=0),0)"
        iTotalPedido = Comm.ExecuteScalar
        Return iTotalPedido - iSurtido
    End Function
    Public Function ChecaTotalSurtidoGlobal(pIdPedido As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(surtido) from tblfertilizantesmovimientos where idpedido=" + pIdPedido.ToString + " and estado=3 and tipo<>3),0)"
        TotalSurtido = Comm.ExecuteScalar
        Return TotalSurtido
    End Function
    Public Sub DaInventarioaCliente(pIdcliente As Integer, pIdinventario As Integer, pCantidad As Double)
        Dim Hay As Integer
        Comm.CommandText = "select ifnull((select idcliente from tblclientesinventario where idinventario=" + pIdinventario.ToString + " and idcliente=" + pIdcliente.ToString + "),0)"
        Hay = Comm.ExecuteScalar
        If Hay = 0 Then
            Comm.CommandText = "insert into tblclientesinventario(idcliente,idinventario,cantidad) values(" + pIdcliente.ToString + "," + pIdinventario.ToString + "," + pCantidad.ToString + ")"
        Else
            Comm.CommandText = "update tblclientesinventario set cantidad=cantidad+" + pCantidad.ToString + " where idcliente=" + pIdcliente.ToString + " and idinventario=" + pIdinventario.ToString
        End If
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ChecaInventarioAFavor(pIdCliente As Integer, pIdIventario As Integer) As Double
        Comm.CommandText = "select ifnull((select cantidad from tblclientesinventario where idcliente=" + pIdCliente.ToString + " and idinventario=" + pIdIventario.ToString + "),0)"
        ClienteInvAFavor = Comm.ExecuteScalar
        Return ClienteInvAFavor
    End Function
    Public Sub QuitaInventarioaFavor(pIdcliente As Integer, pIdInventario As Integer, pCantidad As Double)
        Comm.CommandText = "update tblclientesinventario set cantidad=cantidad-" + pCantidad.ToString + " where idcliente=" + pIdcliente.ToString + " and idinventario=" + pIdInventario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub AjustaIventarioAFavorCliente(pIdCliente As Integer, pIdIventario As Integer)
        Comm.CommandText = "update tblclientesinventario set cantidad=ifnull((select sum(cantidad) from tblfertilizantespedidosdetalles inner join tblfertilizantespedidos on tblfertilizantespedidosdetalles.idpedido=tblfertilizantespedidos.idpedido where tblfertilizantespedidos.idcliente=tblclientesinventario.idcliente and tblfertilizantespedidosdetalles.afavor=0 and tblfertilizantespedidos.estado=3),0)-ifnull((select sum(surtido) from tblfertilizantesmovimientos inner join tblfertilizantespedidos on tblfertilizantesmovimientos.idpedido=tblfertilizantespedidos.idpedido where tblfertilizantespedidos.idcliente=tblclientesinventario.idcliente and tblfertilizantesmovimientos.idinventario=tblclientesinventario.idinventario and tblfertilizantesmovimientos.estadosurtido=1 and tblfertilizantesmovimientos.estado=3 and tblfertilizantespedidos.estado=3)-(select sum(cantidad) from tblfertilizantespedidosdetalles inner join tblfertilizantespedidos on tblfertilizantespedidosdetalles.idpedido=tblfertilizantespedidos.idpedido where tblfertilizantespedidos.idcliente=tblclientesinventario.idcliente and tblfertilizantespedidosdetalles.afavor=1 and tblfertilizantespedidos.estado=3),0) where idcliente=" + pIdCliente.ToString + " and idinventario=" + pIdIventario.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub AgregaEquipo(pidEquipo As Integer, pIdPedido As Integer)
        Comm.CommandText = "insert into tblfertilizantesequipos(idequipo,idpedido) values(" + pidEquipo.ToString + "," + pIdPedido.ToString + ") "
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub EliminaEquipo(pIdEquipop As Integer)
        Comm.CommandText = "delete from tblfertilizantesequipos where idequipop=" + pIdEquipop.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function ConsultaEquipos(pidpedido As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select idequipop,nombre from tblsucequipos inner join tblfertilizantesequipos on tblsucequipos.idequipo=tblfertilizantesequipos.idequipo where idpedido=" + pidpedido.ToString
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspedidose")
        Return DS.Tables("tblventaspedidose").DefaultView
    End Function
    Public Function ConsultaEquiposReader(ByVal pIdPedido As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select idequipop,nombre from tblsucequipos inner join tblfertilizantesequipos on tblsucequipos.idequipo=tblfertilizantesequipos.idequipo where idpedido=" + pIdPedido.ToString
        Return Comm.ExecuteReader
    End Function
    Public Function DaIdsIventario(ByVal pIdPedido As Integer, SoloAfavor As Boolean) As MySql.Data.MySqlClient.MySqlDataReader
        If SoloAfavor = False Then
            Comm.CommandText = "select distinct idinventario from tblfertilizantespedidosdetalles where idpedido=" + pIdPedido.ToString
        Else
            Comm.CommandText = "select idinventario,cantidad from tblfertilizantespedidosdetalles where idpedido=" + pIdPedido.ToString + " and afavor=1"
        End If
        Return Comm.ExecuteReader
    End Function
    Public Sub CierraPedido(pIdPedido As Integer)
        Comm.CommandText = "update tblfertilizantespedidos set estadopedido=1 where idpedido=" + pIdPedido.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pSoloCanceladas As Boolean, ByVal pIdVendedor As Integer, pEstadoPedido As Byte) As DataView
        Dim DS As New DataSet
        'If pMostrarEnPesos = 0 Then
        'Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idmoneda=2,v.total,v.total*v.tipodecambio) as total,if(v.idmoneda=2,v.totalapagar,v.totalapagar*v.tipodecambio) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre " + _
        '"from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente where v.usado=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        'Else
        Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,v.total as total,v.totalapagar as totalapagar,v.fecha,c.nombre as cnombre,v.cultivo,v.estadopedido,ifnull((select sum(cantidad) from tblfertilizantespedidosdetalles where tblfertilizantespedidosdetalles.idpedido=v.idpedido),0) as totalpeso " + _
        "from tblfertilizantespedidos v inner join tblclientes c on v.idcliente=c.idcliente where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        'End If

        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pIdVendedor > 0 Then Comm.CommandText += " and v.idvendedor=" + pIdVendedor.ToString
        If pSoloCanceladas Then
            Comm.CommandText += " and v.estado=4"
        Else
            Comm.CommandText += " and (v.estado=3 or v.estado=4)"
        End If
        If pEstadoPedido > 0 Then
            Comm.CommandText += " and v.estadopedido=" + (pEstadoPedido - 1).ToString
        End If
        Comm.CommandText += " order by v.fecha,v.serie,v.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblfert")
        'DS.WriteXmlSchema("repfertilizantes.xml")
        Return DS.Tables("tblfert").DefaultView
    End Function
    Public Function ReporteClientesInventario(ByVal pIdCliente As Integer) As DataView
        Dim DS As New DataSet
        'If pMostrarEnPesos = 0 Then
        'Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idmoneda=2,v.total,v.total*v.tipodecambio) as total,if(v.idmoneda=2,v.totalapagar,v.totalapagar*v.tipodecambio) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre " + _
        '"from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente where v.usado=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        'Else
        Comm.CommandText = "select tblclientes.clave,tblclientes.nombre,tblinventario.nombre nombrei,tblinventario.clave clavei,tblclientesinventario.cantidad from tblclientesinventario inner join tblclientes on tblclientes.idcliente=tblclientesinventario.idcliente inner join tblinventario on tblinventario.idinventario=tblclientesinventario.idinventario where tblclientesinventario.cantidad<>0"
        If pIdCliente <> 0 Then
            Comm.CommandText += " and tblclientes.idcliente=" + pIdCliente.ToString
        End If
        Comm.CommandText += " order by tblclientes.nombre"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblclientesinventario")
        'DS.WriteXmlSchema("tblclientesinventario.xml")
        Return DS.Tables("tblclientesinventario").DefaultView
    End Function
    Public Sub Usar(ByVal pID() As Integer, ByVal pIdventa As Integer)
        Dim Wherefields As String = ""
        For Each IDf As Integer In pID
            Wherefields += " or idpedido=" + IDf.ToString
        Next
        Comm.CommandText = "update tblfertilizantespedidos set idventa=" + pIdventa.ToString + " where false " + Wherefields
        Comm.ExecuteNonQuery()

    End Sub
End Class
