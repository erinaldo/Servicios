Public Class frmRestaurantePuntoVenta
    Private sf As dbSucursalesFolios
    Private inv As New dbInventario(MySqlcon)
    Private listaPagar As List(Of String)
    Private idSeccion As Integer = -1
    Private idVenta = -1
    Private idMesa As Integer
    Private mesas As New dbRestauranteMesas(MySqlcon, GlobalIdSucursalDefault)
    Private descuentos As New dbRestauranteDescuentos(MySqlcon)
    Private listaSecciones As New elemento
    Private ventas As New dbRestauranteVentas(MySqlcon)
    Private detalles As New dbRestauranteVentasDetalles(MySqlcon)
    Private mesaVenta As New dbRestauranteVentasMesas(MySqlcon)
    Private teclado As frmRestauranteTeclado
    Private precios As New dbInventarioPrecios(MySqlcon)
    Private config As New dbRestauranteConfiguracion(1, MySqlcon)
    Private opciones As New dbOpciones(MySqlcon)
    Dim metodos As New dbFormasdePagoRemisiones(MySqlcon)
    Private listaDetalles As New List(Of Integer)
    Private tabla As New DataTable
    Private listaFormasPago As New elemento
    Private idFormaPago
    Private total As Double
    Public liberar As Boolean = False
    Private nuevaVenta As Boolean = True
    Private cajaClaveActiva As Boolean = False
    Private cajaRecibidoActiva As Boolean = False
    Private cajaActiva As TextBox
    Dim claves As List(Of String)
    Private idForma As Integer
    Private f As frmRestauranteTeclado
    Private cambio As Double
    Private recibido As Double
    Private platillosComensales As New dbRestauranteComensalesPlatillos(MySqlcon)
    Private tablaMetodos As New DataTable
    Private movimientoCaja As New dbCajasMovimientos(MySqlcon)
    Private movimientoCajaDetalle As New dbCajasMovimientosDetalles(MySqlcon)
    Private activarTeclado As Boolean
    Private comensales As New dbRestauranteComensales(MySqlcon)
    Private platillos As New dbRestauranteComensalesPlatillos(MySqlcon)
    Private ventasPagos As New dbRestauranteVentaPago(MySqlcon)
    Private formaPago As New dbRestauranteFormasPagos(MySqlcon)
    Public cuentaCompleta As Boolean = True
    Private mesero As dbVendedores
    Private reImprimir As Boolean = False
    Private esPedido As Boolean = False
    Private pedidos As New dbRestaurantePedidos(MySqlcon)
    Private listaMesasOcupadas As New List(Of Integer)
    Private clientes As dbClientes
    Private desc As Double = 0
    Private deLlevar As Boolean = False
    Private nuevosPlatillos As Boolean = False
    Private comandaEnviada As Boolean = False
    Private listaNuevos As New List(Of Integer)
    Private platillosComensal As New dbRestauranteComensalesPlatillos(MySqlcon)

    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim ImpNDi2 As New Collection
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    Dim CodigoBidimensional As Bitmap
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim CuantaY As Integer
    Dim TipoImpresora As String
    Dim idUltimaVenta As Integer = -1


    Public Sub New(Optional ByVal idMesa As Integer = -1, Optional ByVal idVenta As Integer = -1, Optional ByVal listaDetalles As List(Of Integer) = Nothing, Optional ByVal cuentaCompleta As Boolean = True)

        ' This call is required by the designer.
        InitializeComponent()
        'Me.listaPagar = listaPagar
        Me.idVenta = idVenta
        ventas.buscar(idVenta)
        Me.cuentaCompleta = cuentaCompleta
        If idVenta > 0 Then
            nuevaVenta = False
            mesero = New dbVendedores(ventas.idMesero, MySqlcon)
        End If
        Me.idMesa = idMesa
        'If idMesa > 0 Then
        '    listaMesasOcupadas.Add(idMesa)
        'End If
        Me.listaDetalles = listaDetalles
        ' Add any initialization after the InitializeComponent() call.
        sf = New dbSucursalesFolios(GlobalIdSucursalDefault, MySqlcon)
        'LlenaCombos("tblrestaurantesecciones", comboSeccion, "nombre", "nombret", "idseccion", listaSecciones)
        'configuraTabla()
        'llenaTabla()
        total = ventas.DaTotal(idVenta)
        lblTotal.Text = Format(total, "$#,###,##0.00######")
        lblTotal.TextAlign = ContentAlignment.MiddleRight
        lblCajero.Text = GlobalUsuario
        mesero = New dbVendedores(ventas.idMesero, MySqlcon)
        lblMesero.Text = mesero.Nombre
        lblMesa.Text = Me.idMesa.ToString
    End Sub
    Private Sub frmRestaurantePuntoVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.FromArgb(config.colorVentanas)
        panelObjetos.BackColor = Color.FromArgb(config.colorVentanas)
        activarTeclado = config.activarTeclado
        muestraMetodos()
        panelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
        muestraDellevar()
    End Sub

    'Public Sub buscaFolio()
    '    If sf.BuscaFolios(GlobalIdSucursalDefault, dbSucursalesFolios.TipoDocumentos.RestauranteVentas, 0) Then
    '        'txtSerie.Text = sf.Serie
    '    Else
    '        'txtSerie.Text = ""
    '        sf.FolioInicial = 1
    '    End If

    '    'txtFolio.Text = liquidaciones.DaNuevoFolio(txtSerie.Text)
    '    'If CInt(txtFolio.Text) < sf.FolioInicial Then
    '    '    txtFolio.Text = sf.FolioInicial.ToString
    '    'End If
    'End Sub

    Private Sub agregarPlatillo()
        Dim precio As Double
        precios.BuscaPrecio(inv.ID, 1)
        precio = precios.Precio
        detalles.agregar(inv.ID, 1, inv.Nombre, precio, inv.Iva, idVenta, "", 1)
        detalles.pagarDetalle(detalles.ultimoId, CInt(estadosPlatillos.pendiente))
    End Sub

    'Private Sub muestraPlatillos(ByVal pMesa As Integer)
    '    If mesaVenta.buscar(pMesa) Then
    '        idVenta = mesaVenta.idVenta
    '        'dgvPlatillos.DataSource = detalles.vistaDetalles(idVenta, False)
    '    End If
    'End Sub

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles txtClave.Enter
        If activarTeclado Then
            teclado = New frmRestauranteTeclado(txtClave)
            teclado.Show()
        End If
    End Sub

    Private Sub txtClave_Leave(sender As Object, e As EventArgs) Handles txtClave.Leave
        If activarTeclado Then
            teclado.Dispose()
        End If
    End Sub

    Private Sub btnVenta_Click(sender As Object, e As EventArgs)
        Dim f As New frmRestaurantePagar(idVenta, total)
        f.ShowDialog()
        If f.pagado Then
            For x As Integer = 0 To listaDetalles.Count - 1
                detalles.pagarDetalle(listaDetalles(x), True)
            Next
        End If
        If detalles.cuentaPagadaCompleta(idVenta) Then
            Dim m As RestauranteMesa = mesas.buscar(idMesa)
            m.estado = EstadosMesas.Libre
            mesas.modificar(m)
        End If
    End Sub

    'Private Sub configuraTabla()
    '    tabla.Columns.Add("id", GetType(Integer))
    '    tabla.Columns.Add("descripcion", GetType(String))
    '    tabla.Columns.Add("cantidad", GetType(Double))
    '    tabla.Columns.Add("total", GetType(Double))
    'End Sub

    'Private Sub llenaTabla()

    '    listaDetalles = ventas.listaDetalles(idVenta, CInt(estadosPlatillos.pendiente))

    '    For i As Integer = 0 To listaDetalles.Count - 1
    '        detalles.buscar(listaDetalles(i))
    '        tabla.Rows.Add(detalles.idDetalle, detalles.descripcion, detalles.cantidad, detalles.precio * detalles.cantidad)
    '    Next

    'End Sub

    'Private Function sumaTotal() As Double
    '    Dim res As Double = 0
    '    'For i As Integer = 0 To dgvPlatillos.Rows.Count() - 1
    '    'res += CDbl(dgvPlatillos.Rows(i).Cells("total").Value.ToString())
    '    'Next
    '    ventas.buscar(Me.idVenta)
    '    Dim lista As List(Of Integer)
    '    ' If deLlevar Or esPedido Then
    '    'lista = ventas.listaDetalles(ventas.idVenta, CInt(estadosPlatillos.enviado), CInt(estadosPlatillos.pendiente))
    '    ' Else
    '    lista = ventas.listaDetalles(ventas.idVenta, CInt(estadosPlatillos.pendiente), CInt(estadosPlatillos.pendiente))
    '    ' End If
    '    For Each i As Integer In lista
    '        detalles.buscar(i)
    '        res += detalles.precio
    '    Next
    '    If desc > 0 Then
    '        res -= desc
    '    End If
    '    Return res
    'End Function

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        guardar()
        limpiaDatos()
        ventas.buscar(idVenta)
    End Sub

    Private Sub guardar()
        If idForma > 0 Then
            ventas.buscar(idVenta)
            listaDetalles = ventas.listaDetalles(ventas.idVenta, -1, CInt(estadosPlatillos.pendiente).ToString)
            For i As Integer = 0 To listaDetalles.Count - 1
                detalles.buscar(listaDetalles(i))
                detalles.modificar(listaDetalles(i), detalles.idInventario, detalles.cantidad, detalles.descripcion, detalles.precio, detalles.iva, idVenta, "")
            Next
            pedidos.agregar(ventas.idVenta, DateTime.Now.ToString("yyyy/MM/dd"), TimeOfDay.ToString("HH:mm:ss"), "", pedidos.obtenFolio, CInt(Estados.Pendiente), GlobalIdUsuario, 0)
            If deLlevar Then
                pedidos.modificar(pedidos.idPedido, pedidos.idVenta, pedidos.fecha, pedidos.hora, pedidos.serie, pedidos.folio, CInt(Estados.Guardada), pedidos.idVendedor, 1)
                ventas.buscar(pedidos.idVenta)
                ventas.modificar(ventas.idVenta, ventas.idCliente, ventas.descuento, ventas.total, ventas.totalapagar, CInt(Estados.Guardada), ventas.fecha, ventas.idSucursal, GlobalUsuarioIdVendedor, My.Settings.cajadefault, "")
            Else
                ventas.modificar(idVenta, ventas.idCliente, ventas.descuento, total, total, CInt(Estados.Guardada), ventas.fecha, ventas.idSucursal, GlobalUsuarioIdVendedor, My.Settings.cajadefault, "")
            End If
            If cuentaCompleta Then
                liberar = True
            Else
                liberar = False
            End If
            imprimirTicket()
            For i As Integer = 0 To listaDetalles.Count - 1
                detalles.buscar(listaDetalles(i))
                detalles.pagarDetalle(detalles.idDetalle, CInt(estadosPlatillos.pagado))
            Next
            If esPedido = False Then
                If cuentaCompleta Then
                    For Each i As Integer In listaMesasOcupadas
                        Dim mesa As RestauranteMesa = mesas.buscar(i)
                        mesa.estado = EstadosMesas.Libre
                        mesas.modificar(mesa)
                    Next
                End If
            End If
            idUltimaVenta = ventas.idVenta
            'limpiaDatos()
            PopUp("Guardado", 30)
            limpiaDatos()
            esPedido = False
            deLlevar = False
            comandaEnviada = False
            btnCE.Enabled = False
            idVenta = -1
            muestraDellevar()
        Else
            MsgBox("Debe seleccionar una forma de pago.")
        End If
        'esPedido = False

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles btnMesas.Click
        Dim f As New frmRestauranteCambioMesa(0, listaMesasOcupadas, GlobalIdSucursalDefault)
        f.ShowDialog()
        If f.idMesaNueva > 0 Then
            Dim idmesa = f.idMesaNueva
            listaMesasOcupadas.Add(idmesa)
            Dim v As New frmRestauranteOrden(idmesa, 1, GlobalIdSucursalDefault)
            v.ShowDialog()
            If v.pagar Then
                For Each i As Integer In v.listaPagar
                    detalles.buscar(i)
                    detalles.modificar(detalles.idDetalle, detalles.idInventario, detalles.cantidad, detalles.descripcion, detalles.precio, detalles.iva, idVenta, "")
                    detalles.pagarDetalle(detalles.idDetalle, CInt(estadosPlatillos.pendiente))
                    'agregarPlatillo()
                Next
                Dim venta As Integer
                If v.cuentaCompleta Then
                    venta = v.idVenta
                Else
                    venta = v.idnuevaVenta
                End If
                ventas.eliminar(venta)
                total = ventas.DaTotal(idVenta)
                lblTotal.Text = Format(total, opciones._formatoTotal)
            End If
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles btnReservaciones.Click
        Dim f As New frmRestauranteReservaciones()
        f.Show()
    End Sub

    Private Sub btnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        Dim f As New frmRestauranteBuscador()
        'f.MdiParent = Me
        f.ShowDialog()
        If Not f.clave Is Nothing Then
            claves = f.clave
            For Each s As String In claves
                inv.BuscaArticulo(s, 0)
                agregarPlatillo()
            Next
            total = ventas.DaTotal(idVenta)
            lblTotal.Text = Format(total, opciones._formatoTotal)
            btnCE.Enabled = True
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        crearVenta()
    End Sub

    Private Sub crearVenta()
        idVenta = ventas.Guardar(ventas.obtenFolio, GlobalIdUsuario, config.clienteDefault, GlobalIdSucursalDefault, idMesa, My.Settings.cajadefault)
        ventas.buscar(idVenta)
        listaMesasOcupadas = New List(Of Integer)
        txtClave.Text = ventas.folio.ToString
        lblMesa.Text = "Caja"
    End Sub

    Private Sub btnLlevar_Click(sender As Object, e As EventArgs) Handles btnLlevar.Click
        deLlevar = True
        comandaEnviada = False
        crearVenta()
        If esPedido Then
            Dim fc As New frmRestauranteLlevar(ventas.idVenta)
            fc.ShowDialog()
            If Not fc.cliente Is Nothing Then
                ventas.idCliente = fc.cliente.ID
                clientes = fc.cliente
            End If
        End If
        Dim f As New frmRestauranteBuscador()
        'f.MdiParent = Me
        f.ShowDialog()
        If Not f.clave Is Nothing Then
            claves = f.clave
            For Each s As String In claves
                inv.BuscaArticulo(s, 0)
                agregarPlatillo()
            Next
            imprimir()
            nuevosPlatillos = False
            total = ventas.DaTotal(idVenta)
            lblTotal.Text = Format(total, opciones._formatoTotal)
            pedidos.agregar(ventas.idVenta, DateTime.Now.ToString("yyyy/MM/dd"), TimeOfDay.ToString("HH:mm:ss"), "", pedidos.obtenFolio, CInt(Estados.Pendiente), GlobalIdUsuario, 1)
            pedidos.buscar(pedidos.ultimoId)
            lblMesa.Text = "DE LLEVAR"
            muestraDellevar()
            btnCE.Enabled = True

        End If
    End Sub

    Private Sub escribeCaja(ByVal caja As TextBox)
        cajaActiva = caja
    End Sub

    Private Sub btn0_Click(sender As Object, e As EventArgs) Handles btn0.Click
        cajaActiva.Text += "0"
    End Sub

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        cajaActiva.Text += "1"
    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
        cajaActiva.Text += "2"
    End Sub

    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        cajaActiva.Text += "3"
    End Sub

    Private Sub btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click
        cajaActiva.Text += "4"
    End Sub

    Private Sub btn5_Click(sender As Object, e As EventArgs) Handles btn5.Click
        cajaActiva.Text += "5"
    End Sub

    Private Sub btn6_Click(sender As Object, e As EventArgs) Handles btn6.Click
        cajaActiva.Text += "6"
    End Sub

    Private Sub btn7_Click(sender As Object, e As EventArgs) Handles btn7.Click
        cajaActiva.Text += "7"
    End Sub

    Private Sub btn8_Click(sender As Object, e As EventArgs) Handles btn8.Click
        cajaActiva.Text += "8"
    End Sub

    Private Sub btn9_Click(sender As Object, e As EventArgs) Handles btn9.Click
        cajaActiva.Text += "9"
    End Sub

    Private Sub frmRestaurantePuntoVenta_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        GlobalEstadoRestaurante = "Cerrado"
    End Sub

    Private Sub btnCE_Click(sender As Object, e As EventArgs) Handles btnCE.Click
        imprimirComanda()
    End Sub

    Private Sub btnPedidos_Click(sender As Object, e As EventArgs) Handles btnPedidos.Click
        esPedido = True
        crearVenta()
        pedidos.agregar(idVenta, Date.Now.ToString("yyyy/MM/dd"), TimeOfDay.ToString("HH:mm:ss"), "", pedidos.obtenFolio, Estados.Pendiente, GlobalIdUsuario, 0)
        pedidos.buscar(pedidos.ultimoId)
        Dim f As New frmRestauranteBuscador()
        'f.MdiParent = Me
        f.ShowDialog()
        If Not f.clave Is Nothing Then
            claves = f.clave
            For Each s As String In claves
                inv.BuscaArticulo(s, 0)
                agregarPlatillo()
            Next
            imprimirComanda()
            nuevosPlatillos = False
            total = ventas.DaTotal(idVenta)
            lblTotal.Text = Format(total, opciones._formatoTotal)
            lblMesa.Text = "PEDIDO"
            btnCE.Enabled = True
        End If

    End Sub

    Private Sub imprimirTicket()
        imprimir()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        If ventas.buscarFolio(CInt(txtClave.Text), GlobalIdSucursalDefault) Then
            Me.idVenta = ventas.idVenta
        Else
            MsgBox("El folio es incorrecto.")
        End If
    End Sub

    Private Sub muestraMetodos()
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim lista As List(Of Integer) = metodos.listaFormas
        For Each s As Integer In lista
            metodos.ID = s
            metodos.LlenaDatos()
            Dim b As New Button
            b.Name = metodos.ID.ToString
            b.Text = metodos.Nombre
            b.Width = panelMetodos.Width
            b.Height = 60
            b.BackColor = Color.LightGray
            AddHandler b.Click, AddressOf btnClasificacion
            b.Location = New Point(x, y)
            y += 60
            panelMetodos.Controls.Add(b)
        Next
    End Sub

    Private Sub btnClasificacion(sender As Object, e As EventArgs)
        Dim b As Button = DirectCast(sender, Button)
        For Each b1 As Button In panelMetodos.Controls
            b1.BackColor = Color.LightGray
        Next
        b.BackColor = Color.Green
        idForma = CInt(b.Name)
        txtRecibido.Focus()
    End Sub

    Private Sub txtRecibido_Enter(sender As Object, e As EventArgs) Handles txtRecibido.Enter

    End Sub

    Private Sub txtRecibido_Leave(sender As Object, e As EventArgs) Handles txtRecibido.Leave
        txtRecibido.Text = txtRecibido.Text.Substring(0, txtRecibido.Text.Length)
        If activarTeclado Then
            teclado.Dispose()
        End If
    End Sub

    Private Function calculaCambio(ByVal ptotal As Double, ByVal recibido As Double) As Boolean
        lblNotificacion.Visible = False
        If idForma <= 0 Then
            MsgBox("Debe indicar una forma de pago.")
            Return False
        End If
        If total = recibido Then
            cambio = 0
            lblNotificacion.Visible = False
            Label8.Text = "0.0"
            ventasPagos.agregar(idVenta, idForma, recibido)
            Try
                muestraPagos()
            Catch ex As Exception

            End Try
            Return True
        ElseIf total > recibido Then
            'cambio = total - recibido
            lblNotificacion.Visible = True
            lblNotificacion.ForeColor = Color.Red
            Label1.Text = "Restante"
            total = ptotal - recibido
            'lblTotal.Text = Format(total, total)
            lblTotal.TextAlign = ContentAlignment.MiddleRight
            lblNotificacion.Text = "La cantidad recibida no cubre el total"
            txtRecibido.Text = ""
            Label8.Text = ""
            ventasPagos.agregar(idVenta, idForma, recibido)

            Try
                muestraPagos()
            Catch ex As Exception

            End Try
            idForma = -1
            Return False
        Else
            cambio = recibido - total
            Label8.Text = cambio.ToString("###,###,##0.00")
            ventasPagos.agregar(idVenta, idForma, recibido)
            Try
                muestraPagos()
            Catch ex As Exception

            End Try
            Return True
        End If

    End Function

    Private Sub muestraPagos()
        dgvMetodos.DataSource = ventasPagos.buscarPorVenta(idVenta)
    End Sub


    Private Sub txtRecibido_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRecibido.KeyPress
        If e.KeyChar = vbCrLf Then
            e.Handled = True
        End If
        NumConFrac(txtRecibido, e)
    End Sub

    Public Sub NumConFrac(ByVal CajaTexto As Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False

        ElseIf e.KeyChar = "." And Not CajaTexto.Text.IndexOf(".") Then
            e.Handled = True
        ElseIf e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtRecibido_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRecibido.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtRecibido.Text = "" Then
                recibido = 0
                txtRecibido.Text = txtRecibido.Text.Replace(vbCrLf, "")
            Else
                recibido = CDbl(txtRecibido.Text)
                txtRecibido.Text = txtRecibido.Text.Replace(vbCrLf, "")
            End If
        End If
    End Sub

    Private Sub limpiaDatos()
        txtClave.Text = ""
        txtRecibido.Text = ""
        Label8.Text = ""
        lblTotal.Text = "$0.00"
        For Each b As Button In panelMetodos.Controls
            b.BackColor = Color.LightGray
        Next
        dgvMetodos.DataSource = Nothing
        'dgvPlatillos.DataSource = Nothing
    End Sub

    Private Sub configuraTablaMetodos()
        tablaMetodos.Columns.Add("Metodo")
        tablaMetodos.Columns.Add("Cantidad")
    End Sub

    Private Function imprimir() As Boolean
        If ventas.listaDetalles(ventas.idVenta, CInt(estadosPlatillos.sinEnviar)).Count > 0 Then
            nuevosPlatillos = True
        End If
        Dim suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)

        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        PrintDialog1.PrinterSettings.Copies = 1
        Dim RutaPDF As String
        Dim Archivos As New dbSucursalesArchivos
        RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, False)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(ventas.fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(ventas.fecha), "yyyy") + "\" + Format(CDate(ventas.fecha), "MM") + "\")
        RutaPDF = RutaPDF + "\" + Format(CDate(ventas.fecha), "yyyy") + "\" + Format(CDate(ventas.fecha), "MM")
        'Dim SA As New dbSucursalesArchivos
        Dim TipoImpresora As String
        Impresora = Archivos.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.Venta)
        TipoImpresora = SA.TipoImpresora
        PrintDocument1.PrinterSettings.PrinterName = Impresora
        PrintDocument1.DocumentName = " Venta" + ventas.folio
        If Impresora = "Bullzip PDF Printer" Then
            Dim obj As New Bullzip.PdfWriter.PdfSettings
            obj.Init()
            obj.PrinterName = Impresora
            obj.WriteSettings()

            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            obj.SetValue("ShowSettings", "never")
            obj.SetValue("ShowPDF", "yes")
            obj.SetValue("ShowSaveAS", "nofile")
            obj.SetValue("ConfirmOverwrite", "no")
            obj.SetValue("Target", "printer")
            obj.WriteSettings()
        End If
        If nuevosPlatillos Then
            LlenaNodosImpresionComanda()
        Else
            LlenaNodosImpresion()
        End If
        If nuevosPlatillos Then
            If TipoImpresora = 0 Then
                LlenaNodos(suc.ID, TiposDocumentos.RestauranteComanda)
            Else
                LlenaNodos(suc.ID, TiposDocumentos.RestauranteComanda + 1000)
            End If
        Else
            If TipoImpresora = 0 Then
                LlenaNodos(suc.ID, TiposDocumentos.RestauranteTicket)
            Else
                LlenaNodos(suc.ID, TiposDocumentos.RestauranteTicket + 1000)
            End If
        End If
        PrintDocument1.Print()
        comandaEnviada = True
        Return True
    End Function

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        DibujaPaginaN(e.Graphics)
        If MasPaginas = True Or NumeroPagina > 2 Then
            'e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00") + "/" + Format(CuantaY, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
            e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If


        e.HasMorePages = MasPaginas
    End Sub

    Private Sub LlenaNodos(ByVal pidSucursal As Integer, ByVal pDocumento As Integer)
        Dim I As New dbImpresionesN(MySqlcon)
        Dim Fs As FontStyle
        ImpNDi.Clear()
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader
        Try
            dr = I.DaNodos(pDocumento, pidSucursal)
            While dr.Read
                Select Case dr("fuentestilo")
                    Case 1
                        Fs = FontStyle.Bold
                    Case 2
                        Fs = FontStyle.Italic
                    Case 0
                        Fs = FontStyle.Regular
                    Case 8
                        Fs = FontStyle.Strikeout
                    Case 4
                        Fs = FontStyle.Underline
                End Select
                ImpNDi.Add(New NodoImpresionN(dr("id"), dr("x"), dr("y"), dr("xl"), dr("yl"), dr("texto"), dr("datapropertyname"), New Font(dr("fuente").ToString, CSng(dr("fuentesize")), Fs, GraphicsUnit.Point), dr("alineacion"), dr("tipo"), dr("tipodato"), dr("visible"), dr("documento"), dr("tiponodo"), dr("idsucursal"), dr("conetiqueta"), dr("nombre"), dr("renglon"), dr("clasificacion")))
            End While
            dr.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If deLlevar And comandaEnviada = False Or esPedido Then
            If TipoImpresora = 0 Then
                ImpDb.DaZonaDetalles(TiposDocumentos.RestauranteComanda, GlobalIdSucursalDefault)
            Else
                ImpDb.DaZonaDetalles(TiposDocumentos.RestauranteComanda + 1000, GlobalIdSucursalDefault)
            End If
        Else
            If TipoImpresora = 0 Then
                ImpDb.DaZonaDetalles(TiposDocumentos.RestauranteTicket, GlobalIdSucursalDefault)
            Else
                ImpDb.DaZonaDetalles(TiposDocumentos.RestauranteTicket + 1000, GlobalIdSucursalDefault)
            End If
        End If

        If TipoImpresora = 0 Then
            DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujo(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub

    Private Sub DibujaPaginaEstatico(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)

        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos

        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        Dim codigos As New Collection

        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            If nuevosPlatillos Then
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteComanda, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteComanda + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            Else
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteTicket, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteTicket + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            End If


        Catch ex As Exception

        End Try
        Dim Rec As RectangleF
        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        'Nodos Detalles            

        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim LimY As Integer
        LimY = (pZonaY + pZonaYL) / 40 * 10
        C = Posicion
        YCoord = 0
        'Calcula páginas
        CuantaY = 0
        For Each n As NodoImpresionN In ImpNDD
            If n.DataPropertyName.Contains("descripcion") Then
                CuantaY = CuantaY + 4 + InsertaEnters(n.Valor, pZonaRG, YCoord, pZonaAlt)
            End If
        Next
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 Then
                MasPaginas = True
                Exit While
            End If
            'For Each n As NodoImpresionN In ImpNDi
            '    Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
            '    If n.DataPropertyName.Contains("descripcion") And n.Renglon = 1 Then

            '    End If
            'Next
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then
                                        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        'YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra
            '*************Segundoo Renglon***************
            YExtra = 0
            YExtra2 = 0
            Dim HayRenglon2 As Boolean = False
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    HayRenglon2 = True
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then
                                        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        'YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            If HayRenglon2 Then YCoord = YCoord + 4 + YExtra
            '**************************************
            Posicion += 1
            C += 1
        End While


        'Nodos fijos
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "boleta" Then
                    ncb = n
                    codigos.Add(ncb)
                End If

                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "boleta" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 *, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            End Select
                        End If
                    End If
                End If
            Next

            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName.Contains("cancelado") Then
                    e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
                End If
            Next

            'e.DrawString(ImpND("cancelado").Valor, ImpNDi("cancelado").Fuente, Brushes.Red, ImpNDi("cancelado").X / 40 * 10, ImpNDi("cancelado").Y / 40 * 10)
            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

                YCoord = 0
                'Ivas Sin Retenciones
                C = 0
                If niva2.Visible = 1 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                        If niva2.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

            End If
            For Each n As NodoImpresionN In codigos

                If n.Visible = 1 Then e.DrawImage(CodigoBidimensional, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
            Next
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            'comandaEnviada = False
        End Try
    End Sub
    Private Sub DibujaPaginaFlujo(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos
        'Dim ImpDb As New dbImpresionesN(MySqlcon)
        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            If nuevosPlatillos Then
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteComanda, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteComanda + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            Else
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteTicket, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteTicket + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            End If

        Catch ex As Exception

        End Try
        Dim Rec As RectangleF


        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        'Nodos fijos principio
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "boleta" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "boleta" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 And niva.Tipo = 0 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

                YCoord = 0
                'Ivas Sin Retenciones
                C = 0
                If niva2.Visible = 1 And niva2.Tipo = 0 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                        If niva2.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

            End If
            If ncb.Visible = 1 And ncb.Tipo = 0 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

        For Each n As NodoImpresionN In ImpNDi
            If n.DataPropertyName.Contains("cancelado") Then
                e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
            End If
        Next

        'Nodos Detalles            
        XCoord = 0
        YCoord = 0
        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim LimY As Integer
        LimY = (pZonaY + pZonaYL) / 40 * 10
        C = Posicion
        YCoord = 0
        'Calcula páginas
        CuantaY = 0
        For Each n As NodoImpresionN In ImpNDD
            If n.DataPropertyName.Contains("descripcion") Then
                CuantaY = CuantaY + 4 + InsertaEnters(n.Valor, pZonaRG, YCoord, pZonaAlt)
            End If
        Next
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 And (pModo = 0 Or pModo = 2) Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra

            '***************************segundo
            Dim Haysegundo As Boolean = False
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 And (pModo = 0 Or pModo = 2) Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    Haysegundo = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            If Haysegundo Then YCoord = YCoord + 4 + YExtra


            '*********************************

            Posicion += 1
            C += 1
        End While


        'Nodos fijos final
        If MasPaginas = True And (pModo = 2 Or pModo = 3) Then
            NumeroPagina += 1
            Exit Sub
        End If
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "boleta" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "boleta" Then
                    If n.Visible = 1 And n.Tipo = 2 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(n.XL / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS
            Dim Ycoord2 As Integer
            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                Ycoord2 = 0
                If niva.Visible = 1 And niva.Tipo = 2 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                        If niva.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, Ycoord2)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        End If
                        'End If
                        'Next
                        Ycoord2 += 4
                        C += 1
                    End While
                End If

                Ycoord2 = 0
                'Ivas Sin Retenciones
                C = 0
                If niva2.Visible = 1 And niva2.Tipo = 2 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva2.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                        If niva2.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, Ycoord2)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                        End If
                        'End If
                        'Next
                        Ycoord2 += 4
                        C += 1
                    End While
                End If

            End If

            If ncb.Visible = 1 And ncb.Tipo = 2 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(Math.Abs((ncb.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            'comandaEnviada = False
        End Try
    End Sub

    Private Sub LlenaNodosImpresion()
        Dim O As New dbOpciones(MySqlcon)
        Dim iva As Double = 0
        Dim total As Double = 0
        Dim subtotal As Double = 0
        Dim lista As List(Of Integer)
        If reImprimir Then
            lista = ventas.listaDetalles(ventas.idVenta, CInt(estadosPlatillos.pendiente).ToString, CInt(estadosPlatillos.pagado).ToString)
        Else
            lista = ventas.listaDetalles(ventas.idVenta, CInt(estadosPlatillos.pendiente).ToString, CInt(estadosPlatillos.pendiente).ToString)
        End If
        Dim come As List(Of Integer) = comensales.listaComensales(idMesa)
        ImpND.Clear()
        ImpNDD.Clear()
        CuantosRenglones = 0
        Posicion = 0
        NumeroPagina = 1
        Dim cont As Integer = 0
        For Each x As Integer In lista
            detalles.buscar(x)
            ImpNDD.Add(New NodoImpresionN("", "comensal", "", 0), "comensal" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "producto", detalles.descripcion, 0), "producto" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "precio", detalles.precio.ToString, 0), "precio" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "cantidad", detalles.cantidad.ToString, 0), "cantidad" + Format(cont, "000"))
            cont += 1
            CuantosRenglones += 1
            total += detalles.precio
            iva += detalles.iva
            subtotal += detalles.precio
        Next
        ImpND.Add(New NodoImpresionN("", "empresa", GlobalNombreEmpresa, 0), "empresa")
        Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        ImpND.Add(New NodoImpresionN("", "sucursal", s.Nombre, 0), "sucursal")
        ImpND.Add(New NodoImpresionN("", "direccion", s.Direccion, 0), "direccion")
        ImpND.Add(New NodoImpresionN("", "rfc", s.RFC, 0), "rfc")
        Dim v As New dbVendedores(ventas.idMesero, MySqlcon)
        ImpND.Add(New NodoImpresionN("", "mesero", v.Nombre, 0), "mesero")
        If idMesa > 0 Then
            Dim m As New dbRestauranteMesas(MySqlcon, GlobalIdSucursalDefault)
            Dim m2 As RestauranteMesa = m.buscar(Me.idMesa)
            ImpND.Add(New NodoImpresionN("", "mesa", m2.numero.ToString, 0), "mesa")
        Else
            ImpND.Add(New NodoImpresionN("", "mesa", "Caja", 0), "mesa")
        End If
        ImpND.Add(New NodoImpresionN("", "fecha", ventas.fecha, 0), "fecha")
        ImpND.Add(New NodoImpresionN("", "subtotal", Format(subtotal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal, " "), 0), "subtotal")
        ImpND.Add(New NodoImpresionN("", "total", Format(total, O._formatoTotal).PadLeft(O.Espaciototal, " "), 0), "total")
        ImpND.Add(New NodoImpresionN("", "recibido", Format(recibido, O._formatoTotal).PadLeft(O.Espaciototal, " "), 0), "recibido")
        ImpND.Add(New NodoImpresionN("", "cambio", Format(cambio, O._formatoTotal).PadLeft(O.Espaciototal, " "), 0), "cambio")
        ImpND.Add(New NodoImpresionN("", "hora", TimeOfDay.ToString("HH:mm:ss"), 0), "hora")
        ImpND.Add(New NodoImpresionN("", "iva", iva, 0), "iva")
        ImpND.Add(New NodoImpresionN("", "folio", ventas.folio.ToString, 0), "folio")
        ImpND.Add(New NodoImpresionN("", "descuento", "0", 0), "descuento")
        'ImpND.Add(New NodoImpresionN("", "recibido", "0", 0), "descuento")
        If pedidos.idPedido > 0 Then
            If deLlevar Then
                ImpND.Add(New NodoImpresionN("", "texto", "", 0), "texto")
            Else
                ImpND.Add(New NodoImpresionN("", "texto", clientes.Direccion, 0), "texto")
            End If
        Else
            ImpND.Add(New NodoImpresionN("", "texto", "", 0), "texto")
        End If
    End Sub

    Private Function InsertaEnters(ByVal Cadena As String, ByVal CadaCuantos As Integer, ByVal Y As Integer, ByVal AumentoY As Integer) As Integer
        Dim C As Integer
        C = 0
        Dim CC As Integer = 0
        Dim car As String
        Dim Yx As Integer = 0
        While C < Cadena.Length
            car = Cadena.Substring(C, 1)
            If car = Chr(13) Or CC = CadaCuantos Or car = Chr(10) Then
                If car = Chr(13) Then C += 1
                Yx += AumentoY
                CC = 0
            Else
                CC += 1
            End If
            C += 1
        End While
        Return Yx
    End Function

    Private Sub frmRestaurantePuntoVenta_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        panelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        ventas.buscar(idUltimaVenta)
        reImprimir = True
        imprimir()
        ventas.buscar(idVenta)
        reImprimir = False
    End Sub

    Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        If cajaActiva.Text.Length > 0 Then
            cajaActiva.Text = cajaActiva.Text.Substring(0, cajaActiva.Text.Length)
        End If
    End Sub

    Private Sub btnCajon_Click(sender As Object, e As EventArgs) Handles btnCajon.Click
        Dim f As New frmRestauranteBuscaVenta(frmRestauranteBuscaVenta.tipoBusqueda.ventas)
        f.ShowDialog()
        If f.idventa > 0 Then
            Me.idVenta = f.idventa
            ventas.buscar(idVenta)
            lblTotal.Text = Format(ventas.total, "$#,###,##0.00######")
            lblTotal.TextAlign = ContentAlignment.MiddleRight
            lblCajero.Text = GlobalUsuario
            mesero = New dbVendedores(ventas.idMesero, MySqlcon)
            lblMesero.Text = mesero.Nombre
            lblMesa.Text = ""
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim f As New frmRestauranteBuscaVenta(frmRestauranteBuscaVenta.tipoBusqueda.pedidos)
        f.ShowDialog()
        If f.idPedido > 0 Then
            pedidos.buscar(f.idPedido)
            ventas.buscar(pedidos.idVenta)
            lblTotal.Text = Format(ventas.total, "$#,###,##0.00######")
            lblTotal.TextAlign = ContentAlignment.MiddleRight
            lblCajero.Text = GlobalUsuario
            mesero = New dbVendedores(ventas.idMesero, MySqlcon)
            lblMesero.Text = mesero.Nombre
            lblMesa.Text = ""
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim f As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        f.ShowDialog()
        If Not f.Cliente Is Nothing Then
            clientes = f.Cliente
            If descuentos.buscarCliente(clientes.ID) Then
                lblDescuento.Text = descuentos.descuento + "%"
                desc = descuentos.descuento
                total = ventas.DaTotal(idVenta)
            End If
        End If
    End Sub

    Private Sub LlenaNodosImpresionComanda()
        Dim O As New dbOpciones(MySqlcon)
        Dim iva As Double = 0
        Dim total As Double = 0
        Dim subtotal As Double = 0
        Dim lista As List(Of Integer)
        lista = ventas.listaDetalles(ventas.idVenta, CInt(estadosPlatillos.sinEnviar))
        ImpND.Clear()
        ImpNDD.Clear()
        CuantosRenglones = 0
        Posicion = 0
        NumeroPagina = 1
        Dim cont As Integer = 0
        Dim cont1 As Integer = 0
        ' lista = platillosComensal.listaDetalles(i, CInt(estadosPlatillos.sinEnviar))
        For Each x As Integer In lista
            detalles.buscar(x)
            ImpNDD.Add(New NodoImpresionN("", "comensal", "", 0), "comensal" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "producto", detalles.descripcion, 0), "producto" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "cantidad", detalles.cantidad.ToString, 0), "cantidad" + Format(cont, "000"))
            detalles.cambiarEstado(x, CInt(estadosPlatillos.pendiente))
            cont += 1
            CuantosRenglones += 1
            total += detalles.precio
            iva += detalles.iva
            subtotal = detalles.precio
        Next

        ImpND.Add(New NodoImpresionN("", "mesero", GlobalUsuario, 0), "mesero")
        Dim m As New dbRestauranteMesas(MySqlcon, GlobalIdSucursalDefault)
        'Dim m2 As RestauranteMesa = m.buscar(Me.idMesa)
        ImpND.Add(New NodoImpresionN("", "mesa", "CAJA", 0), "mesa")
        ImpND.Add(New NodoImpresionN("", "hora", TimeOfDay.ToString("HH:mm:ss"), 0), "hora")

        'comandaEnviada = True
    End Sub

    Private Sub imprimirComanda()
        imprimir()
    End Sub

    Private Sub muestraDellevar()
        btnVer.Visible = False
        panelPedidos.Controls.Clear()
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim ancho = (panelPedidos.Width / 10) - 4
        Dim alto = panelPedidos.Height / 2
        Dim l As New List(Of Integer)
        l = pedidos.listaPedidos(CInt(Estados.Pendiente), 1)
        Dim totalPedidos = l.Count
        For Each i As Integer In l
            If x >= panelPedidos.Width - 4 Then
                x = 0
                y += alto
            End If
            Dim b As New Button
            b.Name = i
            b.Text = "Orden " + i.ToString
            b.Width = ancho
            b.Height = alto
            b.BackColor = Color.Red
            AddHandler b.Click, AddressOf clickPedido
            b.Location = New Point(x, y)
            panelPedidos.Controls.Add(b)
            x += ancho
        Next
    End Sub

    Private Sub clickPedido(sender As Object, e As EventArgs)
        deLlevar = True
        Dim b As Button = DirectCast(sender, Button)
        Dim id As Integer = CInt(b.Name)
        pedidos.buscar(id)
        ventas.buscar(pedidos.idVenta)
        Me.idVenta = pedidos.idVenta
        total = ventas.DaTotal(idVenta)
        lblTotal.Text = Format(total, opciones._formatoTotal)
        btnVer.Enabled = True
    End Sub

    Private Sub btnVer_Click(sender As Object, e As EventArgs) Handles btnVer.Click
        Dim f As New frmRestauranteBuscaVenta(frmRestauranteBuscaVenta.tipoBusqueda.muestraVenta, pedidos.idPedido)
        f.ShowDialog()
    End Sub

   
    Private Sub btnClientes_Click(sender As Object, e As EventArgs) Handles btnClientes.Click

    End Sub

    Private Sub txtRecibido_TextChanged(sender As Object, e As EventArgs) Handles txtRecibido.TextChanged

    End Sub
End Class