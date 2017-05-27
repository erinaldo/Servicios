Public Class dbComprasPedidos
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public Idproveedor As Integer
    Public Fecha As String
    Public Proveedor As dbproveedores
    Public Folio As Integer
    Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    Public Estado As Byte
    Public idMoneda As Integer
    Public IdSucursal As Integer
    Public Serie As String

    Public TotalISR As Double
    Public Subtotal As Double
    Public TotalIva As Double
    Public TotalIvaRetenido As Double
    Public TotalVenta As Double
    Public Comentario As String
    Public TotalIeps As Double
    Public TotalIvaRetenidoCon As Double

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        Idproveedor = -1
        Fecha = ""
        Hora = ""
        Folio = 0
        Serie = ""
        idMoneda = 0
        Iva = 0
        TotalaPagar = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        Comentario = ""
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
        Comm.CommandText = "select * from tblcompraspedidos where idpedido=" + ID.ToString
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
            Comentario = DReader("comentario")
        End If
        DReader.Close()
        Proveedor = New dbproveedores(Idproveedor, Comm.Connection)
    End Sub

    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tblcompraspedidos where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4)),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblcompraspedidos where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function
    Public Sub Guardar(ByVal pIdProveedor As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pidMoneda As Integer, ByVal pIva As Double, ByVal pidSucursal As Integer, ByVal pSerie As String)
        Idproveedor = pIdProveedor
        Fecha = pFecha
        Folio = pFolio
        idMoneda = pidMoneda
        Iva = pIva
        IdSucursal = pidSucursal
        Serie = pSerie
        Comm.CommandText = "insert into tblcompraspedidos(idproveedor,fecha,folio,total,hora,estado,iva,totalapagar,idmoneda,idsucursal,serie,comentario,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechacambio,horacambio) values(" + Idproveedor.ToString + ",'" + Fecha + "'," + Folio.ToString + ",0,'" + Format(TimeOfDay, "HH:mm:ss") + "'," + CStr(Estados.SinGuardar) + "," + Iva.ToString + ",0," + idMoneda.ToString + "," + IdSucursal.ToString + ",'" + Replace(Trim(Serie), "'", "''") + "',''," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:ss:mm") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idpedido) from tblcompraspedidos"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pidmoneda As Integer, ByVal pIva As Double, ByVal pEstado As Byte, ByVal pTotal As Double, ByVal pTotalaPagar As Double, ByVal pIdproveedor As Integer, ByVal pSerie As String, ByVal pComentario As String)
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
        Comentario = pComentario
        '(idcliente,fecha,folio,total,hora,estado,iva,totalapagar,desglozar)
        Estado = pEstado
        Comm.CommandText = "update tblcompraspedidos set fecha='" + Fecha + "',folio=" + Folio.ToString + ",idmoneda=" + idMoneda.ToString + ",iva=" + Iva.ToString + ",estado=" + Estado.ToString + ",total=" + Total.ToString + ",totalapagar=" + TotalaPagar.ToString + ",idproveedor=" + pIdproveedor.ToString + ",serie='" + Replace(Trim(Serie), "'", "''") + "',comentario='" + Replace(Comentario, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idpedido=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblcompraspedidos where idpedido=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pNombreClave As String, ByVal pEstado As Byte, ByVal pFolio As String, ByVal pIdSucursal As Integer, porSurtir As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblcompraspedidos.idpedido,tblcompraspedidos.fecha,concat(tblcompraspedidos.serie,convert(tblcompraspedidos.folio using utf8)) as folio,tblproveedores.clave,tblproveedores.nombre as Proveedor,tblcompraspedidos.total,case tblcompraspedidos.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as Estado  from tblcompraspedidos inner join tblproveedores on tblcompraspedidos.idproveedor=tblproveedores.idproveedor inner join tblcompraspedidosdetalles on tblcompraspedidos.idpedido=tblcompraspedidosdetalles.idpedido where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then Comm.CommandText += " and concat(tblproveedores.clave,tblproveedores.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        If pFolio <> "" Then Comm.CommandText += " and concat(tblcompraspedidos.serie,convert(tblcompraspedidos.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        If pEstado <> Estados.Inicio Then
            Comm.CommandText += " and tblcompraspedidos.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblcompraspedidos.estado<>1"
        End If
        If pIdSucursal > 0 Then Comm.CommandText += " and tblcompraspedidos.idsucursal=" + pIdSucursal.ToString
        If porSurtir Then Comm.CommandText += " and tblcompraspedidosdetalles.cantidad>surtido"

        Comm.CommandText += " group by tblcompraspedidos.idpedido order by tblcompraspedidos.fecha desc,tblcompraspedidos.serie,tblcompraspedidos.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblcompraspedidos")
        Return DS.Tables("tblcompraspedidos").DefaultView
    End Function
    Public Function DaIvas(ByVal pIdPedido As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iva,precio,idmoneda from tblcompraspedidosdetalles where idpedido=" + pIdPedido.ToString
        Return Comm.ExecuteReader
    End Function

    Public Function DaTotal(ByVal pidPedido As Integer, ByVal pidMoneda As Integer) As Double
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
        Dim pIVARetenido As Double
        Subtotal = 0
        TotalIva = 0
        TotalVenta = 0
        TotalIeps = 0
        TotalIvaRetenidoCon = 0
        'Comm.CommandText = "select tipodecambio from tblventas where idventa=" + pidVenta.ToString
        'iTipoCambio = Comm.ExecuteScalar
        'Comm.CommandText = "select isr from tblventas where idventa=" + pidVenta.ToString
        'iIsr = Comm.ExecuteScalar
        'Comm.CommandText = "select ivaretenido from tblventas where idventa=" + pidVenta.ToString
        'iIvaRetenido = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tblcompraspedidosdetalles where idpedido=" + pidPedido.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblcompraspedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select iva from tblcompraspedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblcompraspedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            Comm.CommandText = "select ieps from tblcompraspedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
            pIEPS = Comm.ExecuteScalar
            Comm.CommandText = "select IVARetenido from tblcompraspedidosdetalles where iddetalle=" + IDs.Item(Cont).ToString
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
            Subtotal += Precio
            Cont += 1
        End While

        TotalISR = Subtotal * (iIsr / 100)
        TotalIvaRetenido = Subtotal * (iIvaRetenido / 100)
        TotalVenta = Subtotal + TotalIva - TotalISR - TotalIvaRetenido - TotalIvaRetenidoCon + TotalIeps
        Return TotalVenta
    End Function


    'Public ReadOnly Property totalLetra(ByVal idmoneda As Integer) As String
    '    Get
    '        Dim f As New StringFunctions
    '        Return f.PASELETRAS(DaTotal(ID, idmoneda), idmoneda, GlobalIdiomaLetras)
    '    End Get
    'End Property


    Public Sub AgregarDetallesReferencia(ByVal PidPedido As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte)
        '0 cotizacion
        '1 pedido
        '2 remision
        '3 ventas

        If Tipo = 0 Then
            Comm.CommandText = "insert into tblcompraspedidosdetalles(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,surtido,IEPS,ivaRetenido) select " + PidPedido.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,0,IEPS,ivaRetenido from tblcomprascotizacionesdetalles where idcotizacion=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventascotizacionesproductos(idcotizacion,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventascotizacionesproductos where idcotizacion=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 1 Then
            Comm.CommandText = "insert into tblcompraspedidosdetalles(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,surtido,IEPS,ivaRetenido) select " + PidPedido.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,0,IEPS,ivaRetenido from tblcompraspedidosdetalles where idpedido=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventascotizacionesproductos(idcotizacion,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventaspedidosproductos where idpedido=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 2 Then
            Comm.CommandText = "insert into tblcompraspedidosdetalles(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,surtido,IEPS,ivaRetenido) select " + PidPedido.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,0,IEPS,ivaRetenido from tblcomprasremisionesdetalles where idremision=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventascotizacionesproductos(idcotizacion,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasremisionesproductos where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventasservicios(idventa,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasremisionesservicios where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblinventarioseries set idventa=" + PidCotizacion.ToString + " where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 3 Then
            Comm.CommandText = "insert into tblcompraspedidosdetalles(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,surtido,IEPS,ivaRetenido) select " + PidPedido.ToString + ",idinventario,cantidad,precio,(select nombre from tblinventario where tblinventario.idinventario=tblcomprasdetalles.idinventario),idmoneda,iva,extra,descuento,0,IEPS,ivaRetenido from tblcomprasdetalles where idcompra=" + pIdDocumento.ToString
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventascotizacionesproductos(idcotizacion,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasproductos where idventa=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tblventasservicios(idventa,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidCotizacion.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasservicios where idventa=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If
    End Sub
    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, pIdTipoSucursal As Integer, pIdTipoprov As Integer) As DataView
        Dim DS As New DataSet
        'If pMostrarEnPesos = 0 Then
        'Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idmoneda=2,v.total,v.total*v.tipodecambio) as total,if(v.idmoneda=2,v.totalapagar,v.totalapagar*v.tipodecambio) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre " + _
        '"from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente where v.usado=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        'Else
        Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,v.total as total,v.totalapagar as totalapagar,v.fecha,0 as tipodecambio,0 as idmoneda,c.nombre as cnombre,0 usado " + _
        "from tblcompraspedidos v inner join tblproveedores c on v.idproveedor=c.idproveedor inner join tblsucursales s on v.idsucursal=s.idsucursal where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        'End If

        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idproveedor=" + pIdCliente.ToString
        If pIdTipoprov > 0 Then Comm.CommandText += " and c.idtipo=" + pIdTipoprov.ToString
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
    Public Sub ActualizaComentario(ByVal pidPedido As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblcompraspedidos set comentario='" + Replace(pTexto, "'", "''") + "' where idpedido=" + pidPedido.ToString
        Comm.ExecuteNonQuery()
    End Sub
End Class
