Public Class frmRestauranteBuscador
    
    Private inventario As dbInventario
    Private clasificacion As dbInventarioClasificaciones
    Private configuracion As dbRestauranteConfiguracion
   
    Private idClas1 As Integer = -1
    Private idClas2 As Integer = -1
    Private idClas3 As Integer = -1
   
    Private colorClas As Color
    Private colores As dbRestauranteColores

    Private comidaCorrida As Boolean = False
    Private comidas As New dbrestaurantecomidacorrida(MySqlcon)
    
    Private enDrag As Boolean
    Private _idComensal As Integer
    Private _idVenta As Integer

    Dim myMouseDownPoint As Point
    Dim myCurrAutoSMouseDown As Point

    Public Sub New(idVenta As Integer, idComensal As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _idVenta = idVenta
        _idComensal = idComensal
    End Sub

    Private Sub Button_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        myMouseDownPoint = PointToClient(Cursor.Position)
        myCurrAutoSMouseDown = DirectCast(DirectCast(sender, Button).Parent, FlowLayoutPanel).AutoScrollPosition
    End Sub

    Private Sub Button_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim mLocation As Point = PointToClient(Cursor.Position)
            If myMouseDownPoint <> mLocation Then
                Dim mCurrAutoS As Point
                Dim mDeslocation As Point = myMouseDownPoint - mLocation
                mCurrAutoS.X = Math.Abs(myCurrAutoSMouseDown.X) + mDeslocation.X
                mCurrAutoS.Y = Math.Abs(myCurrAutoSMouseDown.Y) + mDeslocation.Y

                DirectCast(DirectCast(sender, Button).Parent, FlowLayoutPanel).AutoScrollPosition = mCurrAutoS

            End If
            enDrag = True
        End If
    End Sub

    Private Sub frmRestauranteBuscador_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        inventario = New dbInventario(MySqlcon)
        colores = New dbRestauranteColores(MySqlcon)
        configuracion = New dbRestauranteConfiguracion(1, MySqlcon)
        clasificacion = New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        'anchoPrincipal = panelCategorias.Width
        'totalClases = clasificacion.totalClasificaciones
        'altoPrincipal = panelCategorias.Height / totalClases
        'ancho = panelPlatillos.Width
        'alto = panelPlatillos.Height
        Me.BackColor = Color.FromArgb(configuracion.colorVentanas)
        muestraCategorias()
        If panelCategorias.Controls.Count > 0 Then DirectCast(panelCategorias.Controls(0), Button).PerformClick()

        Dim dbdetalles As New dbRestauranteVentasDetalles(MySqlcon)
        lstAgregados.DataSource = dbdetalles.vistaDetalles(_idVenta, estadosPlatillos.sinEnviar)
        
    End Sub

    Private Sub muestraCategorias()
        'Yprincipal = 0
        Dim lista As List(Of String) = clasificacion.listaClasificaciones()
        For i As Integer = 0 To lista.Count - 1
            Dim b As New Button()
            b.Parent = panelCategorias
            b.Width = 176 'anchoPrincipal
            b.Height = 80 'altoPrincipal
            'b.Location = New Point(0, Yprincipal)
            clasificacion.BuscaClasificacion(lista(i))
            b.Name = clasificacion.ID.ToString()
            b.Text = clasificacion.Nombre
            If colores.buscarPorClasifiacacion(clasificacion.ID) Then
                b.BackColor = Color.FromArgb(colores.color)
            End If
            colorClas = b.BackColor
            b.BackColor = colorClas
            AddHandler b.Click, AddressOf muestraPlatillos
            AddHandler b.MouseDown, AddressOf Button_MouseDown
            AddHandler b.MouseMove, AddressOf Button_MouseMove

            panelCategorias.Controls.Add(b)
            'Yprincipal += altoPrincipal

        Next
    End Sub
    Private Sub muestraMenu()
        'Dim anchoBoton As Integer
        'Dim altoBoton As Integer
        ''cuantosHorizontal = configuracion.horizontal
        ''cuantosVertical = configuracion.vertical
        ''If buscadorPorPaginas Then
        ''    anchoBoton = (ancho / cuantosHorizontal) - 4
        ''    altoBoton = (alto / cuantosVertical) - 4
        ''Else
        ''anchoBoton = (ancho / 4) - 4
        'anchoBoton = 100 '(ancho / cuantosHorizontal) - 4
        'altoBoton = 60
        'End If
        'y = 0
        'x = 0
        panelPlatillos.Controls.Clear()
        Dim lista As List(Of String)
        If comidaCorrida Then
            Dim aux As List(Of Integer) = comidas.lista(CInt(Date.Now.DayOfWeek))
            lista = New List(Of String)
            For Each i As Integer In aux
                inventario = New dbInventario(i, MySqlcon)
                lista.Add(inventario.Clave)
            Next
        Else
            lista = inventario.listaClaves(idClas1, idClas2, idClas3)
        End If
        If lista.Count > 0 Then
            For Each s As String In lista
                'If x + (ancho / 4) >= (ancho - 4) Then
                '    y += 60
                '    x = 0
                'End If
                Dim b As New Button()
                b.Parent = panelPlatillos
                inventario.BuscaArticulo(s, 0, "")
                b.Width = 120 '(ancho / 4) - 6
                b.Height = 80
                'b.Location = New Point(x, y)
                b.Name = s
                b.Text = inventario.Nombre
                b.BackColor = colorClas
                AddHandler b.Click, AddressOf agregaClick
                b.Tag = inventario.ID
                panelPlatillos.Controls.Add(b)
                'x += (ancho / 4) - 6
                'y += 20
                AddHandler b.MouseDown, AddressOf Button_MouseDown
                AddHandler b.MouseMove, AddressOf Button_MouseMove

            Next
        End If
    End Sub

    Private Sub agregaClick(sender As Object, e As EventArgs)
        If enDrag Then
            enDrag = False
        Else
            Dim b As Button = DirectCast(sender, Button)
            inventario.BuscaArticulo(b.Name, 0, "")
            inventario.LlenaDatos()
            Dim dbdetalles As New dbRestauranteVentasDetalles(MySqlcon)
            Dim precios As New dbInventarioPrecios(MySqlcon)
            precios.BuscaPrecio(inventario.ID, 1)
            dbdetalles.agregar(inventario.ID, 1, inventario.Nombre, precios.precio, inventario.Iva, _idVenta, "", _idComensal)
            lstAgregados.DataSource = dbdetalles.vistaDetalles(_idVenta, estadosPlatillos.sinEnviar)
        End If

    End Sub

    Private Sub muestraPlatillos(sender As Object, e As EventArgs)
        If enDrag Then
            enDrag = False
        Else
            Dim b As Button = DirectCast(sender, Button)
            idClas1 = CInt(b.Name)
            colorClas = b.BackColor
            comidaCorrida = False
            muestraMenu()
        End If
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Dispose()
    End Sub

    Private Sub panelPlatillos_MouseMove(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnComidaCorrida.Click
        'comidaCorrida = True
        muestraMenu()
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim dbdetalles As New dbRestauranteVentasDetalles(MySqlcon)
        If lstAgregados.SelectedIndex > -1 Then
            dbdetalles.eliminar(lstAgregados.SelectedValue)
            lstAgregados.DataSource = dbdetalles.vistaDetalles(_idVenta, estadosPlatillos.sinEnviar)
            lstAgregados.SelectedIndex = lstAgregados.Items.Count - 1
        End If
    End Sub

    Private Sub lstAgregados_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAgregados.SelectedIndexChanged
        btnEliminar.Enabled = lstAgregados.SelectedIndex > -1
    End Sub
End Class