Imports System.IO
Imports System.Threading
Public Class frmRestaurantePrincipal
    Dim mesasController As New dbRestauranteMesas(MySqlcon, GlobalIdSucursalDefault)
    Dim listaMesas As List(Of RestauranteMesa)
    Dim listaSecciones As New elemento
    Dim vendedores As New dbVendedores(MySqlcon)
    Dim idSeccion As Integer = 0
    Dim tablaSecciones As New DataTable
    Dim config As New dbRestauranteConfiguracion(1, MySqlcon)
    Dim t As frmRestauranteTeclado
    Dim estado As Integer
    Dim dbmesas As New dbRestauranteMesas(MySqlcon, GlobalIdSucursalDefault)
    Dim tipoMapa As Integer = 0
    Private secciones As New dbRestauranteSecciones(MySqlcon)
    Private hilo As Thread
    Private activarTeclado As Boolean
    Private hilo2 As New ThreadStart(AddressOf actualizar)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        config.llenaDatos()
        'LlenaCombos("tblrestaurantesecciones", comboSeccion, "nombre", "nombret", "idseccion", listaSecciones)
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim frm As New frmRestauranteAcomodarMesas(GlobalIdSucursalDefault)
        frm.ShowDialog()
        MuestraMesas()
    End Sub

    Private Sub frmRestaurantePrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        muestraSecciones()
        idSeccion = CInt(panelSecciones.Controls(0).Name)
        secciones.buscar(idSeccion)
        panelSecciones.Controls(0).BackColor = Color.FromArgb(87, 183, 230)
        Try
            panelMesas.BackgroundImage = Image.FromFile(secciones.rutaMapa)
        Catch

        End Try
        MuestraMesas()
        activarTeclado = config.activarTeclado
        Me.BackColor = Color.FromArgb(config.colorVentanas)
        PanelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
        'Activar(False)
        GlobalUsuarioActivado = True
        Dim Us As New dbUsuarios(GlobalIdUsuario, MySqlcon)
        vendedores = New dbVendedores(Us.IdVendedor, MySqlcon)
        Me.Show()
        If Us.IdVendedor <= 0 Then
            MsgBox("Este usuario no tiene asociado ningun vendedor, no podrá realizar ninguna operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            GlobalUsuarioActivado = False
        End If
        InitializeTimer()
        InitializeTimer2()
    End Sub

    Private Sub MuestraMesas()
        Dim config As New dbRestauranteConfiguracion(1, MySqlcon)

        listaMesas = mesasController.listaMesas(idSeccion)
        panelMesas.Controls.Clear()
        For Each m As RestauranteMesa In listaMesas
            m.config = config
            m.checaEstado()
            panelMesas.Controls.Add(m)
        Next
        panelMesas.Refresh()
    End Sub

    Private Sub btnReservar_Click(sender As Object, e As EventArgs) Handles btnReservar.Click
        Dim f As New frmRestauranteReservaciones()
        f.ShowDialog()
    End Sub

    Private Sub LogIn()
        Dim f As New frmCambioUsuario(0, 0)
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPass.Text = GlobalUsuario
            GlobalUsuarioActivado = True
            Dim us As New dbUsuarios(GlobalIdUsuario, MySqlcon)
            If us.IdVendedor <= 0 Then
                MsgBox("Este usuario no tiene asociado ningun vendedor, no podrá realizar ninguna operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                GlobalUsuarioActivado = False
            End If
        End If
        f.Dispose()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim f As New frmCambioUsuario(0, 0)
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPass.Text = GlobalUsuario
            'Activar(True)
        End If
        f.Dispose()
    End Sub

    Private Sub txtPass_Enter(sender As Object, e As EventArgs) Handles txtPass.Enter
        If activarTeclado Then
            If tecladoActivo = False Then
                t = New frmRestauranteTeclado(txtPass)
                t.Show()
            Else
                t.BringToFront()
            End If
        End If
        'Dim progFiles As String = "C:\Program Files\Common Files\Microsoft Shared\ink"
        'Dim keyboardPath As String = Path.Combine(progFiles, "TabTip.exe")
        'Process.Start(keyboardPath)
    End Sub

    Private Sub frmRestaurantePrincipal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        GlobalEstadoRestaurante = "Cerrado"
    End Sub

    Private Sub muestraMesasCuadricula()
        panelMesas.BackgroundImage = Nothing
        listaMesas = mesasController.listaMesas(idSeccion)
        panelMesas.Controls.Clear()
        Dim x = 0
        Dim y = 0
        'Dim b As Button
        For Each m As RestauranteMesa In listaMesas
            If x >= panelMesas.Width Then
                x = 0
                y += 60
            End If
            m.checaEstado()
            m.Height = 60
            m.Width = panelMesas.Width / 10
            m.Location = New Point(x, y)
            panelMesas.Controls.Add(m)
            'b = New Button()
            'checaEstado(m.estado, m.numero, b)
            'b.Height = 30
            'b.Width = panelMesas.Width / 10
            'b.Location = New Point(x, y)
            'AddHandler b.Click, AddressOf mesaClick
            'panelMesas.Controls.Add(b)
            x += panelMesas.Width / 10
        Next
        panelMesas.Refresh()
    End Sub

    'Public Sub checaEstado(ByVal estado As Integer, ByVal numero As Integer, ByVal b As Button)

    '    Select Case estado
    '        Case EstadosMesas.Libre
    '            b.BackColor = Color.FromArgb(config.colorLibre)
    '            If config.colorLetraLibre <> "" Then
    '                b.ForeColor = Color.FromArgb(config.colorLetraLibre)
    '            Else
    '                b.ForeColor = Color.Black
    '            End If
    '            b.Text = "Mesa " + numero.ToString() + ": " + config.textoLibre
    '        Case EstadosMesas.Ocupada
    '            b.BackColor = Color.FromArgb(config.colorOcupado)
    '            If config.colorLetraOcupado <> "" Then
    '                b.ForeColor = Color.FromArgb(config.colorLetraOcupado)
    '            Else
    '                b.ForeColor = Color.Black
    '            End If
    '            b.Text = "Mesa " + numero.ToString() + ": " + config.textoOcupado
    '        Case EstadosMesas.Reservada
    '            b.BackColor = Color.FromArgb(config.colorReservado)
    '            If config.colorReservado <> "" Then
    '                b.ForeColor = Color.FromArgb(config.colorReservado)
    '            Else
    '                b.ForeColor = Color.Black
    '            End If
    '            b.Text = "Mesa " + numero.ToString() + ": " + config.textoReservado
    '    End Select
    'End Sub

    Private Sub mesaClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If GlobalUsuarioActivado = False Then LogIn()
        If GlobalUsuarioActivado Then muestraMesasCuadricula()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If GlobalUsuarioActivado = False Then LogIn()
        If GlobalUsuarioActivado Then MuestraMesas()
    End Sub

    Private Sub actualizar()

        If tipoMapa = 0 Then
            MuestraMesas()
        Else
            muestraMesasCuadricula()
        End If
    End Sub

    Private Sub muestraSecciones()
        Dim x = 0
        Dim y = 0
        tablaSecciones = secciones.vistaSecciones(GlobalIdSucursalDefault).ToTable
        For Each r As DataRow In tablaSecciones.Rows
            If x >= panelSecciones.Width Then
                x = 0
                y += 60
            End If
            Dim b As New Button()
            b.Name = r("idseccion")
            b.Text = r("nombre")
            b.Width = panelSecciones.Width / 4
            b.Height = 60
            b.BackColor = Color.FromArgb(190, 203, 205)
            b.Location = New Point(x, y)
            AddHandler b.Click, AddressOf clickSecciones
            panelSecciones.Controls.Add(b)
            x += panelSecciones.Width / 4
        Next
    End Sub

    Private Sub clickSecciones(sender As Object, e As EventArgs)
        If GlobalUsuarioActivado = False Then LogIn()
        If GlobalUsuarioActivado Then
            For Each c As Control In panelSecciones.Controls
                If TypeOf c Is Button Then c.BackColor = Color.FromArgb(190, 203, 205)
            Next
            Dim b As Button = DirectCast(sender, Button)
            idSeccion = CInt(b.Name)
            secciones.buscar(idSeccion)
            b.BackColor = Color.FromArgb(87, 183, 230)
            Try
                panelMesas.BackgroundImage = Image.FromFile(secciones.rutaMapa)
            Catch

            End Try
            MuestraMesas()
        End If
    End Sub

    Private Sub frmRestaurantePrincipal_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        PanelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    'Private Sub Activar(ByVal activado As Boolean)
    '    Button1.Enabled = activado
    '    Button4.Enabled = activado
    '    panelSecciones.Enabled = activado
    '    panelMesas.Enabled = activado
    'End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        actualizar()
    End Sub

    Private Sub InitializeTimer()
        ' Run this procedure in an appropriate event.
        ' Set to 1 second.
        Timer1.Interval = 10000
        ' Enable timer.
        Timer1.Enabled = True
        If GlobalUsuarioActivado Then txtPass.Text = GlobalUsuario
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'Activar(False)
        GlobalUsuarioActivado = False
    End Sub

    Private Sub InitializeTimer2()
        Timer2.Interval = 30000
        Timer2.Enabled = True
    End Sub
End Class