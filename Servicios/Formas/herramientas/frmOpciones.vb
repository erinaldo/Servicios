Public Class frmOpciones
    'Dim IdsMonedas As New elemento
    'Dim IdsAlmacenes As New elemento
    Dim NoCertificado As String
    Dim IdsSucursales As New elemento
    Dim IdsSucursales2 As New elemento
    Dim CostoAnterior As Byte
    Dim IdsCajas As New elemento
    Dim ConsultaOn As Boolean = False
    Dim IdCliente As Integer
    Dim Veces As Integer = 0
    Dim ClaveAnt As String = ""
    Dim IdImpersoraDetalle As Integer
    Dim O As dbOpciones
    Dim IdInventario As Integer
    Dim cont As Integer = 0
    Private Sub frmOpciones_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If S.IsOpen Then S.Close()
    End Sub
    Private Sub frmOpciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Dim Puerto As String = ""
        Try
            If GlobalConExtra = False Then
                Panel24.Visible = False
            End If
            ConsultaOn = False
            ComboBox9.DataSource = My.Computer.Ports.SerialPortNames
            ComboBox3.Items.Add("Promedio")
            ComboBox3.Items.Add("Costo Mayor")
            ComboBox8.Items.Add("Sin Redondeo")
            ComboBox8.Items.Add("Estandar (5.4=5,5.6=6)")
            ComboBox8.Items.Add("Hacia arriba 0.5 (7.4=7.5,5.6=6)")
            ComboBox8.Items.Add("Hacia abajo (5.7=5)")
            ComboBox8.Items.Add("Hacia arriba (7.3=7)")
            ComboBox10.Items.Add("None")
            ComboBox10.Items.Add("Odd")
            ComboBox10.Items.Add("Even")
            ComboBox10.Items.Add("Mark")
            ComboBox10.Items.Add("Space")
            ComboBox12.Items.Add("1")
            ComboBox12.Items.Add("2")
            ComboBox12.Items.Add("3")
            ComboBox12.Items.Add("4")
            ComboBox12.Items.Add("5")
            ComboBox11.Items.Add("None")
            ComboBox11.Items.Add("XOnXOff")
            ComboBox11.Items.Add("RequestToSend")
            ComboBox11.Items.Add("RequestToSendXOnOff")

            ComboBox7.Items.Add("Remisión")
            ComboBox7.Items.Add("Pedido")
            ComboBox7.Items.Add("Cotización")
            ComboBox13.Items.Add("Remision")
            ComboBox13.Items.Add("Pedido")
            ComboBox13.Items.Add("Cotización")
            'ComboBox13.Items.Add("Factura")

            ComboBox15.Items.Add("yyyy/MM/dd")
            ComboBox15.Items.Add("dd/MM/yyyy")
            ComboBox15.SelectedIndex = 0
            ComboBox18.Items.Add("Por cantidad")
            ComboBox18.Items.Add("Por Importe")
            ComboBox18.SelectedIndex = 0
            'ConsultaOn = False
            'LlenaCombos("tblmonedas", cmbmoneda, "nombre", "nombrem", "idmoneda", IdsMonedas, "idmoneda>1")
            'LlenaCombos("tblalmacenes", ComboBox1, "nombre", "nombret", "idalmacen", IdsAlmacenes, , "Sel. Alamacen")
            LlenaCombos("tblsucursales", ComboBox2, "nombre", "nombret", "idsucursal", IdsSucursales, , "Sel. Sucursal")
            LlenaCombos("tblsucursales", ComboBox4, "nombre", "nombret", "idsucursal", IdsSucursales, , "Sel. Sucursal")
            LlenaCombos("tblsucursales", ComboBox5, "nombre", "nombret", "idsucursal", IdsSucursales, , "Sel. Sucursal")
            LlenaCombos("tblsucursales", ComboBox14, "nombre", "nombret", "idsucursal", IdsSucursales, , "Sel. Sucursal")
            LlenaCombos("tblsucursales", ComboBox16, "nombre", "nombret", "idsucursal", IdsSucursales, , "Sel. Sucursal")
            LlenaCombos("tblsucursales", ComboBox17, "nombre", "nombret", "idsucursal", IdsSucursales, , "Sel. Sucursal")

            LlenaCombos("tblsucursales", ComboBox19, "nombre", "nombret", "idsucursal", IdsSucursales2)

            'ConsultaOn = True
            TextBox23.Text = GlobaltpBanxico
            O = New dbOpciones(MySqlcon)
            'TextBox1.Text = O.Imp.ToString
            If O._ConsultaRealTime = 1 Then
                CheckBox1.Checked = True
            End If
            'ComboBox1.SelectedIndex = IdsAlmacenes.Busca(O._idAlmacen)
            ComboBox3.SelectedIndex = O._TipoCosteo
            CostoAnterior = O._TipoCosteo
            'cmbmoneda.SelectedIndex = IdsMonedas.Busca(O._IdMoneda)
            'TextBox24.Text = O._NombreEmpresa
            'TextBox23.Text = O._RFC
            'TextBox5.Text = O._Calle
            'TextBox4.Text = O._noExterior
            'TextBox6.Text = O._noInterior
            'TextBox3.Text = O._Colonia
            'TextBox8.Text = O._Municipio
            'TextBox7.Text = O._Localidad
            'TextBox12.Text = O._ReferenciaDomicilio
            'TextBox10.Text = O._Estado
            'TextBox9.Text = O._CodigoPostal
            'TextBox11.Text = O._DetalleKits

            'TextBox25.Text = O._NombreEmpresaLocal
            'TextBox18.Text = O._TipoSelAlmacen
            'TextBox16.Text = O._noExteriorLocal
            'TextBox15.Text = O._noInteriorLocal
            'TextBox17.Text = O._AvisoCosto
            'TextBox13.Text = O._MunicipioLocal
            'TextBox14.Text = O._LocalidadLocal
            'TextBox21.Text = O._ReferenciaDomicil
            'TextBox20.Text = O._EstadoLocal
            'TextBox19.Text = O._CodigoPostalLocal
            TextBox22.Text = O._ApiKey
            TextBox50.Text = O._DireccionTimbrado
            TextBox52.Text = O._UsuarioFacCom
            TextBox51.Text = O._passFacCom
            TextBox53.Text = O._formatocantidad
            TextBox54.Text = O._formatoPrecioU
            TextBox55.Text = O._formatoImporte
            TextBox56.Text = O._formatoSubtotal
            TextBox57.Text = O._formatoIva
            TextBox58.Text = O._formatoTotal

            DateTimePicker2.Value = O.FechaAdd
            DateTimePicker1.Value = O.FechaVen
            TextBox63.Text = O.Timbres.ToString
            TextBox64.Text = O.AvisoTimbres
            TextBox65.Text = O.AvisoDias
            TextBox81.Text = O.EspacioCantidad.ToString
            TextBox80.Text = O.EspacioSubtotal.ToString
            TextBox83.Text = O.EspacioPrecioUnitacio.ToString
            TextBox82.Text = O.EspacioIva.ToString
            TextBox85.Text = O.EspacioImporte.ToString
            TextBox84.Text = O.Espaciototal.ToString
            If O._TipoFacturacion = 0 Then CheckBox7.Checked = True
            If O._TipoFacturacion = 1 Then CheckBox6.Checked = True
            If O._TipoFacturacion = 2 Then CheckBox5.Checked = True
            If O._TipoFacturacion = 3 Then CheckBox78.Checked = True
            If O.CotizarSoloExistencia = 1 Then CheckBox17.Checked = True
            If O._NCDosPasos = 1 Then CheckBox13.Checked = True
            If O._PacCFDI = 0 Then CheckBox9.Checked = True
            If O._PacCFDI = 1 Then CheckBox10.Checked = True
            If O._PacCFDI = 2 Then CheckBox34.Checked = True
            If O._NoRutas = "1" Then CheckBox43.Checked = True
            If O._LimitarCredito = 1 Then CheckBox15.Checked = True
            If O._IVaCero = 1 Then CheckBox16.Checked = True
            If O.BuscaxFabricante = 1 Then CheckBox28.Checked = True
            'TextBox62.Text = O._PassCredito
            If O._AgregaSeriesAVenta = 1 Then CheckBox14.Checked = True
            If O.PreguntarImpresora = 1 Then CheckBox18.Checked = True
            If O._ApartadosInventariable = "1" Then CheckBox32.Checked = True
            If O._OrdenUbicacion = "1" Then CheckBox23.Checked = True
            If O._IgualarFechaTimbrado = 1 Then CheckBox44.Checked = True
            If O._CalculoAlterno = "1" Then CheckBox45.Checked = True
            If O.BusquedaporClases = 1 Then CheckBox51.Checked = True
            If O.BoletasInventario = 1 Then CheckBox59.Checked = True
            If O.BoletasResumida = 1 Then CheckBox60.Checked = True
            If O.SiemprePorSurtirRemisiones = 1 Then CheckBox62.Checked = True
            If O.SiemprePorSurtirVentas = 1 Then CheckBox61.Checked = True
            If O.Copiaflujoventas = 1 Then CheckBox63.Checked = True
            If O.Copiaflujorem = 1 Then CheckBox64.Checked = True
            If O.IntegrarContabilidad = 1 Then CheckBox65.Checked = True
            If O.PVVentabaCompleta = 1 Then CheckBox66.Checked = True
            If O._ModoFoliosB = "1" Then CheckBox67.Checked = True
            If O.ChecaFolioFacturas = 1 Then CheckBox68.Checked = True
            If O.RecibidoDefault = 1 Then CheckBox69.Checked = True
            If O.NoBloquearCreardesde = 1 Then CheckBox70.Checked = True
            If O.PVConfirmarCobrar = 1 Then CheckBox72.Checked = True
            If O.NoImpSinGuardar = 1 Then CheckBox73.Checked = True
            If O.RemisionesSinDetalleCD = 1 Then CheckBox74.Checked = True
            If O.InicioRapido = 1 Then CheckBox79.Checked = True
            If O.ActNom12 = 1 Then CheckBox11.Checked = True
            If GlobalIdUsuario <> 1000 Then
                CheckBox11.Visible = False
            End If
            'TextBox26.Text = My.Settings.rutacer
            'TextBox27.Text = My.Settings.rutakey
            'TextBox28.Text = My.Settings.passwordkey
            If O.CierreConVentana = 1 Then CheckBox19.Checked = True
            IdCliente = O.IdClienteDefault
            ComboBox8.SelectedIndex = O.TipoRedondeo
            ComboBox18.SelectedIndex = O.TipoProrrateo
            IdInventario = O.IdInventarioCD
            If IdInventario > 0 Then
                Dim Inv As New dbInventario(IdInventario, MySqlcon)
                TextBox6.Text = Inv.Nombre
            End If
            'TextBox39.Text = My.Settings.rutafacturafondo
            ComboBox12.Text = O.DecimalesRedondeo.ToString
            If O._DetalleKits = "1" Then CheckBox33.Checked = True
            If O._ConectorEnviarCorreos = "1" Then CheckBox37.Checked = True
            TextBox42.Text = My.Settings.impresoraPDF
            'TextBox35.Text = My.Settings.rutancfondo

            CheckBox3.Checked = My.Settings.multiplesventanas


            'chkbancos.Checked = My.Settings.cantidadcodigo
            CheckBox12.Checked = My.Settings.preguntarsalir

            CheckBox8.Checked = My.Settings.conceptocero
            'If My.Settings.rutacer <> "" Then
            'LeeArchivoCer(TextBox26.Text)
            'End If
            If My.Settings.menusextendidos Then
                CheckBox2.Checked = True
            End If
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, True)
            'ConsultaOn = False
            ComboBox19.SelectedIndex = IdsSucursales2.Busca(sa.IdSucursal)
            'ConsultaOn = True
            ComboBox13.SelectedIndex = 0

            TextBox49.Text = sa.RutaPFX
            TextBox48.Text = sa.PassPFX
            TextBox87.Text = O.TituloFactura
            TextBox86.Text = O.TituloParcialidad

            TextBox91.Text = O.BasculaBaundRate.ToString
            ComboBox10.SelectedIndex = O.BasculaParity
            TextBox92.Text = O.BasculaDataBits.ToString
            ComboBox11.SelectedIndex = O.BasculaHandshake
            TextBox24.Text = O.NoProveedorSoriana.ToString
            TextBox25.Text = O.NoProveedorWalmart.ToString
            Puerto = O.PuertoBascula
            TextBox93.Text = O.BasculaSecuencia
            ComboBox15.Text = O.FormatoFechaPv
            If O.PedirPrecio = 1 Then CheckBox20.Checked = True
            If O._CodigoPostalLocal = "1" Then CheckBox23.Checked = True
            If O.CostoTiempoReal = 1 Then CheckBox21.Checked = True
            If O.EliminarRefPV = 1 Then CheckBox22.Checked = True
            If O._TipoSelAlmacen = "1" Then CheckBox24.Checked = True
            If O._AvisoCosto = "1" Then CheckBox31.Checked = True
            If O.NParcialidades = 1 Then CheckBox29.Checked = True
            If O.BuscaModoB = 1 Then CheckBox30.Checked = True
            If O._MetodoUtilidad = "1" Then CheckBox35.Checked = True
            If O._VersionConector = "1" Then CheckBox36.Checked = True
            If O._CursorVentas = "1" Then CheckBox38.Checked = True
            If O._ActivarPDF = "1" Then CheckBox40.Checked = True
            If O._MostrarPDF = "1" Then CheckBox39.Checked = True
            If O._ConectorMunrec = "1" Then CheckBox41.Checked = True
            If O._Sinnegativos = "1" Then CheckBox42.Checked = True
            If O.MostrarPredial = 1 Then CheckBox46.Checked = True
            If O.ClientesMayusculas = 1 Then CheckBox47.Checked = True
            If O.ClienteBloquearCodigo = 1 Then CheckBox48.Checked = True
            If O.NoPermitirFacturasdeCredito = 1 Then CheckBox49.Checked = True
            If O.NoPermitirRemisionesdeCredito = 1 Then CheckBox50.Checked = True
            If O.MaximizarVentas = 1 Then CheckBox52.Checked = True
            If O.IntegrarBancosVentasPagos = 1 Then chkbancos.Checked = True
            If O.IntegrarBancosVentasContado = 1 Then chkbancosvc.Checked = True
            If O.IntegrarBancosComprasPagos = 1 Then chkbancoscp.Checked = True
            If O.IntegrarBancosComprasContado = 1 Then chkbancoscc.Checked = True
            If O.ClientesSinRepetir = 1 Then CheckBox56.Checked = True
            If O.SobreEscribeImpLocales = 1 Then CheckBox57.Checked = True
            If O.FacturarSoloExistencia = 1 Then CheckBox4.Checked = True
            If O.TipoCostoPrecios = 1 Then CheckBox58.Checked = True
            If My.Settings.abrepunto = "1" Then CheckBox25.Checked = True
            If O.FacturarPagosRemisiones = 1 Then CheckBox54.Checked = True
            If O.PedirAnticipoRemisiones = 1 Then CheckBox55.Checked = True
            If O.FacturarSoloaCredito = 1 Then CheckBox76.Checked = True
            If O.RemisionesSoloaCredito = 1 Then CheckBox75.Checked = True
            If O.VendedorUsuario = 1 Then CheckBox77.Checked = True
            txtoriginalfactura.Text = O.TituloOriginalFactura
            txtoriginalremision.Text = O.TituloOriginalRemision
            txtcopiafactura.Text = O.TituloCopiaFactura
            txtcopiaremision.Text = O.TituloCopiaRemision
            txtcopia2factura.Text = O.TituloCopia2Factura
            txtcopia2remision.Text = O.TituloCopia2Remision
            If O.ActivarCopiaFactura = 1 Then chkactivarcopia.Checked = True
            If O.ActivarCopiaRemision = 1 Then chkactivarcopiar.Checked = True
            If O.ActivarCopia2Factura = 1 Then chkactivarcopia2.Checked = True
            If O.ActivarCopia2Remision = 1 Then chkactivarcopia2r.Checked = True

            If O.ActivarCopiaFacturaCredito = 1 Then chkactivacopiacre.Checked = True
            If O.ActivarCopiaRemisionCredito = 1 Then chkactivacopiaremisioncre.Checked = True
            If O.ActivarCopiaFacturaCredito2 = 1 Then chkactivacopiacre2.Checked = True
            If O.ActivarCopiaRemisionCredito2 = 1 Then chkactivacopiaremisioncre2.Checked = True

            If O.FacturarSoloaCredito = 1 Then CheckBox76.Checked = True
            If O.RemisionesSoloaCredito = 1 Then CheckBox75.Checked = True

            If O.PagosTicket = 1 Then CheckBox53.Checked = True
            NoCertificado = O._noCertificado
            'ConsultaOn = False
            ComboBox2.SelectedIndex = IdsSucursales.Busca(sa.IdSucursal)
            ComboBox5.SelectedIndex = ComboBox2.SelectedIndex
            'ConsultaOn = True
            ComboBox4.SelectedIndex = ComboBox2.SelectedIndex


            ComboBox14.SelectedIndex = IdsSucursales.Busca(O.idSucursalconector)
            TextBox97.Text = O.Serieconector
            TextBox98.Text = O.SerieNCConecto
            Panel8.BackColor = Color.FromArgb(O.PVRojo, O.PVVerde, O.PVAzul)
            If ComboBox2.SelectedIndex > 0 Then ComboBox6.SelectedIndex = IdsCajas.Busca(sa.idCaja)
            ComboBox7.SelectedIndex = sa.Documentopv
            Dim C As New dbClientes(IdCliente, MySqlcon)
            TextBox88.Text = C.Nombre
            If GlobalconIntegracion = False Then
                chkbancos.Visible = False
                CheckBox65.Visible = False
            End If
            ConsultaOn = True
            DaImpresoras(sa.IdSucursal)
            ConsultaOC()
            'ConsultaImpresorasDetalles()
            Label23.Text = "Test: " + cont.ToString
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        Try
            ComboBox9.Text = Puerto
        Catch ex As Exception
            MsgBox("Puerto serial desconocido.", MsgBoxStyle.Information, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DaRutas(ByVal pIdSucursal As Integer)
        Dim SA As New dbSucursalesArchivos
        TextBox40.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
        TextBox41.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
        TextBox34.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoXML, False)
        TextBox33.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoPDF, False)
        TextBox45.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.DevolucionesXML, False)
        TextBox44.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.DevolucionesPDF, False)
        TextBox36.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoPDF, False)
        TextBox43.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoXML, False)
        TextBox47.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.CotizacionesPDF, False)
        TextBox59.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.PedidosPDF, False)
        TextBox60.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.RemisionesPDF, False)
        TextBox61.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, False)
        TextBox67.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, 250, False)
        TextBox89.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, 249, False)
        TextBox110.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NominasXML, False)
        TextBox109.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NominasPDF, False)
        TextBox12.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FertilizantesPDF, False)
        TextBox13.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.Validador, False)
        TextBox14.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.AValidadar, False)
        TextBox15.Text = SA.DaRutaArchivos(pIdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.Validadas, False)
        SA.CierraDB()
        cont += 1
        Try
            If TextBox89.Text <> "" Then PictureBox2.BackgroundImage = Image.FromFile(TextBox89.Text)
        Catch ex As Exception
            MsgBox("No se pudo cargar la imagen de punto de venta.", MsgBoxStyle.Information, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DaImpresoras(ByVal pidSucursal As Integer)
        If ConsultaOn = False Then Exit Sub
        Dim SA As New dbSucursalesArchivos
        TextBox42.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.Venta)
        If SA.Activa = 1 Then RadioButton1.Checked = True
        TextBox66.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.Venta)
        If SA.Activa = 1 Then RadioButton2.Checked = True

        TextBox69.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.VentaRemision)
        If SA.Activa = 1 Then RadioButton4.Checked = True
        TextBox68.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.VentaRemision)
        If SA.Activa = 1 Then RadioButton3.Checked = True

        TextBox71.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.VentaCotizacion)
        If SA.Activa = 1 Then RadioButton6.Checked = True
        TextBox70.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.VentaCotizacion)
        If SA.Activa = 1 Then RadioButton5.Checked = True

        TextBox73.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.VentaPedido)
        If SA.Activa = 1 Then RadioButton8.Checked = True
        TextBox72.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.VentaPedido)
        If SA.Activa = 1 Then RadioButton7.Checked = True

        TextBox75.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.VentaNotadeCredito)
        If SA.Activa = 1 Then RadioButton10.Checked = True
        TextBox74.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.VentaNotadeCredito)
        If SA.Activa = 1 Then RadioButton9.Checked = True

        TextBox77.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.VentaNotadeCargo)
        If SA.Activa = 1 Then RadioButton12.Checked = True
        TextBox76.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.VentaNotadeCargo)
        If SA.Activa = 1 Then RadioButton11.Checked = True

        TextBox79.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.VentaDevolucion)
        If SA.Activa = 1 Then RadioButton14.Checked = True
        TextBox78.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.VentaDevolucion)
        If SA.Activa = 1 Then RadioButton13.Checked = True

        TextBox102.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.Compra)
        If SA.Activa = 1 Then RadioButton18.Checked = True
        TextBox101.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.Compra)
        If SA.Activa = 1 Then RadioButton17.Checked = True

        TextBox100.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.MovimientosInventario)
        If SA.Activa = 1 Then RadioButton16.Checked = True
        TextBox99.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.MovimientosInventario)
        If SA.Activa = 1 Then RadioButton15.Checked = True


        TextBox104.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.CajasMovimientos)
        If SA.Activa = 1 Then RadioButton20.Checked = True
        TextBox103.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.CajasMovimientos)
        If SA.Activa = 1 Then RadioButton19.Checked = True

        TextBox106.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.VentasApartados)
        If SA.Activa = 1 Then RadioButton22.Checked = True
        TextBox105.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.VentasApartados)
        If SA.Activa = 1 Then RadioButton21.Checked = True

        'Compras
        txtPreordenEstatico.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.CompraCotizacion)
        If SA.Activa = 1 Then rdbPreOrdenE.Checked = True
        txtPreOrdenFlujo.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.CompraCotizacion)
        If SA.Activa = 1 Then rdbPreOrdenF.Checked = True

        txtOrdenCompraE.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.CompraPedido)
        If SA.Activa = 1 Then rdbOrdenCompraE.Checked = True
        txtOrdenCompraF.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.CompraPedido)
        If SA.Activa = 1 Then rdbOrdenCompraF.Checked = True

        txtRemisionE.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.CompraRemision)
        If SA.Activa = 1 Then rdbRemisionE.Checked = True
        txtRemisionF.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.CompraRemision)
        If SA.Activa = 1 Then rdbRemisionF.Checked = True

        txtDevolucionE.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.CompraDevolucion)
        If SA.Activa = 1 Then rdbDevolucionE.Checked = True
        txtDevolucionF.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.CompraDevolucion)
        If SA.Activa = 1 Then rdbDevolucionF.Checked = True

        txtNotasCreditoE.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.CompraNotadeCredito)
        If SA.Activa = 1 Then rdbNotasCreditoE.Checked = True
        txtNotasCreditoF.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.CompraNotadeCredito)
        If SA.Activa = 1 Then rdbNotasCreditoF.Checked = True

        txtNotasCargoE.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.CompraNotadeCargo)
        If SA.Activa = 1 Then rdbNotasCargoE.Checked = True
        txtNotasCargoF.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.CompraNotadeCargo)
        If SA.Activa = 1 Then rdbNotasCargoF.Checked = True

        TextBox108.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.Nominas)
        If SA.Activa = 1 Then RadioButton40.Checked = True
        TextBox107.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.Nominas)
        If SA.Activa = 1 Then RadioButton39.Checked = True

        TextBox111.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.PDF)

        TextBox5.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.Gastos)

        TextBox3.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.Empenios)
        If SA.Activa = 1 Then RadioButton42.Checked = True
        TextBox1.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.Empenios)
        If SA.Activa = 1 Then RadioButton41.Checked = True

        TextBox11.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.ContaPoliza)
        If SA.Activa = 1 Then RadioButton46.Checked = True
        TextBox10.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.ContaPoliza)
        If SA.Activa = 1 Then RadioButton45.Checked = True

        TextBox9.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.FertilizantesPedido)
        If SA.Activa = 1 Then RadioButton48.Checked = True
        TextBox8.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.FertilizantesPedido)
        If SA.Activa = 1 Then RadioButton47.Checked = True

        TextBox17.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.SemillasLiquidacion)
        If SA.Activa = 1 Then RadioButton50.Checked = True
        TextBox16.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.SemillasLiquidacion)
        If SA.Activa = 1 Then RadioButton49.Checked = True

        TextBox19.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.InventarioPedidos)
        If SA.Activa = 1 Then RadioButton52.Checked = True
        TextBox18.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.InventarioPedidos)
        If SA.Activa = 1 Then RadioButton51.Checked = True

        TextBox20.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.RestauranteTicket)
        If SA.Activa = 1 Then RadioButton54.Checked = True
        TextBox7.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.RestauranteTicket)
        If SA.Activa = 1 Then RadioButton53.Checked = True

        TextBox27.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 0, TiposDocumentos.ComplementoPagos)
        If SA.Activa = 1 Then RadioButton56.Checked = True
        TextBox26.Text = SA.DaImpresoraPorTipo(pidSucursal, GlobalIdEmpresa, False, 1, TiposDocumentos.ComplementoPagos)
        If SA.Activa = 1 Then RadioButton55.Checked = True

        SA.CierraDB()
    End Sub
    Private Sub GuardaImpresoras(ByVal pidSucursal As Integer)
        If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.OpcionesModificar, PermisosN.Secciones.Herramientas) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Dim SA As New dbSucursalesArchivos
        Dim Act As Byte = 0
        If RadioButton1.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox42.Text, GlobalIdEmpresa, Act, TiposDocumentos.Venta)
        Act = 0
        If RadioButton2.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox66.Text, GlobalIdEmpresa, Act, TiposDocumentos.Venta)

        Act = 0
        If RadioButton4.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox69.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaRemision)
        Act = 0
        If RadioButton3.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox68.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaRemision)

        Act = 0
        If RadioButton6.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox71.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaCotizacion)
        Act = 0
        If RadioButton5.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox70.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaCotizacion)

        Act = 0
        If RadioButton8.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox73.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaPedido)
        Act = 0
        If RadioButton7.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox72.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaPedido)

        Act = 0
        If RadioButton10.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox75.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaNotadeCredito)
        Act = 0
        If RadioButton9.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox74.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaNotadeCredito)

        Act = 0
        If RadioButton12.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox77.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaNotadeCargo)
        Act = 0
        If RadioButton11.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox76.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaNotadeCargo)

        Act = 0
        If RadioButton14.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox79.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaDevolucion)
        Act = 0
        If RadioButton13.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox78.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentaDevolucion)

        Act = 0
        If RadioButton18.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox102.Text, GlobalIdEmpresa, Act, TiposDocumentos.Compra)
        Act = 0
        If RadioButton17.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox101.Text, GlobalIdEmpresa, Act, TiposDocumentos.Compra)

        Act = 0
        If RadioButton16.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox100.Text, GlobalIdEmpresa, Act, TiposDocumentos.MovimientosInventario)
        Act = 0
        If RadioButton15.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox99.Text, GlobalIdEmpresa, Act, TiposDocumentos.MovimientosInventario)

        Act = 0
        If RadioButton20.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox104.Text, GlobalIdEmpresa, Act, TiposDocumentos.CajasMovimientos)
        Act = 0
        If RadioButton19.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox103.Text, GlobalIdEmpresa, Act, TiposDocumentos.CajasMovimientos)

        Act = 0
        If RadioButton22.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox106.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentasApartados)
        Act = 0
        If RadioButton21.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox105.Text, GlobalIdEmpresa, Act, TiposDocumentos.VentasApartados)

        'anadidas
        Act = 0
        If rdbPreOrdenE.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, txtPreordenEstatico.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraCotizacion)
        Act = 0
        If rdbPreOrdenF.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, txtPreOrdenFlujo.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraCotizacion)

        Act = 0
        If rdbOrdenCompraE.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, txtOrdenCompraE.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraPedido)
        Act = 0
        If rdbOrdenCompraF.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, txtOrdenCompraF.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraPedido)

        Act = 0
        If rdbRemisionE.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, txtRemisionE.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraRemision)
        Act = 0
        If rdbRemisionF.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, txtRemisionF.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraRemision)

        Act = 0
        If rdbDevolucionE.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, txtDevolucionE.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraDevolucion)
        Act = 0
        If rdbDevolucionF.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, txtDevolucionF.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraDevolucion)

        Act = 0
        If rdbNotasCreditoE.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, txtNotasCreditoE.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraNotadeCredito)
        Act = 0
        If rdbNotasCreditoF.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, txtNotasCreditoF.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraNotadeCredito)

        Act = 0
        If rdbNotasCargoE.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, txtNotasCargoE.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraNotadeCargo)
        Act = 0
        If rdbNotasCargoF.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, txtNotasCargoF.Text, GlobalIdEmpresa, Act, TiposDocumentos.CompraNotadeCargo)

        Act = 0
        If RadioButton40.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox108.Text, GlobalIdEmpresa, Act, TiposDocumentos.Nominas)
        Act = 0
        If RadioButton39.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox107.Text, GlobalIdEmpresa, Act, TiposDocumentos.Nominas)

        Act = 0
        If RadioButton42.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox3.Text, GlobalIdEmpresa, Act, TiposDocumentos.Empenios)
        Act = 0
        If RadioButton41.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox1.Text, GlobalIdEmpresa, Act, TiposDocumentos.Empenios)

        Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox111.Text, GlobalIdEmpresa, Act, TiposDocumentos.PDF)
        Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox5.Text, GlobalIdEmpresa, Act, TiposDocumentos.Gastos)

        Act = 0
        If RadioButton46.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox11.Text, GlobalIdEmpresa, Act, TiposDocumentos.ContaPoliza)
        Act = 0
        If RadioButton45.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox10.Text, GlobalIdEmpresa, Act, TiposDocumentos.ContaPoliza)

        Act = 0
        If RadioButton48.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox9.Text, GlobalIdEmpresa, Act, TiposDocumentos.FertilizantesPedido)
        Act = 0
        If RadioButton47.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox8.Text, GlobalIdEmpresa, Act, TiposDocumentos.FertilizantesPedido)

        Act = 0
        If RadioButton50.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox17.Text, GlobalIdEmpresa, Act, TiposDocumentos.SemillasLiquidacion)
        Act = 0
        If RadioButton49.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox16.Text, GlobalIdEmpresa, Act, TiposDocumentos.SemillasLiquidacion)

        Act = 0
        If RadioButton52.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox19.Text, GlobalIdEmpresa, Act, TiposDocumentos.InventarioPedidos)
        Act = 0
        If RadioButton51.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox18.Text, GlobalIdEmpresa, Act, TiposDocumentos.InventarioPedidos)

        Act = 0
        If RadioButton54.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox20.Text, GlobalIdEmpresa, Act, TiposDocumentos.RestauranteTicket)
        Act = 0
        If RadioButton53.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox7.Text, GlobalIdEmpresa, Act, TiposDocumentos.RestauranteTicket)

        Act = 0
        If RadioButton56.Checked Then Act = 1
        SA.GuardaImpresora(0, pidSucursal, TextBox27.Text, GlobalIdEmpresa, Act, TiposDocumentos.ComplementoPagos)
        Act = 0
        If RadioButton55.Checked Then Act = 1
        SA.GuardaImpresora(1, pidSucursal, TextBox26.Text, GlobalIdEmpresa, Act, TiposDocumentos.ComplementoPagos)

        SA.CierraDB()
    End Sub
    Private Sub GuardaRutas(ByVal pidSucursal As Integer)
        If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.OpcionesModificar, PermisosN.Secciones.Herramientas) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Dim SA As New dbSucursalesArchivos
        SA.GuardaRutaArchivos(pidSucursal, TextBox40.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML)
        SA.GuardaRutaArchivos(pidSucursal, TextBox41.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF)
        SA.GuardaRutaArchivos(pidSucursal, TextBox34.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoXML)
        SA.GuardaRutaArchivos(pidSucursal, TextBox33.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoPDF)
        SA.GuardaRutaArchivos(pidSucursal, TextBox45.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.DevolucionesXML)
        SA.GuardaRutaArchivos(pidSucursal, TextBox44.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.DevolucionesPDF)
        SA.GuardaRutaArchivos(pidSucursal, TextBox36.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoPDF)
        SA.GuardaRutaArchivos(pidSucursal, TextBox43.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoXML)
        SA.GuardaRutaArchivos(pidSucursal, TextBox47.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.CotizacionesPDF)
        SA.GuardaRutaArchivos(pidSucursal, TextBox59.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.PedidosPDF)
        SA.GuardaRutaArchivos(pidSucursal, TextBox60.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.RemisionesPDF)
        SA.GuardaRutaArchivos(pidSucursal, TextBox61.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF)
        SA.GuardaRutaArchivos(pidSucursal, TextBox110.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NominasXML)
        SA.GuardaRutaArchivos(pidSucursal, TextBox109.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NominasPDF)
        SA.GuardaRutaArchivos(pidSucursal, TextBox12.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FertilizantesPDF)
        SA.GuardaRutaArchivos(pidSucursal, TextBox13.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.Validador)
        SA.GuardaRutaArchivos(pidSucursal, TextBox14.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.AValidadar)
        SA.GuardaRutaArchivos(pidSucursal, TextBox15.Text, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.Validadas)
        SA.GuardaRutaArchivos(pidSucursal, TextBox67.Text, GlobalIdEmpresa, 250)
        SA.GuardaRutaArchivos(pidSucursal, TextBox89.Text, GlobalIdEmpresa, 249)
        GlobalRutaConector = TextBox67.Text

        SA.CierraDB()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GuardarCambios()
    End Sub
    Private Sub GuardarCambios()
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.OpcionesModificar, PermisosN.Secciones.Herramientas) = False Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            Dim O As New dbOpciones(MySqlcon)
            'If IsNumeric(TextBox1.Text) Then
            
            'O.Imp = CDbl(TextBox1.Text)
            If CheckBox1.Checked Then
                O._ConsultaRealTime = 1
                GlobalConsultaTiempoReal = True
            Else
                O._ConsultaRealTime = 0
                GlobalConsultaTiempoReal = False
            End If
            'O._IdMoneda = IdsMonedas.Valor(cmbmoneda.SelectedIndex)
            'O._idAlmacen = IdsAlmacenes.Valor(ComboBox1.SelectedIndex)
            GlobalIdMoneda = 2

            'O._NombreEmpresa = Trim(TextBox24.Text)
            'O._RFC = Trim(TextBox23.Text)
            'O._Calle = Trim(TextBox5.Text)
            'O._noExterior = Trim(TextBox4.Text)
            'O._noInterior = Trim(TextBox6.Text)
            'O._Colonia = Trim(TextBox3.Text)
            'O._Municipio = Trim(TextBox8.Text)
            'O._Localidad = Trim(TextBox7.Text)
            'O._ReferenciaDomicilio = Trim(TextBox12.Text)
            'O._Estado = Trim(TextBox10.Text)
            'O._CodigoPostal = Trim(TextBox9.Text)
            'O._Pais = Trim(TextBox11.Text)

            'O._NombreEmpresaLocal = Trim(TextBox25.Text)
            'O._TipoSelAlmacen = Trim(TextBox18.Text)
            'O._noExteriorLocal = Trim(TextBox16.Text)
            'O._noInteriorLocal = Trim(TextBox15.Text)
            'O._AvisoCosto = Trim(TextBox17.Text)
            'O._MunicipioLocal = Trim(TextBox13.Text)
            'O._LocalidadLocal = Trim(TextBox14.Text)
            'O._ReferenciaDomicilioLocal = Trim(TextBox21.Text)
            'O._EstadoLocal = Trim(TextBox20.Text)
            'O._CodigoPostalLocal = Trim(TextBox19.Text)
            O._ApiKey = Trim(TextBox22.Text)
            O._noCertificado = Trim(NoCertificado)
            O._DireccionTimbrado = Trim(TextBox50.Text)
            O._formatocantidad = Trim(TextBox53.Text)
            O._formatoPrecioU = Trim(TextBox54.Text)
            O._formatoImporte = Trim(TextBox55.Text)
            O._formatoSubtotal = Trim(TextBox56.Text)
            O._formatoIva = Trim(TextBox57.Text)
            O._formatoTotal = Trim(TextBox58.Text)
            O._TipoCosteo = ComboBox3.SelectedIndex

            O.EspacioCantidad = CByte(TextBox81.Text)
            O.EspacioSubtotal = CByte(TextBox80.Text)
            O.EspacioPrecioUnitacio = CByte(TextBox83.Text)
            O.EspacioIva = CByte(TextBox82.Text)
            O.EspacioImporte = CByte(TextBox85.Text)
            O.Espaciototal = CByte(TextBox84.Text)

            GlobalTipoCosteo = O._TipoCosteo
            If CheckBox9.Checked Then O._PacCFDI = 0
            If CheckBox10.Checked Then O._PacCFDI = 1
            If CheckBox34.Checked Then O._PacCFDI = 2
            If CheckBox7.Checked Then O._TipoFacturacion = 0
            If CheckBox6.Checked Then O._TipoFacturacion = 1
            If CheckBox5.Checked Then O._TipoFacturacion = 2
            If CheckBox78.Checked Then O._TipoFacturacion = 3
            If CheckBox13.Checked Then
                O._NCDosPasos = 1
            Else
                O._NCDosPasos = 0
            End If
            If CheckBox43.Checked Then
                O._NoRutas = "1"
            Else
                O._NoRutas = "0"
            End If
            If CheckBox14.Checked Then
                O._AgregaSeriesAVenta = 1
            Else
                O._AgregaSeriesAVenta = 0
            End If
            If CheckBox15.Checked Then
                O._LimitarCredito = 1
            Else
                O._LimitarCredito = 0
            End If
            If CheckBox16.Checked Then
                O._IVaCero = 1
            Else
                O._IVaCero = 0
            End If
            If CheckBox21.Checked Then
                O.CostoTiempoReal = 1
            Else
                O.CostoTiempoReal = 0
            End If
            If CheckBox17.Checked Then
                O.CotizarSoloExistencia = 1
            Else
                O.CotizarSoloExistencia = 0
            End If
            If CheckBox18.Checked Then
                O.PreguntarImpresora = 1
            Else
                O.PreguntarImpresora = 0
            End If
            If CheckBox20.Checked Then
                O.PedirPrecio = 1
            Else
                O.PedirPrecio = 0
            End If
            If CheckBox33.Checked Then
                O._DetalleKits = "1"
            Else
                O._DetalleKits = "0"
            End If
            If CheckBox22.Checked Then
                O.EliminarRefPV = 1
            Else
                O.EliminarRefPV = 0
            End If
            If CheckBox28.Checked Then
                O.BuscaxFabricante = 1
            Else
                O.BuscaxFabricante = 0
            End If
            If CheckBox32.Checked Then
                O._ApartadosInventariable = "1"
            Else
                O._ApartadosInventariable = "0"
            End If
            If CheckBox35.Checked Then
                O._MetodoUtilidad = "1"
            Else
                O._MetodoUtilidad = "0"
            End If

            If CheckBox36.Checked Then
                O._VersionConector = "1"
            Else
                O._VersionConector = "0"
            End If
            If CheckBox37.Checked Then
                O._ConectorEnviarCorreos = "1"
            Else
                O._ConectorEnviarCorreos = "0"
            End If
            If CheckBox38.Checked Then
                O._CursorVentas = "1"
            Else
                O._CursorVentas = "0"
            End If
            If CheckBox40.Checked Then
                O._ActivarPDF = "1"
            Else
                O._ActivarPDF = "0"
            End If
            If CheckBox23.Checked Then
                O._OrdenUbicacion = "1"
            Else
                O._OrdenUbicacion = "0"
            End If
            If CheckBox39.Checked Then
                O._MostrarPDF = "1"
            Else
                O._MostrarPDF = "0"
            End If
            If CheckBox41.Checked Then
                O._ConectorMunrec = "1"
            Else
                O._ConectorMunrec = "0"
            End If
            If CheckBox42.Checked Then
                O._Sinnegativos = "1"
            Else
                O._Sinnegativos = "0"
            End If
            If CheckBox44.Checked Then
                O._IgualarFechaTimbrado = 1
            Else
                O._IgualarFechaTimbrado = 0
            End If
            If CheckBox45.Checked Then
                O._CalculoAlterno = "1"
            Else
                O._CalculoAlterno = "0"
            End If
            If CheckBox46.Checked Then
                O.MostrarPredial = 1
            Else
                O.MostrarPredial = 0
            End If
            If CheckBox47.Checked Then
                O.ClientesMayusculas = 1
            Else
                O.ClientesMayusculas = 0
            End If
            If CheckBox48.Checked Then
                O.ClienteBloquearCodigo = 1
            Else
                O.ClienteBloquearCodigo = 0
            End If
            If CheckBox49.Checked Then
                O.NoPermitirFacturasdeCredito = 1
            Else
                O.NoPermitirFacturasdeCredito = 0
            End If
            If CheckBox50.Checked Then
                O.NoPermitirRemisionesdeCredito = 1
            Else
                O.NoPermitirRemisionesdeCredito = 0
            End If
            If CheckBox51.Checked Then
                O.BusquedaporClases = 1
            Else
                O.BusquedaporClases = 0
            End If
            If CheckBox52.Checked Then
                O.MaximizarVentas = 1
            Else
                O.MaximizarVentas = 0
            End If
            If chkbancos.Checked Then
                O.IntegrarBancosVentasPagos = 1
            Else
                O.IntegrarBancosVentasPagos = 0
            End If
            If chkbancosvc.Checked Then
                O.IntegrarBancosVentasContado = 1
            Else
                O.IntegrarBancosVentasContado = 0
            End If
            If chkbancoscc.Checked Then
                O.IntegrarBancosComprasContado = 1
            Else
                O.IntegrarBancosComprasContado = 0
            End If
            If chkbancoscp.Checked Then
                O.IntegrarBancosComprasPagos = 1
            Else
                O.IntegrarBancosComprasPagos = 0
            End If
            If CheckBox56.Checked Then
                O.ClientesSinRepetir = 1
            Else
                O.ClientesSinRepetir = 0
            End If
            If CheckBox57.Checked Then
                O.SobreEscribeImpLocales = 1
            Else
                O.SobreEscribeImpLocales = 0
            End If
            If CheckBox59.Checked Then
                O.BoletasInventario = 1
            Else
                O.BoletasInventario = 0
            End If
            If CheckBox63.Checked Then
                O.Copiaflujoventas = 1
            Else
                O.Copiaflujoventas = 0
            End If
            If CheckBox64.Checked Then
                O.Copiaflujorem = 1
            Else
                O.Copiaflujorem = 0
            End If
            If CheckBox65.Checked Then
                O.IntegrarContabilidad = 1
            Else
                O.IntegrarContabilidad = 0
            End If
            If CheckBox60.Checked Then
                O.BoletasResumida = 1
                GlobalSemillasResumida = 1
            Else
                O.BoletasResumida = 0
                GlobalSemillasResumida = 0
            End If
            If CheckBox66.Checked Then
                O.PVVentabaCompleta = 1
            Else
                O.PVVentabaCompleta = 0
            End If
            If CheckBox67.Checked Then
                O._ModoFoliosB = "1"
            Else
                O._ModoFoliosB = "0"
            End If
            If CheckBox68.Checked Then
                O.ChecaFolioFacturas = 1
            Else
                O.ChecaFolioFacturas = 0
            End If
            If CheckBox69.Checked Then
                O.RecibidoDefault = 1
            Else
                O.RecibidoDefault = 0
            End If
            If CheckBox70.Checked Then
                O.NoBloquearCreardesde = 1
            Else
                O.NoBloquearCreardesde = 0
            End If
            If CheckBox72.Checked Then
                O.PVConfirmarCobrar = 1
            Else
                O.PVConfirmarCobrar = 0
            End If
            If CheckBox73.Checked Then
                O.NoImpSinGuardar = 1
            Else
                O.NoImpSinGuardar = 0
            End If
            If CheckBox74.Checked Then
                O.RemisionesSinDetalleCD = 1
            Else
                O.RemisionesSinDetalleCD = 0
            End If
            If CheckBox76.Checked Then
                O.FacturarSoloaCredito = 1
            Else
                O.FacturarSoloaCredito = 0
            End If
            If CheckBox75.Checked Then
                O.RemisionesSoloaCredito = 1
            Else
                O.RemisionesSoloaCredito = 0
            End If
            If CheckBox77.Checked Then
                O.VendedorUsuario = 1
            Else
                O.VendedorUsuario = 0
            End If
            If CheckBox79.Checked Then
                O.InicioRapido = 1
            Else
                O.InicioRapido = 0
            End If
            If CheckBox11.Checked Then
                O.ActNom12 = 1
            Else
                O.ActNom12 = 0
            End If
            If CheckBox54.Checked Then
                O.FacturarPagosRemisiones = 1
            Else
                O.FacturarPagosRemisiones = 0
            End If
            If CheckBox55.Checked Then
                O.PedirAnticipoRemisiones = 1
            Else
                O.PedirAnticipoRemisiones = 0
            End If
            GlobalTipoFacturacion = O._TipoFacturacion
            O._UsuarioFacCom = TextBox52.Text
            O._passFacCom = TextBox51.Text
            O._PassCredito = ""
            GlobalPacCFDI = O._PacCFDI
            O.FechaAdd = Format(DateTimePicker2.Value, "yyyy/MM/dd")
            O.FechaVen = Format(DateTimePicker1.Value, "yyyy/MM/dd")
            O.Timbres = CInt(TextBox63.Text)
            O.AvisoTimbres = CInt(TextBox64.Text)
            O.AvisoDias = CInt(TextBox65.Text)
            O.TituloFactura = TextBox87.Text
            O.TituloParcialidad = TextBox86.Text
            O.IdClienteDefault = IdCliente
            O.PuertoBascula = ComboBox9.Text
            O.BasculaBaundRate = TextBox91.Text
            O.BasculaDataBits = TextBox92.Text
            O.BasculaHandshake = ComboBox11.SelectedIndex
            O.BasculaParity = ComboBox10.SelectedIndex
            O.BasculaSecuencia = TextBox93.Text
            O.TipoProrrateo = ComboBox18.SelectedIndex
            O.IdInventarioCD = IdInventario
            O.NoProveedorSoriana = CInt(TextBox24.Text)
            O.NoProveedorWalmart = CInt(TextBox25.Text)
            If CheckBox19.Checked Then
                O.CierreConVentana = 1
            Else
                O.CierreConVentana = 0
            End If
            O.TipoRedondeo = ComboBox8.SelectedIndex
            O.DecimalesRedondeo = CInt(ComboBox12.Text)
            If CheckBox23.Checked Then
                O._CodigoPostalLocal = "1"
            Else
                O._CodigoPostalLocal = "0"
            End If
            If CheckBox24.Checked Then
                O._TipoSelAlmacen = "1"
            Else
                O._TipoSelAlmacen = "0"
            End If
            If CheckBox31.Checked Then
                O._AvisoCosto = "1"
            Else
                O._AvisoCosto = "0"
            End If
            If CheckBox58.Checked Then
                O.TipoCostoPrecios = 1
            Else
                O.TipoCostoPrecios = 0
            End If
            If CheckBox61.Checked Then
                O.SiemprePorSurtirVentas = 1
            Else
                O.SiemprePorSurtirVentas = 0
            End If
            If CheckBox62.Checked Then
                O.SiemprePorSurtirRemisiones = 1
            Else
                O.SiemprePorSurtirRemisiones = 0
            End If
            If ComboBox14.SelectedIndex = 0 Then
                O.idSucursalconector = 0
            Else
                O.idSucursalconector = IdsSucursales.Valor(ComboBox14.SelectedIndex)
            End If
            O.FormatoFechaPv = ComboBox15.Text
            O.Serieconector = TextBox97.Text
            O.SerieNCConecto = TextBox98.Text
            O.PVRojo = Panel8.BackColor.R
            O.PVVerde = Panel8.BackColor.G
            O.PVAzul = Panel8.BackColor.B
            If CheckBox29.Checked Then
                O.NParcialidades = 1
            Else
                O.NParcialidades = 0
            End If
            If CheckBox30.Checked Then
                O.BuscaModoB = 1
            Else
                O.BuscaModoB = 0
            End If

            O.TituloOriginalFactura = txtoriginalfactura.Text
            O.TituloOriginalRemision = txtoriginalremision.Text
            O.TituloCopiaFactura = txtcopiafactura.Text
            O.TituloCopiaRemision = txtcopiaremision.Text
            O.TituloCopia2Factura = txtcopia2factura.Text
            O.TituloCopia2Remision = txtcopia2remision.Text
            If chkactivarcopia.Checked Then
                O.ActivarCopiaFactura = 1
            Else
                O.ActivarCopiaFactura = 0
            End If

            If chkactivarcopiar.Checked Then
                O.ActivarCopiaRemision = 1
            Else
                O.ActivarCopiaRemision = 0
            End If

            If chkactivarcopia2.Checked Then
                O.ActivarCopia2Factura = 1
            Else
                O.ActivarCopia2Factura = 0
            End If

            If chkactivarcopia2r.Checked Then
                O.ActivarCopia2Remision = 1
            Else
                O.ActivarCopia2Remision = 0
            End If


            If chkactivacopiacre.Checked Then
                O.ActivarCopiaFacturaCredito = 1
            Else
                O.ActivarCopiaFacturaCredito = 0
            End If

            If chkactivacopiaremisioncre.Checked Then
                O.ActivarCopiaRemisionCredito = 1
            Else
                O.ActivarCopiaRemisionCredito = 0
            End If

            If chkactivacopiacre2.Checked Then
                O.ActivarCopiaFacturaCredito2 = 1
            Else
                O.ActivarCopiaFacturaCredito2 = 0
            End If

            If chkactivacopiaremisioncre2.Checked Then
                O.ActivarCopiaRemisionCredito2 = 1
            Else
                O.ActivarCopiaRemisionCredito2 = 0
            End If

            If CheckBox53.Checked Then
                O.PagosTicket = 1
            Else
                O.PagosTicket = 0
            End If
            If CheckBox4.Checked Then
                O.FacturarSoloExistencia = 1
            Else
                O.FacturarSoloExistencia = 0
            End If
            GlobalModoBusqueda = O.BuscaModoB
            O.GuardaOpciones()
            If CheckBox71.Checked Then
                O.FacturaComoegreso = 1
            Else
                O.FacturaComoegreso = 0
            End If
            O.GuardaOpciones3(IdsSucursales.Valor(ComboBox2.SelectedIndex))
            My.Settings.menusextendidos = CheckBox2.Checked
            My.Settings.impresoraPDF = TextBox42.Text
            GlobalIdSucursalDefault = IdsSucursales.Valor(ComboBox2.SelectedIndex)
            My.Settings.multiplesventanas = CheckBox3.Checked
            If CheckBox4.Checked Then
                GlobalSoloExistencia = True
            Else
                GlobalSoloExistencia = False
            End If
            'My.Settings.rutapfx = TextBox49.Text
            'My.Settings.passwordpfx = TextBox48.Text
            My.Settings.conceptocero = CheckBox8.Checked
            'My.Settings.cantidadcodigo = chkbancos.Checked
            My.Settings.preguntarsalir = CheckBox12.Checked
            If CheckBox25.Checked Then
                My.Settings.abrepunto = "1"
            Else
                My.Settings.abrepunto = "0"
            End If

            My.Settings.Save()
            If ComboBox4.SelectedIndex > 0 Then GuardaRutas(IdsSucursales.Valor(ComboBox4.SelectedIndex))
            If ComboBox5.SelectedIndex > 0 Then GuardaImpresoras(IdsSucursales.Valor(ComboBox5.SelectedIndex))
            Dim sa As New dbSucursalesArchivos
            sa.GuardaOpciones(IdsSucursales.Valor(ComboBox2.SelectedIndex), TextBox49.Text, TextBox48.Text, GlobalIdEmpresa, IdsCajas.Valor(ComboBox6.SelectedIndex), ComboBox7.SelectedIndex)
            sa.CierraDB()

            If CostoAnterior <> ComboBox3.SelectedIndex Then
                If MsgBox("Ha cambiado el tipo de costeo. ¿Desea recalcular los costos?" + vbCrLf + " Esta operación puede durar varios minutos.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim I As New dbInventario(MySqlcon)
                    I.ReCalculaCostos(0, GlobalTipoCosteo)
                    MsgBox("Listo", MsgBoxStyle.OkOnly, GlobalNombreApp)
                End If
            End If

            Me.Close()


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GuardarCambios()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GuardarCambios()
    End Sub

    





    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox39.Text = OpenFileDialog1.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Me.Close()
    End Sub

   

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Me.Close()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox40.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox41.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox42.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Try
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox35.Text = OpenFileDialog1.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox34.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox33.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox43.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox36.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

   

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox44.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox47.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked Then
            CheckBox6.Checked = False
            CheckBox5.Checked = False
            CheckBox78.Checked = False
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked Then
            CheckBox7.Checked = False
            CheckBox5.Checked = False
            CheckBox78.Checked = False
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked Then
            CheckBox6.Checked = False
            CheckBox7.Checked = False
            CheckBox78.Checked = False
        End If
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Try
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox49.Text = OpenFileDialog1.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        If CheckBox9.Checked Then
            CheckBox10.Checked = False
            TextBox52.Enabled = False
            TextBox51.Enabled = False
            TextBox48.Enabled = True
            TextBox49.Enabled = True
            TextBox50.Enabled = True
            Button23.Enabled = True
            CheckBox34.Checked = False
        End If

    End Sub

    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        If CheckBox10.Checked Then
            CheckBox9.Checked = False
            TextBox52.Enabled = True
            TextBox51.Enabled = True
            TextBox48.Enabled = False
            TextBox49.Enabled = False
            TextBox50.Enabled = False
            Button23.Enabled = False
            CheckBox34.Checked = False
        End If

    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox45.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox59.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox60.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox61.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        If ComboBox4.SelectedIndex > 0 Then GuardaRutas(IdsSucursales.Valor(ComboBox4.SelectedIndex))
    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        Me.Close()
    End Sub

    Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
        GuardarCambios()
    End Sub

    Private Sub Label49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox66.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox67.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Label57_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label57.Click

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ConsultaOn Then
            ComboBox4.SelectedIndex = ComboBox2.SelectedIndex
            ComboBox5.SelectedIndex = ComboBox2.SelectedIndex
            ConsultaOn = False
            ComboBox16.SelectedIndex = ComboBox2.SelectedIndex
            ConsultaOn = True
        End If
        If ComboBox2.SelectedIndex > 0 Then
            LlenaCombos("tblcajas", ComboBox6, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(ComboBox2.SelectedIndex).ToString, "Sel. Caja")
            O.DaOpciones3(IdsSucursales.Valor(ComboBox2.SelectedIndex))
            If O.FacturaComoegreso = 1 Then
                CheckBox71.Checked = True
            Else
                CheckBox71.Checked = False
            End If
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.SelectedIndex > 0 Then DaRutas(IdsSucursales.Valor(ComboBox4.SelectedIndex))
    End Sub

    Private Sub Button46_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button46.Click
        Me.Close()
    End Sub

    Private Sub Button47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button47.Click
        GuardarCambios()
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        If ComboBox5.SelectedIndex > 0 Then
            Dim ConsultaTemp As Boolean = ConsultaOn
            ConsultaOn = False
            ComboBox16.SelectedIndex = ComboBox5.SelectedIndex
            ComboBox17.SelectedIndex = ComboBox5.SelectedIndex
            ConsultaOn = ConsultaTemp
            DaImpresoras(IdsSucursales.Valor(ComboBox5.SelectedIndex))
        End If

    End Sub

    Private Sub Button48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button48.Click
        If ComboBox5.SelectedIndex > 0 Then GuardaImpresoras(IdsSucursales.Valor(ComboBox5.SelectedIndex))
    End Sub


    Private Sub Button33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button33.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox69.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox68.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button35.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox71.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button34.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox70.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button37.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox73.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button36.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox72.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button39.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox75.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button38.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox74.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button41.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox77.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button40.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox76.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage3.Click

    End Sub

    Private Sub Button44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button44.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            TextBox88.Text = B.Cliente.Nombre
            IdCliente = B.Cliente.ID
        End If
    End Sub

    Private Sub Button45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button45.Click
        Try
            OpenFileDialog1.Filter = "Archivos de imagen(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'Dim Imagen As Bitmap
                'Fondo = Image.FromFile(OpenFileDialog1.FileName)
                TextBox89.Text = OpenFileDialog1.FileName
                PictureBox2.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)
                'Dim SA As New dbSucursalesArchivos
                'If RadioButton1.Checked Then
                'SA.GuardaRuta(ComboBox3.SelectedIndex, IdsSucursales.Valor(ComboBox2.SelectedIndex), OpenFileDialog1.FileName, GlobalIdEmpresa)
                'Else
                '   SA.GuardaRuta(ComboBox3.SelectedIndex + 16, IdsSucursales.Valor(ComboBox2.SelectedIndex), OpenFileDialog1.FileName, GlobalIdEmpresa)
                'End If

            End If
        Catch ex As Exception
            MsgBox("No se pudo cargar la imagen", MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button51.Click
        GuardarCambios()
    End Sub

    Private Sub Button50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button50.Click
        Me.Close()
    End Sub

    Private Sub Button49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button49.Click
        Try
            TextBox90.Text = ""
            'TextBox95.Text = ""
            If S.IsOpen Then S.Close()
            S.PortName = ComboBox9.Text
            S.BaudRate = CInt(TextBox91.Text)
            S.Parity = ComboBox10.SelectedIndex
            S.DataBits = CInt(TextBox92.Text)
            S.Handshake = ComboBox11.SelectedIndex
            S.WriteTimeout = 1000
            S.ReadTimeout = 1000
            'S.Encoding = System.Text.Encoding.Default
            S.Open()
            'If TextBox93.Text <> "" Then
            S.Write(Chr(CInt(TextBox93.Text)))
            'Else
            '    S.Write(TextBox94.Text)
            'End If
            Veces = 0
            Timer1.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
            If S.IsOpen Then S.Close()
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        TextBox90.Text = S.ReadExisting 'System.Text.RegularExpressions.Regex.Replace(S.ReadExisting, "[^\d]", "")
        If TextBox90.Text <> "" And Veces <= 120 Then
            'Label2.Text = Lectura
            Veces = 0
            Timer1.Enabled = False
        Else
            If Veces > 120 Then
                Timer1.Enabled = False
                TextBox90.Text = "Sin lectura"
                Veces = 0
            End If
        End If
        Veces += 1
    End Sub

    Private Sub ConsultaOC()
        If ConsultaOn Then
            Dim OOC As New dbOpcionesOc(MySqlcon)
            OOC.LlenaDatos(ComboBox13.SelectedIndex, IdsSucursales2.Valor(ComboBox19.SelectedIndex))
            DateTimePicker3.Value = "2000/01/01 " + OOC.HoraInicioOc
            DateTimePicker4.Value = "2000/01/01 " + OOC.HoraFinOc
            DateTimePicker5.Value = "2000/01/01 " + OOC.HoraInicioOc2
            DateTimePicker6.Value = "2000/01/01 " + OOC.HoraFinOc2
            TextBox94.Text = OOC.SerieOc
            TextBox95.Text = OOC.FolioOc.ToString
            TextBox21.Text = OOC.SeriesAnt
            If OOC.ActivarOc = 1 Then
                CheckBox26.Checked = True
            Else
                CheckBox26.Checked = False
            End If
            If OOC.OcultarOc = 1 Then
                CheckBox27.Checked = True
            Else
                CheckBox27.Checked = False
            End If
        End If
    End Sub
    Private Sub ComboBox13_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox13.SelectedIndexChanged
        'ConsultaOn = False
        ConsultaOC()
        'ConsultaOn = True
    End Sub

    Private Sub Button54_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button54.Click
        Try
            Dim OOc As New dbOpcionesOc(MySqlcon)
            OOc.Documento = ComboBox13.SelectedIndex
            OOc.HoraInicioOc = Format(DateTimePicker3.Value, "HH:mm")
            OOc.HoraFinOc = Format(DateTimePicker4.Value, "HH:mm")
            OOc.HoraInicioOc2 = Format(DateTimePicker5.Value, "HH:mm")
            OOc.HoraFinOc2 = Format(DateTimePicker6.Value, "HH:mm")
            If CheckBox26.Checked Then
                OOc.ActivarOc = 1
            End If
            If CheckBox27.Checked Then
                OOc.OcultarOc = 1
            End If
            OOc.SerieOc = TextBox94.Text
            OOc.FolioOc = CInt(TextBox95.Text)
            OOc.SeriesAnt = TextBox21.Text
            OOc.IdSucursal = IdsSucursales2.Valor(ComboBox19.SelectedIndex)
            OOc.GuardaDatos()
            PopUp("Opciones Extra Guardadas", 90)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

    

    Private Sub Button56_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button56.Click
        ColorDialog1.Color = Panel8.BackColor
        If ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Panel8.BackColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub Button61_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button61.Click
        Me.Close()
    End Sub

    Private Sub Button62_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button62.Click
        GuardarCambios()
    End Sub

    Private Sub Button63_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button63.Click
        If ComboBox16.SelectedIndex > 0 Then GuardaImpresoras(IdsSucursales.Valor(ComboBox16.SelectedIndex))
    End Sub

    Private Sub ComboBox16_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox16.SelectedIndexChanged
        If ComboBox16.SelectedIndex > 0 Then
            Dim ConsultaTemp As Boolean = ConsultaOn
            ConsultaOn = False
            ComboBox5.SelectedIndex = ComboBox16.SelectedIndex
            ComboBox17.SelectedIndex = ComboBox16.SelectedIndex
            ConsultaOn = ConsultaTemp
            DaImpresoras(IdsSucursales.Valor(ComboBox16.SelectedIndex))
        End If
    End Sub

    Private Sub Button60_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button60.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox102.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button59_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button59.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox101.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button58_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button58.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox100.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button57_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button57.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox99.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button65_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button65.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox104.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button64_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button64.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox103.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button67_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button67.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox106.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button66_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button66.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox105.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub CheckBox34_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox34.CheckedChanged
        If CheckBox34.Checked Then
            CheckBox9.Checked = False
            TextBox52.Enabled = True
            TextBox51.Enabled = True
            TextBox48.Enabled = False
            TextBox49.Enabled = False
            TextBox50.Enabled = False
            CheckBox10.Checked = False
            Button23.Enabled = False
        End If
    End Sub

    Private Sub General_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles General.Click

    End Sub

    Private Sub TabPage6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage6.Click

    End Sub

    Private Sub Button81_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button81.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPreordenEstatico.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button80_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button80.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPreOrdenFlujo.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button79_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button79.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtOrdenCompraE.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button78_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button78.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtOrdenCompraF.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button77_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button77.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtRemisionE.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button76_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button76.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtRemisionF.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button75_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button75.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtDevolucionE.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button74_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button74.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtDevolucionF.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button73_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button73.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtNotasCreditoE.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button72_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button72.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtNotasCreditoF.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button71_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button71.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtNotasCargoE.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button70_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button70.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtNotasCargoF.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button82_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button82.Click
        If ComboBox17.SelectedIndex > 0 Then GuardaImpresoras(IdsSucursales.Valor(ComboBox17.SelectedIndex))
    End Sub

    Private Sub Button69_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button69.Click
        GuardarCambios()
    End Sub

    Private Sub Button68_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button68.Click
        Me.Close()
    End Sub

    
    Private Sub Button84_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button84.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox108.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button83_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button83.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox107.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button43.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox79.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button42.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox78.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub Button86_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button86.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox110.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button85_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button85.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox109.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button87_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button87.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox111.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub ComboBox17_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox17.SelectedIndexChanged
        If ComboBox17.SelectedIndex > 0 Then
            Dim ConsultaTemp As Boolean = ConsultaOn
            ConsultaOn = False
            ComboBox5.SelectedIndex = ComboBox17.SelectedIndex
            ComboBox16.SelectedIndex = ComboBox17.SelectedIndex
            ConsultaOn = ConsultaTemp
            DaImpresoras(IdsSucursales.Valor(ComboBox16.SelectedIndex))
        End If
    End Sub

    Private Sub Button9_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox3.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub TabPage5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage5.Click

    End Sub

    Private Sub Button88_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button88.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox5.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button89_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button89.Click
        Me.Close()
    End Sub

    Private Sub Button90_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button90.Click
        GuardarCambios()
    End Sub

    Private Sub ComboBox19_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox19.SelectedIndexChanged
        ConsultaOC()
    End Sub

    

    
    Private Sub Button98_Click(sender As Object, e As EventArgs) Handles Button98.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox11.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button97_Click(sender As Object, e As EventArgs) Handles Button97.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox10.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button96_Click(sender As Object, e As EventArgs) Handles Button96.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox9.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button95_Click(sender As Object, e As EventArgs) Handles Button95.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox8.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button99_Click(sender As Object, e As EventArgs) Handles Button99.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox12.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button100_Click(sender As Object, e As EventArgs) Handles Button100.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox13.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button101_Click(sender As Object, e As EventArgs) Handles Button101.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button102_Click(sender As Object, e As EventArgs) Handles Button102.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox15.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button103_Click(sender As Object, e As EventArgs) Handles Button103.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox17.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button104_Click(sender As Object, e As EventArgs) Handles Button104.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox16.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button106_Click(sender As Object, e As EventArgs) Handles Button106.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox19.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button105_Click(sender As Object, e As EventArgs) Handles Button105.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox18.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub
    Private Sub Button107_Click(sender As Object, e As EventArgs) Handles Button107.Click
        Me.Close()
    End Sub
    Private Sub Button108_Click(sender As Object, e As EventArgs) Handles Button108.Click
        GuardarCambios()
    End Sub
    Private Sub Button94_Click(sender As Object, e As EventArgs) Handles Button94.Click
        If CheckBox51.Checked = False Then
            Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.ArticuloNoInv, 0, True, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                TextBox6.Text = B.Inventario.Nombre
                IdInventario = B.Inventario.ID
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(frmBuscadorClases.TipoDeBusqueda.ArticuloNoInv, 0, True, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                TextBox6.Text = B.Inventario.Nombre
                IdInventario = B.Inventario.ID
            End If
            B.Dispose()
        End If
    End Sub

    Private Sub CheckBox35_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox35.CheckedChanged
        If ConsultaOn Then
            If MsgBox("Cambiar esta opción cambia el comportamiento del cálculo de precios. ¿Está seguro que desea cambiar esta opción?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                ConsultaOn = False
                If CheckBox35.Checked Then
                    CheckBox35.Checked = False
                Else
                    CheckBox35.Checked = True
                End If
                ConsultaOn = True
            End If
        End If
    End Sub

    Private Sub CheckBox58_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox58.CheckedChanged
        If ConsultaOn Then
            If MsgBox("Cambiar esta opción cambia el comportamiento del cálculo de precios. ¿Está seguro que desea cambiar esta opción?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                ConsultaOn = False
                If CheckBox58.Checked Then
                    CheckBox58.Checked = False
                Else
                    CheckBox58.Checked = True
                End If
                ConsultaOn = True
            End If
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ConsultaOn Then
            If MsgBox("Cambiar esta opción cambia la manera en que se calcula el costo. ¿Está seguro que desea cambiar esta opción?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                ConsultaOn = False
                If ComboBox3.SelectedIndex = 0 Then
                    ComboBox3.SelectedIndex = 1
                Else
                    ComboBox3.SelectedIndex = 0
                End If
                ConsultaOn = True
            End If
        End If
    End Sub

    Private Sub ComboBox18_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox18.SelectedIndexChanged
        If ConsultaOn Then
            If MsgBox("Cambiar esta opción cambia la manera en que se calcula el costo. ¿Está seguro que desea cambiar esta opción?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                ConsultaOn = False
                If ComboBox18.SelectedIndex = 0 Then
                    ComboBox18.SelectedIndex = 1
                Else
                    ComboBox18.SelectedIndex = 0
                End If
                ConsultaOn = True
            End If
        End If
    End Sub

    Private Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox20.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox7.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub CheckBox49_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox49.CheckedChanged
        If CheckBox49.Checked Then
            CheckBox76.Checked = False
        End If
    End Sub

    Private Sub CheckBox50_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox50.CheckedChanged
        If CheckBox50.Checked Then
            CheckBox75.Checked = False
        End If
    End Sub

    Private Sub CheckBox76_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox76.CheckedChanged
        If CheckBox76.Checked Then
            CheckBox49.Checked = False
        End If
    End Sub

    Private Sub CheckBox75_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox75.CheckedChanged
        If CheckBox75.Checked Then
            CheckBox50.Checked = False
        End If
    End Sub

    Private Sub Button53_Click(sender As Object, e As EventArgs) Handles Button53.Click
        Try
            Dim Btp As New banxico.DgieWS
            'Dim X As String = "SF43718"
            Dim doc As New System.Xml.XmlDocument()
            'X = Btp.tiposDeCambioBanxico
            doc.LoadXml(Btp.tiposDeCambioBanxico)
            TextBox23.Text = ""
            For Each n As System.Xml.XmlNode In doc.GetElementsByTagName("bm:Series")
                If n.Attributes("IDSERIE").Value = "SF43718" Then
                    GlobaltpBanxico = n.FirstChild.Attributes("OBS_VALUE").Value
                    TextBox23.Text = GlobaltpBanxico
                End If
            Next
            If TextBox23.Text = "" Then
                For Each n As System.Xml.XmlNode In doc.GetElementsByTagName("bm:Series")
                    If n.Attributes("IDSERIE").Value = "SF60653" Then
                        GlobaltpBanxico = n.FirstChild.Attributes("OBS_VALUE").Value
                        TextBox23.Text = GlobaltpBanxico
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
            GlobaltpBanxico = "Error"
            TextBox23.Text = "Error"
        End Try
    End Sub

    Private Sub CheckBox78_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox78.CheckedChanged
        If CheckBox78.Checked Then
            CheckBox6.Checked = False
            CheckBox7.Checked = False
            CheckBox5.Checked = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If IsNumeric(TextBox2.Text) And IsNumeric(TextBox63.Text) Then
            TextBox63.Text = CInt(TextBox63.Text) + CInt(TextBox2.Text)
            TextBox2.Text = "0"
        End If
    End Sub

    Private Sub Button91_Click(sender As Object, e As EventArgs) Handles Button91.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox27.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button55_Click(sender As Object, e As EventArgs) Handles Button55.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox26.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub
End Class