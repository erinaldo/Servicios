Public Class frmPrincipalN
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
            Ribbon1.Text = "Empresa: " + GlobalNombreEmpresa + vbCrLf + "Usuario: " + GlobalUsuario
            If O._ConsultaRealTime = 1 Then GlobalConsultaTiempoReal = True
            GlobalIdMoneda = 2
            GlobalIdAlmacen = O._idAlmacen
            GlobalTipoFacturacion = O._TipoFacturacion
            FechaVerPunto2 = O._FechaPunto2
            GlobalTipoCosteo = O._TipoCosteo
            GlobalConector = O._Conector
            'If GlobalTipoFacturacion = 2 Then mnuReporteMensual.Visible = False
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
        'CargaImagenes()
        'Catch ex As Exception
        If My.Settings.abrepunto = "1" And ConPuntodeVenta = True Then
            Ribbon1.Visible = False
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
        'GlobalTipoVersion = 1

        btnHerHerActivarconector.Visible = False
        'Version pura Facturacion----------------
        If GlobalTipoVersion = 1 Then
            tabCompras.Visible = False
            tabBancos.Visible = False
            tabNomina.Visible = False
            tabGastos.Visible = False
            tabContabilidad.Visible = False
            tabInventario.Visible = False

            pnlVentasPagos.Visible = False
            pnlVentasApartados.Visible = False
            mnuArcCompras.Visible = False
            mnuArcInventario.Visible = False

            btnArcSerClasificaciones.Visible = False
            btnHerHerModifiarInv.Visible = False

        End If
        'Version Facturacion y Clientes
        If GlobalTipoVersion = 2 Then
            tabCompras.Visible = False
            tabInventario.Visible = False
            
            pnlVentasPagos.Visible = False
            mnuArcCompras.Visible = False
            mnuArcInventario.Visible = False

        End If
        'conector
        If GlobalTipoVersion = 3 Then
            NotifyIcon1.Visible = True

            tabCompras.Visible = False
            tabServicios.Visible = False
            tabGastos.Visible = False
            tabEmpenos.Visible = False
            tabContabilidad.Visible = False
            tabInventario.Visible = False
            tabPuntoVenta.Visible = False
            tabBancos.Visible = False
            tabNomina.Visible = False

            mnuArcServicios.Visible = False
            mnuArcCompras.Visible = False
            mnuArcInventario.Visible = False
            pnlVentasApartados.Visible = False
            pnlVentasPagos.Visible = False
            pnlVentasFertilizantes.Visible = False

            'btnVenConOfertas.Visible = False
            btnHerHerCambioPrecios.Visible = False
            btnHerHerModifiarInv.Visible = False
            btnHerHerActivarconector.Visible = True

            Dim Em As New dbEmpresas
            Em.IniciarMySQLE(My.Settings.BasedeDatos, My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
            Em.LlenaDatos(GlobalIdEmpresa)
            IniciarMySQL2(MySqlcon.Database, Em.Servidor, Em.Usuario, Em.PasswordS, My.Settings.puertodb)
            Em.MySqlconE.Close()
            Cenvios = New dbConectorEnvios(MySqlcon2)
            Timer1.Enabled = True
            Timer2.Enabled = True

        End If
        'pura contabilidad
        If GlobalTipoVersion = 4 Then
            Ribbon1.OrbVisible = False
            tabVentas.Visible = False
            tabBancos.Visible = False
            tabNomina.Visible = False
            tabCompras.Visible = False
            tabInventario.Visible = False
            tabPuntoVenta.Visible = False
            tabEmpenos.Visible = False
            tabGastos.Visible = False
            tabServicios.Visible = False

            btnHerConDistribuidores.Visible = False
            btnHerConLicencias.Visible = False
            btnHerHerActivarconector.Visible = False
            btnHerHerCambioPrecios.Visible = False
            btnHerHerDisenoDocumentos.Visible = False
            btnHerHerModifiarInv.Visible = False
        End If

        tabBancos.Visible = GlobalConBancos
        tabNomina.Visible = GlobalConNomina
        tabServicios.Visible = GlobalConServicios
        tabGastos.Visible = GlobalConGastos
        tabEmpenos.Visible = GlobalconEmpenios
        tabPuntoVenta.Visible = ConPuntodeVenta
        tabContabilidad.Visible = GlobalConContabilidad

        pnlSerInterno.Visible = GlobalConServiciosInterno
        pnlVentasFertilizantes.Visible = GlobalConFertilizantes
        pnlComGranos.Visible = GlobalconSemillas
        pnlPunRestaurante.Visible = GlobalConRestaurant

        btnHerConLicencias.Visible = GlobalConLicencias
        btnHerConDistribuidores.Visible = GlobalConLicencias
        btnComConValidador.Visible = GlobalConValidador
        btnHerHerMovimientosUsuario.Visible = GlobalConUsuarios
        btnConCatMascaras.Visible = GlobalconIntegracion
        btnConOpeGenerarPolizas.Visible = GlobalconIntegracion

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
    Private Sub ClasificacionesDeInventarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcInvClasificaciones.Click

        CierraVentanas()
        If BuscaVentanas("frmInventarioModificar") = False Then
            Dim f As New frmInventarioModificar()
            f.MdiParent = Me
            f.Show()
        End If

    End Sub



    Private Sub ArtículosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcInvArticulos.Click
        CierraVentanas()
        If BuscaVentanas("frmInventario") = False Then
            Dim f As New frmInventario
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ProveedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcComProveedores.Click
        CierraVentanas()
        If BuscaVentanas("frmProveedores") = False Then
            Dim f As New frmProveedores(0, 0, "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ClientesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcVenClientes.Click
        CierraVentanas()
        If BuscaVentanas("frmClientes") = False Then
            Dim f As New frmClientes(0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub




    Private Sub AcercaDeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcerca.Click
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

    Private Sub UsuariosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcGenUsuarios.Click
        CierraVentanas()
        If BuscaVentanas("frmUsuarios") = False Then
            Dim f As New frmUsuarios
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ComprasToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComCapFacturas.Click
        If GlobalTipoVersion = 0 Then
            CierraVentanas()
            If BuscaVentanas("frmCompras") = False Then
                Dim f As New frmCompras
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub NuevoServicioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerComNuevo.Click
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

    Private Sub BuscarServicioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerComBuscar.Click
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

    Private Sub PedidosToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComCapOrdenes.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasPedidos") = False Then
            Dim f As New frmComprasPedidos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub



    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Salir = False
        Me.Close()
    End Sub

    Private Sub OpcionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerConConfigCorreo.Click
        CierraVentanas()
        If BuscaVentanas("frmOpcionesCorreo") = False Then
            Dim f As New frmOpcionesCorreo
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub AlmacenesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcInvAlmacenes.Click
        CierraVentanas()
        If BuscaVentanas("frmAlmacenes") = False Then
            Dim f As New frmAlmacenes
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub OpcionesToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerConOpciones.Click
        CierraVentanas()
        If BuscaVentanas("frmOpciones") = False Then
            Dim f As New frmOpciones
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub PagosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVenPagFacturas.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasPagos") = False Then
            Dim f As New frmVentasPagos("", "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuInventarioConceptos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcInvConceptos.Click
        CierraVentanas()
        If BuscaVentanas("frmInvnetarioConceptos") = False Then
            Dim f As New frmInvnetarioConceptos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub NuevoInventarioInicialToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvOpeMovimientos.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioMovimientosN") = False Then
            Dim f As New frmInventarioMovimientosN()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuVentas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVentasFac.Click
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
                        'Ribbon1.Visible = False
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

    Private Sub mnuVentasRemision_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVenCapRemisiones.Click
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
                'Ribbon1.Visible = False
                Dim f As New frmVentasRemisiones()
                f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                f.WindowState = FormWindowState.Maximized
                f.MdiParent = Me
                f.Show()
            End If
            'End If
        End If
    End Sub

    Private Sub mnuVentasCotizacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVenCapCotizaciones.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasCotizacion") = False Then
            Dim Op As New dbOpciones(MySqlcon)
            If Op.MaximizarVentas = 0 Then
                Dim f As New frmVentasCotizacion
                f.MdiParent = Me
                f.Show()
            Else
                GlobalEstadoVentanas = GlobalEstadoVentanas Or 2
                'Ribbon1.Visible = False
                Dim f As New frmVentasCotizacion()
                f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                f.WindowState = FormWindowState.Maximized
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub KardexToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvConCardex.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioKardex") = False Then
            Dim f As New frmInventarioKardex(ModosDeBusqueda.Principal)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub NuevaRemisiónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComCapRemisiones.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasRemisiones") = False Then
            Dim f As New frmComprasRemisiones
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub ChecaMenus()
        btnArcVenClientes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClientesVer, PermisosN.Secciones.Catalagos)
        btnArcVenVendedores.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.VendedoresVer, PermisosN.Secciones.Catalagos)
        btnArcComProveedores.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ProveedoresVer, PermisosN.Secciones.Catalagos)
        btnArcGenSucursales.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.SucursalesVer, PermisosN.Secciones.Catalagos)
        btnArcInvAlmacenes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesVer, PermisosN.Secciones.Catalagos)
        btnArcInvClasificaciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioVer, PermisosN.Secciones.Catalagos)
        btnArcInvArticulos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.InventarioVer, PermisosN.Secciones.Catalagos)
        btnInvOpeMovimientos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosdeInventarioVer, PermisosN.Secciones.Catalagos)
        btnArcGenMetodoPagoFactura.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.FormasdePagoVer, PermisosN.Secciones.Catalagos)
        btnArcGenMetodoPagoRemision.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.FormasdePagoRemVer, PermisosN.Secciones.Catalagos)
        btnArcSerCajas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.CajasVer, PermisosN.Secciones.Catalagos)
        btnArcGenMedidas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.MedidasVer, PermisosN.Secciones.Catalagos)
        btnArcGenMonedas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.MonedasVer, PermisosN.Secciones.Catalagos)
        btnArcVenConceptosNotas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasVentasVer, PermisosN.Secciones.Catalagos)
        btnArcComConceptosNotas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasComprasVer, PermisosN.Secciones.Catalagos)
        btnArcGenReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ReportesVer, PermisosN.Secciones.Catalagos2)
        mnuArcGeneral.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.UsuariosVer, PermisosN.Secciones.Catalagos2)
        mnuArcVentas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.EmpresasVer, PermisosN.Secciones.Catalagos2)
        btnArcSerTecnicos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.TecnicosVer, PermisosN.Secciones.Catalagos2)
        btnArcGenZonas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ZonasVer, PermisosN.Secciones.Catalagos2)
        btnArcSerOfertas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.OfertasVer, PermisosN.Secciones.Catalagos2)
        btnArcInvRelaciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.InventarioRelacionesVer, PermisosN.Secciones.Catalagos2)
        btnArcVenTiposClientes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ClientesTiposVer, PermisosN.Secciones.Catalagos2)
        btnArcComTiposProveedores.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ProvTiposVer, PermisosN.Secciones.Catalagos2)
        btnArcGenTiposSucursales.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.SucTiposVer, PermisosN.Secciones.Catalagos2)
        'mnuTipoMain.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ClientesTiposVer, PermisosN.Secciones.Catalagos2) Or GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ProvTiposVer, PermisosN.Secciones.Catalagos2) Or GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.SucTiposVer, PermisosN.Secciones.Catalagos2)


        btnVenCapCotizaciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CotizacionesVer, PermisosN.Secciones.Ventas)
        'btnVenCapPedidos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PedidosVer, PermisosN.Secciones.Ventas)
        btnVenCapRemisiones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesVer, PermisosN.Secciones.Ventas)
        btnVentasFac.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas)
        'btnVenCapDevoluciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Ventas.DevolucionesVer, PermisosN.Secciones.Ventas)
        'btnVenConDevoluciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Ventas.DevolucionesVer, PermisosN.Secciones.Ventas)
        btnVenCapNotasCredito.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoVer, PermisosN.Secciones.Ventas)
        btnVenPagFacturas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosVer, PermisosN.Secciones.Ventas)
        btnVenPagRemisiones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosRemVer, PermisosN.Secciones.Ventas)
        btnVenConReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Ventas.ReportesVer, PermisosN.Secciones.Ventas)
        'btnVenApaRealizar.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasApartadosVer, PermisosN.Secciones.Ventas)


        btnComCapPreordenes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.CotizacionesVer, PermisosN.Secciones.Compras)
        btnComCapOrdenes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.PedidosVer, PermisosN.Secciones.Compras)
        btnComCapRemisiones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.RemisionesVer, PermisosN.Secciones.Compras)
        btnComCapFacturas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasVer, PermisosN.Secciones.Compras)
        btnComCapDevoluciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.DevolucionesVer, PermisosN.Secciones.Compras)
        btnComConDevoluciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.DevolucionesVer, PermisosN.Secciones.Compras)
        btnComCapNotasCredito.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoVer, PermisosN.Secciones.Compras)
        btnComCapNotasCargo.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCargoVer, PermisosN.Secciones.Compras)
        'btnComCapDocumentos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.DocumentosProveedoresVer, PermisosN.Secciones.Compras)
        'btnComCapPagos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.PagosVer, PermisosN.Secciones.Compras)
        btnComConReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.Reportes, PermisosN.Secciones.Compras)
        btnComConMovimientos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.Consultas, PermisosN.Secciones.Compras)
        btnComConValidador.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Compras.Consultas, PermisosN.Secciones.Compras)

        btnInvOpeMovimientos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario)
        btnInvConCardex.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.KardexVer, PermisosN.Secciones.Inventario)
        btnInvConReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.RevisionVer, PermisosN.Secciones.Inventario)
        btnInvConReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.Reportes, PermisosN.Secciones.Inventario)
        btnInvHerRecCostos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.RecalcularCostos, PermisosN.Secciones.Inventario)
        btnInvHerRecInventario.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.RecalcularInventarios, PermisosN.Secciones.Inventario)
        btnInvHerBuscarNeg.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.BuscarNegativos, PermisosN.Secciones.Inventario)
        btnInvHerAjustarCero.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.RecalcularInventarios, PermisosN.Secciones.Inventario)
        btnInvOpePedidos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosVer, PermisosN.Secciones.Inventario)
        'btninvope.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.PedidosVer, PermisosN.Secciones.Inventario)

        btnPunGenVentas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.VentasVer, PermisosN.Secciones.PuntodeVenta)
        btnPunGenReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.ReportesVer, PermisosN.Secciones.PuntodeVenta)

        btnHerConOpciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.OpcionesVer, PermisosN.Secciones.Herramientas)
        btnHerConConfigCorreo.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.OpcionesCorreoVer, PermisosN.Secciones.Herramientas)
        btnHerHerDisenoDocumentos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingVer, PermisosN.Secciones.Herramientas)
        btnHerBasRespaldar.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.RespaldoVer, PermisosN.Secciones.Herramientas)
        btnHerBasRestaurar.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.RestaurarVer, PermisosN.Secciones.Herramientas)
        btnHerHerCambioPrecios.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.CambiodePrecios, PermisosN.Secciones.Herramientas)
        btnHerConImportar.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Herramientas.Importador, PermisosN.Secciones.Herramientas)

        btnBanCatCuentas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Bancos.CuentasVer, PermisosN.Secciones.Bancos)
        btnBanOpeDepositos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Bancos.DepositosVer, PermisosN.Secciones.Bancos)
        btnBanOpePagos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Bancos.PagoProveedoresVer, PermisosN.Secciones.Bancos)
        btnBanOpeConciliacion.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Bancos.Consiliacion, PermisosN.Secciones.Bancos)
        btnBanConReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Bancos.Reportes, PermisosN.Secciones.Bancos)

        btnNomOpeNomina.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaVer, PermisosN.Secciones.Nomina)
        btnNomConReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaVer, PermisosN.Secciones.Nomina)
        btnNomCatTrabajadores.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Nominas.TrabajadoresVer, PermisosN.Secciones.Nomina)

        btnSerComNuevo.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServicioVer, PermisosN.Secciones.Servicios)
        btnSerComReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosVerReportes, PermisosN.Secciones.Servicios)

        btnGasOpeGastos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosVer, PermisosN.Secciones.Gastos)
        btnGasCatClasificaciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesVer, PermisosN.Secciones.Gastos)
        btnGasCatEmpleados.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosEmpleadosAlta, PermisosN.Secciones.Gastos)
        btnGasOpeProgramar.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosProgramarVer, PermisosN.Secciones.Gastos)
        btnGasConReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosReportesVer, PermisosN.Secciones.Gastos)
        'btnGasOpeAlertas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosVerNotificaciones, PermisosN.Secciones.Gastos)
        btnGasOpeMovimientos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosVer, PermisosN.Secciones.PuntodeVenta)

        btnEmpOpeEmpenos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosVer, PermisosN.Secciones.Empenios)
        btnEmpherConfiguracion.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosConfiguracionVer, PermisosN.Secciones.Empenios)
        btnEmpCatIdentificaciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosIdentificacionVer, PermisosN.Secciones.Empenios)
        btnEmpOpePagos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosPagosVer, PermisosN.Secciones.Empenios)
        btnEmpConConsultas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosConsultaMovVer, PermisosN.Secciones.Empenios)
        btnEmpOpeAdjudicaciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosAdjudicacionesVer, PermisosN.Secciones.Empenios)
        btnEmpConReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosReportesVer, PermisosN.Secciones.Empenios)
        btnEmpCatClasificaciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosClasificacionesVer, PermisosN.Secciones.Empenios)
        btnEmpOpeCompras.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasVer, PermisosN.Secciones.Empenios)
        btnEmpOpeCortes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Empenios.VerCorte, PermisosN.Secciones.Empenios)

        'btnVenFerPedidos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.PedidosVer, PermisosN.Secciones.Fertilizantes)
        'btnVenFerReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.PedidosVer, PermisosN.Secciones.Fertilizantes)

        btnConConReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ConfiguracionVer, PermisosN.Secciones.Contabilidad)
        btnConCatClasifPolizas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasVer, PermisosN.Secciones.Contabilidad)
        btnConCatCuentas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.CuentasVer, PermisosN.Secciones.Contabilidad)
        btnConOpePolizas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.PolizasVer, PermisosN.Secciones.Contabilidad)
        btnConOpeSaldos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ConsultaSaldosVer, PermisosN.Secciones.Contabilidad)
        btnConOpeConciliarDiot.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.PolizasVer, PermisosN.Secciones.Contabilidad)
        btnConConReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ReportesVer, PermisosN.Secciones.Contabilidad)
        btnConCatMascaras.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.MascarasVer, PermisosN.Secciones.Contabilidad)
        btnConCatMascaras.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.GenerarPolizasPermitir, PermisosN.Secciones.Contabilidad)
        btnConCatConceptos.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.NominaConceptosVer, PermisosN.Secciones.Contabilidad)

        'btnComGraBoletas.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Semillas.BoletasVer, PermisosN.Secciones.Semillas)
        'btnComGraLiquidaciones.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Semillas.LiquidacionVer, PermisosN.Secciones.Semillas)
        'btnComGraComprobantes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Semillas.ComprobanteVer, PermisosN.Secciones.Semillas)
        'btnComGraReportes.Enabled = GlobalPermisos.ChecaPermiso(PermisosN.Semillas.ReportesVer, PermisosN.Secciones.Semillas)

    End Sub

    Private Sub FormasDePagoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcGenMetodoPagoFactura.Click

        CierraVentanas()
        If BuscaVentanas("frmFormasdePago") = False Then
            Dim f As New frmFormasDePago()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub SucursalesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcGenSucursales.Click

        CierraVentanas()
        If BuscaVentanas("frmSucusales") = False Then
            Dim f As New frmSucursales()
            f.MdiParent = Me
            f.Show()
        End If


    End Sub
    Private Sub mnuZona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcGenZonas.Click

        CierraVentanas()
        If BuscaVentanas("frmZona") = False Then
            Dim f As New frmZona()
            f.MdiParent = Me
            f.Show()
        End If


    End Sub

    Private Sub ReportesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVenConReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasReportesN") = False Then
            Dim f As New frmVentasReportesN()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ReporteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvConReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioReportes") = False Then
            Dim f As New frmInventarioReportes()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub VendedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcVenVendedores.Click
        CierraVentanas()
        If BuscaVentanas("frmVendedores") = False Then
            Dim f As New frmVendedores
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcInvArticulos.Click
        CierraVentanas()
        If BuscaVentanas("frmInventario") = False Then
            Dim f As New frmInventario
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    'Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcVenClientes.Click
    '    CierraVentanas()
    '    If BuscaVentanas("frmClientes") = False Then
    '        Dim f As New frmClientes(0, 0)
    '        f.MdiParent = Me
    '        f.Show()
    '    End If
    'End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVentasFac.Click
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
                'Ribbon1.Visible = False
                Dim f As New frmVentasN(0, 0, 0, 0)
                f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                f.WindowState = FormWindowState.Maximized
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComCapFacturas.Click
        CierraVentanas()
        If BuscaVentanas("frmCompras") = False Then
            Dim f As New frmCompras
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuNotasdeCredito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVenCapNotasCredito.Click
        CierraVentanas()
        If BuscaVentanas("frmNotasdeCreditoN") = False Then
            Dim f As New frmNotasdeCreditoN("")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    'Private Sub DevolucionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVenCapDevoluciones.Click
    '    Dim Forma As New frmBuscaDocumentoVenta(2, False, 1, GlobalIdSucursalDefault, 1, False, True, True, 0, True, "", True)
    '    Dim C As String
    '    If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '        If Forma.Tipo = 2 Then
    '            Dim Cr As New dbVentasRemisiones(Forma.id(0), MySqlcon)
    '            C = Cr.Cliente.Clave
    '        Else
    '            Dim Cv As New dbVentas(Forma.id(0), MySqlcon, "0")
    '            C = Cv.Cliente.Clave
    '        End If
    '        CierraVentanas()
    '        If BuscaVentanas("frmDevoluciones") = False Then
    '            Dim f As New frmDevoluciones(0, Forma.id(0), 1, C, Forma.Tipo)
    '            f.MdiParent = Me
    '            f.Show()
    '        End If
    '    End If
    'End Sub

    


    Private Sub ClientesMovimientosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVenConMovimientos.Click
        CierraVentanas()
        If BuscaVentanas("frmClientesMovimientos") = False Then
            Dim f As New frmClientesMovimientos(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

 


    Private Sub mnuCotizacionesb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComCapPreordenes.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasCotizacionesNB") = False Then
            Dim f As New frmComprasCotizacionesNB()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuComprasPagos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmComprasPagos") = False Then
            Dim f As New frmComprasPagos("", "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuDevolucionesCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComCapDevoluciones.Click
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

    Private Sub mnuBuscarDevolucionCompra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComConDevoluciones.Click
        CierraVentanas()
        If BuscaVentanas("frmDevolucionesComprasConsulta") = False Then
            Dim f As New frmDevolucionesComprasConsulta(ModosDeBusqueda.Principal)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuNotasdeCreditoCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComCapNotasCredito.Click
        CierraVentanas()
        If BuscaVentanas("frmNotasdeComprasCreditoN") = False Then
            Dim f As New frmNotasdeCreditoComprasN("")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuNotasdeCargoCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComCapNotasCargo.Click
        CierraVentanas()
        If BuscaVentanas("frmNotasdeCargoC") = False Then
            Dim f As New frmNotasdeCargoC()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ProveedoresMovimientosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComConMovimientos.Click
        CierraVentanas()
        If BuscaVentanas("frmProveedoresMovimientos") = False Then
            Dim f As New frmProveedoresMovimientos(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ImpresionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerHerDisenoDocumentos.Click
        CierraVentanas()
        If BuscaVentanas("frmImpresion") = False Then
            Dim f As New frmImpresion
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub ReportesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComConReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmComprasReportesN") = False Then
            Dim f As New frmComprasReportesN()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub DocumentosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmProveedoresDocumentos") = False Then
            Dim f As New frmProveedoresDocumentos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ReportesToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcGenReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmCatalogos") = False Then
            Dim f As New frmCatalogos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub ConcialiciónDeInventarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvConRevision.Click
        CierraVentanas()
        If BuscaVentanas("frmConciliarInventario") = False Then
            Dim f As New frmConciliarInventario(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ContadorDeTimbresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerHerContadorTimbres.Click
        CierraVentanas()
        If BuscaVentanas("frmContadorTimbres") = False Then
            Dim f As New frmContadorTimbres
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub EmpresasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcGenEmpresas.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpresas") = False Then
            Dim f As New frmEmpresas
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuCambiarDeEmpresa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCambiarEmpresa.Click
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

    Private Sub RespaldoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerBasRespaldar.Click
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

    Private Sub RestaurarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerBasRestaurar.Click
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

    Private Sub PagaréToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmPagare") = False Then
            Dim f As New frmPagare
            f.MdiParent = Me
            f.Show()
        End If
    End Sub



    Private Sub CambioDePreciosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerHerCambioPrecios.Click
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

    Private Sub ActivarConectorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerHerActivarconector.Click
        If Timer1.Enabled = False Then
            Timer1.Enabled = True
            Timer2.Enabled = True
            btnHerHerActivarconector.Checked = True
        Else
            Timer1.Enabled = False
            Timer2.Enabled = False
            'NotifyIcon1.Visible = False
            btnHerHerActivarconector.Checked = False
        End If
    End Sub

    Private Sub SalirToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub


    Private Sub VentasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPunGenVentas.Click
        CierraVentanas()
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If BuscaVentanas("frmPuntodeVenta") = False Then
            Ribbon1.Visible = False
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

    Private Sub mnuCajas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcSerCajas.Click
        CierraVentanas()
        If BuscaVentanas("frmCajas") = False Then
            Dim f As New frmCajas
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuPuntodeVentaReportes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPunGenReportes.Click

        CierraVentanas()
        If BuscaVentanas("frmPuntodeVentaReportes") = False Then
            Dim f As New frmPuntodeVentaReportes
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub FormasDePagoRemisionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcGenMetodoPagoRemision.Click
        CierraVentanas()
        If BuscaVentanas("frmFormasdePagoRemisiones") = False Then
            Dim f As New frmFormasdePagoRemisiones()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub RecalcularCostosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvHerRecCostos.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioRCostos") = False Then
            Dim f As New frmInventarioRCostos(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ClientesHistorialDeVentaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVenConHistorial.Click
        'CierraVentanas()
        If BuscaVentanas("frmClientesConsultaArticulos") = False Then
            Dim f As New frmClientesConsultaArticulos(0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub PagosRemisionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVenPagRemisiones.Click
        CierraVentanas()
        If BuscaVentanas("frmVentasPagosRemisiones") = False Then
            Dim f As New frmVentasPagosRemisiones("", 0, 0, "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub CambioDeUsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCambiarUsuario.Click
        CierraVentanas(True)
        Dim f As New frmCambioUsuario(0, 0)
        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            ChecaMenus()
            Versiones()
            Ribbon1.Text = "Empresa: " + GlobalNombreEmpresa + vbCrLf + "Usuario: " + GlobalUsuario
        End If

    End Sub


    Private Sub LicenciasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerConLicencias.Click
        CierraVentanas()
        If BuscaVentanas("frmLicencias") = False Then
            Dim f As New frmLicencias()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub BuscarNegativosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvHerBuscarNeg.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioAnalisis") = False Then
            Dim f As New frmInventarioAnalisis(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuImportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerConImportar.Click
        CierraVentanas()
        If BuscaVentanas("frmImportador") = False Then
            Dim f As New frmImportador()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    'Private Sub toolRemisiones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolRemisiones.Click
    '    CierraVentanas()
    '    If GlobalChecarConexion() = False Then
    '        MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
    '        Exit Sub
    '    End If
    '    If BuscaVentanas("frmVentasRemisiones") = False Then
    '        Dim Op As New dbOpciones(MySqlcon)
    '        If Op.MaximizarVentas = 0 Then
    '            Dim f As New frmVentasRemisiones()
    '            f.MdiParent = Me
    '            f.Show()
    '        Else
    '            GlobalEstadoVentanas = GlobalEstadoVentanas Or 8
    '            'toolstrip1.Visible = False
    '            Dim f As New frmVentasRemisiones()
    '            f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
    '            f.WindowState = FormWindowState.Maximized
    '            f.MdiParent = Me
    '            f.Show()
    '        End If
    '    End If
    'End Sub

    'Private Sub toolPagosRemisiones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolPagosRemisiones.Click
    '    CierraVentanas()
    '    If BuscaVentanas("frmVentasPagosRemisiones") = False Then
    '        Dim f As New frmVentasPagosRemisiones("", 0, 0, "")
    '        f.MdiParent = Me
    '        f.Show()
    '    End If
    'End Sub

    Private Sub mnuMovimientosCaja_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPunGenMovimientos.Click
        CierraVentanas()
        If BuscaVentanas("frmCajasMovimientos") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmCajasMovimientos(0, sa.idCaja, 0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

 

    'Private Sub ToolApartados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolApartados.Click
    '    CierraVentanas()
    '    If BuscaVentanas("frmVentasApartados") = False Then
    '        Dim f As New frmVentasApartados(0, 0, 0, 0)
    '        f.MdiParent = Me
    '        f.Show()
    '    End If
    'End Sub

    Private Sub mnuRecalcularInventario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvHerRecInventario.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioRCostos") = False Then
            Dim f As New frmInventarioRCostos(1)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ModelosToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcInvModelos.Click
        CierraVentanas()
        If BuscaVentanas("frmEstilosTallasColores") = False Then
            Dim f As New frmEstilosTallasColores(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuTallas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcInvTallas.Click
        CierraVentanas()
        If BuscaVentanas("frmEstilosTallasColores") = False Then
            Dim f As New frmEstilosTallasColores(1)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuColores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcInvColores.Click
        CierraVentanas()
        If BuscaVentanas("frmEstilosTallasColores") = False Then
            Dim f As New frmEstilosTallasColores(2)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub DepositosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBanOpeDepositos.Click
        CierraVentanas()
        If BuscaVentanas("frmDeposito") = False Then
            Dim f As New frmDeposito(0, "", "", "", "", 0, "", "", 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub PagoAProveedoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBanOpePagos.Click
        CierraVentanas()
        If BuscaVentanas("frmPagosProveedores") = False Then
            Dim f As New frmPagosProveedores(0, "", "", "", "", 0, "", "", 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ConsiliaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBanOpeConciliacion.Click
        CierraVentanas()
        If BuscaVentanas("frmConciliacion") = False Then
            Dim f As New frmBancosConciliacion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ReportesToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBanConReportes.Click
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

    Private Sub ReportesToolStripMenuItem_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerComReportes.Click
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

    Private Sub mnuDescuentos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcSerOfertas.Click
        CierraVentanas()
        If BuscaVentanas("frmDescuentos") = False Then
            Dim f As New frmDescuentos()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub



    Private Sub mnuTrabajadores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNomCatTrabajadores.Click
        CierraVentanas()
        If BuscaVentanas("frmTrabajadores") = False Then
            Dim f As New frmTrabajadores(0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub NominasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNomOpeNomina.Click
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

    'Private Sub ToolDevoluciones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolDevoluciones.Click
    '    CierraVentanas()
    '    If BuscaVentanas("frmDevoluciones") = False Then
    '        Dim f As New frmDevoluciones(0, 0, 0, 0, 0)
    '        f.MdiParent = Me
    '        f.Show()
    '    End If
    'End Sub

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

    Private Sub ModificarInventarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerHerModifiarInv.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioModificar") = False Then
            Dim f As New frmInventarioModificar()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub ReportesToolStripMenuItem_Click_3(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNomConReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmNominaReportes") = False Then
            Dim f As New frmNominaReportes()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    'Private Sub ToolNomina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolNomina.Click
    '    CierraVentanas()
    '    If BuscaVentanas("frmNominas") = False Then
    '        Dim f As New frmNominas(0)
    '        f.MdiParent = Me
    '        f.Show()
    '    End If
    'End Sub

    Private Sub mnuGastosClas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGasCatClasificaciones.Click
        CierraVentanas()
        If BuscaVentanas("frmGastosClasificacion") = False Then
            Dim f As New frmGastosClasificacion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuGastos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGasOpeGastos.Click
        CierraVentanas()
        If BuscaVentanas("frmGastos") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmGastos(0, sa.idCaja, 0, GlobalIdSucursalDefault)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ProgramarGastosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGasOpeProgramar.Click
        CierraVentanas()
        If BuscaVentanas("frmGastosProgramables") = False Then
            Dim f As New frmGastosProgramables()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpeniosConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpherConfiguracion.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosConfiguracion") = False Then
            Dim f As New frmEmpeniosConfiguracion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpeniosClas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpCatClasificaciones.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosClasificacion") = False Then
            Dim f As New frmEmpeniosClasificacion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpenios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpOpeEmpenos.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpenios") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmEmpenios(0, sa.idCaja, 0, GlobalIdSucursalDefault, "", -1, False)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpeniosPagos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpOpePagos.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosPagos2") = False Then
            Dim f As New frmEmpeniosPagos2()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuGastosAltaEmp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGasCatEmpleados.Click
        CierraVentanas()
        If BuscaVentanas("frmAltaEmpleados") = False Then
            Dim f As New frmAltaEmpleados()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub AltaTiposIdentificaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpCatIdentificaciones.Click
        CierraVentanas()
        If BuscaVentanas("frmAltaIdentificacion") = False Then
            Dim f As New frmAltaIdentificacion
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ConsultaDeMovimientosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpConConsultas.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosConsultaMovimientos") = False Then
            Dim f As New frmEmpeniosConsultaMovimientos
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub AdjudicacionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpOpeAdjudicaciones.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosAdjudicaciones") = False Then
            Dim f As New frmEmpeniosAdjudicaciones
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    'Private Sub toolEmpenios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolEmpenios.Click
    '    CierraVentanas()
    '    If GlobalChecarConexion() = False Then
    '        MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
    '        Exit Sub
    '    End If
    '    If BuscaVentanas("frmEmpenios") = False Then
    '        Dim sa As New dbSucursalesArchivos
    '        sa.DaOpciones(GlobalIdEmpresa, False)
    '        Dim f As New frmEmpenios(0, sa.idCaja, 0, GlobalIdSucursalDefault, "", -1, False)
    '        f.MdiParent = Me
    '        f.Show()
    '    End If
    'End Sub

    'Private Sub toolEmpeniosPagos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolEmpeniosPagos.Click
    '    CierraVentanas()
    '    If BuscaVentanas("frmEmpeniosPagos2") = False Then
    '        Dim f As New frmEmpeniosPagos2()
    '        f.MdiParent = Me
    '        f.Show()
    '    End If
    'End Sub

    Private Sub ReportesToolStripMenuItem_Click_4(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpConReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosReportes") = False Then
            Dim f As New frmEmpeniosReportes()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuGastosReportes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGasConReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmGastosReportes") = False Then
            Dim f As New frmGastosReportes()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpeniosCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpOpeCompras.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosCompras") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmEmpeniosCompras(0, sa.idCaja, 0, GlobalIdSucursalDefault, "", -1, False)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuGastosMovCajas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGasOpeMovimientos.Click
        CierraVentanas()
        If BuscaVentanas("frmCajasMovimientos") = False Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmCajasMovimientos(0, sa.idCaja, 0, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuEmpeniosCorte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEmpOpeCortes.Click
        CierraVentanas()
        If BuscaVentanas("frmEmpeniosCorte") = False Then
            Dim f As New frmEmpeniosCorte()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub InventarioRelacionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArcInvRelaciones.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioRelaciones") = False Then
            Dim f As New frmInventarioRelaciones()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub DistribuidoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHerConDistribuidores.Click
        CierraVentanas()
        If BuscaVentanas("frmDistribuidores") = False Then
            Dim f As New frmDistribuidores()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    'Private Sub toolEmpeniosCompras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolEmpeniosCompras.Click
    '    CierraVentanas()
    '    If BuscaVentanas("frmEmpeniosCompras") = False Then
    '        Dim sa As New dbSucursalesArchivos
    '        sa.DaOpciones(GlobalIdEmpresa, False)
    '        Dim f As New frmEmpeniosCompras(0, sa.idCaja, 0, GlobalIdSucursalDefault, "", -1, False)
    '        f.MdiParent = Me
    '        f.Show()
    '    End If
    'End Sub

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



    Private Sub mnuAltaEstados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerIntEstados.Click, btnSerComEstados.Click
        CierraVentanas()
        If BuscaVentanas("frmServiciosAltaEstado") = False Then
            Dim f As New frmServiciosAltaEstado()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuNuevoServicioI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerComNuevo.Click, btnSerIntNuevo.Click
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

    Private Sub mnuBuscarServicioI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerComBuscar.Click, btnSerIntBuscar.Click
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

    Private Sub mnuServiciosReportesI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerComReportes.Click, btnSerIntReporte.Click
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

    Private Sub mnuAltaEstadosServiciosI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerIntEstados.Click
        CierraVentanas()
        If BuscaVentanas("frmServiciosAltaEstado") = False Then
            Dim f As New frmServiciosAltaEstado()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub



    Private Sub mnuPolizas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConOpePolizas.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadPolizasN") = False Then
            Dim f As New frmContabilidadPolizasN(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuCuentasContablesCon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConCatCuentas.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadCuentas") = False Then
            Dim f As New frmContabilidadCuentas("", "", "", "", "")
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ConfiguraciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConOpeConfiguracion.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadConfig") = False Then
            Dim f As New frmContabilidadConfig
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuContaReportes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConConReportes.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadReportes") = False Then
            Dim f As New frmContabilidadReportes
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuContaConsultaSaldos_Click(sender As Object, e As EventArgs) Handles btnConOpeSaldos.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadConsultaSaldos") = False Then
            Dim f As New frmContabilidadConsultaSaldos
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    'Private Sub toolpolizas_Click(sender As Object, e As EventArgs) Handles toolpolizas.Click
    '    CierraVentanas()
    '    If GlobalChecarConexion() = False Then
    '        MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
    '        Exit Sub
    '    End If
    '    If BuscaVentanas("frmContabilidadPolizasN") = False Then
    '        Dim f As New frmContabilidadPolizasN(0)
    '        f.MdiParent = Me
    '        f.Show()
    '    End If
    'End Sub

    'Private Sub toolContaReportes_Click(sender As Object, e As EventArgs) Handles toolContaReportes.Click
    '    CierraVentanas()
    '    If BuscaVentanas("frmContabilidadReportes") = False Then
    '        Dim f As New frmContabilidadReportes
    '        f.MdiParent = Me
    '        f.Show()
    '    End If
    'End Sub

    'Private Sub toolFertilizantes_Click(sender As Object, e As EventArgs) Handles toolFertilizantes.Click
    '    CierraVentanas()
    '    If BuscaVentanas("frmFertilizantesPedido") = False Then
    '        Dim f As New frmFertilizantesPedido
    '        f.MdiParent = Me
    '        f.Show()
    '    End If
    'End Sub

    Private Sub mnuConciliacionDiot_Click(sender As Object, e As EventArgs) Handles btnConOpeConciliarDiot.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadDIOT") = False Then
            Dim f As New frmContabilidadDIOT
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub MáscarasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnConCatMascaras.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadMascaras") = False Then
            Dim f As New frmContabilidadMascaras
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuContaGenerarpolizas_Click(sender As Object, e As EventArgs) Handles btnConOpeGenerarPolizas.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadGeneraPolizas") = False Then
            Dim f As New frmContabilidadGeneraPolizas
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub mnuValidar_Click(sender As Object, e As EventArgs) Handles btnComConValidador.Click
        CierraVentanas()
        If BuscaVentanas("frmValidacion") = False Then
            Dim f As New frmValidacion
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuContaClaspoli_Click(sender As Object, e As EventArgs) Handles btnConCatClasifPolizas.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadAltaClas") = False Then
            Dim f As New frmContabilidadAltaClas
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub CorteGlobalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnVenConCorte.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.ReportesVer, PermisosN.Secciones.Ventas) = True Then
            CierraVentanas()
            If BuscaVentanas("frmVentasCorte") = False Then
                Dim f As New frmVentasCorte
                f.MdiParent = Me
                f.Show()
            End If
        End If
    End Sub

    Private Sub mnuBancosCuentas_Click(sender As Object, e As EventArgs) Handles btnBanCatCuentas.Click
        CierraVentanas()
        If BuscaVentanas("frmRegistroCuentas") = False Then
            Dim f As New frmRegistroCuentas()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuContabilidadConceptosNomina_Click(sender As Object, e As EventArgs) Handles btnConCatConceptos.Click
        CierraVentanas()
        If BuscaVentanas("frmContabilidadNominaCuentas") = False Then
            Dim f As New frmContabilidadNominaCuentas()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub BoletasToolStripMenuItem_Click(sender As Object, e As EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmSemillasBoleta") = False Then
            Dim f As New frmSemillasBoleta(GlobalSemillasResumida, GlobalPermisos.ChecaPermiso(PermisosN.Semillas.PrecioVerBoleta, PermisosN.Secciones.Semillas))
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub MnuSemillasLiquidaciones_Click(sender As Object, e As EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmSemillasLiquidacion") = False Then
            Dim f As New frmSemillasLiquidacion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuSemillasComprobantes_Click(sender As Object, e As EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmSemillasComprobante") = False Then
            Dim f As New frmSemillasComprobante()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuSemillasReportes_Click(sender As Object, e As EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmSemillasReportes") = False Then
            Dim f As New frmSemillasReportes(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuBoletasInventario_Click(sender As Object, e As EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmSemillasBoleta") = False Then
            Dim f As New frmSemillasBoleta(GlobalSemillasResumida, GlobalPermisos.ChecaPermiso(PermisosN.Semillas.PrecioVerBoleta, PermisosN.Secciones.Semillas))
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuReportesBoletasInv_Click(sender As Object, e As EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmSemillasReportes") = False Then
            Dim f As New frmSemillasReportes(1)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub MovimientosUsuariosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnHerHerMovimientosUsuario.Click
        CierraVentanas()
        If BuscaVentanas("frmAuditoriaUsuario") = False Then
            Dim f As New frmAuditoriaUsuario()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuInventarioAjusteCero_Click(sender As Object, e As EventArgs) Handles btnInvHerAjustarCero.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioRCostos") = False Then
            Dim f As New frmInventarioRCostos(2)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub ConfigurarConceptosInventarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnInvHerConfigConceptos.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioConfiguracionConceptos") = False Then
            Dim f As New frmInventarioConfiguracionConceptos
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub PedidosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnInvOpePedidos.Click
        CierraVentanas()
        If BuscaVentanas("frmInventarioPedidos") = False Then
            Dim f As New frmInventarioPedidos(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    
    Private Sub TecnicosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnArcSerTecnicos.Click
        CierraVentanas()
        If BuscaVentanas("frmTecnicos") = False Then
            Dim f As New frmTecnicos
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ClasificacionesDeServiciosToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles btnArcSerClasificaciones.Click
        CierraVentanas()
        If BuscaVentanas("frmServiciosClasificaciones") = False Then
            Dim f As New frmServiciosClasificaciones
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub MedidasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnArcGenMedidas.Click
        CierraVentanas()
        If BuscaVentanas("frmCantidades") = False Then
            Dim f As New frmCantidades
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub MonedasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnArcGenMonedas.Click
        CierraVentanas()
        If BuscaVentanas("frmMonedas") = False Then
            Dim f As New frmMonedas
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub ConceptosDeNotasVentasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnArcVenConceptosNotas.Click
        CierraVentanas()
        If BuscaVentanas("frmConceptosNotasVentas") = False Then
            Dim f As New frmConceptosNotasVentas()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub ConceptosDeNotasComprasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnArcComConceptosNotas.Click
        CierraVentanas()
        If BuscaVentanas("frmConceptosNotasCompras") = False Then
            Dim f As New frmConceptosNotasCompras()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub


    Private Sub TiposClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnArcVenTiposClientes.Click
        CierraVentanas()
        If BuscaVentanas("frmTiposCP") = False Then
            Dim f As New frmTiposCP(0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub TiposProveedoresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnArcComTiposProveedores.Click
        CierraVentanas()
        If BuscaVentanas("frmTiposCP") = False Then
            Dim f As New frmTiposCP(1)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuTiposSucursales_Click(sender As Object, e As EventArgs) Handles btnArcGenTiposSucursales.Click
        CierraVentanas()
        If BuscaVentanas("frmTiposCP") = False Then
            Dim f As New frmTiposCP(2)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuRestConfig_Click(sender As Object, e As EventArgs) Handles btnPunResConfiguracion.Click
        CierraVentanas()
        If BuscaVentanas("frmRestauranteConfiguracion") = False Then
            Dim f As New frmRestauranteConfiguracion()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuRestPrincipal_Click(sender As Object, e As EventArgs) Handles btnPunResVentas.Click
        CierraVentanas()
        If BuscaVentanas("frmRestaurantePrincipal") = False Then
            Dim f As New frmRestaurantePrincipal()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub VerAlertasToolStripMenuItem_Click(sender As Object, e As EventArgs)
        CierraVentanas()
        If BuscaVentanas("frmGastosAviso") = False Then
            Dim f As New frmGastosAviso()
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub mnuConsultarPedidos_Click(sender As Object, e As EventArgs) Handles btnInvConMonitor.Click

        CierraVentanas()
        If BuscaVentanas("frmInventarioPedidosConsulta") = False Then
            Dim f As New frmInventarioPedidosConsulta(0, True, 0)
            f.MdiParent = Me
            f.Show()
        End If
    End Sub

    Private Sub MostrarBarraDeHerramientasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnPunGenHerramientas.Click
        Ribbon1.Visible = True
    End Sub

  
End Class
