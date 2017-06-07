Public Class frmVentasRemisiones
    Dim IdsVariantes As New elemento
    Dim idRemision As Integer
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim idCliente As Integer
    Dim IdInventario As Integer
    Dim IdDetalle As Integer
    Dim IdsAlmacenes As New elemento
    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim ManejaSeries As Byte
    Dim IdAlmacen As Integer
    Dim Estado As Byte
    ' Dim FolioAnt As Integer
    Dim IdVariante As Integer
    Dim IdServicio As Integer
    Dim PrecioU As Double
    Dim Tabla As New DataTable
    Dim IdsSucursales As New elemento
    Dim SerieAnt As String
    Dim PrecioBase As Double
    Dim PrecioNeto As Byte
    Dim CostoArticulo As Double
    Dim IdLista As Integer
    Dim Sobre As Byte
    Dim SIVA As Double
    Dim IdsConceptos As New elemento
    Dim IdsVendedores As New elemento
    Dim Usaformula As Byte
    Dim Op As dbOpciones
    Dim Opc As dbOpcionesOc
    Dim SinCredito As Boolean
    Dim Saldo As Double
    Dim CreditoCliente As Double
    Dim SaldoaFavor As Double
    Dim Esamortizacion As Byte
    Dim idVenta As Integer
    Dim EsKit As Byte
    Dim SepararKit As Byte
    Dim P As New dbDescuentos(MySqlcon)
    Dim CD As New dbVentasRemisionesInventario(MySqlcon)
    Dim promocion1 As Integer
    Dim promocion2 As Integer
    Dim nombreProducto As String
    Dim cantAntModificar As Double
    Dim tipoElimianr As String
    Dim PorLotes As Byte
    Dim Aduana As Byte
    Dim IdCajaDefault As Integer
    Dim Veces As Integer = 0
    Dim Secuencia As String
    Dim Lectura As String = ""

    Dim CantidadMostrar As Double
    Dim TipoCantidadMostrar As Integer
    Dim TipoCantidad As Integer
    Dim SinConcersion As Boolean
    Dim IdsCajas As New elemento
    Dim ArtNombre As String
    Dim MetodosDePago As dbVentasAddMetodos
    Dim TotalVenta As Double
    Dim ImpDoc As ImprimirDocumento
    Dim IdVendedorU As Integer
    Dim Almacen As dbAlmacenes
    Dim Descontando As Boolean = True
    Public Sub New(Optional ByVal pidVenta As Integer = 0)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idRemision = pidVenta
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta remisión no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesARemision(idRemision)
                Dim c As New dbVentasRemisiones(MySqlcon)
                c.RegresaInventario(idRemision)
                c.Eliminar(idRemision)
                e.Cancel = False
                P.limpiarDescPromociones()
                P.limpiarVentasdesc()
            Else
                GlobalEstadoVentanas = GlobalEstadoVentanas And Not 8
                e.Cancel = True
            End If
        Else
            GlobalEstadoVentanas = GlobalEstadoVentanas And Not 8
        End If
        My.Settings.remisioneschecafolio = CheckBox3.Checked
        My.Settings.Save()
    End Sub

    Private Sub frmVentasRemisiones_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F12 Then
            If idCliente <> 0 Then
                Dim Cl As New frmClientesConsultaArticulos(idCliente, IdInventario)
                Cl.ShowDialog()
                Cl.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            If Estado = Estados.SinGuardar Then
                If MsgBox("Esta remisión no ha sido guardada. ¿Desea iniciar una nueva remisión? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim S As New dbInventarioSeries(MySqlcon)
                    S.QuitaSeriesARemision(idRemision)
                    Dim C As New dbVentasRemisiones(MySqlcon)
                    C.RegresaInventario(idRemision)
                    C.Eliminar(idRemision)
                    P.limpiarDescPromociones()
                    P.limpiarVentasdesc()
                    Nuevo()
                End If
            Else
                Nuevo()
            End If
        End If
        If e.KeyCode = Keys.F10 Then
            If Button14.Enabled Then Modificar(Estados.Guardada)
        End If
        If e.KeyCode = Keys.F9 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirPendientesRemisiones, PermisosN.Secciones.Ventas) = True Then
                If Button1.Enabled Then Modificar(Estados.Pendiente)
            End If
        End If
        If e.KeyCode = Keys.F6 And IdInventario <> 0 And Estado <= 2 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.Consultas, PermisosN.Secciones.Ventas) = True Then
                Dim f As New frmInventarioConsulta(IdInventario, 1, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    ComboBox8.SelectedIndex = IdsAlmacenes.Busca(f.IdAlmacen)
                End If
                f.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F6 And Estado >= 3 Then
            If RadioButton1.Checked Then
                Dim fmp As New frmVentasSelectorMetodosPago(1, idRemision, TotalVenta, 1, True)
                fmp.ShowDialog()
                fmp.Dispose()
            Else
                Dim fmp As New frmVentasSelectorMetodosPago(1, idRemision, TotalVenta, 0, True)
                fmp.ShowDialog()
                fmp.Dispose()
            End If
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
        If e.KeyCode = Keys.F7 And IdInventario <> 0 And EsKit <> 0 And IdDetalle <> 0 Then
            Dim IDe As New frmInventarioDetalles(IdInventario, 2, IdDetalle, idRemision)
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
        Catch ex As Exception

        End Try
        ConsultaOn = False
        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        CheckBox3.Checked = My.Settings.remisioneschecafolio
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
        Op = New dbOpciones(MySqlcon)
        Opc = New dbOpcionesOc(MySqlcon)
        ImpDoc = New ImprimirDocumento
        MetodosDePago = New dbVentasAddMetodos(MySqlcon)
        Dim U As New dbUsuarios(GlobalIdUsuario, MySqlcon)
        Almacen = New dbAlmacenes(MySqlcon)
        Almacen.AlmacenesSinPermiso(GlobalIdUsuario)
        IdVendedorU = U.IdVendedor
        ConsultaOn = True
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        ConsultaOn = False
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")

        'If Op.NoPermitirRemisionesdeCredito = 0 Then
        '    LlenaCombos("tblformasdepagoremisiones", ComboBox4, "concat(convert(if(tipo=1,'CONTADO',if(tipo=2,'CONTADO','CRÉDITO')) using utf8),'-',nombre)", "nombrec", "idforma", IdsConceptos, , , "idforma")
        'Else
        '    LlenaCombos("tblformasdepagoremisiones", ComboBox4, "concat(convert(if(tipo=1,'CONTADO',if(tipo=2,'CONTADO','CRÉDITO')) using utf8),'-',nombre)", "nombrec", "idforma", IdsConceptos, "tipo=1 or tipo=2", , "idforma")
        'End If
        LlenaCombos("tblvendedores", ComboBox5, "nombre", "nombret", "idvendedor", IdsVendedores)
        
        ConsultaOn = True
        Opc.LlenaDatos(0, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodePrecio, PermisosN.Secciones.Ventas) = False Then
            TextBox12.ReadOnly = True
        End If
        CheckScroll.Checked = My.Settings.ventasscroll
        Try
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            IdCajaDefault = sa.idCaja
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
        If idRemision = 0 Then
            Nuevo()
        Else
            LlenaDatosVenta()
            NuevoConcepto()
        End If
        Panel3.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim T As Double
                'Dim Iva As Double
                Dim V As New dbVentasRemisiones(MySqlcon)
                T = V.DaTotal(idRemision, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                'Dim O As New dbOpciones(MySqlcon)
                'Iva = V.TotalIva
                Label12.Text = Format(V.Subtototal, "#,##0.00")
                Label13.Text = Format(V.TotalIva, "#,##0.00")
                Label14.Text = Format(V.TotalVenta, "#,##0.00")
                TotalVenta = V.TotalVenta
                Label32.Text = Format(V.TotalPeso, "#,##0.00") + "Kg."
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        DateTimePicker1.Value = Date.Now
        TextBox1.Text = ""
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        'FolioAnt = 0
        PrecioNeto = 0
        idRemision = 0
        Label3.Visible = False
        Label39.Visible = False
        idVenta = 0
        SaldoaFavor = 0
        Button22.Visible = False
        Label32.Text = "0.00Kg."
        ConsultaOn = False
        If Op.SiemprePorSurtirRemisiones = 1 Then
            CheckBox2.Checked = True
        Else
            CheckBox2.Checked = False
        End If
        ConsultaOn = True
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False
        Label29.Visible = False
        ComboBox3.Enabled = True
        ComboBox5.Enabled = True
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        Button11.Enabled = True
        CheckBox2.Enabled = True
        RadioButton1.Enabled = True
        ComboBox4.Enabled = True
        If Op.NoPermitirRemisionesdeCredito = 0 And GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirRemisionesCredito, PermisosN.Secciones.Ventas) = True Then
            RadioButton2.Enabled = True
        Else
            RadioButton2.Enabled = False
        End If
        If Op.RemisionesSoloaCredito = 1 Then
            RadioButton2.Enabled = True
            RadioButton2.Checked = True
            RadioButton1.Enabled = False
        Else
            RadioButton1.Checked = True
        End If
        Button37.Visible = False
        TotalVenta = 0
        If GlobaltpBanxico <> "Error" Then
            TextBox10.Text = GlobaltpBanxico
        Else
            Dim CM As New dbMonedasConversiones(1, MySqlcon)
            TextBox10.Text = CM.Cantidad.ToString
        End If
        'Dim o As New dbOpciones(MySqlcon)
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        If Op._TipoSelAlmacen = "0" Then
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            If ComboBox8.Items.Count = 0 Then
                MsgBox("Esta sucursal no tiene almacenes, no podrá hacer remisiones si no se registra uno.", MsgBoxStyle.Information, GlobalNombreApp)
            Else
                ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
            End If
        Else
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. Almacen")
            ComboBox8.SelectedIndex = 0
        End If
        TextBox8.Text = S.Impuesto.ToString
        LlenaCombos("tblcajas", ComboBox6, "nombre", "nombrem", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        'TextBox11.Text = S.Serie

        'RutaImagen = sa.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, 249, True)
        If ComboBox6.Items.Count > 0 Then
            ComboBox6.SelectedIndex = IdsCajas.Busca(IdCajaDefault)
        Else
            MsgBox("Esta sucursal no tiene cajas dadas de alta, no podrá hacer remisiones si no se registra una.", MsgBoxStyle.Information, GlobalNombreApp)
        End If

        Dim Sf As New dbSucursalesFolios(MySqlcon)
        If CheckBox2.Checked = False Then
            Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Remision, 0)
        Else
            Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.RemisionesPorSurtir, 0)
        End If
        TextBox11.Text = Sf.Serie
        'If Opc.ActivarOc = 1 And Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc Then
        If Opc.ActivarOc = 1 And ((Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc) Or (Format(Now, "HH:mm") >= Opc.HoraInicioOc2 And Format(Now, "HH:mm") <= Opc.HoraFinOc2)) Then
            TextBox11.Text = Opc.SerieOc
        End If
        Dim V As New dbVentasRemisiones(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If Opc.ActivarOc = 1 And ((Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc) Or (Format(Now, "HH:mm") >= Opc.HoraInicioOc2 And Format(Now, "HH:mm") <= Opc.HoraFinOc2)) Then
            If CInt(TextBox2.Text) < Opc.FolioOc Then TextBox2.Text = Opc.FolioOc.ToString
        Else
            If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        End If
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"

        ComboBox4.SelectedIndex = 0
        ComboBox5.SelectedIndex = 0
        CheckBox1.Checked = False
        Panel1.Enabled = True
        ComboBox8.Enabled = True
        Panel2.Enabled = True
        SerieAnt = ""
        TextBox14.Text = ""
        Label24.Visible = False
        NuevoConcepto()
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
            ComboBox3.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodeFolio, PermisosN.Secciones.Ventas) = False Then
            TextBox11.Enabled = False
            TextBox2.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirCambiarFechaRemisiones, PermisosN.Secciones.Ventas) = False Then
            DateTimePicker1.Enabled = False
        End If
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Op._TipoSelAlmacen <> "0" Then
                'If ComboBox8.SelectedIndex <= 0 Then
                ComboBox8.Focus()
                'End If
            Else
                If Op._CursorVentas = "0" Then
                    TextBox5.Focus()
                Else
                    TextBox3.Focus()
                End If
            End If
        End If
        If e.KeyCode = Keys.F1 Then
            BotonCliente()
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
                        TextBox7.Text = c.RFC + " " + c.Nombre '+ vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                    Else
                        TextBox7.Text = c.RFC + " " + c.Nombre '+ vbCrLf + c.Direccion2 + " " + c.NoExterior2 + " " + c.Ciudad2 + " " + c.CP2
                    End If
                    If c.NoChecarCr = 0 Then
                        Saldo = c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
                        SaldoaFavor = CDbl(Format(c.DaSaldoAFavor(c.ID), "0.00"))
                    Else
                        Saldo = 0
                        SaldoaFavor = 0
                    End If
                    CreditoCliente = c.Credito
                    TextBox7.Text += vbCrLf + "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00") + " " + "Saldo: " + Format(Saldo, "#,##0.00") + " " + "A Favor: " + Format(SaldoaFavor, "#,##0.00")
                    TextBox7.Text += vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.Estado + " " + c.CP
                    idCliente = c.ID
                    IdLista = c.IdLista
                    SIVA = c.IVA
                    Sobre = c.SobreescribeIVA
                    If Saldo >= c.Credito Then
                        SinCredito = True
                    Else
                        SinCredito = False
                    End If
                    If c.CreditoDias <> 0 Then
                        If c.TieneCreditoporFecha(c.ID, c.CreditoDias) = False Then
                            SinCredito = True
                        End If
                    End If

                    If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                        'If (c.Credito <> 0 Or c.CreditoDias <> 0) And Op.NoPermitirRemisionesdeCredito = 0 Then
                        '    ComboBox4.SelectedIndex = 1
                        'Else
                        '    ComboBox4.SelectedIndex = 0
                        'End If
                        Dim FP As New dbFormasdePagoRemisiones(c.IdFormaF, MySqlcon)
                        If FP.Tipo = dbFormasdePagoRemisiones.Tipos.Contado Or Op.NoPermitirRemisionesdeCredito = 1 Or GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirRemisionesCredito, PermisosN.Secciones.Ventas) = False Then
                            RadioButton1.Checked = True
                        Else
                            RadioButton2.Checked = True
                        End If
                        If Op.RemisionesSoloaCredito = 1 Then
                            RadioButton2.Checked = True
                        End If
                        ComboBox4.SelectedIndex = IdsConceptos.Busca(c.IdFormaR)
                    End If
                    If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                        If IdVendedorU > 0 And Op.VendedorUsuario = 1 Then
                            ComboBox5.SelectedIndex = IdsVendedores.Busca(IdVendedorU)
                        Else
                            ComboBox5.SelectedIndex = IdsVendedores.Busca(c.IdVendedor)
                        End If
                    End If
                Else
                    TextBox7.Text = ""
                    'TextBox13.Text = ""
                    idCliente = 0
                    IdLista = 0
                    Sobre = 0
                    SIVA = 0
                    CreditoCliente = 0
                    SinCredito = False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Modificar(Estados.Pendiente)
    End Sub
    'Private Sub Modificar(ByVal pEstado As Byte)
    '    Try
    '        Dim MensajeError As String = ""
    '        Dim C As New dbVentasRemisiones(MySqlcon)
    '        Dim Desglozar As Byte
    '        If IsNumeric(TextBox2.Text) = False Then MensajeError = "El folio debe ser un valor numérico."
    '        'If FolioAnt <> TextBox2.Text Then
    '        If C.ChecaFolioRepetido(CInt(TextBox2.Text), TextBox11.Text) Then
    '            If pEstado = Estados.Guardada Then TextBox2.Text = C.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
    '            'MensajeError += " Ya existe una remisión con este folio."
    '            'TextBox2.BackColor = Color.FromArgb(250, 150, 150)
    '            'Label17.Visible = True
    '        End If
    '        'End If
    '        If MensajeError = "" Then
    '            If CheckBox1.Checked Then
    '                Desglozar = 1
    '            Else
    '                Desglozar = 0
    '            End If
    '            Dim O As New dbOpciones(MySqlcon)
    '            C.DaTotal(idRemision, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
    '            C.Modificar(idRemision, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, CDbl(TextBox8.Text), pEstado, TextBox11.Text, TextBox10.Text, IDsMonedas2.Valor(ComboBox2.SelectedIndex), C.Subtototal, C.TotalVenta, idCliente)
    '            If pEstado = Estados.Cancelada Then
    '                C.RegresaInventario(idRemision)
    '            End If
    '            Nuevo()
    '        Else
    '            MsgBox(MensajeError, MsgBoxStyle.Information, NombreApp)
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, NombreApp)
    '    End Try
    'End Sub

    Private Sub Modificar(ByVal pEstado As Byte)
        Dim MensajeError As String = ""
        Dim MensajeErrorEx As String = ""
        Try
            Estado = pEstado
            Dim C As New dbVentasRemisiones(MySqlcon)
            Dim Desglozar As Byte
            Dim FP As New dbFormasdePagoRemisiones(IdsConceptos.Valor(ComboBox4.SelectedIndex), MySqlcon)
            If C.RevisaConceptos(idRemision) = False Then
                MensajeError = "Hay conceptos en pesos y en dolares solo se pueden hacer remisiones con conceptos en un tipo de moneda."
            Else
                ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.DaMoneda(idRemision))
            End If
            If IsNumeric(TextBox2.Text) = False Then MensajeError += "El folio debe ser un valor numérico."
            'If FolioAnt <> TextBox2.Text Then
            If C.RevisaEstado(idRemision) And pEstado = Estados.Guardada Then
                MsgBox("Esta remisión ya ha sido procesada en otra computadora.", MsgBoxStyle.Information, GlobalNombreApp)
                Nuevo()
                Exit Sub
            End If
            If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) And CheckBox3.Checked = False And pEstado = Estados.Guardada Then
                MensajeError += "Folio Repetido."
            End If
            If pEstado = Estados.Cancelada And C.EstaLigada(idRemision) <> 0 Then
                MensajeError += " No se puede cancelar una remisión ya facturada necesita cancelar primero la factura."
            End If
            'If pEstado = Estados.Guardada And GlobalSoloExistencia = True Then MensajeError += C.VerificaExistencias(idRemision)
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesAlta, PermisosN.Secciones.Ventas) = False And pEstado <> Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesCancelar, PermisosN.Secciones.Ventas) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            C.DaTotal(idRemision, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
            If (Saldo + C.TotalVenta > CreditoCliente Or SinCredito) And FP.Tipo = dbFormasdePagoRemisiones.Tipos.Credito And Op._LimitarCredito = 1 Then
                If pEstado = Estados.Guardada Then
                    MensajeError = "El cliente exede de su límite de crédito."
                End If
            End If
            If SaldoaFavor - (-1 * C.TotalAmortizacion(idRemision)) < 0 And pEstado = Estados.Guardada Then
                MensajeError += " Saldo a favor insuficiente, debe indicar una cantidad menor."
            End If
            Dim TotalAgregado As Double = MetodosDePago.TotalAgregado(1, idRemision)
            If pEstado = Estados.Guardada And Math.Round(TotalAgregado, 2) <> Math.Round(TotalVenta, 2) And TotalAgregado > 0 Then
                MensajeError += " Los métodos de pago no estan agregados correctamente."
            End If
            If pEstado = Estados.Guardada And GlobalSoloExistencia = True And CheckBox2.Checked = False Then MensajeError += C.VerificaExistencias(idRemision)
            MensajeErrorEx = MensajeError
            MensajeErrorEx += "Antes de guardar"
            If MensajeError = "" Then
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
                Dim Porsurtir As Byte
                If CheckBox2.Checked Then
                    Porsurtir = 1
                Else
                    Porsurtir = 0
                End If
                Dim O As New dbOpciones(MySqlcon)
                'Dim FP As New dbFormasdePago(idsFormasDePago.Valor(ComboBox4.SelectedIndex), MySqlcon)
                Dim CM As New dbMonedasConversiones(MySqlcon)
                'CM.Modificar(1, CDbl(TextBox10.Text))
                'Dim Credito As Byte
                'Credito = FP.Tipo

                Dim Sf As New dbSucursalesFolios(MySqlcon)
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Remision, 0)
                'Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                CM.Modificar(1, CDbl(TextBox10.Text))
                C.Modificar(idRemision, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, pEstado, TextBox11.Text, CDbl(TextBox10.Text), IDsMonedas2.Valor(ComboBox2.SelectedIndex), C.Subtototal, C.TotalVenta, idCliente, IdsConceptos.Valor(ComboBox4.SelectedIndex), IdsVendedores.Valor(ComboBox5.SelectedIndex), TextBox14.Text, Porsurtir, CheckBox3.Checked, IdsCajas.Valor(ComboBox6.SelectedIndex))
                MensajeErrorEx += " Modifico"
                Dim S As New dbInventarioSeries(MySqlcon)
                Dim CajaG As New dbCajas(IdsCajas.Valor(ComboBox6.SelectedIndex), MySqlcon)
                If pEstado = Estados.Cancelada Then
                    S.QuitaSeriesARemision(idRemision)
                    C.RegresaInventario(idRemision)
                    MensajeErrorEx += "Regreso Inventario"
                    Dim TotalAgregadoEf As Double = MetodosDePago.TotalAgregadoPorTipoRemisiones(1, idRemision)
                    If TotalAgregadoEf <> 0 Then CajaG.MovimientodeCaja(IdsCajas.Valor(ComboBox6.SelectedIndex), TotalAgregadoEf * -1)
                End If

                If pEstado = Estados.Guardada Then
                    C.ModificaInventario(idRemision, 0)
                    If TotalAgregado = 0 Then
                        MetodosDePago.Guardar(1, IdsConceptos.Valor(ComboBox4.SelectedIndex), C.TotalVenta, idRemision)
                    End If
                    Dim TotalAgregadoEf As Double = MetodosDePago.TotalAgregadoPorTipoRemisiones(1, idRemision)
                    If TotalAgregadoEf <> 0 Then CajaG.MovimientodeCaja(IdsCajas.Valor(ComboBox6.SelectedIndex), TotalAgregadoEf)
                    If Op.PedirAnticipoRemisiones = 1 And RadioButton2.Checked = True Then
                        If MsgBox("¿Agregar un anticipo a la remisión?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            Dim AP As New frmVentasPagosRemisiones(TextBox11.Text + TextBox2.Text, 0, idCliente, "")
                            AP.ShowDialog()
                            AP.Dispose()
                        End If
                    End If
                    Imprimir(idRemision)
                    If S.CantidadDeSeriesAgregadasaRemision(idRemision, 0) > 0 Then
                        If MsgBox("¿Imprimir listado de series?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            ImprimirSeries()
                        End If
                    End If

                    'If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    'CadenaOriginal()
                    'End If
                End If
                'CadenaOriginal()
                Nuevo()
            Else
                MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            AddError(ex.Message, "Remisiones", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idRemision)
            MsgBox(ex.Message + MensajeErrorEx, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Guardar()
        Try

            'If Button1.Text = "Guardar" Then
            'If (PermisosVentas And CULng((Math.Pow(2, perVentas.Remisiones + 1)))) <> 0 Then
            If idCliente <> 0 Then
                Dim C As New dbVentasRemisiones(MySqlcon)
                Dim Desglozar As Byte
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If

                If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
                    TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                    Label17.Visible = True
                End If
                If IsNumeric(TextBox2.Text) = False Then
                    MsgBox("El folio debe ser un valor numérico", MsgBoxStyle.Critical, GlobalNombreApp)
                    Exit Sub
                End If
                Dim O As New dbOpciones(MySqlcon)
                ComboBox2.SelectedIndex = IDsMonedas2.Busca(IDsMonedas.Valor(ComboBox1.SelectedIndex))
                C.Guardar(idCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, CDbl(TextBox8.Text), IdsSucursales.Valor(ComboBox3.SelectedIndex), TextBox11.Text, TextBox10.Text, IDsMonedas2.Valor(ComboBox2.SelectedIndex), IdsCajas.Valor(ComboBox6.SelectedIndex))
                ComboBox3.Enabled = False
                idRemision = C.ID
                Estado = 1
                'Button1.Text = "Modificar"
                Button2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirCambiarFechaRemisiones, PermisosN.Secciones.Ventas) = False Then
                    DateTimePicker1.Enabled = False
                End If
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirPendientesRemisiones, PermisosN.Secciones.Ventas) = False Then
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
            'If e.KeyCode = Keys.Enter And IdInventario <> 0 Then
            If Usaformula = 1 And IdInventario <> 0 Then
                Dim Fo As New frmInventarioFormula01(ArtNombre)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.00")
                    TextBox4.Text = Fo.FormulaString
                End If
                'End If
            End If
            TextBox12.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            BotonArticulo()
        End If
        If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
            If Usaformula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtNombre)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.00")
                    TextBox4.Text = Fo.FormulaString
                End If
                TextBox12.Focus()
            End If
        End If
        If e.KeyCode = Keys.F2 And IdInventario <> 0 Then
            presionaF2(True)
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
                    'Dim ps As New dbProductos(MySqlcon)
                    'If ps.BuscaProducto(TextBox3.Text) Then
                    'LlenaDatosProducto(ps)
                    'Else
                    TextBox4.Text = ""
                    TextBox6.Text = "0"
                    TextBox8.Text = "0"
                    TextBox9.Text = "0"
                    PrecioU = 0
                    IdVariante = 0
                    'End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub LlenaDatosVenta()
        Dim C As New dbVentasRemisiones(idRemision, MySqlcon)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        TextBox2.Text = C.Folio
        'FolioAnt = C.Folio
        SerieAnt = C.Serie
        TextBox1.Text = C.Cliente.Clave
        TextBox11.Text = C.Serie
        TextBox14.Text = C.Comentario
        Estado = C.Estado
        Button11.Enabled = False
        TextBox8.Text = C.Iva.ToString
        If C.Desglosar = 1 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
        ConsultaOn = False
        If C.PorSurtir = 1 Then
            CheckBox2.Checked = True
        Else
            CheckBox2.Checked = False
        End If
        ConsultaOn = True
        If C.IdVentaR <> 0 Then
            Label29.Visible = True
            idVenta = C.IdVentaR
            Button22.Visible = True
            Label29.Text = C.FolioRef
        Else
            Label29.Visible = False
            Button22.Visible = False
        End If
        DateTimePicker1.Value = C.Fecha

        Dim FP As New dbFormasdePagoRemisiones(C.idForma, MySqlcon)
        If C.Estado = Estados.Guardada Or C.Estado = Estados.Cancelada Then ConsultaOn = False
        If FP.Tipo = dbFormasdePagoRemisiones.Tipos.Contado Or FP.Tipo = dbFormasdePagoRemisiones.Tipos.Otro Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If
        If C.Estado = Estados.Guardada Or C.Estado = Estados.Cancelada Then
            ConsultaOn = True
            LlenaCombos("tblformasdepagoremisiones", ComboBox4, "concat(convert(if(tipo=1,'CONTADO',if(tipo=2,'CONTADO','CRÉDITO')) using utf8),'-',nombre)", "nombrec", "idforma", IdsConceptos, , , "idforma")
        End If
        ComboBox4.SelectedIndex = IdsConceptos.Busca(C.idForma)
        ComboBox5.SelectedIndex = IdsVendedores.Busca(C.IdVEndedor)
        ComboBox6.SelectedIndex = IdsCajas.Busca(C.idCaja)
        ComboBox3.Enabled = False
        ConsultaDetalles()
        Dim KM As New dbkardexdocumentos(MySqlcon)
        If KM.MovimientosRemisionCant(idRemision) > 0 Then
            Label3.Visible = True
        Else
            Label3.Visible = False
        End If
        Label39.Text = "FECHA CANCELACIÓN: " + C.FechaCancelado
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                Button13.Enabled = False
                Panel1.Enabled = False
                ComboBox8.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
                ComboBox5.Enabled = False
                Label39.Visible = True
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Panel1.Enabled = False
                ComboBox8.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
                ComboBox5.Enabled = False
                Label39.Visible = False
            Case Else
                Label39.Visible = False
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                ComboBox8.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button2.Enabled = True
                ComboBox5.Enabled = True
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodeFolio, PermisosN.Secciones.Ventas) = False Then
                    TextBox11.Enabled = False
                    TextBox2.Enabled = False
                End If
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
                    ComboBox3.Enabled = False
                End If
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirPendientesRemisiones, PermisosN.Secciones.Ventas) = False Then
                    Button1.Enabled = False
                End If
        End Select
    End Sub
    'Private Sub LlenaDatosDetalles()
    '    Panel1.Visible = True
    '    ConsultaDetalles()
    'End Sub
    Private Sub ConsultaDetalles()
        Try
            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbVentasRemisionesInventario(MySqlcon)
            T = CD.ConsultaReader(idRemision, 0, 0, "0")
            While T.Read
                'Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("clave"), T("descripcion"), T("precio"), T("abreviatura"))
                If T("cantidad") <> 0 Then
                    If T("idinventario") > 1 Then
                        Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidadm"), T("tipom"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    Else
                        Tabla.Rows.Add(T("iddetalle"), "P", "", T("cantidadm"), T("tipom"), T("pclave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    End If
                Else
                    If T("idinventario") > 1 Then
                        Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidadm"), T("tipom"), T("clave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                    Else
                        Tabla.Rows.Add(T("iddetalle"), "P", "", T("cantidadm"), T("tipom"), T("pclave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                    End If
                End If
            End While
            T.Close()
            'Dim VP As New dbVentasRemisionesProductos(MySqlcon)
            'T = VP.ConsultaReader(idRemision)
            'While T.Read
            '    Tabla.Rows.Add(T("iddetalle"), "P", "", T("cantidad"), T("clave"), T("descripcion"), T("precio"), T("abreviatura"))
            'End While
            'T.Close()
            'Dim VS As New dbVentasRemisionesServicios(MySqlcon)
            'T = VS.ConsultaReader(idRemision)
            'While T.Read
            '    Tabla.Rows.Add(T("iddetalle"), "S", "", T("cantidad"), T("folio"), T("descripcion"), T("precio"), T("abreviatura"))
            'End While
            'T.Close()
            DGDetalles.DataSource = Tabla
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).Visible = False
            DGDetalles.Columns(2).Visible = False
            DGDetalles.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGDetalles.Columns(5).Width = 80
            DGDetalles.Columns(4).Width = 60
            DGDetalles.Columns(8).Width = 80
            DGDetalles.Columns(9).Width = 50
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
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "0"
        TextBox6.Text = "0"
        TextBox8.Text = "0"
        TextBox9.Text = "0"
        TextBox13.Text = "0"
        ArtNombre = ""
        CostoArticulo = 0
        Button24.Visible = False
        TextBox12.Text = "0"
        PrecioNeto = 0
        Esamortizacion = 0
        PrecioBase = 0
        SepararKit = 0
        Usaformula = 0
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        Button6.Enabled = True
        TextBox3.Enabled = True
        Button12.Visible = False
        Button9.Enabled = False
        PorLotes = 0
        Aduana = 0
        TipoCantidadMostrar = 0
        CantidadMostrar = 0
        SinConcersion = False
        Label20.Text = "0"
        Button23.Enabled = False
        Button28.Enabled = False
        lblUbicacion.Visible = False
        cmbUbicacion.Visible = False
        cmbUbicacion.Enabled = True

        ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Agregar Concepto"
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodeAlmacen, PermisosN.Secciones.Ventas) = False Or Estado > 2 Then
            ComboBox8.Enabled = False
        Else
            ComboBox8.Enabled = True
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
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbVentasRemisionesInventario(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            If IdInventario = 0 Then
                MsgError += "Debe indicar un artículo."
                HayError = True
            End If
            Dim R As New dbVentasRemisiones(MySqlcon)
            If R.RevisaEstado(idRemision) Then
                MsgBox("Esta remisión ya esta guardada no se puede modificar.", MsgBoxStyle.Information, GlobalNombreApp)
                Nuevo()
                Exit Sub
            End If
            Dim I As New dbInventario(IdInventario, MySqlcon)
            If IsNumeric(TextBox5.Text) Then
                If CDbl(TextBox5.Text) <= 0 Then
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
                If IsNumeric(txtIEPS.Text) = False Then
                    MsgError += vbCrLf + "El IEPS debe ser un valor numérico."
                    HayError = True
                End If
                If IsNumeric(txtIVARetenido.Text) = False Then
                    MsgError += vbCrLf + "El IVA Retenido debe ser un valor numérico."
                    HayError = True
                End If
                If IsNumeric(TextBox8.Text) = False Then
                    MsgError += vbCrLf + "El IVA  debe ser un valor numérico."
                    HayError = True
                End If
                If IsNumeric(TextBox10.Text) = False Then
                    MsgError += vbCrLf + "El tipo de cambio  debe ser un valor numérico."
                    HayError = True
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
            If Almacen.TienePermiso(IdsAlmacenes.Valor(ComboBox8.SelectedIndex)) = False Then
                HayError = True
                MsgError += vbCrLf + " No tiene permiso para realizar operaciones en el almacén seleccionado."
            End If
            If IdInventario <> 0 And GlobalSoloExistencia = True And CheckBox2.Checked = False Then
                Dim Cant As Double
                Cant = I.DaInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario)
                If Cant < CDbl(TextBox5.Text) And I.Inventariable = 1 Then
                    MsgError += " Artículo sin existencia suficiente." + vbCrLf + "Cantidad disponible: " + Cant.ToString + vbCrLf + "Cantidad solicitada: " + TextBox5.Text + vbCrLf + "Diferencia: " + CStr(Cant - CDbl(TextBox5.Text))
                    HayError = True
                End If
                If EsKit = 1 Then
                    Dim Str As String
                    Str = I.ChecaInventarioKits(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, CDbl(TextBox5.Text))
                    MsgError += Str
                    If Str <> "" Then HayError = True
                End If
            End If
            
            If HayError = False Then
                If PrecioNeto = 1 Then
                    Dim Temp As Double
                    'Temp = CStr(CDbl(TextBox6.Text) / (1 + CDbl(TextBox8.Text) / 100) / CDbl(TextBox5.Text))
                    Temp = CStr(CDbl(TextBox6.Text) / (1 + (CDbl(TextBox8.Text) + CDbl(txtIEPS.Text) - CDbl(txtIVARetenido.Text)) / 100) / CDbl(TextBox5.Text))
                    TextBox12.Text = Temp.ToString
                End If
                If CantidadMostrar = 0 Then TipoCantidadMostrar = TipoCantidad
                If CantidadMostrar = 0 Then CantidadMostrar = CDbl(TextBox5.Text)
                If SinConcersion Then CantidadMostrar = CDbl(TextBox5.Text)
                If Button4.Text = "Agregar Concepto" Then
                    If SepararKit = 0 Then
                        CD.Guardar(idRemision, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, IdServicio, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), CantidadMostrar, TipoCantidadMostrar, CDbl(TextBox13.Text), If(cmbUbicacion.Visible, cmbUbicacion.SelectedValue, ""))
                        hayDescuento()
                    Else
                        If EsKit = 1 And SepararKit = 1 Then
                            CD.SeparaKit(idRemision, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, IdServicio, CDbl(txtIEPS.Text), CDbl(txtIVARetenido.Text), CantidadMostrar, TipoCantidadMostrar, CDbl(TextBox13.Text))
                        End If
                    End If
                    If CheckBox2.Checked Then CheckBox2.Enabled = False
                    IdDetalle = CD.ID
                    If EsKit = 1 And SepararKit = 0 Then
                        Dim IKits As New dbVentasKits(MySqlcon)
                        IKits.InsertarArticulosRemision(IdInventario, idRemision, CD.ID, CDbl(TextBox5.Text), IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    End If
                    If IdInventario <> 0 Then
                        If ManejaSeries <> 0 Then
                            If CD.NuevoConcepto Then
                                Dim F As New frmVentasAsignaSeries(IdInventario, 0, idRemision, CInt(TextBox5.Text))
                                F.ShowDialog()
                            Else
                                Dim F As New frmVentasAsignaSeries(IdInventario, 0, idRemision, CD.Cantidad)
                                F.ShowDialog()
                            End If
                        End If
                        If I.KitconSerie(IdInventario) > 0 Then
                            Dim IDe As New frmInventarioDetalles(IdInventario, 2, IdDetalle, idRemision)
                            IDe.ShowDialog()
                            IDe.Dispose()
                        End If
                        If PorLotes = 1 Then
                            Dim F As New frmInventarioLotes(0, IdDetalle, 0, 0, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        If Aduana = 1 Then
                            Dim F As New frmInventarioAduana(0, IdDetalle, 0, 0, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        'Dim I As New dbInventario(MySqlcon)
                        'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), 0, dbInventario.TipoMovimiento.Baja, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    End If
                    'If IdVariante <> 0 Then
                    'Dim PV As New dbProductosVariantes(MySqlcon)
                    'PV.ModificaInventario(IdVariante, CDbl(TextBox5.Text), IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    'End If


                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo agregado", 90)
                Else

                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CD.BuscaridInventario(IdDetalle), fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex)))
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), CantidadMostrar, TipoCantidadMostrar, CDbl(TextBox13.Text))
                    'Modificar descuento
                    If tipoElimianr = "Promocion" Then
                        modificarDescuento(P.descModificar(IdDetalle, "VentasRemisiones"))
                    Else
                        If P.descModificar(IdDetalle, "VentasRemisiones") <> 0 Then
                            modificarDescuento(P.descModificar(IdDetalle, "VentasRemisiones"))
                        End If
                    End If

                    If EsKit = 1 Then
                        Dim IKits As New dbVentasKits(MySqlcon)
                        IKits.ModificaArtículosRemision(IdDetalle, CDbl(TextBox5.Text), IdInventario)
                    End If
                    If IdInventario <> 0 Then
                        If ManejaSeries <> 0 Then
                            'Dim F As New frmInventarioSeries(IdInventario, 0, idRemision, CDbl(TextBox5.Text))
                            Dim F As New frmVentasAsignaSeries(IdInventario, 0, idRemision, CDbl(TextBox5.Text))
                            F.ShowDialog()
                        End If
                        If I.KitconSerie(IdInventario) > 0 Then
                            Dim IDe As New frmInventarioDetalles(IdInventario, 2, IdDetalle, idRemision)
                            IDe.ShowDialog()
                            IDe.Dispose()
                        End If
                        If PorLotes = 1 Then
                            Dim F As New frmInventarioLotes(0, IdDetalle, 0, 0, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        If Aduana = 1 Then
                            Dim F As New frmInventarioAduana(0, IdDetalle, 0, 0, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        'Dim I As New dbInventario(MySqlcon)
                        'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), CantAnt, dbInventario.TipoMovimiento.CambioBaja, IdAlmacen)
                    End If
                    'If IdVariante <> 0 Then
                    'Dim PV As New dbProductosVariantes(MySqlcon)
                    'PV.ModificaInventario(IdVariante, CantAnt * -1, IdAlmacen)
                    'PV.ModificaInventario(IdVariante, CDbl(TextBox5.Text), IdAlmacen)
                    'End If

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
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesAlta, PermisosN.Secciones.Ventas) = True Then
            If Op._TipoSelAlmacen = "1" Then
                If ComboBox8.SelectedIndex <= 0 Then
                    MsgBox("Debe indicar un almacen.", MsgBoxStyle.Information, GlobalNombreApp)
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
        End If
    End Sub
    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try
            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbVentasRemisionesInventario(IdDetalle, MySqlcon)
            Button6.Enabled = False
            TextBox3.Enabled = False
            ConsultaOn = False
            TextBox5.Text = CD.Cantidad.ToString
            ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            ArtNombre = CD.Inventario.Nombre
            IdInventario = CD.Idinventario
            IdVariante = CD.IdVariante
            TextBox8.Text = CD.Iva.ToString
            'If CD.Idinventario > 1 Then
            TextBox3.Text = CD.Inventario.Clave
            PrecioNeto = CD.Inventario.PrecioNeto
            CostoArticulo = CD.Inventario.CostoBase
            If CD.Inventario.Contenido > 1 Then
                CostoArticulo = CostoArticulo / CD.Inventario.Contenido
            End If
            IdVariante = 0
            EsKit = CD.Inventario.EsKit
            Esamortizacion = CD.Inventario.EsAmortizacion
            PorLotes = CD.Inventario.PorLotes
            If PorLotes = 1 Then
                Button28.Enabled = True
            Else
                Button28.Enabled = False
            End If
            Aduana = CD.Inventario.Aduana
            If Aduana = 1 Then
                Button23.Enabled = True
            Else
                Button23.Enabled = False
            End If

            CantAnt = CD.Cantidad
            IdAlmacen = CD.IdAlmacen
            CantidadMostrar = CD.CantidadM
            TipoCantidadMostrar = CD.TipoCantidadM
            TipoCantidad = TipoCantidadMostrar
            Label20.Text = CD.CantidadM.ToString

            If CD.Cantidad = CD.CantidadM Then
                SinConcersion = True
            Else
                SinConcersion = False
            End If

            'PrecioU = CD.Precio / CD.Cantidad
            TextBox9.Text = CD.Descuento.ToString
            TextBox13.Text = CD.CDescuento.ToString
            txtIEPS.Text = CD.IEPS.ToString()
            txtIVARetenido.Text = CD.IVARetenido.ToString()
            ComboBox8.SelectedIndex = IdsAlmacenes.Busca(IdAlmacen)
            ComboBox8.Enabled = False
            If CD.Descuento = 0 Then
                If PrecioNeto = 0 Then
                    PrecioU = Math.Round(CD.Precio / CD.Cantidad, 2)
                Else
                    PrecioU = Math.Round(CD.Precio / CD.Cantidad * (1 + (CD.Iva + CD.IEPS - CD.IVARetenido) / 100), 2)
                End If
            Else
                Dim Val As Double = (CD.Precio / (1 - CD.Descuento / 100))
                If PrecioNeto = 0 Then
                    PrecioU = Math.Round(Val / CD.Cantidad, 2)
                Else
                    PrecioU = Math.Round(Val / CD.Cantidad * (1 + (CD.Iva + CD.IEPS - CD.IVARetenido) / 100), 2)
                End If
            End If

            TextBox12.Text = PrecioU.ToString("0.00")
            PrecioBase = PrecioU
            If PrecioNeto = 0 Then
                If CD.Descuento = 0 Then
                    TextBox6.Text = CD.Precio.ToString("0.00")
                Else
                    TextBox6.Text = Format(PrecioU * CD.Cantidad, "0.00")
                End If
            End If
            If Estado = Estados.Guardada Or Estado = Estados.Cancelada Then
                Button24.Visible = True
            Else
                Button24.Visible = False
            End If
            'TextBox6.Text = CD.Precio.ToString
            TextBox4.Text = CD.Descripcion
            Button4.Text = "Modificar Concepto"
            nombreProducto = CD.Descripcion
            cantAntModificar = CD.Cantidad
            If IdInventario <> 1 Then
                Usaformula = CD.Inventario.UsaFormula
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
            'ConsultaOn = False
            lblUbicacion.Visible = CD.Inventario.UsaUbicacion
            cmbUbicacion.Visible = CD.Inventario.UsaUbicacion
            cmbUbicacion.DataSource = CD.Inventario.Ubicaciones(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario)
            cmbUbicacion.SelectedValue = CD.Ubicacion
            cmbUbicacion.Enabled = False

            ConsultaOn = True
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button9.Enabled = True
            If CheckScroll.Checked Then TextBox5.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosVenta()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta remisión no ha sido guardada. ¿Desea iniciar una nueva remisión? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesARemision(idRemision)
                Dim c As New dbVentasRemisiones(MySqlcon)
                c.Eliminar(idRemision)
                c.RegresaInventario(idRemision)
                P.limpiarDescPromociones()
                P.limpiarVentasdesc()
                Nuevo()



            End If
        Else
            Nuevo()
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NuevoConcepto()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesAlta, PermisosN.Secciones.Ventas) = True Then
                If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbVentasRemisionesInventario(MySqlcon)
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CD.BuscaridInventario(IdDetalle), fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex)))
                    CD.Eliminar(IdDetalle)

                    If tipoElimianr = "nada" Then


                    Else
                        If tipoElimianr = "Promocion" Then

                            eliminarDescuescuento(P.descModificar(IdDetalle, "VentasRemisiones"), tipoElimianr)
                        Else

                            If P.descModificar(IdDetalle, "VentasRemisiones") <> 0 Then
                                eliminarDescuescuento(P.descModificar(IdDetalle, "VentasRemisiones"), tipoElimianr)
                            End If
                        End If


                        Dim S As New dbInventarioSeries(MySqlcon)
                        S.QuitarSeriesAremisionxArticulo(IdInventario, idRemision)
                        If IdInventario <> 0 Then
                            Dim I As New dbInventario(MySqlcon)
                            I.MovimientoDeInventario(IdInventario, CantAnt, CantAnt, dbInventario.TipoMovimiento.Alta, IdAlmacen)
                        End If
                        If EsKit = 1 Then
                            Dim IKits As New dbVentasKits(MySqlcon)
                            IKits.EliminarArticulosRemisiones(IdDetalle)
                        End If
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
        BotonCliente()
    End Sub
    Private Sub BotonCliente()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            If B.Cliente.DireccionFiscal = 0 Then
                TextBox7.Text = B.Cliente.RFC + " " + B.Cliente.Nombre '+ vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.CP
            Else
                TextBox7.Text = B.Cliente.RFC + " " + B.Cliente.Nombre '+ vbCrLf + B.Cliente.Direccion2 + " " + B.Cliente.NoExterior2 + " " + B.Cliente.Ciudad2 + " " + B.Cliente.CP2
            End If
            If B.Cliente.NoChecarCr = 0 Then
                Saldo = B.Cliente.DaSaldoAFecha(B.Cliente.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
                SaldoaFavor = CDbl(Format(B.Cliente.DaSaldoAFavor(B.Cliente.ID), "0.00"))
            End If
            CreditoCliente = B.Cliente.Credito
            'TextBox13.Text = "Límite: " + Format(B.Cliente.Credito, "#,##0.00") + vbCrLf + "Días: " + B.Cliente.CreditoDias.ToString + vbCrLf + "Saldo: " + Format(Saldo, "#,##0.00")

            TextBox7.Text += vbCrLf + "Días/Lím: " + B.Cliente.CreditoDias.ToString + "/" + Format(B.Cliente.Credito, "#,##0.00") + " " + "Saldo: " + Format(Saldo, "#,##0.00") + " " + "A Favor: " + Format(SaldoaFavor, "#,##0.00")
            TextBox7.Text += vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.Estado + " " + B.Cliente.CP
            idCliente = B.Cliente.ID
            IdLista = B.Cliente.IdLista
            Sobre = B.Cliente.SobreescribeIVA
            SIVA = B.Cliente.IVA
            If Saldo >= B.Cliente.Credito Then
                SinCredito = True
            Else
                SinCredito = False
            End If
            If B.Cliente.CreditoDias <> 0 Then
                If B.Cliente.TieneCreditoporFecha(B.Cliente.ID, B.Cliente.CreditoDias) = False Then
                    SinCredito = True
                End If
            End If
            If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                'If (B.Cliente.Credito <> 0 Or B.Cliente.CreditoDias <> 0) And Op.NoPermitirRemisionesdeCredito = 0 Then
                '    ComboBox4.SelectedIndex = 1
                'Else
                '    ComboBox4.SelectedIndex = 0
                'End If
                'ComboBox4.SelectedIndex = IdsConceptos.Busca(B.Cliente.IdFormaR)
                Dim FP As New dbFormasdePagoRemisiones(B.Cliente.IdFormaF, MySqlcon)
                If FP.Tipo = dbFormasdePagoRemisiones.Tipos.Contado Or Op.NoPermitirRemisionesdeCredito = 1 Or GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirRemisionesCredito, PermisosN.Secciones.Ventas) = False Then
                    RadioButton1.Checked = True
                Else
                    RadioButton2.Checked = True
                End If
                If Op.RemisionesSoloaCredito = 1 Then
                    RadioButton2.Checked = True
                End If
                ComboBox4.SelectedIndex = IdsConceptos.Busca(B.Cliente.IdFormaR)
            End If
            If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                If IdVendedorU > 0 And Op.VendedorUsuario = 1 Then
                    ComboBox5.SelectedIndex = IdsVendedores.Busca(IdVendedorU)
                Else
                    ComboBox5.SelectedIndex = IdsVendedores.Busca(B.Cliente.IdVendedor)
                End If
            End If
            ConsultaOn = False
            TextBox1.Text = B.Cliente.Clave
            ConsultaOn = True
            If Op._TipoSelAlmacen <> "0" Then
                'If ComboBox8.SelectedIndex <= 0 Then
                ComboBox8.Focus()
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
        BotonArticulo()
    End Sub
    Private Sub BotonArticulo()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloProducto
        If Op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), True, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        LlenaDatosArticulo(B.Inventario)
                    
                End Select
                ' IdInventario <> 0 Then
                If Usaformula = 1 Then
                    Dim Fo As New frmInventarioFormula01(ArtNombre)
                    If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TextBox5.Text = Format(Fo.Resultado, "0.00")
                        TextBox4.Text = Fo.FormulaString
                    End If
                    Fo.Dispose()
                End If
                TextBox12.Focus()
                'End If
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(TipodeBusqueda, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), True, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        LlenaDatosArticulo(B.Inventario)
                    
                End Select
                ' IdInventario <> 0 Then
                If Usaformula = 1 Then
                    Dim Fo As New frmInventarioFormula01(ArtNombre)
                    If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TextBox5.Text = Format(Fo.Resultado, "0.00")
                        TextBox4.Text = Fo.FormulaString
                    End If
                    Fo.Dispose()
                End If
                TextBox12.Focus()
                'End If
            End If
            B.Dispose()
        End If
    End Sub
    Private Sub LlenaDatosArticulo(ByVal Articulo As dbInventario)
        Dim a As New dbInventarioPrecios(MySqlcon)
        a.BuscaPrecio(Articulo.ID, IdLista)
        Dim Cant As Double
        TextBox4.Text = Articulo.Nombre
        ArtNombre = Articulo.Nombre
        'a = Articulo.DaPrecioDefault
        If IsNumeric(TextBox5.Text) Then
            Cant = CDbl(TextBox5.Text)
        Else
            TextBox5.Text = "1"
            Cant = 1
        End If

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

        PrecioU = Math.Round(a.Precio, 2)
        Esamortizacion = Articulo.EsAmortizacion
        CostoArticulo = Articulo.CostoBase
        CostoArticulo = Articulo.CostoBase
        If Articulo.Contenido > 1 Then
            CostoArticulo = CostoArticulo / Articulo.Contenido
        End If
        PrecioBase = PrecioU
        TextBox12.Text = a.Precio.ToString("0.00")
        ManejaSeries = Articulo.ManejaSeries
        TextBox6.Text = Format(Cant * PrecioU, "0.00")
        If Sobre = 0 Then
            TextBox8.Text = Articulo.Iva
        Else
            TextBox8.Text = SIVA.ToString
        End If
        EsKit = Articulo.EsKit
        SepararKit = Articulo.SepararKit
        PrecioNeto = Articulo.PrecioNeto
        IdInventario = Articulo.ID
        IdVariante = 0
        Usaformula = Articulo.UsaFormula
        cmbVariante.Visible = False
        PorLotes = Articulo.PorLotes
        Aduana = Articulo.Aduana
        txtIEPS.Text = Articulo.ieps.ToString
        txtIVARetenido.Text = Articulo.ivaRetenido.ToString
        If Op._CodigoPostalLocal = "0" Then
            TipoCantidad = Articulo.TipoContenido.ID
        Else
            TipoCantidad = Articulo.TipoCantidad.ID
        End If
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

        lblUbicacion.Visible = Articulo.UsaUbicacion
        cmbUbicacion.Visible = Articulo.UsaUbicacion
        cmbUbicacion.DataSource = Articulo.Ubicaciones(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario)

        ConsultaOn = True
    End Sub
    
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        SacaTotal()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        SacaTotal()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta remisión no ha sido guardada. ¿Desea iniciar una nueva remisión? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesARemision(idRemision)
                Dim c As New dbVentasRemisiones(MySqlcon)
                c.Eliminar(idRemision)
                c.RegresaInventario(idRemision)
                P.limpiarDescPromociones()
                P.limpiarVentasdesc()
            Else
                Exit Sub
            End If
        End If
        Dim f As New frmVentasRemisionesConsulta(ModosDeBusqueda.Secundario, 0, 0)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idRemision = f.IdRemision
            LlenaDatosVenta()
            NuevoConcepto()
        End If
        f.Dispose()
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar esta remisión?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesAlta, PermisosN.Secciones.Ventas) = True Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesARemision(idRemision)
                Dim C As New dbVentasRemisiones(MySqlcon)
                C.RegresaInventario(idRemision)
                C.Eliminar(idRemision)
                P.limpiarDescPromociones()
                P.limpiarVentasdesc()
                PopUp("Remisión Eliminada", 90)
                Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim F As New frmVentasAsignaSeries(IdInventario, 0, idRemision, CDbl(TextBox5.Text))
        F.ShowDialog()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        'If MsgBox("¿Cancelar esta remisión?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '    Modificar(Estados.Cancelada)
        'End If
        If Estado = Estados.Guardada Then
            If MsgBox("¿Cancelar esta remision?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim VP As New dbVentasPagosRemisiones(MySqlcon)
                If VP.HayPagosVentas(idRemision) = 0 Then
                    Dim V As New dbVentasRemisiones(MySqlcon)
                    If V.TieneSalidas(idRemision) = 0 Then
                        Modificar(Estados.Cancelada)
                    Else
                        MsgBox("No se puede cancelar esta remisión primero debe cancelar todas las salidas relacionadas.", MsgBoxStyle.Information, GlobalNombreApp)
                    End If
                Else
                    MsgBox("Para poder cancelar esta remisión primero debe cancelar todos los pagos.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
            End If
        Else
            MsgBox("No se puede cancelar una remisión pendiente.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    'Private Sub CadenaOriginal()
    '    Dim en As New Encriptador
    '    Dim V As New dbVentas(MySqlcon)
    '    'TextBox9.Text = 
    '    'TextBox10.Text = 
    '    'en.GuardaArchivoTexto("XMLFac-" + V.Folio.ToString + ".xml", V.CreaXML(idVenta, IdMonedaG, TextBox10.Text), System.Text.Encoding.UTF8)
    '    Dim Enc As New System.Text.UTF8Encoding
    '    Dim Bytes() As Byte = Enc.GetBytes(V.CreaXML(idRemision, IdMonedaG, en.GeneraSello(V.CreaCadenaOriginal(idRemision, IdMonedaG), My.Settings.rutakey, My.Settings.passwordkey, My.Settings.rutacer)))
    '    'Dim Bytes() As Byte = Encoder.GetBytes(pCadenaOriginal)
    '    en.GuardaArchivo("XMLFac-" + V.Folio.ToString + ".xml", Bytes)
    'End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Try
            Dim Forma As New frmBuscaDocumentoVenta(0, False, 1, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, False, True, True, 0, False, "", False)
            If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim V As New dbVentasRemisiones(MySqlcon)
                If Estado = 0 Then
                    '0 cotizacion
                    '1 pedido
                    '2 remision
                    '3 ventas
                    Select Case Forma.Tipo
                        Case 0
                            Dim Co As New dbVentasCotizaciones(Forma.id(0), MySqlcon)
                            TextBox1.Text = Co.Cliente.Clave
                        Case 1
                            Dim Cp As New dbVentasPedidos(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cp.Cliente.Clave
                        Case 2
                            Dim Cr As New dbVentasRemisiones(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cr.Cliente.Clave
                        Case 3
                            Dim Cv As New dbVentas(Forma.id(0), MySqlcon, Op._Sinnegativos)
                            TextBox1.Text = Cv.Cliente.Clave
                    End Select
                    Guardar()
                    If Estado <> 0 Then
                        V.AgregarDetallesReferencia(idRemision, Forma.id(0), Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                        ConsultaDetalles()
                    End If
                Else
                    V.AgregarDetallesReferencia(idRemision, Forma.id(0), Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    ConsultaDetalles()
                End If
                NuevoConcepto()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox11.Focus()
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ConsultaOn Then
            Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
            If Op._TipoSelAlmacen = "0" Then
                LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
                If ComboBox8.Items.Count = 0 Then
                    MsgBox("Esta sucursal no tiene almacenes, no podrá hacer remisiones si no se registra uno.", MsgBoxStyle.Information, GlobalNombreApp)
                Else
                    ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
                End If
            Else
                LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. Almacen")
                ComboBox8.SelectedIndex = 0
            End If
            LlenaCombos("tblcajas", ComboBox6, "nombre", "nombrem", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            If ComboBox6.Items.Count = 0 Then
                MsgBox("Esta sucursal no tiene cajas dadas de alta, no podrá hacer remisiones si no se registra una.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
            TextBox11.Text = S.Serie
            Opc.LlenaDatos(0, IdsSucursales.Valor(ComboBox3.SelectedIndex))
            Dim V As New dbVentasRemisiones(MySqlcon)
            Dim Sf As New dbSucursalesFolios(MySqlcon)
            Opc.LlenaDatos(0, IdsSucursales.Valor(ComboBox3.SelectedIndex))
            If CheckBox2.Checked = False Then
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Remision, 0)
            Else
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.RemisionesPorSurtir, 0)
            End If
            TextBox11.Text = Sf.Serie
            If Opc.ActivarOc = 1 And ((Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc) Or (Format(Now, "HH:mm") >= Opc.HoraInicioOc2 And Format(Now, "HH:mm") <= Opc.HoraFinOc2)) Then
                TextBox11.Text = Opc.SerieOc
            End If
            TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If Opc.ActivarOc = 1 And ((Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc) Or (Format(Now, "HH:mm") >= Opc.HoraInicioOc2 And Format(Now, "HH:mm") <= Opc.HoraFinOc2)) Then
                If CInt(TextBox2.Text) < Opc.FolioOc Then TextBox2.Text = Opc.FolioOc.ToString
            Else
                If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
            End If

        End If
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Enabled = True Then
                TextBox3.Focus()
            Else
                TextBox12.Focus()
            End If
        End If
        If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
            If Usaformula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtNombre)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.00")
                    TextBox4.Text = Fo.FormulaString
                End If
            End If
        End If
        If e.KeyCode = Keys.F2 And IdInventario <> 0 Then
            presionaF2(True)
        End If
    End Sub
    Private Sub presionaF2(Habilitado As Boolean)
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
        If IdInventario <> 0 Or IdVariante <> 0 Then
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Op._TipoSelAlmacen = "0" Then
                BotonAgregar()
            Else
                If ComboBox8.SelectedIndex <= 0 Then
                    ComboBox8.Focus()
                Else
                    BotonAgregar()
                End If
            End If
        End If
        If e.KeyCode = Keys.F1 And IdInventario <> 0 Then
            Dim SP As New frmSelectorPrecios(IdInventario)
            If SP.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox12.Text = SP.Precio.ToString("0.00")
            End If
        End If
        If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
            If Usaformula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtNombre)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.00")
                    TextBox4.Text = Fo.FormulaString
                End If
            End If
        End If
        If e.KeyCode = Keys.F2 And IdInventario <> 0 Then
            presionaF2(True)
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
                TextBox13.Text = CDbl(TextBox6.Text) * CDbl(TextBox9.Text) / 100
            Else
                TextBox13.Text = "0"
            End If
            Descontando = True
        End If
    End Sub


    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        'If ConsultaOn Then
        '    If IsNumeric(TextBox6.Text) And IsNumeric(TextBox10.Text) Then
        '        If IDsMonedas.Valor(ComboBox1.SelectedIndex) = 2 Then
        '            TextBox12.Text = CStr(PrecioU * CDbl(TextBox10.Text))
        '            'TextBox6.Text = CStr(CDbl(TextBox6.Text) * CDbl(TextBox10.Text))
        '        Else
        '            TextBox12.Text = CStr(PrecioU / CDbl(TextBox10.Text))
        '            'TextBox6.Text = CStr(CDbl(TextBox6.Text) / CDbl(TextBox10.Text))
        '        End If
        '    End If
        'End If
    End Sub
   
    Private Sub ImprimirRemisiones(pTitulo As String, pEsCopia As Boolean, pIdRemision As Integer, pFlujo As Boolean)
        Try
            Dim Remision As New dbVentasRemisiones(pIdRemision, MySqlcon)
            ImpDoc.IdSucursal = Remision.IdSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.VentaRemision
            ImpDoc.TipoDocumentoT = TiposDocumentos.VentaRemision + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.VentaRemision
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.RemisionesPDF
            ImpDoc.Inicializar(pFlujo)
            LlenaNodosImpresion(pTitulo)
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Remision.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Remision.Fecha), "yyyy") + "\" + Format(CDate(Remision.Fecha), "MM") + "\")
            If Op._NoRutas = "0" Then
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
    Private Sub Imprimir(pIdRemision As Integer)
        Dim V As New dbVentasRemisiones(pIdRemision, MySqlcon)
        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        AddError("REM: IMP: Serie:" + V.Serie + " Folio:" + V.Folio.ToString + " Cliente: " + V.IdCliente.ToString + " Total: " + V.TotalaPagar.ToString + " Estado:" + Estado.ToString, "Remisiones", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), V.ID)
        ImprimirRemisiones(Op.TituloOriginalRemision, False, pIdRemision, False)
        If Op.Copiaflujorem = 1 Then
            ImprimirRemisiones(Op.TituloOriginalRemision, True, pIdRemision, True)
        End If
        Dim fdp As New dbFormasdePagoRemisiones(V.idForma, MySqlcon)
        If (Op.ActivarCopiaRemision = 1 And fdp.Tipo = dbFormasdePagoRemisiones.Tipos.Contado) Or (Op.ActivarCopiaRemisionCredito = 1 And fdp.Tipo = dbFormasdePagoRemisiones.Tipos.Credito) Then
            ImprimirRemisiones(Op.TituloCopiaRemision, True, pIdRemision, False)
        End If
        If (Op.ActivarCopia2Remision = 1 And fdp.Tipo = dbFormasdePagoRemisiones.Tipos.Contado) Or (Op.ActivarCopiaRemisionCredito2 = 1 And fdp.Tipo = dbFormasdePagoRemisiones.Tipos.Credito) Then
            ImprimirRemisiones(Op.TituloCopia2Remision, True, pIdRemision, False)
        End If

        If V.Cliente.Email <> "" Then
            Try
                If MsgBox("¿Enviar remisión por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    If V.Cliente.Email <> "" Then
                        Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                        Dim C As String
                        C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "REMISIÓN" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + Op.CorreoContenido
                        M.send("Remisión: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, ImpDoc.RutaPDF + "\REM_" + V.Serie + V.Folio.ToString("0000") + ".pdf", "")
                    End If
                End If
            Catch ex As Exception
                MsgBox("No se puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        End If
    End Sub
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ImpDoc.DibujaPaginaN(e.Graphics)
        If ImpDoc.MasPaginas = True Or ImpDoc.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(ImpDoc.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If
        If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
            If ImpDoc.TipoImpresora = 0 Then
                e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO INVÁLIDO.", New Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point), Brushes.Red, 2, 2)
                e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO INVÁLIDO.", New Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point), Brushes.Red, 2, 110)
                e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO INVÁLIDO.", New Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point), Brushes.Red, 2, 250)
            Else
                e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO INVÁLIDO.", New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point), Brushes.Red, 2, 30)
            End If
        End If
        e.HasMorePages = ImpDoc.MasPaginas
    End Sub
    Private Sub LlenaNodosImpresion(ByVal pTitulo As String)
        Dim V As New dbVentasRemisiones(idRemision, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(idRemision, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
        Dim TotalDescuento As Double = 0
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
        ImpDoc.ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")

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

        If V.PorSurtir = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "porsurtir", "", 0), "porsurtir")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "porsurtir", "POR SURTIR NO SURTIR MATERIAL CON ESTA REMISIÓN.", 0), "porsurtir")
        End If

        ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbVentasRemisionesInventario(MySqlcon)

        ImpDoc.ImpND.Add(New NodoImpresionN("", "almacen", VI.DaAlmacen(V.ID), 0), "almacen")

        DR = VI.ConsultaReader(idRemision, 1, Op._AgregaSeriesAVenta, Op._DetalleKits)
        ImpDoc.ImpNDD.Clear()
        ImpDoc.CuantosRenglones = 0
        Dim TotalIEPS As Double = 0
        Dim TotalIVARetenido As Double = 0
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
            If DR("clave") <> "" Then
                CodigoBarras.Code = Replace(Replace(DR("clave"), "Ñ", "N"), "ñ", "n")
            Else
                CodigoBarras.Code = ""
            End If
            Try
                Nimagen.Imagen = CodigoBarras.CreateDrawingImage(Color.Black, Color.White)
            Catch ex As Exception

            End Try

            ImpDoc.ImpNDD.Add(Nimagen, "codigobarras1" + Format(Cont, "000"))

            Nimagen = New NodoImpresionN("", "codigobarras2", "", 0)
            If DR("clave2") <> "" Then
                CodigoBarras.Code = Replace(Replace(DR("clave2"), "Ñ", "N"), "ñ", "n")
            Else
                CodigoBarras.Code = ""
            End If
            Try
                Nimagen.Imagen = CodigoBarras.CreateDrawingImage(Color.Black, Color.White)
            Catch ex As Exception

            End Try

            ImpDoc.ImpNDD.Add(Nimagen, "codigobarras2" + Format(Cont, "000"))

            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), 0), "cantidad" + Format(Cont, "000"))
            If DR("iva") = 0 Then
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tieneiva", "", 0), "tieneiva" + Format(Cont, "000"))
            Else
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tieneiva", "*", 0), "tieneiva" + Format(Cont, "000"))
            End If
            If DR("cantidadm") <> 0 Then
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidadm"), Op._formatocantidad).PadLeft(Op.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipom"), 0), "tipocantidad" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidadm"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciouiva", Format((DR("precio") / DR("cantidadm")) * (1 + DR("iva") / 100), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciouiva" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "importe" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(DR("precio") * (1 + DR("iva") / 100), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))
                'Vo = Vd / ( 1 - (Por/100))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ieps", Format(DR("precio") * (DR("ieps") / 100), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "ieps" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", Format(DR("precio") * (1 + DR("ivaretenido") / 100), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "ivaRetenido" + Format(Cont, "000"))
                TotalIEPS += Double.Parse(DR("IEPS").ToString())
                TotalIVARetenido += Double.Parse(DR("IVARetenido").ToString())
            Else
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", "", 0), "cantidad" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipocantidad", "", 0), "tipocantidad" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", "", 0), "preciou" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciouiva", "", 0), "preciouiva" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", "", 0), "importe" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importeiva", "", 0), "importeiva" + Format(Cont, "000"))

                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ieps", "", 0), "ieps" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", "", 0), "ivaRetenido" + Format(Cont, "000"))
            End If
            If DR("cantidadm") <> 0 And DR("descuento") <> 0 Then
                Dim Desc As Double
                Desc = (DR("precio") / (1 - DR("descuento") / 100))
                TotalDescuento += Desc - DR("precio")
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(Op.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - DR("precio"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidadm"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
            Else
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))
                If DR("cantidadm") <> 0 Then
                    ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(DR("precio") / DR("cantidadm"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                Else
                    ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
                End If

            End If
            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()

        ImpDoc.ImpND.Add(New NodoImpresionN("", "Totalieps", Format(V.TotalIeps, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "Totalieps")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "TotalivaRetenido", Format(V.TotalIvaRetenidoConceptos, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "TotalivaRetenido")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "peso", Format(V.TotalPeso, "#,##0.00") + "Kg.", 0), "peso")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totalcantidad", V.DaTotalCantidad(idRemision).ToString, 0), "totalcantidad")
        If V.IdVentaR = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "folioref", "", 0), "folioref")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "folioref", V.FolioRef, 0), "folioref")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtototal, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtototal + TotalDescuento, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalsindesc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totaldesc", Format(TotalDescuento, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totaldesc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totalofertas", Format(V.TotalOfertas, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totalofertas")
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(idRemision)
        ImpDoc.ImpNDDi.Clear()
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                'IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                If IDsMonedas2.Valor(ComboBox2.SelectedIndex) = DR("idmoneda") Then
                    IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                Else
                    IvasImporte.Add((DR("precio") * (DR("iva") / 100)) * DR("tipodecambio"), DR("iva").ToString)
                End If
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                'IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                If IDsMonedas2.Valor(ComboBox2.SelectedIndex) = DR("idmoneda") Then
                    IvasImporte.Add(IAnt + DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                Else
                    IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)) * DR("tipodecambio"), DR("iva").ToString)
                End If
            End If
        End While
        DR.Close()
        Cont = 0
        For Each I As Double In Ivas
            ImpDoc.ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        Next

        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And Op._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If
        Dim FP As New dbFormasdePagoRemisiones(V.idForma, MySqlcon)
        Dim strMetodos As String = ""
        Dim MeP As New dbVentasAddMetodos(MySqlcon)
        DR = MeP.ConsultaReader(1, V.ID)
        While DR.Read()
            If strMetodos <> "" Then strMetodos += vbNewLine
            strMetodos += DR("nombre")
        End While
        DR.Close()
        If strMetodos <> "" Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "metodopago", strMetodos, 0), "metodopago")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "metodopago", FP.Nombre, 0), "metodopago")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "total")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Recibido:", Format(V.TotalVenta, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "recibido")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Cambio:", Format(0, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "cambio")
        Dim Abonado = V.DaTotalAbonado(V.ID)
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Abonado:", Format(Abonado, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "totalabonado")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Restante:", Format(V.TotalVenta - Abonado, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "restante")

        If FP.Tipo = dbFormasdePagoRemisiones.Tipos.Contado Or FP.Tipo = dbFormasdePagoRemisiones.Tipos.Otro Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "condicionpago", "CONTADO", 0), "condicionpago")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "diascredito", "", 0), "diascredito")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "limitecredito", "", 0), "limitecredito")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "condicionpago", "CRÉDITO", 0), "condicionpago")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "diascredito", V.Cliente.CreditoDias.ToString + " Días.", 0), "diascredito")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "limitecredito", Format(DateAdd(DateInterval.Day, V.Cliente.CreditoDias, CDate(V.Fecha)), "yyyy-MM-dd"), 0), "limitecredito")
        End If
        Dim CL As New CLetras
        If V.TotalVenta >= 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(Format(V.TotalVenta, "0.00"), V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(Format(V.TotalVenta * -1, "0.00"), V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
        End If
        Dim Vendedor As New dbVendedores(V.IdVEndedor, MySqlcon)
        ImpDoc.ImpND.Add(New NodoImpresionN("", "vendedor", Vendedor.Nombre, 0), "vendedor")
        If V.Estado = Estados.Cancelada Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        ImpDoc.Posicion = 0
        ImpDoc.NumeroPagina = 1
    End Sub

    

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If Op.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Imprimir(idRemision)
        Dim S As New dbInventarioSeries(MySqlcon)
        If S.CantidadDeSeriesAgregadasaRemision(idRemision, 0) > 0 Then
            If MsgBox("¿Imprimir listado de series?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                ImprimirSeries()
            End If
        End If
    End Sub
    Private Sub ImprimirSeries()
        Dim V As New dbVentasRemisiones(idRemision, MySqlcon)
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Rep = New repVentasSeriesR
        'V.ReporteVentasSeries(idRemision)
        Rep.SetDataSource(V.ReporteVentasSeries(idRemision))
        'Rep.SetParameterValue("Encabezado", O._NombreEmpresa)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        ImprimirSeries()
    End Sub

    Private Sub DateTimePicker1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox3.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub TextBox11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged

    End Sub

    Private Sub ComboBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Op._CursorVentas = "0" Then
                TextBox5.Focus()
            Else
                TextBox3.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged

    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtIEPS.Focus()
        End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim FC As New frmClientes(1, idCliente)
        If FC.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = ""
            TextBox1.Text = FC.CodigoCliente
        End If
        FC.Dispose()
    End Sub

    Private Sub DGDetalles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim et As New frmVentasTextoExtra(TextBox4.Text, 2000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox4.Text = et.Texto
        End If
    End Sub

    Private Sub CheckScroll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckScroll.CheckedChanged
        If CheckScroll.Checked Then
            My.Settings.ventasscroll = True
        Else
            My.Settings.ventasscroll = False
        End If
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        If Estado = Estados.Pendiente Or Estado = Estados.Inicio Or Estado = Estados.SinGuardar Then
            Dim V As New dbVentasRemisiones(MySqlcon)
            If CheckBox3.Checked Then
                Dim Sf As New dbSucursalesFolios(MySqlcon)
                If CheckBox2.Checked = False Then
                    Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Remision, 0)
                Else
                    Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.RemisionesPorSurtir, 0)
                End If
                'TextBox11.Text = Sf.Serie

                If Opc.ActivarOc = 1 And ((Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc) Or (Format(Now, "HH:mm") >= Opc.HoraInicioOc2 And Format(Now, "HH:mm") <= Opc.HoraFinOc2)) Then
                    TextBox11.Text = Opc.SerieOc
                Else
                    TextBox11.Text = Sf.Serie
                End If
                TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
                If Opc.ActivarOc = 1 And ((Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc) Or (Format(Now, "HH:mm") >= Opc.HoraInicioOc2 And Format(Now, "HH:mm") <= Opc.HoraFinOc2)) Then
                    If CInt(TextBox2.Text) < Opc.FolioOc Then TextBox2.Text = Opc.FolioOc.ToString
                Else
                    If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
                End If
                Label17.Visible = False
                'If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
            Else
                TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            End If
        End If
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 500, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbVentasRemisiones(MySqlcon)
                V.ActualizaComentario(idRemision, TextBox14.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Dim Fac As New frmVentasN(idVenta, 0, 0, 0)
        Fac.ShowDialog()
        Fac.Dispose()
    End Sub


    '*************Descuentos***********
    Private Sub hayDescuento()

        ' Dim CD As New dbVentasInventario(MySqlcon)
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
                    des = TotalPorcentaje(CDbl(TextBox12.Text), Integer.Parse(TablaDesc.Rows(0)(2).ToString()))
                    'des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: " + TablaDesc.Rows(0)(2).ToString() + " %"
                Else
                    des = Double.Parse(TablaDesc.Rows(0)(2).ToString())
                    des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: $" + TablaDesc.Rows(0)(2).ToString() + " P/U"
                End If

                CD.Guardar(idRemision, 1, CDbl(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 0, 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), CDbl(TextBox5.Text), TipoCantidad, 0, If(cmbUbicacion.Visible, cmbUbicacion.SelectedValue, ""))
                'CD.Guardar(idVenta, 1, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0, 0, Double.Parse(TextBox5.Text), 1)
                P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idRemision, "VentasRemisiones")

                ConsultaDetalles()
                NuevoConcepto()

            Else
                nombreProducto = TextBox4.Text()
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
        nombreProducto = TextBox4.Text
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
            P.guardarPromocion(idRemision, idDescuento, idProducto, CD.UltomoRegistro())
        Next


        If P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count() >= promocion1 Then

            regAnadir = Int(P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count() Mod promocion1)
            regDesc = Int(P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

            cDesc = promocion1 - promocion2
            des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
            des = des * regDesc

            CD.Guardar(idRemision, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), CDbl(TextBox5.Text), TipoCantidad, 0, If(cmbUbicacion.Visible, cmbUbicacion.SelectedValue, ""))
            P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idRemision, "VentasRemisiones")

            ConsultaDetalles()
            NuevoConcepto()
            P.EliminarDesc(idRemision, idDescuento, idProducto)
            'anadir registros faltantes
            For i As Integer = 1 To regAnadir
                P.guardarPromocion(idRemision, idDescuento, idProducto, CD.UltomoRegistro())
            Next

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

    Public Function fechaFormato() As String
        Dim fechita2 As String
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
    Public Sub modificarDescuento(ByVal idMod As Integer)
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
                CD.Modificar(idMod, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), CDbl(TextBox9.Text), Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), CDbl(TextBox5.Text), TipoCantidad, 0)
                ' CD.Guardar(idVenta, 1, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0, 0, Double.Parse(TextBox5.Text), 1)
                'P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idVenta)
                P.ModificarDescuento(idMod, idDescuento, idRemision, "VentasRemisiones")

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
                P.guardarPromocion(idRemision, idDescuento, idProducto, CD.UltomoRegistro())
            Next

            If P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count() >= promocion1 Then

                regAnadir = Int(P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count() Mod promocion1)
                regDesc = Int(P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

                cDesc = promocion1 - promocion2
                des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
                des = des * regDesc

                CD.Guardar(idRemision, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), regDesc, TipoCantidad, 0, If(cmbUbicacion.Visible, cmbUbicacion.SelectedValue, ""))
                P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idRemision, "VentasRemisiones")

                ConsultaDetalles()
                NuevoConcepto()
                P.EliminarDesc(idRemision, idDescuento, idProducto)
                'anadir registros faltantes
                For i As Integer = 1 To regAnadir
                    P.guardarPromocion(idRemision, idDescuento, idProducto, CD.UltomoRegistro())
                Next



            End If

        Else
            'hay que eliminar todos los registros de promociones y hacer los calculos otra vez
            P.EliminarDesc(idRemision, idDescuento, idProducto)
            P.EliminarDescAnadidosRem(idRemision, descripcion) 'id remision
            Dim dt As DataTable
            Dim tot As Double = 0
            Dim tot2 As Integer
            dt = P.buscarDesAnadidosRem(idRemision, IdInventario)

            For i As Integer = 0 To dt.Rows.Count - 1
                tot = tot + Double.Parse(dt.Rows(i)(3).ToString())
            Next

            tot2 = Int(tot)

            For i As Integer = 1 To tot2 'agregar los que se almacenaron
                P.guardarPromocion(idRemision, idDescuento, idProducto, 0)
            Next

            If P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count() >= promocion1 Then

                regAnadir = Int(P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count() Mod promocion1)
                regDesc = Int(P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

                cDesc = promocion1 - promocion2
                des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
                des = des * regDesc

                CD.Guardar(idRemision, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), CDbl(TextBox5.Text), TipoCantidad, 0, If(cmbUbicacion.Visible, cmbUbicacion.SelectedValue, ""))
                P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idRemision, "VentasRemisiones")

                ConsultaDetalles()
                NuevoConcepto()
                P.EliminarDesc(idRemision, idDescuento, idProducto)
                'anadir registros faltantes
                For i As Integer = 1 To regAnadir
                    P.guardarPromocion(idRemision, idDescuento, idProducto, CD.UltomoRegistro())
                Next
            End If

        End If
    End Sub

    Public Sub eliminarDescuescuento(ByVal idElim As Integer, ByVal Tipo As String)
        If Tipo <> "Promocion" Then
            CD.Eliminar(idElim)
            P.EliminarDesc(idElim, "VentasRemisiones")
        Else
            eliminarPromocion()
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
        P.EliminarDesc(idRemision, idDescuento, idProducto)
        P.EliminarDescAnadidosRem(idRemision, descripcion)
        Dim dt As DataTable
        Dim tot As Double = 0
        Dim tot2 As Integer
        dt = P.buscarDesAnadidosRem(idRemision, IdInventario)

        For i As Integer = 0 To dt.Rows.Count - 1
            tot = tot + Double.Parse(dt.Rows(i)(3).ToString())
        Next

        tot2 = Int(tot)

        For i As Integer = 1 To tot2 'agregar los que se almacenaron
            P.guardarPromocion(idRemision, idDescuento, idProducto, 0)
        Next

        If P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count() >= promocion1 Then

            regAnadir = Int(P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count() Mod promocion1)
            regDesc = Int(P.buscarPromociones(idRemision, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

            cDesc = promocion1 - promocion2
            des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
            des = des * regDesc

            CD.Guardar(idRemision, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), regDesc, TipoCantidad, 0, If(cmbUbicacion.Visible, cmbUbicacion.SelectedValue, ""))
            P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idRemision, "VentasRemisiones")

            ConsultaDetalles()
            NuevoConcepto()
            P.EliminarDesc(idRemision, idDescuento, idProducto)
            'anadir registros faltantes
            For i As Integer = 1 To regAnadir
                P.guardarPromocion(idRemision, idDescuento, idProducto, CD.UltomoRegistro())
            Next
        End If

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

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        If PorLotes = 1 Then
            Dim F As New frmInventarioLotes(0, IdDetalle, 0, 0, CDbl(TextBox5.Text), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub txtIEPS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIEPS.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub frmVentasRemisiones_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Panel3.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub txtIVARetenido_TextChanged(sender As Object, e As EventArgs) Handles txtIVARetenido.TextChanged

    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        If Aduana = 1 Then
            Dim F As New frmInventarioAduana(0, IdDetalle, 0, 0, CDbl(TextBox5.Text), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Lectura = SerialPort1.ReadExisting
            If Lectura <> "" And Veces <= 200 Then
                If Estado <> 3 Or Estado <> 4 Then TextBox5.Text = Replace(Replace(Replace(Replace(Lectura, "+", ""), "oz", ""), "lb", ""), "kg", "").Trim
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
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Timer1.Enabled = False
        End Try
        
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        presionaF2(False)
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Dim F As New frmPuntodeVentaCajaEstado(IdsCajas.Valor(ComboBox6.SelectedIndex))
        F.ShowDialog()
        F.Dispose()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If ConsultaOn Then
            Dim Sf As New dbSucursalesFolios(MySqlcon)
            If CheckBox2.Checked = False Then
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Remision, 0)
            Else
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.RemisionesPorSurtir, 0)
            End If
            TextBox11.Text = Sf.Serie
            'If Opc.ActivarOc = 1 And Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc Then
            If Opc.ActivarOc = 1 And ((Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc) Or (Format(Now, "HH:mm") >= Opc.HoraInicioOc2 And Format(Now, "HH:mm") <= Opc.HoraFinOc2)) Then
                TextBox11.Text = Opc.SerieOc
            End If
            Dim V As New dbVentasRemisiones(MySqlcon)
            TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If Opc.ActivarOc = 1 And ((Format(Now, "HH:mm") >= Opc.HoraInicioOc And Format(Now, "HH:mm") <= Opc.HoraFinOc) Or (Format(Now, "HH:mm") >= Opc.HoraInicioOc2 And Format(Now, "HH:mm") <= Opc.HoraFinOc2)) Then
                If CInt(TextBox2.Text) < Opc.FolioOc Then TextBox2.Text = Opc.FolioOc.ToString
            Else
                If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
            End If
        End If
    End Sub


    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked And ConsultaOn Then
            LlenaCombos("tblformasdepagoremisiones", ComboBox4, "nombre", "nombrec", "idforma", IdsConceptos, "tipo=1 or tipo=2", , "idforma")
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked And ConsultaOn Then
            LlenaCombos("tblformasdepagoremisiones", ComboBox4, "nombre", "nombrec", "idforma", IdsConceptos, "tipo=3", , "idforma")
        End If
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        If MsgBox("¿Remover los métodos de pago agregados?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            MetodosDePago.RemoverTodo(1, idRemision)
            RadioButton1.Enabled = True
            RadioButton2.Enabled = True
            ComboBox4.Enabled = True
            Button37.Visible = False
            PopUp("Métodos removidos", 60)
        End If
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        If TotalVenta > 0 Then
            If RadioButton1.Checked Then
                Dim fmp As New frmVentasSelectorMetodosPago(1, idRemision, TotalVenta, 1, False)
                fmp.ShowDialog()
                fmp.Dispose()
            Else
                Dim fmp As New frmVentasSelectorMetodosPago(1, idRemision, TotalVenta, 0, False)
                fmp.ShowDialog()
                fmp.Dispose()
            End If
            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            DR = MetodosDePago.ConsultaReader(1, idRemision)
            If DR.Read() Then
                ComboBox4.SelectedIndex = IdsConceptos.Busca(DR("idforma"))
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

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        If idRemision > 0 Then
            Dim frmK As New FrmDocKardex(idRemision, 1, TextBox11.Text + TextBox2.Text, TextBox1.Text)
            frmK.ShowDialog()
            frmK.Dispose()
        End If
    End Sub


    Private Sub TextBox13_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged
        If Descontando Then
            Descontando = False
            If IsNumeric(TextBox13.Text) And IsNumeric(TextBox6.Text) Then
                If CDbl(TextBox6.Text) <> 0 Then TextBox9.Text = CDbl(TextBox13.Text) * 100 / CDbl(TextBox6.Text)
            Else
                TextBox9.Text = "0"
            End If
            Descontando = True
        End If
    End Sub
End Class