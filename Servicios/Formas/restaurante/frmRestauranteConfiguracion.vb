	Imports MySql.Data.MySqlClient
Public Class frmRestauranteConfiguracion
    Private idConfig As Integer = 1
    Private idInventario1 As Integer
    Private idInventario2 As Integer
    Private idInventario3 As Integer
    Private idInventario4 As Integer
    Private idInventario5 As Integer
    Private clave1 As String
    Private clave2 As String
    Private clave3 As String
    Private clave4 As String
    Private clave5 As String
    Private fuente As String
    Private tamano As Integer
    Private colorLetra As New String("")
    Private colorLibre As New String("")
    Private colorOcupada As New String("")
    Private colorReservada As New String("")
    Private colorLetraLibre As New String("")
    Private colorLetraOcupado As New String("")
    Private colorLetraReservado As New String("")
    Private textoLibre As String
    Private textoOcupado As String
    Private textoReservado As String
    Private inventario As dbInventario
    Private configuracion As dbRestauranteConfiguracion
    Private listacajas As New elemento
    Private listaVendedores As New elemento
    Private idCaja As Integer
    Private clienteDefault As dbClientes
    Private mediosPagos As dbRestauranteFormasPagos
    Private vendedorDefault As Integer
    Private nuevo As Boolean = True
    Private nuevaSeccion As Boolean = True
    Private idMedio As Integer
    Private secciones As dbRestauranteSecciones
    Private idSeccion As Integer
    Private ancho As Double
    Private alto As Double
    Private x = 0
    Private y = 0
    Private clasificacion As dbInventarioClasificaciones
    Private clasColor As dbRestauranteColores
    Private cajas As New List(Of TextBox)
    Private colores As New List(Of String)
    Private IdsMeseros As New elemento
    Private idMesero As Integer
    Private colorVentanas As New String("")
    Private rutaMapa As String
    Private imagen As Bitmap
    Private horizontal As Integer
    Private vertical As Integer
    Dim idsSucursales As New elemento
    Private dias() As String = {"Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo"}
    Private comidas As New dbrestaurantecomidacorrida(MySqlcon)
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        configuracion = New dbRestauranteConfiguracion(idConfig, MySqlcon)
        mediosPagos = New dbRestauranteFormasPagos(MySqlcon)
        inventario = New dbInventario(MySqlcon)
        secciones = New dbRestauranteSecciones(MySqlcon)
        clasificacion = New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        clasColor = New dbRestauranteColores(MySqlcon)
        LlenaCombos("tblcajas", comboCaja, "nombre", "nombret", "idcaja", listacajas)
        LlenaCombos("tblvendedores", comboVendedores, "nombre", "nombret", "idvendedor", listaVendedores)
        LlenaCombos("tblrestaurantemeseros", comboMeseros, "nombre", "nombret", "idmesero", IdsMeseros)
        LlenaCombos("tblsucursales", comboSucursal, "nombre", "nombret", "idsucursal", idsSucursales)
        ' Add any initialization after the InitializeComponent() call.
        llenaGridSecciones()
        muestraCategorias()
    End Sub
    Private Sub frmRestauranteConfiguracion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        llenaDatos()
        Me.BackColor = Color.FromArgb(configuracion.colorVentanas)
        tabConfiguracion.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
        For Each d As String In dias
            comboComida.Items.Add(d)
        Next
        comboCaja.SelectedIndex = 0
        comboComida.SelectedIndex = 0
        comboSucursal.SelectedIndex = idsSucursales.Busca(GlobalIdSucursalDefault)
    End Sub

    Private Sub llenaDatos()
        If configuracion.llenaDatos Then
            buscar(configuracion.claveProducto1, txtClave1, txtNombre1, idInventario1, clave1)
            buscar(configuracion.claveProducto2, txtClave2, txtNombre2, idInventario2, clave2)
            buscar(configuracion.claveProducto3, txtClave3, txtNombre3, idInventario3, clave3)
            buscar(configuracion.claveProducto4, txtClave4, txtNombre4, idInventario4, clave4)
            buscar(configuracion.claveProducto5, txtClave5, txtNombre5, idInventario5, clave5)
            '  buscaColor(txtColor, configuracion.colorLetra, colorLetra)
            buscaColor(txtColorLibre, configuracion.colorLibre, colorLibre)
            buscaColor(txtColorOcupada, configuracion.colorOcupado, colorOcupada)
            buscaColor(txtColorReservada, configuracion.colorReservado, colorReservada)
            colorLetraLibre = configuracion.colorLetraLibre
            colorLetraOcupado = configuracion.colorLetraOcupado
            colorLetraReservado = configuracion.colorLetraReservado
            If configuracion.colorVentanas = "" Then
                colorVentanas = Me.BackColor.ToArgb()
            Else
                colorVentanas = configuracion.colorVentanas
            End If
            panelColorLetraLibre.BackColor = Color.FromArgb(colorLetraLibre)
            panelColorLetraOcupado.BackColor = Color.FromArgb(colorLetraOcupado)
            panelColorLetraReservado.BackColor = Color.FromArgb(colorLetraReservado)
            panelFondoVentanas.BackColor = Color.FromArgb(colorVentanas)
            textoLibre = configuracion.textoLibre
            txtTextoLibre.Text = textoLibre
            textoOcupado = configuracion.textoOcupado
            txtTextoOcupado.Text = textoOcupado
            textoReservado = configuracion.textoReservado
            txtTextoReservado.Text = textoReservado
            clienteDefault = New dbClientes(configuracion.clienteDefault, MySqlcon)
            txtCliente.Text = clienteDefault.Nombre
            idCaja = configuracion.cajaDefault
            comboCaja.SelectedIndex = listacajas.Busca(idCaja)
            vendedorDefault = configuracion.vendedorDefault
            comboVendedores.SelectedIndex = listaVendedores.Busca(vendedorDefault)
            txtFuente.Text = configuracion.fuente
            txtTamano.Text = configuracion.tamano
            txtHorizontal.Text = configuracion.horizontal.ToString
            txtVertical.Text = configuracion.vertical.ToString
        Else
            configuracion.agregar("0001", "0002", "0003", "0004", "0005", "Arial", "10", Color.Green.ToArgb, Color.Red.ToArgb, Color.Yellow.ToArgb, Color.Black.ToArgb, Color.Black.ToArgb, Color.Black.ToArgb, "Libre", "Ocupado", "Reservado", 2, 1, 2, 1, Me.BackColor.ToArgb, True, 0, 0)
            idConfig = configuracion.regresaIdConfig
            llenaDatos()
        End If
        CheckBox2.Checked = buscadorPorPaginas
    End Sub



    Private Sub buscar(ByVal clave As String, ByVal cajaClave As TextBox, ByVal cajaNombre As TextBox, ByRef idInventario As Integer, ByRef claveArticulo As String)
        If inventario.BuscaArticulo(clave, 0, True) Then
            cajaClave.Text = inventario.Clave
            claveArticulo = inventario.Clave
            cajaNombre.Text = inventario.Nombre
            idInventario = inventario.ID
        Else
            cajaNombre.Text = ""
            idInventario = -1
        End If
    End Sub

    Private Sub buscaColor(ByRef cajaColor As Panel, ByVal pColor As String, ByRef atributoColor As String)
        If pColor <> "" Then
            atributoColor = pColor
            Dim c As Color = Color.FromArgb(pColor)
            cajaColor.BackColor = c
        End If
    End Sub

    Private Sub busquedaBoton(ByRef idInventario As Integer, ByVal cajaClave As TextBox, cajaNombre As TextBox, ByRef clave As String)
        Dim f As New frmBuscador(frmBuscador.TipoDeBusqueda.SoloRestaurante, 0, False, False, False)
        f.ShowDialog()
        If Not f.Inventario Is Nothing Then
            inventario = f.Inventario
            idInventario = inventario.ID
            cajaClave.Text = inventario.Clave
            cajaNombre.Text = inventario.Nombre
            clave = inventario.Clave
        End If
    End Sub

    Private Sub guardar()
        textoLibre = txtTextoLibre.Text
        textoOcupado = txtTextoOcupado.Text
        textoReservado = txtTextoReservado.Text
        horizontal = CInt(If(txtHorizontal.Text = "", "0", txtHorizontal.Text))
        vertical = CInt(If(txtVertical.Text = "", "0", txtVertical.Text))
        Try
            buscadorPorPaginas = CheckBox2.Checked
            'configuracion = New dbRestauranteConfiguracion(1, MySqlcon)
            configuracion.actualiza(idConfig, clave1, clave2, clave3, clave4, clave5, fuente, tamano, colorLibre, colorOcupada, colorReservada, colorLetraLibre, colorLetraOcupado, colorLetraReservado, textoLibre, textoOcupado, textoReservado, clienteDefault.ID, idCaja, vendedorDefault, idMesero, colorVentanas, CheckBox1.Checked, horizontal, vertical)
            PopUp("guardado", 30)
        Catch ex As Exception
            MsgBox("No se pudo guardar la configuración. " + ex.ToString())
        End Try
    End Sub

    Private Sub btnBuscar1_Click(sender As Object, e As EventArgs) Handles btnBuscar1.Click
        busquedaBoton(idInventario1, txtClave1, txtNombre1, clave1)
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        guardar()
        Me.Close()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnBuscar2_Click(sender As Object, e As EventArgs) Handles btnBuscar2.Click
        busquedaBoton(idInventario2, txtClave2, txtNombre2, clave2)
    End Sub

    Private Sub btnBuscar3_Click(sender As Object, e As EventArgs) Handles btnBuscar3.Click
        busquedaBoton(idInventario3, txtClave3, txtNombre3, clave3)
    End Sub

    Private Sub btnBuscar4_Click(sender As Object, e As EventArgs) Handles btnBuscar4.Click
        busquedaBoton(idInventario4, txtClave4, txtNombre4, clave4)
    End Sub

    Private Sub btnBuscar5_Click(sender As Object, e As EventArgs) Handles btnBuscar5.Click
        busquedaBoton(idInventario5, txtClave5, txtNombre5, clave5)
    End Sub

    Private Sub txtClave1_TextChanged(sender As Object, e As EventArgs) Handles txtClave1.TextChanged
        buscar(txtClave1.Text, txtClave1, txtNombre1, idInventario1, clave1)
    End Sub

    Private Sub txtClave2_TextChanged(sender As Object, e As EventArgs) Handles txtClave2.TextChanged
        buscar(txtClave2.Text, txtClave2, txtNombre2, idInventario2, clave2)
    End Sub

    Private Sub txtClave3_TextChanged(sender As Object, e As EventArgs) Handles txtClave3.TextChanged
        buscar(txtClave3.Text, txtClave3, txtNombre3, idInventario3, clave3)
    End Sub

    Private Sub txtClave4_TextChanged(sender As Object, e As EventArgs) Handles txtClave4.TextChanged
        buscar(txtClave4.Text, txtClave4, txtNombre4, idInventario4, clave4)
    End Sub

    Private Sub txtClave5_TextChanged(sender As Object, e As EventArgs) Handles txtClave5.TextChanged
        buscar(txtClave5.Text, txtClave5, txtNombre5, idInventario5, clave5)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnLibre.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            buscaColor(txtColorLibre, ColorDialog1.Color.ToArgb(), colorLibre)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnOcupada.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            buscaColor(txtColorOcupada, ColorDialog1.Color.ToArgb, colorOcupada)
        End If
    End Sub

    Private Sub btnReservada_Click(sender As Object, e As EventArgs) Handles btnReservada.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            buscaColor(txtColorReservada, ColorDialog1.Color.ToArgb, colorReservada)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If FontDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            fuente = FontDialog1.Font.Name
            txtFuente.Text = fuente
            tamano = CDbl(FontDialog1.Font.Size)
            txtTamano.Text = tamano
            buscaColor(txtColor, FontDialog1.Color.ToArgb, colorLetra)
        End If
    End Sub

    Private Sub btnLetraLibre_Click(sender As Object, e As EventArgs) Handles btnLetraLibre.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            buscaColor(panelColorLetraLibre, ColorDialog1.Color.ToArgb(), colorLetraLibre)
        End If
    End Sub

    Private Sub btnLetraOcupado_Click(sender As Object, e As EventArgs) Handles btnLetraOcupado.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            buscaColor(panelColorLetraOcupado, ColorDialog1.Color.ToArgb(), colorLetraOcupado)
        End If
    End Sub

    Private Sub btnLetraReservado_Click(sender As Object, e As EventArgs) Handles btnLetraReservado.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            buscaColor(panelColorLetraReservado, ColorDialog1.Color.ToArgb(), colorLetraReservado)
        End If
    End Sub

    Private Sub comboCaja_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboCaja.SelectedIndexChanged
        idCaja = listacajas.Valor(comboCaja.SelectedIndex)
    End Sub

    Private Sub btnCliente_Click(sender As Object, e As EventArgs) Handles btnCliente.Click
        Dim f As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 1, False, False, False)
        f.ShowDialog()
        If Not f.Cliente Is Nothing Then
            clienteDefault = f.Cliente
            txtCliente.Text = clienteDefault.Nombre
        End If
    End Sub


    Private Sub comboVendedores_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboVendedores.SelectedIndexChanged
        vendedorDefault = listaVendedores.Valor(comboVendedores.SelectedIndex)
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If nuevaSeccion Then
            secciones.guardar(idsSucursales.Valor(comboSucursal.SelectedIndex), CInt(txtNumSeccion.Text), txtNombre.Text, rutaMapa)
            llenaGridSecciones()
            NuevoSeccion()
        Else
            secciones.modificar(idSeccion, idsSucursales.Valor(comboSucursal.SelectedIndex), CInt(txtNumSeccion.Text), txtNombre.Text, rutaMapa)
            llenaGridSecciones()
            NuevoSeccion()
        End If
    End Sub

    Private Sub dgvSecciones_Click(sender As Object, e As EventArgs) Handles dgvSecciones.Click
        Try
            idSeccion = CInt(dgvSecciones.CurrentRow.Cells(0).Value.ToString())
            secciones.buscar(idSeccion)
            txtNombre.Text = secciones.nombre
            txtNumSeccion.Text = secciones.numero
            txtRuta.Text = secciones.rutaMapa
            nuevaSeccion = False
            comboSucursal.Enabled = False
            btnAceptar.Text = "Modificar"
            btnEliminar.Enabled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub llenaGridSecciones()
        dgvSecciones.DataSource = secciones.vistaSecciones(idsSucursales.Valor(comboSucursal.SelectedIndex))
        dgvSecciones.Columns(0).Visible = False
        dgvSecciones.Columns(1).Visible = False
        dgvSecciones.Columns(4).Visible = False
        dgvSecciones.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvSecciones.Columns(2).HeaderText = "No. Sec."
        dgvSecciones.Columns(3).HeaderText = "Sección"
    End Sub

    Private Sub NuevoSeccion()
        nuevaSeccion = True
        txtNombre.Text = ""
        txtNumSeccion.Text = ""
        btnEliminar.Enabled = False
        comboSucursal.Enabled = True
        btnAceptar.Text = "Guardar"
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If MsgBox("Eliminar esta sección.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            secciones.eliminar(idSeccion)
            llenaGridSecciones()
            NuevoSeccion()
        End If
    End Sub

    Private Sub btnNuevaSeccion_Click(sender As Object, e As EventArgs) Handles btnNuevaSeccion.Click
        NuevoSeccion()
    End Sub

    Private Sub muestraCategorias()
        cajas = New List(Of TextBox)
        colores = New List(Of String)
        y = 0
        x = 0
        Dim lista As List(Of String) = clasificacion.listaClasificaciones()
        For Each s As String In lista
            x = 0
            clasificacion.BuscaClasificacion(s)
            Dim t As New TextBox()
            Dim b As New Button()
            t.Height = 25
            t.Width = (panelCategorias.Width / 4) * 3
            t.Text = clasificacion.Nombre
            t.Enabled = False
            If clasColor.buscarPorClasifiacacion(clasificacion.ID) Then
                t.BackColor = Color.FromArgb(clasColor.color)
                't.ForeColor  = Color.FromArgb(&HFFFFFF - t.BackColor.ToArgb)
            End If
            t.Location = New Point(x, y)
            t.Name = clasificacion.Codigo
            cajas.Add(t)
            b.Width = panelCategorias.Width / 4
            b.Height = 25
            b.Name = clasificacion.ID
            b.Text = "..."
            b.BackColor = Color.Gray
            b.Location = New Point((panelCategorias.Width / 4) * 3, y - 2)
            AddHandler b.Click, AddressOf btnColor
            panelCategorias.Controls.Add(t)
            panelCategorias.Controls.Add(b)
            y += 35
        Next
    End Sub

    Private Sub btnColor(sender As Object, e As EventArgs)
        Dim b As Button = DirectCast(sender, Button)
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim c As String = ColorDialog1.Color.ToArgb
            For Each t As TextBox In cajas
                clasificacion.BuscaClasificacion(t.Name)
                If clasificacion.ID = b.Name Then
                    t.BackColor = Color.FromArgb(c)
                    Dim s As String = clasificacion.ID.ToString + "," + c
                    colores.Add(s)
                End If
            Next
        End If
        'panelCategorias.Refresh()
    End Sub

    Private Sub btnGuardarColor_Click(sender As Object, e As EventArgs) Handles btnGuardarColor.Click
        For Each s As String In colores
            Dim arr() As String = s.Split(",")
            Dim clas As Integer = CInt(arr(0))
            Dim c As String = arr(1)
            If clasColor.buscarPorClasifiacacion(clas) Then
                clasColor.modificar(clasColor.id, clas, c)
            Else
                clasColor.agregar(clas, c)
            End If
        Next
        PopUp("Guardado", 30)
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Dim f As New frmRestauranteAcomodarMesas(idsSucursales.Valor(comboSucursal.SelectedIndex))
        f.Show()
    End Sub

    Private Sub comboMeseros_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboMeseros.SelectedIndexChanged
        idMesero = IdsMeseros.Busca(comboMeseros.SelectedIndex)
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim c As String = ColorDialog1.Color.ToArgb
            panelFondoVentanas.BackColor = Color.FromArgb(c)
            colorVentanas = c
        End If
    End Sub

    Private Sub frmRestauranteConfiguracion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        GlobalEstadoRestaurante = "Cerrado"
    End Sub

    Private Sub btnRuta_Click(sender As Object, e As EventArgs) Handles btnRuta.Click
        OpenFileDialog1.Filter = "Archivos de imagen(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            imagen = Image.FromFile(OpenFileDialog1.FileName)
            txtRuta.Text = OpenFileDialog1.FileName
            rutaMapa = Replace(OpenFileDialog1.FileName, "\", "\\")
        End If
    End Sub

    Private Sub frmRestauranteConfiguracion_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        tabConfiguracion.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            txtHorizontal.Enabled = True
            txtVertical.Enabled = True
        Else
            txtHorizontal.Enabled = False
            txtVertical.Enabled = False
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim f As New frmBuscador(frmBuscador.TipoDeBusqueda.SoloRestaurante, 0, False, False, False)
        f.ShowDialog()
        If Not f.Inventario Is Nothing Then
            inventario = f.Inventario
            txtComida.Text = inventario.Nombre
        End If
    End Sub

    Private Sub dgvComidas_Click(sender As Object, e As EventArgs) Handles dgvComidas.Click
        Try
            Dim i As Integer = CInt(dgvComidas.CurrentRow.Cells(0).Value.ToString)
            comidas.buscar(i)
            inventario = New dbInventario(comidas.idInventario, MySqlcon)
            txtComida.Text = inventario.Nombre
            comboComida.SelectedIndex = comidas.dia - 1
            btnComidas.Text = "Modificar"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnComidas_Click(sender As Object, e As EventArgs) Handles btnComidas.Click
        If btnComidas.Text = "Modificar" Then
            comidas.modificar(comidas.id, inventario.ID, comboComida.SelectedIndex + 1)
            txtComida.Text = ""
            btnComidas.Text = "Agregar"
            inventario = Nothing
            llenaGridComidas()
            PopUp("Modificado", 30)
        Else
            If Not inventario Is Nothing Then
                comidas.agregrar(inventario.ID, comboComida.SelectedIndex + 1)
                txtComida.Text = ""
                inventario = Nothing
                llenaGridComidas()
                PopUp("Guardado", 30)
            Else
                MsgBox("Debe seleccionar un platillo.")
            End If
        End If
    End Sub

    Private Sub llenaGridComidas()
        dgvComidas.DataSource = comidas.vista(comboComida.SelectedIndex + 1)
        dgvComidas.Columns(0).Visible = False
        dgvComidas.Columns(1).Visible = False
        dgvComidas.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvComidas.Columns(3).Width = 100
        dgvComidas.Columns(2).HeaderText = "Artículo"
        dgvComidas.Columns(3).HeaderText = "Día"
    End Sub

    Private Sub comboComida_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboComida.SelectedIndexChanged
        llenaGridComidas()
    End Sub

    Private Sub dgvComidas_DoubleClick(sender As Object, e As EventArgs) Handles dgvComidas.DoubleClick
        Try
            If MsgBox("¿Eliminar este artículo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim id As Integer = CInt(dgvComidas.CurrentRow.Cells(0).Value.ToString())
                comidas.eliminar(id)
                llenaGridComidas()
                txtComida.Text = ""
                inventario = Nothing
                btnComidas.Text = "Agregar"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim X As String = ""
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            buscaColor(txtColor, ColorDialog1.Color.ToArgb(), X)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        txtComida.Text = ""
        btnComidas.Text = "Agregar"
        inventario = Nothing
    End Sub

    Private Sub comboSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboSucursal.SelectedIndexChanged
        llenaGridSecciones()
        NuevoSeccion()
    End Sub

    
    Private Sub txtVertical_TextChanged(sender As Object, e As EventArgs) Handles txtVertical.TextChanged

    End Sub
End Class