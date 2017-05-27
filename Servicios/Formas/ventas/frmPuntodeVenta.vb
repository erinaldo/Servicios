Public Class frmPuntodeVenta
    Dim Tipo As Byte
    Dim Remision As dbVentasRemisiones
    Dim Cotizacion As dbVentasCotizaciones
    Dim Pedido As dbVentasPedidos
    Dim RemisionDetalles As dbVentasRemisionesInventario
    Dim CotizacionDetalles As dbVentasCotizacionesInventario
    Dim PedidoDetalles As dbVentasPedidosInventario
    Dim Articulo As dbInventario
    Dim CajaG As dbCajas
    Dim IdVendedor As Integer
    Dim Estado As Byte
    Dim EstadoVentana As Byte
    Dim ConsultaOn As Boolean = True
    Dim idCliente As Integer
    Dim IdClienteDefault As Integer
    Dim idSucursal As Integer
    Dim idAlmacen As Integer
    Dim idPrecioLista As Integer
    Dim idPrecioListaDefault As Integer
    Dim Tabla As New DataTable
    Dim TotalVenta As Double
    Dim Recibido As Double
    Dim Cambio As Double
    Dim DocaImprimir As Byte
    'Dim ImpND As New Collection
    'Dim ImpNDD As New Collection
    'Dim ImpNDDi As New Collection
    'Dim ImpNDi As New Collection
    'Dim ImpNDi2 As New Collection
    'Dim Posicion As Integer
    'Dim CuantosRenglones As Integer
    Dim IdCaja As Integer
    'Dim CadenaCFDI As String
    'Dim CodigoBidimensional As Bitmap
    'Dim MasPaginas As Boolean
    'Dim NumeroPagina As Integer
    'Dim CuantaY As Integer
    'Dim TipoImpresora As Byte
    Dim Idconcepto As Integer
    Dim PidePrecio As Boolean = True
    Dim O As dbOpciones
    Dim ColorAmarillo As Color = Color.FromArgb(255, 255, 182)
    Dim ColorAzul As Color = Color.FromArgb(192, 192, 255)
    Dim ColorRojo As Color = Color.FromArgb(255, 192, 192)
    Dim ColorAmarillo2 As Color = Color.FromArgb(252, 220, 109)
    Dim Veces As Integer = 0
    Dim Secuencia As String
    Dim Lectura As String = ""
    Dim IdConceptoDefault As Integer
    Dim Precio As Double
    Dim HaciendoCambio As Boolean = False
    Dim TipoRedondeo As Byte
    Dim CantidadDecimales As Byte
    'Dim IdDocumentoRef As Integer
    Dim IdsDocumentos As New elemento
    'Dim ReferenciaAgregada As Boolean = False
    Dim TiposReferencias As New elemento
    Dim EliminarREf As Boolean = False
    Dim Descuento As Double
    Dim PrecioParaDescuento As Double
    Dim RutaImagen As String
    Dim AlmacenNombre As String = ""
    Dim MetodosdePago As dbVentasAddMetodos
    'Dim UltimoId As Integer
    Private Enum EstadosVentana
        Inicio = 0
        ArticuloAgregado = 1
        RecibiendoPago = 2
    End Enum
    Dim Opc As dbOpcionesOc
    'VARIABLES DESCUENTOS
    Dim P As New dbDescuentos(MySqlcon)
    Dim CD As New dbVentasRemisionesInventario(MySqlcon)
    Dim IP As New dbInventarioPrecios(MySqlcon)
    Dim idProducto As Integer
    Dim canProducto As Double
    Dim precioArticulo As Double
    Dim tieneDesc As Boolean = False
    Dim tipoElimianr As String = ""
    Dim auxIDRenglon As Integer
    Dim tipoDescuento As String = ""
    Dim promocion1 As String = ""
    Dim promocion2 As String = ""
    Dim CantidadArtDesc As Double = 0
    Dim AuxpIdMoneda As Double
    Dim AuxpIdAlmacen As Double
    Dim AuxpIva As Double
    Dim AuxpIdVariante As Double
    Dim AuxpIdServicio As Double
    Dim nombreProducto As String
    'END VARIABLES DESCUENTOS
    Dim ImpDoc As ImprimirDocumento
    Private Sub frmPuntodeVenta_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("¿Cerrar punto de venta?.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            GlobalEstadoPuntodeVenta = "Cerrado"
            P.limpiarVentasdesc()
            P.limpiarDescPromociones()
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub CargaImagenes()
        On Error Resume Next
        Button3.BackgroundImageLayout = ImageLayout.Stretch
        Button3.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\agregar.png")
        Button4.BackgroundImageLayout = ImageLayout.Stretch
        Button4.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\restar.png")
        Button5.BackgroundImageLayout = ImageLayout.Stretch
        Button5.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\igual.png")
        Button10.BackgroundImageLayout = ImageLayout.Stretch
        Button10.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\precio.png")
        Button6.BackgroundImageLayout = ImageLayout.Stretch
        Button6.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\porciento.png")
        Button11.BackgroundImageLayout = ImageLayout.Stretch
        Button11.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\bascula.png")
        Button9.BackgroundImageLayout = ImageLayout.Stretch
        Button9.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\imprimir.png")
        Button8.BackgroundImageLayout = ImageLayout.Stretch
        Button8.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\rehacer.png")
        Button7.BackgroundImageLayout = ImageLayout.Stretch
        Button7.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\cancelar.png")
        Button12.BackgroundImageLayout = ImageLayout.Stretch
        Button12.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\apartados.png")
        Button13.BackgroundImageLayout = ImageLayout.Stretch
        Button13.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\lotes.png")
        Button14.BackgroundImageLayout = ImageLayout.Stretch
        Button14.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\aduana.png")
        Button15.BackgroundImageLayout = ImageLayout.Stretch
        Button15.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\comentario.png")
    End Sub
    Private Sub frmPuntodeVenta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            TextBox1.Enabled = False
            Button1.Enabled = False
        End If

        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        ImpDoc = New ImprimirDocumento()
        MetodosdePago = New dbVentasAddMetodos(MySqlcon)
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasApartadosVer, PermisosN.Secciones.Ventas) = False Then Button12.Visible = False
        If IdCaja <= 0 Then
            MsgBox("Debe seleccionar una caja.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
        If IdVendedor <= 0 Then
            MsgBox("Debe seleccionar un vendedor.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
        Tabla.Columns.Add("Id", I.GetType)
        Tabla.Columns.Add("TipoR", S.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        'Tabla.Columns.Add("Uni.", S.GetType)
        'Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        Tabla.Columns.Add("Precio U.", S.GetType)
        Tabla.Columns.Add("Importe", S.GetType)
        'Tabla.Columns.Add("Moneda", S.GetType)
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        O = New dbOpciones(MySqlcon)
        Opc = New dbOpcionesOc(MySqlcon)
        Opc.LlenaDatos(Tipo, idSucursal)
        TipoRedondeo = O.TipoRedondeo
        CantidadDecimales = O.DecimalesRedondeo
        Me.BackColor = Color.FromArgb(O.PVRojo, O.PVVerde, O.PVAzul)
        DGDetalles.BackgroundColor = Color.FromArgb(O.PVRojo, O.PVVerde, O.PVAzul)
        DGDetalles.DefaultCellStyle.BackColor = Color.FromArgb(O.PVRojo, O.PVVerde, O.PVAzul)
        If O.PedirPrecio = 1 Then
            PidePrecio = True
        Else
            PidePrecio = False
        End If
        If O.EliminarRefPV = 1 Then
            EliminarREf = True
        Else
            EliminarREf = False
        End If
        If O.PVVentabaCompleta = 1 Then
            PictureBox1.Visible = False
            Me.BackgroundImage = Image.FromFile(RutaImagen)
            Me.BackgroundImageLayout = ImageLayout.Stretch
        Else
            Try
                PictureBox1.BackgroundImage = Image.FromFile(RutaImagen)
            Catch ex As Exception

            End Try
        End If
        CargaImagenes()
        Select Case Tipo
            Case 0
                Me.Text = "Punto de Venta - Remisión"
                tipoDescuento = "VentasRemisiones"
                Button13.Visible = True
                Button14.Visible = True
            Case 1
                Me.Text = "Punto de Venta - Pedido"
                tipoDescuento = "Pedidos"
            Case 2
                Me.Text = "Punto de Venta - Cotización"
                tipoDescuento = "Cotizacion"
        End Select
        Try
            If SerialPort1.IsOpen Then SerialPort1.Close()
            SerialPort1.PortName = O.PuertoBascula
            SerialPort1.BaudRate = O.BasculaBaundRate
            SerialPort1.Parity = O.BasculaParity
            SerialPort1.DataBits = O.BasculaDataBits
            SerialPort1.Handshake = O.BasculaHandshake
            Secuencia = O.BasculaSecuencia
            SerialPort1.WriteTimeout = 1000
            SerialPort1.ReadTimeout = 1000
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
        End Try
        Dim su As New dbSucursales(idSucursal, MySqlcon)
        CajaG = New dbCajas(IdCaja, MySqlcon)
        Dim v As New dbVendedores(IdVendedor, MySqlcon)

        Dim cl As New dbClientes(idCliente, MySqlcon)
        idPrecioLista = cl.IdLista
        idPrecioListaDefault = idPrecioLista
        Label1.Text = "Vendedor: " + v.Nombre + " Caja: " + CajaG.Nombre
        Label9.Text = "Sucursal: " + su.Nombre + " Cliente: " + cl.Nombre
        Label3.Text = "Fecha: " + Format(Date.Now, "dd/MMM/yyyy").ToUpper
        'If cl.CreditoDias <> 0 Or cl.Credito <> 0 Then
        'Idconcepto = 3
        'Else
        Idconcepto = 1
        'End If
        IdConceptoDefault = Idconcepto
        CentraControles()
    End Sub
    Public Sub New(ByVal PTipo As Byte, ByVal pidVendedor As Integer, ByVal pidSucursal As Integer, ByVal pidAlmacen As Integer, ByVal pidCliente As Integer, ByVal pidCaja As Integer, ByVal pRutaImagen As String, pAlmacenNombre As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        '0 Remisión
        '1 Pedido
        '2 Cotización
        Tipo = PTipo
        IdVendedor = pidVendedor
        idSucursal = pidSucursal
        idAlmacen = pidAlmacen
        AlmacenNombre = pAlmacenNombre
        idCliente = pidCliente
        IdClienteDefault = pidCliente
        IdCaja = pidCaja
        Select Case Tipo
            Case 0
                Remision = New dbVentasRemisiones(MySqlcon)
                RemisionDetalles = New dbVentasRemisionesInventario(MySqlcon)
            Case 1
                Pedido = New dbVentasPedidos(MySqlcon)
                PedidoDetalles = New dbVentasPedidosInventario(MySqlcon)
            Case 2
                Cotizacion = New dbVentasCotizaciones(MySqlcon)
                CotizacionDetalles = New dbVentasCotizacionesInventario(MySqlcon)
        End Select
        Articulo = New dbInventario(MySqlcon)
        RutaImagen = pRutaImagen

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub Nuevo()
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        P.limpiarVentasdesc()
        P.limpiarDescPromociones()
        Estado = Estados.Inicio
        EstadoVentana = 0
        Idconcepto = IdConceptoDefault
        TextBox1.BackColor = Color.White
        Label4.BackColor = Color.Transparent
        Label2.Text = "Total: $0.00"
        Label14.Text = "Líneas: 0"
        TextBox1.Text = ""
        'IdDocumentoRef = 0
        If idCliente <> IdClienteDefault Then
            Dim Cl As New dbClientes(IdClienteDefault, MySqlcon)
            Dim su As New dbSucursales(idSucursal, MySqlcon)
            Label9.Text = "Sucursal: " + su.Nombre + " Cliente: " + Cl.Nombre
        End If
        idCliente = IdClienteDefault
        idPrecioLista = idPrecioListaDefault
        'ReferenciaAgregada = False
        IdsDocumentos.Limpiar()
        TiposReferencias.Limpiar()
        Label4.Text = "Producto:"
        DGDetalles.DataSource = Nothing
    End Sub
    Private Function BuscaArticulo() As Boolean
        Try
            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
                Articulo.ID = 0
                Return False
            End If
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox1.Text, 0, False, True, False, False) Then
                    Articulo = p
                    Return True
                Else
                    Articulo.ID = 0
                End If
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Return False
        End Try

    End Function
    Private Function Guardar() As Boolean
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Try
            
            Select Case Tipo
                Case 0
                    'Sf.BuscaFolios(idSucursal, dbSucursalesFolios.TipoDocumentos.Remision, 0)
                    If Opc.ActivarOc = 1 And ((Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc) Or (Format(Now, "HH:mm") >= Opc.HoraInicioOc2 And Format(Now, "HH:mm") <= Opc.HoraFinOc2)) Then
                        Remision.Serie = Opc.SerieOc
                        Remision.Folio = Remision.DaNuevoFolio(Opc.SerieOc, idSucursal)
                        If Remision.Folio < Opc.FolioOc Then Remision.Folio = Opc.FolioOc
                    Else
                        Remision.Folio = Remision.DaNuevoFolio(CajaG.Serie, idSucursal)
                        Remision.Serie = CajaG.Serie
                    End If
                    Remision.Comentario = ""
                    Remision.Guardar(idCliente, Format(Date.Now, "yyyy/MM/dd"), Remision.Folio, 0, 0, idSucursal, Remision.Serie, 1, 2, IdCaja)
                    If CajaG.Maximo > 0 Then
                        If CajaG.Maximo < CajaG.CuantoEnCaja(IdCaja) Then
                            MsgBox("Efectivo en caja exedió $" + CajaG.Maximo.ToString("#,###,##0.00") + " , favor de hacer un retiro.", MsgBoxStyle.Exclamation, GlobalNombreApp)
                        End If
                    End If
                Case 1
                    'Sf.BuscaFolios(idSucursal, dbSucursalesFolios.TipoDocumentos.VentasPedidos, 0)
                    Pedido.Folio = Pedido.DaNuevoFolio(CajaG.SeriePed, idSucursal)
                    Pedido.Serie = CajaG.SeriePed
                    Pedido.Comentario = ""
                    Pedido.Guardar(idCliente, Format(Date.Now, "yyyy/MM/dd"), Pedido.Folio, 0, 0, idSucursal, Pedido.Serie, IdCaja, IdVendedor)
                Case 2
                    'Sf.BuscaFolios(idSucursal, dbSucursalesFolios.TipoDocumentos.VentasCotizaciones, 0)
                    Cotizacion.Folio = Cotizacion.DaNuevoFolio(CajaG.SerieCot, idSucursal)
                    Cotizacion.Serie = CajaG.SerieCot
                    Cotizacion.Comentario = ""
                    Cotizacion.Guardar(idCliente, Format(Date.Now, "yyyy/MM/dd"), Cotizacion.Folio, 0, 0, idSucursal, Cotizacion.Serie, IdCaja, IdVendedor, 2)
            End Select
            Estado = Estados.SinGuardar
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Return False
        End Try
    End Function

    'Select Case Tipo
    '                            Case 0 'remision
    '                            Case 1 'pedido
    '                            Case 2 'cotizacion

    '                        End Select
    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then

            If EstadoVentana <> 3 And EstadoVentana <> 4 And EstadoVentana <> 5 And EstadoVentana <> 6 Then
                If BuscaArticulo() = True Then
                    If Articulo.ID <> 0 Then
                        Label4.Text = "Artículo: " + Articulo.Nombre
                        EstadoVentana = 1
                        PresionaEnter()
                    End If
                    TextBox1.BackColor = Color.White
                    Label4.BackColor = Color.Transparent
                Else
                    If EstadoVentana = 1 Then
                        EstadoVentana = 0
                        TextBox1.BackColor = Color.White
                        Label4.BackColor = Color.Transparent
                    End If
                    PresionaEnter()
                End If
            Else
                PresionaEnter()
            End If
        End If
        If e.KeyCode = Keys.Escape Then
            If EstadoVentana = 3 Or EstadoVentana = 6 Then
                EstadoVentana = 2
                TextBox1.BackColor = Color.White
                Label4.BackColor = Color.Transparent
                Label4.Text = "Producto"
                TextBox1.Text = ""
            End If
        End If
        If e.KeyCode = Keys.F2 Then
            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If EstadoVentana <> 5 Then
                buscaArticuloBoton()
            Else
                Dim Idinv As Integer = 0
                Select Case Tipo
                    Case 0
                        If RemisionDetalles.ID <> 0 Then Idinv = RemisionDetalles.Idinventario
                    Case 1
                        If PedidoDetalles.ID <> 0 Then Idinv = PedidoDetalles.Idinventario
                    Case 2
                        If CotizacionDetalles.ID <> 0 Then Idinv = CotizacionDetalles.Idinventario
                End Select
                Dim SP As New frmSelectorPrecios(Idinv)
                If SP.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    TextBox1.Text = SP.Precio.ToString
                    TextBox1.SelectAll()
                End If
                SP.Dispose()
            End If
        End If

        If e.KeyCode = Keys.F5 Then
            If MsgBox("¿Nueva venta? Se perderán los datos no guardados.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Nuevo()
            End If
        End If
        If e.KeyCode = Keys.F4 Then
            If EstadoVentana = 2 Or EstadoVentana = 0 Then
                EstadoVentana = 6
                TextBox1.BackColor = ColorRojo
                Label4.BackColor = ColorRojo
                Label4.Text = "Folio a cancelar:"
            End If
        End If
        If e.KeyCode = Keys.F6 Then
            EstadoVentana = 7
            Try
                If SerialPort1.IsOpen = False Then SerialPort1.Open()
                SerialPort1.Write(Chr(CInt(Secuencia)))
                Lectura = ""
                Veces = 0
                HaciendoCambio = True
                TextBox1.Enabled = False
                Timer1.Enabled = True
            Catch ex As Exception
                MsgBox(ex.Message)
                If SerialPort1.IsOpen Then SerialPort1.Close()
                EstadoVentana = 2
                TextBox1.BackColor = Color.White
                Label4.BackColor = Color.Transparent
            End Try
        End If
        If e.KeyCode = Keys.F1 Then
            Dim fa As New frmPuntodeVentaAyuda
            fa.ShowDialog()
            fa.Dispose()
        End If

        If e.KeyCode = Keys.F3 Then
            CambiodePrecio()
        End If
        If e.KeyCode = Keys.F7 Then
            HaceDescuento()
        End If
        If e.KeyCode = Keys.F8 Then
            BuscaPendiente()
        End If
        If e.KeyCode = Keys.F9 Then
            If Tipo <> 2 Then CerrarVentaPendiente()
        End If
        If e.KeyCode = Keys.F11 Then

            Dim F As New frmPuntodeVentaCajaEstado(IdCaja)
            F.ShowDialog()
            F.Dispose()
        End If
        If e.KeyCode = Keys.F10 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosVer, PermisosN.Secciones.PuntodeVenta) = True Then
                Dim f As New frmCajasMovimientos(0, IdCaja, 0, 0)
                f.ShowDialog()
                f.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F12 Then
            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.ReportesVer, PermisosN.Secciones.PuntodeVenta) = True Then
                Dim fimp As New frmPuntodeVentaReportes
                fimp.ShowDialog()
                fimp.Dispose()
            End If
        End If
    End Sub
    Private Sub BuscaPendiente()
        Dim iTipo As Byte
        If Tipo = 0 Then iTipo = 2
        If Tipo = 1 Then iTipo = 1
        If Tipo = 2 Then Exit Sub
        Dim Fb As New frmPuntodeVentaBuscaDocumento(0, False, iTipo, idSucursal, 0, False, IdVendedor)
        If Fb.ShowDialog = Windows.Forms.DialogResult.OK Then
            Select Case Tipo
                Case 1
                    Pedido.ID = Fb.id(0)
                    Pedido.LlenaDatos()
                    If idCliente <> Pedido.IdCliente Then
                        idCliente = Pedido.IdCliente
                        Dim Cl As New dbClientes(idCliente, MySqlcon)
                        idPrecioLista = Cl.IdLista
                        Dim su As New dbSucursales(idSucursal, MySqlcon)
                        Label9.Text = "Sucursal: " + su.Nombre + " Cliente: " + Cl.Nombre
                    End If
                Case 0
                    Remision.ID = Fb.id(0)
                    Remision.LlenaDatos()
                    If idCliente <> Remision.IdCliente Then
                        idCliente = Remision.IdCliente
                        Dim Cl As New dbClientes(idCliente, MySqlcon)
                        idPrecioLista = Cl.IdLista
                        Dim su As New dbSucursales(idSucursal, MySqlcon)
                        Label9.Text = "Sucursal: " + su.Nombre + " Cliente: " + Cl.Nombre
                    End If
            End Select
            EstadoVentana = 0
            Estado = Estados.SinGuardar
            ConsultaDetalles()
        End If
    End Sub
    Private Sub PresionaEnter()
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If Estado = Estados.Inicio And EstadoVentana <> 6 Then Guardar()
        'If Estado = Estados.SinGuardar And (EstadoVentana = 1 Or EstadoVentana = 2) Then

        'End If

        Select Case EstadoVentana
            Case 0
                If TextBox1.Text <> "" Then
                    Dim AgregaRef As Byte = 0
                    If Tipo = 0 Then
                        If TextBox1.Text.StartsWith("%") And TextBox1.Text.EndsWith("%") And TextBox1.Text.Length > 2 Then
                            AgregaRef = 1
                        Else
                            If TextBox1.Text.StartsWith("$") And TextBox1.Text.EndsWith("$") And TextBox1.Text.Length > 2 Then
                                AgregaRef = 2
                            End If
                        End If
                    End If
                    If AgregaRef <> 0 Then
                        AgregaRefencia(TextBox1.Text.Replace("%", "").Replace("$", ""), AgregaRef)
                    Else
                        Label4.Text = "Artículo: No existe"
                        TextBox1.Text = ""
                    End If

                Else
                    If Estado = Estados.SinGuardar And DGDetalles.RowCount > 0 Then
                        EstadoVentana = 3
                        TextBox1.BackColor = ColorAzul
                        Label4.BackColor = ColorAzul
                        Label4.Text = "Cobrar"
                    End If
                End If
            Case 1
                AgregarArticulos()
            Case 2
                If TextBox1.Text = "" And DGDetalles.RowCount > 0 Then
                    EstadoVentana = 3
                    TextBox1.BackColor = ColorAzul
                    Label4.BackColor = ColorAzul
                    Label4.Text = "Cobrar"
                End If
                Dim AgregaRef As Byte = 0
                If Tipo = 0 Then
                    If TextBox1.Text.StartsWith("%") And TextBox1.Text.EndsWith("%") And TextBox1.Text.Length > 2 Then
                        AgregaRef = 1
                    Else
                        If TextBox1.Text.StartsWith("$") And TextBox1.Text.EndsWith("$") And TextBox1.Text.Length > 2 Then
                            AgregaRef = 2
                        End If
                    End If
                End If
                If AgregaRef <> 0 Then
                    AgregaRefencia(TextBox1.Text.Replace("%", "").Replace("$", ""), AgregaRef)
                End If
                Dim HayRenlgon As Boolean = False
                Select Case Tipo
                    Case 0
                        If RemisionDetalles.ID <> 0 Then HayRenlgon = True
                    Case 1
                        If PedidoDetalles.ID <> 0 Then HayRenlgon = True
                    Case 2
                        If CotizacionDetalles.ID <> 0 Then HayRenlgon = True
                End Select
                If HayRenlgon And AgregaRef = 0 Then
                    If TextBox1.Text.Contains("+") Then
                        If IsNumeric(TextBox1.Text.Replace("+", "")) Then
                            CambiaCantidadconcepto(CDbl(TextBox1.Text.Replace("+", "")))
                        Else
                            CambiaCantidadconcepto(1)
                        End If
                        TextBox1.Text = ""
                    End If
                    If TextBox1.Text.Contains("-") Then

                        Dim OkBorrar As Boolean = True
                        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.VentasBaja, PermisosN.Secciones.PuntodeVenta) = False Then
                            If MsgBox("No tiene permisos para realizar esta operación. ¿Desea ingresar una autorización?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                                Dim CU As New frmCambioUsuario(1, 4)
                                If CU.ShowDialog <> Windows.Forms.DialogResult.OK Then
                                    OkBorrar = False
                                End If
                            Else
                                OkBorrar = False
                            End If
                        End If
                        If OkBorrar Then
                            If IsNumeric(TextBox1.Text.Replace("-", "")) Then
                                CambiaCantidadconcepto(-1 * CDbl(TextBox1.Text.Replace("-", "")))
                            Else
                                CambiaCantidadconcepto(-1)
                            End If
                            TextBox1.Text = ""
                        End If
                    End If
                    If TextBox1.Text.Contains("=") Or TextBox1.Text.Contains("/") Then

                        Dim OkBorrar As Boolean = True
                        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.AsignarCantidad, PermisosN.Secciones.PuntodeVenta) = False Then
                            If MsgBox("No tiene permisos para realizar esta operación. ¿Desea ingresar una autorización?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                                Dim CU As New frmCambioUsuario(1, 5)
                                If CU.ShowDialog <> Windows.Forms.DialogResult.OK Then
                                    OkBorrar = False
                                End If
                            Else
                                OkBorrar = False
                            End If
                        End If
                        If OkBorrar Then
                            If IsNumeric(TextBox1.Text.Replace("=", "")) Or IsNumeric(TextBox1.Text.Replace("/", "")) Then
                                AsignaCantidadconcepto(CDbl(TextBox1.Text.Replace("=", "").Replace("/", "")))
                            End If
                            TextBox1.Text = ""
                            Lectura = ""
                        End If
                    End If
                    'Else
                    '   Label4.Text = "Artículo: No existe"
                    '  TextBox1.Text = ""
                End If
            Case 3
                Try
                    AddError("PV: " + TextBox1.Text, "Pundo de venta cobrar.", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), Remision.ID)
                Catch ex As Exception

                End Try
                If O.PVConfirmarCobrar = 1 And TextBox1.Text = "" Then
                    If MsgBox("¿Es efectivo pago total?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, GlobalNombreApp) = MsgBoxResult.No Then
                        Exit Sub
                    End If
                End If
                If TextBox1.Text.Contains("*") Then
                    If TextBox1.Text.ToUpper.Contains("**CR") Then
                        Dim Ok As Boolean
                        Dim Cl As New dbClientes(idCliente, MySqlcon)
                        If O._LimitarCredito = 1 Then
                            If (Cl.DaSaldoAFecha(idCliente, Format(Date.Now, "yyyy/MM/dd")) + TotalVenta > Cl.Credito) Then
                                MsgBox("El cliente excede de su límite de crédito.", MsgBoxStyle.Information, GlobalNombreApp)
                                Ok = False
                            Else
                                Ok = True
                            End If
                        Else
                            Ok = True
                        End If
                        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirRemisionesCredito, PermisosN.Secciones.Ventas) = False Or O.NoPermitirRemisionesdeCredito = 1 Then
                            MsgBox("No tiene permiso para realizar ventas a crédito.", MsgBoxStyle.Information, GlobalNombreApp)
                            Ok = False
                        End If
                        
                        If Ok Then
                            Dim fmp As New frmVentasSelectorMetodosPago(1, Remision.ID, TotalVenta, 0, False)
                            fmp.ShowDialog()
                            fmp.Dispose()
                            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
                            DR = MetodosdePago.ConsultaReader(1, Remision.ID)
                            If DR.Read() Then
                                Idconcepto = DR("idforma")
                            End If
                            DR.Close()
                            Recibido = TotalVenta
                            Cambio = 0
                            CerrarVenta()
                        End If
                    Else
                        If TextBox1.Text.ToUpper.Contains("**") Then
                            Dim fmp As New frmVentasSelectorMetodosPago(1, Remision.ID, TotalVenta, 1, False)
                            fmp.ShowDialog()
                            fmp.Dispose()
                            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
                            DR = MetodosdePago.ConsultaReader(1, Remision.ID)
                            If DR.Read() Then
                                Idconcepto = DR("idforma")
                            End If
                            DR.Close()
                            Recibido = TotalVenta
                            Cambio = 0
                            CerrarVenta()
                        Else
                            Dim FPR As New dbFormasdePagoRemisiones(MySqlcon)
                            If FPR.BuscaForma(TextBox1.Text.Replace("*", "").ToUpper) Then
                                Select Case FPR.Tipo
                                    Case 2
                                        Idconcepto = FPR.ID
                                        Recibido = TotalVenta
                                        Cambio = 0
                                        CerrarVenta()
                                    Case 1
                                        Idconcepto = FPR.ID
                                        'Recibido = TotalVenta
                                        EstadoVentana = 4
                                        TextBox1.Text = ""
                                    Case 3
                                        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirRemisionesCredito, PermisosN.Secciones.Ventas) = True And O.NoPermitirRemisionesdeCredito = 0 Then
                                            Dim Cl As New dbClientes(idCliente, MySqlcon)
                                            If O._LimitarCredito = 1 Then
                                                If (Cl.DaSaldoAFecha(idCliente, Format(Date.Now, "yyyy/MM/dd")) + TotalVenta > Cl.Credito) And FPR.Tipo = dbFormasdePagoRemisiones.Tipos.Credito Then
                                                    MsgBox("El cliente exede de su límite de crédito.", MsgBoxStyle.Information, GlobalNombreApp)
                                                Else
                                                    Idconcepto = FPR.ID
                                                    Recibido = TotalVenta
                                                    Cambio = 0
                                                    CerrarVenta()
                                                End If
                                            Else

                                                Idconcepto = FPR.ID
                                                Recibido = TotalVenta
                                                Cambio = 0
                                                CerrarVenta()
                                            End If
                                        Else
                                            MsgBox("No tiene permiso para realizar ventas a crédito.", MsgBoxStyle.Information, GlobalNombreApp)
                                        End If
                                End Select
                            End If
                        End If
                    End If
                    
                Else
                    If TextBox1.Text = "" Then
                        'If IsNumeric(TextBox1.Text) Then
                        'Idconcepto = 1
                        Dim FPR As New dbFormasdePagoRemisiones(Idconcepto, MySqlcon)
                        Dim Cl As New dbClientes(idCliente, MySqlcon)
                        If O._LimitarCredito = 1 Then
                            If (Cl.DaSaldoAFecha(idCliente, Format(Date.Now, "yyyy/MM/dd")) + TotalVenta > Cl.Credito) And FPR.Tipo = dbFormasdePagoRemisiones.Tipos.Credito Then
                                MsgBox("El cliente exede de su límite de crédito.", MsgBoxStyle.Information, GlobalNombreApp)
                            Else
                                Recibido = TotalVenta
                                Cambio = 0
                                'Label3.Text = "Recibido: $" + TotalVenta.ToString + " Cambio:$0.00"
                                Label11.Text = "$" + Format(TotalVenta, "0.00")
                                Label10.Text = "$0.00"
                                CerrarVenta()
                            End If
                        Else
                            Recibido = TotalVenta
                            Cambio = 0
                            'Label3.Text = "Recibido: $" + TotalVenta.ToString + " Cambio:$0.00"
                            Label11.Text = "$" + Format(TotalVenta, "0.00")
                            Label10.Text = "$0.00"
                            CerrarVenta()
                        End If
                        'cerrar venta
                        'End If
                    Else
                        If IsNumeric(TextBox1.Text) Then
                            'Idconcepto = 1
                            Dim FPR As New dbFormasdePagoRemisiones(Idconcepto, MySqlcon)
                            Dim Cl As New dbClientes(idCliente, MySqlcon)
                            If O._LimitarCredito = 1 Then
                                If (Cl.DaSaldoAFecha(idCliente, Format(Date.Now, "yyyy/MM/dd")) + TotalVenta > Cl.Credito) And FPR.Tipo = dbFormasdePagoRemisiones.Tipos.Credito Then
                                    MsgBox("El cliente excede de su límite de crédito.", MsgBoxStyle.Information, GlobalNombreApp)
                                Else
                                    Recibido = CDbl(TextBox1.Text)
                                    Cambio = Recibido - TotalVenta
                                    'Label3.Text = "Recibido: $" + TextBox1.Text + " Cambio: " + Format(CDbl(TextBox1.Text) - TotalVenta, "$#,##0.00")
                                    Label11.Text = Format(CDbl(TextBox1.Text), "$#,##0.00")
                                    Label10.Text = Format(CDbl(TextBox1.Text) - TotalVenta, "$#,##0.00")
                                    CerrarVenta()
                                End If
                            Else
                                Recibido = CDbl(TextBox1.Text)
                                Cambio = Recibido - TotalVenta
                                'Label3.Text = "Recibido: $" + TextBox1.Text + " Cambio: " + Format(CDbl(TextBox1.Text) - TotalVenta, "$#,##0.00")
                                Label11.Text = Format(CDbl(TextBox1.Text), "$#,##0.00")
                                Label10.Text = Format(CDbl(TextBox1.Text) - TotalVenta, "$#,##0.00")
                                CerrarVenta()
                            End If
                            'cerrar venta
                        End If
                    End If
                End If
            Case 4
                    If IsNumeric(TextBox1.Text) Then
                        'Label3.Text = "Recibido: $" + TextBox1.Text + " Cambio: " + Format(CDbl(TextBox1.Text) - TotalVenta, "$#,##0.00")
                        Label11.Text = Format(CDbl(TextBox1.Text), "$#,##0.00")
                        Label10.Text = Format(CDbl(TextBox1.Text) - TotalVenta, "$#,##0.00")
                        CerrarVenta()
                        'cerrar venta
                    End If
            Case 5
                    Dim HayRenlgon As Boolean = False
                    Select Case Tipo
                        Case 0
                            If RemisionDetalles.ID <> 0 Then HayRenlgon = True
                        Case 1
                            If PedidoDetalles.ID <> 0 Then HayRenlgon = True
                        Case 2
                            If CotizacionDetalles.ID <> 0 Then HayRenlgon = True
                    End Select
                    If HayRenlgon Then
                        If IsNumeric(TextBox1.Text) And (TextBox1.Text.Contains("+") = False Or TextBox1.Text.Contains("-") = False Or TextBox1.Text.Contains("=") = False Or TextBox1.Text.Contains("/") = False) Then
                            If Articulo.PrecioNeto = 1 Then
                                CambiaPrecioConcepto(CDbl(TextBox1.Text) / (1 + Articulo.Iva / 100), 0)
                            Else
                                CambiaPrecioConcepto(CDbl(TextBox1.Text), 0)
                            End If
                            EstadoVentana = 2
                            TextBox1.BackColor = Color.White
                            Label4.BackColor = Color.Transparent
                            TextBox1.Text = ""
                            Label4.Text = "Producto:"
                        Else
                            Label4.Text = "Indicar Precio - Debe indicar un número."
                        End If
                    End If
            Case 6
                    If TextBox1.Text <> "" Then

                        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.VentasCancelar, PermisosN.Secciones.PuntodeVenta) = True Then
                            TextBox1.Text = TextBox1.Text.Replace("%", "").Replace("$", "")
                            Dim IdDoc As Integer
                            Select Case Tipo
                                Case 0
                                    IdDoc = Remision.BuscaRemision(TextBox1.Text)
                                    If IdDoc <> 0 Then
                                        Dim Re As New dbVentasRemisiones(IdDoc, MySqlcon)
                                        If Re.Estado = 3 Then
                                            If MsgBox("¿Cancelar documento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                                                Dim S As New dbInventarioSeries(MySqlcon)
                                            Dim FormaPago As New dbFormasdePagoRemisiones(Re.idForma, MySqlcon)
                                            Dim TotalAgregadoEf As Double = MetodosdePago.TotalAgregadoPorTipoRemisiones(1, IdDoc)
                                            If TotalAgregadoEf <> 0 Then CajaG.MovimientodeCaja(Re.idCaja, TotalAgregadoEf * -1)
                                                S.QuitaSeriesARemision(IdDoc)
                                                Re.RegresaInventario(IdDoc)
                                                Re.DaTotal(IdDoc, Re.IdMoneda)
                                                Re.Modificar(IdDoc, Re.Fecha, Re.Folio, Re.Desglosar, Re.Iva, Estados.Cancelada, Re.Serie, Re.TipodeCambio, Re.IdMoneda, Re.Subtototal, Re.TotalVenta, Re.IdCliente, Re.idForma, Re.IdVEndedor, "", 0, False, Re.idCaja)
                                                MsgBox("Cancelación exitosa.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                                            End If
                                        Else
                                            MsgBox("Documento ya cancelado.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                                        End If
                                    Else
                                        MsgBox("No existe el documento.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                                    End If
                                Case 1
                                    IdDoc = Pedido.BuscaPedido(TextBox1.Text)
                                    If IdDoc <> 0 Then
                                        Dim Re As New dbVentasPedidos(IdDoc, MySqlcon)
                                        If Re.Estado = 3 Then
                                            If MsgBox("¿Cancelar documento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

                                                Re.DaTotal(IdDoc, 2)
                                                Re.Modificar(IdDoc, Re.Fecha, Re.Folio, Re.Desglosar, Re.Iva, Estados.Cancelada, Re.Subtotal, Re.TotalVenta, Re.IdCliente, Re.Serie, Re.IdVendedor, "")
                                                MsgBox("Cancelación exitosa.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                                            End If
                                        Else
                                            MsgBox("Documento ya cancelado.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                                        End If
                                    Else
                                        MsgBox("No existe el documento.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                                    End If
                                Case 2
                                    IdDoc = Cotizacion.BuscaCotizacion(TextBox1.Text)
                                    If IdDoc <> 0 Then
                                        Dim Re As New dbVentasCotizaciones(IdDoc, MySqlcon)
                                        If Re.Estado <> 4 Then
                                            If MsgBox("¿Cancelar documento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                                                Re.DaTotal(IdDoc, 2)
                                                Re.Modificar(IdDoc, Re.Fecha, Re.Folio, Re.Desglosar, Re.Iva, Estados.Cancelada, Re.Subtotal, Re.TotalVenta, Re.IdCliente, Re.Serie, Re.IdVendedor, "", 2)
                                                MsgBox("Cancelación exitosa.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                                            End If
                                        Else
                                            MsgBox("Documento ya cancelado.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                                        End If
                                    Else
                                        MsgBox("No existe el documento.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                                    End If
                            End Select
                            Label4.Text = "Producto:"
                            EstadoVentana = 0
                            TextBox1.Text = ""
                            TextBox1.BackColor = Color.White
                            Label4.BackColor = Color.Transparent
                        End If
                    Else
                        Label4.Text = "Debe indicar un folio."
                    End If
            Case 7
                    Dim HayRenlgon As Boolean = False
                    Select Case Tipo
                        Case 0
                            If RemisionDetalles.ID <> 0 Then HayRenlgon = True
                        Case 1
                            If PedidoDetalles.ID <> 0 Then HayRenlgon = True
                        Case 2
                            If CotizacionDetalles.ID <> 0 Then HayRenlgon = True
                    End Select
                    If HayRenlgon Then
                        If IsNumeric(TextBox1.Text) And (TextBox1.Text.Contains("+") = False Or TextBox1.Text.Contains("-") = False Or TextBox1.Text.Contains("=") = False Or TextBox1.Text.Contains("/") = False) Then

                            If Articulo.PrecioNeto = 1 Then
                                CambiaPrecioConcepto((PrecioParaDescuento - (PrecioParaDescuento * CDbl(TextBox1.Text) / 100)) / (1 + Articulo.Iva / 100), Descuento)
                            Else
                                CambiaPrecioConcepto((PrecioParaDescuento - (PrecioParaDescuento * CDbl(TextBox1.Text) / 100)), Descuento)
                            End If
                            EstadoVentana = 2
                            TextBox1.BackColor = Color.White
                            Label4.BackColor = Color.Transparent
                            TextBox1.Text = ""
                            Label4.Text = "Producto:"
                        Else
                            Label4.Text = "Indicar Descuento - Debe indicar un número."
                        End If
                    End If
        End Select

    End Sub
    Private Sub CerrarVenta()
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.VentasAlta, PermisosN.Secciones.PuntodeVenta) = False Then
                Exit Sub
            End If
            If Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
                Select Case Tipo
                    Case 0 'remision
                        Remision.DaTotal(Remision.ID, 2)
                        'If Opc.ActivarOc = 1 And Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc Then
                        If Opc.ActivarOc = 1 And ((Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc) Or (Format(Now, "HH:mm") >= Opc.HoraInicioOc2 And Format(Now, "HH:mm") <= Opc.HoraFinOc2)) Then
                            Dim NF As Integer
                            NF = Remision.DaNuevoFolio(Opc.SerieOc, Remision.IdSucursal)
                            If NF < Opc.FolioOc Then NF = Opc.FolioOc
                            Remision.Modificar(Remision.ID, Remision.Fecha, NF, 0, 0, Estados.Guardada, Opc.SerieOc, Remision.TipodeCambio, Remision.IdMoneda, Remision.Subtototal, Remision.TotalVenta, idCliente, Idconcepto, IdVendedor, Remision.Comentario, 0, False, Remision.idCaja)
                        Else
                            Remision.Modificar(Remision.ID, Remision.Fecha, Remision.DaNuevoFolio(Remision.Serie, Remision.IdSucursal), 0, 0, Estados.Guardada, Remision.Serie, Remision.TipodeCambio, Remision.IdMoneda, Remision.Subtototal, Remision.TotalVenta, idCliente, Idconcepto, IdVendedor, Remision.Comentario, 0, False, Remision.idCaja)
                        End If
                        Remision.ModificaInventario(Remision.ID, 0)
                        Dim TotalAgregado As Double = MetodosdePago.TotalAgregado(1, Remision.ID)
                        If TotalAgregado = 0 Then
                            MetodosdePago.Guardar(1, Idconcepto, Remision.TotalVenta, Remision.ID)
                        End If
                        Dim TotalAgregadoEf As Double = MetodosdePago.TotalAgregadoPorTipoRemisiones(1, Remision.ID)
                        'Dim FormaPago As New dbFormasdePagoRemisiones(Remision.idForma, MySqlcon)
                        If TotalAgregadoEf <> 0 Then CajaG.MovimientodeCaja(Remision.idCaja, TotalAgregadoEf)
                        If CajaG.Maximo > 0 Then
                            If CajaG.Maximo < CajaG.CuantoEnCaja(IdCaja) Then
                                MsgBox("Efectivo en caja exedió $" + CajaG.Maximo.ToString("#,###,##0.00") + " , favor de hacer un retiro.", MsgBoxStyle.Exclamation, GlobalNombreApp)
                            End If
                        End If
                        If IdsDocumentos.TotalDatos > 0 Then
                            Dim Cont As Integer
                            While Cont <= IdsDocumentos.TotalDatos
                                If TiposReferencias.Valor(Cont) = 1 Then
                                    Dim CotR As New dbVentasCotizaciones(MySqlcon)
                                    If EliminarREf Then
                                        CotR.Eliminar(IdsDocumentos.Valor(Cont))
                                    Else
                                        CotR.Usar(IdsDocumentos.Valor(Cont))
                                    End If
                                Else
                                    Dim PedR As New dbVentasPedidos(MySqlcon)
                                    If EliminarREf Then
                                        PedR.Eliminar(IdsDocumentos.Valor(Cont))
                                    Else
                                        PedR.Usar(IdsDocumentos.Valor(Cont))
                                    End If
                                End If
                                Cont += 1
                            End While

                        End If
                    Case 1 'pedido
                        Pedido.DaTotal(Pedido.ID, 2)
                        Pedido.Modificar(Pedido.ID, Pedido.Fecha, Pedido.DaNuevoFolio(Pedido.Serie, Pedido.IdSucursal), 0, 0, Estados.Guardada, Pedido.Subtotal, Pedido.TotalVenta, idCliente, Pedido.Serie, IdVendedor, Pedido.Comentario)
                    Case 2 'cotizacion
                        Cotizacion.DaTotal(Cotizacion.ID, 2)
                        Cotizacion.Modificar(Cotizacion.ID, Cotizacion.Fecha, Cotizacion.DaNuevoFolio(Cotizacion.Serie, Cotizacion.IdSucursal), 0, 0, Estados.Pendiente, Cotizacion.Subtotal, Cotizacion.TotalVenta, idCliente, Cotizacion.Serie, IdVendedor, Cotizacion.Comentario, 2)
                End Select
                Imprimir()
                Nuevo()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub CerrarVentaPendiente()
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.VentasAlta, PermisosN.Secciones.PuntodeVenta) = False Then
                Exit Sub
            End If
            If MsgBox("¿Dejar la venta pendiente?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                Exit Sub
            End If
            If Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
                Select Case Tipo
                    Case 0 'remision
                        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirPendientesRemisiones, PermisosN.Secciones.Ventas) = True Then
                            Remision.DaTotal(Remision.ID, 2)
                            If Opc.ActivarOc = 1 And ((Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc) Or (Format(Now, "HH:mm") >= Opc.HoraInicioOc2 And Format(Now, "HH:mm") <= Opc.HoraFinOc2)) Then
                                Dim NF As Integer
                                NF = Remision.DaNuevoFolio(Opc.SerieOc, Remision.IdSucursal)
                                If NF < Opc.FolioOc Then NF = Opc.FolioOc
                                Remision.Modificar(Remision.ID, Remision.Fecha, NF, 0, 0, Estados.Pendiente, Opc.SerieOc, Remision.TipodeCambio, Remision.IdMoneda, Remision.Subtototal, Remision.TotalVenta, idCliente, Idconcepto, IdVendedor, Remision.Comentario, 0, False, Remision.idCaja)
                            Else
                                Remision.Modificar(Remision.ID, Remision.Fecha, Remision.DaNuevoFolio(Remision.Serie, Remision.IdSucursal), 0, 0, Estados.Pendiente, Remision.Serie, Remision.TipodeCambio, Remision.IdMoneda, Remision.Subtototal, Remision.TotalVenta, idCliente, Idconcepto, IdVendedor, Remision.Comentario, 0, False, Remision.idCaja)
                            End If
                        Else
                            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                            Exit Sub
                        End If
                        'Remision.Modificar(Remision.ID, Remision.Fecha, Remision.DaNuevoFolio(Remision.Serie, Remision.IdSucursal), 0, 0, Estados.Pendiente, Remision.Serie, Remision.TipodeCambio, Remision.IdMoneda, Remision.Subtototal, Remision.TotalVenta, idCliente, Idconcepto, IdVendedor, "", 0, False)
                    Case 1 'pedido
                        Pedido.DaTotal(Pedido.ID, 2)
                        Pedido.Modificar(Pedido.ID, Pedido.Fecha, Pedido.DaNuevoFolio(Pedido.Serie, Pedido.IdSucursal), 0, 0, Estados.Pendiente, Pedido.Subtotal, Pedido.TotalVenta, idCliente, Pedido.Serie, IdVendedor, Pedido.Comentario)
                    Case 2 'cotizacion
                        Cotizacion.DaTotal(Cotizacion.ID, 2)
                        Cotizacion.Modificar(Cotizacion.ID, Cotizacion.Fecha, Cotizacion.DaNuevoFolio(Cotizacion.Serie, Cotizacion.IdSucursal), 0, 0, Estados.Pendiente, Cotizacion.Subtotal, Cotizacion.TotalVenta, idCliente, Cotizacion.Serie, IdVendedor, Cotizacion.Comentario, 2)
                End Select
                'Imprimir()
                Nuevo()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub CambiaCantidadconcepto(ByVal Cantidad As Double)
        Try
            If GlobalSoloExistencia = True And Tipo = 0 Then
                Dim I As New dbInventario(RemisionDetalles.Idinventario, MySqlcon)
                Dim Cant As Double
                Cant = I.DaInventario(idAlmacen, RemisionDetalles.Idinventario)
                If Cant < Cantidad + Remision.DaTotalCantidadxArticulo(Remision.ID, RemisionDetalles.Idinventario) Then
                    MsgBox("Artículo sin existencia.", MsgBoxStyle.Information, GlobalNombreApp)
                    EstadoVentana = 2
                    TextBox1.Text = ""
                    Exit Sub
                End If
            End If
            CantidadArtDesc = Cantidad
            Select Case Tipo
                Case 0 'remision
                    'Descuentos
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CD.BuscaridInventario(RemisionDetalles.ID), fechaFormato() + " " + horaFormato(), idSucursal))
                    auxIDRenglon = RemisionDetalles.ID
                    idProducto = RemisionDetalles.BuscaridInventario(RemisionDetalles.ID)
                    nombreProducto = P.buscarnombreInventarioRem(RemisionDetalles.ID)
                    precioArticulo = P.PrecioPromRemi(Remision.ID, idProducto)
                    '--------
                    RemisionDetalles.AgregarCantidad(RemisionDetalles.ID, Cantidad, TipoRedondeo, CantidadDecimales)
                    If RemisionDetalles.Cantidad > 0 Then
                        If RemisionDetalles.Inventario.PorLotes = 1 Then
                            Dim F As New frmInventarioLotes(0, RemisionDetalles.ID, 0, 0, RemisionDetalles.Cantidad, RemisionDetalles.Inventario.ID, 0, 0, 0, 0, idAlmacen, AlmacenNombre, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        If RemisionDetalles.Inventario.Aduana = 1 Then
                            Dim F As New frmInventarioAduana(0, RemisionDetalles.ID, 0, 0, RemisionDetalles.Cantidad, RemisionDetalles.Inventario.ID, 0, 0, 0, 0, idAlmacen, AlmacenNombre, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                    End If
                    'DESCUENTO

                    If tipoElimianr = "Promocion" Then
                        '  modificarDescuento(P.descModificar(RemisionDetalles.ID, "VentasRemisiones"))
                        modificarDescuentoCantidad(P.descModificar(auxIDRenglon, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales)
                    Else
                        If P.descModificar(auxIDRenglon, tipoDescuento) <> 0 Then
                            modificarDescuentoCantidad(P.descModificar(auxIDRenglon, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales)
                        End If
                    End If
                    'END DESCUENTOS

                Case 1 'pedido
                    'Descuento
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(PedidoDetalles.BuscaridInventario(PedidoDetalles.ID), fechaFormato() + " " + horaFormato(), idSucursal))
                    auxIDRenglon = PedidoDetalles.ID
                    idProducto = PedidoDetalles.BuscaridInventario(PedidoDetalles.ID)
                    nombreProducto = P.buscarnombreInventarioPed(PedidoDetalles.ID)
                    precioArticulo = P.PrecioPromPedido(Pedido.ID, idProducto)
                    '-------------
                    PedidoDetalles.AgregarCantidad(PedidoDetalles.ID, Cantidad, TipoRedondeo, CantidadDecimales)

                    'DESCUENTO

                    If tipoElimianr = "Promocion" Then
                        '  modificarDescuento(P.descModificar(RemisionDetalles.ID, "VentasRemisiones"))
                        modificarDescuentoCantidad(P.descModificar(auxIDRenglon, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales)
                    Else
                        If P.descModificar(auxIDRenglon, tipoDescuento) <> 0 Then
                            modificarDescuentoCantidad(P.descModificar(auxIDRenglon, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales)
                        End If
                    End If
                    'END DESCUENTOS
                Case 2 'cotizacion
                    'desc
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CotizacionDetalles.BuscaridInventario(CotizacionDetalles.ID), fechaFormato() + " " + horaFormato(), idSucursal))
                    auxIDRenglon = CotizacionDetalles.ID
                    idProducto = CotizacionDetalles.BuscaridInventario(CotizacionDetalles.ID)
                    nombreProducto = P.buscarnombreInventarioCot(CotizacionDetalles.ID)
                    precioArticulo = P.PrecioPromCot(Cotizacion.ID, idProducto)
                    '------------
                    CotizacionDetalles.AgregarCantidad(CotizacionDetalles.ID, Cantidad, TipoRedondeo, CantidadDecimales)

                    'DESCUENTO

                    If tipoElimianr = "Promocion" Then
                        '  If P.descModificar(auxIDRenglon, tipoDescuento) <> 0 Then
                        modificarDescuentoCantidad(P.descModificar(auxIDRenglon, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales)
                        ' End If
                    Else
                        If P.descModificar(auxIDRenglon, tipoDescuento) <> 0 Then
                            modificarDescuentoCantidad(P.descModificar(auxIDRenglon, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales)
                        End If
                    End If
                    'END DESCUENTOS
            End Select
            ConsultaDetalles()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub AsignaCantidadconcepto(ByVal Cantidad As Double)
        Try
            If GlobalSoloExistencia = True And Tipo = 0 Then

                Dim I As New dbInventario(RemisionDetalles.Idinventario, MySqlcon)
                Dim Cant As Double
                Cant = I.DaInventario(idAlmacen, RemisionDetalles.Idinventario)
                If Cant < Cantidad Then
                    MsgBox("Artículo sin existencia.", MsgBoxStyle.Information, GlobalNombreApp)
                    EstadoVentana = 2
                    TextBox1.Text = ""
                    Exit Sub
                End If
            End If
            CantidadArtDesc = Cantidad
            Select Case Tipo
                Case 0 'remision
                    'desc
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CD.BuscaridInventario(RemisionDetalles.ID), fechaFormato() + " " + horaFormato(), idSucursal))
                    auxIDRenglon = RemisionDetalles.ID
                    idProducto = RemisionDetalles.BuscaridInventario(RemisionDetalles.ID)
                    nombreProducto = P.buscarnombreInventarioRem(RemisionDetalles.ID)
                    precioArticulo = P.PrecioPromRemi(Remision.ID, idProducto)
                    '-----------
                    RemisionDetalles.AsignaCantidad(RemisionDetalles.ID, Cantidad, TipoRedondeo, CantidadDecimales)
                    If RemisionDetalles.Cantidad > 0 Then
                        If RemisionDetalles.Inventario.PorLotes = 1 Then
                            Dim F As New frmInventarioLotes(0, RemisionDetalles.ID, 0, 0, RemisionDetalles.Cantidad, RemisionDetalles.Inventario.ID, 0, 0, 0, 0, idAlmacen, AlmacenNombre, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        If RemisionDetalles.Inventario.Aduana = 1 Then
                            Dim F As New frmInventarioAduana(0, RemisionDetalles.ID, 0, 0, RemisionDetalles.Cantidad, RemisionDetalles.Inventario.ID, 0, 0, 0, 0, idAlmacen, AlmacenNombre, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                    End If
                    'DESCUENTOS
                    If tipoElimianr = "Promocion" Then
                        Dim tablaDesc As New DataTable
                        Dim idDescuento As Integer
                        idDescuento = P.HayDescuento(idProducto, fechaFormato() + " " + horaFormato(), idSucursal)
                        tablaDesc = P.tablaDesc(idDescuento)

                        eliminarProm("DESCUENTO: PROMOCIÓN " + tablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, idDescuento)
                    Else
                        If P.descModificar(auxIDRenglon, tipoDescuento) <> 0 Then
                            RemisionDetalles.AsignaCantidadDescuento(P.descModificar(auxIDRenglon, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales)
                            'modificarDescuentoCantidad(P.descModificar(auxIDRenglon, "VentasRemisiones"), Cantidad, TipoRedondeo, CantidadDecimales)
                        End If
                    End If
                    'END DESCUENTOS

                Case 1 'pedido

                    'desc
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(PedidoDetalles.BuscaridInventario(PedidoDetalles.ID), fechaFormato() + " " + horaFormato(), idSucursal))
                    auxIDRenglon = PedidoDetalles.ID
                    idProducto = PedidoDetalles.BuscaridInventario(PedidoDetalles.ID)
                    nombreProducto = P.buscarnombreInventarioPed(PedidoDetalles.ID)
                    precioArticulo = P.PrecioPromPedido(Pedido.ID, idProducto)
                    '------------
                    PedidoDetalles.AsignaCantidad(PedidoDetalles.ID, Cantidad, TipoRedondeo, CantidadDecimales)

                    'DESCUENTOS
                    If tipoElimianr = "Promocion" Then
                        '    '  modificarDescuento(P.descModificar(RemisionDetalles.ID, "VentasRemisiones"))
                        Dim tablaDesc As New DataTable
                        Dim idDescuento As Integer
                        idDescuento = P.HayDescuento(idProducto, fechaFormato() + " " + horaFormato(), idSucursal)
                        tablaDesc = P.tablaDesc(idDescuento)

                        eliminarProm("DESCUENTO: PROMOCIÓN " + tablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, idDescuento)
                    Else
                        If P.descModificar(auxIDRenglon, tipoDescuento) <> 0 Then
                            PedidoDetalles.AsignaCantidadDescuento(P.descModificar(auxIDRenglon, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales)
                            'modificarDescuentoCantidad(P.descModificar(auxIDRenglon, "VentasRemisiones"), Cantidad, TipoRedondeo, CantidadDecimales)
                        End If
                    End If
                    'END DESCUENTOS

                Case 2 'cotizacion
                    'desc
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CotizacionDetalles.BuscaridInventario(CotizacionDetalles.ID), fechaFormato() + " " + horaFormato(), idSucursal))
                    auxIDRenglon = CotizacionDetalles.ID
                    idProducto = CotizacionDetalles.BuscaridInventario(CotizacionDetalles.ID)
                    nombreProducto = P.buscarnombreInventarioCot(CotizacionDetalles.ID)
                    precioArticulo = P.PrecioPromCot(Cotizacion.ID, idProducto)
                    '-------

                    CotizacionDetalles.AsignaCantidad(CotizacionDetalles.ID, Cantidad, TipoRedondeo, CantidadDecimales)

                    'DESCUENTOS
                    If tipoElimianr = "Promocion" Then
                        '    '  modificarDescuento(P.descModificar(RemisionDetalles.ID, "VentasRemisiones"))
                        Dim tablaDesc As New DataTable
                        Dim idDescuento As Integer
                        idDescuento = P.HayDescuento(idProducto, fechaFormato() + " " + horaFormato(), idSucursal)
                        tablaDesc = P.tablaDesc(idDescuento)

                        eliminarProm("DESCUENTO: PROMOCIÓN " + tablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, idDescuento)
                    Else
                        If P.descModificar(auxIDRenglon, tipoDescuento) <> 0 Then
                            CotizacionDetalles.AsignaCantidadDescuento(P.descModificar(auxIDRenglon, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales)
                            'modificarDescuentoCantidad(P.descModificar(auxIDRenglon, "VentasRemisiones"), Cantidad, TipoRedondeo, CantidadDecimales)
                        End If
                    End If
                    'END DESCUENTOS

            End Select
            ConsultaDetalles()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub CambiaPrecioConcepto(ByVal Cantidad As Double, ByVal pDescuento As Double)
        Try
            Select Case Tipo
                Case 0 'remision
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CD.BuscaridInventario(RemisionDetalles.ID), fechaFormato() + " " + horaFormato(), idSucursal))


                    'DESCUENTOS
                    idProducto = RemisionDetalles.BuscaridInventario(RemisionDetalles.ID)
                    precioArticulo = Double.Parse(TextBox1.Text)
                    ' precioArticulo = P.PrecioPromRemi(Remision.ID, Articulo.ID)
                    RemisionDetalles.CambiaPrecio(RemisionDetalles.ID, Cantidad, TipoRedondeo, CantidadDecimales, pDescuento)
                    If tipoElimianr = "Promocion" Then
                        Dim tablaDesc As New DataTable
                        Dim idDescuento As Integer
                        idDescuento = P.HayDescuento(idProducto, fechaFormato() + " " + horaFormato(), idSucursal)
                        tablaDesc = P.tablaDesc(idDescuento)
                        actualizaPrecioPromocion("DESCUENTO: PROMOCIÓN " + tablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, precioArticulo)
                    Else
                        If P.descModificar(RemisionDetalles.ID, tipoDescuento) <> 0 Then
                            modificarDescuentoPrecio(P.descModificar(RemisionDetalles.ID, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales, pDescuento)
                        End If
                    End If
                    'END DESCUENTOS
                Case 1 'pedido
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(PedidoDetalles.BuscaridInventario(PedidoDetalles.ID), fechaFormato() + " " + horaFormato(), idSucursal))
                    PedidoDetalles.CambiaPrecio(PedidoDetalles.ID, Cantidad, TipoRedondeo, CantidadDecimales, pDescuento)
                    precioArticulo = P.PrecioPromPedido(Pedido.ID, Articulo.ID)
                    'DESCUENTOS
                    idProducto = PedidoDetalles.BuscaridInventario(PedidoDetalles.ID)
                    precioArticulo = Double.Parse(TextBox1.Text)
                    If tipoElimianr = "Promocion" Then
                        Dim tablaDesc As New DataTable
                        Dim idDescuento As Integer
                        idDescuento = P.HayDescuento(idProducto, fechaFormato() + " " + horaFormato(), idSucursal)
                        tablaDesc = P.tablaDesc(idDescuento)
                        actualizaPrecioPromocion("DESCUENTO: PROMOCIÓN " + tablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, precioArticulo)
                    Else
                        If P.descModificar(PedidoDetalles.ID, tipoDescuento) <> 0 Then
                            modificarDescuentoPrecio(P.descModificar(PedidoDetalles.ID, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales, pDescuento)
                        End If
                    End If
                    'END DESCUENTOS

                Case 2 'cotizacion
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CotizacionDetalles.BuscaridInventario(CotizacionDetalles.ID), fechaFormato() + " " + horaFormato(), idSucursal))
                    CotizacionDetalles.CambiaPrecio(CotizacionDetalles.ID, Cantidad, TipoRedondeo, CantidadDecimales, pDescuento)
                    'DESCUENTOS
                    idProducto = CotizacionDetalles.BuscaridInventario(CotizacionDetalles.ID)
                    precioArticulo = Double.Parse(TextBox1.Text)
                    If tipoElimianr = "Promocion" Then
                        Dim tablaDesc As New DataTable
                        Dim idDescuento As Integer

                        idDescuento = P.HayDescuento(idProducto, fechaFormato() + " " + horaFormato(), idSucursal)
                        tablaDesc = P.tablaDesc(idDescuento)
                        actualizaPrecioPromocion("DESCUENTO: PROMOCIÓN " + tablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, precioArticulo)
                    Else
                        If P.descModificar(CotizacionDetalles.ID, tipoDescuento) <> 0 Then
                            modificarDescuentoPrecio(P.descModificar(CotizacionDetalles.ID, tipoDescuento), Cantidad, TipoRedondeo, CantidadDecimales, pDescuento)
                        End If
                    End If
                    'END DESCUENTOS

            End Select
            ConsultaDetalles()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub AgregarArticulos()
        Try
            Dim IP As New dbInventarioPrecios(MySqlcon)
            Dim Temp As Double
            If Estado = Estados.SinGuardar Then
                If Articulo.ID <> 0 Then
                    IP.BuscaPrecio(Articulo.ID, idPrecioLista)
                End If
                
                Temp = IP.Precio
                'Precio = IP.Precio
                If Articulo.PrecioNeto = 1 Then
                    If TipoRedondeo <> 0 Then
                        IP.Precio = GlobalRedondea(IP.Precio, TipoRedondeo, CantidadDecimales)
                        Temp = IP.Precio / (1 + (CDbl(Articulo.Iva) + CDbl(Articulo.ieps) - CDbl(Articulo.ivaRetenido)) / 100)
                    Else
                        Temp = IP.Precio / (1 + (CDbl(Articulo.Iva) + CDbl(Articulo.ieps) - CDbl(Articulo.ivaRetenido)) / 100)
                    End If
                Else
                    If TipoRedondeo <> 0 Then
                        Temp = GlobalRedondea(Temp * (1 + (Articulo.Iva + Articulo.ieps - Articulo.ivaRetenido) / 100), TipoRedondeo, CantidadDecimales)
                        Temp = Temp / (1 + CDbl(Articulo.Iva + Articulo.ieps - Articulo.ivaRetenido) / 100)
                        'Else
                        'Temp = Temp / (1 + CDbl(Articulo.Iva) / 100)
                    End If
                End If
                If Articulo.ID <> 0 And GlobalSoloExistencia = True And Articulo.Inventariable = 1 And Tipo = 0 Then
                    Dim Cant As Double
                    Cant = Articulo.DaInventario(idAlmacen, Articulo.ID)
                    If Cant < Remision.DaTotalCantidadxArticulo(Remision.ID, Articulo.ID) + 1 Then
                        MsgBox("Artículo sin existencia.", MsgBoxStyle.Information, GlobalNombreApp)
                        EstadoVentana = 0
                        TextBox1.Text = ""
                        Exit Sub
                    End If
                End If


                Precio = Temp
                CantidadArtDesc = 1
                Select Case Tipo
                    Case 0 'remision
                        
                        If Articulo.SepararKit = 0 Then
                            RemisionDetalles.Guardar(Remision.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, idAlmacen, Articulo.Iva, 0, 1, 0, Articulo.ieps, Articulo.ivaRetenido, 1, Articulo.TipoContenido.ID, 0)
                            RemisionDetalles.Inventario = Articulo
                        Else
                            If Articulo.EsKit = 1 And Articulo.SepararKit = 1 Then
                                RemisionDetalles.SeparaKit(Remision.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, idAlmacen, Articulo.Iva, 0, 1, 0, Articulo.ieps, Articulo.ivaRetenido, 1, Articulo.TipoContenido.ID, 0)
                            End If
                        End If
                        If Articulo.EsKit = 1 And Articulo.SepararKit = 0 Then
                            Dim IKits As New dbVentasKits(MySqlcon)
                            IKits.InsertarArticulosRemision(Articulo.ID, Remision.ID, RemisionDetalles.ID, 1, idAlmacen)
                        End If

                        If Articulo.PorLotes = 1 Then
                            Dim F As New frmInventarioLotes(0, RemisionDetalles.ID, 0, 0, 1, Articulo.ID, 0, 0, 0, 0, idAlmacen, AlmacenNombre, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        If Articulo.Aduana = 1 Then
                            Dim F As New frmInventarioAduana(0, RemisionDetalles.ID, 0, 0, 1, Articulo.ID, 0, 0, 0, 0, idAlmacen, AlmacenNombre, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If

                        'desc
                        nombreProducto = P.buscarnombreInventarioRem(RemisionDetalles.ID)
                        precioArticulo = Temp
                        canProducto = 1
                        precioArticulo = P.PrecioPromRemi(Remision.ID, Articulo.ID)
                        hayDescuento(Remision.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, idAlmacen, Articulo.Iva, 0, 1, 0)
                    Case 1 'pedido
                        PedidoDetalles.Guardar(Pedido.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, Articulo.Iva, 0, 1, Articulo.ieps, Articulo.ivaRetenido)
                        PedidoDetalles.Inventario = Articulo
                        'desc
                        nombreProducto = P.buscarnombreInventarioPed(PedidoDetalles.ID)
                        precioArticulo = Temp
                        canProducto = 1
                        precioArticulo = P.PrecioPromPedido(Pedido.ID, Articulo.ID)
                        hayDescuento(Pedido.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, 0, Articulo.Iva, 0, 1, 0)
                    Case 2 'cotizacion

                        CotizacionDetalles.Guardar(Cotizacion.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, Articulo.Iva, 0, 1, Articulo.ieps, Articulo.ivaRetenido)
                        CotizacionDetalles.Inventario = Articulo
                        'desc
                        nombreProducto = P.buscarnombreInventarioCot(CotizacionDetalles.ID)
                        precioArticulo = Temp
                        canProducto = 1
                        precioArticulo = P.PrecioPromCot(Cotizacion.ID, Articulo.ID)
                        hayDescuento(Cotizacion.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, idAlmacen, Articulo.Iva, 0, 1, 0)

                End Select
            End If
            TextBox1.Text = ""
            If Articulo.TipoContenido.UsaBascula = 0 Then
                If PidePrecio And GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CambiodePrecio, PermisosN.Secciones.PuntodeVenta) = True Then
                    EstadoVentana = 5
                    TextBox1.BackColor = ColorAmarillo
                    Label4.BackColor = ColorAmarillo
                    Label4.Text = "Indicar Precio:"
                    If Articulo.PrecioNeto = 1 Then
                        TextBox1.Text = Format(IP.Precio, "0.00")
                    Else
                        TextBox1.Text = Format(Temp, "0.00")
                    End If

                    TextBox1.SelectAll()
                Else
                    EstadoVentana = 2
                    TextBox1.BackColor = Color.White
                    Label4.BackColor = Color.Transparent
                    'Label4.Text = ""
                End If
            Else
                EstadoVentana = 7
                HaciendoCambio = False
                Try
                    If SerialPort1.IsOpen = False Then SerialPort1.Open()
                    'SerialPort1.DiscardInBuffer()
                    SerialPort1.Write(Chr(CInt(Secuencia)))
                    Lectura = ""
                    Veces = 0
                    If Articulo.PrecioNeto = 1 Then
                        Precio = IP.Precio
                    End If
                    TextBox1.Enabled = False
                    Timer1.Enabled = True
                Catch ex As Exception
                    MsgBox(ex.Message)
                    If SerialPort1.IsOpen Then SerialPort1.Close()
                    If PidePrecio And GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CambiodePrecio, PermisosN.Secciones.PuntodeVenta) = True Then
                        EstadoVentana = 5
                        TextBox1.BackColor = ColorAmarillo
                        Label4.BackColor = ColorAmarillo
                        Label4.Text = "Indicar Precio:"
                        If Articulo.PrecioNeto = 1 Then
                            TextBox1.Text = Format(IP.Precio, "0.00")
                        Else
                            TextBox1.Text = Format(Temp, "0.00")
                        End If
                        TextBox1.SelectAll()
                    Else
                        EstadoVentana = 2
                        TextBox1.BackColor = Color.White
                        Label4.BackColor = Color.Transparent
                        'Label4.Text = ""
                    End If
                End Try
            End If

            ConsultaDetalles()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub ConsultaDetalles()
        Try
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            T = Nothing
            Tabla.Rows.Clear()
            Select Case Tipo
                Case 0 'remision
                    T = RemisionDetalles.ConsultaReaderIVa(Remision.ID)
                Case 1 'pedido
                    T = PedidoDetalles.ConsultaReaderIVA(Pedido.ID)
                Case 2 'cotizacion
                    T = CotizacionDetalles.ConsultaReaderIVA(Cotizacion.ID)
            End Select
            While T.Read
                If T("cantidad") <> 0 Then
                    If T("idinventario") > 1 Then
                        Tabla.Rows.Add(T("iddetalle"), "A", T("cantidad"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"))
                    Else
                        Tabla.Rows.Add(T("iddetalle"), "P", T("cantidad"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"))
                    End If
                Else
                    If T("idinventario") > 1 Then
                        Tabla.Rows.Add(T("iddetalle"), "A", T("cantidad"), T("descripcion"), "0.00", Format(T("precio"), "0.00"))
                    Else
                        Tabla.Rows.Add(T("iddetalle"), "P", T("cantidad"), T("descripcion"), "0.00", Format(T("precio"), "0.00"))
                    End If
                End If
            End While
            T.Close()

            DGDetalles.DataSource = Tabla
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).Visible = False

            DGDetalles.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(2).Width = 80
            DGDetalles.Columns(4).Width = 100
            DGDetalles.Columns(5).Width = 100
            DGDetalles.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
            SacaTotal()
            Label14.Text = "Líneas: " + DGDetalles.RowCount.ToString
            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            DGDetalles.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        
    End Sub
    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Select Case Tipo
                    Case 0 'remision
                        Remision.DaTotal(Remision.ID, 2)
                        TotalVenta = Remision.TotalVenta
                        Label2.Text = "Total: $" + Format(Remision.TotalVenta, "#,###,##0.00")
                    Case 1 'pedido
                        Pedido.DaTotal(Pedido.ID, 2)
                        TotalVenta = Pedido.TotalVenta
                        Label2.Text = "Total: $" + Format(Pedido.TotalVenta, "#,###,##0.00")
                    Case 2 'cotizacion
                        Cotizacion.DaTotal(Cotizacion.ID, 2)
                        TotalVenta = Cotizacion.TotalVenta
                        Label2.Text = "Total: $" + Format(Cotizacion.TotalVenta, "#,###,##0.00")
                End Select
                'Label3.Text = "Recibido: $0.00 Cambio: $0.00"
                Label11.Text = "$0.00"
                Label10.Text = "$0.00"
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If EstadoVentana <> 5 Then
            buscaArticuloBoton()
        Else
            Dim Idinv As Integer = 0
            Select Case Tipo
                Case 0
                    If RemisionDetalles.ID <> 0 Then Idinv = RemisionDetalles.Idinventario
                Case 1
                    If PedidoDetalles.ID <> 0 Then Idinv = PedidoDetalles.Idinventario
                Case 2
                    If CotizacionDetalles.ID <> 0 Then Idinv = CotizacionDetalles.Idinventario
            End Select
            Dim SP As New frmSelectorPrecios(Idinv)
            If SP.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox1.Text = SP.Precio.ToString
                TextBox1.SelectAll()
            End If
            SP.Dispose()
        End If
        TextBox1.Focus()
    End Sub
    Private Sub buscaArticuloBoton()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.Articulo
        Dim B As New frmBuscadorGrande(TipodeBusqueda, idAlmacen)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            Select Case B.Tipo
                Case "I"
                    Articulo = B.Inventario
                    
            End Select
            TextBox1.BackColor = Color.White
            Label4.BackColor = Color.Transparent
            EstadoVentana = 1
            PresionaEnter()
        End If
    End Sub
    Private Sub CentraControles()
        Label1.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
        Label3.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 695)
        Label9.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
        Button2.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 901)
        Label2.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 136)
        DGDetalles.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
        Label4.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 4)
        TextBox1.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
        Button1.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 862)
        Label5.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2)
        Label6.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 75)
        Label7.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 736)
        Label8.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 856)
        PictureBox1.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 6)
        Label12.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 352)
        Label11.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 479)
        Label13.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 669)
        Label10.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 777)
        Button3.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 21)
        Button4.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 78)
        Button5.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 135)
        Button10.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 192)
        Button6.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 247)
        Button11.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 303)
        Button9.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 361)
        Button8.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 418)
        Button7.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 475)
        Button12.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 531)
        Button13.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 587)
        Button14.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 643)
        Button15.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 698)
        Label14.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 759)
    End Sub

    Private Sub frmPuntodeVenta_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        CentraControles()
    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        Try
            Select Case Tipo
                Case 0 'remision
                    RemisionDetalles.ID = DGDetalles.Item(0, e.RowIndex).Value
                    RemisionDetalles.LlenaDatos()
                Case 1 'pedido
                    PedidoDetalles.ID = DGDetalles.Item(0, e.RowIndex).Value
                    PedidoDetalles.LlenaDatos()
                Case 2 'cotizacion
                    CotizacionDetalles.ID = DGDetalles.Item(0, e.RowIndex).Value
                    CotizacionDetalles.LlenaDatos()
            End Select
            'ConsultaDetalles()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        TextBox1.Focus()
    End Sub

    Private Sub DGDetalles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub
    

    Private Sub Imprimir()
        Dim RutaPDF As String = ""
        TextBox1.Enabled = False
        Try
            Dim SA As New dbSucursalesArchivos
            Dim Impresora As String = ""
            Select Case Tipo
                Case 0
                    ImprimirRemisiones(O.TituloOriginalRemision, False, False)
                    If O.Copiaflujorem = 1 Then
                        ImprimirRemisiones(O.TituloOriginalRemision, True, True)
                    End If
                    Dim fdp As New dbFormasdePagoRemisiones(Remision.idForma, MySqlcon)
                    If (O.ActivarCopiaRemision = 1 And fdp.Tipo = dbFormasdePagoRemisiones.Tipos.Contado) Or (O.ActivarCopiaRemisionCredito = 1 And fdp.Tipo = dbFormasdePagoRemisiones.Tipos.Credito) Then
                        ImprimirRemisiones(O.TituloCopiaRemision, True, False)
                    End If
                    If (O.ActivarCopia2Remision = 1 And fdp.Tipo = dbFormasdePagoRemisiones.Tipos.Contado) Or (O.ActivarCopiaRemisionCredito2 = 1 And fdp.Tipo = dbFormasdePagoRemisiones.Tipos.Credito) Then
                        ImprimirRemisiones(O.TituloCopia2Remision, True, False)
                    End If
                Case 1
                    ImprimirPedidos()
                Case 2
                    ImprimirCotizaciones()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        TextBox1.Enabled = True
    End Sub

    Private Sub ImprimirRemisiones(pTitulo As String, pEsCopia As Boolean, pFlujo As Boolean)
        Try
            'Dim Compra As New dbCompras(pIdRemision, MySqlcon)
            ImpDoc.IdSucursal = Remision.IdSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.VentaRemision
            ImpDoc.TipoDocumentoT = TiposDocumentos.VentaRemision + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.VentaRemision
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.RemisionesPDF
            ImpDoc.Inicializar(pFlujo)
            LlenaNodosImpresionRemision(pTitulo)
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Remision.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Remision.Fecha), "yyyy") + "\" + Format(CDate(Remision.Fecha), "MM") + "\")
            If O._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(CDate(Remision.Fecha), "yyyy") + "\" + Format(CDate(Remision.Fecha), "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            If pEsCopia Then
                PrintDocument1.DocumentName = "REM_COPIA_" + Remision.Serie + Remision.Folio.ToString("0000")
            Else
                PrintDocument1.DocumentName = "REM_" + Remision.Serie + Remision.Folio.ToString("0000")
            End If
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub ImprimirCotizaciones()
        Try
            'Dim Compra As New dbCompras(pIdRemision, MySqlcon)
            ImpDoc.IdSucursal = Cotizacion.IdSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.VentaCotizacion
            ImpDoc.TipoDocumentoT = TiposDocumentos.VentaCotizacion + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.VentaCotizacion
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.CotizacionesPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresionCotizacion()
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Cotizacion.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Cotizacion.Fecha), "yyyy") + "\" + Format(CDate(Cotizacion.Fecha), "MM") + "\")
            If O._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(CDate(Cotizacion.Fecha), "yyyy") + "\" + Format(CDate(Cotizacion.Fecha), "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "COT_" + Cotizacion.Serie + Cotizacion.Folio.ToString("0000")
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ImprimirPedidos()
        Try
            'Dim Compra As New dbCompras(pIdRemision, MySqlcon)
            ImpDoc.IdSucursal = Pedido.IdSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.VentaPedido
            ImpDoc.TipoDocumentoT = TiposDocumentos.VentaPedido + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.VentaPedido
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.PedidosPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresionPedidos()
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Pedido.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Pedido.Fecha), "yyyy") + "\" + Format(CDate(Pedido.Fecha), "MM") + "\")
            If O._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(CDate(Pedido.Fecha), "yyyy") + "\" + Format(CDate(Pedido.Fecha), "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "COT_" + Cotizacion.Serie + Cotizacion.Folio.ToString("0000")
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub LlenaNodosImpresionCotizacion()

        Dim V As New dbVentasCotizaciones(Cotizacion.ID, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        Dim O As New dbOpciones(MySqlcon)
        Dim Vendedor As New dbVendedores(V.IdVendedor, MySqlcon)
        Dim TotalDescuento As Double = 0
        V.DaTotal(Cotizacion.ID, 2)

        ImpDoc.ImpND.Clear()

        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "direccion", Sucursal.Direccion, 0), "direccion")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "noexterior", Sucursal.NoExterior, 0), "noexterior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nointerior", Sucursal.NoInterior, 0), "nointerior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "colonia", Sucursal.Colonia, 0), "colonia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudad", Sucursal.Ciudad, 0), "ciudad")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "estado", Sucursal.Estado, 0), "estado")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cp", Sucursal.CP, 0), "cp")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "tel", Sucursal.Telefono, 0), "tel")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "email", Sucursal.Email, 0), "email")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "referencia", Sucursal.ReferenciaDomicilio, 0), "referencia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "municipio", Sucursal.Municipio, 0), "municipio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "vendedor", Vendedor.Nombre, 0), "vendedor")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "usuario", V.Usuario, 0), "usuario")
        If V.Cliente.DireccionFiscal = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio, 0), "refcliente")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion2, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior2, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior2, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia2, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP2, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad2, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado2, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio2, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")


        ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "foliobarras", "*%" + V.Serie + V.Folio.ToString + "%*", 0), "foliobarras")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "fecha", Replace(Format(CDate(V.Fecha), O.FormatoFechaPv), "/", "-"), 0), "fecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        'ImpDoc.ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")


        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbVentasCotizacionesInventario(MySqlcon)
        DR = VI.ConsultaReader(Cotizacion.ID)
        ImpDoc.ImpNDD.Clear()
        ImpDoc.CuantosRenglones = 0
        Dim Cont As Integer = 0
        While DR.Read
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            'ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), 0), "cantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipocantidad"), 0), "tipocantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciouiva", Format((DR("precio") / DR("cantidad")) * (1 + (DR("iva") + DR("ieps") - DR("ivaretenido")) / 100), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciouiva" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importe" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(DR("precio") * (1 + (DR("iva") + DR("ieps") - DR("ivaretenido")) / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))
            If DR("cantidad") <> 0 Then
                Dim Desc As Double
                Desc = (DR("precio") / (1 - DR("descuento") / 100))
                TotalDescuento += Desc - DR("precio")
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(O.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - DR("precio"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                'Vo = Vd / ( 1 - (Por/100))
            Else
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
            End If

            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()

        ImpDoc.ImpND.Add(New NodoImpresionN("", "peso", Format(V.TotalPeso, "#,##0.00") + "Kg.", 0), "peso")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtotal + TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsindesc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totaldesc", Format(TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totaldesc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totalcantidad", V.DaTotalCantidad(Cotizacion.ID).ToString, 0), "totalcantidad")
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(Cotizacion.ID)
        ImpDoc.ImpNDDi.Clear()
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
            End If
        End While
        DR.Close()
        Cont = 0
        For Each I As Double In Ivas
            ImpDoc.ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        Next

        'If V.ISR <> 0 Then
        '    ImpDoc.ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "isr", Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    Cont += 1
        'End If
        'If V.IvaRetenido <> 0 Then
        '    ImpDoc.ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "ivaretenido", Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    Cont += 1
        'End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, O._formatoTotal).PadLeft(O.Espaciototal), 0), "total")
        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And O._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If
        'Dim f As New StringFunctions
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), 2), 0), "totalletra")
        Dim CL As New CLetras
        If V.TotalVenta >= 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(V.TotalVenta, 2, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(V.TotalVenta * -1, 2, GlobalIdiomaLetras), 0), "totalletra")
        End If
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
        If V.Estado = Estados.Cancelada Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        ImpDoc.Posicion = 0
        'Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        'CodigoBidimensional = CB.Encode("?&re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, "$#,###,##0.00") + "&id=" + V.uuid, System.Text.Encoding.Default)
        ImpDoc.NumeroPagina = 1
    End Sub
    Private Sub LlenaNodosImpresionPedidos()

        Dim V As New dbVentasPedidos(Pedido.ID, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(Pedido.ID, 2)
        Dim TotalDescuento As Double = 0
        Dim O As New dbOpciones(MySqlcon)
        Dim Vendedor As New dbVendedores(V.IdVendedor, MySqlcon)
        ImpDoc.ImpND.Clear()

        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "direccion", Sucursal.Direccion, 0), "direccion")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "noexterior", Sucursal.NoExterior, 0), "noexterior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nointerior", Sucursal.NoInterior, 0), "nointerior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "colonia", Sucursal.Colonia, 0), "colonia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudad", Sucursal.Ciudad, 0), "ciudad")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "estado", Sucursal.Estado, 0), "estado")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cp", Sucursal.CP, 0), "cp")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "tel", Sucursal.Telefono, 0), "tel")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "email", Sucursal.Email, 0), "email")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "referencia", Sucursal.ReferenciaDomicilio, 0), "referencia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "municipio", Sucursal.Municipio, 0), "municipio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "vendedor", Vendedor.Nombre, 0), "vendedor")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "usuario", V.Usuario, 0), "usuario")
        If V.Cliente.DireccionFiscal = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio, 0), "refcliente")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion2, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior2, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior2, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia2, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP2, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad2, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado2, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio2, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "foliobarras", "*$" + V.Serie + V.Folio.ToString + "$*", 0), "foliobarras")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "fecha", Replace(Format(CDate(V.Fecha), O.FormatoFechaPv), "/", "-"), 0), "fecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        'ImpDoc.ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")


        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbVentasPedidosInventario(MySqlcon)
        DR = VI.ConsultaReader(Pedido.ID)
        ImpDoc.ImpNDD.Clear()
        ImpDoc.CuantosRenglones = 0
        Dim Cont As Integer = 0
        While DR.Read
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipocantidad"), 0), "tipocantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciouiva", Format((DR("precio") / DR("cantidad")) * (1 + (DR("iva") + DR("ieps") - DR("ivaretenido")) / 100), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciouiva" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importe" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(DR("precio") * (1 + (DR("iva") + DR("ieps") - DR("ivaretenido")) / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))
            If DR("cantidad") <> 0 Then
                Dim Desc As Double
                Desc = (DR("precio") / (1 - DR("descuento") / 100))
                TotalDescuento += Desc - DR("precio")
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(O.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - DR("precio"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                'Vo = Vd / ( 1 - (Por/100))
            Else
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
            End If

            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()

        ImpDoc.ImpND.Add(New NodoImpresionN("", "peso", Format(V.TotalPeso, "#,##0.00") + "Kg.", 0), "peso")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtotal + TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsindesc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totaldesc", Format(TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totaldesc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totalcantidad", V.DaTotalCantidad(Pedido.ID).ToString, 0), "totalcantidad")
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(Pedido.ID)
        ImpDoc.ImpNDDi.Clear()
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
            End If
        End While
        DR.Close()
        Cont = 0
        For Each I As Double In Ivas
            ImpDoc.ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        Next
        'If V.ISR <> 0 Then
        '    ImpDoc.ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "isr", Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    Cont += 1
        'End If
        'If V.IvaRetenido <> 0 Then
        '    ImpDoc.ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "ivaretenido", Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    Cont += 1
        'End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, O._formatoTotal).PadLeft(O.Espaciototal), 0), "total")
        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And O._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If
        'Dim f As New StringFunctions
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), 2), 0), "totalletra")
        Dim CL As New CLetras
        If V.TotalVenta >= 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(V.TotalVenta, 2, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(V.TotalVenta * -1, 2, GlobalIdiomaLetras), 0), "totalletra")
        End If
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
        If V.Estado = Estados.Cancelada Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        ImpDoc.Posicion = 0
        'Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        'CodigoBidimensional = CB.Encode("?&re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, "$#,###,##0.00") + "&id=" + V.uuid, System.Text.Encoding.Default)
        ImpDoc.NumeroPagina = 1
    End Sub

    Private Sub LlenaNodosImpresionRemision(pTitulo As String)
        Dim O As New dbOpciones(MySqlcon)
        Dim V As New dbVentasRemisiones(Remision.ID, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        Dim TotalDescuento As Double = 0
        V.DaTotal(Remision.ID, 2)

        ImpDoc.ImpND.Clear()

        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "direccion", Sucursal.Direccion, 0), "direccion")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "noexterior", Sucursal.NoExterior, 0), "noexterior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nointerior", Sucursal.NoInterior, 0), "nointerior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "colonia", Sucursal.Colonia, 0), "colonia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudad", Sucursal.Ciudad, 0), "ciudad")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "estado", Sucursal.Estado, 0), "estado")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cp", Sucursal.CP, 0), "cp")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "tel", Sucursal.Telefono, 0), "tel")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "email", Sucursal.Email, 0), "email")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "referencia", Sucursal.ReferenciaDomicilio, 0), "referencia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "municipio", Sucursal.Municipio, 0), "municipio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "usuario", V.Usuario, 0), "usuario")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
        If V.Cliente.DireccionFiscal = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio, 0), "refcliente")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion2, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior2, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior2, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia2, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP2, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad2, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado2, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio2, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "titulocopia", pTitulo, 0), "titulocopia")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")
        'ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        'ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        'ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "fecha", Replace(Format(CDate(V.Fecha), O.FormatoFechaPv), "/", "-"), 0), "fecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        'ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        'ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        'ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")
        'ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        'ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        'ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")
        If V.PorSurtir = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "porsurtir", "SURTIDO", 0), "porsurtir")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "porsurtir", "POR SURTIR", 0), "porsurtir")
        End If

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbVentasRemisionesInventario(MySqlcon)

        ImpDoc.ImpND.Add(New NodoImpresionN("", "almacen", VI.DaAlmacen(V.ID), 0), "almacen")
        DR = VI.ConsultaReader(Remision.ID, 1, 1, O._DetalleKits)
        ImpDoc.ImpNDD.Clear()
        ImpDoc.CuantosRenglones = 0
        Dim Cont As Integer = 0
        Dim CodigoBarras As iTextSharp.text.pdf.Barcode128 = New iTextSharp.text.pdf.Barcode128
        While DR.Read
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))

            If DR("cantidad") <> 0 Then
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave2", DR("clave2"), 0), "clave2" + Format(Cont, "000"))
            Else
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave2", "", 0), "clave2" + Format(Cont, "000"))
            End If
            Dim Nimagen As NodoImpresionN
            Nimagen = New NodoImpresionN("", "codigobarras1", "", 0)
            CodigoBarras.Code = DR("clave")
            Try
                Nimagen.Imagen = CodigoBarras.CreateDrawingImage(Color.Black, Color.White)
            Catch ex As Exception
            End Try
            ImpDoc.ImpNDD.Add(Nimagen, "codigobarras1" + Format(Cont, "000"))
            Nimagen = New NodoImpresionN("", "codigobarras2", "", 0)
            CodigoBarras.Code = DR("clave2")
            Try
                Nimagen.Imagen = CodigoBarras.CreateDrawingImage(Color.Black, Color.White)
            Catch ex As Exception
            End Try
            ImpDoc.ImpNDD.Add(Nimagen, "codigobarras2" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), 0), "cantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipocantidad"), 0), "tipocantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciouiva", Format((DR("precio") / DR("cantidad")) * (1 + (DR("iva") + DR("ieps") - DR("ivaretenido")) / 100), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciouiva" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importe" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(DR("precio") * (1 + (DR("iva") + DR("ieps") - DR("ivaretenido")) / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))

            If DR("iva") = 0 Then
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tieneiva", "", 0), "tieneiva" + Format(Cont, "000"))
            Else
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tieneiva", "*", 0), "tieneiva" + Format(Cont, "000"))
            End If
            If DR("cantidad") <> 0 Then
                Dim Desc As Double
                Desc = (DR("precio") / (1 - DR("descuento") / 100))
                TotalDescuento += Desc - DR("precio")
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(O.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - DR("precio"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ieps", Format(DR("precio") * (DR("ieps") / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "ieps" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", Format(DR("precio") * (1 + DR("ivaretenido") / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "ivaRetenido" + Format(Cont, "000"))
                'Vo = Vd / ( 1 - (Por/100))
            Else
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ieps", "", 0), "ieps" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", "", 0), "ivaRetenido" + Format(Cont, "000"))
            End If

            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()

        If V.IdVentaR = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "folioref", "", 0), "folioref")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "folioref", V.FolioRef, 0), "folioref")
        End If

        ImpDoc.ImpND.Add(New NodoImpresionN("", "Totalieps", Format(V.TotalIeps, O._formatoIva).PadLeft(O.EspacioIva), 0), "Totalieps")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "TotalivaRetenido", Format(V.TotalIvaRetenidoConceptos, O._formatoIva).PadLeft(O.EspacioIva), 0), "TotalivaRetenido")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totalofertas", Format(V.TotalOfertas, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totalofertas")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "peso", Format(V.TotalPeso, "#,##0.00") + "Kg.", 0), "peso")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtototal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtototal + TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsindesc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totaldesc", Format(TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totaldesc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totalcantidad", V.DaTotalCantidad(Remision.ID).ToString, 0), "totalcantidad")
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(Remision.ID)
        ImpDoc.ImpNDDi.Clear()
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
            End If
        End While
        DR.Close()
        Cont = 0
        For Each I As Double In Ivas
            ImpDoc.ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        Next
        'If V.ISR <> 0 Then
        '    ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "isr", Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    Cont += 1
        'End If
        'If V.IvaRetenido <> 0 Then
        '    ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "ivaretenido", Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    Cont += 1
        'End If
        Dim Vendedor As New dbVendedores(V.IdVEndedor, MySqlcon)
        ImpDoc.ImpND.Add(New NodoImpresionN("", "vendedor", Vendedor.Nombre, 0), "vendedor")
        Dim FP As New dbFormasdePagoRemisiones(V.idForma, MySqlcon)
        Dim strMetodos As String = ""
        Dim MeP As New dbVentasAddMetodos(MySqlcon)
        DR = MeP.ConsultaReader(1, V.ID)
        While DR.Read()
            If strMetodos <> "" Then strMetodos += vbNewLine
            strMetodos += DR("nombre")
        End While
        DR.Close()
        ImpDoc.ImpND.Add(New NodoImpresionN("", "metodopago", strMetodos, 0), "metodopago")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, O._formatoTotal).PadLeft(O.Espaciototal), 0), "total")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Recibido:", Format(Recibido, O._formatoTotal).PadLeft(O.Espaciototal), 0), "recibido")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Cambio:", Format(Cambio, O._formatoTotal).PadLeft(O.Espaciototal), 0), "cambio")

        Dim Abonado = V.DaTotalAbonado(V.ID)
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Abonado:", Format(Abonado, O._formatoTotal).PadLeft(O.Espaciototal), 0), "totalabonado")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Restante:", Format(V.TotalVenta - Abonado, O._formatoTotal).PadLeft(O.Espaciototal), 0), "restante")

        If FP.Tipo = dbFormasdePagoRemisiones.Tipos.Contado Or FP.Tipo = dbFormasdePagoRemisiones.Tipos.Otro Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "condicionpago", "CONTADO", 0), "condicionpago")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "diascredito", "", 0), "diascredito")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "limitecredito", "", 0), "limitecredito")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "condicionpago", "CRÉDITO", 0), "condicionpago")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "diascredito", V.Cliente.CreditoDias.ToString + " Días.", 0), "diascredito")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "limitecredito", Format(DateAdd(DateInterval.Day, V.Cliente.CreditoDias, CDate(V.Fecha)), "yyyy-MM-dd"), 0), "limitecredito")
        End If

        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And O._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If
        'Dim f As New StringFunctions
        'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), 2), 0), "totalletra")
        Dim CL As New CLetras
        If V.TotalVenta >= 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(V.TotalVenta, 2, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(V.TotalVenta * -1, 2, GlobalIdiomaLetras), 0), "totalletra")
        End If
        'ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
        'ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
        If V.Estado = Estados.Cancelada Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        ImpDoc.Posicion = 0
        'Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        'CodigoBidimensional = CB.Encode("?&re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, "$#,###,##0.00") + "&id=" + V.uuid, System.Text.Encoding.Default)
        ImpDoc.NumeroPagina = 1
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
       
        'Select Case DocaImprimir
        '    Case TiposDocumentos.VentaCotizacion
        '        DibujaPaginaNCotizaciones(e.Graphics)
        '    Case TiposDocumentos.VentaPedido
        '        DibujaPaginaNPedidos(e.Graphics)
        '    Case TiposDocumentos.VentaRemision
        '        DibujaPaginaNRemisiones(e.Graphics)
        'End Select
        'If MasPaginas = True Or NumeroPagina > 2 Then
        '    e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00") + "/" + Format(CuantaY, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        'End If


        'e.HasMorePages = MasPaginas

        ImpDoc.DibujaPaginaN(e.Graphics)
        If ImpDoc.MasPaginas = True Or ImpDoc.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(ImpDoc.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If
        'If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
        '    e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
        'End If
        e.HasMorePages = ImpDoc.MasPaginas
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        Dim pvs As New frmPuntodeVentaSettingsB(idSucursal, idCliente, IdCaja, IdVendedor, IdConceptoDefault)
        pvs.ShowDialog()
        If pvs.DialogResult = Windows.Forms.DialogResult.OK Then
            idSucursal = pvs.IdSucursal
            idCliente = pvs.IdCliente
            IdCaja = pvs.IdCaja
            CajaG = New dbCajas(IdCaja, MySqlcon)
            IdVendedor = pvs.IdVendedor
            idAlmacen = pvs.IdAlmacen
            Idconcepto = pvs.IdConcepto
            idPrecioLista = pvs.idListaPrecio
            Label1.Text = "Vendedor: " + pvs.NombreVendedor + " Caja: " + pvs.NombreCaja
            Label9.Text = "Sucursal: " + pvs.NombreSucursal + " Cliente: " + pvs.NombreCliente
            Label3.Text = "Fecha: " + Format(Date.Now, "dd/MMM/yyyy").ToUpper
            Opc.LlenaDatos(Tipo, idSucursal)
        End If
        pvs.Dispose()
        TextBox1.Focus()
    End Sub

  
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Lectura = SerialPort1.ReadExisting
        If Lectura <> "" And Veces <= 200 Then
            TextBox1.Text = "=" + Replace(Replace(Replace(Replace(Lectura, "+", ""), "oz", ""), "lb", ""), "kg", "").Trim '+ System.Text.RegularExpressions.Regex.Replace(Lectura, "[^\d]", "") 
            EstadoVentana = 2
            PresionaEnter()
            If PidePrecio And HaciendoCambio = False And GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CambiodePrecio, PermisosN.Secciones.PuntodeVenta) = True Then
                EstadoVentana = 5
                TextBox1.BackColor = ColorAmarillo
                Label4.BackColor = ColorAmarillo
                Label4.Text = "Indicar Precio:"
                TextBox1.Text = Precio.ToString
                TextBox1.SelectAll()
            Else
                EstadoVentana = 2
                TextBox1.BackColor = Color.White
                Label4.BackColor = Color.Transparent
                'Label4.Text = ""
            End If
            Veces = 0
            TextBox1.Enabled = True
            Timer1.Enabled = False
            TextBox1.Focus()
            SerialPort1.Close()
        Else
            If Veces > 200 Then
                TextBox1.Enabled = True
                Timer1.Enabled = False
                TextBox1.Focus()
                SerialPort1.Close()
                If PidePrecio And GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CambiodePrecio, PermisosN.Secciones.PuntodeVenta) = True Then
                    EstadoVentana = 5
                    TextBox1.BackColor = ColorAmarillo
                    Label4.BackColor = ColorAmarillo
                    Label4.Text = "Indicar Precio:"
                    TextBox1.Text = Format(Precio.ToString, "0.00")
                    TextBox1.SelectAll()
                Else
                    EstadoVentana = 2
                    TextBox1.BackColor = Color.White
                    Label4.BackColor = Color.Transparent
                    'Label4.Text = ""
                End If
                Veces = 0
            End If
        End If
        Veces += 1
    End Sub
    Private Sub AgregaRefencia(ByVal pFolio As String, ByVal pTipo As Byte)
        'If ReferenciaAgregada = False Then

        Select Case pTipo
            Case 1
                'cotizaciones
                Dim iCot As New dbVentasCotizaciones(MySqlcon)
                iCot.ID = iCot.DaId(pFolio)
                Dim IDCheck As Integer = 0
                IDCheck = IdsDocumentos.Busca(iCot.ID, True)
                If IDCheck >= 0 Then
                    If TiposReferencias.Valor(IDCheck) = 1 Then
                        IDCheck = 1
                    Else
                        IDCheck = 0
                    End If
                Else
                    IDCheck = 0
                End If
                If iCot.ID <> 0 And IDCheck = 0 Then
                    Remision.AgregarDetallesReferencia(Remision.ID, iCot.ID, 0, idAlmacen)
                    ConsultaDetalles()
                    'ReferenciaAgregada = True
                    IdsDocumentos.Agregar(iCot.ID)
                    TiposReferencias.Agregar(pTipo)
                    iCot.LlenaDatos()
                    If idCliente <> iCot.IdCliente Then
                        idCliente = iCot.IdCliente
                        Dim Cl As New dbClientes(idCliente, MySqlcon)
                        idPrecioLista = Cl.IdLista
                        Dim su As New dbSucursales(idSucursal, MySqlcon)
                        Label9.Text = "Sucursal: " + su.Nombre + " Cliente: " + Cl.Nombre
                    End If
                End If

            Case 2
                'pedidos
                Dim iPed As New dbVentasPedidos(MySqlcon)
                iPed.ID = iPed.DaId(pFolio)
                Dim IDCheck As Integer = 0
                IDCheck = IdsDocumentos.Busca(iPed.ID, True)
                If IDCheck >= 0 Then
                    If TiposReferencias.Valor(IDCheck) = 2 Then
                        IDCheck = 1
                    Else
                        IDCheck = 0
                    End If
                Else
                    IDCheck = 0
                End If
                If iPed.ID <> 0 And IDCheck = 0 Then
                    Remision.AgregarDetallesReferencia(Remision.ID, iPed.ID, 1, idAlmacen)
                    ConsultaDetalles()
                    IdsDocumentos.Agregar(iPed.ID)
                    TiposReferencias.Agregar(pTipo)
                    iPed.LlenaDatos()
                    If idCliente <> iPed.IdCliente Then
                        idCliente = iPed.IdCliente
                        Dim Cl As New dbClientes(idCliente, MySqlcon)
                        idPrecioLista = Cl.IdLista
                        Dim su As New dbSucursales(idSucursal, MySqlcon)
                        Label9.Text = "Sucursal: " + su.Nombre + " Cliente: " + Cl.Nombre
                    End If
                End If
        End Select
        'End If

        TextBox1.Text = ""
    End Sub

    Private Sub DGDetalles_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellDoubleClick
        TextBox1.Focus()
    End Sub

    Private Sub DGDetalles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGDetalles.Click
        TextBox1.Focus()
    End Sub

    Private Sub DGDetalles_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGDetalles.DoubleClick
        TextBox1.Focus()
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        TextBox1.Focus()
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        TextBox1.Focus()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If IsNumeric(TextBox1.Text) Then
            If CDbl(TextBox1.Text) < 0 Then TextBox1.Text = CStr(CDbl(TextBox1.Text) * -1)
            TextBox1.Text = "+" + TextBox1.Text
            PresionaEnter()
        Else
            TextBox1.Text = "+"
            PresionaEnter()
        End If
        TextBox1.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If IsNumeric(TextBox1.Text) Then
            If CDbl(TextBox1.Text) < 0 Then TextBox1.Text = CStr(CDbl(TextBox1.Text) * -1)
            TextBox1.Text = "-" + TextBox1.Text
            PresionaEnter()
        Else
            TextBox1.Text = "-"
            PresionaEnter()
        End If
        TextBox1.Focus()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If IsNumeric(TextBox1.Text) Then
            TextBox1.Text = "=" + TextBox1.Text
            PresionaEnter()
        End If
        TextBox1.Focus()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If EstadoVentana = 2 Or EstadoVentana = 0 Then
            EstadoVentana = 6
            TextBox1.BackColor = ColorRojo
            Label4.BackColor = ColorRojo
            Label4.Text = "Folio a cancelar:"
        End If
        TextBox1.Focus()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If MsgBox("¿Nueva venta? Se perderán los datos no guardados.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            Nuevo()
        End If
        TextBox1.Focus()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        EstadoVentana = 7
        Try
            If SerialPort1.IsOpen = False Then SerialPort1.Open()
            SerialPort1.Write(Chr(CInt(Secuencia)))
            Lectura = ""
            Veces = 0
            HaciendoCambio = True
            TextBox1.Enabled = False
            Timer1.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
            If SerialPort1.IsOpen Then SerialPort1.Close()
            EstadoVentana = 2
            TextBox1.BackColor = Color.White
            Label4.BackColor = Color.Transparent
        End Try
        TextBox1.Focus()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.ReportesVer, PermisosN.Secciones.PuntodeVenta) = True Then
            Dim fimp As New frmPuntodeVentaReportes
            fimp.ShowDialog()
            fimp.Dispose()
        End If
        TextBox1.Focus()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        CambiodePrecio()
    End Sub
    Private Sub CambiodePrecio()
        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CambiodePrecio, PermisosN.Secciones.PuntodeVenta) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Try
            If EstadoVentana = 2 Then
                EstadoVentana = 5
                TextBox1.BackColor = ColorAmarillo
                Label4.BackColor = ColorAmarillo
                Label4.Text = "Indicar Precio:"
                Dim PrecioNeto As Byte
                Dim Precio As Double
                Select Case Tipo
                    Case 0 'remision
                        Precio = RemisionDetalles.Precio / RemisionDetalles.Cantidad
                        PrecioNeto = RemisionDetalles.Inventario.PrecioNeto
                        If PrecioNeto = 1 Then
                            Precio = Precio * (1 + RemisionDetalles.Iva / 100)
                        End If
                        'RemisionDetalles.Guardar(Remision.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, idAlmacen, Articulo.Iva, 0, 1, 0)
                    Case 1 'pedido
                        Precio = PedidoDetalles.Precio / PedidoDetalles.Cantidad
                        PrecioNeto = PedidoDetalles.Inventario.PrecioNeto
                        If PrecioNeto = 1 Then
                            Precio = Precio * (1 + PedidoDetalles.Iva / 100)
                        End If
                        'PedidoDetalles.Guardar(Pedido.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, Articulo.Iva, 0, 1)
                    Case 2 'cotizacion
                        Precio = CotizacionDetalles.Precio / CotizacionDetalles.Cantidad
                        PrecioNeto = CotizacionDetalles.Inventario.PrecioNeto
                        If PrecioNeto = 1 Then
                            Precio = Precio * (1 + CotizacionDetalles.Iva / 100)
                        End If
                        'CotizacionDetalles.Guardar(Cotizacion.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, Articulo.Iva, 0, 1)
                End Select
                TextBox1.Text = Format(Precio, "0.00")
                TextBox1.Focus()
                TextBox1.SelectAll()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub HaceDescuento()
        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.HacerDescuento, PermisosN.Secciones.PuntodeVenta) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If EstadoVentana = 2 Then
            EstadoVentana = 7
            Descuento = 0
            TextBox1.BackColor = ColorAmarillo2
            Label4.BackColor = ColorAmarillo2
            Label4.Text = "Indicar %Descuento:"
            Dim PrecioNeto As Byte
            Dim Precio As Double
            TextBox1.Text = ""
            Select Case Tipo
                Case 0 'remision
                    Precio = RemisionDetalles.Precio / RemisionDetalles.Cantidad
                    PrecioNeto = RemisionDetalles.Inventario.PrecioNeto
                    If PrecioNeto = 1 Then
                        Precio = Precio * (1 + RemisionDetalles.Iva / 100)
                    End If
                    'RemisionDetalles.Guardar(Remision.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, idAlmacen, Articulo.Iva, 0, 1, 0)
                Case 1 'pedido
                    Precio = PedidoDetalles.Precio / PedidoDetalles.Cantidad
                    PrecioNeto = PedidoDetalles.Inventario.PrecioNeto
                    If PrecioNeto = 1 Then
                        Precio = Precio * (1 + PedidoDetalles.Iva / 100)
                    End If
                    'PedidoDetalles.Guardar(Pedido.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, Articulo.Iva, 0, 1)
                Case 2 'cotizacion
                    Precio = CotizacionDetalles.Precio / CotizacionDetalles.Cantidad
                    PrecioNeto = CotizacionDetalles.Inventario.PrecioNeto
                    If PrecioNeto = 1 Then
                        Precio = Precio * (1 + CotizacionDetalles.Iva / 100)
                    End If
                    'CotizacionDetalles.Guardar(Cotizacion.ID, Articulo.ID, 1, Temp, IP.IdMoneda, Articulo.Nombre, Articulo.Iva, 0, 1)
            End Select
            PrecioParaDescuento = Precio
            'TextBox1.Text = Format(Precio, "0.00")
            TextBox1.Focus()
            'TextBox1.SelectAll()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        HaceDescuento()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasApartadosVer, PermisosN.Secciones.Ventas) = True Then
            Dim f As New frmVentasApartados(0, 0, 0, 0)
            f.ShowDialog()
            f.Dispose()
        End If
    End Sub

    '*********************************************DESCUENTOS********************************************

    '------------REMISIONES------------'
#Region " REMISIONES "
    Private Sub hayDescuento(ByVal pIdVenta As Integer, ByVal pIdinventario As Integer, ByVal pCantidad As Double, ByVal pPrecio As Double, ByVal pIdMoneda As Integer, ByVal pDescripcion As String, ByVal pIdAlmacen As Integer, ByVal pIva As Double, ByVal pDescuento As Double, ByVal pIdVariante As Integer, ByVal pIdServicio As Integer)

        Dim idDescuento As Integer
        idDescuento = P.HayDescuento(Articulo.ID, fechaFormato() + " " + horaFormato(), idSucursal)
        Dim TablaDesc As DataTable
        Dim des As Double = 0
        Dim descripcion As String = ""
        'Dim nombreProducto As String = ""

        If idDescuento = 0 Then
            'No hay descuento
            tieneDesc = False
        Else
            tieneDesc = True
            TablaDesc = P.tablaDesc(idDescuento)
            If TablaDesc.Rows(0)(9).ToString() <> "Promocion" Then

                If TablaDesc.Rows(0)(9).ToString() = "Porcentaje" Then
                    des = TotalPorcentaje(precioArticulo, Integer.Parse(TablaDesc.Rows(0)(2).ToString()))
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: " + nombreProducto + " " + TablaDesc.Rows(0)(2).ToString() + " %"
                Else
                    des = Double.Parse(TablaDesc.Rows(0)(2).ToString())
                    des = des * canProducto
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: $" + TablaDesc.Rows(0)(2).ToString() + " P/U" + " " + nombreProducto
                End If
                If Tipo = 0 Then
                    RemisionDetalles.GuardarDescuento(pIdVenta, 1, canProducto, des, pIdMoneda, descripcion, pIdAlmacen, pIva, 0, pIdVariante, pIdServicio, Articulo.ieps, Articulo.ivaRetenido, Articulo.TipoContenido.ID)
                    P.GuardarDesc(CD.UltomoRegistro(), idDescuento, Remision.Folio, tipoDescuento)
                End If
                If Tipo = 1 Then
                    PedidoDetalles.GuardarDescuento(Pedido.ID, 1, canProducto, des, pIdMoneda, descripcion, Articulo.Iva, 0, 1, Articulo.ieps, Articulo.ivaRetenido)
                    P.GuardarDesc(PedidoDetalles.UltomoRegistro(), idDescuento, Pedido.ID, tipoDescuento)
                End If
                If Tipo = 2 Then
                    CotizacionDetalles.GuardarDescuento(Cotizacion.ID, 1, 1, des, pIdMoneda, descripcion, Articulo.Iva, 0, 1, Articulo.ieps, Articulo.ivaRetenido)
                    P.GuardarDesc(CotizacionDetalles.UltomoRegistro(), idDescuento, Cotizacion.ID, tipoDescuento)
                End If
                'CD.Guardar(idRemision, 1, CDbl(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0)

            Else
                'nombreProducto = TextBox4.Text()'************
                AuxpIdAlmacen = pIdAlmacen
                AuxpIdMoneda = pIdMoneda
                AuxpIdServicio = pIdServicio
                AuxpIdVariante = pIdVariante
                AuxpIva = pIva
                'Precio... ocupo la variable que almacena el precio,,,
                Promociones(idDescuento, TablaDesc.Rows(0)(2).ToString(), Precio, "DESCUENTO: PROMOCIÓN " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, Integer.Parse(TablaDesc.Rows(0)(8).ToString()), pIdMoneda)
                ''hay promocion
                'primero checar si se cumple la promocion
                'si no añadir

            End If

        End If

    End Sub

    Public Sub modificarDescuentoPrecio(ByVal idMod As Integer, ByVal pPrecio As Double, ByVal pTipoRedondeo As Byte, ByVal pCantidadDecimales As Byte, ByVal pDescuento As Double)
        'Modificar precio
        Dim idDescuento As Integer
        idDescuento = P.HayDescuento(idProducto, fechaFormato() + " " + horaFormato(), idSucursal)
        Dim TablaDesc As DataTable
        Dim des As Double = 0
        Dim descripcion As String = ""
        Dim aux As Integer
        If idDescuento = 0 Then
            'No hay descuento
        Else
            TablaDesc = P.tablaDesc(idDescuento)
            If TablaDesc.Rows(0)(9).ToString() <> "Promocion" Then

                If TablaDesc.Rows(0)(9).ToString() = "Porcentaje" Then
                    des = TotalPorcentaje(precioArticulo, Integer.Parse(TablaDesc.Rows(0)(2).ToString()))
                    'des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: " + nombreProducto + " " + TablaDesc.Rows(0)(2).ToString() + " %"
                Else
                    des = Double.Parse(TablaDesc.Rows(0)(2).ToString())
                    ' des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: $" + TablaDesc.Rows(0)(2).ToString() + " P/U" + " " + nombreProducto
                End If
                If Tipo = 0 Then
                    RemisionDetalles.CambiaPrecio(idMod, des, pTipoRedondeo, pCantidadDecimales, 0)
                    P.ModificarDescuento(idMod, idDescuento, Remision.Folio, tipoDescuento)
                    aux = RemisionDetalles.ID
                End If
                If Tipo = 1 Then
                    PedidoDetalles.CambiaPrecio(idMod, des, TipoRedondeo, CantidadDecimales, 0)
                    P.ModificarDescuento(idMod, idDescuento, Pedido.ID, tipoDescuento)
                    aux = PedidoDetalles.ID
                End If
                If Tipo = 2 Then
                    CotizacionDetalles.CambiaPrecioDescuento(idMod, des, TipoRedondeo, CantidadDecimales, 0)
                    P.ModificarDescuento(idMod, idDescuento, Cotizacion.ID, tipoDescuento)
                    aux = CotizacionDetalles.ID
                End If

            Else
                'promociones
                ' modificarPromociones(idDescuento, TablaDesc.Rows(0)(2).ToString(), IDsMonedas.Valor(TextBox12.Text), "DESCUENTO: PROMOCIÓN " + TablaDesc.Rows(0)(2).ToString()+ " " + nombreProducto, IdInventario)
                '  modificarPromociones(idDescuento, TablaDesc.Rows(0)(2).ToString(), Double.Parse(TextBox12.Text), "DESCUENTO: PROMOCIÓN " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, IdInventario)
                'ByVal idDescuento As Integer, ByVal valor As String, ByVal precio As Double, ByVal descripcion As String, ByVal idProducto As Integer

            End If
        End If

    End Sub

    Public Sub modificarDescuentoCantidad(ByVal idMod As Integer, ByVal pCantidad As Double, ByVal pTiporedondeo As Byte, ByVal pCantidadDecimales As Byte)
        'Modificar cantidad
        Dim idDescuento As Integer
        idDescuento = P.HayDescuento(idProducto, fechaFormato() + " " + horaFormato(), idSucursal)
        Dim TablaDesc As DataTable
        Dim des As Double = 0
        Dim temp As String = nombreProducto
        '  Dim nombreProducto As String = ""
        Dim descripcion As String = ""
        Dim aux As Integer
        If idDescuento = 0 Then

            'No hay descuento
        Else
            TablaDesc = P.tablaDesc(idDescuento)
            If TablaDesc.Rows(0)(9).ToString() <> "Promocion" Then

                If TablaDesc.Rows(0)(9).ToString() = "Porcentaje" Then
                    des = TotalPorcentaje(precioArticulo, Integer.Parse(TablaDesc.Rows(0)(2).ToString()))
                    'des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: " + nombreProducto + " " + TablaDesc.Rows(0)(2).ToString() + " %"
                Else
                    des = Double.Parse(TablaDesc.Rows(0)(2).ToString())
                    ' des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: $" + TablaDesc.Rows(0)(2).ToString() + " P/U" + " " + nombreProducto
                End If
                'RemisionDetalles.CambiaPrecio(idMod, des, pTipoRedondeo, pCantidadDecimales, 0)

                If Tipo = 0 Then
                    RemisionDetalles.AgregarCantidadDescuento(idMod, pCantidad, TipoRedondeo, CantidadDecimales, des)
                    P.ModificarDescuento(idMod, idDescuento, Remision.Folio, tipoDescuento)
                    aux = RemisionDetalles.ID
                    AuxpIdServicio = Remision.Folio
                End If
                If Tipo = 1 Then
                    PedidoDetalles.AgregarCantidadDescuento(idMod, pCantidad, TipoRedondeo, CantidadDecimales, des)
                    P.ModificarDescuento(idMod, idDescuento, Pedido.ID, tipoDescuento)
                    aux = PedidoDetalles.ID
                End If
                If Tipo = 2 Then
                    CotizacionDetalles.AgregarCantidadDescuento(idMod, pCantidad, TipoRedondeo, CantidadDecimales)
                    P.ModificarDescuento(idMod, idDescuento, CotizacionDetalles.ID, tipoDescuento)
                    aux = CotizacionDetalles.ID
                End If
            Else
                'promociones
                ' modificarPromociones(idDescuento, TablaDesc.Rows(0)(2).ToString(), IDsMonedas.Valor(TextBox12.Text), "DESCUENTO: PROMOCIÓN " + TablaDesc.Rows(0)(2).ToString()+ " " + nombreProducto, IdInventario)
                '  modificarPromociones(idDescuento, TablaDesc.Rows(0)(2).ToString(), Double.Parse(TextBox12.Text), "DESCUENTO: PROMOCIÓN " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, IdInventario)
                'ByVal idDescuento As Integer, ByVal valor As String, ByVal precio As Double, ByVal descripcion As String, ByVal idProducto As Integer
                AuxpIdAlmacen = 0
                AuxpIdMoneda = 0

                AuxpIdVariante = 0
                AuxpIva = 0
                'Precio... ocupo la variable que almacena el precio,,,
                Promociones(idDescuento, TablaDesc.Rows(0)(2).ToString(), Precio, "DESCUENTO: PROMOCIÓN " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, idProducto, 2)

            End If
        End If

    End Sub

    Public Sub Promociones(ByVal idDescuento As Integer, ByVal valor As String, ByVal precio As Double, ByVal descripcion As String, ByVal idProducto As Integer, ByVal moneda As Integer)
        Dim cDesc As Integer = 0
        Dim des As Double = 0
        Dim cant As Integer
        Dim idVenta As String = "" 'idventa servirá como auxiliar para el ID
        'idDescuento = P.HayDescuento(idProducto, fechaFormato() + " " + horaFormato())
        precio = precioArticulo
        Dim regAnadir As Integer = 0
        Dim regDesc As Integer = 0
        valorProm(valor) 'esto establece los valores 2 x 1  y esos
        'primero que agregue el renglon a la db
        cant = CantidadArtDesc  'cantidad de productos que se estan registrando
        'asegurarse de que sea positivo
        If cant > 0 Then
            'agregar

            If Tipo = 0 Then
                For i As Integer = 1 To cant
                    P.guardarPromocion(Remision.ID, idDescuento, idProducto, RemisionDetalles.UltomoRegistro())
                Next
                idVenta = Remision.ID
            End If
            If Tipo = 1 Then
                For i As Integer = 1 To cant
                    P.guardarPromocion(Pedido.ID, idDescuento, idProducto, PedidoDetalles.UltomoRegistro())
                Next
                idVenta = Pedido.ID
            End If
            If Tipo = 2 Then
                For i As Integer = 1 To cant
                    P.guardarPromocion(Cotizacion.ID, idDescuento, idProducto, CotizacionDetalles.UltomoRegistro())
                Next
                idVenta = Cotizacion.ID
            End If

            If P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count() >= promocion1 Then
                regAnadir = Int(P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count() Mod promocion1)
                regDesc = Int(P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

                cDesc = promocion1 - promocion2
                des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
                des = des * regDesc

                ' CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0, 0, regDesc, 1)
                'P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idVenta, tipoDescuento)
                If Tipo = 0 Then                                                'des, pIdMoneda, descripcion, pIdAlmacen, pIva, 0, pIdVariante, pIdServicio
                    RemisionDetalles.GuardarDescuento(Remision.ID, 1, regDesc, des, moneda, descripcion, idAlmacen, Articulo.Iva, 0, 1, 0, Articulo.ieps, Articulo.ivaRetenido, Articulo.TipoContenido.ID)
                    P.GuardarDesc(CD.UltomoRegistro(), idDescuento, Remision.Folio, tipoDescuento)

                End If
                If Tipo = 1 Then
                    PedidoDetalles.GuardarDescuento(Pedido.ID, 1, regDesc, des, moneda, descripcion, Articulo.Iva, 0, 1, Articulo.ieps, Articulo.ivaRetenido)
                    P.GuardarDesc(PedidoDetalles.UltomoRegistro(), idDescuento, Pedido.ID, tipoDescuento)
                End If
                If Tipo = 2 Then
                    CotizacionDetalles.GuardarDescuento(Cotizacion.ID, 1, regDesc, des, moneda, descripcion, Articulo.Iva, 0, 1, Articulo.ieps, Articulo.ivaRetenido)
                    P.GuardarDesc(CotizacionDetalles.UltomoRegistro(), idDescuento, Cotizacion.ID, tipoDescuento)
                End If
                ' ConsultaDetalles()
                ' NuevoConcepto()
                P.EliminarDesc(idVenta, idDescuento, idProducto)
                'anadir registros faltantes
                If Tipo = 0 Then
                    For i As Integer = 1 To regAnadir
                        P.guardarPromocion(idVenta, idDescuento, idProducto, RemisionDetalles.UltomoRegistro())
                    Next
                End If
                If Tipo = 1 Then
                    For i As Integer = 1 To regAnadir
                        P.guardarPromocion(idVenta, idDescuento, idProducto, PedidoDetalles.UltomoRegistro())
                    Next
                End If
                If Tipo = 2 Then
                    For i As Integer = 1 To regAnadir
                        P.guardarPromocion(idVenta, idDescuento, idProducto, CotizacionDetalles.UltomoRegistro())
                    Next
                End If

            End If


        Else
            'eliminar
            eliminarProm(descripcion, idDescuento)
        End If


        'End If

    End Sub
    Private Sub eliminarProm(ByVal descripcio As String, ByVal idDescuento As Integer)
        Dim dt As New DataTable
        Dim cDesc As Integer = 0
        Dim des As Double = 0
        Dim precio As Double
        ' Dim cant As Integer
        Dim idVenta As String = ""
        Dim tot As Double = 0
        Dim tot2 As Integer
        Dim regAnadir As Integer = 0
        Dim regDesc As Integer = 0
        precio = precioArticulo
        ' cant = CantidadArtDesc

        If Tipo = 0 Then
            idVenta = Remision.ID
            P.EliminarDescAnadidosRem(Remision.ID, descripcio) 'elimina registros
            dt = P.buscarDesAnadidosRem(Remision.ID, idProducto) 'busca registros
        End If
        If Tipo = 1 Then
            idVenta = Pedido.ID
            P.EliminarDescAnadidosPed(Pedido.ID, descripcio)
            dt = P.buscarDesAnadidosPed(Pedido.ID, idProducto) 'busca registros
        End If
        If Tipo = 2 Then
            idVenta = Cotizacion.ID
            P.EliminarDescAnadidosCot(Cotizacion.ID, descripcio)
            dt = P.buscarDesAnadidosCot(Cotizacion.ID, idProducto) 'busca registros
        End If

        P.EliminarDesc(idVenta, idDescuento, idProducto)


        'Idproducto no estoy segura de que este bien
        If Tipo = 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                tot = tot + Double.Parse(dt.Rows(i)(3).ToString())
            Next

        Else
            For i As Integer = 0 To dt.Rows.Count - 1
                tot = tot + Double.Parse(dt.Rows(i)(4).ToString())
            Next

        End If




        tot2 = Int(tot)

        For i As Integer = 1 To tot2 'agregar los que se almacenaron
            P.guardarPromocion(idVenta, idDescuento, idProducto, 0)
        Next

        If P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count() >= promocion1 Then

            regAnadir = Int(P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count() Mod promocion1)
            regDesc = Int(P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

            cDesc = promocion1 - promocion2
            des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
            des = des * regDesc


            If Tipo = 0 Then
                RemisionDetalles.GuardarDescuento(Remision.ID, 1, canProducto, des, 2, descripcio, idAlmacen, AuxpIva, 0, 1, AuxpIdServicio, Articulo.ieps, Articulo.ivaRetenido, Articulo.TipoContenido.ID)
                P.GuardarDesc(CD.UltomoRegistro(), idDescuento, Remision.Folio, tipoDescuento)
            End If
            If Tipo = 1 Then
                PedidoDetalles.GuardarDescuento(Pedido.ID, 1, regDesc, des, IP.IdMoneda, descripcio, Articulo.Iva, 0, 1, Articulo.ieps, Articulo.ivaRetenido)
                P.GuardarDesc(PedidoDetalles.UltomoRegistro(), idDescuento, Pedido.ID, tipoDescuento)
            End If
            If Tipo = 2 Then
                CotizacionDetalles.GuardarDescuento(Cotizacion.ID, 1, regDesc, des, IP.IdMoneda, descripcio, Articulo.Iva, 0, 1, Articulo.ieps, Articulo.ivaRetenido)
                P.GuardarDesc(CotizacionDetalles.UltomoRegistro(), idDescuento, Cotizacion.ID, tipoDescuento)
            End If

            'CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0, 0, regDesc, 1)
            'P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idVenta, "VentasN")

            ' ConsultaDetalles()
            ' NuevoConcepto()
            P.EliminarDesc(idVenta, idDescuento, idProducto)
            'anadir registros faltantes
            For i As Integer = 1 To regAnadir
                P.guardarPromocion(idVenta, idDescuento, idProducto, CD.UltomoRegistro())
            Next
        End If

    End Sub

    Private Sub actualizaPrecioPromocion(ByVal descrip As String, ByVal Precio As Double)
        Dim dt As New DataTable
        Dim nTabla As String = ""
        Dim canti As Double = 0
        Dim tot As Double = 0
        If Tipo = 0 Then
            dt = P.buscarPromocionesAnadidasRem(descrip, Remision.ID)
            nTabla = "tblventasremisionesinventario"
        End If
        If Tipo = 1 Then
            dt = P.buscarPromocionesAnadidasPed(descrip, Pedido.ID)
            nTabla = "tblventaspedidosinventario"
        End If
        If Tipo = 2 Then
            dt = P.buscarPromocionesAnadidasCot(descrip, Cotizacion.ID)
            nTabla = "tblventascotizacionesinventario"
        End If

        For i As Integer = 0 To dt.Rows.Count() - 1
            If Tipo = 0 Then
                canti = Double.Parse(dt.Rows(i)(3).ToString())
                tot = (Precio * canti) * (-1)
                P.cambiarPrecioProm(Integer.Parse(dt.Rows(i)(0).ToString()), tot, nTabla)
            Else
                canti = Double.Parse(dt.Rows(i)(4).ToString())
                tot = (Precio * canti) * (-1)
                P.cambiarPrecioProm(Integer.Parse(dt.Rows(i)(0).ToString()), tot, nTabla)
            End If
        Next


    End Sub
#End Region

#Region " PEDIDOS "

#End Region
    '------------FORMATO--------------'
#Region " FORMATO "
    Public Function horaFormato() As String
        Dim fechita As String
        Dim Aux As String = ""

        fechita = Now.ToString("HH:mm:ss")
        For j As Integer = 0 To 7
            Aux = Aux + fechita.Chars(j)
        Next
        fechita = Aux

        Return fechita
    End Function

    Public Function fechaFormato() As String
        Dim fechita2 As String
        fechita2 = Date.Now.Year.ToString() + "/" + Integer.Parse(Date.Now.Month.ToString).ToString("00") + "/" + Integer.Parse(Date.Now.Day.ToString).ToString("00")
        Return fechita2
    End Function

    Public Function TotalPorcentaje(ByVal total As Double, ByVal porcentaje As Integer) As Double
        Dim desc As Double = 0
        desc = (total * porcentaje) / 100
        Return desc 'devuelve el descuento solamente
    End Function

    Public Sub valorProm(ByVal valor As String)
        Dim aux As String = ""
        Dim bandera As Boolean = False

        For j As Integer = 0 To valor.Length() - 1
            If bandera = False Then
                'agarrar el primero
                If valor.Chars(j) <> "x" Then
                    aux = aux + valor.Chars(j)

                Else
                    ' es X
                    promocion1 = Integer.Parse(aux)
                    bandera = True
                    aux = ""
                End If


            Else
                'agarrar el segundo numero
                aux = aux + valor.Chars(j)
            End If


        Next
        promocion2 = Integer.Parse(aux)
    End Sub
#End Region

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click

        If RemisionDetalles.Inventario.PorLotes = 1 Then
            Dim F As New frmInventarioLotes(0, RemisionDetalles.ID, 0, 0, RemisionDetalles.Cantidad, RemisionDetalles.Inventario.ID, 1, 0, 0, 0, idAlmacen, AlmacenNombre, 0, 0, 0)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If RemisionDetalles.Inventario.Aduana = 1 Then
            Dim F As New frmInventarioAduana(0, RemisionDetalles.ID, 0, 0, RemisionDetalles.Cantidad, RemisionDetalles.Inventario.ID, 1, 0, 0, 0, idAlmacen, AlmacenNombre, 0, 0, 0)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim C As String = ""
        Select Case Tipo
            Case 0
                C = Remision.Comentario
            Case 1
                C = Pedido.Comentario
            Case 2
                C = Cotizacion.Comentario
        End Select
        Dim et As New frmVentasTextoExtra(C, 1000, False, "Arial", 12)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Select Case Tipo
                Case 0
                    Remision.Comentario = et.Texto
                Case 1
                    Pedido.Comentario = et.Texto
                Case 2
                    Cotizacion.Comentario = et.Texto
            End Select
        End If
        et.Dispose()
        TextBox1.Focus()
    End Sub
End Class