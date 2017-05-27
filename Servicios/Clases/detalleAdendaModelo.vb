Public Class detalleAdendaModelo
    Public Property id As Integer
    Public Property idAdenda As Integer
    Public Property posicionPedido As Integer
    Public Property codigoEAN As String
    Public Property numProveedor As String
    Public Property idioma As String
    Public Property cantidadProductosFacturada As Double
    Public Property unidadMedida As String
    Public Property precioBruto As Double
    Public Property precioNeto As Double
    Public Property descripcion As String
    Public Property numReferenciaAdicional As String
    Public Property tipoArancel As String
    Public Property numIdImpuesto As String
    Public Property porcentajeImpuesto As Double
    Public Property montoImpuesto As Double
    Public Property identificacionImpuesto As String
    Public Property precioBrutoArticulos As Double
    Public Property precioNetoArticulos As Double

    Public Sub New()

    End Sub
    Public Sub New(ByVal id As Integer)
        Me.id = id
    End Sub
    Public Sub New(ByVal id As Integer,
                   ByVal idAdenda As Integer,
                   ByVal posicionPedido As Integer,
                   ByVal codigoEAN As String,
                   ByVal numProveedor As String,
                   ByVal idioma As String,
                   ByVal cantidadProductosFacturada As Double,
                   ByVal unidadMedida As String,
                   ByVal precioBruto As Double,
                   ByVal precioNeto As Double,
                   ByVal descripcion As String,
                   ByVal numReferenciaAdicional As String,
                   ByVal tipoArancel As String,
                   ByVal numIdImpuesto As String,
                   ByVal porcentajeImpuesto As Double,
                   ByVal montoImpuesto As Double,
                   ByVal identificacionImpuesto As String,
                   ByVal precioBrutoArticulos As Double,
                   ByVal precioNetoArticulos As Double)

        Me.id = id
        Me.idAdenda = idAdenda
        Me.posicionPedido = posicionPedido
        Me.codigoEAN = codigoEAN
        Me.numProveedor = numProveedor
        Me.idioma = idioma
        Me.cantidadProductosFacturada = cantidadProductosFacturada
        Me.unidadMedida = unidadMedida
        Me.precioBruto = precioBruto
        Me.precioNeto = precioNeto
        Me.descripcion = descripcion
        Me.numReferenciaAdicional = numReferenciaAdicional
        Me.tipoArancel = tipoArancel
        Me.numIdImpuesto = numIdImpuesto
        Me.porcentajeImpuesto = porcentajeImpuesto
        Me.montoImpuesto = montoImpuesto
        Me.identificacionImpuesto = identificacionImpuesto
        Me.precioBrutoArticulos = precioBrutoArticulos
        Me.precioNeto = precioNeto

    End Sub
    Public Sub New(ByVal idAdenda As Integer,
                   ByVal posicionPedido As Integer,
                   ByVal codigoEAN As String,
                   ByVal numProveedor As String,
                   ByVal idioma As String,
                   ByVal cantidadProductosFacturada As Double,
                   ByVal unidadMedida As String,
                   ByVal precioBruto As Double,
                   ByVal precioNeto As Double,
                   ByVal descripcion As String,
                   ByVal numReferenciaAdicional As String,
                   ByVal tipoArancel As String,
                   ByVal numIdImpuesto As String,
                   ByVal porcentajeImpuesto As Double,
                   ByVal montoImpuesto As Double,
                   ByVal identificacionImpuesto As String,
                   ByVal precioBrutoArticulos As Double,
                   ByVal precioNetoArticulos As Double)


        Me.idAdenda = idAdenda
        Me.posicionPedido = posicionPedido
        Me.codigoEAN = codigoEAN
        Me.numProveedor = numProveedor
        Me.idioma = idioma
        Me.cantidadProductosFacturada = cantidadProductosFacturada
        Me.unidadMedida = unidadMedida
        Me.precioBruto = precioBruto
        Me.precioNeto = precioNeto
        Me.descripcion = descripcion
        Me.numReferenciaAdicional = numReferenciaAdicional
        Me.tipoArancel = tipoArancel
        Me.numIdImpuesto = numIdImpuesto
        Me.porcentajeImpuesto = porcentajeImpuesto
        Me.montoImpuesto = montoImpuesto
        Me.identificacionImpuesto = identificacionImpuesto
        Me.precioBrutoArticulos = precioBrutoArticulos
        Me.precioNeto = precioNeto
    End Sub
End Class
