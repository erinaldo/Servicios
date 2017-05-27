Public Interface Addenda
    Function CreaXML() As String
End Interface

Public Class AddendaSoriana
    Implements Addenda
    
    Public Sub New(idventa As Integer, proveedor As String, remision As String, fecharemision As DateTime, tienda As Int16, tipomoneda As Int16, tipobulto As Int16, entregamercancia As Int16, cantidadbultos As Double, subtotal As Double, iva As Double, ieps As Double, otrosimpuestos As Double, total As Double, fechaentrega As Date, cita As Integer, folionotaentrada As Integer)
        Me._IdVenta = idventa
        Me._Proveedor = proveedor
        Me._Remision = remision
        Me._FechaRemision = fecharemision
        Me.Tienda = tienda
        Me.TipoMoneda = tipomoneda
        Me.TipoBulto = tipobulto
        Me.EntregaMercancia = entregamercancia
        Me.CantidadBultos = cantidadbultos
        Me._Subtotal = subtotal
        Me._IVA = iva
        Me._IEPS = ieps
        Me._Total = total
        Me.FechaEntrega = fechaentrega
        Me.Cita = cita
        Me.FolioNotaEntrada = folionotaentrada
        Me.Pedidos = New ArrayList()

    End Sub
    Private _IdVenta As Integer
    Public ReadOnly Property IdVenta As Integer
        Get
            Return _IdVenta
        End Get
    End Property
    Private _Proveedor As Integer
    Public ReadOnly Property Proveedor As Integer
        Get
            Return _Proveedor
        End Get
    End Property
    Private _Remision As String
    Public ReadOnly Property Remision As String
        Get
            Return _Remision
        End Get
    End Property
    Private _FechaRemision As DateTime
    Public ReadOnly Property FechaRemision As DateTime
        Get
            Return _FechaRemision
        End Get
    End Property
    Public Property Tienda As Int16
    Public Property TipoMoneda As Int16
    Public Property TipoBulto As Int16
    Public Property EntregaMercancia As Int16
    Public Property CantidadBultos As Double
    Private _Subtotal As Double
    Public ReadOnly Property Subtotal As Double
        Get
            Return _Subtotal
        End Get
    End Property
    Private _IVA As Double
    Public ReadOnly Property IVA As Double
        Get
            Return _IVA
        End Get
    End Property
    Private _IEPS As Double
    Public ReadOnly Property IEPS As Double
        Get
            Return _IEPS
        End Get
    End Property
    Private _OtrosImpuestos As Double
    Public ReadOnly Property OtrosImpuestos As Double
        Get
            Return _OtrosImpuestos
        End Get
    End Property
    Public _Total As Double
    Public ReadOnly Property Total As Double
        Get
            Return _Total
        End Get
    End Property
    Public Property FechaEntrega As DateTime
    Public Property Cita As Integer
    Public Property FolioNotaEntrada As Integer?
    Public Property Pedimento As AddendaSorianaPedimento
    Public Property Pedidos As ArrayList
    Public Function CreaXML() As String Implements Addenda.CreaXML
        Dim xml As String = "<cfdi:Addenda><DSCargaRemisionProv><Remision>"
        xml += "<Proveedor>" + Proveedor.ToString() + "</Proveedor>"
        xml += "<Remision>" + Remision + "</Remision>"
        xml += "<Consecutivo>0</Consecutivo>"
        xml += "<FechaRemision>" + FechaRemision.ToShortDateString() + "</FechaRemision>"
        xml += "<Tienda>" + Tienda.ToString() + "</Tienda>"
        xml += "<TipoMoneda>" + TipoMoneda.ToString() + "</TipoMoneda>"
        xml += "<TipoBulto>" + TipoBulto.ToString() + "</TipoBulto>"
        xml += "<EntregaMercancia>" + EntregaMercancia.ToString() + "</EntregaMercancia>"
        xml += "<CumpleReqFiscales>True</CumpleReqFiscales>"
        xml += "<CantidadBultos>" + CantidadBultos.ToString() + "</CantidadBultos>"
        xml += "<Subtotal>" + Subtotal.ToString() + "</Subtotal>"
        xml += "<IEPS>" + IEPS.ToString() + "</IEPS>"
        xml += "<IVA>" + IVA.ToString() + "</IVA>"
        xml += "<OtrosImpuestos>" + OtrosImpuestos.ToString() + "</OtrosImpuestos>"
        xml += "<Total>" + Total.ToString() + "</Total>"
        xml += "<CantidadPedidos>" + Pedidos.Count.ToString() + "</CantidadPedidos>"
        xml += "<FechaEntregaMercancia>" + FechaEntrega.ToShortDateString() + "</FechaEntregaMercancia>"
        xml += "<Cita>" + Cita.ToString() + "</Cita>"
        xml += "<FolioNotaEntrada>" + FolioNotaEntrada.ToString() + "</FolioNotaEntrada>"
        xml += "</Remision>"
        If Pedimento IsNot Nothing Then
            xml += "<Pedimento>"
            xml += "<Proveedor>" + Proveedor.ToString() + "</Proveedor>"
            xml += "<Remision>" + Remision + "</Remision>"
            xml += "<Pedimento>" + Pedimento.Pedimento.ToString() + "</Pedimento>"
            xml += "<Aduana>" + Pedimento.Aduana.ToString() + "</Aduana>"
            xml += "<AgenteAduanal>" + Pedimento.AgenteAduanal.ToString() + "</AgenteAduanal>"
            xml += "<TipoPedimento>" + Pedimento.TipoPedimento + "</TipoPedimento>"
            xml += "<FechaPedimento>" + Pedimento.FechaPedimento.ToShortDateString() + "</FechaPedimento>"
            xml += "<FechaReciboLaredo>" + Pedimento.FechaReciboLaredo.ToShortDateString() + "</FechaReciboLaredo>"
            xml += "<FechaBillOfLading>" + Pedimento.FechaBillOfLanding.ToShortDateString() + "</FechaBillOfLading>"
            xml += "</Pedimento>"
        End If
        xml += "<Pedidos>"
        For Each p As AddendaSorianaPedido In Pedidos
            xml += "<Proveedor>" + Proveedor.ToString() + "</Proveedor>"
            xml += "<Remision>" + Remision + "</Remision>"
            xml += "<FolioPedido>" + p.FolioPedido.ToString() + "</FolioPedido>"
            xml += "<Tienda>" + p.Tienda.ToString() + "</Tienda>"
            xml += "<CantidadArticulos>" + p.Articulos.Count.ToString() + "</CantidadArticulos>"
        Next
        xml += "</Pedidos>"
        xml += "<Articulos>"
        For Each p As AddendaSorianaPedido In Pedidos
            For Each a As AddendaSorianaArticulo In p.Articulos
                xml += "<Proveedor>" + Proveedor.ToString() + "</Proveedor>"
                xml += "<Remision>" + Remision + "</Remision>"
                xml += "<FolioPedido>" + p.FolioPedido.ToString() + "</FolioPedido>"
                xml += "<Tienda>" + p.Tienda.ToString() + "</Tienda>"
                xml += "<Codigo>" + a.Codigo.ToString() + "</Codigo>"
                xml += "<CantidadUnidadCompra>" + a.CantidadUnidadCompra.ToString() + "</CantidadUnidadCompra>"
                xml += "<CostoNetoUnidadCompra>" + a.CostoNetoUnidadCompra.ToString() + "</CostoNetoUnidadCompra>"
                xml += "<PorcentajeIEPS>" + a.PorcentajeIEPS.ToString() + "</PorcentajeIEPS>"
                xml += "<PorcentajeIVA>" + a.PorcentajeIVA.ToString() + "</PorcentajeIVA>"
            Next
        Next
        xml += "</Articulos>"
        Return xml + "</DSCargaRemisionProv></cfdi:Addenda>"
    End Function
End Class

Public Class AddendaSorianaPedimento
    Private _IdVenta As Integer
    Public Sub New()

    End Sub
    Public Sub New(idventa As Integer, pedimento As Integer, aduana As Integer, agente As Integer, tipo As String, fecha As DateTime, fecharecibo As DateTime, fechabill As DateTime)
        Me._IdVenta = idventa
        Me.Pedimento = pedimento
        Me.Aduana = aduana
        Me.AgenteAduanal = agente
        Me.TipoPedimento = tipo
        Me.FechaPedimento = fecha
        Me.FechaReciboLaredo = fecharecibo
        Me.FechaBillOfLanding = fechabill
    End Sub
    Public ReadOnly Property IdVenta As Integer
        Get
            Return _IdVenta
        End Get
    End Property
    Public Property Pedimento As Integer
    Public Property Aduana As Int16
    Public Property AgenteAduanal As Int16
    Public Property TipoPedimento As String
    Public Property FechaPedimento As DateTime
    Public Property FechaReciboLaredo As DateTime
    Public Property FechaBillOfLanding As DateTime

End Class

Public Class AddendaSorianaPedido
    Public Sub New(idventa As Integer, folio As Integer, tienda As Integer)
        Me._IdVenta = idventa
        Me.FolioPedido = folio
        Me.Tienda = tienda
        Articulos = New ArrayList()
    End Sub

    Private _IdVenta As Integer
    Public ReadOnly Property IdVenta As Integer
        Get
            Return _IdVenta
        End Get
    End Property
    Public Property FolioPedido As Integer
    Public Property Tienda As Int16
    Public ReadOnly Property CantidadArticulos As Int16
        Get
            Return Articulos.Count
        End Get
    End Property
    Public Property Articulos As ArrayList
End Class

Public Class AddendaSorianaArticulo
    Public Sub New(idventa As Integer, foliopedido As Integer, codigo As Integer, cantidad As Double, costo As Double, porcentajeiva As Int16, porcentajeieps As Int16)
        Me._IdVenta = idventa
        Me._FolioPedido = foliopedido
        Me.Codigo = codigo
        Me.CantidadUnidadCompra = cantidad
        Me.CostoNetoUnidadCompra = costo
        Me.PorcentajeIVA = porcentajeiva
        Me.PorcentajeIEPS = porcentajeieps
    End Sub
    Private _IdVenta As Integer
    Public ReadOnly Property IdVenta As Integer
        Get
            Return _IdVenta
        End Get
    End Property
    Private _FolioPedido As Integer
    Public ReadOnly Property FolioPedido As Integer
        Get
            Return _FolioPedido
        End Get
    End Property
    Public Property Codigo As Integer
    Public Property CantidadUnidadCompra As Double
    Public Property CostoNetoUnidadCompra As Double
    Public Property PorcentajeIEPS As Int16
    Public Property PorcentajeIVA As Int16

End Class