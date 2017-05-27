Public Class Articulo
    'Constructor vacío
    Public Sub Articulo()
    End Sub

    'AddendaOxxo/Articulos/D - Atributos
    Private m_id As Integer = 0
    Public Property Id() As Integer
        Get
            Id = m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property

    Private m_pedidoAdicional As Int64 = 0
    Public Property pedidoAdicional() As Int64
        Get
            pedidoAdicional = m_pedidoAdicional
        End Get
        Set(ByVal value As Int64)
            m_pedidoAdicional = value
        End Set
    End Property

    Private m_remision As Integer = 0
    Public Property remision() As Integer
        Get
            remision = m_remision
        End Get
        Set(ByVal value As Integer)
            m_remision = value
        End Set
    End Property

    Private m_fechaEntrega As String
    Public Property fechaEntrega() As String
        Get
            fechaEntrega = m_fechaEntrega
        End Get
        Set(ByVal value As String)
            m_fechaEntrega = value
        End Set
    End Property

    Private m_crTienda As String
    Public Property crTienda() As String
        Get
            crTienda = m_crTienda
        End Get
        Set(ByVal value As String)
            m_crTienda = value
        End Set
    End Property

    Private m_nombreTienda As String
    Public Property nombreTienda() As String
        Get
            nombreTienda = m_nombreTienda
        End Get
        Set(ByVal value As String)
            m_nombreTienda = value
        End Set
    End Property

    Private m_numProducto As Int64 = 0
    Public Property numProducto() As Int64
        Get
            numProducto = m_numProducto
        End Get
        Set(ByVal value As Int64)
            m_numProducto = value
        End Set
    End Property

    Private m_descripcion As String
    Public Property descripcion() As String
        Get
            descripcion = m_descripcion
        End Get
        Set(ByVal value As String)
            m_descripcion = value
        End Set
    End Property

    Private m_unidadMedida As String
    Public Property unidadMedida() As String
        Get
            unidadMedida = m_unidadMedida
        End Get
        Set(ByVal value As String)
            m_unidadMedida = value
        End Set
    End Property

    Private m_cantidad As Double
    Public Property cantidad() As Double
        Get
            cantidad = m_cantidad
        End Get
        Set(ByVal value As Double)
            m_cantidad = value
        End Set
    End Property

    Private m_noSerieProductos As String
    Public Property noSerieProductos() As String
        Get
            noSerieProductos = m_noSerieProductos
        End Get
        Set(ByVal value As String)
            m_noSerieProductos = value
        End Set
    End Property

    Private m_porcIva As Double
    Public Property porcIva() As Double
        Get
            porcIva = m_porcIva
        End Get
        Set(ByVal value As Double)
            m_porcIva = value
        End Set
    End Property

    Private m_montoIva As Double
    Public Property montoIva() As Double
        Get
            montoIva = m_montoIva
        End Get
        Set(ByVal value As Double)
            m_montoIva = value
        End Set
    End Property

    Private m_porcIeps1 As Double
    Public Property porcIeps1() As Double
        Get
            porcIeps1 = m_porcIeps1
        End Get
        Set(ByVal value As Double)
            m_porcIeps1 = value
        End Set
    End Property

    Private m_montoIeps1 As Double
    Public Property montoIeps1() As Double
        Get
            montoIeps1 = m_montoIeps1
        End Get
        Set(ByVal value As Double)
            m_montoIeps1 = value
        End Set
    End Property

    Private m_porcIeps2 As Double
    Public Property porcIeps2() As Double
        Get
            porcIeps2 = m_porcIeps2
        End Get
        Set(ByVal value As Double)
            m_porcIeps2 = value
        End Set
    End Property

    Private m_montoIeps2 As Double
    Public Property montoIeps2() As Double
        Get
            montoIeps2 = m_montoIeps2
        End Get
        Set(ByVal value As Double)
            m_montoIeps2 = value
        End Set
    End Property

    Private m_porcIeps3 As Double
    Public Property porcIeps3() As Double
        Get
            porcIeps3 = m_porcIeps3
        End Get
        Set(ByVal value As Double)
            m_porcIeps3 = value
        End Set
    End Property

    Private m_montoIeps3 As Double
    Public Property montoIeps3() As Double
        Get
            montoIeps3 = m_montoIeps3
        End Get
        Set(ByVal value As Double)
            m_montoIeps3 = value
        End Set
    End Property

    Private m_ImporteNeto As Double
    Public Property ImporteNeto() As Double
        Get
            ImporteNeto = m_ImporteNeto
        End Get
        Set(ByVal value As Double)
            m_ImporteNeto = value
        End Set
    End Property

End Class
