Public Class dbDevoluciones
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdCliente As Integer
    Public Fecha As String
    Public Cliente As dbClientes
    Public Folio As Integer
    Public Desglosar As Byte
    Public Facturado As Byte
    Public Credito As Double
    Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    Public Serie As String
    Public NoCertificado As String
    Public NoAprobacion As String
    Public YearAprobacion As String
    Public EsElectronica As Byte
    Public Estado As Byte
    Public SerieCertificado As String
    Public IdSucursal As Integer
    Public IdFormadePago As Integer
    Public TipodeCambio As Double
    Public IdConversion As Integer
    Public TotalVenta As Double
    Public Subtototal As Double
    Public TotalIva As Double
    Public TotalISR As Double
    Public TotalIvaRetenido As Double
    Public TotalIeps As Double
    Public ISR As Double
    Public IvaRetenido As Double
    Public idVenta As Integer
    Public IdRemision As Integer
    Public uuid As String
    Public FechaTimbrado As String
    Public SelloCFD As String
    Public NoCertificadoSAT As String
    Public SelloSAT As String
    Public Comentario As String
    Public MensajeError As String
    Public ReferenciaDoc As String
    Public FechaCancelado As String
    Public UUIDVenta As String
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
        Folio = 0
        Desglosar = 0
        Facturado = 0
        Credito = 0
        Iva = 0
        TotalaPagar = 0
        Total = 0
        Serie = ""
        NoCertificado = ""
        NoAprobacion = ""
        YearAprobacion = ""
        EsElectronica = 0
        Estado = 0
        IdSucursal = 0
        IdFormadePago = 0
        TipodeCambio = 0
        IdConversion = 0
        ISR = 0
        IvaRetenido = 0
        idVenta = 0
        IdRemision = 0
        Comentario = ""
        ReferenciaDoc = ""
        Comm.Connection = Conexion
        Cliente = New dbClientes(Comm.Connection)
    End Sub
    Public Sub New(ByVal pID As Integer, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = pID
        Comm.Connection = Conexion
        LlenaDatos()
    End Sub
    Private Sub LlenaDatos()
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select *,if(idventa>0,ifnull((select uuid from tblventastimbrado where idventa=tbldevoluciones.idventa),''),'') uuidventa from tbldevoluciones where iddevolucion=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdCliente = DReader("idcliente")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            Desglosar = DReader("desglosar")
            Facturado = DReader("facturado")
            Credito = DReader("credito")
            Iva = DReader("iva")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Serie = DReader("serie")
            NoAprobacion = DReader("noaprobacion")
            NoCertificado = DReader("nocertificado")
            YearAprobacion = DReader("yearaprobacion")
            EsElectronica = DReader("eselectronica")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            IdFormadePago = DReader("idforma")
            TipodeCambio = DReader("tipodecambio")
            IdConversion = DReader("idconversion")
            IvaRetenido = DReader("ivaretenido")
            ISR = DReader("isr")
            idVenta = DReader("idventa")
            IdRemision = DReader("idremision")
            Comentario = DReader("comentario")
            ReferenciaDoc = DReader("referenciadoc")
            FechaCancelado = DReader("fechacancelado")
            UUIDVenta = DReader("uuidventa")
        End If
        DReader.Close()
        Cliente = New dbClientes(IdCliente, Comm.Connection)
    End Sub
    'Public Function ExisteFolio(ByVal pfolio As Integer, Optional ByVal iddevolucion As Integer = -1) As Boolean
    '   Folio = pfolio
    '  Comm.CommandText = "select count(folio) from tbldevoluciones where folio=" + Folio.ToString + If(iddevolucion = -1, "", " and iddevolucion<>" + CStr(iddevolucion))
    ' If Comm.ExecuteScalar = 0 Then Return False Else Return True
    'End Function

    Public Sub Guardar(ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pSerie As String, ByVal pNoAprobacion As String, ByVal pNoCertificado As String, ByVal pYearAprovacion As String, ByVal pEsElectronica As Byte, ByVal pidSucursal As Integer, ByVal pIdFormaDepago As Integer, ByVal pTipodeCambio As Double, ByVal pIdConversion As Integer, ByVal pIsr As Double, ByVal pIvaretenido As Double, ByVal pidVenta As Integer, ByVal pidRemision As Integer, ByVal pRefDoca As String)
        NoAprobacion = pNoAprobacion
        NoCertificado = pNoCertificado
        YearAprobacion = pYearAprovacion
        EsElectronica = pEsElectronica
        IdCliente = pIdCliente
        Fecha = pFecha
        Folio = pFolio
        Desglosar = pDesglosar
        Iva = pIva
        Serie = pSerie
        IdSucursal = pidSucursal
        IdFormadePago = pIdFormaDepago
        TipodeCambio = pTipodeCambio
        IdConversion = pIdConversion
        IvaRetenido = pIvaretenido
        ISR = pIsr
        idVenta = pidVenta
        IdRemision = pidRemision
        ReferenciaDoc = pRefDoca
        Comm.CommandText = "insert into tbldevoluciones(idcliente,fecha,folio,desglosar,facturado,credito,iva,totalapagar,total,hora,serie,noaprobacion,nocertificado,yearaprobacion,eselectronica,estado,idsucursal,idforma,tipodecambio,idconversion,ivaretenido,isr,fechacancelado,horacancelado,idventa,idremision,comentario,referenciadoc,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + IdCliente.ToString + ",'" + Fecha + "'," + Folio.ToString + "," + Desglosar.ToString + ",0,0," + Iva.ToString + ",0,0,'" + Format(TimeOfDay, "HH:mm:ss") + "','" + Serie + "','" + NoAprobacion + "','" + NoCertificado + "','" + YearAprobacion + "'," + EsElectronica.ToString + ",1," + IdSucursal.ToString + "," + IdFormadePago.ToString + "," + TipodeCambio.ToString + "," + IdConversion.ToString + "," + IvaRetenido.ToString + "," + ISR.ToString + ",'',''," + idVenta.ToString + "," + IdRemision.ToString + ",'','" + Replace(ReferenciaDoc, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'," + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(iddevolucion) from tbldevoluciones"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As Integer, ByVal pDesglosar As Byte, ByVal pIva As Double, ByVal pSerie As String, ByVal pNoAprobacion As String, ByVal pNoCertificado As String, ByVal pYearAprovacion As String, ByVal pEsElectronica As Byte, ByVal pEstado As Byte, ByVal pIdFormadePago As Integer, ByVal pCredito As Byte, ByVal pTipodeCambio As Double, ByVal pidConversion As Integer, ByVal pSubTotal As Double, ByVal pTotal As Double, ByVal pIdCliente As Integer, ByVal pComentario As String)
        ID = pID
        Fecha = pFecha
        Folio = pFolio
        Desglosar = pDesglosar
        Iva = pIva
        Serie = pSerie
        NoAprobacion = pNoAprobacion
        NoCertificado = pNoCertificado
        YearAprobacion = pYearAprovacion
        EsElectronica = pEsElectronica
        Estado = pEstado
        Credito = Credito
        IdFormadePago = pIdFormadePago
        TipodeCambio = pTipodeCambio
        IdConversion = pidConversion
        IdCliente = pIdCliente
        Comentario = pComentario
        Comm.CommandText = "update tbldevoluciones set fecha='" + Fecha + "',folio=" + Folio.ToString + ",serie='" + Serie + "',desglosar=" + Desglosar.ToString + ",iva=" + Iva.ToString + ",noaprobacion='" + NoAprobacion + "',nocertificado='" + NoCertificado + "',yearaprobacion='" + YearAprobacion + "',eselectronica=" + EsElectronica.ToString + ",estado=" + Estado.ToString + ",idforma=" + IdFormadePago.ToString + ",credito=" + Credito.ToString + ",tipodecambio=" + TipodeCambio.ToString + ",idconversion=" + IdConversion.ToString + ",total=" + pSubTotal.ToString + ",totalapagar=" + pTotal.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',idcliente=" + IdCliente.ToString + ",comentario='" + Replace(Comentario, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where iddevolucion=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tbldevoluciones where iddevolucion=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, Optional ByVal pNombreClave As String = "", Optional ByVal pFolio As String = "", Optional ByVal pEstado As Byte = 0, Optional ByVal pCredido As Byte = 200) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevoluciones.iddevolucion,tbldevoluciones.fecha,concat(tbldevoluciones.serie,convert(tbldevoluciones.folio using utf8)),tblclientes.clave,tblclientes.nombre as Cliente,case tbldevoluciones.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado from tbldevoluciones inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblclientes.clave,tblclientes.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tbldevoluciones.serie,convert(tbldevoluciones.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado <> 0 Then
            Comm.CommandText += " and tbldevoluciones.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tbldevoluciones.estado<>1"
        End If
        'If pCredido <> 200 Then
        ' Comm.CommandText += " and tblformasdepago.tipo=" + pCredido.ToString
        ' End If
        Comm.CommandText += " order by tbldevoluciones.fecha desc,tbldevoluciones.serie,tbldevoluciones.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevoluciones")
        Return DS.Tables("tbldevoluciones").DefaultView
    End Function

    Public Function ConsultaDeudas(ByVal pFecha As String, ByVal pFecha2 As String, ByVal pidCliente As Integer, ByVal pFolio As String, ByVal pidTipodePago As Integer, ByVal PorFechas As Boolean, ByVal Todas As Boolean, ByVal pTipodeOrden As Byte) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevoluciones.iddevolucion,0 as sel,tbldevoluciones.fecha,tbldevoluciones.serie,tbldevoluciones.folio,tbldevoluciones.credito,tbldevoluciones.totalapagar,tbldevoluciones.totalapagar-tbldevoluciones.credito as restante from tbldevoluciones inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tbldevoluciones.idforma where tbldevoluciones.estado=3 and tbldevoluciones.idcliente=" + pidCliente.ToString + " and tblformasdepago.tipo=" + pidTipodePago.ToString
        If Todas = False Then
            Comm.CommandText += " and tbldevoluciones.credito<tbldevoluciones.totalapagar"
        End If
        If PorFechas Then
            Comm.CommandText += " and fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and concat(tbldevoluciones.serie,convert(tbldevoluciones.folio using utf8)) like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pTipodeOrden = 0 Then
            Comm.CommandText += " order by tbldevoluciones.fecha,tbldevoluciones.serie,tbldevoluciones.folio"
        Else
            Comm.CommandText += " order by tbldevoluciones.totalapagar,tbldevoluciones.serie,tbldevoluciones.folio"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevoluciones")
        Return DS.Tables("tbldevoluciones").DefaultView
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
        Dim iIsr As Double
        Dim iIeps As Double
        Dim iIvaRetenidoD As Double
        Dim iIvaRetenido As Double
        Subtototal = 0
        TotalIva = 0
        TotalVenta = 0
        TotalIeps = 0
        TotalIvaRetenido = 0
        Comm.CommandText = "select tipodecambio from tbldevoluciones where iddevolucion=" + piddevolucion.ToString
        iTipoCambio = Comm.ExecuteScalar
        Comm.CommandText = "select isr from tbldevoluciones where iddevolucion=" + piddevolucion.ToString
        iIsr = Comm.ExecuteScalar
        Comm.CommandText = "select ivaretenido from tbldevoluciones where iddevolucion=" + piddevolucion.ToString
        iIvaRetenido = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tbldevolucionesdetalles where iddevolucion=" + piddevolucion.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tbldevolucionesdetalles where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select iva from tbldevolucionesdetalles where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select ieps from tbldevolucionesdetalles where iddetalle=" + IDs.Item(Cont).ToString
            iIeps = Comm.ExecuteScalar
            Comm.CommandText = "select ivaretenido from tbldevolucionesdetalles where iddetalle=" + IDs.Item(Cont).ToString
            iIvaRetenidoD = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tbldevolucionesdetalles where iddetalle=" + IDs.Item(Cont).ToString
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
            
            Subtototal += Precio
            TotalIva += (Precio * (iIva / 100))
            TotalIeps += (Precio * (iIeps / 100))
            TotalIvaRetenido += (Precio * (iIvaRetenidoD / 100))
            Cont += 1
        End While
        TotalISR = Subtototal * (iIsr / 100)
        TotalIvaRetenido += Subtototal * (iIvaRetenido / 100)
        TotalVenta = Subtototal + TotalIva - TotalISR - TotalIvaRetenido + TotalIeps
        Return TotalVenta
    End Function
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folio) from tbldevoluciones where serie='" + pSerie + "' and (estado=3 or estado=4) ),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As String, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folio) from tbldevoluciones where folio='" + pFolio + "' and serie='" + Replace(pSerie, "'", "''") + "' and estado<>1 and estado<>2"
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

    'Public Function print(ByVal idmoneda As Integer) As ArrayList
    '    Dim nodos As New ArrayList
    '    Dim abd As New BDImpresiones
    '    Dim n As NodoImpresionTexto

    '    Dim dr As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim CD As New dbVentasInventario(MySqlcon)
    '    dr = CD.ConsultaReader(ID)
    '    Dim articulos As New ArrayList

    '    While dr.Read
    '        articulos.Add(New ArticuloFactura(dr("iddevolucionsinventario"), "A", "", dr("cantidad"), dr("clave"), dr("descripcion"), dr("precio"))) ', dr("abreviatura")))
    '    End While
    '    dr.Close()
    '    Dim VP As New dbVentasProductos(MySqlcon)
    '    dr = VP.ConsultaReader(ID)
    '    While dr.Read
    '        articulos.Add(New ArticuloFactura(dr("iddevolucionsproducto"), "P", "", dr("cantidad"), dr("clave"), dr("descripcion"), dr("precio"))) ', dr("abreviatura")))
    '    End While
    '    dr.Close()
    '    Dim VS As New dbVentasServicios(MySqlcon)
    '    dr = VS.ConsultaReader(ID)
    '    While dr.Read
    '        articulos.Add(New ArticuloFactura(dr("iddevolucionsservicio"), "S", "", dr("cantidad"), dr("folio"), dr("descripcion"), dr("precio"))) ', dr("abreviatura")))
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
    '        Dim f As New StringFunctions
    '        Return f.PASELETRAS(DaTotal(ID, idmoneda), idmoneda)
    '    End Get
    'End Property


    Public Sub AgregarDetallesReferencia(ByVal Piddevolucion As Integer, ByVal pIdDocumento As Integer, ByVal Tipo As Byte, ByVal pidAlmacen As Integer)
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
            Comm.CommandText = "insert into tbldevolucionesdetalles(iddevolucion,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,cantidadm,tipocantidadm,equivalencia,equivalenciab,ieps,ivaretenido) select " + Piddevolucion.ToString + ",tblventasremisionesinventario.idinventario,tblventasremisionesinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idremision=tblventasremisionesinventario.idremision and tbldevolucionesdetalles.idinventario=tblventasremisionesinventario.idinventario and tbldevoluciones.estado=3),0)),tblventasremisionesinventario.precio/tblventasremisionesinventario.cantidad*(tblventasremisionesinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idremision=tblventasremisionesinventario.idremision and tbldevolucionesdetalles.idinventario=tblventasremisionesinventario.idinventario and tbldevoluciones.estado=3),0))),tblventasremisionesinventario.descripcion,tblventasremisionesinventario.idmoneda,tblventasremisionesinventario.idalmacen,tblventasremisionesinventario.iva,tblventasremisionesinventario.extra,tblventasremisionesinventario.descuento,tblventasremisionesinventario.idvariante,tblventasremisionesinventario.idservicio," +
                "(tblventasremisionesinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idremision=tblventasremisionesinventario.idremision and tbldevolucionesdetalles.idinventario=tblventasremisionesinventario.idinventario and tbldevoluciones.estado=3),0)))*tblventasremisionesinventario.cantidadm/tblventasremisionesinventario.cantidad," + _
                "tblventasremisionesinventario.tipocantidadm,1,1,tblventasremisionesinventario.ieps,tblventasremisionesinventario.ivaretenido from tblventasremisionesinventario inner join tblinventario on tblventasremisionesinventario.idinventario=tblinventario.idinventario where tblinventario.inventariable=1 and idremision=" + pIdDocumento.ToString + " and (tblventasremisionesinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idremision=tblventasremisionesinventario.idremision and tbldevolucionesdetalles.idinventario=tblventasremisionesinventario.idinventario and tbldevoluciones.estado=3),0)))>0"
            Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tbldevolucionesproductos(iddevolucion,idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento) select " + Piddevolucion.ToString + ",idvariante,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento from tbldevolucionesremisionesproductos where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()

            'Comm.CommandText = "insert into tbldevolucionesservicios(iddevolucion,idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento) select " + Piddevolucion.ToString + ",idservicio,cantidad,precio,descripcion,idmoneda,iva,extra,descuento from tbldevolucionesremisionesservicios where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
            'Comm.CommandText = "update tblinventarioseries set iddevolucion=" + Piddevolucion.ToString + " where idremision=" + pIdDocumento.ToString
            'Comm.ExecuteNonQuery()
        End If

        If Tipo = 3 Then
            '(CDbl(TextBox5.Text) * Equivalenciab) / Equivalencia
            Comm.CommandText = "insert into tbldevolucionesdetalles(iddevolucion,idinventario,cantidad,precio,descripcion,idmoneda,idalmacen,iva,extra,descuento,idvariante,idservicio,cantidadm,tipocantidadm,equivalencia,equivalenciab,ieps,ivaretenido) select " + Piddevolucion.ToString + ",tblventasinventario.idinventario,tblventasinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idventa=tblventasinventario.idventa and tbldevolucionesdetalles.idinventario=tblventasinventario.idinventario and tbldevoluciones.estado=3),0)),tblventasinventario.precio/tblventasinventario.cantidad*(tblventasinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idventa=tblventasinventario.idventa and tbldevolucionesdetalles.idinventario=tblventasinventario.idinventario and tbldevoluciones.estado=3),0))),tblventasinventario.descripcion,tblventasinventario.idmoneda,tblventasinventario.idalmacen,tblventasinventario.iva,tblventasinventario.extra,tblventasinventario.descuento,tblventasinventario.idvariante,tblventasinventario.idservicio," + _
"(tblventasinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idventa=tblventasinventario.idventa and tbldevolucionesdetalles.idinventario=tblventasinventario.idinventario and tbldevoluciones.estado=3),0)))*tblventasinventario.cantidadm/tblventasinventario.cantidad," + _
"tblventasinventario.tipocantidadm,tblventasinventario.cantidad,tblventasinventario.cantidadm,tblventasinventario.ieps,tblventasinventario.ivaretenido from tblventasinventario inner join tblinventario on tblventasinventario.idinventario=tblinventario.idinventario where tblinventario.inventariable=1 and idventa=" + pIdDocumento.ToString + " and (tblventasinventario.cantidad-(ifnull((select sum(tbldevolucionesdetalles.cantidad) from tbldevolucionesdetalles inner join tbldevoluciones on tbldevolucionesdetalles.iddevolucion=tbldevoluciones.iddevolucion where tbldevoluciones.idventa=tblventasinventario.idventa and tbldevolucionesdetalles.idinventario=tblventasinventario.idinventario and tbldevoluciones.estado=3),0)))>0"
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
        Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,cantidad,0,1,1) from tbldevolucionesdetalles where iddevolucion=" + pId.ToString + ";"
        Comm.CommandText += "select spmodificainventariolotesf(tbldevolucionesdetalles.idinventario,tbldevolucionesdetalles.idalmacen,tbldevolucioneslotes.surtido,0,1,1,tbldevolucioneslotes.idlote) from tbldevolucioneslotes inner join tbldevolucionesdetalles on tbldevolucioneslotes.iddetalle=tbldevolucionesdetalles.iddetalle where tbldevolucionesdetalles.iddevolucion=" + pId.ToString + ";"
        Comm.CommandText += "select spmodificainventarioaduanaf(tbldevolucionesdetalles.idinventario,tbldevolucionesdetalles.idalmacen,tbldevolucionesaduana.surtido,0,1,1,tbldevolucionesaduana.idaduana) from tbldevolucionesaduana inner join tbldevolucionesdetalles on tbldevolucionesaduana.iddetalle=tbldevolucionesdetalles.iddetalle where tbldevolucionesdetalles.iddevolucion=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
        'Comm.CommandText = "update tbldevolucionesdetallesc set surtido=0 where iddevolucion=" + pId.ToString + ";"
        Comm.CommandText = "update tbldevolucioneslotes inner join tbldevolucionesdetalles on tbldevolucioneslotes.iddetalle=tbldevolucionesdetalles.iddetalle set tbldevolucioneslotes.surtido=0 where tbldevolucionesdetalles.iddevolucion=" + pId.ToString + ";"
        Comm.CommandText += "update tbldevolucionesaduana inner join tbldevolucionesdetalles on tbldevolucionesaduana.iddetalle=tbldevolucionesdetalles.iddetalle set tbldevolucionesaduana.surtido=0 where tbldevolucionesdetalles.iddevolucion=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
    End Sub

    Public Sub ModificaInventario(ByVal pId As Integer)
        
        Comm.CommandText = "select spmodificainventarioi(idinventario,idalmacen,cantidad,0,0,1) from tbldevolucionesdetalles where iddevolucion=" + pId.ToString + ";"
        'Comm.CommandText += "update tbldevolucionesdetallesc set surtido=cantidad where iddevolucion=" + pId.ToString + "; "
        Comm.CommandText += "select spmodificainventariolotesf(tbldevolucionesdetalles.idinventario,tbldevolucionesdetalles.idalmacen,tbldevolucioneslotes.cantidad-tbldevolucioneslotes.surtido,0,0,1,tbldevolucioneslotes.idlote) from tbldevolucioneslotes inner join tbldevolucionesdetalles on tbldevolucioneslotes.iddetalle = tbldevolucionesdetalles.iddetalle where tbldevolucionesdetalles.iddevolucion=" + pId.ToString + "; "
        Comm.CommandText += "select spmodificainventarioaduanaf(tbldevolucionesdetalles.idinventario,tbldevolucionesdetalles.idalmacen,tbldevolucionesaduana.cantidad-tbldevolucionesaduana.surtido,0,0,1,tbldevolucionesaduana.idaduana) from tbldevolucionesaduana inner join tbldevolucionesdetalles on tbldevolucionesaduana.iddetalle = tbldevolucionesdetalles.iddetalle where tbldevolucionesdetalles.iddevolucion=" + pId.ToString + "; "
        Comm.CommandText += "update tbldevolucioneslotes inner join tbldevolucionesdetalles on tbldevolucioneslotes.iddetalle=tbldevolucionesdetalles.iddetalle set tbldevolucioneslotes.surtido=tbldevolucioneslotes.cantidad where tbldevolucionesdetalles.iddevolucion=" + pId.ToString + ";"
        Comm.CommandText += "update tbldevolucionesaduana inner join tbldevolucionesdetalles on tbldevolucionesaduana.iddetalle=tbldevolucionesdetalles.iddetalle set tbldevolucionesaduana.surtido=tbldevolucionesaduana.cantidad where tbldevolucionesdetalles.iddevolucion=" + pId.ToString + ";"
        Comm.ExecuteNonQuery()
        
    End Sub
    

    Public Function CreaCadenaOriginal(ByVal piddevolucion As Integer, ByVal pIdMoneda As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|2.0|"
        ID = piddevolucion
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, 2)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        CO += Serie + "|"
        CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        CO += NoAprobacion + "|"
        CO += YearAprobacion + "|"
        CO += "egreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        CO += Format(Subtototal, "#0.00") + "|"
        CO += "0|" 'descuento

        CO += Format(TotalVenta, "#0.00") + "|" ' total factura con iva

        CO += Trim(Sucursal.RFC) + "|"
        CO += Trim(Sucursal.NombreFiscal) + "|"
        CO += Trim(Sucursal.Direccion) + "|"
        CO += Trim(Sucursal.NoExterior) + "|"
        CO += Trim(Sucursal.NoInterior) + "|"
        CO += Trim(Sucursal.Colonia) + "|"
        CO += Trim(Sucursal.Ciudad) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio) + "|"
        CO += Trim(Sucursal.Municipio) + "|"
        CO += Trim(Sucursal.Estado) + "|"
        CO += Trim(Sucursal.Pais) + "|"
        CO += Trim(Sucursal.CP) + "|"

        CO += Trim(Sucursal.Direccion2) + "|"
        CO += Trim(Sucursal.NoExterior2) + "|"
        CO += Trim(Sucursal.NoInterior2) + "|"
        CO += Trim(Sucursal.Colonia2) + "|"
        CO += Trim(Sucursal.Ciudad2) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio2) + "|"
        CO += Trim(Sucursal.Municipio2) + "|"
        CO += Trim(Sucursal.Estado2) + "|"
        CO += Trim(Sucursal.Pais2) + "|"
        CO += Trim(Sucursal.CP2) + "|"

        CO += Trim(Cliente.RFC) + "|"
        CO += Trim(Cliente.Nombre) + "|"
        If Cliente.DireccionFiscal = 0 Then
            CO += Trim(Cliente.Direccion) + "|"
            CO += Trim(Cliente.NoExterior) + "|"
            CO += Trim(Cliente.NoInterior) + "|"
            CO += Trim(Cliente.Colonia) + "|"
            CO += Trim(Cliente.Ciudad) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio) + "|"
            CO += Trim(Cliente.Municipio) + "|"
            CO += Trim(Cliente.Estado) + "|"
            CO += Trim(Cliente.Pais) + "|"
            CO += Trim(Cliente.CP) + "|"
        Else
            CO += Trim(Cliente.Direccion2) + "|"
            CO += Trim(Cliente.NoExterior2) + "|"
            CO += Trim(Cliente.NoInterior2) + "|"
            CO += Trim(Cliente.Colonia2) + "|"
            CO += Trim(Cliente.Ciudad2) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio2) + "|"
            CO += Trim(Cliente.Municipio2) + "|"
            CO += Trim(Cliente.Estado2) + "|"
            CO += Trim(Cliente.Pais2) + "|"
            CO += Trim(Cliente.CP2) + "|"
        End If

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbDevolucionesDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        'Dim PrecioTemp As Double
        While DR.Read
            CO += DR("cantidad").ToString + "|"
            CO += DR("tipocantidad") + "|"
            'CO += DR("clave") + "|"
            CO += Trim(DR("descripcion")) + "|"
            If DR("idmoneda") <> 2 Then
                CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
                CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
            Else
                CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
                CO += Format(DR("precio"), "#0.00") + "|"
            End If

        End While
        DR.Close()

        'Dim VP As New dbVentasProductos(MySqlcon)
        'DR = VP.ConsultaReader(ID)

        'While DR.Read
        '    CO += DR("cantidad").ToString + "|"
        '    CO += DR("tipocantidad") + "|"
        '    'CO += DR("clave") + "|"
        '    CO += DR("descripcion") + "|"
        '    If DR("idmoneda") <> 2 Then
        '        CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
        '        CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
        '    Else
        '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
        '        CO += Format(DR("precio"), "#0.00") + "|"
        '    End If
        'End While
        'DR.Close()

        'Dim VS As New dbVentasServicios(MySqlcon)
        'DR = VS.ConsultaReader(ID)

        'While DR.Read
        '    CO += DR("cantidad").ToString + "|"
        '    CO += "SERV|"
        '    CO += DR("folio") + "|"
        '    CO += DR("descripcion") + "|"
        '    If DR("idmoneda") <> 2 Then
        '        CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
        '        CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
        '    Else
        '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
        '        CO += Format(DR("precio"), "#0.00") + "|"
        '    End If
        'End While
        'DR.Close()

        If ISR <> 0 Then
            CO += "ISR|" + Format(TotalISR, "#0.00") + "|"
        End If
        If IvaRetenido <> 0 Then
            CO += "IVA|" + Format(TotalIvaRetenido, "#0.00") + "|"
        End If
        If ISR <> 0 Or IvaRetenido <> 0 Then
            CO += Format(TotalISR + TotalIvaRetenido, "#0.00") + "|"
        End If
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                Else
                    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                Else
                    IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                End If
            End If
        End While
        DR.Close()
        For Each I As Double In Ivas
            CO += "IVA|"
            CO += CInt(I).ToString + "|"
            CO += Format(IvasImporte(I.ToString), "#0.00") + "|"
        Next

        CO += Format(TotalIva, "#0.00") + "|"
        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        Return CO

    End Function
    Public Function CreaCadenaOriginal22(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|2.2|"
        ID = pIdVenta
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, IdConversion)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        CO += Serie + "|"
        CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        CO += NoAprobacion + "|"
        CO += YearAprobacion + "|"
        CO += "egreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        CO += Format(Subtototal, "#0.00") + "|"
        CO += "0|" 'descuento

        CO += Format(TotalVenta, "#0.00") + "|" ' total factura con iva

        'metododepago
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        'CO += FP.Nombre + "|"
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """"
        'Else
        CO += "No identificado|"
        'End If
        'lugar de expedicion
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            CO += Trim(Sucursal.Ciudad2) + "," + Trim(Sucursal.Estado2) + "|"
        Else
            CO += Trim(Sucursal.Ciudad) + "," + Trim(Sucursal.Estado) + "|"
        End If
        'If NoCuenta <> "" And FP.Tipo = dbFormasdePago.Tipos.Contado Then CO += NoCuenta + "|"
        'Tipo de cambio

        If IdConversion <> 2 Then
            CO += Format(TipodeCambio, "#0.00") + "|"
            Dim Moneda As New dbMonedas(IdConversion, Comm.Connection)
            CO += Moneda.Abreviatura + "|"
        Else
            CO += "MXN|"
        End If

        'Aqui lo de parcialidades
        'proximamente

        CO += Trim(Sucursal.RFC) + "|"
        CO += Trim(Sucursal.NombreFiscal) + "|"
        CO += Trim(Sucursal.Direccion) + "|"
        CO += Trim(Sucursal.NoExterior) + "|"
        CO += Trim(Sucursal.NoInterior) + "|"
        CO += Trim(Sucursal.Colonia) + "|"
        CO += Trim(Sucursal.Ciudad) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio) + "|"
        CO += Trim(Sucursal.Municipio) + "|"
        CO += Trim(Sucursal.Estado) + "|"
        CO += Trim(Sucursal.Pais) + "|"
        CO += Trim(Sucursal.CP) + "|"

        CO += Trim(Sucursal.Direccion2) + "|"
        CO += Trim(Sucursal.NoExterior2) + "|"
        CO += Trim(Sucursal.NoInterior2) + "|"
        CO += Trim(Sucursal.Colonia2) + "|"
        CO += Trim(Sucursal.Ciudad2) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio2) + "|"
        CO += Trim(Sucursal.Municipio2) + "|"
        CO += Trim(Sucursal.Estado2) + "|"
        CO += Trim(Sucursal.Pais2) + "|"
        CO += Trim(Sucursal.CP2) + "|"


        'Regimen fiscal

        Dim Pos As Integer = 0
        Dim Listo As Boolean = False
        Dim AddDir As String = ""

        While Sucursal.RegimenFiscal.Length > Pos
            If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
                CO += Trim(AddDir) + "|"
                AddDir = ""
                Listo = True
            Else
                AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
                Listo = False
            End If
            Pos += 1
        End While

        If Listo = False Then CO += Trim(AddDir) + "|"

        CO += Trim(Cliente.RFC) + "|"
        CO += Trim(Cliente.Nombre) + "|"
        If Cliente.DireccionFiscal = 0 Then
            CO += Trim(Cliente.Direccion) + "|"
            CO += Trim(Cliente.NoExterior) + "|"
            CO += Trim(Cliente.NoInterior) + "|"
            CO += Trim(Cliente.Colonia) + "|"
            CO += Trim(Cliente.Ciudad) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio) + "|"
            CO += Trim(Cliente.Municipio) + "|"
            CO += Trim(Cliente.Estado) + "|"
            CO += Trim(Cliente.Pais) + "|"
            CO += Trim(Cliente.CP) + "|"
        Else
            CO += Trim(Cliente.Direccion2) + "|"
            CO += Trim(Cliente.NoExterior2) + "|"
            CO += Trim(Cliente.NoInterior2) + "|"
            CO += Trim(Cliente.Colonia2) + "|"
            CO += Trim(Cliente.Ciudad2) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio2) + "|"
            CO += Trim(Cliente.Municipio2) + "|"
            CO += Trim(Cliente.Estado2) + "|"
            CO += Trim(Cliente.Pais2) + "|"
            CO += Trim(Cliente.CP2) + "|"
        End If

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbDevolucionesDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        'Dim PrecioTemp As Double
        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                CO += DR("cantidad").ToString + "|"
                CO += DR("tipocantidad") + "|"
                'CO += DR("clave") + "|"
                CO += Trim(DR("descripcion")) + "|"
                'If DR("idmoneda") <> 2 Then
                'CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
                'CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
                'Else
                CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
                CO += Format(DR("precio"), "#0.00") + "|"
                'End If
                'informacion aduanera proximamente
            End If
        End While
        DR.Close()



        If ISR <> 0 Then
            CO += "ISR|" + Format(TotalISR, "#0.00") + "|"
        End If
        If IvaRetenido <> 0 Then
            CO += "IVA|" + Format(TotalIvaRetenido, "#0.00") + "|"
        End If
        If ISR <> 0 Or IvaRetenido <> 0 Then
            CO += Format(TotalISR + TotalIvaRetenido, "#0.00") + "|"
        End If
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                'If DR("idmoneda") <> 2 Then
                '  IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                'Else
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                'End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                'If DR("idmoneda") <> 2 Then
                'IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                'Else
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
            End If
            'End If
        End While
        DR.Close()
        For Each I As Double In Ivas
            CO += "IVA|"
            CO += Format(I, "#0.00") + "|"
            CO += Format(IvasImporte(I.ToString), "#0.00") + "|"
        Next

        CO += Format(TotalIva, "#0.00") + "|"
        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        Return CO

    End Function
    Public Function CreaCadenaOriginali32(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|3.2|"
        ID = pIdVenta
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, IdConversion)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        'CO += Serie + "|"
        'CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        'CO += NoAprobacion + "|"
        'CO += YearAprobacion + "|"
        CO += "egreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        CO += Format(Subtototal, "#0.00####") + "|"
        CO += "0|" 'descuento
        'Tipo de cambio
        If IdConversion <> 2 Then
            CO += Format(TipodeCambio, "#0.00####") + "|"
            Dim Moneda As New dbMonedas(IdConversion, Comm.Connection)
            CO += Moneda.Abreviatura + "|"
        Else
            CO += "MXN|"
        End If
        CO += Format(TotalVenta, "#0.00####") + "|" ' total factura con iva

        'metododepago
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        'CO += Trim(FP.Nombre) + "|"
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """"
        'Else
        If Fecha < "2016/06/01" Then
            CO += "No identificado|"
        Else
            CO += "NA|"
        End If
        'End If
        'lugar de expedicion
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            CO += Trim(Sucursal.Ciudad2) + "," + Trim(Sucursal.Estado2) + "|"
        Else
            CO += Trim(Sucursal.Ciudad) + "," + Trim(Sucursal.Estado) + "|"
        End If
        'If NoCuenta <> "" And FP.Tipo = dbFormasdePago.Tipos.Contado Then CO += Trim(NoCuenta) + "|"




        'Aqui lo de parcialidades
        'proximamente

        CO += Trim(Sucursal.RFC) + "|"
        CO += Trim(Sucursal.NombreFiscal) + "|"
        CO += Trim(Sucursal.Direccion) + "|"
        CO += Trim(Sucursal.NoExterior) + "|"
        CO += Trim(Sucursal.NoInterior) + "|"
        CO += Trim(Sucursal.Colonia) + "|"
        CO += Trim(Sucursal.Ciudad) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio) + "|"
        CO += Trim(Sucursal.Municipio) + "|"
        CO += Trim(Sucursal.Estado) + "|"
        CO += Trim(Sucursal.Pais) + "|"
        CO += Trim(Sucursal.CP) + "|"

        CO += Trim(Sucursal.Direccion2) + "|"
        CO += Trim(Sucursal.NoExterior2) + "|"
        CO += Trim(Sucursal.NoInterior2) + "|"
        CO += Trim(Sucursal.Colonia2) + "|"
        CO += Trim(Sucursal.Ciudad2) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio2) + "|"
        CO += Trim(Sucursal.Municipio2) + "|"
        CO += Trim(Sucursal.Estado2) + "|"
        CO += Trim(Sucursal.Pais2) + "|"
        CO += Trim(Sucursal.CP2) + "|"

        'regimen

        Dim Pos As Integer = 0
        Dim Listo As Boolean = False
        Dim AddDir As String = ""

        While Sucursal.RegimenFiscal.Length > Pos
            If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
                CO += Trim(AddDir) + "|"
                AddDir = ""
                Listo = True
            Else
                AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
                Listo = False
            End If
            Pos += 1
        End While

        If Listo = False Then CO += Trim(AddDir) + "|"

        CO += Trim(Cliente.RFC) + "|"
        CO += Trim(Cliente.Nombre) + "|"
        If Cliente.DireccionFiscal = 0 Then
            CO += Trim(Cliente.Direccion) + "|"
            CO += Trim(Cliente.NoExterior) + "|"
            CO += Trim(Cliente.NoInterior) + "|"
            CO += Trim(Cliente.Colonia) + "|"
            CO += Trim(Cliente.Ciudad) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio) + "|"
            CO += Trim(Cliente.Municipio) + "|"
            CO += Trim(Cliente.Estado) + "|"
            CO += Trim(Cliente.Pais) + "|"
            CO += Trim(Cliente.CP) + "|"
        Else
            CO += Trim(Cliente.Direccion2) + "|"
            CO += Trim(Cliente.NoExterior2) + "|"
            CO += Trim(Cliente.NoInterior2) + "|"
            CO += Trim(Cliente.Colonia2) + "|"
            CO += Trim(Cliente.Ciudad2) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio2) + "|"
            CO += Trim(Cliente.Municipio2) + "|"
            CO += Trim(Cliente.Estado2) + "|"
            CO += Trim(Cliente.Pais2) + "|"
            CO += Trim(Cliente.CP2) + "|"
        End If

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbDevolucionesDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        'Dim PrecioTemp As Double
        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                CO += DR("cantidad").ToString + "|"
                CO += DR("tipocantidad") + "|"
                'CO += DR("clave") + "|"
                CO += Trim(DR("descripcion")) + "|"
                'If DR("idmoneda") <> 2 Then
                '    CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
                '    CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
                'Else
                CO += Format(DR("precio") / DR("cantidad"), "#0.00####") + "|"
                CO += Format(DR("precio"), "#0.00####") + "|"
                'End If
            End If
        End While
        DR.Close()

        If ISR <> 0 Then
            CO += "ISR|" + Format(TotalISR, "#0.00####") + "|"
        End If
        If IvaRetenido <> 0 Then
            CO += "IVA|" + Format(TotalIvaRetenido, "#0.00####") + "|"
        End If
        If ISR <> 0 Or IvaRetenido <> 0 Then
            CO += Format(TotalISR + TotalIvaRetenido, "#0.00####") + "|"
        End If
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                'Else
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                ' End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                'Else
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                'End If
            End If
        End While
        DR.Close()
        For Each I As Double In Ivas
            CO += "IVA|"
            CO += Format(I, "#0.00####") + "|"
            CO += Format(IvasImporte(I.ToString), "#0.00####") + "|"
        Next

        CO += Format(TotalIva, "#0.00####") + "|"
        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        Return CO

    End Function

    Public Function CreaCadenaOriginali(ByVal piddevolucion As Integer, ByVal pIdMoneda As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim CO As String = "|3.0|"
        ID = piddevolucion
        LlenaDatos()
        'Dim TI As Double
        If TipodeCambio = 0 Then TipodeCambio = 1
        'Dim CI As Double
        DaTotal(ID, 2)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'CI = TI * (Iva / 100)
        'CO += Serie + "|"
        'CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"
        'CO += NoAprobacion + "|"
        'CO += YearAprobacion + "|"
        CO += "egreso|Pago en una sola exhibición|"
        'CO += Format(TI - (TI - (TI / (1 + (Iva / 100)))), "#0.00") + "|" 'Total sin Iva
        CO += Format(Subtototal, "#0.00") + "|"
        CO += "0|" 'descuento

        CO += Format(TotalVenta, "#0.00") + "|" ' total factura con iva

        CO += Trim(Sucursal.RFC) + "|"
        CO += Trim(Sucursal.NombreFiscal) + "|"
        CO += Trim(Sucursal.Direccion) + "|"
        CO += Trim(Sucursal.NoExterior) + "|"
        CO += Trim(Sucursal.NoInterior) + "|"
        CO += Trim(Sucursal.Colonia) + "|"
        CO += Trim(Sucursal.Ciudad) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio) + "|"
        CO += Trim(Sucursal.Municipio) + "|"
        CO += Trim(Sucursal.Estado) + "|"
        CO += Trim(Sucursal.Pais) + "|"
        CO += Trim(Sucursal.CP) + "|"

        CO += Trim(Sucursal.Direccion2) + "|"
        CO += Trim(Sucursal.NoExterior2) + "|"
        CO += Trim(Sucursal.NoInterior2) + "|"
        CO += Trim(Sucursal.Colonia2) + "|"
        CO += Trim(Sucursal.Ciudad2) + "|"
        CO += Trim(Sucursal.ReferenciaDomicilio2) + "|"
        CO += Trim(Sucursal.Municipio2) + "|"
        CO += Trim(Sucursal.Estado2) + "|"
        CO += Trim(Sucursal.Pais2) + "|"
        CO += Trim(Sucursal.CP2) + "|"

        CO += Trim(Cliente.RFC) + "|"
        CO += Trim(Cliente.Nombre) + "|"
        If Cliente.DireccionFiscal = 0 Then
            CO += Trim(Cliente.Direccion) + "|"
            CO += Trim(Cliente.NoExterior) + "|"
            CO += Trim(Cliente.NoInterior) + "|"
            CO += Trim(Cliente.Colonia) + "|"
            CO += Trim(Cliente.Ciudad) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio) + "|"
            CO += Trim(Cliente.Municipio) + "|"
            CO += Trim(Cliente.Estado) + "|"
            CO += Trim(Cliente.Pais) + "|"
            CO += Trim(Cliente.CP) + "|"
        Else
            CO += Trim(Cliente.Direccion2) + "|"
            CO += Trim(Cliente.NoExterior2) + "|"
            CO += Trim(Cliente.NoInterior2) + "|"
            CO += Trim(Cliente.Colonia2) + "|"
            CO += Trim(Cliente.Ciudad2) + "|"
            CO += Trim(Cliente.ReferenciaDomicilio2) + "|"
            CO += Trim(Cliente.Municipio2) + "|"
            CO += Trim(Cliente.Estado2) + "|"
            CO += Trim(Cliente.Pais2) + "|"
            CO += Trim(Cliente.CP2) + "|"
        End If

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbDevolucionesDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        'Dim PrecioTemp As Double
        While DR.Read
            CO += DR("cantidad").ToString + "|"
            CO += DR("tipocantidad") + "|"
            'CO += DR("clave") + "|"
            CO += Trim(DR("descripcion")) + "|"
            If DR("idmoneda") <> 2 Then
                CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
                CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
            Else
                CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
                CO += Format(DR("precio"), "#0.00") + "|"
            End If

        End While
        DR.Close()

        'Dim VP As New dbVentasProductos(MySqlcon)
        'DR = VP.ConsultaReader(ID)

        'While DR.Read
        '    CO += DR("cantidad").ToString + "|"
        '    CO += DR("tipocantidad") + "|"
        '    'CO += DR("clave") + "|"
        '    CO += DR("descripcion") + "|"
        '    If DR("idmoneda") <> 2 Then
        '        CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
        '        CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
        '    Else
        '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
        '        CO += Format(DR("precio"), "#0.00") + "|"
        '    End If
        'End While
        'DR.Close()

        'Dim VS As New dbVentasServicios(MySqlcon)
        'DR = VS.ConsultaReader(ID)

        'While DR.Read
        '    CO += DR("cantidad").ToString + "|"
        '    CO += "SERV|"
        '    CO += DR("folio") + "|"
        '    CO += DR("descripcion") + "|"
        '    If DR("idmoneda") <> 2 Then
        '        CO += Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + "|"
        '        CO += Format((DR("precio") * TipodeCambio), "#0.00") + "|"
        '    Else
        '        CO += Format(DR("precio") / DR("cantidad"), "#0.00") + "|"
        '        CO += Format(DR("precio"), "#0.00") + "|"
        '    End If
        'End While
        'DR.Close()

        If ISR <> 0 Then
            CO += "ISR|" + Format(TotalISR, "#0.00") + "|"
        End If
        If IvaRetenido <> 0 Then
            CO += "IVA|" + Format(TotalIvaRetenido, "#0.00") + "|"
        End If
        If ISR <> 0 Or IvaRetenido <> 0 Then
            CO += Format(TotalISR + TotalIvaRetenido, "#0.00") + "|"
        End If
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                Else
                    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                Else
                    IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                End If
            End If
        End While
        DR.Close()
        For Each I As Double In Ivas
            CO += "IVA|"
            CO += CInt(I).ToString + "|"
            CO += Format(IvasImporte(I.ToString), "#0.00") + "|"
        Next

        CO += Format(TotalIva, "#0.00") + "|"
        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        Return CO

    End Function



    Public Function CreaXML(ByVal piddevolucion As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pidEmpresa As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

        XMLDoc += "<Comprobante " + vbCrLf
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pidEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = piddevolucion
        LlenaDatos()
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, 2)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "version = ""2.0""" + vbCrLf
        XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf

        XMLDoc += "subTotal=""" + Format(Subtototal, "#0.00") + """" + vbCrLf

        XMLDoc += "total=""" + Format(TotalVenta, "#0.00") + """" + vbCrLf
        If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf
        XMLDoc += "descuento=""" + "0" + """" + vbCrLf
        XMLDoc += "tipoDeComprobante=""egreso""" + vbCrLf
        XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf
        XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
        XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf

        XMLDoc += ">"

        XMLDoc += "<Emisor rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf

        XMLDoc += "<DomicilioFiscal " + vbCrLf
        If Sucursal.Direccion <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "/>" + vbCrLf
        If Sucursal.Pais2 <> "" Then
            XMLDoc += "<ExpedidoEn  " + vbCrLf


            If Sucursal.Direccion2 <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf

            'If O._CalleLocal <> "" Then XMLDoc += "calle=""" + O._CalleLocal + """" + vbCrLf
            'If O._noExteriorLocal <> "" Then XMLDoc += "noExterior=""" + O._noExteriorLocal + """" + vbCrLf
            'If O._noInteriorLocal <> "" Then XMLDoc += "noInterior=""" + O._noInteriorLocal + """" + vbCrLf
            'If O._ColoniaLocal <> "" Then XMLDoc += "colonia=""" + O._ColoniaLocal + """" + vbCrLf
            'If O._LocalidadLocal <> "" Then XMLDoc += "localidad=""" + O._LocalidadLocal + """" + vbCrLf
            'If O._ReferenciaDomicilioLocal <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilioLocal + """" + vbCrLf
            'If O._MunicipioLocal <> "" Then XMLDoc += "municipio=""" + O._MunicipioLocal + """" + vbCrLf
            'If O._EstadoLocal <> "" Then XMLDoc += "estado=""" + O._EstadoLocal + """" + vbCrLf
            'If O._PaisLocal <> "" Then XMLDoc += "pais=""" + O._PaisLocal + """" + vbCrLf
            'If O._CodigoPostalLocal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostalLocal + """"
            XMLDoc += "/>" + vbCrLf
        End If

        XMLDoc += "</Emisor>" + vbCrLf


        XMLDoc += "<Receptor rfc=""" + Replace(Replace(Replace(Replace(Replace(Cliente.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf
        XMLDoc += "<Domicilio "
        If Cliente.DireccionFiscal = 0 Then
            If Cliente.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        Else
            If Cliente.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        End If
        XMLDoc += "/>" + vbCrLf

        XMLDoc += "</Receptor>" + vbCrLf

        XMLDoc += "<Conceptos>" + vbCrLf

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbDevolucionesDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)

        While DR.Read
            XMLDoc += "<Concepto " + vbCrLf
            XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
            XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(DR("descripcion"), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If DR("idmoneda") <> 2 Then
                XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
                XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
                XMLDoc += "/> " + vbCrLf
            Else
                XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
                XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
                XMLDoc += "/> " + vbCrLf
            End If

        End While
        DR.Close()

        'Dim VP As New dbVentasProductos(MySqlcon)
        'DR = VP.ConsultaReader(ID)

        'While DR.Read

        '    XMLDoc += "<Concepto " + vbCrLf
        '    XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
        '    XMLDoc += "unidad=""" + DR("tipocantidad") + """" + vbCrLf
        '    XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
        '    If DR("idmoneda") <> 2 Then

        '        XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    Else
        '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    End If
        '    XMLDoc += "/> " + vbCrLf
        'End While
        'DR.Close()

        'Dim VS As New dbVentasServicios(MySqlcon)
        'DR = VS.ConsultaReader(ID)

        'While DR.Read

        '    XMLDoc += "<Concepto " + vbCrLf
        '    XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
        '    XMLDoc += "unidad=""SERV""" + vbCrLf
        '    XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
        '    If DR("idmoneda") <> 2 Then
        '        XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    Else
        '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    End If
        '    XMLDoc += "/> " + vbCrLf
        'End While
        'DR.Close()
        XMLDoc += "</Conceptos>"

        XMLDoc += "<Impuestos totalImpuestosTrasladados=""" + Format(TotalIva, "#0.00") + """ "
        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00") + """"
        End If
        XMLDoc += ">" + vbCrLf

        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "<Retenciones>" + vbCrLf
            If ISR <> 0 Then
                XMLDoc += "<Retencion impuesto=""ISR""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalISR, "#0.00") + """ />" + vbCrLf
            End If

            If IvaRetenido <> 0 Then
                XMLDoc += "<Retencion impuesto=""IVA""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00") + """ />" + vbCrLf
            End If

            XMLDoc += "</Retenciones>" + vbCrLf

        End If



        XMLDoc += "<Traslados>" + vbCrLf


        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                Else
                    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                Else
                    IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                End If

            End If
        End While
        DR.Close()
        For Each I As Double In Ivas

            XMLDoc += "<Traslado impuesto=""IVA""" + vbCrLf
            XMLDoc += "tasa=""" + I.ToString + """" + vbCrLf
            XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00") + """ />" + vbCrLf

        Next



        XMLDoc += "</Traslados>" + vbCrLf





        XMLDoc += "</Impuestos>" + vbCrLf
        XMLDoc += "</Comprobante>"


        Return XMLDoc

    End Function
    Public Function CreaXML22(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

        XMLDoc += "<Comprobante " + vbCrLf
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdConversion)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        XMLDoc += "version=""2.2""" + vbCrLf
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
        If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf

        XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf

        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf
        XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf


        XMLDoc += "subTotal=""" + Format(Subtototal, "#0.00") + """" + vbCrLf
        XMLDoc += "descuento=""" + "0" + """" + vbCrLf
        'Nuevo
        If IdConversion <> 2 Then
            XMLDoc += "TipoCambio=""" + Format(TipodeCambio, "#0.00") + """" + vbCrLf
            Dim Moneda As New dbMonedas(IdConversion, Comm.Connection)
            XMLDoc += "Moneda=""" + Moneda.Abreviatura + """" + vbCrLf
        Else
            XMLDoc += "Moneda=""MXN""" + vbCrLf
        End If
        XMLDoc += "total=""" + Format(TotalVenta, "#0.00") + """" + vbCrLf



        XMLDoc += "tipoDeComprobante=""egreso""" + vbCrLf
        'Nuevo
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        'XMLDoc += "metodoDePago=""" + FP.Nombre + """" + vbCrLf
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """" + vbCrLf
        'Else
        XMLDoc += "metodoDePago=""No identificado""" + vbCrLf
        'End If
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad2 + "," + Sucursal.Estado2 + """" + vbCrLf
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad + "," + Sucursal.Estado + """" + vbCrLf
        End If
        'Proximamente
        'XMLDoc += "FolioFiscalOrig="
        'XMLDoc += "FechaFolioFiscalOrig="
        'XMLDoc += "MontoFolioFiscalOrig="

        '-------------------------
        XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv22.xsd""" + vbCrLf
        XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf
        XMLDoc += ">"

        XMLDoc += "<Emisor rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf

        XMLDoc += "<DomicilioFiscal " + vbCrLf
        If Sucursal.Direccion <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "/>" + vbCrLf
        If Sucursal.Pais2 <> "" Then
            XMLDoc += "<ExpedidoEn  " + vbCrLf
            If Sucursal.Direccion2 <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            XMLDoc += "/>" + vbCrLf
        End If

        'Aqui va el regimen fiscal
        Dim Pos As Integer = 0
        Dim Listo As Boolean = False
        Dim AddDir As String = ""

        While Sucursal.RegimenFiscal.Length > Pos
            If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
                XMLDoc += "<RegimenFiscal Regimen=""" + AddDir + """ />"
                AddDir = ""
                Listo = True
            Else
                AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
                Listo = False
            End If
            Pos += 1
        End While

        If Listo = False Then XMLDoc += "<RegimenFiscal Regimen=""" + AddDir + """ /> "


        XMLDoc += "</Emisor>" + vbCrLf


        XMLDoc += "<Receptor rfc=""" + Replace(Replace(Replace(Replace(Replace(Cliente.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf
        XMLDoc += "<Domicilio "
        If Cliente.DireccionFiscal = 0 Then
            If Cliente.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        Else
            If Cliente.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        End If
        XMLDoc += "/>" + vbCrLf

        XMLDoc += "</Receptor>" + vbCrLf

        XMLDoc += "<Conceptos>" + vbCrLf

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbDevolucionesDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)

        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                XMLDoc += "<Concepto " + vbCrLf
                XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
                XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
                'noIdentificacion por si se llega a usar
                Dim Des As String
                Des = Trim(DR("descripcion"))
                While Des.IndexOf("  ") <> -1
                    Des = Replace(Des, "  ", " ")
                End While
                Des = Replace(Des, vbTab, "")
                XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trim(Des), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
                'If DR("idmoneda") <> 2 Then
                '    XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
                '    XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
                '    XMLDoc += "/> " + vbCrLf
                'Else
                XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
                XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
                XMLDoc += "/> " + vbCrLf

                'Informacion aduanera
                '<InformacionAduanera numero=string fecha=aaaa-mm-dd aduana=string />
                'Cuenta Predia
                '<CuentaPredial numero=string />
                'Complemento concepto
                '<ComplementoConcepto ??? />
                'Parte
                '< />
                'End If
            End If
        End While
        DR.Close()

        XMLDoc += "</Conceptos>"

        XMLDoc += "<Impuestos totalImpuestosTrasladados=""" + Format(TotalIva, "#0.00") + """ "
        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00") + """"
        End If
        XMLDoc += ">" + vbCrLf

        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "<Retenciones>" + vbCrLf
            If ISR <> 0 Then
                XMLDoc += "<Retencion impuesto=""ISR""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalISR, "#0.00") + """ />" + vbCrLf
            End If

            If IvaRetenido <> 0 Then
                XMLDoc += "<Retencion impuesto=""IVA""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00") + """ />" + vbCrLf
            End If

            XMLDoc += "</Retenciones>" + vbCrLf

        End If



        XMLDoc += "<Traslados>" + vbCrLf


        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                'Else
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                'End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                'If DR("idmoneda") <> 2 Then
                'IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                'Else
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                'End If

            End If
        End While
        DR.Close()
        For Each I As Double In Ivas

            XMLDoc += "<Traslado impuesto=""IVA""" + vbCrLf
            XMLDoc += "tasa=""" + Format(I, "#0.00") + """" + vbCrLf
            XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00") + """ />" + vbCrLf

        Next



        XMLDoc += "</Traslados>" + vbCrLf





        XMLDoc += "</Impuestos>" + vbCrLf
        XMLDoc += "</Comprobante>"


        Return XMLDoc

    End Function
    Public Function CreaXMLi(ByVal piddevolucion As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

        'XMLDoc += "<Comprobante " + vbCrLf

        'en.Leex509(My.Settings.rutacer)
        'ID = piddevolucion
        'LlenaDatos()
        'If TipodeCambio = 0 Then TipodeCambio = 1
        'DaTotal(ID, 2)
        'Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        'If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        'XMLDoc += "version = ""2.0""" + vbCrLf
        'XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
        'XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
        'If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
        'If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf

        'XMLDoc += "subTotal=""" + Format(Subtototal, "#0.00") + """" + vbCrLf

        'XMLDoc += "total=""" + Format(TotalVenta, "#0.00") + """" + vbCrLf
        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        'XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf
        'XMLDoc += "descuento=""" + "0" + """" + vbCrLf
        'XMLDoc += "tipoDeComprobante=""egreso""" + vbCrLf
        'XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf
        'XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
        'XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        'XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf

        'XMLDoc += ">"

        XMLDoc += "<cfdi:Comprobante " + vbCrLf
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = piddevolucion
        LlenaDatos()
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, 2)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "version = ""3.0""" + vbCrLf
        XMLDoc += "folio=""" + Folio.ToString + """" + vbCrLf
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """" + vbCrLf
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """" + vbCrLf
        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """" + vbCrLf

        XMLDoc += "subTotal=""" + Format(Subtototal, "#0.00") + """" + vbCrLf

        XMLDoc += "total=""" + Format(TotalVenta, "#0.00") + """" + vbCrLf
        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        XMLDoc += "formaDePago=""Pago en una sola exhibición""" + vbCrLf
        XMLDoc += "descuento=""" + "0" + """" + vbCrLf
        XMLDoc += "tipoDeComprobante=""egreso""" + vbCrLf
        XMLDoc += "certificado=""" + en.Certificado64 + """" + vbCrLf
        'XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
        'XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        'XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf
        '++++++++++++++++++++++++++++
        XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:ecb=""http://www.sat.gob.mx/ecb"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:implocal=""http://www.sat.gob.mx/implocal"" xmlns:bfa2=""http://www.buzonfiscal.com/ns/addenda/bf/2""" + vbCrLf
        XMLDoc += "xmlns:terceros=""http://www.sat.gob.mx/terceros"" xmlns:detallista=""http://www.sat.gob.mx/detallista"" xmlns:psgecfd=""http://www.sat.gob.mx/psgecfd"" xmlns:ecc=""http://www.sat.gob.mx/ecc"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital""" + vbCrLf
        XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3  http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv3.xsd http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/TimbreFiscalDigital/TimbreFiscalDigital.xsd http://www.sat.gob.mx/detallista http://www.sat.gob.mx/sitio_internet/cfd/detallista/detallista.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd http://www.buzonfiscal.com/ns/addenda/bf/2 http://www.buzonfiscal.com/schema/xsd/Addenda_BF_v2.2.xsd http://www.sat.gob.mx/ecc http://www.sat.gob.mx/sitio_internet/cfd/ecc/ecc.xsd http://www.sat.gob.mx/psgecfd http://www.sat.gob.mx/sitio_internet/cfd/psgecfd/psgecfd.xsd "
        XMLDoc += "http://www.sat.gob.mx/ecb http://www.sat.gob.mx/sitio_internet/cfd/ecb/ecb.xsd http://www.sat.gob.mx/terceros http://www.sat.gob.mx/sitio_internet/cfd/terceros/terceros.xsd""" + vbCrLf
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        XMLDoc += ">"

        XMLDoc += "<cfdi:Emisor rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf

        XMLDoc += "<cfdi:DomicilioFiscal " + vbCrLf
        If Sucursal.Direccion <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        If Sucursal.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        XMLDoc += "/>" + vbCrLf
        If Sucursal.Pais2 <> "" Then
            XMLDoc += "<cfdi:ExpedidoEn  " + vbCrLf


            If Sucursal.Direccion2 <> "" Then XMLDoc += "calle = """ + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf

            'If O._CalleLocal <> "" Then XMLDoc += "calle=""" + O._CalleLocal + """" + vbCrLf
            'If O._noExteriorLocal <> "" Then XMLDoc += "noExterior=""" + O._noExteriorLocal + """" + vbCrLf
            'If O._noInteriorLocal <> "" Then XMLDoc += "noInterior=""" + O._noInteriorLocal + """" + vbCrLf
            'If O._ColoniaLocal <> "" Then XMLDoc += "colonia=""" + O._ColoniaLocal + """" + vbCrLf
            'If O._LocalidadLocal <> "" Then XMLDoc += "localidad=""" + O._LocalidadLocal + """" + vbCrLf
            'If O._ReferenciaDomicilioLocal <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilioLocal + """" + vbCrLf
            'If O._MunicipioLocal <> "" Then XMLDoc += "municipio=""" + O._MunicipioLocal + """" + vbCrLf
            'If O._EstadoLocal <> "" Then XMLDoc += "estado=""" + O._EstadoLocal + """" + vbCrLf
            'If O._PaisLocal <> "" Then XMLDoc += "pais=""" + O._PaisLocal + """" + vbCrLf
            'If O._CodigoPostalLocal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostalLocal + """"
            XMLDoc += "/>" + vbCrLf
        End If

        XMLDoc += "</cfdi:Emisor>" + vbCrLf


        XMLDoc += "<cfdi:Receptor rfc=""" + Replace(Replace(Replace(Replace(Replace(Cliente.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>" + vbCrLf
        XMLDoc += "<cfdi:Domicilio "
        If Cliente.DireccionFiscal = 0 Then
            If Cliente.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        Else
            If Cliente.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If Cliente.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
        End If
        XMLDoc += "/>" + vbCrLf

        XMLDoc += "</cfdi:Receptor>" + vbCrLf

        XMLDoc += "<cfdi:Conceptos>" + vbCrLf

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbDevolucionesDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)

        While DR.Read
            XMLDoc += "<cfdi:Concepto " + vbCrLf
            XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
            XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(Replace(DR("descripcion"), vbCrLf, ""), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
            If DR("idmoneda") <> 2 Then
                XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
                XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
                XMLDoc += "/> " + vbCrLf
            Else
                XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
                XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
                XMLDoc += "/> " + vbCrLf
            End If

        End While
        DR.Close()

        'Dim VP As New dbVentasProductos(MySqlcon)
        'DR = VP.ConsultaReader(ID)

        'While DR.Read

        '    XMLDoc += "<Concepto " + vbCrLf
        '    XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
        '    XMLDoc += "unidad=""" + DR("tipocantidad") + """" + vbCrLf
        '    XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
        '    If DR("idmoneda") <> 2 Then

        '        XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    Else
        '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    End If
        '    XMLDoc += "/> " + vbCrLf
        'End While
        'DR.Close()

        'Dim VS As New dbVentasServicios(MySqlcon)
        'DR = VS.ConsultaReader(ID)

        'While DR.Read

        '    XMLDoc += "<Concepto " + vbCrLf
        '    XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
        '    XMLDoc += "unidad=""SERV""" + vbCrLf
        '    XMLDoc += "descripcion=""" + DR("descripcion").ToString + """" + vbCrLf
        '    If DR("idmoneda") <> 2 Then
        '        XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    Else
        '        XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "importe=""" + Format(DR("precio"), "#0.00") + """" + vbCrLf
        '        XMLDoc += "/> " + vbCrLf
        '    End If
        '    XMLDoc += "/> " + vbCrLf
        'End While
        'DR.Close()
        XMLDoc += "</cfdi:Conceptos>"

        XMLDoc += "<cfdi:Impuestos totalImpuestosTrasladados=""" + Format(TotalIva, "#0.00") + """ "
        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00") + """"
        End If
        XMLDoc += ">" + vbCrLf

        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "<cfdi:Retenciones>" + vbCrLf
            If ISR <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""ISR""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalISR, "#0.00") + """ />" + vbCrLf
            End If

            If IvaRetenido <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""IVA""" + vbCrLf
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00") + """ />" + vbCrLf
            End If

            XMLDoc += "</cfdi:Retenciones>" + vbCrLf

        End If



        XMLDoc += "<cfdi:Traslados>" + vbCrLf


        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                Else
                    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                If DR("idmoneda") <> 2 Then
                    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                Else
                    IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                End If

            End If
        End While
        DR.Close()
        For Each I As Double In Ivas

            XMLDoc += "<cfdi:Traslado impuesto=""IVA""" + vbCrLf
            XMLDoc += "tasa=""" + I.ToString + """" + vbCrLf
            XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00") + """ />" + vbCrLf

        Next



        XMLDoc += "</cfdi:Traslados>" + vbCrLf





        XMLDoc += "</cfdi:Impuestos>" + vbCrLf
        XMLDoc += "</cfdi:Comprobante>"


        Return XMLDoc

    End Function
    Public Function CreaXMLi32(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer) As String
        'Dim O As New dbOpciones(Comm.Connection)
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>"

        XMLDoc += "<cfdi:Comprobante "
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdConversion)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        XMLDoc += "version=""3.2"" "
        If Serie <> "" Then XMLDoc += "serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        XMLDoc += "folio=""" + Folio.ToString + """ "
        XMLDoc += "fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """ "
        If pSelloDigital <> "" Then XMLDoc += "sello=""" + pSelloDigital + """ "

        XMLDoc += "formaDePago=""Pago en una sola exhibición"" "

        If NoCertificado <> "" Then XMLDoc += "noCertificado=""" + NoCertificado + """ "
        XMLDoc += "certificado=""" + en.Certificado64 + """ "

        XMLDoc += "subTotal=""" + Format(Subtototal, "#0.00####") + """ "


        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf

        XMLDoc += "descuento=""" + "0" + """ "

        'Tipo deCambio nuevo
        If IdConversion <> 2 Then
            XMLDoc += "TipoCambio=""" + Format(TipodeCambio, "#0.00####") + """ "
            Dim Moneda As New dbMonedas(IdConversion, Comm.Connection)
            XMLDoc += "Moneda=""" + Moneda.Abreviatura + """ "
        Else
            XMLDoc += "Moneda=""MXN"" "
        End If
        XMLDoc += "total=""" + Format(TotalVenta, "#0.00####") + """ "

        XMLDoc += "tipoDeComprobante=""egreso"" "
        'Metodo de pago lugar exibibicion nuevo
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        'XMLDoc += "metodoDePago=""" + FP.Nombre + """" + vbCrLf
        'If NoCuenta <> "" Then XMLDoc += "NumCtaPago=""" + NoCuenta + """" + vbCrLf
        'Else
        If Fecha < "2016/06/01" Then
            XMLDoc += "metodoDePago=""No identificado"" "
        Else
            XMLDoc += "metodoDePago=""NA"" "
        End If
        'End If
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad2 + "," + Sucursal.Estado2 + """ "
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.Ciudad + "," + Sucursal.Estado + """ "
        End If

        'XMLDoc += "xsi:schemaLocation = ""http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd""" + vbCrLf
        'XMLDoc += "xmlns=""http://www.sat.gob.mx/cfd/2""" + vbCrLf
        'XMLDoc += "xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""" + vbCrLf
        '++++++++++++++++++++++++++++
        'XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:implocal=""http://www.sat.gob.mx/implocal"" "
        'XMLDoc += "xmlns:terceros=""http://www.sat.gob.mx/terceros"" xmlns:detallista=""http://www.sat.gob.mx/detallista"" xmlns:psgecfd=""http://www.sat.gob.mx/psgecfd"" xmlns:ecc=""http://www.sat.gob.mx/ecc"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital"" "
        'XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/TimbreFiscalDigital/TimbreFiscalDigital.xsd http://www.sat.gob.mx/detallista " + _
        '"http://www.sat.gob.mx/sitio_internet/cfd/detallista/detallista.xsd http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd " + _
        '"http://www.sat.gob.mx/ecc http://www.sat.gob.mx/sitio_internet/cfd/ecc/ecc.xsd http://www.sat.gob.mx/psgecfd http://www.sat.gob.mx/sitio_internet/cfd/psgecfd/psgecfd.xsd " + _
        '"http://www.sat.gob.mx/terceros http://www.sat.gob.mx/sitio_internet/cfd/terceros/terceros11.xsd http://www.sat.gob.mx/donat http://www.sat.gob.mx/sitio_internet/cfd/donat/donat11.xsd"""
        XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" "
        XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd"
        XMLDoc += """ "
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        XMLDoc += ">"

        XMLDoc += "<cfdi:Emisor rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>"

        XMLDoc += "<cfdi:DomicilioFiscal "
        If Sucursal.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        If Sucursal.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        XMLDoc += "/>"
        If Sucursal.Pais2 <> "" Then
            XMLDoc += "<cfdi:ExpedidoEn  "


            If Sucursal.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Sucursal.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "

            'If O._CalleLocal <> "" Then XMLDoc += "calle=""" + O._CalleLocal + """" + vbCrLf
            'If O._noExteriorLocal <> "" Then XMLDoc += "noExterior=""" + O._noExteriorLocal + """" + vbCrLf
            'If O._noInteriorLocal <> "" Then XMLDoc += "noInterior=""" + O._noInteriorLocal + """" + vbCrLf
            'If O._ColoniaLocal <> "" Then XMLDoc += "colonia=""" + O._ColoniaLocal + """" + vbCrLf
            'If O._LocalidadLocal <> "" Then XMLDoc += "localidad=""" + O._LocalidadLocal + """" + vbCrLf
            'If O._ReferenciaDomicilioLocal <> "" Then XMLDoc += "referencia=""" + O._ReferenciaDomicilioLocal + """" + vbCrLf
            'If O._MunicipioLocal <> "" Then XMLDoc += "municipio=""" + O._MunicipioLocal + """" + vbCrLf
            'If O._EstadoLocal <> "" Then XMLDoc += "estado=""" + O._EstadoLocal + """" + vbCrLf
            'If O._PaisLocal <> "" Then XMLDoc += "pais=""" + O._PaisLocal + """" + vbCrLf
            'If O._CodigoPostalLocal <> "" Then XMLDoc += "codigoPostal=""" + O._CodigoPostalLocal + """"
            XMLDoc += "/>"
        End If

        Dim Pos As Integer = 0
        Dim Listo As Boolean = False
        Dim AddDir As String = ""

        While Sucursal.RegimenFiscal.Length > Pos
            If Sucursal.RegimenFiscal.Substring(Pos, 1) = "," Then
                XMLDoc += "<cfdi:RegimenFiscal Regimen=""" + AddDir + """/>"
                AddDir = ""
                Listo = True
            Else
                AddDir += Sucursal.RegimenFiscal.Substring(Pos, 1)
                Listo = False
            End If
            Pos += 1
        End While

        If Listo = False Then XMLDoc += "<cfdi:RegimenFiscal Regimen=""" + AddDir + """/> "



        XMLDoc += "</cfdi:Emisor>"


        XMLDoc += "<cfdi:Receptor rfc=""" + Replace(Replace(Replace(Replace(Replace(Cliente.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ nombre=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """>"
        XMLDoc += "<cfdi:Domicilio "
        If Cliente.DireccionFiscal = 0 Then
            If Cliente.Direccion <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.NoExterior <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.NoInterior <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Colonia <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Ciudad <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.ReferenciaDomicilio <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Municipio <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Estado <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Pais <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.CP <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        Else
            If Cliente.Direccion2 <> "" Then XMLDoc += "calle=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Cliente.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Cliente.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            If Cliente.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Cliente.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        End If
        XMLDoc += "/>"

        XMLDoc += "</cfdi:Receptor>"

        XMLDoc += "<cfdi:Conceptos>"

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbDevolucionesDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)

        While DR.Read
            If DR("cantidad") <> 0 And DR("precio") <> 0 Then
                XMLDoc += "<cfdi:Concepto "
                XMLDoc += "cantidad=""" + DR("cantidad").ToString + """ "
                XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
                Dim Des As String
                Des = Trim(DR("descripcion"))
                While Des.IndexOf("  ") <> -1
                    Des = Replace(Des, "  ", " ")
                End While
                Des = Replace(Des, vbTab, "")
                XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(Des, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
                'If DR("idmoneda") <> 2 Then
                '    XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
                '    XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
                '    XMLDoc += "/> " + vbCrLf
                'Else
                XMLDoc += "valorUnitario=""" + Format(DR("precio") / DR("cantidad"), "#0.00####") + """ "
                XMLDoc += "importe=""" + Format(DR("precio"), "#0.00####") + """ "
                XMLDoc += "/>"
                'End If
            End If
        End While
        DR.Close()

        XMLDoc += "</cfdi:Conceptos>"

        XMLDoc += "<cfdi:Impuestos totalImpuestosTrasladados=""" + Format(TotalIva, "#0.00####") + """"
        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += " totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00####") + """"
        End If
        XMLDoc += ">"

        If ISR <> 0 Or IvaRetenido <> 0 Then
            XMLDoc += "<cfdi:Retenciones>"
            If ISR <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""ISR"" "
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalISR, "#0.00####") + """/>"
            End If

            If IvaRetenido <> 0 Then
                XMLDoc += "<cfdi:Retencion impuesto=""IVA"" "
                'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
                XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00####") + """/>"
            End If

            XMLDoc += "</cfdi:Retenciones>"

        End If



        XMLDoc += "<cfdi:Traslados>"


        DR = DaIvas(ID)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add((DR("precio") * TipodeCambio) * (DR("iva") / 100), DR("iva").ToString)
                'Else
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                'End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                'If DR("idmoneda") <> 2 Then
                '    IvasImporte.Add(IAnt + ((DR("precio") * TipodeCambio) * (DR("iva") / 100)), DR("iva").ToString)
                'Else
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                'End If

            End If
        End While
        DR.Close()
        For Each I As Double In Ivas

            XMLDoc += "<cfdi:Traslado impuesto=""IVA"" "
            XMLDoc += "tasa=""" + Format(I, "#0.00####") + """ "
            XMLDoc += "importe=""" + Format(IvasImporte(I.ToString), "#0.00####") + """/>"

        Next



        XMLDoc += "</cfdi:Traslados>"





        XMLDoc += "</cfdi:Impuestos>"
        XMLDoc += "</cfdi:Comprobante>"


        Return XMLDoc

    End Function
    Public Function DaIvas(ByVal piddevolucion As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iva,precio,idmoneda from tbldevolucionesdetalles where iddevolucion=" + piddevolucion.ToString
        Return Comm.ExecuteReader
    End Function
    Public Function DaIvasIEPS(ByVal piddevolucion As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select ieps,precio,idmoneda from tbldevolucionesdetalles where iddevolucion=" + piddevolucion.ToString
        Return Comm.ExecuteReader
    End Function

    Public Function Reporte(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pSoloCanceladas As Boolean, ByVal pMostrarEnPesos As Byte, ByVal pidInventario As Integer, ByVal pSerie As String, pIdAlmacen As Integer, pIdTipo As Integer, pidTipoSucursal As Integer) As DataView
        Dim DS As New DataSet
        If pMostrarEnPesos = 0 Then
            Comm.CommandText = "select s.nombre as iddevolucion,td.fecha,td.serie,td.folio,tdd.cantidad,if(tdd.idmoneda=2,tdd.precio,tdd.precio*td.tipodecambio) as precio,td.estado,tdd.iva,tdd.ieps,tdd.ivaretenido,if(td.idventa<>0,'FACTURAS','REMISIONES') as tipodev," + _
            "(select clave from tblinventario where tblinventario.idinventario=tdd.idinventario) as clave," + _
            "(select nombre from tblinventario where tblinventario.idinventario=tdd.idinventario) as nombre, " + _
            "if(td.idventa<>0,(select tblventas.serie from tblventas where tblventas.idventa=td.idventa),(select tblventasremisiones.serie from tblventasremisiones where tblventasremisiones.idremision=td.idremision)) as docserie,if(td.idventa<>0,(select tblventas.folio from tblventas where tblventas.idventa=td.idventa),(select tblventasremisiones.folio from tblventasremisiones where tblventasremisiones.idremision=td.idremision)) as docfolio" +
            " from tbldevoluciones as td inner join tbldevolucionesdetalles as tdd on td.iddevolucion=tdd.iddevolucion inner join tblsucursales as s on td.idsucursal=s.idsucursal inner join tblclientes on td.idcliente=tblclientes.idcliente where td.fecha>='" + pFecha1 + "' and td.fecha<='" + pFecha2 + "' "
        Else
            Comm.CommandText = "select s.nombre as iddevolucion,td.fecha,td.serie,td.folio,tdd.cantidad,tdd.precio as precio,td.estado,tdd.iva,tdd.ieps,tdd.ivaretenido,if(td.idventa<>0,'FACTURAS','REMISIONES') as tipodev," + _
            "(select clave from tblinventario where tblinventario.idinventario=tdd.idinventario) as clave," + _
            "(select nombre from tblinventario where tblinventario.idinventario=tdd.idinventario) as nombre, " + _
            "if(td.idventa<>0,(select tblventas.serie from tblventas where tblventas.idventa=td.idventa),(select tblventasremisiones.serie from tblventasremisiones where tblventasremisiones.idremision=td.idremision)) as docserie,if(td.idventa<>0,(select tblventas.folio from tblventas where tblventas.idventa=td.idventa),(select tblventasremisiones.folio from tblventasremisiones where tblventasremisiones.idremision=td.idremision)) as docfolio" +
            "from tbldevoluciones as td inner join tbldevolucionesdetalles as tdd on td.iddevolucion=tdd.iddevolucion inner join tblsucursales as s on td.idsucursal=s.idsucursal inner join tblclientes on td.idcliente=tblclientes.idcliente where td.fecha>='" + pFecha1 + "' and td.fecha<='" + pFecha2 + "' "
        End If
        If pSoloCanceladas Then
            Comm.CommandText += " and td.estado=4"
        Else
            Comm.CommandText += " and (td.estado=3 or td.estado=4)"
        End If
        If pIdSucursal > 0 Then
            Comm.CommandText += " and td.idsucursal=" + pIdSucursal.ToString
        End If
        If pidTipoSucursal > 0 Then
            Comm.CommandText += " and s.idtipo=" + pidTipoSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and td.idcliente=" + pIdCliente.ToString
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
        If pSerie <> "" Then
            Comm.CommandText += " and td.serie like '%" + Replace(pSerie, "'", "''") + "%'"
        End If
        If pIdAlmacen > 0 Then
            Comm.CommandText += " and tbldevolucionesdetalles.idalmacen=" + pIdAlmacen.ToString
        End If
        If pIdTipo > 0 Then
            Comm.CommandText += " and tblclientes.idtipo=" + pIdTipo.ToString
        End If
        Comm.CommandText += " order by td.fecha,td.serie,td.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevoluciones")
        'DS.WriteXmlSchema("tbldevoluciones.xml")
        Return DS.Tables("tbldevoluciones").DefaultView
    End Function
    Public Function ReportePorTipodePago(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, Optional ByVal pIdCliente As Integer = 0) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevoluciones.iddevolucion,tbldevoluciones.folio,tbldevoluciones.serie,tbldevoluciones.estado,if(tbldevoluciones.idconversion=2,tbldevoluciones.total,tbldevoluciones.total*tbldevoluciones.tipodecambio) as total,if(tbldevoluciones.idconversion=2,tbldevoluciones.totalapagar,tbldevoluciones.totalapagar*tbldevoluciones.tipodecambio) as totalapagar,tbldevoluciones.fecha,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion,tbldevolucionesinventario.cantidad,tbldevolucionesinventario.descripcion,tbldevolucionesinventario.precio,0 as costoinv,0 as costopro,tbldevolucionesinventario.idinventario,tbldevolucionesinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre " + _
        "from tbldevoluciones inner join tbldevolucionesinventario on tbldevoluciones.iddevolucion=tbldevolucionesinventario.iddevolucion inner join tblinventario on tbldevolucionesinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tbldevolucionesinventario.idvariante inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tbldevoluciones.idforma where tbldevoluciones.estado=3 and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"

        If pIdSucursal > 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tbldevoluciones.idcliente=" + pIdCliente.ToString
        End If
        'If pidInventario > 1 Then
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
        Comm.CommandText += " order by tbldevoluciones.fecha,tbldevoluciones.serie,tbldevoluciones.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevoluciones")
        'DS.WriteXmlSchema("tbldevoluciones.xml")
        Return DS.Tables("tbldevoluciones").DefaultView
    End Function
    Public Function ReporteVentasArticulos(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdSucursal As Integer, ByVal pIdCliente As Integer, ByVal pIdInventario As Integer, ByVal pidClasificacion As Integer, ByVal pidClasificacion2 As Integer, ByVal pidClasificacion3 As Integer) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tbldevoluciones.iddevolucion,tbldevoluciones.folio,tbldevoluciones.serie,tbldevoluciones.estado,tbldevoluciones.total,tbldevoluciones.totalapagar,tbldevoluciones.fecha,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion,tbldevolucionesinventario.cantidad,if(tbldevolucionesinventario.idinventario>1,tblinventario.nombre,if(tbldevolucionesinventario.idvariante>1,tblproductos.nombre,'SERVICIO')) as descripcion,if(tbldevolucionesinventario.idmoneda=2,tbldevolucionesinventario.precio,tbldevolucionesinventario.precio*tbldevoluciones.tipodecambio) as precio,0 as costoinv,0 as costopro,tbldevolucionesinventario.idinventario,tbldevolucionesinventario.idvariante,tblformasdepago.tipo as formadepago,tblclientes.nombre as cnombre,tbldevolucionesinventario.iva,tbldevoluciones.isr,tbldevoluciones.ivaretenido from tbldevoluciones inner join tbldevolucionesinventario on tbldevoluciones.iddevolucion=tbldevolucionesinventario.iddevolucion inner join tblinventario on tbldevolucionesinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tbldevolucionesinventario.idvariante inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tbldevoluciones.idforma inner join tblproductos on tblproductosvariantes.idproducto=tblproductos.idproducto where tbldevoluciones.estado=3 and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        'Comm.CommandText = "select tbldevoluciones.iddevolucion,tbldevoluciones.folio,tbldevoluciones.serie,tbldevoluciones.estado,tbldevoluciones.total,tbldevoluciones.totalapagar,tbldevoluciones.fecha,tbldevoluciones.tipodecambio,tbldevoluciones.idconversion,tbldevolucionesinventario.cantidad,tbldevolucionesinventario.descripcion,tbldevolucionesinventario.precio,if(tbldevolucionesinventario.idinventario>1,spsacacostoarticulo(tbldevolucionesinventario.idinventario," + pTipoCosteo.ToString + ",tblinventario.contenido),0) as costoinv,if(tbldevolucionesinventario.idvariante>1,spsacacostoproducto(tblproductosvariantes.idproducto," + pTipoCosteo.ToString + "),0) as costopro,tbldevolucionesinventario.idinventario,tbldevolucionesinventario.idvariante,tblformasdepago.nombre as formadepago,tblclientes.nombre as cnombre from tbldevoluciones inner join tbldevolucionesinventario on tbldevoluciones.iddevolucion=tbldevolucionesinventario.iddevolucion inner join tblinventario on tbldevolucionesinventario.idinventario=tblinventario.idinventario inner join tblproductosvariantes on tblproductosvariantes.idvariante=tbldevolucionesinventario.idvariante inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente inner join tblformasdepago on tblformasdepago.idforma=tbldevoluciones.idforma where tbldevoluciones.estado=3 and tbldevoluciones.fecha>='" + pFecha1 + "' and tbldevoluciones.fecha<='" + pFecha2 + "'"
        If pIdSucursal > 0 Then
            Comm.CommandText += " and tbldevoluciones.idsucursal=" + pIdSucursal.ToString
        End If
        If pIdCliente > 0 Then
            Comm.CommandText += " and tbldevoluciones.idcliente=" + pIdCliente.ToString
        End If
        If pIdInventario > 1 Then
            Comm.CommandText += " and tbldevolucionesinventario.idinventario=" + pIdInventario.ToString
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
        Comm.CommandText += " order by tbldevoluciones.fecha,tbldevoluciones.serie,tbldevoluciones.folio"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tbldevoluciones")
        'DS.WriteXmlSchema("tbldevoluciones.xml")
        Return DS.Tables("tbldevoluciones").DefaultView
    End Function
    Public Sub ReporteMensualCFD(ByVal pFecha As Date, ByVal pRutaArchivo As String)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim Fecha1 As Date
        Dim Fecha2 As Date
        Dim Mes1 As Integer
        Dim Mes2 As Integer
        Fecha1 = DateSerial(Year(pFecha), Month(pFecha), 1)
        Fecha2 = DateSerial(Year(pFecha), Month(pFecha) + 1, 0)
        Dim S As String = ""
        Comm.CommandText = "select tbldevoluciones.iddevolucion,tblclientes.rfc,tbldevoluciones.serie,tbldevoluciones.folio,tbldevoluciones.noaprobacion,tbldevoluciones.yearaprobacion,tbldevoluciones.fecha,tbldevoluciones.hora,tbldevoluciones.totalapagar,tbldevoluciones.total,tbldevoluciones.estado,tbldevoluciones.fechacancelado from tbldevoluciones inner join tblclientes on tbldevoluciones.idcliente=tblclientes.idcliente where (fecha>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fecha<='" + Format(Fecha2, "yyyy/MM/dd") + "') or (fechacancelado>='" + Format(Fecha1, "yyyy/MM/dd") + "' and fechacancelado<='" + Format(Fecha2, "yyyy/MM/dd") + "') order by serie,folio"
        DReader = Comm.ExecuteReader
        While DReader.Read
            Mes1 = Month(CDate(DReader("fecha")))
            Mes2 = Month(CDate(DReader("fechacancelado")))
            If Mes1 = Mes2 And DReader("estado") = Estados.Cancelada Then
                If S <> "" Then S += vbCrLf
                S += "|" + DReader("rfc") + "|"
                S += DReader("serie") + "|"
                S += DReader("folio").ToString + "|"
                S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
                S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
                S += Format(DReader("totalapagar"), "#0.00") + "|"
                S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
                S += "1|I||||" + vbCrLf
                S += "|" + DReader("rfc") + "|"
                S += DReader("serie") + "|"
                S += DReader("folio").ToString + "|"
                S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
                S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
                S += Format(DReader("totalapagar"), "#0.00") + "|"
                S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
                S += "0|I||||"
            Else
                If DReader("estado") = Estados.Cancelada Then
                    If S <> "" Then S += vbCrLf
                    S += "|" + DReader("rfc") + "|"
                    S += DReader("serie") + "|"
                    S += DReader("folio").ToString + "|"
                    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
                    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
                    S += Format(DReader("totalapagar"), "#0.00") + "|"
                    S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
                    S += "0|I||||"
                End If
                If DReader("estado") = Estados.Guardada Then
                    If S <> "" Then S += vbCrLf
                    S += "|" + DReader("rfc") + "|"
                    S += DReader("serie") + "|"
                    S += DReader("folio").ToString + "|"
                    S += DReader("yearaprobacion") + DReader("noaprobacion") + "|"
                    S += Format(CDate(DReader("fecha")), "dd/MM/yyyy") + " " + DReader("hora") + "|"
                    S += Format(DReader("totalapagar"), "#0.00") + "|"
                    S += Format(DReader("totalapagar") - DReader("total"), "#0.00") + "|"
                    S += "1|I||||"
                End If
            End If
        End While
        DReader.Close()
        Dim Enc As New System.Text.UTF8Encoding
        Dim Bytes() As Byte = Enc.GetBytes(S)
        Dim en As New Encriptador
        en.GuardaArchivo(pRutaArchivo, Bytes)
    End Sub
    Public Sub Aplicar(ByVal pId As Integer, ByVal pCantidad As Double, ByVal pSuma As Boolean)
        If pSuma Then
            Comm.CommandText = "update tbldevoluciones set credito=credito+" + pCantidad.ToString + " where iddevolucion=" + pId.ToString
        Else
            Comm.CommandText = "update tbldevoluciones set credito=credito-" + pCantidad.ToString + " where iddevolucion=" + pId.ToString
        End If
        Comm.ExecuteNonQuery()
    End Sub

    Private Function ValidarCertificadoRemoto(ByVal sender As Object, ByVal certificate As Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As Security.Cryptography.X509Certificates.X509Chain, ByVal policyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Public Function Timbrar2(ByVal pUsuario As String, ByVal pPassword As String, ByVal pRFC As String, ByVal pRutaXML As String, ByVal pRutaSalida As String, ByVal pConector As Byte, ByVal pConMegbox As Boolean) As Integer
        Try
            Dim Comando As String
            If pConector = 0 Then
                Comando = Application.StartupPath + "\conector\timbrador_cliente.exe " + pUsuario + "*" + pPassword + "*" + pRFC + "*" + pRutaXML + "*" + pRutaSalida
            Else
                Comando = Application.StartupPath + "\conector2012\timbrar.exe " + pUsuario + "*" + pPassword + "*" + pRFC + "*" + pRutaSalida
            End If
            Shell(Comando, AppWinStyle.Hide, True)
            Return 1
        Catch ex As Exception
            If pConMegbox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError += ex.Message
            End If
            NoCertificadoSAT = "Error"
            Return 0
        End Try

    End Function
    Public Function Timbrar3(ByVal pRFC As String, ByVal pXML As String, ByVal pRutaSalida As String, ByVal pAPIKEY As String, ByVal ConMsgBox As Boolean, ByVal pSerie As String, ByVal pFolio As Integer) As String
        'Try
        '    Dim Cadena As String
        '    Dim XmlTimbrado As String = ""
        '    Cadena = pRFC + "~" + pAPIKEY + "~" + "NO" + "~" + "Factura" + "~" + pXML
        '    Dim FF As New facturafiel.server()()
        '    XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
        '    Return XmlTimbrado
        'Catch ex As Exception
        '    If ConMsgBox Then
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        '    Else
        '        MensajeError = ex.Message
        '    End If
        '    NoCertificadoSAT = "Error"
        '    Return "ERROR"
        'End Try
        Dim Cadena As String
        Dim XmlTimbrado As String = ""
        Try

            Dim Alterno As String = "0"
            Comm.CommandText = "select nombreempresalocal from tblopciones limit 1"
            Alterno = Comm.ExecuteScalar
            If Alterno = "1" Then
                Return Timbrar3Alt(pRFC, pXML, pRutaSalida, pAPIKEY, ConMsgBox, pSerie, pFolio)
            End If
            Dim Pruebas As String = "NO"
            Comm.CommandText = "select codigopostal from tblopciones limit 1"
            Pruebas = Comm.ExecuteScalar
            Cadena = pRFC + "~" + pAPIKEY + "~" + Pruebas + "~" + "Factura" + "~" + pXML
            Dim FF As New facturafiel.server()
            FF.Url = "http://www.facturafiel.com/websrv/servicio_timbrado_xml.php?wsdl"
            XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
            FF.Dispose()
            'Return XmlTimbrado
        Catch ex As Exception
            'If ex.Message.Contains("Response is not well-formed XML") Then
            '    If ConMsgBox Then
            '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            '    Else
            '        MensajeError = ex.Message
            '    End If
            '    'Response is not well-formed XML.
            '    NoCertificadoSAT = "Error"
            '    XmlTimbrado = "ERROR"
            '    'Return "ERROR"
            'Else
            Dim en As New Encriptador
            IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
            If IO.File.Exists(Application.StartupPath + "\temp\derror.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\derror.txt")
            End If
            en.GuardaArchivoTexto(Application.StartupPath + "\temp\derror.txt", ex.Message, System.Text.Encoding.Default)



            XmlTimbrado = "Recuperar"
            ' End If
        End Try
        'recuperacion
        Try
            If XmlTimbrado = "Recuperar" Then
                Dim R As New facturafielrecuperacion.server()
                R.Url = "http://www.facturafiel.com/websrv/servicio_recuperacion.php?wsdl"
                If pSerie <> "" Then
                    Cadena = pRFC + "~" + pAPIKEY + "~" + pSerie + "+" + pFolio.ToString
                Else
                    Cadena = pRFC + "~" + pAPIKEY + "~+" + pFolio.ToString
                End If
                XmlTimbrado = R.servicio_recuperacion(Cadena)
                XmlTimbrado = XmlTimbrado.Substring(1, XmlTimbrado.Length - 1)
                R.Dispose()
            End If
        Catch ex As Exception
            If ConMsgBox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError = ex.Message
            End If
            XmlTimbrado = "ERROR"
            NoCertificadoSAT = "Error"
        End Try
        Return XmlTimbrado

    End Function

    Public Function Timbrar3Alt(ByVal pRFC As String, ByVal pXML As String, ByVal pRutaSalida As String, ByVal pAPIKEY As String, ByVal ConMsgBox As Boolean, ByVal pSerie As String, ByVal pFolio As Integer) As String
        'Try
        '    Dim Cadena As String
        '    Dim XmlTimbrado As String = ""
        '    Cadena = pRFC + "~" + pAPIKEY + "~" + "NO" + "~" + "Factura" + "~" + pXML
        '    Dim FF As New facturafiel.server()()
        '    XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
        '    Return XmlTimbrado
        'Catch ex As Exception
        '    If ConMsgBox Thenfacturafiel.server()()
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        '    Else
        '        MensajeError = ex.Message
        '    End If
        '    NoCertificadoSAT = "Error"
        '    Return "ERROR"
        'End Try
        Dim Cadena As String
        Dim XmlTimbrado As String = ""
        Try
            Dim Pruebas As String = "NO"
            Comm.CommandText = "select codigopostal from tblopciones limit 1"
            Pruebas = Comm.ExecuteScalar
            Cadena = pRFC + "~" + pAPIKEY + "~" + Pruebas + "~" + "Factura" + "~" + pXML
            Dim FF As New facturafiel.server()
            FF.Url = "http://www.facturafiel.com/websrv/servicio_timbrado_xml.php?wsdl"
            XmlTimbrado = FF.servicio_timbrado_xml(Cadena)
            FF.Dispose()
            'Return XmlTimbrado
        Catch ex As Exception
            'If ex.Message.Contains("Response is not well-formed XML") Then
            '    If ConMsgBox Then
            '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            '    Else
            '        MensajeError = ex.Message
            '    End If
            '    'Response is not well-formed XML.
            '    NoCertificadoSAT = "Error"
            '    XmlTimbrado = "ERROR"
            '    'Return "ERROR"
            'Else
            Dim en As New Encriptador
            IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
            If IO.File.Exists(Application.StartupPath + "\temp\derror.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\derror.txt")
            End If
            en.GuardaArchivoTexto(Application.StartupPath + "\temp\derror.txt", ex.Message, System.Text.Encoding.Default)


            XmlTimbrado = "Recuperar"
            ' End If
        End Try
        'recuperacion
        Try
            If XmlTimbrado = "Recuperar" Then
                Dim R As New facturafielrecuperacion.server()
                R.Url = "http://www.facturafiel.com/websrv/servicio_recuperacion.php?wsdl"
                If pSerie <> "" Then
                    Cadena = pRFC + "~" + pAPIKEY + "~" + pSerie + "+" + pFolio.ToString
                Else
                    Cadena = pRFC + "~" + pAPIKEY + "~+" + pFolio.ToString
                End If
                XmlTimbrado = R.servicio_recuperacion(Cadena)
                XmlTimbrado = XmlTimbrado.Substring(1, XmlTimbrado.Length - 1)
                R.Dispose()
            End If
        Catch ex As Exception
            If ConMsgBox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError = ex.Message
            End If
            XmlTimbrado = "ERROR"
            NoCertificadoSAT = "Error"
        End Try
        Return XmlTimbrado

    End Function

    Public Function CancelarTimbrado2(ByVal pRFC As String, ByVal pUUID As String, ByVal pAPIKey As String) As Integer
        Dim Cadena As String = ""
        Dim en As New Encriptador
        IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
        If IO.File.Exists(Application.StartupPath + "\temp\cancelacion.txt") Then
            IO.File.Delete(Application.StartupPath + "\temp\cancelacion.txt")
        End If
        If pUUID.Contains("Ambiente") Then Return 1
        Dim Cancela As New facturafielcancelacion.server
        Cancela.Url = "http://www.facturafiel.com/websrv/servicio_cancelacion.php?wsdl"
        Cadena = pRFC + "~" + pAPIKey + "~" + pUUID
        Cadena = Cancela.servicio_cancelacion(Cadena)
        en.GuardaArchivoTexto(Application.StartupPath + "\temp\cancelacion.txt", Cadena, System.Text.Encoding.Default)
        If Cadena.Contains("EXITOSAMENTE") Then
            Return 1
        Else
            'MensajeError = Cadena
            Return 0
        End If
    End Function
    'Public Function Timbrar(ByVal RFCEmisor As String, ByVal RFCCliente As String, ByVal ArchivoCer As String, ByVal CerPassword As String, ByVal strXml As String, ByVal pDireccionTimbrado As String) As TimbreFiscal.TimbreFiscalDigital
    '    Dim en As New Encriptador
    '    Dim T As TimbreFiscal.TimbreFiscalDigital
    '    Try
    '        Dim Tcfdi As New TimbreFiscal.TimbradoCFDI
    '        Dim x509 As New Security.Cryptography.X509Certificates.X509Certificate(en.LeeArchivo(ArchivoCer), CerPassword)
    '        Tcfdi.ClientCertificates.Add(x509)
    '        Tcfdi.Url = pDireccionTimbrado '"https://demotf.buzonfiscal.com/timbrado?wsdl"
    '        Dim Req As New TimbreFiscal.RequestTimbradoCFDType
    '        Req.InfoBasica = New TimbreFiscal.InfoBasicaType
    '        Req.InfoBasica.RfcEmisor = RFCEmisor
    '        Req.InfoBasica.RfcReceptor = Cliente.RFC
    '        Req.InfoBasica.Serie = Serie
    '        Req.RefID = Serie + Folio.ToString
    '        'Req.Documento = New TimbreFiscal.DocumentoType
    '        'Dim Encoder As New System.Text.UTF8Encoding
    '        'Dim Bytes() As Byte = Encoder.GetBytes(strXml)
    '        'Req.Documento.Archivo = Bytes
    '        Dim s As New Xml.Serialization.XmlSerializer(GetType(TimbreFiscal.Comprobante), "http://www.sat.gob.mx/cfd/3")
    '        Dim xml As String = strXml
    '        Dim c As TimbreFiscal.Comprobante = s.Deserialize(New IO.StringReader(xml))
    '        Req.Comprobante = c

    '        Dim x As New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificadoRemoto)
    '        System.Net.ServicePointManager.ServerCertificateValidationCallback = x
    '        System.Net.ServicePointManager.Expect100Continue = True
    '        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
    '        T = Tcfdi.timbradoCFD(Req)
    '        Return T
    '    Catch ex As Exception
    '        T = New TimbreFiscal.TimbreFiscalDigital
    '        T.noCertificadoSAT = "Error"
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '        Return T
    '    End Try

    'End Function
    Public Function Timbrar(ByVal RFCEmisor As String, ByVal RFCCliente As String, ByVal ArchivoCer As String, ByVal CerPassword As String, ByVal strXml As String, ByVal pDireccionTimbrado As String, ByVal pConMsgBox As Boolean) As TimbreFiscal.TimbreFiscalDigital
        Dim en As New Encriptador
        Dim T As TimbreFiscal.TimbreFiscalDigital
        Try
            'Dim FF As New facturafiel.server()()
            'FF.servicio_timbrado_xml()
            Dim Tcfdi As New TimbreFiscal.TimbradoCFDI
            Dim x509 As New Security.Cryptography.X509Certificates.X509Certificate(en.LeeArchivo(ArchivoCer), CerPassword)
            Tcfdi.ClientCertificates.Add(x509)
            Tcfdi.Url = pDireccionTimbrado '"https://demotf.buzonfiscal.com/timbrado?wsdl"
            Dim Req As New TimbreFiscal.RequestTimbradoCFDType
            Req.InfoBasica = New TimbreFiscal.InfoBasicaType
            Req.InfoBasica.RfcEmisor = RFCEmisor
            Req.InfoBasica.RfcReceptor = Cliente.RFC
            Req.InfoBasica.Serie = Serie

            Req.RefID = Serie + Folio.ToString
            'Req.Documento.Tipo = TimbreFiscal.DocumentoTypeTipo.XML
            Req.Documento = New TimbreFiscal.DocumentoType
            ' Req.Documento.Version = "3.0"
            Dim Encoder As New System.Text.UTF8Encoding
            'Dim Bytes() As Byte = Encoder.GetBytes(strXml)
            'Req.Documento.Archivo = Bytes
            'Dim s As New Xml.Serialization.XmlSerializer(GetType(TimbreFiscal.Comprobante), "http://www.sat.gob.mx/cfd/3")
            'Dim xml As String = strXml
            'Dim c As TimbreFiscal.Comprobante = s.Deserialize(New IO.StringReader(xml))
            Dim Bytes() As Byte = Encoder.GetBytes(strXml)
            Req.Documento.Archivo = Bytes
            'Dim c As TimbreFiscal.Comprobante = s.Deserialize(New IO.StringReader(Convert.ToBase64String(xml)))
            'Req.Comprobante = c
            Dim x As New System.Net.Security.RemoteCertificateValidationCallback(AddressOf ValidarCertificadoRemoto)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = x
            System.Net.ServicePointManager.Expect100Continue = True
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
            'Tcfdi.SoapVersion = Web.Services.Protocols.SoapProtocolVersion.Default
            T = Tcfdi.timbradoCFD(Req)
            Return T
        Catch ex As Exception
            T = New TimbreFiscal.TimbreFiscalDigital
            T.noCertificadoSAT = "Error"
            If pConMsgBox Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MensajeError += ex.Message
            End If
            Return T
        End Try

    End Function

    Public Sub GuardaDatosTimbrado(ByVal pidDevolucion As Integer, ByVal pUuid As String, ByVal pFechaTimbrado As String, ByVal pSellocfd As String, ByVal pNoCertificadoSat As String, ByVal pSelloSat As String)
        Comm.CommandText = "insert into tbldevolucionestimbrado(iddevolucion,uuid,fechatimbrado,sellocfd,nocertificadosat,sellosat) values(" + pidDevolucion.ToString + ",'" + Replace(pUuid, "'", "''") + "','" + Replace(pFechaTimbrado, "'", "''") + "','" + Replace(pSellocfd, "'", "''") + "','" + Replace(pNoCertificadoSat, "'", "''") + "','" + Replace(pSelloSat, "'", "''") + "')"
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub DaDatosTimbrado(ByVal pidDevolucion As Integer)
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select * from tbldevolucionestimbrado where iddevolucion=" + pidDevolucion.ToString
        DReader = Comm.ExecuteReader
        uuid = "**No Timbrado**"
        If DReader.Read Then
            uuid = DReader("uuid")
            FechaTimbrado = DReader("fechatimbrado")
            SelloCFD = DReader("sellocfd")
            NoCertificadoSAT = DReader("nocertificadosat")
            SelloSAT = DReader("sellosat")
        End If
        DReader.Close()
    End Sub
    Public Sub ModificaEstado(ByVal pidVenta As Integer, ByVal pEstado As Byte)
        Comm.CommandText = "update tbldevoluciones set estado=" + pEstado.ToString + " where iddevolucion=" + pidVenta.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaDatosDocumento(ByVal pIdDevolucion As Integer) As String
        Comm.CommandText = "select if(idventa<>0," + _
        "ifnull((select concat('FACTURA-',serie,convert(folio using utf8)) from tblventas where tblventas.idventa=tbldevoluciones.idventa),'')," + _
        "ifnull((select concat('REMISIÓN-',serie,convert(folio using utf8)) from tblventasremisiones where tblventasremisiones.idremision=tbldevoluciones.idremision),'')) from tbldevoluciones where iddevolucion=" + pIdDevolucion.ToString
        Return Comm.ExecuteScalar
    End Function
    Public Function CancelarTimbrado(ByVal pRFC As String, ByVal pUUID As String) As Integer
        Try
            Dim Comando As String
            Dim StrCancelacion As String
            Dim en As New Encriptador
            IO.Directory.CreateDirectory(Application.StartupPath + "\temp\")
            If IO.File.Exists(Application.StartupPath + "\temp\cancelacion.txt") Then
                IO.File.Delete(Application.StartupPath + "\temp\cancelacion.txt")
            End If
            If pRFC = "SUL010720JN8" Then
                Comando = Application.StartupPath + "\conector2012\CLIENTE_TIMBRADOR222.exe CANCELAR*" + pRFC + "*" + pUUID + "*" + Application.StartupPath + "\temp\cancelacion.txt"
            Else
                Comando = Application.StartupPath + "\conector2012\CLIENTE_TIMBRADOR.exe CANCELAR*" + pRFC + "*" + pUUID + "*" + Application.StartupPath + "\temp\cancelacion.txt"
            End If
            Shell(Comando, AppWinStyle.Hide, True)
            StrCancelacion = en.LeeArchivoTexto(Application.StartupPath + "\temp\cancelacion.txt")
            StrCancelacion = StrCancelacion.ToUpper
            If StrCancelacion.Contains("HAS BEEN CANCELED") Then
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Return 0
        End Try
    End Function
    Public Sub ActualizaComentario(ByVal piddevolucion As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tbldevoluciones set comentario='" + Replace(pTexto, "'", "''") + "' where iddevolucion=" + piddevolucion.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function DaId(ByVal pFolio As Integer, ByVal pSerie As String) As Integer
        Comm.CommandText = "select ifnull((select iddevolucion from tbldevoluciones where folio=" + pFolio.ToString + " and serie='" + Replace(Trim(pSerie), "'", "''") + "' and estado>2 limit 1),0)"
        ID = Comm.ExecuteScalar
        If ID <> 0 Then LlenaDatos()
        Return ID
    End Function

    Public Function CreaCadenaOriginali33(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer, pXMLINE As String, pEsEgreso As Byte, pCadenaOriginalComp As String) As String
        Dim CO As String = "|3.3|"

        Dim en As New Encriptador
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        Dim IAnt As Double
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()
        'Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        'Dim IDNotaP As Integer
        'Dim NotaP As New dbNotariosPublicos(MySqlcon)
        'IDNotaP = NotaP.HayDatosNotarios(pIdVenta)
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdConversion)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        If Serie <> "" Then CO += Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Folio.ToString + "|"
        CO += Replace(Fecha, "/", "-") + "T" + Hora + "|"

        'If pSelloDigital <> "" Then CO += "Sello=""" + pSelloDigital + """ "

        'Dim strMetodos As String = ""
        'Dim MeP As New dbVentasAddMetodos(Comm.Connection)
        'DR = MeP.ConsultaReader(0, ID)
        'DR.Read()
        'If strMetodos <> "" Then strMetodos += ","
        'If DR("clavesat") < 1000 Then
        'strMetodos += Format(DR("clavesat"), "00")
        'Else
        'strMetodos += "NA"
        'End If
        'DR.Close()
        'CO += strMetodos + "|"
        CO += "99|"
        If NoCertificado <> "" Then CO += NoCertificado + "|"
        'CO += "Certificado=""" + en.Certificado64 + """ "

        'CO+="CondicionesDePago="""""

        CO += Format(Subtototal, "#0.00####") + "|"
        
        'Descuento CO+="0|"
        'Tipo deCambio nuevo
        If IdConversion <> 2 Then
            Dim Moneda As New dbMonedas(IdConversion, Comm.Connection)
            CO += Moneda.Abreviatura + "|"
            CO += Format(TipodeCambio, "#0.00####") + "|"
        Else
            CO += "MXN|"
        End If


        CO += Format(TotalVenta, "#0.00####") + "|"
        

        'Dim CP As New dbVentasCartaPorte(ID, MySqlcon)
        
            CO += "E|"

        
        'If IdFormadePago <> 98 Then
        CO += "PUE|"
        'End If

        If Sucursal.CP2 <> "" Then
            CO += Sucursal.CP2 + "|"
        Else
            CO += Sucursal.CP + "|"
        End If
        'Confirmacion
        'If NoConfirmacion <> "" Then CO += NoConfirmacion + "|"

        'CFDIS relacionados aqui'
        CO += "03|"
        'whiles docs
        CO += UUIDVenta + "|"
        'end while
        'xmldox+="</cfdi:CfdiRelacionados>"

        CO += Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Sucursal.ClaveRegimen.ToString + "|"


        CO += Replace(Replace(Replace(Replace(Replace(Cliente.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        CO += Replace(Replace(Replace(Replace(Replace(Cliente.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
        If Cliente.RFC = "XEXX010101000" Then
            CO += Cliente.cPais + "|"
        End If
        'If pXMLINE.Contains("cce11:ComercioExterior") = True Then
        '    CO += Cliente.RegIdTrib + "|"
        'End If
        CO += "G02|"

        Dim AduanaCol As New Collection
        Dim AduanaCont As Integer
        Dim AduanaXML As String
        Dim IA As New dbInventarioAduana(Comm.Connection)
        DR = IA.ConsultaAduanaDevReader(ID)
        While DR.Read
            AduanaCol.Add(New InfoAduana(DR("numero"), DR("fecha"), DR("aduana"), DR("iddetalle"), DR("yvalidacion"), DR("claveaduana"), DR("patente")))
        End While
        DR.Close()

        Dim VI As New dbDevolucionesDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        Dim PrecioTemp As Double = 0
        Dim ImpXML As String = ""
        While DR.Read
            'If DR("noimpimporte") <> 0 Then
            'PrecioTemp = DR("noimpimporte")
            'Else
            PrecioTemp = DR("precio")
            'End If
            CO += DR("cproductoserv") + "|"
            CO += Replace(Replace(Replace(Replace(Replace(DR("clave"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
            CO += DR("cantidad").ToString + "|"
            CO += DR("cunidad") + "|"
            CO += Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"
            Dim Des As String
            Des = Trim(Replace(DR("descripcion"), vbCrLf, ""))
            While Des.IndexOf("  ") <> -1
                Des = Replace(Des, "  ", " ")
            End While
            Des = Replace(Des, vbTab, "")
            CO += Replace(Replace(Replace(Replace(Replace(Replace(Des, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + "|"

            If DR("cantidad") <> 0 Then
                CO += Format((PrecioTemp + DR("cdescuento")) / DR("cantidad"), "#0.00####") + "|"
                CO += Format(PrecioTemp + DR("cdescuento"), "#0.00####") + "|"
            Else
                CO += "0.00|"
                CO += "0.00|"
            End If

            'If DR("cdescuento") <> 0 Then CO += Format(DR("cdescuento"), "#0.00####") + "|"
            ImpXML = ""
            If DR("iva") <> 0 Or DR("ieps") <> 0 Or DR("ivaretenido") <> 0 Or ISR <> 0 Or IvaRetenido <> 0 Then
                If DR("iva") <> 0 Or DR("ieps") <> 0 Then
                    If DR("iva") <> 0 Then
                        ImpXML += Format(DR("precio"), "0.00####") + "|"
                        ImpXML += "002|"
                        ImpXML += "Tasa|"
                        ImpXML += Format(DR("iva") / 100, "0.000000") + "|"
                        ImpXML += Format(DR("precio") * DR("iva") / 100, "0.00####") + "|"
                    End If
                    If DR("ieps") <> 0 Then
                        ImpXML += Format(DR("precio"), "0.00####") + "|"
                        ImpXML += "003|"
                        ImpXML += "Tasa|"
                        ImpXML += Format(DR("ieps") / 100, "0.000000") + "|"
                        ImpXML += Format(DR("precio") * DR("ieps") / 100, "0.00####") + "|"
                    End If
                End If
                If ISR <> 0 Or DR("ivaretenido") <> 0 Or IvaRetenido <> 0 Then
                    If ISR <> 0 Then
                        ImpXML += Format(DR("precio"), "0.00####") + "|"
                        ImpXML += "001|"
                        ImpXML += "Tasa|"
                        ImpXML += Format(ISR / 100, "0.000000") + "|"
                        ImpXML += Format(DR("precio") * ISR / 100, "0.00####") + "|"
                    End If
                    If DR("ivaretenido") <> 0 Or IvaRetenido <> 0 Then
                        ImpXML += Format(DR("precio"), "0.00####") + "|"
                        ImpXML += "002|"
                        ImpXML += "Tasa|"
                        ImpXML += Format((DR("ivaretenido") + IvaRetenido) / 100, "0.000000") + "|"
                        ImpXML += Format(DR("precio") * (DR("ivaretenido") + IvaRetenido) / 100, "0.00####") + "|"
                    End If
                End If
            End If

            AduanaCont = 0
            AduanaXML = ""
            For Each ad As InfoAduana In AduanaCol
                If ad.IdDetalle = DR("iddetalle") Then
                    AduanaXML += ad.YValidacion + "----" + ad.ClaveAduana + "----" + ad.Patente + "----" + ad.Numero + "|"
                    AduanaCont += 1
                End If
            Next
            
            CO += ImpXML + AduanaXML
        End While
        DR.Close()

        If TotalIva <> 0 Or TotalIeps <> 0 Or TotalIvaRetenido <> 0 Then

            If ISR <> 0 Or IvaRetenido <> 0 Then
                If ISR <> 0 Then
                    CO += "001|"
                    If pEsEgreso = 0 Then
                        CO += Format(TotalISR, "#0.00####") + "|"
                    Else
                        CO += Format(If(TotalISR >= 0, TotalISR, TotalISR * -1), "#0.00####") + "|"
                    End If
                End If
                If IvaRetenido <> 0 Then
                    CO += "002|"
                    CO += Format(TotalIvaRetenido, "#0.00####") + "|"
            End If

                If ISR <> 0 Or IvaRetenido <> 0 Then
                    CO += Format(TotalISR + TotalIvaRetenido, "#0.00####") + "|"
                End If
            End If
            If TotalIva <> 0 Or TotalIeps <> 0 Then
                Ivas.Clear()
                IvasImporte.Clear()
                Dim Diodescuento As Boolean = False

                DR = DaIvas(ID)
            
            While DR.Read
                If Ivas.Contains(DR("iva").ToString) = False Then
                    Ivas.Add(DR("iva"), DR("iva").ToString)
                End If
                If IvasImporte.Contains(DR("iva").ToString) = False Then
                    If DR("precio") > 0 And Diodescuento = False Then
                            IvasImporte.Add((DR("precio")) * (DR("iva") / 100), DR("iva").ToString)
                        Diodescuento = True
                    Else
                        IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                    End If
                Else
                    IAnt = IvasImporte(DR("iva").ToString)
                    IvasImporte.Remove(DR("iva").ToString)
                    If DR("precio") > 0 And Diodescuento = False Then
                            IvasImporte.Add(IAnt + (DR("precio")) * (DR("iva") / 100), DR("iva").ToString)
                        Diodescuento = True
                    Else
                        IvasImporte.Add(IAnt + DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                    End If
                End If
            End While
            DR.Close()
            For Each I As Double In Ivas
                If IvasImporte(I.ToString) > 0 Then
                    CO += "002|"
                    CO += "Tasa|"
                    CO += Format(I / 100, "0.000000") + "|"
                    If pEsEgreso = 0 Then
                        CO += Format(IvasImporte(I.ToString), "#0.00####") + "|"
                    Else
                        CO += Format(If(IvasImporte(I.ToString) >= 0, IvasImporte(I.ToString), IvasImporte(I.ToString) * -1), "#0.00####") + "|"
                    End If
                End If
            Next

            Ivas.Clear()
            IvasImporte.Clear()
                DR = DaIvasIEPS(ID)
            While DR.Read
                If Ivas.Contains(DR("ieps").ToString) = False Then
                    Ivas.Add(DR("ieps"), DR("ieps").ToString)
                End If
                If IvasImporte.Contains(DR("ieps").ToString) = False Then
                    IvasImporte.Add(DR("precio") * (DR("ieps") / 100), DR("ieps").ToString)
                Else
                    IAnt = IvasImporte(DR("ieps").ToString)
                    IvasImporte.Remove(DR("ieps").ToString)
                    IvasImporte.Add(IAnt + (DR("precio") * (DR("ieps") / 100)), DR("ieps").ToString)
                End If
            End While
            DR.Close()
            For Each I As Double In Ivas
                If IvasImporte(I.ToString) > 0 Then
                    CO += "003|"
                    CO += "Tasa|"
                    CO += Format(I / 100, "0.000000") + "|"
                    If pEsEgreso = 0 Then
                        CO += Format(IvasImporte(I.ToString), "#0.00####") + "|"
                    Else
                        CO += Format(If(IvasImporte(I.ToString) >= 0, IvasImporte(I.ToString), IvasImporte(I.ToString) * -1), "#0.00####") + "|"
                    End If
                End If
            Next


                If TotalIva <> 0 Or TotalIeps <> 0 Then
                    CO += Format(TotalIva + TotalIeps, "#0.00####") + "|"
                End If
            


        End If
        End If

        'If ImpLocales.Count > 0 Then
        '    If pEsEgreso = 0 Then
        '        CO += "1.0|" + Format(TotalRetLocal, "#0.00####") + "|" + Format(TotalTrasLocal, "#0.00####") + "|"
        '    Else
        '        CO += "1.0|" + Format(If(TotalRetLocal >= 0, TotalRetLocal, TotalRetLocal * -1), "#0.00####") + "|" + Format(If(TotalTrasLocal >= 0, TotalTrasLocal, TotalTrasLocal * -1), "#0.00####") + "|"
        '    End If
        '    For Each Im As Implocal In ImpLocales
        '        If Im.Tipo = 1 Then
        '            CO += Im.Nombre + "|" + Format(Im.Tasa, "#0.00####") + "|" + Format(Im.Importe, "#0.00####") + "|"
        '        Else
        '            If pEsEgreso = 0 Then
        '                CO += Im.Nombre + "|" + Format(Im.Tasa, "#0.00####") + "|" + Format(Im.Importe, "#0.00####") + "|"
        '            Else
        '                CO += Im.Nombre + "|" + Format(Im.Tasa, "#0.00####") + "|" + Format(If(Im.Importe >= 0, Im.Importe, Im.Importe * -1), "#0.00####") + "|"
        '            End If
        '        End If
        '    Next
        'End If

        'If IDNotaP <> 0 Then
        '    CO += NotaP.CreaCadenaOriginal(IDNotaP)
        'End If
        'If pCadenaOriginalComp <> "" Then
        '    CO += pCadenaOriginalComp
        'End If
        CO = Replace(CO, vbCrLf, "")
        While CO.IndexOf("||") <> -1
            CO = Replace(CO, "||", "|")
        End While
        While CO.IndexOf("  ") <> -1
            CO = Replace(CO, "  ", " ")
        End While
        Replace(CO, "----", "  ")
        CO = Replace(CO, vbTab, "")
        CO = "|" + CO + "|"
        en.GuardaArchivoTexto("co.txt", CO, System.Text.Encoding.Default)
        Return CO
    End Function
    Public Function CreaXMLi33(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String, ByVal pIdEmpresa As Integer, pXMLINE As String, pEsEgreso As Byte) As String
        Dim en As New Encriptador
        Dim XMLDoc As String
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        Dim IAnt As Double
        XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>"
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        XMLDoc += "<cfdi:Comprobante "
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(IdSucursal, pIdEmpresa, True)
        en.Leex509(Archivos.RutaCer)
        ID = pIdVenta
        LlenaDatos()
        Dim FP As New dbFormasdePago(IdFormadePago, Comm.Connection)
        If TipodeCambio = 0 Then TipodeCambio = 1
        DaTotal(ID, IdConversion)
        Dim Sucursal As New dbSucursales(IdSucursal, MySqlcon)
        XMLDoc += "Version=""3.3"" "
        If Serie <> "" Then XMLDoc += "Serie=""" + Replace(Replace(Replace(Replace(Replace(Serie, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
        XMLDoc += "Folio=""" + Folio.ToString + """ "
        XMLDoc += "Fecha=""" + Replace(Fecha, "/", "-") + "T" + Hora + """ "
        If Sucursal.RFC <> "SUL010720JN8" Then
            XMLDoc += "Sello=""" + pSelloDigital + """ "
        Else
            XMLDoc += "Sello="""" "
        End If

        'Dim strMetodos As String = ""
        'Dim MeP As New dbVentasAddMetodos(Comm.Connection)
        'DR = MeP.ConsultaReader(0, ID)
        'DR.Read()
        'While DR.Read()
        '    If strMetodos <> "" Then strMetodos += ","
        '    If DR("clavesat") < 1000 Then
        '        strMetodos += Format(DR("clavesat"), "00")
        '    Else
        '        strMetodos += "NA"
        '    End If
        'End While
        'DR.Close()

        XMLDoc += "FormaPago=""99"" "

        If NoCertificado <> "" Then XMLDoc += "NoCertificado=""" + NoCertificado + """ "
        If Sucursal.RFC <> "SUL010720JN8" Then
            XMLDoc += "Certificado=""" + en.Certificado64 + """ "
        Else
            XMLDoc += "Certificado="""" "
        End If
        'xmldoc+="CondicionesDePago="""""

        XMLDoc += "SubTotal=""" + Format(Subtototal, "#0.00####") + """ "
        

        'If NoAprobacion <> "" Then XMLDoc += "noAprobacion=""" + NoAprobacion + """" + vbCrLf
        'If YearAprobacion <> "" Then XMLDoc += "anoAprobacion=""" + YearAprobacion + """" + vbCrLf
        'If Descuento + DescuentoG2 > 0 Then
        '    If pEsEgreso = 0 Then
        '        XMLDoc += "Descuento=""" + Format(Descuento + DescuentoG2, "#0.00####") + """ "
        '    Else
        '        XMLDoc += "Descuento=""" + Format(If(Descuento + DescuentoG2 >= 0, Descuento + DescuentoG2, (Descuento + DescuentoG2) * -1), "#0.00####") + """ "
        '    End If
        'End If

        'Tipo deCambio nuevo
        If IdConversion <> 2 Then
            Dim Moneda As New dbMonedas(IdConversion, Comm.Connection)
            XMLDoc += "Moneda=""" + Moneda.Abreviatura + """ "
            XMLDoc += "TipoCambio=""" + Format(TipodeCambio, "#0.00####") + """ "
        Else
            XMLDoc += "Moneda=""MXN"" "
        End If

        XMLDoc += "Total=""" + Format(TotalVenta, "#0.00####") + """ "
        XMLDoc += "TipoDeComprobante=""E"" "
            XMLDoc += "MetodoPago=""PUE"" "

        If Sucursal.CP2 <> "" Then
            XMLDoc += "LugarExpedicion=""" + Sucursal.CP2 + """ "
        Else
            XMLDoc += "LugarExpedicion=""" + Sucursal.CP + """ "
        End If
        'Confirmacion
        'If NoConfirmacion <> "" Then XMLDoc += " Confirmacion=""" + NoConfirmacion + """"
        

        XMLDoc += "xmlns:cfdi=""http://www.sat.gob.mx/cfd/3"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" "
        XMLDoc += "xsi:schemaLocation=""http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd"
        
        XMLDoc += """ "
       
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        XMLDoc += ">"

        'CFDIS relacionados aqui'
        XMLDoc += "<cfdi:CfdiRelacionados TipoRelacion=""03"">"
        'whiles docs
        XMLDoc += "<cfdi:CfdiRelacionado UUID=""" + UUIDVenta + """/>"
        'end while
        XMLDoc += "</cfdi:CfdiRelacionados>"

        XMLDoc += "<cfdi:Emisor Rfc=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Nombre=""" + Replace(Replace(Replace(Replace(Replace(Sucursal.NombreFiscal, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """"
        XMLDoc += " RegimenFiscal=""" + Sucursal.ClaveRegimen.ToString + """"
        XMLDoc += "/>"


        XMLDoc += "<cfdi:Receptor Rfc=""" + Replace(Replace(Replace(Replace(Replace(Cliente.RFC, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ Nombre=""" + Replace(Replace(Replace(Replace(Replace(Cliente.Nombre, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """"
        If Cliente.RFC = "XEXX010101000" Then
            XMLDoc += " ResidenciaFiscal=""" + Cliente.cPais + """"
        End If
        If pXMLINE.Contains("cce11:ComercioExterior") = True Then
            XMLDoc += " NumRegIdTrib=""" + Cliente.RegIdTrib + """"
        End If
        XMLDoc += " UsoCFDI=""G02"""
        XMLDoc += "/>"
        

        XMLDoc += "<cfdi:Conceptos>"



        Dim AduanaCol As New Collection
        Dim AduanaCont As Integer
        Dim AduanaXML As String
        Dim PredialXML As String
        Dim IA As New dbInventarioAduana(Comm.Connection)
        'Dim VA As New dbventasaduana(Comm.Connection)
        'If IA.HayViejaAduanaGlobal(ID) Then
        DR = IA.ConsultaAduanaDevReader(ID)
        While DR.Read
            AduanaCol.Add(New InfoAduana(DR("numero"), DR("fecha"), DR("aduana"), DR("iddetalle"), DR("yvalidacion"), DR("claveaduana"), DR("patente")))
        End While
        DR.Close()
        'End If


        Dim VI As New dbDevolucionesDetalles(MySqlcon)
        DR = VI.ConsultaReader(ID)
        Dim PrecioTemp As Double = 0
        Dim ImpXML As String = ""
        While DR.Read
            If DR("noimpimporte") <> 0 Then
                PrecioTemp = DR("noimpimporte")
            Else
                PrecioTemp = DR("precio")
            End If
            'If DR("cantidad") <> 0 And PrecioTemp <> 0 Then
            XMLDoc += "<cfdi:Concepto "
            XMLDoc += "ClaveProdServ=""" + DR("cproductoserv") + """ "
            XMLDoc += "NoIdentificacion=""" + Replace(Replace(Replace(Replace(Replace(DR("clave"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            XMLDoc += "Cantidad=""" + DR("cantidad").ToString + """ "
            XMLDoc += "ClaveUnidad=""" + DR("cunidad") + """ "
            XMLDoc += "Unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            Dim Des As String
            Des = Trim(Replace(DR("descripcion"), vbCrLf, ""))
            While Des.IndexOf("  ") <> -1
                Des = Replace(Des, "  ", " ")
            End While
            Des = Replace(Des, vbTab, "")
            XMLDoc += "Descripcion=""" + Replace(Replace(Replace(Replace(Replace(Replace(Des, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ "
            'If DR("idmoneda") <> 2 Then
            '    XMLDoc += "valorUnitario=""" + Format((DR("precio") * TipodeCambio) / DR("cantidad"), "#0.00") + """" + vbCrLf
            '    XMLDoc += "importe=""" + Format(DR("precio") * TipodeCambio, "#0.00") + """" + vbCrLf
            '    XMLDoc += "/> " + vbCrLf
            'Else
            If pEsEgreso = 0 Then
                If DR("cantidad") <> 0 Then
                    XMLDoc += "ValorUnitario=""" + Format((PrecioTemp + DR("cdescuento")) / DR("cantidad"), "#0.00####") + """ "
                    XMLDoc += "Importe=""" + Format(PrecioTemp + DR("cdescuento"), "#0.00####") + """ "
                Else
                    XMLDoc += "ValorUnitario=""0.00"" "
                    XMLDoc += "Importe=""0.00"" "
                End If
            Else
                If DR("cantidad") <> 0 Then
                    XMLDoc += "ValorUnitario=""" + Format(If((PrecioTemp + DR("cdescuento")) / DR("cantidad") >= 0, (PrecioTemp + DR("cdescuento")) / DR("cantidad"), ((PrecioTemp + DR("cdescuento")) / DR("cantidad")) * -1), "#0.00####") + """ "
                    XMLDoc += "Importe=""" + Format(If(PrecioTemp + DR("cdescuento") >= 0, PrecioTemp + DR("cdescuento"), (PrecioTemp + DR("cdescuento")) * -1), "#0.00####") + """ "
                Else
                    XMLDoc += "ValorUnitario=""0.00"" "
                    XMLDoc += "Importe=""0.00"" "
                End If
            End If
            'If DR("cdescuento") <> 0 Then XMLDoc += "Descuento=""" + Format(DR("cdescuento"), "#0.00####") + """ "
            ImpXML = ""
            If DR("iva") <> 0 Or DR("ieps") <> 0 Or DR("ivaretenido") <> 0 Or ISR <> 0 Or IvaRetenido <> 0 Then
                ImpXML += "<cfdi:Impuestos>"
                If DR("iva") <> 0 Or DR("ieps") <> 0 Then
                    ImpXML += "<cfdi:Traslados>"
                    If DR("iva") <> 0 Then
                        ImpXML += "<cfdi:Traslado "
                        ImpXML += "Base=""" + Format(DR("precio"), "0.00####") + """ "
                        ImpXML += "Impuesto=""002"" "
                        ImpXML += "TipoFactor=""Tasa"" "
                        ImpXML += "TasaOCuota=""" + Format(DR("iva") / 100, "0.000000") + """ "
                        ImpXML += "Importe=""" + Format((DR("precio")) * DR("iva") / 100, "0.00####") + """/>"
                    End If
                    If DR("ieps") <> 0 Then
                        ImpXML += "<cfdi:Traslado "
                        ImpXML += "Base=""" + Format(DR("precio"), "0.00####") + """ "
                        ImpXML += "Impuesto=""003"" "
                        ImpXML += "TipoFactor=""Tasa"" "
                        ImpXML += "TasaOCuota=""" + Format(DR("ieps") / 100, "0.000000") + """ "
                        ImpXML += "Importe=""" + Format((DR("precio")) * DR("ieps") / 100, "0.00####") + """/>"
                    End If
                    ImpXML += "</cfdi:Traslados>"
                End If
                If ISR <> 0 Or DR("ivaretenido") <> 0 Or IvaRetenido <> 0 Then
                    ImpXML += "<cfdi:Retenciones>"
                    If ISR <> 0 Then
                        ImpXML += "<cfdi:Retencion "
                        ImpXML += "Base=""" + Format(DR("precio"), "0.00####") + """ "
                        ImpXML += "Impuesto=""001"" "
                        ImpXML += "TipoFactor=""Tasa"" "
                        ImpXML += "TasaOCuota=""" + Format(ISR / 100, "0.000000") + """ "
                        ImpXML += "Importe=""" + Format(DR("precio") * ISR / 100, "0.00####") + """/>"
                    End If
                    If DR("ivaretenido") <> 0 Or IvaRetenido Then
                        ImpXML += "<cfdi:Retencion "
                        ImpXML += "Base=""" + Format(DR("precio"), "0.00####") + """ "
                        ImpXML += "Impuesto=""002"" "
                        ImpXML += "TipoFactor=""Tasa"" "
                        ImpXML += "TasaOCuota=""" + Format((DR("ivaretenido") + IvaRetenido) / 100, "0.000000") + """ "
                        ImpXML += "Importe=""" + Format(DR("precio") * (DR("ivaretenido") + IvaRetenido) / 100, "0.00####") + """/>"
                    End If
                    ImpXML += "</cfdi:Retenciones>"
                End If
                ImpXML += "</cfdi:Impuestos>"
            End If

            AduanaCont = 0
            AduanaXML = ""
            For Each ad As InfoAduana In AduanaCol
                If ad.IdDetalle = DR("idventasinventario") Then
                    AduanaXML += "<cfdi:InformacionAduanera "
                    AduanaXML += "NumeroPedimento=""" + ad.YValidacion + " " + ad.ClaveAduana + "  " + ad.Patente + "  " + ad.Numero + """/>"
                    AduanaCont += 1
                End If
            Next
            'PredialXML = ""
            'If DR("predial") <> "" And ConPredialenXML Then
            '    PredialXML = "<cfdi:CuentaPredial Numero=""" + Replace(Replace(Replace(Replace(Replace(Replace(Trim(DR("predial")), vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """ />"
            'End If
            If AduanaCont = 0 And ImpXML = "" Then
                XMLDoc += "/> "
            Else
                XMLDoc += ">" + ImpXML + AduanaXML + PredialXML + "</cfdi:Concepto>"
            End If
            'End If


            'End If
        End While
        DR.Close()

        XMLDoc += "</cfdi:Conceptos>"


        If TotalIva <> 0 Or TotalIeps <> 0 Or TotalIvaRetenido <> 0 Then


            XMLDoc += "<cfdi:Impuestos "
            If ISR <> 0 Or IvaRetenido <> 0 Then
                XMLDoc += "TotalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00####") + """ "
            End If
            If TotalIva <> 0 Or TotalIeps <> 0 Then
                XMLDoc += "TotalImpuestosTrasladados=""" + Format(TotalIva + TotalIeps, "#0.00####") + """ "
            End If
        
        XMLDoc += ">"
            If ISR <> 0 Or IvaRetenido <> 0 Then
                XMLDoc += "<cfdi:Retenciones>"
                If ISR <> 0 Then
                    XMLDoc += "<cfdi:Retencion Impuesto=""001"" "
                    If pEsEgreso = 0 Then
                        XMLDoc += "Importe=""" + Format(TotalISR, "#0.00####") + """/>"
                    Else
                        XMLDoc += "Importe=""" + Format(If(TotalISR >= 0, TotalISR, TotalISR * -1), "#0.00####") + """/>"
                    End If
                End If

                If IvaRetenido <> 0 Then
                    XMLDoc += "<cfdi:Retencion Impuesto=""002"" "

                    XMLDoc += "Importe=""" + Format(TotalIvaRetenido, "#0.00####") + """/>"
            End If

                XMLDoc += "</cfdi:Retenciones>"

            End If
        If TotalIva <> 0 Or TotalIeps <> 0 Then
            XMLDoc += "<cfdi:Traslados>"
            Ivas.Clear()
            IvasImporte.Clear()
            Dim Diodescuento As Boolean = False

                DR = DaIvas(ID)
            
            While DR.Read
                If Ivas.Contains(DR("iva").ToString) = False Then
                    Ivas.Add(DR("iva"), DR("iva").ToString)
                End If
                If IvasImporte.Contains(DR("iva").ToString) = False Then
                        IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                Else
                    IAnt = IvasImporte(DR("iva").ToString)
                    IvasImporte.Remove(DR("iva").ToString)
                        IvasImporte.Add(IAnt + DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                    End If
                End While
            DR.Close()
            For Each I As Double In Ivas
                If IvasImporte(I.ToString) > 0 Then
                    XMLDoc += "<cfdi:Traslado Impuesto=""002"" "
                    XMLDoc += "TipoFactor=""Tasa"" "
                    XMLDoc += "TasaOCuota=""" + Format(I / 100, "0.000000") + """ "
                    If pEsEgreso = 0 Then
                        XMLDoc += "Importe=""" + Format(IvasImporte(I.ToString), "#0.00####") + """ />"
                    Else
                        XMLDoc += "Importe=""" + Format(If(IvasImporte(I.ToString) >= 0, IvasImporte(I.ToString), IvasImporte(I.ToString) * -1), "#0.00####") + """ />"
                    End If
                End If
            Next

            Ivas.Clear()
            IvasImporte.Clear()
                DR = DaIvasIEPS(ID)
            While DR.Read
                If Ivas.Contains(DR("ieps").ToString) = False Then
                    Ivas.Add(DR("ieps"), DR("ieps").ToString)
                End If
                If IvasImporte.Contains(DR("ieps").ToString) = False Then
                    
                    IvasImporte.Add(DR("precio") * (DR("ieps") / 100), DR("ieps").ToString)

                Else
                    IAnt = IvasImporte(DR("ieps").ToString)
                    IvasImporte.Remove(DR("ieps").ToString)
                    
                    IvasImporte.Add(IAnt + (DR("precio") * (DR("ieps") / 100)), DR("ieps").ToString)


                End If
            End While
            DR.Close()
            For Each I As Double In Ivas
                If IvasImporte(I.ToString) > 0 Then
                    XMLDoc += "<cfdi:Traslado Impuesto=""003"" "
                    XMLDoc += "TipoFactor=""Tasa"" "
                    XMLDoc += "TasaOCuota=""" + Format(I / 100, "0.000000") + """ "
                    If pEsEgreso = 0 Then
                        XMLDoc += "Importe=""" + Format(IvasImporte(I.ToString), "#0.00####") + """ />"
                    Else
                        XMLDoc += "Importe=""" + Format(If(IvasImporte(I.ToString) >= 0, IvasImporte(I.ToString), IvasImporte(I.ToString) * -1), "#0.00####") + """ />"
                    End If
                End If
            Next
            XMLDoc += "</cfdi:Traslados>"
        End If
        XMLDoc += "</cfdi:Impuestos>"
        End If
        XMLDoc += "</cfdi:Comprobante>"
        Return XMLDoc

    End Function

End Class
