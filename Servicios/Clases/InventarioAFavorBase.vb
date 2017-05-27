Public Class InventarioAFavorBase
    Public IdInventario As Integer
    Public Cantidad As Double
    Public Sub New(ByVal pIdInventario As Integer, ByVal pCantidad As Double)
        IdInventario = pIdInventario
        Cantidad = pCantidad
    End Sub
End Class
