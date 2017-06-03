Public Class frmPrincipal
    Dim Salir As Boolean = True
    Dim DocaImprimir As Byte
    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim ImpNDi2 As New Collection
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    Dim CadenaCFDI As String
    Dim CodigoBidimensional As Bitmap
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim CuantaY As Integer
    Dim TipoImpresora As Byte
    Dim Procesando As Boolean = False
    Dim Estado As Byte = Estados.Inicio
    Dim idSucursalI As Integer
    Dim idSucursalB As Integer
    Dim SerieB As String
    Dim SerieBNC As String
    Dim VersionConector As String
    Dim Cenvios As dbConectorEnvios
    Dim EnviaCorreos As String
    Dim RutaProcesados As String
    Dim RutaPDFs As String
    Dim RutaXML As String
    Dim RutaPDFsNC As String
    Dim RutaXMLNC As String
    Dim RutaXMLDEV As String
    Dim RutaPDFDEV As String
    Dim Munrec As String
    Dim O As dbOpciones
    Dim GP As dbGastosProgramables
    
    Private Sub frmPrincipal_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Salir And My.Settings.preguntarsalir Then
            If MsgBox("¿Salir del sistema?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Me.BackgroundImage = Nothing
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmPrincipal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            O = New dbOpciones(MySqlcon)
            GP = New dbGastosProgramables(MySqlcon)
            ChecaMenus()
            NotifyIcon1.Icon = GlobalIcono
            ToolStripLabel1.Text = "Empresa: " + GlobalNombreEmpresa + vbCrLf + "Usuario: " + GlobalUsuario
            If O._ConsultaRealTime = 1 Then GlobalConsultaTiempoReal = True
            GlobalIdMoneda = 2
            GlobalIdAlmacen = O._idAlmacen
            GlobalTipoFacturacion = O._TipoFacturacion
            FechaVerPunto2 = O._FechaPunto2
            GlobalTipoCosteo = O._TipoCosteo
            GlobalConector = O._Conector
            If GlobalTipoFacturacion = 2 Then mnuReporteMensual.Visible = False
            GlobalDireccionTimbrado = O._DireccionTimbrado
            GlobalPacCFDI = O._PacCFDI
            idSucursalB = O.idSucursalconector
            SerieB = O.Serieconector
            SerieBNC = O.SerieNCConecto
            GlobalIdiomaLetras = O.IdiomaLetras
            GlobalModoBusqueda = O.BuscaModoB
            GlobalSemillasResumida = O.BoletasResumida
            VersionConector = O._VersionConector
            EnviaCorreos = O._ConectorEnviarCorreos
            Munrec = O._ConectorMunrec
            If O.BuscaxFabricante = 1 Then
                GlobalpFabricante = True
            Else
                GlobalpFabricante = False
            End If
            If O.FacturarSoloExistencia = 1 Then
                GlobalSoloExistencia = True
            Else
                GlobalSoloExistencia = False
            End If
            My.Settings.emailfrom = O.Email
            My.Settings.emailhost = O.EmailHost
            My.Settings.emailusuario = O.EmailUsuario
            My.Settings.emailpassword = O.EmailPass
            If IsNumeric(O.EmailPuerto) Then
                My.Settings.emailpuerto = CInt(O.EmailPuerto)
            Else
                My.Settings.emailpuerto = 0
            End If
            If O.EmailSSL = 1 Then
                My.Settings.encriptacionssl = True
            Else
                My.Settings.encriptacionssl = False
            End If
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            GlobalIdSucursalDefault = sa.IdSucursal
            GlobalRutaConector = sa.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, 250, False)
            RutaXML = sa.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
            RutaPDFs = sa.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
            RutaPDFsNC = sa.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoPDF, False)
            RutaXMLNC = sa.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoXML, False)
            RutaPDFDEV = sa.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.DevolucionesPDF, False)
            RutaXMLDEV = sa.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.DevolucionesXML, True)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        Me.Text = GlobalNombreApp
        'mnuServiciosMenu.Visible = False
        NotifyIcon1.Visible = False
        Versiones()
        If GlobalTipoVersion = 3 Then
            IO.Directory.CreateDirectory(GlobalRutaConector + "\Error\")
            IO.Directory.CreateDirectory(GlobalRutaConector + "\Procesados\")
            IO.Directory.CreateDirectory(GlobalRutaConector + "\Procesados\" + Format(Date.Now, "yyyy") + "\")
            IO.Directory.CreateDirectory(GlobalRutaConector + "\Procesados\" + Format(Date.Now, "yyyy") + "\" + Format(Date.Now, "MM") + "\")
            RutaProcesados = GlobalRutaConector + "\Procesados\" + Format(Date.Now, "yyyy") + "\" + Format(Date.Now, "MM") + "\"
        End If
        'Try
        CargaImagenes()
        'Catch ex As Exception
        If My.Settings.abrepunto = "1" And ConPuntodeVenta = True Then
            toolstrip1.Visible = False
            GlobalEstadoPuntodeVenta = "Abierto"
            Dim O As New dbOpciones(MySqlcon)
            Dim RutaImagen As String
            Dim sa As New dbSucursalesArchivos
            Dim U As New dbUsuarios(GlobalIdUsuario, MySqlcon)
            sa.DaOpciones(GlobalIdEmpresa, False)
            RutaImagen = sa.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, 249, True)
            Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim Al As New dbAlmacenes(s.idAlmacen, MySqlcon)
            Dim f As New frmPuntodeVenta(sa.Documentopv, U.IdVendedor, GlobalIdSucursalDefault, s.idAlmacen, O.IdClienteDefault, sa.idCaja, RutaImagen, Al.Nombre)
            f.MdiParent = Me
            f.Show()
        End If
        
        'Aqui comienza lo que se hara al iniciar el sistema
        Dim P As New dbClientesEquipos(MySqlcon)
        If P.cartasPorEnviar() > 0 Then
            If MessageBox.Show("Hay cartas por enviar, ¿desea enviarlas?", "Pull Admin - Cartas por enviar", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim F As New frmClientesCorreos
                F.MdiParent = Me
                F.Show()
            End If
        End If
        If GP.hayAlertas And GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosVerNotificaciones, PermisosN.Secciones.Gastos) Then
            Dim FA As New frmGastosAviso()
            FA.MdiParent = Me
            FA.Show()
        End If
        'Aquí termina!
        'End Try
    End Sub
    Private Sub Versiones()
        'GlobalTipoVersion = 3 '
        
        ActivarConectorToolStripMenuItem.Visible = False
        'Version pura Facturacion----------------
        'mnuTecnicos.Visible = False
        'mnuClasificacionesDeServicios.Visible = False
        If GlobalTipoVersion = 1 Then
            'mnuReporteMensual.Visible = False
            ToolDevoluciones.Visible = False
            toolCompras.Visible = False
            toolPagosVentas.Visible = False
            toolPagosRemisiones.Visible = False
            ComprasToolStripMenuItem.Visible = False
            mnuClasificacionesDeServicios.Visible = False


            mnuarchivoln02.Visible = False
            mnuarchivoln03.Visible = False
            mnuPagos.Visible = False
            mnuPagosRem.Visible = False
            mnubarra13.Visible = False
            mnuClasificacionesDeServicios.Visible = False
            mnuInventarioTop.Visible = False
            mnuProveedores.Visible = False
            
            mnuMonedas.Visible = False

            mnuarchivoln03.Visible = False
            mnuDocumentosVentas.Visible = False
            mnuCompras.Enabled = False
        End If
        'Version Facturacion y Clientes
        If GlobalTipoVersion = 2 Then
            toolCompras.Visible = False
            ToolDevoluciones.Visible = False
            ComprasToolStripMenuItem.Visible = False
            mnuarchivoln03.Visible = False
            mnuClasificacionesDeServicios.Visible = False
            mnuInventarioTop.Visible = False
            mnuProveedores.Visible = False
            mnuMonedas.Visible = False
            mnuarchivoln03.Visible = False
            mnuCompras.Enabled = False
            mnuOpcionesCorreo.Visible = True
        End If

        If GlobalTipoVersion = 3 Then
            NotifyIcon1.Visible = True
            mnuVentasMenu.Visible = False
            ToolDevoluciones.Visible = True
            ComprasToolStripMenuItem.Visible = False
            mnuInventarioTop.Visible = False
            VendedoresToolStripMenuItem.Visible = False
            mnuProveedores.Visible = False
            mnuAlmacenes.Visible = False
            mnuClasificacionesDeInventarioTool.Visible = False
            mnuArticulos.Visible = False
            mnuInventarioConceptos.Visible = False
            mnuFormasdePago.Visible = False
            mnuMedidas.Visible = False
            mnuMonedas.Visible = False
            mnuInvRelaciones.Visible = False
            mnuConceptoNventas.Visible = False
            mnuConceptoNcompras.Visible = False
            mnuReportesCatalagos.Visible = False
            toolInventario.Visible = False
            toolClientes.Visible = False
            toolCompras.Visible = False
            toolPagosVentas.Visible = False
            toolPagosRemisiones.Visible = False
            toolRemisiones.Visible = False
            mnuCajas.Visible = False
            mnuFormasdePagorem.Visible = False
            toolReportes.Visible = False
            toolInvConsulta.Visible = False

            mnuarchivoln02.Visible = False
            mnuarchivoln03.Visible = False
            mnuarchivoln04.Visible = False
            mnuarchivoln05.Visible = False
            mnuarchivoln06.Visible = False
            mnuarchivoln07.Visible = False
            mnuarchivoln09.Visible = False
            'mnuClientes.Visible = False
            ActivarConectorToolStripMenuItem.Visible = True
            ActivarConectorToolStripMenuItem.Checked = True
            toolPuntodeVenta.Visible = False
            mnuPuntoDeVentaMain.Visible = False

            Dim Em As New dbEmpresas
            Em.IniciarMySQLE(My.Settings.BasedeDatos, My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
            Em.LlenaDatos(GlobalIdEmpresa)
            IniciarMySQL2(MySqlcon.Database, Em.Servidor, Em.Usuario, Em.PasswordS, My.Settings.puertodb)
            Em.MySqlconE.Close()
            Cenvios = New dbConectorEnvios(MySqlcon2)
            Timer1.Enabled = True
            Timer2.Enabled = True
            ToolApartados.Visible = False
            'Me.Hide()
            'Me.ShowInTaskbar = False
            'Me.WindowState = FormWindowState.Minimized
            ModelosToolStripMenuItem.Visible = False
            'mnuBancosArchivo.Visible = False
            mnuDescuentos.Visible = False
            mnuBancosOperacion.Visible = False
            mnuConsultaOfertasVentas.Visible = False
        End If
        If GlobalTipoVersion = 4 Then
            mnuReporteMensual.Visible = False
            toolCompras.Visible = False
            toolPagosVentas.Visible = False
            ComprasToolStripMenuItem.Visible = False
            mnuarchivoln02.Visible = False
            mnuarchivoln03.Visible = False
            mnuPagos.Visible = False
            mnubarra13.Visible = False
            mnuClasificacionesDeServicios.Visible = False
            mnuInventarioTop.Visible = False
            mnuProveedores.Visible = False
            mnuMonedas.Visible = False
            mnuarchivoln03.Visible = False
            mnuDocumentosVentas.Visible = False
            mnuCompras.Enabled = False
            toolVentas.Visible = False
            toolPagosVentas.Visible = False
            toolNotasdeCredito.Visible = False
            toolReportes.Visible = False
            toolRemisiones.Visible = False
            toolPagosRemisiones.Visible = False
            ToolApartados.Visible = False
            ToolDevoluciones.Visible = False
            toolInvConsulta.Visible = False
            toolClientes.Visible = False
            toolInventario.Visible = False
            'toolstrip1.Visible = False
            'Abrir punto de venta con datos default segun usuario.
        End If
        If ConPuntodeVenta = False Then
            toolPuntodeVenta.Visible = False
            mnuPuntoDeVentaMain.Visible = False
        End If
        If GlobalConLicencias = False Then
            mnuLicencias.Visible = False
            mnuDitribuidores.Visible = False
        End If
        If GlobalConBancos = False Then
            'mnuBancosArchivo.Visible = False
            mnuBancosOperacion.Visible = False
        End If
        If globalConNomina = False Then
            mnuNomina.Visible = False
            ToolNomina.Visible = False
        End If
        If GlobalConServicios = False Then
            mnuServiciosMenu.Visible = False
            mnuTecnicos.Visible = False
            mnuClasificacionesDeServicios.Visible = False
        End If
        If GlobalConGastos = False Then
            mnuGastosP.Visible = False
        End If
        If GlobalconEmpenios = False Then
            mnuEmpeniosP.Visible = False
            toolEmpenios.Visible = False
            toolEmpeniosPagos.Visible = False
            toolEmpeniosCompras.Visible = False
        End If
        If GlobalConServiciosInterno = False Then
            mnuServiciosInt.Visible = False
        End If
        If GlobalConContabilidad = False Then
            mnuContabilidad.Visible = False
            'mnuProveedores.Visible = True
            toolpolizas.Visible = False
            toolContaReportes.Visible = False
        Else
            mnuProveedores.Visible = True
        End If
        If GlobalConFertilizantes = False Then
            mnuFertilizantes.Visible = False
            toolFertilizantes.Visible = False
        End If
        If GlobalConValidador = False Then
            mmnuValidadorXML.Visible = False
        End If
        If GlobalconSemillas = False Then
            mnuSemillas.Visible = False
        End If
        If GlobalTipoVersion = 4 Then
            mnuVentasMenu.Visible = False
            ComprasToolStripMenuItem.Visible = False
            mnuInventarioTop.Visible = False
            toolCompras.Visible = False
            toolVentas.Visible = False
            toolNotasdeCredito.Visible = False
            ToolApartados.Visible = False
            toolClientes.Visible = False
            toolInventario.Visible = False
            toolInvConsulta.Visible = False
            toolPagosVentas.Visible = False
            toolReportes.Visible = False
            toolRemisiones.Visible = False
            toolPagosRemisiones.Visible = False
            ToolDevoluciones.Visible = False

        End If
        If GlobalConUsuarios = False Then
            mnuMovimientosUsuarios.Visible = False
        End If
        If GlobalconIntegracion = False Then
            mnuContabilidadMascaras.Visible = False
            mnuContaGenerarpolizas.Visible = False
        End If
        If GlobalConRestaurant = False Then
            mnuRestaurante.Visible = False
        End If
    End Sub
    Private Sub CargaImagenes()
        On Error Resume Next
        Me.Icon = GlobalIcono
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.BackgroundImage = Image.FromFile("fondopull.jpg")
        toolInventario.Image = Image.FromFile(Application.StartupPath + "\iconos\articulos.png")
        toolClientes.Image = Image.FromFile(Application.StartupPath + "\iconos\clientes.png")
        toolVentas.Image = Image.FromFile(Application.StartupPath + "\iconos\Facturacion.png")
        toolCompras.Image = Image.FromFile(Application.StartupPath + "\iconos\compras.png")
        toolPagosVentas.Image = Image.FromFile(Application.StartupPath + "\iconos\pagos.png")
        toolNotasdeCredito.Image = Image.FromFile(Application.StartupPath + "\iconos\Notacredito.png")
        toolReportes.Image = Image.FromFile(Application.StartupPath + "\iconos\reportes.png")
        toolInvConsulta.Image = Image.FromFile(Application.StartupPath + "\iconos\Search.png")
        toolPuntodeVenta.Image = Image.FromFile(Application.StartupPath + "\iconos\caja.png")
        toolRemisiones.Image = Image.FromFile(Application.StartupPath + "\iconos\remision.png")
        toolPagosRemisiones.Image = Image.FromFile(Application.StartupPath + "\iconos\pagosremision.png")
        ToolApartados.Image = Image.FromFile(Application.StartupPath + "\iconos\apartados.png")
        ToolDevoluciones.Image = Image.FromFile(Application.StartupPath + "\iconos\devolucion.png")
        ToolNomina.Image = Image.FromFile(Application.StartupPath + "\iconos\nomina.png")
        toolEmpenios.Image = Image.FromFile(Application.StartupPath + "\iconos\empenios.png")
        toolEmpeniosPagos.Image = Image.FromFile(Application.StartupPath + "\iconos\empeniospagos.png")
        toolEmpeniosCompras.Image = Image.FromFile(Application.StartupPath + "\iconos\empenioscompras.png")
        toolpolizas.Image = Image.FromFile(Application.StartupPath + "\iconos\polizas.png")
        toolContaReportes.Image = Image.FromFile(Application.StartupPath + "\iconos\contareportes.png")
        toolFertilizantes.Image = Image.FromFile(Application.StartupPath + "\iconos\fertilizantes.png")
    End Sub
    Private Function BuscaVentanas(ByVal Nombre As String) As Boolean
        If My.Settings.multiplesventanas Then
            Dim f As Form
            For Each f In Me.MdiChildren
                If f.Name = Nombre Then
                    If f.Name = "frmEstilosTallasColores" Then
                        f.Close()
                        Return False
                    Else
                        f.Focus()
                    End If
                    Return True
                End If
            Next
        End If
        Return False
    End Function
    Private Sub CierraVentanas(Optional ByVal Siempre As Boolean = False)
        If My.Settings.multiplesventanas = False And Siempre = False Then
            Dim f As Form
            For Each f In Me.MdiChildren
                f.Close()
            Next
        End If
    End Sub
    Private Sub ClasificacionesDeInventarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClasificacionesDeInventarioTool.Click
        'CierraVentanas()
        'If BuscaVentanas("frmInventarioClasificacion") = False Then
        '    Dim f As New frmInventarioClasificacion
        '    f.MdiParent = Me
        '    f.Show()
        'End If

        CierraVentanas()
        If BuscaVentanas("frmInventarioModificar") = False Then
            Dim f As New frmInventarioModificar()
            f.MdiParent = Me
            f.Show()
        End If

    End Sub

    

    Private Sub ArtículosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuArticulos.Click
        CierraVentanas()
        If BuscaVentanas("frmInventario") = False Then
            Dim f As New frmInventario
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ProveedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuProveedores.Click
        CierraVentanas()
        If BuscaVentanas("frmProveedores") = False Then
            Dim f As New frmProveedores(0, 0, "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ClientesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClientes.Click
        CierraVentanas()
        If BuscaVentanas("frmClientes") = False Then
            Dim f As New frmClientes(0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    


    Private Sub AcercaDeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcercaDeToolStripMenuItem.Click
        CierraVentanas()
        Dim f As New frmAcercade
        f.MdiParent = Me
        f.Show()
    End Sub

   

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If Me.ShowInTaskbar = True Then
            Me.ShowInTaskbar = False
            Me.WindowState = FormWindowState.Minimized
        Else
            Me.ShowInTaskbar = True
            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub

    Private Sub UsuariosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUsuarios.Click
        CierraVentanas()
        If BuscaVentanas("frmUsuarios") = False Then
            Dim f As New frmUsuarios
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ConsultaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCompraConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasConsulta") = False Then
            Dim f As New frmComprasConsulta(ModosDeBusqueda.Principal, 0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ComprasToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCompras.Click
        If GlobalTipoVersion = 0 Then
            CierraVentanas()
            If BuscaVentanas("frmCompras") = False Then
                Dim f As New frmCompras
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub NuevoServicioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuServicios.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServicioVer, PermisosN.Secciones.Servicios) = True) Then
            CierraVentanas()
            If BuscaVentanas("frmServicios") = False Then
                Dim f As New frmServicios
                f.MdiParent = Me
                f.Show()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub BuscarServicioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBuscarServicio.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosConsultaVer, PermisosN.Secciones.Servicios) = True) Then

            CierraVentanas()
            If BuscaVentanas("frmServiciosConsulta") = False Then
                Dim f As New frmServiciosConsulta
                f.MdiParent = Me
                f.Show()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub PedidosToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPedidosCompras.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasPedidos") = False Then
            Dim f As New frmComprasPedidos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    

    Private Sub BuscarPedidoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPedidosComprasConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasPedidosConsulta") = False Then
            Dim f As New frmComprasPedidosConsulta(ModosDeBusqueda.Principal)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        Salir = False
        Me.Close()
    End Sub

   

    Private Sub BuscarCotizaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmComprasCotizacionesConsulta") = False Then
            Dim f As New frmComprasCotizacionesConsulta(ModosDeBusqueda.Principal, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub OpcionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpcionesCorreo.Click
        CierraVentanas()
        If BuscaVentanas("frmOpcionesCorreo") = False Then
            Dim f As New frmOpcionesCorreo
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub DiseñoFacturasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiseñoFacturasToolStripMenuItem.Click, DiseñoRemisionesToolStripMenuItem.Click, DiseñoPedidosToolStripMenuItem.Click, DiseñoCotizacionesToolStripMenuItem.Click, DiseñoFacturasProveedorToolStripMenuItem.Click, DiseñoRemisionesProveedorToolStripMenuItem.Click, DiseñoOrdenesDeCompraToolStripMenuItem.Click, DiseñoPreviosDeCompraToolStripMenuItem.Click

    End Sub

    Private Sub AlmacenesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAlmacenes.Click
        CierraVentanas()
        If BuscaVentanas("frmAlmacenes") = False Then
            Dim f As New frmAlmacenes
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub RequisicionesDeCotizaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ConsultarRequisicionDeCotizaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub OpcionesToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpciones.Click
        CierraVentanas()
        If BuscaVentanas("frmOpciones") = False Then
            Dim f As New frmOpciones
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub PagosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPagos.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasPagos") = False Then
            Dim f As New frmVentasPagos("", "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuInventarioConceptos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventarioConceptos.Click
        CierraVentanas()
        If BuscaVentanas("frmInvnetarioConceptos") = False Then
            Dim f As New frmInvnetarioConceptos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub NuevoInventarioInicialToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventarioMovimientos.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioMovimientosN") = False Then
            Dim f As New frmInventarioMovimientosN()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuVentas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentas.Click
        'If mnuVentas.Visible Then
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If GlobalTipoVersion <> 4 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas) = True Then
                CierraVentanas()
                If BuscaVentanas("frmVentasN") = False Then
                    Dim Op As New dbOpciones(MySqlcon)
                    If Op.MaximizarVentas = 0 Then
                        Dim f As New frmVentasN(0, 0, 0, 0)
                        f.MdiParent = Me
                        f.Show()
                    Else
                        GlobalEstadoVentanas = GlobalEstadoVentanas Or 1
                        'toolstrip1.Visible = False
                        Dim f As New frmVentasN(0, 0, 0, 0)
                        f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                        f.WindowState = FormWindowState.Maximized
                        f.MdiParent = Me
                        f.Show()
                    End If
                End If
            End If
        End If
        'End If

    End Sub

    Private Sub mnuBuscarVenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentaConsulta.Click
        CierraVentanas()

        If BuscaVentanas("frmVentasConsulta") = False Then
            Dim f As New frmVentasConsulta(ModosDeBusqueda.Principal, 0, False)
            f.MdiParent = Me

            f.Show()
        End If
    End Sub

    Private Sub mnuVentasRemision_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentasRemision.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasRemisiones") = False Then
            'Dim f As New frmVentasRemisiones()
            'f.MdiParent = Me
            'f.Show()
            'If BuscaVentanas("frmVentasPedidos") = False Then
            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            Dim Op As New dbOpciones(MySqlcon)
            If Op.MaximizarVentas = 0 Then
                Dim f As New frmVentasRemisiones()
                f.MdiParent = Me
                f.Show()
            Else
                GlobalEstadoVentanas = GlobalEstadoVentanas Or 8
                'toolstrip1.Visible = False
                Dim f As New frmVentasRemisiones()
                f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                f.WindowState = FormWindowState.Maximized
                f.MdiParent = Me
                f.Show()
            End If
            'End If
        End If
    End Sub

    Private Sub mnuVentasRemisionConsulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentasRemisionConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasRemisionesConsulta") = False Then
            Dim f As New frmVentasRemisionesConsulta(ModosDeBusqueda.Principal, 0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuVentasPedidos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentasPedidos.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasPedidos") = False Then
            Dim Op As New dbOpciones(MySqlcon)
            If Op.MaximizarVentas = 0 Then
                Dim f As New frmVentasPedidos()
                f.MdiParent = Me
                f.Show()
            Else
                GlobalEstadoVentanas = GlobalEstadoVentanas Or 4
                'toolstrip1.Visible = False
                Dim f As New frmVentasPedidos()
                f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                f.WindowState = FormWindowState.Maximized
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub mnuVentasCotizacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentasCotizacion.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasCotizacion") = False Then
            Dim Op As New dbOpciones(MySqlcon)
            If Op.MaximizarVentas = 0 Then
                Dim f As New frmVentasCotizacion
                f.MdiParent = Me
                f.Show()
            Else
                GlobalEstadoVentanas = GlobalEstadoVentanas Or 2
                'toolstrip1.Visible = False
                Dim f As New frmVentasCotizacion()
                f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                f.WindowState = FormWindowState.Maximized
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub KardexToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuKardex.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioKardex") = False Then
            Dim f As New frmInventarioKardex(ModosDeBusqueda.Principal)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub NuevaRemisiónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuComprasRemisiones.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasRemisiones") = False Then
            Dim f As New frmComprasRemisiones
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub BuscarRemisiónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemisionesComprasConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasRemisionesConsulta") = False Then
            Dim f As New frmComprasRemisionesConsulta(ModosDeBusqueda.Principal)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ChecaMenus()
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClientesVer, PermisosN.Secciones.Catalagos) = False Then mnuClientes.Visible = False Else mnuClientes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClientesVer, PermisosN.Secciones.Catalagos) = False Then toolClientes.Visible = False Else toolClientes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.VendedoresVer, PermisosN.Secciones.Catalagos) = False Then VendedoresToolStripMenuItem.Visible = False Else VendedoresToolStripMenuItem.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ProveedoresVer, PermisosN.Secciones.Catalagos) = False Then mnuProveedores.Visible = False Else mnuProveedores.Visible = True
        'If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.t, PermisosN.Secciones.Catalagos) = False Then mnuTecnicos.Visible = False
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.SucursalesVer, PermisosN.Secciones.Catalagos) = False Then mnuSucursales.Visible = False Else mnuSucursales.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesVer, PermisosN.Secciones.Catalagos) = False Then mnuAlmacenes.Visible = False Else mnuAlmacenes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioVer, PermisosN.Secciones.Catalagos) = False Then mnuClasificacionesDeInventarioTool.Visible = False Else mnuClasificacionesDeInventarioTool.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.InventarioVer, PermisosN.Secciones.Catalagos) = False Then mnuArticulos.Visible = False Else mnuArticulos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.InventarioVer, PermisosN.Secciones.Catalagos) = False Then toolInventario.Visible = False Else toolInventario.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosdeInventarioVer, PermisosN.Secciones.Catalagos) = False Then mnuInventarioConceptos.Visible = False Else mnuInventarioConceptos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.FormasdePagoVer, PermisosN.Secciones.Catalagos) = False Then mnuFormasdePago.Visible = False Else mnuFormasdePago.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.FormasdePagoRemVer, PermisosN.Secciones.Catalagos) = False Then mnuFormasdePagorem.Visible = False Else mnuFormasdePagorem.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.CajasVer, PermisosN.Secciones.Catalagos) = False Then mnuCajas.Visible = False Else mnuCajas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.MedidasVer, PermisosN.Secciones.Catalagos) = False Then mnuMedidas.Visible = False Else mnuMedidas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.MonedasVer, PermisosN.Secciones.Catalagos) = False Then mnuMonedas.Visible = False Else mnuMonedas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasVentasVer, PermisosN.Secciones.Catalagos) = False Then mnuConceptoNventas.Visible = False Else mnuConceptoNventas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasComprasVer, PermisosN.Secciones.Catalagos) = False Then mnuConceptoNcompras.Visible = False Else mnuConceptoNcompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ReportesVer, PermisosN.Secciones.Catalagos2) = False Then mnuReportesCatalagos.Visible = False Else mnuReportesCatalagos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.UsuariosVer, PermisosN.Secciones.Catalagos2) = False Then mnuUsuarios.Visible = False Else mnuUsuarios.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.EmpresasVer, PermisosN.Secciones.Catalagos2) = False Then mnuEmpresas.Visible = False Else mnuEmpresas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.TecnicosVer, PermisosN.Secciones.Catalagos2) = False Then mnuTecnicos.Visible = False Else mnuTecnicos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ZonasVer, PermisosN.Secciones.Catalagos2) = False Then mnuZonas.Visible = False Else mnuZonas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.OfertasVer, PermisosN.Secciones.Catalagos2) = False Then mnuDescuentos.Visible = False Else mnuDescuentos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.InventarioRelacionesVer, PermisosN.Secciones.Catalagos2) = False Then mnuInvRelaciones.Visible = False Else mnuInvRelaciones.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ClientesTiposVer, PermisosN.Secciones.Catalagos2) = False Then mnuTiposClientes.Visible = False Else mnuTiposClientes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ProvTiposVer, PermisosN.Secciones.Catalagos2) = False Then mnuTiposProveedores.Visible = False Else mnuTiposProveedores.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.SucTiposVer, PermisosN.Secciones.Catalagos2) = False Then mnuTiposSucursales.Visible = False Else mnuTiposSucursales.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ClientesTiposVer, PermisosN.Secciones.Catalagos2) = False And GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ProvTiposVer, PermisosN.Secciones.Catalagos2) = False And GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.SucTiposVer, PermisosN.Secciones.Catalagos2) = False Then
            mnuTipoMain.Visible = False
        Else
            mnuTipoMain.Visible = True
        End If


        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CotizacionesVer, PermisosN.Secciones.Ventas) = False Then mnuVentasCotizacion.Visible = False Else mnuVentasCotizacion.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CotizacionesVer, PermisosN.Secciones.Ventas) = False Then mnuVentasCotizacionConsulta.Visible = False Else mnuVentasCotizacionConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PedidosVer, PermisosN.Secciones.Ventas) = False Then mnuVentasPedidos.Visible = False Else mnuVentasPedidos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PedidosVer, PermisosN.Secciones.Ventas) = False Then mnuVentasPedidosConsulta.Visible = False Else mnuPedidosComprasConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesVer, PermisosN.Secciones.Ventas) = False Then mnuVentasRemision.Visible = False Else mnuVentasRemision.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesVer, PermisosN.Secciones.Ventas) = False Then toolRemisiones.Visible = False Else toolRemisiones.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesVer, PermisosN.Secciones.Ventas) = False Then mnuVentasRemisionConsulta.Visible = False Else mnuVentasRemisionConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas) = False Then mnuVentas.Visible = False Else mnuVentas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas) = False Then toolVentas.Visible = False Else toolVentas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas) = False Then mnuVentaConsulta.Visible = False Else mnuVentaConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.DevolucionesVer, PermisosN.Secciones.Ventas) = False Then mnuVentasDev.Visible = False Else mnuVentasDev.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.DevolucionesVer, PermisosN.Secciones.Ventas) = False Then mnuVentasDevConsulta.Visible = False Else mnuNotasdeCredito.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoVer, PermisosN.Secciones.Ventas) = False Then mnuNotasdeCredito.Visible = False Else mnuNotasdeCreditoConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoVer, PermisosN.Secciones.Ventas) = False Then mnuNotasdeCreditoConsulta.Visible = False Else mnuNotasdeCreditoConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoVer, PermisosN.Secciones.Ventas) = False Then toolNotasdeCredito.Visible = False Else toolNotasdeCredito.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCargoVer, PermisosN.Secciones.Ventas) = False Then mnuNotasdeCargo.Visible = False Else mnuNotasdeCargo.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCargoVer, PermisosN.Secciones.Ventas) = False Then mnuNotasdeCargoconsulta.Visible = False Else mnuNotasdeCargoconsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagareVer, PermisosN.Secciones.Ventas) = False Then mnuPagare.Visible = False Else mnuPagare.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.DocumentosClientesVer, PermisosN.Secciones.Ventas) = False Then mnuDocumentosVentas.Visible = False Else mnuDocumentosVentas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosVer, PermisosN.Secciones.Ventas) = False Then mnuPagos.Visible = False Else mnuPagos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosVer, PermisosN.Secciones.Ventas) = False Then toolPagosVentas.Visible = False Else toolPagosVentas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosRemVer, PermisosN.Secciones.Ventas) = False Then mnuPagosRem.Visible = False Else mnuPagosRem.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosRemVer, PermisosN.Secciones.Ventas) = False Then toolPagosRemisiones.Visible = False Else toolPagosRemisiones.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.ReportesVer, PermisosN.Secciones.Ventas) = False Then mnuVentasReportes.Visible = False Else mnuVentasReportes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.ReportesVer, PermisosN.Secciones.Ventas) = False Then toolReportes.Visible = False Else toolReportes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.Consultas, PermisosN.Secciones.Ventas) = False Then mnuConsultasVentas.Visible = False Else mnuConsultasVentas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.Consultas, PermisosN.Secciones.Ventas) = False Then toolInvConsulta.Visible = False Else toolInvConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasApartadosVer, PermisosN.Secciones.Ventas) = False Then ToolApartados.Visible = False Else ToolApartados.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasApartadosVer, PermisosN.Secciones.Ventas) = False Then mnuApartadosPrincipal.Visible = False Else mnuApartados.Visible = True


        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CotizacionesVer, PermisosN.Secciones.Compras) = False Then mnuCotizacionesCompras.Visible = False Else mnuCotizacionesCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CotizacionesVer, PermisosN.Secciones.Compras) = False Then mnuCotizacionesConsultaCompras.Visible = False Else mnuCotizacionesConsultaCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.PedidosVer, PermisosN.Secciones.Compras) = False Then mnuPedidosCompras.Visible = False Else mnuPedidosCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.PedidosVer, PermisosN.Secciones.Compras) = False Then mnuPedidosComprasConsulta.Visible = False Else mnuPedidosComprasConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.RemisionesVer, PermisosN.Secciones.Compras) = False Then mnuComprasRemisiones.Visible = False Else mnuComprasRemisiones.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.RemisionesVer, PermisosN.Secciones.Compras) = False Then mnuRemisionesComprasConsulta.Visible = False Else mnuRemisionesComprasConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasVer, PermisosN.Secciones.Compras) = False Then mnuCompras.Visible = False Else mnuCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasVer, PermisosN.Secciones.Compras) = False Then toolCompras.Visible = False Else toolCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasVer, PermisosN.Secciones.Compras) = False Then mnuCompraConsulta.Visible = False Else mnuCompraConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DevolucionesVer, PermisosN.Secciones.Compras) = False Then mnuDevolucionesCompras.Visible = False Else mnuDevolucionesCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DevolucionesVer, PermisosN.Secciones.Compras) = False Then mnuDevolucionCompraConsulta.Visible = False Else mnuDevolucionCompraConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoVer, PermisosN.Secciones.Compras) = False Then mnuNotasdeCreditoCompras.Visible = False Else mnuNotasdeCreditoCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoVer, PermisosN.Secciones.Compras) = False Then mnuNotasdeCreditoComprasConsulta.Visible = False Else mnuNotasdeCreditoComprasConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCargoVer, PermisosN.Secciones.Compras) = False Then mnuNotasdeCargoCompras.Visible = False Else mnuNotasdeCargoCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCargoVer, PermisosN.Secciones.Compras) = False Then mnuNotasdeCargoComprasConsulta.Visible = False Else mnuNotasdeCargoComprasConsulta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DocumentosProveedoresVer, PermisosN.Secciones.Compras) = False Then mnuDocumentosCompras.Visible = False Else mnuDocumentosCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.PagosVer, PermisosN.Secciones.Compras) = False Then mnuComprasPagos.Visible = False Else mnuComprasPagos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.Reportes, PermisosN.Secciones.Compras) = False Then mnuReportesCompras.Visible = False Else mnuReportesCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.Consultas, PermisosN.Secciones.Compras) = False Then mnuConsultasCompras.Visible = False Else mnuConsultasCompras.Visible = True

        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario) = False Then mnuInventarioMovimientos.Visible = False Else mnuInventarioMovimientos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.KardexVer, PermisosN.Secciones.Inventario) = False Then mnuKardex.Visible = False Else mnuKardex.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.RevisionVer, PermisosN.Secciones.Inventario) = False Then mnuInventarioRevision.Visible = False Else mnuInventarioRevision.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.Reportes, PermisosN.Secciones.Inventario) = False Then mnuInventarioReportes.Visible = False Else mnuInventarioReportes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.RecalcularCostos, PermisosN.Secciones.Inventario) = False Then mnuInventarioRecalcular.Visible = False Else mnuInventarioRecalcular.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.RecalcularInventarios, PermisosN.Secciones.Inventario) = False Then mnuRecalcularInventario.Visible = False Else mnuRecalcularInventario.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.BuscarNegativos, PermisosN.Secciones.Inventario) = False Then mnuBuscarNegativos.Visible = False Else mnuBuscarNegativos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.RecalcularInventarios, PermisosN.Secciones.Inventario) = False Then mnuInventarioAjusteCero.Visible = False Else mnuInventarioAjusteCero.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosVer, PermisosN.Secciones.Inventario) = False Then mnuInventarioPedidos.Visible = False Else mnuInventarioPedidos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosVer, PermisosN.Secciones.Inventario) = False Then mnuConsultarPedidos.Visible = False Else mnuConsultarPedidos.Visible = True
        'If mnuInventarioRecalcular.Visible = False And mnuRecalcularInventario.Visible = False And mnuBuscarNegativos.Visible = False Then mnuInventarioHerramientas.Visible = False Else mnuInventarioHerramientas.Visible = True


        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.VentasVer, PermisosN.Secciones.PuntodeVenta) = False Then mnuPuntodeVentaVentas.Visible = False Else mnuPuntodeVentaVentas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.VentasVer, PermisosN.Secciones.PuntodeVenta) = False Then toolPuntodeVenta.Visible = False Else toolPuntodeVenta.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.ReportesVer, PermisosN.Secciones.PuntodeVenta) = False Then mnuPuntodeVentaReportes.Visible = False Else mnuPuntodeVentaReportes.Visible = True

        If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.OpcionesVer, PermisosN.Secciones.Herramientas) = False Then mnuOpciones.Visible = False Else mnuOpciones.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.Reportemensual, PermisosN.Secciones.Herramientas) = False Then mnuReporteMensual.Visible = False Else mnuReporteMensual.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.OpcionesCorreoVer, PermisosN.Secciones.Herramientas) = False Then mnuOpcionesCorreo.Visible = False Else mnuOpcionesCorreo.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingVer, PermisosN.Secciones.Herramientas) = False Then mnuDocDesing.Visible = False Else mnuDocDesing.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.RespaldoVer, PermisosN.Secciones.Herramientas) = False Then mnuRespaldo.Visible = False Else mnuRespaldo.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.RestaurarVer, PermisosN.Secciones.Herramientas) = False Then mnuRestaurar.Visible = False Else mnuRestaurar.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.CambiodePrecios, PermisosN.Secciones.Herramientas) = False Then mnuCambioPrecios.Visible = False Else mnuCambioPrecios.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.Importador, PermisosN.Secciones.Herramientas) = False Then mnuImportar.Visible = False Else mnuImportar.Visible = True

        If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.CuentasVer, PermisosN.Secciones.Bancos) = False Then mnuBancosCuentas.Visible = False Else mnuBancosCuentas.Visible = True
        'If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.BancosVer, PermisosN.Secciones.Bancos) = False Then mnuBancos.Visible = False Else mnuBancos.Visible = True
        'If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.CuentasContablesVer, PermisosN.Secciones.Bancos) = False Then mnuCuentasContables.Visible = False Else mnuCuentasContables.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.DepositosVer, PermisosN.Secciones.Bancos) = False Then mnuDepositos.Visible = False Else mnuDepositos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.PagoProveedoresVer, PermisosN.Secciones.Bancos) = False Then mnuPagoProveedores.Visible = False Else mnuPagoProveedores.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.Consiliacion, PermisosN.Secciones.Bancos) = False Then mnuConciliacion.Visible = False Else mnuConciliacion.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.Reportes, PermisosN.Secciones.Bancos) = False Then mnuReportesBancos.Visible = False Else mnuReportesBancos.Visible = True

        If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaVer, PermisosN.Secciones.Nomina) = False Then mnuNominas.Visible = False Else mnuNominas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaVer, PermisosN.Secciones.Nomina) = False Then ToolNomina.Visible = False Else ToolNomina.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaVer, PermisosN.Secciones.Nomina) = False Then mnuNominaReportes.Visible = False Else mnuNominaReportes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.TrabajadoresVer, PermisosN.Secciones.Nomina) = False Then mnuTrabajadores.Visible = False Else mnuTrabajadores.Visible = True

        If GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServicioVer, PermisosN.Secciones.Servicios) = False Then mnuServicios.Visible = False Else mnuServicios.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosVerReportes, PermisosN.Secciones.Servicios) = False Then mnuServiciosReportes.Visible = False Else mnuServiciosReportes.Visible = True

        If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosVer, PermisosN.Secciones.Gastos) = False Then mnuGastos.Visible = False Else mnuGastos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesVer, PermisosN.Secciones.Gastos) = False Then mnuGastosClas.Visible = False Else mnuGastosClas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosEmpleadosAlta, PermisosN.Secciones.Gastos) = False Then mnuGastosAltaEmp.Visible = False Else mnuGastosAltaEmp.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosProgramarVer, PermisosN.Secciones.Gastos) = False Then mnuGastosProg.Visible = False Else mnuGastosProg.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosReportesVer, PermisosN.Secciones.Gastos) = False Then mnuGastosReportes.Visible = False Else mnuGastosReportes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosVerNotificaciones, PermisosN.Secciones.Gastos) = False Then mnuGastosVerAlertas.Visible = False Else mnuGastosVerAlertas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosVer, PermisosN.Secciones.PuntodeVenta) = False Then mnuGastosMovCajas.Visible = False Else mnuGastosMovCajas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosVer, PermisosN.Secciones.PuntodeVenta) = False Then mnuMovimientosCaja.Visible = False Else mnuMovimientosCaja.Visible = True

        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosVer, PermisosN.Secciones.Empenios) = False Then mnuEmpenios.Visible = False Else mnuEmpenios.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosVer, PermisosN.Secciones.Empenios) = False Then toolEmpenios.Visible = False Else toolEmpenios.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosConfiguracionVer, PermisosN.Secciones.Empenios) = False Then mnuEmpeniosConfig.Visible = False Else mnuEmpeniosConfig.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosIdentificacionVer, PermisosN.Secciones.Empenios) = False Then mnuAltaIdentificacion.Visible = False Else mnuAltaIdentificacion.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosPagosVer, PermisosN.Secciones.Empenios) = False Then mnuEmpeniosPagos.Visible = False Else mnuEmpeniosPagos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosPagosVer, PermisosN.Secciones.Empenios) = False Then toolEmpeniosPagos.Visible = False Else toolEmpeniosPagos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosConsultaMovVer, PermisosN.Secciones.Empenios) = False Then mnuEmpeniosConsultaMov.Visible = False Else mnuEmpeniosConsultaMov.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosAdjudicacionesVer, PermisosN.Secciones.Empenios) = False Then mnuEmpeniosAdjudicaciones.Visible = False Else mnuEmpeniosAdjudicaciones.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosReportesVer, PermisosN.Secciones.Empenios) = False Then mnuEmpeniosReportes.Visible = False Else mnuEmpeniosReportes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosClasificacionesVer, PermisosN.Secciones.Empenios) = False Then mnuEmpeniosClas.Visible = False Else mnuEmpeniosClas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasVer, PermisosN.Secciones.Empenios) = False Then mnuEmpeniosCompras.Visible = False Else mnuEmpeniosCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasVer, PermisosN.Secciones.Empenios) = False Then toolEmpeniosCompras.Visible = False Else toolEmpeniosCompras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.VerCorte, PermisosN.Secciones.Empenios) = False Then mnuEmpeniosCorte.Visible = False Else mnuEmpeniosCorte.Visible = True

        If GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.PedidosVer, PermisosN.Secciones.Fertilizantes) = False Then mnuFertilizantesPedidos.Visible = False Else mnuFertilizantesPedidos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.PedidosVer, PermisosN.Secciones.Fertilizantes) = False Then toolFertilizantes.Visible = False Else toolFertilizantes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.PedidosVer, PermisosN.Secciones.Fertilizantes) = False Then mnuFertilizantesReportes.Visible = False Else mnuFertilizantesReportes.Visible = True

        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ConfiguracionVer, PermisosN.Secciones.Contabilidad) = False Then mnuContaConfig.Visible = False Else mnuContaConfig.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasVer, PermisosN.Secciones.Contabilidad) = False Then mnuContaClaspoli.Visible = False Else mnuContaClaspoli.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.CuentasVer, PermisosN.Secciones.Contabilidad) = False Then mnuCuentasContablesCon.Visible = False Else mnuCuentasContablesCon.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.PolizasVer, PermisosN.Secciones.Contabilidad) = False Then mnuPolizas.Visible = False Else mnuPolizas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.PolizasVer, PermisosN.Secciones.Contabilidad) = False Then toolpolizas.Visible = False Else toolpolizas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ConsultaSaldosVer, PermisosN.Secciones.Contabilidad) = False Then mnuContaConsultaSaldos.Visible = False Else mnuContaConsultaSaldos.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.PolizasVer, PermisosN.Secciones.Contabilidad) = False Then mnuConciliacionDiot.Visible = False Else mnuConciliacionDiot.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ReportesVer, PermisosN.Secciones.Contabilidad) = False Then mnuContaReportes.Visible = False Else mnuContaReportes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ReportesVer, PermisosN.Secciones.Contabilidad) = False Then toolContaReportes.Visible = False Else toolContaReportes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.MascarasVer, PermisosN.Secciones.Contabilidad) = False Then mnuContabilidadMascaras.Visible = False Else mnuContabilidadMascaras.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.GenerarPolizasPermitir, PermisosN.Secciones.Contabilidad) = False Then mnuContaGenerarpolizas.Visible = False Else mnuContaGenerarpolizas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.NominaConceptosVer, PermisosN.Secciones.Contabilidad) = False Then mnuContabilidadConceptosNomina.Visible = False Else mnuContabilidadConceptosNomina.Visible = True

        If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.BoletasVer, PermisosN.Secciones.Semillas) = False Then mnuSemillasBoletas.Visible = False Else mnuSemillasBoletas.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.LiquidacionVer, PermisosN.Secciones.Semillas) = False Then MnuSemillasLiquidaciones.Visible = False Else MnuSemillasLiquidaciones.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.ComprobanteVer, PermisosN.Secciones.Semillas) = False Then mnuSemillasComprobantes.Visible = False Else mnuSemillasComprobantes.Visible = True
        If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.ReportesVer, PermisosN.Secciones.Semillas) = False Then mnuSemillasReportes.Visible = False Else mnuSemillasReportes.Visible = True

        If O.BoletasInventario = 1 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.ReportesVer, PermisosN.Secciones.Semillas) = False Then mnuReportesBoletasInv.Visible = False Else mnuReportesBoletasInv.Visible = True
            If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.BoletasVer, PermisosN.Secciones.Semillas) = False Then mnuBoletasInventario.Visible = False Else mnuBoletasInventario.Visible = True
        Else
            mnuReportesBoletasInv.Visible = False
            mnuBoletasInventario.Visible = False
        End If
        If My.Settings.menusextendidos = False Then
            'mnuConsultarSolicitudes.Visible = False
            'mnuBuscarRequisiciones.Visible = False
            'mnuBuscarCotizacion.Visible = False
            mnuPedidosComprasConsulta.Visible = False
            mnuRemisionesComprasConsulta.Visible = False
            mnuVentaConsulta.Visible = False
            mnuVentasCotizacionConsulta.Visible = False
            mnuVentasPedidosConsulta.Visible = False
            mnuVentasRemisionConsulta.Visible = False
            mnuVentaConsulta.Visible = False
            mnuCompraConsulta.Visible = False
            mnuCotizacionesConsultaCompras.Visible = False
            'mnuDevolucionCompraConsulta.Visible = False
            mnuNotasdeCreditoComprasConsulta.Visible = False
            mnuNotasdeCargoComprasConsulta.Visible = False
            'mnuBuscarVentasDevoluciones.Visible = False
            mnuNotasdeCargoconsulta.Visible = False
            mnuNotasdeCreditoConsulta.Visible = False
            'mnuDevolucionCompraConsulta.Visible = True
            mnubarra01.Visible = False
            mnubarra02.Visible = False
            mnuBarra03.Visible = False
            mnuBarra04.Visible = False
            mnuBarra05.Visible = False
            mnuBarra07.Visible = False
            mnuBarra08.Visible = False
            mnuBarra09.Visible = False
            mnuBarra10.Visible = False
            mnuBarra11.Visible = False
            mnuBarra12.Visible = False
            mnubarra13.Visible = False
            MnuBarra14.Visible = False
            MnuBarra15.Visible = False
            mnuBarra16.Visible = False
            mnuBarra17.Visible = False
            mnuBarra18.Visible = False
            mnuBarra19.Visible = False
            mnuBarra20.Visible = False
        End If

        mnubarra02.Visible = False

    End Sub

    Private Sub mnuVentasPedidosConsulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentasPedidosConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasPedidosConsulta") = False Then
            Dim f As New frmVentasPedidosConsulta(ModosDeBusqueda.Principal)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuVentasCotizacionConsulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentasCotizacionConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasCotizacionesConsulta") = False Then
            Dim f As New frmVentasCotizacionesConsulta(ModosDeBusqueda.Principal)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub FormasDePagoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFormasdePago.Click

        CierraVentanas()
        If BuscaVentanas("frmFormasdePago") = False Then
            Dim f As New frmFormasDePago()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub SucursalesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSucursales.Click

        CierraVentanas()
        If BuscaVentanas("frmSucusales") = False Then
            Dim f As New frmSucursales()
            f.MdiParent = Me
            f.Show()
        End If


    End Sub
    Private Sub mnuZona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuZonas.Click

        CierraVentanas()
        If BuscaVentanas("frmZona") = False Then
            Dim f As New frmZona()
            f.MdiParent = Me
            f.Show()
        End If


    End Sub

    Private Sub ReportesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentasReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasReportesN") = False Then
            Dim f As New frmVentasReportesN()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ReporteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventarioReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioReportes") = False Then
            Dim f As New frmInventarioReportes()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ReporteMensualCFDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReporteMensual.Click
        CierraVentanas()
        If BuscaVentanas("frmReporteMensualCFD") = False Then
            Dim f As New frmReporteMensualCFD()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub VendedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VendedoresToolStripMenuItem.Click
        CierraVentanas()
        If BuscaVentanas("frmVendedores") = False Then
            Dim f As New frmVendedores
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolInventario.Click
        CierraVentanas()
        If BuscaVentanas("frmInventario") = False Then
            Dim f As New frmInventario
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolClientes.Click
        CierraVentanas()
        If BuscaVentanas("frmClientes") = False Then
            Dim f As New frmClientes(0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolVentas.Click
        CierraVentanas()
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If BuscaVentanas("frmVentasN") = False Then
            Dim Op As New dbOpciones(MySqlcon)
            If Op.MaximizarVentas = 0 Then
                Dim f As New frmVentasN(0, 0, 0, 0)
                f.MdiParent = Me
                f.Show()
            Else
                GlobalEstadoVentanas = GlobalEstadoVentanas Or 1
                'toolstrip1.Visible = False
                Dim f As New frmVentasN(0, 0, 0, 0)
                f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                f.WindowState = FormWindowState.Maximized
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolCompras.Click
        CierraVentanas()
        If BuscaVentanas("frmCompras") = False Then
            Dim f As New frmCompras
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuNotasdeCredito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNotasdeCredito.Click
        CierraVentanas()
        If BuscaVentanas("frmNotasdeCreditoN") = False Then
            Dim f As New frmNotasdeCreditoN("")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuNotasdeCreditoConsulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNotasdeCreditoConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmNotasdeCreditoConsulta") = False Then
            Dim f As New frmNotasDeCreditoConsulta(ModosDeBusqueda.Principal, "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub DevolucionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentasDev.Click
        Dim Forma As New frmBuscaDocumentoVenta(2, False, 1, GlobalIdSucursalDefault, 1, False, True, True, 0, True, "", True)
        Dim C As String
        If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If Forma.Tipo = 2 Then
                Dim Cr As New dbVentasRemisiones(Forma.id(0), MySqlcon)
                C = Cr.Cliente.Clave
            Else
                Dim Cv As New dbVentas(Forma.id(0), MySqlcon, "0")
                C = Cv.Cliente.Clave
            End If
            CierraVentanas()
            If BuscaVentanas("frmDevoluciones") = False Then
                Dim f As New frmDevoluciones(0, Forma.id(0), 1, C, Forma.Tipo)
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub BuscarDevoluciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentasDevConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmDevolucionesConsulta") = False Then
            Dim f As New frmDevolucionesConsulta(ModosDeBusqueda.Principal)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub ClientesMovimientosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientesMovimientosToolStripMenuItem.Click
        CierraVentanas()
        If BuscaVentanas("frmClientesMovimientos") = False Then
            Dim f As New frmClientesMovimientos(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuVentasMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVentasMenu.Click

    End Sub

    Private Sub NotasDeCargoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNotasdeCargo.Click
        CierraVentanas()
        If BuscaVentanas("frmNotasdeCargo") = False Then
            Dim f As New frmNotasdeCargo()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub BuscarNotaDeCargoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNotasdeCargoconsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmNotasdeCargoConsulta") = False Then
            Dim f As New frmNotasdeCargoConsulta(ModosDeBusqueda.Principal, "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuCotizacionesb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCotizacionesCompras.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasCotizacionesNB") = False Then
            Dim f As New frmComprasCotizacionesNB()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuCotizacionesconsultab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCotizacionesConsultaCompras.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasCotizacionesConsulta") = False Then
            Dim f As New frmComprasCotizacionesConsulta(ModosDeBusqueda.Principal, 1)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuComprasPagos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuComprasPagos.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasPagos") = False Then
            Dim f As New frmComprasPagos("", "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuDevolucionesCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDevolucionesCompras.Click
        Dim Forma As New frmBuscaDocumentoVenta(2, True, 0, GlobalIdSucursalDefault, 1, False, True, True, 0, True, "", True)
        Dim C As String
        If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If Forma.Tipo = 2 Then
                Dim Cr As New dbComprasRemisiones(Forma.id(0), MySqlcon)
                C = Cr.Proveedor.Clave
            Else
                Dim Cv As New dbCompras(Forma.id(0), MySqlcon)
                C = Cv.Proveedor.Clave
            End If
            CierraVentanas()
            If BuscaVentanas("frmDevolucionesCompras") = False Then
                Dim f As New frmDevolucionesCompras(0, Forma.id(0), 1, C, Forma.Tipo)
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub mnuBuscarDevolucionCompra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDevolucionCompraConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmDevolucionesComprasConsulta") = False Then
            Dim f As New frmDevolucionesComprasConsulta(ModosDeBusqueda.Principal)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuNotasdeCreditoCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNotasdeCreditoCompras.Click
        CierraVentanas()
        If BuscaVentanas("frmNotasdeComprasCreditoN") = False Then
            Dim f As New frmNotasdeCreditoComprasN("")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuBuscarNCreditoCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNotasdeCreditoComprasConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmNotasdeCreditoComprasConsulta") = False Then
            Dim f As New frmNotasdeCreditoComprasConsulta(ModosDeBusqueda.Principal, "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuNotasdeCargoCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNotasdeCargoCompras.Click
        CierraVentanas()
        If BuscaVentanas("frmNotasdeCargoC") = False Then
            Dim f As New frmNotasdeCargoC()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuBuscarNCargoCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNotasdeCargoComprasConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmNotasdeCargoComprasConsulta") = False Then
            Dim f As New frmNotasdeCargoComprasConsulta(ModosDeBusqueda.Principal, "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ProveedoresMovimientosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProveedoresMovimientosToolStripMenuItem.Click
        CierraVentanas()
        If BuscaVentanas("frmProveedoresMovimientos") = False Then
            Dim f As New frmProveedoresMovimientos(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ImpresionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDocDesing.Click
        CierraVentanas()
        If BuscaVentanas("frmImpresion") = False Then
            Dim f As New frmImpresion
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolPagosVentas.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasPagos") = False Then
            Dim f As New frmVentasPagos("", "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolNotasdeCredito.Click
        If GlobalTipoVersion <> 3 Then
            CierraVentanas()
            If BuscaVentanas("frmNotasdeCreditoN") = False Then
                Dim f As New frmNotasdeCreditoN("")
                f.MdiParent = Me
                f.Show()
            End If
        Else
            CierraVentanas()
            If BuscaVentanas("frmNotasdeCredito") = False Then
                Dim f As New frmNotasdeCredito(0)
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasReportesN") = False Then
            Dim f As New frmVentasReportesN()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ReportesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportesCompras.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasReportesN") = False Then
            Dim f As New frmComprasReportesN()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolInvConsulta.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioConsulta") = False Then
            Dim f As New frmInventarioConsulta(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuDocumentosVentas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDocumentosVentas.Click
        CierraVentanas()
        If BuscaVentanas("frmClientesDocumentos") = False Then
            Dim f As New frmClientesDocumentos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub DocumentosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDocumentosCompras.Click
        CierraVentanas()
        If BuscaVentanas("frmProveedoresDocumentos") = False Then
            Dim f As New frmProveedoresDocumentos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ReportesToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportesCatalagos.Click
        CierraVentanas()
        If BuscaVentanas("frmCatalogos") = False Then
            Dim f As New frmCatalogos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub ConcialiciónDeInventarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventarioRevision.Click
        CierraVentanas()
        If BuscaVentanas("frmConciliarInventario") = False Then
            Dim f As New frmConciliarInventario(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ContadorDeTimbresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContadorDeTimbresToolStripMenuItem.Click
        CierraVentanas()
        If BuscaVentanas("frmContadorTimbres") = False Then
            Dim f As New frmContadorTimbres
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub EmpresasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmpresas.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpresas") = False Then
            Dim f As New frmEmpresas
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuCambiarDeEmpresa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCambiarDeEmpresa.Click
        If MsgBox("¿Salir de esta empresa?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            CierraVentanas(True)
            MySqlcon.Close()
            Dim Fe As New frmEmpresasSelector()
            Fe.ChecaDefault = False
            Fe.Show()
            Salir = False
            Me.Close()
        End If
    End Sub

    Private Sub RespaldoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRespaldo.Click
        Dim sfd As New SaveFileDialog
        sfd.Filter = "Archivos SQL (.SQL)|*.SQL|Todos los archivos (*.*)|*.*"
        sfd.FileName = "RESP " + My.Application.Info.ProductName.ToUpper + " " + Format(Now, "yyyyMMdd HHmm") + ".SQL"
        sfd.InitialDirectory = Application.StartupPath + "\respaldos"
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Dim r As New Respaldo()
                Dim Em As New dbEmpresas
                Em.IniciarMySQLE(My.Settings.BasedeDatos, My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
                Em.LlenaDatos(GlobalIdEmpresa)
                r.backup(Em.Servidor, sfd.FileName, GlobalBasedeDatos, Em.PasswordS)
                'r.backup(My.Settings.servidor, sfd.FileName, "facturacion")
                Em.MySqlconE.Close()
                MsgBox("Respaldo exitoso!")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub RestaurarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRestaurar.Click
        If MsgBox("¿Seguro desea restaurar un respaldo de la información?" + vbNewLine + "(Se elminará cualquier cambio que no esté respaldado)", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim ofd As New OpenFileDialog
            ofd.Filter = "Archivos SQL (.SQL)|*.SQL|Todos los archivos (*.*)|*.*"
            ofd.InitialDirectory = Application.StartupPath + "\respaldos"
            If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    Dim r As New Respaldo()
                    Dim Em As New dbEmpresas
                    Em.IniciarMySQLE(My.Settings.BasedeDatos, My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
                    Em.LlenaDatos(GlobalIdEmpresa)
                    r.restore(ofd.FileName, Em.Servidor, GlobalBasedeDatos, Em.PasswordS)
                    Process.Start("explorer.exe", "C:\Windows\Temp\")
                    Em.MySqlconE.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub PagaréToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPagare.Click
        CierraVentanas()
        If BuscaVentanas("frmPagare") = False Then
            Dim f As New frmPagare
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    

    Private Sub CambioDePreciosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCambioPrecios.Click
        CierraVentanas()
        If BuscaVentanas("frmListaPreciosCambio") = False Then
            Dim f As New frmListaPreciosCambio()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim en As New Encriptador
        Dim Archivos() As String
        Dim Lineas() As String
        Dim ArchivoCompleto As String
        Dim ArchivoR As String = ""
        Dim ArchivoNombre As String = ""
        Dim MensajeError As String
        Dim C As Integer = 0
        Try
            Archivos = System.IO.Directory.GetFiles(GlobalRutaConector, "fe*.txt", IO.SearchOption.TopDirectoryOnly)
            If Archivos.Length = 0 Then
                Archivos = System.IO.Directory.GetFiles(GlobalRutaConector, "ce*.txt", IO.SearchOption.TopDirectoryOnly)
                If Archivos.Length = 0 Then
                    Archivos = System.IO.Directory.GetFiles(GlobalRutaConector, "na*.txt", IO.SearchOption.TopDirectoryOnly)
                    If Archivos.Length = 0 Then
                        Archivos = System.IO.Directory.GetFiles(GlobalRutaConector, "nc*.txt", IO.SearchOption.TopDirectoryOnly)
                        If Archivos.Length = 0 Then
                            Archivos = System.IO.Directory.GetFiles(GlobalRutaConector, "de*.txt", IO.SearchOption.TopDirectoryOnly)
                            If Archivos.Length = 0 Then
                                Archivos = System.IO.Directory.GetFiles(GlobalRutaConector, "dc*.txt", IO.SearchOption.TopDirectoryOnly)
                            End If
                        End If
                    End If
                End If
            End If
            'Timer1.Enabled = False
            'If Procesando = False Then
            Timer1.Enabled = False
            While C < Archivos.Length
                ArchivoR = Archivos(0)
                ArchivoNombre = ArchivoR.Substring(ArchivoR.LastIndexOf("\") + 1, ArchivoR.Length - ArchivoR.LastIndexOf("\") - 1)
                If ArchivoNombre.StartsWith("fe") Then

                    ArchivoCompleto = en.LeeArchivoTexto(ArchivoR, 1)
                    Lineas = ArchivoCompleto.Split(Chr(10))
                    If MySqlcon.State = ConnectionState.Closed Then
                        MySqlcon.Open()
                    End If

                    Dim CM As New ConectorMacro(Lineas, MySqlcon, VersionConector, Munrec)

                    If CM.ProcesaArchivo("FACTURA", GlobalIdSucursalDefault, SerieB, idSucursalB) = False Then
                        MensajeError = CM.MensajeError
                        'NotifyIcon1.Text = MensajeError
                        If MensajeError = "" Then MensajeError = "Posible error sin controlar."
                        If MensajeError = "Fatal error encountered during command execution." Then
                            Exit While
                        End If
                        NotifyIcon1.BalloonTipText = MensajeError
                        NotifyIcon1.ShowBalloonTip(2000)

                        If MensajeError.Contains("Factura ya procesada") Then
                            Estado = CM.Estado
                            idSucursalI = CM.IdSucursal
                            Dim O As New dbOpciones(MySqlcon)
                            ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1", O.TituloOriginalFactura)
                            If O.ActivarCopiaFactura = 1 Then
                                ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1", O.TituloCopiaFactura)
                            End If
                            If O.ActivarCopia2Factura = 1 Then
                                ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1", O.TituloCopia2Factura)
                            End If
                            If O._ActivarPDF = "1" Then
                                ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "1", O._MostrarPDF, O.TituloOriginalFactura)
                            End If
                            IO.File.Delete(RutaProcesados + ArchivoNombre)
                            System.IO.Directory.Move(ArchivoR, RutaProcesados + ArchivoNombre)
                            Exit While
                        Else
                            IO.File.Delete(GlobalRutaConector + "\Error\" + ArchivoNombre)
                            System.IO.Directory.Move(ArchivoR, GlobalRutaConector + "\Error\" + ArchivoNombre)
                            en.GuardaArchivoTexto(GlobalRutaConector + "\Error\Error.txt", MensajeError, System.Text.Encoding.Default)
                            'MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
                            Exit While
                        End If
                    Else
                        Estado = Estados.Guardada
                        idSucursalI = CM.IdSucursal
                        Dim O As New dbOpciones(MySqlcon)
                        ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1", O.TituloOriginalFactura)
                        If O.ActivarCopiaFactura = 1 Then
                            ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1", O.TituloCopiaFactura)
                        End If
                        If O.ActivarCopia2Factura = 1 Then
                            ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1", O.TituloCopia2Factura)
                        End If
                        If O._ActivarPDF = "1" Then
                            ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "1", O._MostrarPDF, O.TituloOriginalFactura)
                        End If
                        If EnviaCorreos = "1" Then Cenvios.Guardar(CM.IdDocumento, 0, GlobalLicenciaSTR)
                        IO.File.Delete(RutaProcesados + ArchivoNombre)
                        System.IO.Directory.Move(ArchivoR, RutaProcesados + ArchivoNombre)
                        NotifyIcon1.BalloonTipText = "Documento Procesado."
                        NotifyIcon1.ShowBalloonTip(2000)

                        Exit While
                    End If

                End If
                If ArchivoNombre.StartsWith("ce") Then
                    ArchivoCompleto = en.LeeArchivoTexto(ArchivoR, 1)
                    Lineas = ArchivoCompleto.Split("|")
                    Dim CM As New ConectorMacro(Lineas, MySqlcon, VersionConector, Munrec)
                    If CM.ProcesaCancelacion("CANCELARFACTURA") = True Then
                        Estado = Estados.Cancelada
                        idSucursalI = CM.IdSucursal
                        Dim O As New dbOpciones(MySqlcon)
                        ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1", O.TituloOriginalFactura)
                        If O.ActivarCopiaFactura = 1 Then
                            ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1", O.TituloCopiaFactura)
                        End If
                        If O.ActivarCopia2Factura = 1 Then
                            ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1", O.TituloCopia2Factura)
                        End If
                        If O._ActivarPDF = "1" Then
                            ImprimirFacturas(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "1", O._MostrarPDF, O.TituloOriginalFactura)
                        End If
                        If EnviaCorreos = "1" Then Cenvios.Guardar(CM.IdDocumento, 0, GlobalLicenciaSTR)
                        IO.File.Delete(RutaProcesados + ArchivoNombre)
                        System.IO.Directory.Move(ArchivoR, RutaProcesados + ArchivoNombre)
                        Exit While
                    Else
                        Estado = Estados.Inicio
                        NotifyIcon1.BalloonTipText = CM.MensajeError
                        MensajeError = CM.MensajeError
                        NotifyIcon1.ShowBalloonTip(2000)
                        IO.File.Delete(GlobalRutaConector + "\Error\" + ArchivoNombre)
                        System.IO.Directory.Move(ArchivoR, GlobalRutaConector + "\Error\" + ArchivoNombre)
                        en.GuardaArchivoTexto(GlobalRutaConector + "\Error\Error.txt", MensajeError, System.Text.Encoding.Default)
                        Exit While
                    End If
                    Exit While
                End If


                If ArchivoNombre.StartsWith("na") Then

                    ArchivoCompleto = en.LeeArchivoTexto(ArchivoR, 1)
                    Lineas = ArchivoCompleto.Split(Chr(10))
                    If MySqlcon.State = ConnectionState.Closed Then
                        MySqlcon.Open()
                    End If

                    Dim CM As New ConectorMacro(Lineas, MySqlcon, VersionConector, Munrec)

                    If CM.ProcesaArchivo("NOTACREDITO", GlobalIdSucursalDefault, SerieBNC, idSucursalB) = False Then
                        MensajeError = CM.MensajeError
                        'NotifyIcon1.Text = MensajeError
                        If MensajeError = "" Then MensajeError = "Posible error sin controlar."
                        If MensajeError = "Fatal error encountered during command execution." Then
                            Exit While
                        End If
                        NotifyIcon1.BalloonTipText = MensajeError
                        NotifyIcon1.ShowBalloonTip(2000)

                        If MensajeError.Contains("Nota ya procesada") Then
                            Estado = CM.Estado
                            idSucursalI = CM.IdSucursal
                            Dim O As New dbOpciones(MySqlcon)
                            ImprimirNotasCredito(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1")
                            If O._ActivarPDF = "1" Then
                                ImprimirNotasCredito(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "1", O._MostrarPDF)
                            End If
                            IO.File.Delete(RutaProcesados + ArchivoNombre)
                            System.IO.Directory.Move(ArchivoR, RutaProcesados + ArchivoNombre)
                            Exit While
                        Else
                            IO.File.Delete(GlobalRutaConector + "\Error\" + ArchivoNombre)
                            System.IO.Directory.Move(ArchivoR, GlobalRutaConector + "\Error\" + ArchivoNombre)
                            en.GuardaArchivoTexto(GlobalRutaConector + "\Error\Error.txt", MensajeError, System.Text.Encoding.Default)
                            'MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
                            Exit While
                        End If
                    Else
                        Estado = Estados.Guardada
                        idSucursalI = CM.IdSucursal
                        Dim O As New dbOpciones(MySqlcon)
                        ImprimirNotasCredito(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1")
                        If O._ActivarPDF = "1" Then
                            ImprimirNotasCredito(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "1", O._MostrarPDF)
                        End If
                        If EnviaCorreos = "1" Then Cenvios.Guardar(CM.IdDocumento, 1, GlobalLicenciaSTR)
                        IO.File.Delete(RutaProcesados + ArchivoNombre)
                        System.IO.Directory.Move(ArchivoR, RutaProcesados + ArchivoNombre)
                        NotifyIcon1.BalloonTipText = "Documento Procesado."
                        NotifyIcon1.ShowBalloonTip(2000)
                        Exit While
                    End If

                End If


                If ArchivoNombre.StartsWith("nc") Then
                    ArchivoCompleto = en.LeeArchivoTexto(ArchivoR, 1)
                    Lineas = ArchivoCompleto.Split("|")
                    Dim CM As New ConectorMacro(Lineas, MySqlcon, VersionConector, Munrec)
                    If CM.ProcesaCancelacion("CANCELARNOTACREDITO") = True Then
                        Estado = Estados.Cancelada
                        idSucursalI = CM.IdSucursal
                        Dim O As New dbOpciones(MySqlcon)
                        ImprimirNotasCredito(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1")
                        If O._ActivarPDF = "1" Then
                            ImprimirNotasCredito(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "1", O._MostrarPDF)
                        End If
                        If EnviaCorreos = "1" Then Cenvios.Guardar(CM.IdDocumento, 1, GlobalLicenciaSTR)
                        IO.File.Delete(RutaProcesados + ArchivoNombre)
                        System.IO.Directory.Move(ArchivoR, RutaProcesados + ArchivoNombre)
                        Exit While
                    Else
                        Estado = Estados.Inicio
                        NotifyIcon1.BalloonTipText = CM.MensajeError
                        MensajeError = CM.MensajeError
                        NotifyIcon1.ShowBalloonTip(2000)
                        IO.File.Delete(GlobalRutaConector + "\Error\" + ArchivoNombre)
                        System.IO.Directory.Move(ArchivoR, GlobalRutaConector + "\Error\" + ArchivoNombre)
                        en.GuardaArchivoTexto(GlobalRutaConector + "\Error\Error.txt", MensajeError, System.Text.Encoding.Default)
                        Exit While
                    End If
                    Exit While
                End If

                If ArchivoNombre.StartsWith("de") Then

                    ArchivoCompleto = en.LeeArchivoTexto(ArchivoR, 1)
                    Lineas = ArchivoCompleto.Split(Chr(10))
                    If MySqlcon.State = ConnectionState.Closed Then
                        MySqlcon.Open()
                    End If

                    Dim CM As New ConectorMacro(Lineas, MySqlcon, VersionConector, Munrec)

                    If CM.ProcesaArchivo("DEVOLUCION", GlobalIdSucursalDefault, SerieB, idSucursalB) = False Then
                        MensajeError = CM.MensajeError
                        'NotifyIcon1.Text = MensajeError
                        If MensajeError = "" Then MensajeError = "Posible error sin controlar."
                        If MensajeError = "Fatal error encountered during command execution." Then
                            Exit While
                        End If
                        NotifyIcon1.BalloonTipText = MensajeError
                        NotifyIcon1.ShowBalloonTip(2000)

                        If MensajeError.Contains("Devolución ya procesada") Then
                            Estado = CM.Estado
                            idSucursalI = CM.IdSucursal
                            Dim O As New dbOpciones(MySqlcon)
                            ImprimirDevoluciones(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1")
                            If O._ActivarPDF = "1" Then
                                ImprimirDevoluciones(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "1", O._MostrarPDF)
                            End If
                            IO.File.Delete(RutaProcesados + ArchivoNombre)
                            System.IO.Directory.Move(ArchivoR, RutaProcesados + ArchivoNombre)
                            Exit While
                        Else
                            IO.File.Delete(GlobalRutaConector + "\Error\" + ArchivoNombre)
                            System.IO.Directory.Move(ArchivoR, GlobalRutaConector + "\Error\" + ArchivoNombre)
                            en.GuardaArchivoTexto(GlobalRutaConector + "\Error\Error.txt", MensajeError, System.Text.Encoding.Default)
                            'MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
                            Exit While
                        End If
                    Else
                        Estado = Estados.Guardada
                        idSucursalI = CM.IdSucursal
                        Dim O As New dbOpciones(MySqlcon)
                        ImprimirDevoluciones(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1")
                        If O._ActivarPDF = "1" Then
                            ImprimirDevoluciones(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "1", O._MostrarPDF)
                        End If
                        If EnviaCorreos = "1" Then Cenvios.Guardar(CM.IdDocumento, 2, GlobalLicenciaSTR)
                        IO.File.Delete(RutaProcesados + ArchivoNombre)
                        System.IO.Directory.Move(ArchivoR, RutaProcesados + ArchivoNombre)
                        NotifyIcon1.BalloonTipText = "Documento Procesado."
                        NotifyIcon1.ShowBalloonTip(2000)

                        Exit While
                    End If

                End If

                If ArchivoNombre.StartsWith("dc") Then
                    ArchivoCompleto = en.LeeArchivoTexto(ArchivoR, 1)
                    Lineas = ArchivoCompleto.Split("|")
                    Dim CM As New ConectorMacro(Lineas, MySqlcon, VersionConector, Munrec)
                    If CM.ProcesaCancelacion("CANCELARDEVOLUCION") = True Then
                        Estado = Estados.Cancelada
                        idSucursalI = CM.IdSucursal
                        Dim O As New dbOpciones(MySqlcon)
                        ImprimirDevoluciones(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "0", "1")
                        If O._ActivarPDF = "1" Then
                            ImprimirDevoluciones(CM.IdDocumento, CM.RutaPDF, CM.Cadena, CM.Sello, "1", O._MostrarPDF)
                        End If
                        If EnviaCorreos = "1" Then Cenvios.Guardar(CM.IdDocumento, 2, GlobalLicenciaSTR)
                        IO.File.Delete(RutaProcesados + ArchivoNombre)
                        System.IO.Directory.Move(ArchivoR, RutaProcesados + ArchivoNombre)
                        Exit While
                    Else
                        Estado = Estados.Inicio
                        NotifyIcon1.BalloonTipText = CM.MensajeError
                        MensajeError = CM.MensajeError
                        NotifyIcon1.ShowBalloonTip(2000)
                        IO.File.Delete(GlobalRutaConector + "\Error\" + ArchivoNombre)
                        System.IO.Directory.Move(ArchivoR, GlobalRutaConector + "\Error\" + ArchivoNombre)
                        en.GuardaArchivoTexto(GlobalRutaConector + "\Error\Error.txt", MensajeError, System.Text.Encoding.Default)
                        Exit While
                    End If
                    Exit While
                End If
                'For c As Integer = 0 To Lineas.Length - 1
                '    TextBox1.Text += Lineas(c) + vbCrLf
                'Next
                C += 1
            End While
            'Procesando = False
            'End If
            'Timer1.Enabled = True
        Catch ex2 As MySql.Data.MySqlClient.MySqlException
            NotifyIcon1.BalloonTipText = ex2.Message
            NotifyIcon1.ShowBalloonTip(2500)
            If MySqlcon.State <> ConnectionState.Closed Then
                If ArchivoNombre <> "" Then
                    IO.File.Delete(GlobalRutaConector + "\Error\" + ArchivoNombre)
                    System.IO.Directory.Move(ArchivoR, GlobalRutaConector + "\Error\" + ArchivoNombre)
                    en.GuardaArchivoTexto(GlobalRutaConector + "\Error\Error.txt", ex2.Message, System.Text.Encoding.Default)
                End If
            End If
        Catch ex As Exception
            NotifyIcon1.BalloonTipText = ex.Message
            NotifyIcon1.ShowBalloonTip(2500)
            If ArchivoNombre <> "" Then
                IO.File.Delete(GlobalRutaConector + "\Error\" + ArchivoNombre)
                System.IO.Directory.Move(ArchivoR, GlobalRutaConector + "\Error\" + ArchivoNombre)
                en.GuardaArchivoTexto(GlobalRutaConector + "\Error\Error.txt", ex.Message, System.Text.Encoding.Default)
            End If
        Finally
            Timer1.Enabled = True
        End Try
    End Sub

    Private Sub ImprimirFacturas(ByVal pidVenta As Integer, ByVal pRutaPDF As String, ByVal pCadena As String, ByVal pSello As String, ByVal pEsPDF As String, ByVal pMostrarPDF As String, ByVal pTitulo As String)
        Dim V As New dbVentas(pidVenta, MySqlcon, "0")
        Try
            PrintDocument1.DocumentName = "PSSFACTURA-" + V.Serie + V.Folio.ToString
            Dim SA As New dbSucursalesArchivos
            Dim Impresora As String
            If pEsPDF = "0" Then
                Impresora = SA.DaImpresoraActiva(V.IdSucursal, GlobalIdEmpresa, True, 0, TiposDocumentos.Venta)
            Else
                Impresora = SA.DaImpresoraActiva(V.IdSucursal, GlobalIdEmpresa, True, 0, TiposDocumentos.PDF)
            End If
            TipoImpresora = SA.TipoImpresora
            If Impresora = "Bullzip PDF Printer" Then
                Dim obj As New Bullzip.PdfWriter.PdfSettings
                obj.Init()
                obj.PrinterName = Impresora
                obj.SetValue("Output", pRutaPDF + "\<docname>.pdf")
                obj.SetValue("ShowSettings", "never")
                If pMostrarPDF = "0" Then
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
            LlenaNodosImpresionVentas(pidVenta, pCadena, pSello, pTitulo)
            If TipoImpresora = 0 Then
                LlenaNodos(V.IdSucursal, TiposDocumentos.Venta)
            Else
                LlenaNodos(V.IdSucursal, TiposDocumentos.Venta + 1000)
            End If
            DocaImprimir = TiposDocumentos.Venta
            PrintDocument1.Print()

            'If V.ISR <> 0 Or V.IvaRetenido <> 0 Then
            '    If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            '        If TipoImpresora = 0 Then
            '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencion)
            '        Else
            '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
            '        End If
            '        LlenaNodosImpresionRet()
            '        DocAImprimir = 1
            '        PrintDocument1.DocumentName = "RetFac-" + V.Serie + V.Folio.ToString
            '        If Impresora = "Bullzip PDF Printer" Then
            '            Dim obj As New Bullzip.PdfWriter.PdfSettings
            '            obj.Init()
            '            obj.PrinterName = Impresora
            '            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            '            obj.SetValue("ShowSettings", "never")
            '            obj.SetValue("ShowPDF", "yes")
            '            obj.SetValue("ShowSaveAS", "nofile")
            '            obj.SetValue("ConfirmOverwrite", "no")
            '            obj.SetValue("Target", "printer")
            '            obj.WriteSettings()
            '        End If
            '        PrintDocument1.Print()
            '    End If
            'End If
        Catch ex As Exception

            NotifyIcon1.BalloonTipText = ex.Message
            NotifyIcon1.ShowBalloonTip(2000)
        End Try

    End Sub
    Private Sub ImprimirDevoluciones(ByVal pidVenta As Integer, ByVal pRutaPDF As String, ByVal pCadena As String, ByVal pSello As String, ByVal pEsPDF As String, ByVal pMostrarPDF As String)
        Dim V As New dbDevoluciones(pidVenta, MySqlcon)
        Try
            PrintDocument1.DocumentName = "PSSDEVOLUCION-" + V.Serie + V.Folio.ToString
            Dim SA As New dbSucursalesArchivos
            Dim Impresora As String
            If pEsPDF = "0" Then
                Impresora = SA.DaImpresoraActiva(V.IdSucursal, GlobalIdEmpresa, True, 0, TiposDocumentos.VentaDevolucion)
            Else
                Impresora = SA.DaImpresoraActiva(V.IdSucursal, GlobalIdEmpresa, True, 0, TiposDocumentos.PDF)
            End If
            TipoImpresora = SA.TipoImpresora
            If Impresora = "Bullzip PDF Printer" Then
                Dim obj As New Bullzip.PdfWriter.PdfSettings
                obj.Init()
                obj.PrinterName = Impresora
                obj.SetValue("Output", pRutaPDF + "\<docname>.pdf")
                obj.SetValue("ShowSettings", "never")
                If pMostrarPDF = "0" Then
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
            LlenaNodosImpresionDevoluciones(pidVenta, pCadena, pSello)
            If TipoImpresora = 0 Then
                LlenaNodos(V.IdSucursal, TiposDocumentos.VentaDevolucion)
            Else
                LlenaNodos(V.IdSucursal, TiposDocumentos.VentaDevolucion + 1000)
            End If
            DocaImprimir = TiposDocumentos.VentaDevolucion
            PrintDocument1.Print()
            'If V.ISR <> 0 Or V.IvaRetenido <> 0 Then
            '    If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            '        If TipoImpresora = 0 Then
            '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencion)
            '        Else
            '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
            '        End If
            '        LlenaNodosImpresionRet()
            '        DocAImprimir = 1
            '        PrintDocument1.DocumentName = "RetFac-" + V.Serie + V.Folio.ToString
            '        If Impresora = "Bullzip PDF Printer" Then
            '            Dim obj As New Bullzip.PdfWriter.PdfSettings
            '            obj.Init()
            '            obj.PrinterName = Impresora
            '            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            '            obj.SetValue("ShowSettings", "never")
            '            obj.SetValue("ShowPDF", "yes")
            '            obj.SetValue("ShowSaveAS", "nofile")
            '            obj.SetValue("ConfirmOverwrite", "no")
            '            obj.SetValue("Target", "printer")
            '            obj.WriteSettings()
            '        End If
            '        PrintDocument1.Print()
            '    End If
            'End If
        Catch ex As Exception
            NotifyIcon1.BalloonTipText = ex.Message
            NotifyIcon1.ShowBalloonTip(2000)
        End Try

    End Sub
    Private Sub ImprimirNotasCredito(ByVal pidNota As Integer, ByVal pRutaPDF As String, ByVal pCadena As String, ByVal pSello As String, ByVal pEsPDF As String, ByVal pMostrarPDF As String)
        Dim V As New dbNotasDeCredito(pidNota, MySqlcon)
        Try
            PrintDocument1.DocumentName = "PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString
            Dim SA As New dbSucursalesArchivos
            Dim Impresora As String
            If pEsPDF = "0" Then
                Impresora = SA.DaImpresoraActiva(V.IdSucursal, GlobalIdEmpresa, True, 0, TiposDocumentos.VentaNotadeCredito)
            Else
                Impresora = SA.DaImpresoraActiva(V.IdSucursal, GlobalIdEmpresa, True, 0, TiposDocumentos.PDF)
            End If
            TipoImpresora = SA.TipoImpresora
            If Impresora = "Bullzip PDF Printer" Then
                Dim obj As New Bullzip.PdfWriter.PdfSettings
                obj.Init()
                obj.PrinterName = Impresora
                obj.SetValue("Output", pRutaPDF + "\<docname>.pdf")
                obj.SetValue("ShowSettings", "never")
                If pMostrarPDF = "1" Then
                    obj.SetValue("ShowPDF", "yes")
                Else
                    obj.SetValue("ShowPDF", "no")
                End If
                obj.SetValue("ShowSaveAS", "nofile")
                obj.SetValue("ConfirmOverwrite", "no")
                obj.SetValue("Target", "printer")
                obj.WriteSettings()
            End If
            PrintDocument1.PrinterSettings.PrinterName = Impresora
            LlenaNodosImpresionNC(pidNota, pCadena, pSello)
            If TipoImpresora = 0 Then
                LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCredito)
            Else
                LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCredito + 1000)
            End If
            DocaImprimir = TiposDocumentos.VentaNotadeCredito
            PrintDocument1.Print()
            'If V.ISR <> 0 Or V.IvaRetenido <> 0 Then
            '    If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            '        If TipoImpresora = 0 Then
            '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencion)
            '        Else
            '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
            '        End If
            '        LlenaNodosImpresionRet()
            '        DocAImprimir = 1
            '        PrintDocument1.DocumentName = "RetFac-" + V.Serie + V.Folio.ToString
            '        If Impresora = "Bullzip PDF Printer" Then
            '            Dim obj As New Bullzip.PdfWriter.PdfSettings
            '            obj.Init()
            '            obj.PrinterName = Impresora
            '            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            '            obj.SetValue("ShowSettings", "never")
            '            obj.SetValue("ShowPDF", "yes")
            '            obj.SetValue("ShowSaveAS", "nofile")
            '            obj.SetValue("ConfirmOverwrite", "no")
            '            obj.SetValue("Target", "printer")
            '            obj.WriteSettings()
            '        End If
            '        PrintDocument1.Print()
            '    End If
            'End If
        Catch ex As Exception

            NotifyIcon1.BalloonTipText = ex.Message
            NotifyIcon1.ShowBalloonTip(2000)
        End Try

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


    Private Sub LlenaNodosImpresionVentas(ByVal pIdVenta As Integer, ByVal Cadena As String, ByVal Sello As String, ByVal pTitulo As String)
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
        Dim V As New dbVentas(pIdVenta, MySqlcon, "0")
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(pIdVenta, V.IdConversion, O._Sinnegativos, O._CalculoAlterno)
        V.DaDatosTimbrado(pIdVenta)
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
            ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.LugarExp + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")

            If V.uuid = "**No Timbrado**" Or V.uuid = "" Then
                ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.LugarExp + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugartimbrado")
            Else
                ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.LugarExp + " " + Replace(V.FechaTimbrado, "T", " "), 0), "lugartimbrado")
            End If
        Else
            If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
                ImpND.Add(New NodoImpresionN("", "lugarexp", Sucursal.Ciudad2, 0), "lugarexp")
                ImpND.Add(New NodoImpresionN("", "callerexp", Sucursal.Direccion2, 0), "callerexp")
                ImpND.Add(New NodoImpresionN("", "numeroexp", Sucursal.NoExterior2, 0), "numeroexp")
                ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")

                If V.uuid = "**No Timbrado**" Or V.uuid = "" Then
                    ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugartimbrado")
                Else
                    ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.FechaTimbrado, "T", " "), 0), "lugartimbrado")
                End If
            Else
                ImpND.Add(New NodoImpresionN("", "lugarexp", "", 0), "lugarexp")
                ImpND.Add(New NodoImpresionN("", "callerexp", "", 0), "callerexp")
                ImpND.Add(New NodoImpresionN("", "numeroexp", "", 0), "numeroexp")
                ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")

                If V.uuid = "**No Timbrado**" Or V.uuid = "" Then
                    ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugartimbrado")
                Else
                    ImpND.Add(New NodoImpresionN("", "lugartimbrado", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.FechaTimbrado, "T", " "), 0), "lugartimbrado")
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

        Dim CodigoBarras As iTextSharp.text.pdf.Barcode128 = New iTextSharp.text.pdf.Barcode128
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbVentasInventario(MySqlcon)
        DR = VI.ConsultaReader(pIdVenta, AgregaSeries, O._DetalleKits, 1, O._OrdenUbicacion, False)
        ImpNDD.Clear()
        CuantosRenglones = 0
        Dim Cont As Integer = 0
        While DR.Read
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


            If DR("iva") = 0 Then
                ImpNDD.Add(New NodoImpresionN("", "tieneiva", "", 0), "tieneiva" + Format(Cont, "000"))
            Else
                ImpNDD.Add(New NodoImpresionN("", "tieneiva", "*", 0), "tieneiva" + Format(Cont, "000"))
            End If
            ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "ubicacion", DR("ubicacion"), 0), "ubicacion" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "predial", DR("predial"), 0), "predial" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "extra", DR("extra"), 0), "extra" + Format(Cont, "000"))
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
                ImpNDD.Add(New NodoImpresionN("", "ieps", Format(DR("precio") * DR("IEPS") / 100, O._formatoImporte).PadLeft(O.EspacioImporte), 0), "ieps" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", Format(DR("precio") * DR("IVARetenido") / 100, O._formatoImporte).PadLeft(O.EspacioImporte), 0), "ivaRetenido" + Format(Cont, "000"))
            Else
                ImpNDD.Add(New NodoImpresionN("", "preciou", "", 0), "preciou" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "preciouiva", "", 0), "preciouiva" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ieps", "", 0), "ieps" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", "", 0), "ivaRetenido" + Format(Cont, "000"))
            End If
            If DR("precio") <> 0 Then
                ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importe" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(DR("precio") * (1 + DR("iva") / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))

            Else
                ImpNDD.Add(New NodoImpresionN("", "importe", "", 0), "importe" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "importeiva", "", 0), "importeiva" + Format(Cont, "000"))
            End If

            'ImpNDD.Add(New NodoImpresionN("", "ieps", DR("ieps"), 0), "ieps" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", DR("IVARetenido"), 0), "ivaRetenido" + Format(Cont, "000"))

            If DR("cantidad") <> 0 And DR("descuento") <> 0 Then
                Dim Desc As Double
                Desc = (DR("precio") / (1 - DR("descuento") / 100))
                TotalDescuento += Desc - DR("precio")
                ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(O.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - DR("precio"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "importesindesc", Format(Desc, O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "importesindesc" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "descuentocantuni", Format((Desc / DR("cantidad")) * (DR("descuento") / 100), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocantuni" + Format(Cont, "000"))
                'Vo = Vd / ( 1 - (Por/100))
            Else
                ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))

                ImpNDD.Add(New NodoImpresionN("", "descuentocantuni", "", 0), "descuentocantuni" + Format(Cont, "000"))
                If DR("cantidad") = 0 Then
                    ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "importesindesc", "", 0), "importesindesc" + Format(Cont, "000"))
                Else
                    ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(DR("precio") / DR("cantidadm"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "importesindesc", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importesindesc" + Format(Cont, "000"))
                End If
            End If

            CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()

        If V.PorSurtir = 0 Then
            ImpND.Add(New NodoImpresionN("", "porsurtir", "SURTIDO", 0), "porsurtir")
        Else
            ImpND.Add(New NodoImpresionN("", "porsurtir", "POR SURTIR", 0), "porsurtir")
        End If

        ImpND.Add(New NodoImpresionN("", "peso", Format(V.TotalPeso, "#,##0.00") + "Kg.", 0), "peso")
        ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtototal - V.Descuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")
        ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtototal + TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsindesc")
        ImpND.Add(New NodoImpresionN("", "totaldesc", Format(TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totaldesc")
        ImpND.Add(New NodoImpresionN("", "subtotalsinret", Format(V.Subtototal + V.TotalIva - V.Descuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsinret")
        ImpND.Add(New NodoImpresionN("", "totalcantidad", V.DaTotalCantidad(pIdVenta).ToString, 0), "totalcantidad")
        ImpND.Add(New NodoImpresionN("", "totalnegativos", Format(V.TotalNegativosSinIVA * -1, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totalnegativos")

        ImpND.Add(New NodoImpresionN("", "Totalieps", Format(V.TotalIEPS, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "Totalieps")
        ImpND.Add(New NodoImpresionN("", "TotalivaRetenido", Format(V.TotalIvaRetenidoConceptos, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "TotalivaRetenido")
        ImpND.Add(New NodoImpresionN("", "totalofertas", Format(V.TotalOfertas, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totalofertas")
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(pIdVenta)
        ImpNDDi.Clear()
        ImpNDi2.Clear()

        Dim IAnt As Double
        'If Ivas.Count < 2 Then QuitaIvaCero = False
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
        Dim TotalesXIVa As String = ""

        'For Each I As Double In Ivas
        '    If TotalesXIVa = "" Then
        '        TotalesXIVa = "Total Iva " + Format(I, "#0.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
        '    Else
        '        TotalesXIVa += vbCrLf + "Total Iva " + Format(I, "#0.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
        '    End If
        'Next

        For Each I As Double In Ivas
            If TotalesXIVa = "" Then
                If I <> 0 Then
                    TotalesXIVa = "SUBTOTAL GRAVADOS    " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
                Else
                    TotalesXIVa = "SUBTOTAL NO GRAVADOS " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
                End If
            Else
                If I <> 0 Then
                    TotalesXIVa += vbCrLf + "SUBTOTAL GRAVADOS    " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
                Else
                    TotalesXIVa += vbCrLf + "SUBTOTAL NO GRAVADOS " + Format(I, "#00.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
                End If
            End If
        Next

        ImpND.Add(New NodoImpresionN("", "totalxiva", TotalesXIVa, 0), "totalxiva")
        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And QuitaIvaCero Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If
        Cont = 0
        Dim IVAsconversion As String = ""
        For Each I As Double In Ivas
            'If I = 0 And QuitaIvaCero Then

            'Else
            ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            ImpNDi2.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            'End If
            If V.IdConversion = 2 Then
                IVAsconversion += "Iva " + Format(I, "#0.00") + "%: " + Format(IvasImporte(I.ToString) / V.TipodeCambio, O._formatoIva).PadLeft(O.EspacioIva) + vbCrLf
            Else
                IVAsconversion += "Iva " + Format(I, "#0.00") + "%: " + Format(IvasImporte(I.ToString) * V.TipodeCambio, O._formatoIva).PadLeft(O.EspacioIva) + vbCrLf
            End If

            Cont += 1
        Next

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
            ImpND.Add(New NodoImpresionN("", "Subtota C:", Format(V.Subtototal * V.TipodeCambio, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalcon")
        End If
        ImpND.Add(New NodoImpresionN("", "ivacon", IVAsconversion, 0), "ivacon")
        ImpND.Add(New NodoImpresionN("", "totaldesc2", Format(V.DescuentoG2, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totaldesc2")

        Dim f As New StringFunctions
        Dim CL As New CLetras
        'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion), 0), "totalletra")
        If V.TotalVenta >= 0 Then
            ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(Format(V.TotalVenta, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(Format(V.TotalVenta * -1, "0.00"), V.IdConversion, GlobalIdiomaLetras), 0), "totalletra")
        End If
        ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
        ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
        Dim FP As New dbFormasdePago(V.IdFormadePago, MySqlcon)
        If FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
            ImpND.Add(New NodoImpresionN("", "titulo", O.TituloParcialidad, 0), "titulo")
        Else
            ImpND.Add(New NodoImpresionN("", "titulo", O.TituloFactura, 0), "titulo")
        End If
        'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
        'ImpND.Add(New NodoImpresionN("", "metodopago", FP.Nombre, 0), "metodopago")
        'Else
        'ImpND.Add(New NodoImpresionN("", "metodopago", "No Identificado", 0), "metodopago")
        'End If
        If V.IdFormadePago <> 98 Then
            ImpND.Add(New NodoImpresionN("", "metodopago", FP.Nombre, 0), "metodopago")
            If FP.Tipo <> dbFormasdePago.Tipos.Parcialidad Then
                ImpND.Add(New NodoImpresionN("", "formadepago", "PAGO EN UNA SOLA EXHIBICIÓN", 0), "formadepago")
                ImpND.Add(New NodoImpresionN("", "folioorigen", "", 0), "folioorigen")
                ImpND.Add(New NodoImpresionN("", "serieorigen", "", 0), "serieorigen")
                ImpND.Add(New NodoImpresionN("", "montoorigen", "", 0), "montoorigen")
                ImpND.Add(New NodoImpresionN("", "fechaorigen", "", 0), "fechaorigen")
                ImpND.Add(New NodoImpresionN("", "uuiorigen", "", 0), "uuidorigen")
            Else
                V.ObtenerFacturaOriginal(V.IdVentaOrigen)
                ImpND.Add(New NodoImpresionN("", "formadepago", "PARCIALIDAD " + V.Parcialidad.ToString + " DE " + V.Parcialidades.ToString, 0), "formadepago")
                ImpND.Add(New NodoImpresionN("", "folioorigen", Format(V.FolioOrigen, "00000"), 0), "folioorigen")
                ImpND.Add(New NodoImpresionN("", "serieorigen", V.SerieOrigen, 0), "serieorigen")
                ImpND.Add(New NodoImpresionN("", "montoorigen", Format(V.MontoOrigen, O._formatoTotal), 0), "montoorigen")
                ImpND.Add(New NodoImpresionN("", "fechaorigen", V.FechaOrigen, 0), "fechaorigen")
                ImpND.Add(New NodoImpresionN("", "uuiorigen", V.FolioUUIDOrigen, 0), "uuidorigen")
            End If
        Else
            ImpND.Add(New NodoImpresionN("", "metodopago", "NO IDENTIFICADO", 0), "metodopago")
            ImpND.Add(New NodoImpresionN("", "formadepago", "PAGO EN " + V.Parcialidades.ToString + " PARCIALIDADES", 0), "formadepago")
            ImpND.Add(New NodoImpresionN("", "folioorigen", "", 0), "folioorigen")
            ImpND.Add(New NodoImpresionN("", "serieorigen", "", 0), "serieorigen")
            ImpND.Add(New NodoImpresionN("", "montoorigen", "", 0), "montoorigen")
            ImpND.Add(New NodoImpresionN("", "fechaorigen", "", 0), "fechaorigen")
            ImpND.Add(New NodoImpresionN("", "uuiorigen", "", 0), "uuidorigen")
        End If


        If FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
            ImpND.Add(New NodoImpresionN("", "condicionpago", "CONTADO", 0), "condicionpago")
            ImpND.Add(New NodoImpresionN("", "diascredito", "", 0), "diascredito")
            ImpND.Add(New NodoImpresionN("", "limitecredito", "", 0), "limitecredito")
        Else

            ImpND.Add(New NodoImpresionN("", "condicionpago", "CRÉDITO", 0), "condicionpago")
            ImpND.Add(New NodoImpresionN("", "diascredito", V.Cliente.CreditoDias.ToString + " Días.", 0), "diascredito")
            ImpND.Add(New NodoImpresionN("", "limitecredito", Format(DateAdd(DateInterval.Day, V.Cliente.CreditoDias, CDate(V.Fecha)), "yyyy-MM-dd"), 0), "limitecredito")
        End If

        If V.IdConversion = 2 Then
            ImpND.Add(New NodoImpresionN("", "leyendadolar", "", 0), "leyendadolar")
        Else
            ImpND.Add(New NodoImpresionN("", "leyendadolar", "El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar a la venta frente al peso vigente al día de su pago.", 0), "leyendadolar")
        End If
        ImpND.Add(New NodoImpresionN("", "implocales", "", 0), "implocales")
        ImpND.Add(New NodoImpresionN("", "Total:", "", 0), "totalsil")
        ImpND.Add(New NodoImpresionN("", "totalletrasil", "", 0), "totalletrasil")
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

        Posicion = 0
        Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        CodigoBidimensional = CB.Encode("?&re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, O._formatoTotal) + "&id=" + V.uuid, System.Text.Encoding.Default)
        NumeroPagina = 1
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        If DocaImprimir = TiposDocumentos.Venta Or DocaImprimir = TiposDocumentos.VentaNotadeCredito Or DocaImprimir = TiposDocumentos.VentaDevolucion Then
            If DocaImprimir = TiposDocumentos.Venta Then DibujaPaginaNVentas(e.Graphics)
            If DocaImprimir = TiposDocumentos.VentaNotadeCredito Then DibujaPaginaNNC(e.Graphics)
            If DocaImprimir = TiposDocumentos.VentaDevolucion Then DibujaPaginaNDev(e.Graphics)
            If MasPaginas = True Or NumeroPagina > 2 Then
                'e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00") + "/" + Format(CuantaY, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
                e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
            End If
            'If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
            '    e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
            'End If
            If Estado = Estados.Cancelada Then
                e.Graphics.DrawString("CANCELADA", New Font("Arial", 18, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, 80, 130)
            End If
            e.HasMorePages = MasPaginas
            'DibujaPagina(e.Graphics)
            'e.HasMorePages = True
            'End If
        End If
    End Sub

    Private Sub DibujaPaginaNVentas(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.Venta, idSucursalI)
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.Venta + 1000, idSucursalI)
        End If
        If TipoImpresora = 0 Then
            DibujaPaginaEstaticoVentas(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujoVentas(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub
    Private Sub DibujaPaginaNDev(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.VentaDevolucion, idSucursalI)
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.VentaDevolucion + 1000, idSucursalI)
        End If
        If TipoImpresora = 0 Then
            DibujaPaginaEstaticoDEV(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujoDEV(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub
    Private Sub DibujaPaginaNNC(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.VentaNotadeCredito, idSucursalI)
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.VentaNotadeCredito + 1000, idSucursalI)
        End If
        If TipoImpresora = 0 Then
            DibujaPaginaEstaticoNC(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujoNC(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub
    Private Sub DibujaPaginaEstaticoVentas(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)
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
            If DocaImprimir = 0 Then
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Venta, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Venta + 1000, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            Else
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion + 1000, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                    Else
                                        Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    End If
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
                            e.DrawImage(Nimp.Imagen, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra

            '/***********************
            Dim HayRenglon As Boolean = False

            YExtra = 0
            YExtra2 = 0

            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                    Else
                                        Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    End If
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
                            e.DrawImage(Nimp.Imagen, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
                    End Select
                End If
            Next
            If HayRenglon Then YCoord = YCoord + 4 + YExtra
            '************************

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
    Private Sub DibujaPaginaFlujoVentas(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
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
            If DocaImprimir = 0 Then
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Venta, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Venta + 1000, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            Else
                If TipoImpresora = 0 Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                Else
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion + 1000, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


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
            If YCoord >= LimY And Posicion > 0 And pModo = 0 Then
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
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                    Else
                                        Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    End If
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
                            e.DrawImage(Nimp.Imagen, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra
            '***************************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0

            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                    Else
                                        Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    End If
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
                            e.DrawImage(Nimp.Imagen, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
                    End Select
                End If
            Next
            If HayRenglon Then YCoord = YCoord + 4 + YExtra


            '****************************
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

    Private Sub ActivarConectorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActivarConectorToolStripMenuItem.Click
        If Timer1.Enabled = False Then
            Timer1.Enabled = True
            Timer2.Enabled = True
            ActivarConectorToolStripMenuItem.Checked = True
            'NotifyIcon1.Visible = True
        Else
            Timer1.Enabled = False
            Timer2.Enabled = False
            'NotifyIcon1.Visible = False
            ActivarConectorToolStripMenuItem.Checked = False
            'Me.ShowInTaskbar = True
        End If
    End Sub

    Private Sub SalirToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem1.Click
        Me.Close()
    End Sub

    Private Sub MaximizarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaximizarToolStripMenuItem.Click
        Me.Show()
        Me.ShowInTaskbar = True
    End Sub

    Private Sub MinimizarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinimizarToolStripMenuItem.Click
        Me.Hide()
        Me.ShowInTaskbar = False
    End Sub

    Private Sub VentasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPuntodeVentaVentas.Click
        CierraVentanas()
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If BuscaVentanas("frmPuntodeVenta") = False Then
            toolstrip1.Visible = False
            GlobalEstadoPuntodeVenta = "Abierto"
            Dim O As New dbOpciones(MySqlcon)
            Dim RutaImagen As String
            Dim sa As New dbSucursalesArchivos
            Dim U As New dbUsuarios(GlobalIdUsuario, MySqlcon)
            sa.DaOpciones(GlobalIdEmpresa, False)
            RutaImagen = sa.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, 249, True)
            Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim Al As New dbAlmacenes(s.idAlmacen, MySqlcon)
            Dim f As New frmPuntodeVenta(sa.Documentopv, U.IdVendedor, GlobalIdSucursalDefault, s.idAlmacen, O.IdClienteDefault, sa.idCaja, RutaImagen, Al.Nombre)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub
    Private Sub LlenaNodosImpresionNC(ByVal pIdNota As Integer, ByVal Cadena As String, ByVal Sello As String)
        Dim O As New dbOpciones(MySqlcon)
        Dim V As New dbNotasDeCredito(pIdNota, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(pIdNota, V.IdMoneda)
        V.DaDatosTimbrado(pIdNota)
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
        ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
        ImpND.Add(New NodoImpresionN("", "regimenfiscal", Sucursal.RegimenFiscal, 0), "regimenfiscal")
        ImpND.Add(New NodoImpresionN("", "nocuenta", "", 0), "nocuenta")
        If V.Cliente.DireccionFiscal = 0 Then
            ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
            ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
            ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
            ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
            ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
            ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
            ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
            ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
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
            ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
        End If
        ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")
        ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")


        ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")
        ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
        ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")
        'If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
        '    ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'Else
        '    ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'End If

        Sucursal.LlenaExp(V.ID, 1)
        If Sucursal.HayExp Then
            ImpND.Add(New NodoImpresionN("", "lugarexp", Sucursal.LugarExp, 0), "lugarexp")
            ImpND.Add(New NodoImpresionN("", "callerexp", Sucursal.CalleExp, 0), "callerexp")
            ImpND.Add(New NodoImpresionN("", "numeroexp", Sucursal.NumExp, 0), "numeroexp")
            ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.LugarExp + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        Else
            If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
                ImpND.Add(New NodoImpresionN("", "lugarexp", Sucursal.Ciudad2, 0), "lugarexp")
                ImpND.Add(New NodoImpresionN("", "callerexp", Sucursal.Direccion2, 0), "callerexp")
                ImpND.Add(New NodoImpresionN("", "numeroexp", Sucursal.NoExterior2, 0), "numeroexp")
                ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
            Else
                ImpND.Add(New NodoImpresionN("", "lugarexp", "", 0), "lugarexp")
                ImpND.Add(New NodoImpresionN("", "callerexp", "", 0), "callerexp")
                ImpND.Add(New NodoImpresionN("", "numeroexp", "", 0), "numeroexp")
                ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
            End If
        End If


        CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
        ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")


        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
        DR = VI.ConsultaReader(pIdNota)
        ImpNDD.Clear()
        CuantosRenglones = 0
        Dim Cont As Integer = 0
        While DR.Read
            'ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            'If DR("idinventario") = 0 Then
            '    ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Factura: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
            'Else
            '    ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Nota de Cargo: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
            'End If
            If DR("folioventa") <> 0 Then
                If DR("idinventario") = 0 Then ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Factura: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
                If DR("idinventario") = 1 Then ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Nota de Cargo: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
                If DR("idinventario") = 2 Then ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Saldo Inicial: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
                If DR("idinventario") = 3 Then ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Documento: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
            Else
                If DR("idinventario") = 0 Then ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
                If DR("idinventario") = 1 Then ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
                If DR("idinventario") = 2 Then ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
                If DR("idinventario") = 3 Then ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            End If
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), 0), "cantidad" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "preciou", Format((DR("precio") / (1 + (DR("iva") / 100))) / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "importe", Format((DR("precio") / (1 + (DR("iva") / 100))), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importe" + Format(Cont, "000"))
            CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()


        'ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal - V.TotalIva, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")

        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(pIdNota)
        ImpNDDi.Clear()
        Dim IAnt As Double
        '(Precio / (1 + (iIva / 100)))
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                IvasImporte.Add(DR("precio") - (DR("precio") / (1 + ((DR("iva") / 100)))), DR("iva").ToString)
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                IvasImporte.Add(IAnt + (DR("precio") - ((DR("precio") / (1 + ((DR("iva") / 100)))))), DR("iva").ToString)
            End If
        End While
        DR.Close()
        Cont = 0
        For Each I As Double In Ivas
            ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
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
        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And O._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If
        ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalNota, O._formatoTotal).PadLeft(O.Espaciototal), 0), "total")

        Dim f As New StringFunctions
        Dim CL As New CLetras
        'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalNota, 2), V.IdMoneda), 0), "totalletra")
        If V.TotalNota >= 0 Then
            ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(V.TotalNota, V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(V.TotalNota * -1, V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
        End If
        ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
        ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
        Posicion = 0
        Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        CodigoBidimensional = CB.Encode("?&re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalNota, O._formatoTotal) + "&id=" + V.uuid, System.Text.Encoding.Default)
        NumeroPagina = 1
    End Sub
    Private Sub DibujaPaginaEstaticoNC(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)
        'On Error Resume Next
        Dim Nimp As NodoImpresionN
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
            ' If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaNotadeCredito, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaNotadeCredito + 1000, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
            'Else
            'If TipoImpresora = 0 Then
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'Else
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'End If
            'End If
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
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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

            '*******************************
            Dim Hayrenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0

            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    Hayrenglon = True
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
            If Hayrenglon Then YCoord = YCoord + 4 + YExtra

            '********************************

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
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
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
                If niva.Visible = 1 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta = 1 Then
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

                        If niva2.ConEtiqueta = 1 Then
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
    Private Sub DibujaPaginaFlujoNC(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
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
            'If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaNotadeCredito, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaNotadeCredito + 1000, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
            'Else
            'If TipoImpresora = 0 Then
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'Else
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'End If
            'End If
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
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
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

                        If niva.ConEtiqueta = 1 Then
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

                        If niva2.ConEtiqueta = 1 Then
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
            If YCoord >= LimY And Posicion > 0 And pModo = 0 Then
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
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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

            '**************************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0

            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
            If HayRenglon Then YCoord = YCoord + 4 + YExtra
            '************************
            Posicion += 1
            C += 1
        End While


        'Nodos fijos final
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
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
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

                        If niva.ConEtiqueta = 1 Then
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

                        If niva2.ConEtiqueta = 1 Then
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

    Private Sub mnuCajas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCajas.Click
        CierraVentanas()
        If BuscaVentanas("frmCajas") = False Then
            Dim f As New frmCajas
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub MovimientosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MovimientosToolStripMenuItem.Click

    End Sub

    Private Sub mnuPuntodeVentaReportes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPuntodeVentaReportes.Click

        CierraVentanas()
        If BuscaVentanas("frmPuntodeVentaReportes") = False Then
            Dim f As New frmPuntodeVentaReportes
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub MostrarBarraDeHerramientasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MostrarBarraDeHerramientasToolStripMenuItem.Click
        toolstrip1.Visible = True
    End Sub

    Private Sub FormasDePagoRemisionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFormasdePagorem.Click
        CierraVentanas()
        If BuscaVentanas("frmFormasdePagoRemisiones") = False Then
            Dim f As New frmFormasdePagoRemisiones()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolPuntodeVenta.Click
        CierraVentanas()
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If BuscaVentanas("frmPuntodeVenta") = False Then
            GlobalEstadoPuntodeVenta = "Abierto"
            toolstrip1.Visible = False
            Dim O As New dbOpciones(MySqlcon)
            Dim RutaImagen As String
            Dim sa As New dbSucursalesArchivos
            Dim U As New dbUsuarios(GlobalIdUsuario, MySqlcon)
            sa.DaOpciones(GlobalIdEmpresa, False)
            RutaImagen = sa.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, 249, True)
            Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim Al As New dbAlmacenes(s.idAlmacen, MySqlcon)
            Dim f As New frmPuntodeVenta(sa.Documentopv, U.IdVendedor, GlobalIdSucursalDefault, s.idAlmacen, O.IdClienteDefault, sa.idCaja, RutaImagen, Al.Nombre)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub RecalcularCostosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInventarioRecalcular.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioRCostos") = False Then
            Dim f As New frmInventarioRCostos(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ClientesHistorialDeVentaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientesHistorialDeVentaToolStripMenuItem.Click
        'CierraVentanas()
        If BuscaVentanas("frmClientesConsultaArticulos") = False Then
            Dim f As New frmClientesConsultaArticulos(0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub PagosRemisionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPagosRem.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasPagosRemisiones") = False Then
            Dim f As New frmVentasPagosRemisiones("", 0, 0, "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub CambioDeUsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CambioDeUsuarioToolStripMenuItem.Click
        CierraVentanas(True)
        Dim f As New frmCambioUsuario(0, 0)
        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            ChecaMenus()
            Versiones()
            ToolStripLabel1.Text = "Empresa: " + GlobalNombreEmpresa + vbCrLf + "Usuario: " + GlobalUsuario
        End If

    End Sub

    Private Sub frmPrincipal_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MdiChildActivate
        If GlobalEstadoPuntodeVenta = "Cerrado" And GlobalEstadoVentanas = 0 Then
            toolstrip1.Visible = True
        End If

    End Sub

    Private Sub LicenciasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLicencias.Click
        CierraVentanas()
        If BuscaVentanas("frmLicencias") = False Then
            Dim f As New frmLicencias()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub BuscarNegativosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBuscarNegativos.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioAnalisis") = False Then
            Dim f As New frmInventarioAnalisis(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuImportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportar.Click
        CierraVentanas()
        If BuscaVentanas("frmImportador") = False Then
            Dim f As New frmImportador()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub toolRemisiones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolRemisiones.Click
        CierraVentanas()
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If BuscaVentanas("frmVentasRemisiones") = False Then
            Dim Op As New dbOpciones(MySqlcon)
            If Op.MaximizarVentas = 0 Then
                Dim f As New frmVentasRemisiones()
                f.MdiParent = Me
                f.Show()
            Else
                GlobalEstadoVentanas = GlobalEstadoVentanas Or 8
                'toolstrip1.Visible = False
                Dim f As New frmVentasRemisiones()
                f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                f.WindowState = FormWindowState.Maximized
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub toolPagosRemisiones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolPagosRemisiones.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasPagosRemisiones") = False Then
            Dim f As New frmVentasPagosRemisiones("", 0, 0, "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuMovimientosCaja_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMovimientosCaja.Click
        CierraVentanas()
        If BuscaVentanas("frmCajasMovimientos") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmCajasMovimientos(0, sa.idCaja, 0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuApartados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuApartados.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasApartados") = False Then
            Dim f As New frmVentasApartados(0, 0, 0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuApartadosConsulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuApartadosConsulta.Click
        Dim f As New frmVentasApartadosConsulta(ModosDeBusqueda.Principal, 1)
        f.ShowDialog()
    End Sub

    Private Sub ToolApartados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolApartados.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasApartados") = False Then
            Dim f As New frmVentasApartados(0, 0, 0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuRecalcularInventario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRecalcularInventario.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioRCostos") = False Then
            Dim f As New frmInventarioRCostos(1)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ModelosToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuModelos.Click
        CierraVentanas()
        If BuscaVentanas("frmEstilosTallasColores") = False Then
            Dim f As New frmEstilosTallasColores(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuTallas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTallas.Click
        CierraVentanas()
        If BuscaVentanas("frmEstilosTallasColores") = False Then
            Dim f As New frmEstilosTallasColores(1)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuColores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColores.Click
        CierraVentanas()
        If BuscaVentanas("frmEstilosTallasColores") = False Then
            Dim f As New frmEstilosTallasColores(2)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub RegistroDeBancosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmRegistroBancos") = False Then
            Dim f As New frmRegistroBancos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub RegistroDeCuentasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmRegistroCuentas") = False Then
            Dim f As New frmRegistroCuentas()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub DepositosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDepositos.Click
        CierraVentanas()
        If BuscaVentanas("frmDeposito") = False Then
            Dim f As New frmDeposito(0, "", "", "", "", 0, "", "", 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub PagoAProveedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPagoProveedores.Click
        CierraVentanas()
        If BuscaVentanas("frmPagosProveedores") = False Then
            Dim f As New frmPagosProveedores(0, "", "", "", "", 0, "", "", 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ConsiliaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConciliacion.Click
        CierraVentanas()
        If BuscaVentanas("frmConciliacion") = False Then
            Dim f As New frmBancosConciliacion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ReportesToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportesBancos.Click
        CierraVentanas()
        If BuscaVentanas("frmReportesBancos") = False Then
            Dim f As New frmReportesBancos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Function EnviarCorreo(ByVal pidVenta As String, ByVal pRutaXML As String, ByVal pRutaPDF As String, ByVal pTitulo As String) As Boolean
        Dim Resultado As Boolean = False
        Try
            'If MsgBox("¿Enviar factura por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim V As New dbVentas(pidVenta, MySqlcon2, "0")
            Dim RutaxmlTimbrado As String
            Dim RutaXmlTimbradob As String
            Dim RutaXml As String
            V.DaDatosTimbrado(V.ID)
            RutaxmlTimbrado = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXmlTimbradob = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXml = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            pRutaPDF = pRutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\" + "PSSFACTURA-" + V.Serie + V.Folio.ToString + ".pdf"
            If V.Cliente.Email <> "" Then
                Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon2)
                Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                Dim O As New dbOpciones(MySqlcon2)
                Dim C As String
                C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + pTitulo + vbNewLine + "Folio: " + V.uuid + vbNewLine + vbNewLine
                C += O.CorreoContenido
                If V.EsElectronica >= 2 Then
                    If IO.File.Exists(pRutaPDF) Then
                        If GlobalConector = 0 Then
                            M.send("Comprobante Fiscal Digital por Internet " + pTitulo + ": " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaXml)
                            Cenvios.MarcarEnviado(pidVenta, 0, GlobalLicenciaSTR)
                        Else
                            If IO.File.Exists(RutaxmlTimbrado) Then
                                M.send("Comprobante Fiscal Digital por Internet " + pTitulo + ": " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaxmlTimbrado)
                                Cenvios.MarcarEnviado(pidVenta, 0, GlobalLicenciaSTR)
                            Else
                                M.send("Comprobante Fiscal Digital por Internet " + pTitulo + ": " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaXmlTimbradob)
                                Cenvios.MarcarEnviado(pidVenta, 0, GlobalLicenciaSTR)
                            End If
                        End If
                        Resultado = True
                    Else
                        Resultado = False
                    End If
                Else
                    'CFD
                    RutaXml = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\FAC-" + Trim(V.Serie) + V.Folio.ToString + ".xml"
                    C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + pTitulo + vbNewLine + "Folio: " + V.Serie + Format(V.Folio, "0000") + vbNewLine + "Comprobante fiscal digital enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
                    If IO.File.Exists(pRutaPDF) Then
                        M.send("Comprobante Fiscal " + pTitulo + ": " + V.Serie + Format(V.Folio, "0000"), C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaXml)
                        Cenvios.MarcarEnviado(pidVenta, 0, GlobalLicenciaSTR)
                        Resultado = True
                    Else
                        Resultado = False
                    End If
                End If
            Else
                Resultado = True
            End If
            'End If
            If Resultado Then
                If V.Cliente.Email <> "" Then
                    NotifyIcon1.BalloonTipText = "Correo enviado: Factura: " + V.Serie + Format(V.Folio, "0000")
                Else
                    'NotifyIcon1.BalloonTipText = "Cliente sin correo"
                    Cenvios.MarcarEnviado(pidVenta, 0, GlobalLicenciaSTR)
                End If
                NotifyIcon1.ShowBalloonTip(2500)
            Else
                Cenvios.Intentos += 1
                If Cenvios.Intentos >= 2 Then
                    Cenvios.MarcarEnviado(pidVenta, 0, GlobalLicenciaSTR)
                End If
            End If
            Return Resultado
        Catch ex As Exception
            Cenvios.Intentos += 1
            If Cenvios.Intentos >= 2 Then
                Cenvios.MarcarEnviado(pidVenta, 0, GlobalLicenciaSTR)
            End If
            NotifyIcon1.BalloonTipText = "No se pudo enviar el correo. " + ex.Message
            NotifyIcon1.ShowBalloonTip(2500)
            Return False
            'MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Function
    Private Function EnviarCorreoNC(ByVal pidVenta As String, ByVal pRutaXML As String, ByVal pRutaPDF As String, ByVal pTitulo As String) As Boolean
        Dim Resultado As Boolean = False
        Try
            'If MsgBox("¿Enviar factura por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim V As New dbNotasDeCredito(pidVenta, MySqlcon2)
            Dim RutaxmlTimbrado As String
            Dim RutaXmlTimbradob As String
            Dim RutaXml As String
            V.DaDatosTimbrado(V.ID)
            RutaxmlTimbrado = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXmlTimbradob = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXml = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"
            'If V.EsElectronica >= 2 Then
            'pRutaPDF = pRutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PDFNCCFDI" + V.Serie + V.Folio.ToString + ".pdf"
            'Else
            pRutaPDF = pRutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".pdf"
            'End If
            If V.Cliente.Email <> "" Then
                Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                Dim O As New dbOpciones(MySqlcon2)
                Dim C As String
                C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + pTitulo + vbNewLine + "Folio: " + V.uuid + vbNewLine + vbNewLine
                C += O.CorreoContenido
                If V.EsElectronica >= 2 Then
                    If IO.File.Exists(pRutaPDF) Then
                        If GlobalConector = 0 Then
                            M.send("Comprobante Fiscal Digital por Internet " + pTitulo + ": " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaXml)
                            Cenvios.MarcarEnviado(pidVenta, 1, GlobalLicenciaSTR)
                        Else
                            If IO.File.Exists(RutaxmlTimbrado) Then
                                M.send("Comprobante Fiscal Digital por Internet " + pTitulo + ": " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaxmlTimbrado)
                                Cenvios.MarcarEnviado(pidVenta, 1, GlobalLicenciaSTR)
                            Else
                                M.send("Comprobante Fiscal Digital por Internet " + pTitulo + ": " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaXmlTimbradob)
                                Cenvios.MarcarEnviado(pidVenta, 1, GlobalLicenciaSTR)
                            End If
                        End If
                        Resultado = True
                    Else
                        Resultado = False
                    End If
                Else
                    'CFD
                    RutaXml = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"

                    C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + pTitulo + vbNewLine + "Folio: " + V.Serie + Format(V.Folio, "0000") + vbNewLine + "Comprobante fiscal digital enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
                    If IO.File.Exists(pRutaPDF) Then
                        M.send("Comprobante Fiscal " + pTitulo + ": " + V.Serie + Format(V.Folio, "0000"), C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaXml)
                        Cenvios.MarcarEnviado(pidVenta, 1, GlobalLicenciaSTR)
                        Resultado = True
                    Else
                        Resultado = False
                    End If
                End If
            Else
                Resultado = True
            End If
            'End If
            If Resultado Then
                If V.Cliente.Email <> "" Then
                    NotifyIcon1.BalloonTipText = "Correo enviado: NC: " + V.Serie + Format(V.Folio, "0000")
                Else
                    'NotifyIcon1.BalloonTipText = "Cliente sin correo"
                    Cenvios.MarcarEnviado(pidVenta, 1, GlobalLicenciaSTR)
                End If
                NotifyIcon1.ShowBalloonTip(2500)
            Else
                Cenvios.Intentos += 1
                If Cenvios.Intentos >= 5 Then
                    Cenvios.MarcarEnviado(pidVenta, 1, GlobalLicenciaSTR)
                End If
            End If
            Return Resultado
        Catch ex As Exception
            Cenvios.Intentos += 1
            If Cenvios.Intentos >= 5 Then
                Cenvios.MarcarEnviado(pidVenta, 1, GlobalLicenciaSTR)
            End If
            NotifyIcon1.BalloonTipText = "No se pudo enviar el correo. " + ex.Message
            NotifyIcon1.ShowBalloonTip(2500)
            Return False
            'MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Function
    Private Function EnviarCorreoDEV(ByVal pidVenta As String, ByVal pRutaXML As String, ByVal pRutaPDF As String, ByVal pTitulo As String) As Boolean
        Dim Resultado As Boolean = False
        Try
            'If MsgBox("¿Enviar factura por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim V As New dbDevoluciones(pidVenta, MySqlcon2)
            Dim RutaxmlTimbrado As String
            Dim RutaXmlTimbradob As String
            Dim RutaXml As String
            V.DaDatosTimbrado(V.ID)
            RutaxmlTimbrado = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSDEVOLUCION-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXmlTimbradob = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSDEVOLUCION-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXml = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSDEVOLUCION-" + V.Serie + V.Folio.ToString + ".xml"
            pRutaPDF = pRutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\" + "PSSDEVOLUCION-" + V.Serie + V.Folio.ToString + ".pdf"
            If V.Cliente.Email <> "" Then
                Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon2)
                Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                'Dim O As New dbOpciones(MySqlcon)
                Dim C As String
                C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + pTitulo + vbNewLine + "Folio: " + V.uuid + vbNewLine + "Comprobante fiscal digital por internet enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
                If V.EsElectronica >= 2 Then
                    If IO.File.Exists(pRutaPDF) Then
                        If GlobalConector = 0 Then
                            M.send("Comprobante Fiscal Digital por Internet " + pTitulo + ": " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaXml)
                            Cenvios.MarcarEnviado(pidVenta, 2, GlobalLicenciaSTR)
                        Else
                            If IO.File.Exists(RutaxmlTimbrado) Then
                                M.send("Comprobante Fiscal Digital por Internet " + pTitulo + ": " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaxmlTimbrado)
                                Cenvios.MarcarEnviado(pidVenta, 2, GlobalLicenciaSTR)
                            Else
                                M.send("Comprobante Fiscal Digital por Internet " + pTitulo + ": " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaXmlTimbradob)
                                Cenvios.MarcarEnviado(pidVenta, 2, GlobalLicenciaSTR)
                            End If
                        End If
                        Resultado = True
                    Else
                        Resultado = False
                    End If
                Else
                    'CFD
                    RutaXml = pRutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSDEVOLUCION-" + Trim(V.Serie) + V.Folio.ToString + ".xml"
                    Dim O As New dbOpciones(MySqlcon2)
                    V.DaDatosTimbrado(V.ID)
                    C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + pTitulo + vbNewLine + "Folio: " + V.uuid + vbNewLine + vbNewLine
                    C += O.CorreoContenido
                    If IO.File.Exists(pRutaPDF) Then
                        M.send("Comprobante Fiscal " + pTitulo + ": " + V.Serie + Format(V.Folio, "0000"), C, V.Cliente.Email, V.Cliente.Nombre, pRutaPDF, RutaXml)
                        Cenvios.MarcarEnviado(pidVenta, 2, GlobalLicenciaSTR)
                        Resultado = True
                    Else
                        Resultado = False
                    End If
                End If
            Else
                Resultado = True
            End If
            'End If
            If Resultado Then
                If V.Cliente.Email <> "" Then
                    NotifyIcon1.BalloonTipText = "Correo enviado: Devolución: " + V.Serie + Format(V.Folio, "0000")
                Else
                    'NotifyIcon1.BalloonTipText = "Cliente sin correo"
                    Cenvios.MarcarEnviado(pidVenta, 2, GlobalLicenciaSTR)
                End If
                NotifyIcon1.ShowBalloonTip(2500)
            Else
                Cenvios.Intentos += 1
                If Cenvios.Intentos >= 5 Then
                    Cenvios.MarcarEnviado(pidVenta, 2, GlobalLicenciaSTR)
                End If
            End If
            Return Resultado
        Catch ex As Exception
            Cenvios.Intentos += 1
            If Cenvios.Intentos >= 5 Then
                Cenvios.MarcarEnviado(pidVenta, 2, GlobalLicenciaSTR)
            End If
            NotifyIcon1.BalloonTipText = "No se pudo enviar el correo. " + ex.Message
            NotifyIcon1.ShowBalloonTip(2500)
            Return False
            'MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Function
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If BackgroundWorker2.IsBusy = False Then
            GlobalChecarConexion2()
            If Cenvios.ChecaSiHay(GlobalLicenciaSTR) Then
                BackgroundWorker2.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub ReportesToolStripMenuItem_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuServiciosReportes.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosVerReportes, PermisosN.Secciones.Servicios) = True) Then
            CierraVentanas()
            If BuscaVentanas("frmReportesServicios") = False Then
                Dim f As New frmReportesServicios()
                f.MdiParent = Me
                f.Show()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub mnuDescuentos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDescuentos.Click
        CierraVentanas()
        If BuscaVentanas("frmDescuentos") = False Then
            Dim f As New frmDescuentos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub



    Private Sub mnuTrabajadores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTrabajadores.Click
        CierraVentanas()
        If BuscaVentanas("frmTrabajadores") = False Then
            Dim f As New frmTrabajadores(0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub NominasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNominas.Click
        CierraVentanas()
        If BuscaVentanas("frmNominas") = False Then
            Dim f As New frmNominas(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub
    Private Sub LlenaNodosImpresionDevoluciones(ByVal PidDevolucion As Integer, ByVal pCadena As String, ByVal pSello As String)
        Dim O As New dbOpciones(MySqlcon)
        Dim V As New dbDevoluciones(PidDevolucion, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(PidDevolucion, V.IdConversion)
        V.DaDatosTimbrado(PidDevolucion)
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

        ImpND.Add(New NodoImpresionN("", "regimenfiscal", Sucursal.RegimenFiscal, 0), "regimenfiscal")
        ImpND.Add(New NodoImpresionN("", "nocuenta", "", 0), "nocuenta")
        ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
        ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
        If GlobalTipoVersion = 3 Then
            ImpND.Add(New NodoImpresionN("", "docafecta", V.ReferenciaDoc, 0), "docafecta")
        Else
            ImpND.Add(New NodoImpresionN("", "docafecta", V.DaDatosDocumento(PidDevolucion), 0), "docafecta")
        End If

        If V.Cliente.DireccionFiscal = 0 Then
            ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
            ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
            ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
            ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
            ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
            ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
            ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
            ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
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
            ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
        End If
        ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")
        ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")


        ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")
        ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
        ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")
        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        Else
            ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        End If
        CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
        ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")


        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbDevolucionesDetalles(MySqlcon)
        DR = VI.ConsultaReader(PidDevolucion)
        ImpNDD.Clear()
        CuantosRenglones = 0
        Dim Cont As Integer = 0
        While DR.Read
            ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipocantidad2"), 0), "tipocantidad" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidadm"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidadm"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importe" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "ieps", Format(DR("ieps"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "ieps" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "ivaretenido", Format(DR("ivaretenido"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "ivaretenido" + Format(Cont, "000"))
            CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()

        ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        'ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtototal, O._formatoSubtotal).PadLeft(13), 0), "subtotal")

        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(PidDevolucion)
        ImpNDDi.Clear()
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
            ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        Next
        If V.ISR <> 0 Then
            ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "isr", Format(V.TotalISR, O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        End If
        If V.IvaRetenido <> 0 Then
            ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "ivaretenido", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        End If
        ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, O._formatoTotal).PadLeft(O.Espaciototal), 0), "total")
        ImpND.Add(New NodoImpresionN("", "Total Iva retenido:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(O.EspacioIva), 0), "totalivaretenido")
        ImpND.Add(New NodoImpresionN("", "Total IEPS:", Format(V.TotalIeps, O._formatoIva).PadLeft(O.EspacioIva), 0), "totalieps")
        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And O._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If

        Dim f As New StringFunctions
        Dim CL As New CLetras
        'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion), 0), "totalletra")
        If V.TotalVenta >= 0 Then
            ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(V.TotalVenta, V.IdConversion, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(V.TotalVenta * -1, V.IdConversion, GlobalIdiomaLetras), 0), "totalletra")
        End If
        ImpND.Add(New NodoImpresionN("", "cadenaoriginal", pCadena, 0), "cadenaoriginal")
        ImpND.Add(New NodoImpresionN("", "sello", pSello, 0), "sello")
        If V.Estado = Estados.Cancelada Then
            ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        Posicion = 0
        Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        CodigoBidimensional = CB.Encode("?&re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, O._formatoTotal) + "&id=" + V.uuid, System.Text.Encoding.Default)
        NumeroPagina = 1
    End Sub

    Private Sub ToolDevoluciones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolDevoluciones.Click
        CierraVentanas()
        If BuscaVentanas("frmDevoluciones") = False Then
            Dim f As New frmDevoluciones(0, 0, 0, 0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub DibujaPaginaEstaticoDEV(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)
        Dim Nimp As NodoImpresionN
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
            ' If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaDevolucion, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaDevolucion + 1000, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
            'Else
            'If TipoImpresora = 0 Then
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'Else
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'End If
            'End If
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
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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

            '*****************************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0

            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
            If HayRenglon Then YCoord = YCoord + 4 + YExtra
            '*****************************

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
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
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
                If niva.Visible = 1 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta = 1 Then
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

                        If niva2.ConEtiqueta = 1 Then
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
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName.Contains("cancelado") Then
                    e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
                End If
            Next
            If ncb.Visible = 1 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DibujaPaginaFlujoDEV(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
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
            'If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaDevolucion, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaDevolucion + 1000, idSucursalI, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
            'Else
            'If TipoImpresora = 0 Then
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'Else
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'End If
            'End If
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
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
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

                        If niva.ConEtiqueta = 1 Then
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

                        If niva2.ConEtiqueta = 1 Then
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
            If YCoord >= LimY And Posicion > 0 And pModo = 0 Then
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
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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

            '*************************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0

            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
            If HayRenglon Then YCoord = YCoord + 4 + YExtra


            '*****************************

            Posicion += 1
            C += 1
        End While


        'Nodos fijos final
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
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
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

                        If niva.ConEtiqueta = 1 Then
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

                        If niva2.ConEtiqueta = 1 Then
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

    Private Sub ModificarInventarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuModificaInventario.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioModificar") = False Then
            Dim f As New frmInventarioModificar()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub ReportesToolStripMenuItem_Click_3(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNominaReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmNominaReportes") = False Then
            Dim f As New frmNominaReportes()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ToolNomina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolNomina.Click
        CierraVentanas()
        If BuscaVentanas("frmNominas") = False Then
            Dim f As New frmNominas(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuGastosClas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGastosClas.Click
        CierraVentanas()
        If BuscaVentanas("frmGastosClasificacion") = False Then
            Dim f As New frmGastosClasificacion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuGastos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGastos.Click
        CierraVentanas()
        If BuscaVentanas("frmGastos") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmGastos(0, sa.idCaja, 0, GlobalIdSucursalDefault)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ProgramarGastosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGastosProg.Click
        CierraVentanas()
        If BuscaVentanas("frmGastosProgramables") = False Then
            Dim f As New frmGastosProgramables()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpeniosConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmpeniosConfig.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosConfiguracion") = False Then
            Dim f As New frmEmpeniosConfiguracion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpeniosClas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmpeniosClas.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosClasificacion") = False Then
            Dim f As New frmEmpeniosClasificacion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpenios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmpenios.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpenios") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmEmpenios(0, sa.idCaja, 0, GlobalIdSucursalDefault, "", -1, False)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpeniosPagos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmpeniosPagos.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosPagos2") = False Then
            Dim f As New frmEmpeniosPagos2()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuGastosAltaEmp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGastosAltaEmp.Click
        CierraVentanas()
        If BuscaVentanas("frmAltaEmpleados") = False Then
            Dim f As New frmAltaEmpleados()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub AltaTiposIdentificaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAltaIdentificacion.Click
        CierraVentanas()
        If BuscaVentanas("frmAltaIdentificacion") = False Then
            Dim f As New frmAltaIdentificacion
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ConsultaDeMovimientosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmpeniosConsultaMov.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosConsultaMovimientos") = False Then
            Dim f As New frmEmpeniosConsultaMovimientos
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub AdjudicacionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmpeniosAdjudicaciones.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosAdjudicaciones") = False Then
            Dim f As New frmEmpeniosAdjudicaciones
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub toolEmpenios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolEmpenios.Click
        CierraVentanas()
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If BuscaVentanas("frmEmpenios") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmEmpenios(0, sa.idCaja, 0, GlobalIdSucursalDefault, "", -1, False)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub toolEmpeniosPagos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolEmpeniosPagos.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosPagos2") = False Then
            Dim f As New frmEmpeniosPagos2()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ReportesToolStripMenuItem_Click_4(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmpeniosReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosReportes") = False Then
            Dim f As New frmEmpeniosReportes()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuGastosReportes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGastosReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmGastosReportes") = False Then
            Dim f As New frmGastosReportes()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpeniosCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmpeniosCompras.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosCompras") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmEmpeniosCompras(0, sa.idCaja, 0, GlobalIdSucursalDefault, "", -1, False)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuGastosMovCajas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGastosMovCajas.Click
        CierraVentanas()
        If BuscaVentanas("frmCajasMovimientos") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmCajasMovimientos(0, sa.idCaja, 0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpeniosCorte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEmpeniosCorte.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosCorte") = False Then
            Dim f As New frmEmpeniosCorte()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub InventarioRelacionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInvRelaciones.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioRelaciones") = False Then
            Dim f As New frmInventarioRelaciones()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub DistribuidoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDitribuidores.Click
        CierraVentanas()
        If BuscaVentanas("frmDistribuidores") = False Then
            Dim f As New frmDistribuidores()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub toolEmpeniosCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolEmpeniosCompras.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosCompras") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmEmpeniosCompras(0, sa.idCaja, 0, GlobalIdSucursalDefault, "", -1, False)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Select Case Cenvios.Documento
            Case 0
                EnviarCorreo(Cenvios.IdDocumento, RutaXML, RutaPDFs, "FACTURA")
            Case 1
                EnviarCorreoNC(Cenvios.IdDocumento, RutaXMLNC, RutaPDFsNC, "NOTA DE CRÉDITO")
            Case 2
                EnviarCorreoDEV(Cenvios.IdDocumento, RutaXMLDEV, RutaPDFDEV, "DEVOLUCIÓN")
        End Select
    End Sub



    Private Sub mnuAltaEstados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAltaEstados.Click
        CierraVentanas()
        If BuscaVentanas("frmServiciosAltaEstado") = False Then
            Dim f As New frmServiciosAltaEstado()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuNuevoServicioI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNuevoServicioI.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServicioVer, PermisosN.Secciones.Servicios) = True) Then
            CierraVentanas()
            If BuscaVentanas("frmServiciosSuc") = False Then
                Dim f As New frmServiciosSuc
                f.MdiParent = Me
                f.Show()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub mnuBuscarServicioI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBuscarServicioI.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosConsultaVer, PermisosN.Secciones.Servicios) = True) Then

            CierraVentanas()
            If BuscaVentanas("frmServiciosConsultaSucursales") = False Then
                Dim f As New frmServiciosConsultaSucursales
                f.MdiParent = Me
                f.Show()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub mnuServiciosReportesI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuServiciosReportesI.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosVerReportes, PermisosN.Secciones.Servicios) = True) Then
            CierraVentanas()
            If BuscaVentanas("frmReportesServiciosSuc") = False Then
                Dim f As New frmReportesServiciosSuc()
                f.MdiParent = Me
                f.Show()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub mnuAltaEstadosServiciosI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAltaEstadosServiciosI.Click
        CierraVentanas()
        If BuscaVentanas("frmServiciosAltaEstado") = False Then
            Dim f As New frmServiciosAltaEstado()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub



    Private Sub mnuPolizas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPolizas.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadPolizasN") = False Then
            Dim f As New frmContabilidadPolizasN(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuCuentasContablesCon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCuentasContablesCon.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadCuentas") = False Then
            Dim f As New frmContabilidadCuentas("", "", "", "", "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ConfiguraciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuContaConfig.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadConfig") = False Then
            Dim f As New frmContabilidadConfig
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuContaReportes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuContaReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadReportes") = False Then
            Dim f As New frmContabilidadReportes
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuFertilizantesPedidos_Click(sender As Object, e As EventArgs) Handles mnuFertilizantesPedidos.Click
        CierraVentanas()
        If BuscaVentanas("frmFertilizantesPedido") = False Then
            Dim f As New frmFertilizantesPedido
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuFertilizantesReportes_Click(sender As Object, e As EventArgs) Handles mnuFertilizantesReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmFertilizantesReportes") = False Then
            Dim f As New frmFertilizantesReportes
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuContaConsultaSaldos_Click(sender As Object, e As EventArgs) Handles mnuContaConsultaSaldos.Click
        If GlobalConContabilidad Then
            CierraVentanas()
            If BuscaVentanas("frmContabilidadConsultaSaldos") = False Then
                Dim f As New frmContabilidadConsultaSaldos
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub toolpolizas_Click(sender As Object, e As EventArgs) Handles toolpolizas.Click
        CierraVentanas()
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If BuscaVentanas("frmContabilidadPolizasN") = False Then
            Dim f As New frmContabilidadPolizasN(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub toolContaReportes_Click(sender As Object, e As EventArgs) Handles toolContaReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadReportes") = False Then
            Dim f As New frmContabilidadReportes
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub toolFertilizantes_Click(sender As Object, e As EventArgs) Handles toolFertilizantes.Click
        CierraVentanas()
        If BuscaVentanas("frmFertilizantesPedido") = False Then
            Dim f As New frmFertilizantesPedido
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuConciliacionDiot_Click(sender As Object, e As EventArgs) Handles mnuConciliacionDiot.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadDIOT") = False Then
            Dim f As New frmContabilidadDIOT
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub MáscarasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuContabilidadMascaras.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadMascaras") = False Then
            Dim f As New frmContabilidadMascaras
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuContaGenerarpolizas_Click(sender As Object, e As EventArgs) Handles mnuContaGenerarpolizas.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadGeneraPolizas") = False Then
            Dim f As New frmContabilidadGeneraPolizas
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuValidar_Click(sender As Object, e As EventArgs) Handles mnuValidar.Click
        CierraVentanas()
        If BuscaVentanas("frmValidacion") = False Then
            Dim f As New frmValidacion
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuContaClaspoli_Click(sender As Object, e As EventArgs) Handles mnuContaClaspoli.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadAltaClas") = False Then
            Dim f As New frmContabilidadAltaClas
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub CorteGlobalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CorteGlobalToolStripMenuItem.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.ReportesVer, PermisosN.Secciones.Ventas) = True Then
            CierraVentanas()
            If BuscaVentanas("frmVentasCorte") = False Then
                Dim f As New frmVentasCorte
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub mnuBancosCuentas_Click(sender As Object, e As EventArgs) Handles mnuBancosCuentas.Click
        CierraVentanas()
        If BuscaVentanas("frmRegistroCuentas") = False Then
            Dim f As New frmRegistroCuentas()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuApartadosPagos_Click(sender As Object, e As EventArgs) Handles mnuApartadosPagos.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasPagosRemisiones") = False Then
            Dim f As New frmVentasPagosRemisiones("", 1, 0, "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ConsultaDeOfertasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuConsultaOfertasVentas.Click
        CierraVentanas()
        If BuscaVentanas("frmDescuentosConsulta") = False Then
            Dim f As New frmDescuentosConsulta()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuContabilidadConceptosNomina_Click(sender As Object, e As EventArgs) Handles mnuContabilidadConceptosNomina.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadNominaCuentas") = False Then
            Dim f As New frmContabilidadNominaCuentas()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub BoletasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuSemillasBoletas.Click
        CierraVentanas()
        If BuscaVentanas("frmSemillasBoleta") = False Then
            Dim f As New frmSemillasBoleta(GlobalSemillasResumida, GlobalPermisos.ChecaPermiso(PermisosN.Semillas.PrecioVerBoleta, PermisosN.Secciones.Semillas))
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub MnuSemillasLiquidaciones_Click(sender As Object, e As EventArgs) Handles MnuSemillasLiquidaciones.Click
        CierraVentanas()
        If BuscaVentanas("frmSemillasLiquidacion") = False Then
            Dim f As New frmSemillasLiquidacion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuSemillasComprobantes_Click(sender As Object, e As EventArgs) Handles mnuSemillasComprobantes.Click
        CierraVentanas()
        If BuscaVentanas("frmSemillasComprobante") = False Then
            Dim f As New frmSemillasComprobante()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuSemillasReportes_Click(sender As Object, e As EventArgs) Handles mnuSemillasReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmSemillasReportes") = False Then
            Dim f As New frmSemillasReportes(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuBoletasInventario_Click(sender As Object, e As EventArgs) Handles mnuBoletasInventario.Click
        CierraVentanas()
        If BuscaVentanas("frmSemillasBoleta") = False Then
            Dim f As New frmSemillasBoleta(GlobalSemillasResumida, GlobalPermisos.ChecaPermiso(PermisosN.Semillas.PrecioVerBoleta, PermisosN.Secciones.Semillas))
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuReportesBoletasInv_Click(sender As Object, e As EventArgs) Handles mnuReportesBoletasInv.Click
        CierraVentanas()
        If BuscaVentanas("frmSemillasReportes") = False Then
            Dim f As New frmSemillasReportes(1)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub MovimientosUsuariosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuMovimientosUsuarios.Click
        CierraVentanas()
        If BuscaVentanas("frmAuditoriaUsuario") = False Then
            Dim f As New frmAuditoriaUsuario()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuInventarioAjusteCero_Click(sender As Object, e As EventArgs) Handles mnuInventarioAjusteCero.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioRCostos") = False Then
            Dim f As New frmInventarioRCostos(2)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub ConfigurarConceptosInventarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuConfigurarConceptosInv.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioConfiguracionConceptos") = False Then
            Dim f As New frmInventarioConfiguracionConceptos
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub PedidosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuInventarioPedidos.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioPedidos") = False Then
            Dim f As New frmInventarioPedidos(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuConsultarPedidos_Click(sender As Object, e As EventArgs) Handles mnuConsultarPedidos.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioPedidosConsulta") = False Then
            Dim f As New frmInventarioPedidosConsulta(0, True, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub TecnicosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuTecnicos.Click
        CierraVentanas()
        If BuscaVentanas("frmTecnicos") = False Then
            Dim f As New frmTecnicos
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ClasificacionesDeServiciosToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles mnuClasificacionesDeServicios.Click
        CierraVentanas()
        If BuscaVentanas("frmServiciosClasificaciones") = False Then
            Dim f As New frmServiciosClasificaciones
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub MedidasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuMedidas.Click
        CierraVentanas()
        If BuscaVentanas("frmCantidades") = False Then
            Dim f As New frmCantidades
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub MonedasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuMonedas.Click
        CierraVentanas()
        If BuscaVentanas("frmMonedas") = False Then
            Dim f As New frmMonedas
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    
    Private Sub ConceptosDeNotasVentasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuConceptoNventas.Click
        CierraVentanas()
        If BuscaVentanas("frmConceptosNotasVentas") = False Then
            Dim f As New frmConceptosNotasVentas()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ConceptosDeNotasComprasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuConceptoNcompras.Click
        CierraVentanas()
        If BuscaVentanas("frmConceptosNotasCompras") = False Then
            Dim f As New frmConceptosNotasCompras()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

   
    Private Sub TiposClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuTiposClientes.Click
        CierraVentanas()
        If BuscaVentanas("frmTiposCP") = False Then
            Dim f As New frmTiposCP(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub TiposProveedoresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuTiposProveedores.Click
        CierraVentanas()
        If BuscaVentanas("frmTiposCP") = False Then
            Dim f As New frmTiposCP(1)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuTiposSucursales_Click(sender As Object, e As EventArgs) Handles mnuTiposSucursales.Click
        CierraVentanas()
        If BuscaVentanas("frmTiposCP") = False Then
            Dim f As New frmTiposCP(2)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuRestConfig_Click(sender As Object, e As EventArgs) Handles mnuRestConfig.Click
        CierraVentanas()
        If BuscaVentanas("frmRestauranteConfiguracion") = False Then
            Dim f As New frmRestauranteConfiguracion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuRestPrincipal_Click(sender As Object, e As EventArgs) Handles mnuRestPrincipal.Click
        CierraVentanas()
        If BuscaVentanas("frmRestaurantePrincipal") = False Then
            Dim f As New frmRestaurantePrincipal()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub VerAlertasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnuGastosVerAlertas.Click
        CierraVentanas()
        If BuscaVentanas("frmGastosAviso") = False Then
            Dim f As New frmGastosAviso()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub CartaDeSalidaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CartaDeSalidaToolStripMenuItem.Click
        CierraVentanas()
        If BuscaVentanas("frmCartaSalida") = False Then
            Dim f As New frmCartaSalida()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub
End Class
