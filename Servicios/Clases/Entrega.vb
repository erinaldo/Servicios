Public Class Entrega
    Public Property Id As Integer
    Public Property Unidad As String
    Public Property Marca As String
    Public Property Modelo As String
    Public Property Color As String
    Public Property Placas As String
    Public Property Chofer As String
    Public Property Salida As DateTime
    Public Property Lugar As String
    Public Property Paquetes As Integer
    Public Property Lote As String
    Public Property NumeroSellos As String
    Public Property Kilos As Double
    Public Property Transportista As String
    Public Property Llegada As DateTime
    Public Sub New(id As Integer, unidad As String, marca As String, modelo As String, color As String, placas As String, chofer As String, salida As DateTime, lugar As String, paquetes As Integer, lote As String, numerosellos As String, kilos As Double, transportista As String, llegada As DateTime)
        Me.Id = id
        Me.Unidad = unidad
        Me.Marca = marca
        Me.Modelo = modelo
        Me.Color = color
        Me.Placas = placas
        Me.Chofer = chofer
        Me.Salida = salida
        Me.Lugar = lugar
        Me.Paquetes = paquetes
        Me.Lote = lote
        Me.NumeroSellos = numerosellos
        Me.Kilos = kilos
        Me.Transportista = transportista
        Me.Llegada = llegada
    End Sub

End Class
