Imports MySql.Data.MySqlClient
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel


Public Class frmSemillasConsulta
    Dim conexion As MySqlConnection
    Dim ruta As String = " Server=localhost; user id=root; password=masterdb; database=semagros; port=3306;"
    Dim comm As MySqlCommand
    Private boletas As dbSemillasBoleta
    Public boleta As dbSemillasBoleta
    Dim proveedores As dbproveedores
    Dim id As Integer
    Dim proveedorDB As dbproveedores
    Dim clienteDB As dbClientes
    Dim productoDB As dbInventario
    Private liquidaciones As dbSemillasLiquidacion
    Public liquidacion As dbSemillasLiquidacion
    Dim totalSelecion As Integer = 0
    Private comprobantes As dbSemillasComprobante
    Public comprobante As dbSemillasComprobante
    Dim opciones As dbOpciones
    Dim ConsultaOn As Boolean = False
    Dim tipos() As String = {"Entrada", "Salida"}

    Public Enum tipoConsulta
        boletas = 1
        liquidaciones = 2
        comprobantes = 3
    End Enum
    Dim op As tipoConsulta
    ' Dim filtros() As String = {"Productor", "Producto", "Folio", "Fecha"}

    Public Sub New(ByVal tipo As tipoConsulta)
        op = tipo
        'conexion = New MySqlConnection(ruta)
        'conexion.Open()
        comm = New MySqlCommand
        comm.Connection = MySqlcon
        boletas = New dbSemillasBoleta(MySqlcon)
        liquidaciones = New dbSemillasLiquidacion(MySqlcon)
        comprobantes = New dbSemillasComprobante(MySqlcon)
        proveedorDB = New dbproveedores(MySqlcon)
        productoDB = New dbInventario(MySqlcon)
        opciones = New dbOpciones(MySqlcon)
        ConsultaOn = False
        InitializeComponent()
        If tipo = tipoConsulta.boletas Then
            comboTipo.Enabled = True
            comboTipo.Items.AddRange(tipos)
            comboTipo.SelectedIndex = 0
        End If
        If tipo = tipoConsulta.comprobantes Then
            ' btnBuscar.Enabled = False
            txtClaveProducto.Enabled = False
            btnBuscarProducto.Enabled = False
            btnExportar.Visible = True
            Me.Text = "Consulta de Comprobantes"
        End If
        If tipo = tipoConsulta.liquidaciones Then
            Me.Text = "consulta de liquidaciones"
        End If
        chckTiempo.Checked = GlobalConsultaTiempoReal
        'configuraGrid()


    End Sub

    Private Sub configuraGrid()
        'dgvBoletas.ReadOnly = True
        dgvBoletas.RowHeadersVisible = False
        dgvBoletas.Columns(0).Visible = True
        dgvBoletas.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(1).Visible = False
        dgvBoletas.Columns(2).HeaderText = "Folio"
        dgvBoletas.Columns(2).Width = 70
        dgvBoletas.Columns(2).ReadOnly = True
        dgvBoletas.Columns(3).HeaderText = "Fecha"
        dgvBoletas.Columns(3).Width = 100
        dgvBoletas.Columns(3).ReadOnly = True
        If comboTipo.SelectedItem.ToString.Chars(0) = "S" Then
            dgvBoletas.Columns(4).Visible = True
            dgvBoletas.Columns(4).HeaderText = "Destino"
            'dgvBoletas.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            dgvBoletas.Columns(4).Width = 250
            dgvBoletas.Columns(4).ReadOnly = True
        End If
        If comboTipo.SelectedItem.ToString.Chars(0) = "E" Then
            dgvBoletas.Columns(4).Visible = True
            dgvBoletas.Columns(4).HeaderText = "Productor"
            'dgvBoletas.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgvBoletas.Columns(4).Width = 250
            dgvBoletas.Columns(4).ReadOnly = True
        End If
        If comboTipo.SelectedItem.ToString.Chars(0) = "T" Then
            dgvBoletas.Columns(4).Visible = False
        End If
        dgvBoletas.Columns(5).HeaderText = "Producto"
        dgvBoletas.Columns(5).Width = 150
        dgvBoletas.Columns(5).ReadOnly = True
        dgvBoletas.Columns(6).HeaderText = "Peso final"
        dgvBoletas.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgvBoletas.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(6).ReadOnly = True
        dgvBoletas.Columns(7).ReadOnly = True
        dgvBoletas.Columns(7).HeaderText = "Tipo"
        dgvBoletas.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'dgvBoletas.Columns(14).HeaderText = "peso neto analizado"

    End Sub

    Private Sub configuraGridLiquidaciones()
        dgvBoletas.RowHeadersVisible = False
        'dgvBoletas.Columns(0).Visible = True
        dgvBoletas.Columns(0).Visible = True
        dgvBoletas.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(1).Visible = False
        dgvBoletas.Columns(2).HeaderText = "Fecha"
        'dgvBoletas.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(2).ReadOnly = True
        dgvBoletas.Columns(3).HeaderText = "Folio"
        'dgvBoletas.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(3).ReadOnly = True
        dgvBoletas.Columns(4).HeaderText = "Productor"
        dgvBoletas.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(4).ReadOnly = True
        dgvBoletas.Columns(5).HeaderText = "Producto"
        'dgvBoletas.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(5).ReadOnly = True
        dgvBoletas.Columns(6).HeaderText = "Total"
        dgvBoletas.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        ' dgvBoletas.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(6).ReadOnly = True
        dgvBoletas.Columns(7).HeaderText = "Estado"
        dgvBoletas.Columns(7).ReadOnly = True
    End Sub

    Private Sub configuraGridComprobantes()
        dgvBoletas.RowHeadersVisible = False
        'dgvBoletas.Columns(0).Visible = True
        dgvBoletas.Columns(0).Visible = True
        dgvBoletas.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(1).Visible = False
        dgvBoletas.Columns(2).HeaderText = "Fecha"
        'dgvBoletas.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(2).ReadOnly = True
        dgvBoletas.Columns(3).HeaderText = "Folio"
        'dgvBoletas.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(3).ReadOnly = True
        dgvBoletas.Columns(4).HeaderText = "Cliente"
        'dgvBoletas.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(4).ReadOnly = True
        dgvBoletas.Columns(5).HeaderText = "Vol. Fac."
        dgvBoletas.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgvBoletas.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(5).ReadOnly = True
        dgvBoletas.Columns(6).HeaderText = "Sup."
        dgvBoletas.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvBoletas.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        dgvBoletas.Columns(6).ReadOnly = True
        dgvBoletas.Columns(7).HeaderText = "Estado"
        dgvBoletas.Columns(7).ReadOnly = True

    End Sub
    'Private Sub actualizaGrid()
    '    If ConsultaOn Then
    '        Select Case op
    '            Case tipoConsulta.boletas
    '                dgvBoletas.DataSource = boletas.consultaBoletas
    '                configuraGrid()
    '            Case tipoConsulta.liquidaciones
    '                dgvBoletas.DataSource = liquidaciones.vistaLiquidaciones
    '                configuraGridLiquidaciones()
    '            Case tipoConsulta.comprobantes
    '                dgvBoletas.DataSource = comprobantes.vistaComprobantes()
    '                configuraGridComprobantes()
    '        End Select
    '    End If
    'End Sub

    Function exportarAExcel(ByVal ElGrid As DataGridView) As Boolean
        'Creamos las variables
        Dim exApp As New Application
        Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
        Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
        Try
            'Añadimos el Libro al programa, y la hoja al libro
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets.Add()
            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = ElGrid.ColumnCount
            Dim NRow As Integer = ElGrid.RowCount
            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                exHoja.Cells.Item(1, i) = ElGrid.Columns(i - 1).Name.ToString
                'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
            Next
            For Fila As Integer = 0 To NRow - 1
                For Col As Integer = 0 To NCol - 1
                    exHoja.Cells.Item(Fila + 2, Col + 1) = ElGrid.Rows(Fila).Cells(Col).Value
                Next
            Next
            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exHoja.Rows.Item(1).Font.Bold = 1
            exHoja.Rows.Item(1).HorizontalAlignment = 3
            exHoja.Columns.AutoFit()
            'Aplicación visible
            exApp.Application.Visible = True
            exHoja = Nothing
            exLibro = Nothing
            exApp = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al exportar a Excel")
            Return False
        End Try
        Return True
    End Function

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        comprobantes.exportarVarios(dtpFecha1.Value.ToString("yyyy/MM/dd"), dtpFecha2.Value.ToString("yyyy/MM/dd"))
        'If exportarAExcel(dgvBoletas) Then
        '    MsgBox("archivo exportado.")
        'Else
        '    MsgBox("no se pudo exportar el archivo")
        'End If
    End Sub

    Private Sub frmSemillasConsultaBoletas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'actualizaGrid()
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        dgvBoletas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        ConsultaOn = False
        dtpFecha1.Value = CDate(Format(Date.Now, "yyyy") + "/01/01")
        dtpFecha2.Value = Date.Now
        ConsultaOn = True
        Filtrar()
    End Sub

    Private Sub btnVer_Click(sender As Object, e As EventArgs) Handles btnVer.Click
        Select Case op
            Case tipoConsulta.boletas
                boleta = boletas.buscar(New dbSemillasBoleta(id))
                Dispose()
                'Dim frm As New frmSemillasBoleta(boleta)
                'frm.ShowDialog()
                'actualizaGrid()
            Case tipoConsulta.liquidaciones
                liquidacion = liquidaciones.obten(New dbSemillasLiquidacion(id))
                Dispose()
                'Dim frm As New frmSemillasLiquidacion(liquidacion)
                'frm.ShowDialog()
                'actualizaGrid()
            Case tipoConsulta.comprobantes
                comprobante = comprobantes.buscar(id)
                Dispose()
                'Dim frm As New frmSemillasComprobante(comprobante)
                'frm.ShowDialog()
                'actualizaGrid()
        End Select
        'actualizaGrid()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If MsgBox("¿Eliminar los elementos seleccionados?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
            Exit Sub
        End If
        Select Case op
            Case tipoConsulta.boletas
                If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.BoletasBaja, PermisosN.Secciones.Semillas) = False Then
                    MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    Exit Sub
                End If
                Dim boleta As New dbSemillasBoleta()
                For i As Integer = 0 To dgvBoletas.Rows.Count - 1
                    If Convert.ToBoolean(dgvBoletas.Rows(i).Cells("seleccion").Value) Then
                        boleta.id = Integer.Parse(dgvBoletas.Rows(i).Cells("id").Value)
                        boletas.elimina(boleta)
                    End If
                Next
                Filtrar()
            Case tipoConsulta.liquidaciones
                If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.LiquidacionBaja, PermisosN.Secciones.Semillas) = False Then
                    MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    Exit Sub
                End If
                Dim liquidacion As New dbSemillasLiquidacion()
                For i As Integer = 0 To dgvBoletas.Rows.Count - 1
                    If Convert.ToBoolean(dgvBoletas.Rows(i).Cells("seleccion").Value) Then
                        liquidacion.id = Integer.Parse(dgvBoletas.Rows(i).Cells("id").Value)
                        liquidaciones.eliminar(liquidacion)
                    End If
                Next
                Filtrar()
            Case tipoConsulta.comprobantes
                If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.ComprobanteBaja, PermisosN.Secciones.Semillas) = False Then
                    MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    Exit Sub
                End If
                Dim idComprobante As Integer
                For i As Integer = 0 To dgvBoletas.Rows.Count - 1
                    If Convert.ToBoolean(dgvBoletas.Rows(i).Cells("seleccion").Value) Then
                        idComprobante = Integer.Parse(dgvBoletas.Rows(i).Cells("id").Value)
                        comprobantes.eliminar(id)
                    End If
                Next
                Filtrar()
        End Select
    End Sub

    Private Sub dgvBoletas_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvBoletas.MouseClick
        id = Integer.Parse(dgvBoletas.CurrentRow.Cells(1).Value.ToString())
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Select Case op
            Case tipoConsulta.boletas
                If comboTipo.SelectedItem.ToString.Chars(0) = "E" Then
                    Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 1, False, False, False)
                    frm.ShowDialog()
                    If Not frm.Proveedor Is Nothing Then
                        Dim prov As dbproveedores = frm.Proveedor
                        txtFiltro.Text = prov.Nombre
                        txtClave.Text = prov.Clave
                        If chckTiempo.Checked Then
                            Filtrar()
                        End If
                    End If
                End If
                If comboTipo.SelectedItem.ToString.Chars(0) = "S" Then
                    Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 1, False, False, False)
                    frm.ShowDialog()
                    If Not frm.Proveedor Is Nothing Then
                        Dim cli As dbClientes = frm.Cliente
                        txtFiltro.Text = cli.Nombre
                        txtClave.Text = cli.Clave
                        If chckTiempo.Checked Then
                            Filtrar()
                        End If
                    End If
                End If
            Case tipoConsulta.comprobantes
                Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 1, False, False, False)
                frm.ShowDialog()
                If Not frm.Cliente Is Nothing Then
                    Dim cliente As dbClientes = frm.Cliente
                    txtFiltro.Text = cliente.Nombre
                    txtClave.Text = cliente.Clave
                    If chckTiempo.Checked Then
                        Filtrar()
                    End If
                End If
        End Select
    End Sub
    Private Sub porProductor(ByVal proveedor As dbproveedores)
        dgvBoletas.DataSource = boletas.consultaProductor(proveedor)
        configuraGrid()
    End Sub

    Private Sub porProducto(ByVal producto As dbInventario)
        dgvBoletas.DataSource = boletas.consultaProducto(producto)
        configuraGrid()
    End Sub



    Private Sub habilitaCampos(ByVal bandera As Boolean)
        txtClave.Enabled = bandera
        txtFiltro.Enabled = bandera
        btnBuscar.Enabled = bandera
    End Sub

    Private Sub limpiaCampos()
        txtClave.Text = ""
        txtFiltro.Text = ""
    End Sub

    Private Sub dgvBoletas_DoubleClick(sender As Object, e As EventArgs) Handles dgvBoletas.DoubleClick

        Select Case op
            Case tipoConsulta.boletas
                boleta = boletas.buscar(New dbSemillasBoleta(id))
                Dispose()
                'Dim frm As New frmSemillasBoleta(boleta)
                'frm.ShowDialog()
                'actualizaGrid()
            Case tipoConsulta.liquidaciones
                liquidacion = liquidaciones.obten(New dbSemillasLiquidacion(id))
                Dispose()
                'Dim frm As New frmSemillasLiquidacion(liquidacion)
                'frm.ShowDialog()
                'actualizaGrid()
            Case tipoConsulta.comprobantes
                comprobante = comprobantes.buscar(id)
                Dispose()
                'Dim frm As New frmSemillasComprobante(comprobante)
                'frm.ShowDialog()
                'actualizaGrid()
        End Select

    End Sub

    Private Sub frmSemillasConsultaBoletas_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Dispose()
        End If
    End Sub

    Private Sub Filtrar()
        If ConsultaOn Then
            Select Case op
                Case tipoConsulta.boletas
                    If comboTipo.SelectedItem.ToString.Chars(0) = "E" Then
                        dgvBoletas.DataSource = boletas.buscaProductoProductor(txtClave.Text, "", txtClaveProducto.Text, dtpFecha1.Value.ToString("yyyy/MM/dd"), dtpFecha2.Value.ToString("yyyy/MM/dd"), txtFolio.Text, comboTipo.SelectedItem.ToString.Chars(0))
                    End If
                    If comboTipo.SelectedItem.ToString.Chars(0) = "S" Then
                        dgvBoletas.DataSource = boletas.buscaProductoProductor("", txtClave.Text, txtClaveProducto.Text, dtpFecha1.Value.ToString("yyyy/MM/dd"), dtpFecha2.Value.ToString("yyyy/MM/dd"), txtFolio.Text, comboTipo.SelectedItem.ToString.Chars(0))
                    End If
                    configuraGrid()
                Case tipoConsulta.liquidaciones
                    dgvBoletas.DataSource = liquidaciones.filtroLiquidaciones(txtClaveProducto.Text, txtClave.Text, dtpFecha1.Value.ToString("yyyy/MM/dd"), dtpFecha2.Value.ToString("yyyy/MM/dd"), txtFolio.Text)
                    configuraGridLiquidaciones()
                Case tipoConsulta.comprobantes
                    dgvBoletas.DataSource = comprobantes.buscarCliente(txtClave.Text, dtpFecha1.Value.ToString("yyyy/MM/dd"), dtpFecha2.Value.ToString("yyyy/MM/dd"), txtFolio.Text)
                    configuraGridComprobantes()
            End Select
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnBuscarProducto.Click

        Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.Articulo, 0, False, False, False)
        frm.ShowDialog()
        If Not frm.Inventario Is Nothing Then
            Dim prod As dbInventario = frm.Inventario
            txtClaveProducto.Text = prod.Clave
            txtNombreProducto.Text = prod.Nombre
            If chckTiempo.Checked Then
                Filtrar()
            End If
        End If
    End Sub

    Private Sub txtClave_TextChanged(sender As Object, e As EventArgs) Handles txtClave.TextChanged
        txtFolio.Text = ""
        If chckTiempo.Checked Then
            If proveedorDB.BuscaProveedor(txtClave.Text) Then
                txtFiltro.Text = proveedorDB.Nombre
            Else
                txtFiltro.Text = ""
            End If
            Filtrar()
        End If
    End Sub

    Private Sub txtClaveProducto_TextChanged(sender As Object, e As EventArgs) Handles txtClaveProducto.TextChanged
        txtFolio.Text = ""
        If chckTiempo.Checked Then
            If productoDB.BuscaArticulo(txtClaveProducto.Text, 1, "") Then
                txtNombreProducto.Text = productoDB.Nombre
            Else
                txtNombreProducto.Text = ""
            End If
            Filtrar()
        End If
    End Sub

    Private Sub txtFolio_TextChanged(sender As Object, e As EventArgs) Handles txtFolio.TextChanged
        If chckTiempo.Checked Then
            Filtrar()
        End If
    End Sub
    'Private Sub filtraFolio()
    '    Select Case op
    '        Case tipoConsulta.boletas
    '            dgvBoletas.DataSource = boletas.vistaFolio(txtFolio.Text)
    '            configuraGrid()
    '        Case tipoConsulta.liquidaciones
    '            dgvBoletas.DataSource = liquidaciones.buscarPorFolio(txtFolio.Text)
    '            configuraGridLiquidaciones()
    '        Case tipoConsulta.comprobantes
    '            dgvBoletas.DataSource = comprobantes.buscaFolio(txtFolio.Text)
    '    End Select
    'End Sub



    Private Sub checaMultiSeleccion()
        If totalSelecion > 1 Then
            btnVer.Enabled = False
        Else
            btnVer.Enabled = True
        End If
    End Sub
    Private Sub checarSeleccion()
        For i As Integer = 0 To dgvBoletas.Columns.Count - 1
            If Convert.ToBoolean(dgvBoletas.CurrentRow.Cells("seleccion").Value) Then
                totalSelecion += 1
                checaMultiSeleccion()
            Else
                totalSelecion -= 1
                checaMultiSeleccion()
            End If
        Next
    End Sub

    Private Sub txtFolio_Enter(sender As Object, e As EventArgs) Handles txtFolio.Enter
        txtClave.Text = ""
        txtNombreProducto.Text = ""
        txtClaveProducto.Text = ""
        txtFiltro.Text = ""
    End Sub

    Private Sub txtClave_Enter(sender As Object, e As EventArgs) Handles txtClave.Enter
        txtFolio.Text = ""
    End Sub

    Private Sub txtClaveProducto_Enter(sender As Object, e As EventArgs) Handles txtClaveProducto.Enter
        txtFolio.Text = ""
    End Sub

    Private Sub btnBusqueda_Click(sender As Object, e As EventArgs) Handles btnBusqueda.Click
        Filtrar()
    End Sub

    Private Sub dtpFecha1_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha1.ValueChanged
        If chckTiempo.Checked Then Filtrar()
    End Sub

    Private Sub dtpFecha2_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha2.ValueChanged
        If chckTiempo.Checked Then Filtrar()
    End Sub

    Private Sub comboTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboTipo.SelectedIndexChanged
        txtClave.Text = ""
        txtClaveProducto.Text = ""
        Filtrar()
    End Sub

    Private Sub dgvBoletas_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvBoletas.CellFormatting
        Select Case op
            Case tipoConsulta.boletas
                If e.ColumnIndex = 6 Then
                    e.Value = Format(e.Value, "#,###,##0")
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            Case tipoConsulta.liquidaciones
                If e.ColumnIndex = 6 Then
                    e.Value = Format(e.Value, opciones._formatoTotal)
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
        End Select
    End Sub
End Class