Public Class NodoImpresionN
    Public id As Integer
    ''' <summary>
    ''' Posicion X en pixeles del nodo
    ''' </summary>
    ''' <remarks></remarks>
    Public X As Integer
    Public Y As Integer
    Public Texto As String
    Public Nombre As String
    Public DataPropertyName As String
    Public XL As Integer
    Public YL As Integer
    Dim iFuente As Font
    ''' <summary>
    ''' Tipo de nodo: 0 estático, 1 detalles, 2 pie de pagina.
    ''' </summary>
    ''' <remarks></remarks>
    Public Tipo As Integer
    ''' <summary>
    ''' Tipo de dato del nodo: 0 Texto, 1 Numérico.
    ''' </summary>
    ''' <remarks></remarks>
    Public TipoDato As Integer
    Public Alineacion As Integer
    Public Seleccionado As Boolean
    Public MouseX As Integer
    Public MouseY As Integer
    Public Visible As Byte
    Public Documento As Integer
    ''' <summary>
    ''' Tipo del nodo: 0 caja de texto, 1 línea, 2 Etiqueta, 3 Imagen.
    ''' </summary>
    ''' <remarks></remarks>
    Public TipoNodo As Byte
    Public IdSucursal As Integer
    Public ConEtiqueta As Byte
    Public Valor As String
    Public ValorN As Double
    ''' <summary>
    ''' Renglon: Para indicar en los detalles si se va a imprimir en diferentes renglones.
    ''' </summary>
    ''' <remarks></remarks>
    Public Renglon As Byte
    Public Clasificacion As Byte
    Public Imagen As Image
    Public Property Fuente() As Font
        Get
            Return iFuente
        End Get
        Set(ByVal value As Font)
            iFuente = value
            'If TipoNodo = 0 Then
            '    If XL < iFuente.Size * Texto.Length Then
            '        XL = iFuente.Size * Texto.Length
            '    End If
            '    If YL < iFuente.Size * 2 Then
            '        YL = iFuente.Size * 2
            '    End If
            'End If
        End Set
    End Property
    Public Enum Alineaciones
        Izquierda = 0
        Derecha = 1
        Centrado = 2
    End Enum
    
    Public Sub New(ByVal pid As Integer, ByVal CoorX As Integer, ByVal CoorY As Integer, ByVal LargoX As Integer, ByVal LargoY As Integer, ByVal pTexto As String, ByVal pDato As String, ByVal pFuente As Font, ByVal pAlineacion As Alineaciones, ByVal pTipo As Integer, ByVal pTipoDato As Integer, ByVal pVisible As Byte, ByVal pDocumento As Integer, ByVal pTipoNodo As Byte, ByVal pidSucursal As Integer, ByVal pConEtiqueta As Byte, ByVal pNombre As String, ByVal pRenglon As Byte, ByVal pClasificacion As Byte)
        id = pid
        X = CoorX
        Y = CoorY
        Texto = pTexto
        DataPropertyName = pDato
        XL = LargoX
        YL = LargoY
        TipoNodo = pTipoNodo
        Fuente = pFuente
        Alineacion = pAlineacion
        Tipo = pTipo
        TipoDato = pTipoDato
        Seleccionado = False
        Visible = pVisible
        Documento = pDocumento
        ConEtiqueta = pConEtiqueta
        IdSucursal = pidSucursal
        Nombre = pNombre
        Renglon = pRenglon
        Clasificacion = pClasificacion
    End Sub
    Public Sub New(ByVal pid As Integer, ByVal CoorX As Integer, ByVal CoorY As Integer, ByVal LargoX As Integer, ByVal LargoY As Integer, ByVal pTexto As String, ByVal pDato As String, ByVal pFuente As Font, ByVal pAlineacion As Alineaciones, ByVal pTipo As Integer, ByVal pTipoDato As Integer, ByVal pVisible As Byte, ByVal pDocumento As Integer, ByVal pTipoNodo As Byte, ByVal pidSucursal As Integer, ByVal pConEtiqueta As Byte, ByVal pValor As String, ByVal pValorN As String, ByVal pNombre As String, ByVal pRenglon As Byte, ByVal pClasificacion As Byte)
        id = pid
        X = CoorX
        Y = CoorY
        Texto = pTexto
        DataPropertyName = pDato
        XL = LargoX
        YL = LargoY
        TipoNodo = pTipoNodo
        Fuente = pFuente
        Alineacion = pAlineacion
        Tipo = pTipo
        TipoDato = pTipoDato
        Seleccionado = False
        Visible = pVisible
        Documento = pDocumento
        ConEtiqueta = pConEtiqueta
        IdSucursal = pidSucursal
        Valor = pValor
        ValorN = pValorN
        Nombre = pNombre
        Renglon = pRenglon
        Clasificacion = pClasificacion
    End Sub
    Public Sub New(ByVal pTexto As String, ByVal pDato As String, ByVal pValor As String, ByVal pValorN As Double)
        Texto = pTexto
        DataPropertyName = pDato
        Valor = pValor
        ValorN = pValorN
    End Sub
End Class
