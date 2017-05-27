Public Class frmRestauranteBuscador
    Private ancho As Double
    Private alto As Double
    Private x As Double = 0
    Private y As Double = 0
    Private anchoPrincipal As Double
    Private altoPrincipal As Double
    Private Yprincipal As Double = 0
    Private totalClases As Integer
    Private inventario As dbInventario
    Private clasificacion As dbInventarioClasificaciones
    Private configuracion As dbRestauranteConfiguracion
    Private idsClas1 As New elemento
    Private idsClas2 As New elemento
    Private idsClas3 As New elemento
    Private idClas1 As Integer = -1
    Private idClas2 As Integer = -1
    Private idClas3 As Integer = -1
    Private cuantosHorizontal As Integer
    Private cuantosVertical As Integer
    'Private colores As Color() = {Color.Red, Color.Blue, Color.Yellow, Color.SandyBrown, Color.Green}
    Private colorClas As Color
    Private colores As dbRestauranteColores
    Public clave As New List(Of String)
    Private comidaCorrida As Boolean = False
    Private comidas As New dbrestaurantecomidacorrida(MySqlcon)
    Private lista As New List(Of String)
    Private index As Integer
    Private Sub frmRestauranteBuscador_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        inventario = New dbInventario(MySqlcon)
        colores = New dbRestauranteColores(MySqlcon)
        configuracion = New dbRestauranteConfiguracion(1, MySqlcon)
        clasificacion = New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        anchoPrincipal = panelCategorias.Width
        totalClases = clasificacion.totalClasificaciones
        altoPrincipal = panelCategorias.Height / totalClases
        ancho = panelPlatillos.Width
        alto = panelPlatillos.Height
        Me.BackColor = Color.FromArgb(configuracion.colorVentanas)
        muestraCategorias()
        'LlenaCombos("tblinventarioclasificaciones", comboClas1, "nombre", "nombret", "idclasificacion", idsClas1, , "Todas")
        'muestraMenu()
    End Sub

    Private Sub muestraCategorias()
        Yprincipal = 0
        Dim lista As List(Of String) = clasificacion.listaClasificaciones()
        For i As Integer = 0 To lista.Count - 1
            Dim b As New Button()
            b.Width = anchoPrincipal
            b.Height = altoPrincipal
            b.Location = New Point(0, Yprincipal)
            clasificacion.BuscaClasificacion(lista(i))
            b.Name = clasificacion.ID.ToString()
            b.Text = clasificacion.Nombre
            If colores.buscarPorClasifiacacion(clasificacion.ID) Then
                b.BackColor = Color.FromArgb(colores.color)
            End If
            colorClas = b.BackColor
            b.BackColor = colorClas
            AddHandler b.Click, AddressOf muestraPlatillos
            panelCategorias.Controls.Add(b)
            Yprincipal += altoPrincipal
        Next
    End Sub
    Private Sub muestraMenu()
        Dim anchoBoton As Integer
        Dim altoBoton As Integer
        cuantosHorizontal = configuracion.horizontal
        cuantosVertical = configuracion.vertical
        If buscadorPorPaginas Then
            anchoBoton = (ancho / cuantosHorizontal) - 4
            altoBoton = (alto / cuantosVertical) - 4
        Else
            'anchoBoton = (ancho / 4) - 4
            anchoBoton = (ancho / cuantosHorizontal) - 4
            altoBoton = 60
        End If
        y = 0
        x = 0
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
                If x + (ancho / 4) >= (ancho - 4) Then
                    y += 60
                    x = 0
                End If
                Dim b As New Button()
                inventario.BuscaArticulo(s, 0)
                b.Width = (ancho / 4) - 6
                b.Height = 60
                b.Location = New Point(x, y)
                b.Name = s
                b.Text = inventario.Nombre
                b.BackColor = colorClas
                AddHandler b.Click, AddressOf agregaClick
                panelPlatillos.Controls.Add(b)
                x += (ancho / 4) - 6
                'y += 20
            Next
        End If
    End Sub

    Private Sub agregaClick(sender As Object, e As EventArgs)
        Dim b As Button = DirectCast(sender, Button)
        clave.Add(b.Name)
        inventario.BuscaArticulo(b.Name, 0)
        'txtAgregados.Items.Add(inventario.Nombre)
        lista.Add(inventario.Nombre)
        txtAgregados.Items.Clear()
        For Each s As String In lista
            txtAgregados.Items.Add(s)
        Next
    End Sub

    Private Sub muestraPlatillos(sender As Object, e As EventArgs)
        Dim b As Button = DirectCast(sender, Button)
        idClas1 = CInt(b.Name)
        colorClas = b.BackColor
        comidaCorrida = False
        muestraMenu()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Dispose()
    End Sub

    Private Sub panelPlatillos_MouseMove(sender As Object, e As MouseEventArgs) Handles panelPlatillos.MouseMove

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        comidaCorrida = True
        muestraMenu()
    End Sub

    Private Sub txtAgregados_Click(sender As Object, e As EventArgs) Handles txtAgregados.Click
        index = txtAgregados.SelectedIndex
        btnEliminar.Visible = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        clave.RemoveAt(index)
        lista.RemoveAt(index)
        txtAgregados.Items.Clear()
        For Each s As String In lista
            txtAgregados.Items.Add(s)
        Next
        btnEliminar.Visible = False
    End Sub
End Class