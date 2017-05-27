Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Imports System.Drawing.Graphics
Public Class RestauranteMesa
    Inherits Button
    Dim IdSucursal As Integer
    Public Property X As Integer = 10
    Public Property Y As Integer = 10
    Private _numero As Integer = 1
    Public Property numero As Integer
        Get
            Return _numero
        End Get
        Set(value As Integer)
            _numero = value
            Me.Text = _numero.ToString() + vbCrLf
        End Set
    End Property
    Private _seccion As Integer = 1
    Public Property seccion As Integer
        Get
            Return _seccion
        End Get
        Set(value As Integer)
            _seccion = value
        End Set
    End Property
    Private _id As Integer
    Public Property id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Private _estado As Integer
    Public seleccionado As Boolean = False
    Public Property estado As Integer
        Get
            Return _estado
        End Get
        Set(value As Integer)
            _estado = value
            'checaEstado()
        End Set
    End Property

    Public Property capacidad As Integer
    Private ventanaOrden As frmRestauranteOrden
    Private dbmesas As New dbRestauranteMesas(MySqlcon, IdSucursal)
    Private ventaPagar As Integer
    Private mesero As dbVendedores
    Private ventas As New dbRestauranteVentas(MySqlcon)
    Public config As dbRestauranteConfiguracion

    Public Sub New(pIdSucursal As Integer)
        IdSucursal = pIdSucursal
    End Sub
    Public Sub New(ByVal numero As Integer, ByVal seccion As Integer, ByVal estado As Integer, ByVal capacidad As Integer, pIdSucursal As Integer)
        Me.Height = 50
        Me.Width = 70
        Me.numero = numero
        Me.seccion = seccion
        Me.estado = estado
        Me.capacidad = capacidad
        IdSucursal = pIdSucursal
        'checaEstado()
    End Sub


    Public Sub checaEstado()
        Select Case _estado
            Case 0
                BackColor = Color.FromArgb(config.colorLibre)
                If config.colorLetraLibre <> "" Then
                    ForeColor = Color.FromArgb(config.colorLetraLibre)
                Else
                    ForeColor = Color.Black
                End If
                Me.Text = "Mesa " + numero.ToString() + ": " + config.textoLibre
            Case 1
                BackColor = Color.FromArgb(config.colorOcupado)
                If config.colorLetraOcupado <> "" Then
                    ForeColor = Color.FromArgb(config.colorLetraOcupado)
                Else
                    ForeColor = Color.Black
                End If
                'Dim mv As New dbRestauranteVentasMesas(MySqlcon)
                'If mv.buscar(Me.id) Then
                'ventas.buscar(mv.idVenta)
                'Dim m As New dbVendedores(ventas.idMesero, MySqlcon)
                Me.Text = "Mesa " + numero.ToString() + ": " + config.textoOcupado '+ vbCrLf + m.Nombre

                'End If
            Case 2
                BackColor = Color.FromArgb(config.colorReservado)
                If config.colorReservado <> "" Then
                    ForeColor = Color.FromArgb(config.colorReservado)
                Else
                    ForeColor = Color.Black
                End If
                Me.Text = "Mesa " + numero.ToString() + ": " + config.textoReservado
        End Select
    End Sub


    Private Sub RestauranteMesa_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        If GlobalUsuarioActivado = False Then
            Dim f As New frmCambioUsuario(0, 0)
            If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                GlobalUsuarioActivado = True
            End If
            f.Dispose()
        End If
        If GlobalUsuarioActivado Then
            If estado <> EstadosMesas.Reservada Then
                ventanaOrden = New frmRestauranteOrden(id, GlobalUsuarioIdVendedor, IdSucursal)
                mesero = New dbVendedores(GlobalUsuarioIdVendedor, MySqlcon)
                ventanaOrden.ShowDialog()
                'Me.estado = ventanaOrden.estado
                'Me.mesero = ventanaOrden.mesero
                'Me.ventas = ventanaOrden.ventas
                'If ventanaOrden.cuentaCompleta Then
                '    dbmesas.modificar(Me)
                'End If
                'checaEstado()
                'checaPagar()
                'ventanaOrden.Dispose()
            Else

            End If
        End If
    End Sub
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'RestauranteMesa
        '
        Me.ResumeLayout(False)

    End Sub

    'Private Sub checaPagar()
    '    If mesero.ID > 0 Then
    '        If mesero.EsCajero = 1 Then
    '            If ventanaOrden.pagar Then
    '                If ventanaOrden.cuentaCompleta Then
    '                    Dim f As New frmRestaurantePuntoVenta(Me.id, ventanaOrden.idVenta, ventanaOrden.listaPagar, True)
    '                    f.Show()
    '                Else
    '                    Dim f As New frmRestaurantePuntoVenta(Me.id, ventanaOrden.idVenta, ventanaOrden.listaPagar, False)
    '                    f.Show()
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub
End Class
