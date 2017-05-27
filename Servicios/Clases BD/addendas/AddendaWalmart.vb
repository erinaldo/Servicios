Public Class AddendaWalmart
    Implements Addenda
    Public Sub New(idventa As Integer, folio As Integer, fecha As DateTime, serie As String, uuid As String, folioorden As Integer?, fechaorden As DateTime, foliorecibo As Integer?, fecharecibo As DateTime, clavecomprador As String, calleYNumeroComprador As String, calleYNumeroLugarEntrega As String, calleYNumeroProveedor As String, codigoPostalComprador As String, codigoPostalLugarEntrega As String, codigoPostalProveedor As String, claveLugarEntrega As String, coloniaLugarEntrega As String, coloniaComprador As String, coloniaProveedor As String, estadoLugarEntrega As String, estadoComprador As String, estadoProveedor As String, localidadLugarEntrega As String, localidadComprador As String, localidadProveedor As String, nombreLugarEntrega As String, nombreComprador As String, nombreProveedor As String, rfcComprador As String, rfcProveedor As String, moneda As String, numeroProveedor As Integer, diasCredito As Int16, tasaIva As Int16, iva As Double, tasaIeps As Int16, ieps As Double, total As Double, subtotal As Double, cedulaieps As String)
        Articulos = New ArrayList
        _IdVenta = idventa
        _Folio = folio
        _Fecha = fecha
        _Serie = serie
        _UUID = uuid
        Me.FolioOrden = folioorden
        Me.FechaOrden = fechaorden
        Me.FolioRecibo = foliorecibo
        Me.FechaRecibo = fecharecibo
        _ClaveComprador = clavecomprador
        _CalleYNumeroComprador = calleYNumeroComprador
        _CalleYNumeroLugarEntrega = calleYNumeroLugarEntrega
        _CalleYNumeroProveedor = calleYNumeroProveedor
        _CodigoPostalComprador = codigoPostalComprador
        _CodigoPostalLugarEntrega = codigoPostalLugarEntrega
        _CodigoPostalProveedor = codigoPostalProveedor
        Me.ClaveLugarEntrega = claveLugarEntrega
        '_ClaveProveedor = claveProveedor
        _ColoniaLugarEntrega = coloniaLugarEntrega
        _ColoniaComprador = coloniaComprador
        _ColoniaProveedor = coloniaProveedor
        _EstadoLugarEntrega = estadoLugarEntrega
        _EstadoComprador = estadoComprador
        _EstadoProveedor = estadoProveedor
        _LocalidadLugarEntrega = localidadLugarEntrega
        _LocalidadComprador = localidadComprador
        _LocalidadProveedor = localidadProveedor
        Me.NombreLugarEntrega = nombreLugarEntrega
        _NombreComprador = nombreComprador
        _NombreProveedor = nombreProveedor
        _RfcComprador = rfcComprador
        _RfcProveedor = rfcProveedor
        _Moneda = moneda
        _NumeroProveedor = numeroProveedor
        _DiasCredito = diasCredito
        _TasaIva = tasaIva
        _Iva = iva
        _Total = total
        _Subtotal = subtotal
        Me.CedulaIEPS = cedulaieps
    End Sub

    Private _IdVenta As Integer
    Private _Folio As Integer
    Private _Fecha As DateTime
    Private _Serie As String
    Private _UUID As String
    Private _ClaveComprador As String
    Private _CalleYNumeroComprador As String
    Private _CalleYNumeroLugarEntrega As String
    Private _CalleYNumeroProveedor As String
    Private _CodigoPostalComprador As String
    Private _CodigoPostalLugarEntrega As String
    Private _CodigoPostalProveedor As String
    'Private _ClaveLugarEntrega As String
    'Private _ClaveProveedor As String
    Private _ColoniaLugarEntrega As String
    Private _ColoniaComprador As String
    Private _ColoniaProveedor As String
    Private _EstadoLugarEntrega As String
    Private _EstadoComprador As String
    Private _EstadoProveedor As String
    Private _LocalidadLugarEntrega As String
    Private _LocalidadComprador As String
    Private _LocalidadProveedor As String
    'Private _NombreLugarEntrega As String
    Private _NombreComprador As String
    Private _NombreProveedor As String
    Private _RfcComprador As String
    Private _RfcProveedor As String
    Private _Moneda As String
    Private _NumeroProveedor As String
    Private _DiasCredito As Int16
    Private _TasaIva As Int16
    Private _Iva As Double
    Private _TasaIeps As Int16
    Private _Ieps As Double
    Private _Total As Double
    Private _Subtotal As Double
    Public ReadOnly Property IdVenta As Integer
        Get
            Return _IdVenta
        End Get
    End Property
    Public Property Articulos As ArrayList
    Public ReadOnly Property Folio As Integer
        Get
            Return _Folio
        End Get
    End Property
    Public ReadOnly Property Fecha As DateTime
        Get
            Return _Fecha
        End Get
    End Property
    Public Property FolioOrden As Integer?
    Public Property FolioRecibo As Integer?
    Public Property FechaOrden As DateTime
    Public Property FechaRecibo As DateTime
    Public ReadOnly Property Serie As String
        Get
            Return _Serie
        End Get
    End Property
    Public ReadOnly Property UUID As String
        Get
            Return _UUID
        End Get
    End Property
    Public ReadOnly Property ClaveComprador As String
        Get
            Return _ClaveComprador
        End Get
    End Property
    Public ReadOnly Property NombreComprador As String
        Get
            Return _NombreComprador
        End Get
    End Property
    Public ReadOnly Property CalleYNumeroComprador As String
        Get
            Return _CalleYNumeroComprador
        End Get
    End Property
    Public ReadOnly Property ColoniaComprador As String
        Get
            Return _ColoniaComprador
        End Get
    End Property
    Public ReadOnly Property LocalidadComprador As String
        Get
            Return _LocalidadComprador
        End Get
    End Property
    Public ReadOnly Property EstadoComprador As String
        Get
            Return _EstadoComprador
        End Get
    End Property
    Public ReadOnly Property CodigoPostalComprador As String
        Get
            Return _CodigoPostalComprador
        End Get
    End Property
    Public ReadOnly Property RfcComprador As String
        Get
            Return _RfcComprador
        End Get
    End Property

    'Public ReadOnly Property ClaveProveedor As String
    '    Get
    '        Return _ClaveProveedor
    '    End Get
    'End Property
    Public ReadOnly Property NombreProveedor As String
        Get
            Return _NombreProveedor
        End Get
    End Property
    Public ReadOnly Property CalleYNumeroProveedor As String
        Get
            Return _CalleYNumeroProveedor
        End Get
    End Property
    Public ReadOnly Property ColoniaProveedor As String
        Get
            Return _ColoniaProveedor
        End Get
    End Property
    Public ReadOnly Property LocalidadProveedor As String
        Get
            Return _LocalidadProveedor
        End Get
    End Property
    Public ReadOnly Property EstadoProveedor As String
        Get
            Return _EstadoProveedor
        End Get
    End Property
    Public ReadOnly Property CodigoPostalProveedor As String
        Get
            Return _CodigoPostalProveedor
        End Get
    End Property
    Public ReadOnly Property RfcProveedor As String
        Get
            Return _RfcProveedor
        End Get
    End Property
    Public ReadOnly Property NumeroProveedor As Integer
        Get
            Return _NumeroProveedor
        End Get
    End Property


    Public Property ClaveLugarEntrega As String

    Public Property NombreLugarEntrega As String

    Public ReadOnly Property CalleYNumeroLugarEntrega As String
        Get
            Return _CalleYNumeroLugarEntrega
        End Get
    End Property
    Public ReadOnly Property ColoniaLugarEntrega As String
        Get
            Return _ColoniaLugarEntrega
        End Get
    End Property
    Public ReadOnly Property LocalidadLugarEntrega As String
        Get
            Return _LocalidadLugarEntrega
        End Get
    End Property
    Public ReadOnly Property EstadoLugarEntrega As String
        Get
            Return _EstadoLugarEntrega
        End Get
    End Property
    Public ReadOnly Property CodigoPostalLugarEntrega As String
        Get
            Return _CodigoPostalLugarEntrega
        End Get
    End Property

    Public Property CedulaIEPS As String
    Public ReadOnly Property Moneda As String
        Get
            Return _Moneda
        End Get
    End Property
    Public ReadOnly Property DiasCredito As Int16
        Get
            Return _DiasCredito
        End Get
    End Property
    Public ReadOnly Property Total As Double
        Get
            Return _Total
        End Get
    End Property
    Public ReadOnly Property SubTotal As Double
        Get
            Return _SubTotal
        End Get
    End Property
    Public ReadOnly Property TasaIva As Int16
        Get
            Return _TasaIva
        End Get
    End Property
    Public ReadOnly Property Iva As Double
        Get
            Return _Iva
        End Get
    End Property
    Public ReadOnly Property TasaIeps As Int16
        Get
            Return _TasaIeps
        End Get
    End Property
    Public ReadOnly Property Ieps As Double
        Get
            Return _Ieps
        End Get
    End Property

    Public Function CreaXML() As String Implements Addenda.CreaXML
        Dim xml As String = "<Addenda>UNB+UNOB:1+EDIID:ZZ+925485MX00:8+061231:1000+3945" 'NUMERO DE CONTRL (copiado del ejemplo sin editar)
        xml += "UNH+1+INVOIC:D:01B:UN:AMC002" 'UNH=ENCABEZADO
        xml += "BGM+380+" + Folio.ToString() + "+9" '–FOLIO DE LA FACTURA  BGM=inicio del mensaje, 380 indica factura, 9 indica original"
        xml += "DTM+137:" + Format(Fecha, "yyyyMMddHHmmss") + ":204" '– FECHA DE LA FACTURA DTM=datetime, 137 indica fecha de factura, 204 indica el formato"
        xml += "FTX+ZZZ+++OCHO MIL OCHOCIENTOS TREINTA Y DOS PESOS 00/100 M.N.+ES" 'FTX=texto libre, ZZZ calificador de texto que indica mutually defined, ES indica idioma español
        If True Then 'se excluyen mutuamente
            xml += "RFF+ON:" + FolioOrden.ToString() ' – ORDEN DE COMPRA  RFF=referencia, ON indica numero de orden de compra"
            xml += "DTM+171:" + Format(FechaOrden, "yyyyMMdd") + ":102" '–FECHA DE LA ORDEN DE COMPRA O FOLIO DE RECIBO, 171 indica referencia de fecha, 102 indica el formato yyyyMMdd"
        Else
            xml += "RFF+DQ:" + FolioRecibo.ToString() ' – FOLIO DE RECIBO  RFF=referencia, DQ indica numero de folio de recibo"
            xml += "DTM+171:" + Format(FechaRecibo, "yyyyMMdd") + ":102" '–FECHA DE LA ORDEN DE COMPRA O FOLIO DE RECIBO, 171 indica referencia de fecha, 102 indica el formato yyyyMMdd"
        End If
        xml += "RFF+BT:" + Serie '– SERIE DE LA FACTURA RFF=referencia, BT=numero de lote, "
        xml += "RFF+ATZ:" + UUID '– NUMERO DE APROBACION DE HACIENDA. NOTA: en los archivos dice que es el numero de autorización asignado a la serie y los folios"
        xml += "NAD+BY+" + ClaveComprador + "::9++" + NombreComprador + "+" + CalleYNumeroComprador + ":" + ColoniaComprador + "+" + LocalidadComprador + "+" + EstadoComprador + "+" + CodigoPostalComprador '– DATOS DEL COMPRADOR NAD=nombre y direccion, BY=comprador, 9 indica asociación internacional..., "
        xml += "RFF+GN:" + RfcComprador '- RFC DEL COMPRADOR GN=numero de referencia del gobierno"
        xml += "RFF+ZZZ:" + CedulaIEPS '- CEDULA DE IEPS  aplica para articulos que graven IEPS"
        xml += "NAD+SU+++" + NombreProveedor + "+" + CalleYNumeroProveedor + ":" + ColoniaProveedor + "+" + LocalidadProveedor + "+" + EstadoProveedor + "+" + CodigoPostalProveedor ' – DATOS DEL PROVEEDOR NAD=name and address, SU=supplier "
        xml += "RFF+GN:" + RfcProveedor ' –RFC DEL PROVEEDOR 
        xml += "RFF+IA:" + NumeroProveedor.ToString() ' –NUMERO DE PROVEEDOR A 9 DIGITOS "
        xml += "NAD+ST+" + ClaveLugarEntrega + "::9++" + NombreLugarEntrega + "+" + CalleYNumeroLugarEntrega + ":" + ColoniaLugarEntrega + "+" + LocalidadLugarEntrega + "+" + EstadoLugarEntrega + "+" + CodigoPostalLugarEntrega ' –DATOS DEL LUGAR DE ENTREGA "
        xml += "CUX+2:" + Moneda + ":4" '-MONEDA NACIONAL/TIPO DE CAMBIO CUX=currency, 2 indica referencia de moneda, 4 indica moneda de factura"
        xml += "PAT+1++5:3:D:" + DiasCredito.ToString() ' –DIAS DE VENCIMIENTO PAT=payment terms, 1 indica básico, 5 indica fecha de factura, 3 indica despues, D indica dias, 30 indica los dias de crédito"

        Dim cont As Integer = 1
        For Each a As AddendaWalmartArticulo In Articulos
            xml += "LIN+" + cont.ToString() + "++" + a.CodigoBarras + ":SRV::9" ' – UPC (CODIGO DE BARRAS DEL ARTICULO A 13 DIGITOS) LIN=line item identifier, SRV indica global trade item number, 9 indica EAN"
            xml += "IMD+F++:::" + a.Descripcion + "::E" ' – DESCRIPCION DEL ARTICULO IMD=item description, F indica forma libre, E indica español"
            xml += "QTY+47:" + a.Cantidad.ToString() + ":EA" ' – PIEZAS FACTURADAS QTY=quantity, 47 indica canidad de factura, EA indica unidad each"
            xml += "MOA+203:" + a.Importe.ToString() ' – SUBTOTAL DE LA LINEA MOA=monetary ammount, 203 indica monto de la linea"
            xml += "PRI+AAA:" + a.Precio.ToString() + "::::EA" ' – PRECIO UNITARIO PRI=price, AAA=cálculo neto, EA=each"
            xml += "TAX+7+VAT+++:::" + a.TasaIva.ToString() + ".0" ' –PORCENTAJE DEL IVA  TAX indica impuesto o deber, 7 indica impuesto, VAT=value added tax (IVA), C indica impuesto pagado por proveedor"
            xml += "MOA+124:" + a.Iva.ToString() ' – MONTO DEL IVA MOA=monetary ammount, 124 indica impuesto"
            xml += "UNS+S" ' UNS indica session de control, S indica separador de detalle
            xml += " CNT+2:" + Articulos.Count.ToString() ' –NUMERO DE LINEAS CNT=control total, 2 indica numero total de lineas en detalle"
            cont += 1
        Next

        xml += "MOA+9:" + Total.ToString() ' – TOTAL DE LA FACTURA MOA=monetary ammount, 9 indica total a pagar"
        xml += "MOA+79:" + SubTotal.ToString() ' – SUBTOTAL DE LA FACTURA MOA=monetary ammount, 79 indica total de los articulos"
        xml += "MOA+125:" + SubTotal.ToString() ' – SUBTOTAL DE LA FACTURA MOA=monetary ammount, 125 indica total antes de impuestos"
        xml += "TAX+7+VAT+++:::" + TasaIva.ToString() ' –PORCENTAJE DEL IVA TAX indica deber o impuesto, 7 indica impuesto, VAT=value added tax"
        xml += "MOA+124:" + Iva.ToString() '– MONTO TOTAL DEL IVA MOA=monetary ammount, 124 indica impuesto"
        If Ieps > 0 Then
            xml += "TAX+7+GST+++:::" + TasaIeps.ToString() ' –PORCENTAJE DEL IVA TAX indica deber o impuesto, 7 indica impuesto, VAT=?"
            xml += "MOA+124:" + Ieps.ToString() '– MONTO TOTAL DEL IVA MOA=monetary ammount, 124 indica impuesto"
        End If
        xml += "UNT+" + (23 + Articulos.Count * 9).ToString() + "+1" '-NUMERO DE SEGMENTOS DEL MENSAJE"
        xml += "UNZ+1+3945</Addenda>" '-NUMERO DE CONTRL (copiado del ejemplo sin editar)"
        Return xml
    End Function
End Class

Public Class AddendaWalmartArticulo
    Private _IdVenta As String
    Private _CodigoBarras As String
    Private _Descripcion As String
    Private _Cantidad As Double
    Private _Precio As Double
    Private _TasaIva As Int16
    Public Sub New(idventa As Integer, codigobarras As String, descripcion As String, cantidad As Double, precio As Double, tasaiva As Int16)
        _IdVenta = idventa
        _CodigoBarras = codigobarras
        _Descripcion = descripcion
        _Cantidad = cantidad
        _Precio = precio
        _TasaIva = tasaiva
    End Sub
    Public ReadOnly Property IdVenta As Integer
        Get
            Return _IdVenta
        End Get
    End Property
    Public ReadOnly Property CodigoBarras As String
        Get
            Return _CodigoBarras
        End Get
    End Property
    Public ReadOnly Property Descripcion As String
        Get
            Return _Descripcion
        End Get
    End Property
    Public ReadOnly Property Cantidad As Double
        Get
            Return _Cantidad
        End Get
    End Property
    Public ReadOnly Property Precio As Double
        Get
            Return _Precio
        End Get
    End Property
    Public ReadOnly Property Importe As Double
        Get
            Return _Cantidad * _Precio
        End Get
    End Property
    Public ReadOnly Property TasaIva As Int16
        Get
            Return _TasaIva
        End Get
    End Property
    Public ReadOnly Property Iva As Double
        Get
            Return Importe * (1 + TasaIva / 100)
        End Get
    End Property
End Class