Public Class frmAuditoriaUsuario
    Private IdsUsuarios As New elemento
    Private idUsuario As Integer
    Private auditoria As New dbAuditoriaUsuario(MySqlcon)
    Private vista As New DataTable
    Private usuario As New dbUsuarios(MySqlcon)
    Private intentos As New dbIntentosLogin(MySqlcon)
    Private modulos As New List(Of String)
    Dim dt As DataTable

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        llenaListaModulos()
        ComboBox1.DataSource = modulos
        Dim f As Date = dtpDesde.Value.Date
        ' dtpDesde.Value = CDate(f.Year.ToString() + "/01/01")
        LlenaCombos("tblusuarios", ComboUsuarios, "nombre", "nombren", "idUsuario", IdsUsuarios, "", "Todos")
    End Sub

    Private Sub ComboUsuarios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboUsuarios.SelectedIndexChanged
        idUsuario = IdsUsuarios.Valor(ComboUsuarios.SelectedIndex)
        generarAuditoria()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        generarAuditoria()
    End Sub

    Private Sub frmAuditoriaUsuario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
            dgvAuditoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            'dgvAuditoria.Columns("Referencia").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvIntentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Catch ex As Exception

        End Try
    End Sub

    Private Sub llenaGrids()

        vista = New DataTable
        'vista.Columns.Add("Fecha Alta", GetType(String)) '2
        'vista.Columns.Add("Hora Alta", GetType(String)) '3 
        vista.Columns.Add("Fecha", GetType(String))
        vista.Columns.Add("Hora", GetType(String))
        vista.Columns.Add("Acción", GetType(String))
        vista.Columns.Add("Módulo", GetType(String)) '9
        vista.Columns.Add("Referencia", GetType(String))
        vista.Columns.Add("Usuario", GetType(String)) '10

        dt = auditoria.modificacionesClientes(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clientes", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clientes", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesProveedores(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Proveedores", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Proveedores", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesVendedores(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Vendedores", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Vendedores", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesArticulos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Artículos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Artículos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesTrabajadores(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Trabajadores", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Trabajadores", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesCajas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Cajas", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Cajas", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesAlmacenes(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Almacenes", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Almacenes", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesSucursales(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Sucursales", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Sucursales", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesTiposClientes(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Tipos clientes", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Tipos Clientes", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesZonas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Zonas", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Zonas", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesTiposCantidades(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Tipos Cantidades", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Tipos Cantidades", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesModelos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Modelos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Modelos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesTiposFormasPagos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Formas de pago", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Formas de pago", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesConceptosInventario(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Conceptos de inventario", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Conceptos de inventario", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesDepartamentos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Departamentos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Departamentos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesDepartamentosAreas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Áreas departamentos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Áreas departamentos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesClasificaciones1(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificaciones nivel 1", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasficaciones nivel 1", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesClasificaciones2(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificaciones nivel 2", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificaciones nivel 2", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesClasificaciones3(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificaciones nivel 3", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasficaciones nivel 3", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesMonedas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))

        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Monedas", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Monedas", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesCodigosBarras(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Código de barras", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Código de barras", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesDescuentos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Descuentos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Descuentos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesTallas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Tallas", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Tallas", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesColores(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Colores", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Colores", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesTecnicos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Técnicos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Técnicos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesProveedoresCuentas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Cuentas proveedores", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Cuentas proveedores", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesClientesCuentas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Cuentas Clientes", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Cuentas Clientes", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesClientesEquipos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Equipos Clientes", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Equipos Clientes", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesConceptosNotasCompras(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Conceptos notas de compras", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Conceptos notas de compras", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesConceptosNotasVentas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Conceptos notas de ventas", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Conceptos notas de ventas", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesVentas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesVentasPedidos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas pedidos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas pedidos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesVentasRemisioes(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas remisiones", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas remisiones", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesVentasCotizaciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas cotizaciones", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas cotizaciones", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesVentasApartados(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas apartados", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas apartados", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesVentasPagos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas pagos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "ventas pagos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesVentasNotasDeCargo(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas notas de cargo", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas notas de cargo", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesVentasNotasDeCredito(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas notas de crédito", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas notas de crédito", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesCompras(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesComprasCotizaciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras cotizaciones", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras cotizaciones", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesComprasPedidos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras pedidos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras pedidos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesComprasRemisiones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras remisiones", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras remisiones", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesComprasNotasDeCargo(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras notas de cargo", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras notas de cargo", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesComprasNotasDeCredito(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras notas de crédito", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras notas de crédito", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesMovimientosInventario(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Movimientos de inventario", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Movimientos de inventario", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesMovimientosCajas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Movimientos de caja", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Movimientos de caja", row("referencia"), row("usuario"))
            End If
        Next
        'dt = auditoria.modificacionesBancos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        'For Each row As DataRow In dt.Rows
        '    vista.Rows.Add(row("Alta"), row("horaAlta"), row("referencia"), row("fecha"), row("hora"), row("tipo"), "Bancos", row("usuario"))
        'Next
        dt = auditoria.modificacionesNominas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Nóminas", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Nóminas", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesEmpenios(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Empeños", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Empeños", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesEmpenosConfiguracion(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Empeños configuración", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Empeños configuración", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesEmpeniosCompras(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Empeños compras", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Empeños compras", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesEmpeniosAdjudicaciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Empeños adjudicaciones", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Empeños adjudicaciones", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesEmpeniosPagos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Empeños pagos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Empeños pagos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesContabilidadConfiguracion(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Contabilidad configuración", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Contabilidad configuración", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesContabilidadCatalogoCuentas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Catálogo de cuentas", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Catálogo de cuentas", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesContabilidadPolizas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Contabilidad pólizas", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Contabilidad pólizas", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesFertlizantesPedidos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Fertilizantes", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Fertilizantes pedidos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesFertlizantesMovimientos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Fertilizantes movimientos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Fertilizantes movimientos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesSemillasBoletas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Boletas", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Boletas", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesSemillasLiquidaciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Liquidaciones", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Liquidaciones", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesSemillasComprobantes(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Comprobantes", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Comprobantes", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesClientesImpuetos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Impuestos Clientes", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Impuestos Clientes", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesSucursalesCertificados(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Certificados sucursales", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Certificados sucursales", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesSucursalesFolios(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Fólios sucursales", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Fólios sucursales", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesSucursalesEquipos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Equipos sucursales", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Equipos sucursales", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesDetallesEquipos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Detalles de equipos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Detalles de equipos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesDetalleEquiposS(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Equipos sucursales detalles", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Equipos sucursales detalles", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesInventarioDetalles(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Detalles de inventario", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Detalles de inventario", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesInventarioRelaciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Relaciones de inventario", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Relaciones de inventario", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesFormasPagoRemisiones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Formas de pago remisiones", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Formas de pago remisiones", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesServiciosClasificaciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificaciones de servicios", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasficaciones de servicios", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesServiciosClasificaciones2(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificaciones de servicios 2", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasficaciones de servicios 2", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesUsuario(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Usuarios", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Usuarios", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesVentasPagosApartados(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas pagos apartados", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas pagos apartados", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesDocumentosClientes(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Documentos de clientes", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Documentos de clientes", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesDevoluciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Devoluciones", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Devoluciones", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesDevolucionesCompras(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Devoluciones compras", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Devoluciones compras", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesDocumentosProveedores(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Documentos de proveedores", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Documentos de proveedores", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesCuentas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Cuentas", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Cuentas", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesGastosClasificacion(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificación de gastos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificación de gastos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesGastosClasificacion2(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificación de gastos 2", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificación de gastos 2", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesGastosClasificacion3(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificación de gastos 3", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificación de gastos 3", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesGastos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Gastos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Gastos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesGastosProgramables(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Gastos programables", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Gastos programables", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesIdentificacion(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Identificación", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Identificación", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesEmpeniosClasificacion(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificación de empeños", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificación de empeños", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesContabilidadClas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificación de contabilidad", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificación ded contabilidad", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesContabilidadMascaras(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Contabilidad máscaras", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Contabilidad máscaras", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesOpciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Opciones", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Opciones", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesServicios(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Servicios", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Servicios", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesServiciosEventos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Servicios eventos", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Servicios eventos", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesServiciosEstados(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Servicios estados", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Servicios estados", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesServiciosSuc(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Servicios Sucursal", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Servicios Sucursal", row("referencia"), row("usuario"))
            End If
        Next
        dt = auditoria.modificacionesServiciosEventosSuc(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
        For Each row As DataRow In dt.Rows
            vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Servicios eventos sucursales", row("referencia"), row("usAlta"))
            If row("tipo") <> "Alta" Then
                vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Servicios eventos sucursales", row("referencia"), row("usuario"))
            End If
        Next
        Dim dv As New DataView(vista)
        dv.Sort = "Fecha,Hora,Referencia"
        dgvAuditoria.DataSource = dv
        dgvAuditoria.Columns("Referencia").Width = 330
        'dgvAuditoria.Columns(0).Width = 80
        dgvAuditoria.Columns(1).Width = 80
    End Sub

    Private Sub llenaGridIntentos()
        dgvIntentos.DataSource = intentos.vistaIntentos(idUsuario)
    End Sub

    Private Sub llenaListaModulos()
        modulos.Add("Todos")
        modulos.Add("Catálogos")
        ' modulos.Add("Clientes")
        ' modulos.Add("Proveedores")
        ' modulos.Add("Vendedores")
        modulos.Add("Articulos")
        modulos.Add("Inventario")
        ' modulos.Add("Trabajadores")
        ' modulos.Add("Cajas")
        ' modulos.Add("Almacenes")
        ' modulos.Add("Sucursales")
        ' modulos.Add("Zonas")
        ' modulos.Add("Tipos Cantidades")
        ' modulos.Add("Formas de pago")
        ' modulos.Add("Departamentos")
        ' modulos.Add("Áreas departamentos")
        modulos.Add("Clasificaciones")
        ' modulos.Add("Monedas")
        modulos.Add("Código de barras")
        ' modulos.Add("Descuentos")
        ' modulos.Add("Técnicos")
        modulos.Add("Compras")
        modulos.Add("Ventas")
        modulos.Add("Nóminas")
        modulos.Add("Bancos")
        modulos.Add("Empeños")
        modulos.Add("Fertilizantes")
        modulos.Add("Semillas")
        modulos.Add("Modelos, Tallas y Colores")
        modulos.Add("Servicios")
        modulos.Add("Contabilidad")
        modulos.Add("Empeños")
        modulos.Add("Gastos")
    End Sub

    Private Sub generarAuditoria()
        filtrar(ComboBox1.SelectedItem.ToString())
        llenaGridIntentos()
    End Sub

    Private Sub dtpDesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpDesde.ValueChanged
        generarAuditoria()
    End Sub

    Private Sub dtpHasta_ValueChanged(sender As Object, e As EventArgs) Handles dtpHasta.ValueChanged
        generarAuditoria()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim ds As New DataSet
        ds.Tables.Add(vista)
        Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rep = New repAuditoriaUsuario
        rep.SetDataSource(ds)
        rep.SetParameterValue("nombreEmpresa", GlobalNombreEmpresa)
        If idUsuario > 0 Then
            Dim u As New dbUsuarios(idUsuario, MySqlcon)
            rep.SetParameterValue("usuario", u.NombreUsuario)
        Else
            rep.SetParameterValue("usuario", "Todos")
        End If
        rep.SetParameterValue("desde", dtpDesde.Value.ToString("yyyy/MM/dd"))
        rep.SetParameterValue("hasta", dtpHasta.Value.ToString("yyyy/MM/dd"))
        Dim RV As New frmReportes(rep, False)
        RV.Show()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        filtrar(ComboBox1.SelectedItem.ToString())
    End Sub

    Private Sub filtrar(ByVal modulo As String)
        vista = New DataTable
        'vista.Columns.Add("Fecha Alta", GetType(String))
        'vista.Columns.Add("Hora Alta", GetType(String))
        vista.Columns.Add("Fecha", GetType(String))
        vista.Columns.Add("Hora", GetType(String))
        vista.Columns.Add("Acción", GetType(String))
        vista.Columns.Add("Módulo", GetType(String))
        vista.Columns.Add("Referencia", GetType(String))
        vista.Columns.Add("Usuario", GetType(String))
        Select Case modulo
            Case "Todos"
                llenaGrids()
            Case "Catálogos"
                dt = auditoria.modificacionesClientes(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clientes", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clientes", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesClientesImpuetos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Impuestos clientes", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Impuestos clientes", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesClientesCuentas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Cuentas clientes", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Cuentas clientes", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesClientesEquipos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Equipos clientes", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Equipos clientes", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesTiposClientes(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Tipos clientes", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Tipos clientes", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesProveedores(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Proveedores", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Proveedores", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesProveedoresCuentas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Cuentas proveedores", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Cuentas proveedores", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesTrabajadores(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Trabajadores", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Trabajadores", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesVendedores(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Vendedores", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Vendedores", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesZonas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Zonas", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Zonas", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesCajas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Cajas", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Cajas", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesMovimientosCajas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Movimientos de cajas", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Movimientos de cajas", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesAlmacenes(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Alamecenes", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Almacenes", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesSucursales(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Sucursales", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Sucursales", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesTiposCantidades(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Tipos cantidades", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Tipos cantidades", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesDepartamentosAreas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Áreas departamentos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Áreas departamentos", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesTecnicos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Técnicos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Técnicos", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesMonedas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Monedas", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Monedas", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Articulos"
                dt = auditoria.modificacionesArticulos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Artículos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Artículos", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Inventario"
                dt = auditoria.modificacionesConceptosInventario(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "conceptos de inventario", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "conceptos de inventario", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesMovimientosInventario(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Movimientos de inventario", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Movimientos de inventario", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Clasificaciones"
                dt = auditoria.modificacionesClasificaciones1(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificaciones nivel 1", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificaciones nivel 1", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesClasificaciones2(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasficaciones nivel 2", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificaciones nivel 2", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesClasificaciones3(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasficaciones nivel 3", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificaciones nivel 3", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Nóminas"
                dt = auditoria.modificacionesNominas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Nóminas", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Nóminas", row("referencia"), row("usuario"))
                    End If
                Next
                'Case "Bancos"
                '    dt = auditoria.modificacionesBancos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                '    For Each row As DataRow In dt.Rows
                '        vista.Rows.Add(row("Alta"), row("horaAlta"), row("referencia"), row("fecha"), row("hora"), row("tipo"), "Bancos", row("usuario"))
                '    Next
            Case "Compras"
                dt = auditoria.modificacionesCompras(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesComprasCotizaciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras cotizaciones", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras cotizaciones", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesComprasPedidos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras pedidos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras pedidos", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesComprasRemisiones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras remisiones", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras remisiones", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesComprasNotasDeCargo(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras notas de cargo", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras notas de cargo", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesComprasNotasDeCredito(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras notas de crédito", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Compras notas de crédito", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesConceptosNotasCompras(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Compras notas de compras", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Conceptos notas de compras", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Ventas"
                dt = auditoria.modificacionesConceptosNotasVentas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Conceptos notas de ventas", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Conceptos notas de ventas", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesVentas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesVentasPedidos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas pedidos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas pedidos", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesVentasRemisioes(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas remisiones", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas remisiones", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesVentasCotizaciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas cotizaciones", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas cotizaciones", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesVentasApartados(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas Apartados", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas Apartados", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesVentasPagos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas pagos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas pagos", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesVentasNotasDeCargo(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas notas de cargo", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas notas de cargo", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesVentasNotasDeCredito(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Ventas notas de crédito", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Ventas notas de crédito", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Contabilidad"
                dt = auditoria.modificacionesContabilidadConfiguracion(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Contabilidad configuración", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Contabilidad configuración", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesContabilidadCatalogoCuentas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Catálogo de cuentas", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Catálogo de cuentas", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesContabilidadPolizas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Contabilidad pólizas", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Contabilidad pólizas", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesContabilidadClas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasficación contabilidad", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificación contabilidad", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesContabilidadMascaras(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Contabilidad mascaras", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Contabilidad mascaras", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Empeños"
                dt = auditoria.modificacionesEmpenios(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Empeños", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Empeños", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesEmpenosConfiguracion(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Empeños configuración", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Empeños configuración", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesEmpeniosCompras(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Empeños compras", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Empeños compras", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesEmpeniosAdjudicaciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Empeños adjudicaciones", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Empeños adjudicaciones", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesEmpeniosPagos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Empeños pagos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Empeños pagos", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Fertilizantes"
                dt = auditoria.modificacionesFertlizantesPedidos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Fertilizantes pedidos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Fertilizantes pedidos", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesFertlizantesMovimientos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Fertilizantes movimientos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Fertilizantes movimientos", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Semillas"
                dt = auditoria.modificacionesSemillasBoletas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Boletas", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Boletas", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesSemillasLiquidaciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Liquidaciones", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Liquidaciones", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesSemillasComprobantes(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Comprobantes", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Comprobantes", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Modelos, Tallas y Colores"
                dt = auditoria.modificacionesModelos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Modelos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Modelos", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesColores(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Colores", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Colores", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesTallas(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Tallas", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Tallas", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Descuentos"
                dt = auditoria.modificacionesDescuentos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Descuentos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Descuentos", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Codigos de barra"
                dt = auditoria.modificacionesCodigosBarras(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Código de barras", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Código de barras", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Servicios"
                dt = auditoria.modificacionesServicios(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Servicios", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Servicios", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesServiciosEventos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Servicios eventos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Servicios eventos", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesServiciosEstados(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Servicios estados", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Servicios estados", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesServiciosSuc(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Servicios sucursal", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Servicios sucursal", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesServiciosEventosSuc(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Servicios eventos sucursales", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Servicios eventos sucursales", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesServiciosClasificaciones(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificaciones de servicios", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificaciones de servicios", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesServiciosClasificaciones2(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasficaciones de servicios 2", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificaciones de Servicios 2", row("referencia"), row("usuario"))
                    End If
                Next
            Case "Gastos"
                dt = auditoria.modificacionesGastosClasificacion(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificación de gastos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificación de gastos", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesGastosClasificacion2(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasificación de gastos 2", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificación de gastos 2", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesGastosClasificacion3(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Clasficación de gastos 3", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Clasificación de gastos 3", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesGastos(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Gastos", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Gastos", row("referencia"), row("usuario"))
                    End If
                Next
                dt = auditoria.modificacionesGastosProgramables(idUsuario, dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                For Each row As DataRow In dt.Rows
                    vista.Rows.Add(row("Alta"), row("horaAlta"), "Alta", "Gastos programables", row("referencia"), row("usAlta"))
                    If row("tipo") <> "Alta" Then
                        vista.Rows.Add(row("fecha"), row("hora"), row("tipo"), "Gastos programables", row("referencia"), row("usuario"))
                    End If
                Next
        End Select
        Dim dv As New DataView(vista)
        dv.Sort = "Fecha,Hora,Referencia"
        dgvAuditoria.DataSource = dv
        'dgvAuditoria.DataSource = vista
        dgvAuditoria.Columns("Referencia").Width = 330
        'dgvAuditoria.Columns(0).Width = 80
        dgvAuditoria.Columns(1).Width = 80
    End Sub

    Private Function checaAlta(ByRef row As DataRow) As DataRow
        If row("tipo") = "Alta" Then

        End If
        Return row
    End Function
End Class