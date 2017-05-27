Public Class PermisosN
    Public PermisosCatalogos As ULong
    Public PermisosCatalogos2 As ULong
    Public PermisosCompras As ULong
    Public PermisosInventario As ULong
    Public PermisosVentas As ULong
    Public PermisosHerramientas As ULong
    Public PermisosPuntodeVenta As ULong
    Public PermisosBancos As ULong
    Public PermisosServicios As ULong
    Public PermisosNomina As ULong
    Public PermisosGastos As ULong
    Public PermisosEmpenios As ULong
    Public PermisosContabilidad As ULong
    Public PermisosFertilizantes As ULong
    Public PermisosSemillas As ULong
    Public Sub New()
    End Sub
    Public Function ChecaPermiso(ByVal Permiso As ULong, ByVal Seccion As Integer) As Boolean
        Select Case Seccion
            Case Secciones.Catalagos
                If (PermisosCatalogos And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Catalagos2
                If (PermisosCatalogos2 And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Compras
                If (PermisosCompras And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Inventario
                If (PermisosInventario And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Ventas
                If (PermisosVentas And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Herramientas
                If (PermisosHerramientas And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.PuntodeVenta
                If (PermisosPuntodeVenta And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Bancos
                If (PermisosBancos And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Servicios
                If (PermisosServicios And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Nomina
                If (PermisosNomina And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Gastos
                If (PermisosGastos And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Empenios
                If (PermisosEmpenios And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Contabilidad
                If (PermisosContabilidad And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Fertilizantes
                If (PermisosFertilizantes And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Case Secciones.Semillas
                If (PermisosSemillas And CULng((Math.Pow(2, Permiso)))) <> 0 Then
                    Return True
                Else
                    Return False
                End If
        End Select
        Return False
    End Function
    Public Sub AsignaPermiso(ByVal Permiso As ULong, ByVal Seccion As Integer)
        Select Case Seccion
            Case Secciones.Catalagos
                PermisosCatalogos = PermisosCatalogos Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Catalagos2
                PermisosCatalogos2 = PermisosCatalogos2 Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Compras
                PermisosCompras = PermisosCompras Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Inventario
                PermisosInventario = PermisosInventario Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Ventas
                PermisosVentas = PermisosVentas Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Herramientas
                PermisosHerramientas = PermisosHerramientas Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.PuntodeVenta
                PermisosPuntodeVenta = PermisosPuntodeVenta Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Bancos
                PermisosBancos = PermisosBancos Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Servicios
                PermisosServicios = PermisosServicios Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Nomina
                PermisosNomina = PermisosNomina Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Gastos
                PermisosGastos = PermisosGastos Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Empenios
                PermisosEmpenios = PermisosEmpenios Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Fertilizantes
                PermisosFertilizantes = PermisosFertilizantes Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Contabilidad
                PermisosContabilidad = PermisosContabilidad Or CULng((Math.Pow(2, Permiso)))
            Case Secciones.Semillas
                PermisosSemillas = PermisosSemillas Or CULng((Math.Pow(2, Permiso)))
        End Select
    End Sub
    Public Enum Secciones
        Catalagos = 0
        Catalagos2 = 1
        Compras = 3
        Inventario = 4
        Ventas = 5
        Herramientas = 6
        PuntodeVenta = 7
        Bancos = 8
        Servicios = 9
        Nomina = 10
        Gastos = 11
        Empenios = 12
        Contabilidad = 13
        Fertilizantes = 14
        Semillas = 15
    End Enum
    Public Enum Catalogos

        VendedoresVer = 0
        VendedoresAlta = 1
        VendedoresBaja = 2
        VendedoresCambio = 3

        ProveedoresVer = 4
        ProveedoresAlta = 5
        ProveedoresBaja = 6
        ProveedoresCambio = 7

        SucursalesVer = 8
        SucursalesAlta = 9
        SucursalesBaja = 10
        SucursalesCambio = 11
        SucursalesFolio = 12
        SucursalesCertificados = 13

        ClientesVer = 14
        ClientesAlta = 15
        ClientesBaja = 16
        ClientesCambio = 17

        AlmacenesVer = 18
        AlmacenesAlta = 19
        AlmacenesBaja = 20
        AlmacenesCambio = 21

        ClasificacionesdeInventarioVer = 22
        ClasificacionesdeInventarioAlta = 23
        ClasificacionesdeInventarioBaja = 24
        ClasificacionesdeInventarioCambios = 25


        InventarioVer = 26
        InventarioAlta = 27
        InventarioBaja = 28
        InventarioCambio = 29

        ConceptosdeInventarioVer = 30
        ConceptosdeInventarioAlta = 31
        ConceptosdeInventarioBaja = 32
        ConceptosdeInventarioCambio = 33

        FormasdePagoVer = 34
        FormasdePagoAlta = 35
        FormasdePagoBaja = 36
        FormasdePagoCambio = 37

        FormasdePagoRemVer = 38
        FormasdePagoRemAlta = 39
        FormasdePagoRemBaja = 40
        FormasdePagoRemCambio = 41

        CajasVer = 42
        CajasAlta = 43
        CajasBaja = 44
        CajasCambio = 45

        MedidasVer = 46
        MedidasAlta = 47
        MedidasBaja = 48
        MedidasCambio = 49

        MonedasVer = 50
        MonedasAlta = 51
        MonedasBaja = 52
        MonedasCambio = 53

        ConceptosNotasComprasVer = 54
        ConceptosNotasComprasAlta = 55
        ConceptosNotasComprasBaja = 56
        ConceptosNotasComprasCambio = 57

        ConceptosNotasVentasVer = 58
        ConceptosNotasVentasAlta = 59
        ConceptosNotasVentasBaja = 60
        ConceptosNotasVentasCambio = 61

        

        '--------------------

        'ConversionesVer = 20
        'ConversionesAlta = 21
        'ConversionesBaja = 22
        'ConversionesCambio = 23

        

        

        

        'ListasdePrecioVer = 44
        'ListasdePrecioAlta = 45
        'ListasdePrecioBaja = 46
        'ListasdePrecioCambio = 47

        'ProductosVer = 52
        'ProductosAlta = 53
        'ProductoBaja = 54
        'ProductosCambio = 55

        'ProductosClasificacionesVer = 56
        'ProdcutosClasificacionesAlta = 57
        'ProductosClasificacionesBaja = 58
        'ProductosClasificacionesCambio = 59

    End Enum

    Public Enum Catalogos2
        
        ReportesVer = 0

        UsuariosVer = 1
        UsuariosAlta = 2
        UsuariosBaja = 3
        UsuariosCambio = 4

        EmpresasVer = 5
        EmpresasAlta = 6
        EmpresasBaja = 7
        EmpresasCambio = 8

        InventarioPrecios = 9

        ZonasVer = 10
        ZonasAlta = 11
        ZonasBaja = 12
        ZonasCambio = 13

        TecnicosVer = 14
        TecnicosAlta = 15
        TecnicosBaja = 16
        TecnicosCambio = 17
        OfertasVer = 18
        OfertasAlta = 19
        OfertasBaja = 20
        OfertasCambio = 21

        InventarioRelacionesVer = 22
        InventarioRelacionesAlta = 23
        InventarioRelacionesBaja = 24
        InventarioRelacionesCambio = 25

        ClientesDiasCredito = 26
        ClientesMontoCredito = 27
        ClientesTiposVer = 28
        ClientesTiposAlta = 29
        ClientesTiposCambio = 30
        ClientesTiposBaja = 31
        ProvTiposVer = 32
        ProvTiposAlta = 33
        ProvTiposCambio = 34
        ProvTiposBaja = 35
        SucTiposVer = 36
        SucTiposAlta = 37
        SucTiposCambio = 38
        SucTiposBaja = 39

    End Enum

    Public Enum Compras

        PedidosVer = 0
        PedidosAlta = 1
        PedidosCancelar = 2

        CotizacionesVer = 3
        CotizacionesAlta = 4
        CotizacionesCancelar = 5
        CotizacionesBaja = 6

        RemisionesVer = 7
        RemisionesAlta = 8
        RemisionesCancelar = 9
        'RemisionesModificar = 18

        ComprasVer = 10
        ComprasAlta = 11
        ComprasCancelar = 12
        ComprasModificar = 13
        'ComprasAutorizacion = 4

        DevolucionesVer = 14
        DevolucionesAlta = 15
        DevolucionesCancelar = 16

        NotasdeCreditoVer = 17
        NotasdeCreditoAlta = 18
        NotasdeCreditoCancelacion = 19

        NotasdeCargoVer = 20
        NotasdeCargoAlta = 21
        NotasdeCargoCancelacion = 22

        DocumentosProveedoresVer = 23
        DocumentosProveedoresAlta = 24
        DocumentosProveedoresCancelar = 25

        PagosVer = 26
        PagosAlta = 27
        PagosCancelar = 28

        Reportes = 29
        CambioSucursal = 30
        Consultas = 31
        PagosCambios = 32
        ComprasBaja = 33
        CambiodeAlmacen = 34
        CambiodeFechaPagos = 35
        '--------------

    End Enum
    Public Enum Inventario
        MovimientosVer = 0
        MovimientosAltaEntrada = 1
        MovimientosCancelarEntrada = 2
        MovimientosAltaSalidas = 3
        MovimientosCancelarSalida = 4
        MovimientosAltaAjuste = 5
        MovimientosCancelarAjuste = 6
        MovimientosAltaTraspaso = 7
        MovimientosCancelarTraspaso = 8
        RevisionVer = 9
        RevisionAlta = 10
        KardexVer = 11
        CambioSucursal = 12
        Reportes = 13
        RecalcularCostos = 14
        RecalcularInventarios = 15
        BuscarNegativos = 16
        CambiodeAlamcen = 17
        MovimientosEdicion = 18
        PedidosVer = 19
        PedidosAlta = 20
        PedidosCancelar = 21
        PedidosAutorizar = 22
        MovimientosSinInventario = 23

    End Enum

    Public Enum Ventas

        CotizacionesVer = 0
        CotizacionesAlta = 1
        CotizacionesCancelar = 2

        PedidosVer = 3
        PedidosAlta = 4
        PedidosCancelar = 5

        RemisionesVer = 6
        RemisionesAlta = 7
        RemisionesCancelar = 8

        VentasVer = 9
        VentasAlta = 10
        VentasCancelar = 11
        'VentasAutorizacion = 12

        DevolucionesVer = 12
        DevolucionesAlta = 13
        DevolucionesCancelar = 14

        NotasdeCreditoVer = 15
        NotasdeCreditoAlta = 16
        NotasdeCreditoCancelar = 17

        NotasdeCargoVer = 18
        NotasdeCargoAlta = 19
        NotasdeCargoCancelar = 20

        PagareVer = 21

        DocumentosClientesVer = 22
        DocumentosClientesAlta = 23
        DocumentosClientesCancelar = 24

        PagosVer = 25
        PagosAlta = 26
        PagosCancelar = 27

        PagosRemVer = 28
        PagosRemAlta = 29
        PagosRemCancelar = 30

        ReportesVer = 31
        Consultas = 32
        CambioSucursal = 33
        PagosCambios = 34
        PagosRemCambios = 35
        VentasApartadosVer = 36
        VentasApartadosAlta = 37
        VentasApartadosCancelar = 38
        CambiodeAlmacen = 39
        CambioDescripcion = 40
        CambiodePrecio = 41
        PermitirDescuento = 42
        CambiodeFolio = 43
        PermitirVentaBajoCosto = 44
        PermitirCambioFechaPagos = 45
        PermitirCambioFechaPagosRemisiones = 46
        PermitirCambiarFechaVentas = 47
        PermitirCambiarFechaRemisiones = 48
        PermitirPendienteVentas = 49
        PermitirPendientesRemisiones = 50
        PermitirVentasCredito = 51
        PermitirRemisionesCredito = 52
    End Enum
    Public Enum Herramientas
        OpcionesVer = 0
        OpcionesModificar = 1
        Reportemensual = 2
        DocumentosDesingVer = 3
        DocumentosDesingModificar = 4
        OpcionesCorreoVer = 5
        OpcionesCorreoMoficar = 6
        RespaldoVer = 7
        RestaurarVer = 8
        CambiodePrecios = 9
        Importador = 10
    End Enum

    Public Enum PuntodeVentas
        VentasVer = 0
        VentasAlta = 1
        VentasCancelar = 2
        ReportesVer = 3
        CambiarSucursal = 4
        CambiodePrecio = 5
        HacerDescuento = 6
        CajasmovimientosVer = 7
        CajasmovimientosAlta = 8
        CajasmovimientosCancelar = 9
        CajasPermitirCambioFecha = 10
        VentasBaja = 11
        CambioVendedor = 12
        CambioCaja = 13
        AsignarCantidad = 14
        'CambiodeAlamcen = 8
    End Enum

    Public Enum Bancos
        BancosVer = 0
        BancosAlta = 1
        BancosModificar = 2
        BancosEliminar = 3
        CuentasVer = 4
        CuentasAlta = 5
        CuentasModificar = 6
        CuentasEliminar = 7
        CuentasContablesVer = 8
        CuentasContablesAlta = 9
        CuentasContablesModificar = 10
        CuentasContablesEliminar = 11
        DepositosVer = 12
        DepositosAlta = 13
        DepositosCancelar = 14
        PagoProveedoresVer = 15
        PagoProveedoresAlta = 16
        PagoProveedoresCancelar = 17
        Consiliacion = 18
        Reportes = 19

    End Enum

    Public Enum Servicios
        ServicioGuardar = 0
        ServicioVer = 1

        ServiciosClasGuardar = 2
        ServiciosClasEliminar = 3
        ServiciosClasModificar = 4
        ServiciosClasVer = 5

        ServiciosConsultaVer = 6

        ServiciosDetallesModDetalles = 7
        ServiciosDetallesAgregarEstatus = 8
        ServiciosDetallesModificarEstatus = 9
        ServiciosDetallesEliminarEstatus = 10
        ServiciosDetallesVer = 11

        ServiciosAgregarArticuloAgregar = 12
        ServiciosAgregarArticuloEliminar = 13
        ServiciosAgregarArticuloModificar = 14
        ServiciosAgregarArticuloVer = 15

        ServiciosClientesEquiposGuardar = 16
        ServiciosClientesEquiposEliminar = 17
        ServiciosClientesEquiposModificar = 18

        SericiosDetallesEquiposAgregar = 19
        SericiosDetallesEquiposModificar = 20
        SericiosDetallesEquiposEliminar = 21
        SericiosDetallesEquiposVer = 22

        ServiciosVerHistorial = 23
        ServiciosVerDetalles = 24
        ServiciosVerConponentes = 25
        ServiciosVerReportes = 26

    End Enum
    Public Enum Nominas
        NominaVer = 0
        NominaAlta = 1
        NomminaCancelar = 2
        NominaImportar = 3
        CambiarSucursal = 4
        TrabajadoresVer = 5
        TrabajadoresAlta = 6
        TrabajadoresBaja = 7
        TrabajadoresCambios = 8
    End Enum

    Public Enum Gastos
        GastosClasificacionesVer = 0
        GastosClasificacionesAltas = 1
        GastosClasificacionesCambios = 2
        GastosClasificacionesBajas = 3
        GastosEmpleadosVer = 4
        GastosEmpleadosAlta = 5
        GastosEmpleadosCambios = 6
        GastosEmpleadosBaja = 7
        GastosVer = 8
        GastosAlta = 9
        GastosCancelar = 10
        GastosProgramarVer = 11
        GastosProgramarAlta = 12
        GastosProgramarCambios = 13
        GastosProgramarBajas = 14
        GastosReportesVer = 15
        GastosCambiarSucursal = 16
        GastosPermitirCambioFecha = 17
        GastosVerNotificaciones = 18
    End Enum
    Public Enum Empenios
        EmpeniosConfiguracionVer = 0
        EmpeniosConfiguracionAlta = 1
        EmpeniosIdentificacionVer = 2
        EmpeniosIdentificacionAlta = 3
        EmpeniosIdentificacionCambios = 4
        EmpeniosIdentificacionBaja = 5
        EmpeniosVer = 6
        EmpeniosAlta = 7
        EmpeniosCancelar = 8
        EmpeniosSobreValouPermitir = 9
        EmpeniosPagosVer = 10
        EmpeniosPAgosAlta = 11
        EmpeniosPagosCambios = 12
        EmpeniosPAgosBaja = 13
        EmpeniosConsultaMovVer = 14
        EmpeniosAdjudicacionesVer = 15
        EmpeniosAdjudicacionesAlta = 16
        EmpeniosReportesVer = 17
        EmpeniosCambiarSucursal = 18
        EmpeniosClasificacionesVer = 19
        EmpeniosClasificacionesAlta = 20
        EmpeniosClasificacionesCambios = 21
        EmpeniosClasificacionesBaja = 22
        EmpeniosComprasVer = 23
        EmpeniosComprasAlta = 24
        EmpeniosComprasCancelar = 25
        EmpeniosPermitirCambiarVendedor = 26
        VerCorte = 27
        NoLimitarEvaluo = 28
        PermitirDescuento = 29
        PermitirCambioFecha = 30
        PermitirExtraVer = 31
        PermitirExtraCambio = 32
    End Enum

    Public Enum Fertilizantes
        PedidosVer = 0
        PedidosAlta = 1
        PedidosCancelar = 2
        MovimientosVer = 3
        MovimientosAlta = 4
        MovimientosCambios = 5
        MovimientosCancelar = 6
        ReportesVer = 7
    End Enum

    Public Enum Contabilidad
        ConfiguracionVer = 0
        ConfiguracionModificar = 1
        ClasificacionesPolizasVer = 2
        ClasificacionesPolizasAlta = 3
        ClasificacionesPolizasCambios = 4
        ClasificacionesPolizasBaja = 5
        CuentasVer = 6
        CuentasAlta = 7
        CuentasModificar = 8
        CuentasBaja = 9
        PolizasVer = 10
        PolizasAlta = 11
        PolizasBaja = 12
        ConsultaSaldosVer = 13
        ConsiliaciondiotVer = 14
        ReportesVer = 15
        MascarasVer = 16
        MascarasAlta = 17
        MascarasCambios = 18
        MascarasBaja = 19
        GenerarPolizasPermitir = 20
        NominaConceptosVer = 21
        NominaConceptosAlta = 22
        VerPolizasGeneradas = 23
        VerFechaConta = 24
    End Enum
    Public Enum Semillas
        Configuracion = 0
        BoletasVer = 1
        BoletasAlta = 2
        BoletasCambios = 3
        BoletasBaja = 4
        ComprobanteVer = 5
        ComprobanteAlta = 6
        ComprobanteCambios = 7
        ComprobanteBaja = 8
        LiquidacionVer = 9
        LiquidacionAlta = 10
        LiquidacionCambios = 11
        LiquidacionBaja = 12
        ReportesVer = 13
        PrecioVerBoleta = 14
    End Enum
End Class
