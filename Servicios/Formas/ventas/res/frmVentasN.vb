
Public Class frmVentasN
    Dim IdsVariantes As New elemento
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
    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim ImpNDi2 As New Collection
    Dim Sobre As Byte
    Dim SIVA As Double
    Dim TipoCreardesde As Byte
    'Dim ImpNDS As New Collection
    'Dim ImpNDSD As New Collection
    'Dim ImpNDDiS As New Collection
    'Dim ImpNDiS As New Collection
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    Dim CadenaCFDI As String
    Dim CodigoBidimensional As Bitmap
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim CuantaY As Integer
    Dim LimitedeFolios As Boolean = False
    Dim idRemisiones() As Integer
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

    Dim P As New dbDescuentos(MySqlcon)
    Dim CD As New dbVentasInventario(MySqlcon)
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
                e.Cancel = False
            Else
                GlobalEstadoVentanas = GlobalEstadoVentanas And Not 1
                e.Cancel = True
            End If
        Else
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
            BotonBuscar()
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
        If e.KeyCode = Keys.F6 And IdInventario <> 0 Then
            Dim f As New frmInventarioConsulta(IdInventario)
            f.ShowDialog()
            f.Dispose()
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


                Op = New dbOpciones(MySqlcon)
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
            'ConsultaOn = True
            LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
            ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
            'ConsultaOn = False
            If Op.NoPermitirFacturasdeCredito = 0 Then
                LlenaCombos("tblformasdepago", ComboBox4, "concat(convert(if(tipo=0,'CRÉDITO',if(tipo=1,'CONTADO','PARCIALIDAD')) using utf8),'-',nombre)", "nombret", "idforma", idsFormasDePago, , , "idforma")
            Else
                LlenaCombos("tblformasdepago", ComboBox4, "concat(convert(if(tipo=0,'CRÉDITO',if(tipo=1,'CONTADO','PARCIALIDAD')) using utf8),'-',nombre)", "nombret", "idforma", idsFormasDePago, "tipo=1", , "idforma")
            End If
            LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
            LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
            LlenaCombos("tblvendedores", ComboBox5, "nombre", "nombret", "idvendedor", IdsVendedores)
            ConsultaOn = True
            If idVenta = 0 And Funcion = 0 Then
                Nuevo()
            Else
                If Funcion = 0 Then
                    LlenaDatosVenta()
                    NuevoConcepto()
                End If
            End If
            If IdVentaOrigen <> 0 And Funcion = 1 Then
                Nuevo()
                Dim FO As New dbVentas(IdVentaOrigen, MySqlcon, Op._Sinnegativos)
                TextBox11.Text = "PAR"
                TextBox2.Text = FO.DaNuevoFolio("PAR", IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion).ToString
                Dim Cl As New dbClientes(IdClienteOrigen, MySqlcon)
                TextBox1.Text = Cl.Clave
                ComboBox4.SelectedIndex = idsFormasDePago.Busca(99)
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
                Label32.Text = Format(V.TotalPeso, "#,##0.00") + "Kg."
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        idRemisiones = idremisionescero
        Button11.Enabled = True
        DateTimePicker1.Value = Date.Now
        TextBox1.Text = ""
        FolioAnt = 0
        idVenta = 0
        TipoCreardesde = 0
        SaldoaFavor = 0
        CheckBox2.Enabled = True
        CheckBox3.Checked = False
        If Op.SiemprePorSurtirVentas = 0 Then
            CheckBox2.Checked = False
        Else
            CheckBox2.Checked = True
        End If
        Label32.Text = "0.00Kg."
        Button30.Enabled = False
        Panel1.Enabled = True
        Panel2.Enabled = True
        Button27.Enabled = False
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
        CheckBox1.Checked = False
        ComboBox3.Enabled = True
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        iTipoFacturacion = GlobalTipoFacturacion

        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        If Op._TipoSelAlmacen = "0" Then
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
        Else
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. almacen")
            ComboBox8.SelectedIndex = 0
        End If
        'TextBox11.Text = S.Serie

        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Factura, iTipoFacturacion)

        TextBox11.Text = Sf.Serie
        Eselectronica = Sf.EsElectronica
        Dim V As New dbVentas(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        Dim CM As New dbMonedasConversiones(1, MySqlcon)
        TextBox10.Text = CM.Cantidad.ToString
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        Button20.Visible = False
        ComboBox6.Items.Clear()
        ComboBox6.Text = ""
        CheckBox1.Checked = False
        SerieAnt = ""
        Button2.Enabled = True
        Label24.Visible = False
        TextBox14.Text = ""
        TextBox15.Text = "1"
        Button23.Visible = False
        ComboBox4.SelectedIndex = 0
        NuevoConcepto()

        If Sf.EsElectronica >= 1 Then
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
                    Saldo = c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
                    CreditoCliente = c.Credito
                    ActivarImpuestos = c.ActivarImpuestos
                    SaldoaFavor = CDbl(Format(c.DaSaldoAFavor(c.ID), "0.00"))
                    TextBox7.Text += vbCrLf + "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00") + " " + "Saldo: " + Format(Saldo, "#,##0.00") + " " + "A Favor: " + Format(SaldoaFavor, "#,##0.00")
                    TextBox7.Text += vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.Estado + " " + c.CP
                    If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                        If (c.Credito > 0 Or c.CreditoDias > 0) And Op.NoPermitirFacturasdeCredito = 0 Then
                            ComboBox4.SelectedIndex = 1
                        Else
                            ComboBox4.SelectedIndex = 0
                        End If
                        ComboBox5.SelectedIndex = IdsVendedores.Busca(c.IdVendedor)
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
                    ComboBox6.SelectedIndex = 0
                Else
                    TextBox7.Text = ""
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
            If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
                If pEstado = Estados.Guardada Then
                    MensajeError += "Folio repetido."
                End If
                'TextBox2.Text = C.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion).ToString
            End If
            If MensajeError = "" Then
                Dim PorSutir As Byte
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
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

                C.Modificar(idVenta, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, TextBox11.Text, Sf.NoAprobacion, Sc.NoSerie, Sf.YearAprobacion, Eselectronica, pEstado, iIdFormaPago, 0, CDbl(TextBox10.Text), IDsMonedas2.Valor(ComboBox2.SelectedIndex), C.Subtototal, C.TotalVenta, idCliente, IdsVendedores.Valor(ComboBox5.SelectedIndex), TextBox14.Text, ComboBox6.Text, CDbl(TextBox16.Text), 0, IdVentaOrigen, Parcialidad, Parcialidades, PorSutir, RefDocumento, Adicional, CDbl(TextBox19.Text), CDbl(TextBox18.Text), formaNa)

                Dim CM As New dbMonedasConversiones(MySqlcon)
                CM.Modificar(1, CDbl(TextBox10.Text))
                Estado = pEstado
                If pEstado = Estados.Cancelada Then
                    Dim S As New dbInventarioSeries(MySqlcon)
                    Dim Ligada As Integer
                    Ligada = C.VienedeRemision(idVenta)
                    C.DesligaRemisiones(idVenta)
                    'Ligada = C.VienedeFertilizantePedido(idVenta)
                    C.DesligaFertilizantesPedidos(idVenta)
                    S.QuitaSeriesAVenta(idVenta)
                    If Ligada = 0 Then C.RegresaInventario(idVenta)
                    If MsgBox("¿Imprimir Factura Cancelada?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Select Case Eselectronica
                            Case 0
                                Imprimir()
                            Case 1
                                CadenaOriginal(pEstado, XMLAdenda)
                            Case 2
                                CadenaOriginali(pEstado, XMLAdenda)
                        End Select

                    End If
                End If
                If pEstado = Estados.Guardada Then
                    C.ModificaInventario(idVenta, PorSutir)
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
                    If Op.IntegrarBancos = 1 And (FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad) And GlobalConBancos And GlobalPermisos.ChecaPermiso(PermisosN.Bancos.DepositosVer, PermisosN.Secciones.Bancos) = True Then
                        Dim PP As New frmDeposito(Op.IntegrarBancos, "FACTURA: " + TextBox11.Text + TextBox2.Text, ComboBox4.Text.Replace("CONTADO-", ""), "", C.TotalVenta, idCliente, idVenta.ToString, Format(DateTimePicker1.Value, "dd/MM/yyyy"), 2)
                        'TextBox1.Text=Total pagado
                        If PP.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                            MsgBox("No se completo el ligado a bancos. Para ligar esta factura a bancos lo puede hacer después en un depósito.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                        PP.Dispose()
                    End If

                    Select Case Eselectronica
                        Case 0
                            Imprimir()
                        Case 1
                            CadenaOriginal(pEstado, XMLAdenda)
                        Case 2
                            CadenaOriginali(pEstado, XMLAdenda)
                    End Select
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
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
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
                C.Guardar(idCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, TextBox11.Text, Sf.NoAprobacion, Sc.NoSerie, Sf.YearAprobacion, iTipoFacturacion, IdsSucursales.Valor(ComboBox3.SelectedIndex), idsFormasDePago.Valor(ComboBox4.SelectedIndex), CDbl(TextBox10.Text), IDsMonedas2.Valor(ComboBox2.SelectedIndex), Isr, IvaRetenido, IdsVendedores.Valor(ComboBox5.SelectedIndex), 0, CDbl(TextBox19.Text), CDbl(TextBox18.Text))
                idVenta = C.ID
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
                If p.BuscaArticulo(TextBox3.Text, 0) Then
                    LlenaDatosArticulo(p)
                Else
                    IdInventario = 0
                    Dim ps As New dbProductos(MySqlcon)
                    If ps.BuscaProducto(TextBox3.Text) Then
                        LlenaDatosProducto(ps)
                    Else
                        TextBox4.Text = ""
                        TextBox6.Text = "0"
                        TextBox8.Text = "0"
                        TextBox9.Text = "0"
                        PrecioU = 0
                        IdVariante = 0
                    End If
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
        IvaRetenido = C.IvaRetenido
        TextBox18.Text = C.SobreEscribeImpLoc.ToString
        Isr = C.ISR
        If C.Desglosar = 1 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
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
        RefDocumento = C.RefDocumento
        Adicional = C.Adicional
        TextBox15.Text = C.Parcialidades.ToString
        IdVentaOrigen = C.IdVentaOrigen
        DateTimePicker1.Value = C.Fecha
        ComboBox4.SelectedIndex = idsFormasDePago.Busca(C.IdFormadePago)
        ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.IdConversion)
        ComboBox5.SelectedIndex = IdsVendedores.Busca(C.IdVendedor)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        ComboBox3.Enabled = False
        Button11.Enabled = False
        Button27.Enabled = True
        'ConsultaOn = True
        LlenaDatosDetalles()
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                Button13.Enabled = False
                Panel1.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
                Button15.Enabled = True
                Button30.Enabled = True
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Button2.Enabled = False
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Panel1.Enabled = False
                Panel2.Enabled = False
                Button15.Enabled = True
                Button30.Enabled = True
            Case Else
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
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
            T = CD.ConsultaReader(idVenta, False, "0", 0, Op._OrdenUbicacion)
            While T.Read
                If T("cantidad") <> 0 Then
                    If T("idinventario") > 1 Then
                        Tabla.Rows.Add(T("idventasinventario"), "A", "", T("cantidadm"), T("tipom"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidadm"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    Else
                        Tabla.Rows.Add(T("idventasinventario"), "P", "", T("cantidadm"), T("tipom"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidadm"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    End If
                Else
                    If T("idinventario") > 1 Then
                        Tabla.Rows.Add(T("idventasinventario"), "A", "", T("cantidadm"), T("tipom"), T("clave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                    Else
                        Tabla.Rows.Add(T("idventasinventario"), "P", "", T("cantidadm"), T("tipom"), T("clave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                    End If
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
        cmbVariante.Visible = False
        TextBox12.Text = "0"
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "0"
        TextBox9.Text = "0"
        TextBox6.Text = "0"
        Label20.Text = "0"
        'txtIEPS.Text = "0"
        'txtIVARetenido.Text = "0"
        PrecioBase = 0
        Button32.Visible = False
        ComboBox7.Text = ""
        cmbVariante.Visible = False
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
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodeAlmacen, PermisosN.Secciones.Ventas) = False Then
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
            Dim CD As New dbVentasInventario(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""

            If IdInventario = 0 Then
                MsgError += "Debe indicar un artículo."
                HayError = True
            End If
            'If TipoCreardesde = 1 Then
            '    If idRemisiones.Length <> 0 Then
            '        If idRemisiones(0) <> 0 Then
            '            MsgError += "No se puede agregar mas conceptos en una factura que se generó de ""Crear desde""."
            '            HayError = True
            '        End If
            '    End If
            'End If

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

                If Op._AvisoCosto = "1" And GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirVentaBajoCosto, PermisosN.Secciones.Ventas) = True And I.Inventariable = 1 Then
                    If CDbl(TextBox12.Text) < CostoArticulo Then
                        If MsgBox("El precio del artículo está debajo del costo. ¿Agregar el concepto de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                            MsgError += "Precio debajo del costo."
                            HayError = True
                        End If
                    End If
                End If
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirVentaBajoCosto, PermisosN.Secciones.Ventas) = False And CDbl(TextBox12.Text) < CostoArticulo And I.Inventariable = 1 Then
                    MsgError += " No se puede vender un artículo debajo de su costo."
                    HayError = True
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


            If IdInventario <> 0 And GlobalSoloExistencia = True And I.Inventariable = 1 And CheckBox2.Checked = False Then
                Dim Cant As Double
                Cant = I.DaInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario)
                If I.Contenido <> 1 Then
                    Cant = Cant * I.Contenido
                End If
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
                        CD.Guardar(idVenta, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), Trim(TextBox4.Text), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, IdServicio, I.Inventariable, CantidadMostrar, TipoCantidadMostrar, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), ComboBox7.Text)
                        'agregar descuento
                        hayDescuento()
                        IdDetalle = CD.ID
                    Else
                        If EsKit = 1 And SeparaKit = 1 Then
                            CD.SeparaKit(idVenta, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), Trim(TextBox4.Text), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, IdServicio, I.Inventariable, CantidadMostrar, TipoCantidadMostrar, CDbl(txtIEPS.Text), CDbl(txtIVARetenido.Text))
                            IdDetalle = 0
                        End If
                    End If
                    If EsKit = 1 And SeparaKit = 0 Then
                        Dim IKits As New dbVentasKits(MySqlcon)
                        IKits.InsertarArticulos(IdInventario, idVenta, CD.ID, CDbl(TextBox5.Text), IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
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
                            Dim F As New frmInventarioLotes(IdDetalle, 0, 0, 0, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        If Aduana = 1 Then
                            Dim F As New frmInventarioAduana(IdDetalle, 0, 0, 0, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
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
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), CantidadMostrar, TipoCantidadMostrar, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), ComboBox7.Text)
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
                            Dim F As New frmInventarioLotes(IdDetalle, 0, 0, 0, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                        If Aduana = 1 Then
                            Dim F As New frmInventarioAduana(IdDetalle, 0, 0, 0, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
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


    Private Sub AgregaProducto()
        Try
            Dim CD As New dbVentasProductos(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            If IdVariante = 0 Then MsgError += "Debe indicar un prodcucto."
            If IsNumeric(TextBox5.Text) Then
                If CDbl(TextBox5.Text) <= 0 Then
                    MsgError += "La cantidad debe ser un valor mayor a 0."
                    HayError = True
                End If
            Else
                MsgError += "La cantidad debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(TextBox6.Text) = False Then
                MsgError += vbCrLf + "El costo debe ser un valor numérico."
            Else
                If CInt(TextBox6.Text) <= 0 Then MsgError += " El costo debe ser un valor mayor a 0."
            End If
            If HayError = False Then
                If Button4.Text = "Agregar concepto" Then
                    CD.Guardar(idVenta, IdVariante, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text))

                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo agregado", 90)
                Else
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text))

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
                If ComboBox8.SelectedIndex <= 0 Then
                    MsgBox("Debe seleccionar un almacen.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
            End If
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                If IdInventario <> 0 Or IdVariante <> 0 Or IdServicio <> 0 Then AgregaArticulo()
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
            If CD.Inventario.Contenido > 1 Then
                CostoArticulo = CostoArticulo / CD.Inventario.Contenido
            End If
            Esamortizacion = CD.Inventario.EsAmortizacion
            EsKit = CD.Inventario.EsKit
            ConsultaOn = True
            IdVariante = 0
            cmbVariante.Visible = False
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
            ComboBox8.Enabled = False
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
            cmbVariante.Visible = False
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
            ComboBox8.SelectedIndex = IdsAlmacenes.Busca(CD.IdAlmacen)
            Button4.Text = "Modificar Concepto"
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button9.Enabled = True
            'cmbtipoarticulo.Text = "A"

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
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            Saldo = B.Cliente.DaSaldoAFecha(B.Cliente.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
            CreditoCliente = B.Cliente.Credito
            SaldoaFavor = CDbl(Format(B.Cliente.DaSaldoAFavor(B.Cliente.ID), "0.00"))
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
                If (B.Cliente.Credito > 0 Or B.Cliente.CreditoDias > 0) And Op.NoPermitirFacturasdeCredito = 0 Then
                    ComboBox4.SelectedIndex = 1
                Else
                    ComboBox4.SelectedIndex = 0
                End If
                ComboBox5.SelectedIndex = IdsVendedores.Busca(B.Cliente.IdVendedor)
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
            ComboBox6.SelectedIndex = 0
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
        buscaArticuloBoton()
    End Sub
    Private Sub buscaArticuloBoton()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloProducto
        If Op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        LlenaDatosArticulo(B.Inventario)
                    Case "P"
                        LlenaDatosProducto(B.Producto)
                        'TextBox12.Focus()
                    Case "S"
                        LlenaDatosServicio(B.Servicio)
                        'TextBox12.Focus()
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
            Dim B As New frmBuscadorClases(TipodeBusqueda, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        LlenaDatosArticulo(B.Inventario)
                    Case "P"
                        LlenaDatosProducto(B.Producto)
                        'TextBox12.Focus()
                    Case "S"
                        LlenaDatosServicio(B.Servicio)
                        'TextBox12.Focus()
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
        cmbVariante.Visible = False
        PrecioU = Math.Round(a.Precio, 2)
        PrecioBase = Math.Round(a.Precio, 2) 'a.Precio
        CostoArticulo = Articulo.CostoBase
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
        cmbVariante.Visible = False
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
        ConsultaOn = True
    End Sub
    Private Sub LlenaDatosProducto(ByVal Producto As dbProductos)
        LlenaCombos("tblproductosvariantes", cmbVariante, "nombre", "nombrem", "idvariante", IdsVariantes, "idproducto=" + Producto.ID.ToString)
        cmbVariante.Visible = True
        If cmbVariante.Items.Count > 0 Then
            cmbVariante.SelectedIndex = 0
            IdVariante = IdsVariantes.Valor(0)
        Else
            MsgBox("Se necesita que un producto tenga al menos una variante.", MsgBoxStyle.Critical, GlobalNombreApp)
            IdVariante = 0
        End If
        Dim PV As New dbProductosVariantes(IdVariante, MySqlcon)
        Dim P As New dbProductos(PV.IdProducto, MySqlcon)
        Dim Cant As Double
        TextBox3.Text = P.Clave
        cmbVariante.Visible = True
        TextBox4.Text = PV.Nombre
        If Sobre = 0 Then
            TextBox8.Text = P.Iva.ToString
        Else
            TextBox8.Text = SIVA.ToString
        End If
        If IsNumeric(TextBox5.Text) Then
            Cant = CDbl(TextBox5.Text)
        Else
            TextBox5.Text = "1"
            Cant = 1
        End If
        PrecioU = PV.Precio
        PrecioBase = PV.Precio
        TextBox12.Text = PrecioU.ToString
        TextBox6.Text = CStr(Cant * PrecioU)
        ConsultaOn = False
        ComboBox1.SelectedIndex = IDsMonedas.Busca(PV.Moneda.ID)
        ConsultaOn = True
        IdVariante = PV.ID
    End Sub
    Private Sub LlenaDatosServicio(ByVal Servicio As dbServicios)
        Dim Cant As Double
        TextBox4.Text = Servicio.Detalles
        If IsNumeric(TextBox5.Text) Then
            Cant = CDbl(TextBox5.Text)
        Else
            TextBox5.Text = "1"
            Cant = 1
        End If
        Servicio.CalculaTotales(Servicio.ID)
        PrecioU = Servicio.TotalServicio
        cmbVariante.Visible = False
        TextBox6.Text = Cant * PrecioU
        PrecioBase = Servicio.TotalServicio
        ComboBox1.SelectedIndex = 0
        If Sobre = 0 Then
            TextBox8.Text = "16"
        Else
            TextBox8.Text = SIVA.ToString
        End If
        IdServicio = Servicio.ID
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
        BotonBuscar()
    End Sub
    Private Sub BotonBuscar()
        Dim f As New frmVentasConsulta(ModosDeBusqueda.Secundario, 0)
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
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"))
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
        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.Venta)
        TipoImpresora = SA.TipoImpresora
        PrintDocument1.DocumentName = "FAC-" + V.Serie + V.Folio.ToString
        If Impresora = "Bullzip PDF Printer" Then
            Dim obj As New Bullzip.PdfWriter.PdfSettings
            obj.Init()
            obj.PrinterName = Impresora
            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            obj.SetValue("ShowSettings", "never")
            obj.SetValue("ShowPDF", "yes")
            obj.SetValue("ShowSaveAS", "nofile")
            obj.SetValue("ConfirmOverwrite", "no")
            obj.SetValue("Target", "printer")
            obj.WriteSettings()
        End If
        PrintDocument1.PrinterSettings.PrinterName = Impresora
        LlenaNodosImpresion(Op.TituloOriginalFactura)
        If TipoImpresora = 0 Then
            LlenaNodos(V.IdSucursal, TiposDocumentos.Venta)
        Else
            LlenaNodos(V.IdSucursal, TiposDocumentos.VentaTicket)
        End If
        DocAImprimir = 0
        PrintDocument1.Print()

        If Op.ActivarCopiaFactura = 1 Then
            LlenaNodosImpresion(Op.TituloCopiaFactura)
            PrintDocument1.Print()
        End If
        If Op.ActivarCopia2Factura = 1 Then
            LlenaNodosImpresion(Op.TituloCopia2Factura)
            PrintDocument1.Print()
        End If
        
        If V.ISR <> 0 Or V.IvaRetenido <> 0 Then
            If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                LlenaNodosImpresionRet()
                If TipoImpresora = 0 Then
                    LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencion)
                Else
                    LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
                End If
                DocAImprimir = 1
                PrintDocument1.DocumentName = "RetFac-" + V.Serie + V.Folio.ToString
                If Impresora = "Bullzip PDF Printer" Then
                    Dim obj As New Bullzip.PdfWriter.PdfSettings
                    obj.Init()
                    obj.PrinterName = Impresora
                    obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
                    obj.SetValue("ShowSettings", "never")
                    obj.SetValue("ShowPDF", "yes")
                    obj.SetValue("ShowSaveAS", "nofile")
                    obj.SetValue("ConfirmOverwrite", "no")
                    obj.SetValue("Target", "printer")
                    obj.WriteSettings()
                End If
                PrintDocument1.Print()
            End If
        End If
        If My.Settings.impresoraPDF = "Bullzip PDF Printer" Then Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\PDFFac" + V.Serie + V.Folio.ToString + ".pdf", 1000)

        If V.Cliente.Email <> "" Then
            Try
                If MsgBox("¿Enviar factura por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    If V.Cliente.Email <> "" Then
                        Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                        'Dim O As New dbOpciones(MySqlcon)
                        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                        Dim C As String
                        C = "Eviado por: " + S.NombreFiscal + vbNewLine + "RFC: " + S.RFC + vbNewLine + "FACTURA" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Comprobante fiscal digital enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
                        If pEstado = Estados.Pendiente Then
                            M.send("Comprobante Fiscal Digital Factura: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\FAC-" + V.Serie + V.Folio.ToString + ".pdf", "")
                        Else
                            M.send("Comprobante Fiscal Digital Factura: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\FAC-" + V.Serie + V.Folio.ToString + ".pdf", RutaXml + "\FAC-" + V.Serie + V.Folio.ToString + ".xml")
                        End If

                        PopUp("Correo enviado", 90)
                    End If
                End If
            Catch ex As Exception
                MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        End If
        Dim Ss As New dbInventarioSeries(MySqlcon)
        If Ss.CantidadDeSeriesAgregadasaVenta(idVenta, 0) > 0 Then
            If MsgBox("¿Imprimir listado de series?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                ImprimirSeries()
            End If
        End If
    End Sub


    Private Sub CadenaOriginali(ByVal pEstado As Byte, ByVal pXMLAdenda As String)
        Dim en As New Encriptador
        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        V.Alterno = Op._CalculoAlterno
        If Op._noCertificado = "Predial:" Then
            V.ConPredialenXML = True
        Else
            V.ConPredialenXML = False
        End If
        Dim RutaXmlTemp As String
        Dim RutaXml As String
        Dim RutaXmlTimbrado As String
        Dim RutaXMLTimbradob As String
        Dim RutaPDF As String
        Dim MsgError As String = ""
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            Cadena = V.CreaCadenaOriginali32(idVenta, GlobalIdMoneda)
        Else
            Cadena = V.CreaCadenaOriginali(idVenta, GlobalIdMoneda)
        End If
        'en.GuardaArchivoTexto(Application.StartupPath + "\cadena.txt", Cadena, System.Text.Encoding.UTF8)
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
        RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
        Archivos.CierraDB()
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"))
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
            strXML = V.CreaXMLi32(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa)
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
                    If pXMLAdenda <> "" Then
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                    End If
                    en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
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
                    End If
                Else
                    Dim ChecarXML As String
                    ChecarXML = en.LeeArchivoTexto(RutaXmlTimbrado)
                    If ChecarXML.StartsWith("ERROR") Then
                        MsgError = ChecarXML
                        V.NoCertificadoSAT = "Error"
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
                            en.GuardaArchivoTexto(RutaXmlTimbrado, ChecarXML, System.Text.Encoding.UTF8)
                        Else
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
                    If pXMLAdenda <> "" Then
                        Timbre = Timbre.Insert(Timbre.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                    End If
                    If V.Cliente.UsaAdenda = 4 Then
                        Dim frmA As New frmAdendaPEPSICO(idVenta, False)
                        frmA.ShowDialog()
                        pXMLAdenda = frmA.strXML
                        Timbre = Timbre.Insert(Timbre.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                        frmA.Dispose()
                    End If
                    en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)

                Else
                    MsgError = Timbre
                    V.NoCertificadoSAT = "Error"
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
                If pXMLAdenda <> "" Then
                    strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                End If
                If V.Cliente.UsaAdenda = 4 Then
                    Dim frmA As New frmAdendaPEPSICO(idVenta, True)
                    frmA.ShowDialog()
                    pXMLAdenda = frmA.strXML
                    strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), pXMLAdenda)
                    frmA.Dispose()
                End If
                en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
            End If
        End If

        If V.NoCertificadoSAT <> "Error" Then
            CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
            
            Try

                Dim SA As New dbSucursalesArchivos
                Dim Impresora As String
                Dim ImpAnt As String
                Dim ImpFlujo As String
                Dim TipoAnt As Integer
                Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, False, 0, TiposDocumentos.Venta)
                ImpFlujo = SA.DaImpresoraPorTipo(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 1, TiposDocumentos.Venta)
                TipoImpresora = SA.TipoImpresora
                ImpAnt = Impresora
                TipoAnt = TipoImpresora
                If Impresora = "Bullzip PDF Printer" Then
                    Dim obj As New Bullzip.PdfWriter.PdfSettings
                    obj.Init()
                    obj.PrinterName = Impresora
                    obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
                    obj.SetValue("ShowSettings", "never")
                    obj.SetValue("ShowPDF", "yes")
                    obj.SetValue("ShowSaveAS", "nofile")
                    obj.SetValue("ConfirmOverwrite", "no")
                    obj.SetValue("Target", "printer")
                    obj.WriteSettings()
                End If

                DocAImprimir = 0
                If Op.Copiaflujoventas = 1 Then
                    Impresora = ImpFlujo
                    TipoImpresora = 1
                    LlenaNodos(V.IdSucursal, TiposDocumentos.VentaTicket)
                    LlenaNodosImpresion(Op.TituloOriginalFactura)
                    PrintDocument1.PrinterSettings.PrinterName = Impresora
                    PrintDocument1.DocumentName = "PSSFACTURA Copia-" + V.Serie + V.Folio.ToString
                    PrintDocument1.Print()
                    Impresora = ImpAnt
                    TipoImpresora = TipoAnt

                End If
                If TipoImpresora = 0 Then
                    LlenaNodos(V.IdSucursal, TiposDocumentos.Venta)
                Else
                    LlenaNodos(V.IdSucursal, TiposDocumentos.VentaTicket)
                End If
                LlenaNodosImpresion(Op.TituloOriginalFactura)
                PrintDocument1.PrinterSettings.PrinterName = Impresora
                PrintDocument1.DocumentName = "PSSFACTURA-" + V.Serie + V.Folio.ToString
                PrintDocument1.Print()
                Dim Fdp As New dbFormasdePago(V.IdFormadePago, MySqlcon)
                If (Op.ActivarCopiaFactura = 1 And Fdp.Tipo = dbFormasdePago.Tipos.Contado) Or (Op.ActivarCopiaFacturaCredito And Fdp.Tipo = dbFormasdePago.Tipos.Credito) Then
                    LlenaNodosImpresion(Op.TituloCopiaFactura)
                    PrintDocument1.Print()
                End If
                If (Op.ActivarCopia2Factura = 1 And Fdp.Tipo = dbFormasdePago.Tipos.Contado) Or (Op.ActivarCopiaFacturaCredito2 And Fdp.Tipo = dbFormasdePago.Tipos.Credito) Then
                    LlenaNodosImpresion(Op.TituloCopia2Factura)
                    PrintDocument1.Print()
                End If
                If Op._ActivarPDF = "1" Then
                    'PrintDocument1.DocumentName = "FAC-" + V.Serie + V.Folio.ToString
                    Dim SA2 As New dbSucursalesArchivos
                    Impresora = SA2.DaImpresoraActiva(V.IdSucursal, GlobalIdEmpresa, True, 0, TiposDocumentos.PDF)
                    'TipoImpresora = SA2.TipoImpresora
                    If Impresora = "Bullzip PDF Printer" Then
                        Dim obj As New Bullzip.PdfWriter.PdfSettings
                        obj.Init()
                        obj.PrinterName = Impresora
                        obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
                        obj.SetValue("ShowSettings", "never")
                        If Op._MostrarPDF = "0" Then
                            obj.SetValue("ShowPDF", "no")
                        Else
                            obj.SetValue("ShowPDF", "yes")
                        End If
                        obj.SetValue("ShowSaveAS", "nofile")
                        obj.SetValue("ConfirmOverwrite", "no")
                        obj.SetValue("Target", "printer")
                        obj.WriteSettings()
                    End If
                    PrintDocument1.PrinterSettings.PrinterName = Impresora
                    LlenaNodosImpresion(Op.TituloOriginalFactura)
                    PrintDocument1.Print()
                End If
                If V.ISR <> 0 Or V.IvaRetenido <> 0 Then
                    If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If TipoImpresora = 0 Then
                            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencion)
                        Else
                            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
                        End If
                        LlenaNodosImpresionRet()
                        DocAImprimir = 1
                        PrintDocument1.DocumentName = "RetFac-" + V.Serie + V.Folio.ToString
                        If Impresora = "Bullzip PDF Printer" Then
                            Dim obj As New Bullzip.PdfWriter.PdfSettings
                            obj.Init()
                            obj.PrinterName = Impresora
                            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
                            obj.SetValue("ShowSettings", "never")
                            obj.SetValue("ShowPDF", "yes")
                            obj.SetValue("ShowSaveAS", "nofile")
                            obj.SetValue("ConfirmOverwrite", "no")
                            obj.SetValue("Target", "printer")
                            obj.WriteSettings()
                        End If
                        PrintDocument1.Print()
                    End If
                End If
                'impresion notarios
                imprimirNotarial()
            Catch ex As Exception
                MsgBox("Error al imprimir " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
            
            'If My.Settings.impresoraPDF = "Bullzip PDF Printer" Then Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\PDFFacCFDI" + V.Serie + V.Folio.ToString + ".pdf", 1000)

            If V.Cliente.Email <> "" Then
                Try
                    If MsgBox("¿Enviar factura por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If V.Cliente.Email <> "" Then

                            Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                            'Dim O As New dbOpciones(MySqlcon)
                            Dim C As String
                            Dim RutaPDFcompleto As String = RutaPDF + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".pdf"
                            If IO.File.Exists(RutaPDF + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".pdf") = False Then
                                RutaPDFcompleto = RutaPDF + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".pdf"
                            End If
                            C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "FACTURA" + vbNewLine + "Folio: " + V.uuid + vbNewLine + vbNewLine
                            C += Op.CorreoContenido

                            If GlobalConector = 0 Then
                                If pEstado = Estados.Pendiente Then
                                    M.send("Comprobante Fiscal Digital por Internet Factura: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDFcompleto, "")
                                Else
                                    M.send("Comprobante Fiscal Digital por Internet Factura: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDFcompleto, RutaXml)
                                End If

                            Else
                                If IO.File.Exists(RutaXmlTimbrado) Then
                                    If pEstado = Estados.Pendiente Then
                                        M.send("Comprobante Fiscal Digital por Internet Factura: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDFcompleto, "")
                                    Else
                                        M.send("Comprobante Fiscal Digital por Internet Factura: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDFcompleto, RutaXmlTimbrado)
                                    End If

                                Else
                                    If pEstado = Estados.Pendiente Then
                                        M.send("Comprobante Fiscal Digital por Internet Factura: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDFcompleto, "")
                                    Else
                                        M.send("Comprobante Fiscal Digital por Internet Factura: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDFcompleto, RutaXMLTimbradob)
                                    End If

                                End If
                            End If
                            'If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = False Then PopUp("Correo enviado", 90)
                            PopUp("Correo enviado", 90)
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                End Try
            End If
            Dim Ss As New dbInventarioSeries(MySqlcon)
            If Ss.CantidadDeSeriesAgregadasaVenta(idVenta, 0) > 0 Then
                If MsgBox("¿Imprimir listado de series?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    ImprimirSeries()
                End If
            End If
        Else
            MsgBox("Ha ocurrido un error en el timbrado del la factura, intente mas tarde." + vbCrLf + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
                
            If idRemisiones.Length = 1 And idRemisiones(0) = 0 And GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirPendienteVentas, PermisosN.Secciones.Ventas) = True Then
                If MsgBox("¿Guardar factura como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    V.ModificaEstado(idVenta, Estados.Pendiente)
                    Nuevo()
                Else
                    Dim Se As New dbInventarioSeries(MySqlcon)
                    Se.QuitaSeriesAVenta(idVenta)
                    V.RegresaInventario(idVenta)
                    V.DesligaRemisiones(idVenta)
                    V.Eliminar(idVenta)
                    PopUp("Factura Eliminada", 90)
                    Nuevo()
                End If
            Else
                'Dim Ligada As Integer
                'Ligada = V.VienedeRemision(idVenta)
                V.DesligaRemisiones(idVenta)
                Dim Se As New dbInventarioSeries(MySqlcon)
                Se.QuitaSeriesAVenta(idVenta)
                V.RegresaInventario(idVenta)
                V.DesligaRemisiones(idVenta)
                V.Eliminar(idVenta)
                PopUp("Factura Eliminada", 90)
                Nuevo()
            End If
            'Error en timbrado
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
                LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            Else
                LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. Almacen")
            End If
            Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
            If ComboBox8.Items.Count > 0 Then
                If Op._TipoSelAlmacen = "0" Then
                    ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
                Else
                    ComboBox8.SelectedIndex = 0
                End If
            Else
                MsgBox("Esta sucursal no cuenta con almacenes.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
            If LlenandoDatos = False Then
                Dim Sf As New dbSucursalesFolios(MySqlcon)
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Factura, iTipoFacturacion)
                TextBox11.Text = Sf.Serie
                Eselectronica = Sf.EsElectronica
                If Sf.EsElectronica >= 1 Then
                    CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
                End If
                Dim V As New dbVentas(MySqlcon)
                TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion).ToString
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
        If IdInventario <> 0 Or IdVariante <> 0 Then
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Try
            Dim Forma As New frmBuscaDocumentoVenta(0, False, 2, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, True, True, True, 0, False, "")
            If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim V As New dbVentas(Forma.id(0), MySqlcon, Op._Sinnegativos)
                If Estado = 0 Then
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
                        Case 3
                            Dim Cv As New dbVentas(Forma.id(0), MySqlcon, Op._Sinnegativos)
                            TextBox1.Text = Cv.Cliente.Clave
                            TextBox16.Text = Cv.Descuento.ToString
                            TextBox18.Text = Cv.SobreEscribeImpLoc.ToString
                            TextBox19.Text = Cv.DescuentoG2.ToString
                            TipoCreardesde = 0
                        Case 4
                            Dim Fp As New dbFertilizantesPedido(Forma.id(0), MySqlcon)
                            idRemisiones = Forma.id
                            TextBox1.Text = Fp.Cliente.Clave
                            TipoCreardesde = 2
                    End Select
                    Guardar()
                    If Estado <> 0 Then
                        V.AgregarDetallesReferencia(idVenta, Forma.Id, Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                        ConsultaDetalles()
                        CheckBox2.Enabled = False
                    End If
                Else
                    V.AgregarDetallesReferencia(idVenta, Forma.Id, Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    ConsultaDetalles()
                End If
                NuevoConcepto()
                Button11.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    'Version Normal
    'Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage



    '    Dim O As New dbOpciones(MySqlcon)
    '    Dim en As New Encriptador
    '    'Dim XMLDoc As String
    '    Dim V As New dbVentas(idVenta, MySqlcon)
    '    Dim Fuente As New Font("Lucida Console", 10, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteB As New Font("Lucida Console", 12, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteC As New Font("Lucida Console", 8, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArial As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArialB As New Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArialC As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim Pluma As New Pen(Color.Black, 0.5)
    '    Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
    '    'Dim Fondo As New Image
    '    'XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
    '    ' XMLDoc += "<Comprobante " + vbCrLf
    '    en.Leex509(My.Settings.rutacer)
    '    'Dim TI As Double
    '    'Dim CI As Double
    '    V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
    '    'CI = TI * (V.Iva / 100) 'Pendiente

    '    Try
    '        e.Graphics.DrawImage(Image.FromFile(My.Settings.rutafacturafondo), 20, 1, 810, 1100)
    '    Catch ex As Exception

    '    End Try

    '    e.Graphics.PageUnit = GraphicsUnit.Millimeter

    '    'e.Graphics.DrawLine(Pluma, 10, 10, 10, 30)
    '    'e.Graphics.DrawLine(Pluma, 10, 30, 100, 30)
    '    Dim XEncabezado As Integer = 65
    '    e.Graphics.DrawString(Sucursal.NombreFiscal, FuenteArialC, Brushes.Black, XEncabezado, 4)
    '    e.Graphics.DrawString(Sucursal.Direccion + " " + Sucursal.NoExterior + " " + Sucursal.NoInterior, FuenteArialC, Brushes.Black, XEncabezado, 8)
    '    e.Graphics.DrawString("Col: " + Sucursal.Colonia + " " + Sucursal.Ciudad + " " + Sucursal.Estado, FuenteArialC, Brushes.Black, XEncabezado, 12)
    '    e.Graphics.DrawString("CP: " + Sucursal.CP + " RFC:" + Sucursal.RFC, FuenteArialC, Brushes.Black, XEncabezado, 16)
    '    e.Graphics.DrawString(If(Sucursal.Telefono <> "", "Tel: " + Sucursal.Telefono, "") + If(Sucursal.Email <> "", " E-mail:" + Sucursal.Email, ""), FuenteArialC, Brushes.Black, XEncabezado, 20)
    '    e.Graphics.DrawString(Sucursal.ReferenciaDomicilio, FuenteArialC, Brushes.Black, XEncabezado, 24)

    '    'e.Graphics.DrawLine(Pluma, 10, 34, 10, 55)
    '    'e.Graphics.DrawLine(Pluma, 10, 55, 100, 55)
    '    'e.Graphics.DrawString("Receptor:", Fuente, Brushes.Black, 12, 34)
    '    'Dim Nenter As String = V.Cliente.Nombre
    '    'InsertaEnters(Nenter, 60, 0, 0)

    '    Dim strF As New StringFormat
    '    strF.Alignment = StringAlignment.Near
    '    strF.LineAlignment = StringAlignment.Near
    '    Dim Rec As RectangleF
    '    Rec = New RectangleF(12, 40, 100, 100)
    '    e.Graphics.DrawString(V.Cliente.Nombre, FuenteC, Brushes.Black, Rec, strF)
    '    'e.Graphics.DrawString(Nenter, FuenteC, Brushes.Black, 12, 40)
    '    If V.Cliente.DireccionFiscal = 0 Then
    '        e.Graphics.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, FuenteC, Brushes.Black, 12, 46)
    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, FuenteC, Brushes.Black, 12, 50)
    '        e.Graphics.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, FuenteC, Brushes.Black, 12, 54)
    '    Else
    '        e.Graphics.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, FuenteC, Brushes.Black, 12, 46)
    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, FuenteC, Brushes.Black, 12, 50)
    '        e.Graphics.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, FuenteC, Brushes.Black, 12, 54)
    '    End If
    '    e.Graphics.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)


    '    e.Graphics.DrawString(V.Serie + V.Folio.ToString, Fuente, Brushes.Black, 163, 28)
    '    'e.Graphics.DrawString("No. Aprobación:", Fuente, Brushes.Black, 130, 16)
    '    e.Graphics.DrawString(O._noAprobacion + " " + O._yearAprobacion, Fuente, Brushes.Black, 163, 37)
    '    'e.Graphics.DrawString("Año Aprobación:  ", Fuente, Brushes.Black, 130, 24)
    '    'e.Graphics.DrawString(O._yearAprobacion, Fuente, Brushes.Black, 130, 28)
    '    'e.Graphics.DrawString("No. Certificado: ", Fuente, Brushes.Black, 130, 32)
    '    e.Graphics.DrawString(en.Seriex509, Fuente, Brushes.Black, 163, 44)
    '    'e.Graphics.DrawString("Fecha:", Fuente, Brushes.Black, 155, 36)
    '    e.Graphics.DrawString(Replace(V.Fecha, "/", "-") + " " + V.Hora, Fuente, Brushes.Black, 163, 52)
    '    'e.Graphics.DrawString(V.Hora, FuenteC, Brushes.Black, 187, 32)


    '    'e.Graphics.DrawString("Cant.", Fuente, Brushes.Black, 12, 60)
    '    'e.Graphics.DrawString("Unidad", Fuente, Brushes.Black, 25, 60)
    '    'e.Graphics.DrawString("Descripción", Fuente, Brushes.Black, 42, 60)
    '    'e.Graphics.DrawString("P. Unitario", Fuente, Brushes.Black, 130, 60)
    '    'e.Graphics.DrawString("Importe", Fuente, Brushes.Black, 170, 60)


    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReader(idVenta)
    '    Dim CadenaB As String
    '    Dim Y As Integer = 74
    '    Dim YB As Integer
    '    While DR.Read
    '        e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
    '        'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)

    '        YB = Y
    '        CadenaB = DR("descripcion")

    '        Y = InsertaEnters(CadenaB, 34, Y, 4)
    '        Rec = New RectangleF(43, YB, 75, 200)
    '        e.Graphics.DrawString(DR("descripcion"), Fuente, Brushes.Black, Rec, strF)
    '        'e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
    '        'e.Graphics.DrawString(, Fuente, Brushes.Black, 130, YB)
    '        e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), Fuente, Brushes.Black, 119, YB)
    '        e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
    '        e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
    '        Y += 6
    '    End While
    '    DR.Close()

    '    'Dim VP As New dbVentasProductos(MySqlcon)
    '    'DR = VP.ConsultaReader(idVenta)
    '    'While DR.Read
    '    '    e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
    '    '    'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
    '    '    YB = Y
    '    '    CadenaB = DR("descripcion")
    '    '    Y = InsertaEnters(CadenaB, 34, Y, 4)
    '    '    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
    '    '    e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
    '    '    e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
    '    '    e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
    '    '    Y += 6
    '    'End While
    '    'DR.Close()


    '    'Dim VS As New dbVentasServicios(MySqlcon)
    '    'DR = VS.ConsultaReader(idVenta)
    '    'While DR.Read
    '    '    'e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
    '    '    'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
    '    '    YB = Y
    '    '    CadenaB = DR("descripcion")
    '    '    Y = InsertaEnters(CadenaB, 34, Y, 4)
    '    '    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
    '    '    e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
    '    '    e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
    '    '    e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
    '    '    Y += 6
    '    'End While
    '    'DR.Close()
    '    Y = 170
    '    e.Graphics.DrawString("Sub total:", FuenteC, Brushes.White, 153, Y)
    '    e.Graphics.DrawString(Format(V.Subtototal, "$#,###,###0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '    If V.IdConversion <> 2 Then
    '        e.Graphics.DrawString("El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar " + vbCrLf + "frente al peso vigente al día de su pago.", FuenteArialC, Brushes.Black, 8, 158)
    '    End If
    '    '“El importe de la presente factura deberá ser pagada de acuerdo A la cotización del dólar frente al peso vigente al día de su pago”
    '    Dim Ivas As New Collection
    '    Dim IvasImporte As New Collection
    '    DR = V.DaIvas(idVenta)
    '    Dim IAnt As Double
    '    While DR.Read
    '        If Ivas.Contains(DR("iva").ToString) = False Then
    '            Ivas.Add(DR("iva"), DR("iva").ToString)
    '        End If
    '        If IvasImporte.Contains(DR("iva").ToString) = False Then
    '            IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
    '        Else
    '            IAnt = IvasImporte(DR("iva").ToString)
    '            IvasImporte.Remove(DR("iva").ToString)
    '            IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
    '        End If
    '    End While
    '    DR.Close()
    '    Y += 4
    '    For Each I As Double In Ivas
    '        'If I <> 0 Then
    '        e.Graphics.DrawString("Iva " + Format(I, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '        'End If
    '    Next
    '    If V.ISR <> 0 Then
    '        e.Graphics.DrawString("ISR " + Format(V.ISR, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '    End If
    '    If V.IvaRetenido <> 0 Then
    '        e.Graphics.DrawString("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '    End If
    '    e.Graphics.DrawString("Total: ", Fuente, Brushes.White, 153, Y)
    '    e.Graphics.DrawString(Format(V.TotalVenta, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)

    '    Dim f As New PaseLetras

    '    Y = 175

    '    If V.IdConversion = 2 Then
    '        CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), MONEDAS.MN) + " M.N."
    '    Else
    '        CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), MONEDAS.USD) + " U.S.D"
    '    End If

    '    YB = Y
    '    Y = InsertaEnters(CadenaB, 63, Y, 4)


    '    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 10, 175)



    '    Y = 198
    '    YB = Y
    '    CadenaB = Cadena
    '    Y = InsertaEnters(CadenaB, 105, Y, 4)
    '    e.Graphics.DrawString(CadenaB, FuenteC, Brushes.Black, 10, YB)

    '    'e.Graphics.DrawString("Sello:", Fuente, Brushes.Black, 10, Y + 6)

    '    Dim SelloB As String
    '    Y = 238
    '    SelloB = Sello
    '    YB = Y
    '    Y = InsertaEnters(SelloB, 105, Y, 4)




    '    e.Graphics.DrawString(SelloB, FuenteC, Brushes.Black, 10, Y)
    '    'e.Graphics.DrawString("Pago en una sola exhibición", Fuente, Brushes.Black, 10, Y + 10)
    '    'e.Graphics.DrawString("Este documento es una impresión de un comprobante fiscal digital", Fuente, Brushes.Black, 10, Y + 16)

    'End Sub
    '---------------Termina Version Normal
    '--------------------------------------------------------------------

    'Por si truena---------------------------------------------------


    'Dim V As New dbVentas(idVenta, MySqlcon)
    'Dim Fuente As New Font("Lucida Console", 10, FontStyle.Regular, GraphicsUnit.Point)
    'Dim FuenteB As New Font("Lucida Console", 12, FontStyle.Regular, GraphicsUnit.Point)
    'Dim FuenteC As New Font("Lucida Console", 8, FontStyle.Regular, GraphicsUnit.Point)
    'Dim FuenteArial As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point)
    'Dim FuenteArialB As New Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point)
    'Dim FuenteArialC As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
    'Dim FuenteArialCancela As New Font("Arial", 32, FontStyle.Regular, GraphicsUnit.Point)
    'Dim Pluma As New Pen(Color.Black, 0.5)
    'Dim PlumaRed As New Pen(Color.Red, 1)
    'Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
    ''Dim SF As New dbSucursalesFolios(MySqlcon)
    ''Dim NumeroAprobacion As String = ""
    ''Dim YearAprobacion As String = ""
    ''If SF.BuscaFolios(V.IdSucursal, dbSucursalesFolios.TipoDocumentos.Factura, 1) Then
    ''    NumeroAprobacion = SF.NoAprobacion
    ''    YearAprobacion = SF.YearAprobacion
    ''End If
    'Dim Cer As New dbSucursalesCertificados(MySqlcon)
    'Dim Certificado As String = ""
    '    If Cer.BuscaCertificado(V.IdSucursal) Then
    '        Certificado = Cer.NoSerie
    '    End If
    ''Dim Fondo As New Image
    ''XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
    '' XMLDoc += "<Comprobante " + vbCrLf
    ''en.Leex509(My.Settings.rutacer)
    ''Dim TI As Double
    ''Dim CI As Double
    '    V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
    ''CI = TI * (V.Iva / 100) 'Pendiente

    '    Try
    '        e.Graphics.DrawImage(Image.FromFile(My.Settings.rutafacturafondo), 20, 1, 810, 1100)
    '    Catch ex As Exception

    '    End Try

    '    e.Graphics.PageUnit = GraphicsUnit.Millimeter

    ''e.Graphics.DrawLine(Pluma, 10, 10, 10, 30)
    ''e.Graphics.DrawLine(Pluma, 10, 30, 100, 30)
    'Dim XEncabezado As Integer = 65
    '    e.Graphics.DrawString(Sucursal.NombreFiscal, FuenteArialC, Brushes.Black, XEncabezado, 4)
    '    e.Graphics.DrawString(Sucursal.Direccion + " " + Sucursal.NoExterior + " " + Sucursal.NoInterior, FuenteArialC, Brushes.Black, XEncabezado, 8)
    '    e.Graphics.DrawString("Col: " + Sucursal.Colonia + " " + Sucursal.Ciudad + " " + Sucursal.Estado, FuenteArialC, Brushes.Black, XEncabezado, 12)
    '    e.Graphics.DrawString("CP: " + Sucursal.CP + " RFC:" + Sucursal.RFC, FuenteArialC, Brushes.Black, XEncabezado, 16)
    '    e.Graphics.DrawString(If(Sucursal.Telefono <> "", "Tel: " + Sucursal.Telefono, "") + If(Sucursal.Email <> "", " E-mail:" + Sucursal.Email, ""), FuenteArialC, Brushes.Black, XEncabezado, 20)
    '    e.Graphics.DrawString(Sucursal.ReferenciaDomicilio, FuenteArialC, Brushes.Black, XEncabezado, 24)

    ''e.Graphics.DrawLine(Pluma, 10, 34, 10, 55)
    ''e.Graphics.DrawLine(Pluma, 10, 55, 100, 55)
    ''e.Graphics.DrawString("Receptor:", Fuente, Brushes.Black, 12, 34)
    ''Dim Nenter As String = V.Cliente.Nombre
    ''InsertaEnters(Nenter, 60, 0, 0)

    'Dim strF As New StringFormat
    '    strF.Alignment = StringAlignment.Near
    '    strF.LineAlignment = StringAlignment.Near
    'Dim Rec As RectangleF
    '    Rec = New RectangleF(12, 40, 100, 100)
    '    e.Graphics.DrawString(V.Cliente.Nombre, FuenteC, Brushes.Black, Rec, strF)
    ''e.Graphics.DrawString(Nenter, FuenteC, Brushes.Black, 12, 40)
    '    If V.Cliente.DireccionFiscal = 0 Then
    '        e.Graphics.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, FuenteC, Brushes.Black, 12, 46)
    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, FuenteC, Brushes.Black, 12, 50)
    '        e.Graphics.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, FuenteC, Brushes.Black, 12, 54)
    '    Else
    '        e.Graphics.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, FuenteC, Brushes.Black, 12, 46)
    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, FuenteC, Brushes.Black, 12, 50)
    '        e.Graphics.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, FuenteC, Brushes.Black, 12, 54)
    '    End If
    '    e.Graphics.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)


    '    e.Graphics.DrawString(V.Serie + V.Folio.ToString, Fuente, Brushes.Black, 163, 28)
    ''e.Graphics.DrawString("No. Aprobación:", Fuente, Brushes.Black, 130, 16)
    '    e.Graphics.DrawString(V.NoAprobacion + " " + V.YearAprobacion, Fuente, Brushes.Black, 163, 37)
    ''e.Graphics.DrawString("Año Aprobación:  ", Fuente, Brushes.Black, 130, 24)
    ''e.Graphics.DrawString(O._yearAprobacion, Fuente, Brushes.Black, 130, 28)
    ''e.Graphics.DrawString("No. Certificado: ", Fuente, Brushes.Black, 130, 32)
    '    e.Graphics.DrawString(Certificado, Fuente, Brushes.Black, 163, 44)
    ''e.Graphics.DrawString("Fecha:", Fuente, Brushes.Black, 155, 36)
    '    e.Graphics.DrawString(Replace(V.Fecha, "/", "-") + " " + V.Hora, Fuente, Brushes.Black, 163, 52)
    ''e.Graphics.DrawString(V.Hora, FuenteC, Brushes.Black, 187, 32)


    ''e.Graphics.DrawString("Cant.", Fuente, Brushes.Black, 12, 60)
    ''e.Graphics.DrawString("Unidad", Fuente, Brushes.Black, 25, 60)
    ''e.Graphics.DrawString("Descripción", Fuente, Brushes.Black, 42, 60)
    ''e.Graphics.DrawString("P. Unitario", Fuente, Brushes.Black, 130, 60)
    ''e.Graphics.DrawString("Importe", Fuente, Brushes.Black, 170, 60)


    'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    'Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReader(idVenta)
    'Dim CadenaB As String
    'Dim Y As Integer = 74
    'Dim YB As Integer
    '    While DR.Read
    '        e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
    ''e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)

    '        YB = Y
    '        CadenaB = DR("descripcion")

    '        Y = InsertaEnters(CadenaB, 34, Y, 4)
    '        Rec = New RectangleF(53, YB, 75, 200)
    '        e.Graphics.DrawString(DR("descripcion"), FuenteC, Brushes.Black, Rec, strF)
    ''e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
    ''e.Graphics.DrawString(, Fuente, Brushes.Black, 130, YB)
    '        e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), Fuente, Brushes.Black, 119, YB)
    '        e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
    '        e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
    '        Y += 6
    '    End While
    '    DR.Close()

    ''Dim VP As New dbVentasProductos(MySqlcon)
    ''DR = VP.ConsultaReader(idVenta)
    ''While DR.Read
    ''    e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
    ''    'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
    ''    YB = Y
    ''    CadenaB = DR("descripcion")
    ''    Y = InsertaEnters(CadenaB, 34, Y, 4)
    ''    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
    ''    e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
    ''    e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
    ''    e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
    ''    Y += 6
    ''End While
    ''DR.Close()


    ''Dim VS As New dbVentasServicios(MySqlcon)
    ''DR = VS.ConsultaReader(idVenta)
    ''While DR.Read
    ''    'e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
    ''    'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
    ''    YB = Y
    ''    CadenaB = DR("descripcion")
    ''    Y = InsertaEnters(CadenaB, 34, Y, 4)
    ''    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
    ''    e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
    ''    e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
    ''    e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
    ''    Y += 6
    ''End While
    ''DR.Close()
    '    Y = 170
    '    e.Graphics.DrawString("Sub total:", FuenteC, Brushes.White, 153, Y)
    '    e.Graphics.DrawString(Format(V.Subtototal, "$#,###,###0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '    If V.Comentario <> "" Then
    '        e.Graphics.DrawString(V.Comentario, FuenteArialC, Brushes.Black, 8, 154)
    '    End If
    '    If V.IdConversion <> 2 Then
    '        e.Graphics.DrawString("El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar " + vbCrLf + "frente al peso vigente al día de su pago.", FuenteArialC, Brushes.Black, 8, 158)
    '    End If
    ''“El importe de la presente factura deberá ser pagada de acuerdo A la cotización del dólar frente al peso vigente al día de su pago”
    'Dim Ivas As New Collection
    'Dim IvasImporte As New Collection
    '    DR = V.DaIvas(idVenta)
    'Dim IAnt As Double
    '    While DR.Read
    '        If Ivas.Contains(DR("iva").ToString) = False Then
    '            Ivas.Add(DR("iva"), DR("iva").ToString)
    '        End If
    '        If IvasImporte.Contains(DR("iva").ToString) = False Then
    '            IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
    '        Else
    '            IAnt = IvasImporte(DR("iva").ToString)
    '            IvasImporte.Remove(DR("iva").ToString)
    '            IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
    '        End If
    '    End While
    '    DR.Close()
    '    Y += 4
    '    For Each I As Double In Ivas
    ''If I <> 0 Then
    '        e.Graphics.DrawString("Iva " + Format(I, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    ''End If
    '    Next
    '    If V.ISR <> 0 Then
    '        e.Graphics.DrawString("ISR " + Format(V.ISR, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '    End If
    '    If V.IvaRetenido <> 0 Then
    '        e.Graphics.DrawString("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '    End If
    '    e.Graphics.DrawString("Total: ", Fuente, Brushes.White, 153, Y)
    '    e.Graphics.DrawString(Format(V.TotalVenta, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)

    'Dim f As New PaseLetras

    '    Y = 175

    '    If V.IdConversion = 2 Then
    '        CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), MONEDAS.MN) + " M.N."
    '    Else
    '        CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), MONEDAS.USD) + " U.S.D"
    '    End If

    '    YB = Y
    '    Y = InsertaEnters(CadenaB, 63, Y, 4)


    '    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 10, 175)



    '    Y = 198
    '    YB = Y
    '    CadenaB = Cadena
    '    Y = InsertaEnters(CadenaB, 105, Y, 4)
    '    e.Graphics.DrawString(CadenaB, FuenteC, Brushes.Black, 10, YB)

    ''e.Graphics.DrawString("Sello:", Fuente, Brushes.Black, 10, Y + 6)

    'Dim SelloB As String
    '    Y = 238
    '    SelloB = Sello
    '    YB = Y
    '    Y = InsertaEnters(SelloB, 105, Y, 4)




    '    e.Graphics.DrawString(SelloB, FuenteC, Brushes.Black, 10, Y)

    '    If V.Estado = Estados.Cancelada Then
    '        e.Graphics.DrawString("CANCELADA", FuenteArialCancela, Brushes.Red, 70, 110)
    '        e.Graphics.DrawLine(PlumaRed, 70, 107, 140, 107)
    '        e.Graphics.DrawLine(PlumaRed, 70, 125, 140, 125)
    '    End If

    ''e.Graphics.DrawString("Este documento es una impresión de un comprobante fiscal digital", Fuente, Brushes.Black, 10, Y + 16)

    'Fin Por si truena
    ''---------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------
    '---------------------------Nueva Version -----------------------------------------------------
    '-------------------------------------------------------------------------------------
    Private Sub LlenaNodosImpresion(ByVal pTitulo As String)
        Try
            Dim O As New dbOpciones(MySqlcon)
            Dim AgregaSeries As Boolean
            Dim QuitaIvaCero As Boolean
            Dim TotalDescuento As Double = 0
            If O._IVaCero = 1 Then
                QuitaIvaCero = True
            Else
                QuitaIvaCero = False
            End If
            If O._AgregaSeriesAVenta = 0 Then
                AgregaSeries = False
            Else
                AgregaSeries = True
            End If
            Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
            V.Alterno = O._CalculoAlterno
            Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
            V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
            V.DaDatosTimbrado(idVenta)
            Dim Vendedor As New dbVendedores(V.IdVendedor, MySqlcon)

            ImpND.Clear()

            ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
            ImpND.Add(New NodoImpresionN("", "direccion", Sucursal.Direccion, 0), "direccion")
            ImpND.Add(New NodoImpresionN("", "noexterior", Sucursal.NoExterior, 0), "noexterior")
            ImpND.Add(New NodoImpresionN("", "nointerior", Sucursal.NoInterior, 0), "nointerior")
            ImpND.Add(New NodoImpresionN("", "colonia", Sucursal.Colonia, 0), "colonia")
            ImpND.Add(New NodoImpresionN("", "ciudad", Sucursal.Ciudad, 0), "ciudad")
            ImpND.Add(New NodoImpresionN("", "estado", Sucursal.Estado, 0), "estado")
            ImpND.Add(New NodoImpresionN("", "cp", Sucursal.CP, 0), "cp")
            ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
            ImpND.Add(New NodoImpresionN("", "tel", Sucursal.Telefono, 0), "tel")
            ImpND.Add(New NodoImpresionN("", "email", Sucursal.Email, 0), "email")
            ImpND.Add(New NodoImpresionN("", "referencia", Sucursal.ReferenciaDomicilio, 0), "referencia")
            ImpND.Add(New NodoImpresionN("", "municipio", Sucursal.Municipio, 0), "municipio")
            ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
            ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
            ImpND.Add(New NodoImpresionN("", "telcliente", V.Cliente.Telefono, 0), "telcliente")
            ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
            ImpND.Add(New NodoImpresionN("", "regimenfiscal", Sucursal.RegimenFiscal, 0), "regimenfiscal")
            ImpND.Add(New NodoImpresionN("", "nocuenta", V.NoCuenta, 0), "nocuenta")
            ImpND.Add(New NodoImpresionN("", "vendedor", Vendedor.Nombre, 0), "vendedor")
            ImpND.Add(New NodoImpresionN("", "clavevendedor", Vendedor.Clave, 0), "clavevendedor")
            ImpND.Add(New NodoImpresionN("", "refdocumento", V.RefDocumento, 0), "refdocumento")
            ImpND.Add(New NodoImpresionN("", "adicional", V.Adicional, 0), "adicional")
            ImpND.Add(New NodoImpresionN("", "titulocopia", pTitulo, 0), "titulocopia")

            If V.Cliente.DireccionFiscal = 0 Then
                ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
                ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
                ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
                ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
                ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
                ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
                ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
                ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
                ImpND.Add(New NodoImpresionN("", "paiscliente", V.Cliente.Pais, 0), "paiscliente")
                ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio, 0), "refcliente")
            Else
                ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion2, 0), "direccioncliente")
                ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior2, 0), "noexteriorcliente")
                ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior2, 0), "nointeriorcliente")
                ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia2, 0), "coloniacliente")
                ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP2, 0), "cpcliente")
                ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad2, 0), "ciudadcliente")
                ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado2, 0), "estadocliente")
                ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio2, 0), "municipiocliente")
                ImpND.Add(New NodoImpresionN("", "paiscliente", V.Cliente.Pais2, 0), "paiscliente")
                ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
            End If
            ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")
            ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")
            ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
            ImpND.Add(New NodoImpresionN("", "folio", Format(V.Folio, "00000"), 0), "folio")
            ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
            ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
            ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")


            ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
            ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

            Sucursal.LlenaExp(V.ID, 0)
            If Sucursal.HayExp Then
                ImpND.Add(New NodoImpresionN("", "lugarexp", Sucursal.LugarExp, 0), "lugarexp")
                ImpND.Add(New NodoImpresionN("", "callerexp", Sucursal.CalleExp, 0), "callerexp")
                ImpND.Add(New NodoImpresionN("", "numeroexp", Sucursal.NumExp, 0), "numeroexp")
                If O._IgualarFechaTimbrado = 0 Then
                    'MsgBox("ok1")
                    ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.LugarExp + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
                Else
                    'MsgBox("nook1")
                    ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.LugarExp + " " + V.FechaTimbrado, 0), "lugar")
                End If

                If V.uuid = "**No Timbrado**" Or V.uuid = "" Then
                    ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.LugarExp + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugartimbrado")
                Else
                    ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.LugarExp + " " + V.FechaTimbrado, 0), "lugartimbrado")
                End If
            Else
                If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
                    ImpND.Add(New NodoImpresionN("", "lugarexp", Sucursal.Ciudad2, 0), "lugarexp")
                    ImpND.Add(New NodoImpresionN("", "callerexp", Sucursal.Direccion2, 0), "callerexp")
                    ImpND.Add(New NodoImpresionN("", "numeroexp", Sucursal.NoExterior2, 0), "numeroexp")
                    If O._IgualarFechaTimbrado = 0 Then
                        'MsgBox("ok2")
                        ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
                    Else
                        'MsgBox("nook2")
                        ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + V.FechaTimbrado, 0), "lugar")
                    End If

                    If V.uuid = "**No Timbrado**" Or V.uuid = "" Then
                        ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugartimbrado")
                    Else
                        ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + V.FechaTimbrado, 0), "lugartimbrado")
                    End If
                Else
                    ImpND.Add(New NodoImpresionN("", "lugarexp", "", 0), "lugarexp")
                    ImpND.Add(New NodoImpresionN("", "callerexp", "", 0), "callerexp")
                    ImpND.Add(New NodoImpresionN("", "numeroexp", "", 0), "numeroexp")
                    If O._IgualarFechaTimbrado = 0 Then
                        Dim Fe As String
                        Fe = Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora
                        'MsgBox("ok3 " + Fe)
                        ImpND.Add(New NodoImpresionN("", "lugar", Fe, 0), "lugar")
                    Else
                        'MsgBox("nook3")
                        ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + V.FechaTimbrado, 0), "lugar")
                    End If
                    If V.uuid = "**No Timbrado**" Or V.uuid = "" Then

                        ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugartimbrado")
                    Else
                        ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad + " " + Sucursal.Estado + " " + V.FechaTimbrado, 0), "lugartimbrado")
                    End If

                End If
            End If

            ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
            ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
            ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")

            ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
            ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
            CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
            ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")
            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            Dim VI As New dbVentasInventario(MySqlcon)
            DR = VI.ConsultaReader(idVenta, AgregaSeries, Op._DetalleKits, 1, Op._OrdenUbicacion)
            ImpNDD.Clear()
            Dim TotalIEPS As Double = 0
            Dim TotalIVARetenido As Double = 0
            CuantosRenglones = 0
            Dim brinca As Boolean
            Dim Cont As Integer = 0
            Dim CodigoBarras As iTextSharp.text.pdf.Barcode128 = New iTextSharp.text.pdf.Barcode128
            While DR.Read
                brinca = False
                If Op._Sinnegativos = "1" And DR("precio") < 0 Then brinca = True
                If DR("esrevdev") = 1 Then brinca = True
                If brinca = False Then
                    If DR("cantidad") <> 0 Then
                        ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
                    Else
                        ImpNDD.Add(New NodoImpresionN("", "clave", "", 0), "clave" + Format(Cont, "000"))
                    End If
                    If DR("cantidad") <> 0 Then
                        ImpNDD.Add(New NodoImpresionN("", "clave2", DR("clave2"), 0), "clave2" + Format(Cont, "000"))
                    Else
                        ImpNDD.Add(New NodoImpresionN("", "clave2", "", 0), "clave2" + Format(Cont, "000"))
                    End If
                    Dim Nimagen As NodoImpresionN
                    Nimagen = New NodoImpresionN("", "codigobarras1", "", 0)
                    CodigoBarras.Code = DR("clave")
                    Nimagen.Imagen = CodigoBarras.CreateDrawingImage(Color.Black, Color.White)
                    ImpNDD.Add(Nimagen, "codigobarras1" + Format(Cont, "000"))

                    Nimagen = New NodoImpresionN("", "codigobarras2", "", 0)
                    CodigoBarras.Code = DR("clave2")
                    Nimagen.Imagen = CodigoBarras.CreateDrawingImage(Color.Black, Color.White)
                    ImpNDD.Add(Nimagen, "codigobarras2" + Format(Cont, "000"))



                    'Dim image As Image =
                    'CodigoBidimensional = New Bitmap(image, New Size(300, 200))


                    TotalIEPS += Double.Parse(DR("IEPS").ToString())
                    TotalIVARetenido += Double.Parse(DR("IVARetenido").ToString())

                    ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "ubicacion", DR("ubicacion"), 0), "ubicacion" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "predial", DR("predial"), 0), "predial" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "extra", DR("extra"), 0), "extra" + Format(Cont, "000"))
                    If DR("iva") = 0 Then
                        ImpNDD.Add(New NodoImpresionN("", "tieneiva", "", 0), "tieneiva" + Format(Cont, "000"))
                    Else
                        ImpNDD.Add(New NodoImpresionN("", "tieneiva", "*", 0), "tieneiva" + Format(Cont, "000"))
                    End If
                    If DR("cantidad") <> 0 Then
                        ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidadm"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipom"), 0), "tipocantidad" + Format(Cont, "000"))
                    Else
                        ImpNDD.Add(New NodoImpresionN("", "cantidad", "", 0), "cantidad" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "tipocantidad", "", 0), "tipocantidad" + Format(Cont, "000"))
                    End If
                    If DR("cantidad") <> 0 Then
                        ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidadm"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "preciouiva", Format((DR("precio") / DR("cantidadm")) * (1 + DR("iva") / 100), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciouiva" + Format(Cont, "000"))
                    Else
                        ImpNDD.Add(New NodoImpresionN("", "preciou", "", 0), "preciou" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "preciouiva", "", 0), "preciouiva" + Format(Cont, "000"))
                    End If
                    If DR("cantidad") <> 0 Then
                        ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importe" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(DR("precio") * (1 + DR("iva") / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "ieps", Format(DR("precio") * DR("IEPS") / 100, O._formatoImporte).PadLeft(O.EspacioImporte), 0), "ieps" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", Format(DR("precio") * DR("IVARetenido") / 100, O._formatoImporte).PadLeft(O.EspacioImporte), 0), "ivaRetenido" + Format(Cont, "000"))
                    Else
                        ImpNDD.Add(New NodoImpresionN("", "importe", "", 0), "importe" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "importeiva", "", 0), "importeiva" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "ieps", "", 0), "ieps" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", "", 0), "ivaRetenido" + Format(Cont, "000"))
                    End If

                    If DR("cantidadm") <> 0 And DR("descuento") <> 0 Then
                        Dim Desc As Double
                        Desc = (DR("precio") / (1 - DR("descuento") / 100))
                        TotalDescuento += Desc - DR("precio")
                        ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(O.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - DR("precio"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidadm"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "importesindesc", Format(Desc, O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "importesindesc" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "descuentocantuni", Format((Desc / DR("cantidadm")) * (DR("descuento") / 100), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocantuni" + Format(Cont, "000"))
                        'Vo = Vd / ( 1 - (Por/100))
                    Else
                        ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))
                        ImpNDD.Add(New NodoImpresionN("", "descuentocantuni", "", 0), "descuentocantuni" + Format(Cont, "000"))
                        If DR("cantidad") <> 0 Then
                            ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(DR("precio") / DR("cantidadm"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                            ImpNDD.Add(New NodoImpresionN("", "importesindesc", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importesindesc" + Format(Cont, "000"))
                        Else
                            ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
                            ImpNDD.Add(New NodoImpresionN("", "importesindesc", "", 0), "importesindesc" + Format(Cont, "000"))
                        End If
                    End If
                    CuantosRenglones += 1
                    Cont += 1
                End If
            End While
            DR.Close()

            ImpND.Add(New NodoImpresionN("", "Totalieps", Format(V.TotalIEPS, O._formatoIva).PadLeft(O.EspacioIva), 0), "Totalieps")
            ImpND.Add(New NodoImpresionN("", "TotalivaRetenido", Format(V.TotalIvaRetenidoConceptos, O._formatoIva).PadLeft(O.EspacioIva), 0), "TotalivaRetenido")

            If V.PorSurtir = 0 Then
                ImpND.Add(New NodoImpresionN("", "porsurtir", "", 0), "porsurtir")
            Else
                ImpND.Add(New NodoImpresionN("", "porsurtir", "POR SURTIR", 0), "porsurtir")
            End If
            ImpND.Add(New NodoImpresionN("", "peso", Format(V.TotalPeso, "#,##0.00") + "Kg.", 0), "peso")
            ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")

            If Op._Sinnegativos = "0" Then
                ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtototal + V.TotalDescuento - V.TotalRevdev, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsindesc")
                ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtototal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")
            Else
                ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtototal + V.TotalDescuento - V.TotalNegativosSinIVA - V.TotalRevdev, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsindesc")
                'Dim Subb As Double
                'Subb = V.Subtototal - V.TotalNegativosSinIVA
                ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtototal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")
            End If
            ImpND.Add(New NodoImpresionN("", "totalofertas", Format(V.TotalOfertas, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totalofertas")
            ImpND.Add(New NodoImpresionN("", "totaldesc", Format(V.TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totaldesc")
            ImpND.Add(New NodoImpresionN("", "totaldesc2", Format(V.DescuentoG2, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totaldesc2")
            ImpND.Add(New NodoImpresionN("", "subtotalsinret", Format(V.Subtototal + V.TotalIva, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsinret")
            ImpND.Add(New NodoImpresionN("", "totalcantidad", V.DaTotalCantidad(idVenta).ToString, 0), "totalcantidad")
            ImpND.Add(New NodoImpresionN("", "totalnegativos", Format((V.TotalNegativosSinIVA * -1) + V.TotalRevdev, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totalnegativos")
            Dim Ivas As New Collection
            Dim IvasImporte As New Collection
            DR = V.DaIvas(idVenta)
            ImpNDDi.Clear()
            ImpNDi2.Clear()
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
            '            TotalesXIVa = "SUBTOTAL GRAVADOS    " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
            '        Else
            '            TotalesXIVa = "SUBTOTAL NO GRAVADOS " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
            '        End If
            '    Else
            '        If I <> 0 Then
            '            TotalesXIVa += vbCrLf + "SUBTOTAL GRAVADOS    " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
            '        Else
            '            TotalesXIVa += vbCrLf + "SUBTOTAL NO GRAVADOS " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
            '        End If
            '    End If
            'Next


            TotalesXIVa = "SUBTOTAL GRAVADOS:    " + Format(V.DaTotalGravado(V.ID), O._formatoTotal)
            TotalesXIVa += vbCrLf + "SUBTOTAL NO GRAVADOS: " + Format(V.DaTotalNoGravado(V.ID), O._formatoTotal)


            ImpND.Add(New NodoImpresionN("", "totalxiva", TotalesXIVa, 0), "totalxiva")
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
                ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
                If V.IdConversion = 2 Then
                    IvasConversion += "Iva " + Format(I, "#0.00") + "%: " + Format(IvasImporte(I.ToString) / V.TipodeCambio, O._formatoIva).PadLeft(O.EspacioIva) + vbCrLf
                Else
                    IvasConversion += "Iva " + Format(I, "#0.00") + "%: " + Format(IvasImporte(I.ToString) * V.TipodeCambio, O._formatoIva).PadLeft(O.EspacioIva) + vbCrLf
                End If
                ImpNDi2.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
                Cont += 1
            Next

            If V.ImpLocales.Count > 0 Then
                ImpND.Add(New NodoImpresionN("", "implocales", V.DaTextoImpLocales(O._formatoIva, O.EspacioIva), 0), "implocales")
            Else
                ImpND.Add(New NodoImpresionN("", "implocales", "", 0), "implocales")
            End If


            If V.ISR <> 0 Then
                ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
                ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, O._formatoIva).PadLeft(O.EspacioIva), 0), "isr")
                Cont += 1
            Else
                ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, O._formatoIva).PadLeft(O.EspacioIva), 0), "isr")
            End If
            If V.IvaRetenido <> 0 Then
                ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
                ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(O.EspacioIva), 0), "ivaret")
                Cont += 1
            Else
                ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(O.EspacioIva), 0), "ivaret")
            End If

            ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, O._formatoTotal).PadLeft(O.Espaciototal), 0), "total")
            If V.IdConversion = 2 Then
                ImpND.Add(New NodoImpresionN("", "Total C:", Format(V.TotalVenta / V.TipodeCambio, O._formatoTotal).PadLeft(O.Espaciototal), 0), "totalcon")
                ImpND.Add(New NodoImpresionN("", "Subtotal C:", Format(V.Subtototal / V.TipodeCambio, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalcon")
            Else
                ImpND.Add(New NodoImpresionN("", "Total C:", Format(V.TotalVenta * V.TipodeCambio, O._formatoTotal).PadLeft(O.Espaciototal), 0), "totalcon")
                ImpND.Add(New NodoImpresionN("", "Subtotal C:", Format(V.Subtototal * V.TipodeCambio, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalcon")
            End If
            ImpND.Add(New NodoImpresionN("", "ivacon", IvasConversion, 0), "ivacon")


            ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalSinRetencion + V.DescuentoG2, O._formatoTotal).PadLeft(O.Espaciototal), 0), "totalsil")

            Dim f As New StringFunctions
            Dim CL As New CLetras
            'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion), 0), "totalletra")
            ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(Format(V.TotalVenta, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletra")
            ImpND.Add(New NodoImpresionN("", "totalletrasil", CL.LetrasM(Format(V.TotalSinRetencion, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletrasil")
            ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
            ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
            Dim FP As New dbFormasdePago(V.IdFormadePago, MySqlcon)
            If FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
                ImpND.Add(New NodoImpresionN("", "titulo", O.TituloParcialidad, 0), "titulo")
            Else
                ImpND.Add(New NodoImpresionN("", "titulo", O.TituloFactura, 0), "titulo")
            End If
            'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
            If V.IdFormadePago <> 98 Then
                ImpND.Add(New NodoImpresionN("", "metodopago", FP.Nombre, 0), "metodopago")
                If FP.Tipo <> dbFormasdePago.Tipos.Parcialidad Then
                    ImpND.Add(New NodoImpresionN("", "formadepago", "PAGO EN UNA SOLA EXHIBICIÓN", 0), "formadepago")
                    ImpND.Add(New NodoImpresionN("", "folioorigen", "", 0), "folioorigen")
                    ImpND.Add(New NodoImpresionN("", "serieorigen", "", 0), "serieorigen")
                    ImpND.Add(New NodoImpresionN("", "montoorigen", "", 0), "montoorigen")
                    ImpND.Add(New NodoImpresionN("", "fechaorigen", "", 0), "fechaorigen")
                    ImpND.Add(New NodoImpresionN("", "uuidorigen", "", 0), "uuidorigen")
                Else
                    V.ObtenerFacturaOriginal(V.IdVentaOrigen)
                    If V.Parcialidades <> 1 Then
                        ImpND.Add(New NodoImpresionN("", "formadepago", "PARCIALIDAD " + V.Parcialidad.ToString + " DE " + V.Parcialidades.ToString, 0), "formadepago")
                    Else
                        ImpND.Add(New NodoImpresionN("", "formadepago", "PARCIALIDAD " + V.Parcialidad.ToString, 0), "formadepago")
                    End If
                    ImpND.Add(New NodoImpresionN("", "folioorigen", Format(V.FolioOrigen, "00000"), 0), "folioorigen")
                    ImpND.Add(New NodoImpresionN("", "serieorigen", V.SerieOrigen, 0), "serieorigen")
                    ImpND.Add(New NodoImpresionN("", "montoorigen", Format(V.MontoOrigen, O._formatoTotal), 0), "montoorigen")
                    ImpND.Add(New NodoImpresionN("", "fechaorigen", V.FechaOrigen, 0), "fechaorigen")
                    ImpND.Add(New NodoImpresionN("", "uuidorigen", V.FolioUUIDOrigen, 0), "uuidorigen")
                End If
            Else
                ImpND.Add(New NodoImpresionN("", "metodopago", "NO IDENTIFICADO", 0), "metodopago")
                If V.Parcialidades <> 1 Then
                    ImpND.Add(New NodoImpresionN("", "formadepago", "PAGO EN " + V.Parcialidades.ToString + " PARCIALIDADES", 0), "formadepago")
                Else
                    ImpND.Add(New NodoImpresionN("", "formadepago", "PAGO EN PARCIALIDADES", 0), "formadepago")
                End If
                ImpND.Add(New NodoImpresionN("", "folioorigen", "", 0), "folioorigen")
                ImpND.Add(New NodoImpresionN("", "serieorigen", "", 0), "serieorigen")
                ImpND.Add(New NodoImpresionN("", "montoorigen", "", 0), "montoorigen")
                ImpND.Add(New NodoImpresionN("", "fechaorigen", "", 0), "fechaorigen")
                ImpND.Add(New NodoImpresionN("", "uuidorigen", "", 0), "uuidorigen")
            End If

            'Else
            'ImpND.Add(New NodoImpresionN("", "metodopago", "No Identificado", 0), "metodopago")
            'End If
            If FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
                If V.FormaPagoNA = 0 Then
                    ImpND.Add(New NodoImpresionN("", "condicionpago", "CONTADO", 0), "condicionpago")
                Else
                    ImpND.Add(New NodoImpresionN("", "condicionpago", "N/A", 0), "condicionpago")
                End If
                ImpND.Add(New NodoImpresionN("", "diascredito", "", 0), "diascredito")
                ImpND.Add(New NodoImpresionN("", "limitecredito", "", 0), "limitecredito")
            Else
                If V.FormaPagoNA = 0 Then
                    ImpND.Add(New NodoImpresionN("", "condicionpago", "CRÉDITO", 0), "condicionpago")
                Else
                    ImpND.Add(New NodoImpresionN("", "condicionpago", "N/A", 0), "condicionpago")
                End If
                ImpND.Add(New NodoImpresionN("", "diascredito", V.Cliente.CreditoDias.ToString + " Días.", 0), "diascredito")
                ImpND.Add(New NodoImpresionN("", "limitecredito", Format(DateAdd(DateInterval.Day, V.Cliente.CreditoDias, CDate(V.Fecha)), "yyyy-MM-dd"), 0), "limitecredito")
                End If


                Dim CP As New dbVentasCartaPorte(V.ID, MySqlcon)
                If CP.Origen = "Nohay" Then
                    ImpND.Add(New NodoImpresionN("", "cporigen", "", 0), "cporigen")
                    ImpND.Add(New NodoImpresionN("", "cpdestino", "", 0), "cpdestino")
                    ImpND.Add(New NodoImpresionN("", "cpchofer", "", 0), "cpchofer")
                    ImpND.Add(New NodoImpresionN("", "cpmercancia", "", 0), "cpmercancia")
                    ImpND.Add(New NodoImpresionN("", "cpmatricula", "", 0), "cpmatricula")
                    ImpND.Add(New NodoImpresionN("", "cppeso", "", 0), "cppeso")
                    ImpND.Add(New NodoImpresionN("", "cpfecha", "", 0), "cpfecha")
                    ImpND.Add(New NodoImpresionN("", "cpvalorunitario", "", 0), "cpvalorunitario")
                    ImpND.Add(New NodoImpresionN("", "cpvalordeclarado", "", 0), "cpvalordeclarado")
                    ImpND.Add(New NodoImpresionN("", "cpreferencia", "", 0), "cpreferencia")
                    ImpND.Add(New NodoImpresionN("", "cppedimento", "", 0), "cppedimento")
                    ImpND.Add(New NodoImpresionN("", "cppedimentofecha", "", 0), "cppedimentofecha")
                Else
                    ImpND.Add(New NodoImpresionN("", "cporigen", CP.Origen, 0), "cporigen")
                    ImpND.Add(New NodoImpresionN("", "cpdestino", CP.Destino, 0), "cpdestino")
                    ImpND.Add(New NodoImpresionN("", "cpchofer", CP.Chofer, 0), "cpchofer")
                    ImpND.Add(New NodoImpresionN("", "cpmercancia", CP.Mercancia, 0), "cpmercancia")
                    ImpND.Add(New NodoImpresionN("", "cpmatricula", CP.Matricula, 0), "cpmatricula")
                    ImpND.Add(New NodoImpresionN("", "cppeso", CP.Peso, 0), "cppeso")
                    ImpND.Add(New NodoImpresionN("", "cpfecha", CP.Fecha, 0), "cpfecha")
                    ImpND.Add(New NodoImpresionN("", "cpvalorunitario", CP.ValorUnitario, 0), "cpvalorunitario")
                    ImpND.Add(New NodoImpresionN("", "cpvalordeclarado", CP.ValorDeclarado, 0), "cpvalordeclarado")
                    ImpND.Add(New NodoImpresionN("", "cpreferencia", CP.Referencia, 0), "cpreferencia")
                    ImpND.Add(New NodoImpresionN("", "cppedimento", CP.Pedimento, 0), "cppedimento")
                    ImpND.Add(New NodoImpresionN("", "cppedimentofecha", CP.FechaPedimento, 0), "cppedimentofecha")
                End If

                If V.IdConversion = 2 Then
                    ImpND.Add(New NodoImpresionN("", "leyendadolar", "", 0), "leyendadolar")
                Else
                    ImpND.Add(New NodoImpresionN("", "leyendadolar", "El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar a la venta frente al peso vigente al día de su pago.", 0), "leyendadolar")
                End If
                If V.Estado = Estados.Cancelada Then
                    ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
                Else
                    ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
                End If
                Posicion = 0
                Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
                CodigoBidimensional = CB.Encode("?re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, "0000000000.000000") + "&id=" + V.uuid, System.Text.Encoding.UTF8)
                NumeroPagina = 1
        Catch ex As Exception
            MsgBox("P1 " + ex.Message + " E=" + ImpND.Item(ImpND.Count - 1).DataPropertyName, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub LlenaNodosImpresionRet()
        Dim O As New dbOpciones(MySqlcon)
        'Dim AgregaSeries As Boolean
        'Dim QuitaIvaCero As Boolean
        'If O._IVaCero = 1 Then
        '    QuitaIvaCero = True
        'Else
        '    QuitaIvaCero = False
        'End If
        'If O._AgregaSeriesAVenta = 0 Then
        '    AgregaSeries = False
        'Else
        '    AgregaSeries = True
        'End If
        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
        'V.DaDatosTimbrado(idVenta)
        ImpND.Clear()

        ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
        ImpND.Add(New NodoImpresionN("", "nombrefiscal2", Sucursal.NombreFiscal, 0), "nombrefiscal2")
        'ImpND.Add(New NodoImpresionN("", "direccion", Sucursal.Direccion, 0), "direccion")
        'ImpND.Add(New NodoImpresionN("", "noexterior", Sucursal.NoExterior, 0), "noexterior")
        'ImpND.Add(New NodoImpresionN("", "nointerior", Sucursal.NoInterior, 0), "nointerior")
        'ImpND.Add(New NodoImpresionN("", "colonia", Sucursal.Colonia, 0), "colonia")
        'ImpND.Add(New NodoImpresionN("", "ciudad", Sucursal.Ciudad, 0), "ciudad")
        'ImpND.Add(New NodoImpresionN("", "estado", Sucursal.Estado, 0), "estado")
        'ImpND.Add(New NodoImpresionN("", "cp", Sucursal.CP, 0), "cp")
        ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
        ImpND.Add(New NodoImpresionN("", "curp", Sucursal.CURP, 0), "curp")
        ImpND.Add(New NodoImpresionN("", "mesini", Format(CDate(V.Fecha), "MM"), 0), "mesini")
        ImpND.Add(New NodoImpresionN("", "mesfin", Format(CDate(V.Fecha), "MM"), 0), "mesfin")
        ImpND.Add(New NodoImpresionN("", "ejercicio", Format(CDate(V.Fecha), "yyyy"), 0), "ejercicio")

        ImpND.Add(New NodoImpresionN("", "total1", Format(V.Subtototal, O._formatoTotal).PadLeft(13), 0), "total1")
        ImpND.Add(New NodoImpresionN("", "total2", Format(V.Subtototal, O._formatoTotal).PadLeft(13), 0), "total2")
        ImpND.Add(New NodoImpresionN("", "total3", "$0.00", 0), "total3")


        'If V.ISR <> 0 Then
        'ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, O._formatoIva).PadLeft(13), 0), "iva" + Format(Cont, "00"))
        ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, O._formatoIva).PadLeft(13), 0), "isr")
        'Cont += 1
        'End If
        'If V.IvaRetenido <> 0 Then
        'ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(13), 0), "iva" + Format(Cont, "00"))
        ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(13), 0), "ivaret")
        'Cont += 1
        'End If
        ImpND.Add(New NodoImpresionN("", "ieps", "$0.00", 0), "ieps")

        'ImpND.Add(New NodoImpresionN("", "tel", Sucursal.Telefono, 0), "tel")
        'ImpND.Add(New NodoImpresionN("", "email", Sucursal.Email, 0), "email")
        'ImpND.Add(New NodoImpresionN("", "referencia", Sucursal.ReferenciaDomicilio, 0), "referencia")
        'ImpND.Add(New NodoImpresionN("", "municipio", Sucursal.Municipio, 0), "municipio")
        ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        'ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
        'ImpND.Add(New NodoImpresionN("", "telcliente", V.Cliente.Telefono, 0), "telcliente")
        'ImpND.Add(New NodoImpresionN("", "regimenfiscal", Sucursal.RegimenFiscal, 0), "regimenfiscal")
        'ImpND.Add(New NodoImpresionN("", "nocuenta", V.NoCuenta, 0), "nocuenta")


        'If V.Cliente.DireccionFiscal = 0 Then
        '    ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
        '    ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
        '    ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
        '    ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
        '    ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
        '    ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
        '    ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
        '    ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
        '    ImpND.Add(New NodoImpresionN("", "paiscliente", V.Cliente.Pais, 0), "paiscliente")
        'Else
        '    ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion2, 0), "direccioncliente")
        '    ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior2, 0), "noexteriorcliente")
        '    ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior2, 0), "nointeriorcliente")
        '    ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia2, 0), "coloniacliente")
        '    ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP2, 0), "cpcliente")
        '    ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad2, 0), "ciudadcliente")
        '    ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado2, 0), "estadocliente")
        '    ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio2, 0), "municipiocliente")
        '    ImpND.Add(New NodoImpresionN("", "paiscliente", V.Cliente.Pais2, 0), "paiscliente")
        'End If



        ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")
        ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")
        ImpND.Add(New NodoImpresionN("", "repcliente", V.Cliente.Representante, 0), "repcliente")
        ImpND.Add(New NodoImpresionN("", "repcliente2", V.Cliente.Representante, 0), "repcliente2")
        ImpND.Add(New NodoImpresionN("", "rfcrepcliente", V.Cliente.RepresentanteRFC, 0), "rfcrepcliente")
        ImpND.Add(New NodoImpresionN("", "curprepcliente", V.Cliente.RepresentanteRegistro, 0), "curprepcliente")

        'ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        'ImpND.Add(New NodoImpresionN("", "folio", Format(V.Folio, "00000"), 0), "folio")
        'ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        'ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        'ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        'ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
        'ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        'If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
        '    ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'Else
        '    ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'End If

        'ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        'ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        'ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")

        'ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        'ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        'CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
        'ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")


        'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Dim VI As New dbVentasInventario(MySqlcon)
        'DR = VI.ConsultaReader(idVenta, AgregaSeries)
        'ImpNDD.Clear()
        'CuantosRenglones = 0
        'Dim Cont As Integer = 0
        'While DR.Read
        '    If DR("cantidad") <> 0 Then
        '        ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
        '    Else
        '        ImpNDD.Add(New NodoImpresionN("", "clave", "", 0), "clave" + Format(Cont, "000"))
        '    End If
        '    ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))

        '    If DR("cantidad") <> 0 Then
        '        ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidadm"), O._formatocantidad).PadLeft(8), 0), "cantidad" + Format(Cont, "000"))
        '        ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipom"), 0), "tipocantidad" + Format(Cont, "000"))
        '    Else
        '        ImpNDD.Add(New NodoImpresionN("", "cantidad", "", 0), "cantidad" + Format(Cont, "000"))
        '        ImpNDD.Add(New NodoImpresionN("", "tipocantidad", "", 0), "tipocantidad" + Format(Cont, "000"))
        '    End If
        '    If DR("cantidad") <> 0 Then
        '        ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidadm"), O._formatoPrecioU).PadLeft(13), 0), "preciou" + Format(Cont, "000"))
        '    Else
        '        ImpNDD.Add(New NodoImpresionN("", "preciou", "", 0), "preciou" + Format(Cont, "000"))
        '    End If
        '    If DR("precio") <> 0 Then
        '        ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(13), 0), "importe" + Format(Cont, "000"))
        '    Else
        '        ImpNDD.Add(New NodoImpresionN("", "importe", "", 0), "importe" + Format(Cont, "000"))
        '    End If

        '    CuantosRenglones += 1
        '    Cont += 1
        'End While
        'DR.Close()


        'ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        'ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtototal, O._formatoSubtotal).PadLeft(13), 0), "subtotal")
        'ImpND.Add(New NodoImpresionN("", "subtotalsinret", Format(V.Subtototal + V.TotalIva, O._formatoSubtotal).PadLeft(13), 0), "subtotalsinret")
        'Dim Ivas As New Collection
        'Dim IvasImporte As New Collection
        'DR = V.DaIvas(idVenta)
        'ImpNDDi.Clear()
        'ImpNDi2.Clear()
        'Dim IAnt As Double
        ''If Ivas.Count < 2 Then QuitaIvaCero = False
        'While DR.Read
        '    If DR("iva") = 0 And QuitaIvaCero Then
        '    Else
        '        If Ivas.Contains(DR("iva").ToString) = False Then
        '            Ivas.Add(DR("iva"), DR("iva").ToString)
        '        End If
        '        If IvasImporte.Contains(DR("iva").ToString) = False Then
        '            IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
        '        Else
        '            IAnt = IvasImporte(DR("iva").ToString)
        '            IvasImporte.Remove(DR("iva").ToString)
        '            IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
        '        End If
        '    End If

        'End While
        'DR.Close()
        'Cont = 0

        'For Each I As Double In Ivas
        '    'If I = 0 And QuitaIvaCero Then

        '    'Else
        '    ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    ImpNDi2.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    'End If

        '    Cont += 1
        'Next
        'If V.ISR <> 0 Then
        '    ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, O._formatoIva).PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, O._formatoIva).PadLeft(13), 0), "isr")
        '    Cont += 1
        'End If
        'If V.IvaRetenido <> 0 Then
        '    ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(13), 0), "ivaret")
        '    Cont += 1
        'End If

        'ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, O._formatoTotal).PadLeft(13), 0), "total")

        'Dim f As New StringFunctions
        'Dim CL As New CLetras
        ''ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion), 0), "totalletra")
        'ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(Format(V.TotalVenta, "0.00"), V.IdConversion), 0), "totalletra")
        'ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
        'ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
        'Dim FP As New dbFormasdePago(V.IdFormadePago, MySqlcon)
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        '    ImpND.Add(New NodoImpresionN("", "condicionpago", "Contado", 0), "condicionpago")
        '    ImpND.Add(New NodoImpresionN("", "diascredito", "", 0), "diascredito")
        '    ImpND.Add(New NodoImpresionN("", "limitecredito", "", 0), "limitecredito")
        'Else
        '    ImpND.Add(New NodoImpresionN("", "condicionpago", "Crédito", 0), "condicionpago")
        '    ImpND.Add(New NodoImpresionN("", "diascredito", V.Cliente.CreditoDias.ToString + " Días.", 0), "diascredito")
        '    ImpND.Add(New NodoImpresionN("", "limitecredito", Format(DateAdd(DateInterval.Day, V.Cliente.CreditoDias, CDate(V.Fecha)), "yyyy-MM-dd"), 0), "limitecredito")
        'End If

        'If V.IdConversion = 2 Then
        '    ImpND.Add(New NodoImpresionN("", "leyendadolar", "", 0), "leyendadolar")
        'Else
        '    ImpND.Add(New NodoImpresionN("", "leyendadolar", "El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar a la venta frente al peso vigente al día de su pago.", 0), "leyendadolar")
        'End If

        'Posicion = 0
        'Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        'CodigoBidimensional = CB.Encode("?&re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, O._formatoTotal) + "&id=" + V.uuid, System.Text.Encoding.Default)
        'NumeroPagina = 1
    End Sub

    'Private Sub LlenaNodosImpresionSeries()
    '    Dim O As New dbOpciones(MySqlcon)
    '    Dim V As New dbVentas(idVenta, MySqlcon)
    '    'Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
    '    V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
    '    'V.DaDatosTimbrado(idVenta)
    '    ImpNDS.Clear()
    '    ImpNDS.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
    '    ImpNDS.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")


    '    ImpNDS.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")



    '    ImpNDS.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
    '    ImpNDS.Add(New NodoImpresionN("", "folio", V.Folio.ToString, 0), "folio")

    '    ImpNDS.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
    '    ImpNDS.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")
    '    ImpNDS.Add(New NodoImpresionN("", "documento", "Factura", 0), "documento")



    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReaderSeries(idVenta)
    '    ImpNDSD.Clear()
    '    CuantosRenglones = 0
    '    Dim Cont As Integer = 0
    '    While DR.Read
    '        'If DR("cantidad") <> 0 Then
    '        ImpNDSD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
    '        'Else
    '        ImpNDSD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(8), 0), "cantidad" + Format(Cont, "000"))
    '        'End If
    '        ImpNDSD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
    '        ImpNDSD.Add(New NodoImpresionN("", "noserie", DR("noserie"), 0), "noserie" + Format(Cont, "000"))
    '        ImpNDSD.Add(New NodoImpresionN("", "fechagarantia", DR("fechagarantia"), 0), "fechagarantia" + Format(Cont, "000"))

    '        'If DR("cantidad") <> 0 Then
    '        '    
    '        '    ImpNDSD.Add(New NodoImpresionN("", "tipocantidad", DR("tipocantidad"), 0), "tipocantidad" + Format(Cont, "000"))
    '        'Else
    '        '    ImpNDSD.Add(New NodoImpresionN("", "cantidad", "", 0), "cantidad" + Format(Cont, "000"))
    '        '    ImpNDSD.Add(New NodoImpresionN("", "tipocantidad", "", 0), "tipocantidad" + Format(Cont, "000"))
    '        'End If
    '        'If DR("cantidad") <> 0 Then
    '        '    ImpNDSD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidad"), O._formatoPrecioU).PadLeft(13), 0), "preciou" + Format(Cont, "000"))
    '        'Else
    '        '    ImpNDSD.Add(New NodoImpresionN("", "preciou", "", 0), "preciou" + Format(Cont, "000"))
    '        'End If
    '        'If DR("precio") <> 0 Then
    '        '    ImpNDSD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(13), 0), "importe" + Format(Cont, "000"))
    '        'Else
    '        '    ImpNDSD.Add(New NodoImpresionN("", "importe", "", 0), "importe" + Format(Cont, "000"))
    '        'End If

    '        CuantosRenglones += 1
    '        Cont += 1
    '    End While
    '    DR.Close()


    '    'ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
    '    'ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtototal, O._formatoSubtotal).PadLeft(13), 0), "subtotal")

    '    'Dim Ivas As New Collection
    '    'Dim IvasImporte As New Collection
    '    'DR = V.DaIvas(idVenta)
    '    'ImpNDDi.Clear()
    '    'Dim IAnt As Double
    '    'While DR.Read
    '    '    If Ivas.Contains(DR("iva").ToString) = False Then
    '    '        Ivas.Add(DR("iva"), DR("iva").ToString)
    '    '    End If
    '    '    If IvasImporte.Contains(DR("iva").ToString) = False Then
    '    '        IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
    '    '    Else
    '    '        IAnt = IvasImporte(DR("iva").ToString)
    '    '        IvasImporte.Remove(DR("iva").ToString)
    '    '        IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
    '    '    End If
    '    'End While
    '    'DR.Close()
    '    'Cont = 0
    '    'For Each I As Double In Ivas
    '    '    ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(13), 0), "iva" + Format(Cont, "00"))
    '    '    Cont += 1
    '    'Next
    '    'If V.ISR <> 0 Then
    '    '    ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "isr", Format(V.TotalISR, O._formatoIva).PadLeft(13), 0), "iva" + Format(Cont, "00"))
    '    '    Cont += 1
    '    'End If
    '    'If V.IvaRetenido <> 0 Then
    '    '    ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "ivaretenido", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(13), 0), "iva" + Format(Cont, "00"))
    '    '    Cont += 1
    '    'End If
    '    'ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, O._formatoTotal).PadLeft(13), 0), "total")

    '    'Dim f As New StringFunctions
    '    'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion), 0), "totalletra")

    '    'ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
    '    'ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
    '    'Dim FP As New dbFormasdePago(V.IdFormadePago, MySqlcon)
    '    'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
    '    '    ImpND.Add(New NodoImpresionN("", "condicionpago", "Contado", 0), "condicionpago")
    '    '    ImpND.Add(New NodoImpresionN("", "diascredito", "", 0), "diascredito")
    '    '    ImpND.Add(New NodoImpresionN("", "limitecredito", "", 0), "limitecredito")
    '    'Else
    '    '    ImpND.Add(New NodoImpresionN("", "condicionpago", "Crédito", 0), "condicionpago")
    '    '    ImpND.Add(New NodoImpresionN("", "diascredito", V.Cliente.CreditoDias.ToString + " Días.", 0), "diascredito")
    '    '    ImpND.Add(New NodoImpresionN("", "limitecredito", Format(DateAdd(DateInterval.Day, V.Cliente.CreditoDias, CDate(V.Fecha)), "dd/MM/yyyy"), 0), "limitecredito")
    '    'End If

    '    'If V.IdConversion = 2 Then
    '    '    ImpND.Add(New NodoImpresionN("", "leyendadolar", "", 0), "leyendadolar")
    '    'Else
    '    '    ImpND.Add(New NodoImpresionN("", "leyendadolar", "El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar a la venta frente al peso vigente al día de su pago.", 0), "leyendadolar")
    '    'End If

    '    Posicion = 0
    '    'Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
    '    'CodigoBidimensional = CB.Encode("?&re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, O._formatoTotal) + "&id=" + V.uuid, System.Text.Encoding.Default)
    '    NumeroPagina = 1
    'End Sub

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
    'Private Sub LlenaNodosSeries(ByVal pidSucursal As Integer, ByVal pDocumento As Integer)
    '    Dim I As New dbImpresionesN(MySqlcon)
    '    Dim Fs As FontStyle
    '    ImpNDiS.Clear()
    '    Dim dr As MySql.Data.MySqlClient.MySqlDataReader
    '    Try
    '        dr = I.DaNodos(pDocumento, pidSucursal)
    '        While dr.Read
    '            Select Case dr("fuentestilo")
    '                Case 1
    '                    Fs = FontStyle.Bold
    '                Case 2
    '                    Fs = FontStyle.Italic
    '                Case 0
    '                    Fs = FontStyle.Regular
    '                Case 8
    '                    Fs = FontStyle.Strikeout
    '                Case 4
    '                    Fs = FontStyle.Underline
    '            End Select
    '            ImpNDiS.Add(New NodoImpresionN(dr("id"), dr("x"), dr("y"), dr("xl"), dr("yl"), dr("texto"), dr("datapropertyname"), New Font(dr("fuente").ToString, CSng(dr("fuentesize")), Fs, GraphicsUnit.Point), dr("alineacion"), dr("tipo"), dr("tipodato"), dr("visible"), dr("documento"), dr("tiponodo"), dr("idsucursal"), dr("conetiqueta"), dr("nombre")))
    '        End While
    '        dr.Close()
    '    Catch ex As Exception

    '    End Try

    'End Sub
    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.VentaTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        End If
        If TipoImpresora = 0 Then
            DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujo(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub
    Private Sub DibujaPaginaEstatico(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)
        Try

        Catch ex As Exception

        End Try
        Dim Nimp As New NodoImpresionN("", "", "", 0)
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos

        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            If DocAImprimir = 0 Then
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            Else
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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

                        Case 3
                            'imagenes
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            e.DrawImage(Nimp.Imagen, CInt(n.X / 40 * 10), YCoord, CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))

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
                        Case 3
                            'imagenes
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            e.DrawImage(Nimp.Imagen, CInt(n.X / 40 * 10), YCoord, CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
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
                If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
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
                                Case 3
                                    'imagenes
                                    Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                                    e.DrawImage(Nimp.Imagen, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
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

            If ncb.Visible = 1 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DibujaPaginaFlujo(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
        Dim Nimp As New NodoImpresionN("", "", "", 0)
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
            If DocAImprimir = 0 Then
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            Else
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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
            If pModo = 0 Or (NumeroPagina = 1 And (pModo = 1 Or pModo = 3)) Then
                For Each n As NodoImpresionN In ImpNDi
                    If n.DataPropertyName = "iva" Then niva = n
                    If n.DataPropertyName = "iva2" Then niva2 = n
                    If n.DataPropertyName = "codigobi" Then ncb = n
                    If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
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
                                    Case 3
                                        'imagenes
                                        Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                                        e.DrawImage(Nimp.Imagen, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
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
            End If
            
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
            If YCoord >= LimY And Posicion > 0 Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then
                        YCoord = (pZonaY + 3) / 40 * 10
                        If NumeroPagina > 1 And (pModo = 1 Or pModo = 3) Then YCoord = 1
                    End If

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
                        Case 3
                            'imagenes
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            e.DrawImage(Nimp.Imagen, CInt(n.X / 40 * 10), YCoord, CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra

            '***************************segundo
            Dim Haysegundo As Boolean = False
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    Haysegundo = True
                    If YCoord = 0 Then
                        YCoord = (pZonaY + 3) / 40 * 10
                        If NumeroPagina > 1 And (pModo = 1 Or pModo = 3) Then YCoord = 1
                    End If

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
                        Case 3
                            'imagenes
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            e.DrawImage(Nimp.Imagen, CInt(n.X / 40 * 10), YCoord, CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
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
                If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
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
                                Case 3
                                    'imagenes
                                    Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                                    e.DrawImage(Nimp.Imagen, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
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
        End Try
    End Sub
    '------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------
    '--------------------------Version Micro ------------------------------------
    Private Sub DibujaPagina(ByRef e As System.Drawing.Graphics)

        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        Dim Fuente As New Font("Lucida Console", 10, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteB As New Font("Lucida Console", 12, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteC As New Font("Lucida Console", 8, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteArial As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteArialB As New Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteArialC As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteArialCancela As New Font("Arial", 32, FontStyle.Regular, GraphicsUnit.Point)
        Dim Pluma As New Pen(Color.Black, 0.5)
        Dim PlumaRed As New Pen(Color.Red, 1)








        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        'Dim SF As New dbSucursalesFolios(MySqlcon)
        'Dim NumeroAprobacion As String = ""
        'Dim YearAprobacion As String = ""
        'If SF.BuscaFolios(V.IdSucursal, dbSucursalesFolios.TipoDocumentos.Factura, 1) Then
        '    NumeroAprobacion = SF.NoAprobacion
        '    YearAprobacion = SF.YearAprobacion
        'End If
        'Dim Cer As New dbSucursalesCertificados(MySqlcon)
        'Dim Certificado As String = ""
        'If Cer.BuscaCertificado(V.IdSucursal) Then
        ' Certificado = Cer.NoSerie
        ' End If
        'Dim Fondo As New Image
        'XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
        ' XMLDoc += "<Comprobante " + vbCrLf
        'en.Leex509(My.Settings.rutacer)
        'Dim TI As Double
        'Dim CI As Double
        V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
        'CI = TI * (V.Iva / 100) 'Pendiente

        Try
            Dim sa As New dbSucursalesArchivos
            e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Venta, V.IdSucursal, GlobalIdEmpresa, True)), 20, 1, 810, 1100)
        Catch ex As Exception

        End Try

        e.PageUnit = GraphicsUnit.Millimeter

        'e.DrawLine(Pluma, 10, 10, 10, 30)
        'e.DrawLine(Pluma, 10, 30, 100, 30)
        Dim XEncabezado As Integer = 65
        e.DrawString(Sucursal.NombreFiscal, FuenteArialC, Brushes.Black, XEncabezado, 4)
        e.DrawString(Sucursal.Direccion + " " + Sucursal.NoExterior + " " + Sucursal.NoInterior, FuenteArialC, Brushes.Black, XEncabezado, 8)
        e.DrawString("Col: " + Sucursal.Colonia + " " + Sucursal.Ciudad + " " + Sucursal.Estado, FuenteArialC, Brushes.Black, XEncabezado, 12)
        e.DrawString("CP: " + Sucursal.CP + " RFC:" + Sucursal.RFC, FuenteArialC, Brushes.Black, XEncabezado, 16)
        e.DrawString(If(Sucursal.Telefono <> "", "Tel: " + Sucursal.Telefono, "") + If(Sucursal.Email <> "", " E-mail:" + Sucursal.Email, ""), FuenteArialC, Brushes.Black, XEncabezado, 20)
        e.DrawString(Sucursal.ReferenciaDomicilio, FuenteArialC, Brushes.Black, XEncabezado, 24)
        'e.DrawLine(Pluma, 10, 34, 10, 55)
        'e.DrawLine(Pluma, 10, 55, 100, 55)
        'e.DrawString("Receptor:", Fuente, Brushes.Black, 12, 34)
        'Dim Nenter As String = V.Cliente.Nombre
        'InsertaEnters(Nenter, 60, 0, 0)

        Dim strF As New StringFormat
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        Dim Rec As RectangleF
        Rec = New RectangleF(12, 40, 100, 100)
        e.DrawString(V.Cliente.Nombre, FuenteC, Brushes.Black, Rec, strF)
        'e.DrawString(Nenter, FuenteC, Brushes.Black, 12, 40)
        If V.Cliente.DireccionFiscal = 0 Then
            e.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, FuenteC, Brushes.Black, 12, 46)
            e.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, FuenteC, Brushes.Black, 12, 50)
            e.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, FuenteC, Brushes.Black, 12, 54)
        Else
            e.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, FuenteC, Brushes.Black, 12, 46)
            e.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, FuenteC, Brushes.Black, 12, 50)
            e.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, FuenteC, Brushes.Black, 12, 54)
        End If
        e.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)


        e.DrawString(V.Serie + V.Folio.ToString, Fuente, Brushes.Black, 163, 28)
        'e.DrawString("No. Aprobación:", Fuente, Brushes.Black, 130, 16)
        e.DrawString(V.NoAprobacion + " " + V.YearAprobacion, Fuente, Brushes.Black, 163, 37)
        'e.DrawString("Año Aprobación:  ", Fuente, Brushes.Black, 130, 24)
        'e.DrawString(O._yearAprobacion, Fuente, Brushes.Black, 130, 28)
        'e.DrawString("No. Certificado: ", Fuente, Brushes.Black, 130, 32)
        e.DrawString(V.NoCertificado, Fuente, Brushes.Black, 163, 44)
        'e.DrawString("Fecha:", Fuente, Brushes.Black, 155, 36)
        e.DrawString(Replace(V.Fecha, "/", "-") + " " + V.Hora, Fuente, Brushes.Black, 163, 52)
        'e.DrawString(V.Hora, FuenteC, Brushes.Black, 187, 32)


        'e.DrawString("Cant.", Fuente, Brushes.Black, 12, 60)
        'e.DrawString("Unidad", Fuente, Brushes.Black, 25, 60)
        'e.DrawString("Descripción", Fuente, Brushes.Black, 42, 60)
        'e.DrawString("P. Unitario", Fuente, Brushes.Black, 130, 60)
        'e.DrawString("Importe", Fuente, Brushes.Black, 170, 60)


        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbVentasInventario(MySqlcon)
        DR = VI.ConsultaReader(idVenta, False, "0", 0, "0")
        Dim CadenaB As String
        Dim Y As Integer = 74
        Dim YB As Integer
        While DR.Read
            e.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
            'e.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)

            YB = Y
            CadenaB = DR("descripcion")

            Y = InsertaEnters(CadenaB, 34, Y, 4)
            Rec = New RectangleF(53, YB, 75, 200)
            e.DrawString(DR("descripcion"), FuenteC, Brushes.Black, Rec, strF)
            'e.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
            'e.DrawString(, Fuente, Brushes.Black, 130, YB)
            e.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), Fuente, Brushes.Black, 119, YB)
            e.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
            e.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
            Y += 6
        End While
        DR.Close()

        'Dim VP As New dbVentasProductos(MySqlcon)
        'DR = VP.ConsultaReader(idVenta)
        'While DR.Read
        '    e.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
        '    'e.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
        '    YB = Y
        '    CadenaB = DR("descripcion")
        '    Y = InsertaEnters(CadenaB, 34, Y, 4)
        '    e.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
        '    e.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
        '    e.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
        '    e.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
        '    Y += 6
        'End While
        'DR.Close()


        'Dim VS As New dbVentasServicios(MySqlcon)
        'DR = VS.ConsultaReader(idVenta)
        'While DR.Read
        '    'e.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
        '    'e.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
        '    YB = Y
        '    CadenaB = DR("descripcion")
        '    Y = InsertaEnters(CadenaB, 34, Y, 4)
        '    e.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
        '    e.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
        '    e.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
        '    e.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
        '    Y += 6
        'End While
        'DR.Close()
        Y = 170
        e.DrawString("Sub total:", FuenteC, Brushes.White, 153, Y)
        e.DrawString(Format(V.Subtototal, "$#,###,###0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
        If V.Comentario <> "" Then
            e.DrawString(V.Comentario, FuenteArialC, Brushes.Black, 8, 154)
        End If
        If V.IdConversion <> 2 Then
            e.DrawString("El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar " + vbCrLf + "frente al peso vigente al día de su pago.", FuenteArialC, Brushes.Black, 8, 158)
        End If
        '“El importe de la presente factura deberá ser pagada de acuerdo A la cotización del dólar frente al peso vigente al día de su pago”
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(idVenta)
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
        Y += 4
        For Each I As Double In Ivas
            'If I <> 0 Then
            e.DrawString("Iva " + Format(I, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
            e.DrawString(Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
            Y += 4
            'End If
        Next
        If V.ISR <> 0 Then
            e.DrawString("ISR " + Format(V.ISR, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
            e.DrawString(Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
            Y += 4
        End If
        If V.IvaRetenido <> 0 Then
            e.DrawString("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
            e.DrawString(Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
            Y += 4
        End If
        e.DrawString("Total: ", Fuente, Brushes.White, 153, Y)
        e.DrawString(Format(V.TotalVenta, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)

        Dim f As New StringFunctions

        Y = 175
        CadenaB = ""
        If V.IdConversion = 2 Then
            'CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion)
        Else
            'CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion)
        End If

        YB = Y
        Y = InsertaEnters(CadenaB, 63, Y, 4)


        e.DrawString(CadenaB, Fuente, Brushes.Black, 10, 175)



        Y = 198
        YB = Y
        CadenaB = Cadena
        Y = InsertaEnters(CadenaB, 105, Y, 4)
        e.DrawString(CadenaB, FuenteC, Brushes.Black, 10, YB)

        'e.DrawString("Sello:", Fuente, Brushes.Black, 10, Y + 6)

        Dim SelloB As String
        Y = 238
        SelloB = Sello
        YB = Y
        Y = InsertaEnters(SelloB, 105, Y, 4)




        e.DrawString(SelloB, FuenteC, Brushes.Black, 10, Y)

        If V.Estado = Estados.Cancelada Then
            e.DrawString("CANCELADA", FuenteArialCancela, Brushes.Red, 70, 110)
            e.DrawLine(PlumaRed, 70, 107, 140, 107)
            e.DrawLine(PlumaRed, 70, 125, 140, 125)
        End If

        'e.DrawString("Este documento es una impresión de un comprobante fiscal digital", Fuente, Brushes.Black, 10, Y + 16)


    End Sub
    '---------------------------------------Fac 2011------------------------------------------------------------
    '----------------------------------------------------------------------------------------------------------

    Private Sub DibujaPaginai(ByRef e As System.Drawing.Graphics)

        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        Dim Fuente As New Font("Lucida Console", 10, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteB As New Font("Lucida Console", 12, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteC As New Font("Lucida Console", 8, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteArial As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteArialB As New Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteArialC As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteArialCancela As New Font("Arial", 32, FontStyle.Regular, GraphicsUnit.Point)
        Dim Pluma As New Pen(Color.Black, 0.5)
        Dim PlumaRed As New Pen(Color.Red, 1)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        'Dim SF As New dbSucursalesFolios(MySqlcon)
        'Dim NumeroAprobacion As String = ""
        'Dim YearAprobacion As String = ""
        'If SF.BuscaFolios(V.IdSucursal, dbSucursalesFolios.TipoDocumentos.Factura, 1) Then
        '    NumeroAprobacion = SF.NoAprobacion
        '    YearAprobacion = SF.YearAprobacion
        'End If
        'Dim Cer As New dbSucursalesCertificados(MySqlcon)
        'Dim Certificado As String = ""
        'If Cer.BuscaCertificado(V.IdSucursal) Then
        ' Certificado = Cer.NoSerie
        ' End If
        'Dim Fondo As New Image
        'XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
        ' XMLDoc += "<Comprobante " + vbCrLf
        'en.Leex509(My.Settings.rutacer)
        'Dim TI As Double
        'Dim CI As Double
        V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
        'CI = TI * (V.Iva / 100) 'Pendiente

        Try
            Dim sa As New dbSucursalesArchivos
            e.DrawImage(Image.FromFile(sa.DaRuta(TiposDocumentos.Venta, V.IdSucursal, GlobalIdEmpresa, True)), 20, 1, 810, 1100)
        Catch ex As Exception

        End Try

        e.PageUnit = GraphicsUnit.Millimeter

        'e.DrawLine(Pluma, 10, 10, 10, 30)
        'e.DrawLine(Pluma, 10, 30, 100, 30)
        Dim XEncabezado As Integer = 65
        e.DrawString(Sucursal.NombreFiscal, FuenteArialC, Brushes.Black, XEncabezado, 4)
        e.DrawString(Sucursal.Direccion + " " + Sucursal.NoExterior + " " + Sucursal.NoInterior, FuenteArialC, Brushes.Black, XEncabezado, 8)
        e.DrawString("Col: " + Sucursal.Colonia + " " + Sucursal.Ciudad + " " + Sucursal.Estado, FuenteArialC, Brushes.Black, XEncabezado, 12)
        e.DrawString("CP: " + Sucursal.CP + " RFC:" + Sucursal.RFC, FuenteArialC, Brushes.Black, XEncabezado, 16)
        e.DrawString(If(Sucursal.Telefono <> "", "Tel: " + Sucursal.Telefono, "") + If(Sucursal.Email <> "", " E-mail:" + Sucursal.Email, ""), FuenteArialC, Brushes.Black, XEncabezado, 20)
        e.DrawString(Sucursal.ReferenciaDomicilio, FuenteArialC, Brushes.Black, XEncabezado, 24)

        'e.DrawLine(Pluma, 10, 34, 10, 55)
        'e.DrawLine(Pluma, 10, 55, 100, 55)
        'e.DrawString("Receptor:", Fuente, Brushes.Black, 12, 34)
        'Dim Nenter As String = V.Cliente.Nombre
        'InsertaEnters(Nenter, 60, 0, 0)

        Dim strF As New StringFormat
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        Dim Rec As RectangleF
        Rec = New RectangleF(12, 40, 100, 100)
        e.DrawString(V.Cliente.Nombre, FuenteC, Brushes.Black, Rec, strF)
        'e.DrawString(Nenter, FuenteC, Brushes.Black, 12, 40)
        If V.Cliente.DireccionFiscal = 0 Then
            e.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, FuenteC, Brushes.Black, 12, 46)
            e.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, FuenteC, Brushes.Black, 12, 50)
            e.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, FuenteC, Brushes.Black, 12, 54)
        Else
            e.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, FuenteC, Brushes.Black, 12, 46)
            e.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, FuenteC, Brushes.Black, 12, 50)
            e.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, FuenteC, Brushes.Black, 12, 54)
        End If
        e.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)


        e.DrawString(V.Serie + V.Folio.ToString, Fuente, Brushes.Black, 163, 28)
        'e.DrawString("No. Aprobación:", Fuente, Brushes.Black, 130, 16)
        e.DrawString(V.NoAprobacion + " " + V.YearAprobacion, Fuente, Brushes.Black, 163, 37)
        'e.DrawString("Año Aprobación:  ", Fuente, Brushes.Black, 130, 24)
        'e.DrawString(O._yearAprobacion, Fuente, Brushes.Black, 130, 28)
        'e.DrawString("No. Certificado: ", Fuente, Brushes.Black, 130, 32)
        e.DrawString(V.NoCertificado, Fuente, Brushes.Black, 163, 44)
        'e.DrawString("Fecha:", Fuente, Brushes.Black, 155, 36)
        e.DrawString(Replace(V.Fecha, "/", "-") + " " + V.Hora, Fuente, Brushes.Black, 163, 52)
        'e.DrawString(V.Hora, FuenteC, Brushes.Black, 187, 32)


        'e.DrawString("Cant.", Fuente, Brushes.Black, 12, 60)
        'e.DrawString("Unidad", Fuente, Brushes.Black, 25, 60)
        'e.DrawString("Descripción", Fuente, Brushes.Black, 42, 60)
        'e.DrawString("P. Unitario", Fuente, Brushes.Black, 130, 60)
        'e.DrawString("Importe", Fuente, Brushes.Black, 170, 60)


        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbVentasInventario(MySqlcon)
        DR = VI.ConsultaReader(idVenta, False, "0", 0, Op._OrdenUbicacion)
        Dim CadenaB As String
        Dim Y As Integer = 74
        Dim YB As Integer
        While DR.Read
            e.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
            'e.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)

            YB = Y
            CadenaB = DR("descripcion")

            Y = InsertaEnters(CadenaB, 34, Y, 4)
            Rec = New RectangleF(53, YB, 75, 200)
            e.DrawString(DR("descripcion"), FuenteC, Brushes.Black, Rec, strF)
            'e.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
            'e.DrawString(, Fuente, Brushes.Black, 130, YB)
            e.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), Fuente, Brushes.Black, 119, YB)
            e.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
            e.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
            Y += 6
        End While
        DR.Close()

        'Dim VP As New dbVentasProductos(MySqlcon)
        'DR = VP.ConsultaReader(idVenta)
        'While DR.Read
        '    e.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
        '    'e.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
        '    YB = Y
        '    CadenaB = DR("descripcion")
        '    Y = InsertaEnters(CadenaB, 34, Y, 4)
        '    e.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
        '    e.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
        '    e.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
        '    e.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
        '    Y += 6
        'End While
        'DR.Close()


        'Dim VS As New dbVentasServicios(MySqlcon)
        'DR = VS.ConsultaReader(idVenta)
        'While DR.Read
        '    'e.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
        '    'e.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
        '    YB = Y
        '    CadenaB = DR("descripcion")
        '    Y = InsertaEnters(CadenaB, 34, Y, 4)
        '    e.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
        '    e.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
        '    e.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
        '    e.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
        '    Y += 6
        'End While
        'DR.Close()
        Y = 170
        e.DrawString("Sub total:", FuenteC, Brushes.White, 153, Y)
        e.DrawString(Format(V.Subtototal, "$#,###,###0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
        If V.Comentario <> "" Then
            e.DrawString(V.Comentario, FuenteArialC, Brushes.Black, 8, 154)
        End If
        If V.IdConversion <> 2 Then
            e.DrawString("El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar " + vbCrLf + "frente al peso vigente al día de su pago.", FuenteArialC, Brushes.Black, 8, 158)
        End If
        '“El importe de la presente factura deberá ser pagada de acuerdo A la cotización del dólar frente al peso vigente al día de su pago”
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(idVenta)
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
        Y += 4
        For Each I As Double In Ivas
            'If I <> 0 Then
            e.DrawString("Iva " + Format(I, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
            e.DrawString(Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
            Y += 4
            'End If
        Next
        If V.ISR <> 0 Then
            e.DrawString("ISR " + Format(V.ISR, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
            e.DrawString(Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
            Y += 4
        End If
        If V.IvaRetenido <> 0 Then
            e.DrawString("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
            e.DrawString(Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
            Y += 4
        End If
        e.DrawString("Total: ", Fuente, Brushes.White, 153, Y)
        e.DrawString(Format(V.TotalVenta, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)

        Dim f As New StringFunctions

        Y = 175

        If V.IdConversion = 2 Then
            ' CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion)
        Else
            'CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion)
        End If

        YB = Y
        Y = InsertaEnters("", 63, Y, 4)


        e.DrawString("", Fuente, Brushes.Black, 10, 175)



        Y = 198
        YB = Y
        CadenaB = Cadena
        Y = InsertaEnters(CadenaB, 105, Y, 4)
        e.DrawString(CadenaB, FuenteC, Brushes.Black, 10, YB)

        'e.DrawString("Sello:", Fuente, Brushes.Black, 10, Y + 6)

        Dim SelloB As String
        Y = 238
        SelloB = Sello
        YB = Y
        Y = InsertaEnters(SelloB, 105, Y, 4)




        e.DrawString(SelloB, FuenteC, Brushes.Black, 10, Y)

        If V.Estado = Estados.Cancelada Then
            e.DrawString("CANCELADA", FuenteArialCancela, Brushes.Red, 70, 110)
            e.DrawLine(PlumaRed, 70, 107, 140, 107)
            e.DrawLine(PlumaRed, 70, 125, 140, 125)
        End If

        'e.DrawString("Este documento es una impresión de un comprobante fiscal digital", Fuente, Brushes.Black, 10, Y + 16)


    End Sub
    '----------------------------------Termina 2011------------------------------------------------------------

    Private Sub DibujaPaginaDaniel(ByRef e As System.Drawing.Graphics)

        'Dim O As New dbOpciones(MySqlcon)
        'Dim en As New Encriptador
        'Dim XMLDoc As String
        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        Dim Fuente As New Font("Lucida Console", 10, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteB As New Font("Lucida Console", 12, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteC As New Font("Lucida Console", 8, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteArial As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteArialB As New Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point)
        Dim FuenteArialC As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
        Dim Pluma As New Pen(Color.Black, 0.5)
        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Op._Sinnegativos, Op._CalculoAlterno)
        Try
            Dim sa As New dbSucursalesArchivos
            e.DrawImage(Image.FromFile(sa.DaRuta(TiposDocumentos.Venta, V.IdSucursal, GlobalIdEmpresa, True)), 20, 1, 810, 1100)
        Catch ex As Exception

        End Try
        e.PageUnit = GraphicsUnit.Millimeter
        'e.DrawLine(Pluma, 10, 10, 10, 30)
        'e.DrawLine(Pluma, 10, 30, 100, 30)
        Dim XEncabezado As Integer = 65
        e.DrawString(S.Nombre, FuenteArialB, Brushes.Black, XEncabezado, 4)
        e.DrawString(S.Direccion + " " + S.NoExterior + " " + S.NoInterior, FuenteArial, Brushes.Black, XEncabezado, 10)
        e.DrawString("Col: " + S.Colonia + " " + S.Ciudad + " " + S.Estado, FuenteArial, Brushes.Black, XEncabezado, 15)
        e.DrawString("CP: " + S.CP + " RFC:" + S.RFC, FuenteArial, Brushes.Black, XEncabezado, 20)
        e.DrawString(S.ReferenciaDomicilio, FuenteArial, Brushes.Black, XEncabezado, 24)

        'e.DrawLine(Pluma, 10, 34, 10, 55)
        'e.DrawLine(Pluma, 10, 55, 100, 55)
        'e.DrawString("Receptor:", Fuente, Brushes.Black, 12, 34)


        'Dim Nenter As String = V.Cliente.Nombre
        'InsertaEnters(Nenter, 60, 0, 0)
        'e.DrawString(Nenter, FuenteC, Brushes.Black, 12, 40)

        Dim strF As New StringFormat
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        Dim Rec As RectangleF
        Rec = New RectangleF(12, 40, 100, 100)
        e.DrawString(V.Cliente.Nombre, FuenteC, Brushes.Black, Rec, strF)

        If V.Cliente.DireccionFiscal = 0 Then
            e.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, FuenteC, Brushes.Black, 12, 46)
            e.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, FuenteC, Brushes.Black, 12, 50)
            e.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, FuenteC, Brushes.Black, 12, 54)
        Else
            e.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, FuenteC, Brushes.Black, 12, 46)
            e.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, FuenteC, Brushes.Black, 12, 50)
            e.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, FuenteC, Brushes.Black, 12, 54)
        End If
        e.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)






        '    e.DrawString(V.Cliente.Nombre, Fuente, Brushes.Black, 12, 42)
        '    If V.Cliente.DireccionFiscal = 0 Then
        '        e.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, Fuente, Brushes.Black, 12, 46)
        '        e.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, Fuente, Brushes.Black, 12, 50)
        '        e.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, Fuente, Brushes.Black, 12, 54)
        '    Else
        '        e.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, Fuente, Brushes.Black, 12, 46)
        '        e.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, Fuente, Brushes.Black, 12, 50)
        '        e.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, Fuente, Brushes.Black, 12, 54)
        '    End If
        '    e.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)


        e.DrawString(V.Serie + V.Folio.ToString, Fuente, Brushes.Black, 163, 28)
        'e.DrawString("No. Aprobación:", Fuente, Brushes.Black, 130, 16)
        e.DrawString(V.NoAprobacion + " " + V.YearAprobacion, Fuente, Brushes.Black, 163, 37)
        'e.DrawString("Año Aprobación:  ", Fuente, Brushes.Black, 130, 24)
        'e.DrawString(O._yearAprobacion, Fuente, Brushes.Black, 130, 28)
        'e.DrawString("No. Certificado: ", Fuente, Brushes.Black, 130, 32)
        e.DrawString(V.NoCertificado, Fuente, Brushes.Black, 163, 44)
        'e.DrawString("Fecha:", Fuente, Brushes.Black, 155, 36)
        e.DrawString(Replace(V.Fecha, "/", "-") + " " + V.Hora, Fuente, Brushes.Black, 163, 52)
        'e.DrawString(V.Hora, FuenteC, Brushes.Black, 187, 32)


        'e.DrawString("Cant.", Fuente, Brushes.Black, 12, 60)
        'e.DrawString("Unidad", Fuente, Brushes.Black, 25, 60)
        'e.DrawString("Descripción", Fuente, Brushes.Black, 42, 60)
        'e.DrawString("P. Unitario", Fuente, Brushes.Black, 130, 60)
        'e.DrawString("Importe", Fuente, Brushes.Black, 170, 60)
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbVentasInventario(MySqlcon)
        DR = VI.ConsultaReader(idVenta, False, "0", 0, "0")
        Dim CadenaB As String
        Dim Y As Integer = 70
        Dim YB As Integer
        While DR.Read
            'e.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
            'e.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)

            YB = Y
            CadenaB = DR("descripcion")
            Y = InsertaEnters(CadenaB, 50, Y, 4)
            Rec = New RectangleF(32, YB, 120, 200)
            e.DrawString(DR("descripcion"), Fuente, Brushes.Black, Rec, strF)
            'e.DrawString(CadenaB, Fuente, Brushes.Black, 32, YB)
            e.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 10, YB)
            e.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 146, YB)
            e.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
            Y += 6
        End While
        DR.Close()

        Y = 170
        e.DrawString("Sub total:", FuenteC, Brushes.White, 153, Y)
        e.DrawString(Format(V.Subtototal, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
        If V.Comentario <> "" Then
            e.DrawString(V.Comentario, FuenteArialC, Brushes.Black, 8, 154)
        End If
        If V.IdConversion <> 2 Then
            e.DrawString("El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar " + vbCrLf + "frente al peso vigente al día de su pago.", FuenteArialC, Brushes.Black, 8, 158)
        End If
        '“El importe de la presente factura deberá ser pagada de acuerdo A la cotización del dólar frente al peso vigente al día de su pago”
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(idVenta)
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
        Y += 4
        For Each I As Double In Ivas
            'If I <> 0 Then
            e.DrawString("Iva " + Format(I, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
            e.DrawString(Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
            Y += 4
            'End If
        Next
        If V.ISR <> 0 Then
            e.DrawString("ISR " + Format(V.ISR, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
            e.DrawString(Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
            Y += 4
        End If
        If V.IvaRetenido <> 0 Then
            e.DrawString("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
            e.DrawString(Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
            Y += 4
        End If
        e.DrawString("Total: ", Fuente, Brushes.White, 153, Y)
        e.DrawString(Format(V.TotalVenta, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)

        Dim f As New StringFunctions

        Y = 175

        'If V.IdConversion = 2 Then
        '    CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion)
        'Else
        '    CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion)
        'End If

        YB = Y
        Y = InsertaEnters("", 63, Y, 4)
        e.DrawString("", Fuente, Brushes.Black, 10, 175)

        Y = 198
        YB = Y
        CadenaB = Cadena
        Y = InsertaEnters(CadenaB, 105, Y, 4)
        e.DrawString(CadenaB, FuenteC, Brushes.Black, 10, YB)

        Dim SelloB As String
        Y = 238
        SelloB = Sello
        YB = Y
        Y = InsertaEnters(SelloB, 105, Y, 4)
        e.DrawString(SelloB, FuenteC, Brushes.Black, 10, Y)

    End Sub


    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'If iTipoFacturacion = 2 Then
        '    'CFDi
        '    DibujaPaginai(e.Graphics)
        'Else
        'CFD
        If DocAImprimir = 0 Then
            DibujaPaginaN(e.Graphics)
            If MasPaginas = True Or NumeroPagina > 2 Then
                'e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00") + "/" + Format(CuantaY, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
                e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
            End If
            If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
                e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
            End If
            'If Estado = Estados.Cancelada Then
            '    e.Graphics.DrawString("CANCELADA", New Font("Arial", 18, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, 80, 130)
            'End If
            e.HasMorePages = MasPaginas
            'DibujaPagina(e.Graphics)
            'e.HasMorePages = True
            'End If
        End If
        If DocAImprimir = 1 Then
            DibujaPaginaN(e.Graphics)
            If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
                e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
            End If
        End If
    End Sub


    '-------------------------------- Termina Version Micro-----------------------------------
    '---------------------------------------------------------------------------
    'Version Daniel
    'Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage



    '    Dim O As New dbOpciones(MySqlcon)
    '    Dim en As New Encriptador
    '    'Dim XMLDoc As String
    '    Dim V As New dbVentas(idVenta, MySqlcon)
    '    Dim Fuente As New Font("Lucida Console", 10, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteB As New Font("Lucida Console", 12, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteC As New Font("Lucida Console", 8, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArial As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArialB As New Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArialC As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim Pluma As New Pen(Color.Black, 0.5)
    '    'Dim Fondo As New Image
    '    'XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
    '    ' XMLDoc += "<Comprobante " + vbCrLf
    '    en.Leex509(My.Settings.rutacer)
    '    'Dim TI As Double
    '    'Dim CI As Double
    '    V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
    '    'CI = TI * (V.Iva / 100) 'Pendiente

    '    Try
    '        e.Graphics.DrawImage(Image.FromFile(My.Settings.rutafacturafondo), 20, 1, 810, 1100)
    '    Catch ex As Exception

    '    End Try

    '    e.Graphics.PageUnit = GraphicsUnit.Millimeter

    '    'e.Graphics.DrawLine(Pluma, 10, 10, 10, 30)
    '    'e.Graphics.DrawLine(Pluma, 10, 30, 100, 30)
    '    Dim XEncabezado As Integer = 65
    '    e.Graphics.DrawString(O._NombreEmpresa, FuenteArialB, Brushes.Black, XEncabezado, 4)
    '    e.Graphics.DrawString(O._Calle + " " + O._noExterior + " " + O._noInterior, FuenteArial, Brushes.Black, XEncabezado, 10)
    '    e.Graphics.DrawString("Col: " + O._Colonia + " " + O._Localidad + " " + O._Estado, FuenteArial, Brushes.Black, XEncabezado, 15)
    '    e.Graphics.DrawString("CP: " + O._CodigoPostal + " RFC:" + O._RFC, FuenteArial, Brushes.Black, XEncabezado, 20)
    '    e.Graphics.DrawString(O._ReferenciaDomicilio, FuenteArial, Brushes.Black, XEncabezado, 24)

    '    'e.Graphics.DrawLine(Pluma, 10, 34, 10, 55)
    '    'e.Graphics.DrawLine(Pluma, 10, 55, 100, 55)
    '    'e.Graphics.DrawString("Receptor:", Fuente, Brushes.Black, 12, 34)


    '    'Dim Nenter As String = V.Cliente.Nombre
    '    'InsertaEnters(Nenter, 60, 0, 0)
    '    'e.Graphics.DrawString(Nenter, FuenteC, Brushes.Black, 12, 40)

    '    Dim strF As New StringFormat
    '    strF.Alignment = StringAlignment.Near
    '    strF.LineAlignment = StringAlignment.Near
    '    Dim Rec As RectangleF
    '    Rec = New RectangleF(12, 40, 100, 100)
    '    e.Graphics.DrawString(V.Cliente.Nombre, FuenteC, Brushes.Black, Rec, strF)

    '    If V.Cliente.DireccionFiscal = 0 Then
    '        e.Graphics.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, FuenteC, Brushes.Black, 12, 46)
    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, FuenteC, Brushes.Black, 12, 50)
    '        e.Graphics.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, FuenteC, Brushes.Black, 12, 54)
    '    Else
    '        e.Graphics.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, FuenteC, Brushes.Black, 12, 46)
    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, FuenteC, Brushes.Black, 12, 50)
    '        e.Graphics.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, FuenteC, Brushes.Black, 12, 54)
    '    End If
    '    e.Graphics.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)






    '    '    e.Graphics.DrawString(V.Cliente.Nombre, Fuente, Brushes.Black, 12, 42)
    '    '    If V.Cliente.DireccionFiscal = 0 Then
    '    '        e.Graphics.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, Fuente, Brushes.Black, 12, 46)
    '    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, Fuente, Brushes.Black, 12, 50)
    '    '        e.Graphics.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, Fuente, Brushes.Black, 12, 54)
    '    '    Else
    '    '        e.Graphics.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, Fuente, Brushes.Black, 12, 46)
    '    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, Fuente, Brushes.Black, 12, 50)
    '    '        e.Graphics.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, Fuente, Brushes.Black, 12, 54)
    '    '    End If
    '    '    e.Graphics.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)


    '    e.Graphics.DrawString(V.Serie + V.Folio.ToString, Fuente, Brushes.Black, 163, 28)
    '    'e.Graphics.DrawString("No. Aprobación:", Fuente, Brushes.Black, 130, 16)
    '    e.Graphics.DrawString(O._noAprobacion + " " + O._yearAprobacion, Fuente, Brushes.Black, 163, 37)
    '    'e.Graphics.DrawString("Año Aprobación:  ", Fuente, Brushes.Black, 130, 24)
    '    'e.Graphics.DrawString(O._yearAprobacion, Fuente, Brushes.Black, 130, 28)
    '    'e.Graphics.DrawString("No. Certificado: ", Fuente, Brushes.Black, 130, 32)
    '    e.Graphics.DrawString(en.Seriex509, Fuente, Brushes.Black, 163, 44)
    '    'e.Graphics.DrawString("Fecha:", Fuente, Brushes.Black, 155, 36)
    '    e.Graphics.DrawString(Replace(V.Fecha, "/", "-") + " " + V.Hora, Fuente, Brushes.Black, 163, 52)
    '    'e.Graphics.DrawString(V.Hora, FuenteC, Brushes.Black, 187, 32)


    '    'e.Graphics.DrawString("Cant.", Fuente, Brushes.Black, 12, 60)
    '    'e.Graphics.DrawString("Unidad", Fuente, Brushes.Black, 25, 60)
    '    'e.Graphics.DrawString("Descripción", Fuente, Brushes.Black, 42, 60)
    '    'e.Graphics.DrawString("P. Unitario", Fuente, Brushes.Black, 130, 60)
    '    'e.Graphics.DrawString("Importe", Fuente, Brushes.Black, 170, 60)
    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReader(idVenta)
    '    Dim CadenaB As String
    '    Dim Y As Integer = 70
    '    Dim YB As Integer
    '    While DR.Read
    '        'e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
    '        'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)

    '        YB = Y
    '        CadenaB = DR("descripcion")
    '        Y = InsertaEnters(CadenaB, 50, Y, 4)
    '        Rec = New RectangleF(32, YB, 120, 200)
    '        e.Graphics.DrawString(DR("descripcion"), Fuente, Brushes.Black, Rec, strF)
    '        'e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 32, YB)
    '        e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 10, YB)
    '        e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 146, YB)
    '        e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
    '        Y += 6
    '    End While
    '    DR.Close()

    '    Y = 170
    '    e.Graphics.DrawString("Sub total:", FuenteC, Brushes.White, 153, Y)
    '    e.Graphics.DrawString(Format(V.Subtototal, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '    If V.Comentario <> "" Then
    '        e.Graphics.DrawString(V.Comentario, FuenteArialC, Brushes.Black, 8, 154)
    '    End If
    '    If V.IdConversion <> 2 Then
    '        e.Graphics.DrawString("El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar " + vbCrLf + "frente al peso vigente al día de su pago.", FuenteArialC, Brushes.Black, 8, 158)
    '    End If
    '    '“El importe de la presente factura deberá ser pagada de acuerdo A la cotización del dólar frente al peso vigente al día de su pago”
    '    Dim Ivas As New Collection
    '    Dim IvasImporte As New Collection
    '    DR = V.DaIvas(idVenta)
    '    Dim IAnt As Double
    '    While DR.Read
    '        If Ivas.Contains(DR("iva").ToString) = False Then
    '            Ivas.Add(DR("iva"), DR("iva").ToString)
    '        End If
    '        If IvasImporte.Contains(DR("iva").ToString) = False Then
    '            IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
    '        Else
    '            IAnt = IvasImporte(DR("iva").ToString)
    '            IvasImporte.Remove(DR("iva").ToString)
    '            IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
    '        End If
    '    End While
    '    DR.Close()
    '    Y += 4
    '    For Each I As Double In Ivas
    '        'If I <> 0 Then
    '        e.Graphics.DrawString("Iva " + Format(I, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '        'End If
    '    Next
    '    If V.ISR <> 0 Then
    '        e.Graphics.DrawString("ISR " + Format(V.ISR, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '    End If
    '    If V.IvaRetenido <> 0 Then
    '        e.Graphics.DrawString("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '    End If
    '    e.Graphics.DrawString("Total: ", Fuente, Brushes.White, 153, Y)
    '    e.Graphics.DrawString(Format(V.TotalVenta, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)

    '    Dim f As New PaseLetras

    '    Y = 175

    '    If V.IdConversion = 2 Then
    '        CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), MONEDAS.MN) + " M.N."
    '    Else
    '        CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), MONEDAS.USD) + " U.S.D"
    '    End If

    '    YB = Y
    '    Y = InsertaEnters(CadenaB, 63, Y, 4)
    '    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 10, 175)

    '    Y = 198
    '    YB = Y
    '    CadenaB = Cadena
    '    Y = InsertaEnters(CadenaB, 105, Y, 4)
    '    e.Graphics.DrawString(CadenaB, FuenteC, Brushes.Black, 10, YB)

    '    Dim SelloB As String
    '    Y = 238
    '    SelloB = Sello
    '    YB = Y
    '    Y = InsertaEnters(SelloB, 105, Y, 4)

    '    e.Graphics.DrawString(SelloB, FuenteC, Brushes.Black, 10, Y)

    'End Sub
    '----------------Termina Version DAniel---------------------------------------------------------------
    '-------------------------------------------------------------------------------------------------------

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        'Pendiente Llenar Addenda
        Dim XMLAddenda As String = ""
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
            End Select
        Else
            Modificar(Estados.SinGuardar)
        End If
        Select Case Eselectronica
            Case 0
                Imprimir()
            Case 1
                CadenaOriginal(Estado, XMLAddenda)
            Case 2
                CadenaOriginali(Estado, XMLAddenda)
        End Select
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
        'If ConsultaOn Then
        '    If IsNumeric(TextBox12.Text) And IsNumeric(TextBox9.Text) Then
        '        If PrecioBase <> 0 Then
        '            TextBox12.Text = CStr(PrecioBase - (PrecioBase * CDbl(TextBox9.Text) / 100))
        '        Else
        '            TextBox12.Text = CStr(PrecioU - (PrecioU * CDbl(TextBox9.Text) / 100))
        '        End If
        '        'TextBox6.Text = CStr(CDbl(TextBox6.Text) * CDbl(TextBox10.Text))
        '    End If
        'End If
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
    Private Sub Imprimir()
        Dim RutaPDF As String
        Dim Archivos As New dbSucursalesArchivos
        RutaPDF = Archivos.DaRutaArchivos(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
        If Op._NoRutas = "0" Then
            RutaPDF = RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
        End If
        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Dim ImpAnt As String
        Dim ImpFlujo As String
        Dim TipoAnt As Integer
        Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, False, 0, TiposDocumentos.Venta)
        ImpFlujo = SA.DaImpresoraPorTipo(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 1, TiposDocumentos.Venta)
        ImpAnt = Impresora
        TipoImpresora = SA.TipoImpresora
        TipoAnt = TipoImpresora

        If Impresora = "Bullzip PDF Printer" Then
            Dim obj As New Bullzip.PdfWriter.PdfSettings
            obj.Init()
            obj.PrinterName = Impresora
            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            obj.SetValue("ShowSettings", "never")
            obj.SetValue("ShowPDF", "yes")
            obj.SetValue("ShowSaveAS", "nofile")
            obj.SetValue("ConfirmOverwrite", "no")
            obj.SetValue("Target", "printer")
            obj.WriteSettings()
        End If


        
        DocAImprimir = 0
        If Op.Copiaflujoventas = 1 Then
            Impresora = ImpFlujo
            LlenaNodos(IdsSucursales.Valor(ComboBox3.SelectedIndex), TiposDocumentos.VentaTicket)
            LlenaNodosImpresion(Op.TituloOriginalFactura)
            TipoImpresora = 1
            PrintDocument1.PrinterSettings.PrinterName = Impresora
            PrintDocument1.DocumentName = "PSSFACTURA Copia-" + TextBox11.Text + TextBox2.Text
            PrintDocument1.Print()
            TipoImpresora = TipoAnt
            Impresora = ImpAnt
        End If
        If TipoImpresora = 0 Then
            LlenaNodos(IdsSucursales.Valor(ComboBox3.SelectedIndex), TiposDocumentos.Venta)
        Else
            LlenaNodos(IdsSucursales.Valor(ComboBox3.SelectedIndex), TiposDocumentos.VentaTicket)
        End If
        LlenaNodosImpresion(Op.TituloOriginalFactura)
        PrintDocument1.PrinterSettings.PrinterName = Impresora
        PrintDocument1.DocumentName = "PSSFACTURA-" + TextBox11.Text + TextBox2.Text
        PrintDocument1.Print()
        If Op.ActivarCopiaFactura = 1 Then
            LlenaNodosImpresion(Op.TituloCopiaFactura)
            PrintDocument1.Print()
        End If
        If Op.ActivarCopia2Factura = 1 Then
            LlenaNodosImpresion(Op.TituloCopia2Factura)
            PrintDocument1.Print()
        End If


        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        If V.ISR <> 0 Or V.IvaRetenido <> 0 Then
            If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                LlenaNodosImpresionRet()
                If TipoImpresora = 0 Then
                    LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencion)
                Else
                    LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
                End If
                PrintDocument1.DocumentName = "RetFac-" + V.Serie + V.Folio.ToString
                If Impresora = "Bullzip PDF Printer" Then
                    Dim obj As New Bullzip.PdfWriter.PdfSettings
                    obj.Init()
                    obj.PrinterName = Impresora
                    obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
                    obj.SetValue("ShowSettings", "never")
                    obj.SetValue("ShowPDF", "yes")
                    obj.SetValue("ShowSaveAS", "nofile")
                    obj.SetValue("ConfirmOverwrite", "no")
                    obj.SetValue("Target", "printer")
                    obj.WriteSettings()
                End If
                DocAImprimir = 1
                PrintDocument1.Print()
                
            End If
        End If
        imprimirNotarial()
        If V.Cliente.Email <> "" Then
            Try
                If MsgBox("¿Enviar factura por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    If V.Cliente.Email <> "" Then
                        Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                        'Dim O As New dbOpciones(MySqlcon)
                        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                        Dim C As String
                        C = "Eviado por: " + S.NombreFiscal + vbNewLine + "RFC: " + S.RFC + vbNewLine + "FACTURA" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Comprobante fiscal enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje."
                        'If pEstado = Estados.Pendiente Then
                        M.send("Factura: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\PSSFACTURA-" + TextBox11.Text + TextBox2.Text + ".pdf", "")
                        'Else
                        '   M.send("Comprobante Fiscal Digital Factura: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\FAC-" + V.Serie + V.Folio.ToString + ".pdf", RutaXml + "\FAC-" + V.Serie + V.Folio.ToString + ".xml")
                        'End If

                        PopUp("Correo enviado", 90)
                    End If
                End If
            Catch ex As Exception
                MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        End If

    End Sub
    Private Sub ImprimirRetenciones()
        Dim RutaPDF As String
        Dim Archivos As New dbSucursalesArchivos
        RutaPDF = Archivos.DaRutaArchivos(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
        RutaPDF = RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Dim Tipo As Byte
        Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.Venta)
        TipoImpresora = SA.TipoImpresora
        Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        If V.ISR <> 0 Or V.IvaRetenido <> 0 Then
            If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                LlenaNodosImpresionRet()
                If Tipo = 0 Then
                    LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencion)
                Else
                    LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
                End If
                PrintDocument1.DocumentName = "RetFac-" + V.Serie + V.Folio.ToString
                PrintDocument1.PrinterSettings.PrinterName = Impresora
                If Impresora = "Bullzip PDF Printer" Then
                    Dim obj As New Bullzip.PdfWriter.PdfSettings
                    obj.Init()
                    obj.PrinterName = Impresora
                    obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
                    obj.SetValue("ShowSettings", "never")
                    obj.SetValue("ShowPDF", "yes")
                    obj.SetValue("ShowSaveAS", "nofile")
                    obj.SetValue("ConfirmOverwrite", "no")
                    obj.SetValue("Target", "printer")
                    obj.WriteSettings()
                End If
                DocAImprimir = 1
                PrintDocument1.Print()
            End If
        End If
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
            Eselectronica = Sf.EsElectronica
            If Sf.EsElectronica > 1 Then
                CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
            End If
            Dim V As New dbVentas(MySqlcon)
            TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion).ToString
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
        End Select

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

                CD.Guardar(idVenta, 1, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, 0, Double.Parse(TextBox5.Text), 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), ComboBox7.Text)
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

            CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, 0, regDesc, 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), "")
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

                CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, 0, regDesc, 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), "")
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

                CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, 0, regDesc, 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), "")
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

            CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, 0, 0, regDesc, 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), "")
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
                CD.Modificar(idMod, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), CDbl(TextBox9.Text), Double.Parse(TextBox5.Text), 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), "")
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
            Dim F As New frmInventarioAduana(IdDetalle, 0, 0, 0, CDbl(TextBox5.Text), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
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
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            Cadena = V.CreaCadenaOriginali32(idVenta, GlobalIdMoneda)
        Else
            Cadena = V.CreaCadenaOriginali(idVenta, GlobalIdMoneda)
        End If
        'en.GuardaArchivoTexto(Application.StartupPath + "\cadena.txt", Cadena, System.Text.Encoding.UTF8)
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
        RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
        Archivos.CierraDB()
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"))
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDIb" + V.Serie + V.Folio.ToString + ".xml"
        RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + "_TIMBRADO.xml"
        RutaXMLTimbradob = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + ".xml"

        RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        Dim strXML As String
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            strXML = V.CreaXMLi32(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        Else
            strXML = V.CreaXMLi(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        End If

        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
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
            PrintDocument1.DocumentName = "PSSFACTURA-" + V.Serie + V.Folio.ToString
            Dim SA As New dbSucursalesArchivos
            Dim Impresora As String
            Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.Venta)
            TipoImpresora = SA.TipoImpresora
            If Impresora = "Bullzip PDF Printer" Then
                Dim obj As New Bullzip.PdfWriter.PdfSettings
                obj.Init()
                obj.PrinterName = Impresora
                obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
                obj.SetValue("ShowSettings", "never")
                obj.SetValue("ShowPDF", "yes")
                obj.SetValue("ShowSaveAS", "nofile")
                obj.SetValue("ConfirmOverwrite", "no")
                obj.SetValue("Target", "printer")
                obj.WriteSettings()
            End If
            PrintDocument1.PrinterSettings.PrinterName = Impresora
            LlenaNodosImpresion(Op.TituloOriginalFactura)
            If TipoImpresora = 0 Then
                LlenaNodos(V.IdSucursal, TiposDocumentos.Venta)
            Else
                LlenaNodos(V.IdSucursal, TiposDocumentos.VentaTicket)
            End If
            DocAImprimir = 0
            PrintDocument1.Print()
            If Op.ActivarCopiaFactura = 1 Then
                LlenaNodosImpresion(Op.TituloCopiaFactura)
                PrintDocument1.Print()
            End If
            If Op.ActivarCopia2Factura = 1 Then
                LlenaNodosImpresion(Op.TituloCopia2Factura)
                PrintDocument1.Print()
            End If
        Else
            MsgBox("Ha ocurrido un error en el timbrado del la factura, intente mas tarde." + vbCrLf + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            If MsgBox("¿Guardar factura como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                V.ModificaEstado(idVenta, Estados.Pendiente)
                Nuevo()
            Else
                Dim Se As New dbInventarioSeries(MySqlcon)
                Se.QuitaSeriesAVenta(idVenta)
                If V.Estado = Estados.Guardada Then V.RegresaInventario(idVenta)
                V.Eliminar(idVenta)
                PopUp("Factura Eliminada", 90)
                Nuevo()
            End If
            'Error en timbrado
        End If
    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        If PorLotes = 1 Then
            Dim F As New frmInventarioLotes(IdDetalle, 0, 0, 0, CDbl(TextBox5.Text), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
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

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged

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
                        Dim frmp As New frmContabilidadPolizas(GP.IdPoliza)
                        frmp.ShowDialog()
                        frmp.Dispose()
                    Else
                        PopUp("Póliza Generada", 90)
                    End If
                End If
                End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        

    End Sub
End Class