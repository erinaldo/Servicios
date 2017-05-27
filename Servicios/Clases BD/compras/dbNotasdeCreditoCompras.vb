Public Class dbNotasdeCreditoCompras
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Public ID As Integer
    Public IdProveedor As Integer
    Public Fecha As String
    Public Proveedor As dbproveedores
    Public Folio As String
    Public Iva As Double
    Public TotalaPagar As Double
    Public Total As Double
    Public Hora As String
    Public Estado As Byte
    'Public Desglosar As Byte
    Public IdSucursal As Integer
    Public Serie As String
    Public Folioi As Integer
    Public FolioCFDI As String
    Public Subtotal As Double
    Public TotalIva As Double
    Public TotalNota As Double
    Public TipodeCambio As Double
    'Public NoAprobacion As String
    'Public YearAprobacion As String
    'Public NoCertificado As String
    'Public EsElectronica As Byte
    Public IdMoneda As Integer
    Public Aplicado As Double
    Public HoraCancelado As String
    Public FechaCancelado As String
    Public Comentario As String
    'Public Enum TiposFactura As Byte
    '    Enproceso = 0
    '    Facturado = 1
    '    Cancelado = 2
    'End Enum
    Public IdConcepto As Integer

    Public Sub New(ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection)
        ID = -1
        IdProveedor = -1
        Fecha = ""
        Hora = ""
        Folio = 0

        Iva = 0
        TotalaPagar = 0
        Total = 0
        Estado = 0
        IdSucursal = 0
        
        TipodeCambio = 0
        IdMoneda = 0
        Aplicado = 0
        HoraCancelado = 0
        FechaCancelado = 0
        IdConcepto = 0
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
        Comm.CommandText = "select * from tblnotasdecreditocompras where idnota=" + ID.ToString
        DReader = Comm.ExecuteReader
        If DReader.Read() Then
            IdProveedor = DReader("idproveedor")
            Fecha = DReader("fecha")
            Folio = DReader("folio")
            'Desglosar = DReader("desglosar")
            Iva = DReader("iva")
            TotalaPagar = DReader("totalapagar")
            Total = DReader("total")
            Hora = DReader("hora")
            Estado = DReader("estado")
            IdSucursal = DReader("idsucursal")
            'Serie = DReader("serie")
            TipodeCambio = DReader("tipodecambio")
            'NoAprobacion = DReader("noaprobacion")
            'YearAprobacion = DReader("yearaprobacion")
            'NoCertificado = DReader("nocertificado")
            'EsElectronica = DReader("eselectronica")
            IdMoneda = DReader("idmoneda")
            Aplicado = DReader("aplicado")
            FechaCancelado = DReader("fechacancelado")
            HoraCancelado = DReader("horacancelado")
            IdConcepto = DReader("idconcepto")
            Comentario = DReader("comentarionc")
            Folioi = DReader("folioi")
            FolioCFDI = DReader("foliocfdi")
            Serie = DReader("serie")
        End If
        DReader.Close()
        Proveedor = New dbproveedores(IdProveedor, Comm.Connection)
    End Sub
    'Public Function ExisteFolio(ByVal pfolio As Integer, Optional ByVal idventa As Integer = -1) As Boolean
    '    Folio = pfolio
    '    Comm.CommandText = "select count(folio) from tblventas where folio=" + Folio.ToString + If(idventa = -1, "", " and idventa<>" + CStr(idventa))
    '    If Comm.ExecuteScalar = 0 Then Return False Else Return True
    'End Function

    Public Sub Guardar(ByVal pIdCliente As Integer, ByVal pFecha As String, ByVal pFolio As String, ByVal pIva As Double, ByVal pidSucursal As Integer, ByVal pTipoDeCambio As Double, ByVal pIdMoneda As Integer, ByVal pIdConcepto As Integer, ByVal pSerie As String, ByVal pFolioi As Integer, ByVal pFolioCFDI As String)
        IdProveedor = pIdCliente
        Fecha = pFecha
        Folio = pFolio
        'Desglosar = pDesglosar
        Iva = pIva
        IdSucursal = pidSucursal
        'Serie = pSerie
        TipodeCambio = pTipoDeCambio
        'NoAprobacion = pNoAprobacion
        'NoCertificado = pNoCertificado
        'YearAprobacion = pYearAprobacion
        'EsElectronica = pEselectronica
        IdMoneda = pIdMoneda
        IdConcepto = pIdConcepto
        Comm.CommandText = "insert into tblnotasdecreditocompras(idproveedor,fecha,folio,total,hora,estado,iva,totalapagar,idsucursal,tipodecambio,idmoneda,aplicado,fechacancelado,horacancelado,idconcepto,comentarionc,serie,folioi,foliocfdi,idUsuarioAlta,fechaAlta,horaAlta,idUsuarioCambio,fechaCambio,horaCambio) values(" + IdProveedor.ToString + ",'" + Fecha + "','" + Replace(Folio, "'", "''") + "',0,'" + Format(TimeOfDay, "HH:mm:ss") + "'," + CStr(Estados.SinGuardar) + "," + Iva.ToString + ",0," + IdSucursal.ToString + "," + TipodeCambio.ToString + "," + IdMoneda.ToString + ",0,'',''," + IdConcepto.ToString + ",'','" + Replace(pSerie, "'", "''") + "'," + pFolioi.ToString + ",'" + Replace(pFolioCFDI, "'", "''") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "'," + GlobalIdUsuario.ToString() + ",'" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + TimeOfDay.ToString("HH:mm:ss") + "')"
        Comm.ExecuteNonQuery()
        Comm.CommandText = "select max(idnota) from tblnotasdecreditocompras"
        ID = Comm.ExecuteScalar
    End Sub
    Public Sub Modificar(ByVal pID As Integer, ByVal pFecha As String, ByVal pFolio As String, ByVal pIva As Double, ByVal pEstado As Byte, ByVal pTotal As Double, ByVal pTotalaPagar As Double, ByVal pIdProveedor As Integer, ByVal pTipodeCambio As Double, ByVal pÌdMoneda As Integer, ByVal pIdConcepto As Integer, ByVal pComentario As String, ByVal pSerie As String, ByVal pFolioi As Integer, ByVal pFolioCFDI As String)
        ID = pID
        Fecha = pFecha
        Folio = pFolio

        Iva = pIva
        Estado = pEstado
        Total = pTotal
        TotalaPagar = pTotalaPagar
        IdProveedor = pIdProveedor

        TipodeCambio = pTipodeCambio

        IdMoneda = pÌdMoneda
        Estado = pEstado
        IdConcepto = pIdConcepto
        Comentario = pComentario
        Comm.CommandText = "update tblnotasdecreditocompras set fecha='" + Fecha + "',folio='" + Replace(Folio, "'", "''") + "',iva=" + Iva.ToString + ",estado=" + Estado.ToString + ",total=" + Total.ToString + ",totalapagar=" + TotalaPagar.ToString + ",idproveedor=" + IdProveedor.ToString + ",tipodecambio=" + TipodeCambio.ToString + ",idmoneda=" + IdMoneda.ToString + ",fechacancelado='" + Format(Date.Now, "yyyy/MM/dd") + "',horacancelado='" + Format(TimeOfDay, "HH:mm:ss") + "',idconcepto=" + IdConcepto.ToString + ",comentarionc='" + Replace(Comentario, "'", "''") + "',serie='" + Replace(pSerie, "'", "''") + "',folioi=" + pFolioi.ToString + ",foliocfdi='" + Replace(pFolioCFDI, "'", "''") + "', idUsuarioCambio=" + GlobalIdUsuario.ToString() + ", fechaCambio='" + DateTime.Now.ToString("yyyy/MM/dd") + "', horaCambio='" + TimeOfDay.ToString("HH:mm:ss") + "' where idnota=" + ID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub ActualizaComentario(ByVal pidnota As Integer, ByVal pTexto As String)
        Comm.CommandText = "update tblnotasdecreditocompras set comentarionc='" + Replace(pTexto, "'", "''") + "' where idnota=" + pidnota.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Sub Eliminar(ByVal pID As Integer)
        Comm.CommandText = "delete from tblnotasdecreditocompras where idnota=" + pID.ToString
        Comm.ExecuteNonQuery()
    End Sub
    Public Function Consulta(ByVal pFecha As String, ByVal pFecha2 As String, Optional ByVal pNombreClave As String = "", Optional ByVal pFolio As String = "", Optional ByVal pEstado As Byte = Estados.Inicio, Optional ByVal pSinAplicar As Boolean = False) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblnotasdecreditocompras.idnota,tblnotasdecreditocompras.fecha,tblnotasdecreditocompras.folio,tblproveedores.clave,tblproveedores.nombre as Cliente,tblnotasdecreditocompras.totalapagar,tblnotasdecreditocompras.aplicado,case tblnotasdecreditocompras.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado  from tblnotasdecreditocompras inner join tblproveedores on tblnotasdecreditocompras.idproveedor=tblproveedores.idproveedor where fecha>='" + pFecha + "' and fecha<='" + pFecha2 + "'"
        If pNombreClave <> "" Then
            Comm.CommandText += " and concat(tblproveedores.clave,tblproveedores.nombre) like '%" + Replace(pNombreClave, "'", "''") + "%'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and tblnotasdecreditocompras.folio  like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If pEstado <> Estados.Inicio Then
            Comm.CommandText += " and tblnotasdecreditocompras.estado=" + pEstado.ToString
        Else
            Comm.CommandText += " and tblnotasdecreditocompras.estado<>1"
        End If
        If pSinAplicar Then
            Comm.CommandText += " and tblnotasdecreditocompras.totalapagar-tblnotasdecreditocompras.aplicado>0"
        End If
        Comm.CommandText += " order by tblnotasdecreditocompras.fecha desc,tblnotasdecreditocompras.serie,tblnotasdecreditocompras.folio desc"
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblnotasdecreditocompras")
        Return DS.Tables("tblnotasdecreditocompras").DefaultView
    End Function

    Public Function ConsultaxProveedor(ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdProveedor As Integer, ByVal pPorFecha As Boolean, ByVal pFolio As String, ByVal pOrdenDecendente As Boolean) As DataView
        Dim DS As New DataSet
        Comm.CommandText = "select tblnotasdecreditocompras.idnota,tblnotasdecreditocompras.fecha,tblnotasdecreditocompras.folio,tblproveedores.clave,tblproveedores.nombre as Cliente,tblnotasdecreditocompras.totalapagar,tblnotasdecreditocompras.aplicado,case tblnotasdecreditocompras.estado when 2 then 'Pendiente' when 3 then 'Guardado' when 4 then 'Cancelada' end  as sestado  from tblnotasdecreditocompras inner join tblproveedores on tblnotasdecreditocompras.idproveedor=tblproveedores.idproveedor where tblnotasdecreditocompras.estado<>1"
        If pPorFecha Then
            Comm.CommandText += " fecha>='" + pFecha1 + "' and fecha<='" + pFecha2 + "'"
        End If
        If pFolio <> "" Then
            Comm.CommandText += " and tblnotasdecreditocompras.folio like '%" + Replace(pFolio, "'", "''") + "%'"
        End If
        If IdProveedor <> 0 Then
            Comm.CommandText += " and tblnotasdecreditocompras.idproveedor=" + pIdProveedor.ToString
        End If
        If pOrdenDecendente Then
            Comm.CommandText += " order by tblnotasdecreditocompras.folio desc,tblnotasdecreditocompras.fecha desc"
        Else
            Comm.CommandText += " order by tblnotasdecreditocompras.folio,tblnotasdecreditocompras.fecha"
        End If
        Dim DA As New MySql.Data.MySqlClient.MySqlDataAdapter(Comm)
        DA.Fill(DS, "tblnotasdecreditocompras")
        Return DS.Tables("tblnotasdecreditocompras").DefaultView
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

    Public Function DaTotal(ByVal pidNota As Integer, ByVal pidMoneda As Integer) As Double
        Dim DReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim IDs As New Collection
        Dim Precio As Double
        Dim IdMonedaC As Integer
        Dim Total As Double = 0
        'Dim Encontro As Double
        Dim iIva As Double
        Dim Cont As Integer = 1
        Dim iTipoCambio As Double
        'Dim iIsr As Double
        'Dim iIvaRetenido As Double
        Subtotal = 0
        TotalIva = 0
        TotalNota = 0
        Comm.CommandText = "select tipodecambio from tblnotasdecreditocompras where idnota=" + pidNota.ToString
        iTipoCambio = Comm.ExecuteScalar
        'Comm.CommandText = "select isr from tblventas where idventa=" + pidNota.ToString
        'iIsr = Comm.ExecuteScalar
        'Comm.CommandText = "select ivaretenido from tblventas where idventa=" + pidVenta.ToString
        'iIvaRetenido = Comm.ExecuteScalar
        '+++++++++++++++++++++++++++++++++++++++Total por Articulos ++++++++++++++++++++++++++++++++++++
        Comm.CommandText = "select iddetalle from tblnotasdecreditodetallesc where idnota=" + pidNota.ToString
        DReader = Comm.ExecuteReader
        While DReader.Read
            IDs.Add(DReader("iddetalle"))
        End While
        DReader.Close()
        While Cont <= IDs.Count
            Comm.CommandText = "select precio from tblnotasdecreditodetallesc where iddetalle=" + IDs.Item(Cont).ToString
            Precio = Comm.ExecuteScalar
            Comm.CommandText = "select iva from tblnotasdecreditodetallesc where iddetalle=" + IDs.Item(Cont).ToString
            iIva = Comm.ExecuteScalar
            Comm.CommandText = "select idmoneda from tblnotasdecreditodetallesc where iddetalle=" + IDs.Item(Cont).ToString
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
            TotalIva += Precio - (Precio / (1 + (iIva / 100)))
            Cont += 1
        End While
        'TotalISR = Subtototal * (iIsr / 100)
        'TotalIvaRetenido = Subtototal * (iIvaRetenido / 100)
        TotalNota = Subtotal '- TotalISR - TotalIvaRetenido
        Subtotal = Subtotal - TotalIva
        'Subtotal = Total - TotalIva
        Return TotalNota
    End Function
    Public Function DaNuevoFolio(ByVal pSerie As String, ByVal pidSucursal As Integer) As Integer
        Comm.CommandText = "select ifnull((select max(folioi) from tblnotasdecreditocompras where serie='" + pSerie + "' and (estado=3 or estado=4) ),0)"
        DaNuevoFolio = Comm.ExecuteScalar + 1
    End Function
    Public Function ChecaFolioRepetido(ByVal pFolio As Integer, ByVal pSerie As String) As Boolean
        Dim Resultado As Integer = 0
        Comm.CommandText = "select count(folioi) from tblnotasdecreditocompras where folioi=" + pFolio.ToString + " and estado<>1 and estado<>2 and serie='" + Replace(pSerie, "'", "''") + "'"
        Resultado = Comm.ExecuteScalar
        If Resultado = 0 Then
            ChecaFolioRepetido = False
        Else
            ChecaFolioRepetido = True
        End If
    End Function



    'Public ReadOnly Property totalLetra(ByVal idmoneda As Integer) As String
    '    Get
    '        Dim f As New PaseLetras
    '        Return f.PASELETRAS(DaTotal(ID, idmoneda), idmoneda) + " " + [Enum].GetName(GetType(MONEDAS), idmoneda)
    '    End Get
    'End Property

    'Public Function CreaCadenaOriginal(ByVal pIdNota As Integer, ByVal pIdMoneda As Integer) As String
    '    Dim O As New dbOpciones(Comm.Connection)
    '    Dim CO As String = "|2.0|"
    '    ID = pIdNota
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
    '    CO += Format(Subtotal - TotalIva, "#0.00") + "|"
    '    CO += "0|" 'descuento

    '    CO += Format(TotalNota, "#0.00") + "|" ' total factura con iva

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
    '    If Proveedor.DireccionFiscal = 0 Then
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
    '        CO += Proveedor.Direccion2 + "|"
    '        CO += Proveedor.NoExterior2 + "|"
    '        CO += Proveedor.NoInterior2 + "|"
    '        CO += Proveedor.Colonia2 + "|"
    '        CO += Proveedor.Ciudad2 + "|"
    '        CO += Proveedor.ReferenciaDomicilio2 + "|"
    '        CO += Proveedor.Municipio2 + "|"
    '        CO += Proveedor.Estado2 + "|"
    '        CO += Proveedor.Pais2 + "|"
    '        CO += Proveedor.CP2 + "|"
    '    End If

    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
    '    DR = VI.ConsultaReader(ID)
    '    'Dim PrecioTemp As Double
    '    Dim PU As Double
    '    Dim PT As Double
    '    While DR.Read
    '        CO += DR("cantidad").ToString + "|"
    '        'CO += DR("tipocantidad") + "|"
    '        'CO += DR("clave") + "|"
    '        CO += DR("descripcion") + "|"

    '        If DR("idmoneda") <> 2 Then

    '            PU = DR("precio") / DR("cantidad") * TipodeCambio
    '            PT = DR("precio") * TipodeCambio
    '            CO += Format((PU / (1 + DR("iva") / 100)), "#0.00") + "|"
    '            CO += Format((PT / (1 + DR("iva") / 100)), "#0.00") + "|"
    '        Else
    '            PU = DR("precio") / DR("cantidad")
    '            PT = DR("precio")
    '            CO += Format((PU / (1 + DR("iva") / 100)), "#0.00") + "|"
    '            CO += Format((PT / (1 + DR("iva") / 100)), "#0.00") + "|"
    '        End If

    '    End While
    '    DR.Close()


    '    'If ISR <> 0 Then
    '    '    CO += "ISR|" + Format(TotalISR, "#0.00") + "|"
    '    'End If
    '    'If IvaRetenido <> 0 Then
    '    '    CO += "IVA|" + Format(TotalIvaRetenido, "#0.00") + "|"
    '    'End If
    '    'If ISR <> 0 Or IvaRetenido <> 0 Then
    '    '    CO += Format(TotalISR + TotalIvaRetenido, "#0.00") + "|"
    '    'End If
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
    '                PT = DR("precio") * TipodeCambio
    '                IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
    '            Else
    '                PT = DR("precio")
    '                IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
    '            End If
    '        Else
    '            IAnt = IvasImporte(DR("iva").ToString)
    '            IvasImporte.Remove(DR("iva").ToString)
    '            If DR("idmoneda") <> 2 Then
    '                PT = DR("precio") * TipodeCambio
    '                IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
    '            Else
    '                PT = DR("precio")
    '                IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
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





    'Public Function CreaXML(ByVal pIdVenta As Integer, ByVal pIdMoneda As Integer, ByVal pSelloDigital As String) As String
    '    Dim O As New dbOpciones(Comm.Connection)
    '    Dim en As New Encriptador
    '    Dim XMLDoc As String
    '    Dim Ivas As New Collection
    '    Dim IvasImporte As New Collection
    '    XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf

    '    XMLDoc += "<Comprobante " + vbCrLf

    '    en.Leex509(My.Settings.rutacer)
    '    ID = pIdVenta
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

    '    XMLDoc += "subTotal=""" + Format(Subtotal - TotalIva, "#0.00") + """" + vbCrLf

    '    XMLDoc += "total=""" + Format(TotalNota, "#0.00") + """" + vbCrLf
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
    '    If Proveedor.DireccionFiscal = 0 Then
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
    '        If Proveedor.Direccion2 <> "" Then XMLDoc += "<Domicilio calle=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Direccion2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.NoExterior2 <> "" Then XMLDoc += "noExterior=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.NoExterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.NoInterior2 <> "" Then XMLDoc += "noInterior=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.NoInterior2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.Colonia2 <> "" Then XMLDoc += "colonia=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Colonia2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.Ciudad2 <> "" Then XMLDoc += "localidad=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Ciudad2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.ReferenciaDomicilio2 <> "" Then XMLDoc += "referencia=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.ReferenciaDomicilio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.Municipio2 <> "" Then XMLDoc += "municipio=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Municipio2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.Estado2 <> "" Then XMLDoc += "estado=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Estado2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.Pais2 <> "" Then XMLDoc += "pais=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.Pais2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If Proveedor.CP2 <> "" Then XMLDoc += "codigoPostal=""" + Replace(Replace(Replace(Replace(Replace(Proveedor.CP2, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '    End If
    '    XMLDoc += "/>" + vbCrLf

    '    XMLDoc += "</Receptor>" + vbCrLf

    '    XMLDoc += "<Conceptos>" + vbCrLf

    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
    '    DR = VI.ConsultaReader(ID)
    '    Dim PU As Double
    '    Dim PT As Double
    '    While DR.Read
    '        XMLDoc += "<Concepto " + vbCrLf
    '        XMLDoc += "cantidad=""" + DR("cantidad").ToString + """" + vbCrLf
    '        'XMLDoc += "unidad=""" + Replace(Replace(Replace(Replace(Replace(DR("tipocantidad"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        XMLDoc += "descripcion=""" + Replace(Replace(Replace(Replace(Replace(DR("descripcion"), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """" + vbCrLf
    '        If DR("idmoneda") <> 2 Then
    '            PU = DR("precio") / DR("cantidad") * TipodeCambio
    '            PT = DR("precio") * TipodeCambio
    '            XMLDoc += "valorUnitario=""" + Format((PU) / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
    '            XMLDoc += "importe=""" + Format(PT / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
    '            XMLDoc += "/> " + vbCrLf
    '        Else
    '            PU = DR("precio") / DR("cantidad")
    '            PT = DR("precio")
    '            XMLDoc += "valorUnitario=""" + Format(PU / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
    '            XMLDoc += "importe=""" + Format(PT / (1 + DR("iva") / 100), "#0.00") + """" + vbCrLf
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
    '    'If ISR <> 0 Or IvaRetenido <> 0 Then
    '    '    XMLDoc += "totalImpuestosRetenidos=""" + Format(TotalISR + TotalIvaRetenido, "#0.00") + """"
    '    'End If
    '    XMLDoc += ">" + vbCrLf

    '    'If ISR <> 0 Or IvaRetenido <> 0 Then
    '    '    XMLDoc += "<Retenciones>" + vbCrLf
    '    '    If ISR <> 0 Then
    '    '        XMLDoc += "<Retencion impuesto=""ISR""" + vbCrLf
    '    '        'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
    '    '        XMLDoc += "importe=""" + Format(TotalISR, "#0.00") + """ />" + vbCrLf
    '    '    End If

    '    '    If ISR <> 0 Then
    '    '        XMLDoc += "<Retencion impuesto=""IVA""" + vbCrLf
    '    '        'XMLDoc += "tasa=""" + ISR.ToString + """" + vbCrLf
    '    '        XMLDoc += "importe=""" + Format(TotalIvaRetenido, "#0.00") + """ />" + vbCrLf
    '    '    End If

    '    '    XMLDoc += "</Retenciones>" + vbCrLf

    '    'End If



    '    XMLDoc += "<Traslados>" + vbCrLf


    '    DR = DaIvas(ID)
    '    Dim IAnt As Double
    '    While DR.Read
    '        If Ivas.Contains(DR("iva").ToString) = False Then
    '            Ivas.Add(DR("iva"), DR("iva").ToString)
    '        End If
    '        If IvasImporte.Contains(DR("iva").ToString) = False Then
    '            If DR("idmoneda") <> 2 Then
    '                PT = DR("precio") * TipodeCambio
    '                IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
    '            Else
    '                PT = DR("precio")
    '                IvasImporte.Add(PT - (PT / (1 + DR("iva") / 100)), DR("iva").ToString)
    '            End If
    '        Else
    '            IAnt = IvasImporte(DR("iva").ToString)
    '            IvasImporte.Remove(DR("iva").ToString)
    '            If DR("idmoneda") <> 2 Then
    '                PT = DR("precio") * TipodeCambio
    '                IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
    '            Else
    '                PT = DR("precio")
    '                IvasImporte.Add(IAnt + (PT - (PT / (1 + DR("iva") / 100))), DR("iva").ToString)
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

    Public Function DaIvas(ByVal pIdNota As Integer) As MySql.Data.MySqlClient.MySqlDataReader
        Comm.CommandText = "select iva,precio,idmoneda from tblnotasdecreditodetallesc where idnota=" + pIdNota.ToString
        Return Comm.ExecuteReader
    End Function
    Public Sub Aplicar(ByVal pId As Integer, ByVal pCantidad As Double, ByVal pSuma As Boolean)
        If pSuma Then
            Comm.CommandText = "update tblnotasdecreditocompras set aplicado=aplicado+" + pCantidad.ToString + " where idnota=" + pId.ToString
        Else
            Comm.CommandText = "update tblnotasdecreditocompras set aplicado=aplicado-" + pCantidad.ToString + " where idnota=" + pId.ToString
        End If
        Comm.ExecuteNonQuery()
    End Sub
End Class
