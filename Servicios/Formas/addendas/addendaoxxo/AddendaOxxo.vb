Public Class AddendaOxxo

    'Constructor vacío
    Public Sub AddendaOxxo()
    End Sub

    'AddendaOxxo - Atributos y Propiedades

    Private m_id As Integer = 0
    Public Property Id() As Integer
        Get
            Id = m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property

    Private m_noVersAdd As Integer = 0
    Public Property noVersAdd() As Integer
        Get
            noVersAdd = m_noVersAdd
        End Get
        Set(ByVal value As Integer)
            m_noVersAdd = value
        End Set
    End Property

    Private m_claseDoc As Integer = 0
    Public Property claseDoc() As Integer
        Get
            claseDoc = m_claseDoc
        End Get
        Set(ByVal value As Integer)
            m_claseDoc = value
        End Set
    End Property

    Private m_plaza As String
    Public Property plaza() As String
        Get
            plaza = m_plaza
        End Get
        Set(ByVal value As String)
            m_plaza = value
        End Set
    End Property

    Private m_tipoProv As Integer = 0
    Public Property tipoProv() As Integer
        Get
            tipoProv = m_tipoProv
        End Get
        Set(ByVal value As Integer)
            m_tipoProv = value
        End Set
    End Property

    Private m_locType As String
    Public Property locType() As String
        Get
            locType = m_locType
        End Get
        Set(ByVal value As String)
            m_locType = value
        End Set
    End Property

    Private m_folioPago As String
    Public Property folioPago() As String
        Get
            folioPago = m_folioPago
        End Get
        Set(ByVal value As String)
            m_folioPago = value
        End Set
    End Property

    Private m_ordCompra As Int64 = 0
    Public Property ordCompra() As Int64
        Get
            ordCompra = m_ordCompra
        End Get
        Set(ByVal value As Int64)
            m_ordCompra = value
        End Set
    End Property

    Private m_glnEmisor As String = "0"
    Public Property glnEmisor() As String
        Get
            glnEmisor = m_glnEmisor
        End Get
        Set(ByVal value As String)
            m_glnEmisor = value
        End Set
    End Property

    Private m_glnReceptor As String = 0
    Public Property glnReceptor() As String
        Get
            glnReceptor = m_glnReceptor
        End Get
        Set(ByVal value As String)
            m_glnReceptor = value
        End Set
    End Property

    Private m_moneda As String
    Public Property moneda() As String
        Get
            moneda = m_moneda
        End Get
        Set(ByVal value As String)
            m_moneda = value
        End Set
    End Property

    Private m_tipoCambio As Double = 0
    Public Property tipoCambio() As Double
        Get
            tipoCambio = m_tipoCambio
        End Get
        Set(ByVal value As Double)
            m_tipoCambio = value
        End Set
    End Property

    Private m_cfdReferenciaSerie As String
    Public Property cfdReferenciaSerie() As String
        Get
            cfdReferenciaSerie = m_cfdReferenciaSerie
        End Get
        Set(ByVal value As String)
            m_cfdReferenciaSerie = value
        End Set
    End Property

    Private m_cfdReferenciaFolio As String = 0
    Public Property cfdReferenciaFolio() As String
        Get
            cfdReferenciaFolio = m_cfdReferenciaFolio
        End Get
        Set(ByVal value As String)
            m_cfdReferenciaFolio = value
        End Set
    End Property

    Private m_montoDescuento0 As Double = 0
    Public Property montoDescuento0() As Double
        Get
            montoDescuento0 = m_montoDescuento0
        End Get
        Set(ByVal value As Double)
            m_montoDescuento0 = value
        End Set
    End Property

    Private m_tipoDescuento0 As String
    Public Property tipoDescuento0() As String
        Get
            tipoDescuento0 = m_tipoDescuento0
        End Get
        Set(ByVal value As String)
            m_tipoDescuento0 = value
        End Set
    End Property

    Private m_montoDescuento1 As Double = 0
    Public Property montoDescuento1() As Double
        Get
            montoDescuento1 = m_montoDescuento1
        End Get
        Set(ByVal value As Double)
            m_montoDescuento1 = value
        End Set
    End Property

    Private m_tipoDescuento1 As String
    Public Property tipoDescuento1() As String
        Get
            tipoDescuento1 = m_tipoDescuento1
        End Get
        Set(ByVal value As String)
            m_tipoDescuento1 = value
        End Set
    End Property

    Private m_montoDescuento2 As Double = 0
    Public Property montoDescuento2() As Double
        Get
            montoDescuento2 = m_montoDescuento2
        End Get
        Set(ByVal value As Double)
            m_montoDescuento2 = value
        End Set
    End Property

    Private m_tipoDescuento2 As String
    Public Property tipoDescuento2() As String
        Get
            tipoDescuento2 = m_tipoDescuento2
        End Get
        Set(ByVal value As String)
            m_tipoDescuento2 = value
        End Set
    End Property

    Private m_montoDescuento3 As Double = 0
    Public Property montoDescuento3() As Double
        Get
            montoDescuento3 = m_montoDescuento3
        End Get
        Set(ByVal value As Double)
            m_montoDescuento3 = value
        End Set
    End Property

    Private m_tipoDescuento3 As String
    Public Property tipoDescuento3() As String
        Get
            tipoDescuento3 = m_tipoDescuento3
        End Get
        Set(ByVal value As String)
            m_tipoDescuento3 = value
        End Set
    End Property

    Private m_importeTotal As Double = 0
    Public Property importeTotal() As Double
        Get
            importeTotal = m_importeTotal
        End Get
        Set(ByVal value As Double)
            m_importeTotal = value
        End Set
    End Property

    Private m_tipoValidacion As Integer = 0
    Public Property tipoValidacion() As Integer
        Get
            tipoValidacion = m_tipoValidacion
        End Get
        Set(ByVal value As Integer)
            m_tipoValidacion = value
        End Set
    End Property

    Private m_fuenteNota As Integer = 0
    Public Property fuenteNota() As Integer
        Get
            fuenteNota = m_fuenteNota
        End Get
        Set(ByVal value As Integer)
            m_fuenteNota = value
        End Set
    End Property
    Private m_articulos As New List(Of Articulo)
    Public articulos As New Collection



End Class
