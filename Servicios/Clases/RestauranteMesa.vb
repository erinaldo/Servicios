Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Imports System.Drawing.Graphics
Public Class RestauranteMesa
    Inherits Button
    Public Property IdSucursal As Integer
    Private _Numero As Integer
    Public Property Numero As Integer
        Get
            Return _Numero
        End Get
        Set(value As Integer)
            _Numero = value
            If Mesero IsNot Nothing Then
                Text = "Mesa " + Numero.ToString() + vbNewLine + Mesero.Nombre
            Else
                Text = "Mesa " + Numero.ToString()
            End If
        End Set
    End Property
    Public Property Seccion As Integer
    Public Property Id As Integer
    Public Property Capacidad As Integer
    Public Property Mesero As dbVendedores

    Private _Estado As Integer
    Public Property Estado As Integer
        Get
            Return _Estado
        End Get
        Set(value As Integer)
            _Estado = value
            If Mesero IsNot Nothing Then
                Text = "Mesa " + Numero.ToString() + vbNewLine + Mesero.Nombre
            Else
                Text = "Mesa " + Numero.ToString()
            End If
        End Set
    End Property
    
    Public Sub New(pIdSucursal As Integer)
        IdSucursal = pIdSucursal
        numero = 1
        Estado = 1
        Width = 100
        Height = 60
    End Sub
    Public Sub New(id As Integer, ByVal numero As Integer, ByVal seccion As Integer, ByVal estado As Integer, ByVal capacidad As Integer, pIdSucursal As Integer, top As Integer, left As Integer)
  
        Me.Height = 60
        Me.Width = 100
        Me.Top = top
        Me.Left = left
        Me.id = id
        Me.numero = numero
        Me.seccion = seccion
        Me.Estado = estado
        Me.capacidad = capacidad
        IdSucursal = pIdSucursal
        'checaEstado()
    End Sub


End Class

Public Class RestauranteSeccion
    Inherits RadioButton
    Public Property IdSucursal As Integer
    Public Property Id As Integer
    Public Property Numero As Integer
    
    Public Sub New(id As Integer, idSucursal As Integer, numero As Integer, ByVal nombre As String)
        Me.Id = id
        Me.IdSucursal = idSucursal
        Me.Numero = numero
        Me.Text = nombre
        Me.Height = 50
        Me.Width = 80
        Me.Appearance = Windows.Forms.Appearance.Button
    End Sub
End Class

Public Class RestaurantePedido
    Inherits Button
    Public Property Id
    Public Sub New()
        Height = 60
        Width = 100
    End Sub
End Class