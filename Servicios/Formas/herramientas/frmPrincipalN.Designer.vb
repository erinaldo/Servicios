<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrincipalN
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrincipalN))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.Ribbon1 = New System.Windows.Forms.Ribbon()
        Me.mnuArcGeneral = New System.Windows.Forms.RibbonOrbMenuItem()
        Me.btnArcGenUsuarios = New System.Windows.Forms.RibbonButton()
        Me.btnArcGenEmpresas = New System.Windows.Forms.RibbonButton()
        Me.btnArcGenTiposSucursales = New System.Windows.Forms.RibbonButton()
        Me.btnArcGenSucursales = New System.Windows.Forms.RibbonButton()
        Me.RibbonSeparator = New System.Windows.Forms.RibbonSeparator()
        Me.btnArcGenZonas = New System.Windows.Forms.RibbonButton()
        Me.btnArcGenMetodoPagoFactura = New System.Windows.Forms.RibbonButton()
        Me.btnArcGenMetodoPagoRemision = New System.Windows.Forms.RibbonButton()
        Me.btnArcGenMedidas = New System.Windows.Forms.RibbonButton()
        Me.btnArcGenMonedas = New System.Windows.Forms.RibbonButton()
        Me.RibbonSeparator2 = New System.Windows.Forms.RibbonSeparator()
        Me.btnArcGenReportes = New System.Windows.Forms.RibbonButton()
        Me.mnuArcVentas = New System.Windows.Forms.RibbonOrbMenuItem()
        Me.btnArcVenTiposClientes = New System.Windows.Forms.RibbonButton()
        Me.btnArcVenClientes = New System.Windows.Forms.RibbonButton()
        Me.btnArcVenVendedores = New System.Windows.Forms.RibbonButton()
        Me.btnArcVenConceptosNotas = New System.Windows.Forms.RibbonButton()
        Me.mnuArcCompras = New System.Windows.Forms.RibbonOrbMenuItem()
        Me.btnArcComTiposProveedores = New System.Windows.Forms.RibbonButton()
        Me.btnArcComProveedores = New System.Windows.Forms.RibbonButton()
        Me.btnArcComConceptosNotas = New System.Windows.Forms.RibbonButton()
        Me.mnuArcInventario = New System.Windows.Forms.RibbonOrbMenuItem()
        Me.btnArcInvArticulos = New System.Windows.Forms.RibbonButton()
        Me.btnArcInvClasificaciones = New System.Windows.Forms.RibbonButton()
        Me.btnArcInvConceptos = New System.Windows.Forms.RibbonButton()
        Me.btnArcInvModelos = New System.Windows.Forms.RibbonButton()
        Me.btnArcInvTallas = New System.Windows.Forms.RibbonButton()
        Me.btnArcInvColores = New System.Windows.Forms.RibbonButton()
        Me.btnArcInvRelaciones = New System.Windows.Forms.RibbonButton()
        Me.btnArcInvAlmacenes = New System.Windows.Forms.RibbonButton()
        Me.mnuArcServicios = New System.Windows.Forms.RibbonOrbMenuItem()
        Me.btnArcSerCajas = New System.Windows.Forms.RibbonButton()
        Me.btnArcSerTecnicos = New System.Windows.Forms.RibbonButton()
        Me.btnArcSerClasificaciones = New System.Windows.Forms.RibbonButton()
        Me.btnArcSerOfertas = New System.Windows.Forms.RibbonButton()
        Me.btnSalir = New System.Windows.Forms.RibbonOrbOptionButton()
        Me.btnCambiarEmpresa = New System.Windows.Forms.RibbonOrbOptionButton()
        Me.btnCambiarUsuario = New System.Windows.Forms.RibbonOrbOptionButton()
        Me.btnAcerca = New System.Windows.Forms.RibbonOrbOptionButton()
        Me.tabVentas = New System.Windows.Forms.RibbonTab()
        Me.pnlVentasExpedir = New System.Windows.Forms.RibbonPanel()
        Me.btnVentasFac = New System.Windows.Forms.RibbonButton()
        Me.btnVenCapRemisiones = New System.Windows.Forms.RibbonButton()
        Me.btnVenCapNotasCredito = New System.Windows.Forms.RibbonButton()
        Me.btnVenCapCotizaciones = New System.Windows.Forms.RibbonButton()
        Me.btnVenCapDevoluciones = New System.Windows.Forms.RibbonButton()
        Me.mnuVentasApartados = New System.Windows.Forms.RibbonButton()
        Me.mnuVentasNotasCargo = New System.Windows.Forms.RibbonButton()
        Me.mnuPedidos = New System.Windows.Forms.RibbonButton()
        Me.pnlVentasPagos = New System.Windows.Forms.RibbonPanel()
        Me.btnVenPagFacturas = New System.Windows.Forms.RibbonButton()
        Me.btnVenPagRemisiones = New System.Windows.Forms.RibbonButton()
        Me.RibbonButton2 = New System.Windows.Forms.RibbonButton()
        Me.pnlVentasConsultas = New System.Windows.Forms.RibbonPanel()
        Me.btnVenConReportes = New System.Windows.Forms.RibbonButton()
        Me.btnVenConMovimientos = New System.Windows.Forms.RibbonButton()
        Me.btnVenConHistorial = New System.Windows.Forms.RibbonButton()
        Me.btnVenConCorte = New System.Windows.Forms.RibbonButton()
        Me.tabCompras = New System.Windows.Forms.RibbonTab()
        Me.pnlComCapturar = New System.Windows.Forms.RibbonPanel()
        Me.btnComCapFacturas = New System.Windows.Forms.RibbonButton()
        Me.btnComCapRemisiones = New System.Windows.Forms.RibbonButton()
        Me.btnComCapOrdenes = New System.Windows.Forms.RibbonButton()
        Me.btnComCapPreordenes = New System.Windows.Forms.RibbonButton()
        Me.btnComCapDevoluciones = New System.Windows.Forms.RibbonButton()
        Me.btnComCapNotasCargo = New System.Windows.Forms.RibbonButton()
        Me.btnComCapNotasCredito = New System.Windows.Forms.RibbonButton()
        Me.btnComCapPagos = New System.Windows.Forms.RibbonButton()
        Me.btnComCapDocumentos = New System.Windows.Forms.RibbonButton()
        Me.pnlComGranos = New System.Windows.Forms.RibbonPanel()
        Me.btnComGraBoletas = New System.Windows.Forms.RibbonButton()
        Me.btnComGraLiquidaciones = New System.Windows.Forms.RibbonButton()
        Me.btnComGraComprobantes = New System.Windows.Forms.RibbonButton()
        Me.btnComGraReportes = New System.Windows.Forms.RibbonButton()
        Me.pnlComConsultas = New System.Windows.Forms.RibbonPanel()
        Me.btnComConReportes = New System.Windows.Forms.RibbonButton()
        Me.btnComConMovimientos = New System.Windows.Forms.RibbonButton()
        Me.btnComConDevoluciones = New System.Windows.Forms.RibbonButton()
        Me.btnComConValidador = New System.Windows.Forms.RibbonButton()
        Me.tabInventario = New System.Windows.Forms.RibbonTab()
        Me.pnlInvOperacion = New System.Windows.Forms.RibbonPanel()
        Me.btnInvOpeMovimientos = New System.Windows.Forms.RibbonButton()
        Me.btnInvOpePedidos = New System.Windows.Forms.RibbonButton()
        Me.btnInvOpeBoletas = New System.Windows.Forms.RibbonButton()
        Me.pnlInvConsultas = New System.Windows.Forms.RibbonPanel()
        Me.btnInvConReportes = New System.Windows.Forms.RibbonButton()
        Me.btnInvConInventario = New System.Windows.Forms.RibbonButton()
        Me.btnInvConMonitor = New System.Windows.Forms.RibbonButton()
        Me.btnInvConCardex = New System.Windows.Forms.RibbonButton()
        Me.btnInvConRevision = New System.Windows.Forms.RibbonButton()
        Me.btnInvConReportesBoletas = New System.Windows.Forms.RibbonButton()
        Me.pnlInvHerramientas = New System.Windows.Forms.RibbonPanel()
        Me.btnInvHerRecCostos = New System.Windows.Forms.RibbonButton()
        Me.btnInvHerBuscarNeg = New System.Windows.Forms.RibbonButton()
        Me.btnInvHerRecInventario = New System.Windows.Forms.RibbonButton()
        Me.btnInvHerAjustarCero = New System.Windows.Forms.RibbonButton()
        Me.btnInvHerConfigConceptos = New System.Windows.Forms.RibbonButton()
        Me.tabServicios = New System.Windows.Forms.RibbonTab()
        Me.pnlSerComercial = New System.Windows.Forms.RibbonPanel()
        Me.btnSerComNuevo = New System.Windows.Forms.RibbonButton()
        Me.btnSerComBuscar = New System.Windows.Forms.RibbonButton()
        Me.btnSerComReportes = New System.Windows.Forms.RibbonButton()
        Me.btnSerComEstados = New System.Windows.Forms.RibbonButton()
        Me.pnlSerInterno = New System.Windows.Forms.RibbonPanel()
        Me.btnSerIntNuevo = New System.Windows.Forms.RibbonButton()
        Me.btnSerIntBuscar = New System.Windows.Forms.RibbonButton()
        Me.btnSerIntReporte = New System.Windows.Forms.RibbonButton()
        Me.btnSerIntEstados = New System.Windows.Forms.RibbonButton()
        Me.tabPuntoVenta = New System.Windows.Forms.RibbonTab()
        Me.pnlPunGeneral = New System.Windows.Forms.RibbonPanel()
        Me.btnPunGenVentas = New System.Windows.Forms.RibbonButton()
        Me.btnPunGenMovimientos = New System.Windows.Forms.RibbonButton()
        Me.btnPunGenHerramientas = New System.Windows.Forms.RibbonButton()
        Me.btnPunGenReportes = New System.Windows.Forms.RibbonButton()
        Me.pnlPunRestaurante = New System.Windows.Forms.RibbonPanel()
        Me.btnPunResVentas = New System.Windows.Forms.RibbonButton()
        Me.btnPunResConfiguracion = New System.Windows.Forms.RibbonButton()
        Me.tabBancos = New System.Windows.Forms.RibbonTab()
        Me.pnlBanCatalogos = New System.Windows.Forms.RibbonPanel()
        Me.btnBanCatCuentas = New System.Windows.Forms.RibbonButton()
        Me.pnlBanOperacion = New System.Windows.Forms.RibbonPanel()
        Me.btnBanOpeDepositos = New System.Windows.Forms.RibbonButton()
        Me.btnBanOpePagos = New System.Windows.Forms.RibbonButton()
        Me.btnBanOpeConciliacion = New System.Windows.Forms.RibbonButton()
        Me.pnlBanConsultas = New System.Windows.Forms.RibbonPanel()
        Me.btnBanConReportes = New System.Windows.Forms.RibbonButton()
        Me.tabNomina = New System.Windows.Forms.RibbonTab()
        Me.pnlNomCatalogos = New System.Windows.Forms.RibbonPanel()
        Me.btnNomCatTrabajadores = New System.Windows.Forms.RibbonButton()
        Me.pnlNomOpercion = New System.Windows.Forms.RibbonPanel()
        Me.btnNomOpeNomina = New System.Windows.Forms.RibbonButton()
        Me.pnlNomConsultas = New System.Windows.Forms.RibbonPanel()
        Me.btnNomConReportes = New System.Windows.Forms.RibbonButton()
        Me.tabGastos = New System.Windows.Forms.RibbonTab()
        Me.pnlGasCatalogos = New System.Windows.Forms.RibbonPanel()
        Me.btnGasCatClasificaciones = New System.Windows.Forms.RibbonButton()
        Me.btnGasCatEmpleados = New System.Windows.Forms.RibbonButton()
        Me.pnlGasOperacion = New System.Windows.Forms.RibbonPanel()
        Me.btnGasOpeGastos = New System.Windows.Forms.RibbonButton()
        Me.btnGasOpeMovimientos = New System.Windows.Forms.RibbonButton()
        Me.btnGasOpeProgramar = New System.Windows.Forms.RibbonButton()
        Me.btnGasOpeAlertas = New System.Windows.Forms.RibbonButton()
        Me.pnlGasConsultas = New System.Windows.Forms.RibbonPanel()
        Me.btnGasConReportes = New System.Windows.Forms.RibbonButton()
        Me.tabEmpenos = New System.Windows.Forms.RibbonTab()
        Me.pnlEmpCatalogos = New System.Windows.Forms.RibbonPanel()
        Me.btnEmpCatIdentificaciones = New System.Windows.Forms.RibbonButton()
        Me.btnEmpCatClasificaciones = New System.Windows.Forms.RibbonButton()
        Me.pnlEmpOperacion = New System.Windows.Forms.RibbonPanel()
        Me.btnEmpOpeEmpenos = New System.Windows.Forms.RibbonButton()
        Me.btnEmpOpePagos = New System.Windows.Forms.RibbonButton()
        Me.btnEmpOpeCompras = New System.Windows.Forms.RibbonButton()
        Me.btnEmpOpeAdjudicaciones = New System.Windows.Forms.RibbonButton()
        Me.btnEmpOpeCortes = New System.Windows.Forms.RibbonButton()
        Me.pnlEmpConsultas = New System.Windows.Forms.RibbonPanel()
        Me.btnEmpConReportes = New System.Windows.Forms.RibbonButton()
        Me.btnEmpConConsultas = New System.Windows.Forms.RibbonButton()
        Me.pnlEmpHerramientas = New System.Windows.Forms.RibbonPanel()
        Me.btnEmpherConfiguracion = New System.Windows.Forms.RibbonButton()
        Me.tabContabilidad = New System.Windows.Forms.RibbonTab()
        Me.pnlConCatalogos = New System.Windows.Forms.RibbonPanel()
        Me.btnConCatClasifPolizas = New System.Windows.Forms.RibbonButton()
        Me.btnConCatCuentas = New System.Windows.Forms.RibbonButton()
        Me.btnConCatConceptos = New System.Windows.Forms.RibbonButton()
        Me.btnConCatMascaras = New System.Windows.Forms.RibbonButton()
        Me.pnlConOperación = New System.Windows.Forms.RibbonPanel()
        Me.btnConOpePolizas = New System.Windows.Forms.RibbonButton()
        Me.btnConOpeSaldos = New System.Windows.Forms.RibbonButton()
        Me.btnConOpeConciliarDiot = New System.Windows.Forms.RibbonButton()
        Me.btnConOpeGenerarPolizas = New System.Windows.Forms.RibbonButton()
        Me.btnConOpeConfiguracion = New System.Windows.Forms.RibbonButton()
        Me.pnlConConsultas = New System.Windows.Forms.RibbonPanel()
        Me.btnConConReportes = New System.Windows.Forms.RibbonButton()
        Me.tabHerramientas = New System.Windows.Forms.RibbonTab()
        Me.pnlHerConfiguracion = New System.Windows.Forms.RibbonPanel()
        Me.btnHerConOpciones = New System.Windows.Forms.RibbonButton()
        Me.btnHerConConfigCorreo = New System.Windows.Forms.RibbonButton()
        Me.btnHerConLicencias = New System.Windows.Forms.RibbonButton()
        Me.btnHerConDistribuidores = New System.Windows.Forms.RibbonButton()
        Me.btnHerConImportar = New System.Windows.Forms.RibbonButton()
        Me.pnlHerHerramientas = New System.Windows.Forms.RibbonPanel()
        Me.btnHerHerContadorTimbres = New System.Windows.Forms.RibbonButton()
        Me.btnHerHerActivarconector = New System.Windows.Forms.RibbonButton()
        Me.btnHerHerMovimientosUsuario = New System.Windows.Forms.RibbonButton()
        Me.btnHerHerDisenoDocumentos = New System.Windows.Forms.RibbonButton()
        Me.btnHerHerCambioPrecios = New System.Windows.Forms.RibbonButton()
        Me.btnHerHerModifiarInv = New System.Windows.Forms.RibbonButton()
        Me.pnHerBaseDatos = New System.Windows.Forms.RibbonPanel()
        Me.btnHerBasRespaldar = New System.Windows.Forms.RibbonButton()
        Me.btnHerBasRestaurar = New System.Windows.Forms.RibbonButton()
        Me.RibbonButton49 = New System.Windows.Forms.RibbonButton()
        Me.RibbonButton48 = New System.Windows.Forms.RibbonButton()
        Me.RibbonButton47 = New System.Windows.Forms.RibbonButton()
        Me.RibbonButton46 = New System.Windows.Forms.RibbonButton()
        Me.RibbonButton45 = New System.Windows.Forms.RibbonButton()
        Me.RibbonButton44 = New System.Windows.Forms.RibbonButton()
        Me.RibbonButton25 = New System.Windows.Forms.RibbonButton()
        Me.btnInvConBoletas = New System.Windows.Forms.RibbonButton()
        Me.RibbonPanel1 = New System.Windows.Forms.RibbonPanel()
        Me.mnuVentasPagare = New System.Windows.Forms.RibbonButton()
        Me.mnuVentasDocumentos = New System.Windows.Forms.RibbonButton()
        Me.mnuVentasConOfertas = New System.Windows.Forms.RibbonButton()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'Timer2
        '
        '
        'PrintDocument1
        '
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'BackgroundWorker2
        '
        '
        'Ribbon1
        '
        Me.Ribbon1.CaptionBarVisible = False
        Me.Ribbon1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Ribbon1.Location = New System.Drawing.Point(0, 0)
        Me.Ribbon1.Minimized = False
        Me.Ribbon1.Name = "Ribbon1"
        '
        '
        '
        Me.Ribbon1.OrbDropDown.BorderRoundness = 8
        Me.Ribbon1.OrbDropDown.Location = New System.Drawing.Point(0, 0)
        Me.Ribbon1.OrbDropDown.MenuItems.Add(Me.mnuArcGeneral)
        Me.Ribbon1.OrbDropDown.MenuItems.Add(Me.mnuArcVentas)
        Me.Ribbon1.OrbDropDown.MenuItems.Add(Me.mnuArcCompras)
        Me.Ribbon1.OrbDropDown.MenuItems.Add(Me.mnuArcInventario)
        Me.Ribbon1.OrbDropDown.MenuItems.Add(Me.mnuArcServicios)
        Me.Ribbon1.OrbDropDown.Name = ""
        Me.Ribbon1.OrbDropDown.OptionItems.Add(Me.btnSalir)
        Me.Ribbon1.OrbDropDown.OptionItems.Add(Me.btnCambiarEmpresa)
        Me.Ribbon1.OrbDropDown.OptionItems.Add(Me.btnCambiarUsuario)
        Me.Ribbon1.OrbDropDown.OptionItems.Add(Me.btnAcerca)
        Me.Ribbon1.OrbDropDown.Size = New System.Drawing.Size(527, 292)
        Me.Ribbon1.OrbDropDown.TabIndex = 0
        Me.Ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013
        Me.Ribbon1.OrbText = "Archivo"
        Me.Ribbon1.RibbonTabFont = New System.Drawing.Font("Trebuchet MS", 9.0!)
        Me.Ribbon1.Size = New System.Drawing.Size(1030, 78)
        Me.Ribbon1.TabIndex = 0
        Me.Ribbon1.Tabs.Add(Me.tabVentas)
        Me.Ribbon1.Tabs.Add(Me.tabCompras)
        Me.Ribbon1.Tabs.Add(Me.tabInventario)
        Me.Ribbon1.Tabs.Add(Me.tabServicios)
        Me.Ribbon1.Tabs.Add(Me.tabPuntoVenta)
        Me.Ribbon1.Tabs.Add(Me.tabBancos)
        Me.Ribbon1.Tabs.Add(Me.tabNomina)
        Me.Ribbon1.Tabs.Add(Me.tabGastos)
        Me.Ribbon1.Tabs.Add(Me.tabEmpenos)
        Me.Ribbon1.Tabs.Add(Me.tabContabilidad)
        Me.Ribbon1.Tabs.Add(Me.tabHerramientas)
        Me.Ribbon1.TabsMargin = New System.Windows.Forms.Padding(5, 2, 20, 0)
        Me.Ribbon1.TabSpacing = 4
        Me.Ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Green
        '
        'mnuArcGeneral
        '
        Me.mnuArcGeneral.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.mnuArcGeneral.DropDownItems.Add(Me.btnArcGenUsuarios)
        Me.mnuArcGeneral.DropDownItems.Add(Me.btnArcGenEmpresas)
        Me.mnuArcGeneral.DropDownItems.Add(Me.btnArcGenTiposSucursales)
        Me.mnuArcGeneral.DropDownItems.Add(Me.btnArcGenSucursales)
        Me.mnuArcGeneral.DropDownItems.Add(Me.RibbonSeparator)
        Me.mnuArcGeneral.DropDownItems.Add(Me.btnArcGenZonas)
        Me.mnuArcGeneral.DropDownItems.Add(Me.btnArcGenMetodoPagoFactura)
        Me.mnuArcGeneral.DropDownItems.Add(Me.btnArcGenMetodoPagoRemision)
        Me.mnuArcGeneral.DropDownItems.Add(Me.btnArcGenMedidas)
        Me.mnuArcGeneral.DropDownItems.Add(Me.btnArcGenMonedas)
        Me.mnuArcGeneral.DropDownItems.Add(Me.RibbonSeparator2)
        Me.mnuArcGeneral.DropDownItems.Add(Me.btnArcGenReportes)
        Me.mnuArcGeneral.Image = CType(resources.GetObject("mnuArcGeneral.Image"), System.Drawing.Image)
        Me.mnuArcGeneral.LargeImage = CType(resources.GetObject("mnuArcGeneral.LargeImage"), System.Drawing.Image)
        Me.mnuArcGeneral.Name = "mnuArcGeneral"
        Me.mnuArcGeneral.SmallImage = CType(resources.GetObject("mnuArcGeneral.SmallImage"), System.Drawing.Image)
        Me.mnuArcGeneral.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.mnuArcGeneral.Text = "General"
        '
        'btnArcGenUsuarios
        '
        Me.btnArcGenUsuarios.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcGenUsuarios.Image = CType(resources.GetObject("btnArcGenUsuarios.Image"), System.Drawing.Image)
        Me.btnArcGenUsuarios.LargeImage = CType(resources.GetObject("btnArcGenUsuarios.LargeImage"), System.Drawing.Image)
        Me.btnArcGenUsuarios.Name = "btnArcGenUsuarios"
        Me.btnArcGenUsuarios.SmallImage = CType(resources.GetObject("btnArcGenUsuarios.SmallImage"), System.Drawing.Image)
        Me.btnArcGenUsuarios.Text = "Usuarios"
        '
        'btnArcGenEmpresas
        '
        Me.btnArcGenEmpresas.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcGenEmpresas.Image = CType(resources.GetObject("btnArcGenEmpresas.Image"), System.Drawing.Image)
        Me.btnArcGenEmpresas.LargeImage = CType(resources.GetObject("btnArcGenEmpresas.LargeImage"), System.Drawing.Image)
        Me.btnArcGenEmpresas.Name = "btnArcGenEmpresas"
        Me.btnArcGenEmpresas.SmallImage = CType(resources.GetObject("btnArcGenEmpresas.SmallImage"), System.Drawing.Image)
        Me.btnArcGenEmpresas.Text = "Empresas"
        '
        'btnArcGenTiposSucursales
        '
        Me.btnArcGenTiposSucursales.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcGenTiposSucursales.Image = CType(resources.GetObject("btnArcGenTiposSucursales.Image"), System.Drawing.Image)
        Me.btnArcGenTiposSucursales.LargeImage = CType(resources.GetObject("btnArcGenTiposSucursales.LargeImage"), System.Drawing.Image)
        Me.btnArcGenTiposSucursales.Name = "btnArcGenTiposSucursales"
        Me.btnArcGenTiposSucursales.SmallImage = CType(resources.GetObject("btnArcGenTiposSucursales.SmallImage"), System.Drawing.Image)
        Me.btnArcGenTiposSucursales.Text = "Tipos sucursales"
        '
        'btnArcGenSucursales
        '
        Me.btnArcGenSucursales.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcGenSucursales.Image = CType(resources.GetObject("btnArcGenSucursales.Image"), System.Drawing.Image)
        Me.btnArcGenSucursales.LargeImage = CType(resources.GetObject("btnArcGenSucursales.LargeImage"), System.Drawing.Image)
        Me.btnArcGenSucursales.Name = "btnArcGenSucursales"
        Me.btnArcGenSucursales.SmallImage = CType(resources.GetObject("btnArcGenSucursales.SmallImage"), System.Drawing.Image)
        Me.btnArcGenSucursales.Text = "Sucursales"
        '
        'RibbonSeparator
        '
        Me.RibbonSeparator.Name = "RibbonSeparator"
        '
        'btnArcGenZonas
        '
        Me.btnArcGenZonas.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcGenZonas.Image = CType(resources.GetObject("btnArcGenZonas.Image"), System.Drawing.Image)
        Me.btnArcGenZonas.LargeImage = CType(resources.GetObject("btnArcGenZonas.LargeImage"), System.Drawing.Image)
        Me.btnArcGenZonas.Name = "btnArcGenZonas"
        Me.btnArcGenZonas.SmallImage = CType(resources.GetObject("btnArcGenZonas.SmallImage"), System.Drawing.Image)
        Me.btnArcGenZonas.Text = "Zonas"
        '
        'btnArcGenMetodoPagoFactura
        '
        Me.btnArcGenMetodoPagoFactura.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcGenMetodoPagoFactura.Image = CType(resources.GetObject("btnArcGenMetodoPagoFactura.Image"), System.Drawing.Image)
        Me.btnArcGenMetodoPagoFactura.LargeImage = CType(resources.GetObject("btnArcGenMetodoPagoFactura.LargeImage"), System.Drawing.Image)
        Me.btnArcGenMetodoPagoFactura.Name = "btnArcGenMetodoPagoFactura"
        Me.btnArcGenMetodoPagoFactura.SmallImage = CType(resources.GetObject("btnArcGenMetodoPagoFactura.SmallImage"), System.Drawing.Image)
        Me.btnArcGenMetodoPagoFactura.Text = "Métodos de pago facturas"
        '
        'btnArcGenMetodoPagoRemision
        '
        Me.btnArcGenMetodoPagoRemision.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcGenMetodoPagoRemision.Image = CType(resources.GetObject("btnArcGenMetodoPagoRemision.Image"), System.Drawing.Image)
        Me.btnArcGenMetodoPagoRemision.LargeImage = CType(resources.GetObject("btnArcGenMetodoPagoRemision.LargeImage"), System.Drawing.Image)
        Me.btnArcGenMetodoPagoRemision.Name = "btnArcGenMetodoPagoRemision"
        Me.btnArcGenMetodoPagoRemision.SmallImage = CType(resources.GetObject("btnArcGenMetodoPagoRemision.SmallImage"), System.Drawing.Image)
        Me.btnArcGenMetodoPagoRemision.Text = "Métodos de pago remisiones"
        '
        'btnArcGenMedidas
        '
        Me.btnArcGenMedidas.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcGenMedidas.Image = CType(resources.GetObject("btnArcGenMedidas.Image"), System.Drawing.Image)
        Me.btnArcGenMedidas.LargeImage = CType(resources.GetObject("btnArcGenMedidas.LargeImage"), System.Drawing.Image)
        Me.btnArcGenMedidas.Name = "btnArcGenMedidas"
        Me.btnArcGenMedidas.SmallImage = CType(resources.GetObject("btnArcGenMedidas.SmallImage"), System.Drawing.Image)
        Me.btnArcGenMedidas.Text = "Medidas"
        '
        'btnArcGenMonedas
        '
        Me.btnArcGenMonedas.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcGenMonedas.Image = CType(resources.GetObject("btnArcGenMonedas.Image"), System.Drawing.Image)
        Me.btnArcGenMonedas.LargeImage = CType(resources.GetObject("btnArcGenMonedas.LargeImage"), System.Drawing.Image)
        Me.btnArcGenMonedas.Name = "btnArcGenMonedas"
        Me.btnArcGenMonedas.SmallImage = CType(resources.GetObject("btnArcGenMonedas.SmallImage"), System.Drawing.Image)
        Me.btnArcGenMonedas.Text = "Monedas"
        '
        'RibbonSeparator2
        '
        Me.RibbonSeparator2.Name = "RibbonSeparator2"
        '
        'btnArcGenReportes
        '
        Me.btnArcGenReportes.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcGenReportes.Image = CType(resources.GetObject("btnArcGenReportes.Image"), System.Drawing.Image)
        Me.btnArcGenReportes.LargeImage = CType(resources.GetObject("btnArcGenReportes.LargeImage"), System.Drawing.Image)
        Me.btnArcGenReportes.Name = "btnArcGenReportes"
        Me.btnArcGenReportes.SmallImage = CType(resources.GetObject("btnArcGenReportes.SmallImage"), System.Drawing.Image)
        Me.btnArcGenReportes.Text = "Reportes"
        '
        'mnuArcVentas
        '
        Me.mnuArcVentas.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.mnuArcVentas.DropDownItems.Add(Me.btnArcVenTiposClientes)
        Me.mnuArcVentas.DropDownItems.Add(Me.btnArcVenClientes)
        Me.mnuArcVentas.DropDownItems.Add(Me.btnArcVenVendedores)
        Me.mnuArcVentas.DropDownItems.Add(Me.btnArcVenConceptosNotas)
        Me.mnuArcVentas.Image = CType(resources.GetObject("mnuArcVentas.Image"), System.Drawing.Image)
        Me.mnuArcVentas.LargeImage = CType(resources.GetObject("mnuArcVentas.LargeImage"), System.Drawing.Image)
        Me.mnuArcVentas.Name = "mnuArcVentas"
        Me.mnuArcVentas.SmallImage = CType(resources.GetObject("mnuArcVentas.SmallImage"), System.Drawing.Image)
        Me.mnuArcVentas.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.mnuArcVentas.Text = "Ventas"
        '
        'btnArcVenTiposClientes
        '
        Me.btnArcVenTiposClientes.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcVenTiposClientes.Image = CType(resources.GetObject("btnArcVenTiposClientes.Image"), System.Drawing.Image)
        Me.btnArcVenTiposClientes.LargeImage = CType(resources.GetObject("btnArcVenTiposClientes.LargeImage"), System.Drawing.Image)
        Me.btnArcVenTiposClientes.Name = "btnArcVenTiposClientes"
        Me.btnArcVenTiposClientes.SmallImage = CType(resources.GetObject("btnArcVenTiposClientes.SmallImage"), System.Drawing.Image)
        Me.btnArcVenTiposClientes.Text = "Tipos clientes"
        '
        'btnArcVenClientes
        '
        Me.btnArcVenClientes.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcVenClientes.Image = CType(resources.GetObject("btnArcVenClientes.Image"), System.Drawing.Image)
        Me.btnArcVenClientes.LargeImage = CType(resources.GetObject("btnArcVenClientes.LargeImage"), System.Drawing.Image)
        Me.btnArcVenClientes.Name = "btnArcVenClientes"
        Me.btnArcVenClientes.SmallImage = CType(resources.GetObject("btnArcVenClientes.SmallImage"), System.Drawing.Image)
        Me.btnArcVenClientes.Text = "Clientes"
        '
        'btnArcVenVendedores
        '
        Me.btnArcVenVendedores.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcVenVendedores.Image = CType(resources.GetObject("btnArcVenVendedores.Image"), System.Drawing.Image)
        Me.btnArcVenVendedores.LargeImage = CType(resources.GetObject("btnArcVenVendedores.LargeImage"), System.Drawing.Image)
        Me.btnArcVenVendedores.Name = "btnArcVenVendedores"
        Me.btnArcVenVendedores.SmallImage = CType(resources.GetObject("btnArcVenVendedores.SmallImage"), System.Drawing.Image)
        Me.btnArcVenVendedores.Text = "Vendedores"
        '
        'btnArcVenConceptosNotas
        '
        Me.btnArcVenConceptosNotas.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcVenConceptosNotas.Image = CType(resources.GetObject("btnArcVenConceptosNotas.Image"), System.Drawing.Image)
        Me.btnArcVenConceptosNotas.LargeImage = CType(resources.GetObject("btnArcVenConceptosNotas.LargeImage"), System.Drawing.Image)
        Me.btnArcVenConceptosNotas.Name = "btnArcVenConceptosNotas"
        Me.btnArcVenConceptosNotas.SmallImage = CType(resources.GetObject("btnArcVenConceptosNotas.SmallImage"), System.Drawing.Image)
        Me.btnArcVenConceptosNotas.Text = "Conceptos  notas"
        '
        'mnuArcCompras
        '
        Me.mnuArcCompras.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.mnuArcCompras.DropDownItems.Add(Me.btnArcComTiposProveedores)
        Me.mnuArcCompras.DropDownItems.Add(Me.btnArcComProveedores)
        Me.mnuArcCompras.DropDownItems.Add(Me.btnArcComConceptosNotas)
        Me.mnuArcCompras.Image = CType(resources.GetObject("mnuArcCompras.Image"), System.Drawing.Image)
        Me.mnuArcCompras.LargeImage = CType(resources.GetObject("mnuArcCompras.LargeImage"), System.Drawing.Image)
        Me.mnuArcCompras.Name = "mnuArcCompras"
        Me.mnuArcCompras.SmallImage = CType(resources.GetObject("mnuArcCompras.SmallImage"), System.Drawing.Image)
        Me.mnuArcCompras.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.mnuArcCompras.Text = "Compras"
        '
        'btnArcComTiposProveedores
        '
        Me.btnArcComTiposProveedores.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcComTiposProveedores.Image = CType(resources.GetObject("btnArcComTiposProveedores.Image"), System.Drawing.Image)
        Me.btnArcComTiposProveedores.LargeImage = CType(resources.GetObject("btnArcComTiposProveedores.LargeImage"), System.Drawing.Image)
        Me.btnArcComTiposProveedores.Name = "btnArcComTiposProveedores"
        Me.btnArcComTiposProveedores.SmallImage = CType(resources.GetObject("btnArcComTiposProveedores.SmallImage"), System.Drawing.Image)
        Me.btnArcComTiposProveedores.Text = "Tipos proveedores"
        '
        'btnArcComProveedores
        '
        Me.btnArcComProveedores.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcComProveedores.Image = CType(resources.GetObject("btnArcComProveedores.Image"), System.Drawing.Image)
        Me.btnArcComProveedores.LargeImage = CType(resources.GetObject("btnArcComProveedores.LargeImage"), System.Drawing.Image)
        Me.btnArcComProveedores.Name = "btnArcComProveedores"
        Me.btnArcComProveedores.SmallImage = CType(resources.GetObject("btnArcComProveedores.SmallImage"), System.Drawing.Image)
        Me.btnArcComProveedores.Text = "Proveedores"
        '
        'btnArcComConceptosNotas
        '
        Me.btnArcComConceptosNotas.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcComConceptosNotas.Image = CType(resources.GetObject("btnArcComConceptosNotas.Image"), System.Drawing.Image)
        Me.btnArcComConceptosNotas.LargeImage = CType(resources.GetObject("btnArcComConceptosNotas.LargeImage"), System.Drawing.Image)
        Me.btnArcComConceptosNotas.Name = "btnArcComConceptosNotas"
        Me.btnArcComConceptosNotas.SmallImage = CType(resources.GetObject("btnArcComConceptosNotas.SmallImage"), System.Drawing.Image)
        Me.btnArcComConceptosNotas.Text = "Conceptos notas"
        '
        'mnuArcInventario
        '
        Me.mnuArcInventario.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.mnuArcInventario.DropDownItems.Add(Me.btnArcInvArticulos)
        Me.mnuArcInventario.DropDownItems.Add(Me.btnArcInvClasificaciones)
        Me.mnuArcInventario.DropDownItems.Add(Me.btnArcInvConceptos)
        Me.mnuArcInventario.DropDownItems.Add(Me.btnArcInvModelos)
        Me.mnuArcInventario.DropDownItems.Add(Me.btnArcInvTallas)
        Me.mnuArcInventario.DropDownItems.Add(Me.btnArcInvColores)
        Me.mnuArcInventario.DropDownItems.Add(Me.btnArcInvRelaciones)
        Me.mnuArcInventario.DropDownItems.Add(Me.btnArcInvAlmacenes)
        Me.mnuArcInventario.Image = CType(resources.GetObject("mnuArcInventario.Image"), System.Drawing.Image)
        Me.mnuArcInventario.LargeImage = CType(resources.GetObject("mnuArcInventario.LargeImage"), System.Drawing.Image)
        Me.mnuArcInventario.Name = "mnuArcInventario"
        Me.mnuArcInventario.SmallImage = CType(resources.GetObject("mnuArcInventario.SmallImage"), System.Drawing.Image)
        Me.mnuArcInventario.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.mnuArcInventario.Text = "Inventario"
        '
        'btnArcInvArticulos
        '
        Me.btnArcInvArticulos.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcInvArticulos.Image = CType(resources.GetObject("btnArcInvArticulos.Image"), System.Drawing.Image)
        Me.btnArcInvArticulos.LargeImage = CType(resources.GetObject("btnArcInvArticulos.LargeImage"), System.Drawing.Image)
        Me.btnArcInvArticulos.Name = "btnArcInvArticulos"
        Me.btnArcInvArticulos.SmallImage = CType(resources.GetObject("btnArcInvArticulos.SmallImage"), System.Drawing.Image)
        Me.btnArcInvArticulos.Text = "Articulos"
        '
        'btnArcInvClasificaciones
        '
        Me.btnArcInvClasificaciones.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcInvClasificaciones.Image = CType(resources.GetObject("btnArcInvClasificaciones.Image"), System.Drawing.Image)
        Me.btnArcInvClasificaciones.LargeImage = CType(resources.GetObject("btnArcInvClasificaciones.LargeImage"), System.Drawing.Image)
        Me.btnArcInvClasificaciones.Name = "btnArcInvClasificaciones"
        Me.btnArcInvClasificaciones.SmallImage = CType(resources.GetObject("btnArcInvClasificaciones.SmallImage"), System.Drawing.Image)
        Me.btnArcInvClasificaciones.Text = "Clasificaciones"
        '
        'btnArcInvConceptos
        '
        Me.btnArcInvConceptos.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcInvConceptos.Image = CType(resources.GetObject("btnArcInvConceptos.Image"), System.Drawing.Image)
        Me.btnArcInvConceptos.LargeImage = CType(resources.GetObject("btnArcInvConceptos.LargeImage"), System.Drawing.Image)
        Me.btnArcInvConceptos.Name = "btnArcInvConceptos"
        Me.btnArcInvConceptos.SmallImage = CType(resources.GetObject("btnArcInvConceptos.SmallImage"), System.Drawing.Image)
        Me.btnArcInvConceptos.Text = "Conceptos"
        '
        'btnArcInvModelos
        '
        Me.btnArcInvModelos.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcInvModelos.Image = CType(resources.GetObject("btnArcInvModelos.Image"), System.Drawing.Image)
        Me.btnArcInvModelos.LargeImage = CType(resources.GetObject("btnArcInvModelos.LargeImage"), System.Drawing.Image)
        Me.btnArcInvModelos.Name = "btnArcInvModelos"
        Me.btnArcInvModelos.SmallImage = CType(resources.GetObject("btnArcInvModelos.SmallImage"), System.Drawing.Image)
        Me.btnArcInvModelos.Text = "Modelos"
        '
        'btnArcInvTallas
        '
        Me.btnArcInvTallas.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcInvTallas.Image = CType(resources.GetObject("btnArcInvTallas.Image"), System.Drawing.Image)
        Me.btnArcInvTallas.LargeImage = CType(resources.GetObject("btnArcInvTallas.LargeImage"), System.Drawing.Image)
        Me.btnArcInvTallas.Name = "btnArcInvTallas"
        Me.btnArcInvTallas.SmallImage = CType(resources.GetObject("btnArcInvTallas.SmallImage"), System.Drawing.Image)
        Me.btnArcInvTallas.Text = "Tallas"
        '
        'btnArcInvColores
        '
        Me.btnArcInvColores.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcInvColores.Image = CType(resources.GetObject("btnArcInvColores.Image"), System.Drawing.Image)
        Me.btnArcInvColores.LargeImage = CType(resources.GetObject("btnArcInvColores.LargeImage"), System.Drawing.Image)
        Me.btnArcInvColores.Name = "btnArcInvColores"
        Me.btnArcInvColores.SmallImage = CType(resources.GetObject("btnArcInvColores.SmallImage"), System.Drawing.Image)
        Me.btnArcInvColores.Text = "Colores"
        '
        'btnArcInvRelaciones
        '
        Me.btnArcInvRelaciones.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcInvRelaciones.Image = CType(resources.GetObject("btnArcInvRelaciones.Image"), System.Drawing.Image)
        Me.btnArcInvRelaciones.LargeImage = CType(resources.GetObject("btnArcInvRelaciones.LargeImage"), System.Drawing.Image)
        Me.btnArcInvRelaciones.Name = "btnArcInvRelaciones"
        Me.btnArcInvRelaciones.SmallImage = CType(resources.GetObject("btnArcInvRelaciones.SmallImage"), System.Drawing.Image)
        Me.btnArcInvRelaciones.Text = "Relaciones"
        '
        'btnArcInvAlmacenes
        '
        Me.btnArcInvAlmacenes.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcInvAlmacenes.Image = CType(resources.GetObject("btnArcInvAlmacenes.Image"), System.Drawing.Image)
        Me.btnArcInvAlmacenes.LargeImage = CType(resources.GetObject("btnArcInvAlmacenes.LargeImage"), System.Drawing.Image)
        Me.btnArcInvAlmacenes.Name = "btnArcInvAlmacenes"
        Me.btnArcInvAlmacenes.SmallImage = CType(resources.GetObject("btnArcInvAlmacenes.SmallImage"), System.Drawing.Image)
        Me.btnArcInvAlmacenes.Text = "Almacenes"
        '
        'mnuArcServicios
        '
        Me.mnuArcServicios.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.mnuArcServicios.DropDownItems.Add(Me.btnArcSerCajas)
        Me.mnuArcServicios.DropDownItems.Add(Me.btnArcSerTecnicos)
        Me.mnuArcServicios.DropDownItems.Add(Me.btnArcSerClasificaciones)
        Me.mnuArcServicios.DropDownItems.Add(Me.btnArcSerOfertas)
        Me.mnuArcServicios.Image = CType(resources.GetObject("mnuArcServicios.Image"), System.Drawing.Image)
        Me.mnuArcServicios.LargeImage = CType(resources.GetObject("mnuArcServicios.LargeImage"), System.Drawing.Image)
        Me.mnuArcServicios.Name = "mnuArcServicios"
        Me.mnuArcServicios.SmallImage = CType(resources.GetObject("mnuArcServicios.SmallImage"), System.Drawing.Image)
        Me.mnuArcServicios.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.mnuArcServicios.Text = "Servicios"
        '
        'btnArcSerCajas
        '
        Me.btnArcSerCajas.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcSerCajas.Image = CType(resources.GetObject("btnArcSerCajas.Image"), System.Drawing.Image)
        Me.btnArcSerCajas.LargeImage = CType(resources.GetObject("btnArcSerCajas.LargeImage"), System.Drawing.Image)
        Me.btnArcSerCajas.Name = "btnArcSerCajas"
        Me.btnArcSerCajas.SmallImage = CType(resources.GetObject("btnArcSerCajas.SmallImage"), System.Drawing.Image)
        Me.btnArcSerCajas.Text = "Cajas"
        '
        'btnArcSerTecnicos
        '
        Me.btnArcSerTecnicos.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcSerTecnicos.Image = CType(resources.GetObject("btnArcSerTecnicos.Image"), System.Drawing.Image)
        Me.btnArcSerTecnicos.LargeImage = CType(resources.GetObject("btnArcSerTecnicos.LargeImage"), System.Drawing.Image)
        Me.btnArcSerTecnicos.Name = "btnArcSerTecnicos"
        Me.btnArcSerTecnicos.SmallImage = CType(resources.GetObject("btnArcSerTecnicos.SmallImage"), System.Drawing.Image)
        Me.btnArcSerTecnicos.Text = "Técnicos"
        '
        'btnArcSerClasificaciones
        '
        Me.btnArcSerClasificaciones.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcSerClasificaciones.Image = CType(resources.GetObject("btnArcSerClasificaciones.Image"), System.Drawing.Image)
        Me.btnArcSerClasificaciones.LargeImage = CType(resources.GetObject("btnArcSerClasificaciones.LargeImage"), System.Drawing.Image)
        Me.btnArcSerClasificaciones.Name = "btnArcSerClasificaciones"
        Me.btnArcSerClasificaciones.SmallImage = CType(resources.GetObject("btnArcSerClasificaciones.SmallImage"), System.Drawing.Image)
        Me.btnArcSerClasificaciones.Text = "Clasificaciones"
        '
        'btnArcSerOfertas
        '
        Me.btnArcSerOfertas.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.btnArcSerOfertas.Image = CType(resources.GetObject("btnArcSerOfertas.Image"), System.Drawing.Image)
        Me.btnArcSerOfertas.LargeImage = CType(resources.GetObject("btnArcSerOfertas.LargeImage"), System.Drawing.Image)
        Me.btnArcSerOfertas.Name = "btnArcSerOfertas"
        Me.btnArcSerOfertas.SmallImage = CType(resources.GetObject("btnArcSerOfertas.SmallImage"), System.Drawing.Image)
        Me.btnArcSerOfertas.Text = "Ofertas"
        '
        'btnSalir
        '
        Me.btnSalir.Image = CType(resources.GetObject("btnSalir.Image"), System.Drawing.Image)
        Me.btnSalir.LargeImage = CType(resources.GetObject("btnSalir.LargeImage"), System.Drawing.Image)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.SmallImage = CType(resources.GetObject("btnSalir.SmallImage"), System.Drawing.Image)
        Me.btnSalir.Text = "Salir"
        '
        'btnCambiarEmpresa
        '
        Me.btnCambiarEmpresa.Image = CType(resources.GetObject("btnCambiarEmpresa.Image"), System.Drawing.Image)
        Me.btnCambiarEmpresa.LargeImage = CType(resources.GetObject("btnCambiarEmpresa.LargeImage"), System.Drawing.Image)
        Me.btnCambiarEmpresa.Name = "btnCambiarEmpresa"
        Me.btnCambiarEmpresa.SmallImage = CType(resources.GetObject("btnCambiarEmpresa.SmallImage"), System.Drawing.Image)
        Me.btnCambiarEmpresa.Text = "Cambiar empresa"
        '
        'btnCambiarUsuario
        '
        Me.btnCambiarUsuario.Image = CType(resources.GetObject("btnCambiarUsuario.Image"), System.Drawing.Image)
        Me.btnCambiarUsuario.LargeImage = CType(resources.GetObject("btnCambiarUsuario.LargeImage"), System.Drawing.Image)
        Me.btnCambiarUsuario.Name = "btnCambiarUsuario"
        Me.btnCambiarUsuario.SmallImage = CType(resources.GetObject("btnCambiarUsuario.SmallImage"), System.Drawing.Image)
        Me.btnCambiarUsuario.Text = "Cambiar usuario"
        '
        'btnAcerca
        '
        Me.btnAcerca.Image = CType(resources.GetObject("btnAcerca.Image"), System.Drawing.Image)
        Me.btnAcerca.LargeImage = CType(resources.GetObject("btnAcerca.LargeImage"), System.Drawing.Image)
        Me.btnAcerca.Name = "btnAcerca"
        Me.btnAcerca.SmallImage = CType(resources.GetObject("btnAcerca.SmallImage"), System.Drawing.Image)
        Me.btnAcerca.Text = "Acerca de..."
        '
        'tabVentas
        '
        Me.tabVentas.Name = "tabVentas"
        Me.tabVentas.Panels.Add(Me.pnlVentasExpedir)
        Me.tabVentas.Panels.Add(Me.pnlVentasPagos)
        Me.tabVentas.Panels.Add(Me.pnlVentasConsultas)
        Me.tabVentas.Panels.Add(Me.RibbonPanel1)
        Me.tabVentas.Text = "Ventas"
        '
        'pnlVentasExpedir
        '
        Me.pnlVentasExpedir.ButtonMoreVisible = False
        Me.pnlVentasExpedir.Items.Add(Me.btnVentasFac)
        Me.pnlVentasExpedir.Items.Add(Me.btnVenCapRemisiones)
        Me.pnlVentasExpedir.Items.Add(Me.btnVenCapNotasCredito)
        Me.pnlVentasExpedir.Items.Add(Me.btnVenCapCotizaciones)
        Me.pnlVentasExpedir.Items.Add(Me.btnVenCapDevoluciones)
        Me.pnlVentasExpedir.Items.Add(Me.mnuVentasApartados)
        Me.pnlVentasExpedir.Items.Add(Me.mnuVentasNotasCargo)
        Me.pnlVentasExpedir.Items.Add(Me.mnuPedidos)
        Me.pnlVentasExpedir.Name = "pnlVentasExpedir"
        Me.pnlVentasExpedir.Text = ""
        '
        'btnVentasFac
        '
        Me.btnVentasFac.Image = CType(resources.GetObject("btnVentasFac.Image"), System.Drawing.Image)
        Me.btnVentasFac.LargeImage = CType(resources.GetObject("btnVentasFac.LargeImage"), System.Drawing.Image)
        Me.btnVentasFac.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large
        Me.btnVentasFac.Name = "btnVentasFac"
        Me.btnVentasFac.SmallImage = CType(resources.GetObject("btnVentasFac.SmallImage"), System.Drawing.Image)
        Me.btnVentasFac.Text = ""
        Me.btnVentasFac.ToolTip = "Facturas"
        '
        'btnVenCapRemisiones
        '
        Me.btnVenCapRemisiones.Image = CType(resources.GetObject("btnVenCapRemisiones.Image"), System.Drawing.Image)
        Me.btnVenCapRemisiones.LargeImage = CType(resources.GetObject("btnVenCapRemisiones.LargeImage"), System.Drawing.Image)
        Me.btnVenCapRemisiones.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large
        Me.btnVenCapRemisiones.Name = "btnVenCapRemisiones"
        Me.btnVenCapRemisiones.SmallImage = CType(resources.GetObject("btnVenCapRemisiones.SmallImage"), System.Drawing.Image)
        Me.btnVenCapRemisiones.Text = ""
        Me.btnVenCapRemisiones.ToolTip = "Remisiones"
        '
        'btnVenCapNotasCredito
        '
        Me.btnVenCapNotasCredito.Image = CType(resources.GetObject("btnVenCapNotasCredito.Image"), System.Drawing.Image)
        Me.btnVenCapNotasCredito.LargeImage = CType(resources.GetObject("btnVenCapNotasCredito.LargeImage"), System.Drawing.Image)
        Me.btnVenCapNotasCredito.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large
        Me.btnVenCapNotasCredito.Name = "btnVenCapNotasCredito"
        Me.btnVenCapNotasCredito.SmallImage = CType(resources.GetObject("btnVenCapNotasCredito.SmallImage"), System.Drawing.Image)
        Me.btnVenCapNotasCredito.Text = ""
        Me.btnVenCapNotasCredito.ToolTip = "Notas de crédito"
        '
        'btnVenCapCotizaciones
        '
        Me.btnVenCapCotizaciones.Image = CType(resources.GetObject("btnVenCapCotizaciones.Image"), System.Drawing.Image)
        Me.btnVenCapCotizaciones.LargeImage = CType(resources.GetObject("btnVenCapCotizaciones.LargeImage"), System.Drawing.Image)
        Me.btnVenCapCotizaciones.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large
        Me.btnVenCapCotizaciones.Name = "btnVenCapCotizaciones"
        Me.btnVenCapCotizaciones.SmallImage = CType(resources.GetObject("btnVenCapCotizaciones.SmallImage"), System.Drawing.Image)
        Me.btnVenCapCotizaciones.Text = ""
        Me.btnVenCapCotizaciones.ToolTip = "Cotizaciones"
        '
        'btnVenCapDevoluciones
        '
        Me.btnVenCapDevoluciones.Image = CType(resources.GetObject("btnVenCapDevoluciones.Image"), System.Drawing.Image)
        Me.btnVenCapDevoluciones.LargeImage = CType(resources.GetObject("btnVenCapDevoluciones.LargeImage"), System.Drawing.Image)
        Me.btnVenCapDevoluciones.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large
        Me.btnVenCapDevoluciones.Name = "btnVenCapDevoluciones"
        Me.btnVenCapDevoluciones.SmallImage = CType(resources.GetObject("btnVenCapDevoluciones.SmallImage"), System.Drawing.Image)
        Me.btnVenCapDevoluciones.Text = ""
        Me.btnVenCapDevoluciones.ToolTip = "Devoluciones"
        '
        'mnuVentasApartados
        '
        Me.mnuVentasApartados.Image = CType(resources.GetObject("mnuVentasApartados.Image"), System.Drawing.Image)
        Me.mnuVentasApartados.LargeImage = CType(resources.GetObject("mnuVentasApartados.LargeImage"), System.Drawing.Image)
        Me.mnuVentasApartados.Name = "mnuVentasApartados"
        Me.mnuVentasApartados.SmallImage = CType(resources.GetObject("mnuVentasApartados.SmallImage"), System.Drawing.Image)
        '
        'mnuVentasNotasCargo
        '
        Me.mnuVentasNotasCargo.Image = CType(resources.GetObject("mnuVentasNotasCargo.Image"), System.Drawing.Image)
        Me.mnuVentasNotasCargo.LargeImage = CType(resources.GetObject("mnuVentasNotasCargo.LargeImage"), System.Drawing.Image)
        Me.mnuVentasNotasCargo.Name = "mnuVentasNotasCargo"
        Me.mnuVentasNotasCargo.SmallImage = CType(resources.GetObject("mnuVentasNotasCargo.SmallImage"), System.Drawing.Image)
        Me.mnuVentasNotasCargo.ToolTip = "Notas de Cargo"
        '
        'mnuPedidos
        '
        Me.mnuPedidos.Image = CType(resources.GetObject("mnuPedidos.Image"), System.Drawing.Image)
        Me.mnuPedidos.LargeImage = CType(resources.GetObject("mnuPedidos.LargeImage"), System.Drawing.Image)
        Me.mnuPedidos.Name = "mnuPedidos"
        Me.mnuPedidos.SmallImage = CType(resources.GetObject("mnuPedidos.SmallImage"), System.Drawing.Image)
        '
        'pnlVentasPagos
        '
        Me.pnlVentasPagos.Items.Add(Me.btnVenPagFacturas)
        Me.pnlVentasPagos.Items.Add(Me.btnVenPagRemisiones)
        Me.pnlVentasPagos.Items.Add(Me.RibbonButton2)
        Me.pnlVentasPagos.Name = "pnlVentasPagos"
        Me.pnlVentasPagos.Text = ""
        '
        'btnVenPagFacturas
        '
        Me.btnVenPagFacturas.Image = CType(resources.GetObject("btnVenPagFacturas.Image"), System.Drawing.Image)
        Me.btnVenPagFacturas.LargeImage = CType(resources.GetObject("btnVenPagFacturas.LargeImage"), System.Drawing.Image)
        Me.btnVenPagFacturas.Name = "btnVenPagFacturas"
        Me.btnVenPagFacturas.SmallImage = CType(resources.GetObject("btnVenPagFacturas.SmallImage"), System.Drawing.Image)
        Me.btnVenPagFacturas.Text = ""
        Me.btnVenPagFacturas.ToolTip = "Pagos Facturas"
        '
        'btnVenPagRemisiones
        '
        Me.btnVenPagRemisiones.Image = CType(resources.GetObject("btnVenPagRemisiones.Image"), System.Drawing.Image)
        Me.btnVenPagRemisiones.LargeImage = CType(resources.GetObject("btnVenPagRemisiones.LargeImage"), System.Drawing.Image)
        Me.btnVenPagRemisiones.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large
        Me.btnVenPagRemisiones.Name = "btnVenPagRemisiones"
        Me.btnVenPagRemisiones.SmallImage = CType(resources.GetObject("btnVenPagRemisiones.SmallImage"), System.Drawing.Image)
        Me.btnVenPagRemisiones.Text = ""
        '
        'RibbonButton2
        '
        Me.RibbonButton2.Image = CType(resources.GetObject("RibbonButton2.Image"), System.Drawing.Image)
        Me.RibbonButton2.LargeImage = CType(resources.GetObject("RibbonButton2.LargeImage"), System.Drawing.Image)
        Me.RibbonButton2.Name = "RibbonButton2"
        Me.RibbonButton2.SmallImage = CType(resources.GetObject("RibbonButton2.SmallImage"), System.Drawing.Image)
        '
        'pnlVentasConsultas
        '
        Me.pnlVentasConsultas.Items.Add(Me.btnVenConReportes)
        Me.pnlVentasConsultas.Items.Add(Me.btnVenConMovimientos)
        Me.pnlVentasConsultas.Items.Add(Me.btnVenConHistorial)
        Me.pnlVentasConsultas.Items.Add(Me.btnVenConCorte)
        Me.pnlVentasConsultas.Items.Add(Me.mnuVentasConOfertas)
        Me.pnlVentasConsultas.Name = "pnlVentasConsultas"
        Me.pnlVentasConsultas.Text = ""
        '
        'btnVenConReportes
        '
        Me.btnVenConReportes.Image = CType(resources.GetObject("btnVenConReportes.Image"), System.Drawing.Image)
        Me.btnVenConReportes.LargeImage = CType(resources.GetObject("btnVenConReportes.LargeImage"), System.Drawing.Image)
        Me.btnVenConReportes.Name = "btnVenConReportes"
        Me.btnVenConReportes.SmallImage = CType(resources.GetObject("btnVenConReportes.SmallImage"), System.Drawing.Image)
        Me.btnVenConReportes.Text = ""
        Me.btnVenConReportes.ToolTip = "Reportes"
        '
        'btnVenConMovimientos
        '
        Me.btnVenConMovimientos.Image = CType(resources.GetObject("btnVenConMovimientos.Image"), System.Drawing.Image)
        Me.btnVenConMovimientos.LargeImage = CType(resources.GetObject("btnVenConMovimientos.LargeImage"), System.Drawing.Image)
        Me.btnVenConMovimientos.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large
        Me.btnVenConMovimientos.Name = "btnVenConMovimientos"
        Me.btnVenConMovimientos.SmallImage = CType(resources.GetObject("btnVenConMovimientos.SmallImage"), System.Drawing.Image)
        Me.btnVenConMovimientos.Text = ""
        '
        'btnVenConHistorial
        '
        Me.btnVenConHistorial.Image = CType(resources.GetObject("btnVenConHistorial.Image"), System.Drawing.Image)
        Me.btnVenConHistorial.LargeImage = CType(resources.GetObject("btnVenConHistorial.LargeImage"), System.Drawing.Image)
        Me.btnVenConHistorial.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large
        Me.btnVenConHistorial.Name = "btnVenConHistorial"
        Me.btnVenConHistorial.SmallImage = CType(resources.GetObject("btnVenConHistorial.SmallImage"), System.Drawing.Image)
        Me.btnVenConHistorial.Text = "Historial"
        '
        'btnVenConCorte
        '
        Me.btnVenConCorte.Image = CType(resources.GetObject("btnVenConCorte.Image"), System.Drawing.Image)
        Me.btnVenConCorte.LargeImage = CType(resources.GetObject("btnVenConCorte.LargeImage"), System.Drawing.Image)
        Me.btnVenConCorte.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large
        Me.btnVenConCorte.Name = "btnVenConCorte"
        Me.btnVenConCorte.SmallImage = CType(resources.GetObject("btnVenConCorte.SmallImage"), System.Drawing.Image)
        Me.btnVenConCorte.Text = "Corte global"
        '
        'tabCompras
        '
        Me.tabCompras.Name = "tabCompras"
        Me.tabCompras.Panels.Add(Me.pnlComCapturar)
        Me.tabCompras.Panels.Add(Me.pnlComGranos)
        Me.tabCompras.Panels.Add(Me.pnlComConsultas)
        Me.tabCompras.Text = "Compras"
        '
        'pnlComCapturar
        '
        Me.pnlComCapturar.Items.Add(Me.btnComCapFacturas)
        Me.pnlComCapturar.Items.Add(Me.btnComCapRemisiones)
        Me.pnlComCapturar.Items.Add(Me.btnComCapOrdenes)
        Me.pnlComCapturar.Items.Add(Me.btnComCapPreordenes)
        Me.pnlComCapturar.Items.Add(Me.btnComCapDevoluciones)
        Me.pnlComCapturar.Items.Add(Me.btnComCapNotasCargo)
        Me.pnlComCapturar.Items.Add(Me.btnComCapNotasCredito)
        Me.pnlComCapturar.Items.Add(Me.btnComCapPagos)
        Me.pnlComCapturar.Items.Add(Me.btnComCapDocumentos)
        Me.pnlComCapturar.Name = "pnlComCapturar"
        Me.pnlComCapturar.Text = "Capturar"
        '
        'btnComCapFacturas
        '
        Me.btnComCapFacturas.Image = CType(resources.GetObject("btnComCapFacturas.Image"), System.Drawing.Image)
        Me.btnComCapFacturas.LargeImage = CType(resources.GetObject("btnComCapFacturas.LargeImage"), System.Drawing.Image)
        Me.btnComCapFacturas.Name = "btnComCapFacturas"
        Me.btnComCapFacturas.SmallImage = CType(resources.GetObject("btnComCapFacturas.SmallImage"), System.Drawing.Image)
        Me.btnComCapFacturas.Text = "Facturas"
        '
        'btnComCapRemisiones
        '
        Me.btnComCapRemisiones.Image = CType(resources.GetObject("btnComCapRemisiones.Image"), System.Drawing.Image)
        Me.btnComCapRemisiones.LargeImage = CType(resources.GetObject("btnComCapRemisiones.LargeImage"), System.Drawing.Image)
        Me.btnComCapRemisiones.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComCapRemisiones.Name = "btnComCapRemisiones"
        Me.btnComCapRemisiones.SmallImage = CType(resources.GetObject("btnComCapRemisiones.SmallImage"), System.Drawing.Image)
        Me.btnComCapRemisiones.Text = "Remisiones"
        '
        'btnComCapOrdenes
        '
        Me.btnComCapOrdenes.Image = CType(resources.GetObject("btnComCapOrdenes.Image"), System.Drawing.Image)
        Me.btnComCapOrdenes.LargeImage = CType(resources.GetObject("btnComCapOrdenes.LargeImage"), System.Drawing.Image)
        Me.btnComCapOrdenes.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComCapOrdenes.Name = "btnComCapOrdenes"
        Me.btnComCapOrdenes.SmallImage = CType(resources.GetObject("btnComCapOrdenes.SmallImage"), System.Drawing.Image)
        Me.btnComCapOrdenes.Text = "Ordenes"
        '
        'btnComCapPreordenes
        '
        Me.btnComCapPreordenes.Image = CType(resources.GetObject("btnComCapPreordenes.Image"), System.Drawing.Image)
        Me.btnComCapPreordenes.LargeImage = CType(resources.GetObject("btnComCapPreordenes.LargeImage"), System.Drawing.Image)
        Me.btnComCapPreordenes.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComCapPreordenes.Name = "btnComCapPreordenes"
        Me.btnComCapPreordenes.SmallImage = CType(resources.GetObject("btnComCapPreordenes.SmallImage"), System.Drawing.Image)
        Me.btnComCapPreordenes.Text = "Preordenes"
        '
        'btnComCapDevoluciones
        '
        Me.btnComCapDevoluciones.Image = CType(resources.GetObject("btnComCapDevoluciones.Image"), System.Drawing.Image)
        Me.btnComCapDevoluciones.LargeImage = CType(resources.GetObject("btnComCapDevoluciones.LargeImage"), System.Drawing.Image)
        Me.btnComCapDevoluciones.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComCapDevoluciones.Name = "btnComCapDevoluciones"
        Me.btnComCapDevoluciones.SmallImage = CType(resources.GetObject("btnComCapDevoluciones.SmallImage"), System.Drawing.Image)
        Me.btnComCapDevoluciones.Text = "Devoluciones"
        '
        'btnComCapNotasCargo
        '
        Me.btnComCapNotasCargo.Image = CType(resources.GetObject("btnComCapNotasCargo.Image"), System.Drawing.Image)
        Me.btnComCapNotasCargo.LargeImage = CType(resources.GetObject("btnComCapNotasCargo.LargeImage"), System.Drawing.Image)
        Me.btnComCapNotasCargo.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComCapNotasCargo.Name = "btnComCapNotasCargo"
        Me.btnComCapNotasCargo.SmallImage = CType(resources.GetObject("btnComCapNotasCargo.SmallImage"), System.Drawing.Image)
        Me.btnComCapNotasCargo.Text = "Notas cargo"
        '
        'btnComCapNotasCredito
        '
        Me.btnComCapNotasCredito.Image = CType(resources.GetObject("btnComCapNotasCredito.Image"), System.Drawing.Image)
        Me.btnComCapNotasCredito.LargeImage = CType(resources.GetObject("btnComCapNotasCredito.LargeImage"), System.Drawing.Image)
        Me.btnComCapNotasCredito.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComCapNotasCredito.Name = "btnComCapNotasCredito"
        Me.btnComCapNotasCredito.SmallImage = CType(resources.GetObject("btnComCapNotasCredito.SmallImage"), System.Drawing.Image)
        Me.btnComCapNotasCredito.Text = "Notas crédito"
        '
        'btnComCapPagos
        '
        Me.btnComCapPagos.Image = CType(resources.GetObject("btnComCapPagos.Image"), System.Drawing.Image)
        Me.btnComCapPagos.LargeImage = CType(resources.GetObject("btnComCapPagos.LargeImage"), System.Drawing.Image)
        Me.btnComCapPagos.Name = "btnComCapPagos"
        Me.btnComCapPagos.SmallImage = CType(resources.GetObject("btnComCapPagos.SmallImage"), System.Drawing.Image)
        Me.btnComCapPagos.Text = "Pagos"
        '
        'btnComCapDocumentos
        '
        Me.btnComCapDocumentos.Image = CType(resources.GetObject("btnComCapDocumentos.Image"), System.Drawing.Image)
        Me.btnComCapDocumentos.LargeImage = CType(resources.GetObject("btnComCapDocumentos.LargeImage"), System.Drawing.Image)
        Me.btnComCapDocumentos.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComCapDocumentos.Name = "btnComCapDocumentos"
        Me.btnComCapDocumentos.SmallImage = CType(resources.GetObject("btnComCapDocumentos.SmallImage"), System.Drawing.Image)
        Me.btnComCapDocumentos.Text = "Documentos"
        '
        'pnlComGranos
        '
        Me.pnlComGranos.Items.Add(Me.btnComGraBoletas)
        Me.pnlComGranos.Items.Add(Me.btnComGraLiquidaciones)
        Me.pnlComGranos.Items.Add(Me.btnComGraComprobantes)
        Me.pnlComGranos.Items.Add(Me.btnComGraReportes)
        Me.pnlComGranos.Name = "pnlComGranos"
        Me.pnlComGranos.Text = "Granos"
        '
        'btnComGraBoletas
        '
        Me.btnComGraBoletas.Image = CType(resources.GetObject("btnComGraBoletas.Image"), System.Drawing.Image)
        Me.btnComGraBoletas.LargeImage = CType(resources.GetObject("btnComGraBoletas.LargeImage"), System.Drawing.Image)
        Me.btnComGraBoletas.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComGraBoletas.Name = "btnComGraBoletas"
        Me.btnComGraBoletas.SmallImage = CType(resources.GetObject("btnComGraBoletas.SmallImage"), System.Drawing.Image)
        Me.btnComGraBoletas.Text = "Boletas"
        '
        'btnComGraLiquidaciones
        '
        Me.btnComGraLiquidaciones.Image = CType(resources.GetObject("btnComGraLiquidaciones.Image"), System.Drawing.Image)
        Me.btnComGraLiquidaciones.LargeImage = CType(resources.GetObject("btnComGraLiquidaciones.LargeImage"), System.Drawing.Image)
        Me.btnComGraLiquidaciones.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComGraLiquidaciones.Name = "btnComGraLiquidaciones"
        Me.btnComGraLiquidaciones.SmallImage = CType(resources.GetObject("btnComGraLiquidaciones.SmallImage"), System.Drawing.Image)
        Me.btnComGraLiquidaciones.Text = "Liquidaciones"
        '
        'btnComGraComprobantes
        '
        Me.btnComGraComprobantes.Image = CType(resources.GetObject("btnComGraComprobantes.Image"), System.Drawing.Image)
        Me.btnComGraComprobantes.LargeImage = CType(resources.GetObject("btnComGraComprobantes.LargeImage"), System.Drawing.Image)
        Me.btnComGraComprobantes.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComGraComprobantes.Name = "btnComGraComprobantes"
        Me.btnComGraComprobantes.SmallImage = CType(resources.GetObject("btnComGraComprobantes.SmallImage"), System.Drawing.Image)
        Me.btnComGraComprobantes.Text = "Comprobantes"
        '
        'btnComGraReportes
        '
        Me.btnComGraReportes.Image = CType(resources.GetObject("btnComGraReportes.Image"), System.Drawing.Image)
        Me.btnComGraReportes.LargeImage = CType(resources.GetObject("btnComGraReportes.LargeImage"), System.Drawing.Image)
        Me.btnComGraReportes.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComGraReportes.Name = "btnComGraReportes"
        Me.btnComGraReportes.SmallImage = CType(resources.GetObject("btnComGraReportes.SmallImage"), System.Drawing.Image)
        Me.btnComGraReportes.Text = "Reportes"
        '
        'pnlComConsultas
        '
        Me.pnlComConsultas.Items.Add(Me.btnComConReportes)
        Me.pnlComConsultas.Items.Add(Me.btnComConMovimientos)
        Me.pnlComConsultas.Items.Add(Me.btnComConDevoluciones)
        Me.pnlComConsultas.Items.Add(Me.btnComConValidador)
        Me.pnlComConsultas.Name = "pnlComConsultas"
        Me.pnlComConsultas.Text = "Consultas"
        '
        'btnComConReportes
        '
        Me.btnComConReportes.Image = CType(resources.GetObject("btnComConReportes.Image"), System.Drawing.Image)
        Me.btnComConReportes.LargeImage = CType(resources.GetObject("btnComConReportes.LargeImage"), System.Drawing.Image)
        Me.btnComConReportes.Name = "btnComConReportes"
        Me.btnComConReportes.SmallImage = CType(resources.GetObject("btnComConReportes.SmallImage"), System.Drawing.Image)
        Me.btnComConReportes.Text = "Reportes"
        '
        'btnComConMovimientos
        '
        Me.btnComConMovimientos.Image = CType(resources.GetObject("btnComConMovimientos.Image"), System.Drawing.Image)
        Me.btnComConMovimientos.LargeImage = CType(resources.GetObject("btnComConMovimientos.LargeImage"), System.Drawing.Image)
        Me.btnComConMovimientos.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComConMovimientos.Name = "btnComConMovimientos"
        Me.btnComConMovimientos.SmallImage = CType(resources.GetObject("btnComConMovimientos.SmallImage"), System.Drawing.Image)
        Me.btnComConMovimientos.Text = "Movimientos"
        '
        'btnComConDevoluciones
        '
        Me.btnComConDevoluciones.Image = CType(resources.GetObject("btnComConDevoluciones.Image"), System.Drawing.Image)
        Me.btnComConDevoluciones.LargeImage = CType(resources.GetObject("btnComConDevoluciones.LargeImage"), System.Drawing.Image)
        Me.btnComConDevoluciones.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComConDevoluciones.Name = "btnComConDevoluciones"
        Me.btnComConDevoluciones.SmallImage = CType(resources.GetObject("btnComConDevoluciones.SmallImage"), System.Drawing.Image)
        Me.btnComConDevoluciones.Text = "Devoluciones"
        '
        'btnComConValidador
        '
        Me.btnComConValidador.Image = CType(resources.GetObject("btnComConValidador.Image"), System.Drawing.Image)
        Me.btnComConValidador.LargeImage = CType(resources.GetObject("btnComConValidador.LargeImage"), System.Drawing.Image)
        Me.btnComConValidador.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnComConValidador.Name = "btnComConValidador"
        Me.btnComConValidador.SmallImage = CType(resources.GetObject("btnComConValidador.SmallImage"), System.Drawing.Image)
        Me.btnComConValidador.Text = "Validador"
        '
        'tabInventario
        '
        Me.tabInventario.Name = "tabInventario"
        Me.tabInventario.Panels.Add(Me.pnlInvOperacion)
        Me.tabInventario.Panels.Add(Me.pnlInvConsultas)
        Me.tabInventario.Panels.Add(Me.pnlInvHerramientas)
        Me.tabInventario.Text = "Inventario"
        '
        'pnlInvOperacion
        '
        Me.pnlInvOperacion.Items.Add(Me.btnInvOpeMovimientos)
        Me.pnlInvOperacion.Items.Add(Me.btnInvOpePedidos)
        Me.pnlInvOperacion.Items.Add(Me.btnInvOpeBoletas)
        Me.pnlInvOperacion.Name = "pnlInvOperacion"
        Me.pnlInvOperacion.Text = "Operación"
        '
        'btnInvOpeMovimientos
        '
        Me.btnInvOpeMovimientos.Image = CType(resources.GetObject("btnInvOpeMovimientos.Image"), System.Drawing.Image)
        Me.btnInvOpeMovimientos.LargeImage = CType(resources.GetObject("btnInvOpeMovimientos.LargeImage"), System.Drawing.Image)
        Me.btnInvOpeMovimientos.Name = "btnInvOpeMovimientos"
        Me.btnInvOpeMovimientos.SmallImage = CType(resources.GetObject("btnInvOpeMovimientos.SmallImage"), System.Drawing.Image)
        Me.btnInvOpeMovimientos.Text = "Movimientos"
        '
        'btnInvOpePedidos
        '
        Me.btnInvOpePedidos.Image = CType(resources.GetObject("btnInvOpePedidos.Image"), System.Drawing.Image)
        Me.btnInvOpePedidos.LargeImage = CType(resources.GetObject("btnInvOpePedidos.LargeImage"), System.Drawing.Image)
        Me.btnInvOpePedidos.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvOpePedidos.Name = "btnInvOpePedidos"
        Me.btnInvOpePedidos.SmallImage = CType(resources.GetObject("btnInvOpePedidos.SmallImage"), System.Drawing.Image)
        Me.btnInvOpePedidos.Text = "Pedidos"
        '
        'btnInvOpeBoletas
        '
        Me.btnInvOpeBoletas.Image = CType(resources.GetObject("btnInvOpeBoletas.Image"), System.Drawing.Image)
        Me.btnInvOpeBoletas.LargeImage = CType(resources.GetObject("btnInvOpeBoletas.LargeImage"), System.Drawing.Image)
        Me.btnInvOpeBoletas.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvOpeBoletas.Name = "btnInvOpeBoletas"
        Me.btnInvOpeBoletas.SmallImage = CType(resources.GetObject("btnInvOpeBoletas.SmallImage"), System.Drawing.Image)
        Me.btnInvOpeBoletas.Text = "Boletas"
        '
        'pnlInvConsultas
        '
        Me.pnlInvConsultas.Items.Add(Me.btnInvConReportes)
        Me.pnlInvConsultas.Items.Add(Me.btnInvConInventario)
        Me.pnlInvConsultas.Items.Add(Me.btnInvConMonitor)
        Me.pnlInvConsultas.Items.Add(Me.btnInvConCardex)
        Me.pnlInvConsultas.Items.Add(Me.btnInvConRevision)
        Me.pnlInvConsultas.Items.Add(Me.btnInvConReportesBoletas)
        Me.pnlInvConsultas.Name = "pnlInvConsultas"
        Me.pnlInvConsultas.Text = "Consultas"
        '
        'btnInvConReportes
        '
        Me.btnInvConReportes.Image = CType(resources.GetObject("btnInvConReportes.Image"), System.Drawing.Image)
        Me.btnInvConReportes.LargeImage = CType(resources.GetObject("btnInvConReportes.LargeImage"), System.Drawing.Image)
        Me.btnInvConReportes.Name = "btnInvConReportes"
        Me.btnInvConReportes.SmallImage = CType(resources.GetObject("btnInvConReportes.SmallImage"), System.Drawing.Image)
        Me.btnInvConReportes.Text = "Reportes"
        '
        'btnInvConInventario
        '
        Me.btnInvConInventario.Image = CType(resources.GetObject("btnInvConInventario.Image"), System.Drawing.Image)
        Me.btnInvConInventario.LargeImage = CType(resources.GetObject("btnInvConInventario.LargeImage"), System.Drawing.Image)
        Me.btnInvConInventario.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvConInventario.Name = "btnInvConInventario"
        Me.btnInvConInventario.SmallImage = CType(resources.GetObject("btnInvConInventario.SmallImage"), System.Drawing.Image)
        Me.btnInvConInventario.Text = "Inventario"
        '
        'btnInvConMonitor
        '
        Me.btnInvConMonitor.Image = CType(resources.GetObject("btnInvConMonitor.Image"), System.Drawing.Image)
        Me.btnInvConMonitor.LargeImage = CType(resources.GetObject("btnInvConMonitor.LargeImage"), System.Drawing.Image)
        Me.btnInvConMonitor.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvConMonitor.Name = "btnInvConMonitor"
        Me.btnInvConMonitor.SmallImage = CType(resources.GetObject("btnInvConMonitor.SmallImage"), System.Drawing.Image)
        Me.btnInvConMonitor.Text = "Monitor pedidos"
        '
        'btnInvConCardex
        '
        Me.btnInvConCardex.Image = CType(resources.GetObject("btnInvConCardex.Image"), System.Drawing.Image)
        Me.btnInvConCardex.LargeImage = CType(resources.GetObject("btnInvConCardex.LargeImage"), System.Drawing.Image)
        Me.btnInvConCardex.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvConCardex.Name = "btnInvConCardex"
        Me.btnInvConCardex.SmallImage = CType(resources.GetObject("btnInvConCardex.SmallImage"), System.Drawing.Image)
        Me.btnInvConCardex.Text = "Cardex"
        '
        'btnInvConRevision
        '
        Me.btnInvConRevision.Image = CType(resources.GetObject("btnInvConRevision.Image"), System.Drawing.Image)
        Me.btnInvConRevision.LargeImage = CType(resources.GetObject("btnInvConRevision.LargeImage"), System.Drawing.Image)
        Me.btnInvConRevision.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvConRevision.Name = "btnInvConRevision"
        Me.btnInvConRevision.SmallImage = CType(resources.GetObject("btnInvConRevision.SmallImage"), System.Drawing.Image)
        Me.btnInvConRevision.Text = "Revisión inv."
        '
        'btnInvConReportesBoletas
        '
        Me.btnInvConReportesBoletas.Image = CType(resources.GetObject("btnInvConReportesBoletas.Image"), System.Drawing.Image)
        Me.btnInvConReportesBoletas.LargeImage = CType(resources.GetObject("btnInvConReportesBoletas.LargeImage"), System.Drawing.Image)
        Me.btnInvConReportesBoletas.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvConReportesBoletas.Name = "btnInvConReportesBoletas"
        Me.btnInvConReportesBoletas.SmallImage = CType(resources.GetObject("btnInvConReportesBoletas.SmallImage"), System.Drawing.Image)
        Me.btnInvConReportesBoletas.Text = "Boletas"
        '
        'pnlInvHerramientas
        '
        Me.pnlInvHerramientas.Items.Add(Me.btnInvHerRecCostos)
        Me.pnlInvHerramientas.Items.Add(Me.btnInvHerBuscarNeg)
        Me.pnlInvHerramientas.Items.Add(Me.btnInvHerRecInventario)
        Me.pnlInvHerramientas.Items.Add(Me.btnInvHerAjustarCero)
        Me.pnlInvHerramientas.Items.Add(Me.btnInvHerConfigConceptos)
        Me.pnlInvHerramientas.Name = "pnlInvHerramientas"
        Me.pnlInvHerramientas.Text = "Herramientas"
        '
        'btnInvHerRecCostos
        '
        Me.btnInvHerRecCostos.Image = CType(resources.GetObject("btnInvHerRecCostos.Image"), System.Drawing.Image)
        Me.btnInvHerRecCostos.LargeImage = CType(resources.GetObject("btnInvHerRecCostos.LargeImage"), System.Drawing.Image)
        Me.btnInvHerRecCostos.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvHerRecCostos.Name = "btnInvHerRecCostos"
        Me.btnInvHerRecCostos.SmallImage = CType(resources.GetObject("btnInvHerRecCostos.SmallImage"), System.Drawing.Image)
        Me.btnInvHerRecCostos.Text = "Recalcular costos"
        '
        'btnInvHerBuscarNeg
        '
        Me.btnInvHerBuscarNeg.Image = CType(resources.GetObject("btnInvHerBuscarNeg.Image"), System.Drawing.Image)
        Me.btnInvHerBuscarNeg.LargeImage = CType(resources.GetObject("btnInvHerBuscarNeg.LargeImage"), System.Drawing.Image)
        Me.btnInvHerBuscarNeg.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvHerBuscarNeg.Name = "btnInvHerBuscarNeg"
        Me.btnInvHerBuscarNeg.SmallImage = CType(resources.GetObject("btnInvHerBuscarNeg.SmallImage"), System.Drawing.Image)
        Me.btnInvHerBuscarNeg.Text = "Buscar negativos"
        '
        'btnInvHerRecInventario
        '
        Me.btnInvHerRecInventario.Image = CType(resources.GetObject("btnInvHerRecInventario.Image"), System.Drawing.Image)
        Me.btnInvHerRecInventario.LargeImage = CType(resources.GetObject("btnInvHerRecInventario.LargeImage"), System.Drawing.Image)
        Me.btnInvHerRecInventario.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvHerRecInventario.Name = "btnInvHerRecInventario"
        Me.btnInvHerRecInventario.SmallImage = CType(resources.GetObject("btnInvHerRecInventario.SmallImage"), System.Drawing.Image)
        Me.btnInvHerRecInventario.Text = "Recalc. inventario"
        '
        'btnInvHerAjustarCero
        '
        Me.btnInvHerAjustarCero.Image = CType(resources.GetObject("btnInvHerAjustarCero.Image"), System.Drawing.Image)
        Me.btnInvHerAjustarCero.LargeImage = CType(resources.GetObject("btnInvHerAjustarCero.LargeImage"), System.Drawing.Image)
        Me.btnInvHerAjustarCero.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvHerAjustarCero.Name = "btnInvHerAjustarCero"
        Me.btnInvHerAjustarCero.SmallImage = CType(resources.GetObject("btnInvHerAjustarCero.SmallImage"), System.Drawing.Image)
        Me.btnInvHerAjustarCero.Text = "Ajustar a cero"
        '
        'btnInvHerConfigConceptos
        '
        Me.btnInvHerConfigConceptos.Image = CType(resources.GetObject("btnInvHerConfigConceptos.Image"), System.Drawing.Image)
        Me.btnInvHerConfigConceptos.LargeImage = CType(resources.GetObject("btnInvHerConfigConceptos.LargeImage"), System.Drawing.Image)
        Me.btnInvHerConfigConceptos.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvHerConfigConceptos.Name = "btnInvHerConfigConceptos"
        Me.btnInvHerConfigConceptos.SmallImage = CType(resources.GetObject("btnInvHerConfigConceptos.SmallImage"), System.Drawing.Image)
        Me.btnInvHerConfigConceptos.Text = "Config. conceptos"
        '
        'tabServicios
        '
        Me.tabServicios.Name = "tabServicios"
        Me.tabServicios.Panels.Add(Me.pnlSerComercial)
        Me.tabServicios.Panels.Add(Me.pnlSerInterno)
        Me.tabServicios.Text = "Servicios"
        '
        'pnlSerComercial
        '
        Me.pnlSerComercial.Items.Add(Me.btnSerComNuevo)
        Me.pnlSerComercial.Items.Add(Me.btnSerComBuscar)
        Me.pnlSerComercial.Items.Add(Me.btnSerComReportes)
        Me.pnlSerComercial.Items.Add(Me.btnSerComEstados)
        Me.pnlSerComercial.Name = "pnlSerComercial"
        Me.pnlSerComercial.Text = "Comercial"
        '
        'btnSerComNuevo
        '
        Me.btnSerComNuevo.Image = CType(resources.GetObject("btnSerComNuevo.Image"), System.Drawing.Image)
        Me.btnSerComNuevo.LargeImage = CType(resources.GetObject("btnSerComNuevo.LargeImage"), System.Drawing.Image)
        Me.btnSerComNuevo.Name = "btnSerComNuevo"
        Me.btnSerComNuevo.SmallImage = CType(resources.GetObject("btnSerComNuevo.SmallImage"), System.Drawing.Image)
        Me.btnSerComNuevo.Text = "Nuevo"
        '
        'btnSerComBuscar
        '
        Me.btnSerComBuscar.Image = CType(resources.GetObject("btnSerComBuscar.Image"), System.Drawing.Image)
        Me.btnSerComBuscar.LargeImage = CType(resources.GetObject("btnSerComBuscar.LargeImage"), System.Drawing.Image)
        Me.btnSerComBuscar.Name = "btnSerComBuscar"
        Me.btnSerComBuscar.SmallImage = CType(resources.GetObject("btnSerComBuscar.SmallImage"), System.Drawing.Image)
        Me.btnSerComBuscar.Text = "Buscar"
        '
        'btnSerComReportes
        '
        Me.btnSerComReportes.Image = CType(resources.GetObject("btnSerComReportes.Image"), System.Drawing.Image)
        Me.btnSerComReportes.LargeImage = CType(resources.GetObject("btnSerComReportes.LargeImage"), System.Drawing.Image)
        Me.btnSerComReportes.Name = "btnSerComReportes"
        Me.btnSerComReportes.SmallImage = CType(resources.GetObject("btnSerComReportes.SmallImage"), System.Drawing.Image)
        Me.btnSerComReportes.Text = "Reportes"
        '
        'btnSerComEstados
        '
        Me.btnSerComEstados.Image = CType(resources.GetObject("btnSerComEstados.Image"), System.Drawing.Image)
        Me.btnSerComEstados.LargeImage = CType(resources.GetObject("btnSerComEstados.LargeImage"), System.Drawing.Image)
        Me.btnSerComEstados.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnSerComEstados.Name = "btnSerComEstados"
        Me.btnSerComEstados.SmallImage = CType(resources.GetObject("btnSerComEstados.SmallImage"), System.Drawing.Image)
        Me.btnSerComEstados.Text = "Estados"
        '
        'pnlSerInterno
        '
        Me.pnlSerInterno.Items.Add(Me.btnSerIntNuevo)
        Me.pnlSerInterno.Items.Add(Me.btnSerIntBuscar)
        Me.pnlSerInterno.Items.Add(Me.btnSerIntReporte)
        Me.pnlSerInterno.Items.Add(Me.btnSerIntEstados)
        Me.pnlSerInterno.Name = "pnlSerInterno"
        Me.pnlSerInterno.Text = "Interno"
        '
        'btnSerIntNuevo
        '
        Me.btnSerIntNuevo.Image = CType(resources.GetObject("btnSerIntNuevo.Image"), System.Drawing.Image)
        Me.btnSerIntNuevo.LargeImage = CType(resources.GetObject("btnSerIntNuevo.LargeImage"), System.Drawing.Image)
        Me.btnSerIntNuevo.Name = "btnSerIntNuevo"
        Me.btnSerIntNuevo.SmallImage = CType(resources.GetObject("btnSerIntNuevo.SmallImage"), System.Drawing.Image)
        Me.btnSerIntNuevo.Text = "Nuevo"
        '
        'btnSerIntBuscar
        '
        Me.btnSerIntBuscar.Image = CType(resources.GetObject("btnSerIntBuscar.Image"), System.Drawing.Image)
        Me.btnSerIntBuscar.LargeImage = CType(resources.GetObject("btnSerIntBuscar.LargeImage"), System.Drawing.Image)
        Me.btnSerIntBuscar.Name = "btnSerIntBuscar"
        Me.btnSerIntBuscar.SmallImage = CType(resources.GetObject("btnSerIntBuscar.SmallImage"), System.Drawing.Image)
        Me.btnSerIntBuscar.Text = "Buscar"
        '
        'btnSerIntReporte
        '
        Me.btnSerIntReporte.Image = CType(resources.GetObject("btnSerIntReporte.Image"), System.Drawing.Image)
        Me.btnSerIntReporte.LargeImage = CType(resources.GetObject("btnSerIntReporte.LargeImage"), System.Drawing.Image)
        Me.btnSerIntReporte.Name = "btnSerIntReporte"
        Me.btnSerIntReporte.SmallImage = CType(resources.GetObject("btnSerIntReporte.SmallImage"), System.Drawing.Image)
        Me.btnSerIntReporte.Text = "Reportes"
        '
        'btnSerIntEstados
        '
        Me.btnSerIntEstados.Image = CType(resources.GetObject("btnSerIntEstados.Image"), System.Drawing.Image)
        Me.btnSerIntEstados.LargeImage = CType(resources.GetObject("btnSerIntEstados.LargeImage"), System.Drawing.Image)
        Me.btnSerIntEstados.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnSerIntEstados.Name = "btnSerIntEstados"
        Me.btnSerIntEstados.SmallImage = CType(resources.GetObject("btnSerIntEstados.SmallImage"), System.Drawing.Image)
        Me.btnSerIntEstados.Text = "Estados"
        '
        'tabPuntoVenta
        '
        Me.tabPuntoVenta.Name = "tabPuntoVenta"
        Me.tabPuntoVenta.Panels.Add(Me.pnlPunGeneral)
        Me.tabPuntoVenta.Panels.Add(Me.pnlPunRestaurante)
        Me.tabPuntoVenta.Text = "Punto de venta"
        '
        'pnlPunGeneral
        '
        Me.pnlPunGeneral.Items.Add(Me.btnPunGenVentas)
        Me.pnlPunGeneral.Items.Add(Me.btnPunGenMovimientos)
        Me.pnlPunGeneral.Items.Add(Me.btnPunGenHerramientas)
        Me.pnlPunGeneral.Items.Add(Me.btnPunGenReportes)
        Me.pnlPunGeneral.Name = "pnlPunGeneral"
        Me.pnlPunGeneral.Text = "General"
        '
        'btnPunGenVentas
        '
        Me.btnPunGenVentas.Image = CType(resources.GetObject("btnPunGenVentas.Image"), System.Drawing.Image)
        Me.btnPunGenVentas.LargeImage = CType(resources.GetObject("btnPunGenVentas.LargeImage"), System.Drawing.Image)
        Me.btnPunGenVentas.Name = "btnPunGenVentas"
        Me.btnPunGenVentas.SmallImage = CType(resources.GetObject("btnPunGenVentas.SmallImage"), System.Drawing.Image)
        Me.btnPunGenVentas.Text = "Ventas"
        '
        'btnPunGenMovimientos
        '
        Me.btnPunGenMovimientos.Image = CType(resources.GetObject("btnPunGenMovimientos.Image"), System.Drawing.Image)
        Me.btnPunGenMovimientos.LargeImage = CType(resources.GetObject("btnPunGenMovimientos.LargeImage"), System.Drawing.Image)
        Me.btnPunGenMovimientos.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnPunGenMovimientos.Name = "btnPunGenMovimientos"
        Me.btnPunGenMovimientos.SmallImage = CType(resources.GetObject("btnPunGenMovimientos.SmallImage"), System.Drawing.Image)
        Me.btnPunGenMovimientos.Text = "Movimientos de caja"
        '
        'btnPunGenHerramientas
        '
        Me.btnPunGenHerramientas.Image = CType(resources.GetObject("btnPunGenHerramientas.Image"), System.Drawing.Image)
        Me.btnPunGenHerramientas.LargeImage = CType(resources.GetObject("btnPunGenHerramientas.LargeImage"), System.Drawing.Image)
        Me.btnPunGenHerramientas.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnPunGenHerramientas.Name = "btnPunGenHerramientas"
        Me.btnPunGenHerramientas.SmallImage = CType(resources.GetObject("btnPunGenHerramientas.SmallImage"), System.Drawing.Image)
        Me.btnPunGenHerramientas.Text = "Barra de herramientas"
        '
        'btnPunGenReportes
        '
        Me.btnPunGenReportes.Image = CType(resources.GetObject("btnPunGenReportes.Image"), System.Drawing.Image)
        Me.btnPunGenReportes.LargeImage = CType(resources.GetObject("btnPunGenReportes.LargeImage"), System.Drawing.Image)
        Me.btnPunGenReportes.Name = "btnPunGenReportes"
        Me.btnPunGenReportes.SmallImage = CType(resources.GetObject("btnPunGenReportes.SmallImage"), System.Drawing.Image)
        Me.btnPunGenReportes.Text = "Reportes"
        '
        'pnlPunRestaurante
        '
        Me.pnlPunRestaurante.Items.Add(Me.btnPunResVentas)
        Me.pnlPunRestaurante.Items.Add(Me.btnPunResConfiguracion)
        Me.pnlPunRestaurante.Name = "pnlPunRestaurante"
        Me.pnlPunRestaurante.Text = "Restaurante"
        '
        'btnPunResVentas
        '
        Me.btnPunResVentas.Image = CType(resources.GetObject("btnPunResVentas.Image"), System.Drawing.Image)
        Me.btnPunResVentas.LargeImage = CType(resources.GetObject("btnPunResVentas.LargeImage"), System.Drawing.Image)
        Me.btnPunResVentas.Name = "btnPunResVentas"
        Me.btnPunResVentas.SmallImage = CType(resources.GetObject("btnPunResVentas.SmallImage"), System.Drawing.Image)
        Me.btnPunResVentas.Text = "Ventas"
        '
        'btnPunResConfiguracion
        '
        Me.btnPunResConfiguracion.Image = CType(resources.GetObject("btnPunResConfiguracion.Image"), System.Drawing.Image)
        Me.btnPunResConfiguracion.LargeImage = CType(resources.GetObject("btnPunResConfiguracion.LargeImage"), System.Drawing.Image)
        Me.btnPunResConfiguracion.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnPunResConfiguracion.Name = "btnPunResConfiguracion"
        Me.btnPunResConfiguracion.SmallImage = CType(resources.GetObject("btnPunResConfiguracion.SmallImage"), System.Drawing.Image)
        Me.btnPunResConfiguracion.Text = "Configuración"
        '
        'tabBancos
        '
        Me.tabBancos.Name = "tabBancos"
        Me.tabBancos.Panels.Add(Me.pnlBanCatalogos)
        Me.tabBancos.Panels.Add(Me.pnlBanOperacion)
        Me.tabBancos.Panels.Add(Me.pnlBanConsultas)
        Me.tabBancos.Text = "Bancos"
        '
        'pnlBanCatalogos
        '
        Me.pnlBanCatalogos.Items.Add(Me.btnBanCatCuentas)
        Me.pnlBanCatalogos.Name = "pnlBanCatalogos"
        Me.pnlBanCatalogos.Text = "Catálogos"
        '
        'btnBanCatCuentas
        '
        Me.btnBanCatCuentas.Image = CType(resources.GetObject("btnBanCatCuentas.Image"), System.Drawing.Image)
        Me.btnBanCatCuentas.LargeImage = CType(resources.GetObject("btnBanCatCuentas.LargeImage"), System.Drawing.Image)
        Me.btnBanCatCuentas.Name = "btnBanCatCuentas"
        Me.btnBanCatCuentas.SmallImage = CType(resources.GetObject("btnBanCatCuentas.SmallImage"), System.Drawing.Image)
        Me.btnBanCatCuentas.Text = "Cuentas"
        '
        'pnlBanOperacion
        '
        Me.pnlBanOperacion.Items.Add(Me.btnBanOpeDepositos)
        Me.pnlBanOperacion.Items.Add(Me.btnBanOpePagos)
        Me.pnlBanOperacion.Items.Add(Me.btnBanOpeConciliacion)
        Me.pnlBanOperacion.Name = "pnlBanOperacion"
        Me.pnlBanOperacion.Text = "Operación"
        '
        'btnBanOpeDepositos
        '
        Me.btnBanOpeDepositos.Image = CType(resources.GetObject("btnBanOpeDepositos.Image"), System.Drawing.Image)
        Me.btnBanOpeDepositos.LargeImage = CType(resources.GetObject("btnBanOpeDepositos.LargeImage"), System.Drawing.Image)
        Me.btnBanOpeDepositos.Name = "btnBanOpeDepositos"
        Me.btnBanOpeDepositos.SmallImage = CType(resources.GetObject("btnBanOpeDepositos.SmallImage"), System.Drawing.Image)
        Me.btnBanOpeDepositos.Text = "Depósitos"
        '
        'btnBanOpePagos
        '
        Me.btnBanOpePagos.Image = CType(resources.GetObject("btnBanOpePagos.Image"), System.Drawing.Image)
        Me.btnBanOpePagos.LargeImage = CType(resources.GetObject("btnBanOpePagos.LargeImage"), System.Drawing.Image)
        Me.btnBanOpePagos.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnBanOpePagos.Name = "btnBanOpePagos"
        Me.btnBanOpePagos.SmallImage = CType(resources.GetObject("btnBanOpePagos.SmallImage"), System.Drawing.Image)
        Me.btnBanOpePagos.Text = "Pagos"
        '
        'btnBanOpeConciliacion
        '
        Me.btnBanOpeConciliacion.Image = CType(resources.GetObject("btnBanOpeConciliacion.Image"), System.Drawing.Image)
        Me.btnBanOpeConciliacion.LargeImage = CType(resources.GetObject("btnBanOpeConciliacion.LargeImage"), System.Drawing.Image)
        Me.btnBanOpeConciliacion.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnBanOpeConciliacion.Name = "btnBanOpeConciliacion"
        Me.btnBanOpeConciliacion.SmallImage = CType(resources.GetObject("btnBanOpeConciliacion.SmallImage"), System.Drawing.Image)
        Me.btnBanOpeConciliacion.Text = "Conciliación"
        '
        'pnlBanConsultas
        '
        Me.pnlBanConsultas.Items.Add(Me.btnBanConReportes)
        Me.pnlBanConsultas.Name = "pnlBanConsultas"
        Me.pnlBanConsultas.Text = "Consulta"
        '
        'btnBanConReportes
        '
        Me.btnBanConReportes.Image = CType(resources.GetObject("btnBanConReportes.Image"), System.Drawing.Image)
        Me.btnBanConReportes.LargeImage = CType(resources.GetObject("btnBanConReportes.LargeImage"), System.Drawing.Image)
        Me.btnBanConReportes.Name = "btnBanConReportes"
        Me.btnBanConReportes.SmallImage = CType(resources.GetObject("btnBanConReportes.SmallImage"), System.Drawing.Image)
        Me.btnBanConReportes.Text = "Reportes"
        '
        'tabNomina
        '
        Me.tabNomina.Name = "tabNomina"
        Me.tabNomina.Panels.Add(Me.pnlNomCatalogos)
        Me.tabNomina.Panels.Add(Me.pnlNomOpercion)
        Me.tabNomina.Panels.Add(Me.pnlNomConsultas)
        Me.tabNomina.Text = "Nómina"
        '
        'pnlNomCatalogos
        '
        Me.pnlNomCatalogos.Items.Add(Me.btnNomCatTrabajadores)
        Me.pnlNomCatalogos.Name = "pnlNomCatalogos"
        Me.pnlNomCatalogos.Text = "Catálogos"
        '
        'btnNomCatTrabajadores
        '
        Me.btnNomCatTrabajadores.Image = CType(resources.GetObject("btnNomCatTrabajadores.Image"), System.Drawing.Image)
        Me.btnNomCatTrabajadores.LargeImage = CType(resources.GetObject("btnNomCatTrabajadores.LargeImage"), System.Drawing.Image)
        Me.btnNomCatTrabajadores.Name = "btnNomCatTrabajadores"
        Me.btnNomCatTrabajadores.SmallImage = CType(resources.GetObject("btnNomCatTrabajadores.SmallImage"), System.Drawing.Image)
        Me.btnNomCatTrabajadores.Text = "Trabajadores"
        '
        'pnlNomOpercion
        '
        Me.pnlNomOpercion.Items.Add(Me.btnNomOpeNomina)
        Me.pnlNomOpercion.Name = "pnlNomOpercion"
        Me.pnlNomOpercion.Text = "Operación"
        '
        'btnNomOpeNomina
        '
        Me.btnNomOpeNomina.Image = CType(resources.GetObject("btnNomOpeNomina.Image"), System.Drawing.Image)
        Me.btnNomOpeNomina.LargeImage = CType(resources.GetObject("btnNomOpeNomina.LargeImage"), System.Drawing.Image)
        Me.btnNomOpeNomina.Name = "btnNomOpeNomina"
        Me.btnNomOpeNomina.SmallImage = CType(resources.GetObject("btnNomOpeNomina.SmallImage"), System.Drawing.Image)
        Me.btnNomOpeNomina.Text = "Nómina"
        '
        'pnlNomConsultas
        '
        Me.pnlNomConsultas.Items.Add(Me.btnNomConReportes)
        Me.pnlNomConsultas.Name = "pnlNomConsultas"
        Me.pnlNomConsultas.Text = "Consultas"
        '
        'btnNomConReportes
        '
        Me.btnNomConReportes.Image = CType(resources.GetObject("btnNomConReportes.Image"), System.Drawing.Image)
        Me.btnNomConReportes.LargeImage = CType(resources.GetObject("btnNomConReportes.LargeImage"), System.Drawing.Image)
        Me.btnNomConReportes.Name = "btnNomConReportes"
        Me.btnNomConReportes.SmallImage = CType(resources.GetObject("btnNomConReportes.SmallImage"), System.Drawing.Image)
        Me.btnNomConReportes.Text = "Reportes"
        '
        'tabGastos
        '
        Me.tabGastos.Name = "tabGastos"
        Me.tabGastos.Panels.Add(Me.pnlGasCatalogos)
        Me.tabGastos.Panels.Add(Me.pnlGasOperacion)
        Me.tabGastos.Panels.Add(Me.pnlGasConsultas)
        Me.tabGastos.Text = "Gastos"
        '
        'pnlGasCatalogos
        '
        Me.pnlGasCatalogos.Items.Add(Me.btnGasCatClasificaciones)
        Me.pnlGasCatalogos.Items.Add(Me.btnGasCatEmpleados)
        Me.pnlGasCatalogos.Name = "pnlGasCatalogos"
        Me.pnlGasCatalogos.Text = "Catálogos"
        '
        'btnGasCatClasificaciones
        '
        Me.btnGasCatClasificaciones.Image = CType(resources.GetObject("btnGasCatClasificaciones.Image"), System.Drawing.Image)
        Me.btnGasCatClasificaciones.LargeImage = CType(resources.GetObject("btnGasCatClasificaciones.LargeImage"), System.Drawing.Image)
        Me.btnGasCatClasificaciones.Name = "btnGasCatClasificaciones"
        Me.btnGasCatClasificaciones.SmallImage = CType(resources.GetObject("btnGasCatClasificaciones.SmallImage"), System.Drawing.Image)
        Me.btnGasCatClasificaciones.Text = "Clasificaciones"
        '
        'btnGasCatEmpleados
        '
        Me.btnGasCatEmpleados.Image = CType(resources.GetObject("btnGasCatEmpleados.Image"), System.Drawing.Image)
        Me.btnGasCatEmpleados.LargeImage = CType(resources.GetObject("btnGasCatEmpleados.LargeImage"), System.Drawing.Image)
        Me.btnGasCatEmpleados.Name = "btnGasCatEmpleados"
        Me.btnGasCatEmpleados.SmallImage = CType(resources.GetObject("btnGasCatEmpleados.SmallImage"), System.Drawing.Image)
        Me.btnGasCatEmpleados.Text = "Empleados"
        '
        'pnlGasOperacion
        '
        Me.pnlGasOperacion.Items.Add(Me.btnGasOpeGastos)
        Me.pnlGasOperacion.Items.Add(Me.btnGasOpeMovimientos)
        Me.pnlGasOperacion.Items.Add(Me.btnGasOpeProgramar)
        Me.pnlGasOperacion.Items.Add(Me.btnGasOpeAlertas)
        Me.pnlGasOperacion.Name = "pnlGasOperacion"
        Me.pnlGasOperacion.Text = "Operación"
        '
        'btnGasOpeGastos
        '
        Me.btnGasOpeGastos.Image = CType(resources.GetObject("btnGasOpeGastos.Image"), System.Drawing.Image)
        Me.btnGasOpeGastos.LargeImage = CType(resources.GetObject("btnGasOpeGastos.LargeImage"), System.Drawing.Image)
        Me.btnGasOpeGastos.Name = "btnGasOpeGastos"
        Me.btnGasOpeGastos.SmallImage = CType(resources.GetObject("btnGasOpeGastos.SmallImage"), System.Drawing.Image)
        Me.btnGasOpeGastos.Text = "Gastos"
        '
        'btnGasOpeMovimientos
        '
        Me.btnGasOpeMovimientos.Image = CType(resources.GetObject("btnGasOpeMovimientos.Image"), System.Drawing.Image)
        Me.btnGasOpeMovimientos.LargeImage = CType(resources.GetObject("btnGasOpeMovimientos.LargeImage"), System.Drawing.Image)
        Me.btnGasOpeMovimientos.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnGasOpeMovimientos.Name = "btnGasOpeMovimientos"
        Me.btnGasOpeMovimientos.SmallImage = CType(resources.GetObject("btnGasOpeMovimientos.SmallImage"), System.Drawing.Image)
        Me.btnGasOpeMovimientos.Text = "Movimientos"
        '
        'btnGasOpeProgramar
        '
        Me.btnGasOpeProgramar.Image = CType(resources.GetObject("btnGasOpeProgramar.Image"), System.Drawing.Image)
        Me.btnGasOpeProgramar.LargeImage = CType(resources.GetObject("btnGasOpeProgramar.LargeImage"), System.Drawing.Image)
        Me.btnGasOpeProgramar.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnGasOpeProgramar.Name = "btnGasOpeProgramar"
        Me.btnGasOpeProgramar.SmallImage = CType(resources.GetObject("btnGasOpeProgramar.SmallImage"), System.Drawing.Image)
        Me.btnGasOpeProgramar.Text = "Programar"
        '
        'btnGasOpeAlertas
        '
        Me.btnGasOpeAlertas.Image = CType(resources.GetObject("btnGasOpeAlertas.Image"), System.Drawing.Image)
        Me.btnGasOpeAlertas.LargeImage = CType(resources.GetObject("btnGasOpeAlertas.LargeImage"), System.Drawing.Image)
        Me.btnGasOpeAlertas.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnGasOpeAlertas.Name = "btnGasOpeAlertas"
        Me.btnGasOpeAlertas.SmallImage = CType(resources.GetObject("btnGasOpeAlertas.SmallImage"), System.Drawing.Image)
        Me.btnGasOpeAlertas.Text = "Alertas"
        '
        'pnlGasConsultas
        '
        Me.pnlGasConsultas.Items.Add(Me.btnGasConReportes)
        Me.pnlGasConsultas.Name = "pnlGasConsultas"
        Me.pnlGasConsultas.Text = "Consultas"
        '
        'btnGasConReportes
        '
        Me.btnGasConReportes.Image = CType(resources.GetObject("btnGasConReportes.Image"), System.Drawing.Image)
        Me.btnGasConReportes.LargeImage = CType(resources.GetObject("btnGasConReportes.LargeImage"), System.Drawing.Image)
        Me.btnGasConReportes.Name = "btnGasConReportes"
        Me.btnGasConReportes.SmallImage = CType(resources.GetObject("btnGasConReportes.SmallImage"), System.Drawing.Image)
        Me.btnGasConReportes.Text = "Reportes"
        '
        'tabEmpenos
        '
        Me.tabEmpenos.Name = "tabEmpenos"
        Me.tabEmpenos.Panels.Add(Me.pnlEmpCatalogos)
        Me.tabEmpenos.Panels.Add(Me.pnlEmpOperacion)
        Me.tabEmpenos.Panels.Add(Me.pnlEmpConsultas)
        Me.tabEmpenos.Panels.Add(Me.pnlEmpHerramientas)
        Me.tabEmpenos.Text = "Empeños"
        '
        'pnlEmpCatalogos
        '
        Me.pnlEmpCatalogos.Items.Add(Me.btnEmpCatIdentificaciones)
        Me.pnlEmpCatalogos.Items.Add(Me.btnEmpCatClasificaciones)
        Me.pnlEmpCatalogos.Name = "pnlEmpCatalogos"
        Me.pnlEmpCatalogos.Text = "Catálogos"
        '
        'btnEmpCatIdentificaciones
        '
        Me.btnEmpCatIdentificaciones.Image = CType(resources.GetObject("btnEmpCatIdentificaciones.Image"), System.Drawing.Image)
        Me.btnEmpCatIdentificaciones.LargeImage = CType(resources.GetObject("btnEmpCatIdentificaciones.LargeImage"), System.Drawing.Image)
        Me.btnEmpCatIdentificaciones.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnEmpCatIdentificaciones.Name = "btnEmpCatIdentificaciones"
        Me.btnEmpCatIdentificaciones.SmallImage = CType(resources.GetObject("btnEmpCatIdentificaciones.SmallImage"), System.Drawing.Image)
        Me.btnEmpCatIdentificaciones.Text = "Identificaciones"
        '
        'btnEmpCatClasificaciones
        '
        Me.btnEmpCatClasificaciones.Image = CType(resources.GetObject("btnEmpCatClasificaciones.Image"), System.Drawing.Image)
        Me.btnEmpCatClasificaciones.LargeImage = CType(resources.GetObject("btnEmpCatClasificaciones.LargeImage"), System.Drawing.Image)
        Me.btnEmpCatClasificaciones.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnEmpCatClasificaciones.Name = "btnEmpCatClasificaciones"
        Me.btnEmpCatClasificaciones.SmallImage = CType(resources.GetObject("btnEmpCatClasificaciones.SmallImage"), System.Drawing.Image)
        Me.btnEmpCatClasificaciones.Text = "Clasificaciones"
        '
        'pnlEmpOperacion
        '
        Me.pnlEmpOperacion.Items.Add(Me.btnEmpOpeEmpenos)
        Me.pnlEmpOperacion.Items.Add(Me.btnEmpOpePagos)
        Me.pnlEmpOperacion.Items.Add(Me.btnEmpOpeCompras)
        Me.pnlEmpOperacion.Items.Add(Me.btnEmpOpeAdjudicaciones)
        Me.pnlEmpOperacion.Items.Add(Me.btnEmpOpeCortes)
        Me.pnlEmpOperacion.Name = "pnlEmpOperacion"
        Me.pnlEmpOperacion.Text = "Operación"
        '
        'btnEmpOpeEmpenos
        '
        Me.btnEmpOpeEmpenos.Image = CType(resources.GetObject("btnEmpOpeEmpenos.Image"), System.Drawing.Image)
        Me.btnEmpOpeEmpenos.LargeImage = CType(resources.GetObject("btnEmpOpeEmpenos.LargeImage"), System.Drawing.Image)
        Me.btnEmpOpeEmpenos.Name = "btnEmpOpeEmpenos"
        Me.btnEmpOpeEmpenos.SmallImage = CType(resources.GetObject("btnEmpOpeEmpenos.SmallImage"), System.Drawing.Image)
        Me.btnEmpOpeEmpenos.Text = "Empeños"
        '
        'btnEmpOpePagos
        '
        Me.btnEmpOpePagos.Image = CType(resources.GetObject("btnEmpOpePagos.Image"), System.Drawing.Image)
        Me.btnEmpOpePagos.LargeImage = CType(resources.GetObject("btnEmpOpePagos.LargeImage"), System.Drawing.Image)
        Me.btnEmpOpePagos.Name = "btnEmpOpePagos"
        Me.btnEmpOpePagos.SmallImage = CType(resources.GetObject("btnEmpOpePagos.SmallImage"), System.Drawing.Image)
        Me.btnEmpOpePagos.Text = "Pagos"
        '
        'btnEmpOpeCompras
        '
        Me.btnEmpOpeCompras.Image = CType(resources.GetObject("btnEmpOpeCompras.Image"), System.Drawing.Image)
        Me.btnEmpOpeCompras.LargeImage = CType(resources.GetObject("btnEmpOpeCompras.LargeImage"), System.Drawing.Image)
        Me.btnEmpOpeCompras.Name = "btnEmpOpeCompras"
        Me.btnEmpOpeCompras.SmallImage = CType(resources.GetObject("btnEmpOpeCompras.SmallImage"), System.Drawing.Image)
        Me.btnEmpOpeCompras.Text = "Compras"
        '
        'btnEmpOpeAdjudicaciones
        '
        Me.btnEmpOpeAdjudicaciones.Image = CType(resources.GetObject("btnEmpOpeAdjudicaciones.Image"), System.Drawing.Image)
        Me.btnEmpOpeAdjudicaciones.LargeImage = CType(resources.GetObject("btnEmpOpeAdjudicaciones.LargeImage"), System.Drawing.Image)
        Me.btnEmpOpeAdjudicaciones.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnEmpOpeAdjudicaciones.Name = "btnEmpOpeAdjudicaciones"
        Me.btnEmpOpeAdjudicaciones.SmallImage = CType(resources.GetObject("btnEmpOpeAdjudicaciones.SmallImage"), System.Drawing.Image)
        Me.btnEmpOpeAdjudicaciones.Text = "Adjudicaciones"
        '
        'btnEmpOpeCortes
        '
        Me.btnEmpOpeCortes.Image = CType(resources.GetObject("btnEmpOpeCortes.Image"), System.Drawing.Image)
        Me.btnEmpOpeCortes.LargeImage = CType(resources.GetObject("btnEmpOpeCortes.LargeImage"), System.Drawing.Image)
        Me.btnEmpOpeCortes.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnEmpOpeCortes.Name = "btnEmpOpeCortes"
        Me.btnEmpOpeCortes.SmallImage = CType(resources.GetObject("btnEmpOpeCortes.SmallImage"), System.Drawing.Image)
        Me.btnEmpOpeCortes.Text = "Cortes"
        '
        'pnlEmpConsultas
        '
        Me.pnlEmpConsultas.Items.Add(Me.btnEmpConReportes)
        Me.pnlEmpConsultas.Items.Add(Me.btnEmpConConsultas)
        Me.pnlEmpConsultas.Name = "pnlEmpConsultas"
        Me.pnlEmpConsultas.Text = "Consultas"
        '
        'btnEmpConReportes
        '
        Me.btnEmpConReportes.Image = CType(resources.GetObject("btnEmpConReportes.Image"), System.Drawing.Image)
        Me.btnEmpConReportes.LargeImage = CType(resources.GetObject("btnEmpConReportes.LargeImage"), System.Drawing.Image)
        Me.btnEmpConReportes.Name = "btnEmpConReportes"
        Me.btnEmpConReportes.SmallImage = CType(resources.GetObject("btnEmpConReportes.SmallImage"), System.Drawing.Image)
        Me.btnEmpConReportes.Text = "Reportes"
        '
        'btnEmpConConsultas
        '
        Me.btnEmpConConsultas.Image = CType(resources.GetObject("btnEmpConConsultas.Image"), System.Drawing.Image)
        Me.btnEmpConConsultas.LargeImage = CType(resources.GetObject("btnEmpConConsultas.LargeImage"), System.Drawing.Image)
        Me.btnEmpConConsultas.Name = "btnEmpConConsultas"
        Me.btnEmpConConsultas.SmallImage = CType(resources.GetObject("btnEmpConConsultas.SmallImage"), System.Drawing.Image)
        Me.btnEmpConConsultas.Text = "Consultas"
        '
        'pnlEmpHerramientas
        '
        Me.pnlEmpHerramientas.Items.Add(Me.btnEmpherConfiguracion)
        Me.pnlEmpHerramientas.Name = "pnlEmpHerramientas"
        Me.pnlEmpHerramientas.Text = "Herramientas"
        '
        'btnEmpherConfiguracion
        '
        Me.btnEmpherConfiguracion.Image = CType(resources.GetObject("btnEmpherConfiguracion.Image"), System.Drawing.Image)
        Me.btnEmpherConfiguracion.LargeImage = CType(resources.GetObject("btnEmpherConfiguracion.LargeImage"), System.Drawing.Image)
        Me.btnEmpherConfiguracion.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnEmpherConfiguracion.Name = "btnEmpherConfiguracion"
        Me.btnEmpherConfiguracion.SmallImage = CType(resources.GetObject("btnEmpherConfiguracion.SmallImage"), System.Drawing.Image)
        Me.btnEmpherConfiguracion.Text = "Configuración"
        '
        'tabContabilidad
        '
        Me.tabContabilidad.Name = "tabContabilidad"
        Me.tabContabilidad.Panels.Add(Me.pnlConCatalogos)
        Me.tabContabilidad.Panels.Add(Me.pnlConOperación)
        Me.tabContabilidad.Panels.Add(Me.pnlConConsultas)
        Me.tabContabilidad.Text = "Contabilidad"
        '
        'pnlConCatalogos
        '
        Me.pnlConCatalogos.Items.Add(Me.btnConCatClasifPolizas)
        Me.pnlConCatalogos.Items.Add(Me.btnConCatCuentas)
        Me.pnlConCatalogos.Items.Add(Me.btnConCatConceptos)
        Me.pnlConCatalogos.Items.Add(Me.btnConCatMascaras)
        Me.pnlConCatalogos.Name = "pnlConCatalogos"
        Me.pnlConCatalogos.Text = "Catálogos"
        '
        'btnConCatClasifPolizas
        '
        Me.btnConCatClasifPolizas.Image = CType(resources.GetObject("btnConCatClasifPolizas.Image"), System.Drawing.Image)
        Me.btnConCatClasifPolizas.LargeImage = CType(resources.GetObject("btnConCatClasifPolizas.LargeImage"), System.Drawing.Image)
        Me.btnConCatClasifPolizas.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnConCatClasifPolizas.Name = "btnConCatClasifPolizas"
        Me.btnConCatClasifPolizas.SmallImage = CType(resources.GetObject("btnConCatClasifPolizas.SmallImage"), System.Drawing.Image)
        Me.btnConCatClasifPolizas.Text = "Clasif. pólizas"
        '
        'btnConCatCuentas
        '
        Me.btnConCatCuentas.Image = CType(resources.GetObject("btnConCatCuentas.Image"), System.Drawing.Image)
        Me.btnConCatCuentas.LargeImage = CType(resources.GetObject("btnConCatCuentas.LargeImage"), System.Drawing.Image)
        Me.btnConCatCuentas.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnConCatCuentas.Name = "btnConCatCuentas"
        Me.btnConCatCuentas.SmallImage = CType(resources.GetObject("btnConCatCuentas.SmallImage"), System.Drawing.Image)
        Me.btnConCatCuentas.Text = "Cuentas"
        '
        'btnConCatConceptos
        '
        Me.btnConCatConceptos.Image = CType(resources.GetObject("btnConCatConceptos.Image"), System.Drawing.Image)
        Me.btnConCatConceptos.LargeImage = CType(resources.GetObject("btnConCatConceptos.LargeImage"), System.Drawing.Image)
        Me.btnConCatConceptos.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnConCatConceptos.Name = "btnConCatConceptos"
        Me.btnConCatConceptos.SmallImage = CType(resources.GetObject("btnConCatConceptos.SmallImage"), System.Drawing.Image)
        Me.btnConCatConceptos.Text = "Conceptos"
        '
        'btnConCatMascaras
        '
        Me.btnConCatMascaras.Image = CType(resources.GetObject("btnConCatMascaras.Image"), System.Drawing.Image)
        Me.btnConCatMascaras.LargeImage = CType(resources.GetObject("btnConCatMascaras.LargeImage"), System.Drawing.Image)
        Me.btnConCatMascaras.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnConCatMascaras.Name = "btnConCatMascaras"
        Me.btnConCatMascaras.SmallImage = CType(resources.GetObject("btnConCatMascaras.SmallImage"), System.Drawing.Image)
        Me.btnConCatMascaras.Text = "Máscaras"
        '
        'pnlConOperación
        '
        Me.pnlConOperación.Items.Add(Me.btnConOpePolizas)
        Me.pnlConOperación.Items.Add(Me.btnConOpeSaldos)
        Me.pnlConOperación.Items.Add(Me.btnConOpeConciliarDiot)
        Me.pnlConOperación.Items.Add(Me.btnConOpeGenerarPolizas)
        Me.pnlConOperación.Items.Add(Me.btnConOpeConfiguracion)
        Me.pnlConOperación.Name = "pnlConOperación"
        Me.pnlConOperación.Text = "Operación"
        '
        'btnConOpePolizas
        '
        Me.btnConOpePolizas.Image = CType(resources.GetObject("btnConOpePolizas.Image"), System.Drawing.Image)
        Me.btnConOpePolizas.LargeImage = CType(resources.GetObject("btnConOpePolizas.LargeImage"), System.Drawing.Image)
        Me.btnConOpePolizas.Name = "btnConOpePolizas"
        Me.btnConOpePolizas.SmallImage = CType(resources.GetObject("btnConOpePolizas.SmallImage"), System.Drawing.Image)
        Me.btnConOpePolizas.Text = "Pólizas"
        '
        'btnConOpeSaldos
        '
        Me.btnConOpeSaldos.Image = CType(resources.GetObject("btnConOpeSaldos.Image"), System.Drawing.Image)
        Me.btnConOpeSaldos.LargeImage = CType(resources.GetObject("btnConOpeSaldos.LargeImage"), System.Drawing.Image)
        Me.btnConOpeSaldos.Name = "btnConOpeSaldos"
        Me.btnConOpeSaldos.SmallImage = CType(resources.GetObject("btnConOpeSaldos.SmallImage"), System.Drawing.Image)
        Me.btnConOpeSaldos.Text = "Saldos"
        '
        'btnConOpeConciliarDiot
        '
        Me.btnConOpeConciliarDiot.Image = CType(resources.GetObject("btnConOpeConciliarDiot.Image"), System.Drawing.Image)
        Me.btnConOpeConciliarDiot.LargeImage = CType(resources.GetObject("btnConOpeConciliarDiot.LargeImage"), System.Drawing.Image)
        Me.btnConOpeConciliarDiot.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnConOpeConciliarDiot.Name = "btnConOpeConciliarDiot"
        Me.btnConOpeConciliarDiot.SmallImage = CType(resources.GetObject("btnConOpeConciliarDiot.SmallImage"), System.Drawing.Image)
        Me.btnConOpeConciliarDiot.Text = "Conciliar DIOT"
        '
        'btnConOpeGenerarPolizas
        '
        Me.btnConOpeGenerarPolizas.Image = CType(resources.GetObject("btnConOpeGenerarPolizas.Image"), System.Drawing.Image)
        Me.btnConOpeGenerarPolizas.LargeImage = CType(resources.GetObject("btnConOpeGenerarPolizas.LargeImage"), System.Drawing.Image)
        Me.btnConOpeGenerarPolizas.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnConOpeGenerarPolizas.Name = "btnConOpeGenerarPolizas"
        Me.btnConOpeGenerarPolizas.SmallImage = CType(resources.GetObject("btnConOpeGenerarPolizas.SmallImage"), System.Drawing.Image)
        Me.btnConOpeGenerarPolizas.Text = "Generar pólizas"
        '
        'btnConOpeConfiguracion
        '
        Me.btnConOpeConfiguracion.Image = CType(resources.GetObject("btnConOpeConfiguracion.Image"), System.Drawing.Image)
        Me.btnConOpeConfiguracion.LargeImage = CType(resources.GetObject("btnConOpeConfiguracion.LargeImage"), System.Drawing.Image)
        Me.btnConOpeConfiguracion.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnConOpeConfiguracion.Name = "btnConOpeConfiguracion"
        Me.btnConOpeConfiguracion.SmallImage = CType(resources.GetObject("btnConOpeConfiguracion.SmallImage"), System.Drawing.Image)
        Me.btnConOpeConfiguracion.Text = "Configuración"
        '
        'pnlConConsultas
        '
        Me.pnlConConsultas.Items.Add(Me.btnConConReportes)
        Me.pnlConConsultas.Name = "pnlConConsultas"
        Me.pnlConConsultas.Text = "Consultas"
        '
        'btnConConReportes
        '
        Me.btnConConReportes.Image = CType(resources.GetObject("btnConConReportes.Image"), System.Drawing.Image)
        Me.btnConConReportes.LargeImage = CType(resources.GetObject("btnConConReportes.LargeImage"), System.Drawing.Image)
        Me.btnConConReportes.Name = "btnConConReportes"
        Me.btnConConReportes.SmallImage = CType(resources.GetObject("btnConConReportes.SmallImage"), System.Drawing.Image)
        Me.btnConConReportes.Text = "Reportes"
        '
        'tabHerramientas
        '
        Me.tabHerramientas.Name = "tabHerramientas"
        Me.tabHerramientas.Panels.Add(Me.pnlHerConfiguracion)
        Me.tabHerramientas.Panels.Add(Me.pnlHerHerramientas)
        Me.tabHerramientas.Panels.Add(Me.pnHerBaseDatos)
        Me.tabHerramientas.Text = "Herramientas"
        '
        'pnlHerConfiguracion
        '
        Me.pnlHerConfiguracion.Items.Add(Me.btnHerConOpciones)
        Me.pnlHerConfiguracion.Items.Add(Me.btnHerConConfigCorreo)
        Me.pnlHerConfiguracion.Items.Add(Me.btnHerConLicencias)
        Me.pnlHerConfiguracion.Items.Add(Me.btnHerConDistribuidores)
        Me.pnlHerConfiguracion.Items.Add(Me.btnHerConImportar)
        Me.pnlHerConfiguracion.Name = "pnlHerConfiguracion"
        Me.pnlHerConfiguracion.Text = "Configuración"
        '
        'btnHerConOpciones
        '
        Me.btnHerConOpciones.Image = CType(resources.GetObject("btnHerConOpciones.Image"), System.Drawing.Image)
        Me.btnHerConOpciones.LargeImage = CType(resources.GetObject("btnHerConOpciones.LargeImage"), System.Drawing.Image)
        Me.btnHerConOpciones.Name = "btnHerConOpciones"
        Me.btnHerConOpciones.SmallImage = CType(resources.GetObject("btnHerConOpciones.SmallImage"), System.Drawing.Image)
        Me.btnHerConOpciones.Text = "Opciones"
        '
        'btnHerConConfigCorreo
        '
        Me.btnHerConConfigCorreo.Image = CType(resources.GetObject("btnHerConConfigCorreo.Image"), System.Drawing.Image)
        Me.btnHerConConfigCorreo.LargeImage = CType(resources.GetObject("btnHerConConfigCorreo.LargeImage"), System.Drawing.Image)
        Me.btnHerConConfigCorreo.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerConConfigCorreo.Name = "btnHerConConfigCorreo"
        Me.btnHerConConfigCorreo.SmallImage = CType(resources.GetObject("btnHerConConfigCorreo.SmallImage"), System.Drawing.Image)
        Me.btnHerConConfigCorreo.Text = "Config. correo"
        '
        'btnHerConLicencias
        '
        Me.btnHerConLicencias.Image = CType(resources.GetObject("btnHerConLicencias.Image"), System.Drawing.Image)
        Me.btnHerConLicencias.LargeImage = CType(resources.GetObject("btnHerConLicencias.LargeImage"), System.Drawing.Image)
        Me.btnHerConLicencias.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerConLicencias.Name = "btnHerConLicencias"
        Me.btnHerConLicencias.SmallImage = CType(resources.GetObject("btnHerConLicencias.SmallImage"), System.Drawing.Image)
        Me.btnHerConLicencias.Text = "Licencias"
        '
        'btnHerConDistribuidores
        '
        Me.btnHerConDistribuidores.Image = CType(resources.GetObject("btnHerConDistribuidores.Image"), System.Drawing.Image)
        Me.btnHerConDistribuidores.LargeImage = CType(resources.GetObject("btnHerConDistribuidores.LargeImage"), System.Drawing.Image)
        Me.btnHerConDistribuidores.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerConDistribuidores.Name = "btnHerConDistribuidores"
        Me.btnHerConDistribuidores.SmallImage = CType(resources.GetObject("btnHerConDistribuidores.SmallImage"), System.Drawing.Image)
        Me.btnHerConDistribuidores.Text = "Distribuidores"
        '
        'btnHerConImportar
        '
        Me.btnHerConImportar.Image = CType(resources.GetObject("btnHerConImportar.Image"), System.Drawing.Image)
        Me.btnHerConImportar.LargeImage = CType(resources.GetObject("btnHerConImportar.LargeImage"), System.Drawing.Image)
        Me.btnHerConImportar.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerConImportar.Name = "btnHerConImportar"
        Me.btnHerConImportar.SmallImage = CType(resources.GetObject("btnHerConImportar.SmallImage"), System.Drawing.Image)
        Me.btnHerConImportar.Text = "Importar"
        '
        'pnlHerHerramientas
        '
        Me.pnlHerHerramientas.Items.Add(Me.btnHerHerContadorTimbres)
        Me.pnlHerHerramientas.Items.Add(Me.btnHerHerActivarconector)
        Me.pnlHerHerramientas.Items.Add(Me.btnHerHerMovimientosUsuario)
        Me.pnlHerHerramientas.Items.Add(Me.btnHerHerDisenoDocumentos)
        Me.pnlHerHerramientas.Items.Add(Me.btnHerHerCambioPrecios)
        Me.pnlHerHerramientas.Items.Add(Me.btnHerHerModifiarInv)
        Me.pnlHerHerramientas.Name = "pnlHerHerramientas"
        Me.pnlHerHerramientas.Text = "Herramientas"
        '
        'btnHerHerContadorTimbres
        '
        Me.btnHerHerContadorTimbres.Image = CType(resources.GetObject("btnHerHerContadorTimbres.Image"), System.Drawing.Image)
        Me.btnHerHerContadorTimbres.LargeImage = CType(resources.GetObject("btnHerHerContadorTimbres.LargeImage"), System.Drawing.Image)
        Me.btnHerHerContadorTimbres.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerHerContadorTimbres.Name = "btnHerHerContadorTimbres"
        Me.btnHerHerContadorTimbres.SmallImage = CType(resources.GetObject("btnHerHerContadorTimbres.SmallImage"), System.Drawing.Image)
        Me.btnHerHerContadorTimbres.Text = "Contador timbres"
        '
        'btnHerHerActivarconector
        '
        Me.btnHerHerActivarconector.CheckOnClick = True
        Me.btnHerHerActivarconector.Image = CType(resources.GetObject("btnHerHerActivarconector.Image"), System.Drawing.Image)
        Me.btnHerHerActivarconector.LargeImage = CType(resources.GetObject("btnHerHerActivarconector.LargeImage"), System.Drawing.Image)
        Me.btnHerHerActivarconector.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerHerActivarconector.Name = "btnHerHerActivarconector"
        Me.btnHerHerActivarconector.SmallImage = CType(resources.GetObject("btnHerHerActivarconector.SmallImage"), System.Drawing.Image)
        Me.btnHerHerActivarconector.Text = "Activar conector"
        '
        'btnHerHerMovimientosUsuario
        '
        Me.btnHerHerMovimientosUsuario.Image = CType(resources.GetObject("btnHerHerMovimientosUsuario.Image"), System.Drawing.Image)
        Me.btnHerHerMovimientosUsuario.LargeImage = CType(resources.GetObject("btnHerHerMovimientosUsuario.LargeImage"), System.Drawing.Image)
        Me.btnHerHerMovimientosUsuario.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerHerMovimientosUsuario.Name = "btnHerHerMovimientosUsuario"
        Me.btnHerHerMovimientosUsuario.SmallImage = CType(resources.GetObject("btnHerHerMovimientosUsuario.SmallImage"), System.Drawing.Image)
        Me.btnHerHerMovimientosUsuario.Text = "Movimientos usuario"
        '
        'btnHerHerDisenoDocumentos
        '
        Me.btnHerHerDisenoDocumentos.Image = CType(resources.GetObject("btnHerHerDisenoDocumentos.Image"), System.Drawing.Image)
        Me.btnHerHerDisenoDocumentos.LargeImage = CType(resources.GetObject("btnHerHerDisenoDocumentos.LargeImage"), System.Drawing.Image)
        Me.btnHerHerDisenoDocumentos.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerHerDisenoDocumentos.Name = "btnHerHerDisenoDocumentos"
        Me.btnHerHerDisenoDocumentos.SmallImage = CType(resources.GetObject("btnHerHerDisenoDocumentos.SmallImage"), System.Drawing.Image)
        Me.btnHerHerDisenoDocumentos.Text = "Diseño de documentos"
        '
        'btnHerHerCambioPrecios
        '
        Me.btnHerHerCambioPrecios.Image = CType(resources.GetObject("btnHerHerCambioPrecios.Image"), System.Drawing.Image)
        Me.btnHerHerCambioPrecios.LargeImage = CType(resources.GetObject("btnHerHerCambioPrecios.LargeImage"), System.Drawing.Image)
        Me.btnHerHerCambioPrecios.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerHerCambioPrecios.Name = "btnHerHerCambioPrecios"
        Me.btnHerHerCambioPrecios.SmallImage = CType(resources.GetObject("btnHerHerCambioPrecios.SmallImage"), System.Drawing.Image)
        Me.btnHerHerCambioPrecios.Text = "Cambio de precios"
        '
        'btnHerHerModifiarInv
        '
        Me.btnHerHerModifiarInv.Image = CType(resources.GetObject("btnHerHerModifiarInv.Image"), System.Drawing.Image)
        Me.btnHerHerModifiarInv.LargeImage = CType(resources.GetObject("btnHerHerModifiarInv.LargeImage"), System.Drawing.Image)
        Me.btnHerHerModifiarInv.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerHerModifiarInv.Name = "btnHerHerModifiarInv"
        Me.btnHerHerModifiarInv.SmallImage = CType(resources.GetObject("btnHerHerModifiarInv.SmallImage"), System.Drawing.Image)
        Me.btnHerHerModifiarInv.Text = "Modificar Inventario"
        '
        'pnHerBaseDatos
        '
        Me.pnHerBaseDatos.Items.Add(Me.btnHerBasRespaldar)
        Me.pnHerBaseDatos.Items.Add(Me.btnHerBasRestaurar)
        Me.pnHerBaseDatos.Name = "pnHerBaseDatos"
        Me.pnHerBaseDatos.Text = "Base de datos"
        '
        'btnHerBasRespaldar
        '
        Me.btnHerBasRespaldar.Image = CType(resources.GetObject("btnHerBasRespaldar.Image"), System.Drawing.Image)
        Me.btnHerBasRespaldar.LargeImage = CType(resources.GetObject("btnHerBasRespaldar.LargeImage"), System.Drawing.Image)
        Me.btnHerBasRespaldar.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerBasRespaldar.Name = "btnHerBasRespaldar"
        Me.btnHerBasRespaldar.SmallImage = CType(resources.GetObject("btnHerBasRespaldar.SmallImage"), System.Drawing.Image)
        Me.btnHerBasRespaldar.Text = "Respaldar"
        '
        'btnHerBasRestaurar
        '
        Me.btnHerBasRestaurar.Image = CType(resources.GetObject("btnHerBasRestaurar.Image"), System.Drawing.Image)
        Me.btnHerBasRestaurar.LargeImage = CType(resources.GetObject("btnHerBasRestaurar.LargeImage"), System.Drawing.Image)
        Me.btnHerBasRestaurar.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnHerBasRestaurar.Name = "btnHerBasRestaurar"
        Me.btnHerBasRestaurar.SmallImage = CType(resources.GetObject("btnHerBasRestaurar.SmallImage"), System.Drawing.Image)
        Me.btnHerBasRestaurar.Text = "Restaurar"
        '
        'RibbonButton49
        '
        Me.RibbonButton49.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.RibbonButton49.Image = CType(resources.GetObject("RibbonButton49.Image"), System.Drawing.Image)
        Me.RibbonButton49.LargeImage = CType(resources.GetObject("RibbonButton49.LargeImage"), System.Drawing.Image)
        Me.RibbonButton49.Name = "RibbonButton49"
        Me.RibbonButton49.SmallImage = CType(resources.GetObject("RibbonButton49.SmallImage"), System.Drawing.Image)
        Me.RibbonButton49.Text = "Notas de cargo"
        '
        'RibbonButton48
        '
        Me.RibbonButton48.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.RibbonButton48.Image = CType(resources.GetObject("RibbonButton48.Image"), System.Drawing.Image)
        Me.RibbonButton48.LargeImage = CType(resources.GetObject("RibbonButton48.LargeImage"), System.Drawing.Image)
        Me.RibbonButton48.Name = "RibbonButton48"
        Me.RibbonButton48.SmallImage = CType(resources.GetObject("RibbonButton48.SmallImage"), System.Drawing.Image)
        Me.RibbonButton48.Text = "Notas de crédito"
        '
        'RibbonButton47
        '
        Me.RibbonButton47.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.RibbonButton47.Image = CType(resources.GetObject("RibbonButton47.Image"), System.Drawing.Image)
        Me.RibbonButton47.LargeImage = CType(resources.GetObject("RibbonButton47.LargeImage"), System.Drawing.Image)
        Me.RibbonButton47.Name = "RibbonButton47"
        Me.RibbonButton47.SmallImage = CType(resources.GetObject("RibbonButton47.SmallImage"), System.Drawing.Image)
        Me.RibbonButton47.Text = "Devoluciones"
        '
        'RibbonButton46
        '
        Me.RibbonButton46.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.RibbonButton46.Image = CType(resources.GetObject("RibbonButton46.Image"), System.Drawing.Image)
        Me.RibbonButton46.LargeImage = CType(resources.GetObject("RibbonButton46.LargeImage"), System.Drawing.Image)
        Me.RibbonButton46.Name = "RibbonButton46"
        Me.RibbonButton46.SmallImage = CType(resources.GetObject("RibbonButton46.SmallImage"), System.Drawing.Image)
        Me.RibbonButton46.Text = "Cotizaciones"
        '
        'RibbonButton45
        '
        Me.RibbonButton45.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.RibbonButton45.Image = CType(resources.GetObject("RibbonButton45.Image"), System.Drawing.Image)
        Me.RibbonButton45.LargeImage = CType(resources.GetObject("RibbonButton45.LargeImage"), System.Drawing.Image)
        Me.RibbonButton45.Name = "RibbonButton45"
        Me.RibbonButton45.SmallImage = CType(resources.GetObject("RibbonButton45.SmallImage"), System.Drawing.Image)
        Me.RibbonButton45.Text = "Pedidos"
        '
        'RibbonButton44
        '
        Me.RibbonButton44.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.RibbonButton44.Image = CType(resources.GetObject("RibbonButton44.Image"), System.Drawing.Image)
        Me.RibbonButton44.LargeImage = CType(resources.GetObject("RibbonButton44.LargeImage"), System.Drawing.Image)
        Me.RibbonButton44.Name = "RibbonButton44"
        Me.RibbonButton44.SmallImage = CType(resources.GetObject("RibbonButton44.SmallImage"), System.Drawing.Image)
        Me.RibbonButton44.Text = "Remisiones"
        '
        'RibbonButton25
        '
        Me.RibbonButton25.DropDownItems.Add(Me.RibbonButton44)
        Me.RibbonButton25.DropDownItems.Add(Me.RibbonButton45)
        Me.RibbonButton25.DropDownItems.Add(Me.RibbonButton46)
        Me.RibbonButton25.DropDownItems.Add(Me.RibbonButton47)
        Me.RibbonButton25.DropDownItems.Add(Me.RibbonButton48)
        Me.RibbonButton25.DropDownItems.Add(Me.RibbonButton49)
        Me.RibbonButton25.Image = CType(resources.GetObject("RibbonButton25.Image"), System.Drawing.Image)
        Me.RibbonButton25.LargeImage = CType(resources.GetObject("RibbonButton25.LargeImage"), System.Drawing.Image)
        Me.RibbonButton25.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.RibbonButton25.Name = "RibbonButton25"
        Me.RibbonButton25.SmallImage = CType(resources.GetObject("RibbonButton25.SmallImage"), System.Drawing.Image)
        Me.RibbonButton25.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.RibbonButton25.Text = "Expedir"
        '
        'btnInvConBoletas
        '
        Me.btnInvConBoletas.Image = CType(resources.GetObject("btnInvConBoletas.Image"), System.Drawing.Image)
        Me.btnInvConBoletas.LargeImage = CType(resources.GetObject("btnInvConBoletas.LargeImage"), System.Drawing.Image)
        Me.btnInvConBoletas.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium
        Me.btnInvConBoletas.Name = "btnInvConBoletas"
        Me.btnInvConBoletas.SmallImage = CType(resources.GetObject("btnInvConBoletas.SmallImage"), System.Drawing.Image)
        Me.btnInvConBoletas.Text = "Boletas"
        '
        'RibbonPanel1
        '
        Me.RibbonPanel1.Items.Add(Me.mnuVentasPagare)
        Me.RibbonPanel1.Items.Add(Me.mnuVentasDocumentos)
        Me.RibbonPanel1.Name = "RibbonPanel1"
        Me.RibbonPanel1.Text = ""
        '
        'mnuVentasPagare
        '
        Me.mnuVentasPagare.Image = CType(resources.GetObject("mnuVentasPagare.Image"), System.Drawing.Image)
        Me.mnuVentasPagare.LargeImage = CType(resources.GetObject("mnuVentasPagare.LargeImage"), System.Drawing.Image)
        Me.mnuVentasPagare.Name = "mnuVentasPagare"
        Me.mnuVentasPagare.SmallImage = CType(resources.GetObject("mnuVentasPagare.SmallImage"), System.Drawing.Image)
        '
        'mnuVentasDocumentos
        '
        Me.mnuVentasDocumentos.Image = CType(resources.GetObject("mnuVentasDocumentos.Image"), System.Drawing.Image)
        Me.mnuVentasDocumentos.LargeImage = CType(resources.GetObject("mnuVentasDocumentos.LargeImage"), System.Drawing.Image)
        Me.mnuVentasDocumentos.Name = "mnuVentasDocumentos"
        Me.mnuVentasDocumentos.SmallImage = CType(resources.GetObject("mnuVentasDocumentos.SmallImage"), System.Drawing.Image)
        '
        'mnuVentasConOfertas
        '
        Me.mnuVentasConOfertas.Image = CType(resources.GetObject("mnuVentasConOfertas.Image"), System.Drawing.Image)
        Me.mnuVentasConOfertas.LargeImage = CType(resources.GetObject("mnuVentasConOfertas.LargeImage"), System.Drawing.Image)
        Me.mnuVentasConOfertas.Name = "mnuVentasConOfertas"
        Me.mnuVentasConOfertas.SmallImage = CType(resources.GetObject("mnuVentasConOfertas.SmallImage"), System.Drawing.Image)
        '
        'frmPrincipalN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1030, 488)
        Me.Controls.Add(Me.Ribbon1)
        Me.DoubleBuffered = True
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.Name = "frmPrincipalN"
        Me.Text = "frmPrincipalN"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Ribbon1 As System.Windows.Forms.Ribbon
    Friend WithEvents tabVentas As System.Windows.Forms.RibbonTab
    Friend WithEvents mnuArcGeneral As System.Windows.Forms.RibbonOrbMenuItem
    Friend WithEvents mnuArcVentas As System.Windows.Forms.RibbonOrbMenuItem
    Friend WithEvents mnuArcCompras As System.Windows.Forms.RibbonOrbMenuItem
    Friend WithEvents mnuArcInventario As System.Windows.Forms.RibbonOrbMenuItem
    Friend WithEvents mnuArcServicios As System.Windows.Forms.RibbonOrbMenuItem
    Friend WithEvents pnlVentasExpedir As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnVentasFac As System.Windows.Forms.RibbonButton
    Friend WithEvents btnVenCapRemisiones As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlVentasPagos As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnVenPagFacturas As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlVentasConsultas As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnVenConReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnVenConMovimientos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnVenConHistorial As System.Windows.Forms.RibbonButton
    Friend WithEvents btnVenCapCotizaciones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnVenCapNotasCredito As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonButton49 As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonButton48 As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonButton47 As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonButton46 As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonButton45 As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonButton44 As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonButton25 As System.Windows.Forms.RibbonButton
    Friend WithEvents btnVenPagRemisiones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnVenConCorte As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlVentasApartados As System.Windows.Forms.RibbonPanel
    Friend WithEvents tabCompras As System.Windows.Forms.RibbonTab
    Friend WithEvents pnlComCapturar As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnComCapFacturas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComCapRemisiones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComCapOrdenes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComCapPreordenes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComCapDevoluciones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComCapNotasCargo As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComCapNotasCredito As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlComConsultas As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnComConReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComConMovimientos As System.Windows.Forms.RibbonButton
    Friend WithEvents tabInventario As System.Windows.Forms.RibbonTab
    Friend WithEvents pnlInvOperacion As System.Windows.Forms.RibbonPanel
    Friend WithEvents pnlInvConsultas As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnInvConInventario As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvOpeMovimientos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvOpePedidos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvConMonitor As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvConCardex As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvConRevision As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlInvHerramientas As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnInvHerRecCostos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvHerBuscarNeg As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvHerRecInventario As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvHerAjustarCero As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvHerConfigConceptos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvOpeBoletas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvConReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComCapPagos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComConDevoluciones As System.Windows.Forms.RibbonButton
    Friend WithEvents tabServicios As System.Windows.Forms.RibbonTab
    Friend WithEvents pnlSerComercial As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnSerComNuevo As System.Windows.Forms.RibbonButton
    Friend WithEvents btnSerComBuscar As System.Windows.Forms.RibbonButton
    Friend WithEvents btnSerComReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnSerComEstados As System.Windows.Forms.RibbonButton
    Friend WithEvents tabPuntoVenta As System.Windows.Forms.RibbonTab
    Friend WithEvents pnlPunGeneral As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnPunGenVentas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnPunGenReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnPunGenMovimientos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnPunGenHerramientas As System.Windows.Forms.RibbonButton
    Friend WithEvents tabBancos As System.Windows.Forms.RibbonTab
    Friend WithEvents pnlBanOperacion As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnBanOpeDepositos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnBanOpePagos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnBanOpeConciliacion As System.Windows.Forms.RibbonButton
    Friend WithEvents tabNomina As System.Windows.Forms.RibbonTab
    Friend WithEvents pnlNomCatalogos As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnNomCatTrabajadores As System.Windows.Forms.RibbonButton
    Friend WithEvents tabGastos As System.Windows.Forms.RibbonTab
    Friend WithEvents pnlGasCatalogos As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnGasCatClasificaciones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnGasCatEmpleados As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlGasOperacion As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnGasOpeGastos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnGasOpeMovimientos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnGasOpeProgramar As System.Windows.Forms.RibbonButton
    Friend WithEvents btnGasOpeAlertas As System.Windows.Forms.RibbonButton
    Friend WithEvents tabEmpenos As System.Windows.Forms.RibbonTab
    Friend WithEvents pnlEmpCatalogos As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnEmpCatIdentificaciones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnEmpCatClasificaciones As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlEmpOperacion As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnEmpOpeEmpenos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnEmpOpePagos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnEmpOpeAdjudicaciones As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlEmpHerramientas As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnEmpherConfiguracion As System.Windows.Forms.RibbonButton
    Friend WithEvents tabContabilidad As System.Windows.Forms.RibbonTab
    Friend WithEvents pnlConCatalogos As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnConCatClasifPolizas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnConCatCuentas As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlConOperación As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnConOpePolizas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnConOpeSaldos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnConOpeConciliarDiot As System.Windows.Forms.RibbonButton
    Friend WithEvents btnConCatConceptos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnConCatMascaras As System.Windows.Forms.RibbonButton
    Friend WithEvents btnConOpeGenerarPolizas As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlVentasFertilizantes As System.Windows.Forms.RibbonPanel
    Friend WithEvents pnlComGranos As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnComGraBoletas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComGraLiquidaciones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComGraComprobantes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComGraReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComConValidador As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlPunRestaurante As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnPunResVentas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnPunResConfiguracion As System.Windows.Forms.RibbonButton
    Friend WithEvents tabHerramientas As System.Windows.Forms.RibbonTab
    Friend WithEvents pnlHerConfiguracion As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnHerConOpciones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnHerConConfigCorreo As System.Windows.Forms.RibbonButton
    Friend WithEvents btnHerConLicencias As System.Windows.Forms.RibbonButton
    Friend WithEvents btnHerConDistribuidores As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlSerInterno As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnSerIntNuevo As System.Windows.Forms.RibbonButton
    Friend WithEvents btnSerIntBuscar As System.Windows.Forms.RibbonButton
    Friend WithEvents btnSerIntReporte As System.Windows.Forms.RibbonButton
    Friend WithEvents btnSerIntEstados As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlBanConsultas As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnBanConReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlBanCatalogos As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnBanCatCuentas As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlNomOpercion As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnNomOpeNomina As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlNomConsultas As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnNomConReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlGasConsultas As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnGasConReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlEmpConsultas As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnEmpConReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnEmpOpeCompras As System.Windows.Forms.RibbonButton
    Friend WithEvents btnEmpOpeCortes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnEmpConConsultas As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlConConsultas As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnConConReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnHerConImportar As System.Windows.Forms.RibbonButton
    Friend WithEvents pnlHerHerramientas As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnHerHerContadorTimbres As System.Windows.Forms.RibbonButton
    Friend WithEvents pnHerBaseDatos As System.Windows.Forms.RibbonPanel
    Friend WithEvents btnHerBasRespaldar As System.Windows.Forms.RibbonButton
    Friend WithEvents btnHerBasRestaurar As System.Windows.Forms.RibbonButton
    Friend WithEvents btnHerHerActivarconector As System.Windows.Forms.RibbonButton
    Friend WithEvents btnHerHerMovimientosUsuario As System.Windows.Forms.RibbonButton
    Friend WithEvents btnHerHerDisenoDocumentos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnHerHerCambioPrecios As System.Windows.Forms.RibbonButton
    Friend WithEvents btnHerHerModifiarInv As System.Windows.Forms.RibbonButton
    Friend WithEvents btnConOpeConfiguracion As System.Windows.Forms.RibbonButton
    Friend WithEvents btnVenCapDevoluciones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnComCapDocumentos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvConReportesBoletas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnInvConBoletas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcGenUsuarios As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcGenEmpresas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcGenSucursales As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcGenTiposSucursales As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcGenZonas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcGenMetodoPagoFactura As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcGenMetodoPagoRemision As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcGenMonedas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcGenReportes As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonSeparator As System.Windows.Forms.RibbonSeparator
    Friend WithEvents RibbonSeparator2 As System.Windows.Forms.RibbonSeparator
    Friend WithEvents btnArcVenTiposClientes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcVenClientes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcVenVendedores As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcVenConceptosNotas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcComTiposProveedores As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcComProveedores As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcComConceptosNotas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcInvArticulos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcInvClasificaciones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcInvConceptos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcInvModelos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcInvTallas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcInvColores As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcInvRelaciones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcInvAlmacenes As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcSerCajas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcSerTecnicos As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcSerClasificaciones As System.Windows.Forms.RibbonButton
    Friend WithEvents btnArcSerOfertas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnSalir As System.Windows.Forms.RibbonOrbOptionButton
    Friend WithEvents btnArcGenMedidas As System.Windows.Forms.RibbonButton
    Friend WithEvents btnCambiarEmpresa As System.Windows.Forms.RibbonOrbOptionButton
    Friend WithEvents btnCambiarUsuario As System.Windows.Forms.RibbonOrbOptionButton
    Friend WithEvents btnAcerca As System.Windows.Forms.RibbonOrbOptionButton
    Friend WithEvents mnuVentasApartados As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonButton2 As System.Windows.Forms.RibbonButton
    Friend WithEvents mnuVentasNotasCargo As System.Windows.Forms.RibbonButton
    Friend WithEvents mnuPedidos As System.Windows.Forms.RibbonButton
    Friend WithEvents mnuVentasConOfertas As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonPanel1 As System.Windows.Forms.RibbonPanel
    Friend WithEvents mnuVentasPagare As System.Windows.Forms.RibbonButton
    Friend WithEvents mnuVentasDocumentos As System.Windows.Forms.RibbonButton
End Class
