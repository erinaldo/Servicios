﻿Public Class dbVentasPedidos
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
    Public Usuario As String
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
        Comm.CommandText = "select *,(select nombreusuario from tblusuarios where idusuario=tblventaspedidos.idusuariocambio) as usuario from tblventaspedidos where idpedido=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdCliente = DReader("idcliente")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            Desglosar = DReader("desglosar")
            Iva = DReader("iva")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            Serie = DReader("serie")
            IdCaja = DReader("idcaja")
            Usado = DReader("usado")
            IdVendedor = DReader("idvendedor")
            Comentario = DReader("comentario")
            Usuario = DReader("usuario")
        End If
        DReader.Close()
        Cliente = New dbClientes(IdCliente, Comm.Connection)
    End Sub
    'Public Function ExisteFolio(ByVal pfolio As Integer, Optional ByVal idventa As Integer = -1) As Boolean
    '    Folio = pfolio
    '    Comm.CommandText = "select count(folio) from tblventas where folio=" + Folio.ToString + If(idventa = -1, "", " and idventa<>" + CStr(idventa))
    '    If Comm.ExecuteScalar = 0 Then Return False Else Return True
    'End Function

    Public Sub Guardar(ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pidSucursal As Integer, ByVal pSerie As String, ByVal pidCaja As Integer, ByVal pIdVendedor As Integer)
        IdCliente = pIdCliente
        IdVendedor = pIdVendedor
        Fecha = pFecha
        Folio = pFolio
        Desglosar = pDesglosar
        Iva = pIva
        IdSucursal = pidSucursal
        Serie = pSerie
        IdCaja = pidCaja
        Comm.CommandText = "insert into tblventaspedidos(idcliente,fecha,folio,total,hora,estado,iva,totalapagar,desglosar,idsucursal,serie,idcaja,usado,idvendedor,comentario,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + IdCliente.ToString + ",'" + Fecha + "'," + Folio.ToString + ",0,'" + Format(TimeOfDay, "HH:mm:ss") + "'," + CStr(Estados.SinGuardar) + "," + Iva.ToString + ",0," + Desglosar.ToString + "," + IdSucursal.ToString + ",'" + Replace(Trim(Serie), "'", "''") + "'," + IdCaja.ToString + ",0," + IdVendedor.ToString + ",''," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "');"
        'Comm.ExecuteNonQuery()
        Comm.CommandText += "select ifnull(last_insert_id(),0);"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pEstado As Byte, ByVal pTotal As Double, ByVal pTotalaPagar As Double, ByVal pIdCliente As Integer, ByVal pSerie As String, ByVal pIdVendedor As Integer, ByVal pComentario As String)
        ID = pID
        Fecha = pFecha
        Folio = pFolio
        Desglosar = pDesglosar
        Iva = pIva
        Estado = pEstado
        Total = pTotal
        TotalaPagar = pTotalaPagar
        IdCliente = pIdCliente
        Serie = pSerie
        IdVendedor = pIdVendedor
        Comentario = pComentario
        '(idcliente,fecha,folio,total,hora,estado,iva,totalapagar,desglozar)
        Estado = pEstado
        Comm.CommandText = "update tblventaspedidos set fecha='" + Fecha + "',folio=" + Folio.ToString + ",desglosar=" + Desglosar.ToString + ",iva=" + Iva.ToString + ",estado=" + Estado.ToString + ",total=" + Total.ToString + ",totalapagar=" + TotalaPagar.ToString + ",idcliente=" + IdCliente.ToString + ",serie='" + Replace(Trim(Serie), "'", "''") + "',idvendedor=" + IdVendedor.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idpedido=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaComentario(ByVal pidnota As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblventaspedidos set comentario='" + Replace(pTexto, "'", "''") + "' where idpedido=" + pidnota.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblventaspedidos where idpedido=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pNombreClave As String, ByVal pEstado As Byte, ByVal pFolio As String, ByVal pIdSucursal As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblventaspedidos.idpedido,tblventaspedidos.fecha,concat(tblventaspedidos.serie,convert(tblventaspedidos.folio using utf8)) as foliop,tblclientes.clave,tblclientes.nombre as Cliente,round(tblventaspedidos.totalapagar) as totalapagar,case tblventaspedidos.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado from tblventaspedidos inner join tblclientes on tblventaspedidos.idcliente=tblclientes.idcliente where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
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
        Comm.CommandText += " order by tblventaspedidos.fecha desc,tblventaspedidos.serie,tblventaspedidos.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblventaspedidos")
        Return DS.Tables("tblventaspedidos").DefaultView
    End Function

    'Public Function ConsultaDeudas(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidCliente As Integer, Optional ByVal pFolio As String = "") As DataView
    '    Dim DS As New DataSet
    '    Comm.CommandText = "select tblventas.idventa,tblventas.fecha,tblventas.folio,tblventas.total,tblventas.totalapagar from tblventas inner join tblclientes on tblventas.idcliente=tblclientes.idcliente where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "' and tblventas.facturado=1 and tblventas.credito=1 and tblventas.idcliente=" + pidCliente.ToString
    '    If pFolio <> "" Then
    '        Comm.CommandText += " and tblventas.folio like '%" + Replace(pFolio, "'", "''") + "%'"
    '    End If
    '    Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
    '    DA.Fill(DS, "tblventas")
    '    Return DS.Tables("tblventas").DefaultView
    'End Function

    'Public Function DaTotal(ByVal pidPedido As Integer, ByVal pidMoneda As Integer) As Double
    '    Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim IDs As New Collection
    '    Dim Precio As Double
    '    Dim IdMonedaC As Integer
    '    Dim Total As Double = 0
    '    Dim Encontro As Double
    '    Dim Cont As Integer = 1
    '    '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
    '    Comm.CommandText = "select iddetalle from tblventaspedidosinventario where idpedido=" + pidPedido.ToString
    '    DReader = Comm.ExecuteReader
    '    While DReader.Read
    '        IDs.Add(DReader("iddetalle"))
    '    End While
    '    DReader.Close()
    '    While Cont <= IDs.Count
    '        Comm.CommandText = "select precio from tblventaspedidosinventario where iddetalle=" + IDs.Item(Cont).ToString
    '        Precio = Comm.ExecuteScalar
    '        Comm.CommandText = "select idmoneda from tblventaspedidosinventario where iddetalle=" + IDs.Item(Cont).ToString
    '        IdMonedaC = Comm.ExecuteScalar
    '        If IdMonedaC <> pidMoneda Then
    '            Comm.CommandText = "select if((select cantidad from tblmonedasconversiones where idmoneda=" + pidMoneda.ToString + " and idmoneda2=" + IdMonedaC.ToString + ") is null,0,(select cantidad from tblmonedasconversiones where idmoneda=" + pidMoneda.ToString + " and idmoneda2=" + IdMonedaC.ToString + "))"
    '            Encontro = Comm.ExecuteScalar
    '            If Encontro = 0 Then
    '                Comm.CommandText = "select if((select cantidad from tblmonedasconversiones where idmoneda2=" + pidMoneda.ToString + " and idmoneda=" + IdMonedaC.ToString + ") is null,1,(select cantidad from tblmonedasconversiones where idmoneda2=" + pidMoneda.ToString + " and idmoneda=" + IdMonedaC.ToString + "))"
    '                Encontro = Comm.ExecuteScalar
    '                Precio = Precio * Encontro
    '            Else
    '                Precio = Precio / Encontro
    '            End If
    '        End If
    '        Total += Precio
    '        Cont += 1
    '    End While
    '    '++++++++++++++++++++++++++++Total por Productos+++++++++++++++++++++++++++++++++++
    '    IDs.Clear()
    '    Cont = 1
    '    Comm.CommandText = "select iddetalle from tblventaspedidosproductos where idpedido=" + pidPedido.ToString
    '    DReader = Comm.ExecuteReader
    '    While DReader.Read
    '        IDs.Add(DReader("iddetalle"))
    '    End While
    '    DReader.Close()
    '    While Cont <= IDs.Count
    '        Comm.CommandText = "select precio from tblventaspedidosproductos where iddetalle=" + IDs.Item(Cont).ToString
    '        Precio = Comm.ExecuteScalar
    '        Comm.CommandText = "select idmoneda from tblventaspedidosproductos where iddetalle=" + IDs.Item(Cont).ToString
    '        IdMonedaC = Comm.ExecuteScalar
    '        If IdMonedaC <> pidMoneda Then
    '            Comm.CommandText = "select if((select cantidad from tblmonedasconversiones where idmoneda=" + pidMoneda.ToString + " and idmoneda2=" + IdMonedaC.ToString + ") is null,0,(select cantidad from tblmonedasconversiones where idmoneda=" + pidMoneda.ToString + " and idmoneda2=" + IdMonedaC.ToString + "))"
    '            Encontro = Comm.ExecuteScalar
    '            If Encontro = 0 Then
    '                Comm.CommandText = "select if((select cantidad from tblmonedasconversiones where idmoneda2=" + pidMoneda.ToString + " and idmoneda=" + IdMonedaC.ToString + ") is null,1,(select cantidad from tblmonedasconversiones where idmoneda2=" + pidMoneda.ToString + " and idmoneda=" + IdMonedaC.ToString + "))"
    '                Encontro = Comm.ExecuteScalar
    '                Precio = Precio * Encontro
    '            Else
    '                Precio = Precio / Encontro
    '            End If
    '        End If
    '        Total += Precio
    '        Cont += 1
    '    End While
    '    '+++++++++++++++++++++++++++++++++++++++Total por Servicios+++++++++++++++++++++++++++++++++++++++
    '    'IDs.Clear()
    '    'Cont = 1
    '    'Comm.CommandText = "select idventasservicio from tblventasservicios where idventa=" + pidVenta.ToString
    '    'DReader = Comm.ExecuteReader
    '    'While DReader.Read$
    '    '    IDs.Add(DReader("idventasservicio"))
    '    'End While
    '    'DReader.Close()
    '    'While Cont <= IDs.Count
    '    '    Comm.CommandText = "select precio from tblventasservicios where idventasservicio=" + IDs.Item(Cont).ToString
    '    '    Precio = Comm.ExecuteScalar
    '    '    Comm.CommandText = "select idmoneda from tblventasservicios where idventasservicio=" + IDs.Item(Cont).ToString
    '    '    IdMonedaC = Comm.ExecuteScalar
    '    '    If IdMonedaC <> pidMoneda Then
    '    '        Comm.CommandText = "select if((select cantidad from tblmonedasconversiones where idmoneda=" + pidMoneda.ToString + " and idmoneda2=" + IdMonedaC.ToString + ") is null,0,(select cantidad from tblmonedasconversiones where idmoneda=" + pidMoneda.ToString + " and idmoneda2=" + IdMonedaC.ToString + "))"
    '    '        Encontro = Comm.ExecuteScalar
    '    '        If Encontro = 0 Then
    '    '            Comm.CommandText = "select if((select cantidad from tblmonedasconversiones where idmoneda2=" + pidMoneda.ToString + " and idmoneda=" + IdMonedaC.ToString + ") is null,1,(select cantidad from tblmonedasconversiones where idmoneda2=" + pidMoneda.ToString + " and idmoneda=" + IdMonedaC.ToString + "))"
    '    '            Encontro = Comm.ExecuteScalar
    '    '            Precio = Precio * Encontro
    '    '        Else
    '    '            Precio = Precio / Encontro
    '    '        End If
    '    '    End If
    '    '    Total += Precio
    '    '    Cont += 1
    '    'End While
    '    Return Total
    'End Function
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
        Comm.CommandText = "select iddetalle from tblventaspedidosinventario where idpedido=" + pidCotizacion.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblventaspedidosinventario where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select iva from tblventaspedidosinventario where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblventaspedidosinventario where iddetalle=" + IDs.Item(Cont).ToString
            IdMonedaC = Comm.ExecuteScalar
            Comm.CommandText = "select IEPS from tblventaspedidosinventario where iddetalle=" + IDs.Item(Cont).ToString
            pIEPS = Comm.ExecuteScalar
            Comm.CommandText = "select IVARetenido from tblventaspedidosinventario where iddetalle=" + IDs.Item(Cont).ToString
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
        Comm.CommandText = "select ifnull((select sum(tblinventario.peso*tblventaspedidosinventario.cantidad) from tblventaspedidosinventario inner join tblinventario on tblventaspedidosinventario.idinventario=tblinventario.idinventario where tblventaspedidosinventario.idpedido=" + pidCotizacion.ToString + "),0)"
        TotalPeso = Comm.ExecuteScalar
        'TotalISR = Subtotal * (iIsr / 100)
        'TotalIvaRetenido = Subtotal * (iIvaRetenido / 100)
        TotalVenta = Subtotal + TotalIva + TotalIeps - TotalIvaRetenidoConceptos '- TotalISR - TotalIvaRetenido
        Return TotalVenta
    End Function
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        'Comm.CommandText = "select ifnull((select max(folio) from tblventaspedidos where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4)),0)"
        Comm.CommandText = "select ifnull((select folio from tblventaspedidos where serie='" + Replace(Trim(pSerie), "'", "''") + "' and (estado=3 or estado=4) order by folio desc limit 1),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tblventaspedidos where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado<>1 and estado<>2"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function

    'Public Function print(ByVal idmoneda As Integer) As ArrayList
    '    Dim nodos As New ArrayList
    '    Dim abd As New BDImpresiones
    '    Dim n As NodoImpresionTexto

    '    Dim dr As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim CD As New dbVentasInventario(MySqlcon)
    '    dr = CD.ConsultaReader(ID)
    '    Dim articulos As New ArrayList

    '    While dr.Read
    '        articulos.Add(New ArticuloFactura(dr("idventasinventario"), "A", "", dr("cantidad"), dr("clave"), dr("descripcion"), dr("precio"))) ', dr("abreviatura")))
    '    End While
    '    dr.Close()
    '    Dim VP As New dbVentasProductos(MySqlcon)
    '    dr = VP.ConsultaReader(ID)
    '    While dr.Read
    '        articulos.Add(New ArticuloFactura(dr("idventasproducto"), "P", "", dr("cantidad"), dr("clave"), dr("descripcion"), dr("precio"))) ', dr("abreviatura")))
    '    End While
    '    dr.Close()
    '    Dim VS As New dbVentasServicios(MySqlcon)
    '    dr = VS.ConsultaReader(ID)
    '    While dr.Read
    '        articulos.Add(New ArticuloFactura(dr("idventasservicio"), "S", "", dr("cantidad"), dr("folio"), dr("descripcion"), dr("precio"))) ', dr("abreviatura")))
    '    End While
    '    dr.Close()

    '    Cliente.BuscaCliente(IdCliente)

    '    Dim descripcion = "", cantidad = "", codigo = "", importe = "", preciounitario As String = ""
    '    Dim af As ArticuloFactura
    '    Const descmaxlength As Integer = 50
    '    For Each af In articulos
    '        Dim start = 0, length As Integer = 0
    '        'If af.idarticulo <> 0 Then
    '        cantidad += CStr(af.cantidad).PadLeft(6)
    '        codigo += af.codigo
    '        'importe += Format(af.importe, "C2").PadLeft(10)
    '        preciounitario += Format(af.preciounitario, "C2").PadLeft(10)
    '        'End If

    '        Do
    '            length = If(Format(af.descripcion).Substring(start, Format(af.descripcion).Length - start).Length <= descmaxlength, Format(af.descripcion).Length - start, If(Format(af.descripcion).Substring(start, descmaxlength).LastIndexOf(" ") = -1, descmaxlength, Format(af.descripcion).Substring(start, descmaxlength).LastIndexOf(" ")))
    '            descripcion += Format(af.descripcion).Substring(start, length) + vbNewLine
    '            cantidad += vbNewLine
    '            codigo += vbNewLine
    '            importe += vbNewLine
    '            preciounitario += vbNewLine
    '            start += length + 1
    '        Loop While start < Format(af.descripcion).Length

    '    Next

    '    For Each n In abd.find(7).campos
    '        If n.visible Then
    '            If n.texto = "titulo" Then nodos.Add(New NodoImpresionTexto("FACTURA", n.x, n.y, n.visible))
    '            If n.texto = "nombreempresa" Then nodos.Add(New NodoImpresionTexto(My.Settings.empresa, n.x, n.y, n.visible))
    '            If n.texto = "rfcempresa" Then nodos.Add(New NodoImpresionTexto(My.Settings.rfc, n.x, n.y, n.visible))

    '            If n.texto = "nombre" Then nodos.Add(New NodoImpresionTexto(Cliente.Nombre, n.x, n.y, n.visible))
    '            If n.texto = "nocliente" Then nodos.Add(New NodoImpresionTexto(Cliente.Clave, n.x, n.y, n.visible))
    '            If n.texto = "direccion" Then nodos.Add(New NodoImpresionTexto(Cliente.Direccion, n.x, n.y, n.visible))
    '            'If n.texto = "direccioncol" Then nodos.Add(New NodoImpresionTexto(Cliente.Direccion + " " + Cliente.colonia, n.x, n.y, n.visible))
    '            If n.texto = "ciudad" Then nodos.Add(New NodoImpresionTexto(Cliente.Ciudad + ", " + Cliente.Estado + " " + Cliente.CP, n.x, n.y, n.visible))
    '            'If n.texto = "colonia" Then nodos.Add(New NodoImpresionTexto(Cliente.colonia, n.x, n.y, n.visible))
    '            If n.texto = "telefono" Then nodos.Add(New NodoImpresionTexto(Cliente.Telefono, n.x, n.y, n.visible))
    '            'If n.texto = "curp" Then nodos.Add(New NodoImpresionTexto(Cliente.curp, n.x, n.y, n.visible))
    '            'If n.texto = "ruc" Then nodos.Add(New NodoImpresionTexto(Cliente.ruc, n.x, n.y, n.visible))
    '            'If n.texto = "cnpj" Then nodos.Add(New NodoImpresionTexto(Cliente.cnpj, n.x, n.y, n.visible))

    '            'If n.texto = "adicional1" Then nodos.Add(New NodoImpresionTexto(_adicional1, n.x, n.y, n.visible))
    '            'If n.texto = "adicional2" Then nodos.Add(New NodoImpresionTexto(_adicional2, n.x, n.y, n.visible))
    '            'If n.texto = "adicionalc" Then If _tipoventa = TIPOSVENTAS.CREDITO Then nodos.Add(New NodoImpresionTexto(_adicionalc, n.x, n.y, n.visible))
    '            'If n.texto = "comentario" Then nodos.Add(New NodoImpresionTexto(_comentario, n.x, n.y, n.visible))
    '            'If n.texto = "condiciones" Then nodos.Add(New NodoImpresionTexto(condiciones, n.x, n.y, n.visible))
    '            'If n.texto = "exhibiciones1" Then If exhibiciones = 0 Then nodos.Add(New NodoImpresionTexto("X", n.x, n.y, n.visible))
    '            'If n.texto = "exhibiciones2" Then If exhibiciones = 1 Then nodos.Add(New NodoImpresionTexto("X", n.x, n.y, n.visible))

    '            'If n.texto = "tasaiva" Then nodos.Add(New NodoImpresionTexto(CStr(Cliente.iva.tasa) + "%", n.x, n.y, n.visible))
    '            If n.texto = "fecha" Then nodos.Add(New NodoImpresionTexto(Format(Fecha, "dd MM yyyy"), n.x, n.y, n.visible))
    '            If n.texto = "nofactura" Then nodos.Add(New NodoImpresionTexto(Format(CInt(Folio), "0000"), n.x, n.y, n.visible))
    '            If n.texto = "rfc" Then nodos.Add(New NodoImpresionTexto(Cliente.RFC, n.x, n.y, n.visible))

    '            ''''''
    '            Dim T As Double = DaTotal(ID, idmoneda)
    '            Dim Iva As Double
    '            If Desglosar Then
    '                Iva = T - (T / (1 + (Iva / 100)))
    '                If n.texto = "subtotal" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(T - Iva, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '                If n.texto = "iva" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(Iva, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '                If n.texto = "total" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(T, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '            Else
    '                Iva = T * (Iva / 100)
    '                If n.texto = "subtotal" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(T, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '                If n.texto = "iva" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(Iva, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '                If n.texto = "total" Then nodos.Add(New NodoImpresionTexto(Format(System.Math.Round(T + Iva, 2), "C2").PadLeft(10), n.x, n.y, n.visible))
    '            End If

    '            ''''''

    '            If n.texto = "totalletra" Then nodos.Add(New NodoImpresionTexto(totalLetra(idmoneda), n.x, n.y, n.visible))

    '            If n.texto = "descripcion" Then nodos.Add(New NodoImpresionTexto(descripcion, n.x, n.y, n.visible))
    '            If n.texto = "cantidad" Then nodos.Add(New NodoImpresionTexto(cantidad, n.x, n.y, n.visible))
    '            If n.texto = "codigo" Then nodos.Add(New NodoImpresionTexto(codigo, n.x, n.y, n.visible))
    '            If n.texto = "importe" Then nodos.Add(New NodoImpresionTexto(importe, n.x, n.y, n.visible))
    '            If n.texto = "preciounitario" Then nodos.Add(New NodoImpresionTexto(preciounitario, n.x, n.y, n.visible))

    '            'If n.texto = "pagfecha1" Then nodos.Add(New NodoImpresionTexto(Format(CDate(Fecha), "dd MM yyyy"), n.x, n.y, n.visible))
    '            'If n.texto = "pagfecha2" Then nodos.Add(New NodoImpresionTexto(Format(CDate(Fecha), "dd MM yyyy"), n.x, n.y, n.visible))
    '            'If n.texto = "pagtotal" Then nodos.Add(New NodoImpresionTexto(Format(total, "C2").PadLeft(10), n.x, n.y, n.visible))
    '            'If n.texto = "pagtotalletra" Then nodos.Add(New NodoImpresionTexto(totalLetra, n.x, n.y, n.visible))
    '            'If n.texto = "pagnombre" Then nodos.Add(New NodoImpresionTexto(Cliente.Nombre, n.x, n.y, n.visible))
    '            'If n.texto = "pagdireccion" Then nodos.Add(New NodoImpresionTexto(Cliente.Direccion, n.x, n.y, n.visible))
    '            'If n.texto = "pagciudad" Then nodos.Add(New NodoImpresionTexto(Cliente.Ciudad + ", " + Cliente.Estado + " " + Cliente.CP, n.x, n.y, n.visible))
    '            'If n.texto = "pagcondiciones" Then nodos.Add(New NodoImpresionTexto(condiciones, n.x, n.y, n.visible))
    '        End If
    '    Next

    '    Return nodos
    'End Function

    'Public ReadOnly Property totalLetra(ByVal idmoneda As Integer) As String
    '    Get
    '        Dim f As New PaseLetras
    '        Return f.PASELETRAS(DaTotal(ID, idmoneda), idmoneda) + " " + [Enum].GetName(GetType(MONEDAS), idmoneda)
    '    End Get
    'End Property
    Public Function DaIvas(ByVal pIdPedido As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iva,precio,idmoneda from tblventaspedidosinventario where idpedido=" + pIdPedido.ToString
        Return Comm.ExecuteReader
    End Function

    'Public Sub AgregarDetallesReferencia(ByVal PidPedido As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte)
    '    '0 cotizacion
    '    '1 pedido
    '    '2 remision
    '    '3 ventas

    '    If Tipo = 0 Then
    '        Comm.CommandText = "insert into tblventaspedidosinventario(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,surtido,preciooriginal) select " + PidPedido.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,0,precio from tblventascotizacionesinventario where idcotizacion=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()

    '        'Comm.CommandText = "insert into tblventaspedidosproductos(idpedido,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventascotizacionesproductos where idcotizacion=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()
    '    End If

    '    If Tipo = 1 Then
    '        Comm.CommandText = "insert into tblventaspedidosinventario(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,surtido,preciooriginal) select " + PidPedido.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,0,precio from tblventaspedidosinventario where idpedido=" + pIdDocumento.ToString
    '        Comm.ExecuteNonQuery()

    '        'Comm.CommandText = "insert into tblventaspedidosproductos(idpedido,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventaspedidosproductos where idpedido=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()
    '    End If

    '    If Tipo = 2 Then
    '        Comm.CommandText = "insert into tblventaspedidosinventario(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,surtido,preciooriginal) select " + PidPedido.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,0,precio from tblventasremisionesinventario where idremision=" + pIdDocumento.ToString + " and idservicio=0"
    '        Comm.ExecuteNonQuery()

    '        'Comm.CommandText = "insert into tblventaspedidosproductos(idpedido,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasremisionesproductos where idremision=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()

    '        'Comm.CommandText = "insert into tblventaspedidosservicios(idventa,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasremisionesservicios where idremision=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()
    '        'Comm.CommandText = "update tblinventarioseries set idventa=" + PidPedido.ToString + " where idremision=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()
    '    End If

    '    If Tipo = 3 Then
    '        Comm.CommandText = "insert into tblventaspedidosinventario(idpedido,idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,surtido,preciooriginal) select " + PidPedido.ToString + ",idinventario,cantidad,precio,descripcion,idmoneda,iva,extra,descuento,idvariante,0,precio from tblventasinventario where idventa=" + pIdDocumento.ToString + " and idservicio=0"
    '        Comm.ExecuteNonQuery()

    '        'Comm.CommandText = "insert into tblventaspedidosproductos(idpedido,idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasproductos where idventa=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()

    '        'Comm.CommandText = "insert into tblventasservicios(idventa,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + PidPedido.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tblventasservicios where idventa=" + pIdDocumento.ToString
    '        'Comm.ExecuteNonQuery()
    '    End If
    'End Sub

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

    Public Sub RegresaInventario(ByVal pId As Integer)
        Dim Str As String = ""
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select idalmacen,cantidad,idinventario from tblventas inner join tblventasinventario on tblventas.idventa=tblventasinventario.idventa where tblventas.idventa=" + pId.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read()
            Str += "update tblalmacenesi set cantidad=cantidad+" + DReader("cantidad").ToString + " where idinventario=" + DReader("idinventario").ToString + " and idalmacen=" + DReader("idalmacen").ToString + "; "
        End While
        DReader.Close()
        Comm.CommandText = Str
        Comm.ExecuteNonQuery()
    End Sub


    'Public Function CreaCadenaOriginal(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
    '    Dim O As New dbOpciones(Comm.Connection)
    '    Dim CO As String = "|2.0|"
    '    ID = pIdVenta
    '    LlenaDatos()
    '    Dim TI As Double
    '    Dim CI As Double
    '    TI = DaTotal(ID, pIdMoneda)
    '    CI = TI * (Iva / 100)
    '    CO += Serie + "|"
    '    CO += Folio.ToString + "|"
    '    CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
    '    CO += NoAprobacion + "|"
    '    CO += YearAprobacion + "|"
    '    CO += "ingreso|UNA SOLA EXHIBICION|"
    '    'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
    '    CO += Format(TI, "#0.00") + "|"
    '    CO += "0|" 'descuento

    '    CO += Format(TI + CI, "#0.00") + "|" ' total factura con iva

    '    CO += O._RFC + "|"
    '    CO += O._NombreEmpresa + "|"
    '    CO += O._Calle + "|"
    '    CO += O._noExterior + "|"
    '    CO += O._noInterior + "|"
    '    CO += O._Colonia + "|"
    '    CO += O._Localidad + "|"
    '    CO += O._ReferenciaDomicilio + "|"
    '    CO += O._Municipio + "|"
    '    CO += O._Estado + "|"
    '    CO += O._Pais + "|"
    '    CO += O._CodigoPostal + "|"

    '    CO += O._CalleLocal + "|"
    '    CO += O._noExteriorLocal + "|"
    '    CO += O._noInteriorLocal + "|"
    '    CO += O._ColoniaLocal + "|"
    '    CO += O._LocalidadLocal + "|"
    '    CO += O._ReferenciaDomicilioLocal + "|"
    '    CO += O._MunicipioLocal + "|"
    '    CO += O._EstadoLocal + "|"
    '    CO += O._PaisLocal + "|"
    '    CO += O._CodigoPostalLocal + "|"

    '    CO += Cliente.RFC + "|"
    '    CO += Cliente.Nombre + "|"
    '    CO += Cliente.Direccion + "|"
    '    CO += Cliente.NoExterior + "|"
    '    CO += Cliente.NoInterior + "|"
    '    CO += Cliente.Colonia + "|"
    '    CO += Cliente.Ciudad + "|"
    '    CO += Cliente.ReferenciaDomicilio + "|"
    '    CO += Cliente.Municipio + "|"
    '    CO += Cliente.Estado + "|"
    '    CO += Cliente.Pais + "|"
    '    CO += Cliente.CP + "|"

    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    End While
    '    DR.Close()

    '    Dim VP As New dbVentasProductos(MySqlcon)
    '    DR = VP.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    End While
    '    DR.Close()

    '    Dim VS As New dbVentasServicios(MySqlcon)
    '    DR = VS.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += "SERV|"
    '        CO += DR("folio") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    End While
    '    DR.Close()
    '    'CO += "IVA|0|0|"
    '    CO += "IVA|"
    '    CO += Iva.ToString + "|"
    '    'CO += Format(((TI - (TI / (1 + (Iva / 100))))), "#0.00") + "|"
    '    'CO += Format((TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|"

    '    CO += Format(CI, "#0.00") + "|"
    '    CO += Format(CI, "#0.00") + "|"

    '    CO = Replace(CO, "||", "|")
    '    CO = Replace(CO, "||", "|")
    '    CO = Replace(CO, "||", "|")
    '    CO = "|" + CO + "|"
    '    Return CO

    'End Function


    'Public Function CreaCadenaOriginal(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
    '    Dim O As New dbOpciones(Comm.Connection)
    '    Dim CO As String = "|2.0|"
    '    ID = pIdVenta
    '    LlenaDatos()

    '    Dim mf As New Gooru.Componentes.CFD.MotorCFD(My.Settings.rutacer, My.Settings.rutakey, My.Settings.passwordkey, Application.StartupPath + "\")

    '    Dim Factura As New Gooru.Componentes.CFD.Comprobante(Fecha, Serie, Folio, NoAprobacion, YearAprobacion, Gooru.Componentes.CFD.ComprobanteTipoDeComprobante.ingreso, "UNA SOLA EXIBICIÓN", "CONTADO", 0, "")
    '    Dim TI As Double
    '    Dim CI As Double
    '    TI = DaTotal(ID, pIdMoneda)
    '    CI = TI * (Iva / 100)
    '    CO += Serie + "|"
    '    CO += Folio.ToString + "|"
    '    CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
    '    CO += NoAprobacion + "|"
    '    CO += YearAprobacion + "|"
    '    CO += "ingreso|UNA SOLA EXHIBICION|"
    '    'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
    '    CO += Format(TI, "#0.00") + "|"
    '    CO += "0|" 'descuento

    '    CO += Format(TI + CI, "#0.00") + "|" ' total factura con iva

    '    CO += O._RFC + "|"
    '    CO += O._NombreEmpresa + "|"
    '    CO += O._Calle + "|"
    '    CO += O._noExterior + "|"
    '    CO += O._noInterior + "|"
    '    CO += O._Colonia + "|"
    '    CO += O._Localidad + "|"
    '    CO += O._ReferenciaDomicilio + "|"
    '    CO += O._Municipio + "|"
    '    CO += O._Estado + "|"
    '    CO += O._Pais + "|"
    '    CO += O._CodigoPostal + "|"

    '    Factura.AgregaDatosEmisor("DEMO101010A1A", O._NombreEmpresa, O._Calle, O._noExterior, O._noInterior, O._Colonia, O._CodigoPostal, O._Municipio, O._Estado, O._Localidad, O._Pais, O._ReferenciaDomicilio)
    '    Factura.AgregaDatosExpedicion(O._CalleLocal, O._noExteriorLocal, O._noInteriorLocal, O._ColoniaLocal, O._CodigoPostalLocal, O._MunicipioLocal, O._EstadoLocal, O._LocalidadLocal, O._PaisLocal, O._ReferenciaDomicilioLocal)
    '    Factura.AgregaDatosReceptor(Cliente.RFC, Cliente.Nombre, Cliente.Direccion, Cliente.NoExterior, Cliente.NoInterior, Cliente.Colonia, Cliente.CP, Cliente.Municipio, Cliente.Estado, Cliente.Ciudad, Cliente.Pais, Cliente.ReferenciaDomicilio)
    '    CO += O._CalleLocal + "|"
    '    CO += O._noExteriorLocal + "|"
    '    CO += O._noInteriorLocal + "|"
    '    CO += O._ColoniaLocal + "|"
    '    CO += O._LocalidadLocal + "|"
    '    CO += O._ReferenciaDomicilioLocal + "|"
    '    CO += O._MunicipioLocal + "|"
    '    CO += O._EstadoLocal + "|"
    '    CO += O._PaisLocal + "|"
    '    CO += O._CodigoPostalLocal + "|"

    '    CO += Cliente.RFC + "|"
    '    CO += Cliente.Nombre + "|"
    '    CO += Cliente.Direccion + "|"
    '    CO += Cliente.NoExterior + "|"
    '    CO += Cliente.NoInterior + "|"
    '    CO += Cliente.Colonia + "|"
    '    CO += Cliente.Ciudad + "|"
    '    CO += Cliente.ReferenciaDomicilio + "|"
    '    CO += Cliente.Municipio + "|"
    '    CO += Cliente.Estado + "|"
    '    CO += Cliente.Pais + "|"
    '    CO += Cliente.CP + "|"

    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '        Factura.AgregaConcepto(DR("cantidad").ToString, DR("tipocantidad").ToString, "", DR("descripcion"), DR("precio") / DR("cantidad"))
    '    End While
    '    DR.Close()

    '    Dim VP As New dbVentasProductos(MySqlcon)
    '    DR = VP.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    End While
    '    DR.Close()

    '    Dim VS As New dbVentasServicios(MySqlcon)
    '    DR = VS.ConsultaReader(ID)

    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        CO += "SERV|"
    '        CO += DR("folio") + "|"
    '        CO += DR("descripcion") + "|"
    '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
    '        CO += Format(DR("precio"), "#0.00") + "|"
    '    End While
    '    DR.Close()
    '    'CO += "IVA|0|0|"
    '    CO += "IVA|"
    '    CO += Iva.ToString + "|"
    '    'CO += Format(((TI - (TI / (1 + (Iva / 100))))), "#0.00") + "|"
    '    'CO += Format((TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|"
    '    Factura.AgregaImpuesto(Gooru.Componentes.CFD.ComprobanteImpuestosTrasladoImpuesto.IVA, CDec(Iva), CDec(CI))
    '    CO += Format(CI, "#0.00") + "|"
    '    CO += Format(CI, "#0.00") + "|"

    '    CO = Replace(CO, "||", "|")
    '    CO = Replace(CO, "||", "|")
    '    CO = Replace(CO, "||", "|")
    '    CO = "|" + CO + "|"

    '    mf.Comprobantes.Add(Factura)
    '    Dim resultado As Gooru.Componentes.CFD.ResultadoProceso = Nothing
    '    'dispara el proceso para la generacion de archivos
    '    resultado = mf.ProcesarComprobantes(True, False)
    '    If resultado.ArchivosXMLPDF.Length <> 0 Then
    '        Dim en As New Encriptador
    '        en.GuardaArchivoTexto("Pueba.xml", resultado.ArchivosXMLPDF(0), System.Text.Encoding.UTF8)
    '    Else
    '        For Each c As Gooru.Componentes.CFD.Comprobante In resultado.ComprobantesNoGenerados
    '            MsgBox(c.ErrorGeneracion.Message)
    '        Next
    '    End If
    '    Return CO

    'End Function


    'Public Function CreaXML(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String) As String
    '    Dim O As New dbOpciones(Comm.Connection)
    '    Dim en As New Encriptador
    '    Dim XMLDoc As String

    '    XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

    '    XMLDoc += "<Comprobante " + vbCrLf

    '    en.Leex509(My.Settings.rutacer)



    '    ID = pIdVenta
    '    LlenaDatos()

    '    Dim TI As Double
    '    Dim CI As Double
    '    TI = DaTotal(ID, pIdMoneda)
    '    CI = TI * (Iva / 100)
    '    If Serie <> "" Then XMLDoc += "serie=""" + Serie + """" + vbCrLf
    '    XMLDoc += "version = ""2.0""" + vbCrLf
    '    XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
    '    XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
    '    If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
    '    If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf
    '    'XMLDoc += "subTotal=""" + Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + """" + vbCrLf
    '    XMLDoc += "subTotal=""" + Format(TI, "#0.00") + """" + vbCrLf
    '    'XMLDoc += "total=""" + Format(TI, "#0.00") + """" + vbCrLf
    '    XMLDoc += "total=""" + Format(TI + CI, "#0.00") + """" + vbCrLf
    '    If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
    '    If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
    '    XMLDoc += "formaDePago=""UNA SOLA EXHIBICION""" + vbCrLf
    '    XMLDoc += "descuento=""" + "0" + """" + vbCrLf
    '    XMLDoc += "tipoDeComprobante=""ingreso""" + vbCrLf
    '    XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf
    '    XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
    '    XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
    '    XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf

    '    XMLDoc += ">"

    '    XMLDoc += "<Emisor rfc=""" + O._RFC + """ nombre=""" + O._NombreEmpresa + """>" + vbCrLf

    '    XMLDoc += "<DomicilioFiscal " + vbCrLf
    '    If O._Calle <> "" Then XMLDoc += "calle = """ + O._Calle + """" + vbCrLf
    '    If O._noExterior <> "" Then XMLDoc += "noExterior=""" + O._noExterior + """" + vbCrLf
    '    If O._noInterior <> "" Then XMLDoc += "noInterior=""" + O._noInterior + """" + vbCrLf
    '    If O._Colonia <> "" Then XMLDoc += "colonia=""" + O._Colonia + """" + vbCrLf
    '    If O._Localidad <> "" Then XMLDoc += "localidad=""" + O._Localidad + """" + vbCrLf
    '    If O._ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilio + """" + vbCrLf
    '    If O._Municipio <> "" Then XMLDoc += "municipio=""" + O._Municipio + """" + vbCrLf
    '    If O._Estado <> "" Then XMLDoc += "estado=""" + O._Estado + """" + vbCrLf
    '    If O._Pais <> "" Then XMLDoc += "pais=""" + O._Pais + """" + vbCrLf
    '    If O._CodigoPostal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostal + """" + vbCrLf
    '    XMLDoc += "/>" + vbCrLf

    '    If O._CalleLocal <> "" Then XMLDoc += "<ExpedidoEn calle=""" + O._CalleLocal + """" + vbCrLf
    '    If O._noExteriorLocal <> "" Then XMLDoc += "noExterior=""" + O._noExteriorLocal + """" + vbCrLf
    '    If O._noInteriorLocal <> "" Then XMLDoc += "noInterior=""" + O._noInteriorLocal + """" + vbCrLf
    '    If O._ColoniaLocal <> "" Then XMLDoc += "colonia=""" + O._ColoniaLocal + """" + vbCrLf
    '    If O._LocalidadLocal <> "" Then XMLDoc += "localidad=""" + O._LocalidadLocal + """" + vbCrLf
    '    If O._ReferenciaDomicilioLocal <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilioLocal + """" + vbCrLf
    '    If O._MunicipioLocal <> "" Then XMLDoc += "municipio=""" + O._MunicipioLocal + """" + vbCrLf
    '    If O._EstadoLocal <> "" Then XMLDoc += "estado=""" + O._EstadoLocal + """" + vbCrLf
    '    If O._PaisLocal <> "" Then XMLDoc += "pais=""" + O._PaisLocal + """" + vbCrLf
    '    If O._CodigoPostalLocal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostalLocal + """"
    '    XMLDoc += "/>" + vbCrLf


    '    XMLDoc += "</Emisor>" + vbCrLf


    '    XMLDoc += "<Receptor rfc=""" + Cliente.RFC + """ nombre=""" + Cliente.Nombre + """>" + vbCrLf

    '    If Cliente.Direccion <> "" Then XMLDoc += "<Domicilio calle=""" + Cliente.Direccion + """" + vbCrLf
    '    If Cliente.NoExterior <> "" Then XMLDoc += "noExterior=""" + Cliente.NoExterior + """" + vbCrLf
    '    If Cliente.NoInterior <> "" Then XMLDoc += "noInterior=""" + Cliente.NoInterior + """" + vbCrLf
    '    If Cliente.Colonia <> "" Then XMLDoc += "colonia=""" + Cliente.Colonia + """" + vbCrLf
    '    If Cliente.Ciudad <> "" Then XMLDoc += "localidad=""" + Cliente.Ciudad + """" + vbCrLf
    '    If Cliente.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Cliente.ReferenciaDomicilio + """" + vbCrLf
    '    If Cliente.Municipio <> "" Then XMLDoc += "municipio=""" + Cliente.Municipio + """" + vbCrLf
    '    If Cliente.Estado <> "" Then XMLDoc += "estado=""" + Cliente.Estado + """" + vbCrLf
    '    If Cliente.Pais <> "" Then XMLDoc += "pais=""" + Cliente.Pais + """" + vbCrLf
    '    If Cliente.CP <> "" Then XMLDoc += "codigoPostal=""" + Cliente.CP + """" + vbCrLf
    '    XMLDoc += "/>" + vbCrLf

    '    XMLDoc += "</Receptor>" + vbCrLf

    '    XMLDoc += "<Conceptos>" + vbCrLf

    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReader(ID)

    '    While DR.Read
    '        XMLDoc += "<Concepto " + vbCrLf
    '        XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
    '        XMLDoc += "unidad=""" + DR("tipocantidad") + """" + vbCrLf
    '        XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
    '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "/> " + vbCrLf
    '    End While
    '    DR.Close()

    '    Dim VP As New dbVentasProductos(MySqlcon)
    '    DR = VP.ConsultaReader(ID)

    '    While DR.Read

    '        XMLDoc += "<Concepto " + vbCrLf
    '        XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
    '        XMLDoc += "unidad=""" + DR("tipocantidad") + """" + vbCrLf
    '        XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
    '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "/> " + vbCrLf

    '        'CO += DR("cantidad").ToString + "|"
    '        'CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        'CO += DR("descripcion") + "|"
    '        'CO += CStr(DR("precio") / DR("cantidad")) + "|"
    '        'CO += DR("precio").ToString + "|"
    '    End While
    '    DR.Close()

    '    Dim VS As New dbVentasServicios(MySqlcon)
    '    DR = VS.ConsultaReader(ID)

    '    While DR.Read

    '        XMLDoc += "<Concepto " + vbCrLf
    '        XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
    '        XMLDoc += "unidad=""SERV""" + vbCrLf
    '        XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
    '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
    '        XMLDoc += "/> " + vbCrLf

    '        'CO += DR("cantidad").ToString + "|"
    '        'CO += "SERV|"
    '        'CO += DR("folio") + "|"
    '        'CO += DR("descripcion") + "|"
    '        'CO += CStr(DR("precio") / DR("cantidad")) + "|"
    '        'CO += DR("precio").ToString + "|"
    '    End While
    '    DR.Close()
    '    XMLDoc += "</Conceptos>"
    '    'XMLDoc += "<Impuestos totalImpuestosTrasladados=""" + Format(((TI - (TI / (1 + (Iva / 100))))), "#0.00") + """>" + vbCrLf
    '    XMLDoc += "<Impuestos totalImpuestosTrasladados=""" + Format(CI, "#0.00") + """>" + vbCrLf
    '    XMLDoc += "<Traslados>" + vbCrLf
    '    XMLDoc += "<Traslado impuesto=""IVA""" + vbCrLf
    '    XMLDoc += "tasa=""" + Iva.ToString + """" + vbCrLf
    '    XMLDoc += "importe=""" + Format(CI, "#0.00") + """ />" + vbCrLf
    '    XMLDoc += "</Traslados>" + vbCrLf
    '    XMLDoc += "</Impuestos>" + vbCrLf
    '    XMLDoc += "</Comprobante>"


    '    Return XMLDoc

    'End Function
    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, ByVal pIdVendedor As Integer, ByVal pSerie As String, pidCaja As Integer, pIdTipoSucursal As Integer, pConcentrado As Boolean) As DataView
        Dim DS As New DataSet
        'If pMostrarEnPesos = 0 Then
        'Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idmoneda=2,v.total,v.total*v.tipodecambio) as total,if(v.idmoneda=2,v.totalapagar,v.totalapagar*v.tipodecambio) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre " + _
        '"from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente where v.usado=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        'Else
        If pConcentrado = False Then
            'If pMostrarEnPesos = 0 Then
            'Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idconversion=2,round(v.total,2),round(v.total*v.tipodecambio,2)) as total,if(v.idconversion=2,round(v.totalapagar,2),round(v.totalapagar*v.tipodecambio,2)) as totalapagar,v.fecha,0 as tipodecambio,2 as idmoneda,c.nombre as cnombre,v.usado " + _
            '"from tblventaspedidos v inner join tblclientes c on v.idcliente=c.idcliente inner join tblsucursales s on v.idsucursal=s.idsucursal where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
            'Else
            Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,v.total as total,v.totalapagar as totalapagar,v.fecha,0 as tipodecambio,2 as idmoneda,c.nombre as cnombre,v.usado " + _
            "from tblventaspedidos v inner join tblclientes c on v.idcliente=c.idcliente inner join tblsucursales s on v.idsucursal=s.idsucursal where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
            'End If
        Else
            Comm.CommandText = "select v.fecha,sum(round(v.total,2)) as total,sum(round(v.totalapagar,2)) as totalapagar,c.nombre as cnombre " + _
        "from tblventaspedidos v inner join tblclientes c on v.idcliente=c.idcliente inner join tblsucursales s on v.idsucursal=s.idsucursal where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        End If

        If pIdSucursal > 0 Then Comm.CommandText += " and v.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and v.idcliente=" + pIdCliente.ToString
        If pIdVendedor > 0 Then Comm.CommandText += " and v.idvendedor=" + pIdVendedor.ToString
        If pSoloCanceladas Then
            Comm.CommandText += " and v.estado=4"
        Else
            Comm.CommandText += " and v.estado=3"
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and v.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pidCaja > 0 Then
            Comm.CommandText += " and v.idcaja=" + pidCaja.ToString
        End If
        If pConcentrado Then
            Comm.CommandText += " group by v.fecha,v.idcliente"
        End If
        Comm.CommandText += " order by v.fecha"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblremisiones")
        'DS.WriteXmlSchema("tblremisiones.xml")
        Return DS.Tables("tblremisiones").DefaultView
    End Function
    Public Function ReportePorArticulo(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pMostrarEnPesos As Byte, ByVal pSoloCanceladas As Boolean, ByVal pIdVendedor As Integer, ByVal pSerie As String, pidCaja As Integer, pIdTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        'If pMostrarEnPesos = 0 Then
        'Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idmoneda=2,v.total,v.total*v.tipodecambio) as total,if(v.idmoneda=2,v.totalapagar,v.totalapagar*v.tipodecambio) as totalapagar,v.fecha,v.tipodecambio,v.idmoneda,c.nombre as cnombre " + _
        '"from tblventasremisiones v inner join tblclientes c on v.idcliente=c.idcliente where v.usado=0 and v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        'Else

        'If pMostrarEnPesos = 0 Then
        'Comm.CommandText = "select v.idpedido idventa,v.fecha,v.folio,v.serie,v.estado,if(v.idconversion=2,round(v.total,2),round(v.total*v.tipodecambio,2)) as total,if(v.idconversion=2,round(v.totalapagar,2),round(v.totalapagar*v.tipodecambio,2)) as totalapagar,v.fecha,0 as tipodecambio,2 as idmoneda,c.nombre as cnombre,v.usado " + _
        '"from tblventaspedidos v inner join tblclientes c on v.idcliente=c.idcliente inner join tblsucursales s on v.idsucursal=s.idsucursal where v.fecha>='" + pFecha1 + "' and v.fecha<='" + pFecha2 + "'"
        'Comm.CommandText = "select sum(pd.cantidad) cantidad,sum(if(pd.idmoneda=2,pd.precio,pd.precio*p.tipodecambio)) precio,pd.descripcion,i.clave " + _
        '"from tblventaspedidos p inner join tblventaspedidosdetalles pd p.idpedido=pd.idpedido inner join tblsucursales s on p.idsucursal=s.idsucursal inner join tblinventario i on pd.idinventario=i.idinventario where p.fecha>='" + pFecha1 + "' and p.fecha<='" + pFecha2 + "'"
        'Else
        Comm.CommandText = "select sum(pd.cantidad) cantidad,sum(pd.precio) precio,pd.descripcion,i.clave " + _
        "from tblventaspedidos p inner join tblventaspedidosinventario pd on p.idpedido=pd.idpedido inner join tblsucursales s on p.idsucursal=s.idsucursal inner join tblinventario i on pd.idinventario=i.idinventario where p.fecha>='" + pFecha1 + "' and p.fecha<='" + pFecha2 + "'"
        'End If


        If pIdSucursal > 0 Then Comm.CommandText += " and p.idsucursal=" + pIdSucursal.ToString
        If pIdTipoSucursal > 0 Then Comm.CommandText += " and s.idtipo=" + pIdTipoSucursal.ToString
        If pIdCliente > 0 Then Comm.CommandText += " and p.idcliente=" + pIdCliente.ToString
        If pIdVendedor > 0 Then Comm.CommandText += " and p.idvendedor=" + pIdVendedor.ToString
        If pSoloCanceladas Then
            Comm.CommandText += " and p.estado=4"
        Else
            Comm.CommandText += " and p.estado=3"
        End If
        If pSerie <> "" Then
            Comm.CommandText += " and p.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pidCaja > 0 Then
            Comm.CommandText += " and p.idcaja=" + pidCaja.ToString
        End If
        'If pConcentrado Then
        Comm.CommandText += " group by pd.idinventario"
        'End If
        Comm.CommandText += " order by i.clave"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblpedidosd")
        DS.WriteXmlSchema("reppedidosd.xml")
        Return DS.Tables("tblpedidosd").DefaultView
    End Function
    Public Function BuscaPedido(ByVal pFolio As String) As Integer
        Comm.CommandText = "select ifnull((select idpedido from tblventaspedidos where concat(tblventaspedidos.serie,convert(tblventaspedidos.folio using utf8))='" + Replace(pFolio, "'", "''") + "' and estado>2 limit 1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function DaId(ByVal pFolio As String) As Integer
        Comm.CommandText = "select ifnull((select idpedido from tblventaspedidos where tblventaspedidos.estado=3 and usado=0 and concat(tblventaspedidos.serie,convert(tblventaspedidos.folio using utf8))='" + Replace(pFolio, "'", "''") + "'),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Sub Usar(ByVal pid As Integer)
        Comm.CommandText = "update tblventaspedidos set usado=1 where idpedido=" + pid.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaTotalCantidad(ByVal pIdVenta As Integer) As Double
        Comm.CommandText = "select ifnull((select sum(tblventaspedidosinventario.cantidad) from tblventaspedidosinventario inner join tblinventario on tblventaspedidosinventario.idinventario=tblinventario.idinventario where idpedido=" + pIdVenta.ToString + " and inventariable=1),0)"
        Return Comm.ExecuteScalar
    End Function
    Public Function RevisaConceptos(ByVal pIdPedido As Integer) As Boolean
        Dim C1 As Integer
        Dim C2 As Integer
        Comm.CommandText = "select count(idmoneda) from tblventaspedidosinventario where idpedido=" + pIdPedido.ToString + " and idmoneda=2"
        C1 = Comm.ExecuteScalar
        Comm.CommandText = "select count(idmoneda) from tblventaspedidosinventario where idpedido=" + pIdPedido.ToString + " and idmoneda<>2"
        C2 = Comm.ExecuteScalar
        If C1 <> 0 And C2 <> 0 Then
            Return False
        End If
        Return True
    End Function
    Public Function DaMoneda(ByVal pIdPedido As Integer) As Integer
        Comm.CommandText = "select ifnull((select idmoneda from tblventaspedidosinventario where idpedido=" + pIdPedido.ToString + " limit 1),2)"
        Return Comm.ExecuteScalar
    End Function
End Class
