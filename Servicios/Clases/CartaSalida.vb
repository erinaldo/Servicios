Public Class CartaSalida
    Public Sub New(id As Integer, fecha As DateTime, unidad As String, marca As String, modelo As String, color As String, placas As String, transportista As String, chofer As String, lote As String, observaciones As String)
        Me.Id = id
        Me.Fecha = fecha
        Me.Unidad = unidad
        Me.Marca = marca
        Me.Modelo = modelo
        Me.Color = color
        Me.Placas = placas
        Me.Transportista = transportista
        Me.Chofer = chofer
        Me.Lote = lote
        Me.Observaciones = observaciones
        Me.Detalles = New ArrayList
        Me.Sellos = New ArrayList
    End Sub
    Public Property Id As Integer
    Public Property Fecha As DateTime
    Public Property Unidad As String
    Public Property Marca As String
    Public Property Modelo As String
    Public Property Color As String
    Public Property Placas As String
    Public Property Transportista As String
    Public Property Chofer As String
    Public Property Lote As String
    Public Property Observaciones As String
    Public Property Detalles As ArrayList
    Public Property Sellos As ArrayList
End Class

Public Class CartaSalidaDetalle

    Public Property IdCarta As Integer
    Public Property Cantidad As Integer
    Public Property Descripcion As String
    Public Property KilosUnidad As Double
    Public ReadOnly Property Total As Double
        Get
            Return Cantidad * KilosUnidad
        End Get
    End Property
    Public Sub New(idcarta As Integer, cantidad As Integer, descripcion As String, kilosunidad As Double)
        Me.IdCarta = idcarta
        Me.Cantidad = cantidad
        Me.Descripcion = descripcion
        Me.KilosUnidad = kilosunidad
    End Sub
End Class
Public Class CartaSalidaSello
    Public Property IdCarta As Integer
    Public Property Numero As String
    Public Sub New(idcarta As Integer, numero As String)
        Me.IdCarta = idcarta
        Me.Numero = numero
    End Sub
End Class