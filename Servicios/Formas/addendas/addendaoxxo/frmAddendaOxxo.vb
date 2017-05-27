Imports System.Xml
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient
Imports System
Imports System.Xml.Schema
Imports System.IO

Public Class frmAddendaOxxo

    'Atributo y propiedad


    Dim xmlAddendaOxxo As New AddendaOxxo()
    Dim addendaService As New AddendaService()
    Const CAMPO_VACIO As String = "No debe quedar vacío"
    Const TIPO_DESCUENTO As String = "Debe seleccionar un Tipo de Descuento"
    Const LIMPIAR As String = ""
    Const FORMATO_99AAA As String = "Ingresar con formato - 99AAA"
    Const GUARDAR_REG = "¡El registro se ha guardado exitosamente!"
    Const ACTUALIZAR_REG = "¡La registro se ha actualizado exitosamente!"
    Const ERROR_APP = "¡Ha ocurrido un error en la aplicación"
    Const LENGTH = "El tamaño del campo debe ser: "
    Const ERROR_VAL = "Corregir información: "
    Dim IdVenta As Integer
    Public elementos As New Collection
    Public Moneda As String
    Public TipodeCambio As Double
    Public Serie As String
    Public Folio As String
    Public Doc As Byte
    Public Total As String
    Dim row As Integer
    Dim valXml As Boolean = True
    Dim errorValXml As String = ""
    Public XMLResultado As String
    Public AFuerzas As Boolean
    Public EsElectronica As Byte
    ''' <summary>
    ''' Método que tiene por función obtener los articulos para el gridArticulos a través del identificador de la addenda.
    ''' </summary>
    ''' <param name="idAddenda">Identificador de la addenda</param>
    ''' <remarks></remarks>
    Private Sub ObtenerDatosGrid(ByVal idAddenda As Integer)

        If gridArticulos.Rows.Count > 0 Then
            gridArticulos.Rows.Clear()
        End If

        Dim row As DataGridViewRow
        Dim cell As DataGridViewCell

        Dim da As New MySqlDataAdapter()
        Dim dtrDatos As DataTableReader = Nothing

        Dim ds As New DataSet
        Dim dt As New DataTable()
        Dim dtr As DataTableReader
        da = addendaService.obtenerArticulosPorAddenda(idAddenda)
        da.Fill(dt)
        ds.Tables.Add(dt)

        dtr = dt.CreateDataReader()

        Do While dtr.Read()
            row = New DataGridViewRow

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("id").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("pedidoAdicional").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("remision").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("fechaEntrega").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("crTienda").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("nombreTienda").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("numProducto").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("descripcion").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("unidadMedida").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("cantidad").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("noSerieProductos").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("porcIva").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("montoIva").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("porcIeps1").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("montoIeps1").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("porcIeps2").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("montoIeps2").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("porcIeps3").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("montoIeps3").ToString()
            row.Cells.Add(cell)

            cell = New DataGridViewTextBoxCell
            cell.Value = dtr("ImporteNeto").ToString()
            row.Cells.Add(cell)

            gridArticulos.Rows.Add(row)
        Loop

        'gridArticulos.Columns.Item()
        dtr.Close()



    End Sub

    ''' <summary>
    ''' Método que tiene por función crear un objeto tipo Articulo
    ''' </summary>
    ''' <returns>Un objeto tipo Articulo</returns>
    ''' <remarks></remarks>
    Private Function crearArticulo() As Articulo
        'Se crea variable de tipo Boolean para establecer si se creo el objeto, 
        'verdadero por lo menos 1 propiedad tiene valor y falso si no tiene 
        'ningun valor en cualquiera de sus propiedades
        Dim estadoArticulo As Boolean = False
        Dim articulo As New Articulo()

        articulo.Id = gIdArticulo

        If dtpFechaEntregaMercancia.Enabled Then
            articulo.fechaEntrega = If(dtpFechaEntregaMercancia.Text = "", "", dtpFechaEntregaMercancia.Text)
            estadoArticulo = True
        Else
            articulo.fechaEntrega = ""
        End If

        articulo.remision = If(txtNumeroRemision.Text = "", 0, Integer.Parse(txtNumeroRemision.Text))
        If articulo.remision <> 0 Then
            estadoArticulo = True
        End If

        articulo.pedidoAdicional = If(txtNumeroPedidoAdicional.Text = "", 0, Integer.Parse(txtNumeroPedidoAdicional.Text))
        If articulo.pedidoAdicional <> 0 Then
            estadoArticulo = True
        End If

        articulo.crTienda = If(txtCrDeTienda.Text = "", "", txtCrDeTienda.Text)
        If articulo.crTienda <> "" Then
            estadoArticulo = True
        End If

        articulo.nombreTienda = If(txtLugarDeEntrega.Text = "", "", txtLugarDeEntrega.Text)
        If articulo.nombreTienda <> "" Then
            estadoArticulo = True
        End If

        articulo.numProducto = If(txtIdentificadorProductoSku.Text = "", 0, Integer.Parse(txtIdentificadorProductoSku.Text))
        If articulo.numProducto <> 0 Then
            estadoArticulo = True
        End If

        articulo.descripcion = If(txtDescripcionArticulo.Text = "", "", txtDescripcionArticulo.Text)
        If articulo.descripcion <> "" Then
            estadoArticulo = True
        End If

        articulo.unidadMedida = If(txtUnidadMedida.Text = "", "", txtUnidadMedida.Text)
        If articulo.unidadMedida <> "" Then
            estadoArticulo = True
        End If

        articulo.cantidad = If(txtCantidadMercanciaEntregada.Text = "", 0, Double.Parse(txtCantidadMercanciaEntregada.Text))
        If articulo.cantidad <> 0 Then
            estadoArticulo = True
        End If

        articulo.noSerieProductos = If(txtNumeroSerie.Text = "", "", txtNumeroSerie.Text)
        If articulo.noSerieProductos <> "" Then
            estadoArticulo = True
        End If

        articulo.porcIva = If(txtPorcentajeIva.Text = "", 0, Double.Parse(txtPorcentajeIva.Text))
        If articulo.porcIva <> 0 Then
            estadoArticulo = True
        End If

        articulo.montoIva = If(txtMontoImpuestoIva.Text = "", 0, Double.Parse(txtMontoImpuestoIva.Text))
        If articulo.montoIva <> 0 Then
            estadoArticulo = True
        End If

        articulo.porcIeps1 = If(txtPorcentajeImpuestoIeps1.Text = "", 0, Double.Parse(txtPorcentajeImpuestoIeps1.Text))
        If articulo.porcIeps1 <> 0 Then
            estadoArticulo = True
        End If

        articulo.montoIeps1 = If(txtMontoImpuestoIeps1.Text = "", 0, Double.Parse(txtMontoImpuestoIeps1.Text))
        If articulo.montoIeps1 <> 0 Then
            estadoArticulo = True
        End If

        articulo.porcIeps2 = If(txtPorcentajeImpuestoIeps2.Text = "", 0, Double.Parse(txtPorcentajeImpuestoIeps2.Text))
        If articulo.porcIeps2 <> 0 Then
            estadoArticulo = True
        End If

        articulo.montoIeps2 = If(txtMontoImpuestoIeps2.Text = "", 0, Double.Parse(txtMontoImpuestoIeps2.Text))
        If articulo.montoIeps2 <> 0 Then
            estadoArticulo = True
        End If

        articulo.porcIeps3 = If(txtPorcentajeImpuestoIeps3.Text = "", 0, Double.Parse(txtPorcentajeImpuestoIeps3.Text))
        If articulo.porcIeps3 <> 0 Then
            estadoArticulo = True
        End If

        articulo.montoIeps3 = If(txtMontoImpuestoIeps3.Text = "", 0, Double.Parse(txtMontoImpuestoIeps3.Text))
        If articulo.montoIeps3 <> 0 Then
            estadoArticulo = True
        End If

        articulo.ImporteNeto = If(txtImporteNetoConImpuestosIncluidos.Text = "", 0, Double.Parse(txtImporteNetoConImpuestosIncluidos.Text))
        If articulo.ImporteNeto <> 0 Then
            estadoArticulo = True
        End If

        If Not estadoArticulo Then
            articulo = Nothing
        End If

        Return articulo
    End Function

    ''' <summary>
    ''' Método que tiene por función crear un objeto tipo Addenda
    ''' </summary>
    ''' <returns>Un objeto tipo Addenda</returns>
    ''' <remarks></remarks>
    Private Function crearAddenda() As AddendaOxxo
        Dim addendaOxxo As New AddendaOxxo()

        'CREAMOS EL OBJETO addendaOxxo
        Select Case cboClaseDocumento.SelectedIndex
            Case 0 'Factura
                addendaOxxo.claseDoc = 1
            Case 1 'Nota de cargo
                addendaOxxo.claseDoc = 2

            Case 2 'Nota de crédito
                addendaOxxo.claseDoc = 3
        End Select

        Select Case cboTipoProveedor.SelectedIndex
            Case 0 'DIRECTO
                addendaOxxo.tipoProv = 1
            Case 1 'CEDIS
                addendaOxxo.tipoProv = 2
            Case 2 'Activo Fijo
                addendaOxxo.tipoProv = 3
            Case 3 'Servicios
                addendaOxxo.tipoProv = 4
            Case 4 'Intercompañia
                addendaOxxo.tipoProv = 5
        End Select

        Select Case cboTipoLocalizacion.SelectedIndex
            Case -1 'Null
                addendaOxxo.locType = ""
            Case 0 'Tienda
                addendaOxxo.locType = "T"
            Case 1 ' CEDIS
                addendaOxxo.locType = "C"
            Case 2 'Plaza
                addendaOxxo.locType = "P"
        End Select

        Select Case cboTipoMoneda.SelectedIndex
            Case 0 'MXN
                addendaOxxo.moneda = "MXN"
            Case 1 'USD
                addendaOxxo.moneda = "USD"
        End Select

        Select Case cboTipoDescuento.SelectedIndex
            Case -1
                addendaOxxo.tipoDescuento0 = ""
            Case 0 'Distribucion
                addendaOxxo.tipoDescuento0 = "001"
            Case 1 'Pronto pago
                addendaOxxo.tipoDescuento0 = "002"
        End Select

        Select Case cboTipoDescuento1.SelectedIndex
            Case -1
                addendaOxxo.tipoDescuento1 = ""
            Case 0 'Distribucion
                addendaOxxo.tipoDescuento1 = "001"
            Case 1 'Pronto pago
                addendaOxxo.tipoDescuento1 = "002"
        End Select

        Select Case cboTipoDescuento2.SelectedIndex
            Case -1
                addendaOxxo.tipoDescuento2 = ""
            Case 0 'Distribucion
                addendaOxxo.tipoDescuento2 = "001"
            Case 1 'Pronto pago
                addendaOxxo.tipoDescuento2 = "002"
        End Select

        Select Case cboTipoDescuento3.SelectedIndex
            Case -1
                addendaOxxo.tipoDescuento3 = ""
            Case 0 'Distribucion
                addendaOxxo.tipoDescuento3 = "001"
            Case 1 'Pronto pago
                addendaOxxo.tipoDescuento3 = "002"
        End Select

        If Not txtNumeroDeVersion.Text = "" Then
            addendaOxxo.noVersAdd = Integer.Parse(txtNumeroDeVersion.Text)
        Else
            addendaOxxo.noVersAdd = 0
        End If

        If Not txtIdentificadorPlazaOxxo.Text = "" Then
            addendaOxxo.plaza = txtIdentificadorPlazaOxxo.Text
        Else
            addendaOxxo.plaza = ""
        End If

        If Not txtNumeroFolioPago.Text = "" Then
            addendaOxxo.folioPago = txtNumeroFolioPago.Text
        Else
            addendaOxxo.folioPago = ""
        End If

        If Not txtOrdenCompraOxxo.Text = "" Then
            addendaOxxo.ordCompra = Int64.Parse(txtOrdenCompraOxxo.Text)
        Else
            addendaOxxo.ordCompra = 0
        End If

        If Not txtGlnEmisorCfd.Text = "" Then
            'Dim aux As Long
            'aux = Int64.Parse()
            addendaOxxo.glnEmisor = txtGlnEmisorCfd.Text
        Else
            addendaOxxo.glnEmisor = 0
        End If

        If Not txtGlnReceptorCfd.Text = "" Then
            'Dim aux As Long
            'aux = Int64.Parse()
            addendaOxxo.glnReceptor = txtGlnReceptorCfd.Text
        Else
            addendaOxxo.glnReceptor = 0
        End If

        If Not txtTipoCambio.Text = "" Then
            addendaOxxo.tipoCambio = Double.Parse(txtTipoCambio.Text)
        Else
            addendaOxxo.tipoCambio = 0
        End If

        If Not txtSerie.Text = "" Then
            addendaOxxo.cfdReferenciaSerie = txtSerie.Text
        Else
            addendaOxxo.cfdReferenciaSerie = ""
        End If

        If Not txtFolio.Text = "" Then
            addendaOxxo.cfdReferenciaFolio = txtFolio.Text
        Else
            addendaOxxo.cfdReferenciaFolio = ""
        End If

        If Not txtMontoDescuento.Text = "" Then
            addendaOxxo.montoDescuento0 = Double.Parse(txtMontoDescuento.Text)
        Else
            addendaOxxo.montoDescuento0 = 0
        End If

        If Not txtMontoDescuento1.Text = "" Then
            addendaOxxo.montoDescuento1 = Double.Parse(txtMontoDescuento1.Text)
        Else
            addendaOxxo.montoDescuento1 = 0
        End If

        If Not txtMontoDescuento2.Text = "" Then
            addendaOxxo.montoDescuento2 = Double.Parse(txtMontoDescuento2.Text)
        Else
            addendaOxxo.montoDescuento2 = 0
        End If

        If Not txtMontoDescuento3.Text = "" Then
            addendaOxxo.montoDescuento3 = Double.Parse(txtMontoDescuento3.Text)
        Else
            addendaOxxo.montoDescuento3 = 0
        End If

        If Not txtImporteTotalPagar.Text = "" Then
            addendaOxxo.importeTotal = Double.Parse(txtImporteTotalPagar.Text)
        Else
            addendaOxxo.importeTotal = 0
        End If

        If Not txtTipoValidacion.Text = "" Then
            addendaOxxo.tipoValidacion = txtTipoValidacion.Text
        Else
            addendaOxxo.tipoValidacion = 1
        End If

        If Not txtFuenteNota.Text = "" Then
            addendaOxxo.fuenteNota = txtFuenteNota.Text
        Else
            addendaOxxo.fuenteNota = 1
        End If

        Return addendaOxxo
    End Function

    ''' <summary>
    ''' Método que tiene por función limpiar campos de la vista correspondiente a la parte del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub limpiarArticulo()

        errorGeneral.SetError(txtNumeroPedidoAdicional, LIMPIAR)
        errorGeneral.SetError(txtNumeroRemision, LIMPIAR)
        errorGeneral.SetError(dtpFechaEntregaMercancia, LIMPIAR)
        errorGeneral.SetError(txtCrDeTienda, LIMPIAR)
        errorGeneral.SetError(txtLugarDeEntrega, LIMPIAR)
        errorGeneral.SetError(txtIdentificadorProductoSku, LIMPIAR)
        errorGeneral.SetError(txtDescripcionArticulo, LIMPIAR)
        errorGeneral.SetError(txtUnidadMedida, LIMPIAR)
        errorGeneral.SetError(txtCantidadMercanciaEntregada, LIMPIAR)
        errorGeneral.SetError(txtNumeroSerie, LIMPIAR)
        errorGeneral.SetError(txtPorcentajeIva, LIMPIAR)
        errorGeneral.SetError(txtMontoImpuestoIva, LIMPIAR)
        errorGeneral.SetError(txtPorcentajeImpuestoIeps1, LIMPIAR)
        errorGeneral.SetError(txtMontoImpuestoIeps1, LIMPIAR)
        errorGeneral.SetError(txtPorcentajeImpuestoIeps2, LIMPIAR)
        errorGeneral.SetError(txtMontoImpuestoIeps2, LIMPIAR)
        errorGeneral.SetError(txtPorcentajeImpuestoIeps3, LIMPIAR)
        errorGeneral.SetError(txtMontoImpuestoIeps3, LIMPIAR)
        errorGeneral.SetError(txtImporteNetoConImpuestosIncluidos, LIMPIAR)

        txtNumeroPedidoAdicional.Text = ""
        txtNumeroRemision.Text = ""
        dtpFechaEntregaMercancia.Text = ""
        txtCrDeTienda.Text = ""
        txtLugarDeEntrega.Text = ""
        txtIdentificadorProductoSku.Text = ""
        txtDescripcionArticulo.Text = ""
        txtUnidadMedida.Text = ""
        txtCantidadMercanciaEntregada.Text = ""
        txtNumeroSerie.Text = ""
        txtPorcentajeIva.Text = ""
        txtMontoImpuestoIva.Text = ""
        txtPorcentajeImpuestoIeps1.Text = ""
        txtMontoImpuestoIeps1.Text = ""
        txtPorcentajeImpuestoIeps2.Text = ""
        txtMontoImpuestoIeps2.Text = ""
        txtPorcentajeImpuestoIeps3.Text = ""
        txtMontoImpuestoIeps3.Text = ""
        txtImporteNetoConImpuestosIncluidos.Text = ""

        'pnlNumeroPedidoAdicional.Enabled = False
        'pnlNumeroRemision.Enabled = False
        'pnlNumeroSerie.Enabled = False
        'pnlCrDeTienda.Enabled = False
        'pnlLugarDeEntrega.Enabled = False
        'pnlFechaEntregaMercancia.Enabled = False
        'pnlIdentificadorProductoSku.Enabled = False
        'pnlDescripcionArticulo.Enabled = False
        'pnlUnidadMedida.Enabled = False

        'txtMontoImpuestoIva.Enabled = False
        txtMontoImpuestoIeps1.Enabled = False
        txtMontoImpuestoIeps2.Enabled = False
        txtMontoImpuestoIeps3.Enabled = False

    End Sub

    ''' <summary>
    ''' Método que tiene por función limpiar campos de la vista correspondiente a la parte del encabezado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub limpiarAddenda()

        cboClaseDocumento.SelectedIndex = -1
        cboTipoProveedor.SelectedIndex = -1
        cboTipoLocalizacion.SelectedIndex = -1
        cboTipoMoneda.SelectedIndex = -1
        cboTipoDescuento.SelectedIndex = -1
        cboTipoDescuento1.SelectedIndex = -1
        cboTipoDescuento2.SelectedIndex = -1
        cboTipoDescuento3.SelectedIndex = -1

        txtNumeroDeVersion.Text = ""
        txtIdentificadorPlazaOxxo.Text = ""
        txtNumeroFolioPago.Text = ""
        txtOrdenCompraOxxo.Text = ""
        txtGlnEmisorCfd.Text = ""
        txtGlnReceptorCfd.Text = ""
        txtTipoCambio.Text = ""
        txtSerie.Text = ""
        txtFolio.Text = ""
        txtMontoDescuento.Text = ""
        txtMontoDescuento1.Text = ""
        txtMontoDescuento2.Text = ""
        txtMontoDescuento3.Text = ""
        txtImporteTotalPagar.Text = ""
        txtTipoValidacion.Text = ""
        txtFuenteNota.Text = ""

    End Sub

    ''' <summary>
    ''' Método que tiene por función cargar datos de objeto tipo Articulo en la vista de la parte del detalle
    ''' </summary>
    ''' <param name="row">Fila seleccionada de objeto gridArticulos</param>
    ''' <remarks></remarks>
    Private Sub cargarDatosDetalle(ByVal row As Integer)

        gIdArticulo = If(gridArticulos.Item(0, row).Value <> "", gridArticulos.Item(0, row).Value, 0)


        txtNumeroPedidoAdicional.Text = If(gridArticulos.Item(1, row).Value <> 0, CStr(gridArticulos.Item(1, row).Value).PadLeft(10, "0"), "")
        If txtNumeroPedidoAdicional.Text <> "" Then
            pnlNumeroPedidoAdicional.Enabled = True
        Else
        End If

        txtNumeroRemision.Text = If(gridArticulos.Item(2, row).Value <> 0, CStr(gridArticulos.Item(2, row).Value).PadLeft(6, "0"), "")
        If txtNumeroRemision.Text <> "" Then
            pnlNumeroRemision.Enabled = True
        End If

        Dim dt As String = If(gridArticulos.Item(3, row).Value <> "", gridArticulos.Item(3, row).Value, "")
        If dt <> "" Then
            dtpFechaEntregaMercancia.Text = CDate(dt)
            pnlFechaEntregaMercancia.Enabled = True
        End If

        txtCrDeTienda.Text = If(gridArticulos.Item(4, row).Value <> "", gridArticulos.Item(4, row).Value, "")
        If txtCrDeTienda.Text <> "" Then
            pnlCrDeTienda.Enabled = True
        End If

        txtLugarDeEntrega.Text = If(gridArticulos.Item(5, row).Value <> "", gridArticulos.Item(5, row).Value, "")
        If txtLugarDeEntrega.Text <> "" Then
            pnlLugarDeEntrega.Enabled = True
        End If

        txtIdentificadorProductoSku.Text = If(gridArticulos.Item(6, row).Value <> 0, gridArticulos.Item(6, row).Value, "0")
        If txtIdentificadorProductoSku.Text <> "" Then
            pnlIdentificadorProductoSku.Enabled = True
        End If

        txtDescripcionArticulo.Text = If(gridArticulos.Item(7, row).Value <> "", gridArticulos.Item(7, row).Value, "")
        If txtDescripcionArticulo.Text <> "" Then
            pnlDescripcionArticulo.Enabled = True
        End If

        txtUnidadMedida.Text = If(gridArticulos.Item(8, row).Value <> "", gridArticulos.Item(8, row).Value, "")
        If txtUnidadMedida.Text <> "" Then
            pnlUnidadMedida.Enabled = True
        End If

        txtCantidadMercanciaEntregada.Text = If(gridArticulos.Item(9, row).Value <> 0, gridArticulos.Item(9, row).Value, "")

        txtNumeroSerie.Text = If(gridArticulos.Item(10, row).Value <> "", gridArticulos.Item(10, row).Value, "")
        If txtNumeroSerie.Text <> "" Then
            pnlNumeroSerie.Enabled = True
        End If

        txtPorcentajeIva.Text = If(gridArticulos.Item(11, row).Value <> 0, gridArticulos.Item(11, row).Value, "0")

        txtMontoImpuestoIva.Text = If(gridArticulos.Item(12, row).Value <> 0, gridArticulos.Item(12, row).Value, "0")
        If txtMontoImpuestoIva.Text <> "" Then
            txtMontoImpuestoIva.Enabled = True
        End If

        txtPorcentajeImpuestoIeps1.Text = If(gridArticulos.Item(13, row).Value <> 0, gridArticulos.Item(13, row).Value, "")
        If txtPorcentajeImpuestoIeps1.Text <> "" Then
            txtPorcentajeImpuestoIeps1.Enabled = True
        End If

        txtMontoImpuestoIeps1.Text = If(gridArticulos.Item(14, row).Value <> 0, gridArticulos.Item(14, row).Value, "")
        If txtMontoImpuestoIeps1.Text <> "" Then
            txtMontoImpuestoIeps1.Enabled = True
        End If

        txtPorcentajeImpuestoIeps2.Text = If(gridArticulos.Item(15, row).Value <> 0, gridArticulos.Item(15, row).Value, "")
        If txtPorcentajeImpuestoIeps2.Text <> "" Then
            txtPorcentajeImpuestoIeps2.Enabled = True
        End If

        txtMontoImpuestoIeps2.Text = If(gridArticulos.Item(16, row).Value <> 0, gridArticulos.Item(16, row).Value, "")
        If txtMontoImpuestoIeps2.Text <> "" Then
            txtMontoImpuestoIeps2.Enabled = True
        End If


        txtPorcentajeImpuestoIeps3.Text = If(gridArticulos.Item(17, row).Value <> 0, gridArticulos.Item(17, row).Value, "")
        If txtPorcentajeImpuestoIeps3.Text <> "" Then
            txtPorcentajeImpuestoIeps3.Enabled = True
        End If

        txtMontoImpuestoIeps3.Text = If(gridArticulos.Item(18, row).Value <> 0, gridArticulos.Item(18, row).Value, "")
        If txtMontoImpuestoIeps3.Text <> "" Then
            txtMontoImpuestoIeps3.Enabled = True
        End If

        txtImporteNetoConImpuestosIncluidos.Text = If(gridArticulos.Item(19, row).Value <> 0, gridArticulos.Item(19, row).Value, "")


        btnAgregar.Text = "Actualizar artículo"
        btnAgregar.Enabled = True
    End Sub

    ''' <summary>
    ''' Método que tiene por funcion guardar o actualizar un objeto tipo Articulo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub guardarActualizarArticulo()
        Dim articulo As Articulo = crearArticulo()
        articulo.Id = gIdArticulo

        If articulo.Id <> 0 Then
            If validarCamposDetalle() Then
                If (addendaService.actualizarArticulo(articulo)) Then
                    'Se carga el grid con el nuevo registro
                    ObtenerDatosGrid(gIdAddenda)

                    'Se limpia el formulario
                    limpiarArticulo()

                    MsgBox(ACTUALIZAR_REG)
                End If
            End If
        Else
            If validarCamposDetalle() Then
                If (addendaService.guardarArticuloPorAddenda(articulo, gIdAddenda)) Then

                    'Se carga el grid con el nuevo registro
                    ObtenerDatosGrid(gIdAddenda)

                    'Se limpia el formulario
                    limpiarArticulo()

                    MsgBox(GUARDAR_REG)

                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que tiene por función crear una lista de tipo Articulo a partir de las filas o registros del objeto gridArticulos
    ''' </summary>
    ''' <returns>Una lista de tipo Articulo</returns>
    ''' <remarks></remarks>
    Private Function crearArticulosGrid() As Collection
        Dim articulos As New Collection

        For Each fila As DataGridViewRow In gridArticulos.Rows
            Dim art As New Articulo
            art.pedidoAdicional = If(fila.Cells(1).Value <> 0, fila.Cells(1).Value, 0)
            art.remision = If(fila.Cells(2).Value <> 0, fila.Cells(2).Value, 0)
            art.fechaEntrega = If(fila.Cells(3).Value <> "", fila.Cells(3).Value, "")
            art.crTienda = If(fila.Cells(4).Value <> "", fila.Cells(4).Value, "")
            art.nombreTienda = If(fila.Cells(5).Value <> Nothing, fila.Cells(5).Value, "")
            art.numProducto = If(fila.Cells(6).Value <> 0, fila.Cells(6).Value, 0)
            art.descripcion = If(fila.Cells(7).Value <> Nothing, fila.Cells(7).Value, "")
            art.unidadMedida = If(fila.Cells(8).Value <> Nothing, fila.Cells(8).Value, "")
            art.cantidad = If(fila.Cells(9).Value <> 0, fila.Cells(9).Value, 0)
            art.noSerieProductos = If(fila.Cells(10).Value <> Nothing, fila.Cells(10).Value, "")
            art.porcIva = If(fila.Cells(11).Value <> 0, fila.Cells(11).Value, 0)
            art.montoIva = If(fila.Cells(12).Value <> 0, fila.Cells(12).Value, 0)
            art.porcIeps1 = If(fila.Cells(13).Value <> 0, fila.Cells(13).Value, 0)
            art.montoIeps1 = If(fila.Cells(14).Value <> 0, fila.Cells(14).Value, 0)
            art.porcIeps2 = If(fila.Cells(15).Value <> 0, fila.Cells(15).Value, 0)
            art.montoIeps2 = If(fila.Cells(16).Value <> 0, fila.Cells(16).Value, 0)
            art.porcIeps3 = If(fila.Cells(17).Value <> 0, fila.Cells(17).Value, 0)
            art.montoIeps3 = If(fila.Cells(18).Value <> 0, fila.Cells(18).Value, 0)
            art.ImporteNeto = If(fila.Cells(19).Value <> 0, fila.Cells(19).Value, 0)
            articulos.Add(art)
        Next

        Return articulos
    End Function

    ''' <summary>
    ''' Método que tiene por función guardar o actualizar un objeto tipo AddendaOxxo
    ''' </summary>
    ''' <returns>Una respuesta tipo Boolean, falsa o verdadera si pudo o no guardar la Addenda</returns>
    ''' <remarks></remarks>
    Private Function guardarActualizarAddenda(ByVal addendaOxxo As AddendaOxxo) As Boolean
        Dim estado As Boolean = False

        If gIdAddenda = 0 Then ' Se registra nueva addenda con articulos
            If (addendaService.guardarAddenda(addendaOxxo, IdVenta)) Then
                xmlAddendaOxxo = addendaOxxo
                estado = True
            End If
        End If

        Return estado
    End Function


    Private Function crearXML2(ByVal addenda As AddendaOxxo) As String
        Dim doc As New XmlDocument()

        Dim dir As String = Application.StartupPath & "\Addenda.xml"
        Dim mXml As New XmlTextWriter(dir, System.Text.Encoding.UTF8)

        With mXml
            'Definimos el formato del documento XML a crear
            .Formatting = Formatting.Indented

            'Iniciamos el documento XML
            .WriteStartDocument(True)

            'Creamos el primer elemento raíz
            .WriteStartElement("Addenda")

            'Creamos los hijos del primer elemento de raiz "Addenda"
            .WriteStartElement("AddendaOxxo")

            If Not addenda.noVersAdd = 0 Then
                .WriteStartElement("noVersAdd")
                .WriteString(addenda.noVersAdd)
                .WriteEndElement()
            End If

            .WriteStartElement("claseDoc")
            .WriteString(addenda.claseDoc)
            .WriteEndElement()

            If Not addenda.plaza = "" Then
                .WriteStartElement("plaza")
                .WriteString(addenda.plaza)
                .WriteEndElement()
            End If

            .WriteStartElement("tipoProv")
            .WriteString(addenda.tipoProv)
            .WriteEndElement()

            If Not addenda.locType = "" Then
                .WriteStartElement("locType")
                .WriteString(addenda.locType)
                .WriteEndElement()
            End If

            If Not addenda.folioPago = "" Then
                .WriteStartElement("folioPago")
                .WriteString(addenda.folioPago)
                .WriteEndElement()
            End If

            If Not addenda.ordCompra = 0 Then
                .WriteStartElement("ordCompra")
                .WriteString(addenda.ordCompra)
                .WriteEndElement()
            End If

            If Not addenda.glnEmisor = 0 Then
                .WriteStartElement("glnEmisor")
                .WriteString(addenda.glnEmisor)
                .WriteEndElement()
            End If

            If Not addenda.glnReceptor = 0 Then
                .WriteStartElement("glnReceptor")
                .WriteString(addenda.glnReceptor)
                .WriteEndElement()
            End If

            .WriteStartElement("moneda")
            .WriteString(addenda.moneda)
            .WriteEndElement()

            If Not addenda.tipoCambio = 0 Then
                .WriteStartElement("tipoCambio")
                .WriteString(addenda.tipoCambio)
                .WriteEndElement()
            End If

            If Not addenda.cfdReferenciaSerie = "" Then
                .WriteStartElement("cfdReferenciaSerie")
                .WriteString(addenda.cfdReferenciaSerie)
                .WriteEndElement()
            End If

            If Not addenda.cfdReferenciaFolio = 0 Then
                .WriteStartElement("cfdReferenciaFolio")
                .WriteString(addenda.cfdReferenciaFolio)
                .WriteEndElement()
            End If

            If Not addenda.montoDescuento0 = 0 Then
                .WriteStartElement("montoDescuento0")
                .WriteString(addenda.montoDescuento0)
                .WriteEndElement()
            End If

            If Not addenda.montoDescuento1 = 0 Then
                .WriteStartElement("montoDescuento1")
                .WriteString(addenda.montoDescuento1)
                .WriteEndElement()
            End If

            If Not addenda.montoDescuento2 = 0 Then
                .WriteStartElement("montoDescuento2")
                .WriteString(addenda.montoDescuento2)
                .WriteEndElement()
            End If

            If Not addenda.montoDescuento3 = 0 Then
                .WriteStartElement("montoDescuento3")
                .WriteString(addenda.montoDescuento3)
                .WriteEndElement()
            End If

            If Not addenda.tipoDescuento0 = "" Then
                .WriteStartElement("tipoDescuento0")
                .WriteString(addenda.tipoDescuento0)
                .WriteEndElement()
            End If

            If Not addenda.tipoDescuento1 = "" Then
                .WriteStartElement("tipoDescuento1")
                .WriteString(addenda.tipoDescuento1)
                .WriteEndElement()
            End If

            If Not addenda.tipoDescuento2 = "" Then
                .WriteStartElement("tipoDescuento2")
                .WriteString(addenda.tipoDescuento2)
                .WriteEndElement()
            End If

            If Not addenda.tipoDescuento3 = "" Then
                .WriteStartElement("tipoDescuento3")
                .WriteString(addenda.tipoDescuento3)
                .WriteEndElement()
            End If

            .WriteStartElement("importeTotal")
            .WriteString(addenda.importeTotal)
            .WriteEndElement()

            .WriteStartElement("tipoValidacion")
            .WriteString(addenda.tipoValidacion)
            .WriteEndElement()

            .WriteStartElement("fuenteNota")
            .WriteString(addenda.fuenteNota)
            .WriteEndElement()

            If addenda.articulos.Count > 0 Then
                'Creamos los hijos del segundo elemento de raiz "Addenda", el cual es "AddendaOXXO / Articulos"
                .WriteStartElement("Articulos")

                For Each art As Articulo In addenda.articulos
                    .WriteStartElement("Detalle")

                    If Not art.pedidoAdicional = 0 Then
                        .WriteStartElement("pedidoAdicional")
                        .WriteString(art.pedidoAdicional)
                        .WriteEndElement()
                    End If

                    If Not art.remision = 0 Then
                        .WriteStartElement("remision")
                        .WriteString(art.remision)
                        .WriteEndElement()
                    End If

                    If Not art.fechaEntrega = "" Then
                        .WriteStartElement("fechaEntrega")
                        .WriteString(art.fechaEntrega)
                        .WriteEndElement()
                    End If

                    If Not art.crTienda = "" Then
                        .WriteStartElement("crTienda")
                        .WriteString(art.crTienda)
                        .WriteEndElement()
                    End If

                    If Not art.nombreTienda = "" Then
                        .WriteStartElement("nombreTienda")
                        .WriteString(art.nombreTienda)
                        .WriteEndElement()
                    End If

                    If Not art.numProducto = 0 Then
                        .WriteStartElement("numProducto")
                        .WriteString(art.numProducto)
                        .WriteEndElement()
                    End If

                    If Not art.descripcion = "" Then
                        .WriteStartElement("descripcion")
                        .WriteString(art.descripcion)
                        .WriteEndElement()
                    End If

                    If Not art.unidadMedida = "" Then
                        .WriteStartElement("unidadMedida")
                        .WriteString(art.unidadMedida)
                        .WriteEndElement()
                    End If

                    If Not art.cantidad = 0 Then
                        .WriteStartElement("cantidad")
                        .WriteString(art.cantidad)
                        .WriteEndElement()
                    End If

                    If Not art.noSerieProductos = "" Then
                        .WriteStartElement("noSerieProductos")
                        .WriteString(art.noSerieProductos)
                        .WriteEndElement()
                    End If

                    If Not art.porcIva = 0 Then
                        .WriteStartElement("porcIva")
                        .WriteString(art.porcIva)
                        .WriteEndElement()
                    End If

                    If Not art.montoIva = 0 Then
                        .WriteStartElement("montoIva")
                        .WriteString(art.montoIva)
                        .WriteEndElement()
                    End If

                    If Not art.porcIeps1 = 0 Then
                        .WriteStartElement("porcIeps1")
                        .WriteString(art.porcIeps1)
                        .WriteEndElement()
                    End If

                    If Not art.montoIeps1 = 0 Then
                        .WriteStartElement("montoIeps1")
                        .WriteString(art.montoIeps1)
                        .WriteEndElement()
                    End If

                    If Not art.porcIeps2 = 0 Then
                        .WriteStartElement("porcIeps2")
                        .WriteString(art.porcIeps2)
                        .WriteEndElement()
                    End If

                    If Not art.montoIeps2 = 0 Then
                        .WriteStartElement("montoIeps2")
                        .WriteString(art.montoIeps2)
                        .WriteEndElement()
                    End If

                    If Not art.porcIeps3 = 0 Then
                        .WriteStartElement("porcIeps3")
                        .WriteString(art.porcIeps3)
                        .WriteEndElement()
                    End If

                    If Not art.montoIeps3 = 0 Then
                        .WriteStartElement("montoIeps3")
                        .WriteString(art.montoIeps3)
                        .WriteEndElement()
                    End If

                    If Not art.ImporteNeto = 0 Then
                        .WriteStartElement("ImporteNeto")
                        .WriteString(art.ImporteNeto)
                        .WriteEndElement()
                    End If

                    .WriteEndElement()
                Next

                'Cerramos 
                .Close()

            End If

            'Cerramos             
            .Close()


            doc.PreserveWhitespace = True
            doc.Load(dir)
            Return doc.InnerXml
        End With
    End Function

    ''' <summary>
    ''' Método que tiene por función crear una cadena con estructura XML de Oxxo
    ''' </summary>
    ''' <param name="addenda">Un objeto de tipo AddendaOxxo</param>
    ''' <returns>Un cadena con la estructura XML creada a partir del objeto tipo AddendaOxxo que recibe</returns>
    ''' <remarks></remarks>
    Private Function crearXml(ByVal addenda As AddendaOxxo, ByVal pTipoCFD As Byte) As String
        Dim xmlDoc As String
        If pTipoCFD = 1 Then
            xmlDoc = "<Addenda>" + vbCrLf
        Else
            xmlDoc = "<cfdi:Addenda>"
        End If
        'xmlDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
        'REQUERIDOS
        xmlDoc += "<AddendaOXXO importeTotal =""" + addenda.importeTotal.ToString() + """"
        xmlDoc += " claseDoc =""" + addenda.claseDoc.ToString() + """"
        xmlDoc += " plaza =""" + addenda.plaza.ToString() + """"
        xmlDoc += " tipoProv =""" + "0" + addenda.tipoProv.ToString() + """"
        xmlDoc += " noVersAdd =""" + addenda.noVersAdd.ToString() + """"
        xmlDoc += " moneda =""" + addenda.moneda.ToString() + """"

        'NO REQUERIDOS
        If Not addenda.locType = "" Then
            xmlDoc += " locType =""" + addenda.locType.ToString() + """"
        End If

        If Not addenda.folioPago = "" Then
            xmlDoc += " folioPago =""" + addenda.folioPago.ToString() + """"
        End If


        If Not addenda.ordCompra = 0 Then
            xmlDoc += " ordCompra =""" + addenda.ordCompra.ToString() + """"
        End If

        If addenda.glnEmisor <> "" Then
            xmlDoc += " glnEmisor =""" + addenda.glnEmisor.ToString() + """"
        End If

        If addenda.glnReceptor <> "" Then
            xmlDoc += " glnReceptor =""" + addenda.glnReceptor.ToString() + """"
        End If

        If Not addenda.tipoCambio = 0 Then
            xmlDoc += " tipoCambio =""" + addenda.tipoCambio.ToString() + """"
        End If

        If Not addenda.cfdReferenciaSerie = "" Then
            xmlDoc += " cfdReferenciaSerie =""" + addenda.cfdReferenciaSerie.ToString() + """"
        End If

        If Not addenda.cfdReferenciaFolio = "" Then
            xmlDoc += " cfdReferenciaFolio =""" + addenda.cfdReferenciaFolio.ToString() + """"
        End If

        If Not addenda.montoDescuento0 = 0 Then
            xmlDoc += " montoDescuento0 =""" + addenda.montoDescuento0.ToString() + """"
        End If

        If Not addenda.tipoDescuento0 = "" Then
            xmlDoc += " tipoDescuento0 =""" + addenda.tipoDescuento0.ToString() + """"
        End If

        If Not addenda.montoDescuento1 = 0 Then
            xmlDoc += " montoDescuento1 =""" + addenda.montoDescuento1.ToString() + """"
        End If

        If Not addenda.tipoDescuento1 = "" Then
            xmlDoc += " tipoDescuento1 =""" + addenda.tipoDescuento1.ToString() + """"
        End If

        If Not addenda.montoDescuento2 = 0 Then
            xmlDoc += " montoDescuento2 =""" + addenda.montoDescuento2.ToString() + """"
        End If

        If Not addenda.tipoDescuento2 = "" Then
            xmlDoc += " tipoDescuento2 =""" + addenda.tipoDescuento2.ToString() + """"
        End If

        If Not addenda.montoDescuento3 = 0 Then
            xmlDoc += " montoDescuento3 =""" + addenda.montoDescuento3.ToString() + """"
        End If

        If Not addenda.tipoDescuento3 = "" Then
            xmlDoc += " tipoDescuento3 =""" + addenda.tipoDescuento3.ToString() + """"
        End If

        If Not addenda.tipoValidacion = 0 Then
            xmlDoc += " tipoValidacion =""" + addenda.tipoValidacion.ToString() + """"
        End If

        If Not addenda.fuenteNota = 0 Then
            xmlDoc += " fuenteNota =""" + addenda.fuenteNota.ToString() + """"
        End If

        xmlDoc += ">" + vbCrLf

        xmlDoc += "<Articulos>" + vbCrLf
        For Each articulo As Articulo In addenda.articulos

            xmlDoc += "<Detalle ImporteNeto =""" + articulo.ImporteNeto.ToString() + """"
            xmlDoc += " cantidad =""" + articulo.cantidad.ToString() + """"

            If Not articulo.pedidoAdicional = 0 Then
                xmlDoc += " pedidoAdicional =""" + articulo.pedidoAdicional.ToString() + """"
            End If

            If Not articulo.remision = 0 Then
                xmlDoc += " remision =""" + articulo.remision.ToString() + """"
            End If

            If Not articulo.fechaEntrega = "" Then
                xmlDoc += " fechaEntrega =""" + articulo.fechaEntrega + """"
            End If

            If Not articulo.crTienda = "" Then
                xmlDoc += " crTienda =""" + articulo.crTienda + """"
            End If

            If Not articulo.nombreTienda = "" Then
                xmlDoc += " nombreTienda =""" + articulo.nombreTienda + """"
            End If

            If Not articulo.numProducto = 0 Then
                xmlDoc += " numProducto =""" + articulo.numProducto.ToString() + """"
            End If

            If Not articulo.descripcion = "" Then
                xmlDoc += " descripcion =""" + Replace(Replace(Replace(Replace(Replace(articulo.descripcion, "&", "&amp;"), ">", "&gt"), "<", "&lt;"), """", "&quot;"), "'", "&apos;") + """"
            End If

            If Not articulo.unidadMedida = "" Then
                xmlDoc += " unidadMedida =""" + articulo.unidadMedida + """"
            End If

            If Not articulo.noSerieProductos = "" Then
                xmlDoc += " noSerieProductos =""" + articulo.noSerieProductos.ToString() + """"
            End If

            If Not articulo.porcIva = 0 Then
                xmlDoc += " porcIva =""" + articulo.porcIva.ToString() + """"
            End If

            If Not articulo.montoIva = 0 Then
                xmlDoc += " montoIva =""" + articulo.montoIva.ToString() + """"
            End If

            If Not articulo.porcIeps1 = 0 Then
                xmlDoc += " porcIeps1 =""" + articulo.porcIeps1.ToString() + """"
            End If

            If Not articulo.montoIeps1 = 0 Then
                xmlDoc += " montoIeps1 =""" + articulo.montoIeps1.ToString() + """"
            End If

            If Not articulo.porcIeps2 = 0 Then
                xmlDoc += " porcIeps2 =""" + articulo.porcIeps2.ToString() + """"
            End If

            If Not articulo.montoIeps2 = 0 Then
                xmlDoc += " montoIeps2 =""" + articulo.montoIeps2.ToString() + """"
            End If

            If Not articulo.porcIeps3 = 0 Then
                xmlDoc += " porcIeps3 =""" + articulo.porcIeps3.ToString() + """"
            End If

            If Not articulo.montoIeps3 = 0 Then
                xmlDoc += " montoIeps3 =""" + articulo.montoIeps3.ToString() + """"
            End If
            xmlDoc += ">"
            xmlDoc += "</Detalle>" + vbCrLf
        Next
        xmlDoc += "</Articulos>" + vbCrLf
        xmlDoc += "</AddendaOXXO>" + vbCrLf

        If pTipoCFD = 1 Then
            xmlDoc += "</Addenda>"
        Else
            xmlDoc += "</cfdi:Addenda>"
        End If
        Return xmlDoc
    End Function

    ''' <summary>
    ''' Método que tiene por función validar los campos de la vista del detalle
    ''' </summary>
    ''' <returns>Una respuesta tipo Boolean, falsa o verdadera si paso o no la validación</returns>
    ''' <remarks></remarks>
    Private Function validarCamposDetalle() As Boolean
        Dim estado As Boolean = True

        If txtCantidadMercanciaEntregada.Enabled Then
            If txtCantidadMercanciaEntregada.Text = "" Then
                errorGeneral.SetError(txtCantidadMercanciaEntregada, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(txtCantidadMercanciaEntregada, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(txtCantidadMercanciaEntregada, LIMPIAR)
        End If

        If txtNumeroPedidoAdicional.Enabled Then
            If txtNumeroPedidoAdicional.Text = "" Then
                errorGeneral.SetError(txtNumeroPedidoAdicional, CAMPO_VACIO)
                estado = False
            Else
                If txtNumeroPedidoAdicional.TextLength < 10 Then
                    'errorGeneral.SetError(txtNumeroPedidoAdicional, LENGTH + "10")
                    txtNumeroPedidoAdicional.Text = txtNumeroPedidoAdicional.Text.PadLeft(10, "0")
                    'estado = False
                Else
                    errorGeneral.SetError(txtNumeroPedidoAdicional, LIMPIAR)
                End If
            End If
        Else
            errorGeneral.SetError(txtNumeroPedidoAdicional, LIMPIAR)
        End If

        If txtNumeroRemision.Enabled Then
            If txtNumeroRemision.Text = "" Then
                errorGeneral.SetError(txtNumeroRemision, CAMPO_VACIO)
                estado = False
            Else
                If txtNumeroRemision.TextLength < 6 Then
                    'errorGeneral.SetError(txtNumeroRemision, LENGTH + "6")
                    'estado = False
                    txtNumeroRemision.Text = txtNumeroRemision.Text.PadLeft(6, "0")
                Else
                    errorGeneral.SetError(txtNumeroRemision, LIMPIAR)
                End If
            End If
        Else
            errorGeneral.SetError(txtNumeroRemision, LIMPIAR)
        End If

        If txtNumeroSerie.Enabled Then
            If txtNumeroSerie.Text = "" Then
                errorGeneral.SetError(txtNumeroSerie, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(txtNumeroSerie, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(txtNumeroSerie, LIMPIAR)
        End If

        If txtCrDeTienda.Enabled Then
            If txtCrDeTienda.Text = "" Then
                errorGeneral.SetError(txtCrDeTienda, CAMPO_VACIO)
                estado = False
            Else
                Dim regExpCrTienda As New Regex("[0-9]{2}[a-zA-Z]{3}")
                Dim matchCrTienda As Match = regExpCrTienda.Match(txtCrDeTienda.Text)
                If Not matchCrTienda.Success Then
                    errorGeneral.SetError(txtCrDeTienda, FORMATO_99AAA)
                    estado = False
                Else
                    errorGeneral.SetError(txtCrDeTienda, LIMPIAR)
                End If
            End If
        Else
            errorGeneral.SetError(txtCrDeTienda, LIMPIAR)
        End If

        If txtLugarDeEntrega.Enabled Then
            If txtLugarDeEntrega.Text = "" Then
                errorGeneral.SetError(txtLugarDeEntrega, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(txtLugarDeEntrega, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(txtLugarDeEntrega, LIMPIAR)
        End If

        If txtIdentificadorProductoSku.Enabled Then
            If txtIdentificadorProductoSku.Text = "" Then
                errorGeneral.SetError(txtIdentificadorProductoSku, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(txtIdentificadorProductoSku, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(txtIdentificadorProductoSku, LIMPIAR)
        End If

        If txtDescripcionArticulo.Enabled Then
            If txtDescripcionArticulo.Text = "" Then
                errorGeneral.SetError(txtDescripcionArticulo, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(txtDescripcionArticulo, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(txtDescripcionArticulo, LIMPIAR)
        End If

        If txtUnidadMedida.Enabled Then
            If txtUnidadMedida.Text = "" Then
                errorGeneral.SetError(txtUnidadMedida, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(txtUnidadMedida, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(txtUnidadMedida, LIMPIAR)
        End If

        If dtpFechaEntregaMercancia.Enabled Then
            If dtpFechaEntregaMercancia.Text = "" Then
                errorGeneral.SetError(dtpFechaEntregaMercancia, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(dtpFechaEntregaMercancia, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(dtpFechaEntregaMercancia, LIMPIAR)
        End If

        If txtMontoImpuestoIva.Enabled Then
            If txtMontoImpuestoIva.Text = "" Then
                errorGeneral.SetError(txtMontoImpuestoIva, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(txtMontoImpuestoIva, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(txtMontoImpuestoIva, LIMPIAR)
        End If

        If txtMontoImpuestoIeps1.Enabled Then
            If txtMontoImpuestoIeps1.Text = "" Then
                errorGeneral.SetError(txtMontoImpuestoIeps1, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(txtMontoImpuestoIeps1, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(txtMontoImpuestoIeps1, LIMPIAR)
        End If

        If txtMontoImpuestoIeps2.Enabled Then
            If txtMontoImpuestoIeps2.Text = "" Then
                errorGeneral.SetError(txtMontoImpuestoIeps2, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(txtMontoImpuestoIeps2, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(txtMontoImpuestoIeps2, LIMPIAR)
        End If

        If txtMontoImpuestoIeps3.Enabled Then
            If txtMontoImpuestoIeps3.Text = "" Then
                errorGeneral.SetError(txtMontoImpuestoIeps3, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(txtMontoImpuestoIeps3, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(txtMontoImpuestoIeps3, LIMPIAR)
        End If

        If txtImporteNetoConImpuestosIncluidos.Enabled Then
            If txtImporteNetoConImpuestosIncluidos.Text = "" Then
                errorGeneral.SetError(txtImporteNetoConImpuestosIncluidos, CAMPO_VACIO)
                estado = False
            Else
                errorGeneral.SetError(txtImporteNetoConImpuestosIncluidos, LIMPIAR)
            End If
        Else
            errorGeneral.SetError(txtImporteNetoConImpuestosIncluidos, LIMPIAR)
        End If

        Return estado
    End Function

    ''' <summary>
    ''' Método que tiene por función validar los campos de la vista encabezado
    ''' </summary>
    ''' <returns>Una respuesta tipo Boolean, falsa o verdadera si paso o no la validación</returns>
    ''' <remarks></remarks>
    Private Function validarCamposEncabezado() As Boolean
        Dim estado As Boolean = True

        'Validamos campos requeridos
        If txtNumeroDeVersion.Text = "" Then
            errorGeneral.SetError(txtNumeroDeVersion, CAMPO_VACIO)
            estado = False
        Else
            errorGeneral.SetError(txtNumeroDeVersion, "")
        End If

        'txtIdentificadorPlazaOxxo
        If txtIdentificadorPlazaOxxo.Text = "" Then
            estado = False
            errorGeneral.SetError(txtIdentificadorPlazaOxxo, CAMPO_VACIO)
        Else
            errorGeneral.SetError(txtIdentificadorPlazaOxxo, "")
            Dim regExp As New Regex("[0-9]{2}[a-zA-Z]{3}")
            Dim match As Match = regExp.Match(txtIdentificadorPlazaOxxo.Text)

            If Not match.Success Then
                errorGeneral.SetError(txtIdentificadorPlazaOxxo, FORMATO_99AAA)
                estado = False
            Else
                errorGeneral.SetError(txtIdentificadorPlazaOxxo, "")
            End If
        End If

        'cboClaseDocumento
        If cboClaseDocumento.SelectedIndex = -1 Then
            estado = False
            errorGeneral.SetError(cboClaseDocumento, "Debe seleccionar una Clase documento")
        Else
            errorGeneral.SetError(cboClaseDocumento, "")
        End If

        If cboTipoLocalizacion.SelectedIndex = -1 Then
            estado = False
            errorGeneral.SetError(cboTipoLocalizacion, "Debe seleccionar un Tipo localización")
        Else
            errorGeneral.SetError(cboTipoLocalizacion, "")
        End If


        'cboTipoProveedor
        If cboTipoProveedor.SelectedIndex = -1 Then
            estado = False
            errorGeneral.SetError(cboTipoProveedor, "Debe seleccionar un Tipo proveedor")
        Else
            errorGeneral.SetError(cboTipoProveedor, "")
        End If

        'cboTipoMoneda
        If cboTipoMoneda.SelectedIndex = -1 Then
            estado = False
            errorGeneral.SetError(cboTipoMoneda, "Debe seleccionar un Tipo moneda")
        Else
            errorGeneral.SetError(cboTipoMoneda, "")
        End If

        'txtImporteTotalPagar
        If txtImporteTotalPagar.Text = "" Then
            estado = False
            errorGeneral.SetError(txtImporteTotalPagar, CAMPO_VACIO)
        Else
            errorGeneral.SetError(txtImporteTotalPagar, "")
        End If

        'txtTipoCambio
        If txtTipoCambio.Enabled Then
            If txtTipoCambio.Text = "" Then
                estado = False
                errorGeneral.SetError(txtTipoCambio, CAMPO_VACIO)
            End If
        Else
            errorGeneral.SetError(txtTipoCambio, "")
        End If

        'txtSerie
        If txtSerie.Enabled Then
            If txtSerie.Text = "" Then
                estado = False
                errorGeneral.SetError(txtSerie, CAMPO_VACIO)
            End If
        Else
            errorGeneral.SetError(txtSerie, "")
        End If

        'txtFolio
        If txtFolio.Enabled Then
            If txtFolio.Text = "" Then
                estado = False
                errorGeneral.SetError(txtFolio, CAMPO_VACIO)
            End If
        Else
            errorGeneral.SetError(txtFolio, "")
        End If

        'txtOrdenCompraOxxo
        If txtOrdenCompraOxxo.Enabled Then
            If txtOrdenCompraOxxo.Text = "" Then
                estado = False
                errorGeneral.SetError(txtOrdenCompraOxxo, CAMPO_VACIO)
            Else
                If txtOrdenCompraOxxo.TextLength < 9 Then
                    estado = False
                    errorGeneral.SetError(txtOrdenCompraOxxo, LENGTH + "9")
                Else
                    errorGeneral.SetError(txtOrdenCompraOxxo, "")
                End If
            End If
        Else
            errorGeneral.SetError(txtOrdenCompraOxxo, "")
        End If

        'txtGlnEmisorCfd
        If txtGlnEmisorCfd.Enabled Then
            If txtGlnEmisorCfd.Text = "" Then
                estado = False
                errorGeneral.SetError(txtGlnEmisorCfd, CAMPO_VACIO)
            Else
                If txtGlnEmisorCfd.TextLength < 12 Then
                    estado = False
                    errorGeneral.SetError(txtGlnEmisorCfd, LENGTH + "13")
                Else
                    errorGeneral.SetError(txtGlnEmisorCfd, "")
                End If
            End If
        Else
            errorGeneral.SetError(txtGlnEmisorCfd, "")
        End If

        'txtGlnReceptorCfed
        If txtGlnReceptorCfd.Enabled Then
            If txtGlnReceptorCfd.Text = "" Then
                estado = False
                errorGeneral.SetError(txtGlnReceptorCfd, CAMPO_VACIO)
            Else
                If txtGlnReceptorCfd.TextLength < 12 Then
                    estado = False
                    errorGeneral.SetError(txtGlnReceptorCfd, LENGTH + "13")
                Else
                    errorGeneral.SetError(txtGlnReceptorCfd, "")
                End If
            End If
        Else
            errorGeneral.SetError(txtGlnReceptorCfd, "")
        End If

        'txtNumeroFolioPago
        If txtNumeroFolioPago.Enabled Then
            If txtNumeroFolioPago.Text = "" Then
                estado = False
                errorGeneral.SetError(txtNumeroFolioPago, CAMPO_VACIO)
            Else
                If txtNumeroFolioPago.Enabled Then
                    If txtNumeroFolioPago.Text.Length < 25 Then
                        'errorGeneral.SetError(txtNumeroFolioPago, "El número de folio de pago debe contener mínimo 25 caracteres")
                        txtNumeroFolioPago.Text = txtNumeroFolioPago.Text.PadLeft(25, "0")
                        'estado = False
                    Else
                        errorGeneral.SetError(txtNumeroFolioPago, "")
                    End If
                Else
                    errorGeneral.SetError(txtNumeroFolioPago, "")
                End If
            End If
        Else
            errorGeneral.SetError(txtNumeroFolioPago, "")
        End If

        'txtTipoCambio
        If txtTipoCambio.Enabled Then
            If txtTipoCambio.Text = "" Then
                estado = False
                errorGeneral.SetError(txtTipoCambio, CAMPO_VACIO)
            End If
        Else
            errorGeneral.SetError(txtTipoCambio, "")
        End If

        'cboTipoDescuento
        If cboTipoDescuento.Enabled Then
            If cboTipoDescuento.SelectedIndex = -1 Then
                estado = False
                errorGeneral.SetError(cboTipoDescuento, TIPO_DESCUENTO)
            End If
        Else
            errorGeneral.SetError(cboTipoDescuento, "")
        End If

        'cboTipoDescuento1
        If cboTipoDescuento1.Enabled Then
            If cboTipoDescuento1.SelectedIndex = -1 Then
                estado = False
                errorGeneral.SetError(cboTipoDescuento1, TIPO_DESCUENTO)
            End If
        Else
            errorGeneral.SetError(cboTipoDescuento1, "")
        End If

        'cboTipoDescuento2
        If cboTipoDescuento2.Enabled Then
            If cboTipoDescuento2.SelectedIndex = -1 Then
                estado = False
                errorGeneral.SetError(cboTipoDescuento2, TIPO_DESCUENTO)
            End If
        Else
            errorGeneral.SetError(cboTipoDescuento2, "")
        End If

        'cboTipoDescuento3
        If cboTipoDescuento3.Enabled Then
            If cboTipoDescuento3.SelectedIndex = -1 Then
                estado = False
                errorGeneral.SetError(cboTipoDescuento3, TIPO_DESCUENTO)
            End If
        Else
            errorGeneral.SetError(cboTipoDescuento3, "")
        End If


        If Not estado Then
            Return estado
        End If

        Return (estado)

    End Function

    '' <summary>
    '' Método que tiene por función validar un XML generado comparandolo con el Schema de OXXO
    '' </summary>
    '' <param name="Xml">XML a comparar con el Schema de Oxxo</param>
    '' <returns>Una respuesta de tipo Boolean, falsa o verdadera si paso o no la validación</returns>
    '' <remarks></remarks>
    'Private Function validarXml(ByVal Xml As String) As Boolean
    '    Dim sr As New StringReader(Xml) 'Creamos un StringReader para poder leer la cadena string que recibe como parametro, y posteriormente establecerla como otro
    '    'parametro a la variable XmlTextReader


    '    Dim tr As New XmlTextReader(sr)
    '    Dim vr As New XmlValidatingReader(tr)
    '    vr.Schemas.Add("", "C:\Users\jcarcamo\Desktop\Proyectos\PULL_SYSTEM\xmlOxxo.xsd") 'Ruta donde se encuentra ubicado el Schema de Oxxo
    '    vr.ValidationType = ValidationType.Schema
    '    AddHandler vr.ValidationEventHandler, AddressOf xmlValidationEventHandler

    '    While vr.Read()
    '    End While


    '    vr.Close()

    '    If gValidation Then
    '        Return True 'Si nunca entro al xmlValidationEventHandlerr
    '    Else
    '        MsgBox(ERROR_VAL + gMessageValidation) 'Si entro al evento xmlValidationEventHandler
    '        gValidation = True
    '        gMessageValidation = ""
    '        Return False
    '    End If

    '    Return Nothing

    'End Function


    ''' <summary>
    ''' Método que tiene por función delegar el tipo de exception que genera la validación del método validarXml
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Shared Sub xmlValidationEventHandler(ByVal sender As Object, ByVal e As ValidationEventArgs)
        If e.Severity = XmlSeverityType.Warning Then
            gMessageValidation = e.Message 'Se establece la excepcion a una variable global de tipo string
            gValidation = False 'Se establece a False a una variable global de tipo Boolean
        ElseIf e.Severity = XmlSeverityType.Error Then
            gMessageValidation = e.Message 'Se establece la excepcion a una variable global de tipo string
            gValidation = False 'Se establece a False a una variable global de tipo Boolean
        End If
    End Sub

    ''' <summary>
    ''' Método que tiene por función crear las columnas correspondientes al objeto gridArticulos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub crearColumnasGrid()

        Dim colText As DataGridViewTextBoxColumn

        colText = New DataGridViewTextBoxColumn
        colText.Name = "id"
        colText.HeaderText = "Identificador"
        colText.Visible = False
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "pedidoAdicional"
        colText.HeaderText = "Pedido adicional"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "remision"
        colText.HeaderText = "Numero de la remisión"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "fechaEntrega"
        colText.HeaderText = "Fecha de entrega de la mercancia"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "crTienda"
        colText.HeaderText = "CR de Tienda"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "nombreTienda"
        colText.HeaderText = "Lugar de entrega"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "numProducto"
        colText.HeaderText = "Identificador del producto SKU"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "descripcion"
        colText.HeaderText = "Descripción del articulo"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "unidadMedida"
        colText.HeaderText = "Unidad de medida en caso de que sea distinta a la facturada"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "cantidad"
        colText.HeaderText = "Cantidad de mercancia entregada"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "noSerieProductos"
        colText.HeaderText = "Número de serie"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "porcIva"
        colText.HeaderText = "Porcentaje de IVA"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "montoIva"
        colText.HeaderText = "Monto del impuesto IVA"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "porcIeps1"
        colText.HeaderText = "Porcentaje de impuesto IEPS"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "montoIeps1"
        colText.HeaderText = "Monto del impuesto IEPS"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "porcIeps2"
        colText.HeaderText = "Porcentaje de impuesto IEPS"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "montoIeps2"
        colText.HeaderText = "Monto del impuesto IEPS"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "porcIeps3"
        colText.HeaderText = "Porcentaje de impuesto IEPS"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "montoIeps3"
        colText.HeaderText = "Monto del impuesto IEPS"
        gridArticulos.Columns.Add(colText)

        colText = New DataGridViewTextBoxColumn
        colText.Name = "ImporteNeto"
        colText.HeaderText = "Importe neto con impuestos incluidos"
        gridArticulos.Columns.Add(colText)


        'gridArticulos.Columns(1).DataPropertyName = "pedidoAdicional"
    End Sub


    Private Sub eliminarInfoColumnaGrid(ByVal nombreColumna As String)

        Select Case nombreColumna
            Case "pedidoAdicional"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(1).Value = Nothing
                Next
            Case "remision"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(2).Value = Nothing
                Next
            Case "fechaEntrega"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(3).Value = Nothing
                Next
            Case "crTienda"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(4).Value = Nothing
                Next
            Case "nombreTienda"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(5).Value = Nothing
                Next
            Case "numProducto"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(6).Value = Nothing
                Next
            Case "descripcion"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(7).Value = Nothing
                Next
            Case "unidadMedida"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(8).Value = Nothing
                Next
            Case "cantidad"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(9).Value = Nothing
                Next
            Case "noSerieProductos"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(10).Value = Nothing
                Next
            Case "porcIva"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(11).Value = Nothing
                Next
            Case "montoIva"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(12).Value = Nothing
                Next
            Case "porcIeps1"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(13).Value = Nothing
                Next
            Case "montoIeps1"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(14).Value = Nothing
                Next
            Case "porcIeps2"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(15).Value = Nothing
                Next
            Case "montoIeps2"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(16).Value = Nothing
                Next
            Case "porcIeps3"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(17).Value = Nothing
                Next
            Case "montoIeps3"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(18).Value = Nothing
                Next
            Case "ImporteNeto"
                For Each fila As DataGridViewRow In gridArticulos.Rows
                    fila.Cells(19).Value = Nothing
                Next
        End Select

    End Sub
    ''' <summary>
    ''' Método que tiene por función habilitar las columnas del objeto gridArticulos de acuerdo a las objetos habilitados de la vista de
    ''' el encabezado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub habilitarColumnasGrid()

        gridArticulos.Columns("descripcion").Visible = True
        gridArticulos.Columns("unidadMedida").Visible = True

        If txtNumeroPedidoAdicional.Enabled Then
            gridArticulos.Columns("pedidoAdicional").Visible = True
        Else
            gridArticulos.Columns("pedidoAdicional").Visible = False
            txtNumeroPedidoAdicional.Text = ""
            eliminarInfoColumnaGrid("pedidoAdicional")
        End If

        If txtNumeroRemision.Enabled Then
            gridArticulos.Columns("remision").Visible = True
        Else
            gridArticulos.Columns("remision").Visible = False
            txtNumeroRemision.Text = ""
            eliminarInfoColumnaGrid("remision")
        End If

        If dtpFechaEntregaMercancia.Enabled Then
            gridArticulos.Columns("fechaEntrega").Visible = True
        Else
            gridArticulos.Columns("fechaEntrega").Visible = False
            dtpFechaEntregaMercancia.Text = ""
            eliminarInfoColumnaGrid("fechaEntrega")
        End If

        If txtCrDeTienda.Enabled Then
            gridArticulos.Columns("crTienda").Visible = True
        Else
            gridArticulos.Columns("crTienda").Visible = False
            txtCrDeTienda.Text = ""
            eliminarInfoColumnaGrid("crTienda")
        End If

        If txtLugarDeEntrega.Enabled Then
            gridArticulos.Columns("nombreTienda").Visible = True
        Else
            gridArticulos.Columns("nombreTienda").Visible = False
            txtLugarDeEntrega.Text = ""
            eliminarInfoColumnaGrid("nombreTienda")
        End If

        If txtIdentificadorProductoSku.Enabled Then
            gridArticulos.Columns("numProducto").Visible = True
        Else
            gridArticulos.Columns("numProducto").Visible = False
            txtIdentificadorProductoSku.Text = ""
            eliminarInfoColumnaGrid("numProducto")
        End If

        If txtCantidadMercanciaEntregada.Enabled Then
            gridArticulos.Columns("cantidad").Visible = True
        Else
            gridArticulos.Columns("cantidad").Visible = False
            txtCantidadMercanciaEntregada.Text = ""
            eliminarInfoColumnaGrid("cantidad")
        End If

        If txtNumeroSerie.Enabled Then
            gridArticulos.Columns("noSerieProductos").Visible = True
        Else
            gridArticulos.Columns("noSerieProductos").Visible = False
            txtNumeroSerie.Text = ""
            eliminarInfoColumnaGrid("noSerieProductos")
        End If

        If txtPorcentajeIva.Enabled Then
            gridArticulos.Columns("porcIva").Visible = True
        Else
            gridArticulos.Columns("porcIva").Visible = False
            txtPorcentajeIva.Text = ""
            eliminarInfoColumnaGrid("porcIva")
        End If

        If txtMontoImpuestoIva.Enabled Then
            gridArticulos.Columns("montoIva").Visible = True
        Else
            gridArticulos.Columns("montoIva").Visible = False
            txtMontoImpuestoIva.Text = ""
            eliminarInfoColumnaGrid("montoIva")
        End If

        If txtPorcentajeImpuestoIeps1.Enabled Then
            gridArticulos.Columns("porcIeps1").Visible = True
        Else
            gridArticulos.Columns("porcIeps1").Visible = False
            txtPorcentajeImpuestoIeps1.Text = ""
            eliminarInfoColumnaGrid("porcIeps1")
        End If

        If txtMontoImpuestoIeps1.Enabled Then
            gridArticulos.Columns("montoIeps1").Visible = True
        Else
            gridArticulos.Columns("montoIeps1").Visible = False
            txtMontoImpuestoIeps1.Text = ""
            eliminarInfoColumnaGrid("montoIeps1")
        End If

        If txtPorcentajeImpuestoIeps2.Enabled Then
            gridArticulos.Columns("porcIeps2").Visible = True
        Else
            gridArticulos.Columns("porcIeps2").Visible = False
            txtPorcentajeImpuestoIeps2.Text = ""
            eliminarInfoColumnaGrid("porcIeps2")
        End If

        If txtMontoImpuestoIeps2.Enabled Then
            gridArticulos.Columns("montoIeps2").Visible = True
        Else
            gridArticulos.Columns("montoIeps2").Visible = False
            txtMontoImpuestoIeps2.Text = ""
            eliminarInfoColumnaGrid("montoIeps2")
        End If

        If txtPorcentajeImpuestoIeps3.Enabled Then
            gridArticulos.Columns("porcIeps3").Visible = True
        Else
            gridArticulos.Columns("porcIeps3").Visible = False
            txtPorcentajeImpuestoIeps3.Text = ""
            eliminarInfoColumnaGrid("porcIeps3")
        End If

        If txtMontoImpuestoIeps3.Enabled Then
            gridArticulos.Columns("montoIeps3").Visible = True
        Else
            gridArticulos.Columns("montoIeps3").Visible = False
            txtMontoImpuestoIeps3.Text = ""
            eliminarInfoColumnaGrid("montoIeps3")
        End If

        If txtImporteNetoConImpuestosIncluidos.Enabled Then
            gridArticulos.Columns("ImporteNeto").Visible = True
        Else
            gridArticulos.Columns("ImporteNeto").Visible = False
            txtImporteNetoConImpuestosIncluidos.Text = ""
            eliminarInfoColumnaGrid("ImporteNeto")
        End If


    End Sub

    ''' <summary>
    ''' Método que tiene por función obtener aquellos datos que vienen de la vista de la factura electrónica
    ''' </summary>
    ''' <param name="moneda"></param>
    ''' <param name="tipoCambio"></param>
    ''' <param name="listaArticulos"></param>
    ''' <remarks></remarks>
    Private Sub obtenerDatos(ByVal moneda As String, ByVal tipoCambio As Double, ByVal listaArticulos As Collection)

        If moneda <> "" Then
            Select Case moneda
                Case "MXN"
                    cboTipoMoneda.SelectedIndex = 0
                Case "USD"
                    cboTipoMoneda.SelectedIndex = 1
                    If tipoCambio <> 0 Then
                        txtTipoCambio.Text = tipoCambio
                        txtTipoCambio.Enabled = True
                    End If
            End Select
        End If

        For Each art As Articulo In listaArticulos
            gridArticulos.Rows.Add("", art.pedidoAdicional, art.remision, art.fechaEntrega, art.crTienda, art.nombreTienda, art.numProducto, art.descripcion, art.unidadMedida, art.cantidad, art.noSerieProductos, art.porcIva, art.montoIva, art.porcIeps1, art.montoIeps1, art.porcIeps2, art.montoIeps2, art.porcIeps3, art.montoIeps3, art.ImporteNeto)
        Next

    End Sub

    ''' <summary>
    ''' Método que tiene por función actualizar la información de un registro del objeto gridArticulos
    ''' </summary>
    ''' <param name="rowSelected">Fila seleccionada</param>
    ''' <remarks></remarks>
    Private Sub actualizarListaGrid(ByVal rowSelected As Integer)

        gridArticulos.Item(1, rowSelected).Value = If(txtNumeroPedidoAdicional.Text <> "", Int64.Parse(txtNumeroPedidoAdicional.Text), 0)
        gridArticulos.Item(2, rowSelected).Value = If(txtNumeroRemision.Text <> "", Int32.Parse(txtNumeroRemision.Text), 0)
        gridArticulos.Item(3, rowSelected).Value = If(dtpFechaEntregaMercancia.Text <> "", dtpFechaEntregaMercancia.Text, "")
        gridArticulos.Item(4, rowSelected).Value = If(txtCrDeTienda.Text <> "", txtCrDeTienda.Text, "")
        gridArticulos.Item(5, rowSelected).Value = If(txtLugarDeEntrega.Text <> "", txtLugarDeEntrega.Text, "")
        gridArticulos.Item(6, rowSelected).Value = If(txtIdentificadorProductoSku.Text <> "", Int64.Parse(txtIdentificadorProductoSku.Text), 0)
        gridArticulos.Item(7, rowSelected).Value = If(txtDescripcionArticulo.Text <> "", txtDescripcionArticulo.Text, "")
        gridArticulos.Item(8, rowSelected).Value = If(txtUnidadMedida.Text <> "", txtUnidadMedida.Text, "")
        gridArticulos.Item(9, rowSelected).Value = If(txtCantidadMercanciaEntregada.Text, Double.Parse(txtCantidadMercanciaEntregada.Text), 0)
        gridArticulos.Item(10, rowSelected).Value = If(txtNumeroSerie.Text <> "", txtNumeroSerie.Text, "")
        gridArticulos.Item(11, rowSelected).Value = If(txtPorcentajeIva.Text <> "", Double.Parse(txtPorcentajeIva.Text), 0)
        gridArticulos.Item(12, rowSelected).Value = If(txtMontoImpuestoIva.Text <> "", Double.Parse(txtMontoImpuestoIva.Text), 0)
        gridArticulos.Item(13, rowSelected).Value = If(txtPorcentajeImpuestoIeps1.Text <> "", Double.Parse(txtPorcentajeImpuestoIeps1.Text), 0)
        gridArticulos.Item(14, rowSelected).Value = If(txtMontoImpuestoIeps1.Text <> "", Double.Parse(txtMontoImpuestoIeps1.Text), 0)
        gridArticulos.Item(15, rowSelected).Value = If(txtPorcentajeImpuestoIeps2.Text <> "", Double.Parse(txtPorcentajeImpuestoIeps2.Text), 0)
        gridArticulos.Item(16, rowSelected).Value = If(txtMontoImpuestoIeps2.Text <> "", Double.Parse(txtMontoImpuestoIeps2.Text), 0)
        gridArticulos.Item(17, rowSelected).Value = If(txtPorcentajeImpuestoIeps3.Text <> "", Double.Parse(txtPorcentajeImpuestoIeps3.Text), 0)
        gridArticulos.Item(18, rowSelected).Value = If(txtMontoImpuestoIeps3.Text <> "", Double.Parse(txtMontoImpuestoIeps3.Text), 0)
        gridArticulos.Item(19, rowSelected).Value = If(txtImporteNetoConImpuestosIncluidos.Text <> "", Double.Parse(txtImporteNetoConImpuestosIncluidos.Text), 0)

    End Sub

#Region "keyPress, keyUp, selectIndexChanged, cellClick, click, load"

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            'guardarActualizarArticulo()
            If validarCamposDetalle() Then
                actualizarListaGrid(row)
                limpiarArticulo()
            End If
        Catch ex As Exception
            MsgBox(ERROR_APP)
        End Try
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try

            Dim addendaOxxo As New AddendaOxxo()
            Dim articulo As New Articulo()
            Dim articulos As New Collection

            'If validarCamposEncabezado() Then
            'Se crea el objet addenda 
            addendaOxxo = crearAddenda()

            'Se obtienen los articulos del grid
            articulos = crearArticulosGrid()

            'Se agrega el o los articulos al objeto addenda
            If articulos.Count > 0 Then
                addendaOxxo.articulos = articulos
            End If

            'Se crea el XML
            'Dim xml As String = crearXml(addendaOxxo)
            XMLResultado = crearXml(addendaOxxo, EsElectronica)
            guardarActualizarAddenda(addendaOxxo)
            AFuerzas = False
            Me.Close()
            'Se valida el XML generado
            'If validarXml(XMLResultado) Then
            'If (guardarActualizarAddenda(addendaOxxo)) Then 'Se guarda el objeto addendaOxxo
            'MsgBox(XMLResultado)
            'End If
            'End If
            'End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    
    Private Sub frmAddendaOxxo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If AFuerzas Then
            MsgBox("Solo puede cerrar esta ventana dando click en guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            e.Cancel = True
        End If
    End Sub

    Private Sub frmOxxo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        btnAgregar.Enabled = False

        crearColumnasGrid()

        'Inicializamos los items del combo Clase Documento
        cboClaseDocumento.Items.Add("Factura")
        cboClaseDocumento.Items.Add("Nota de Cargo")
        cboClaseDocumento.Items.Add("Nota de Crédito")

        'Inicializamos los items del combo de Tipo Proveedor        
        cboTipoProveedor.Items.Add("Directo")
        cboTipoProveedor.Items.Add("CEDIS")
        cboTipoProveedor.Items.Add("Activo Fijo")
        cboTipoProveedor.Items.Add("Servicios")
        cboTipoProveedor.Items.Add("Intercompañia")
        cboTipoProveedor.TabIndex = 1

        'Inicializamos los items del combo de Tipo Localización
        cboTipoLocalizacion.Items.Add("Tienda")
        cboTipoLocalizacion.Items.Add("CEDIS")
        cboTipoLocalizacion.Items.Add("Plaza")

        'Inicializamos los items del combo de Tipo Moneda
        cboTipoMoneda.Items.Add("MXN")
        cboTipoMoneda.Items.Add("USD")

        'Inicializamos los items del combo Tipo Descuento
        cboTipoDescuento.Items.Add("Distribución")
        cboTipoDescuento.Items.Add("Pronto Pago")

        'Inicializamos los items del combo Tipo Descuento1
        cboTipoDescuento1.Items.Add("Distribución")
        cboTipoDescuento1.Items.Add("Pronto Pago")

        'Inicializamos los items del combo Tipo Descuento2
        cboTipoDescuento2.Items.Add("Distribución")
        cboTipoDescuento2.Items.Add("Pronto Pago")


        'Inicializamos los items del combo Tipo Descuento3
        cboTipoDescuento3.Items.Add("Distribución")
        cboTipoDescuento3.Items.Add("Pronto Pago")

        txtMontoImpuestoIva.Enabled = False
        txtMontoImpuestoIeps1.Enabled = False
        txtMontoImpuestoIeps2.Enabled = False
        txtMontoImpuestoIeps3.Enabled = False

        cboTipoDescuento.Enabled = False
        cboTipoDescuento1.Enabled = False
        cboTipoDescuento2.Enabled = False
        cboTipoDescuento3.Enabled = False

        pnlClaseDocumento.Enabled = False
        pnlTipoLocalizacion.Enabled = False
        pnlTipoProveedor.Enabled = False
        pnlTipoMoneda.Enabled = False
        pnlNumeroPedidoAdicional.Enabled = False
        pnlNumeroRemision.Enabled = False
        pnlFechaEntregaMercancia.Enabled = False
        pnlCrDeTienda.Enabled = False
        pnlLugarDeEntrega.Enabled = False
        pnlIdentificadorProductoSku.Enabled = False
        pnlDescripcionArticulo.Enabled = False
        pnlUnidadMedida.Enabled = False
        pnlNumeroSerie.Enabled = False
        pnlNumeroFolioPago.Enabled = False

        btnGuardar.Enabled = False

        txtTipoValidacion.Text = 1
        txtFuenteNota.Text = 1

        dtpFechaEntregaMercancia.Format = DateTimePickerFormat.Custom
        dtpFechaEntregaMercancia.CustomFormat = "yyyy-MM-dd"



        ' ------------------------------------------ SOLO PARA PRUEBA ------------------------------------------

        'Dim articulo As New Articulo
        'articulo.cantidad = 18
        'articulo.porcIva = 16
        'articulo.ImporteNeto = 13.36
        'articulo.montoIva = 325
        'articulo.descripcion = "Sabrita Rancheritos"
        'articulo.unidadMedida = "kg"

        'Dim articulo2 As New Articulo
        'articulo2.cantidad = 28
        'articulo2.porcIva = 16
        'articulo2.ImporteNeto = 11.65
        'articulo2.montoIva = 654
        'articulo2.descripcion = "Celular Apple"
        'articulo2.unidadMedida = "pza"

        'Dim articulo3 As New Articulo
        'articulo3.cantidad = 38
        'articulo3.porcIva = 16
        'articulo3.ImporteNeto = 14.02
        'articulo3.montoIva = 987
        'articulo3.descripcion = "Camiseta"
        'articulo3.unidadMedida = "pza"


        'elementos.Add(articulo)
        'elementos.Add(articulo2)
        'elementos.Add(articulo3)

        ' ------------------------------------------------------------------------------------------------------
        Dim Ade As New AddendaData(MySqlcon)
        Dim adde As New AddendaOxxo
        adde = Ade.obtenerAddendaConArticulos(IdVenta)
        If adde.Id = 0 Then
            txtNumeroDeVersion.Text = "1"
            txtFolio.Text = Folio
            txtSerie.Text = Serie
            cboClaseDocumento.SelectedIndex = Doc
            txtImporteTotalPagar.Text = Total
            txtTipoValidacion.Text = "1"
            txtFuenteNota.Text = "1"
            obtenerDatos(Moneda, TipodeCambio, elementos)
        Else
            'Dim Ade As New AddendaData(MySqlcon)
            'Dim adde As New AddendaOxxo
            'adde = Ade.obtenerAddendaConArticulos(IdVenta)
            If adde.Id <> 0 Then
                'Llenar todo
                txtNumeroDeVersion.Text = "1"
                txtIdentificadorPlazaOxxo.Text = adde.plaza
                cboClaseDocumento.SelectedIndex = Doc
                txtSerie.Text = adde.cfdReferenciaSerie
                txtFolio.Text = adde.cfdReferenciaFolio
                Select Case adde.locType
                    Case "T"
                        cboTipoLocalizacion.SelectedIndex = 0
                    Case "C"
                        cboTipoLocalizacion.SelectedIndex = 1
                    Case "P"
                        cboTipoLocalizacion.SelectedIndex = 2
                End Select
                txtOrdenCompraOxxo.Text = adde.ordCompra.ToString
                txtMontoDescuento.Text = adde.montoDescuento0.ToString
                txtMontoDescuento1.Text = adde.montoDescuento1.ToString
                txtMontoDescuento2.Text = adde.montoDescuento2.ToString
                txtMontoDescuento3.Text = adde.montoDescuento3.ToString
                txtImporteTotalPagar.Text = adde.importeTotal.ToString
                txtTipoValidacion.Text = "1"
                txtFuenteNota.Text = "1"
                cboTipoProveedor.SelectedIndex = adde.tipoProv - 1
                txtGlnEmisorCfd.Text = adde.glnEmisor.ToString
                txtGlnReceptorCfd.Text = adde.glnReceptor.ToString
                txtNumeroFolioPago.Text = adde.folioPago.PadLeft(25, "0")
                cboTipoMoneda.Text = adde.moneda
                txtTipoCambio.Text = adde.tipoCambio.ToString
                Select Case adde.tipoDescuento0
                    Case "001"
                        cboTipoDescuento.SelectedIndex = 1
                    Case "002"
                        cboTipoDescuento.SelectedIndex = 2
                End Select
                Select Case adde.tipoDescuento1
                    Case "001"
                        cboTipoDescuento1.SelectedIndex = 1
                    Case "002"
                        cboTipoDescuento1.SelectedIndex = 2
                End Select
                Select Case adde.tipoDescuento2
                    Case "001"
                        cboTipoDescuento2.SelectedIndex = 1
                    Case "002"
                        cboTipoDescuento2.SelectedIndex = 2
                End Select
                Select Case adde.tipoDescuento3
                    Case "001"
                        cboTipoDescuento3.SelectedIndex = 1
                    Case "002"
                        cboTipoDescuento3.SelectedIndex = 2
                End Select

                obtenerDatos(Moneda, TipodeCambio, adde.articulos)
            End If
        End If
    End Sub
    Public Sub New(ByVal pIdVenta As Integer, ByVal pAfuerzas As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdVenta = pIdVenta
        AFuerzas = pAfuerzas
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub cboTipoProveedor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoProveedor.SelectedIndexChanged
        errorGeneral.SetError(cboTipoProveedor, "")

        If cboTipoLocalizacion.SelectedIndex = 0 And cboTipoProveedor.SelectedIndex = 0 Then
            pnlNumeroFolioPago.Enabled = True
        Else
            pnlNumeroFolioPago.Enabled = False
            txtNumeroFolioPago.Text = ""
            errorGeneral.SetError(txtNumeroFolioPago, "")
        End If

        If cboTipoProveedor.SelectedIndex = 0 Or cboTipoProveedor.SelectedIndex = 1 Then
            pnlTipoProveedor.Enabled = True
            pnlFechaEntregaMercancia.Enabled = True
            pnlLugarDeEntrega.Enabled = True
            pnlIdentificadorProductoSku.Enabled = True
            pnlDescripcionArticulo.Enabled = True
            pnlUnidadMedida.Enabled = True
        Else
            txtGlnEmisorCfd.Text = ""
            txtGlnReceptorCfd.Text = ""
            errorGeneral.SetError(txtGlnEmisorCfd, "")
            errorGeneral.SetError(txtGlnReceptorCfd, "")
            pnlTipoProveedor.Enabled = False

            pnlFechaEntregaMercancia.Enabled = False

            txtLugarDeEntrega.Text = ""
            errorGeneral.SetError(txtLugarDeEntrega, "")
            pnlLugarDeEntrega.Enabled = False

            txtIdentificadorProductoSku.Text = ""
            errorGeneral.SetError(txtIdentificadorProductoSku, "")
            pnlIdentificadorProductoSku.Enabled = False

            txtDescripcionArticulo.Text = ""
            errorGeneral.SetError(txtDescripcionArticulo, "")
            pnlDescripcionArticulo.Enabled = False

            txtUnidadMedida.Text = ""
            errorGeneral.SetError(txtUnidadMedida, "")
            pnlUnidadMedida.Enabled = False
        End If

        If cboTipoProveedor.SelectedIndex = 2 Then
            pnlNumeroSerie.Enabled = True
        Else
            pnlNumeroSerie.Enabled = False
        End If

    End Sub

    Private Sub cboClaseDocumento_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboClaseDocumento.SelectedIndexChanged
        errorGeneral.SetError(cboClaseDocumento, "")
        If cboClaseDocumento.SelectedIndex = 1 Or cboClaseDocumento.SelectedIndex Then
            pnlClaseDocumento.Enabled = True
        Else
            pnlClaseDocumento.Enabled = False
            txtSerie.Text = ""
            txtFolio.Text = ""
            errorGeneral.SetError(txtSerie, "")
            errorGeneral.SetError(txtFolio, "")
        End If
    End Sub

    Private Sub cboTipoLocalizacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoLocalizacion.SelectedIndexChanged

        errorGeneral.SetError(cboTipoLocalizacion, "")

        If cboTipoLocalizacion.SelectedIndex = 1 Then
            pnlTipoLocalizacion.Enabled = True
        Else
            pnlTipoLocalizacion.Enabled = False
            txtOrdenCompraOxxo.Text = ""
            errorGeneral.SetError(txtOrdenCompraOxxo, "")
        End If

        If cboTipoLocalizacion.SelectedIndex = 0 Then
            pnlNumeroPedidoAdicional.Enabled = True
            pnlNumeroRemision.Enabled = True
            pnlCrDeTienda.Enabled = True
        Else
            pnlNumeroPedidoAdicional.Enabled = False
            pnlNumeroRemision.Enabled = False
            pnlCrDeTienda.Enabled = False
            txtNumeroPedidoAdicional.Text = ""
            txtNumeroRemision.Text = ""
            txtCrDeTienda.Text = ""
            errorGeneral.SetError(txtNumeroPedidoAdicional, "")
            errorGeneral.SetError(txtNumeroRemision, "")
            errorGeneral.SetError(txtCrDeTienda, "")
        End If

        If cboTipoLocalizacion.SelectedIndex = 0 And cboTipoProveedor.SelectedIndex = 0 Then
            pnlNumeroFolioPago.Enabled = True
        Else
            pnlNumeroFolioPago.Enabled = False
            txtNumeroFolioPago.Text = ""
            errorGeneral.SetError(txtNumeroFolioPago, "")
        End If
    End Sub

    Private Sub cboTipoMoneda_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoMoneda.SelectedIndexChanged
        errorGeneral.SetError(cboTipoMoneda, "")
        If cboTipoMoneda.SelectedIndex <> 0 Then
            pnlTipoMoneda.Enabled = True
        Else
            pnlTipoMoneda.Enabled = False
            txtTipoCambio.Text = ""
            errorGeneral.SetError(txtTipoCambio, "")
        End If
    End Sub

    Private Sub txtNumeroDeVersion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumeroDeVersion.KeyPress
        'txtNumeroDeVersion.Focus()
        ''Clipboard.Clear()

        errorGeneral.SetError(txtNumeroDeVersion, "")

        'If e.KeyChar.IsDigit(e.KeyChar) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub

    Private Sub txtOrdenCompraOxxo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOrdenCompraOxxo.KeyPress
        'txtOrdenCompraOxxo.Focus()
        ''Clipboard.Clear()

        errorGeneral.SetError(txtOrdenCompraOxxo, "")
        'If (e.KeyChar.IsDigit(e.KeyChar)) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub

    Private Sub txtGlnEmisorCfd_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGlnEmisorCfd.KeyPress
        'txtGlnEmisorCfd.Focus()
        ''Clipboard.Clear()

        errorGeneral.SetError(txtGlnEmisorCfd, "")
        'If (e.KeyChar.IsDigit(e.KeyChar)) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub

    Private Sub txtGlnReceptorCfd_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGlnReceptorCfd.KeyPress
        'txtGlnReceptorCfd.Focus()
        ''Clipboard.Clear()

        errorGeneral.SetError(txtGlnReceptorCfd, "")
        'If (e.KeyChar.IsDigit(e.KeyChar)) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub

    Private Sub txtTipoCambio_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTipoCambio.KeyPress
        'txtTipoCambio.Focus()
        ''Clipboard.Clear()

        errorGeneral.SetError(txtTipoCambio, "")
        'Dim dig As Integer = Len(txtTipoCambio.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtTipoCambio.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtTipoCambio.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtTipoCambio.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 8 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If

    End Sub

    Private Sub txtFolio_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFolio.KeyPress
        'txtFolio.Focus()
        ''Clipboard.Clear()

        errorGeneral.SetError(txtFolio, "")
    End Sub

    Private Sub txtMontoDescuento_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoDescuento.KeyPress

        'txtMontoDescuento.Focus()
        ''Clipboard.Clear()

        'Dim dig As Integer = Len(txtMontoDescuento.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtMontoDescuento.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtMontoDescuento.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtMontoDescuento.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtMontoDescuento1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoDescuento1.KeyPress
        'txtMontoDescuento1.Focus()
        ''Clipboard.Clear()

        'Dim dig As Integer = Len(txtMontoDescuento1.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtMontoDescuento1.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtMontoDescuento1.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtMontoDescuento1.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtMontoDescuento2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoDescuento2.KeyPress
        'txtMontoDescuento2.Focus()
        ''Clipboard.Clear()

        'Dim dig As Integer = Len(txtMontoDescuento2.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtMontoDescuento2.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtMontoDescuento2.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtMontoDescuento2.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtMontoDescuento3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoDescuento3.KeyPress
        'txtMontoDescuento3.Focus()
        ''Clipboard.Clear()

        'Dim dig As Integer = Len(txtMontoDescuento3.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtMontoDescuento3.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtMontoDescuento3.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtMontoDescuento3.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtImporteTotalPagar_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporteTotalPagar.KeyPress
        'txtImporteTotalPagar.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtImporteTotalPagar, "")
        'Dim dig As Integer = Len(txtImporteTotalPagar.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtImporteTotalPagar.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtImporteTotalPagar.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtImporteTotalPagar.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtTipoValidacion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTipoValidacion.KeyPress
        'txtTipoValidacion.Focus()
        ''Clipboard.Clear()

        'If (e.KeyChar.IsDigit(e.KeyChar)) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub

    Private Sub txtFuenteNota_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFuenteNota.KeyPress

        'txtFuenteNota.Focus()
        ''Clipboard.Clear()

        'If (e.KeyChar.IsDigit(e.KeyChar)) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub

    Private Sub txtNumeroPedidoAdicional_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumeroPedidoAdicional.KeyPress
        'txtNumeroPedidoAdicional.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtNumeroPedidoAdicional, "")

        'If (e.KeyChar.IsDigit(e.KeyChar)) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub

    Private Sub txtNumeroRemision_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumeroRemision.KeyPress
        'txtNumeroRemision.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtNumeroRemision, "")


        'If (e.KeyChar.IsDigit(e.KeyChar)) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub

    Private Sub txtIdentificadorProductoSku_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIdentificadorProductoSku.KeyPress
        'txtIdentificadorProductoSku.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtIdentificadorProductoSku, "")

        'If (e.KeyChar.IsDigit(e.KeyChar)) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
    End Sub

    Private Sub txtCantidadMercanciaEntregada_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidadMercanciaEntregada.KeyPress
        'txtCantidadMercanciaEntregada.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtCantidadMercanciaEntregada, "")

        'Dim dig As Integer = Len(txtCantidadMercanciaEntregada.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtCantidadMercanciaEntregada.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtCantidadMercanciaEntregada.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtCantidadMercanciaEntregada.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtPorcentajeIva_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorcentajeIva.KeyPress
        'txtPorcentajeIva.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtPorcentajeIva, "")

        'Dim dig As Integer = Len(txtPorcentajeIva.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtPorcentajeIva.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtPorcentajeIva.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtPorcentajeIva.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtMontoImpuestoIva_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoImpuestoIva.KeyPress
        'txtMontoImpuestoIva.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtMontoImpuestoIva, "")

        'Dim dig As Integer = Len(txtMontoImpuestoIva.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtMontoImpuestoIva.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtMontoImpuestoIva.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtMontoImpuestoIva.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtPorcentajeImpuestoIeps1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorcentajeImpuestoIeps1.KeyPress
        'txtPorcentajeImpuestoIeps1.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtPorcentajeImpuestoIeps1, "")

        'Dim dig As Integer = Len(txtPorcentajeImpuestoIeps1.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtPorcentajeImpuestoIeps1.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtPorcentajeImpuestoIeps1.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtPorcentajeImpuestoIeps1.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtMontoImpuestoIeps1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoImpuestoIeps1.KeyPress
        'txtMontoImpuestoIeps1.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtMontoImpuestoIeps1, "")

        'Dim dig As Integer = Len(txtMontoImpuestoIeps1.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtMontoImpuestoIeps1.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtMontoImpuestoIeps1.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtMontoImpuestoIeps1.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtPorcentajeImpuestoIeps2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorcentajeImpuestoIeps2.KeyPress
        'txtPorcentajeImpuestoIeps2.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtPorcentajeImpuestoIeps2, "")

        'Dim dig As Integer = Len(txtPorcentajeImpuestoIeps2.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtPorcentajeImpuestoIeps2.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtPorcentajeImpuestoIeps2.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtPorcentajeImpuestoIeps2.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtMontoImpuestoIeps_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoImpuestoIeps2.KeyPress
        'txtMontoImpuestoIeps2.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtMontoImpuestoIeps2, "")

        'Dim dig As Integer = Len(txtMontoImpuestoIeps2.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtMontoImpuestoIeps2.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtMontoImpuestoIeps2.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtMontoImpuestoIeps2.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtPorcentajeImpuestoIeps3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorcentajeImpuestoIeps3.KeyPress
        'txtPorcentajeImpuestoIeps3.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtPorcentajeImpuestoIeps3, "")

        'Dim dig As Integer = Len(txtPorcentajeImpuestoIeps3.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtPorcentajeImpuestoIeps3.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtPorcentajeImpuestoIeps3.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtPorcentajeImpuestoIeps3.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtMontoImpuestoIeps3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoImpuestoIeps3.KeyPress
        'txtMontoImpuestoIeps3.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtMontoImpuestoIeps3, "")

        'Dim dig As Integer = Len(txtMontoImpuestoIeps3.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(8) Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtMontoImpuestoIeps3.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtMontoImpuestoIeps3.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtMontoImpuestoIeps3.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtImporteNetoConImpuestosIncluidos_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImporteNetoConImpuestosIncluidos.KeyPress
        'txtImporteNetoConImpuestosIncluidos.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtImporteNetoConImpuestosIncluidos, "")

        'Dim dig As Integer = Len(txtImporteNetoConImpuestosIncluidos.Text & e.KeyChar)
        'Dim a, esDecimal, NumDecimales As Integer
        'Dim esDec As Boolean
        '' se verifica si es un digito o un punto 
        'If e.KeyChar.IsDigit(e.KeyChar) Or e.KeyChar = "." Then
        '    e.Handled = False
        'ElseIf e.KeyChar.IsControl(e.KeyChar) Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        '' se verifica que el primer digito ingresado no sea un punto al seleccionar
        'If txtImporteNetoConImpuestosIncluidos.SelectedText <> "" Then
        '    If e.KeyChar = "." Then
        '        e.Handled = True
        '    End If
        'End If
        'If dig = 1 And e.KeyChar = "." Then
        '    e.Handled = True
        'End If
        ''aqui se hace la verificacion cuando es seleccionado el valor del texto
        ''y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        'If txtImporteNetoConImpuestosIncluidos.SelectedText = "" Then
        '    ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
        '    For a = 0 To dig - 1
        '        Dim car As String = CStr(txtImporteNetoConImpuestosIncluidos.Text & e.KeyChar)
        '        If car.Substring(a, 1) = "." Then
        '            esDecimal = esDecimal + 1
        '            esDec = True
        '        End If
        '        If esDec = True Then
        '            NumDecimales = NumDecimales + 1
        '        End If
        '        ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
        '        If NumDecimales >= 4 Or esDecimal >= 2 Then
        '            e.Handled = True
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub txtIdentificadorPlazaOxxo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIdentificadorPlazaOxxo.KeyPress
        'txtIdentificadorPlazaOxxo.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtIdentificadorPlazaOxxo, "")

        'If e.KeyChar.IsLetter(e.KeyChar) Then
        '    e.KeyChar = UCase(e.KeyChar)
        'End If


    End Sub

    Private Sub gridArticulos_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridArticulos.CellClick
        Try
            row = gridArticulos.CurrentRow.Index
            cargarDatosDetalle(row)
        Catch ex As Exception
            MsgBox(ERROR_APP)
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        limpiarArticulo()
        btnAgregar.Text = "Actualizar artículo"
        btnAgregar.Enabled = False
        gIdArticulo = 0
    End Sub

    Private Sub txtMontoDescuento_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMontoDescuento.KeyUp
        If txtMontoDescuento.Text <> "" Then
            cboTipoDescuento.Enabled = True
        Else
            cboTipoDescuento.SelectedIndex = -1
            cboTipoDescuento.Enabled = False
            errorGeneral.SetError(cboTipoDescuento, "")
        End If
    End Sub

    Private Sub txtMontoDescuento1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMontoDescuento1.KeyUp
        If txtMontoDescuento1.Text <> "" Then
            cboTipoDescuento1.Enabled = True
        Else
            cboTipoDescuento1.SelectedIndex = -1
            cboTipoDescuento1.Enabled = False
            errorGeneral.SetError(cboTipoDescuento1, "")
        End If
    End Sub

    Private Sub txtMontoDescuento2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMontoDescuento2.KeyUp
        If txtMontoDescuento2.Text <> "" Then
            cboTipoDescuento2.Enabled = True
        Else
            cboTipoDescuento2.SelectedIndex = -1
            cboTipoDescuento2.Enabled = False
            errorGeneral.SetError(cboTipoDescuento2, "")
        End If
    End Sub

    Private Sub txtMontoDescuento3_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMontoDescuento3.KeyUp
        If txtMontoDescuento3.Text <> "" Then
            cboTipoDescuento3.Enabled = True
        Else
            cboTipoDescuento3.SelectedIndex = -1
            cboTipoDescuento3.Enabled = False
            errorGeneral.SetError(cboTipoDescuento3, "")
        End If
    End Sub

    Private Sub txtPorcentajeIva_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPorcentajeIva.KeyUp
        If txtPorcentajeIva.Text <> "" Then
            txtMontoImpuestoIva.Enabled = True
        Else
            txtMontoImpuestoIva.Text = ""
            txtMontoImpuestoIva.Enabled = False
        End If

    End Sub

    Private Sub txtPorcentajeImpuestoIeps1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPorcentajeImpuestoIeps1.KeyUp
        If txtPorcentajeImpuestoIeps1.Text <> "" Then
            txtMontoImpuestoIeps1.Enabled = True
        Else
            txtMontoImpuestoIeps1.Text = ""
            txtMontoImpuestoIeps1.Enabled = False
        End If
    End Sub

    Private Sub txtPorcentajeImpuestoIeps2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPorcentajeImpuestoIeps2.KeyUp
        If txtPorcentajeImpuestoIeps2.Text <> "" Then
            txtMontoImpuestoIeps2.Enabled = True
        Else
            txtMontoImpuestoIeps2.Text = ""
            txtMontoImpuestoIeps2.Enabled = False
        End If
    End Sub

    Private Sub txtPorcentajeImpuestoIeps3_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPorcentajeImpuestoIeps3.KeyUp
        If txtPorcentajeImpuestoIeps3.Text <> "" Then
            txtMontoImpuestoIeps3.Enabled = True
        Else
            txtMontoImpuestoIeps3.Text = ""
            txtMontoImpuestoIeps3.Enabled = False
        End If
    End Sub

    Private Sub txtSerie_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSerie.KeyPress
        txtSerie.Focus()
        ''Clipboard.Clear()
        errorGeneral.SetError(txtSerie, "")
    End Sub

    Private Sub txtNumeroFolioPago_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumeroFolioPago.KeyPress
        txtNumeroFolioPago.Focus()
        ''Clipboard.Clear()

        errorGeneral.SetError(txtNumeroFolioPago, "")
    End Sub


    Private Sub txtMontoDescuento_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtMontoDescuento.MouseDown
        txtMontoDescuento.Focus()
        ''Clipboard.Clear()
    End Sub

    Private Sub txtMontoDescuento1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtMontoDescuento1.MouseDown
        txtMontoDescuento1.Focus()
        ''Clipboard.Clear()
    End Sub

    Private Sub txtMontoDescuento2_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtMontoDescuento2.MouseDown
        txtMontoDescuento2.Focus()
        ''Clipboard.Clear()
    End Sub

    Private Sub txtMontoDescuento3_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtMontoDescuento3.MouseDown
        txtMontoDescuento3.Focus()
        ''Clipboard.Clear()
    End Sub

    Private Sub cboTipoDescuento_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboTipoDescuento.KeyPress
        cboTipoDescuento.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub cboTipoDescuento1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboTipoDescuento1.KeyPress
        cboTipoDescuento1.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub cboTipoDescuento2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboTipoDescuento2.KeyPress
        cboTipoDescuento2.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub cboTipoDescuento3_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cboTipoDescuento3.MouseDown
        cboTipoDescuento3.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtImporteTotalPagar_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtImporteTotalPagar.MouseDown
        txtImporteTotalPagar.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtTipoValidacion_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtTipoValidacion.MouseDown
        txtTipoValidacion.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtFuenteNota_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtFuenteNota.MouseDown
        txtFuenteNota.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtNumeroDeVersion_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtNumeroDeVersion.MouseDown
        txtNumeroDeVersion.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtIdentificadorPlazaOxxo_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtIdentificadorPlazaOxxo.MouseDown
        txtIdentificadorPlazaOxxo.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub cboClaseDocumento_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboClaseDocumento.KeyPress
        cboClaseDocumento.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtSerie_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtSerie.MouseDown
        txtSerie.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtFolio_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtFolio.MouseDown
        txtFolio.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub cboTipoLocalizacion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboTipoLocalizacion.KeyPress
        cboTipoLocalizacion.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtOrdenCompraOxxo_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtOrdenCompraOxxo.MouseDown
        txtOrdenCompraOxxo.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub cboTipoProveedor_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboTipoProveedor.KeyPress
        cboTipoProveedor.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtGlnEmisorCfd_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtGlnEmisorCfd.MouseDown
        txtGlnEmisorCfd.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtGlnReceptorCfd_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtGlnReceptorCfd.MouseDown
        txtGlnReceptorCfd.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtNumeroFolioPago_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtNumeroFolioPago.MouseDown
        txtNumeroFolioPago.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub cboTipoMoneda_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboTipoMoneda.KeyPress
        cboTipoMoneda.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtTipoCambio_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtTipoCambio.MouseDown
        txtTipoCambio.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtNumeroSerie_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumeroSerie.KeyPress
        txtNumeroSerie.Focus()
        'Clipboard.Clear()

        errorGeneral.SetError(txtNumeroSerie, "")
    End Sub

    Private Sub txtCrDeTienda_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCrDeTienda.KeyPress
        'txtCrDeTienda.Focus()
        ''Clipboard.Clear()

        'errorGeneral.SetError(txtCrDeTienda, "")

        'If e.KeyChar.IsLetter(e.KeyChar) Then
        '    e.KeyChar = UCase(e.KeyChar)
        'End If


    End Sub

    Private Sub txtLugarDeEntrega_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLugarDeEntrega.KeyPress
        txtLugarDeEntrega.Focus()
        'Clipboard.Clear()

        errorGeneral.SetError(txtLugarDeEntrega, "")
    End Sub

    Private Sub txtDescripcionArticulo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescripcionArticulo.KeyPress
        txtDescripcionArticulo.Focus()
        'Clipboard.Clear()

        errorGeneral.SetError(txtDescripcionArticulo, "")
    End Sub

    Private Sub txtUnidadMedida_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUnidadMedida.KeyPress
        txtUnidadMedida.Focus()
        'Clipboard.Clear()

        errorGeneral.SetError(txtUnidadMedida, "")
    End Sub

    Private Sub dtpFechaEntregaMercancia_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpFechaEntregaMercancia.KeyPress
        dtpFechaEntregaMercancia.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtNumeroPedidoAdicional_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtNumeroPedidoAdicional.MouseDown
        txtNumeroPedidoAdicional.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtNumeroRemision_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtNumeroRemision.MouseDown
        txtNumeroRemision.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtNumeroSerie_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtNumeroSerie.MouseDown
        txtNumeroSerie.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtCrDeTienda_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtCrDeTienda.MouseDown
        txtCrDeTienda.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtLugarDeEntrega_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtLugarDeEntrega.MouseDown
        txtLugarDeEntrega.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtCantidadMercanciaEntregada_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtCantidadMercanciaEntregada.MouseDown
        txtCantidadMercanciaEntregada.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtIdentificadorProductoSku_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtIdentificadorProductoSku.MouseDown
        txtIdentificadorProductoSku.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtDescripcionArticulo_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtDescripcionArticulo.MouseDown
        txtDescripcionArticulo.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtUnidadMedida_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtUnidadMedida.MouseDown
        txtUnidadMedida.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtPorcentajeIva_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtPorcentajeIva.MouseDown
        txtPorcentajeIva.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtPorcentajeImpuestoIeps1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtPorcentajeImpuestoIeps1.MouseDown
        txtPorcentajeImpuestoIeps1.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtPorcentajeImpuestoIeps2_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtPorcentajeImpuestoIeps2.MouseDown
        txtPorcentajeImpuestoIeps2.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtPorcentajeImpuestoIeps3_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtPorcentajeImpuestoIeps3.MouseDown
        txtPorcentajeImpuestoIeps3.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtImporteNetoConImpuestosIncluidos_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtImporteNetoConImpuestosIncluidos.MouseDown
        txtImporteNetoConImpuestosIncluidos.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtMontoImpuestoIva_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtMontoImpuestoIva.MouseDown
        txtMontoImpuestoIva.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtMontoImpuestoIeps1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtMontoImpuestoIeps1.MouseDown
        txtMontoImpuestoIeps1.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtMontoImpuestoIeps2_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtMontoImpuestoIeps2.MouseDown
        txtMontoImpuestoIeps2.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub txtMontoImpuestoIeps3_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtMontoImpuestoIeps3.MouseDown
        txtMontoImpuestoIeps3.Focus()
        'Clipboard.Clear()
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        'validarXml(Nothing)
        Me.Close()
    End Sub

    Private Sub tabOxxo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabOxxo.SelectedIndexChanged
        Dim estado As Boolean
        If tabOxxo.SelectedTab Is Detalle Then
            estado = validarCamposEncabezado()
        End If

        If estado Then
            tabOxxo.SelectedTab = Detalle
            habilitarColumnasGrid()
            btnGuardar.Enabled = True
        Else
            tabOxxo.SelectedTab = Encabezado
        End If
    End Sub


#End Region

    Private Sub txtOrdenCompraOxxo_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOrdenCompraOxxo.KeyUp

    End Sub

    Private Sub cboTipoDescuento_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoDescuento.SelectedIndexChanged
        errorGeneral.SetError(cboTipoDescuento, "")
    End Sub

    Private Sub cboTipoDescuento1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoDescuento1.SelectedIndexChanged
        errorGeneral.SetError(cboTipoDescuento1, "")
    End Sub

    Private Sub cboTipoDescuento2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoDescuento2.SelectedIndexChanged
        errorGeneral.SetError(cboTipoDescuento2, "")
    End Sub

    Private Sub cboTipoDescuento3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoDescuento3.SelectedIndexChanged
        errorGeneral.SetError(cboTipoDescuento3, "")
    End Sub

    Private Sub txtNumeroDeVersion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumeroDeVersion.TextChanged

    End Sub

    Private Sub txtIdentificadorProductoSku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIdentificadorProductoSku.TextChanged

    End Sub

    Private Sub txtCrDeTienda_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCrDeTienda.TextChanged

    End Sub

    Private Sub gridArticulos_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridArticulos.CellContentClick

    End Sub
End Class
