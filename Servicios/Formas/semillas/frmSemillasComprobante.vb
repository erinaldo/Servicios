
Imports MySql.Data.MySqlClient


Public Class frmSemillasComprobante
    Private proveedor As dbproveedores
    Private liquidacion As dbSemillasLiquidacion
    Private proveedores As dbproveedores
    Private detalles As dbSemillasDetalleComprobante
    Private comprobantes As dbSemillasComprobante
    Private detalle As dbSemillasDetalleComprobante
    Private comprobante As dbSemillasComprobante
    Private cliente As dbClientes
    Private idCompra As Integer
    Private compras As dbCompras
    Private cantidad As Double
    Private nombreProducto As String
    Private serie As String
    Private folio As Integer
    Private AGREGAR As Integer = 0
    Private MODIFICAR As Integer = 1
    Private op As Integer = AGREGAR
    Private idDetalle As Integer
    Dim IdsSucursales As New elemento
    Private idSucursal As Integer
    Private sf As dbSucursalesFolios

    Public Sub New(ByRef liquidacion As dbSemillasLiquidacion)
        InitializeComponent()
        Me.liquidacion = liquidacion
        proveedores = New dbproveedores(MySqlcon)
        detalles = New dbSemillasDetalleComprobante(MySqlcon)
        comprobantes = New dbSemillasComprobante(MySqlcon)
        llenaDatosEmpresa()
    End Sub

    Public Sub New(ByVal idcompra As Integer)
        InitializeComponent()
        Me.idCompra = idcompra
        proveedores = New dbproveedores(MySqlcon)
        detalles = New dbSemillasDetalleComprobante(MySqlcon)
        comprobantes = New dbSemillasComprobante(MySqlcon)
        'llenaDatosEmpresa()
        txtSerie.Text = serie
    End Sub

    Public Sub New()
        sf = New dbSucursalesFolios(GlobalIdSucursalDefault, MySqlcon)
        ' This call is required by the designer.
        InitializeComponent()
        idSucursal = GlobalIdSucursalDefault
        ' Add any initialization after the InitializeComponent() call.
        proveedores = New dbproveedores(MySqlcon)
        detalles = New dbSemillasDetalleComprobante(MySqlcon)
        comprobantes = New dbSemillasComprobante(MySqlcon)
        comprobante = New dbSemillasComprobante(comprobantes.crearComprobante, MySqlcon)
        txtSerie.Text = serie
        txtFolio.Text = comprobante.id
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        llenaDatosEmpresa()
        btnModificar.Enabled = False
        btnAgregar.Enabled = False
    End Sub
    Private Sub llenarDatos()
        txtRazonSocial.Text = cliente.Nombre
        txtRFCmoral.Text = cliente.RFC
        'txtNombreMoral.Text = cliente.Representante
        txtDireccionMoral.Text = cliente.Direccion
        txtColoniaMoral.Text = cliente.Colonia
        txtMunicipioMoral.Text = cliente.Municipio
        txtCiudadMoral.Text = cliente.Ciudad
        txtEstadoMoral.Text = cliente.Estado
        txtCPMoral.Text = cliente.CP
        txtTelMoral.Text = cliente.Telefono
        comprobantes.DaultimoRepresentante(cliente.ID)
        txtApellidoP.Text = comprobantes.AP
        txtApellidoM.Text = comprobantes.AM
        txtNombreMoral.Text = comprobantes.NR
        txtSocio.Text = comprobante.socioPersonaMoral
    End Sub

    Public Sub New(ByRef comprobante As dbSemillasComprobante)
        sf = New dbSucursalesFolios(GlobalIdSucursalDefault, MySqlcon)
        InitializeComponent()
        Me.comprobante = comprobante
        proveedores = New dbproveedores(MySqlcon)
        detalles = New dbSemillasDetalleComprobante(MySqlcon)
        comprobantes = New dbSemillasComprobante(MySqlcon)
        llenarDatosComprobante()
        idSucursal = comprobante.idSucursal
        llenaDatosEmpresa()
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
    End Sub

    'Private Sub ExportToExcel(ByVal dtTemp As DataTable, ByVal filepath As String)
    '    Dim strFileName As String = filepath
    '    If System.IO.File.Exists(strFileName) Then
    '        If (MessageBox.Show("eEl archivo ya existe, ¿desea reemplezarlo?", "Exportar a Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.Yes) Then
    '            System.IO.File.Delete(strFileName)
    '        Else
    '            Return
    '        End If

    '    End If
    '    Dim _excel As New Application
    '    Dim wBook As Excel.Workbook
    '    Dim wSheet As Excel.Worksheet

    '    wBook = _excel.Workbooks.Add()
    '    wSheet = wBook.ActiveSheet()

    '    Dim dt As System.Data.DataTable = dtTemp
    '    Dim dc As System.Data.DataColumn
    '    Dim dr As System.Data.DataRow
    '    Dim colIndex As Integer = 0
    '    Dim rowIndex As Integer = 0
    '    For Each dr In dt.Rows
    '        rowIndex = rowIndex + 1
    '        colIndex = 0
    '        For Each dc In dt.Columns
    '            colIndex = colIndex + 1
    '            wSheet.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
    '        Next
    '    Next
    '    wSheet.Columns.AutoFit()
    '    wBook.SaveAs(strFileName)

    '    ReleaseObject(wSheet)
    '    wBook.Close(False)
    '    ReleaseObject(wBook)
    '    _excel.Quit()
    '    ReleaseObject(_excel)
    '    GC.Collect()

    '    MessageBox.Show("Archivo exportado!")
    'End Sub
    'Private Sub ReleaseObject(ByVal o As Object)
    '    Try
    '        While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
    '        End While
    '    Catch
    '    Finally
    '        o = Nothing
    '    End Try
    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        buscarCliente()
    End Sub

    Private Sub buscarCliente()

        Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        frm.ShowDialog()
        If Not frm.Cliente Is Nothing Then
            cliente = frm.Cliente
            llenarDatos()
            txtApellidoP.Focus()
        End If
    End Sub

    Private Function idVentaFolio(ByVal folio As Integer) As Integer
        Dim comm As New MySqlCommand
        comm.Connection = MySqlcon
        comm.CommandText = "select ifnull(idventa,0) as id from tblventas where folio=" + folio.ToString() + ";"
        Dim i As Integer = comm.ExecuteScalar
        Return i
    End Function

    Private Sub llenaDatosEmpresa()
        Dim sucursal As New dbSucursales(idSucursal, MySqlcon)
        txtNombreReceptor.Text = sucursal.Nombre
        txtRFCReceptor.Text = sucursal.RFC
        txtDireccionReceptor.Text = sucursal.Direccion
        txtColoniaReceptor.Text = sucursal.Colonia
        txtMunicipioReceptor.Text = sucursal.Municipio
        txtCiudadReceptor.Text = sucursal.Ciudad
        txtEstadoReceptor.Text = sucursal.Estado
        txtTelReceptor.Text = sucursal.Telefono
        txtCPReceptor.Text = sucursal.CP
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        buscarFactura()
    End Sub

    Private Sub buscarFactura()
        Dim frm As New frmComprasConsulta(ModosDeBusqueda.Secundario, 0, 0)
        frm.ShowDialog()
        If frm.DialogResult = Windows.Forms.DialogResult.OK Then
            idCompra = frm.IdCompra
            llenaDatosFactura()
            btnModificar.Enabled = True
            btnAgregar.Enabled = True
            txtVolumenFacturado.Focus()
        End If
    End Sub

    Private Sub llenaDatosFactura()
        Dim op As New dbOpciones(MySqlcon)
        compras = New dbCompras(idCompra, MySqlcon)
        'ventas = New dbVentas(MySqlcon)

        txtFechaFactura.Text = convierteFecha(compras.Fecha)
        txtImporteFactura.Text = Format(compras.TotalaPagar, "#,###,##0")

        txtNumFactura.Text = compras.Referencia
        Dim d As New dbComprasDetalles(MySqlcon)
        Dim dr As MySqlDataReader
        dr = d.ConsultaReader(idCompra, 0)
        cantidad = 0
        While dr.Read()
            nombreProducto = dr("descripcion")
            cantidad += dr("cantidad")
        End While
        dr.Close()
        txtVolumenFacturado.Text = cantidad / 1000

    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.ComprobanteAlta, PermisosN.Secciones.Semillas) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        accionDetalle()
    End Sub

    Private Sub accionDetalle()
        detalle = New dbSemillasDetalleComprobante

        If op = AGREGAR Then
            agregarDetalle()
        ElseIf op = MODIFICAR Then
            modificarDetalle()
        End If
    End Sub

    Private Sub agregarDetalle()
        If txtCosechadas.Text <> "" And txtVolumen.Text <> "" Then
            Dim superficie = Double.Parse(txtCosechadas.Text)
            If superficie > 0 Then
                Dim volumen = Double.Parse(txtVolumen.Text)
                detalle.superficie = superficie
                detalle.descripcion = nombreProducto
                detalle.volumen = volumen
                detalle.idComprobante = comprobante.id
                detalle.rendimiento = Format((detalle.volumen / detalle.superficie), "0.00")
                detalle.guardado = False
                detalles.agregar(detalle)
                limpiaCampos()
                dgvComprobante.DataSource = detalles.vistaDetallesComprobante(comprobante.id)
                configuraGrid()
                txtCosechadas.BackColor = Color.White
                txtVolumen.BackColor = Color.White
            Else
                MsgBox("La superficie debe ser un número mayor de cero.", vbInformation, GlobalNombreApp)
                txtCosechadas.BackColor = Color.Red
                Return
            End If
        Else
            MsgBox("Debe llenar ambos campos.", MsgBoxStyle.Information, GlobalNombreApp)
            If txtCosechadas.Text = "" Then
                txtCosechadas.BackColor = Color.Red
            Else
                txtVolumen.BackColor = Color.Red
            End If
        End If
    End Sub

    Private Sub modificarDetalle()
        detalle.id = idDetalle
        If txtCosechadas.Text <> "" And txtVolumen.Text <> "" Then
            Dim superficie = Double.Parse(txtCosechadas.Text)
            Dim volumen = Double.Parse(txtVolumen.Text)
            If superficie > 0 Then
                detalle.superficie = superficie
                detalle.descripcion = nombreProducto
                detalle.volumen = volumen
                detalle.idComprobante = comprobante.id
                detalle.rendimiento = detalle.volumen / detalle.superficie
                detalles.actualizar(detalle)
                limpiaCampos()
                dgvComprobante.DataSource = detalles.vistaDetallesComprobante(comprobante.id)
                configuraGrid()
                op = AGREGAR
                btnAgregar.Text = "Agregar"
            Else
                MsgBox("La superficie debe ser un número mayor que 0.")
                Return
            End If
        Else
            MsgBox("Debe llenar ambos campos.")
        End If
    End Sub

    Private Sub eliminarDetalle()
        detalles.eliminar(idDetalle)
        dgvComprobante.DataSource = detalles.vistaDetallesComprobante(comprobante.id)
        configuraGrid()
        btnAgregar.Text = "Agregar"
        op = AGREGAR
        limpiaCampos()
    End Sub

    Private Sub limpiaCampos()
        txtVolumen.Text = ""
        txtCosechadas.Text = ""
    End Sub

    Private Sub frmSemillasComprobante_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        dgvComprobante.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        comprobantes.eliminaSinGuardar()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        guardar()
    End Sub

    Private Function guardar() As Boolean

        If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.ComprobanteAlta, PermisosN.Secciones.Semillas) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Function
        End If
        If IsNumeric(txtVolumenFacturado.Text) = False Then
            MsgBox("Debe indicar el volumen facturado.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Function
        End If
        If Not cliente Is Nothing And Not compras Is Nothing Then
            If checaCampos() Then
                If detalles.sumaVolumen(comprobante.id) = CDbl(txtVolumenFacturado.Text) Then
                    If IsNumeric(txtFolio.Text) And txtFolio.Text <> "" Then
                        If comprobante.estado = Estados.Inicio Then
                            If comprobantes.checaFolioRepetido(txtSerie.Text, CInt(txtFolio.Text)) = False Then
                                comprobante.idCliente = cliente.ID
                                comprobante.riego = Integer.Parse(txtRiego.Text)
                                comprobante.socioPersonaMoral = txtSocio.Text
                                comprobante.superficie = detalles.sumaSuperficies(comprobante.id)
                                comprobante.idCompra = compras.ID
                                comprobante.serie = txtSerie.Text
                                comprobante.folio = txtFolio.Text
                                comprobante.nombreRepresentante = txtNombreMoral.Text
                                comprobante.apellidoP = txtApellidoP.Text
                                comprobante.apellidoM = txtApellidoM.Text
                                comprobante.numComprobante = txtSerie.Text + " " + txtFolio.Text
                                comprobante.volumen = CDbl(txtVolumenFacturado.Text)
                                comprobante.estado = Estados.Guardada
                                comprobante.idSucursal = idSucursal
                                txtVolumen.BackColor = Color.White
                                comprobante.Fecha = DateTimePicker1.Value.ToString("yyyy/MM/dd")
                                comprobantes.actualiza(comprobante)
                                detalles.guardar(comprobante.id)
                                PopUp("Guardado", 30)
                                Nuevo()
                                Return True
                            Else
                                MsgBox("Folio repetido.")
                                Return False
                            End If
                        Else
                            If serie = txtSerie.Text And CInt(txtFolio.Text) = folio Then
                                comprobante.idCliente = cliente.ID
                                comprobante.riego = Integer.Parse(txtRiego.Text)
                                comprobante.socioPersonaMoral = txtSocio.Text
                                comprobante.superficie = detalles.sumaSuperficies(comprobante.id)
                                comprobante.idCompra = compras.ID
                                comprobante.serie = txtSerie.Text
                                comprobante.folio = txtFolio.Text
                                comprobante.nombreRepresentante = txtNombreMoral.Text
                                comprobante.apellidoP = txtApellidoP.Text
                                comprobante.apellidoM = txtApellidoM.Text
                                comprobante.numComprobante = txtSerie.Text + " " + txtFolio.Text
                                comprobante.volumen = CDbl(txtVolumenFacturado.Text)
                                comprobante.estado = Estados.Guardada
                                comprobante.idSucursal = idSucursal
                                comprobante.Fecha = DateTimePicker1.Value.ToString("yyyy/MM/dd")
                                txtVolumen.BackColor = Color.White
                                comprobantes.actualiza(comprobante)
                                detalles.guardar(comprobante.id)
                                PopUp("Guardado", 30)
                                Nuevo()
                                Return True
                            Else
                                If comprobantes.checaFolioRepetido(txtSerie.Text, CInt(txtFolio.Text)) = False Then
                                    comprobante.idCliente = cliente.ID
                                    comprobante.riego = Integer.Parse(txtRiego.Text)
                                    comprobante.socioPersonaMoral = txtSocio.Text
                                    comprobante.superficie = detalles.sumaSuperficies(comprobante.id)
                                    comprobante.idCompra = compras.ID
                                    comprobante.serie = txtSerie.Text
                                    comprobante.folio = txtFolio.Text
                                    comprobante.nombreRepresentante = txtNombreMoral.Text
                                    comprobante.apellidoP = txtApellidoP.Text
                                    comprobante.apellidoM = txtApellidoM.Text
                                    comprobante.numComprobante = txtSerie.Text + " " + txtFolio.Text
                                    comprobante.volumen = CDbl(txtVolumenFacturado.Text)
                                    comprobante.estado = Estados.Guardada
                                    comprobante.idSucursal = idSucursal
                                    comprobante.Fecha = DateTimePicker1.Value.ToString("yyyy/MM/dd")
                                    txtVolumen.BackColor = Color.White
                                    comprobantes.actualiza(comprobante)
                                    detalles.guardar(comprobante.id)
                                    PopUp("Guardado", 30)
                                    Nuevo()
                                    Return True
                                End If
                            End If
                        End If
                    Else
                        MsgBox("El folio debe ser un número.")
                        Return False
                    End If
                Else
                    MsgBox("El volumen facturado y la suma de las cosechas deben ser iguales.")
                    txtVolumen.BackColor = Color.Red
                    Return False
                End If
            Else
                MsgBox("Debe llenar todos los campos")
                Return False
            End If
        Else
            MsgBox("debe indicar un cliente y una factura.")
            Return False
        End If
    End Function

    Private Sub llenarDatosComprobante()
        llenaDatosEmpresa()
        idCompra = comprobante.idCompra
        llenaDatosFactura()
        cliente = New dbClientes(comprobante.idCliente, MySqlcon)
        llenarDatos()
        txtRiego.Text = comprobante.riego
        txtSocio.Text = comprobante.socioPersonaMoral
        txtSerie.Text = comprobante.serie
        txtFolio.Text = comprobante.folio
        txtNombreMoral.Text = comprobante.nombreRepresentante
        txtApellidoP.Text = comprobante.apellidoP
        txtApellidoM.Text = comprobante.apellidoM
        txtVolumenFacturado.Text = comprobante.volumen.ToString
        ComboBox3.SelectedIndex = IdsSucursales.Busca(comprobante.idSucursal)
        DateTimePicker1.Value = comprobante.Fecha
        dgvComprobante.DataSource = detalles.vistaDetallesComprobante(comprobante.id)
        configuraGrid()
    End Sub

    Private Sub configuraGrid()
        dgvComprobante.Columns(0).Visible = False
    End Sub

    Private Sub dgvComprobante_MouseClick(sender As Object, e As MouseEventArgs) Handles dgvComprobante.MouseClick
        Try
            Dim id = Integer.Parse(dgvComprobante.CurrentRow.Cells(0).Value)
            btnAgregar.Text = "Guardar Cambio"
            btnModificar.Enabled = True
            op = MODIFICAR
            Dim d As dbSemillasDetalleComprobante = detalles.buscar(id)
            muestraDetalle(d)
        Catch ex As Exception
            Return
        End Try
    End Sub

    Private Sub muestraDetalle(ByVal detalle As dbSemillasDetalleComprobante)
        txtCosechadas.Text = detalle.superficie
        txtVolumen.Text = detalle.volumen
        idDetalle = detalle.id
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        If comprobante.estado = Estados.Inicio Then
            Dim aux As Integer = comprobante.id
            If guardar() Then
                comprobantes.reporteComprobante(aux)
            Else
                MsgBox("Debe guardar los cambios para poder exportar el comprobante.")
            End If
        ElseIf comprobante.estado = Estados.Guardada Then
            comprobantes.reporteComprobante(comprobante.id)
        End If
        'comprobantes.reporteComprobante(comprobante.id)
        'comprobantes.exportar(comprobante.id)
    End Sub

    Private Sub txtVolumen_KeyDown(sender As Object, e As KeyEventArgs) Handles txtVolumen.KeyDown
        If e.KeyCode = Keys.Enter Then
            accionDetalle()
        End If
    End Sub

    Private Sub Button2btnBuscarC_Click(sender As Object, e As EventArgs) Handles Button2btnBuscarC.Click
        Dim frm As New frmSemillasConsulta(frmSemillasConsulta.tipoConsulta.comprobantes)
        frm.ShowDialog()
        If Not frm.comprobante Is Nothing Then
            If comprobante.estado = Estados.Inicio Then
                comprobantes.eliminar(comprobante.id)
            End If
            op = MODIFICAR
            comprobante = frm.comprobante
            serie = comprobante.serie
            folio = comprobante.folio
            llenarDatosComprobante()
            ComboBox3.Enabled = False
            btnModificar.Enabled = True
            btnAgregar.Enabled = True
        End If
    End Sub
    Public Sub buscaFolio()
        If sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.SemillasComprobante, 0) Then
            txtSerie.Text = sf.Serie
        Else
            txtSerie.Text = ""
            sf.FolioInicial = 1
        End If
        txtFolio.Text = comprobantes.DaNuevoFolio(txtSerie.Text)
        If CInt(txtFolio.Text) < sf.FolioInicial Then
            txtFolio.Text = sf.FolioInicial.ToString
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        buscaFolio()
        idSucursal = IdsSucursales.Valor(ComboBox3.SelectedIndex)
        llenaDatosEmpresa()
    End Sub

    Private Sub dgvComprobante_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgvComprobante.MouseDoubleClick
        Dim result As Integer = MessageBox.Show("¿Desea elimnar esta superficie?", GlobalNombreApp, MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Yes Then
            Try
                Dim id = Integer.Parse(dgvComprobante.CurrentRow.Cells(0).Value)
                eliminarDetalle()
            Catch ex As Exception
                Return
            End Try
        End If
    End Sub

    Private Sub Nuevo()
        If comprobante.estado = Estados.Inicio Then
            If Not cliente Is Nothing Then
                Dim result As Integer = MessageBox.Show("¿Desea guardar los cambios?", GlobalNombreApp, MessageBoxButtons.YesNoCancel)
                If result = DialogResult.Cancel Then
                    Return
                ElseIf DialogResult.No Then
                    idSucursal = IdsSucursales.Valor(ComboBox3.SelectedIndex)
                    If Not cliente Is Nothing And Not compras Is Nothing Then
                        comprobante.estado = Estados.SinGuardar
                        comprobante.idSucursal = idSucursal
                        comprobante.idCliente = cliente.ID
                        comprobante.idCompra = compras.ID
                        detalles.borraSinGuardar(comprobante.id)
                        If comprobante.estado = Estados.Inicio Then
                            comprobantes.eliminar(comprobante.id)
                        End If
                    End If
                    ComboBox3.Enabled = True
                ElseIf DialogResult.Yes Then
                    guardar()
                    ComboBox3.Enabled = True
                End If
            End If
            comprobante = New dbSemillasComprobante(comprobantes.crearComprobante(), MySqlcon)
            op = AGREGAR
            cliente = Nothing
            compras = Nothing
            txtRazonSocial.Text = ""
            txtRFCmoral.Text = ""
            txtNombreMoral.Text = ""
            txtDireccionMoral.Text = ""
            txtMunicipioMoral.Text = ""
            txtCiudadMoral.Text = ""
            txtEstadoMoral.Text = ""
            txtApellidoM.Text = ""
            txtApellidoP.Text = ""
            txtColoniaMoral.Text = ""
            txtCPMoral.Text = ""
            txtTelMoral.Text = ""
            txtNumFactura.Text = ""
            txtVolumenFacturado.Text = ""
            txtFechaFactura.Text = ""
            txtRiego.Text = ""
            txtSocio.Text = ""
            txtImporteFactura.Text = ""
            serie = ""
            DateTimePicker1.Value = Date.Now
            folio = -1
            limpiaCampos()
            dgvComprobante.Columns.Clear()
            ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
            buscaFolio()
            ComboBox3.Enabled = True
        Else
            comprobante = New dbSemillasComprobante(comprobantes.crearComprobante(), MySqlcon)
            op = AGREGAR
            cliente = Nothing
            compras = Nothing
            ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
            txtRazonSocial.Text = ""
            txtRFCmoral.Text = ""
            txtNombreMoral.Text = ""
            txtDireccionMoral.Text = ""
            txtMunicipioMoral.Text = ""
            txtCiudadMoral.Text = ""
            txtEstadoMoral.Text = ""
            txtApellidoM.Text = ""
            txtApellidoP.Text = ""
            txtColoniaMoral.Text = ""
            txtCPMoral.Text = ""
            txtTelMoral.Text = ""
            txtNumFactura.Text = ""
            txtVolumenFacturado.Text = ""
            txtFechaFactura.Text = ""
            txtRiego.Text = ""
            txtSocio.Text = ""
            txtImporteFactura.Text = ""
            DateTimePicker1.Value = Date.Now
            serie = ""
            folio = -1
            limpiaCampos()
            dgvComprobante.Columns.Clear()
            buscaFolio()
            ComboBox3.Enabled = True
        End If
    End Sub


    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Nuevo()
    End Sub

    Private Sub frmSemillasComprobante_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not cliente Is Nothing Then
            Dim result As Integer = MessageBox.Show("¿Desea guardar los cambios?", GlobalNombreApp, MessageBoxButtons.YesNoCancel)
            If result = DialogResult.Cancel Then
                e.Cancel = True
            ElseIf DialogResult.No Then
                idSucursal = IdsSucursales.Valor(ComboBox3.SelectedIndex)
                If Not cliente Is Nothing And Not compras Is Nothing Then
                    comprobante.estado = Estados.SinGuardar
                    comprobante.idSucursal = idSucursal
                    comprobante.idCliente = cliente.ID
                    comprobante.idCompra = compras.ID
                    'comprobantes.actualiza(comprobante)
                    detalles.borraSinGuardar(comprobante.id)
                    If comprobante.estado = Estados.Inicio Then
                        comprobantes.eliminar(comprobante.id)
                    End If
                End If
                e.Cancel = False
            ElseIf DialogResult.Yes Then
                guardar()
                e.Cancel = False
            End If
        Else
            comprobantes.eliminar(comprobante.id)
        End If

    End Sub

    Private Sub txtRiego_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRiego.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Function checaCampos() As Boolean
        Dim res = True
        If txtRiego.Text = "" Then
            res = False
            txtRiego.BackColor = Color.Red
        Else
            txtRiego.BackColor = Color.White
        End If

        If txtSocio.Text = "" Then
            res = False
            txtSocio.BackColor = Color.Red
        Else
            txtSocio.BackColor = Color.White
        End If
        Return res
    End Function

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        limpiaCampos()
        btnAgregar.Text = "Agregar"
        op = AGREGAR
    End Sub

   

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

   
    Private Sub txtRazonSocial_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRazonSocial.KeyDown
        If e.KeyCode = Keys.F1 Then
            buscarCliente()
        End If
    End Sub

    Private Sub txtNumFactura_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNumFactura.KeyDown
        If e.KeyCode = Keys.F1 Then
            buscarFactura()
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
End Class