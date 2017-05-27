Public Class InfoAduana
    Public Numero As String
    Public Fecha As String
    Public Aduana As String
    Public IdDetalle As Integer
    Public YValidacion As String
    Public ClaveAduana As String
    Public Patente As String
    Public Sub New(ByVal N As String, ByVal F As String, ByVal A As String, ByVal I As Integer, Y As String, CA As String, P As String)
        Numero = N
        Fecha = F
        Aduana = A
        IdDetalle = I
        YValidacion = Y
        ClaveAduana = CA
        Patente = P
    End Sub
End Class
