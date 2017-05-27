Public Class AdendaModelo
    Public Property id As Integer
    Public Property entityType As String
    Public Property idCreador As String
    Public Property texto As String
    Public Property idReferencia As String
    Public Property informacionComprador As String
    Public Property idCreadorAlt As String
    Public Property tipoDivisa As String
    Public Property tipoCambio As Double
    Public Property cantidadTotal As Double
    Public Property cantidadBase2 As Double
    Public Property porcentajeTax2 As Double
    Public Property cantidadTax2 As Double
    Public Property categoriaTax2 As String
    Public Property tipoTax As String
    Public Property cantidadFinal As Double
    Public Property idVenta As Integer
    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Integer)
        Me.id = id
    End Sub

    Public Sub New(ByVal id As Integer,
                   ByVal entityType As String,
                   ByVal idCreador As String,
                   ByVal texto As String,
                   ByVal idReferencia As String,
                   ByVal informacionComprador As String,
                   ByVal idCreadorAlt As String,
                   ByVal tipoDivisa As String,
                   ByVal tipoCambio As Double,
                   ByVal cantidadtotal As Double,
                   ByVal cantidadBase2 As Double,
                   ByVal porcentajeTax2 As Double,
                   ByVal cantidadTax2 As Double,
                   ByVal categoriaTax2 As String,
                   ByVal tipoTax As String,
                   ByVal cantidadFinal As Double,
                   ByVal idVenta As Integer)
        Me.id = id
        Me.entityType = entityType
        Me.idCreador = idCreador
        Me.texto = texto
        Me.idReferencia = idReferencia
        Me.informacionComprador = informacionComprador
        Me.idCreadorAlt = idCreadorAlt
        Me.tipoDivisa = tipoDivisa
        Me.tipoCambio = tipoCambio
        Me.cantidadTotal = cantidadTotal
        Me.cantidadBase2 = cantidadBase2
        Me.porcentajeTax2 = porcentajeTax2
        Me.cantidadTax2 = cantidadTax2
        Me.categoriaTax2 = categoriaTax2
        Me.tipoTax = tipoTax
        Me.cantidadFinal = cantidadFinal
        Me.idVenta = idVenta

    End Sub

    Public Sub New(ByVal entityType As String,
                   ByVal idCreador As String,
                   ByVal texto As String,
                   ByVal idReferencia As String,
                   ByVal informacionComprador As String,
                   ByVal idCreadorAlt As String,
                   ByVal tipoDivisa As String,
                   ByVal tipoCambio As Double,
                   ByVal cantidadtotal As Double,
                   ByVal cantidadBase2 As Double,
                   ByVal porcentajeTax2 As Double,
                   ByVal cantidadTax2 As Double,
                   ByVal categoriaTax2 As String,
                   ByVal tipoTax As String,
                   ByVal cantidadFinal As Double,
                   ByVal idVenta As Integer)

        Me.entityType = entityType
        Me.idCreador = idCreador
        Me.texto = texto
        Me.idReferencia = idReferencia
        Me.informacionComprador = informacionComprador
        Me.idCreadorAlt = idCreadorAlt
        Me.tipoDivisa = tipoDivisa
        Me.tipoCambio = tipoCambio
        Me.cantidadTotal = cantidadtotal
        Me.cantidadBase2 = cantidadBase2
        Me.porcentajeTax2 = porcentajeTax2
        Me.cantidadTax2 = cantidadTax2
        Me.categoriaTax2 = categoriaTax2
        Me.tipoTax = tipoTax
        Me.cantidadFinal = cantidadFinal
        Me.idVenta = idVenta

    End Sub

    Public Function crearXml() As String
        Dim detalles As New detalleAdendaModeloDAO(MySqlcon)
        Dim lista As List(Of detalleAdendaModelo) = detalles.listaDetalles(Me.id)
        Dim xml As String = "<cfdi:Addenda>" + vbCrLf
        xml += "<modelo:AddendaModelo xmlns:modelo=""http://www.gmodelo.com.mx/CFD/Addenda/Receptor"" xsi:schemaLocation=""http://www.gmodelo.com.mx/CFD/Addenda/Receptor https://femodelo.gmodelo.com/Addenda/ADDENDAMODELOCORTA.xsd"">" + vbCrLf
        xml += "<modelo:requestForPayment>" + vbCrLf
        xml += "<modelo:requestForPaymentIdentification>" + vbCrLf
        xml += "<modelo:entityType>" + Me.entityType + "</modelo:entityType>" + vbCrLf
        xml += "<modelo:uniqueCreatorIdentification>" + Me.idCreador + "</modelo:uniqueCreatorIdentification>" + vbCrLf
        xml += "</modelo:requestForPaymentIdentification>" + vbCrLf
        xml += "<modelo:specialInstruction>" + vbCrLf
        xml += "<modelo:text>" + Me.texto + "</modelo:text>" + vbCrLf
        xml += "</modelo:specialInstruction>" + vbCrLf
        xml += "<modelo:orderIdentification>" + vbCrLf
        xml += "<modelo:referenceIdentification>" + Me.idReferencia + "</modelo:referenceIdentification>" + vbCrLf
        xml += "</modelo:orderIdentification>" + vbCrLf
        xml += "<modelo:buyer>" + vbCrLf
        xml += "<modelo:contactInformation>" + vbCrLf
        xml += "<modelo:personOrDepartmentName>" + vbCrLf
        xml += "<modelo:text>" + Me.informacionComprador + "</modelo:text>" + vbCrLf
        xml += "</modelo:personOrDepartmentName>" + vbCrLf
        xml += "</modelo:contactInformation>" + vbCrLf
        xml += "</modelo:buyer>" + vbCrLf
        xml += "<modelo:InvoiceCreator>" + vbCrLf
        xml += "<modelo:alternatePartyIdentification>" + Me.idCreadorAlt + "</modelo:alternatePartyIdentification>" + vbCrLf
        xml += "</modelo:InvoiceCreator>" + vbCrLf
        xml += "<modelo:currency currencyISOCode=""" + Me.tipoDivisa + """>" + vbCrLf
        xml += "<modelo:rateOfChange>" + Me.tipoCambio.ToString() + "</modelo:rateOfChange>" + vbCrLf
        xml += "</modelo:currency>" + vbCrLf
        Dim Des As String
        For Each d As detalleAdendaModelo In lista
            xml += "<modelo:lineItem orderLineNumber=""" + d.posicionPedido.ToString() + """>" + vbCrLf
            xml += "<modelo:tradeItemIdentification>" + vbCrLf
            xml += "<modelo:gtin>" + d.codigoEAN + "</modelo:gtin>" + vbCrLf
            xml += "</modelo:tradeItemIdentification>" + vbCrLf
            xml += "<modelo:alternateTradeItemIdentification>" + d.numProveedor + "</modelo:alternateTradeItemIdentification>" + vbCrLf
            xml += "<modelo:tradeItemDescriptionInformation language=""" + d.idioma + """>" + vbCrLf

            If d.descripcion.Length > 35 Then
                Des = d.descripcion.Substring(0, 35)
            Else
                Des = d.descripcion
            End If
            Des = Trim(Replace(Des, vbCrLf, ""))
            While Des.IndexOf("  ") <> -1
                Des = Replace(Des, "  ", " ")
            End While
            Des = Replace(Replace(Replace(Replace(Replace(Replace(Des, vbCrLf, ""), "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;")

            xml += "<modelo:longText>" + Des + "</modelo:longText>"

            xml += "</modelo:tradeItemDescriptionInformation>" + vbCrLf
            xml += "<modelo:invoicedQuantity unitOfMeasure=""" + d.unidadMedida + """>" + d.cantidadProductosFacturada.ToString() + "</modelo:invoicedQuantity>" + vbCrLf
            xml += "<modelo:grossPrice>" + vbCrLf
            xml += "<modelo:Amount>" + d.precioBruto.ToString() + "</modelo:Amount>" + vbCrLf
            xml += "</modelo:grossPrice>" + vbCrLf
            xml += "<modelo:netPrice>" + vbCrLf
            xml += "<modelo:Amount>" + d.precioNeto.ToString() + "</modelo:Amount>" + vbCrLf
            xml += "</modelo:netPrice>" + vbCrLf
            xml += "<modelo:AdditionalInformation>" + vbCrLf
            xml += "<modelo:referenceIdentification>" + d.numReferenciaAdicional + "</modelo:referenceIdentification>" + vbCrLf
            xml += "</modelo:AdditionalInformation>" + vbCrLf
            xml += "<modelo:tradeItemTaxInformation>" + vbCrLf
            xml += "<modelo:taxTypeDescription>" + d.tipoArancel + "</modelo:taxTypeDescription>" + vbCrLf
            xml += "<modelo:taxCategory>" + d.identificacionImpuesto + "</modelo:taxCategory>" + vbCrLf
            xml += "<modelo:tradeItemTaxAmount>" + vbCrLf
            xml += "<modelo:taxPercentage>" + d.porcentajeImpuesto.ToString() + "</modelo:taxPercentage>" + vbCrLf
            xml += "<modelo:taxAmount>" + d.montoImpuesto.ToString() + "</modelo:taxAmount>" + vbCrLf
            xml += "</modelo:tradeItemTaxAmount>" + vbCrLf
            xml += "</modelo:tradeItemTaxInformation>" + vbCrLf
            xml += "<modelo:totalLineAmount>" + vbCrLf
            xml += "<modelo:grossAmount>" + vbCrLf
            xml += "<modelo:Amount>" + d.precioBrutoArticulos.ToString() + "</modelo:Amount>" + vbCrLf
            xml += "</modelo:grossAmount>" + vbCrLf
            xml += "<modelo:netAmount>" + vbCrLf
            xml += "<modelo:Amount>" + d.precioNetoArticulos.ToString() + "</modelo:Amount>" + vbCrLf
            xml += "</modelo:netAmount>" + vbCrLf
            xml += "</modelo:totalLineAmount>" + vbCrLf
            xml += "</modelo:lineItem>" + vbCrLf
        Next
        xml += "<modelo:totalAmount>" + vbCrLf
        xml += "<modelo:Amount>" + Me.cantidadTotal.ToString() + "</modelo:Amount>" + vbCrLf
        xml += "</modelo:totalAmount>" + vbCrLf
        xml += "<modelo:baseAmount>" + vbCrLf
        xml += "<modelo:Amount>" + Me.cantidadBase2.ToString() + "</modelo:Amount>" + vbCrLf
        xml += "</modelo:baseAmount>" + vbCrLf
        xml += "<modelo:tax type=""" + Me.tipoTax + """>" + vbCrLf
        xml += "<modelo:taxPercentage>" + Me.porcentajeTax2.ToString() + "</modelo:taxPercentage>" + vbCrLf
        xml += "<modelo:taxAmount>" + Me.cantidadTax2.ToString() + "</modelo:taxAmount>" + vbCrLf
        xml += "<modelo:taxCategory>" + Me.categoriaTax2 + "</modelo:taxCategory>" + vbCrLf
        xml += "</modelo:tax>" + vbCrLf
        xml += "<modelo:payableAmount>" + vbCrLf
        xml += "<modelo:Amount>" + Me.cantidadFinal.ToString() + "</modelo:Amount>" + vbCrLf
        xml += "</modelo:payableAmount>" + vbCrLf
        xml += "</modelo:requestForPayment>" + vbCrLf
        xml += "</modelo:AddendaModelo>" + vbCrLf
        xml += "</cfdi:Addenda>"
        Return xml
    End Function
End Class
