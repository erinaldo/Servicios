
Public Class frmVentasN
    Dim IdsVariantes As New elemento
    Dim IdVendedorU As Integer
    Dim idVenta As Integer
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim IdsVendedores As New elemento
    Dim IdsTemp As New elemento
    Dim idCliente As Integer
    Dim IdInventario As Integer
    Dim IdDetalle As Integer
    Dim IdsAlmacenes As New elemento
    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim ManejaSeries As Byte
    Dim IdAlmacen As Integer
    Dim Estado As Byte
    Dim FolioAnt As Integer
    Dim IdVariante As Integer
    Dim IdServicio As Integer
    Dim PrecioU As Double
    Dim Tabla As New DataTable
    Dim PrecioNeto As Byte
    Dim IdsSucursales As New elemento
    Dim idsFormasDePago As New elemento
    Dim SerieAnt As String
    Dim Cadena As String
    Dim Sello As String
    Dim Isr As Double
    Dim IvaRetenido As Double
    Dim PrecioBase As Double
    Dim iTipoFacturacion As Byte
    Dim CantidadMostrar As Double
    Dim TipoCantidadMostrar As Integer
    Dim TipoCantidad As Integer
    Dim LlenandoDatos As Boolean = False
    Dim Sobre As Byte
    Dim SIVA As Double
    Dim TipoCreardesde As Byte
    Dim CadenaCFDI As String
    Dim CodigoBidimensional As Bitmap
    Dim LimitedeFolios As Boolean = False
    Dim idRemisiones() As Integer
    Public idPagos() As Integer
    Dim idRemisionesCero() As Integer = {0}
    Dim SinConcersion As Boolean
    Dim IdLista As Integer
    Dim IdsCuentas As New elemento
    Dim Op As dbOpciones
    Dim SinCredito As Boolean
    Dim Saldo As Double
    Dim CreditoCliente As Double
    Dim Eselectronica As Byte
    Dim CertificadoCaduco As Boolean = False
    Dim SinTimbres As Boolean = False
    Dim DocAImprimir As Byte = 0
    Dim TipoImpresora As Byte = 0
    Dim UsaAdenda As Integer = 0
    Dim IdVentaOrigen As Integer
    Dim Parcialidad As Integer = 1
    Dim Parcialidades As Integer = 1
    Dim Funcion As Byte
    Dim ParcialidadImporte As Double
    Dim IdClienteOrigen As Integer
    Dim UsaFormula As Byte
    Dim Articulos As New Collection
    Dim CostoArticulo As Double
    Dim SaldoaFavor As Double
    Dim Esamortizacion As Byte
    Dim ActivarImpuestos As Byte
    Dim EsKit As Byte
    Dim SeparaKit As Byte
    Dim FacturaGlobal As dbVentas
    Dim P As dbDescuentos
    Dim CD As dbVentasInventario
    Dim promocion1 As Integer
    Dim promocion2 As Integer
    Dim nombreProducto As String
    Dim cantAntModificar As Integer
    Dim tipoElimianr As String
    Dim RefDocumento As String
    Dim Adicional As String
    Dim PorLotes As Byte
    Dim Aduana As Byte
    Dim Veces As Integer = 0
    Dim Secuencia As String
    Dim Lectura As String = ""
    Dim ArtArticulo As String
    Dim MetodosDePago As dbVentasAddMetodos
    Dim TotalVenta As Double
    Dim ImpDoc As ImprimirDocumento
    Dim Almacen As dbAlmacenes
    Dim CSat As dbCatalogosSat
    Dim xSat As New elemento
    Dim Descontando As Boolean = True
    Dim Contenido As Double
    Public Sub New(ByVal pidVenta As Integer, ByVal pFuncion As Byte, ByVal pImporte As Double, ByVal pidCliente As Integer)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idVenta = pidVenta
        If pFuncion = 1 Then
            IdVentaOrigen = pidVenta
            idVenta = 0
        End If
        Funcion = pFuncion
        ParcialidadImporte = pImporte
        IdClienteOrigen = pidCliente
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta factura no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesAVenta(idVenta)
                Dim C As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
                'C.RegresaInventario(idVenta)
                C.Eliminar(idVenta)
                P.limpiarDescPromociones()
                P.limpiarVentasdesc()
                AddError("Se eliminó factura con folio: " + TextBox11.Text + TextBox2.Text, "ventas eliminar cerrando ventana.", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm"), idVenta)
                e.Cancel = False
                CSat.Con.Close()
            Else
                GlobalEstadoVentanas = GlobalEstadoVentanas And Not 1
                e.Cancel = True
            End If
        Else
            CSat.Con.Close()
            GlobalEstadoVentanas = GlobalEstadoVentanas And Not 1
        End If
    End Sub

    Private Sub frmVentasN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F10 Then
            If Button14.Enabled = True Then
                Modificar(Estados.Guardada)
            End If
        End If
        If e.KeyCode = Keys.F12 Then
            If idCliente <> 0 Then
                Dim Cl As New frmClientesConsultaArticulos(idCliente, IdInventario)
                Cl.ShowDialog()
                Cl.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F9 Then
            If Button1.Enabled = True Then
                If idRemisiones.Length = 1 And idRemisiones(0) = 0 Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirPendienteVentas, PermisosN.Secciones.Ventas) = True Then
                        Modificar(Estados.Pendiente)
                    End If
                Else
                    MsgBox("No se puede dejar pendiente esta factura.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            BotonNuevo()
        End If
        If e.KeyCode = Keys.F4 Then
            BotonBuscar(False)
        End If
        If e.KeyCode = Keys.F3 Then
            Try
                If SerialPort1.IsOpen = False Then SerialPort1.Open()
                SerialPort1.Write(Chr(CInt(Secuencia)))
                Lectura = ""
                Veces = 0
                Timer1.Enabled = True
            Catch ex As Exception
                MsgBox(ex.Message)
                If SerialPort1.IsOpen Then SerialPort1.Close()
            End Try
        End If
        If e.KeyCode = Keys.F6 And IdInventario <> 0 And Estado <= 2 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.Consultas, PermisosN.Secciones.Ventas) = True Then
                Dim f As New frmInventarioConsulta(IdInventario, 1, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    cmbAlmacen.SelectedIndex = IdsAlmacenes.Busca(f.IdAlmacen)
                End If
                f.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F6 And Estado >= 3 Then
            If RadioButton1.Checked Then
                Dim fmp As New frmVentasSelectorMetodosPago(0, idVenta, TotalVenta, 1, True)
                fmp.ShowDialog()
                fmp.Dispose()
            Else
                Dim fmp As New frmVentasSelectorMetodosPago(0, idVenta, TotalVenta, 0, True)
                fmp.ShowDialog()
                fmp.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F7 And IdInventario <> 0 And EsKit <> 0 And IdDetalle <> 0 Then
            Dim IDe As New frmInventarioDetalles(IdInventario, 1, IdDetalle, idVenta)
            IDe.ShowDialog()
            IDe.Dispose()
        End If
        If e.KeyCode = Keys.F8 Then
            If GlobalTipoVersion = 0 Then
                Dim f As New frmCompras
                f.ShowDialog()
                f.Dispose()
            End If
        End If
    End Sub

    Private Sub frmVentasN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = Me.Text + " " + GlobalUsuario
            Me.Icon = GlobalIcono
            Button34.BackgroundImageLayout = ImageLayout.Stretch
            Button34.BackgroundImage = Image.FromFile(Application.StartupPath + "\iconos\message.png")
        Catch ex As Exception

        End Try
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            For Each c As Control In Me.Controls
                c.Enabled = False
            Next
            Button3.Enabled = True
        Else
            Try
                P = New dbDescuentos(MySqlcon)
                CD = New dbVentasInventario(MySqlcon)
                Op = New dbOpciones(MySqlcon)
                FacturaGlobal = New dbVentas(MySqlcon)
                ImpDoc = New ImprimirDocumento
                MetodosDePago = New dbVentasAddMetodos(MySqlcon)
                Almacen = New dbAlmacenes(MySqlcon)
                Almacen.AlmacenesSinPermiso(GlobalIdUsuario)
                Dim U As New dbUsuarios(GlobalIdUsuario, MySqlcon)
                CSat = New dbCatalogosSat
                Csat.IniciarMySQL(My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
                IdVendedorU = U.IdVendedor
                Label35.Text = Op._noCertificado
                If Op.MostrarPredial = 0 Then
                    Label35.Visible = False
                    ComboBox7.Visible = False
                End If
                If Op.SobreEscribeImpLocales = 0 Then
                    Label37.Visible = False
                    TextBox18.Visible = False
                    Button31.Visible = False
                End If
            Catch ex As Exception
            End Try
            Try
                
                If SerialPort1.IsOpen Then SerialPort1.Close()
                SerialPort1.PortName = Op.PuertoBascula
                SerialPort1.BaudRate = Op.BasculaBaundRate
                SerialPort1.Parity = Op.BasculaParity
                SerialPort1.DataBits = Op.BasculaDataBits
                SerialPort1.Handshake = Op.BasculaHandshake
                Secuencia = Op.BasculaSecuencia
                SerialPort1.WriteTimeout = 1000
                SerialPort1.ReadTimeout = 1000
            Catch ex As Exception
                'MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
            End Try
            CheckScroll.Checked = My.Settings.ventasscroll
            If GlobalTipoFacturacion > 1 Then
                SinTimbres = ChecaTimbres()
            End If
            ConsultaOn = False
            Dim I As Integer = 0
            Dim S As String = ""
            Dim D As Double = 0
            Tabla.Columns.Add("Id", I.GetType)
            Tabla.Columns.Add("TipoR", S.GetType)
            Tabla.Columns.Add("Extra", S.GetType)
            Tabla.Columns.Add("Cantidad", D.GetType)
            Tabla.Columns.Add("Uni.", S.GetType)
            Tabla.Columns.Add("Código", S.GetType)
            Tabla.Columns.Add("Descripción", S.GetType)
            Tabla.Columns.Add("Precio U.", S.GetType)
            Tabla.Columns.Add("Importe", S.GetType)
            Tabla.Columns.Add("Moneda", S.GetType)
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodePrecio, PermisosN.Secciones.Ventas) = False Then
                TextBox12.ReadOnly = True
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerFechaConta, PermisosN.Secciones.Contabilidad) = False Then
                Label3.Visible = False
                DateTimePicker2.Visible = False
            End If
            'ConsultaOn = True
            LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
            ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
            'ConsultaOn = False

            LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
            LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
            LlenaCombos("tblvendedores", ComboBox5, "nombre", "nombret", "idvendedor", IdsVendedores)
            ConsultaOn = True
            If idVenta = 0 And Funcion = 0 Then
                Nuevo()

                If FacturaGlobal.CuantasSinTimbrar(DateAdd(DateInterval.Day, -7, DateTimePicker1.Value).ToString("yyyy/MM/dd")) > 0 Then
                    'Me.Show()
                    If MsgBox("Hay facturas sin timbrar. ¿Desea revisarlas?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        BotonBuscar(True)
                    End If
                End If
            Else
                If Funcion = 0 Then

                    If Op._TipoSelAlmacen = "0" Then
                        Dim Su As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
                        LlenaCombos("tblalmacenes", cmbAlmacen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
                        cmbAlmacen.SelectedIndex = IdsAlmacenes.Busca(Su.idAlmacen)
                    Else
                        LlenaCombos("tblalmacenes", cmbAlmacen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. almacen")
                        cmbAlmacen.SelectedIndex = 0
                    End If
                    LlenaDatosVenta()
                    NuevoConcepto()
                End If
            End If
            If IdVentaOrigen <> 0 And Funcion = 1 Then
                Nuevo()
                Dim FO As New dbVentas(IdVentaOrigen, MySqlcon, Op._Sinnegativos)
                TextBox11.Text = "PAR"
                TextBox2.Text = FO.DaNuevoFolio("PAR", IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion, Op._ModoFoliosB).ToString
                Dim Cl As New dbClientes(IdClienteOrigen, MySqlcon)
                TextBox1.Text = Cl.Clave
                ComboBox4.SelectedIndex = idsFormasDePago.Busca(99)
                Button5.Enabled = False
                Button19.Enabled = False
                TextBox1.Enabled = False
                Guardar()
                If Estado <> 0 Then
                    Dim Ar As New dbInventario(MySqlcon)
                    TextBox5.Text = "1"
                    IdInventario = Ar.DaArticuloNoInventariable
                    Ar = New dbInventario(IdInventario, MySqlcon)
                    LlenaDatosArticulo(Ar)
                    TextBox12.Text = ParcialidadImporte.ToString
                    Parcialidad = FO.DaCantidadParcialidades(IdVentaOrigen) + 1
                    Parcialidades = FO.Parcialidades
                    TextBox15.Text = FO.Parcialidades.ToString
                    If FO.Parcialidades <> 1 Then
                        TextBox4.Text = "PAGO PARCIALIDAD " + Parcialidad.ToString + " DE " + FO.Parcialidades.ToString + " DE LA FACTURA " + FO.Serie + Format(FO.Folio, "00000")
                    Else
                        TextBox4.Text = "PAGO PARCIALIDAD DE LA FACTURA " + FO.Serie + Format(FO.Folio, "00000")
                    End If
                    IdVariante = 1
                    If IdInventario <> 0 Or IdVariante <> 0 Or IdServicio <> 0 Then AgregaArticulo()
                Else
                    MsgBox("No se pudo generar la parcialidad.", MsgBoxStyle.Critical, GlobalNombreApp)
                End If

            End If
            If Funcion = 2 Then
                Nuevo()
                CrearDesdePago(IdClienteOrigen)
            End If
        End If
        Panel4.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim V As New dbVentas(MySqlcon)
                V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
                Label12.Text = Format(V.Subtototal, "#,##0.00")
                Label13.Text = Format(V.TotalIva - V.TotalISR - V.TotalIvaRetenido, "#,##0.00")
                Label14.Text = Format(V.TotalVenta, "#,##0.00")
                TotalVenta = V.TotalVenta
                Label32.Text = Format(V.TotalPeso, "#,##0.00") + "Kg."
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        idRemisiones = idremisionescero
        Button11.Enabled = True
        Button33.Enabled = True
        Button35.Enabled = False
        DateTimePicker1.Value = Date.Now
        TextBox1.Text = ""
        FolioAnt = 0
        idVenta = 0
        Label38.Visible = False
        Label39.Visible = False
        Label40.Visible = False
        TextBox13.Text = ""
        TotalVenta = 0
        TipoCreardesde = 0
        SaldoaFavor = 0
        CheckBox2.Enabled = True
        CheckBox3.Checked = False
        Button34.Enabled = False
        TextBox20.Enabled = True
        If Op.SiemprePorSurtirVentas = 0 Then
            CheckBox2.Checked = False
        Else
            CheckBox2.Checked = True
        End If
        Label32.Text = "0.00Kg."
        Button30.Enabled = False
        TextBox20.Text = ""
        Panel1.Enabled = True
        cmbAlmacen.Enabled = True
        Panel2.Enabled = True
        Button27.Enabled = False
        RadioButton1.Enabled = True
        If Op.NoPermitirFacturasdeCredito = 0 And GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirVentasCredito, PermisosN.Secciones.Ventas) = True Then
            RadioButton2.Enabled = True
        Else
            RadioButton2.Enabled = False
        End If
        If Op.FacturarSoloaCredito Then
            RadioButton2.Enabled = True
            RadioButton1.Enabled = False
            RadioButton2.Checked = True
        Else
            RadioButton1.Checked = True
        End If
        ComboBox4.Enabled = True
        Button37.Visible = False
        RefDocumento = ""
        Adicional = ""
        TextBox18.Text = "0"
        TextBox19.Text = "0"
        TextBox16.Text = "0"
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button21.Enabled = False
        'CheckBox2.Checked = False
        Button1.Enabled = False
        Button14.Enabled = False
        Button15.Enabled = False
        ComboBox3.Enabled = True
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        iTipoFacturacion = GlobalTipoFacturacion

        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        If Op._TipoSelAlmacen = "0" Then
            LlenaCombos("tblalmacenes", cmbAlmacen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            cmbAlmacen.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
        Else
            LlenaCombos("tblalmacenes", cmbAlmacen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. almacen")
            cmbAlmacen.SelectedIndex = 0
        End If
        'TextBox11.Text = S.Serie

        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Factura, iTipoFacturacion)

        TextBox11.Text = Sf.Serie
        Eselectronica = iTipoFacturacion
        Dim V As New dbVentas(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion, Op._ModoFoliosB).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString

        If GlobaltpBanxico <> "Error" Then
            TextBox10.Text = GlobaltpBanxico
        Else
            Dim CM As New dbMonedasConversiones(1, MySqlcon)
            TextBox10.Text = CM.Cantidad.ToString
        End If

        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        Button20.Visible = False
        ComboBox6.Items.Clear()
        ComboBox6.Text = ""
        SerieAnt = ""
        Button2.Enabled = True
        Label24.Visible = False
        TextBox14.Text = ""
        TextBox15.Text = "1"
        Button23.Visible = False
        NuevoConcepto()

        If Eselectronica >= 1 Then
            CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
        End If
        If CInt(TextBox2.Text) > Sf.FolioFinal Then
            LimitedeFolios = True
            MsgBox("Se ha alcanzado el límite de folios.", MsgBoxStyle.Information, GlobalNombreApp)
        Else
            LimitedeFolios = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
            ComboBox3.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirDescuento, PermisosN.Secciones.Ventas) = False Then
            TextBox16.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodeFolio, PermisosN.Secciones.Ventas) = False Then
            TextBox11.Enabled = False
            TextBox2.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirCambiarFechaVentas, PermisosN.Secciones.Ventas) = False Then
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        End If
        If IdVentaOrigen <> 0 And Funcion = 1 Then
            RadioButton2.Enabled = False
        End If
        If GlobalTipoFacturacion = 3 Then
            Label29.Visible = False
            ComboBox6.Visible = False
        Else
            Label29.Visible = True
            ComboBox6.Visible = True
        End If
        TextBox1.Focus()
        If GlobalTipoVersion = 3 Then
            For Each c As Control In Panel4.Controls
                c.Enabled = False
            Next
            'Panel4.Enabled = True
            Button3.Enabled = True
            Button10.Enabled = True
            Button27.Enabled = True
        End If
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then


            If TextBox20.Visible = False Then
                If Op._TipoSelAlmacen <> "0" Then
                    'If ComboBox8.SelectedIndex <= 0 Then
                    cmbAlmacen.Focus()
                    'End If
                Else
                    If Op._CursorVentas = "0" Then
                        TextBox5.Focus()
                    Else
                        TextBox3.Focus()
                    End If
                End If
            Else
                TextBox20.Focus()
            End If

        End If
        If e.KeyCode = Keys.F1 Then
            BuscaClienteBoton()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaCliente()
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbClientes(MySqlcon)

                If c.BuscaCliente(TextBox1.Text) Then
                    If c.DireccionFiscal = 0 Then
                        TextBox7.Text = c.RFC + " " + c.Nombre '+ vbCrLf + c.RFC + vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                    Else
                        TextBox7.Text = c.RFC + " " + c.Nombre 'c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion2 + " " + c.NoExterior2 + " " + c.Ciudad2 + " " + c.CP2
                    End If
                    If c.NoChecarCr = 0 Then
                        Saldo = c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
                        SaldoaFavor = CDbl(Format(c.DaSaldoAFavor(c.ID), "0.00"))
                    Else
                        Saldo = 0
                        SaldoaFavor = 0
                    End If
                    CreditoCliente = c.Credito
                    ActivarImpuestos = c.ActivarImpuestos

                    TextBox7.Text += vbCrLf + "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00") + " " + "Saldo: " + Format(Saldo, "#,##0.00") + " " + "A Favor: " + Format(SaldoaFavor, "#,##0.00")
                    TextBox7.Text += vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.Estado + " " + c.CP
                    If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                        'If (c.Credito > 0 Or c.CreditoDias > 0) And Op.NoPermitirFacturasdeCredito = 0 Then
                        '    ComboBox4.SelectedIndex = 1
                        'Else
                        '    ComboBox4.SelectedIndex = 0
                        'End If
                        'If Op.NoPermitirFacturasdeCredito = 0 Then
                        'LlenaCombos("tblformasdepago", ComboBox4, "concat(convert(if(tipo=0,'CRÉDITO',if(tipo=1,'CONTADO','PARCIALIDAD')) using utf8),' ',lpad(convert(clavesat using utf8),2,'0'),' ',nombre)", "nombret", "idforma", idsFormasDePago, , , "idforma")
                        Dim FP As New dbFormasdePago(c.IdFormaF, MySqlcon)
                        If IdVentaOrigen <> 0 And Funcion = 1 Then
                            RadioButton1.Checked = True
                            RadioButton2.Enabled = False
                        Else
                            If FP.Tipo = dbFormasdePago.Tipos.Contado Or Op.NoPermitirFacturasdeCredito = 1 Or GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirVentasCredito, PermisosN.Secciones.Ventas) = False Then
                                RadioButton1.Checked = True
                            Else
                                RadioButton2.Checked = True
                            End If
                            If Op.FacturarSoloaCredito = 1 Then
                                RadioButton2.Checked = True
                            End If
                        End If
                        ComboBox4.SelectedIndex = idsFormasDePago.Busca(c.IdFormaF)
                        If IdVendedorU > 0 And Op.VendedorUsuario = 1 Then
                            ComboBox5.SelectedIndex = IdsVendedores.Busca(IdVendedorU)
                        Else
                            ComboBox5.SelectedIndex = IdsVendedores.Busca(c.IdVendedor)
                        End If
                    End If
                    If Saldo >= c.Credito Then
                        SinCredito = True
                    End If
                    If c.CreditoDias <> 0 Then
                        If c.TieneCreditoporFecha(c.ID, c.CreditoDias) = False Then
                            SinCredito = True
                        End If
                    End If
                    UsaAdenda = c.UsaAdenda
                    If c.UsaAdenda <> 0 Then
                        Button23.Visible = True
                    Else
                        Button23.Visible = False
                    End If
                    idCliente = c.ID
                    IdLista = c.IdLista
                    Isr = c.ISR
                    SIVA = c.IVA
                    Sobre = c.SobreescribeIVA
                    IvaRetenido = c.IvaRetenido
                    LlenaCombos("tblclientescuentas", ComboBox6, "cuenta", "nombret", "idcuenta", IdsCuentas, "idcliente=" + idCliente.ToString, "NO APLICA")

                    If ComboBox7.Visible Then
                        LlenaCombos("tblventasinventario", ComboBox7, "distinct predial", "nombret", "idventasinventario", IdsTemp, "inner join tblventas on tblventasinventario.idventa=tblventas.idventa where predial<>'' and idcliente=" + idCliente.ToString, , "nombret", True)
                    End If
                    If ComboBox6.Items.Count >= 2 Then
                        ComboBox6.SelectedIndex = 1
                    Else
                        ComboBox6.SelectedIndex = 0
                    End If
                    If c.RFC.Length = 13 Then CSat.LlenaCombos("tblusoscfdi", ComboBox9, "concat(clave,' ',descripcion)", "nombrem", "clave", xSat, , , , True)
                    If c.RFC.Length = 12 Then CSat.LlenaCombos("tblusoscfdi", ComboBox9, "concat(clave,' ',descripcion)", "nombrem", "clave", xSat, "moral='Sí'", , , True)
                    ComboBox9.Text = CSat.DaUsoCFDI(c.UsoCFDI)
                    If c.RFC = "XAXX010101000" Then
                        TextBox20.Visible = True
                        Label43.Visible = True
                        TextBox7.Height = 28
                    Else
                        Label43.Visible = False
                        TextBox7.Height = 57
                        TextBox20.Visible = False
                    End If
                Else
                    TextBox7.Text = ""
                    TextBox7.Height = 57
                    TextBox20.Text = ""
                    TextBox20.Visible = False
                    Label43.Visible = False
                    'TextBox13.Text = ""
                    ActivarImpuestos = 0
                    ComboBox6.Items.Clear()
                    ComboBox6.Text = ""
                    idCliente = 0
                    Isr = 0
                    IvaRetenido = 0
                    SIVA = 0
                    Sobre = 0
                    UsaAdenda = 0
                    SinCredito = False
                    Saldo = 0
                    ComboBox7.Items.Clear()
                    Button23.Visible = False
                    CreditoCliente = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If idRemisiones.Length = 1 And idRemisiones(0) = 0 Then
            Modificar(Estados.Pendiente)
        Else
            MsgBox("No se puede dejar pendiente esta factura.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub Modificar(ByVal pEstado As Byte)
        Try
            Dim XMLAdenda As String = ""
            Dim CadenaOrginalComplemento As String = ""
            Dim MensajeError As String = ""
            Dim formaNa As Byte = 0
            If CheckBox3.Checked Then formaNa = 1
            Dim C As New dbVentas(MySqlcon)
            Dim Desglozar As Byte

            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If C.RevisaConceptos(idVenta) = False Then
                'If MsgBox("Hay conceptos en pesos y en dolares si es correcto seleccione Aceptar si no de click en Cancelar.", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                MsgBox("Hay conceptos en pesos y en dolares solo se pueden hacer facturas con conceptos en un tipo de moneda.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
                'End If
            Else
                ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.DaMoneda(idVenta))
            End If

            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasAlta, PermisosN.Secciones.Ventas) = False And pEstado <> Estados.Cancelada Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasCancelar, PermisosN.Secciones.Ventas) = False And pEstado = Estados.Cancelada Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If C.RevisaEstado(idVenta) And pEstado = Estados.Guardada Then
                MsgBox("Esta factura ya ha sido procesada en otra computadora.", MsgBoxStyle.Information, GlobalNombreApp)
                Nuevo()
                Exit Sub
            End If
            If UsaAdenda > 0 And pEstado = Estados.Guardada Then
                Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
                Select Case UsaAdenda
                    Case 1
                        Dim frmA As New frmAddendaFemsa(0, If(IDsMonedas.Valor(ComboBox2.SelectedIndex) = 2, "MXN", "USD"), idVenta, pEstado, Eselectronica, S.Email, True)
                        frmA.ShowDialog()
                        XMLAdenda = frmA.XMLAdenda
                        frmA.Dispose()
                    Case 2
                        Dim frmA As New frmAddendaOxxo(idVenta, True)
                        frmA.elementos = Articulos
                        frmA.Serie = TextBox11.Text
                        frmA.Folio = TextBox2.Text
                        frmA.Doc = 0
                        frmA.EsElectronica = Eselectronica
                        frmA.Total = CDbl(Label14.Text)
                        frmA.TipodeCambio = CDbl(TextBox10.Text)
                        frmA.Moneda = If(IDsMonedas.Valor(ComboBox2.SelectedIndex) = 2, "MXN", "USD")
                        frmA.ShowDialog()
                        XMLAdenda = frmA.XMLResultado
                        frmA.Dispose()
                    Case 3
                        C.DaTotal(idVenta, 2, Op._Sinnegativos, Op._CalculoAlterno)
                        Dim Suc As New dbSucursales(C.IdSucursal, MySqlcon)
                        Dim FrmA As New frmAddendaLey(idVenta, C.TotalDescuento, Eselectronica, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Suc.Email)
                        FrmA.ShowDialog()
                        XMLAdenda = FrmA.strXML
                        FrmA.Dispose()
                    Case 5
                        'Addenda modelo
                        Dim moneda As New dbMonedas(IDsMonedas2.Valor(ComboBox2.SelectedIndex), MySqlcon)
                        Dim frmAm As New frmAdendaGrupoModelo(idVenta, moneda.Abreviatura, TextBox10.Text)
                        frmAm.ShowDialog()
                        XMLAdenda = frmAm.adendaXML
                        frmAm.Dispose()
                    Case 6
                        Dim frmA As New frmComplementoINE(idVenta)
                        frmA.ShowDialog()
                        XMLAdenda = frmA.XML
                        CadenaOrginalComplemento = frmA.cadenaOriginal
                        frmA.Dispose()
                    Case 7
                        C.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
                        Dim frmA As New frmExportacion(idVenta, C.TotalVenta)
                        frmA.ShowDialog()
                        XMLAdenda = frmA.xml
                        CadenaOrginalComplemento = frmA.cadenaOriginal
                        frmA.Dispose()
                    Case 8 'Soriana
                        Dim frmA As New frmAddendaSoriana(idVenta)
                        frmA.ShowDialog()
                        XMLAdenda = frmA.addenda.CreaXML
                        frmA.Dispose()
                End Select

            End If
            Button1.Enabled = False
            Button14.Enabled = False

            If IsNumeric(TextBox16.Text) = False Then
                MensajeError += " El descuento debe ser un valor numérico."
            End If
            If IsNumeric(TextBox18.Text) = False Then
                MensajeError += " El sobre impuesto local debe ser numérico."
            End If
            If IsNumeric(TextBox19.Text) = False Then
                MensajeError += " El descuento 2 debe ser un valor numérico."
            End If
            C.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
            If IsNumeric(TextBox2.Text) = False Then MensajeError += "El folio debe ser un valor numérico."
            If ComboBox6.Text <> "" Then
                If ComboBox6.Text.Length < 4 Then MensajeError += "El número de cuenta debe ser por lo menos de 4 dígitos."
            End If
            If IsNumeric(TextBox15.Text) Then
                If idsFormasDePago.Valor(ComboBox4.SelectedIndex) = 98 And CInt(TextBox15.Text) <= 1 And Op.NParcialidades = 0 And pEstado <> Estados.Cancelada Then
                    MensajeError += "La cantidad de parcialidades debe ser mayor a uno."
                End If
            Else
                MensajeError += " Las parcialidades deben llevar un valor numérico."
            End If
            Dim FP As New dbFormasdePago(idsFormasDePago.Valor(ComboBox4.SelectedIndex), MySqlcon)
            If IdVentaOrigen = 0 And FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
                MensajeError += " No se puede crear una parcialidad sin indicar su factura origen."
            End If
            'If FolioAnt <> TextBox2.Text Then

            'End If
            If DateTimePicker1.Value.ToString("yyyy/MM/dd") < DateAdd(DateInterval.Day, -2, Date.Now).ToString("yyyy/MM/dd") And pEstado = Estados.Guardada Then
                MensajeError += "Fecha no válida. No puede ser mayor a 3 días atrás."
            End If
            If (Saldo + C.TotalVenta > CreditoCliente Or SinCredito) And FP.Tipo = dbFormasdePago.Tipos.Credito And Op._LimitarCredito = 1 Then
                If pEstado = Estados.Guardada Then
                    MensajeError += " El cliente exede de su límite de crédito."
                End If
            End If
            If SaldoaFavor - (-1 * C.TotalAmortizacion(idVenta)) < 0 And pEstado = Estados.Guardada Then
                MensajeError += " Saldo a favor insuficiente, debe indicar una cantidad menor."
            End If
            If idRemisiones.Length = 1 And idRemisiones(0) = 0 And TipoCreardesde = 1 Then
                If pEstado = Estados.Guardada And GlobalSoloExistencia = True And CheckBox2.Checked = False Then MensajeError += C.VerificaExistencias(idVenta)
            End If
            If Op.ChecaFolioFacturas = 0 Then
                If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
                    If pEstado = Estados.Guardada Then
                        MensajeError += "Folio repetido."
                    End If
                    'TextBox2.Text = C.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion).ToString
                End If
            End If
            Dim TotalAgregado As Double = MetodosDePago.TotalAgregado(0, idVenta)
            If pEstado = Estados.Guardada And Math.Round(TotalAgregado, 2) <> Math.Round(TotalVenta, 2) And TotalAgregado > 0 Then
                MensajeError += " Los métodos de pago no estan agregados correctamente."
            End If

            If MensajeError = "" Then
                Dim PorSutir As Byte
                Desglozar = 0
                If CheckBox2.Checked Then
                    PorSutir = 1
                Else
                    PorSutir = 0
                End If
                'Dim O As New dbOpciones(MySqlcon)


                'Dim Credito As Byte
                'Credito = FP.Tipo
                C.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
                Dim Sf As New dbSucursalesFolios(MySqlcon)
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Factura, Eselectronica)
                Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                Dim iIdFormaPago As Integer = idsFormasDePago.Valor(ComboBox4.SelectedIndex)

                If FP.Tipo <> dbFormasdePago.Tipos.Parcialidad Then
                    'iIdFormaPago = 98
                    IdVentaOrigen = 0
                    Parcialidad = 1
                    If Op.NParcialidades = 0 Then
                        Parcialidades = CInt(TextBox15.Text)
                    Else
                        Parcialidades = 1
                    End If
                End If
                AddError("Folio:" + TextBox11.Text + TextBox2.Text + " Cliente:" + idCliente.ToString + " Suc:" + ComboBox3.Text + " Fecha: " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " Estado:" + pEstado.ToString, "Facturacion antes de guardar", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idVenta)
                C.Modificar(idVenta, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, TextBox11.Text, Sf.NoAprobacion, Sc.NoSerie, Sf.YearAprobacion, iTipoFacturacion, pEstado, iIdFormaPago, 0, CDbl(TextBox10.Text), IDsMonedas2.Valor(ComboBox2.SelectedIndex), C.Subtototal, C.TotalVenta, idCliente, IdsVendedores.Valor(ComboBox5.SelectedIndex), TextBox14.Text, ComboBox6.Text, CDbl(TextBox16.Text), 0, IdVentaOrigen, Parcialidad, Parcialidades, PorSutir, RefDocumento, Adicional, CDbl(TextBox19.Text), CDbl(TextBox18.Text), formaNa, Op.ChecaFolioFacturas, Op._ModoFoliosB, DateTimePicker2.Value.ToString("yyyy/MM/dd"), ComboBox9.Text.Substring(0, 3), TextBox13.Text, TextBox20.Text)

                Dim CM As New dbMonedasConversiones(MySqlcon)
                CM.Modificar(1, CDbl(TextBox10.Text))
                Estado = pEstado
                If pEstado = Estados.Cancelada Then
                    Dim S As New dbInventarioSeries(MySqlcon)
                    Dim Ligada As Integer
                    Ligada = C.VienedeRemision(idVenta)
                    C.DesligaRemisiones(idVenta)
                    C.DesligaPagos(idVenta)
                    'Ligada = C.VienedeFertilizantePedido(idVenta)
                    C.DesligaFertilizantesPedidos(idVenta)
                    S.QuitaSeriesAVenta(idVenta)
                    If Ligada = 0 Then C.RegresaInventario(idVenta)
                    If MsgBox("¿Imprimir Factura Cancelada?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Select Case Eselectronica
                            Case 0
                                Imprimir(idVenta)
                            Case 1
                                CadenaOriginal(pEstado, XMLAdenda)
                            Case 2
                                CadenaOriginali(pEstado, XMLAdenda, CadenaOrginalComplemento, False, True)
                            Case 3
                                CadenaOriginali33(Estado, XMLAdenda, CadenaOrginalComplemento, True, True)
                        End Select

                    End If
                End If
                If pEstado = Estados.Guardada Then
                    C.ModificaInventario(idVenta, PorSutir)
                    If TotalAgregado = 0 Then
                        MetodosDePago.Guardar(0, idsFormasDePago.Valor(ComboBox4.SelectedIndex), C.TotalVenta, idVenta)
                    End If
                    'If idRemisiones Is Not Nothing Then
                    If TipoCreardesde = 1 Then
                        If idRemisiones.Length <> 0 Then
                            If idRemisiones(0) <> 0 Then
                                Dim VR As New dbVentasRemisiones(MySqlcon)
                                VR.Usar(idRemisiones, idVenta)
                                C.Usar(idVenta)
                            End If
                        End If
                    End If
                    If TipoCreardesde = 2 Then
                        If idRemisiones.Length <> 0 Then
                            If idRemisiones(0) <> 0 Then
                                Dim VR As New dbFertilizantesPedido(MySqlcon)
                                VR.Usar(idRemisiones, idVenta)
                                C.Usar(idVenta)
                            End If
                        End If
                    End If
                    'End If
                    If TipoCreardesde = 3 Then
                        If idRemisiones.Length <> 0 Then
                            If idRemisiones(0) <> 0 Then
                                Dim VR As New dbVentasPagosRemisiones(MySqlcon)
                                VR.Usar(idRemisiones, idVenta)
                            End If
                        End If
                    End If

                    Select Case Eselectronica
                        Case 0
                            Imprimir(idVenta)
                        Case 1
                            CadenaOriginal(pEstado, XMLAdenda)
                        Case 2
                            'If CadenaOriginali(pEstado, XMLAdenda, CadenaOrginalComplemento, False, False) = False Then
                            'If CadenaOriginali(pEstado, XMLAdenda, CadenaOrginalComplemento, True, False) = False Then
                            CadenaOriginali(pEstado, XMLAdenda, CadenaOrginalComplemento, True, True)
                            'End If
                            'End If
                        Case 3
                            CadenaOriginali33(pEstado, XMLAdenda, CadenaOrginalComplemento, True, True)
                    End Select
                End If

                If Op.IntegrarBancosVentasContado = 1 And (FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad) And GlobalConBancos And GlobalPermisos.ChecaPermiso(PermisosN.Bancos.DepositosVer, PermisosN.Secciones.Bancos) = True And pEstado = Estados.Guardada Then
                    Dim PP As New frmDeposito(Op.IntegrarBancosVentasContado, "FACTURA: " + TextBox11.Text + TextBox2.Text, ComboBox4.Text.Replace("CONTADO-", ""), "", C.TotalVenta, idCliente, idVenta.ToString, Format(DateTimePicker1.Value, "dd/MM/yyyy"), 2)
                    'TextBox1.Text=Total pagado
                    If PP.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                        MsgBox("No se completo el ligado a bancos. Para ligar esta factura a bancos lo puede hacer después en un depósito.", MsgBoxStyle.Information, GlobalNombreApp)
                    End If
                    PP.Dispose()
                End If

                If (pEstado = Estados.Guardada Or pEstado = Estados.Cancelada) And idVenta <> 0 Then
                    GeneraPoliza()
                End If
                If pEstado <> Estados.SinGuardar Then
                    Nuevo()
                Else
                    Button1.Enabled = True
                    Button14.Enabled = True
                End If
            Else
                MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
                Button1.Enabled = True
                Button14.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            'If Estado = Estados.Guardada Then
            '    Nuevo()
            'Else
            AddError(Replace(ex.Message, "'", "''"), "Ventas - Modificar", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idVenta)
            Dim EV As New dbVentas(MySqlcon)
            EV.RegresaInventario(idVenta)
            Estado = Estados.Pendiente
            Button1.Enabled = True
            Button14.Enabled = True
            'End If
        End Try
    End Sub
    Private Sub Guardar()
        Try
            If LimitedeFolios = True Then
                MsgBox("Se ha alcanzado el limite de folios.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If CertificadoCaduco = True Then
                MsgBox("El certificado del sello digital esta vencido.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If SinTimbres = True Then
                MsgBox("Los timbres se acabaron o se han caducado.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            'If Button1.Text = "Guardar" Then
            'If (PermisosVentas And CULng((Math.Pow(2, perVentas.Ventas)))) <> 0 Then
            If idCliente <> 0 Then
                Dim C As New dbVentas(MySqlcon)
                Dim Desglozar As Byte
                Desglozar = 0
                If IsNumeric(TextBox2.Text) = False Then
                    MsgBox("El folio debe ser un valor numérico", MsgBoxStyle.Critical, GlobalNombreApp)
                    Exit Sub
                End If
                If IsNumeric(TextBox19.Text) = False Then TextBox19.Text = "0"
                If C.ChecaFolioRepetido(CInt(TextBox2.Text), TextBox11.Text) Then
                    TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                    Label17.Visible = True
                    FolioAnt = 0
                Else
                    FolioAnt = TextBox2.Text
                End If
                'Dim O As New dbOpciones(MySqlcon)
                Dim CM As New dbMonedasConversiones(MySqlcon)
                Dim Sf As New dbSucursalesFolios(MySqlcon)
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Factura, iTipoFacturacion)

                Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                CM.Modificar(1, CDbl(TextBox10.Text))
                ComboBox2.SelectedIndex = IDsMonedas2.Busca(IDsMonedas.Valor(ComboBox1.SelectedIndex))
                C.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
                C.Guardar(idCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, TextBox11.Text, Sf.NoAprobacion, Sc.NoSerie, Sf.YearAprobacion, iTipoFacturacion, IdsSucursales.Valor(ComboBox3.SelectedIndex), idsFormasDePago.Valor(ComboBox4.SelectedIndex), CDbl(TextBox10.Text), IDsMonedas2.Valor(ComboBox2.SelectedIndex), Isr, IvaRetenido, IdsVendedores.Valor(ComboBox5.SelectedIndex), 0, CDbl(TextBox19.Text), CDbl(TextBox18.Text), ComboBox9.Text.Substring(0, 3), TextBox13.Text, TextBox20.Text)
                idVenta = C.ID
                Button33.Enabled = False
                If ActivarImpuestos = 1 Then
                    Dim IL As New dbClientesImpuestos(MySqlcon)
                    IL.InsertaImpuestos(idVenta, idCliente)
                End If
                Estado = 1
                'Button1.Text = "Modificar"
                Button2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button15.Enabled = True
                Button21.Enabled = True
                Button30.Enabled = True
                Button11.Enabled = False
                ComboBox3.Enabled = False
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirPendienteVentas, PermisosN.Secciones.Ventas) = False Then
                    Button1.Enabled = False
                End If
                'LlenaDatosDetalles()
            Else
                MsgBox("Debe indicar un cliente", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
            'Else
            'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            If UsaFormula = 1 And IdInventario <> 0 Then
                Dim Fo As New frmInventarioFormula01(ArtArticulo)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.000")
                    TextBox4.Text = Fo.FormulaString
                End If
                Fo.Dispose()
            End If
            If ComboBox7.Visible = False Then
                TextBox12.Focus()
            Else
                ComboBox7.Focus()
            End If
        End If
        If e.KeyCode = Keys.F1 Then
            buscaArticuloBoton()
        End If
        If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
            If UsaFormula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtArticulo)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.000")
                    TextBox4.Text = Fo.FormulaString
                End If
                TextBox12.Focus()
                Fo.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F2 And IdInventario <> 0 Then
            Dim Precio As Double
            If CantidadMostrar <> 0 Then
                Precio = CDbl(TextBox6.Text) / CantidadMostrar
            Else
                Precio = 0
            End If
            Dim FE As New frmVentasDaEquivalencia(IdInventario, CDbl(TextBox5.Text), CantidadMostrar, TipoCantidadMostrar, Precio, True)
            If FE.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox5.Text = FE.Cantidad.ToString
                CantidadMostrar = FE.CantidadM
                TipoCantidadMostrar = FE.TipoCantidadM
                If FE.Cantidad = FE.CantidadM Then
                    SinConcersion = True
                Else
                    SinConcersion = False
                End If
                If FE.Cantidad <> 0 Then
                    TextBox12.Text = Format((FE.CantidadM * FE.PrecioM) / FE.Cantidad, "0.00")
                Else
                    TextBox12.Text = "0"
                End If
                Label20.Text = CantidadMostrar.ToString
            End If
            FE.Dispose()
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 0, False, True, False, False) Then
                    LlenaDatosArticulo(p)
                Else
                    IdInventario = 0
                    TextBox4.Text = ""
                    TextBox6.Text = "0"
                    TextBox8.Text = "0"
                    TextBox9.Text = "0"
                    PrecioU = 0
                    IdVariante = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub LlenaDatosVenta()
        LlenandoDatos = True
        idRemisiones = idRemisionesCero
        TipoCreardesde = 0
        Dim C As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        TextBox2.Text = C.Folio
        FolioAnt = C.Folio
        TextBox1.Text = C.Cliente.Clave
        Estado = C.Estado
        Button21.Enabled = True
        TextBox8.Text = C.Iva.ToString
        TextBox11.Text = C.Serie
        TextBox10.Text = C.TipodeCambio.ToString
        TextBox14.Text = C.Comentario
        TextBox16.Text = C.Descuento.ToString
        TextBox19.Text = C.DescuentoG2.ToString
        ComboBox6.Text = C.NoCuenta
        TextBox20.Text = C.ClientePG
        IvaRetenido = C.IvaRetenido
        TextBox18.Text = C.SobreEscribeImpLoc.ToString
        Isr = C.ISR

        If C.PorSurtir = 1 Then
            CheckBox2.Checked = True
        Else
            CheckBox2.Checked = False
        End If
        If C.FormaPagoNA = 1 Then
            CheckBox3.Checked = True
        Else
            CheckBox3.Checked = False
        End If
        Button2.Enabled = True
        Eselectronica = C.EsElectronica
        If Eselectronica >= 2 Then
            Button20.Visible = True
        Else
            Button20.Visible = False
        End If
        If Eselectronica = 3 Then
            Label29.Visible = False
            ComboBox6.Visible = False
        Else
            Label29.Visible = True
            ComboBox6.Visible = True
        End If
        RefDocumento = C.RefDocumento
        Adicional = C.Adicional
        TextBox15.Text = C.Parcialidades.ToString
        IdVentaOrigen = C.IdVentaOrigen
        DateTimePicker1.Value = C.Fecha
        DateTimePicker2.Value = C.FechaConta
        Dim FP As New dbFormasdePago(C.IdFormadePago, MySqlcon)
        If C.Estado = Estados.Guardada Or C.Estado = Estados.Cancelada Then ConsultaOn = False
        If FP.Tipo = dbFormasdePago.Tipos.Contado Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If
        If C.Estado = Estados.Guardada Or C.Estado = Estados.Cancelada Then
            ConsultaOn = True
            LlenaCombos("tblformasdepago", ComboBox4, "concat(if(clavesat<1000,lpad(convert(clavesat using utf8),2,'0'),''),' ',nombre)", "nombret", "idforma", idsFormasDePago, , , "idforma")
        End If
        ComboBox4.SelectedIndex = idsFormasDePago.Busca(C.IdFormadePago)
        ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.IdConversion)
        ComboBox5.SelectedIndex = IdsVendedores.Busca(C.IdVendedor)
        TextBox13.Text = C.NoConfirmacion
        ComboBox3.Enabled = False
        Button11.Enabled = False
        Button27.Enabled = True
        Button33.Enabled = False
        Button34.Enabled = True
        Dim KM As New dbkardexdocumentos(MySqlcon)
        If KM.MovimientosFacturaCant(idVenta) > 0 Then
            Label38.Visible = True
        Else
            Label38.Visible = False
        End If
        Label39.Text = "FECHA CANCELACIÓN: " + C.FechaCancelado
        ComboBox9.Text = CSat.DaUsoCFDI(C.cUsoCFDI)
        'ConsultaOn = True
        LlenaDatosDetalles()
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                Button13.Enabled = False
                Panel1.Enabled = False
                cmbAlmacen.Enabled = False
                TextBox20.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
                Button15.Enabled = True
                Button30.Enabled = True
                Button35.Enabled = True
                Label39.Visible = True
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                TextBox20.Enabled = False
                Button2.Enabled = False
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Panel1.Enabled = False
                cmbAlmacen.Enabled = False
                Panel2.Enabled = False
                Button15.Enabled = True
                Button30.Enabled = True
                Button35.Enabled = False
                Label39.Visible = False
            Case Else
                Label39.Visible = False
                Button35.Enabled = False
                Label24.Visible = False
                Button13.Enabled = True
                TextBox20.Enabled = True
                Panel1.Enabled = True
                cmbAlmacen.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button2.Enabled = True
                Button15.Enabled = True
                Button30.Enabled = True
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirDescuento, PermisosN.Secciones.Ventas) = False Then
                    TextBox16.Enabled = False
                End If
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodeFolio, PermisosN.Secciones.Ventas) = False Then
                    TextBox11.Enabled = False
                    TextBox2.Enabled = False
                End If
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
                    ComboBox3.Enabled = False
                End If
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirCambiarFechaVentas, PermisosN.Secciones.Ventas) = False Then
                    DateTimePicker1.Enabled = False
                    DateTimePicker2.Enabled = False
                End If
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirPendienteVentas, PermisosN.Secciones.Ventas) = False Then
                    Button1.Enabled = False
                End If
        End Select
        If GlobalTipoVersion = 3 Then
            For Each cc As Control In Panel4.Controls
                cc.Enabled = False
            Next
            Button3.Enabled = True
            Button10.Enabled = True
            Button15.Enabled = True
            Button20.Enabled = True
            Button27.Enabled = True
            If Estado = Estados.Guardada Then Button13.Enabled = True
        End If
        LlenandoDatos = False
    End Sub
    Private Sub LlenaDatosDetalles()
        Panel1.Visible = True
        ConsultaDetalles()
    End Sub
    Private Sub ConsultaDetalles()
        Try
            If UsaAdenda > 0 Then Articulos.Clear()
            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbVentasInventario(MySqlcon)
            T = CD.ConsultaReader(idVenta, False, "0", 0, Op._OrdenUbicacion, False)
            While T.Read
                If T("cantidad") <> 0 Then
                    If T("noimpimporte") = 0 Then
                        Tabla.Rows.Add(T("idventasinventario"), "A", "", T("cantidadm"), T("tipom"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidadm"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    Else
                        Tabla.Rows.Add(T("idventasinventario"), "A", "", T("cantidadm"), T("tipom"), T("clave"), T("descripcion"), Format(T("noimpimporte"), "0.00"), Format(T("noimpimporte"), "0.00"), T("abreviatura"))
                    End If
                Else
                    'If T("noimpimporte") = 0 Then
                    Tabla.Rows.Add(T("idventasinventario"), "A", "", T("cantidadm"), T("tipom"), T("clave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                    'Else
                    'Tabla.Rows.Add(T("idventasinventario"), "A", "", T("cantidadm"), T("tipom"), T("clave"), T("descripcion"), "0.00", Format(T("noimpimporte"), "0.00"), T("abreviatura"))
                    'End If
                End If
                If UsaAdenda > 0 Then
                    Dim Ar As New Articulo()
                    Ar.cantidad = T("cantidadm")
                    Ar.porcIva = T("iva")
                    Ar.ImporteNeto = T("precio") * (1 + T("iva") / 100)
                    Ar.montoIva = T("precio") * (T("iva") / 100)
                    Ar.descripcion = T("descripcion")
                    Ar.unidadMedida = T("tipom")
                    Articulos.Add(Ar)
                End If
            End While
            T.Close()

            DGDetalles.DataSource = Tabla
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).Visible = False
            DGDetalles.Columns(2).Visible = False
            DGDetalles.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(4).Width = 80
            DGDetalles.Columns(9).Width = 80
            DGDetalles.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(6).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(7).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(8).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(9).SortMode = DataGridViewColumnSortMode.NotSortable
            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub NuevoConcepto()
        IdInventario = 0
        IdVariante = 0
        IdServicio = 0
        PrecioNeto = 0
        PorLotes = 0
        Aduana = 0

        Button25.Enabled = False
        Button28.Enabled = False
        ArtArticulo = ""
        CostoArticulo = 0
        CantidadMostrar = 0
        UsaFormula = 0
        TipoCantidadMostrar = 0
        TipoCantidad = 0
        SeparaKit = 0
        Esamortizacion = 0
        TextBox12.Text = "0"
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "0"
        TextBox9.Text = "0"
        TextBox17.Text = "0"
        TextBox6.Text = "0"
        Label20.Text = "0"
        'txtIEPS.Text = "0"
        'txtIVARetenido.Text = "0"
        PrecioBase = 0
        Button32.Visible = False
        ComboBox7.Text = ""
        Button12.Visible = False
        'Button25.Visible = False
        Button9.Enabled = False
        SinConcersion = False
        TextBox3.Enabled = True
        Button6.Enabled = True
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        Button28.Enabled = False
        ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Agregar Concepto"
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodeAlmacen, PermisosN.Secciones.Ventas) = False Or Estado > 2 Then
            cmbAlmacen.Enabled = False
        Else
            cmbAlmacen.Enabled = True
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioDescripcion, PermisosN.Secciones.Ventas) = False Then
            TextBox4.Enabled = False
            Button16.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirDescuento, PermisosN.Secciones.Ventas) = False Then
            TextBox9.Enabled = False
        End If
        If Op._CursorVentas = "0" Then
            TextBox5.Focus()
        Else
            TextBox5.Text = "1"
            TextBox3.Focus()
        End If

        pnlUbicacion.Visible = False
        cmbUbicacion.Enabled = True
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbVentasInventario(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            Dim EsCrearDesde As Boolean = False
            If IdInventario = 0 Then
                MsgError += "Debe indicar un artículo."
                HayError = True
            End If
            If IsNumeric(txtIEPS.Text) = False Then
                MsgError += vbCrLf + "El IEPS debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(txtIVARetenido.Text) = False Then
                MsgError += vbCrLf + "El IVA Retenido debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(TextBox8.Text) = False Then
                MsgError += vbCrLf + "El IVA Retenido debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(TextBox10.Text) = False Then
                MsgError += vbCrLf + "El tipo de cambio debe ser un valor numérico."
                HayError = True
            End If
            If FacturaGlobal.RevisaEstado(idVenta) Then
                MsgBox("Esta factura ya esta guardada no se puede modificar.", MsgBoxStyle.Information, GlobalNombreApp)
                Nuevo()
                Exit Sub
            End If
            If TipoCreardesde = 1 Then
                If Button4.Text = "Agregar Concepto" Then
                    If idRemisiones.Length <> 0 Then
                        If idRemisiones(0) <> 0 And Op.NoBloquearCreardesde = 0 Then
                            MsgError += "No se puede agregar mas conceptos en una factura que se generó de ""Crear desde""."
                            HayError = True
                        End If
                    End If
                Else
                    If idRemisiones.Length <> 0 Then
                        If idRemisiones(0) <> 0 Then
                            EsCrearDesde = True
                        End If
                    End If
                End If
            End If
            If Almacen.TienePermiso(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex)) = False Then
                HayError = True
                MsgError += vbCrLf + " No tiene permiso para realizar operaciones en el almacén seleccionado."
            End If
            Dim I As New dbInventario(IdInventario, MySqlcon)
            If IsNumeric(TextBox5.Text) Then
                If CDbl(TextBox5.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += "La cantidad debe ser un valor mayor a 0."
                    HayError = True
                End If
            Else
                MsgError += "La cantidad debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(TextBox12.Text) = False Then
                MsgError += vbCrLf + "El precio debe ser un valor numérico."
                HayError = True
            Else
                If IsNumeric(TextBox9.Text) = False Then
                    MsgError += vbCrLf + "El descuento debe ser un valor numérico."
                    HayError = True
                Else
                    If CDbl(TextBox9.Text) <> 0 Then
                        TextBox12.Text = CStr(CDbl(TextBox12.Text) - (CDbl(TextBox12.Text) * CDbl(TextBox9.Text) / 100))
                    End If
                End If
                If HayError = False Then
                    Dim PrecioaComparar As Double = CDbl(TextBox12.Text)
                    If IDsMonedas.Valor(ComboBox1.SelectedIndex) <> 2 Then
                        PrecioaComparar = PrecioaComparar * CDbl(TextBox10.Text)
                    End If
                    If PrecioNeto = 1 Then
                        PrecioaComparar = PrecioaComparar / (1 + (CDbl(TextBox8.Text) + CDbl(txtIEPS.Text) - CDbl(txtIVARetenido.Text)) / 100)
                    End If
                    If Op._AvisoCosto = "1" And GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirVentaBajoCosto, PermisosN.Secciones.Ventas) = True And I.Inventariable = 1 Then
                        If PrecioaComparar < CostoArticulo Then
                            If MsgBox("El precio del artículo está debajo del costo. ¿Agregar el concepto de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                                MsgError += "Precio debajo del costo."
                                HayError = True
                            End If
                        End If
                    End If
                    If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirVentaBajoCosto, PermisosN.Secciones.Ventas) = False And PrecioaComparar < CostoArticulo And I.Inventariable = 1 Then
                        MsgError += " No se puede vender un artículo debajo de su costo."
                        HayError = True
                    End If
                End If
                If Esamortizacion = 1 Then
                    If SaldoaFavor - (-1 * CDbl(TextBox6.Text)) < 0 Then
                        MsgError += " Saldo a favor insuficiente, debe indicar una cantidad menor."
                        HayError = True
                    End If
                End If
                If CDbl(TextBox6.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += " El precio debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If


            If IdInventario <> 0 And GlobalSoloExistencia = True And I.Inventariable = 1 And CheckBox2.Checked = False And EsCrearDesde = False Then
                Dim Cant As Double
                Cant = I.DaInventario(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), IdInventario)
                If I.Contenido <> 1 Then
                    Cant = Cant * I.Contenido
                End If
                If Cant < CDbl(TextBox5.Text) And I.Inventariable = 1 Then
                    MsgError += " Artículo sin existencia suficiente." + vbCrLf + "Cantidad disponible: " + Cant.ToString + vbCrLf + "Cantidad solicitada: " + TextBox5.Text + vbCrLf + "Diferencia: " + CStr(Cant - CDbl(TextBox5.Text))
                    HayError = True
                End If
                If EsKit = 1 Then
                    Dim Str As String
                    Str = I.ChecaInventarioKits(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), IdInventario, CDbl(TextBox5.Text))
                    MsgError += Str
                    If Str <> "" Then HayError = True
                End If
            End If
            If HayError = False Then

                If PrecioNeto = 1 Then
                    Dim Temp As Double
                    Temp = CStr(CDbl(TextBox6.Text) / (1 + (CDbl(TextBox8.Text) - Isr - IvaRetenido + CDbl(txtIEPS.Text) - CDbl(txtIVARetenido.Text)) / 100) / CDbl(TextBox5.Text))
                    TextBox12.Text = Temp.ToString
                End If
                If CantidadMostrar = 0 Then TipoCantidadMostrar = TipoCantidad
                If CantidadMostrar = 0 Then CantidadMostrar = CDbl(TextBox5.Text)
                If SinConcersion Then CantidadMostrar = CDbl(TextBox5.Text)
                If Button4.Text = "Agregar Concepto" Then
                    If SeparaKit = 0 Then
                        CD.Guardar(idVenta, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), Trim(TextBox4.Text), IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, IdServicio, I.Inventariable, CantidadMostrar, TipoCantidadMostrar, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), ComboBox7.Text, CDbl(TextBox17.Text), If(pnlUbicacion.Visible, cmbUbicacion.SelectedValue, ""), txtTarima.Text)
                        'agregar descuento
                        hayDescuento()
                        IdDetalle = CD.ID
                    Else
                        If EsKit = 1 And SeparaKit = 1 Then
                            CD.SeparaKit(idVenta, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), Trim(TextBox4.Text), IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, IdServicio, I.Inventariable, CantidadMostrar, TipoCantidadMostrar, CDbl(txtIEPS.Text), CDbl(txtIVARetenido.Text), CDbl(TextBox17.Text))
                            IdDetalle = 0
                        End If
                    End If
                    If EsKit = 1 And SeparaKit = 0 Then
                        Dim IKits As New dbVentasKits(MySqlcon)
                        IKits.InsertarArticulos(IdInventario, idVenta, CD.ID, CDbl(TextBox5.Text), IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex))
                    End If
                    If CheckBox2.Checked Then CheckBox2.Enabled = False
                    If IdInventario <> 0 Then
                        If ManejaSeries <> 0 Then
                            If CD.NuevoConcepto Then
                                Dim F As New frmVentasAsignaSeries(IdInventario, idVenta, 0, CInt(TextBox5.Text))
                                F.ShowDialog()
                                F.Dispose()
                            Else
                                Dim F As New frmVentasAsignaSeries(IdInventario, idVenta, 0, CD.Cantidad)
                                F.ShowDialog()
                                F.Dispose()
                            End If
                        End If
                        If I.KitconSerie(IdInventario) > 0 Then
                            Dim IDe As New frmInventarioDetalles(IdInventario, 1, IdDetalle, idVenta)
                            IDe.ShowDialog()
                            IDe.Dispose()
                        End If
                        If PorLotes = 1 Then
                            Dim F As New frmInventarioLotes(IdDetalle, 0, 0, 0, Math.Round(CDbl(TextBox5.Text) / Contenido, 4), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        If Aduana = 1 Then
                            Dim F As New frmInventarioAduana(IdDetalle, 0, 0, 0, Math.Round(CDbl(TextBox5.Text) / Contenido, 4), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        'Dim I As New dbInventario(MySqlcon)
                        'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), 0, dbInventario.TipoMovimiento.Baja, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    End If
                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo agregado", 90)
                Else
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CD.BuscaridInventario(IdDetalle), fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex)))
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), CantidadMostrar, TipoCantidadMostrar, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), ComboBox7.Text, CDbl(TextBox17.Text), If(pnlUbicacion.Visible, cmbUbicacion.SelectedValue, ""), txtTarima.Text)
                    If tipoElimianr = "Promocion" Then
                        modificarDescuento(P.descModificar(IdDetalle, "VentasN"))
                    Else
                        If P.descModificar(IdDetalle, "VentasN") <> 0 Then
                            modificarDescuento(P.descModificar(IdDetalle, "VentasN"))
                        End If
                    End If

                    If EsKit = 1 Then
                        Dim IKits As New dbVentasKits(MySqlcon)
                        IKits.ModificaArtículos(IdDetalle, CDbl(TextBox5.Text), IdInventario)
                    End If
                    If IdInventario <> 0 Then
                        If ManejaSeries <> 0 Then
                            Dim F As New frmVentasAsignaSeries(IdInventario, idVenta, 0, CDbl(TextBox5.Text))
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        If I.KitconSerie(IdInventario) > 0 Then
                            Dim IDe As New frmInventarioDetalles(IdInventario, 1, IdDetalle, idVenta)
                            IDe.ShowDialog()
                            IDe.Dispose()
                        End If
                        If PorLotes = 1 Then
                            Dim F As New frmInventarioLotes(IdDetalle, 0, 0, 0, Math.Round(CDbl(TextBox5.Text) / Contenido, 4), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        If Aduana = 1 Then
                            Dim F As New frmInventarioAduana(IdDetalle, 0, 0, 0, Math.Round(CDbl(TextBox5.Text) / Contenido, 4), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        'Dim I As New dbInventario(MySqlcon)
                        'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), CantAnt, dbInventario.TipoMovimiento.CambioBaja, IdAlmacen)
                    End If
                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo modificado", 90)
                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()
    End Sub
    Private Sub BotonAgregar()
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasAlta, PermisosN.Secciones.Ventas) = True Then
            If Op._TipoSelAlmacen = "1" Then
                If cmbAlmacen.SelectedIndex <= 0 Then
                    MsgBox("Debe seleccionar un almacen.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
            End If
            If Estado = 0 And IdInventario <> 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                If IdInventario <> 0 Then AgregaArticulo()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            TextBox3.Focus()
        End If
    End Sub
    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try

            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbVentasInventario(IdDetalle, MySqlcon)
            ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            IdInventario = CD.Idinventario
            IdVariante = CD.idVariante
            ArtArticulo = CD.Inventario.Nombre
            TextBox3.Enabled = False
            Button6.Enabled = False
            'If IdInventario > 1 Then
            ConsultaOn = False
            TextBox3.Text = CD.Inventario.Clave
            PrecioNeto = CD.Inventario.PrecioNeto
            UsaFormula = CD.Inventario.UsaFormula
            CostoArticulo = CD.Inventario.CostoBase
            Contenido = CD.Inventario.Contenido
            If CD.Inventario.Contenido > 1 Then
                CostoArticulo = CostoArticulo / CD.Inventario.Contenido
            End If
            Esamortizacion = CD.Inventario.EsAmortizacion
            EsKit = CD.Inventario.EsKit
            ConsultaOn = True
            IdVariante = 0
            PorLotes = CD.Inventario.PorLotes
            If PorLotes = 1 Then
                Button28.Enabled = True
            Else
                Button28.Enabled = False
            End If
            Aduana = CD.Inventario.Aduana
            If Aduana = 1 Then
                Button25.Enabled = True
            Else
                Button25.Enabled = False
            End If
            If CD.Inventario.ManejaSeries = 1 Then
                Button12.Visible = True
            Else
                Button12.Visible = True
            End If
            If CD.Inventario.Inventariable = 1 Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioDescripcion, PermisosN.Secciones.Ventas) = False Then
                    TextBox4.Enabled = False
                    Button16.Enabled = False
                Else
                    TextBox4.Enabled = True
                    Button16.Enabled = True
                End If
            Else
                TextBox4.Enabled = True
                Button16.Enabled = True
            End If
            cmbAlmacen.Enabled = False
            'Else
            'PrecioNeto = 0
            'End If
            'If IdVariante > 1 Then
            '    Dim P As New dbProductos(CD.Producto.IdProducto, MySqlcon)
            '    ConsultaOn = False
            '    TextBox3.Text = P.Clave
            '    ConsultaOn = True
            '    IdInventario = 0
            '    cmbVariante.Visible = True
            'End If
            'Button25.Visible = True
            If Estado = Estados.Cancelada Or Estado = Estados.Guardada Then
                Button32.Visible = True
            Else
                Button32.Visible = False
            End If
            CantidadMostrar = CD.CantidadM
            TipoCantidadMostrar = CD.TipoCantidadM
            TipoCantidad = TipoCantidadMostrar
            Label20.Text = CantidadMostrar.ToString
            TextBox5.Text = CD.Cantidad.ToString
            TextBox8.Text = CD.Iva.ToString
            TextBox9.Text = CD.Descuento.ToString
            TextBox17.Text = CD.CDescuento.ToString
            txtIEPS.Text = CD.IEPS.ToString
            txtIVARetenido.Text = CD.ivaRetencion.ToString
            ComboBox7.Text = CD.Predial
            If CD.Cantidad = CD.CantidadM Then
                SinConcersion = True
            Else
                SinConcersion = False
            End If
            If CD.Cantidad = 0 Then
                PrecioU = 0
            Else
                If CD.Descuento = 0 Then
                    If PrecioNeto = 0 Then
                        PrecioU = Math.Round(CD.Precio / CD.Cantidad, 2)
                    Else
                        PrecioU = Math.Round(CD.Precio / CD.Cantidad * (1 + ((CD.Iva + CD.IEPS - CD.ivaRetencion) - IvaRetenido - Isr) / 100), 2)
                    End If
                Else
                    Dim Val As Double
                    If CD.Descuento <> 100 Then
                        Val = (CD.Precio / (1 - CD.Descuento / 100))
                    Else
                        Val = CD.Precio
                    End If
                    If PrecioNeto = 0 Then
                        PrecioU = Math.Round(Val / CD.Cantidad, 2)
                    Else
                        PrecioU = Math.Round(Val / CD.Cantidad * (1 + ((CD.Iva + CD.IEPS - CD.ivaRetencion) - IvaRetenido - Isr) / 100), 2)
                    End If
                End If
            End If

            PrecioBase = PrecioU
            TextBox12.Text = PrecioU.ToString("0.00")
            CantAnt = CD.Cantidad
            IdAlmacen = CD.IdAlmacen
            If PrecioNeto = 0 Then
                If CD.Descuento = 0 Then
                    TextBox6.Text = CD.Precio.ToString("0.00")
                Else
                    TextBox6.Text = Format(PrecioU * CD.Cantidad, "0.00")
                End If
            End If
            TextBox4.Text = CD.Descripcion
            nombreProducto = CD.Descripcion
            cantAntModificar = CD.Cantidad
            cmbAlmacen.SelectedIndex = IdsAlmacenes.Busca(CD.IdAlmacen)
            Button4.Text = "Modificar Concepto"
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button9.Enabled = True
            'cmbtipoarticulo.Text = "A"

            pnlUbicacion.Visible = CD.Inventario.UsaUbicacion
            cmbUbicacion.DataSource = CD.Inventario.Ubicaciones(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), IdInventario)
            cmbUbicacion.SelectedValue = CD.Ubicacion
            txtTarima.Text = CD.Tarima
            'cmbUbicacion.Enabled = Estado = Estados.Inicio Or Estado = Estados.Pendiente Or Estado = Estados.SinGuardar

            If CheckScroll.Checked Then TextBox5.Focus()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosVenta()
    End Sub
    Private Sub BotonNuevo()
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta factura no ha sido guardada. ¿Desea iniciar una nueva factura? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesAVenta(idVenta)
                Dim C As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
                'If Estado = Estados.Guardada And C.Credito = 0 Then
                '    Dim Cliente As New dbClientes(MySqlcon)
                '    Cliente.ModificaSaldo(idCliente, C.TotalaPagar, 1)
                'End If
                'C.RegresaInventario(idVenta)
                C.Eliminar(idVenta)
                P.limpiarDescPromociones()
                P.limpiarVentasdesc()
                AddError("Se eliminó factura con folio: " + TextBox11.Text + TextBox2.Text, "ventas eliminar boton nuevo.", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm"), idVenta)
                Nuevo()
            End If
        Else
            Nuevo()
        End If
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        BotonNuevo()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NuevoConcepto()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasAlta, PermisosN.Secciones.Ventas) = True Then
                If MsgBox("¿Desea eliminar este concepto de la factura?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbVentasInventario(MySqlcon)
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CD.BuscaridInventario(IdDetalle), fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex)))
                    CD.Eliminar(IdDetalle)
                    If tipoElimianr = "Promocion" Then
                        eliminarDescuescuento(P.descModificar(IdDetalle, "VentasN"), tipoElimianr)
                    Else
                        If P.descModificar(IdDetalle, "VentasN") <> 0 Then
                            eliminarDescuescuento(P.descModificar(IdDetalle, "VentasN"), tipoElimianr)
                        End If
                    End If
                    Dim S As New dbInventarioSeries(MySqlcon)
                    S.QuitarSeriesAventaxArticulo(IdInventario, idVenta)
                    If EsKit = 1 Then
                        Dim IKits As New dbVentasKits(MySqlcon)
                        IKits.EliminarArticulos(IdDetalle)
                    End If
                    ConsultaDetalles()
                    NuevoConcepto()
                    PopUp("Concepto Eliminado", 90)
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        BuscaClienteBoton()
    End Sub
    Private Sub BuscaClienteBoton()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            If B.Cliente.NoChecarCr = 0 Then
                Saldo = B.Cliente.DaSaldoAFecha(B.Cliente.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
                SaldoaFavor = CDbl(Format(B.Cliente.DaSaldoAFavor(B.Cliente.ID), "0.00"))
            Else
                Saldo = 0
                SaldoaFavor = 0
            End If
            CreditoCliente = B.Cliente.Credito

            'If B.Cliente.DireccionFiscal = 0 Then
            TextBox7.Text = B.Cliente.RFC + " " + B.Cliente.Nombre '+ vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.CP
            'Else
            '   TextBox7.Text = B.Cliente.RFC + " " + B.Cliente.Nombre '+ vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion2 + " " + B.Cliente.NoExterior2 + " " + B.Cliente.Ciudad2 + " " + B.Cliente.CP2
            'End If
            UsaAdenda = B.Cliente.UsaAdenda
            If B.Cliente.UsaAdenda <> 0 Then
                Button23.Visible = True
            Else
                Button23.Visible = False
            End If
            TextBox7.Text += vbCrLf + "Días/Lím: " + B.Cliente.CreditoDias.ToString + "/" + Format(B.Cliente.Credito, "#,##0.00") + " " + "Saldo: " + Format(Saldo, "#,##0.00") + " " + "A Favor: " + Format(SaldoaFavor, "#,##0.00")
            TextBox7.Text += vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.Estado + " " + B.Cliente.CP
            If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                'If (B.Cliente.Credito > 0 Or B.Cliente.CreditoDias > 0) And Op.NoPermitirFacturasdeCredito = 0 Then
                '    ComboBox4.SelectedIndex = 1
                'Else
                '    ComboBox4.SelectedIndex = 0
                'End If
                Dim FP As New dbFormasdePago(B.Cliente.IdFormaF, MySqlcon)
                If FP.Tipo = dbFormasdePago.Tipos.Contado Or (Op.NoPermitirFacturasdeCredito = 1 Or GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirVentasCredito, PermisosN.Secciones.Ventas) = False) Then
                    RadioButton1.Checked = True
                Else
                    RadioButton2.Checked = True
                End If
                If Op.FacturarSoloaCredito = 1 Then
                    RadioButton2.Checked = True
                End If
                ComboBox4.SelectedIndex = idsFormasDePago.Busca(B.Cliente.IdFormaF)
                If IdVendedorU > 0 And Op.VendedorUsuario = 1 Then
                    ComboBox5.SelectedIndex = IdsVendedores.Busca(IdVendedorU)
                Else
                    ComboBox5.SelectedIndex = IdsVendedores.Busca(B.Cliente.IdVendedor)
                End If

            End If
            If Saldo >= B.Cliente.Credito Then
                SinCredito = True
            End If
            If B.Cliente.CreditoDias <> 0 Then
                If B.Cliente.TieneCreditoporFecha(B.Cliente.ID, B.Cliente.CreditoDias) = False Then
                    SinCredito = True
                End If
            End If
            idCliente = B.Cliente.ID
            LlenaCombos("tblclientescuentas", ComboBox6, "cuenta", "nombret", "idcuenta", IdsCuentas, "idcliente=" + idCliente.ToString, "NO APLICA")

            If ComboBox7.Visible Then
                LlenaCombos("tblventasinventario", ComboBox7, "distinct predial", "nombret", "idventasinventario", IdsTemp, "inner join tblventas on tblventasinventario.idventa=tblventas.idventa where predial<>'' and idcliente=" + idCliente.ToString, , "nombret", True)
            End If
            If ComboBox6.Items.Count >= 2 Then
                ComboBox6.SelectedIndex = 1
            Else
                ComboBox6.SelectedIndex = 0
            End If

            If B.Cliente.RFC.Length = 13 Then CSat.LlenaCombos("tblusoscfdi", ComboBox9, "concat(clave,' ',descripcion)", "nombrem", "clave", xSat, , , , True)
            If B.Cliente.RFC.Length = 12 Then CSat.LlenaCombos("tblusoscfdi", ComboBox9, "concat(clave,' ',descripcion)", "nombrem", "clave", xSat, "moral='Sí'", , , True)
            If B.Cliente.RFC = "XAXX010101000" Then
                TextBox20.Visible = True
                Label43.Visible = True
                TextBox7.Height = 28
            Else
                Label43.Visible = False
                TextBox7.Height = 57
                TextBox20.Visible = False
            End If
            ComboBox9.Text = CSat.DaUsoCFDI(B.Cliente.UsoCFDI)
            Isr = B.Cliente.ISR
            IdLista = B.Cliente.IdLista
            IvaRetenido = B.Cliente.IvaRetenido
            SIVA = B.Cliente.IVA
            Sobre = B.Cliente.SobreescribeIVA
            ActivarImpuestos = B.Cliente.ActivarImpuestos
            ConsultaOn = False
            TextBox1.Text = B.Cliente.Clave
            ConsultaOn = True
            B.Dispose()
            If Op._TipoSelAlmacen <> "0" Then
                'If ComboBox8.SelectedIndex <= 0 Then
                cmbAlmacen.Focus()
                'End If
            Else
                If Op._CursorVentas = "0" Then
                    TextBox5.Focus()
                Else
                    TextBox3.Focus()
                End If
            End If
        End If
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        buscaArticuloBoton()
    End Sub
    Private Sub buscaArticuloBoton()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloProducto
        If Op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), True, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        LlenaDatosArticulo(B.Inventario)
                End Select
                If UsaFormula = 1 Then
                    Dim Fo As New frmInventarioFormula01(ArtArticulo)
                    If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TextBox5.Text = Format(Fo.Resultado, "0.00")
                        TextBox4.Text = Fo.FormulaString
                    End If
                    Fo.Dispose()
                End If
                TextBox12.Focus()
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(TipodeBusqueda, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), True, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        LlenaDatosArticulo(B.Inventario)

                End Select
                If UsaFormula = 1 Then
                    Dim Fo As New frmInventarioFormula01(ArtArticulo)
                    If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TextBox5.Text = Format(Fo.Resultado, "0.00")
                        TextBox4.Text = Fo.FormulaString
                    End If
                    Fo.Dispose()
                End If
                TextBox12.Focus()
            End If
            B.Dispose()
        End If
    End Sub
    Private Sub LlenaDatosArticulo(ByVal Articulo As dbInventario)
        Dim a As New dbInventarioPrecios(MySqlcon)
        a.BuscaPrecio(Articulo.ID, IdLista)
        Dim Cant As Double

        Dim Desc As Double = 0
        If idCliente <> 0 Then
            Dim cdesc As New dbClientesDescuentos(MySqlcon)
            Desc = cdesc.buscaDescuento(idCliente, Articulo.Clasificacion.ID, Articulo.Clasificacion2.ID, Articulo.Clasificacion3.ID)
            If Desc = -1000 Then
                Desc = cdesc.buscaDescuento(idCliente, Articulo.Clasificacion.ID, Articulo.Clasificacion2.ID, 0)
                If Desc = -1000 Then
                    Desc = cdesc.buscaDescuento(idCliente, Articulo.Clasificacion.ID, 0, 0)
                    If Desc = -1000 Then
                        Desc = 0
                    End If
                End If
            End If
        End If
        TextBox9.Text = Desc.ToString()

        TextBox4.Text = Articulo.Nombre
        txtIEPS.Text = Articulo.ieps
        ArtArticulo = Articulo.Nombre
        txtIVARetenido.Text = Articulo.ivaRetenido
        'a = Articulo.DaPrecioDefault
        If IsNumeric(TextBox5.Text) Then
            Cant = CDbl(TextBox5.Text)
        Else
            TextBox5.Text = "1"
            Cant = 1
        End If

        PrecioU = Math.Round(a.Precio, 2)
        PrecioBase = Math.Round(a.Precio, 2) 'a.Precio
        CostoArticulo = Articulo.CostoBase
        Contenido = Articulo.Contenido
        If Articulo.Contenido > 1 Then
            CostoArticulo = CostoArticulo / Articulo.Contenido
        End If
        Esamortizacion = Articulo.EsAmortizacion
        If Op._CodigoPostalLocal = "0" Then
            TipoCantidad = Articulo.TipoContenido.ID
        Else
            TipoCantidad = Articulo.TipoCantidad.ID
        End If
        SeparaKit = Articulo.SepararKit
        EsKit = Articulo.EsKit
        TextBox12.Text = a.Precio.ToString("0.00")
        ManejaSeries = Articulo.ManejaSeries
        TextBox6.Text = Format(Cant * PrecioU, "0.00")

        UsaFormula = Articulo.UsaFormula
        PorLotes = Articulo.PorLotes
        Aduana = Articulo.Aduana
        If Sobre = 0 Then
            TextBox8.Text = Articulo.Iva.ToString
        Else
            TextBox8.Text = SIVA.ToString
        End If
        'cmbtipoarticulo.Text = "A"
        IdInventario = Articulo.ID
        PrecioNeto = Articulo.PrecioNeto
        If ManejaSeries = 0 Then
            Button12.Visible = False
        Else
            Button12.Visible = True
        End If
        ConsultaOn = False
        TextBox3.Text = Articulo.Clave
        TextBox3.Select(TextBox3.Text.Length, 0)
        ComboBox1.SelectedIndex = IDsMonedas.Busca(a.IdMoneda)
        If Articulo.Inventariable = 1 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioDescripcion, PermisosN.Secciones.Ventas) = False Then
                TextBox4.Enabled = False
            Else
                TextBox4.Enabled = True
            End If
        Else
            TextBox4.Enabled = True
        End If

        pnlUbicacion.Visible = Articulo.UsaUbicacion
        cmbUbicacion.DataSource = Articulo.Ubicaciones(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), IdInventario)


        ConsultaOn = True
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        SacaTotal()
    End Sub
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        BotonBuscar(False)
    End Sub
    Private Sub BotonBuscar(pSinTimbrar As Boolean)
        If Estado > 0 And Estado < 2 Then
            If MsgBox("Esta factura no ha sido guardada. ¿Desea continuar? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesAVenta(idVenta)
                Dim C As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
                'If Estado = Estados.Guardada And C.Credito = 0 Then
                '    Dim Cliente As New dbClientes(MySqlcon)
                '    Cliente.ModificaSaldo(idCliente, C.TotalaPagar, 1)
                'End If
                'C.RegresaInventario(idVenta)
                C.Eliminar(idVenta)
                P.limpiarDescPromociones()
                P.limpiarVentasdesc()
                AddError("Se eliminó factura con folio: " + TextBox11.Text + TextBox2.Text, "ventas eliminar boton buscar.", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm"), idVenta)
            Else
                Exit Sub
            End If
        End If
        Dim f As New frmVentasConsulta(ModosDeBusqueda.Secundario, 0, pSinTimbrar)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idVenta = f.IdVenta
            LlenaDatosVenta()
            NuevoConcepto()
        End If
        f.Dispose()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar esta factura?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasAlta, PermisosN.Secciones.Ventas) = True Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesAVenta(idVenta)
                Dim C As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
                'If Estado = Estados.Guardada And C.Credito = 0 Then
                '    Dim Cliente As New dbClientes(MySqlcon)
                '    Cliente.ModificaSaldo(idCliente, C.TotalaPagar, 1)
                'End If
                C.RegresaInventario(idVenta)
                C.Eliminar(idVenta)
                P.limpiarDescPromociones()
                P.limpiarVentasdesc()
                AddError("Se eliminó factura con folio: " + TextBox11.Text + TextBox2.Text, "ventas botón eliminar", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm"), idVenta)
                PopUp("Factura Eliminada", 90)
                Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim F As New frmVentasAsignaSeries(IdInventario, idVenta, 0, CDbl(TextBox5.Text))
        F.ShowDialog()
        F.Dispose()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasCancelar, PermisosN.Secciones.Ventas) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If Estado = Estados.Pendiente Then
            MsgBox("No se puede cancelar una factura pendiente.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Dim Dep As New dbDeposito(MySqlcon)
        If Dep.TieneDepositos(idVenta) Then
            MsgBox("Esta factura tiene depósitos ligados, necesita eliminar primero los depósitos para poder cancelarla.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If MsgBox("¿Cancelar esta factura?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim VP As New dbVentasPagos(MySqlcon)
            If VP.HayPagosVentas(idVenta) = 0 Then
                Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
                If V.TieneSalidas(idVenta) = 0 Then
                    If Eselectronica = 2 And GlobalConector = 1 And GlobalPacCFDI = 1 Then
                        V.DaDatosTimbrado(idVenta)
                        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                        If V.CancelarTimbrado(S.RFC, V.uuid) = 1 Then
                            Modificar(Estados.Cancelada)
                        Else
                            MsgBox("Error en la cancelación de la factura. Intente mas tarde.", MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        If Eselectronica = 2 And GlobalPacCFDI = 2 Then
                            V.DaDatosTimbrado(idVenta)
                            Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                            If V.CancelarTimbrado2(S.RFC, V.uuid, Op._ApiKey) = 1 Then
                                Modificar(Estados.Cancelada)
                            Else
                                MsgBox("Error en la cancelación de la factura. Intente mas tarde. " + V.MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
                            End If
                        Else
                            Modificar(Estados.Cancelada)
                        End If
                    End If
                Else
                    MsgBox("No se puede cancelar esta factura primero debe cancelar todas las salidas relacionadas.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
            Else
                MsgBox("Para poder cancelar esta factura primero debe cancelar todos los pagos.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        End If
    End Sub

    Private Sub CadenaOriginal(ByVal pEstado As Byte, ByVal pXMLAdenda As String)
        Dim en As New Encriptador
        Dim RutaXml As String
        Dim RutaPDF As String
        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            Cadena = V.CreaCadenaOriginal22(idVenta, GlobalIdMoneda)
        Else
            Cadena = V.CreaCadenaOriginal(idVenta, GlobalIdMoneda)
        End If
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
        RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
        Archivos.CierraDB()
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
        Dim Enc As New System.Text.UTF8Encoding
        Dim xmldoc As String
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            xmldoc = V.CreaXML22(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        Else
            xmldoc = V.CreaXML(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        End If
        Dim Bytes() As Byte = Enc.GetBytes(xmldoc)
        'Dim Bytes() As Byte = Encoder.GetBytes(pCadenaOriginal)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        If Op._NoRutas = "0" Then
            RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        End If
        RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        'en.GuardaArchivo(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFac-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
        If pXMLAdenda <> "" Then
            xmldoc = xmldoc.Insert(xmldoc.LastIndexOf("</Comprobante>"), pXMLAdenda)
        End If
        en.GuardaArchivoTexto(RutaXml + "\FAC-" + V.Serie + V.Folio.ToString + ".xml", xmldoc, System.Text.Encoding.UTF8)
        Imprimir(idVenta)
    End Sub


    Private Function CadenaOriginali(ByVal pEstado As Byte, ByVal pXMLAdenda As String, pCadenaOriginalComp As String, pPorImp As Boolean, pConMensajes As Boolean) As Boolean
        Dim en As New Encriptador
        Dim HuboError As Boolean = False
        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        V.Alterno = Op._CalculoAlterno
        Op.DaOpciones3(V.IdSucursal)
        FacturaGlobal = New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        FacturaGlobal.Alterno = Op._CalculoAlterno
        If Op._noCertificado = "Predial:" Then
            V.ConPredialenXML = True
        Else
            V.ConPredialenXML = False
        End If
        V.DaDatosTimbrado(idVenta)
        If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada And pPorImp Then
            If DateTimePicker1.Value.ToString("yyyy/MM/dd") < DateAdd(DateInterval.Day, -2, Date.Now).ToString("yyyy/MM/dd") And pEstado = Estados.Guardada Then
                If MsgBox("La fecha ya no es válida para timbrar esta factura. ¿Cambiarla a la fecha actual?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    V.ModificarFechaHora(idVenta, True)
                    DateTimePicker1.Value = Date.Now
                Else
                    Return False
                End If
            Else
                V.ModificarFechaHora(idVenta, False)
            End If

        End If
        Dim RutaXmlTemp As String = ""
        Dim RutaXml As String = ""
        Dim RutaXmlTimbrado As String = ""
        Dim RutaXMLTimbradob As String = ""
        Dim RutaPDF As String = ""
        Dim MsgError As String = ""

        Try
            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                Cadena = V.CreaCadenaOriginali32(idVenta, GlobalIdMoneda, pCadenaOriginalComp, Op.FacturaComoegreso)
            Else
                Cadena = V.CreaCadenaOriginali(idVenta, GlobalIdMoneda)
            End If
            'en.GuardaArchivoTexto(Application.StartupPath + "\cadena.txt", Cadena, System.Text.Encoding.UTF8)
            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
            RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
            Archivos.CierraDB()
            Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXMLTimbradob = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            If Op._NoRutas = "0" Then
                RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
            End If
            Dim strXML As String
            V.NoCertificadoSAT = ""
            V.DaDatosTimbrado(idVenta)
            'If V.SelloCFD <> "" And V.SelloCFD.Contains("Ambiente de Pruebas") = False Then Sello = V.SelloCFD

            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                If pCadenaOriginalComp <> "" Then
                    strXML = V.CreaXMLi32(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa, pXMLAdenda, Op.FacturaComoegreso)
                Else
                    strXML = V.CreaXMLi32(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa, "", Op.FacturaComoegreso)
                End If
            Else
                strXML = V.CreaXMLi(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa)
            End If

            Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
            If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada Then

                If GlobalPacCFDI = 0 Then
                    en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                    Dim Timbre As TimbreFiscal.TimbreFiscalDigital
                    Dim sa As New dbSucursalesArchivos
                    sa.DaOpciones(GlobalIdEmpresa, True)
                    Timbre = V.Timbrar(S.RFC, V.Cliente.RFC, sa.RutaPFX, sa.PassPFX, strXML, GlobalDireccionTimbrado, True)

                    V.NoCertificadoSAT = Timbre.noCertificadoSAT
                    If V.NoCertificadoSAT <> "Error" Then
                        V.uuid = Timbre.UUID
                        V.FechaTimbrado = Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss")
                        V.SelloCFD = Timbre.selloCFD
                        V.NoCertificadoSAT = Timbre.noCertificadoSAT
                        V.SelloSAT = Timbre.selloSAT
                        V.GuardaDatosTimbrado(idVenta, Timbre.UUID, Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss"), Timbre.selloCFD, Timbre.noCertificadoSAT, Timbre.selloSAT)
                        Dim strTimbrado As String
                        strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                        strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ xsi:schemaLocation=""http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/TimbreFiscalDigital/TimbreFiscalDigital.xsd"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital"" />" + vbCrLf
                        strTimbrado += "</cfdi:Complemento>" + vbCrLf
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), strTimbrado)
                        If V.Cliente.UsaAdenda = 4 Then
                            Dim frmA As New frmAdendaPEPSICO(idVenta, False)
                            frmA.ShowDialog()
                            pXMLAdenda = frmA.strXML
                            frmA.Dispose()
                        End If
                        If V.Cliente.UsaAdenda = 9 Then
                            'Walmart
                            Dim frmA As New frmAddendaWalmart(idVenta)
                            frmA.ShowDialog()
                            pXMLAdenda = frmA.addenda.CreaXML
                            'strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                            frmA.Dispose()
                        End If
                        If pXMLAdenda <> "" And pCadenaOriginalComp = "" Then
                            strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                        End If
                        en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                    Else
                        HuboError = True
                    End If
                End If
                If GlobalPacCFDI = 1 Then
                    Dim Os As New dbOpciones(MySqlcon)
                    If GlobalConector = 0 Then
                        en.GuardaArchivoTexto(RutaXmlTemp, strXML, System.Text.Encoding.UTF8)
                        If IO.File.Exists(RutaXml) Then
                            IO.File.Delete(RutaXml)
                        End If
                    Else
                        If IO.File.Exists(RutaXmlTimbrado) Then
                            IO.File.Delete(RutaXmlTimbrado)
                        End If
                        en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                    End If

                    If V.Timbrar2(Os._UsuarioFacCom, Os._passFacCom, S.RFC, RutaXmlTemp, RutaXml, GlobalConector, True) = 0 Then
                        V.NoCertificadoSAT = "Error"
                    End If
                    Dim xmldoc As New Xml.XmlDocument
                    'Dim xmldoc2 As New Xml.XmlDocument
                    If GlobalConector = 0 Then
                        xmldoc.Load(RutaXml)
                        If xmldoc.DocumentElement.Name = "ERROR" Then
                            V.NoCertificadoSAT = "Error"
                            MsgError = xmldoc.InnerText
                            HuboError = True
                        End If
                    Else
                        Dim ChecarXML As String
                        ChecarXML = en.LeeArchivoTexto(RutaXmlTimbrado)
                        If ChecarXML.StartsWith("ERROR") Then
                            MsgError = ChecarXML
                            V.NoCertificadoSAT = "Error"
                            HuboError = True
                        Else
                            If ChecarXML.StartsWith("<?xml version=""1.0"" encoding=""utf-8""?>" + vbCrLf + "<?xml version=""1.0"" encoding=""utf-8""?>") Then
                                ChecarXML = ChecarXML.Substring(40, ChecarXML.Length - 40)
                                If pXMLAdenda <> "" Then
                                    ChecarXML = ChecarXML.Insert(ChecarXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                                End If
                                If V.Cliente.UsaAdenda = 4 Then
                                    Dim frmA As New frmAdendaPEPSICO(idVenta, False)
                                    frmA.ShowDialog()
                                    pXMLAdenda = frmA.strXML
                                    ChecarXML = ChecarXML.Insert(ChecarXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                                    frmA.Dispose()
                                End If
                                If V.Cliente.UsaAdenda = 9 Then
                                    'Walmart
                                    Dim frmA As New frmAddendaWalmart(idVenta)
                                    frmA.ShowDialog()
                                    pXMLAdenda = frmA.addenda.CreaXML
                                    ChecarXML = ChecarXML.Insert(ChecarXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                                    frmA.Dispose()
                                End If
                                en.GuardaArchivoTexto(RutaXmlTimbrado, ChecarXML, System.Text.Encoding.UTF8)
                            Else
                                If pXMLAdenda <> "" And pCadenaOriginalComp = "" Then
                                    ChecarXML = ChecarXML.Insert(ChecarXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                                End If
                                If V.Cliente.UsaAdenda = 4 Then
                                    Dim frmA As New frmAdendaPEPSICO(idVenta, False)
                                    frmA.ShowDialog()
                                    pXMLAdenda = frmA.strXML
                                    ChecarXML = ChecarXML.Insert(ChecarXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                                    frmA.Dispose()
                                End If
                                If V.Cliente.UsaAdenda = 9 Then
                                    'Walmart
                                    Dim frmA As New frmAddendaWalmart(idVenta)
                                    frmA.ShowDialog()
                                    pXMLAdenda = frmA.addenda.CreaXML
                                    ChecarXML = ChecarXML.Insert(ChecarXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                                    frmA.Dispose()
                                End If
                                en.GuardaArchivoTexto(RutaXmlTimbrado, ChecarXML, System.Text.Encoding.UTF8)
                            End If
                            xmldoc.Load(RutaXmlTimbrado)
                        End If

                        'xmldoc2.Load(RutaXmlTimbrado)
                    End If
                    If V.NoCertificadoSAT <> "Error" Then
                        '    V.NoCertificadoSAT = "Error"
                        '    MsgError = xmldoc.InnerText
                        'Else
                        V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                        V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                        V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                        V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                        V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value

                        V.GuardaDatosTimbrado(idVenta, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                        If IO.File.Exists(RutaXmlTemp) Then
                            IO.File.Delete(RutaXmlTemp)
                        End If
                        If IO.File.Exists(RutaXml) And GlobalConector = 1 Then
                            IO.File.Delete(RutaXml)
                        End If
                    Else
                        HuboError = True
                    End If

                End If

                If GlobalPacCFDI = 2 Then
                    en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                    Dim Timbre As String
                    Dim sa As New dbSucursalesArchivos
                    sa.DaOpciones(GlobalIdEmpresa, True)
                    Timbre = V.Timbrar3(S.RFC, strXML, "", Op._ApiKey, True, V.Folio, V.Serie)
                    If UCase(Timbre.Substring(0, 5)) <> "ERROR" Then
                        Dim xmldoc As New Xml.XmlDocument
                        en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)
                        xmldoc.Load(RutaXmlTimbrado)
                        V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                        V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                        V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                        V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                        V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value
                        V.GuardaDatosTimbrado(idVenta, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                        If pXMLAdenda <> "" And pCadenaOriginalComp = "" Then
                            Timbre = Timbre.Insert(Timbre.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                        End If
                        If V.Cliente.UsaAdenda = 4 Then
                            Dim frmA As New frmAdendaPEPSICO(idVenta, False)
                            frmA.ShowDialog()
                            pXMLAdenda = frmA.strXML
                            Timbre = Timbre.Insert(Timbre.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                            frmA.Dispose()
                        End If
                        If V.Cliente.UsaAdenda = 9 Then
                            'Walmart
                            Dim frmA As New frmAddendaWalmart(idVenta)
                            frmA.ShowDialog()
                            pXMLAdenda = frmA.addenda.CreaXML
                            Timbre = Timbre.Insert(Timbre.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                            frmA.Dispose()
                        End If
                        en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)

                    Else
                        MsgError = Timbre
                        V.NoCertificadoSAT = "Error"
                        HuboError = True
                    End If
                End If


                '    End If

            Else
                'Crear xml timbrado
                Dim ExisteArchivo As Boolean = False
                If GlobalConector = 0 Then
                    If IO.File.Exists(RutaXml) Then ExisteArchivo = True
                Else
                    If IO.File.Exists(RutaXmlTimbrado) Then ExisteArchivo = True
                End If
                If pEstado = Estados.Guardada And ExisteArchivo = False Then
                    Dim strTimbrado As String
                    strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                    strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ xsi:schemaLocation=""http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/TimbreFiscalDigital/TimbreFiscalDigital.xsd"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital""/>" + vbCrLf
                    strTimbrado += "</cfdi:Complemento>" + vbCrLf
                    strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), strTimbrado)
                    'strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                    If pXMLAdenda <> "" And pCadenaOriginalComp = "" Then
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                    End If
                    If V.Cliente.UsaAdenda = 4 Then
                        Dim frmA As New frmAdendaPEPSICO(idVenta, True)
                        frmA.ShowDialog()
                        pXMLAdenda = frmA.strXML
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                        frmA.Dispose()
                    End If
                    If V.Cliente.UsaAdenda = 9 Then
                        'Walmart
                        Dim frmA As New frmAddendaWalmart(idVenta)
                        frmA.ShowDialog()
                        pXMLAdenda = frmA.addenda.CreaXML
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                        frmA.Dispose()
                    End If
                    en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                End If
            End If

            If V.NoCertificadoSAT = "Error" Then
                If pConMensajes Then MsgBox("Ha ocurrido un error en el timbrado del la factura, intente mas tarde." + vbCrLf + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
                AddErrorTimbrado(Replace(MsgError, "'", "''"), "Ventas - Timbrado", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idVenta)
                HuboError = True
                ''If idRemisiones.Length = 1 And idRemisiones(0) = 0 And GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirPendienteVentas, PermisosN.Secciones.Ventas) = True Then
                ''    If MsgBox("¿Guardar factura como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                ''        V.ModificaEstado(idVenta, Estados.Pendiente)
                ''        Nuevo()
                ''    Else
                ''        Dim Se As New dbInventarioSeries(MySqlcon)
                ''        Se.QuitaSeriesAVenta(idVenta)
                ''        V.RegresaInventario(idVenta)
                ''        V.DesligaRemisiones(idVenta)
                ''        V.Eliminar(idVenta)
                ''        PopUp("Factura Eliminada", 90)
                ''        Nuevo()
                ''    End If
                ''Else
                ''    'Dim Ligada As Integer
                ''    'Ligada = V.VienedeRemision(idVenta)
                ''    V.DesligaRemisiones(idVenta)
                ''    Dim Se As New dbInventarioSeries(MySqlcon)
                ''    Se.QuitaSeriesAVenta(idVenta)
                ''    V.RegresaInventario(idVenta)
                ''    V.DesligaRemisiones(idVenta)
                ''    V.Eliminar(idVenta)
                ''    PopUp("Factura Eliminada", 90)
                ''    Nuevo()
                ''End If
                'Error en timbrado
            End If
        Catch ex As Exception
            AddError(Replace(ex.Message, "'", "''"), "Ventas - Timbrado", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idVenta)
            If pConMensajes Then MsgBox("Error al timbrar " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            HuboError = True
        End Try
        'Imprimir 
        If HuboError = False Then
            Imprimir(FacturaGlobal.ID)
            'Enviar por correo
            EnviarCorreo(pEstado, RutaPDF, RutaXml, RutaXmlTimbrado, RutaXMLTimbradob)
            Return True
        Else
            Return False
        End If
    End Function


    Private Function CadenaOriginali33(ByVal pEstado As Byte, ByVal pXMLAdenda As String, pCadenaOriginalComp As String, pPorImp As Boolean, pConMensajes As Boolean) As Boolean
        Dim en As New Encriptador
        Dim HuboError As Boolean = False
        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        V.Alterno = Op._CalculoAlterno
        Op.DaOpciones3(V.IdSucursal)
        FacturaGlobal = New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        FacturaGlobal.Alterno = Op._CalculoAlterno
        If Op._noCertificado = "Predial:" Then
            V.ConPredialenXML = True
        Else
            V.ConPredialenXML = False
        End If
        V.DaDatosTimbrado(idVenta)
        If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada And pPorImp Then
            If DateTimePicker1.Value.ToString("yyyy/MM/dd") < DateAdd(DateInterval.Day, -2, Date.Now).ToString("yyyy/MM/dd") And pEstado = Estados.Guardada Then
                If MsgBox("La fecha ya no es válida para timbrar esta factura. ¿Cambiarla a la fecha actual?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    V.ModificarFechaHora(idVenta, True)
                    DateTimePicker1.Value = Date.Now
                Else
                    Return False
                End If
            Else
                V.ModificarFechaHora(idVenta, False)
            End If

        End If
        Dim RutaXmlTemp As String = ""
        Dim RutaXml As String = ""
        Dim RutaXmlTimbrado As String = ""
        Dim RutaXMLTimbradob As String = ""
        Dim RutaPDF As String = ""
        Dim MsgError As String = ""

        Try
            Cadena = V.CreaCadenaOriginali33(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa, pXMLAdenda, Op.FacturaComoegreso, pCadenaOriginalComp)
            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
            RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
            Archivos.CierraDB()
            Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), True)
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXMLTimbradob = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            If Op._NoRutas = "0" Then
                RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
            End If
            Dim strXML As String
            V.NoCertificadoSAT = ""
            V.DaDatosTimbrado(idVenta)

            If pCadenaOriginalComp <> "" Then
                strXML = V.CreaXMLi33(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa, pXMLAdenda, Op.FacturaComoegreso)
            Else
                strXML = V.CreaXMLi33(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa, "", Op.FacturaComoegreso)
            End If


            Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
            If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada Then

                If GlobalPacCFDI = 0 Then
                    en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                    Dim Timbre As TimbreFiscal.TimbreFiscalDigital
                    Dim sa As New dbSucursalesArchivos
                    sa.DaOpciones(GlobalIdEmpresa, True)
                    Timbre = V.Timbrar(S.RFC, V.Cliente.RFC, sa.RutaPFX, sa.PassPFX, strXML, GlobalDireccionTimbrado, True)

                    V.NoCertificadoSAT = Timbre.noCertificadoSAT
                    If V.NoCertificadoSAT <> "Error" Then
                        V.uuid = Timbre.UUID
                        V.FechaTimbrado = Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss")
                        V.SelloCFD = Timbre.selloCFD
                        V.NoCertificadoSAT = Timbre.noCertificadoSAT
                        V.SelloSAT = Timbre.selloSAT
                        V.GuardaDatosTimbrado(idVenta, Timbre.UUID, Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss"), Timbre.selloCFD, Timbre.noCertificadoSAT, Timbre.selloSAT)
                        Dim strTimbrado As String
                        strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                        strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ xsi:schemaLocation=""http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/TimbreFiscalDigital/TimbreFiscalDigital.xsd"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital"" />" + vbCrLf
                        strTimbrado += "</cfdi:Complemento>" + vbCrLf
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), strTimbrado)
                        If V.Cliente.UsaAdenda = 4 Then
                            Dim frmA As New frmAdendaPEPSICO(idVenta, False)
                            frmA.ShowDialog()
                            pXMLAdenda = frmA.strXML
                            frmA.Dispose()
                        End If
                        If V.Cliente.UsaAdenda = 9 Then
                            'Walmart
                            Dim frmA As New frmAddendaWalmart(idVenta)
                            frmA.ShowDialog()
                            pXMLAdenda = frmA.addenda.CreaXML
                            'strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                            frmA.Dispose()
                        End If
                        If pXMLAdenda <> "" And pCadenaOriginalComp = "" Then
                            strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                        End If
                        en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                    Else
                        HuboError = True
                    End If
                End If


                If GlobalPacCFDI = 2 Then
                    en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                    Dim Timbre As String
                    Dim sa As New dbSucursalesArchivos
                    sa.DaOpciones(GlobalIdEmpresa, True)
                    Timbre = V.Timbrar4(S.RFC, strXML, "", Op._ApiKey, True, V.Folio, V.Serie)
                    If UCase(Timbre.Substring(0, 5).ToUpper) <> "ERROR" Then
                        Dim xmldoc As New Xml.XmlDocument
                        en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)
                        xmldoc.Load(RutaXmlTimbrado)
                        V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                        V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("SelloCFD").Value
                        V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("NoCertificadoSAT").Value
                        V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                        V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("SelloSAT").Value
                        V.GuardaDatosTimbrado(idVenta, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                        If pXMLAdenda <> "" And pCadenaOriginalComp = "" Then
                            Timbre = Timbre.Insert(Timbre.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                        End If
                        If V.Cliente.UsaAdenda = 4 Then
                            Dim frmA As New frmAdendaPEPSICO(idVenta, False)
                            frmA.ShowDialog()
                            pXMLAdenda = frmA.strXML
                            Timbre = Timbre.Insert(Timbre.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                            frmA.Dispose()
                        End If
                        If V.Cliente.UsaAdenda = 9 Then
                            'Walmart
                            Dim frmA As New frmAddendaWalmart(idVenta)
                            frmA.ShowDialog()
                            pXMLAdenda = frmA.addenda.CreaXML
                            Timbre = Timbre.Insert(Timbre.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                            frmA.Dispose()
                        End If
                        en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)

                    Else
                        MsgError = Timbre
                        V.NoCertificadoSAT = "Error"
                        HuboError = True
                    End If
                End If

            Else
                'Crear xml timbrado
                Dim ExisteArchivo As Boolean = False
                If GlobalConector = 0 Then
                    If IO.File.Exists(RutaXml) Then ExisteArchivo = True
                Else
                    If IO.File.Exists(RutaXmlTimbrado) Then ExisteArchivo = True
                End If
                If pEstado = Estados.Guardada And ExisteArchivo = False Then
                    Dim strTimbrado As String
                    strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                    strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ xsi:schemaLocation=""http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/TimbreFiscalDigital/TimbreFiscalDigital.xsd"" xmlns:tfd=""http://www.sat.gob.mx/TimbreFiscalDigital""/>" + vbCrLf
                    strTimbrado += "</cfdi:Complemento>" + vbCrLf
                    strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), strTimbrado)
                    'strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                    If pXMLAdenda <> "" And pCadenaOriginalComp = "" Then
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                    End If
                    If V.Cliente.UsaAdenda = 4 Then
                        Dim frmA As New frmAdendaPEPSICO(idVenta, True)
                        frmA.ShowDialog()
                        pXMLAdenda = frmA.strXML
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                        frmA.Dispose()
                    End If
                    If V.Cliente.UsaAdenda = 9 Then
                        'Walmart
                        Dim frmA As New frmAddendaWalmart(idVenta)
                        frmA.ShowDialog()
                        pXMLAdenda = frmA.addenda.CreaXML
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                        frmA.Dispose()
                    End If
                    en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                End If
            End If

            If V.NoCertificadoSAT = "Error" Then
                If pConMensajes Then MsgBox("Ha ocurrido un error en el timbrado del la factura, intente mas tarde." + vbCrLf + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
                AddErrorTimbrado(Replace(MsgError, "'", "''"), "Ventas - Timbrado", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idVenta)
                HuboError = True
            End If
        Catch ex As Exception
            AddError(Replace(ex.Message, "'", "''"), "Ventas - Timbrado", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idVenta)
            If pConMensajes Then MsgBox("Error al timbrar " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            HuboError = True
        End Try
        'Imprimir 
        If HuboError = False Then
            Imprimir(FacturaGlobal.ID)
            'Enviar por correo
            EnviarCorreo(pEstado, RutaPDF, RutaXml, RutaXmlTimbrado, RutaXMLTimbradob)
            Return True
        Else
            Return False
        End If
    End Function
    'Private Sub ImprimirCFDI(pRutaPDF As String)
    '    CadenaCFDI = "||1.0|" + FacturaGlobal.uuid + "|" + FacturaGlobal.FechaTimbrado + "|" + FacturaGlobal.SelloCFD + "|" + FacturaGlobal.NoCertificadoSAT + "||"

    '    Try

    '        Dim SA As New dbSucursalesArchivos
    '        Dim Impresora As String
    '        Dim ImpAnt As String
    '        Dim ImpFlujo As String
    '        Dim TipoAnt As Integer
    '        Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, False, 0, TiposDocumentos.Venta)
    '        ImpFlujo = SA.DaImpresoraPorTipo(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 1, TiposDocumentos.Venta)
    '        TipoImpresora = SA.TipoImpresora
    '        ImpAnt = Impresora
    '        TipoAnt = TipoImpresora
    '        If Impresora = "Bullzip PDF Printer" Then
    '            Dim obj As New Bullzip.PdfWriter.PdfSettings
    '            obj.Init()
    '            obj.PrinterName = Impresora
    '            obj.SetValue("Output", pRutaPDF + "\<docname>.pdf")
    '            obj.SetValue("ShowSettings", "never")
    '            obj.SetValue("ShowPDF", "yes")
    '            obj.SetValue("ShowSaveAS", "nofile")
    '            obj.SetValue("ConfirmOverwrite", "no")
    '            obj.SetValue("Target", "printer")
    '            obj.WriteSettings()
    '        End If

    '        DocAImprimir = 0
    '        If Op.Copiaflujoventas = 1 Then
    '            Impresora = ImpFlujo
    '            TipoImpresora = 1
    '            LlenaNodos(FacturaGlobal.IdSucursal, TiposDocumentos.VentaTicket)
    '            LlenaNodosImpresion(Op.TituloOriginalFactura)
    '            PrintDocument1.PrinterSettings.PrinterName = Impresora
    '            PrintDocument1.DocumentName = "PSSFACTURA Copia-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString
    '            PrintDocument1.Print()
    '            Impresora = ImpAnt
    '            TipoImpresora = TipoAnt

    '        End If
    '        If TipoImpresora = 0 Then
    '            LlenaNodos(FacturaGlobal.IdSucursal, TiposDocumentos.Venta)
    '        Else
    '            LlenaNodos(FacturaGlobal.IdSucursal, TiposDocumentos.VentaTicket)
    '        End If
    '        LlenaNodosImpresion(Op.TituloOriginalFactura)
    '        PrintDocument1.PrinterSettings.PrinterName = Impresora
    '        PrintDocument1.DocumentName = "PSSFACTURA-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString
    '        PrintDocument1.Print()
    '        Dim Fdp As New dbFormasdePago(FacturaGlobal.IdFormadePago, MySqlcon)
    '        If (Op.ActivarCopiaFactura = 1 And Fdp.Tipo = dbFormasdePago.Tipos.Contado) Or (Op.ActivarCopiaFacturaCredito And Fdp.Tipo = dbFormasdePago.Tipos.Credito) Then
    '            LlenaNodosImpresion(Op.TituloCopiaFactura)
    '            PrintDocument1.Print()
    '        End If
    '        If (Op.ActivarCopia2Factura = 1 And Fdp.Tipo = dbFormasdePago.Tipos.Contado) Or (Op.ActivarCopiaFacturaCredito2 And Fdp.Tipo = dbFormasdePago.Tipos.Credito) Then
    '            LlenaNodosImpresion(Op.TituloCopia2Factura)
    '            PrintDocument1.Print()
    '        End If
    '        If Op._ActivarPDF = "1" Then
    '            'PrintDocument1.DocumentName = "FAC-" + facturaglobal.Serie + facturaglobal.Folio.ToString
    '            Dim SA2 As New dbSucursalesArchivos
    '            Impresora = SA2.DaImpresoraActiva(FacturaGlobal.IdSucursal, GlobalIdEmpresa, True, 0, TiposDocumentos.PDF)
    '            'TipoImpresora = SA2.TipoImpresora
    '            If Impresora = "Bullzip PDF Printer" Then
    '                Dim obj As New Bullzip.PdfWriter.PdfSettings
    '                obj.Init()
    '                obj.PrinterName = Impresora
    '                obj.SetValue("Output", pRutaPDF + "\<docname>.pdf")
    '                obj.SetValue("ShowSettings", "never")
    '                If Op._MostrarPDF = "0" Then
    '                    obj.SetValue("ShowPDF", "no")
    '                Else
    '                    obj.SetValue("ShowPDF", "yes")
    '                End If
    '                obj.SetValue("ShowSaveAS", "nofile")
    '                obj.SetValue("ConfirmOverwrite", "no")
    '                obj.SetValue("Target", "printer")
    '                obj.WriteSettings()
    '            End If
    '            PrintDocument1.PrinterSettings.PrinterName = Impresora
    '            LlenaNodosImpresion(Op.TituloOriginalFactura)
    '            PrintDocument1.Print()
    '        End If
    '        If FacturaGlobal.ISR <> 0 Or FacturaGlobal.IvaRetenido <> 0 Then
    '            If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
    '                If TipoImpresora = 0 Then
    '                    LlenaNodos(FacturaGlobal.IdSucursal, TiposDocumentos.FormatoRetencion)
    '                Else
    '                    LlenaNodos(FacturaGlobal.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
    '                End If
    '                LlenaNodosImpresionRet()
    '                DocAImprimir = 1
    '                PrintDocument1.DocumentName = "RetFac-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString
    '                If Impresora = "Bullzip PDF Printer" Then
    '                    Dim obj As New Bullzip.PdfWriter.PdfSettings
    '                    obj.Init()
    '                    obj.PrinterName = Impresora
    '                    obj.SetValue("Output", pRutaPDF + "\<docname>.pdf")
    '                    obj.SetValue("ShowSettings", "never")
    '                    obj.SetValue("ShowPDF", "yes")
    '                    obj.SetValue("ShowSaveAS", "nofile")
    '                    obj.SetValue("ConfirmOverwrite", "no")
    '                    obj.SetValue("Target", "printer")
    '                    obj.WriteSettings()
    '                End If
    '                PrintDocument1.Print()
    '            End If
    '        End If
    '        'impresion notarios
    '        imprimirNotarial()
    '        Dim Ss As New dbInventarioSeries(MySqlcon)
    '        If Ss.CantidadDeSeriesAgregadasaVenta(idVenta, 0) > 0 Then
    '            If MsgBox("¿Imprimir listado de series?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
    '                ImprimirSeries()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        AddError(Replace(ex.Message, "'", "''"), "Ventas - Imprimir", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idVenta)
    '        MsgBox("Error al imprimir " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try

    'End Sub
    Private Sub EnviarCorreo(pEstado As Byte, pRutaPDF As String, pRutaXML As String, pRutaXMLTimbrado As String, pRutaXMLTimbradob As String)
        If FacturaGlobal.Cliente.Email <> "" Then
            FacturaGlobal.DaDatosTimbrado(FacturaGlobal.ID)
            Try
                If MsgBox("¿Enviar factura por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    If FacturaGlobal.Cliente.Email <> "" Then

                        Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                        'Dim O As New dbOpciones(MySqlcon)
                        Dim C As String
                        Dim S As New dbSucursales(FacturaGlobal.IdSucursal, MySqlcon)
                        Dim RutaPDFcompleto As String = pRutaPDF + "\PSSFACTURA-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString + ".pdf"
                        If IO.File.Exists(pRutaPDF + "\PSSFACTURA-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString + ".pdf") = False Then
                            RutaPDFcompleto = pRutaPDF + "\PSSFACTURA-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString + ".pdf"
                        End If
                        C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "FACTURA" + vbNewLine + "Folio: " + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString("0000") + vbNewLine + "UUID:" + FacturaGlobal.uuid + vbNewLine + vbNewLine
                        C += Op.CorreoContenido
                        If GlobalConector = 0 Then
                            If pEstado = Estados.Pendiente Then
                                M.send("Comprobante Fiscal Digital por Internet Factura: " + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString("0000") + " " + FacturaGlobal.uuid, C, FacturaGlobal.Cliente.Email, FacturaGlobal.Cliente.Nombre, RutaPDFcompleto, "")
                            Else
                                M.send("Comprobante Fiscal Digital por Internet Factura: " + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString("0000") + " " + FacturaGlobal.uuid, C, FacturaGlobal.Cliente.Email, FacturaGlobal.Cliente.Nombre, RutaPDFcompleto, pRutaXML)
                            End If
                        Else
                            If IO.File.Exists(pRutaXMLTimbrado) Then
                                If pEstado = Estados.Pendiente Then
                                    M.send("Comprobante Fiscal Digital por Internet Factura: " + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString("0000") + " " + FacturaGlobal.uuid, C, FacturaGlobal.Cliente.Email, FacturaGlobal.Cliente.Nombre, RutaPDFcompleto, "")
                                Else
                                    M.send("Comprobante Fiscal Digital por Internet Factura: " + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString("0000") + " " + FacturaGlobal.uuid, C, FacturaGlobal.Cliente.Email, FacturaGlobal.Cliente.Nombre, RutaPDFcompleto, pRutaXMLTimbrado)
                                End If
                            Else
                                If pEstado = Estados.Pendiente Then
                                    M.send("Comprobante Fiscal Digital por Internet Factura: " + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString("0000") + " " + FacturaGlobal.uuid, C, FacturaGlobal.Cliente.Email, FacturaGlobal.Cliente.Nombre, RutaPDFcompleto, "")
                                Else
                                    If IO.File.Exists(pRutaXMLTimbradob) Then
                                        M.send("Comprobante Fiscal Digital por Internet Factura: " + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString("0000") + " " + FacturaGlobal.uuid, C, FacturaGlobal.Cliente.Email, FacturaGlobal.Cliente.Nombre, RutaPDFcompleto, pRutaXMLTimbradob)
                                    Else
                                        MsgBox("No existe el archivo XML.", MsgBoxStyle.Critical, GlobalNombreApp)
                                    End If
                                End If
                            End If
                        End If
                        'If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = False Then PopUp("Correo enviado", 90)
                        PopUp("Correo enviado", 90)
                    End If
                End If
            Catch ex As Exception
                AddError(Replace(ex.Message, "'", "''"), "Ventas - Envio Correo", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idVenta)
                MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        End If
    End Sub
    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox11.Focus()
        End If
    End Sub


    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ConsultaOn Then
            If Op._TipoSelAlmacen = "0" Then
                LlenaCombos("tblalmacenes", cmbAlmacen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            Else
                LlenaCombos("tblalmacenes", cmbAlmacen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. Almacen")
            End If
            Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
            If cmbAlmacen.Items.Count > 0 Then
                If Op._TipoSelAlmacen = "0" Then
                    cmbAlmacen.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
                Else
                    cmbAlmacen.SelectedIndex = 0
                End If
            Else
                MsgBox("Esta sucursal no cuenta con almacenes.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
            If LlenandoDatos = False Then
                Dim Sf As New dbSucursalesFolios(MySqlcon)
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Factura, iTipoFacturacion)
                TextBox11.Text = Sf.Serie
                Eselectronica = iTipoFacturacion
                If Sf.EsElectronica >= 1 Then
                    CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
                End If
                Dim V As New dbVentas(MySqlcon)
                TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion, Op._ModoFoliosB).ToString
                If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
                If CInt(TextBox2.Text) > Sf.FolioFinal Then
                    LimitedeFolios = True
                    MsgBox("Se ha alcanzado el límite de folios.", MsgBoxStyle.Information, GlobalNombreApp)
                Else
                    LimitedeFolios = False
                End If
            End If
        End If
    End Sub
    Private Function ChecaTimbres() As Boolean
        Dim TTimbres As Integer
        TTimbres = CuentaTimbres()
        Dim Ops As New dbOpciones(MySqlcon)
        If Ops.FechaVen <= Format(Date.Now, "yyyy/MM/dd") Then
            MsgBox("Los timbres han caducado.", MsgBoxStyle.Critical, GlobalNombreApp)
            Return True
        Else
            If TTimbres >= Ops.Timbres Then
                MsgBox("Los timbres se han terminado.", MsgBoxStyle.Critical, GlobalNombreApp)
                Return True
            Else
                If Format(Date.Now, "yyyy/MM/dd") > DateAdd(DateInterval.Day, Ops.AvisoDias * -1, CDate(Ops.FechaVen)) Then
                    MsgBox("Los timbres estan por caducar.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
                If Ops.Timbres - TTimbres <= Ops.AvisoTimbres Then
                    MsgBox("Los timbres estan por terminarse.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
                Return False
            End If
        End If
    End Function
    Private Function ChecaCertificado(ByVal pIdCertificado As Integer) As Boolean
        Dim SC As New dbSucursalesCertificados(pIdCertificado, MySqlcon)
        If SC.FechaVencimiento <= Format(Date.Now, "yyyy/MM/dd") Then
            MsgBox("El certificado del sello digital a caducado.", MsgBoxStyle.Critical, GlobalNombreApp)
            Return True
        Else
            If Format(Date.Now, "yyyy/MM/dd") > DateAdd(DateInterval.Day, SC.Aviso * -1, CDate(SC.FechaVencimiento)) Then
                MsgBox("El certificado del sello digital esta por caducar.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
            Return False
        End If
    End Function
    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Enabled = True Then
                TextBox3.Focus()
            Else
                TextBox12.Focus()
            End If
        End If
        If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
            If UsaFormula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtArticulo)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.00")
                    TextBox4.Text = Fo.FormulaString
                End If
                Fo.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F2 And IdInventario <> 0 Then
            PresionaF2(True)
        End If
    End Sub
    Private Sub PresionaF2(Habilitado As Boolean)
        Dim Precio As Double
        If CantidadMostrar <> 0 Then
            Precio = CDbl(TextBox6.Text) / CantidadMostrar
        Else
            Precio = 0
        End If
        Dim FE As New frmVentasDaEquivalencia(IdInventario, CDbl(TextBox5.Text), CantidadMostrar, TipoCantidadMostrar, Precio, Habilitado)
        If FE.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox5.Text = FE.Cantidad.ToString
            CantidadMostrar = FE.CantidadM
            TipoCantidadMostrar = FE.TipoCantidadM
            If FE.Cantidad = FE.CantidadM Then
                SinConcersion = True
            Else
                SinConcersion = False
            End If
            If FE.Cantidad <> 0 Then
                TextBox12.Text = Format((FE.CantidadM * FE.PrecioM) / FE.Cantidad, "0.00")
            Else
                TextBox12.Text = "0"
            End If
            Label20.Text = CantidadMostrar.ToString
        End If
        FE.Dispose()
    End Sub
    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If IdInventario <> 0 Then
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim Como As Byte = 0
        If Op.FacturarPagosRemisiones = 1 Then
            Dim Resultado As MsgBoxResult
            Resultado = MsgBox("¿Desde pagos de remisiones?", MsgBoxStyle.YesNoCancel)
            If Resultado = MsgBoxResult.Yes Then Como = 2
            If Resultado = MsgBoxResult.No Then Como = 1
        Else
            Como = 1
        End If
        Try
            If Como = 1 Then
                Dim Forma As New frmBuscaDocumentoVenta(0, False, 2, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, True, True, True, 0, False, "", False)
                If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Dim V As New dbVentas(Forma.id(0), MySqlcon, Op._Sinnegativos)
                    Dim detrem As Boolean = False
                    'If Estado = 0 Then
                    '0 cotizacion
                    '1 pedido
                    '2 remision
                    '3 ventas
                    Select Case Forma.Tipo
                        Case 0
                            Dim Co As New dbVentasCotizaciones(Forma.id(0), MySqlcon)
                            TextBox1.Text = Co.Cliente.Clave
                            TipoCreardesde = 0
                        Case 1
                            Dim Cp As New dbVentasPedidos(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cp.Cliente.Clave
                            TipoCreardesde = 0
                        Case 2
                            Dim Cr As New dbVentasRemisiones(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cr.Cliente.Clave
                            idRemisiones = Forma.id
                            If Cr.PorSurtir = 1 Then
                                CheckBox2.Checked = "true"
                            End If
                            TipoCreardesde = 1
                            If Op.RemisionesSinDetalleCD = 1 Then
                                If MsgBox("¿Agregar detalle por remisión?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                                    detrem = True
                                End If
                            End If
                        Case 3
                            Dim Cv As New dbVentas(Forma.id(0), MySqlcon, Op._Sinnegativos)
                            TextBox1.Text = Cv.Cliente.Clave
                            TextBox16.Text = Cv.Descuento.ToString
                            TextBox18.Text = Cv.SobreEscribeImpLoc.ToString
                            TextBox19.Text = Cv.DescuentoG2.ToString
                            ComboBox9.Text = CSat.DaUsoCFDI(Cv.cUsoCFDI)
                            TipoCreardesde = 0
                        Case 4
                            Dim Fp As New dbFertilizantesPedido(Forma.id(0), MySqlcon)
                            idRemisiones = Forma.id
                            TextBox1.Text = Fp.Cliente.Clave
                            TipoCreardesde = 2
                    End Select
                    Guardar()
                    If Estado <> 0 Then
                        V.AgregarDetallesReferencia(idVenta, Forma.id, Forma.Tipo, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), detrem, Op.IdInventarioCD)
                        ConsultaDetalles()
                        CheckBox2.Enabled = False
                    End If
                    NuevoConcepto()
                    Button11.Enabled = False
                End If
            End If
            If Como = 2 Then
                CrearDesdePago(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub CrearDesdePago(pIdcliente As Integer)
        Dim resultado As DialogResult

        If pIdcliente <> 0 Then
            resultado = Windows.Forms.DialogResult.OK
        Else
            Dim Forma As New frmBuscarPagos()
            resultado = Forma.ShowDialog()
            If resultado = Windows.Forms.DialogResult.OK Then idPagos = Forma.id
            Forma.Dispose()
        End If

        If resultado = Windows.Forms.DialogResult.OK Then
            Dim V As New dbVentas(MySqlcon)
            If pIdcliente = 0 Then
                Dim Cr As New dbVentasPagosRemisiones(idPagos(0), MySqlcon)
                idCliente = Cr.IdCliente
                Dim Cli As New dbClientes(Cr.IdCliente, MySqlcon)
                TextBox1.Text = Cli.Clave
            Else
                Dim cli As New dbClientes(pIdcliente, MySqlcon)
                TextBox1.Text = cli.Clave
            End If
            Guardar()
            If Estado <> 0 Then
                V.AgregarDetallesPagos(idVenta, idPagos, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), Op.IdInventarioCD)
                ConsultaDetalles()
                TipoCreardesde = 3
                idRemisiones = idPagos
                CheckBox2.Enabled = False
            End If
            NuevoConcepto()
            Button11.Enabled = False
        End If
    End Sub
    Private Sub LlenaNodosImpresion(ByVal pTitulo As String)
        Try
            Dim AgregaSeries As Boolean
            Dim QuitaIvaCero As Boolean
            Dim TotalDescuento As Double = 0
            If Op._IVaCero = 1 Then
                QuitaIvaCero = True
            Else
                QuitaIvaCero = False
            End If
            If Op._AgregaSeriesAVenta = 0 Then
                AgregaSeries = False
            Else
                AgregaSeries = True
            End If
            Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
            V.Alterno = Op._CalculoAlterno
            Op.DaOpciones3(V.IdSucursal)
            Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
            V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
            V.DaDatosTimbrado(idVenta)
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
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "telcliente", V.Cliente.Telefono, 0), "telcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "regimenfiscal", Sucursal.RegimenFiscal, 0), "regimenfiscal")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nocuenta", V.NoCuenta, 0), "nocuenta")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "vendedor", Vendedor.Nombre, 0), "vendedor")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "clavevendedor", Vendedor.Clave, 0), "clavevendedor")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refdocumento", V.RefDocumento, 0), "refdocumento")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "adicional", V.Adicional, 0), "adicional")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "titulocopia", pTitulo, 0), "titulocopia")
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
                ImpDoc.ImpND.Add(New NodoImpresionN("", "paiscliente", V.Cliente.Pais, 0), "paiscliente")
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
                ImpDoc.ImpND.Add(New NodoImpresionN("", "paiscliente", V.Cliente.Pais2, 0), "paiscliente")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
            End If
            ImpDoc.ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "folio", Format(V.Folio, "00000"), 0), "folio")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")


            ImpDoc.ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

            Sucursal.LlenaExp(V.ID, 0)
            If Sucursal.HayExp Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "lugarexp", Sucursal.LugarExp, 0), "lugarexp")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "callerexp", Sucursal.CalleExp, 0), "callerexp")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "numeroexp", Sucursal.NumExp, 0), "numeroexp")
                If Op._IgualarFechaTimbrado = 0 Then
                    'MsgBox("ok1")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.LugarExp + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
                Else
                    'MsgBox("nook1")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.LugarExp + " " + V.FechaTimbrado, 0), "lugar")
                End If

                If V.uuid = "**No Timbrado**" Or V.uuid = "" Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.LugarExp + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugartimbrado")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.LugarExp + " " + V.FechaTimbrado, 0), "lugartimbrado")
                End If
            Else
                If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "lugarexp", Sucursal.Ciudad2, 0), "lugarexp")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "callerexp", Sucursal.Direccion2, 0), "callerexp")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "numeroexp", Sucursal.NoExterior2, 0), "numeroexp")
                    If Op._IgualarFechaTimbrado = 0 Then
                        'MsgBox("ok2")
                        ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
                    Else
                        'MsgBox("nook2")
                        ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + V.FechaTimbrado, 0), "lugar")
                    End If

                    If V.uuid = "**No Timbrado**" Or V.uuid = "" Then
                        ImpDoc.ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugartimbrado")
                    Else
                        ImpDoc.ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + V.FechaTimbrado, 0), "lugartimbrado")
                    End If
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "lugarexp", "", 0), "lugarexp")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "callerexp", "", 0), "callerexp")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "numeroexp", "", 0), "numeroexp")
                    If Op._IgualarFechaTimbrado = 0 Then
                        Dim Fe As String
                        Fe = Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora
                        'MsgBox("ok3 " + Fe)
                        ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Fe, 0), "lugar")
                    Else
                        'MsgBox("nook3")
                        ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + V.FechaTimbrado, 0), "lugar")
                    End If
                    If V.uuid = "**No Timbrado**" Or V.uuid = "" Then

                        ImpDoc.ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugartimbrado")
                    Else
                        ImpDoc.ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad + " " + Sucursal.Estado + " " + V.FechaTimbrado, 0), "lugartimbrado")
                    End If

                End If
            End If

            ImpDoc.ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")

            ImpDoc.ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
            CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")
            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            Dim VI As New dbVentasInventario(MySqlcon)
            DR = VI.ConsultaReader(idVenta, AgregaSeries, Op._DetalleKits, 1, Op._OrdenUbicacion, True)
            ImpDoc.ImpNDD.Clear()

            ImpDoc.CuantosRenglones = 0
            Dim brinca As Boolean
            Dim Cont As Integer = 0
            Dim PrecioG As Double
            Dim CodigoBarras As iTextSharp.text.pdf.Barcode128 = New iTextSharp.text.pdf.Barcode128
            While DR.Read
                brinca = False
                If Op._Sinnegativos = "1" And DR("precio") < 0 Then brinca = True
                If DR("esrevdev") = 1 Then brinca = True
                If DR("noimpimporte") = 0 Then
                    PrecioG = DR("precio")
                Else
                    PrecioG = DR("noimpimporte")
                End If
                If brinca = False Then
                    If DR("cantidad") <> 0 Then
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
                    Else
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", "", 0), "clave" + Format(Cont, "000"))
                    End If
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



                    'Dim image As Image =
                    'CodigoBidimensional = New Bitmap(image, New Size(300, 200))


                    'TotalIEPS += Double.Parse(DR("IEPS").ToString())
                    'TotalIVARetenido += Double.Parse(DR("IVARetenido").ToString())

                    ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
                    ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ubicacion", DR("ubicacion"), 0), "ubicacion" + Format(Cont, "000"))
                    ImpDoc.ImpNDD.Add(New NodoImpresionN("", "predial", DR("predial"), 0), "predial" + Format(Cont, "000"))
                    ImpDoc.ImpNDD.Add(New NodoImpresionN("", "extra", DR("extra"), 0), "extra" + Format(Cont, "000"))
                    If DR("iva") = 0 Then
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tieneiva", "", 0), "tieneiva" + Format(Cont, "000"))
                    Else
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tieneiva", "*", 0), "tieneiva" + Format(Cont, "000"))
                    End If
                    If DR("cantidad") <> 0 Then
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidadm"), Op._formatocantidad).PadLeft(Op.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipom"), 0), "tipocantidad" + Format(Cont, "000"))
                    Else
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", "", 0), "cantidad" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipocantidad", "", 0), "tipocantidad" + Format(Cont, "000"))
                    End If
                    If DR("cantidad") <> 0 Then
                        If Op.FacturaComoegreso = 0 Then
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", Format(PrecioG / DR("cantidadm"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciouiva", Format((PrecioG / DR("cantidadm")) * (1 + DR("iva") / 100), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciouiva" + Format(Cont, "000"))
                        Else
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", Format(If(PrecioG / DR("cantidadm") >= 0, PrecioG / DR("cantidadm"), PrecioG / DR("cantidadm") * -1), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciouiva", Format(If((PrecioG / DR("cantidadm")) * (1 + DR("iva") / 100) >= 0, (PrecioG / DR("cantidadm")) * (1 + DR("iva") / 100), (PrecioG / DR("cantidadm")) * (1 + DR("iva") / 100) * -1), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciouiva" + Format(Cont, "000"))
                        End If
                    Else
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", "", 0), "preciou" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciouiva", "", 0), "preciouiva" + Format(Cont, "000"))
                    End If
                    If DR("cantidad") <> 0 Then
                        If Op.FacturaComoegreso = 0 Then
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", Format(PrecioG, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "importe" + Format(Cont, "000"))
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(PrecioG * (1 + DR("iva") / 100), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ieps", Format(PrecioG * DR("IEPS") / 100, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "ieps" + Format(Cont, "000"))
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", Format(PrecioG * DR("IVARetenido") / 100, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "ivaRetenido" + Format(Cont, "000"))
                        Else
                            Dim Precio As Double = PrecioG
                            If Precio < 0 Then Precio = Precio * -1
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", Format(Precio, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "importe" + Format(Cont, "000"))
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(Precio * (1 + DR("iva") / 100), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ieps", Format(Precio * DR("IEPS") / 100, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "ieps" + Format(Cont, "000"))
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", Format(Precio * DR("IVARetenido") / 100, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "ivaRetenido" + Format(Cont, "000"))
                        End If
                    Else
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", "", 0), "importe" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importeiva", "", 0), "importeiva" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ieps", "", 0), "ieps" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", "", 0), "ivaRetenido" + Format(Cont, "000"))
                    End If

                    If DR("cantidadm") <> 0 And DR("descuento") <> 0 Then
                        Dim Desc As Double
                        Desc = (PrecioG / (1 - DR("descuento") / 100))
                        TotalDescuento += Desc - PrecioG
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(Op.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - PrecioG, Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidadm"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importesindesc", Format(Desc, Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "importesindesc" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocantuni", Format((Desc / DR("cantidadm")) * (DR("descuento") / 100), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "descuentocantuni" + Format(Cont, "000"))
                        'Vo = Vd / ( 1 - (Por/100))
                    Else
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))
                        ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocantuni", "", 0), "descuentocantuni" + Format(Cont, "000"))
                        If DR("cantidad") <> 0 Then
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(PrecioG / DR("cantidadm"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importesindesc", Format(PrecioG, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "importesindesc" + Format(Cont, "000"))
                        Else
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
                            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importesindesc", "", 0), "importesindesc" + Format(Cont, "000"))
                        End If
                    End If
                    ImpDoc.CuantosRenglones += 1
                    Cont += 1
                End If
            End While
            DR.Close()
            If Op.FacturaComoegreso = 0 Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "Totalieps", Format(V.TotalIEPS, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "Totalieps")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "TotalivaRetenido", Format(V.TotalIvaRetenidoConceptos, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "TotalivaRetenido")
            Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "Totalieps", Format(If(V.TotalIEPS >= 0, V.TotalIEPS, V.TotalIEPS * -1), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "Totalieps")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "TotalivaRetenido", Format(If(V.TotalIvaRetenidoConceptos >= 0, V.TotalIvaRetenidoConceptos, V.TotalIvaRetenidoConceptos * -1), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "TotalivaRetenido")
            End If
            If V.PorSurtir = 0 Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "porsurtir", "", 0), "porsurtir")
            Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "porsurtir", "POR SURTIR", 0), "porsurtir")
            End If
            ImpDoc.ImpND.Add(New NodoImpresionN("", "peso", Format(V.TotalPeso, "#,##0.00") + "Kg.", 0), "peso")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
            If Op.FacturaComoegreso = 0 Then
                If Op._Sinnegativos = "0" Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtototal + V.TotalDescuento - V.TotalRevdev, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalsindesc")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtototal, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotal")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtototal + V.TotalDescuento - V.TotalNegativosSinIVA - V.TotalRevdev, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalsindesc")
                    'Dim Subb As Double
                    'Subb = V.Subtototal - V.TotalNegativosSinIVA
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtototal, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotal")
                End If
                ImpDoc.ImpND.Add(New NodoImpresionN("", "totalofertas", Format(V.TotalOfertas, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totalofertas")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "totaldesc", Format(V.TotalDescuento, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totaldesc")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "totaldesc2", Format(V.DescuentoG2, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totaldesc2")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotalsinret", Format(V.Subtototal + V.TotalIva, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalsinret")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "totalcantidad", V.DaTotalCantidad(idVenta).ToString, 0), "totalcantidad")
                If V.TotalRevdev < 0 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalnegativos", Format((V.TotalNegativosSinIVA * -1), Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totalnegativos")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalnegativos", Format((V.TotalNegativosSinIVA * -1) + V.TotalRevdev, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totalnegativos")
                End If
            Else
                If Op._Sinnegativos = "0" Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(If(V.Subtototal + V.TotalDescuento - V.TotalRevdev >= 0, V.Subtototal + V.TotalDescuento - V.TotalRevdev, (V.Subtototal + V.TotalDescuento - V.TotalRevdev) * -1), Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalsindesc")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotal", Format(If(V.Subtototal >= 0, V.Subtototal, V.Subtototal * -1), Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotal")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(If(V.Subtototal + V.TotalDescuento - V.TotalNegativosSinIVA - V.TotalRevdev >= 0, V.Subtototal + V.TotalDescuento - V.TotalNegativosSinIVA - V.TotalRevdev, (V.Subtototal + V.TotalDescuento - V.TotalNegativosSinIVA - V.TotalRevdev) * -1), Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalsindesc")
                    'Dim Subb As Double
                    'Subb = V.Subtototal - V.TotalNegativosSinIVA
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotal", Format(If(V.Subtototal >= 0, V.Subtototal, V.Subtototal * -1), Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotal")
                End If
                ImpDoc.ImpND.Add(New NodoImpresionN("", "totalofertas", Format(If(V.TotalOfertas >= 0, V.TotalOfertas, V.TotalOfertas * -1), Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totalofertas")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "totaldesc", Format(If(V.TotalDescuento >= 0, V.TotalDescuento, V.TotalDescuento * -1), Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totaldesc")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "totaldesc2", Format(If(V.DescuentoG2 >= 0, V.TotalDescuento, V.TotalDescuento * -1), Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totaldesc2")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotalsinret", Format(If(V.Subtototal + V.TotalIva >= 0, V.Subtototal + V.TotalIva, (V.Subtototal + V.TotalIva) * -1), Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalsinret")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "totalcantidad", V.DaTotalCantidad(idVenta).ToString, 0), "totalcantidad")
                If V.TotalRevdev < 0 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalnegativos", Format(If((V.TotalNegativosSinIVA * -1) >= 0, (V.TotalNegativosSinIVA * -1), ((V.TotalNegativosSinIVA * -1)) * -1), Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totalnegativos")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalnegativos", Format(If((V.TotalNegativosSinIVA * -1) + V.TotalRevdev >= 0, (V.TotalNegativosSinIVA * -1) + V.TotalRevdev, ((V.TotalNegativosSinIVA * -1) + V.TotalRevdev) * -1), Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totalnegativos")
                End If
            End If

            Dim Ivas As New Collection
            Dim IvasImporte As New Collection
            DR = V.DaIvas(idVenta)
            ImpDoc.ImpNDDi.Clear()
            ImpDoc.ImpNDi2.Clear()
            Dim IAnt As Double
            'If Ivas.Count < 2 Then QuitaIvaCero = False
            While DR.Read
                If Ivas.Contains(DR("iva").ToString) = False Then
                    Ivas.Add(DR("iva"), DR("iva").ToString)
                End If
                If IvasImporte.Contains(DR("iva").ToString) = False Then

                    If V.Alterno = "0" Then
                        If IDsMonedas2.Valor(ComboBox2.SelectedIndex) = DR("idmoneda") Then
                            IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                        Else
                            IvasImporte.Add((DR("precio") * (DR("iva") / 100)) * DR("tipodecambio"), DR("iva").ToString)
                        End If
                    Else
                        If DR("precio") > 0 Then
                            If IDsMonedas2.Valor(ComboBox2.SelectedIndex) = DR("idmoneda") Then
                                IvasImporte.Add((DR("precio")) * (DR("iva") / 100), DR("iva").ToString)
                            Else
                                IvasImporte.Add(((DR("precio")) * (DR("iva") / 100)) * DR("tipodecambio"), DR("iva").ToString)
                            End If
                        Else
                            If IDsMonedas2.Valor(ComboBox2.SelectedIndex) = DR("idmoneda") Then
                                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                            Else
                                IvasImporte.Add((DR("precio") * (DR("iva") / 100)) * DR("tipodecambio"), DR("iva").ToString)
                            End If
                        End If
                    End If

                    'If IDsMonedas2.Valor(ComboBox2.SelectedIndex) = DR("idmoneda") Then
                    '    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                    'Else
                    '    IvasImporte.Add((DR("precio") * (DR("iva") / 100)) * DR("tipodecambio"), DR("iva").ToString)
                    'End If
                Else
                    IAnt = IvasImporte(DR("iva").ToString)
                    IvasImporte.Remove(DR("iva").ToString)
                    'IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                    If V.Alterno = "0" Then
                        If IDsMonedas2.Valor(ComboBox2.SelectedIndex) = DR("idmoneda") Then
                            IvasImporte.Add(IAnt + DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                        Else
                            IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)) * DR("tipodecambio"), DR("iva").ToString)
                        End If
                    Else
                        If DR("precio") > 0 Then
                            If IDsMonedas2.Valor(ComboBox2.SelectedIndex) = DR("idmoneda") Then
                                IvasImporte.Add(IAnt + (DR("precio")) * (DR("iva") / 100), DR("iva").ToString)
                            Else
                                IvasImporte.Add(IAnt + ((DR("precio")) * (DR("iva") / 100)) * DR("tipodecambio"), DR("iva").ToString)
                            End If
                        Else
                            If IDsMonedas2.Valor(ComboBox2.SelectedIndex) = DR("idmoneda") Then
                                IvasImporte.Add(IAnt + DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                            Else
                                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)) * DR("tipodecambio"), DR("iva").ToString)
                            End If
                        End If
                    End If
                End If

            End While
            DR.Close()
            If V.Alterno = "1" Then
                Dim IvaDescuento As Double
                For Each I As Double In Ivas
                    If I <> 0 Then
                        IvaDescuento = V.Descuento * (I / 100)
                        IAnt = IvasImporte(I.ToString)
                        IvasImporte.Remove(I.ToString)
                        IvasImporte.Add(IAnt - IvaDescuento, I.ToString)
                    End If
                Next
            End If
            Dim TotalesXIVa As String = ""

            'For Each I As Double In Ivas
            '    If TotalesXIVa = "" Then
            '        If I <> 0 Then
            '            TotalesXIVa = "SUBTOTAL GRAVADOS    " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), Op._formatoTotal)
            '        Else
            '            TotalesXIVa = "SUBTOTAL NO GRAVADOS " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), Op._formatoTotal)
            '        End If
            '    Else
            '        If I <> 0 Then
            '            TotalesXIVa += vbCrLf + "SUBTOTAL GRAVADOS    " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), Op._formatoTotal)
            '        Else
            '            TotalesXIVa += vbCrLf + "SUBTOTAL NO GRAVADOS " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), Op._formatoTotal)
            '        End If
            '    End If
            'Next

            If Op.FacturaComoegreso = 0 Then
                TotalesXIVa = "SUBTOTAL GRAVADOS:    " + Format(V.DaTotalGravado(V.ID), Op._formatoTotal)
                TotalesXIVa += vbCrLf + "SUBTOTAL NO GRAVADOS: " + Format(V.DaTotalNoGravado(V.ID), Op._formatoTotal)
            Else
                TotalesXIVa = "SUBTOTAL GRAVADOS:    " + Format(If(V.DaTotalGravado(V.ID) >= 0, V.DaTotalGravado(V.ID), V.DaTotalGravado(V.ID) * -1), Op._formatoTotal)
                TotalesXIVa += vbCrLf + "SUBTOTAL NO GRAVADOS: " + Format(If(V.DaTotalNoGravado(V.ID) >= 0, V.DaTotalNoGravado(V.ID), V.DaTotalNoGravado(V.ID) * -1), Op._formatoTotal)
            End If

            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalxiva", TotalesXIVa, 0), "totalxiva")
            'Try
            If IvasImporte.Contains("0") Then
                If Ivas.Count > 1 And QuitaIvaCero Then
                    IvasImporte.Remove("0")
                    Ivas.Remove("0")
                End If
            End If
            'Catch ex As Exception
            'End Try

            Cont = 0
            Dim IvasConversion As String = ""
            For Each I As Double In Ivas
                If Op.FacturaComoegreso = 0 Then
                    ImpDoc.ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
                    If V.IdConversion = 2 Then
                        IvasConversion += "Iva " + Format(I, "#0.00") + "%: " + Format(IvasImporte(I.ToString) / V.TipodeCambio, Op._formatoIva).PadLeft(Op.EspacioIva) + vbCrLf
                    Else
                        IvasConversion += "Iva " + Format(I, "#0.00") + "%: " + Format(IvasImporte(I.ToString) * V.TipodeCambio, Op._formatoIva).PadLeft(Op.EspacioIva) + vbCrLf
                    End If
                    ImpDoc.ImpNDi2.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
                Else
                    ImpDoc.ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(If(IvasImporte(I.ToString) >= 0, IvasImporte(I.ToString), IvasImporte(I.ToString) * -1), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
                    If V.IdConversion = 2 Then
                        IvasConversion += "Iva " + Format(I, "#0.00") + "%: " + Format(If(IvasImporte(I.ToString) / V.TipodeCambio >= 0, IvasImporte(I.ToString) / V.TipodeCambio, IvasImporte(I.ToString) / V.TipodeCambio * -1), Op._formatoIva).PadLeft(Op.EspacioIva) + vbCrLf
                    Else
                        IvasConversion += "Iva " + Format(I, "#0.00") + "%: " + Format(If(IvasImporte(I.ToString) * V.TipodeCambio >= 0, IvasImporte(I.ToString) * V.TipodeCambio, IvasImporte(I.ToString) * V.TipodeCambio * -1), Op._formatoIva).PadLeft(Op.EspacioIva) + vbCrLf
                    End If
                    ImpDoc.ImpNDi2.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(If(IvasImporte(I.ToString) >= 0, IvasImporte(I.ToString), IvasImporte(I.ToString) * -1), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
                End If
                Cont += 1
            Next

            If V.ImpLocales.Count > 0 Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "implocales", V.DaTextoImpLocales(Op._formatoIva, Op.EspacioIva, Op.FacturaComoegreso), 0), "implocales")
            Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "implocales", "", 0), "implocales")
            End If

            If Op.FacturaComoegreso = 0 Then
                If V.ISR <> 0 Then
                    ImpDoc.ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
                    ImpDoc.ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "isr")
                    Cont += 1
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "isr")
                End If
                If V.IvaRetenido <> 0 Then
                    ImpDoc.ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", Format(V.TotalIvaRetenido, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
                    ImpDoc.ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenido, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "ivaret")
                    Cont += 1
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenido, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "ivaret")
                End If
                ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "total")
                If V.IdConversion = 2 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "Total C:", Format(V.TotalVenta / V.TipodeCambio, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "totalcon")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "Subtotal C:", Format(V.Subtototal / V.TipodeCambio, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalcon")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "Total C:", Format(V.TotalVenta * V.TipodeCambio, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "totalcon")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "Subtotal C:", Format(V.Subtototal * V.TipodeCambio, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalcon")
                End If
                ImpDoc.ImpND.Add(New NodoImpresionN("", "ivacon", IvasConversion, 0), "ivacon")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalSinRetencion + V.DescuentoG2, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "totalsil")

                Dim f As New StringFunctions
                Dim CL As New CLetras
                'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion), 0), "totalletra")
                If V.TotalaPagar >= 0 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(Format(V.TotalVenta, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletra")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(Format(V.TotalVenta * -1, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletra")
                End If
                If V.TotalSinRetencion >= 0 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletrasil", CL.LetrasM(Format(V.TotalSinRetencion, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletrasil")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletrasil", "MENOS " + CL.LetrasM(Format(V.TotalSinRetencion * -1, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletrasil")
                End If
            Else
                If V.ISR <> 0 Then
                    ImpDoc.ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(If(V.TotalISR >= 0, V.TotalISR, V.TotalISR * -1), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
                    ImpDoc.ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(If(V.TotalISR >= 0, V.TotalISR, V.TotalISR * -1), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "isr")
                    Cont += 1
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "isr")
                End If
                If V.IvaRetenido <> 0 Then
                    ImpDoc.ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", Format(If(V.TotalIvaRetenido >= 0, V.TotalIvaRetenido, V.TotalIvaRetenido * -1), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
                    ImpDoc.ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(If(V.TotalIvaRetenido >= 0, V.TotalIvaRetenido, V.TotalIvaRetenido * -1), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "ivaret")
                    Cont += 1
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenido, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "ivaret")
                End If
                ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(If(V.TotalVenta >= 0, V.TotalVenta, V.TotalVenta * -1), Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "total")
                If V.IdConversion = 2 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "Total C:", Format(V.TotalVenta / V.TipodeCambio, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "totalcon")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "Subtotal C:", Format(V.Subtototal / V.TipodeCambio, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalcon")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "Total C:", Format(V.TotalVenta * V.TipodeCambio, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "totalcon")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "Subtotal C:", Format(V.Subtototal * V.TipodeCambio, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalcon")
                End If
                ImpDoc.ImpND.Add(New NodoImpresionN("", "ivacon", IvasConversion, 0), "ivacon")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(If(V.TotalSinRetencion + V.DescuentoG2 >= 0, V.TotalSinRetencion + V.DescuentoG2, (V.TotalSinRetencion + V.DescuentoG2) * -1), Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "totalsil")

                Dim f As New StringFunctions
                Dim CL As New CLetras
                'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion), 0), "totalletra")
                If V.TotalaPagar >= 0 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(Format(V.TotalVenta, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletra")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(Format(V.TotalVenta * -1, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletra")
                End If
                If V.TotalSinRetencion >= 0 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletrasil", CL.LetrasM(Format(V.TotalSinRetencion, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletrasil")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletrasil", CL.LetrasM(Format(V.TotalSinRetencion * -1, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletrasil")
                End If

            End If
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
            Dim FP As New dbFormasdePago(V.IdFormadePago, MySqlcon)
            If FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "titulo", Op.TituloParcialidad, 0), "titulo")
            Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "titulo", Op.TituloFactura, 0), "titulo")
            End If
            'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
            If V.IdFormadePago <> 98 Then
                Dim strMetodos As String = ""
                Dim MeP As New dbVentasAddMetodos(MySqlcon)
                DR = MeP.ConsultaReader(0, V.ID)
                While DR.Read()
                    If strMetodos <> "" Then strMetodos += vbNewLine
                    If DR("clavesat") < 1000 Then
                        strMetodos += Format(DR("clavesat"), "00") + " " + DR("nombre")
                    Else
                        strMetodos += DR("nombre")
                    End If
                End While
                DR.Close()
                If strMetodos <> "" Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "metodopago", strMetodos, 0), "metodopago")
                Else
                    If FP.clavesat < 1000 Then
                        ImpDoc.ImpND.Add(New NodoImpresionN("", "metodopago", Format(FP.clavesat, "00") + " " + FP.Nombre, 0), "metodopago")
                    Else
                        ImpDoc.ImpND.Add(New NodoImpresionN("", "metodopago", FP.Nombre, 0), "metodopago")
                    End If
                End If
                If FP.Tipo <> dbFormasdePago.Tipos.Parcialidad Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "formadepago", "PAGO EN UNA SOLA EXHIBICIÓN", 0), "formadepago")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "folioorigen", "", 0), "folioorigen")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "serieorigen", "", 0), "serieorigen")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "montoorigen", "", 0), "montoorigen")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "fechaorigen", "", 0), "fechaorigen")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "uuidorigen", "", 0), "uuidorigen")
                Else
                    V.ObtenerFacturaOriginal(V.IdVentaOrigen)
                    If V.Parcialidades <> 1 Then
                        ImpDoc.ImpND.Add(New NodoImpresionN("", "formadepago", "PARCIALIDAD " + V.Parcialidad.ToString + " DE " + V.Parcialidades.ToString, 0), "formadepago")
                    Else
                        ImpDoc.ImpND.Add(New NodoImpresionN("", "formadepago", "PARCIALIDAD " + V.Parcialidad.ToString, 0), "formadepago")
                    End If
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "folioorigen", Format(V.FolioOrigen, "00000"), 0), "folioorigen")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "serieorigen", V.SerieOrigen, 0), "serieorigen")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "montoorigen", Format(V.MontoOrigen, Op._formatoTotal), 0), "montoorigen")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "fechaorigen", V.FechaOrigen, 0), "fechaorigen")
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "uuidorigen", V.FolioUUIDOrigen, 0), "uuidorigen")
                End If
            Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "metodopago", "NA", 0), "metodopago")
                If V.Parcialidades <> 1 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "formadepago", "PAGO EN " + V.Parcialidades.ToString + " PARCIALIDADES", 0), "formadepago")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "formadepago", "PAGO EN PARCIALIDADES", 0), "formadepago")
                End If
                ImpDoc.ImpND.Add(New NodoImpresionN("", "folioorigen", "", 0), "folioorigen")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "serieorigen", "", 0), "serieorigen")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "montoorigen", "", 0), "montoorigen")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "fechaorigen", "", 0), "fechaorigen")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "uuidorigen", "", 0), "uuidorigen")
            End If

            'Else
            'ImpND.Add(New NodoImpresionN("", "metodopago", "No Identificado", 0), "metodopago")
            'End If
            If FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
                If V.FormaPagoNA = 0 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "condicionpago", "CONTADO", 0), "condicionpago")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "condicionpago", "N/A", 0), "condicionpago")
                End If
                ImpDoc.ImpND.Add(New NodoImpresionN("", "diascredito", "", 0), "diascredito")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "limitecredito", "", 0), "limitecredito")
            Else
                If V.FormaPagoNA = 0 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "condicionpago", "CRÉDITO", 0), "condicionpago")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "condicionpago", "N/A", 0), "condicionpago")
                End If
                ImpDoc.ImpND.Add(New NodoImpresionN("", "diascredito", V.Cliente.CreditoDias.ToString + " Días.", 0), "diascredito")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "limitecredito", Format(DateAdd(DateInterval.Day, V.Cliente.CreditoDias, CDate(V.Fecha)), "yyyy-MM-dd"), 0), "limitecredito")
            End If


            Dim CP As New dbVentasCartaPorte(V.ID, MySqlcon)
            If CP.Origen = "Nohay" Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cporigen", "", 0), "cporigen")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpdestino", "", 0), "cpdestino")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpchofer", "", 0), "cpchofer")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpmercancia", "", 0), "cpmercancia")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpmatricula", "", 0), "cpmatricula")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cppeso", "", 0), "cppeso")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpfecha", "", 0), "cpfecha")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpvalorunitario", "", 0), "cpvalorunitario")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpvalordeclarado", "", 0), "cpvalordeclarado")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpreferencia", "", 0), "cpreferencia")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cppedimento", "", 0), "cppedimento")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cppedimentofecha", "", 0), "cppedimentofecha")
            Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cporigen", CP.Origen, 0), "cporigen")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpdestino", CP.Destino, 0), "cpdestino")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpchofer", CP.Chofer, 0), "cpchofer")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpmercancia", CP.Mercancia, 0), "cpmercancia")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpmatricula", CP.Matricula, 0), "cpmatricula")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cppeso", CP.Peso, 0), "cppeso")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpfecha", CP.Fecha, 0), "cpfecha")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpvalorunitario", CP.ValorUnitario, 0), "cpvalorunitario")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpvalordeclarado", CP.ValorDeclarado, 0), "cpvalordeclarado")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cpreferencia", CP.Referencia, 0), "cpreferencia")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cppedimento", CP.Pedimento, 0), "cppedimento")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cppedimentofecha", CP.FechaPedimento, 0), "cppedimentofecha")
            End If

            If V.IdConversion = 2 Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "leyendadolar", "", 0), "leyendadolar")
            Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "leyendadolar", "El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar a la venta frente al peso vigente al día de su pago.", 0), "leyendadolar")
            End If
            If V.Estado = Estados.Cancelada Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
            Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
            End If
            ImpDoc.Posicion = 0
            Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
            If Op.FacturaComoegreso = 0 Then
                ImpDoc.CodigoBidimensional = CB.Encode("?re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, "0000000000.000000") + "&id=" + V.uuid, System.Text.Encoding.UTF8)
            Else
                ImpDoc.CodigoBidimensional = CB.Encode("?re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(If(V.TotalVenta >= 0, V.TotalVenta, V.TotalVenta * -1), "0000000000.000000") + "&id=" + V.uuid, System.Text.Encoding.UTF8)
            End If
            ImpDoc.NumeroPagina = 1
        Catch ex As Exception
            MsgBox("P1 " + ex.Message + " E=" + ImpDoc.ImpND.Item(ImpDoc.ImpND.Count - 1).DataPropertyName, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub LlenaNodosImpresionRet()
        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
        ImpDoc.ImpND.Clear()
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrefiscal2", Sucursal.NombreFiscal, 0), "nombrefiscal2")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "curp", Sucursal.CURP, 0), "curp")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "mesini", Format(CDate(V.Fecha), "MM"), 0), "mesini")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "mesfin", Format(CDate(V.Fecha), "MM"), 0), "mesfin")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "ejercicio", Format(CDate(V.Fecha), "yyyy"), 0), "ejercicio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "total1", Format(V.Subtototal, Op._formatoTotal).PadLeft(13), 0), "total1")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "total2", Format(V.Subtototal, Op._formatoTotal).PadLeft(13), 0), "total2")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "total3", "$0.00", 0), "total3")
        ImpDoc.ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, Op._formatoIva).PadLeft(13), 0), "isr")
        ImpDoc.ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenido, Op._formatoIva).PadLeft(13), 0), "ivaret")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "ieps", "$0.00", 0), "ieps")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "repcliente", V.Cliente.Representante, 0), "repcliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "repcliente2", V.Cliente.Representante, 0), "repcliente2")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfcrepcliente", V.Cliente.RepresentanteRFC, 0), "rfcrepcliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "curprepcliente", V.Cliente.RepresentanteRegistro, 0), "curprepcliente")
    End Sub



    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ImpDoc.DibujaPaginaN(e.Graphics)
        If ImpDoc.MasPaginas = True Or ImpDoc.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(ImpDoc.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If
        If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
            e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO INVÁLIDO.", New Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point), Brushes.Red, 2, 2)
        End If
        e.HasMorePages = ImpDoc.MasPaginas
    End Sub


    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If Op.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Dim XMLAddenda As String = ""
        Dim CadenaOriginalComp As String = ""
        If Estado <> Estados.SinGuardar Then
            Select Case UsaAdenda
                Case 1
                    Dim Ad As New dbAdendasFemsa(MySqlcon)
                    Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
                    Ad.BuscaAddenda(idVenta, 0)
                    If Ad.ID <> 0 Then
                        XMLAddenda = Ad.CreaXMLCFDI(Eselectronica, S.Email)
                    End If
                Case 2
                    Dim frmA As New frmAddendaOxxo(idVenta, True)
                    frmA.elementos = Articulos
                    frmA.Serie = TextBox11.Text
                    frmA.Folio = TextBox2.Text
                    frmA.Doc = 0
                    frmA.EsElectronica = Eselectronica
                    frmA.Total = CDbl(Label14.Text)
                    frmA.TipodeCambio = CDbl(TextBox10.Text)
                    frmA.Moneda = If(IDsMonedas.Valor(ComboBox2.SelectedIndex) = 2, "MXN", "USD")
                    frmA.ShowDialog()
                    XMLAddenda = frmA.XMLResultado
                    frmA.Dispose()
                Case 3
                    Dim C As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
                    C.DaTotal(idVenta, 2, Op._Sinnegativos, Op._CalculoAlterno)
                    Dim Suc As New dbSucursales(C.IdSucursal, MySqlcon)
                    Dim FrmA As New frmAddendaLey(idVenta, C.TotalDescuento, Eselectronica, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Suc.Email)
                    FrmA.ShowDialog()
                    XMLAddenda = FrmA.strXML
                    FrmA.Dispose()
                Case 5
                    'Addenda modelo
                    Dim moneda As New dbMonedas(IDsMonedas2.Valor(ComboBox2.SelectedIndex), MySqlcon)
                    Dim frmAm As New frmAdendaGrupoModelo(idVenta, moneda.Abreviatura, TextBox10.Text)
                    frmAm.ShowDialog()
                    XMLAddenda = frmAm.adendaXML
                    frmAm.Dispose()
                Case 6
                    Dim frmA As New frmComplementoINE(idVenta)
                    frmA.ShowDialog()
                    XMLAddenda = frmA.XML
                    CadenaOriginalComp = frmA.cadenaOriginal
                    frmA.Dispose()
                Case 7
                    Dim C As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
                    C.DaTotal(idVenta, C.IdConversion, Op._Sinnegativos, Op._CalculoAlterno)
                    Dim frmA As New frmExportacion(idVenta, C.TotalVenta)
                    frmA.ShowDialog()
                    XMLAddenda = frmA.xml
                    CadenaOriginalComp = frmA.cadenaOriginal
                    frmA.Dispose()
                Case 8 'Soriana
                    Dim frmA As New frmAddendaSoriana(idVenta)
                    frmA.ShowDialog()
                    XMLAddenda = frmA.addenda.CreaXML
                    frmA.Dispose()
            End Select
        Else
            Modificar(Estados.SinGuardar)
        End If
        Select Case Eselectronica
            Case 0
                Imprimir(idVenta)
            Case 1
                CadenaOriginal(Estado, XMLAddenda)
            Case 2
                CadenaOriginali(Estado, XMLAddenda, CadenaOriginalComp, True, True)
            Case 3
                CadenaOriginali33(Estado, XMLAddenda, CadenaOriginalComp, True, True)
        End Select
    End Sub

    Private Sub TextBox12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Op._TipoSelAlmacen = "0" Then
                BotonAgregar()
            Else
                If cmbAlmacen.SelectedIndex <= 0 Then
                    cmbAlmacen.Focus()
                Else
                    BotonAgregar()
                End If
            End If
        End If
        If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
            If UsaFormula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtArticulo)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.00")
                    TextBox4.Text = Fo.FormulaString
                End If
                Fo.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F2 And IdInventario <> 0 Then
            Dim Precio As Double
            If CantidadMostrar <> 0 Then
                Precio = CDbl(TextBox6.Text) / CantidadMostrar
            Else
                Precio = 0
            End If
            Dim FE As New frmVentasDaEquivalencia(IdInventario, CDbl(TextBox5.Text), CantidadMostrar, TipoCantidadMostrar, Precio, True)
            If FE.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox5.Text = FE.Cantidad.ToString
                CantidadMostrar = FE.CantidadM
                TipoCantidadMostrar = FE.TipoCantidadM
                If FE.Cantidad = FE.CantidadM Then
                    SinConcersion = True
                Else
                    SinConcersion = False
                End If
                If FE.Cantidad <> 0 Then
                    TextBox12.Text = Format((FE.CantidadM * FE.PrecioM) / FE.Cantidad, "0.00")
                Else
                    TextBox12.Text = "0"
                End If
                Label20.Text = CantidadMostrar.ToString

            End If
            FE.Dispose()
        End If
        If e.KeyCode = Keys.F1 And IdInventario <> 0 Then
            Dim SP As New frmSelectorPrecios(IdInventario)
            If SP.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox12.Text = SP.Precio.ToString("0.00")
            End If
            SP.Dispose()
        End If
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        If IdInventario <> 0 Or IdVariante <> 0 Then
            If IsNumeric(TextBox12.Text) Then
                PrecioU = CDbl(TextBox12.Text)
                If IsNumeric(TextBox5.Text) Then
                    TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
                End If
            End If
        End If
    End Sub

    Private Sub TextBox9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox8.Focus()
        End If
    End Sub



    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged

        If Descontando Then
            Descontando = False
            If IsNumeric(TextBox9.Text) And IsNumeric(TextBox6.Text) Then
                TextBox17.Text = CDbl(TextBox6.Text) * CDbl(TextBox9.Text) / 100
            Else
                TextBox17.Text = "0"
            End If
            Descontando = True
        End If
    End Sub

    Private Sub DGDetalles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim et As New frmVentasTextoExtra(TextBox4.Text, 2000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox4.Text = et.Texto
        End If

    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 500, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
        End If
    End Sub
    Private Sub ImprimirSeries()
        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Rep = New repVentasSeries
        Rep.SetDataSource(V.ReporteVentasSeries(idVenta))
        'Rep.SetParameterValue("Encabezado", O._NombreEmpresa)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
        RV.Focus()
    End Sub
    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        ImprimirSeries()
    End Sub

    Private Sub Imprimir(pIdVenta As Integer)
        Dim V As New dbVentas(pIdVenta, MySqlcon, Op._Sinnegativos)
        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        AddError("FAC: IMP: Serie:" + V.Serie + " Folio:" + V.Folio.ToString + " Cliente: " + V.IdCliente.ToString + " Total: " + V.TotalaPagar.ToString + " Estado:" + Estado.ToString, "Ventas", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), V.ID)
        ImprimirFactura(Op.TituloOriginalFactura, False, pIdVenta, False, False, "1")
        If Op.Copiaflujoventas = 1 Then
            ImprimirFactura(Op.TituloOriginalFactura, True, pIdVenta, True, False, "1")
        End If
        If Op._ActivarPDF = "1" Then
            ImprimirFactura(Op.TituloOriginalFactura, False, pIdVenta, False, True, Op._MostrarPDF)
        End If
        Dim fdp As New dbFormasdePago(V.IdFormadePago, MySqlcon)
        If (Op.ActivarCopiaFactura = 1 And fdp.Tipo = dbFormasdePago.Tipos.Contado) Or (Op.ActivarCopiaFacturaCredito And fdp.Tipo = dbFormasdePago.Tipos.Credito) Then
            ImprimirFactura(Op.TituloCopiaFactura, True, pIdVenta, False, False, "1")
        End If
        If (Op.ActivarCopia2Factura = 1 And fdp.Tipo = dbFormasdePago.Tipos.Contado) Or (Op.ActivarCopiaFacturaCredito2 And fdp.Tipo = dbFormasdePago.Tipos.Credito) Then
            ImprimirFactura(Op.TituloCopia2Factura, True, pIdVenta, False, False, "1")
        End If
        If FacturaGlobal.ISR <> 0 Or FacturaGlobal.IvaRetenido <> 0 Then
            If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                ImprimirRetenciones()
            End If
        End If
        imprimirNotarial()
        Dim Ss As New dbInventarioSeries(MySqlcon)
        If Ss.CantidadDeSeriesAgregadasaVenta(idVenta, 0) > 0 Then
            If MsgBox("¿Imprimir listado de series?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                ImprimirSeries()
            End If
        End If
    End Sub
    Private Sub ImprimirFactura(pTitulo As String, pEsCopia As Boolean, pIdVenta As Integer, pFlujo As Boolean, pEsPDF As Boolean, pMostrarPDF As String)
        Try
            Dim Factura As New dbVentas(pIdVenta, MySqlcon, Op._Sinnegativos)
            CadenaCFDI = "||1.0|" + FacturaGlobal.uuid + "|" + FacturaGlobal.FechaTimbrado + "|" + FacturaGlobal.SelloCFD + "|" + FacturaGlobal.NoCertificadoSAT + "||"
            ImpDoc.IdSucursal = Factura.IdSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.Venta
            ImpDoc.TipoDocumentoT = TiposDocumentos.Venta + 1000
            If pEsPDF = False Then
                ImpDoc.TipoDocumentoImp = TiposDocumentos.Venta
            Else
                ImpDoc.TipoDocumentoImp = TiposDocumentos.PDF
            End If
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.FacturaPDF
            ImpDoc.Inicializar(pFlujo)
            LlenaNodosImpresion(pTitulo)
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Factura.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Factura.Fecha), "yyyy") + "\" + Format(CDate(Factura.Fecha), "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(CDate(Factura.Fecha), "yyyy") + "\" + Format(CDate(Factura.Fecha), "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF, pMostrarPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            If pEsCopia Then
                PrintDocument1.DocumentName = "PSSFACTURA_COPIA-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString
            Else
                PrintDocument1.DocumentName = "PSSFACTURA-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString
            End If
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ImprimirRetenciones()
        Try
            Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
            ImpDoc.IdSucursal = S.ID
            ImpDoc.TipoDocumento = TiposDocumentos.FormatoRetencion
            ImpDoc.TipoDocumentoT = TiposDocumentos.FormatoRetencion + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.Venta
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.FacturaPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresionRet()
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "PSSRET " + TextBox11.Text + " " + TextBox2.Text
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub TextBox11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged

    End Sub

    Private Sub DateTimePicker1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox3.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker2.Value = DateTimePicker1.Value
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim FC As New frmClientes(1, idCliente)
        If FC.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = ""
            TextBox1.Text = FC.CodigoCliente
        End If
        FC.Dispose()
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim V As New dbVentas(MySqlcon)
        V.DaDatosTimbrado(idVenta)
        Dim FDT As New frmVentasDatosTimbrado(V.uuid, V.FechaTimbrado, V.NoCertificadoSAT, V.SelloCFD, V.SelloSAT)
        FDT.ShowDialog()
        FDT.Dispose()
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbVentas(MySqlcon)
                V.ActualizaComentario(idVenta, TextBox14.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        If Estado = Estados.Pendiente Or Estado = Estados.Inicio Or Estado = Estados.SinGuardar Then
            Dim Sf As New dbSucursalesFolios(MySqlcon)
            Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Factura, iTipoFacturacion)
            TextBox11.Text = Sf.Serie
            Eselectronica = iTipoFacturacion
            If Sf.EsElectronica > 1 Then
                CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
            End If
            Dim V As New dbVentas(MySqlcon)
            TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion, Op._ModoFoliosB).ToString
            If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
            If CInt(TextBox2.Text) > Sf.FolioFinal Then
                LimitedeFolios = True
                MsgBox("Se ha alcanzado el límite de folios.", MsgBoxStyle.Information, GlobalNombreApp)
            Else
                LimitedeFolios = False
            End If
        End If
    End Sub

    Private Sub CheckScroll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckScroll.CheckedChanged
        If CheckScroll.Checked Then
            My.Settings.ventasscroll = True
        Else
            My.Settings.ventasscroll = False
        End If
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        Select Case UsaAdenda
            Case 1
                Dim frmA As New frmAddendaFemsa(0, If(IDsMonedas.Valor(ComboBox2.SelectedIndex) = 2, "MXN", "USD"), idVenta, Estado, Eselectronica, S.Email, False)
                frmA.ShowDialog()
                frmA.Dispose()
            Case 2
                Dim frmA As New frmAddendaOxxo(idVenta, False)
                frmA.elementos = Articulos
                frmA.Serie = TextBox11.Text
                frmA.Folio = TextBox2.Text
                frmA.Doc = 0
                frmA.EsElectronica = Eselectronica
                frmA.Total = CDbl(Label14.Text)
                frmA.TipodeCambio = CDbl(TextBox10.Text)
                frmA.Moneda = If(IDsMonedas.Valor(ComboBox2.SelectedIndex) = 2, "MXN", "USD")
                frmA.ShowDialog()
                frmA.Dispose()
            Case 3
                Dim C As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
                C.DaTotal(idVenta, 2, Op._Sinnegativos, Op._CalculoAlterno)
                Dim Suc As New dbSucursales(C.IdSucursal, MySqlcon)
                Dim FrmA As New frmAddendaLey(idVenta, C.TotalDescuento, Eselectronica, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Suc.Email)
                FrmA.ShowDialog()
                'XMLAdenda = FrmA.strXML
                FrmA.Dispose()
            Case 4
                Dim frmA As New frmAdendaPEPSICO(idVenta, False)
                frmA.ShowDialog()
                frmA.Dispose()
            Case 5
                'Modelo
                Dim moneda As New dbMonedas(IDsMonedas2.Valor(ComboBox2.SelectedIndex), MySqlcon)
                Dim frmAm As New frmAdendaGrupoModelo(idVenta, moneda.Abreviatura, TextBox10.Text)
                frmAm.ShowDialog()
                frmAm.Dispose()
            Case 6
                Dim frmA As New frmComplementoINE(idVenta)
                frmA.ShowDialog()
                frmA.Dispose()
            Case 7
                Dim C As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
                C.DaTotal(idVenta, C.IdConversion, Op._Sinnegativos, Op._CalculoAlterno)
                Dim frmA As New frmExportacion(idVenta, C.TotalVenta)
                frmA.ShowDialog()
                frmA.Dispose()
            Case 8 'Soriana
                Dim frmA As New frmAddendaSoriana(idVenta)
                frmA.ShowDialog()
                frmA.Dispose()
            Case 9 'Walmart
                Dim frmA As New frmAddendaWalmart(idVenta)
                frmA.ShowDialog()
                frmA.Dispose()
        End Select

    End Sub

    Private Sub ComboBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbAlmacen.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Op._CursorVentas = "0" Then
                TextBox5.Focus()
            Else
                TextBox3.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtIEPS.Focus()
        End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        If idVenta <> 0 Then
            Dim f As New frmVentasRemisionesConsulta(ModosDeBusqueda.Principal, 1, idVenta)
            f.ShowDialog()
            f.Dispose()
        Else
            MsgBox("No hay factura activa.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    'Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
    '    If IdDetalle <> 0 Then
    '        Dim F As New frmVentasAduana(IdDetalle)
    '        F.ShowDialog()
    '        F.Dispose()
    '    Else
    '        MsgBox("Debe seleccionar un concepto.", MsgBoxStyle.Information, GlobalNombreApp)
    '    End If
    'End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        If idVenta <> 0 Then
            Dim F As New frmVentasCartaPorte(idVenta, Estado)
            F.ShowDialog()
            F.Dispose()
        Else
            MsgBox("Debe agregar minimo un concepto.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    '*************Descuentos***********
    Private Sub hayDescuento()

        Dim CD As New dbVentasInventario(MySqlcon)
        Dim idDescuento As Integer
        idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim TablaDesc As DataTable
        Dim des As Double = 0
        Dim descripcion As String = ""

        If idDescuento = 0 Then
            'No hay descuento
        Else
            TablaDesc = P.tablaDesc(idDescuento)
            If TablaDesc.Rows(0)(9).ToString() <> "Promocion" Then

                If TablaDesc.Rows(0)(9).ToString() = "Porcentaje" Then
                    des = TotalPorcentaje(CDbl(TextBox6.Text), Integer.Parse(TablaDesc.Rows(0)(2).ToString()))
                    'des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: " + TablaDesc.Rows(0)(2).ToString() + " %"
                Else
                    des = Double.Parse(TablaDesc.Rows(0)(2).ToString())
                    des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: $" + TablaDesc.Rows(0)(2).ToString() + " P/U"
                End If

                CD.Guardar(idVenta, 1, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, 0, Double.Parse(TextBox5.Text), 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), ComboBox7.Text, 0, If(pnlUbicacion.Visible, cmbUbicacion.SelectedValue, ""), txtTarima.Text)
                P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idVenta, "VentasN")

                ConsultaDetalles()
                NuevoConcepto()

            Else
                Promociones(Integer.Parse(TablaDesc.Rows(0)(10).ToString()), TablaDesc.Rows(0)(2).ToString(), CDbl(TextBox12.Text), "DESCUENTO: promoción " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, Integer.Parse(TablaDesc.Rows(0)(8).ToString()))
                'hay promocion
                'primero checar si se cumple la promocion
                'si no añadir

            End If

        End If


        'Si haye descuento, agregar el renglon a la venta
        'agregarlo a la tabla de descuentos

    End Sub
    Public Sub Promociones(ByVal idDescuento As Integer, ByVal valor As String, ByVal precio As Double, ByVal descripcion As String, ByVal idProducto As Integer)
        Dim cDesc As Integer = 0
        Dim des As Double = 0
        Dim cant As Integer
        idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim regAnadir As Integer = 0
        Dim regDesc As Integer = 0
        valorProm(valor) 'esto establece los valores 2 x 1  y esos
        'primero que agregue el renglon a la db
        cant = Int(Double.Parse(TextBox5.Text)) 'cantidad de productos que se estan registrando
        For i As Integer = 1 To cant
            P.guardarPromocion(idVenta, idDescuento, idProducto, CD.UltomoRegistro())
        Next


        If P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count() >= promocion1 Then

            regAnadir = Int(P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count() Mod promocion1)
            regDesc = Int(P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

            cDesc = promocion1 - promocion2
            des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
            des = des * regDesc

            CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, 0, regDesc, 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), "", 0, If(pnlUbicacion.Visible, cmbUbicacion.SelectedValue, ""), txtTarima.Text)
            P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idVenta, "VentasN")

            ConsultaDetalles()
            NuevoConcepto()
            P.EliminarDesc(idVenta, idDescuento, idProducto)
            'anadir registros faltantes
            For i As Integer = 1 To regAnadir
                P.guardarPromocion(idVenta, idDescuento, idProducto, CD.UltomoRegistro())
            Next

        End If

    End Sub
    Public Sub modificarPromociones(ByVal idDescuento As Integer, ByVal valor As String, ByVal precio As Double, ByVal descripcion As String, ByVal idProducto As Integer)
        Dim cDesc As Integer = 0
        Dim des As Double = 0
        Dim regAnadir As Integer = 0
        idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim regDesc As Integer = 0
        Dim mayor As Integer
        valorProm(valor)
        If cantAntModificar <= Int(Double.Parse(TextBox5.Text)) Then
            mayor = Int(Double.Parse(TextBox5.Text)) - cantAntModificar 'los que estan de mas

            For i As Integer = 1 To mayor 'agregar los que se almacenaron
                P.guardarPromocion(idVenta, idDescuento, idProducto, CD.UltomoRegistro())
            Next

            If P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count() >= promocion1 Then

                regAnadir = Int(P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count() Mod promocion1)
                regDesc = Int(P.buscarPromociones(idVenta, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

                cDesc = promocion1 - promocion2
                des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
                des = des * regDesc

                CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, 0, regDesc, 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), "", 0, If(pnlUbicacion.Visible, cmbUbicacion.SelectedValue, ""), txtTarima.Text)
                P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idVenta, "VentasN")

                ConsultaDetalles()
                NuevoConcepto()
                P.EliminarDesc(idVenta, idDescuento, idProducto)
                'anadir registros faltantes
                For i As Integer = 1 To regAnadir
                    P.guardarPromocion(idVenta, idDescuento, idProducto, CD.UltomoRegistro())
                Next



            End If

        Else
            'hay que eliminar todos los registros de promociones y hacer los calculos otra vez
            P.EliminarDesc(idVenta, idDescuento, idProducto)
            P.EliminarDescAnadidos(idVenta, descripcion)
            Dim dt As DataTable
            Dim tot As Double = 0
            Dim tot2 As Integer
            dt = P.buscarDesAnadidos(idVenta, IdInventario)

            For i As Integer = 0 To dt.Rows.Count - 1
                tot = tot + Double.Parse(dt.Rows(i)(3).ToString())
            Next

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

                CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, 0, regDesc, 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), "", 0, If(pnlUbicacion.Visible, cmbUbicacion.SelectedValue, ""), txtTarima.Text)
                P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idVenta, "VentasN")

                ConsultaDetalles()
                NuevoConcepto()
                P.EliminarDesc(idVenta, idDescuento, idProducto)
                'anadir registros faltantes
                For i As Integer = 1 To regAnadir
                    P.guardarPromocion(idVenta, idDescuento, idProducto, CD.UltomoRegistro())
                Next
            End If

        End If







    End Sub

    Public Sub eliminarPromocion()
        Dim cDesc As Integer = 0
        Dim des As Double = 0
        'Dim cant As Integer
        Dim regAnadir As Integer = 0
        Dim precio As Double
        Dim descripcion As String
        Dim idDescuento As Integer
        Dim idProducto As Integer
        idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim regDesc As Integer = 0
        'Dim mayor As Integer
        Dim TablaDesc As DataTable
        Dim valor As String
        TablaDesc = P.tablaDesc(idDescuento)
        valor = TablaDesc.Rows(0)(2).ToString()
        valorProm(valor)
        descripcion = "DESCUENTO: promoción " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto
        idProducto = Integer.Parse(TablaDesc.Rows(0)(8).ToString())
        precio = Double.Parse(TextBox12.Text)
        P.EliminarDesc(idVenta, idDescuento, idProducto)
        P.EliminarDescAnadidos(idVenta, descripcion)
        Dim dt As DataTable
        Dim tot As Double = 0
        Dim tot2 As Integer
        dt = P.buscarDesAnadidos(idVenta, IdInventario)

        For i As Integer = 0 To dt.Rows.Count - 1
            tot = tot + Double.Parse(dt.Rows(i)(3).ToString())
        Next

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

            CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, 0, regDesc, 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), "", 0, If(pnlUbicacion.Visible, cmbUbicacion.SelectedValue, ""), txtTarima.Text)
            P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idVenta, "VentasN")

            ConsultaDetalles()
            NuevoConcepto()
            P.EliminarDesc(idVenta, idDescuento, idProducto)
            'anadir registros faltantes
            For i As Integer = 1 To regAnadir
                P.guardarPromocion(idVenta, idDescuento, idProducto, CD.UltomoRegistro())
            Next
        End If

    End Sub
    Public Sub modificarDescuento(ByVal idMod As Integer)
        Dim CD As New dbVentasInventario(MySqlcon)
        Dim idDescuento As Integer
        idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim TablaDesc As DataTable
        Dim des As Double = 0
        Dim descripcion As String = ""

        If idDescuento = 0 Then
            'No hay descuento
        Else
            TablaDesc = P.tablaDesc(idDescuento)
            If TablaDesc.Rows(0)(9).ToString() <> "Promocion" Then

                If TablaDesc.Rows(0)(9).ToString() = "Porcentaje" Then
                    des = TotalPorcentaje(CDbl(TextBox6.Text), Integer.Parse(TablaDesc.Rows(0)(2).ToString()))
                    'des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: " + TablaDesc.Rows(0)(2).ToString() + " %"
                Else
                    des = Double.Parse(TablaDesc.Rows(0)(2).ToString())
                    des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: $" + TablaDesc.Rows(0)(2).ToString() + " P/U"
                End If
                CD.Modificar(idMod, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), CDbl(TextBox9.Text), Double.Parse(TextBox5.Text), 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), "", 0, "", txtTarima.Text)
                ' CD.Guardar(idVenta, 1, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0, 0, Double.Parse(TextBox5.Text), 1)
                'P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idVenta)
                P.ModificarDescuento(idMod, idDescuento, idVenta, "VentasN")
                ConsultaDetalles()
                NuevoConcepto()
            Else
                'promociones
                ' modificarPromociones(idDescuento, TablaDesc.Rows(0)(2).ToString(), IDsMonedas.Valor(TextBox12.Text), "DESCUENTO: promoción " + TablaDesc.Rows(0)(2).ToString()+ " " + nombreProducto, IdInventario)
                modificarPromociones(idDescuento, TablaDesc.Rows(0)(2).ToString(), Double.Parse(TextBox12.Text), "DESCUENTO: promoción " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, IdInventario)
                'ByVal idDescuento As Integer, ByVal valor As String, ByVal precio As Double, ByVal descripcion As String, ByVal idProducto As Integer

            End If
        End If


        'Si haye descuento, agregar el renglon a la venta
        'agregarlo a la tabla de descuentos
    End Sub

    Public Sub eliminarDescuescuento(ByVal idElim As Integer, ByVal Tipo As String)
        If Tipo <> "Promocion" Then
            CD.Eliminar(idElim)
            P.EliminarDesc(idElim, "VentasN")
        Else
            eliminarPromocion()
        End If

    End Sub
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
    Public Function horaFormato() As String
        Dim fechita As String
        ' Dim hora As String = Now.ToString("HH:mm:ss")
        Dim Aux As String = ""
        fechita = Now.ToString("HH:mm:ss")

        For j As Integer = 0 To 7
            Aux = Aux + fechita.Chars(j)
        Next
        'Aux = Aux + fechita.Chars(11)
        fechita = Aux

        Return fechita
    End Function
    'Public Function horaFormato() As String
    '    Dim fechita As String
    '    Dim Aux As String = ""
    '    Dim Aux2 As String = ""
    '    Dim FechaHoy As String = Now().ToString()
    '    Dim hora As Boolean = False

    '    For j As Integer = 0 To FechaHoy.Length() - 1
    '        If (hora = False) Then
    '            If FechaHoy.Chars(j) = " " Then
    '                hora = True
    '            End If
    '        Else
    '            If FechaHoy.Chars(j) = ":" Then
    '                Aux = Aux + Integer.Parse(Aux2).ToString("00") + ":"
    '                Aux2 = ""
    '            Else
    '                If FechaHoy.Chars(j) <> "." Then
    '                    Aux2 = Aux2 + FechaHoy.Chars(j)
    '                End If

    '            End If

    '        End If

    '    Next
    '    Aux = Aux + Aux2

    '    fechita = Aux

    '    Return fechita
    'End Function
    Public Function fechaFormato() As String
        '   Dim fechita As Date = Date.Now()
        Dim fechita2 As String
        'fechita = fechita.ToString("yyyy/MM/dd")

        fechita2 = Date.Now.Year.ToString() + "/" + Integer.Parse(Date.Now.Month.ToString).ToString("00") + "/" + Integer.Parse(Date.Now.Day.ToString).ToString("00")
        Return fechita2
    End Function
    'Metodo de sacar el Total del descuento
    Public Function TotalPorcentaje(ByVal total As Double, ByVal porcentaje As Integer) As Double
        ' Dim TotalFull As Double = 0
        Dim desc As Double = 0
        desc = (total * porcentaje) / 100
        ' TotalFull = total - desc
        Return desc 'devuelve el descuento solamente
    End Function

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        If Aduana = 1 Then
            Dim F As New frmInventarioAduana(IdDetalle, 0, 0, 0, Math.Round(CDbl(TextBox5.Text) / Contenido, 4), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
            If F.HayViejaAduana(IdDetalle) = False Then
                F.ShowDialog()
                F.Dispose()
            Else
                Dim Fa As New frmVentasAduana(IdDetalle)
                Fa.ShowDialog()
                Fa.Dispose()
            End If
        End If
        'If IdDetalle <> 0 Then
        '    Dim F As New frmVentasAduana(IdDetalle)
        '    F.ShowDialog()
        '    F.Dispose()
        'Else
        '    MsgBox("Debe seleccionar un concepto.", MsgBoxStyle.Information, GlobalNombreApp)
        'End If
    End Sub

    Private Sub TextBox8_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.Leave

    End Sub

    Private Sub txtIEPS_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIEPS.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtIVARetenido.Focus()
        End If
    End Sub

    Private Sub txtIEPS_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIEPS.Leave
        If txtIEPS.Text = "" Then
            txtIEPS.Text = "0"
        End If
    End Sub

    Private Sub txtIVARetenido_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIVARetenido.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub txtIVARetenido_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIVARetenido.Leave
        If txtIVARetenido.Text = "" Then
            txtIVARetenido.Text = "0"
        End If
    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        Dim en As New Encriptador
        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        Dim RutaXmlTemp As String
        Dim RutaXml As String
        Dim RutaXmlTimbrado As String
        Dim RutaXMLTimbradob As String
        Dim RutaPDF As String
        Dim MsgError As String = ""
        Dim XMLAdenda As String = ""
        Dim CadenaOriginalComplemento As String = ""

        Op.DaOpciones3(V.IdSucursal)
        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        Select Case UsaAdenda
            Case 1
                Dim frmA As New frmAddendaFemsa(0, If(IDsMonedas.Valor(ComboBox2.SelectedIndex) = 2, "MXN", "USD"), idVenta, Estado, Eselectronica, S.Email, True)
                frmA.ShowDialog()
                XMLAdenda = frmA.XMLAdenda
                frmA.Dispose()
            Case 2
                Dim frmA As New frmAddendaOxxo(idVenta, True)
                frmA.elementos = Articulos
                frmA.Serie = TextBox11.Text
                frmA.Folio = TextBox2.Text
                frmA.Doc = 0
                frmA.EsElectronica = Eselectronica
                frmA.Total = CDbl(Label14.Text)
                frmA.TipodeCambio = CDbl(TextBox10.Text)
                frmA.Moneda = If(IDsMonedas.Valor(ComboBox2.SelectedIndex) = 2, "MXN", "USD")
                frmA.ShowDialog()
                XMLAdenda = frmA.XMLResultado
                frmA.Dispose()
            Case 3
                V.DaTotal(idVenta, 2, Op._Sinnegativos, Op._CalculoAlterno)
                Dim Suc As New dbSucursales(V.IdSucursal, MySqlcon)
                Dim FrmA As New frmAddendaLey(idVenta, V.TotalDescuento, Eselectronica, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Suc.Email)
                FrmA.ShowDialog()
                XMLAdenda = FrmA.strXML
                FrmA.Dispose()
            Case 5
                'Addenda modelo
                Dim moneda As New dbMonedas(IDsMonedas2.Valor(ComboBox2.SelectedIndex), MySqlcon)
                Dim frmAm As New frmAdendaGrupoModelo(idVenta, moneda.Abreviatura, TextBox10.Text)
                frmAm.ShowDialog()
                XMLAdenda = frmAm.adendaXML
                frmAm.Dispose()
            Case 6
                Dim frmA As New frmComplementoINE(idVenta)
                frmA.ShowDialog()
                XMLAdenda = frmA.XML
                CadenaOriginalComplemento = frmA.cadenaOriginal
                frmA.Dispose()
        End Select



        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            Cadena = V.CreaCadenaOriginali32(idVenta, GlobalIdMoneda, CadenaOriginalComplemento, Op.FacturaComoegreso)
        Else
            Cadena = V.CreaCadenaOriginali(idVenta, GlobalIdMoneda)
        End If
        'en.GuardaArchivoTexto(Application.StartupPath + "\cadena.txt", Cadena, System.Text.Encoding.UTF8)
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
        RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
        Archivos.CierraDB()
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        'RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDIb" + V.Serie + V.Folio.ToString + ".xml"
        'RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + "_TIMBRADO.xml"
        'RutaXMLTimbradob = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + ".xml"
        'RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXMLTimbradob = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"

        RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        Dim strXML As String
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            If CadenaOriginalComplemento <> "" Then
                strXML = V.CreaXMLi32(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa, XMLAdenda, Op.FacturaComoegreso)
            Else
                strXML = V.CreaXMLi32(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa, "", Op.FacturaComoegreso)
            End If
        Else
            strXML = V.CreaXMLi(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        End If

        'Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        V.NoCertificadoSAT = ""
        V.DaDatosTimbrado(idVenta)
        If GlobalPacCFDI = 2 Then
            en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
            Dim Timbre As String
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, True)
            Timbre = V.Recuperar(S.RFC, Op._ApiKey, V.Serie, V.Folio, True)
            If UCase(Timbre.Substring(0, 5)) <> "ERROR" Then
                Dim xmldoc As New Xml.XmlDocument
                en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)
                xmldoc.Load(RutaXmlTimbrado)
                If V.uuid = "**No Timbrado**" Then
                    V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                    V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                    V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                    V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                    V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value
                    V.GuardaDatosTimbrado(idVenta, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                End If
                'If pXMLAdenda <> "" Then
                '    Timbre = Timbre.Insert(Timbre.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                'End If
                'If V.Cliente.UsaAdenda = 4 Then
                '    Dim frmA As New frmAdendaPEPSICO(idVenta)
                '    frmA.ShowDialog()
                '    pXMLAdenda = frmA.strXML
                '    Timbre = Timbre.Insert(Timbre.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                '    frmA.Dispose()
                'End If
                en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)

            Else
                MsgError = Timbre
                V.NoCertificadoSAT = "Error"
            End If
        End If


        If V.NoCertificadoSAT <> "Error" Then
            If Estado = Estados.Cancelada Then
                V.ModificaEstado(idVenta, Estados.Cancelada)
            Else
                V.ModificaEstado(idVenta, Estados.Guardada)
            End If
            CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
            Imprimir(idVenta)
        Else
            MsgBox("Ha ocurrido un error en el timbrado del la factura, intente mas tarde." + vbCrLf + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            AddErrorTimbrado(Replace(MsgError, "'", "''"), "Ventas Recuperando", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm"), idVenta)
            'If MsgBox("¿Guardar factura como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            '    V.ModificaEstado(idVenta, Estados.Pendiente)
            '    Nuevo()
            'Else
            '    Dim Se As New dbInventarioSeries(MySqlcon)
            '    Se.QuitaSeriesAVenta(idVenta)
            '    If V.Estado = Estados.Guardada Then V.RegresaInventario(idVenta)
            '    V.Eliminar(idVenta)
            '    PopUp("Factura Eliminada", 90)
            '    Nuevo()
            'End If
            'Error en timbrado
        End If
    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        If PorLotes = 1 Then
            Dim F As New frmInventarioLotes(IdDetalle, 0, 0, 0, Math.Round(CDbl(TextBox5.Text) / Contenido, 4), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
        Try
            'If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim V As New dbVentas(MySqlcon)
            V.ActualizaDescuento(idVenta, CDbl(TextBox16.Text), CDbl(TextBox19.Text), CDbl(TextBox18.Text))
            SacaTotal()
            PopUp("Guardado", 90)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Label26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label26.Click

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged

    End Sub
    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click
        Dim Nota As New frmNotariosPublicos(idVenta)
        Nota.ShowDialog()
        Nota.Dispose()

    End Sub
    Private Sub imprimirNotarial()
        Dim I As New dbNotariosPublicos(MySqlcon)
        Dim Id As Integer
        Id = I.HayDatosNotarios(idVenta)
        If Id <> 0 Then
            If MsgBox("¿Imprimir información notarial?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                I.LlenaDatos(Id)
                Dim ventas As New dbVentas(idVenta, MySqlcon, "")
                ventas.DaDatosTimbrado(idVenta)
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repNotariosPublicos
                Rep.Subreports.Item("repInmuebles").SetDataSource(I.seleccionarInmueble(Id))
                Rep.Subreports.Item("repEnajenante").SetDataSource(I.dtImpresionEnajenante)
                Rep.Subreports.Item("repAdquiriente").SetDataSource(I.dtimpresionAdquiriente)

                Rep.SetParameterValue("serie", ventas.Serie)
                Rep.SetParameterValue("folio", ventas.Folio)
                Rep.SetParameterValue("folioFiscal", ventas.uuid)
                Rep.SetParameterValue("nombreNotario", I.NombreNotario)
                Rep.SetParameterValue("noNotaria", I.NumNotaria)
                Rep.SetParameterValue("CURPNotario", I.CURP)
                Rep.SetParameterValue("entidadFNotario", Integer.Parse(I.EntidadFederativa).ToString("00") + " - " + I.encontrarEstado(I.EntidadFederativa))
                Rep.SetParameterValue("noInstrumento", I.NumInstrumentoNotarial)
                Rep.SetParameterValue("montoOperacion", I.MontoOperacion.ToString("C2"))
                Rep.SetParameterValue("fechaFirma", I.FechaInstNotarial)
                Rep.SetParameterValue("subTotal", I.Subtotal.ToString("C2"))
                Rep.SetParameterValue("IVA", I.IVA.ToString("C2"))
                'rpt.Subreports.Item("nombredelreporte.rpt").SetDataSource("SELECT * FROM Tabla")
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            End If
        End If

    End Sub


    Private Sub frmVentasN_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Panel4.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub



    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Dim F As New frmVentasDaImporteIL(idVenta)
        F.ShowDialog()
        F.Dispose()
        SacaTotal()
    End Sub

    Private Sub ComboBox7_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox7.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox12.Focus()
        End If
    End Sub

    Private Sub txtIEPS_TextChanged(sender As Object, e As EventArgs) Handles txtIEPS.TextChanged

    End Sub

    Private Sub txtIVARetenido_TextChanged(sender As Object, e As EventArgs) Handles txtIVARetenido.TextChanged

    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlmacen.SelectedIndexChanged
        If IdInventario <> 0 Then
            Dim db As New dbInventario(IdInventario, MySqlcon)
            cmbUbicacion.DataSource = db.Ubicaciones(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), IdInventario)
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Lectura = SerialPort1.ReadExisting
        If Lectura <> "" And Veces <= 200 Then
            If Estado <> 3 Or Estado <> 4 Then TextBox5.Text = Replace(Replace(Replace(Replace(Lectura.Trim, "+", ""), "oz", ""), "lb", ""), "kg", "")
            Veces = 0
            Timer1.Enabled = False
            SerialPort1.Close()
        Else
            If Veces > 200 Then
                Timer1.Enabled = False
                SerialPort1.Close()
                Veces = 0
            End If
        End If
        Veces += 1
    End Sub


    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        PresionaF2(False)
    End Sub

    Private Sub GeneraPoliza()
        Try
            If Op.IntegrarContabilidad = 1 And GlobalconIntegracion Then
                Dim M As New dbContabilidadMascaras(MySqlcon)
                Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
                Dim Canceladas As Byte = 0
                Dim credito As Byte
                Dim cuantas As Integer
                If V.Estado = Estados.Cancelada Then
                    Canceladas = 1
                End If
                Dim FP As New dbFormasdePago(V.IdFormadePago, MySqlcon)
                If FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
                    credito = 0
                Else
                    credito = 1
                End If
                cuantas = M.CuantasHay(0, Canceladas, credito)
                If cuantas > 0 Then
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(0, Canceladas, credito)
                    Else
                        cuantas = M.CuantasHay(0, Canceladas, 3)
                        If cuantas = 1 Then
                            M.ID = M.DaMascaraActiva(0, Canceladas, 3)
                        Else
                            Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 0)
                            f.ShowDialog()
                            If f.DialogResult = Windows.Forms.DialogResult.OK Then
                                M.ID = f.IdMascara
                            Else
                                Exit Sub
                            End If
                        End If
                    End If
                    M.LlenaDatos()
                    Dim GP As dbContabilidadGeneraPolizas
                    If Canceladas = 0 Then
                        GP = New dbContabilidadGeneraPolizas(M, V.Fecha, V.Fecha, V.Fecha)
                    Else
                        GP = New dbContabilidadGeneraPolizas(M, V.FechaCancelado, V.FechaCancelado, V.FechaCancelado)
                    End If
                    GP.GeneraPolizaGeneral(V.ID, V.IdCliente, 0, 0, 0, 0, 0)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = True Then
                        If GP.Exito Then
                            Dim frmp As New frmContabilidadPolizasN(GP.IdPoliza)
                            frmp.ShowDialog()
                            frmp.Dispose()
                        Else
                            MsgBox("No se generó la póliza", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    Else
                        PopUp("Póliza Generada", 90)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click

        If idCliente <> 0 Then
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim archivo = OpenFileDialog1.FileName
                Dim ConAdo As Data.OleDb.OleDbConnection
                Dim ComAdo As New Data.OleDb.OleDbCommand
                If IO.Path.GetExtension(archivo).ToLower = ".xlsx" Then
                    ConAdo = New Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""" + archivo + """;Extended Properties=""Excel 12.0;HDR=YES;IMEX=1"";")
                ElseIf IO.Path.GetExtension(archivo).ToLower = ".xls" Then
                    ConAdo = New Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=""" + archivo + """;Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";")
                Else
                    Throw New Exception("Tipo de archivo no válido.")
                End If
                ConAdo.Open()
                ComAdo.Connection = ConAdo
                ' ConMyslq = conexion
                'Dim excel As New crearFacturaExel(archivo, MySqlcon)
                ComAdo.CommandText = "select * from [Detalles$]"
                Dim dr As Data.OleDb.OleDbDataReader = ComAdo.ExecuteReader()
                Dim mensajeError = "Los siguientes conceptos no se puede agregar debido a que contienen errores:" + vbCrLf
                Dim hayError As Boolean = False
                While dr.Read()
                    Try
                        Dim s As String = dr("concepto")
                        If s.Length > 2000 Then
                            Dim i As Integer = s.Length / 2000
                            If i Mod 2000 <> 0 Then
                                i += 1
                            End If
                            Dim aux As Integer = 0
                            Dim s1 As String = ""
                            For x As Integer = 0 To i - 1
                                Dim lon As Integer = 2000
                                If aux = 0 Then
                                    s1 = s.Substring(aux, lon)
                                    aux += 2000
                                Else
                                    If (s.Length - aux) >= 2000 Then
                                        ' aux += 2000
                                        If (s.Length - aux) < 2000 Then
                                            lon = s.Length - aux
                                        Else
                                            s1 = s.Substring(aux, lon)
                                            aux += 2000
                                        End If
                                    Else
                                        lon = s.Length - aux
                                        s1 = s.Substring(aux, lon)
                                        'aux += s.Length - aux
                                    End If
                                End If
                                TextBox3.Text = dr("clave")
                                ' s1 = s.Substring(aux, lon)
                                TextBox5.Text = CDbl(dr("cantidad"))
                                TextBox12.Text = CDbl(dr("unitario"))
                                TextBox6.Text = CDbl(dr("importe"))

                                TextBox4.Text = s1
                                BotonAgregar()
                            Next
                            's = s.Substring(0, 2000)
                        Else
                            TextBox3.Text = dr("clave")
                            TextBox5.Text = CDbl(dr("cantidad"))
                            TextBox12.Text = CDbl(dr("unitario"))
                            TextBox6.Text = CDbl(dr("importe"))
                            TextBox4.Text = s
                            BotonAgregar()
                        End If

                    Catch ex As Exception
                        hayError = True
                        mensajeError += dr("clave") + ": " + ex.Message + " " + vbCrLf
                    End Try
                End While
                dr.Close()
                ConAdo.Close()
                ConAdo.Dispose()
                If hayError Then
                    MsgBox(mensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            End If
        Else
            MsgBox("Debe indicar un cliente", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Dim RutaXmlTemp As String
        Dim RutaXml As String
        Dim RutaXmlTimbrado As String
        Dim RutaXMLTimbradob As String
        Dim RutaPDF As String
        Dim Archivos As New dbSucursalesArchivos
        FacturaGlobal = New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        FacturaGlobal.Alterno = Op._CalculoAlterno
        Archivos.DaRutaCER(FacturaGlobal.IdSucursal, GlobalIdEmpresa, False)
        RutaXml = Archivos.DaRutaArchivos(FacturaGlobal.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
        RutaPDF = Archivos.DaRutaArchivos(FacturaGlobal.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
        Archivos.CierraDB()
        'Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(FacturaGlobal.Fecha), "yyyy"))
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(FacturaGlobal.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(FacturaGlobal.Fecha), "yyyy") + "\" + Format(CDate(FacturaGlobal.Fecha), "MM") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(FacturaGlobal.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(FacturaGlobal.Fecha), "yyyy") + "\" + Format(CDate(FacturaGlobal.Fecha), "MM") + "\")
        RutaXmlTemp = RutaXml + "\" + Format(CDate(FacturaGlobal.Fecha), "yyyy") + "\" + Format(CDate(FacturaGlobal.Fecha), "MM") + "\PSSFACTURA-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString + ".xml"
        RutaXmlTimbrado = RutaXml + "\" + Format(CDate(FacturaGlobal.Fecha), "yyyy") + "\" + Format(CDate(FacturaGlobal.Fecha), "MM") + "\PSSFACTURA-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString + ".xml"
        RutaXMLTimbradob = RutaXml + "\" + Format(CDate(FacturaGlobal.Fecha), "yyyy") + "\" + Format(CDate(FacturaGlobal.Fecha), "MM") + "\PSSFACTURA-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString + ".xml"
        RutaXml = RutaXml + "\" + Format(CDate(FacturaGlobal.Fecha), "yyyy") + "\" + Format(CDate(FacturaGlobal.Fecha), "MM") + "\PSSFACTURA-" + FacturaGlobal.Serie + FacturaGlobal.Folio.ToString + ".xml"
        If Op._NoRutas = "0" Then
            RutaPDF = RutaPDF + "\" + Format(CDate(FacturaGlobal.Fecha), "yyyy") + "\" + Format(CDate(FacturaGlobal.Fecha), "MM")
        End If
        EnviarCorreo(Estado, RutaPDF, RutaXml, RutaXmlTimbrado, RutaXMLTimbradob)
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Try
            Dim En As New Encriptador()
            Dim XmlAcuse As String
            Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
            V.DaDatosTimbrado(idVenta)
            V.DaTotal(idVenta, V.IdConversion, Op._Sinnegativos, Op._CalculoAlterno)

            Dim RutaXml As String
            Dim ImpOp As Boolean = False
            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
            Archivos.CierraDB()
            'Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(FacturaGlobal.Fecha), "yyyy"))
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSACUSECANFAC-" + V.Serie + V.Folio.ToString + ".xml"
            If IO.File.Exists(RutaXml) = False Then
                Dim Suc As New dbSucursales(V.IdSucursal, MySqlcon)
                XmlAcuse = AcuseCancelacion(V.uuid, Op._ApiKey, Suc.RFC, "Factura", idVenta)
                If XmlAcuse.ToUpper.Contains("ERROR") = False Then
                    If XmlAcuse.ToUpper.Contains("NOENCONTRADA") = True Then
                        MsgBox("El SAT aun no ha notificado al pac de este acuse de cancelación, vuelva a intentarlo mas tarde.", MsgBoxStyle.Information, GlobalNombreApp)
                    Else
                        En.GuardaArchivoTexto(RutaXml, XmlAcuse, System.Text.Encoding.UTF8)
                        ImpOp = True
                        PopUp("Acuse Obtenido", 70)
                    End If
                Else
                    MsgBox("A ocurrido un error al tratar de obtener el acuse de cancelación. " + XmlAcuse, MsgBoxStyle.Information, GlobalNombreApp)
                    AddErrorTimbrado(Replace(XmlAcuse, "'", "''"), "Ventas - Acuse", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idVenta)
                End If
            Else
                ImpOp = True
            End If
            Try
                'Dim S As String
                'Generar reporte de acuse
                If ImpOp = True Then
                    Dim xmldoc As New Xml.XmlDocument
                    xmldoc.Load(RutaXml)
                    ''S = xmldoc.Item("Acuse").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                    'S = xmldoc.Item("Acuse").Attributes("Fecha").Value
                    'S = xmldoc.Item("Acuse").Attributes("RfcEmisor").Value
                    'S = xmldoc.Item("Acuse").Item("Folios").Item("UUID").InnerText
                    'S = xmldoc.Item("Acuse").Item("Signature").Item("SignatureValue").InnerText
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                    Rep = New repAcuseCancelacion
                    Rep.SetParameterValue("empresa", suc.Nombre)
                    Rep.SetParameterValue("rfcemisor", suc.RFC)
                    Rep.SetParameterValue("rfccliente", V.Cliente.RFC)
                    Rep.SetParameterValue("Documento", "FACTURA")
                    Rep.SetParameterValue("folio", V.Serie + V.Folio.ToString("00000"))
                    Rep.SetParameterValue("fechac", xmldoc.Item("Acuse").Attributes("Fecha").Value)
                    Rep.SetParameterValue("uuid", xmldoc.Item("Acuse").Item("Folios").Item("UUID").InnerText)
                    Rep.SetParameterValue("monto", V.TotalaPagar.ToString("$#,###,###,##0.00"))
                    Rep.SetParameterValue("sello", xmldoc.Item("Acuse").Item("Signature").Item("SignatureValue").InnerText)
                    Rep.SetParameterValue("nombrecliente", V.Cliente.Nombre)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                End If
            Catch ex As Exception
                AddError(Replace(ex.Message, "'", "''"), "Ventas - Acuse Impresión", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idVenta)
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        Catch ex As Exception
            AddErrorTimbrado(Replace(ex.Message, "'", "''"), "Ventas - Acuse", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idVenta)
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked And ConsultaOn Then
            If IdVentaOrigen <> 0 And Funcion = 1 Then
                LlenaCombos("tblformasdepago", ComboBox4, "concat(if(clavesat<1000,lpad(convert(clavesat using utf8),2,'0'),''),' ',nombre)", "nombret", "idforma", idsFormasDePago, "tipo=2", , "idforma")
            Else
                LlenaCombos("tblformasdepago", ComboBox4, "concat(if(clavesat<1000,lpad(convert(clavesat using utf8),2,'0'),''),' ',nombre)", "nombret", "idforma", idsFormasDePago, "tipo=1", , "idforma")
            End If


        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked And ConsultaOn Then
            LlenaCombos("tblformasdepago", ComboBox4, "concat(if(clavesat<1000,lpad(convert(clavesat using utf8),2,'0'),''),' ',nombre)", "nombret", "idforma", idsFormasDePago, "tipo=0", , "idforma")
        End If
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        If TotalVenta > 0 Then
            If RadioButton1.Checked Then
                Dim fmp As New frmVentasSelectorMetodosPago(0, idVenta, TotalVenta, 1, False)
                fmp.ShowDialog()
                fmp.Dispose()
            Else
                Dim fmp As New frmVentasSelectorMetodosPago(0, idVenta, TotalVenta, 0, False)
                fmp.ShowDialog()
                fmp.Dispose()
            End If
            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            DR = MetodosDePago.ConsultaReader(0, idVenta)
            If DR.Read() Then
                ComboBox4.SelectedIndex = idsFormasDePago.Busca(DR("idforma"))
            End If
            DR.Close()
            Button37.Visible = True
            RadioButton1.Enabled = False
            RadioButton2.Enabled = False
            ComboBox4.Enabled = False
        Else
            MsgBox("La venta debe ser mayor a cero.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        If MsgBox("¿Remover los métodos de pago agregados?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            MetodosDePago.RemoverTodo(0, idVenta)
            RadioButton1.Enabled = True
            RadioButton2.Enabled = True
            ComboBox4.Enabled = True
            Button37.Visible = False
            PopUp("Métodos removidos", 60)
        End If

    End Sub


    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        If idVenta > 0 Then
            Dim frmK As New FrmDocKardex(idVenta, 0, TextBox11.Text + TextBox2.Text, TextBox1.Text)
            frmK.ShowDialog()
            frmK.Dispose()
        End If
        'OpenFileDialog1.Filter = ""
        'If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '    Dim xmldoc As New Xml.XmlDocument
        '    Dim str As String
        '    Dim archivo = OpenFileDialog1.FileName
        '    xmldoc.Load(archivo)
        '    str = "UUID: " + xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value + vbCrLf
        '    str += "SELLOCDF: " + xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("SelloCFD").Value + vbCrLf
        '    str += "NoCER: " + xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("NoCertificadoSAT").Value + vbCrLf
        '    str += "FECHA: " + xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value + vbCrLf
        '    str += "SAT: " + xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("SelloSAT").Value
        '    MsgBox(str)
        'End If



    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub TextBox17_TextChanged(sender As Object, e As EventArgs) Handles TextBox17.TextChanged
        If Descontando Then
            Descontando = False
            If IsNumeric(TextBox17.Text) And IsNumeric(TextBox6.Text) Then
                If CDbl(TextBox6.Text) <> 0 Then TextBox9.Text = CDbl(TextBox17.Text) * 100 / CDbl(TextBox6.Text)
            Else
                TextBox9.Text = "0"
            End If
            Descontando = True
        End If
    End Sub

    Private Sub TextBox20_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox20.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Op._TipoSelAlmacen <> "0" Then
                'If ComboBox8.SelectedIndex <= 0 Then
                cmbAlmacen.Focus()
                'End If
            Else
                If Op._CursorVentas = "0" Then
                    TextBox5.Focus()
                Else
                    TextBox3.Focus()
                End If
            End If
        End If
    End Sub



    Private Sub cmbUbicacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUbicacion.SelectedIndexChanged
        If cmbUbicacion.SelectedIndex = -1 Then
            txtTarima.Text = ""
        Else
            Dim db As New dbAlmacenes(MySqlcon)
            txtTarima.Text = db.Tarima(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbUbicacion.SelectedValue)
        End If
    End Sub
End Class