Imports MySql.Data.MySqlClient
Public Class frmSemillasLiquidacion
    Private boletas As dbSemillasBoleta
    Private productores As dbproveedores
    Private proveedor As dbproveedores
    Private producto As dbInventario
    Public liquidacion As dbSemillasLiquidacion
    Private anticipos As dbAnticipos
    Private liquidaciones As dbSemillasLiquidacion
    Private detalles As dbSemillasDetalleLiquidacion
    Private facturas As dbSemillasAnticiposFacturas
    Dim IdsSucursales As New elemento
    Private idFactura As Integer
    Private idVenta As Integer
    Private sumaAnticipos As Double = 0
    Private sumaFacturas As Double = 0
    Private sumaBoletas As Double = 0
    Private total As Double = 0
    Private pendiente As Double = 0
    Private idSucursal As Integer
    Private serie As String
    Private folio As Integer
    Private sf As dbSucursalesFolios
    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim ImpNDi2 As New Collection
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    'Dim CodigoBidimensional As Bitmap
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim CuantaY As Integer
    Dim TipoImpresora As Byte
    Dim codigo As String
    Dim idInventario As Integer
    Dim precio As Double
    Dim copias As Integer
    Dim opciones As dbOpciones
    Dim idBoleta As Integer = -1
    Dim precio2 As Double
    Dim precioOriginal As Double
    Dim preciosAnteriores As New List(Of String)
    Dim sel As Boolean = False
    Dim nuevoCambio As Boolean = False
    Private Enum operaciones
        NUEVO = 0
        MODIFICAR = 1
    End Enum
    Private op As operaciones = operaciones.NUEVO

    Public Sub New(ByVal liquidacion As dbSemillasLiquidacion, ByVal idFactura As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        boletas = New dbSemillasBoleta(MySqlcon)
        productores = New dbproveedores(MySqlcon)
        anticipos = New dbAnticipos(MySqlcon)
        liquidaciones = New dbSemillasLiquidacion(MySqlcon)
        opciones = New dbOpciones(MySqlcon)
        Me.liquidacion = liquidacion
        Me.idFactura = idFactura
        'llenaCombo()
        configuraGridAnticipos()

        detalles = New dbSemillasDetalleLiquidacion(MySqlcon)
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        liquidaciones.eliminaSinGuardar()
    End Sub

    Public Sub New()
        sf = New dbSucursalesFolios(GlobalIdSucursalDefault, MySqlcon)
        InitializeComponent()
        boletas = New dbSemillasBoleta(MySqlcon)
        productores = New dbproveedores(MySqlcon)
        anticipos = New dbAnticipos(MySqlcon)
        liquidaciones = New dbSemillasLiquidacion(MySqlcon)
        detalles = New dbSemillasDetalleLiquidacion(MySqlcon)
        liquidacion = New dbSemillasLiquidacion()
        liquidacion.estado = Estados.SinGuardar
        facturas = New dbSemillasAnticiposFacturas(MySqlcon)

        btnBoletas.Enabled = False
        panelBoletas.Enabled = False
        'txtProductor.Visible = False
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        opciones = New dbOpciones(MySqlcon)
        buscaFolio()
        liquidaciones.eliminaSinGuardar()
    End Sub

    Public Sub New(ByVal liquidacion As dbSemillasLiquidacion)
        sf = New dbSucursalesFolios(GlobalIdSucursalDefault, MySqlcon)
        InitializeComponent()
        Me.liquidacion = liquidacion
        boletas = New dbSemillasBoleta(MySqlcon)
        productores = New dbproveedores(MySqlcon)
        anticipos = New dbAnticipos(MySqlcon)
        liquidaciones = New dbSemillasLiquidacion(MySqlcon)
        detalles = New dbSemillasDetalleLiquidacion(MySqlcon)
        facturas = New dbSemillasAnticiposFacturas(MySqlcon)
        opciones = New dbOpciones(MySqlcon)
        txtSerie.Text = liquidacion.folio
        txtFolio.Text = liquidacion.serie
        proveedor = New dbproveedores(liquidacion.proveedor.ID, MySqlcon)
        txtProductor.Text = proveedor.Nombre
        btnLiquidacion.Enabled = False
        actualizaBoletas()
        muestraAnticipos()
        datosProductor()
        Me.Focus()
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(liquidacion.idSucursal)
        liquidaciones.eliminaSinGuardar()
    End Sub
    Private Sub comboProductores_SelectedIndexChanged(sender As Object, e As EventArgs)
        lblDomicilio.Text = proveedor.Direccion & vbCrLf & _
         proveedor.Colonia & vbCrLf & _
        proveedor.Ciudad
        lblRFC.Text = proveedor.RFC

    End Sub

    Private Sub frmLiquidacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvLiqudadas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAnticipos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.Icon = GlobalIcono
    End Sub
    Private Sub configuraGridFacturas()
        dgvFacturas.Columns(0).Visible = False
        dgvFacturas.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub
    Private Sub actualizaBoletas()
        dgvFacturas.DataSource = facturas.vistaFacturas(liquidacion.id)
        configuraGridFacturas()
        dgvLiqudadas.DataSource = liquidaciones.listaBoleta(liquidacion, True)
        configuraGridBoletas()
        sumaBoletas = liquidaciones.sumaBoletas(liquidacion.id, True)
        txtTotalBoletas.Text = Format(sumaBoletas, opciones._formatoTotal)
        txtTotalBoletas.Visible = True
        txtAnticipos.Text = Format(sumaAnticipos, opciones._formatoTotal)
        txtAnticipos.Visible = True
        ' total = sumaBoletas - sumaAnticipos - sumaFacturas

        If liquidacion.id > 0 Then
            sumaAnticipos = anticipos.sumaAnticipos(liquidacion.id)
            txtAnticipos.Text = Format(sumaAnticipos, opciones._formatoTotal)
            txtAnticipos.Visible = True
            sumaFacturas = facturas.sumaFacturas(liquidacion.id)
            txtFacturas.Text = Format(sumaFacturas, opciones._formatoTotal)
            txtFacturas.Visible = True

        End If
        total = sumaBoletas - sumaAnticipos - sumaFacturas
        liquidacion.total = total
        txtTotal.Text = Format(total, opciones._formatoTotal)
        txtTotal.Visible = True
        txtPesoBruto.Text = Format(Math.Round(detalles.sumaPesoBruto(liquidacion.id), 2), "###,###,##0")
        txtPesoFinal.Text = Format(Math.Round(detalles.sumaPesoAnalizado(liquidacion.id), 2), "###,###,##0")
    End Sub


    Private Sub btnBoletas_Click(sender As Object, e As EventArgs) Handles btnBoletas.Click
        If Not proveedor Is Nothing Then
            Dim frm As New frmSemillasBoletasProductor(liquidacion)
            frm.ShowDialog()
            If Not frm.producto Is Nothing Then
                producto = frm.producto
                lblCosecha.Text = producto.Nombre
                actualizaBoletas()
                nuevoCambio = True
            Else
                Return
            End If
        Else
            MsgBox("Debe seleccionar un proveedor.")
        End If

    End Sub

    Private Sub btnFactura_Click(sender As Object, e As EventArgs) Handles btnFactura.Click
        Dim Forma As New frmBuscaDocumentoVenta(3, False, 1, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, False, False, True, 0, True, "", False)
        Forma.ShowDialog()
        If Forma.DialogResult = Windows.Forms.DialogResult.OK Then
            For Each i As Integer In Forma.id
                idVenta = i
                agregarFacturaAnticipo()
                nuevoCambio = True
            Next
        End If
    End Sub

    Private Sub btnAnticipo_Click(sender As Object, e As EventArgs) Handles btnAnticipo.Click
        agregarAnticipo()
    End Sub

    Private Sub agregarAnticipo()
        If txtMedio.Text <> "" And txtImporte.Text <> "" Then
            Dim anticipo As New dbAnticipos()
            Dim medio As String = txtMedio.Text
            Dim importe As Double = Convert.ToDouble(txtImporte.Text)
            anticipo.medio = medio
            anticipo.importe = importe
            anticipo.liquidacion = New dbSemillasLiquidacion(liquidacion.id)
            anticipo.guardado = False

            'anticipo.factura = idFactura
            Try
                anticipos.agregar(anticipo)
                nuevoCambio = True
                muestraAnticipos()
                txtAnticipos.Text = anticipos.sumaAnticipos(liquidacion.id)
                actualizaBoletas()
                limpiaCamposAnticipo()
                txtMedio.Focus()
            Catch ex As Exception
                MsgBox("no se pudo guardar.")
            End Try
        Else
            MsgBox("Debe llenar ambos campos.")
        End If
    End Sub

    Private Sub btnLiquidacion_Click(sender As Object, e As EventArgs) Handles btnLiquidacion.Click
        Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 1, False, False, False)
        frm.ShowDialog()
        If Not frm.Proveedor Is Nothing Then
            Dim prov As dbproveedores = frm.Proveedor
            proveedor = New dbproveedores(prov.ID, MySqlcon)
            datosProductor()
            crearLiquidacion()
            txtProductor.Text = proveedor.Nombre
            btnLiquidacion.Enabled = False
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.LiquidacionAlta, PermisosN.Secciones.Semillas) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If Not proveedor Is Nothing Then
            If guardar() Then

                '  If MsgBox("Debe seleccionar un proveedor.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Imprimir()
                'End If
                Nuevo()
            End If

        Else
            MsgBox("Debe seleccionar un proveedor.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Function guardar() As Boolean
        If IsNumeric(txtFolio.Text) Then
            If liquidacion.estado = Estados.Guardada Then
                If txtSerie.Text <> serie Or CInt(txtFolio.Text) <> folio Then
                    If liquidaciones.checaFolioRepetido(txtSerie.Text, CInt(txtFolio.Text)) = False Then
                        'idSucursal = IdsSucursales.Valor(ComboBox3.SelectedIndex)
                        'liquidacion.idSucursal = idSucursal
                        'liquidacion.estado = Estados.Guardada
                        liquidacion.fecha = DateTimePicker1.Value.ToString("yyyy/MM/dd")
                        liquidaciones.actualizar(liquidacion)
                        detalles.guardar(liquidacion.id)
                        facturas.guardar(liquidacion.id)
                        anticipos.guardar(liquidacion.id)
                        'PopUp("Guardado", 30)
                        'Nuevo()
                        Return True
                    Else
                        MsgBox("Folio repetido.")
                        Return False
                    End If
                Else
                    liquidaciones.actualizar(liquidacion)
                    detalles.guardar(liquidacion.id)
                    facturas.guardar(liquidacion.id)
                    anticipos.guardar(liquidacion.id)
                    ' PopUp("Guardado", 30)
                    ' Nuevo()
                    Return True
                End If
            Else
                If txtSerie.Text <> serie Or CInt(txtFolio.Text) <> folio Then
                    If liquidaciones.checaFolioRepetido(txtSerie.Text, CInt(txtFolio.Text)) = False Then
                        idSucursal = IdsSucursales.Valor(ComboBox3.SelectedIndex)
                        liquidacion.idSucursal = idSucursal
                        liquidacion.estado = Estados.Guardada
                        liquidacion.serie = txtSerie.Text
                        liquidacion.folio = txtFolio.Text
                        liquidaciones.actualizar(liquidacion)
                        detalles.guardar(liquidacion.id)
                        facturas.guardar(liquidacion.id)
                        anticipos.guardar(liquidacion.id)
                        'PopUp("Guardado", 30)
                        'Nuevo()
                        Return True
                    Else
                        MsgBox("Folio repetido.")
                        Return False
                    End If
                Else
                    idSucursal = IdsSucursales.Valor(ComboBox3.SelectedIndex)
                    liquidacion.idSucursal = idSucursal
                    liquidacion.estado = Estados.Guardada
                    liquidacion.serie = txtSerie.Text
                    liquidacion.folio = txtFolio.Text
                    liquidaciones.actualizar(liquidacion)
                    detalles.guardar(liquidacion.id)
                    facturas.guardar(liquidacion.id)
                    anticipos.guardar(liquidacion.id)
                    'PopUp("Guardado", 30)
                    'Nuevo()
                    Return True
                End If
            End If
        Else
            MsgBox("Formato de folio incorrecto. Deben ser solo números.")
            txtFolio.BackColor = Color.Red
            Return False
        End If
    End Function

    Private Sub configuraGridAnticipos()
        dgvAnticipos.Columns(0).Visible = False
        dgvAnticipos.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub
    Private Sub configuraGridBoletas()
        dgvLiqudadas.ReadOnly = True
        dgvLiqudadas.Columns(0).Visible = False
        dgvLiqudadas.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvLiqudadas.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvLiqudadas.Columns(5).ReadOnly = False
        dgvLiqudadas.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    Private Sub agregarFacturaAnticipo()
        Dim Op As New dbOpciones(MySqlcon)
        Dim C As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        Dim anticipo As New dbSemillasAnticiposFacturas()
        'anticipo.factura = idVenta
        anticipo.importe = C.TotalaPagar
        anticipo.idLiquidacion = liquidacion.id
        anticipo.idVenta = C.ID
        anticipo.importe = C.TotalaPagar
        anticipo.guardado = False
        facturas.agregar(anticipo)
        'muestraAnticipos()
        'txtAnticipos.Text = anticipos.sumaAnticipos(liquidacion.id)
        dgvFacturas.DataSource = facturas.vistaFacturas(liquidacion.id)
        actualizaBoletas()
    End Sub

    Private Sub muestraAnticipos()
        dgvAnticipos.DataSource = anticipos.vistaAnticipos(liquidacion)
        configuraGridAnticipos()
    End Sub

    Private Sub panelBoletas_Paint(sender As Object, e As PaintEventArgs) Handles panelBoletas.Paint
        dgvLiqudadas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAnticipos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub datosProductor()
        lblDomicilio.Text = proveedor.Direccion + " " + proveedor.NoExterior + " " + proveedor.Colonia + " " + proveedor.Ciudad + " " + proveedor.Estado
        lblRFC.Text = proveedor.RFC
    End Sub

    Private Sub crearLiquidacion()
        Dim subtotal As Double = boletas.totalBoletasProductorLiquidadas(proveedor, False)
        Dim total = boletas.totalBoletasProductor(proveedor)
        liquidacion = New dbSemillasLiquidacion(subtotal, total, proveedor, txtFolio.Text, txtSerie.Text, Estados.Inicio)
        liquidaciones.agregar(liquidacion)
        liquidacion = liquidaciones.obten(liquidacion.serie, liquidacion.folio)
        liquidacion.fecha = DateTimePicker1.Value.ToString("yyyy/MM/dd")
        txtSerie.Text = liquidacion.serie
        txtFolio.Text = liquidacion.folio
        folio = CInt(liquidacion.folio)
        serie = liquidacion.serie
        'liquidacion = liquidaciones.obten(New dbLiquidacion(liquidaciones.ultimaLiquiacion))
        btnLiquidacion.Enabled = False
        btnBoletas.Enabled = True
        panelBoletas.Enabled = True
        ' comboProductores.Visible = False
        txtProductor.Visible = True
        txtProductor.Text = proveedor.Nombre
        If Not proveedor Is Nothing Then
            Dim frm As New frmSemillasBoletasProductor(liquidacion)
            frm.ShowDialog()
            If Not frm.producto Is Nothing Then
                producto = frm.producto
                'lblCosecha.Text = producto.Nombre
                'lblCosecha.Text = "SEMILLA"
            End If
            actualizaBoletas()
        End If
    End Sub

    Private Sub txtMedio_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMedio.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtImporte.Focus()
        End If
    End Sub

    Private Sub txtImporte_KeyDown(sender As Object, e As KeyEventArgs) Handles txtImporte.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtMedio.Text <> "" And txtImporte.Text <> "" Then
                agregarAnticipo()
            Else
                MsgBox("debe llenar los 2 campos.")
            End If
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        If liquidacion.estado = Estados.Inicio And proveedor Is Nothing Then
            Dim result As Integer = MessageBox.Show("¿Desea Guardar los cambios?", "Semagro", MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Yes Then
                If guardar() Then
                    Imprimir()
                End If
            ElseIf result = DialogResult.Cancel Then
                Exit Sub
            ElseIf result = DialogResult.No Then
                If liquidacion.estado = Estados.Inicio Then
                    liquidacion.estado = Estados.SinGuardar
                    liquidacion.idSucursal = idSucursal
                    detalles.borraSinGuardar(liquidacion.id)
                    facturas.borraSinGuardar(liquidacion.id)
                    anticipos.borraSinGuardar(liquidacion.id)
                    regresaPreciosOriginales()
                    liquidaciones.eliminar(liquidacion)
                Else
                    regresaPreciosOriginales()
                End If
            End If
        ElseIf liquidacion.estado = Estados.Guardada Then
            If nuevoCambio Then
                Dim result As Integer = MessageBox.Show("¿Desea Guardar los cambios?", "Semagro", MessageBoxButtons.YesNoCancel)
                If result = DialogResult.Yes Then
                    If guardar() Then
                        Imprimir()
                    End If
                ElseIf result = DialogResult.Cancel Then
                    Exit Sub
                ElseIf result = DialogResult.No Then
                    liquidacion.idSucursal = idSucursal
                    detalles.borraSinGuardar(liquidacion.id)
                    facturas.borraSinGuardar(liquidacion.id)
                    anticipos.borraSinGuardar(liquidacion.id)
                    regresaPreciosOriginales()
                    liquidaciones.eliminar(liquidacion)
                    regresaPreciosOriginales()
                    nuevoCambio = False
                End If
            End If
        End If
        buscar()
    End Sub

    Private Sub buscar()
        Dim frm As New frmSemillasConsulta(frmSemillasConsulta.tipoConsulta.liquidaciones)
        frm.ShowDialog()
        If Not frm.liquidacion Is Nothing Then
            liquidacion = frm.liquidacion
            op = operaciones.MODIFICAR
            txtSerie.Text = liquidacion.serie
            txtFolio.Text = liquidacion.folio
            serie = liquidacion.serie
            folio = liquidacion.folio
            producto = New dbInventario(liquidaciones.productoLiquidacion(liquidacion.id), MySqlcon)
            lblCosecha.Text = producto.Nombre
            proveedor = New dbproveedores(liquidacion.proveedor.ID, MySqlcon)
            'comboProductores.Visible = False
            txtProductor.Text = proveedor.Nombre
            btnLiquidacion.Enabled = False
            DateTimePicker1.Value = Date.Parse(liquidacion.fecha)
            ComboBox3.SelectedIndex = IdsSucursales.Busca(liquidacion.idSucursal)
            actualizaBoletas()
            muestraAnticipos()
            datosProductor()
            btnLiquidacion.Enabled = False
            panelBoletas.Enabled = True
            btnGuardar.Enabled = True
            btnBoletas.Enabled = True
            btnNuevo.Enabled = True
            ComboBox3.Enabled = False
        End If
    End Sub

    Public Sub buscaFolio()
        If sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.SemillasLiquidacion, 0) Then
            txtSerie.Text = sf.Serie
        Else
            txtSerie.Text = ""
            sf.FolioInicial = 1
        End If

        txtFolio.Text = liquidaciones.DaNuevoFolio(txtSerie.Text)
        If CInt(txtFolio.Text) < sf.FolioInicial Then
            txtFolio.Text = sf.FolioInicial.ToString
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        buscaFolio()
    End Sub

    Private Sub btNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Nuevo()
    End Sub

    Private Sub Nuevo()
        If liquidacion.estado = Estados.Inicio Then
            If Not proveedor Is Nothing Then
                Dim result As Integer = MessageBox.Show("DeseaGuardar los cambios", "Semagro", MessageBoxButtons.YesNoCancel)
                If result = DialogResult.Cancel Then
                    Return
                ElseIf result = DialogResult.No Then
                    idSucursal = IdsSucursales.Valor(ComboBox3.SelectedIndex)
                    If Not proveedor Is Nothing Then
                        If liquidacion.estado = Estados.Inicio Then
                            liquidacion.estado = Estados.SinGuardar
                            liquidacion.idSucursal = idSucursal
                            'liquidaciones.actualizar(liquidacion)
                            detalles.borraSinGuardar(liquidacion.id)
                            facturas.borraSinGuardar(liquidacion.id)
                            anticipos.borraSinGuardar(liquidacion.id)
                            liquidaciones.eliminar(liquidacion)
                        End If
                    End If
                    ComboBox3.Enabled = True
                ElseIf result = DialogResult.Yes Then
                    guardar()
                    ComboBox3.Enabled = True
                End If
            End If
            op = operaciones.NUEVO
            nuevoCambio = False
            liquidacion = New dbSemillasLiquidacion()
            liquidacion.estado = Estados.Inicio
            btnLiquidacion.Enabled = True
            btnBoletas.Enabled = False
            panelBoletas.Enabled = False
            txtProductor.Visible = True
            preciosAnteriores = New List(Of String)
            txtFolio.Text = ""
            txtSerie.Text = ""
            txtProductor.Text = ""
            lblCosecha.Text = ""
            lblDomicilio.Text = ""
            lblRFC.Text = ""
            txtImporte.Text = ""
            txtMedio.Text = ""
            txtPesoBruto.Text = ""
            txtPesoFinal.Text = ""
            txtTotalBoletas.Text = ""
            txtAnticipos.Text = ""
            txtTotal.Text = ""
            txtPrecio.Text = ""
            DateTimePicker1.Value = Date.Now
            serie = ""
            folio = -1
            dgvAnticipos.Columns.Clear()
            dgvFacturas.Columns.Clear()
            dgvLiqudadas.Columns.Clear()
            btnBoletas.Enabled = False
            panelBoletas.Enabled = False
            proveedor = Nothing
            ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
            buscaFolio()
            ComboBox3.Enabled = True
        Else
            op = operaciones.NUEVO
            liquidacion = New dbSemillasLiquidacion()
            btnLiquidacion.Enabled = True
            btnBoletas.Enabled = False
            panelBoletas.Enabled = False
            txtProductor.Visible = True
            preciosAnteriores = New List(Of String)
            txtFolio.Text = ""
            txtSerie.Text = ""
            txtProductor.Text = ""
            lblCosecha.Text = ""
            lblDomicilio.Text = ""
            lblRFC.Text = ""
            txtImporte.Text = ""
            txtMedio.Text = ""
            txtPesoBruto.Text = ""
            txtPesoFinal.Text = ""
            txtTotal.Text = ""
            txtTotalBoletas.Text = ""
            txtAnticipos.Text = ""
            txtPrecio.Text = ""
            serie = ""
            DateTimePicker1.Value = Date.Now
            folio = -1
            dgvAnticipos.Columns.Clear()
            dgvFacturas.Columns.Clear()
            dgvLiqudadas.Columns.Clear()
            btnBoletas.Enabled = False
            panelBoletas.Enabled = False
            proveedor = Nothing
            ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
            buscaFolio()
            ComboBox3.Enabled = True
        End If
    End Sub

    Private Sub dgvFacturas_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgvFacturas.MouseDoubleClick
        Dim result As Integer = MessageBox.Show("¿Desea eliminar esta factura de la liquidación?", GlobalNombreApp, MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Yes Then
            Dim id As Integer = CInt(dgvFacturas.CurrentRow.Cells(0).Value)
            facturas.eliminar(id)
            actualizaBoletas()
        End If
    End Sub

    Private Sub dgvAnticipos_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgvAnticipos.MouseDoubleClick
        Dim result As Integer = MessageBox.Show("¿Desea eliminar este anticipo de la liquidación?", GlobalNombreApp, MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Yes Then
            Dim id As Integer = CInt(dgvAnticipos.CurrentRow.Cells(0).Value)
            anticipos.eliminar(id)
            muestraAnticipos()
            txtAnticipos.Text = anticipos.sumaAnticipos(liquidacion.id)
            actualizaBoletas()
        End If
    End Sub

    Private Sub limpiaCamposAnticipo()
        txtMedio.Text = ""
        txtImporte.Text = ""
    End Sub

    Private Sub frmSemillasLiquidacion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not proveedor Is Nothing Then
            Dim result As Integer = MessageBox.Show("¿Desea guardar los cambios?", GlobalNombreApp, MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Cancel Then
                e.Cancel = True
            ElseIf result = DialogResult.No Then
                idSucursal = IdsSucursales.Valor(ComboBox3.SelectedIndex)
                If Not proveedor Is Nothing Then
                    If liquidacion.estado = Estados.SinGuardar Then
                        liquidacion.estado = Estados.SinGuardar
                        liquidacion.idSucursal = idSucursal
                        detalles.borraSinGuardar(liquidacion.id)
                        facturas.borraSinGuardar(liquidacion.id)
                        anticipos.borraSinGuardar(liquidacion.id)
                        regresaPreciosOriginales()
                        liquidaciones.eliminar(liquidacion)
                    Else
                        regresaPreciosOriginales()
                    End If
                End If
                e.Cancel = False
            ElseIf result = DialogResult.Yes Then
                If guardar() Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            End If
        End If
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

    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.semillasLiquidacion, liquidacion.idSucursal)
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.SemillasLiquidacion + 100, liquidacion.idSucursal)
        End If
        If TipoImpresora = 0 Then
            DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujo(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub

    Private Sub DibujaPaginaEstatico(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)

        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos

        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        Dim codigos As New Collection

        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try

            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.semillasLiquidacion, liquidacion.idSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.SemillasLiquidacion + 1000, liquidacion.idSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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
                If n.DataPropertyName = "codigobi" Then
                    ncb = n
                    codigos.Add(ncb)
                End If

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
            For Each n As NodoImpresionN In codigos

                'If n.Visible = 1 Then e.DrawImage(CodigoBidimensional, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
            Next
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DibujaPaginaFlujo(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
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

            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.SemillasLiquidacion, liquidacion.idSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.SemillasLiquidacion + 1000, liquidacion.idSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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
            'If ncb.Visible = 1 And ncb.Tipo = 0 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
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
            If YCoord >= LimY And Posicion > 0 And (pModo = 0 Or pModo = 2) Then
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
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra

            '***************************segundo
            Dim Haysegundo As Boolean = False
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 And (pModo = 0 Or pModo = 2) Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    Haysegundo = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
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

            'If ncb.Visible = 1 And ncb.Tipo = 2 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(Math.Abs((ncb.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub



    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'If iTipoFacturacion = 2 Then
        '    'CFDi
        '    DibujaPaginai(e.Graphics)
        'Else
        'CFD

        DibujaPaginaN(e.Graphics)
        If MasPaginas = True Or NumeroPagina > 2 Then
            'e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00") + "/" + Format(CuantaY, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
            e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If

        'If Estado = Estados.Cancelada Then
        '    e.Graphics.DrawString("CANCELADA", New Font("Arial", 18, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, 80, 130)
        'End If
        e.HasMorePages = MasPaginas
        'DibujaPagina(e.Graphics)
        'e.HasMorePages = True
        'End If


    End Sub

    Private Sub Imprimir()
        Dim suc As New dbSucursales(liquidacion.idSucursal, MySqlcon)

        'Dim SA As New dbSucursalesArchivos
        'Dim Impresora As String
        PrintDialog1.PrinterSettings.Copies = copias

        Dim RutaPDF As String
        Dim Archivos As New dbSucursalesArchivos
        RutaPDF = Archivos.DaRutaArchivos(liquidacion.idSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, False)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(liquidacion.fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(liquidacion.fecha), "yyyy") + "\" + Format(CDate(liquidacion.fecha), "MM") + "\")
        RutaPDF = RutaPDF + "\" + Format(CDate(liquidacion.fecha), "yyyy") + "\" + Format(CDate(liquidacion.fecha), "MM")
        'Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        ' Dim TipoImpresora As Byte
        Impresora = Archivos.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.SemillasLiquidacion)
        TipoImpresora = Archivos.TipoImpresora
        PrintDocument1.PrinterSettings.PrinterName = Impresora
        PrintDocument1.DocumentName = " Liquidacion" + liquidacion.serie + liquidacion.folio
        If Impresora = "Bullzip PDF Printer" Then
            Dim obj As New Bullzip.PdfWriter.PdfSettings
            obj.Init()
            obj.PrinterName = Impresora
            obj.WriteSettings()

            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            obj.SetValue("ShowSettings", "never")
            obj.SetValue("ShowPDF", "yes")
            obj.SetValue("ShowSaveAS", "nofile")
            obj.SetValue("ConfirmOverwrite", "no")
            obj.SetValue("Target", "printer")
            obj.WriteSettings()
        End If
        PrintDocument1.PrinterSettings.PrinterName = Impresora
        LlenaNodosImpresion()
        If TipoImpresora = 0 Then
            LlenaNodos(suc.ID, TiposDocumentos.SemillasLiquidacion)
        Else
            LlenaNodos(suc.ID, TiposDocumentos.SemillasLiquidacion + 1000)
        End If

        PrintDocument1.Print()


        'Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        'If V.ISR <> 0 Or V.IvaRetenido <> 0 Then
        '    If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '        LlenaNodosImpresionRet()
        '        If TipoImpresora = 0 Then
        '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencion)
        '        Else
        '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
        '        End If
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
        '        DocAImprimir = 1
        '        PrintDocument1.Print()

        '    End If
        'End If
        'imprimirNotarial()
        'If V.Cliente.Email <> "" Then
        '    Try
        '        If MsgBox("¿Enviar factura por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '            If V.Cliente.Email <> "" Then
        '                Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
        '                'Dim O As New dbOpciones(MySqlcon)
        '                Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        '                Dim C As String
        '                C = "Eviado por: " + S.NombreFiscal + vbNewLine + "RFC: " + S.RFC + vbNewLine + "FACTURA" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Comprobante fiscal enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje."
        '                'If pEstado = Estados.Pendiente Then
        '                M.send("Factura: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\Fac" + TextBox11.Text + TextBox2.Text + ".pdf", "")
        '                'Else
        '                '   M.send("Comprobante Fiscal Digital Factura: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\FAC-" + V.Serie + V.Folio.ToString + ".pdf", RutaXml + "\FAC-" + V.Serie + V.Folio.ToString + ".xml")
        '                'End If

        '                PopUp("Correo enviado", 90)
        '            End If
        '        End If
        '    Catch ex As Exception
        '        MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        '    End Try
        'End If

    End Sub
    Private Sub LlenaNodosImpresion()
        Dim O As New dbOpciones(MySqlcon)
        Dim art As New dbInventarioPrecios(MySqlcon)
        Dim totalHumedad As Double = 0
        Dim totalImpurezas As Double = 0
        Dim totalDanado As Double = 0
        Dim totalQuebrado As Double = 0
        Dim totalPeso As Double = 0
        Dim totalLiquidar As Double = 0
        Dim totalDeducciones As Double = 0
        ImpND.Clear()
        ImpNDD.Clear()
        CuantosRenglones = 0
        Posicion = 0
        NumeroPagina = 1
        Dim dr As MySqlDataReader
        dr = liquidaciones.boletas(liquidacion.id)
        Dim cont As Integer = 0
        While dr.Read()
            ImpNDD.Add(New NodoImpresionN("", "fecha", convierteFecha(dr("fecha")), 0), "fecha" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "boleta", CInt(dr("folio")).ToString("000"), 0), "boleta" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "pesoBruto", Format(dr("peso"), "#,###,##0").PadLeft(10, " "), 0), "pesoBruto" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "humedad", Format(dr("humedad"), "#,###,##0").PadLeft(10, " "), 0), "humedad" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "danado", Format(dr("granoDanado"), "#,###,##0").PadLeft(10, " "), 0), "danado" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "impurezas", Format(dr("impurezas"), "#,###,##0").PadLeft(10, " "), 0), "impurezas" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "quebrado", Format(dr("granoQuebrado"), "#,###,##0").PadLeft(10, " "), 0), "quebrado" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "pesoanalizado", Format(Math.Round(dr("pesoanalizado"), 2), "#,###,##0").PadLeft(10, " "), 0), "pesoanalizado" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "precio", Format(dr("precio") * 1000, "$#,###,##0.00###").PadLeft(10, " "), 0), "precio" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "importe", Format(dr("importe"), O._formatoTotal).PadLeft(O.Espaciototal, " "), 0), "importe" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "total deducciones", Format(Math.Round(dr("castigototal"), 2), "#,###,##0").PadLeft(10, " "), 0), "total deducciones" + Format(cont, "000"))
            cont += 1
            CuantosRenglones += 1

            totalHumedad += dr("humedad")
            totalImpurezas += dr("impurezas")
            totalDanado += dr("granoDanado")
            totalQuebrado += dr("granoQuebrado")
            totalDeducciones += dr("castigototal")
            totalPeso += dr("peso")
            totalLiquidar += dr("pesoanalizado")
        End While
        dr.Close()
        Dim contF As String = ""
        Dim importes As String = ""
        Dim dr2 As MySqlDataReader = liquidaciones.facturas(liquidacion.id, True)
        While dr2.read()
            contF += dr2("folio").ToString() + vbCrLf
            importes += Format(dr2("importe"), O._formatoTotal).PadLeft(10, " ") + vbCrLf
        End While
        'importes.Replace(".", ",")
        dr2.close()

        Dim dr3 As MySqlDataReader = liquidaciones.anticipos(liquidacion.id)
        Dim contA As String = ""
        Dim importesA As String = ""
        While dr3.Read()
            contA += dr3("medio").ToString() + vbCrLf
            importesA += Format(dr3("importe"), O._formatoTotal).PadLeft(10, " ") + vbCrLf
        End While
        importesA.Replace(".", ",")
        dr3.Close()
        Dim i As New dbInventario(idInventario, MySqlcon)
        Dim l As New CLetras
        Dim e As New dbSucursales(liquidacion.idSucursal, MySqlcon)
        ImpND.Add(New NodoImpresionN("", "folioLiquidacion", (liquidacion.serie + (CInt(liquidacion.folio).ToString("000"))), 0), "folioLiquidacion")
        ImpND.Add(New NodoImpresionN("", "subtotal", Format(sumaBoletas, O._formatoSubtotal).PadLeft(O.EspacioSubtotal, " "), 0), "subtotal")
        ImpND.Add(New NodoImpresionN("", "totalFacturas", Format(sumaFacturas, O._formatoTotal).PadLeft(O.Espaciototal, " "), 0), "totalFacturas")
        ImpND.Add(New NodoImpresionN("", "totalAnticipos", Format(sumaAnticipos, O._formatoTotal).PadLeft(O.Espaciototal, " "), 0), "totalAnticipos")
        ImpND.Add(New NodoImpresionN("", "total", Format(total, O._formatoTotal).PadLeft(O.Espaciototal, " "), 0), "total")
        ImpND.Add(New NodoImpresionN("", "letra", l.LetrasM(total, 2, GlobalIdiomaLetras), 0), "letra")
        ImpND.Add(New NodoImpresionN("", "cultivo", producto.Nombre, 0), "cultivo")
        Dim direccionEmpresa As String = e.Direccion + vbCrLf + e.NoExterior + " " + e.Colonia + " " + e.Ciudad + " " + e.Estado + " " + e.CP
        ImpND.Add(New NodoImpresionN("", "direccionEmpresa", direccionEmpresa, 0), "direccionEmpresa")
        ImpND.Add(New NodoImpresionN("", "rfcEmpresa", e.RFC, 0), "rfcEmpresa")
        ImpND.Add(New NodoImpresionN("", "nombreEmpresa", e.Nombre, 0), "nombreEmpresa")
        ImpND.Add(New NodoImpresionN("", "nombreEmpresa2", proveedor.Nombre, 0), "nombreEmpresa2")
        ImpND.Add(New NodoImpresionN("", "nombreProductor", proveedor.Nombre, 0), "nombreProductor")
        Dim direccionProductor As String = proveedor.Direccion + " " + proveedor.NoExterior + " " + proveedor.Colonia + " " + proveedor.Ciudad + " " + proveedor.Estado + " " + proveedor.CP
        ImpND.Add(New NodoImpresionN("", "direccionProductor", direccionProductor, 0), "direccionProductor")
        ImpND.Add(New NodoImpresionN("", "rfcProductor", proveedor.RFC, 0), "rfcProductor")
        ImpND.Add(New NodoImpresionN("", "fechaLiquidacion", convierteFecha(liquidacion.fecha), 0), "fechaLiquidacion")
        ImpND.Add(New NodoImpresionN("", "conceptoAnticipo", contA, 0), "conceptoAnticipo")
        'ImpND.Add(New NodoImpresionN("", "importeAnticipo", Format(importesA, O._formatoTotal).PadLeft(O.Espaciototal, ""), 0), "importeAnticipo")
        ImpND.Add(New NodoImpresionN("", "importeAnticipo", importesA, 0), "importeAnticipo")
        ImpND.Add(New NodoImpresionN("", "factura", contF, 0), "factura")
        'ImpND.Add(New NodoImpresionN("", "importefactura", Format(importes, O._formatoTotal).PadLeft(O.Espaciototal, ""), 0), "importefactura")
        ImpND.Add(New NodoImpresionN("", "importefactura", importes, 0), "importefactura")
        ImpND.Add(New NodoImpresionN("", "representante", proveedor.representateLegal, 0), "representante")
        ImpND.Add(New NodoImpresionN("", "totalHumedad", Format(Math.Round(totalHumedad, 2), "#,###,##0").PadLeft(10, " "), 0), "totalHumedad")
        ImpND.Add(New NodoImpresionN("", "totalImpurezas", Format(Math.Round(totalImpurezas, 2), "#,###,##0").PadLeft(10, " "), 0), "totalImpurezas")
        ImpND.Add(New NodoImpresionN("", "totalDanado", Format(Math.Round(totalDanado, 2), "#,###,##0").PadLeft(10, " "), 0), "totalDanado")
        ImpND.Add(New NodoImpresionN("", "totalQuebrado", Format(Math.Round(totalQuebrado, 2), "#,###,##0").PadLeft(10, " "), 0), "totalQuebrado")
        ImpND.Add(New NodoImpresionN("", "totaPeso", Format(Math.Round(totalPeso, 2), "#,###,##0").PadLeft(10, " "), 0), "totaPeso")
        ImpND.Add(New NodoImpresionN("", "totalPesoLiquidar", Format(Math.Round(totalLiquidar, 2), "#,###,##0").PadLeft(10, " "), 0), "totalPesoLiquidar")
        ImpND.Add(New NodoImpresionN("", "totalDeducciones2", Format(Math.Round(totalDeducciones, 2), "#,###,##0").PadLeft(10, " "), 0), "totalDeducciones2")
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Imprimir()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub


    Private Sub dgvLiqudadas_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvLiqudadas.CellFormatting
        If e.ColumnIndex = 5 Then
            e.Value = Format(e.Value, "$###,##0.00######")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        If e.ColumnIndex = 6 Then
            e.Value = Format(e.Value, opciones._formatoTotal)
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        If e.ColumnIndex = 4 Then
            e.Value = Format(e.Value, "#,###,##0")
        End If
        If e.ColumnIndex = 2 Then
            e.Value = convierteFecha(e.Value.ToString())
        End If
    End Sub

    Private Sub dgvFacturas_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvFacturas.CellFormatting
        If e.ColumnIndex = 2 Then
            e.Value = Format(e.Value, opciones._formatoTotal)
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub dgvAnticipos_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvAnticipos.CellFormatting
        If e.ColumnIndex = 2 Then
            e.Value = Format(e.Value, opciones._formatoTotal)
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub frmSemillasLiquidacion_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            buscar()
        End If
    End Sub
    Private Function convierteFecha(ByVal fecha As String) As String
        Dim arr() = fecha.Split("/")
        Select Case arr(1)
            Case "01"
                arr(1) = "ene"
            Case "02"
                arr(1) = "feb"
            Case "03"
                arr(1) = "mar"
            Case "04"
                arr(1) = "abr"
            Case "05"
                arr(1) = "may"
            Case "06"
                arr(1) = "jun"
            Case "07"
                arr(1) = "jul"
            Case "08"
                arr(1) = "ago"
            Case "09"
                arr(1) = "sep"
            Case "10"
                arr(1) = "oct"
            Case "11"
                arr(1) = "nov"
            Case "12"
                arr(1) = "dic"
        End Select
        Dim aux = arr(2) + "/" + arr(1) + "/" + arr(0)
        Return aux
    End Function

    Private Sub dgvLiqudadas_DoubleClick(sender As Object, e As EventArgs) Handles dgvLiqudadas.DoubleClick
        Try
            Dim result As Integer = MessageBox.Show("¿Desea eliminar la boleta?", GlobalNombreApp, MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Yes Then
                Dim idboleta As Integer = CInt(dgvLiqudadas.CurrentRow.Cells(0).Value.ToString())
                liquidaciones.eliminarBoletaLiquidacion(idboleta, liquidacion.id)
                actualizaBoletas()
            Else
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnVer_Click(sender As Object, e As EventArgs) Handles btnVer.Click
        If idBoleta > 0 Then
            Dim f As New frmSemillasBoleta(New dbSemillasBoleta(idBoleta), GlobalSemillasResumida, GlobalPermisos.ChecaPermiso(PermisosN.Semillas.PrecioVerBoleta, PermisosN.Secciones.Semillas))
            f.ShowDialog()
            actualizaBoletas()
        End If
    End Sub

    Private Sub dgvLiqudadas_Click(sender As Object, e As EventArgs) Handles dgvLiqudadas.Click
        idBoleta = CInt(dgvLiqudadas.CurrentRow.Cells(0).Value.ToString())
        txtPrecio.Text = dgvLiqudadas.CurrentRow.Cells(5).Value.ToString()
        sel = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim sel As Boolean = False
        'For i As Integer = 0 To dgvLiqudadas.RowCount - 1
        '    If dgvLiqudadas.Rows(i).Selected Then
        '        sel = True
        '        Exit For
        '    End If
        'Next
        If sel = False Then
            MsgBox("Debe seleccionar almenos una boleta.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If txtPrecio.Text = "" Then
            MsgBox("Debe indicar un precio.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.BoletasAlta, PermisosN.Secciones.Semillas) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        precio2 = CDbl(txtPrecio.Text)
        For i As Integer = 0 To dgvLiqudadas.RowCount - 1
            If dgvLiqudadas.Rows(i).Selected Then
                idBoleta = CInt(dgvLiqudadas.Rows(i).Cells(0).Value.ToString())
                precioOriginal = CDbl(dgvLiqudadas.CurrentRow.Cells(5).Value.ToString())
                Dim s As String = idBoleta.ToString() + "," + precioOriginal.ToString()
                preciosAnteriores.Add(s)
                boletas.actualizaPrecio(idBoleta, precio2)
                nuevoCambio = True
            End If
        Next
        actualizaBoletas()
        txtPrecio.Text = ""
        sel = False
    End Sub
 

    Private Sub txtPrecio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrecio.KeyPress
        NumConFrac(txtPrecio, e)
    End Sub

    Public Sub NumConFrac(ByVal CajaTexto As Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "." And Not CajaTexto.Text.IndexOf(".") Then
            e.Handled = True
        ElseIf e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub regresaPreciosOriginales()
        If preciosAnteriores.Count > 0 Then
            For Each a As String In preciosAnteriores
                idBoleta = CInt(a.Split(",")(0))
                precio2 = CDbl(a.Split(",")(1))
                boletas.actualizaPrecio(idBoleta, precio2)
            Next
            txtPrecio.Text = ""
        End If
    End Sub

    Private Sub dgvLiqudadas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvLiqudadas.CellContentClick

    End Sub
End Class