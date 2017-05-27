Public Class NodoXML
    Private _tag As Integer
    Private _parent As Integer
    Private _clase As String = ""
    Private _declarado As Boolean = False

    Public Sub New(ByVal tag As Integer, ByVal parent As Integer)
        _clase = clase
        _tag = tag
        _parent = parent
    End Sub
    Public Property tag() As Integer
        Get
            Return _tag
        End Get
        Set(ByVal value As Integer)
            _tag = value
        End Set
    End Property

    Public Property parent() As Integer
        Get
            Return _parent
        End Get
        Set(ByVal value As Integer)
            _parent = value
        End Set
    End Property

    Public Property clase() As String
        Get
            Return _clase
        End Get
        Set(ByVal value As String)
            _clase = value
        End Set
    End Property

    Public Property declarado() As Boolean
        Get
            Return _declarado
        End Get
        Set(ByVal value As Boolean)
            _declarado = value
        End Set
    End Property

End Class
